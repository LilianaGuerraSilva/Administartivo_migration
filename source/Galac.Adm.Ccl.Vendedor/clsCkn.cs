using LibGalac.Aos.Base;

namespace Galac.Adm.Ccl.Vendedor {
    public sealed class clsCkn {
        #region Variables
        static private string _ConfigKeyForDbServiceAdministrativoVendedor = "";
        #endregion //Variables
        #region Propiedades
        static public string ConfigKeyForDbServiceAdministrativoVendedor {
            get { return _ConfigKeyForDbServiceAdministrativoVendedor; }
        }
        #endregion //Propiedades

        static public void SetConfigKeyForDbServiceAdministrativoVendedor(string valConfigKeyForDbServiceAdministrativoVendedor) {
            if (LibString.IsNullOrEmpty(valConfigKeyForDbServiceAdministrativoVendedor, true)) {
                throw new LibGalac.Aos.Catching.GalacException("No se puede asignar un valor Null o Blanco", LibGalac.Aos.Catching.eExceptionManagementType.Uncontrolled);
            } else {
                _ConfigKeyForDbServiceAdministrativoVendedor = valConfigKeyForDbServiceAdministrativoVendedor;
            }

        }
    }
}
