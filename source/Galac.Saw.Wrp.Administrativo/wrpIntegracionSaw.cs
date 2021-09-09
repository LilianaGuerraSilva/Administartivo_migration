using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Controls ;
using System.Windows.Controls.Primitives ;
using LibGalac.Aos.Base;
using LibGalac.Aos.UI.WpfControls;
using LibGalac.Aos.UI.Wpf;
using LibGalac.Aos.Uil.Usal;
using LibGalac.Aos.Catching;
using LibGalac.Aos.Uil;
using LibGalac.Aos.Vbwa;
using Galac.Saw.Ccl.Integracion;
using Galac.Saw.Wrp.IntegracionDDL;
#if IsExeBsF
namespace Galac.SawBsF.Wrp.IntegracionDDL {
#elif IsExeBsS​
namespace Galac.SawBsS.Wrp.IntegracionDDL {
#else
namespace Galac.Saw.Wrp.IntegracionDDL {
#endif

    [ClassInterface(ClassInterfaceType.None)]

    public class wrpIntegracionSaw : System.EnterpriseServices.ServicedComponent, IWrpIntegracionSawVb {
#region Variables
        ILibBusinessComponentWithSearch<IList<IntegracionSaw>, IList<IntegracionSaw>> _Reglas;
        string _Title = "Integracion Saw";
#endregion //Variables
#region Propiedades

        private string Title {
            get { return _Title; }
        }
#endregion //Propiedades
#region Constructores
#endregion //Constructores
#region Metodos Generados
#region Miembros de IWrpVb
        private void RegistraCliente() {
            _Reglas = new Galac.Saw.Brl.Integracion.clsIntegracionSawNav();
        }

#endregion //Miembros de IWrpVb
#endregion //Metodos Generados

        void IWrpIntegracionSawVb.InitializeComponent(string vfwLogin, string vfwPassword, string vfwPath) {
            try {
                LibWrp.SetAppConfigToCurrentDomain(vfwPath);
            } catch (Exception vEx) {
                if (vEx is AccessViolationException) {
                    throw;
                }
                throw new GalacWrapperException(Title + " - Inicializar", vEx);
            }
        }

        void IWrpIntegracionSawVb.InitializeDefProg(string vfwProgramInitials, string vfwProgramVersion, string vfwDbVersion, string vfwStrDateOfVersion, string vfwStrHourOfVersion, string vfwValueSpecialCharacteristic, string vfwCountry, string vfwCMTO, bool vfwUsePASOnLine) {
            try {
                string vLogicUnitDir = LibGalac.Aos.Cnf.LibAppSettings.ULS;
                LibGalac.Aos.DefGen.LibDefGen.InitializeProgramInfo(vfwProgramInitials, vfwProgramVersion, vfwDbVersion, LibConvert.ToDate(vfwStrDateOfVersion), vfwStrHourOfVersion, "", vfwCountry, LibConvert.ToInt(vfwCMTO));
                LibGalac.Aos.DefGen.LibDefGen.InitializeWorkPaths("", vLogicUnitDir, LibApp.AppPath(), LibGalac.Aos.DefGen.LibDefGen.ProgramInfo.ProgramInitials);
            } catch (Exception vEx) {
                if (vEx is AccessViolationException) {
                    throw;
                }
                throw new GalacWrapperException(Title + " - Inicializar", vEx);
            }
        }

        void IWrpIntegracionSawVb.InitializeContext(string vfwInfo) {
            try {
                LibGalac.Aos.DefGen.LibDefGen.Initialize(vfwInfo);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                throw new GalacWrapperException(Title + " - Inicialización", vEx);
            }
        }

        void IWrpIntegracionSawVb.Execute(string vfwAction) {
            try {
                RegistraCliente();
                if ((eAccionSR)new LibEAccionSR().ToInt(vfwAction) == eAccionSR.Insertar) {
                    ((IIntegracionSawPdn)_Reglas).InsertaValorPorDefecto();
                } else if ((eAccionSR)new LibEAccionSR().ToInt(vfwAction) == eAccionSR.Modificar) {
                    ((IIntegracionSawPdn)_Reglas).ActualizaVersion();
                }
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, null, Title + " - " + vfwAction);
            } catch (Exception vEx) {
                if (vEx is AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

    } //End of class wrpIntegracionSaw

} //End of namespace Galac.Saw.Wrp.Integracion

