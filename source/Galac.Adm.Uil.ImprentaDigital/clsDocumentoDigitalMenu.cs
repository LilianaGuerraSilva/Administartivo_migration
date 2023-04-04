using System;
using System.Collections.Generic;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.Uil;
using Galac.Adm.Uil.ImprentaDigital.ViewModel;
using LibGalac.Aos.UI.Mvvm.Messaging;
using Galac.Saw.Ccl.SttDef;
using LibGalac.Aos.Catching;

namespace Galac.Adm.Uil.ImprentaDigital {
    public class clsDocumentoDigitalMenu: ILibMenu {
        #region Metodos Generados

        public bool EjecutarAccion(eTipoDocumentoFactura valTipoDocumento, string valNumeroFactura, eAccionSR valAction, ref string refNumeroControl) {
            try {
                eTipoDocumentoFactura _TipoDeDocumento = valTipoDocumento;
                eAccionSR _Action = valAction;
                EnviarDocumentoViewModel vViewModel = new EnviarDocumentoViewModel(_TipoDeDocumento, valNumeroFactura, _Action);
                LibMessages.EditViewModel.ShowEditor(vViewModel, true);
                refNumeroControl = vViewModel.NumeroControl;
                return vViewModel.DocumentoEnviado;
            } catch (Exception vEx) {
                throw new GalacException(vEx.Message, eExceptionManagementType.Controlled);
            }
        }

        void ILibMenu.Ejecuta(eAccionSR valAction, int valUseInterop) {
            new NotImplementedException();                     
        }

        public static bool ChooseFromInterop(ref XmlDocument refXmlDocument, List<LibSearchDefaultValues> valSearchCriteria, List<LibSearchDefaultValues> valFixedCriteria) {
            return false; // LibFKRetrievalHelper.ChooseRecord<FkDocumentoDigitalViewModel>("Imprenta Digital", ref refXmlDocument, valSearchCriteria, valFixedCriteria, new clsDocumentoDigitalNav());
        }
        #endregion //Metodos Generados
    } //End of class clsDocumentoDigitalMenu
} //End of namespace Galac.Adm.Uil.ImprentaDigital

