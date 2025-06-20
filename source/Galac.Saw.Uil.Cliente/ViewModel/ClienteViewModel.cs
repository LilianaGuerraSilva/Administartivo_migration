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
using Galac.Saw.Brl.Cliente;
using Galac.Saw.Ccl.Cliente;
using Entity = Galac.Saw.Ccl.Cliente;

namespace Galac.Saw.Uil.Cliente.ViewModel {
    public class ClienteViewModel : LibInputMasterViewModelMfc<Entity.Cliente> {
        #region Constantes
        public const string CodigoPropertyName = "Codigo";
        public const string NombrePropertyName = "Nombre";
        public const string NumeroRIFPropertyName = "NumeroRIF";
        public const string NumeroNITPropertyName = "NumeroNIT";
        public const string StatusPropertyName = "Status";
        public const string ContactoPropertyName = "Contacto";
        public const string TelefonoPropertyName = "Telefono";
        public const string FAXPropertyName = "FAX";
        public const string EmailPropertyName = "Email";
        public const string ZonaDeCobranzaPropertyName = "ZonaDeCobranza";
        public const string SectorDeNegocioPropertyName = "SectorDeNegocio";
        public const string NivelDePrecioPropertyName = "NivelDePrecio";
        public const string DireccionPropertyName = "Direccion";
        public const string CiudadPropertyName = "Ciudad";
        public const string ZonaPostalPropertyName = "ZonaPostal";
        public const string CodigoVendedorPropertyName = "CodigoVendedor";
        //public const string NombreVendedorPropertyName = "NombreVendedor";
        public const string RazonInactividadPropertyName = "RazonInactividad";
        public const string ActivarAvisoAlEscogerPropertyName = "ActivarAvisoAlEscoger";
        public const string TextoDelAvisoPropertyName = "TextoDelAviso";
        public const string CuentaContableCxCPropertyName = "CuentaContableCxC";
        public const string DescripcionCuentaContableCxCPropertyName = "DescripcionCuentaContableCxC";
        public const string CuentaContableIngresosPropertyName = "CuentaContableIngresos";
        public const string DescripcionCuentaContableIngresosPropertyName = "DescripcionCuentaContableIngresos";
        public const string CuentaContableAnticipoPropertyName = "CuentaContableAnticipo";
        public const string DescripcionCuentaContableAnticipoPropertyName = "DescripcionCuentaContableAnticipo";
        public const string InfoGalacPropertyName = "InfoGalac";
        public const string CodigoLotePropertyName = "CodigoLote";
        public const string DiaCumpleanosPropertyName = "DiaCumpleanos";
        public const string MesCumpleanosPropertyName = "MesCumpleanos";
        public const string CorrespondenciaXEnviarPropertyName = "CorrespondenciaXEnviar";
        public const string EsExtranjeroPropertyName = "EsExtranjero";
        public const string ClienteDesdeFechaPropertyName = "ClienteDesdeFecha";
        public const string AQueSeDedicaElClientePropertyName = "AQueSeDedicaElCliente";
        public const string TipoDocumentoIdentificacionPropertyName = "TipoDocumentoIdentificacion";
        public const string TipoDeContribuyentePropertyName = "TipoDeContribuyente";
        public const string CampoDefinible1PropertyName = "CampoDefinible1";
        public const string NombreOperadorPropertyName = "NombreOperador";
        public const string FechaUltimaModificacionPropertyName = "FechaUltimaModificacion";
        #endregion
        #region Variables
        private FkZonaCobranzaViewModel _ConexionZonaDeCobranza = null;
        private FkSectorDeNegocioViewModel _ConexionSectorDeNegocio = null;
        private FkCiudadViewModel _ConexionCiudad = null;
        private FkVendedorViewModel _ConexionCodigoVendedor = null;
       //private FkVendedorViewModel _ConexionNombreVendedor = null;
        private FkCuentaViewModel _ConexionCuentaContableCxC = null;
        private FkCuentaViewModel _ConexionDescripcionCuentaContableCxC = null;
        private FkCuentaViewModel _ConexionCuentaContableIngresos = null;
        private FkCuentaViewModel _ConexionDescripcionCuentaContableIngresos = null;
        private FkCuentaViewModel _ConexionCuentaContableAnticipo = null;
        private FkCuentaViewModel _ConexionDescripcionCuentaContableAnticipo = null;
        #endregion //Variables
        #region Propiedades

