using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.UI.Wpf;
using LibGalac.Aos.Uil;
using Galac.Saw.Ccl.Vehiculo;
using Galac.Saw.Brl.Vehiculo;
using Galac.Saw.Uil.Vehiculo.ViewModel;

namespace Galac.Saw.Uil.Vehiculo {
    public class clsModeloMenu: ILibMenu {

        #region Metodos Generados

        void ILibMenu.Ejecuta(eAccionSR valAction, int valUseInterop) {
            ModeloMngViewModel vViewModel = new ModeloMngViewModel();
            vViewModel.ExecuteSearchAndInitLookAndFeel();
            LibSearchView insFrmSearch = new LibSearchView(vViewModel);
                if (valUseInterop ==0) {
                    insFrmSearch.Owner = System.Windows.Application.Current.MainWindow;
                    insFrmSearch.Show();
                } else {
                    insFrmSearch.ShowDialog();
                }
            }
        public static bool ChooseFromInterop(ref XmlDocument refXmlDocument, List<LibSearchDefaultValues> valSearchCriteria, List<LibSearchDefaultValues> valFixedCriteria) {
            return LibFKRetrievalHelper.ChooseRecord<FkModeloViewModel>("Modelo", ref refXmlDocument, valSearchCriteria, valFixedCriteria, new clsModeloNav());
        }
        #endregion //Metodos Generados


    } //End of class clsModeloMenu

} //End of namespace Galac.Saw.Uil.Vehiculo

