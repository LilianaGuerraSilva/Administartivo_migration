using System.Xml.Linq;
using LibGalac.Aos.UI.Mvvm;
using Galac.Adm.Brl.DispositivosExternos.ImpresoraFiscal;
using Galac.Adm.Ccl.DispositivosExternos;
using System.Threading.Tasks;
using LibGalac.Aos.UI.Mvvm.Messaging;
using System;
using LibGalac.Aos.Catching;
using System.Threading;
using LibGalac.Aos.Base;

namespace Galac.Adm.Uil.DispositivosExternos.ViewModel {
    public class ImpresoraFiscalViewModel:LibGenericViewModel {
        #region Constantes
        public const string NumeroComprobantePropertyName = "NumeroComprobante";
        public const string SerialImpresoraFiscalPropertyName = "SerialImpresoraFiscal";
        #endregion

        #region Variables
        string _NumeroComprobante = "";
        string _SerialImpresoraFiscal = "";
        string _SerialImpresoraFiscalDB = "";
        bool _SeImprimioDocumento = false;
        bool _IsRunning = false;
        IImpresoraFiscalPdn insImpresoraFiscal;
        clsImpresoraFiscalNav insImpresoraFiscalNav;
        eTipoDocumentoFiscal _TipoDocumentoFiscal;
        XElement xData;
       

        #endregion Variables

        #region Propiedades

        public override string ModuleName {
            get {
                return "Impresora Fiscal";
            }
        }

        public string NumeroComprobante {
            get {
                return _NumeroComprobante;
            }
            set {
                if(_NumeroComprobante != value) {
                    _NumeroComprobante = value;
                    RaisePropertyChanged(NumeroComprobantePropertyName);
                }
            }
        }

