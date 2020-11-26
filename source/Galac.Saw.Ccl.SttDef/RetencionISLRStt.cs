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
    public class RetencionISLRStt : ISettDefinition {
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
        private bool _UsaRetencion;
        private int _NumCopiasComprobanteRetencion;
        private string _DiaDelCierreFiscal;
        private string _MesDelCierreFiscal;
        private bool _TomarEnCuentaRetencionesCeroParaARCVyRA;
        private eDondeSeEfectuaLaRetencionISLR _EnDondeRetenerISLR;
        private string _NumeroRIFR;
        private string _NombreYApellidoR;
        private string _CodTelfR;
        private string _TelefonoR;
        private string _DireccionR;
        private string _CiudadRepLegal;
        private string _CorreoElectronicoRepLegal;
        private string _NombrePlantillaComprobanteDeRetISRL;
        private long _fldTimeStamp;
        XmlDocument _datos;
        #endregion //Variables
        #region Propiedades

        public bool UsaRetencionAsBool {
            get { return _UsaRetencion; }
            set { _UsaRetencion = value; }
        }

        public string UsaRetencion {
            set { _UsaRetencion = LibConvert.SNToBool(value); }
        }


        public int NumCopiasComprobanteRetencion {
            get { return _NumCopiasComprobanteRetencion; }
            set { _NumCopiasComprobanteRetencion = value; }
        }

        public string DiaDelCierreFiscal {
            get { return _DiaDelCierreFiscal; }
            set { _DiaDelCierreFiscal = LibString.Mid(value, 0, 2); }
        }

        public string MesDelCierreFiscal {
            get { return _MesDelCierreFiscal; }
            set { _MesDelCierreFiscal = LibString.Mid(value, 0, 2); }
        }

        public bool TomarEnCuentaRetencionesCeroParaARCVyRAAsBool {
            get { return _TomarEnCuentaRetencionesCeroParaARCVyRA; }
            set { _TomarEnCuentaRetencionesCeroParaARCVyRA = value; }
        }

        public string TomarEnCuentaRetencionesCeroParaARCVyRA {
            set { _TomarEnCuentaRetencionesCeroParaARCVyRA = LibConvert.SNToBool(value); }
        }


        public eDondeSeEfectuaLaRetencionISLR EnDondeRetenerISLRAsEnum {
            get { return _EnDondeRetenerISLR; }
            set { _EnDondeRetenerISLR = value; }
        }

        public string EnDondeRetenerISLR {
            set { _EnDondeRetenerISLR = (eDondeSeEfectuaLaRetencionISLR)LibConvert.DbValueToEnum(value); }
        }

        public string EnDondeRetenerISLRAsDB {
            get { return LibConvert.EnumToDbValue((int) _EnDondeRetenerISLR); }
        }

        public string EnDondeRetenerISLRAsString {
            get { return LibEnumHelper.GetDescription(_EnDondeRetenerISLR); }
        }

        public string NumeroRIFR {
            get { return _NumeroRIFR; }
            set { _NumeroRIFR = LibString.Mid(value, 0, 20); }
        }

        public string NombreYApellidoR {
            get { return _NombreYApellidoR; }
            set { _NombreYApellidoR = LibString.Mid(value, 0, 50); }
        }

        public string CodTelfR {
            get { return _CodTelfR; }
            set { _CodTelfR = LibString.Mid(value, 0, 5); }
        }

        public string TelefonoR {
            get { return _TelefonoR; }
            set { _TelefonoR = LibString.Mid(value, 0, 20); }
        }

        public string DireccionR {
            get { return _DireccionR; }
            set { _DireccionR = LibString.Mid(value, 0, 255); }
        }

        public string CiudadRepLegal {
            get { return _CiudadRepLegal; }
            set { _CiudadRepLegal = LibString.Mid(value, 0, 100); }
        }

        public string CorreoElectronicoRepLegal {
            get { return _CorreoElectronicoRepLegal; }
            set { _CorreoElectronicoRepLegal = LibString.Mid(value, 0, 40); }
        }

        public string NombrePlantillaComprobanteDeRetISRL {
            get { return _NombrePlantillaComprobanteDeRetISRL; }
            set { _NombrePlantillaComprobanteDeRetISRL = value; }
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

        public RetencionISLRStt() {
            Clear();
        }
        #endregion //Constructores
        #region Metodos Generados

        public object TextDateLastModifiedForInput() {
            return "";
        }

        public void Clear() {
            UsaRetencionAsBool = false;
            NumCopiasComprobanteRetencion = 0;
            DiaDelCierreFiscal = "";
            MesDelCierreFiscal = "";
            TomarEnCuentaRetencionesCeroParaARCVyRAAsBool = false;
            EnDondeRetenerISLRAsEnum = eDondeSeEfectuaLaRetencionISLR.NoRetenida;
            NumeroRIFR = "";
            NombreYApellidoR = "";
            CodTelfR = "";
            TelefonoR = "";
            DireccionR = "";
            CiudadRepLegal = "";
            CorreoElectronicoRepLegal = "";
            NombrePlantillaComprobanteDeRetISRL = "";
            fldTimeStamp = 0;
        }

        public RetencionISLRStt Clone() {
            RetencionISLRStt vResult = new RetencionISLRStt();
            vResult.UsaRetencionAsBool = _UsaRetencion;
            vResult.NumCopiasComprobanteRetencion = _NumCopiasComprobanteRetencion;
            vResult.DiaDelCierreFiscal = _DiaDelCierreFiscal;
            vResult.MesDelCierreFiscal = _MesDelCierreFiscal;
            vResult.TomarEnCuentaRetencionesCeroParaARCVyRAAsBool = _TomarEnCuentaRetencionesCeroParaARCVyRA;
            vResult.EnDondeRetenerISLRAsEnum = _EnDondeRetenerISLR;
            vResult.NumeroRIFR = _NumeroRIFR;
            vResult.NombreYApellidoR = _NombreYApellidoR;
            vResult.CodTelfR = _CodTelfR;
            vResult.TelefonoR = _TelefonoR;
            vResult.DireccionR = _DireccionR;
            vResult.CiudadRepLegal = _CiudadRepLegal;
            vResult.CorreoElectronicoRepLegal = _CorreoElectronicoRepLegal;
            vResult.NombrePlantillaComprobanteDeRetISRL = _NombrePlantillaComprobanteDeRetISRL;
            vResult.fldTimeStamp = _fldTimeStamp;
            return vResult;
        }

        public override string ToString() {
            return "UsaRetencion = " + _UsaRetencion +
                "\nNum Copias Comprobante Retencion  = " + _NumCopiasComprobanteRetencion.ToString() +
                "\nDia Del Cierre Fiscal = " + _DiaDelCierreFiscal +
                "\nMes Del Cierre Fiscal = " + _MesDelCierreFiscal +
                "\nTomar En Cuenta Retenciones Cero Para ARCV y RA = " + _TomarEnCuentaRetencionesCeroParaARCVyRA +
                "\nDonde Se Efectua La Retencion ISLR = " + _EnDondeRetenerISLR.ToString() +
                "\nRIF = " + _NumeroRIFR +
                "\nNombre Y Apellido = " + _NombreYApellidoR +
                "\nCod Telf R = " + _CodTelfR +
                "\nTelefono R = " + _TelefonoR +
                "\nDireccion = " + _DireccionR +
                "\nCiudad Rep Legal = " + _CiudadRepLegal +
                "\nCorreo = " + _CorreoElectronicoRepLegal +
                "\nNombre Plantilla de Comprobante De Ret ISRL = " + _NombrePlantillaComprobanteDeRetISRL;
        }
        #endregion //Metodos Generados
    } //End of class RetencionISLRStt

} //End of namespace Galac.Saw.Ccl.SttDef

