using System;
using System.Text;
using LibGalac.Aos.UI.Mvvm.Command;
using LibGalac.Aos.UI.Mvvm.Messaging;
using LibGalac.Aos.UI.Mvvm.Ribbon;
using LibGalac.Aos.Base;
using Galac.Adm.Brl.Vendedor;
using System.ComponentModel;
using Microsoft.Win32;
using LibGalac.Aos.UI.Wpf.ViewModel;
using System.Xml.Linq;

namespace Galac.Adm.Uil.Vendedor.ViewModel {

    public class VendedorImportarExportarViewModel : LibImportViewModel {

        #region Variables y Constantes

        private eExportDelimiterType _TipoDeSeparacionAsEnum;
        public string _NombreDelArchivo { get; set; }
        public bool _CodigoIncluyeSeparadorAsBool { get; set; }
        public int _NumeroDeLote { get; set; }
        public DateTime _FechaDeApertura { get; set; }
        public DateTime _FechaDeCierre { get; set; }

        private BackgroundWorker _BWorker;
        private decimal _ProgressPercent = 0;
        private decimal _ProgressValue = 0;
        private string _ProgressMessage = null;
        private bool _IsVisibleProgressPanel = false;

        #endregion //Variables

        #region Constantes

        private const string DatosGeneralesPropertyName = "DatosGenerales";
        private const string TipoDeSeparacionPropertyName = "TipoDeSeparacion";
        private const string NombreDelArchivoPropertyName = "NombreDelArchivo";
        private const string ProgressPercentPropertyName = "ProgressPercent";
        private const string ProgressPrimaryMessagePropertyName = "ProgressPrimaryMessage";
        private const string ProgressSecondaryMessagePropertyName = "ProgressSecondaryMessage";
        private const string ProgressValuePropertyName = "ProgressValue";
        private const string IsVisibleProgressPanelPropertyName = "IsVisibleProgressPanel";

        #endregion //Constantes

        #region Propiedades
        public override string ModuleName {
            get { return "Importar Vendedores"; }
        }
        public eExportDelimiterType TipoDeSeparacion {
            get {
                return _TipoDeSeparacionAsEnum;
            }
            set {
                if (_TipoDeSeparacionAsEnum != value) {
                    _TipoDeSeparacionAsEnum = value;
                    RaisePropertyChanged(TipoDeSeparacionPropertyName);
                }
            }
        }

        public string NombreDelArchivo {
            get {
                return _NombreDelArchivo;
            }
            set {
                if (_NombreDelArchivo != value) {
                    _NombreDelArchivo = value;
                    RaisePropertyChanged(NombreDelArchivoPropertyName);
                }
            }
        }

        public eExportDelimiterType[] ArrayExportDelimiterType {
            get {
                return LibEnumHelper<eExportDelimiterType>.GetValuesInArray();
            }
        }

        public RelayCommand ImportarCommand {
            get;
            private set;
        }

        public RelayCommand BuscarDirectorioArchivoCommand {
            get;
            private set;

        }

        public decimal ProgressPercent {
            get {
                return _ProgressPercent;
            }
            set {
                if (_ProgressPercent != value) {
                    _ProgressPercent = value;
                    RaisePropertyChanged(ProgressPercentPropertyName);
                }
            }
        }

        public bool IsVisibleProgressPanel {
            get {
                return _IsVisibleProgressPanel;
            }
            set {
                if (_IsVisibleProgressPanel != value) {
                    _IsVisibleProgressPanel = value;
                    RaisePropertyChanged(IsVisibleProgressPanelPropertyName);
                }
            }
        }

        public string ProgressPrimaryMessage {
            get {
                return _ProgressMessage;
            }
            set {
                if (_ProgressMessage != value) {
                    _ProgressMessage = value;
                    RaisePropertyChanged(ProgressPrimaryMessagePropertyName);
                }
            }
        }

        public string ProgressSecondaryMessage {
            get {
                return _ProgressMessage;
            }
            set {
                if (_ProgressMessage != value) {
                    _ProgressMessage = value;
                    RaisePropertyChanged(ProgressSecondaryMessagePropertyName);
                }
            }
        }

