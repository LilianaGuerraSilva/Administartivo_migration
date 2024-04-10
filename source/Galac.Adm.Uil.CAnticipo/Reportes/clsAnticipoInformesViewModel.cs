using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using LibGalac.Aos.ARRpt;
using LibGalac.Aos.ARRpt.Reports;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Report;
using LibGalac.Aos.UI.Mvvm;

namespace Galac.Adm.Uil.CAnticipo.Reportes {

    public class clsAnticipoInformesViewModel : LibReportsViewModel {
        #region Constructores

        public clsAnticipoInformesViewModel()
            : this(null, null) {
        }

        public clsAnticipoInformesViewModel(LibXmlMemInfo initAppMemInfo, LibXmlMFC initMfc) {
            AppMemoryInfo = initAppMemInfo;
            Mfc = initMfc;
            AvailableReports.Add(new clsAnticipoPorProveedorOClienteViewModel());
            Title = "Informes de Anticipo";
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override ILibRpt ConfigReport() {
            ILibRpt vResult = null;
            if (SelectedReport is clsAnticipoPorProveedorOClienteViewModel) {
                vResult = ConfigReportAnticipoPorProveedorOCliente(SelectedReport as clsAnticipoPorProveedorOClienteViewModel);
            }
            return vResult;
        }

        private ILibRpt ConfigReportAnticipoPorProveedorOCliente(clsAnticipoPorProveedorOClienteViewModel valViewModel) {
            ILibRpt vResult = null;
            if (valViewModel != null) {
                vResult = new Galac.Adm.Rpt.CAnticipo.clsAnticipoPorProveedorOCliente(PrintingDevice, ExportFileFormat, AppMemoryInfo, Mfc,valViewModel.EstatusAnticipo,valViewModel.CantidadAImprimir,valViewModel.CodigoClientProveedor,valViewModel.OrdenamientoClienteStatus,valViewModel.MonedaDelGrupo,valViewModel.EsProveedorOCliente) {
                    Worker = Manager
                };
            }
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class clsAnticipoInformesViewModel

} //End of namespace Galac.Adm.Uil.CAnticipo

