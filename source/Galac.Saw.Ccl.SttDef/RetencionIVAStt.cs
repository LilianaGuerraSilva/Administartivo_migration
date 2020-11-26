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
    public class RetencionIVAStt : ISettDefinition {
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
        private eDondeSeEfectuaLaRetencionIVA _EnDondeRetenerIVA;
        private bool _UsaMismoNumeroCompRetTodasCxP;
        private int _PrimerNumeroComprobanteRetIVA;
        private eFormaDeReiniciarComprobanteRetIVA _FormaDeReiniciarElNumeroDeComprobanteRetIVA;
        private bool _ImprimirComprobanteDeRetIVA;
        private int _NumeroDeCopiasComprobanteRetencionIVA;
        private bool _UnComprobanteDeRetIVAPorHoja;
        private string _NombrePlantillaComprobanteDeRetIVA;
        private bool _GenerarNumCompDeRetIVASoloSiPorcentajeEsMayorACero;
        private long _fldTimeStamp;
        XmlDocument _datos;
        #endregion //Variables
        #region Propiedades

        public eDondeSeEfectuaLaRetencionIVA EnDondeRetenerIVAAsEnum {
            get { return _EnDondeRetenerIVA; }
            set { _EnDondeRetenerIVA = value; }
        }

        public string EnDondeRetenerIVA {
            set { _EnDondeRetenerIVA = (eDondeSeEfectuaLaRetencionIVA)LibConvert.DbValueToEnum(value); }
        }

        public string EnDondeRetenerIVAAsDB {
            get { return LibConvert.EnumToDbValue((int) _EnDondeRetenerIVA); }
        }

        public string EnDondeRetenerIVAAsString {
            get { return LibEnumHelper.GetDescription(_EnDondeRetenerIVA); }
        }

        public bool UsaMismoNumeroCompRetTodasCxPAsBool {
            get { return _UsaMismoNumeroCompRetTodasCxP; }
            set { _UsaMismoNumeroCompRetTodasCxP = value; }
        }

        public string UsaMismoNumeroCompRetTodasCxP {
            set { _UsaMismoNumeroCompRetTodasCxP = LibConvert.SNToBool(value); }
        }


        public int PrimerNumeroComprobanteRetIVA {
            get { return _PrimerNumeroComprobanteRetIVA; }
            set { _PrimerNumeroComprobanteRetIVA = value; }
        }

        public eFormaDeReiniciarComprobanteRetIVA FormaDeReiniciarElNumeroDeComprobanteRetIVAAsEnum {
            get { return _FormaDeReiniciarElNumeroDeComprobanteRetIVA; }
            set { _FormaDeReiniciarElNumeroDeComprobanteRetIVA = value; }
        }

        public string FormaDeReiniciarElNumeroDeComprobanteRetIVA {
            set { _FormaDeReiniciarElNumeroDeComprobanteRetIVA = (eFormaDeReiniciarComprobanteRetIVA)LibConvert.DbValueToEnum(value); }
        }

        public string FormaDeReiniciarElNumeroDeComprobanteRetIVAAsDB {
            get { return LibConvert.EnumToDbValue((int) _FormaDeReiniciarElNumeroDeComprobanteRetIVA); }
        }

        public string FormaDeReiniciarElNumeroDeComprobanteRetIVAAsString {
            get { return LibEnumHelper.GetDescription(_FormaDeReiniciarElNumeroDeComprobanteRetIVA); }
        }

        public bool ImprimirComprobanteDeRetIVAAsBool {
            get { return _ImprimirComprobanteDeRetIVA; }
            set { _ImprimirComprobanteDeRetIVA = value; }
        }

        public string ImprimirComprobanteDeRetIVA {
            set { _ImprimirComprobanteDeRetIVA = LibConvert.SNToBool(value); }
        }


        public int NumeroDeCopiasComprobanteRetencionIVA {
            get { return _NumeroDeCopiasComprobanteRetencionIVA; }
            set { _NumeroDeCopiasComprobanteRetencionIVA = value; }
        }

        public bool UnComprobanteDeRetIVAPorHojaAsBool {
            get { return _UnComprobanteDeRetIVAPorHoja; }
            set { _UnComprobanteDeRetIVAPorHoja = value; }
        }

        public string UnComprobanteDeRetIVAPorHoja {
            set { _UnComprobanteDeRetIVAPorHoja = LibConvert.SNToBool(value); }
        }


        public string NombrePlantillaComprobanteDeRetIVA {
            get { return _NombrePlantillaComprobanteDeRetIVA; }
            set { _NombrePlantillaComprobanteDeRetIVA = LibString.Mid(value, 0, 50); }
        }

        public bool GenerarNumCompDeRetIVASoloSiPorcentajeEsMayorACeroAsBool {
            get { return _GenerarNumCompDeRetIVASoloSiPorcentajeEsMayorACero; }
            set { _GenerarNumCompDeRetIVASoloSiPorcentajeEsMayorACero = value; }
        }

        public string GenerarNumCompDeRetIVASoloSiPorcentajeEsMayorACero {
            set { _GenerarNumCompDeRetIVASoloSiPorcentajeEsMayorACero = LibConvert.SNToBool(value); }
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

        public RetencionIVAStt() {
            Clear();
        }
        #endregion //Constructores
        #region Metodos Generados

        public object TextDateLastModifiedForInput() {
            return "";
        }

        public void Clear() {
            EnDondeRetenerIVAAsEnum = eDondeSeEfectuaLaRetencionIVA.NoRetenida;
            UsaMismoNumeroCompRetTodasCxPAsBool = false;
            PrimerNumeroComprobanteRetIVA = 0;
            FormaDeReiniciarElNumeroDeComprobanteRetIVAAsEnum = eFormaDeReiniciarComprobanteRetIVA.SinEscoger;
            ImprimirComprobanteDeRetIVAAsBool = false;
            NumeroDeCopiasComprobanteRetencionIVA = 0;
            UnComprobanteDeRetIVAPorHojaAsBool = false;
            NombrePlantillaComprobanteDeRetIVA = "";
            GenerarNumCompDeRetIVASoloSiPorcentajeEsMayorACeroAsBool = false;
            fldTimeStamp = 0;
        }

        public RetencionIVAStt Clone() {
            RetencionIVAStt vResult = new RetencionIVAStt();
            vResult.EnDondeRetenerIVAAsEnum = _EnDondeRetenerIVA;
            vResult.UsaMismoNumeroCompRetTodasCxPAsBool = _UsaMismoNumeroCompRetTodasCxP;
            vResult.PrimerNumeroComprobanteRetIVA = _PrimerNumeroComprobanteRetIVA;
            vResult.FormaDeReiniciarElNumeroDeComprobanteRetIVAAsEnum = _FormaDeReiniciarElNumeroDeComprobanteRetIVA;
            vResult.ImprimirComprobanteDeRetIVAAsBool = _ImprimirComprobanteDeRetIVA;
            vResult.NumeroDeCopiasComprobanteRetencionIVA = _NumeroDeCopiasComprobanteRetencionIVA;
            vResult.UnComprobanteDeRetIVAPorHojaAsBool = _UnComprobanteDeRetIVAPorHoja;
            vResult.NombrePlantillaComprobanteDeRetIVA = _NombrePlantillaComprobanteDeRetIVA;
            vResult.GenerarNumCompDeRetIVASoloSiPorcentajeEsMayorACeroAsBool = _GenerarNumCompDeRetIVASoloSiPorcentajeEsMayorACero;
            vResult.fldTimeStamp = _fldTimeStamp;
            return vResult;
        }

        public override string ToString() {
           return "En Donde Retener IVA = " + _EnDondeRetenerIVA.ToString() +
               "\nUsa el Mismo Numero de Comp. de Ret IVA para todas las CxP = " + _UsaMismoNumeroCompRetTodasCxP +
               "\nPrimer Numero Comprobante Ret IVA = " + _PrimerNumeroComprobanteRetIVA.ToString() +
               "\nForma De Reiniciar El Numero De Comprobante Ret IVA = " + _FormaDeReiniciarElNumeroDeComprobanteRetIVA.ToString() +
               "\nImprimir el Comprobante de Retención de IVA al ingresar el Pago = " + _ImprimirComprobanteDeRetIVA +
               "\nNumero De Copias Comprobante Retencion IVA = " + _NumeroDeCopiasComprobanteRetencionIVA.ToString() +
               "\nUn Comprobante De Ret IVA Por Hoja = " + _UnComprobanteDeRetIVAPorHoja +
               "\nNombre Plantilla Comprobante De Ret IVA  = " + _NombrePlantillaComprobanteDeRetIVA +
               "\nGenerar NumComp De Ret IVA Solo Si Porcentaje Es Mayor A Cero = " + _GenerarNumCompDeRetIVASoloSiPorcentajeEsMayorACero;
        }
        #endregion //Metodos Generados


    } //End of class RetencionIVAStt

} //End of namespace Galac.Saw.Ccl.SttDef

