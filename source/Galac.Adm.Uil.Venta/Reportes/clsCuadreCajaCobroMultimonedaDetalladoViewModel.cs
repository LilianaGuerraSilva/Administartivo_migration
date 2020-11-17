using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Report;
//using LibGalac.Aos.UI.Cib;
using LibGalac.Aos.DefGen;
using LibGalac.Aos.UI.Mvvm;
using LibGalac.Aos.UI.Mvvm.Command;
using LibGalac.Aos.UI.Mvvm.Helpers;
using LibGalac.Aos.UI.Mvvm.Validation;
using Galac.Adm.Ccl.Venta;
using Galac.Adm.Brl.Venta;
using Galac.Adm.Uil.Venta.ViewModel;
using LibGalac.Aos.Ccl.Usal;
using System.Collections.ObjectModel;

namespace Galac.Adm.Uil.Venta.Reportes {

    public class clsCuadreCajaCobroMultimonedaDetalladoViewModel:LibInputRptViewModelBase<Caja> {
        #region Constantes

        private const string FechaInicialPropertyName = "FechaInicial";
        private const string FechaFinalPropertyName = "FechaFinal";
        private const string MonedaPropertyName = "Moneda";
        private const string CantidadAImprimirPropertyName = "CantidadAImprimir";
        private const string IsEnabledNombreDelUsuarioPropertyName = "IsEnabledNombreDelUsuario";
        private const string IsVisibleNombreDelUsuarioPropertyName = "IsVisibleNombreDelUsuario";
        private const string NombreDelUsuarioPropertyName = "NombreDelUsuario";
        private const string TotalesTipoCobroPropertyName = "TotalesTipoCobro";

        #endregion
       
        #region Variables
        private DateTime _FechaInicial;
        private DateTime _FechaFinal;
        private Galac.Saw.Lib.eMonedaParaImpresion _Moneda;
        private Galac.Saw.Lib.eCantidadAImprimir _CantidadAImprimir;
        private bool _IsEnabledNombreDelUsuario;
        private bool _IsVisibleNombreDelUsuario;
        private FkGUserViewModel _ConexionNombreDelUsuario = null;
        private string _NombreDelUsuario;
        private bool _TotalesTipoCobro;
        
        #endregion //Variables

        #region Propiedades
        public override string DisplayName {
            get { return "Cuadre de Caja con Cobro Multimoneda"; }
        }

        public LibXmlMemInfo AppMemoryInfo { get; set; }

        public LibXmlMFC Mfc { get; set; }

        public override bool IsSSRS {
            get {
                return false;
            }
        }

        [LibCustomValidation("FechaInicialValidating")]
        [LibGridColum("Fecha Inicial", eGridColumType.DatePicker)]
        public DateTime FechaInicial {
            get {
                return _FechaInicial;
            }
            set {
                if (_FechaInicial != value)
                {
                    _FechaInicial = value;
                    RaisePropertyChanged(FechaInicialPropertyName);
                }
            }
        }

        [LibCustomValidation("FechaFinalValidating")]
        [LibGridColum("Fecha Final", eGridColumType.DatePicker)]
        public DateTime FechaFinal {
            get {
                return _FechaFinal;
            }
            set {
                if (_FechaFinal != value)
                {
                    _FechaFinal = value;
                    RaisePropertyChanged(FechaFinalPropertyName);
                }
            }
        }

