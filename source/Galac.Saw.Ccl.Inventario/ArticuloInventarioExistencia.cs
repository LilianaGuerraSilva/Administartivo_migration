using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base;

namespace Galac.Saw.Ccl.Inventario {
    public class ArticuloInventarioExistencia {
        private int _ConsecutivoCompania;
        private string _CodigoAlmacen;
        private string _CodigoArticulo;
        private decimal _Cantidad;
        private string _Ubicacion;
        private int _ConsecutivoAlmacen;
        private List<ArticuloInventarioExistenciaSerial> _DetalleArticuloInventarioExistenciaSerial;

        public ArticuloInventarioExistencia() {
            TipoActualizacion = eTipoActualizacion.Existencia;
        }

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

        public List<ArticuloInventarioExistenciaSerial> DetalleArticuloInventarioExistenciaSerial {
            get { return _DetalleArticuloInventarioExistenciaSerial; }
            set { _DetalleArticuloInventarioExistenciaSerial = value; }
        }

        public decimal CostoUnitario { get; set; }
        public decimal CostoUnitarioME { get; set; }

        public eTipoActualizacion TipoActualizacion { get; set; }
    }
}
