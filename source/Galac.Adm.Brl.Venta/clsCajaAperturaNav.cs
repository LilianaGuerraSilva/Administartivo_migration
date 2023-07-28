using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security;
using System.Security.Permissions;
using System.Xml;
using System.Xml.Linq;
using LibGalac.Aos.Base;
using LibGalac.Aos.Brl;
using LibGalac.Aos.Base.Dal;
using Galac.Adm.Ccl.Venta;
using Galac.Saw.Lib;
using Galac.Comun.Ccl.TablasGen;
using Galac.Comun.Brl.TablasGen;

namespace Galac.Adm.Brl.Venta {
    public partial class clsCajaAperturaNav:LibBaseNav<IList<CajaApertura>,IList<CajaApertura>>, ICajaAperturaPdn {
        #region Variables
        QAdvSql insSql;
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores

        public clsCajaAperturaNav() {
            insSql = new QAdvSql("");
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override ILibDataComponentWithSearch<IList<CajaApertura>,IList<CajaApertura>> GetDataInstance() {
            return new Galac.Adm.Dal.Venta.clsCajaAperturaDat();
        }
        #region Miembros de ILibPdn

        bool ILibPdn.CanBeChoosen(string valCallingModule,eAccionSR valAction,string valExtendedAction,XmlDocument valXmlRow) {
            bool vResult = false;
            switch(valCallingModule) {
                default:
                    vResult = true;
                    break;
            }
            return vResult;
        }

        bool ILibPdn.GetDataForList(string valCallingModule,ref XmlDocument refXmlDocument,StringBuilder valXmlParamsExpression) {            
            LibGpParams vParamsInt = new LibGpParams();
            ILibDataFKSearch instanciaDal = new Galac.Adm.Dal.Venta.clsCajaAperturaDat();
            valXmlParamsExpression.Replace("CajaAsignada","CajaCerrada");
            valXmlParamsExpression.Replace("CajaCerradaStr","CajaCerrada");
            valXmlParamsExpression.Replace("FechaUltimaModificacion","Gv_CajaApertura_B1.FechaUltimaModificacion");
            valXmlParamsExpression.Replace("HoraAperturaDt","HoraApertura");
            valXmlParamsExpression.Replace("HoraCierreDt","HoraCierre");

            //
            //Para Agregar Parametros del CTE_CajaApertura interno del SqlSearch, con el cual se obtiene siempre el ultimo registro para cada CajaAperura
            vParamsInt.AddInInteger("ConsecutivoCompania",LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"));
            string  vParamStr = vParamsInt.Get().ToString();
            vParamStr = vParamStr.Replace("</GpParameters>","");
            vParamStr = vParamStr.Replace("<GpParameters>","");
            vParamStr = vParamStr.Replace("\r\n","");
            int vEnd = valXmlParamsExpression.ToString().IndexOf("</GpParameters>") - 1;            
            valXmlParamsExpression.Insert(vEnd,vParamStr);            
            return instanciaDal.ConnectFk(ref refXmlDocument,eProcessMessageType.SpName,"Adm.Gp_CajaAperturaSCH",valXmlParamsExpression);
        }        

        System.Xml.Linq.XElement ILibPdn.GetFk(string valCallingModule,StringBuilder valParameters) {
            ILibDataComponent<IList<CajaApertura>,IList<CajaApertura>> instanciaDal = new Galac.Adm.Dal.Venta.clsCajaAperturaDat();
            return instanciaDal.QueryInfo(eProcessMessageType.SpName,"Adm.Gp_CajaAperturaGetFk",valParameters);
        }


        bool ICajaAperturaPdn.GetCajaCerrada(int valConsecutivoCompania,int valConsecutivo,bool valCajaCerrada) {            
            bool vResult;
            LibGpParams vParams = new LibGpParams();
            vParams.AddInBoolean("CajaCerrada",valCajaCerrada);
            vParams.AddInInteger("ConsecutivoCompania",valConsecutivoCompania);
            vParams.AddInInteger("ConsecutivoCaja",valConsecutivo);
            vResult = new Galac.Adm.Dal.Venta.clsCajaAperturaDat().CajasCerradas(vParams.Get());            
            return vResult;
        }       

        bool ICajaAperturaPdn.CerrarCaja(CajaApertura valCajaApertura) {
            LibGpParams vParams = new LibGpParams();
            bool vResult = false;
            vParams.AddInInteger("ConsecutivoCompania",valCajaApertura.ConsecutivoCompania);
            vParams.AddInInteger("ConsecutivoCaja",valCajaApertura.ConsecutivoCaja);
            vParams.AddInDateTime("FechaUltimaModificacion",LibDate.Today());
            vParams.AddInString("HoraCierre",valCajaApertura.HoraCierre,5);
            vParams.AddInDecimal("MontoEfectivo",valCajaApertura.MontoEfectivo,4);
            vParams.AddInDecimal("MontoCheque",valCajaApertura.MontoCheque,4);
            vParams.AddInDecimal("MontoTarjeta",valCajaApertura.MontoTarjeta,4);
            vParams.AddInDecimal("MontoDeposito",valCajaApertura.MontoDeposito,4);
            vParams.AddInDecimal("MontoAnticipo",valCajaApertura.MontoAnticipo,4);
            vParams.AddInDecimal("MontoCierre",valCajaApertura.MontoCierre,4);           
            vParams.AddInDecimal("MontoChequeME", valCajaApertura.MontoChequeME, 4);
            vParams.AddInDecimal("MontoTarjetaME", valCajaApertura.MontoTarjetaME, 4);
            vParams.AddInDecimal("MontoDepositoME", valCajaApertura.MontoDepositoME, 4);
            vParams.AddInDecimal("MontoAnticipoME", valCajaApertura.MontoAnticipoME, 4);
            vParams.AddInDecimal("MontoCierreME", valCajaApertura.MontoCierreME, 4);
            vParams.AddInString("CodigoMoneda", valCajaApertura.CodigoMoneda, 4);
            vParams.AddInDecimal("Cambio",valCajaApertura.Cambio,4);            
            vResult = new Galac.Adm.Dal.Venta.clsCajaAperturaDat().CerrarCaja(vParams.Get());
            return vResult;
        }


        bool ICajaAperturaPdn.UsuarioFueAsignado(int valConsecutivoCompania,int valConsecutivoCaja,string valNombreDelUsuario, bool valCajaCerrada,bool valResumenDiario ) {
            LibGpParams vParams = new LibGpParams();
            bool vResult = false;
            vParams.AddInInteger("ConsecutivoCompania",valConsecutivoCompania);            
            vParams.AddInString("NombreDelUsuario",valNombreDelUsuario,20);
            vParams.AddInInteger("ConsecutivoCaja",valConsecutivoCaja);
            vParams.AddInBoolean("CajaCerrada",valCajaCerrada);
            vParams.AddInBoolean("ResumenDiario",valResumenDiario);            
            vResult = new Galac.Adm.Dal.Venta.clsCajaAperturaDat().UsuarioAsignado(vParams.Get());
            return vResult;
        }

        bool ICajaAperturaPdn.AsignarCaja(int ConsecutivoCaja) {
            bool vReq = false;
            try {
                ConfigHelper.AddKeyToAppSettings("CAJALOCAL",LibConvert.ToStr(ConsecutivoCaja,0));
                return vReq;
            } catch(Exception vEx) {
                throw vEx;
            }            
        }       

        #endregion //Miembros de ILibPdn       

        protected override bool RetrieveListInfo(string valModule,ref XmlDocument refXmlDocument,StringBuilder valXmlParamsExpression) {
            bool vResult = false;            
            ILibPdn vPdnModule;               
            switch(valModule) {
                case "Caja Registradora":
                    vPdnModule = new Galac.Adm.Brl.Venta.clsCajaAperturaNav();                   
                    vResult = vPdnModule.GetDataForList("Caja Apertura",ref refXmlDocument,valXmlParamsExpression);
                break;
                case "Caja":
                    vPdnModule = new Galac.Adm.Brl.Venta.clsCajaNav();
                    vResult = vPdnModule.GetDataForList("Caja Apertura",ref refXmlDocument,valXmlParamsExpression);
                    break;
                case "Usuario":
                    vPdnModule = new LibGalac.Aos.Brl.Usal.LibGUserNav();
                    vResult = vPdnModule.GetDataForList("Caja Apertura",ref refXmlDocument,valXmlParamsExpression);
                    break;
                case "Moneda":
                    vPdnModule = new Galac.Comun.Brl.TablasGen.clsMonedaNav();
                    vResult = vPdnModule.GetDataForList("Caja Apertura", ref refXmlDocument, valXmlParamsExpression);
                    break;
				default:
                    vPdnModule = new Galac.Adm.Brl.Venta.clsCajaAperturaNav();
                    vResult = vPdnModule.GetDataForList("Caja Apertura",ref refXmlDocument,valXmlParamsExpression);
                    break;                    
            }
            return vResult;
        }

        protected override void FillWithForeignInfo(ref IList<CajaApertura> refData) {
            //FillWithForeignInfoCajaApertura(ref refData);
        }

        private string vSqlWhereTotalesMontosPorFormaDecobro(int valconsecutivoCompania, int valConsecutivoCaja, string valHoraApertura, string valHoraCierre, bool valEnDivisas) {
            IMonedaLocalActual insMonedaLocalActual = new clsMonedaLocalActual();
            string vCodigoMonedaLocal = insMonedaLocalActual.CodigoMoneda(LibDate.Today());
            string vSqlWhere = "";
            if (valEnDivisas) {
                vSqlWhere = "renglonCobroDeFactura.CodigoMoneda <> " + insSql.ToSqlValue(vCodigoMonedaLocal) +" AND ";
            } else {
                vSqlWhere = "renglonCobroDeFactura.CodigoMoneda = " + insSql.ToSqlValue(vCodigoMonedaLocal) + " AND ";
            }
            vSqlWhere = vSqlWhere + "factura.Fecha BETWEEN CTE_MaxAperturaCaja.Fecha AND " + insSql.ToSqlValue(LibDate.Today());
            vSqlWhere = insSql.SqlIntValueWithOperators(vSqlWhere, "factura.ConsecutivoCaja", valConsecutivoCaja, " AND ", "=");
            vSqlWhere = insSql.SqlIntValueWithOperators(vSqlWhere, "factura.ConsecutivoCompania", valconsecutivoCompania, "", "=");
            vSqlWhere = insSql.SqlValueBetween(vSqlWhere, "factura.HoraModificacion", valHoraApertura, valHoraCierre, "");
            vSqlWhere = insSql.WhereSql(vSqlWhere);
            return vSqlWhere;
        }

        bool ICajaAperturaPdn.TotalesMontosPorFormaDecobro(ref XElement refResult, int valconsecutivoCompania, int valConsecutivoCaja, string valHoraApertura, string valHoraCierre) {
            bool vReq = false;
            StringBuilder vSql = new StringBuilder();
            vSql.AppendLine(" ;WITH CTE_MaxAperturaCaja(Fecha,ConsecutivoCaja,ConsecutivoCompania) AS");
            vSql.AppendLine(" (SELECT TOP(1)Fecha,ConsecutivoCaja,ConsecutivoCompania ");
            vSql.AppendLine(" FROM Adm.CajaApertura");
            vSql.AppendLine(" WHERE ConsecutivoCaja=" + insSql.ToSqlValue(valConsecutivoCaja));
            vSql.AppendLine(" AND CajaCerrada =" + insSql.ToSqlValue(false));
            vSql.AppendLine(" AND ConsecutivoCompania=" + insSql.ToSqlValue(valconsecutivoCompania));
            vSql.AppendLine(" ORDER BY Fecha DESC)");
            vSql.AppendLine(" ,CTE_MontoCierre (MontoEfectivo,MontoCheque,MontoTarjeta,MontoDeposito,MontoAnticipo, MontoEfectivoME,MontoChequeME,MontoTarjetaME,MontoDepositoME,MontoAnticipoME) AS");
            vSql.AppendLine("  ( SELECT ");
            vSql.AppendLine(insSql.IIF("FormaDelCobro.TipoDePago = " + insSql.EnumToSqlValue((int)eFormaDeCobro.Efectivo), "SUM(renglonCobroDeFactura.Monto)", "0", true) + " AS MontoEfectivo, ");
            vSql.AppendLine(insSql.IIF("FormaDelCobro.TipoDePago = " + insSql.EnumToSqlValue((int)eFormaDeCobro.Cheque), "SUM(renglonCobroDeFactura.Monto)", "0", true) + " AS MontoCheque, ");
            vSql.AppendLine(insSql.IIF("FormaDelCobro.TipoDePago = " + insSql.EnumToSqlValue((int)eFormaDeCobro.Tarjeta), "SUM(renglonCobroDeFactura.Monto)", "0", true) + " AS MontoTarjeta, ");
            vSql.AppendLine(insSql.IIF("FormaDelCobro.TipoDePago = " + insSql.EnumToSqlValue((int)eFormaDeCobro.Deposito), "SUM(renglonCobroDeFactura.Monto)", "0", true) + " AS MontoDeposito, ");
            vSql.AppendLine(insSql.IIF("FormaDelCobro.TipoDePago = " + insSql.EnumToSqlValue((int)eFormaDeCobro.Anticipo), "SUM(renglonCobroDeFactura.Monto)", "0", true) + " AS MontoAnticipo ");
            vSql.AppendLine(" , 0, 0 ,0 ,0, 0 ");
            vSql.AppendLine("  FROM renglonCobroDeFactura");
            vSql.AppendLine("  INNER JOIN  FormaDelCobro ON");
            vSql.AppendLine("  renglonCobroDeFactura.CodigoFormaDelCobro = FormaDelCobro.Codigo");
            vSql.AppendLine("  INNER JOIN factura  ON");
            vSql.AppendLine("  renglonCobroDeFactura.ConsecutivoCompania = factura.ConsecutivoCompania");
            vSql.AppendLine("  AND renglonCobroDeFactura.NumeroFactura = factura.Numero");
            vSql.AppendLine("  AND  renglonCobroDeFactura.TipoDeDocumento = factura.TipoDeDocumento");
            vSql.AppendLine("  INNER JOIN CTE_MaxAperturaCaja ON");
            vSql.AppendLine("  factura.ConsecutivoCaja = CTE_MaxAperturaCaja.ConsecutivoCaja AND");
            vSql.AppendLine("  factura.ConsecutivoCompania = CTE_MaxAperturaCaja.ConsecutivoCompania");
            vSql.AppendLine(vSqlWhereTotalesMontosPorFormaDecobro(valconsecutivoCompania, valConsecutivoCaja, valHoraApertura, valHoraCierre, false));
            vSql.AppendLine("  GROUP BY FormaDelCobro.TipoDePago");
            vSql.AppendLine(" UNION ");
            vSql.AppendLine(" SELECT ");
            vSql.AppendLine(" 0, 0, 0, 0, 0, ");
            vSql.AppendLine(insSql.IIF("FormaDelCobro.TipoDePago = " + insSql.EnumToSqlValue((int)eFormaDeCobro.Efectivo), "SUM(renglonCobroDeFactura.Monto)", "0", true) + " AS MontoEfectivoME, ");
            vSql.AppendLine(insSql.IIF("FormaDelCobro.TipoDePago = " + insSql.EnumToSqlValue((int)eFormaDeCobro.Cheque), "SUM(renglonCobroDeFactura.Monto)", "0", true) + " AS MontoChequeME, ");
            vSql.AppendLine(insSql.IIF("FormaDelCobro.TipoDePago = " + insSql.EnumToSqlValue((int)eFormaDeCobro.Tarjeta), "SUM(renglonCobroDeFactura.Monto)", "0", true) + " AS MontoTarjetaME, ");
            vSql.AppendLine(insSql.IIF("FormaDelCobro.TipoDePago = " + insSql.EnumToSqlValue((int)eFormaDeCobro.Deposito), "SUM(renglonCobroDeFactura.Monto)", "0", true) + " AS MontoDepositoME, ");
            vSql.AppendLine(insSql.IIF("FormaDelCobro.TipoDePago = " + insSql.EnumToSqlValue((int)eFormaDeCobro.Anticipo), "SUM(renglonCobroDeFactura.Monto)", "0", true) + " AS MontoAnticipoME ");
            vSql.AppendLine("  FROM renglonCobroDeFactura");
            vSql.AppendLine("  INNER JOIN  FormaDelCobro ON");
            vSql.AppendLine("  renglonCobroDeFactura.CodigoFormaDelCobro = FormaDelCobro.Codigo");
            vSql.AppendLine("  INNER JOIN factura  ON");
            vSql.AppendLine("  renglonCobroDeFactura.ConsecutivoCompania = factura.ConsecutivoCompania");
            vSql.AppendLine("  AND renglonCobroDeFactura.NumeroFactura = factura.Numero");
            vSql.AppendLine("  AND  renglonCobroDeFactura.TipoDeDocumento = factura.TipoDeDocumento");
            vSql.AppendLine("  INNER JOIN CTE_MaxAperturaCaja ON");
            vSql.AppendLine("  factura.ConsecutivoCaja = CTE_MaxAperturaCaja.ConsecutivoCaja AND");
            vSql.AppendLine("  factura.ConsecutivoCompania = CTE_MaxAperturaCaja.ConsecutivoCompania");
            vSql.AppendLine(vSqlWhereTotalesMontosPorFormaDecobro(valconsecutivoCompania, valConsecutivoCaja, valHoraApertura, valHoraCierre, true));
            vSql.AppendLine("  GROUP BY FormaDelCobro.TipoDePago)");
            vSql.AppendLine("  SELECT ");
            vSql.AppendLine("ISNULL(SUM(MontoEfectivo),0) AS MontoEfectivo ,");
            vSql.AppendLine("ISNULL(SUM(MontoTarjeta) ,0) AS MontoTarjeta ,");
            vSql.AppendLine("ISNULL(SUM(MontoDeposito),0) AS MontoDeposito ,");
            vSql.AppendLine("ISNULL(SUM(MontoAnticipo),0) AS MontoAnticipo ,");
            vSql.AppendLine("ISNULL(SUM(MontoCheque),0) AS MontoCheque, ");
            vSql.AppendLine("ISNULL(SUM(MontoEfectivoME),0) AS MontoEfectivoME ,");
            vSql.AppendLine("ISNULL(SUM(MontoTarjetaME) ,0) AS MontoTarjetaME ,");
            vSql.AppendLine("ISNULL(SUM(MontoDepositoME),0) AS MontoDepositoME ,");
            vSql.AppendLine("ISNULL(SUM(MontoAnticipoME),0) AS MontoAnticipoME ,");
            vSql.AppendLine("ISNULL(SUM(MontoChequeME),0) AS MontoChequeME ");
            vSql.AppendLine(" FROM CTE_MontoCierre ");
            refResult = LibBusiness.ExecuteSelect(vSql.ToString(), new LibGpParams().Get(), "", -1);
            return vReq = (refResult.Descendants("GpResult").Count() > 0);
        }
        #region CajaApertura       

        private XElement FindInfoCaja(IList<CajaApertura> valData) {
            XElement vXElement = new XElement("GpData");            
            ILibPdn insCaja = new Galac.Adm.Brl.Venta.clsCajaNav();
            XElement vXElementResult = insCaja.GetFk("CajaApertura",ParametersGetFKCajaForXmlSubSet(valData[0].ConsecutivoCompania,vXElement));
            return vXElementResult;
        }     

        private StringBuilder ParametersGetFKCajaForXmlSubSet(int valConsecutivoCompania,XElement valXElement) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInInteger("ConsecutivoCompania",valConsecutivoCompania);
            vParams.AddInXml("XmlData",valXElement);
            vResult = vParams.Get();
            return vResult;
        }

        private XElement FindInfoGUser() {
            LibGalac.Aos.Ccl.Usal.ILibGUserPdn vGUserPdn = new LibGalac.Aos.Brl.Usal.LibGUserNav();
            return vGUserPdn.GetFk("CajaApertura",null);
        }
        #endregion //CajaApertura        

        XElement ICajaAperturaPdn.FindByConsecutivoApertura(int valConsecutivoCompania,int valConsecutivo) {
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("Consecutivo",valConsecutivo);
            vParams.AddInInteger("ConsecutivoCompania",valConsecutivoCompania);
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("SELECT * FROM Adm.CajaApertura");
            SQL.AppendLine("WHERE Consecutivo = @Consecutivo");
            SQL.AppendLine("AND ConsecutivoCompania = @ConsecutivoCompania");
            return LibBusiness.ExecuteSelect(SQL.ToString(),vParams.Get(),"",-1);
        }

        #endregion //Metodos Generados        
    } //End of class clsCajaAperturaNav   
} //End of namespace Galac.Adm.Brl.Venta

