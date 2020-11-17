using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Security;
using System.Security.Permissions;
using System.Xml;
using System.Xml.Linq;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Dal;
using LibGalac.Aos.Catching;
using LibGalac.Aos.Dal;
using LibGalac.Aos.DefGen;
using Entity = Galac.Saw.Ccl.Cliente;
using Galac.Saw.Ccl.Cliente;

namespace Galac.Saw.Dal.Cliente {
    public class clsClienteDat : LibData, ILibDataComponentWithSearch<IList<Entity.Cliente>, IList<Entity.Cliente>> {
        #region Variables
        Entity.Cliente _CurrentRecord;
        #endregion //Variables
        #region Propiedades
        private Entity.Cliente CurrentRecord {
            get { return _CurrentRecord; }
            set { _CurrentRecord = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public clsClienteDat() {
            DbSchema = "dbo";
        }
        #endregion //Constructores
        #region Metodos Generados

        private StringBuilder ParametrosActualizacion(Entity.Cliente valRecord, eAccionSR valAction) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInDateFormat("DateFormat");
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInInteger("Consecutivo", valRecord.Consecutivo);
            vParams.AddInString("Codigo", valRecord.Codigo, 10);
            vParams.AddInString("Nombre", valRecord.Nombre, 80);
            vParams.AddInString("NumeroRIF", valRecord.NumeroRIF, 20);
            vParams.AddInString("NumeroNIT", valRecord.NumeroNIT, 12);
            vParams.AddInString("Direccion", valRecord.Direccion, 255);
            vParams.AddInString("Ciudad", valRecord.Ciudad, 100);
            vParams.AddInString("ZonaPostal", valRecord.ZonaPostal, 7);
            vParams.AddInString("Telefono", valRecord.Telefono, 40);
            vParams.AddInString("FAX", valRecord.FAX, 25);
            vParams.AddInEnum("Status", valRecord.StatusAsDB);
            vParams.AddInString("Contacto", valRecord.Contacto, 35);
            vParams.AddInString("ZonaDeCobranza", valRecord.ZonaDeCobranza, 20);
            vParams.AddInString("CodigoVendedor", valRecord.CodigoVendedor, 5);
            vParams.AddInString("RazonInactividad", valRecord.RazonInactividad, 35);
            vParams.AddInString("Email", valRecord.Email, 40);
            vParams.AddInBoolean("ActivarAvisoAlEscoger", valRecord.ActivarAvisoAlEscogerAsBool);
            vParams.AddInString("TextoDelAviso", valRecord.TextoDelAviso, 150);
            vParams.AddInString("CuentaContableCxC", valRecord.CuentaContableCxC, 30);
            vParams.AddInString("CuentaContableIngresos", valRecord.CuentaContableIngresos, 30);
            vParams.AddInString("CuentaContableAnticipo", valRecord.CuentaContableAnticipo, 30);
            vParams.AddInString("InfoGalac", valRecord.InfoGalac, 1);
            vParams.AddInString("SectorDeNegocio", valRecord.SectorDeNegocio, 20);
            vParams.AddInString("CodigoLote", valRecord.CodigoLote, 10);
            vParams.AddInEnum("NivelDePrecio", valRecord.NivelDePrecioAsDB);
            vParams.AddInEnum("Origen", valRecord.OrigenAsDB);
            vParams.AddInInteger("DiaCumpleanos", valRecord.DiaCumpleanos);
            vParams.AddInInteger("MesCumpleanos", valRecord.MesCumpleanos);
            vParams.AddInBoolean("CorrespondenciaXEnviar", valRecord.CorrespondenciaXEnviarAsBool);
            vParams.AddInBoolean("EsExtranjero", valRecord.EsExtranjeroAsBool);
            vParams.AddInDateTime("ClienteDesdeFecha", valRecord.ClienteDesdeFecha);
            vParams.AddInString("AQueSeDedicaElCliente", valRecord.AQueSeDedicaElCliente, 100);
            vParams.AddInEnum("TipoDocumentoIdentificacion", valRecord.TipoDocumentoIdentificacionAsDB);
            vParams.AddInEnum("TipoDeContribuyente", valRecord.TipoDeContribuyenteAsDB);
            vParams.AddInString("CampoDefinible1", valRecord.CampoDefinible1, 20);
            vParams.AddInString("NombreOperador", ((CustomIdentity) Thread.CurrentPrincipal.Identity).Login, 10);
            vParams.AddInDateTime("FechaUltimaModificacion", LibDate.Today());
            if (valAction == eAccionSR.Modificar) {
                vParams.AddInTimestamp("TimeStampAsInt", valRecord.fldTimeStamp);
            }
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosClave(Entity.Cliente valRecord, bool valIncludeTimestamp, bool valAddReturnParameter) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            if (valAddReturnParameter) {
                vParams.AddReturn();
            }
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            //vParams.AddInString("Codigo", valRecord.Codigo, 10);
            vParams.AddInString("NumeroRIF", valRecord.NumeroRIF, 20);
            if (valIncludeTimestamp) {
                vParams.AddInTimestamp("TimeStampAsInt", valRecord.fldTimeStamp);
            }
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosProximoConsecutivo(Entity.Cliente valRecord) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vResult = vParams.Get();
            return vResult;
        }
        #region Miembros de ILibDataComponent<IList<Cliente>, IList<Cliente>>

        LibResponse ILibDataComponent<IList<Entity.Cliente>, IList<Entity.Cliente>>.CanBeChoosen(IList<Entity.Cliente> refRecord, eAccionSR valAction) {
            LibResponse vResult = new LibResponse();
            Entity.Cliente vRecord = refRecord[0];
            StringBuilder vSbInfo = new StringBuilder();
            string vErrMsg = "";
            LibDatabase insDB = new LibDatabase(clsCkn.ConfigKeyForDbService);
            if (valAction == eAccionSR.Eliminar) {
                //if (insDB.ExistsValueOnMultifile("dbo.CxC", "CodigoCliente", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.codigo), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                //    vSbInfo.AppendLine("Cx C");
                //}
                //if (insDB.ExistsValueOnMultifile("dbo.Cotizacion", "CodigoCliente", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.codigo), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                //    vSbInfo.AppendLine("Cotizacion");
                //}
                //if (insDB.ExistsValueOnMultifile("dbo.ParametrosCompania", "CodigoGenericoCliente", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.codigo), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                //    vSbInfo.AppendLine("Parametros Compania");
                //}
                //if (insDB.ExistsValueOnMultifile("dbo.Cobranza", "CodigoCliente", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.codigo), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                //    vSbInfo.AppendLine("Cobranza");
                //}
                //if (insDB.ExistsValueOnMultifile("dbo.Factura", "CodigoCliente", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.codigo), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                //    vSbInfo.AppendLine("Factura");
                //}
                //if (insDB.ExistsValueOnMultifile("dbo.FacturaMayorista", "CodigoCliente", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Codigo), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                //    vSbInfo.AppendLine("Factura Mayorista");
                //}
                //if (insDB.ExistsValueOnMultifile("dbo.FacturaMayorista", "CodigoVendedor", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Codigo), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                //    vSbInfo.AppendLine("Factura Mayorista");
                //}
                //if (insDB.ExistsValueOnMultifile("dbo.Autorizacion", "CodigoCliente", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.codigo), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                //    vSbInfo.AppendLine("Autorizacion");
                //}
                //if (insDB.ExistsValueOnMultifile("dbo.Llamada", "CodigoCliente", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.codigo), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                //    vSbInfo.AppendLine("Llamada");
                //}
                //if (insDB.ExistsValueOnMultifile("dbo.Visita", "CodigoCliente", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.codigo), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                //    vSbInfo.AppendLine("Visita");
                //}
                //if (insDB.ExistsValueOnMultifile("dbo.Visita", "Telefono", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.telefono), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                //    vSbInfo.AppendLine("Visita");
                //}
                //if (insDB.ExistsValueOnMultifile("dbo.Visita", "Direccion", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Direccion), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                //    vSbInfo.AppendLine("Visita");
                //}
                //if (insDB.ExistsValueOnMultifile("dbo.Visita", "ZonaPostal", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.zonapostal), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                //    vSbInfo.AppendLine("Visita");
                //}
                //if (insDB.ExistsValueOnMultifile("Saw.Almacen", "CodigoCliente", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.codigo), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                //    vSbInfo.AppendLine("Almacén");
                //}
                //if (insDB.ExistsValueOnMultifile("dbo.NotaDeEntradaSalida", "CodigoCliente", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.codigo), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                //    vSbInfo.AppendLine("Nota de Entrada/Salida");
                //}
                //if (insDB.ExistsValueOnMultifile("dbo.DireccionDeDespacho", "CodigoCliente", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.codigo), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                //    vSbInfo.AppendLine("Direccion De Despacho");
                //}
                //if (insDB.ExistsValueOnMultifile("dbo.Despacho", "CodigoCliente", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.codigo), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                //    vSbInfo.AppendLine("Despacho");
                //}
                //if (insDB.ExistsValueOnMultifile("dbo.Despacho", "CiudadDestino", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.ciudad), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                //    vSbInfo.AppendLine("Despacho");
                //}
                //if (insDB.ExistsValueOnMultifile("dbo.Despacho", "DireccionDestino", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Direccion), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                //    vSbInfo.AppendLine("Despacho");
                //}
                //if (insDB.ExistsValueOnMultifile("dbo.OrdenDeServicio", "CodigoCliente", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.codigo), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                //    vSbInfo.AppendLine("Orden De Servicio");
                //}
                //if (insDB.ExistsValueOnMultifile("dbo.Participante", "Cliente", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.nombre), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                //    vSbInfo.AppendLine("Participante");
                //}
                //if (insDB.ExistsValueOnMultifile("dbo.Contrato", "CodigoCliente", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.codigo), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                //    vSbInfo.AppendLine("Contrato");
                //}
                //if (insDB.ExistsValueOnMultifile("dbo.ClaveSuperUtilitario", "CodigoCliente", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.codigo), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                //    vSbInfo.AppendLine("Clave Super Utilitario");
                //}
                //if (insDB.ExistsValueOnMultifile("dbo.RevisionDeData", "CodigoCliente", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.codigo), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                //    vSbInfo.AppendLine("Revision De Data");
                //}
                //if (insDB.ExistsValueOnMultifile("dbo.CorreccionDeFechas", "CodigoCliente", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.codigo), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                //    vSbInfo.AppendLine("Correccion De Fechas");
                //}
                //if (insDB.ExistsValueOnMultifile("dbo.CorreccionDeFechas", "NombreCliente", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.nombre), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                //    vSbInfo.AppendLine("Correccion De Fechas");
                //}
                //if (insDB.ExistsValueOnMultifile("dbo.MailFax", "CodigoCliente", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.codigo), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                //    vSbInfo.AppendLine("Mail Fax");
                //}
                //if (insDB.ExistsValueOnMultifile("dbo.GestionDeCobranza", "CodigoCliente", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.codigo), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                //    vSbInfo.AppendLine("Gestión de Cobranza");
                //}
                //if (insDB.ExistsValueOnMultifile("dbo.ConversionIVA", "CodigoCliente", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.codigo), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                //    vSbInfo.AppendLine("Conversion IVA");
                //}
                //if (insDB.ExistsValueOnMultifile("dbo.ConversionIVA", "NombreCliente", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.nombre), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                //    vSbInfo.AppendLine("Conversion IVA");
                //}
                //if (insDB.ExistsValueOnMultifile("dbo.ConversionIVA", "Telefono", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Telefono), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                //    vSbInfo.AppendLine("Conversion IVA");
                //}
                //if (insDB.ExistsValueOnMultifile("dbo.Anticipo", "CodigoCliente", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.codigo), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                //    vSbInfo.AppendLine("Anticipo");
                //}
                //if (insDB.ExistsValueOnMultifile("dbo.NotaDeEntrega", "CodigoCliente", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.codigo), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                //    vSbInfo.AppendLine("Nota De Entrega");
                //}
                //if (insDB.ExistsValueOnMultifile("Saw.Vehiculo", "CodigoCliente", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Codigo), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                //    vSbInfo.AppendLine("Vehiculo");
                //}
                if (vSbInfo.Length == 0) {
                    vResult.Success = true;
                }
            } else {
                vResult.Success = true;
            }
            insDB.Dispose();
            if (!vResult.Success) {
                vErrMsg = LibResMsg.InfoForeignKeyCanNotBeDeleted(vSbInfo.ToString());
                throw new GalacAlertException(vErrMsg);
            } else {
                return vResult;
            }
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Cliente.Eliminar")]
        LibResponse ILibDataComponent<IList<Entity.Cliente>, IList<Entity.Cliente>>.Delete(IList<Entity.Cliente> refRecord) {
            LibResponse vResult = new LibResponse();
            string vErrMsg = "";
            CurrentRecord = refRecord[0];
            if (Validate(eAccionSR.Eliminar, out vErrMsg)) {
                LibDatabase insDb = new LibDatabase(clsCkn.ConfigKeyForDbService);
                vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "ClienteDEL"), ParametrosClave(CurrentRecord, true, true));
                insDb.Dispose();
            } else {
                throw new GalacValidationException(vErrMsg);
            }
            return vResult;
        }

