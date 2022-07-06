using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using Galac.Saw.DDL;
using Galac.Saw;
using Galac.Saw.Wrp.DDL;

#if IsExeBsF
namespace Galac.SawBsF.Wrp.DDL {
#elif IsExeBsS​
namespace Galac.SawBsS.Wrp.DDL {
#else
namespace Galac.Saw.Wrp.DDL {
#endif
    [ClassInterface(ClassInterfaceType.None)]
    public class wrpDbReest:System.EnterpriseServices.ServicedComponent, IwrpVbReest {
#region Propiedades
        string _Title = "Reestructuración de Datos";

        private string Title {
            get { return _Title; }
        }
#endregion
#region Miembros de IwrpVbReest

        void IwrpVbReest.InitializeComponent(string vfwPath) {
            try {
               /* 17/10/2016 debido a que el proceso de reestructuracion no usa formularios de interaccion con el usuario
                * No se requiere agregar la instruccion LibWrpHelper.ConfigureRuntimeContext 
                */
               clsLibSawDDL.SetAppConfigToCurrentDomain(vfwPath);
                clsNivelesDeSeguridad.DefinirPlantilla();
                LibSessionParameters.PlatformArchitecture = 1;
            } catch (Exception vEx) {
                if (vEx is AccessViolationException || vEx is SystemException) {
                    throw;
                }
                throw new GalacWrapperException(Title + " - Inicialización", vEx);
            }
        }

        void IwrpVbReest.InitializeDefProg(string vfwProgramInitials, string vfwProgramVersion, string vfwDbVersion, string vfwStrDateOfVersion, string vfwStrHourOfVersion, string vfwValueSpecialCharacteristic, string vfwCountry, string vfwCMTO, bool vfwUsePASOnLine) {
            try {
                LibGalac.Aos.DefGen.LibDefGen.InitializeProgramInfo(vfwProgramInitials, vfwProgramVersion, vfwProgramVersion, Convert.ToDateTime(vfwStrDateOfVersion), vfwStrHourOfVersion, "", vfwCountry, Convert.ToInt32(vfwCMTO));
            } catch (Exception vEx) {
                if (vEx is AccessViolationException || vEx is SystemException) {
                    throw;
                }
                throw new GalacWrapperException(Title +" - Inicialización", vEx);
            }
        }

        void IwrpVbReest.InitializeContext(string vfwInfo) {
            try {
                LibGalac.Aos.DefGen.LibDefGen.Initialize(vfwInfo);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                throw new GalacWrapperException(Title + " - Inicialización", vEx);
            }
        }

        bool IwrpVbReest.CreateDropViewsAndSps(bool vfwCreate) {
            try {
                if (vfwCreate) {
                    return new clsReestructurarDatabase().CrearVistasYSps();
                } else {
                    return new clsReestructurarDatabase().BorrarVistasYSps();
                }
            } catch (Exception vEx) {
                if (vEx is AccessViolationException || vEx is SystemException) {
                    throw;
                }
                throw new GalacWrapperException(Title, vEx);
            }
        }

        bool IwrpVbReest.UpdateToVersion(string vfwCurrentVersionInDb, string vfwDestinyVersion) {
            try {
                //return new clsReestructurarDatabase().UpgradeToNewVersion(vfwCurrentVersionInDb, vfwDestinyVersion);
                return new clsReestructurarDatabase(vfwCurrentVersionInDb, vfwDestinyVersion, ObtenerNombreDeBaseDatos()).UpgradeToNewVersion(vfwCurrentVersionInDb, vfwDestinyVersion);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                throw new GalacWrapperException(Title, vEx);
            }
        }
        private string ObtenerNombreDeBaseDatos() {
            string vResult;
            string vCS = new LibGalac.Aos.Cnf.LibAppConfig().ReadCnn("Datos");
            string[] vCnnInfo = vCS.Split(';');
            vResult = new LibGalac.Aos.Cnf.LibAppConfig().ExtractCnnParameterValue("Datos", "Initial Catalog");
            return vResult;
        }
#endregion
    }
}
