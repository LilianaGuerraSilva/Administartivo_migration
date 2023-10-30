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
using LibGalac.Aos.Ccl.Usal;
using Galac.Adm.Brl.DispositivosExternos.ImpresoraFiscal;
using Galac.Adm.Ccl.DispositivosExternos;
using LibGalac.Aos.Cnf;
using System.Collections;
using Galac.Comun.Ccl.TablasGen;
using Galac.Comun.Brl.TablasGen;
using Galac.Comun.Uil.TablasGen.ViewModel;
using System.Windows;
using Galac.Adm.Ccl.CajaChica;
using Galac.Saw.Lib;

namespace Galac.Adm.Uil.Venta.ViewModel {
    public class CajaAperturaViewModel: LibInputViewModel<CajaApertura> {

        #region Constantes y Variables
        const string NombreCajaPropertyName = "NombreCaja";
        const string NombreDelUsuarioPropertyName = "NombreDelUsuario";
        const string MontoAperturaPropertyName = "MontoApertura";
        const string MontoCierrePropertyName = "MontoCierre";
        const string MontoEfectivoPropertyName = "MontoEfectivo";
        const string MontoTarjetaPropertyName = "MontoTarjeta";
        const string MontoChequePropertyName = "MontoCheque";
        const string MontoDepositoPropertyName = "MontoDeposito";
        const string MontoAnticipoPropertyName = "MontoAnticipo";
        const string MontoVueltoPropertyName = "MontoVuelto";
        const string MontoVueltoPMPropertyName = "MontoVueltoPM";
        const string FechaPropertyName = "Fecha";
        const string HoraAperturaPropertyName = "HoraApertura";
        const string HoraCierrePropertyName = "HoraCierre";
        const string CajaCerradaPropertyName = "CajaCerrada";
        const string CodigoMonedaPropertyName = "CodigoMoneda";
        const string MonedaPropertyName = "Moneda";
        const string CambioPropertyName = "Cambio";
        const string MontoAperturaMEPropertyName = "MontoAperturaME";
        const string MontoCierreMEPropertyName = "MontoCierreME";
        const string MontoEfectivoMEPropertyName = "MontoEfectivoME";
        const string MontoTarjetaMEPropertyName = "MontoTarjetaME";
        const string MontoChequeMEPropertyName = "MontoChequeME";
        const string MontoDepositoMEPropertyName = "MontoDepositoME";
        const string MontoAnticipoMEPropertyName = "MontoAnticipoME";
        const string MontoVueltoMEPropertyName = "MontoVueltoME";
        const string NombreOperadorPropertyName = "NombreOperador";
        const string FechaUltimaModificacionPropertyName = "FechaUltimaModificacion";
        private FkCajaViewModel _ConexionNombreCaja = null;
        private FkGUserViewModel _ConexionNombreDelUsuario = null;
        private FkMonedaViewModel _ConexionMoneda = null;
        ICajaAperturaPdn insCajaApertura;
        bool _CajaCerrada = false;
        bool _UsuarioFueAsignado = false;
        private Saw.Lib.clsNoComunSaw vMonedaLocal = null;
        private bool _UsaCobroMultimoneda = false;
        private string _CodigoMEInicial;
        #endregion //Constantes y Variables

        #region Propiedades
        public override string ModuleName {
            get { return "Caja Registradora"; }
        }

        public bool ShowDetalle { get; private set; }
        public bool ShowTitulos { get; private set; }
        public bool ShowApertura { get; private set; }
        public bool ShowStatusCajaCerrada { get; private set; }
        public bool ShowStatusCajaAbierta { get; private set; }

        public RelayCommand AbrirCajaCommand {
            get;
            private set;
        }

        public RelayCommand CerrarCajaCommand {
            get;
            private set;
        }

        public RelayCommand CloseCommand {
            get;
            private set;
        }

