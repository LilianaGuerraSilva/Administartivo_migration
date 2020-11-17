using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using LibGalac.Aos.DefGen;
using LibGalac.Aos.Base.Report;
//using LibGalac.Aos.UI.Cib;
using LibGalac.Aos.UI.Mvvm;
using LibGalac.Aos.UI.Mvvm.Command;
using LibGalac.Aos.UI.Mvvm.Helpers;
using LibGalac.Aos.UI.Mvvm.Messaging;
using LibGalac.Aos.UI.Mvvm.Ribbon;
using LibGalac.Aos.UI.Mvvm.Validation;
using Galac.Adm.Ccl.Venta;
using Galac.Adm.Brl.Venta;
using Galac.Adm.Uil.Venta.ViewModel;
using System.Collections.ObjectModel;
using LibGalac.Aos.Ccl.Usal;
using System.ComponentModel.DataAnnotations;

namespace Galac.Adm.Uil.Venta.Reportes {

    public class clsCuadreCajaPorUsuarioViewModel : LibInputRptViewModelBase<Caja> {
        #region Constantes
        private const string FechaInicialPropertyName = "FechaInicial";
        private const string FechaFinalPropertyName = "FechaFinal";
        private const string MonedaPropertyName = "Moneda";
        private const string TipoDeInformePropertyName = "TipoDeInforme";
        private const string CantidadAImprimirPropertyName = "CantidadAImprimir";
        private const string IsEnabledNombreDelOperadorPropertyName = "IsEnabledNombreDelOperador";
        private const string IsVisibleNombreDelOperadorPropertyName = "IsVisibleNombreDelOperador";
        private const string NombreOperadorPropertyName = "NombreDelOperador";
        #endregion
        #region Variables
        private DateTime _FechaInicial;
        private DateTime _FechaFinal;
        private Galac.Saw.Lib.eMonedaParaImpresion _Moneda;
        private Galac.Saw.Lib.eTipoDeInforme _TipoDeInforme;
        private Galac.Saw.Lib.eCantidadAImprimir _CantidadAImprimir;
        private bool _IsEnabledNombreDelOperador;
        private bool _IsVisibleNombreDelOperador;
        private FkGUserViewModel _ConexionNombreDelOperador = null;
        private string _NombreDelOperador;
        #endregion //Variables
        #region Propiedades

        public override string DisplayName {
            get { return "Cuadre de Caja por Usuario"; }
        }

        public LibXmlMemInfo AppMemoryInfo { get; set; }

        public LibXmlMFC Mfc { get; set; }

