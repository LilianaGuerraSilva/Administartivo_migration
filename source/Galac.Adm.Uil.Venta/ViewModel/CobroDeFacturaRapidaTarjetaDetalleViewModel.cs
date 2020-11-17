using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Xml.Linq;
using System.Text.RegularExpressions;
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

namespace Galac.Adm.Uil.Venta.ViewModel {
    public class CobroDeFacturaRapidaTarjetaDetalleViewModel : LibInputDetailViewModelMfc<CobroDeFacturaRapidaTarjetaDetalle> {
        #region Constantes
        public const string CodigoFormaDelCobroPropertyName = "CodigoFormaDelCobro";
        public const string NumeroDelDocumentoPropertyName = "NumeroDelDocumento";
        public const string CodigoBancoPropertyName = "CodigoBanco";
        public const string NombreBancoPropertyName = "NombreBanco";
        public const string MontoPropertyName = "Monto";
        public const string CodigoPuntoDeVentaPropertyName = "CodigoPuntoDeVenta";
        public const string NombreBancoPuntoDeVentaPropertyName = "NombreBancoPuntoDeVenta";
        public const string NumeroDocumentoAprobacionPropertyName = "NumeroDocumentoAprobacion";
        #endregion
        #region Variables
        private FkFormaDelCobroViewModel _ConexionCodigoFormaDelCobro = null;
        private FkBancoViewModel _ConexionCodigoBanco = null;
        private FkBancoViewModel _ConexionCodigoPuntoDeVenta = null;


        int _ConsecutivoCompania;
        string _NumeroFactura;
        string _CodigoFormaDelCobro;
        string _NumeroDelDocumento;
        int _CodigoBanco;
        string _NombreBanco;
        int _CodigoPuntoDeVenta;
        string _NombreBancoPuntoDeVenta;
        decimal _Monto;
        string _NumeroDocumentoAprobacion;

        #endregion //Variables
        #region Propiedades

        public override string ModuleName {
            get { return "Punto de Venta Tarjeta Detalle"; }
        }

        public int ConsecutivoCompania {
            get {
                return _ConsecutivoCompania;
            }
            set {
                if (_ConsecutivoCompania != value) {
                    _ConsecutivoCompania = value;
                }
            }
        }

        public string CodigoFormaDelCobro {
            get {
                return _CodigoFormaDelCobro;
            }
            set {
                if (_CodigoFormaDelCobro != value) {
                    _CodigoFormaDelCobro = value;
                    IsDirty = true;
                    RaisePropertyChanged(CodigoFormaDelCobroPropertyName);
                    if (LibString.IsNullOrEmpty(CodigoFormaDelCobro, true)) {
                        ConexionCodigoFormaDelCobro = null;
                    }
                }
            }
        }

        [LibRequired(ErrorMessage = "El campo Número del Documento es requerido.")]
        [LibGridColum("Nro. de Tarjeta", eGridColumType.Generic, Width = 160, MaxLength = 20)]
        [LibCustomValidation("NumeroTarjetaValidating")]
        //[LibRegularExpression("^[0-9](-*){1,20}", ErrorMessage = "El número de Tarjeta debe ")]
        public string NumeroDelDocumento {
            get {
                return _NumeroDelDocumento;
            }
            set {
                if (_NumeroDelDocumento != value) {
                    _NumeroDelDocumento = value;
                    IsDirty = true;
                    RaisePropertyChanged(NumeroDelDocumentoPropertyName);
                }
            }
        }

