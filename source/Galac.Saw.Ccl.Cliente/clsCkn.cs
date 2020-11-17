using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base;

namespace Galac.Saw.Ccl.Cliente {
    public sealed class clsCkn {
        #region Variables
        static private string _ConfigKeyForDbService = "";
        #endregion //Variables
        #region Propiedades
        static public string ConfigKeyForDbService {
            get { return _ConfigKeyForDbService; }
        }
        #endregion //Propiedades

        static public void SetConfigKeyForDbService(string valConfigKeyForDbService) {
            if (LibString.IsNullOrEmpty(valConfigKeyForDbService, true)) {
                throw new LibGalac.Aos.Catching.GalacException("No se puede asignar un valor Null o Blanco", LibGalac.Aos.Catching.eExceptionManagementType.Uncontrolled);
            } else {
                _ConfigKeyForDbService = valConfigKeyForDbService;
            }
        }
    }
}
