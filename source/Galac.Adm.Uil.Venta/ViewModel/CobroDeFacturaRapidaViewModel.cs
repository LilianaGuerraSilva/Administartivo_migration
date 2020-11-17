using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Xml.Linq;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using LibGalac.Aos.DefGen;
using LibGalac.Aos.UI.Mvvm;
using System.ComponentModel;
using LibGalac.Aos.UI.Mvvm.Command;
using LibGalac.Aos.UI.Mvvm.Helpers;
using LibGalac.Aos.UI.Mvvm.Messaging;
using LibGalac.Aos.UI.Mvvm.Ribbon;
using LibGalac.Aos.UI.Mvvm.Validation;
using LibGalac.Aos.Base.Dal;
using LibGalac.Aos.Dal;
using Galac.Adm.Brl.Venta;
using Galac.Adm.Ccl.Venta;
using Galac.Adm.Dal.Venta;
using Galac.Saw.Ccl.Tablas;
using Galac.Comun.Ccl.SttDef;
using Galac.Adm.Uil.DispositivosExternos.ViewModel;
using Galac.Adm.Ccl.DispositivosExternos;
using Galac.Saw.Ccl.Inventario;
using LibGalac.Aos.Brl;
using Galac.Adm.Brl.CAnticipo;
using Galac.Adm.Ccl.CAnticipo;
using Galac.Adm.Dal.CAnticipo;
using Galac.Comun.Ccl.TablasGen;
using Galac.Comun.Brl.TablasGen;

namespace Galac.Adm.Uil.Venta.ViewModel {
    public class CobroDeFacturaRapidaViewModel : CobroRapidoVzlaViewModelBase {

        #region Constantes

        private const string EfectivoPropertyName = "Efectivo";
        private const string DiferenciaPropertyName = "Diferencia";
        private const string ColorDiferenciaPropertyName = "ColorDiferencia";
        private const string TarjetaPropertyName = "Tarjeta";
        private const string DepositoTransfPropertyName = "DepositoTransf";
        private const string ChequePropertyName = "Cheque";
        private const string AnticipoPropertyName = "Anticipo";
        private const string lblMontoDiferenciaPropertyName = "lblMontoDiferencia";
        private const string lblMontoTotalCobradoPropertyName = "lblMontoTotalCobrado";
        private const string lblMontoTotalACobrarPropertyName = "lblMontoTotalACobrar";
        private const string lblMontoEfectivoPropertyName = "lblMontoEfectivo";
        private const string lblMontoTarjetaPropertyName = "lblMontoTarjeta";
        private const string lblMontoDepositoTransfPropertyName = "lblMontoDepositoTransf";
        private const string lblMontoChequePropertyName = "lblMontoCheque";
        private const string lblMontoAnticipoPropertyName = "lblMontoAnticipo";

        private decimal _Efectivo;
        private decimal _Tarjeta;
        private decimal _DepositoTransf;
        private decimal _Cheque;
        private decimal _Anticipo;
        private string _Diferencia;
        private int _ContaforErrorPK;
        private string _ColorDiferencia;
        public event EventHandler IrACobroFactura;
        private IMonedaLocalActual vMonedaLocalActual;
        List<RenglonCobroDeFactura> ListDeCobro = new List<RenglonCobroDeFactura>();
        List<RenglonCobroDeFactura> ListDeCobroMaster = new List<RenglonCobroDeFactura>();

        #endregion

        #region Propiedades

        public override string ModuleName {
            get {
                return "Punto de Venta";
            }
        }

        public string lblMontoTotalACobrar {
            get {
                return LibConvert.NumToString(TotalFactura, 2);
            }
            set {

            }
        }

        public string lblMontoDiferencia {
            get {
                return LibConvert.NumToString(MontoRestantePorPagar, 2);
            }
            set {

            }
        }

        public string lblMontoTotalCobrado {
            get {
                return LibConvert.NumToString(TotalCobrado, 2);
            }
            set {

            }
        }

        public decimal Efectivo {
            get {
                return _Efectivo;
            }
            set {
                if (_Efectivo != value) {
                    _Efectivo = value;
                    RaisePropertyChanged(EfectivoPropertyName);
                    RaisePropertyChanged(lblMontoEfectivoPropertyName);
                }
            }
        }

        public string lblMontoEfectivo {
            get {
                return LibConvert.NumToString(Efectivo, 2);
            }
            set {

            }
        }

        public decimal Tarjeta {
            get {
                return _Tarjeta;
            }
            set {
                if (_Tarjeta != value) {
                    _Tarjeta = value;
                    RaisePropertyChanged(TarjetaPropertyName);
                    RaisePropertyChanged(lblMontoTarjetaPropertyName);
                }
            }
        }

        public string lblMontoTarjeta {
            get {
                return LibConvert.NumToString(Tarjeta, 2);
            }
            set {

            }
        }

