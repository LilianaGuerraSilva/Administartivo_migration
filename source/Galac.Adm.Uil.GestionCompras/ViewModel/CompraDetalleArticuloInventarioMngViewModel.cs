using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using LibGalac.Aos.UI.Mvvm;
using LibGalac.Aos.UI.Mvvm.Command;
using LibGalac.Aos.UI.Mvvm.Messaging;
using LibGalac.Aos.UI.Mvvm.Ribbon;
using LibGalac.Aos.UI.Mvvm.Helpers;
using LibGalac.Aos.Brl.Contracts;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Report;
using LibGalac.Aos.Catching;
using LibGalac.Aos.ARRpt.Reports;
using Galac.Adm.Brl.GestionCompras;
using Galac.Adm.Brl.GestionCompras.Reportes;
using Galac.Adm.Ccl.GestionCompras;

namespace Galac.Adm.Uil.GestionCompras.ViewModel {

    public class CompraDetalleArticuloInventarioMngViewModel : LibMngDetailViewModelMfc<CompraDetalleArticuloInventarioViewModel, CompraDetalleArticuloInventario> {
        #region Propiedades

        public override string ModuleName {
            get { return "Compra Detalle Articulo Inventario"; }
        }

        public CompraViewModel Master {
            get;
            set;
        }

        public int DecimalDigits {
            get {
                return LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("Parametros", "CantidadDeDecimales");
            }
        }

       

        public ObservableCollection<LibGridColumModel> VisibleColumnsParaDistribucionAutomatica {
            get;
            private set;
        }
        #endregion //Propiedades
        #region Constructores

        public CompraDetalleArticuloInventarioMngViewModel(CompraViewModel initMaster)
            : base(LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            Master = initMaster;
            Title = "Buscar " + ModuleName;
        }

