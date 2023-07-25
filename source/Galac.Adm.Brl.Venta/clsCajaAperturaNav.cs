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

namespace Galac.Adm.Brl.Venta {
    public partial class clsCajaAperturaNav:LibBaseNav<IList<CajaApertura>,IList<CajaApertura>>, ICajaAperturaPdn {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores

        public clsCajaAperturaNav() {
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

        bool ICajaAperturaPdn.AbrirCaja(CajaApertura valCajaApertura) {
            LibGpParams vParams = new LibGpParams();
            IList<CajaApertura> ListCajaApertura = new List<CajaApertura>();
            ILibDataComponent<IList<CajaApertura>,IList<CajaApertura>> insCajaAperturaFiscalDat = new Dal.Venta.clsCajaAperturaDat();
            bool vResult = false;
            ListCajaApertura.Add(new CajaApertura {
                ConsecutivoCompania = valCajaApertura.ConsecutivoCompania,
                Consecutivo = valCajaApertura.Consecutivo,
                ConsecutivoCaja = valCajaApertura.ConsecutivoCaja,
                NombreDelUsuario = valCajaApertura.NombreDelUsuario,
                MontoApertura = valCajaApertura.MontoApertura,
                MontoCierre = valCajaApertura.MontoCierre,
                MontoEfectivo = valCajaApertura.MontoEfectivo,
                MontoTarjeta = valCajaApertura.MontoTarjeta,
                MontoCheque = valCajaApertura.MontoCheque,
                MontoDeposito = valCajaApertura.MontoDeposito,
                MontoAnticipo = valCajaApertura.MontoAnticipo,
                Fecha =LibDate.Today(),
                HoraApertura =valCajaApertura.HoraApertura,
                HoraCierre = valCajaApertura.HoraCierre,
                CajaCerradaAsBool =false,
                NombreCaja =valCajaApertura.NombreCaja,                
                CodigoMoneda = valCajaApertura.CodigoMoneda,
                Cambio = valCajaApertura.Cambio,
                NombreOperador ="",
                FechaUltimaModificacion = valCajaApertura.FechaUltimaModificacion
            });            
            vResult = insCajaAperturaFiscalDat.Insert(ListCajaApertura).Success;            
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
            vParams.AddInString("CodigoMoneda",valCajaApertura.CodigoMoneda,4);
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


        bool ICajaAperturaPdn.TotalesMontosPorFormaDecobro(ref XElement refResult,int valconsecutivoCompania,int valConsecutivoCaja,string valHoraApertura,string valHoraCierre) {
            bool vReq = false;
            StringBuilder vSql = new StringBuilder();
            QAdvSql vQAdvSql = new QAdvSql("");
            string vSqlTipoFormaDeCobro = "FormaDelCobro.TipoDePago = ";
            string vSqlSumMontoXFormaDeCobro = "";
            string vSqlWhere = "";
            vSqlSumMontoXFormaDeCobro = "SUM(renglonCobroDeFactura.CambioAMonedaLocal * renglonCobroDeFactura.Monto)";
            vSqlWhere = "factura.Fecha BETWEEN CTE_MaxAperturaCaja.Fecha AND "+ vQAdvSql.ToSqlValue(LibDate.Today());
            vSqlWhere = vQAdvSql.SqlIntValueWithOperators(vSqlWhere,"factura.ConsecutivoCaja",valConsecutivoCaja," AND ","=");
            vSqlWhere = vQAdvSql.SqlIntValueWithOperators(vSqlWhere,"factura.ConsecutivoCompania",valconsecutivoCompania,"","=");
            vSqlWhere = vQAdvSql.SqlValueBetween(vSqlWhere,"factura.HoraModificacion",valHoraApertura,valHoraCierre,"");
            vSqlWhere = vQAdvSql.WhereSql(vSqlWhere);
            vSql.AppendLine(" SET DATEFORMAT DMY ");
            vSql.AppendLine(" ;WITH CTE_MaxAperturaCaja(Fecha,ConsecutivoCaja,ConsecutivoCompania) AS");
            vSql.AppendLine(" (SELECT TOP(1)Fecha,ConsecutivoCaja,ConsecutivoCompania ");            
            vSql.AppendLine(" FROM Adm.CajaApertura");
            vSql.AppendLine(" WHERE ConsecutivoCaja=" + vQAdvSql.ToSqlValue(valConsecutivoCaja));
            vSql.AppendLine(" AND CajaCerrada =" + vQAdvSql.ToSqlValue(false));
            vSql.AppendLine(" AND ConsecutivoCompania=" + vQAdvSql.ToSqlValue(valconsecutivoCompania));
            vSql.AppendLine(" ORDER BY Fecha DESC)");
            vSql.AppendLine(" ,CTE_MontoCierre (MontoEfectivo,MontoCheque,MontoTarjeta,MontoDeposito,MontoAnticipo) AS");
            vSql.AppendLine("  ( SELECT ");
            vSql.AppendLine(vQAdvSql.IIF(vSqlTipoFormaDeCobro + vQAdvSql.EnumToSqlValue((int)eFormaDeCobro.Efectivo),vSqlSumMontoXFormaDeCobro,"0",true) + " AS MontoEfectivo, ");
            vSql.AppendLine(vQAdvSql.IIF(vSqlTipoFormaDeCobro + vQAdvSql.EnumToSqlValue((int)eFormaDeCobro.Cheque),vSqlSumMontoXFormaDeCobro,"0",true) + " AS MontoCheque, ");
            vSql.AppendLine(vQAdvSql.IIF(vSqlTipoFormaDeCobro + vQAdvSql.EnumToSqlValue((int)eFormaDeCobro.Tarjeta),vSqlSumMontoXFormaDeCobro,"0",true) + " AS MontoTarjeta, ");
            vSql.AppendLine(vQAdvSql.IIF(vSqlTipoFormaDeCobro + vQAdvSql.EnumToSqlValue((int)eFormaDeCobro.Deposito),vSqlSumMontoXFormaDeCobro,"0",true) + " AS MontoDeposito, ");
            vSql.AppendLine(vQAdvSql.IIF(vSqlTipoFormaDeCobro + vQAdvSql.EnumToSqlValue((int)eFormaDeCobro.Anticipo),vSqlSumMontoXFormaDeCobro,"0",true) + " AS MontoAnticipo ");
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
            vSql.AppendLine(vSqlWhere);
            vSql.AppendLine("  GROUP BY FormaDelCobro.TipoDePago)");
            vSql.AppendLine("  SELECT ");
            vSql.AppendLine("ISNULL(SUM(MontoEfectivo),0) AS MontoEfectivo ,");
            vSql.AppendLine("ISNULL(SUM(MontoTarjeta) ,0) AS MontoTarjeta ,");
            vSql.AppendLine("ISNULL(SUM(MontoDeposito),0) AS MontoDeposito ,");
            vSql.AppendLine("ISNULL(SUM(MontoAnticipo),0) AS MontoAnticipo ,"); 
            vSql.AppendLine("ISNULL(SUM(MontoCheque),0) AS MontoCheque ");            
            vSql.AppendLine(" FROM CTE_MontoCierre ");
            refResult = LibBusiness.ExecuteSelect(vSql.ToString(),new LibGpParams().Get(),"",-1);
            return vReq = (refResult.Descendants("GpResult").Count() > 0);
        }

        #region CajaApertura

        /*
        private void FillWithForeignInfoCajaApertura(ref IList<CajaApertura> refData) {
            XElement vInfoConexionCaja = FindInfoCaja(refData);
            var vListCaja = (from vRecord in vInfoConexionCaja.Descendants("GpResult")
                             select new {
                                 ConsecutivoCompania = LibConvert.ToInt(vRecord.Element("ConsecutivoCompania")),
                                 Consecutivo = LibConvert.ToInt(vRecord.Element("ConsecutivoCaja")),
                                 NombreCaja = vRecord.Element("NombreCaja").Value,
                                 UsaGaveta = vRecord.Element("UsaGaveta").Value,
                                 Puerto = vRecord.Element("Puerto").Value,
                                 Comando = vRecord.Element("Comando").Value,
                                 PermitirAbrirSinSupervisor = vRecord.Element("PermitirAbrirSinSupervisor").Value,
                                 UsaAccesoRapido = vRecord.Element("UsaAccesoRapido").Value,
                                 UsaMaquinaFiscal = vRecord.Element("UsaMaquinaFiscal").Value,
                                 FamiliaImpresoraFiscal = vRecord.Element("FamiliaImpresoraFiscal").Value,
                                 ModeloDeMaquinaFiscal = vRecord.Element("ModeloDeMaquinaFiscal").Value,
                                 SerialDeMaquinaFiscal = vRecord.Element("SerialDeMaquinaFiscal").Value,
                                 PuertoMaquinaFiscal = vRecord.Element("PuertoMaquinaFiscal").Value,
                                 AbrirGavetaDeDinero = vRecord.Element("AbrirGavetaDeDinero").Value,
                                 UltimoNumeroCompFiscal = vRecord.Element("UltimoNumeroCompFiscal").Value,
                                 UltimoNumeroNCFiscal = vRecord.Element("UltimoNumeroNCFiscal").Value,
                                 IpParaConexion = vRecord.Element("IpParaConexion").Value,
                                 MascaraSubred = vRecord.Element("MascaraSubred").Value,
                                 Gateway = vRecord.Element("Gateway").Value,
                                 PermitirDescripcionDelArticuloExtendida = vRecord.Element("PermitirDescripcionDelArticuloExtendida").Value,
                                 PermitirNombreDelClienteExtendido = vRecord.Element("PermitirNombreDelClienteExtendido").Value,
                                 UsarModoDotNet = vRecord.Element("UsarModoDotNet").Value
                             }).Distinct();
            XElement vInfoConexionGUser = FindInfoGUser();
            var vListGUser = (from vRecord in vInfoConexionGUser.Descendants("GpResult")
                              select new {
                                  UserName = vRecord.Element("UserName").Value,
                                  FirstAndLastName = vRecord.Element("FirstAndLastName").Value,
                                  UserPassword = vRecord.Element("UserPassword").Value,
                                  Cargo = vRecord.Element("Cargo").Value,
                                  EMail = vRecord.Element("EMail").Value,
                                  Status = vRecord.Element("Status").Value,
                                  IsSuperviser = vRecord.Element("IsSuperviser").Value,
                                  NameLastUsedCompany = vRecord.Element("NameLastUsedCompany").Value,
                                  LastUsedMFCList = vRecord.Element("LastUsedMFCList").Value
                              }).Distinct();

            foreach(CajaApertura vItem in refData) {
                vItem.NombreCaja = vInfoConexionCaja.Descendants("GpResult")
                    .Where(p => p.Element("ConsecutivoCaja").Value == LibConvert.ToStr(vItem.ConsecutivoCaja))
                    .Select(p => p.Element("NombreCaja").Value).FirstOrDefault();
            }
        }
        */

        private XElement FindInfoCaja(IList<CajaApertura> valData) {
            XElement vXElement = new XElement("GpData");
            //foreach(CajaApertura vItem in valData) {
            //    //vXElement.Add(FilterCajaAperturaByDistinctCaja(vItem).Descendants("GpResult"));
            //}
            ILibPdn insCaja = new Galac.Adm.Brl.Venta.clsCajaNav();
            XElement vXElementResult = insCaja.GetFk("CajaApertura",ParametersGetFKCajaForXmlSubSet(valData[0].ConsecutivoCompania,vXElement));
            return vXElementResult;
        }

        private XElement FilterCajaAperturaByDistinctCaja(CajaApertura valMaster) {
            //XElement vXElement = new XElement("GpData",
            //    from vEntity in valMaster.DetailCajaApertura.Distinct()
            //    select new XElement("GpResult",
            //        new XElement("ConsecutivoCaja", vEntity.ConsecutivoCaja)));
            //return vXElement;
            return null;
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

