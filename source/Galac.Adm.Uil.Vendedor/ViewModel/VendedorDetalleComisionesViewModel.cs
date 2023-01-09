using System;
using System.Collections.Generic;
using LibGalac.Aos.Base;
using LibGalac.Aos.UI.Mvvm;
using LibGalac.Aos.UI.Mvvm.Command;
using LibGalac.Aos.UI.Mvvm.Helpers;
using LibGalac.Aos.UI.Mvvm.Messaging;
using LibGalac.Aos.UI.Mvvm.Validation;
using Galac.Adm.Brl.Vendedor;
using Galac.Adm.Ccl.Vendedor;
using Galac.Saw.Uil.Tablas.ViewModel;
using LibGalac.Aos.Uil;
using Galac.Saw.Brl.Tablas;

namespace Galac.Adm.Uil.Vendedor.ViewModel {
    public class VendedorDetalleComisionesViewModel : LibInputDetailViewModelMfc<VendedorDetalleComisiones> {
        #region Constantes
        public const string ConsecutivoVendedorPropertyName = "ConsecutivoVendedor";
        public const string ConsecutivoRenglonPropertyName = "ConsecutivoRenglon";
        public const string NombreDeLineaDeProductoPropertyName = "NombreDeLineaDeProducto";
        public const string TipoDeComisionPropertyName = "TipoDeComision";
        public const string MontoPropertyName = "Monto";
        public const string PorcentajePropertyName = "Porcentaje";
        public const string IsVisibleMontoPropertyName = "IsVisibleMonto";
        public const string IsVisiblePorcentajePropertyName = "IsVisiblePorcentaje";
        #endregion
        #region Variables
        private FkLineaDeProductoViewModel _ConexionLineaDeProducto = null;
        #endregion
        #region Propiedades

        public override string ModuleName {
            get { return "Comisiones de Vendedor por Linea de Producto"; }
        }

        public int  ConsecutivoCompania {
            get {
                return Model.ConsecutivoCompania;
            }
            set {
                if (Model.ConsecutivoCompania != value) {
                    Model.ConsecutivoCompania = value;
                }
            }
        }

        [LibRequired(ErrorMessage = "El campo Consecutivo Vendedor es requerido.")]
        public int  ConsecutivoVendedor {
            get {
                return Model.ConsecutivoVendedor;
            }
            set {
                if (Model.ConsecutivoVendedor != value) {
                    Model.ConsecutivoVendedor = value;
                    IsDirty = true;
                    RaisePropertyChanged(ConsecutivoVendedorPropertyName);
                }
            }
        }

        [LibRequired(ErrorMessage = "El campo Consecutivo Renglon es requerido.")]
        public int  ConsecutivoRenglon {
            get {
                return Model.ConsecutivoRenglon;
            }
            set {
                if (Model.ConsecutivoRenglon != value) {
                    Model.ConsecutivoRenglon = value;
                    IsDirty = true;
                    RaisePropertyChanged(ConsecutivoRenglonPropertyName);
                }
            }
        }

        [LibRequired(ErrorMessage = "El campo Nombre De Linea De Producto es requerido.")]
        [LibGridColum("Nombre De Linea De Producto", MaxLength=20)]
        public string  NombreDeLineaDeProducto {
            get {
                return Model.NombreDeLineaDeProducto;
            }
            set {
                if (Model.NombreDeLineaDeProducto != value) {
                    Model.NombreDeLineaDeProducto = value;
                    IsDirty = true;
                    RaisePropertyChanged(NombreDeLineaDeProductoPropertyName);
                }
            }
        }

        [LibRequired(ErrorMessage = "El campo Tipo De Comision es requerido.")]
        [LibGridColum("Tipo De Comision", eGridColumType.Enum, PrintingMemberPath = "TipoDeComisionStr")]
        public eTipoComision  TipoDeComision {
            get {
                return Model.TipoDeComisionAsEnum;
            }
            set {
                if (Model.TipoDeComisionAsEnum != value) {
                    Model.TipoDeComisionAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(TipoDeComisionPropertyName);
                    RaisePropertyChanged(IsVisibleMontoPropertyName);
                    RaisePropertyChanged(IsVisiblePorcentajePropertyName);
                }
            }
        }

