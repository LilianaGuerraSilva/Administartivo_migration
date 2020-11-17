using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base;

namespace Galac.Saw.Ccl.Inventario {
    public class ArticuloInventarioExistenciaSerial {
        private int _ConsecutivoCompania;
        private string _CodigoAlmacen;
        private string _CodigoArticulo;
        private int _ConsecutivoRenglon;
        private string _CodigoSerial;
        private string _CodigoRollo;
        private decimal _Cantidad;
        private string _Ubicacion;
        private int _ConsecutivoAlmacen;
        
        public int ConsecutivoCompania {
            get { return _ConsecutivoCompania; }
            set { _ConsecutivoCompania = value; }
        }

        public string CodigoAlmacen {
            get { return _CodigoAlmacen; }
            set { _CodigoAlmacen = LibString.Mid(value, 0, 5); }
        }

        public string CodigoArticulo {
            get { return _CodigoArticulo; }
            set { _CodigoArticulo = LibString.Mid(value, 0, 30); }
        }

        public int ConsecutivoRenglon {
            get { return _ConsecutivoRenglon ; }
            set { _ConsecutivoRenglon = value; }
        }

        public string CodigoSerial {
            get { return _CodigoSerial; }
            set { _CodigoSerial = LibString.Mid(value, 0, 50); }
        }

        public string CodigoRollo {
            get { return _CodigoRollo; }
            set { _CodigoRollo = LibString.Mid(value, 0, 20); }
        }

        public decimal Cantidad {
            get { return _Cantidad; }
            set { _Cantidad = value; }
        }

        public string Ubicacion {
            get { return _Ubicacion; }
            set { _Ubicacion = LibString.Mid(value, 0, 30); }
        }

        public int ConsecutivoAlmacen {
            get { return _ConsecutivoAlmacen; }
            set { _ConsecutivoAlmacen = value; }
        }
    }
}
