using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base.Report;
using Galac.Saw.Ccl.Inventario;

namespace Galac.Saw.Brl.Inventario.Reportes {

    public class clsLoteDeInventarioRpt: ILibReportInfo, ILoteDeInventarioInformes {
        #region Variables
        private Dictionary<string, Dictionary<string, string>> _PropertiesForReportList;
        #endregion //Variables
        #region Propiedades
        Dictionary<string, Dictionary<string, string>> ILibReportInfo.PropertiesForReportList {
            get { return _PropertiesForReportList; }
        }
        #endregion //Propiedades
        #region Constructores

        public clsLoteDeInventarioRpt() {
            _PropertiesForReportList = new Dictionary<string, Dictionary<string, string>>();
            _PropertiesForReportList.Add("Lote de Inventario", LoteDeInventarioInfo());
        }
        #endregion //Constructores
        #region Metodos Generados

        private Dictionary<string, string> LoteDeInventarioInfo() {
            Dictionary<string, string> vResult = new Dictionary<string, string>();
            vResult.Add("SpSearchName", "Saw.Gp_LoteDeInventarioSCH");
            vResult.Add("ConfigKeyForDbService", string.Empty);
            return vResult;
        }

        System.Data.DataTable ILoteDeInventarioInformes.BuildArticulosPorVencer(int valConsecutivoCompania, string valLineaDeProducto, string valCodigoArticulo, int valDiasPorVencer, eOrdenarFecha valOrdenarFecha) {
            string vSql = "";
            clsLoteDeInventarioSql insLoteDeInventarioSql = new clsLoteDeInventarioSql();
            LibGalac.Aos.Base.ILibDataRpt insArticulosPorVencer = new Galac.Saw.Dal.Inventario.clsLoteDeInventarioDat();
            vSql = insLoteDeInventarioSql.SqlArticulosPorVencer(valConsecutivoCompania, valLineaDeProducto, valCodigoArticulo, valDiasPorVencer, valOrdenarFecha);
            return insArticulosPorVencer.GetDt(vSql, 0);
        }
		
		System.Data.DataTable ILoteDeInventarioInformes.BuildLoteDeInventarioVencidos(int valConsecutivoCompania, string valLineaDeProducto, string valCodigoArticulo, eOrdenarFecha valOrdenarFecha) {
            string vSql = "";
            clsLoteDeInventarioSql insLoteDeInventarioSql = new clsLoteDeInventarioSql();
            LibGalac.Aos.Base.ILibDataRpt insLoteDeInventarioVencidos = new Galac.Saw.Dal.Inventario.clsLoteDeInventarioDat();
            vSql = insLoteDeInventarioSql.SqlLoteDeInventarioVencidos(valConsecutivoCompania, valLineaDeProducto, valCodigoArticulo, valOrdenarFecha);
            return insLoteDeInventarioVencidos.GetDt(vSql, 0);
        }
        #endregion //Metodos Generados
    } //End of class clsLoteDeInventarioRpt
} //End of namespace Galac.Saw.Brl.Inventario

