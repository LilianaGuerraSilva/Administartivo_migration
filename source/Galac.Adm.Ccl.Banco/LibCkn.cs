using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base;



namespace Galac.Adm.Ccl.Banco {
    public sealed class LibCkn {
        #region Variables
        static private string _ConfigKeyForDbService = "";
        #endregion //variables

        #region propiedades
        static public string ConfigKeyForDbService {
            get { return _ConfigKeyForDbService; }
        }
        #endregion //propiedades

        static public void SetConfigKeyForDbService(string vConfigKeyForDbService) {
            if (LibString.IsNullOrEmpty(vConfigKeyForDbService, true)) {
                throw new LibGalac.Aos.Catching.GalacException("No se puede asignar un valor Null o Blanco", LibGalac.Aos.Catching.eExceptionManagementType.Uncontrolled);
            } else {
                _ConfigKeyForDbService = vConfigKeyForDbService;
            }
        }
    }
}
