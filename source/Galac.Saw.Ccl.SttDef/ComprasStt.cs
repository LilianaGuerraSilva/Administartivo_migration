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
    public class ComprasStt : ISettDefinition {
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
        private bool _GenerarCxPdesdeCompra;
        private bool _ImprimirOrdenDeCompra;
        private string _NombrePlantillaOrdenDeCompra;
        private string _NombrePlantillaImpresionCodigoBarrasCompras;
        private bool _IvaEsCostoEnCompras;
        private bool _ImprimirCompraAlInsertar;
        private string _NombrePlantillaCompra;
        private bool _SugerirNumeroDeOrdenDeCompra;
        private long _fldTimeStamp;
        XmlDocument _datos;
        #endregion //Variables
        #region Propiedades

        public bool GenerarCxPdesdeCompraAsBool {
            get { return _GenerarCxPdesdeCompra; }
            set { _GenerarCxPdesdeCompra = value; }
        }

        public string GenerarCxPdesdeCompra {
            set { _GenerarCxPdesdeCompra = LibConvert.SNToBool(value); }
        }

        public bool ImprimirOrdenDeCompraAsBool {
            get { return _ImprimirOrdenDeCompra; }
            set { _ImprimirOrdenDeCompra = value; }
        }

        public string ImprimirOrdenDeCompra {
            set { _ImprimirOrdenDeCompra = LibConvert.SNToBool(value); }
        }


        public string NombrePlantillaOrdenDeCompra {
            get { return _NombrePlantillaOrdenDeCompra; }
            set { _NombrePlantillaOrdenDeCompra = LibString.Mid(value, 0, 50); }
        }

        public string NombrePlantillaImpresionCodigoBarrasCompras
        {
            get { return _NombrePlantillaImpresionCodigoBarrasCompras; }
            set { _NombrePlantillaImpresionCodigoBarrasCompras = LibString.Mid(value, 0, 50); }
        }

        public bool IvaEsCostoEnComprasAsBool {
            get { return _IvaEsCostoEnCompras; }
            set { _IvaEsCostoEnCompras = value; }
        }

        public string IvaEsCostoEnCompras {
            set { _IvaEsCostoEnCompras = LibConvert.SNToBool(value); }
        }


        public bool ImprimirCompraAlInsertarAsBool {
            get { return _ImprimirCompraAlInsertar; }
            set { _ImprimirCompraAlInsertar = value; }
        }

        public string ImprimirCompraAlInsertar {
            set { _ImprimirCompraAlInsertar = LibConvert.SNToBool(value); }
        }


        public string NombrePlantillaCompra {
            get { return _NombrePlantillaCompra; }
            set { _NombrePlantillaCompra = LibString.Mid(value, 0, 50); }
        }

        public long fldTimeStamp {
            get { return _fldTimeStamp; }
            set { _fldTimeStamp = value; }
        }

        public XmlDocument Datos {
            get { return _datos; }
            set { _datos = value; }
        }

        public bool SugerirNumeroDeOrdenDeCompraAsBool {
            get { return _SugerirNumeroDeOrdenDeCompra; }
            set { _SugerirNumeroDeOrdenDeCompra = value; }
        }
        public string SugerirNumeroDeOrdenDeCompra {
            set { _SugerirNumeroDeOrdenDeCompra = LibConvert.SNToBool(value); }
        }
        #endregion //Propiedades
        #region Constructores

        public ComprasStt() {
            Clear();
        }
        #endregion //Constructores
        #region Metodos Generados

        public object TextDateLastModifiedForInput() {
            return "";
        }

        public void Clear() {
            GenerarCxPdesdeCompraAsBool = false;
            ImprimirOrdenDeCompraAsBool = false;
            NombrePlantillaOrdenDeCompra = "";
            NombrePlantillaImpresionCodigoBarrasCompras = "";
            IvaEsCostoEnComprasAsBool = false;
            ImprimirCompraAlInsertarAsBool = false;
            NombrePlantillaCompra = "";
            SugerirNumeroDeOrdenDeCompraAsBool = false;
            fldTimeStamp = 0;
        }

        public ComprasStt Clone() {
            ComprasStt vResult = new ComprasStt();
            vResult.GenerarCxPdesdeCompraAsBool = _GenerarCxPdesdeCompra;
            vResult.ImprimirOrdenDeCompraAsBool = _ImprimirOrdenDeCompra;
            vResult.NombrePlantillaOrdenDeCompra = _NombrePlantillaOrdenDeCompra;
            vResult.NombrePlantillaOrdenDeCompra = _NombrePlantillaImpresionCodigoBarrasCompras;
            vResult.IvaEsCostoEnComprasAsBool = _IvaEsCostoEnCompras;
            vResult.ImprimirCompraAlInsertarAsBool = _ImprimirCompraAlInsertar;
            vResult.NombrePlantillaCompra = _NombrePlantillaCompra;
            vResult.SugerirNumeroDeOrdenDeCompraAsBool = _SugerirNumeroDeOrdenDeCompra;
            vResult.fldTimeStamp = _fldTimeStamp;
            return vResult;
        }

        public override string ToString() {
           return "Generar CxP desde Compra = " + _GenerarCxPdesdeCompra +
               "\nImprimirOrdenDeCompra = " + _ImprimirOrdenDeCompra +
               "\nNombre Plantilla Orden De Compra = " + _NombrePlantillaOrdenDeCompra +
               "\nNombre Plantilla de Etiquetas Para Códigos De Barras En Compra = " + _NombrePlantillaImpresionCodigoBarrasCompras +
               "\nIva Es Costo En Compras = " + _IvaEsCostoEnCompras +
               "\nImprimir Compra Al Insertar = " + _ImprimirCompraAlInsertar +
               "\nNombre Plantilla Compra = " + _NombrePlantillaCompra +
               "\nSugerir Número de Orden de Compra = " + _SugerirNumeroDeOrdenDeCompra;
        }
        #endregion //Metodos Generados


    } //End of class ComprasStt

} //End of namespace Galac.Saw.Ccl.SttDef

