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
using Galac.Adm.Brl.DispositivosExternos;
using Galac.Adm.Ccl.DispositivosExternos;
using Galac.Adm.Brl.DispositivosExternos.BalanzaElectronica;
using LibGalac.Aos.UI.Wpf;

namespace Galac.Adm.Uil.DispositivosExternos.ViewModel {
    public class BalanzaViewModel : LibInputViewModelMfc<Balanza> {
        #region Constantes
        public const string ConsecutivoPropertyName = "Consecutivo";
        public const string ModeloPropertyName = "Modelo";
        public const string NombrePropertyName = "Nombre";
        public const string PuertoPropertyName = "Puerto";
        public const string BitsDatosPropertyName = "BitsDatos";
        public const string ParidadPropertyName = "Paridad";
        public const string BitDeParadaPropertyName = "BitDeParada";
        public const string BaudRatePropertyName = "BaudRate";
        public const string ControlDeFlujoPropertyName = "ControlDeFlujo";
        #endregion
        #region Propiedades
      
        eAccionSR eAccion;
        bool ExecuteEnabled;       

        public override string ModuleName {
            get { return "Balanza"; }
        }

        public RelayCommand ProbarConexionCommand {
            get;
            private set;
        }

        public RelayCommand EscogerBalanzaCommand {
            get;
            private set;
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
        [LibGridColum("Consecutivo")]
        public int  Consecutivo {
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
        [LibGridColum("Modelo")]
        public eModeloDeBalanza  Modelo {
            get {
                return Model.ModeloAsEnum;
            }
            set {
                if (Model.ModeloAsEnum != value) {
                    Model.ModeloAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(ModeloPropertyName);
                    ExecuteEnabled = false;
                    ExecuteActionCommand.RaiseCanExecuteChanged();
                    ExecuteActionAndCloseCommand.RaiseCanExecuteChanged();
                }
            }
        }

        [LibGridColum("Nombre", MaxLength=40)]
        [LibRequired(ErrorMessage = "El nombre es requerido.")]
        public string  Nombre {
            get {
                return Model.Nombre;
            }
            set {
                if (Model.Nombre != value) {
                    Model.Nombre = value;
                    IsDirty = true;
                    RaisePropertyChanged(NombrePropertyName);
                }
            }
        }
        [LibGridColum("Puerto")]
        public ePuerto Puerto {
            get {
                return Model.PuertoAsEnum;
            }
            set {
                if(Model.PuertoAsEnum != value) {
                    Model.PuertoAsEnum = value;
                    IsDirty = true;                    
                    RaisePropertyChanged(PuertoPropertyName);
                    ExecuteEnabled = false;
                    ExecuteActionCommand.RaiseCanExecuteChanged();
                    ExecuteActionAndCloseCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public eBitsDeDatos  BitsDatos {
            get {
                return Model.BitsDeDatosAsEnum;
            }
            set {
                if(Model.BitsDeDatosAsEnum != value) {
                    Model.BitsDeDatosAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(BitsDatosPropertyName);
                    ExecuteEnabled = false;
                    ExecuteActionCommand.RaiseCanExecuteChanged();
                    ExecuteActionAndCloseCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public eParidad  Paridad {
            get {
                return Model.ParidadAsEnum;
            }
            set {
                if (Model.ParidadAsEnum != value) {
                    Model.ParidadAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(ParidadPropertyName);
                    ExecuteEnabled = false;
                    ExecuteActionCommand.RaiseCanExecuteChanged();
                    ExecuteActionAndCloseCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public eBitsDeParada  BitDeParada {
            get {
                return Model.BitDeParadaAsEnum;
            }
            set {
                if (Model.BitDeParadaAsEnum != value) {
                    Model.BitDeParadaAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(BitDeParadaPropertyName);
                    ExecuteEnabled = false;
                    ExecuteActionCommand.RaiseCanExecuteChanged();
                    ExecuteActionAndCloseCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public eBaudRate  BaudRate {
            get {
                return Model.BaudRateAsEnum;
            }
            set {
                if (Model.BaudRateAsEnum != value) {
                    Model.BaudRateAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(BaudRatePropertyName);
                    ExecuteEnabled = false;
                    ExecuteActionCommand.RaiseCanExecuteChanged();
                    ExecuteActionAndCloseCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public eControlDeFlujo  ControlDeFlujo {
            get {
                return Model.ControlDeFlujoAsEnum;
            }
            set {
                if (Model.ControlDeFlujoAsEnum != value) {
                    Model.ControlDeFlujoAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(ControlDeFlujoPropertyName);
                    ExecuteEnabled = false;
                    ExecuteActionCommand.RaiseCanExecuteChanged();
                    ExecuteActionAndCloseCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public eModeloDeBalanza[] ArrayModeloBalanza {
            get {
                return LibEnumHelper<eModeloDeBalanza>.GetValuesInArray();
            }
        }

        public ePuerto[] ArrayPuerto {
            get {
                return LibEnumHelper<ePuerto>.GetValuesInArray();
            }
        }

        public eBitsDeDatos[] ArrayBitsDatos {
            get {
                return LibEnumHelper<eBitsDeDatos>.GetValuesInArray();
            }
        }

        public eParidad[] ArrayParidad {
            get {
                return LibEnumHelper<eParidad>.GetValuesInArray();
            }
        }

        public eBitsDeParada[] ArrayBitDeParada {
            get {
                return LibEnumHelper<eBitsDeParada>.GetValuesInArray();
            }
        }

        public eBaudRate[] ArrayBaudRate {
            get {
                return LibEnumHelper<eBaudRate>.GetValuesInArray();
            }
        }

        public eControlDeFlujo[] ArrayControlFlujo {
            get {
                return LibEnumHelper<eControlDeFlujo>.GetValuesInArray();
            }
        }

        protected override bool CanExecuteAction() {
            return ExecuteEnabled;
        }

        protected override bool CanExecuteExecuteActionAndCloseCommand() {
            return ExecuteEnabled;
        }
        
        public bool CanExecuteEscogerrBalanza() {            
                return true;            
        }

        public bool CanExecuteProbarConexion() {
            return true;
        }

        public bool EscogerEnabled {
            get;
            set;
        }

        #endregion //Propiedades
        #region Constructores
        public BalanzaViewModel()
            : this(new Balanza(), eAccionSR.Insertar) {
        }
        public BalanzaViewModel(Balanza initModel, eAccionSR initAction)
            : base(initModel, initAction, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            DefaultFocusedPropertyName = ModeloPropertyName;
            Model.ConsecutivoCompania = Mfc.GetInt("Compania");            
        }

        private string[] GetSerialPort() {            
            string[] vSerialPortsList = new string[] { };
            try {
                vSerialPortsList = new Brl.DispositivosExternos.clsConexionPuertoSerial().ListarPuertos();                
            } catch(GalacException vEx) {                
                throw vEx;
            }
            return vSerialPortsList;
        }

        public override void InitializeViewModel(eAccionSR valAction) {
            base.InitializeViewModel(valAction);
            eAccion = valAction;            
            EscogerEnabled = (eAccion == eAccionSR.Modificar || eAccion == eAccionSR.Insertar);
            InitializeRibbon();            
        }

        #endregion //Constructores
        #region Metodos Generados

        protected override void InitializeLookAndFeel(Balanza valModel) {
            base.InitializeLookAndFeel(valModel);
        }

        protected override void InitializeCommands() {
            base.InitializeCommands();
            EscogerBalanzaCommand = new RelayCommand(ExecuteEscogerCommand,CanExecuteEscogerrBalanza);
            ProbarConexionCommand = new RelayCommand(ExecuteProbarConexionCommand,CanExecuteProbarConexion);
        }

        protected override Balanza FindCurrentRecord(Balanza valModel) {
            if (valModel == null) {
                return null;
            }
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valModel.ConsecutivoCompania);
            vParams.AddInInteger("Consecutivo", valModel.Consecutivo);
            return BusinessComponent.GetData(eProcessMessageType.SpName, "BalanzaGET", vParams.Get()).FirstOrDefault();
        }

        protected override ILibBusinessComponentWithSearch<IList<Balanza>, IList<Balanza>> GetBusinessComponent() {
            return new clsBalanzaNav();
        }
        protected override void InitializeRibbon() {
            base.InitializeRibbon();
            if(RibbonData.TabDataCollection != null && RibbonData.TabDataCollection.Count > 0) {
                if(eAccion == eAccionSR.Escoger) {
                    var vRibbonGroup = RibbonData.GetRibbonGroupData("Acciones");
                    RibbonData.TabDataCollection[0].GroupDataCollection.Remove(vRibbonGroup);
                    RibbonData.TabDataCollection[0].GroupDataCollection.Add(new LibRibbonGroupData("Gestionar"));
                    vRibbonGroup = RibbonData.GetRibbonGroupData("Gestionar");
                    if(vRibbonGroup != null) {
                        vRibbonGroup.ControlDataCollection.Add(CreateAsignarRibbonButtonGroup());                
                    }
                } else if(eAccion == eAccionSR.Insertar || eAccion == eAccionSR.Modificar) {
                    int vTop=RibbonData.TabDataCollection[0].GroupDataCollection[0].ControlDataCollection.Count;
                    RibbonData.TabDataCollection[0].GroupDataCollection[0].ControlDataCollection.Insert(vTop,CreateProbarComunicaionRibbonButtonGroup());                                    
                }
            }
        }

        private LibRibbonButtonData CreateAsignarRibbonButtonGroup() {
            LibRibbonButtonData vResult = new LibRibbonButtonData() {
                Label = "Escoger",                
                Command = EscogerBalanzaCommand,
                LargeImage = new Uri("/LibGalac.Aos.UI.WpfRD;component/Images/exec.png",UriKind.Relative),
                ToolTipDescription = "Guarda los cambios en " + ModuleName + ".",
                ToolTipTitle = "Ejecutar Acción (F4)",
                IsVisible = true,
                KeyTip = "F4"
            };
            return vResult;
        }

        private LibRibbonButtonData CreateProbarComunicaionRibbonButtonGroup() {
            LibRibbonButtonData vResult = new LibRibbonButtonData() {
                Label = "Probar Comunicaión",
                Command = ProbarConexionCommand,
                LargeImage = new Uri("/LibGalac.Aos.UI.WpfRD;component/Images/settings.png",UriKind.Relative),
                ToolTipDescription = "Probar Comunicaión",
                ToolTipTitle = "Probar Comunicaión (F5)",
                IsVisible = true,
                KeyTip = "F5"
            };
            return vResult;
        }      

        public void ExecuteEscogerCommand() {
            int vConsecutivo = Model.Consecutivo;
            clsBalanzaNav insBalanzaNav = new clsBalanzaNav();
            try {
                if(insBalanzaNav.EscogerBalanzaEnPOS(vConsecutivo)) {
                    LibMessages.MessageBox.Information(null,"Balanza seleccionada con exíto","");
                }
            } catch(Exception vEx) {
                LibExceptionDisplay.Show(vEx);
            }
        }      

        public void ExecuteProbarConexionCommand() {            
            clsBalanza insBalanza = new clsBalanzaNav().CreateBalanza(Model.ConsecutivoCompania,Model.Consecutivo);            
            insBalanza.Conexion.SetPort( Model.PuertoAsString);
            try {
                if(insBalanza.VerficarEstado()){
                    LibMessages.MessageBox.Information(null,"Comunicación exitosa","");
                    ExecuteEnabled = true;                    
                    ExecuteActionCommand.RaiseCanExecuteChanged();
                    ExecuteActionAndCloseCommand.RaiseCanExecuteChanged();
                } else {
                    LibMessages.MessageBox.Information(null,"Error de comunicación, Verficar cableados y conexiones","");
                }
            } catch(Exception vEx) {
                LibExceptionDisplay.Show(vEx);
            }
        }
        #endregion //Metodos Generados
    } //End of class BalanzaViewModel

} //End of namespace Galac.Adm.Uil.DispositivosExternos

