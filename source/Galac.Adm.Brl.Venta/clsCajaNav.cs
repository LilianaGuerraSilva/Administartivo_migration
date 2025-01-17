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
using Galac.Saw.Ccl.Tablas;
using Galac.Saw.Brl.Tablas;
using System.Threading;

namespace Galac.Adm.Brl.Venta {
    public partial class clsCajaNav: LibBaseNav<IList<Caja>, IList<Caja>>, ICajaPdn {
        #region Variables
        string _SerialMaquinaFiscal = "";
        #endregion //Variables
        #region Propiedades

        public string SerialMaquinaFiscal {
            get { return _SerialMaquinaFiscal; }
            set { _SerialMaquinaFiscal = value; }
        }

        string ICajaPdn.SerialMaquinaFiscal { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        #endregion //Propiedades
        #region Constructores

        public clsCajaNav() {
        }
        #endregion //Constructores
        #region Metodos Generados
        protected override ILibDataComponentWithSearch<IList<Caja>, IList<Caja>> GetDataInstance() {
            return new Galac.Adm.Dal.Venta.clsCajaDat();
        }
        #region Miembros de ILibPdn

        bool ILibPdn.CanBeChoosen(string valCallingModule, eAccionSR valAction, string valExtendedAction, XmlDocument valXmlRow) {
            bool vResult = false;
            switch (valCallingModule) {
                default:
                    vResult = true;
                    break;
            }
            return vResult;
        }

        bool ILibPdn.GetDataForList(string valCallingModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            ILibDataFKSearch instanciaDal = new Galac.Adm.Dal.Venta.clsCajaDat();
            valXmlParamsExpression.Replace("UsaMaquinaFiscalStr", "Gv_Caja_B1.UsaMaquinaFiscal");
            valXmlParamsExpression.Replace("ModeloMaquinaFiscalStr", "Gv_Caja_B1.ModeloDeMaquinaFiscalStr");
            return instanciaDal.ConnectFk(ref refXmlDocument, eProcessMessageType.SpName, "Adm.Gp_CajaSCH", valXmlParamsExpression);
        }

        System.Xml.Linq.XElement ILibPdn.GetFk(string valCallingModule, StringBuilder valParameters) {
            ILibDataComponent<IList<Caja>, IList<Caja>> instanciaDal = new Galac.Adm.Dal.Venta.clsCajaDat();
            return instanciaDal.QueryInfo(eProcessMessageType.SpName, "Adm.Gp_CajaGetFk", valParameters);
        }
        #endregion //Miembros de ILibPdn

        protected override bool RetrieveListInfo(string valModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            ILibPdn vPdnModule;
            bool vResult = false;
            switch (valModule) {
                case "Caja Registradora":
                    vResult = ((ILibPdn)this).GetDataForList(valModule, ref refXmlDocument, valXmlParamsExpression);
                    break;
                case "Usuario":
                    vPdnModule = new LibGalac.Aos.Brl.Usal.LibGUserNav();
                    vResult = vPdnModule.GetDataForList("Caja Registradora", ref refXmlDocument, valXmlParamsExpression);
                    break;
                default:
                    throw new NotImplementedException();
            }
            return vResult;
        }

        bool ICajaPdn.InsertarCajaPorDefecto(int valConsecutivoCompania) {
            try {
                ILibDataComponent<IList<Caja>, IList<Caja>> instanciaDal = new Galac.Adm.Dal.Venta.clsCajaDat();
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
                vCurrentRecord.RegistroDeRetornoEnTxtAsBool = false;
                vCurrentRecord.NombreOperador = "";
                vCurrentRecord.FechaUltimaModificacion = LibDate.Today();
                vLista.Add(vCurrentRecord);
                return instanciaDal.Insert(vLista).Success;
            } catch (Exception vEx) {
                if (LibString.S1IsInS2("Ya existe la clave primaria.", vEx.Message)) {
                    throw new GalacException("La caja por defecto ya existe para esta compañia ", eExceptionManagementType.Alert);
                } else {
                    throw vEx;
                }
            }
        }

        bool ICajaPdn.FindByConsecutivoCaja(int valConsecutivoCompania, int valConsecutivo, string valSqlWhere, ref XElement XmlResult) {
            bool vResult = false;
            LibGpParams vParams = new LibGpParams();
            try {
                vParams.AddInInteger("Consecutivo", valConsecutivo);
                vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
                StringBuilder SQL = new StringBuilder();
                SQL.AppendLine(" SELECT TOP(1) ISNULL(CajaApertura.NombreDelUsuario,'') AS NombreOperador,");
                SQL.AppendLine(" Caja.Consecutivo, NombreCaja,UsaGaveta,Puerto,Comando,PermitirAbrirSinSupervisor,");
                SQL.AppendLine(" UsaAccesoRapido, UsaMaquinaFiscal, FamiliaImpresoraFiscal, ModeloDeMaquinaFiscal,");
                SQL.AppendLine(" SerialDeMaquinaFiscal, PuertoMaquinaFiscal, AbrirGavetaDeDinero, UltimoNumeroCompFiscal,");
                SQL.AppendLine(" UltimoNumeroNCFiscal ,TipoConexion, IpParaConexion, MascaraSubred,");
                SQL.AppendLine(" Gateway, PermitirDescripcionDelArticuloExtendida, PermitirNombreDelClienteExtendido, ");
                SQL.AppendLine(" UsarModoDotNet, RegistroDeRetornoEnTxt,  CONVERT(varchar,Caja.FechaUltimaModificacion,101) AS FechaUltimaModificacion ");
                SQL.AppendLine(" FROM Adm.Caja ");
                SQL.AppendLine(" LEFT JOIN Adm.CajaApertura ON");
                SQL.AppendLine(" CajaApertura.ConsecutivoCompania = Caja.ConsecutivoCompania ");
                SQL.AppendLine(" AND CajaApertura.ConsecutivoCaja = Caja.Consecutivo ");
                if (!LibString.IsNullOrEmpty(valSqlWhere)) {
                    SQL.AppendLine(" INNER JOIN Comun.SettValueByCompany ON ");
                    SQL.AppendLine(" Comun.SettValueByCompany.ConsecutivoCompania = Caja.ConsecutivoCompania AND ");
                    SQL.AppendLine(" Comun.SettValueByCompany.NameSettDefinition = UsaMaquinaFiscal ");
                }
                SQL.AppendLine(" WHERE Caja.ConsecutivoCompania = @ConsecutivoCompania ");
                SQL.AppendLine(" AND Caja.Consecutivo = @Consecutivo ");                
                if (!LibString.IsNullOrEmpty(valSqlWhere)) {
                    SQL.AppendLine(" AND " + valSqlWhere);
                }
                SQL.AppendLine(" ORDER BY CajaApertura.NombreDelUsuario ASC ");
                XmlResult = LibBusiness.ExecuteSelect(SQL.ToString(), vParams.Get(), "", -1);
                vResult = (XmlResult != null && XmlResult.HasElements);                
            } catch (Exception) {
                return false;
            }
            return vResult;
        }

        string ICajaPdn.BuscarModeloDeMaquinaFiscal(int valConsecutivoCompania, int valConsecutivo) {
            XElement xElement;
            StringBuilder sql = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            string vModelo = string.Empty;
            vParams.AddInInteger("Consecutivo", valConsecutivo);
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            sql.AppendLine(" SELECT ModeloDeMaquinaFiscal FROM Adm.Caja");
            sql.AppendLine(" WHERE Consecutivo = @Consecutivo");
            sql.AppendLine(" AND ConsecutivoCompania = @ConsecutivoCompania");
            xElement = LibBusiness.ExecuteSelect(sql.ToString(), vParams.Get(), string.Empty, 0);
            if (xElement != null) {
                vModelo = LibXml.GetPropertyString(xElement, "ModeloDeMaquinaFiscal");
            }
            return vModelo;
        }

        bool ICajaPdn.ActualizaUltimoNumComprobante(int valConsecutivoCompania, int valConsecutivoCaja, string valNumero, bool valEsNotaDeCredito) {
            LibGpParams vParams = new LibGpParams();
            bool vResult = false;
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInInteger("Consecutivo", valConsecutivoCaja);
            vParams.AddInString("Numero", valNumero, 12);
            vParams.AddInBoolean("EsNotaDeCredito", false);
            vResult = new Galac.Adm.Dal.Venta.clsCajaDat().ActualizaUltimoNumComprobante(vParams.Get());
            return vResult;
        }

        private IImpresoraFiscalPdn InitializeImpresoraFiscal(XElement xmlCaja, int valCajaLocal) {
            IImpresoraFiscalPdn vImpresoraFiscal = null;
            clsImpresoraFiscalCreator vCreatorImpresoraFiscal = new clsImpresoraFiscalCreator();
            string vPuerto = LibXml.GetPropertyString(xmlCaja, "PuertoMaquinaFiscal");
            string vModelo = LibXml.GetPropertyString(xmlCaja, "ModeloDeMaquinaFiscal");
            string vRegistroDeRetornoEnTxt = LibXml.GetPropertyString(xmlCaja, "RegistroDeRetornoEnTxt");
            SerialMaquinaFiscal = LibXml.GetPropertyString(xmlCaja, "SerialDeMaquinaFiscal");
            int vCajaDB = LibConvert.ToInt(LibXml.GetPropertyString(xmlCaja, "Consecutivo"));
            if (!LibString.IsNullOrEmpty(vPuerto) && !LibString.IsNullOrEmpty(vModelo) && valCajaLocal.Equals(vCajaDB)) {
                vImpresoraFiscal = vCreatorImpresoraFiscal.Crear(xmlCaja);
            }
            return vImpresoraFiscal;
        }

        XElement ICajaPdn.ValidateImpresoraFiscal(ref string refMensaje) {//Todo cambio se debe adaptar en ValidaImpresoraFiscalVb
            bool SeDetectoImpresoraFiscal = false;
            XElement xmlCajaDat = null;
            IImpresoraFiscalPdn vImpresoraFiscal = null;
            clsImpresoraFiscalNav insMaquinaFiscalNav;
            eStatusImpresorasFiscales refStatusPapel = new eStatusImpresorasFiscales();
            try {
                ICajaPdn insCaja = new clsCajaNav();
                int CajaLocal = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("Parametros", "ConsecutivoCaja");
                int ConsecutivoCompania = LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania");               
                if (!CajaLocal.Equals(0) && (ConsecutivoCompania > 0)) {
                    insCaja.FindByConsecutivoCaja(ConsecutivoCompania, CajaLocal, "", ref xmlCajaDat);
                    vImpresoraFiscal = InitializeImpresoraFiscal(xmlCajaDat, CajaLocal);
                    insMaquinaFiscalNav = new clsImpresoraFiscalNav(vImpresoraFiscal);
                    insMaquinaFiscalNav.SerialImpresoraFiscal = SerialMaquinaFiscal;
                    SeDetectoImpresoraFiscal = insMaquinaFiscalNav.DetectarImpresoraFiscal(ref refStatusPapel);
                    if (refStatusPapel.Equals(eStatusImpresorasFiscales.ePocoPapel)) {
                        refMensaje = "La impresora fiscal tiene poco papel";
                    }
                    if (!SeDetectoImpresoraFiscal) {
                        xmlCajaDat = null;
                    }
                }
                return xmlCajaDat;
            } catch (GalacException vEx) {
                refMensaje = vEx.Message;
                return null;
            }
        }

        string ICajaPdn.ValidaImpresoraFiscalVb() { //Todo cambio se debe adaptar en ValidateImpresoraFiscal
            string vResult = string.Empty;
            try {
                int vCajaLocal = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("Parametros", "ConsecutivoCaja");
                int vConsecutivoCompania = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("FacturaRapida", "ConsecutivoCompania");
                if ((vCajaLocal != 0) && (vConsecutivoCompania > 0)) {
                    eStatusImpresorasFiscales refStatusPapel = new eStatusImpresorasFiscales();
                    XElement xmlCajaDat = null;
                    ICajaPdn insCaja = new clsCajaNav();
                    insCaja.FindByConsecutivoCaja(vConsecutivoCompania, vCajaLocal, "", ref xmlCajaDat);
                    IImpresoraFiscalPdn vImpresoraFiscal = InitializeImpresoraFiscal(xmlCajaDat, vCajaLocal);
                    clsImpresoraFiscalNav insIF = new clsImpresoraFiscalNav(vImpresoraFiscal);
                    insIF.SerialImpresoraFiscal = SerialMaquinaFiscal;
                    LibFile.WriteLineInFile(@"C:\Temp\pasos.txt", "ValidaImpresoraFiscalVb-9", false);
                    bool vSeDetectoImpresoraFiscal = insIF.DetectarImpresoraFiscalVb(ref refStatusPapel, ref vResult);
                    LibFile.WriteLineInFile(@"C:\Temp\pasos.txt", "ValidaImpresoraFiscalVb-10", true);
                    if (!vSeDetectoImpresoraFiscal) {
                        LibFile.WriteLineInFile(@"C:\Temp\pasos.txt", "ValidaImpresoraFiscalVb-11", true);
                        vResult +=  "\nNo se pudo detectar la impresora fiscal.";
                        LibFile.WriteLineInFile(@"C:\Temp\pasos.txt", "ValidaImpresoraFiscalVb-12", true);
                    } else if (refStatusPapel.Equals(eStatusImpresorasFiscales.ePocoPapel)) {
                        LibFile.WriteLineInFile(@"C:\Temp\pasos.txt", "ValidaImpresoraFiscalVb-13", true);
                        vResult += "\nLa impresora fiscal tiene poco papel.";
                        LibFile.WriteLineInFile(@"C:\Temp\pasos.txt", "ValidaImpresoraFiscalVb-14", true);
                    }
                }
            } catch (GalacException vEx) {
                vResult = vEx.Message;
            }
            return vResult;
        }

        void ICajaPdn.ActualizarRegistroDeMaquinaFiscal(eAccionSR valAccion, int valConsecutivoCompania, eImpresoraFiscal valModeloImpresoraFiscal, string valSerialMaquinaFiscal, string valUltimoNumeroComptbanteFiscal, string valNombreOperador) {
            StringBuilder Sql = new StringBuilder();
            LibGpParams vParam = new LibGpParams();
            Galac.Saw.Ccl.Tablas.IMaquinaFiscalPdn insMaquinaFiscalNav = new Saw.Brl.Tablas.clsMaquinaFiscalNav();
            string vConsecutivoMaquinaFiscal = "";
            bool HasPreviusRegistres = false;

            try {
                vParam.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
                vParam.AddInString("NumeroRegistro", valSerialMaquinaFiscal, 20);
                Sql.AppendLine(" SELECT top(1) SAW.MaquinaFiscal.ConsecutivoMaquinaFiscal as ConsecutivoMaquinaFiscal ");
                Sql.AppendLine(",SAW.MaquinaFiscal.ConsecutivoCompania ");
                Sql.AppendLine("FROM SAW.MaquinaFiscal ");
                Sql.AppendLine("WHERE NumeroRegistro = @NumeroRegistro  AND ");
                Sql.AppendLine("SAW.MaquinaFiscal.ConsecutivoCompania = @ConsecutivoCompania ");
                Sql.AppendLine("ORDER BY ConsecutivoMaquinaFiscal DESC ");

                XElement vXmlResult = LibBusiness.ExecuteSelect(Sql.ToString(), vParam.Get(), "", 0);
                if (vXmlResult != null && vXmlResult.HasElements) {
                    vConsecutivoMaquinaFiscal = LibXml.GetPropertyString(vXmlResult, "ConsecutivoMaquinaFiscal");
                    HasPreviusRegistres = LibString.Len(vConsecutivoMaquinaFiscal) > 0;
                }
                insMaquinaFiscalNav.GenerarRegistroDeMaquinaFiscal(valConsecutivoCompania, vConsecutivoMaquinaFiscal, valSerialMaquinaFiscal, LibString.UCase(valModeloImpresoraFiscal.GetDescription()), LibString.Len(valUltimoNumeroComptbanteFiscal), valNombreOperador);
            } catch (Exception vEx) {
                throw vEx;
            }
        }

        public XElement BuscarcajaAsignada() {
            StringBuilder vSql = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCaja", LibConvert.ToInt(LibAppSettings.ReadAppSettingsKey("CAJALOCAL")));
            vParams.AddInInteger("ConsecutivoCompania", LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"));
            vSql.AppendLine("SELECT UsaGaveta, Puerto, Comando FROM Adm.Caja WHERE Consecutivo = @ConsecutivoCaja AND ConsecutivoCompania = @ConsecutivoCompania");
            return LibBusiness.ExecuteSelect(vSql.ToString(), vParams.Get(), "", 0);
        }

        bool ICajaPdn.ActualizarCierreXEnFacturas(int valConsecutivoCompania, int valConsecutivoCaja, DateTime valFechaModificacion, string valHoraDesde, string valHoraHasta) {
            bool vResult = false;
            StringBuilder vSql = new StringBuilder();
            QAdvSql _QAdvSql = new QAdvSql("");
            LibGpParams vParams = new LibGpParams();
            vParams.AddInBoolean("RealizoCierreX", true);
            vParams.AddInDateTime("FechaModificacion", valFechaModificacion);
            vParams.AddInInteger("ConsecutivoCaja", valConsecutivoCaja);
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vSql.AppendLine("UPDATE Factura");
            vSql.AppendLine("SET RealizoCierreX = @RealizoCierreX");
            vSql.AppendLine("WHERE ");
            vSql.AppendLine(_QAdvSql.SqlValueBetween(" Fecha = @FechaModificacion ", "HoraModificacion", valHoraDesde, valHoraHasta, "AND"));
            vSql.AppendLine(" AND TipoDeDocumento IN(" + _QAdvSql.EnumToSqlValue((int)eTipoDocumentoFactura.ComprobanteFiscal) + "," + _QAdvSql.EnumToSqlValue((int)eTipoDocumentoFactura.NotaDeCreditoComprobanteFiscal) + ")");
            vSql.AppendLine(" AND ConsecutivoCaja = @ConsecutivoCaja");
            vSql.AppendLine(" AND ConsecutivoCompania = @ConsecutivoCompania");
            vResult = LibBusiness.ExecuteUpdateOrDelete(vSql.ToString(), vParams.Get(), "", 0) != 0;
            return vResult;
        }

        void BuscaFamiliaYModeloDeMaquinaFiscal(int valConsecutivoCompania, int valConsecutivo, ref string refCajaNombre, ref string refFamilia, ref string refModelo, ref string refSerial) {
            XElement xElement;
            StringBuilder sql = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            string vFamilia = string.Empty;
            string vModelo = string.Empty;
            vParams.AddInInteger("Consecutivo", valConsecutivo);
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            sql.AppendLine(" SELECT NombreCaja AS CajaNombre, FamiliaImpresoraFiscalStr AS FamiliaImpresoraFiscal, ModeloDeMaquinaFiscalStr AS ModeloDeMaquinaFiscal, SerialDeMaquinaFiscal FROM Adm.Gv_Caja_B1");
            sql.AppendLine(" WHERE Consecutivo = @Consecutivo");
            sql.AppendLine(" AND ConsecutivoCompania = @ConsecutivoCompania");
            xElement = LibBusiness.ExecuteSelect(sql.ToString(), vParams.Get(), string.Empty, 0);
            if (xElement != null) {
                refCajaNombre = LibXml.GetPropertyString(xElement, "CajaNombre");
                refFamilia = LibXml.GetPropertyString(xElement, "FamiliaImpresoraFiscal");
                refModelo = LibXml.GetPropertyString(xElement, "ModeloDeMaquinaFiscal");
                refSerial = LibXml.GetPropertyString(xElement, "SerialDeMaquinaFiscal");
            }
        }

        public bool HomologadaSegunGaceta43032(int valConsecutivoCompania, int valConsecutivoCaja, string valAccionDeAutorizacionDeProceso, ref string refMensaje) {
            string vFabricante= string.Empty;
            string vModelo = string.Empty;
            string vSerial = string.Empty;
            string vCajaNombre = string.Empty;
            refMensaje = string.Empty;
            BuscaFamiliaYModeloDeMaquinaFiscal(valConsecutivoCompania, valConsecutivoCaja, ref vCajaNombre, ref vFabricante, ref vModelo, ref vSerial);
            bool vResult = VerificaSiMaquinaFiscalEstaHomologada(vCajaNombre, vFabricante, vModelo,vSerial, valAccionDeAutorizacionDeProceso,ref refMensaje);
            return vResult;
        }

        bool ICajaPdn.ImpresoraFiscalEstaHomologada(int valConsecutivoCompania, int valConsecutivoCaja,  string valAccionDeAutorizacionDeProceso, ref string refMensaje) {
            return HomologadaSegunGaceta43032(valConsecutivoCompania, valConsecutivoCaja, valAccionDeAutorizacionDeProceso, ref refMensaje);
        }
        LibResponse ICajaPdn.ActualizarYAuditarCambiosMF(IList<Caja> refRecord, bool valAuditarMF, string valMotivoCambiosMaqFiscal, string valFamiliaOriginal, string valModeloOriginal, string valTipoDeConexionOriginal, string valSerialMFOriginal, string valUltNumComprobanteFiscalOriginal, string valUltNumNCFiscalOriginal) {
            string valoresOriginales = string.Empty;
            string valoresModificados = string.Empty;
            IAuditoriaConfiguracionPdn insPdn = new clsAuditoriaConfiguracionNav();
            RegisterClient();
            LibResponse result = base.UpdateRecord(refRecord);
            if (result.Success && valAuditarMF) {
                valoresOriginales = "ConsecutivoCaja: " + refRecord[0].Consecutivo + ",";
                valoresOriginales = valoresOriginales + "NombreCaja: " + refRecord[0].NombreCaja + ",";
                valoresOriginales = valoresOriginales + "Fabricante: " + valModeloOriginal + ",";
                valoresOriginales = valoresOriginales + "Modelo: " + valModeloOriginal + ",";
                valoresOriginales = valoresOriginales + "Serial: " + valSerialMFOriginal + ",";
                valoresOriginales = valoresOriginales + "Tipo de Conexión: " + valTipoDeConexionOriginal + ",";
                valoresOriginales = valoresOriginales + "Último num. comp. fiscal: " + valUltNumComprobanteFiscalOriginal + ",";
                valoresOriginales = valoresOriginales + "Último num. NC fiscal: " + valUltNumNCFiscalOriginal + ",";

                valoresModificados = "ConsecutivoCaja: " + refRecord[0].Consecutivo + ",";
                valoresModificados = valoresModificados + "NombreCaja: " + refRecord[0].NombreCaja + ",";
                valoresModificados = valoresModificados + "Fabricante: " + refRecord[0].FamiliaImpresoraFiscalAsString + ",";
                valoresModificados = valoresModificados + "Modelo: " + refRecord[0].ModeloDeMaquinaFiscalAsString + ",";
                valoresModificados = valoresModificados + "Serial: " + refRecord[0].SerialDeMaquinaFiscal + ",";
                valoresModificados = valoresModificados + "Tipo de Conexión: " + refRecord[0].TipoConexionAsString + ",";
                valoresModificados = valoresModificados + "Último num. comp. fiscal: " + refRecord[0].UltimoNumeroCompFiscal + ",";
                valoresModificados = valoresModificados + "Último num. NC fiscal: " + refRecord[0].UltimoNumeroNCFiscal + ",";

                insPdn.Auditar(valMotivoCambiosMaqFiscal
                 , "MODIFICAR"
                 , valoresOriginales
                 , valoresModificados
                 );
            }
            return result;
        }
        #endregion //Codigo Ejemplo
        protected override LibResponse InsertRecord(IList<Caja> refRecord) {
            RegisterClient();
            LibResponse result = base.InsertRecord(refRecord);
            IAuditoriaConfiguracionPdn insPdn = new clsAuditoriaConfiguracionNav();
            if (result.Success) { //ojo si se empieza a insertar en lote hay que cambiar etso
                var currentRecord = refRecord[0];
                insPdn.Auditar("Configuración inicial"
                        ,"INSERTAR"
                        , string.Empty
                        ,"ConsecutivoCaja:" + currentRecord.Consecutivo
                            + ", NombreCaja:" + currentRecord.NombreCaja
                            + ", Fabricante:" + currentRecord.FamiliaImpresoraFiscalAsString
                            + ", Modelo:" + currentRecord.ModeloDeMaquinaFiscalAsString
                            + ", Serial:" + currentRecord.SerialDeMaquinaFiscal
                            + ", Tipo de conexión:" + currentRecord.TipoConexionAsString
                            + ", Último num. comp. fiscal:" + currentRecord.UltimoNumeroCompFiscal
                            + ", Último num. NC fiscal:" + currentRecord.UltimoNumeroNCFiscal
                        );
            }
            return result;
        }

        bool ICajaPdn.ImpresoraFiscalEstaHomologada(string valCajaNombre, string valFabricante, string valModelo, string valSerial, string valAccionDeAutorizacionDeProceso, ref string refMensaje) {
            bool vResult = VerificaSiMaquinaFiscalEstaHomologada(valCajaNombre, valFabricante, valModelo, valSerial, valAccionDeAutorizacionDeProceso, ref refMensaje);
            return vResult;
        }
        bool VerificaSiMaquinaFiscalEstaHomologada(string valCajaNombre, string valFabricante, string valModelo, string valSerial, string valAccionDeAutorizacionDeProceso, ref string refMensaje) {
            bool vResult = true;
            clsCajaProcesos insCajaProcesos = new clsCajaProcesos();
            if (!insCajaProcesos.SendPostEstaHomologadaMaquinaFiscal(valCajaNombre, valFabricante
                , valModelo, valSerial
                , ((CustomIdentity)Thread.CurrentPrincipal.Identity).Login
                , valAccionDeAutorizacionDeProceso)) {
                refMensaje = "La impresora fiscal " + valFabricante + ", modelo " + valModelo + ", no se encuentra homologada.";
                vResult = false;
            }
            return vResult;
        }

    } //End of class clsCajaNav
} //End of namespace Galac.Adm.Brl.Venta


