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
using Galac.Adm.Brl.Venta;
using Galac.Adm.Ccl.Venta;
using Galac.Saw.Ccl.SttDef;
using Galac.Saw.Ccl.Inventario;
using System.Threading;
using System.Text;
using Galac.Saw.Ccl.Vendedor;
using Galac.Saw.Brl.Vendedor;
using Galac.Saw.Brl.Inventario;
using Galac.Saw.Ccl.Cliente;
using Galac.Adm.Ccl.DispositivosExternos;
using Galac.Comun.Ccl.Impuesto;
using Galac.Comun.Brl.Impuesto;
using Galac.Adm.Uil.DispositivosExternos.ViewModel;
using Galac.Saw.Uil.Inventario.ViewModel;
using Galac.Saw.Uil.Inventario.Views;
using Galac.Saw.Lib;
using Galac.Comun.Ccl.TablasGen;
using Galac.Comun.Brl.TablasGen;
using Galac.Comun.Uil.TablasGen.ViewModel;
using Galac.Saw.Lib.Uil;

namespace Galac.Adm.Uil.Venta.ViewModel {
    public class FacturaRapidaViewModel:LibInputMasterViewModelMfc<FacturaRapida> {

        #region Constantes

        private const string NumeroPropertyName = "Numero";
        private const string FechaPropertyName = "Fecha";
        private const string NumeroRIFPropertyName = "NumeroRIF";
        private const string NombreClientePropertyName = "NombreCliente";
        private const string DireccionClientePropertyName = "DireccionCliente";
        private const string TelefonoClientePropertyName = "TelefonoCliente";
        private const string CodigoVendedorPropertyName = "CodigoVendedor";
        private const string NombreVendedorPropertyName = "NombreVendedor";
        private const string TotalMontoExentoPropertyName = "TotalMontoExento";
        private const string TotalBaseImponiblePropertyName = "TotalBaseImponible";
        private const string TotalRenglonesPropertyName = "TotalRenglones";
        private const string TotalIVAPropertyName = "TotalIVA";
        private const string TotalFacturaPropertyName = "TotalFactura";
        private const string PorcentajeDescuentoPropertyName = "PorcentajeDescuento";
        private const string StatusFacturaPropertyName = "StatusFactura";
        private const string TipoDeDocumentoPropertyName = "TipoDeDocumento";
        private const string CambioABolivaresPropertyName = "CambioABolivares";
        private const string MontoDelAbonoPropertyName = "MontoDelAbono";
        private const string TipoDeVentaPropertyName = "TipoDeVenta";
        private const string MontoIvaRetenidoPropertyName = "MontoIvaRetenido";
        private const string PorcentajeAlicuota1PropertyName = "PorcentajeAlicuota1";
        private const string PorcentajeAlicuota2PropertyName = "PorcentajeAlicuota2";
        private const string PorcentajeAlicuota3PropertyName = "PorcentajeAlicuota3";
        private const string MontoIvaAlicuota2PropertyName = "MontoIvaAlicuota2";
        private const string MontoIvaAlicuota1PropertyName = "MontoIvaAlicuota1";
        private const string MontoIvaAlicuota3PropertyName = "MontoIvaAlicuota3";
        private const string MontoGravableAlicuota1PropertyName = "MontoGravableAlicuota1";
        private const string MontoGravableAlicuota2PropertyName = "MontoGravableAlicuota2";
        private const string MontoGravableAlicuota3PropertyName = "MontoGravableAlicuota3";
        private const string ConsecutivoAlmacenPropertyName = "ConsecutivoAlmacen";
        private const string MontoDescuentoPropertyName = "MontoDescuento";
        private const string PorcentajeDeLaInicialPropertyName = "PorcentajeDeLaInicial";
        private const string NumeroDeCuotasPropertyName = "NumeroDeCuotas";
        private const string NumDiasDeVencimiento1aCuotaPropertyName = "NumDiasDeVencimiento1aCuota";
        private const string UsaMaquinaFiscalPropertyName = "UsaMaquinaFiscal";
        private const string FacturaConPreciosSinIvaPropertyName = "FacturaConPreciosSinIva";
        private const string CodigoMonedaPropertyName = "CodigoMoneda";
        private const string AplicaDecretoIvaEspecialPropertyName = "AplicaDecretoIvaEspecial";
        private const string EsGeneradaPorPuntoDeVentaPropertyName = "EsGeneradaPorPuntoDeVenta";
        private const string NombreOperadorPropertyName = "NombreOperador";
        private const string FechaUltimaModificacionPropertyName = "FechaUltimaModificacion";
        private const string SeccionFacturaRapidaDetallePropertyName = "SeccionFacturaRapidaDetalle";
        private const string InfoCliente1PropertyName = "InfoCliente1";
        private const string InfoCliente2PropertyName = "InfoCliente2";
        private const string InfoCliente3PropertyName = "InfoCliente3";
        private const string InfoArticulo1PropertyName = "InfoArticulo1";
        private const string InfoArticulo2PropertyName = "InfoArticulo2";
        private const string InfoArticulo3PropertyName = "InfoArticulo3";
        private const string NombreCajeroPropertyName = "NombreCajero";
        private const string ArticuloPropertyName = "Articulo";
        private const string DescripcionPropertyName = "Descripcion";
        private const string DescripcionCortaPropertyName = "DescripcionCorta";
        private const string CantidadPropertyName = "Cantidad";
        private const string DetailFacturaRapidaDetallePropertyName = "DetailFacturaRapidaDetalle";
        private const string TotalDeItemsPropertyName = "TotalDeItems";
        private const string ConexionArticuloPropertyName = "ConexionArticulo";
        private const string BackgroundDecretoIvaEspecialPropertyName = "BackgroundDecretoIvaEspecial";
        private const string IsVisibleAplicaDecretoIvaEspecialPropertyName = "IsVisibleAplicaDecretoIvaEspecial";
        private const string ObservacionesPropertyName = "Observaciones";
        private const decimal MontoMaximoDecreto2602 = 200000000;
        private const string TotalFacturaDivisasPropertyName = "TotalFacturaDivisas";
        private const string CambioMostrarTotalEnDivisasPropertyName = "CambioMostrarTotalEnDivisas";
        private const string CodigoMonedaDeCobroPropertyName = "CodigoMonedaDeCobro";
        private const string NombreMonedaDeCobroPropertyName = "NombreMonedaDeCobro";
        private const string MostrarTasaDeCambioPropertyName = "MostrarTasaDeCambio";
        private const string SimboloMonedaParaTotalEnDivisasPropertyName = "SimboloMonedaParaTotalEnDivisas";
        private const string CuadroDeBusquedaDeArticulosViewModelPropertyName = "CuadroDeBusquedaDeArticulosViewModel";
        private const string CuadroDeBusquedaDeClientesViewModelPropertyName = "CuadroDeBusquedaDeClientesViewModel";
        private const string TotalDescuentoPropertyName = "TotalDescuento";
        private const string SimboloMonedaDeFacturaPropertyName = "SimboloMonedaDeFactura";
        #endregion

        #region Variables
        private FkClienteViewModel _ConexionCliente = null;
        private FkVendedorViewModel _ConexionCodigoVendedor = null;
        private FkVendedorViewModel _ConexionNombreVendedor = null;
        private FkArticuloInventarioViewModel _ConexionArticulo = null;
        private FkArticuloInventarioViewModel _ConexionDescripcion = null;
        private FkMonedaViewModel _ConexionCodigoMonedaDeCobro = null;
        private FkMonedaViewModel _ConexionNombreMonedaDeCobro = null;
        private clsNoComunSaw _clsNoComun = null;

        private string _InfoCliente1;
        private string _InfoCliente2;
        private string _InfoCliente3;
        private string _InfoArticulo1;
        private string _InfoArticulo2;
        private string _InfoArticulo3;
        private string _NombreCajero;
        private bool _UsaPesoEnCodigo = false;
        private bool _UsaPrecioEnCodigo = false;
        private bool _EtiquetaIncluyeIva = false;
        private int NumeroDeLineas;
        private bool EsFacturaEnEspera;
        private XElement _XmlDatosImprFiscal;
        private bool FacturaAplicaIvaEspecial;
        private int EmpresaAplicaIVAEspecial;
        private bool EmpresaUsaPrecioSinIva;
        private List<RenglonCobroDeFactura> ListDeCobroMaster = new List<RenglonCobroDeFactura>();
        public int _CantidadDeDecimales;
        private decimal _PorcentajeAlicuotaEspecial;
        private bool _SustituyeAlicuota;
        private int _AlicuotaIvaASustituir;
        private int _AplicarPorContribuyente;
        private bool _UsaBalanzaEnPOS = false;
        private decimal totalFacturaDivisas;
        private BalanzaTomarPesoViewModel _BalanzaTomarPesoViewModel;
        private bool _MostrarTasaDeCambio;
        private bool vResultCobro = false;
        private string _SimboloMonedaParaTotalEnDivisas;
        private bool _UsaListaDePrecioEnMonedaExtranjera = false;
        private bool _MostrarPorcentajeDescuento;
        private decimal totalDescuento;
        private string _SimboloMonedaDeFactura;


        #endregion //Variables

        #region Propiedades
        public ISearchBoxViewModel CuadroDeBusquedaDeArticulosViewModel { get; set; }
        public ISearchBoxViewModel CuadroDeBusquedaDeClientesViewModel { get; set; }

        public override string ModuleName {
            get {
                return "Punto de Venta";
            }
        }

        public int ConsecutivoCompania {
            get {
                return Model.ConsecutivoCompania;
            }
            set {
                if(Model.ConsecutivoCompania != value) {
                    Model.ConsecutivoCompania = value;
                }
            }
        }

        public string Numero {
            get {
                return Model.Numero;
            }
            set {
                if(Model.Numero != value) {
                    Model.Numero = value;
                    IsDirty = true;
                    RaisePropertyChanged(NumeroPropertyName);
                }
            }
        }

        //[LibCustomValidation("FechaValidating")]
        [LibGridColum("Fecha",eGridColumType.DatePicker)]
        public DateTime Fecha {
            get {
                return Model.Fecha;
            }
            set {
                if(Model.Fecha != value) {
                    Model.Fecha = value;
                    IsDirty = true;
                    RaisePropertyChanged(FechaPropertyName);
                }
            }
        }

        public string CodigoCliente {
            get {
                return Model.CodigoCliente;
            }
            set {
                if(Model.CodigoCliente != value) {
                    Model.CodigoCliente = value;
                }
            }
        }

        [LibRequired(ErrorMessage = "Debe ingresar la cédula.")]
        public string NumeroRIF {
            get {
                return Model.NumeroRIF;
            }
            set {
                if(Model.NumeroRIF != value) {
                    Model.NumeroRIF = value;
                    IsDirty = true;
                    RaisePropertyChanged(NumeroRIFPropertyName);
                    if(LibString.IsNullOrEmpty(NumeroRIF,true)) {
                        ConexionCliente = null;
                    }
                }
            }
        }

        [LibRequired(ErrorMessage = "El campo Nombre Cliente es requerido.")]
        [LibGridColum("Nombre Cliente",eGridColumType.Connection,ConnectionSearchCommandName = "ChooseNombreClienteCommand")]
        public string NombreCliente {
            get {
                return Model.NombreCliente;
            }
            set {
                if(Model.NombreCliente != value) {
                    Model.NombreCliente = value;
                    IsDirty = true;
                    RaisePropertyChanged(NombreClientePropertyName);
                    if(LibString.IsNullOrEmpty(NombreCliente,true)) {
                        ConexionCliente = null;
                    }
                }
            }
        }

        public string DireccionCliente {
            get {
                return Model.DireccionCliente;
            }
            set {
                if(Model.DireccionCliente != value) {
                    Model.DireccionCliente = value;
                    IsDirty = true;
                    RaisePropertyChanged(DireccionClientePropertyName);
                }
            }
        }

        public string TelefonoCliente {
            get {
                return Model.TelefonoCliente;
            }
            set {
                if(Model.TelefonoCliente != value) {
                    Model.TelefonoCliente = value;
                    IsDirty = true;
                    RaisePropertyChanged(TelefonoClientePropertyName);
                }
            }
        }

        public string CodigoVendedor {
            get {
                return Model.CodigoVendedor;
            }
            set {
                if(Model.CodigoVendedor != value) {
                    Model.CodigoVendedor = value;
                    IsDirty = true;
                    RaisePropertyChanged(CodigoVendedorPropertyName);
                    if(LibString.IsNullOrEmpty(CodigoVendedor,true)) {
                        ConexionCodigoVendedor = null;
                    }
                }
            }
        }

        [LibRequired(ErrorMessage = "El campo Nombre del Vendedor es requerido.")]
        public string NombreVendedor {
            get {
                return Model.NombreVendedor;
            }
            set {
                if(Model.NombreVendedor != value) {
                    Model.NombreVendedor = value;
                    IsDirty = true;
                    RaisePropertyChanged(NombreVendedorPropertyName);
                    if(LibString.IsNullOrEmpty(NombreVendedor,true)) {
                        ConexionNombreVendedor = null;
                    }
                }
            }
        }

        public decimal TotalMontoExento {
            get {
                return Model.TotalMontoExento;
            }
            set {
                if(Model.TotalMontoExento != value) {
                    Model.TotalMontoExento = value;
                    IsDirty = true;
                    RaisePropertyChanged(TotalMontoExentoPropertyName);
                }
            }
        }

        public decimal TotalBaseImponible {
            get {
                return Model.TotalBaseImponible;
            }
            set {
                if(Model.TotalBaseImponible != value) {
                    Model.TotalBaseImponible = value;
                    IsDirty = true;
                    RaisePropertyChanged(TotalBaseImponiblePropertyName);
                }
            }
        }

        public decimal TotalRenglones {
            get {
                return Model.TotalRenglones;
            }
            set {
                if(Model.TotalRenglones != value) {
                    Model.TotalRenglones = value;
                    IsDirty = true;
                    RaisePropertyChanged(TotalRenglonesPropertyName);
                }
            }
        }

        public decimal TotalIVA {
            get {
                return Model.TotalIVA;
            }
            set {
                if(Model.TotalIVA != value) {
                    Model.TotalIVA = value;
                    IsDirty = true;
                    RaisePropertyChanged(TotalIVAPropertyName);
                }
            }
        }

        public bool SeMuestraTotalEnDivisas {
            get { return EsPosibleMostrarTotalEnDivisas(); }
        }

        [LibGridColum("Total Factura",eGridColumType.Numeric)]
        public decimal TotalFactura {
            get {
                return Model.TotalFactura;
            }
            set {
                if(Model.TotalFactura != value) {
                    Model.TotalFactura = value;
                    IsDirty = true;
                    CalcularTotalFacturaDivisas();
                    RaisePropertyChanged(TotalFacturaPropertyName);
                }
            }
        }

        private void CalcularTotalFacturaDivisas() {
            if(CambioMostrarTotalEnDivisas != 0) {
                TotalFacturaDivisas = LibMath.RoundToNDecimals(TotalFactura / CambioMostrarTotalEnDivisas,2);
            } else {
                TotalFacturaDivisas = 0;
            }
        }

        private void CalcularTotalDescuento() {
            decimal totales = Model.TotalRenglones;
            if(totales > 0) {
                TotalDescuento = totales * (Model.PorcentajeDescuento / 100);
            } else {
                TotalDescuento = 0;
            }
            Model.MontoDescuento1 = TotalDescuento;
            RaisePropertyChanged(TotalDescuentoPropertyName);
        }

        public decimal TotalDescuento {
            get {
                return totalDescuento;
            }
            set {
                if(totalDescuento != value) {
                    totalDescuento = value;
                    RaisePropertyChanged(TotalDescuentoPropertyName);
                }
            }
        }

        public decimal TotalFacturaDivisas {
            get { return totalFacturaDivisas; }
            set {
                if(totalFacturaDivisas != value) {
                    totalFacturaDivisas = value;
                    RaisePropertyChanged(TotalFacturaDivisasPropertyName);
                }
            }
        }

        public decimal PorcentajeDescuento {
            get {
                return Model.PorcentajeDescuento;
            }
            set {
                if(Model.PorcentajeDescuento != value) {
                    decimal DescuentoDesdeParametros = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetDecimal("FacturaRapida","MaximoDescuentoEnFactura");
                    Model.PorcentajeDescuento = value > DescuentoDesdeParametros ? DescuentoDesdeParametros : value;
                    Model.PorcentajeDescuento = Model.PorcentajeDescuento == 100 ? 99.99M : Model.PorcentajeDescuento;
                    IsDirty = true;
                    CalcularTotalDescuento();
                    ActualizaTotalesDeFactura();
                    Model.PorcentajeDescuento1 = Model.PorcentajeDescuento;
                    RaisePropertyChanged(PorcentajeDescuentoPropertyName);
                    if(value > DescuentoDesdeParametros)
                        RaiseMoveFocus(ArticuloPropertyName);
                }
            }
        }

        public string Moneda {
            get {
                return Model.Moneda;
            }
            set {
                if(Model.Moneda != value) {
                    Model.Moneda = value;
                }
            }
        }

        public Saw.Ccl.Cliente.eNivelDePrecio NivelDePrecio {
            get {
                return Model.NivelDePrecioAsEnum;
            }
            set {
                if(Model.NivelDePrecioAsEnum != value) {
                    Model.NivelDePrecioAsEnum = value;
                }
            }
        }

        public eStatusFactura StatusFactura {
            get {
                return Model.StatusFacturaAsEnum;
            }
            set {
                if(Model.StatusFacturaAsEnum != value) {
                    Model.StatusFacturaAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(StatusFacturaPropertyName);
                }
            }
        }

        public eTipoDocumentoFactura TipoDeDocumento {
            get {
                return Model.TipoDeDocumentoAsEnum;
            }
            set {
                if(Model.TipoDeDocumentoAsEnum != value) {
                    Model.TipoDeDocumentoAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(TipoDeDocumentoPropertyName);
                }
            }
        }

        public decimal CambioABolivares {
            get {
                return Model.CambioABolivares;
            }
            set {
                if(Model.CambioABolivares != value) {
                    Model.CambioABolivares = value;
                    IsDirty = true;
                    RaisePropertyChanged(CambioABolivaresPropertyName);
                }
            }
        }

