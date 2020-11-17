using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base.Dal;
using LibGalac.Aos.Dal.Settings;
using LibGalac.Aos.Base;
using System.Threading;
using System.Data;
using System.Xml.Linq;
using LibGalac.Aos.Dal;
using System.Xml;

namespace Galac.Saw.DDL.VersionesReestructuracion {
    class clsVersion5_99:clsVersionARestructurar {
        public clsVersion5_99(string valCurrentDataBaseName)
            : base(valCurrentDataBaseName) {
            _VersionDataBase = "5.99";
        }

        public override bool UpdateToVersion() {
            StartConnectionNoTransaction();
            CorregirColumnaUSaBalanzaEnInventario();
            AgregarParametroEnFacturacion();
            UdpateNombreMonedaEnTablas();
            UdpateSimboloMonedaEnTablas();
            DisposeConnectionNoTransaction();
            return true;
        }

        private void CorregirColumnaUSaBalanzaEnInventario() {
            AlterColumnIfExist("dbo.ArticuloInventario","UsaBalanza",InsSql.CharTypeForDb(1),"",LibConvert.BoolToSN(false));
        }

        private void AgregarParametroEnFacturacion() {
            AgregarNuevoParametro("MostrarMtoTotalBsFEnObservaciones","Factura",2,"2.2.- Facturación",2,"",'2',"",'N',"N");
        }

        private void UdpateNombreMonedaEnTablas() {
            StringBuilder vSql = new StringBuilder();
            List<string> vTablas = new List<string> {
                "anticipo",
                "Cobranza",
                "Compra",
                "Contrato",
                "cotizacion",
                "CxC",
                "cxP",
                "factura",
                "Pago",                
                "saw.CuentaBancaria"
            };
            vSql.AppendLine("UPDATE Moneda SET Nombre = ");
            vSql.AppendLine(" CASE WHEN Codigo = 'VES' THEN ");
            vSql.AppendLine(" 'Bolívar' ");
            vSql.AppendLine(" WHEN Codigo = 'VEF' THEN ");
            vSql.AppendLine(" 'Bolívar Fuerte' ");
            vSql.AppendLine(" ELSE");
            vSql.AppendLine(" Nombre ");
            vSql.AppendLine(" END ");
            vSql.AppendLine(", Simbolo = ");
            vSql.AppendLine(" CASE WHEN Codigo = 'VES' THEN ");
            vSql.AppendLine(" 'Bs' ");
            vSql.AppendLine(" WHEN Codigo = 'VEF' THEN ");
            vSql.AppendLine(" 'BsF' ");
            vSql.AppendLine(" ELSE");
            vSql.AppendLine(" Simbolo ");
            vSql.AppendLine("END ");
            Execute(vSql.ToString());
            vSql.Clear();                        
            foreach(string vTabla in vTablas) {
                IList<int> ListaDeConsecutivosDeCompania = ObtenerTodosLosConsecutivosDeLasCompanias();
                string vColMoneda = "";
                foreach (int vConsecutivoCompania in ListaDeConsecutivosDeCompania) {
                    if (vTabla == "saw.CuentaBancaria") {
                        vColMoneda = "NombreDeLaMoneda";
                    } else {
                        vColMoneda = "Moneda";
                    }
                    string vSqlMoneda = "SELECT TOP 1 " + vColMoneda + " FROM " + vTabla + " WHERE " + vTabla + ".CodigoMoneda = 'VES' AND " + vTabla + "." + vColMoneda + " = 'Bolívar Soberano' AND " + vTabla + ".ConsecutivoCompania = " + vConsecutivoCompania.ToString();
                    vSql.AppendLine("UPDATE TOP (20000) " + vTabla + " SET " + vColMoneda + " = ");
                    vSql.AppendLine("'Bolívar' ");
                    vSql.AppendLine(" WHERE " + vTabla + ".CodigoMoneda = 'VES' ");
                    vSql.AppendLine(" AND " + vTabla + "." + vColMoneda + " = 'Bolívar Soberano'");
                    vSql.AppendLine(" AND ConsecutivoCompania = " + vConsecutivoCompania.ToString());
                    while (ExistenMonedasEnLaTabla(vSqlMoneda, vColMoneda)) {
                        Execute(vSql.ToString(),1500000);
                    }
                    vSql.Clear();
                }
                
            }
        }