        [LibRequired(ErrorMessage = "El campo Código del Banco es requerido.")]
        [LibGridColum("Banco", eGridColumType.Connection, DisplayMemberPath = "NombreBanco", ConnectionDisplayMemberPath = "NombreBanco", ConnectionModelPropertyName = "CodigoBanco", ConnectionSearchCommandName = "ChooseCodigoBancoCommand", Width = 250, MaxLength = 250)]        
        public int CodigoBanco {
            get {
                return _CodigoBanco;
            }
            set {
                if (_CodigoBanco != value) {
                    _CodigoBanco = value;
                    IsDirty = true;
                    RaisePropertyChanged(CodigoBancoPropertyName);
                    if (LibString.IsNullOrEmpty(LibConvert.ToStr(CodigoBanco), true)) {
                        ConexionCodigoBanco = null;
                    }
                }
            }
        }

       // [LibRequired(ErrorMessage = "El campo Banco es requerido.")]
       // [LibGridColum("Banco", eGridColumType.Generic, Width = 250, MaxLength = 250)]
        public string NombreBanco {
            get {
                return _NombreBanco;
            }
            set {
                if (_NombreBanco != value) {
                    _NombreBanco = value;
                    IsDirty = true;
                    RaisePropertyChanged(NombreBancoPropertyName);
                }
            }
        }

        [LibRequired(ErrorMessage = "El campo Código del Banco es requerido.")]
        [LibGridColum("Punto de Venta", eGridColumType.Connection, DisplayMemberPath = "NombreBancoPuntoDeVenta", ConnectionDisplayMemberPath = "NombreBancoPuntoDeVenta", ConnectionModelPropertyName = "CodigoPuntoDeVenta", ConnectionSearchCommandName = "ChooseCodigoPuntoDeVentaCommand", Width = 250, MaxLength = 250)]        

        public int CodigoPuntoDeVenta {
            get {
                return _CodigoPuntoDeVenta;
            }
            set {
                if (_CodigoPuntoDeVenta != value) {
                    _CodigoPuntoDeVenta = value;
                    IsDirty = true;
                    RaisePropertyChanged(CodigoPuntoDeVentaPropertyName);
                    if (LibString.IsNullOrEmpty(LibConvert.ToStr(CodigoPuntoDeVenta), true)) {

                        ConexionCodigoPuntoDeVenta = null;
                    }
                }
            }
        }

        //[LibRequired(ErrorMessage = "El campo Banco del Punto de Venta es requerido.")]
        //[LibGridColum("Punto de Venta", eGridColumType.Generic, Width = 250, MaxLength = 250)]
        public string NombreBancoPuntoDeVenta {
            get {
                return _NombreBancoPuntoDeVenta;
            }
            set {
                if (_NombreBancoPuntoDeVenta != value) {
                    _NombreBancoPuntoDeVenta = value;
                    IsDirty = true;
                    RaisePropertyChanged(NombreBancoPuntoDeVentaPropertyName);
                }
            }
        }

        [LibRequired(ErrorMessage = "El campo Monto es requerido." )]
        [LibCustomValidation("MontoTarjetaValidating")]
        [LibGridColum("Monto", eGridColumType.UDecimal, Width = 120, MaxLength = 80, BindingStringFormat = "N2",Alignment = eTextAlignment.Right)]
        public decimal Monto {
            get {
                return _Monto;
            }
            set {
                if (_Monto != value) {
                    _Monto = value;
                    IsDirty = true;
                    RaisePropertyChanged(MontoPropertyName);
                    
                }
            }
        }

        [LibGridColum("Nro. de Aprobación", eGridColumType.Generic, Width = 180, MaxLength = 20)]
        public string NumeroDocumentoAprobacion {
            get {
                return _NumeroDocumentoAprobacion;
            }
            set {
                if (_NumeroDocumentoAprobacion != value) {
                    _NumeroDocumentoAprobacion = value;
                    IsDirty = true;
                    RaisePropertyChanged(NumeroDocumentoAprobacionPropertyName);
                }
            }
        }

        public CobroDeFacturaRapidaTarjetaViewModel Master {
            get;
            set;
        }

