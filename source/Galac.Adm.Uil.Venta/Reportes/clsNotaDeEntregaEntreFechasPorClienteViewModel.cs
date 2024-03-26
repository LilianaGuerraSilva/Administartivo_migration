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
using Galac.Saw.Lib;
using System.Collections.ObjectModel;
using Galac.Adm.Uil.Venta.ViewModel;
using LibGalac.Aos.Base.Report;

namespace Galac.Adm.Uil.Venta.Reportes {

    public class clsNotaDeEntregaEntreFechasPorClienteViewModel : LibInputRptViewModelBase<FacturaRapida> {

        #region Constantes

        private const string FechaDesdePropertyName = "FechaDesde";
        private const string FechaHastaPropertyName = "FechaHasta";
        private const string IncluirNotasDeEntregasAnuladasPropertyName = "IncluirNotasDeEntregasAnuladas";
        private const string CantidadAImprimirPropertyName = "CantidadAImprimir";
        private const string CodigoClientePropertyName = "CodigoCliente";
        private const string NombreClientePropertyName = "NombreCliente";
        private const string IsEnabledClientePropertyName = "IsEnabledCliente";
        private DateTime _FechaDesde;
        private DateTime _FechaHasta;
        private bool _IncluirNotasDeEntregasAnuladasAsBool;
        private eCantidadAImprimir _CantidadAImprimirAsEnum;
        private string _CodigoCliente;
        private string _NombreCliente;
        private FkClienteViewModel _ConexionCliente;
        private bool _IsEnabledCliente;
        private const string IncluirDetalleNotasDeEntregasPropertyName = "IncluirDetalleNotasDeEntregas";
        private bool _IncluirDetalleNotasDeEntregasAsBool;
        const string IsVisibleNombreDelClientePropertyName = "IsVisibleNombreDelCliente";
        const string IsVisibleIncluirNotasDeEntregasAnuladasPropertyName = "IsVisibleIncluirNotasDeEntregasAnuladas";
        #endregion

        #region Propiedades

        public override string DisplayName {
            get { return "Notas de Entrega entre Fechas por Cliente"; }
        }

        public override bool IsSSRS {
            get { return false; }
        }

        [LibRequired(ErrorMessage = "El campo Fecha Desde es requerido.")]
        public DateTime FechaDesde {
            get {
                return _FechaDesde;
            }
            set {
                if (_FechaDesde != value) {
                    _FechaDesde = value;
                    RaisePropertyChanged(FechaDesdePropertyName);
                }
            }
        }

        [LibRequired(ErrorMessage = "El campo Fecha Hasta es requerido.")]
        public DateTime FechaHasta {
            get {
                return _FechaHasta;
            }
            set {
                if (_FechaHasta != value) {
                    _FechaHasta = value;
                    RaisePropertyChanged(FechaHastaPropertyName);
                }
            }
        }

        public bool IncluirNotasDeEntregasAnuladas {
            get {
                return _IncluirNotasDeEntregasAnuladasAsBool;
            }
            set {
                if (_IncluirNotasDeEntregasAnuladasAsBool != value) {
                    _IncluirNotasDeEntregasAnuladasAsBool = value;
                    RaisePropertyChanged(IncluirNotasDeEntregasAnuladasPropertyName);
                }
            }
        }

        public bool IncluirDetalleNotasDeEntregas {
            get {
                return _IncluirDetalleNotasDeEntregasAsBool;
            }
            set {
                if(_IncluirDetalleNotasDeEntregasAsBool != value) {
                    _IncluirDetalleNotasDeEntregasAsBool = value;
                    RaisePropertyChanged(IncluirDetalleNotasDeEntregasPropertyName);
                    RaisePropertyChanged(IsVisibleIncluirNotasDeEntregasAnuladasPropertyName);
                }
            }
        }

        public eCantidadAImprimir CantidadAImprimir {
            get {
                return _CantidadAImprimirAsEnum;
            }
            set {
                if (_CantidadAImprimirAsEnum != value) {
                    _CantidadAImprimirAsEnum = value;
                    RaisePropertyChanged(CantidadAImprimirPropertyName);
                    if (_CantidadAImprimirAsEnum == eCantidadAImprimir.All) {
                        ConexionCliente = null;
                        CodigoCliente = string.Empty;
                        NombreCliente = string.Empty;
                        IsEnabledCliente = false;
                    } else {
                        IsEnabledCliente = true;
                    }
                }
            }
        }