        public RelayCommand AsignarCajaCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseNombreCajaCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseNombreDelUsuarioCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseMonedaCommand {
            get;
            private set;
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

        public int ConsecutivoCaja {
            get {
                return Model.ConsecutivoCaja;
            }
            set {
                if (Model.ConsecutivoCaja != value) {
                    Model.ConsecutivoCaja = value;
                }
            }
        }

        [LibRequired(ErrorMessage = "El campo Nombre Caja es requerido.")]
        [LibGridColum("Nombre Caja", eGridColumType.Connection, ConnectionSearchCommandName = "ChooseNombreCajaCommand", IsForSearch = true, IsForList = true, ColumnOrder = 0, Width = 190)]
        public string NombreCaja {
            get {
                return Model.NombreCaja;
            }
            set {
                if (Model.NombreCaja != value) {
                    Model.NombreCaja = value;
                    RaisePropertyChanged(NombreCajaPropertyName);
                    if (LibString.IsNullOrEmpty(NombreCaja, true)) {
                        ConexionNombreCaja = null;
                    }
                }
            }
        }

        [LibRequired(ErrorMessage = "El campo Operador es requerido.")]
        [LibGridColum("Nombre del Usuario", eGridColumType.Connection, ConnectionSearchCommandName = "ChooseNombreDelUsuarioCommand", IsForSearch = true, IsForList = true, ColumnOrder = 1, Width = 190)]
        public string NombreDelUsuario {
            get {
                return Model.NombreDelUsuario;
            }
            set {
                if (Model.NombreDelUsuario != value) {
                    Model.NombreDelUsuario = value;
                    RaisePropertyChanged(NombreDelUsuarioPropertyName);
                    if (LibString.IsNullOrEmpty(NombreDelUsuario, true)) {
                        ConexionNombreDelUsuario = null;
                    }
                }
            }
        }

        [LibGridColum("MontoApertura", eGridColumType.Numeric, IsForSearch = false, IsForList = true, Header = "Monto de Apertura", ConditionalPropertyDecimalDigits = "DecimalDigits", Alignment = eTextAlignment.Right, ColumnOrder = 2, Width = 120)]
        public decimal MontoApertura {
            get {
                return Model.MontoApertura;
            }
            set {
                if (Model.MontoApertura != value) {
                    Model.MontoApertura = value;
                    RaisePropertyChanged(MontoAperturaPropertyName);
                }
            }
        }

        [LibGridColum("MontoCierre", eGridColumType.Numeric, IsForSearch = false, IsForList = true, Header = "Monto de Cierre", ConditionalPropertyDecimalDigits = "DecimalDigits", Alignment = eTextAlignment.Right, ColumnOrder = 3, Width = 120)]
        public decimal MontoCierre {
            get {
                return Model.MontoCierre;
            }
            set {
                if (Model.MontoCierre != value) {
                    Model.MontoCierre = value;
                    RaisePropertyChanged(MontoCierrePropertyName);
                }
            }
        }

        public decimal MontoEfectivo {
            get {
                return Model.MontoEfectivo;
            }
            set {
                if (Model.MontoEfectivo != value) {
                    Model.MontoEfectivo = value;
                    RaisePropertyChanged(MontoEfectivoPropertyName);
                }
            }
        }

        public decimal MontoTarjeta {
            get {
                return Model.MontoTarjeta;
            }
            set {
                if (Model.MontoTarjeta != value) {
                    Model.MontoTarjeta = value;
                    RaisePropertyChanged(MontoTarjetaPropertyName);
                }
            }
        }

        public decimal MontoCheque {
            get {
                return Model.MontoCheque;
            }
            set {
                if (Model.MontoCheque != value) {
                    Model.MontoCheque = value;
                    RaisePropertyChanged(MontoChequePropertyName);
                }
            }
        }

        public decimal MontoDeposito {
            get {
                return Model.MontoDeposito;
            }
            set {
                if (Model.MontoDeposito != value) {
                    Model.MontoDeposito = value;
                    RaisePropertyChanged(MontoDepositoPropertyName);
                }
            }
        }

        public decimal MontoAnticipo {
            get {
                return Model.MontoAnticipo;
            }
            set {
                if (Model.MontoAnticipo != value) {
                    Model.MontoAnticipo = value;
                    RaisePropertyChanged(MontoAnticipoPropertyName);
                }
            }
        }

        public decimal  MontoVuelto {
            get {
                return Model.MontoVuelto;
            }
            set {
                if (Model.MontoVuelto != value) {
                    Model.MontoVuelto = value;
                    RaisePropertyChanged(MontoVueltoPropertyName);
                }
            }
        }
        public decimal MontoVueltoPM {
            get {
                return Model.MontoVueltoPM;
            }
            set {
                if (Model.MontoVueltoPM != value) {
                    Model.MontoVueltoPM = value;
                    RaisePropertyChanged(MontoVueltoPMPropertyName);
                }
            }
        }

        [LibCustomValidation("FechaValidating")]
        public DateTime  Fecha {
            get {
                return Model.Fecha;
            }
            set {
                if (Model.Fecha != value) {
                    Model.Fecha = value;
                    RaisePropertyChanged(FechaPropertyName);
                }
            }
        }

        public string HoraApertura {
            get {
                return Model.HoraApertura;
            }
            set {
                if (Model.HoraApertura != value) {
                    Model.HoraApertura = value;
                    RaisePropertyChanged(HoraAperturaPropertyName);
                }
            }
        }

        [LibGridColum("HoraAperturaDt", eGridColumType.Generic, Header = "Hora de Apertura", IsForList = true, IsForSearch = false, Alignment = eTextAlignment.Center, Width = 110, ColumnOrder = 9)]
        public string HoraAperturaDt {
            get {
                return LibConvert.ToStrOnlyForHour(LibConvert.ToDate(HoraApertura), "hh:mm tt");
            }
        }

        public string HoraCierre {
            get {
                return Model.HoraCierre;
            }
            set {
                if (Model.HoraCierre != value) {
                    Model.HoraCierre = value;
                    RaisePropertyChanged(HoraCierrePropertyName);
                }
            }
        }

        [LibGridColum("HoraCierreDt", eGridColumType.Generic, Header = "Hora de Cierre", IsForList = true, IsForSearch = false, Alignment = eTextAlignment.Center, Width = 110, ColumnOrder = 10)]
        public string HoraCierreDt {
            get {
                return LibConvert.ToStrOnlyForHour(LibConvert.ToDate(HoraCierre), "hh:mm tt");
            }
        }

        public bool CajaCerrada {
            get {
                return Model.CajaCerradaAsBool;
            }
            set {
                if (Model.CajaCerradaAsBool != value) {
                    Model.CajaCerradaAsBool = value;
                    RaisePropertyChanged(CajaCerradaPropertyName);
                }
            }
        }

        [LibGridColum("CajaCerradaStr", eGridColumType.Generic, Header = "Caja Abierta", IsForList = true, IsForSearch = false, Alignment = eTextAlignment.Center, Width = 100, ColumnOrder = 6)]
        public string CajaCerradaStr {
            get {
                return Model.CajaCerradaAsBool ? "" : "\u2713";
            }
        }

        public string CodigoMoneda {
            get {
                return Model.CodigoMoneda;
            }
            set {
                if (Model.CodigoMoneda != value) {
                    Model.CodigoMoneda = value;
                    IsDirty = true;
                    RaisePropertyChanged(CodigoMonedaPropertyName);
                }
            }
        }

        [LibCustomValidation("CodigoMonedaValidating")]
        public string Moneda {
            get {
                return Model.Moneda;
            }
            set {
                if (Model.Moneda != value) {
                    Model.Moneda = value;
                    IsDirty = true;
                    RaisePropertyChanged(MonedaPropertyName);
                    if (LibString.IsNullOrEmpty(Moneda, true)) {
                        ConexionMoneda = null;
                    }
                } else if (!LibString.IsNullOrEmpty(Model.Moneda)) {
                    RaisePropertyChanged(MonedaPropertyName);
                }
            }
        }

        [LibCustomValidation("TasaDeCambioValidating")]
        public decimal Cambio {
            get {
                return Model.Cambio;
            }
            set {
                if (Model.Cambio != value) {
                    Model.Cambio = value;
                    IsDirty = true;
                    RaisePropertyChanged(CambioPropertyName);
                }
            }
        }

        [LibGridColum("MontoAperturaME", eGridColumType.Numeric, IsForSearch = false, IsForList = true, Header = "Monto de Apertura en Divisas", ConditionalPropertyDecimalDigits = "DecimalDigits", Alignment = eTextAlignment.Right, ColumnOrder = 4, Width = 180)]
        public decimal MontoAperturaME {
            get {
                return Model.MontoAperturaME;
            }
            set {
                if (Model.MontoAperturaME != value) {
                    Model.MontoAperturaME = value;
                    IsDirty = true;
                    RaisePropertyChanged(MontoAperturaMEPropertyName);
                }
            }
        }

        [LibGridColum("MontCierreME", eGridColumType.Numeric, IsForSearch = false, IsForList = true, Header = "Monto de Cierre en Divisas", ConditionalPropertyDecimalDigits = "DecimalDigits", Alignment = eTextAlignment.Right, ColumnOrder = 5, Width = 180)]
        public decimal MontoCierreME {
            get {
                return Model.MontoCierreME;
            }
            set {
                if (Model.MontoCierreME != value) {
                    Model.MontoCierreME = value;
                    IsDirty = true;
                    RaisePropertyChanged(MontoCierreMEPropertyName);
                }
            }
        }

        public decimal MontoEfectivoME {
            get {
                return Model.MontoEfectivoME;
            }
            set {
                if (Model.MontoEfectivoME != value) {
                    Model.MontoEfectivoME = value;
                    IsDirty = true;
                    RaisePropertyChanged(MontoEfectivoMEPropertyName);
                }
            }
        }

        public decimal MontoTarjetaME {
            get {
                return Model.MontoTarjetaME;
            }
            set {
                if (Model.MontoTarjetaME != value) {
                    Model.MontoTarjetaME = value;
                    IsDirty = true;
                    RaisePropertyChanged(MontoTarjetaMEPropertyName);
                }
            }
        }

        public decimal MontoChequeME {
            get {
                return Model.MontoChequeME;
            }
            set {
                if (Model.MontoChequeME != value) {
                    Model.MontoChequeME = value;
                    IsDirty = true;
                    RaisePropertyChanged(MontoChequeMEPropertyName);
                }
            }
        }

        public decimal MontoDepositoME {
            get {
                return Model.MontoDepositoME;
            }
            set {
                if (Model.MontoDepositoME != value) {
                    Model.MontoDepositoME = value;
                    IsDirty = true;
                    RaisePropertyChanged(MontoDepositoMEPropertyName);
                }
            }
        }

        public decimal MontoAnticipoME {
            get {
                return Model.MontoAnticipoME;
            }
            set {
                if (Model.MontoAnticipoME != value) {
                    Model.MontoAnticipoME = value;
                    IsDirty = true;
                    RaisePropertyChanged(MontoAnticipoMEPropertyName);
                }
            }
        }

        public decimal  MontoVueltoME {
            get {
                return Model.MontoVueltoME;
            }
            set {
                if (Model.MontoVueltoME != value) {
                    Model.MontoVueltoME = value;
                    RaisePropertyChanged(MontoVueltoMEPropertyName);
                }
            }
        }

        public string NombreOperador {
            get {
                return Model.NombreOperador;
            }
            set {
                if (Model.NombreOperador != value) {
                    Model.NombreOperador = value;
                    RaisePropertyChanged(NombreOperadorPropertyName);
                }
            }
        }

        [LibCustomValidation("FechaValidating")]
        [LibGridColum("Fecha Ult Modificación", eGridColumType.DatePicker, IsForList = true, IsForSearch = false, Width = 150, ColumnOrder = 8)]
        public DateTime FechaUltimaModificacion {
            get {
                return Model.FechaUltimaModificacion;
            }
            set {
                if (Model.FechaUltimaModificacion != value) {
                    Model.FechaUltimaModificacion = value;
                    RaisePropertyChanged(FechaUltimaModificacionPropertyName);
                }
            }
        }

        [LibGridColum("CajaAsignada", Header = "Caja asignada a estación", IsForList = true, IsForSearch = false, Alignment = eTextAlignment.Center, Width = 150, ColumnOrder = 7)]
        public string CajaAsignada {
            get {
                return ObtenerCajaAsignada();
            }
        }

        private string ObtenerCajaAsignada() {
            string vCajaLocal = "";
            vCajaLocal = LibAppSettings.ReadAppSettingsKey("CAJALOCAL");
            return LibConvert.ToStr(Model.ConsecutivoCaja) == vCajaLocal ? "\u2713" : "";
        }

        public FkCajaViewModel ConexionNombreCaja {
            get {
                return _ConexionNombreCaja;
            }
            set {
                if (_ConexionNombreCaja != value) {
                    _ConexionNombreCaja = value;
                    RaisePropertyChanged(NombreCajaPropertyName);
                }
                if (_ConexionNombreCaja == null) {
                    NombreCaja = string.Empty;
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
                if (_ConexionNombreDelUsuario == null) {
                    NombreDelUsuario = string.Empty;
                }
            }
        }

        public FkMonedaViewModel ConexionMoneda {
            get {
                return _ConexionMoneda;
            }
            set {
                if (_ConexionMoneda != value) {
                    _ConexionMoneda = value;
                }
                if (_ConexionMoneda == null) {
                    CodigoMoneda = string.Empty;
                }
            }
        }
        #endregion //Propiedades

        #region Constructores e Inicializadores
        public CajaAperturaViewModel()
            : this(new CajaApertura(), eAccionSR.Insertar) {
        }

        public CajaAperturaViewModel(CajaApertura initModel, eAccionSR initAction)
            : base(initModel, initAction) {
            DefaultFocusedPropertyName = NombreCajaPropertyName;
            Model.ConsecutivoCompania = LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania");
            if (insCajaApertura == null) {
                insCajaApertura = new Brl.Venta.clsCajaAperturaNav() as ICajaAperturaPdn;
            }
        }

        public override void InitializeViewModel(eAccionSR valAction) {
            base.InitializeViewModel(valAction);
            InitializeRibbon();
            clsLibSaw insLibSaw = new clsLibSaw();
            if (Action == eAccionSR.Insertar) {
                HoraApertura = insLibSaw.ConvertHourToLongFormat(LibDate.CurrentHourAsStr);
                HoraCierre = "";
                MontoCierre = 0m;
                MontoCheque = 0m;
                MontoDeposito = 0m;
                MontoEfectivo = 0m;
                MontoTarjeta = 0m;
                MontoAnticipo = 0m;
                MontoCierreME = 0m;
                MontoChequeME = 0m;
                MontoDepositoME = 0m;
                MontoEfectivoME = 0m;
                MontoTarjetaME = 0m;
                MontoAnticipoME = 0m;
            } else if (Action == eAccionSR.Modificar) {              
                TotalesPorCierreDeCaja();
                HoraCierre = insLibSaw.ConvertHourToLongFormat(LibDate.CurrentHourAsStr);
            }
            ShowDetalle = CajaCerrada || Action == eAccionSR.Modificar;
            ShowApertura = Action != eAccionSR.Escoger || CajaCerrada;
            ShowTitulos = Action != eAccionSR.Escoger || CajaCerrada;
            ShowStatusCajaCerrada = CajaCerrada;
            ShowStatusCajaAbierta = !CajaCerrada;
            CargarValoresInicialesDeMoneda();
        }

        private void CargarValoresInicialesDeMoneda() {
            vMonedaLocal = new Saw.Lib.clsNoComunSaw();
            _UsaCobroMultimoneda = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "UsaCobroDirectoEnMultimoneda");
            if (Action == eAccionSR.Insertar) {
                if (_UsaCobroMultimoneda) {
                    CodigoMoneda = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "CodigoMonedaExtranjera");
                    _CodigoMEInicial = CodigoMoneda;
                } else {
                    CodigoMoneda = vMonedaLocal.InstanceMonedaLocalActual.CodigoMoneda(LibDate.Today());
                }
                AsignaTasaDelDia(CodigoMoneda);
            } else {
                ConexionMoneda = FirstConnectionRecordOrDefault<FkMonedaViewModel>("Moneda", LibSearchCriteria.CreateCriteriaFromText("Codigo", CodigoMoneda));
                CodigoMoneda = ConexionMoneda.Codigo;
                Moneda = ConexionMoneda.Nombre;
            }
        }

