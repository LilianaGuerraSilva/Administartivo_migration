using LibGalac.Aos.Dal;
using LibGalac.Aos.Base;

namespace Galac.Saw.DDL {
    class clsVersionParaPrimeraTotalAos: QAdvReest, ILibRestructureToVersion  {
        string ILibRestructureToVersion.FriendlyDescription {
            get { return string.Format("Actualizar a la versión {0} de base de datos", ((ILibRestructureToVersion)this).NewDbVersion); }
        }

        bool ILibRestructureToVersion.HasToUpgradeToVersion(string valCurrentVersionInDb) {
            return LibVersionApp.VersionAIsGreaterThanVersionB(((ILibRestructureToVersion)this).NewDbVersion, valCurrentVersionInDb);
        }

        string ILibRestructureToVersion.NewDbVersion {
            get { return "5.57"; }
        }

        void ILibRestructureToVersion.UpgradeToNewVersion(string valCurrentVersionInDb, System.ComponentModel.BackgroundWorker valBgWorker) {
            StartConnectionNoTransaction();
            DisposeConnectionNoTransaction();
        }		

        void ILibRestructureToVersion.UpgradeToTempNonOfficialVersion(string valCurrentVersionInDb, System.ComponentModel.BackgroundWorker valBgWorker) {
            //
        }


    }
}
