using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.UI.Mvvm;
using LibGalac.Aos.UI.Mvvm.Command;
using LibGalac.Aos.UI.Mvvm.Messaging;
using LibGalac.Aos.UI.Mvvm.Ribbon;
using LibGalac.Aos.UI.Mvvm.Helpers;
using LibGalac.Aos.Brl.Contracts;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Report;
using LibGalac.Aos.Catching;
using LibGalac.Aos.ARRpt.Reports;
using Galac.Adm.Brl.CajaChica;
using Galac.Adm.Brl.CajaChica.Reportes;
using Galac.Adm.Ccl.CajaChica;

namespace Galac.Adm.Uil.CajaChica.ViewModel {

   public class RendicionMngViewModel : LibMngMasterViewModelMfc<RendicionViewModel, Rendicion> {
      #region Propiedades

      public override string ModuleName {
         get { return "Reposición de Caja Chica"; }
      }

      public string ModuleNameEnSeguridad {
         get { return "Reposicion de Caja Chica"; }
      }
        public RelayCommand InformesCommand {
            get;
            private set;
        }
      #region Codigo Ejemplo
      /* Codigo de Ejemplo

        public LibXmlMemInfo AppMemoryInfo { get; set; }
        */
      #endregion //Codigo Ejemplo
      #endregion //Propiedades
      #region Constructores

      public RendicionMngViewModel()
         : base(LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
         Title = "Buscar " + ModuleName;
         OrderByMember = "Consecutivo";
         OrderByDirection = "DESC";
         #region Codigo Ejemplo
         /* Codigo de Ejemplo
            OrderByDirection = "DESC";
            AppMemoryInfo = LibGlobalValues.Instance.GetAppMemInfo();
        */
         #endregion //Codigo Ejemplo
      }
      #endregion //Constructores
      #region Metodos Generados

      protected override RendicionViewModel CreateNewElement(Rendicion valModel, eAccionSR valAction) {
         var vNewModel = valModel;
         if (vNewModel == null) {
            vNewModel = new Rendicion();
         }
         return new RendicionViewModel(vNewModel, valAction);
      }

      protected override LibSearchCriteria GetMFCCriteria() {
         return LibSearchCriteria.CreateCriteria("Gv_Rendicion_B1.ConsecutivoCompania", Mfc.GetInt("Compania"));
      }

      protected override ILibBusinessMasterComponentWithSearch<IList<Rendicion>, IList<Rendicion>> GetBusinessComponent() {
         return new clsRendicionNav();
      }

      protected override ILibReportInfo GetDataRetrievesInstance() {
         return new clsRendicionRpt();
      }

      protected override ILibRpt GetReportForList(string valModuleName, ILibReportInfo valReportInfo, LibCollFieldFormatForGrid valFieldsFormat, LibGpParams valParams) {
         return new LibGenericList(valModuleName, valReportInfo, valFieldsFormat, valParams);
      }

      protected override void InitializeCommands() {
         base.InitializeCommands();
            InformesCommand = new RelayCommand(ExecuteInformesCommand, CanExecuteInformesCommand);
         #region Codigo Ejemplo
         /* Codigo de Ejemplo
            SUPROCESOPARTICULARCommand = new RelayCommand(ExecuteSUPROCESOPARTICULARCommand, CanExecuteSUPROCESOPARTICULARCommand);
        */
         #endregion //Codigo Ejemplo
         CerrarRendicionCommand = new RelayCommand(ExecuteCerrarRendicionCommand, CanExecuteCerrarRendicionCommand);
         AnularRendicionCommand = new RelayCommand(ExecuteAnularRendicionCommand, CanExecuteAnularRendicionCommand);         
         ReimprimirComprobanteRendicionCommand = new RelayCommand(ExecuteReimprimirComprobanteRendicionCommand, CanExecuteReimprimirComprobanteRendicionCommand);
      }