        public decimal DepositoTransf {
            get {
                return _DepositoTransf;
            }
            set {
                if (_DepositoTransf != value) {
                    _DepositoTransf = value;
                    RaisePropertyChanged(DepositoTransfPropertyName);
                    RaisePropertyChanged(lblMontoDepositoTransfPropertyName);
                }
            }
        }

        public string lblMontoDepositoTransf {
            get {
                return LibConvert.NumToString(DepositoTransf, 2);
            }
            set {

            }
        }

        public decimal Cheque {
            get {
                return _Cheque;
            }
            set {
                if (_Cheque != value) {
                    _Cheque = value;
                    RaisePropertyChanged(ChequePropertyName);
                    RaisePropertyChanged(lblMontoChequePropertyName);
                }
            }
        }

        public string lblMontoCheque {
            get {
                return LibConvert.NumToString(Cheque, 2);
            }
            set {

            }
        }

        public decimal Anticipo {
            get {
                return _Anticipo;
            }
            set {
                if (_Anticipo != value) {
                    _Anticipo = value;
                    RaisePropertyChanged(AnticipoPropertyName);
                    RaisePropertyChanged(lblMontoAnticipoPropertyName);
                }
            }
        }

        public string lblMontoAnticipo {
            get {
                return LibConvert.NumToString(Anticipo, 2);
            }
            set {

            }
        }

        public string Diferencia {
            get {
                return _Diferencia;
            }
            set {
                if (MontoRestantePorPagar > 0) {
                    _Diferencia = "Falta";
                } else {
                    _Diferencia = "Cambio";
                }
                RaisePropertyChanged(ColorDiferenciaPropertyName);
            }
        }

        public string ColorDiferencia {
            get {
                return _ColorDiferencia;
            }
            set {
                if (MontoRestantePorPagar > 0) {
                    _ColorDiferencia = "Red";
                } else {
                    _ColorDiferencia = "Green";
                }
                RaisePropertyChanged(DiferenciaPropertyName);
            }
        }

        public RelayCommand CobroEfectivoCommand {
            get;
            private set;
        }

        public RelayCommand CobroChequeCommand {
            get;
            private set;
        }

        public RelayCommand CobroTarjetaCommand {
            get;
            private set;
        }

        public RelayCommand CobroDepositoCommand {
            get;
            private set;
        }

        public RelayCommand CobroAnticipoCommand {
            get;
            private set;
        }

        protected override bool CanExecuteCobrarCommand() {
            return base.CanExecuteCobrarCommand() && MontoRestantePorPagar <= 0;
        }

        #endregion //Propiedades

        #region Constructores

        public CobroDeFacturaRapidaViewModel() {
            PropertyChanged += OnPropertyChanged;
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (e.PropertyName == MontoRestantePorPagarPropertyName) {
                Diferencia = null;
                ColorDiferencia = null;
                RaisePropertyChanged(lblMontoDiferenciaPropertyName);
                RaisePropertyChanged(DiferenciaPropertyName);
                RaisePropertyChanged(ColorDiferenciaPropertyName);
            } else if (e.PropertyName == TotalFacturaPropertyName) {
                RaisePropertyChanged(lblMontoTotalACobrarPropertyName);
                RaisePropertyChanged(DiferenciaPropertyName);
                RaisePropertyChanged(lblMontoDiferenciaPropertyName);
            } else if (e.PropertyName == TotalCobradoPropertyName) {
                RaisePropertyChanged(lblMontoTotalCobradoPropertyName);
                RaisePropertyChanged(DiferenciaPropertyName);
            }
        }

        //public CobroDeFacturaRapidaViewModel()
        //    : this(new CobroDeFacturaRapida(), eAccionSR.Insertar) {
        //}
        //public CobroDeFacturaRapidaViewModel(CobroDeFacturaRapida initModel, eAccionSR initAction)
        //    : base(initModel, initAction, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
        //    DefaultFocusedPropertyName = TotalACobrarPropertyName;
        //    Model.ConsecutivoCompania = Mfc.GetInt("Compania");
        //    InitializeDetails();
        //}

        #endregion //Constructores

        #region Metodos Generados

        protected override void InitializeCommands() {
            base.InitializeCommands();
            CobroEfectivoCommand = new RelayCommand(ExecuteCobroEfectivoCommand, CanExecuteCobroEfectivoCommand);
            CobroChequeCommand = new RelayCommand(ExecuteCobroChequeCommand, CanExecuteCobroChequeCommand);
            CobroTarjetaCommand = new RelayCommand(ExecuteCobroTarjetaCommand, CanExecuteCobroTarjetaCommand);
            CobroDepositoCommand = new RelayCommand(ExecuteCobroDepositoCommand, CanExecuteCobroTarjetaCommand);
            CobroAnticipoCommand = new RelayCommand(ExecuteCobroAnticipoCommand, CanExecuteCobroAnticipoCommand);
        }

