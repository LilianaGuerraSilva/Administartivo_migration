using Galac.Adm.Ccl.Venta;
using Galac.Saw.Ccl.SttDef;
using LibGalac.Aos.Base;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Galac.Adm.Ccl.ImprentaDigital {

    #region ComprobantesDeRetencion
    public class ComprobantesDeRetencion {
        #region Variables
        private eProveedorImprentaDigital _ProveedorImprentaDigital;
        private eStatusDocumentoCxP _StatusCxP;
        public eTipoDeTransaccionDeLibrosFiscales _TipoDeTransaccion;     
        #endregion Variables
        #region Propiedades
        public string CodigoProveedor { get; set; }
        public decimal TotalDocumentoCXP { get; set; }        
        public DateTime FechaEmision { get; set; }
        public DateTime FechaDeVencimiento { get; set; }
        public int MesAplicRetIVA { get; set; }
        public int AnoAplicRetIVA { get; set; }
        public string NumeroComprobanteRetencion { get; set; }        
        public string NumeroDeDocumento { get; set; }
        public string NumeroControl { get; set; }        
        public bool EsUnacuentaDeTerceros { get; set; }
        public string NumeroControlRetencionIvaImpDigital { get; set; }
        public string MotivoDeAnulacionDeComprobante { get; set; }
        public string NumeroDeFacturaAfectada { get; set; }
        public decimal MontoExento { get; set; }
        public decimal MontoGravado { get; set; }
        public decimal MontoIva { get; set; }
        public decimal MontoRetenido { get; set; }
        public decimal MontoISLR { get; set; }
        public string CodigoMoneda { get; set; }
        public bool RetencionIvaEnviadaImpDigital { get; set; }
        public bool SeHizoLaRetencionIVA { get; set; }		
        public eProveedorImprentaDigital ProveedorImprentaDigitalAsEnum {
            get {
                return _ProveedorImprentaDigital;
            }
            set {
                _ProveedorImprentaDigital = value;
            }
        }

        public string ProveedorImprentaDigital {
            set {
                _ProveedorImprentaDigital = (eProveedorImprentaDigital)LibConvert.DbValueToEnum(value);
            }
        }

        public string ProveedorImprentaDigitalAsString {
            get {
                return LibEnumHelper.GetDescription(_ProveedorImprentaDigital);
            }
        }

        public eStatusDocumentoCxP StatusCxPAsEnum {
            get {
                return _StatusCxP;
            }
            set {
                _StatusCxP = value;
            }
        }

        public string StatusCxP {
            set {
                _StatusCxP = (eStatusDocumentoCxP)LibConvert.DbValueToEnum(value);
            }
        }

        public string StatusCxPAsString {
            get {
                return LibEnumHelper.GetDescription(_StatusCxP);
            }
        }

        public eTipoDeTransaccionDeLibrosFiscales TipoDeTransaccionAsEnum {
            get {
                return _TipoDeTransaccion;
            }
            set {
                _TipoDeTransaccion = value;
            }
        }

        public string TipoDeTransaccion {
            set {
                _TipoDeTransaccion = (eTipoDeTransaccionDeLibrosFiscales)LibConvert.DbValueToEnum(value);
            }
        }

        public string TipoDeTransaccionAsString {
            get {
                return LibEnumHelper.GetDescription(_TipoDeTransaccion);
            }
        }

        public ComprobantesDeRetencion() {
            TotalDocumentoCXP = 0m;
            CodigoProveedor = string.Empty;
            FechaEmision = LibDate.MinDateForDB();
            NumeroComprobanteRetencion = string.Empty;
            NumeroDeDocumento = string.Empty;
            NumeroControl = string.Empty;
            NumeroDeFacturaAfectada = string.Empty;
            MontoGravado = 0m;
            MontoIva = 0m;
            MontoExento = 0m;
            MontoRetenido = 0m;
            MontoISLR = 0m;
            AnoAplicRetIVA = LibDate.MinDateForDB().Year;
            MesAplicRetIVA = LibDate.MinDateForDB().Month;
            FechaDeVencimiento = LibDate.MinDateForDB();
            CodigoMoneda = string.Empty;
            RetencionIvaEnviadaImpDigital = false;
            SeHizoLaRetencionIVA = false;
            NumeroControlRetencionIvaImpDigital = string.Empty;
            EsUnacuentaDeTerceros = false;
            MotivoDeAnulacionDeComprobante = string.Empty;
            StatusCxPAsEnum = eStatusDocumentoCxP.PorCancelar;
            ProveedorImprentaDigitalAsEnum = eProveedorImprentaDigital.NoAplica; 
            TipoDeTransaccionAsEnum = eTipoDeTransaccionDeLibrosFiscales.Registro;
        }
        #endregion Propiedades
    }
    #endregion ComprobantesDeRetencion
    #region DetalleComprobantesDeRetencionIVA

    public class ComprobanteRetDetalle {
        private eTipoDeTransaccionID _TipoDeCxP;
        private eTipoDeTransaccionDeLibrosFiscales _TipoDeTransaccion;
        public string NumeroDocumento { get; set; }
        public DateTime FechaDelDocumento { get; set; }     
        public string SerieDocumento { get; set; }
        public string NumeroControlDocumento { get; set; }      
        public decimal BaseImponible { get; set; }   
        public decimal PorcentajeRetencion { get; set;  }
        public decimal MontoExento { get; set; }
        public decimal MontoIVA { get; set; }
        public decimal MontoTotal { get; set; }
        public decimal MontoRetenido { get; set; }
        public decimal MontoPercibido { get; set; }
        public decimal MontoSustraendo { get; set; }
        public string CodigoMoneda { get; set;  }
        public string CodigoConcepto { get; set;  }
        public string DescripcionConcepto { get; set; }

        public eTipoDeTransaccionDeLibrosFiscales TipoDeTransaccionAsEnum {
            get {
                return _TipoDeTransaccion;
            }
            set {
                _TipoDeTransaccion = value;
            }
        }

        public string TipoDeTransaccion {
            set {
                _TipoDeTransaccion = (eTipoDeTransaccionDeLibrosFiscales)LibConvert.DbValueToEnum(value);
            }
        }

        public string TipoDeTransaccionAsString {
            get {
                return LibEnumHelper.GetDescription(_TipoDeTransaccion);
            }
        }

        public eTipoDeTransaccionID TipoDeCxPAsEnum {
            get {
                return _TipoDeCxP;
            }
            set {
                _TipoDeCxP = value;
            }
        }

        public string TipoDeCxP {
            set {
                _TipoDeCxP = (eTipoDeTransaccionID)LibConvert.DbValueToEnum(value);
            }
        }

        public string TipoDeCxPAsString {
            get {
                return LibEnumHelper.GetDescription(_TipoDeCxP);
            }
        }
       
        public ComprobanteRetDetalle() {
            NumeroDocumento = string.Empty;
            FechaDelDocumento = LibDate.MinDateForDB();           
            SerieDocumento = string.Empty;
            NumeroControlDocumento = string.Empty;
            TipoDeTransaccionAsEnum = eTipoDeTransaccionDeLibrosFiscales.Registro;
            TipoDeCxPAsEnum = eTipoDeTransaccionID.Factura;
            BaseImponible = 0m;
            PorcentajeRetencion = 0m;
            MontoExento = 0m;
            MontoIVA = 0m;
            MontoTotal = 0m;
            MontoRetenido = 0m;
            MontoPercibido = 0m;
            MontoSustraendo= 0m;    
            CodigoMoneda = string.Empty;
            CodigoConcepto = string.Empty;
            DescripcionConcepto = string.Empty;
        }
     }

    #endregion DetalleComprobantesDeRetencionIVA
    #region SujetoDeRetencion
    public class SujetoDeRetencion {        
        private eTipodePersonaRetencionID _TipoDePersonaRetencionAsEnum;
        public string Codigo { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string NumeroRIF { get; set; }
        public string NombreProveedor { get; set; }
        public string CodigoProveedor { get; set; }
        public string TipoDeProveedor { get; set; }

        public eTipodePersonaRetencionID TipoDePersonaRetencionAsEnum {
            get {
                return _TipoDePersonaRetencionAsEnum;
            }
            set {
                _TipoDePersonaRetencionAsEnum = value;
            }
        }

        public string TipoDePersonaRetencion {
            set {
                _TipoDePersonaRetencionAsEnum = (eTipodePersonaRetencionID)LibConvert.DbValueToEnum(value);
            }
        }

        public string TipoDePersonaRetencionAsString {
            get {
                return LibEnumHelper.GetDescription(_TipoDePersonaRetencionAsEnum);
            }
        }

        public SujetoDeRetencion() {
            Codigo = string.Empty;
            Email = string.Empty;
            NombreProveedor = string.Empty;
            NumeroRIF = string.Empty;
            Direccion = string.Empty;
            Telefono = string.Empty;
            TipoDeProveedor = string.Empty;
            CodigoProveedor = string.Empty;
            TipoDePersonaRetencionAsEnum= eTipodePersonaRetencionID.JuridicaDomiciliada;
        }
    }
    #endregion SujetoDeRetencion
}
