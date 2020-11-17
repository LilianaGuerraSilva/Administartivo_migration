using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
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
using LibGalac.Aos.Uil;
using System.Xml;
using Galac.Saw.Brl.Tablas;

namespace Galac.Saw.Uil.Inventario.ViewModel {
    public class AsignarBalanzaViewModel : LibGenericViewModel {
        #region Constantes
        public const string LineaDeProductoPropertyName = "LineaDeProducto";
        public const string ArticuloDesdePropertyName = "ArticuloDesde";
        public const string ArticuloHastaPropertyName = "ArticuloHasta";
        public const string TipoDeAsignacionPropertyName = "TipoDeAsignacion";
        public const string IsVisibleLineaDeProductoPropertyName = "IsVisibleLineaDeProducto";
        public const string IsVisibleRangoDeArticulosPropertyName = "IsVisibleRangoDeArticulos";
        public const string TipoDeAccionPropertyName = "TipoDeAccion";
        #endregion
        #region Variables
        private FkLineaDeProductoViewModel _ConexionLineaDeProducto = null;
        private FkArticuloInventarioViewModel _ConexionArticuloDesde = null;
        private FkArticuloInventarioViewModel _ConexionArticuloHasta = null;
        private AsignarBalanza Model;

        #endregion //Variables
        #region Propiedades        

