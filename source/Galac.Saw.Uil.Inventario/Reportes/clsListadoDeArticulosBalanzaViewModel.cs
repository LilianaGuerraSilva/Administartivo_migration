using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Report;
using LibGalac.Aos.UI.Cib;
using LibGalac.Aos.UI.Mvvm;
using LibGalac.Aos.UI.Mvvm.Command;
using LibGalac.Aos.UI.Mvvm.Helpers;
using Galac.Saw.Ccl.Inventario;
using Galac.Saw.Brl.Inventario;
using Galac.Saw.Uil.Inventario.ViewModel;
using LibGalac.Aos.UI.Mvvm.Validation;
using Galac.Saw.Brl.Tablas;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Galac.Saw.Uil.Inventario.Reportes {

    public class clsListadoDeArticulosBalanzaViewModel :LibInputRptViewModelBase<ArticuloInventario> {
                      
        #region Constantes
        public const string NombreLineaDeProductoPropertyName = "NombreLineaDeProducto";
        public const string TipoDeInformePropertyName = "TipoDeInforme";
        public const string IsVisibleLineaDeProductoPropertyName = "IsVisibleLineaDeProducto";
        #endregion

        #region Variables
        private FkLineaDeProductoViewModel _ConexionNombreLineaDeProducto = null;
        eListarPorLineaDeProducto _TipoDeInforme;             
        private string _NombreLineaDeProducto;
        private bool _FiltrarPorLineaDeProducto;        
                     
        #endregion //Variables
        #region Propiedades

        public override string DisplayName {
            get { return "Listado de Artículos con Balanza"; }
        }

        public override bool IsSSRS {
            get {
                return false;
            }
        }

        public bool FiltrarPorLineaDeProducto {
            get {
                return _FiltrarPorLineaDeProducto;
            }
            private set {
                _FiltrarPorLineaDeProducto = value;
            }
        }

        public RelayCommand<string> ChooseNombreLineaDeProductoCommand {
            get;
            private set;
        }

        public eListarPorLineaDeProducto TipoDeInforme {
            get {
                return _TipoDeInforme;
            }
            set {
                if(_TipoDeInforme != value) {
                    _TipoDeInforme = value;
                    FiltrarPorLineaDeProducto = (_TipoDeInforme == eListarPorLineaDeProducto.LineaDeProducto);
                    RaisePropertyChanged(TipoDeInformePropertyName);
                    RaisePropertyChanged(NombreLineaDeProductoPropertyName);
                    RaisePropertyChanged(IsVisibleLineaDeProductoPropertyName);
                }
            }
        }

        public eListarPorLineaDeProducto[] ArrayTipoDeInforme {
            get {
                return LibEnumHelper<eListarPorLineaDeProducto>.GetValuesInArray();
            }
        }

        [LibCustomValidation("LineaDeProductoValidating")]
        public string NombreLineaDeProducto {
            get {
                return _NombreLineaDeProducto;
            }
            set {
                if(_NombreLineaDeProducto != value) {
                    _NombreLineaDeProducto = value;
                    RaisePropertyChanged(NombreLineaDeProductoPropertyName);
                    if(LibString.IsNullOrEmpty(NombreLineaDeProducto,true)) {
                        ConexionNombreLineaDeProducto = null;
                    }
                }
            }
        }

        public LibXmlMemInfo AppMemoryInfo { get; set; }

        public LibXmlMFC Mfc { get; set; }
        #endregion //Propiedades
        #region Constructores

        public clsListadoDeArticulosBalanzaViewModel() {
            _TipoDeInforme = eListarPorLineaDeProducto.Todos;
            NombreLineaDeProducto = string.Empty;
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override ILibBusinessSearch GetBusinessComponent() {
            return new clsLineaDeProductoNav();
        }

        protected override void InitializeCommands() {
            base.InitializeCommands();
            ChooseNombreLineaDeProductoCommand = new RelayCommand<string>(ExecuteChooseNombreLineaDeProductoCommand);
        }

        public FkLineaDeProductoViewModel ConexionNombreLineaDeProducto {
            get {
                return _ConexionNombreLineaDeProducto;
            }
            set {
                if(_ConexionNombreLineaDeProducto != value) {
                    _ConexionNombreLineaDeProducto = value;
                    RaisePropertyChanged(NombreLineaDeProductoPropertyName);
                }
                if(_ConexionNombreLineaDeProducto == null) {
                    NombreLineaDeProducto = string.Empty;
                }
            }
        }

        private void ExecuteChooseNombreLineaDeProductoCommand(string valNombre) {
            try {
                if(valNombre == null) {
                    valNombre = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Nombre",valNombre);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("ConsecutivoCompania",LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"));
                ConexionNombreLineaDeProducto = ChooseRecord<FkLineaDeProductoViewModel>("Línea de Producto",vDefaultCriteria,vFixedCriteria,"Nombre");
                if(ConexionNombreLineaDeProducto != null) {
                    NombreLineaDeProducto = ConexionNombreLineaDeProducto.Nombre;
                } else {
                    NombreLineaDeProducto = string.Empty;
                }
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx,DisplayName);
            }
        }

        public bool IsVisibleLineaDeProducto {
            get {
                bool vResult = false;
                vResult = (TipoDeInforme == eListarPorLineaDeProducto.LineaDeProducto);
                return vResult;
            }
        }

        private ValidationResult LineaDeProductoValidating() {            
            ValidationResult vResult = ValidationResult.Success;
            if(TipoDeInforme == eListarPorLineaDeProducto.LineaDeProducto && NombreLineaDeProducto != "") {
                return vResult;
            } else if(TipoDeInforme == eListarPorLineaDeProducto.Todos) {
                return ValidationResult.Success;
            } else {
                vResult = new ValidationResult("La linea de producto es requerida.");
            }            
            return vResult;
        }

        #endregion //Metodos Generados
    } //End of class clsListarArticulosBalanzaViewModel

} //End of namespace Galac.Saw.Uil.Inventario

