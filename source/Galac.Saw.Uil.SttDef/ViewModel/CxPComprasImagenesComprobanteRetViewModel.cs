using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using LibGalac.Aos.UI.Mvvm;
using LibGalac.Aos.UI.Mvvm.Command;
using LibGalac.Aos.UI.Mvvm.Validation;
using Galac.Saw.Ccl.SttDef;
using Microsoft.Win32;
using System.IO;

namespace Galac.Saw.Uil.SttDef.ViewModel {
    public class CxPComprasImagenesComprobanteRetViewModel:LibInputViewModelMfc<ImagenesComprobantesRetencionStt> {
        #region Constantes
        public const string NombreFirmaPropertyName = "NombreFirma";
        public const string NombreSelloPropertyName = "NombreSello";
        public const string NombreLogoPropertyName = "NombreLogo";
        #endregion

        private string _LogosPath;

        #region Propiedades

        public override string ModuleName {
            get { return "6.6.- Imágenes para Comprobantes"; }
        }

        [LibCustomValidation("NombreFirmaValidating")]
        public string NombreFirma {
            get {
                return Model.NombreFirma;
            }
            set {
                if(Model.NombreFirma != value) {
                    Model.NombreFirma = value;
                    IsDirty = true;
                    RaisePropertyChanged(NombreFirmaPropertyName);
                }
            }
        }

        [LibCustomValidation("NombreLogoValidating")]
        public string NombreLogo {
            get {
                return Model.NombreLogo;
            }
            set {
                if(Model.NombreLogo != value) {
                    Model.NombreLogo = value;
                    IsDirty = true;
                    RaisePropertyChanged(NombreLogoPropertyName);
                }
            }
        }

        [LibCustomValidation("NombreSelloValidating")]
        public string NombreSello {
            get {
                return Model.NombreSello;
            }
            set {
                if(Model.NombreSello != value) {
                    Model.NombreSello = value;
                    IsDirty = true;
                    RaisePropertyChanged(NombreSelloPropertyName);
                }
            }
        }

        public RelayCommand ChooseNombreSelloCommand {
            get;
            private set;
        }
        public RelayCommand ChooseNombreLogoCommand {
            get;
            private set;
        }

        public RelayCommand ChooseNombreFirmaCommand {
            get;
            private set;
        }


        public RelayCommand ChooseBorrarNombreSelloCommand {
            get;
            private set;
        }
        public RelayCommand ChooseBorrarNombreLogoCommand {
            get;
            private set;
        }

        public RelayCommand ChooseBorrarNombreFirmaCommand {
            get;
            private set;
        }
        #endregion //Propiedades      
        #region Constructores
        public CxPComprasImagenesComprobanteRetViewModel()
            : this(new ImagenesComprobantesRetencionStt(),eAccionSR.Insertar) {
        }
        public CxPComprasImagenesComprobanteRetViewModel(ImagenesComprobantesRetencionStt initModel,eAccionSR initAction)
            : base(initModel,initAction,LibGlobalValues.Instance.GetAppMemInfo(),LibGlobalValues.Instance.GetMfcInfo()) {
            DefaultFocusedPropertyName = NombreFirmaPropertyName;
            //Model.ConsecutivoCompania = Mfc.GetInt("Compania");
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override void InitializeCommands() {
            ChooseNombreSelloCommand = new RelayCommand(ExecuteNombreSelloCommand);
            ChooseNombreLogoCommand = new RelayCommand(ExecuteNombreLogoCommand);
            ChooseNombreFirmaCommand = new RelayCommand(ExecuteNombreFirmaCommand);
            ChooseBorrarNombreFirmaCommand = new RelayCommand(ExecuteBorrarNombreFirmaCommand);
            ChooseBorrarNombreLogoCommand = new RelayCommand(ExecuteBorrarNombreLogoCommand);
            ChooseBorrarNombreSelloCommand = new RelayCommand(ExecuteBorrarNombreSelloCommand);
        }

        protected override void InitializeLookAndFeel(ImagenesComprobantesRetencionStt valModel) {
            base.InitializeLookAndFeel(valModel);
            _LogosPath = LibString.ToTitleCase(System.IO.Path.Combine(LibWorkPaths.LogicUnitDir,"Logos"));
        }

        protected override ImagenesComprobantesRetencionStt FindCurrentRecord(ImagenesComprobantesRetencionStt valModel) {
            if(valModel == null) {
                return new ImagenesComprobantesRetencionStt();
            }
            return valModel;
        }

        protected override ILibBusinessComponentWithSearch<IList<ImagenesComprobantesRetencionStt>,IList<ImagenesComprobantesRetencionStt>> GetBusinessComponent() {
            return null;
        }

        private void ExecuteNombreSelloCommand() {
            try {
                string vRuta = "";
                if(LibDirectory.DirExists(_LogosPath)) {
                    vRuta = BuscarNombreImagen();
                    if(!LibString.IsNullOrEmpty(vRuta)) {
                        NombreSello = vRuta;
                    }
                } else {
                    throw new GalacException($"El directorio predeterminado {_LogosPath} no existe, por favor debe crearlo en la ruta indicada",eExceptionManagementType.Alert);
                }
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx,ModuleName);
            }
        }

