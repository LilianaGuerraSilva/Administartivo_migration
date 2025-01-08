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
using Galac.Adm.Ccl.DispositivosExternos;
using System.Collections.ObjectModel;
using LibGalac.Aos.UI.Wpf;
using Galac.Adm.Brl.DispositivosExternos.ImpresoraFiscal;
using Galac.Saw.Lib;
using LibGalac.Aos.Ccl.Usal;
using System.Net.NetworkInformation;

namespace Galac.Adm.Uil.Venta.ViewModel {
    public class CajaViewModel : LibInputViewModel<Caja> {

        #region Constantes y Variables

        const string ConsecutivoPropertyName = "Consecutivo";
        const string NombreCajaPropertyName = "NombreCaja";
        const string UsaGavetaPropertyName = "UsaGaveta";
        const string PuertoPropertyName = "Puerto";
        const string ComandoPropertyName = "Comando";
        const string PermitirAbrirSinSupervisorPropertyName = "PermitirAbrirSinSupervisor";
        const string UsaAccesoRapidoPropertyName = "UsaAccesoRapido";
        const string FamiliaImpresoraFiscalPropertyName = "FamiliaImpresoraFiscal";
        const string ModeloDeMaquinaFiscalPropertyName = "ModeloDeMaquinaFiscal";
        const string SerialDeMaquinaFiscalPropertyName = "SerialDeMaquinaFiscal";
        const string TipoConexionPropertyName = "TipoConexion";
        const string PuertoMaquinaFiscalPropertyName = "PuertoMaquinaFiscal";
        const string AbrirGavetaDeDineroPropertyName = "AbrirGavetaDeDinero";
        const string UltimoNumeroCompFiscalPropertyName = "UltimoNumeroCompFiscal";
        const string UltimoNumeroNCFiscalPropertyName = "UltimoNumeroNCFiscal";
        const string IpParaConexionPropertyName = "IpParaConexion";
        const string MascaraSubredPropertyName = "MascaraSubred";
        const string GatewayPropertyName = "Gateway";
        const string PermitirDescripcionDelArticuloExtendidaPropertyName = "PermitirDescripcionDelArticuloExtendida";
        const string PermitirNombreDelClienteExtendidoPropertyName = "PermitirNombreDelClienteExtendido";
        const string UsarModoDotNetPropertyName = "UsarModoDotNet";
        const string RegistroDeRetornoEnTxtPropertyName = "RegistroDeRetornoEnTxt";
        const string NombreOperadorPropertyName = "NombreOperador";
        const string FechaUltimaModificacionPropertyName = "FechaUltimaModificacion";
        const string IsEnabledUsaGavetaPropertyName = "IsEnabledUsaGaveta";
        const string IsEnabledPuertoSerialPropertyName = "IsEnabledPuertoSerial";
        const string IsVisibleRegistroDeRetornoEnTxtPropertyName = "IsVisibleRegistroDeRetornoEnTxt";
        private Brl.DispositivosExternos.clsConexionPuertoSerial PuertoSerial;
        IImpresoraFiscalPdn insMaquinaFiscal;
        private bool _PuedeAbrirGaveta = false;
        FkCajaViewModel _ConexionNombreCaja;
        FkGUserViewModel _ConexionNombreDelOperador;
        bool _RegistroDeRetornoEnTxtOld;

        bool UsaMaquinaFiscalValorPrevio;
        eFamiliaImpresoraFiscal FamiliaImpresoraPreConfiguradaAsEnum;
        eImpresoraFiscal ModeloDeMaquinaPreconfiguradaAsEnum;
        eTipoConexion TipoConexionPreconfiguradaAsEnum;
        string SerialDeMaquinaPreconfigurada;
        string UltimoNumeroCompPreconfigurada;
        string UltimoNumeroNCPreconfigurada;
        string _MotivoEliminacionOModificacion;
        #endregion //Constantes y Variables

        #region Propiedades      

        public override string ModuleName {
            get { return "Caja Registradora"; }
        }

        public bool EnableParaMaquinaFiscal { get; set; }

        public ObservableCollection<ePuerto> ListarPuertos { get; set; }

        public ObservableCollection<ePuerto> ListarPuertosImpFiscal { get; set; }

        public ObservableCollection<eImpresoraFiscal> ListarMaquinaFiscal { get; set; }

        public ObservableCollection<eTipoConexion> ListarTipoConexion { get; set; }

        eFamiliaImpresoraFiscal FamiliaImpresoraFiscalSeleccionada { get; set; }

        public RelayCommand ObtenerSerialCommand {
            get;
            private set;
        }

        public RelayCommand CancelarDocumentoCommand {
            get;
            private set;
        }

        public RelayCommand AbrirGavetaCommand {
            get;
            private set;
        }

        public RelayCommand DiagnosticarCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseNombreCajaCommand {
            get;
            private set;
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

        public FkGUserViewModel ConexionNombreDelOperador {
            get {
                return _ConexionNombreDelOperador;
            }
            set {
                if (_ConexionNombreDelOperador != value) {
                    _ConexionNombreDelOperador = value;
                    RaisePropertyChanged(NombreOperadorPropertyName);
                }
                if (_ConexionNombreDelOperador == null) {
                    NombreOperador = string.Empty;
                }
            }
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
                    RaisePropertyChanged(ConsecutivoPropertyName);
                }
            }
        }

