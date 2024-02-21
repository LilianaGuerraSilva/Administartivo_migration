using Galac.Adm.Uil.Venta;
using LibGalac.Aos.Catching;
using LibGalac.Aos.UI.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Galac.Saw.Wrp.Venta {
    public interface IWrpMesesGenerados{
        void InitializeComponent(string vfwLogin, string vfwPassword, string vfwPath);
        void InitializeDefProg(string vfwProgramInitials, string vfwProgramVersion, string vfwDbVersion, string vfwStrDateOfVersion, string vfwStrHourOfVersion, string vfwValueSpecialCharacteristic, string vfwCountry, string vfwCMTO, bool vfwUsePASOnLine);
        void InitializeContext(string vfwInfo);
        void Execute(string valNumeroContrato, string valConjuntoMesesGenerados);
    }

    [ClassInterface(ClassInterfaceType.None)]
    public class wrpMesesGenerados : IWrpMesesGenerados {
        void IWrpMesesGenerados.Execute(string valNumeroContrato, string valConjuntoMesesGenerados) {
            try {
                winMesesGenerados insMesesGenerados = new winMesesGenerados(valNumeroContrato, valConjuntoMesesGenerados);
                insMesesGenerados.ShowDialog();
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, null, "Meses Generados - Execute");
            } catch (Exception vEx) {
                if (vEx is AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void IWrpMesesGenerados.InitializeComponent(string vfwLogin, string vfwPassword, string vfwPath) {
            try {
                //no hace falta inicializar nada
            } catch (Exception vEx) {
                if (vEx is AccessViolationException) {
                    throw;
                }
                throw new GalacWrapperException("Meses Generados - Inicializar", vEx);
            }
        }

        void IWrpMesesGenerados.InitializeContext(string vfwInfo) {
            try {
                //no hace falta inicializar nada
            } catch (Exception vEx) {
                if (vEx is AccessViolationException) {
                    throw;
                }
                throw new GalacWrapperException("Meses Generados - Inicializar", vEx);
            }
        }

        void IWrpMesesGenerados.InitializeDefProg(string vfwProgramInitials, string vfwProgramVersion, string vfwDbVersion, string vfwStrDateOfVersion, string vfwStrHourOfVersion, string vfwValueSpecialCharacteristic, string vfwCountry, string vfwCMTO, bool vfwUsePASOnLine) {
            try {
                //no hace falta inicializar nada
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                throw new GalacWrapperException("Meses Generados - Inicialización", vEx);
            }
        }
    }
}