        IList<Entity.Cliente> ILibDataComponent<IList<Entity.Cliente>, IList<Entity.Cliente>>.GetData(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters) {
            List<Entity.Cliente> vResult = new List<Entity.Cliente>();
            LibDatabase insDb = new LibDatabase(clsCkn.ConfigKeyForDbService);
            switch (valType) {
                case eProcessMessageType.SpName:
                    valProcessMessage = insDb.ToSpName(DbSchema, valProcessMessage);
                    vResult = insDb.LoadFromSp<Entity.Cliente>(valProcessMessage, valParameters, CmdTimeOut);
                    break;
                case eProcessMessageType.Query:
                    vResult = insDb.LoadData<Entity.Cliente>(valProcessMessage, CmdTimeOut, valParameters);
                    break;
                default: break;
            }
            insDb.Dispose();
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Cliente.Insertar")]
        LibResponse ILibDataComponent<IList<Entity.Cliente>, IList<Entity.Cliente>>.Insert(IList<Entity.Cliente> refRecord) {
            LibResponse vResult = new LibResponse();
            string vErrMsg = "";
            CurrentRecord = refRecord[0];
            if (ExecuteProcessBeforeInsert()) {
                if (Validate(eAccionSR.Insertar, out vErrMsg)) {
                    LibDatabase insDb = new LibDatabase(clsCkn.ConfigKeyForDbService);
                    CurrentRecord.Consecutivo = insDb.NextLngConsecutive(DbSchema + ".Cliente", "Consecutivo", ParametrosProximoConsecutivo(CurrentRecord));
                    vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "ClienteINS"), ParametrosActualizacion(CurrentRecord, eAccionSR.Insertar));
                    insDb.Dispose();
                } else {
                    throw new GalacValidationException(vErrMsg);
                }
            }
            return vResult;
        }

