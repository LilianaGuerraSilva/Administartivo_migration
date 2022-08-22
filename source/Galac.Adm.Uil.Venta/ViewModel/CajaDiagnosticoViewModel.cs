using System;
using System.Xml.Linq;
using LibGalac.Aos.Base;
using LibGalac.Aos.UI.Mvvm;
using LibGalac.Aos.UI.Mvvm.Command;
using LibGalac.Aos.UI.Mvvm.Messaging;
using LibGalac.Aos.UI.Mvvm.Ribbon;
using Galac.Adm.Ccl.DispositivosExternos;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Threading;

namespace Galac.Adm.Uil.Venta.ViewModel {
    public class CajaDiagnosticoViewModel:LibGenericViewModel {
        #region Constantes
        public const string EstatusDeComunicacionPropertyName = "EstatusDeComunicacion";
        public const string EstatusDeComunicacionDescriptionPropertyName = "EstatusDeComunicacionDescription";
        public const string VersionDeControladoresPropertyName = "VersionDeControladores";
        public const string VersionDeControladoresDescriptionPropertyName = "VersionDeControladoresDescription";
        public const string AlicuotasRegistradasPropertyName = "AlicuotasRegistradas";
        public const string AlicoutasRegistradasDescriptionPropertyName = "AlicuotasRegistradasDescription";
        public const string ConfiguracionImpresoraPropertyName = "ConfiguracionImpresora";
        public const string ConfiguracionImpresoraDescriptionPropertyName = "ConfiguracionImpresoraDescription";
        public const string FechaYHoraPropertyName = "FechaYHora";
        public const string FechaYHoraDescriptionPropertyName = "FechaYHoraDescription";
        public const string ColaDeImpresionPropertyName = "ColaDeImpresion";
        public const string ColaDeImpresionDescriptionPropertyName = "ColaDeImpresionDescription";
        private const string statusIsGood = "\u2713";
        private const string statusIsBad = "X";
        private const string ColorStatusColaDeImpresionPropertyName = "ColorStatusColaDeImpresion";
        private const string ColorStatusFechaYHoraPropertyName = "ColorStatusFechaYHora";
        private const string ColorStatusAlicuotasRegistradasPropertyName = "ColorStatusAlicuotasRegistradas";
        private const string ColorStatusVersionDeControladoresPropertyName = "ColorStatusVersionDeControladores";
        private const string ColorStatusEstatusDeComunicacionPropertyName = "ColorStatusEstatusDeComunicacion";
        private const string ShowProgressPropertyName = "ShowProgress";
        private const string IsVisibleCierreZPropertyName = "IsVisibleCierreZ";
        #endregion
        #region Propiedades

        string _EstatusDeComunicacion;
        string _VersionDeControladores;
        string _AlicuotasRegistradas;
        string _EstatusDeComunicacionDescription;
        string _VersionDeControladoresDescription;
        string _AlicoutasRegistradasDescription;
        string _ConfiguracionImpresoraDescription;
        string _FechaYHora;
        string _FechaYHoraDescription;
        string _ColaDeImpresion;
        string _ColaDeImpresionDescription;
        string _ColorStatusColaDeImpresion;
        string _ColorStatusFechaYHora;
        string _ColorStatusAlicuotasRegistradas;
        string _ColorStatusVersionDeControladores;
        string _ColorStatusEstatusDeComunicacion;
        bool _ShowProgress;
        bool _IsRunning = false;

        IFDiagnostico _Diagnostico;
        IImpresoraFiscalPdn _ImpresoraFiscal;
        eFamiliaImpresoraFiscal _FamImprFiscal;

        public override string ModuleName {
            get { return "Diagnóstico Máquina Fiscal"; }
        }

        public RelayCommand DiagnosticoCommand {
            get;
            private set;
        }

        public RelayCommand CierreZCommand {
            get;
            private set;
        }

        private bool CanExecuteDiagnosticarCommand() {
            return true;
        }

        private bool CanExecuteCierreZCommand() {
            return true;
        }


        public string EstatusDeComunicacion {
            get {
                return _EstatusDeComunicacion;
            }
            set {
                if(_EstatusDeComunicacion != value) {
                    _EstatusDeComunicacion = value;
                    RaisePropertyChanged(EstatusDeComunicacionPropertyName);
                    RaisePropertyChanged(ColorStatusEstatusDeComunicacionPropertyName);
                }
            }
        }