        public FkFormaDelCobroViewModel ConexionCodigoFormaDelCobro {
            get {
                return _ConexionCodigoFormaDelCobro;
            }
            set {
                if (_ConexionCodigoFormaDelCobro != value) {
                    _ConexionCodigoFormaDelCobro = value;
                    RaisePropertyChanged(CodigoFormaDelCobroPropertyName);
                }
                if (_ConexionCodigoFormaDelCobro == null) {
                    CodigoFormaDelCobro = string.Empty;
                }
            }
        }

        public FkBancoViewModel ConexionCodigoBanco {
            get {
                return _ConexionCodigoBanco;
            }
            set {
                if (_ConexionCodigoBanco != value) {
                    _ConexionCodigoBanco = value;
                    RaisePropertyChanged(CodigoBancoPropertyName);
                    if (_ConexionCodigoBanco != null) {
                        CodigoBanco = LibConvert.ToInt(ConexionCodigoBanco.Codigo);
                        NombreBanco = ConexionCodigoBanco.Nombre;
                    }
                }
                if (_ConexionCodigoBanco == null) {
                    CodigoBanco = 0;
                    NombreBanco = string.Empty;
                }
            }
        }

        public FkBancoViewModel ConexionCodigoPuntoDeVenta {
            get {
                return _ConexionCodigoPuntoDeVenta;
            }
            set {
                if (_ConexionCodigoPuntoDeVenta != value) {
                    _ConexionCodigoPuntoDeVenta = value;
                    RaisePropertyChanged(CodigoPuntoDeVentaPropertyName);
                    if (_ConexionCodigoPuntoDeVenta != null) {
                        CodigoPuntoDeVenta = LibConvert.ToInt(ConexionCodigoPuntoDeVenta.Codigo);
                        NombreBancoPuntoDeVenta = ConexionCodigoPuntoDeVenta.Nombre;
                    }
                }
                if (_ConexionCodigoPuntoDeVenta == null) {
                    CodigoPuntoDeVenta = 0;
                    NombreBancoPuntoDeVenta = string.Empty;
                }
            }

        }


        private ValidationResult MontoTarjetaValidating() {
            ValidationResult vResult = ValidationResult.Success;
            //NumeroDelDocumento
            if (Monto > 0) {
                return ValidationResult.Success;
            } else {
                vResult = new ValidationResult("El monto de la tarjeta no puede ser 0 (cero).");
            }
            return vResult;
        }


        private ValidationResult NumeroTarjetaValidating() {
            ValidationResult vResult = ValidationResult.Success;
            bool vEsNumeroValido = true ;
            String Aceptados = "0123456789-*";
            for (int ctr = 0; ctr < NumeroDelDocumento.Length; ctr++) {
                if (Aceptados.Contains(NumeroDelDocumento[ctr])) {
                    vEsNumeroValido = true;
                } else {
                    vEsNumeroValido = false;
                    break;
                }
            }
            if (vEsNumeroValido) {
                vResult = ValidationResult.Success;
            } else {
                vResult = new ValidationResult("El numero de documento");
            }
            return vResult;
        }

