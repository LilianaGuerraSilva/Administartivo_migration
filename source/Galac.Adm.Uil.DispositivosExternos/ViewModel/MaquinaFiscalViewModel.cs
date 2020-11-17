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
using Galac.Adm.Brl.DispositivosExternos;
using Galac.Adm.Brl.DispositivosExternos.MaquinaFiscal;
using Galac.Adm.Ccl.DispositivosExternos;

namespace Galac.Adm.Uil.DispositivosExternos.ViewModel {
   public class MaquinaFiscalViewModel : LibGenericViewModel {
        #region Constantes       
        public const string NumeroFacturaPropertyName = "NumeroFactura";        
        public const string SerialImpresoraFiscalPropertyName = "SerialImpresoraFiscal";
        #endregion

        #region Variables
        string _NumeroFactura = "";
        string _SerialImpresoraFiscal = "";
        bool _SeImprimioDocumento = false;
        IMaquinaFiscalPdn insMaquinaFiscal;
        clsMaquinaFiscalNav vMaquinaFiscalNav;
        BackgroundWorker BWorker;
        XElement xData;
        
        #endregion Variables

        #region Propiedades

        public override string ModuleName {
            get { return "Impresora Fiscal"; }
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

        public string  SerialImpresoraFiscal {
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
           get {
              return _SeImprimioDocumento;
           }
           set {
              _SeImprimioDocumento = value;
           }
        }
        
        #endregion //Propiedades
        #region Constructores

        public MaquinaFiscalViewModel(IMaquinaFiscalPdn valMaquinaFiscal, XElement valData) {
           insMaquinaFiscal = valMaquinaFiscal;
           xData = valData;
           InitializeViewModel();           
        }

        public void InitializeViewModel() {
           RibbonData.HideAllControls();
           vMaquinaFiscalNav  = new clsMaquinaFiscalNav();
           SerialImpresoraFiscal = "";
           NumeroFactura = "";
           Title = "Imprimiendo";
           InitializeBworker();
        }

        private void InitializeBworker() {
           BWorker = new BackgroundWorker();
           BWorker.WorkerReportsProgress = true;
           BWorker.WorkerSupportsCancellation = true;
           BWorker.RunWorkerCompleted+=new RunWorkerCompletedEventHandler(BWorkerRunWorkerCompleted);
           BWorker.ProgressChanged+=new ProgressChangedEventHandler(BWorkerProgressChanged);
           BWorker.DoWork+=new DoWorkEventHandler(BWorkerDoWork);
           BWorker.RunWorkerAsync();
        }

        private void BWorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
           RaiseRequestCloseEvent();
        }

        private void BWorkerProgressChanged(object sender, ProgressChangedEventArgs e) {
           
        }

        private void BWorkerDoWork(object sender, DoWorkEventArgs e) {
            try {
                vMaquinaFiscalNav.LeerDatosDeMaquinaFiscal(insMaquinaFiscal);
                SerialImpresoraFiscal = vMaquinaFiscalNav.SerialMaquinaFiscal;
                NumeroFactura = vMaquinaFiscalNav.NumeroComprobanteFiscal;
                SeImprimioDocumento = vMaquinaFiscalNav.ImprimirDocumentoFiscal(xData, insMaquinaFiscal);
            } catch(GalacException vEx) {                
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
            }
        }        
        #endregion //Constructores        
    } //End of class ImpresoraFiscalViewModel

} //End of namespace Galac.Saw.Uil.DispositivosExternos