        [LibRequired(ErrorMessage = "El campo Monto es requerido.")]
        [LibGridColum("Monto", eGridColumType.Numeric, Alignment = eTextAlignment.Right)]
        public decimal  Monto {
            get {
                return Model.Monto;
            }
            set {
                if (Model.Monto != value) {
                    Model.Monto = value;
                    IsDirty = true;
                    RaisePropertyChanged(MontoPropertyName);
                }
            }
        }

        [LibRequired(ErrorMessage = "El campo Porcentaje es requerido.")]
        [LibGridColum("Porcentaje", eGridColumType.Numeric, Alignment = eTextAlignment.Right)]
        public decimal  Porcentaje {
            get {
                return Model.Porcentaje;
            }
            set {
                if (Model.Porcentaje != value) {
                    Model.Porcentaje = value;
                    IsDirty = true;
                    RaisePropertyChanged(PorcentajePropertyName);
                }
            }
        }

        public eTipoComision[] ArrayTipoComision {
            get {
                return LibEnumHelper<eTipoComision>.GetValuesInArray();
            }
        }
        public bool IsVisibleMonto {
            get {
                return (TipoDeComision == eTipoComision.PorMonto);
            }
        }
        public bool IsVisiblePorcentaje {
            get {
                return (TipoDeComision == eTipoComision.PorPorcentaje);
            }
        }
        public VendedorViewModel Master {
            get;
            set;
        }

        public FkLineaDeProductoViewModel ConexionNombreDeLineaDeProducto {
            get {
                return _ConexionLineaDeProducto;
            }
            set {
                if (_ConexionLineaDeProducto != value) {
                    _ConexionLineaDeProducto = value;
                    RaisePropertyChanged(NombreDeLineaDeProductoPropertyName);
                }
                if (_ConexionLineaDeProducto == null) {
                    NombreDeLineaDeProducto = string.Empty;
                }
            }
        }

        public RelayCommand<string> ChooseNombreDeLineaDeProductoCommand {
            get;
            private set;
        }
        #endregion //Propiedades
        #region Constructores
        public VendedorDetalleComisionesViewModel()
            : base(new VendedorDetalleComisiones(), eAccionSR.Insertar, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
        }
        public VendedorDetalleComisionesViewModel(VendedorViewModel initMaster, VendedorDetalleComisiones initModel, eAccionSR initAction)
            : base(initModel, initAction, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            Master = initMaster;
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override void InitializeLookAndFeel(VendedorDetalleComisiones valModel) {
            base.InitializeLookAndFeel(valModel);
        }

        protected override ILibBusinessDetailComponent<IList<VendedorDetalleComisiones>, IList<VendedorDetalleComisiones>> GetBusinessComponent() {
            return new clsVendedorDetalleComisionesNav();
        }

        protected override void InitializeCommands() {
            base.InitializeCommands();
            ChooseNombreDeLineaDeProductoCommand = new RelayCommand<string>(ExecuteChooseNombreDeLineaDeProductoCommand);
        }

        protected override void ReloadRelatedConnections() {
            base.ReloadRelatedConnections();
            // ConexionCodigoArticuloInventario = Master.FirstConnectionRecordOrDefault<FkArticuloInventarioViewModel>("Artículo Inventario", LibSearchCriteria.CreateCriteria("CodigoArticuloInventario", CodigoArticuloInventario));

        }

        private void ExecuteChooseNombreDeLineaDeProductoCommand(string valNombre) {
            try {
                if (valNombre == null) {
                    valNombre = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Gv_LineaDeProducto_B1.Nombre", valNombre);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("Gv_LineaDeProducto_B1.ConsecutivoCompania", Mfc.GetInt("Compania"));
                ConexionNombreDeLineaDeProducto = LibFKRetrievalHelper.ChooseRecord<FkLineaDeProductoViewModel>("Línea de Producto", vDefaultCriteria, vFixedCriteria, new clsLineaDeProductoNav(), string.Empty);
                if (ConexionNombreDeLineaDeProducto != null) {
                    NombreDeLineaDeProducto = ConexionNombreDeLineaDeProducto.Nombre;
                } else {
                    NombreDeLineaDeProducto = string.Empty;
                }
            } catch (AccessViolationException) {
                throw;
            } catch (Exception vEx) {
                LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        #endregion //Metodos Generados

    } //End of class VendedorDetalleComisionesViewModel

} //End of namespace Galac.Adm.Uil.Vendedor