        public Galac.Saw.Lib.eMonedaParaImpresion Moneda {
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
        public Galac.Saw.Lib.eCantidadAImprimir CantidadAImprimir {
            get {
                return _CantidadAImprimir;
            }
            set {
                if (_CantidadAImprimir != value) {
                    _CantidadAImprimir = value;
                    RaisePropertyChanged(CantidadAImprimirPropertyName);
                    if (_CantidadAImprimir == Saw.Lib.eCantidadAImprimir.Uno) {
                        IsEnabledNombreDelUsuario = true;
                        IsVisibleNombreDelUsuario = true;
                    } else {
                        IsVisibleNombreDelUsuario = false;
                        IsEnabledNombreDelUsuario = false;
                    }
                }
            }
        }
        public Galac.Saw.Lib.eCantidadAImprimir[] ArrayCantidadOperadorDeReporte {
            get {
                return LibEnumHelper<Galac.Saw.Lib.eCantidadAImprimir>.GetValuesInArray();
            }
        }
        public bool IsEnabledNombreDelUsuario {
            get {
                return _IsEnabledNombreDelUsuario;
            }
            set {
                if (_IsEnabledNombreDelUsuario != value) {
                    _IsEnabledNombreDelUsuario = value;
                    RaisePropertyChanged(IsEnabledNombreDelUsuarioPropertyName);
                    if (_IsEnabledNombreDelUsuario == false) {
                        ConexionNombreDelUsuario = null;
                    }
                }

            }
        }
        public bool IsVisibleNombreDelUsuario {
            get {
                return _IsVisibleNombreDelUsuario;
            }
            set {
                if (_IsVisibleNombreDelUsuario != value) {
                    _IsVisibleNombreDelUsuario = value;
                    RaisePropertyChanged(IsVisibleNombreDelUsuarioPropertyName);
                }
            }
        }

        public bool TotalesTipoCobro {
            get {
                return _TotalesTipoCobro;
            }
            set {
                if (_TotalesTipoCobro != value) {
                    _TotalesTipoCobro = value;
                    RaisePropertyChanged(TotalesTipoCobroPropertyName);
                }
            }
        }
        [LibCustomValidation("NombreDelUsuarioValidating")]
        [LibGridColum("Nombre Cajero", eGridColumType.Connection, IsForSearch = true, ConnectionDisplayMemberPath = "NombreDelUsuario", ConnectionModelPropertyName = "NombreDelUsuario", Header = "Nombre Cajero", ConnectionSearchCommandName = "ChooseNombreDelUsuarioCommand", Width = 255)]
        public string NombreDelUsuario {
            get {
                return _NombreDelUsuario;
            }
            set {
                if (_NombreDelUsuario != value) {
                    _NombreDelUsuario = value;
                    RaisePropertyChanged(NombreDelUsuarioPropertyName);
                    if (LibString.IsNullOrEmpty(NombreDelUsuario, true)) {
                        ConexionNombreDelUsuario = null;
                    }
                }
            }
        }

        public FkGUserViewModel ConexionNombreDelUsuario {
            get {
                return _ConexionNombreDelUsuario;
            }
            set {
                if (_ConexionNombreDelUsuario != value) {
                    _ConexionNombreDelUsuario = value;
                    RaisePropertyChanged(NombreDelUsuarioPropertyName);
                }
                if (_ConexionNombreDelUsuario != null) {
                    NombreDelUsuario = _ConexionNombreDelUsuario.UserName;
                }
                if (_ConexionNombreDelUsuario == null) {
                    NombreDelUsuario = string.Empty;
                }
            }
        }
        public RelayCommand<string> ChooseNombreDelUsuarioCommand {
            get;
            private set;
        }

        public ObservableCollection<Galac.Saw.Lib.eMonedaParaImpresion> _MonedaDeReporte = new ObservableCollection<Galac.Saw.Lib.eMonedaParaImpresion>();
        public ObservableCollection<Galac.Saw.Lib.eMonedaParaImpresion> MonedaDeReporte { get { return _MonedaDeReporte; } set { _MonedaDeReporte = value; } }

        private void LlenarEnumerativosMonedaDeReporte () {
            MonedaDeReporte.Clear();
            MonedaDeReporte.Add(Saw.Lib.eMonedaParaImpresion.EnMonedaOriginal);
            if (LibDefGen.ProgramInfo.IsCountryVenezuela()) {
                MonedaDeReporte.Add(Saw.Lib.eMonedaParaImpresion.EnBolivares);
            } else if (LibDefGen.ProgramInfo.IsCountryPeru()) {
                MonedaDeReporte.Add(Saw.Lib.eMonedaParaImpresion.EnSoles);
            }
        }
        #endregion //Propiedades
        #region Constructores

        public clsCuadreCajaCobroMultimonedaDetalladoViewModel() {
            FechaInicial = LibDate.Today();
            FechaFinal = LibDate.Today();            
            LlenarEnumerativosMonedaDeReporte();
            Moneda = Galac.Saw.Lib.eMonedaParaImpresion.EnMonedaOriginal;
            NombreDelUsuario = string.Empty;
            CantidadAImprimir = Galac.Saw.Lib.eCantidadAImprimir.Todos;
            IsVisibleNombreDelUsuario = false;
            IsEnabledNombreDelUsuario = false;
            TotalesTipoCobro = true;
        }

        #endregion //Constructores
        #region Metodos Generados
        protected override ILibBusinessSearch GetBusinessComponent()
        {
            return new clsCajaNav();
        }
       
        private ValidationResult FechaInicialValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if (LibDefGen.DateIsGreaterThanDateLimitForEnterData(FechaFinal, false, eAccionSR.Imprimir)) {
                vResult = new ValidationResult(LibDefGen.TooltipMessageDateRestrictionDemoProgram("Fecha inicial"));
            }
            return vResult;
        }
        private ValidationResult FechaFinalValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if (!LibDate.F1IsGreaterOrEqualThanF2(FechaFinal, FechaInicial))
            {
                vResult = new ValidationResult("La fecha final debe ser mayor o igual a la fecha inicial:" + FechaInicial.ToShortDateString());
            }
            return vResult;
        }
        #endregion //Metodos Generados

        protected override void InitializeCommands(){
            base.InitializeCommands();
            ChooseNombreDelUsuarioCommand = new RelayCommand<string>(ExecuteChooseNombreDelUsuarioCommand);
        }
        private void ExecuteChooseNombreDelUsuarioCommand(string valUserName) {
            try {
                if (valUserName == null) {
                    valUserName = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("UserName", valUserName);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("Status", eStatusUsuario.Activo);
                ConexionNombreDelUsuario = null;
                ConexionNombreDelUsuario = ChooseRecord<FkGUserViewModel>("Usuario", vDefaultCriteria, vFixedCriteria, string.Empty);
                if (ConexionNombreDelUsuario != null) {
                    NombreDelUsuario = ConexionNombreDelUsuario.UserName;
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, DisplayName);
            }
        }
        private ValidationResult NombreDelUsuarioValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if (LibString.IsNullOrEmpty(NombreDelUsuario) && CantidadAImprimir == Saw.Lib.eCantidadAImprimir.Uno){
                vResult = new ValidationResult("El nombre del cajero no puede estar en blanco");
            }
            return vResult;
        }




    } //End of class clsCuadreCajaCobroMultimonedaDetalladoViewModel

} //End of namespace Galac.Adm.Uil.Venta

