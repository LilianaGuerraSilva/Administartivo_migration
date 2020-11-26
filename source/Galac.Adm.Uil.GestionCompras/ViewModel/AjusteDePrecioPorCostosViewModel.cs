using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Galac.Adm.Ccl.GestionCompras;
using Galac.Saw.Ccl.SttDef;
using Galac.Saw.Ccl.Inventario;
using Galac.Saw.Brl.Inventario;
using LibGalac.Aos.Uil;
using Galac.Saw.Brl.Tablas;
using Galac.Adm.Brl.GestionCompras;

namespace Galac.Adm.Uil.GestionCompras.ViewModel {
    public class AjusteDePrecioPorCostosViewModel : LibGenericViewModel  {

        #region Constantes
        const string RedondearPrecioPropertyName = "RedondearPrecio";
        const string NivelDePrecioPropertyName = "NivelDePrecio";
        const string PrecioAjustarPropertyName = "PrecioAjustar";
        const string EstablecerMargenPropertyName = "EstablecerMargen";
        const string Margen1PropertyName = "Margen1";
        const string Margen2PropertyName = "Margen2";
        const string Margen3PropertyName = "Margen3";
        const string Margen4PropertyName = "Margen4";
        const string UsaFormulaAlternaPropertyName = "UsaFormulaAlterna";

        public const string LineaDeProductoOptionPropertyName = "LineaDeProductoOption";
        public const string CategoriaOptionPropertyName = "CategoriaOption";
        public const string MarcaOptionPropertyName = "MarcaOption";
        public const string DesdePropertyName = "Desde";
        public const string HastaPropertyName = "Hasta";
        public const string NombreLineaDeProductoComboPropertyName = "NombreLineaDeProductoCombo";
        public const string CategoriaComboPropertyName = "CategoriaCombo";
        public const string LineaDeProductoPropertyName = "NombreLineaDeProducto";
        public const string CategoriaPropertyName = "Categoria";
        public const string MarcaPropertyName = "Marca";
        public const string TipoDeInfoPropertyName = "TipoDeInfo";

        #endregion
        #region Variables
        private eNivelDePrecio _NivelDePrecio;       
        private eRedondearPrecio _RedondearPrecio;
        private ePrecioAjustar _PrecioAjustar;       
        private bool _EstablecerMargen;
        private decimal _Margen1;       
        private decimal _Margen2;       
        private decimal _Margen3;     
        private decimal _Margen4;       
        private bool _UsaFormulaAlterna;
        private string _NumeroOperacion;
        private string _TipoDeInfo;
        private DateTime _FechaOperacion;
        private bool _VieneDeCompra;


        private FkArticuloInventarioViewModel _ConexionDesde = null;
        private FkArticuloInventarioViewModel _ConexionHasta = null;
        private FkLineaDeProductoViewModel _ConexionLineaDeProducto = null;
        private FkCategoriaViewModel _ConexionCategoria = null;
        private string _Marca;
        private AjusteDePrecioPorCostos Model;
        private string _IsVisibleGB;
        private string IsVisibleGBPropertyName;
        private bool _EsMonedaLocal;
        private string EsMonedaLocalPropertyName;

        #endregion
        #region Propiedades

        public override string ModuleName {
            get { return "Ajuste De Precio Por Costos"; }
        }

        public eNivelDePrecio NivelDePrecio {
            get {
                return _NivelDePrecio;
            }
            set {
                if (_NivelDePrecio != value) {
                    _NivelDePrecio = value;
                    RaisePropertyChanged(NivelDePrecioPropertyName);
                    RaisePropertyChanged("IsEnabledMarge1");
                    RaisePropertyChanged("IsEnabledMarge2");
                    RaisePropertyChanged("IsEnabledMarge3");
                    RaisePropertyChanged("IsEnabledMarge4");
                }
            }
        }

        public eRedondearPrecio RedondearPrecio {
            get {
                return _RedondearPrecio;
            }
            set {
                if (_RedondearPrecio != value) {
                    _RedondearPrecio = value;
                    RaisePropertyChanged(RedondearPrecioPropertyName);
                     RaisePropertyChanged("IsVisiblePrecioAjustar");
                }  
            }
        }