        public eFamiliaImpresoraFiscal FamiliaImpresoraFiscal {
            get {
                return Model.FamiliaImpresoraFiscalAsEnum;
            }
            set {
                if (Model.FamiliaImpresoraFiscalAsEnum != value) {
                    Model.FamiliaImpresoraFiscalAsEnum = value;
                    UsarModoDotNet = FamiliaValidaParaModoMejorado(value);
                    RaisePropertyChanged(FamiliaImpresoraFiscalPropertyName);
                    RaisePropertyChanged(UsarModoDotNetPropertyName);
                    RaisePropertyChanged(IsVisibleRegistroDeRetornoEnTxtPropertyName);
                }
            }
        }

        [LibRequired(ErrorMessage = "El campo Nombre Caja es requerido.")]
        [LibGridColum("Nombre Caja", eGridColumType.Connection, IsForSearch = true, IsForList = true, ConnectionSearchCommandName = "ChooseNombreCajaCommand", ColumnOrder = 0, Width = 190)]
        public string NombreCaja {
            get {
                return Model.NombreCaja;
            }
            set {
                if (Model.NombreCaja != value) {
                    Model.NombreCaja = value;
                    RaisePropertyChanged(NombreCajaPropertyName);
                }
            }
        }

        public bool UsaGaveta {
            get {
                return Model.UsaGavetaAsBool;
            }
            set {
                if (Model.UsaGavetaAsBool != value) {
                    Model.UsaGavetaAsBool = value;
                    _PuedeAbrirGaveta = value;
                    AbrirGavetaCommand.RaiseCanExecuteChanged();
                    RaisePropertyChanged(UsaGavetaPropertyName);
                    RaisePropertyChanged(PuertoPropertyName);
                    RaisePropertyChanged(IsEnabledUsaGavetaPropertyName);
                    AbrirGavetaCommand.RaiseCanExecuteChanged();
                }
            }
        }

        [LibCustomValidation("PuertoDeMaquinaFiscalValidating")]
        public ePuerto Puerto {
            get {
                return Model.PuertoAsEnum;
            }
            set {
                if (Model.PuertoAsEnum != value) {
                    Model.PuertoAsEnum = value;
                    RaisePropertyChanged(PuertoPropertyName);
                    RaisePropertyChanged(PuertoMaquinaFiscalPropertyName);
                }
            }
        }

        [LibCustomValidation("ComandoGavetaValidating")]
        public string Comando {
            get {
                return Model.Comando;
            }
            set {
                if (Model.Comando != value) {
                    Model.Comando = value;
                    RaisePropertyChanged(ComandoPropertyName);
                }
            }
        }

        public bool PermitirAbrirSinSupervisor {
            get {
                return Model.PermitirAbrirSinSupervisorAsBool;
            }
            set {
                if (Model.PermitirAbrirSinSupervisorAsBool != value) {
                    Model.PermitirAbrirSinSupervisorAsBool = value;
                    RaisePropertyChanged(PermitirAbrirSinSupervisorPropertyName);
                }
            }
        }

        public bool UsaAccesoRapido {
            get {
                return Model.UsaAccesoRapidoAsBool;
            }
            set {
                if (Model.UsaAccesoRapidoAsBool != value) {
                    Model.UsaAccesoRapidoAsBool = value;
                    RaisePropertyChanged(UsaAccesoRapidoPropertyName);
                }
            }
        }

        public bool UsaMaquinaFiscal {
            get {
                return Model.UsaMaquinaFiscalAsBool;
            }
            set {
                if (Model.UsaMaquinaFiscalAsBool != value) {
                    Model.UsaMaquinaFiscalAsBool = value;
                    RaisePropertyChanged(SerialDeMaquinaFiscalPropertyName);
                }
            }
        }

        [LibCustomValidation("ModeloDeMaquinaFiscalValidating")]
        public eImpresoraFiscal ModeloDeMaquinaFiscal {
            get {
                return Model.ModeloDeMaquinaFiscalAsEnum;
            }
            set {
                if (Model.ModeloDeMaquinaFiscalAsEnum != value) {
                    Model.ModeloDeMaquinaFiscalAsEnum = value;
                    RaisePropertyChanged(ModeloDeMaquinaFiscalPropertyName);
                }
            }
        }

        [LibCustomValidation("SerialDeMaquinaFiscalValidating")]
        [LibGridColum("SerialDeMaquinaFiscal", eGridColumType.Generic, IsForList = true, IsForSearch = true, Header = "Serial", Width = 80, ColumnOrder = 6)]
        public string SerialDeMaquinaFiscal {
            get {
                return Model.SerialDeMaquinaFiscal;
            }
            set {
                if (Model.SerialDeMaquinaFiscal != value) {
                    Model.SerialDeMaquinaFiscal = value;
                    RaisePropertyChanged(SerialDeMaquinaFiscalPropertyName);
                }
            }
        }

        public eTipoConexion TipoConexion {
            get {
                return Model.TipoConexionAsEnum;
            }
            set {
                if (Model.TipoConexionAsEnum != value) {
                    Model.TipoConexionAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(TipoConexionPropertyName);
                    RaisePropertyChanged(IsEnabledPuertoSerialPropertyName);
                }
            }
        }

