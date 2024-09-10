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
using System.Collections.ObjectModel;
using Galac.Saw.Ccl.Inventario;
using Galac.Saw.Brl.Inventario;
using LibGalac.Aos.Brl;

namespace Galac.Adm.Uil.GestionCompras.ViewModel {
    public class CompraDetalleArticuloInventarioViewModel : LibInputDetailViewModelMfc<CompraDetalleArticuloInventario> {
        #region Constantes
        public const string CodigoArticuloPropertyName = "CodigoArticulo";
        public const string DescripcionArticuloPropertyName = "DescripcionArticulo";
        public const string CantidadPropertyName = "Cantidad";
        public const string PrecioUnitarioPropertyName = "PrecioUnitario";
        public const string CantidadRecibidaPropertyName = "CantidadRecibida";
        public const string PorcentajeDeDistribucionPropertyName = "PorcentajeDeDistribucion";
        public const string MontoDistribucionPropertyName = "MontoDistribucion";
        public const string PorcentajeSeguroPropertyName = "PorcentajeSeguro";
        public const string CodigoLotePropertyName = "CodigoLote";
        public const string FechaDeElaboracionPropertyName = "FechaDeElaboracion";
        public const string FechaDeVencimientoPropertyName = "FechaDeVencimiento";
        #endregion
        #region Variables
        private FkArticuloInventarioViewModel _ConexionCodigoArticulo = null;
        private FkLoteDeInventarioViewModel _ConexionLoteDeInventario = null;
        #endregion //Variables
        #region Propiedades

        public override string ModuleName {
            get { return "Compra Detalle Articulo Inventario"; }
        }

        public int ConsecutivoCompania {
            get {
                return Model.ConsecutivoCompania;
            }
            set {
                if (Model.ConsecutivoCompania != value) {
                    Model.ConsecutivoCompania = value;
                }
            }
        }

        public int ConsecutivoCompra {
            get {
                return Model.ConsecutivoCompra;
            }
            set {
                if (Model.ConsecutivoCompra != value) {
                    Model.ConsecutivoCompra = value;
                }
            }
        }

        public int Consecutivo {
            get {
                return Model.Consecutivo;
            }
            set {
                if (Model.Consecutivo != value) {
                    Model.Consecutivo = value;
                }
            }
        }

        [LibRequired(ErrorMessage = "El campo Producto es requerido.")]
        [LibGridColum("Producto", eGridColumType.Connection, ConnectionDisplayMemberPath = "Codigo", ConnectionModelPropertyName = "CodigoArticulo", ConnectionSearchCommandName = "ChooseCodigoArticuloCommand", MaxWidth = 120)]
        public string CodigoArticulo {
            get {
                return Model.CodigoArticulo;
            }
            set {
                if (Model.CodigoArticulo != value) {
                    Model.CodigoArticulo = value;
                    IsDirty = true;
                    RaisePropertyChanged(CodigoArticuloPropertyName);
                    if (LibString.IsNullOrEmpty(CodigoArticulo, true)) {
                        ConexionCodigoArticulo = null;
                    }
                }
            }
        }


        public string DescripcionArticulo {
            get {
                return Model.DescripcionArticulo;
            }
            set {
                if (Model.DescripcionArticulo != value) {
                    Model.DescripcionArticulo = value;
                    IsDirty = true;
                    RaisePropertyChanged(DescripcionArticuloPropertyName);
                }
            }
        }

       
        [LibCustomValidation("CantidadValidating")]
        [LibGridColum("Cantidad", eGridColumType.Numeric, Alignment = eTextAlignment.Right, Width = 90, ConditionalPropertyDecimalDigits = "DecimalDigits")]
        public decimal Cantidad {
            get {
                return Model.Cantidad;
            }
            set {
                if (Model.Cantidad != value) {
                    Model.Cantidad = value;
                    IsDirty = true;
                    RaisePropertyChanged(CantidadPropertyName);
                    RaisePropertyChanged("SubTotal");
                    if (!Master.VieneDeOrdenDeCompra) {
                        CantidadRecibida = Cantidad;
                    }

                }
            }
        }

