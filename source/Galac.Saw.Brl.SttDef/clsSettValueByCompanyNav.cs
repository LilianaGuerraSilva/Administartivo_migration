using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.Brl;
using Galac.Saw.Ccl.SttDef;
using System.Xml.Linq;
using LibGalac.Aos.Base.Dal;
using Galac.Adm.Ccl.Vendedor;
using Galac.Saw.Ccl.Cliente;
using Galac.Comun.Ccl.Impuesto;
using Galac.Comun.Brl.Impuesto;
using System.Globalization;
using Galac.Adm.Ccl.Banco;
using Galac.Comun.Ccl.TablasGen;
using Galac.Comun.Brl.TablasGen;
using LibGalac.Aos.DefGen;
using Galac.Saw.Ccl.Inventario;
using LibGalac.Aos.Dal;
using System.Collections.ObjectModel;
using Galac.Saw.Lib;
using LibGalac.Aos.Catching;
using static Galac.Saw.LibWebConnector.clsSuscripcion;

namespace Galac.Saw.Brl.SttDef {
    public partial class clsSettValueByCompanyNav: LibBaseNav<IList<SettValueByCompany>, IList<SettValueByCompany>>, ILibPdn, ISettValueByCompanyPdn {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores
        public clsSettValueByCompanyNav() {
        }

        #endregion //Constructores
        #region Metodos Generados
        protected override ILibDataComponentWithSearch<IList<SettValueByCompany>, IList<SettValueByCompany>> GetDataInstance() {
            return new Galac.Saw.Dal.SttDef.clsSettValueByCompanyDat();
        }
        #region Miembros de ILibPdn

        bool ILibPdn.CanBeChoosen(string valCallingModule, eAccionSR valAction, string valExtendedAction, XmlDocument valXmlRow) {
            bool vResult = false;
            //ILibDataFKSearch instanciaDal = new Galac.Saw.Dal.SttDef.clsSettValueByCompanyDat();
            switch (valCallingModule) {
                default:
                    vResult = true;
                    break;
            }
            return vResult;
        }

        bool ILibPdn.GetDataForList(string valCallingModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            ILibDataFKSearch instanciaDal = new Galac.Saw.Dal.SttDef.clsSettValueByCompanyDat();
            return instanciaDal.ConnectFk(ref refXmlDocument, eProcessMessageType.SpName, "Comun.Gp_SettValueByCompanySCH", valXmlParamsExpression);
        }

        System.Xml.Linq.XElement ILibPdn.GetFk(string valCallingModule, StringBuilder valParameters) {
            ILibDataComponent<IList<SettValueByCompany>, IList<SettValueByCompany>> instanciaDal = new Galac.Saw.Dal.SttDef.clsSettValueByCompanyDat();
            return instanciaDal.QueryInfo(eProcessMessageType.SpName, "Comun.Gp_SettValueByCompanyGetFk", valParameters);
        }
        #endregion //Miembros de ILibPdn

        protected override bool RetrieveListInfo(string valModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            bool vResult = false;
            ILibPdn vPdnModule;
            switch (valModule) {
                case "Sett Value By Company":
                    vResult = ((ILibPdn)this).GetDataForList(valModule, ref refXmlDocument, valXmlParamsExpression);
                    break;
                case "Sett Definition":
                    vPdnModule = new Galac.Saw.Brl.SttDef.clsSettDefinitionNav();
                    vResult = vPdnModule.GetDataForList("Parametros", ref refXmlDocument, valXmlParamsExpression);
                    break;
                case "Cliente":
                    vPdnModule = new Galac.Saw.Brl.Cliente.clsClienteNav();
                    vResult = vPdnModule.GetDataForList("Parametros", ref refXmlDocument, valXmlParamsExpression);
                    break;
                case "Moneda":
                    vPdnModule = new Galac.Comun.Brl.TablasGen.clsMonedaNav();
                    vResult = vPdnModule.GetDataForList("Parametros", ref refXmlDocument, valXmlParamsExpression);
                    break;
                case "Cuenta Bancaria":
                    vPdnModule = new Galac.Adm.Brl.Banco.clsCuentaBancariaNav();
                    vResult = vPdnModule.GetDataForList("Parametros", ref refXmlDocument, valXmlParamsExpression);
                    break;
                case "Concepto Bancario":
                    vPdnModule = new Galac.Adm.Brl.Banco.clsConceptoBancarioNav();
                    vResult = vPdnModule.GetDataForList("Parametros", ref refXmlDocument, valXmlParamsExpression);
                    break;
                case "Ciudad":
                    vPdnModule = new Galac.Comun.Brl.TablasGen.clsCiudadNav();
                    vResult = vPdnModule.GetDataForList("Parametros", ref refXmlDocument, valXmlParamsExpression);
                    break;
                case "Vendedor":
                    vPdnModule = new Galac.Adm.Brl.Vendedor.clsVendedorNav();
                    vResult = vPdnModule.GetDataForList("Parametros", ref refXmlDocument, valXmlParamsExpression);
                    break;
                case "Almacén":
                    vPdnModule = new Galac.Saw.Brl.Inventario.clsAlmacenNav();
                    vResult = vPdnModule.GetDataForList("Parametros", ref refXmlDocument, valXmlParamsExpression);
                    break;
                case "Beneficiario":
                    vPdnModule = new Galac.Adm.Brl.Banco.clsBeneficiarioNav();
                    vResult = vPdnModule.GetDataForList("Parametros", ref refXmlDocument, valXmlParamsExpression);
                    break;
                default:
                    break;
            }
            return vResult;
        }
        #endregion //Metodos Generados
        XElement ListadoParametros(XElement valItemMaster) {
            XElement vXElement = null;
            XElement vResult = new XElement("GpData");
            XElement vGpResult = new XElement("GpResult");
            IList<SettValueByCompany> vBusinessObject = new List<SettValueByCompany>();
            var vEntity = from vRecord in valItemMaster.Descendants("GpResult")
                          select vRecord;
            foreach (XElement vItem in vEntity) {
                SettValueByCompany insSettValueByCompany = new SettValueByCompany();
                insSettValueByCompany.NameSettDefinition = LibConvert.ToStr(vItem.Element("NameSettDefinition").Value);
                if (vItem.Element("DataType").Value == "3") {
                    insSettValueByCompany.Value = LibConvert.ToStr(LibImportData.ToDec(vItem.Element("value").Value, 2), 2);
                } else {
                    insSettValueByCompany.Value = LibConvert.ToStr(vItem.Element("value").Value);
                }
                vXElement = new XElement(LibString.LCase(insSettValueByCompany.NameSettDefinition), insSettValueByCompany.Value);
                vGpResult.Add(vXElement);
            }
            vResult.Add(vGpResult);
            return vResult;
        }

        string ISettValueByCompanyPdn.ListadoParametrosGenerales() {
            string vResult = "";
            QAdvSql insQAdvSql = new QAdvSql("");
            string vWhere = "";
            StringBuilder vSql = new StringBuilder();
            vWhere = insQAdvSql.SqlBoolValueWithAnd("", "v.IsSetForAllEnterprise", true);
            vWhere = insQAdvSql.WhereSql(vWhere);
            vSql.Append("SELECT ");
            vSql.Append("NameSettDefinition,");
            vSql.Append("value, ");
            vSql.Append("DataType ");
            vSql.Append("FROM ");
            vSql.Append("Comun.Gv_SettValueByCompany_B1 v");
            vSql.Append(" INNER JOIN Comun.SettDefinition d");
            vSql.Append(" ON (v.NameSettDefinition = d.Name)");
            vSql.Append(vWhere);
            vSql.Append(" GROUP BY ");
            vSql.Append("NameSettDefinition,");
            vSql.Append("value, ");
            vSql.Append("DataType ");
            ILibDataComponent<IList<SettValueByCompany>, IList<SettValueByCompany>> instanciaDal = GetDataInstance();
            XElement vResulset = instanciaDal.QueryInfo(eProcessMessageType.Query, vSql.ToString(), null);
            if (vResulset != null) {
                vResult = ListadoParametros(vResulset).ToString();
            }
            return vResult;
        }
        string ISettValueByCompanyPdn.ListadoParametros(int valConsecutivoCompania) {
            string vResult = "";
            QAdvSql insQAdvSql = new QAdvSql("");
            string vWhere = "";
            StringBuilder vSql = new StringBuilder();
            try {
                DatosSuscripcion vSuscripcion = new LibWebConnector.clsSuscripcion().GetCaracteristicaGVentas();
                List<DatosSuscripcionCaracteristicas> vListaCaracteristicas = vSuscripcion.Caracteristicas;
                string vListaCodigos = string.Join(";", vListaCaracteristicas.Select(s => s.Codigo));                
                vSql.Append("UPDATE Comun.SettValueByCompany SET Value = " + insQAdvSql.ToSqlValue(vListaCodigos) + " WHERE NameSettDefinition = " + insQAdvSql.ToSqlValue("SuscripcionGVentas"));
                LibBusiness.ExecuteUpdateOrDelete(vSql.ToString(), null, "", 0);
            } catch (System.AccessViolationException) {
                throw;
            } catch (Exception) {
            }
            vSql.Clear();
            vWhere = insQAdvSql.SqlIntValueWithAnd("", "ConsecutivoCompania", valConsecutivoCompania);
            vWhere = insQAdvSql.WhereSql(vWhere);
            vSql.Append(" SELECT ");
            vSql.Append(" NameSettDefinition,");
            vSql.Append(" value,");
            vSql.Append(" DataType ");
            vSql.Append(" FROM ");
            vSql.Append(" Comun.Gv_SettValueByCompany_B1 v");
            vSql.Append(" INNER JOIN Comun.SettDefinition d");
            vSql.Append(" ON (v.NameSettDefinition = d.Name)");
            vSql.Append(vWhere);
            ILibDataComponent<IList<SettValueByCompany>, IList<SettValueByCompany>> instanciaDal = GetDataInstance();
            XElement vResulset = instanciaDal.QueryInfo(eProcessMessageType.Query, vSql.ToString(), null);
            if (vResulset != null) {
                vResult = ListadoParametros(vResulset).ToString();
            }
            return vResult;

        }

        #region CompaniaStt
        private CompaniaStt CompaniaSttPorDefecto() {
            CompaniaStt insEntidad = new CompaniaStt();
            insEntidad.IntegracionRISAsBool = false;
            insEntidad.TipoNegocioAsEnum = eTipoNegocio.eTN_General;
            insEntidad.TipoDeAgrupacionParaLibrosDeVentaAsEnum = eTipoDeAgrupacionParaLibrosDeVenta.PORRESUMENDEVENTAS;
            insEntidad.FechaDeInicioContabilizacion = LibConvert.ToDate("1900-01-01");
            insEntidad.FechaMinimaIngresarDatos = LibConvert.ToDate("1990-12-31");
            insEntidad.UsarVentasConIvaDiferidoAsBool = false;
            insEntidad.ImprimirVentasDiferidasAsBool = false;
            insEntidad.AplicacionAlicuotaEspecialAsEnum = eAplicacionAlicuota.No_Aplica;
            insEntidad.AplicarIVAEspecialAsBool = false;
            insEntidad.FacturarPorDefectoIvaEspecialAsBool = false;
            insEntidad.FechaInicioAlicuotaIva10Porciento = LibConvert.ToDate("2017-09-11");
            insEntidad.FechaFinAlicuotaIva10Porciento = LibConvert.ToDate("2018-12-31");
            insEntidad.ImprimirMensajeAplicacionDecretoAsBool = false;
            insEntidad.BaseDeCalculoParaAlicuotaEspecialAsEnum = eBaseCalculoParaAlicuotaEspecial.Solo_Base_Imponible_Alicuota_General;
            return insEntidad;
        }

