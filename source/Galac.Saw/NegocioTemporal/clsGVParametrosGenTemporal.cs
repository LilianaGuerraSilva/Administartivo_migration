using LibGalac.Aos.Base;
using LibGalac.Aos.Brl;
using LibGalac.Aos.Brl.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Galac.Saw.NegocioTemporal {
    public class clsGVParametrosGenTemporal : ILibMefGlobalValues {
        public string Name {
            get { return "ParametrosGen"; }
        }

        public eGlobalValueType Type {
            get { return eGlobalValueType.Application; }
        }

        bool ILibMefGlobalValues.LoadCurrentValues() {
            bool vResult = ChooseCurrent();
            return vResult;
        }

        //este metodo debe estar en el navegador de parametros gen
        bool ChooseCurrent() {
            bool vResult = false;
            StringBuilder vSqlSb = new StringBuilder();
            vSqlSb.AppendLine("SELECT * "); //para efectos de que esto es provisional no se coloca campo a campo. En su momento deberan crear query explicito
            vSqlSb.AppendLine("    FROM Contab.ParametrosGen");
            XElement vElement = LibBusiness.ExecuteSelect(vSqlSb.ToString(), null, "Datos", -1);
            if (vElement == null || !vElement.HasElements) {
                //en este provisional no haremos insercion por defecto, la idea es que la cia y por ende todos sus valores globales hayan sido creados en VB6
                /*
                if (((IParametrosGenPdn)this).InsertarValoresPorDefecto()) {
                    vElement = instanciaDal.QueryInfo(eProcessMessageType.Query, vSqlSb.ToString(), null);
                }
                */
            }
            var AppMemInfo = LibGlobalValues.Instance.GetAppMemInfo();
            if (AppMemInfo != null) {
                try {
                    AppMemInfo.GlobalValuesAdd(LibXml.ToXmlElement(vElement.Descendants("GpResult").FirstOrDefault()), "ParametrosGen");
                    vResult = true;
                } catch (Exception) {
                    vResult = false;
                }
            }
            return vResult;
        }
    }
}