        [LibRequired(ErrorMessage = "El campo Precio Unitario es requerido.")]
        [LibGridColum("Precio Unitario", eGridColumType.Numeric, Alignment = eTextAlignment.Right, ConditionalPropertyDecimalDigits = "DecimalDigits")]
        public decimal PrecioUnitario {
            get {
                return Model.PrecioUnitario;
            }
            set {
                if (Model.PrecioUnitario != value) {
                    Model.PrecioUnitario = value;
                    IsDirty = true;
                    RaisePropertyChanged(PrecioUnitarioPropertyName);
                    RaisePropertyChanged("SubTotal");
                }
            }
        }

        [LibGridColum("Sub Total", eGridColumType.Numeric, Alignment = eTextAlignment.Right, ConditionalPropertyDecimalDigits = "DecimalDigits")]
        [LibCustomValidation ("SubTotalValidating")]
        public decimal SubTotal {
            get {

                return Model.Cantidad * Model.PrecioUnitario;
            }
            set {

            }
        }

        [LibGridColum("Cantidad Disponible", eGridColumType.Numeric, Alignment = eTextAlignment.Right, ConditionalPropertyDecimalDigits = "DecimalDigits")]
        public decimal CantidadRecibida {
            get {
                return Model.CantidadRecibida;
            }
            set {
                if (Model.CantidadRecibida != value) {
                    Model.CantidadRecibida = value;
                    IsDirty = true;
                    RaisePropertyChanged(CantidadRecibidaPropertyName);
                }
            }
        }
        [LibGridColum("Porcentaje de Distribucion", eGridColumType.Numeric, Alignment = eTextAlignment.Right, Width = 150, ConditionalPropertyDecimalDigits = "DecimalDigits")]
        public decimal PorcentajeDeDistribucion {
            get {
                return Model.PorcentajeDeDistribucion;
            }
            set {
                if (Model.PorcentajeDeDistribucion != value) {
                    Model.PorcentajeDeDistribucion = value;
                    IsDirty = true;
                    RaisePropertyChanged(PorcentajeDeDistribucionPropertyName);
                    RaisePropertyChanged("CostoUnitario");
                }
            }
        }
        [LibGridColum("Monto de Distribución", eGridColumType.Numeric, Alignment = eTextAlignment.Right, Width = 150, ConditionalPropertyDecimalDigits = "DecimalDigits")]
        public decimal MontoDistribucion {
            get {
                return Model.MontoDistribucion;
            }
            set {
                if (Model.MontoDistribucion != value) {
                    Model.MontoDistribucion = value;
                    IsDirty = true;
                    RaisePropertyChanged(MontoDistribucionPropertyName);
                    RaisePropertyChanged("CostoUnitario");
                }
            }
        }

        public decimal PorcentajeSeguro {
            get {
                return Model.PorcentajeSeguro;
            }
            set {
                if (Model.PorcentajeSeguro != value) {
                    Model.PorcentajeSeguro = value;
                    IsDirty = true;
                    RaisePropertyChanged(PorcentajeSeguroPropertyName);
                }
            }
        }

        [LibGridColum("Costo Unitario", eGridColumType.Numeric, Alignment = eTextAlignment.Right, ConditionalPropertyDecimalDigits = "DecimalDigits")]
        public decimal CostoUnitario {
            get {
                if (Master.TipoDeDistribucion == eTipoDeDistribucion.ManualPorPorcentaje) {
                    return PrecioUnitario + Master.DetailCompraDetalleGasto.Items.Where(p => p.TipoDeCosto == eTipoDeCosto.Otro).Sum(p => p.Monto) * PorcentajeDeDistribucion / 100;
                } else if (Master.TipoDeDistribucion == eTipoDeDistribucion.ManualPorMonto) {
                    if (Cantidad == 0) {
                        return 0;
                    }
                    return PrecioUnitario + (MontoDistribucion / Cantidad);
                } else if (Master.TipoDeDistribucion == eTipoDeDistribucion.Automatica) {
                    if (Cantidad == 0) {
                        return 0;
                    }
                    return Total / Cantidad;
                } else {
                    if (Cantidad == 0) {
                        return 0;
                    }
                    return SubTotal / Cantidad;
                }
            }
            set {

            }
        }

        public string CodigoArticuloInv {
            get {
                return Model.CodigoArticuloInv  ;
            }
            set {
                if (Model.CodigoArticuloInv != value) {
                    Model.CodigoArticuloInv = value;
                }
            }
        }

        public int ConsecutivoLoteDeInventario {
            get {
                return Model.ConsecutivoLoteDeInventario;
            }
            set {
                if (Model.ConsecutivoLoteDeInventario != value) {
                    Model.ConsecutivoLoteDeInventario = value;
                }
            }
        }       

