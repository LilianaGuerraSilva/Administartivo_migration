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
using LibGalac.Aos.UI.Wpf;

namespace Galac.Adm.Uil.Venta.ViewModel {

    public class CajaMngViewModel : LibMngViewModelMfc<CajaViewModel, Caja> {
        #region Propiedades          
        public override string ModuleName {
            get { return "Caja Registradora"; }
        }

        public RelayCommand CrearCajaGenericaCommand {
            get;
            private set;
        }

        public bool CanExecuteCrearCajaGenerica() {            
            return LibConvert.SNToBool(AppMemoryInfo.GlobalValuesGetString("Parametros","PuedeInsertarCajaGenerica"));
            //return vResult &= LibSecurityManager.CurrentUserHasAccessTo("Caja Registradora","Insertar Caja Generica");
        }

        #endregion //Propiedades
        #region Constructores

        public CajaMngViewModel()
            : base(LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            Title = "Buscar " + ModuleName;
            OrderByMember = "ConsecutivoCompania, Consecutivo";
            OrderByDirection = "ASC";      
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override CajaViewModel CreateNewElement(Caja valModel, eAccionSR valAction) {
            var vNewModel = valModel;
            if (vNewModel == null) {
                vNewModel = new Caja();
            }
            return new CajaViewModel(vNewModel, valAction);
        }

        protected override LibSearchCriteria GetMFCCriteria() {
            return LibSearchCriteria.CreateCriteria("Gv_Caja_B1.ConsecutivoCompania", Mfc.GetInt("Compania"));
        }

        protected override ILibBusinessComponentWithSearch<IList<Caja>, IList<Caja>> GetBusinessComponent() {
            return new clsCajaNav();
        }

        protected override ILibReportInfo GetDataRetrievesInstance() {
            return null; //new clsCajaRpt();
        }

        protected override ILibRpt GetReportForList(string valModuleName, ILibReportInfo valReportInfo, LibCollFieldFormatForGrid valFieldsFormat, LibGpParams valParams) {
            return new LibGenericList(valModuleName, valReportInfo, valFieldsFormat, valParams);
        }

        protected override void InitializeCommands() {            
            base.InitializeCommands();
            CrearCajaGenericaCommand = new RelayCommand(ExecuteCrearCajaGenerica,CanExecuteCrearCajaGenerica);
        }        

        protected override void InitializeRibbon() {
            base.InitializeRibbon();
            if(RibbonData.TabDataCollection != null && RibbonData.TabDataCollection.Count > 0) {
                RibbonData.TabDataCollection[0].AddTabGroupData(new LibRibbonGroupData("Especial"));
                RibbonData.TabDataCollection[0].GroupDataCollection[2].AddRibbonControlData(CreateCajaGenericaRibbonButtonData());
                var GroupTemp = RibbonData.TabDataCollection[0].GroupDataCollection[1];
                RibbonData.TabDataCollection[0].GroupDataCollection[1] = RibbonData.TabDataCollection[0].GroupDataCollection[2];
                RibbonData.TabDataCollection[0].GroupDataCollection[2] = GroupTemp;
            }            
        }

        private void ExecuteCrearCajaGenerica() {
            try {
                ICajaPdn insCaja = new clsCajaNav();                
                if(insCaja.InsertarCajaPorDefecto(LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"))) {
                    LibMessages.MessageBox.Information(this,"La caja por defecto fue creada con exíto",ModuleName);
                }
            } catch(Exception vEx) {
                LibExceptionDisplay.Show(vEx);
            }
        }

        private LibRibbonButtonData CreateCajaGenericaRibbonButtonData() {
            return new LibRibbonButtonData() {
                Label = "Crear Caja Genérica",
                Command = CrearCajaGenericaCommand,
                LargeImage = new Uri("/LibGalac.Aos.UI.WpfRD;component/Images/exec.png",UriKind.Relative),
                ToolTipDescription = "Crear Caja Genérica",
                ToolTipTitle = "Crear Caja Genérica"
            };
        }     
        #endregion //Metodos Generados        
    } //End of class CajaMngViewModel

} //End of namespace Galac.Adm.Uil.Venta

