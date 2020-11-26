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
    public class MetododecostosStt : ISettDefinition {
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
        private eTipoDeMetodoDeCosteo _MetodoDeCosteo;
        private DateTime _FechaDesdeUsoMetodoDeCosteo;
        private DateTime _FechaContabilizacionDeCosteo;
        private bool _ComprobanteCostoDetallado;
        private bool _CalculoAutomaticoDeCosto;
        private decimal _MaximoGastosAdmisibles;
        private long _fldTimeStamp;
        XmlDocument _datos;
        #endregion //Variables
        #region Propiedades

        public eTipoDeMetodoDeCosteo MetodoDeCosteoAsEnum {
            get { return _MetodoDeCosteo; }
            set { _MetodoDeCosteo = value; }
        }

        public string MetodoDeCosteo {
            set { _MetodoDeCosteo = (eTipoDeMetodoDeCosteo)LibConvert.DbValueToEnum(value); }
        }

        public string MetodoDeCosteoAsDB {
            get { return LibConvert.EnumToDbValue((int) _MetodoDeCosteo); }
        }

        public string MetodoDeCosteoAsString {
            get { return LibEnumHelper.GetDescription(_MetodoDeCosteo); }
        }

        public DateTime FechaDesdeUsoMetodoDeCosteo {
            get { return _FechaDesdeUsoMetodoDeCosteo; }
            set { _FechaDesdeUsoMetodoDeCosteo = LibConvert.DateToDbValue(value); }
        }

        public DateTime FechaContabilizacionDeCosteo {
            get { return _FechaContabilizacionDeCosteo; }
            set { _FechaContabilizacionDeCosteo = LibConvert.DateToDbValue(value); }
        }

        public bool ComprobanteCostoDetalladoAsBool {
            get { return _ComprobanteCostoDetallado; }
            set { _ComprobanteCostoDetallado = value; }
        }

        public string ComprobanteCostoDetallado {
            set { _ComprobanteCostoDetallado = LibConvert.SNToBool(value); }
        }


        public bool CalculoAutomaticoDeCostoAsBool {
            get { return _CalculoAutomaticoDeCosto; }
            set { _CalculoAutomaticoDeCosto = value; }
        }

        public string CalculoAutomaticoDeCosto {
            set { _CalculoAutomaticoDeCosto = LibConvert.SNToBool(value); }
        }

        public decimal MaximoGastosAdmisibles {
            get { return _MaximoGastosAdmisibles; }
            set { _MaximoGastosAdmisibles = value; }
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

        public MetododecostosStt() {
            Clear();
        }
        #endregion //Constructores
        #region Metodos Generados

        public object TextDateLastModifiedForInput() {
            return "";
        }

        public void Clear() {
            MetodoDeCosteoAsEnum = eTipoDeMetodoDeCosteo.UltimoCosto;
            FechaDesdeUsoMetodoDeCosteo = LibDate.Today();
            FechaContabilizacionDeCosteo = LibDate.Today();
            ComprobanteCostoDetalladoAsBool = false;
            CalculoAutomaticoDeCostoAsBool = false;
            MaximoGastosAdmisibles = 0;
            fldTimeStamp = 0;
        }

        public MetododecostosStt Clone() {
            MetododecostosStt vResult = new MetododecostosStt();
            vResult.MetodoDeCosteoAsEnum = _MetodoDeCosteo;
            vResult.FechaDesdeUsoMetodoDeCosteo = _FechaDesdeUsoMetodoDeCosteo;
            vResult.FechaContabilizacionDeCosteo = _FechaContabilizacionDeCosteo;
            vResult.ComprobanteCostoDetalladoAsBool = _ComprobanteCostoDetallado;
            vResult.CalculoAutomaticoDeCostoAsBool = _CalculoAutomaticoDeCosto;
            vResult.MaximoGastosAdmisibles = _MaximoGastosAdmisibles;
            vResult.fldTimeStamp = _fldTimeStamp;
            return vResult;
        }

        public override string ToString() {
            return "Metodo De Costeo = " + _MetodoDeCosteo.ToString() +
                "\nFecha Desde Uso Metodo De Costeo = " + _FechaDesdeUsoMetodoDeCosteo.ToShortDateString() +
                "\nFecha Contabilizacion De Costeo = " + _FechaContabilizacionDeCosteo.ToShortDateString() +
                "\nComprobante Costo Detallado = " + _ComprobanteCostoDetallado +
                "\nCalculo Automatico De Costo = " + _CalculoAutomaticoDeCosto +
                 "\nMaximo Gastos Admisibles = " + _MaximoGastosAdmisibles.ToString();
        }
        #endregion //Metodos Generados


    } //End of class MetododecostosStt

} //End of namespace Galac.Saw.Ccl.SttDef

