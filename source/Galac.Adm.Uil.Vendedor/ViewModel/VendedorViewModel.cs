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
using Galac.Adm.Brl.Vendedor;
using Galac.Adm.Ccl.Vendedor;
using LibGalac.Aos.Uil;
using Galac.Comun.Uil.TablasGen.ViewModel;
using Galac.Adm.Uil.Vendedor.Properties;
using Galac.Saw.Ccl.SttDef;

namespace Galac.Adm.Uil.Vendedor.ViewModel { 
    public class VendedorViewModel : LibInputMasterViewModelMfc<Galac.Adm.Ccl.Vendedor.Vendedor> {
        #region Variables
        private FkCiudadViewModel _ConexionCiudad = null;
        #endregion
        #region Constantes
        public const string ConsecutivoCompaniaPropertyName = "ConsecutivoCompania";
        public const string ConsecutivoPropertyName = "Consecutivo";
        public const string CodigoPropertyName = "Codigo";
        public const string NombrePropertyName = "Nombre";
        public const string RIFPropertyName = "RIF";
        public const string StatusVendedorPropertyName = "StatusVendedor";
        public const string DireccionPropertyName = "Direccion";
        public const string CiudadPropertyName = "Ciudad";
        public const string ZonaPostalPropertyName = "ZonaPostal";
        public const string TelefonoPropertyName = "Telefono";
        public const string FaxPropertyName = "Fax";
        public const string EmailPropertyName = "Email";
        public const string NotasPropertyName = "Notas";
        public const string ComisionPorVentaPropertyName = "ComisionPorVenta";
        public const string ComisionPorCobroPropertyName = "ComisionPorCobro";
        public const string TopeInicialVenta1PropertyName = "TopeInicialVenta1";
        public const string TopeFinalVenta1PropertyName = "TopeFinalVenta1";
        public const string PorcentajeVentas1PropertyName = "PorcentajeVentas1";
        public const string TopeFinalVenta2PropertyName = "TopeFinalVenta2";
        public const string PorcentajeVentas2PropertyName = "PorcentajeVentas2";
        public const string TopeFinalVenta3PropertyName = "TopeFinalVenta3";
        public const string PorcentajeVentas3PropertyName = "PorcentajeVentas3";
        public const string TopeFinalVenta4PropertyName = "TopeFinalVenta4";
        public const string PorcentajeVentas4PropertyName = "PorcentajeVentas4";
        public const string TopeFinalVenta5PropertyName = "TopeFinalVenta5";
        public const string PorcentajeVentas5PropertyName = "PorcentajeVentas5";
        public const string TopeInicialCobranza1PropertyName = "TopeInicialCobranza1";
        public const string TopeFinalCobranza1PropertyName = "TopeFinalCobranza1";
        public const string PorcentajeCobranza1PropertyName = "PorcentajeCobranza1";
        public const string TopeFinalCobranza2PropertyName = "TopeFinalCobranza2";
        public const string PorcentajeCobranza2PropertyName = "PorcentajeCobranza2";
        public const string TopeFinalCobranza3PropertyName = "TopeFinalCobranza3";
        public const string PorcentajeCobranza3PropertyName = "PorcentajeCobranza3";
        public const string TopeFinalCobranza4PropertyName = "TopeFinalCobranza4";
        public const string PorcentajeCobranza4PropertyName = "PorcentajeCobranza4";
        public const string TopeFinalCobranza5PropertyName = "TopeFinalCobranza5";
        public const string PorcentajeCobranza5PropertyName = "PorcentajeCobranza5";
        public const string UsaComisionPorVentaPropertyName = "UsaComisionPorVenta";
        public const string UsaComisionPorCobranzaPropertyName = "UsaComisionPorCobranza";
        public const string CodigoLotePropertyName = "CodigoLote";
        public const string TipoDocumentoIdentificacionPropertyName = "TipoDocumentoIdentificacion";
        public const string RutaDeComercializacionPropertyName = "RutaDeComercializacion";
        public const string NombreOperadorPropertyName = "NombreOperador";
        public const string FechaUltimaModificacionPropertyName = "FechaUltimaModificacion";
        public const string IsEnabledDetalleComisionesPorVentasPropertyName = "IsEnabledDetalleComisionesPorVentas";
        public const string IsEnabledDetalleComisionesPorCobranzasPropertyName = "IsEnabledDetalleComisionesPorCobranzas";
        #endregion
        #region Propiedades

