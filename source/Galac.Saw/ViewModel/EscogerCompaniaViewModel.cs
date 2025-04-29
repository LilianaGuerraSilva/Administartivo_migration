using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.UI.Mvvm;
using LibGalac.Aos.UI.Mvvm.Command;
using LibGalac.Aos.Base;
using System.Xml.Linq;
using LibGalac.Aos.UI.Mvvm.Helpers;
using LibGalac.Aos.UI.Mvvm.Messaging;
using System.Collections.ObjectModel;
using System.Xml;
using System.Collections;
using LibGalac.Aos.UI.Contracts;
using System.ComponentModel.Composition;
using System.ComponentModel.DataAnnotations;
using Galac.Ent.Brl.Empresarial;

namespace Galac.Saw.ViewModel {
    [Export(typeof(LibMfcViewModelBase))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class EscogerCompaniaViewModel : LibMfcViewModelBase {
        #region Constantes
        public const string ChooseCompanyByCodigoPropertyName = "ChooseCompanyByCodigo";
        public const string ChooseCompanyByNamePropertyName = "ChooseCompanyByNombre";
        public const string NombrePropertyName = "Nombre";
        public const string CodigoPropertyName = "Codigo";
        public const string CompaniaPropertyName = "Compania";
        #endregion

        #region Variables
        private bool _ChooseCompanyByCodigo = false;
        private string _Nombre = null;
        private string _Codigo = null;
        private FkCompaniaViewModel _Compania = null;
        #endregion

        #region Propiedades

        public bool ChooseCompanyByCodigo {
            get {
                return _ChooseCompanyByCodigo;
            }
            set {
                if (_ChooseCompanyByCodigo != value) {
                    _ChooseCompanyByCodigo = value;
                    RaisePropertyChanged(ChooseCompanyByCodigoPropertyName);
                    RaisePropertyChanged(ChooseCompanyByNamePropertyName);
                }
            }
        }

        public bool ChooseCompanyByNombre {
            get {
                return !ChooseCompanyByCodigo;
            }
        }

        [Required(ErrorMessage = "Campo Código es requerido.")]
        public string Codigo {
            get {
                if (LibString.IsNullOrEmpty(_Codigo, true)) {
                    _Codigo = string.Empty;
                }
                return _Codigo;
            }
            set {
                if (_Codigo != value) {
                    _Codigo = value;
                    RaisePropertyChanged(CodigoPropertyName);
                    ChooseCompanyByCodigoCommand.RaiseCanExecuteChanged();
                    ChooseCompanyByNombreCommand.RaiseCanExecuteChanged();
                    if (LibString.IsNullOrEmpty(_Codigo, true)) {
                        Compania = null;
                    }
                }
            }
        }

        [Required(ErrorMessage = "Campo Nombre es requerido.")]
        public string Nombre {
            get {
                if (LibString.IsNullOrEmpty(_Nombre, true)) {
                    _Nombre = string.Empty;
                }
                return _Nombre;
            }
            set {
                if (_Nombre != value) {
                    _Nombre = value;
                    RaisePropertyChanged(NombrePropertyName);
                    ChooseCompanyByCodigoCommand.RaiseCanExecuteChanged();
                    ChooseCompanyByNombreCommand.RaiseCanExecuteChanged();
                    if (LibString.IsNullOrEmpty(_Nombre, true)) {
                        Compania = null;
                    }
                }
            }
        }

        public FkCompaniaViewModel Compania {
            get {
                return _Compania;
            }
            set {
                if (_Compania != value) {
                    _Compania = value;
                    RaisePropertyChanged(CompaniaPropertyName);
                    if (Compania != null) {
                        Codigo = Compania.Codigo;
                        Nombre = Compania.Nombre;
                        SetGlobalValues(Compania);
                    } else {
                        Codigo = string.Empty;
                        Nombre = string.Empty;
                    }
                }
            }
        }
        #endregion

        #region Commands
        public RelayCommand<string> ChooseCompanyByCodigoCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseCompanyByNombreCommand {
            get;
            private set;
        }
        #endregion

        #region Métodos
        public EscogerCompaniaViewModel() {
            MFCFieldDisplayName = "Consecutivo Compañía";
            MFCFieldName = "ConsecutivoCompania";
            MFCFieldType = typeof(int);
            MFCRecordDisplayName = "Empresa";
            MFCRecordName = "Compania";
            InitializeCommands();
        }

        private void InitializeCommands() {
            ChooseCompanyByCodigoCommand = new RelayCommand<string>(ExecuteChooseCompanyByCodigoCommand, CanExecuteChooseCompanyByCodigoCommand);
            ChooseCompanyByNombreCommand = new RelayCommand<string>(ExecuteChooseCompanyByNombreCommand, CanExecuteChooseCompanyByNombreCommand);
        }

        private bool CanExecuteChooseCompanyByNombreCommand(string valNombre) {
            return ChooseCompanyByNombre;
        }

        private bool CanExecuteChooseCompanyByCodigoCommand(string valCodigo) {
            return ChooseCompanyByCodigo;
        }

        private void ExecuteChooseCompanyByNombreCommand(string valNombre) {
            try {
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Gv_Compania_B1.Nombre", valNombre);
                if (ChooseCompanyByCodigo) {
                    vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Gv_Compania_B1.Codigo", Codigo);
                }
                Compania = ChooseRecord<FkCompaniaViewModel>("Contribuyente", vDefaultCriteria, null, "Codigo");
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
            }
        }

        private void ExecuteChooseCompanyByCodigoCommand(string valCodigo) {
            try {
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Gv_Compania_B1.Nombre", valCodigo);
                if (ChooseCompanyByCodigo) {
                    vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Gv_Compania_B1.Codigo", Codigo);
                }
                Compania = ChooseRecord<FkCompaniaViewModel>("Empresa", vDefaultCriteria, null, "Codigo");
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
            }
        }
        //este metodo en su momento deberia llamar a un metodo propio de la capa de negocio de Cia.
        private void SetGlobalValues(FkCompaniaViewModel vCia) {
            XElement vElement = new XElement("Compania",
                    new XElement("ConsecutivoCompania", vCia.ConsecutivoCompania),
                    new XElement("Codigo", vCia.Codigo),
                    new XElement("Nombre", vCia.Nombre),
                    new XElement("NombreCorto", vCia.NombreCorto),
                    new XElement("CodigoPais", vCia.CodigoPais),
                    new XElement("EsAccesibleParaTodosLosUsuarios", LibConvert.BoolToSN(vCia.EsAccesibleParaTodosLosUsuarios))
                    , new XElement("TipoDeContribuyente", vCia.TipoDeContribuyente)
                    , new XElement("Direccion", vCia.Direccion)
                    , new XElement("Estado", vCia.Estado)
                    , new XElement("UsaCostoDeVentas", vCia.UsaCostoDeVentas)
                    , new XElement("EsCatalogoGeneral", vCia.EsCatalogoGeneral)
                    , new XElement("UsaModuloDeActivoFijo", vCia.UsaModuloDeActivoFijo)
                    , new XElement("UsaConexionConIslr", vCia.UsaConexionConIslr)
                    , new XElement("UsaConexionConAxi", vCia.UsaConexionConAxi)
                    , new XElement("UsaCentroDeCostos", vCia.UsaCentroDeCostos)
                    , new XElement("CodigoMoneda", vCia.CodigoMoneda)
                    , new XElement("StatusCompania", vCia.StatusCompania)
                    , new XElement("StatusMonetaryReconversionProcess", vCia.StatusMonetaryReconversionProcess)
                    , new XElement("UsaModuloDeContabilidad", vCia.UsaModuloDeContabilidad)
                    , new XElement("UsaInformesFinancieros", vCia.UsaInformesFinancieros)
                    , new XElement("CodigoCiiu", vCia.CodigoCiiu)
                    , new XElement("ConsecutivoMunicipio", vCia.ConsecutivoMunicipio)
                    , new XElement("CiudadMunicipioCiudad", vCia.CiudadMunicipioCiudad)
                    , new XElement("CodigoMunicipioCiudad", vCia.CodigoMunicipioCiudad)
                    , new XElement("TipoDeContribuyenteIva", vCia.TipoDeContribuyenteIva)
                    , new XElement("PorcentajeDePatente", vCia.PorcentajeDePatente)
                    , new XElement("PersonaFiscal", vCia.PersonaFiscal)
                    , new XElement("EsAgenteDeRetencionIVA", vCia.EsAgenteDeRetencionIVA)
                    , new XElement("TipoDocumentoIdentificacion", vCia.TipoDocumentoIdentificacion)
                    , new XElement("EstaIntegradaConNomina", vCia.EstaIntegradaConNomina)
                    , new XElement("CodigoDeIntegracion", vCia.CodigoDeIntegracion)
                    , new XElement("ConectadaConG360", vCia.ConectadaConG360)
                    , new XElement("ImprentaDigitalUrl", vCia.ConectadaConG360)
                    , new XElement("ImprentaDigitalNombreCampoUsuario", vCia.ConectadaConG360)
                    , new XElement("ImprentaDigitalNombreCampoClave", vCia.ConectadaConG360)
                    , new XElement("ImprentaDigitalUsuario", vCia.ConectadaConG360)
                    , new XElement("ImprentaDigitalClave", vCia.ConectadaConG360)
                    );

            XmlAppMemoryInfo = vElement;
        }
        #endregion

        protected override ILibPdn GetBusinessInstance() {
            return new clsCompaniaBaseNav();
        }

        protected override void ExecuteChooseCommand(string valParameter) {
            try {
                if (LibText.S1IsEqualToS2("ListaCompania", valParameter)) {
                    ExecuteChooseCompanyByNombreCommand("*");
                } else {
                    ExecuteChooseCompanyByNombreCommand(valParameter);
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
            }
        }


        protected override IEnumerable GetListFromModule(string valModuleName, LibSearchCriteria valCriteria, Type valRecordType, string valOrderByMember) {
            return base.GetListFromModule(valModuleName, valCriteria, valRecordType, valOrderByMember);
        }

        public override void InitializeViewModel() {
            base.InitializeViewModel();
            Nombre = null;
        }

    }
    //esta clase es provisional y el exceso de propiedades en ella tambien
    //cuando se tenga el componente propio de Compania en C# debe modificarse
    public class FkCompaniaViewModel {
        [LibGridColum("Codigo")]
        public string Codigo { get; set; }

        [LibGridColum("Nombre", Width = 400)]
        public string Nombre { get; set; }

        public int ConsecutivoCompania { get; set; }
        string _NombreCorto;
        public string NombreCorto {
            get { return string.IsNullOrEmpty(_NombreCorto) ? Nombre : _NombreCorto; }
            set { _NombreCorto = value; }
        }

        public string CodigoPais { get; set; }

        public bool EsAccesibleParaTodosLosUsuarios { get; set; }
        public string TipoDeContribuyente { get; set; }
        public string Direccion { get; set; }
        public string Estado { get; set; }
        public string UsaCostoDeVentas { get; set; }
        public string EsCatalogoGeneral { get; set; }
        public string UsaModuloDeActivoFijo { get; set; }
        public string UsaConexionConIslr { get; set; }
        public string UsaConexionConAxi { get; set; }
        public string UsaAuxiliares { get; set; }
        public string UsaCentroDeCostos { get; set; }
        public string CodigoMoneda { get; set; }
        public string StatusCompania { get; set; }
        public string StatusMonetaryReconversionProcess { get; set; }
        public string UsaModuloDeContabilidad { get; set; }
        public string UsaInformesFinancieros { get; set; }
        public string CodigoCiiu { get; set; }
        public int ConsecutivoMunicipio { get; set; }
        public string CiudadMunicipioCiudad { get; set; }
        public string CodigoMunicipioCiudad { get; set; }
        public string TipoDeContribuyenteIva { get; set; }
        public string PorcentajeDePatente { get; set; }
        public string PersonaFiscal { get; set; }
        public string EsAgenteDeRetencionIVA { get; set; }
        public string TipoDocumentoIdentificacion { get; set; }
        public string EstaIntegradaConNomina { get; set; }
        public string CodigoDeIntegracion { get; set; }
        public string ConectadaConG360 { get; set; }
        public string ImprentaDigitalUrl { get; set; }
        public string ImprentaDigitalNombreCampoUsuario { get; set; }
        public string ImprentaDigitalNombreCampoClave { get; set; }
        public string ImprentaDigitalUsuario { get; set; }
        public string ImprentaDigitalClave { get; set; }

    }


}
