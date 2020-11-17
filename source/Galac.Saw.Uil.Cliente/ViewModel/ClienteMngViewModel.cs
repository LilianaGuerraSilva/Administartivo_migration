using System;
using System.Collections.Generic;
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
using Galac.Saw.Brl.Cliente;
using Galac.Saw.Brl.Cliente.Reportes;
using Galac.Saw.Ccl.Cliente;
using Entity = Galac.Saw.Ccl.Cliente;

namespace Galac.Saw.Uil.Cliente.ViewModel {

    public class ClienteMngViewModel : LibMngViewModelMfc<ClienteViewModel, Entity.Cliente> {
        #region Propiedades

        public override string ModuleName {
            get { return "Cliente"; }
        }
        #region Codigo Ejemplo
        /* Codigo de Ejemplo

        public LibXmlMemInfo AppMemoryInfo { get; set; }
        */
        #endregion //Codigo Ejemplo
        #endregion //Propiedades
        #region Constructores

        public ClienteMngViewModel()
            : base(LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            Title = "Buscar " + ModuleName;
            OrderByMember = "ConsecutivoCompania, Codigo";
        #region Codigo Ejemplo
        /* Codigo de Ejemplo
            OrderByDirection = "DESC";
            AppMemoryInfo = LibGlobalValues.Instance.GetAppMemInfo();
        */
        #endregion //Codigo Ejemplo
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override ClienteViewModel CreateNewElement(Entity.Cliente valModel, eAccionSR valAction) {
            var vNewModel = valModel;
            if (vNewModel == null) {
                vNewModel = new Entity.Cliente();
            }
            return new ClienteViewModel(vNewModel, valAction);
        }

        protected override LibSearchCriteria GetMFCCriteria() {
            return LibSearchCriteria.CreateCriteria("Gv_Cliente_B1.ConsecutivoCompania", Mfc.GetInt("Compania"));
        }

        protected override ILibBusinessComponentWithSearch<IList<Entity.Cliente>, IList<Entity.Cliente>> GetBusinessComponent() {
            return new clsClienteNav();
        }

        protected override ILibReportInfo GetDataRetrievesInstance() {
            return new clsClienteRpt();
        }

        protected override ILibRpt GetReportForList(string valModuleName, ILibReportInfo valReportInfo, LibCollFieldFormatForGrid valFieldsFormat, LibGpParams valParams) {
            return new LibGenericList(valModuleName, valReportInfo, valFieldsFormat, valParams);
        }

        protected override void InitializeCommands() {
            base.InitializeCommands();
        #region Codigo Ejemplo
        /* Codigo de Ejemplo
            SUPROCESOPARTICULARCommand = new RelayCommand(ExecuteSUPROCESOPARTICULARCommand, CanExecuteSUPROCESOPARTICULARCommand);
        */
        #endregion //Codigo Ejemplo
        }

        protected override void InitializeRibbon() {
            base.InitializeRibbon();
            if (RibbonData.TabDataCollection != null && RibbonData.TabDataCollection.Count > 0) {
            }
        }
        #endregion //Metodos Generados
       


    } //End of class ClienteMngViewModel

} //End of namespace Galac.Saw.Uil.Cliente