        public decimal ProgressValue {
            get {
                return _ProgressValue;
            }
            set {
                if (_ProgressValue != value) {
                    _ProgressValue = value;
                    RaisePropertyChanged(ProgressValuePropertyName);
                }
            }
        }

        bool ProcesoDeValidacion = false;
        LibResponse ArchivoValido = new LibResponse();
        clsVendedorImpExp vVendedorImport = new clsVendedorImpExp();

        #endregion //Propiedades

        #region Constructores e Inicializadores

        public VendedorImportarExportarViewModel(string initModuleName, ILibImpExp initImportExport) : base(initModuleName, initImportExport) {
        }

        internal void InitializeViewModel(eAccionSR eAccionSR) {
            base.InitializeLookAndFeel();
        }

        protected override void InitializeRibbon() {
            base.InitializeRibbon();
            RibbonData.TabDataCollection[0].GroupDataCollection[0].ControlDataCollection.Insert(0, CreateAdicionalRibbonGroup());
        }

        protected override void InitializeCommands() {
            base.InitializeCommands();
            ImportarCommand = new RelayCommand(ExecuteAction, CanExecuteImportarCommand);
            BuscarDirectorioArchivoCommand = new RelayCommand(BuscarDirectorioArchivo, CanExecutBuscarDirectorioArchivo);
        }

        private void InitializeBackgroundWorker() {
            _BWorker = new BackgroundWorker();
            _BWorker.WorkerReportsProgress = true;
            _BWorker.WorkerSupportsCancellation = false;
            _BWorker.DoWork += new DoWorkEventHandler(BWorker_DoWork);
            _BWorker.ProgressChanged += new ProgressChangedEventHandler(BWorker_ProgressChanged);
            _BWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BWorker_RunWorkerCompleted);
        }

        #endregion //Constructores