        public override bool IsSSRS {
            get {
                return false;
            }
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

        public Saw.Lib.eMonedaParaImpresion Moneda {
            get {
                return _Moneda;
            }
            set {
                if (_Moneda != value) {
                    _Moneda = value;
                    RaisePropertyChanged(MonedaPropertyName);
                }
            }
        }

        public ObservableCollection<Galac.Saw.Lib.eMonedaParaImpresion> _MonedaDeReporte = new ObservableCollection<Galac.Saw.Lib.eMonedaParaImpresion>();
        public ObservableCollection<Galac.Saw.Lib.eMonedaParaImpresion> MonedaDeReporte {
            get { return _MonedaDeReporte; }
            set { _MonedaDeReporte = value; }
        }
        private void LlenarEnumerativosMonedaDeReporte() {
            MonedaDeReporte.Clear();
            MonedaDeReporte.Add(Saw.Lib.eMonedaParaImpresion.EnMonedaOriginal);
            if (LibDefGen.ProgramInfo.IsCountryVenezuela()) {
                MonedaDeReporte.Add(Saw.Lib.eMonedaParaImpresion.EnBolivares);
            } else if (LibDefGen.ProgramInfo.IsCountryPeru()) {
                MonedaDeReporte.Add(Saw.Lib.eMonedaParaImpresion.EnSoles);
            }
        }

        public Saw.Lib.eTipoDeInforme TipoDeInforme {
            get {
                return _TipoDeInforme;
            }
            set {
                if (_TipoDeInforme != value) {
                    _TipoDeInforme = value;
                    RaisePropertyChanged(TipoDeInformePropertyName);
                }
            }
        }

        public Saw.Lib.eTipoDeInforme[] ArrayTipoDeInforme {
            get {
                return LibEnumHelper<Saw.Lib.eTipoDeInforme>.GetValuesInArray();
            }
        }

        public Saw.Lib.eCantidadAImprimir CantidadAImprimir {
            get {
                return _CantidadAImprimir;
            }
            set {
                if (_CantidadAImprimir != value) {
                    _CantidadAImprimir = value;
                    RaisePropertyChanged(CantidadAImprimirPropertyName);
                    if (_CantidadAImprimir == Saw.Lib.eCantidadAImprimir.Uno) {
                        IsEnabledNombreDelOperador = true;
                        IsVisibleNombreDelOperador = true;
                    } else {
                        IsEnabledNombreDelOperador = false;
                        IsVisibleNombreDelOperador = false;
                    }
                }
            }
        }

        public Saw.Lib.eCantidadAImprimir[] ArrayCantidadAImprimir {
            get {
                return LibEnumHelper<Saw.Lib.eCantidadAImprimir>.GetValuesInArray();
            }
        }

        [LibCustomValidation("NombreDelUsuarioValidating")]
        [LibGridColum("Nombre Cajero", eGridColumType.Connection, IsForSearch = true, ConnectionDisplayMemberPath = "NombreDelUsuario", ConnectionModelPropertyName = "NombreDelUsuario", Header = "Nombre Cajero", ConnectionSearchCommandName = "ChooseNombreDelUsuarioCommand", Width = 255)]
        public string  NombreDelOperador {
            get {
                return _NombreDelOperador;
            }
            set {
                if (_NombreDelOperador != value) {
                    _NombreDelOperador = value;                    
                    RaisePropertyChanged(NombreOperadorPropertyName);
                    if (LibString.IsNullOrEmpty(NombreDelOperador, true)) {
                        ConexionNombreDelOperador = null;
                    }
                }
            }
        }

        public FkGUserViewModel ConexionNombreDelOperador {
            get {
                return _ConexionNombreDelOperador;
            }
            set {
                if (_ConexionNombreDelOperador != value) {
                    _ConexionNombreDelOperador = value;
                    RaisePropertyChanged(NombreOperadorPropertyName);
                }
                if (_ConexionNombreDelOperador != null) {
                    NombreDelOperador = _ConexionNombreDelOperador.UserName;
                }
                if (_ConexionNombreDelOperador == null) {
                    NombreDelOperador = string.Empty;
                }
            }
        }

        public bool IsEnabledNombreDelOperador {
            get {
                return _IsEnabledNombreDelOperador;
            }
            set {
                if (_IsEnabledNombreDelOperador != value) {
                    _IsEnabledNombreDelOperador = value;
                    RaisePropertyChanged(IsEnabledNombreDelOperadorPropertyName);
                    if (_IsEnabledNombreDelOperador == false) {
                        ConexionNombreDelOperador = null;
                    }
                }

            }
        }
        public bool IsVisibleNombreDelOperador {
            get {
                return _IsVisibleNombreDelOperador;
            }
            set {
                if (_IsVisibleNombreDelOperador != value) {
                    _IsVisibleNombreDelOperador = value;
                    RaisePropertyChanged(IsVisibleNombreDelOperadorPropertyName);
                }
            }
        }

        public RelayCommand<string> ChooseNombreDelOperadorCommand {
            get;
            private set;
        } 


        #endregion //Propiedades
        #region Constructores

        public clsCuadreCajaPorUsuarioViewModel() {
            FechaInicial = LibDate.AddsNMonths(LibDate.Today(), -1, false);
            FechaFinal = LibDate.Today();
            LlenarEnumerativosMonedaDeReporte();
            Moneda = Saw.Lib.eMonedaParaImpresion.EnMonedaOriginal;
            TipoDeInforme = Saw.Lib.eTipoDeInforme.Detallado;
            IsEnabledNombreDelOperador = false;
            IsVisibleNombreDelOperador = false;
            NombreDelOperador = string.Empty;
        }
        #endregion //Constructores
        #region Metodos Generados
        protected override ILibBusinessSearch GetBusinessComponent() {
            return new clsCajaNav();
        }

        private ValidationResult FechaInicialValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if (LibDefGen.DateIsGreaterThanDateLimitForEnterData(FechaInicial, false, eAccionSR.InformesPantalla)) {
                vResult = new ValidationResult(LibDefGen.TooltipMessageDateRestrictionDemoProgram("Fecha inicial"));
            } else if(LibDate.F1IsGreaterThanF2(FechaInicial,FechaFinal)) {
                vResult = new ValidationResult("La fecha inicial no puede ser mayor a la fecha final");
            }
            return vResult;
        }
        private ValidationResult FechaFinalValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if (LibDefGen.DateIsGreaterThanDateLimitForEnterData(FechaFinal, false, eAccionSR.InformesPantalla)) {
                vResult = new ValidationResult(LibDefGen.TooltipMessageDateRestrictionDemoProgram("Fecha final"));
            } else if (!LibDate.F1IsGreaterOrEqualThanF2(FechaFinal, FechaInicial)) {
                vResult = new ValidationResult("La fecha final debe ser mayor o igual a la fecha inicial:" + FechaInicial.ToShortDateString());
            }
            return vResult;
        }

        private ValidationResult NombreDelUsuarioValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if (LibString.IsNullOrEmpty(NombreDelOperador) && CantidadAImprimir == Saw.Lib.eCantidadAImprimir.Uno){
                vResult = new ValidationResult("El nombre del cajero no puede estar en blanco");
            }
            return vResult;
        }
        protected override void InitializeCommands(){
            base.InitializeCommands();
            ChooseNombreDelOperadorCommand = new RelayCommand<string>(ExecuteChooseNombreDelOperadorCommand);
        }
        private void ExecuteChooseNombreDelOperadorCommand(string valUserName) {
            try {
                if (valUserName == null) {
                    valUserName = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("UserName", valUserName);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("Status", eStatusUsuario.Activo);
                ConexionNombreDelOperador = null;
                ConexionNombreDelOperador = ChooseRecord<FkGUserViewModel>("Usuario", vDefaultCriteria, vFixedCriteria, string.Empty);
                if (ConexionNombreDelOperador != null) {
                    NombreDelOperador = ConexionNombreDelOperador.UserName;
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, DisplayName);
            }
        }
        #endregion //Metodos Generados


    } //End of class clsCuadreCajaPorUsuarioViewModel

} //End of namespace Galac.Dbo.Uil.Venta

