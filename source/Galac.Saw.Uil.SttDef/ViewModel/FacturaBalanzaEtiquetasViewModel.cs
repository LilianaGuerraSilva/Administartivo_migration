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
using Galac.Adm.Uil.Banco.ViewModel;
using LibGalac.Aos.Uil;

namespace Galac.Saw.Uil.SttDef.ViewModel {
    public class FacturaBalanzaEtiquetasViewModel : LibInputViewModelMfc<FacturaBalanzaEtiquetasStt> {
        #region Constantes
        public const string UsaPesoEnCodigoPropertyName = "UsaPesoEnCodigo";
        public const string PrefijoCodigoPesoPropertyName = "PrefijoCodigoPeso";
        public const string NumDigitosCodigoArticuloPesoPropertyName = "NumDigitosCodigoArticuloPeso";
        public const string PosicionCodigoArticuloPesoPropertyName = "PosicionCodigoArticuloPeso";
        public const string NumDigitosPesoPropertyName = "NumDigitosPeso";
        public const string NumDecimalesPesoPropertyName = "NumDecimalesPeso";
        public const string UsaPrecioEnCodigoPropertyName = "UsaPrecioEnCodigo";
        public const string PrefijoCodigoPrecioPropertyName = "PrefijoCodigoPrecio";
        public const string NumDigitosCodigoArticuloPrecioPropertyName = "NumDigitosCodigoArticuloPrecio";
        public const string PosicionCodigoArticuloPrecioPropertyName = "PosicionCodigoArticuloPrecio";
        public const string NumDigitosPrecioPropertyName = "NumDigitosPrecio";
        public const string NumDecimalesPrecioPropertyName = "NumDecimalesPrecio";
        public const string PrecioIncluyeIvaPropertyName = "PrecioIncluyeIva";        
        public const string IsEnabledPesoEnCodigoPropertyName = "IsEnabledPesoEnCodigo";
        public const string IsEnabledPrecioEnCodigoPropertyName = "IsEnabledPrecioEnCodigo";
        public const string IsEnabledPosicionArticuloPesoPropertyName = "IsEnabledPosicionArticuloPeso";
        public const string IsEnabledPosicionArticuloPrecioPropertyName = "IsEnabledPosicionArticuloPrecio";
        public const string NumeroDeCaracteresRestantesCodPesoPropertyName = "NumeroDeCaracteresRestantesCodPeso";
        public const string NumeroDeCaracteresRestantesCodPrecioPropertyName = "NumeroDeCaracteresRestantesCodPrecio";
        public const string ArrayPosicionCodArticuloPesoPropertyName = "ArrayPosicionCodArticuloPeso";
        public const string ArrayPosicionCodArticuloPrecioPropertyName = "ArrayPosicionCodArticuloPrecio";
        public const string CodigoEjemploPesoPropertyName = "CodigoEjemploPeso";
        public const string CodigoEjemploPrecioPropertyName = "CodigoEjemploPrecio";
        public const string CantidadDecimalesInventarioPropertyName = "CantidadDeDecimales";
        public const int MaxCaracteresCodigo = 12;        
        #endregion
        #region Variables        
        private int _NumeroDeCaracteresRestantesCodPeso;
        private int _NumeroDeCaracteresRestantesCodPrecio;
        private eCantidadDeDecimales _CantidadDecimalesPrecioInventario;
        #endregion //Variables
        #region Propiedades
        public bool InitFirstTime { get; set; }
        public override string ModuleName {
            get { return "2.7.- Balanza - Etiqueta"; }
        }
        
        public bool UsaPesoEnCodigo {
            get {
                return Model.UsaPesoEnCodigoAsBool;
            }
            set{
                if (Model.UsaPesoEnCodigoAsBool != value) {
                    Model.UsaPesoEnCodigoAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(UsaPesoEnCodigoPropertyName);
                    RaisePropertyChanged(IsEnabledPesoEnCodigoPropertyName);
                    RaisePropertyChanged(IsEnabledPosicionArticuloPesoPropertyName);
                    if (!Model.UsaPesoEnCodigoAsBool) {
                        LimpiarCamposPeso();
                    }
                }
            }
        }

