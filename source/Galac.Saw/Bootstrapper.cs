using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Text;
using System.Windows;
using Galac.Adm.Uil.GestionProduccion;
using Galac.Adm.Uil.Venta;
using Galac.Saw.Ccl.Inventario;
using Galac.Saw.DDL;
using Galac.Saw.Properties;
using Galac.Saw.Uil.Inventario;
using Galac.Saw.Uil.Inventario.ViewModel;
using Galac.Saw.Views;
using LibGalac.Aos.Base;
using LibGalac.Aos.Brl;
using LibGalac.Aos.Cnf;
using LibGalac.Aos.DefGen;
using LibGalac.Aos.UI.Composition;
using LibGalac.Aos.UI.Mvvm.Messaging;
using LibGalac.Aos.UI.Wpf;
using LibGalac.Aos.Uil.Usal;

namespace Galac.Saw {
    internal class Bootstrapper : LibMefBootstrapper {
        Galac.Saw.DDL.clsCreateDb _DbCreator;
        Galac.Saw.DDL.clsDbDefaultValues _DbDefaultValues;

        public static Bootstrapper Instanse {
            get;
            private set;
        }

        public Bootstrapper()
            : base() {
            AddPathOfCatalogsForMefFromIDE(@"bin\", "*.dll");
        }

        protected override void ConfigureContainer() {
            base.ConfigureContainer();
            this.Container.ComposeExportedValue<Galac.Saw.DDL.clsCreateDb>(_DbCreator);
            this.Container.ComposeExportedValue<Galac.Saw.DDL.clsDbDefaultValues>(_DbDefaultValues);
            //var a = base.AggregateCatalog;
        }

        protected override DependencyObject CreateShell() {
            return this.Container.GetExportedValue<ShellWindow>();
        }

        protected override void InitializeShell() {
            Application.Current.MainWindow = ((Window)this.Shell);
            Application.Current.MainWindow.Show();
        }

        protected override void RegisterDefaultTypesIfMissing() {
            base.RegisterDefaultTypesIfMissing();
            LibGalac.Aos.Uil.LibMessagesHandler.RegisterMessages();
            LibGalac.Aos.Uil.Settings.LibSettingsMessagesHandler.RegisterMessages();
            LibGalac.Aos.Uil.PASOnLine.LibPASOnLineHandler.RegisterMessages();
            InventarioMessagesHandler.RegisterMessages();
            OrdenDeProduccionMessagesHandler.RegisterMessages();
            VentaMessagesHandler.RegisterMessages();

        }

        protected override bool CreateTablesIfNecesary() {
            bool vResult = false;
            LibSessionParameters.PlatformArchitecture = 0;
            if (_DbCreator.MustCreateTables()) {
                LibSecurityManager.SetDbCreatorPrincipal(LibSecurityLevels.Levels);
                LibMessages.BackgroundWorker.ReportProgress("Creando tablas....", true);
                this.Container.ComposeParts(_DbCreator);
                vResult = _DbCreator.CrearTablas();
                if (vResult) {
                    vResult = vResult && InstallDefaultValues();
                    if (!vResult) {
                        throw new LibGalac.Aos.Catching.GalacException("No se logró instalar todos los valores por defecto en el sistema. La base de datos presenta inconsistencia de registros", LibGalac.Aos.Catching.eExceptionManagementType.Alert);
                    }
                } else {
                    throw new LibGalac.Aos.Catching.GalacException("No se pudieron instalar todas las tablas del sistema, en consecuencia los valores iniciales no pueden cargarse. La base de datos está inestable", LibGalac.Aos.Catching.eExceptionManagementType.Alert);
                }
                LibSecurityManager.SetSecurityPrincipal();
            }
            return vResult;
        }

        protected override void InitCollectionOfUserSecurityLevels() {
            clsNivelesDeSeguridad.DefinirPlantilla();
            LibSecurityManager.SetSecurityPrincipal();
            _DbCreator = new clsCreateDb(LibSecurityLevels.Levels, LoadProductAdmittedComponents(Resources.Components, "DalComponents"));
            _DbDefaultValues = new clsDbDefaultValues(LibSecurityLevels.Levels, LoadProductAdmittedComponents(Resources.Components, "BrlComponents"));
        }

        protected override void InitDefProgDefinitions() {
            LibGalac.Aos.DefGen.LibDefGen.InitializeProgramInfo(
                clsDefProg.SiglasDelPrograma,
                clsDefProg.VersionDelPrograma,
                clsDefProg.VersionBaseDeDatos,
                clsDefProg.FechaDelaVersion,
                clsDefProg.HoraDeLaVersion, "",
                clsDefProg.Pais(),
                clsDefProg.CMTO());
            LibDefGen.ShowAlertReconversionPhaseOne = false;
            LibDefGen.ShowAlertReconversionPhaseTwo = false;
        }

        protected override void InitPrinterSettings() {
            LibGalac.Aos.Uil.PrnStt.LibInitPrnSttLibrary.Initialize();
        }

        protected override void LoadGlobalValues() {
        }

        protected override bool InstallDefaultValues() {
            bool vResult = false;
            LibMessages.BackgroundWorker.ReportProgress("Instalando valores iniciales....", true);
            this.Container.ComposeParts(_DbDefaultValues);
            if (_DbDefaultValues.InsertDefaultRecords()) {
                vResult = true;
            }
            return vResult;
        }

        protected override bool ReadConfigAndConnectDataBase() {
            bool vResult = true;
            bool vReplaceCS = false;
            bool vEncryptCS = !LibApp.IsRunningFromIde();
            LibConfigDataAccess insConnfig = new LibConfigDataAccess();
            bool vAgregaSettingsAdicionales = !LibApp.IsRunningFromIde() && !LibIO.FileExists(LibApp.AppAssemblyName() + "App.settings");
            vResult = vResult && insConnfig.ReadConfigAndConnectDataBase(LibAppConfig.DefaultConfigKeyForDbServiceName, vEncryptCS, vReplaceCS, LibDefGen.ProgramInfo.ProgramInitials);
            return vResult;
        }

        public override void Run() {
            Instanse = this;
            base.Run();
        }

        internal clsCreateDb GetDbCreator() {
            if (_DbCreator == null) {
                _DbCreator = new clsCreateDb(LibSecurityLevels.Levels, LoadProductAdmittedComponents(Resources.Components, "DalComponents"));
            }
            this.Container.ComposeParts(_DbCreator);
            return _DbCreator;
        }

        public override void SetExternalConfig() {
            LibAppConfig.UseExternalConfig = true;
            LibAppConfig.SetExternalConfig(LibDefGen.DataPath(clsDefProg.SiglasDelPrograma));
        }

        protected override void InitializeBackupSettingsForAutomaricBackup() {
            clsBackupSettings vBackupSettings = new clsBackupSettings();
            vBackupSettings.Initialize();
            LibGlobalValues.Instance.BackupSettings = vBackupSettings;
        }

        protected override void SetProgramType() {
            string vProgramType = string.Empty;//ejemplo:
            if (LibDefGen.HasAccessTo(144)) {
                vProgramType = "Pyme";
            } else if (LibDefGen.HasAccessTo(102)) {
                vProgramType = "Emprendedor";
            } else if (LibDefGen.HasAccessTo(148)) {
                vProgramType = "G-Factura";
            }
            if (LibDefGen.HasAccessTo(101)) {
                vProgramType += " Integrado con Contabilidad";
            }
            LibDefGen.ProgramInfo.ProgramType = vProgramType;
        }
		
		protected override void AllowUseOfPASOnLineByProgram() {
            LibDefGen.UsePASOnLine = true;
        }

        protected override bool IsAllowedToConnectWithDB() {
            bool vResult = base.IsAllowedToConnectWithDB();
            if (vResult) {
                object vValue = new LibGalac.Aos.Dal.QAdvDb("").ExecuteScalar("SELECT fldVersionPrograma FROM Version WHERE fldSiglasPrograma = " + new LibGalac.Aos.Base.Dal.QAdvSql("").ToSqlValue(clsDefProg.SiglasDelPrograma), 0, false);
                string vProgramCurrentVersionInDb = (vValue != null ? vValue.ToString() : string.Empty);
                if (LibVersionApp.VersionAIsGreaterThanVersionB(vProgramCurrentVersionInDb, clsDefProg.VersionDelPrograma)) {
                    LibMessages.MessageBox.Error(this, "Usted está ejecutando la versión " + clsDefProg.VersionDelPrograma + " del programa. Debe actualizarlo a la versión " + vProgramCurrentVersionInDb, clsDefProg.SiglasDelPrograma + " - Versión desactualizada");
                    return false;
                }
                AgregaRegistrosDeLasTablasDeVersionDelSchemaLib();
            }
            return vResult;
        }

        void AgregaRegistrosDeLasTablasDeVersionDelSchemaLib() {
            new LibGalac.Aos.Dal.QAdvDb("").Execute("DELETE FROM Lib.Version", 0, false);
            new LibGalac.Aos.Dal.QAdvDb("").Execute("DELETE FROM Lib.VersionLib", 0, false);
            //codigo provisional porque estas tablas no se estan llenando en productos hibridos.
            StringBuilder vSqlSb = new StringBuilder();
            vSqlSb.AppendLine("     INSERT INTO [Lib].[Version]");
            vSqlSb.AppendLine("           ([InitialsOfProgram]");
            vSqlSb.AppendLine("           ,[DataBaseCurrentVersion]");
            vSqlSb.AppendLine("           ,[ProgramCurrentVersion]");
            vSqlSb.AppendLine("           ,[ProgramOldVersion])");
            vSqlSb.AppendLine("     SELECT fldSiglasPrograma");
            vSqlSb.AppendLine("	 , fldVersionBDD");
            vSqlSb.AppendLine("	 , fldVersionPrograma");
            vSqlSb.AppendLine("	 , fldVersionPrograma");
            vSqlSb.AppendLine("	 FROM dbo.[Version]");
            vSqlSb.AppendLine("	 GO");
            
            vSqlSb.AppendLine("	INSERT INTO [Lib].[VersionLib]");
            vSqlSb.AppendLine("           ([InitialsOfProgram]");
            vSqlSb.AppendLine("           ,[CurrentVersion]");
            vSqlSb.AppendLine("           ,[OldVersion])");
            vSqlSb.AppendLine(" SELECT");
            vSqlSb.AppendLine("	InitialsOfProgram");
            vSqlSb.AppendLine("	, CurrentVersion");
            vSqlSb.AppendLine("	, OldVersion");
            vSqlSb.AppendLine("	FROM dbo.versionLib");
            vSqlSb.AppendLine("	GO");
            new LibGalac.Aos.Dal.QAdvDb("").Execute(vSqlSb.ToString(), 0, false);
        
        }
    }
}