        protected override void InitializeLookAndFeel(CajaApertura valModel) {
            base.InitializeLookAndFeel(valModel);
            switch (Action) {
                case eAccionSR.Insertar:
                    Title = ModuleName + " - " + "Abrir";
                    break;
                case eAccionSR.Modificar:
                    Title = ModuleName + " - " + "Cerrar";
                    break;
                case eAccionSR.Escoger:
                    Title = ModuleName + " - " + " Asignar";
                    break;
            }

        }

        protected override void InitializeCommands() {
            base.InitializeCommands();
            ChooseNombreCajaCommand = new RelayCommand<string>(ExecuteChooseNombreCajaCommand);
            ChooseNombreDelUsuarioCommand = new RelayCommand<string>(ExecuteChooseNombreDelUsuarioCommand);
            ChooseMonedaCommand = new RelayCommand<string>(ExecuteChooseMonedaCommand);
            AbrirCajaCommand = new RelayCommand(ExecuteAbrirCajaCommand, CanExecuteAbrirCajaCommand);
            CerrarCajaCommand = new RelayCommand(ExecuteCerrarCajaCommand, CanExecuteCerrarCajaCommand);
            CloseCommand = new RelayCommand(ExecuteCloseCommand, CanExecuteAction);
            AsignarCajaCommand = new RelayCommand(ExecuteAsignarCajaCommand, CanExecuteAsignarCajaCommand);
        }

