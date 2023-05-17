using System.Collections.Generic;
using LibGalac.Aos.Base;
using LibGalac.Aos.Brl;
using Galac.Adm.Ccl.Vendedor;

namespace Galac.Adm.Brl.Vendedor {
    public partial class clsVendedorDetalleComisionesNav : LibBaseNavDetail<IList<VendedorDetalleComisiones>, IList<VendedorDetalleComisiones>> {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores

        public clsVendedorDetalleComisionesNav() {
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override ILibDataDetailComponent<IList<VendedorDetalleComisiones>, IList<VendedorDetalleComisiones>> GetDataInstance() {
            return new Galac.Adm.Dal.Vendedor.clsVendedorDetalleComisionesDat();
        }

        private void FillWithForeignInfo(ref IList<VendedorDetalleComisiones> refData) {
        }
        #endregion //Metodos Generados

    } //End of class clsVendedorDetalleComisionesNav

} //End of namespace Galac.Adm.Brl.Vendedor