        public CompraDetalleArticuloInventarioMngViewModel(CompraViewModel initMaster, ObservableCollection<CompraDetalleArticuloInventario> initDetail, eAccionSR initAction)
            : base(LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            Master = initMaster;
            foreach (var vItem in initDetail) {
                var vViewModel = new CompraDetalleArticuloInventarioViewModel(Master, vItem, initAction);
                vViewModel.InitializeViewModel(initAction);
                if (initAction == eAccionSR.ReImprimir) {
                    vViewModel.CargarConexiones();
                }
                Add(vViewModel);
                
            }

            //if (!Master.VieneDeOrdenDeCompra) {
            //    RemoveColumnByDisplayMemberPath("CantidadRecibida");
            //}
            ////if (EsParaDistribucionDeGasto) {
            ////    RemoveColumnByDisplayMemberPath("CostoUnitario");
            ////    RemoveColumnByDisplayMemberPath("Cantidad");
            ////} else {
            ////    RemoveColumnByDisplayMemberPath("PorcentajeDeDistribucion");
            ////    RemoveColumnByDisplayMemberPath("MontoDistribucion");
            ////}

           // VisibleColumns = LibGridColumModel.GetGridColumsFromType(typeof(CompraDetalleArticuloInventarioViewModel));

            VisibleColumnsParaDistribucionAutomatica = new ObservableCollection<LibGridColumModel>();
            VisibleColumnsParaDistribucionAutomatica.Add(new LibGridColumModel() { Header = "Codigo", IsReadOnly = true, IsForList = true, Type = eGridColumType.Generic, Alignment = eTextAlignment.Left, ModelType = typeof(CompraDetalleArticuloInventarioViewModel), DbMemberPath = "CodigoArticulo", DisplayMemberPath = "CodigoArticulo", Width = 120 });
            VisibleColumnsParaDistribucionAutomatica.Add(new LibGridColumModel() { Header = "Valor Fob", IsReadOnly = true, IsForList = true, Type = eGridColumType.Numeric, Alignment = eTextAlignment.Right, ModelType = typeof(CompraDetalleArticuloInventarioViewModel), DbMemberPath = "SubTotal", DisplayMemberPath = "SubTotal", Width = 120, ConditionalPropertyDecimalDigits = "DecimalDigits" });
            VisibleColumnsParaDistribucionAutomatica.Add(new LibGridColumModel() { Header = "% Distribución Fob", IsReadOnly = true, IsForList = true, Type = eGridColumType.Numeric, Alignment = eTextAlignment.Right, ModelType = typeof(CompraDetalleArticuloInventarioViewModel), DbMemberPath = "PorcentajDistribucionFOB", DisplayMemberPath = "PorcentajDistribucionFOB", Width = 120, ConditionalPropertyDecimalDigits = "DecimalDigits" });
            VisibleColumnsParaDistribucionAutomatica.Add(new LibGridColumModel() { Header = "Flete", IsReadOnly = true, IsForList = true, Type = eGridColumType.Numeric, Alignment = eTextAlignment.Right, ModelType = typeof(CompraDetalleArticuloInventarioViewModel), DbMemberPath = "TotalFlete", DisplayMemberPath = "TotalFlete", Width = 120, ConditionalPropertyDecimalDigits = "DecimalDigits" });
            if (Master.TipoDeCompra == eTipoCompra.Importacion) {
                VisibleColumnsParaDistribucionAutomatica.Add(new LibGridColumModel() { Header = "% Seguro Ley", IsReadOnly = true, IsForList = true, Type = eGridColumType.Numeric, Alignment = eTextAlignment.Right, ModelType = typeof(CompraDetalleArticuloInventarioViewModel), DbMemberPath = "ValorSeguro", DisplayMemberPath = "ValorSeguro", Width = 120, ConditionalPropertyDecimalDigits = "DecimalDigits" });
                VisibleColumnsParaDistribucionAutomatica.Add(new LibGridColumModel() { Header = "Valor Seguro", IsReadOnly = true, IsForList = true, Type = eGridColumType.Numeric, Alignment = eTextAlignment.Right, ModelType = typeof(CompraDetalleArticuloInventarioViewModel), DbMemberPath = "SeguroPagado", DisplayMemberPath = "SeguroPagado", Width = 120, ConditionalPropertyDecimalDigits = "DecimalDigits" });
                VisibleColumnsParaDistribucionAutomatica.Add(new LibGridColumModel() { Header = "Valor CIF", IsReadOnly = true, IsForList = true, Type = eGridColumType.Numeric, Alignment = eTextAlignment.Right, ModelType = typeof(CompraDetalleArticuloInventarioViewModel), DbMemberPath = "ValorCIF", DisplayMemberPath = "ValorCIF", Width = 120, ConditionalPropertyDecimalDigits = "DecimalDigits" });
                VisibleColumnsParaDistribucionAutomatica.Add(new LibGridColumModel() { Header = "% Arancel", IsReadOnly = true, IsForList = true, Type = eGridColumType.Numeric, Alignment = eTextAlignment.Right, ModelType = typeof(CompraDetalleArticuloInventarioViewModel), DbMemberPath = "PorcentajeArancel", DisplayMemberPath = "PorcentajeArancel", Width = 120, ConditionalPropertyDecimalDigits = "DecimalDigits" });
                VisibleColumnsParaDistribucionAutomatica.Add(new LibGridColumModel() { Header = "Impuesto", IsReadOnly = true, IsForList = true, Type = eGridColumType.Numeric, Alignment = eTextAlignment.Right, ModelType = typeof(CompraDetalleArticuloInventarioViewModel), DbMemberPath = "Impuesto", DisplayMemberPath = "Impuesto", Width = 120, ConditionalPropertyDecimalDigits = "DecimalDigits" });
                VisibleColumnsParaDistribucionAutomatica.Add(new LibGridColumModel() { Header = "Tasa de gasto Aduanero", IsReadOnly = true, IsForList = true, Type = eGridColumType.Numeric, Alignment = eTextAlignment.Right, ModelType = typeof(CompraDetalleArticuloInventarioViewModel), DbMemberPath = "TasaGastoAduanero", DisplayMemberPath = "TasaGastoAduanero", Width = 120, ConditionalPropertyDecimalDigits = "DecimalDigits" });
            }
            VisibleColumnsParaDistribucionAutomatica.Add(new LibGridColumModel() { Header = "Total otros Gastos", IsReadOnly = true, IsForList = true, Type = eGridColumType.Numeric, Alignment = eTextAlignment.Right, ModelType = typeof(CompraDetalleArticuloInventarioViewModel), DbMemberPath = "TotalOtrosGastos", DisplayMemberPath = "TotalOtrosGastos", Width = 120, ConditionalPropertyDecimalDigits = "DecimalDigits" });
            VisibleColumnsParaDistribucionAutomatica.Add(new LibGridColumModel() { Header = "Total", IsReadOnly = true, IsForList = true, Type = eGridColumType.Numeric, Alignment = eTextAlignment.Right, ModelType = typeof(CompraDetalleArticuloInventarioViewModel), DbMemberPath = "Total", DisplayMemberPath = "Total", Width = 120, ConditionalPropertyDecimalDigits = "DecimalDigits" });
            VisibleColumnsParaDistribucionAutomatica.Add(new LibGridColumModel() { Header = "Cantidad", IsReadOnly = true, IsForList = true, Type = eGridColumType.Numeric, Alignment = eTextAlignment.Right, ModelType = typeof(CompraDetalleArticuloInventarioViewModel), DbMemberPath = "Cantidad", DisplayMemberPath = "Cantidad", Width = 120, ConditionalPropertyDecimalDigits = "DecimalDigits" });
            VisibleColumnsParaDistribucionAutomatica.Add(new LibGridColumModel() { Header = "Costo Unitario", IsReadOnly = true, IsForList = true, Type = eGridColumType.Numeric, Alignment = eTextAlignment.Right, ModelType = typeof(CompraDetalleArticuloInventarioViewModel), DbMemberPath = "CostoUnitario", DisplayMemberPath = "CostoUnitario", Width = 120, ConditionalPropertyDecimalDigits = "DecimalDigits" });
            //if (!Master.VieneDeOrdenDeCompra) {
            //    VisibleColumns.RemoveAt(4);
            //}
            //if (Master.TipoDeDistribucion == eTipoDeDistribucion.ManualPorMonto) {
            //    VisibleColumns.RemoveAt(4);
            //} else if (Master.TipoDeDistribucion == eTipoDeDistribucion.ManualPorPorcentaje ) {
            //    VisibleColumns.RemoveAt(5);
            //}
            //if (EsParaDistribucionDeGasto) {
            //    VisibleColumns.RemoveAt(3);
            //    VisibleColumns.RemoveAt(2);
            //} else {                
            //    VisibleColumns.RemoveAt(5);
            //    VisibleColumns.RemoveAt(4);
            //}

        }
        #endregion //Constructores
        #region Metodos Generados

