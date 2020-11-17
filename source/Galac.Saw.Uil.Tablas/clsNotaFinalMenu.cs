using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.UI.Wpf;
using LibGalac.Aos.Uil;
using Galac.Saw.Ccl.Tablas;
using Galac.Saw.Uil.Tablas.ViewModel;
using Galac.Saw.Brl.Tablas;

namespace Galac.Saw.Uil.Tablas {
    public class clsNotaFinalMenu: ILibMenuMultiFile {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores

        public clsNotaFinalMenu() {
        }
        #endregion //Constructores
        #region Metodos Generados

        #region Miembros de ILibMenuMultiFile
        void ILibMenuMultiFile.Ejecuta(eAccionSR valAction, int valUseInterop, IDictionary<string, XmlDocument> refGlobalValues) {

            NotaFinalMngViewModel vViewModel = new NotaFinalMngViewModel();
            vViewModel.ExecuteSearchAndInitLookAndFeel();
            LibSearchView insFrmSearch = new LibSearchView(vViewModel);
            if (valUseInterop == 0) {
                insFrmSearch.Owner = System.Windows.Application.Current.MainWindow;
                insFrmSearch.Show();
            } else {
                insFrmSearch.ShowDialog();
            }
        }

        public static bool ChooseNotaFinal(System.Windows.Window refOwner, ref XmlDocument refXmlDocument, List<LibSearchDefaultValues> valSearchCriteria, List<LibSearchDefaultValues> valFixedCriteria) {
            return LibFKRetrievalHelper.ChooseRecord<FkNotaFinalViewModel>("Nota Final", ref refXmlDocument, valSearchCriteria, valFixedCriteria, new clsNotaFinalNav());
        }

        #endregion //Miembros de ILibMenu
        #endregion //Metodos Generados


    } //End of class clsNotaFinalMenu

} //End of namespace Galac.Saw.Uil.Tablas

