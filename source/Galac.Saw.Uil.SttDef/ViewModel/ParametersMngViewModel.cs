using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Brl.Contracts;
using LibGalac.Aos.UI.Mvvm.Command;
using LibGalac.Aos.UI.Mvvm;
using LibGalac.Aos.Base;
using Galac.Saw.Ccl.SttDef;
using LibGalac.Aos.UI.Mvvm.Messaging;
using LibGalac.Aos.UI.Mvvm.Ribbon;
using Galac.Saw.Brl.SttDef;
using Galac.Comun.Ccl.TablasGen;
using System.Xml.Linq;
using LibGalac.Aos.DefGen;
using LibGalac.Ssm.U;
namespace Galac.Saw.Uil.SttDef.ViewModel {

    public class ParametersMngViewModel : LibGenericMngViewModel {

        #region Propiedades
        public override string ModuleName {
            get { return "Parametros Administrativos"; }
        }

        #region Command
        public RelayCommand UpdateCommand {
            get;
            private set;
        }

        public RelayCommand ReadCommand {
            get;
            private set;
        }

        public RelayCommand ActivarImprentaDigitalCommand {
            get;
            private set;
        }

        public RelayCommand ActualizarDatosDeConexionImprentaDigitalCommand {
            get;
            private set;
        }
        #endregion Command
        #endregion Propiedades