        [LibCustomValidation("ValidarPrefijoPeso")]
        public string PrefijoCodigoPeso {
            get {
                return Model.PrefijoCodigoPeso;
            }
            set {
                if (Model.PrefijoCodigoPeso != value) {
                    Model.PrefijoCodigoPeso = value;
                    IsDirty = true;
                    RaisePropertyChanged(PrefijoCodigoPesoPropertyName);
                    NumeroDeCaracteresRestantesCodPeso = CalcularCaracteresRestantesPesoPrecio(true);                    
                    RaisePropertyChanged(NumeroDeCaracteresRestantesCodPesoPropertyName);
                    RaisePropertyChanged(ArrayPosicionCodArticuloPesoPropertyName);
                    RaisePropertyChanged(IsEnabledPosicionArticuloPesoPropertyName);
                    CodigoEjemploPeso = ConstruirCodigoEjemploPeso();
                    RaisePropertyChanged(CodigoEjemploPesoPropertyName);                    
                }
            }
        }

        [LibCustomValidation("ValidarDigitosArticuloPeso")]
        public int NumDigitosCodigoArticuloPeso {
            get {
                return Model.NumDigitosCodigoArticuloPeso;
            }
            set {
                if (Model.NumDigitosCodigoArticuloPeso != value) {
                    Model.NumDigitosCodigoArticuloPeso = value;
                    IsDirty = true;
                    RaisePropertyChanged(NumDigitosCodigoArticuloPesoPropertyName);
                    NumeroDeCaracteresRestantesCodPeso = CalcularCaracteresRestantesPesoPrecio(true);
                    RaisePropertyChanged(NumeroDeCaracteresRestantesCodPesoPropertyName);
                    NumDigitosPeso = CalcularNumDigitosPesoPrecio(true);
                    CodigoEjemploPeso = ConstruirCodigoEjemploPeso();
                    RaisePropertyChanged(CodigoEjemploPesoPropertyName);
                    RaisePropertyChanged(ArrayPosicionCodArticuloPesoPropertyName);
                    RaisePropertyChanged(IsEnabledPosicionArticuloPesoPropertyName);
                }
            }
        }

        public string PosicionCodigoArticuloPeso {
            get {
                return Model.PosicionCodigoArticuloPeso.ToString();
            }
            set {
                if (Model.PosicionCodigoArticuloPeso.ToString() != value) {
                    if (LibArray.Contains(ArrayPosicionCodArticuloPeso, value)) {
                        Model.PosicionCodigoArticuloPeso = LibConvert.ToInt(value);
                        IsDirty = true;
                        RaisePropertyChanged(PosicionCodigoArticuloPesoPropertyName);
                        CodigoEjemploPeso = ConstruirCodigoEjemploPeso();
                        RaisePropertyChanged(CodigoEjemploPesoPropertyName);
                    } else {
                        Model.PosicionCodigoArticuloPeso = 0;
                        IsDirty = true;
                        RaisePropertyChanged(PosicionCodigoArticuloPesoPropertyName);
                    }
                }
            }
        }
        
        public int NumDigitosPeso {
            get {
                return Model.NumDigitosPeso;
            }
            set {
                if (Model.NumDigitosPeso != value) {
                    Model.NumDigitosPeso = value;
                    IsDirty = true;
                    RaisePropertyChanged(NumDigitosPesoPropertyName);
                    NumeroDeCaracteresRestantesCodPeso = CalcularCaracteresRestantesPesoPrecio(true);
                    RaisePropertyChanged(NumeroDeCaracteresRestantesCodPesoPropertyName);
                    CodigoEjemploPeso = ConstruirCodigoEjemploPeso();
                    RaisePropertyChanged(CodigoEjemploPesoPropertyName);
                }
            }
        }
        [LibCustomValidation("ValidarDecimalesPeso")]
        public int NumDecimalesPeso {
            get {
                return Model.NumDecimalesPeso;
            }
            set {
                if (Model.NumDecimalesPeso != value) {
                    Model.NumDecimalesPeso = value;
                    IsDirty = true;
                    RaisePropertyChanged(NumDecimalesPesoPropertyName);
                    NumeroDeCaracteresRestantesCodPeso = CalcularCaracteresRestantesPesoPrecio(true);
                    RaisePropertyChanged(NumeroDeCaracteresRestantesCodPesoPropertyName);
                    CodigoEjemploPeso =  ConstruirCodigoEjemploPeso();
                    RaisePropertyChanged(CodigoEjemploPesoPropertyName);
                }
            }
        }

