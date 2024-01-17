using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Report;
using LibGalac.Aos.UI.Mvvm;
using LibGalac.Aos.UI.Mvvm.Command;
using LibGalac.Aos.UI.Mvvm.Helpers;
using Galac.Adm.Ccl.Venta;
using Galac.Adm.Brl.Venta;
using Galac.Adm.Uil.Venta.ViewModel;
using LibGalac.Aos.UI.Mvvm.Validation;
using System.ComponentModel.DataAnnotations;
using LibGalac.Aos.DefGen;
using System.Collections.ObjectModel;

namespace Galac.Adm.Uil.Venta.Reportes {

    public class clsCobranzasEntreFechasViewModel:LibInputRptViewModelBase<Cobranza> {
        #region Constantes		
        public const string FechaDesdePropertyName = "FechaDesde";
        public const string FechaHastaPropertyName = "FechaHasta";
        public const string FiltrarPorPropertyName = "FiltrarPor";
        public const string MonedaDelInformePropertyName = "MonedaDelInforme";
        public const string TipoTasaDeCambioPropertyName = "TipoTasaDeCambio";
        public const string CantidadAImprimirPropertyName = "CantidadAImprimir";
        public const string NombreCobradorPropertyName = "NombreCobrador";
        public const string NombreClientePropertyName = "NombreCliente";
        public const string NombreCuentaBancariaPropertyName = "NombreCuentaBancaria";
        public const string AgruparCamposPropertyName = "AgruparCampos";
        public const string IsVisibleNombreDelCobradorPropertyName = "IsVisibleNombreDelCobrador";
        public const string IsVisibleNombreDelClientePropertyName = "IsVisibleNombreDelCliente";
        public const string IsVisibleNombreCuentaBancariaPropertyName = "IsVisibleNombreCuentaBancaria";         
        public const string IsVisibleCantidadAImprimirPropertyName = "IsVisibleCantidadAImprimir";
        public const string AgruparPorLblPropertyName = "AgruparPorLbl";
        public const string UsaVentasConIvaDiferidosPropertyName = "UsaVentasConIvaDiferidos";


        #endregion Constantes		
        #region Variables
        DateTime _FechaDesde;
        DateTime _FechaHasta;
        eFiltrarCobranzasPor _FiltrarPorAsEnum;
        eCantidadAImprimir _CantidadAImprimirAsEnum;
        Galac.Saw.Lib.eMonedaParaImpresion _MonedaDelInformeAsEnum;
        Saw.Lib.eTasaDeCambioParaImpresion _TipoTasaDeCambioAsEnum;
        string _NombreCobrador;
        string _NombreCliente;
        string _NombreCuentaBancaria;
        bool _AgruparCampos;
        decimal _TasaDeCambio;
        string _AgruparPorLbl;
        bool _UsaVentasConIvaDiferidos;

        private FkVendedorViewModel _ConexionVendedor = null;
        private FkClienteViewModel _ConexionCliente = null;
        private FkCuentaBancariaViewModel _ConexionCuentaBacaria = null;

        #endregion Variables
        #region Propiedades

        public override string DisplayName {
            get { return "Cobranzas Entre Fechas"; }
        }

        
        public override bool IsSSRS {
            get { return false; }
        }

        public RelayCommand<string> ChooseVendedorCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseClienteCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseCuentaBancariaCommand {
            get;
            private set;
        }

        public ObservableCollection<Galac.Saw.Lib.eMonedaParaImpresion> _ListarMoneda =new ObservableCollection<Galac.Saw.Lib.eMonedaParaImpresion>();
        public ObservableCollection<Galac.Saw.Lib.eMonedaParaImpresion> ListarMoneda { get { return _ListarMoneda;} set{_ListarMoneda= value;} }
        
        private void LlenarEnumerativosMonedas() {
            ListarMoneda.Clear();
            if(LibDefGen.ProgramInfo.IsCountryVenezuela()) {
                ListarMoneda.Add(Saw.Lib.eMonedaParaImpresion.EnBolivares);
            } else if(LibDefGen.ProgramInfo.IsCountryPeru()) {
                ListarMoneda.Add(Saw.Lib.eMonedaParaImpresion.EnSoles);
            }
            ListarMoneda.Add(Saw.Lib.eMonedaParaImpresion.EnMonedaOriginal);            
        }       

        [LibCustomValidation("FechaDesdeValidating")]
        public DateTime FechaDesde {
            get {
                return _FechaDesde;
            }
            set {
                if(_FechaDesde != value) {
                    _FechaDesde = value;
                    RaisePropertyChanged(FechaDesdePropertyName);
                }
            }
        }

