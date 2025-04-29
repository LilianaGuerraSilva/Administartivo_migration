using Galac.Saw.Brl.SttDef;
using Galac.Saw.Ccl.SttDef;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Dal;
using LibGalac.Aos.Brl;
using LibGalac.Aos.Brl.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Galac.Saw.NegocioTemporal {
    public class clsGVParametrosCompaniaTemporal : ILibMefGlobalValues {
        public string Name {
            get { return "ParametrosCompania"; }
        }

        public eGlobalValueType Type {
            get { return eGlobalValueType.Component; }
        }

        bool ILibMefGlobalValues.LoadCurrentValues() {
            return ParametrosCompaniaChooseCurrent();
        }

        bool ParametrosCompaniaChooseCurrent() {
            bool vResult = ChooseCurrent();
            return vResult;
        }
        //este metodo debe estar en el navegador de parametros de activo fijo
        bool ChooseCurrent() {
            bool vResult = false;
            ISettValueByCompanyPdn insParams = new clsSettValueByCompanyNav();
            string parametrosGenerales = insParams.ListadoParametrosGenerales();
            string parametrosCia = insParams.ListadoParametros(LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"));
            XElement vElement = CombinarLosParametrosEnUnXElement(parametrosGenerales, parametrosCia);

            var AppMemInfo = LibGlobalValues.Instance.GetAppMemInfo();
            if (AppMemInfo != null) {
                try {
                    AppMemInfo.GlobalValuesAdd(LibXml.ToXmlElement(vElement.Descendants("GpResult").FirstOrDefault()), "Parametros");
                    vResult = true;
                } catch (Exception) {
                    vResult = false;
                }
            }
            return vResult;
        }

        XElement CombinarLosParametrosEnUnXElement(string xmlExpressionParametrosGenerales, string xmlExpressionParametrosCia) {
            XElement elementParamGeneral = XElement.Parse(xmlExpressionParametrosGenerales);
            XElement elementParamCia = XElement.Parse(xmlExpressionParametrosCia);
            XElement elementParamAlicuota = ParametrosAlicuotasIVA();
            XElement combinedElement = new XElement("GpData",
            new XElement("GpResult",
                elementParamGeneral.Element("GpResult").Elements(),
                elementParamCia.Element("GpResult").Elements()
                , elementParamAlicuota.Element("GpResult").Elements()
                )
            );
            return combinedElement;
        }

        XElement ParametrosAlicuotasIVA() {
            StringBuilder vSqlSb = new StringBuilder();
            vSqlSb.AppendLine("SELECT TOP(1) PorcentajeAlicuota1 = MontoAlicuotaGeneral");
            vSqlSb.AppendLine(", PorcentajeAlicuota2 = MontoAlicuota2");
            vSqlSb.AppendLine(", PorcentajeAlicuota3 = MontoAlicuota3, PorcentajePasajeAereo");
            vSqlSb.AppendLine("FROM alicuotaIVA");
            vSqlSb.AppendLine("WHERE FechaDeInicioDeVigencia <= " + new QAdvSql(string.Empty).ToSqlValue(LibDate.Today()));
            vSqlSb.AppendLine("ORDER BY FechaDeInicioDeVigencia Desc");

            XElement vElement = LibBusiness.ExecuteSelect(vSqlSb.ToString(), null, "Datos", -1);
            if (vElement == null || !vElement.HasElements) {
            }
            return vElement;
        }
    }
}