        public ePrecioAjustar PrecioAjustar {
            get {
                return _PrecioAjustar;
            }
            set {
                if (_PrecioAjustar != value) {
                    _PrecioAjustar = value;
                     RaisePropertyChanged(PrecioAjustarPropertyName);
                   
                }
            }
        }

        public bool  EstablecerMargen {
            get {
                return _EstablecerMargen;
            }
            set {
                if (_EstablecerMargen != value) {
                    _EstablecerMargen = value;
                    RaisePropertyChanged(EstablecerMargenPropertyName);
                    RaisePropertyChanged("IsVisibleEstablecerMargen");
                    RaisePropertyChanged("ChangeInfoFromLabel");
                    RaisePropertyChanged("IsEnabledMarge1");
                    RaisePropertyChanged("IsEnabledMarge2");
                    RaisePropertyChanged("IsEnabledMarge3");
                    RaisePropertyChanged("IsEnabledMarge4");
                }
            }
        }


        public bool EsMonedaLocal {
            get {
                return _EsMonedaLocal;
            }
            set {
                if(_EsMonedaLocal != value) {
                    _EsMonedaLocal = value;
                    RaisePropertyChanged(EsMonedaLocalPropertyName);

                }
            }
        }

        public decimal Margen1 {
            get {
                return _Margen1;
            }
            set {
                if (_Margen1 != value) {
                    _Margen1 = value;
                    RaisePropertyChanged(Margen1PropertyName);
                }
            }
        }

        public decimal Margen2 {
            get {
                return _Margen2;
            }
            set {
                if (_Margen2 != value) {
                    _Margen2 = value;
                    RaisePropertyChanged(Margen2PropertyName);
                }
            }
        }

        public decimal Margen3 {
            get {
                return _Margen3;
            }
            set {
                if (_Margen3 != value) {
                    _Margen3 = value;
                    RaisePropertyChanged(Margen3PropertyName);
                }
            }
        }

        public decimal Margen4 {
            get {
                return _Margen4;
            }
            set {
                if (_Margen4 != value) {
                    _Margen4 = value;
                    RaisePropertyChanged(Margen4PropertyName);
                }
            }
        }

        public bool UsaFormulaAlterna {
            get {
                return _UsaFormulaAlterna;
            }
            set {
                if (_UsaFormulaAlterna != value) {
                    _UsaFormulaAlterna = value;
                    RaisePropertyChanged(UsaFormulaAlternaPropertyName);
                    RaisePropertyChanged("ChangeInfoFromLabel");
                }
            }
        }

        public bool LineaDeProductoOption {
            get {
                return Model.LineaDeProductoOptionAsBool;
            }
            set {
                if(Model.LineaDeProductoOptionAsBool != value) {
                    Model.LineaDeProductoOptionAsBool = value;
                    RaisePropertyChanged(LineaDeProductoOptionPropertyName);
                    RaisePropertyChanged("IsVisibleLineaDeProducto");
                }
            }
        }

        public bool CategoriaOption {
            get {
                return Model.CategoriaOptionAsBool;
            }
            set {
                if(Model.CategoriaOptionAsBool != value) {
                    Model.CategoriaOptionAsBool = value;

                    RaisePropertyChanged(CategoriaOptionPropertyName);
                    RaisePropertyChanged("IsVisibleCategoria");
                }
            }
        }

        public bool MarcaOption {
            get {
                return Model.MarcaOptionAsBool;
            }
            set {
                if(Model.MarcaOptionAsBool != value) {
                    Model.MarcaOptionAsBool = value;

                    RaisePropertyChanged(MarcaOptionPropertyName);
                    RaisePropertyChanged("IsVisibleMarca");
                }
            }
        }

        public string Desde {
            get {
                return Model.Desde;
            }
            set {
                if(Model.Desde != value) {
                    Model.Desde = value;

                    RaisePropertyChanged(DesdePropertyName);
                    if(LibString.IsNullOrEmpty(Desde,true)) {
                        ConexionDesde = null;
                    }
                }
            }
        }

        public string Hasta {
            get {
                return Model.Hasta;
            }
            set {
                if(Model.Hasta != value) {
                    Model.Hasta = value;

                    RaisePropertyChanged(HastaPropertyName);
                    if(LibString.IsNullOrEmpty(Hasta,true)) {
                        ConexionHasta = null;
                    }
                }
            }
        }

