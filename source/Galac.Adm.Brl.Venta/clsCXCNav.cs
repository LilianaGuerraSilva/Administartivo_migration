using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Galac.Adm.Ccl.Venta;
using Galac.Adm.Dal.Venta;
using LibGalac.Aos.Base;
using LibGalac.Aos.Brl;
using Galac.Saw.Ccl.SttDef;
using LibGalac.Aos.Base.Dal;
using System.Xml;

namespace Galac.Adm.Brl.Venta {
    public class clsCXCNav : LibBaseNav<IList<CxC>, IList<CxC>>, ICXCPdn {

        bool ICXCPdn.Insert(int valConsecutivoCompnaia, XElement valData) {
            clsCXCDat insCxCDat = new clsCXCDat();
            bool vResult = LibBusiness.ExecuteUpdateOrDelete(insCxCDat.CXCSqlInsertar(), insCxCDat.CXCParametrosInsertar(valConsecutivoCompnaia, valData), "", 0) >= 0;
            return vResult;
        }

        bool ILibPdn.CanBeChoosen(string valCallingModule, eAccionSR valAction, string valExtendedAction, System.Xml.XmlDocument valXmlRow) {
            bool vResult = false;
            //ILibDataFKSearch instanciaDal = new Galac.Adm.Dal.Venta.clsCxCDat();
            switch (valCallingModule) {
                default:
                    vResult = true;
                    break;
            }
            return vResult;
        }

        bool ILibPdn.GetDataForList(string valCallingModule, ref System.Xml.XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            ILibDataFKSearch instanciaDal = new Galac.Adm.Dal.Venta.clsCXCDat();
            return instanciaDal.ConnectFk(ref refXmlDocument, eProcessMessageType.SpName, "Adm.Gp_CxCSCH", valXmlParamsExpression);
        }

        XElement ILibPdn.GetFk(string valCallingModule, StringBuilder valParameters) {
            ILibDataMasterComponentWithSearch<IList<CxC>, IList<CxC>> instanciaDal = new clsCXCDat();
            return instanciaDal.QueryInfo(eProcessMessageType.SpName, "Dbo.Gp_CobranzaGetFk", valParameters);
        }

        protected override bool RetrieveListInfo(string valModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            bool vResult = false;
            ILibPdn vPdnModule;
            switch (valModule) {
                case "CXC":
                    vResult = ((ILibPdn)this).GetDataForList(valModule, ref refXmlDocument, valXmlParamsExpression);
                    break;
                case "Cliente":
                    vPdnModule = new Galac.Saw.Brl.Cliente.clsClienteNav();
                    vResult = vPdnModule.GetDataForList("CXC", ref refXmlDocument, valXmlParamsExpression);
                    break;
                case "Vendedor":
                    vPdnModule = new Galac.Saw.Brl.Vendedor.clsVendedorNav();
                    vResult = vPdnModule.GetDataForList("CXC", ref refXmlDocument, valXmlParamsExpression);
                    break;
                case "Moneda":
                    vPdnModule = new Galac.Comun.Brl.TablasGen.clsMonedaNav();
                    vResult = vPdnModule.GetDataForList("CXC", ref refXmlDocument, valXmlParamsExpression);
                    break;
                case "Zona Cobranza":
                    vPdnModule = new Galac.Saw.Brl.Tablas.clsZonaCobranzaNav();
                    vResult = vPdnModule.GetDataForList("CXC", ref refXmlDocument, valXmlParamsExpression);
                    break;
                default: throw new NotImplementedException();
            }
            return vResult;
        }
       
	    void ICXCPdn.CambiarStatusDeCxcACancelada(int valConsecutivoCompania, string valNumeroDeFactura, int valTipoDeDocumento, decimal valMontoAbonado) {
            StringBuilder vSql = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInString("Numero", valNumeroDeFactura, 20);
            vParams.AddInEnum("TipoCxc", valTipoDeDocumento);
            vParams.AddInDateTime("FechaCancelacion", LibDate.Today());            
            vSql.AppendLine(" UPDATE dbo.CxC");
            vSql.AppendLine(" SET Status = '1'");
            vSql.AppendLine(", FechaCancelacion = @FechaCancelacion");
            vSql.AppendLine(", MontoAbonado = (MontoExento + MontoGravado + MontoIVA)");
            vSql.AppendLine(" WHERE Numero = @Numero");
            vSql.AppendLine(" AND TipoCxc = @TipoCxc");
            vSql.AppendLine(" AND ConsecutivoCompania = @ConsecutivoCompania");
            LibBusiness.ExecuteUpdateOrDelete(vSql.ToString(), vParams.Get(), string.Empty, 0);
        }
    }
}