      protected override void InitializeRibbon() {
         base.InitializeRibbon();
         if (RibbonData.TabDataCollection != null && RibbonData.TabDataCollection.Count > 0) {
            RibbonData.TabDataCollection[0].AddTabGroupData(CreateInformesRibbonGroup());
            //RibbonData.GetRibbonGroupData("Administrar").AddRibbonControlData(CreateCerrarRendicionRibbon());
            //RibbonData.GetRibbonGroupData("Administrar").AddRibbonControlData(CreateAnularRendicionRibbon());            
            //RibbonData.GetRibbonGroupData("Consultas").AddRibbonControlData(CreateReimprimirComprobanteRendicionRibbon());          

            #region Codigo Ejemplo
            /* Codigo de Ejemplo
                RibbonData.TabDataCollection[0].AddTabGroupData(CreateSUPROCESOPARTICULARRibbonGroup());
        */
            #endregion //Codigo Ejemplo
         }
      }
      
        private LibRibbonGroupData CreateInformesRibbonGroup() {
            LibRibbonGroupData vResult = new LibRibbonGroupData("");
            vResult.ControlDataCollection.Add(new LibRibbonButtonData() {
                Label = "Informes",
                Command = InformesCommand,
                LargeImage = new Uri("/LibGalac.Aos.UI.WpfRD;component/Images/report.png", UriKind.Relative),
                ToolTipDescription = "Informes",
                ToolTipTitle = "Informes"
            });
            return vResult;
        }

