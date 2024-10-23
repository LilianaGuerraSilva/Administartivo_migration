using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Report;
using Galac.Saw.Ccl.Inventario;
using Galac.Saw.Dal.Inventario;

namespace Galac.Saw.Brl.Inventario.Reportes {

    public class clsNotaDeEntradaSalidaRpt: ILibReportInfo, INotaDeEntradaSalidaInformes {
        #region Variables
        private Dictionary<string, Dictionary<string, string>> _PropertiesForReportList;
        #endregion //Variables
        #region Propiedades
        Dictionary<string, Dictionary<string, string>> ILibReportInfo.PropertiesForReportList {
            get { return _PropertiesForReportList; }
        }
        #endregion //Propiedades
        #region Constructores

        public clsNotaDeEntradaSalidaRpt() {
            _PropertiesForReportList = new Dictionary<string, Dictionary<string, string>>();
            _PropertiesForReportList.Add("Nota de Entrada/Salida", NotaDeEntradaSalidaInfo());
        }
        #endregion //Constructores
        #region Metodos Generados

        private Dictionary<string, string> NotaDeEntradaSalidaInfo() {
            Dictionary<string, string> vResult = new Dictionary<string, string>();
            vResult.Add("SpSearchName", "dbo.Gp_NotaDeEntradaSalidaSCH");
            vResult.Add("ConfigKeyForDbService", string.Empty);
            return vResult;
        }

        System.Data.DataTable INotaDeEntradaSalidaInformes.BuildNotaDeEntradaSalidaDeInventario(int valConsecutivoCompania, string valNumeroDocumento) {
            string vSql = "";
            clsNotaDeEntradaSalidaSql insNotaDeEntradaSalidaSql = new clsNotaDeEntradaSalidaSql();
            ILibDataRpt insNotaDeEntradaSalidaDeInventario = new clsNotaDeEntradaSalidaDat();
            vSql = insNotaDeEntradaSalidaSql.SqlNotaDeEntradaSalidaDeInventario(valConsecutivoCompania, valNumeroDocumento);
            return insNotaDeEntradaSalidaDeInventario.GetDt(vSql, 0);
        }
        #endregion //Metodos Generados


    } //End of class clsNotaDeEntradaSalidaRpt

} //End of namespace Galac.Saw.Brl.Inventario

