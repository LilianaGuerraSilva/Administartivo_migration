using Galac.Saw.Properties;
using LibGalac.Aos.Base;
using LibGalac.Aos.Cib;
using LibGalac.Aos.Dal;
using LibGalac.Aos.DefGen;
using LibGalac.Aos.UI.Mvvm;
using LibGalac.Aos.UI.Mvvm.Command;
using LibGalac.Aos.UI.Mvvm.Messaging;
using LibGalac.Aos.UI.Mvvm.Ribbon;
using LibGalac.Aos.UI.Wpf;
using LibGalac.Aos.Uil.Usal;
using System;
using System.ComponentModel.Composition;
using System.Text;
using System.Xml.Linq;

namespace Galac.Saw.ViewModel {
    [Export(typeof(ShellViewModel))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ShellViewModel:LibShellViewModelBase {
        #region Constantes
        #endregion

        #region Variables
        private GUserLogin _Login = new GUserLogin();
        #endregion

        #region Propiedades
        public RelayCommand TestInterop {
            get;
            private set;
        }

        public RelayCommand CrearTablasCommand {
            get;
            private set;
        }

        public RelayCommand CrearVistasYSpsCommand {

            get;
            private set;
        }
        #endregion

        #region Constructores
        public ShellViewModel() {
            clsProcesosPostAccionCia.RegisterMessages(this);
            clsProcesosPostAccionPeriodo.RegisterMessages(this);
            LibGalac.Aos.Brl.LibBusinessProcess.Register(this,"EscogerPeriodo",OnEscogerPeriodo);
            IsVisibleUserStatusBarRegion = false;
            IsVisibleProtectionStatusBarRegion = true;
            CanChooseMultiFileControllers = true;
            LoadProductAdmittedComponents(Resources.Components);
            MfcCompanyRecordName = "Compania";
            MfcCompanyFieldName = "NombreCorto";
            Mfc1RecordName = "Periodo";
            Mfc1FieldName = "ConsecutivoPeriodo";
            Mfc1Label = "Período";
            MfcCompanyLabel = "Empresa";
            Mfc1FieldForFrindlyName = "AperturaCierreStr";

            this.PropertyChanged += (s,e) => {
                if(e.PropertyName == "SelectedComponent") {
                    RaisePropertyChanged(RibbonDataPropertyName);
                }
            };
        }
        #endregion

        #region Métodos
        protected override ILibRestructureDb GetRestructureApp() {
            return new Galac.Saw.DDL.clsReestructurarDatabase(ReadDataBaseCurrentVersion());
        }

        protected override ILibRestructureDb GetRestructureLib() {
            return new LibControlVersion();
        }

        protected override void InitializeLookAndFeel() {
            base.InitializeLookAndFeel();
            if(LibDefGen.ProgramInfo != null) {
                DisplayName = LibDefGen.ProgramInfo.ProgramName;
            }
        }

        protected override void InitializeCommands() {
            base.InitializeCommands();
            CrearTablasCommand = new RelayCommand(ExecuteCrearTablasCommand,CanExecuteCrearTablasCommand);
            CrearVistasYSpsCommand = new RelayCommand(ExecuteCrearVistasYSPCommand,CanExecuteCrearVistasYSPCommand);
            TestInterop = new RelayCommand(ExecuteTestInterop);
        }

        protected override void InitializeRibbonInternal() {
            base.InitializeRibbonInternal();
            RibbonData.ApplicationMenuData.ControlDataCollection.Insert(RibbonData.ApplicationMenuData.ControlDataCollection.Count - 1,
                new LibRibbonApplicationMenuItemData() {
                    Command = TestInterop,
                    Label = "Ver Valores en Memoria"
                });
        }

        protected override void ExecuteChooseUserCommand() {
            try {
                if(_Login.ChooseUser()) {
                    LibSecurityManager.SetCurrentPrincipalPermissions(_Login.CurrentPermissions);
                    CurrentUserName = _Login.LoginUserName;
                    LastUsedCompany = _Login.LoginLastUsedCompany;
                    CompanyName = string.Empty;
                    RaiseCanExecuteChanged();
                    IsVisibleUserStatusBarRegion = true;
                } else if(LibString.IsNullOrEmpty(CurrentUserName,true)) {
                    System.Windows.Application.Current.Shutdown();
                }
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
            }
        }

        protected override void ExecuteOnClosedCommand() {
            try {
                string vCompany = LibString.IsNullOrEmpty(CompanyName,true) ? LastUsedCompany : CompanyName;
                if(!LibString.IsNullOrEmpty(vCompany)) {
                    _Login.UpdateNameLastUsedCompany(CurrentUserName,vCompany);
                }
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
            }
        }

        protected override void InitializeBackupSettings() {
            base.InitializeBackupSettings();
            clsBackupSettings vBackupSettings = new clsBackupSettings();
            vBackupSettings.Initialize();
            LibRestructurationHelper.BackupSettings = vBackupSettings;
            LibGlobalValues.Instance.BackupSettings = vBackupSettings;
            clsRestoreSettings vRestoreSettings = new clsRestoreSettings();
            LibGlobalValues.Instance.RestoreSettings = vRestoreSettings;
        }

        protected override void OnChooseMultiFileControllersSuccess() {
            base.OnChooseMultiFileControllersSuccess();
            try {
                int vConsecutivoCompania = LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania");
                if(vConsecutivoCompania > 0) {
                    LoadGlobalValuesComponents();
                    //IPeriodoPdn vPdn = new clsPeriodoNav();
                    //if (vPdn.LaCompaniaTienePeriodos(vConsecutivoCompania)) {
                    //    bool vEsCatalogoGeneral = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Compania", "EsCatalogoGeneral");
                    //    if (vEsCatalogoGeneral) {
                    //        LibMessages.MessageBox.Information(this,
                    //            "La Empresa actual es una Empresa que viene pre-cargada como ejemplo de un catálogo de cuentas y no puede ser modificada." +
                    //            Environment.NewLine + Environment.NewLine + "Algunos menús aparecerán inactivos." +
                    //            Environment.NewLine + Environment.NewLine + "Para tener acceso a todas las funciones y menús del programa inserte una nueva Empresa.", "Escoger Empresa");
                    //    }
                    //} else {
                    //    string vNombreCompania = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Compania", "NombreCorto");
                    //    LibMessages.MessageBox.Alert(this, string.Format("La empresa '{0}' no tiene periodos. A continuación se pedirán los datos del primer período.", vNombreCompania), "ADVERTENCIA");
                    //    PeriodoViewModel vVieModel = new PeriodoViewModel();
                    //    vVieModel.InitializeViewModel(eAccionSR.Insertar);
                    //    LibMessages.EditViewModel.ShowEditor(vVieModel, true);
                    //}
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
            }
        }

        private void OnEscogerPeriodo(LibGalac.Aos.Brl.LibBusinessProcessMessage valMessage) {
            try {
                ExecuteChooseMultiFileControllersCommand(Mfc1RecordName);
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
            }
        }
        #endregion

        #region Temporales
        private bool CanExecuteCrearTablasCommand() {
            return true;
        }

        private void ExecuteCrearTablasCommand() {
            try {
                Galac.Saw.DDL.clsCreateDb insDb = Bootstrapper.Instanse.GetDbCreator();
                if(LibMessages.MessageBox.YesNo(this,"Desea CREAR todas las tablas?","Crear Tablas")) {
                    bool vResult = insDb.CrearTablas();
                    if(vResult) {
                        LibMessages.MessageBox.Information(this,"Proceso Finalizado","Crear Tablas");
                    }
                }
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
            }
        }

        private bool CanExecuteCrearVistasYSPCommand() {
            return true;
        }

        private void ExecuteCrearVistasYSPCommand() {
            try {
                Galac.Saw.DDL.clsReestructurarDatabase insDb = new Galac.Saw.DDL.clsReestructurarDatabase();
                bool vResult = true;
                if(LibMessages.MessageBox.YesNo(this,"Borrar todas las sps y views?","Borrar?")) {
                    vResult = insDb.BorrarVistasYSps() && vResult;
                }
                if(vResult) {
                    LibMessages.MessageBox.Information(this,"Proceso Finalizado","Borrar Vistas y SP");
                } else {
                    LibMessages.MessageBox.Warning(this,"Proceso Finalizado","FALLA!!");
                }
                if(LibMessages.MessageBox.YesNo(this,"Crear todas las sps y views?","Crear?")) {
                    vResult = insDb.CrearVistasYSps() && vResult;
                }
                if(vResult) {
                    LibMessages.MessageBox.Information(this,"Proceso Finalizado","Crear Vistas y SP");
                } else {
                    LibMessages.MessageBox.Warning(this,"Proceso Finalizado","FALLA!!");
                }
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
            }
        }

        private void ExecuteTestInterop() {
            try {
                //Window frmLote = new Window();
                ////WpfControlInsertLote input = new WpfControlInsertLote();
                //frmLote.Width = 475;
                //frmLote.Height = 210;
                //frmLote.Content = input;
                //frmLote.ResizeMode = ResizeMode.NoResize;
                //frmLote.Show();
                //IWrpVb insWrp = new Galac.Saw.Wrp.TablasGen.wrpPais();
                //insWrp.Execute(eAccionSR.Consultar.GetDescription());
                //LibMefBootstrapperForInterop vBootstrapper = new LibMefBootstrapperForInterop();
                //vBootstrapper.Run(ComponentesInterop(), CurrentUserName, CompanyName, new System.Uri("/Images/Fondo WinCont.jpg", System.UriKind.Relative));

                string fileName = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "ValoresGlobalesSAW.xml");
                LibIO.DeleteFile(fileName);
                LibGlobalValues.Instance.GetAppMemInfo().Save(fileName);
                LibDiagnostics.Shell(fileName, string.Empty, false, -1);
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
            }
        }

        private XElement ComponentesInterop() {
            XElement vResult = new XElement("Components",
                new XElement("UilComponents",
                    new XElement("UilComponent",new XAttribute("Name","UIMefTablasGenPais")),
                    new XElement("UilComponent",new XAttribute("Name","UIMefUsalGUser"))));
            return vResult;
        }
        #endregion

        protected override void CallInsertMultiFileController() {
            /*
            if(LibSecurityManager.CurrentUserHasAccessToModule("Compañía")) {
                LibMessages.MessageBox.Alert(null,"El primer paso para utilizar el sistema es incluir al menos una Empresa. A continuación se iniciará el proceso de Insertar una empresa.",LibDefGen.ProgramInfo.ProgramName);
                //Galac.Contab.Uil.Empresa.ViewModel.CompaniaViewModel vViewModel = new Galac.Contab.Uil.Empresa.ViewModel.CompaniaViewModel();
                //vViewModel.InitializeViewModel(eAccionSR.Insertar);
                //ShowEditor(vViewModel, true);
            } else {
                LibMessages.MessageBox.Alert(null,"El primer paso para utilizar el sistema es incluir al menos una Empresa.",LibDefGen.ProgramInfo.ProgramName);
            }
            */
            LibMessages.MessageBox.Alert(null, "El primer paso para utilizar el sistema es incluir al menos una Empresa.", LibDefGen.ProgramInfo.ProgramName);
        }

        private string ReadDataBaseCurrentVersion() {
            string vResult = "";
            QAdvDb insDb = new QAdvDb();
            string vSql = "SELECT fldVersionBDD FROM Version WHERE fldSiglasPrograma = " + insDb.InsSql.ToSqlValue(LibDefGen.ProgramInfo.ProgramInitials);
            object vValue = insDb.ExecuteScalar(vSql, 0, false);
            if (vValue != null) {
                vResult = vValue.ToString();
            }
            return vResult;
        }

        protected override bool AppMustBeClosedForBreakingDataRequirements(out string outMessage) {
            bool vResult = false;
            outMessage = string.Empty;
            vResult = SeEstaEjecutandoRM(ref outMessage);
            return vResult;
        }

        protected override void ChooseAllMultiFileControllers() {
            base.ChooseAllMultiFileControllers();
        }

        protected override bool ReestructureDBIfNeccesary(string valProgramCurrentVersionInDb) {
            return base.ReestructureDBIfNeccesary(valProgramCurrentVersionInDb);
        }

        protected override void ClearListOfComponentsNotAllowed() {
            if (IsBsFExecutable()) {
                DeleteComponentFromList("UIMefLibBackUpRestore");
                DeleteComponentFromList("UIMefSettingsSendMailStt");
                DeleteComponentFromList("UIMefSettingsNotificationStt");
                DeleteComponentFromList("UIMefSettingsParametersLib");
            }
        }

        bool SeEstaEjecutandoRM(ref string refMessage) {
            bool vResult = false;
            /*
            refMessage = "Esta Compañía no pude ser escogida, ya que no ha sido reconvertida a bolívares soberanos";
            string vCodigoMoneda = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Compania", "CodigoMoneda");
            IMonedaLocalActual insMonedaLocalActual = new clsMonedaLocalActual();
            if (!LibString.S1IsEqualToS2(vCodigoMoneda, insMonedaLocalActual.GetHoyCodigoMoneda())) {
                vResult = true;
            }
            */
            return vResult;
        }
    }
}