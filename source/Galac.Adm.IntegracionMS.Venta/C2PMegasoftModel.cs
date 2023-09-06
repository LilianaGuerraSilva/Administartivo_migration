using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Galac.Adm.IntegracionMS.Venta {
    namespace Preregister {
        public class request {
            public string cod_afiliacion { get; set; }
        }

        public class response {
            public string codigo { get; set; }
            public string descripcion { get; set; }
            public string control { get; set; }
        }

        public static class Constantes {
            public const string valido = "00";
            public const string invalido = "99";

        }
    }

    namespace ProcesarCambioPagoMovil {
        public class request {
            public string cod_afiliacion { get; set; }
            public string control { get; set; }
            public string cid { get; set; }
            public string telefono { get; set; }
            public string codigobanco { get; set; }
            public string tipo_moneda { get; set; }
            public string amount { get; set; }
            public string factura { get; set; }
        }

        public class response {
            public string control { get; set; }
            public string codigo { get; set; }
            public string descripcion { get; set; }
            public string vtid { get; set; }
            public string seqnum { get; set; }
            public string authid { get; set; }
            public string authname { get; set; }
            public string factura { get; set; }
            public string referencia { get; set; }
            public string terminal { get; set; }
            public string lote { get; set; }
            public string rifbanco { get; set; }
            public string afiliacion { get; set; }
            public voucher voucher { get; set; }
        }

        public class voucher {
            public string linea { get; set; }
        }

        public static class Constantes {
            public const string valido = "00";
            public const string invalido = "99";

        }
    }

    namespace Querystatus {
        public class request {
            public string cod_afiliacion { get; set; }
            public string control { get; set; }
            public string cid { get; set; }
            public double telefono { get; set; }
            public string codigobanco { get; set; }
            public string tipo_moneda { get; set; }
            public string amount { get; set; }
            public string factura { get; set; }
        }

        public class response {
            public string control { get; set; }
            public string cod_afiliacion { get; set; }
            public string factura { get; set; }
            public string monto { get; set; }
            public string estado { get; set; }
            public string codigo { get; set; }
            public string descripcion { get; set; }
            public string vtid { get; set; }
            public string seqnum { get; set; }
            public string authid { get; set; }
            public string authname { get; set; }
            public string tarjeta { get; set; }
            public string referencia { get; set; }
            public string terminal { get; set; }
            public string lote { get; set; }
            public string afiliacion { get; set; }
            public string telefonoEmisor { get; set; }
            public string tipotrx { get; set; }
            public voucher voucher { get; set; }
            public string text { get; set; }
        }
        public class voucher {
            public string UT { get; set; }
        }
    }

    namespace ProcesarMetodoPago    {
        public class request {
            public string accion { get; set; }
            public string montoTransaccion { get; set; }
            public string cedula { get; set; }
            public string tipoMoneda { get; set; }
        }

        public class response {
            public string codRespuesta { get; set; }
            public string mensajeRespuesta { get; set; }
            public string nombreVoucher { get; set; }
            public int numSeq { get; set; }
            public string numeroTarjeta { get; set; }
            public string cedula { get; set; }
            public string montoTransaccion { get; set; }
            public string montoAvance { get; set; }
            public string montoServicios { get; set; }
            public string montoServiciosAprobado { get; set; }
            public string montoDonativo { get; set; }
            public string tipoCuenta { get; set; }
            public string tipoTarjeta { get; set; }
            public string fechaExpiracion { get; set; }
            public bool existeCopiaVoucher { get; set; }
            public string fechaTransaccion { get; set; }
            public string horaTransaccion { get; set; }
            public string terminalVirtual { get; set; }
            public string tipoTransaccion { get; set; }
            public string numeroAutorizacion { get; set; }
            public string codigoAfiliacion { get; set; }
            public string tid { get; set; }
            public string numeroReferencia { get; set; }
            public string nombreAutorizador { get; set; }
            public string codigoAdquiriente { get; set; }
            public string numeroLote { get; set; }
            public string tipoProducto { get; set; }
            public string bancoEmisorCheque { get; set; }
            public string numeroCuenta { get; set; }
            public string numeroCheque { get; set; }
            public string tipoMonedaFiat { get; set; }
            public string descrMonedaFiat { get; set; }
            public string montoCriptomoneda { get; set; }
            public string tipoCriptomoneda { get; set; }
            public string descrCriptomoneda { get; set; }
            public string tipoMoneda { get; set; }
            public string montoDivisa { get; set; }
            public string descrMoneda { get; set; }
            public string archivoCierre { get; set; }
            public bool flagImpresion { get; set; }
            public int medioPago { get; set; }
            public string voucherServicios { get; set; }
            public object listaTransServiciosOLB { get; set; }
        }

        //public class voucher
        //{
        //    public string linea { get; set; }
        //}

        //public static class Constantes
        //{
        //    public const string valido = "00";
        //    public const string invalido = "99";

        //}
    }
}
