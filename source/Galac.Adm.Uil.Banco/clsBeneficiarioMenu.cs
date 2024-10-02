using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.UI.Wpf;
using LibGalac.Aos.Uil;
using Galac.Adm.Ccl.Banco;
using Galac.Adm.Uil.Banco.ViewModel;
using Galac.Adm.Brl.Banco;

namespace Galac.Adm.Uil.Banco {
    public class clsBeneficiarioMenu: ILibMenuMultiFile {
        #region Metodos Generados

        void ILibMenuMultiFile.Ejecuta(eAccionSR valAction, int valUseInterop, IDictionary<string, XmlDocument> refGlobalValues) {
           BeneficiarioMngViewModel vViewModel = new BeneficiarioMngViewModel();
           vViewModel.ExecuteSearchAndInitLookAndFeel();
           LibSearchView insFrmSearch = new LibSearchView(vViewModel);
            if (valUseInterop == 0) {
                insFrmSearch.Owner = System.Windows.Application.Current.MainWindow;
                insFrmSearch.Show();
            } else if (valUseInterop == 1) {
                insFrmSearch.Show();
            } else {
                insFrmSearch.ShowDialog();
            }
        }

        public static bool ChooseFromInterop(ref XmlDocument refXmlDocument, List<LibSearchDefaultValues> valSearchCriteria, List<LibSearchDefaultValues> valFixedCriteria) {
            return LibFKRetrievalHelper.ChooseRecord<FkBeneficiarioViewModel>("Beneficiario", ref refXmlDocument, valSearchCriteria, valFixedCriteria, new clsBeneficiarioNav());
        }
        #endregion //Metodos Generados


    } //End of class clsBeneficiarioMenu

} //End of namespace Galac.Adm.Uil.Banco

