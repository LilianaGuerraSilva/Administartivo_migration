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
using Galac.Saw.Brl.Vehiculo;
using Galac.Saw.Ccl.Vehiculo;

namespace Galac.Saw.Uil.Vehiculo.ViewModel {
    public class ModeloViewModel : LibInputViewModel<Modelo> {
        #region Constantes
        public const string NombrePropertyName = "Nombre";
        public const string MarcaPropertyName = "Marca";
        #endregion
        #region Variables
        private FkMarcaViewModel _ConexionMarca = null;
        #endregion //Variables
        #region Propiedades

        public override string ModuleName {
            get { return "Modelo"; }
        }

        [LibRequired(ErrorMessage = "El campo Nombre es requerido.")]
        [LibGridColum("Nombre", Width = 300, DbMemberPath = "Saw.Gv_Modelo_B1.Nombre")]
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

        [LibRequired(ErrorMessage = "El campo Marca es requerido.")]
        [LibGridColum("Marca", eGridColumType.Connection, Width = 300, DbMemberPath = "Saw.Gv_Marca_B1.Nombre", ConnectionDisplayMemberPath = "Nombre", ConnectionModelPropertyName = "ConexionMarca", ConnectionSearchCommandName = "ChooseMarcaCommand")]
        public string  Marca {
            get {
                return Model.Marca;
            }
            set {
                if (Model.Marca != value) {
                    Model.Marca = value;
                    IsDirty = true;
                    RaisePropertyChanged(MarcaPropertyName);
                }
            }
        }

        public FkMarcaViewModel ConexionMarca {
            get {
                return _ConexionMarca;
            }
            set {
                if (_ConexionMarca != value) {
                    _ConexionMarca = value;
                    RaisePropertyChanged(MarcaPropertyName);
                }
            }
        }

        public RelayCommand<string> ChooseMarcaCommand {
            get;
            private set;
        }
        #endregion //Propiedades
        #region Constructores
        public ModeloViewModel()
            : this(new Modelo(), eAccionSR.Insertar) {
        }
        public ModeloViewModel(Modelo initModel, eAccionSR initAction)
            : base(initModel, initAction) {
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override void InitializeLookAndFeel(Modelo valModel) {
            base.InitializeLookAndFeel(valModel);
        }

        protected override Modelo FindCurrentRecord(Modelo valModel) {
            if (valModel == null) {
                return null;
            }
            LibGpParams vParams = new LibGpParams();
            vParams.AddInString("Nombre", valModel.Nombre, 20);
            return BusinessComponent.GetData(eProcessMessageType.SpName, "ModeloGET", vParams.Get()).FirstOrDefault();
        }

        protected override ILibBusinessComponentWithSearch<IList<Modelo>, IList<Modelo>> GetBusinessComponent() {
            return new clsModeloNav();
        }

        protected override void InitializeCommands() {
            base.InitializeCommands();
            ChooseMarcaCommand = new RelayCommand<string>(ExecuteChooseMarcaCommand);
        }

        protected override void ReloadRelatedConnections() {
            base.ReloadRelatedConnections();
            ConexionMarca = FirstConnectionRecordOrDefault<FkMarcaViewModel>("Marca", LibSearchCriteria.CreateCriteria("Nombre", Marca));
        }

        internal void ExecuteChooseMarcaCommand(string valNombre) {
            try {
                if (valNombre == null) {
                    valNombre = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Nombre", valNombre);
                LibSearchCriteria vFixedCriteria = null;
        #region Codigo Ejemplo
        /* Codigo de Ejemplo
                vFixedCriteria = LibSearchCriteria.CreateCriteria("NombreCampoEnLaTablaConLaQueSeConecta", valorAUsarComoFiltroFijo);
        */
        #endregion //Codigo Ejemplo
                ConexionMarca = ChooseRecord<FkMarcaViewModel>("Marca", vDefaultCriteria, vFixedCriteria, string.Empty);
                if (ConexionMarca != null) {
                    Marca = ConexionMarca.Nombre;
                } else {
                    Marca = string.Empty;
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }
        #endregion //Metodos Generados


    } //End of class ModeloViewModel

} //End of namespace Galac.Saw.Uil.Vehiculo