        public eBuscarPor NombreLineaDeProductoCombo {
            get {
                return Model.NombreLineaDeProductoComboAsEnum;
            }
            set {
                if(Model.NombreLineaDeProductoComboAsEnum != value) {
                    Model.NombreLineaDeProductoComboAsEnum = value;

                    RaisePropertyChanged(NombreLineaDeProductoComboPropertyName);
                }
            }
        }

        public eBuscarPor CategoriaCombo {
            get {
                return Model.CategoriaComboAsEnum;
            }
            set {
                if(Model.CategoriaComboAsEnum != value) {
                    Model.CategoriaComboAsEnum = value;

                    RaisePropertyChanged(CategoriaComboPropertyName);
                }
            }
        }

        public string NombreLineaDeProducto {
            get {
                return Model.NombreLineaDeProducto;
            }
            set {
                if(Model.NombreLineaDeProducto != value) {
                    Model.NombreLineaDeProducto = value;

                    RaisePropertyChanged(LineaDeProductoPropertyName);
                    if(LibString.IsNullOrEmpty(NombreLineaDeProducto,true)) {
                        ConexionLineaDeProducto = null;
                    }
                }
            }
        }

        public string Categoria {
            get {
                return Model.Categoria;
            }
            set {
                if(Model.Categoria != value) {
                    Model.Categoria = value;

                    RaisePropertyChanged(CategoriaPropertyName);
                    if(LibString.IsNullOrEmpty(Categoria,true)) {
                        ConexionCategoria = null;
                    }
                }
            }
        }

        public string IsVisibleGB {
            get {
                return _IsVisibleGB;
            }
            set {
                if(_IsVisibleGB != value) {
                    _IsVisibleGB = value;
                    RaisePropertyChanged(IsVisibleGBPropertyName);
                }
            }
        }


        public string Marca {

            get {
                return _Marca;
            }
            set {
                if(_Marca != value) {
                    _Marca = value;
                    RaisePropertyChanged(MarcaPropertyName);
                }
            }
        }

        public string TipoDeInfo {

            get {
                return _TipoDeInfo;
            }
            set {
                if(_TipoDeInfo != value) {
                    _TipoDeInfo = value;
                    RaisePropertyChanged(TipoDeInfoPropertyName);
                }
            }
        }

        public eNivelDePrecio[] ArrayNivelDePrecio {
            get {
                return LibEnumHelper<eNivelDePrecio>.GetValuesInArray();
            }
        }

        public eRedondearPrecio[] ArrayRedondearPrecio {
            get {
                return LibEnumHelper<eRedondearPrecio>.GetValuesInArray();
            }
        }

       
      
        public ePrecioAjustar[] ArrayPrecioAjustar {
            get {
                return LibEnumHelper<ePrecioAjustar>.GetValuesInArray();
            }
        }

        public eBuscarPor[] ArrayBuscarPor {
            get {
                return LibEnumHelper<eBuscarPor>.GetValuesInArray();
            }
        }

        public bool IsVisiblePrecioAjustar {
            get {
                return RedondearPrecio != eRedondearPrecio.SinRedondear;
            }
        }

        public bool IsVisibleEstablecerMargen {
            get {
                return EstablecerMargen;
            }
        }


        public bool IsVisibleLineaDeProducto {
            get {
                return LineaDeProductoOption;
            }
        }

        public bool IsVisibleMarca {
            get {
                return MarcaOption;
            }
        }

        public bool IsVisibleCategoria {
            get {
                return CategoriaOption;
            }
        }

        public bool IsEnabledMarge1
        {
            get
            {
                return (EstablecerMargen && NivelDePrecio == eNivelDePrecio.Nivel1) || (EstablecerMargen && NivelDePrecio == eNivelDePrecio.Todos);
            }
        }

        public bool IsEnabledMarge2 {
            get {
                return (EstablecerMargen && NivelDePrecio == eNivelDePrecio.Nivel2) || (EstablecerMargen && NivelDePrecio == eNivelDePrecio.Todos);
            }
        }

