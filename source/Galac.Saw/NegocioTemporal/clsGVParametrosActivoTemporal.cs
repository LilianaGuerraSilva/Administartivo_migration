using LibGalac.Aos.Base;
using LibGalac.Aos.Brl;
using LibGalac.Aos.Brl.Contracts;
using LibGalac.Aos.DefGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Galac.Saw.NegocioTemporal {
    public class clsGVParametrosActivoTemporal : ILibMefGlobalValues {
        public string Name {
            get { return "ParametrosActivo"; }
        }

        public eGlobalValueType Type {
            get { return eGlobalValueType.Component; }
        }

        bool ILibMefGlobalValues.LoadCurrentValues() {
            int vConsecutivoCompania = LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania");
            if (vConsecutivoCompania > 0
                //&& LibDefGen.HasAccessTo(101) -- revisar despues, esta devolviendo falso
                && LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Compania", "UsaModuloDeContabilidad")
                && LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Compania", "UsaModuloDeActivoFijo")) {
                return ParametrosActivoChooseCurrent();
            } else {
                LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesAddOrReplace(new XElement("ParametrosActivo"), "ParametrosActivo");
            }
            return true;
        }

        bool ParametrosActivoChooseCurrent() {
            bool vResult = ChooseCurrent();
            return vResult;
        }
        //este metodo debe estar en el navegador de parametros de activo fijo
        bool ChooseCurrent() {
            bool vResult = false;
            StringBuilder vSqlSb = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"));
            vSqlSb.AppendLine("SELECT * "); //para efectos de que esto es provisional no se coloca campo a campo. En su momento deberan crear query explicito
            vSqlSb.AppendLine("    FROM ParametrosActivo");
            vSqlSb.AppendLine("WHERE ConsecutivoCompania = @ConsecutivoCompania");
            XElement vElement = LibBusiness.ExecuteSelect(vSqlSb.ToString(), vParams.Get(), "Datos", -1);
            if (vElement == null || !vElement.HasElements) {
                //en este provisional no haremos insercion por defecto, la idea es que la cia y por ende todos sus valores globales hayan sido creados en VB6
                /*
                if (((IParametrosActivoFijoPdn)this).InsertarValoresPorDefecto(LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"))) {
                    vElement = _Db.QueryInfo(eProcessMessageType.Query, vSqlSb.ToString(), vParams.Get());
                }
                */
            }
            var AppMemInfo = LibGlobalValues.Instance.GetAppMemInfo();
            if (AppMemInfo != null) {
                try {
                    AppMemInfo.GlobalValuesAdd(LibXml.ToXmlElement(vElement.Descendants("GpResult").FirstOrDefault()), "ParametrosActivo");
                    vResult = true;
                } catch (Exception) {
                    vResult = false;
                }
            }
            return vResult;
        }
    }
}
