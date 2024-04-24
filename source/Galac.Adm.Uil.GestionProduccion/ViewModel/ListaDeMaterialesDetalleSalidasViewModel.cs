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
using Galac.Adm.Brl.GestionProduccion;
using Galac.Adm.Ccl.GestionProduccion;

namespace Galac.Adm.Uil.GestionProduccion.ViewModel {
    public class ListaDeMaterialesDetalleSalidasViewModel : LibInputDetailViewModelMfc<ListaDeMaterialesDetalleSalidas> {
        #region Constantes
        public const string CodigoArticuloInventarioPropertyName = "CodigoArticuloInventario";
        public const string DescripcionArticuloInventarioPropertyName = "DescripcionArticuloInventario";
        public const string CantidadPropertyName = "Cantidad";
        public const string UnidadDeVentaPropertyName = "UnidadDeVenta";
        public const string PorcentajeDeCostoPropertyName = "PorcentajeDeCosto";
        #endregion
        #region Variables
        private FkArticuloInventarioViewModel _ConexionCodigoArticuloInventario = null;
        private FkArticuloInventarioViewModel _ConexionDescripcionArticuloInventario = null;
        private FkArticuloInventarioViewModel _ConexionUnidadDeVenta = null;
        #endregion //Variables
        #region Propiedades

