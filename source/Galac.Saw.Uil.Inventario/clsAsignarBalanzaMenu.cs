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
using LibGalac.Aos.UI.Mvvm.Messaging;

namespace Galac.Saw.Uil.Inventario {
    public class clsAsignarBalanzaMenu: ILibMenu {
        #region Metodos Generados

        void ILibMenu.Ejecuta(eAccionSR valAction, int valUseInterop) {
            try {
                AsignarBalanzaViewModel vViewModel = new AsignarBalanzaViewModel();
                LibMessages.EditViewModel.ShowEditor(vViewModel,true);
            } catch (Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
            }                       
        }

        public static bool ChooseFromInterop(ref XmlDocument refXmlDocument, List<LibSearchDefaultValues> valSearchCriteria, List<LibSearchDefaultValues> valFixedCriteria) {
            return LibFKRetrievalHelper.ChooseRecord<FkArticuloInventarioViewModel>("Inventario Asignar Balanza",ref refXmlDocument,valSearchCriteria,valFixedCriteria,new clsAsignarBalanzaNav());
        }
        #endregion //Metodos Generados


    } //End of class clsInventarioAsignarBalanzaMenu

} //End of namespace Galac.Saw.Uil.Inventario

