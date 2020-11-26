using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using Galac.Saw.Ccl.SttDef;

namespace Galac.Saw.Ccl.SttDef {
    [Serializable]
    public class NotaEntradaSalidaStt : ISettDefinition {
        private string _GroupName = null;
        private string _Module = null;

        public string GroupName {
            get { return _GroupName; }
            set { _GroupName = value; }
        }

        public string Module {
            get { return _Module; }
            set { _Module = value; }
        }
        #region Variables
        private bool _ImprimirReporteAlIngresarNotaEntradaSalida;
        private string _NombrePlantillaNotaEntradaSalida;
        private string _NombrePlantillaCodigoDeBarras;
        private bool _ImprimirNotaESconPrecio;
        private long _fldTimeStamp;
        XmlDocument _datos;
        #endregion //Variables
        #region Propiedades

        public bool ImprimirReporteAlIngresarNotaEntradaSalidaAsBool {
            get { return _ImprimirReporteAlIngresarNotaEntradaSalida; }
            set { _ImprimirReporteAlIngresarNotaEntradaSalida = value; }
        }

        public string ImprimirReporteAlIngresarNotaEntradaSalida {
            set { _ImprimirReporteAlIngresarNotaEntradaSalida = LibConvert.SNToBool(value); }
        }


        public string NombrePlantillaNotaEntradaSalida {
            get { return _NombrePlantillaNotaEntradaSalida; }
            set { _NombrePlantillaNotaEntradaSalida = LibString.Mid(value, 0, 50); }
        }

        public string NombrePlantillaCodigoDeBarras {
            get { return _NombrePlantillaCodigoDeBarras; }
            set { _NombrePlantillaCodigoDeBarras = LibString.Mid(value, 0, 50); }
        }


        public bool ImprimirNotaESconPrecioAsBool {
            get { return _ImprimirNotaESconPrecio; }
            set { _ImprimirNotaESconPrecio = value; }
        }

        public string ImprimirNotaESconPrecio {
            set { _ImprimirNotaESconPrecio = LibConvert.SNToBool(value); }
        }


        public long fldTimeStamp {
            get { return _fldTimeStamp; }
            set { _fldTimeStamp = value; }
        }

        public XmlDocument Datos {
            get { return _datos; }
            set { _datos = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public NotaEntradaSalidaStt() {
            Clear();
        }
        #endregion //Constructores
        #region Metodos Generados

        public object TextDateLastModifiedForInput() {
            return "";
        }

        public void Clear() {
            ImprimirReporteAlIngresarNotaEntradaSalidaAsBool = false;
            NombrePlantillaNotaEntradaSalida = "";
            NombrePlantillaCodigoDeBarras = "";
            ImprimirNotaESconPrecioAsBool = false;
            fldTimeStamp = 0;
        }

        public NotaEntradaSalidaStt Clone() {
            NotaEntradaSalidaStt vResult = new NotaEntradaSalidaStt();
            vResult.ImprimirReporteAlIngresarNotaEntradaSalidaAsBool = _ImprimirReporteAlIngresarNotaEntradaSalida;
            vResult.NombrePlantillaNotaEntradaSalida = _NombrePlantillaNotaEntradaSalida;
            vResult.NombrePlantillaCodigoDeBarras = _NombrePlantillaCodigoDeBarras;
            vResult.ImprimirNotaESconPrecioAsBool = _ImprimirNotaESconPrecio;
            vResult.fldTimeStamp = _fldTimeStamp;
            return vResult;
        }

        public override string ToString() {
           return "Imprimir Reporte Al Ingresar Nota Entrada Salida = " + _ImprimirReporteAlIngresarNotaEntradaSalida +
               "\nNombre Plantilla Nota Entrada Salida = " + _NombrePlantillaNotaEntradaSalida +
               "\nNombre Plantilla Codigo De Barras = " + _NombrePlantillaCodigoDeBarras +
               "\nImprimir NotaES con Precio = " + _ImprimirNotaESconPrecio;
        }
        #endregion //Metodos Generados


    } //End of class NotaEntradaSalidaStt

} //End of namespace Galac.Saw.Ccl.SttDef