        public override string ModuleName {
            get { return "Vendedor"; }
        }

        public int  ConsecutivoCompania {
            get {
                return Model.ConsecutivoCompania;
            }
            set {
                if (Model.ConsecutivoCompania != value) {
                    Model.ConsecutivoCompania = value;
                    IsDirty = true;
                    RaisePropertyChanged(ConsecutivoCompaniaPropertyName);
                }
            }
        }

        public int  Consecutivo {
            get {
                return Model.Consecutivo;
            }
            set {
                if (Model.Consecutivo != value) {
                    Model.Consecutivo = value;
                    IsDirty = true;
                    RaisePropertyChanged(ConsecutivoPropertyName);
                }
            }
        }
        [LibRequired(ErrorMessage = "Código del Vendedor es requerido.")]
        [LibGridColum("Código", ColumnOrder = 0)]
        public string  Codigo {
            get {
                return Model.Codigo;
            }
            set {
                if (Model.Codigo != value) {
                    Model.Codigo = value;
                    IsDirty = true;
                    RaisePropertyChanged(CodigoPropertyName);
                }
            }
        }

        [LibRequired(ErrorMessage = "Nombre del Vendedor es requerido.")]
        [LibGridColum("Nombre", ColumnOrder = 1)]
        public string  Nombre {
            get {
                return Model.Nombre;
            }
            set {
                if (Model.Nombre != value) {
                    Model.Nombre = value;
                    IsDirty = true;
                    RaisePropertyChanged(NombrePropertyName);
                }
            }
        }

        //[LibRequired(ErrorMessage = "RIF/RUC del Vendedor es requerido.")]
        [LibGridColum(HeaderResourceName = "lblEtiquetaNumero", HeaderResourceType = typeof(Resources), ColumnOrder = 2)]
        public string  RIF {
            get {
                return Model.RIF;
            }
            set {
                if (Model.RIF != value) {
                    Model.RIF = value;
                    IsDirty = true;
                    RaisePropertyChanged(RIFPropertyName);
                }
            }
        }

        public string PromptNumeroRif {
            get {
                string vResult = "";
                if (EsVenezuela()) {
                    vResult = "N° R.I.F.";
                } else if (EsEcuador()) {
                    vResult = "RUC";
                }
                return vResult;
            }
        }

        [LibGridColum("Estado", eGridColumType.Enum, PrintingMemberPath = "StatusVendedorStr", ColumnOrder = 4, Width = 70)]
        public eStatusVendedor  StatusVendedor {
            get {
                return Model.StatusVendedorAsEnum;
            }
            set {
                if (Model.StatusVendedorAsEnum != value) {
                    Model.StatusVendedorAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(StatusVendedorPropertyName);
                }
            }
        }

        public string  Direccion {
            get {
                return Model.Direccion;
            }
            set {
                if (Model.Direccion != value) {
                    Model.Direccion = value;
                    IsDirty = true;
                    RaisePropertyChanged(DireccionPropertyName);
                }
            }
        }

        [LibRequired(ErrorMessage = "El campo Ciudad es requerido.")]
        [LibGridColum("Ciudad", ColumnOrder = 3)]
        public string  Ciudad {
            get {
                return Model.Ciudad;
            }
            set {
                if (Model.Ciudad != value) {
                    Model.Ciudad = value;
                    IsDirty = true;
                    RaisePropertyChanged(CiudadPropertyName);
                }
            }
        }

        public string  ZonaPostal {
            get {
                return Model.ZonaPostal;
            }
            set {
                if (Model.ZonaPostal != value) {
                    Model.ZonaPostal = value;
                    IsDirty = true;
                    RaisePropertyChanged(ZonaPostalPropertyName);
                }
            }
        }

