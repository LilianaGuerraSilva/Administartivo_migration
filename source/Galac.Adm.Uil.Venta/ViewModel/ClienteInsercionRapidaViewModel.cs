using System;
using System.ComponentModel.DataAnnotations;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using LibGalac.Aos.DefGen;
using LibGalac.Aos.UI.Mvvm;
using LibGalac.Aos.UI.Mvvm.Command;
using LibGalac.Aos.UI.Mvvm.Helpers;
using LibGalac.Aos.UI.Mvvm.Ribbon;
using LibGalac.Aos.UI.Mvvm.Validation;
using Galac.Adm.Ccl.Venta;
using Galac.Saw.Ccl.Cliente;
using Galac.Adm.Brl.Venta;
using System.Xml;
using LibGalac.Aos.UI.Wpf;
using Galac.Comun.Brl.TablasGen;
using Galac.Comun.Ccl.SttDef;

namespace Galac.Adm.Uil.Venta.ViewModel {
    public class ClienteInsercionRapidaViewModel:LibGenericViewModel {
        #region Constantes
        public const string CodigoPropertyName = "Codigo";
        public const string NombrePropertyName = "Nombre";
        public const string NumeroRIFPropertyName = "NumeroRIF";
        public const string DireccionPropertyName = "Direccion";
        public const string TelefonoPropertyName = "Telefono";
        public const string TipoDeContribuyentePropertyName = "TipoDeContribuyente";
        public const string NombreOperadorPropertyName = "NombreOperador";
        public const string FechaUltimaModificacionPropertyName = "FechaUltimaModificacion";
        public const string PrefijoRifPropertyName = "PrefijoRif";
        private eTipoDocumentoIdentificacion _TipoDocumentoIdentificacion;
        #endregion

        #region Variables
        private string _TipoDocumentoEmpresa = LibDefGen.ProgramInfo.IsCountryPeru() ? "RUC" : "RIF";        
        private string _StatusDocumento;
        private StatuRif _ForeGroundDocumento;
        int _ConsecutivoCompania;
        int _Consecutivo;
        string _Codigo;
        string _Nombre;
        string _NumeroRIF;
        string _Direccion;
        string _Telefono;
        eTipoDeContribuyente _TipoDeContribuyenteAsEnum;
        string _NombreOperador;
        DateTime _FechaUltimaModificacion;
        private string[] _ArrayPrefijoRif;
        string _PrefijoRif;
        #endregion

        #region Propiedades

        public override string ModuleName {
            get { return "Cliente"; }
        }

        public int ConsecutivoCompania {
            get {
                return _ConsecutivoCompania;
            }
            set {
                if(_ConsecutivoCompania != value) {
                    _ConsecutivoCompania = value;
                }
            }
        }

        public int Consecutivo {
            get {
                return _Consecutivo;
            }
            set {
                if(_Consecutivo != value) {
                    _Consecutivo = value;
                }
            }
        }

        //[LibRequired(ErrorMessage = "El campo Código es requerido.")]
        [LibGridColum("Código")]
        public string Codigo {
            get {
                return _Codigo;
            }
            set {
                if(_Codigo != value) {
                    _Codigo = value;
                    RaisePropertyChanged(CodigoPropertyName);
                }
            }
        }

        [LibRequired(ErrorMessage = "El campo Nombre es requerido.")]
        [LibGridColum("Nombre")]
        public string Nombre {
            get {
                return _Nombre;
            }
            set {
                if(_Nombre != value) {
                    _Nombre = value;
                    RaisePropertyChanged(NombrePropertyName);
                }
            }
        }

        [LibCustomValidation("RifRucValidating")]
        public string NumeroRIF {
            get {
                return _NumeroRIF;
            }
            set {
                if(_NumeroRIF != value) {
                    _NumeroRIF = value;

                    RaisePropertyChanged(NumeroRIFPropertyName);
                }
            }
        }

        public string Direccion {
            get {
                return _Direccion;
            }
            set {
                if(_Direccion != value) {
                    _Direccion = value;

                    RaisePropertyChanged(DireccionPropertyName);
                }
            }
        }

        public string Telefono {
            get {
                return _Telefono;
            }
            set {
                if(_Telefono != value) {
                    _Telefono = value;

                    RaisePropertyChanged(TelefonoPropertyName);
                }
            }
        }

        public eTipoDeContribuyente TipoDeContribuyente {
            get {
                return _TipoDeContribuyenteAsEnum;
            }
            set {
                if(_TipoDeContribuyenteAsEnum != value) {
                    _TipoDeContribuyenteAsEnum = value;
                    RaisePropertyChanged(TipoDeContribuyentePropertyName);
                }
            }
        }

        public string StatusDocumento {
            get {
                return _StatusDocumento;
            }
            set {
                if(_StatusDocumento != value) {
                    _StatusDocumento = value;
                    RaisePropertyChanged("StatusDocumento");
                }
            }
        }

