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

namespace Galac.Saw.Uil.Cliente.ViewModel {
    public class DireccionDeDespachoViewModel : LibInputDetailViewModelMfc<DireccionDeDespacho> {
        #region Constantes
        public const string ConsecutivoDireccionPropertyName = "ConsecutivoDireccion";
        public const string PersonaContactoPropertyName = "PersonaContacto";
        public const string DireccionPropertyName = "Direccion";
        public const string CiudadPropertyName = "Ciudad";
        public const string ZonaPostalPropertyName = "ZonaPostal";
        #endregion
        #region Variables
        private FkCiudadViewModel _ConexionCiudad = null;
        #endregion //Variables
        #region Propiedades

        public override string ModuleName {
            get { return "Direccion De Despacho"; }
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

        public string  CodigoCliente {
            get {
                return Model.CodigoCliente;
            }
            set {
                if (Model.CodigoCliente != value) {
                    Model.CodigoCliente = value;
                }
            }
        }

        public int  ConsecutivoDireccion {
            get {
                return Model.ConsecutivoDireccion;
            }
            set {
                if (Model.ConsecutivoDireccion != value) {
                    Model.ConsecutivoDireccion = value;
                    IsDirty = true;
                    RaisePropertyChanged(ConsecutivoDireccionPropertyName);
                }
            }
        }

        [LibGridColum("Contacto", MaxLength=20)]
        public string  PersonaContacto {
            get {
                return Model.PersonaContacto;
            }
            set {
                if (Model.PersonaContacto != value) {
                    Model.PersonaContacto = value;
                    IsDirty = true;
                    RaisePropertyChanged(PersonaContactoPropertyName);
                }
            }
        }

        [LibRequired(ErrorMessage = "El campo Direcci?n es requerido.")]
        [LibGridColum("Direcci?n", MaxLength=100)]
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

        [LibGridColum("Ciudad", eGridColumType.Connection, ConnectionDisplayMemberPath = "NombreCiudad", ConnectionModelPropertyName = "Ciudad", ConnectionSearchCommandName = "ChooseCiudadCommand", MaxWidth=120)]
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

        [LibGridColum("Zona Postal", MaxLength=7)]
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

        public ClienteViewModel Master {
            get;
            set;
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

        public RelayCommand<string> ChooseCiudadCommand {
            get;
            private set;
        }
        #endregion //Propiedades
        #region Constructores
        public DireccionDeDespachoViewModel()
            : base(new DireccionDeDespacho(), eAccionSR.Insertar, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
        }
        public DireccionDeDespachoViewModel(ClienteViewModel initMaster, DireccionDeDespacho initModel, eAccionSR initAction)
            : base(initModel, initAction, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            Master = initMaster;
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override void InitializeLookAndFeel(DireccionDeDespacho valModel) {
            base.InitializeLookAndFeel(valModel);
        }

        protected override ILibBusinessDetailComponent<IList<DireccionDeDespacho>, IList<DireccionDeDespacho>> GetBusinessComponent() {
            return new clsDireccionDeDespachoNav();
        }

        protected override void InitializeCommands() {
            base.InitializeCommands();
            ChooseCiudadCommand = new RelayCommand<string>(ExecuteChooseCiudadCommand);
        }

        protected override void ReloadRelatedConnections() {
            base.ReloadRelatedConnections();
            ConexionCiudad = Master.FirstConnectionRecordOrDefault<FkCiudadViewModel>("Ciudad", LibSearchCriteria.CreateCriteria("NombreCiudad", Ciudad));
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
                ConexionCiudad = Master.ChooseRecord<FkCiudadViewModel>("Ciudad", vDefaultCriteria, vFixedCriteria, string.Empty);
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
        #endregion //Metodos Generados


    } //End of class DireccionDeDespachoViewModel

} //End of namespace Galac.Saw.Uil.Cliente