        public string  Telefono {
            get {
                return Model.Telefono;
            }
            set {
                if (Model.Telefono != value) {
                    Model.Telefono = value;
                    IsDirty = true;
                    RaisePropertyChanged(TelefonoPropertyName);
                }
            }
        }

        public string  Fax {
            get {
                return Model.Fax;
            }
            set {
                if (Model.Fax != value) {
                    Model.Fax = value;
                    IsDirty = true;
                    RaisePropertyChanged(FaxPropertyName);
                }
            }
        }

        public string  Email {
            get {
                return Model.Email;
            }
            set {
                if (Model.Email != value) {
                    Model.Email = value;
                    IsDirty = true;
                    RaisePropertyChanged(EmailPropertyName);
                }
            }
        }

        public string  Notas {
            get {
                return Model.Notas;
            }
            set {
                if (Model.Notas != value) {
                    Model.Notas = value;
                    IsDirty = true;
                    RaisePropertyChanged(NotasPropertyName);
                }
            }
        }

        public decimal  ComisionPorVenta {
            get {
                return Model.ComisionPorVenta;
            }
            set {
                if (Model.ComisionPorVenta != value) {
                    Model.ComisionPorVenta = value;
                    IsDirty = true;
                    RaisePropertyChanged(ComisionPorVentaPropertyName);
                }
            }
        }

        public decimal  ComisionPorCobro {
            get {
                return Model.ComisionPorCobro;
            }
            set {
                if (Model.ComisionPorCobro != value) {
                    Model.ComisionPorCobro = value;
                    IsDirty = true;
                    RaisePropertyChanged(ComisionPorCobroPropertyName);
                }
            }
        }

        public decimal  TopeInicialVenta1 {
            get {
                return LibMath.RoundToNDecimals(Model.TopeInicialVenta1, 2);
            }
            set {
                if (Model.TopeInicialVenta1 != value) {
                    Model.TopeInicialVenta1 = value;
                    IsDirty = true;
                    RaisePropertyChanged(TopeInicialVenta1PropertyName);
                }
            }
        }

        public decimal  TopeFinalVenta1 {
            get {
                return Model.TopeFinalVenta1;
            }
            set {
                if (Model.TopeFinalVenta1 != value) {
                    Model.TopeFinalVenta1 = value;
                    IsDirty = true;
                    RaisePropertyChanged(TopeFinalVenta1PropertyName);
                }
            }
        }

        public decimal  PorcentajeVentas1 {
            get {
                return Model.PorcentajeVentas1;
            }
            set {
                if (Model.PorcentajeVentas1 != value) {
                    Model.PorcentajeVentas1 = value;
                    IsDirty = true;
                    RaisePropertyChanged(PorcentajeVentas1PropertyName);
                }
            }
        }

        public decimal  TopeFinalVenta2 {
            get {
                return Model.TopeFinalVenta2;
            }
            set {
                if (Model.TopeFinalVenta2 != value) {
                    Model.TopeFinalVenta2 = value;
                    IsDirty = true;
                    RaisePropertyChanged(TopeFinalVenta2PropertyName);
                }
            }
        }

        public decimal  PorcentajeVentas2 {
            get {
                return Model.PorcentajeVentas2;
            }
            set {
                if (Model.PorcentajeVentas2 != value) {
                    Model.PorcentajeVentas2 = value;
                    IsDirty = true;
                    RaisePropertyChanged(PorcentajeVentas2PropertyName);
                }
            }
        }

        public decimal  TopeFinalVenta3 {
            get {
                return Model.TopeFinalVenta3;
            }
            set {
                if (Model.TopeFinalVenta3 != value) {
                    Model.TopeFinalVenta3 = value;
                    IsDirty = true;
                    RaisePropertyChanged(TopeFinalVenta3PropertyName);
                }
            }
        }

