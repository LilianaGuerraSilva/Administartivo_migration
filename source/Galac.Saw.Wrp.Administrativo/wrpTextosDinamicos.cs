using Galac.Adm.Uil.Venta;
using Galac.Adm.Uil.Venta.Views;
using LibGalac.Aos.Catching;
using LibGalac.Aos.UI.Mvvm.Messaging;
using LibGalac.Aos.UI.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Galac.Saw.Wrp.Venta {
    public interface IWrpTextosDinamicos {
        void InitializeComponent(string vfwLogin, string vfwPassword, string vfwPath);
        void InitializeDefProg(string vfwProgramInitials, string vfwProgramVersion, string vfwDbVersion, string vfwStrDateOfVersion, string vfwStrHourOfVersion, string vfwValueSpecialCharacteristic, string vfwCountry, string vfwCMTO, bool vfwUsePASOnLine);
        void InitializeContext(string vfwInfo);
        void Execute();
    }

    [ClassInterface(ClassInterfaceType.None)]
    public class wrpTextosDinamicos : IWrpTextosDinamicos {
        void IWrpTextosDinamicos.Execute() {
            try {
                winTextosDinamicos insTextosDinamicos = new winTextosDinamicos();
                insTextosDinamicos.ShowDialog();
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, null, "Textos Dinámicos - Execute");
            } catch (Exception vEx) {
                if (vEx is AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void IWrpTextosDinamicos.InitializeComponent(string vfwLogin, string vfwPassword, string vfwPath) {
            try {
                //no hace falta inicializar nada
            } catch (Exception vEx) {
                if (vEx is AccessViolationException) {
                    throw;
                }
                throw new GalacWrapperException("Textos Dinámicos - Inicializar", vEx);
            }
        }

        void IWrpTextosDinamicos.InitializeDefProg(string vfwProgramInitials, string vfwProgramVersion, string vfwDbVersion, string vfwStrDateOfVersion, string vfwStrHourOfVersion, string vfwValueSpecialCharacteristic, string vfwCountry, string vfwCMTO, bool vfwUsePASOnLine) {
            try {
                //no hace falta inicializar nada
            } catch (Exception vEx) {
                if (vEx is AccessViolationException) {
                    throw;
                }
                throw new GalacWrapperException("Textos Dinámicos - Inicializar", vEx);
            }
        }

        void IWrpTextosDinamicos.InitializeContext(string vfwInfo) {
            try {
                //no hace falta inicializar nada
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                throw new GalacWrapperException("Textos Dinámicos - Inicialización", vEx);
            }
        }

    }
}