        protected override void InitializeRibbon() {
            base.InitializeRibbon();
            if (RibbonData.TabDataCollection != null && RibbonData.TabDataCollection.Count > 0) {
                RibbonData.TabDataCollection[0].GroupDataCollection.Remove(RibbonData.TabDataCollection[0].GroupDataCollection[0]);
                RibbonData.TabDataCollection[0].AddTabGroupData(new LibRibbonGroupData("Configurar"));
                if (Action == eAccionSR.Insertar) {
                    RibbonData.TabDataCollection[0].GroupDataCollection[0].AddRibbonControlData(CreateAbrirCajaRibbonButtonData());
                } else if (Action == eAccionSR.Escoger) {
                    RibbonData.TabDataCollection[0].GroupDataCollection[0].AddRibbonControlData(CreateAsignarCajaRibbonButtonData());
                } else if (Action == eAccionSR.Modificar) {
                    RibbonData.TabDataCollection[0].GroupDataCollection[0].AddRibbonControlData(CreateCerrarCajaRibbonButtonData());
                }
                RibbonData.TabDataCollection[0].GroupDataCollection[0].AddRibbonControlData(CreateSalirRibbonButtonData());
            }
        }

        #endregion //Constructores

        #region Comandos

        private LibRibbonButtonData CreateAbrirCajaRibbonButtonData() {
            return new LibRibbonButtonData() {
                Label = "Abrir Caja",
                Command = AbrirCajaCommand,
                LargeImage = new Uri("/Galac.Adm.Uil.Venta;component/Images/Open.png", UriKind.Relative),
                ToolTipDescription = "Abrir Caja Registradora",
                ToolTipTitle = "Abrir Caja"
            };
        }