        public decimal  PorcentajeVentas3 {
            get {
                return Model.PorcentajeVentas3;
            }
            set {
                if (Model.PorcentajeVentas3 != value) {
                    Model.PorcentajeVentas3 = value;
                    IsDirty = true;
                    RaisePropertyChanged(PorcentajeVentas3PropertyName);
                }
            }
        }

        public decimal  TopeFinalVenta4 {
            get {
                return Model.TopeFinalVenta4;
            }
            set {
                if (Model.TopeFinalVenta4 != value) {
                    Model.TopeFinalVenta4 = value;
                    IsDirty = true;
                    RaisePropertyChanged(TopeFinalVenta4PropertyName);
                }
            }
        }

        public decimal  PorcentajeVentas4 {
            get {
                return Model.PorcentajeVentas4;
            }
            set {
                if (Model.PorcentajeVentas4 != value) {
                    Model.PorcentajeVentas4 = value;
                    IsDirty = true;
                    RaisePropertyChanged(PorcentajeVentas4PropertyName);
                }
            }
        }

        public decimal  TopeFinalVenta5 {
            get {
                return Model.TopeFinalVenta5;
            }
            set {
                if (Model.TopeFinalVenta5 != value) {
                    Model.TopeFinalVenta5 = value;
                    IsDirty = true;
                    RaisePropertyChanged(TopeFinalVenta5PropertyName);
                }
            }
        }

        public decimal  PorcentajeVentas5 {
            get {
                return Model.PorcentajeVentas5;
            }
            set {
                if (Model.PorcentajeVentas5 != value) {
                    Model.PorcentajeVentas5 = value;
                    IsDirty = true;
                    RaisePropertyChanged(PorcentajeVentas5PropertyName);
                }
            }
        }

        public decimal  TopeInicialCobranza1 {
            get {
                return Model.TopeInicialCobranza1;
            }
            set {
                if (Model.TopeInicialCobranza1 != value) {
                    Model.TopeInicialCobranza1 = value;
                    IsDirty = true;
                    RaisePropertyChanged(TopeInicialCobranza1PropertyName);
                }
            }
        }

        public decimal  TopeFinalCobranza1 {
            get {
                return Model.TopeFinalCobranza1;
            }
            set {
                if (Model.TopeFinalCobranza1 != value) {
                    Model.TopeFinalCobranza1 = value;
                    IsDirty = true;
                    RaisePropertyChanged(TopeFinalCobranza1PropertyName);
                }
            }
        }

        public decimal  PorcentajeCobranza1 {
            get {
                return Model.PorcentajeCobranza1;
            }
            set {
                if (Model.PorcentajeCobranza1 != value) {
                    Model.PorcentajeCobranza1 = value;
                    IsDirty = true;
                    RaisePropertyChanged(PorcentajeCobranza1PropertyName);
                }
            }
        }

        public decimal  TopeFinalCobranza2 {
            get {
                return Model.TopeFinalCobranza2;
            }
            set {
                if (Model.TopeFinalCobranza2 != value) {
                    Model.TopeFinalCobranza2 = value;
                    IsDirty = true;
                    RaisePropertyChanged(TopeFinalCobranza2PropertyName);
                }
            }
        }

        public decimal  PorcentajeCobranza2 {
            get {
                return Model.PorcentajeCobranza2;
            }
            set {
                if (Model.PorcentajeCobranza2 != value) {
                    Model.PorcentajeCobranza2 = value;
                    IsDirty = true;
                    RaisePropertyChanged(PorcentajeCobranza2PropertyName);
                }
            }
        }

        public decimal  TopeFinalCobranza3 {
            get {
                return Model.TopeFinalCobranza3;
            }
            set {
                if (Model.TopeFinalCobranza3 != value) {
                    Model.TopeFinalCobranza3 = value;
                    IsDirty = true;
                    RaisePropertyChanged(TopeFinalCobranza3PropertyName);
                }
            }
        }

        public decimal  PorcentajeCobranza3 {
            get {
                return Model.PorcentajeCobranza3;
            }
            set {
                if (Model.PorcentajeCobranza3 != value) {
                    Model.PorcentajeCobranza3 = value;
                    IsDirty = true;
                    RaisePropertyChanged(PorcentajeCobranza3PropertyName);
                }
            }
        }

