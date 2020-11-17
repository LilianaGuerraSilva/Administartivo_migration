using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.UI.Wpf;
using LibGalac.Aos.Uil;
using Galac.Adm.Ccl.GestionProduccion;
using Galac.Adm.Brl.GestionProduccion;
using Galac.Adm.Uil.GestionProduccion.ViewModel;
using LibGalac.Aos.UI.Mvvm.Messaging;

namespace Galac.Adm.Uil.GestionProduccion {
    public class clsOrdenDeProduccionMenu: ILibMenu {
        int _Consecutivo;
        public clsOrdenDeProduccionMenu(int initConsecutivo) {
            _Consecutivo = initConsecutivo;
        }
        #region Metodos Generados

        void ILibMenu.Ejecuta(eAccionSR valAction, int valUseInterop) {
            OrdenDeProduccionViewModel vViewModel = new OrdenDeProduccionViewModel(new OrdenDeProduccion() { ConsecutivoCompania = LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania") , Consecutivo = _Consecutivo }, eAccionSR.Consultar);
            vViewModel.InitializeViewModel(eAccionSR.Consultar);
            LibMessages.EditViewModel.ShowEditor(vViewModel, true);
            
        }

        public static bool ChooseFromInterop(ref XmlDocument refXmlDocument, List<LibSearchDefaultValues> valSearchCriteria, List<LibSearchDefaultValues> valFixedCriteria) {
            return LibFKRetrievalHelper.ChooseRecord<FkOrdenDeProduccionViewModel>("Orden de Producción", ref refXmlDocument, valSearchCriteria, valFixedCriteria, new clsOrdenDeProduccionNav());
        }
        #endregion //Metodos Generados


    } //End of class clsOrdenDeProduccionMenu

} //End of namespace Galac.Adm.Uil.GestionProduccion

