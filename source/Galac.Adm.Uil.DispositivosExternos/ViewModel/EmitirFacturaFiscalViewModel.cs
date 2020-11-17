using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Xml.Linq;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using LibGalac.Aos.DefGen;
using LibGalac.Aos.UI.Mvvm;
using LibGalac.Aos.UI.Mvvm.Command;
using LibGalac.Aos.UI.Mvvm.Helpers;
using LibGalac.Aos.UI.Mvvm.Messaging;
using LibGalac.Aos.UI.Mvvm.Ribbon;
using LibGalac.Aos.UI.Mvvm.Validation;
using Galac.Comun.Ccl.SttDef;
using Galac.Adm.Brl.Venta;
using Galac.Adm.Ccl.Venta;
using Galac.Adm.Ccl.DispositivosExternos;
using Galac.Adm.Brl.DispositivosExternos;
using Galac.Adm.Brl.DispositivosExternos.MaquinaFiscal;

namespace Galac.Adm.Uil.DispositivosExternos.ViewModel {
   public class EmitirFacturaFiscalViewModel : LibGenericViewModel {
        #region Constantes
        private const string NombreOperadorPropertyName = "NombreOperador";
        private const string NumeroFacturaPropertyName = "NumeroFactura";
        private const string SerialImpresoraFiscalPropertyName = "SerialImpresoraFiscal";
        private const string ProgressBarValuePropertyName = "ProgressBarValue";
        #endregion

        #region Variables

        private BackgroundWorker BWorker;        
        private  int _ProgressBarValue;       
        private XElement xmlData;
        private string _NumeroFactura = "";
        private string _SerialImpresoraFiscal;
        private IMaquinaFiscalPdn insMaquinaFiscal;
        private bool _SeImprimioDocumento = false;
        #endregion Variables

        #region Propiedades
        public override string ModuleName {
            get { return "Procesando"; }
      }           
        
      public string  NumeroFactura {
            get {
                return _NumeroFactura;
            }
            set {
               if (_NumeroFactura != value) {
                   _NumeroFactura = value;                    
                   RaisePropertyChanged(NumeroFacturaPropertyName);
                }
            }
      }

      public string SerialImpresoraFiscal {
         get {
            return _SerialImpresoraFiscal;
         }
         set {
            if (_SerialImpresoraFiscal != value) {
               _SerialImpresoraFiscal = value;
               RaisePropertyChanged(SerialImpresoraFiscalPropertyName);
            }
         }
      }

      public bool SeImprimioDocumento {

         get { return _SeImprimioDocumento; }
         set { _SeImprimioDocumento = value; }
      
      }

      public int ProgressBarValue {
           get {
              return _ProgressBarValue;
           }
           set {
              if (_ProgressBarValue != value) {
                 _ProgressBarValue = value;
                 RaisePropertyChanged(ProgressBarValuePropertyName);
               }
             }        
        }
      
      #endregion //Propiedades
      #region Constructores

      public EmitirFacturaFiscalViewModel() {
         
      }

      public EmitirFacturaFiscalViewModel(XElement valData, IMaquinaFiscalPdn valMaquinaFiscal)
      {
           xmlData = valData;
           insMaquinaFiscal = valMaquinaFiscal;
           InitializeBackgroundWorker();
           BWorker.RunWorkerAsync();  
      }

      private void InitializeBackgroundWorker() {
           BWorker = new BackgroundWorker();
           BWorker.WorkerReportsProgress = true;
           BWorker.WorkerSupportsCancellation = true;
           BWorker.DoWork += new DoWorkEventHandler(BWorker_DoWork);
           BWorker.ProgressChanged += new ProgressChangedEventHandler(BWorker_ProgressChanged);
           BWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BWorker_RunWorkerCompleted);
      }

        
      private void BWorker_DoWork(object sender, DoWorkEventArgs e) {
         ExecuteProces();
      }

      private void ExecuteProces() {
         string vNumeroFactura = "";
         string vSerialMFiscal = "";
         try {
            vSerialMFiscal =  insMaquinaFiscal.ObtenerSerial();
            vNumeroFactura = insMaquinaFiscal.ObtenerUltimoNumeroFactura();            
            vNumeroFactura = LibConvert.ToStr(LibConvert.ToInt(vNumeroFactura) + 1);
            vNumeroFactura = LibText.FillWithCharToLeft(vNumeroFactura, "0", 8);
            SerialImpresoraFiscal = vSerialMFiscal;
            NumeroFactura = vNumeroFactura;
            eTipoDocumentoFactura TipoDocumento = (eTipoDocumentoFactura) LibConvert.DbValueToEnum(LibText.CleanSpacesToBothSides(LibXml.GetPropertyString(xmlData, "GpResult", "TipoDeDocumento")));            
            if (TipoDocumento.Equals(eTipoDocumentoFactura.ComprobanteFiscal)) {
               SeImprimioDocumento = insMaquinaFiscal.ImprimirFacturaFiscal(xmlData);
            }
            else if (TipoDocumento.Equals(eTipoDocumentoFactura.NotaDeCredito))
            {
               SeImprimioDocumento = insMaquinaFiscal.ImprimirNotaCredito(xmlData);
            }                        
         } catch (GalacAlertException vEx) {
            throw new GalacException(vEx.Message, eExceptionManagementType.Alert);
         } finally {
            BWorker.ReportProgress(100);
         }          
      }

      private void BWorker_ProgressChanged(object sender, ProgressChangedEventArgs e) {
         string vMensajes = e.UserState as string;
         if (ProgressBarValue <= 100) {
            ProgressBarValue = e.ProgressPercentage;            
         }
      }

      private void BWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {         
            ProgressBarValue = 0;
            RaiseRequestCloseEvent();         
      }       
        #endregion //Metodos Generados
    } //End of class EmitirFacturaFiscalViewModel

} //End of namespace Galac.Adm.Uil.Venta

