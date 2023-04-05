using System;
using System.Collections.Generic;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.Uil;
using Galac.Adm.Uil.ImprentaDigital.ViewModel;
using LibGalac.Aos.UI.Mvvm.Messaging;
using Galac.Saw.Ccl.SttDef;
using LibGalac.Aos.Catching;
using Galac.Adm.Brl.ImprentaDigital;
using System.Threading.Tasks;

namespace Galac.Adm.Uil.ImprentaDigital {
    public class clsDocumentoDigitalMenu: ILibMenu {
        #region Metodos Generados

        public bool EjecutarAccion(eTipoDocumentoFactura valTipoDocumento, string valNumeroFactura, eAccionSR valAction, bool valEsPorLote, ref string refNumeroControl) {
            try {
                bool vDocumentoEnviado = false;
                //if (valEsPorLote) {
                EnviarDocumento(valTipoDocumento, valNumeroFactura, ref refNumeroControl, ref vDocumentoEnviado);
                if (vDocumentoEnviado && !valEsPorLote) {
                    LibMessages.MessageBox.Information(this, LibEnumHelper.GetDescription(valTipoDocumento) + " enviada con éxito.", "Imprenta Digital");
                }
                return vDocumentoEnviado;
                //} else {
                //    EnviarDocumentoViewModel vViewModel = new EnviarDocumentoViewModel(valTipoDocumento, valNumeroFactura, false, valAction);
                //    LibMessages.EditViewModel.ShowEditor(vViewModel, true);
                //    refNumeroControl = vViewModel.NumeroControl;
                //    return vViewModel.DocumentoEnviado;
                //}
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

        private void EnviarDocumento(eTipoDocumentoFactura valTipoDocumento,string valNumeroFactura, ref string refNumeroControl, ref bool refDocumentoEnviado) {
            try {
                string vMensaje = string.Empty;
                var taskTestConnection = Task.Factory.StartNew(() => DoEnviarDocumento(valTipoDocumento, valNumeroFactura, ref vMensaje));
                Task.WaitAll(taskTestConnection);
                refNumeroControl = vMensaje;
                refDocumentoEnviado = taskTestConnection.Result;
            } catch (Exception) {
                throw;
            }
        }

        private bool DoEnviarDocumento(eTipoDocumentoFactura valTipoDocumento, string valNumeroFactura, ref string refNumeroControl) {
            try {
                eProveedorImprentaDigital vProveedorImprentaDigital = (eProveedorImprentaDigital)LibConvert.DbValueToEnum(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "ProveedorImprentaDigital"));
                var _insImprentaDigital = ImprentaDigitalCreator.Create(vProveedorImprentaDigital, valTipoDocumento, valNumeroFactura);
                bool vDocumentoEnviado = _insImprentaDigital.EnviarDocumento();
                refNumeroControl = _insImprentaDigital.NumeroControl;
                return vDocumentoEnviado;
            } catch (Exception) {
                throw;
            }
        }
        #endregion //Metodos Generados
    } //End of class clsDocumentoDigitalMenu
} //End of namespace Galac.Adm.Uil.ImprentaDigital