        private void LlenaListado(CompaniaStt valRecord, ref List<SettValueByCompany> valBusinessObject, int valConsecutivoCompania) {
            valBusinessObject.Add(ConvierteValor((valRecord.FechaDeInicioContabilizacion).ToString("yyyy-MM-dd HH:mm:ss"), "FechaDeInicioContabilizacion", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.EsAsociadoEnCtaDeParticipacionAsBool), "EsAsociadoEnCtaDeParticipacion", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.VerificarDocumentoSinContabilizarAsBool), "VerificarDocumentoSinContabilizar", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.TipoDeAgrupacionParaLibrosDeVentaAsDB, "TipoDeAgrupacionParaLibrosDeVenta", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.IntegracionRISAsBool), "IntegracionRIS", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor((valRecord.FechaMinimaIngresarDatos).ToString("yyyy-MM-dd HH:mm:ss"), "FechaMinimaIngresarDatos", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.AutorellenaResumenDiarioAsBool), "AutorellenaResumenDiario", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.TipoNegocioAsDB, "TipoNegocio", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.UsarVentasConIvaDiferidoAsBool), "UsarVentasConIvaDiferido", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.ImprimirVentasDiferidasAsBool), "ImprimirVentasDiferidas", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.AplicacionAlicuotaEspecialAsDB, "AplicacionAlicuotaEspecial", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.AplicarIVAEspecialAsBool), "AplicarIVAEspecial", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.FacturarPorDefectoIvaEspecialAsBool), "FacturarPorDefectoIvaEspecial", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor((valRecord.FechaInicioAlicuotaIva10Porciento).ToString("yyyy-MM-dd HH:mm:ss"), "FechaInicioAlicuotaIva10Porciento", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor((valRecord.FechaFinAlicuotaIva10Porciento).ToString("yyyy-MM-dd HH:mm:ss"), "FechaFinAlicuotaIva10Porciento", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.ImprimirMensajeAplicacionDecretoAsBool), "ImprimirMensajeAplicacionDecreto", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.BaseDeCalculoParaAlicuotaEspecialAsDB, "BaseDeCalculoParaAlicuotaEspecial", valConsecutivoCompania));
        }


        CompaniaStt GetCompaniaStt(List<SettValueByCompany> valListGetSettValueByCompany) {
            CompaniaStt vResult = new CompaniaStt();
            vResult.Module = GetModuleSegunColumna(valListGetSettValueByCompany, "FechaDeInicioContabilizacion");
            vResult.GroupName = GetGroupNameSegunColumna(valListGetSettValueByCompany, "FechaDeInicioContabilizacion");
            vResult.FechaDeInicioContabilizacion = LibConvert.ToDate(ValorSegunColumna(valListGetSettValueByCompany, "FechaDeInicioContabilizacion"));
            vResult.EsAsociadoEnCtaDeParticipacionAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "EsAsociadoEnCtaDeParticipacion"));
            vResult.VerificarDocumentoSinContabilizarAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "VerificarDocumentoSinContabilizar"));
            vResult.TipoDeAgrupacionParaLibrosDeVentaAsEnum = (eTipoDeAgrupacionParaLibrosDeVenta)LibConvert.DbValueToEnum(ValorSegunColumna(valListGetSettValueByCompany, "TipoDeAgrupacionParaLibrosDeVenta"));
            vResult.IntegracionRISAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "IntegracionRIS"));
            vResult.FechaMinimaIngresarDatos = LibConvert.ToDate(ValorSegunColumna(valListGetSettValueByCompany, "FechaMinimaIngresarDatos"));
            vResult.AutorellenaResumenDiarioAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "AutorellenaResumenDiario"));
            vResult.TipoNegocioAsEnum = (eTipoNegocio)LibConvert.DbValueToEnum(ValorSegunColumna(valListGetSettValueByCompany, "TipoNegocio"));
            vResult.UsarVentasConIvaDiferidoAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "UsarVentasConIvaDiferido"));
            vResult.ImprimirVentasDiferidasAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "ImprimirVentasDiferidas"));
            vResult.AplicacionAlicuotaEspecialAsEnum = (eAplicacionAlicuota)LibConvert.DbValueToEnum(ValorSegunColumna(valListGetSettValueByCompany, "AplicacionAlicuotaEspecial"));
            vResult.AplicarIVAEspecialAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "AplicarIVAEspecial"));
            vResult.FacturarPorDefectoIvaEspecialAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "FacturarPorDefectoIvaEspecial"));
            vResult.FechaInicioAlicuotaIva10Porciento = LibConvert.ToDate(ValorSegunColumna(valListGetSettValueByCompany, "FechaInicioAlicuotaIva10Porciento"));
            vResult.FechaFinAlicuotaIva10Porciento = LibConvert.ToDate(ValorSegunColumna(valListGetSettValueByCompany, "FechaFinAlicuotaIva10Porciento"));
            vResult.ImprimirMensajeAplicacionDecretoAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "ImprimirMensajeAplicacionDecreto"));
            vResult.BaseDeCalculoParaAlicuotaEspecialAsEnum = (eBaseCalculoParaAlicuotaEspecial)LibConvert.DbValueToEnum(ValorSegunColumna(valListGetSettValueByCompany, "BaseDeCalculoParaAlicuotaEspecial"));
            return vResult;
        }

        #endregion //CompaniaStt
        #region  GeneralStt
        private GeneralStt GeneralSttPorDefecto() {
            GeneralStt insEntidad = new GeneralStt();
            insEntidad.OrdenamientoDeCodigoStringAsEnum = eFormaDeOrdenarCodigos.NORMAL;
            insEntidad.ValidarRifEnLaWebAsBool = false;
            insEntidad.UsaMultiplesAlicuotasAsBool = false;
            insEntidad.EsSistemaParaIGAsBool = false;
            insEntidad.UsaNotaEntregaAsBool = false;
            insEntidad.SuscripcionGVentas = "1000";
            insEntidad.NumeroIDGVentas = "";
            insEntidad.SerialConectorGVentas = "";
            return insEntidad;
        }
        private void LlenaListado(GeneralStt valRecord, ref List<SettValueByCompany> valBusinessObject, int valConsecutivoCompania) {
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.PermitirEditarIVAenCxC_CxPAsBool), "PermitirEditarIVAenCxC_CxP", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.UsaMultiplesAlicuotasAsBool), "UsaMultiplesAlicuotas", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.OrdenamientoDeCodigoStringAsDB, "OrdenamientoDeCodigoString", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.ImprimirComprobanteDeCxCAsBool), "ImprimirComprobanteDeCxC", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.ImprimirComprobanteDeCxPAsBool), "ImprimirComprobanteDeCxP", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.EsSistemaParaIGAsBool), "EsSistemaParaIG", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.ValidarRifEnLaWebAsBool), "ValidarRifEnLaWeb", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.UsaNotaEntregaAsBool), "UsaNotaEntrega", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.SuscripcionGVentas, "SuscripcionGVentas", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.SerialConectorGVentas, "SerialConectorGVentas", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.NumeroIDGVentas, "NumeroIDGVentas", valConsecutivoCompania));
        }

        GeneralStt GetGeneralStt(List<SettValueByCompany> valListGetSettValueByCompany) {
            GeneralStt vResult = new GeneralStt();
            vResult.Module = GetModuleSegunColumna(valListGetSettValueByCompany, "PermitirEditarIVAenCxC_CxP");
            vResult.GroupName = GetGroupNameSegunColumna(valListGetSettValueByCompany, "PermitirEditarIVAenCxC_CxP");
            vResult.PermitirEditarIVAenCxC_CxPAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "PermitirEditarIVAenCxC_CxP"));
            vResult.UsaMultiplesAlicuotasAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "UsaMultiplesAlicuotas"));
            vResult.OrdenamientoDeCodigoStringAsEnum = (eFormaDeOrdenarCodigos)LibConvert.DbValueToEnum(ValorSegunColumna(valListGetSettValueByCompany, "OrdenamientoDeCodigoString"));
            vResult.ImprimirComprobanteDeCxCAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "ImprimirComprobanteDeCxC"));
            vResult.ImprimirComprobanteDeCxPAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "ImprimirComprobanteDeCxP"));
            vResult.ValidarRifEnLaWebAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "ValidarRifEnLaWeb"));
            vResult.EsSistemaParaIGAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "EsSistemaParaIG"));
            vResult.UsaNotaEntregaAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "UsaNotaEntrega"));
            vResult.SuscripcionGVentas = ValorSegunColumna(valListGetSettValueByCompany, "SuscripcionGVentas");
            vResult.NumeroIDGVentas = ValorSegunColumna(valListGetSettValueByCompany, "NumeroIDGVentas");
            vResult.SerialConectorGVentas = ValorSegunColumna(valListGetSettValueByCompany, "SerialConectorGVentas");
            return vResult;
        }
        #endregion // GeneralStt
        #region FacturacionStt
        private FacturacionStt FacturacionSttPorDefecto() {
            FacturacionStt insEntidad = new FacturacionStt();
            insEntidad.UsaPrecioSinIvaAsBool = true;
            insEntidad.PermitirFacturarConCantidadCeroAsBool = false;
            insEntidad.SugerirNumeroControlFacturaAsBool = false;
            insEntidad.CambiarFechaEnCuotasLuegoDeFijarFechaEntregaAsBool = false;
            insEntidad.ItemsMontoAsEnum = eItemsMontoFactura.NO_PERMITIR_ITEMS_NEGATIVOS;
            insEntidad.ExigirRifdeClienteAlEmitirFacturaAsBool = true;
            insEntidad.UsaPrecioSinIvaEnResumenVtasAsBool = true;
            insEntidad.UsaListaDePrecioEnMonedaExtranjeraAsBool = false;
            insEntidad.VerificarFacturasManualesFaltantesAsBool = true;
            insEntidad.NumFacturasManualesFaltantes = 20;
            insEntidad.DevolucionReversoSeGeneraComoAsEnum = eTipoDocumentoFactura.NotaDeCredito;
            insEntidad.PedirInformacionLibroVentasXlsalEmitirFacturaAsBool = true;
            insEntidad.PermitirCambioTasaMondExtrajalEmitirFacturaAsBool = false;
            insEntidad.FormaDeCalculoDePrecioRenglonFacturaAsEnum = eFormaDeCalculoDePrecioRenglonFactura.APartirDelPrecioSinIVA;
            return insEntidad;
        }
        private void LlenaListado(FacturacionStt valRecord, ref List<SettValueByCompany> valBusinessObject, int valConsecutivoCompania) {
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.VerificarFacturasManualesFaltantesAsBool), "VerificarFacturasManualesFaltantes", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.ToStr(valRecord.NumFacturasManualesFaltantes), "NumFacturasManualesFaltantes", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.PermitirFacturarConCantidadCeroAsBool), "PermitirFacturarConCantidadCero", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.DevolucionReversoSeGeneraComoAsDB, "DevolucionReversoSeGeneraComo", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.ExigirRifdeClienteAlEmitirFacturaAsBool), "ExigirRifdeClienteAlEmitirFactura", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.SugerirNumeroControlFacturaAsBool), "SugerirNumeroControlFactura", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.PedirInformacionLibroVentasXlsalEmitirFacturaAsBool), "PedirInformacionLibroVentasXlsalEmitirFactura", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.TipoDeNivelDePreciosAsDB, "TipoDeNivelDePrecios", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.ComplConComodinEnBusqDeArtInvAsBool), "ComplConComodinEnBusqDeArtInv", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.UsarResumenDiarioDeVentasAsBool), "UsarResumenDiarioDeVentas", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.ItemsMontoAsDB, "ItemsMonto", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.ComisionesEnFacturaAsDB, "ComisionesEnFactura", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.ComisionesEnRenglonesAsDB, "ComisionesEnRenglones", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.CambiarFechaEnCuotasLuegoDeFijarFechaEntregaAsBool), "CambiarFechaEnCuotasLuegoDeFijarFechaEntrega", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.BuscarArticuloXSerialAlFacturarAsBool), "BuscarArticuloXSerialAlFacturar", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.NombreVendedorUno, "NombreVendedorUno", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.NombreVendedorDos, "NombreVendedorDos", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.NombreVendedorTres, "NombreVendedorTres", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.UsaPrecioSinIvaAsBool), "UsaPrecioSinIva", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.UsaPrecioSinIvaEnResumenVtasAsBool), "UsaPrecioSinIvaEnResumenVtas", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.UsaListaDePrecioEnMonedaExtranjeraAsBool), "UsaListaDePrecioEnMonedaExtranjera", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.UsarRenglonesEnResumenVtasAsBool), "UsarRenglonesEnResumenVtas", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.ResumenVtasAfectaInventarioAsBool), "ResumenVtasAfectaInventario", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.PermitirCambioTasaMondExtrajalEmitirFacturaAsBool), "PermitirCambioTasaMondExtrajalEmitirFactura", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.FormaDeCalculoDePrecioRenglonFacturaAsDB, "FormaDeCalculoDePrecioRenglonFactura", valConsecutivoCompania));
        }

        FacturacionStt GetFacturacionStt(List<SettValueByCompany> valListGetSettValueByCompany) {
            FacturacionStt vResult = new FacturacionStt();
            vResult.Module = GetModuleSegunColumna(valListGetSettValueByCompany, "VerificarFacturasManualesFaltantes");
            vResult.GroupName = GetGroupNameSegunColumna(valListGetSettValueByCompany, "VerificarFacturasManualesFaltantes");
            vResult.VerificarFacturasManualesFaltantesAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "VerificarFacturasManualesFaltantes"));
            vResult.NumFacturasManualesFaltantes = LibConvert.ToInt(ValorSegunColumna(valListGetSettValueByCompany, "NumFacturasManualesFaltantes"));
            vResult.PermitirFacturarConCantidadCeroAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "PermitirFacturarConCantidadCero"));
            vResult.DevolucionReversoSeGeneraComoAsEnum = (eTipoDocumentoFactura)LibConvert.DbValueToEnum(ValorSegunColumna(valListGetSettValueByCompany, "DevolucionReversoSeGeneraComo"));
            vResult.ExigirRifdeClienteAlEmitirFacturaAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "ExigirRifdeClienteAlEmitirFactura"));
            vResult.SugerirNumeroControlFacturaAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "SugerirNumeroControlFactura"));
            vResult.PedirInformacionLibroVentasXlsalEmitirFacturaAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "PedirInformacionLibroVentasXlsalEmitirFactura"));
            vResult.TipoDeNivelDePreciosAsEnum = (eTipoDeNivelDePrecios)LibConvert.DbValueToEnum(ValorSegunColumna(valListGetSettValueByCompany, "TipoDeNivelDePrecios"));
            vResult.ComplConComodinEnBusqDeArtInvAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "ComplConComodinEnBusqDeArtInv"));
            vResult.UsarResumenDiarioDeVentasAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "UsarResumenDiarioDeVentas"));
            vResult.ItemsMontoAsEnum = (eItemsMontoFactura)LibConvert.DbValueToEnum(ValorSegunColumna(valListGetSettValueByCompany, "ItemsMonto"));
            vResult.ComisionesEnFacturaAsEnum = (eComisionesEnFactura)LibConvert.DbValueToEnum(ValorSegunColumna(valListGetSettValueByCompany, "ComisionesEnFactura"));
            vResult.ComisionesEnRenglonesAsEnum = (eComisionesEnRenglones)LibConvert.DbValueToEnum(ValorSegunColumna(valListGetSettValueByCompany, "ComisionesEnRenglones"));
            vResult.CambiarFechaEnCuotasLuegoDeFijarFechaEntregaAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "CambiarFechaEnCuotasLuegoDeFijarFechaEntrega"));
            vResult.BuscarArticuloXSerialAlFacturarAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "BuscarArticuloXSerialAlFacturar"));
            vResult.NombreVendedorUno = ValorSegunColumna(valListGetSettValueByCompany, "NombreVendedorUno");
            vResult.NombreVendedorDos = ValorSegunColumna(valListGetSettValueByCompany, "NombreVendedorDos");
            vResult.NombreVendedorTres = ValorSegunColumna(valListGetSettValueByCompany, "NombreVendedorTres");
            vResult.UsaPrecioSinIvaAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "UsaPrecioSinIva"));
            vResult.UsaPrecioSinIvaEnResumenVtasAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "UsaPrecioSinIvaEnResumenVtas"));
            vResult.UsaListaDePrecioEnMonedaExtranjeraAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "UsaListaDePrecioEnMonedaExtranjera"));
            vResult.ResumenVtasAfectaInventarioAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "ResumenVtasAfectaInventario"));
            vResult.UsarRenglonesEnResumenVtasAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "UsarRenglonesEnResumenVtas"));
            vResult.PermitirCambioTasaMondExtrajalEmitirFacturaAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "PermitirCambioTasaMondExtrajalEmitirFactura"));
            vResult.FormaDeCalculoDePrecioRenglonFacturaAsEnum = (eFormaDeCalculoDePrecioRenglonFactura)LibConvert.DbValueToEnum(ValorSegunColumna(valListGetSettValueByCompany, "FormaDeCalculoDePrecioRenglonFactura"));
            return vResult;
        }

        #endregion // FacturacionStt
        #region FacturacionContinuacionStt
        private FacturacionContinuacionStt FacturacionContinuacionSttPorDefecto(int valConsecutivoCompania) {
            FacturacionContinuacionStt insEntidad = new FacturacionContinuacionStt();
            insEntidad.CuentaBancariaCobroDirecto = GetCuentaBancariaPorDefecto();
            insEntidad.ConceptoBancarioCobroDirecto = GetConceptoBancario("COBRO_DIRECTO_DE_FACT");
            insEntidad.CuentaBancariaCobroMultimoneda = "";
            insEntidad.ConceptoBancarioCobroMultimoneda = "";
            insEntidad.EmitirDirectoAsBool = false;
            insEntidad.UsarOtrosCargoDeFacturaAsBool = false;
            insEntidad.ForzarFechaFacturaAmesEspecificoAsBool = false;
            insEntidad.MesFacturacionEnCursoAsEnum = eMes.Enero;
            insEntidad.PermitirIncluirFacturacionHistoricaAsBool = false;
            insEntidad.UltimaFechaDeFacturacionHistorica = LibDate.Today();
            insEntidad.GenerarCxCalEmitirUnaFacturaHistoricaAsBool = false;
            insEntidad.UsaCamposExtrasEnRenglonFacturaAsBool = false;
            insEntidad.MaximoDescuentoEnFactura = 100;
            insEntidad.PermitirDobleDescuentoEnFacturaAsBool = false;
            insEntidad.MostrarMtoTotalBsFEnObservacionesAsBool = false;
            insEntidad.UsaCobroDirectoEnMultimonedaAsBool = false;
            insEntidad.SeMuestraTotalEnDivisasAsBool = false;
            insEntidad.UsaListaDePrecioEnMonedaExtranjeraAsBool = false;
            insEntidad.UsaListaDePrecioEnMonedaExtranjeraCXCAsBool = false;
            insEntidad.NroDiasMantenerTasaCambio = 0;
            insEntidad.UsaMediosElectronicosDeCobroAsBool = false;
            insEntidad.UsaCreditoElectronicoAsBool = false;
            insEntidad.NombreCreditoElectronico = "Crédito Electrónico";
            insEntidad.DiasUsualesCreditoElectronico = 14;
            insEntidad.DiasMaximoCreditoElectronico = 14;
            return insEntidad;
        }
        private void LlenaListado(FacturacionContinuacionStt valRecord, ref List<SettValueByCompany> valBusinessObject, int valConsecutivoCompania) {
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.ForzarFechaFacturaAmesEspecificoAsBool), "ForzarFechaFacturaAmesEspecifico", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.MesFacturacionEnCursoAsDB, "MesFacturacionEnCurso", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.PermitirIncluirFacturacionHistoricaAsBool), "PermitirIncluirFacturacionHistorica", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor((valRecord.UltimaFechaDeFacturacionHistorica).ToString("yyyy-MM-dd HH:mm:ss"), "UltimaFechaDeFacturacionHistorica", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.GenerarCxCalEmitirUnaFacturaHistoricaAsBool), "GenerarCxCalEmitirUnaFacturaHistorica", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.AccionAlAnularFactDeMesesAntAsDB, "AccionAlAnularFactDeMesesAnt", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.UsarOtrosCargoDeFacturaAsBool), "UsarOtrosCargoDeFactura", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.UsaCamposExtrasEnRenglonFacturaAsBool), "UsaCamposExtrasEnRenglonFactura", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.EmitirDirectoAsBool), "EmitirDirecto", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.UsaCobroDirectoAsBool), "UsaCobroDirecto", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.UsaCobroDirectoEnMultimonedaAsBool), "UsaCobroDirectoEnMultimoneda", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.CuentaBancariaCobroDirecto, "CuentaBancariaCobroDirecto", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.ConceptoBancarioCobroDirecto, "ConceptoBancarioCobroDirecto", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.CuentaBancariaCobroMultimoneda, "CuentaBancariaCobroMultimoneda", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.ConceptoBancarioCobroMultimoneda, "ConceptoBancarioCobroMultimoneda", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.PermitirDobleDescuentoEnFacturaAsBool), "PermitirDobleDescuentoEnFactura", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.ToStr(valRecord.MaximoDescuentoEnFactura), "MaximoDescuentoEnFactura", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.ToStr(valRecord.BloquearEmisionAsDB), "BloquearEmision", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.MostrarMtoTotalBsFEnObservacionesAsBool), "MostrarMtoTotalBsFEnObservaciones", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.SeMuestraTotalEnDivisasAsBool), "SeMuestraTotalEnDivisas", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.UsaListaDePrecioEnMonedaExtranjeraAsBool), "UsaListaDePrecioEnMonedaExtranjera", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.UsaListaDePrecioEnMonedaExtranjeraCXCAsBool), "UsaListaDePrecioEnMonedaExtranjeraCXC", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.ToStr(valRecord.NroDiasMantenerTasaCambio), "NroDiasMantenerTasaCambio", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.ToStr(valRecord.UsaMediosElectronicosDeCobroAsBool), "UsaMediosElectronicosDeCobro", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.UsaCreditoElectronicoAsBool), "UsuaCreditoElectronico", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.NombreCreditoElectronico, "NombreCreditoElectronico", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.ToStr(valRecord.DiasUsualesCreditoElectronico), "DiasUsualesCreditoElectronico", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.ToStr(valRecord.DiasMaximoCreditoElectronico), "DiasMaximoCreditoElectronico", valConsecutivoCompania));
        }
        FacturacionContinuacionStt GetFacturacionContinuacionStt(List<SettValueByCompany> valListGetSettValueByCompany) {
            FacturacionContinuacionStt vResult = new FacturacionContinuacionStt();
            vResult.Module = GetModuleSegunColumna(valListGetSettValueByCompany, "ForzarFechaFacturaAmesEspecifico");
            vResult.GroupName = GetGroupNameSegunColumna(valListGetSettValueByCompany, "ForzarFechaFacturaAmesEspecifico");
            vResult.ForzarFechaFacturaAmesEspecificoAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "ForzarFechaFacturaAmesEspecifico"));
            vResult.MesFacturacionEnCursoAsEnum = (eMes)LibConvert.DbValueToEnum(ValorSegunColumna(valListGetSettValueByCompany, "MesFacturacionEnCurso"));
            vResult.PermitirIncluirFacturacionHistoricaAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "PermitirIncluirFacturacionHistorica"));
            vResult.UltimaFechaDeFacturacionHistorica = LibConvert.ToDate(ValorSegunColumna(valListGetSettValueByCompany, "UltimaFechaDeFacturacionHistorica"));
            vResult.GenerarCxCalEmitirUnaFacturaHistoricaAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "GenerarCxCalEmitirUnaFacturaHistorica"));
            vResult.AccionAlAnularFactDeMesesAntAsEnum = (eAccionAlAnularFactDeMesesAnt)LibConvert.DbValueToEnum(ValorSegunColumna(valListGetSettValueByCompany, "AccionAlAnularFactDeMesesAnt"));
            vResult.UsarOtrosCargoDeFacturaAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "UsarOtrosCargoDeFactura"));
            vResult.UsaCamposExtrasEnRenglonFacturaAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "UsaCamposExtrasEnRenglonFactura"));
            vResult.EmitirDirectoAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "EmitirDirecto"));
            vResult.UsaCobroDirectoAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "UsaCobroDirecto"));
            vResult.UsaCobroDirectoEnMultimonedaAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "UsaCobroDirectoEnMultimoneda"));
            vResult.CuentaBancariaCobroDirecto = ValorSegunColumna(valListGetSettValueByCompany, "CuentaBancariaCobroDirecto");
            vResult.ConceptoBancarioCobroDirecto = ValorSegunColumna(valListGetSettValueByCompany, "ConceptoBancarioCobroDirecto");
            vResult.CuentaBancariaCobroMultimoneda = ValorSegunColumna(valListGetSettValueByCompany, "CuentaBancariaCobroMultimoneda");
            vResult.ConceptoBancarioCobroMultimoneda = ValorSegunColumna(valListGetSettValueByCompany, "ConceptoBancarioCobroMultimoneda");
            vResult.PermitirDobleDescuentoEnFacturaAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "PermitirDobleDescuentoEnFactura"));
            vResult.MaximoDescuentoEnFactura = LibConvert.ToDec(ValorSegunColumna(valListGetSettValueByCompany, "MaximoDescuentoEnFactura", true));
            vResult.BloquearEmisionAsEnum = (eBloquearEmision)LibConvert.DbValueToEnum(ValorSegunColumna(valListGetSettValueByCompany, "BloquearEmision"));
            vResult.MostrarMtoTotalBsFEnObservacionesAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "MostrarMtoTotalBsFEnObservaciones"));
            vResult.SeMuestraTotalEnDivisasAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "SeMuestraTotalEnDivisas"));
            vResult.UsaListaDePrecioEnMonedaExtranjeraAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "UsaListaDePrecioEnMonedaExtranjera"));
            vResult.UsaListaDePrecioEnMonedaExtranjeraCXCAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "UsaListaDePrecioEnMonedaExtranjeraCXC"));
            vResult.NroDiasMantenerTasaCambio = LibConvert.ToInt(ValorSegunColumna(valListGetSettValueByCompany, "NroDiasMantenerTasaCambio"));
            vResult.UsaMediosElectronicosDeCobroAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "UsaMediosElectronicosDeCobro"));
            vResult.UsaCreditoElectronicoAsBool  = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "UsuaCreditoElectronico"));
            vResult.NombreCreditoElectronico = ValorSegunColumna(valListGetSettValueByCompany, "NombreCreditoElectronico");
            vResult.DiasUsualesCreditoElectronico = LibConvert.ToInt( ValorSegunColumna(valListGetSettValueByCompany, "DiasUsualesCreditoElectronico"));
            vResult.DiasMaximoCreditoElectronico = LibConvert.ToInt(ValorSegunColumna(valListGetSettValueByCompany, "DiasMaximoCreditoElectronico"));
            return vResult;
        }

        #endregion // FacturacionStt
        #region ImpresiondeFacturaStt
        private ImpresiondeFacturaStt ImpresiondeFacturaSttPorDefecto() {
            ImpresiondeFacturaStt insEntidad = new ImpresiondeFacturaStt();
            insEntidad.FormatoDeFechaAsEnum = eTipoDeFormatoFecha.eCSF_CON_BARRA;
            insEntidad.FormatoDeFechaTexto = LibEnumHelper.GetDescription(eTipoDeFormatoFecha.eCSF_CON_BARRA);
            insEntidad.CantidadDeCopiasDeLaFacturaAlImprimir = 1;
            insEntidad.NumeroDeDigitosEnFactura = 10;
            insEntidad.NoImprimirFacturaAsBool = false;
            insEntidad.ImprimirFacturaConSubtotalesPorLineaDeProductoAsBool = false;
            insEntidad.UsarDecimalesAlImprimirCantidadAsBool = true;
            insEntidad.NombrePlantillaAnexoSeriales = "rpxAnexoSeriales";
            insEntidad.ImprimirComprobanteFiscalEnContratoAsBool = false;
            return insEntidad;
        }
        private void LlenaListado(ImpresiondeFacturaStt valRecord, ref List<SettValueByCompany> valBusinessObject, int valConsecutivoCompania) {
            valBusinessObject.Add(ConvierteValor(LibConvert.ToStr(valRecord.NumeroDeDigitosEnFactura), "NumeroDeDigitosEnFactura", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.ToStr(valRecord.CantidadDeCopiasDeLaFacturaAlImprimir), "CantidadDeCopiasDeLaFacturaAlImprimir", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.UsarDecimalesAlImprimirCantidadAsBool), "UsarDecimalesAlImprimirCantidad", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.DetalleProdCompFacturaAsBool), "DetalleProdCompFactura", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.FormaDeOrdenarDetalleFacturaAsDB, "FormaDeOrdenarDetalleFactura", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.ImprimirFacturaConSubtotalesPorLineaDeProductoAsBool), "ImprimirFacturaConSubtotalesPorLineaDeProducto", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.NoImprimirFacturaAsBool), "NoImprimirFactura", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.ImprimirBorradorAlInsertarFacturaAsBool), "ImprimirBorradorAlInsertarFactura", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.ConcatenaLetraEaArticuloExentoAsBool), "ConcatenaLetraEaArticuloExento", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.ImprimirTipoCobroEnFacturaAsBool), "ImprimirTipoCobroEnFactura", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.ImprimirBorradorAlInsertarFacturaAsBool), "ImprimeDireccionAlFinalDelComprobanteFiscal", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.ToStr(valRecord.NumItemImprimirFactura), "NumItemImprimirFactura", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.AccionLimiteItemsFacturaAsDB, "AccionLimiteItemsFactura", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.FormatoDeFechaAsDB, "FormatoDeFecha", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.FormatoDeFechaTexto, "FormatoDeFechaTexto", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.ImprimirAnexoDeSerialAsBool), "ImprimirAnexoDeSerial", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.NombrePlantillaAnexoSeriales, "NombrePlantillaAnexoSeriales", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.ImprimirComprobanteFiscalEnContratoAsBool), "ImprimirComprobanteFiscalEnContrato", valConsecutivoCompania));
        }

        ImpresiondeFacturaStt GetImpresiondeFacturaStt(List<SettValueByCompany> valListGetSettValueByCompany) {
            ImpresiondeFacturaStt vResult = new ImpresiondeFacturaStt();
            vResult.Module = GetModuleSegunColumna(valListGetSettValueByCompany, "NumeroDeDigitosEnFactura");
            vResult.GroupName = GetGroupNameSegunColumna(valListGetSettValueByCompany, "NumeroDeDigitosEnFactura");
            vResult.NumeroDeDigitosEnFactura = LibConvert.ToInt(ValorSegunColumna(valListGetSettValueByCompany, "NumeroDeDigitosEnFactura"));
            vResult.CantidadDeCopiasDeLaFacturaAlImprimir = LibConvert.ToInt(ValorSegunColumna(valListGetSettValueByCompany, "CantidadDeCopiasDeLaFacturaAlImprimir"));
            vResult.UsarDecimalesAlImprimirCantidadAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "UsarDecimalesAlImprimirCantidad"));
            vResult.DetalleProdCompFacturaAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "DetalleProdCompFactura"));
            vResult.FormaDeOrdenarDetalleFacturaAsEnum = (eFormaDeOrdenarDetalleFactura)LibConvert.DbValueToEnum(ValorSegunColumna(valListGetSettValueByCompany, "FormaDeOrdenarDetalleFactura"));
            vResult.ImprimirFacturaConSubtotalesPorLineaDeProductoAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "ImprimirFacturaConSubtotalesPorLineaDeProducto"));
            vResult.NoImprimirFacturaAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "NoImprimirFactura"));
            vResult.ImprimirBorradorAlInsertarFacturaAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "ImprimirBorradorAlInsertarFactura"));
            vResult.ImprimeDireccionAlFinalDelComprobanteFiscalAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "ImprimeDireccionAlFinalDelComprobanteFiscal"));
            vResult.ConcatenaLetraEaArticuloExentoAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "ConcatenaLetraEaArticuloExento"));
            vResult.ImprimirTipoCobroEnFacturaAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "ImprimirTipoCobroEnFactura"));
            vResult.NumItemImprimirFactura = LibConvert.ToInt(ValorSegunColumna(valListGetSettValueByCompany, "NumItemImprimirFactura"));
            vResult.AccionLimiteItemsFacturaAsEnum = (eAccionLimiteItemsFactura)LibConvert.DbValueToEnum(ValorSegunColumna(valListGetSettValueByCompany, "AccionLimiteItemsFactura"));
            vResult.FormatoDeFechaAsEnum = (eTipoDeFormatoFecha)LibConvert.DbValueToEnum(ValorSegunColumna(valListGetSettValueByCompany, "FormatoDeFecha"));
            vResult.FormatoDeFechaTexto = ValorSegunColumna(valListGetSettValueByCompany, "FormatoDeFechaTexto");
            vResult.ImprimirAnexoDeSerialAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "ImprimirAnexoDeSerial"));
            vResult.NombrePlantillaAnexoSeriales = ValorSegunColumna(valListGetSettValueByCompany, "NombrePlantillaAnexoSeriales");
            vResult.ImprimirComprobanteFiscalEnContratoAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "ImprimirComprobanteFiscalEnContrato"));
            return vResult;
        }
        #endregion // ImpresiondeFacturaStt
        #region ModeloDeFacturaStt
        private ModeloDeFacturaStt ModeloDeFacturaSttPorDefecto() {
            ModeloDeFacturaStt insEntidad = new ModeloDeFacturaStt();
            insEntidad.ModeloDeFacturaAsEnum = eModeloDeFactura.eMD_FORMALIBRE;
            insEntidad.PrimeraFactura = "00000000001";
            insEntidad.TipoDePrefijoAsEnum = eTipoDePrefijo.SinPrefijo;
            insEntidad.Prefijo = "";
            insEntidad.FacturaPreNumeradaAsBool = true;
            insEntidad.ModeloFacturaModoTexto = "Factura_PosEpsonTMU220FormatoLibre";
            insEntidad.ModeloDeFactura2AsEnum = eModeloDeFactura.eMD_FORMALIBRE;
            insEntidad.PrimeraFactura2 = "00000000001";
            insEntidad.FacturaPreNumerada2AsBool = true;
            insEntidad.TipoDePrefijo2AsEnum = eTipoDePrefijo.SinPrefijo;
            insEntidad.ModeloFacturaModoTexto2 = "Factura_PosEpsonTMU220FormatoLibre";
            insEntidad.UsarDosTalonariosAsBool = false;
            insEntidad.NombrePlantillaSubFacturaConOtrosCargos = "rpxSubFacturaConOtrosCargos";
            return insEntidad;
        }
        private void LlenaListado(ModeloDeFacturaStt valRecord, ref List<SettValueByCompany> valBusinessObject, int valConsecutivoCompania) {
            valBusinessObject.Add(ConvierteValor(valRecord.ModeloDeFacturaAsDB, "ModeloDeFactura", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.NombrePlantillaFactura, "NombrePlantillaFactura", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.PrimeraFactura, "PrimeraFactura", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.TipoDePrefijoAsDB, "TipoDePrefijo", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.Prefijo, "Prefijo", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.ModeloFacturaModoTexto, "ModeloFacturaModoTexto", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.FacturaPreNumeradaAsBool), "FacturaPreNumerada", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.TipoDePrefijo2AsDB, "TipoDePrefijo2", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.PrimeraFactura2, "PrimeraFactura2", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.TipoDePrefijo2AsDB, "TipoDePrefijo2", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.Prefijo2, "Prefijo2", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.ModeloFacturaModoTexto2, "ModeloFacturaModoTexto2", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.FacturaPreNumerada2AsBool), "FacturaPreNumerada2", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.ModeloDeFactura2AsDB, "ModeloDeFactura2", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.NombrePlantillaFactura2, "NombrePlantillaFactura2", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.UsarDosTalonariosAsBool), "UsarDosTalonarios", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.NombrePlantillaSubFacturaConOtrosCargos, "NombrePlantillaSubFacturaConOtrosCargos", valConsecutivoCompania));
        }

        ModeloDeFacturaStt GetModeloDeFacturaStt(List<SettValueByCompany> valListGetSettValueByCompany) {
            ModeloDeFacturaStt vResult = new ModeloDeFacturaStt();
            vResult.Module = GetModuleSegunColumna(valListGetSettValueByCompany, "ModeloDeFactura");
            vResult.GroupName = GetGroupNameSegunColumna(valListGetSettValueByCompany, "ModeloDeFactura");
            vResult.ModeloDeFacturaAsEnum = (eModeloDeFactura)LibConvert.DbValueToEnum(ValorSegunColumna(valListGetSettValueByCompany, "ModeloDeFactura"));
            vResult.NombrePlantillaFactura = ValorSegunColumna(valListGetSettValueByCompany, "NombrePlantillaFactura");
            vResult.FacturaPreNumeradaAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "FacturaPreNumerada"));
            vResult.PrimeraFactura = ValorSegunColumna(valListGetSettValueByCompany, "PrimeraFactura");
            vResult.TipoDePrefijoAsEnum = (eTipoDePrefijo)LibConvert.DbValueToEnum(ValorSegunColumna(valListGetSettValueByCompany, "TipoDePrefijo"));
            vResult.Prefijo = ValorSegunColumna(valListGetSettValueByCompany, "Prefijo");
            vResult.ModeloFacturaModoTexto = ValorSegunColumna(valListGetSettValueByCompany, "ModeloFacturaModoTexto");
            vResult.ModeloDeFactura2AsEnum = (eModeloDeFactura)LibConvert.DbValueToEnum(ValorSegunColumna(valListGetSettValueByCompany, "ModeloDeFactura2"));
            vResult.NombrePlantillaFactura2 = ValorSegunColumna(valListGetSettValueByCompany, "NombrePlantillaFactura2");
            vResult.FacturaPreNumerada2AsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "FacturaPreNumerada2"));
            vResult.PrimeraFactura2 = ValorSegunColumna(valListGetSettValueByCompany, "PrimeraFactura2");
            vResult.TipoDePrefijo2AsEnum = (eTipoDePrefijo)LibConvert.DbValueToEnum(ValorSegunColumna(valListGetSettValueByCompany, "TipoDePrefijo2"));
            vResult.Prefijo2 = ValorSegunColumna(valListGetSettValueByCompany, "Prefijo2");
            vResult.ModeloFacturaModoTexto2 = ValorSegunColumna(valListGetSettValueByCompany, "ModeloFacturaModoTexto2");
            vResult.UsarDosTalonariosAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "UsarDosTalonarios"));
            vResult.NombrePlantillaSubFacturaConOtrosCargos = ValorSegunColumna(valListGetSettValueByCompany, "NombrePlantillaSubFacturaConOtrosCargos");
            return vResult;
        }
        #endregion // ModeloDeFacturaStt
        #region CamposDefiniblesStt
        private CamposDefiniblesStt CamposDefiniblesSttPorDefecto() {
            CamposDefiniblesStt insEntidad = new CamposDefiniblesStt();
            return insEntidad;
        }
        private void LlenaListado(CamposDefiniblesStt valRecord, ref List<SettValueByCompany> valBusinessObject, int valConsecutivoCompania) {
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.UsaCamposDefiniblesAsBool), "UsaCamposDefinibles", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.NombreCampoDefinible1, "NombreCampoDefinible1", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.NombreCampoDefinible2, "NombreCampoDefinible2", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.NombreCampoDefinible3, "NombreCampoDefinible3", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.NombreCampoDefinible4, "NombreCampoDefinible4", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.NombreCampoDefinible5, "NombreCampoDefinible5", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.NombreCampoDefinible6, "NombreCampoDefinible6", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.NombreCampoDefinible7, "NombreCampoDefinible7", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.NombreCampoDefinible8, "NombreCampoDefinible8", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.NombreCampoDefinible9, "NombreCampoDefinible9", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.NombreCampoDefinible10, "NombreCampoDefinible10", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.NombreCampoDefinible11, "NombreCampoDefinible11", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.NombreCampoDefinible12, "NombreCampoDefinible12", valConsecutivoCompania));
        }
        CamposDefiniblesStt GetCamposDefiniblesStt(List<SettValueByCompany> valListGetSettValueByCompany) {
            CamposDefiniblesStt vResult = new CamposDefiniblesStt();
            vResult.Module = GetModuleSegunColumna(valListGetSettValueByCompany, "UsaCamposDefinibles");
            vResult.GroupName = GetGroupNameSegunColumna(valListGetSettValueByCompany, "UsaCamposDefinibles");
            vResult.UsaCamposDefiniblesAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "UsaCamposDefinibles"));
            vResult.NombreCampoDefinible1 = ValorSegunColumna(valListGetSettValueByCompany, "NombreCampoDefinible1");
            vResult.NombreCampoDefinible2 = ValorSegunColumna(valListGetSettValueByCompany, "NombreCampoDefinible2");
            vResult.NombreCampoDefinible3 = ValorSegunColumna(valListGetSettValueByCompany, "NombreCampoDefinible3");
            vResult.NombreCampoDefinible4 = ValorSegunColumna(valListGetSettValueByCompany, "NombreCampoDefinible4");
            vResult.NombreCampoDefinible5 = ValorSegunColumna(valListGetSettValueByCompany, "NombreCampoDefinible5");
            vResult.NombreCampoDefinible6 = ValorSegunColumna(valListGetSettValueByCompany, "NombreCampoDefinible6");
            vResult.NombreCampoDefinible7 = ValorSegunColumna(valListGetSettValueByCompany, "NombreCampoDefinible7");
            vResult.NombreCampoDefinible8 = ValorSegunColumna(valListGetSettValueByCompany, "NombreCampoDefinible8");
            vResult.NombreCampoDefinible9 = ValorSegunColumna(valListGetSettValueByCompany, "NombreCampoDefinible9");
            vResult.NombreCampoDefinible10 = ValorSegunColumna(valListGetSettValueByCompany, "NombreCampoDefinible10");
            vResult.NombreCampoDefinible11 = ValorSegunColumna(valListGetSettValueByCompany, "NombreCampoDefinible11");
            vResult.NombreCampoDefinible12 = ValorSegunColumna(valListGetSettValueByCompany, "NombreCampoDefinible12");
            return vResult;
        }

        #endregion // CamposDefiniblesStt
        #region FacturaPuntoDeVenta
        private FacturaPuntoDeVentaStt FacturaPuntoDeVentaSttPorDefecto() {
            FacturaPuntoDeVentaStt insEntidad = new FacturaPuntoDeVentaStt();
            insEntidad.AcumularItemsEnRenglonesDeFacturaAsBool = true;
            insEntidad.UsaPrecioSinIvaAsBool = true;
            insEntidad.TipoDeNivelDePreciosAsEnum = eTipoDeNivelDePrecios.PorUsuario;
            insEntidad.CuentaBancariaCobroDirecto = GetCuentaBancariaPorDefecto();
            insEntidad.ConceptoBancarioCobroDirecto = GetConceptoBancario("COBRO_DIRECTO_DE_FACT");
            insEntidad.ImprimeDireccionAlFinalDelComprobanteFiscalAsBool = false;
            insEntidad.UsaCobroDirectoAsBool = false;
            insEntidad.UsaClienteGenericoAlFacturarAsBool = false;
            insEntidad.UsarBalanzaAsBool = false;
            insEntidad.UsaBusquedaDinamicaEnPuntoDeVentaAsBool = false;
            return insEntidad;
        }

        private void LlenaListado(FacturaPuntoDeVentaStt valRecord, ref List<SettValueByCompany> valBusinessObject, int valConsecutivoCompania) {
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.AcumularItemsEnRenglonesDeFacturaAsBool), "AcumularItemsEnRenglonesDeFactura", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.UsaPrecioSinIvaAsBool), "UsaPrecioSinIva", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.TipoDeNivelDePreciosAsDB, "TipoDeNivelDePrecios", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.CuentaBancariaCobroDirecto, "CuentaBancariaCobroDirecto", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.ConceptoBancarioCobroDirecto, "ConceptoBancarioCobroDirecto", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.ImprimeDireccionAlFinalDelComprobanteFiscalAsBool), "ImprimeDireccionAlFinalDelComprobanteFiscal", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.UsaCobroDirectoAsBool), "UsaCobroDirecto", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.UsaClienteGenericoAlFacturarAsBool), "UsaClienteGenericoAlFacturar", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.UsarBalanzaAsBool), "UsarBalanza", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.UsaBusquedaDinamicaEnPuntoDeVentaAsBool), "UsaBusquedaDinamicaEnPuntoDeVenta", valConsecutivoCompania));
        }

        FacturaPuntoDeVentaStt GetFacturaPuntoDeVentaStt(List<SettValueByCompany> valListGetSettValueByCompany) {
            FacturaPuntoDeVentaStt vResult = new FacturaPuntoDeVentaStt();
            vResult.Module = GetModuleSegunColumna(valListGetSettValueByCompany, "AcumularItemsEnRenglonesDeFactura");
            vResult.GroupName = GetGroupNameSegunColumna(valListGetSettValueByCompany, "AcumularItemsEnRenglonesDeFactura");
            vResult.AcumularItemsEnRenglonesDeFacturaAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "AcumularItemsEnRenglonesDeFactura"));
            vResult.UsaPrecioSinIvaAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "UsaPrecioSinIva"));
            vResult.TipoDeNivelDePreciosAsEnum = (eTipoDeNivelDePrecios)LibConvert.DbValueToEnum(ValorSegunColumna(valListGetSettValueByCompany, "TipoDeNivelDePrecios"));
            vResult.CuentaBancariaCobroDirecto = ValorSegunColumna(valListGetSettValueByCompany, "CuentaBancariaCobroDirecto");
            vResult.ConceptoBancarioCobroDirecto = ValorSegunColumna(valListGetSettValueByCompany, "ConceptoBancarioCobroDirecto");
            vResult.ImprimeDireccionAlFinalDelComprobanteFiscalAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "ImprimeDireccionAlFinalDelComprobanteFiscal"));
            vResult.UsaCobroDirectoAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "UsaCobroDirecto"));
            vResult.UsaClienteGenericoAlFacturarAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "UsaClienteGenericoAlFacturar"));
            vResult.UsarBalanzaAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "UsarBalanza"));
            vResult.UsaBusquedaDinamicaEnPuntoDeVentaAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "UsaBusquedaDinamicaEnPuntoDeVenta"));
            return vResult;
        }

        #endregion // FacturaPuntoDeVentaStt
        #region FacturaBalanzaEtiquetas
        private FacturaBalanzaEtiquetasStt FacturaBalanzaEtiquetasSttPorDefecto() {
            FacturaBalanzaEtiquetasStt insEntidad = new FacturaBalanzaEtiquetasStt();
            insEntidad.UsaPesoEnCodigoAsBool = false;
            insEntidad.PrefijoCodigoPeso = "";
            insEntidad.NumDigitosCodigoArticuloPeso = 0;
            insEntidad.PosicionCodigoArticuloPeso = 0;
            insEntidad.NumDigitosPeso = 0;
            insEntidad.NumDecimalesPeso = 0;
            insEntidad.UsaPrecioEnCodigoAsBool = false;
            insEntidad.PrefijoCodigoPrecio = "";
            insEntidad.NumDigitosCodigoArticuloPrecio = 0;
            insEntidad.PosicionCodigoArticuloPrecio = 0;
            insEntidad.NumDigitosPrecio = 0;
            insEntidad.NumDecimalesPrecio = 0;
            insEntidad.PrecioIncluyeIvaAsBool = true;
            return insEntidad;
        }
        private void LlenaListado(FacturaBalanzaEtiquetasStt valRecord, ref List<SettValueByCompany> valBusinessObject, int valConsecutivoCompania) {
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.UsaPesoEnCodigoAsBool), "UsaPesoEnCodigo", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.PrefijoCodigoPeso, "PrefijoCodigoPeso", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.ToStr(valRecord.NumDigitosCodigoArticuloPeso), "NumDigitosCodigoArticuloPeso", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.ToStr(valRecord.PosicionCodigoArticuloPeso), "PosicionCodigoArticuloPeso", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.ToStr(valRecord.NumDigitosPeso), "NumDigitosPeso", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.ToStr(valRecord.NumDecimalesPeso), "NumDecimalesPeso", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.UsaPrecioEnCodigoAsBool), "UsaPrecioEnCodigo", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.PrefijoCodigoPrecio, "PrefijoCodigoPrecio", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.ToStr(valRecord.NumDigitosCodigoArticuloPrecio), "NumDigitosCodigoArticuloPrecio", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.ToStr(valRecord.PosicionCodigoArticuloPrecio), "PosicionCodigoArticuloPrecio", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.ToStr(valRecord.NumDigitosPrecio), "NumDigitosPrecio", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.ToStr(valRecord.NumDecimalesPrecio), "NumDecimalesPrecio", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.PrecioIncluyeIvaAsBool), "PrecioIncluyeIva", valConsecutivoCompania));
        }
        FacturaBalanzaEtiquetasStt GetFacturaBalanzaEtiquetasStt(List<SettValueByCompany> valListGetSettValueByCompany) {
            FacturaBalanzaEtiquetasStt vResult = new FacturaBalanzaEtiquetasStt();
            vResult.Module = GetModuleSegunColumna(valListGetSettValueByCompany, "UsaPesoEnCodigo");
            vResult.GroupName = GetGroupNameSegunColumna(valListGetSettValueByCompany, "UsaPesoEnCodigo");
            vResult.UsaPesoEnCodigoAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "UsaPesoEnCodigo"));
            vResult.PrefijoCodigoPeso = ValorSegunColumna(valListGetSettValueByCompany, "PrefijoCodigoPeso");
            vResult.NumDigitosCodigoArticuloPeso = LibConvert.ToInt(ValorSegunColumna(valListGetSettValueByCompany, "NumDigitosCodigoArticuloPeso"));
            vResult.PosicionCodigoArticuloPeso = LibConvert.ToInt(ValorSegunColumna(valListGetSettValueByCompany, "PosicionCodigoArticuloPeso"));
            vResult.NumDigitosPeso = LibConvert.ToInt(ValorSegunColumna(valListGetSettValueByCompany, "NumDigitosPeso"));
            vResult.NumDecimalesPeso = LibConvert.ToInt(ValorSegunColumna(valListGetSettValueByCompany, "NumDecimalesPeso"));
            vResult.UsaPrecioEnCodigoAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "UsaPrecioEnCodigo"));
            vResult.PrefijoCodigoPrecio = ValorSegunColumna(valListGetSettValueByCompany, "PrefijoCodigoPrecio");
            vResult.NumDigitosCodigoArticuloPrecio = LibConvert.ToInt(ValorSegunColumna(valListGetSettValueByCompany, "NumDigitosCodigoArticuloPrecio"));
            vResult.PosicionCodigoArticuloPrecio = LibConvert.ToInt(ValorSegunColumna(valListGetSettValueByCompany, "PosicionCodigoArticuloPrecio"));
            vResult.NumDigitosPrecio = LibConvert.ToInt(ValorSegunColumna(valListGetSettValueByCompany, "NumDigitosPrecio"));
            vResult.NumDecimalesPrecio = LibConvert.ToInt(ValorSegunColumna(valListGetSettValueByCompany, "NumDecimalesPrecio"));
            vResult.PrecioIncluyeIvaAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "PrecioIncluyeIva"));
            return vResult;
        }
        #endregion // FacturaBalanzaEtiquetas        
        #region ImprentaDigitalStt
        private FacturaImprentaDigitalStt FacturaImprentaDigitalSttPorDefecto() {
            FacturaImprentaDigitalStt insEntidad = new FacturaImprentaDigitalStt();
            return insEntidad;
        }

        private void LlenaListado(FacturaImprentaDigitalStt valRecord, ref List<SettValueByCompany> valBusinessObject, int valConsecutivoCompania) {
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.UsaImprentaDigitalAsBool), "UsaImprentaDigital", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.FechaInicioImprentaDigital.ToString("yyyy-MM-dd HH:mm:ss"), "FechaInicioImprentaDigital", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.ProveedorImprentaDigitalAsDB, "ProveedorImprentaDigital", valConsecutivoCompania));
        }

        FacturaImprentaDigitalStt GetFacturaImprentaDigitalStt(List<SettValueByCompany> valListGetSettValueByCompany) {
            FacturaImprentaDigitalStt vResult = new FacturaImprentaDigitalStt();
            vResult.Module = GetModuleSegunColumna(valListGetSettValueByCompany, "UsaImprentaDigital");
            vResult.GroupName = GetGroupNameSegunColumna(valListGetSettValueByCompany, "UsaImprentaDigital");
            vResult.UsaImprentaDigitalAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "UsaImprentaDigital"));
            vResult.FechaInicioImprentaDigital = LibConvert.ToDate(ValorSegunColumna(valListGetSettValueByCompany, "FechaInicioImprentaDigital"));
            vResult.ProveedorImprentaDigital = ValorSegunColumna(valListGetSettValueByCompany, "ProveedorImprentaDigital");
            return vResult;
        }

        #endregion // ImprentaDigitalStt
        #region CotizacionStt
        private CotizacionStt CotizacionSttPorDefecto() {
            CotizacionStt insEntidad = new CotizacionStt();
            insEntidad.NombrePlantillaCotizacion = "rpxCotizacionFormatoLibre";
            insEntidad.ValidarArticulosAlGenerarFacturaAsBool = false;
            insEntidad.CampoCodigoAlternativoDeArticuloAsEnum = eCampoCodigoAlternativoDeArticulo.eCCADA_NoAsignado;
            insEntidad.UsaControlDespachoAsBool = false;
            return insEntidad;
        }
        private void LlenaListado(CotizacionStt valRecord, ref List<SettValueByCompany> valBusinessObject, int valConsecutivoCompania) {
            valBusinessObject.Add(ConvierteValor(valRecord.NombrePlantillaCotizacion, "NombrePlantillaCotizacion", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.DetalleProdCompCotizacionAsBool), "DetalleProdCompCotizacion", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.UsaControlDespachoAsBool), "UsaControlDespacho", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.LimpiezaDeCotizacionXFacturaAsBool), "LimpiezaDeCotizacionXFactura", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.ValidarArticulosAlGenerarFacturaAsBool), "ValidarArticulosAlGenerarFactura", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.CampoCodigoAlternativoDeArticuloAsDB, "CampoCodigoAlternativoDeArticulo", valConsecutivoCompania));
        }
        CotizacionStt GetCotizacionStt(List<SettValueByCompany> valListGetSettValueByCompany) {
            CotizacionStt vResult = new CotizacionStt();
            vResult.Module = GetModuleSegunColumna(valListGetSettValueByCompany, "NombrePlantillaCotizacion");
            vResult.GroupName = GetGroupNameSegunColumna(valListGetSettValueByCompany, "NombrePlantillaCotizacion");
            vResult.NombrePlantillaCotizacion = ValorSegunColumna(valListGetSettValueByCompany, "NombrePlantillaCotizacion");
            vResult.DetalleProdCompCotizacionAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "DetalleProdCompCotizacion"));
            vResult.UsaControlDespachoAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "UsaControlDespacho"));
            vResult.LimpiezaDeCotizacionXFacturaAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "LimpiezaDeCotizacionXFactura"));
            vResult.ValidarArticulosAlGenerarFacturaAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "ValidarArticulosAlGenerarFactura"));
            vResult.CampoCodigoAlternativoDeArticuloAsEnum = (eCampoCodigoAlternativoDeArticulo)LibConvert.DbValueToEnum(ValorSegunColumna(valListGetSettValueByCompany, "CampoCodigoAlternativoDeArticulo"));
            return vResult;
        }
        #endregion // CotizacionStt
        #region NotasDebitoCreditoEntregaStt
        private NotasDebitoCreditoEntregaStt NotasDebitoCreditoEntregaSttPorDefecto() {
            NotasDebitoCreditoEntregaStt insEntidad = new NotasDebitoCreditoEntregaStt();
            insEntidad.PrimeraNotaDeCredito = "00000000001";
            insEntidad.PrimeraNotaDeDebito = "00000000001";
            insEntidad.NCPreNumeradaAsBool = true;
            insEntidad.NDPreNumeradaAsBool = true;
            insEntidad.NombrePlantillaNotaDeCredito = "rpxNotaDeCreditoFormatoLibre";
            insEntidad.NombrePlantillaNotaDeDebito = "rpxNotaDeDebitoFormatoLibre";
            insEntidad.NombrePlantillaBoleta = "rpxBoletaFormatoLibre";
            insEntidad.PrimeraBoleta = "00000000001";
            return insEntidad;
        }
        private void LlenaListado(NotasDebitoCreditoEntregaStt valRecord, ref List<SettValueByCompany> valBusinessObject, int valConsecutivoCompania) {
            valBusinessObject.Add(ConvierteValor(valRecord.NombrePlantillaNotaDeCredito, "NombrePlantillaNotaDeCredito", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.NCPreNumeradaAsBool), "NCPreNumerada", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.PrimeraNotaDeCredito, "PrimeraNotaDeCredito", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.TipoDePrefijoNCAsDB, "TipoDePrefijoNC", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.PrefijoNC, "PrefijoNC", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.PrimeraBoleta, "PrimeraBoleta", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.NombrePlantillaBoleta, "NombrePlantillaBoleta", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.NombrePlantillaNotaDeDebito, "NombrePlantillaNotaDeDebito", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.NDPreNumeradaAsBool), "NDPreNumerada", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.PrimeraNotaDeDebito, "PrimeraNotaDeDebito", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.TipoDePrefijoNDAsDB, "TipoDePrefijoND", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.PrefijoND, "PrefijoND", valConsecutivoCompania));
        }

        NotasDebitoCreditoEntregaStt GetNotasDebitoCreditoEntregaStt(List<SettValueByCompany> valListGetSettValueByCompany) {
            NotasDebitoCreditoEntregaStt vResult = new NotasDebitoCreditoEntregaStt();
            vResult.Module = GetModuleSegunColumna(valListGetSettValueByCompany, "NombrePlantillaNotaDeCredito");
            vResult.GroupName = GetGroupNameSegunColumna(valListGetSettValueByCompany, "NombrePlantillaNotaDeCredito");
            vResult.NombrePlantillaNotaDeCredito = ValorSegunColumna(valListGetSettValueByCompany, "NombrePlantillaNotaDeCredito");
            vResult.NCPreNumeradaAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "NCPreNumerada"));
            vResult.PrimeraNotaDeCredito = ValorSegunColumna(valListGetSettValueByCompany, "PrimeraNotaDeCredito");
            vResult.TipoDePrefijoNCAsEnum = (eTipoDePrefijo)LibConvert.DbValueToEnum(ValorSegunColumna(valListGetSettValueByCompany, "TipoDePrefijoNC"));
            vResult.PrefijoNC = ValorSegunColumna(valListGetSettValueByCompany, "PrefijoNC");
            vResult.PrimeraBoleta = ValorSegunColumna(valListGetSettValueByCompany, "PrimeraBoleta");
            vResult.NombrePlantillaBoleta = ValorSegunColumna(valListGetSettValueByCompany, "NombrePlantillaBoleta");
            vResult.NombrePlantillaNotaDeDebito = ValorSegunColumna(valListGetSettValueByCompany, "NombrePlantillaNotaDeDebito");
            vResult.NDPreNumeradaAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "NDPreNumerada"));
            vResult.PrimeraNotaDeDebito = ValorSegunColumna(valListGetSettValueByCompany, "PrimeraNotaDeDebito");
            vResult.TipoDePrefijoNDAsEnum = (eTipoDePrefijo)LibConvert.DbValueToEnum(ValorSegunColumna(valListGetSettValueByCompany, "TipoDePrefijoND"));
            vResult.PrefijoND = ValorSegunColumna(valListGetSettValueByCompany, "PrefijoND");
            return vResult;
        }

        #endregion // NotasDebitoCreditoEntregaStt
        #region VendedorStt
        private VendedorStt VendedorSttPorDefecto(int valConsecutivoCompania) {
            VendedorStt insEntidad = new VendedorStt();
            insEntidad.CodigoGenericoVendedor = GetCodigoVendedorPorDefecto(valConsecutivoCompania);
            insEntidad.LongitudCodigoVendedor = ((ISettValueByCompanyPdn)this).DefaultLongitudCodigoVendedor();
            return insEntidad;
        }

        private void LlenaListado(VendedorStt valRecord, ref List<SettValueByCompany> valBusinessObject, int valConsecutivoCompania) {
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.UsaCodigoVendedorEnPantallaAsBool), "UsaCodigoVendedorEnPantalla", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.CodigoGenericoVendedor, "CodigoGenericoVendedor", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.ToStr(valRecord.LongitudCodigoVendedor), "LongitudCodigoVendedor", valConsecutivoCompania));
        }

        VendedorStt GetVendedorStt(List<SettValueByCompany> valListGetSettValueByCompany) {
            VendedorStt vResult = new VendedorStt();
            vResult.Module = GetModuleSegunColumna(valListGetSettValueByCompany, "CodigoGenericoVendedor");
            vResult.GroupName = GetGroupNameSegunColumna(valListGetSettValueByCompany, "CodigoGenericoVendedor");
            vResult.UsaCodigoVendedorEnPantallaAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "UsaCodigoVendedorEnPantalla"));
            vResult.CodigoGenericoVendedor = ValorSegunColumna(valListGetSettValueByCompany, "CodigoGenericoVendedor");
            vResult.LongitudCodigoVendedor = LibConvert.ToInt(ValorSegunColumna(valListGetSettValueByCompany, "LongitudCodigoVendedor"));
            return vResult;
        }
        #endregion // VendedorStt
        #region ClienteStt
        private ClienteStt ClienteSttPorDefecto(int valConsecutivoCompania) {
            ClienteStt insEntidad = new ClienteStt();
            insEntidad.CodigoGenericoCliente = GetCodigoClientePorDefecto(valConsecutivoCompania);
            insEntidad.RellenaCerosAlaIzquierdaAsBool = true;
            insEntidad.LongitudCodigoCliente = ((ISettValueByCompanyPdn)this).DefaultLongitudCodigoCliente();
            insEntidad.MontoApartirDelCualEnviarAvisoDeuda = 10;
            return insEntidad;
        }
        private void LlenaListado(ClienteStt valRecord, ref List<SettValueByCompany> valBusinessObject, int valConsecutivoCompania) {
            valBusinessObject.Add(ConvierteValor(valRecord.CodigoGenericoCliente, "CodigoGenericoCliente", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.ToStr(valRecord.LongitudCodigoCliente), "LongitudCodigoCliente", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.AvisoDeClienteConDeudaAsBool), "AvisoDeClienteConDeuda", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.ToStr(valRecord.MontoApartirDelCualEnviarAvisoDeuda), "MontoApartirDelCualEnviarAvisoDeuda", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.UsaCodigoClienteEnPantallaAsBool), "UsaCodigoClienteEnPantalla", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.BuscarClienteXRifAlFacturarAsBool), "BuscarClienteXRifAlFacturar", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.ColocarEnFacturaElVendedorAsinagoAlClienteAsBool), "ColocarEnFacturaElVendedorAsinagoAlCliente", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.ImprimirDatosClienteEnCompFiscalAsBool), "ImprimirDatosClienteEnCompFiscal", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.AvisoDeFacturacionMenorAsBool), "AvisoDeFacturacionMenor", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.NombreCampoDefinibleCliente1, "NombreCampoDefinibleCliente1", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.RellenaCerosAlaIzquierdaAsBool), "RellenaCerosAlaIzquierda", valConsecutivoCompania));
        }

        ClienteStt GetClienteStt(List<SettValueByCompany> valListGetSettValueByCompany) {
            ClienteStt vResult = new ClienteStt();
            vResult.Module = GetModuleSegunColumna(valListGetSettValueByCompany, "AvisoDeClienteConDeuda");
            vResult.GroupName = GetGroupNameSegunColumna(valListGetSettValueByCompany, "AvisoDeClienteConDeuda");
            vResult.CodigoGenericoCliente = ValorSegunColumna(valListGetSettValueByCompany, "CodigoGenericoCliente");
            vResult.LongitudCodigoCliente = LibConvert.ToInt(ValorSegunColumna(valListGetSettValueByCompany, "LongitudCodigoCliente"));
            vResult.AvisoDeClienteConDeudaAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "AvisoDeClienteConDeuda"));
            vResult.MontoApartirDelCualEnviarAvisoDeuda = LibConvert.ToDec(ValorSegunColumna(valListGetSettValueByCompany, "MontoApartirDelCualEnviarAvisoDeuda"));
            vResult.UsaCodigoClienteEnPantallaAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "UsaCodigoClienteEnPantalla"));
            vResult.BuscarClienteXRifAlFacturarAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "BuscarClienteXRifAlFacturar"));
            vResult.ColocarEnFacturaElVendedorAsinagoAlClienteAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "ColocarEnFacturaElVendedorAsinagoAlCliente"));
            vResult.ImprimirDatosClienteEnCompFiscalAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "ImprimirDatosClienteEnCompFiscal"));
            vResult.AvisoDeFacturacionMenorAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "AvisoDeFacturacionMenor"));
            vResult.NombreCampoDefinibleCliente1 = ValorSegunColumna(valListGetSettValueByCompany, "NombreCampoDefinibleCliente1");
            vResult.RellenaCerosAlaIzquierdaAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "RellenaCerosAlaIzquierda"));
            return vResult;
        }
        #endregion // ClienteStt
        #region CobranzasStt
        private CobranzasStt CobranzasSttPorDefecto() {
            CobranzasStt insEntidad = new CobranzasStt();
            insEntidad.ConceptoReversoCobranza = GetConceptoBancario("REVERSO_AUTOMATICO_COBRANZA");
            insEntidad.ImprimirCombrobanteAlIngresarCobranzaAsBool = false;
            insEntidad.UsarZonaCobranzaAsBool = true;
            //insEntidad.NombrePlantillaCompobanteCobranza = "rpxComprobanteDeCobro";
            return insEntidad;
        }
        private void LlenaListado(CobranzasStt valRecord, ref List<SettValueByCompany> valBusinessObject, int valConsecutivoCompania) {
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.UsarZonaCobranzaAsBool), "UsarZonaCobranza", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.SugerirConsecutivoEnCobranzaAsBool), "SugerirConsecutivoEnCobranza", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.ConceptoReversoCobranza, "ConceptoReversoCobranza", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.ImprimirCombrobanteAlIngresarCobranzaAsBool), "ImprimirCombrobanteAlIngresarCobranza", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.NombrePlantillaCompobanteCobranza, "NombrePlantillaCompobanteCobranza", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.AsignarComisionDeVendedorEnCobranzaAsBool), "AsignarComisionDeVendedorEnCobranza", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.CambiarCobradorVendedorAsBool), "CambiarCobradorVendedor", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.BloquearNumeroCobranzaAsBool), "BloquearNumeroCobranza", valConsecutivoCompania));
        }

        CobranzasStt GetCobranzasStt(List<SettValueByCompany> valListGetSettValueByCompany) {
            CobranzasStt vResult = new CobranzasStt();
            vResult.Module = GetModuleSegunColumna(valListGetSettValueByCompany, "UsarZonaCobranza");
            vResult.GroupName = GetGroupNameSegunColumna(valListGetSettValueByCompany, "UsarZonaCobranza");
            vResult.UsarZonaCobranzaAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "UsarZonaCobranza"));
            vResult.SugerirConsecutivoEnCobranzaAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "SugerirConsecutivoEnCobranza"));
            vResult.ConceptoReversoCobranza = ValorSegunColumna(valListGetSettValueByCompany, "ConceptoReversoCobranza");
            vResult.ImprimirCombrobanteAlIngresarCobranzaAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "ImprimirCombrobanteAlIngresarCobranza"));
            vResult.NombrePlantillaCompobanteCobranza = ValorSegunColumna(valListGetSettValueByCompany, "NombrePlantillaCompobanteCobranza");
            vResult.AsignarComisionDeVendedorEnCobranzaAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "AsignarComisionDeVendedorEnCobranza"));
            vResult.CambiarCobradorVendedorAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "CambiarCobradorVendedor"));
            vResult.BloquearNumeroCobranzaAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "BloquearNumeroCobranza"));
            return vResult;
        }
        #endregion // CobranzasStt
        #region ComisionesStt
        private ComisionesStt ComisionesSttPorDefecto() {
            ComisionesStt insEntidad = new ComisionesStt();
            insEntidad.FormaDeCalcularComisionesSobreCobranzaAsEnum = eCalculoParaComisionesSobreCobranzaEnBaseA.Monto;
            insEntidad.NombrePlantillaComisionSobreCobranza = "rpxComisionPorVendedorEnCobranzas";
            return insEntidad;
        }
        private void LlenaListado(ComisionesStt valRecord, ref List<SettValueByCompany> valBusinessObject, int valConsecutivoCompania) {
            valBusinessObject.Add(ConvierteValor(valRecord.FormaDeCalcularComisionesSobreCobranzaAsDB, "FormaDeCalcularComisionesSobreCobranza", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.NombrePlantillaComisionSobreCobranza, "NombrePlantillaComisionSobreCobranza", valConsecutivoCompania));
        }

        ComisionesStt GetComisionesStt(List<SettValueByCompany> valListGetSettValueByCompany) {
            ComisionesStt vResult = new ComisionesStt();
            vResult.Module = GetModuleSegunColumna(valListGetSettValueByCompany, "FormaDeCalcularComisionesSobreCobranza");
            vResult.GroupName = GetGroupNameSegunColumna(valListGetSettValueByCompany, "FormaDeCalcularComisionesSobreCobranza");
            vResult.FormaDeCalcularComisionesSobreCobranzaAsEnum = (eCalculoParaComisionesSobreCobranzaEnBaseA)LibConvert.DbValueToEnum(ValorSegunColumna(valListGetSettValueByCompany, "FormaDeCalcularComisionesSobreCobranza"));
            vResult.NombrePlantillaComisionSobreCobranza = ValorSegunColumna(valListGetSettValueByCompany, "NombrePlantillaComisionSobreCobranza");
            return vResult;
        }
        #endregion // ComisionesStt
        #region InventarioStt
        private InventarioStt InventarioSttPorDefecto(int valConsecutivoCompania) {
            InventarioStt insEntidad = new InventarioStt();
            insEntidad.CodigoAlmacenGenerico = GetCodigoAlmacenPorDefecto(valConsecutivoCompania);
            insEntidad.UsaAlmacenAsBool = false;
            insEntidad.UsarBaseImponibleDiferenteA0Y100AsBool = false;
            insEntidad.SinonimoGrupo = "Grupos de Tallas y Colores";
            insEntidad.SinonimoTalla = "Talla";
            insEntidad.SinonimoColor = "Color";
            insEntidad.SinonimoSerial = "Serial";
            insEntidad.SinonimoRollo = "Rollo";
            insEntidad.ImprimeSerialRolloLuegoDeDescripArticuloAsBool = false;
            insEntidad.UsaLoteFechaDeVencimientoAsBool = false;
            return insEntidad;
        }

        private int ObtieneConsecutivoAlmacenGenerico(string valCodigoAlmacenGenerico, int valConsecutivoCompania) {
            int vResult = 1;
            IAlmacenPdn ReglasAlmacen = new Galac.Saw.Brl.Inventario.clsAlmacenNav();
            vResult = ReglasAlmacen.ObtenerConsecutivoAlmacen(valCodigoAlmacenGenerico, valConsecutivoCompania);
            return vResult;
        }

        private void LlenaListado(InventarioStt valRecord, ref List<SettValueByCompany> valBusinessObject, int valConsecutivoCompania) {
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.UsarBaseImponibleDiferenteA0Y100AsBool), "UsarBaseImponibleDiferenteA0Y100", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.UsaAlmacenAsBool), "UsaAlmacen", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.CodigoAlmacenGenerico, "CodigoAlmacenGenerico", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.ToStr(ObtieneConsecutivoAlmacenGenerico(valRecord.CodigoAlmacenGenerico, valConsecutivoCompania)), "ConsecutivoAlmacenGenerico", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.PermitirSobregiroAsDB, "PermitirSobregiro", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.ActivarFacturacionPorAlmacenAsBool), "ActivarFacturacionPorAlmacen", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.SinonimoGrupo, "SinonimoGrupo", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.SinonimoTalla, "SinonimoTalla", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.SinonimoColor, "SinonimoColor", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.SinonimoSerial, "SinonimoSerial", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.SinonimoRollo, "SinonimoRollo", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.ImprimirTransferenciaAlInsertarAsBool), "ImprimirTransferenciaAlInsertar", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.CantidadDeDecimalesAsDB, "CantidadDeDecimales", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.NombreCampoDefinibleInventario1, "NombreCampoDefinibleInventario1", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.NombreCampoDefinibleInventario2, "NombreCampoDefinibleInventario2", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.NombreCampoDefinibleInventario3, "NombreCampoDefinibleInventario3", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.NombreCampoDefinibleInventario4, "NombreCampoDefinibleInventario4", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.NombreCampoDefinibleInventario5, "NombreCampoDefinibleInventario5", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.AsociarCentroDeCostosAsDB, "AsociarCentroDeCostos", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.AvisoDeReservasvencidasAsBool), "AvisoDeReservasvencidas", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.VerificarStockAsBool), "VerificarStock", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.ImprimeSerialRolloLuegoDeDescripArticuloAsBool), "ImprimeSerialRolloLuegoDeDescripArticulo", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.UsaLoteFechaDeVencimientoAsBool), "UsaLoteFechaDeVencimiento", valConsecutivoCompania));            
        }
        InventarioStt GetInventarioStt(List<SettValueByCompany> valListGetSettValueByCompany) {
            InventarioStt vResult = new InventarioStt();
            vResult.Module = GetModuleSegunColumna(valListGetSettValueByCompany, "UsaAlmacen");
            vResult.GroupName = GetGroupNameSegunColumna(valListGetSettValueByCompany, "UsaAlmacen");
            vResult.UsarBaseImponibleDiferenteA0Y100AsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "UsarBaseImponibleDiferenteA0Y100"));
            vResult.UsaAlmacenAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "UsaAlmacen"));
            vResult.CodigoAlmacenGenerico = ValorSegunColumna(valListGetSettValueByCompany, "CodigoAlmacenGenerico");
            vResult.ConsecutivoAlmacenGenerico = LibConvert.ToInt(ValorSegunColumna(valListGetSettValueByCompany, "ConsecutivoAlmacenGenerico"));
            vResult.PermitirSobregiroAsEnum = (ePermitirSobregiro)LibConvert.DbValueToEnum(ValorSegunColumna(valListGetSettValueByCompany, "PermitirSobregiro"));
            vResult.ActivarFacturacionPorAlmacenAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "ActivarFacturacionPorAlmacen"));
            vResult.SinonimoGrupo = ValorSegunColumna(valListGetSettValueByCompany, "SinonimoGrupo");
            vResult.SinonimoTalla = ValorSegunColumna(valListGetSettValueByCompany, "SinonimoTalla");
            vResult.SinonimoColor = ValorSegunColumna(valListGetSettValueByCompany, "SinonimoColor");
            vResult.SinonimoSerial = ValorSegunColumna(valListGetSettValueByCompany, "SinonimoSerial");
            vResult.SinonimoRollo = ValorSegunColumna(valListGetSettValueByCompany, "SinonimoRollo");
            vResult.ImprimirTransferenciaAlInsertarAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "ImprimirTransferenciaAlInsertar"));
            vResult.CantidadDeDecimalesAsEnum = (eCantidadDeDecimales)LibConvert.DbValueToEnum(ValorSegunColumna(valListGetSettValueByCompany, "CantidadDeDecimales"));
            vResult.NombreCampoDefinibleInventario1 = ValorSegunColumna(valListGetSettValueByCompany, "NombreCampoDefinibleInventario1");
            vResult.NombreCampoDefinibleInventario2 = ValorSegunColumna(valListGetSettValueByCompany, "NombreCampoDefinibleInventario2");
            vResult.NombreCampoDefinibleInventario3 = ValorSegunColumna(valListGetSettValueByCompany, "NombreCampoDefinibleInventario3");
            vResult.NombreCampoDefinibleInventario4 = ValorSegunColumna(valListGetSettValueByCompany, "NombreCampoDefinibleInventario4");
            vResult.NombreCampoDefinibleInventario5 = ValorSegunColumna(valListGetSettValueByCompany, "NombreCampoDefinibleInventario5");
            vResult.AsociarCentroDeCostosAsEnum = (eFormaDeAsociarCentroDeCostos)LibConvert.DbValueToEnum(ValorSegunColumna(valListGetSettValueByCompany, "AsociarCentroDeCostos"));
            vResult.AvisoDeReservasvencidasAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "AvisoDeReservasvencidas"));
            vResult.VerificarStockAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "VerificarStock"));
            vResult.ImprimeSerialRolloLuegoDeDescripArticuloAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "ImprimeSerialRolloLuegoDeDescripArticulo"));
            vResult.UsaLoteFechaDeVencimientoAsBool= LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "UsaLoteFechaDeVencimiento"));
            return vResult;
        }
        #endregion // InventarioStt
        #region NotaEntradaSalidaStt
        private NotaEntradaSalidaStt NotaEntradaSalidaSttPorDefecto() {
            NotaEntradaSalidaStt insEntidad = new NotaEntradaSalidaStt();
            insEntidad.NombrePlantillaNotaEntradaSalida = "rpxNotaES";
            insEntidad.NombrePlantillaCodigoDeBarras = "rpxImpresionDeCodigoDeBarras";
            return insEntidad;
        }
        private void LlenaListado(NotaEntradaSalidaStt valRecord, ref List<SettValueByCompany> valBusinessObject, int valConsecutivoCompania) {
            valBusinessObject.Add(ConvierteValor(valRecord.NombrePlantillaNotaEntradaSalida, "NombrePlantillaNotaEntradaSalida", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.NombrePlantillaCodigoDeBarras, "NombrePlantillaCodigoDeBarras", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.ImprimirNotaESconPrecioAsBool), "ImprimirNotaESconPrecio", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.ImprimirReporteAlIngresarNotaEntradaSalidaAsBool), "ImprimirReporteAlIngresarNotaEntradaSalida", valConsecutivoCompania));


        }
        NotaEntradaSalidaStt GetNotaEntradaSalidaStt(List<SettValueByCompany> valListGetSettValueByCompany) {
            NotaEntradaSalidaStt vResult = new NotaEntradaSalidaStt();
            vResult.Module = GetModuleSegunColumna(valListGetSettValueByCompany, "ImprimirReporteAlIngresarNotaEntradaSalida");
            vResult.GroupName = GetGroupNameSegunColumna(valListGetSettValueByCompany, "ImprimirReporteAlIngresarNotaEntradaSalida");
            vResult.ImprimirReporteAlIngresarNotaEntradaSalidaAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "ImprimirReporteAlIngresarNotaEntradaSalida"));
            vResult.NombrePlantillaNotaEntradaSalida = ValorSegunColumna(valListGetSettValueByCompany, "NombrePlantillaNotaEntradaSalida");
            vResult.NombrePlantillaCodigoDeBarras = ValorSegunColumna(valListGetSettValueByCompany, "NombrePlantillaCodigoDeBarras");
            vResult.ImprimirNotaESconPrecioAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "ImprimirNotaESconPrecio"));
            return vResult;
        }
        #endregion // NotaEntradaSalidaStt
        #region MetododecostosStt
        private MetododecostosStt MetododecostosSttPorDefecto() {
            MetododecostosStt insEntidad = new MetododecostosStt();
            return insEntidad;
        }

        private void LlenaListado(MetododecostosStt valRecord, ref List<SettValueByCompany> valBusinessObject, int valConsecutivoCompania) {
            valBusinessObject.Add(ConvierteValor(valRecord.MetodoDeCosteoAsDB, "MetodoDeCosteo", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor((valRecord.FechaDesdeUsoMetodoDeCosteo).ToString("yyyy-MM-dd HH:mm:ss"), "FechaDesdeUsoMetodoDeCosteo", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor((valRecord.FechaContabilizacionDeCosteo).ToString("yyyy-MM-dd HH:mm:ss"), "FechaContabilizacionDeCosteo", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.ComprobanteCostoDetalladoAsBool), "ComprobanteCostoDetallado", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.ComprobanteCostoDetalladoAsBool), "CalculoAutomaticoDeCosto", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.ToStr(valRecord.MaximoGastosAdmisibles), "MaximoGastosAdmisibles", valConsecutivoCompania));
        }
        MetododecostosStt GetMetododecostosStt(List<SettValueByCompany> valListGetSettValueByCompany) {
            MetododecostosStt vResult = new MetododecostosStt();
            vResult.Module = GetModuleSegunColumna(valListGetSettValueByCompany, "MetodoDeCosteo");
            vResult.GroupName = GetGroupNameSegunColumna(valListGetSettValueByCompany, "MetodoDeCosteo");
            vResult.MetodoDeCosteoAsEnum = (Ccl.SttDef.eTipoDeMetodoDeCosteo)LibConvert.DbValueToEnum(ValorSegunColumna(valListGetSettValueByCompany, "MetodoDeCosteo"));
            vResult.MetodoDeCosteoAsEnum = (Ccl.SttDef.eTipoDeMetodoDeCosteo)LibConvert.DbValueToEnum(ValorSegunColumna(valListGetSettValueByCompany, "MetodoDeCosteo"));
            vResult.FechaDesdeUsoMetodoDeCosteo = LibConvert.ToDate(ValorSegunColumna(valListGetSettValueByCompany, "FechaDesdeUsoMetodoDeCosteo"));
            vResult.FechaContabilizacionDeCosteo = LibConvert.ToDate(ValorSegunColumna(valListGetSettValueByCompany, "FechaContabilizacionDeCosteo"));
            vResult.ComprobanteCostoDetalladoAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "ComprobanteCostoDetallado"));
            vResult.CalculoAutomaticoDeCostoAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "CalculoAutomaticoDeCosto"));
            vResult.MaximoGastosAdmisibles = LibConvert.ToDec(ValorSegunColumna(valListGetSettValueByCompany, "MaximoGastosAdmisibles"));
            return vResult;
        }
        #endregion // MetododecostosStt
        #region ComprasStt
        private ComprasStt ComprasSttPorDefecto() {
            ComprasStt insEntidad = new ComprasStt();
            insEntidad.ImprimirOrdenDeCompraAsBool = false;
            insEntidad.IvaEsCostoEnComprasAsBool = false;
            insEntidad.ImprimirCompraAlInsertarAsBool = false;
            insEntidad.NombrePlantillaOrdenDeCompra = "rpxOrdenDeCompraFormatoLibre";
            insEntidad.NombrePlantillaImpresionCodigoBarrasCompras = "rpxImpresionDeCodigoDeBarrasCompras";
            insEntidad.NombrePlantillaCompra = "rpxCompra";
            insEntidad.GenerarCxPdesdeCompraAsBool = false;
            insEntidad.SugerirNumeroDeOrdenDeCompraAsBool = true;
            return insEntidad;
        }
        private void LlenaListado(ComprasStt valRecord, ref List<SettValueByCompany> valBusinessObject, int valConsecutivoCompania) {
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.GenerarCxPdesdeCompraAsBool), "GenerarCxPdesdeCompra", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.ImprimirOrdenDeCompraAsBool), "ImprimirOrdenDeCompra", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.NombrePlantillaOrdenDeCompra, "NombrePlantillaOrdenDeCompra", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.NombrePlantillaImpresionCodigoBarrasCompras, "NombrePlantillaImpresionCodigoBarrasCompras", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.IvaEsCostoEnComprasAsBool), "IvaEsCostoEnCompras", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.ImprimirCompraAlInsertarAsBool), "ImprimirCompraAlInsertar", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.NombrePlantillaCompra, "NombrePlantillaCompra", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.SugerirNumeroDeOrdenDeCompraAsBool), "SugerirNumeroDeOrdenDeCompra", valConsecutivoCompania));
        }
        ComprasStt GetComprasStt(List<SettValueByCompany> valListGetSettValueByCompany) {
            ComprasStt vResult = new ComprasStt();
            vResult.Module = GetModuleSegunColumna(valListGetSettValueByCompany, "GenerarCxPdesdeCompra");
            vResult.GroupName = GetGroupNameSegunColumna(valListGetSettValueByCompany, "GenerarCxPdesdeCompra");
            vResult.GenerarCxPdesdeCompraAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "GenerarCxPdesdeCompra"));
            vResult.ImprimirOrdenDeCompraAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "ImprimirOrdenDeCompra"));
            vResult.NombrePlantillaOrdenDeCompra = ValorSegunColumna(valListGetSettValueByCompany, "NombrePlantillaOrdenDeCompra");
            vResult.NombrePlantillaImpresionCodigoBarrasCompras = ValorSegunColumna(valListGetSettValueByCompany, "NombrePlantillaImpresionCodigoBarrasCompras");
            vResult.IvaEsCostoEnComprasAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "IvaEsCostoEnCompras"));
            vResult.ImprimirCompraAlInsertarAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "ImprimirCompraAlInsertar"));
            vResult.NombrePlantillaCompra = ValorSegunColumna(valListGetSettValueByCompany, "NombrePlantillaCompra");
            vResult.SugerirNumeroDeOrdenDeCompraAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "SugerirNumeroDeOrdenDeCompra"));
            return vResult;
        }
        #endregion // ComprasStt
        #region CxPCompras
        bool ISettValueByCompanyPdn.ExistenCxPGeneradasDesdeCompra(int valConsecutivoCompania) {
            bool vResult = false;
            StringBuilder vSql = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            XElement CxPGeneradasDesdeCompra;
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vSql.AppendLine("   SELECT COUNT(Consecutivo) AS CantidadDeCxPGeneradasDesdeCompras");
            vSql.AppendLine("   FROM Adm.Compra");
            vSql.AppendLine("   WHERE GenerarCXP = 'S'");
            vSql.AppendLine("   AND ConsecutivoCompania = @ConsecutivoCompania");
            CxPGeneradasDesdeCompra = LibBusiness.ExecuteSelect(vSql.ToString(), vParams.Get(), string.Empty, 0);
            if (CxPGeneradasDesdeCompra != null) {
                int vCantidadDeCxPGeneradasDesdeCompras = CxPGeneradasDesdeCompra.Descendants().Select(s => (int)s.Element("CantidadDeCxPGeneradasDesdeCompras")).FirstOrDefault();
                if (vCantidadDeCxPGeneradasDesdeCompras > 0) {
                    vResult = true;
                }
            }
            return vResult;
        }
        #endregion
        #region CxPProveedorPagosStt
        private CxPProveedorPagosStt CxPProveedorPagosSttPorDefecto() {
            CxPProveedorPagosStt insEntidad = new CxPProveedorPagosStt();
            insEntidad.LongitudCodigoProveedor = ((ISettValueByCompanyPdn)this).DefaultLongitudCodigoProveedor();
            insEntidad.NumCopiasComprobantepago = 2;
            insEntidad.ConceptoBancarioReversoDePago = GetConceptoBancario("REVERSO_AUTOMATICO_PAGO");
            insEntidad.TipoDeOrdenDePagoAImprimirAsEnum = eTipoDeOrdenDePagoAImprimir.OrdendePagoconCheque;
            insEntidad.NoImprimirComprobanteDePagoAsBool = false;
            insEntidad.ImprimirComprobanteContableDePagoAsBool = false;
            insEntidad.NombrePlantillaComprobanteDePago = "rpxComprobanteDePago";
            //insEntidad.NombrePlantillaRetencionImpuestoMunicipal = "rpxComprobanteDeRetencionDeActividadesEconomicas";
            insEntidad.ExigirInformacionLibroDeComprasAsBool = true;
            insEntidad.AvisarSiProveedorTieneAnticiposAsBool = false;
            insEntidad.ConfirmarImpresionPorSeccionesAsBool = true;
            insEntidad.PrimerNumeroComprobanteRetImpuestoMunicipal = 0;
            insEntidad.FechaSugeridaRetencionesCxPAsEnum = eFechaSugeridaRetencionesCxP.FechaFacturaCxP;
            insEntidad.NombrePlantillaRetencionImpuestoMunicipalInforme = "rpxResumenRetencionDeActividadesEconomicas";
            return insEntidad;
        }

        private void LlenaListado(CxPProveedorPagosStt valRecord, ref List<SettValueByCompany> valBusinessObject, int valConsecutivoCompania) {
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.ExigirInformacionLibroDeComprasAsBool), "ExigirInformacionLibroDeCompras", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.UsarCodigoProveedorEnPantallaAsBool), "UsarCodigoProveedorEnPantalla", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.ToStr(valRecord.LongitudCodigoProveedor), "LongitudCodigoProveedor", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.ToStr(valRecord.NumCopiasComprobantepago), "NumCopiasComprobantepago", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.NombrePlantillaComprobanteDePago, "NombrePlantillaComprobanteDePago", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.TipoDeOrdenDePagoAImprimirAsDB, "TipoDeOrdenDePagoAImprimir", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.ConfirmarImpresionPorSeccionesAsBool), "ConfirmarImpresionPorSecciones", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.NoImprimirComprobanteDePagoAsBool), "NoImprimirComprobanteDePago", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.ImprimirComprobanteContableDePagoAsBool), "ImprimirComprobanteContableDePago", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.ConceptoBancarioReversoDePago, "ConceptoBancarioReversoDePago", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.AvisarSiProveedorTieneAnticiposAsBool), "AvisarSiProveedorTieneAnticipos", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.OrdenarCxPPorFacturaDocumentoAsBool), "OrdenarCxPPorFacturaDocumento", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.RetieneImpuestoMunicipalAsBool), "RetieneImpuestoMunicipal", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.ToStr(valRecord.PrimerNumeroComprobanteRetImpuestoMunicipal), "PrimerNumeroComprobanteRetImpuestoMunicipal", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.NombrePlantillaComprobanteDePago, "NombrePlantillaRetencionImpuestoMunicipal", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.NombrePlantillaRetencionImpuestoMunicipalInforme, "NombrePlantillaRetencionImpuestoMunicipalInforme", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.FechaSugeridaRetencionesCxPAsDB, "FechaSugeridaRetencionesCxP", valConsecutivoCompania));
        }

        CxPProveedorPagosStt GetCxPProveedorPagosStt(List<SettValueByCompany> valListGetSettValueByCompany) {
            CxPProveedorPagosStt vResult = new CxPProveedorPagosStt();
            vResult.Module = GetModuleSegunColumna(valListGetSettValueByCompany, "ExigirInformacionLibroDeCompras");
            vResult.GroupName = GetGroupNameSegunColumna(valListGetSettValueByCompany, "ExigirInformacionLibroDeCompras");
            vResult.ExigirInformacionLibroDeComprasAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "ExigirInformacionLibroDeCompras"));
            vResult.UsarCodigoProveedorEnPantallaAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "UsarCodigoProveedorEnPantalla"));
            vResult.LongitudCodigoProveedor = LibConvert.ToInt(ValorSegunColumna(valListGetSettValueByCompany, "LongitudCodigoProveedor"));
            vResult.NumCopiasComprobantepago = LibConvert.ToInt(ValorSegunColumna(valListGetSettValueByCompany, "NumCopiasComprobantepago"));
            vResult.NombrePlantillaComprobanteDePago = ValorSegunColumna(valListGetSettValueByCompany, "NombrePlantillaComprobanteDePago");
            vResult.TipoDeOrdenDePagoAImprimirAsEnum = (eTipoDeOrdenDePagoAImprimir)LibConvert.DbValueToEnum(ValorSegunColumna(valListGetSettValueByCompany, "TipoDeOrdenDePagoAImprimir"));
            vResult.ConfirmarImpresionPorSeccionesAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "ConfirmarImpresionPorSecciones"));
            vResult.NoImprimirComprobanteDePagoAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "NoImprimirComprobanteDePago"));
            vResult.ImprimirComprobanteContableDePagoAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "ImprimirComprobanteContableDePago"));
            vResult.ConceptoBancarioReversoDePago = ValorSegunColumna(valListGetSettValueByCompany, "ConceptoBancarioReversoDePago");
            vResult.AvisarSiProveedorTieneAnticiposAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "AvisarSiProveedorTieneAnticipos"));
            vResult.OrdenarCxPPorFacturaDocumentoAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "OrdenarCxPPorFacturaDocumento"));
            vResult.NombrePlantillaRetencionImpuestoMunicipal = ValorSegunColumna(valListGetSettValueByCompany, "NombrePlantillaRetencionImpuestoMunicipal");
            vResult.RetieneImpuestoMunicipalAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "RetieneImpuestoMunicipal"));
            vResult.PrimerNumeroComprobanteRetImpuestoMunicipal = LibConvert.ToInt(ValorSegunColumna(valListGetSettValueByCompany, "PrimerNumeroComprobanteRetImpuestoMunicipal"));
            vResult.NombrePlantillaRetencionImpuestoMunicipalInforme = ValorSegunColumna(valListGetSettValueByCompany, "NombrePlantillaRetencionImpuestoMunicipalInforme");
            vResult.FechaSugeridaRetencionesCxPAsEnum = (eFechaSugeridaRetencionesCxP)LibConvert.DbValueToEnum(ValorSegunColumna(valListGetSettValueByCompany, "FechaSugeridaRetencionesCxP"));
            return vResult;
        }
        #endregion //  CxPProveedorPagosStt
        #region RetencionIVAStt
        private RetencionIVAStt RetencionIVAStttPorDefecto() {
            RetencionIVAStt insEntidad = new RetencionIVAStt();
            insEntidad.NumeroDeCopiasComprobanteRetencionIVA = 1;
            insEntidad.EnDondeRetenerIVAAsEnum = eDondeSeEfectuaLaRetencionIVA.NoRetenida;
            insEntidad.PrimerNumeroComprobanteRetIVA = 0;
            insEntidad.FormaDeReiniciarElNumeroDeComprobanteRetIVAAsEnum = eFormaDeReiniciarComprobanteRetIVA.AlCompletar;
            insEntidad.ImprimirComprobanteDeRetIVAAsBool = true;
            insEntidad.NombrePlantillaComprobanteDeRetIVA = "rpxComprobanteDeRetencionIVA";
            insEntidad.GenerarNumCompDeRetIVASoloSiPorcentajeEsMayorACeroAsBool = true;
            insEntidad.UnComprobanteDeRetIVAPorHojaAsBool = true;
            return insEntidad;
        }
        private void LlenaListado(RetencionIVAStt valRecord, ref List<SettValueByCompany> valBusinessObject, int valConsecutivoCompania) {
            valBusinessObject.Add(ConvierteValor(valRecord.EnDondeRetenerIVAAsDB, "EnDondeRetenerIVA", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.UsaMismoNumeroCompRetTodasCxPAsBool), "UsaMismoNumeroCompRetTodasCxP", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.ToStr(valRecord.PrimerNumeroComprobanteRetIVA), "PrimerNumeroComprobanteRetIVA", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.FormaDeReiniciarElNumeroDeComprobanteRetIVAAsDB, "FormaDeReiniciarElNumeroDeComprobanteRetIVA", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.ImprimirComprobanteDeRetIVAAsBool), "ImprimirComprobanteDeRetIVA", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.ToStr(valRecord.NumeroDeCopiasComprobanteRetencionIVA), "NumeroDeCopiasComprobanteRetencionIVA", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.UnComprobanteDeRetIVAPorHojaAsBool), "UnComprobanteDeRetIVAPorHoja", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.NombrePlantillaComprobanteDeRetIVA, "NombrePlantillaComprobanteDeRetIVA", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.GenerarNumCompDeRetIVASoloSiPorcentajeEsMayorACeroAsBool), "GenerarNumCompDeRetIVASoloSiPorcentajeEsMayorACero", valConsecutivoCompania));
        }
        RetencionIVAStt GetRetencionIVAStt(List<SettValueByCompany> valListGetSettValueByCompany) {
            RetencionIVAStt vResult = new RetencionIVAStt();
            vResult.Module = GetModuleSegunColumna(valListGetSettValueByCompany, "EnDondeRetenerIVA");
            vResult.GroupName = GetGroupNameSegunColumna(valListGetSettValueByCompany, "EnDondeRetenerIVA");
            if (LibDefGen.ProgramInfo.IsCountryPeru()) {
                vResult.GroupName = "6.3.- Retención IGV";
            }
            vResult.EnDondeRetenerIVAAsEnum = (eDondeSeEfectuaLaRetencionIVA)LibConvert.DbValueToEnum(ValorSegunColumna(valListGetSettValueByCompany, "EnDondeRetenerIVA"));
            vResult.UsaMismoNumeroCompRetTodasCxPAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "UsaMismoNumeroCompRetTodasCxP"));
            vResult.PrimerNumeroComprobanteRetIVA = LibConvert.ToInt(ValorSegunColumna(valListGetSettValueByCompany, "PrimerNumeroComprobanteRetIVA"));
            vResult.FormaDeReiniciarElNumeroDeComprobanteRetIVAAsEnum = (eFormaDeReiniciarComprobanteRetIVA)LibConvert.DbValueToEnum(ValorSegunColumna(valListGetSettValueByCompany, "FormaDeReiniciarElNumeroDeComprobanteRetIVA"));
            vResult.ImprimirComprobanteDeRetIVAAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "ImprimirComprobanteDeRetIVA"));
            vResult.NumeroDeCopiasComprobanteRetencionIVA = LibConvert.ToInt(ValorSegunColumna(valListGetSettValueByCompany, "NumeroDeCopiasComprobanteRetencionIVA"));
            vResult.UnComprobanteDeRetIVAPorHojaAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "UnComprobanteDeRetIVAPorHoja"));
            vResult.NombrePlantillaComprobanteDeRetIVA = ValorSegunColumna(valListGetSettValueByCompany, "NombrePlantillaComprobanteDeRetIVA");
            vResult.GenerarNumCompDeRetIVASoloSiPorcentajeEsMayorACeroAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "GenerarNumCompDeRetIVASoloSiPorcentajeEsMayorACero"));
            return vResult;
        }
        #endregion //   RetencionIVAStt
        #region RetencionISLRStt
        private RetencionISLRStt RetencionISLRSttPorDefecto(string valCiudad) {
            RetencionISLRStt insEntidad = new RetencionISLRStt();
            insEntidad.NumCopiasComprobanteRetencion = 2;
            insEntidad.DiaDelCierreFiscal = "31";
            insEntidad.MesDelCierreFiscal = "12";
            insEntidad.UsaRetencionAsBool = false;
            insEntidad.TomarEnCuentaRetencionesCeroParaARCVyRAAsBool = true;
            insEntidad.EnDondeRetenerISLRAsEnum = eDondeSeEfectuaLaRetencionISLR.NoRetenida;
            insEntidad.CiudadRepLegal = valCiudad;
            insEntidad.NombrePlantillaComprobanteDeRetISRL = "rpxComprobanteDeRetencion";
            return insEntidad;
        }
        private void LlenaListado(RetencionISLRStt valRecord, ref List<SettValueByCompany> valBusinessObject, int valConsecutivoCompania) {
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.UsaRetencionAsBool), "UsaRetencion", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.ToStr(valRecord.NumCopiasComprobanteRetencion), "NumCopiasComprobanteRetencion", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.DiaDelCierreFiscal, "DiaDelCierreFiscal", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.MesDelCierreFiscal, "MesDelCierreFiscal", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.TomarEnCuentaRetencionesCeroParaARCVyRAAsBool), "TomarEnCuentaRetencionesCeroParaARCVyRA", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.EnDondeRetenerISLRAsDB, "EnDondeRetenerISLR", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.NumeroRIFR, "NumeroRIFR", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.NombreYApellidoR, "NombreYApellidoR", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.CodTelfR, "CodTelfR", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.TelefonoR, "TelefonoR", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.DireccionR, "DireccionR", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.CiudadRepLegal, "CiudadRepLegal", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.CorreoElectronicoRepLegal, "CorreoElectronicoRepLegal", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.NombrePlantillaComprobanteDeRetISRL, "NombrePlantillaComprobanteDeRetISRL", valConsecutivoCompania));

        }
        RetencionISLRStt GetRetencionISLRSStt(List<SettValueByCompany> valListGetSettValueByCompany) {
            RetencionISLRStt vResult = new RetencionISLRStt();
            vResult.Module = GetModuleSegunColumna(valListGetSettValueByCompany, "UsaRetencion");
            vResult.GroupName = GetGroupNameSegunColumna(valListGetSettValueByCompany, "UsaRetencion");
            vResult.UsaRetencionAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "UsaRetencion"));
            vResult.NumCopiasComprobanteRetencion = LibConvert.ToInt(ValorSegunColumna(valListGetSettValueByCompany, "NumCopiasComprobanteRetencion"));
            vResult.DiaDelCierreFiscal = ValorSegunColumna(valListGetSettValueByCompany, "DiaDelCierreFiscal");
            vResult.MesDelCierreFiscal = ValorSegunColumna(valListGetSettValueByCompany, "MesDelCierreFiscal");
            vResult.TomarEnCuentaRetencionesCeroParaARCVyRAAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "TomarEnCuentaRetencionesCeroParaARCVyRA"));
            vResult.EnDondeRetenerISLRAsEnum = (eDondeSeEfectuaLaRetencionISLR)LibConvert.DbValueToEnum(ValorSegunColumna(valListGetSettValueByCompany, "EnDondeRetenerISLR"));
            vResult.NumeroRIFR = ValorSegunColumna(valListGetSettValueByCompany, "NumeroRIFR");
            vResult.NombreYApellidoR = ValorSegunColumna(valListGetSettValueByCompany, "NombreYApellidoR");
            vResult.CodTelfR = ValorSegunColumna(valListGetSettValueByCompany, "CodTelfR");
            vResult.TelefonoR = ValorSegunColumna(valListGetSettValueByCompany, "TelefonoR");
            vResult.DireccionR = ValorSegunColumna(valListGetSettValueByCompany, "DireccionR");
            vResult.CiudadRepLegal = ValorSegunColumna(valListGetSettValueByCompany, "CiudadRepLegal");
            vResult.CorreoElectronicoRepLegal = ValorSegunColumna(valListGetSettValueByCompany, "CorreoElectronicoRepLegal");
            vResult.NombrePlantillaComprobanteDeRetISRL = ValorSegunColumna(valListGetSettValueByCompany, "NombrePlantillaComprobanteDeRetISRL");
            return vResult;
        }

        #endregion //  RetencionISLRStt
        #region PlanillaDeIVAStt
        private PlanillaDeIVAStt PlanillaDeIVASttPorDefecto() {
            PlanillaDeIVAStt insEntidad = new PlanillaDeIVAStt();
            insEntidad.ImprimirCentimosEnPlanillaAsBool = false;
            insEntidad.ModeloPlanillaForma00030AsEnum = eModeloPlanillaForma00030.F00030_F04_Grafibond;
            return insEntidad;
        }
        private void LlenaListado(PlanillaDeIVAStt valRecord, ref List<SettValueByCompany> valBusinessObject, int valConsecutivoCompania) {
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.ImprimirCentimosEnPlanillaAsBool), "ImprimirCentimosEnPlanilla", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.ModeloPlanillaForma00030AsDB, "ModeloPlanillaForma00030", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.NombreContador, "NombreContador", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.CedulaContador, "CedulaContador", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.CedulaContador, "NumeroCPC", valConsecutivoCompania));
        }

        PlanillaDeIVAStt GetPlanillaDeIVAStt(List<SettValueByCompany> valListGetSettValueByCompany) {
            PlanillaDeIVAStt vResult = new PlanillaDeIVAStt();
            vResult.Module = GetModuleSegunColumna(valListGetSettValueByCompany, "ImprimirCentimosEnPlanilla");
            vResult.GroupName = GetGroupNameSegunColumna(valListGetSettValueByCompany, "ImprimirCentimosEnPlanilla");
            vResult.ImprimirCentimosEnPlanillaAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "ImprimirCentimosEnPlanilla"));
            vResult.ModeloPlanillaForma00030AsEnum = (eModeloPlanillaForma00030)LibConvert.DbValueToEnum(ValorSegunColumna(valListGetSettValueByCompany, "ModeloPlanillaForma00030"));
            vResult.NombreContador = ValorSegunColumna(valListGetSettValueByCompany, "NombreContador");
            vResult.CedulaContador = ValorSegunColumna(valListGetSettValueByCompany, "CedulaContador");
            vResult.NumeroCPC = ValorSegunColumna(valListGetSettValueByCompany, "NumeroCPC");
            return vResult;
        }

        #endregion //  ImagenesComprobantesRetencionStt
        #region  ImagenesComprobantesRetencionStt

        private ImagenesComprobantesRetencionStt ImagenesComprobantesRetencionSttPorDefecto() {
            ImagenesComprobantesRetencionStt insEntidad = new ImagenesComprobantesRetencionStt();
            insEntidad.NombreFirma = string.Empty;
            insEntidad.NombreLogo = string.Empty;
            insEntidad.NombreSello = string.Empty;
            return insEntidad;
        }

        private void LlenaListado(ImagenesComprobantesRetencionStt valRecord, ref List<SettValueByCompany> valBusinessObject, int valConsecutivoCompania) {
            valBusinessObject.Add(ConvierteValor(valRecord.NombreSello, "NombreSello", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.NombreLogo, "NombreLogo", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.NombreFirma, "NombreFirma", valConsecutivoCompania));
        }

        private ImagenesComprobantesRetencionStt GetImagenesComprobantesRetencionStt(List<SettValueByCompany> valListGetSettValueByCompany) {
            ImagenesComprobantesRetencionStt vResult = new ImagenesComprobantesRetencionStt();
            vResult.Module = GetModuleSegunColumna(valListGetSettValueByCompany, "NombreFirma");
            vResult.GroupName = GetGroupNameSegunColumna(valListGetSettValueByCompany, "NombreFirma");
            vResult.NombreFirma = ValorSegunColumna(valListGetSettValueByCompany, "NombreFirma");
            vResult.NombreLogo = ValorSegunColumna(valListGetSettValueByCompany, "NombreLogo");
            vResult.NombreSello = ValorSegunColumna(valListGetSettValueByCompany, "NombreSello");
            return vResult;
        }

        #endregion  ImagenesComprobantesRetencionStt
        #region BancosStt
        private BancosStt BancosSttPorDefecto() {
            BancosStt insEntidad = new BancosStt();
            insEntidad.CodigoGenericoCuentaBancaria = GetCuentaBancariaPorDefecto();
            insEntidad.ConceptoDebitoBancario = GetConceptoBancario("DEBITO_BANCARIO_AUTOMATICO");
            insEntidad.ConceptoCreditoBancario = GetConceptoBancario("DEBITO_BANCARIO_AUTOMATICO");
            insEntidad.ConsideraConciliadosLosMovIngresadosAntesDeFechaAsBool = false;
            insEntidad.ManejaDebitoBancarioAsBool = false;
            insEntidad.RedondeaMontoDebitoBancarioAsBool = false;
            insEntidad.ManejaCreditoBancarioAsBool = false;
            insEntidad.RedondeaMontoCreditoBancarioAsBool = false;
            if (LibGalac.Aos.DefGen.LibDefGen.IsDemoProgram) {
                insEntidad.FechaDeInicioConciliacion = LibGalac.Aos.DefGen.LibDefGen.DateLimitForEnterData;
            } else {
                insEntidad.FechaDeInicioConciliacion = LibDate.Today();
            }
            return insEntidad;
        }
        private void LlenaListado(BancosStt valRecord, ref List<SettValueByCompany> valBusinessObject, int valConsecutivoCompania) {
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.UsaCodigoBancoEnPantallaAsBool), "UsaCodigoBancoEnPantalla", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.CodigoGenericoCuentaBancaria, "CodigoGenericoCuentaBancaria", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.ManejaDebitoBancarioAsBool), "ManejaDebitoBancario", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.RedondeaMontoDebitoBancarioAsBool), "RedondeaMontoDebitoBancario", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.ConceptoDebitoBancario, "ConceptoDebitoBancario", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.ConsideraConciliadosLosMovIngresadosAntesDeFechaAsBool), "ConsideraConciliadosLosMovIngresadosAntesDeFecha", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor((valRecord.FechaDeInicioConciliacion).ToString("yyyy-MM-dd HH:mm:ss"), "FechaDeInicioConciliacion", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.ManejaCreditoBancarioAsBool), "ManejaCreditoBancario", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.RedondeaMontoCreditoBancarioAsBool), "RedondeaMontoCreditoBancario", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.ConceptoCreditoBancario, "ConceptoCreditoBancario", valConsecutivoCompania));
        }

        BancosStt GetBancosStt(List<SettValueByCompany> valListGetSettValueByCompany) {
            BancosStt vResult = new BancosStt();
            vResult.Module = GetModuleSegunColumna(valListGetSettValueByCompany, "UsaCodigoBancoEnPantalla");
            vResult.GroupName = GetGroupNameSegunColumna(valListGetSettValueByCompany, "UsaCodigoBancoEnPantalla");
            vResult.UsaCodigoBancoEnPantallaAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "UsaCodigoBancoEnPantalla"));
            vResult.CodigoGenericoCuentaBancaria = ValorSegunColumna(valListGetSettValueByCompany, "CodigoGenericoCuentaBancaria");
            vResult.ManejaDebitoBancarioAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "ManejaDebitoBancario"));
            vResult.RedondeaMontoDebitoBancarioAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "RedondeaMontoDebitoBancario"));
            vResult.ConceptoDebitoBancario = ValorSegunColumna(valListGetSettValueByCompany, "ConceptoDebitoBancario");
            vResult.ConsideraConciliadosLosMovIngresadosAntesDeFechaAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "ConsideraConciliadosLosMovIngresadosAntesDeFecha"));
            vResult.FechaDeInicioConciliacion = LibConvert.ToDate(ValorSegunColumna(valListGetSettValueByCompany, "FechaDeInicioConciliacion"));
            vResult.ManejaCreditoBancarioAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "ManejaCreditoBancario"));
            vResult.RedondeaMontoCreditoBancarioAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "RedondeaMontoCreditoBancario"));
            vResult.ConceptoCreditoBancario = ValorSegunColumna(valListGetSettValueByCompany, "ConceptoCreditoBancario");
            return vResult;
        }
        #endregion //  BancosStt
        #region MonedaStt
        private MonedaStt MonedaSttPorDefecto(string valCodigoMonedaLocal, string valNombreMonedaLocal, string valCodigoMonedaExtranjera, string valNombreMonedaExtranjera) {
            MonedaStt insEntidad = new MonedaStt();
            insEntidad.CodigoMonedaLocal = valCodigoMonedaLocal;
            insEntidad.NombreMonedaLocal = valNombreMonedaLocal;
            insEntidad.CodigoMonedaExtranjera = valCodigoMonedaExtranjera;
            insEntidad.NombreMonedaExtranjera = valNombreMonedaExtranjera;
            insEntidad.UsaDivisaComoMonedaPrincipalDeIngresoDeDatosAsBool = false;
            insEntidad.UsarLimiteMaximoParaIngresoDeTasaDeCambio = false;
            insEntidad.MaximoLimitePermitidoParaLaTasaDeCambio = 30m;
            insEntidad.ObtenerAutomaticamenteTasaDeCambioDelBCV = false;
            return insEntidad;
        }

        private void LlenaListado(MonedaStt valRecord, ref List<SettValueByCompany> valBusinessObject, int valConsecutivoCompania) {
            valBusinessObject.Add(ConvierteValor(valRecord.CodigoMonedaLocal, "CodigoMonedaLocal", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.NombreMonedaLocal, "NombreMonedaLocal", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.UsaMonedaExtranjeraAsBool), "UsaMonedaExtranjera", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.CodigoMonedaExtranjera, "CodigoMonedaExtranjera", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.NombreMonedaExtranjera, "NombreMonedaExtranjera", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.UsaDivisaComoMonedaPrincipalDeIngresoDeDatosAsBool), "UsaDivisaComoMonedaPrincipalDeIngresoDeDatos", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.UsarLimiteMaximoParaIngresoDeTasaDeCambio), "UsarLimiteMaximoParaIngresoDeTasaDeCambio", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.ToStr(valRecord.MaximoLimitePermitidoParaLaTasaDeCambio), "MaximoLimitePermitidoParaLaTasaDeCambio", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.ObtenerAutomaticamenteTasaDeCambioDelBCV), "ObtenerAutomaticamenteTasaDeCambioDelBCV", valConsecutivoCompania));
        }

        MonedaStt GetMonedaStt(List<SettValueByCompany> valListGetSettValueByCompany) {
            MonedaStt vResult = new MonedaStt();
            vResult.Module = GetModuleSegunColumna(valListGetSettValueByCompany, "CodigoMonedaLocal");
            vResult.GroupName = GetGroupNameSegunColumna(valListGetSettValueByCompany, "CodigoMonedaLocal");
            vResult.CodigoMonedaLocal = ValorSegunColumna(valListGetSettValueByCompany, "CodigoMonedaLocal");
            vResult.NombreMonedaLocal = ValorSegunColumna(valListGetSettValueByCompany, "NombreMonedaLocal");
            vResult.UsaMonedaExtranjeraAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "UsaMonedaExtranjera"));
            vResult.CodigoMonedaExtranjera = ValorSegunColumna(valListGetSettValueByCompany, "CodigoMonedaExtranjera");
            vResult.NombreMonedaExtranjera = ValorSegunColumna(valListGetSettValueByCompany, "NombreMonedaExtranjera");
            vResult.UsaDivisaComoMonedaPrincipalDeIngresoDeDatosAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "UsaDivisaComoMonedaPrincipalDeIngresoDeDatos"));
            vResult.UsarLimiteMaximoParaIngresoDeTasaDeCambio = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "UsarLimiteMaximoParaIngresoDeTasaDeCambio"));
            vResult.MaximoLimitePermitidoParaLaTasaDeCambio = LibImportData.ToDec(ValorSegunColumna(valListGetSettValueByCompany, "MaximoLimitePermitidoParaLaTasaDeCambio"));
            vResult.ObtenerAutomaticamenteTasaDeCambioDelBCV = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "ObtenerAutomaticamenteTasaDeCambioDelBCV"));
            return vResult;
        }
        #endregion // MonedaStt
        #region AnticipoStt
        private AnticipoStt AnticipoSttPorDefecto() {
            AnticipoStt insEntidad = new AnticipoStt();
            insEntidad.CuentaBancariaAnticipo = GetCuentaBancariaPorDefecto();
            insEntidad.ConceptoBancarioAnticipoCobrado = GetConceptoBancario("ANTICIPO_COBRADO");
            insEntidad.ConceptoBancarioReversoAnticipoCobrado = GetConceptoBancario("REV_AUTOMATICO_ANT_COBRADO");
            insEntidad.ConceptoBancarioReversoAnticipoPagado = GetConceptoBancario("REV_AUTOMATICO_ANT_PAGADO");
            insEntidad.ConceptoBancarioAnticipoPagado = GetConceptoBancario("ANTICIPO_PAGADO");
            insEntidad.TipoComprobanteDeAnticipoAImprimirAsEnum = eComprobanteConCheque.ComprobanteconCheque;
            insEntidad.SugerirConsecutivoAnticipoAsBool = true;
            insEntidad.NombrePlantillaReciboDeAnticipoCobrado = "rpxComprobanteDeAnticipoCobrado";
            insEntidad.NombrePlantillaReciboDeAnticipoPagado = "rpxComprobanteDeAnticipoPagado";
            return insEntidad;
        }


        private void LlenaListado(AnticipoStt valRecord, ref List<SettValueByCompany> valBusinessObject, int valConsecutivoCompania) {
            valBusinessObject.Add(ConvierteValor(valRecord.CuentaBancariaAnticipo, "CuentaBancariaAnticipo", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.SugerirConsecutivoAnticipoAsBool), "SugerirConsecutivoAnticipo", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.ConceptoBancarioAnticipoCobrado, "ConceptoBancarioAnticipoCobrado", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.ConceptoBancarioReversoAnticipoCobrado, "ConceptoBancarioReversoAnticipoCobrado", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.ConceptoBancarioReversoAnticipoCobrado, "ConceptoBancarioReversoAnticipoCobrado", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.ConceptoBancarioReversoAnticipoCobrado, "ConceptoBancarioReversoAnticipoCobrado", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.NombrePlantillaReciboDeAnticipoCobrado, "NombrePlantillaReciboDeAnticipoCobrado", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.NombrePlantillaReciboDeAnticipoPagado, "NombrePlantillaReciboDeAnticipoPagado", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.ConceptoBancarioReversoAnticipoPagado, "ConceptoBancarioReversoAnticipoPagado", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.TipoComprobanteDeAnticipoAImprimirAsDB, "TipoComprobanteDeAnticipoAImprimir", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.ConceptoBancarioAnticipoPagado, "ConceptoBancarioAnticipoPagado", valConsecutivoCompania));

        }

        AnticipoStt GetAnticipoStt(List<SettValueByCompany> valListGetSettValueByCompany) {
            AnticipoStt vResult = new AnticipoStt();
            vResult.Module = GetModuleSegunColumna(valListGetSettValueByCompany, "CuentaBancariaAnticipo");
            vResult.GroupName = GetGroupNameSegunColumna(valListGetSettValueByCompany, "CuentaBancariaAnticipo");
            vResult.CuentaBancariaAnticipo = ValorSegunColumna(valListGetSettValueByCompany, "CuentaBancariaAnticipo");
            vResult.SugerirConsecutivoAnticipoAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "SugerirConsecutivoAnticipo"));
            vResult.ConceptoBancarioAnticipoCobrado = ValorSegunColumna(valListGetSettValueByCompany, "ConceptoBancarioAnticipoCobrado");
            vResult.ConceptoBancarioReversoAnticipoCobrado = ValorSegunColumna(valListGetSettValueByCompany, "ConceptoBancarioReversoAnticipoCobrado");
            vResult.NombrePlantillaReciboDeAnticipoCobrado = ValorSegunColumna(valListGetSettValueByCompany, "NombrePlantillaReciboDeAnticipoCobrado");
            vResult.NombrePlantillaReciboDeAnticipoPagado = ValorSegunColumna(valListGetSettValueByCompany, "NombrePlantillaReciboDeAnticipoPagado");
            vResult.ConceptoBancarioReversoAnticipoPagado = ValorSegunColumna(valListGetSettValueByCompany, "ConceptoBancarioReversoAnticipoPagado");
            vResult.TipoComprobanteDeAnticipoAImprimirAsEnum = (eComprobanteConCheque)LibConvert.DbValueToEnum(ValorSegunColumna(valListGetSettValueByCompany, "TipoComprobanteDeAnticipoAImprimir"));
            vResult.ConceptoBancarioAnticipoPagado = ValorSegunColumna(valListGetSettValueByCompany, "ConceptoBancarioAnticipoPagado");
            return vResult;
        }
        #endregion // AnticipoStt
        #region MovimientoBancarioStt
        private MovimientoBancarioStt MovimientoBancarioSttPorDefecto(int valConsecutivoCompania) {
            MovimientoBancarioStt insEntidad = new MovimientoBancarioStt();
            insEntidad.NumCopiasComprobanteMovBancario = 2;
            insEntidad.BeneficiarioGenerico = GetConsecutivoBeneficiarioPorDefecto(valConsecutivoCompania);
            insEntidad.ConceptoBancarioReversoSolicitudDePago = GetConceptoBancario("REV_AUTOMATICO_SOLI_PAGADO");
            insEntidad.ConfirmarImpresionMovBancarioPorSeccionesAsBool = true;
            insEntidad.ImprimirComprobanteDeMovBancarioAsBool = false;
            insEntidad.ImprimirCompContDespuesDeChequeMovBancarioAsBool = false;
            insEntidad.NombrePlantillaComprobanteDeMovBancario = "rpxComprobanteDeImpresionDeCheque";
            insEntidad.NombrePlantillaComprobanteDePagoSueldo = "rpxComprobanteDePagoSueldos";
            insEntidad.GenerarMovBancarioDesdeCobroAsBool = true;
            insEntidad.GenerarMovBancarioDesdePagoAsBool = true;
            return insEntidad;
        }

        private void LlenaListado(MovimientoBancarioStt valRecord, ref List<SettValueByCompany> valBusinessObject, int valConsecutivoCompania) {
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.MandarMensajeNumeroDeMovimientoBancarioAsBool), "MandarMensajeNumeroDeMovimientoBancario", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.GenerarMovBancarioDesdeCobroAsBool), "GenerarMovBancarioDesdeCobro", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.UsaCodigoConceptoBancarioEnPantallaAsBool), "UsaCodigoConceptoBancarioEnPantalla", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.GenerarMovBancarioDesdePagoAsBool), "GenerarMovBancarioDesdePago", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.ToStr(valRecord.NumCopiasComprobanteMovBancario), "NumCopiasComprobanteMovBancario", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.NombrePlantillaComprobanteDeMovBancario, "NombrePlantillaComprobanteDeMovBancario", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.ConfirmarImpresionMovBancarioPorSeccionesAsBool), "ConfirmarImpresionMovBancarioPorSecciones", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.ImprimirCompContDespuesDeChequeMovBancarioAsBool), "ImprimirCompContDespuesDeChequeMovBancario", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.ImprimirComprobanteDeMovBancarioAsBool), "ImprimirComprobanteDeMovBancario", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.ToStr(valRecord.BeneficiarioGenerico), "BeneficiarioGenerico", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.ConceptoBancarioReversoSolicitudDePago, "ConceptoBancarioReversoSolicitudDePago", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.NombrePlantillaComprobanteDePagoSueldo, "NombrePlantillaComprobanteDePagoSueldo", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.GenerarMovReversoSiAnulaPagoAsBool), "GenerarMovReversoSiAnulaPago", valConsecutivoCompania));

        }
        MovimientoBancarioStt GetMovimientoBancarioStt(List<SettValueByCompany> valListGetSettValueByCompany) {
            MovimientoBancarioStt vResult = new MovimientoBancarioStt();
            vResult.Module = GetModuleSegunColumna(valListGetSettValueByCompany, "MandarMensajeNumeroDeMovimientoBancario");
            vResult.GroupName = GetGroupNameSegunColumna(valListGetSettValueByCompany, "MandarMensajeNumeroDeMovimientoBancario");
            vResult.MandarMensajeNumeroDeMovimientoBancarioAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "MandarMensajeNumeroDeMovimientoBancario"));
            vResult.GenerarMovBancarioDesdeCobroAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "GenerarMovBancarioDesdeCobro"));
            vResult.UsaCodigoConceptoBancarioEnPantallaAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "UsaCodigoConceptoBancarioEnPantalla"));
            vResult.GenerarMovBancarioDesdePagoAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "GenerarMovBancarioDesdePago"));
            vResult.NumCopiasComprobanteMovBancario = LibConvert.ToInt(ValorSegunColumna(valListGetSettValueByCompany, "NumCopiasComprobanteMovBancario"));
            vResult.NombrePlantillaComprobanteDeMovBancario = ValorSegunColumna(valListGetSettValueByCompany, "NombrePlantillaComprobanteDeMovBancario");
            vResult.ConfirmarImpresionMovBancarioPorSeccionesAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "ConfirmarImpresionMovBancarioPorSecciones"));
            vResult.ImprimirCompContDespuesDeChequeMovBancarioAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "ImprimirCompContDespuesDeChequeMovBancario"));
            vResult.ImprimirComprobanteDeMovBancarioAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "ImprimirComprobanteDeMovBancario"));
            vResult.BeneficiarioGenerico = LibConvert.ToInt(ValorSegunColumna(valListGetSettValueByCompany, "BeneficiarioGenerico"));
            vResult.ConceptoBancarioReversoSolicitudDePago = ValorSegunColumna(valListGetSettValueByCompany, "ConceptoBancarioReversoSolicitudDePago");
            vResult.NombrePlantillaComprobanteDePagoSueldo = ValorSegunColumna(valListGetSettValueByCompany, "NombrePlantillaComprobanteDePagoSueldo");
            vResult.GenerarMovReversoSiAnulaPagoAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "GenerarMovReversoSiAnulaPago"));
            return vResult;
        }

        #endregion // MovimientoBancarioStt
        #region TransferenciaBancariaStt
        private TransferenciaStt TransferenciaBancariaSttPorDefecto(int valConsecutivoCompania) {
            TransferenciaStt insEntidad = new TransferenciaStt();
            insEntidad.ConceptoBancarioReversoTransfIngreso = GetConceptoBancario("REV_AUTOMATICO_TRANSF_INGRESO");
            insEntidad.ConceptoBancarioReversoTransfEgreso = GetConceptoBancario("REV_AUTOMATICO_TRANSF_EGRESO");
            return insEntidad;
        }

        private void LlenaListado(TransferenciaStt valRecord, ref List<SettValueByCompany> valBusinessObject, int valConsecutivoCompania) {
            valBusinessObject.Add(ConvierteValor(valRecord.ConceptoBancarioReversoTransfIngreso, "ConceptoBancarioReversoTransfIngreso", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.ConceptoBancarioReversoTransfEgreso, "ConceptoBancarioReversoTransfEgreso", valConsecutivoCompania));
        }
        TransferenciaStt GetTransferenciaStt(List<SettValueByCompany> valListGetSettValueByCompany) {
            TransferenciaStt vResult = new TransferenciaStt();
            vResult.Module = GetModuleSegunColumna(valListGetSettValueByCompany, "ConceptoBancarioReversoTransfIngreso");
            vResult.GroupName = GetGroupNameSegunColumna(valListGetSettValueByCompany, "ConceptoBancarioReversoTransfIngreso");
            vResult.ConceptoBancarioReversoTransfIngreso = ValorSegunColumna(valListGetSettValueByCompany, "ConceptoBancarioReversoTransfIngreso");
            vResult.ConceptoBancarioReversoTransfEgreso = ValorSegunColumna(valListGetSettValueByCompany, "ConceptoBancarioReversoTransfEgreso");
            return vResult;
        }

        #endregion // TransferenciaBancariaStt

        #region  ProcesosStt
        ProcesosStt ProcesosSttPorDefecto(int valConsecutivoCompania) {
            ProcesosStt insEntidad = new ProcesosStt();
            insEntidad.InsertandoPorPrimeraVezAsBool = true;
            return insEntidad;
        }

        private void LlenaListado(ProcesosStt valRecord, ref List<SettValueByCompany> valBusinessObject, int valConsecutivoCompania) {
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.SeResincronizaronLosSupervisoresAsBool), "SeResincronizaronLosSupervisores", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.InsertandoPorPrimeraVezAsBool), "InsertandoPorPrimeraVez", valConsecutivoCompania));
        }
        ProcesosStt GetProcesosStt(List<SettValueByCompany> valListGetSettValueByCompany) {
            ProcesosStt vResult = new ProcesosStt();
            vResult.Module = GetModuleSegunColumna(valListGetSettValueByCompany, "SeResincronizaronLosSupervisores");
            vResult.GroupName = GetGroupNameSegunColumna(valListGetSettValueByCompany, "SeResincronizaronLosSupervisores");
            vResult.SeResincronizaronLosSupervisoresAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "SeResincronizaronLosSupervisores"));
            vResult.InsertandoPorPrimeraVezAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "InsertandoPorPrimeraVez"));
            return vResult;
        }
        #endregion //  ProcesosStt

        #region  NotaEntregaStt
        NotaEntregaStt NotaEntregaSttPorDefecto(int valConsecutivoCompania) {
            NotaEntregaStt insEntidad = new NotaEntregaStt();
            insEntidad.NumCopiasOrdenDeDespacho = 1;
            insEntidad.ModeloNotaEntregaAsEnum = eModeloDeFactura.eMD_FORMALIBRE;
            insEntidad.NotaEntregaPreNumeradaAsBool = false;
            insEntidad.PrimeraNotaEntrega = "00000000001";
            insEntidad.TipoPrefijoNotaEntregaAsEnum = eTipoDePrefijo.SinPrefijo;
            insEntidad.NombrePlantillaOrdenDeDespacho = "rpxOrdenDeDespachoFormatoLibre";
            insEntidad.ModeloNotaEntregaModoTexto = "Factura_PosEpsonTMU220FormatoLibre";
            return insEntidad;
        }

        private void LlenaListado(NotaEntregaStt valRecord, ref List<SettValueByCompany> valBusinessObject, int valConsecutivoCompania) {
            valBusinessObject.Add(ConvierteValor(valRecord.ModeloNotaEntregaAsDB, "ModeloNotaEntrega", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.NotaEntregaPreNumeradaAsBool), "NotaEntregaPreNumerada", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.PrimeraNotaEntrega, "PrimeraNotaEntrega", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.TipoPrefijoNotaEntregaAsDB, "TipoPrefijoNotaEntrega", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.NombrePlantillaNotaEntrega, "NombrePlantillaNotaEntrega", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.NombrePlantillaOrdenDeDespacho, "NombrePlantillaOrdenDeDespacho", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.ToStr(valRecord.NumCopiasOrdenDeDespacho), "NumCopiasOrdenDeDespacho", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.PrefijoNotaEntrega, "PrefijoNotaEntrega", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(valRecord.ModeloNotaEntregaModoTexto, "ModeloNotaEntregaModoTexto", valConsecutivoCompania));

        }
        NotaEntregaStt GetNotaEntregaStt(List<SettValueByCompany> valListGetSettValueByCompany) {
            NotaEntregaStt vResult = new NotaEntregaStt();
            vResult.Module = GetModuleSegunColumna(valListGetSettValueByCompany, "ModeloNotaEntrega");
            vResult.GroupName = GetGroupNameSegunColumna(valListGetSettValueByCompany, "ModeloNotaEntrega");
            vResult.ModeloNotaEntregaAsEnum = (eModeloDeFactura)LibConvert.DbValueToEnum(ValorSegunColumna(valListGetSettValueByCompany, "ModeloNotaEntrega"));
            vResult.NotaEntregaPreNumeradaAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "NotaEntregaPreNumerada"));
            vResult.PrimeraNotaEntrega = ValorSegunColumna(valListGetSettValueByCompany, "PrimeraNotaEntrega");
            vResult.TipoPrefijoNotaEntregaAsEnum = (eTipoDePrefijo)LibConvert.DbValueToEnum(ValorSegunColumna(valListGetSettValueByCompany, "TipoPrefijoNotaEntrega"));
            vResult.PrefijoNotaEntrega = ValorSegunColumna(valListGetSettValueByCompany, "PrefijoNotaEntrega");
            vResult.NombrePlantillaNotaEntrega = ValorSegunColumna(valListGetSettValueByCompany, "NombrePlantillaNotaEntrega");
            vResult.NombrePlantillaOrdenDeDespacho = ValorSegunColumna(valListGetSettValueByCompany, "NombrePlantillaOrdenDeDespacho");
            vResult.NumCopiasOrdenDeDespacho = LibConvert.ToInt(ValorSegunColumna(valListGetSettValueByCompany, "NumCopiasOrdenDeDespacho"));
            vResult.ModeloNotaEntregaModoTexto = ValorSegunColumna(valListGetSettValueByCompany, "ModeloNotaEntregaModoTexto");
            return vResult;
        }
        #endregion //  NotaEntregaStt

        #region Valores Por defecto
        private int GetConsecutivoBeneficiarioPorDefecto(int valConsecutivoCompania) {
            int vResult = 0;
            IBeneficiarioPdn vPdnModule = new Galac.Adm.Brl.Banco.clsBeneficiarioNav();
            vResult = vPdnModule.ConsecutivoBeneficiarioGenericoParaCrearEmpresa(valConsecutivoCompania);
            return vResult;
        }

        private string GetCodigoAlmacenPorDefecto(int valConsecutivoCompania) {
            string vResult = "";
            IAlmacenPdn vPdnModule = new Galac.Saw.Brl.Inventario.clsAlmacenNav();
            vResult = vPdnModule.GetCodigoAlmacenPorDefecto(valConsecutivoCompania);
            return vResult;
        }


        private string GetCodigoClientePorDefecto(int valConsecutivoCompania) {
            string vResult = "";
            IClientePdn vPdnModule = new Galac.Saw.Brl.Cliente.clsClienteNav();
            vResult = ValorDeLaColumna(vPdnModule.ClientePorDefecto(valConsecutivoCompania), "Codigo");
            return vResult;
        }


        private string GetCodigoVendedorPorDefecto(int valConsecutivoCompania) {
            string vResult = "";
            IVendedorPdn vPdnModule = new Galac.Adm.Brl.Vendedor.clsVendedorNav();
            vResult = ValorDeLaColumna(vPdnModule.VendedorPorDefecto(valConsecutivoCompania), "Codigo");
            return vResult;
        }


        private string GetCuentaBancariaPorDefecto() {
            string vResult = "";
            ICuentaBancariaPdn vPdnModule = new Galac.Adm.Brl.Banco.clsCuentaBancariaNav();
            vResult = vPdnModule.GetCuentaBancariaGenericaPorDefecto();
            return vResult;
        }


        private string GetConceptoBancario(string valNombreConcepto) {
            string vResult = "";
            //Galac.Adm.Ccl.Banco.IConceptoBanacarioPdn
            IConceptoBancarioPdn vPdnModule = new Galac.Adm.Brl.Banco.clsConceptoBancarioNav();
            vResult = ValorDeLaColumna(vPdnModule.LisConceptosBancariosPorDefecto(), valNombreConcepto);
            return vResult;
        }

        string ValorDeLaColumna(XElement valRecord, string valNombreCampo) {
            string vResult = "";
            var vEntity = from vRecord in valRecord.Descendants("GpResult")
                          select vRecord;
            foreach (XElement vItem in vEntity) {
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element(valNombreCampo), null))) {
                    vResult = LibConvert.ToStr(vItem.Element(valNombreCampo).Value);
                }
            }
            return vResult;
        }
        #endregion //Valores Por defecto

        #region VerificadorDePreciosStt
        VerificadorDePreciosStt VerificadorDePreciosSttPorDefecto(int valConsecutivoCompania) {
            VerificadorDePreciosStt insEntidad = new VerificadorDePreciosStt();
            insEntidad.RutaImagen = "";
            insEntidad.DuracionEnPantallaEnSegundos = 3;
            insEntidad.TipoDeBusquedaArticuloAsEnum = eTipoDeBusquedaArticulo.Codigo;
            insEntidad.NivelDePrecioAMostrarAsEnum = Ccl.SttDef.eNivelDePrecio.Nivel1;
            insEntidad.TipoDePrecioAMostrarEnVerificadorAsEnum = eTipoDePrecioAMostrarEnVerificador.PrecioDesglosado;
            insEntidad.UsaMostrarPreciosEnDivisaAsBool = false;
            insEntidad.TipoDeConversionParaPreciosAsEnum = eTipoDeConversionParaPrecios.MonedaLocalADivisa;
            return insEntidad;
        }

        private void LlenaListado(VerificadorDePreciosStt valRecord, ref List<SettValueByCompany> valBusinessObject, int valConsecutivoCompania) {
            valBusinessObject.Add(ConvierteValor(valRecord.RutaImagen, "RutaImagen", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.EnumToDbValue((int)valRecord.NivelDePrecioAMostrarAsEnum), "NivelDePrecioAMostrar", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.ToStr(valRecord.DuracionEnPantallaEnSegundos), "DuracionEnPantallaEnSegundos", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.EnumToDbValue((int)valRecord.TipoDeBusquedaArticuloAsEnum), "TipoDeBusquedaArticulo", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.EnumToDbValue((int)valRecord.TipoDePrecioAMostrarEnVerificadorAsEnum), "TipoDePrecioAMostrarEnVerificador", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.BoolToSN(valRecord.UsaMostrarPreciosEnDivisaAsBool), "UsaMostrarPreciosEnDivisa", valConsecutivoCompania));
            valBusinessObject.Add(ConvierteValor(LibConvert.EnumToDbValue((int)valRecord.TipoDeConversionParaPreciosAsEnum), "TipoDeConversionParaPrecios", valConsecutivoCompania));
        }

        VerificadorDePreciosStt GetVerificadorDePreciosStt(List<SettValueByCompany> valListGetSettValueByCompany) {
            VerificadorDePreciosStt vResult = new VerificadorDePreciosStt();
            vResult.Module = GetModuleSegunColumna(valListGetSettValueByCompany, "RutaImagen");
            vResult.GroupName = GetGroupNameSegunColumna(valListGetSettValueByCompany, "RutaImagen");
            vResult.RutaImagen = ValorSegunColumna(valListGetSettValueByCompany, "RutaImagen");
            vResult.DuracionEnPantallaEnSegundos = LibConvert.ToInt(ValorSegunColumna(valListGetSettValueByCompany, "DuracionEnPantallaEnSegundos"));
            vResult.TipoDeBusquedaArticuloAsEnum = (eTipoDeBusquedaArticulo)LibConvert.DbValueToEnum(ValorSegunColumna(valListGetSettValueByCompany, "TipoDeBusquedaArticulo"));
            vResult.NivelDePrecioAMostrarAsEnum = (Ccl.SttDef.eNivelDePrecio)LibConvert.DbValueToEnum(ValorSegunColumna(valListGetSettValueByCompany, "NivelDePrecioAMostrar"));
            vResult.TipoDePrecioAMostrarEnVerificadorAsEnum = (Ccl.SttDef.eTipoDePrecioAMostrarEnVerificador)LibConvert.DbValueToEnum(ValorSegunColumna(valListGetSettValueByCompany, "TipoDePrecioAMostrarEnVerificador"));
            vResult.UsaMostrarPreciosEnDivisaAsBool = LibConvert.SNToBool(ValorSegunColumna(valListGetSettValueByCompany, "UsaMostrarPreciosEnDivisa"));
            vResult.TipoDeConversionParaPreciosAsEnum = (eTipoDeConversionParaPrecios)LibConvert.DbValueToEnum(ValorSegunColumna(valListGetSettValueByCompany, "TipoDeConversionParaPrecios"));
            return vResult;
        }
        #endregion //  VerificadorDePreciosStt

        #region ProduccionStt
        private ProduccionStt ProduccionSttPorDefecto(int valConsecutivoCompania) {
            ProduccionStt insEntidad = new ProduccionStt();
            insEntidad.CalcularCostoDelArticuloTerminadoAPartirDeAsEnum = eFormaDeCalcularCostoTerminado.APartirDeCostoEnMonedaLocal;
            return insEntidad;
        }

        private void LlenaListado(ProduccionStt valRecord, ref List<SettValueByCompany> valBusinessObject, int valConsecutivoCompania) {
            valBusinessObject.Add(ConvierteValor(valRecord.CostoTerminadoCalculadoAPartirDeAsDB, "CostoTerminadoCalculadoAPartirDe", valConsecutivoCompania));
        }
        ProduccionStt GetProduccionStt(List<SettValueByCompany> valListGetSettValueByCompany) {
            ProduccionStt vResult = new ProduccionStt();
            vResult.Module = GetModuleSegunColumna(valListGetSettValueByCompany, "CostoTerminadoCalculadoAPartirDe");
            vResult.GroupName = GetGroupNameSegunColumna(valListGetSettValueByCompany, "CostoTerminadoCalculadoAPartirDe");
            vResult.CalcularCostoDelArticuloTerminadoAPartirDeAsEnum = (eFormaDeCalcularCostoTerminado)LibConvert.DbValueToEnum(ValorSegunColumna(valListGetSettValueByCompany, "CostoTerminadoCalculadoAPartirDe"));
            return vResult;
        }
        #endregion // ProduccionStt

        public SettValueByCompany ConvierteValor(string Value, string valNameSettDefinition, int valConsecutivoCompania) {
            SettValueByCompany vResult = new SettValueByCompany();
            vResult.ConsecutivoCompania = valConsecutivoCompania;
            vResult.NameSettDefinition = valNameSettDefinition;
            vResult.Value = Value;
            return vResult;
        }

        List<SettValueByCompany> DatosPorDefecto(int valConsecutivoCompania, string valCodigoMonedaLocal, string valNombreMonedaLocal, string valCodigoMonedaExtranjera, string valNombreMonedaExtranjera, string valCiudad) {
            List<SettValueByCompany> vResult = new List<SettValueByCompany>();
            LlenaListado(CompaniaSttPorDefecto(), ref vResult, valConsecutivoCompania);
            LlenaListado(GeneralSttPorDefecto(), ref vResult, valConsecutivoCompania);
            LlenaListado(FacturacionSttPorDefecto(), ref vResult, valConsecutivoCompania);
            LlenaListado(FacturacionContinuacionSttPorDefecto(valConsecutivoCompania), ref vResult, valConsecutivoCompania);
            LlenaListado(ImpresiondeFacturaSttPorDefecto(), ref vResult, valConsecutivoCompania);
            LlenaListado(ModeloDeFacturaSttPorDefecto(), ref vResult, valConsecutivoCompania);
            LlenaListado(CamposDefiniblesSttPorDefecto(), ref vResult, valConsecutivoCompania);
            LlenaListado(FacturaPuntoDeVentaSttPorDefecto(), ref vResult, valConsecutivoCompania);
            LlenaListado(FacturaBalanzaEtiquetasSttPorDefecto(), ref vResult, valConsecutivoCompania);
            LlenaListado(FacturaImprentaDigitalSttPorDefecto(), ref vResult, valConsecutivoCompania);
            LlenaListado(CotizacionSttPorDefecto(), ref vResult, valConsecutivoCompania);
            LlenaListado(NotasDebitoCreditoEntregaSttPorDefecto(), ref vResult, valConsecutivoCompania);
            LlenaListado(VendedorSttPorDefecto(valConsecutivoCompania), ref vResult, valConsecutivoCompania);
            LlenaListado(ClienteSttPorDefecto(valConsecutivoCompania), ref vResult, valConsecutivoCompania);
            LlenaListado(CobranzasSttPorDefecto(), ref vResult, valConsecutivoCompania);
            LlenaListado(ComisionesSttPorDefecto(), ref vResult, valConsecutivoCompania);
            LlenaListado(InventarioSttPorDefecto(valConsecutivoCompania), ref vResult, valConsecutivoCompania);
            LlenaListado(NotaEntradaSalidaSttPorDefecto(), ref vResult, valConsecutivoCompania);
            LlenaListado(MetododecostosSttPorDefecto(), ref vResult, valConsecutivoCompania);
            LlenaListado(ComprasSttPorDefecto(), ref vResult, valConsecutivoCompania);
            LlenaListado(CxPProveedorPagosSttPorDefecto(), ref vResult, valConsecutivoCompania);
            LlenaListado(RetencionIVAStttPorDefecto(), ref vResult, valConsecutivoCompania);
            LlenaListado(RetencionISLRSttPorDefecto(valCiudad), ref vResult, valConsecutivoCompania);
            LlenaListado(PlanillaDeIVASttPorDefecto(), ref vResult, valConsecutivoCompania);
            LlenaListado(BancosSttPorDefecto(), ref vResult, valConsecutivoCompania);
            LlenaListado(MonedaSttPorDefecto(valCodigoMonedaLocal, valNombreMonedaLocal, valCodigoMonedaExtranjera, valNombreMonedaExtranjera), ref vResult, valConsecutivoCompania);
            LlenaListado(AnticipoSttPorDefecto(), ref vResult, valConsecutivoCompania);
            LlenaListado(MovimientoBancarioSttPorDefecto(valConsecutivoCompania), ref vResult, valConsecutivoCompania);
            LlenaListado(ProcesosSttPorDefecto(valConsecutivoCompania), ref vResult, valConsecutivoCompania);
            LlenaListado(NotaEntregaSttPorDefecto(valConsecutivoCompania), ref vResult, valConsecutivoCompania);
            LlenaListado(VerificadorDePreciosSttPorDefecto(valConsecutivoCompania), ref vResult, valConsecutivoCompania);
            LlenaListado(ImagenesComprobantesRetencionSttPorDefecto(), ref vResult, valConsecutivoCompania);
            LlenaListado(ProduccionSttPorDefecto(valConsecutivoCompania), ref vResult, valConsecutivoCompania);
            LlenaListado(TransferenciaBancariaSttPorDefecto(valConsecutivoCompania), ref vResult, valConsecutivoCompania);
            return vResult;
        }

        bool ISettValueByCompanyPdn.InsertaValoresPorDefecto(int valConsecutivoCompania, string valCodigoMonedaLocal, string valNombreMonedaLocal, string valCodigoMonedaExtranjera, string valNombreMonedaExtranjera, string valCiudad) {
            IList<SettValueByCompany> vBusinessObject = new List<SettValueByCompany>();
            vBusinessObject = DatosPorDefecto(valConsecutivoCompania, valCodigoMonedaLocal, valNombreMonedaLocal, valCodigoMonedaExtranjera, valNombreMonedaExtranjera, valCiudad);
            ILibDataComponent<IList<SettValueByCompany>, IList<SettValueByCompany>> instanciaDal = new Galac.Saw.Dal.SttDef.clsSettValueByCompanyDat();
            return instanciaDal.Insert(vBusinessObject).Success;
        }

        List<Parametros> ISettValueByCompanyPdn.ParametrosList(int valConsecutivoCompania) {
            List<Parametros> insParametrosList = new List<Parametros>();
            List<SettValueByCompany> vListGetSettValueByCompany = new List<SettValueByCompany>();
            Parametros insParametros = new Parametros();
            XElement vSettValueByCompany = GetSettValueByCompany(valConsecutivoCompania);
            vListGetSettValueByCompany = ListGetSettValueByCompany(vSettValueByCompany);
            insParametros.ParametrosAnticipoStt = GetAnticipoStt(vListGetSettValueByCompany);
            insParametros.ParametrosBancosStt = GetBancosStt(vListGetSettValueByCompany);
            insParametros.ParametrosCamposDefiniblesStt = GetCamposDefiniblesStt(vListGetSettValueByCompany);
            insParametros.ParametrosClienteStt = GetClienteStt(vListGetSettValueByCompany);
            insParametros.ParametrosCobranzasStt = GetCobranzasStt(vListGetSettValueByCompany);
            insParametros.ParametrosComisionesStt = GetComisionesStt(vListGetSettValueByCompany);
            insParametros.ParametrosCompaniaStt = GetCompaniaStt(vListGetSettValueByCompany);
            insParametros.ParametrosComprasStt = GetComprasStt(vListGetSettValueByCompany);            
            insParametros.ParametrosCotizacionStt = GetCotizacionStt(vListGetSettValueByCompany);
            insParametros.ParametrosCxPProveedorPagosStt = GetCxPProveedorPagosStt(vListGetSettValueByCompany);
            insParametros.ParametrosFacturacionContinuacionStt = GetFacturacionContinuacionStt(vListGetSettValueByCompany);
            insParametros.ParametrosFacturacionStt = GetFacturacionStt(vListGetSettValueByCompany);
            insParametros.ParametrosFacturaImprentaDigitalStt = GetFacturaImprentaDigitalStt(vListGetSettValueByCompany);
            insParametros.ParametrosGeneralStt = GetGeneralStt(vListGetSettValueByCompany);
            insParametros.ParametrosImpresiondeFacturaStt = GetImpresiondeFacturaStt(vListGetSettValueByCompany);
            insParametros.ParametrosInventarioStt = GetInventarioStt(vListGetSettValueByCompany);
            insParametros.ParametrosMetododecostosStt = GetMetododecostosStt(vListGetSettValueByCompany);
            insParametros.ParametrosModeloDeFacturaStt = GetModeloDeFacturaStt(vListGetSettValueByCompany);
            insParametros.ParametrosMonedaStt = GetMonedaStt(vListGetSettValueByCompany);
            insParametros.ParametrosMovimientoBancarioStt = GetMovimientoBancarioStt(vListGetSettValueByCompany);
            insParametros.ParametrosTransferenciaBancariaStt = GetTransferenciaStt(vListGetSettValueByCompany);
            insParametros.ParametrosNotaEntradaSalidaStt = GetNotaEntradaSalidaStt(vListGetSettValueByCompany);
            insParametros.ParametrosNotaEntregaStt = GetNotaEntregaStt(vListGetSettValueByCompany);
            insParametros.ParametrosNotasDebitoCreditoEntregaStt = GetNotasDebitoCreditoEntregaStt(vListGetSettValueByCompany);
            insParametros.ParametrosPlanillaDeIVAStt = GetPlanillaDeIVAStt(vListGetSettValueByCompany);
            insParametros.ParametrosProcesosStt = GetProcesosStt(vListGetSettValueByCompany);
            insParametros.ParametrosRetencionISLRStt = GetRetencionISLRSStt(vListGetSettValueByCompany);
            insParametros.ParametrosRetencionIVAStt = GetRetencionIVAStt(vListGetSettValueByCompany);
            insParametros.ParametrosVendedorStt = GetVendedorStt(vListGetSettValueByCompany);
            insParametros.ParametrosVerificadorDePreciosStt = GetVerificadorDePreciosStt(vListGetSettValueByCompany);
            insParametros.ParametrosImagenesComprobantesRetencionStt = GetImagenesComprobantesRetencionStt(vListGetSettValueByCompany);
            insParametros.ParametrosProduccionStt = GetProduccionStt(vListGetSettValueByCompany);
            insParametrosList.Add(insParametros);
            return insParametrosList;
        }

        List<Module> ISettValueByCompanyPdn.GetModuleList(int valConsecutivoCompania) {
            List<Module> vResult = new List<Module>();
            List<ISettDefinition> vSettDef = GetSettDefinition(valConsecutivoCompania);
            vSettDef.Sort(new Comparison<ISettDefinition>((a, b) => a.GroupName.CompareTo(b.GroupName)));
            XElement vModules = new clsSettDefinitionNav().GetModuleNames();
            var vItems = from vData in vModules.Descendants("GpResult")
                         select vData;
            foreach (var vItem in vItems) {
                if (LibConvert.ToInt(vItem.Element("LevelModule").Value) == 9) {
                    continue;
                }
                Module vModule = new Module();
                vModule.DisplayName = vItem.Element("DisplayName").Value;
                var vParametrosGrupo = vSettDef.Where(p => p.Module == vModule.DisplayName).ToList();
                if (vParametrosGrupo != null) {
                    foreach (var vGrupo in vParametrosGrupo) {
                        vModule.Groups.Add(new Group(vGrupo.GroupName, vGrupo));
                    }
                }
                vResult.Add(vModule);
            }
            return vResult;
        }

        private List<ISettDefinition> GetSettDefinition(int valConsecutivoCompania) {
            List<ISettDefinition> vResult = new List<ISettDefinition>();
            XElement vSettValueByCompany = GetSettValueByCompany(valConsecutivoCompania);
            List<SettValueByCompany> vListGetSettValueByCompany = ListGetSettValueByCompany(vSettValueByCompany);
            vResult.Add(GetVerificadorDePreciosStt(vListGetSettValueByCompany));
            vResult.Add(GetAnticipoStt(vListGetSettValueByCompany));
            vResult.Add(GetBancosStt(vListGetSettValueByCompany));
            vResult.Add(GetCamposDefiniblesStt(vListGetSettValueByCompany));
            vResult.Add(GetClienteStt(vListGetSettValueByCompany));
            vResult.Add(GetCobranzasStt(vListGetSettValueByCompany));
            vResult.Add(GetComisionesStt(vListGetSettValueByCompany));
            vResult.Add(GetCompaniaStt(vListGetSettValueByCompany));
            vResult.Add(GetComprasStt(vListGetSettValueByCompany));
            vResult.Add(GetCotizacionStt(vListGetSettValueByCompany));
            vResult.Add(GetCxPProveedorPagosStt(vListGetSettValueByCompany));
            vResult.Add(GetFacturacionContinuacionStt(vListGetSettValueByCompany));
            vResult.Add(GetFacturacionStt(vListGetSettValueByCompany));
            vResult.Add(GetFacturaImprentaDigitalStt(vListGetSettValueByCompany));
            if (LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "EsPuntoDeVenta")) {
                vResult.Add(GetFacturaPuntoDeVentaStt(vListGetSettValueByCompany));
                vResult.Add(GetFacturaBalanzaEtiquetasStt(vListGetSettValueByCompany));
            }
            vResult.Add(GetGeneralStt(vListGetSettValueByCompany));
            vResult.Add(GetImpresiondeFacturaStt(vListGetSettValueByCompany));
            vResult.Add(GetInventarioStt(vListGetSettValueByCompany));
            vResult.Add(GetMetododecostosStt(vListGetSettValueByCompany));
            vResult.Add(GetModeloDeFacturaStt(vListGetSettValueByCompany));
            vResult.Add(GetMonedaStt(vListGetSettValueByCompany));
            vResult.Add(GetMovimientoBancarioStt(vListGetSettValueByCompany));
            vResult.Add(GetTransferenciaStt(vListGetSettValueByCompany));
            vResult.Add(GetNotaEntradaSalidaStt(vListGetSettValueByCompany));
            vResult.Add(GetNotaEntregaStt(vListGetSettValueByCompany));
            vResult.Add(GetNotasDebitoCreditoEntregaStt(vListGetSettValueByCompany));
            if (!LibDefGen.ProgramInfo.IsCountryPeru()) {
                vResult.Add(GetPlanillaDeIVAStt(vListGetSettValueByCompany));
                vResult.Add(GetRetencionISLRSStt(vListGetSettValueByCompany));
            }
            vResult.Add(GetProcesosStt(vListGetSettValueByCompany));
            vResult.Add(GetRetencionIVAStt(vListGetSettValueByCompany));
            vResult.Add(GetVendedorStt(vListGetSettValueByCompany));
            vResult.Add(GetImagenesComprobantesRetencionStt(vListGetSettValueByCompany));
            vResult.Add(GetProduccionStt(vListGetSettValueByCompany));
            return vResult;
        }

        XElement GetSettValueByCompany(int valConsecutivoCompania) {
            XElement vResult;
            QAdvSql insQAdvSql = new QAdvSql("");
            StringBuilder vSql = new StringBuilder();
            string vWhere = insQAdvSql.SqlIntValueWithAnd("", "ConsecutivoCompania", valConsecutivoCompania);
            vWhere = insQAdvSql.WhereSql(vWhere);
            vSql.AppendLine("SELECT ");
            vSql.AppendLine("    Comun.Gv_SettValueByCompany_B1.NameSettDefinition, ");
            vSql.AppendLine("    Comun.Gv_SettValueByCompany_B1.Value, ");
            vSql.AppendLine("    Comun.Gv_SettDefinition_B1.LevelModule, ");
            vSql.AppendLine("    Comun.Gv_SettDefinition_B1.Module, ");
            vSql.AppendLine("    CAST(Comun.Gv_SettDefinition_B1.LevelModule AS nvarchar) + ' - ' + Comun.Gv_SettDefinition_B1.Module AS DisplayName, ");
            vSql.AppendLine("    Comun.Gv_SettDefinition_B1.GroupName ");
            vSql.AppendLine("FROM Comun.Gv_SettValueByCompany_B1 ");
            vSql.AppendLine("    INNER JOIN Comun.Gv_SettDefinition_B1 ON ");
            vSql.AppendLine("            Comun.Gv_SettValueByCompany_B1.NameSettDefinition = Comun.Gv_SettDefinition_B1.Name ");
            vSql.AppendLine(vWhere);
            ILibDataComponent<IList<SettValueByCompany>, IList<SettValueByCompany>> instanciaDal = GetDataInstance();
            vResult = instanciaDal.QueryInfo(eProcessMessageType.Query, vSql.ToString(), null);
            return vResult;
        }

        string ValorSegunColumna(List<SettValueByCompany> valListSettValueByCompany, string valColumna) {
            string vResult = "";
            var vData = from vRecord in valListSettValueByCompany
                        where vRecord.NameSettDefinition == valColumna
                        select vRecord;
            foreach (SettValueByCompany vItem in vData) {
                vResult = vItem.Value;
                break;
            }
            return vResult;
        }

        XElement ValorSegunColumna(List<SettValueByCompany> valListSettValueByCompany, string valColumna, bool valIsNumeric) {
            string vResult = ValorSegunColumna(valListSettValueByCompany, valColumna);
            return new XElement(valColumna, vResult);
        }

        string GetGroupNameSegunColumna(List<SettValueByCompany> valListSettValueByCompany, string valColumna) {
            string vResult = null;
            var vData = from vRecord in valListSettValueByCompany
                        where vRecord.NameSettDefinition.ToLower() == valColumna.ToLower()
                        select vRecord;
            foreach (SettValueByCompany vItem in vData) {
                vResult = vItem.GroupName;
                break;
            }
            return vResult;
        }

        string GetModuleSegunColumna(List<SettValueByCompany> valListSettValueByCompany, string valColumna) {
            string vResult = null;
            var vData = from vRecord in valListSettValueByCompany
                        where vRecord.NameSettDefinition.ToLower() == valColumna.ToLower()
                        select vRecord;
            foreach (SettValueByCompany vItem in vData) {
                vResult = vItem.Module;
                break;
            }
            return vResult;
        }

        List<SettValueByCompany> ListGetSettValueByCompany(XElement valItem) {
            List<SettValueByCompany> vResult = new List<SettValueByCompany>();
            var vEnty = from vData in valItem.Descendants("GpResult")
                        select vData;
            foreach (XElement vItem in vEnty) {
                SettValueByCompany insSettValueByCompany = new SettValueByCompany();
                insSettValueByCompany.NameSettDefinition = LibString.Trim(vItem.Element("NameSettDefinition").Value);
                insSettValueByCompany.Module = LibString.Trim(vItem.Element("DisplayName").Value);
                insSettValueByCompany.GroupName = LibString.Trim(vItem.Element("GroupName").Value);
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Value"), null))) {
                    insSettValueByCompany.Value = LibString.Trim(vItem.Element("Value").Value);
                } else {
                    insSettValueByCompany.Value = "";
                }

                vResult.Add(insSettValueByCompany);
            }
            return vResult;
        }

        bool ISettValueByCompanyPdn.SpecializedUpdate(List<Module> valModules) {
            bool vResult = true;
            List<SettValueByCompany> vList = new List<SettValueByCompany>();

            foreach (Module vModule in valModules) {
                foreach (Group vGroup in vModule.Groups) {
                    var vModel = LibReflection.GetPropertyValue(vGroup.Content, "Model", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
                    var vProperties = vModel.GetType().GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
                        .Where(p => p.CanRead);
                    foreach (var vProperty in vProperties) {
                        string vNameSettDefinition = vProperty.Name;

                        if (vNameSettDefinition == "GroupName" || vNameSettDefinition == "Module" ||
                            vNameSettDefinition == "fldTimeStamp" || vNameSettDefinition == "Datos" ||
                            vNameSettDefinition.LastIndexOf("AsString") > 0 ||
                            vProperty.PropertyType.IsEnum) {
                            continue;
                        }
                        vNameSettDefinition = vNameSettDefinition.Replace("AsDB", "").Replace("AsBool", "");
                        SettValueByCompany insSettValueByCompany = new SettValueByCompany();
                        insSettValueByCompany.ConsecutivoCompania = LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania");
                        insSettValueByCompany.NameSettDefinition = vNameSettDefinition;
                        object vValue = vProperty.GetValue(vModel, null);
                        if (!Object.ReferenceEquals(vValue, null)) {
                            if (vProperty.PropertyType.Equals(typeof(DateTime))) {
                                insSettValueByCompany.Value = (LibConvert.ToDate(vValue)).ToString("yyyy-MM-dd HH:mm:ss");
                            } else if (vProperty.PropertyType.Equals(typeof(Boolean))) {
                                insSettValueByCompany.Value = LibConvert.BoolToSN(LibConvert.ToBool(vValue));
                            } else if (vProperty.PropertyType.Equals(typeof(decimal)) || vProperty.PropertyType.Equals(typeof(double)) || vProperty.PropertyType.Equals(typeof(float))) {
                                var floatStrValue = LibImportData.ToDec(vValue.ToString());
                                insSettValueByCompany.Value = floatStrValue.ToString(CultureInfo.InvariantCulture);
                            } else {
                                insSettValueByCompany.Value = vValue.ToString();
                            }
                        } else {
                            insSettValueByCompany.Value = string.Empty;
                        }
                        vList.Add(insSettValueByCompany);
                    }
                }
            }
            RegisterClient();
            _Db.Update(vList);
            return vResult;
        }

        private DateTime ConvertTicksToDateTime(long lticks) {
            return new DateTime(lticks);
        }

        bool ISettValueByCompanyPdn.SePuedeModificarParametrosDeConciliacion() {
            return true;
        }


        string ISettValueByCompanyPdn.GeneraPriemraBoleta(int valConsecutivoCompania, int valPrimerDocumento) {
            return new ReglasDeOtrosModulos().GenerateNextNumeroByTipoDocumento(valConsecutivoCompania, "NC-", "6", valPrimerDocumento, true);
        }

        string ISettValueByCompanyPdn.GeneraPriemraNotaDeCredito(int valConsecutivoCompania, int valPrimerDocumento) {
            return new ReglasDeOtrosModulos().GenerateNextNumeroByTipoDocumento(valConsecutivoCompania, "NC-", "1", valPrimerDocumento, true);

        }

        string ISettValueByCompanyPdn.GeneraPriemraNotaDeDebito(int valConsecutivoCompania, int valPrimerDocumento) {
            return new ReglasDeOtrosModulos().GenerateNextNumeroByTipoDocumento(valConsecutivoCompania, "ND-", "2", valPrimerDocumento, true);

        }

        int ISettValueByCompanyPdn.DefaultLongitudCodigoCliente() {
            return 10;
        }

        int ISettValueByCompanyPdn.DefaultLongitudCodigoProveedor() {
            return 10;
        }

        int ISettValueByCompanyPdn.DefaultLongitudCodigoVendedor() {
            return 5;
        }

        bool ISettValueByCompanyPdn.ActualizaValoresMonedaLocal(int valConsecutivoCompania, string valCodigoMonedaLocal, string valNombreMonedaLocal, string valSimboloMonedaLocal, decimal valMontoAPartirDelCualEnviarAvisoDeuda) {
            bool vResult = false;
            if (valConsecutivoCompania > 0) {

                ILibDataComponent<IList<SettValueByCompany>, IList<SettValueByCompany>> instanciaDal = GetDataInstance();
                List<SettValueByCompany> vListRecord = new List<SettValueByCompany>();
                SettValueByCompany vRecord = new SettValueByCompany();
                vRecord.ConsecutivoCompania = valConsecutivoCompania;

                vRecord.NameSettDefinition = "CodigoMonedaLocal";
                vRecord.Value = valCodigoMonedaLocal;
                vListRecord.Add(vRecord);

                vRecord.NameSettDefinition = "NombreMonedaLocal";
                vRecord.Value = valNombreMonedaLocal;
                vListRecord.Add(vRecord);

                vRecord.NameSettDefinition = "SimboloMonedaLocal";
                vRecord.Value = valSimboloMonedaLocal;
                vListRecord.Add(vRecord);

                vRecord.NameSettDefinition = "MontoAPartirDelCualEnviarAvisoDeuda";
                vRecord.Value = LibConvert.ToStr(valMontoAPartirDelCualEnviarAvisoDeuda);
                vListRecord.Add(vRecord);

                vResult = instanciaDal.SpecializedUpdate(vListRecord, "ActualizaValor").Success;
            }
            return vResult;
        }

        bool ISettValueByCompanyPdn.ActualizaValorEnDondeRetenerIVA(int valConsecutivoCompania, string valDondeRetenerIVA) {
            bool vResult = false;
            if (valConsecutivoCompania > 0) {

                ILibDataComponent<IList<SettValueByCompany>, IList<SettValueByCompany>> instanciaDal = GetDataInstance();
                List<SettValueByCompany> vListRecord = new List<SettValueByCompany>();
                SettValueByCompany vRecord = new SettValueByCompany();
                vRecord.ConsecutivoCompania = valConsecutivoCompania;

                vRecord.NameSettDefinition = "EnDondeRetenerIVA";
                vRecord.Value = valDondeRetenerIVA;
                vListRecord.Add(vRecord);

                vResult = instanciaDal.SpecializedUpdate(vListRecord, "ActualizaValor").Success;
            }
            return vResult;
        }


        bool ISettValueByCompanyPdn.ResetFechaDeInicioContabilizacion(int valConsecutivoCompania, DateTime valFechaDeInicioContabilizacion) {
            ILibDataComponent<IList<SettValueByCompany>, IList<SettValueByCompany>> instanciaDal = GetDataInstance();
            List<SettValueByCompany> vListRecord = new List<SettValueByCompany>();
            SettValueByCompany vRecord = new SettValueByCompany();
            vRecord.ConsecutivoCompania = valConsecutivoCompania;

            vRecord.NameSettDefinition = "FechaDeInicioContabilizacion";
            vRecord.Value = LibConvert.ToStr(valFechaDeInicioContabilizacion);
            vListRecord.Add(vRecord);

            return instanciaDal.SpecializedUpdate(vListRecord, "ActualizaValor").Success;
        }


        bool ISettValueByCompanyPdn.SttUsaVendedor(int valConsecutivoCompania, string valCodigoVendedor) {
            decimal vResult = 0;
            QAdvSql insQAdvSql = new QAdvSql("");
            string vWhere = "";
            StringBuilder vSql = new StringBuilder();
            vWhere = insQAdvSql.SqlValueWithAnd("", "NameSettDefinition", "@NameSettDefinition");
            vWhere = insQAdvSql.SqlValueWithAnd(vWhere, "Value", "@Value");
            vWhere = insQAdvSql.SqlValueWithAnd(vWhere, "ConsecutivoCompania", "@ConsecutivoCompania");
            vWhere = insQAdvSql.WhereSql(vWhere);
            vSql.Append("SELECT ");
            vSql.Append("   COUNT(NameSettDefinition) AS Cantidad");
            vSql.Append(" FROM ");
            vSql.Append(" Comun.Gv_SettValueByCompany_B1 ");
            vSql.Append(vWhere);
            LibGpParams insParams = new LibGpParams();
            insParams.AddInString("NameSettDefinition", "CodigoGenericoVendedor", 50);
            insParams.AddInString("Value", valCodigoVendedor, 200);
            insParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            ILibDataComponent<IList<SettValueByCompany>, IList<SettValueByCompany>> instanciaDal = GetDataInstance();
            XElement vResulset = instanciaDal.QueryInfo(eProcessMessageType.Query, vSql.ToString(), insParams.Get());
            vResult = (from vRecord in vResulset.Descendants("GpResult")
                       select new {
                           Cantidad = LibConvert.ToDec(vRecord.Element("Cantidad"))
                       }).FirstOrDefault().Cantidad;
            return vResult > 0;
        }

        string ISettValueByCompanyPdn.ConsultaCampoSettValueByCompany(string valCampo, int valConsecutivoCompania) {
            string x;
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInString("NameSettDefinition", valCampo, 50);
            IList<Galac.Saw.Ccl.SttDef.SettValueByCompany> ListStt = ((ILibBusinessComponentWithSearch<IList<Galac.Saw.Ccl.SttDef.SettValueByCompany>, IList<Galac.Saw.Ccl.SttDef.SettValueByCompany>>)this).GetData(eProcessMessageType.SpName, "SettValueByCompanyGET", vParams.Get());
            if (ListStt.Count > 0) {
                Galac.Saw.Ccl.SttDef.SettValueByCompany svl = ListStt[0];
                x = (string)svl.GetType().GetProperty("Value").GetValue(svl, null);
            } else {
                x = "";
            }
            return x;
        }

        bool ISettValueByCompanyPdn.ExisteMunicipio(int valConsecutivoMunicipio, string valNombreCiudad) {
            QAdvSql insQAdvSql = new QAdvSql("");
            string vWhere = "";
            StringBuilder vSql = new StringBuilder();
            int vResult;
            try {
                vWhere = insQAdvSql.SqlValueWithAnd("", "Comun.Gv_MunicipioCiudad_B1.Consecutivo", LibConvert.ToStr(valConsecutivoMunicipio));
                vWhere = insQAdvSql.SqlValueWithAnd(vWhere, "Comun.Gv_MunicipioCiudad_B1.NombreCiudad", valNombreCiudad);
                vWhere = insQAdvSql.WhereSql(vWhere);
                vSql.Append(" SELECT ");
                vSql.Append(" Consecutivo");
                vSql.Append(" FROM ");
                vSql.Append(" Comun.Gv_MunicipioCiudad_B1 ");
                vSql.Append(vWhere);
                LibGpParams insParams = new LibGpParams();
                ILibDataComponent<IList<SettValueByCompany>, IList<SettValueByCompany>> instanciaDal = GetDataInstance();
                XElement vResulset = instanciaDal.QueryInfo(eProcessMessageType.Query, vSql.ToString(), insParams.Get());
                if (vResulset != null && vResulset.HasElements) {
                    vResult = (from vRecord in vResulset.Descendants("GpResult")
                               select vRecord).Count();
                    return vResult > 0;
                } else {
                    return false;
                }
            } catch (Exception vEx) {
                throw vEx;
            }
        }

        bool ISettValueByCompanyPdn.PuedeActivarModulo(string valCodigoMunicipio) {
            IClasificadorActividadEconomicaPdn insActEcon = new clsClasificadorActividadEconomicaNav();
            return insActEcon.TieneConceptos(valCodigoMunicipio);
        }

        string ISettValueByCompanyPdn.BuscaNombreMoneda(string valCodigoMoneda) {
            IMonedaPdn insPdn = new clsMonedaNav();
            return insPdn.GetNombreMoneda(valCodigoMoneda);
        }

        int ISettValueByCompanyPdn.CopiarParametrosAdministrativos(int valConsecutivoCompaniaOrigen, int valConsecutivoCompaniaDestino) {
            bool vResultParcial = false;
            int vResult = 1;
            if (!TieneParametrosCompletos(valConsecutivoCompaniaOrigen)) {
                vResult = (int)eMensajeCopiarParametros.Error_Parametros_Incompletos;
                return vResult;
            }
            if (EliminarParametrosAntesDeCopiar(valConsecutivoCompaniaDestino)) {
                vResultParcial = BuscarEInsertaCopiaDeParametros(valConsecutivoCompaniaOrigen, valConsecutivoCompaniaDestino);
            } else {
                vResult = (int)eMensajeCopiarParametros.Error_Al_Eliminar_Parametros;
                return vResult;
            }
            if (vResultParcial) {
                vResult = (int)eMensajeCopiarParametros.Copia_Exitosa;
            } else {
                vResult = (int)eMensajeCopiarParametros.Error_Al_Copiar_Parametros;
            }
            return vResult;
        }

        private bool BuscarEInsertaCopiaDeParametros(int valConsecutivoCompaniaOrigen, int valConsecutivoCompaniaDestino) {
            bool vResult = false;
            XElement vSettValueByCompany = GetSettValueByCompany(valConsecutivoCompaniaOrigen);
            List<SettValueByCompany> vListaSettValueByCompany = ReemplazarConsecutivoCompania(vSettValueByCompany, valConsecutivoCompaniaDestino);
            ILibDataComponent<IList<SettValueByCompany>, IList<SettValueByCompany>> instanciaDal = new Galac.Saw.Dal.SttDef.clsSettValueByCompanyDat();
            vResult = instanciaDal.Insert(vListaSettValueByCompany).Success;
            return vResult;
        }

        private bool EliminarParametrosAntesDeCopiar(int valConsecutivoCompania) {
            bool vResult = false;
            StringBuilder vSql = new StringBuilder();
            LibGpParams vParametros = new LibGpParams();
            vParametros.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vSql.AppendLine("DELETE FROM Comun.SettValueByCompany ");
            vSql.AppendLine("WHERE ConsecutivoCompania = @ConsecutivoCompania");
            vResult = LibBusiness.ExecuteUpdateOrDelete(vSql.ToString(), vParametros.Get(), string.Empty, 0) >= 0;
            return vResult;
        }

        List<SettValueByCompany> ReemplazarConsecutivoCompania(XElement valItem, int valConsecutivoCompania) {
            List<SettValueByCompany> vResult = new List<SettValueByCompany>();
            var vEnty = from vData in valItem.Descendants("GpResult")
                        select vData;
            foreach (XElement vItem in vEnty) {
                SettValueByCompany insSettValueByCompany = new SettValueByCompany();
                insSettValueByCompany.NameSettDefinition = LibString.Trim(vItem.Element("NameSettDefinition").Value);
                insSettValueByCompany.ConsecutivoCompania = valConsecutivoCompania;
                insSettValueByCompany.Module = LibString.Trim(vItem.Element("DisplayName").Value);
                insSettValueByCompany.GroupName = LibString.Trim(vItem.Element("GroupName").Value);
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Value"), null))) {
                    insSettValueByCompany.Value = LibString.Trim(vItem.Element("Value").Value);
                } else {
                    insSettValueByCompany.Value = "";
                }
                vResult.Add(insSettValueByCompany);
            }
            return vResult;
        }

        private bool TieneParametrosCompletos(int valConsecutivoCompania) {
            bool vResult = false;
            int vCantidadParametrosSistema = 0;
            int vCantidadParametrosCompania = 0;

            vCantidadParametrosSistema = new Galac.Saw.Brl.SttDef.clsSettDefinitionNav().GetTotalParametrosAdministrativos();
            vCantidadParametrosCompania = GetTotalParametrosCompania(valConsecutivoCompania);

            if (vCantidadParametrosSistema == vCantidadParametrosCompania) {
                vResult = true;
            }
            return vResult;
        }

        private int GetTotalParametrosCompania(int valConsecutivoCompania) {
            int vResult = 0;
            StringBuilder vSql = new StringBuilder();
            LibGpParams vParametro = new LibGpParams();
            vParametro.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vSql.AppendLine("Select COUNT(*) AS Cantidad FROM Comun.Gv_SettValueByCompany_B1 WHERE ConsecutivoCompania = @ConsecutivoCompania");
            ILibDataComponent<IList<SettValueByCompany>, IList<SettValueByCompany>> instanciaDal = GetDataInstance();
            XElement vResulset = instanciaDal.QueryInfo(eProcessMessageType.Query, vSql.ToString(), vParametro.Get());
            vResulset = instanciaDal.QueryInfo(eProcessMessageType.Query, vSql.ToString(), vParametro.Get());
            vResult = (from vRecord in vResulset.Descendants("GpResult")
                       select new {
                           Cantidad = LibConvert.ToInt(vRecord.Element("Cantidad"))
                       }).FirstOrDefault().Cantidad;
            return vResult;
        }

        string ISettValueByCompanyPdn.SiguienteNumeroDocumentoAntesDeImprentaDigital(eTipoDocumentoFactura valTipoDeDocumento, eTalonario valTalonarioEnum, eTipoDePrefijo valTipoPrefijo) {
            string vResult;
            string vMaximo = "1";
            int vLastNumero = 1;
            int vPosicionDeInicioDelConsecutivo = 0;
            eTipoDePrefijo vTipoPrefijo = TipoDePrefijoSegunTalonarioAntesDeID(valTipoDeDocumento, valTalonarioEnum);
            int vPrimeraFactura = LibConvert.ToInt(PrimeraFacturaSegunTalonarioAntesDeID(valTipoDeDocumento, valTalonarioEnum));
            string vSql = SqlSiguienteNumeroDocumentoAntesDeImprentaDigital(valTipoDeDocumento, valTalonarioEnum, vTipoPrefijo);
            XElement vElement = LibBusiness.ExecuteSelect(vSql, new StringBuilder(), string.Empty, 0);
            if (vElement != null) {
                vMaximo = LibConvert.ToStr(vElement.Descendants().Select(m => (string)m.Element("Maximo")).FirstOrDefault());
                if (!LibString.IsNullOrEmpty(vMaximo)) {
                    if ((valTipoDeDocumento == eTipoDocumentoFactura.Factura) && (vTipoPrefijo == eTipoDePrefijo.SinPrefijo)) {
                    } else if (vTipoPrefijo == eTipoDePrefijo.Ano) {
                        if (valTipoDeDocumento != eTipoDocumentoFactura.Factura) {
                            vPosicionDeInicioDelConsecutivo = 4;//NC- ND- NE- +1
                        }
                        vPosicionDeInicioDelConsecutivo += 4;//yyyy
                        vMaximo = LibString.Mid(vMaximo, vPosicionDeInicioDelConsecutivo, LibString.Len(vMaximo));
                    } else {//(valTipoPrefijo == eTipoDePrefijo.Indicar)
                        string vPrefijo = PrefijoAntesDeID(valTipoDeDocumento, valTalonarioEnum, valTipoPrefijo);
                        vPosicionDeInicioDelConsecutivo = LibString.Len(vPrefijo) + 1;
                        vMaximo = LibString.Mid(vMaximo, vPosicionDeInicioDelConsecutivo, LibString.Len(vMaximo));
                    }
                }
                vLastNumero = LibConvert.ToInt(vMaximo) + 1;
                if (vLastNumero < vPrimeraFactura) {
                    vLastNumero = vPrimeraFactura;
                }
            } else if (vPrimeraFactura > 1) {
                vLastNumero = vPrimeraFactura;
            }
            vResult = LibText.FillWithCharToLeft(LibConvert.ToStr(vLastNumero), "0", 10);
            return vResult;
        }

        private string SqlSiguienteNumeroDocumentoAntesDeImprentaDigital(eTipoDocumentoFactura valTipoDeDocumento, eTalonario valTalonarioEnum, eTipoDePrefijo valTipoPrefijo) {
            QAdvSql insSql = new QAdvSql("");
            StringBuilder vSql = new StringBuilder();
            int vConsecutivoCompania = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("Compania", "ConsecutivoCompania");
            if ((valTipoDeDocumento == eTipoDocumentoFactura.Factura) && (valTipoPrefijo == eTipoDePrefijo.SinPrefijo)) {
                vSql.AppendLine("SELECT MAX(" + insSql.ToDbl("Numero") + ") AS Maximo ");
                vSql.AppendLine("FROM Factura");
                vSql.AppendLine("WHERE ConsecutivoCompania = " + insSql.ToSqlValue(vConsecutivoCompania));
                vSql.AppendLine("AND TipoDeDocumento = " + insSql.EnumToSqlValue((int)valTipoDeDocumento));
                vSql.AppendLine("AND StatusFactura IN ('0','1')");
                vSql.AppendLine("AND " + insSql.Mid("Numero", 1, 1) + " = '0'");
                vSql.AppendLine("AND Talonario = " + insSql.EnumToSqlValue((int)valTalonarioEnum));
            } else {
                string vPrefijo = PrefijoAntesDeID(valTipoDeDocumento, valTalonarioEnum, valTipoPrefijo);

                vSql.AppendLine("SELECT MAX(Numero) AS Maximo ");
                vSql.AppendLine("FROM Factura");
                vSql.AppendLine("WHERE ConsecutivoCompania = " + insSql.ToSqlValue(vConsecutivoCompania));
                vSql.AppendLine("AND TipoDeDocumento = " + insSql.EnumToSqlValue((int)valTipoDeDocumento));
                vSql.AppendLine("AND StatusFactura IN ('0','1')");
                vSql.AppendLine("AND Numero LIKE '" + vPrefijo + insSql.ComodinSymbol() + "'");
                if (valTipoDeDocumento == eTipoDocumentoFactura.Factura) {
                    vSql.AppendLine("AND Talonario = " + insSql.EnumToSqlValue((int)valTalonarioEnum));
                }
            }
            return vSql.ToString();
        }

        private string PrefijoAntesDeID(eTipoDocumentoFactura valTipoDeDocumento, eTalonario valTalonarioEnum, eTipoDePrefijo valTipoDePrefijo) {
            string vResult = string.Empty;
            if (valTipoDeDocumento == eTipoDocumentoFactura.Factura) {
                if (valTipoDePrefijo == eTipoDePrefijo.Ano) {
                    vResult = LibConvert.ToStr(LibDate.Today().Year);
                } else if (valTipoDePrefijo == eTipoDePrefijo.Indicar) {
                    vResult = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "PrefijoTalonario1");
                    if (valTalonarioEnum == eTalonario.Talonario2) {
                        vResult = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "PrefijoTalonario2");
                    }
                }
            } else {
                if (valTipoDeDocumento == eTipoDocumentoFactura.NotaDeDebito) {
                    vResult = "ND-";
                    bool vUsaPreNumerada = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "NDPreNumerada");
                    if (!vUsaPreNumerada) {
                        if (valTipoDePrefijo == eTipoDePrefijo.Ano) {
                            vResult += LibConvert.ToStr(LibDate.Today().Year);
                        } else if (valTipoDePrefijo == eTipoDePrefijo.Indicar) {
                            vResult += LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "PrefijoND");
                        }
                    }
                } else if (valTipoDeDocumento == eTipoDocumentoFactura.NotaDeCredito) {
                    vResult = "NC-";
                    bool vUsaPreNumerada = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "NCPreNumerada");
                    if (!vUsaPreNumerada) {
                        if (valTipoDePrefijo == eTipoDePrefijo.Ano) {
                            vResult += LibConvert.ToStr(LibDate.Today().Year);
                        } else if (valTipoDePrefijo == eTipoDePrefijo.Indicar) {
                            vResult += LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "PrefijoNC");
                        }
                    }
                } else if (valTipoDeDocumento == eTipoDocumentoFactura.NotaEntrega) {
                    vResult = "NE-";
                    bool vUsaPreNumerada = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "NEPreNumerada");
                    if (!vUsaPreNumerada) {
                        if (valTipoDePrefijo == eTipoDePrefijo.Ano) {
                            vResult += LibConvert.ToStr(LibDate.Today().Year);
                        } else if (valTipoDePrefijo == eTipoDePrefijo.Indicar) {
                            vResult += LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "PrefijoNE");
                        }
                    }
                }
            }
            return vResult;
        }

        private eTipoDePrefijo TipoDePrefijoSegunTalonarioAntesDeID(eTipoDocumentoFactura valTipoDeDocumento, eTalonario valTalonarioEnum) {
            eTipoDePrefijo vResult = eTipoDePrefijo.SinPrefijo;
            if (valTipoDeDocumento == eTipoDocumentoFactura.Factura) {
                vResult = (eTipoDePrefijo)LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetEnum("Parametros", "TipoDePrefijoTalonario1");
                if (valTalonarioEnum == eTalonario.Talonario2) {
                    vResult = (eTipoDePrefijo)LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetEnum("Parametros", "TipoDePrefijoTalonario2");
                }
            } else if (valTipoDeDocumento == eTipoDocumentoFactura.NotaDeDebito) {
                vResult = (eTipoDePrefijo)LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetEnum("Parametros", "TipoDePrefijoND");
            } else if (valTipoDeDocumento == eTipoDocumentoFactura.NotaDeCredito) {
                vResult = (eTipoDePrefijo)LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetEnum("Parametros", "TipoDePrefijoNC");
            } else if (valTipoDeDocumento == eTipoDocumentoFactura.NotaEntrega) {
                vResult = (eTipoDePrefijo)LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetEnum("Parametros", "TipoPrefijoNE");
            }
            return vResult;
        }

        private string PrimeraFacturaSegunTalonarioAntesDeID(eTipoDocumentoFactura valTipoDeDocumento, eTalonario valTalonarioEnum) {
            string vResult = "1";
            if (valTipoDeDocumento == eTipoDocumentoFactura.Factura) {
                vResult = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "PrimeraFacturaTalonario1");
                if (valTalonarioEnum == eTalonario.Talonario2) {
                    vResult = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "PrimeraFacturaTalonario2");
                }
            } else if (valTipoDeDocumento == eTipoDocumentoFactura.NotaDeDebito) {
                vResult = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "PrimeraND");
            } else if (valTipoDeDocumento == eTipoDocumentoFactura.NotaDeCredito) {
                vResult = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "PrimeraNC");
            } else if (valTipoDeDocumento == eTipoDocumentoFactura.NotaEntrega) {
                vResult = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "PrimeraNE");
            }
            return vResult;
        }

        void ISettValueByCompanyPdn.ConfiguracionImprentaDigitalPorTipoDeDocumentoFactura(eTipoDocumentoFactura valTipoDeDocumento, string valPrimerNumeroTalonario1) {
            QAdvSql insSql = new QAdvSql("");
            if (valTipoDeDocumento == eTipoDocumentoFactura.Factura) {
                //talonario 1                
                LibBusiness.ExecuteUpdateOrDelete(SqlUpdateComunSettValueByCompanyParaID("ModeloDeFactura", insSql.EnumToSqlValue((int)eModeloDeFactura.eMD_FORMALIBRE)), (new LibGpParams()).Get(), string.Empty, 0);//modelo de factura -> formalibre
                LibBusiness.ExecuteUpdateOrDelete(SqlUpdateComunSettValueByCompanyParaID("NombrePlantillaFactura", insSql.ToSqlValue("rpxFacturaFormatoLibre")), (new LibGpParams()).Get(), string.Empty, 0); //plantilla -> ""
                LibBusiness.ExecuteUpdateOrDelete(SqlUpdateComunSettValueByCompanyParaID("FacturaPreNumerada", insSql.ToSqlValue(false)), (new LibGpParams()).Get(), string.Empty, 0);//factura pre-numerada -> no
                LibBusiness.ExecuteUpdateOrDelete(SqlUpdateComunSettValueByCompanyParaID("PrimeraFactura", insSql.ToSqlValue(valPrimerNumeroTalonario1)), (new LibGpParams()).Get(), string.Empty, 0);//primera factura -> valPrimerNumeroTalonario1
                LibBusiness.ExecuteUpdateOrDelete(SqlUpdateComunSettValueByCompanyParaID("TipoDePrefijo", insSql.EnumToSqlValue((int)eTipoDePrefijo.SinPrefijo)), (new LibGpParams()).Get(), string.Empty, 0);//tipo de prefijo -> sin prefijo
                LibBusiness.ExecuteUpdateOrDelete(SqlUpdateComunSettValueByCompanyParaID("Prefijo", insSql.ToSqlValue("")), (new LibGpParams()).Get(), string.Empty, 0);//prefijo -> ""

                //talonario 2 -> se coloca como No usa dos talonarios
                LibBusiness.ExecuteUpdateOrDelete(SqlUpdateComunSettValueByCompanyParaID("ModeloDeFactura2", insSql.EnumToSqlValue((int)eModeloDeFactura.eMD_FORMALIBRE)), (new LibGpParams()).Get(), string.Empty, 0);//modelo de factura -> formalibre
                LibBusiness.ExecuteUpdateOrDelete(SqlUpdateComunSettValueByCompanyParaID("NombrePlantillaFactura2", insSql.ToSqlValue("rpxFacturaFormatoLibre")), (new LibGpParams()).Get(), string.Empty, 0);//plantilla -> ""
                LibBusiness.ExecuteUpdateOrDelete(SqlUpdateComunSettValueByCompanyParaID("FacturaPreNumerada2", insSql.ToSqlValue(false)), (new LibGpParams()).Get(), string.Empty, 0);//factura pre-numerada -> no
                LibBusiness.ExecuteUpdateOrDelete(SqlUpdateComunSettValueByCompanyParaID("PrimeraFactura2", insSql.ToSqlValue("")), (new LibGpParams()).Get(), string.Empty, 0);//primera factura -> ""
                LibBusiness.ExecuteUpdateOrDelete(SqlUpdateComunSettValueByCompanyParaID("TipoDePrefijo2", insSql.EnumToSqlValue((int)eTipoDePrefijo.SinPrefijo)), (new LibGpParams()).Get(), string.Empty, 0);//tipo de prefijo -> sin prefijo
                LibBusiness.ExecuteUpdateOrDelete(SqlUpdateComunSettValueByCompanyParaID("Prefijo2", insSql.ToSqlValue("")), (new LibGpParams()).Get(), string.Empty, 0);//prefijo -> ""
            } else if (valTipoDeDocumento == eTipoDocumentoFactura.NotaDeCredito) {
                LibBusiness.ExecuteUpdateOrDelete(SqlUpdateComunSettValueByCompanyParaID("NombrePlantillaNotaDeCredito", insSql.ToSqlValue("rpxNotaDeCreditoFormatoLibre")), (new LibGpParams()).Get(), string.Empty, 0);//plantilla -> "rpxNotaDeCreditoFormatoLibre"
                LibBusiness.ExecuteUpdateOrDelete(SqlUpdateComunSettValueByCompanyParaID("NCPreNumerada", insSql.ToSqlValue(false)), (new LibGpParams()).Get(), string.Empty, 0);//NC pre-numerada -> no
                LibBusiness.ExecuteUpdateOrDelete(SqlUpdateComunSettValueByCompanyParaID("PrimeraNotaDeCredito", insSql.ToSqlValue(valPrimerNumeroTalonario1)), (new LibGpParams()).Get(), string.Empty, 0);//primera NC -> valPrimerNumeroTalonario1
                LibBusiness.ExecuteUpdateOrDelete(SqlUpdateComunSettValueByCompanyParaID("TipoDePrefijoNC", insSql.EnumToSqlValue((int)eTipoDePrefijo.SinPrefijo)), (new LibGpParams()).Get(), string.Empty, 0);//tipo de prefijo -> sin prefijo
                LibBusiness.ExecuteUpdateOrDelete(SqlUpdateComunSettValueByCompanyParaID("PrefijoNC", insSql.ToSqlValue("")), (new LibGpParams()).Get(), string.Empty, 0);//prefijo -> ""
            } else if (valTipoDeDocumento == eTipoDocumentoFactura.NotaDeDebito) {
                LibBusiness.ExecuteUpdateOrDelete(SqlUpdateComunSettValueByCompanyParaID("NombrePlantillaNotaDeDebito", insSql.ToSqlValue("rpxNotaDeDebitoFormatoLibre")), (new LibGpParams()).Get(), string.Empty, 0);//plantilla -> "rpxNotaDeDebitoFormatoLibre"
                LibBusiness.ExecuteUpdateOrDelete(SqlUpdateComunSettValueByCompanyParaID("NDPreNumerada", insSql.ToSqlValue(false)), (new LibGpParams()).Get(), string.Empty, 0);//NC pre-numerada -> no
                LibBusiness.ExecuteUpdateOrDelete(SqlUpdateComunSettValueByCompanyParaID("PrimeraNotaDeDebito", insSql.ToSqlValue(valPrimerNumeroTalonario1)), (new LibGpParams()).Get(), string.Empty, 0);//primera ND -> valPrimerNumeroTalonario1
                LibBusiness.ExecuteUpdateOrDelete(SqlUpdateComunSettValueByCompanyParaID("TipoDePrefijoND", insSql.ToSqlValue((int)eTipoDePrefijo.SinPrefijo)), (new LibGpParams()).Get(), string.Empty, 0);//tipo de prefijo -> sin prefijo
                LibBusiness.ExecuteUpdateOrDelete(SqlUpdateComunSettValueByCompanyParaID("PrefijoND", insSql.ToSqlValue("")), (new LibGpParams()).Get(), string.Empty, 0);//prefijo -> ""
            } else if (valTipoDeDocumento == eTipoDocumentoFactura.NotaEntrega) {
                LibBusiness.ExecuteUpdateOrDelete(SqlUpdateComunSettValueByCompanyParaID("ModeloNotaEntrega", insSql.EnumToSqlValue((int)eModeloDeFactura.eMD_FORMALIBRE)), (new LibGpParams()).Get(), string.Empty, 0);//modelo de NE -> formalibre
                LibBusiness.ExecuteUpdateOrDelete(SqlUpdateComunSettValueByCompanyParaID("NombrePlantillaNotaEntrega", insSql.ToSqlValue("")), (new LibGpParams()).Get(), string.Empty, 0);//plantilla -> ""
                LibBusiness.ExecuteUpdateOrDelete(SqlUpdateComunSettValueByCompanyParaID("NotaEntregaPreNumerada", insSql.ToSqlValue(false)), (new LibGpParams()).Get(), string.Empty, 0);//NC pre-numerada -> no
                LibBusiness.ExecuteUpdateOrDelete(SqlUpdateComunSettValueByCompanyParaID("PrimeraNotaEntrega", insSql.ToSqlValue(valPrimerNumeroTalonario1)), (new LibGpParams()).Get(), string.Empty, 0);//primera NE -> valPrimerNumeroTalonario1
                LibBusiness.ExecuteUpdateOrDelete(SqlUpdateComunSettValueByCompanyParaID("TipoPrefijoNotaEntrega", insSql.ToSqlValue((int)eTipoDePrefijo.SinPrefijo)), (new LibGpParams()).Get(), string.Empty, 0);//tipo de prefijo -> sin prefijo
                LibBusiness.ExecuteUpdateOrDelete(SqlUpdateComunSettValueByCompanyParaID("PrefijoNotaEntrega", insSql.ToSqlValue("")), (new LibGpParams()).Get(), string.Empty, 0);//prefijo -> ""
            }
        }

        string SqlUpdateComunSettValueByCompanyParaID(string valNameSettDefinition, string valValue) {
            int vConsecutivoCompania = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("Compania", "ConsecutivoCompania");
            QAdvSql insSql = new QAdvSql("");
            StringBuilder vSql = new StringBuilder();
            vSql.AppendLine("UPDATE Comun.SettValueByCompany");
            vSql.AppendLine("   SET Value = " + valValue);
            vSql.AppendLine(" WHERE (ConsecutivoCompania = " + insSql.ToSqlValue(vConsecutivoCompania) + ") AND (NameSettDefinition = " + insSql.ToSqlValue(valNameSettDefinition) + " )");
            return vSql.ToString();
        }

        void ISettValueByCompanyPdn.MoverDocumentosDeTalonario(eTipoDocumentoFactura valTipoDeDocumento, eTalonario valTalonarioOrigen, eTalonario valTalonarioDestino) {
            int vConsecutivoCompania = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("Compania", "ConsecutivoCompania");
            StringBuilder vSql = new StringBuilder();
            QAdvSql insSql = new QAdvSql("");
            vSql.AppendLine("UPDATE factura");
            vSql.AppendLine("   SET Talonario = " + insSql.EnumToSqlValue((int)valTalonarioDestino));
            vSql.AppendLine(" WHERE (ConsecutivoCompania = " + insSql.ToSqlValue(vConsecutivoCompania) + ") AND (Talonario = " + insSql.EnumToSqlValue((int)valTalonarioOrigen) + ")");
            LibBusiness.ExecuteUpdateOrDelete(vSql.ToString(), (new LibGpParams()).Get(), string.Empty, 0);
        }

        void ISettValueByCompanyPdn.ConfigurarImprentaDigital(eProveedorImprentaDigital valProveedorImprentaDigital, DateTime valFechaDeInicioDeUsoDeImprentaDigital) {
            int vConsecutivoCompania = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("Compania", "ConsecutivoCompania");
            StringBuilder vSql = new StringBuilder();
            QAdvSql insSql = new QAdvSql("");
            vSql.AppendLine("UPDATE Comun.SettValueByCompany");
            vSql.AppendLine("   SET Value = " + insSql.ToSqlValue(true));
            vSql.AppendLine(" WHERE (ConsecutivoCompania = " + insSql.ToSqlValue(vConsecutivoCompania) + ") AND (NameSettDefinition = " + insSql.ToSqlValue("UsaImprentaDigital") + " )");
            LibBusiness.ExecuteUpdateOrDelete(vSql.ToString(), (new LibGpParams()).Get(), string.Empty, 0);

            vSql.AppendLine("UPDATE Comun.SettValueByCompany");
            vSql.AppendLine("   SET Value = " + insSql.ToSqlValue(valFechaDeInicioDeUsoDeImprentaDigital));
            vSql.AppendLine(" WHERE (ConsecutivoCompania = " + insSql.ToSqlValue(vConsecutivoCompania) + ") AND (NameSettDefinition = " + insSql.ToSqlValue("FechaInicioImprentaDigital") + " )");
            LibBusiness.ExecuteUpdateOrDelete(vSql.ToString(), (new LibGpParams()).Get(), string.Empty, 0);

            vSql.AppendLine("UPDATE Comun.SettValueByCompany");
            vSql.AppendLine("   SET Value = " + insSql.EnumToSqlValue((int)valProveedorImprentaDigital));
            vSql.AppendLine(" WHERE (ConsecutivoCompania = " + insSql.ToSqlValue(vConsecutivoCompania) + ") AND (NameSettDefinition = " + insSql.ToSqlValue("ProveedorImprentaDigital") + " )");
            LibBusiness.ExecuteUpdateOrDelete(vSql.ToString(), (new LibGpParams()).Get(), string.Empty, 0);

            string vNumeroDeDigitosEnFactura = "11";
            if (valProveedorImprentaDigital == eProveedorImprentaDigital.TheFactoryHKA) {
                vNumeroDeDigitosEnFactura = "8";
            }
            vSql.AppendLine("UPDATE Comun.SettValueByCompany");
            vSql.AppendLine("   SET Value = " + insSql.ToSqlValue(vNumeroDeDigitosEnFactura));
            vSql.AppendLine(" WHERE (ConsecutivoCompania = " + insSql.ToSqlValue(vConsecutivoCompania) + ") AND (NameSettDefinition = " + insSql.ToSqlValue("NumeroDeDigitosEnFactura") + " )");
            LibBusiness.ExecuteUpdateOrDelete(vSql.ToString(), (new LibGpParams()).Get(), string.Empty, 0);

            //hasta nuevo aviso
            vSql.AppendLine("UPDATE Comun.SettValueByCompany");
            vSql.AppendLine("   SET Value = " + insSql.ToSqlValue(false));
            vSql.AppendLine(" WHERE (ConsecutivoCompania = " + insSql.ToSqlValue(vConsecutivoCompania) + ") AND (NameSettDefinition = " + insSql.ToSqlValue("UsarDosTalonarios") + " )");
            LibBusiness.ExecuteUpdateOrDelete(vSql.ToString(), (new LibGpParams()).Get(), string.Empty, 0);

            //hasta nuevo aviso
            vSql.AppendLine("UPDATE Comun.SettValueByCompany");
            vSql.AppendLine("   SET Value = " + insSql.ToSqlValue(false));
            vSql.AppendLine(" WHERE (ConsecutivoCompania = " + insSql.ToSqlValue(vConsecutivoCompania) + ") AND (NameSettDefinition = " + insSql.ToSqlValue("UsaNotaEntrega") + " )");
            LibBusiness.ExecuteUpdateOrDelete(vSql.ToString(), (new LibGpParams()).Get(), string.Empty, 0);
        }

        bool ISettValueByCompanyPdn.SonValidosLosSiguienteNumerosDeDocumentosParaImprentaDigital(string valPrimerNumeroFacturaT1, string valPrimerNumeroNotaDeCredito, string valPrimerNumeroNotaDeDebito, out StringBuilder outMessage) {
            outMessage = new StringBuilder();
            bool vResult = true;
            int vConsecutivoCompania = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("Compania", "ConsecutivoCompania");
            string vSql;
            LibDatabase insDb = new LibDatabase();
            vSql = "SELECT Numero FROM factura WHERE ConsecutivoCompania = " + insDb.InsSql.ToSqlValue(vConsecutivoCompania) + " AND TipoDeDocumento = " + insDb.InsSql.EnumToSqlValue((int)eTipoDocumentoFactura.Factura) + " AND Numero = " + insDb.InsSql.ToSqlValue(valPrimerNumeroFacturaT1);
            if (insDb.RecordCountOfSql(vSql) > 0) {
                vResult = false;
                outMessage.AppendLine("El Número de Factura: " + valPrimerNumeroFacturaT1 + " ya existe. No puede ser utilizado como primer Número de Factura para Imprenta Digital");
            }

            vSql = "SELECT Numero FROM factura WHERE ConsecutivoCompania = " + insDb.InsSql.ToSqlValue(vConsecutivoCompania) + " AND TipoDeDocumento = " + insDb.InsSql.EnumToSqlValue((int)eTipoDocumentoFactura.NotaDeCredito) + " AND Numero = " + insDb.InsSql.ToSqlValue(valPrimerNumeroNotaDeCredito);
            if (insDb.RecordCountOfSql(vSql) > 0) {
                vResult = false;
                outMessage.AppendLine("El Número de Nota de Crédito: " + valPrimerNumeroFacturaT1 + " ya existe. No puede ser utilizado como primer Número de Nota de Crédito para Imprenta Digital");
            }

            vSql = "SELECT Numero FROM factura WHERE ConsecutivoCompania = " + insDb.InsSql.ToSqlValue(vConsecutivoCompania) + " AND TipoDeDocumento = " + insDb.InsSql.EnumToSqlValue((int)eTipoDocumentoFactura.NotaDeDebito) + " AND Numero = " + insDb.InsSql.ToSqlValue(valPrimerNumeroNotaDeDebito);
            if (insDb.RecordCountOfSql(vSql) > 0) {
                vResult = false;
                outMessage.AppendLine("El Número de Nota de Débito: " + valPrimerNumeroFacturaT1 + " ya existe. No puede ser utilizado como primer Número de Nota de Débito para Imprenta Digital");
            }
            return vResult;
        }

        void ISettValueByCompanyPdn.GuardarDatosImprentaDigitalAppSettings(eProveedorImprentaDigital valProveedor, string valUsuario, string valClave, string valUrl) {
            string vCampoUsuario = string.Empty;
            string vCampoClave = string.Empty;
            bool vContinuar = false;
            if (valProveedor == eProveedorImprentaDigital.TheFactoryHKA) {
                vCampoUsuario = "usuario";
                vCampoClave = "clave";
                vContinuar = true;
            }
            if (vContinuar) {
                clsImprentaDigitalSettings insIDStt = new clsImprentaDigitalSettings() {
                    DireccionURL = LibString.Trim(valUrl),
                    CampoUsuario = LibString.Trim(vCampoUsuario),
                    CampoClave = LibString.Trim(vCampoClave),
                    Usuario = LibString.Trim(valUsuario),
                    Clave = LibString.Trim(LibCryptography.SymEncryptDES(valClave))
                };
                insIDStt.ActualizarValores();
            }
        }

        ObservableCollection<string> ISettValueByCompanyPdn.ListaDeUsuariosSupervisoresActivos() {
            ObservableCollection<string> vResult = new ObservableCollection<string>();
            string vSql = "SELECT UserName FROM Lib.GUser WHERE Status = '0' AND IsSuperviser = 'S' ORDER BY UserName";
            XElement vResultSet = LibBusiness.ExecuteSelect(vSql, null, "", 0);
            if (vResultSet != null) {
                var vEntity = from vRecord in vResultSet.Descendants("GpResult") select vRecord;
                foreach (XElement vItem in vEntity) {
                    if (vItem != null) {
                        vResult.Add(LibConvert.ToStr(vItem.Element("UserName").Value));
                    }
                }
            }
            return vResult;
        }

        bool ISettValueByCompanyPdn.EjecutaConexionConGVentas(int valConsecutivoCompania, string valParametroSuscripcionGVentas, string valSerialConectorGVentas, string valNombreCompaniaAdmin, string valNombreUsuarioOperaciones, eAccionSR valAction) {
            try {
                bool vResult = false;
                LibWebConnector.clsSuscripcion insSuscripcion = new LibWebConnector.clsSuscripcion();
                string vDatabaseName = LibServiceInfo.GetDatabaseName();
                string vServerName = LibServiceInfo.GetServerName();
                int vGuionSeparador = LibString.IndexOf(valNombreCompaniaAdmin, '|') + 1;
                string vRIFCompaniaGVentas = LibString.Trim(LibString.SubString(valNombreCompaniaAdmin, vGuionSeparador + 1));
                valNombreCompaniaAdmin = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Compania", "Nombre");
                if (insSuscripcion.ActivarDesactivarConexionGVentas(valConsecutivoCompania, valSerialConectorGVentas, vRIFCompaniaGVentas, valNombreCompaniaAdmin, valNombreUsuarioOperaciones, vDatabaseName, vServerName, valAction)) {
                    if (valAction == eAccionSR.Activar) {
                        ActualizaValoresEnAdministrativo(valConsecutivoCompania, valParametroSuscripcionGVentas, valSerialConectorGVentas, valNombreCompaniaAdmin, valAction);
                    } else if (valAction == eAccionSR.Desactivar) {
                        ActualizaValoresEnAdministrativo(valConsecutivoCompania, "", "", "", valAction);
                    }
                    vResult = true;
                }
                return vResult;
            } catch (Exception vEx) {
                throw new GalacException(vEx.Message, eExceptionManagementType.Controlled);
            }
        }

        private void ActualizaValoresEnAdministrativo(int valConsecutivoCompania, string valParametroSuscripcionGVentas, string valSerialConectorGVentas, string valNumeroIDGVentas, eAccionSR valAction) {
            QAdvSql insSql = new QAdvSql("");
            string vActivaDesactivaCompania = (valAction == eAccionSR.Activar ? insSql.ToSqlValue(true) : insSql.ToSqlValue(false));
            string vSql = "UPDATE COMPANIA SET ConectadaConG360 = " + vActivaDesactivaCompania + " WHERE ConsecutivoCompania = " + insSql.ToSqlValue(valConsecutivoCompania);
            LibBusiness.ExecuteUpdateOrDelete(vSql, null, "", 0);
            vSql = "UPDATE Comun.SettValueByCompany SET Value = " + insSql.ToSqlValue(valParametroSuscripcionGVentas) + " WHERE ConsecutivoCompania = " + insSql.ToSqlValue(valConsecutivoCompania) + " AND NameSettDefinition = " + insSql.ToSqlValue("SuscripcionGVentas");
            LibBusiness.ExecuteUpdateOrDelete(vSql, null, "", 0);
            vSql = "UPDATE Comun.SettValueByCompany SET Value = " + insSql.ToSqlValue(valSerialConectorGVentas) + " WHERE ConsecutivoCompania = " + insSql.ToSqlValue(valConsecutivoCompania) + " AND NameSettDefinition =" + insSql.ToSqlValue("SerialConectorGVentas");
            LibBusiness.ExecuteUpdateOrDelete(vSql, null, "", 0);
            vSql = "UPDATE Comun.SettValueByCompany SET Value = " + insSql.ToSqlValue(valNumeroIDGVentas) + " WHERE ConsecutivoCompania = " + insSql.ToSqlValue(valConsecutivoCompania) + " AND NameSettDefinition =" + insSql.ToSqlValue("NumeroIDGVentas");
            LibBusiness.ExecuteUpdateOrDelete(vSql, null, "", 0);
        }

        bool ISettValueByCompanyPdn.ExistenArticulosMercanciaNoSimpleNoLoteFDV(int valConsecutivoCompania) {
            bool vResult = false;
            QAdvSql insSql = new QAdvSql("");
            string vSql = "SELECT COUNT(*) AS CantidadArticulos FROM articuloInventario WHERE TipoDeArticulo = '0' AND TipoArticuloInv IN ('1', '2', '3', '4') AND ConsecutivoCompania = " + insSql.ToSqlValue(valConsecutivoCompania);
            XElement vCountArtMercanciaNoSimpleNoLoteFdV = LibBusiness.ExecuteSelect(vSql, new StringBuilder(), string.Empty, 0);
            if (vCountArtMercanciaNoSimpleNoLoteFdV != null) {
                int vCount = LibConvert.ToInt(vCountArtMercanciaNoSimpleNoLoteFdV.Descendants().Select(s => s.Element("CantidadArticulos")).FirstOrDefault());
                vResult = vCount > 0;
            }
            return vResult;
        }

        bool ISettValueByCompanyPdn.ExistenArticulosLoteFdV(int valConsecutivoCompania) {
            bool vResult = false;
            QAdvSql insSql = new QAdvSql("");
            string vSql = "SELECT COUNT(*) AS CantidadArticulos FROM articuloInventario WHERE TipoDeArticulo = '0' AND TipoArticuloInv IN ('5') AND ConsecutivoCompania = " + insSql.ToSqlValue(valConsecutivoCompania);
            XElement vCountArtMercanciaNoSimpleNoLoteFdV = LibBusiness.ExecuteSelect(vSql, new StringBuilder(), string.Empty, 0);
            if (vCountArtMercanciaNoSimpleNoLoteFdV != null) {
                int vCount = LibConvert.ToInt(vCountArtMercanciaNoSimpleNoLoteFdV.Descendants().Select(s => s.Element("CantidadArticulos")).FirstOrDefault());
                vResult = vCount > 0;
            }
            return vResult;
        }
    } //End of class clsSettValueByCompanyNav
} //End of namespace Galac.Saw.Brl.PrdStt