using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
using Galac.Adm.Brl.GestionCompras;
using Galac.Adm.Ccl.GestionCompras;
using System.ComponentModel;

namespace Galac.Adm.Uil.GestionCompras.ViewModel {
    public class LibroDeComprasViewModel : LibGenericViewModel {
        #region Constantes
        public const string NumeroPropertyName = "Numero";
        public const string MesPropertyName = "Mes";
        public const string AnoPropertyName = "Ano";
        public const string ProgressPercentPropertyName = "ProgressPercent";
        public const string ProgressMessagePropertyName = "ProgressMessage";
        public const string ProgressValuePropertyName = "ProgressValue";
        public const string IsVisibleProgressPanelPropertyName = "IsVisibleProgressPanel";
        public const string RegistroDeComprasCompletoPropertyName = "RegistroDeComprasCompleto";
        public const string RegistroDeComprasResumidoPropertyName = "RegistroDeComprasResumido";
        
        ILibroDeComprasPdn insLibroDeCompras = new clsLibroDeComprasNav();
        int vConsecutivoCompania = LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania");
        string vNombreCompaniaParaInformes = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("RegistroDeCompras", "NombreCompaniaParaInformes");
        string vNumeroRIF = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("RegistroDeCompras", "NumeroRIF");        
        #endregion

        #region Variables
        int _ConsecutivoCompania;
        string _Numero;
        string _Mes;
        string _Ano;
        string _ProgressPercent;
        decimal _ProgressValue = 0;
        string _ProgressMessage = null;
        bool _RegistroDeComprasCompleto;        
        bool _RegistroDeComprasResumido;
        bool _IsVisibleProgressPanel = false;
        BackgroundWorker _BWorker;
        bool vGeneraLibroDeCompras;
        #endregion Variables

        #region Propiedades

        public override string ModuleName {
            get {
              return "Registro De Compras";
            }
        }

        public int ConsecutivoCompania {
            get {
                return _ConsecutivoCompania;
            }
            set {
                if (_ConsecutivoCompania != value) {
                    _ConsecutivoCompania = value;
                }
            }
        }

        public string Numero {
            get {
                return _Numero;
            }
            set {
                if (_Numero != value) {
                    _Numero = value;
                    RaisePropertyChanged(RegistroDeComprasResumidoPropertyName);
                }
            }
        }

        public bool RegistroDeComprasCompleto {
            get { 
                return _RegistroDeComprasCompleto; 
            }
            set { 
                _RegistroDeComprasCompleto = value;                             
                RaisePropertyChanged(RegistroDeComprasCompletoPropertyName);
                MostrarOcultarRibbonButton();
            }
        }

        public bool RegistroDeComprasResumido {
            get {
                return _RegistroDeComprasResumido;
            }
            set {                
                _RegistroDeComprasResumido = value;                                              
                RaisePropertyChanged(RegistroDeComprasResumidoPropertyName);              
            }
        }

        [LibRequired(ErrorMessage = "El campo de Mes es requerido.")]
        [LibCustomValidation("MesValidating")]
        public string Mes {
            get {
                return _Mes;
            }
            set {
                if (_Mes != value) {
                    _Mes = value;
                    RaisePropertyChanged (MesPropertyName);
                }
            }
        }

