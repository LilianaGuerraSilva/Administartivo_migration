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
    public class ClienteViewModel : LibInputViewModelMfc<Entity.Cliente> {
        #region Constantes
        public const string CodigoPropertyName = "Codigo";
        public const string NombrePropertyName = "Nombre";
        public const string NumeroRIFPropertyName = "NumeroRIF";
        public const string NumeroNITPropertyName = "NumeroNIT";
        public const string DireccionPropertyName = "Direccion";
        public const string CiudadPropertyName = "Ciudad";
        public const string ZonaPostalPropertyName = "ZonaPostal";
        public const string TelefonoPropertyName = "Telefono";
        public const string FAXPropertyName = "FAX";
        public const string StatusPropertyName = "Status";
        public const string ContactoPropertyName = "Contacto";
        public const string ZonaDeCobranzaPropertyName = "ZonaDeCobranza";
        public const string CodigoVendedorPropertyName = "CodigoVendedor";
        public const string RazonInactividadPropertyName = "RazonInactividad";
        public const string EmailPropertyName = "Email";
        public const string ActivarAvisoAlEscogerPropertyName = "ActivarAvisoAlEscoger";
        public const string TextoDelAvisoPropertyName = "TextoDelAviso";
        public const string CuentaContableCxCPropertyName = "CuentaContableCxC";
        public const string CuentaContableIngresosPropertyName = "CuentaContableIngresos";
        public const string CuentaContableAnticipoPropertyName = "CuentaContableAnticipo";
        public const string InfoGalacPropertyName = "InfoGalac";
        public const string SectorDeNegocioPropertyName = "SectorDeNegocio";
        public const string CodigoLotePropertyName = "CodigoLote";
        public const string NivelDePrecioPropertyName = "NivelDePrecio";
        public const string OrigenPropertyName = "Origen";
        public const string DiaCumpleanosPropertyName = "DiaCumpleanos";
        public const string MesCumpleanosPropertyName = "MesCumpleanos";
        public const string CorrespondenciaXEnviarPropertyName = "CorrespondenciaXEnviar";
        public const string EsExtranjeroPropertyName = "EsExtranjero";
        public const string ClienteDesdeFechaPropertyName = "ClienteDesdeFecha";
        public const string AQueSeDedicaElClientePropertyName = "AQueSeDedicaElCliente";
        public const string TipoDocumentoIdentificacionPropertyName = "TipoDocumentoIdentificacion";
        public const string TipoDeContribuyentePropertyName = "TipoDeContribuyente";
        public const string NombreOperadorPropertyName = "NombreOperador";
        public const string FechaUltimaModificacionPropertyName = "FechaUltimaModificacion";
        #endregion
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

        [LibRequired(ErrorMessage = "El campo Código es requerido.")]
        [LibGridColum("Código")]
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
        [LibGridColum("Nombre")]
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

        [LibGridColum("N° R.I.F.")]
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

   
        public string  InfoGalac {
            get {
                return Model.InfoGalac;
            }
            set {
                if (Model.InfoGalac != value) {
                    Model.InfoGalac = value;
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

        public eOrigenFacturacionOManual  Origen {
            get {
                return Model.OrigenAsEnum;
            }
            set {
                if (Model.OrigenAsEnum != value) {
                    Model.OrigenAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(OrigenPropertyName);
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

      
        public RelayCommand<string> ChooseCiudadCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseZonaDeCobranzaCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseCodigoVendedorCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseCuentaContableCxCCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseCuentaContableIngresosCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseCuentaContableAnticipoCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseSectorDeNegocioCommand {
            get;
            private set;
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
            return BusinessComponent.GetData(eProcessMessageType.SpName, "ClienteGET", vParams.Get()).FirstOrDefault();
        }

        protected override ILibBusinessComponentWithSearch<IList<Entity.Cliente>, IList<Entity.Cliente>> GetBusinessComponent() {
            return new clsClienteNav();
        }

        private string GenerarProximoCodigo() {
            string vResult = string.Empty;
            XElement vResulset = GetBusinessComponent().QueryInfo(eProcessMessageType.Message, "ProximoCodigo", Mfc.GetIntAsParam("Compania"));
            vResult = LibXml.GetPropertyString(vResulset, "Codigo");
            return vResult;
        }

        protected override void InitializeCommands() {
            base.InitializeCommands();
        }

        protected override void ReloadRelatedConnections() {
            base.ReloadRelatedConnections();
            //ConexionCiudad = FirstConnectionRecordOrDefault<FkCiudadViewModel>("Ciudad", LibSearchCriteria.CreateCriteria("NombreCiudad", Ciudad));
            //ConexionZonaDeCobranza = FirstConnectionRecordOrDefault<FkZonaCobranzaViewModel>("Zona Cobranza", LibSearchCriteria.CreateCriteria("nombre", ZonaDeCobranza));
            //ConexionCodigoVendedor = FirstConnectionRecordOrDefault<FkVendedorViewModel>("Vendedor", LibSearchCriteria.CreateCriteria("codigo", CodigoVendedor));
            //ConexionCuentaContableCxC = FirstConnectionRecordOrDefault<FkCuentaViewModel>("Cuenta", LibSearchCriteria.CreateCriteria("codigo", CuentaContableCxC));
            //ConexionCuentaContableIngresos = FirstConnectionRecordOrDefault<FkCuentaViewModel>("Cuenta", LibSearchCriteria.CreateCriteria("codigo", CuentaContableIngresos));
            //ConexionCuentaContableAnticipo = FirstConnectionRecordOrDefault<FkCuentaViewModel>("Cuenta", LibSearchCriteria.CreateCriteria("codigo", CuentaContableAnticipo));
            //ConexionSectorDeNegocio = FirstConnectionRecordOrDefault<FkSectorDeNegocioViewModel>("Sector de Negocio", LibSearchCriteria.CreateCriteria("Descripcion", SectorDeNegocio));
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