        private LibRibbonButtonData CreateCerrarCajaRibbonButtonData() {
            return new LibRibbonButtonData() {
                Label = "Cerrar Caja",
                Command = CerrarCajaCommand,
                LargeImage = new Uri("/Galac.Adm.Uil.Venta;component/Images/Close.png", UriKind.Relative),
                ToolTipDescription = "Cerrar Caja Registradora",
                ToolTipTitle = "Cerrar Caja"
            };
        }

        private LibRibbonButtonData CreateSalirRibbonButtonData() {
            return new LibRibbonButtonData() {
                Label = "Salir",
                Command = CloseCommand,
                LargeImage = new Uri("/LibGalac.Aos.UI.WpfRD;component/Images/close.png", UriKind.Relative),
                ToolTipDescription = "Salir",
                ToolTipTitle = "Salir"
            };
        }

        private LibRibbonButtonData CreateAsignarCajaRibbonButtonData() {
            return new LibRibbonButtonData() {
                Label = "Asignar Caja",
                Command = AsignarCajaCommand,
                LargeImage = new Uri("/LibGalac.Aos.UI.WpfRD;component/Images/exec.png", UriKind.Relative),
                ToolTipDescription = "Asignar una Caja",
                ToolTipTitle = "Asignar Caja"
            };
        }

