using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.UI.Wpf;
using LibGalac.Aos.Uil;
using Galac.Saw.Ccl.SttDef;
using Galac.Saw.Brl.SttDef;
using Galac.Saw.Uil.SttDef.ViewModel;

namespace Galac.Saw.Uil.SttDef {
    public class clsSettDefinitionMenu: ILibMenu {
        #region Metodos Generados

        void ILibMenu.Ejecuta(eAccionSR valAction, int valUseInterop) {
            if (valAction == eAccionSR.Instalar || valAction == eAccionSR.ReInstalar) {
                new SettDefinitionMngViewModel().InstallOrReInstallDataFromFile(valAction);
            } else {
                SettDefinitionMngViewModel vViewModel = new SettDefinitionMngViewModel();
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
            return false; //LibFKRetrievalHelper.ChooseRecord<FkSettDefinitionViewModel>("Sett Definition", ref refXmlDocument, valSearchCriteria, valFixedCriteria, new clsSettDefinitionNav());
        }
        #endregion //Metodos Generados


    } //End of class clsSettDefinitionMenu

} //End of namespace Galac.Saw.Uil.SttDef