        [LibCustomValidation("PuertoDeMaquinaFiscalValidating")]
        [LibGridColum("PuertoMaquinaFiscal", eGridColumType.Generic, Header = "Puerto Maq. Fiscal", Width = 120, ColumnOrder = 7)]
        public ePuerto PuertoMaquinaFiscal {
            get {
                return Model.PuertoMaquinaFiscalAsEnum;
            }
            set {
                if (Model.PuertoMaquinaFiscalAsEnum != value) {
                    Model.PuertoMaquinaFiscalAsEnum = value;
                    RaisePropertyChanged(PuertoMaquinaFiscalPropertyName);
                    RaisePropertyChanged(PuertoPropertyName);
                }
            }
        }

        public bool AbrirGavetaDeDinero {
            get {
                return Model.AbrirGavetaDeDineroAsBool;
            }
            set {
                if (Model.AbrirGavetaDeDineroAsBool != value) {
                    Model.AbrirGavetaDeDineroAsBool = value;
                    RaisePropertyChanged(AbrirGavetaDeDineroPropertyName);
                }
            }
        }

        public string UltimoNumeroCompFiscal {
            get {
                return Model.UltimoNumeroCompFiscal;
            }
            set {
                if (Model.UltimoNumeroCompFiscal != value) {
                    Model.UltimoNumeroCompFiscal = value;

                    RaisePropertyChanged(UltimoNumeroCompFiscalPropertyName);
                }
            }
        }

        public string UltimoNumeroNCFiscal {
            get {
                return Model.UltimoNumeroNCFiscal;
            }
            set {
                if (Model.UltimoNumeroNCFiscal != value) {
                    Model.UltimoNumeroNCFiscal = value;
                    RaisePropertyChanged(UltimoNumeroNCFiscalPropertyName);
                }
            }
        }

        public string IpParaConexion {
            get {
                return Model.IpParaConexion;
            }
            set {
                if (Model.IpParaConexion != value) {
                    Model.IpParaConexion = value;

                    RaisePropertyChanged(IpParaConexionPropertyName);
                }
            }
        }

        public string MascaraSubred {
            get {
                return Model.MascaraSubred;
            }
            set {
                if (Model.MascaraSubred != value) {
                    Model.MascaraSubred = value;

                    RaisePropertyChanged(MascaraSubredPropertyName);
                }
            }
        }

        public string Gateway {
            get {
                return Model.Gateway;
            }
            set {
                if (Model.Gateway != value) {
                    Model.Gateway = value;

                    RaisePropertyChanged(GatewayPropertyName);
                }
            }
        }

        public bool PermitirDescripcionDelArticuloExtendida {
            get {
                return Model.PermitirDescripcionDelArticuloExtendidaAsBool;
            }
            set {
                if (Model.PermitirDescripcionDelArticuloExtendidaAsBool != value) {
                    Model.PermitirDescripcionDelArticuloExtendidaAsBool = value;
                    if (Model.PermitirDescripcionDelArticuloExtendidaAsBool) {
                        UsarModoDotNet = false;
                        RaisePropertyChanged(UsarModoDotNetPropertyName);
                    }
                    RaisePropertyChanged(PermitirDescripcionDelArticuloExtendidaPropertyName);
                }
            }
        }

        public bool PermitirNombreDelClienteExtendido {
            get {
                return Model.PermitirNombreDelClienteExtendidoAsBool;
            }
            set {
                if (Model.PermitirNombreDelClienteExtendidoAsBool != value) {
                    Model.PermitirNombreDelClienteExtendidoAsBool = value;
                    if (Model.PermitirNombreDelClienteExtendidoAsBool) {
                        UsarModoDotNet = false;
                        RaisePropertyChanged(UsarModoDotNetPropertyName);
                    }
                    RaisePropertyChanged(PermitirNombreDelClienteExtendidoPropertyName);
                }
            }
        }

        public bool UsarModoDotNet {
            get {
                return Model.UsarModoDotNetAsBool;
            }
            set {
                if (Model.UsarModoDotNetAsBool != value) {
                    Model.UsarModoDotNetAsBool = value;
                    RaisePropertyChanged(UsarModoDotNetPropertyName);
                }
            }
        }