        public string EstatusDeComunicacionDescription {
            get {
                return _EstatusDeComunicacionDescription;
            }
            set {
                if(_EstatusDeComunicacionDescription != value) {
                    _EstatusDeComunicacionDescription = value;
                    RaisePropertyChanged(EstatusDeComunicacionDescriptionPropertyName);
                }
            }
        }

        public bool ShowProgress {
            get {
                return _ShowProgress;
            }
            set {
                if(_ShowProgress != value) {
                    _ShowProgress = value;
                    RaisePropertyChanged(ShowProgressPropertyName);
                }
            }
        }

        public string VersionDeControladores {
            get {
                return _VersionDeControladores;
            }
            set {
                if(_VersionDeControladores != value) {
                    _VersionDeControladores = value;
                    RaisePropertyChanged(VersionDeControladoresPropertyName);
                    RaisePropertyChanged(ColorStatusVersionDeControladoresPropertyName);
                }
            }
        }

        public string VersionDeControladoresDescription {
            get {
                return _VersionDeControladoresDescription;
            }
            set {
                if(_VersionDeControladoresDescription != value) {
                    _VersionDeControladoresDescription = value;
                    RaisePropertyChanged(VersionDeControladoresDescriptionPropertyName);
                }
            }
        }

        public string AlicuotasRegistradas {
            get {
                return _AlicuotasRegistradas;
            }
            set {
                if(_AlicuotasRegistradas != value) {
                    _AlicuotasRegistradas = value;
                    RaisePropertyChanged(AlicuotasRegistradasPropertyName);
                    RaisePropertyChanged(ColorStatusAlicuotasRegistradasPropertyName);
                }
            }
        }

        public string AlicuotasRegistradasDescription {
            get {
                return _AlicoutasRegistradasDescription;
            }
            set {
                if(_AlicoutasRegistradasDescription != value) {
                    _AlicoutasRegistradasDescription = value;
                    RaisePropertyChanged(AlicoutasRegistradasDescriptionPropertyName);
                }
            }
        }
        public string ConfiguracionImpresoraDescription {
            get {
                return _ConfiguracionImpresoraDescription;
            }
            set {
                if(_ConfiguracionImpresoraDescription != value) {
                    _ConfiguracionImpresoraDescription = value;
                    RaisePropertyChanged(ConfiguracionImpresoraDescriptionPropertyName);
                }
            }
        }

        public string FechaYHora {
            get {
                return _FechaYHora;
            }
            set {
                if(_FechaYHora != value) {
                    _FechaYHora = value;
                    RaisePropertyChanged(FechaYHoraPropertyName);
                    RaisePropertyChanged(ColorStatusFechaYHoraPropertyName);
                }
            }
        }

        public string FechaYHoraDescription {
            get {
                return _FechaYHoraDescription;
            }
            set {
                if(_FechaYHoraDescription != value) {
                    _FechaYHoraDescription = value;
                    RaisePropertyChanged(FechaYHoraDescriptionPropertyName);
                }
            }
        }

        public string ColaDeImpresion {
            get {
                return _ColaDeImpresion;
            }
            set {
                if(_ColaDeImpresion != value) {
                    _ColaDeImpresion = value;
                    RaisePropertyChanged(ColaDeImpresionPropertyName);
                    RaisePropertyChanged(ColorStatusColaDeImpresionPropertyName);
                }
            }
        }

        public string ColaDeImpresionDescription {
            get {
                return _ColaDeImpresionDescription;
            }
            set {
                if(ColaDeImpresionDescription != value) {
                    _ColaDeImpresionDescription = value;
                    RaisePropertyChanged(ColaDeImpresionDescriptionPropertyName);
                    RaisePropertyChanged(IsVisibleCierreZPropertyName);
                }
            }
        }

        public string ColorStatusColaDeImpresion {
            get {
                return _ColorStatusColaDeImpresion;
            }
            set {
                if(_ColorStatusColaDeImpresion != value) {
                    _ColorStatusColaDeImpresion = value;
                    RaisePropertyChanged(ColorStatusColaDeImpresionPropertyName);
                }

            }
        }

        public string ColorStatusFechaYHora {
            get {
                return _ColorStatusFechaYHora;
            }
            set {
                if(_ColorStatusFechaYHora != value) {
                    _ColorStatusFechaYHora = value;
                    RaisePropertyChanged(ColorStatusFechaYHoraPropertyName);
                }
            }

        }

