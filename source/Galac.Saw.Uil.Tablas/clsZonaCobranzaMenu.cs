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
    public class clsZonaCobranzaMenu: ILibMenuMultiFile {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores

        public clsZonaCobranzaMenu() {
        }
        #endregion //Constructores
        #region Metodos Generados

        #region Miembros de ILibMenuMultiFile
        void ILibMenuMultiFile.Ejecuta(eAccionSR valAction, int valUseInterop,  IDictionary<string, XmlDocument> refGlobalValues) {
            ZonaCobranzaMngViewModel vViewModel = new ZonaCobranzaMngViewModel();
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
            return LibFKRetrievalHelper.ChooseRecord<FkZonaCobranzaViewModel>("Zona Cobranza", ref refXmlDocument, valSearchCriteria, valFixedCriteria, new clsZonaCobranzaNav());
        }
        #endregion //Miembros de ILibMenu
        #endregion //Metodos Generados


    } //End of class clsZonaCobranzaMenu

} //End of namespace Galac.Saw.Uil.Tablas