        public decimal MontoDelAbono {
            get {
                return Model.MontoDelAbono;
            }
            set {
                if(Model.MontoDelAbono != value) {
                    Model.MontoDelAbono = value;
                    IsDirty = true;
                    RaisePropertyChanged(MontoDelAbonoPropertyName);
                }
            }
        }

        public eTipoDeVenta TipoDeVenta {
            get {
                return Model.TipoDeVentaAsEnum;
            }
            set {
                if(Model.TipoDeVentaAsEnum != value) {
                    Model.TipoDeVentaAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(TipoDeVentaPropertyName);
                }
            }
        }
        public decimal MontoIvaRetenido {
            get {
                return Model.MontoIvaRetenido;
            }
            set {
                if(Model.MontoIvaRetenido != value) {
                    Model.MontoIvaRetenido = value;
                    IsDirty = true;
                    RaisePropertyChanged(MontoIvaRetenidoPropertyName);
                }
            }
        }

        public bool FacturaConPreciosSinIva {
            get {
                return Model.FacturaConPreciosSinIvaAsBool;
            }
            set {
                if(Model.FacturaConPreciosSinIvaAsBool != value) {
                    Model.FacturaConPreciosSinIvaAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(FacturaConPreciosSinIvaPropertyName);
                }
            }
        }


        public decimal PorcentajeAlicuota1 {
            get {
                return Model.PorcentajeAlicuota1;
            }
            set {
                if(Model.PorcentajeAlicuota1 != value) {
                    Model.PorcentajeAlicuota1 = value;
                    IsDirty = true;
                    RaisePropertyChanged(PorcentajeAlicuota1PropertyName);
                }
            }
        }

        public decimal PorcentajeAlicuota2 {
            get {
                return Model.PorcentajeAlicuota2;
            }
            set {
                if(Model.PorcentajeAlicuota2 != value) {
                    Model.PorcentajeAlicuota2 = value;
                    IsDirty = true;
                    RaisePropertyChanged(PorcentajeAlicuota2PropertyName);
                }
            }
        }

        public decimal PorcentajeAlicuota3 {
            get {
                return Model.PorcentajeAlicuota3;
            }
            set {
                if(Model.PorcentajeAlicuota3 != value) {
                    Model.PorcentajeAlicuota3 = value;
                    IsDirty = true;
                    RaisePropertyChanged(PorcentajeAlicuota3PropertyName);
                }
            }
        }

        public decimal MontoIvaAlicuota2 {
            get {
                return Model.MontoIvaAlicuota2;
            }
            set {
                if(Model.MontoIvaAlicuota2 != value) {
                    Model.MontoIvaAlicuota2 = value;
                    IsDirty = true;
                    RaisePropertyChanged(MontoIvaAlicuota2PropertyName);
                }
            }
        }

        public decimal MontoIvaAlicuota1 {
            get {
                return Model.MontoIvaAlicuota1;
            }
            set {
                if(Model.MontoIvaAlicuota1 != value) {
                    Model.MontoIvaAlicuota1 = value;
                    IsDirty = true;
                    RaisePropertyChanged(MontoIvaAlicuota1PropertyName);
                }
            }
        }

        public decimal MontoIvaAlicuota3 {
            get {
                return Model.MontoIvaAlicuota3;
            }
            set {
                if(Model.MontoIvaAlicuota3 != value) {
                    Model.MontoIvaAlicuota3 = value;
                    IsDirty = true;
                    RaisePropertyChanged(MontoIvaAlicuota3PropertyName);
                }
            }
        }

        public decimal MontoGravableAlicuota1 {
            get {
                return Model.MontoGravableAlicuota1;
            }
            set {
                if(Model.MontoGravableAlicuota1 != value) {
                    Model.MontoGravableAlicuota1 = value;
                    IsDirty = true;
                    RaisePropertyChanged(MontoGravableAlicuota1PropertyName);
                }
            }
        }

        public decimal MontoGravableAlicuota2 {
            get {
                return Model.MontoGravableAlicuota2;
            }
            set {
                if(Model.MontoGravableAlicuota2 != value) {
                    Model.MontoGravableAlicuota2 = value;
                    IsDirty = true;
                    RaisePropertyChanged(MontoGravableAlicuota2PropertyName);
                }
            }
        }

        public decimal MontoGravableAlicuota3 {
            get {
                return Model.MontoGravableAlicuota3;
            }
            set {
                if(Model.MontoGravableAlicuota3 != value) {
                    Model.MontoGravableAlicuota3 = value;
                    IsDirty = true;
                    RaisePropertyChanged(MontoGravableAlicuota3PropertyName);
                }
            }
        }

        public int ConsecutivoAlmacen {
            get {
                return Model.ConsecutivoAlmacen;
            }
            set {
                if(Model.ConsecutivoAlmacen != value) {
                    Model.ConsecutivoAlmacen = value;
                    IsDirty = true;
                    RaisePropertyChanged(ConsecutivoAlmacenPropertyName);
                }
            }
        }

        public decimal PorcentajeDeLaInicial {
            get {
                return Model.PorcentajeDeLaInicial;
            }
            set {
                if(Model.PorcentajeDeLaInicial != value) {
                    Model.PorcentajeDeLaInicial = value;
                    IsDirty = true;
                    RaisePropertyChanged(PorcentajeDeLaInicialPropertyName);
                }
            }
        }

        public int NumeroDeCuotas {
            get {
                return Model.NumeroDeCuotas;
            }
            set {
                if(Model.NumeroDeCuotas != value) {
                    Model.NumeroDeCuotas = value;
                    IsDirty = true;
                    RaisePropertyChanged(NumeroDeCuotasPropertyName);
                }
            }
        }

        public int NumDiasDeVencimiento1aCuota {
            get {
                return Model.NumDiasDeVencimiento1aCuota;
            }
            set {
                if(Model.NumDiasDeVencimiento1aCuota != value) {
                    Model.NumDiasDeVencimiento1aCuota = value;
                    IsDirty = true;
                    RaisePropertyChanged(NumDiasDeVencimiento1aCuotaPropertyName);
                }
            }
        }

        public int ConsecutivoCaja {
            get {
                return Model.ConsecutivoCaja;
            }
            set {
                if(Model.ConsecutivoCaja != value) {
                    Model.ConsecutivoCaja = value;
                }
            }
        }

        public string CodigoAlmacen {
            get {
                return Model.CodigoAlmacen;
            }
            set {
                if(Model.CodigoAlmacen != value) {
                    Model.CodigoAlmacen = value;
                }
            }
        }

        public string CodigoMoneda {
            get {
                return Model.CodigoMoneda;
            }
            set {
                if(Model.CodigoMoneda != value) {
                    Model.CodigoMoneda = value;
                    IsDirty = true;
                    RaisePropertyChanged(CodigoMonedaPropertyName);
                }
            }
        }

        public string InfoCliente1 {
            get {
                return _InfoCliente1;
            }
            set {
                if(_InfoCliente1 != value) {
                    _InfoCliente1 = value;
                    RaisePropertyChanged(InfoCliente1PropertyName);
                }
            }
        }

        public string InfoCliente2 {
            get {
                return _InfoCliente2;
            }
            set {
                if(_InfoCliente2 != value) {
                    _InfoCliente2 = value;
                    RaisePropertyChanged(InfoCliente2PropertyName);
                }
            }
        }

        public string InfoCliente3 {
            get {
                return _InfoCliente3;
            }
            set {
                if(_InfoCliente3 != value) {
                    _InfoCliente3 = value;
                    RaisePropertyChanged(InfoCliente3PropertyName);
                }
            }
        }

        public string InfoArticulo1 {
            get {
                return _InfoArticulo1;
            }
            set {
                if(_InfoArticulo1 != value) {
                    _InfoArticulo1 = value;
                    RaisePropertyChanged(InfoArticulo1PropertyName);
                }
            }
        }

        public string InfoArticulo2 {
            get {
                return _InfoArticulo2;
            }
            set {
                if(_InfoCliente2 != value) {
                    _InfoCliente2 = value;
                    RaisePropertyChanged(InfoArticulo2PropertyName);
                }
            }
        }

        public string InfoArticulo3 {
            get {
                return _InfoArticulo3;
            }
            set {
                if(_InfoArticulo3 != value) {
                    _InfoArticulo3 = value;
                    RaisePropertyChanged(InfoArticulo3PropertyName);
                }
            }
        }

        public string NombreCajero {
            get {
                return _NombreCajero;
            }
            set {
                if(_NombreCajero != value) {
                    _NombreCajero = value;
                    RaisePropertyChanged(NombreCajeroPropertyName);
                }
            }
        }

        private string _Articulo;

        public string Articulo {
            get {
                return _Articulo;
            }
            set {
                if(_Articulo != value) {
                    _Articulo = value;
                    RaisePropertyChanged(ArticuloPropertyName);
                    if(LibString.IsNullOrEmpty(_Articulo)) {
                        ConexionArticulo = null;
                    }
                }
            }
        }

        private string _descripcion;
        public string Descripcion {
            get {
                return _descripcion;
            }
            set {
                if(_descripcion != value) {
                    _descripcion = value;
                    RaisePropertyChanged(DescripcionPropertyName);
                }
            }
        }

        private string _descripcionCorta;
        public string DescripcionCorta {
            get {
                return _descripcionCorta;
            }
            set {
                if(_descripcionCorta != value) {
                    _descripcionCorta = value;
                    RaisePropertyChanged(DescripcionCortaPropertyName);
                }
            }
        }

        private decimal _cantidad;
        public decimal Cantidad {
            get {
                return _cantidad;
            }
            set {
                if(_cantidad != value) {
                    _cantidad = value;
                    RaisePropertyChanged(CantidadPropertyName);
                }
            }
        }

        private decimal _TotalDeItems;

        public decimal TotalDeItems {
            get {
                return _TotalDeItems;
            }
            set {
                _TotalDeItems = value;
                RaisePropertyChanged(TotalDeItemsPropertyName);
            }
        }

        public bool AplicaDecretoIvaEspecial {
            get {
                return Model.AplicaDecretoIvaEspecialAsBool;
            }
            set {
                if(Model.AplicaDecretoIvaEspecialAsBool != value) {
                    Model.AplicaDecretoIvaEspecialAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(AplicaDecretoIvaEspecialPropertyName);
                    RaisePropertyChanged(BackgroundDecretoIvaEspecialPropertyName);
                    MensajeImpresionFiscalDecreto3085();
                    if(TotalFactura > 0) {
                        DetailFacturaRapidaDetalle.SelectedIndex = 0;
                        ActualizaSaldos(true);
                    }
                }
            }
        }

        public string Observaciones {
            get {
                return Model.Observaciones;
            }
            set {
                if(Model.Observaciones != value) {
                    Model.Observaciones = value;
                    IsDirty = true;
                    RaisePropertyChanged(ObservacionesPropertyName);
                }
            }
        }

        public decimal CambioMostrarTotalEnDivisas {
            get {
                return Model.CambioMostrarTotalEnDivisas;
            }
            set {
                if(Model.CambioMostrarTotalEnDivisas != value) {
                    Model.CambioMostrarTotalEnDivisas = value;
                    IsDirty = true;
                    RaisePropertyChanged(CambioMostrarTotalEnDivisasPropertyName);
                    CalcularTotalFacturaDivisas();
                }
            }
        }

        public string CodigoMonedaDeCobro {
            get {
                return Model.CodigoMonedaDeCobro;
            }
            set {
                if(Model.CodigoMonedaDeCobro != null) {
                    Model.CodigoMonedaDeCobro = value;
                    RaisePropertyChanged(CodigoMonedaDeCobroPropertyName);
                }
            }
        }

        [LibRequired(ErrorMessage = "El campo Nombre de la Moneda de Cobro es requerido.")]
        public string NombreMonedaDeCobro {
            get {
                return Model.NombreMonedaDeCobro;
            }
            set {
                if(Model.NombreMonedaDeCobro != value) {
                    Model.NombreMonedaDeCobro = value;
                    RaisePropertyChanged(NombreMonedaDeCobroPropertyName);
                    if(LibString.IsNullOrEmpty(NombreMonedaDeCobro,true)) {
                        ConexionNombreMonedaDeCobro = null;
                    }
                }
            }
        }

        public string NombreOperador {
            get {
                return Model.NombreOperador;
            }
            set {
                if(Model.NombreOperador != value) {
                    Model.NombreOperador = value;
                    IsDirty = true;
                    RaisePropertyChanged(NombreOperadorPropertyName);
                }
            }
        }

        public DateTime FechaUltimaModificacion {
            get {
                return Model.FechaUltimaModificacion;
            }
            set {
                if(Model.FechaUltimaModificacion != value) {
                    Model.FechaUltimaModificacion = value;
                    IsDirty = true;
                    RaisePropertyChanged(FechaUltimaModificacionPropertyName);
                }
            }
        }

        public bool IsVisibleAlicuotas {
            get {
                return false;//LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("FacturaRapida", "UsaMultiplesAlicuotas");
            }
        }


        public bool BuscaClienteXRifAlFacturar {
            get {
                return LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("FacturaRapida","BuscarClienteXRifAlFacturar");
            }
        }

        public bool IsVisibleNumeroRif {
            get {
                return LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("FacturaRapida","BuscarClienteXRifAlFacturar") &&
                    !LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("FacturaRapida","UsaBusquedaDinamicaEnPuntoDeVenta");
            }
        }

        public bool IsVisibleLabelNumeroRif {
            get {
                return !LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("FacturaRapida","BuscarClienteXRifAlFacturar");
            }
        }

        public bool IsVisibleNombreCliente {
            get {
                return !LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("FacturaRapida","BuscarClienteXRifAlFacturar") &&
                       !LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("FacturaRapida","UsaBusquedaDinamicaEnPuntoDeVenta");
            }
        }

        public bool IsVisibleArticulo {
            get {
                return !LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("FacturaRapida","UsaBusquedaDinamicaEnPuntoDeVenta");
            }
        }

        public bool IsVisibleLabelNombreCliente {
            get {
                return LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("FacturaRapida","BuscarClienteXRifAlFacturar");
            }
        }

        public bool IsVisibleInfoCliente {
            get {
                return true;
            }
        }

        public bool IsVisibleInfoArticulo {
            get {
                return true;
            }
        }

        public bool BackgroundDecretoIvaEspecial {
            get {
                return AplicaDecretoIvaEspecial;
            }
        }

        public bool IsVisibleAplicaDecretoIvaEspecial {
            get {
                return EsValidaFacturaParaDecretoIvaEspecial() && EmpresaUsaPrecioSinIva;
            }
        }

        public bool MostrarTasaDeCambio {
            get {
                return EsPosibleMostrarTasaDeCambio();
            }
        }

        public bool MostrarPorcentajeDescuento {
            get {
                return _MostrarPorcentajeDescuento;
            }
            set {
                if(_MostrarPorcentajeDescuento != value) {
                    _MostrarPorcentajeDescuento = value;
                }
            }
        }

        public string SimboloMonedaParaTotalEnDivisas {
            get {
                return _SimboloMonedaParaTotalEnDivisas;
            }
            set {
                if(_SimboloMonedaParaTotalEnDivisas != value) {
                    _SimboloMonedaParaTotalEnDivisas = value;
                    RaisePropertyChanged(SimboloMonedaParaTotalEnDivisasPropertyName);
                }
            }
        }

        public string SimboloMonedaDeFactura {
            get {
                return _SimboloMonedaDeFactura;
            }
            set {
                if(_SimboloMonedaDeFactura != value) {
                    _SimboloMonedaDeFactura = value;
                    RaisePropertyChanged(SimboloMonedaDeFacturaPropertyName);
                }
            }
        }

        public BalanzaTomarPesoViewModel BalanzaTomarPesoViewModel {
            get {
                return _BalanzaTomarPesoViewModel;
            }
            set {
                _BalanzaTomarPesoViewModel = value;
            }
        }

        public bool UsaBalanzaEnPOS {
            get {
                return _UsaBalanzaEnPOS;
            }
            set {
                _UsaBalanzaEnPOS = value;
            }
        }

        public Saw.Ccl.Cliente.eNivelDePrecio[] ArrayNivelDePrecio {
            get {
                return LibEnumHelper<Saw.Ccl.Cliente.eNivelDePrecio>.GetValuesInArray();
            }
        }

        public eStatusFactura[] ArrayStatusFactura {
            get {
                return LibEnumHelper<eStatusFactura>.GetValuesInArray();
            }
        }

        public eTipoDocumentoFactura[] ArrayTipoDocumentoFactura {
            get {
                return LibEnumHelper<eTipoDocumentoFactura>.GetValuesInArray();
            }
        }

        public eTipoDeVenta[] ArrayTipoDeVenta {
            get {
                return LibEnumHelper<eTipoDeVenta>.GetValuesInArray();
            }
        }
        [LibDetailRequired(ErrorMessage = "Debe ingresar Items para el Punto de Venta.")]
        public FacturaRapidaDetalleMngViewModel DetailFacturaRapidaDetalle {
            get;
            set;
        }

        public FkVendedorViewModel ConexionCodigoVendedor {
            get {
                return _ConexionCodigoVendedor;
            }
            set {
                if(_ConexionCodigoVendedor != value) {
                    _ConexionCodigoVendedor = value;
                    RaisePropertyChanged(CodigoVendedorPropertyName);
                    if(_ConexionCodigoVendedor != null) {
                        CodigoVendedor = ConexionCodigoVendedor.Codigo;
                        NombreVendedor = ConexionCodigoVendedor.Nombre;
                    }
                }
                if(_ConexionCodigoVendedor == null) {
                    CodigoVendedor = string.Empty;
                    NombreVendedor = string.Empty;
                }
            }
        }

        public FkVendedorViewModel ConexionNombreVendedor {
            get {
                return _ConexionNombreVendedor;
            }
            set {
                if(_ConexionNombreVendedor != value) {
                    _ConexionNombreVendedor = value;
                    RaisePropertyChanged(NombreVendedorPropertyName);
                    if(_ConexionNombreVendedor != null) {
                        CodigoVendedor = ConexionNombreVendedor.Codigo;
                        NombreVendedor = ConexionNombreVendedor.Nombre;
                    }
                }
                if(_ConexionNombreVendedor == null) {
                    CodigoVendedor = string.Empty;
                    NombreVendedor = string.Empty;
                }
            }
        }