        public string SerialImpresoraFiscal {
            get {
                return _SerialImpresoraFiscal;
            }
            set {
                if(_SerialImpresoraFiscal != value) {
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

        public eTipoDocumentoFiscal TipoDocumentoFiscal {
            get {
                return _TipoDocumentoFiscal;
            }
            set {
                _TipoDocumentoFiscal = value;
            }
        }

        public bool IsVisibleNumDocumento {
            get {
                return TipoDocumentoFiscal != eTipoDocumentoFiscal.ReporteX;
            }
        }

        #endregion //Propiedades
        #region Constructores

        public ImpresoraFiscalViewModel(XElement valXmlMaquinaFiscal,XElement valData,eTipoDocumentoFiscal valTipoDocumentoFiscal) {
            clsImpresoraFiscalCreator vCreatorMaquinaFiscal = new clsImpresoraFiscalCreator();
            insImpresoraFiscal = vCreatorMaquinaFiscal.Crear(valXmlMaquinaFiscal);
            insImpresoraFiscalNav = new clsImpresoraFiscalNav(insImpresoraFiscal);
            _TipoDocumentoFiscal = valTipoDocumentoFiscal;
            _SerialImpresoraFiscalDB = LibXml.GetPropertyString(valXmlMaquinaFiscal, "SerialDeMaquinaFiscal");
            Title = "Imprimiendo " + LibEnumHelper.GetDescription(TipoDocumentoFiscal);
            if(_TipoDocumentoFiscal == eTipoDocumentoFiscal.FacturaFiscal || _TipoDocumentoFiscal == eTipoDocumentoFiscal.NotadeCredito) {
                xData = valData;
            }
        }

        protected override void InitializeLookAndFeel() {
            base.InitializeLookAndFeel();
            RibbonData.HideAllControls();
            SerialImpresoraFiscal = "";
            NumeroComprobante = "";
        }


        public void ImprimirDocumentoTask() {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            Task vTask = Task.Factory.StartNew(() => {
                _IsRunning = true;
                if (insImpresoraFiscal.AbrirConexion()) {
                    if (insImpresoraFiscal.ComprobarEstado()) {
                        insImpresoraFiscalNav.LeerDatosDeImpresoraFiscal(_TipoDocumentoFiscal);
                        NumeroComprobante = insImpresoraFiscalNav.NumeroComprobanteFiscal;
                        SerialImpresoraFiscal = insImpresoraFiscalNav.SerialImpresoraFiscal;
                        if (LibString.S1IsEqualToS2(SerialImpresoraFiscal, _SerialImpresoraFiscalDB)) {
                            SeImprimioDocumento = insImpresoraFiscalNav.ImprimirDocumentoFiscal(xData);
                        } else {
                            LibMessages.MessageBox.Alert(null, "El serial de la impresora fiscal asignada a esta caja no corresponde. Revisar el dispositivo fiscal para continuar.", ModuleName);
                            SeImprimioDocumento = false;
                        }
                    } else {
                        SeImprimioDocumento = false;
                    }
                    insImpresoraFiscal.CerrarConexion();
                }
            });
            vTask.ContinueWith((t) => {
                _IsRunning = false;
                if (t.IsCompleted) {
                    CancelCommand.Execute(null);
                } else {
                    LibMessages.MessageBox.Alert(null, "Proceso Cancelado", "");
                    CancelCommand.Execute(null);
                }
            }, cancellationTokenSource.Token, TaskContinuationOptions.OnlyOnRanToCompletion, TaskScheduler.FromCurrentSynchronizationContext());
            vTask.ContinueWith((t) => {
                _IsRunning = false;
                CancelCommand.Execute(null);
                if (t.Exception.InnerException != null) {
                    LibMessages.MessageBox.Information(this, t.Exception.InnerException.Message, "Información");
                } else {
                    LibMessages.MessageBox.Information(this, t.Exception.Message, "Información");
                }
            }, cancellationTokenSource.Token, TaskContinuationOptions.OnlyOnCanceled, TaskScheduler.FromCurrentSynchronizationContext());
            vTask.ContinueWith((t) => {
                _IsRunning = false;
                CancelCommand.Execute(null);
                if (t.Exception.InnerException != null) {
                    LibMessages.MessageBox.Information(this, t.Exception.InnerException.Message, "Información");
                } else {
                    LibMessages.MessageBox.Information(this, t.Exception.Message, "Información");
                }
            }, cancellationTokenSource.Token, TaskContinuationOptions.OnlyOnFaulted, TaskScheduler.FromCurrentSynchronizationContext());
        }

        public void RealizarReporteZ() {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
                Task vTask = Task.Factory.StartNew(() => {
                    _IsRunning = true;
                    if(insImpresoraFiscal.AbrirConexion()) {
                        insImpresoraFiscalNav.LeerDatosDeImpresoraFiscal(_TipoDocumentoFiscal);
                        NumeroComprobante = insImpresoraFiscalNav.NumeroComprobanteFiscal;
                        SerialImpresoraFiscal = insImpresoraFiscalNav.SerialImpresoraFiscal;
                        SeImprimioDocumento = insImpresoraFiscalNav.ImprimirReporteZ();
                        insImpresoraFiscal.CerrarConexion();
                    }
                });
                vTask.ContinueWith((t) => {
                    _IsRunning = false;
                    if(t.IsCompleted) {
                        CancelCommand.Execute(null);
                    } else {
                        LibMessages.MessageBox.Alert(null,"Proceso Cancelado","");
                        CancelCommand.Execute(null);
                    }
                },cancellationTokenSource.Token,TaskContinuationOptions.OnlyOnRanToCompletion,TaskScheduler.FromCurrentSynchronizationContext());
                vTask.ContinueWith((t) => {
                    _IsRunning = false;
                    CancelCommand.Execute(null);
                    if(t.Exception.InnerException != null) {
                        LibMessages.MessageBox.Information(this,t.Exception.InnerException.Message,"Información");
                    } else {
                        LibMessages.MessageBox.Information(this,t.Exception.Message,"Información");
                    }
                },cancellationTokenSource.Token,TaskContinuationOptions.OnlyOnCanceled,TaskScheduler.FromCurrentSynchronizationContext());
                vTask.ContinueWith((t) => {
                    _IsRunning = false;
                    CancelCommand.Execute(null);
                    if(t.Exception.InnerException != null) {
                        LibMessages.MessageBox.Information(this,t.Exception.InnerException.Message,"Información");
                    } else {
                        LibMessages.MessageBox.Information(this,t.Exception.Message,"Información");
                    }
                },cancellationTokenSource.Token,TaskContinuationOptions.OnlyOnFaulted,TaskScheduler.FromCurrentSynchronizationContext());            
        }

        public void RealizarReporteX() {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            Task vTask = Task.Factory.StartNew(() => {
                _IsRunning = true;
                if(insImpresoraFiscal.AbrirConexion()) {
                    insImpresoraFiscalNav.LeerDatosDeImpresoraFiscal(_TipoDocumentoFiscal);
                    SerialImpresoraFiscal = insImpresoraFiscalNav.SerialImpresoraFiscal;
                    NumeroComprobante = insImpresoraFiscalNav.NumeroComprobanteFiscal;
                    SeImprimioDocumento = insImpresoraFiscalNav.ImprimirReporteX();
                    insImpresoraFiscal.CerrarConexion();
                }
            });
            vTask.ContinueWith((t) => {
                _IsRunning = false;
                if(t.IsCompleted) {
                    CancelCommand.Execute(null);
                } else {
                    LibMessages.MessageBox.Alert(null,"Proceso Cancelado","");
                    CancelCommand.Execute(null);
                }
            },cancellationTokenSource.Token,TaskContinuationOptions.OnlyOnRanToCompletion,TaskScheduler.FromCurrentSynchronizationContext());
            vTask.ContinueWith((t) => {
                _IsRunning = false;
                CancelCommand.Execute(null);
                if(t.Exception.InnerException != null) {
                    LibMessages.MessageBox.Information(this,t.Exception.InnerException.Message,"Información");
                } else {
                    LibMessages.MessageBox.Information(this,t.Exception.Message,"Información");
                }
            },cancellationTokenSource.Token,TaskContinuationOptions.OnlyOnCanceled,TaskScheduler.FromCurrentSynchronizationContext());
            vTask.ContinueWith((t) => {
                _IsRunning = false;
                CancelCommand.Execute(null);
                if(t.Exception.InnerException != null) {
                    LibMessages.MessageBox.Information(this,t.Exception.InnerException.Message,"Información");
                } else {
                    LibMessages.MessageBox.Information(this,t.Exception.Message,"Información");
                }
            },cancellationTokenSource.Token,TaskContinuationOptions.OnlyOnFaulted,TaskScheduler.FromCurrentSynchronizationContext());
        }

        public override bool OnClosing() {
            return _IsRunning;
        }


        #endregion //Constructores
    } //End of class ImpresoraFiscalViewModel
} //End of namespace Galac.Saw.Uil.DispositivosExternos