        private void ExecuteNombreLogoCommand() {
            try {
                string vRuta = "";
                if(LibDirectory.DirExists(_LogosPath)) {
                    vRuta = BuscarNombreImagen();
                    if(!LibString.IsNullOrEmpty(vRuta)) {
                        NombreLogo = vRuta;
                    } else {
                        throw new GalacException($"El directorio predeterminado {_LogosPath} no existe, por favor debe crearlo en la ruta indicada",eExceptionManagementType.Alert);
                    }
                }
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx,ModuleName);
            }
        }

        private void ExecuteNombreFirmaCommand() {
            try {
                string vRuta = "";
                if(LibDirectory.DirExists(_LogosPath)) {
                    vRuta = BuscarNombreImagen();
                    if(!LibString.IsNullOrEmpty(vRuta)) {
                        NombreFirma = vRuta;
                    }
                } else {
                    throw new GalacException($"El directorio predeterminado {_LogosPath} no existe, por favor debe crearlo en la ruta indicada",eExceptionManagementType.Alert);
                }
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx,ModuleName);
            }
        }

        private void ExecuteBorrarNombreFirmaCommand() {
            NombreFirma = string.Empty;
        }

        private void ExecuteBorrarNombreLogoCommand() {
            NombreLogo = string.Empty;
        }

        private void ExecuteBorrarNombreSelloCommand() {
            NombreSello = string.Empty;
        }

        private string BuscarNombreImagen() {
            string vResult = "";
            const char _BackSlash = '\u005c';
            const char _Point = '\u002e';
            OpenFileDialog vOpenFileDialog = new OpenFileDialog();
            string vSelectedPath = "";
            vOpenFileDialog.AddExtension = false;
            vOpenFileDialog.ReadOnlyChecked = true;
            vOpenFileDialog.Multiselect = false;
            vOpenFileDialog.Filter = "Imagenes (*.jpg)|*.jpg";
            vOpenFileDialog.InitialDirectory = _LogosPath;
            bool vSeleccionoImagen = (bool)vOpenFileDialog.ShowDialog();
            if(vSeleccionoImagen) {
                vSelectedPath = vOpenFileDialog.FileName;
                vSelectedPath = LibString.ToTitleCase(LibString.SubString(vSelectedPath,0,vSelectedPath.LastIndexOf(_BackSlash)));
                if(vSelectedPath != _LogosPath) {
                    throw new GalacException($"El directorio seleccionado no es válido, debe seleccionar el directorio predeterminado {_LogosPath}",eExceptionManagementType.Alert);
                }
                vResult = vOpenFileDialog.SafeFileName;
                vResult = LibString.SubString(vResult,0,vResult.LastIndexOf(_Point));
            } else {
                vResult = string.Empty;
            }
            return vResult;
        }

        internal static bool EsValidoNombreImagen(string valRutaLogo,string valNameImagenes) {
            valRutaLogo = Path.Combine(valRutaLogo,valNameImagenes) + ".jpg";
            return File.Exists(valRutaLogo);
        }

        private ValidationResult NombreLogoValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else {
                if(!LibString.IsNullOrEmpty(NombreLogo) && !EsValidoNombreImagen(_LogosPath,NombreLogo)) {
                    vResult = new ValidationResult($"La imágen {  NombreLogo  }, No existe en el directorio predeterminado {_LogosPath}");
                }
            }
            return vResult;
        }

        private ValidationResult NombreFirmaValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else {
                if(!LibString.IsNullOrEmpty(NombreFirma) && !EsValidoNombreImagen(_LogosPath,NombreFirma)) {
                    vResult = new ValidationResult($"La imágen {  NombreFirma  }, No existe en el directorio predeterminado {_LogosPath}");
                }
            }
            return vResult;
        }


        private ValidationResult NombreSelloValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else {
                if(!LibString.IsNullOrEmpty(NombreSello) && !EsValidoNombreImagen(_LogosPath,NombreSello)) {
                    vResult = new ValidationResult($"La imágen {  NombreSello  }, No existe en el directorio predeterminado {_LogosPath}");
                }
            }
            return vResult;
        }
        #endregion //Metodos Generados        
    } //End of class ImagenesComprobantesRetencionStt
} //End of namespace Galac.Saw.Uil.SttDef

