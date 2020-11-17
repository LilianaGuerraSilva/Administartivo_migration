using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Report;
using LibGalac.Aos.UI.Mvvm;
using LibGalac.Aos.UI.Mvvm.Command;
using LibGalac.Aos.UI.Mvvm.Helpers;
using Galac.Saw.Ccl.Inventario;
using Galac.Saw.Brl.Inventario;
using Galac.Saw.Uil.Inventario.ViewModel;
using Galac.Saw.Brl.Tablas;

namespace Galac.Saw.Uil.Inventario.Reportes {

    public class clsListadoDePreciosViewModel : LibInputRptViewModelBase<ArticuloInventario> {
        #region Constantes
        public const string NombreLineaDeProductoPropertyName = "NombreLineaDeProducto";
        #endregion
        #region Variables
        #region Codigo Ejemplo
        /* Codigo de Ejemplo
        public const string CantidadAImprimirPropertyName = "CantidadAImprimir";
        private eCantidadAImprimir _CantidadAImprimir;
        */
        #endregion //Codigo Ejemplo
        private FkLineaDeProductoViewModel _ConexionNombreLineaDeProducto = null;
        string _NombreLineaDeProducto;
        #endregion //Variables
        #region Propiedades

        public override string DisplayName {
            get { return "Listado de Precios Bolívar Soberano";}
        }

        public LibXmlMemInfo AppMemoryInfo { get; set; }

        public LibXmlMFC Mfc { get; set; }
             
        public string NombreLineaDeProducto {
            get {
                return _NombreLineaDeProducto;
            }
            set {
                if (_NombreLineaDeProducto != value) {
                    _NombreLineaDeProducto = value;
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

        public override bool IsSSRS {
            get {
                return false;
            }
        }
        #endregion //Propiedades
        #region Constructores

        public clsListadoDePreciosViewModel() {
        #region Codigo Ejemplo
        /* Codigo de Ejemplo
            FechaDesde = LibDate.AddsNMonths(LibDate.Today(), - 1, false);
            FechaHasta = LibDate.Today();
        */
        #endregion //Codigo Ejemplo
        }
        #endregion //Constructores
        #region Metodos Generados
        
        protected override ILibBusinessSearch GetBusinessComponent() {
            return new clsLineaDeProductoNav();
        }        
        #endregion //Metodos Generados
        protected override void InitializeCommands() {
            base.InitializeCommands();
            ChooseNombreLineaDeProductoCommand = new RelayCommand<string>(ExecuteChooseNombreLineaDeProductoCommand);
        }
        private void ExecuteChooseNombreLineaDeProductoCommand(string valNombre) {
            try {
                if (valNombre == null) {
                    valNombre = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Nombre", valNombre);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("ConsecutivoCompania", LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"));
                ConexionNombreLineaDeProducto = ChooseRecord<FkLineaDeProductoViewModel>("Línea de Producto", vDefaultCriteria, vFixedCriteria, "Nombre");
                if (ConexionNombreLineaDeProducto != null) {
                    NombreLineaDeProducto = ConexionNombreLineaDeProducto.Nombre;
                } else {
                    NombreLineaDeProducto = string.Empty;
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, DisplayName);
            }
        }

       
    } //End of class clsListadoDePreciosViewModel

} //End of namespace Galac.Saw.Uil.Inventario