        public void InitializeViewModel(eAccionSR valAction, FacturaRapida valFactura, List<RenglonCobroDeFactura> valListDeCobroMaster, int valAlicuotaIvaASustituir) {
            NumeroFactura = valFactura.Numero;
            TotalFactura = valFactura.TotalFactura;
            MontoRestantePorPagar = valFactura.TotalFactura;
            Diferencia = "Cambio";
            ColorDiferencia = "Green";
            CodigoCliente = valFactura.CodigoCliente;
            ConsecutivoCompania = LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania");
            insFactura = valFactura;
            ListDeCobroMaster = valListDeCobroMaster;
            if (ListDeCobroMaster.Count > 0) {
                ListDeCobro.AddRange(ListDeCobroMaster);
                ActualizarTotalesCobrado();
            }
            _ContaforErrorPK = 0;
            _AlicuotaIvaASustituir = valAlicuotaIvaASustituir;
            vMonedaLocalActual = new clsMonedaLocalActual();
            vMonedaLocalActual.CargarTodasEnMemoriaYAsignarValoresDeLaActual(LibDefGen.ProgramInfo.Country, LibDate.Today());
        }

        protected override void InitializeRibbon() {
            base.InitializeRibbon();
            if (RibbonData.TabDataCollection != null && RibbonData.TabDataCollection.Count > 0) {
                RibbonData.TabDataCollection[0].AddTabGroupData(CreateFormasdePagoRibbonButtonGroup());
                RibbonData.RemoveRibbonControl("Acciones", "Insertar");
                var vAccionesGrupo = RibbonData.TabDataCollection[0].GroupDataCollection[0];
                RibbonData.TabDataCollection[0].GroupDataCollection.Remove(vAccionesGrupo);
                RibbonData.TabDataCollection[0].GroupDataCollection.Insert(2, vAccionesGrupo);
                //RibbonData.TabDataCollection[0].GroupDataCollection[0].ControlDataCollection.Insert(0, CreateInsertarRibbonButtonGroup());
            }
        }

        CobroDeFacturaRapidaViewModel CreateNewElementForCobroEfectivo(CobroDeFacturaRapida valModel, eAccionSR valAction) {
            var vNewModel = valModel;
            return new CobroDeFacturaRapidaViewModel();
        }

        CobroDeFacturaRapidaViewModel CreateNewElementForCobroCheque(CobroDeFacturaRapida valModel, eAccionSR valAction) {
            var vNewModel = valModel;
            return new CobroDeFacturaRapidaViewModel();
        }

        CobroDeFacturaRapidaViewModel CreateNewElementForCobroTarjeta(CobroDeFacturaRapida valModel, eAccionSR valAction) {
            var vNewModel = valModel;
            return new CobroDeFacturaRapidaViewModel();
        }

        CobroDeFacturaRapidaViewModel CreateNewElementForCobroDeposito(CobroDeFacturaRapida valModel, eAccionSR valAction) {
            var vNewModel = valModel;
            return new CobroDeFacturaRapidaViewModel();
        }

        CobroDeFacturaRapidaViewModel CreateNewElementForCobroAnticipo(CobroDeFacturaRapida valModel, eAccionSR valAction) {
            var vNewModel = valModel;
            return new CobroDeFacturaRapidaViewModel();
        }

        CobroDeFacturaRapidaViewModel CreateNewElementForCobroInsertar(CobroDeFacturaRapida valModel, eAccionSR valAction) {
            var vNewModel = valModel;
            return new CobroDeFacturaRapidaViewModel();
        }

        protected void ExecuteCommandsRaiseCanExecuteChanged() {
            CobroEfectivoCommand.RaiseCanExecuteChanged();
            CobroChequeCommand.RaiseCanExecuteChanged();
            CobroTarjetaCommand.RaiseCanExecuteChanged();
            CobroDepositoCommand.RaiseCanExecuteChanged();
            CobroAnticipoCommand.RaiseCanExecuteChanged();
            CobrarCommand.RaiseCanExecuteChanged();
        }

        #endregion //CobroDeFacturaRapidaDetalle