        [LibCustomValidation("FechaHastaValidating")]
        public DateTime FechaHasta {
            get {
                return _FechaHasta;
            }
            set {
                if(_FechaHasta != value) {
                    _FechaHasta = value;
                    RaisePropertyChanged(FechaHastaPropertyName);
                }
            }
        }

        public FkVendedorViewModel ConexionVendedor {
            get {
                return _ConexionVendedor;
            }
            set {
                if(_ConexionVendedor != value) {
                    _ConexionVendedor = value;
                    RaisePropertyChanged(NombreCobradorPropertyName);
                    if(_ConexionVendedor != null) {
                        NombreCobrador = _ConexionVendedor.Nombre;
                    }
                    if(_ConexionVendedor == null) {
                        NombreCobrador = string.Empty;
                    }
                }
            }
        }

        public FkClienteViewModel ConexionCliente {
            get {
                return _ConexionCliente;
            }
            set {
                if(_ConexionCliente != value) {
                    _ConexionCliente = value;
                    RaisePropertyChanged(NombreClientePropertyName);
                }
                if(_ConexionCliente != null) {
                    NombreCliente = _ConexionCliente.Nombre;
                }
                if(_ConexionCliente == null) {
                    NombreCliente = string.Empty;
                }
            }
        }

        public FkCuentaBancariaViewModel ConexionCuentaBancaria {
            get {
                return _ConexionCuentaBacaria;
            }
            set {
                if(_ConexionCuentaBacaria != value) {
                    _ConexionCuentaBacaria = value;
                    RaisePropertyChanged(NombreCuentaBancariaPropertyName);
                }
                if(_ConexionCuentaBacaria != null) {
                    NombreCuentaBancaria = _ConexionCuentaBacaria.NombreCuenta;
                }
                if(_ConexionCuentaBacaria == null) {
                    NombreCuentaBancaria = string.Empty;
                }
            }
        }

        [LibCustomValidation("NombreCobradorValidating")]
        public string NombreCobrador {
            get {
                return _NombreCobrador;
            }
            set {
                if(_NombreCobrador != value) {
                    _NombreCobrador = value;
                    RaisePropertyChanged(NombreCobradorPropertyName);
                }
            }
        }

        [LibCustomValidation("NombreClienteValidating")]
        public string NombreCliente {
            get {
                return _NombreCliente;
            }
            set {
                if(_NombreCliente != value) {
                    _NombreCliente = value;
                    RaisePropertyChanged(NombreClientePropertyName);
                }
            }
        }

        [LibCustomValidation("NombreCuentaBancariaValidating")]
        public string NombreCuentaBancaria {
            get {
                return _NombreCuentaBancaria;
            }
            set {
                if(_NombreCuentaBancaria != value) {
                    _NombreCuentaBancaria = value;
                    RaisePropertyChanged(NombreCuentaBancariaPropertyName);
                }
            }
        }

        public eFiltrarCobranzasPor FiltrarPor {
            get {
                return _FiltrarPorAsEnum;
            }
            set {
                if(_FiltrarPorAsEnum != value) {
                    _FiltrarPorAsEnum = value;
                    AgruparPorLbl = "Agrupar por \r\n" + _FiltrarPorAsEnum.GetDescription();
                    RaisePropertyChanged(FiltrarPorPropertyName);
                    RaisePropertyChanged(AgruparPorLblPropertyName);
                    RaisePropertyChanged(IsVisibleNombreDelCobradorPropertyName);
                    RaisePropertyChanged(IsVisibleNombreDelClientePropertyName);
                    RaisePropertyChanged(IsVisibleNombreCuentaBancariaPropertyName);
                }
            }
        }

        public Saw.Lib.eMonedaParaImpresion MonedaDelInforme {
            get {
                return _MonedaDelInformeAsEnum;
            }
            set {
                if(_MonedaDelInformeAsEnum != value) {
                    _MonedaDelInformeAsEnum = value;
                    RaisePropertyChanged(MonedaDelInformePropertyName);
                }
            }
        }

        public bool UsaVentasConIvaDiferidos {
            get {
                return _UsaVentasConIvaDiferidos;
            }
            set {
                if(_UsaVentasConIvaDiferidos != value) {
                    _UsaVentasConIvaDiferidos = value;
                    RaisePropertyChanged("UsaVentasConIvaDiferidosPropertyName");
                }
            }

        }        