        public bool UsaPrecioEnCodigo {
            get {
                return Model.UsaPrecioEnCodigoAsBool;
            }
            set {
                if (Model.UsaPrecioEnCodigoAsBool != value) {
                    Model.UsaPrecioEnCodigoAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(UsaPrecioEnCodigoPropertyName);
                    RaisePropertyChanged(IsEnabledPrecioEnCodigoPropertyName);
                    RaisePropertyChanged(IsEnabledPosicionArticuloPrecioPropertyName);
                    if (!Model.UsaPrecioEnCodigoAsBool) {
                        LimpiarCamposPrecio();
                    }
                }
            }
        }

        [LibCustomValidation("ValidarPrefijoPrecio")]
        public string PrefijoCodigoPrecio {
            get {
                return Model.PrefijoCodigoPrecio;
            }
            set {
                if (Model.PrefijoCodigoPrecio != value) {
                    Model.PrefijoCodigoPrecio = value;
                    IsDirty = true;
                    RaisePropertyChanged(PrefijoCodigoPrecioPropertyName);                    
                    NumeroDeCaracteresRestantesCodPrecio = CalcularCaracteresRestantesPesoPrecio(false);
                    RaisePropertyChanged(NumeroDeCaracteresRestantesCodPrecioPropertyName);
                    RaisePropertyChanged(ArrayPosicionCodArticuloPrecioPropertyName);
                    RaisePropertyChanged(IsEnabledPosicionArticuloPrecioPropertyName);
                    CodigoEjemploPrecio = ConstruirCodigoEjemploPrecio();
                    RaisePropertyChanged(CodigoEjemploPrecioPropertyName);
                }
            }
        }
        
        [LibCustomValidation("ValidarDigitosArticuloPrecio")]
        public int NumDigitosCodigoArticuloPrecio {
            get {
                return Model.NumDigitosCodigoArticuloPrecio;
            }
            set {
                if (Model.NumDigitosCodigoArticuloPrecio != value) {
                    Model.NumDigitosCodigoArticuloPrecio = value;
                    IsDirty = true;
                    RaisePropertyChanged(NumDigitosCodigoArticuloPrecioPropertyName);                    
                    NumeroDeCaracteresRestantesCodPrecio = CalcularCaracteresRestantesPesoPrecio(false);
                    RaisePropertyChanged(NumeroDeCaracteresRestantesCodPrecioPropertyName);
                    NumDigitosPrecio = CalcularNumDigitosPesoPrecio(false);
                    CodigoEjemploPrecio = ConstruirCodigoEjemploPrecio();
                    RaisePropertyChanged(CodigoEjemploPrecioPropertyName);
                    RaisePropertyChanged(ArrayPosicionCodArticuloPrecioPropertyName);
                    RaisePropertyChanged(IsEnabledPosicionArticuloPrecioPropertyName);
                }
            }
        }

        public string PosicionCodigoArticuloPrecio {
            get {
                return Model.PosicionCodigoArticuloPrecio.ToString();
            }
            set {
                if (Model.PosicionCodigoArticuloPrecio.ToString() != value) {                    
                    if (LibArray.Contains(ArrayPosicionCodArticuloPrecio, value)) {
                        Model.PosicionCodigoArticuloPrecio = LibConvert.ToInt(value);
                        IsDirty = true;
                        RaisePropertyChanged(PosicionCodigoArticuloPrecioPropertyName);
                        CodigoEjemploPeso = ConstruirCodigoEjemploPrecio();
                        RaisePropertyChanged(CodigoEjemploPrecioPropertyName);
                    } else {
                        Model.PosicionCodigoArticuloPrecio = 0;
                        IsDirty = true;
                        RaisePropertyChanged(PosicionCodigoArticuloPrecioPropertyName);
                    }
                }
            }
        }