        public StatuRif ForeGroundDocumento {
            get {
                return _ForeGroundDocumento;
            }
            set {
                if(_ForeGroundDocumento != value) {
                    _ForeGroundDocumento = value;
                    RaisePropertyChanged("ForeGroundDocumento");
                }
            }
        }

        public string NombreOperador {
            get {
                return _NombreOperador;
            }
            set {
                if(_NombreOperador != value) {
                    _NombreOperador = value;

                    RaisePropertyChanged(NombreOperadorPropertyName);
                }
            }
        }

        public DateTime FechaUltimaModificacion {
            get {
                return _FechaUltimaModificacion;
            }
            set {
                if(_FechaUltimaModificacion != value) {
                    _FechaUltimaModificacion = value;
                    RaisePropertyChanged(FechaUltimaModificacionPropertyName);
                }
            }
        }

        public eTipoDeContribuyente[] ArrayTipoDeContribuyente {
            get {
                return LibEnumHelper<eTipoDeContribuyente>.GetValuesInArray();
            }
        }

        public RelayCommand InsertCommand {
            get;
            private set;
        }

        public RelayCommand ValidarRifWebCommand {
            get;
            private set;
        }

        public enum StatuRif {
            Valido = 0,
            Invalido,
            FalloConexion
        }

        private bool Cerrar {
            get; set;
        }

        public string NombreTiposDocumento {
            get {
                if(LibDefGen.ProgramInfo.IsCountryPeru()) {
                    return "DNI / RUC";
                } else {
                    return "Cédula/RIF";
                }
            }
        }

        public string NombreTipoDocumentoEmpresa {
            get {
                return _TipoDocumentoEmpresa;
            }
            set {
                if(_TipoDocumentoEmpresa != value) {
                    _TipoDocumentoEmpresa = value;
                    RaisePropertyChanged("NombreTipoDocumentoEmpresa");
                }
            }
        }

        public string ContentButton {
            get {
                if(LibDefGen.ProgramInfo.IsCountryPeru()) {
                    return "Validar RUC en la Web";
                } else {
                    return "Validar RIF en la Web";
                }
            }
        }

        public string PrefijoRif {
            get {
                return _PrefijoRif;
            }
            set {
                if(_PrefijoRif != value) {
                    _PrefijoRif = value;
                    RaisePropertyChanged(PrefijoRifPropertyName);
                }
            }
        }

        public string[] ArrayPrefijoRif {
            get {
                return _ArrayPrefijoRif;
            }
        }       
        #endregion //Propiedades

        #region Constructores

        public ClienteInsercionRapidaViewModel()
            : base() {            
            _ConsecutivoCompania = LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania");            
            _ArrayPrefijoRif = new string[] {"V","E","J","G","P","C"};
            PrefijoRif = _ArrayPrefijoRif[0];            
        }
        #endregion //Constructores

        #region Metodos Generados

        internal void InitLookAndFeel(eTipoDocumentoIdentificacion valTipoDocumentoIdentificacion,string valNumeroRIF,string valNombre) {
            _TipoDocumentoIdentificacion = valTipoDocumentoIdentificacion;
            NumeroRIF = valNumeroRIF;
            Nombre = valNombre;
            TipoDeContribuyente = eTipoDeContribuyente.NoContribuyente;
        }

        protected override void InitializeCommands() {
            base.InitializeCommands();
            InsertCommand = new RelayCommand(ExecuteInsertCommand);
            ValidarRifWebCommand = new RelayCommand(ExecuteCommandValidarRifWeb);
        }

        protected override void InitializeRibbon() {
            base.InitializeRibbon();
            RibbonData.ApplicationMenuData = new LibRibbonMenuButtonData() {
                IsVisible = false
            };
            if(RibbonData.TabDataCollection != null && RibbonData.TabDataCollection.Count > 0) {
                RibbonData.TabDataCollection[0].GroupDataCollection[0].ControlDataCollection.Insert(0,CreateActionRibbonButton());
            }
        }

        private LibRibbonButtonData CreateActionRibbonButton() {
            LibRibbonButtonData vResult = new LibRibbonButtonData() {
                Label = "Insertar",
                Command = InsertCommand,
                LargeImage = new Uri("/LibGalac.Aos.UI.WpfRD;component/Images/saveAndClose.png",UriKind.Relative),
                ToolTipDescription = "Guarda los cambios en " + ModuleName + ".",
                ToolTipTitle = "Ejecutar Acción (F6)",
                IsVisible = true,
                KeyTip = "F6"
            };
            return vResult;
        }

