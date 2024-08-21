using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.UI.Wpf;
using LibGalac.Aos.Uil;
using Galac.Saw.Ccl.Inventario;
using Galac.Saw.Brl.Inventario;
using Galac.Saw.Uil.Inventario.ViewModel;

namespace Galac.Saw.Uil.Inventario {
    public class clsNotaDeEntradaSalidaMenu: ILibMenu {
        #region Metodos Generados

        void ILibMenu.Ejecuta(eAccionSR valAction, int valUseInterop) {
            NotaDeEntradaSalidaMngViewModel vViewModel = new NotaDeEntradaSalidaMngViewModel();
            vViewModel.ExecuteSearchAndInitLookAndFeel();
            LibSearchView insFrmSearch = new LibSearchView(vViewModel);
            if (valUseInterop == 0) {
                insFrmSearch.Owner = System.Windows.Application.Current.MainWindow;
                insFrmSearch.Show();
            } else {
                insFrmSearch.Show();
            }
        }

        public static bool ChooseFromInterop(ref XmlDocument refXmlDocument, List<LibSearchDefaultValues> valSearchCriteria, List<LibSearchDefaultValues> valFixedCriteria) {
            return false;// LibFKRetrievalHelper.ChooseRecord<FkNotaDeEntradaSalidaViewModel>("Nota de Entrada/Salida", ref refXmlDocument, valSearchCriteria, valFixedCriteria, new clsNotaDeEntradaSalidaNav());
        }
        #endregion //Metodos Generados


    } //End of class clsNotaDeEntradaSalidaMenu

} //End of namespace Galac.Saw.Uil.Inventario

