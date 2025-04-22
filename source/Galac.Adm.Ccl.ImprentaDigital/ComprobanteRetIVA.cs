using Galac.Adm.Ccl.GestionCompras;
using Galac.Adm.Ccl.Venta;
using LibGalac.Aos.Base;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Galac.Adm.Ccl.ImprentaDigital {
    public class ComprobanteRetIVA {        
        private eTipoDeTransaccion _TipoDeCxP;
        private eTipoDeTransaccionDeLibrosFiscales _TipoDeTransaccion;
        public string CodigoProveedor {
            get; set;
        }
        public decimal TotalCXPComprobanteRetIva {
            get; set;
        }

        public decimal TotalCXP {
            get; set;
        }

        public DateTime FechaAplicacionRetIVA {
            get; set;
        }
        public DateTime FechaDeVencimiento {
            get; set;
        }

        public int MesDeAplicacion {
            get; set;
        }
        public int AnoDeAplicacion {
            get; set;
        }
        public string NumeroComprobanteRetencion {
            get; set;
        }
        public decimal PorcentajeRetencionAplicado {
            get; set;
        }
        public DateTime FechaDelDocOrigen {
            get; set;
        }
        public string NumeroDeDocumento {
            get; set;
        }
        public string NumeroControl {
            get; set;
        }
        public string NumeroDeNotaDebito {
            get; set;
        }
        public string NumeroDeNotaCredito {
            get; set;
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

        public eTipoDeTransaccion TipoDeCxPAsEnum {
            get {
                return _TipoDeCxP;
            }
            set {
                _TipoDeCxP = value;
            }
        }

        public string TipoDeCxP {
            set {
                _TipoDeCxP = (eTipoDeTransaccion)LibConvert.DbValueToEnum(value);
            }
        }

        public string TipoDeCxPAsString {
            get {
                return LibEnumHelper.GetDescription(_TipoDeCxP);
            }
        }                        

        public string NumeroDeFacturaAfectada {
            get; set;
        }
        public decimal MontoExento {
            get; set;
        }
        public decimal MontoGravado {
            get; set;
        }
        public decimal MontoGravableAlicuotaGeneral {
            get; set;
        }
        public decimal MontoGravableAlicuota2 {
            get; set;
        }
        public decimal MontoGravableAlicuota3 {
            get; set;
        }
        public decimal AlicuotaG {
            get; set;
        }
        public decimal Alicuota2 {
            get; set;
        }
        public decimal Alicuota3 {
            get; set;
        }
        public decimal MontoIva {
            get; set;
        }
        public decimal MontoIVAAlicuotaGeneral {
            get; set;
        }
        public decimal MontoIVAAlicuota2 {
            get; set;
        }
        public decimal MontoIVAAlicuota3 {
            get; set;
        }
        public decimal MontoRetenido {
            get; set;
        }
        public int AnoAplicRetIVA {
            get; set;
        }
        public int MesAplicRetIVA {
            get; set;
        }

        public string CodigoMoneda {
            get; set;
        }
      
        public ComprobanteRetIVA() {
            TotalCXPComprobanteRetIva = 0m;
            TotalCXP = 0m;
            CodigoProveedor = string.Empty;
            FechaAplicacionRetIVA = LibDate.MinDateForDB();
            MesDeAplicacion = 0;
            AnoDeAplicacion = 0;
            NumeroComprobanteRetencion = string.Empty;
            PorcentajeRetencionAplicado = 0m;
            FechaDelDocOrigen = LibDate.MinDateForDB();
            NumeroDeDocumento = string.Empty;
            NumeroControl = string.Empty;
            NumeroDeNotaDebito = string.Empty;
            NumeroDeNotaCredito = string.Empty;
            TipoDeTransaccionAsEnum = eTipoDeTransaccionDeLibrosFiscales.Registro;
            NumeroDeFacturaAfectada = string.Empty;
            MontoExento = 0m;
            MontoGravado = 0m;
            MontoGravableAlicuotaGeneral = 0m;
            MontoGravableAlicuota2 = 0m;
            MontoGravableAlicuota3 = 0m;
            AlicuotaG = 0m;
            Alicuota2 = 0m;
            Alicuota3 = 0m;
            MontoIva = 0m;
            MontoIVAAlicuotaGeneral = 0m;
            MontoIVAAlicuota2 = 0m;
            MontoIVAAlicuota3 = 0m;
            MontoRetenido = 0m;
            AnoAplicRetIVA = 0;
            MesAplicRetIVA = 0;
            CodigoMoneda = string.Empty;
            FechaDeVencimiento = LibDate.MinDateForDB();           
        }
    }
	
    public class SujetoDeRetencion {
        private eTipoDeProveedorDeLibrosFiscales _TipoDeProveedorDeLibrosFiscalesAsEnum;
        public string Codigo {
            get; set;
        }
        public string Direccion {
            get; set;
        }
        public string Telefono {
            get; set;
        }

        public string Email {
            get; set;
        }

        public string NumeroRIF {
            get; set;
        }

        public string NombreProveedor {
            get; set;
        }

        public eTipoDeProveedorDeLibrosFiscales TipoDeProveedorDeLibrosFiscalesAsEnum {
            get {
                return _TipoDeProveedorDeLibrosFiscalesAsEnum;
            }
            set {
                _TipoDeProveedorDeLibrosFiscalesAsEnum = value;
            }
        }

        public string TipoDeProveedorDeLibrosFiscales {
            set {
                _TipoDeProveedorDeLibrosFiscalesAsEnum = (eTipoDeProveedorDeLibrosFiscales)LibConvert.DbValueToEnum(value);
            }
        }

        public string TipoDeProveedorDeLibrosFiscalesAsString {
            get {
                return LibEnumHelper.GetDescription(_TipoDeProveedorDeLibrosFiscalesAsEnum);
            }
        }

        public string CodigoProveedor {
            get; set;
        }
		
        public SujetoDeRetencion() {
            Codigo = string.Empty;
            Email = string.Empty;
            NombreProveedor = string.Empty;
            NumeroRIF = string.Empty;
            Direccion = string.Empty;
            Telefono = string.Empty;
            TipoDeProveedorDeLibrosFiscalesAsEnum = eTipoDeProveedorDeLibrosFiscales.ConRif;
        }
    }
}
