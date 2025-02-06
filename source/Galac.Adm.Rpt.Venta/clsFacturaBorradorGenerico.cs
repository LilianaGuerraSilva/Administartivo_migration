using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.ARRpt.Reports;
using System.Data;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Report;
using LibGalac.Aos.ARRpt;
using Galac.Adm.Ccl.Venta;
using Galac.Saw.Ccl.SttDef;

namespace Galac.Adm.Rpt.Venta {

    public class clsFacturaBorradorGenerico: LibRptBaseMfc {
        #region Propiedades
        protected DataTable Data { get; set; }
        int _ConsecutivoCompania;
        string _NombreCompania;
        string _NumeroDocumento;
        eTipoDocumentoFactura _TipoDocumento;
        eStatusFactura _StatusDocumento;
        eTalonario _Talonario;
        eFormaDeOrdenarDetalleFactura _FormaDeOrdenarDetalleFactura;
        bool _ImprimirFacturaConSubtotalesPorLineaDeProducto;
        string _CiudadCompania;
        string _NombreOperador;
        #endregion //Propiedades
        #region Constructores
        public clsFacturaBorradorGenerico(ePrintingDevice initPrintingDevice, eExportFileFormat initExportFileFormat, string valNombreCompania, int valConsecutivoCompania, string valNumeroDocumento, eTipoDocumentoFactura valTipoDocumento, eStatusFactura valStatusDocumento, eTalonario valTalonario, eFormaDeOrdenarDetalleFactura valFormaDeOrdenarDetalleFactura, bool valImprimirFacturaConSubtotalesPorLineaDeProducto, string valCiudadCompania, string valNombreOperador)
            : base(initPrintingDevice, initExportFileFormat, null, null) {
            _NombreCompania = valNombreCompania;
            _ConsecutivoCompania = valConsecutivoCompania;
            _NumeroDocumento = valNumeroDocumento;
            _TipoDocumento = valTipoDocumento;
            _StatusDocumento = valStatusDocumento;
            _Talonario = valTalonario;
            _FormaDeOrdenarDetalleFactura = valFormaDeOrdenarDetalleFactura;
            _ImprimirFacturaConSubtotalesPorLineaDeProducto = valImprimirFacturaConSubtotalesPorLineaDeProducto;
            _CiudadCompania = valCiudadCompania;
            _NombreOperador = valNombreOperador;
        }
        #endregion //Constructores
        #region Metodos Generados

        public static string ReportName {
            get { return new dsrFacturaBorradorGenerico().ReportTitle(); }
        }

        public override Dictionary<string, string> GetConfigReportParameters() {
            string vTitulo = clsFacturaBorradorGenerico.ReportName;
            if (UseExternalRpx) {
                vTitulo += " - desde RPX externo.";
            }
            Dictionary<string, string> vParams = new Dictionary<string, string>();
            //vParams.Add("NombreCompania", AppMemoryInfo.GlobalValuesGetString("Compania", "Nombre"));
            vParams.Add("NombreCompania", "");
            vParams.Add("TituloInforme", vTitulo);
            return vParams;
        }

        public override void RunReport() {
            if (WorkerCancellPending()) {
                return;
            }
            WorkerReportProgress(30, "Obteniendo datos...");
            IFacturaInformes vRpt = new Galac.Adm.Brl.Venta.Reportes.clsFacturaRpt() as IFacturaInformes;
            Data = vRpt.BuildFacturaBorradorGenerico(_ConsecutivoCompania, _NumeroDocumento, _TipoDocumento, _StatusDocumento, _Talonario, _FormaDeOrdenarDetalleFactura, _ImprimirFacturaConSubtotalesPorLineaDeProducto, _CiudadCompania, _NombreOperador);
        }

        public override void SendReportToDevice() {
            WorkerReportProgress(90, "Configurando Informe...");
            Dictionary<string, string> vParams = GetConfigReportParameters();
            dsrFacturaBorradorGenerico vRpt = new dsrFacturaBorradorGenerico();
            if (vRpt.ConfigReport(Data, _NombreCompania, _ConsecutivoCompania, _NumeroDocumento, _TipoDocumento, _StatusDocumento, false, false)) {
                LibReport.SendReportToDevice(vRpt, 1, PrintingDevice, clsFacturaBorradorGenerico.ReportName, true, ExportFileFormat, "", false);
            }
            WorkerReportProgress(100, "Finalizando...");
        }
        #endregion //Metodos Generados


    } //End of class clsFacturaBorradorGenerico

} //End of namespace Galac.Adm.Rpt.Venta
