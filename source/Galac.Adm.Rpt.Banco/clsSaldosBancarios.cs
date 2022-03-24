using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.ARRpt.Reports;
using System.Data;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Report;
using LibGalac.Aos.ARRpt;
using Galac.Adm.Ccl.Banco;

namespace Galac.Adm.Rpt.Banco {
	public class clsSaldosBancarios : LibRptBaseMfc {
		#region Propiedades
		protected DataTable Data { get; set; }
		public string RpxName { get; set; }
		private DateTime FechaDesde { get; set; }
		private DateTime FechaHasta { get; set; }
		private bool SoloCuentasActivas { get; set; }
		#endregion //Propiedades

		#region Constructores
		public clsSaldosBancarios(ePrintingDevice initPrintingDevice, eExportFileFormat initExportFileFormat, LibXmlMemInfo initAppMemInfo, LibXmlMFC initMfc, DateTime valFechaDesde, DateTime valFechaHasta, bool valSoloCuentasActivas)
			: base(initPrintingDevice, initExportFileFormat, initAppMemInfo, initMfc) {
			FechaDesde = valFechaDesde;
			FechaHasta = valFechaHasta;
			SoloCuentasActivas = valSoloCuentasActivas;
		}
		#endregion //Constructores

		#region Metodos Generados
		public static string ReportName {
			get { return new dsrSaldosBancarios().ReportTitle(); }
		}

		public override Dictionary<string, string> GetConfigReportParameters() {
			string vTitulo = ReportName;
			if (UseExternalRpx) {
				vTitulo += " - desde RPX externo.";
			}
			Dictionary<string, string> vParams = new Dictionary<string, string>();
			vParams.Add("NombreCompania", LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Compania", "Nombre") + " - " + LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Compania", "NumeroDeRif"));
			vParams.Add("TituloInforme", vTitulo);
			vParams.Add("FechaInicialYFinal", string.Format("del {0} al {1}", LibConvert.ToStr(FechaDesde, "dd/MM/yyyy"), LibConvert.ToStr(FechaHasta, "dd/MM/yyyy")));
			return vParams;
		}

		public override void RunReport() {
			if (WorkerCancellPending()) {
				return;
			}
			WorkerReportProgress(30, "Obteniendo datos...");
			ICuentaBancariaInformes vRpt = new Brl.Banco.Reportes.clsCuentaBancariaRpt();
			Data = vRpt.BuildSaldosBancarios(LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"), FechaDesde, FechaHasta, SoloCuentasActivas);
		}

		public override void SendReportToDevice() {
			WorkerReportProgress(90, "Configurando Informe...");
			Dictionary<string, string> vParams = GetConfigReportParameters();
			dsrSaldosBancarios vRpt = new dsrSaldosBancarios();
			if (vRpt.ConfigReport(Data, vParams)) {
				LibReport.SendReportToDevice(vRpt, 1, PrintingDevice, ReportName, true, ExportFileFormat, "", false);
			}
			WorkerReportProgress(100, "Finalizando...");
		}
		#endregion //Metodos Generados

	} //End of class clsSaldosBancarios

} //End of namespace Galac.Adm.Rpt.Banco