        private LibRibbonGroupData CreateFormasdePagoRibbonButtonGroup() {
            LibRibbonGroupData vResult = new LibRibbonGroupData("");
            vResult.ControlDataCollection.Add(new LibRibbonButtonData() {
                Label = "Efectivo",
                Command = CobroEfectivoCommand,
                LargeImage = new Uri("/Galac.Adm.Uil.Venta;component/Images/F1.png", UriKind.Relative),
                ToolTipDescription = "Cobro en Efectivo.",
                ToolTipTitle = "Ejecutar Acción (F1)",
                IsVisible = true,
                KeyTip = "F1"
            });
            vResult.ControlDataCollection.Add(new LibRibbonButtonData() {
                Label = "Cheque",
                Command = CobroChequeCommand,
                LargeImage = new Uri("/Galac.Adm.Uil.Venta;component/Images/F2.png", UriKind.Relative),
                ToolTipDescription = "Cobro en Cheque",
                ToolTipTitle = "Ejecutar Acción (F2)",
                IsVisible = true,
                KeyTip = "F2"
            });
            vResult.ControlDataCollection.Add(new LibRibbonButtonData() {
                Label = "Tarjeta",
                Command = CobroTarjetaCommand,
                LargeImage = new Uri("/Galac.Adm.Uil.Venta;component/Images/F3.png", UriKind.Relative),
                ToolTipDescription = "Cobro con Tarjetas",
                ToolTipTitle = "Ejecutar Acción (F3)",
                IsVisible = true,
                KeyTip = "F3"
            });
            vResult.ControlDataCollection.Add(new LibRibbonButtonData() {
                Label = "Depósito",
                Command = CobroDepositoCommand,
                LargeImage = new Uri("/Galac.Adm.Uil.Venta;component/Images/F4.png", UriKind.Relative),
                ToolTipDescription = "Cobro con Deposito",
                ToolTipTitle = "Ejecutar Acción (F4)",
                IsVisible = true,
                KeyTip = "F4"
            });
            vResult.ControlDataCollection.Add(new LibRibbonButtonData() {
                Label = "Anticipo",
                Command = CobroAnticipoCommand,
                LargeImage = new Uri("/Galac.Adm.Uil.Venta;component/Images/F5.png", UriKind.Relative),
                ToolTipDescription = "Cobro con Anticipo",
                ToolTipTitle = "Cobro con Anticipo",
                IsVisible = true,
                KeyTip = "F5"
            });
            return vResult;
        }

        private bool CanExecuteCobroEfectivoCommand() {
            if (insFactura.AplicaDecretoIvaEspecialAsBool) {
                return false;
            } else {
                return true;
            }
        }

        private bool CanExecuteCobroChequeCommand() {
            return true;
        }


        private bool CanExecuteCobroTarjetaCommand() {
            return true;
        }

        private bool CanExecuteCobroAnticipoCommand() {
            return true;
        }

        private void ExecuteCobroEfectivoCommand() {
            decimal TotalCobradoOtrosTipos;
            try {
                var vListEfectivo = ListDeCobro.Where(p => p.CodigoFormaDelCobro == eFormaDeCobro.Efectivo.GetDescription(1)).Select(p => p).FirstOrDefault();
                TotalCobradoOtrosTipos = CalcularTotalCobradoOtrosTipos(eFormaDeCobro.Efectivo.GetDescription(1));
                CobroDeFacturaRapidaEfectivoViewModel vViewModelEfectivo = new CobroDeFacturaRapidaEfectivoViewModel();
                vViewModelEfectivo.InitLookAndFeel(NumeroFactura, TotalFactura, TotalCobrado, Efectivo, TotalCobradoOtrosTipos);
                bool resultado = LibMessages.EditViewModel.ShowEditor(vViewModelEfectivo, true);
                if (vViewModelEfectivo.EsBotonGrabar) {
                    if (vListEfectivo != null) {
                        ListDeCobro.RemoveAll(p => p.CodigoFormaDelCobro == eFormaDeCobro.Efectivo.GetDescription(1));
                    }
                    RenglonCobroDeFactura vRenglonCobro = new RenglonCobroDeFactura();
                    vRenglonCobro.ConsecutivoCompania = ConsecutivoCompania;
                    vRenglonCobro.NumeroFactura = NumeroFactura;
                    vRenglonCobro.TipoDeDocumentoAsEnum = eTipoDocumentoFactura.ComprobanteFiscal;
                    vRenglonCobro.ConsecutivoRenglon = 1;
                    vRenglonCobro.CodigoFormaDelCobro = eFormaDeCobro.Efectivo.GetDescription(1);
                    vRenglonCobro.NumeroDelDocumento = "";
                    vRenglonCobro.Monto = vViewModelEfectivo.MontoEfectivo;
                    vRenglonCobro.CodigoBanco = 1;
                    vRenglonCobro.CodigoPuntoDeVenta = 1;
                    vRenglonCobro.CodigoMoneda = vMonedaLocalActual.GetHoyCodigoMoneda();
                    vRenglonCobro.CambioAMonedaLocal = 1;
                    ListDeCobro.Add(vRenglonCobro);
                    Efectivo = vViewModelEfectivo.MontoEfectivo;
                }
                CalcularTotales();
                RaiseIrACobroFactura();
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
            }
        }