        public string ColorStatusAlicuotasRegistradas {
            get {
                return _ColorStatusAlicuotasRegistradas;
            }
            set {
                if(_ColorStatusAlicuotasRegistradas != value) {
                    _ColorStatusAlicuotasRegistradas = value;
                    RaisePropertyChanged(ColorStatusAlicuotasRegistradasPropertyName);
                }
            }
        }

        public string ColorStatusVersionDeControladores {
            get {
                return _ColorStatusVersionDeControladores;
            }
            set {
                if(_ColorStatusVersionDeControladores != value) {
                    _ColorStatusVersionDeControladores = value;
                    RaisePropertyChanged(ColorStatusVersionDeControladoresPropertyName);
                }
            }
        }

        public string ColorStatusEstatusDeComunicacion {
            get {
                return _ColorStatusEstatusDeComunicacion;
            }
            set {
                if(_ColorStatusEstatusDeComunicacion != value) {
                    _ColorStatusEstatusDeComunicacion = value;
                    RaisePropertyChanged(ColorStatusEstatusDeComunicacionPropertyName);
                }
            }
        }

        public bool IsVisibleCierreZ {
            get {
                return _FamImprFiscal == eFamiliaImpresoraFiscal.EPSONPNP && ColaDeImpresionDescription.Contains("Más de un Día desde el ultimo cierre Z");
            }
        }

        public bool IsVisibleConfiguracionImpresora {
            get {
                return _FamImprFiscal == eFamiliaImpresoraFiscal.THEFACTORY;
            }
        }

        #endregion //Propiedades
        #region Constructores
        public CajaDiagnosticoViewModel(IImpresoraFiscalPdn valImpresoraFiscal,eFamiliaImpresoraFiscal valFamImprFiscal)
            : this(eAccionSR.Insertar) {
            _ImpresoraFiscal = valImpresoraFiscal;
            _FamImprFiscal = valFamImprFiscal;
        }
        public CajaDiagnosticoViewModel(eAccionSR initAction)
            : base() {

        }
        #endregion //Constructores
        #region Metodos Generados

        protected override void InitializeLookAndFeel() {
            base.InitializeLookAndFeel();
            ShowProgress = false;
            ColaDeImpresionDescription = string.Empty;
        }

        protected override void InitializeCommands() {
            base.InitializeCommands();
            DiagnosticoCommand = new RelayCommand(ExecuteDiganosticoCommand,CanExecuteDiagnosticarCommand);
            CierreZCommand = new RelayCommand(ExecuteCierreZCommand,CanExecuteCierreZCommand);
        }

        protected override void InitializeRibbon() {
            base.InitializeRibbon();
            if(RibbonData.TabDataCollection != null && RibbonData.TabDataCollection.Count > 0) {
                RibbonData.TabDataCollection[0].GroupDataCollection[0].AddRibbonControlData(CreateDiganosticoRibbonButtonData());
                var vRibbonControl = RibbonData.TabDataCollection[0].GroupDataCollection[0].ControlDataCollection[0];
                RibbonData.TabDataCollection[0].GroupDataCollection[0].ControlDataCollection[0] = RibbonData.TabDataCollection[0].GroupDataCollection[0].ControlDataCollection[1];
                RibbonData.TabDataCollection[0].GroupDataCollection[0].ControlDataCollection[1] = vRibbonControl;
            }
        }

        private void ExecuteDiganosticoCommand() {
            try {
                if(!_IsRunning) {
                    _IsRunning = true;
                    EjecutarDiagnosticoTask();
                }
            } catch(Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
            }
        }

        private void ExecuteCierreZCommand() {
            if(_ImpresoraFiscal.AbrirConexion()) {
                ShowProgress = true;
                RaisePropertyChanged(ShowProgressPropertyName);
                _ImpresoraFiscal.RealizarReporteZ();
                ShowProgress = false;
                RaisePropertyChanged(ShowProgressPropertyName);
                ColaDeImpresion = (statusIsGood);
                ColorStatusColaDeImpresion = SetStatusColor(true);
                ColaDeImpresionDescription = "Lista, En Espera";
                RaisePropertyChanged(IsVisibleCierreZPropertyName);
            }
        }