        public bool IsEnabledMarge3 {
            get {
                return (EstablecerMargen && NivelDePrecio == eNivelDePrecio.Nivel3) || (EstablecerMargen && NivelDePrecio == eNivelDePrecio.Todos);
            }
        }

        public bool IsEnabledMarge4 {
            get {
                return (EstablecerMargen && NivelDePrecio == eNivelDePrecio.Nivel4) || (EstablecerMargen && NivelDePrecio == eNivelDePrecio.Todos);
            }
        }

        public string ChangeInfoFromLabel {
            get {
                if(!EstablecerMargen && UsaFormulaAlterna) {
                    return "Se ajustarán los precios basado en los márgenes ya establecidos en la ficha del artículo \ny usando fórmula alterna.";
                } else if(EstablecerMargen && !UsaFormulaAlterna) {
                    return "Se ajustarán los precios basado en los nuevos márgenes.";
                } else if(EstablecerMargen && UsaFormulaAlterna) {
                    return "Se ajustarán los precios basado en los nuevos márgenes y usando la fórmula alterna.";
                } else {
                    return "Se ajustarán los precios basado en los márgenes ya establecidos en la ficha del artículo.";
                }
            }
        }

        public FkArticuloInventarioViewModel ConexionDesde {
            get {
                return _ConexionDesde;
            }
            set {
                if(_ConexionDesde != value) {
                    _ConexionDesde = value;
                    RaisePropertyChanged(DesdePropertyName);
                }
                if(_ConexionDesde == null) {
                    Desde = string.Empty;
                }
            }
        }

        public FkArticuloInventarioViewModel ConexionHasta {
            get {
                return _ConexionHasta;
            }
            set {
                if(_ConexionHasta != value) {
                    _ConexionHasta = value;
                    RaisePropertyChanged(HastaPropertyName);
                }
                if(_ConexionHasta == null) {
                    Hasta = string.Empty;
                }
            }
        }

        public FkLineaDeProductoViewModel ConexionLineaDeProducto {
            get {
                return _ConexionLineaDeProducto;
            }
            set {
                if(_ConexionLineaDeProducto != value) {
                    _ConexionLineaDeProducto = value;
                    RaisePropertyChanged(LineaDeProductoPropertyName);
                }
                if(_ConexionLineaDeProducto == null) {
                    NombreLineaDeProducto = string.Empty;
                }
            }
        }

        public FkCategoriaViewModel ConexionCategoria {
            get {
                return _ConexionCategoria;
            }
            set {
                if(_ConexionCategoria != value) {
                    _ConexionCategoria = value;
                    RaisePropertyChanged(CategoriaPropertyName);
                }
                if(_ConexionCategoria == null) {
                    Categoria = string.Empty;
                }
            }
        }

        public RelayCommand GrabarCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseDesdeCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseHastaCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseNombreLineaDeProductoCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseCategoriaCommand {
            get;
            private set;
        }

        #endregion //Propiedades
        #region Constructores
        public AjusteDePrecioPorCostosViewModel(string  initNumeroOperacion, DateTime initFechaOperacion, bool initVieneDeCompra, bool valEsMonedaLocal)
            : base() {
                Model = new AjusteDePrecioPorCostos();
            
                _NumeroOperacion = initNumeroOperacion;
                _FechaOperacion = initFechaOperacion;
                _VieneDeCompra = initVieneDeCompra;
            IsVisibleGB = initVieneDeCompra ? "Collapsed" : "Visible";
            EsMonedaLocal = valEsMonedaLocal;
        }

        public AjusteDePrecioPorCostosViewModel()
            : base() {
            Title = ModuleName;
            IsVisibleGB = "Visible";
            try {

            } catch(Exception vEx) {
                LibMessages.MessageBox.Alert(this,vEx.Message,"");
            }
        }
        #endregion //Constructores

        #region Metodos Generados

        #endregion //Metodos Generados

        protected override void InitializeCommands() {
            base.InitializeCommands();
            GrabarCommand = new RelayCommand(ExecuteGrabarCommand);
            ChooseDesdeCommand = new RelayCommand<string>(ExecuteChooseDesdeCommand);
            ChooseHastaCommand = new RelayCommand<string>(ExecuteChooseHastaCommand);
            ChooseNombreLineaDeProductoCommand = new RelayCommand<string>(ExecuteChooseNombreLineaDeProductoCommand);
            ChooseCategoriaCommand = new RelayCommand<string>(ExecuteChooseCategoriaCommand);
            //ChooseMarcaCommand = new RelayCommand<string>(ExecuteChooseMarcaCommand);
        }