        public override string ModuleName {
            get { return "Cliente"; }
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

        public int  Consecutivo {
            get {
                return Model.Consecutivo;
            }
            set {
                if (Model.Consecutivo != value) {
                    Model.Consecutivo = value;
                }
            }
        }

        [LibRequired(ErrorMessage = "El campo C?digo es requerido.")]
        [LibGridColum("C?digo", MaxLength=10)]
        public string  Codigo {
            get {
                return Model.Codigo;
            }
            set {
                if (Model.Codigo != value) {
                    Model.Codigo = value;
                    IsDirty = true;
                    RaisePropertyChanged(CodigoPropertyName);
                }
            }
        }

        [LibRequired(ErrorMessage = "El campo Nombre es requerido.")]
        [LibGridColum("Nombre", MaxLength=220)]
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

        [LibGridColum("N? R.I.F.", MaxLength=20)]
        public string  NumeroRIF {
            get {
                return Model.NumeroRIF;
            }
            set {
                if (Model.NumeroRIF != value) {
                    Model.NumeroRIF = value;
                    IsDirty = true;
                    RaisePropertyChanged(NumeroRIFPropertyName);
                }
            }
        }

        public string  NumeroNIT {
            get {
                return Model.NumeroNIT;
            }
            set {
                if (Model.NumeroNIT != value) {
                    Model.NumeroNIT = value;
                    IsDirty = true;
                    RaisePropertyChanged(NumeroNITPropertyName);
                }
            }
        }

        public eStatusCliente  Status {
            get {
                return Model.StatusAsEnum;
            }
            set {
                if (Model.StatusAsEnum != value) {
                    Model.StatusAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(StatusPropertyName);
                }
            }
        }

        public string  Contacto {
            get {
                return Model.Contacto;
            }
            set {
                if (Model.Contacto != value) {
                    Model.Contacto = value;
                    IsDirty = true;
                    RaisePropertyChanged(ContactoPropertyName);
                }
            }
        }

        [LibGridColum("Tel?fonos", MaxLength=40)]
        public string  Telefono {
            get {
                return Model.Telefono;
            }
            set {
                if (Model.Telefono != value) {
                    Model.Telefono = value;
                    IsDirty = true;
                    RaisePropertyChanged(TelefonoPropertyName);
                }
            }
        }

        [LibGridColum("N? Fax", MaxLength=25)]
        public string  FAX {
            get {
                return Model.FAX;
            }
            set {
                if (Model.FAX != value) {
                    Model.FAX = value;
                    IsDirty = true;
                    RaisePropertyChanged(FAXPropertyName);
                }
            }
        }

        public string  Email {
            get {
                return Model.Email;
            }
            set {
                if (Model.Email != value) {
                    Model.Email = value;
                    IsDirty = true;
                    RaisePropertyChanged(EmailPropertyName);
                }
            }
        }

        public string  ZonaDeCobranza {
            get {
                return Model.ZonaDeCobranza;
            }
            set {
                if (Model.ZonaDeCobranza != value) {
                    Model.ZonaDeCobranza = value;
                    IsDirty = true;
                    RaisePropertyChanged(ZonaDeCobranzaPropertyName);
                    if (LibString.IsNullOrEmpty(ZonaDeCobranza, true)) {
                        ConexionZonaDeCobranza = null;
                    }
                }
            }
        }

        public string  SectorDeNegocio {
            get {
                return Model.SectorDeNegocio;
            }
            set {
                if (Model.SectorDeNegocio != value) {
                    Model.SectorDeNegocio = value;
                    IsDirty = true;
                    RaisePropertyChanged(SectorDeNegocioPropertyName);
                    if (LibString.IsNullOrEmpty(SectorDeNegocio, true)) {
                        ConexionSectorDeNegocio = null;
                    }
                }
            }
        }

        public eNivelDePrecio  NivelDePrecio {
            get {
                return Model.NivelDePrecioAsEnum;
            }
            set {
                if (Model.NivelDePrecioAsEnum != value) {
                    Model.NivelDePrecioAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(NivelDePrecioPropertyName);
                }
            }
        }

        public string  Direccion {
            get {
                return Model.Direccion;
            }
            set {
                if (Model.Direccion != value) {
                    Model.Direccion = value;
                    IsDirty = true;
                    RaisePropertyChanged(DireccionPropertyName);
                }
            }
        }

        public string  Ciudad {
            get {
                return Model.Ciudad;
            }
            set {
                if (Model.Ciudad != value) {
                    Model.Ciudad = value;
                    IsDirty = true;
                    RaisePropertyChanged(CiudadPropertyName);
                    if (LibString.IsNullOrEmpty(Ciudad, true)) {
                        ConexionCiudad = null;
                    }
                }
            }
        }

        public string  ZonaPostal {
            get {
                return Model.ZonaPostal;
            }
            set {
                if (Model.ZonaPostal != value) {
                    Model.ZonaPostal = value;
                    IsDirty = true;
                    RaisePropertyChanged(ZonaPostalPropertyName);
                }
            }
        }

        public string  CodigoVendedor {
            get {
                return Model.CodigoVendedor;
            }
            set {
                if (Model.CodigoVendedor != value) {
                    Model.CodigoVendedor = value;
                    IsDirty = true;
                    RaisePropertyChanged(CodigoVendedorPropertyName);
                    if (LibString.IsNullOrEmpty(CodigoVendedor, true)) {
                        ConexionCodigoVendedor = null;
                    }
                }
            }
        }

        public int  ConsecutivoVendedor {
            get {
                return Model.ConsecutivoVendedor;
            }
            set {
                if (Model.ConsecutivoVendedor != value) {
                    Model.ConsecutivoVendedor = value;
                }
            }
        }

        //[LibRequired(ErrorMessage = "El campo Nombre Vendedor es requerido.")]
        //public string  NombreVendedor {
        //    get {
        //        return Model.NombreVendedor;
        //    }
        //    set {
        //        if (Model.NombreVendedor != value) {
        //            Model.NombreVendedor = value;
        //            IsDirty = true;
        //            RaisePropertyChanged(NombreVendedorPropertyName);
        //            if (LibString.IsNullOrEmpty(NombreVendedor, true)) {
        //                ConexionNombreVendedor = null;
        //            }
        //        }
        //    }
        //}

        public string  RazonInactividad {
            get {
                return Model.RazonInactividad;
            }
            set {
                if (Model.RazonInactividad != value) {
                    Model.RazonInactividad = value;
                    IsDirty = true;
                    RaisePropertyChanged(RazonInactividadPropertyName);
                }
            }
        }

        public bool  ActivarAvisoAlEscoger {
            get {
                return Model.ActivarAvisoAlEscogerAsBool;
            }
            set {
                if (Model.ActivarAvisoAlEscogerAsBool != value) {
                    Model.ActivarAvisoAlEscogerAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(ActivarAvisoAlEscogerPropertyName);
                }
            }
        }

        public string  TextoDelAviso {
            get {
                return Model.TextoDelAviso;
            }
            set {
                if (Model.TextoDelAviso != value) {
                    Model.TextoDelAviso = value;
                    IsDirty = true;
                    RaisePropertyChanged(TextoDelAvisoPropertyName);
                }
            }
        }

        public string  CuentaContableCxC {
            get {
                return Model.CuentaContableCxC;
            }
            set {
                if (Model.CuentaContableCxC != value) {
                    Model.CuentaContableCxC = value;
                    IsDirty = true;
                    RaisePropertyChanged(CuentaContableCxCPropertyName);
                    if (LibString.IsNullOrEmpty(CuentaContableCxC, true)) {
                        ConexionCuentaContableCxC = null;
                    }
                }
            }
        }

        //[LibRequired(ErrorMessage = "El campo Descripcion Cuenta Contable Cx C es requerido.")]
        public string  DescripcionCuentaContableCxC {
            get {
                return Model.DescripcionCuentaContableCxC;
            }
            set {
                if (Model.DescripcionCuentaContableCxC != value) {
                    Model.DescripcionCuentaContableCxC = value;
                    IsDirty = true;
                    RaisePropertyChanged(DescripcionCuentaContableCxCPropertyName);
                    if (LibString.IsNullOrEmpty(DescripcionCuentaContableCxC, true)) {
                        ConexionDescripcionCuentaContableCxC = null;
                    }
                }
            }
        }

        public string  CuentaContableIngresos {
            get {
                return Model.CuentaContableIngresos;
            }
            set {
                if (Model.CuentaContableIngresos != value) {
                    Model.CuentaContableIngresos = value;
                    IsDirty = true;
                    RaisePropertyChanged(CuentaContableIngresosPropertyName);
                    if (LibString.IsNullOrEmpty(CuentaContableIngresos, true)) {
                        ConexionCuentaContableIngresos = null;
                    }
                }
            }
        }

        //[LibRequired(ErrorMessage = "El campo Descripcion Cuenta Contable Ingresos es requerido.")]
        public string  DescripcionCuentaContableIngresos {
            get {
                return Model.DescripcionCuentaContableIngresos;
            }
            set {
                if (Model.DescripcionCuentaContableIngresos != value) {
                    Model.DescripcionCuentaContableIngresos = value;
                    IsDirty = true;
                    RaisePropertyChanged(DescripcionCuentaContableIngresosPropertyName);
                    if (LibString.IsNullOrEmpty(DescripcionCuentaContableIngresos, true)) {
                        ConexionDescripcionCuentaContableIngresos = null;
                    }
                }
            }
        }

        public string  CuentaContableAnticipo {
            get {
                return Model.CuentaContableAnticipo;
            }
            set {
                if (Model.CuentaContableAnticipo != value) {
                    Model.CuentaContableAnticipo = value;
                    IsDirty = true;
                    RaisePropertyChanged(CuentaContableAnticipoPropertyName);
                    if (LibString.IsNullOrEmpty(CuentaContableAnticipo, true)) {
                        ConexionCuentaContableAnticipo = null;
                    }
                }
            }
        }

        //[LibRequired(ErrorMessage = "El campo Descripcion Cuenta Contable Anticipo es requerido.")]
        public string  DescripcionCuentaContableAnticipo {
            get {
                return Model.DescripcionCuentaContableAnticipo;
            }
            set {
                if (Model.DescripcionCuentaContableAnticipo != value) {
                    Model.DescripcionCuentaContableAnticipo = value;
                    IsDirty = true;
                    RaisePropertyChanged(DescripcionCuentaContableAnticipoPropertyName);
                    if (LibString.IsNullOrEmpty(DescripcionCuentaContableAnticipo, true)) {
                        ConexionDescripcionCuentaContableAnticipo = null;
                    }
                }
            }
        }

        public eInfoGalacModoEnvio  InfoGalac {
            get {
                return Model.InfoGalacAsEnum;
            }
            set {
                if (Model.InfoGalacAsEnum != value) {
                    Model.InfoGalacAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(InfoGalacPropertyName);
                }
            }
        }

        public string  CodigoLote {
            get {
                return Model.CodigoLote;
            }
            set {
                if (Model.CodigoLote != value) {
                    Model.CodigoLote = value;
                    IsDirty = true;
                    RaisePropertyChanged(CodigoLotePropertyName);
                }
            }
        }

        public eOrigenFacturacionOManual  Origen {
            get {
                return Model.OrigenAsEnum;
            }
            set {
                if (Model.OrigenAsEnum != value) {
                    Model.OrigenAsEnum = value;
                }
            }
        }

        public int  DiaCumpleanos {
            get {
                return Model.DiaCumpleanos;
            }
            set {
                if (Model.DiaCumpleanos != value) {
                    Model.DiaCumpleanos = value;
                    IsDirty = true;
                    RaisePropertyChanged(DiaCumpleanosPropertyName);
                }
            }
        }

        public int  MesCumpleanos {
            get {
                return Model.MesCumpleanos;
            }
            set {
                if (Model.MesCumpleanos != value) {
                    Model.MesCumpleanos = value;
                    IsDirty = true;
                    RaisePropertyChanged(MesCumpleanosPropertyName);
                }
            }
        }

        public bool  CorrespondenciaXEnviar {
            get {
                return Model.CorrespondenciaXEnviarAsBool;
            }
            set {
                if (Model.CorrespondenciaXEnviarAsBool != value) {
                    Model.CorrespondenciaXEnviarAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(CorrespondenciaXEnviarPropertyName);
                }
            }
        }

        public bool  EsExtranjero {
            get {
                return Model.EsExtranjeroAsBool;
            }
            set {
                if (Model.EsExtranjeroAsBool != value) {
                    Model.EsExtranjeroAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(EsExtranjeroPropertyName);
                }
            }
        }

        [LibCustomValidation("ClienteDesdeFechaValidating")]
        public DateTime  ClienteDesdeFecha {
            get {
                return Model.ClienteDesdeFecha;
            }
            set {
                if (Model.ClienteDesdeFecha != value) {
                    Model.ClienteDesdeFecha = value;
                    IsDirty = true;
                    RaisePropertyChanged(ClienteDesdeFechaPropertyName);
                }
            }
        }

        public string  AQueSeDedicaElCliente {
            get {
                return Model.AQueSeDedicaElCliente;
            }
            set {
                if (Model.AQueSeDedicaElCliente != value) {
                    Model.AQueSeDedicaElCliente = value;
                    IsDirty = true;
                    RaisePropertyChanged(AQueSeDedicaElClientePropertyName);
                }
            }
        }

        public eTipoDocumentoIdentificacion  TipoDocumentoIdentificacion {
            get {
                return Model.TipoDocumentoIdentificacionAsEnum;
            }
            set {
                if (Model.TipoDocumentoIdentificacionAsEnum != value) {
                    Model.TipoDocumentoIdentificacionAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(TipoDocumentoIdentificacionPropertyName);
                }
            }
        }

        public eTipoDeContribuyente  TipoDeContribuyente {
            get {
                return Model.TipoDeContribuyenteAsEnum;
            }
            set {
                if (Model.TipoDeContribuyenteAsEnum != value) {
                    Model.TipoDeContribuyenteAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(TipoDeContribuyentePropertyName);
                }
            }
        }

        public string  CampoDefinible1 {
            get {
                return Model.CampoDefinible1;
            }
            set {
                if (Model.CampoDefinible1 != value) {
                    Model.CampoDefinible1 = value;
                    IsDirty = true;
                    RaisePropertyChanged(CampoDefinible1PropertyName);
                }
            }
        }

        public string  NombreOperador {
            get {
                return Model.NombreOperador;
            }
            set {
                if (Model.NombreOperador != value) {
                    Model.NombreOperador = value;
                    IsDirty = true;
                    RaisePropertyChanged(NombreOperadorPropertyName);
                }
            }
        }

        public DateTime  FechaUltimaModificacion {
            get {
                return Model.FechaUltimaModificacion;
            }
            set {
                if (Model.FechaUltimaModificacion != value) {
                    Model.FechaUltimaModificacion = value;
                    IsDirty = true;
                    RaisePropertyChanged(FechaUltimaModificacionPropertyName);
                }
            }
        }

        public eStatusCliente[] ArrayStatusCliente {
            get {
                return LibEnumHelper<eStatusCliente>.GetValuesInArray();
            }
        }

        public eNivelDePrecio[] ArrayNivelDePrecio {
            get {
                return LibEnumHelper<eNivelDePrecio>.GetValuesInArray();
            }
        }

        public eInfoGalacModoEnvio[] ArrayInfoGalacModoEnvio {
            get {
                return LibEnumHelper<eInfoGalacModoEnvio>.GetValuesInArray();
            }
        }

        public eOrigenFacturacionOManual[] ArrayOrigenFacturacionOManual {
            get {
                return LibEnumHelper<eOrigenFacturacionOManual>.GetValuesInArray();
            }
        }

        public eTipoDocumentoIdentificacion[] ArrayTipoDocumentoIdentificacion {
            get {
                return LibEnumHelper<eTipoDocumentoIdentificacion>.GetValuesInArray();
            }
        }

        public eTipoDeContribuyente[] ArrayTipoDeContribuyente {
            get {
                return LibEnumHelper<eTipoDeContribuyente>.GetValuesInArray();
            }
        }

        [LibDetailRequired(ErrorMessage = "Direccion De Despacho es requerido.")]
        public DireccionDeDespachoMngViewModel DetailDireccionDeDespacho {
            get;
            set;
        }

        public FkZonaCobranzaViewModel ConexionZonaDeCobranza {
            get {
                return _ConexionZonaDeCobranza;
            }
            set {
                if (_ConexionZonaDeCobranza != value) {
                    _ConexionZonaDeCobranza = value;
                    RaisePropertyChanged(ZonaDeCobranzaPropertyName);
                }
                if (_ConexionZonaDeCobranza == null) {
                    ZonaDeCobranza = string.Empty;
                }
            }
        }

        public FkSectorDeNegocioViewModel ConexionSectorDeNegocio {
            get {
                return _ConexionSectorDeNegocio;
            }
            set {
                if (_ConexionSectorDeNegocio != value) {
                    _ConexionSectorDeNegocio = value;
                    RaisePropertyChanged(SectorDeNegocioPropertyName);
                }
                if (_ConexionSectorDeNegocio == null) {
                    SectorDeNegocio = string.Empty;
                }
            }
        }

        public FkCiudadViewModel ConexionCiudad {
            get {
                return _ConexionCiudad;
            }
            set {
                if (_ConexionCiudad != value) {
                    _ConexionCiudad = value;
                    RaisePropertyChanged(CiudadPropertyName);
                }
                if (_ConexionCiudad == null) {
                    Ciudad = string.Empty;
                }
            }
        }

        public FkVendedorViewModel ConexionCodigoVendedor {
            get {
                return _ConexionCodigoVendedor;
            }
            set {
                if (_ConexionCodigoVendedor != value) {
                    _ConexionCodigoVendedor = value;
                    RaisePropertyChanged(CodigoVendedorPropertyName);
                }
                if (_ConexionCodigoVendedor == null) {
                    CodigoVendedor = string.Empty;
                }
            }
        }

        //public FkVendedorViewModel ConexionNombreVendedor {
        //    get {
        //        return _ConexionNombreVendedor;
        //    }
        //    set {
        //        if (_ConexionNombreVendedor != value) {
        //            _ConexionNombreVendedor = value;
        //            RaisePropertyChanged(NombreVendedorPropertyName);
        //        }
        //        if (_ConexionNombreVendedor == null) {
        //            NombreVendedor = string.Empty;
        //        }
        //    }
        //}

        public FkCuentaViewModel ConexionCuentaContableCxC {
            get {
                return _ConexionCuentaContableCxC;
            }
            set {
                if (_ConexionCuentaContableCxC != value) {
                    _ConexionCuentaContableCxC = value;
                    RaisePropertyChanged(CuentaContableCxCPropertyName);
                }
                if (_ConexionCuentaContableCxC == null) {
                    CuentaContableCxC = string.Empty;
                }
            }
        }

        public FkCuentaViewModel ConexionDescripcionCuentaContableCxC {
            get {
                return _ConexionDescripcionCuentaContableCxC;
            }
            set {
                if (_ConexionDescripcionCuentaContableCxC != value) {
                    _ConexionDescripcionCuentaContableCxC = value;
                    RaisePropertyChanged(DescripcionCuentaContableCxCPropertyName);
                }
                if (_ConexionDescripcionCuentaContableCxC == null) {
                    DescripcionCuentaContableCxC = string.Empty;
                }
            }
        }

        public FkCuentaViewModel ConexionCuentaContableIngresos {
            get {
                return _ConexionCuentaContableIngresos;
            }
            set {
                if (_ConexionCuentaContableIngresos != value) {
                    _ConexionCuentaContableIngresos = value;
                    RaisePropertyChanged(CuentaContableIngresosPropertyName);
                }
                if (_ConexionCuentaContableIngresos == null) {
                    CuentaContableIngresos = string.Empty;
                }
            }
        }

        public FkCuentaViewModel ConexionDescripcionCuentaContableIngresos {
            get {
                return _ConexionDescripcionCuentaContableIngresos;
            }
            set {
                if (_ConexionDescripcionCuentaContableIngresos != value) {
                    _ConexionDescripcionCuentaContableIngresos = value;
                    RaisePropertyChanged(DescripcionCuentaContableIngresosPropertyName);
                }
                if (_ConexionDescripcionCuentaContableIngresos == null) {
                    DescripcionCuentaContableIngresos = string.Empty;
                }
            }
        }

        public FkCuentaViewModel ConexionCuentaContableAnticipo {
            get {
                return _ConexionCuentaContableAnticipo;
            }
            set {
                if (_ConexionCuentaContableAnticipo != value) {
                    _ConexionCuentaContableAnticipo = value;
                    RaisePropertyChanged(CuentaContableAnticipoPropertyName);
                }
                if (_ConexionCuentaContableAnticipo == null) {
                    CuentaContableAnticipo = string.Empty;
                }
            }
        }

        public FkCuentaViewModel ConexionDescripcionCuentaContableAnticipo {
            get {
                return _ConexionDescripcionCuentaContableAnticipo;
            }
            set {
                if (_ConexionDescripcionCuentaContableAnticipo != value) {
                    _ConexionDescripcionCuentaContableAnticipo = value;
                    RaisePropertyChanged(DescripcionCuentaContableAnticipoPropertyName);
                }
                if (_ConexionDescripcionCuentaContableAnticipo == null) {
                    DescripcionCuentaContableAnticipo = string.Empty;
                }
            }
        }

        public RelayCommand<string> ChooseZonaDeCobranzaCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseSectorDeNegocioCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseCiudadCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseCodigoVendedorCommand {
            get;
            private set;
        }

        //public RelayCommand<string> ChooseNombreVendedorCommand {
        //    get;
        //    private set;
        //}

        public RelayCommand<string> ChooseCuentaContableCxCCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseDescripcionCuentaContableCxCCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseCuentaContableIngresosCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseDescripcionCuentaContableIngresosCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseCuentaContableAnticipoCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseDescripcionCuentaContableAnticipoCommand {
            get;
            private set;
        }

        public RelayCommand<string> CreateDireccionDeDespachoCommand {
            get { return DetailDireccionDeDespacho.CreateCommand; }
        }

        public RelayCommand<string> UpdateDireccionDeDespachoCommand {
            get { return DetailDireccionDeDespacho.UpdateCommand; }
        }

        public RelayCommand<string> DeleteDireccionDeDespachoCommand {
            get { return DetailDireccionDeDespacho.DeleteCommand; }
        }
        #endregion //Propiedades
        #region Constructores
        public ClienteViewModel()
            : this(new Entity.Cliente(), eAccionSR.Insertar) {
        }
        public ClienteViewModel(Entity.Cliente initModel, eAccionSR initAction)
            : base(initModel, initAction, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            DefaultFocusedPropertyName = CodigoPropertyName;
            Model.ConsecutivoCompania = Mfc.GetInt("Compania");
            InitializeDetails();
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override void InitializeLookAndFeel(Entity.Cliente valModel) {
            base.InitializeLookAndFeel(valModel);
            if (LibString.IsNullOrEmpty(Codigo, true)) {
                Codigo = GenerarProximoCodigo();
            }
        }

        protected override Entity.Cliente FindCurrentRecord(Entity.Cliente valModel) {
            if (valModel == null) {
                return null;
            }
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valModel.ConsecutivoCompania);
            vParams.AddInString("Codigo", valModel.Codigo, 10);
            return BusinessComponent.GetData(eProcessMessageType.SpName, "ClienteGET", vParams.Get(), UseDetail).FirstOrDefault();
        }

        protected override ILibBusinessMasterComponentWithSearch<IList<Entity.Cliente>, IList<Entity.Cliente>> GetBusinessComponent() {
            return new clsClienteNav();
        }

        private string GenerarProximoCodigo() {
            string vResult = string.Empty;
            XElement vResulset = GetBusinessComponent().QueryInfo(eProcessMessageType.Message, "ProximoCodigo", Mfc.GetIntAsParam("Compania"), false);
            vResult = LibXml.GetPropertyString(vResulset, "Codigo");
            return vResult;
        }

        protected override void InitializeDetails() {
            DetailDireccionDeDespacho = new DireccionDeDespachoMngViewModel(this, Model.DetailDireccionDeDespacho, Action);
            DetailDireccionDeDespacho.OnCreated += new EventHandler<SearchCollectionChangedEventArgs<DireccionDeDespachoViewModel>>(DetailDireccionDeDespacho_OnCreated);
            DetailDireccionDeDespacho.OnUpdated += new EventHandler<SearchCollectionChangedEventArgs<DireccionDeDespachoViewModel>>(DetailDireccionDeDespacho_OnUpdated);
            DetailDireccionDeDespacho.OnDeleted += new EventHandler<SearchCollectionChangedEventArgs<DireccionDeDespachoViewModel>>(DetailDireccionDeDespacho_OnDeleted);
            DetailDireccionDeDespacho.OnSelectedItemChanged += new EventHandler<SearchCollectionChangedEventArgs<DireccionDeDespachoViewModel>>(DetailDireccionDeDespacho_OnSelectedItemChanged);
        }
        #region DireccionDeDespacho

        private void DetailDireccionDeDespacho_OnSelectedItemChanged(object sender, SearchCollectionChangedEventArgs<DireccionDeDespachoViewModel> e) {
            try {
                UpdateDireccionDeDespachoCommand.RaiseCanExecuteChanged();
                DeleteDireccionDeDespachoCommand.RaiseCanExecuteChanged();
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void DetailDireccionDeDespacho_OnDeleted(object sender, SearchCollectionChangedEventArgs<DireccionDeDespachoViewModel> e) {
            try {
                IsDirty = true;
                Model.DetailDireccionDeDespacho.Remove(e.ViewModel.GetModel());
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void DetailDireccionDeDespacho_OnUpdated(object sender, SearchCollectionChangedEventArgs<DireccionDeDespachoViewModel> e) {
            try {
                IsDirty = e.ViewModel.IsDirty;
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void DetailDireccionDeDespacho_OnCreated(object sender, SearchCollectionChangedEventArgs<DireccionDeDespachoViewModel> e) {
            try {
                Model.DetailDireccionDeDespacho.Add(e.ViewModel.GetModel());
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }
        #endregion //DireccionDeDespacho

        protected override void InitializeCommands() {
            base.InitializeCommands();
            ChooseZonaDeCobranzaCommand = new RelayCommand<string>(ExecuteChooseZonaDeCobranzaCommand);
            ChooseSectorDeNegocioCommand = new RelayCommand<string>(ExecuteChooseSectorDeNegocioCommand);
            ChooseCiudadCommand = new RelayCommand<string>(ExecuteChooseCiudadCommand);
            ChooseCodigoVendedorCommand = new RelayCommand<string>(ExecuteChooseCodigoVendedorCommand);
            //ChooseNombreVendedorCommand = new RelayCommand<string>(ExecuteChooseNombreVendedorCommand);
            ChooseCuentaContableCxCCommand = new RelayCommand<string>(ExecuteChooseCuentaContableCxCCommand);
            ChooseDescripcionCuentaContableCxCCommand = new RelayCommand<string>(ExecuteChooseDescripcionCuentaContableCxCCommand);
            ChooseCuentaContableIngresosCommand = new RelayCommand<string>(ExecuteChooseCuentaContableIngresosCommand);
            ChooseDescripcionCuentaContableIngresosCommand = new RelayCommand<string>(ExecuteChooseDescripcionCuentaContableIngresosCommand);
            ChooseCuentaContableAnticipoCommand = new RelayCommand<string>(ExecuteChooseCuentaContableAnticipoCommand);
            ChooseDescripcionCuentaContableAnticipoCommand = new RelayCommand<string>(ExecuteChooseDescripcionCuentaContableAnticipoCommand);
        }

        protected override void ReloadRelatedConnections() {
            base.ReloadRelatedConnections();
            ConexionZonaDeCobranza = FirstConnectionRecordOrDefault<FkZonaCobranzaViewModel>("Zona Cobranza", LibSearchCriteria.CreateCriteria("Nombre", ZonaDeCobranza));
            ConexionSectorDeNegocio = FirstConnectionRecordOrDefault<FkSectorDeNegocioViewModel>("Sector de Negocio", LibSearchCriteria.CreateCriteria("Descripcion", SectorDeNegocio));
            ConexionCiudad = FirstConnectionRecordOrDefault<FkCiudadViewModel>("Ciudad", LibSearchCriteria.CreateCriteria("NombreCiudad", Ciudad));
            ConexionCodigoVendedor = FirstConnectionRecordOrDefault<FkVendedorViewModel>("Vendedor", LibSearchCriteria.CreateCriteria("Codigo", CodigoVendedor));
            //ConexionNombreVendedor = FirstConnectionRecordOrDefault<FkVendedorViewModel>("Vendedor", LibSearchCriteria.CreateCriteria("Nombre", NombreVendedor));
            ConexionCuentaContableCxC = FirstConnectionRecordOrDefault<FkCuentaViewModel>("Cuenta", LibSearchCriteria.CreateCriteria("Codigo", CuentaContableCxC));
            ConexionDescripcionCuentaContableCxC = FirstConnectionRecordOrDefault<FkCuentaViewModel>("Cuenta", LibSearchCriteria.CreateCriteria("Descripcion", DescripcionCuentaContableCxC));
            ConexionCuentaContableIngresos = FirstConnectionRecordOrDefault<FkCuentaViewModel>("Cuenta", LibSearchCriteria.CreateCriteria("Codigo", CuentaContableIngresos));
            ConexionDescripcionCuentaContableIngresos = FirstConnectionRecordOrDefault<FkCuentaViewModel>("Cuenta", LibSearchCriteria.CreateCriteria("Descripcion", DescripcionCuentaContableIngresos));
            ConexionCuentaContableAnticipo = FirstConnectionRecordOrDefault<FkCuentaViewModel>("Cuenta", LibSearchCriteria.CreateCriteria("Codigo", CuentaContableAnticipo));
            ConexionDescripcionCuentaContableAnticipo = FirstConnectionRecordOrDefault<FkCuentaViewModel>("Cuenta", LibSearchCriteria.CreateCriteria("Descripcion", DescripcionCuentaContableAnticipo));
        }

        private void ExecuteChooseZonaDeCobranzaCommand(string valNombre) {
            try {
                if (valNombre == null) {
                    valNombre = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Nombre", valNombre);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("ConsecutivoCompania", Mfc.GetInt("Compania"));
                ConexionZonaDeCobranza = ChooseRecord<FkZonaCobranzaViewModel>("Zona Cobranza", vDefaultCriteria, vFixedCriteria, string.Empty);
                if (ConexionZonaDeCobranza != null) {
                    ZonaDeCobranza = ConexionZonaDeCobranza.Nombre;
                } else {
                    ZonaDeCobranza = string.Empty;
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void ExecuteChooseSectorDeNegocioCommand(string valDescripcion) {
            try {
                if (valDescripcion == null) {
                    valDescripcion = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Descripcion", valDescripcion);
                LibSearchCriteria vFixedCriteria = null;
        #region Codigo Ejemplo
        /* Codigo de Ejemplo
                vFixedCriteria = LibSearchCriteria.CreateCriteria("NombreCampoEnLaTablaConLaQueSeConecta", valorAUsarComoFiltroFijo);
        */
        #endregion //Codigo Ejemplo
                ConexionSectorDeNegocio = ChooseRecord<FkSectorDeNegocioViewModel>("Sector de Negocio", vDefaultCriteria, vFixedCriteria, string.Empty);
                if (ConexionSectorDeNegocio != null) {
                    SectorDeNegocio = ConexionSectorDeNegocio.Descripcion;
                } else {
                    SectorDeNegocio = string.Empty;
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void ExecuteChooseCiudadCommand(string valNombreCiudad) {
            try {
                if (valNombreCiudad == null) {
                    valNombreCiudad = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("NombreCiudad", valNombreCiudad);
                LibSearchCriteria vFixedCriteria = null;
        #region Codigo Ejemplo
        /* Codigo de Ejemplo
                vFixedCriteria = LibSearchCriteria.CreateCriteria("NombreCampoEnLaTablaConLaQueSeConecta", valorAUsarComoFiltroFijo);
        */
        #endregion //Codigo Ejemplo
                ConexionCiudad = ChooseRecord<FkCiudadViewModel>("Ciudad", vDefaultCriteria, vFixedCriteria, string.Empty);
                if (ConexionCiudad != null) {
                    Ciudad = ConexionCiudad.NombreCiudad;
                } else {
                    Ciudad = string.Empty;
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void ExecuteChooseCodigoVendedorCommand(string valCodigo) {
            try {
                if (valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo", valCodigo);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("ConsecutivoCompania", Mfc.GetInt("Compania"));
                ConexionCodigoVendedor = ChooseRecord<FkVendedorViewModel>("Vendedor", vDefaultCriteria, vFixedCriteria, string.Empty);
                if (ConexionCodigoVendedor != null) {
                    CodigoVendedor = ConexionCodigoVendedor.Codigo;
                   // NombreVendedor = ConexionCodigoVendedor.Nombre;
                } else {
                    CodigoVendedor = string.Empty;
                   // NombreVendedor = string.Empty;
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        //private void ExecuteChooseNombreVendedorCommand(string valNombre) {
        //    try {
        //        if (valNombre == null) {
         //           valNombre = string.Empty;
         //       }
          //      LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Nombre", valNombre);
          //	    LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("ConsecutivoCompania", Mfc.GetInt("Compania"));
           //     ConexionNombreVendedor = ChooseRecord<FkVendedorViewModel>("Vendedor", vDefaultCriteria, vFixedCriteria, string.Empty);
            //    if (ConexionNombreVendedor != null) {
             //       CodigoVendedor = ConexionNombreVendedor.Codigo;
              //      NombreVendedor = ConexionNombreVendedor.Nombre;
               // } else {
                //    CodigoVendedor = string.Empty;
                 //   NombreVendedor = string.Empty;
                //}
            //} catch (System.AccessViolationException) {
             //   throw;
            //} catch (System.Exception vEx) {
             //   LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            //}
        //}

        private void ExecuteChooseCuentaContableCxCCommand(string valCodigo) {
            try {
                if (valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo", valCodigo);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("ConsecutivoPeriodo", Mfc.GetInt("Periodo"));
                ConexionCuentaContableCxC = ChooseRecord<FkCuentaViewModel>("Cuenta", vDefaultCriteria, vFixedCriteria, string.Empty);
                if (ConexionCuentaContableCxC != null) {
                    CuentaContableCxC = ConexionCuentaContableCxC.Codigo;
                    DescripcionCuentaContableCxC = ConexionCuentaContableCxC.Descripcion;
                    CuentaContableIngresos = ConexionCuentaContableCxC.Codigo;
                    DescripcionCuentaContableIngresos = ConexionCuentaContableCxC.Descripcion;
                    CuentaContableAnticipo = ConexionCuentaContableCxC.Codigo;
                    DescripcionCuentaContableAnticipo = ConexionCuentaContableCxC.Descripcion;
                } else {
                    CuentaContableCxC = string.Empty;
                    DescripcionCuentaContableCxC = string.Empty;
                    CuentaContableIngresos = string.Empty;
                    DescripcionCuentaContableIngresos = string.Empty;
                    CuentaContableAnticipo = string.Empty;
                    DescripcionCuentaContableAnticipo = string.Empty;
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void ExecuteChooseDescripcionCuentaContableCxCCommand(string valDescripcion) {
            try {
                if (valDescripcion == null) {
                    valDescripcion = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Descripcion", valDescripcion);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("ConsecutivoPeriodo", Mfc.GetInt("Periodo"));
                ConexionDescripcionCuentaContableCxC = ChooseRecord<FkCuentaViewModel>("Cuenta", vDefaultCriteria, vFixedCriteria, string.Empty);
                if (ConexionDescripcionCuentaContableCxC != null) {
                    CuentaContableCxC = ConexionDescripcionCuentaContableCxC.Codigo;
                    DescripcionCuentaContableCxC = ConexionDescripcionCuentaContableCxC.Descripcion;
                    CuentaContableIngresos = ConexionDescripcionCuentaContableCxC.Codigo;
                    DescripcionCuentaContableIngresos = ConexionDescripcionCuentaContableCxC.Descripcion;
                    CuentaContableAnticipo = ConexionDescripcionCuentaContableCxC.Codigo;
                    DescripcionCuentaContableAnticipo = ConexionDescripcionCuentaContableCxC.Descripcion;
                } else {
                    CuentaContableCxC = string.Empty;
                    DescripcionCuentaContableCxC = string.Empty;
                    CuentaContableIngresos = string.Empty;
                    DescripcionCuentaContableIngresos = string.Empty;
                    CuentaContableAnticipo = string.Empty;
                    DescripcionCuentaContableAnticipo = string.Empty;
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void ExecuteChooseCuentaContableIngresosCommand(string valCodigo) {
            try {
                if (valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo", valCodigo);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("ConsecutivoPeriodo", Mfc.GetInt("Periodo"));
                ConexionCuentaContableIngresos = ChooseRecord<FkCuentaViewModel>("Cuenta", vDefaultCriteria, vFixedCriteria, string.Empty);
                if (ConexionCuentaContableIngresos != null) {
                    CuentaContableCxC = ConexionCuentaContableIngresos.Codigo;
                    DescripcionCuentaContableCxC = ConexionCuentaContableIngresos.Descripcion;
                    CuentaContableIngresos = ConexionCuentaContableIngresos.Codigo;
                    DescripcionCuentaContableIngresos = ConexionCuentaContableIngresos.Descripcion;
                    CuentaContableAnticipo = ConexionCuentaContableIngresos.Codigo;
                    DescripcionCuentaContableAnticipo = ConexionCuentaContableIngresos.Descripcion;
                } else {
                    CuentaContableCxC = string.Empty;
                    DescripcionCuentaContableCxC = string.Empty;
                    CuentaContableIngresos = string.Empty;
                    DescripcionCuentaContableIngresos = string.Empty;
                    CuentaContableAnticipo = string.Empty;
                    DescripcionCuentaContableAnticipo = string.Empty;
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void ExecuteChooseDescripcionCuentaContableIngresosCommand(string valDescripcion) {
            try {
                if (valDescripcion == null) {
                    valDescripcion = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Descripcion", valDescripcion);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("ConsecutivoPeriodo", Mfc.GetInt("Periodo"));
                ConexionDescripcionCuentaContableIngresos = ChooseRecord<FkCuentaViewModel>("Cuenta", vDefaultCriteria, vFixedCriteria, string.Empty);
                if (ConexionDescripcionCuentaContableIngresos != null) {
                    CuentaContableCxC = ConexionDescripcionCuentaContableIngresos.Codigo;
                    DescripcionCuentaContableCxC = ConexionDescripcionCuentaContableIngresos.Descripcion;
                    CuentaContableIngresos = ConexionDescripcionCuentaContableIngresos.Codigo;
                    DescripcionCuentaContableIngresos = ConexionDescripcionCuentaContableIngresos.Descripcion;
                    CuentaContableAnticipo = ConexionDescripcionCuentaContableIngresos.Codigo;
                    DescripcionCuentaContableAnticipo = ConexionDescripcionCuentaContableIngresos.Descripcion;
                } else {
                    CuentaContableCxC = string.Empty;
                    DescripcionCuentaContableCxC = string.Empty;
                    CuentaContableIngresos = string.Empty;
                    DescripcionCuentaContableIngresos = string.Empty;
                    CuentaContableAnticipo = string.Empty;
                    DescripcionCuentaContableAnticipo = string.Empty;
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void ExecuteChooseCuentaContableAnticipoCommand(string valCodigo) {
            try {
                if (valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo", valCodigo);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("ConsecutivoPeriodo", Mfc.GetInt("Periodo"));
                ConexionCuentaContableAnticipo = ChooseRecord<FkCuentaViewModel>("Cuenta", vDefaultCriteria, vFixedCriteria, string.Empty);
                if (ConexionCuentaContableAnticipo != null) {
                    CuentaContableCxC = ConexionCuentaContableAnticipo.Codigo;
                    DescripcionCuentaContableCxC = ConexionCuentaContableAnticipo.Descripcion;
                    CuentaContableIngresos = ConexionCuentaContableAnticipo.Codigo;
                    DescripcionCuentaContableIngresos = ConexionCuentaContableAnticipo.Descripcion;
                    CuentaContableAnticipo = ConexionCuentaContableAnticipo.Codigo;
                    DescripcionCuentaContableAnticipo = ConexionCuentaContableAnticipo.Descripcion;
                } else {
                    CuentaContableCxC = string.Empty;
                    DescripcionCuentaContableCxC = string.Empty;
                    CuentaContableIngresos = string.Empty;
                    DescripcionCuentaContableIngresos = string.Empty;
                    CuentaContableAnticipo = string.Empty;
                    DescripcionCuentaContableAnticipo = string.Empty;
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void ExecuteChooseDescripcionCuentaContableAnticipoCommand(string valDescripcion) {
            try {
                if (valDescripcion == null) {
                    valDescripcion = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Descripcion", valDescripcion);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("ConsecutivoPeriodo", Mfc.GetInt("Periodo"));
                ConexionDescripcionCuentaContableAnticipo = ChooseRecord<FkCuentaViewModel>("Cuenta", vDefaultCriteria, vFixedCriteria, string.Empty);
                if (ConexionDescripcionCuentaContableAnticipo != null) {
                    CuentaContableCxC = ConexionDescripcionCuentaContableAnticipo.Codigo;
                    DescripcionCuentaContableCxC = ConexionDescripcionCuentaContableAnticipo.Descripcion;
                    CuentaContableIngresos = ConexionDescripcionCuentaContableAnticipo.Codigo;
                    DescripcionCuentaContableIngresos = ConexionDescripcionCuentaContableAnticipo.Descripcion;
                    CuentaContableAnticipo = ConexionDescripcionCuentaContableAnticipo.Codigo;
                    DescripcionCuentaContableAnticipo = ConexionDescripcionCuentaContableAnticipo.Descripcion;
                } else {
                    CuentaContableCxC = string.Empty;
                    DescripcionCuentaContableCxC = string.Empty;
                    CuentaContableIngresos = string.Empty;
                    DescripcionCuentaContableIngresos = string.Empty;
                    CuentaContableAnticipo = string.Empty;
                    DescripcionCuentaContableAnticipo = string.Empty;
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private ValidationResult ClienteDesdeFechaValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if ((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else {
                if (LibDefGen.DateIsGreaterThanDateLimitForEnterData(ClienteDesdeFecha, false, Action)) {
                    vResult = new ValidationResult(LibDefGen.TooltipMessageDateRestrictionDemoProgram("Cliente Desde"));
                }
            }
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class ClienteViewModel

} //End of namespace Galac.Saw.Uil.Cliente