        public CompraViewModel Master {
            get;
            set;
        }

        public FkArticuloInventarioViewModel ConexionCodigoArticulo {
            get {
                return _ConexionCodigoArticulo;
            }
            set {
                if (_ConexionCodigoArticulo != value) {
                    _ConexionCodigoArticulo = value;
                    RaisePropertyChanged(CodigoArticuloPropertyName);
                    RaisePropertyChanged(() => IsVisbleLoteDeInventario);
                    RaisePropertyChanged(() => IsEnabledLoteDeInventario);
                    RaisePropertyChanged(() => IsVisibleFechaLoteDeInventario);
                    RaisePropertyChanged(() => IsVisbleLabelLoteNuevo);                    
                    if (_ConexionCodigoArticulo != null) {
                        CodigoArticulo = ConexionCodigoArticulo.CodigoCompuesto;
                        DescripcionArticulo = ConexionCodigoArticulo.Descripcion;
                        PorcentajeSeguroLey = ConexionCodigoArticulo.Seguro;
                        PorcentajeArancel = ConexionCodigoArticulo.AdValorem;
                        CodigoArticuloInv = ConexionCodigoArticulo.Codigo;
                        CodigoGrupo = ConexionCodigoArticulo.CodigoGrupo;
                        TipoArticuloInv = ConexionCodigoArticulo.TipoArticuloInv;
                        CantidadMaxima = ConexionCodigoArticulo.CantidadMaxima;
                        Existencia = ConexionCodigoArticulo.Existencia;
                        TipoArticulo = ConexionCodigoArticulo.TipoDeArticulo;
                        Model.TipoDeAlicuota  = LibConvert.ToInt(ConexionCodigoArticulo.AlicuotaIva);
                        Model.TipoDeArticulo = (int)ConexionCodigoArticulo.TipoDeArticulo;
                        RaisePropertyChanged(() => TipoDeMercanciaStr);
                        CodigoLote = string.Empty;
                        FechaDeElaboracion = LibDate.MinDateForDB();
                        FechaDeVencimiento = LibDate.MaxDateForDB();
                        RaisePropertyChanged(() => CodigoLote);
                    }
                }
                if (_ConexionCodigoArticulo == null) {
                    CodigoArticulo = string.Empty;
                }
            }
        }

        public RelayCommand<string> ChooseCodigoArticuloCommand {
            get;
            private set;
        }

        public bool IsVisibleCantidadRecibida {
            get {
                return Master.VieneDeOrdenDeCompra;
            }
        }

        public bool IsVisiblePorDistribucionPorMonto {
            get {
                return Master.TipoDeDistribucion == eTipoDeDistribucion.ManualPorMonto; ;
            }
        }

        public bool IsVisiblePorDistribucionPorPorcentaje {
            get {
                return Master.TipoDeDistribucion == eTipoDeDistribucion.ManualPorPorcentaje;
            }
        }

        public bool IsVisiblePorDistribucioAutomatica {
            get {
                return Master.TipoDeCompra != eTipoCompra.Nacional && Master.TipoDeDistribucion == eTipoDeDistribucion.Automatica;
            }
        }

        public int DecimalDigits {
            get {
                return LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("Parametros", "CantidadDeDecimales");
            }
        }      

        bool _IsEnabledCantidad;
        public bool IsEnabledCantidad {
            get {
                return IsEnabled && _IsEnabledCantidad;
            }
            set {
                if (_IsEnabledCantidad != value) {
                    _IsEnabledCantidad = value;
                    IsDirty = true;
                    RaisePropertyChanged("IsEnabledCantidad");
                    if (!value) {
                        RaiseMoveFocus(PrecioUnitarioPropertyName);
                    }
                }
            }
        }

        public bool IsEnabledCodigoArticulo {
            get {
                return Master.IsEnabled;
            }
        }
       
        public string CodigoGrupo { get; set; }
        public eTipoArticuloInv TipoArticuloInv {
            get {
                return Model.TipoArticuloInv;
            }
            set {
                if (Model.TipoArticuloInv != value) {
                    Model.TipoArticuloInv = value;
                }
            }
        }

        public bool IsVisibleUsaSeguro {
            get {
                return IsVisiblePorDistribucioAutomatica && Master.UsaSeguro;
            }
        }

        public decimal PorcentajeSeguroLey { get; set; }