        public RelayCommand<string> ChooseCodigoFormaDelCobroCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseCodigoBancoCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseCodigoPuntoDeVentaCommand {
            get;
            private set;
        }
        #endregion //Propiedades
        #region Constructores
        public CobroDeFacturaRapidaTarjetaDetalleViewModel()
            : base(new CobroDeFacturaRapidaTarjetaDetalle(), eAccionSR.Insertar, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
        }
        public CobroDeFacturaRapidaTarjetaDetalleViewModel(CobroDeFacturaRapidaTarjetaViewModel initMaster, CobroDeFacturaRapidaTarjetaDetalle initModel, eAccionSR initAction)
            : base(initModel, initAction, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            Master = initMaster;
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override void InitializeLookAndFeel(CobroDeFacturaRapidaTarjetaDetalle valModel) {
            base.InitializeLookAndFeel(valModel);
            // aqui vamos a pasar por defecto total factura y el total cobrado por parametros
            // samuel 

        }

        protected override ILibBusinessDetailComponent<IList<CobroDeFacturaRapidaTarjetaDetalle>, IList<CobroDeFacturaRapidaTarjetaDetalle>> GetBusinessComponent() {
            return new clsCobroDeFacturaRapidaTarjetaDetalleNav();
        }

        protected override void InitializeCommands() {
            base.InitializeCommands();
            ChooseCodigoFormaDelCobroCommand = new RelayCommand<string>(ExecuteChooseCodigoFormaDelCobroCommand);
            ChooseCodigoBancoCommand = new RelayCommand<string>(ExecuteChooseCodigoBancoCommand);
            ChooseCodigoPuntoDeVentaCommand = new RelayCommand<string>(ExecuteChooseCodigoPuntoDeVentaCommand);
        }

        protected override void ReloadRelatedConnections() {
            base.ReloadRelatedConnections();
           // ConexionCodigoFormaDelCobro = Master.FirstConnectionRecordOrDefault<FkFormaDelCobroViewModel>("Forma Del Cobro", LibSearchCriteria.CreateCriteria("Codigo", CodigoFormaDelCobro));
            ConexionCodigoBanco = Master.FirstConnectionRecordOrDefault<FkBancoViewModel>("Banco", LibSearchCriteria.CreateCriteria("codigo", CodigoBanco));
            ConexionCodigoPuntoDeVenta = Master.FirstConnectionRecordOrDefault<FkBancoViewModel>("Banco", LibSearchCriteria.CreateCriteria("codigo", CodigoPuntoDeVenta));
        }

        internal void RecargarConexiones() {
            ReloadRelatedConnections();
        }

        private void ExecuteChooseCodigoFormaDelCobroCommand(string valCodigo) {
            try {
                if (valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo", valCodigo);
                LibSearchCriteria vFixedCriteria = null;
                #region Codigo Ejemplo
                /* Codigo de Ejemplo
                vFixedCriteria = LibSearchCriteria.CreateCriteria("NombreCampoEnLaTablaConLaQueSeConecta", valorAUsarComoFiltroFijo);
        */
                #endregion //Codigo Ejemplo
                ConexionCodigoFormaDelCobro = Master.ChooseRecord<FkFormaDelCobroViewModel>("Forma Del Cobro", vDefaultCriteria, vFixedCriteria, string.Empty);
                if (ConexionCodigoFormaDelCobro != null) {
                    CodigoFormaDelCobro = ConexionCodigoFormaDelCobro.Codigo;
                } else {
                    CodigoFormaDelCobro = string.Empty;
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void ExecuteChooseCodigoBancoCommand(string valcodigo) {
            try {
                if (valcodigo == null) {
                    valcodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("nombre", valcodigo);
                LibSearchCriteria vFixedCriteria = null;
                ConexionCodigoBanco = Master.ChooseRecord<FkBancoViewModel>("Banco", vDefaultCriteria, vFixedCriteria, string.Empty);
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void ExecuteChooseCodigoPuntoDeVentaCommand(string valcodigo) {
            try {
                if (valcodigo == null) {
                    valcodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("nombre", valcodigo);
                LibSearchCriteria vFixedCriteria = null;
                #region Codigo Ejemplo
                /* Codigo de Ejemplo
                vFixedCriteria = LibSearchCriteria.CreateCriteria("NombreCampoEnLaTablaConLaQueSeConecta", valorAUsarComoFiltroFijo);
        */
                #endregion //Codigo Ejemplo
                ConexionCodigoPuntoDeVenta = Master.ChooseRecord<FkBancoViewModel>("Banco", vDefaultCriteria, vFixedCriteria, string.Empty);
             } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        #endregion //Metodos Generados


    } //End of class CobroDeFacturaRapidaTarjetaDetalleViewModel

} //End of namespace Galac.Adm.Uil.Venta

