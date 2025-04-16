using LibGalac.Aos.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Galac.Adm.Ccl.ImprentaDigital {
    public class ComprobanteRetIVA {
        public decimal TotalCXPComprobanteRetIva {
            get; set;
        }
        public string Direccion {
            get; set;
        }
        public string Telefonos {
            get; set;
        }
        public string NumeroRIF {
            get; set;
        }
        public string NumeroNIT {
            get; set;
        }
        public string NombreProveedor {
            get; set;
        }
        public string CodigoProveedor {
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
        public string NumeroDeFactura {
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
        public string TipoDeTransaccion {
            get; set;
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
            Direccion = string.Empty;
            Telefonos = string.Empty;
            NumeroRIF = string.Empty;
            NumeroNIT = string.Empty;
            NombreProveedor = string.Empty;
            CodigoProveedor = string.Empty;
            FechaAplicacionRetIVA = LibDate.MinDateForDB();
            MesDeAplicacion = 0;
            AnoDeAplicacion = 0;
            NumeroComprobanteRetencion = string.Empty;
            PorcentajeRetencionAplicado = 0m;
            FechaDelDocOrigen = LibDate.MinDateForDB();
            NumeroDeFactura = string.Empty;
            NumeroControl = string.Empty;
            NumeroDeNotaDebito = string.Empty;
            NumeroDeNotaCredito = string.Empty;
            TipoDeTransaccion = string.Empty;
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
}