        private void ExecuteInsertCommand() {
            try {
                if(!IsValid) {
                    LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(new GalacValidationException(Error),ModuleName,ModuleName);
                    return;
                }
                string refCodigo = "";
                if(LibDefGen.ProgramInfo.IsCountryVenezuela()) {
                    NumeroRIF = PrefijoRif + NumeroRIF;
                }
                IFacturaRapidaPdn vFacturaRapida = new clsFacturaRapidaNav();
                DialogResult = vFacturaRapida.InsertarClienteDesdeFacturaRapida(Nombre,NumeroRIF,Direccion,Telefono,ref refCodigo,_TipoDocumentoIdentificacion).Success;
                if(DialogResult) {
                    Codigo = refCodigo;
                }
                Cerrar = true;
                RaiseRequestCloseEvent();
            } catch(System.AccessViolationException) {
                throw;
            } catch(GalacException vEx) {
                if(vEx.ExceptionManagementType == eExceptionManagementType.Validation) {
                    LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.MessageBox.Warning(null,vEx.Message,"Validación de Consistencia");
                } else {
                    LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
                }
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
            }
        }

        #endregion //Metodos Generados

        #region Métodos Programados

        #region ValidarRifPorWeb
        string ValidarRifWeb(string valRif) {
            LibGalac.Aos.Ccl.IdFiscal.ILibIdFiscalPdn insBrlIdFiscal = new LibGalac.Aos.Brl.IdFiscal.LibIdFiscalNav();
            return insBrlIdFiscal.WebVerification(valRif);
        }

        private void ExecuteCommandValidarRifWeb() {
            string xmlWeb = ValidarRifWeb(NumeroRIF);
            XmlDocument xDoc = new XmlDocument();
            var navigator = new clsDireccionesUbigeoNav();
            try {
                if(NumeroRIF == null) {
                    NumeroRIF = string.Empty;
                }
                xDoc.LoadXml(xmlWeb);
                LibXmlDataParse insParse = new LibXmlDataParse(xDoc);
                Nombre = insParse.GetString(0,"Nombre","");
                string vCodigoUbigeo = insParse.GetString(0,"Ubigeo","");
                Direccion = insParse.GetString(0,"Direccion","");
                if(!LibString.IsNullOrEmpty(vCodigoUbigeo)) {
                    Direccion += Direccion[Direccion.Length - 1].Equals(" ") ? "" : " " + navigator.ObtenerDireccion(vCodigoUbigeo);
                    Direccion = Direccion.ToUpperInvariant();
                }
                bool validadoEnLaWeb = insParse.GetBool(0,"ValidadoEnLaWeb",false);
                bool idFiscalValido = insParse.GetBool(0,"IdFiscalValido",false);
                if(validadoEnLaWeb) {
                    if(idFiscalValido) {
                        StatusDocumento = "RIF Válido";
                        ForeGroundDocumento = StatuRif.Valido;
                        RaiseMoveFocus(TelefonoPropertyName);
                    } else {
                        StatusDocumento = "RIF Inválido";
                        ForeGroundDocumento = StatuRif.Invalido;
                    }
                } else {
                    StatusDocumento = "Falló Conexión";
                    ForeGroundDocumento = StatuRif.FalloConexion;
                }
                RaisePropertyChanged("RifContent");
                RaisePropertyChanged("ForeGroundRif");
            } catch(GalacException gEx) {
                LibExceptionDisplay.Show(gEx,this.Title);
            } catch(Exception vEx) {
                if(vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        public bool IsEnabledValidaRif {
            get {
                return LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("FacturaRapida","ValidaRifEnLaWeb");                
            }
        }

        public bool IsVisibleVenezuela {
            get {
                return LibDefGen.ProgramInfo.IsCountryVenezuela();
            }
        }

        public bool IsVisiblePeru {
            get {
                return !LibDefGen.ProgramInfo.IsCountryVenezuela();
            }
        }

        #endregion

        #region Validaciones
        private ValidationResult RifRucValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if(NumeroRIF == null || NumeroRIF == string.Empty) {
                if(LibDefGen.ProgramInfo.IsCountryPeru()) {
                    return new ValidationResult("El campo DNI/RUC es requerido");
                } else {
                    return new ValidationResult("El campo Cédula/RIF es requerido");
                }
            } else if(LibDefGen.ProgramInfo.IsCountryVenezuela()) {
                RegularExpressionAttribute patron = new RegularExpressionAttribute("[VvEeJjPpGgCc][0-9]{2,12}");
                if(!patron.IsValid(PrefijoRif + NumeroRIF)) {
                    return new ValidationResult("La Cédula o RIF solo puede tener letras en la primera posición del texto. Nota: El conjunto de letras válidas es (EGJNPVZ)");
                }
            } else if(LibDefGen.ProgramInfo.IsCountryPeru()) {
                RegularExpressionAttribute patron = new RegularExpressionAttribute("[0-9]{2,12}");
                if(!patron.IsValid(NumeroRIF)) {
                    return new ValidationResult("El DNI o RUC solo puede tener números.");
                }
            }
            return vResult;
        }
        #endregion
        #endregion
    } //End of class ClienteInsercionRapidaViewModel

} //End of namespace Galac.Adm.Uil.Venta

