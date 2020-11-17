using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Galac.Adm.Ccl.DispositivosExternos;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using System.Threading;

namespace Galac.Adm.Brl.DispositivosExternos.BalanzaElectronica {
    class clsBalanzaOHAUST23 : clsBalanza {

        public static string NO_VALIDO = "NO_VALIDO";
        private string _Respuesta = "";

        public clsBalanzaOHAUST23(clsConexionPuertoSerial conexion)
            : base(conexion) {

        }

        #region Comunicacion Basico
        private bool enviarComandoBalanza(string valComando) {
            bool enviado = false;
            enviado = base.Conexion.enviarDatosSync(valComando);
            _Respuesta = LeerPeso();
            enviado &= (_Respuesta != string.Empty);
            return enviado;
        }
        #endregion
        #region Comandos Preestablecidos
        private bool traerPeso() {
            return enviarComandoBalanza("P\r\n");
        }
        public override bool colocarEnCero() {
            return enviarComandoBalanza("Z\r\n");
        }

        public override bool VerficarEstado() {
            bool vEstado = false;
            if(abrirConexion()) {
                vEstado = enviarComandoBalanza("P\r\n");
                cerrarConexion();         
            }
            return vEstado;
        }
        #endregion
        #region Respuestas

        public static string analizarRespuesta(string respuesta) {
            String resultado = respuesta;
            if (LibText.S1IsInS2("Kg", respuesta)) {
                resultado = resultado.Substring(1, resultado.IndexOf("g"));
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
                throw new GalacException("Error de Datos", eExceptionManagementType.Alert);
            }
            return vResult;
        }

        public override bool abrirConexion() {
            bool vResult = false;
            vResult = base.Conexion.abrirConexion();
            Thread.Sleep(100);
            return vResult;
        }

        public override bool cerrarConexion() {
            bool vResult = false;
            base.Conexion.liberarBuffer();
            vResult = base.Conexion.cerrarConexion();
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
