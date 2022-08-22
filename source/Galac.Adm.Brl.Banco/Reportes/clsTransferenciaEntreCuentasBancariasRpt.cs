using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base.Report;
using Galac.Adm.Ccl.Banco;

namespace Galac.Adm.Brl.Banco.Reportes {

	public class clsTransferenciaEntreCuentasBancariasRpt : ILibReportInfo {
		#region Variables
		private Dictionary<string, Dictionary<string, string>> _PropertiesForReportList;
		#endregion //Variables

		#region Propiedades
		Dictionary<string, Dictionary<string, string>> ILibReportInfo.PropertiesForReportList {
			get { return _PropertiesForReportList; }
		}
		#endregion //Propiedades

		#region Constructores
		public clsTransferenciaEntreCuentasBancariasRpt() {
			_PropertiesForReportList = new Dictionary<string, Dictionary<string, string>>();
			_PropertiesForReportList.Add("Transferencia entre Cuentas Bancarias", TransferenciaEntreCuentasBancariasInfo());
		}
		#endregion //Constructores

		#region Metodos Generados
		private Dictionary<string, string> TransferenciaEntreCuentasBancariasInfo() {
			Dictionary<string, string> vResult = new Dictionary<string, string>();
			vResult.Add("SpSearchName", "Adm.Gp_TransferenciaEntreCuentasBancariasSCH");
			vResult.Add("ConfigKeyForDbService", string.Empty);
			return vResult;
		}
		#endregion //Metodos Generados

	} //End of class clsTransferenciaEntreCuentasBancariasRpt

} //End of namespace Galac.Adm.Brl.Banco