        public Saw.Lib.eTasaDeCambioParaImpresion TipoTasaDeCambio {
            get {
                return _TipoTasaDeCambioAsEnum;
            }
            set {
                if(_TipoTasaDeCambioAsEnum != value) {
                    _TipoTasaDeCambioAsEnum = value;
                    RaisePropertyChanged(TipoTasaDeCambioPropertyName);
                }
            }
        }

        public eCantidadAImprimir CantidadAImprimir {
            get {
                return _CantidadAImprimirAsEnum;
            }
            set {
                if(_CantidadAImprimirAsEnum != value) {
                    _CantidadAImprimirAsEnum = value;
                    NombreCliente = string.Empty;
                    NombreCobrador = string.Empty;
                    NombreCuentaBancaria = string.Empty;
                    RaisePropertyChanged(CantidadAImprimirPropertyName);
                    RaisePropertyChanged(IsVisibleNombreDelCobradorPropertyName);
                    RaisePropertyChanged(IsVisibleNombreDelClientePropertyName);
                    RaisePropertyChanged(IsVisibleNombreCuentaBancariaPropertyName);
                }
            }
        }

        public bool AgruparCampos {
            get {
                return _AgruparCampos;
            }
            set {
                if(_AgruparCampos != value) {
                    _AgruparCampos = value;
                    RaisePropertyChanged(IsVisibleCantidadAImprimirPropertyName);
                    RaisePropertyChanged(IsVisibleNombreDelCobradorPropertyName);
                    RaisePropertyChanged(IsVisibleNombreDelClientePropertyName);
                    RaisePropertyChanged(IsVisibleNombreCuentaBancariaPropertyName);
                }
                if(!_AgruparCampos) {
                    CantidadAImprimir = eCantidadAImprimir.All;
                    NombreCuentaBancaria = string.Empty;
                    NombreCobrador = string.Empty;
                    NombreCliente = string.Empty;
                }
            }
        }

        public decimal TasaDeCambio {
            get {
                return _TasaDeCambio;
            }
            set {
                if(_TasaDeCambio != value) {
                    _TasaDeCambio = value;
                }
            }
        }

        public string AgruparPorLbl {
            get {
                return _AgruparPorLbl;
            }
            private set {
                if(_AgruparPorLbl != value) {
                    _AgruparPorLbl = value;
                    RaisePropertyChanged(AgruparPorLblPropertyName);
                }
            }
        }

        public eFiltrarCobranzasPor[] ArrayFiltarCobranzaPor {
            get {
                return LibEnumHelper<eFiltrarCobranzasPor>.GetValuesInArray();
            }
        }       

        public Saw.Lib.eTasaDeCambioParaImpresion[] ArrayTipoTasaDeCambio {
            get {
                return LibEnumHelper<Saw.Lib.eTasaDeCambioParaImpresion>.GetValuesInArray();
            }
        }

        public eCantidadAImprimir[] ArrayCantidadAImprimir {
            get {
                return LibEnumHelper<eCantidadAImprimir>.GetValuesInArray();
            }
        }
        
        #endregion //Propiedades
        #region Constructores

        public clsCobranzasEntreFechasViewModel() {
            FechaDesde = LibDate.Today();
            FechaHasta = LibDate.Today();
            CantidadAImprimir = eCantidadAImprimir.All;
            FiltrarPor = eFiltrarCobranzasPor.Cobrador;
            TasaDeCambio = 1;
            TipoTasaDeCambio = Saw.Lib.eTasaDeCambioParaImpresion.DelDia;
            MonedaDelInforme = Saw.Lib.eMonedaParaImpresion.EnMonedaOriginal;
            AgruparPorLbl = "Agrupar por \r\n" + _FiltrarPorAsEnum.GetDescription();
            LlenarEnumerativosMonedas();
        }

        protected override void InitializeCommands() {
            base.InitializeCommands();
            ChooseVendedorCommand = new RelayCommand<string>(ExecuteChooseVendedorCommand);
            ChooseClienteCommand = new RelayCommand<string>(ExecuteChooseClienteCommand);
            ChooseCuentaBancariaCommand = new RelayCommand<string>(ExecuteChooseCuentaBancariaCommand);
        }

        protected override ILibBusinessSearch GetBusinessComponent() {
            return new clsCobranzaNav();
        }		

