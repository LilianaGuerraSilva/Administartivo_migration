using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.UI.Wpf;
using LibGalac.Aos.Uil;
using Galac.Saw.Ccl.Inventario;
using Galac.Saw.Uil.Inventario.ViewModel;
using Galac.Saw.Brl.Inventario;

namespace Galac.Saw.Uil.Inventario {
    public class clsAlmacenMenu: ILibMenuMultiFile {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores

        public clsAlmacenMenu() {
        }
        #endregion //Constructores
        #region Metodos Generados

        #region Miembros de ILibMenuMultiFile

        void ILibMenuMultiFile.Ejecuta(eAccionSR valAction, int valUseInterop, IDictionary<string, XmlDocument> refGlobalValues) {
            AlmacenMngViewModel vViewModel = new AlmacenMngViewModel();
            vViewModel.ExecuteSearchAndInitLookAndFeel();
            LibSearchView insFrmSearch = new LibSearchView(vViewModel);
            if (valUseInterop == 0) {
                insFrmSearch.Owner = System.Windows.Application.Current.MainWindow;
                insFrmSearch.Show();
            } else {
                insFrmSearch.ShowDialog();
            }
        }

        public static bool ChooseAlmacenFromInterop(ref XmlDocument refXmlDocument, List<LibSearchDefaultValues> valSearchCriteria, List<LibSearchDefaultValues> valFixedCriteria) {
            return LibFKRetrievalHelper.ChooseRecord<FkAlmacenViewModel>("Almacén", ref refXmlDocument, valSearchCriteria, valFixedCriteria, new clsAlmacenNav());
        }
        #endregion //Miembros de ILibMenu
        #endregion //Metodos Generados


    } //End of class clsAlmacenMenu

} //End of namespace Galac.Saw.Uil.Inventario

