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
            //public const string valido = "00";
            //public const string invalido = "99";

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

   
}