        public decimal PorcentajDistribucionFOB {
            get {
                decimal vSumSubTotal = Master.DetailCompraDetalleArticuloInventario.Items.Sum(p => p.SubTotal);
                if (vSumSubTotal > 0) {
                    return SubTotal * 100 / Master.DetailCompraDetalleArticuloInventario.Items.Sum(p => p.SubTotal);
                }
                return 0;
            }
        }

        public decimal TotalFlete {
            get {
                return PorcentajDistribucionFOB * Master.DetailCompraDetalleGasto.Items.Where(p => p.TipoDeCosto == eTipoDeCosto.FleteInternacional).Sum(p => p.Monto) / 100;
            }
        }

        public decimal TotalGastosSeguro {
            get {
                decimal vTotalGastosSeguro = PorcentajDistribucionFOB * Master.DetailCompraDetalleGasto.Items.Where(p => p.TipoDeCosto == eTipoDeCosto.Seguro).Sum(p => p.Monto) / 100;
                return LibMath.RoundToNDecimals(vTotalGastosSeguro, 2);
            }
        }

        public decimal ValorSeguro {
            get {
                if (PorcentajeSeguroLey > PorcentajeSeguro) {
                    return PorcentajeSeguroLey;
                } else {
                    return PorcentajeSeguro;
                }
            }
        }

        [LibGridColum("Seguro Pagado *", eGridColumType.Numeric, Alignment = eTextAlignment.Right, ConditionalPropertyDecimalDigits = "DecimalDigits")]
        public decimal SeguroPagado {
            get {
                return (SubTotal * (ValorSeguro / 100)) + TotalGastosSeguro;
            }
        }

        public decimal ValorCIF {
            get {
                return SubTotal + TotalFlete + SeguroPagado;
            }
        }

        public decimal PorcentajeArancel { get; set; }
        public int AlicuotaIVA { get; set; }

        public decimal Impuesto {
            get {
                return PorcentajeArancel * ValorCIF / 100;
            }
        }

        public decimal TasaGastoAduanero {
            get {
                return CalculaTasaAduanera() * PorcentajDistribucionFOB / 100;
            }
        }

        public decimal TotalOtrosGastos {
            get {
                return Master.DetailCompraDetalleGasto.Items.Where(p => p.TipoDeCosto == eTipoDeCosto.Otro).Sum(p => p.Monto) * PorcentajDistribucionFOB / 100;
            }
        }

        public decimal Total {
            get {
                return ValorCIF + Impuesto + TotalOtrosGastos;
            }
        }

        public decimal Existencia { get; set; }
        public eTipoDeArticulo TipoArticulo { get; set; }
        public decimal CantidadMaxima { get; set; }

        [LibCustomValidation("LoteDeInventarioValidating")]        
        public string CodigoLote {
            get {
                return Model.CodigoLote;
            }
            set {
                if (Model.CodigoLote != value) {
                    Model.CodigoLote = value;
                    IsDirty = true;
                    RaisePropertyChanged(CodigoLotePropertyName);
                }
            }
        }        
        public DateTime FechaDeElaboracion {
            get {
                return Model.FechaDeElaboracion;
            }
            set {
                if (Model.FechaDeElaboracion != value) {
                    Model.FechaDeElaboracion = value;
                    IsDirty = true;
                    RaisePropertyChanged(FechaDeElaboracionPropertyName);
                }
            }
        }        
        public DateTime FechaDeVencimiento {
            get {
                return Model.FechaDeVencimiento;
            }
            set {
                if (Model.FechaDeVencimiento != value) {
                    Model.FechaDeVencimiento = value;
                    IsDirty = true;
                    RaisePropertyChanged(FechaDeVencimientoPropertyName);
                }
            }
        }

        public string TipoDeMercanciaStr {
            get { return LibEnumHelper.GetDescription(TipoArticuloInv); }
        }

        public bool IsVisbleLoteDeInventario {
            get { return (ConexionCodigoArticulo != null) && (ConexionCodigoArticulo.TipoDeArticulo == eTipoDeArticulo.Mercancia) && (ConexionCodigoArticulo.TipoArticuloInv == eTipoArticuloInv.LoteFechadeVencimiento || ConexionCodigoArticulo.TipoArticuloInv == eTipoArticuloInv.Lote); }
        }

        public bool IsEnabledLoteDeInventario {
            get { return IsEnabled && SePuedeEditarLote(); }
        }