        public override string ModuleName {
            get { return "Salidas"; }
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

        public int  ConsecutivoListaDeMateriales {
            get {
                return Model.ConsecutivoListaDeMateriales;
            }
            set {
                if (Model.ConsecutivoListaDeMateriales != value) {
                    Model.ConsecutivoListaDeMateriales = value;
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
                }
            }
        }

        [LibGridColum("Código Inventario", eGridColumType.Connection, ConnectionDisplayMemberPath = "Codigo", ConnectionModelPropertyName = "CodigoArticuloInventario", ConnectionSearchCommandName = "ChooseCodigoArticuloInventarioCommand", MaxWidth=120)]
        public string  CodigoArticuloInventario {
            get {
                return Model.CodigoArticuloInventario;
            }
            set {
                if (Model.CodigoArticuloInventario != value) {
                    Model.CodigoArticuloInventario = value;
                    IsDirty = true;
                    RaisePropertyChanged(CodigoArticuloInventarioPropertyName);
                    if (LibString.IsNullOrEmpty(CodigoArticuloInventario, true)) {
                        ConexionCodigoArticuloInventario = null;
                    }
                }
            }
        }

        [LibRequired(ErrorMessage = "El campo Descripción Artículo es requerido.")]
        public string  DescripcionArticuloInventario {
            get {
                return Model.DescripcionArticuloInventario;
            }
            set {
                if (Model.DescripcionArticuloInventario != value) {
                    Model.DescripcionArticuloInventario = value;
                    IsDirty = true;
                    RaisePropertyChanged(DescripcionArticuloInventarioPropertyName);
                    if (LibString.IsNullOrEmpty(DescripcionArticuloInventario, true)) {
                        ConexionDescripcionArticuloInventario = null;
                    }
                }
            }
        }

        [LibRequired(ErrorMessage = "El campo Cantidad es requerido.")]
        [LibGridColum("Cantidad", eGridColumType.Numeric, Alignment = eTextAlignment.Right)]
        public decimal  Cantidad {
            get {
                return Model.Cantidad;
            }
            set {
                if (Model.Cantidad != value) {
                    Model.Cantidad = value;
                    IsDirty = true;
                    RaisePropertyChanged(CantidadPropertyName);
                }
            }
        }

        [LibRequired(ErrorMessage = "El campo Unidad es requerido.")]
        [LibGridColum("Unidad", eGridColumType.Connection, ConnectionDisplayMemberPath = "UnidadDeVenta", ConnectionModelPropertyName = "UnidadDeVenta", ConnectionSearchCommandName = "ChooseUnidadDeVentaCommand", MaxWidth=120)]
        public string  UnidadDeVenta {
            get {
                return Model.UnidadDeVenta;
            }
            set {
                if (Model.UnidadDeVenta != value) {
                    Model.UnidadDeVenta = value;
                    IsDirty = true;
                    RaisePropertyChanged(UnidadDeVentaPropertyName);
                    if (LibString.IsNullOrEmpty(UnidadDeVenta, true)) {
                        ConexionUnidadDeVenta = null;
                    }
                }
            }
        }

        [LibRequired(ErrorMessage = "El campo %Costo es requerido.")]
        [LibGridColum("%Costo", eGridColumType.Numeric, Alignment = eTextAlignment.Right)]
        public decimal  PorcentajeDeCosto {
            get {
                return Model.PorcentajeDeCosto;
            }
            set {
                if (Model.PorcentajeDeCosto != value) {
                    Model.PorcentajeDeCosto = value;
                    IsDirty = true;
                    RaisePropertyChanged(PorcentajeDeCostoPropertyName);
                }
            }
        }

        public ListaDeMaterialesViewModel Master {
            get;
            set;
        }

        public FkArticuloInventarioViewModel ConexionCodigoArticuloInventario {
            get {
                return _ConexionCodigoArticuloInventario;
            }
            set {
                if (_ConexionCodigoArticuloInventario != value) {
                    _ConexionCodigoArticuloInventario = value;
                    RaisePropertyChanged(CodigoArticuloInventarioPropertyName);
                }
                if (_ConexionCodigoArticuloInventario == null) {
                    CodigoArticuloInventario = string.Empty;
                }
            }
        }

        public FkArticuloInventarioViewModel ConexionDescripcionArticuloInventario {
            get {
                return _ConexionDescripcionArticuloInventario;
            }
            set {
                if (_ConexionDescripcionArticuloInventario != value) {
                    _ConexionDescripcionArticuloInventario = value;
                    RaisePropertyChanged(DescripcionArticuloInventarioPropertyName);
                }
                if (_ConexionDescripcionArticuloInventario == null) {
                    DescripcionArticuloInventario = string.Empty;
                }
            }
        }

        public FkArticuloInventarioViewModel ConexionUnidadDeVenta {
            get {
                return _ConexionUnidadDeVenta;
            }
            set {
                if (_ConexionUnidadDeVenta != value) {
                    _ConexionUnidadDeVenta = value;
                    RaisePropertyChanged(UnidadDeVentaPropertyName);
                }
                if (_ConexionUnidadDeVenta == null) {
                    UnidadDeVenta = string.Empty;
                }
            }
        }

        public RelayCommand<string> ChooseCodigoArticuloInventarioCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseDescripcionArticuloInventarioCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseUnidadDeVentaCommand {
            get;
            private set;
        }
        #endregion //Propiedades
        #region Constructores
        public ListaDeMaterialesDetalleSalidasViewModel()
            : base(new ListaDeMaterialesDetalleSalidas(), eAccionSR.Insertar, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
        }
        public ListaDeMaterialesDetalleSalidasViewModel(ListaDeMaterialesViewModel initMaster, ListaDeMaterialesDetalleSalidas initModel, eAccionSR initAction)
            : base(initModel, initAction, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            Master = initMaster;
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override void InitializeLookAndFeel(ListaDeMaterialesDetalleSalidas valModel) {
            base.InitializeLookAndFeel(valModel);
        }

        protected override ILibBusinessDetailComponent<IList<ListaDeMaterialesDetalleSalidas>, IList<ListaDeMaterialesDetalleSalidas>> GetBusinessComponent() {
            return new clsListaDeMaterialesDetalleSalidasNav();
        }

        protected override void InitializeCommands() {
            base.InitializeCommands();
            ChooseCodigoArticuloInventarioCommand = new RelayCommand<string>(ExecuteChooseCodigoArticuloInventarioCommand);
            ChooseDescripcionArticuloInventarioCommand = new RelayCommand<string>(ExecuteChooseDescripcionArticuloInventarioCommand);
            ChooseUnidadDeVentaCommand = new RelayCommand<string>(ExecuteChooseUnidadDeVentaCommand);
        }

        protected override void ReloadRelatedConnections() {
            base.ReloadRelatedConnections();
            ConexionCodigoArticuloInventario = Master.FirstConnectionRecordOrDefault<FkArticuloInventarioViewModel>("Artículo Inventario", LibSearchCriteria.CreateCriteria("Codigo", CodigoArticuloInventario));
            ConexionDescripcionArticuloInventario = Master.FirstConnectionRecordOrDefault<FkArticuloInventarioViewModel>("Artículo Inventario", LibSearchCriteria.CreateCriteria("Descripcion", DescripcionArticuloInventario));
            ConexionUnidadDeVenta = Master.FirstConnectionRecordOrDefault<FkArticuloInventarioViewModel>("Artículo Inventario", LibSearchCriteria.CreateCriteria("UnidadDeVenta", UnidadDeVenta));
        }

        private void ExecuteChooseCodigoArticuloInventarioCommand(string valCodigo) {
            try {
                if (valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo", valCodigo);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("ConsecutivoCompania", Mfc.GetInt("Compania"));
                ConexionCodigoArticuloInventario = Master.ChooseRecord<FkArticuloInventarioViewModel>("Artículo Inventario", vDefaultCriteria, vFixedCriteria, string.Empty);
                if (ConexionCodigoArticuloInventario != null) {
                    CodigoArticuloInventario = ConexionCodigoArticuloInventario.Codigo;
                    DescripcionArticuloInventario = ConexionCodigoArticuloInventario.Descripcion;
                    UnidadDeVenta = ConexionCodigoArticuloInventario.UnidadDeVenta;
                } else {
                    CodigoArticuloInventario = string.Empty;
                    DescripcionArticuloInventario = string.Empty;
                    UnidadDeVenta = string.Empty;
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void ExecuteChooseDescripcionArticuloInventarioCommand(string valDescripcion) {
            try {
                if (valDescripcion == null) {
                    valDescripcion = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Descripcion", valDescripcion);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("ConsecutivoCompania", Mfc.GetInt("Compania"));
                ConexionDescripcionArticuloInventario = Master.ChooseRecord<FkArticuloInventarioViewModel>("Artículo Inventario", vDefaultCriteria, vFixedCriteria, string.Empty);
                if (ConexionDescripcionArticuloInventario != null) {
                    CodigoArticuloInventario = ConexionDescripcionArticuloInventario.Codigo;
                    DescripcionArticuloInventario = ConexionDescripcionArticuloInventario.Descripcion;
                    UnidadDeVenta = ConexionDescripcionArticuloInventario.UnidadDeVenta;
                } else {
                    CodigoArticuloInventario = string.Empty;
                    DescripcionArticuloInventario = string.Empty;
                    UnidadDeVenta = string.Empty;
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void ExecuteChooseUnidadDeVentaCommand(string valUnidadDeVenta) {
            try {
                if (valUnidadDeVenta == null) {
                    valUnidadDeVenta = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("UnidadDeVenta", valUnidadDeVenta);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("ConsecutivoCompania", Mfc.GetInt("Compania"));
                ConexionUnidadDeVenta = Master.ChooseRecord<FkArticuloInventarioViewModel>("Artículo Inventario", vDefaultCriteria, vFixedCriteria, string.Empty);
                if (ConexionUnidadDeVenta != null) {
                    CodigoArticuloInventario = ConexionUnidadDeVenta.Codigo;
                    DescripcionArticuloInventario = ConexionUnidadDeVenta.Descripcion;
                    UnidadDeVenta = ConexionUnidadDeVenta.UnidadDeVenta;
                } else {
                    CodigoArticuloInventario = string.Empty;
                    DescripcionArticuloInventario = string.Empty;
                    UnidadDeVenta = string.Empty;
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }
        #endregion //Metodos Generados


    } //End of class ListaDeMaterialesDetalleSalidasViewModel

} //End of namespace Galac.Adm.Uil.GestionProduccion

