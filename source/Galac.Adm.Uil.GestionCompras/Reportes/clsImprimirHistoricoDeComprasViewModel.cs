using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Xml.Linq;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Report;
using LibGalac.Aos.Catching;
using LibGalac.Aos.DefGen;
using LibGalac.Aos.UI.Mvvm;
using LibGalac.Aos.UI.Mvvm.Command;
using LibGalac.Aos.UI.Mvvm.Helpers;
using LibGalac.Aos.UI.Mvvm.Messaging;
using LibGalac.Aos.UI.Mvvm.Validation;
using Galac.Adm.Brl.GestionCompras;
using Galac.Adm.Ccl.GestionCompras;
using Galac.Adm.Uil.GestionCompras.ViewModel;
using LibGalac.Aos.Uil;
using System.Xml;

namespace Galac.Adm.Uil.GestionCompras.Reportes {
    public class clsImprimirHistoricoDeComprasViewModel : LibInputRptViewModelBase<Compra> {
        #region Constantes
        public const string FechaInicialPropertyName = "FechaInicial";
        public const string FechaFinalPropertyName = "FechaFinal";
        public const string CantidadAImprimirPropertyName = "CantidadAImprimir";
        public const string CodigoProductoPropertyName = "CodigoProducto";
        public const string EntidadABuscarPropertyName = "EntidadABuscar";
        public const string IsVisiblePropertyName = "IsVisible";

        #endregion
        #region Variables
        DateTime _FechaInicial;
        DateTime _FechaFinal;
        eReporteCostoDeCompras _CantidadAImprimir;
        FkArticuloInventarioViewModel _ConexionCodigoProducto;
        string _CodigoProducto;
        #endregion Constantes
        #region Propiedades

        public override string DisplayName {
            get {
                return "Historico De Compras";
            }
        }

        public LibXmlMemInfo AppMemoryInfo {
            get;
            set;
        }

        public LibXmlMFC Mfc {
            get;
            set;
        }

        [LibCustomValidation("FechaInicialValidating")]
        public DateTime  FechaInicial {
            get {
                return _FechaInicial;
            }
            set {
                if (_FechaInicial != value) {
                    _FechaInicial = value;
                    RaisePropertyChanged(FechaInicialPropertyName);
                }
            }
        }

        [LibCustomValidation("FechaFinalValidating")]
        public DateTime  FechaFinal {
            get {
                return _FechaFinal;
            }
            set {
                if (_FechaFinal != value) {
                    _FechaFinal = value;
                    RaisePropertyChanged(FechaFinalPropertyName);
                }
            }
        }

        public eReporteCostoDeCompras CantidadAImprimir {
            get {
                return _CantidadAImprimir;
            }
            set {
                if (_CantidadAImprimir != value) {
                    _CantidadAImprimir = value;
                    _CodigoProducto = "";
                    RaisePropertyChanged(CantidadAImprimirPropertyName);
                    RaisePropertyChanged(CodigoProductoPropertyName);
                    RaisePropertyChanged(CodigoProductoPropertyName);
                    RaisePropertyChanged(EntidadABuscarPropertyName);
                    RaisePropertyChanged(IsVisiblePropertyName);
                }
            }
        }

        [LibRequired(ErrorMessage = "El campo Codigo Producto es requerido.", IsEnabledPropertyName = IsVisiblePropertyName)]
        [LibGridColum("Producto", eGridColumType.Connection, ConnectionDisplayMemberPath = "Codigo", ConnectionModelPropertyName = "CodigoProducto", ConnectionSearchCommandName = "ChooseCodigoProductoCommand", MaxWidth = 120)]
        public string CodigoProducto {
            get {
                return _CodigoProducto;
            }
            set {
                if (_CodigoProducto != value) {
                    _CodigoProducto = value;
                    RaisePropertyChanged(CodigoProductoPropertyName);
                    if (LibString.IsNullOrEmpty(CodigoProducto, true)) {
                        ConexionCodigoProducto = null;
                    }
                }
            }
        }

        public FkArticuloInventarioViewModel ConexionCodigoProducto {
            get {
                return _ConexionCodigoProducto;
            }
            set {
                if (_ConexionCodigoProducto != value) {
                    _ConexionCodigoProducto = value;
                    if (_ConexionCodigoProducto != null) {
                        CodigoProducto = ConexionCodigoProducto.CodigoCompuesto;
                    }
                }
                if (_ConexionCodigoProducto == null) {
                    CodigoProducto = string.Empty;
                }
            }
        }

        public eReporteCostoDeCompras[] ArrayCantidadAImprimir {
            get {
                return LibEnumHelper<eReporteCostoDeCompras>.GetValuesInArray();
            }
        }