        private void ExecuteCobroChequeCommand() {
            try {
                decimal TotalCobradoOtrosTipos;
                TotalCobradoOtrosTipos = CalcularTotalCobradoOtrosTipos(eFormaDeCobro.Cheque.GetDescription(1));
                FacturaRapidaViewModel vViewModelFacturaRapida = new FacturaRapidaViewModel();
                CobroDeFacturaRapidaDepositoTransfViewModel vViewModel = new CobroDeFacturaRapidaDepositoTransfViewModel(true);
                vViewModel.InitializeViewModel(eAccionSR.Insertar);
                vViewModel.InitLookAndFeel(NumeroFactura, TotalFactura, Cheque, TotalCobradoOtrosTipos, true);
                foreach (var vitem in ListDeCobro.Where(p => p.CodigoFormaDelCobro == eFormaDeCobro.Cheque.GetDescription(1))) {
                    CobroDeFacturaRapidaDepositoTransfDetalleViewModel vDetalle = new CobroDeFacturaRapidaDepositoTransfDetalleViewModel();
                    vDetalle.NumeroDelDocumento = vitem.NumeroDelDocumento;
                    vDetalle.CodigoBanco = vitem.CodigoBanco;
                    vDetalle.Monto = vitem.Monto;
                    vDetalle.Master = vViewModel;
                    vDetalle.RecargarConexiones();
                    vViewModel.DetailCobroDeFacturaRapidaDepositoTransfDetalle.Items.Add(vDetalle);
                }
                int vConsecutivoRenglon = 0;
                bool result = LibMessages.EditViewModel.ShowEditor(vViewModel, true);
                if (vViewModel.EsBotonGrabar) {
                    ListDeCobro.RemoveAll(p => p.CodigoFormaDelCobro == eFormaDeCobro.Cheque.GetDescription(1));
                    vConsecutivoRenglon = ListDeCobro.Count;
                    if (vViewModel.DetailCobroDeFacturaRapidaDepositoTransfDetalle.Items.Count > 0) {
                        foreach (var vItem in vViewModel.DetailCobroDeFacturaRapidaDepositoTransfDetalle.Items) {
                            if ((!LibString.IsNullOrEmpty(vItem.NumeroDelDocumento, true)) && (!LibString.IsNullOrEmpty(vItem.NombreBanco, true)) && (vItem.Monto > 0)) {
                                RenglonCobroDeFactura vRenglonCobro = new RenglonCobroDeFactura();
                                vConsecutivoRenglon = vConsecutivoRenglon + 1;
                                vRenglonCobro.ConsecutivoCompania = ConsecutivoCompania;
                                vRenglonCobro.NumeroFactura = NumeroFactura;
                                vRenglonCobro.TipoDeDocumentoAsEnum = eTipoDocumentoFactura.ComprobanteFiscal;
                                vRenglonCobro.ConsecutivoRenglon = vConsecutivoRenglon;
                                vRenglonCobro.CodigoFormaDelCobro = eFormaDeCobro.Cheque.GetDescription(1);
                                vRenglonCobro.NumeroDelDocumento = vItem.NumeroDelDocumento;
                                vRenglonCobro.CodigoBanco = vItem.CodigoBanco;
                                vRenglonCobro.Monto = vItem.Monto;
                                vRenglonCobro.NombreBanco = vItem.NombreBanco;
                                vRenglonCobro.CodigoMoneda = vMonedaLocalActual.GetHoyCodigoMoneda();
                                vRenglonCobro.CambioAMonedaLocal = 1;
                                ListDeCobro.Add(vRenglonCobro);
                            }
                        }
                    }
                }
                Cheque = CalcularTotalCobradoPorTipo(eFormaDeCobro.Cheque.GetDescription(1));
                CalcularTotales();
                RaiseIrACobroFactura();
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
            }
        }

