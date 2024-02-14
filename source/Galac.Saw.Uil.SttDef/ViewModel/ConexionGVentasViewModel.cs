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
using LibGalac.Aos.Cnf;
using static Galac.Saw.LibWebConnector.clsSuscripcion;
using System.Text;
using System.Diagnostics.Eventing.Reader;
using System.Security.Cryptography;

namespace Galac.Saw.Uil.SttDef.ViewModel {
    public class ConexionGVentasViewModel: LibGenericViewModel {
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
            get {
                return "Conexión G-Ventas";
            }
        }
        public bool IsDirty {
            get; private set;
        }

        private eAccionSR mAction;

        public string CompaniaActualNombre {
            get;
        }
        public string CompaniaActualRIF {
            get;
        }
        public string InquilinoNombre {
            get;
        }

        public ObservableCollection<string> ListaCompaniaGVentasNombres {
            get; set;
        }
        string _CompaniaGVentasNombres;

        [LibCustomValidation("CompaniaGVentasNombresValidating")]
        public string CompaniaGVentasNombres {
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
        public string SerialConector {
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

        public ObservableCollection<string> ListaUsuariosDeOperaciones {
            get; set;
        }
        string _UsuarioDeOperaciones;
        public string UsuarioDeOperaciones {
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
            try {
                mAction = valAction;
                CompaniaActualNombre = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Compania", "Nombre");
                CompaniaActualRIF = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Compania", "NumeroDeRIF");
                SuscripcionGVentas = new clsSuscripcion().GetCaracteristicaGVentas();
                InquilinoNombre = LibString.IsNullOrEmpty(SuscripcionGVentas.TenantNombre) ? "No se encontró información del inquilino." : SuscripcionGVentas.TenantNombre;
                LlenaListaCompaniaGVentas();
                LlenaListaUsuariosSupervisoresActivos();
            } catch (Exception vEx) {
                InquilinoNombre = "No se encontró información del inquilino.";
                LibGalac.Aos.UI.Wpf.LibExceptionDisplay.Show(vEx);
            }
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
            if (ValidaAlGrabar()) {
                if (MensajeConfirmacion()) {
                    ExecuteEstablecerConexionConGVentas();
                }
            }
        }

        private bool ValidaAlGrabar() {
            bool vResult = true;
            if (LibString.IsNullOrEmpty(SerialConector, true)) {
                vResult = false;
                LibMessages.MessageBox.Alert(this, "El valor del Serial del Conector Web es obligatorio.", ModuleName);
            } else if (LibString.Len(SerialConector) != 36) {
                vResult = false;
                LibMessages.MessageBox.Alert(this, "La longitud del valor proporcionado para Serial del Conector Web no es válida.", ModuleName);
            }           
            if (LibString.IsNullOrEmpty(CompaniaGVentasNombres, true)) {
                vResult = false;
                LibMessages.MessageBox.Alert(this, "La compañia no puede estar en blanco, debe seleccionar una de la lista.", ModuleName);
            }
            return vResult;
        }

        private void ExecuteEstablecerConexionConGVentas() {
            try {
                string[] vCodigos = SuscripcionGVentas.Caracteristicas.Select(p => p.Codigo).ToArray();
                string vParametroSuscripcionGVentas;
                if (vCodigos != null && vCodigos.Count() > 0) {
                    vParametroSuscripcionGVentas = string.Join(";", vCodigos);
                } else {
                    vParametroSuscripcionGVentas = "1000";
                }
                if (((ISettValueByCompanyPdn)new clsSettValueByCompanyNav()).EjecutaConexionConGVentas(LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"), vParametroSuscripcionGVentas, SerialConector, CompaniaGVentasNombres, CompaniaActualRIF, CompaniaActualNombre, UsuarioDeOperaciones)) {
                    LibMessages.MessageBox.Information(this, "Conexión entre Administrativo y G-Ventas realizada con éxito.", ModuleName);
                }
            } catch (GalacException vGx) {
                LibGalac.Aos.UI.Wpf.LibExceptionDisplay.Show(vGx);
            } catch (Exception vEx) {
                LibGalac.Aos.UI.Wpf.LibExceptionDisplay.Show(vEx);
            }
        }
        #endregion //Metodos Generados       

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
            } else if (LibString.Len(SerialConector) != 36) {
                vResult = new ValidationResult("La longitud del valor proporcionado para Serial del Conector Web no es válida.");
            }
            return vResult;
        }

        private ValidationResult CompaniaGVentasNombresValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if (LibString.IsNullOrEmpty(CompaniaGVentasNombres)) {
                vResult = new ValidationResult("La compañia no puede estar en blanco, debe seleccionar una de la lista.");
            }
            return vResult;
        }

        private bool MensajeConfirmacion() {
            bool vResult;
            StringBuilder vMensaje = new StringBuilder();
            int vSeparador = LibString.IndexOf(CompaniaGVentasNombres, '|');
            string vNombre = LibString.SubString(CompaniaGVentasNombres, vSeparador + 2);
            string vRif = LibString.SubString(CompaniaGVentasNombres, 0, vSeparador - 2);
            vMensaje.Append("Los datos de Nombre y RIF de la compañía en el Sistema Administrativo ");
            vMensaje.AppendLine("deben coincidir con los datos de la compañía con la cual va a conectarse en G-Ventas.");
            vMensaje.AppendLine();
            vMensaje.AppendLine("Sistema Administrativo: " + CompaniaActualRIF + " - " + CompaniaActualNombre);
            vMensaje.AppendLine("G-Ventas: " + vRif + " - " + vNombre);
            vMensaje.AppendLine();
            vMensaje.Append("Al establecer la conexión, los datos de la compañía ");
            vMensaje.Append("en G-Ventas serán actualizados ");
            vMensaje.AppendLine("con los datos de la compañía en el Sistema Administrativo.");
            vMensaje.AppendLine();
            vMensaje.Append("¿Desea continuar?");
            vResult = LibMessages.MessageBox.YesNo(this, vMensaje.ToString(), "Conexión con G-Ventas");
            return vResult;
        }
    } //End of class ConexionGVentasViewModel
} //End of namespace Galac..Uil.SttDef

