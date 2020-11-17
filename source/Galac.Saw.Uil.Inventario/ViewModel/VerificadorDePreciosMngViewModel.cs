using System.Collections.Generic;
using Galac.Saw.Ccl.Inventario;
using Galac.Saw.Uil.Inventario.Views;
using LibGalac.Aos.ARRpt.Reports;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Report;
using LibGalac.Aos.UI.Mvvm;

namespace Galac.Saw.Uil.Inventario.ViewModel {
    public class VerificadorDePreciosMngViewModel : LibMngViewModelMfc<VerificadorDePreciosViewModel, ArticuloInventario>, ILibClosableViewModel {

        #region Propiedades
        public override string ModuleName {
            get { return "Verificador de Precios"; }
        }
        public bool IsClosing { get; set; }
        public bool CancelClosing { get; set; }
        public bool RequestLoginAtClosing { get; set; }
        #endregion //Propiedades

        #region Constructores
        public VerificadorDePreciosMngViewModel()
            : base(LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            Title = "Buscar " + ModuleName;
            OrderByMember = "ConsecutivoCompania, CodigoVerificadorDePrecios";
        }
        #endregion //Constructores

        #region Metodos Generados
        protected override VerificadorDePreciosViewModel CreateNewElement(ArticuloInventario valModel, eAccionSR valAction) {
            var vNewModel = valModel;
            if (vNewModel == null) {
                vNewModel = new ArticuloInventario();
            }
            return new VerificadorDePreciosViewModel(vNewModel, valAction);
        }

        protected override LibSearchCriteria GetMFCCriteria() {
            return LibSearchCriteria.CreateCriteria("Gv_VerificadorDePrecios_B1.ConsecutivoCompania", Mfc.GetInt("Compania"));
        }

        protected override ILibBusinessComponentWithSearch<IList<ArticuloInventario>, IList<ArticuloInventario>> GetBusinessComponent() {
            return null;
        }

        protected override ILibReportInfo GetDataRetrievesInstance() {
            return null;
        }

        protected override ILibRpt GetReportForList(string valModuleName, ILibReportInfo valReportInfo, LibCollFieldFormatForGrid valFieldsFormat, LibGpParams valParams) {
            return new LibGenericList(valModuleName, valReportInfo, valFieldsFormat, valParams);
        }

        protected override void InitializeCommands() {
            base.InitializeCommands();
        }

        protected override void InitializeRibbon() {
            base.InitializeRibbon();
        }
        #endregion //Metodos Generados

        #region Codigo Ejemplo
        protected override void ExecuteCreateCommand() {
            var vViewModel = CreateNewElement(null, eAccionSR.Abrir);
            vViewModel.RequestLoginAtClosing = RequestLoginAtClosing;
            vViewModel.InitializeViewModel(eAccionSR.Abrir);
            var verificador = new GSVerificadorDePreciosView(vViewModel);
            switch (vViewModel.UsaMonedaExtranjera) {
                case true:
                    if (vViewModel.UsaMostrarPreciosEnDivisa) {
                        if (vViewModel.AsignaTasaDelDia()) {
                            verificador.Show();
                        }
                    } else {
                        verificador.Show();
                    }
                    break;
                case false:
                    verificador.Show();
                    break;
            }
        }

        protected override bool CanExecuteCreateCommand() {
            return CanCreate;
        }

        protected override bool CanExecuteUpdateCommand() {
            return CanUpdate && CurrentItem != null;
        }

        protected override bool CanExecuteDeleteCommand() {
            return CanDelete && CurrentItem != null;
        }

        protected override bool CanExecuteReadCommand() {
            return CanRead && CurrentItem != null;
        }

        public void OnClosed() {
        }

        public bool OnClosing() {
            return false;
        }
        #endregion

    } //End of class VerificadorDePreciosMngViewModel

} //End of namespace Galac.Saw.Uil.Inventario