        public bool RegistroDeRetornoEnTxt {
            get {
                return Model.RegistroDeRetornoEnTxtAsBool;
            }
            set {
                if (Model.RegistroDeRetornoEnTxtAsBool != value) {
                    Model.RegistroDeRetornoEnTxtAsBool = value;
                    RaisePropertyChanged(RegistroDeRetornoEnTxtPropertyName);
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

        //[LibGridColum("UsaMaquinaFiscal", eGridColumType.Generic, Header = "Usa Maquina Fiscal", IsForSearch = false, Alignment = eTextAlignment.Center, Width = 150)]
        public string UsaMaquinaFiscalStr {
            get {
                return Model.UsaMaquinaFiscalAsBool ? "\u2713" : "";
            }
        }

        [LibGridColum("ModeloDeMaquinaFiscal", eGridColumType.Generic, Header = "Modelo de Maquina Fiscal", IsForSearch = false, Alignment = eTextAlignment.Center, Width = 200)]
        public string ModeloDeMaquinaFiscalStr {
            get {
                return Model.UsaMaquinaFiscalAsBool ? LibEnumHelper.GetDescription(ModeloDeMaquinaFiscal) : "";
            }
        }

        public eFamiliaImpresoraFiscal[] ArrayFamiliaImpresoraFiscal {
            get {
                return LibEnumHelper<eFamiliaImpresoraFiscal>.GetValuesInArray();
            }
        }

        [LibCustomValidation("MotivoEliminacionOModificacionValidating")]
        public string MotivoEliminacionOModificacion {
            get {
                return _MotivoEliminacionOModificacion;
            }
            set {
                if (_MotivoEliminacionOModificacion != value) {
                    _MotivoEliminacionOModificacion = value;
                    RaisePropertyChanged("MotivoEliminacionOModificacion");
                }
            }
        }

        public bool IsVisibleParaEpson {
            get {
                return false; // FamiliaImpresoraFiscalSeleccionada == eFamiliaImpresoraFiscal.EPSONPNP;
            }
        }

        public bool IsVisibleParaQPrint {
            get {
                return false; //FamiliaImpresoraFiscalSeleccionada == eFamiliaImpresoraFiscal.QPRINT;
            }
        }

        public bool IsVisibleImpresoraFiscal {
            get {
                return LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "UsaMaquinaFiscal");
            }
        }

        public bool IsVisibleMotivoEliminacionOModificacion {
            get {
                return (LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "UsaMaquinaFiscal") && Action == eAccionSR.Modificar);
            }
        }

        public bool IsVisibleRegistroDeRetornoEnTxt {
            get {
                return LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "EsModoAvanzado") && FamiliaImpresoraFiscal == eFamiliaImpresoraFiscal.BEMATECH;
            }
        }

        public bool IsEnabledMaquinaFiscal {
            get {
                return IsEnabled && !LibString.S1IsEqualToS2(NombreCaja, "CAJA GENÉRICA");
            }
        }

        private bool FamiliaValidaParaModoMejorado(eFamiliaImpresoraFiscal valFamilia) {
            return (valFamilia == eFamiliaImpresoraFiscal.EPSONPNP
                    || valFamilia == eFamiliaImpresoraFiscal.THEFACTORY
                    || valFamilia == eFamiliaImpresoraFiscal.BEMATECH);

        }

        public bool IsVisibleParaVenezuela {
            get {
                return LibDefGen.ProgramInfo.IsCountryVenezuela();
            }
        }

        public bool IsEnabledUsaGaveta {
            get {
                return UsaGaveta && IsEnabled;
            }
        }

        public bool IsEnabledCancelarComprobanteFiscal {
            get {
                return IsEnabled || LibSecurityManager.CurrentUserHasAccessTo("Caja Registradora", "Cancelar Documento");
            }
        }

        public bool IsEnabledForCajaGenerica {
            get {
                return IsEnabled && Model.NombreCaja != "CAJA GENÉRICA";
            }
        }

        public bool IsEnabledPuertoSerial {
            get { return IsEnabled && TipoConexion == eTipoConexion.PuertoSerial; }
        }

        #endregion //Propiedades

        #region Constructores e Inicializadores

        public CajaViewModel()
            : this(new Caja(), eAccionSR.Insertar) {
        }

        public CajaViewModel(Caja initModel, eAccionSR initAction)
            : base(initModel, initAction) {
            Model.ConsecutivoCompania = LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania");
            DefaultFocusedPropertyName = NombreCajaPropertyName;
            ListarPuertos = new ObservableCollection<ePuerto>();
            ListarPuertosImpFiscal = new ObservableCollection<ePuerto>();
            ListarMaquinaFiscal = new ObservableCollection<eImpresoraFiscal>();
            ListarTipoConexion = new ObservableCollection<eTipoConexion>();
            if (initAction == eAccionSR.Insertar) {
                FamiliaImpresoraFiscal = eFamiliaImpresoraFiscal.THEFACTORY;
                TipoConexion = eTipoConexion.PuertoSerial;
            }
            PuertoSerial = new Brl.DispositivosExternos.clsConexionPuertoSerial();
            _RegistroDeRetornoEnTxtOld = RegistroDeRetornoEnTxt;
        }

        protected override void InitializeLookAndFeel(Caja valModel) {
            base.InitializeLookAndFeel(valModel);
            Comando = "#0#0";
            Model.ConsecutivoCompania = LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania");
            UltimoNumeroCompFiscal = (UltimoNumeroCompFiscal == "") ? LibText.FillWithCharToLeft("", LibConvert.ToStr("0"), 8) : UltimoNumeroCompFiscal;
            UltimoNumeroNCFiscal = (UltimoNumeroNCFiscal == "") ? LibText.FillWithCharToLeft("", LibConvert.ToStr("0"), 8) : UltimoNumeroNCFiscal;
            LlenarEnumerativosPuertos();
            LlenarEnumerativosTipoDeConexion();
            LlenarEnumerativosPuertosImpFiscal();
            _PuedeAbrirGaveta = UsaGaveta;

            UsaMaquinaFiscalValorPrevio = Model.UsaMaquinaFiscalAsBool;
            FamiliaImpresoraPreConfiguradaAsEnum = Model.FamiliaImpresoraFiscalAsEnum;
            ModeloDeMaquinaPreconfiguradaAsEnum = Model.ModeloDeMaquinaFiscalAsEnum;
            TipoConexionPreconfiguradaAsEnum = Model.TipoConexionAsEnum;
            SerialDeMaquinaPreconfigurada = Model.SerialDeMaquinaFiscal;
            UltimoNumeroCompPreconfigurada = Model.UltimoNumeroCompFiscal;
            UltimoNumeroNCPreconfigurada = Model.UltimoNumeroCompFiscal;
        }