        private void ExecuteCobroTarjetaCommand() {
            try {
                decimal TotalCobradoOtrosTipos;
                TotalCobradoOtrosTipos = CalcularTotalCobradoOtrosTipos(eFormaDeCobro.Tarjeta.GetDescription(1));
                CobroDeFacturaRapidaTarjetaViewModel vViewModel = new CobroDeFacturaRapidaTarjetaViewModel();
                vViewModel.InitializeViewModel(eAccionSR.Insertar);
                vViewModel.InitLookAndFeel(NumeroFactura, TotalFactura, Tarjeta, TotalCobradoOtrosTipos);
                foreach (var vitem in ListDeCobro.Where(p => p.CodigoFormaDelCobro == eFormaDeCobro.Tarjeta.GetDescription(1))) {
                    CobroDeFacturaRapidaTarjetaDetalleViewModel vDetalle = new CobroDeFacturaRapidaTarjetaDetalleViewModel();
                    vDetalle.NumeroDelDocumento = vitem.NumeroDelDocumento;
                    vDetalle.CodigoBanco = vitem.CodigoBanco;
                    vDetalle.CodigoPuntoDeVenta = vitem.CodigoPuntoDeVenta;
                    vDetalle.Monto = vitem.Monto;
                    vDetalle.NumeroDocumentoAprobacion = vitem.NumeroDocumentoAprobacion;
                    vDetalle.Master = vViewModel;
                    vDetalle.RecargarConexiones();
                    vViewModel.DetailCobroDeFacturaRapidaTarjetaDetalle.Items.Add(vDetalle);
                }
                int vConsecutivoRenglon = 0;
                bool result = LibMessages.EditViewModel.ShowEditor(vViewModel, true);
                if (vViewModel.EsBotonGrabar) {
                    ListDeCobro.RemoveAll(p => p.CodigoFormaDelCobro == eFormaDeCobro.Tarjeta.GetDescription(1));
                    vConsecutivoRenglon = ListDeCobro.Count;
                    if (vViewModel.DetailCobroDeFacturaRapidaTarjetaDetalle.Items.Count > 0) {
                        foreach (var vItem in vViewModel.DetailCobroDeFacturaRapidaTarjetaDetalle.Items) {
                            if ((!LibString.IsNullOrEmpty(vItem.NumeroDelDocumento, true)) && (!LibString.IsNullOrEmpty(vItem.NombreBanco, true)) && (vItem.Monto > 0)) {
                                RenglonCobroDeFactura vRenglonCobro = new RenglonCobroDeFactura();
                                vConsecutivoRenglon = vConsecutivoRenglon + 1;
                                vRenglonCobro.ConsecutivoCompania = ConsecutivoCompania;
                                vRenglonCobro.NumeroFactura = NumeroFactura;
                                vRenglonCobro.TipoDeDocumentoAsEnum = eTipoDocumentoFactura.ComprobanteFiscal;
                                vRenglonCobro.ConsecutivoRenglon = vConsecutivoRenglon;
                                vRenglonCobro.CodigoFormaDelCobro = eFormaDeCobro.Tarjeta.GetDescription(1);
                                vRenglonCobro.NumeroDelDocumento = vItem.NumeroDelDocumento;
                                vRenglonCobro.CodigoBanco = vItem.CodigoBanco;
                                vRenglonCobro.CodigoPuntoDeVenta = vItem.CodigoPuntoDeVenta;
                                vRenglonCobro.Monto = vItem.Monto;
                                vRenglonCobro.NumeroDocumentoAprobacion = vItem.NumeroDocumentoAprobacion;
                                vRenglonCobro.NombreBanco = vItem.NombreBanco;
                                vRenglonCobro.NombreBancoPuntoDeVenta = vItem.NombreBancoPuntoDeVenta;
                                vRenglonCobro.CodigoMoneda = vMonedaLocalActual.GetHoyCodigoMoneda();
                                vRenglonCobro.CambioAMonedaLocal = 1;
                                ListDeCobro.Add(vRenglonCobro);
                            }
                        }
                    }
                }
                Tarjeta = CalcularTotalCobradoPorTipo(eFormaDeCobro.Tarjeta.GetDescription(1));
                CalcularTotales();
                RaiseIrACobroFactura();
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
            }
        }

        private void ExecuteCobroDepositoCommand() {
            try {
                decimal TotalCobradoOtrosTipos;
                TotalCobradoOtrosTipos = CalcularTotalCobradoOtrosTipos(eFormaDeCobro.Deposito.GetDescription(1));
                CobroDeFacturaRapidaDepositoTransfViewModel vViewModel = new CobroDeFacturaRapidaDepositoTransfViewModel(false);
                vViewModel.InitializeViewModel(eAccionSR.Insertar);
                vViewModel.InitLookAndFeel(NumeroFactura, TotalFactura, DepositoTransf, TotalCobradoOtrosTipos, false);
                foreach (var vitem in ListDeCobro.Where(p => p.CodigoFormaDelCobro == eFormaDeCobro.Deposito.GetDescription(1))) {
                    CobroDeFacturaRapidaDepositoTransfDetalleViewModel vDetalle = new CobroDeFacturaRapidaDepositoTransfDetalleViewModel();
                    vDetalle.NumeroDelDocumento = vitem.NumeroDelDocumento;
                    vDetalle.CodigoBanco = vitem.CodigoBanco;
                    vDetalle.Monto = vitem.Monto;
                    vDetalle.Master = vViewModel;
                    vDetalle.RecargarConexiones();
                    vViewModel.DetailCobroDeFacturaRapidaDepositoTransfDetalle.Items.Add(vDetalle);
                }
                int vConsecutivoRenglon = 0;
                bool result = LibMessages.EditViewModel.ShowEditor(vViewModel, true);
                if (vViewModel.EsBotonGrabar) {
                    ListDeCobro.RemoveAll(p => p.CodigoFormaDelCobro == eFormaDeCobro.Deposito.GetDescription(1));
                    vConsecutivoRenglon = ListDeCobro.Count;
                    if (vViewModel.DetailCobroDeFacturaRapidaDepositoTransfDetalle.Items.Count > 0) {
                        foreach (var vItem in vViewModel.DetailCobroDeFacturaRapidaDepositoTransfDetalle.Items) {
                            if ((!LibString.IsNullOrEmpty(vItem.NumeroDelDocumento, true)) && (!LibString.IsNullOrEmpty(vItem.NombreBanco, true)) && (vItem.Monto > 0)) {
                                RenglonCobroDeFactura vRenglonCobro = new RenglonCobroDeFactura();
                                vConsecutivoRenglon = vConsecutivoRenglon + 1;
                                vRenglonCobro.ConsecutivoCompania = ConsecutivoCompania;
                                vRenglonCobro.NumeroFactura = NumeroFactura;
                                vRenglonCobro.TipoDeDocumentoAsEnum = eTipoDocumentoFactura.ComprobanteFiscal;
                                vRenglonCobro.ConsecutivoRenglon = vConsecutivoRenglon;
                                vRenglonCobro.CodigoFormaDelCobro = eFormaDeCobro.Deposito.GetDescription(1);
                                vRenglonCobro.NumeroDelDocumento = vItem.NumeroDelDocumento;
                                vRenglonCobro.CodigoBanco = vItem.CodigoBanco;
                                vRenglonCobro.Monto = vItem.Monto;
                                vRenglonCobro.NombreBanco = vItem.NombreBanco;
                                vRenglonCobro.CodigoMoneda = vMonedaLocalActual.GetHoyCodigoMoneda();
                                vRenglonCobro.CambioAMonedaLocal = 1;
                                ListDeCobro.Add(vRenglonCobro);
                            }
                        }
                    }
                }
                DepositoTransf = CalcularTotalCobradoPorTipo(eFormaDeCobro.Deposito.GetDescription(1));
                CalcularTotales();
                RaiseIrACobroFactura();
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
            }
        }

