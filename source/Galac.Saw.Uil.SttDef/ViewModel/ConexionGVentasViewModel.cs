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
using Galac.Saw.Brl.SttDef;
using Galac.Saw.Ccl.SttDef;
using Galac.Saw.Lib;
using Galac.Saw.LibWebConnector;
using System.Threading;
using System.Collections.ObjectModel;

namespace Galac.Saw.Uil.SttDef.ViewModel {
    public class ConexionGVentasViewModel : LibGenericViewModel {
        #region Constantes
        public const string CompaniaActualNombrePropertyName = "CompaniaActualNombre";
        public const string CompaniaActualRIFPropertyName = "CompaniaActualRIF";
        public const string InquilinoNombrePropertyName = "InquilinoNombre";
        public const string CompaniaGVentasNombresPropertyName = "CompaniaGVentasNombres";
        public const string SerialConectorPropertyName = "SerialConector";
        public const string UsuarioDeOperacionesPropertyName = "UsuarioDeOperaciones";
        #endregion
        #region Variables
        clsSuscripcion.DatosSuscripcion SuscripcionGVentas;
        #endregion //Variables
        #region Propiedades
        public override string ModuleName {
            get { return "Conexión G-Ventas"; }
        }
        public bool IsDirty { get; private set; }

        private eAccionSR mAction;

        public string CompaniaActualNombre { get; }
        public string CompaniaActualRIF { get; }
        public string InquilinoNombre { get; }

        public ObservableCollection<string> ListaCompaniaGVentasNombres { get; set; }
        string _CompaniaGVentasNombres;
        public string  CompaniaGVentasNombres {
            get {
                return _CompaniaGVentasNombres;
            }
            set {
                if (_CompaniaGVentasNombres != value) {
                    _CompaniaGVentasNombres = value;
                    IsDirty = true;
                    RaisePropertyChanged(CompaniaGVentasNombresPropertyName);
                }
            }
        }
                
        string _SerialConector;
        [LibCustomValidation("SerialConectorValidating")]
        public string  SerialConector {
            get {
                return _SerialConector;
            }
            set {
                if (_SerialConector != value) {
                    _SerialConector = value;
                    IsDirty = true;
                    RaisePropertyChanged(SerialConectorPropertyName);
                }
            }
        }

        public ObservableCollection<string> ListaUsuariosDeOperaciones { get; set; }
        string _UsuarioDeOperaciones;
        public string  UsuarioDeOperaciones {
            get {
                return _UsuarioDeOperaciones;
            }
            set {
                if (_UsuarioDeOperaciones != value) {
                    _UsuarioDeOperaciones = value;
                    IsDirty = true;
                    RaisePropertyChanged(UsuarioDeOperacionesPropertyName);
                }
            }
        }

        public RelayCommand GuardarCommand {
            get;
            private set;
        }
        #endregion //Propiedades
        #region Constructores
        public ConexionGVentasViewModel(eAccionSR valAction) {
            mAction = valAction;
            CompaniaActualNombre = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Compania", "Nombre");
            CompaniaActualRIF = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Compania", "NumeroDeRIF");

            SuscripcionGVentas = new clsSuscripcion().GetCaracteristicaGVentas();

            InquilinoNombre = LibString.IsNullOrEmpty(SuscripcionGVentas.TenantNombre) ? "No se encontró información del inquilino.": SuscripcionGVentas.TenantNombre;
            LlenaListaCompaniaGVentas();
            LlenaListaUsuariosSupervisoresActivos();
        }
        #endregion //Constructores
        #region Metodos Generados
        protected override void InitializeCommands() {
            base.InitializeCommands();
            GuardarCommand = new RelayCommand(ExecuteGuardarCommand, CanExecuteGuardarCommand);
        }

        protected override void InitializeRibbon() {
            base.InitializeRibbon();
            LibRibbonControlData vRibbonControlSalir = RibbonData.TabDataCollection[0].GroupDataCollection[0].ControlDataCollection[0];
            RibbonData.RemoveRibbonGroup("Acciones");
            if (RibbonData.TabDataCollection != null && RibbonData.TabDataCollection.Count > 0) {
                RibbonData.TabDataCollection[0].AddTabGroupData(CreateAccionesRibbonGroup());
                RibbonData.TabDataCollection[0].GroupDataCollection[0].AddRibbonControlData(vRibbonControlSalir);
            }
        }

        private LibRibbonGroupData CreateAccionesRibbonGroup() {
            LibRibbonGroupData vResult = new LibRibbonGroupData("Acciones");
            vResult.ControlDataCollection.Add(new LibRibbonButtonData() {
                Label = "Guardar",
                Command = GuardarCommand,
                LargeImage = new Uri("/LibGalac.Aos.UI.WpfRD;component/Images/saveAndClose.png", UriKind.Relative),
                ToolTipDescription = "Guardar",
                ToolTipTitle = "Guardar",
                KeyTip = "F6"
            });
            return vResult;
        }

        private bool CanExecuteGuardarCommand() {
            return true;
        }

        private void ExecuteGuardarCommand() {
            ExecuteEstablecerConexionConGVentas();
        }

        private void ExecuteEstablecerConexionConGVentas() {
            string[] vCodigos = SuscripcionGVentas.Caracteristicas.Select(p => p.Codigo).ToArray();
            string vParametroSuscripcionGVentas;
            if (vCodigos != null && vCodigos.Count() > 0) {
                vParametroSuscripcionGVentas = string.Join(";", vCodigos);
            } else {
                vParametroSuscripcionGVentas = "1000";
            }
            ((ISettValueByCompanyPdn)new clsSettValueByCompanyNav()).EjecutaConexionConGVentas(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("Compania", "ConsecutivoCompania"), vParametroSuscripcionGVentas, SerialConector, "");
        }
        #endregion //Metodos Generados

        private string BuscaNombreInquilinoSobreSuscripcion() {
            return "Falta Programar";
        }

        private void LlenaListaCompaniaGVentas() {
            if (LibString.IsNullOrEmpty(SuscripcionGVentas.TenantNombre, true)) {
                ListaCompaniaGVentasNombres = new ObservableCollection<string>();
            } else {
                ListaCompaniaGVentasNombres = new LibWebConnector.clsSuscripcion().GetCompaniaGVentas("");
            }
        }

        private void LlenaListaUsuariosSupervisoresActivos() {
            ListaUsuariosDeOperaciones = ((ISettValueByCompanyPdn)new clsSettValueByCompanyNav()).ListaDeUsuariosSupervisoresActivos();
            UsuarioDeOperaciones = ((CustomIdentity)Thread.CurrentPrincipal.Identity).Login;
        }


        private ValidationResult SerialConectorValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if (LibString.IsNullOrEmpty(SerialConector)) {
                vResult = new ValidationResult("El valor del Serial del Conector Web es obligatorio.");
            }else if (LibString.Len(SerialConector) != 36) {
                vResult = new ValidationResult("La longitud del valor proporcionado para Serial del Conector Web no es válida.");
            }
            return vResult;
        }


    } //End of class ConexionGVentasViewModel

} //End of namespace Galac..Uil.SttDef

