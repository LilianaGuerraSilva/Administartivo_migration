using System;
using System.Collections.Generic;
using LibGalac.Aos.UI.Mvvm;
using LibGalac.Aos.UI.Mvvm.Command;
using LibGalac.Aos.UI.Mvvm.Messaging;
using LibGalac.Aos.UI.Mvvm.Ribbon;
using LibGalac.Aos.Brl.Contracts;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Report;
using LibGalac.Aos.ARRpt.Reports;
using Galac.Adm.Ccl.DispositivosExternos;
using Galac.Adm.Brl.DispositivosExternos.BalanzaElectronica;
using LibGalac.Aos.Catching;

namespace Galac.Adm.Uil.DispositivosExternos.ViewModel {

    [LibMefInstallValuesMetadata(typeof(BalanzaMngViewModel))]
    public class BalanzaMngViewModel : LibMngViewModelMfc<BalanzaViewModel, Balanza> {
        #region Propiedades

        bool _ExistenPuertosSeriales;


        public override string ModuleName {
            get { return "Balanza"; }
        }

        public RelayCommand EscogerBalanzaPOSCommand {
            get;
            private set;
        }

        #endregion //Propiedades
        #region Constructores

        public BalanzaMngViewModel()
            : base(LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            Title = "Gestionar " + ModuleName;
            OrderByMember = "ConsecutivoCompania, Consecutivo";
            _ExistenPuertosSeriales = ExistePuertosSerial();
            if(!_ExistenPuertosSeriales) {
                throw new GalacException("No existen puertos seriales en este computador", eExceptionManagementType.Alert);
            }
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override BalanzaViewModel CreateNewElement(Balanza valModel, eAccionSR valAction) {
            var vNewModel = valModel;
            if(vNewModel == null) {
                vNewModel = new Balanza();
            }
            return new BalanzaViewModel(vNewModel, valAction);
        }

        protected override LibSearchCriteria GetMFCCriteria() {
            return LibSearchCriteria.CreateCriteria("Gv_Balanza_B1.ConsecutivoCompania", Mfc.GetInt("Compania"));
        }

        protected override ILibBusinessComponentWithSearch<IList<Balanza>, IList<Balanza>> GetBusinessComponent() {
            return new clsBalanzaNav();
        }

        protected override ILibReportInfo GetDataRetrievesInstance() {
            return null;//new clsBalanzaRpt();
        }

        protected override ILibRpt GetReportForList(string valModuleName, ILibReportInfo valReportInfo, LibCollFieldFormatForGrid valFieldsFormat, LibGpParams valParams) {
            return new LibGenericList(valModuleName, valReportInfo, valFieldsFormat, valParams);
        }


        protected override void InitializeCommands() {
            base.InitializeCommands();
            EscogerBalanzaPOSCommand = new RelayCommand(ExecuteEscogerBalanzaPOSCommand, CanExecuteEscogerBalanzaPOSCommand);
        }

        protected override void InitializeRibbon() {
            base.InitializeRibbon();
            if(RibbonData.TabDataCollection != null && RibbonData.TabDataCollection.Count > 0) {
                RibbonData.TabDataCollection[0].GroupDataCollection.Add(CreateAsignarBalanzaPOSRibbonGroup());
                RibbonData.RemoveRibbonControl("Consultas", "Imprimir Lista");
            }
        }

        private LibRibbonGroupData CreateAsignarBalanzaPOSRibbonGroup() {
            LibRibbonGroupData vResult = new LibRibbonGroupData("Seleccionar Balanza");
            vResult.ControlDataCollection.Add(new LibRibbonButtonData() {
                Label = "Seleccionar Balanza en POS",
                Command = EscogerBalanzaPOSCommand,
                LargeImage = new Uri("/LibGalac.Aos.UI.WpfRD;component/Images/settings.png", UriKind.Relative),
                ToolTipDescription = "Asignar Balanza a POS",
                ToolTipTitle = "Seleccionar Balanza en POS"
            });
            return vResult;
        }

        protected override bool CanExecuteCreateCommand() {
            return CanCreate && _ExistenPuertosSeriales;
        }

        protected override bool CanExecuteUpdateCommand() {
            return CurrentItem != null && CanUpdate && _ExistenPuertosSeriales;
        }

        protected override bool CanExecuteDeleteCommand() {
            return CurrentItem != null && CanDelete && _ExistenPuertosSeriales;
        }

        protected override bool CanExecuteReadCommand() {
            return CurrentItem != null && CanRead && _ExistenPuertosSeriales;
        }

        private bool CanExecuteEscogerBalanzaPOSCommand() {
            return CurrentItem != null && _ExistenPuertosSeriales;
        }

        private void ExecuteEscogerBalanzaPOSCommand() {
            try {
                BalanzaViewModel BalanzaViewModel = CreateNewElement(CurrentItem.GetModel(), eAccionSR.Cerrar);
                BalanzaViewModel.InitializeViewModel(eAccionSR.Escoger);
                LibMessages.EditViewModel.ShowEditor(BalanzaViewModel);
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                if(LibString.S1IsInS2("Referencia a objeto no establecida como instancia de un objeto", vEx.Message)) {
                    LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.MessageBox.Alert(null, "No existe una balanza registrada", "");
                } else {
                    LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
                }
            }
        }

        private bool ExistePuertosSerial() {
            Brl.DispositivosExternos.clsConexionPuertoSerial PuertoSerial = new Brl.DispositivosExternos.clsConexionPuertoSerial();
            string[] ListaPuertos = PuertoSerial.ListarPuertos();
            return ListaPuertos.Length > 0;
        }
        #endregion Metodos Generados
    } //End of class BalanzaMngViewModel

} //End of namespace Galac.Adm.Uil.DispositivosExternos