        private void ExecuteInformesCommand() {
            try {                
                if (LibMessages.ReportsView.ShowReportsView(new Galac.Adm.Uil.CajaChica.Reportes.clsRendicionInformesViewModel (LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()))) {
                    DialogResult = true;
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
            }
        }

        private bool CanExecuteInformesCommand() {            
            return LibSecurityManager.CurrentUserHasAccessTo(ModuleNameEnSeguridad, "Informes");
        }
      #endregion //Metodos Generados
      #region Codigo Ejemplo
      /* Codigo de Ejemplo
        //para cambiar el mecanismo de activacion de los botones de operaciones CRUD, debes sobreescribirla y ajustarla las necesidades de tu negocio:

        protected override bool CanExecuteCreateCommand() {
            return CanCreate;
        }

        protected override bool CanExecuteUpdateCommand() {
            return CanUpdate && CurrentItem != null;
        }

        protected override bool CanExecuteDeleteCommand() {
            return CanDelete && CurrentItem != null;
        }

        protected override bool CanExecuteReadCommand() {
            return CanRead && CurrentItem != null;
        }
        //para agregar una nueva accion en el Ribbon, debes agregar este conjunto de métodos (6 en total) y modificar las inicializaciones.
        //Por favor recuerda autodocumentar, el codigo es de ejemplo para que te sirva de guía, no código final:

        protected override void ExecuteCommandsRaiseCanExecuteChanged() {
            base.ExecuteCommandsRaiseCanExecuteChanged();
            SUPROCESOPARTICULARCommand.RaiseCanExecuteChanged();
        }

        ANYRELATEDViewModel CreateNewElementForSUPROCESOPARTICULAR(Rendicion valModel, eAccionSR valAction) {
            var vNewModel = valModel;
            return new ANYRELATEDViewModel(vNewModel, valAction);
        }

        public RelayCommand SUPROCESOPARTICULARCommand {
            get;
            private set;
        }

        private LibRibbonGroupData CreateSUPROCESOPARTICULARRibbonGroup() {
            LibRibbonGroupData vResult = new LibRibbonGroupData("SU PROCESO PARTICULAR");
            vResult.ControlDataCollection.Add(new LibRibbonButtonData() {
                Label = "LO QUE SE LEE EN EL RIBBON",
                Command = SUPROCESOPARTICULARCommand,
                LargeImage = new Uri("/LibGalac.Aos.UI.WpfRD;component/Images/edit.png", UriKind.Relative),
                ToolTipDescription = "LO QUE SE LEE AL PASAR EL MOUSE SOBRE EL NUEVO BOTON.",
                ToolTipTitle = "TITULO PARA EL TOOLTIP"
            });
            return vResult;
        }

        private bool CanExecuteSUPROCESOPARTICULARCommand() {
            return CurrentItem != null
                && LibSecurityManager.CurrentUserHasAccessTo(ModuleName, "Su nivel de permiso asociado");
        }

        private void ExecuteSUPROCESOPARTICULARCommand() {
            try {
                ANYRELATEDViewModel vViewModel = CreateNewElementForSUPROCESOPARTICULAR(CurrentItem.GetModel(), eAccionSR.Cerrar);
                vViewModel.InitializeViewModel(eAccionSR.SUACCION);
                bool result = LibMessages.EditViewModel.ShowEditor(vViewModel);
                if (result) {
                    SearchItems();
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
            }
        }
        */
      #endregion //Codigo Ejemplo

      protected override bool CanExecuteCreateCommand() {
         return LibSecurityManager.CurrentUserHasAccessTo(ModuleNameEnSeguridad, "Insertar");
      }
	  
      protected override bool CanExecuteUpdateCommand() {
         return LibSecurityManager.CurrentUserHasAccessTo(ModuleNameEnSeguridad, "Modificar") && CurrentItem != null && CurrentItem.StatusRendicion.Equals(eStatusRendicion.EnProceso);
      }

      protected override bool CanExecuteDeleteCommand() {
         return LibSecurityManager.CurrentUserHasAccessTo(ModuleNameEnSeguridad, "Eliminar") && CurrentItem != null && CurrentItem.StatusRendicion.Equals(eStatusRendicion.EnProceso);
      }

      protected override bool CanExecuteReadCommand() {
         return LibSecurityManager.CurrentUserHasAccessTo(ModuleNameEnSeguridad, "Consultar") && CurrentItem != null;
      }

      protected override bool HasAccessToModule() {
         return LibSecurityManager.CurrentUserHasAccessToModule(ModuleNameEnSeguridad);
      }

      protected override void ExecuteCommandsRaiseCanExecuteChanged() {
         base.ExecuteCommandsRaiseCanExecuteChanged();
         InformesCommand.RaiseCanExecuteChanged();
         CerrarRendicionCommand.RaiseCanExecuteChanged();
         AnularRendicionCommand.RaiseCanExecuteChanged();
         ReimprimirComprobanteRendicionCommand.RaiseCanExecuteChanged();
      }

      // CIERRE -----------------------------

      private LibRibbonButtonData CreateCerrarRendicionRibbon() {
         return new LibRibbonButtonData() {
            Label = "Cerrar",
            Command = CerrarRendicionCommand,
            LargeImage = new Uri("/LibGalac.Aos.UI.WpfRD;component/Images/edit.png", UriKind.Relative),
            ToolTipDescription = "Muestra el elemento actual en la lista en el editor para cerrar.",
            ToolTipTitle = "Cerrar"
         };
      }

      public RelayCommand CerrarRendicionCommand {
         get;
         private set;
      }     

      private bool CanExecuteCerrarRendicionCommand() {
         return CurrentItem != null && LibSecurityManager.CurrentUserHasAccessTo(ModuleNameEnSeguridad, "Cierre") && CurrentItem.StatusRendicion.Equals(eStatusRendicion.EnProceso);
      }

      private void ExecuteCerrarRendicionCommand() {
         try {
            RendicionViewModel vViewModel = CreateNewElementForCerrarRendicion(CurrentItem.GetModel(), eAccionSR.Cerrar);          
            vViewModel.InitializeViewModel(eAccionSR.Cerrar);
            bool result = LibMessages.EditViewModel.ShowEditor(vViewModel);
            if (result) {
               SearchItems();
            }
         } catch (System.AccessViolationException) {
            throw;
         } catch (System.Exception vEx) {
            LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
         }
      }

      RendicionViewModel CreateNewElementForCerrarRendicion(Rendicion valModel, eAccionSR valAction) {
         var vNewModel = valModel;
         return new RendicionViewModel(vNewModel, valAction);
      }

      // ANULACION -------------------------

      private LibRibbonButtonData CreateAnularRendicionRibbon() {         
        return new LibRibbonButtonData() {
            Label = "Anular",
            Command = AnularRendicionCommand,
            LargeImage = new Uri("/LibGalac.Aos.UI.WpfRD;component/Images/edit.png", UriKind.Relative),
            ToolTipDescription = "Muestra el elemento actual en la lista en el editor para anular.",
            ToolTipTitle = "Anular"
         };
      }

      public RelayCommand AnularRendicionCommand {
         get;
         private set;
      }
      
      private bool CanExecuteAnularRendicionCommand() {
         return CurrentItem != null && LibSecurityManager.CurrentUserHasAccessTo(ModuleNameEnSeguridad, "Anular") && CurrentItem.StatusRendicion.Equals(eStatusRendicion.Cerrada);
      }

      private void ExecuteAnularRendicionCommand() {
         try {
            RendicionViewModel vViewModel = CreateNewElementForAnularRendicion(CurrentItem.GetModel(), eAccionSR.Anular);
            vViewModel.InitializeViewModel(eAccionSR.Anular);
            bool result = LibMessages.EditViewModel.ShowEditor(vViewModel);
            if (result) {
               SearchItems();
            }
         } catch (System.AccessViolationException) {
            throw;
         } catch (System.Exception vEx) {
            LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
         }
      }

      RendicionViewModel CreateNewElementForAnularRendicion(Rendicion valModel, eAccionSR valAction) {
         var vNewModel = valModel;
         return new RendicionViewModel(vNewModel, valAction);
      }

      // REIMPRIMIR COMPROBANTE -------------------------

      private LibRibbonButtonData CreateReimprimirComprobanteRendicionRibbon() {
        return new LibRibbonButtonData() {
            Label = "Reimprimir Comprobante",
            Command = ReimprimirComprobanteRendicionCommand,
            LargeImage = new Uri("/LibGalac.Aos.UI.WpfRD;component/Images/printList.png", UriKind.Relative),
            ToolTipDescription = "Muestra el elemento actual en la lista en el editor para reimprimir comprobante.",
            ToolTipTitle = "Reimprimir Comprobante"
         };
      }

      public RelayCommand ReimprimirComprobanteRendicionCommand {
         get;
         private set;
      }

      private bool CanExecuteReimprimirComprobanteRendicionCommand() {
         return CurrentItem != null && LibSecurityManager.CurrentUserHasAccessTo(ModuleNameEnSeguridad, "Reimprimir Comprobante") && CurrentItem.StatusRendicion.Equals(eStatusRendicion.Cerrada);
      }

      private void ExecuteReimprimirComprobanteRendicionCommand() {
         try {
            RendicionViewModel vViewModel = CreateNewElementForReimprimirComprobanteRendicion(CurrentItem.GetModel(), eAccionSR.ReImprimir);
            vViewModel.InitializeViewModel(eAccionSR.ReImprimir);
            bool result = LibMessages.EditViewModel.ShowEditor(vViewModel);
            if (result) {
               SearchItems();
            }
         } catch (System.AccessViolationException) {
            throw;
         } catch (System.Exception vEx) {
            LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
         }
      }

      RendicionViewModel CreateNewElementForReimprimirComprobanteRendicion(Rendicion valModel, eAccionSR valAction) {
         var vNewModel = valModel;
         return new RendicionViewModel(vNewModel, valAction);
      }

   } //End of class RendicionMngViewModel

} //End of namespace Galac.Adm.Uil.CajaChica

