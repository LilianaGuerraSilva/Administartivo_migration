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
using System.Threading;
using System.Windows.Markup;

namespace Galac.Adm.Uil.ImprentaDigital {
    public class clsDocumentoDigitalMenu: ILibMenu {
        #region Metodos Generados
        public bool EjecutarAccion(eTipoDocumentoFactura valTipoDocumento, string valNumeroFactura, eAccionSR valAction, bool valEsPorLote, ref string refNumeroControl) {
            try {
                bool vDocumentoEnviado = false;               
                EnviarDocumento(valTipoDocumento, valNumeroFactura, ref refNumeroControl, ref vDocumentoEnviado);                
                return vDocumentoEnviado;               
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

        public void EnviarDocumento(eTipoDocumentoFactura valTipoDocumento, string valNumeroFactura, ref string refNumeroControl, ref bool refDocumentoEnviado) {
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
                refDocumentoEnviado = vDocumentoEnviado;
            } catch (AggregateException gEx) {
                throw new GalacException(gEx.InnerException.Message, eExceptionManagementType.Controlled);
            } catch (GalacException) {
                throw;
            }
        }       
        #endregion //Metodos Generados
    } //End of class clsDocumentoDigitalMenu
} //End of namespace Galac.Adm.Uil.ImprentaDigital