        protected override void InitializeRibbon() {
            base.InitializeRibbon();
            if (RibbonData.TabDataCollection != null && RibbonData.TabDataCollection.Count > 0) {
                RibbonData.TabDataCollection[0].AddTabGroupData(new LibRibbonGroupData("Gaveta"));
                RibbonData.TabDataCollection[0].GroupDataCollection[1].AddRibbonControlData(CreateAbrirGavetaRibbonButtonData());
                RibbonData.TabDataCollection[0].AddTabGroupData(new LibRibbonGroupData("Maquina Fiscal"));
                RibbonData.TabDataCollection[0].GroupDataCollection[2].AddRibbonControlData(CreateProbarConexionRibbonButtonData());
                RibbonData.TabDataCollection[0].GroupDataCollection[2].AddRibbonControlData(CreateDigantosticarRibbonButtonData());
                RibbonData.TabDataCollection[0].GroupDataCollection[2].AddRibbonControlData(CreateCancelarDocumentoRibbonButtonData());
            }
        }

        protected override void InitializeCommands() {
            base.InitializeCommands();
            CancelarDocumentoCommand = new RelayCommand(ExecuteCancelarDocumentoCommand, CanExecuteCancelarDocumento);
            ObtenerSerialCommand = new RelayCommand(ExecuteProbrarConexion, CanExecuteObtenerSerial);
            AbrirGavetaCommand = new RelayCommand(ExecuteAbrirGaveta, CanExecuteAbrirGaveta);
            DiagnosticarCommand = new RelayCommand(ExecuteDiagnosticar, CanExecuteDiagnosticar);
            ChooseNombreCajaCommand = new RelayCommand<string>(ExecuteChooseNombreCajaCommand);
        }

        public override void InitializeViewModel(eAccionSR valAction) {
            base.InitializeViewModel(valAction);
            InitializeRibbon();
        }
        #endregion //Constructores e Inicializadores

        #region Comandos

        private LibRibbonButtonData CreateAbrirGavetaRibbonButtonData() {
            return new LibRibbonButtonData() {
                Label = "Probar Gaveta",
                Command = AbrirGavetaCommand,
                LargeImage = new Uri("/LibGalac.Aos.UI.WpfRD;component/Images/exec.png", UriKind.Relative),
                ToolTipDescription = "Probar Gaveta",
                ToolTipTitle = "Probar Gaveta"
            };
        }

        private LibRibbonButtonData CreateDigantosticarRibbonButtonData() {
            return new LibRibbonButtonData() {
                Label = "Diagnosticar",
                Command = DiagnosticarCommand,
                LargeImage = new Uri("/LibGalac.Aos.UI.WpfRD;component/Images/check.png", UriKind.Relative),
                ToolTipDescription = "Diagnosticar",
                ToolTipTitle = "Diagnosticar"
            };
        }

        private LibRibbonButtonData CreateProbarConexionRibbonButtonData() {
            return new LibRibbonButtonData() {
                Label = "Obtener Serial",
                Command = ObtenerSerialCommand,
                LargeImage = new Uri("/LibGalac.Aos.UI.WpfRD;component/Images/exec.png", UriKind.Relative),
                ToolTipDescription = "Obtener Serial",
                ToolTipTitle = "Obtener Serial"
            };
        }

        private LibRibbonButtonData CreateCancelarDocumentoRibbonButtonData() {
            return new LibRibbonButtonData() {
                Label = "Cancelar Documento Fiscal",
                Command = CancelarDocumentoCommand,
                LargeImage = new Uri("/LibGalac.Aos.UI.WpfRD;component/Images/exec.png", UriKind.Relative),
                ToolTipDescription = "Cancelar Documento Fiscal",
                ToolTipTitle = "Cancelar Documento Fiscal"
            };
        }

        protected override void ExecuteAction() {
            base.ExecuteAction();
            if (Model.ModeloDeMaquinaFiscalAsEnum == eImpresoraFiscal.BEMATECH_MP_4000_FI && _RegistroDeRetornoEnTxtOld != RegistroDeRetornoEnTxt) {
                LibMessages.MessageBox.Information(this, "Los cambios surtirán efecto la próxima vez que Inicie Sesión en el Sistema.", ModuleName);
            }
        }

        protected override void ExecuteProcessBeforeAction() {
            base.ExecuteProcessBeforeAction();
            MoveFocusIfNecessary();
        }

        private void ExecuteAbrirGaveta() {
            bool vReturn = false;
            try {
                if (UsaGaveta) {
                    IGavetaPdn vGaveta = new Brl.DispositivosExternos.CajaGaveta.clsGavetaNav();
                    vReturn = vGaveta.AbrirGaveta(Puerto, Comando);
                }
            } catch (GalacException vEx) {
                LibExceptionDisplay.Show(vEx);
            }
        }

