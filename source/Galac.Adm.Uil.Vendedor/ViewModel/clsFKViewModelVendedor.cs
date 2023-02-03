using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.UI.Mvvm.Helpers;
using Galac.Adm.Ccl.Vendedor;
using Galac.Saw.Ccl.Tablas;

namespace Galac.Adm.Uil.Vendedor.ViewModel {

    public class FkVendedorViewModel : IFkVendedorViewModel {
        public int ConsecutivoCompania { get; set; }
        public int Consecutivo { get; set; }
        [LibGridColum("Código")]
        public string Codigo { get; set; }
        [LibGridColum("Nombre Vendedor", Width = 200)]
        public string Nombre { get; set; }
        [LibGridColum("RIF")]
        public string RIF { get; set; }
        [LibGridColum("Status", eGridColumType.Enum, PrintingMemberPath = "StatusVendedorStr")]
        public eStatusVendedor StatusVendedor { get; set; }
        public string Ciudad { get; set; }
        public string ZonaPostal { get; set; }
        [LibGridColum("Teléfono")]
        public string Telefono { get; set; }
    }
    public class FkLineaDeProductoViewModel : IFkLineaDeProductoViewModel {
        public int ConsecutivoCompania { get; set; }
        public int Consecutivo { get; set; }
        [LibGridColum("Nombre", Width = 150)]
        public string Nombre { get; set; }
        [LibGridColum("Porcentaje de Comisión", eGridColumType.Numeric, Width = 150, Alignment = eTextAlignment.Right)]
        public decimal PorcentajeComision { get; set; }
    }
}
