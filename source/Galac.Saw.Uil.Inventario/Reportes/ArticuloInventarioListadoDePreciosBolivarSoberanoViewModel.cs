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
using Galac.Saw.Brl.Inventario;
using Galac.Saw.Ccl.Inventario;

namespace Galac.Saw.Uil.Inventario.Reportes {
    public class ArticuloInventarioListadoDePreciosBolivarSoberanoViewModel : LibInputRptViewModelBase<ArticuloInventario> {
        #region Constantes
        public const string NombreLineaDeProductoPropertyName = "NombreLineaDeProducto";
        #endregion
        #region Variables
        private FkLineaDeProductoViewModel _ConexionNombreLineaDeProducto = null;
        #endregion //Variables
        #region Propiedades

        public override string DisplayName {
            get { return "Listado de Precios Bolívar Soberano";}
        }
		
		public LibXmlMemInfo AppMemoryInfo { get; set; }

        public LibXmlMFC Mfc { get; set; }

        public string  NombreLineaDeProducto {
            get {
                return Model.NombreLineaDeProducto;
            }
            set {
                if (Model.NombreLineaDeProducto != value) {
                    Model.NombreLineaDeProducto = value;
                    IsDirty = true;
                    RaisePropertyChanged(NombreLineaDeProductoPropertyName);
                    if (LibString.IsNullOrEmpty(NombreLineaDeProducto, true)) {
                        ConexionNombreLineaDeProducto = null;
                    }
                }
            }
        }

        public FkLineaDeProductoViewModel ConexionNombreLineaDeProducto {
            get {
                return _ConexionNombreLineaDeProducto;
            }
            set {
                if (_ConexionNombreLineaDeProducto != value) {
                    _ConexionNombreLineaDeProducto = value;
                    RaisePropertyChanged(NombreLineaDeProductoPropertyName);
                }
                if (_ConexionNombreLineaDeProducto == null) {
                    NombreLineaDeProducto = string.Empty;
                }
            }
        }

        public RelayCommand<string> ChooseNombreLineaDeProductoCommand {
            get;
            private set;
        }
        #endregion //Propiedades
        #region Constructores
        public ArticuloInventarioListadoDePreciosBolivarSoberanoViewModel()
            : this(new ArticuloInventarioListadoDePreciosBolivarSoberano(), eAccionSR.Insertar) {
        }
        public ArticuloInventarioListadoDePreciosBolivarSoberanoViewModel(ArticuloInventarioListadoDePreciosBolivarSoberano initModel, eAccionSR initAction)
            : base(initModel, initAction, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            DefaultFocusedPropertyName = NombreLineaDeProductoPropertyName;
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override void InitializeLookAndFeel(ArticuloInventarioListadoDePreciosBolivarSoberano valModel) {
            base.InitializeLookAndFeel(valModel);
        }

        protected override ArticuloInventarioListadoDePreciosBolivarSoberano FindCurrentRecord(ArticuloInventarioListadoDePreciosBolivarSoberano valModel) {
            if (valModel == null) {
                return null;
            }
            LibGpParams vParams = new LibGpParams();
            vParams.AddInString("NombreLineaDeProducto", valModel.NombreLineaDeProducto, 30);
            return BusinessComponent.GetData(eProcessMessageType.SpName, "ArticuloInventarioListadoDePreciosBolivarSoberanoGET", vParams.Get()).FirstOrDefault();
        }

        protected override ILibBusinessComponentWithSearch<IList<ArticuloInventario>, IList<ArticuloInventario>> GetBusinessComponent() {
            return new clsArticuloInventarioNav();
        }

        protected override void InitializeCommands() {
            base.InitializeCommands();
            ChooseNombreLineaDeProductoCommand = new RelayCommand<string>(ExecuteChooseNombreLineaDeProductoCommand);
        }

        protected override void ReloadRelatedConnections() {
            base.ReloadRelatedConnections();
            ConexionNombreLineaDeProducto = FirstConnectionRecordOrDefault<FkLineaDeProductoViewModel>("Linea De Producto", LibSearchCriteria.CreateCriteria("Nombre", NombreLineaDeProducto));
        }

        private void ExecuteChooseNombreLineaDeProductoCommand(string valNombre) {
            try {
                if (valNombre == null) {
                    valNombre = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Nombre", valNombre);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("ConsecutivoCompania", Mfc.GetInt("Compania"));
                ConexionNombreLineaDeProducto = ChooseRecord<FkLineaDeProductoViewModel>("Linea De Producto", vDefaultCriteria, vFixedCriteria, string.Empty);
                if (ConexionNombreLineaDeProducto != null) {
                    NombreLineaDeProducto = ConexionNombreLineaDeProducto.Nombre;
                } else {
                    NombreLineaDeProducto = string.Empty;
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }
        #endregion //Metodos Generados


    } //End of class ArticuloInventarioListadoDePreciosBolivarSoberanoViewModel

} //End of namespace Galac.Saw.Uil.Inventario