        protected override CompraDetalleArticuloInventarioViewModel CreateNewElement(CompraDetalleArticuloInventario valModel) {
            var vNewModel = valModel;
            if (vNewModel == null) {
                vNewModel = new CompraDetalleArticuloInventario();
            }
            return new CompraDetalleArticuloInventarioViewModel(Master, vNewModel, eAccionSR.Insertar);
        }

        protected override void RaiseOnCreatedEvent(CompraDetalleArticuloInventarioViewModel valViewModel) {
            valViewModel.Master = Master;
            base.RaiseOnCreatedEvent(valViewModel);
        }
        #endregion //Metodos Generados

        private int _SelectedIndex;
        public int SelectedIndex {
            get {
                return _SelectedIndex;
            }
            set {
                _SelectedIndex = value;
                RaisePropertyChanged("SelectedIndex");
            }
        }

        protected override void ExecuteCreateCommand(string valUseExternalEditorStr) {
            base.ExecuteCreateCommand(valUseExternalEditorStr);
            Master.RaiseMoveFocusArticuloInventario();
        }

        //protected override void ExecuteCreateCommand(string valUseExternalEditorStr) {            
        //    CompraDetalleArticuloInventarioViewModel vViewModel = new CompraDetalleArticuloInventarioViewModel();            
        //    Add(vViewModel);
        //    SelectedItem = vViewModel;
        //    
        //    if (Items.Count > 1) {
        //        SelectedIndex = Items.Count - 1;
        //    }
        //    RaiseSelectedItemChanged();

        //    //base.ExecuteCreateCommand(valUseExternalEditorStr);
        //}

        internal void ActualizaDetalleDesdeOrdenDeCompra(CompraViewModel valMaster, ObservableCollection<CompraDetalleArticuloInventario> valDetail, eAccionSR valAction) {
            foreach (var vItem in valDetail) {
                var vViewModel = new CompraDetalleArticuloInventarioViewModel(valMaster, vItem, valAction);
                vViewModel.InitializeViewModel(valAction);
                vViewModel.CargarConexiones();
                Add(vViewModel);
            }
        }

        protected override bool CanExecuteCreateCommand(string valUseExternalEditorStr) {
            bool vResult = base.CanExecuteCreateCommand(valUseExternalEditorStr);
            vResult = vResult && !Master.VieneDeOrdenDeCompra;
            return vResult;
        }
        protected override bool CanExecuteDeleteCommand(string valUseExternalEditorStr) {            
            bool vResult = base.CanExecuteDeleteCommand(valUseExternalEditorStr);
            vResult = vResult && !Master.VieneDeOrdenDeCompra;
            return vResult;
        }

        internal void QuitarArticuloConSerialRepetido() {
            ExecuteDeleteCommand("");            
        }
    } //End of class CompraDetalleArticuloInventarioMngViewModel


} //End of namespace Galac.Adm.Uil.GestionCompras