        public int NumDigitosPrecio {
            get {
                return Model.NumDigitosPrecio;
            }
            set {
                if (Model.NumDigitosPrecio != value) {
                    Model.NumDigitosPrecio = value;
                    IsDirty = true;
                    RaisePropertyChanged(NumDigitosPrecioPropertyName);                    
                    NumeroDeCaracteresRestantesCodPrecio = CalcularCaracteresRestantesPesoPrecio(false);
                    RaisePropertyChanged(NumeroDeCaracteresRestantesCodPrecioPropertyName);
                    CodigoEjemploPrecio = ConstruirCodigoEjemploPrecio();
                    RaisePropertyChanged(CodigoEjemploPrecioPropertyName);
                }
            }
        }
        [LibCustomValidation("ValidarDecimalesPrecio")]
        public int NumDecimalesPrecio {
            get {
                return Model.NumDecimalesPrecio;
            }
            set {
                if (Model.NumDecimalesPrecio != value) {
                    Model.NumDecimalesPrecio = value;
                    IsDirty = true;
                    RaisePropertyChanged(NumDecimalesPrecioPropertyName);                    
                    NumeroDeCaracteresRestantesCodPrecio = CalcularCaracteresRestantesPesoPrecio(false);
                    RaisePropertyChanged(NumeroDeCaracteresRestantesCodPrecioPropertyName);
                    CodigoEjemploPrecio = ConstruirCodigoEjemploPrecio();
                    RaisePropertyChanged(CodigoEjemploPrecioPropertyName);
                }
            }
        }

        public bool  PrecioIncluyeIva {
            get {
                return Model.PrecioIncluyeIvaAsBool;
            }
            set {
                if (Model.PrecioIncluyeIvaAsBool != value) {
                    Model.PrecioIncluyeIvaAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(PrecioIncluyeIvaPropertyName);
                }
            }
        }

        public eTipoDeNivelDePrecios[] ArrayTipoDeNivelDePrecios {
            get {
                return LibEnumHelper<eTipoDeNivelDePrecios>.GetValuesInArray();
            }
        }

        public string[] ArrayPosicionCodArticuloPeso {
            get {
                return CalcularPosicionesDisponibles(LibText.Len(PrefijoCodigoPeso), NumDigitosCodigoArticuloPeso);
            }
        }

        public string[] ArrayPosicionCodArticuloPrecio {
            get {
                return CalcularPosicionesDisponibles(LibText.Len(PrefijoCodigoPrecio), NumDigitosCodigoArticuloPrecio);
            }            
        }

        public bool IsEnabledPesoEnCodigo {
            get {
                return (IsEnabled && UsaPesoEnCodigo);
            }
        }

        public bool IsEnabledPrecioEnCodigo {
            get {
                return (IsEnabled && UsaPrecioEnCodigo);                    
            }
        }

        public int NumeroDeCaracteresRestantesCodPeso {
            get {
                return _NumeroDeCaracteresRestantesCodPeso;
            }
            set {
                if (_NumeroDeCaracteresRestantesCodPeso != value) {
                    _NumeroDeCaracteresRestantesCodPeso = value;
                    IsDirty = true;
                    RaisePropertyChanged(NumeroDeCaracteresRestantesCodPesoPropertyName);
                }
            }
        }

        public int NumeroDeCaracteresRestantesCodPrecio {
            get {
                return _NumeroDeCaracteresRestantesCodPrecio;
            }
            set {
                if (_NumeroDeCaracteresRestantesCodPrecio != value) {
                    _NumeroDeCaracteresRestantesCodPrecio = value;
                    IsDirty = true;
                    RaisePropertyChanged(NumeroDeCaracteresRestantesCodPrecioPropertyName);
                }
            }
        }

        public bool IsEnabledPosicionArticuloPeso {
            get {
                return IsEnabled && SePuedeHabilitarPosicionArticuloPeso();
            }
        }

        public bool IsEnabledPosicionArticuloPrecio {
            get {
                return IsEnabled && SePuedeHabilitarPosicionArticuloPrecio();
            }
        }

        public bool IsEnabledConfiguracionBalanza {
            get { return !UsaImprentaDigital(); }
        }

        public string CodigoEjemploPeso {get;set;}
        public string CodigoEjemploPrecio { get; set; }
        