        public decimal  TopeFinalCobranza4 {
            get {
                return Model.TopeFinalCobranza4;
            }
            set {
                if (Model.TopeFinalCobranza4 != value) {
                    Model.TopeFinalCobranza4 = value;
                    IsDirty = true;
                    RaisePropertyChanged(TopeFinalCobranza4PropertyName);
                }
            }
        }

        public decimal  PorcentajeCobranza4 {
            get {
                return Model.PorcentajeCobranza4;
            }
            set {
                if (Model.PorcentajeCobranza4 != value) {
                    Model.PorcentajeCobranza4 = value;
                    IsDirty = true;
                    RaisePropertyChanged(PorcentajeCobranza4PropertyName);
                }
            }
        }

        public decimal  TopeFinalCobranza5 {
            get {
                return Model.TopeFinalCobranza5;
            }
            set {
                if (Model.TopeFinalCobranza5 != value) {
                    Model.TopeFinalCobranza5 = value;
                    IsDirty = true;
                    RaisePropertyChanged(TopeFinalCobranza5PropertyName);
                }
            }
        }

        public decimal  PorcentajeCobranza5 {
            get {
                return Model.PorcentajeCobranza5;
            }
            set {
                if (Model.PorcentajeCobranza5 != value) {
                    Model.PorcentajeCobranza5 = value;
                    IsDirty = true;
                    RaisePropertyChanged(PorcentajeCobranza5PropertyName);
                }
            }
        }

        public bool  UsaComisionPorVenta {
            get {
                return Model.UsaComisionPorVentaAsBool;
            }
            set {
                if (Model.UsaComisionPorVentaAsBool != value) {
                    Model.UsaComisionPorVentaAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(UsaComisionPorVentaPropertyName);
                    RaisePropertyChanged(IsEnabledDetalleComisionesPorVentasPropertyName);
                }
            }
        }

        public bool  UsaComisionPorCobranza {
            get {
                return Model.UsaComisionPorCobranzaAsBool;
            }
            set {
                if (Model.UsaComisionPorCobranzaAsBool != value) {
                    Model.UsaComisionPorCobranzaAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(UsaComisionPorCobranzaPropertyName);
                    RaisePropertyChanged(IsEnabledDetalleComisionesPorCobranzasPropertyName);
                }
            }
        }