        public eCantidadAImprimir[] ArrayCantidadAImprimir {
            get {
                return LibEnumHelper<eCantidadAImprimir>.GetValuesInArray();
            }
        }
        public bool IsVisibleIncluirNotasDeEntregasAnuladas { get { return IncluirDetalleNotasDeEntregas == false; } }

        [LibCustomValidation("ClienteValidating")]
        [LibGridColum("Codigo", eGridColumType.Connection, ConnectionSearchCommandName = "ChooseNombreClienteCommand")]
        public string CodigoCliente {
            get {
                return _CodigoCliente;
            }
            set {
                if (_CodigoCliente != value) {
                    _CodigoCliente = value;
                    RaisePropertyChanged(CodigoClientePropertyName);
                }
            }
        }

        [LibGridColum("Nombre Cliente", eGridColumType.Connection, ConnectionSearchCommandName = "ChooseNombreClienteCommand")]
        public string NombreCliente {
            get {
                return _NombreCliente;
            }
            set {
                if (_NombreCliente != value) {
                    _NombreCliente = value;
                    RaisePropertyChanged(NombreClientePropertyName);
                    if (LibString.IsNullOrEmpty(NombreCliente, true)) {
                        ConexionCliente = null;
                    }
                }
            }
        }

        public RelayCommand<string> ChooseNombreClienteCommand {
            get;
            private set;
        }

        public FkClienteViewModel ConexionCliente {
            get {
                return _ConexionCliente;
            }
            set {
                if (value != null) {
                    _ConexionCliente = value;
                    CodigoCliente = value.Codigo;
                    NombreCliente = value.Nombre;
                } else if (value == null && _ConexionCliente != null) {
                    CodigoCliente = string.Empty;
                    NombreCliente = string.Empty;
                }
            }
        }

        public bool IsEnabledCliente {
            get {
                return _IsEnabledCliente;
            }
            set {
                if (_IsEnabledCliente != value) {
                    _IsEnabledCliente = value;
                    RaisePropertyChanged(IsEnabledClientePropertyName);
                }
            }
        }


        #endregion //Propiedades

        #region Constructores
        public clsNotaDeEntregaEntreFechasPorClienteViewModel() {
            FechaDesde = LibDate.DateFromMonthAndYear(1, LibDate.Today().Year, true);
            FechaHasta = LibDate.Today();
            CantidadAImprimir = eCantidadAImprimir.All;
        }

        protected override void InitializeCommands() {
            base.InitializeCommands();
            ChooseNombreClienteCommand = new RelayCommand<string>(ExecuteChooseNombreClienteCommand);
        }

        protected override ILibBusinessSearch GetBusinessComponent() {
            return new clsFacturaRapidaNav();
        }

        private ValidationResult ClienteValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if (LibString.IsNullOrEmpty(CodigoCliente) && CantidadAImprimir == eCantidadAImprimir.One) {
                vResult = new ValidationResult("El cliente debe ser especificado");
            }
            return vResult;
        }

        #endregion //Constructores

        #region Metodos

        private void ExecuteChooseNombreClienteCommand(string valNombre) {
            try {
                if (valNombre == null) {
                    valNombre = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Gv_Cliente_B1.Nombre", valNombre);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("Gv_Cliente_B1.ConsecutivoCompania", LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"));
                var vConexionNombreCliente = ChooseRecord<FkClienteViewModel>("Cliente", vDefaultCriteria, vFixedCriteria, string.Empty);
                if (vConexionNombreCliente != null) {
                    ConexionCliente = vConexionNombreCliente;
                    CodigoCliente = ConexionCliente.Codigo;
                    NombreCliente = ConexionCliente.Nombre;
                } else {
                    CodigoCliente = string.Empty;
                    NombreCliente = string.Empty;
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibMessages.RaiseError.ShowError(vEx);
            }
        }

        #endregion //Metodos

    } //End of class NotaDeEntregaEntreFechasPorClienteViewModel

} //End of namespace Galac.Adm.Uil.Venta

