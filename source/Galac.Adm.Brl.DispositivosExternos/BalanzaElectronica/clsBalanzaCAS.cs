using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Galac.Adm.Ccl.DispositivosExternos;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using System.Threading;

namespace Galac.Adm.Brl.DispositivosExternos.BalanzaElectronica {
    class clsBalanzaCAS: clsBalanza {

        public static string NO_VALIDO = "NO_VALIDO";
        private string _Respuesta = "";
        private bool _PuertoEstaAbierto;

        public clsBalanzaCAS(clsConexionPuertoSerial conexion, eModeloDeBalanza valModelo)
            : base(conexion) {
                ModeloAsEnum = valModelo;
        }

        #region Comunicacion Basico
        private bool enviarComandoBalanza(string valComando) {
            bool enviado = false;
            enviado = base.Conexion.enviarDatosSync(valComando,false);
            _Respuesta = LeerPeso();
            return enviado;
        }     
        #endregion
        #region Comandos Preestablecidos
        private bool traerPeso() {
            return enviarComandoBalanza("\u0011");
        }

        public override bool colocarEnCero() {
            return true;
        }

        public override bool VerficarEstado() {
            bool vChecked = false;
            string vRequest = string.Empty;
            if(abrirConexion()) {
                vChecked = base.Conexion.enviarDatosSync("\u0005",true);
                Thread.Sleep(350);
                vRequest = base.Conexion.recibirDatos();                
                vChecked = vChecked && LibString.S1IsInS2("\u0006",vRequest);
                cerrarConexion();
            }
            return vChecked;
        }
        #endregion
        #region Respuestas

        public static string analizarRespuesta(string respuesta) {
            string resultado = LibText.UCase(respuesta);
            int vPosInit = 0;
            int vPosEnd = 0;
            if(!LibString.IsNullOrEmpty(resultado)) {
                vPosInit = LibString.IndexOf(resultado,'\u0002')+2;
                vPosEnd = LibString.IndexOf(resultado,'\u0003')-vPosInit-1;
                if(vPosInit >= 0 && vPosEnd > vPosInit) {
                    resultado = LibString.Trim(LibString.SubString(resultado,vPosInit,vPosEnd));
                }
            }           
            return resultado;
        }

        private string LeerPeso() {
            string vResult = "";
            vResult = base.Conexion.recibirDatos();
            vResult = analizarRespuesta(vResult);
            if (LibText.S1IsInS2(NO_VALIDO, vResult)) {
                cerrarConexion();
                throw new GalacException("Datos Inválidos ", eExceptionManagementType.Validation);
            }
            return vResult;
        }       

        public override bool abrirConexion() {
            bool vResult = false;
            vResult = base.Conexion.abrirConexion();
            _PuertoEstaAbierto = vResult;
            Thread.Sleep(100);
            return vResult;
        }

        public override bool cerrarConexion() {
            bool vResult = false;
            base.Conexion.liberarBuffer();
            vResult = base.Conexion.cerrarConexion();
            _PuertoEstaAbierto = false;
            Thread.Sleep(100);
            return vResult;
        }
        #endregion

        public override string GetPeso() {
            string vResult = "";
            traerPeso();
            vResult = LeerPeso();
            return vResult;
        }

       
    }
}