        public bool IsVisibleFechaLoteDeInventario {
            get { return (ConexionCodigoArticulo != null) && (ConexionCodigoArticulo.TipoDeArticulo == eTipoDeArticulo.Mercancia) && (ConexionCodigoArticulo.TipoArticuloInv == eTipoArticuloInv.LoteFechadeVencimiento); }
        }

        public bool IsVisbleLabelLoteNuevo {
            get { return (ConexionCodigoArticulo != null); }
        }
        public RelayCommand<string> ChooseLoteDeInventarioCommand {
            get;
            private set;
        }

        public FkLoteDeInventarioViewModel ConexionLoteDeInventario {
            get {
                return _ConexionLoteDeInventario;
            }
            set {
                if (_ConexionLoteDeInventario != value) {
                    _ConexionLoteDeInventario = value;
                    RaisePropertyChanged(CodigoLotePropertyName);
                }
            }
        }

        #endregion //Propiedades
        #region Constructores
        public CompraDetalleArticuloInventarioViewModel()
            : base(new CompraDetalleArticuloInventario(), eAccionSR.Insertar, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
        }
        public CompraDetalleArticuloInventarioViewModel(CompraViewModel initMaster, CompraDetalleArticuloInventario initModel, eAccionSR initAction)
            : base(initModel, initAction, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            Master = initMaster;
            IsEnabledCantidad = initModel.TipoArticuloInv == eTipoArticuloInv.Simple || initModel.TipoArticuloInv == eTipoArticuloInv.UsaTallaColor;
            TipoArticuloInv = initModel.TipoArticuloInv;
            CodigoGrupo = initModel.TipoArticuloInv == eTipoArticuloInv.UsaSerial || initModel.TipoArticuloInv == eTipoArticuloInv.UsaSerialRollo ? "0" : initModel.CodigoGrupo;
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override void InitializeLookAndFeel(CompraDetalleArticuloInventario valModel) {
            base.InitializeLookAndFeel(valModel);
        }

        protected override ILibBusinessDetailComponent<IList<CompraDetalleArticuloInventario>, IList<CompraDetalleArticuloInventario>> GetBusinessComponent() {
            return new clsCompraDetalleArticuloInventarioNav();
        }

        protected override void InitializeCommands() {
            base.InitializeCommands();
            ChooseCodigoArticuloCommand = new RelayCommand<string>(ExecuteChooseCodigoArticuloCommand);
            ChooseLoteDeInventarioCommand = new RelayCommand<string>(ExecuteChooseLoteDeInventarioCommand);
        }

        protected override void ReloadRelatedConnections() {
            base.ReloadRelatedConnections();
            //ConexionCodigoArticulo = Master.FirstConnectionRecordOrDefault<FkArticuloInventarioViewModel>("Articulo Inventario", LibSearchCriteria.CreateCriteria("CodigoCompuesto", CodigoArticulo));         
            LibSearchCriteria vDefaultCriteriaInventario = LibSearchCriteria.CreateCriteria("Codigo", CodigoArticulo);
            vDefaultCriteriaInventario.Add(LibSearchCriteria.CreateCriteria("ConsecutivoCompania", Mfc.GetInt("Compania")), eLogicOperatorType.And);
            ConexionCodigoArticulo = Master.FirstConnectionRecordOrDefault<FkArticuloInventarioViewModel>("Articulo Inventario", vDefaultCriteriaInventario);
            LibSearchCriteria vDefaultCriteriaLote = LibSearchCriteria.CreateCriteria("CodigoLote", CodigoLote);
            vDefaultCriteriaLote.Add(LibSearchCriteria.CreateCriteria("ConsecutivoCompania", Mfc.GetInt("Compania")), eLogicOperatorType.And);
            ConexionLoteDeInventario = Master.FirstConnectionRecordOrDefault<FkLoteDeInventarioViewModel>("Lote de Inventario", vDefaultCriteriaLote);
        }
        private void ExecuteChooseCodigoArticuloCommand(string valCodigo) {
            bool vAplicaProductoTerminado = false;
            //clsArticuloInventarioED clsArticuloInventarioED = new clsArticuloInventarioED();
            //clsArticuloInventarioED.BorrarVistasYSps();
            //clsArticuloInventarioED.InstalarVistasYSps();
            try {
                if(valCodigo == null) {
                    valCodigo = string.Empty;
                }

                vAplicaProductoTerminado = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Compania","ProductoTerminado");
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("CodigoCompuesto",valCodigo);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("ConsecutivoCompania",Mfc.GetInt("Compania"));
                vFixedCriteria.Add(LibSearchCriteria.CreateCriteria("StatusdelArticulo ","0"),eLogicOperatorType.And);
                vFixedCriteria.Add("TipoDeArticulo",eBooleanOperatorType.IdentityInequality,"2",eLogicOperatorType.And);
                if(vAplicaProductoTerminado) {
                    //vFixedCriteria.Add(LibSearchCriteria.CreateCriteria("TipoDeMercanciaProduccion", eTipoDeMercancia.NoAplica), eLogicOperatorType.Or);
                    //vFixedCriteria.Add(LibSearchCriteria.CreateCriteria("TipoDeMercanciaProduccion", eTipoDeMercancia.MateriaPrima), eLogicOperatorType.And);
                    vFixedCriteria.Add("TipoDeMercanciaProduccion",eBooleanOperatorType.IdentityInequality,LibConvert.EnumToDbValue((int)eTipoDeMercancia.ProductoTerminado));
                }
                ConexionCodigoArticulo = Master.ChooseRecord<FkArticuloInventarioViewModel>("Articulo Inventario",vDefaultCriteria,vFixedCriteria,string.Empty);

                if(ConexionCodigoArticulo != null) {
                    if(LibDefGen.ProgramInfo.IsCountryPeru()) {
                        PrecioUnitario = ConexionCodigoArticulo.CostoUnitario;
                    }
                    if(ConexionCodigoArticulo.TipoArticuloInv == Saw.Ccl.Inventario.eTipoArticuloInv.UsaSerial ||
                        ConexionCodigoArticulo.TipoArticuloInv == Saw.Ccl.Inventario.eTipoArticuloInv.UsaSerialRollo ||
                        ConexionCodigoArticulo.TipoArticuloInv == Saw.Ccl.Inventario.eTipoArticuloInv.UsaTallaColorySerial) {
                        if(ConexionCodigoArticulo.TipoArticuloInv == Saw.Ccl.Inventario.eTipoArticuloInv.UsaSerial ||
                            ConexionCodigoArticulo.TipoArticuloInv == Saw.Ccl.Inventario.eTipoArticuloInv.UsaSerialRollo) {
                            ConexionCodigoArticulo.CodigoGrupo = "0";
                        }
                        if(BuscarCodigoRepetidoEnElGrid(ConexionCodigoArticulo.Codigo)) {
                            Master.DetailCompraDetalleArticuloInventario.QuitarArticuloConSerialRepetido();
                            return;
                        } else {
                            BuscarSerialLote(ConexionCodigoArticulo.Codigo,ConexionCodigoArticulo.CodigoGrupo,ConexionCodigoArticulo.CodigoCompuesto,ConexionCodigoArticulo.TipoArticuloInv,false);

                        }
                        IsEnabledCantidad = false;
                    } else {
                        IsEnabledCantidad = true;
                    }
                } else {
                    CodigoArticulo = string.Empty;
                    DescripcionArticulo = string.Empty;
                    PorcentajeSeguroLey = 0;
                    PorcentajeArancel = 0;

                }
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx,ModuleName);
            }
        }