        private void ExecuteDiagnosticar() {
            try {
                XElement ImpresoraXmlFiscalData = ConfiguraDatosDeMaquinaFiscalParaConexion();
                IImpresoraFiscalPdn insIMaquinaFiscal = new clsImpresoraFiscalCreator().Crear(ImpresoraXmlFiscalData);
                CajaDiagnosticoViewModel vViewModel = new CajaDiagnosticoViewModel(insIMaquinaFiscal, FamiliaImpresoraFiscal);
                LibMessages.EditViewModel.ShowEditor(vViewModel, true);
            } catch (GalacException vEx) {
                LibExceptionDisplay.Show(vEx);
            }
        }

        private void ExecuteProbrarConexion() {
            try {
                SerialDeMaquinaFiscal = "";
                UltimoNumeroCompFiscal = "";
                UltimoNumeroNCFiscal = "";
                XElement ImpresoraXmlFiscalData = ConfiguraDatosDeMaquinaFiscalParaConexion();
                insMaquinaFiscal = new Brl.DispositivosExternos.ImpresoraFiscal.clsImpresoraFiscalCreator().Crear(ImpresoraXmlFiscalData);
                SerialDeMaquinaFiscal = insMaquinaFiscal.ObtenerSerial(true);
                UltimoNumeroCompFiscal = LibText.FillWithCharToLeft(insMaquinaFiscal.ObtenerUltimoNumeroFactura(true), "0", 8);
                UltimoNumeroNCFiscal = LibText.FillWithCharToLeft(insMaquinaFiscal.ObtenerUltimoNumeroNotaDeCredito(true), "0", 8);
            } catch (GalacException vEx) {
                LibExceptionDisplay.Show(vEx);
            }
        }

        private void ExecuteCancelarDocumentoCommand() {
            bool vResult = false;
            XElement ImpresoraXmlFiscalData = ConfiguraDatosDeMaquinaFiscalParaConexion();
            IImpresoraFiscalPdn insIMaquinaFiscal = new clsImpresoraFiscalCreator().Crear(ImpresoraXmlFiscalData);
            vResult = insIMaquinaFiscal.CancelarDocumentoFiscalEnImpresion(true);
            insIMaquinaFiscal = null;
        }

