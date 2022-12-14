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
using Galac.Saw.Ccl.Inventario;

namespace Galac.Adm.Uil.GestionProduccion.ViewModel {
    public class ListaDeMaterialesDetalleArticuloViewModel : LibInputDetailViewModelMfc<ListaDeMaterialesDetalleArticulo> {

        #region Constantes

        private const string CodigoArticuloInventarioPropertyName = "CodigoArticuloInventario";
        private const string DescripcionArticuloInventarioPropertyName = "DescripcionArticuloInventario";
        private const string CantidadPropertyName = "Cantidad";

        #endregion
        #region Variables

        private FkArticuloInventarioViewModel _ConexionCodigoArticuloInventario = null;

        #endregion //Variables

        #region Propiedades

        public override string ModuleName {
            get { return "Productos y/o Servicios"; }
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

        public int ConsecutivoListaDeMateriales {
            get {
                return Model.ConsecutivoListaDeMateriales;
            }
            set {
                if (Model.ConsecutivoListaDeMateriales != value) {
                    Model.ConsecutivoListaDeMateriales = value;
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

        [LibGridColum("Código Artículo", eGridColumType.Connection, ConnectionDisplayMemberPath = "Codigo", ConnectionModelPropertyName = "CodigoArticuloInventario", ConnectionSearchCommandName = "ChooseCodigoArticuloInventarioCommand", Width = 180)]
        public string CodigoArticuloInventario {
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

        [LibRequired(ErrorMessage = "El campo Descripción es requerido.")]
        [LibGridColum("Descripción", eGridColumType.Connection, ConnectionDisplayMemberPath = "Descripcion", ConnectionModelPropertyName = "DescripcionArticuloInventario", ConnectionSearchCommandName = "ChooseDescripcionArticuloInventarioCommand", Width = 300, Trimming = System.Windows.TextTrimming.WordEllipsis)]
        public string DescripcionArticuloInventario {
            get {
                return Model.DescripcionArticuloInventario;
            }
            set {
                if (Model.DescripcionArticuloInventario != value) {
                    Model.DescripcionArticuloInventario = value;
                    IsDirty = true;
                    RaisePropertyChanged(DescripcionArticuloInventarioPropertyName);
                }
            }
        }

        [LibRequired(ErrorMessage = "El campo Cantidad es requerido.")]
        [LibGridColum("Cantidad", eGridColumType.Numeric, Alignment = eTextAlignment.Right, Width = 90, ConditionalPropertyDecimalDigits = "DecimalDigits")]
        public decimal Cantidad {
            get {
                return Model.Cantidad;
            }
            set {
                if (Model.Cantidad != value) {
                    Model.Cantidad = value;
                    RaisePropertyChanged(CantidadPropertyName);
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

        public RelayCommand<string> ChooseCodigoArticuloInventarioCommand {
            get;
            private set;
        }

        public int DecimalDigits {
            get {
                return 8;
            }
        }

        #endregion //Propiedades

        #region Constructores

        public ListaDeMaterialesDetalleArticuloViewModel()
            : base(new ListaDeMaterialesDetalleArticulo(), eAccionSR.Insertar, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
        }

        public ListaDeMaterialesDetalleArticuloViewModel(ListaDeMaterialesViewModel initMaster, ListaDeMaterialesDetalleArticulo initModel, eAccionSR initAction)
            : base(initModel, initAction, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            Master = initMaster;
        }

        #endregion //Constructores

        #region Metodos Generados

        protected override void InitializeLookAndFeel(ListaDeMaterialesDetalleArticulo valModel) {
            base.InitializeLookAndFeel(valModel);
        }

        protected override ILibBusinessDetailComponent<IList<ListaDeMaterialesDetalleArticulo>, IList<ListaDeMaterialesDetalleArticulo>> GetBusinessComponent() {
            return new clsListaDeMaterialesDetalleArticuloNav();
        }

        protected override void InitializeCommands() {
            base.InitializeCommands();
            ChooseCodigoArticuloInventarioCommand = new RelayCommand<string>(ExecuteChooseCodigoArticuloInventarioCommand);
        }

        protected override void ReloadRelatedConnections() {
            base.ReloadRelatedConnections();
            // ConexionCodigoArticuloInventario = Master.FirstConnectionRecordOrDefault<FkArticuloInventarioViewModel>("Artículo Inventario", LibSearchCriteria.CreateCriteria("CodigoArticuloInventario", CodigoArticuloInventario));

        }

        private void ExecuteChooseCodigoArticuloInventarioCommand(string valCodigo) {
            try {
                if (valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Gv_ArticuloInventario_B2.Codigo", valCodigo);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("ConsecutivoCompania", Mfc.GetInt("Compania"));
                vFixedCriteria.Add(LibSearchCriteria.CreateCriteria("StatusdelArticulo ", eStatusArticulo.Vigente), eLogicOperatorType.And);
                vFixedCriteria.Add("TipoArticuloInv", eBooleanOperatorType.IdentityEquality, eTipoArticuloInv.Simple, eLogicOperatorType.And);
                vFixedCriteria.Add("TipoDeArticulo", eBooleanOperatorType.IdentityInequality, eTipoDeArticulo.ProductoCompuesto, eLogicOperatorType.And);
                ConexionCodigoArticuloInventario = Master.ChooseRecord<FkArticuloInventarioViewModel>("Artículo Inventario", vDefaultCriteria, vFixedCriteria, string.Empty);
                if (ConexionCodigoArticuloInventario != null) {
                    CodigoArticuloInventario = ConexionCodigoArticuloInventario.Codigo;
                    DescripcionArticuloInventario = ConexionCodigoArticuloInventario.Descripcion;
                } else {
                    CodigoArticuloInventario = string.Empty;
                    DescripcionArticuloInventario = string.Empty;
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        #endregion //Metodos Generados


    } //End of class ListaDeMaterialesDetalleArticuloViewModel

} //End of namespace Galac.Saw.Uil.Inventario