        internal void ActualizaCostoUnitario() {
            Model.CostoUnitario = CostoUnitario;
            RaisePropertyChanged("CostoUnitario");
        }

        decimal CalculaTasaAduanera() {
            if (Master.TasaDolar > 0) {
                return 1 * Master.GetModel().ValorUT * Master.TasaAduanera / Master.TasaDolar;
            } else {
                return 1;
            }
        }

        
        #endregion //Metodos Generados
        private bool BuscarCodigoRepetidoEnElGrid(string valCodigo) {
            return Master.BuscarCodigoRepetidoEnElGrid(valCodigo);
        }

        internal void BuscarSerialLote(string valCodigo, string valCodigoGrupo, string valCodigoComp, eTipoArticuloInv valTipoArticuloInv, bool valSolicitaSeriales) {
            ICompraPdn vCompra = new clsCompraNav();
            XElement vData = vCompra.BuscarSerial(Master.ConsecutivoCompania, valCodigoComp, valCodigoGrupo);
            string vCodigoTalla = string.Empty;
            string vDescripcionTalla = string.Empty;
            string vCodigoColor = string.Empty;
            string vDescripcionColor = string.Empty;
            if (vData != null && vData.HasElements) {
                vCodigoColor = LibXml.GetPropertyString(vData, "CodigoColor");
                vDescripcionColor = LibXml.GetPropertyString(vData, "DescripcionColor");
                vCodigoTalla = LibXml.GetPropertyString(vData, "CodigoTalla");
                vDescripcionTalla = LibXml.GetPropertyString(vData, "DescripcionTalla");
            }

            List<CompraDetalleSerialRolloViewModel> vList = new List<ViewModel.CompraDetalleSerialRolloViewModel>();

            if (ExisteArticuloEnRenglonCompraXSerial(valCodigoComp, valTipoArticuloInv)) {
                vList = Master.DetailCompraDetalleSerialRollo.Items.Where(p => p.CodigoArticulo == valCodigoComp || p.CodigoArticulo == valCodigo).ToList();
            }
            if (!valSolicitaSeriales && vList != null && vList.Count > 0) {
                return;
            }

            CompraSerialRolloViewModel vViewModelCompraSerialRollo = new CompraSerialRolloViewModel(new ObservableCollection<CompraDetalleSerialRolloViewModel>(vList), valTipoArticuloInv);
            vViewModelCompraSerialRollo.Color = vDescripcionColor;
            vViewModelCompraSerialRollo.Talla = vDescripcionTalla;
            Master.DetailCompraDetalleSerialRollo.TipoArticuloInventario = valTipoArticuloInv;
            bool result = LibMessages.EditViewModel.ShowEditor(vViewModelCompraSerialRollo, true);
            if (vViewModelCompraSerialRollo.DialogResult) {
                foreach (CompraDetalleSerialRolloViewModel item in vList) {
                    Master.DetailCompraDetalleSerialRollo.Items.Remove(item);
                }
                foreach (CompraDetalleSerialRolloViewModel item in vViewModelCompraSerialRollo.DetailCompraDetalleSerialRollo.Items) {
                    Master.DetailCompraDetalleSerialRollo.Items.Add(new ViewModel.CompraDetalleSerialRolloViewModel() { Cantidad = item.Cantidad, Rollo = item.Rollo, Serial = item.Serial, CodigoArticulo = valCodigoComp });
                }
                if (valTipoArticuloInv == eTipoArticuloInv.UsaSerial) {
                    Cantidad = vViewModelCompraSerialRollo.DetailCompraDetalleSerialRollo.Items.Count;
                } else if (valTipoArticuloInv == eTipoArticuloInv.UsaTallaColorySerial || valTipoArticuloInv == eTipoArticuloInv.UsaSerialRollo) {
                    Cantidad = vViewModelCompraSerialRollo.DetailCompraDetalleSerialRollo.Items.Sum(p => p.Cantidad);
                }
            }
        }