        private void UdpateSimboloMonedaEnTablas() {
            StringBuilder vSql = new StringBuilder();
            List<string> vTablas = new List<string> {
                "anticipoCobrado",
                "anticipoPagado",
                "DocumentoCobrado",
                "DocumentoPagado",
                "OPFalsoRetencion"
            };            
            foreach(string vTabla in vTablas) {
                string vColSimbolo = "";
                string vNombreColumnaCodigoMoneda = "";
                switch (vTabla) {
                    case "DocumentoPagado":
                    case "OPFalsoRetencion":
                        vColSimbolo = "SimboloMonedaDeCxP";
                        vNombreColumnaCodigoMoneda = "CodigoMonedaDeCxP";
                        break;
                    case "DocumentoCobrado":
                        vColSimbolo = "SimboloMonedaDeCxC";
                        vNombreColumnaCodigoMoneda = "CodigoMonedaDeCxC";
                        break;
                    default:
                        vColSimbolo = "SimboloMoneda";
                        vNombreColumnaCodigoMoneda = "CodigoMoneda";
                        break;
                }
                IList<int> ListaDeConsecutivosDeCompania = ObtenerTodosLosConsecutivosDeLasCompanias();
                foreach (int vConsecutivoCompania in ListaDeConsecutivosDeCompania) {
                    vSql.AppendLine("UPDATE TOP (20000) " + vTabla + " SET " + vColSimbolo + " = 'Bs' ");
                    vSql.AppendLine(" WHERE " + vColSimbolo + " = 'BsS'");
                    vSql.AppendLine(" AND " + vNombreColumnaCodigoMoneda + " = 'VES'" );
                    vSql.AppendLine(" AND ConsecutivoCompania = " + vConsecutivoCompania.ToString());
                    string vSqlMoneda = "SELECT TOP 1 " + vColSimbolo + " FROM " + vTabla + " WHERE " + vTabla + "." + vNombreColumnaCodigoMoneda + " = 'VES' AND " + vTabla + "." + vColSimbolo + " = 'Bs' AND " + vTabla + ".ConsecutivoCompania = " + vConsecutivoCompania.ToString();
                    if (ExistenSimboloMonedasEnLaTabla(vSqlMoneda, vColSimbolo)) { 
                        Execute(vSql.ToString(),1500000);
                    }
                    vSql.Clear();
                }
            }
        }
        private IList<int> ObtenerTodosLosConsecutivosDeLasCompanias() {
            StringBuilder vSQL = new StringBuilder();
            XElement vConsultaConsecutivo = null;
            LibDatabase insDb = new LibDatabase();
            IList<int> vResult = null;
            vSQL.Append(" SELECT ConsecutivoCompania FROM COMPANIA");
            XmlDocument vData = insDb.LoadData(vSQL.ToString(),1500000);
            if (!LibXml.IsEmptyOrNull(vData)) {
                vConsultaConsecutivo = LibXml.ToXElement(vData);
                vResult = CompaniaToList(vConsultaConsecutivo);
            }
            return vResult;
        }

        IList<int> CompaniaToList(XElement valItem) {
            IList<int> vDetailList = new List<int>();
            foreach (XElement vItemDetail in valItem.Descendants("GpResult")) {
                vDetailList.Add(LibConvert.ToInt(vItemDetail.Element("ConsecutivoCompania")));
            }
            return vDetailList;
        }

        private bool ExistenMonedasEnLaTabla(string valSQL, string valNombreMoneda) {
            bool vResult = false;
            LibDatabase insDb = new LibDatabase();            
            XmlDocument vData = insDb.LoadData(valSQL,15000000);
            if (!LibXml.IsEmptyOrNull(vData)) {
                XElement vNombreMoneda = LibXml.ToXElement(vData);
                foreach (XElement vItemDetail in vNombreMoneda.Descendants("GpResult")) {
                    if (vItemDetail.Element(valNombreMoneda) != null) {
                        vResult = true;
                    };
                }
            }
            return vResult;
        }

        private bool ExistenSimboloMonedasEnLaTabla(string valSQL,string valNombreMoneda) {
            bool vResult = false;
            LibDatabase insDb = new LibDatabase();
            XmlDocument vData = insDb.LoadData(valSQL,-1);
            if (!LibXml.IsEmptyOrNull(vData)) {
                XElement vNombreMoneda = LibXml.ToXElement(vData);
                foreach (XElement vItemDetail in vNombreMoneda.Descendants("GpResult")) {
                    if (vItemDetail.Element(valNombreMoneda) != null) {
                        vResult = true;
                    };
                }
            }
            return vResult;
        }

    }
}
