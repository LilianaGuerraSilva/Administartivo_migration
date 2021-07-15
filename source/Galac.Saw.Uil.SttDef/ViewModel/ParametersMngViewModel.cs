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
namespace Galac.Saw.Uil.SttDef.ViewModel {

    public class ParametersMngViewModel : LibGenericMngViewModel {

        #region "Propiedades"
        public override string ModuleName {
            get { return "Parametros Administrativos"; }
        }
       
        public RelayCommand UpdateCommand {
            get;
            private set;
        }

        public RelayCommand ReadCommand {
            get;
            private set;
        }
        #endregion

        #region "Constructores"
        protected override void InitializeCommands() {
            base.InitializeCommands();
            UpdateCommand = new RelayCommand(ExecuteUpdateCommand, CanExecuteUpdateCommand);
            ReadCommand = new RelayCommand(ExecuteReadCommand, CanExecuteReadCommand);  
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
            return vResult;
        }
        #endregion

        #region "Metodos"
        private bool CanExecuteUpdateCommand() {
            bool vResult = false;
            if (LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania") > 0) {
                vResult = LibSecurityManager.CurrentUserHasAccessTo("Parámetros", "Modificar");
            }
            return vResult;
        }

        private void ExecuteUpdateCommand() {
            try {
                SettValueByCompany vParametros = FindCurrentRecord(LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"));
                if(vParametros != null) {
                    ParametersViewModel vViewModel = new ParametersViewModel(eAccionSR.Modificar, false);
                    vViewModel.InitializeViewModel(eAccionSR.Modificar);
                    LibMessages.EditViewModel.ShowEditor(vViewModel, true, false);
                }
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.MessageBox.Error(this, vEx.Message, "Error");
            }
        } 

        private bool CanExecuteReadCommand() {
            bool vResult = false;
            if (LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania") > 0) {
                vResult =  LibSecurityManager.CurrentUserHasAccessTo("Parámetros", "Consultar");
            }
             return vResult;
        }

        private void ExecuteReadCommand() {
            try {
                SettValueByCompany vParametros = FindCurrentRecord(LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"));
                if(vParametros != null) {
                    ParametersViewModel vViewModel = new ParametersViewModel(eAccionSR.Consultar);
                    vViewModel.InitializeViewModel(eAccionSR.Consultar);
                    LibMessages.EditViewModel.ShowEditor(vViewModel, true, false);
                } 
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
            }
        }


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
        
        private SettValueByCompany FindCurrentRecord(int valConsecutivoCompania) {
            SettValueByCompany vResult = null;
            ILibBusinessComponent<IList<SettValueByCompany>, IList<SettValueByCompany>> vBusinessComponent = new clsSettValueByCompanyNav() as ILibBusinessComponent<IList<SettValueByCompany>, IList<SettValueByCompany>>;

            try {
                LibGpParams vParams = new LibGpParams();
                vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
                XElement vElement = vBusinessComponent.QueryInfo(eProcessMessageType.Query, "SELECT * FROM Comun.Gv_SettValueByCompany_B1 WHERE ConsecutivoCompania = @ConsecutivoCompania ", vParams.Get());

                if(vElement != null && vElement.HasElements) {
                    vResult = LibParserHelper.ParseToItem<SettValueByCompany>(vElement.Elements().FirstOrDefault());
                }
                
                if(vResult == null) {
                    clsUtilMonedaLocal vMonedaLocal = new clsUtilMonedaLocal();

                    if(((ISettValueByCompanyPdn)vBusinessComponent).InsertaValoresPorDefecto(valConsecutivoCompania,
                                                    vMonedaLocal.InstanceMonedaLocalActual.GetHoyCodigoMoneda(),
                                                    vMonedaLocal.InstanceMonedaLocalActual.GetHoyNombreMoneda(),
                                                    "USD", ((ISettValueByCompanyPdn)vBusinessComponent).BuscaNombreMoneda("USD"),
                                                    LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Compania", "Ciudad"))) {
                        XElement vElementDefault = vBusinessComponent.QueryInfo(eProcessMessageType.Query, "SELECT * FROM Comun.Gv_SettValueByCompany_B1 WHERE ConsecutivoCompania = @ConsecutivoCompania ", vParams.Get());
                        if(vElementDefault != null && vElementDefault.HasElements) {
                            vResult = LibParserHelper.ParseToItem<SettValueByCompany>(vElementDefault.Elements().FirstOrDefault());
                        }
                    }

                    if(vResult == null) {
                        LibMessages.MessageBox.Error(this, "Error al cargar los parámetros del sistema. Comuníquese con  " + LibGalac.Aos.DefGen.LibDefGen.GalacName(), "Error al cargar Parámetros");
                    }
                }
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
            }
            return vResult;
        }

        
        #endregion


    }
}