        private bool ExisteArticuloEnRenglonCompraXSerial(string valCodigoComp, eTipoArticuloInv valTipoArticuloInv) {
            if (valTipoArticuloInv == eTipoArticuloInv.UsaTallaColorySerial || valTipoArticuloInv == eTipoArticuloInv.UsaSerialRollo || valTipoArticuloInv == eTipoArticuloInv.UsaSerial) {                
                return true;
            }
            return false;
        }

        internal void RaiseVisiblePorTipoDeDistribucion() {
            RaisePropertyChanged("IsVisiblePorDistribucioAutomatica");
            RaisePropertyChanged("IsVisiblePorDistribucionPorPorcentaje");
            RaisePropertyChanged("IsVisiblePorDistribucionPorMonto");
        }

        private ValidationResult CantidadValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if ((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else {
                if (Cantidad == 0) {
                    vResult = new ValidationResult("El campo Cantidad es requerido.");                     
                }
                if (Master.VieneDeOrdenDeCompra && Cantidad > CantidadRecibida) {
                    vResult = new ValidationResult("La Cantidad debe ser menor a la Cantidad Disponible.");
                }
                if (Action == eAccionSR.Insertar) {
                    if (TipoArticulo != eTipoDeArticulo.Servicio  && LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "VerificarStock") &&  Existencia + Cantidad > CantidadMaxima) {
                        LibMessages.MessageBox.Alert(this, "Con esta operación usted dejará las catidades por arriba del Stock Máximo", "Stock");
                    }
                }
            }
            return vResult;
        }

