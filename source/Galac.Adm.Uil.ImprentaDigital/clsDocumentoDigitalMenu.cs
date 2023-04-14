using System;
using System.Collections.Generic;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.Uil;
using Galac.Saw.Ccl.SttDef;
using LibGalac.Aos.Catching;
using Galac.Adm.Brl.ImprentaDigital;
using System.Threading.Tasks;
using System.Threading;
using LibGalac.Aos.UI.Mvvm.Messaging;

namespace Galac.Adm.Uil.ImprentaDigital {
    public class clsDocumentoDigitalMenu: ILibMenu {
        #region Metodos Generados
        public bool EnviarAImprentaDigital(eTipoDocumentoFactura valTipoDocumento, string valNumeroFactura, eAccionSR valAction, ref string refNumeroControl) {
            try {
                bool vEstadoDocumento = false;
                switch (valAction) {
                    case eAccionSR.Emitir:
                        vEstadoDocumento = EnviarDocumento(valTipoDocumento, valNumeroFactura, ref refNumeroControl);
                        break;
                    case eAccionSR.Anular:
                        vEstadoDocumento = AnularDocumento(valTipoDocumento, valNumeroFactura);
                        break;
                    default:
                        break;
                }
                return vEstadoDocumento;
            } catch (GalacException) {
                throw;
            }
        }

        void ILibMenu.Ejecuta(eAccionSR valAction, int valUseInterop) {
            new NotImplementedException();
        }

        public static bool ChooseFromInterop(ref XmlDocument refXmlDocument, List<LibSearchDefaultValues> valSearchCriteria, List<LibSearchDefaultValues> valFixedCriteria) {
            return false; // LibFKRetrievalHelper.ChooseRecord<FkDocumentoDigitalViewModel>("Imprenta Digital", ref refXmlDocument, valSearchCriteria, valFixedCriteria, new clsDocumentoDigitalNav());
        }

        public bool AnularDocumento(eTipoDocumentoFactura valTipoDocumento, string valNumeroFactura) {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            bool vDocumentoAnulado = false;
            try {
                eProveedorImprentaDigital vProveedorImprentaDigital = (eProveedorImprentaDigital)LibConvert.DbValueToEnum(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "ProveedorImprentaDigital"));
                var _insImprentaDigital = ImprentaDigitalCreator.Create(vProveedorImprentaDigital, valTipoDocumento, valNumeroFactura);
                Task vTask = Task.Factory.StartNew(() => {
                    if (_insImprentaDigital.EstadoDocumento()) {
                        if (_insImprentaDigital.CodigoRespuesta == "200") {
                            vDocumentoAnulado = _insImprentaDigital.AnularDocumento();
                            if (!vDocumentoAnulado) {
                                LibMessages.MessageBox.Alert(this, "No se pudo anular el documento en la Imprenta Digital, por favor diríjase a la página web del proveedor del servicio y anule el documento manualmente.", "Imprenta Digital");
                            }
                        } else {
                            LibMessages.MessageBox.Alert(this, "No se pudo anular el documento en la Imprenta Digital porque no existe en su proveedor.\r\nSincronice sus documentos para continuar con el proceso.", "Imprenta Digital");
                        }
                    }
                });
                vTask.Wait();
                return vDocumentoAnulado;
            } catch (AggregateException gEx) {
                throw new GalacException(gEx.InnerException.Message, eExceptionManagementType.Controlled);
            } catch (GalacException) {
                throw;
            }
        }

        public bool EnviarDocumento(eTipoDocumentoFactura valTipoDocumento, string valNumeroFactura, ref string refNumeroControl) {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            string vNumeroControl = "";
            bool vDocumentoEnviado = false;
            try {
                Task vTask = Task.Factory.StartNew(() => {
                    eProveedorImprentaDigital vProveedorImprentaDigital = (eProveedorImprentaDigital)LibConvert.DbValueToEnum(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "ProveedorImprentaDigital"));
                    var _insImprentaDigital = ImprentaDigitalCreator.Create(vProveedorImprentaDigital, valTipoDocumento, valNumeroFactura);
                    vDocumentoEnviado = _insImprentaDigital.EnviarDocumento();
                    vNumeroControl = _insImprentaDigital.NumeroControl;
                });
                vTask.Wait();
                refNumeroControl = vNumeroControl;
                return vDocumentoEnviado;
            } catch (AggregateException gEx) {
                throw new GalacException(gEx.InnerException.Message, eExceptionManagementType.Controlled);
            } catch (GalacException) {
                throw;
            }
        }
        #endregion //Metodos Generados
    } //End of class clsDocumentoDigitalMenu
} //End of namespace Galac.Adm.Uil.ImprentaDigital