        #endregion //Propiedades
        #region Constructores
        public FacturaBalanzaEtiquetasViewModel()
            : this(new FacturaBalanzaEtiquetasStt(), eAccionSR.Insertar, true) {
        }
        public FacturaBalanzaEtiquetasViewModel(FacturaBalanzaEtiquetasStt initModel, eAccionSR initAction, bool firstTime)
           : base(initModel, initAction, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            InitFirstTime = firstTime;
            LibMessages.Notification.Register<eCantidadDeDecimales>(this, OnCantidadDecimalesPrecioInventarioChanged);
            NumeroDeCaracteresRestantesCodPeso = CalcularCaracteresRestantesPesoPrecio(true);
            NumeroDeCaracteresRestantesCodPrecio = CalcularCaracteresRestantesPesoPrecio(false);
            CodigoEjemploPeso = ConstruirCodigoEjemploPeso();
            CodigoEjemploPrecio = ConstruirCodigoEjemploPrecio();
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override void InitializeLookAndFeel(FacturaBalanzaEtiquetasStt valModel) {
            base.InitializeLookAndFeel(valModel);
        }

        protected override FacturaBalanzaEtiquetasStt FindCurrentRecord(FacturaBalanzaEtiquetasStt valModel) {
            if (valModel == null) {
                return new FacturaBalanzaEtiquetasStt();
            }
            return valModel;            
        }

        protected override ILibBusinessComponentWithSearch<IList<FacturaBalanzaEtiquetasStt>, IList<FacturaBalanzaEtiquetasStt>> GetBusinessComponent()
        {
            return null;
        }
        
        #endregion //Metodos Generados		

        private int CalcularCaracteresRestantesPesoPrecio(bool valEsPeso) {
            int vCaracteresRestantes = 0;
            if (valEsPeso) {
                vCaracteresRestantes = MaxCaracteresCodigo - (PrefijoCodigoPeso.Length + NumDigitosCodigoArticuloPeso + NumDigitosPeso); 
            } else {
                vCaracteresRestantes = MaxCaracteresCodigo - (PrefijoCodigoPrecio.Length + NumDigitosCodigoArticuloPrecio + NumDigitosPrecio); 
            }
            return vCaracteresRestantes;
        }

        private int CalcularNumDigitosPesoPrecio(bool valEsPeso) {
            int vNumDigitos = 0;
            if (valEsPeso) {
                vNumDigitos = MaxCaracteresCodigo - (PrefijoCodigoPeso.Length + NumDigitosCodigoArticuloPeso);    
            } else {
                vNumDigitos = MaxCaracteresCodigo - (PrefijoCodigoPrecio.Length + NumDigitosCodigoArticuloPrecio);
            }            
            return vNumDigitos;
        }

        private string[] CalcularPosicionesDisponibles(int valLenPrefijo, int valNumDigitosCampo) {
            string [] vArrayPosiciones = new string[2];
            if ((valLenPrefijo > 0) && (valNumDigitosCampo > 0)) {
                vArrayPosiciones[0] = LibConvert.ToStr(valLenPrefijo + 1);
                vArrayPosiciones[1] = LibConvert.ToStr((MaxCaracteresCodigo - valNumDigitosCampo) + 1);
            }
            return vArrayPosiciones;
        }

        private string ConstruirCodigoEjemploPeso() {
            string vPrefijo = "";
            string vCodigoEjemplo = "";
            string vCodigoArticulo = "";
            string vPeso = "";
            const string CaracterReservado = " R";
            if (!UsaPesoEnCodigo) {
                return vCodigoEjemplo;
            }
            if (LibText.Len(PrefijoCodigoPeso) > 0) {
                vPrefijo = PrefijoCodigoPeso;
            }
            if (NumDigitosCodigoArticuloPeso > 0) {
                vCodigoArticulo = LibText.NCar('A', NumDigitosCodigoArticuloPeso);
            }
            if ((NumDigitosPeso > 0) && (NumDecimalesPeso < NumDigitosPeso)) {
                vPeso = LibText.NCar('P', NumDigitosPeso - NumDecimalesPeso) + LibText.NCar('D', NumDecimalesPeso);
            }            
            if ((LibConvert.ToInt(PosicionCodigoArticuloPeso) > 0) && (LibConvert.ToInt(PosicionCodigoArticuloPeso)== (LibText.Len(PrefijoCodigoPeso) + 1))) {
                vCodigoEjemplo = vPrefijo + " " + vCodigoArticulo + " " + vPeso + CaracterReservado;
            } else {
                vCodigoEjemplo = vPrefijo + " " + vPeso + " " + vCodigoArticulo + CaracterReservado;
            }            
            if (LibText.Len(vCodigoEjemplo) <= 3) {
                vCodigoEjemplo = "Ingrese una configuración para ver una previsualización de ejemplo";
            } else if (LibText.Len(vCodigoEjemplo) < MaxCaracteresCodigo + 4) {
                vCodigoEjemplo = vCodigoEjemplo + " - Código incompleto. Debe usar todos los caracteres";
            } else if (LibText.Len(vCodigoEjemplo) > MaxCaracteresCodigo + 4) {
                vCodigoEjemplo = vCodigoEjemplo + " - Error, se excede del máximo de caracteres";
            }
            return vCodigoEjemplo;
        }

        private string ConstruirCodigoEjemploPrecio() {
            string vPrefijo = "";
            string vCodigoEjemplo = "";
            string vCodigoArticulo = "";
            string vPrecio = "";
            const string CaracterReservado = " R";
            if (!UsaPrecioEnCodigo) {
                return vCodigoEjemplo;
            }
            if (LibText.Len(PrefijoCodigoPrecio) > 0) {
                vPrefijo = PrefijoCodigoPrecio;
            }
            if (NumDigitosCodigoArticuloPrecio > 0) {
                vCodigoArticulo = LibText.NCar('A', NumDigitosCodigoArticuloPrecio);
            }
            if ((NumDigitosPrecio > 0) && (NumDecimalesPrecio < NumDigitosPrecio)) {
                vPrecio = LibText.NCar('P', NumDigitosPrecio - NumDecimalesPrecio) + LibText.NCar('D', NumDecimalesPrecio);
            }            
            if ((LibConvert.ToInt(PosicionCodigoArticuloPrecio) > 0) && (LibConvert.ToInt(PosicionCodigoArticuloPrecio) == (LibText.Len(PrefijoCodigoPrecio) + 1))) {
                vCodigoEjemplo = vPrefijo + " " + vCodigoArticulo + " " + vPrecio + CaracterReservado;
            } else {
                vCodigoEjemplo = vPrefijo + " " + vPrecio + " " + vCodigoArticulo + CaracterReservado;
            }            
            if (LibText.Len(vCodigoEjemplo) <= 3) {
                vCodigoEjemplo = "Ingrese una configuración para ver una previsualización de ejemplo";
            } else if (LibText.Len(vCodigoEjemplo) < MaxCaracteresCodigo + 4) { //4= 1 caracter reservado para control y 3 espacios en blanco agregados a la cadena para mejor visualizacion en el ejemplo
                vCodigoEjemplo = vCodigoEjemplo + " - Código incompleto. Debe usar todos los caracteres";
            } else if ((LibText.Len(vCodigoEjemplo) > MaxCaracteresCodigo + 4)) {
                vCodigoEjemplo = vCodigoEjemplo + " - Error, se excede del máximo de caracteres";
            }
            return vCodigoEjemplo;
        }

        private ValidationResult ValidarDigitosArticuloPeso() {
            ValidationResult vResult = ValidationResult.Success;
            if (UsaPesoEnCodigo) {
                if (LibText.IsNullOrEmpty(LibConvert.ToStr(NumDigitosCodigoArticuloPeso)) || NumDigitosCodigoArticuloPeso == 0 ) {
                    vResult = new ValidationResult(this.ModuleName + "->Debe ingresar una cantidad de dígitos para el código del artículo");
                } else if ((MaxCaracteresCodigo - (LibText.Len(PrefijoCodigoPeso) + NumDigitosCodigoArticuloPeso + NumDigitosPeso)) < 0) {
                    vResult = new ValidationResult(this.ModuleName + "-> El código para la lectura de peso en el código de barra no puede exceder de 12 caracteres");
                } else if ((MaxCaracteresCodigo - (LibText.Len(PrefijoCodigoPeso) + NumDigitosCodigoArticuloPeso))<=0) {
                    vResult = new ValidationResult(this.ModuleName + "->La suma de la cantidad de dígitos del código del artículo y el largo del prefijo debe permitir al menos 1 dígito para el peso y no exceder de 12 caracteres");
                }
            }
            return vResult;
        }

        private ValidationResult ValidarDigitosArticuloPrecio() {
            ValidationResult vResult = ValidationResult.Success;
            if (UsaPrecioEnCodigo) {
                if (LibText.IsNullOrEmpty(LibConvert.ToStr(NumDigitosCodigoArticuloPrecio))) {
                    vResult = new ValidationResult(this.ModuleName + "-> Debe ingresar una cantidad de dígitos para el código del artículo");
                } else if ((MaxCaracteresCodigo - (LibText.Len(PrefijoCodigoPrecio) + NumDigitosCodigoArticuloPrecio + NumDigitosPrecio)) < 0) {
                    vResult = new ValidationResult(this.ModuleName + "-> El código para la lectura de precio en el código de barra no puede exceder de 12 caracteres");
                } else if ((MaxCaracteresCodigo - (LibText.Len(PrefijoCodigoPrecio) + NumDigitosCodigoArticuloPrecio)) <= 0) {
                    vResult = new ValidationResult(this.ModuleName + "-> La suma de la cantidad de dígitos del código del artículo y el largo del prefijo debe permitir al menos 1 dígito para el precio y no exceder de 12 caracteres");
                }
            }
            return vResult;
        }

        private ValidationResult ValidarDecimalesPeso() {
            ValidationResult vResult = ValidationResult.Success;
            if (UsaPesoEnCodigo) {
                if (LibText.IsNullOrEmpty(LibConvert.ToStr(NumDecimalesPeso))) {
                    vResult = new ValidationResult(this.ModuleName + "-> Debe ingresar una cantidad de dígitos para el código del artículo");
                } else if (NumDecimalesPeso > GetCantidadDecimalesAsInteger()) {
                    vResult = new ValidationResult(this.ModuleName + "-> El número de decimales del peso no puede ser mayor que el parámetro de cantidad de decimales en la sección 5.1-Inventario");
                } else if (NumDecimalesPeso >= NumDigitosPeso) {
                    vResult = new ValidationResult(this.ModuleName + "-> El número de dígitos de los decimales del peso no puede ser mayor o igual que el número total de dígitos del peso");
                }
            }
            return vResult;
        }

        private ValidationResult ValidarDecimalesPrecio() {
            ValidationResult vResult = ValidationResult.Success;
            if (UsaPrecioEnCodigo) {
                if (LibText.IsNullOrEmpty(LibConvert.ToStr(NumDecimalesPrecio))) {
                    vResult = new ValidationResult(this.ModuleName + "-> Debe ingresar una cantidad de dígitos para el código del artículo");
                } else if (NumDecimalesPrecio > GetCantidadDecimalesAsInteger()) {
                    vResult = new ValidationResult(this.ModuleName + "-> El número de decimales del precio no puede ser mayor que el parámetro de cantidad de decimales en la sección 5.1-Inventario");
                } else if (NumDecimalesPrecio >= NumDigitosPrecio) {
                    vResult = new ValidationResult(this.ModuleName + "-> El número de dígitos de los decimales del precio no puede ser mayor o igual que el número total de dígitos del precio");
                }
            }
            return vResult;
        }

        private void OnCantidadDecimalesPrecioInventarioChanged(NotificationMessage<eCantidadDeDecimales> valMessage) {
            try {
                if (LibString.S1IsEqualToS2(LibConvert.ToStr(valMessage.Notification), CantidadDecimalesInventarioPropertyName)) {
                    _CantidadDecimalesPrecioInventario = valMessage.Content;
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private int GetCantidadDecimalesAsInteger() {
            int vResult = 0;
            switch (_CantidadDecimalesPrecioInventario) {                
                case eCantidadDeDecimales.Dos:
                    vResult = 2;
                    break;
                case eCantidadDeDecimales.Tres:
                    vResult = 3;
                    break;
                case eCantidadDeDecimales.Cuatro:
                    vResult = 4;
                    break;
                default:
                    break;
            }
            return vResult;
        }

        private ValidationResult ValidarPrefijoPeso() {
            ValidationResult vResult = ValidationResult.Success;
            if (UsaPesoEnCodigo) {
                if (LibText.IsNullOrEmpty(PrefijoCodigoPeso)) {
                    vResult = new ValidationResult(this.ModuleName + "-> Debe ingresar un prefijo para esta configuración");
                } else if (PrefijoCodigoPeso == PrefijoCodigoPrecio) {
                    vResult = new ValidationResult(this.ModuleName + "-> Los prefijos de peso y precio deben ser diferentes");
                } else if ((LibText.Len(PrefijoCodigoPeso) != LibText.Len(PrefijoCodigoPrecio)) && (UsaPrecioEnCodigo)) {
                    vResult = ValidarPrefijos();
                } else if ((MaxCaracteresCodigo - (LibText.Len(PrefijoCodigoPeso) + NumDigitosCodigoArticuloPeso + NumDigitosPeso)) < 0) {
                    vResult = new ValidationResult(this.ModuleName + "-> El código para la lectura de peso en el código de barra no puede exceder de 12 caracteres");
                } 
                if (LibText.Left(PrefijoCodigoPeso, 1) == "0") {
                    LibMessages.MessageBox.Warning(this, this.ModuleName + "-> Usar prefijos que inicien en 0 (cero) puede causar problemas en la lectura de los códigos de barra", "Advertencia");
                }                
            }
            return vResult;
        }

        private ValidationResult ValidarPrefijoPrecio() {
            ValidationResult vResult = ValidationResult.Success;
            if (UsaPrecioEnCodigo) {
                if (LibText.IsNullOrEmpty(PrefijoCodigoPrecio)) {
                    vResult = new ValidationResult(this.ModuleName + "-> Debe ingresar un prefijo para esta configuración.");
                } else if (PrefijoCodigoPeso == PrefijoCodigoPrecio) {
                    vResult = new ValidationResult(this.ModuleName + "-> Los prefijos de peso y precio deben ser diferentes.");
                } else if ((LibText.Len(PrefijoCodigoPeso) != LibText.Len(PrefijoCodigoPrecio)) && (UsaPesoEnCodigo)) {
                    vResult = ValidarPrefijos();
                } else if ((MaxCaracteresCodigo - (LibText.Len(PrefijoCodigoPrecio) + NumDigitosCodigoArticuloPrecio + NumDigitosPrecio)) < 0) {
                    vResult = new ValidationResult(this.ModuleName + "-> El código para la lectura de precio en el código de barra no puede exceder de 12 caracteres");
                } 
                if (LibText.Left(PrefijoCodigoPrecio, 1) == "0") {
                    LibMessages.MessageBox.Warning(this, this.ModuleName + "-> Usar prefijos que inicien en 0 (cero) puede causar problemas en la lectura de los códigos de barra.", "Advertencia");
                }
            }
            return vResult;
        }

        private ValidationResult ValidarPrefijos() {
            ValidationResult vResult = ValidationResult.Success;
            if (LibText.S1IsEqualToS2(LibText.Left(PrefijoCodigoPrecio,1), LibText.Left(PrefijoCodigoPeso,1))) {
                vResult = new ValidationResult(this.ModuleName + "-> Los prefijos no pueden iniciar con el mismo caracter cuando el número de dígitos de un prefijo es mayor que el otro.");
            }
            return vResult;
        }

        private void LimpiarCamposPeso() {
            PrefijoCodigoPeso = "";
            NumDigitosCodigoArticuloPeso = 0;
            NumDigitosPeso = 0;
            NumDecimalesPeso = 0;
        }

        private void LimpiarCamposPrecio() {
            PrefijoCodigoPrecio = "";
            NumDigitosCodigoArticuloPrecio = 0;
            NumDigitosPrecio = 0;
            NumDecimalesPrecio = 0;
        }

        private bool SePuedeHabilitarPosicionArticuloPeso() {
            return IsEnabledPesoEnCodigo && LibText.Len(PrefijoCodigoPeso) > 0 && NumDigitosCodigoArticuloPeso > 0;
        }

        private bool SePuedeHabilitarPosicionArticuloPrecio() {
            return IsEnabledPrecioEnCodigo && LibText.Len(PrefijoCodigoPrecio) > 0 && NumDigitosCodigoArticuloPrecio > 0;
        }

        private bool UsaImprentaDigital() {
            return LibConvert.SNToBool(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "UsaImprentaDigital"));
        }
    } //End of class FacturaBalanzaEtiquetasViewModel

} //End of namespace Galac.Saw.Uil.SttDef

