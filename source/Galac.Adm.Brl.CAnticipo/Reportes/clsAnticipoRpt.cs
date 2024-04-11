using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base.Report;
using Galac.Adm.Ccl.CAnticipo;
using Galac.Saw.Lib;

namespace Galac.Adm.Brl.CAnticipo.Reportes {

    public class clsAnticipoRpt: ILibReportInfo, IAnticipoInformes {
        #region Variables
        private Dictionary<string, Dictionary<string, string>> _PropertiesForReportList;
        private string CodigoMoneda { get;set; }
        #endregion //Variables
        #region Propiedades
        Dictionary<string, Dictionary<string, string>> ILibReportInfo.PropertiesForReportList {
            get { return _PropertiesForReportList; }
        }
        #endregion //Propiedades
        #region Constructores

        public clsAnticipoRpt() {
            _PropertiesForReportList = new Dictionary<string, Dictionary<string, string>>();
            _PropertiesForReportList.Add("Anticipo", AnticipoInfo());
        }
        #endregion //Constructores
        #region Metodos Generados

        private Dictionary<string, string> AnticipoInfo() {
            Dictionary<string, string> vResult = new Dictionary<string, string>();
            vResult.Add("SpSearchName", "dbo.Gp_AnticipoSCH");
            vResult.Add("ConfigKeyForDbService", string.Empty);
            return vResult;
        }

        System.Data.DataTable IAnticipoInformes.BuildAnticipoPorProveedorOCliente(int valConsecutivoCompania, eStatusAnticipo valStatusAnticipo, eCantidadAImprimir valCantidadAImprimir, string valCodigoClienteProveedor, bool valOrdenamientoClienteStatus, eMonedaDelInformeMM valMonedaDelInformeMM, bool valEsCliente) {
            string vSql = "";
            clsAnticipoSql insAnticipoSql = new clsAnticipoSql();
            LibGalac.Aos.Base.ILibDataRpt insAnticipoPorProveedorOCliente = new Galac.Adm.Dal.CAnticipo.clsAnticipoDat();
            vSql = insAnticipoSql.SqlAnticipoPorProveedorOCliente(valConsecutivoCompania, valStatusAnticipo, valCantidadAImprimir, valCodigoClienteProveedor, valOrdenamientoClienteStatus, valMonedaDelInformeMM, CodigoMoneda, valEsCliente);
            return insAnticipoPorProveedorOCliente.GetDt(vSql, 0);
        }
        #endregion //Metodos Generados


    } //End of class clsAnticipoRpt

} //End of namespace Galac.Adm.Brl.CAnticipo