        protected override void InitializeRibbon() {
            base.InitializeRibbon();
            if (RibbonData.TabDataCollection != null && RibbonData.TabDataCollection.Count > 0) {
                RibbonData.TabDataCollection[0].GroupDataCollection[0].ControlDataCollection.Insert(0, CreateExecuteActionRibbonButtonData());
            }
        }

        private LibRibbonButtonData CreateExecuteActionRibbonButtonData() {
            LibRibbonButtonData vButton = new LibRibbonButtonData() {
                Label = "Grabar",
                LargeImage = new Uri("/LibGalac.Aos.UI.WpfRD;component/Images/saveAndClose.png", UriKind.Relative),
                Command = GrabarCommand,
                ToolTipDescription = "Ejecutar Grabar",
                ToolTipTitle = "Grabar" + " (F6)",
                IsVisible = true
            };
            return vButton;
        }

        internal void ExecuteShowCommand()
        {
            try {
                AjusteDePrecioPorCostosViewModel insAjusteDePrecioPorCosto = new AjusteDePrecioPorCostosViewModel();
                LibMessages.EditViewModel.ShowEditor(insAjusteDePrecioPorCosto,true);
            } catch(Exception vEx) {
                LibMessages.MessageBox.Alert(this,vEx.Message,"");
            }
        }

        private void ExecuteGrabarCommand() {
            try {
                bool vRespuesta = true;
                if (!IsValid) {
                      LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(new GalacValidationException(Error), ModuleName, ModuleName);
                      return;
                }
                if (UsaFormulaAlterna) {
                    if (EstablecerMargen && (Margen1 >= 100 || Margen2 >= 100 || Margen3 >= 100 || Margen4 >= 100)) {
                        LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(new GalacValidationException("Con la formula alterna no podrá utilizar márgenes superiores o iguales a 100"), ModuleName, ModuleName);
                        return;
                    }
                }
                if (EstablecerMargen && NivelDePrecio == eNivelDePrecio.Todos) {
                    if (Margen1 == 0 || Margen2 == 0 || Margen3 == 0 || Margen4 == 0) {
                        vRespuesta = LibMessages.MessageBox.YesNo(this,"Se procederá a ajustar los precios de los Artículos." + Environment.NewLine + "Los márgenes de ganancias tiene un valor de 0%." + Environment.NewLine + "Se realizará la operación sin afectar los márgenes actuales.",ModuleName);
                        if (vRespuesta) {
                            EjecutaAjustePorCostos();
                        }
                    } else {
                        EjecutaAjustePorCostos();
                    }
                } else {
                    EjecutaAjustePorCostos();
                }
                if(vRespuesta) {
                    DialogResult = true;
                    RaiseRequestCloseEvent();
                }
                
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
            }
        }

