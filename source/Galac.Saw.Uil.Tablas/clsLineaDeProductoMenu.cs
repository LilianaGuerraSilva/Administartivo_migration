using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.UI.Wpf;
using LibGalac.Aos.Uil;
using Galac.Saw.Ccl.Tablas;
using Galac.Saw.Brl.Tablas;
using Galac.Saw.Uil.Tablas.ViewModel;

namespace Galac.Saw.Uil.Tablas {
    public class clsLineaDeProductoMenu: ILibMenu {
        #region Metodos Generados

        void ILibMenu.Ejecuta(eAccionSR valAction, int valUseInterop) {
            if (valAction == eAccionSR.Instalar || valAction == eAccionSR.ReInstalar) {
                new LineaDeProductoMngViewModel().InstallOrReInstallDataFromFile(valAction);
            } else {
                LineaDeProductoMngViewModel vViewModel = new LineaDeProductoMngViewModel();
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
            return LibFKRetrievalHelper.ChooseRecord<FkLineaDeProductoViewModel>("Línea de Producto", ref refXmlDocument, valSearchCriteria, valFixedCriteria, new clsLineaDeProductoNav());
        }
        #endregion //Metodos Generados


    } //End of class clsLineaDeProductoMenu

} //End of namespace Galac.Saw.Uil.Tablas

