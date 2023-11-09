using Galac.Adm.Uil.SttDef;
//using Galac.Adm.Uil.SttDef.Views;
using LibGalac.Aos.Catching;
using LibGalac.Aos.UI.Mvvm.Messaging;
using LibGalac.Aos.UI.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Galac.Saw.Wrp.SttDef {
    public interface IWrpInformacionGVentas {
        void InitializeComponent(string vfwLogin, string vfwPassword, string vfwPath);
        void InitializeDefProg(string vfwProgramInitials, string vfwProgramVersion, string vfwDbVersion, string vfwStrDateOfVersion, string vfwStrHourOfVersion, string vfwValueSpecialCharacteristic, string vfwCountry, string vfwCMTO, bool vfwUsePASOnLine);
        void InitializeContext(string vfwInfo);
        void Execute();
    }

    [ClassInterface(ClassInterfaceType.None)]
    public class wrpInformacionGVentas : IWrpInformacionGVentas {
        void IWrpInformacionGVentas.Execute() {
            try {
                winInformacionGVentas insInformacionGVentas = new winInformacionGVentas();
                insInformacionGVentas.ShowDialog();
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, null, "Información G-Ventas - Execute");
            } catch (Exception vEx) {
                if (vEx is AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void IWrpInformacionGVentas.InitializeComponent(string vfwLogin, string vfwPassword, string vfwPath) {
            try {
                //no hace falta inicializar nada
            } catch (Exception vEx) {
                if (vEx is AccessViolationException) {
                    throw;
                }
                throw new GalacWrapperException("Información G-Ventas - Inicializar", vEx);
            }
        }

        void IWrpInformacionGVentas.InitializeDefProg(string vfwProgramInitials, string vfwProgramVersion, string vfwDbVersion, string vfwStrDateOfVersion, string vfwStrHourOfVersion, string vfwValueSpecialCharacteristic, string vfwCountry, string vfwCMTO, bool vfwUsePASOnLine) {
            try {
                //no hace falta inicializar nada
            } catch (Exception vEx) {
                if (vEx is AccessViolationException) {
                    throw;
                }
                throw new GalacWrapperException("Información G-Ventas - Inicializar", vEx);
            }
        }

        void IWrpInformacionGVentas.InitializeContext(string vfwInfo) {
            try {
                //no hace falta inicializar nada
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                throw new GalacWrapperException("Información G-Ventas - Inicialización", vEx);
            }
        }

    }
}