        #region Constructores
        protected override void InitializeCommands() {
            base.InitializeCommands();
            UpdateCommand = new RelayCommand(ExecuteUpdateCommand, CanExecuteUpdateCommand);
            ReadCommand = new RelayCommand(ExecuteReadCommand, CanExecuteReadCommand);
            if (LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "EsModoAvanzado")) {
                if (LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "UsaImprentaDigital")) {
                    ActivarImprentaDigitalCommand = new RelayCommand(ExecuteActualizarDatosDeConexionImprentaDigitalCommand, CanExecuteActualizarDatosDeConexionDigitalCommand);
                } else {
                    ActualizarDatosDeConexionImprentaDigitalCommand = new RelayCommand(ExecuteActivarImprentaDigitalCommand, CanExecuteActivarImprentaDigitalCommand);
                }
            }
        }

        protected override void InitializeRibbon() {
            base.InitializeRibbon();
            RibbonData.AddTabData(CreateModuleRibbonTab());
        }

        private LibRibbonTabData CreateModuleRibbonTab() {
            LibRibbonTabData vResult = new LibRibbonTabData(ModuleName) {
                KeyTip = "G"
            };
            vResult.GroupDataCollection.Add(CreateAccionesRibbonGroup());
            if (LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "EsModoAvanzado")) {
                vResult.GroupDataCollection.Add(CreateImprentaDigitalRibbonGroup());
            }
            return vResult;
        }
        #endregion Constructores

        #region Metodos
        #region CanExecute--Command
        private bool CanExecuteUpdateCommand() {
            bool vResult = false;
            if (LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania") > 0) {
                vResult = LibSecurityManager.CurrentUserHasAccessTo("Parámetros", "Modificar");
            }
            return vResult;
        }

        private bool CanExecuteReadCommand() {
            bool vResult = false;
            if (LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania") > 0) {
                vResult = LibSecurityManager.CurrentUserHasAccessTo("Parámetros", "Consultar");
            }
            return vResult;
        }

        private bool CanExecuteActivarImprentaDigitalCommand() {
            bool vUsaDosTalonarios = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "UsarDosTalonarios");
            bool vUsaNotaDeEntrega = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "UsaNotaEntrega");
            return !vUsaDosTalonarios && !vUsaNotaDeEntrega;
        }

        private bool CanExecuteActualizarDatosDeConexionDigitalCommand() {
            bool vUsaDosTalonarios = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "UsarDosTalonarios");
            bool vUsaNotaDeEntrega = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "UsaNotaEntrega");
            return !vUsaDosTalonarios && !vUsaNotaDeEntrega;
        }
        #endregion CanExecute--Command

        #region Execute--Command
        private void ExecuteUpdateCommand() {
            try {
                SettValueByCompany vParametros = FindCurrentRecord(LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"));
                if (vParametros != null) {
                    ParametersViewModel vViewModel = new ParametersViewModel(eAccionSR.Modificar, false);
                    vViewModel.InitializeViewModel(eAccionSR.Modificar);
                    LibMessages.EditViewModel.ShowEditor(vViewModel, true, false);
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.MessageBox.Error(this, vEx.Message, "Error");
            }
        }

        private void ExecuteReadCommand() {
            try {
                SettValueByCompany vParametros = FindCurrentRecord(LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"));
                if (vParametros != null) {
                    ParametersViewModel vViewModel = new ParametersViewModel(eAccionSR.Consultar);
                    vViewModel.InitializeViewModel(eAccionSR.Consultar);
                    LibMessages.EditViewModel.ShowEditor(vViewModel, true, false);
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
            }
        }

        private void ExecuteActivarImprentaDigitalCommand() {
            try {
                bool vPuedeEjecutar = !LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "UsaImprentaDigital");
                bool vClaveEspecialValida = vPuedeEjecutar && new LibGalac.Ssm.U.LibRequestAdvancedOperation().AuthorizeProcess("Activar Imprenta Digital", "VE");
                if (vClaveEspecialValida) {
                    ImprentaDigitalActivacionViewModel vViewModel = new ImprentaDigitalActivacionViewModel();
                    LibMessages.EditViewModel.ShowEditor(vViewModel, true);
                } else {
                    LibMessages.MessageBox.Information(this, "No se cumplen las condiciones para ejecutar la acción.", "Activar Imprenta Digital");
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
            }
        }

        private void ExecuteActualizarDatosDeConexionImprentaDigitalCommand() {
            try {
                bool vPuedeEjecutar = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "UsaImprentaDigital");
                bool vClaveEspecialValida = vPuedeEjecutar && new LibGalac.Ssm.U.LibRequestAdvancedOperation().AuthorizeProcess("Actualizar Datos de Conexión de Imprenta Digital", "VE");
                if (vClaveEspecialValida) {
                    ImprentaDigitalDatosDeConexionViewModel vViewModel = new ImprentaDigitalDatosDeConexionViewModel();
                    LibMessages.EditViewModel.ShowEditor(vViewModel, true);
                } else {
                    LibMessages.MessageBox.Information(this, "No se cumplen las condiciones para ejecutar la acción.", "Actualizar Datos de Conexión de Imprenta Digital");
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
            }
        }

        #endregion Execute--Command

        private LibRibbonGroupData CreateAccionesRibbonGroup() {
            LibRibbonGroupData vResult = new LibRibbonGroupData("Acciones");
            vResult.ControlDataCollection.Add(new LibRibbonButtonData() {
                Label = "Modificar",
                Command = UpdateCommand,
                LargeImage = new Uri("/LibGalac.Aos.UI.WpfRD;component/Images/edit.png", UriKind.Relative),
                ToolTipDescription = "Muestra al elemento actual en la lista en el editor para modificar.",
                ToolTipTitle = "Modificar",
                IsVisible = !LibDefGen.DataBaseInfo.IsReadOnlyRMDB
            });
            vResult.ControlDataCollection.Add(new LibRibbonButtonData() {
                Label = "Consultar",
                Command = ReadCommand,
                LargeImage = new Uri("/LibGalac.Aos.UI.WpfRD;component/Images/read.png", UriKind.Relative),
                ToolTipDescription = "Muestra al elemento actual en la lista en el editor para consultar.",
                ToolTipTitle = "Consultar"
            });
            return vResult;
        }

        private LibRibbonGroupData CreateImprentaDigitalRibbonGroup() {
            LibRibbonGroupData vResult = new LibRibbonGroupData("Imprenta Digital");
            if (LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "UsaImprentaDigital")) {
                vResult.ControlDataCollection.Add(new LibRibbonButtonData() {
                    Label = "Actualizar datos de Conexión",
                    Command = ActivarImprentaDigitalCommand,
                    LargeImage = new Uri("/LibGalac.Aos.UI.WpfRD;component/Images/exec.png", UriKind.Relative),
                    ToolTipDescription = "Actualizar datos de conexión con el proveedor de Imprenta Digital.",
                    ToolTipTitle = "Actualizar datos de Conexión"
                });
            } else {
                vResult.ControlDataCollection.Add(new LibRibbonButtonData() {
                    Label = "Activar Imprenta Digital",
                    Command = ActualizarDatosDeConexionImprentaDigitalCommand,
                    LargeImage = new Uri("/LibGalac.Aos.UI.WpfRD;component/Images/exec.png", UriKind.Relative),
                    ToolTipDescription = "Activar y Configurar parámetros de Imprenta Digital.",
                    ToolTipTitle = "Activar Imprenta Digital"
                });
            }
            return vResult;
        }

        private SettValueByCompany FindCurrentRecord(int valConsecutivoCompania) {
            SettValueByCompany vResult = null;
            ILibBusinessComponent<IList<SettValueByCompany>, IList<SettValueByCompany>> vBusinessComponent = new clsSettValueByCompanyNav() as ILibBusinessComponent<IList<SettValueByCompany>, IList<SettValueByCompany>>;

            try {
                LibGpParams vParams = new LibGpParams();
                vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
                XElement vElement = vBusinessComponent.QueryInfo(eProcessMessageType.Query, "SELECT * FROM Comun.Gv_SettValueByCompany_B1 WHERE ConsecutivoCompania = @ConsecutivoCompania ", vParams.Get());

                if (vElement != null && vElement.HasElements) {
                    vResult = LibParserHelper.ParseToItem<SettValueByCompany>(vElement.Elements().FirstOrDefault());
                }
                if (vResult == null) {
                    clsUtilMonedaLocal vMonedaLocal = new clsUtilMonedaLocal();

                    if (((ISettValueByCompanyPdn)vBusinessComponent).InsertaValoresPorDefecto(valConsecutivoCompania,
                                                    vMonedaLocal.InstanceMonedaLocalActual.GetHoyCodigoMoneda(),
                                                    vMonedaLocal.InstanceMonedaLocalActual.GetHoyNombreMoneda(),
                                                    "USD", ((ISettValueByCompanyPdn)vBusinessComponent).BuscaNombreMoneda("USD"),
                                                    LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Compania", "Ciudad"))) {
                        XElement vElementDefault = vBusinessComponent.QueryInfo(eProcessMessageType.Query, "SELECT * FROM Comun.Gv_SettValueByCompany_B1 WHERE ConsecutivoCompania = @ConsecutivoCompania ", vParams.Get());
                        if (vElementDefault != null && vElementDefault.HasElements) {
                            vResult = LibParserHelper.ParseToItem<SettValueByCompany>(vElementDefault.Elements().FirstOrDefault());
                        }
                    }

                    if (vResult == null) {
                        LibMessages.MessageBox.Error(this, "Error al cargar los parámetros del sistema. Comuníquese con  " + LibGalac.Aos.DefGen.LibDefGen.GalacName(), "Error al cargar Parámetros");
                    }
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
            }
            return vResult;
        }
        #endregion Metodos
    }
}