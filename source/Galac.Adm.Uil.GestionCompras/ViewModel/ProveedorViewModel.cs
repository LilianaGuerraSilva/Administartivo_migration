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
using Galac.Adm.Brl.GestionCompras;
using Galac.Adm.Ccl.GestionCompras;
using Galac.Comun.Ccl.TablasLey;
using System.Xml;
using LibGalac.Aos.UI.Wpf;
using Galac.Adm.Uil.GestionCompras.Properties;

namespace Galac.Adm.Uil.GestionCompras.ViewModel {
    public class ProveedorViewModel : LibInputViewModelMfc<Proveedor> {
        #region Constantes
        public const string CodigoProveedorPropertyName = "CodigoProveedor";
        public const string ConsecutivoPropertyName = "Consecutivo";
        public const string NombreProveedorPropertyName = "NombreProveedor";
        public const string ContactoPropertyName = "Contacto";
        public const string NumeroRIFPropertyName = "NumeroRIF";
        public const string NumeroNITPropertyName = "NumeroNIT";
        public const string TipoDePersonaPropertyName = "TipoDePersona";
        public const string CodigoRetencionUsualPropertyName = "CodigoRetencionUsual";
        public const string TelefonosPropertyName = "Telefonos";
        public const string DireccionPropertyName = "Direccion";
        public const string FaxPropertyName = "Fax";
        public const string EmailPropertyName = "Email";
        public const string TipodeProveedorPropertyName = "TipodeProveedor";
        public const string TipoDeProveedorDeLibrosFiscalesPropertyName = "TipoDeProveedorDeLibrosFiscales";
        public const string PorcentajeRetencionIVAPropertyName = "PorcentajeRetencionIVAAsEnum";
        public const string CuentaContableCxPPropertyName = "CuentaContableCxP";
        public const string CuentaContableGastosPropertyName = "CuentaContableGastos";
        public const string CuentaContableAnticipoPropertyName = "CuentaContableAnticipo";
        public const string CodigoLotePropertyName = "CodigoLote";
        public const string BeneficiarioPropertyName = "Beneficiario";
        public const string UsarBeneficiarioImpCheqPropertyName = "UsarBeneficiarioImpCheq";
        public const string TipoDocumentoIdentificacionPropertyName = "TipoDocumentoIdentificacion";
        public const string EsAgenteDeRetencionIvaPropertyName = "EsAgenteDeRetencionIva";
        public const string NombrePropertyName = "Nombre";
        public const string ApellidoPaternoPropertyName = "ApellidoPaterno";
        public const string ApellidoMaternoPropertyName = "ApellidoMaterno";
        public const string NumeroCuentaBancariaPropertyName = "NumeroCuentaBancaria";
        public const string CodigoContribuyentePropertyName = "CodigoContribuyente";
        public const string NumeroRUCPropertyName = "NumeroRUC";
        public const string NombreOperadorPropertyName = "NombreOperador";
        public const string FechaUltimaModificacionPropertyName = "FechaUltimaModificacion";
        public const string DescripcionCodRetPropertyName = "DescripcionCodRet";
        public const string IsEnabledBeneficiarioPropertyName = "IsEnabledBeneficiario";
        public const string DescripcionCuentaContableGastosPropertyName = "DescripcionCuentaContableGastos";
        public const string DescripcionCuentaContableAnticipoPropertyName = "DescripcionCuentaContableAnticipo";
        public const string DescripcionCuentaContableCxPPropertyName = "DescripcionCuentaContableCxP";
        public const string TipoDePersonaLibrosElectronicosPropertyName = "TipoDePersonaLibrosElectronicos";
        public const string NombrePaisResidenciaPropertyName = "NombrePaisResidencia";
        public const string PaisConveniosSunatPropertyName = "PaisConveniosSunat";
        public const string CodigoPaisDeResidenciaPropertyName = "CodigoPaisDeResidencia";
        public const string IsVisibleParaSujetoNoDomicilidadoPropertyName = "IsVisibleParaSujetoNoDomicilidado";
        public const string IsVisibleParaSujetoNoDomicilidadoNaturalPropertyName = "IsVisibleParaSujetoNoDomicilidadoNatural";
        public const string IsVisibleNombreProveedorPropertyName = "IsVisibleNombreProveedor";


        #endregion
        #region Variables
        private FkTablaRetencionViewModel _ConexionCodigoRetencionUsual = null;
        private FkTipoProveedorViewModel _ConexionTipodeProveedor = null;
        private FkCuentaViewModel _ConexionCuentaContableCxP = null;
        private FkCuentaViewModel _ConexionCuentaContableGastos = null;
        private FkCuentaViewModel _ConexionCuentaContableAnticipo = null;
        private FkPaisSunatViewModel _ConexionNombrePaisResidencia = null;
        private FkConveniosSunatViewModel _ConexionPaisConveniosSunat = null;
        #endregion //Variables
        #region Propiedades

