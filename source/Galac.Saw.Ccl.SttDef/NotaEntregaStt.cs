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
    public class NotaEntregaStt : ISettDefinition {
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
        private eModeloDeFactura _ModeloNotaEntrega;
        private bool _NotaEntregaPreNumerada;
        private string _PrimeraNotaEntrega;
        private eTipoDePrefijoFactura _TipoPrefijoNotaEntrega;
        private string _PrefijoNotaEntrega;
        private string _NombrePlantillaNotaEntrega;
        private string _NombrePlantillaOrdenDeDespacho;
        private int _NumCopiasOrdenDeDespacho;
        private string _ModeloNotaEntregaModoTexto;
        private long _fldTimeStamp;
        XmlDocument _datos;
        #endregion //Variables
        #region Propiedades

        public eModeloDeFactura ModeloNotaEntregaAsEnum {
            get { return _ModeloNotaEntrega; }
            set { _ModeloNotaEntrega = value; }
        }

        public string ModeloNotaEntrega {
            set { _ModeloNotaEntrega = (eModeloDeFactura)LibConvert.DbValueToEnum(value); }
        }

        public string ModeloNotaEntregaAsDB {
            get { return LibConvert.EnumToDbValue((int) _ModeloNotaEntrega); }
        }

        public string ModeloNotaEntregaAsString {
            get { return LibEnumHelper.GetDescription(_ModeloNotaEntrega); }
        }

        public bool NotaEntregaPreNumeradaAsBool {
            get { return _NotaEntregaPreNumerada; }
            set { _NotaEntregaPreNumerada = value; }
        }

        public string NotaEntregaPreNumerada {
            set { _NotaEntregaPreNumerada = LibConvert.SNToBool(value); }
        }


        public string PrimeraNotaEntrega {
            get { return _PrimeraNotaEntrega; }
            set { _PrimeraNotaEntrega = LibString.Mid(value, 0, 11); }
        }

        public eTipoDePrefijoFactura TipoPrefijoNotaEntregaAsEnum {
            get { return _TipoPrefijoNotaEntrega; }
            set { _TipoPrefijoNotaEntrega = value; }
        }

        public string TipoPrefijoNotaEntrega {
            set { _TipoPrefijoNotaEntrega = (eTipoDePrefijoFactura)LibConvert.DbValueToEnum(value); }
        }

        public string TipoPrefijoNotaEntregaAsDB {
            get { return LibConvert.EnumToDbValue((int) _TipoPrefijoNotaEntrega); }
        }

        public string TipoPrefijoNotaEntregaAsString {
            get { return LibEnumHelper.GetDescription(_TipoPrefijoNotaEntrega); }
        }

        public string PrefijoNotaEntrega {
            get { return _PrefijoNotaEntrega; }
            set { _PrefijoNotaEntrega = LibString.Mid(value, 0, 5); }
        }

        public string NombrePlantillaNotaEntrega {
            get { return _NombrePlantillaNotaEntrega; }
            set { _NombrePlantillaNotaEntrega = LibString.Mid(value, 0, 50); }
        }

        public string NombrePlantillaOrdenDeDespacho {
            get { return _NombrePlantillaOrdenDeDespacho; }
            set { _NombrePlantillaOrdenDeDespacho = LibString.Mid(value, 0, 50); }
        }

        public int NumCopiasOrdenDeDespacho {
            get { return _NumCopiasOrdenDeDespacho; }
            set { _NumCopiasOrdenDeDespacho = value; }
        }

        public string ModeloNotaEntregaModoTexto {
            get { return _ModeloNotaEntregaModoTexto; }
            set { _ModeloNotaEntregaModoTexto = LibString.Mid(value, 0, 50); }
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

        public NotaEntregaStt() {
            Clear();
        }
        #endregion //Constructores
        #region Metodos Generados

        public object TextDateLastModifiedForInput() {
            return "";
        }

        public void Clear() {
            ModeloNotaEntregaAsEnum = eModeloDeFactura.eMD_FORMALIBRE;
            NotaEntregaPreNumeradaAsBool = false;
            PrimeraNotaEntrega = "";
            TipoPrefijoNotaEntregaAsEnum = eTipoDePrefijoFactura.SinPrefijo;
            PrefijoNotaEntrega = "";
            NombrePlantillaNotaEntrega = "";
            NombrePlantillaOrdenDeDespacho = "";
            NumCopiasOrdenDeDespacho = 0;
            ModeloNotaEntregaModoTexto = "";
            fldTimeStamp = 0;
        }

        public NotaEntregaStt Clone() {
            NotaEntregaStt vResult = new NotaEntregaStt();
            vResult.ModeloNotaEntregaAsEnum = _ModeloNotaEntrega;
            vResult.NotaEntregaPreNumeradaAsBool = _NotaEntregaPreNumerada;
            vResult.PrimeraNotaEntrega = _PrimeraNotaEntrega;
            vResult.TipoPrefijoNotaEntregaAsEnum = _TipoPrefijoNotaEntrega;
            vResult.PrefijoNotaEntrega = _PrefijoNotaEntrega;
            vResult.NombrePlantillaNotaEntrega = _NombrePlantillaNotaEntrega;
            vResult.NombrePlantillaOrdenDeDespacho = _NombrePlantillaOrdenDeDespacho;
            vResult.NumCopiasOrdenDeDespacho = _NumCopiasOrdenDeDespacho;
            vResult.ModeloNotaEntregaModoTexto = _ModeloNotaEntregaModoTexto;
            vResult.fldTimeStamp = _fldTimeStamp;
            return vResult;
        }

        public override string ToString() {
           return "Modelo Nota Entrega = " + _ModeloNotaEntrega.ToString() +
               "\nNota Entrega Pre Numerada = " + _NotaEntregaPreNumerada +
               "\nPrimera Nota Entrega = " + _PrimeraNotaEntrega +
               "\nTipoPrefijoNotaEntrega = " + _TipoPrefijoNotaEntrega.ToString() +
               "\nPrefijo Nota Entrega = " + _PrefijoNotaEntrega +
               "\nNombre Plantilla Nota Entrega = " + _NombrePlantillaNotaEntrega +
               "\nNombre Plantilla Orden De Despacho = " + _NombrePlantillaOrdenDeDespacho +
               "\nNum Copias Orden De Despacho = " + _NumCopiasOrdenDeDespacho.ToString() +
               "\nModelo Nota de Entrega Modo Texto = " + _ModeloNotaEntregaModoTexto;
        }
        #endregion //Metodos Generados


    } //End of class NotaEntregaStt

} //End of namespace Galac.Saw.Ccl.SttDef