        private void ExecuteChooseVendedorCommand(string valNombre) {
            try {
                if(valNombre == null) {
                    valNombre = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Nombre",valNombre);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("ConsecutivoCompania",LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"));
                ConexionVendedor = null;
                ConexionVendedor = ChooseRecord<FkVendedorViewModel>("Vendedor",vDefaultCriteria,vFixedCriteria,string.Empty);
                if(ConexionVendedor != null) {
                    NombreCobrador = ConexionVendedor.Nombre;                 
                }
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx,DisplayName);
            }
        }

        private void ExecuteChooseClienteCommand(string valNombre) {
            try {
                if(valNombre == null) {
                    valNombre = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Nombre",valNombre);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("ConsecutivoCompania",LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"));
                ConexionCliente = null;
                ConexionCliente = ChooseRecord<FkClienteViewModel>("Cliente",vDefaultCriteria,vFixedCriteria,string.Empty);
                if(ConexionCliente != null) {
                    NombreCliente = ConexionCliente.Nombre;
                }
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx,DisplayName);
            }
        }

        private void ExecuteChooseCuentaBancariaCommand(string valNombre) {
            try {
                if(valNombre == null) {
                    valNombre = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("NombreCuenta",valNombre);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("ConsecutivoCompania",LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"));
                ConexionCuentaBancaria = null;
                ConexionCuentaBancaria = ChooseRecord<FkCuentaBancariaViewModel>("CuentaBancaria",vDefaultCriteria,vFixedCriteria,string.Empty);
                if(ConexionCuentaBancaria != null) {
                    NombreCuentaBancaria = ConexionCuentaBancaria.NombreCuenta;
                }
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx,DisplayName);
            }
        }


        #endregion //Constructores
        #region Metodos Generados

        private ValidationResult FechaDesdeValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if(LibDefGen.DateIsGreaterThanDateLimitForEnterData(FechaDesde,false,eAccionSR.InformesPantalla)) {
                vResult = new ValidationResult(LibDefGen.TooltipMessageDateRestrictionDemoProgram("Fecha Desde"));
            } else if(LibDate.F1IsGreaterThanF2(FechaDesde,FechaHasta)) {
                vResult = new ValidationResult("La fecha desde no puede ser mayor a la fecha hasta");
            }
            return vResult;
        }

        private ValidationResult FechaHastaValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if(LibDefGen.DateIsGreaterThanDateLimitForEnterData(FechaHasta,false,eAccionSR.InformesPantalla)) {
                vResult = new ValidationResult(LibDefGen.TooltipMessageDateRestrictionDemoProgram("Fecha Desde"));
            } else if(LibDate.F1IsLessThanF2(FechaHasta,FechaDesde)) {
                vResult = new ValidationResult("La fecha hasta no puede ser menor a la fecha desde");
            }
            return vResult;
        }

        private ValidationResult NombreCobradorValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if(LibString.IsNullOrEmpty(NombreCobrador) && CantidadAImprimir==eCantidadAImprimir.One && FiltrarPor == eFiltrarCobranzasPor.Cobrador) {
                vResult = new ValidationResult("El nombre del cobrador no puede estar en blanco");
            }
            return vResult;
        }

        private ValidationResult NombreClienteValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if(LibString.IsNullOrEmpty(NombreCliente) && CantidadAImprimir == eCantidadAImprimir.One && FiltrarPor == eFiltrarCobranzasPor.Cliente) {
                vResult = new ValidationResult("El nombre del cliente no puede estar en blanco");
            }
            return vResult;
        }

        private ValidationResult NombreCuentaBancariaValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if(LibString.IsNullOrEmpty(NombreCuentaBancaria) && CantidadAImprimir == eCantidadAImprimir.One && FiltrarPor == eFiltrarCobranzasPor.CuentaBancaria) {
                vResult = new ValidationResult("El nombre de la cuenta bancaria no puede estar en blanco");
            }
            return vResult;
        }      

        public bool IsVisibleCantidadAImprimir {
            get { return AgruparCampos; }
        }

        public bool IsVisibleNombreDelCobrador {            
            get { return AgruparCampos && FiltrarPor == eFiltrarCobranzasPor.Cobrador && CantidadAImprimir == eCantidadAImprimir.One; }           
        }

        public bool IsVisibleNombreDelCliente {
            get{ return AgruparCampos && FiltrarPor == eFiltrarCobranzasPor.Cliente && CantidadAImprimir == eCantidadAImprimir.One; }            
        }

        public bool IsVisibleNombreCuentaBancaria {
            get { return AgruparCampos &&  FiltrarPor == eFiltrarCobranzasPor.CuentaBancaria && CantidadAImprimir == eCantidadAImprimir.One; }
        }

        public bool IsVisibleUsaVentasConIvaDiferidos {
            get {
                return LibConvert.SNToBool(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros","UsaVentasConIvaDiferidos"));
            }
        }
        #endregion //Metodos Generados       

    } //End of class clsCobranzasEntreFechasViewModel

} //End of namespace Galac.Dbo.Uil.Venta