        public override string ModuleName {
            get { return "Proveedor"; }
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

        [LibRequired(ErrorMessage = "El campo Código es requerido.")]
        [LibGridColum("Código", ColumnOrder = 0)]
        public string CodigoProveedor {
            get {
                return Model.CodigoProveedor;
            }
            set {
                if (Model.CodigoProveedor != value) {
                    Model.CodigoProveedor = value;
                    IsDirty = true;
                    RaisePropertyChanged(CodigoProveedorPropertyName);
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
                    IsDirty = true;
                    RaisePropertyChanged(ConsecutivoPropertyName);
                }
            }
        }

        [LibCustomValidation("NombreProveedorValidating")]
        [LibGridColum("Nombre Proveedor", Width = 300, ColumnOrder = 1)]
        public string NombreProveedor {
            get {
                return Model.NombreProveedor;
            }
            set {
                if (Model.NombreProveedor != value) {
                    Model.NombreProveedor = value;
                    IsDirty = true;
                    RaisePropertyChanged(NombreProveedorPropertyName);
                }
            }
        }

        [LibRequired(ErrorMessageResourceName = "lblValidacionNumero", ErrorMessageResourceType = typeof(Properties.Resources))]
        [LibGridColum(HeaderResourceName = "lblEtiquetaNumero", HeaderResourceType = typeof(Resources), ColumnOrder = 2)]
        public string NumeroRIF {
            get {
                return Model.NumeroRIF;
            }
            set {
                if (Model.NumeroRIF != value) {
                    Model.NumeroRIF = value;
                    if (LibDefGen.ProgramInfo.IsCountryPeru()) {
                        NumeroRUC = value;
                    }
                    IsDirty = true;
                    RaisePropertyChanged(NumeroRIFPropertyName);
                }
            }
        }

        public string NumeroNIT {
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

        public eTipodePersonaRetencion TipoDePersona {
            get {
                return Model.TipoDePersonaAsEnum;
            }
            set {                
                if (Model.TipoDePersonaAsEnum != value) {
                    Model.TipoDePersonaAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(TipoDePersonaPropertyName);
                    CodigoRetencionUsual = "";
                }
            }
        }

        [LibCustomValidation("CodigoRetencionUsualValidating")]
        [LibGridColum("Codigo Retención", eGridColumType.Connection, ConnectionDisplayMemberPath = "Codigo", ConnectionModelPropertyName = "CodigoRetencionUsual", ConnectionSearchCommandName = "ChooseCodigoRetencionUsualCommand", ColumnOrder = 3)]
        public string CodigoRetencionUsual {
            get {
                return Model.CodigoRetencionUsual;
            }
            set {
                if (Model.CodigoRetencionUsual != value) {
                    Model.CodigoRetencionUsual = value;
                    IsDirty = true;
                    RaisePropertyChanged(CodigoRetencionUsualPropertyName);
                    RaisePropertyChanged(DescripcionCodRetPropertyName);
                    if (LibString.IsNullOrEmpty(CodigoRetencionUsual, true)) {
                        ConexionCodigoRetencionUsual = null;
                    }
                }
            }
        }

        [LibGridColum("Contacto", ColumnOrder = 4)]
        public string Contacto {
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

        public string Telefonos {
            get {
                return Model.Telefonos;
            }
            set {
                if (Model.Telefonos != value) {
                    Model.Telefonos = value;
                    IsDirty = true;
                    RaisePropertyChanged(TelefonosPropertyName);
                }
            }
        }

        public string Direccion {
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

        public string Fax {
            get {
                return Model.Fax;
            }
            set {
                if (Model.Fax != value) {
                    Model.Fax = value;
                    IsDirty = true;
                    RaisePropertyChanged(FaxPropertyName);
                }
            }
        }

        public string Email {
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

        [LibRequired(ErrorMessage = "El campo Tipo es requerido.")]
        public string TipodeProveedor {
            get {
                return Model.TipodeProveedor;
            }
            set {
                if (Model.TipodeProveedor != value) {
                    Model.TipodeProveedor = value;
                    IsDirty = true;
                    RaisePropertyChanged(TipodeProveedorPropertyName);
                    if (LibString.IsNullOrEmpty(TipodeProveedor, true)) {
                        ConexionTipodeProveedor = null;
                    }
                }
            }
        }

        public eTipoDeProveedorDeLibrosFiscales TipoDeProveedorDeLibrosFiscales {
            get {
                return Model.TipoDeProveedorDeLibrosFiscalesAsEnum;
            }
            set {
                if (Model.TipoDeProveedorDeLibrosFiscalesAsEnum != value) {
                    Model.TipoDeProveedorDeLibrosFiscalesAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(TipoDeProveedorDeLibrosFiscalesPropertyName);
                }
            }
        }


        ePorcentajeDeRetencionDeIVA _PorcentajeRetencionIVAAsEnum;
        public ePorcentajeDeRetencionDeIVA PorcentajeRetencionIVAAsEnum {
            get {
                if (Model.PorcentajeRetencionIVA == LibConvert.ToDec(0)) {
                    return ePorcentajeDeRetencionDeIVA.por0;
                } else if (Model.PorcentajeRetencionIVA == LibConvert.ToDec(75)) {
                    return ePorcentajeDeRetencionDeIVA.por75;
                } else if (Model.PorcentajeRetencionIVA == LibConvert.ToDec(100)) {
                    return ePorcentajeDeRetencionDeIVA.por100;
                } else {
                    return ePorcentajeDeRetencionDeIVA.por0;
                }
            }
            set {
                if (_PorcentajeRetencionIVAAsEnum != value) {
                    _PorcentajeRetencionIVAAsEnum = value;
                    if (_PorcentajeRetencionIVAAsEnum == ePorcentajeDeRetencionDeIVA.por0) {
                        Model.PorcentajeRetencionIVA = 0;
                    } else if (_PorcentajeRetencionIVAAsEnum == ePorcentajeDeRetencionDeIVA.por75) {
                        Model.PorcentajeRetencionIVA = 75;
                    } else if (_PorcentajeRetencionIVAAsEnum == ePorcentajeDeRetencionDeIVA.por100) {
                        Model.PorcentajeRetencionIVA = 100;
                    }
                    IsDirty = true;
                    RaisePropertyChanged(PorcentajeRetencionIVAPropertyName);
                }
            }
        }

        public string CuentaContableCxP {
            get {
                return Model.CuentaContableCxP;
            }
            set {
                if (Model.CuentaContableCxP != value) {
                    Model.CuentaContableCxP = value;
                    IsDirty = true;
                    RaisePropertyChanged(CuentaContableCxPPropertyName);
                    RaisePropertyChanged(DescripcionCuentaContableCxPPropertyName);
                    if (LibString.IsNullOrEmpty(CuentaContableCxP, true)) {
                        ConexionCuentaContableCxP = null;
                    }
                }
            }
        }

        public string CuentaContableGastos {
            get {
                return Model.CuentaContableGastos;
            }
            set {
                if (Model.CuentaContableGastos != value) {
                    Model.CuentaContableGastos = value;
                    IsDirty = true;
                    RaisePropertyChanged(CuentaContableGastosPropertyName);
                    RaisePropertyChanged(DescripcionCuentaContableGastosPropertyName);
                    if (LibString.IsNullOrEmpty(CuentaContableGastos, true)) {
                        ConexionCuentaContableGastos = null;
                    }
                }
            }
        }

        public string CuentaContableAnticipo {
            get {
                return Model.CuentaContableAnticipo;
            }
            set {
                if (Model.CuentaContableAnticipo != value) {
                    Model.CuentaContableAnticipo = value;
                    IsDirty = true;
                    RaisePropertyChanged(CuentaContableAnticipoPropertyName);
                    RaisePropertyChanged(DescripcionCuentaContableAnticipoPropertyName);
                    if (LibString.IsNullOrEmpty(CuentaContableAnticipo, true)) {
                        ConexionCuentaContableAnticipo = null;
                    }
                }
            }
        }

        public string CodigoLote {
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

        public string Beneficiario {
            get {
                return Model.Beneficiario;
            }
            set {
                if (Model.Beneficiario != value) {
                    Model.Beneficiario = value;
                    IsDirty = true;
                    RaisePropertyChanged(BeneficiarioPropertyName);
                }
            }
        }

        public bool UsarBeneficiarioImpCheq {
            get {
                return Model.UsarBeneficiarioImpCheqAsBool;
            }
            set {
                if (Model.UsarBeneficiarioImpCheqAsBool != value) {
                    Model.UsarBeneficiarioImpCheqAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(UsarBeneficiarioImpCheqPropertyName);
                    RaisePropertyChanged(IsEnabledBeneficiarioPropertyName);
                    Beneficiario = string.Empty;
                }
            }
        }

        public eTipoDocumentoIdentificacion TipoDocumentoIdentificacion {
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

        public bool EsAgenteDeRetencionIva {
            get {
                return Model.EsAgenteDeRetencionIvaAsBool;
            }
            set {
                if (Model.EsAgenteDeRetencionIvaAsBool != value) {
                    Model.EsAgenteDeRetencionIvaAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(EsAgenteDeRetencionIvaPropertyName);
                }
            }
        }

        [LibCustomValidation("NombreSujNoDomiciladoValidating")]
        public string Nombre {
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

        [LibCustomValidation("ApellidoSujNoDomiciladoValidating")]
        public string ApellidoPaterno {
            get {
                return Model.ApellidoPaterno;
            }
            set {
                if (Model.ApellidoPaterno != value) {
                    Model.ApellidoPaterno = value;
                    IsDirty = true;
                    RaisePropertyChanged(ApellidoPaternoPropertyName);
                }
            }
        }

        public string ApellidoMaterno {
            get {
                return Model.ApellidoMaterno;
            }
            set {
                if (Model.ApellidoMaterno != value) {
                    Model.ApellidoMaterno = value;
                    IsDirty = true;
                    RaisePropertyChanged(ApellidoMaternoPropertyName);
                }
            }
        }

        public string NumeroCuentaBancaria {
            get {
                return Model.NumeroCuentaBancaria;
            }
            set {
                if (Model.NumeroCuentaBancaria != value) {
                    Model.NumeroCuentaBancaria = value;
                    IsDirty = true;
                    RaisePropertyChanged(NumeroCuentaBancariaPropertyName);
                }
            }
        }

        public string CodigoContribuyente {
            get {
                return Model.CodigoContribuyente;
            }
            set {
                if (Model.CodigoContribuyente != value) {
                    Model.CodigoContribuyente = value;
                    IsDirty = true;
                    RaisePropertyChanged(CodigoContribuyentePropertyName);
                }
            }
        }

        public string NumeroRUC {
            get {
                return Model.NumeroRUC;
            }
            set {
                if (Model.NumeroRUC != value) {
                    Model.NumeroRUC = value;
                    IsDirty = true;
                    RaisePropertyChanged(NumeroRUCPropertyName);
                }
            }
        }

        public eTipoDePersonaLibrosElectronicos TipoDePersonaLibrosElectronicos {
            get {
                return Model.TipoDePersonaLibrosElectronicosAsEnum;
            }
            set {
                if (Model.TipoDePersonaLibrosElectronicosAsEnum != value) {
                    Model.TipoDePersonaLibrosElectronicosAsEnum = value;
                    IsDirty = true;
                    ValidateTipoDePersonaLibrosElectronicos();
                    RaisePropertyChanged(NombrePaisResidenciaPropertyName);
                    RaisePropertyChanged(NombrePropertyName);
                    RaisePropertyChanged(NombreProveedorPropertyName);
                    RaisePropertyChanged(ApellidoPaternoPropertyName);
                    RaisePropertyChanged(NombrePaisResidenciaPropertyName);
                    RaisePropertyChanged(PaisConveniosSunatPropertyName);
                    RaisePropertyChanged(IsVisibleParaSujetoNoDomicilidadoPropertyName);
                    RaisePropertyChanged(IsVisibleParaSujetoNoDomicilidadoNaturalPropertyName);
                    RaisePropertyChanged(IsVisibleNombreProveedorPropertyName);
                    RaisePropertyChanged(TipoDePersonaLibrosElectronicosPropertyName);
                }
            }
        }       

        public string CodigoPaisResidencia {
            get {
                return Model.CodigoPaisResidencia;
            }
            set {
                if (Model.CodigoPaisResidencia != value) {
                    Model.CodigoPaisResidencia = value;
                }
            }
        }

        [LibCustomValidation("NombrePaisResidenciaValidating")]
        public string NombrePaisResidencia {
            get {
                return Model.NombrePaisResidencia;
            }
            set {
                if (Model.NombrePaisResidencia != value) {
                    Model.NombrePaisResidencia = value;
                    IsDirty = true;
                    RaisePropertyChanged(NombrePaisResidenciaPropertyName);
                    if (LibString.IsNullOrEmpty(NombrePaisResidencia, true)) {
                        ConexionNombrePaisResidencia = null;
                    }
                }
            }
        }

        public string CodigoConveniosSunat {
            get {
                return Model.CodigoConveniosSunat;
            }
            set {
                if (Model.CodigoConveniosSunat != value) {
                    Model.CodigoConveniosSunat = value;
                }
            }
        }

        [LibCustomValidation("ConveniosSunatValidating")]
        public string PaisConveniosSunat {
            get {
                return Model.PaisConveniosSunat;
            }
            set {
                if (Model.PaisConveniosSunat != value) {
                    Model.PaisConveniosSunat = value;
                    IsDirty = true;
                    RaisePropertyChanged(PaisConveniosSunatPropertyName);
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
                    IsDirty = true;
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
                    IsDirty = true;
                    RaisePropertyChanged(FechaUltimaModificacionPropertyName);
                }
            }
        }

        public eTipodePersonaRetencion[] ArrayTipodePersonaRetencion {
            get {
                return LibEnumHelper<eTipodePersonaRetencion>.GetValuesInArray();
            }
        }

        public eTipoDeProveedorDeLibrosFiscales[] ArrayTipoDeProveedorDeLibrosFiscales {
            get {
                return LibEnumHelper<eTipoDeProveedorDeLibrosFiscales>.GetValuesInArray();
            }
        }

        public ePorcentajeDeRetencionDeIVA[] ArrayPorcentajeRetencionIva {
            get {
                return LibEnumHelper<ePorcentajeDeRetencionDeIVA>.GetValuesInArray();
            }
        }

        public eTipoDocumentoIdentificacion[] ArrayTipoDocumentoIdentificacion {
            get {
                return LibEnumHelper<eTipoDocumentoIdentificacion>.GetValuesInArray();
            }
        }

        public eTipoDePersonaLibrosElectronicos[] ArrayTipoDePersonaLibrosElectronicos {
            get {
                return LibEnumHelper<eTipoDePersonaLibrosElectronicos>.GetValuesInArray();
            }
        }

        public FkTablaRetencionViewModel ConexionCodigoRetencionUsual {
            get {
                return _ConexionCodigoRetencionUsual;
            }
            set {
                if (_ConexionCodigoRetencionUsual != value) {
                    _ConexionCodigoRetencionUsual = value;
                    if (_ConexionCodigoRetencionUsual != null) {
                        CodigoRetencionUsual = ConexionCodigoRetencionUsual.Codigo;
                    }
                }
                if (_ConexionCodigoRetencionUsual == null) {
                    CodigoRetencionUsual = string.Empty;
                }
            }
        }

        public FkTipoProveedorViewModel ConexionTipodeProveedor {
            get {
                return _ConexionTipodeProveedor;
            }
            set {
                if (_ConexionTipodeProveedor != value) {
                    _ConexionTipodeProveedor = value;
                    RaisePropertyChanged(TipodeProveedorPropertyName);
                }
                if (_ConexionTipodeProveedor == null) {
                    TipodeProveedor = string.Empty;
                }
            }
        }

        public FkCuentaViewModel ConexionCuentaContableCxP {
            get {
                return _ConexionCuentaContableCxP;
            }
            set {
                if (_ConexionCuentaContableCxP != value) {
                    _ConexionCuentaContableCxP = value;
                    RaisePropertyChanged(CuentaContableCxPPropertyName);
                }
                if (_ConexionCuentaContableCxP == null) {
                    CuentaContableCxP = string.Empty;
                }
            }
        }

        public FkCuentaViewModel ConexionCuentaContableGastos {
            get {
                return _ConexionCuentaContableGastos;
            }
            set {
                if (_ConexionCuentaContableGastos != value) {
                    _ConexionCuentaContableGastos = value;
                    RaisePropertyChanged(CuentaContableGastosPropertyName);
                }
                if (_ConexionCuentaContableGastos == null) {
                    CuentaContableGastos = string.Empty;
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

        public FkPaisSunatViewModel ConexionNombrePaisResidencia {
            get {
                return _ConexionNombrePaisResidencia;
            }
            set {
                if (_ConexionNombrePaisResidencia != value) {
                    _ConexionNombrePaisResidencia = value;
                    RaisePropertyChanged(NombrePaisResidenciaPropertyName);
                }
                if (_ConexionNombrePaisResidencia == null) {
                    NombrePaisResidencia = string.Empty;
                }
            }
        }

        public FkConveniosSunatViewModel ConexionPaisConveniosSunat {
            get {
                return _ConexionPaisConveniosSunat;
            }
            set {
                if (_ConexionPaisConveniosSunat != value) {
                    _ConexionPaisConveniosSunat = value;
                    RaisePropertyChanged(PaisConveniosSunatPropertyName);
                }
                if (_ConexionPaisConveniosSunat == null) {
                    PaisConveniosSunat = string.Empty;
                }
            }
        }

        public RelayCommand<string> ChooseCodigoRetencionUsualCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseTipodeProveedorCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseCuentaContableCxPCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseCuentaContableGastosCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseCuentaContableAnticipoCommand {
            get;
            private set;
        }

        public RelayCommand ValidarRifWebCommand {
            get;
            private set;
        }

        public bool IsEnabledValidaRif {
            get {
                return IsEnabled && LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "ValidarRifEnLaWeb");
            }
        }

        public bool IsVisibleISLR {
            get {
                return LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "UsaRetencion");
            }
        }

        public bool IsVisibleMunicipales {
            get {
                return LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "RetieneImpuestoMunicipal");
            }
        }

        public bool IsVisibleContribuyenteEspecial {
            get {
                return LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros","PuedoUsarOpcionesDeContribuyenteEspecial") && !LibDefGen.ProgramInfo.IsCountryPeru();
            }
        }

        public bool IsVisibleContablidadActiva {
            get {
                return LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Compania", "AccesoCaracteristicaDeContabilidadActiva");
            }
        }

        public string DescripcionCodRet {
            get {
                if (!LibString.IsNullOrEmpty(CodigoRetencionUsual, true)) {
                    return ConexionCodigoRetencionUsual.TipoDePago;
                }
                return "";
            }
        }
        public string DescripcionCuentaContableGastos {
            get {
                if (ConexionCuentaContableGastos != null) {
                    return ConexionCuentaContableGastos.Descripcion;
                }
                return "";
            }
        }

        public string DescripcionCuentaContableAnticipo {
            get {
                if (ConexionCuentaContableAnticipo != null) {
                    return ConexionCuentaContableAnticipo.Descripcion;
                }
                return "";
            }
        }

        public string DescripcionCuentaContableCxP {
            get {
                if (ConexionCuentaContableCxP != null) {
                    return ConexionCuentaContableCxP.Descripcion;
                }
                return "";
            }
        }

        public bool IsEnabledBeneficiario {
            get {
                return IsEnabled && UsarBeneficiarioImpCheq;
            }
        }

        public bool IsEnabledCodigoProveedor {
            get {
                return Action == eAccionSR.Insertar;
            }
        }

        public bool IsEnabledTipoPersona {
            get {
                return Action == eAccionSR.Insertar;
            }
        }

        private ValidationResult CodigoRetencionUsualValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if ((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else {
                if (IsVisibleISLR) {
                    if (LibString.IsNullOrEmpty(CodigoRetencionUsual)) {
                        vResult = new ValidationResult("El campo Código de Retención Usual, es requerido.");
                    }
                }
            }
            return vResult;
        }

        private ValidationResult NombreProveedorValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if (TipoDePersonaLibrosElectronicos != eTipoDePersonaLibrosElectronicos.NaturalNoDomiciliado) {
                if (NombreProveedor != string.Empty) {
                    return vResult;
                } else {
                    vResult = new ValidationResult("El nombre del Proveedor es requerido.");
                }
            }
            return vResult;
        }

        private ValidationResult NombreSujNoDomiciladoValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if (LibDefGen.ProgramInfo.IsCountryPeru() && TipoDePersonaLibrosElectronicos == eTipoDePersonaLibrosElectronicos.NaturalNoDomiciliado) {
                if (Nombre != string.Empty) {
                    return vResult;
                } else {
                    vResult = new ValidationResult("El nombre es requerido.");
                }
            }
            return vResult;
        }

        private ValidationResult ApellidoSujNoDomiciladoValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if (LibDefGen.ProgramInfo.IsCountryPeru() && TipoDePersonaLibrosElectronicos == eTipoDePersonaLibrosElectronicos.NaturalNoDomiciliado) {
                if (ApellidoPaterno != string.Empty) {
                    return vResult;
                } else {
                    vResult = new ValidationResult("Al menos un apellido es requerido.");
                }
            }
            return vResult;
        }

        private ValidationResult NombrePaisResidenciaValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if (LibDefGen.ProgramInfo.IsCountryPeru() &&
                (TipoDePersonaLibrosElectronicos == eTipoDePersonaLibrosElectronicos.NaturalNoDomiciliado ||
                TipoDePersonaLibrosElectronicos == eTipoDePersonaLibrosElectronicos.JuridicoNoDomiciliado)) {
                if (NombrePaisResidencia != string.Empty) {
                    return vResult;
                } else {
                    vResult = new ValidationResult("El país de residencia es requerido.");
                }
            }
            return vResult;
        }

        private ValidationResult ConveniosSunatValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if (LibDefGen.ProgramInfo.IsCountryPeru() &&
                (TipoDePersonaLibrosElectronicos == eTipoDePersonaLibrosElectronicos.NaturalNoDomiciliado ||
                TipoDePersonaLibrosElectronicos == eTipoDePersonaLibrosElectronicos.JuridicoNoDomiciliado)) {
                if (PaisConveniosSunat != string.Empty) {
                    return vResult;
                } else {
                    vResult = new ValidationResult("El país convenio es requerido.");
                }
            }
            return vResult;
        }

        public RelayCommand<string> ChooseNombrePaisResidenciaCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChoosePaisConveniosSunatCommand {
            get;
            private set;
        }

        public string RifContent {
            get;
            set;
        }

        public StatuRif ForeGroundRif {
            get;
            set;
        }


        public enum StatuRif {
            Valido = 0,
            Invalido,
            FalloConexion
        }
        #endregion //Propiedades
        #region Constructores
        public ProveedorViewModel()
            : this(new Proveedor(), eAccionSR.Insertar) {
        }
        public ProveedorViewModel( Proveedor initModel, eAccionSR initAction )
            : base(initModel, initAction, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            DefaultFocusedPropertyName = CodigoProveedorPropertyName;
            Model.ConsecutivoCompania = Mfc.GetInt("Compania");            
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override void InitializeLookAndFeel( Proveedor valModel ) {
            base.InitializeLookAndFeel(valModel);
            if (LibString.IsNullOrEmpty(CodigoProveedor, true)) {
                CodigoProveedor = GenerarProximoCodigoProveedor();
            }
            if (Model.PorcentajeRetencionIVA == LibConvert.ToDec(0)) {
                _PorcentajeRetencionIVAAsEnum = ePorcentajeDeRetencionDeIVA.por0;
            } else if (Model.PorcentajeRetencionIVA == LibConvert.ToDec(75)) {
                _PorcentajeRetencionIVAAsEnum = ePorcentajeDeRetencionDeIVA.por75;
            } else if (Model.PorcentajeRetencionIVA == LibConvert.ToDec(100)) {
                _PorcentajeRetencionIVAAsEnum = ePorcentajeDeRetencionDeIVA.por100;
            }

            if (LibDefGen.ProgramInfo.IsCountryPeru() && Model.NombreProveedor==string.Empty) {
                TipoDePersonaLibrosElectronicos = eTipoDePersonaLibrosElectronicos.JuridicoDomiciliado;
            }
        }

        protected override Proveedor FindCurrentRecord( Proveedor valModel ) {
            if (valModel == null) {
                return null;
            }
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valModel.ConsecutivoCompania);
            vParams.AddInString("CodigoProveedor", valModel.CodigoProveedor, 10);
            return BusinessComponent.GetData(eProcessMessageType.SpName, "ProveedorGET", vParams.Get()).FirstOrDefault();
        }

        protected override ILibBusinessComponentWithSearch<IList<Proveedor>, IList<Proveedor>> GetBusinessComponent() {
            return new clsProveedorNav();
        }

        private string GenerarProximoCodigoProveedor() {
            string vResult = string.Empty;
            XElement vResulset = GetBusinessComponent().QueryInfo(eProcessMessageType.Message, "ProximoCodigoProveedor", Mfc.GetIntAsParam("Compania"));
            vResult = LibXml.GetPropertyString(vResulset, "CodigoProveedor");
            return vResult;
        }

        protected override void InitializeCommands() {
            base.InitializeCommands();
            ChooseCodigoRetencionUsualCommand = new RelayCommand<string>(ExecuteChooseCodigoRetencionUsualCommand);
            ChooseTipodeProveedorCommand = new RelayCommand<string>(ExecuteChooseTipodeProveedorCommand);
            ChooseCuentaContableCxPCommand = new RelayCommand<string>(ExecuteChooseCuentaContableCxPCommand);
            ChooseCuentaContableGastosCommand = new RelayCommand<string>(ExecuteChooseCuentaContableGastosCommand);
            ChooseCuentaContableAnticipoCommand = new RelayCommand<string>(ExecuteChooseCuentaContableAnticipoCommand);
            ValidarRifWebCommand = new RelayCommand(ExecuteCommandValidarRifWeb);
            ChooseNombrePaisResidenciaCommand = new RelayCommand<string>(ExecuteChooseNombrePaisResidenciaCommand);
            ChoosePaisConveniosSunatCommand = new RelayCommand<string>(ExecuteChoosePaisConveniosSunatCommand);
        }

        protected override void ReloadRelatedConnections() {
            base.ReloadRelatedConnections();
            Galac.Comun.Ccl.TablasLey.ITablaRetencionPdn insTablaRetencion = new Galac.Comun.Brl.TablasLey.clsTablaRetencionNav();
            DateTime vFechaInicioVigencia = insTablaRetencion.BuscaFechaDeInicioDeVigencia(LibDate.Today());
            LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("TipoDePersona", TipoDePersona);            
            vFixedCriteria.Add(LibSearchCriteria.CreateCriteria("FechaDeInicioDeVigencia", vFechaInicioVigencia.ToShortDateString()), eLogicOperatorType.And);
            LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo", CodigoRetencionUsual);
            vDefaultCriteria.Add(vFixedCriteria, eLogicOperatorType.And);
            ConexionCodigoRetencionUsual = FirstConnectionRecordOrDefault<FkTablaRetencionViewModel>("Tabla Retención", vDefaultCriteria);

            vFixedCriteria = LibSearchCriteria.CreateCriteria("ConsecutivoCompania", Mfc.GetInt("Compania"));
            vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("nombre", TipodeProveedor);
            ConexionTipodeProveedor = FirstConnectionRecordOrDefault<FkTipoProveedorViewModel>("Tipo Proveedor", vDefaultCriteria);

            vFixedCriteria = LibSearchCriteria.CreateCriteria("ConsecutivoPeriodo",LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("Parametros","ConsecutivoPeriodo"));
            vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo", CuentaContableCxP);
            vDefaultCriteria.Add(vFixedCriteria, eLogicOperatorType.And);
            ConexionCuentaContableCxP = FirstConnectionRecordOrDefault<FkCuentaViewModel>("Cuenta", vDefaultCriteria);

            vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo", CuentaContableGastos);
            vDefaultCriteria.Add(vFixedCriteria, eLogicOperatorType.And);
            ConexionCuentaContableGastos = FirstConnectionRecordOrDefault<FkCuentaViewModel>("Cuenta", vDefaultCriteria);

            vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo", CuentaContableAnticipo);
            vDefaultCriteria.Add(vFixedCriteria, eLogicOperatorType.And);
            ConexionCuentaContableAnticipo = FirstConnectionRecordOrDefault<FkCuentaViewModel>("Cuenta", vDefaultCriteria);

            if(LibDefGen.ProgramInfo.IsCountryPeru()) {
                if(CodigoPaisResidencia != string.Empty) {
                    vFixedCriteria = LibSearchCriteria.CreateCriteria("ConsecutivoCompania",Mfc.GetInt("Compania"));
                    vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo",CodigoPaisResidencia);
                    ConexionNombrePaisResidencia = FirstConnectionRecordOrDefault<FkPaisSunatViewModel>("País Sunat",vDefaultCriteria);
                    NombrePaisResidencia = ConexionNombrePaisResidencia.Nombre;
                }

                if(CodigoConveniosSunat != string.Empty) {
                    vFixedCriteria = LibSearchCriteria.CreateCriteria("ConsecutivoCompania",Mfc.GetInt("Compania"));
                    vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo",CodigoConveniosSunat);
                    ConexionPaisConveniosSunat = FirstConnectionRecordOrDefault<FkConveniosSunatViewModel>("Convenios Sunat",vDefaultCriteria);
                    PaisConveniosSunat = ConexionPaisConveniosSunat.Pais;
                }
            }
        }

        private void ExecuteChooseCodigoRetencionUsualCommand( string valcodigo ) {
            try {
                DateTime vFechaInicioVigencia;
                DateTime vFechaHoy = LibDate.Today();
                if (LibString.IsNullOrEmpty(valcodigo, true)) {
                    valcodigo = "";
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo", valcodigo);

                Galac.Comun.Ccl.TablasLey.ITablaRetencionPdn insTablaRetencion = new Galac.Comun.Brl.TablasLey.clsTablaRetencionNav();
                vFechaInicioVigencia = insTablaRetencion.BuscaFechaDeInicioDeVigencia(vFechaHoy);

                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("TipoDePersona", TipoDePersona);
                vFixedCriteria.Add(LibSearchCriteria.CreateCriteria("FechaDeInicioDeVigencia", vFechaInicioVigencia.ToShortDateString()), eLogicOperatorType.And);
                ConexionCodigoRetencionUsual = ChooseRecord<FkTablaRetencionViewModel>("Tabla Retención", vDefaultCriteria, vFixedCriteria, string.Empty);
                if (ConexionCodigoRetencionUsual != null) {
                    CodigoRetencionUsual = ConexionCodigoRetencionUsual.Codigo;
                } else {
                    CodigoRetencionUsual = "";
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void ExecuteChooseTipodeProveedorCommand( string valnombre ) {
            try {
                if (valnombre == null) {
                    valnombre = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Nombre", valnombre);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("ConsecutivoCompania", Mfc.GetInt("Compania"));
                ConexionTipodeProveedor = ChooseRecord<FkTipoProveedorViewModel>("Tipo Proveedor", vDefaultCriteria, vFixedCriteria, string.Empty);
                if (ConexionTipodeProveedor != null) {
                    TipodeProveedor = ConexionTipodeProveedor.Nombre;
                } else {
                    TipodeProveedor = string.Empty;
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void ExecuteChooseNombrePaisResidenciaCommand( string valNombre ) {
            try {
                if (valNombre == null) {
                    valNombre = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Nombre", valNombre);
                LibSearchCriteria vFixedCriteria = null;

                ConexionNombrePaisResidencia = ChooseRecord<FkPaisSunatViewModel>("País Sunat", vDefaultCriteria, vFixedCriteria, string.Empty);
                if (ConexionNombrePaisResidencia != null) {
                    Model.CodigoPaisResidencia = ConexionNombrePaisResidencia.Codigo;
                    NombrePaisResidencia = ConexionNombrePaisResidencia.Nombre;
                    CodigoPaisResidencia = ConexionNombrePaisResidencia.Codigo;
                } else {
                    Model.CodigoPaisResidencia = string.Empty;
                    NombrePaisResidencia = string.Empty;
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void ExecuteChoosePaisConveniosSunatCommand( string valPais ) {
            try {
                if (valPais == null) {
                    valPais = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Pais", valPais);
                LibSearchCriteria vFixedCriteria = null;               
                ConexionPaisConveniosSunat = ChooseRecord<FkConveniosSunatViewModel>("Convenios Sunat", vDefaultCriteria, vFixedCriteria, string.Empty);
                if (ConexionPaisConveniosSunat != null) {
                    Model.CodigoConveniosSunat = ConexionPaisConveniosSunat.Codigo;
                    PaisConveniosSunat = ConexionPaisConveniosSunat.Pais;
                    CodigoConveniosSunat = ConexionPaisConveniosSunat.Codigo;
                } else {
                    Model.CodigoConveniosSunat = string.Empty;
                    PaisConveniosSunat = string.Empty;
                }

            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void ExecuteChooseCuentaContableCxPCommand( string valcodigo ) {
            try {
                if (valcodigo == null) {
                    valcodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo", valcodigo);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("ConsecutivoPeriodo",LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("Parametros","ConsecutivoPeriodo"));
                vFixedCriteria.Add(LibSearchCriteria.CreateCriteria("TieneSubCuentas", "N"), eLogicOperatorType.And);
                vFixedCriteria.Add(LibSearchCriteria.CreateCriteria("ConsecutivoCompania", ConsecutivoCompania), eLogicOperatorType.And);
                ConexionCuentaContableCxP = ChooseRecord<FkCuentaViewModel>("Cuenta", vDefaultCriteria, vFixedCriteria, string.Empty);
                if (ConexionCuentaContableCxP != null) {
                    CuentaContableCxP = ConexionCuentaContableCxP.Codigo;
                } else {
                    CuentaContableCxP = string.Empty;
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void ExecuteChooseCuentaContableGastosCommand( string valcodigo ) {
            try {
                if (valcodigo == null) {
                    valcodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo", valcodigo);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("ConsecutivoPeriodo",LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("Parametros","ConsecutivoPeriodo"));
                vFixedCriteria.Add(LibSearchCriteria.CreateCriteria("TieneSubCuentas", "N"), eLogicOperatorType.And);
                vFixedCriteria.Add(LibSearchCriteria.CreateCriteria("ConsecutivoCompania", ConsecutivoCompania), eLogicOperatorType.And);
                ConexionCuentaContableGastos = ChooseRecord<FkCuentaViewModel>("Cuenta", vDefaultCriteria, vFixedCriteria, string.Empty);
                if (ConexionCuentaContableGastos != null) {
                    CuentaContableGastos = ConexionCuentaContableGastos.Codigo;
                } else {
                    CuentaContableGastos = string.Empty;
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void ExecuteChooseCuentaContableAnticipoCommand( string valcodigo ) {
            try {
                if (valcodigo == null) {
                    valcodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo", valcodigo);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("ConsecutivoPeriodo",LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("Parametros","ConsecutivoPeriodo"));
                vFixedCriteria.Add(LibSearchCriteria.CreateCriteria("TieneSubCuentas", "N"), eLogicOperatorType.And);
                vFixedCriteria.Add(LibSearchCriteria.CreateCriteria("ConsecutivoCompania", ConsecutivoCompania), eLogicOperatorType.And);
                ConexionCuentaContableAnticipo = ChooseRecord<FkCuentaViewModel>("Cuenta", vDefaultCriteria, vFixedCriteria, string.Empty);
                if (ConexionCuentaContableAnticipo != null) {
                    CuentaContableAnticipo = ConexionCuentaContableAnticipo.Codigo;
                } else {
                    CuentaContableAnticipo = string.Empty;
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        string ValidarRifWeb( string valRif ) {
            LibGalac.Aos.Ccl.IdFiscal.ILibIdFiscalPdn insBrlIdFiscal = new LibGalac.Aos.Brl.IdFiscal.LibIdFiscalNav();
            return insBrlIdFiscal.WebVerification(valRif);
        }

        private void ExecuteCommandValidarRifWeb() {
            string xmlWeb = ValidarRifWeb(NumeroRIF);
            XmlDocument xDoc = new XmlDocument();
            string vTipoDocumento = "RIF";
            string ubigeo = "";

            try {
                if (NumeroRIF == null) {
                    NumeroRIF = string.Empty;
                }

                if (LibDefGen.ProgramInfo.IsCountryPeru()) {
                    vTipoDocumento = "RUC";
                } else if (LibDefGen.ProgramInfo.IsCountryVenezuela()) {
                    vTipoDocumento = "RIF";
                }

                xDoc.LoadXml(xmlWeb);
                LibXmlDataParse insParse = new LibXmlDataParse(xDoc);
                NombreProveedor = insParse.GetString(0, "Nombre", "");
                if(LibDefGen.ProgramInfo.IsCountryPeru()) {
                    ubigeo = insParse.GetString(0,"Ubigeo","");
                    Direccion += (ubigeo == "") ? "" : ObtenerDireccionUbigeo(insParse.GetString(0,"Direccion",""),ubigeo);
                }
                bool validadoEnLaWeb = insParse.GetBool(0, "ValidadoEnLaWeb", false);
                bool idFiscalValido = insParse.GetBool(0, "IdFiscalValido", false);
                if (validadoEnLaWeb) {
                    if (idFiscalValido) {
                        RifContent = vTipoDocumento + " Válido";
                        ForeGroundRif = StatuRif.Valido;
                        RaiseMoveFocus(NumeroNITPropertyName);
                    } else {
                        RifContent = vTipoDocumento + " Inválido";
                        ForeGroundRif = StatuRif.Invalido;
                    }
                } else {
                    RifContent = "Falló Conexión";
                    ForeGroundRif = StatuRif.FalloConexion;
                }
                RaisePropertyChanged("RifContent");
                RaisePropertyChanged("ForeGroundRif");
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        private string ObtenerDireccionUbigeo(string valDireecion,string valIDUbigeo) {
            try {
                return valDireecion + new Galac.Comun.Brl.TablasGen.clsDireccionesUbigeoNav().ObtenerDireccion(valIDUbigeo);
            } catch(GalacException) {
                throw;
            } catch(Exception) {
                throw;
            }
        }
        #endregion //Metodos Generados
        protected override void ExecuteAction() {
            if (!IsVisibleISLR) {
                if (LibDefGen.ProgramInfo.IsCountryVenezuela()) {
                    Model.CodigoRetencionUsual = "NORET";
                } else if (LibDefGen.ProgramInfo.IsCountryPeru()) {
                    Model.CodigoRetencionUsual = "NOAPLI";
                }
            }
            if (LibDefGen.ProgramInfo.IsCountryPeru() && TipoDePersonaLibrosElectronicos == eTipoDePersonaLibrosElectronicos.NaturalNoDomiciliado) {
                NombreProveedor = Nombre + " " + ApellidoPaterno + " " + ApellidoMaterno;
            }

            base.ExecuteAction();
        }

        private void ValidateTipoDePersonaLibrosElectronicos() {
            NombrePaisResidencia = string.Empty;
            PaisConveniosSunat = string.Empty;
            CodigoPaisResidencia = string.Empty;
            CodigoConveniosSunat = string.Empty;
            if (LibDefGen.ProgramInfo.IsCountryPeru() &&
                (PaisConveniosSunat==string.Empty) &&
                (TipoDePersonaLibrosElectronicos == eTipoDePersonaLibrosElectronicos.JuridicoNoDomiciliado || 
                 TipoDePersonaLibrosElectronicos == eTipoDePersonaLibrosElectronicos.NaturalNoDomiciliado)) {                               
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("ConsecutivoCompania", Mfc.GetInt("Compania"));
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo", "00");
                ConexionPaisConveniosSunat = FirstConnectionRecordOrDefault<FkConveniosSunatViewModel>("Convenios Sunat", vDefaultCriteria);
                PaisConveniosSunat = ConexionPaisConveniosSunat.Pais;
                CodigoConveniosSunat = ConexionPaisConveniosSunat.Codigo;
            } else if (TipoDePersonaLibrosElectronicos != eTipoDePersonaLibrosElectronicos.NaturalNoDomiciliado) {
                Nombre = string.Empty;
                ApellidoPaterno = string.Empty;
                ApellidoMaterno = string.Empty;
            }
        }

        public string PromptNumeroRif {
            get {
                string vResult = "";
                if (LibDefGen.ProgramInfo.IsCountryVenezuela()) {
                    vResult = "N° R.I.F.";
                } else if (LibDefGen.ProgramInfo.IsCountryPeru()) {
                    vResult = "RUC";
                }
                return vResult;
            }
        }

        public bool isVisibleParaVenezuela {
            get {
                return LibDefGen.ProgramInfo.IsCountryVenezuela();
            }
        }

        public string PromptValidarRifWeb {
            get {
                string vResult = "";
                if (LibDefGen.ProgramInfo.IsCountryVenezuela()) {
                    vResult = "Validar RIF en la Web";
                } else if (LibDefGen.ProgramInfo.IsCountryPeru()) {
                    vResult = "Validar RUC en la Web";
                }
                return vResult;
            }
        }

        public bool isVisibleParaPeru {
            get {
                return LibDefGen.ProgramInfo.IsCountryPeru();
            }
        }

        public bool IsVisibleNombreProveedor {
            get {
                return !IsVisibleParaSujetoNoDomicilidadoNatural;
            }
        }

        public bool IsVisibleParaSujetoNoDomicilidadoNatural {
            get {
                return TipoDePersonaLibrosElectronicos.Equals(eTipoDePersonaLibrosElectronicos.NaturalNoDomiciliado) &&
                    LibDefGen.ProgramInfo.IsCountryPeru();
            }
        }

        public bool IsVisibleParaSujetoNoDomicilidado {
            get {
                return (TipoDePersonaLibrosElectronicos.Equals(eTipoDePersonaLibrosElectronicos.JuridicoNoDomiciliado) ||
                    TipoDePersonaLibrosElectronicos.Equals(eTipoDePersonaLibrosElectronicos.NaturalNoDomiciliado));
            }
        }
    } //End of class ProveedorViewModel

} //End of namespace Galac.Adm.Uil.GestionCompras