        public bool IsVisible {
            get {
                return _CantidadAImprimir != eReporteCostoDeCompras.TodosLosArticulos;
            }
        }

        public string EntidadABuscar {
            get {
                if (_CantidadAImprimir == eReporteCostoDeCompras.UnArticulo) {
                    return "Codigo del Producto";
                } else if (_CantidadAImprimir == eReporteCostoDeCompras.UnaLineaDeProducto) {
                    return "Linea de Producto";
                } else {
                    return "";
                }
            }
        }

        public RelayCommand<string> ChooseCodigoProductoCommand {
            get;
            private set;
        }
        #endregion //Propiedades
        #region Constructores
        public clsImprimirHistoricoDeComprasViewModel() {
            FechaInicial = LibDate.Today();
            FechaFinal = LibDate.Today();
            CantidadAImprimir = eReporteCostoDeCompras.UnArticulo;
            CodigoProducto = "";
            ConexionCodigoProducto = FirstConnectionRecordOrDefault<FkArticuloInventarioViewModel>("Articulo Inventario", LibSearchCriteria.CreateCriteria("CodigoCompuesto", CodigoProducto));
        }
        #endregion //Constructores
        #region Metodos Generados
        protected override ILibBusinessSearch GetBusinessComponent() {
            return new clsCompraNav();
        }

        private ValidationResult FechaInicialValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if (LibDefGen.DateIsGreaterThanDateLimitForEnterData(FechaInicial, false, eAccionSR.Imprimir)) {
                vResult = new ValidationResult(LibDefGen.TooltipMessageDateRestrictionDemoProgram("Fecha Inicial"));
            }
            return vResult;
        }

        private ValidationResult FechaFinalValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if (!LibDate.F1IsGreaterOrEqualThanF2(FechaFinal, FechaInicial)) {
                vResult = new ValidationResult("La fecha de traslado debe ser mayor o igual a la fecha: " + FechaInicial.ToShortDateString());
            }
            return vResult;
        }

        private void ExecuteChooseCodigoProductoCommand(string valCodigo) {
            try {
                if (CantidadAImprimir == eReporteCostoDeCompras.UnArticulo) {
                       ChooseCodigoProducto(valCodigo);
                } else if (CantidadAImprimir == eReporteCostoDeCompras.UnaLineaDeProducto) {
                       ChooseLineaDeProducto(valCodigo);
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, DisplayName);
            }
        }

        private void ChooseCodigoProducto(string valCodigo) {
            if (valCodigo == null) {
                valCodigo = string.Empty;
            }
            LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("CodigoCompuesto", valCodigo);
            vDefaultCriteria.Add(LibSearchCriteria.CreateCriteria("StatusdelArticulo ", "0"), eLogicOperatorType.And);
            vDefaultCriteria.Add("TipoDeArticulo", eBooleanOperatorType.IdentityInequality, "2", eLogicOperatorType.And);
            LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("ConsecutivoCompania", Mfc.GetInt("Compania"));
            ConexionCodigoProducto = ChooseRecord<FkArticuloInventarioViewModel>("Articulo Inventario", vDefaultCriteria, vFixedCriteria, string.Empty);
            if (ConexionCodigoProducto != null) {
                CodigoProducto = ConexionCodigoProducto.CodigoCompuesto;
            } else {
                CodigoProducto = string.Empty;
            }
        }

        private void ChooseLineaDeProducto(string valNombre) {

            LibSearch insLibSearch = new LibSearch();
            List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
            List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
            vSearchValues = insLibSearch.CreateListOfParameter("Nombre =");
            vFixedValues = insLibSearch.CreateListOfParameter("ConsecutivoCompania = " + Mfc.GetInt("Compania"));
            System.Xml.XmlDocument vXmlDocument = null;
            if (Galac.Saw.Uil.Tablas.clsLineaDeProductoMenu.ChooseFromInterop(ref vXmlDocument, vSearchValues, vFixedValues)) {
                XElement document = XElement.Parse(vXmlDocument.OuterXml);
                XElement element = document.Element("GpResult").Element("Nombre");
                CodigoProducto = element.Value;
            }
        }

        #endregion //Metodos Generados
        public override bool IsSSRS {
            get {
                return false;
            }
        }
        protected override void InitializeCommands() {
            base.InitializeCommands();
            ChooseCodigoProductoCommand = new RelayCommand<string>(ExecuteChooseCodigoProductoCommand);
        }

    } //End of class CostoDeCompraEntreFechasViewModel

} //End of namespace Galac.Adm.Uil.GestionCompras