        private void ExecuteChooseDesdeCommand(string valCodigo)
        {
            try {
                if(valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo",valCodigo);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("dbo.Gv_ArticuloInventario_B1.ConsecutivoCompania",LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"));
                ConexionDesde = LibFKRetrievalHelper.ChooseRecord<FkArticuloInventarioViewModel>("Articulo Inventario",vDefaultCriteria,vFixedCriteria,new clsArticuloInventarioNav(),string.Empty);

                if(ConexionDesde != null) {
                    Desde = ConexionDesde.Codigo;
                } else {
                    Desde = string.Empty;
                }
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx,ModuleName);
            }
        }

        private void ExecuteChooseHastaCommand(string valCodigo)
        {
            try {
                if(valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo",valCodigo);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("dbo.Gv_ArticuloInventario_B1.ConsecutivoCompania",LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"));
                ConexionHasta = LibFKRetrievalHelper.ChooseRecord<FkArticuloInventarioViewModel>("Articulo Inventario",vDefaultCriteria,vFixedCriteria,new clsArticuloInventarioNav(),string.Empty);
                if(ConexionHasta != null) {
                    Hasta = ConexionHasta.Codigo;
                } else {
                    Hasta = string.Empty;
                }
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx,ModuleName);
            }
        }

        private void ExecuteChooseNombreLineaDeProductoCommand(string valLineaDeProducto)
        {
            try {
                if(valLineaDeProducto == null) {
                    valLineaDeProducto = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteria("Adm.Gv_LineaDeProducto_B1.Nombre",valLineaDeProducto);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("Adm.Gv_LineaDeProducto_B1.ConsecutivoCompania",LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"));
                ConexionLineaDeProducto = LibFKRetrievalHelper.ChooseRecord<FkLineaDeProductoViewModel>("Línea de Producto",vDefaultCriteria,vFixedCriteria,new clsLineaDeProductoNav(),string.Empty);
                if(ConexionLineaDeProducto != null) {
                    NombreLineaDeProducto = ConexionLineaDeProducto.Nombre;
                } else {
                    NombreLineaDeProducto = string.Empty;
                }
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx,ModuleName);
            }
        }

        private void ExecuteChooseCategoriaCommand(string valDescripcion)
        {
            try {
                if(valDescripcion == null) {
                    valDescripcion = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Descripcion",valDescripcion);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("Saw.Gv_Categoria_B1.ConsecutivoCompania",LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"));
                ConexionCategoria = LibFKRetrievalHelper.ChooseRecord<FkCategoriaViewModel>("Categoria",vDefaultCriteria,vFixedCriteria,new clsCategoriaNav(),string.Empty);
                if(ConexionCategoria != null) {
                    Categoria = ConexionCategoria.Descripcion;
                } else {
                    Categoria = string.Empty;
                }
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx,ModuleName);
            }
        }

        //private void ExecuteChooseMarcaCommand(string valNombre)
        //{
        //    try {
        //        if(valNombre == null) {
        //            valNombre = string.Empty;
        //        }
        //        LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Nombre",valNombre);
        //        //LibSearchCriteria vFixedCriteria = null;
        //        LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("Saw.Gv_Marca_B1.Nombre",valNombre);
        //        #region Codigo Ejemplo
        //        /* Codigo de Ejemplo
        //                vFixedCriteria = LibSearchCriteria.CreateCriteria("NombreCampoEnLaTablaConLaQueSeConecta", valorAUsarComoFiltroFijo);
        //        */
        //        #endregion //Codigo Ejemplo
        //        ConexionMarca = LibFKRetrievalHelper.ChooseRecord<FkMarcaViewModel>("Marca",vDefaultCriteria,vFixedCriteria,new Brl.Vehiculo.clsMarcaNav(),string.Empty);
        //        if(ConexionMarca != null) {
        //            Marca = ConexionMarca.Nombre;
        //        } else {
        //            Marca = string.Empty;
        //        }
        //    } catch(System.AccessViolationException) {
        //        throw;
        //    } catch(System.Exception vEx) {
        //        LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx,ModuleName);
        //    }
        //}

        private void EjecutaAjustePorCostos() {
            bool vMonedaLocal = EsMonedaLocal;
            ICompraPdn vPdn = new clsCompraNav();
            if (vPdn.AjustaPreciosxCostos(UsaFormulaAlterna, LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"), RedondearPrecio, PrecioAjustar, EstablecerMargen, NivelDePrecio == eNivelDePrecio.Todos || NivelDePrecio == eNivelDePrecio.Nivel1, NivelDePrecio == eNivelDePrecio.Todos || NivelDePrecio == eNivelDePrecio.Nivel2, NivelDePrecio == eNivelDePrecio.Todos || NivelDePrecio == eNivelDePrecio.Nivel3, NivelDePrecio == eNivelDePrecio.Todos || NivelDePrecio == eNivelDePrecio.Nivel4, Margen1, Margen2, Margen3, Margen4, _FechaOperacion, _NumeroOperacion, vMonedaLocal)) {
                LibMessages.MessageBox.Information(this, "Todos los precios de los Artículos fueron actualizados!" + Environment.NewLine + "Recuerde modificar los datos en los borradores de las facturas.", ModuleName);
            }
        } 
    } //End of class CompraSerialRolloViewModel

} //End of namespace Galac.Adm.Uil.GestionCompras