        public override string ModuleName {
            get { return "Inventario Asignar Balanza"; }
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

        [LibCustomValidation("LineaDeProductoValidating")]
        public string  LineaDeProducto {
            get {
                return Model.LineaDeProducto;
            }
            set {
                if (Model.LineaDeProducto != value) {
                    Model.LineaDeProducto = value;                  
                    RaisePropertyChanged(LineaDeProductoPropertyName);
                    if (LibString.IsNullOrEmpty(LineaDeProducto, true)) {
                        ConexionLineaDeProducto = null;
                    }
                }
            }
        }

        [LibCustomValidation("ArticuloDesdeValidating")]
        public string  ArticuloDesde {
            get {
                return Model.ArticuloDesde;
            }
            set {
                if (Model.ArticuloDesde != value) {
                    Model.ArticuloDesde = value;                   
                    RaisePropertyChanged(ArticuloDesdePropertyName);
                    if (LibString.IsNullOrEmpty(ArticuloDesde, true)) {
                        ConexionArticuloDesde = null;
                    }
                }
            }
        }

        [LibCustomValidation("ArticuloHastaValidating")]
        public string  ArticuloHasta {
            get {
                return Model.ArticuloHasta;
            }
            set {
                if (Model.ArticuloHasta != value) {
                    Model.ArticuloHasta = value;                  
                    RaisePropertyChanged(ArticuloHastaPropertyName);
                    if (LibString.IsNullOrEmpty(ArticuloHasta, true)) {
                        ConexionArticuloHasta = null;
                    }
                }
            }
        }

        public eTipoDeAsignacion  TipoDeAsignacion {
            get {
                return Model.TipoDeAsignacionAsEnum;
            }
            set {
                if (Model.TipoDeAsignacionAsEnum != value) {
                    Model.TipoDeAsignacionAsEnum = value;
                    ArticuloDesde = string.Empty;
                    ArticuloHasta = string.Empty;
                    LineaDeProducto = string.Empty;
                    RaisePropertyChanged(TipoDeAsignacionPropertyName);
                    RaisePropertyChanged(IsVisibleLineaDeProductoPropertyName);
                    RaisePropertyChanged(IsVisibleRangoDeArticulosPropertyName);
                    RaisePropertyChanged(LineaDeProductoPropertyName);
                    RaisePropertyChanged(ArticuloDesdePropertyName);
                    RaisePropertyChanged(ArticuloHastaPropertyName);         
                }
            }
        }

        public eTipoDeAccion  TipoDeAccion {
            get {
                return Model.AccionAsEnum;
            }
            set {
                if (Model.AccionAsEnum != value) {
                    Model.AccionAsEnum = value;
                   RaisePropertyChanged(TipoDeAccionPropertyName);
                }
            }
        }

        public eTipoDeAsignacion[] ArrayTipoDeAsignacion {
            get {
                return LibEnumHelper<eTipoDeAsignacion>.GetValuesInArray();
            }
        }

        public eTipoDeAccion[] ArrayTipoDeAccion {
            get {
                return LibEnumHelper<eTipoDeAccion>.GetValuesInArray();
            }
        }

        public FkLineaDeProductoViewModel ConexionLineaDeProducto {
            get {
                return _ConexionLineaDeProducto;
            }
            set {
                if (_ConexionLineaDeProducto != value) {
                    _ConexionLineaDeProducto = value;
                    RaisePropertyChanged(LineaDeProductoPropertyName);
                }
                if (_ConexionLineaDeProducto == null) {
                    LineaDeProducto = string.Empty;
                }
            }
        }

        public FkArticuloInventarioViewModel ConexionArticuloDesde {
            get {
                return _ConexionArticuloDesde;
            }
            set {
                if (_ConexionArticuloDesde != value) {
                    _ConexionArticuloDesde = value;
                    RaisePropertyChanged(ArticuloDesdePropertyName);
                }
                if (_ConexionArticuloDesde == null) {
                    ArticuloDesde = string.Empty;
                }
            }
        }

        public FkArticuloInventarioViewModel ConexionArticuloHasta {
            get {
                return _ConexionArticuloHasta;
            }
            set {
                if (_ConexionArticuloHasta != value) {
                    _ConexionArticuloHasta = value;
                    RaisePropertyChanged(ArticuloHastaPropertyName);
                }
                if (_ConexionArticuloHasta == null) {
                    ArticuloHasta = string.Empty;
                }
            }
        }       

        private bool CanExecuteUpdateCommand() {
            return true;
        }

        public RelayCommand UpdateCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseLineaDeProductoCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseArticuloDesdeCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseArticuloHastaCommand {
            get;
            private set;
        }
        #endregion //Propiedades           
        #region Constructores
        public AsignarBalanzaViewModel()
            : base() {
            Model = new AsignarBalanza();
            TipoDeAccion = eTipoDeAccion.Activar;
            ArticuloDesde = string.Empty;
            ArticuloHasta = string.Empty;
            LineaDeProducto = string.Empty;            
        }
        
        #endregion //Constructores
        #region Metodos Generados            
        
        #endregion //Constructores
        #region Metodos Generados       

        protected override void InitializeRibbon() {
            base.InitializeRibbon();            
            if (RibbonData.TabDataCollection != null && RibbonData.TabDataCollection.Count > 0) {               
                RibbonData.TabDataCollection[0].AddTabGroupData(CreateInsertarRibbonButtonGroup());                
            }
        }       

        protected override void InitializeCommands() {
            base.InitializeCommands();
            ChooseLineaDeProductoCommand = new RelayCommand<string>(ExecuteChooseLineaDeProductoCommand);
            ChooseArticuloDesdeCommand = new RelayCommand<string>(ExecuteChooseArticuloDesdeCommand);
            ChooseArticuloHastaCommand = new RelayCommand<string>(ExecuteChooseArticuloHastaCommand);
            UpdateCommand = new RelayCommand(ExecuteUpdateCommand, CanExecuteUpdateCommand);            
        }        

        private LibRibbonGroupData CreateInsertarRibbonButtonGroup() {
            LibRibbonGroupData vResult = new LibRibbonGroupData("");
            vResult.ControlDataCollection.Add(new LibRibbonButtonData() {
                Label = "Asignar",
                Command = UpdateCommand,
                LargeImage = new Uri("/LibGalac.Aos.UI.WpfRD;component/Images/exec.png",UriKind.Relative),
                ToolTipDescription = "Guarda los cambios en " + ModuleName + ".",
                ToolTipTitle = "Ejecutar Acción (F6)",
                IsVisible = true,
                KeyTip = "F6"
            });
            return vResult;
        }

        private void ExecuteChooseLineaDeProductoCommand(string valLineaDeProducto) {
            try {
                if (valLineaDeProducto == null) {
                    valLineaDeProducto = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteria("Gv_LineaDeProducto_B1.Nombre", valLineaDeProducto);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("Gv_LineaDeProducto_B1.ConsecutivoCompania", LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"));
                ConexionLineaDeProducto = LibFKRetrievalHelper.ChooseRecord<FkLineaDeProductoViewModel>("Línea de Producto",vDefaultCriteria,vFixedCriteria,new clsLineaDeProductoNav(),string.Empty);
                if (ConexionLineaDeProducto != null) {
                    LineaDeProducto = ConexionLineaDeProducto.Nombre;                   
                } else {
                    LineaDeProducto = string.Empty;                   
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void ExecuteChooseArticuloDesdeCommand(string valCodigo) {
            try {
                if (valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteria("dbo.Gv_ArticuloInventario_B1.Codigo", valCodigo);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("dbo.Gv_ArticuloInventario_B1.ConsecutivoCompania", LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"));
                vFixedCriteria.Add(LibSearchCriteria.CreateCriteria("TipoArticuloInv", eTipoArticuloInv.Simple), eLogicOperatorType.And);
                ConexionArticuloDesde = LibFKRetrievalHelper.ChooseRecord<FkArticuloInventarioViewModel>("Articulo Inventario",vDefaultCriteria,vFixedCriteria,new clsArticuloInventarioNav(),string.Empty);
                if (ConexionArticuloDesde != null) {
                    ArticuloDesde = ConexionArticuloDesde.Codigo;                    
                } else {
                    ArticuloDesde = string.Empty;                                   
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void ExecuteChooseArticuloHastaCommand(string valCodigo) {
            try {
                if (valCodigo == null) {
                    valCodigo = string.Empty;
                }                
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteria("dbo.Gv_ArticuloInventario_B1.Codigo", valCodigo);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("dbo.Gv_ArticuloInventario_B1.ConsecutivoCompania", LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"));
                vFixedCriteria.Add(LibSearchCriteria.CreateCriteria("TipoArticuloInv", eTipoArticuloInv.Simple), eLogicOperatorType.And);
                ConexionArticuloHasta = LibFKRetrievalHelper.ChooseRecord<FkArticuloInventarioViewModel>("Articulo Inventario",vDefaultCriteria,vFixedCriteria,new clsArticuloInventarioNav(),string.Empty);
                if (ConexionArticuloHasta != null) {
                    ArticuloHasta = ConexionArticuloHasta.Codigo;                    
                } else {                    
                    ArticuloHasta = string.Empty;
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }    
        
        private void ExecuteUpdateCommand() {
            bool vAsignar = false;
            string vArticuloAux = "";
            vAsignar =(TipoDeAccion==eTipoDeAccion.Activar);
            try {
                if(!IsValid) {
                    LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(new GalacValidationException(Error),ModuleName,ModuleName);
                    return;
                }
                if(LibText.S1IsGreaterThanS2(ArticuloDesde,ArticuloHasta)) {
                    vArticuloAux = ArticuloHasta;
                    ArticuloHasta = ArticuloDesde;
                    ArticuloDesde = vArticuloAux;
                } 
                if(new clsAsignarBalanzaNav().AsignarBalanza(LineaDeProducto,ArticuloDesde,ArticuloHasta,vAsignar)) {
                    LibMessages.MessageBox.Information(null,"La balanza fue asignada en artículos con exíto","");
                } else {
                    throw new GalacException("No se pudo actualizar el articulo, Existen articulos registrados?",eExceptionManagementType.Controlled);
                }
                ArticuloDesde = string.Empty;
                ArticuloHasta = string.Empty;
                LineaDeProducto = string.Empty;
            } catch(System.AccessViolationException) {
                throw;
            } catch(GalacException vEx) {
                if(vEx.ExceptionManagementType == eExceptionManagementType.Validation) {
                    LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.MessageBox.Warning(null,vEx.Message,"Validación de Consistencia");
                } else {
                    LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
                }
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
            }
        }


        protected void ExecuteCommandsRaiseCanExecuteChanged() {
            UpdateCommand.RaiseCanExecuteChanged();
        }

        public bool IsVisibleLineaDeProducto{
            get {
                bool vResult = false;
                vResult = (TipoDeAsignacion == eTipoDeAsignacion.LineaDeProducto);
                return vResult;
            }
        }

        public bool IsVisibleRangoDeArticulos {
            get {
                bool vResult = false;
                vResult = (TipoDeAsignacion == eTipoDeAsignacion.RangoDeArticulos);
                return vResult;
            }
        }

        private ValidationResult LineaDeProductoValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if(TipoDeAsignacion == eTipoDeAsignacion.LineaDeProducto && LineaDeProducto != "") {
                return vResult;
            } else if(TipoDeAsignacion == eTipoDeAsignacion.RangoDeArticulos || TipoDeAsignacion == eTipoDeAsignacion.Todos) {
                return ValidationResult.Success;
            } else {
                vResult = new ValidationResult("La linea de producto es requerida.");
            }
            return vResult;
        }

        private ValidationResult ArticuloDesdeValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if(TipoDeAsignacion == eTipoDeAsignacion.RangoDeArticulos && ArticuloDesde != string.Empty) {
                return vResult;
            } else if(TipoDeAsignacion == eTipoDeAsignacion.LineaDeProducto || TipoDeAsignacion == eTipoDeAsignacion.Todos) {
                return ValidationResult.Success;
            } else {
                vResult = new ValidationResult("Articulo Requerido.");
            }
            return vResult;
        }

        private ValidationResult ArticuloHastaValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if(TipoDeAsignacion == eTipoDeAsignacion.RangoDeArticulos && ArticuloHasta != string.Empty) {
            } else if(TipoDeAsignacion == eTipoDeAsignacion.LineaDeProducto || TipoDeAsignacion == eTipoDeAsignacion.Todos) {
                return ValidationResult.Success;              
            } else {
                vResult = new ValidationResult("Articulo Requerido.");
            }
            return vResult;
        }
        #endregion //Metodos Generados

    } //End of class AsignarBalanzaViewModel

} //End of namespace Galac.Saw.Uil.Inventario