        public string  CodigoLote {
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

        public eTipoDocumentoIdentificacion  TipoDocumentoIdentificacion {
            get {
                return Model.TipoDocumentoIdentificacionAsEnum;
            }
            set {
                if (Model.TipoDocumentoIdentificacionAsEnum != value) {
                    Model.TipoDocumentoIdentificacionAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(TipoDocumentoIdentificacionPropertyName);
                }
            }
        }

        public eRutaDeComercializacion  RutaDeComercializacion {
            get {
                return Model.RutaDeComercializacionAsEnum;
            }
            set {
                if (Model.RutaDeComercializacionAsEnum != value) {
                    Model.RutaDeComercializacionAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(RutaDeComercializacionPropertyName);
                }
            }
        }

        public string  NombreOperador {
            get {
                return Model.NombreOperador;
            }
            set {
                if (Model.NombreOperador != value) {
                    Model.NombreOperador = value;
                    IsDirty = true;
                    RaisePropertyChanged(NombreOperadorPropertyName);
                }
            }
        }

        public bool IsEnabledDetalleComisionesPorVentas {
            get {
                if (Action == eAccionSR.Insertar) {
                    return UsaComisionPorVenta;
                } else {
                    return IsEnabled && UsaComisionPorVenta;
                }
            }
        }

        public bool IsEnabledDetalleComisionesPorCobranzas {
            get {
                if (Action == eAccionSR.Insertar) {
                    return UsaComisionPorCobranza;
                } else {
                    return IsEnabled && UsaComisionPorCobranza;
                }
            }
        }

        public bool IsEnabledAsignacionDeComisiones {
            get {
                eComisionesEnFactura vFormaDeAsignarComisionesDeVendedor = (eComisionesEnFactura)LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("Parametros", "ComisionesEnFactura");
                return vFormaDeAsignarComisionesDeVendedor != eComisionesEnFactura.SobreRenglones;
            }
        }

        public bool IsEnabledComisionesPorLineaDeProducto {
            get {
                eComisionesEnFactura vFormaDeAsignarComisionesDeVendedor = (eComisionesEnFactura)LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("Parametros", "ComisionesEnFactura");
                return vFormaDeAsignarComisionesDeVendedor != eComisionesEnFactura.SobreTotalFactura;
            }
        }

        public DateTime  FechaUltimaModificacion {
            get {
                return Model.FechaUltimaModificacion;
            }
            set {
                if (Model.FechaUltimaModificacion != value) {
                    Model.FechaUltimaModificacion = value;
                    IsDirty = true;
                    RaisePropertyChanged(FechaUltimaModificacionPropertyName);
                }
            }
        }

        public eStatusVendedor[] ArrayStatusVendedor {
            get {
                return LibEnumHelper<eStatusVendedor>.GetValuesInArray();
            }
        }

        public eTipoDocumentoIdentificacion[] ArrayTipoDocumentoIdentificacion {
            get {
                return LibEnumHelper<eTipoDocumentoIdentificacion>.GetValuesInArray();
            }
        }

        public eRutaDeComercializacion[] ArrayRutaDeComercializacion {
            get {
                return LibEnumHelper<eRutaDeComercializacion>.GetValuesInArray();
            }
        }
        public FkCiudadViewModel ConexionCiudad {
            get {
                return _ConexionCiudad;
            }
            set {
                if (_ConexionCiudad != value) {
                    _ConexionCiudad = value;
                    if (_ConexionCiudad != null) {
                        Ciudad = _ConexionCiudad.NombreCiudad;
                    }
                }
                if (_ConexionCiudad == null) {
                    Ciudad = string.Empty;
                }
            }
        }
        //[LibRequired(ErrorMessage = "Comisiones de Vendedor por Linea de Producto es requerido.")]
        public VendedorDetalleComisionesMngViewModel DetailVendedorDetalleComisiones {
            get;
            set;
        }

        public RelayCommand<string> CreateVendedorDetalleComisionesCommand {
            get { return DetailVendedorDetalleComisiones.CreateCommand; }
        }

        public RelayCommand<string> UpdateVendedorDetalleComisionesCommand {
            get { return DetailVendedorDetalleComisiones.UpdateCommand; }
        }

        public RelayCommand<string> DeleteVendedorDetalleComisionesCommand {
            get { return DetailVendedorDetalleComisiones.DeleteCommand; }
        }
        public RelayCommand<string> ChooseCiudadCommand {
            get;
            private set;
        }
        #endregion //Propiedades
        #region Constructores
        public VendedorViewModel()
            : this(new Galac.Adm.Ccl.Vendedor.Vendedor(), eAccionSR.Insertar) {
        }
        public VendedorViewModel(Galac.Adm.Ccl.Vendedor.Vendedor initModel, eAccionSR initAction)
            : base(initModel, initAction, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            DefaultFocusedPropertyName = ConsecutivoCompaniaPropertyName;
            Model.ConsecutivoCompania = Mfc.GetInt("Compania");
            InitializeDetails();
        }
        #endregion //Constructores
        #region Metodos Generados
        protected override void InitializeCommands() {
            base.InitializeCommands();
            ChooseCiudadCommand = new RelayCommand<string>(ExecuteChooseCiudadCommand);
        }
        protected override void InitializeLookAndFeel(Galac.Adm.Ccl.Vendedor.Vendedor valModel) {
            base.InitializeLookAndFeel(valModel);
        }

        protected override Galac.Adm.Ccl.Vendedor.Vendedor FindCurrentRecord(Galac.Adm.Ccl.Vendedor.Vendedor valModel) {
            if (valModel == null) {
                return null;
            }
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valModel.ConsecutivoCompania);
            vParams.AddInInteger("Consecutivo", valModel.Consecutivo);
            return BusinessComponent.GetData(eProcessMessageType.SpName, "VendedorGET", vParams.Get(), UseDetail).FirstOrDefault();
        }

