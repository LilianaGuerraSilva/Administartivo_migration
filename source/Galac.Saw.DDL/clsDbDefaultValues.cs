using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using LibGalac.Aos.Brl.Contracts;
using LibGalac.Aos.Dal;
using LibGalac.Aos.Catching;

namespace Galac.Saw.DDL {
    public class clsDbDefaultValues {
        private List<CustomRole> _AppPermissions;
        private List<string> _BrlComponents;

        [ImportMany]
        public ObservableCollection<Lazy<ILibMefBrlComponent, ILibMefBrlComponentMetadata>> MefBrlComponents {
            get;
            set;
        }

        public clsDbDefaultValues(List<CustomRole> initAppPermissions, List<string> initBrlComponents) {
            _AppPermissions = initAppPermissions;
            _BrlComponents = initBrlComponents;
        }

        public bool InsertDefaultRecords() {
            bool vResult = true;
            if (MefBrlComponents != null && _BrlComponents != null) {
                SetDefaultUserApplicationPermissions();
                LibDbo vDbo = new LibDbo();
                foreach (string vBrlComponentName in _BrlComponents) {
                    ILibMefBrlComponent vComponent = MefBrlComponents.Where(c => c.Metadata.Name == vBrlComponentName).Select(c => c.Value).FirstOrDefault();
                    if (vComponent != null) {
                        vResult = vResult && vComponent.InsertDefaultRecords();
                    }
                }
            } else {
                vResult = false;
            }
            return vResult;
        }

        private void SetDefaultUserApplicationPermissions() {
            if (_AppPermissions != null && _AppPermissions.Count > 0) {
                CustomIdentity _Usuario = new CustomIdentity("JEFE", "JEFE", "");
                LibSecurityManager.SetCurrentPrincipalPermissions(new CustomPrincipal(_Usuario, _AppPermissions));
            } else {
                throw new ProgrammerException("No ha sido proporcionada la plantilla de permisos de esta aplicación.");
            }
        }

    }
}
