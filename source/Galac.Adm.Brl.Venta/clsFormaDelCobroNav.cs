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
using Galac.Adm.Dal.Venta;
using Galac.Adm.Ccl.Banco;
using LibGalac.Aos.Dal;
using Galac.Saw.Ccl.SttDef;

namespace Galac.Adm.Brl.Venta {
    public partial class clsFormaDelCobroNav: LibBaseNav<IList<FormaDelCobro>, IList<FormaDelCobro>>, IFormaDelCobroPdn {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores

        public clsFormaDelCobroNav() {
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override ILibDataComponentWithSearch<IList<FormaDelCobro>, IList<FormaDelCobro>> GetDataInstance() {
            return new clsFormaDelCobroDat();
        }
        #region Miembros de ILibPdn

        bool ILibPdn.CanBeChoosen(string valCallingModule, eAccionSR valAction, string valExtendedAction, XmlDocument valXmlRow) {
            bool vResult = false;
            //ILibDataFKSearch instanciaDal = new Galac.Adm.Dal.Venta.clsFormaDelCobroDat();
            switch (valCallingModule) {
                default:
                    vResult = true;
                    break;
            }
            return vResult;
        }

        bool ILibPdn.GetDataForList(string valCallingModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            ILibDataFKSearch instanciaDal = new clsFormaDelCobroDat();
            return instanciaDal.ConnectFk(ref refXmlDocument, eProcessMessageType.SpName, "Adm.Gp_FormaDelCobroSCH", valXmlParamsExpression);
        }

        System.Xml.Linq.XElement ILibPdn.GetFk(string valCallingModule, StringBuilder valParameters) {
            ILibDataComponent<IList<FormaDelCobro>, IList<FormaDelCobro>> instanciaDal = new clsFormaDelCobroDat();
            return instanciaDal.QueryInfo(eProcessMessageType.SpName, "Adm.Gp_FormaDelCobroGetFk", valParameters);
        }
        #endregion //Miembros de ILibPdn

        protected override bool RetrieveListInfo(string valModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            bool vResult = false;
            ILibPdn vPdnModule;
            switch (valModule) {
                case "Forma de Cobro":
                    vResult = ((ILibPdn)this).GetDataForList(valModule, ref refXmlDocument, valXmlParamsExpression);
                    break;
                case "Cuenta Bancaria":
                    vPdnModule = new Banco.clsCuentaBancariaNav();
                    vResult = vPdnModule.GetDataForList("Forma del Cobro", ref refXmlDocument, valXmlParamsExpression);
                    break;
                case "Moneda":
                    vPdnModule = new Comun.Brl.TablasGen.clsMonedaNav();
                    vResult = vPdnModule.GetDataForList("Forma del Cobro", ref refXmlDocument, valXmlParamsExpression);
                    break;
                default: throw new NotImplementedException();
            }
            return vResult;
        }

        //protected override void FillWithForeignInfo(ref IList<FormaDelCobro> refData) {
        //    FillWithForeignInfoFormaDelCobro(ref refData);
        //}
        #region FormaDelCobro

        //private void FillWithForeignInfoFormaDelCobro(ref IList<FormaDelCobro> refData) {
        //    XElement vInfoConexionCuentaBancaria = FindInfoCuentaBancaria(refData);
        //    var vListCuentaBancaria = (from vRecord in vInfoConexionCuentaBancaria.Descendants("GpResult")
        //                              select new {
        //                                  ConsecutivoCompania = LibConvert.ToInt(vRecord.Element("ConsecutivoCompania")),
        //                                  Codigo = vRecord.Element("Codigo").Value, 
        //                                  Status = vRecord.Element("Status").Value, 
        //                                  NumeroCuenta = vRecord.Element("NumeroCuenta").Value, 
        //                                  NombreCuenta = vRecord.Element("NombreCuenta").Value, 
        //                                  CodigoBanco = LibConvert.ToInt(vRecord.Element("CodigoBanco")), 
        //                                  NombreBanco = vRecord.Element("NombreBanco").Value, 
        //                                  NombreSucursal = vRecord.Element("NombreSucursal").Value, 
        //                                  TipoCtaBancaria = vRecord.Element("TipoCtaBancaria").Value, 
        //                                  ManejaDebitoBancario = vRecord.Element("ManejaDebitoBancario").Value, 
        //                                  ManejaCreditoBancario = vRecord.Element("ManejaCreditoBancario").Value, 
        //                                  SaldoDisponible = LibConvert.ToDec(vRecord.Element("SaldoDisponible")), 
        //                                  NombreDeLaMoneda = vRecord.Element("NombreDeLaMoneda").Value, 
        //                                  NombrePlantillaCheque = vRecord.Element("NombrePlantillaCheque").Value, 
        //                                  CuentaContable = vRecord.Element("CuentaContable").Value, 
        //                                  CodigoMoneda = vRecord.Element("CodigoMoneda").Value, 
        //                                  TipoDeAlicuotaPorContribuyente = vRecord.Element("TipoDeAlicuotaPorContribuyente").Value, 
        //                                  ExcluirDelInformeDeDeclaracionIGTF = vRecord.Element("ExcluirDelInformeDeDeclaracionIGTF").Value, 
        //                                  GeneraMovBancarioPorIGTF = vRecord.Element("GeneraMovBancarioPorIGTF").Value
        //                              }).Distinct();

        //    foreach (FormaDelCobro vItem in refData) {
        //        vItem.NombreCuentaBancaria = vInfoConexionCuentaBancaria.Descendants("GpResult")
        //            .Where(p => p.Element("Codigo").Value == vItem.CodigoCuentaBancaria)
        //            .Select(p => p.Element("Nombre").Value).FirstOrDefault();
        //    }
        //}

        //private XElement FindInfoCuentaBancaria(IList<FormaDelCobro> valData) {
        //    XElement vXElement = new XElement("GpData");
        //    //foreach (FormaDelCobro vItem in valData) {
        //    //    vXElement.Add(FilterFormaDelCobroByDistinctCuentaBancaria(vItem).Descendants("GpResult"));
        //    //}
        //    ILibPdn insCuentaBancaria = new Galac.Adm.Brl.Banco.clsCuentaBancariaNav();
        //    XElement vXElementResult = insCuentaBancaria.GetFk("FormaDelCobro", ParametersGetFKCuentaBancariaForXmlSubSet(vXElement));
        //    return vXElementResult;
        //}

        //private XElement FilterFormaDelCobroByDistinctCuentaBancaria(FormaDelCobro valMaster) {
        //    XElement vXElement = new XElement("GpData",
        //        from vEntity in valMaster.DetailFormaDelCobro.Distinct()
        //        select new XElement("GpResult",
        //            new XElement("CodigoCuentaBancaria", vEntity.CodigoCuentaBancaria)));
        //    return vXElement;
        //}

        //private StringBuilder ParametersGetFKCuentaBancariaForXmlSubSet(XElement valXElement) {
        //    StringBuilder vResult = new StringBuilder();
        //    LibGpParams vParams = new LibGpParams();
        //    vParams.AddReturn();
        //    vParams.AddInXml("XmlData", valXElement);
        //    vResult = vParams.Get();
        //    return vResult;
        //}
        #endregion //FormaDelCobro

        XElement IFormaDelCobroPdn.FindByConsecutivoCompaniaCodigo(int valConsecutivoCompania, string valCodigo) {
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInString("Codigo", valCodigo, 5);
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("SELECT * FROM Adm.FormaDelCobro");
            SQL.AppendLine("WHERE ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("AND Codigo = @Codigo");
            return LibBusiness.ExecuteSelect(SQL.ToString(), vParams.Get(), "", -1);
        }

        XElement IFormaDelCobroPdn.FindByConsecutivoCompaniaNombre(int valConsecutivoCompania, string valNombre) {
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInString("Nombre", valNombre, 50);
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("SELECT * FROM Adm.FormaDelCobro");
            SQL.AppendLine("WHERE ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("AND Nombre = @Nombre");
            return LibBusiness.ExecuteSelect(SQL.ToString(), vParams.Get(), "", -1);
        }

        void IFormaDelCobroPdn.InsertDefaultRecord(int valConsecutivoCompania, string valCodigoMonedaLocal, string valCodigoMonedaExtranjera) {
            InsertFormaDelCobroPorDefecto(valConsecutivoCompania, 1, "00001", "Efectivo", eFormaDeCobro.Efectivo, string.Empty, valCodigoMonedaLocal, CodigosTheFactory(eFormaDeCobro.Efectivo), eOrigen.Sistema);
            InsertFormaDelCobroPorDefecto(valConsecutivoCompania, 2, "00002", "Tarjeta", eFormaDeCobro.Tarjeta, string.Empty, valCodigoMonedaLocal, CodigosTheFactory(eFormaDeCobro.Tarjeta), eOrigen.Sistema);
            InsertFormaDelCobroPorDefecto(valConsecutivoCompania, 3, "00003", "Cheque", eFormaDeCobro.Cheque, string.Empty, valCodigoMonedaLocal, CodigosTheFactory(eFormaDeCobro.Cheque), eOrigen.Sistema);
            InsertFormaDelCobroPorDefecto(valConsecutivoCompania, 4, "00004", "Depósito", eFormaDeCobro.Deposito, string.Empty, valCodigoMonedaLocal, CodigosTheFactory(eFormaDeCobro.Deposito), eOrigen.Sistema);
            InsertFormaDelCobroPorDefecto(valConsecutivoCompania, 5, "00005", "Anticipo", eFormaDeCobro.Anticipo, string.Empty, valCodigoMonedaLocal, CodigosTheFactory(eFormaDeCobro.Anticipo), eOrigen.Sistema);
            InsertFormaDelCobroPorDefecto(valConsecutivoCompania, 6, "00006", "Transferencia", eFormaDeCobro.Transferencia, string.Empty, valCodigoMonedaLocal, CodigosTheFactory(eFormaDeCobro.Transferencia), eOrigen.Sistema);
            InsertFormaDelCobroPorDefecto(valConsecutivoCompania, 7, "00007", "Vuelto Efectivo", eFormaDeCobro.VueltoEfectivo, string.Empty, valCodigoMonedaLocal, CodigosTheFactory(eFormaDeCobro.VueltoEfectivo), eOrigen.Sistema);
            InsertFormaDelCobroPorDefecto(valConsecutivoCompania, 8, "00008", "Vuelto Pago Móvil", eFormaDeCobro.VueltoPM, string.Empty, valCodigoMonedaLocal, CodigosTheFactory(eFormaDeCobro.VueltoPM), eOrigen.Sistema);
            InsertFormaDelCobroPorDefecto(valConsecutivoCompania, 9, "00009", "Tarjeta Medios Electrónicos", eFormaDeCobro.TarjetaMS, string.Empty, valCodigoMonedaLocal, CodigosTheFactory(eFormaDeCobro.TarjetaMS), eOrigen.Sistema);
            InsertFormaDelCobroPorDefecto(valConsecutivoCompania, 10, "00010", "Zelle", eFormaDeCobro.Zelle, string.Empty, valCodigoMonedaExtranjera, CodigosTheFactory(eFormaDeCobro.Zelle), eOrigen.Sistema);
            InsertFormaDelCobroPorDefecto(valConsecutivoCompania, 11, "00011", "Pago Móvil P2C", eFormaDeCobro.PagoMovil, string.Empty, valCodigoMonedaLocal, CodigosTheFactory(eFormaDeCobro.PagoMovil), eOrigen.Sistema);
            InsertFormaDelCobroPorDefecto(valConsecutivoCompania, 12, "00012", "Transferencia Medios Electrónicos", eFormaDeCobro.TransferenciaMS, string.Empty, valCodigoMonedaLocal, CodigosTheFactory(eFormaDeCobro.TransferenciaMS), eOrigen.Sistema);
            InsertFormaDelCobroPorDefecto(valConsecutivoCompania, 13, "00013", "Pago Móvil C2P", eFormaDeCobro.C2P, string.Empty, valCodigoMonedaLocal, CodigosTheFactory(eFormaDeCobro.C2P), eOrigen.Sistema);
            InsertFormaDelCobroPorDefecto(valConsecutivoCompania, 14, "00014", "Depósito Medios Electrónicos", eFormaDeCobro.DepositoMS, string.Empty, valCodigoMonedaLocal, CodigosTheFactory(eFormaDeCobro.DepositoMS), eOrigen.Sistema);
            InsertFormaDelCobroPorDefecto(valConsecutivoCompania, 15, "00015", "Crédito Electrónico", eFormaDeCobro.CreditoElectronico, string.Empty, valCodigoMonedaExtranjera, CodigosTheFactory(eFormaDeCobro.CreditoElectronico), eOrigen.Sistema);
            InsertFormaDelCobroPorDefecto(valConsecutivoCompania, 16, "00016", "Tarjeta de Crédito", eFormaDeCobro.TarjetadeCredito, string.Empty, valCodigoMonedaLocal, CodigosTheFactory(eFormaDeCobro.TarjetadeCredito), eOrigen.Sistema);
            InsertFormaDelCobroPorDefecto(valConsecutivoCompania, 17, "00017", "Tarjeta de Débito", eFormaDeCobro.TarjetadeDebito, string.Empty, valCodigoMonedaLocal, CodigosTheFactory(eFormaDeCobro.TarjetadeDebito), eOrigen.Sistema);
            InsertFormaDelCobroPorDefecto(valConsecutivoCompania, 18, "00018", "Efectivo Divisas", eFormaDeCobro.EfectivoDivisas, string.Empty, valCodigoMonedaExtranjera, CodigosTheFactory(eFormaDeCobro.EfectivoDivisas), eOrigen.Sistema);
            InsertFormaDelCobroPorDefecto(valConsecutivoCompania, 19, "00019", "Transferencia Divisas", eFormaDeCobro.TransferenciaDivisas, string.Empty, valCodigoMonedaExtranjera, CodigosTheFactory(eFormaDeCobro.TransferenciaDivisas), eOrigen.Sistema);
            InsertFormaDelCobroPorDefecto(valConsecutivoCompania, 20, "00020", "Vuelto Efectivo Divisas", eFormaDeCobro.VueltoEfectivoDivisas, string.Empty, valCodigoMonedaExtranjera, CodigosTheFactory(eFormaDeCobro.VueltoEfectivoDivisas), eOrigen.Sistema);
        }

        private string CodigosTheFactory(eFormaDeCobro valFormaDelCobro) {
            string vResult = "01";
            switch (valFormaDelCobro) {
                case eFormaDeCobro.Cheque:
                    vResult = "07";
                    break;
                case eFormaDeCobro.Tarjeta:
                case eFormaDeCobro.TarjetadeCredito:
                case eFormaDeCobro.TarjetadeDebito:
                case eFormaDeCobro.TarjetaMS:
                    vResult = "13";
                    break;
                case eFormaDeCobro.Transferencia:
                case eFormaDeCobro.TransferenciaMS:
                case eFormaDeCobro.TransferenciaDivisas:
                    vResult = "06";
                    break;
                case eFormaDeCobro.CreditoElectronico:
                    vResult = "15";
                    break;
            }

            return vResult;
        }
        
        void InsertFormaDelCobroPorDefecto(int valConsecutivoCompania, int valConsecutivo, string valCodigo, string valNombre, eFormaDeCobro valFormaDelCobro, string valCodigoCtaBancaria, string valCodigoMoneda, string valCodigoTheFactory, eOrigen valOrigen) {
            QAdvSql insUtilSql = new QAdvSql("");
            StringBuilder vSQL = new StringBuilder();
            vSQL.AppendLine("INSERT INTO Adm.FormaDelCobro (ConsecutivoCompania, Consecutivo, Codigo,Nombre, TipoDePago, CodigoCuentaBancaria, CodigoMoneda, CodigoTheFactory, Origen) VALUES (");
            vSQL.AppendLine(insUtilSql.ToSqlValue(valConsecutivoCompania) + ", " + insUtilSql.ToSqlValue(valConsecutivo) + " , "+ insUtilSql.ToSqlValue(valCodigo)  + " , " + insUtilSql.ToSqlValue(valNombre) + ", " + insUtilSql.EnumToSqlValue((int)valFormaDelCobro) + " , " + insUtilSql.ToSqlValue(valCodigoCtaBancaria) + " , " + insUtilSql.ToSqlValue(valCodigoMoneda) + " , " + insUtilSql.ToSqlValue(valCodigoTheFactory) + ", " + insUtilSql.EnumToSqlValue((int)valOrigen) + ")");
            LibBusiness.ExecuteUpdateOrDelete(vSQL.ToString(), new StringBuilder(), string.Empty, 0);
        }
        #endregion //Metodos Generados

    } //End of class clsFormaDelCobroNav

} //End of namespace Galac.Adm.Brl.Venta