        protected override ILibBusinessMasterComponentWithSearch<IList<Galac.Adm.Ccl.Vendedor.Vendedor>, IList<Galac.Adm.Ccl.Vendedor.Vendedor>> GetBusinessComponent() {
            return new clsVendedorNav();
        }

        protected override void InitializeDetails() {
            DetailVendedorDetalleComisiones = new VendedorDetalleComisionesMngViewModel(this, Model.DetailVendedorDetalleComisiones, Action);
            DetailVendedorDetalleComisiones.OnCreated += new EventHandler<SearchCollectionChangedEventArgs<VendedorDetalleComisionesViewModel>>(DetailVendedorDetalleComisiones_OnCreated);
            DetailVendedorDetalleComisiones.OnUpdated += new EventHandler<SearchCollectionChangedEventArgs<VendedorDetalleComisionesViewModel>>(DetailVendedorDetalleComisiones_OnUpdated);
            DetailVendedorDetalleComisiones.OnDeleted += new EventHandler<SearchCollectionChangedEventArgs<VendedorDetalleComisionesViewModel>>(DetailVendedorDetalleComisiones_OnDeleted);
            DetailVendedorDetalleComisiones.OnSelectedItemChanged += new EventHandler<SearchCollectionChangedEventArgs<VendedorDetalleComisionesViewModel>>(DetailVendedorDetalleComisiones_OnSelectedItemChanged);
        }
        #region RenglonComisionesDeVendedor

        private void DetailVendedorDetalleComisiones_OnSelectedItemChanged(object sender, SearchCollectionChangedEventArgs<VendedorDetalleComisionesViewModel> e) {
            try {
                UpdateVendedorDetalleComisionesCommand.RaiseCanExecuteChanged();
                DeleteVendedorDetalleComisionesCommand.RaiseCanExecuteChanged();
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void DetailVendedorDetalleComisiones_OnDeleted(object sender, SearchCollectionChangedEventArgs<VendedorDetalleComisionesViewModel> e) {
            try {
                IsDirty = true;
                Model.DetailVendedorDetalleComisiones.Remove(e.ViewModel.GetModel());
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void DetailVendedorDetalleComisiones_OnUpdated(object sender, SearchCollectionChangedEventArgs<VendedorDetalleComisionesViewModel> e) {
            try {
                IsDirty = e.ViewModel.IsDirty;
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void DetailVendedorDetalleComisiones_OnCreated(object sender, SearchCollectionChangedEventArgs<VendedorDetalleComisionesViewModel> e) {
            try {
                Model.DetailVendedorDetalleComisiones.Add(e.ViewModel.GetModel());
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        #endregion //VendedorDetalleComisiones
        protected override void ReloadRelatedConnections() {
            base.ReloadRelatedConnections();
            //ConexionCiudad = LibFKRetrievalHelper.FirstConnectionRecordOrDefault<FkCiudadViewModel>("Ciudad", LibSearchCriteria.CreateCriteria("NombreCiudad", Ciudad), new Saw.Brl.SttDef.clsSettValueByCompanyNav());
        }

        private static bool EsEcuador() {
            return LibDefGen.ProgramInfo.IsCountryEcuador();
        }

        private static bool EsVenezuela() {
            return LibDefGen.ProgramInfo.IsCountryVenezuela();
        }

        private void ExecuteChooseCiudadCommand(string valNombreCiudad) {
            try {
                if (valNombreCiudad == null) {
                    valNombreCiudad = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("NombreCiudad", valNombreCiudad);
                ConexionCiudad = null;
                ConexionCiudad = ChooseRecord<FkCiudadViewModel>("Ciudad", vDefaultCriteria, null, string.Empty);
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        #endregion //Metodos Generados

    } //End of class VendedorViewModel

} //End of namespace Galac.Adm.Uil.Vendedor