        private void ExecuteCobroAnticipoCommand() {
            try {
                decimal TotalCobradoOtrosTipos;
                TotalCobradoOtrosTipos = CalcularTotalCobradoOtrosTipos(eFormaDeCobro.Anticipo.GetDescription(1));
                CobroDeFacturaRapidaAnticipoViewModel vViewModel = new CobroDeFacturaRapidaAnticipoViewModel();
                vViewModel.InitializeViewModel(eAccionSR.Insertar);
                vViewModel.InitLookAndFeel(NumeroFactura, TotalFactura, Anticipo, TotalCobradoOtrosTipos, CodigoCliente);
                foreach (var vitem in ListDeCobro.Where(p => p.CodigoFormaDelCobro == eFormaDeCobro.Anticipo.GetDescription(1))) {
                    CobroDeFacturaRapidaAnticipoDetalleViewModel vDetalle = new CobroDeFacturaRapidaAnticipoDetalleViewModel();
                    vDetalle.NumeroAnticipo = LibConvert.ToStr(vitem.NumeroDelDocumento);
                    vDetalle.Monto = vitem.Monto;
                    vDetalle.Master = vViewModel;
                    vDetalle.RecargarConexiones();
                    vViewModel.DetailCobroDeFacturaRapidaAnticipoDetalle.Items.Add(vDetalle);
                }
                int vConsecutivoRenglon = 0;
                bool result = LibMessages.EditViewModel.ShowEditor(vViewModel, true);
                if (vViewModel.EsBotonGrabar) {
                    ListDeCobro.RemoveAll(p => p.CodigoFormaDelCobro == eFormaDeCobro.Anticipo.GetDescription(1));
                    vConsecutivoRenglon = ListDeCobro.Count;
                    if (vViewModel.DetailCobroDeFacturaRapidaAnticipoDetalle.Items.Count > 0) {
                        foreach (var vItem in vViewModel.DetailCobroDeFacturaRapidaAnticipoDetalle.Items) {
                            if ((!LibString.IsNullOrEmpty(LibConvert.ToStr(vItem.CodigoAnticipo), true)) && (vItem.Monto > 0)) {
                                RenglonCobroDeFactura vRenglonCobro = new RenglonCobroDeFactura();
                                vConsecutivoRenglon = vConsecutivoRenglon + 1;
                                vRenglonCobro.ConsecutivoCompania = ConsecutivoCompania;
                                vRenglonCobro.NumeroFactura = NumeroFactura;
                                vRenglonCobro.TipoDeDocumentoAsEnum = eTipoDocumentoFactura.ComprobanteFiscal;
                                vRenglonCobro.ConsecutivoRenglon = vConsecutivoRenglon;
                                vRenglonCobro.CodigoFormaDelCobro = eFormaDeCobro.Anticipo.GetDescription(1);
                                vRenglonCobro.NumeroDelDocumento = LibConvert.ToStr(vItem.NumeroAnticipo);
                                vRenglonCobro.Monto = vItem.Monto;
                                vRenglonCobro.CodigoMoneda = vMonedaLocalActual.GetHoyCodigoMoneda();
                                vRenglonCobro.CambioAMonedaLocal = 1;
                                ListDeCobro.Add(vRenglonCobro);
                            }
                        }
                    }
                }
                Anticipo = CalcularTotalCobradoPorTipo(eFormaDeCobro.Anticipo.GetDescription(1));
                CalcularTotales();
                RaiseIrACobroFactura();
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
            }
        }

