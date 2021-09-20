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
using Galac.Adm.Brl.Venta;
using Galac.Adm.Ccl.Venta;
using System.Xml.Linq;
using LibGalac.Aos.Cnf;
using Galac.Adm.Ccl.DispositivosExternos;
using LibGalac.Aos.UI.Wpf;
using LibGalac.Aos.DefGen;

namespace Galac.Adm.Uil.Venta.ViewModel {

    public class CajaAperturaMngViewModel : LibMngViewModelMfc<CajaAperturaViewModel, CajaApertura> {

        #region Variables

        ICajaAperturaPdn insCajaApertura;

        #endregion

        #region Propiedades

        public override string ModuleName {
            get {
                return "Caja Registradora";
            }
        }

        public RelayCommand AbrirCajaCommand {
            get;
            private set;
        }

        public RelayCommand CerrarCajaCommand {
            get;
            private set;
        }

        public RelayCommand AsignarCajaCommand {
            get;
            private set;
        }
        public RelayCommand AbrirGavetaCommand {
            get;
            private set;
        }

        #endregion //Propiedades

        #region Constructores e Inicializadores

        public CajaAperturaMngViewModel()
            : base(LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            Title = "Buscar " + ModuleName;
            OrderByMember = "ConsecutivoCompania, ConsecutivoCaja,Consecutivo";
            OrderByDirection = "ASC";
            SearchCriteria = LibSearchCriteria.CreateCriteria("Adm.Gv_CajaApertura_B1.ConsecutivoCompania", LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"));
            DefaultSearchCriteria = SearchCriteria;
            insCajaApertura = new clsCajaAperturaNav() as ICajaAperturaPdn;
        }

        public override void InitializeViewModel() {
            base.InitializeViewModel();
        }

        protected override void InitializeCommands() {
            base.InitializeCommands();
            AbrirCajaCommand = new RelayCommand(ExecuteAbrirCajaCommand, CanExecuteAbrirCajaCommand);
            CerrarCajaCommand = new RelayCommand(ExecuteCerrarCajaCommand, CanExecuteCerrarCajaCommand);
            AsignarCajaCommand = new RelayCommand(ExecuteAsignarCajaCommand, CanExecuteAsignarCajaCommand);
            AbrirGavetaCommand = new RelayCommand(ExecuteAbrirGavetaCommand, CanExecuteAbrirGavetaCommand);
        }

        protected override void InitializeRibbon() {
            base.InitializeRibbon();
            if(RibbonData.TabDataCollection != null && RibbonData.TabDataCollection.Count > 0) {
                var tempSwp1 = RibbonData.TabDataCollection[0].GroupDataCollection[1];
                RibbonData.TabDataCollection[0].GroupDataCollection.Remove(RibbonData.TabDataCollection[0].GroupDataCollection[1]);
                RibbonData.TabDataCollection[0].GroupDataCollection.Remove(RibbonData.TabDataCollection[0].GroupDataCollection[0]);
                RibbonData.TabDataCollection[0].AddTabGroupData(new LibRibbonGroupData("Configurar"));
                RibbonData.TabDataCollection[0].AddTabGroupData(new LibRibbonGroupData("Gaveta"));
                RibbonData.TabDataCollection[0].AddTabGroupData(tempSwp1);
                RibbonData.TabDataCollection[0].GroupDataCollection[0].AddRibbonControlData(CreateAbrirCajaRibbonButtonData());
                RibbonData.TabDataCollection[0].GroupDataCollection[0].AddRibbonControlData(CreateCerrarCajaRibbonButtonData());
                RibbonData.TabDataCollection[0].GroupDataCollection[0].AddRibbonControlData(CreateAsignarCajaRibbonButtonData());
                RibbonData.TabDataCollection[0].GroupDataCollection[1].AddRibbonControlData(CreateAbrirGavetaRibbonButtonData());
            }
        }

        #endregion //Constructores e Inicializadores

        #region Comandos

        private LibRibbonButtonData CreateAbrirCajaRibbonButtonData() {
            return new LibRibbonButtonData() {
                Label = "Abrir Caja",
                Command = AbrirCajaCommand,
                LargeImage = new Uri("/Galac.Adm.Uil.Venta;component/Images/Open.png", UriKind.Relative),
                ToolTipDescription = "Abrir Caja Registradora",
                ToolTipTitle = "Abrir Caja",
                IsVisible = !LibDefGen.DataBaseInfo.IsReadOnlyRMDB
            };
        }

        private LibRibbonButtonData CreateCerrarCajaRibbonButtonData() {
            return new LibRibbonButtonData() {
                Label = "Cerrar Caja",
                Command = CerrarCajaCommand,
                LargeImage = new Uri("/Galac.Adm.Uil.Venta;component/Images/Close.png", UriKind.Relative),
                ToolTipDescription = "Cerrar Caja Registradora",
                ToolTipTitle = "Cerrar Caja",
                IsVisible = !LibDefGen.DataBaseInfo.IsReadOnlyRMDB
            };
        }

        private LibRibbonButtonData CreateAsignarCajaRibbonButtonData() {
            return new LibRibbonButtonData() {
                Label = "Asignar Caja",
                Command = AsignarCajaCommand,
                LargeImage = new Uri("/LibGalac.Aos.UI.WpfRD;component/Images/exec.png", UriKind.Relative),
                ToolTipDescription = "Asignar una Caja",
                ToolTipTitle = "Asignar Caja",
                IsVisible = !LibDefGen.DataBaseInfo.IsReadOnlyRMDB
            };
        }

        private LibRibbonButtonData CreateAbrirGavetaRibbonButtonData() {
            return new LibRibbonButtonData() {
                Label = "Abrir Gaveta",                
                Command = AbrirGavetaCommand,
                LargeImage = new Uri("/LibGalac.Aos.UI.WpfRD;component/Images/exec.png", UriKind.Relative),
                ToolTipDescription = "Abrir Gaveta",
                ToolTipTitle = "Abrir Gaveta",
                KeyTip ="A",
                IsVisible = !LibDefGen.DataBaseInfo.IsReadOnlyRMDB
            };
        }

        private void ExecuteAbrirCajaCommand() {
            try {
                CajaAperturaViewModel vCajaAperturaViewModel = CreateNewElement(null, eAccionSR.Insertar);
                vCajaAperturaViewModel.InitializeViewModel(eAccionSR.Insertar);
                LibMessages.EditViewModel.ShowEditor(vCajaAperturaViewModel);
            } catch(GalacException vEx) {
                LibMessages.MessageBox.Information(this, vEx.Message, "");
            }
        }

        private void ExecuteCerrarCajaCommand() {
            try {
                if(CurrentItem != null) {
                    CajaAperturaViewModel vCajaAperturaViewModel = CreateNewElement(CurrentItem.GetModel(), eAccionSR.Modificar);
                    vCajaAperturaViewModel.InitializeViewModel(eAccionSR.Modificar);
                    LibMessages.EditViewModel.ShowEditor(vCajaAperturaViewModel);
                } else {
                    LibMessages.MessageBox.Information(this, "No existen cajas abiertas", "");
                }
            } catch(GalacException vEx) {
                LibMessages.MessageBox.Information(this, vEx.Message, "");
            }
        }

        private void ExecuteAsignarCajaCommand() {
            try {
                if(CurrentItem != null) {
                    CajaAperturaViewModel vCajaAperturaViewModel = CreateNewElement(CurrentItem.GetModel(), eAccionSR.Escoger);
                    vCajaAperturaViewModel.InitializeViewModel(eAccionSR.Escoger);
                    LibMessages.EditViewModel.ShowEditor(vCajaAperturaViewModel);
                } else {
                    LibMessages.MessageBox.Information(this, "No existen cajas abiertas", "");
                }
            } catch(GalacException vEx) {
                LibMessages.MessageBox.Information(this, vEx.Message, "");
            }
        }

        private void ExecuteAbrirGavetaCommand() {
            try {
                XElement vCaja = new clsCajaNav().BuscarcajaAsignada();
                if(LibConvert.SNToBool(LibXml.GetPropertyString(vCaja, "UsaGaveta"))) {
                    IGavetaPdn vGaveta = new Brl.DispositivosExternos.CajaGaveta.clsGavetaNav();
                    vGaveta.AbrirGaveta((ePuerto)LibConvert.ToInt(LibXml.GetPropertyString(vCaja, "Puerto")), LibXml.GetPropertyString(vCaja, "Comando"));
                } else {
                    LibMessages.MessageBox.Information(this, "La Caja asignada no usa Gaveta.", "");
                }
            } catch(GalacException vEx) {
                LibExceptionDisplay.Show(vEx);
            }
        }

        private bool CanExecuteAsignarCajaCommand() {
            return LibSecurityManager.CurrentUserHasAccessTo("Caja Registradora", "Asignar Caja Registradora")
                && LibSecurityManager.CurrentUserHasAccessTo("Caja Registradora", "Modificar")
                && LibSecurityManager.CurrentUserHasAccessTo("Caja Registradora", "Insertar");
        }

        private bool CanExecuteCerrarCajaCommand() {
            return base.CanUpdate && LibSecurityManager.CurrentUserHasAccessTo("Caja Registradora", "Modificar");
        }

        private bool CanExecuteAbrirCajaCommand() {
            return base.CanCreate && LibSecurityManager.CurrentUserHasAccessTo("Caja Registradora", "Insertar");
        }

        private bool CanExecuteAbrirGavetaCommand() {
            return LibSecurityManager.CurrentUserHasAccessTo("Caja Registradora", "Abrir Gaveta");
        }

        #endregion

        #region Metodos

        protected override CajaAperturaViewModel CreateNewElement(CajaApertura valModel, eAccionSR valAction) {
            var vNewModel = valModel;
            if(vNewModel == null) {
                vNewModel = new CajaApertura();
            }
            return new CajaAperturaViewModel(vNewModel, valAction);
        }

        protected override void SearchItems() {
            base.SearchItems();
        }

        protected override LibSearchCriteria GetMFCCriteria() {
            return SearchCriteria;
        }

        protected override ILibBusinessComponentWithSearch<IList<CajaApertura>, IList<CajaApertura>> GetBusinessComponent() {
            return new clsCajaAperturaNav();
        }

        protected override ILibReportInfo GetDataRetrievesInstance() {
            return null; //new clsCajaAperturaRpt();
        }

        protected override ILibRpt GetReportForList(string valModuleName, ILibReportInfo valReportInfo, LibCollFieldFormatForGrid valFieldsFormat, LibGpParams valParams) {
            return new LibGenericList(valModuleName, valReportInfo, valFieldsFormat, valParams);
        }

        #endregion //Metodos 

    } //End of class CajaAperturaMngViewModel

} //End of namespace Galac.Adm.Uil.Venta

