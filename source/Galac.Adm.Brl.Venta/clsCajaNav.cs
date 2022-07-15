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
using LibGalac.Aos.Brl.Usal;
using LibGalac.Aos.Ccl.Usal;
using LibGalac.Aos.Base.Dal;
using Galac.Adm.Ccl.Venta;
using LibGalac.Aos.Cnf;
using Galac.Adm.Dal.Venta;
using Galac.Adm.Ccl.DispositivosExternos;
using Galac.Adm.Brl.DispositivosExternos.ImpresoraFiscal;
using LibGalac.Aos.Catching;
using Galac.Saw.Ccl.SttDef;

namespace Galac.Adm.Brl.Venta {
    public partial class clsCajaNav:LibBaseNav<IList<Caja>,IList<Caja>>, ICajaPdn {
        #region Variables
        string _SerialMaquinaFiscal = "";
        #endregion //Variables
        #region Propiedades

        public string SerialMaquinaFiscal {
            get { return _SerialMaquinaFiscal; }
            set { _SerialMaquinaFiscal = value; }
        }

        #endregion //Propiedades
        #region Constructores

        public clsCajaNav() {
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override ILibDataComponentWithSearch<IList<Caja>,IList<Caja>> GetDataInstance() {
            return new Galac.Adm.Dal.Venta.clsCajaDat();
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
            ILibDataFKSearch instanciaDal = new Galac.Adm.Dal.Venta.clsCajaDat();
            valXmlParamsExpression.Replace("UsaMaquinaFiscalStr","Gv_Caja_B1.UsaMaquinaFiscal");
            valXmlParamsExpression.Replace("ModeloMaquinaFiscalStr","Gv_Caja_B1.ModeloDeMaquinaFiscalStr");
            return instanciaDal.ConnectFk(ref refXmlDocument,eProcessMessageType.SpName,"Adm.Gp_CajaSCH",valXmlParamsExpression);
        }

        System.Xml.Linq.XElement ILibPdn.GetFk(string valCallingModule,StringBuilder valParameters) {
            ILibDataComponent<IList<Caja>,IList<Caja>> instanciaDal = new Galac.Adm.Dal.Venta.clsCajaDat();
            return instanciaDal.QueryInfo(eProcessMessageType.SpName,"Adm.Gp_CajaGetFk",valParameters);
        }
        #endregion //Miembros de ILibPdn

        protected override bool RetrieveListInfo(string valModule,ref XmlDocument refXmlDocument,StringBuilder valXmlParamsExpression) {
            ILibPdn vPdnModule;
            bool vResult = false;
            switch(valModule) {
            case "Caja Registradora":
                vResult = ((ILibPdn)this).GetDataForList(valModule,ref refXmlDocument,valXmlParamsExpression);
                break;
            case "Usuario":
                vPdnModule = new LibGalac.Aos.Brl.Usal.LibGUserNav();
                vResult = vPdnModule.GetDataForList("Caja Registradora",ref refXmlDocument,valXmlParamsExpression);
                break;
            default: throw new NotImplementedException();
            }
            return vResult;
        }

        bool ICajaPdn.InsertarCajaPorDefecto(int valConsecutivoCompania) {
            try {
                ILibDataComponent<IList<Caja>,IList<Caja>> instanciaDal = new Galac.Adm.Dal.Venta.clsCajaDat();
                IList<Caja> vLista = new List<Caja>();
                Caja vCurrentRecord = new Caja();
                vCurrentRecord.ConsecutivoCompania = valConsecutivoCompania;
                vCurrentRecord.Consecutivo = 0;
                vCurrentRecord.NombreCaja = "CAJA GENÉRICA";
                vCurrentRecord.UsaGavetaAsBool = false;
                vCurrentRecord.PuertoAsEnum = ePuerto.COM1;
                vCurrentRecord.Comando = "";
                vCurrentRecord.PermitirAbrirSinSupervisorAsBool = false;
                vCurrentRecord.UsaAccesoRapidoAsBool = false;
                vCurrentRecord.UsaMaquinaFiscalAsBool = false;
                vCurrentRecord.FamiliaImpresoraFiscalAsEnum = eFamiliaImpresoraFiscal.EPSONPNP;
                vCurrentRecord.ModeloDeMaquinaFiscalAsEnum = eImpresoraFiscal.EPSON_PF_220;
                vCurrentRecord.SerialDeMaquinaFiscal = "";
                vCurrentRecord.PuertoMaquinaFiscalAsEnum = ePuerto.COM1;
                vCurrentRecord.AbrirGavetaDeDineroAsBool = false;
                vCurrentRecord.UltimoNumeroCompFiscal = "";
                vCurrentRecord.UltimoNumeroNCFiscal = "";
                vCurrentRecord.IpParaConexion = "";
                vCurrentRecord.MascaraSubred = "";
                vCurrentRecord.Gateway = "";
                vCurrentRecord.PermitirDescripcionDelArticuloExtendidaAsBool = false;
                vCurrentRecord.PermitirNombreDelClienteExtendidoAsBool = false;
                vCurrentRecord.UsarModoDotNetAsBool = false;
                vCurrentRecord.NombreOperador = "";
                vCurrentRecord.FechaUltimaModificacion = LibDate.Today();
                vLista.Add(vCurrentRecord);
                return instanciaDal.Insert(vLista).Success;
            } catch(Exception vEx) {
                if(LibString.S1IsInS2("Ya existe la clave primaria.",vEx.Message)) {
                    throw new GalacException("La caja por defecto ya existe para esta compañia ",eExceptionManagementType.Alert);
                } else {
                    throw vEx;
                }
            }
        }

        bool ICajaPdn.FindByConsecutivoCaja(int valConsecutivoCompania,int valConsecutivo,string valSqlWhere,ref XElement XmlResult) {
            bool vResult = false;
            LibGpParams vParams = new LibGpParams();
            try {
                vParams.AddInInteger("Consecutivo",valConsecutivo);
                vParams.AddInInteger("ConsecutivoCompania",valConsecutivoCompania);
                StringBuilder SQL = new StringBuilder();                
                SQL.AppendLine("SELECT Consecutivo,NombreCaja,UsaGaveta,Puerto,Comando,PermitirAbrirSinSupervisor,");
                SQL.AppendLine(" UsaAccesoRapido, UsaMaquinaFiscal, FamiliaImpresoraFiscal, ModeloDeMaquinaFiscal,");
                SQL.AppendLine(" SerialDeMaquinaFiscal, PuertoMaquinaFiscal, AbrirGavetaDeDinero, UltimoNumeroCompFiscal,");
                SQL.AppendLine(" UltimoNumeroNCFiscal ,TipoConexion, IpParaConexion, MascaraSubred,");
                SQL.AppendLine(" Gateway, PermitirDescripcionDelArticuloExtendida, PermitirNombreDelClienteExtendido, ");
                SQL.AppendLine(" UsarModoDotNet, NombreOperador, CONVERT(varchar,FechaUltimaModificacion,101) AS FechaUltimaModificacion ");
                SQL.AppendLine("FROM Adm.Caja WHERE Consecutivo = @Consecutivo");
                SQL.AppendLine("AND ConsecutivoCompania = @ConsecutivoCompania ");
                if(!LibString.IsNullOrEmpty(valSqlWhere)) {
                    SQL.AppendLine(" AND " + valSqlWhere);
                }
                XmlResult = LibBusiness.ExecuteSelect(SQL.ToString(),vParams.Get(),"",-1);
                vResult = (XmlResult != null && XmlResult.HasElements);
            } catch(Exception) {
                return false;
            }
            return vResult;
        }

        string ICajaPdn.BuscarModeloDeMaquinaFiscal(int valConsecutivoCompania,int valConsecutivo) {
            XElement xElement;
            StringBuilder sql = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            string vModelo = string.Empty;
            vParams.AddInInteger("Consecutivo",valConsecutivo);
            vParams.AddInInteger("ConsecutivoCompania",valConsecutivoCompania);
            sql.AppendLine(" SELECT ModeloDeMaquinaFiscal FROM Adm.Caja");
            sql.AppendLine(" WHERE Consecutivo = @Consecutivo");
            sql.AppendLine(" AND ConsecutivoCompania = @ConsecutivoCompania");
            xElement = LibBusiness.ExecuteSelect(sql.ToString(),vParams.Get(),string.Empty,0);
            if(xElement != null) {
                vModelo = LibXml.GetPropertyString(xElement,"ModeloDeMaquinaFiscal");
            }
            return vModelo;
        }

        bool ICajaPdn.ActualizaUltimoNumComprobante(int valConsecutivoCompania,int valConsecutivoCaja,string valNumero,bool valEsNotaDeCredito) {
            LibGpParams vParams = new LibGpParams();
            bool vResult = false;
            vParams.AddInInteger("ConsecutivoCompania",valConsecutivoCompania);
            vParams.AddInInteger("Consecutivo",valConsecutivoCaja);
            vParams.AddInString("Numero",valNumero,12);
            vParams.AddInBoolean("EsNotaDeCredito",false);
            vResult = new Galac.Adm.Dal.Venta.clsCajaDat().ActualizaUltimoNumComprobante(vParams.Get());
            return vResult;
        }

        private IImpresoraFiscalPdn InitializeImpresoraFiscal(XElement xmlCaja,int valCajaLocal) {
            IImpresoraFiscalPdn vImpresoraFiscal = null;
            clsImpresoraFiscalCreator vCreatorImpresoraFiscal = new clsImpresoraFiscalCreator();
            string vPuerto = LibXml.GetPropertyString(xmlCaja,"PuertoMaquinaFiscal");
            string vModelo = LibXml.GetPropertyString(xmlCaja,"ModeloDeMaquinaFiscal");
            SerialMaquinaFiscal = LibXml.GetPropertyString(xmlCaja,"SerialDeMaquinaFiscal");
            int vCajaDB = LibConvert.ToInt(LibXml.GetPropertyString(xmlCaja,"Consecutivo"));
            if(!LibString.IsNullOrEmpty(vPuerto) && !LibString.IsNullOrEmpty(vModelo) && valCajaLocal.Equals(vCajaDB)) {
                vImpresoraFiscal = vCreatorImpresoraFiscal.Crear(xmlCaja);
            }
            return vImpresoraFiscal;
        }

        XElement ICajaPdn.ValidateImpresoraFiscal(ref string refMensaje) {
            bool SeDetectoImpresoraFiscal = false;
            XElement xmlCajaDat = null;
            IImpresoraFiscalPdn vImpresoraFiscal = null;
            clsImpresoraFiscalNav insMaquinaFiscalNav;
            eStatusImpresorasFiscales refStatusPapel = new eStatusImpresorasFiscales();

            try {
                ICajaPdn insCaja = new clsCajaNav();
                int CajaLocal = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("Parametros","ConsecutivoCaja");
                int ConsecutivoCompania = LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania");
                if(!CajaLocal.Equals(0) && (ConsecutivoCompania > 0)) {
                    insCaja.FindByConsecutivoCaja(ConsecutivoCompania,CajaLocal,"",ref xmlCajaDat);
                    vImpresoraFiscal = InitializeImpresoraFiscal(xmlCajaDat,CajaLocal);
                    insMaquinaFiscalNav = new clsImpresoraFiscalNav(vImpresoraFiscal);
                    insMaquinaFiscalNav.SerialImpresoraFiscal = SerialMaquinaFiscal;
                    SeDetectoImpresoraFiscal = insMaquinaFiscalNav.DetectarImpresoraFiscal(ref refStatusPapel);
                    if(refStatusPapel.Equals(eStatusImpresorasFiscales.ePocoPapel)) {
                        refMensaje = "La impresora fiscal tiene poco papel";
                    }
                    if(!SeDetectoImpresoraFiscal) {
                        xmlCajaDat = null;
                    }
                }
                return xmlCajaDat;
            } catch(GalacException vEx) {
                refMensaje = vEx.Message;
                return null;
            }
        }

        void ICajaPdn.ActualizarRegistroDeMaquinaFiscal(eAccionSR valAccion,int valConsecutivoCompania,eImpresoraFiscal valModeloImpresoraFiscal,string valSerialMaquinaFiscal,string valUltimoNumeroComptbanteFiscal,string valNombreOperador) {
            StringBuilder Sql = new StringBuilder();
            LibGpParams vParam = new LibGpParams();
            Galac.Saw.Ccl.Tablas.IMaquinaFiscalPdn insMaquinaFiscalNav = new Saw.Brl.Tablas.clsMaquinaFiscalNav();
            string vConsecutivoMaquinaFiscal = "";
            bool HasPreviusRegistres = false;

            try {
                vParam.AddInInteger("ConsecutivoCompania",valConsecutivoCompania);
                vParam.AddInString("NumeroRegistro",valSerialMaquinaFiscal,20);
                Sql.AppendLine(" SELECT top(1) SAW.MaquinaFiscal.ConsecutivoMaquinaFiscal as ConsecutivoMaquinaFiscal ");
                Sql.AppendLine(",SAW.MaquinaFiscal.ConsecutivoCompania ");
                Sql.AppendLine("FROM SAW.MaquinaFiscal ");
                Sql.AppendLine("WHERE NumeroRegistro = @NumeroRegistro  AND ");
                Sql.AppendLine("SAW.MaquinaFiscal.ConsecutivoCompania = @ConsecutivoCompania ");
                Sql.AppendLine("ORDER BY ConsecutivoMaquinaFiscal DESC ");

                XElement vXmlResult = LibBusiness.ExecuteSelect(Sql.ToString(),vParam.Get(),"",0);
                if(vXmlResult != null && vXmlResult.HasElements) {
                    vConsecutivoMaquinaFiscal = LibXml.GetPropertyString(vXmlResult,"ConsecutivoMaquinaFiscal");
                    HasPreviusRegistres = LibString.Len(vConsecutivoMaquinaFiscal) > 0;
                }
                insMaquinaFiscalNav.GenerarRegistroDeMaquinaFiscal(valConsecutivoCompania,vConsecutivoMaquinaFiscal,valSerialMaquinaFiscal,LibString.UCase(valModeloImpresoraFiscal.GetDescription()),LibString.Len(valUltimoNumeroComptbanteFiscal),valNombreOperador);
            } catch(Exception vEx) {
                throw vEx;
            }
        }

        public XElement BuscarcajaAsignada() {
            StringBuilder vSql = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCaja",LibConvert.ToInt(LibAppSettings.ReadAppSettingsKey("CAJALOCAL")));
            vParams.AddInInteger("ConsecutivoCompania",LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"));
            vSql.AppendLine("SELECT UsaGaveta, Puerto, Comando FROM Adm.Caja WHERE Consecutivo = @ConsecutivoCaja AND ConsecutivoCompania = @ConsecutivoCompania");
            return LibBusiness.ExecuteSelect(vSql.ToString(),vParams.Get(),"",0);
        }

        bool ICajaPdn.ActualizarCierreXEnFacturas(int valConsecutivoCompania,int valConsecutivoCaja,DateTime valFechaModificacion,string valHoraDesde,string valHoraHasta) {
            bool vResult = false;
            StringBuilder vSql = new StringBuilder();
            QAdvSql _QAdvSql = new QAdvSql("");
            LibGpParams vParams = new LibGpParams();
            vParams.AddInBoolean("RealizoCierreX",true);
            vParams.AddInDateTime("FechaModificacion",valFechaModificacion);
            vParams.AddInInteger("ConsecutivoCaja",valConsecutivoCaja);
            vParams.AddInInteger("ConsecutivoCompania",valConsecutivoCompania);
            vSql.AppendLine("UPDATE Factura");
            vSql.AppendLine("SET RealizoCierreX = @RealizoCierreX");
            vSql.AppendLine("WHERE ");
            vSql.AppendLine(_QAdvSql.SqlValueBetween(" Fecha = @FechaModificacion ","HoraModificacion",valHoraDesde,valHoraHasta,"AND"));
            vSql.AppendLine(" AND TipoDeDocumento IN(" + _QAdvSql.EnumToSqlValue((int)eTipoDocumentoFactura.ComprobanteFiscal) + "," + _QAdvSql.EnumToSqlValue((int)eTipoDocumentoFactura.NotaDeCreditoComprobanteFiscal) + ")");
            vSql.AppendLine(" AND ConsecutivoCaja = @ConsecutivoCaja");
            vSql.AppendLine(" AND ConsecutivoCompania = @ConsecutivoCompania");
            vResult = LibBusiness.ExecuteUpdateOrDelete(vSql.ToString(),vParams.Get(),"",0) != 0;
            return vResult;
        }
        #endregion //Codigo Ejemplo
    } //End of class clsCajaNav
} //End of namespace Galac.Adm.Brl.Venta