        #region Comandos
        private LibRibbonButtonData CreateAdicionalRibbonGroup() {
            LibRibbonButtonData vResult = new LibRibbonButtonData() {
                Label = "Importar",
                Command = ImportarCommand,
                LargeImage = new Uri("/LibGalac.Aos.UI.WpfRD;component/Images/report.png", UriKind.Relative),
                ToolTipDescription = "Importar",
                ToolTipTitle = "Importar"
            };
            return vResult;
        }
        protected void ExecuteAction() {
            LibResponse vExisteDirectorio = ExisteDirectorio(NombreDelArchivo);
            try {
                if (vExisteDirectorio.Success) {
                    InitializeBackgroundWorker();
                    ImportarCommand.RaiseCanExecuteChanged();
                    CancelCommand.RaiseCanExecuteChanged();
                    BuscarDirectorioArchivoCommand.RaiseCanExecuteChanged();
                    ProcesoDeValidacion = true;
                    _BWorker.RunWorkerAsync();
                } else {
                    LibMessages.MessageBox.Alert(this, vExisteDirectorio.GetInformation(), "Alerta");
                }
            } catch (AccessViolationException) {
                throw;
            } catch (Exception vEx) {
                LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }
        LibResponse ExisteDirectorio(string valPath) {
            LibResponse vResult = new LibResponse();
            bool vExisteArchivo = LibDirectory.FileExists(valPath);
            if (!vExisteArchivo) {
                vResult.Success = false;
                vResult.AddError("No se encontr� la ruta indicada o el archivo de datos. Revise sus datos y coloque la informaci�n nuevamente.");
            } else {
                vResult.Success = true;
            }
            return vResult;
        }
        protected void BuscarDirectorioArchivo() {
            OpenFileDialog vSFD = new OpenFileDialog();
            if (TipoDeSeparacion == eExportDelimiterType.Csv) {
                vSFD.Filter = "Text Files (.csv)|*.csv|All Files (*.*)|*.*";
            } else {
                vSFD.Filter = "Text Files (.txt)|*.txt|All Files (*.*)|*.*";
            }
            vSFD.FilterIndex = 1;
            vSFD.Multiselect = false;
            bool? vResult = vSFD.ShowDialog();
            if (vResult == true) {
                NombreDelArchivo = vSFD.FileName;
            }
        }
        LibResponse ElArchivoEsValido(string valPath, eExportDelimiterType valSeparador, BackgroundWorker valBWorker, ref int vCantidadRegistrosValidos) {
            LibResponse vResult = new LibResponse();
            StringBuilder vErrorMessage = new StringBuilder();
            XElement vXElement = vVendedorImport.ImportFile(valPath, LibEExportDelimiterType.ToDelimiter(valSeparador));
            vResult.Success = vVendedorImport.VerifyIntegrityOfRecord(vXElement, vErrorMessage);
            if (!LibString.IsNullOrEmpty(vErrorMessage.ToString())) {
                vResult.AddError("" + vErrorMessage);
            }
            return vResult;
        }
        private void Importar() {
            string vOption = "";
            if (ArchivoValido.Success) {
                if (!LibText.IsNullOrEmpty(ArchivoValido.GetInformation())) {
                    LibMessages.MessageBox.Information(this, ArchivoValido.GetInformation(), "Informaci�n");
                    string vMessage = string.Format("Si usted desea continuar con el proceso, pulse la opci�n de importar (Importar).");
                    bool vCanceled = false;
                    vOption = LibMessages.MessageBox.RequestOption(this, vMessage, Title, new string[] { "Importar", "Cancelar" }, out vCanceled);
                } else {
                    vOption = "Importar";
                }
                if (LibString.S1IsEqualToS2(vOption, "Importar")) {
                    ProcesoDeValidacion = false;
                    InitializeBackgroundWorker();
                    _BWorker.RunWorkerAsync();
                } else {
                    RaiseRequestCloseEvent();
                }
            } else {
                string vMessageNotSuccessful = "Existe informaci�n inv�lida en el archivo. "
                    + Environment.NewLine
                    + ArchivoValido.GetInformation();
                LibMessages.MessageBox.Information(this, vMessageNotSuccessful, "Informaci�n");
                RaiseRequestCloseEvent();
            }
        }
        private void BWorker_DoWork(object sender, DoWorkEventArgs e) {
            int vCantidadRegistrosValidosValidas = 0;
            if (ProcesoDeValidacion) {
                ArchivoValido = ElArchivoEsValido(NombreDelArchivo, TipoDeSeparacion, _BWorker, ref vCantidadRegistrosValidosValidas);
            } else {
                vVendedorImport.Importar(NombreDelArchivo, TipoDeSeparacion, _BWorker); //ImportFile(NombreDelArchivo, LibEExportDelimiterType.ToDelimiter(TipoDeSeparacion)); //PROBAR CON IMPORTFILE AQUI
            }
        }

        private void BWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            if (e.Cancelled) {
                LibMessages.MessageBox.Alert(this, "La acci�n ha sido cancelada por el usuario.", "Importar");
            } else if (e.Error != null) {
                if (e.Error is AccessViolationException) {
                    throw e.Error;
                } else {
                    LibMessages.RaiseError.ShowError(e.Error, ModuleName);
                    RaiseRequestCloseEvent();
                }
            } else {
                IsVisibleProgressPanel = false;
                ProgressValue = 0;
                ProgressPrimaryMessage = "";
                _BWorker = null;
                if (ProcesoDeValidacion) {
                    Importar();
                } else {
                    LibMessages.MessageBox.Information(this, "El proceso termino satisfactoriamente", "");
                    CancelCommand.RaiseCanExecuteChanged();
                    RaiseRequestCloseEvent();
                }
            }
        }

        private void BWorker_ProgressChanged(object sender, ProgressChangedEventArgs e) {
            string vMensaje = e.UserState as string;
            ProgressPrimaryMessage = vMensaje;
            if (ProgressValue <= 100) {
                if (e.ProgressPercentage == 1) {
                    ProgressValue++;
                } else {
                    ProgressValue = e.ProgressPercentage;
                }
            } else {
                ProgressValue = 1;
            }
        }

        public override bool OnClosing() {
            if (_BWorker != null && _BWorker.IsBusy) {
                LibMessages.MessageBox.Alert(this, "El proceso no puede ser cancelado", "Alerta!");
                return true;
            }
            return base.OnClosing();
        }



        private bool CanExecuteImportarCommand() {
            return _BWorker == null;
        }

        protected override bool CanExecuteCancel() {
            return _BWorker == null;
        }

        bool CanExecutBuscarDirectorioArchivo() {
            return _BWorker == null;
        }

        #endregion //Comandos
    }
}