        private ValidationResult MesValidating() {
            ValidationResult vResult = ValidationResult.Success;           
            if (LibConvert.ToInt(_Mes) > 12) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.MessageBox.Information(null, "Mes no puede ser mayor a 12.", "Información");
                Mes = "";
            } else if (LibConvert.ToInt(_Mes) < 0) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.MessageBox.Information(null, "Mes debe ser mayor o igual a 1.", "Información");
                Mes = "";
            };           
            return vResult;
        }

        [LibRequired(ErrorMessage = "El campo de Año es requerido.")]
        [LibCustomValidation("AnoValidating")]
        public string Ano {
            get {
                return _Ano;
            }
            set {
                if (_Ano != value) {
                    _Ano = value;
                    RaisePropertyChanged(AnoPropertyName);
                }
            }
        }

        private ValidationResult AnoValidating() {
            ValidationResult vResult = ValidationResult.Success;
            int vAñoActual = LibDate.Today().Year;
            int vMesActual = LibDate.Today().Month;
            if (LibConvert.ToInt(Ano) > vAñoActual) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.MessageBox.Information(null, "Año no puede ser Mayor al " + vAñoActual + ".", "Información");
                Ano = vAñoActual.ToString();
            }else if (LibConvert.ToInt(Mes) > vMesActual && LibConvert.ToInt(_Ano) == vAñoActual) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.MessageBox.Information(null, "El Mes no puede ser Mayor al " + vMesActual + " ya que el año selccionado es " + vAñoActual + ".", "Información");
                vMesActual = vMesActual - 1;
                Mes = vMesActual.ToString();
            }
            return vResult;
        }

        public string ProgressPercent {
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

        public string ProgressMessage {
            get {
                return _ProgressMessage;
            }
            set {
                if (_ProgressMessage != value) {
                    _ProgressMessage = value;
                    RaisePropertyChanged(ProgressMessagePropertyName);
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

        #endregion //Propiedades

        public RelayCommand GenerarLibroDeComprasCommand {
            get;
            private set;
        }

        public RelayCommand GenerarPLELibroDeComprasCommand {
            get;
            private set;
        }

        #region Constructores
        public LibroDeComprasViewModel() {
        }
       

        #endregion //Constructores
        #region Metodos Generados

        protected override void InitializeLookAndFeel() {
            RegistroDeComprasCompleto = true;
            sCalculaMesYAnoParaInput();
        }

        private void GenerarLibroDeCompras() {
            Mes = LibText.FillWithCharToLeft(Mes, "0", 2);
            string vMesPeriodo = Mes;
            string vAnoPeriodo = Ano;

            try {
                insLibroDeCompras.GenerarLibroDeCompras(vConsecutivoCompania, Mes, Ano, vNombreCompaniaParaInformes, vNumeroRIF, _BWorker);
            } catch (GalacException) {
                throw;
            }
        }
       
        private void GenerarPLELibroDeCompras() {
            Mes = LibText.FillWithCharToLeft(Mes, "0", 2);
            string vMesPeriodo = Mes;
            string vAnoPeriodo = Ano;          
            try {
                insLibroDeCompras.GenerarPLELibroDeCompras(vConsecutivoCompania, Mes, Ano, vNombreCompaniaParaInformes, vNumeroRIF, RegistroDeComprasCompleto);
            } catch (GalacException) {                
                throw;
            }                       
        }
        
        private bool CanExecuteGenerarLibroDeComprasCommand() {
            return true;
        }

        private bool CanExecuteGenerarLibroDeComprasPLECommand() {
            return true;
        }


        public void ExecuteGenerarLibroDeComprasCommand() {
            try {
                if (LibString.IsNullOrEmpty(Mes, true) || LibString.IsNullOrEmpty(Ano, true)) {
                    LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.MessageBox.Information(null, "Campos Requeridos, verifique e Intente de Nuevo.", "Información");
                } else {
                    vGeneraLibroDeCompras = true;
                    ExecuteAction();
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
            }
        }

        public void ExecuteGenerarLibroDeComprasPLECommand() {
            try {
                if (LibString.IsNullOrEmpty(Mes, true) || LibString.IsNullOrEmpty(Ano, true)) {
                    LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.MessageBox.Information(null, "Campos Requeridos, verifique e Intente de Nuevo.", "Información");
                } else {
                    vGeneraLibroDeCompras = false;
                    ExecuteAction();
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
            }
        }

        protected override void InitializeCommands() {
            base.InitializeCommands();
            GenerarLibroDeComprasCommand = new RelayCommand(ExecuteGenerarLibroDeComprasCommand, CanExecuteGenerarLibroDeComprasCommand);
            GenerarPLELibroDeComprasCommand = new RelayCommand(ExecuteGenerarLibroDeComprasPLECommand, CanExecuteGenerarLibroDeComprasPLECommand);
        }

        protected override void InitializeRibbon() {
            base.InitializeRibbon();           
            RibbonData.ApplicationMenuData = new LibRibbonMenuButtonData() {
                IsVisible = false
            };
            if (RibbonData.TabDataCollection != null && RibbonData.TabDataCollection.Count > 0) {               
                RibbonData.RemoveRibbonControl("Acciones", "Insertar");
                var vAccionesGrupo = RibbonData.TabDataCollection[0].GroupDataCollection[0];
                RibbonData.TabDataCollection[0].GroupDataCollection.Remove(vAccionesGrupo);              
                RibbonData.TabDataCollection[0].AddTabGroupData(CreateGenerarLibroDeComprasRibbonButtonGroup());                
                RibbonData.TabDataCollection[0].AddTabGroupData(CreateGenerarPLELibroDeComprasRibbonButtonGroup());               
            }
        }

        private void MostrarOcultarRibbonButton() {
            if (RegistroDeComprasCompleto) {
                RibbonData.TabDataCollection[0].GroupDataCollection[0].IsVisible=true;
            } else {
                RibbonData.TabDataCollection[0].GroupDataCollection[0].IsVisible = false;                
            }        
        }

        private LibRibbonGroupData CreateGenerarLibroDeComprasRibbonButtonGroup() {
            LibRibbonGroupData vResult = new LibRibbonGroupData("");
            vResult.ControlDataCollection.Add(new LibRibbonButtonData() {
                Label = "Generar",
                Command = GenerarLibroDeComprasCommand,
                LargeImage = new Uri("/Galac.Adm.Uil.GestionCompras;component/Images/F6.png", UriKind.Relative),
                ToolTipDescription = "Guarda los cambios en " + ModuleName + ".",
                ToolTipTitle = "Ejecutar Acción (F6)",
                IsVisible = true,
                KeyTip = "F6"                
            });
            return vResult;
        }

        private LibRibbonGroupData CreateGenerarPLELibroDeComprasRibbonButtonGroup() {
            LibRibbonGroupData vResult = new LibRibbonGroupData("");
            vResult.ControlDataCollection.Add(new LibRibbonButtonData() {
                Label = "Generar PLE",
                Command = GenerarPLELibroDeComprasCommand,
                LargeImage = new Uri("/Galac.Adm.Uil.GestionCompras;component/Images/F7.png", UriKind.Relative),
                ToolTipDescription = "Guarda los cambios en " + ModuleName + ".",
                ToolTipTitle = "Ejecutar Acción (F7)",
                IsVisible = true,
                KeyTip = "F7"
            });
            return vResult;
        }

        protected void ExecuteAction() {
            try {
                if (_BWorker != null && _BWorker.IsBusy) {
                    return;
                }
                InitializeBackgroundWorker();
                CancelCommand.RaiseCanExecuteChanged();
                _BWorker.RunWorkerAsync();
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void InitializeBackgroundWorker() {
            _BWorker = new BackgroundWorker();
            _BWorker.WorkerReportsProgress = true;
            _BWorker.WorkerSupportsCancellation = false;
            _BWorker.DoWork += new DoWorkEventHandler(BWorker_DoWork);
            _BWorker.ProgressChanged += new ProgressChangedEventHandler(BWorker_ProgressChanged);
            _BWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BWorker_RunWorkerCompleted);
        }

        private void BWorker_DoWork(object sender, DoWorkEventArgs e) {
            try {
                if (vGeneraLibroDeCompras) {
                    GenerarLibroDeCompras();
                } else {
                    GenerarPLELibroDeCompras();
                }
            } catch (GalacAlertException vEx) {
                e.Result = vEx;
            }
        }

        private void BWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
                                            
            if (e.Cancelled) {
                LibMessages.MessageBox.Alert(this, "La acción ha sido cancelada por el usuario.", "Exportar");
            } else if (e.Error != null) {
                LibMessages.RaiseError.ShowError(e.Error, "Error", ModuleName, "");
            } else if (e.Result != null) {
                LibMessages.MessageBox.Alert(this, e.Result.ToString(), "");
            } else {
                string vRutaLibroDeVentas = new Galac.Saw.Brl.Inventario.clsLibroElectronicoHelper().PathLibroElectronico("Registro de Compras");
                LibMessages.MessageBox.Information(this, "El archivo se generó satisfactoriamente en la ruta " + vRutaLibroDeVentas, "");                                
                IsVisibleProgressPanel = false;
                _BWorker = null;
                CancelCommand.RaiseCanExecuteChanged();
                RaiseRequestCloseEvent();
            }
        }

        private void BWorker_ProgressChanged(object sender, ProgressChangedEventArgs e) {
            ProgressPercent = ProgressValue.ToString() + "%";
            if (ProgressValue <= 100) {
                if (ProgressValue == 0) {
                    ProgressMessage = "Creando encabezado del Informe....";
                    IsVisibleProgressPanel = true;
                    RaisePropertyChanged("IsVisibleProgressPanel");
                } else if (ProgressValue == 20) {
                    ProgressMessage = "Cargando Datos en el Informe....";
                }
            } else if (ProgressValue == 99) {
                ProgressMessage = "Cargado de Datos culminado....";
            }
            ProgressValue = e.ProgressPercentage;
            ProgressPercent = ProgressValue.ToString() + "%";
        }

        private void sCalculaMesYAnoParaInput() {
            int vMes = LibDate.Today().Month - 1;
            int vAño = LibDate.Today().Year;          
            if (vMes == 0) {
                vMes = 12;
                vAño = LibDate.Today().Year - 1;
            }
            _Mes = vMes.ToString();
            _Ano = vAño.ToString();
            Mes = vMes.ToString();
            Ano = vAño.ToString();
        }

        #endregion //Metodos Generados


    } //End of class LibroDeComprasViewModel

} //End of namespace Galac.Adm.Uil.Compras

