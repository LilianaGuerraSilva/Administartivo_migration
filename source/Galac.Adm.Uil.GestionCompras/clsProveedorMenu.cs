using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.UI.Wpf;
using LibGalac.Aos.Uil;
using Galac.Adm.Ccl.GestionCompras;
using Galac.Adm.Uil.GestionCompras.ViewModel;
using Galac.Adm.Brl.GestionCompras;

namespace Galac.Adm.Uil.GestionCompras {
    public class clsProveedorMenu: ILibMenu {
        #region Metodos Generados

        void ILibMenu.Ejecuta(eAccionSR valAction, int valUseInterop) {
            if (valAction == eAccionSR.Instalar || valAction == eAccionSR.ReInstalar) {
                new ProveedorMngViewModel().InstallOrReInstallDataFromFile(valAction);
            } else {
                ProveedorMngViewModel vViewModel = new ProveedorMngViewModel();
                vViewModel.ExecuteSearchAndInitLookAndFeel();
                LibSearchView insFrmSearch = new LibSearchView(vViewModel);
                if (valUseInterop == 0) {
                    insFrmSearch.Owner = System.Windows.Application.Current.MainWindow;
                    insFrmSearch.Show();
                } else {
                    insFrmSearch.ShowDialog();
                }
            }
        }

        public static bool ChooseFromInterop(ref XmlDocument refXmlDocument, List<LibSearchDefaultValues> valSearchCriteria, List<LibSearchDefaultValues> valFixedCriteria) {
            return LibFKRetrievalHelper.ChooseRecord<FkProveedorViewModel>("Proveedor", ref refXmlDocument, valSearchCriteria, valFixedCriteria, new clsProveedorNav());
        }
        public static bool ChooseFromInteropForPago(ref XmlDocument refXmlDocument, List<LibSearchDefaultValues> valSearchCriteria, List<LibSearchDefaultValues> valFixedCriteria) {
           return LibFKRetrievalHelper.ChooseRecord<FkProveedorViewModel>("ProveedorForPago", ref refXmlDocument, valSearchCriteria, valFixedCriteria, new clsProveedorNav());
        }
        #endregion //Metodos Generados


    } //End of class clsProveedorMenu

} //End of namespace Galac.Adm.Uil.GestionCompras

