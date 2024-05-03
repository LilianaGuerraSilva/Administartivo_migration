using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Report;
using LibGalac.Aos.UI.Cib;
using LibGalac.Aos.UI.Mvvm;
using LibGalac.Aos.UI.Mvvm.Command;
using LibGalac.Aos.UI.Mvvm.Helpers;
using Galac.Adm.Ccl.GestionProduccion;
using Galac.Adm.Brl.GestionProduccion;
using Galac.Adm.Uil.GestionProduccion.ViewModel;
using LibGalac.Aos.UI.Mvvm.Validation;
using System.ComponentModel.DataAnnotations;
using Galac.Saw.Ccl.Inventario;

namespace Galac.Adm.Uil.GestionProduccion.Reportes {
    public class clsListaDeMaterialesDeInventarioAProducirViewModel : LibInputRptViewModelBase<ListaDeMateriales> {

        #region Constantes

        private const string CantidadAImprimirPropertyName = "CantidadAImprimir";
        private const string CodigoArticuloInventarioPropertyName = "CodigoArticuloInventario";
        private const string DescripcionArticuloInventarioPropertyName = "DescripcionArticuloInventario";
        private const string IsEnabledCodigoArticuloInventarioPropertyName = "IsEnabledCodigoArticuloInventario";
        private const string CantidadAProducirPropertyName = "CantidadAProducir";

        #endregion

        #region Variables

        private eCantidadAImprimir _CantidadAImprimir;
        private string _CodigoArticuloInventario;
        private string _DescripcionArticuloInventario;
        private bool _IsEnabledCodigoArticuloInventario;
        private decimal _CantidadAProducir;
        private FkArticuloInventarioViewModel _ConexionCodigoArticuloInventario = null;

        #endregion //Variables

        #region Propiedades

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
                    _CodigoArticuloInventario = string.Empty;
                }
            }
        }

        [LibCustomValidation("IsCodigoArticuloRequired")]
        public string CodigoArticuloInventario {
            get {
                return _CodigoArticuloInventario;
            }
            set {
                if (_CodigoArticuloInventario != value) {
                    _CodigoArticuloInventario = value;
                    RaisePropertyChanged(CodigoArticuloInventarioPropertyName);
                    if (LibString.IsNullOrEmpty(CodigoArticuloInventario, true)) {
                        ConexionCodigoArticuloInventario = null;
                        DescripcionArticuloInventario = string.Empty;
                    } else {
                        DescripcionArticuloInventario = ConexionCodigoArticuloInventario.Descripcion;
                    }
                }
            }
        }

        public string DescripcionArticuloInventario {
            get {
                return _DescripcionArticuloInventario;
            }
            set {
                if (_DescripcionArticuloInventario != value) {
                    _DescripcionArticuloInventario = value;
                    RaisePropertyChanged(DescripcionArticuloInventarioPropertyName);
                }
            }
        }

        public eCantidadAImprimir CantidadAImprimir {
            get {
                return _CantidadAImprimir;
            }
            set {
                if (_CantidadAImprimir != value) {
                    _CantidadAImprimir = value;
                    RaisePropertyChanged(CantidadAImprimirPropertyName);
                    RaisePropertyChanged(IsEnabledCodigoArticuloInventarioPropertyName);
                    if (eCantidadAImprimir.One.Equals(_CantidadAImprimir)) {
                        IsEnabledCodigoArticuloInventario = true;
                    } else {
                        IsEnabledCodigoArticuloInventario = false;
                        DescripcionArticuloInventario = string.Empty;
                        CodigoArticuloInventario = string.Empty;
                    }
                }
            }
        }

        public decimal CantidadAProducir {
            get {
                return _CantidadAProducir;
            }
            set {
                if (_CantidadAProducir != value) {
                    _CantidadAProducir = value;
                    RaisePropertyChanged(CantidadAProducirPropertyName);
                    if (_CantidadAProducir == 0) {
                        _CantidadAProducir = 1;
                    }
                }
            }
        }

        public override string DisplayName {
            get { return "Lista de Materiales de Inventario a Producir"; }
        }

        public override bool IsSSRS {
            get { return false; }
        }

        public RelayCommand<string> ChooseCodigoArticuloInventarioCommand {
            get;
            private set;
        }

        public bool IsEnabledCodigoArticuloInventario {
            get {
                return _IsEnabledCodigoArticuloInventario;
            }
            set {
                if (_IsEnabledCodigoArticuloInventario != value) {
                    _IsEnabledCodigoArticuloInventario = value;
                    RaisePropertyChanged(IsEnabledCodigoArticuloInventarioPropertyName);
                }
            }
        }

        #endregion

        #region Constructores

        public clsListaDeMaterialesDeInventarioAProducirViewModel() {
            _CantidadAImprimir = eCantidadAImprimir.All;
            _CodigoArticuloInventario = string.Empty;
            _CantidadAProducir = 1;
        }

        #endregion //Constructores

        #region Metodos Generados

        public eCantidadAImprimir[] ECantidadAImprimir {
            get {
                return LibEnumHelper<eCantidadAImprimir>.GetValuesInArray();
            }
        }

        protected override ILibBusinessSearch GetBusinessComponent() {
            return new clsListaDeMaterialesNav();
        }

        protected override void InitializeCommands() {
            base.InitializeCommands();
            ChooseCodigoArticuloInventarioCommand = new RelayCommand<string>(ExecuteChooseCodigoArticuloInventarioCommand);
        }

        private void ExecuteChooseCodigoArticuloInventarioCommand(string valCodigo) {
            try {
                if (valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Gv_ArticuloInventario_B2.Codigo", valCodigo);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("ConsecutivoCompania", LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"));
                vFixedCriteria.Add(LibSearchCriteria.CreateCriteria("StatusdelArticulo ", eStatusArticulo.Vigente), eLogicOperatorType.And);
                vFixedCriteria.Add("TipoArticuloInv", eBooleanOperatorType.IdentityEquality, eTipoArticuloInv.Simple, eLogicOperatorType.And);
                vFixedCriteria.Add("TipoDeArticulo", eBooleanOperatorType.IdentityEquality, eTipoDeArticulo.Mercancia, eLogicOperatorType.And);
                ConexionCodigoArticuloInventario = ChooseRecord<FkArticuloInventarioViewModel>("Artículo Inventario", vDefaultCriteria, vFixedCriteria, string.Empty);
                if(ConexionCodigoArticuloInventario != null) {
                    CodigoArticuloInventario = ConexionCodigoArticuloInventario.Codigo;
                    DescripcionArticuloInventario = ConexionCodigoArticuloInventario.Descripcion;
                } else {
                    CodigoArticuloInventario = string.Empty;
                    DescripcionArticuloInventario = string.Empty;
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, Galac.Adm.Rpt.GestionProduccion.clsListaDeMaterialesDeInventarioAProducir.ReportName);
            }
        }

        #endregion //Metodos Generados

        #region Código Programador

        private ValidationResult IsCodigoArticuloRequired() {
            ValidationResult vResult = ValidationResult.Success;
            if (IsEnabledCodigoArticuloInventario && LibText.IsNullOrEmpty(CodigoArticuloInventario)) {
                vResult = new ValidationResult("Debe seleccionar un Articulo de Inventario a consultar.");
            }
            return vResult;
        }

        #endregion //Código Programador

    } //End of class clsListaDeMaterialesDeInventarioAProducirViewModel

} //End of namespace Galac.Adm.Uil.GestionProduccion