        private void ActualizarCamposEnXmlFactura(XElement valXmlFactura, string valNumerofactura, string valNumeroComporbanteFiscal, string valSerialMaquinaFiscal) {
            if (valXmlFactura != null) {
                var vRecords = valXmlFactura.Descendants("GpResult");
                if (vRecords != null && vRecords.FirstOrDefault() != null) {
                    var vRecord = vRecords.FirstOrDefault();
                    var vProperty = vRecord.Element("Numero");
                    if (vProperty != null && !LibString.IsNullOrEmpty(valNumerofactura, true)) {
                        vProperty.Value = valNumerofactura;
                    }
                    vProperty = vRecord.Element("NumeroComprobanteFiscal");
                    if (vProperty != null && LibString.IsNullOrEmpty(vProperty.Value, true)) {
                        vProperty.Value = valNumeroComporbanteFiscal;
                    }
                    vProperty = vRecord.Element("SerialMaquinaFiscal");
                    if (vProperty != null && LibString.IsNullOrEmpty(vProperty.Value, true)) {
                        vProperty.Value = valSerialMaquinaFiscal;
                    }
                }
            }
        }

        protected override void ExecuteCobrarCommand() {
            try {
                if (!IsValid) {
                    LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(new GalacValidationException(Error), ModuleName, ModuleName);
                    return;
                }
                IRenglonCobroDeFacturaPdn vRenglonCobroDeFactura = new clsRenglonCobroDeFacturaNav();
                clsRenglonCobroDeFacturaNav vRenglonCobro = new clsRenglonCobroDeFacturaNav();
                DialogResult = vRenglonCobro.InsertChildRenglonCobroDeFactura(ConsecutivoCompania, NumeroFactura, eTipoDocumentoFactura.ComprobanteFiscal, ListDeCobro).Success;
                if (DialogResult) {
                    ImprimirFacturaFiscal(ListDeCobro);
                }
                RaiseRequestCloseEvent();
                ListDeCobroMaster.Clear();
            } catch (System.AccessViolationException) {
                throw;
            } catch (GalacException vEx) {
                if (vEx.ExceptionManagementType == eExceptionManagementType.Validation) {
                    LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.MessageBox.Warning(null, vEx.Message, "Validación de Consistencia");
                } else {
                    LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
                }
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
            }
        }

        private void RaiseIrACobroFactura() {
            var handle = IrACobroFactura;
            if (handle != null) {
                handle(this, EventArgs.Empty);
            }
        }

        #region Métodos

        public override void CalcularTotales() {
            decimal MontoCobrado;
            MontoCobrado = 0;
            if (ListDeCobro.Count > 0) {
                foreach (RenglonCobroDeFactura item in ListDeCobro) {
                    MontoCobrado = MontoCobrado + item.Monto;
                }
            }
            TotalCobrado = MontoCobrado;
            MontoRestantePorPagar = LibMath.RoundToNDecimals(TotalFactura - TotalCobrado, 2);
            CobrarCommand.RaiseCanExecuteChanged();
        }

        decimal CalcularTotalCobradoOtrosTipos(string valFormaCobro) {
            decimal MontoCobradoOtrosTipos;
            MontoCobradoOtrosTipos = 0;
            if (ListDeCobro.Count > 0) {
                foreach (RenglonCobroDeFactura item in ListDeCobro) {
                    if (item.CodigoFormaDelCobro != valFormaCobro) {
                        MontoCobradoOtrosTipos = MontoCobradoOtrosTipos + item.Monto;
                    }
                }
            }
            return MontoCobradoOtrosTipos;
        }

        decimal CalcularTotalCobradoPorTipo(string valFormaCobro) {
            decimal MontoCobradoPorFormaDeCobro;
            MontoCobradoPorFormaDeCobro = 0;
            if (ListDeCobro.Count > 0) {
                foreach (var item in ListDeCobro) {
                    if (item.CodigoFormaDelCobro == valFormaCobro) {
                        MontoCobradoPorFormaDeCobro = MontoCobradoPorFormaDeCobro + item.Monto;
                    }
                }
            }
            return MontoCobradoPorFormaDeCobro;
        }

        void ActualizarTotalesCobrado() {
            CobroDeFacturaRapidaEfectivoViewModel vViewModelEfectivo = new CobroDeFacturaRapidaEfectivoViewModel();
            Efectivo = CalcularTotalCobradoPorTipo("00001");
            Cheque = CalcularTotalCobradoPorTipo("00002");
            Tarjeta = CalcularTotalCobradoPorTipo("00003");
            DepositoTransf = CalcularTotalCobradoPorTipo("00004");
            Anticipo = CalcularTotalCobradoPorTipo("00005");
            CalcularTotales();
        }

        void ReasignarConsecutivoRenglon(List<RenglonCobroDeFactura> valListaDeCobro) {

        }

        public override void OnClosed() {
            ListDeCobroMaster.Clear();
            ListDeCobroMaster.AddRange(ListDeCobro);
        }

        #endregion

    } //End of class CobroDeFacturaRapidaViewModel

} //End of namespace Galac.Adm.Uil.Venta