        private void RefreshProperties() {
            EstatusDeComunicacion = (_Diagnostico.EstatusDeComunicacion ? statusIsGood : statusIsBad);
            ColorStatusEstatusDeComunicacion = SetStatusColor(_Diagnostico.EstatusDeComunicacion);
            EstatusDeComunicacionDescription = _Diagnostico.EstatusDeComunicacionDescription;
            ColaDeImpresion = (_Diagnostico.ColaDeImpresion ? statusIsGood : statusIsBad);
            ColorStatusColaDeImpresion = SetStatusColor(_Diagnostico.ColaDeImpresion);
            ColaDeImpresionDescription = _Diagnostico.ColaDeImpresioDescription;
            FechaYHora = (_Diagnostico.FechaYHora ? statusIsGood : statusIsBad);
            ColorStatusFechaYHora = SetStatusColor(_Diagnostico.FechaYHora);
            FechaYHoraDescription = _Diagnostico.FechaYHoraDescription;
            AlicuotasRegistradas = (_Diagnostico.AlicuotasRegistradas ? statusIsGood : statusIsBad);
            ColorStatusAlicuotasRegistradas = SetStatusColor(_Diagnostico.AlicuotasRegistradas);
            AlicuotasRegistradasDescription = _Diagnostico.AlicoutasRegistradasDescription;
            ConfiguracionImpresoraDescription = _Diagnostico.ConfiguracionImpresoraDescription;
            VersionDeControladores = (_Diagnostico.VersionDeControladores ? statusIsGood : statusIsBad);
            ColorStatusVersionDeControladores = SetStatusColor(_Diagnostico.VersionDeControladores);
            VersionDeControladoresDescription = _Diagnostico.VersionDeControladoresDescription;
        }

        private string SetStatusColor(bool valStatus) {
            return (valStatus ? "Green" : "Red");
        }

        private void EjecutarDiagnosticoTask() {
            ShowProgress = true;
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            Task IfDiagnosticoTask = Task.Factory.StartNew(() => {
                _Diagnostico = _ImpresoraFiscal.RealizarDiagnostico(true);
                RefreshProperties();
            });
            IfDiagnosticoTask.ContinueWith((t) => {
                ShowProgress = false;
                _IsRunning = false;
                if(t.IsCompleted) {
                    LibMessages.MessageBox.Information(null,"El proceso fué completado con éxito.","");
                } else {
                    LibMessages.MessageBox.Alert(null,"El proceso fué cancelado.","");

                }
            },cancellationTokenSource.Token,TaskContinuationOptions.OnlyOnRanToCompletion,TaskScheduler.FromCurrentSynchronizationContext());
            IfDiagnosticoTask.ContinueWith((t) => {
                ShowProgress = false;
                _IsRunning = false;
                if(t.Exception.InnerException != null) {
                    LibMessages.MessageBox.Alert(this,t.Exception.InnerException.Message,"Información");
                } else {
                    LibMessages.MessageBox.Alert(this,t.Exception.Message,"Información");
                }
            },cancellationTokenSource.Token,TaskContinuationOptions.OnlyOnCanceled,TaskScheduler.FromCurrentSynchronizationContext());
            IfDiagnosticoTask.ContinueWith((t) => {
                ShowProgress = false;
                _IsRunning = false;
                if(t.Exception.InnerException != null) {
                    LibMessages.MessageBox.Alert(this,t.Exception.InnerException.Message,"Información");
                } else {
                    LibMessages.MessageBox.Alert(this,t.Exception.Message,"Información");
                }
            },cancellationTokenSource.Token,TaskContinuationOptions.OnlyOnFaulted,TaskScheduler.FromCurrentSynchronizationContext());
        }

        private LibRibbonButtonData CreateDiganosticoRibbonButtonData() {
            return new LibRibbonButtonData() {
                Label = "Iniciar Diagnóstico",
                Command = DiagnosticoCommand,
                LargeImage = new Uri("/LibGalac.Aos.UI.WpfRD;component/Images/exec.png",UriKind.Relative),
                ToolTipDescription = "Iniciar Diagnóstico",
                ToolTipTitle = "Iniciar Diagnóstico"
            };
        }

        XElement BuildImpresoraFiscalData(eImpresoraFiscal vImpresoraFiscal,ePuerto ePuerto) {
            string vPuerto = SerialPortFormat(ePuerto);
            XElement vResult = new XElement("GpData",new XElement("GpResult",
                new XElement("ModeloDeMaquinaFiscal",LibConvert.EnumToDbValue((int)vImpresoraFiscal)),
                new XElement("PuertoMaquinaFiscal",vPuerto)
                ));
            return vResult;
        }

        string SerialPortFormat(ePuerto ePuerto) {
            return Regex.Replace(ePuerto.GetDescription(),"[A-Z-az]","",RegexOptions.Compiled);
        }
        #endregion //Metodos Generados

    } //End of class CajaDiagnosticoViewModel

} //End of namespace Galac.Adm.Uil.Venta