        XElement ILibDataComponent<IList<Entity.Cliente>, IList<Entity.Cliente>>.QueryInfo(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters) {
            XElement vResult = null;
            LibDatabase insDb = new LibDatabase(clsCkn.ConfigKeyForDbService);
            switch (valType) {
                case eProcessMessageType.Message:
                    if (valProcessMessage.Equals("ProximoCodigo")) {
                        vResult = LibXml.ValueToXElement(insDb.NextStrConsecutive(DbSchema + ".Cliente", "Codigo", valParameters.ToString(), true, 10, true), "Codigo");
                    }
                    break;
                case eProcessMessageType.SpName:
                    vResult = LibXml.ToXElement(insDb.LoadFromSp(valProcessMessage, valParameters, CmdTimeOut));
                    break;
                case eProcessMessageType.Query:
                    vResult = LibXml.ToXElement(insDb.LoadData(valProcessMessage, CmdTimeOut, valParameters));
                    break;
                default: break;
            }
            insDb.Dispose();
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Cliente.Modificar")]
        LibResponse ILibDataComponent<IList<Entity.Cliente>, IList<Entity.Cliente>>.Update(IList<Entity.Cliente> refRecord) {
            LibResponse vResult = new LibResponse();
            string vErrMsg ="";
            CurrentRecord = refRecord[0];
            if (ExecuteProcessBeforeUpdate()) {
                if (Validate(eAccionSR.Modificar, out vErrMsg)) {
                    LibDatabase insDb = new LibDatabase(clsCkn.ConfigKeyForDbService);
                    vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "ClienteUPD"), ParametrosActualizacion(CurrentRecord, eAccionSR.Modificar));
                    insDb.Dispose();
                } else {
                    throw new GalacValidationException(vErrMsg);
                }
            }
            return vResult;
        }

        bool ILibDataComponent<IList<Entity.Cliente>, IList<Entity.Cliente>>.ValidateAll(IList<Entity.Cliente> refRecords, eAccionSR valAction, StringBuilder refErrorMessage) {
            bool vResult = true;
            string vErroMessage = "";
            foreach (Entity.Cliente vItem in refRecords) {
                CurrentRecord = vItem;
                vResult = vResult && Validate(valAction, out vErroMessage);
                if (LibString.IsNullOrEmpty(vErroMessage, true)) {
                    refErrorMessage.AppendLine(vErroMessage);
                }
            }
            return vResult;
        }

        LibResponse ILibDataComponent<IList<Entity.Cliente>, IList<Entity.Cliente>>.SpecializedUpdate(IList<Entity.Cliente> refRecord, string valSpecializedAction) {
            throw new NotImplementedException();
        }
        #endregion //ILibDataComponent<IList<Cliente>, IList<Cliente>>
        #region Validaciones
        protected override bool Validate(eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            ClearValidationInfo();
            vResult = IsValidConsecutivoCompania(valAction, CurrentRecord.ConsecutivoCompania, CurrentRecord.Codigo);
            vResult = IsValidCodigo(valAction, CurrentRecord.ConsecutivoCompania, CurrentRecord.Codigo) && vResult;            
            vResult = IsValidNumeroRIF(valAction, CurrentRecord.ConsecutivoCompania, CurrentRecord.NumeroRIF) && vResult;
            outErrorMessage = Information.ToString();
            return vResult;
        }

        private bool IsValidConsecutivoCompania(eAccionSR valAction, int valConsecutivoCompania, string valCodigo){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valConsecutivoCompania <= 0) {
                BuildValidationInfo(MsgRequiredField("Consecutivo Compania"));
                vResult = false;
            }
            return vResult;
        }
        private bool IsValidConsecutivo(eAccionSR valAction, int valConsecutivo){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valConsecutivo == 0) {
                BuildValidationInfo(MsgRequiredField("Consecutivo"));
                vResult = false;
            }
            return vResult;
        }

        private bool IsValidCodigo(eAccionSR valAction, int valConsecutivoCompania, string valCodigo){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCodigo = LibString.Trim(valCodigo);
            if (LibString.IsNullOrEmpty(valCodigo, true)) {
                BuildValidationInfo(MsgRequiredField("Código"));
                vResult = false;
            } else if (valAction == eAccionSR.Insertar) {
                LibGpParams vParams = new LibGpParams();
                vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
                vParams.AddInString("Codigo", valCodigo, 10);
                if (KeyExists(valConsecutivoCompania, vParams)) {
                    BuildValidationInfo(MsgFieldValueAlreadyExist("Código", valCodigo));
                    vResult = false;
                }
            }
            return vResult;
        }       

        private bool IsValidNumeroRIF(eAccionSR valAction, int valConsecutivoCompania, string valNumeroRIF){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valNumeroRIF = LibString.Trim(valNumeroRIF);
            if (LibString.IsNullOrEmpty(valNumeroRIF, true)) {
                BuildValidationInfo(MsgRequiredField("Cédula"));
                vResult = false;
            } else if (valAction == eAccionSR.Insertar) {
                Entity.Cliente vRecBusqueda = new Entity.Cliente();
                vRecBusqueda.NumeroRIF = valNumeroRIF;
                LibGpParams vParams = new LibGpParams();
                vParams.AddInInteger("ConsecutivoCompania", vRecBusqueda.ConsecutivoCompania);
                vParams.AddInString("NumeroRIF", vRecBusqueda.NumeroRIF, 20);
                if (KeyExists(valConsecutivoCompania, vParams)) {
                    BuildValidationInfo(MsgFieldValueAlreadyExist("Cédula", valNumeroRIF));
                    vResult = false;
                }
            }
            return vResult;
        }


        private bool KeyExists(int valConsecutivoCompania, string valCodigo) {
            bool vResult = false;
            Entity.Cliente vRecordBusqueda = new Entity.Cliente();
            vRecordBusqueda.ConsecutivoCompania = valConsecutivoCompania;
            vRecordBusqueda.Codigo = valCodigo;
            LibDatabase insDb = new LibDatabase(clsCkn.ConfigKeyForDbService);
            vResult = insDb.ExistsRecord(DbSchema + ".Cliente", "ConsecutivoCompania", ParametrosClave(vRecordBusqueda, false, false));
            insDb.Dispose();
            return vResult;
        }

        private bool KeyExists(int valConsecutivoCompania, LibGpParams valParams) {
            bool vResult = false;            
            LibDatabase insDb = new LibDatabase(clsCkn.ConfigKeyForDbService);
            vResult = insDb.ExistsRecord(DbSchema + ".Cliente", "ConsecutivoCompania", valParams.Get());
            insDb.Dispose();
            return vResult;
        }
        #endregion //Validaciones
        #region Miembros de ILibDataFKSearch
        bool ILibDataFKSearch.ConnectFk(ref XmlDocument refResulset, eProcessMessageType valType, string valProcessMessage, StringBuilder valXmlParamsExpression) {
            bool vResult = false;
            LibDatabase insDb = new LibDatabase(clsCkn.ConfigKeyForDbService);
            refResulset = insDb.LoadForConnect(valProcessMessage, valXmlParamsExpression, CmdTimeOut);
            if (refResulset != null && refResulset.DocumentElement != null && refResulset.DocumentElement.HasChildNodes) {
                vResult = true;
            }
            return vResult;
        }
        #endregion //Miembros de ILibDataFKSearch
        #endregion //Metodos Generados


    } //End of class clsClienteDat

} //End of namespace Galac.Saw.Dal.Clientes

