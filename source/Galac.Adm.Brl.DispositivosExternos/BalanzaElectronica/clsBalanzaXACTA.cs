using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Galac.Adm.Ccl.DispositivosExternos;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using System.Threading;

namespace Galac.Adm.Brl.DispositivosExternos.BalanzaElectronica {
    class clsBalanzaXACTA : clsBalanza {

        public static string NO_VALIDO = "NO_VALIDO";
        private string _Respuesta = "";
        private bool _PuertoEstaAbierto;

        public clsBalanzaXACTA(clsConexionPuertoSerial conexion, eModeloDeBalanza valModelo)
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
            return enviarComandoBalanza("W\r");
        }
        public override bool colocarEnCero() {
            return base.Conexion.enviarDatosASync("Z\r");
        }

        public override bool VerficarEstado() {
            bool vChecked = false;
            string vRequest = string.Empty;
            if(abrirConexion()) {
                if(ModeloAsEnum == eModeloDeBalanza.Xacta) {
                    vChecked = base.Conexion.enviarDatosSync("S\r",false);
                    Thread.Sleep(350);
                    vRequest = base.Conexion.recibirDatos();
                    vChecked = vChecked && LibString.S1IsInS2("S00",vRequest);
                } else if(ModeloAsEnum == eModeloDeBalanza.AclasOS2X) {
                    vChecked = base.Conexion.enviarDatosSync("W",false);
                    Thread.Sleep(350);
                    vRequest = base.Conexion.recibirDatos();
                    vChecked = vChecked && vRequest != string.Empty;
                }
                cerrarConexion();
            }    
            return vChecked;
        }
        #endregion
        #region Respuestas

        public static string analizarRespuesta(string respuesta) {
            string resultado = LibText.UCase(respuesta);
            if(LibText.S1IsInS2("KG",respuesta)) {
                resultado = resultado.Substring(1, resultado.IndexOf("G"));
            } else {
                resultado = NO_VALIDO;
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
