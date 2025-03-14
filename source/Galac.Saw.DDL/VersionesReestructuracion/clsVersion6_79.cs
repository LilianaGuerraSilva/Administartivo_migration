using Galac.Adm.Dal.Vendedor;
using Galac.Saw.Ccl.SttDef;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Dal;
using LibGalac.Aos.Brl;
using LibGalac.Aos.Catching;
using LibGalac.Aos.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Galac.Saw.DDL.VersionesReestructuracion {
    class clsVersion6_79: clsVersionARestructurar {
        public clsVersion6_79(string valCurrentDataBaseName) : base(valCurrentDataBaseName) { }
        public override bool UpdateToVersion() {
            StartConnectionNoTransaction();
            AgregaLoteEnExistenciaPorAlmacen();
            ReestructuraRenglonesArticulosTipoLoteEnExistenciaPorAlmacen();
            DisposeConnectionNoTransaction();
            return true;
        }
        private void AgregaLoteEnExistenciaPorAlmacen() {
            if (!ColumnExists("dbo.ExistenciaPorAlmacen", "ConsecutivoLoteDeInventario")) {
                AddColumnInteger("dbo.ExistenciaPorAlmacen", "ConsecutivoLoteDeInventario", "", 0);
                AddUniqueKey("dbo.ExistenciaPorAlmacen", "ConsecutivoCompania,ConsecutivoAlmacen,CodigoArticulo,ConsecutivoLoteDeInventario", "ExistenciaPorAlmacenLote");                
            }
        } 

        private void ReestructuraRenglonesArticulosTipoLoteEnExistenciaPorAlmacen() {
            // Pendiente por realizar
        }
    }
}