        public FkArticuloInventarioViewModel ConexionArticulo {
            get {
                return _ConexionArticulo;
            }
            set {
                if(_ConexionArticulo != value) {
                    _ConexionArticulo = value;
                    RaisePropertyChanged(ConexionArticuloPropertyName);
                    if(_ConexionArticulo != null) {
                        Articulo = ConexionArticulo.Codigo;
                        Descripcion = ConexionArticulo.Descripcion;
                        DescripcionCorta = LibString.SubString(ConexionArticulo.Descripcion,0,50);
                    }
                }
                if(_ConexionArticulo == null) {
                    Articulo = string.Empty;
                    Descripcion = string.Empty;
                    DescripcionCorta = string.Empty;
                }
            }
        }

        public FkArticuloInventarioViewModel ConexionDescripcion {
            get {
                return _ConexionDescripcion;
            }
            set {
                if(_ConexionDescripcion != value) {
                    _ConexionDescripcion = value;
                    RaisePropertyChanged(DescripcionPropertyName);
                }
                if(_ConexionDescripcion == null) {
                    Descripcion = string.Empty;
                    DescripcionCorta = string.Empty;
                }
            }
        }

        public FkMonedaViewModel ConexionCodigoMonedaDeCobro {
            get {
                return _ConexionCodigoMonedaDeCobro;
            }
            set {
                if(_ConexionCodigoMonedaDeCobro != value) {
                    _ConexionCodigoMonedaDeCobro = value;
                    RaisePropertyChanged(CodigoMonedaDeCobroPropertyName);
                    if(_ConexionCodigoMonedaDeCobro != null) {
                        CodigoMonedaDeCobro = ConexionCodigoMonedaDeCobro.Codigo;
                        NombreMonedaDeCobro = ConexionCodigoMonedaDeCobro.Nombre;
                    }
                }
                if(_ConexionCodigoMonedaDeCobro == null) {
                    CodigoMonedaDeCobro = string.Empty;
                    NombreMonedaDeCobro = string.Empty;
                }
            }
        }

        public FkMonedaViewModel ConexionNombreMonedaDeCobro {
            get {
                return _ConexionNombreMonedaDeCobro;
            }
            set {
                if(_ConexionNombreMonedaDeCobro != value) {
                    _ConexionNombreMonedaDeCobro = value;
                    RaisePropertyChanged(NombreMonedaDeCobroPropertyName);
                    if(_ConexionNombreMonedaDeCobro != null) {
                        CodigoMonedaDeCobro = ConexionNombreMonedaDeCobro.Codigo;
                        NombreMonedaDeCobro = ConexionNombreMonedaDeCobro.Nombre;
                    }
                }
                if(_ConexionNombreMonedaDeCobro == null) {
                    CodigoMonedaDeCobro = string.Empty;
                    NombreMonedaDeCobro = string.Empty;
                }
            }
        }

        public FkClienteViewModel ConexionCliente {
            get {
                return _ConexionCliente;
            }
            set {
                if(value != null) {
                    _ConexionCliente = value;
                    CodigoCliente = value.Codigo;
                    NumeroRIF = value.NumeroRIF;
                    NombreCliente = value.Nombre;
                    RaisePropertyChanged(NombreClientePropertyName);
                    if(ConexionCliente.TipoDeContribuyente == LibConvert.ToStr((int)eTipoDeContribuyente.Contribuyente)) {
                        TipoDeVenta = eTipoDeVenta.AContribuyente;
                    } else {
                        TipoDeVenta = eTipoDeVenta.ANoContribuyente;
                    }
                    DireccionCliente = value.Direccion;
                    TelefonoCliente = value.Telefono;
                    InfoCliente1 = value.Direccion;
                    InfoCliente2 = value.Telefono;
                    InfoCliente3 = LibConvert.ToStr(value.ClienteDesdeFecha);
                }
                if(value == null && _ConexionCliente != null) {
                    CodigoCliente = string.Empty;
                    NumeroRIF = string.Empty;
                    NombreCliente = string.Empty;
                    DireccionCliente = string.Empty;
                    TelefonoCliente = string.Empty;
                    InfoCliente1 = string.Empty;
                    InfoCliente2 = string.Empty;
                    InfoCliente3 = string.Empty;
                }
            }
        }

        public string CategoriaConexion {
            get {
                FacturaRapidaDetalleViewModel vViewModel = new FacturaRapidaDetalleViewModel();
                return vViewModel.Categoria;
            }
        }

        public bool IsVisibleCuadroDeBusqueda {
            get {
                return LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("FacturaRapida","UsaBusquedaDinamicaEnPuntoDeVenta");
            }
        }

        public RelayCommand<string> ChooseArticuloCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseNumeroRIFCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseNombreClienteCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseCodigoVendedorCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseNombreVendedorCommand {
            get;
            private set;
        }

        public RelayCommand<string> CreateFacturaRapidaDetalleCommand {
            get {
                return DetailFacturaRapidaDetalle.CreateCommand;
            }
        }

        public RelayCommand<string> UpdateFacturaRapidaDetalleCommand {
            get {
                return DetailFacturaRapidaDetalle.UpdateCommand;
            }
        }

        public RelayCommand<string> DeleteFacturaRapidaDetalleCommand {
            get {
                return DetailFacturaRapidaDetalle.DeleteCommand;
            }
        }

        public RelayCommand AbrirConsultorDePreciosCommand {
            get;
            private set;
        }
        public RelayCommand InsertaClienteCommand {
            get;
            private set;
        }

        public RelayCommand BuscarFacturaEnEsperaCommand {
            get;
            private set;
        }
        public RelayCommand FacturaEnEsperaCommand {
            get;
            private set;
        }
        public RelayCommand CobrarCommand {
            get;
            private set;
        }

        public RelayCommand BuscarClienteCommand {
            get;
            private set;
        }

        public RelayCommand CantidadCommand {
            get;
            private set;
        }

        public RelayCommand BuscarArticuloCommand {
            get;
            private set;
        }

        public RelayCommand MoverRegistrosGridCommand {
            get;
            private set;
        }

        public RelayCommand BuscarVendedorCommand {
            get;
            private set;
        }
        public RelayCommand BuscarMonedaDeCobroCommand {
            get;
            private set;
        }

        public RelayCommand AsignarDescuentoCommand {
            get;
            private set;
        }

        public RelayCommand BuscarClienteLeyendaCommand {
            get;
            private set;
        }

        public RelayCommand BuscarArticuloLeyendaCommand {
            get;
            private set;
        }

        public RelayCommand CambiarCantidadLeyendaCommand {
            get;
            private set;
        }

        public RelayCommand BuscarVendedorLeyendaCommand {
            get;
            private set;
        }
        public RelayCommand BuscarMonedaDeCobroLeyendaCommand {
            get;
            private set;
        }

        public RelayCommand AplicaDecretoIvaEspecialCommand {
            get;
            private set;
        }

        public RelayCommand NuevaFacturaCommand {
            get;
            private set;
        }

        public RelayCommand BuscarPrecioDeArticuloCommand {
            get;
            private set;
        }

        public RelayCommand IrADescripcionGridCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseCodigoMonedaDeCobroCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseNombreMonedaDeCobroCommand {
            get;
            private set;
        }

        public bool FocoEnArticulo {
            get;
            set;
        }

        public bool SumoCantidad {
            get;
            set;
        }

        public bool IsEnterKeyPressedArticulo {
            get;
            set;
        }

        private FacturaRapida FacturaRapidaACobrar {
            get;
            set;
        }

        public Saw.Ccl.Cliente.eTipoDocumentoIdentificacion TipoDeDocumentoIdentificacion {
            get {
                if(LibDefGen.ProgramInfo.IsCountryPeru()) {
                    return TipoDeDocumento == eTipoDocumentoFactura.Boleta ? Saw.Ccl.Cliente.eTipoDocumentoIdentificacion.DNI : Saw.Ccl.Cliente.eTipoDocumentoIdentificacion.RUC;
                } else {
                    return Saw.Ccl.Cliente.eTipoDocumentoIdentificacion.RUC;
                }
            }
        }
        #endregion //Propiedades

        #region Constructores

        public FacturaRapidaViewModel()
            : this(new FacturaRapida(),eAccionSR.Insertar) {
        }

        public FacturaRapidaViewModel(FacturaRapida initModel,eAccionSR initAction)
            : base(initModel,initAction,LibGlobalValues.Instance.GetAppMemInfo(),LibGlobalValues.Instance.GetMfcInfo(),true) {
            DefaultFocusedPropertyName = NumeroPropertyName;
            _clsNoComun = new clsNoComunSaw();
            Model.ConsecutivoCompania = Mfc.GetInt("Compania");
            InitializeDetails();
            FocoEnArticulo = true;
            SumoCantidad = false;
            Cantidad = 1;
            TotalDeItems = 0;
            EsFacturaEnEspera = false;
            CambioABolivares = 1;
            AsignarMonedaDeCobroFacturaSegunCorresponda();
            EmpresaAplicaIVAEspecial = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("FacturaRapida","AplicarIVAEspecial");
            EmpresaUsaPrecioSinIva = LibConvert.SNToBool(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("FacturaRapida","UsaPrecioSinIva"));
            _CantidadDeDecimales = LibConvert.ToInt(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("FacturaRapida","CantidadDeDecimales"));
            _UsaPesoEnCodigo = LibConvert.SNToBool(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("FacturaRapida","UsaPesoEnCodigo"));
            _UsaPrecioEnCodigo = LibConvert.SNToBool(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("FacturaRapida","UsaPrecioEnCodigo"));
            _EtiquetaIncluyeIva = LibConvert.SNToBool(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("FacturaRapida","PrecioIncluyeIva"));
            _UsaListaDePrecioEnMonedaExtranjera = LibConvert.SNToBool(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros","UsaListaDePrecioEnMonedaExtranjera"));
            RaisePropertyChanged(IsVisibleAplicaDecretoIvaEspecialPropertyName);
            CuadroDeBusquedaDeClientesViewModel = new CuadroDeBusquedaDeClientesViewModel();
            CuadroDeBusquedaDeArticulosViewModel = new CuadroDeBusquedaDeArticulosViewModel();
            CuadroDeBusquedaDeArticulosViewModel.ItemSelected += ArticuloSeleccionado;
            CuadroDeBusquedaDeClientesViewModel.ItemSelected += ClienteSeleccionado;
            CuadroDeBusquedaDeArticulosViewModel.IsControlVisible = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("FacturaRapida","UsaBusquedaDinamicaEnPuntoDeVenta");
            CuadroDeBusquedaDeClientesViewModel.IsControlVisible = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("FacturaRapida","UsaBusquedaDinamicaEnPuntoDeVenta");
            MostrarPorcentajeDescuento = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("FacturaRapida","OtorgarDescuento");
        }

        private void ClienteSeleccionado(LookupItem itemEscogido) {
            ExecuteChooseNombreClienteCommand(itemEscogido.Description);
        }

        private void ArticuloSeleccionado(LookupItem itemEscogido) {
            IsEnterKeyPressedArticulo = true;
            ExecuteChooseArticuloCommand(itemEscogido.Code);
            ActualizarValoresCuadrosDeBusqueda(CuadroDeBusquedaDeArticulosViewModel,Articulo);
            IsEnterKeyPressedArticulo = false;
        }
        #endregion //Constructores

        #region Metodos Generados

        protected override void InitializeLookAndFeel(FacturaRapida valModel) {
            base.InitializeLookAndFeel(valModel);
            if(!EsFacturaEnEspera) {
                if(LibString.IsNullOrEmpty(Numero,true)) {
                    Numero = GenerarProximoNumero();
                }
                NombreCajero = ((CustomIdentity)Thread.CurrentPrincipal.Identity).Login;
                TipoDeDocumento = eTipoDocumentoFactura.ComprobanteFiscal;
                AsignarMonedaDeFacturaSegunCorresponda();
                AsignarMonedaDeCobroFacturaSegunCorresponda();
                CargaVendedorGenerico();
                CargaDeAlicuotasIva();
                Articulo = string.Empty;
                TotalDeItems = 0;
                ListDeCobroMaster.Clear();
                CargarClienteGenerico();
            }
            if(CodigoAlmacen == string.Empty) {
                IAlmacenPdn insAlmacen = new clsAlmacenNav();
                CodigoAlmacen = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("FacturaRapida","CodigoAlmacen");
                ConsecutivoAlmacen = insAlmacen.ObtenerConsecutivoAlmacen(CodigoAlmacen,Mfc.GetInt("Compania"));
            }
            if(ConsecutivoCaja == 0) {
                ConsecutivoCaja = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("Parametros","ConsecutivoCaja");
            }
            CalcularTotalFacturaDivisas();
        }

        protected override FacturaRapida FindCurrentRecord(FacturaRapida valModel) {
            if(valModel == null) {
                return null;
            }
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania",valModel.ConsecutivoCompania);
            vParams.AddInString("Numero",valModel.Numero,11);
            vParams.AddInEnum("TipoDeDocumento",valModel.TipoDeDocumentoAsDB);
            vParams.AddInEnum("StatusFactura",valModel.StatusFacturaAsDB);
            return BusinessComponent.GetData(eProcessMessageType.SpName,"FacturaRapidaGET",vParams.Get(),UseDetail).FirstOrDefault();
        }

        protected override ILibBusinessMasterComponentWithSearch<IList<FacturaRapida>,IList<FacturaRapida>> GetBusinessComponent() {
            return new clsFacturaRapidaNav();
        }

        private string GenerarProximoNumero() {
            string vResult = string.Empty;
            //XElement vResulset = GetBusinessComponent().QueryInfo(eProcessMessageType.Query, "ProximoNumero", Mfc.GetIntAsParam("Compania"), false);
            //vResult = LibXml.GetPropertyString(vResulset, "Numero");
            return vResult;
        }

        #region FacturaRapidaDetalle
        protected override void InitializeDetails() {
            DetailFacturaRapidaDetalle = new FacturaRapidaDetalleMngViewModel(this,Model.DetailFacturaRapidaDetalle,Action);
            DetailFacturaRapidaDetalle.OnCreated += new EventHandler<SearchCollectionChangedEventArgs<FacturaRapidaDetalleViewModel>>(DetailFacturaRapidaDetalle_OnCreated);
            DetailFacturaRapidaDetalle.OnUpdated += new EventHandler<SearchCollectionChangedEventArgs<FacturaRapidaDetalleViewModel>>(DetailFacturaRapidaDetalle_OnUpdated);
            DetailFacturaRapidaDetalle.OnDeleted += new EventHandler<SearchCollectionChangedEventArgs<FacturaRapidaDetalleViewModel>>(DetailFacturaRapidaDetalle_OnDeleted);
            DetailFacturaRapidaDetalle.OnSelectedItemChanged += new EventHandler<SearchCollectionChangedEventArgs<FacturaRapidaDetalleViewModel>>(DetailFacturaRapidaDetalle_OnSelectedItemChanged);
        }
        private void DetailFacturaRapidaDetalle_OnSelectedItemChanged(object sender,SearchCollectionChangedEventArgs<FacturaRapidaDetalleViewModel> e) {
            try {
                UpdateFacturaRapidaDetalleCommand.RaiseCanExecuteChanged();
                DeleteFacturaRapidaDetalleCommand.RaiseCanExecuteChanged();
            } catch(AccessViolationException) {
                throw;
            } catch(Exception vEx) {
                LibMessages.RaiseError.ShowError(vEx,ModuleName);
            }
        }
        private void DetailFacturaRapidaDetalle_OnDeleted(object sender,SearchCollectionChangedEventArgs<FacturaRapidaDetalleViewModel> e) {
            try {
                IsDirty = true;
                Model.DetailFacturaRapidaDetalle.Remove(e.ViewModel.GetModel());
                e.ViewModel.PropertyChanged -= OnDetailPropertyChanged;
                ActualizaSaldos(false);
                RaiseMoveFocus(ArticuloPropertyName);
            } catch(AccessViolationException) {
                throw;
            } catch(Exception vEx) {
                LibMessages.RaiseError.ShowError(vEx,ModuleName);
            }
        }

        protected override bool CanExecuteDeleteSelectedDetailCommand(object valSelectedItem) {
            try {
                bool vCanExecute = false;
                if(LibSecurityManager.CurrentUserHasAccessTo("Punto de Venta","Eliminar Item en Factura")) {
                    vCanExecute = true;
                } else {
                    if(LibMessages.MessageBox.YesNo(this,"Este usuario no tiene permiso para eliminar un Item. ¿Desea ingresar clave de supervisor?","Información")) {
                        LibGalac.Aos.Uil.Usal.GUserLogin vGUserLogin = new LibGalac.Aos.Uil.Usal.GUserLogin();
                        List<CustomRole> vListRoles = new List<CustomRole>();
                        vListRoles.Add(new CustomRole("Punto de Venta","Eliminar Item en Factura"));
                        if(!vGUserLogin.RequestCredential("Eliminar Item",true,vListRoles)) {
                            LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.MessageBox.Information(null,"Usuario o Clave no válido.","Información");
                        } else {
                            vCanExecute = true;
                        }
                    }
                }
                return vCanExecute;
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx,ModuleName);
                return false;
            }
        }

