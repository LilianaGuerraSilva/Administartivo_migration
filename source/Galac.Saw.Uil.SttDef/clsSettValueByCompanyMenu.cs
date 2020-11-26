using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.UI.Wpf;
using LibGalac.Aos.Uil;
using Galac.Saw.Ccl.SttDef;
using Galac.Saw.Uil.SttDef.ViewModel;
using LibGalac.Aos.UI.Mvvm.Messaging;

namespace Galac.Saw.Uil.SttDef {
    public class clsSettValueByCompanyMenu: ILibMenuMultiFile {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores

        public clsSettValueByCompanyMenu() {
        }
        #endregion //Constructores
        #region Metodos Generados

        #region Miembros de ILibMenuMultiFile
        void ILibMenuMultiFile.Ejecuta(eAccionSR valAction, int valUseInterop, IDictionary<string, XmlDocument> refGlobalValues) {
            ParametersViewModel vViewModel = new ParametersViewModel(valAction);
            vViewModel.InitializeViewModel(valAction);
            LibMessages.EditViewModel.ShowEditor(vViewModel, true, false);   

        }
            
        public static bool ChooseFromInterop(ref XmlDocument refXmlDocument, List<LibSearchDefaultValues> valSearchCriteria, List<LibSearchDefaultValues> valFixedCriteria) {
            return false;
        }

        #endregion //Miembros de ILibMenu
        #endregion //Metodos Generados


    } //End of class clsSettValueByCompanyMenu

} //End of namespace Galac.Saw.Uil.SttDef

