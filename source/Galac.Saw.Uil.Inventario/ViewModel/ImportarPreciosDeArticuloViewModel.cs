using Galac.Saw.Brl.Inventario;
using Galac.Saw.Ccl.Inventario;
using LibGalac.Aos.Base;
using LibGalac.Aos.UI.Mvvm;
using LibGalac.Aos.UI.Mvvm.Command;
using LibGalac.Aos.UI.Mvvm.Messaging;
using LibGalac.Aos.UI.Mvvm.Ribbon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Galac.Saw.Uil.Inventario.ViewModel {
    public class ImportarPreciosDeArticuloViewModel : LibGenericViewModel {

        #region Variables y Constantes

        private const string PathFilePropertyName = "PathFile";
        private const string TipoDePrecioPropertyName = "TipoDePrecio";
        private const string PbIsIndeterminatePropertyName = "PbIsIndeterminate";
        private const string ProgressPercentPropertyName = "ProgressPercent";
        private const string DesincorporarArticulosPropertyName = "DesincorporarArticulos";
        private const string ImportandoArchivosPropertyName = "ImportandoArchivos";
        private string _FileName = string.Empty;
        private ePrecioAjustar _TipoDePrecio;
        private decimal _ProgressPercent;
        private bool _DesincorporarArticulos;
        private bool _ImportandoArchivos;

        #endregion

        #region Propiedades

        public override string ModuleName {
            get {
                return "Ajuste de Precios desde Archivo";
            }
        }

        public string PathFile {
            get {
                return _FileName;
            }
            set {
                if(_FileName != value) {
                    _FileName = value;
                    RaisePropertyChanged(PathFilePropertyName);
                    ImportarCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public ePrecioAjustar TipoDePrecio {
            get {
                return _TipoDePrecio;
            }
            set {
                if(_TipoDePrecio != value) {
                    _TipoDePrecio = value;
                    RaisePropertyChanged(TipoDePrecioPropertyName);
                }
            }
        }

        public ePrecioAjustar[] ArrayTipoDePrecio {
            get {
                return LibEnumHelper<ePrecioAjustar>.GetValuesInArray();
            }
        }

        public decimal ProgressPercent {
            get {
                return _ProgressPercent;
            }
            set {
                if(_ProgressPercent != value) {
                    _ProgressPercent = value;
                    RaisePropertyChanged(ProgressPercentPropertyName);
                }
            }
        }

        public bool IsDesincorporarArticulosSelected {
            get {
                return _DesincorporarArticulos;
            }
            set {
                if(_DesincorporarArticulos != value) {
                    _DesincorporarArticulos = value;
                    RaisePropertyChanged(DesincorporarArticulosPropertyName);
                }
            }
        }

        public bool ImportandoArchivos {
            get {
                return _ImportandoArchivos;
            }
            set {
                if(_ImportandoArchivos != value) {
                    _ImportandoArchivos = value;
                    RaisePropertyChanged(ImportandoArchivosPropertyName);
                    CancelCommand.RaiseCanExecuteChanged();
                    ImportarCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public RelayCommand ChooseFileCommand { get; private set; }

        public RelayCommand ImportarCommand { get; private set; }

        public RelayCommand VerInformacionDeFormatoCommand { get; private set; }

        #endregion

        #region Constructores e Inicializadores

        public ImportarPreciosDeArticuloViewModel() : base() {
            Title = ModuleName;
        }

        protected override void InitializeLookAndFeel() {
            base.InitializeLookAndFeel();
            ProgressPercent = 0;
        }

        protected override void InitializeRibbon() {
            base.InitializeRibbon();
            LibRibbonControlData vRibbonControlSalir = RibbonData.TabDataCollection[0].GroupDataCollection[0].ControlDataCollection[0];
            RibbonData.RemoveRibbonGroup("Acciones");
            if(RibbonData.TabDataCollection != null && RibbonData.TabDataCollection.Count > 0) {
                RibbonData.TabDataCollection[0].AddTabGroupData(CreateAccionesRibbonGroup());
                RibbonData.TabDataCollection[0].GroupDataCollection[0].AddRibbonControlData(vRibbonControlSalir);
            }
        }

        protected override void InitializeCommands() {
            base.InitializeCommands();
            ChooseFileCommand = new RelayCommand(ExecuteChooseFile, CanExecuteChooseFile);
            ImportarCommand = new RelayCommand(ExecuteImportar, CanExecuteImportar);
            VerInformacionDeFormatoCommand = new RelayCommand(ExecuteVerInformacionDeFormatoCommand, CanExecuteVerInformacionDeFormatoCommand);
        }

        #endregion

        #region Creacción de RibbonGroup

        private LibRibbonGroupData CreateAccionesRibbonGroup() {
            LibRibbonGroupData vResult = new LibRibbonGroupData("Acciones");
            vResult.ControlDataCollection.Add(new LibRibbonButtonData() {
                Label = "Importar",
                Command = ImportarCommand,
                LargeImage = new Uri("/LibGalac.Aos.UI.WpfRD;component/Images/read.png", UriKind.Relative),
                ToolTipDescription = "Importar Precios de Articulo desde Archivo",
                ToolTipTitle = "Importar"
            });
            vResult.ControlDataCollection.Add(new LibRibbonButtonData() {
                Label = "Ayuda",
                Command = VerInformacionDeFormatoCommand,
                LargeImage = new Uri("/LibGalac.Aos.UI.WpfRD;component/Images/MessageQuestion.png", UriKind.Relative),
                ToolTipDescription = "Ver Ayuda acerca del formato del Archivo",
                ToolTipTitle = "Ayuda"
            });
            return vResult;
        }

        #endregion //Creación de RiibbonGroup

        #region Comandos

        private void ExecuteChooseFile() {
            using(var vFileBrowser = new OpenFileDialog()) {
                if(vFileBrowser.ShowDialog() == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(vFileBrowser.FileName)) {
                    PathFile = vFileBrowser.FileName;
                }
            }
        }

        private bool CanExecuteChooseFile() {
            return true;
        }

        private void ExecuteImportar() {
            if(((IImportarPreciosDeArticuloPdn)new clsImportarPreciosDeArticuloNav()).ValidarExtensionDeArchivo(PathFile)) {
                ImportarPrecios();
                ImportandoArchivos = false;
                ProgressPercent = 0;
            } else {
                LibMessages.MessageBox.Information(this, "El Formato del archivo no es correcto. Verifique e intente nuevamente.", "Información");
            }
        }

        private bool CanExecuteImportar() {
            return !LibString.IsNullOrEmpty(PathFile) && !ImportandoArchivos;
        }

        private void ExecuteVerInformacionDeFormatoCommand() {
            StringBuilder vInfo = new StringBuilder();
            vInfo.AppendLine("");
            vInfo.AppendLine("DISEÑO DEL REGISTRO DE AJUSTE DE PRECIOS:");
            vInfo.AppendLine("      CAMPO                        FORMATO");
            vInfo.AppendLine("      1.  Código                    PIC X (30)");
            vInfo.AppendLine("      2.  Descripción             PIC X (5000)");
            vInfo.AppendLine("      3.  PrecioNivel1Bs        PIC 9 (15).9999");
            vInfo.AppendLine("      4.  PrecioNivel2Bs        PIC 9 (15).9999");
            vInfo.AppendLine("      5.  PrecioNivel3Bs        PIC 9 (15).9999");
            vInfo.AppendLine("      6.  PrecioNivel4Bs        PIC 9 (15).9999");
            vInfo.AppendLine("      7.  PrecioNivel1ME       PIC 9 (15).9999");
            vInfo.AppendLine("      8.  PrecioNivel2ME       PIC 9 (15).9999");
            vInfo.AppendLine("      9.  PrecioNivel3ME       PIC 9 (15).9999");
            vInfo.AppendLine("     10. PrecioNivel4ME       PIC 9 (15).9999");
            vInfo.AppendLine();
            vInfo.AppendLine("Notas");
            vInfo.AppendLine("- Los campos numéricos no deben llevar separador de millares y utilizar punto (.) como separador de decimales.");
            vInfo.AppendLine("- El carácter separador de campos es el punto y coma (;).");
            vInfo.AppendLine("- Todos los campos son requeridos. En caso de no querer modificar la descripción del artículo no coloque nada en el campo correspondiente a Descripción.");
            vInfo.AppendLine();
            vInfo.AppendLine("Ejemplos:");
            vInfo.AppendLine();
            vInfo.AppendLine("001;Descripcion;1.9999;2.9999;3.9999;4.9999;1.0000;2.0000;3.0000;4.0000");
            vInfo.AppendLine("002;Descripcion;1.0000;2.0000;3.0000;4.0000;0;0;0;0");
            vInfo.AppendLine("003;;0;0;0;0;13.9999;14.9999;15.9999;16.9999");
            LibMessages.MessageBox.Information(this, vInfo.ToString(), "Ayuda");
        }

        private bool CanExecuteVerInformacionDeFormatoCommand() {
            return true;
        }

        protected override bool CanExecuteCancel() {
            return base.CanExecuteCancel() && !ImportandoArchivos; ;
        }

        #endregion

        #region Metodos

        private Task ImportarPrecios() {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            clsImportarPreciosDeArticuloNav vImportarNav = new clsImportarPreciosDeArticuloNav(TipoDePrecio);
            vImportarNav.OnProgressPercentChanged += PercentageChanged;
            ImportandoArchivos = true;
            string vRespuesta = string.Empty;
            Task vImportarPreciosTask = Task.Factory.StartNew(() => {
                vRespuesta = vImportarNav.ImportFile(PathFile, IsDesincorporarArticulosSelected);
            });
            vImportarPreciosTask.ContinueWith(t => {
                if(t.IsCompleted) {
                    LibMessages.MessageBox.Information(this, vRespuesta, "Información");
                    PercentageChanged(0);
                }
            }, cancellationTokenSource.Token, TaskContinuationOptions.OnlyOnRanToCompletion, TaskScheduler.FromCurrentSynchronizationContext());
            vImportarPreciosTask.ContinueWith(t => {
                ImportandoArchivos = false;
                PercentageChanged(0);
                if(t.Exception.InnerException != null) {
                    LibMessages.MessageBox.Information(this, "Ocurrió un error al importar.\r\n" + t.Exception.Message + " - " + t.Exception.InnerException.Message, "Información");
                } else {
                    LibMessages.MessageBox.Information(this, "Ocurrió un error al importar.\r\n" + t.Exception.Message, "Información");
                }
            }, cancellationTokenSource.Token, TaskContinuationOptions.OnlyOnFaulted, TaskScheduler.FromCurrentSynchronizationContext());
            return vImportarPreciosTask;
        }

        private void PercentageChanged(decimal valPorcentage) {
            ProgressPercent = valPorcentage;
        }

        #endregion

    }
}
