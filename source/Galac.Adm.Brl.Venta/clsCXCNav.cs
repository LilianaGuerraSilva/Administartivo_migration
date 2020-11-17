using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Galac.Adm.Ccl.Venta;
using Galac.Adm.Dal.Venta;
using LibGalac.Aos.Base;
using LibGalac.Aos.Brl;
using Galac.Comun.Ccl.SttDef;
using LibGalac.Aos.Base.Dal;

namespace Galac.Adm.Brl.Venta {
    public class clsCXCNav : ICXCPdn {
        bool ICXCPdn.Insert(int valConsecutivoCompnaia, XElement valData) {
            clsCXCDat insCxCDat = new clsCXCDat();
            bool vResult = LibBusiness.ExecuteUpdateOrDelete(insCxCDat.CXCSqlInsertar(), insCxCDat.CXCParametrosInsertar(valConsecutivoCompnaia, valData), "", 0) >= 0;
            return vResult;
        }

        bool ILibPdn.CanBeChoosen(string valCallingModule, eAccionSR valAction, string valExtendedAction, System.Xml.XmlDocument valXmlRow) {
            throw new NotImplementedException();
        }

        bool ILibPdn.GetDataForList(string valCallingModule, ref System.Xml.XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            throw new NotImplementedException();
        }

        XElement ILibPdn.GetFk(string valCallingModule, StringBuilder valParameters) {
            throw new NotImplementedException();
        }

        void ICXCPdn.CambiarStatusDeCxcACancelada(int valConsecutivoCompania, string valNumeroDeFactura, int valTipoDeDocumento, decimal valMontoAbonado) {
            StringBuilder vSql = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInString("Numero", valNumeroDeFactura, 20);
            vParams.AddInEnum("TipoCxc", valTipoDeDocumento);
            vParams.AddInDateTime("FechaCancelacion", LibDate.Today());
            vParams.AddInDecimal("MontoAbonado", valMontoAbonado, 2);
            vSql.AppendLine(" UPDATE dbo.CxC");
            vSql.AppendLine(" SET Status = '1'");
            vSql.AppendLine(", FechaCancelacion = @FechaCancelacion");
            vSql.AppendLine(", MontoAbonado = @MontoAbonado");
            vSql.AppendLine(" WHERE Numero = @Numero");
            vSql.AppendLine(" AND TipoCxc = @TipoCxc");
            vSql.AppendLine(" AND ConsecutivoCompania = @ConsecutivoCompania");
            LibBusiness.ExecuteUpdateOrDelete(vSql.ToString(), vParams.Get(), string.Empty, 0);
        }
    }
}