        private void DetailFacturaRapidaDetalle_OnUpdated(object sender,SearchCollectionChangedEventArgs<FacturaRapidaDetalleViewModel> e) {
            try {
                IsDirty = e.ViewModel.IsDirty;
                ActualizaSaldos(true);
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx,ModuleName);
            }
        }
        private void DetailFacturaRapidaDetalle_OnCreated(object sender,SearchCollectionChangedEventArgs<FacturaRapidaDetalleViewModel> e) {
            try {
                FacturaRapidaDetalle vModel = e.ViewModel.GetModel();
                Model.DetailFacturaRapidaDetalle.Add(e.ViewModel.GetModel());
                e.ViewModel.PropertyChanged += OnDetailPropertyChanged;
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx,ModuleName);
            }
        }

        private void OnDetailPropertyChanged(object sender,System.ComponentModel.PropertyChangedEventArgs e) {
            ActualizaSaldos(true);
        }

        #endregion //FacturaRapidaDetalle

        #region Ribbon
        protected override void InitializeRibbon() {
            base.InitializeRibbon();
            if(RibbonData.TabDataCollection != null && RibbonData.TabDataCollection.Count > 0) {
                RibbonData.TabDataCollection[0].AddTabGroupData(CreateCobrarRibbonGroup());
                RibbonData.TabDataCollection[0].AddTabGroupData(CreateNuevaFacturaRibbonGroup());
                RibbonData.TabDataCollection[0].AddTabGroupData(CreateFacturaEnEsperaRibbonGroup());
                RibbonData.TabDataCollection[0].AddTabGroupData(CreateBuscarFacturaEnEsperaRibbonGroup());
                RibbonData.TabDataCollection[0].AddTabGroupData(CreateBotonesLeyendaRibbonGroup());
                RibbonData.RemoveRibbonControl("Acciones","Insertar");
                var vAccionesGrupo = RibbonData.TabDataCollection[0].GroupDataCollection[0];
                RibbonData.TabDataCollection[0].GroupDataCollection.Remove(vAccionesGrupo);
                RibbonData.TabDataCollection[0].GroupDataCollection.Insert(4,vAccionesGrupo);
            }
        }

        private LibRibbonGroupData CreateCobrarRibbonGroup() {
            LibRibbonGroupData vResult = new LibRibbonGroupData("");
            vResult.ControlDataCollection.Add(new LibRibbonButtonData() {
                Label = "Cobrar",
                Command = CobrarCommand,
                LargeImage = new Uri("/Galac.Adm.Uil.Venta;component/Images/F6.png",UriKind.Relative),
                ToolTipDescription = ".",
                ToolTipTitle = "Cobrar"
            });
            return vResult;
        }

        private LibRibbonGroupData CreateFacturaEnEsperaRibbonGroup() {
            LibRibbonGroupData vResult = new LibRibbonGroupData("");
            vResult.ControlDataCollection.Add(new LibRibbonButtonData() {
                Label = "Factura en Espera",
                Command = FacturaEnEsperaCommand,
                LargeImage = new Uri("/Galac.Adm.Uil.Venta;component/Images/F9.png",UriKind.Relative),
                ToolTipDescription = "Indica que la factura será guardada para su posterior facturación.",
                ToolTipTitle = "Factura en Espera"
            });
            return vResult;
        }

        private LibRibbonGroupData CreateBuscarFacturaEnEsperaRibbonGroup() {
            LibRibbonGroupData vResult = new LibRibbonGroupData("");
            vResult.ControlDataCollection.Add(new LibRibbonButtonData() {
                Label = "Buscar Factura en Espera",
                Command = BuscarFacturaEnEsperaCommand,
                LargeImage = new Uri("/Galac.Adm.Uil.Venta;component/Images/F10.png",UriKind.Relative),
                ToolTipDescription = "Busca las facturas que estén en espera, para ser facturadas.",
                ToolTipTitle = "Buscar Factura en Espera"
            });
            return vResult;
        }

        private LibRibbonGroupData CreateNuevaFacturaRibbonGroup() {
            LibRibbonGroupData vResult = new LibRibbonGroupData("");
            vResult.ControlDataCollection.Add(new LibRibbonButtonData() {
                Label = "Nueva Factura",
                Command = NuevaFacturaCommand,
                LargeImage = new Uri("/Galac.Adm.Uil.Venta;component/Images/F8.png",UriKind.Relative),
                ToolTipDescription = "Limpia la pantalla para ingresar la factura",
                ToolTipTitle = "Nueva Factura"
            });
            return vResult;
        }

        private LibRibbonGroupData CreateBotonesLeyendaRibbonGroup() {
            LibRibbonGroupData vResult = new LibRibbonGroupData("Leyenda");
            vResult.ControlDataCollection.Add(new LibRibbonButtonData() {
                Label = "Ir a Cliente",
                Command = BuscarClienteLeyendaCommand,
                LargeImage = new Uri("/Galac.Adm.Uil.Venta;component/Images/Alt1.png",UriKind.Relative),
                ToolTipDescription = "Clientes a buscar o insertar.",
                ToolTipTitle = "Ir a Cliente",
                KeyTip = "Alt+F1"
            });
            vResult.ControlDataCollection.Add(new LibRibbonButtonData() {
                Label = "Ir al Código del Artículo",
                Command = BuscarArticuloLeyendaCommand,
                LargeImage = new Uri("/Galac.Adm.Uil.Venta;component/Images/F1.png",UriKind.Relative),
                ToolTipDescription = "Busca o inserta el artículo.",
                ToolTipTitle = "Ir al Código del  Artículo",
                KeyTip = "F1"
            });
            vResult.ControlDataCollection.Add(new LibRibbonButtonData() {
                Label = "Ir a Cantidad",
                Command = CambiarCantidadLeyendaCommand,
                LargeImage = new Uri("/Galac.Adm.Uil.Venta;component/Images/F3.png",UriKind.Relative),
                ToolTipDescription = "Para ingresar cantidad distinta.",
                ToolTipTitle = "Ir a Cantidad",
                KeyTip = "F3"
            });
            vResult.ControlDataCollection.Add(new LibRibbonButtonData() {
                Label = "Ir a los ítems de la factura",
                Command = MoverRegistrosGridCommand,
                LargeImage = new Uri("/Galac.Adm.Uil.Venta;component/Images/F4.png",UriKind.Relative),
                ToolTipDescription = "Para moverse en la lista de artículos",
                ToolTipTitle = "Ir a los ítems de la factura",
                KeyTip = "F4"
            });
            vResult.ControlDataCollection.Add(new LibRibbonButtonData() {
                Label = "Ir al Vendedor",
                Command = BuscarVendedorLeyendaCommand,
                LargeImage = new Uri("/Galac.Adm.Uil.Venta;component/Images/Alt5.png",UriKind.Relative),
                ToolTipDescription = "Ir al Vendedor",
                ToolTipTitle = "Ir al Vendedor",
                KeyTip = "Alt+F5"
            });
            vResult.ControlDataCollection.Add(new LibRibbonButtonData() {
                Label = "Buscar el precio de un artículo",
                Command = BuscarPrecioDeArticuloCommand,
                LargeImage = new Uri("/Galac.Adm.Uil.Venta;component/Images/Alt11.png",UriKind.Relative),
                ToolTipDescription = "Buscar el precio de un artículo",
                ToolTipTitle = "Buscar el precio de un artículo",
                KeyTip = "Alt + F11"
            });
            if(EsValidaFacturaParaDecretoIvaEspecial()) {
                vResult.ControlDataCollection.Add(new LibRibbonButtonData() {
                    Label = "Aplica Decreto I.V.A. Especial",
                    Command = AplicaDecretoIvaEspecialCommand,
                    LargeImage = new Uri("/Galac.Adm.Uil.Venta;component/Images/F11.png",UriKind.Relative),
                    ToolTipDescription = "Aplica Decreto I.V.A. Especial",
                    ToolTipTitle = "Aplica Decreto I.V.A. Especial",
                    KeyTip = "F11"
                });
            }
            vResult.ControlDataCollection.Add(new LibRibbonButtonData() {
                Label = "Editar Descripción de Artículo",
                Command = IrADescripcionGridCommand,
                LargeImage = new Uri("/Galac.Adm.Uil.Venta;component/Images/F12.png",UriKind.Relative),
                ToolTipDescription = "Editar Descripción de Artículo",
                ToolTipTitle = "Editar Descripción de Artículo",
                KeyTip = "F12"
            });
            vResult.ControlDataCollection.Add(new LibRibbonButtonData() {
                Label = "Abrir Consultor de Precios",
                Command = AbrirConsultorDePreciosCommand,
                LargeImage = new Uri("/Galac.Adm.Uil.Venta;component/Images/Alt12.png",UriKind.Relative),
                ToolTipDescription = "Consultar Precios de Artículos",
                ToolTipTitle = "Consultar Precios de Artículos",
                KeyTip = "Alt + F12"
            });
            vResult.ControlDataCollection.Add(new LibRibbonButtonData() {
                Label = "Moneda de Cobro",
                Command = BuscarMonedaDeCobroLeyendaCommand,
                LargeImage = new Uri("/Galac.Adm.Uil.Venta;component/Images/Alt10.png",UriKind.Relative),
                ToolTipDescription = "Seleccionar Moneda de Cobro",
                ToolTipTitle = "Seleccionar Moneda de Cobro",
                KeyTip = "Alt + F10"
            });

            vResult.ControlDataCollection.Add(new LibRibbonButtonData() {
                Label = "Asignar descuento",
                Command = AsignarDescuentoCommand,
                LargeImage = new Uri("/Galac.Adm.Uil.Venta;component/Images/Alt9.png",UriKind.Relative),
                ToolTipDescription = "Asigne descuentos a esta factura",
                ToolTipTitle = "Asignar descuento",
                KeyTip = "Alt + F9"
            });

            return vResult;
        }
        #endregion Ribbon

        #region Factura En Espera
        FacturaRapidaViewModel CreateNewElementForFacturaEnEspera(FacturaRapida valModel,eAccionSR valAction) {
            var vNewModel = valModel;
            return new FacturaRapidaViewModel(vNewModel,valAction);
        }

        private void CargarValoresdeFacturaEnEspera(FkFacturaRapidaViewModel valConexionFacturaRapida) {
            FacturaRapida vFacturaEnEspera = new FacturaRapida();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania",valConexionFacturaRapida.ConsecutivoCompania);
            vParams.AddInString("Numero",valConexionFacturaRapida.Numero,11);
            vParams.AddInEnum("TipoDeDocumento",LibConvert.EnumToDbValue((int)eTipoDocumentoFactura.ComprobanteFiscal));
            vParams.AddInEnum("StatusFactura",LibConvert.EnumToDbValue((int)eStatusFactura.Borrador));
            vFacturaEnEspera = BusinessComponent.GetData(eProcessMessageType.SpName,"FacturaRapidaGET",vParams.Get(),UseDetail).FirstOrDefault();
            if(vFacturaEnEspera != null) {
                RecargarValoresDeFactura(vFacturaEnEspera);
                if(!EsValidaFacturaParaDecretoIvaEspecial()) {
                    AplicaDecretoIvaEspecial = false;
                }
                CambioMostrarTotalEnDivisas = vFacturaEnEspera.CambioMostrarTotalEnDivisas;
                InitializeLookAndFeel(GetModel());
                DetailFacturaRapidaDetalle = new FacturaRapidaDetalleMngViewModel(this,Model.DetailFacturaRapidaDetalle,Action);
                if(!EsValidaFacturaParaDecretoIvaEspecial()) {
                    DetailFacturaRapidaDetalle.SelectedIndex = 0;
                    ActualizaSaldos(true);
                }
                InitializeDetails();
                NotificarcambiosEnTodasLasPropiedades();
                ActualizarValoresCuadrosDeBusqueda(CuadroDeBusquedaDeArticulosViewModel,string.Empty);
                ActualizarValoresCuadrosDeBusqueda(CuadroDeBusquedaDeClientesViewModel,NombreCliente);
                ReloadRelatedConnections();
                TotalDeItems = DetailFacturaRapidaDetalle.Items.Sum(p => p.Cantidad);
                CalcularTotalFacturaDivisas();
                AsignarTasaDeCambioDeMonedaDeCobroYParaMostrarTotales(true);
            }
        }
        #endregion

        private void RecargarValoresDeFactura(FacturaRapida valFacturaEnEsperaModel) {
            GetModel().GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.DeclaredOnly)
            .Where(P => !(LibString.S1IsEqualToS2(P.Name,"TipoDeDocumento") || LibString.S1IsEqualToS2(P.Name,"FormaDeCobro")
            || LibString.S1IsEqualToS2(P.Name,"TipoDeVenta") || LibString.S1IsEqualToS2(P.Name,"StatusFactura")
            || LibString.S1IsEqualToS2(P.Name,"NivelDePrecio") || LibString.S1IsEqualToS2(P.Name,"FormaDeLaInicial")
            || LibString.S1IsEqualToS2(P.Name,"Talonario") || LibString.S1IsEqualToS2(P.Name,"FormaDePago")
            || LibString.S1IsEqualToS2(P.Name,"TipoDeTransaccion") || LibString.S1IsEqualToS2(P.Name,"ReservarMercancia")
            || LibString.S1IsEqualToS2(P.Name,"Cancelada") || LibString.S1IsEqualToS2(P.Name,"UsarDireccionFiscal")
            || LibString.S1IsEqualToS2(P.Name,"EditarMontoCuota") || LibString.S1IsEqualToS2(P.Name,"UsaMaquinaFiscal")
            || LibString.S1IsEqualToS2(P.Name,"SeRetuvoIVA") || LibString.S1IsEqualToS2(P.Name,"FacturaConPreciosSinIva")
            || LibString.S1IsEqualToS2(P.Name,"GeneraCobroDirecto") || LibString.S1IsEqualToS2(P.Name,"Devolucion")
            || LibString.S1IsEqualToS2(P.Name,"RealizoCierreZ") || LibString.S1IsEqualToS2(P.Name,"AplicarPromocion")
            || LibString.S1IsEqualToS2(P.Name,"RealizoCierreX") || LibString.S1IsEqualToS2(P.Name,"GeneradaPorNotaEntrega")
            || LibString.S1IsEqualToS2(P.Name,"ImprimeFiscal") || LibString.S1IsEqualToS2(P.Name,"EsDiferida")
            || LibString.S1IsEqualToS2(P.Name,"EsOriginalmenteDiferida") || LibString.S1IsEqualToS2(P.Name,"SeContabilizoIvaDiferido")
            || LibString.S1IsEqualToS2(P.Name,"AplicaDecretoIvaEspecial") || LibString.S1IsEqualToS2(P.Name,"EsGeneradaPorPuntoDeVenta")
            || LibString.S1IsEqualToS2(P.Name,"InsertadaManualmente") || LibString.S1IsEqualToS2(P.Name,"FacturaHistorica")
            || LibString.S1IsEqualToS2(P.Name,"CambioMostrarTotalEnDivisas")))
            .ToList().ForEach(p => {
                if(p.PropertyType != typeof(RelayCommand)) {
                    if(valFacturaEnEsperaModel.GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.DeclaredOnly)
                            .ToList().Where(q => q.Name == p.Name).Count() > 0) {
                        LibReflection.SetPropertyValue(GetModel(),p.Name,LibReflection.GetPropertyValue(valFacturaEnEsperaModel,p.Name));
                    }
                }
            });
        }

        private void NotificarcambiosEnTodasLasPropiedades() {
            GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.DeclaredOnly)
              .ToList().ForEach(p => RaisePropertyChanged(p.Name));
        }

        #region Command
        protected override void InitializeCommands() {
            base.InitializeCommands();
            ChooseNumeroRIFCommand = new RelayCommand<string>(ExecuteChooseNumeroRIFCommand);
            ChooseNombreClienteCommand = new RelayCommand<string>(ExecuteChooseNombreClienteCommand);
            ChooseCodigoVendedorCommand = new RelayCommand<string>(ExecuteChooseCodigoVendedorCommand);
            ChooseNombreVendedorCommand = new RelayCommand<string>(ExecuteChooseNombreVendedorCommand);
            ChooseArticuloCommand = new RelayCommand<string>(ExecuteChooseArticuloCommand);
            InsertaClienteCommand = new RelayCommand(ExecuteInsertaClienteCommand,CanExecuteInsertaClienteCommand);
            BuscarFacturaEnEsperaCommand = new RelayCommand(ExecuteBuscarFacturaEnEsperaCommand,CanExecuteBuscarFacturaEnEsperaCommand);
            FacturaEnEsperaCommand = new RelayCommand(ExecuteFacturaEnEsperaCommand,CanExecuteFacturaEnEsperaCommand);
            CobrarCommand = new RelayCommand(ExecuteCobrarCommand,CanExecuteCobrarCommand);
            BuscarArticuloCommand = new RelayCommand(ExecuteBuscarArticuloCommand,CanExecuteBuscarArticuloCommand);
            MoverRegistrosGridCommand = new RelayCommand(ExecuteMoverRegistrosGridCommand,CanExecuteMoverRegistrosGridCommand);
            BuscarVendedorCommand = new RelayCommand(ExecuteBuscarVendedorCommand,CanExecuteBuscarVendedorCommand);
            BuscarClienteCommand = new RelayCommand(ExecuteBuscarClienteCommand,CanExecuteBuscarClienteCommand);
            CantidadCommand = new RelayCommand(ExecuteCantidadCommand,CanExecuteCantidadCommand);
            BuscarClienteLeyendaCommand = new RelayCommand(ExecuteBuscarClienteCommand,CanExecuteLeyendaCommand);
            BuscarArticuloLeyendaCommand = new RelayCommand(ExecuteBuscarArticuloCommand,CanExecuteLeyendaCommand);
            CambiarCantidadLeyendaCommand = new RelayCommand(ExecuteCantidadCommand,CanExecuteLeyendaCommand);
            BuscarVendedorLeyendaCommand = new RelayCommand(ExecuteBuscarVendedorCommand,CanExecuteLeyendaCommand);
            AplicaDecretoIvaEspecialCommand = new RelayCommand(ExecuteAplicaDecretoIvaEspecialCommand,CanExecuteAplicaDecretoIvaCommand);
            NuevaFacturaCommand = new RelayCommand(ExecuteNuevaFacturaCommand,CanExecuteLeyendaCommand);
            BuscarPrecioDeArticuloCommand = new RelayCommand(ExecuteBuscarPrecioArticuloCommand);
            IrADescripcionGridCommand = new RelayCommand(ExecuteIrADescripcionGridCommand,CanExecuteIrADescripcionGridCommand);
            AbrirConsultorDePreciosCommand = new RelayCommand(ExecuteAbrirConsultorDePreciosCommand);
            ChooseNombreMonedaDeCobroCommand = new RelayCommand<string>(ExecuteChooseNombreMonedaDeCobroCommand);
            BuscarMonedaDeCobroCommand = new RelayCommand(ExecuteBuscarMonedaDeCobroCommand,CanExecuteBuscarMonedaDeCobroCommand);
            BuscarMonedaDeCobroLeyendaCommand = new RelayCommand(ExecuteBuscarMonedaDeCobroCommand,CanExecuteLeyendaCommand);
            AsignarDescuentoCommand = new RelayCommand(ExecuteAsignarDescuentoCommand,CanExecuteAsignarDescuentoCommand);
        }

        private void ExecuteAbrirConsultorDePreciosCommand() {
            var vVerificadorDePreciosViewModel = new VerificadorDePreciosViewModel();
            vVerificadorDePreciosViewModel.InitializeViewModel(eAccionSR.Abrir);
            var verificador = new GSVerificadorDePreciosView(vVerificadorDePreciosViewModel);
            switch(vVerificadorDePreciosViewModel.UsaMonedaExtranjera) {
            case true:
                if(vVerificadorDePreciosViewModel.UsaMostrarPreciosEnDivisa) {
                    if(vVerificadorDePreciosViewModel.AsignaTasaDelDia()) {
                        verificador.Show();
                    }
                } else {
                    verificador.Show();
                }
                break;
            case false:
                verificador.Show();
                break;
            }
        }

        private void ExecuteChooseNombreMonedaDeCobroCommand(string valNombre) {
            try {
                if(valNombre == null) {
                    valNombre = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Nombre",valNombre);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("Activa",LibConvert.BoolToSN(true));
                vFixedCriteria.Add("TipoDeMoneda",eBooleanOperatorType.IdentityEquality,eTipoDeMoneda.Fisica);
                AgregarCriteriaParaExcluirMonedasLocalesNoVigentesAlDiaActual(ref vFixedCriteria);
                AgregarCriteriaParaExcluirLasMonedasExtranjerasSiAplica(ref vFixedCriteria);
                FkMonedaViewModel vConexionNombreMonedaDeCobro = ChooseRecord<FkMonedaViewModel>("Moneda",vDefaultCriteria,vFixedCriteria,string.Empty);
                ConexionNombreMonedaDeCobro = null;
                if(vConexionNombreMonedaDeCobro != null) {
                    ConexionNombreMonedaDeCobro = vConexionNombreMonedaDeCobro;
                    CodigoMonedaDeCobro = ConexionNombreMonedaDeCobro.Codigo;
                    NombreMonedaDeCobro = ConexionNombreMonedaDeCobro.Nombre;
                } else {
                    ConexionNombreMonedaDeCobro = FirstConnectionRecordOrDefault<FkMonedaViewModel>("Moneda",LibSearchCriteria.CreateCriteriaFromText("Codigo",_clsNoComun.InstanceMonedaLocalActual.GetHoyCodigoMoneda()));
                    CodigoMonedaDeCobro = ConexionNombreMonedaDeCobro.Codigo;
                    NombreMonedaDeCobro = ConexionNombreMonedaDeCobro.Nombre;
                }
                AsignarTasaDeCambioDeMonedaDeCobroYParaMostrarTotales();
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx,ModuleName);
            }
        }

        private void AgregarCriteriaParaExcluirMonedasLocalesNoVigentesAlDiaActual(ref LibSearchCriteria vFixedCriteria) {
            XElement vXmlMonedaLocales = ((IMonedaLocalPdn)new clsMonedaLocalProcesos()).BusquedaTodasLasMonedasLocales(LibDefGen.ProgramInfo.Country);
            IList<MonedaLocalActual> vListaDeMonedaLocales = new List<MonedaLocalActual>();
            vListaDeMonedaLocales = vXmlMonedaLocales != null ? LibParserHelper.ParseToList<MonedaLocalActual>(new XDocument(vXmlMonedaLocales)) : null;
            if(vListaDeMonedaLocales != null) {
                foreach(MonedaLocalActual vMoneda in vListaDeMonedaLocales) {
                    if(vMoneda.CodigoMoneda != _clsNoComun.InstanceMonedaLocalActual.GetHoyCodigoMoneda()) {
                        vFixedCriteria.Add("Codigo",eBooleanOperatorType.IdentityInequality,vMoneda.CodigoMoneda);
                    }
                }
            }
        }

        private void AgregarCriteriaParaExcluirLasMonedasExtranjerasSiAplica(ref LibSearchCriteria vFixedCriteria) {
            FkMonedaViewModel insMonedaExtranjera = new FkMonedaViewModel();
            string vCodigoMonedaLocal = _clsNoComun.InstanceMonedaLocalActual.CodigoMoneda(LibDate.Today());
            if (EmpresaUsaMonedaExtranjeraComoPredeterminada(ref insMonedaExtranjera)) {
                vFixedCriteria.Add("Codigo", eBooleanOperatorType.IdentityEquality, vCodigoMonedaLocal);
                vFixedCriteria.Add("Codigo", eBooleanOperatorType.IdentityEquality, insMonedaExtranjera.Codigo, eLogicOperatorType.Or);
            }
        }

        protected void ExecuteCommandsRaiseCanExecuteChanged() {
            //base.ExecuteCommandsRaiseCanExecuteChanged();
            BuscarFacturaEnEsperaCommand.RaiseCanExecuteChanged();
            FacturaEnEsperaCommand.RaiseCanExecuteChanged();
            CobrarCommand.RaiseCanExecuteChanged();
            CantidadCommand.RaiseCanExecuteChanged();
        }

        private bool CanExecuteBuscarFacturaEnEsperaCommand() {
            return true;// && LibSecurityManager.CurrentUserHasAccessTo(ModuleName, "Recalcular");
        }

        private void ExecuteBuscarFacturaEnEsperaCommand() {
            try {
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("dbo.Gv_FacturaRapida_B1.TipoDeDocumento",eTipoDocumentoFactura.ComprobanteFiscal);
                vFixedCriteria.Add("dbo.Gv_FacturaRapida_B1.EsGeneradaPorPuntoDeVenta",true);
                vFixedCriteria.Add("dbo.Gv_FacturaRapida_B1.StatusFactura",eStatusFactura.Borrador);
                vFixedCriteria.Add("dbo.Gv_FacturaRapida_B1.ConsecutivoCompania",LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"));
                var vConexionFacturaRapida = ChooseRecord<FkFacturaRapidaViewModel>("Punto de Venta",null,vFixedCriteria,string.Empty);
                if(vConexionFacturaRapida != null) {
                    EsFacturaEnEspera = true;
                    CargarValoresdeFacturaEnEspera(vConexionFacturaRapida);
                }
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
            }
        }

        private bool CanExecuteFacturaEnEsperaCommand() {
            return true;// && LibSecurityManager.CurrentUserHasAccessTo(ModuleName, "Recalcular");
        }

        private void ExecuteFacturaEnEsperaCommand() {
            try {
                if(!IsValid) {
                    LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(new GalacValidationException(Error),ModuleName,ModuleName);
                    return;
                }
                if(CambioABolivares > 0) {
                    if(ElMontoEsMayorACero()) {
                        if(EsFacturaEnEspera) {
                            Action = eAccionSR.Modificar;
                        }
                        /*if(_clsNoComun.InstanceMonedaLocalActual.EsMonedaLocalDelPais(CodigoMonedaDeCobro)) {
                            CambioMostrarTotalEnDivisas = 1;
                        }*/
                        ExecuteAction();
                        if(Action == eAccionSR.Modificar) {
                            InitializeViewModel(eAccionSR.Insertar);
                            NotificarcambiosEnTodasLasPropiedades();
                            EsFacturaEnEspera = false;
                        }
                        if(IsValid) {
                            RecargarValoresDeFactura(new FacturaRapida());
                            InitializeViewModel(eAccionSR.Insertar);
                            AsignarTasaDeCambioDeMonedaDeCobroYParaMostrarTotales();
                            NotificarcambiosEnTodasLasPropiedades();
                            ActualizarValoresCuadrosDeBusqueda(CuadroDeBusquedaDeArticulosViewModel,string.Empty);
                            ActualizarValoresCuadrosDeBusqueda(CuadroDeBusquedaDeClientesViewModel,string.Empty);
                            MoverFocoACliente();
                        }
                    }
                } else {
                    LibMessages.MessageBox.Alert(this,"La tasa de cambio no es válida\nIngrese una tasa mayor a cero.","Tasa de cambio incorrecta");
                }

            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
            }
        }

        private bool CanExecuteCobrarCommand() {
            return true;
        }

        private void ExecuteCobrarCommand() {
            try {
                if(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetEnum("FacturaRapida","PermitirSobregiro") != (int)ePermitirSobregiro.NoChequearExistencia) {
                    if(!HayExistenciaParaFacturar()) {
                        return;
                    }
                }
                if(EsFacturaEnEspera) {
                    Action = eAccionSR.Modificar;
                    FacturaRapidaACobrar = null;
                    FacturaRapidaACobrar = this.GetModel().Clone();
                }
                MoveFocusIfNecessary();
                if(!IsValid) {
                    EsFacturaEnEspera = false;
                    LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(new GalacValidationException(Error),ModuleName,ModuleName);
                    return;
                }

                if(ElMontoEsMayorACero()) {
                    /*if(_clsNoComun.InstanceMonedaLocalActual.EsMonedaLocalDelPais(CodigoMonedaDeCobro)) {
                        CambioMostrarTotalEnDivisas = 1;
                    }*/
                    ExecuteAction();
                    if(DialogResult) {
                        bool vUsaCobroDirectoMultimoneda = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "UsaCobroDirectoEnMultimoneda");
                        if(vUsaCobroDirectoMultimoneda || EmpresaUsaMonedaExtranjeraComoPredeterminada()) {
                            CobroRapidoMultimonedaViewModel vViewModelCobroMultimoneda = new CobroRapidoMultimonedaViewModel(Action,FacturaRapidaACobrar,ListDeCobroMaster,_AlicuotaIvaASustituir,CambioMostrarTotalEnDivisas,false);
                            vViewModelCobroMultimoneda.XmlDatosImprFiscal = _XmlDatosImprFiscal;
                            vViewModelCobroMultimoneda.SeCobro += (arg) => vResultCobro = arg;
                            LibMessages.EditViewModel.ShowEditor(vViewModelCobroMultimoneda,true);
                            if(vResultCobro) {
                                SiguienteFactura();
                                ActualizarValoresCuadrosDeBusqueda(CuadroDeBusquedaDeArticulosViewModel,string.Empty);
                                ActualizarValoresCuadrosDeBusqueda(CuadroDeBusquedaDeClientesViewModel,string.Empty);
                            } else {
                                FacturaEnEspera();
                            }
                        } else {
                            CobroDeFacturaRapidaViewModel vViewModel = new CobroDeFacturaRapidaViewModel();
                            vViewModel.InitializeViewModel(Action,FacturaRapidaACobrar,ListDeCobroMaster,_AlicuotaIvaASustituir);
                            vViewModel.XmlDatosImprFiscal = _XmlDatosImprFiscal;
                            bool result = LibMessages.EditViewModel.ShowEditor(vViewModel,true);
                            if(vViewModel.DialogResult) {
                                SiguienteFactura();
                                ActualizarValoresCuadrosDeBusqueda(CuadroDeBusquedaDeArticulosViewModel,string.Empty);
                                ActualizarValoresCuadrosDeBusqueda(CuadroDeBusquedaDeClientesViewModel,string.Empty);
                            } else {
                                FacturaEnEspera();
                            }
                        }
                    }
                }
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
            }
        }

        private void ExecuteChooseNumeroRIFCommand(string valNumeroRIF) {
            try {
                if(valNumeroRIF == null) {
                    valNumeroRIF = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteria("Gv_Cliente_B1.NumeroRIF",valNumeroRIF);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("Gv_Cliente_B1.ConsecutivoCompania",LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"));
                var vConexionCliente = ChooseRecord<FkClienteViewModel>("Cliente",vDefaultCriteria,vFixedCriteria,string.Empty);
                AplicaDecretoIvaEspecial = false;
                if(vConexionCliente != null) {
                    ConexionCliente = vConexionCliente;
                    CuadroDeBusquedaDeClientesViewModel.Filter = vConexionCliente.Nombre;
                    CuadroDeBusquedaDeClientesViewModel.HideListCommand.Execute(null);
                } else {
                    if(LibMessages.MessageBox.YesNo(this,"¿Desea ingresar el cliente?","Información")) {
                        EscogeCliente(vDefaultCriteria,vFixedCriteria,valNumeroRIF,"");
                    } else {
                        ConexionCliente = null;
                    }
                }
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx,ModuleName);
            }
        }

        private void ExecuteChooseNombreClienteCommand(string valNombre) {
            try {
                if(valNombre == null) {
                    valNombre = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Gv_Cliente_B1.Nombre",valNombre);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("Gv_Cliente_B1.ConsecutivoCompania",LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"));
                var vConexionNombreCliente = ChooseRecord<FkClienteViewModel>("Cliente",vDefaultCriteria,vFixedCriteria,string.Empty);
                AplicaDecretoIvaEspecial = false;
                if(vConexionNombreCliente != null) {
                    ConexionCliente = vConexionNombreCliente;
                    CuadroDeBusquedaDeClientesViewModel.Filter = vConexionNombreCliente.Nombre;
                    CuadroDeBusquedaDeClientesViewModel.HideListCommand.Execute(null);
                } else {
                    if(LibMessages.MessageBox.YesNo(this,"¿Desea ingresar el cliente?","Información")) {
                        EscogeCliente(vDefaultCriteria,vFixedCriteria,"",valNombre);
                    } else {
                        ConexionCliente = null;
                    }
                }
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx,ModuleName);
            }
        }

        private bool CanExecuteCalculadoraCommand() {
            return true;
        }

        private void ExecuteCalculadoraCommand() {
            try {
                //Calculadora.ViewModel.CalculadoraViewModel vViewModel = new Calculadora.ViewModel.CalculadoraViewModel();
                //bool result = LibMessages.EditViewModel.ShowEditor(vViewModel,false);

            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
            }
        }

        private bool CanExecuteInsertaClienteCommand() {
            return true;
        }

        private void ExecuteInsertaClienteCommand() {
            try {
                ClienteInsercionRapidaViewModel vViewModel = new ClienteInsercionRapidaViewModel();
                bool result = LibMessages.EditViewModel.ShowEditor(vViewModel,true);

            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
            }
        }

        private void ExecuteChooseArticuloCommand(string valCodigo) {
            decimal vPrecioConIva = 0;
            decimal vPrecioSinIva = 0;
            decimal vPrecioBase = 0;
            decimal vPeso = 0;

            if(!IsEnterKeyPressedArticulo) {
                return;
            }

            try {
                if(valCodigo == null) {
                    valCodigo = string.Empty;
                }

                if(_UsaPesoEnCodigo || _UsaPrecioEnCodigo) {
                    DetailFacturaRapidaDetalle.ProcesarArticuloPorPesoOPrecio(ref valCodigo,ref vPrecioBase,ref vPeso);
                    if(LibString.IsNullOrEmpty(valCodigo)) {
                        return;
                    }
                }

                ConexionArticulo = null;
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteria("dbo.Gv_ArticuloInventario_B1.Codigo",valCodigo);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("dbo.Gv_ArticuloInventario_B1.ConsecutivoCompania",LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"));
                vFixedCriteria.Add(LibSearchCriteria.CreateCriteria("TipoArticuloInv",eTipoArticuloInv.Simple),eLogicOperatorType.And);
                var vConexionArticulo = ChooseRecord<FkArticuloInventarioViewModel>("Articulo Inventario",vDefaultCriteria,vFixedCriteria,string.Empty);
                if(vConexionArticulo != null) {
                    if(_UsaPrecioEnCodigo && vPrecioBase != 0) {
                        DetailFacturaRapidaDetalle.CalcularPreciosParaEtiquetas(EmpresaUsaPrecioSinIva,_EtiquetaIncluyeIva,vPrecioBase,
                        vConexionArticulo.PorcentajeBaseImponible,vConexionArticulo.AlicuotaIVA,ref vPrecioSinIva,ref vPrecioConIva);
                        vConexionArticulo.PrecioSinIVA = vPrecioSinIva;
                        vConexionArticulo.PrecioConIVA = vPrecioConIva;
                    } else if(_UsaPesoEnCodigo && vPeso != 0) {
                        Cantidad = vPeso;
                    }
                    if(UsaBalanzaEnPOS && vConexionArticulo.UsaBalanza) {
                        BalanzaTomarPesoViewModel.IniciarBalanza();
                        LibMessages.EditViewModel.ShowEditor(_BalanzaTomarPesoViewModel,true);
                        vPeso = BalanzaTomarPesoViewModel.PesoBalanza;
                        Cantidad = vPeso;
                    }
                    ConexionArticulo = vConexionArticulo;
                    AgregarArticuloAlGridSiNecesario();
                    RaisePropertyChanged("DetailFacturaRapidaDetalle");
                }
                IsEnterKeyPressedArticulo = false;
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx,ModuleName);
            }
        }

        private void ExecuteBuscarPrecioArticuloCommand() {
            try {
                LibSearchCriteria vDefaultCriteria = null;
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("dbo.Gv_ArticuloInventario_B1.ConsecutivoCompania",LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"));
                vFixedCriteria.Add(LibSearchCriteria.CreateCriteria("TipoArticuloInv",eTipoArticuloInv.Simple),eLogicOperatorType.And);
                var vConexionArticulo = ChooseRecord<FkArticuloInventarioBuscarViewModel>("Articulo Inventario",vDefaultCriteria,vFixedCriteria,string.Empty);
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx,ModuleName);
            }
        }

        private void ExecuteChooseCodigoVendedorCommand(string valcodigo) {
            try {
                if(valcodigo == null) {
                    valcodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo",valcodigo);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("ConsecutivoCompania",Mfc.GetInt("Compania"));
                ConexionCodigoVendedor = ChooseRecord<FkVendedorViewModel>("Vendedor",vDefaultCriteria,vFixedCriteria,string.Empty);
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx,ModuleName);
            }
        }

        private void ExecuteChooseNombreVendedorCommand(string valNombre) {
            try {
                if(valNombre == null) {
                    valNombre = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Nombre",valNombre);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("ConsecutivoCompania",Mfc.GetInt("Compania"));
                ConexionNombreVendedor = null;
                ConexionNombreVendedor = ChooseRecord<FkVendedorViewModel>("Vendedor",vDefaultCriteria,vFixedCriteria,string.Empty);

            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx,ModuleName);
            }
        }

        private bool CanExecuteCantidadCommand() {
            return true;
        }

        private void ExecuteCantidadCommand() {
            try {
                FocoEnArticulo = false;
                Articulo = string.Empty;
                Descripcion = string.Empty;
                DescripcionCorta = string.Empty;
                RaiseMoveFocus(CantidadPropertyName);
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx,ModuleName);
            }
        }

        private bool CanExecuteBuscarClienteCommand() {
            return true;
        }

        private void ExecuteBuscarClienteCommand() {
            try {
                if(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("FacturaRapida","UsaBusquedaDinamicaEnPuntoDeVenta"))
                    EnfocarCliente();
                else
                    MoverFocoACliente();
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
            }
        }

        private bool CanExecuteBuscarArticuloCommand() {
            return true;
        }

        private bool CanExecuteBuscarVendedorCommand() {
            return true;
        }

        private void ExecuteBuscarVendedorCommand() {
            try {
                RaiseMoveFocus(NombreVendedorPropertyName);
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
            }
        }

        private bool CanExecuteBuscarMonedaDeCobroCommand() {
            return true;
        }

        private bool CanExecuteAsignarDescuentoCommand() {
            return MostrarPorcentajeDescuento;//LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("FacturaRapida","OtorgarDescuento");
        }

        private void ExecuteBuscarMonedaDeCobroCommand() {
            try {
                RaiseMoveFocus(NombreMonedaDeCobroPropertyName);
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
            }
        }

        private void ExecuteAsignarDescuentoCommand() {
            try {
                RaiseMoveFocus(PorcentajeDescuentoPropertyName);
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
            }
        }

        private void ExecuteBuscarArticuloCommand() {
            try {
                RaiseMoveFocus(ArticuloPropertyName);
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
            }
        }

        private bool CanExecuteMoverRegistrosGridCommand() {
            return true;
        }

        private void ExecuteMoverRegistrosGridCommand() {
            try {
                if(DetailFacturaRapidaDetalle.Items.Count > 0) {
                    DetailFacturaRapidaDetalle.SelectedIndex = DetailFacturaRapidaDetalle.Items.Count - 1;
                    DetailFacturaRapidaDetalle.SelectedItem = DetailFacturaRapidaDetalle.Items[DetailFacturaRapidaDetalle.SelectedIndex];
                    RaiseIrAlGridEvent();
                }
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
            }
        }

        private bool CanExecuteLeyendaCommand() {
            return true;
        }

        private bool CanExecuteAplicaDecretoIvaCommand() {
            if(EsValidaFacturaParaDecretoIvaEspecial()) {
                return true;
            } else {
                return false;
            }

        }

        public void ExecuteAplicaDecretoIvaEspecialCommand() {
            bool vResult = false;
            try {
                if(LibText.Len(NumeroRIF) > 0 && TotalFactura > 0 && EmpresaUsaPrecioSinIva) {
                    //if (ValidarAplicarIvaEspecialPorArticulo() && ValidarAplicarIvaEspecialPorCliente(AplicaDecretoIvaEspecial)) {
                    if(ValidarAplicarIvaEspecialPorArticulo()) {
                        if(AplicaDecretoIvaEspecial) {
                            vResult = false;
                        } else {
                            vResult = true;
                        }
                    }
                }
                RaiseMoveFocus(ArticuloPropertyName);
                AplicaDecretoIvaEspecial = vResult;
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
            }
        }

        private void ExecuteNuevaFacturaCommand() {
            try {
                ActualizarValoresCuadrosDeBusqueda(CuadroDeBusquedaDeArticulosViewModel,string.Empty);
                ActualizarValoresCuadrosDeBusqueda(CuadroDeBusquedaDeClientesViewModel,string.Empty);
                EsFacturaEnEspera = false;
                Cantidad = 1;
                TotalDeItems = 0;
                RecargarValoresDeFactura(new FacturaRapida());
                InitializeViewModel(eAccionSR.Insertar);
                AsignarTasaDeCambioDeMonedaDeCobroYParaMostrarTotales();
                NotificarcambiosEnTodasLasPropiedades();
                MoverFocoACliente();
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
            }
        }
        #endregion

        protected override bool CreateRecord() {
            bool vResult = base.CreateRecord();
            FacturaRapidaACobrar = null;
            FacturaRapidaACobrar = this.GetModel().Clone();
            FacturaRapidaACobrar.UsaMaquinaFiscalAsBool = true;
            return vResult;
        }

        protected override void ReloadRelatedConnections() {
            base.ReloadRelatedConnections();
            LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("ConsecutivoCompania",Mfc.GetInt("Compania"));
            vFixedCriteria.Add(LibSearchCriteria.CreateCriteria("NumeroRIF",Model.NumeroRIF),eLogicOperatorType.And);
            ConexionCliente = FirstConnectionRecordOrDefault<FkClienteViewModel>("Cliente",vFixedCriteria);

            vFixedCriteria = LibSearchCriteria.CreateCriteria("ConsecutivoCompania",Mfc.GetInt("Compania"));
            vFixedCriteria.Add(LibSearchCriteria.CreateCriteria("Nombre",Model.NombreCliente),eLogicOperatorType.And);
            ConexionCliente = FirstConnectionRecordOrDefault<FkClienteViewModel>("Cliente",vFixedCriteria);

            vFixedCriteria = LibSearchCriteria.CreateCriteria("ConsecutivoCompania",Mfc.GetInt("Compania"));
            vFixedCriteria.Add(LibSearchCriteria.CreateCriteria("Nombre",Model.NombreVendedor),eLogicOperatorType.And);
            //ConexionCodigoVendedor = FirstConnectionRecordOrDefault<FkVendedorViewModel>("Vendedor", LibSearchCriteria.CreateCriteria("Codigo", CodigoVendedor));
            ConexionNombreVendedor = FirstConnectionRecordOrDefault<FkVendedorViewModel>("Vendedor",vFixedCriteria);
        }

        private void EscogeCliente(LibSearchCriteria valDefaultCriteria,LibSearchCriteria valFixedCriteria,string valNumeroRIF,string valNombre) {
            ClienteInsercionRapidaViewModel vViewModel = new ClienteInsercionRapidaViewModel();
            vViewModel.InitLookAndFeel(TipoDeDocumentoIdentificacion,valNumeroRIF,valNombre);
            if(LibMessages.EditViewModel.ShowEditor(vViewModel,true)) {
                if(vViewModel.DialogResult) {
                    LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("ConsecutivoCompania",Mfc.GetInt("Compania"));
                    vFixedCriteria.Add(LibSearchCriteria.CreateCriteria("NumeroRIF",vViewModel.NumeroRIF),eLogicOperatorType.And);
                    ConexionCliente = FirstConnectionRecordOrDefault<FkClienteViewModel>("Cliente",vFixedCriteria);
                }
            }
        }

        private ValidationResult FechaValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else {
                if(LibDefGen.DateIsGreaterThanDateLimitForEnterData(Fecha,false,Action)) {
                    vResult = new ValidationResult(LibDefGen.TooltipMessageDateRestrictionDemoProgram("Fecha"));
                }
            }
            return vResult;
        }
        #endregion //Metodos Generados

        #region Cálculos de Montos de la factura
        public void ActualizaSaldos(bool valActualizar) {
            if(valActualizar) {
                DetailFacturaRapidaDetalle.ActualizaTotalRenglon();
                ActualizaTotalesDeFactura();
                if(AplicaDecretoIvaEspecial) {
                    BuscaInfoAlicuotaEspecial();
                    PorcentajeAlicuota1 = _PorcentajeAlicuotaEspecial;
                } else {
                    PorcentajeAlicuota1 = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetDecimal("FacturaRapida","PorcentajeAlicuota1");
                    _AlicuotaIvaASustituir = (int)eAplicacionAlicuota.No_Aplica;
                }
            }
            DetailFacturaRapidaDetalle.ActualizaTotalRenglon();
            ActualizaTotalesDeFactura();
        }

        private void ActualizaTotalesDeFactura() {
            decimal vDescuento = (Model.PorcentajeDescuento / 100);
            if(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("FacturaRapida","UsaPrecioSinIva") == true) {
                decimal vTotalMontoExento = DetailFacturaRapidaDetalle.MontoTotalExento(3);
                TotalMontoExento = CalcularDescuentoAlActualizarFactura(vTotalMontoExento,vDescuento);
                MontoIvaAlicuota1 = CalcularDescuentoAlActualizarFactura(DetailFacturaRapidaDetalle.MontoIvaGeneral(3),vDescuento);
                MontoGravableAlicuota1 = CalcularDescuentoAlActualizarFactura(DetailFacturaRapidaDetalle.MontoGravableGeneral(3),vDescuento);
                MontoIvaAlicuota2 = CalcularDescuentoAlActualizarFactura(DetailFacturaRapidaDetalle.MontoIvaReducida(3),vDescuento);
                MontoGravableAlicuota2 = CalcularDescuentoAlActualizarFactura(DetailFacturaRapidaDetalle.MontoGravableReducida(3),vDescuento);
                MontoIvaAlicuota3 = CalcularDescuentoAlActualizarFactura(DetailFacturaRapidaDetalle.MontoIvaExtendida(3),vDescuento);
                MontoGravableAlicuota3 = CalcularDescuentoAlActualizarFactura(DetailFacturaRapidaDetalle.MontoGravableExtendida(3),vDescuento);

                TotalIVA = MontoIvaAlicuota1 + MontoIvaAlicuota2 + MontoIvaAlicuota3;
                decimal vTotalBaseImponible = DetailFacturaRapidaDetalle.CalcularTotalMontoBaseImponible();
                TotalBaseImponible = CalcularDescuentoAlActualizarFactura(vTotalBaseImponible,vDescuento);
                TotalRenglones = vTotalMontoExento + vTotalBaseImponible;
                TotalFactura = TotalMontoExento + TotalIVA + TotalBaseImponible;

            } else {
                decimal vTotalMontoExento = LibMath.RoundToNDecimals(DetailFacturaRapidaDetalle.MontoTotalExento(3),2);

                MontoGravableAlicuota1 = LibMath.RoundToNDecimals(DetailFacturaRapidaDetalle.MontoGravableGeneralConIva(3),2);
                MontoGravableAlicuota2 = LibMath.RoundToNDecimals(DetailFacturaRapidaDetalle.MontoGravableReducidaConIva(3),2);
                MontoGravableAlicuota3 = LibMath.RoundToNDecimals(DetailFacturaRapidaDetalle.MontoGravableReducidaConIva(3),2);

                MontoIvaAlicuota1 = LibMath.RoundToNDecimals(MontoGravableAlicuota1 - (MontoGravableAlicuota1 / (1 + (PorcentajeAlicuota1 / 100))),2);
                MontoIvaAlicuota2 = LibMath.RoundToNDecimals(MontoGravableAlicuota2 - (MontoGravableAlicuota2 / (1 + (PorcentajeAlicuota2 / 100))),2);
                MontoIvaAlicuota3 = LibMath.RoundToNDecimals(MontoGravableAlicuota3 - (MontoGravableAlicuota3 / (1 + (PorcentajeAlicuota3 / 100))),2);

                decimal vBI1 = MontoGravableAlicuota1 - MontoIvaAlicuota1;
                decimal vBI2 = MontoGravableAlicuota2 - MontoIvaAlicuota2;
                decimal vBI3 = MontoGravableAlicuota3 - MontoIvaAlicuota3;

                CalcularDescuentoCuandoPrecioVieneConIVA(vTotalMontoExento,vBI1,vBI2,vBI3,vDescuento);                               

                TotalIVA = MontoIvaAlicuota1 + MontoIvaAlicuota2 + MontoIvaAlicuota3;
                decimal vTotalBaseImponibleSinDescuento = vBI1 + vBI2 + vBI3;
                TotalBaseImponible = MontoGravableAlicuota1 + MontoGravableAlicuota2 + MontoGravableAlicuota3;
                TotalFactura = TotalMontoExento + TotalBaseImponible + TotalIVA;
                TotalRenglones = vTotalMontoExento + vTotalBaseImponibleSinDescuento;
            }
            CalcularTotalDescuento();
            TotalDeItems = DetailFacturaRapidaDetalle.Items.Sum(p => p.Cantidad);
        }

        #endregion

        #region Métodos Programados
        private bool SumarArticuloExistenteEnGrid() {
            bool vResult = false;
            foreach(var item in DetailFacturaRapidaDetalle.Items) {
                if(item.Articulo == ConexionArticulo.Codigo) {
                    item.Cantidad += Cantidad;
                    vResult = true;
                    break;
                }
            }
            return vResult;
        }

        public void AgregarArticuloAlGridSiNecesario() {
            if(ConexionArticulo != null) {
                bool vYaExiste = DetailFacturaRapidaDetalle.Items.Any(i => i.Articulo == ConexionArticulo.Codigo);
                bool vAcumularItemsEnRenglonesDeFactura = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("FacturaRapida","AcumularItemsEnRenglonesDeFactura ");
                if(!vAcumularItemsEnRenglonesDeFactura || !vYaExiste) {
                    bool vUsaDivisaMonedaPredeterminada = EmpresaUsaMonedaExtranjeraComoPredeterminada();
                    bool vEsNecesarioUsarPrecioMe = _UsaListaDePrecioEnMonedaExtranjera || vUsaDivisaMonedaPredeterminada;
                    switch (vEsNecesarioUsarPrecioMe) {
                    case true:
                        int vNivelDePrecio = 0;
                        decimal vMePrecioSinIva = 0;
                        decimal vMePrecioConIva = 0;
                        decimal vCambio = vUsaDivisaMonedaPredeterminada ? 1 : CambioMostrarTotalEnDivisas;
                        vNivelDePrecio = new clsLibSaw().ObtenerNivelDePrecio(ConsecutivoCompania,CodigoCliente);
                        ObtenerPrecioConYSinIvaSegunNivelDePrecio(vNivelDePrecio,ref vMePrecioSinIva,ref vMePrecioConIva);
                        DetailFacturaRapidaDetalle.InsertRow(Articulo,Descripcion,Cantidad,vMePrecioSinIva * vCambio,vMePrecioConIva * CambioMostrarTotalEnDivisas,(eTipoDeAlicuota)ConexionArticulo.AlicuotaIVA,LibConvert.ToDec(ConexionArticulo.PorcentajeBaseImponible),ConexionArticulo.TipoDeArticulo,ConexionArticulo.TipoArticuloInv);
                        break;
                    case false:
                        DetailFacturaRapidaDetalle.InsertRow(Articulo,Descripcion,Cantidad,LibConvert.ToDec(ConexionArticulo.PrecioSinIVA),LibConvert.ToDec(ConexionArticulo.PrecioConIVA),(eTipoDeAlicuota)ConexionArticulo.AlicuotaIVA,LibConvert.ToDec(ConexionArticulo.PorcentajeBaseImponible),ConexionArticulo.TipoDeArticulo,ConexionArticulo.TipoArticuloInv);
                        break;
                    }
                } else {
                    DetailFacturaRapidaDetalle.Items
                        .Where(i => i.Articulo == ConexionArticulo.Codigo).ToList()
                        .ForEach(i => i.Cantidad += Cantidad);
                }
                if(EmpresaAplicaIVAEspecial != (int)eAplicacionAlicuota.No_Aplica) {
                    if(AplicaDecretoIvaEspecial == true) {
                        ValidaLaAlicuotaDelArticuloSeleccionado();
                    }
                }
                ActualizaSaldos(true);
                DetailFacturaRapidaDetalle.SelectedIndex = -1;
            }
            Cantidad = 1;
        }

        public event EventHandler IrAlGridEvent;

        private void RaiseIrAlGridEvent() {
            var handle = IrAlGridEvent;
            if(handle != null) {
                handle(this,EventArgs.Empty);
            }
        }

        public event EventHandler IrADescripcionGridEvent;

        private void RaiseIrADescripcionGridEvent() {
            var handle = IrADescripcionGridEvent;
            if(handle != null) {
                handle(this,EventArgs.Empty);
            }
        }

        public void LimpiarArticulo() {
            ConexionArticulo = null;
        }

        private void CargaVendedorGenerico() {
            IVendedorPdn insVendedor = new clsVendedorNav();
            CodigoVendedor = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("FacturaRapida","CodigoGenericoVendedor");
            NombreVendedor = insVendedor.BuscarNombreVendedor(Mfc.GetInt("Compania"),LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("FacturaRapida","CodigoGenericoVendedor"));
        }

        private void CargaDeAlicuotasIva() {
            PorcentajeAlicuota1 = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetDecimal("FacturaRapida","PorcentajeAlicuota1");
            PorcentajeAlicuota2 = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetDecimal("FacturaRapida","PorcentajeAlicuota2");
            PorcentajeAlicuota3 = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetDecimal("FacturaRapida","PorcentajeAlicuota3");
        }
        #endregion

        #region Validaciones

        private bool ElMontoEsMayorACero() {
            bool vResult = false;
            if(TotalFactura > 0) {
                vResult = true;
            } else {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.MessageBox.Alert(this,"El total de la factura debe ser mayor a 0 (cero).","Información");
            }
            return vResult;
        }

        private bool ValidarAplicarIvaEspecialPorCliente(bool vAplicaDecretoIvaEspecial) {
            bool vResult = true;
            bool vActivar = false;
            if(EmpresaAplicaIVAEspecial != (int)eAplicacionAlicuota.No_Aplica) {
                if(vAplicaDecretoIvaEspecial == false) {
                    vActivar = true;
                }
                if(vActivar == true) {
                    if(TipoDeVenta == eTipoDeVenta.AContribuyente) {
                        vResult = false;
                        AplicaDecretoIvaEspecial = false;
                        LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.MessageBox.Information(this,"Para aplicar el Decreto del IVA al " + _PorcentajeAlicuotaEspecial.ToString() + " %, el cliente debe ser 'No Contribuyente'.","Información");
                    }

                    if(LibText.SubString(NumeroRIF,0,1) == "J" || LibText.SubString(NumeroRIF,0,1) == "G") {
                        vResult = false;
                        AplicaDecretoIvaEspecial = false;
                        LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.MessageBox.Information(this,"Para aplicar el Decreto del IVA al " + _PorcentajeAlicuotaEspecial.ToString() + " %, el cliente debe tener otro tipo de RIF.","Información");
                    }
                }
            }
            return vResult;
        }

        private bool ValidarAplicarIvaEspecialPorArticulo() {
            bool vResult = true;
            if(EmpresaAplicaIVAEspecial == (int)eAplicacionAlicuota.No_Aplica) {
                vResult = false;
            }
            if(!ValidaLaAlicuotaDelArticulo()) {
                vResult = false;
            }
            //if (!TotalFacturaEsValidoParaDecreto2602()) {
            //    vResult = false;
            //}
            return vResult;
        }

        private bool TotalFacturaEsValidoParaDecreto2602() {
            bool vResult = true;
            decimal vTotalFacturaAValidar;
            vTotalFacturaAValidar = TotalMontoExento + TotalBaseImponible + TotalIVA;
            if(vTotalFacturaAValidar > MontoMaximoDecreto2602) {
                vResult = false;
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.MessageBox.Information(this,"Para aplicar el Decreto del IVA al 10 %, el Total de la factura, debe ser menor a 200.000,00 bs.","Información");
            }
            return vResult;
        }

        private bool ValidaLaAlicuotaDelArticulo() {
            bool vResult = true;
            if(DetailFacturaRapidaDetalle != null && DetailFacturaRapidaDetalle.Items != null) {
                if(EmpresaAplicaIVAEspecial == (int)eAplicacionAlicuota.Sustituir_Alicuota_Adicional) {
                    vResult = !(DetailFacturaRapidaDetalle.Items.Any(i => i.AlicuotaIva == eTipoDeAlicuota.Alicuota2));
                    if(!vResult) {
                        LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.MessageBox.Information(this,"En los Parámetros Administrativo tiene configurado aplicar el IVA al 10 % en Alícuota Reducida. Se desactivará la opción: 'Aplica Decreto I.V.A. Especial', en esta factura.","Información");
                    }
                }

                if(EmpresaAplicaIVAEspecial == (int)eAplicacionAlicuota.Sustituir_Alicuota_Adicional) {
                    vResult = !(DetailFacturaRapidaDetalle.Items.Any(i => i.AlicuotaIva == eTipoDeAlicuota.Alicuota3));
                    if(!vResult) {
                        LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.MessageBox.Information(this,"En los Parámetros Administrativo tiene configurado aplicar el IVA al 10 % en Alícuota Adicional. Se desactivará la opción: 'Aplica Decreto I.V.A. Especial', en esta factura.","Información");
                    }
                }
            }
            return vResult;
        }

        private bool ValidaLaAlicuotaDelArticuloSeleccionado() {
            bool vResult = true;
            if(DetailFacturaRapidaDetalle != null && DetailFacturaRapidaDetalle.Items.Count > 0) {
                if(EmpresaAplicaIVAEspecial == (int)eAplicacionAlicuota.Sustituir_Alicuota_Reducida) {
                    if(DetailFacturaRapidaDetalle.SelectedItem.AlicuotaIva == eTipoDeAlicuota.Alicuota2) {
                        AplicaDecretoIvaEspecial = false;
                        LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.MessageBox.Information(this,"En los Parámetros Administrativo tiene configurado aplicar el IVA al " + _PorcentajeAlicuotaEspecial.ToString() + " % en Alícuota Reducida, no pude seleccionar artículos de este tipo, para esta factura.","Información");
                    }
                }

                if(EmpresaAplicaIVAEspecial == (int)eAplicacionAlicuota.Sustituir_Alicuota_Adicional) {
                    if(DetailFacturaRapidaDetalle.SelectedItem.AlicuotaIva == eTipoDeAlicuota.Alicuota3) {
                        AplicaDecretoIvaEspecial = false;
                        LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.MessageBox.Information(this,"En los Parámetros Administrativo tiene configurado aplicar el IVA al " + _PorcentajeAlicuotaEspecial.ToString() + " % en Alícuota Adicional, no pude seleccionar artículos de este tipo, para esta factura.","Información");
                    }
                }
            }
            return vResult;
        }

        private bool AplicarDecretoImpuestoEspecial() {
            bool vResult = true;
            if(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("FacturaRapida","BuscarClienteXRifAlFacturar")) {
                if(TipoDeVenta == eTipoDeVenta.AContribuyente) {
                    vResult = false;
                    AplicaDecretoIvaEspecial = false;
                    LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.MessageBox.Information(this,"Para aplicar el Decreto del IVA al " + _PorcentajeAlicuotaEspecial.ToString() + " %, el cliente debe ser 'No Contribuyente'.","Información");
                }
                if(LibText.SubString(NumeroRIF,0,1) == "J" || LibText.SubString(NumeroRIF,0,1) == "G") {
                    vResult = false;
                    AplicaDecretoIvaEspecial = false;
                    LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.MessageBox.Information(this,"Para aplicar el Decreto del IVA al " + _PorcentajeAlicuotaEspecial.ToString() + " %, el cliente debe tener otro tipo de RIF.","Información");
                }
            }
            return vResult;
        }

        private bool EsPosibleMostrarTasaDeCambio() {
            bool vResult = false;
            bool vMuestraTotalEnDivisas = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "SeMuestraTotalEnDivisas");
            bool vEsFacturaEnMonedaLocal = _clsNoComun.InstanceMonedaLocalActual.EsMonedaLocalDelPais(CodigoMonedaDeCobro);
            if(EmpresaUsaMonedaExtranjeraComoPredeterminada()) {
                vResult = true;
            } else {
                if(!LibString.IsNullOrEmpty(NombreMonedaDeCobro)) {
                    if(vMuestraTotalEnDivisas || !vEsFacturaEnMonedaLocal) {
                        vResult = true;
                    }
                }
            }
            return vResult;
        }

        private bool EsPosibleMostrarTotalEnDivisas() {
            bool vResult = false;
            bool vMuestraTotalEnDivisas = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "SeMuestraTotalEnDivisas");
            if (vMuestraTotalEnDivisas && !EmpresaUsaMonedaExtranjeraComoPredeterminada()) {
                vResult = true;
            }
            return vResult;
        }
        #endregion

        private void MoverFocoACliente() {
            if(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("FacturaRapida","BuscarClienteXRifAlFacturar")
                && !LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("FacturaRapida","UsaBusquedaDinamicaEnPuntoDeVenta")) {
                RaiseMoveFocus(NumeroRIFPropertyName);
            } else {
                if(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("FacturaRapida","UsaBusquedaDinamicaEnPuntoDeVenta")) {
                    EnfocarCliente();
                } else {
                    RaiseMoveFocus(NombreClientePropertyName);
                }
            }
        }

        protected override bool CanExecuteAction() {
            return false;
        }

        #region Métodos Sobreescritos

        protected override void ExecuteAction() {
            try {
                MoveFocusIfNecessary();
                ExecuteProcessBeforeAction();

                if(!IsValid) {
                    if((Action != eAccionSR.Consultar) && (Action != eAccionSR.Eliminar)) {
                        LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(new GalacValidationException(Error),ModuleName,ModuleName);
                        return;
                    }
                }
                switch(Action) {
                case eAccionSR.Insertar:
                    DialogResult = CreateRecord();
                    break;
                case eAccionSR.Modificar:
                    DialogResult = UpdateRecord();
                    break;
                case eAccionSR.Eliminar:
                    DialogResult = DeleteRecord();
                    CloseOnActionComplete = true;
                    break;
                case eAccionSR.Custom:
                    ExecuteSpecialAction(CustomActionLabel);
                    break;
                default:
                    ExecuteSpecialAction(Action);
                    break;
                }

                ExecuteProcessAfterAction();
                IsDirty = false;
                if(!IsClosing && CloseOnActionComplete) {
                    RaiseRequestCloseEvent();
                }
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx,ModuleName);
                StopClosing = true;
            }
        }

        public override bool OnClosing() {
            bool vResult = base.OnClosing();
            Action = eAccionSR.Insertar;
            return vResult;
        }

        #endregion

        bool EsValidaFacturaParaDecretoIvaEspecial() {
            bool vResult = false;
            DateTime vInicio = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetDateTime("FacturaRapida","FechaInicioAlicuotaIva10Porciento");
            DateTime vFin = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetDateTime("FacturaRapida","FechaFinAlicuotaIva10Porciento");
            DateTime vFecha;

            if(EsFacturaEnEspera) {
                vFecha = LibDate.Today();
            } else {
                vFecha = Fecha;
            }

            if(LibDate.DateIsBetweenF1AndF2(vFecha,vInicio,vFin)) {
                vResult = true;
            }
            return vResult;
        }

        void CargarClienteGenerico() {
            if(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("FacturaRapida","UsaClienteGenericoAlFacturar")) {
                string vCodigo = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("FacturaRapida","CodigoGenericoCliente");
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteria("Gv_Cliente_B1.Codigo",vCodigo);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("Gv_Cliente_B1.ConsecutivoCompania",LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"));
                ConexionCliente = ChooseRecord<FkClienteViewModel>("Cliente",vDefaultCriteria,vFixedCriteria,string.Empty);
                CuadroDeBusquedaDeClientesViewModel.Filter = ConexionCliente.Nombre;
                CuadroDeBusquedaDeClientesViewModel.HideListCommand.Execute(null);
            } else {
                ConexionCliente = null;
            }
        }

        private bool CanExecuteIrADescripcionGridCommand() {
            return LibSecurityManager.CurrentUserHasAccessTo("Punto de Venta","Modificar Descripción del Item");
        }

        private void ExecuteIrADescripcionGridCommand() {
            try {
                if(DetailFacturaRapidaDetalle.Items.Count > 0) {
                    DetailFacturaRapidaDetalle.SelectedIndex = DetailFacturaRapidaDetalle.Items.Count - 1;
                    DetailFacturaRapidaDetalle.SelectedItem = DetailFacturaRapidaDetalle.Items[DetailFacturaRapidaDetalle.SelectedIndex];
                    RaiseIrADescripcionGridEvent();
                }
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
            }
        }

        bool HayExistenciaParaFacturar() {
            bool vResult = false;
            bool EsSerialORollo = false;
            decimal vCantidad = 0;

            for(int index = 0;index < DetailFacturaRapidaDetalle.Items.Count();index++) {
                vCantidad = 0;
                if(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("FacturaRapida","AcumularItemsEnRenglonesDeFactura") == false) {
                    for(int i = 0;i < DetailFacturaRapidaDetalle.Items.Count();i++) {
                        if(DetailFacturaRapidaDetalle.Items[index].Articulo == DetailFacturaRapidaDetalle.Items[i].Articulo)
                            vCantidad = vCantidad + DetailFacturaRapidaDetalle.Items[i].Cantidad;
                    }
                } else {
                    vCantidad = DetailFacturaRapidaDetalle.Items[index].Cantidad;
                }

                if(EsSerialORollo) {

                } else if(DetailFacturaRapidaDetalle.Items[index].TipoDeArticulo == eTipoDeArticulo.ProductoCompuesto) {
                    IArticuloInventarioPdn insArtInv = new clsArticuloInventarioNav();
                    XElement xProductosCompuestos = insArtInv.BuscarDetalleArticuloCompuesto(ConsecutivoCompania,DetailFacturaRapidaDetalle.Items[index].Articulo);
                    var vEntity = (from vRecord in xProductosCompuestos.Descendants("GpResult")
                                   select new {
                                       Codigo = LibXml.GetElementValueOrEmpty(vRecord,"Codigo"),
                                       TipoDeArticulo = LibImportData.ToInt(LibXml.GetElementValueOrEmpty(vRecord,"TipoDeArticulo")),
                                       TipoArticuloInv = LibImportData.ToInt(LibXml.GetElementValueOrEmpty(vRecord,"TipoArticuloInv")),
                                       Cantidad = LibImportData.ToDec(LibXml.GetElementValueOrEmpty(vRecord,"Cantidad"))
                                   }).Distinct();

                    if(vEntity != null) {
                        foreach(var vItem in vEntity) {
                            vResult = HayExistenciaParaLaSalida(vItem.Codigo,vItem.TipoDeArticulo,vItem.TipoArticuloInv,string.Empty,string.Empty,vCantidad * vItem.Cantidad);
                            return vResult;
                        }
                    }
                } else {
                    vResult = HayExistenciaParaLaSalida(DetailFacturaRapidaDetalle.Items[index].Articulo,(int)DetailFacturaRapidaDetalle.Items[index].TipoDeArticulo,(int)DetailFacturaRapidaDetalle.Items[index].TipoArticuloInv,string.Empty,string.Empty,vCantidad);
                    if(vResult == false) {
                        return false;
                    }
                }
            }
            return vResult;
        }

        bool HayExistenciaParaLaSalida(string valCodigoArticulo,int valTipoDeArticulo,int valTipoDeArticuloInv,string valSerial,string valRollo,decimal valCantidadArticulo) {
            bool vResult = false;
            IArticuloInventarioPdn insArticuloInventarioNav = new clsArticuloInventarioNav();
            int vPermitirSobregiro = (int)LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetEnum("FacturaRapida","PermitirSobregiro");

            StringBuilder vMensaje1 = new StringBuilder("El resultado de la operación actual ");
            vMensaje1.AppendLine(" generará una existencia negativa para el item: " + valCodigoArticulo);
            vMensaje1.AppendLine(valCodigoArticulo + ". ¿Desea continuar con el proceso? ");

            StringBuilder vMensaje2 = new StringBuilder("No hay existencia suficiente en el almacén: " + CodigoAlmacen + " del ítem: " + valCodigoArticulo);
            vMensaje2.AppendLine(". El sistema permite la posibilidad de sobregirar ");
            vMensaje2.AppendLine("el inventario, pero el usuario actual no posee nivel para la autorización ");
            vMensaje2.AppendLine("de dicha operación. Consulte con su Supervisor. El proceso de será cancelado.");

            StringBuilder vMensaje3 = new StringBuilder("No hay existencia suficiente en almacén: " + CodigoAlmacen + " del artículo: " + valCodigoArticulo);
            vMensaje3.AppendLine(". El proceso de facturación será cancelado.");

            if(Numero != string.Empty) {
                vMensaje3.AppendLine(" La factura de tipo Borrador que tuvo el problema es la número : " + Numero + ".");
            }
            if(valTipoDeArticulo == (int)eTipoDeArticulo.Servicio) {
                vResult = true;
            } else {
                decimal vDisponibilidad = insArticuloInventarioNav.DisponibilidadDeArticulo(ConsecutivoCompania,CodigoAlmacen,valCodigoArticulo,valTipoDeArticulo,valSerial,valRollo);
                if(valCantidadArticulo > vDisponibilidad) {
                    if(vPermitirSobregiro == (int)ePermitirSobregiro.NoChequearExistencia) {
                        vResult = true;
                    } else if(vPermitirSobregiro == (int)ePermitirSobregiro.PermitirSobregiro && LibSecurityManager.CurrentUserHasAccessTo("Artículo","Autorizar Sobregiro")) {
                        vResult = LibMessages.MessageBox.YesNo(this,vMensaje1.ToString(),"Advertencia");
                    } else if(vPermitirSobregiro == (int)ePermitirSobregiro.PermitirSobregiro && !LibSecurityManager.CurrentUserHasAccessTo("Artículo","Autorizar Sobregiro")) {
                        LibMessages.MessageBox.Alert(this,vMensaje2.ToString(),"Advertencia");
                        vResult = false;
                    } else if(vPermitirSobregiro == (int)ePermitirSobregiro.NoPermitirSobregiro) {
                        LibMessages.MessageBox.Alert(this,vMensaje3.ToString(),"Advertencia");
                        vResult = false;
                    }
                } else {
                    vResult = true;
                }
            }
            return vResult;
        }

        string BuscaInfoAlicuotaEspecial() {
            DateTime vFecha;
            try {
                string vResult = string.Empty;
                IAlicuotaImpuestoEspecialPdn insAlicuotaEspecial = new clsAlicuotaImpuestoEspecialNav();
                if(LibDate.Equals(Fecha,LibDate.Today())) {
                    vFecha = Fecha;
                } else {
                    vFecha = LibDate.Today();
                }
                XElement xAlicuotaEspecial = insAlicuotaEspecial.ObtenerAlicuotaEspecial(vFecha);
                decimal vTotalFacturaAValidar = 0;
                decimal vBaseImponible = LibConvert.ToDec(TotalBaseImponible);
                if(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetEnum("FacturaRapida","BaseDeCalculoParaAlicuotaEspecial ") == (int)eBaseCalculoParaAlicuotaEspecial.Monto_Exento_mas_Base_Imponible) {
                    vTotalFacturaAValidar = vBaseImponible + LibConvert.ToDec(TotalMontoExento);
                }
                if(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetEnum("FacturaRapida","BaseDeCalculoParaAlicuotaEspecial ") == (int)eBaseCalculoParaAlicuotaEspecial.Total_Factura) {
                    vTotalFacturaAValidar = vBaseImponible + LibConvert.ToDec(TotalMontoExento) + (vBaseImponible * (LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetDecimal("FacturaRapida","PorcentajeAlicuota1") / 100));
                }
                if(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetEnum("FacturaRapida","BaseDeCalculoParaAlicuotaEspecial ") == (int)eBaseCalculoParaAlicuotaEspecial.Solo_Base_Imponible) {
                    vTotalFacturaAValidar = vBaseImponible;
                }
                if(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetEnum("FacturaRapida","BaseDeCalculoParaAlicuotaEspecial ") == (int)eBaseCalculoParaAlicuotaEspecial.Solo_Base_Imponible_Alicuota_General) {
                    vTotalFacturaAValidar = LibConvert.ToDec(MontoGravableAlicuota1);
                }
                if(xAlicuotaEspecial != null && xAlicuotaEspecial.HasElements) {
                    var x = xAlicuotaEspecial.Descendants("GpResult")
                            .Where(c =>
                                        LibImportData.ToDec(c.Element("MontoMinimo").Value) <= LibConvert.ToDec(vTotalFacturaAValidar)
                                        && LibConvert.ToDec(vTotalFacturaAValidar) <= LibImportData.ToDec(c.Element("MontoMaximo").Value)
                                     //&& LibConvert.DbValueToEnum(c.Element("Estatus").Value) == (int)eEstatusAlicuotaImpuestoEspecial.Activo
                                     )
                            .Select(c => new {
                                PorcentajeAlicuota = LibImportData.ToDec(c.Element("Alicuota").Value),
                                SustituyeAlicuota = LibConvert.SNToBool(c.Element("SustituyeAlicuota").Value),
                                AlicuotaIvaASustituir = LibConvert.DbValueToEnum(c.Element("AlicuotaIVAASustituir").Value),
                                AplicarPorContribuyente = LibConvert.DbValueToEnum(c.Element("AplicarPorContribuyente").Value)
                            });

                    foreach(var item in x) {
                        _PorcentajeAlicuotaEspecial = item.PorcentajeAlicuota;
                        _SustituyeAlicuota = item.SustituyeAlicuota;
                        if(item.AlicuotaIvaASustituir == (int)eAlicuotaIVA.Reducida) {
                            _AlicuotaIvaASustituir = (int)eTipoDeAlicuota.Alicuota2;
                        } else if(item.AlicuotaIvaASustituir == (int)eAlicuotaIVA.Extendida) {
                            _AlicuotaIvaASustituir = (int)eTipoDeAlicuota.Alicuota3;
                        }
                        _AplicarPorContribuyente = item.AplicarPorContribuyente;
                        break;
                    }
                }
                return vResult;
            } catch(Exception ex) {
                throw ex;
            }
        }

        void MensajeImpresionFiscalDecreto3085() {
            StringBuilder vMensaje = new StringBuilder();
            bool vLimpiamensaje = false;
            eImpresoraFiscal vModeloImpresora;
            ICajaPdn insCaja = new clsCajaNav();
            if(_XmlDatosImprFiscal != null) {
                vModeloImpresora = LibEnumHelper<eImpresoraFiscal>.Parse(insCaja.BuscarModeloDeMaquinaFiscal(ConsecutivoCompania,ConsecutivoCaja));
                vMensaje.Clear();
                vMensaje.AppendLine("\n * SE APLICARÁ REBAJA DEL IVA * \n");
                vMensaje.AppendLine("* SEGÚN DECRETO 3.085/G.0.41.239*");
                vLimpiamensaje = !(vModeloImpresora == eImpresoraFiscal.BEMATECH_MP_20_FI_II || vModeloImpresora == eImpresoraFiscal.BEMATECH_MP_2100_FI);
                if(vLimpiamensaje) {
                    string vMsg = LibText.Replace(vMensaje.ToString(),"\n"," ");
                    vMensaje.Clear();
                    vMensaje.AppendLine(vMsg);
                }
            }
            if(AplicaDecretoIvaEspecial) {
                Observaciones = vMensaje.ToString();
            } else {
                Observaciones = String.Empty;
            }
        }

        public void ValidateImpresoraFiscal() {
            string vRefMensaje = "";
            ICajaPdn insCaja = new clsCajaNav();
            _XmlDatosImprFiscal = insCaja.ValidateImpresoraFiscal(ref vRefMensaje);
            if(!LibString.IsNullOrEmpty(vRefMensaje)) {
                LibMessages.MessageBox.Information(this,vRefMensaje,"");
            }
        }

        public bool AsignarTasaDeCambioDeMonedaDeCobroYParaMostrarTotales(bool valFacturaEnEspera = false) {
            bool vResult = false;
            bool vEsMonedaLocal = false;
            string vMoneda = string.Empty;
            FkMonedaViewModel vConexionMoneda = null;
            if(_clsNoComun.InstanceMonedaLocalActual.EsMonedaLocalDelPais(CodigoMonedaDeCobro)) {
                vEsMonedaLocal = true;
                vMoneda = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros","CodigoMonedaExtranjera");
            } else {
                vMoneda = CodigoMonedaDeCobro;
            }
            vConexionMoneda = FirstConnectionRecordOrDefault<FkMonedaViewModel>("Moneda",LibSearchCriteria.CreateCriteriaFromText("Codigo",vMoneda));
            bool vInsertarCambio = EsNecesarioInsertarCambio();
            if(vInsertarCambio) {
                decimal vTasa = 1;
                DateTime vFecha = LibDate.Today();
                if(((ICambioPdn)new clsCambioNav()).ExisteTasaDeCambioParaElDia(vMoneda,vFecha,out vTasa)) {
                    vResult = AsignarTasaDeCambio(valFacturaEnEspera,vEsMonedaLocal,vTasa,vFecha);
                } else {
                    vResult = SolicitarTasaDeCambio(vMoneda,vFecha,vConexionMoneda);
                    if(!vResult) {
                        AsignarTasaDeCambioDeMonedaDeCobroYParaMostrarTotales();
                    }
                }
            } else {
                CambioMostrarTotalEnDivisas = 1;
                vResult = true;
            }
            SimboloMonedaParaTotalEnDivisas = vConexionMoneda.Simbolo;
            RaisePropertyChanged(MostrarTasaDeCambioPropertyName);
            return vResult;
        }

        private bool SolicitarTasaDeCambio(string valMoneda,DateTime valFecha,FkMonedaViewModel valConexionMoneda) {
            bool vResult = false;
            CambioViewModel vViewModel = new CambioViewModel(valMoneda);
            vViewModel.InitializeViewModel(eAccionSR.Insertar);
            vViewModel.OnCambioAMonedaLocalChanged += CambioChanged;
            vViewModel.FechaDeVigencia = valFecha;
            vViewModel.IsEnabledFecha = false;
            vViewModel.CodigoMoneda = valConexionMoneda.Codigo;
            vViewModel.NombreMoneda = valConexionMoneda.Nombre;
            vResult = LibMessages.EditViewModel.ShowEditor(vViewModel,true);
            return vResult;
        }
        private bool AsignarTasaDeCambio(bool valFacturaEnEspera,bool valEsMonedaLocal,decimal valTasa,DateTime valFecha) {
            if (CambioMostrarTotalEnDivisas != valTasa) {
                if (valFacturaEnEspera) {
                    if(DeseaActualizarLaTasaDeCambioDeLaFacturaEnEspera(valEsMonedaLocal, valTasa, valFecha)) {
                        CambioMostrarTotalEnDivisas = valTasa;
                    }
                } else {
                    CambioMostrarTotalEnDivisas = valTasa;
                }
            } else if (CambioMostrarTotalEnDivisas == 1) {
                CambioMostrarTotalEnDivisas = valTasa;
            }
            if (EmpresaUsaMonedaExtranjeraComoPredeterminada()) {
                CambioABolivares = valTasa;
            }
            return true;
        }

        private void CambioChanged(decimal valCambio) {
            CambioMostrarTotalEnDivisas = valCambio;
        }

        private void SiguienteFactura() {
            EsFacturaEnEspera = false;
            RecargarValoresDeFactura(new FacturaRapida());
            InitializeViewModel(eAccionSR.Insertar);
            NotificarcambiosEnTodasLasPropiedades();
            AsignarTasaDeCambioDeMonedaDeCobroYParaMostrarTotales();
            MoverFocoACliente();
        }

        private void FacturaEnEspera() {
            FacturaRapida vFacturaEnEspera = new FacturaRapida();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania",ConsecutivoCompania);
            vParams.AddInString("Numero",Numero,11);
            vParams.AddInEnum("TipoDeDocumento",LibConvert.EnumToDbValue((int)eTipoDocumentoFactura.ComprobanteFiscal));
            vParams.AddInEnum("StatusFactura",LibConvert.EnumToDbValue((int)eStatusFactura.Borrador));
            vFacturaEnEspera = BusinessComponent.GetData(eProcessMessageType.SpName,"FacturaRapidaGET",vParams.Get(),UseDetail).FirstOrDefault();
            Model.fldTimeStamp = vFacturaEnEspera.fldTimeStamp;
        }

        decimal FactorBaseAlicuotaIva(eTipoDeAlicuota valTipoDeAlicuota) {
            decimal vResult = 0;
            switch(valTipoDeAlicuota) {
            case eTipoDeAlicuota.Exento:
                vResult = 1m;
                break;
            case eTipoDeAlicuota.AlicuotaGeneral:
                vResult = 1 + (PorcentajeAlicuota1 / 100m);
                break;
            case eTipoDeAlicuota.Alicuota2:
                vResult = 1 + (PorcentajeAlicuota2 / 100m);
                break;
            case eTipoDeAlicuota.Alicuota3:
                vResult = 1 + (PorcentajeAlicuota3 / 100m);
                break;
            default:
                vResult = 1 + (PorcentajeAlicuota1 / 100m);
                break;
            }
            return vResult;
        }
        private void ObtenerPrecioConYSinIvaSegunNivelDePrecio(int valNivelDePrecio,ref decimal valMePrecioSinIva,ref decimal valMePrecioConIva) {
            switch(valNivelDePrecio) {
            case 1:
                valMePrecioSinIva = ConexionArticulo.MePrecioConIva / FactorBaseAlicuotaIva((eTipoDeAlicuota)ConexionArticulo.AlicuotaIVA);
                valMePrecioConIva = ConexionArticulo.MePrecioConIva;
                break;
            case 2:
                valMePrecioSinIva = ConexionArticulo.MePrecioConIva2 / FactorBaseAlicuotaIva((eTipoDeAlicuota)ConexionArticulo.AlicuotaIVA);
                valMePrecioConIva = ConexionArticulo.MePrecioConIva2;
                break;
            case 3:
                valMePrecioSinIva = ConexionArticulo.MePrecioConIva3 / FactorBaseAlicuotaIva((eTipoDeAlicuota)ConexionArticulo.AlicuotaIVA);
                valMePrecioConIva = ConexionArticulo.MePrecioConIva3;
                break;
            case 4:
                valMePrecioSinIva = ConexionArticulo.MePrecioConIva4 / FactorBaseAlicuotaIva((eTipoDeAlicuota)ConexionArticulo.AlicuotaIVA);
                valMePrecioConIva = ConexionArticulo.MePrecioConIva4;
                break;
            default:
                valMePrecioSinIva = ConexionArticulo.MePrecioConIva / FactorBaseAlicuotaIva((eTipoDeAlicuota)ConexionArticulo.AlicuotaIVA);
                valMePrecioConIva = ConexionArticulo.MePrecioConIva;
                break;
            }
        }

        public void EnfocarArticulo() {
            if(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("FacturaRapida","UsaBusquedaDinamicaEnPuntoDeVenta")) {
                LibMessages.Notification.Send<string>(CuadroDeBusquedaDeArticulosViewModelPropertyName,"Focus");
            } else {
                RaiseMoveFocus(ArticuloPropertyName);
            }
        }

        public void EnfocarCliente() {
            LibMessages.Notification.Send<string>(CuadroDeBusquedaDeClientesViewModelPropertyName,"Focus");
        }
        private decimal CalcularDescuentoAlActualizarFactura(decimal vMonto,decimal vDescuento) {
            return LibMath.RoundToNDecimals(vMonto - (vMonto * vDescuento),2);
        }
        private void CalcularDescuentoCuandoPrecioVieneConIVA(decimal vMontoExento,decimal vMontoGravable1,decimal vMontoGravable2,decimal vMontoGravable3,decimal vDescuento) {

            TotalMontoExento = CalcularDescuentoAlActualizarFactura(vMontoExento,vDescuento);

            MontoGravableAlicuota1 = CalcularDescuentoAlActualizarFactura(vMontoGravable1,vDescuento);
            MontoGravableAlicuota2 = CalcularDescuentoAlActualizarFactura(vMontoGravable2,vDescuento);
            MontoGravableAlicuota3 = CalcularDescuentoAlActualizarFactura(vMontoGravable3,vDescuento);

            MontoIvaAlicuota1 = LibMath.RoundToNDecimals(MontoGravableAlicuota1 * (PorcentajeAlicuota1 / 100),2);
            MontoIvaAlicuota2 = LibMath.RoundToNDecimals(MontoGravableAlicuota2 * (PorcentajeAlicuota2 / 100),2);
            MontoIvaAlicuota3 = LibMath.RoundToNDecimals(MontoGravableAlicuota3 * (PorcentajeAlicuota3 / 100),2);

        }
        private void ActualizarValoresCuadrosDeBusqueda(ISearchBoxViewModel cuadroDeBusqueda,string valor) {
            cuadroDeBusqueda.Filter = valor;
            cuadroDeBusqueda.HideListCommand.Execute(null);
        }

        private bool EsNecesarioInsertarCambio() {
            bool vResult = false;
            bool vMuestraTotalEnDivisas = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "SeMuestraTotalEnDivisas");
            bool vUsaListaDePrecioEnMonedaExtranjera = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "UsaListaDePrecioEnMonedaExtranjera");
            bool vEsFacturaEnMonedaLocal = _clsNoComun.InstanceMonedaLocalActual.EsMonedaLocalDelPais(CodigoMonedaDeCobro);
            bool vEmpresaUsaMonedaExtranjeraPredeterminada = EmpresaUsaMonedaExtranjeraComoPredeterminada();
            vResult = !vEsFacturaEnMonedaLocal || vMuestraTotalEnDivisas || vUsaListaDePrecioEnMonedaExtranjera || vEmpresaUsaMonedaExtranjeraPredeterminada;            return vResult;
        }

        private bool DeseaActualizarLaTasaDeCambioDeLaFacturaEnEspera(bool valEsMonedaLocal, decimal valTasa, DateTime valFecha) {
            StringBuilder vMensaje = new StringBuilder(); 
            if (!valEsMonedaLocal) {
                vMensaje.Append("La factura que está cargando se guardó con Moneda de Cobro " + NombreMonedaDeCobro + ",");
                vMensaje.AppendLine(" con tasa de cambio " + LibConvert.ToStr(CambioMostrarTotalEnDivisas) + " el día " + LibConvert.ToStr(Fecha)).AppendLine();
                vMensaje.AppendLine("La tasa de cambio actual para " + NombreMonedaDeCobro + " es " + LibConvert.ToStr(valTasa) + " del día " + LibConvert.ToStr(valFecha)).AppendLine();
            } else {
                vMensaje.Append("La factura que está cargando se guardó con tasa de cambio " + LibConvert.ToStr(CambioMostrarTotalEnDivisas) + " el día " + LibConvert.ToStr(Fecha));
                vMensaje.AppendLine(" y la tasa de cambio actual de la moneda extranjera es " + LibConvert.ToStr(valTasa) + " del día " + LibConvert.ToStr(valFecha)).AppendLine();
            }
            vMensaje.AppendLine("¿Desea actualizar la tasa de cambio?");
            return LibMessages.MessageBox.YesNo(this, vMensaje.ToString(), "Tasa de Cambio");
        }
        
        private bool EmpresaUsaMonedaExtranjeraComoPredeterminada(ref FkMonedaViewModel refMonedaExtranjera) {
            bool vResult = false;
            bool vUsaMonedaExtranjera = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "UsaMonedaExtranjera");
            bool vUsaMonedaExtranjeraComoMonedaPredeterminada = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "UsaDivisaComoMonedaPrincipalDeIngresoDeDatos");
            string vCodigoMonedaExtranjera = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "CodigoMonedaExtranjera");
            if(vUsaMonedaExtranjera && vUsaMonedaExtranjeraComoMonedaPredeterminada && !LibString.IsNullOrEmpty(vCodigoMonedaExtranjera)) {
                FkMonedaViewModel vConexionMoneda = FirstConnectionRecordOrDefault<FkMonedaViewModel>("Moneda", LibSearchCriteria.CreateCriteriaFromText("Codigo", vCodigoMonedaExtranjera));
                refMonedaExtranjera = vConexionMoneda;
                vResult = true;
            }
            return vResult;
        }

        private bool EmpresaUsaMonedaExtranjeraComoPredeterminada() {
            bool vResult = false;
            bool vUsaMonedaExtranjera = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "UsaMonedaExtranjera");
            bool vUsaMonedaExtranjeraComoMonedaPredeterminada = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "UsaDivisaComoMonedaPrincipalDeIngresoDeDatos");
            string vCodigoMonedaExtranjera = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "CodigoMonedaExtranjera");
            if (vUsaMonedaExtranjera && vUsaMonedaExtranjeraComoMonedaPredeterminada && !LibString.IsNullOrEmpty(vCodigoMonedaExtranjera)) {
                vResult = true;
            }
            return vResult;
        }

        private void AsignarMonedaDeCobroFacturaSegunCorresponda() {
            FkMonedaViewModel insMonedaExtranjera = new FkMonedaViewModel();
            if (EmpresaUsaMonedaExtranjeraComoPredeterminada(ref insMonedaExtranjera)) {
                CodigoMonedaDeCobro = insMonedaExtranjera.Codigo;
                NombreMonedaDeCobro = insMonedaExtranjera.Nombre;
            } else {
               CodigoMonedaDeCobro = _clsNoComun.InstanceMonedaLocalActual.CodigoMoneda(LibDate.Today());
               NombreMonedaDeCobro = _clsNoComun.InstanceMonedaLocalActual.NombreMoneda(LibDate.Today());
            }
        }

        private void AsignarMonedaDeFacturaSegunCorresponda() {
            FkMonedaViewModel insMonedaExtranjera = new FkMonedaViewModel();
            if (EmpresaUsaMonedaExtranjeraComoPredeterminada(ref insMonedaExtranjera)) {
                CodigoMoneda = insMonedaExtranjera.Codigo;
                SimboloMonedaDeFactura = insMonedaExtranjera.Simbolo;
                Moneda = insMonedaExtranjera.Nombre;
            } else {
                CodigoMoneda = _clsNoComun.InstanceMonedaLocalActual.CodigoMoneda(LibDate.Today());
                SimboloMonedaDeFactura = _clsNoComun.InstanceMonedaLocalActual.SimboloMoneda(LibDate.Today());
                Moneda = _clsNoComun.InstanceMonedaLocalActual.NombreMoneda(LibDate.Today());
            }
        }
    } //End of class FacturacionRapidaViewModel
} //End of namespace Galac.Adm.Uil.Venta
