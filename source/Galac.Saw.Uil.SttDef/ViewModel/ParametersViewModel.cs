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
using Galac.Saw.Ccl.SttDef;
using System.Collections.ObjectModel;
using Galac.Saw.Brl.SttDef;

namespace Galac.Saw.Uil.SttDef.ViewModel {
    public class ParametersViewModel: LibGenericViewModel {
        public const string ModuleListPropertyName = "ModuleList";
        private List<Module> _ModuleList;

        public bool InitFirstTime { get; set; }

        public override string ModuleName {
            get { return "Parámetros Administrativos"; }
        }

        public List<Module> ModuleList {
            get {
                return _ModuleList;
            }
            set {
                if (_ModuleList != value) {
                    _ModuleList = value;
                    RaisePropertyChanged(ModuleListPropertyName);
                }
            }
        }

        public RelayCommand UpdateCommand {
            get;
            private set;
        }

        public bool StopClosing { get; set; }

        public bool IsDirty { get; set; }

        public eAccionSR Action { get; set; }

        #region Constructores
        public ParametersViewModel(eAccionSR initAccionSR) {
            Action = initAccionSR;
            var vResult = GetModuleList(LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"));
            List<Module> vListResult = new List<Module>();
            foreach (var vModule in vResult) {
                var vGroups = new GroupCollection();
                foreach (var vGroup in vModule.Groups) {
                    var vNewGroup = new Group(vGroup.DisplayName, ParseGroupContentToViewModel(vGroup.Content, initAccionSR));
                    vGroups.Add(vNewGroup);
                }
                var vNewModule = new Module(vModule.DisplayName, vGroups);
                vListResult.Add(vNewModule);
            }
            ModuleList = vListResult;
        }

        public ParametersViewModel(eAccionSR initAccionSR, bool firstTime) {
            InitFirstTime = firstTime;
            Action = initAccionSR;
            var vResult = GetModuleList(LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"));
            List<Module> vListResult = new List<Module>();
            foreach (var vModule in vResult) {
                var vGroups = new GroupCollection();
                foreach (var vGroup in vModule.Groups) {
                    var vNewGroup = new Group(vGroup.DisplayName, ParseGroupContentToViewModel(vGroup.Content, initAccionSR));
                    vGroups.Add(vNewGroup);
                }
                var vNewModule = new Module(vModule.DisplayName, vGroups);
                vListResult.Add(vNewModule);
            }
            ModuleList = vListResult;
        }

        #endregion //Constructores

        #region "Metodos"
        private object ParseGroupContentToViewModel(object valContent, eAccionSR eAccionSR) {
            object vResult = null;
            if (valContent != null) {

                if (valContent is AnticipoStt) {
                    LibInputViewModelBase<AnticipoStt> vModel = new BancosAnticipoViewModel(valContent as AnticipoStt, eAccionSR);
                    vModel.InitializeViewModel(eAccionSR);
                    vResult = vModel;
                } else if (valContent is BancosStt) {
                    LibInputViewModelBase<BancosStt> vModel = new BancosViewModel(valContent as BancosStt, eAccionSR);
                    vModel.InitializeViewModel(eAccionSR);
                    vResult = vModel;
                } else if (valContent is CamposDefiniblesStt) {
                    LibInputViewModelBase<CamposDefiniblesStt> vModel = new FacturaCamposDefiniblesViewModel(valContent as CamposDefiniblesStt, eAccionSR);
                    vModel.InitializeViewModel(eAccionSR);
                    vResult = vModel;
                } else if (valContent is ClienteStt) {
                    LibInputViewModelBase<ClienteStt> vModel = new CXCCobranzasClienteViewModel(valContent as ClienteStt, eAccionSR);
                    vModel.InitializeViewModel(eAccionSR);
                    vResult = vModel;
                } else if (valContent is CobranzasStt) {
                    LibInputViewModelBase<CobranzasStt> vModel = new CXCCobranzasViewModel(valContent as CobranzasStt, eAccionSR);
                    vModel.InitializeViewModel(eAccionSR);
                    vResult = vModel;
                } else if (valContent is ComisionesStt) {
                    LibInputViewModelBase<ComisionesStt> vModel = new CXCCobranzasComisionesViewModel(valContent as ComisionesStt, eAccionSR);
                    vModel.InitializeViewModel(eAccionSR);
                    vResult = vModel;
                } else if (valContent is CompaniaStt) {
                    LibInputViewModelBase<CompaniaStt> vModel = new DatosGeneralesCompaniaViewModel(valContent as CompaniaStt, eAccionSR);
                    vModel.InitializeViewModel(eAccionSR);
                    vResult = vModel;
                } else if (valContent is ComprasStt) {
                    LibInputViewModelBase<ComprasStt> vModel = new CxPComprasViewModel(valContent as ComprasStt, eAccionSR);
                    vModel.InitializeViewModel(eAccionSR);
                    vResult = vModel;
                } else if (valContent is CotizacionStt) {
                    LibInputViewModelBase<CotizacionStt> vModel = new CotizacionViewModel(valContent as CotizacionStt, eAccionSR);
                    vModel.InitializeViewModel(eAccionSR);
                    vResult = vModel;
                } else if (valContent is CxPProveedorPagosStt) {
                    LibInputViewModelBase<CxPProveedorPagosStt> vModel = new CxPComprasCxCProveedorViewModel(valContent as CxPProveedorPagosStt, eAccionSR);
                    vModel.InitializeViewModel(eAccionSR);
                    vResult = vModel;
                } else if (valContent is FacturacionContinuacionStt) {
                    LibInputViewModelBase<FacturacionContinuacionStt> vModel = new FacturaFacturacionContViewModel(valContent as FacturacionContinuacionStt, eAccionSR);
                    vModel.InitializeViewModel(eAccionSR);
                    vResult = vModel;
                } else if (valContent is FacturaCobroFacturaStt) {
                    LibInputViewModelBase<FacturaCobroFacturaStt> vModel = new FacturaCobroFacturaViewModel(valContent as FacturaCobroFacturaStt, eAccionSR);
                    vModel.InitializeViewModel(eAccionSR);
                    vResult = vModel;
                } else if (valContent is FacturacionStt) {
                    LibInputViewModelBase<FacturacionStt> vModel = new FacturaFacturacionViewModel(valContent as FacturacionStt, eAccionSR, InitFirstTime);
                    vModel.InitializeViewModel(eAccionSR);
                    vResult = vModel;
                } else if (valContent is GeneralStt) {
                    LibInputViewModelBase<GeneralStt> vModel = new DatosGeneralesViewModel(valContent as GeneralStt, eAccionSR);
                    vModel.InitializeViewModel(eAccionSR);
                    vResult = vModel;
                } else if (valContent is ImpresiondeFacturaStt) {
                    LibInputViewModelBase<ImpresiondeFacturaStt> vModel = new FacturaImpresionViewModel(valContent as ImpresiondeFacturaStt, eAccionSR, InitFirstTime);
                    vModel.InitializeViewModel(eAccionSR);
                    vResult = vModel;
                } else if (valContent is InventarioStt) {
                    LibInputViewModelBase<InventarioStt> vModel = new InventarioViewModel(valContent as InventarioStt, eAccionSR);
                    vModel.InitializeViewModel(eAccionSR);
                    vResult = vModel;
                } else if (valContent is MetododecostosStt) {
                    LibInputViewModelBase<MetododecostosStt> vModel = new InventarioMetodoCostoViewModel(valContent as MetododecostosStt, eAccionSR);
                    vModel.InitializeViewModel(eAccionSR);
                    vResult = vModel;
                } else if (valContent is ModeloDeFacturaStt) {
                    LibInputViewModelBase<ModeloDeFacturaStt> vModel = new FacturaModeloFacturaViewModel(valContent as ModeloDeFacturaStt, eAccionSR);
                    vModel.InitializeViewModel(eAccionSR);
                    vResult = vModel;
                } else if (valContent is MonedaStt) {
                    LibInputViewModelBase<MonedaStt> vModel = new BancosMonedaViewModel(valContent as MonedaStt, eAccionSR);
                    vModel.InitializeViewModel(eAccionSR);
                    vResult = vModel;
                } else if (valContent is MovimientoBancarioStt) {
                    LibInputViewModelBase<MovimientoBancarioStt> vModel = new BancosMovimientoBancarioViewModel(valContent as MovimientoBancarioStt, eAccionSR);
                    vModel.InitializeViewModel(eAccionSR);
                    vResult = vModel;
                } else if (valContent is TransferenciaStt) {
                    LibInputViewModelBase<TransferenciaStt> vModel = new BancosTransferenciaViewModel(valContent as TransferenciaStt, eAccionSR);
                    vModel.InitializeViewModel(eAccionSR);
                    vResult = vModel;
                } else if (valContent is NotaEntradaSalidaStt) {
                    LibInputViewModelBase<NotaEntradaSalidaStt> vModel = new InventarioNotaEntradaSalidaViewModel(valContent as NotaEntradaSalidaStt, eAccionSR);
                    vModel.InitializeViewModel(eAccionSR);
                    vResult = vModel;
                } else if (valContent is NotaEntregaStt) {
                    LibInputViewModelBase<NotaEntregaStt> vModel = new NotasDeEntregaViewModel(valContent as NotaEntregaStt, eAccionSR);
                    vModel.InitializeViewModel(eAccionSR);
                    vResult = vModel;
                } else if (valContent is NotasDebitoCreditoEntregaStt) {
                    LibInputViewModelBase<NotasDebitoCreditoEntregaStt> vModel = new CotizacionNotaDebitoCreditoViewModel(valContent as NotasDebitoCreditoEntregaStt, eAccionSR);
                    vModel.InitializeViewModel(eAccionSR);
                    vResult = vModel;
                } else if (valContent is PlanillaDeIVAStt) {
                    LibInputViewModelBase<PlanillaDeIVAStt> vModel = new CxPComprasPlanillaIVAViewModel(valContent as PlanillaDeIVAStt, eAccionSR);
                    vModel.InitializeViewModel(eAccionSR);
                    vResult = vModel;
                } else if (valContent is RetencionISLRStt) {
                    LibInputViewModelBase<RetencionISLRStt> vModel = new CxPComprasRetISLRViewModel(valContent as RetencionISLRStt, eAccionSR);
                    vModel.InitializeViewModel(eAccionSR);
                    vResult = vModel;
                } else if (valContent is RetencionIVAStt) {
                    LibInputViewModelBase<RetencionIVAStt> vModel = new CxPComprasRetIVAViewModel(valContent as RetencionIVAStt, eAccionSR);
                    vModel.InitializeViewModel(eAccionSR);
                    vResult = vModel;
                } else if (valContent is VendedorStt) {
                    LibInputViewModelBase<VendedorStt> vModel = new CotizacionVendedorViewModel(valContent as VendedorStt, eAccionSR);
                    vModel.InitializeViewModel(eAccionSR);
                    vResult = vModel;
                } else if (valContent is FacturaPuntoDeVentaStt) {
                    LibInputViewModelBase<FacturaPuntoDeVentaStt> vModel = new FacturaPuntoDeVentaViewModel(valContent as FacturaPuntoDeVentaStt, eAccionSR, InitFirstTime);
                    vModel.InitializeViewModel(eAccionSR);
                    vResult = vModel;
                } else if (valContent is FacturaBalanzaEtiquetasStt) {
                    LibInputViewModelBase<FacturaBalanzaEtiquetasStt> vModel = new FacturaBalanzaEtiquetasViewModel(valContent as FacturaBalanzaEtiquetasStt, eAccionSR, InitFirstTime);
                    vModel.InitializeViewModel(eAccionSR);
                    vResult = vModel;
                } else if (valContent is FacturaImprentaDigitalStt) {
                    LibInputViewModelBase<FacturaImprentaDigitalStt> vModel = new FacturaImprentaDigitalViewModel(valContent as FacturaImprentaDigitalStt, eAccionSR);
                    vModel.InitializeViewModel(eAccionSR);
                    vResult = vModel;
                } else if (valContent is VerificadorDePreciosStt) {
                    LibInputViewModelBase<VerificadorDePreciosStt> vModel = new VerificadorDePreciosViewModel(valContent as VerificadorDePreciosStt, eAccionSR);
                    vModel.InitializeViewModel(eAccionSR);
                    vResult = vModel;
                } else if (valContent is ImagenesComprobantesRetencionStt) {
                    LibInputViewModelBase<ImagenesComprobantesRetencionStt> vModel = new CxPComprasImagenesComprobanteRetViewModel(valContent as ImagenesComprobantesRetencionStt, eAccionSR);
                    vModel.InitializeViewModel(eAccionSR);
                    vResult = vModel;
                } else if (valContent is ProduccionStt) {
                    LibInputViewModelBase<ProduccionStt> vModel = new InventarioProduccionViewModel(valContent as ProduccionStt, eAccionSR);
                    vModel.InitializeViewModel(eAccionSR);
                    vResult = vModel;
                } else {
                    vResult = valContent;
                }
            }
            return vResult;
        }

        private List<Module> GetModuleList(int valConsecutivoCompania) {
            ISettValueByCompanyPdn vPdn = new clsSettValueByCompanyNav();
            List<Module> vResult = vPdn.GetModuleList(valConsecutivoCompania);
            return vResult;
        }

        private LibRibbonGroupData CreateAccionesRibbonGroup() {
            LibRibbonGroupData vResult = new LibRibbonGroupData("Acciones");
            vResult.ControlDataCollection.Add(new LibRibbonButtonData() {
                KeyTip = "G",
                Label = "Modificar",
                Command = UpdateCommand,
                LargeImage = new Uri("/LibGalac.Aos.UI.WpfRD;component/Images/save.png", UriKind.Relative),
                ToolTipDescription = "Modificar",
                ToolTipTitle = "",
                IsVisible = !LibDefGen.DataBaseInfo.IsReadOnlyRMDB
            });
            vResult.ControlDataCollection.Add(RibbonData.TabDataCollection[0].GroupDataCollection[0].ControlDataCollection[0]);
            RibbonData.RemoveRibbonGroup("Acciones");
            return vResult;
        }

        internal void InitializeViewModel(eAccionSR valAccion) {
            Title = ModuleName + " - " + valAccion.GetDescription();
            if (RibbonData.TabDataCollection != null && RibbonData.TabDataCollection.Count > 0) {
                RibbonData.TabDataCollection[0].Header = Title;
                if (valAccion == eAccionSR.Modificar) {
                    RibbonData.TabDataCollection[0].AddTabGroupData(CreateAccionesRibbonGroup());
                }
            }
            (ModuleList.Where(w => w.DisplayName == LibEnumHelper.GetDescription(eModulesLevelName.Bancos)).FirstOrDefault().Groups.Where(y => y.DisplayName == new BancosMonedaViewModel(null, eAccionSR.Consultar).ModuleName).FirstOrDefault().Content as BancosMonedaViewModel).ParametrosViewModel = this;
            (ModuleList.Where(w => w.DisplayName == LibEnumHelper.GetDescription(eModulesLevelName.Factura)).FirstOrDefault().Groups.Where(y => y.DisplayName == new FacturaFacturacionContViewModel(null, eAccionSR.Consultar).ModuleName).FirstOrDefault().Content as FacturaFacturacionContViewModel).ParametrosViewModel = this;
            (ModuleList.Where(w => w.DisplayName == LibEnumHelper.GetDescription(eModulesLevelName.Factura)).FirstOrDefault().Groups.Where(y => y.DisplayName == new FacturaCobroFacturaViewModel(null, eAccionSR.Consultar).ModuleName).FirstOrDefault().Content as FacturaCobroFacturaViewModel).ParametrosViewModel = this;
        }

        public static void Send<T>(string valPropertyName, T valValue) {
            var valMessage = new NotificationMessage<T>(valValue, valPropertyName);
            Messenger.Default.Send<NotificationMessage<T>>(valMessage, "PropertyChanged");
        }

        public static void Register<T>(object valRecipient, Action<NotificationMessage<T>> valAction) {
            Messenger.Default.Register<NotificationMessage<T>>(valRecipient, "PropertyChanged", valAction);
        }

        public override string Error {
            get {
                List<string> vErrors = new List<string>(BuildValidationErrors());
                ModuleList.OrderBy(p => p.Groups);
                foreach (var vModule in ModuleList) {
                    foreach (var vGroup in vModule.Groups) {
                        ViewModelBase vViewModel = vGroup.Content as ViewModelBase;
                        if (!LibString.IsNullOrEmpty(vViewModel.Error, true)) {
                            vErrors.Add(vViewModel.Error);
                            return string.Join(Environment.NewLine, vErrors);
                        }
                    }
                }
                return string.Join(Environment.NewLine, vErrors);
            }
        }


        protected override void InitializeCommands() {
            base.InitializeCommands();
            UpdateCommand = new RelayCommand(ExecuteUpdateCommand);
        }

        public override bool OnClosing() {
            IsClosing = true;
            StopClosing = false;
            if (Action == eAccionSR.Modificar) {
                if (IsDirty) {
                    string vMessage = string.Format("¿Desea guardar los cambios efectuados en {0}?", ModuleName);
                    bool vCanceled = false;
                    string vOption = LibMessages.MessageBox.RequestOption(this, vMessage, Title, new string[] { "Guardar", "No Guardar", "Cancelar" }, out vCanceled);
                    if (LibString.S1IsEqualToS2(vOption, "Guardar")) {
                        StopClosing = !IsValid;
                        ExecuteUpdateCommand();
                    } else if (LibString.S1IsEqualToS2(vOption, "No Guardar")) {
                        StopClosing = false;
                    } else if (LibString.S1IsEqualToS2(vOption, "Cancelar")) {
                        StopClosing = true;
                        IsClosing = false;
                    }
                }
            }
            return StopClosing;
        }

        private bool SeModificoAlgunValor() {
            bool vResult = false;
            foreach (var item in ModuleList) {
                foreach (var item1 in item.Groups) {
                    ILibInputViewModel vViewModel = item1.Content as ILibInputViewModel;
                    if (vViewModel.IsDirty) {
                        return true;
                    }
                }
            }
            return vResult;
        }

        private void ExecuteUpdateCommand() {
            try {
                if (!IsValid) {
                    LibMessages.RaiseError.ShowError(new GalacValidationException(Error), ModuleName);
                    return;
                }
                ISettValueByCompanyPdn vSettValueByCompanyPdn = new clsSettValueByCompanyNav();
                if (vSettValueByCompanyPdn.SpecializedUpdate(ModuleList)) {
                    DialogResult = true;
                    IsDirty = false;
                    if (!IsClosing) {
                        RaiseRequestCloseEvent();
                    }
                    if (!IsClosing) {
                        RaiseRequestCloseEvent();
                    }
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
            }
        }

        protected override void ExecuteCancel() {
            IsDirty = SeModificoAlgunValor();
            base.ExecuteCancel();
        }
        #endregion
    }
}
