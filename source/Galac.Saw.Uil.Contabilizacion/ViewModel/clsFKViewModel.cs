using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.UI.Mvvm.Helpers;
using Galac.Contab.Ccl.Tablas;
using Galac.Saw.Ccl.Contabilizacion;
using Galac.Contab.Ccl.WinCont;
namespace Galac.Saw.Uil.Contabilizacion.ViewModel {

    public class FkTipoDeComprobanteViewModel : Galac.Contab.Ccl.Tablas.IFkTipoDeComprobanteViewModel {
        [LibGridColum("Código", Width = 60)]
        public string Codigo { get; set; }
        [LibGridColum("Nombre", Width = 300)]
        public string Nombre { get; set; }
    }

    public class FkCuentaViewModel : IFkCuentaViewModel {
        public int ConsecutivoPeriodo { get; set; }
        [LibGridColum("Código")]
        public string Codigo { get; set; }
        [LibGridColum("Descripción", Width = 320)]
        public string Descripcion { get; set; }
        [LibGridColum("Naturaleza")]
        public eNaturalezaDeLaCuenta NaturalezaDeLaCuenta { get; set; }
        [LibGridColum("Tiene Auxiliar",  Type = eGridColumType.YesNo)]
        public bool TieneAuxiliar { get; set; }
        [LibGridColum("Grupo Auxiliar")]
        public eGrupoAuxiliar GrupoAuxiliar { get; set; }
        //[LibGridColum("Es Activo Fijo",  Type = eGridColumType.YesNo)]
        //public bool EsActivoFijo { get; set; }
        public bool TieneSubCuentas { get; set; }
        public eMetodoAjuste MetodoAjuste { get; set; }

    }

}
