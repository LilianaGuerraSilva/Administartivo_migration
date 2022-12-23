using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security;
using System.Security.Permissions;
using System.Xml;
using System.Xml.Linq;
using LibGalac.Aos.Base;
using LibGalac.Aos.Brl;
using LibGalac.Aos.Base.Dal;
using Galac.Adm.Ccl.Vendedor;

namespace Galac.Adm.Brl.Vendedor {
    public partial class clsRenglonComisionesDeVendedorNav: LibBaseNavDetail<IList<RenglonComisionesDeVendedor>, IList<RenglonComisionesDeVendedor>> {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores

        public clsRenglonComisionesDeVendedorNav() {
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override ILibDataDetailComponent<IList<RenglonComisionesDeVendedor>, IList<RenglonComisionesDeVendedor>> GetDataInstance() {
            return new Galac.Adm.Dal.Vendedor.clsRenglonComisionesDeVendedorDat();
        }

        private void FillWithForeignInfo(ref IList<RenglonComisionesDeVendedor> refData) {
        }
        #endregion //Metodos Generados

    } //End of class clsRenglonComisionesDeVendedorNav

} //End of namespace Galac..Brl.ComponenteNoEspecificado