        private void ExecuteAbrirCajaCommand() {
            bool vSePuede = false;
            try {
                vSePuede = ValidarCajasAbiertas() && ValidarUsuarioAsignado();
                if (vSePuede) {
                    base.ExecuteAction();
                    LibMessages.MessageBox.Information(this, "La caja " + NombreCaja + " fue abierta con exito.", "");
                    ExecuteCloseCommand();
                }

            } catch (Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void ExecuteCerrarCajaCommand() {
            bool vSePuede = false;
            try {
                vSePuede = ValidarCajasAbiertas() && ValidarUsuarioAsignado();
                if (vSePuede) {
                    CajaCerrada = true;
                    base.ExecuteAction();
                    if (ConexionNombreCaja == null) {
                        LibMessages.MessageBox.Information(this, "La caja no pudo ser cerrada", "");
                    } else {
                        if (ConexionNombreCaja.UsaMaquinaFiscal) {
                            ImprimirCierreX();
                        }
                        LibMessages.MessageBox.Information(this, "La caja " + NombreCaja + " fue Cerrada con exíto", "");
                        ExecuteCloseCommand();
                    }
                }
            } catch (Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void ExecuteCloseCommand() {
            RaiseRequestCloseEvent();
        }

        private void ExecuteAsignarCajaCommand() {
            try {
                MoveFocusIfNecessary();
                if (!LibString.IsNullOrEmpty(NombreCaja)) {
                    insCajaApertura.AsignarCaja(ConsecutivoCaja);
                    LibMessages.MessageBox.Information(this, "La caja " + NombreCaja + " fue Asignada con exito.", "");
                    base.ExecuteAction();
                    ExecuteCloseCommand();
                } else {
                    LibMessages.MessageBox.Alert(this, "El nombre de la caja es requierido ", "");
                }
            } catch (GalacException vEx) {
                LibMessages.MessageBox.Information(this, vEx.Message, "");
            }
        }

        private void ExecuteChooseNombreCajaCommand(string valNombreCaja) {
            try {
                if (valNombreCaja == null) {
                    valNombreCaja = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("NombreCaja", valNombreCaja);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("ConsecutivoCompania", LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"));
                ConexionNombreCaja = ChooseRecord<FkCajaViewModel>("Caja", vDefaultCriteria, vFixedCriteria, string.Empty);
                if (ConexionNombreCaja != null) {
                    ConsecutivoCaja = ConexionNombreCaja.Consecutivo;
                    NombreCaja = ConexionNombreCaja.NombreCaja;
                    if (Action == eAccionSR.Modificar || Action == eAccionSR.Insertar) {
                        _CajaCerrada = insCajaApertura.GetCajaCerrada(ConsecutivoCompania, ConsecutivoCaja, false);                        
                    }
                } else {
                    ConsecutivoCaja = 0;
                    NombreCaja = string.Empty;
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void ExecuteChooseNombreDelUsuarioCommand(string valUserName) {
            try {
                if (valUserName == null) {
                    valUserName = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("UserName", valUserName);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("Status", eStatusUsuario.Activo);
                ConexionNombreDelUsuario = ChooseRecord<FkGUserViewModel>("Usuario", vDefaultCriteria, vFixedCriteria, string.Empty);
                if (ConexionNombreDelUsuario != null) {
                    NombreDelUsuario = ConexionNombreDelUsuario.UserName;
                    if (Action == eAccionSR.Modificar || Action == eAccionSR.Insertar) {
                        _UsuarioFueAsignado = insCajaApertura.UsuarioFueAsignado(ConsecutivoCompania, ConsecutivoCaja, NombreDelUsuario, false, false);
                    }
                } else {
                    NombreDelUsuario = string.Empty;
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void ExecuteChooseMonedaCommand(string valMoneda) {
            try {
                if (valMoneda == null) {
                    valMoneda = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Nombre", valMoneda);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("Activa", LibConvert.BoolToSN(true));
                vFixedCriteria.Add("TipoDeMoneda", eBooleanOperatorType.IdentityEquality, eTipoDeMoneda.Fisica);
                AgregarCriteriaParaExcluirMonedasLocalesNoVigentesAlDiaActual(ref vFixedCriteria);
                ConexionMoneda = ChooseRecord<FkMonedaViewModel>("Moneda", vDefaultCriteria, vFixedCriteria, string.Empty);
                if (ConexionMoneda != null) {
                    Moneda = ConexionMoneda.Nombre;
                    CodigoMoneda = ConexionMoneda.Codigo;
                    AsignaTasaDelDia(CodigoMoneda);
                } else {
                    CodigoMoneda = string.Empty;
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void AgregarCriteriaParaExcluirMonedasLocalesNoVigentesAlDiaActual(ref LibSearchCriteria vFixedCriteria) {
            XElement vXmlMonedaLocales = ((Comun.Ccl.TablasGen.IMonedaLocalPdn)new Comun.Brl.TablasGen.clsMonedaLocalProcesos()).BusquedaTodasLasMonedasLocales(LibDefGen.ProgramInfo.Country);
            IList<Comun.Ccl.TablasGen.MonedaLocalActual> vListaDeMonedaLocales = vXmlMonedaLocales != null ? LibParserHelper.ParseToList<Comun.Ccl.TablasGen.MonedaLocalActual>(new XDocument(vXmlMonedaLocales)) : null;
            if (vListaDeMonedaLocales != null) {
                foreach (Comun.Ccl.TablasGen.MonedaLocalActual vMoneda in vListaDeMonedaLocales) {
                    vFixedCriteria.Add("Codigo", eBooleanOperatorType.IdentityInequality, vMoneda.CodigoMoneda);
                }
            }
        }

        private bool CanExecuteAbrirCajaCommand() {
            return true;
        }

        private bool CanExecuteCerrarCajaCommand() {
            return true;
        }

        private bool CanExecuteAsignarCajaCommand() {
            return true;
        }

        #endregion

        #region Validations

        private ValidationResult FechaValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if ((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else {
                if (LibDefGen.DateIsGreaterThanDateLimitForEnterData(Fecha, false, Action)) {
                    vResult = new ValidationResult(LibDefGen.TooltipMessageDateRestrictionDemoProgram("Fecha"));
                }
            }
            return vResult;
        }

        #endregion //Validations

        #region Metodos

        protected override CajaApertura FindCurrentRecord(CajaApertura valModel) {
            if (valModel == null) {
                return null;
            }
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valModel.ConsecutivoCompania);
            vParams.AddInInteger("Consecutivo", valModel.Consecutivo);
            vParams.AddInInteger("ConsecutivoCaja", valModel.ConsecutivoCaja);
            return BusinessComponent.GetData(eProcessMessageType.SpName, "CajaAperturaGET", vParams.Get()).FirstOrDefault();
        }

        protected override ILibBusinessComponentWithSearch<IList<CajaApertura>, IList<CajaApertura>> GetBusinessComponent() {
            return new clsCajaAperturaNav();
        }

        protected override void ReloadRelatedConnections() {
            base.ReloadRelatedConnections();
            ConexionNombreCaja = FirstConnectionRecordOrDefault<FkCajaViewModel>("Caja", LibSearchCriteria.CreateCriteria("NombreCaja", NombreCaja));
            ConexionNombreDelUsuario = FirstConnectionRecordOrDefault<FkGUserViewModel>("Usuario", LibSearchCriteria.CreateCriteria("UserName", NombreDelUsuario));
            ConexionMoneda = FirstConnectionRecordOrDefault<FkMonedaViewModel>("Moneda", LibSearchCriteria.CreateCriteria("Codigo", CodigoMoneda));
        }

        protected override IEnumerable GetListFromModule(string valModuleName, LibSearchCriteria valCriteria, Type valRecordType, string valOrderByMember) {
            return base.GetListFromModule(valModuleName, valCriteria, valRecordType, valOrderByMember);
        }

        protected override List<TItem> GetListFromModule<TItem>(string valModuleName, LibSearchCriteria valCriteria, string valOrderByMember) {
            return base.GetListFromModule<TItem>(valModuleName, valCriteria, valOrderByMember);
        }

        private bool ValidarUsuarioAsignado() {
            bool vResult = true;
            if (_UsuarioFueAsignado && Action != eAccionSR.Escoger) {
                LibMessages.MessageBox.Information(this, "Ya existe una caja abierta por el usuario: " + NombreDelUsuario, ModuleName);
                NombreDelUsuario = string.Empty;
                vResult = false;
            }
            return vResult;
        }

        private bool ValidarCajasAbiertas() {
            bool vResult = true;
            if (!_CajaCerrada && Action == eAccionSR.Insertar) {
                LibMessages.MessageBox.Information(this, "La caja " + NombreCaja + " ya ha sido abierta", ModuleName);
                NombreCaja = string.Empty;
                ConsecutivoCaja = 0;
                vResult = false;
            } else if (_CajaCerrada && Action == eAccionSR.Modificar) {
                LibMessages.MessageBox.Information(this, "La caja " + NombreCaja + " ya fue cerrada", ModuleName);
                NombreCaja = string.Empty;
                vResult = false;
            }
            return vResult;
        }

        private bool ImprimirCierreX() {
            try {
                bool vResult = false;
                ICajaPdn insCaja = new clsCajaNav();
                XElement xmlImpresoraFiscal = null;
                insCaja.FindByConsecutivoCaja(ConsecutivoCompania, ConsecutivoCaja, "", ref xmlImpresoraFiscal);
                clsImpresoraFiscalCreator vCreatorMaquinaFiscal = new clsImpresoraFiscalCreator();
                IImpresoraFiscalPdn insIMaquinaFiscal = vCreatorMaquinaFiscal.Crear(xmlImpresoraFiscal);
                vResult = insIMaquinaFiscal.RealizarReporteX();
                insCaja.ActualizarCierreXEnFacturas(ConsecutivoCompania, ConsecutivoCaja, Fecha, HoraApertura, HoraCierre);
                insIMaquinaFiscal = null;
                return vResult;
            } catch (Exception vEx) {
                throw vEx;
            }
        }

        private void TotalesPorCierreDeCaja() {
            XElement vReq = null;
            if (insCajaApertura.TotalesMontosPorFormaDecobro(ref vReq, ConsecutivoCompania, ConsecutivoCaja, Fecha, HoraCierre, CodigoMoneda)) {
                MontoEfectivo = LibImportData.ToDec(LibXml.GetPropertyString(vReq, "MontoEfectivo"));
                MontoTarjeta = LibImportData.ToDec(LibXml.GetPropertyString(vReq, "MontoTarjeta"));
                MontoCheque = LibImportData.ToDec(LibXml.GetPropertyString(vReq, "MontoCheque"));
                MontoDeposito = LibImportData.ToDec(LibXml.GetPropertyString(vReq, "MontoDeposito"));
                MontoAnticipo = LibImportData.ToDec(LibXml.GetPropertyString(vReq, "MontoAnticipo"));
                MontoVuelto = LibImportData.ToDec(LibXml.GetPropertyString(vReq, "MontoVuelto"));
                MontoVueltoPM = LibImportData.ToDec(LibXml.GetPropertyString(vReq, "MontoVueltoPM"));
                MontoCierre = MontoApertura + MontoEfectivo + MontoTarjeta + MontoCheque + MontoDeposito + MontoAnticipo + MontoVuelto + MontoVueltoPM;
                MontoEfectivoME = LibImportData.ToDec(LibXml.GetPropertyString(vReq, "MontoEfectivoME"));
                MontoTarjetaME = LibImportData.ToDec(LibXml.GetPropertyString(vReq, "MontoTarjetaME"));
                MontoChequeME = LibImportData.ToDec(LibXml.GetPropertyString(vReq, "MontoChequeME"));
                MontoDepositoME = LibImportData.ToDec(LibXml.GetPropertyString(vReq, "MontoDepositoME"));
                MontoAnticipoME = LibImportData.ToDec(LibXml.GetPropertyString(vReq, "MontoAnticipoME"));
                MontoVueltoME = LibImportData.ToDec(LibXml.GetPropertyString(vReq, "MontoVueltoME"));
                MontoCierreME = MontoAperturaME + MontoEfectivoME + MontoTarjetaME + MontoChequeME + MontoDepositoME + MontoAnticipoME + MontoVueltoME;
            }
        }        

        public bool IsEnabledForInsert {
            get {
                return (Action == eAccionSR.Insertar);
            }
        }

        public bool IsEnabledForME {
            get {
                return _UsaCobroMultimoneda && IsEnabledForInsert;
            }
        }

        public bool AsignaTasaDelDia(string valCodigoMoneda) {
            vMonedaLocal.InstanceMonedaLocalActual.CargarTodasEnMemoriaYAsignarValoresDeLaActual(LibDefGen.ProgramInfo.Country, LibDate.Today());
            if (!vMonedaLocal.InstanceMonedaLocalActual.EsMonedaLocalDelPais(valCodigoMoneda)) {                
                ConexionMoneda = FirstConnectionRecordOrDefault<FkMonedaViewModel>("Moneda", LibSearchCriteria.CreateCriteriaFromText("Codigo", valCodigoMoneda));
                CodigoMoneda = ConexionMoneda.Codigo;
                Moneda = ConexionMoneda.Nombre;
                bool vElProgramaEstaEnModoAvanzado = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "EsModoAvanzado");
                bool vUsarLimiteMaximoParaIngresoDeTasaDeCambio = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "UsarLimiteMaximoParaIngresoDeTasaDeCambio");
                decimal vMaximoLimitePermitidoParaLaTasaDeCambio = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetDecimal("Parametros", "MaximoLimitePermitidoParaLaTasaDeCambio");
                bool vObtenerAutomaticamenteTasaDeCambioDelBCV = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "ObtenerAutomaticamenteTasaDeCambioDelBCV");
                Cambio = clsSawCambio.InsertaTasaDeCambioParaElDia(valCodigoMoneda, LibDate.Today(), vUsarLimiteMaximoParaIngresoDeTasaDeCambio, vMaximoLimitePermitidoParaLaTasaDeCambio, vElProgramaEstaEnModoAvanzado, vObtenerAutomaticamenteTasaDeCambioDelBCV);
                return Cambio > 0;                
            } else {
                CodigoMoneda = vMonedaLocal.InstanceMonedaLocalActual.CodigoMoneda(Fecha);
                Moneda = vMonedaLocal.InstanceMonedaLocalActual.NombreMoneda(Fecha);
                Cambio = 1;
                return true;
            }
        }

        private ValidationResult TasaDeCambioValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if (Action != eAccionSR.Insertar) {
                return ValidationResult.Success;
            } else if (!LibString.S1IsEqualToS2(CodigoMoneda, vMonedaLocal.InstanceMonedaLocalActual.CodigoMoneda(LibDate.Today()))) {
                if (Cambio <= 1) {
                    vResult = new ValidationResult("La tasa de cambio debe ser mayor a 1");
                }
            }
            return vResult;
        }

        private ValidationResult CodigoMonedaValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if (Action != eAccionSR.Insertar) {
                return ValidationResult.Success;
            } else {
                if (LibString.IsNullOrEmpty(CodigoMoneda)) {
                    vResult = new ValidationResult("El Código de la Moneda es requerido.");
                } else if (_UsaCobroMultimoneda && LibString.S1IsEqualToS2(CodigoMoneda, vMonedaLocal.InstanceMonedaLocalActual.CodigoMoneda(LibDate.Today()))) {
                    vResult = new ValidationResult("La moneda seleccionada debe ser distinta de " + vMonedaLocal.InstanceMonedaLocalActual.NombreMoneda(LibDate.Today()));
                }
            }
            return vResult;
        }
        #endregion //Metodos

    } //End of class CajaAperturaViewModel
} //End of namespace Galac.Adm.Uil.Venta