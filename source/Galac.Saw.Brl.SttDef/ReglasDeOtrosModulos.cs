using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base;
using Galac.Saw.Ccl.SttDef;
using System.Xml.Linq;

namespace Galac.Saw.Brl.SttDef {
    internal class ReglasDeOtrosModulos {
        private ILibDataComponentWithSearch<IList<SettValueByCompany>, IList<SettValueByCompany>> GetDataInstance() {
            return new Galac.Saw.Dal.SttDef.clsSettValueByCompanyDat();
        }




        internal string GenerateNextNumeroByTipoDocumento(int valConsecutivoCompania, string valPrefijo, string valTipoDocumentoFactura, int valPrimerDocumento, bool valFillWithCeros) {
            string strUltimoNumero = "";
            int vDblLastNumero = 0;
            XElement vXElement = BuscaElMaximoValorPorTipoDeDocumento(valConsecutivoCompania, valPrefijo, valTipoDocumentoFactura);            
            if(vXElement != null) {
                string vMaxino = (from vRecord in vXElement.Elements("GpResult")
                                  select vRecord.Element("maximo").Value).FirstOrDefault();
                if(LibString.IsNullOrEmpty(vMaxino)) {
                    int posicionDeInicioDelConsecutivo = LibText.Len(valPrefijo) + 1;
                    vDblLastNumero = LibConvert.ToInt(LibText.Mid(vMaxino, posicionDeInicioDelConsecutivo, LibString.Len(vMaxino))) + 1;
                    if(vDblLastNumero < valPrimerDocumento) {
                        vDblLastNumero = valPrimerDocumento;
                    }
                } else if(valPrimerDocumento > 1) {
                    vDblLastNumero = valPrimerDocumento;
                } else {
                    vDblLastNumero = 1;
                }
            } else {
                vDblLastNumero = 1;
            }
            if(valFillWithCeros) {
                strUltimoNumero = LibText.FillWithCharToLeft(LibConvert.ToStr(vDblLastNumero), "0", LibConvert.ToByte(11 - LibText.Len(valPrefijo)));
            }
            return  ValidaNumeroGenerado(valConsecutivoCompania,strUltimoNumero, valTipoDocumentoFactura, vDblLastNumero, valPrefijo);
        }

        private string ValidaNumeroGenerado(int valConsecutivoCompania, string strUltimoNumero, string valTipoDocumentoFactura, int vDblLastNumero, string valPrefijo) {
            StringBuilder vSQL = new StringBuilder();
            ILibDataComponent<IList<SettValueByCompany>, IList<SettValueByCompany>> instanciaDal = GetDataInstance();
            bool vPuedoSalir = false;
            while(!vPuedoSalir) {
                vSQL = new StringBuilder();
                vSQL.AppendLine(" SELECT Numero FROM factura");
                vSQL.AppendLine(" WHERE ");
                vSQL.AppendLine(" CONSECUTIVOCOMPANIA =  @ConsecutivoCompania ");
                vSQL.AppendLine(" AND TipoDeDocumento = @TipoDocumentoFactura");
                vSQL.AppendLine(" AND Numero = @StrUltimoNumero");
                LibGpParams insParams = new LibGpParams();
                insParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
                insParams.AddInEnum("TipoDocumentoFactura", valTipoDocumentoFactura);
                insParams.AddInString("StrUltimoNumero", strUltimoNumero, 11);
                XElement vXElement = instanciaDal.QueryInfo(eProcessMessageType.Query, vSQL.ToString(), insParams.Get());
                if(vXElement != null) {
                    string vNumero = (from vRecord in vXElement.Elements("GpResult")
                                      select vRecord.Element("Numero").Value).FirstOrDefault();
                    if(!LibString.IsNullOrEmpty(vNumero)) {
                        vDblLastNumero++;
                        strUltimoNumero = LibText.Trim(LibConvert.ToStr(vDblLastNumero));
                        strUltimoNumero = LibText.FillWithCharToLeft(strUltimoNumero, "0", LibConvert.ToByte(11 - LibText.Len(valPrefijo)));
                        strUltimoNumero = valPrefijo + strUltimoNumero;
                    } else {
                        vPuedoSalir = true;
                    }
                } else {
                    vPuedoSalir = true;
                }
            }
            return strUltimoNumero;
        }

        private XElement BuscaElMaximoValorPorTipoDeDocumento(int valConsecutivoCompania, string valPrefijo, string valTipoDocumentoFactura) {
            StringBuilder vSQL = new StringBuilder();
            LibGpParams insParams = new LibGpParams();
            insParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            insParams.AddInEnum("TipoDocumentoFactura", valTipoDocumentoFactura);

            //vSQL.AppendLine(" DECLARE @ConsecutivoCompania INT");
            //vSQL.AppendLine(" DECLARE @TipoDocumentoFactura VARCHAR(1)");

            vSQL.AppendLine(" SELECT ISNULL(MAX(Numero),0) AS maximo FROM factura");
            vSQL.AppendLine(" WHERE ");
            vSQL.AppendLine(" CONSECUTIVOCOMPANIA =  @ConsecutivoCompania ");
            vSQL.AppendLine(" AND TipoDeDocumento = @TipoDocumentoFactura");
            if(!LibString.IsNullOrEmpty(valPrefijo)) {
                vSQL.AppendLine(" AND NUMERO LIKE @prefijoNumero + '%' ");
                insParams.AddInEnum("prefijoNumero", valPrefijo); 
            }
            vSQL.AppendLine("  AND STATUSFACTURA = '0'");
            ILibDataComponent<IList<SettValueByCompany>, IList<SettValueByCompany>> instanciaDal = GetDataInstance();
            return instanciaDal.QueryInfo(eProcessMessageType.Query, vSQL.ToString(),insParams.Get());
        }
    }
}
