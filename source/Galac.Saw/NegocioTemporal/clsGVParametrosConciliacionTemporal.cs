using LibGalac.Aos.Base;
using LibGalac.Aos.Brl;
using LibGalac.Aos.Brl.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Galac.Saw.NegocioTemporal {
    public class clsGVParametrosConciliacionTemporal : ILibMefGlobalValues {
        public string Name {
            get { return "ParametrosConciliacion"; }
        }

        public eGlobalValueType Type {
            get { return eGlobalValueType.Component; }
        }

        bool ILibMefGlobalValues.LoadCurrentValues() {
            int vConsecutivoCompania = LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania");
            if (vConsecutivoCompania > 0) {
                return ChooseCurrent();
            } else {
                LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesAddOrReplace(new XElement("ParametrosConciliacion"), "ParametrosConciliacion");
            }
            return true;
        }

        //este metodo debe estar en el navegador de parametros conciliacion
        bool ChooseCurrent() {
            bool vResult = false;
            StringBuilder vSqlSb = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"));
            vSqlSb.AppendLine("SELECT * "); //para efectos de que esto es provisional no se coloca campo a campo. En su momento deberan crear query explicito
            vSqlSb.AppendLine("    FROM Contab.ParametrosConciliacion");
            vSqlSb.AppendLine("WHERE ConsecutivoCompania = @ConsecutivoCompania");
            XElement vElement = LibBusiness.ExecuteSelect(vSqlSb.ToString(), vParams.Get(), "Datos", -1);
            if (vElement == null || !vElement.HasElements) {
                //en este provisional no haremos insercion por defecto, la idea es que la cia y por ende todos sus valores globales hayan sido creados en VB6
                /*
                if (((IParametrosConciliacionPdn)this).InsertarValoresPorDefecto(valConsecutivoCompania)) {
                    vElement = instanciaDal.QueryInfo(eProcessMessageType.Query, vSqlSb.ToString(), new LibGpParams().ToXmlParamInt("ConsecutivoCompania", valConsecutivoCompania));
                }
                */
            }
            var AppMemInfo = LibGlobalValues.Instance.GetAppMemInfo();
            if (AppMemInfo != null) {
                try {
                    AppMemInfo.GlobalValuesAdd(LibXml.ToXmlElement(vElement.Descendants("GpResult").FirstOrDefault()), "ParametrosConciliacion");
                    vResult = true;
                } catch (Exception) {
                    vResult = false;
                }
            }
            return vResult;
        }

    }
}