        private void ExecuteChooseNombreDelOperadorCommand(string valUserName) {
            try {
                if (valUserName == null) {
                    valUserName = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("UserName", valUserName);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("Status", eStatusUsuario.Activo);
                ConexionNombreDelOperador = ChooseRecord<FkGUserViewModel>("Usuario", vDefaultCriteria, vFixedCriteria, string.Empty);
                if (ConexionNombreDelOperador != null) {
                    NombreOperador = ConexionNombreDelOperador.UserName;
                } else {
                    NombreOperador = string.Empty;
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        public bool CanExecuteCancelarDocumento() {
            return EnableParaMaquinaFiscal && LibSecurityManager.CurrentUserHasAccessTo("Caja Registradora", "Cancelar Documento");
        }

        public bool CanExecuteAbrirGaveta() {
            return _PuedeAbrirGaveta && LibSecurityManager.CurrentUserHasAccessTo("Caja Registradora", "Abrir Gaveta");
        }

        public bool CanExecuteObtenerSerial() {
            return EnableParaMaquinaFiscal;
        }

        public bool CanExecuteDiagnosticar() {
            return EnableParaMaquinaFiscal && !LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "EsUsuarioCajero");
        }


        #endregion

        #region Validations

        ValidationResult SerialDeMaquinaFiscalValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if (LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "UsaMaquinaFiscal") && LibString.IsNullOrEmpty(SerialDeMaquinaFiscal)) {
                return new ValidationResult("El seríal de la maquína Fiscal es requerido");
            } else {
                return ValidationResult.Success;
            }
        }

        ValidationResult ModeloDeMaquinaFiscalValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if (LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "UsaMaquinaFiscal") && LibString.Len(ModeloDeMaquinaFiscalStr) <= 1) {
                return new ValidationResult("El Modelo de maquína Fiscal es requerido");
            } else {
                return ValidationResult.Success;
            }
        }

        ValidationResult PuertoDeMaquinaFiscalValidating() {
            ValidationResult vResult = ValidationResult.Success;
            bool UseSamePorts = (Puerto == PuertoMaquinaFiscal);
            if (LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "UsaMaquinaFiscal") && UsaGaveta && UseSamePorts) {
                return new ValidationResult("El puerto de la gaveta y el de la Máquina Fiscal no pueden ser el mismo");
            } else {
                return ValidationResult.Success;
            }
        }

        ValidationResult ComandoGavetaValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if (UsaGaveta && (Comando == string.Empty)) {
                return new ValidationResult("El comando no puede estar en blanco si se usa la gaveta");
            } else {
                return ValidationResult.Success;
            }
        }

        private bool SeRequiereClaveEspecial() {
            bool vOmitirClaveEspecial = true;

            vOmitirClaveEspecial = vOmitirClaveEspecial && FamiliaImpresoraPreConfiguradaAsEnum == Model.FamiliaImpresoraFiscalAsEnum;
            vOmitirClaveEspecial = vOmitirClaveEspecial && ModeloDeMaquinaPreconfiguradaAsEnum == Model.ModeloDeMaquinaFiscalAsEnum;
            vOmitirClaveEspecial = vOmitirClaveEspecial && TipoConexionPreconfiguradaAsEnum == Model.TipoConexionAsEnum;
            vOmitirClaveEspecial = vOmitirClaveEspecial && SerialDeMaquinaPreconfigurada == Model.SerialDeMaquinaFiscal;
            vOmitirClaveEspecial = vOmitirClaveEspecial && UltimoNumeroCompPreconfigurada == Model.UltimoNumeroCompFiscal;
            vOmitirClaveEspecial = vOmitirClaveEspecial && UltimoNumeroNCPreconfigurada == Model.UltimoNumeroCompFiscal;

            return !vOmitirClaveEspecial;
        }
        ValidationResult MotivoEliminacionOModificacionValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if (LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "UsaMaquinaFiscal") && Action == eAccionSR.Modificar && LibString.IsNullOrEmpty(MotivoEliminacionOModificacion) && SeRequiereClaveEspecial()) {
                return new ValidationResult("Se requiere indicar el Motivo del Cambio.");
            } else {
                return ValidationResult.Success;
            }
        }

        #endregion //Validations

        #region Metodos 

        public void LlenarEnumerativosPuertos() {
            ListarPuertos.Clear();
            string[] vPuertosDisponibles = PuertoSerial.ListarPuertos();
            foreach (var itemPuerto in vPuertosDisponibles) {
                ListarPuertos.Add((ePuerto)Enum.Parse(typeof(ePuerto), itemPuerto));
            }

            if (!ListarPuertos.Contains(Model.PuertoAsEnum)) {
                Model.PuertoAsEnum = ListarPuertos.First();
                LibMessages.MessageBox.Information(this, "Se detectó un cambio del puerto de comunicación de este equipo, recuerde guardar la configuracion actual para tomar el cambio", "");
            }
        }

        public void LlenarEnumerativosPuertosImpFiscal() {
            ListarPuertosImpFiscal.Clear();
            string[] vPuertosDisponibles = PuertoSerial.ListarPuertos();
            foreach (var itemPuerto in vPuertosDisponibles) {
                ListarPuertosImpFiscal.Add((ePuerto)Enum.Parse(typeof(ePuerto), itemPuerto));
            }
            if (!ListarPuertosImpFiscal.Contains(Model.PuertoMaquinaFiscalAsEnum)) {
                Model.PuertoMaquinaFiscalAsEnum = ListarPuertosImpFiscal.First();
                LibMessages.MessageBox.Information(this, "Se detectó un cambio del puerto de comunicación de este equipo, recuerde guardar la configuracion actual para tomar el cambio", "");
            }
        }

        public void LlenarEnumerativosImpresoraFiscal() {
            ListarMaquinaFiscal.Clear();
            FamiliaImpresoraFiscalSeleccionada = Model.FamiliaImpresoraFiscalAsEnum;
            var enumImpreFiscal = (IEnumerable<eImpresoraFiscal>)LibEnumHelper.GetValuesInEnumeration(typeof(eImpresoraFiscal));
            foreach (var enumerativoImpFiscal in enumImpreFiscal) {
                if (FamiliaImpresoraFiscalSeleccionada.GetDescription() == (enumerativoImpFiscal).GetDescription(1)) {
                    ListarMaquinaFiscal.Add(enumerativoImpFiscal);
                }
            }
        }

        public void LlenarEnumerativosTipoDeConexion() {
            ListarTipoConexion.Clear();
            switch (FamiliaImpresoraFiscal) {
                case eFamiliaImpresoraFiscal.THEFACTORY:
                case eFamiliaImpresoraFiscal.ELEPOSVMAX:
                case eFamiliaImpresoraFiscal.EPSONPNP:
                case eFamiliaImpresoraFiscal.QPRINT:
                case eFamiliaImpresoraFiscal.BMC:
                    ListarTipoConexion.Add(eTipoConexion.PuertoSerial);
                    break;
                case eFamiliaImpresoraFiscal.BEMATECH:
                    ListarTipoConexion.Add(eTipoConexion.PuertoSerial);
                    ListarTipoConexion.Add(eTipoConexion.USB);
                    break;
                default:
                    ListarTipoConexion.Add(eTipoConexion.PuertoSerial);
                    break;
            }
            RaisePropertyChanged(TipoConexionPropertyName);
        }


        protected override Caja FindCurrentRecord(Caja valModel) {
            if (valModel == null) {
                return null;
            }
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valModel.ConsecutivoCompania);
            vParams.AddInInteger("Consecutivo", valModel.Consecutivo);
            return BusinessComponent.GetData(eProcessMessageType.SpName, "CajaGET", vParams.Get()).FirstOrDefault();
        }

        protected override ILibBusinessComponentWithSearch<IList<Caja>, IList<Caja>> GetBusinessComponent() {
            return new clsCajaNav();
        }

        protected override void ReloadRelatedConnections() {
            base.ReloadRelatedConnections();
            LibSearchCriteria vCajaCriteria = LibSearchCriteria.CreateCriteria("NombreCaja", NombreCaja);
            vCajaCriteria.Add("ConsecutivoCompania", LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"));
            ConexionNombreCaja = FirstConnectionRecordOrDefault<FkCajaViewModel>("Caja Registradora", vCajaCriteria);
        }

        XElement ConfiguraDatosDeMaquinaFiscalParaConexion() {
            XElement vResult;
            vResult = new XElement("GpData", new XElement("GpResult",
            new XElement("ModeloDeMaquinaFiscal", LibConvert.EnumToDbValue((int)ModeloDeMaquinaFiscal)),
            new XElement("PuertoMaquinaFiscal", LibConvert.EnumToDbValue((int)PuertoMaquinaFiscal)),
            new XElement("TipoConexion", LibConvert.EnumToDbValue((int)TipoConexion)),
            new XElement("RegistroDeRetornoEnTxt", LibConvert.BoolToSN(RegistroDeRetornoEnTxt))));
            return vResult;
        }

        string SerialPortFormat(ePuerto ePuerto) {
            return LibConvert.EnumToDbValue((int)ePuerto);
        }

        private void ExecuteChooseNombreCajaCommand(string valNombreCaja) {
            try {
                if (valNombreCaja == null) {
                    valNombreCaja = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("NombreCaja", valNombreCaja);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("ConsecutivoCompania", LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"));
                ConexionNombreCaja = ChooseRecord<FkCajaViewModel>("Caja Registradora", vDefaultCriteria, vFixedCriteria, string.Empty);
                if (ConexionNombreCaja != null) {
                    NombreCaja = ConexionNombreCaja.NombreCaja;
                } else {
                    NombreCaja = string.Empty;
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        public void MoverFocoSiCambiaTab() {
            MoveFocusIfNecessary();
        }

        protected override bool UpdateRecord() {
            bool vSePuede = true;
            bool vAuditarMF = false;
            IList<Caja> vBusinessObject = new List<Caja>();
            vBusinessObject.Add(Model);
            ICajaPdn vBussinessPdn = new clsCajaNav();
            LibResponse vResult = new LibResponse();

            if (LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "UsaMaquinaFiscal") && SeRequiereClaveEspecial()) {
                vSePuede = SeAutorizaElCambioEnLaConfiguracionDeLaCaja("Reconfigurar");
                vAuditarMF = true;
            }
            if (vSePuede) {
                vResult = vBussinessPdn.ActualizarYAuditarCambiosMF(vBusinessObject, vAuditarMF, MotivoEliminacionOModificacion, LibEnumHelper.GetDescription(FamiliaImpresoraPreConfiguradaAsEnum), LibEnumHelper.GetDescription(ModeloDeMaquinaPreconfiguradaAsEnum), LibEnumHelper.GetDescription(TipoConexionPreconfiguradaAsEnum), SerialDeMaquinaPreconfigurada, UltimoNumeroCompPreconfigurada, UltimoNumeroNCPreconfigurada);
            } else {
                vResult.Success = false;
                LibMessages.MessageBox.Information(this, "No fue autorizado la configuración de la máquina fiscal.", "");
            }
            return vResult.Success;
        }

        private bool HayConexionAInternet() {
            bool vResult = false;
            try {
                using (Ping ping = new Ping()) {
                    PingReply reply = ping.Send("8.8.8.8", 3000); // Se Puede usar cualquier IP conocida, como la de Google DNS
                    vResult = reply.Status == IPStatus.Success;
                }
            } catch {
                vResult = false;
            }
            return vResult;
        }

        private bool MaquinaFiscalEstaHomologada(string valAccionDeAutorizacionDeProceso) {
            string vMensaje = string.Empty;
            ICajaPdn insCaja = new clsCajaNav();
            bool vResul = insCaja.ImpresoraFiscalEstaHomologada(Model.NombreCaja, Model.FamiliaImpresoraFiscalAsString, 
                            Model.ModeloDeMaquinaFiscalAsString, Model.SerialDeMaquinaFiscal,  
                            valAccionDeAutorizacionDeProceso,  ref vMensaje);
            if (!vResul) {
                LibMessages.MessageBox.Alert(this, vMensaje, Title);
            }
            return vResul;
        }

        protected override bool CreateRecord() {
            if (UsaMaquinaFiscal) {
                if (SeAutorizaElCambioEnLaConfiguracionDeLaCaja("Configurar")) {
                    return base.CreateRecord();
                } else {
                    return false;
                }
            } else {
                return base.CreateRecord();
            }
        }

        bool SeAutorizaElCambioEnLaConfiguracionDeLaCaja(string valAccionDeAutorizacionDeProceso) {
            bool vSePuede = false;
            if (LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "UsaMaquinaFiscal")) {
                bool vPedirClaveEspecial = false;
                bool vTengoInternet = HayConexionAInternet();
                string vTextoAdicional = (!vTengoInternet) ? " Nota: No se logró verificar Homologación (SCI)" : string.Empty;
                MotivoEliminacionOModificacion = MotivoEliminacionOModificacion + vTextoAdicional;
                if (vTengoInternet) {
                    vPedirClaveEspecial = MaquinaFiscalEstaHomologada(valAccionDeAutorizacionDeProceso);
                }
                if (vPedirClaveEspecial || !vTengoInternet) {
                    vSePuede = new LibGalac.Ssm.U.LibRequestAdvancedOperation().AuthorizeProcess(valAccionDeAutorizacionDeProceso + " Máquina Fiscal", "VE");
                    if (!vSePuede) {
                        LibMessages.MessageBox.Information(this, "No fue autorizada la configuración de la máquina fiscal.", Title);
                    }
                }
            } else {
                vSePuede = true;
            }
            return vSePuede;
        }
        #endregion
    } //End of class CajaViewModel
} //End of namespace Galac.Adm.Uil.Venta