        internal void RaiseVisibleUsaSeguro() {
            RaisePropertyChanged("IsVisibleUsaSeguro");
        }

        private ValidationResult SubTotalValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if ((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else {
                if (SubTotal == 0) {
                    vResult = new ValidationResult("No se puede grabar una Compra con monto cero.");
                }
            }
            return vResult;
        }

        private void ExecuteChooseLoteDeInventarioCommand(string valCodigoLote) {
            try {
                if (valCodigoLote == null) {
                    valCodigoLote = string.Empty;
                }
                bool vInvocarCrear = true;
                vInvocarCrear = vInvocarCrear && !LibString.IsNullOrEmpty(valCodigoLote, true);
                vInvocarCrear = vInvocarCrear && !LibString.S1IsInS2("*", valCodigoLote);
                vInvocarCrear = vInvocarCrear && !((ILoteDeInventarioPdn)new clsLoteDeInventarioNav()).ExisteLoteDeInventario(Mfc.GetInt("Compania"), CodigoArticulo, valCodigoLote);
                if (vInvocarCrear) {
                    LibBusinessProcessMessage libBusinessProcessMessage = new LibBusinessProcessMessage();
                    libBusinessProcessMessage.Content = valCodigoLote + "|" + CodigoArticulo + "|" + (int)TipoArticuloInv;
                    LibBusinessProcess.Call("InsertarLoteInventarioDesdeModuloExterno", libBusinessProcessMessage);
                    valCodigoLote = libBusinessProcessMessage.Result.ToString();
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("CodigoLote", valCodigoLote);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("ConsecutivoCompania", Mfc.GetInt("Compania"));
                vFixedCriteria.Add(LibSearchCriteria.CreateCriteria("CodigoArticulo", CodigoArticulo), eLogicOperatorType.And);
                ConexionLoteDeInventario = Master.ChooseRecord<FkLoteDeInventarioViewModel>("Lote de Inventario", vDefaultCriteria, vFixedCriteria, "FechaDeVencimiento, FechaDeElaboracion, CodigoLote");
                if (ConexionLoteDeInventario == null) {
                    CodigoLote = string.Empty;
                    FechaDeElaboracion = LibDate.MinDateForDB();
                    FechaDeVencimiento = LibDate.MaxDateForDB();
                } else {
                    ConsecutivoLoteDeInventario = ConexionLoteDeInventario.Consecutivo;
                    CodigoLote = ConexionLoteDeInventario.CodigoLote;
                    FechaDeElaboracion = ConexionLoteDeInventario.FechaDeElaboracion;
                    FechaDeVencimiento = ConexionLoteDeInventario.FechaDeVencimiento;
                }
                RaisePropertyChanged(() => IsVisbleLoteDeInventario);
                RaisePropertyChanged(() => IsEnabledLoteDeInventario);
                RaisePropertyChanged(() => IsVisibleFechaLoteDeInventario);
                RaisePropertyChanged(() => IsVisbleLabelLoteNuevo);
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private bool SePuedeEditarLote() {
            if ((Action == eAccionSR.Insertar)
                && (ConexionCodigoArticulo != null)
                && (ConexionCodigoArticulo.TipoDeArticulo == eTipoDeArticulo.Mercancia)
                && (ConexionCodigoArticulo.TipoArticuloInv == eTipoArticuloInv.LoteFechadeVencimiento || ConexionCodigoArticulo.TipoArticuloInv == eTipoArticuloInv.Lote)) {
                return true;
            } else {
                return false;
            }
        }

        private ValidationResult LoteDeInventarioValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if ((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else if (TipoArticuloInv == eTipoArticuloInv.LoteFechadeVencimiento || TipoArticuloInv == eTipoArticuloInv.Lote) {
                if (LibString.IsNullOrEmpty(CodigoLote, true)) {
                    vResult = new ValidationResult("El Lote de Inventario no fue ingresado.");                
                }
            } else if (TipoArticuloInv != eTipoArticuloInv.LoteFechadeVencimiento || TipoArticuloInv != eTipoArticuloInv.Lote) {
                CodigoLote = string.Empty;
                FechaDeElaboracion = LibDate.MinDateForDB();
                FechaDeVencimiento = LibDate.MaxDateForDB();
            }
            return vResult;
        }
    } //End of class CompraDetalleArticuloInventarioViewModel

} //End of namespace Galac.Adm.Uil.GestionCompras

