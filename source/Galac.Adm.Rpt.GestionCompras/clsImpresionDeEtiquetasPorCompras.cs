using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.ARRpt.Reports;
using System.Data;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Report;
using LibGalac.Aos.ARRpt;
using Galac.Adm.Ccl.GestionCompras;
using Galac.Comun.Ccl.SttDef;
namespace Galac.Adm.Rpt.GestionCompras {

    public class clsImpresionDeEtiquetasPorCompras: LibRptBaseMfc {
        #region Propiedades

        protected DataTable Data { get; set; }

        public eNivelDePrecio NivelDePrecio {
            get;
            set;
        }

        public string NumeroCompra {
            get;
            set;
        }

        public bool PrecioSinIva {
            get;
            set;
        }

        public bool MostrarProveedor {
            get;
            set;
        }

        #endregion //Propiedades
        #region Constructores
        public clsImpresionDeEtiquetasPorCompras(ePrintingDevice initPrintingDevice, eExportFileFormat initExportFileFormat, LibXmlMemInfo initAppMemInfo, LibXmlMFC initMfc)
            : base(initPrintingDevice, initExportFileFormat, initAppMemInfo, initMfc) {
        }
        #endregion //Constructores
        #region Metodos Generados

        public static string ReportName {
            get {
                return new dsrImpresionDeEtiquetasPorCompras().ReportTitle();
            }
        }

        public override Dictionary<string, string> GetConfigReportParameters() {
            string vTitulo = clsImpresionDeEtiquetasPorCompras.ReportName;
            if (UseExternalRpx) {
                vTitulo += " - desde RPX externo.";
            }
            Dictionary<string, string> vParams = new Dictionary<string, string>();
            vParams.Add("NombreCompania", LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Compania", "NumeroDeRIF") + " - " + LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Compania", "Nombre"));
            vParams.Add("TituloInforme", vTitulo);
            vParams.Add("PrecioSinIva", LibConvert.BoolToSN(PrecioSinIva));
            vParams.Add("MostrarProveedor", LibConvert.BoolToSN(MostrarProveedor));
            return vParams;
        }

        public override void RunReport() {
            if (WorkerCancellPending()) {
                return;
            }
            WorkerReportProgress(30, "Obteniendo datos...");
            ICompraInformes vRpt = new Galac.Adm.Brl.GestionCompras.Reportes.clsCompraRpt() as ICompraInformes;
            Data = vRpt.BuildImpresionDeComprasEtiquetas(Mfc.GetInt("Compania"), NivelDePrecio, NumeroCompra);
        }

        public override void SendReportToDevice() {
            WorkerReportProgress(90, "Configurando Informe...");
            Dictionary<string, string> vParams = GetConfigReportParameters();
            string vReportName = clsImpresionDeEtiquetasPorCompras.ReportName;
            string vRpx = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "NombrePlantillaImpresionCodigoBarrasCompras");
            if (LibText.IsNullOrEmpty(vRpx)) {
                vRpx = "rpxImpresionDeCodigoDeBarrasCompras";
            }

            dsrImpresionDeEtiquetasPorCompras vRpt = new dsrImpresionDeEtiquetasPorCompras(true, vRpx);
            if (vRpt.ConfigReport(Data, vParams)) {
                LibReport.RunReport(vRpt, true);
                vRpt.AsignarMargenesImpresionDeEtiquetasPorCompras(vRpt);
                LibReport.SendReportToDevice(vRpt, 1, PrintingDevice, vReportName, true, ExportFileFormat, "", false);
            }

            WorkerReportProgress(100, "Finalizando...");
        }
        #endregion //Metodos Generados


    } //End of class clsImprimirCostoDeCompraEntreFechas

} //End of namespace Galac.Dbo.Rpt.ComponenteNoEspecificado

