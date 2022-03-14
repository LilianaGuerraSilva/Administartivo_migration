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
using Galac.Adm.Brl.Banco;
using Galac.Adm.Brl.Banco.Reportes;
using Galac.Adm.Ccl.Banco;

namespace Galac.Adm.Uil.Banco.ViewModel {

    public class ConceptoBancarioMngViewModel : LibMngViewModel<ConceptoBancarioViewModel, ConceptoBancario> {
        #region Propiedades
        public override string ModuleName {
            get { return "Concepto Bancario"; }
        }
        #endregion //Propiedades

        #region Constructores
        public ConceptoBancarioMngViewModel() {
            Title = "Buscar " + ModuleName;
            OrderByMember = "Codigo";
        }
        #endregion //Constructores

        #region Metodos Generados
        protected override ConceptoBancarioViewModel CreateNewElement(ConceptoBancario valModel, eAccionSR valAction) {
            var vNewModel = valModel;
            if (vNewModel == null) {
                vNewModel = new ConceptoBancario();
            }
            return new ConceptoBancarioViewModel(vNewModel, valAction);
        }

        protected override ILibBusinessComponentWithSearch<IList<ConceptoBancario>, IList<ConceptoBancario>> GetBusinessComponent() {
            return new clsConceptoBancarioNav();
        }

        protected override ILibReportInfo GetDataRetrievesInstance() {
            return new clsConceptoBancarioRpt();
        }

        protected override ILibRpt GetReportForList(string valModuleName, ILibReportInfo valReportInfo, LibCollFieldFormatForGrid valFieldsFormat, LibGpParams valParams) {
            return new LibGenericList(valModuleName, valReportInfo, valFieldsFormat, valParams);
        }
        #endregion //Metodos Generados

    } //End of class ConceptoBancarioMngViewModel

} //End of namespace Galac.Adm.Uil.Banco

