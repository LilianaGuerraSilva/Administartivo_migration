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
using System.Data;

namespace Galac.Saw.Dal.Cliente {
    public class clsClienteDat : LibData, ILibDataMasterComponentWithSearch<IList<Entity.Cliente>, IList<Entity.Cliente>>, ILibDataRpt {
        #region Variables
        LibTrn insTrn;
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
            insTrn = new LibTrn();
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
            vParams.AddInString("ZonaDeCobranza", valRecord.ZonaDeCobranza, 100);
            vParams.AddInInteger("ConsecutivoVendedor", valRecord.ConsecutivoVendedor);
            vParams.AddInString("CodigoVendedor", valRecord.CodigoVendedor, 5);
            vParams.AddInString("RazonInactividad", valRecord.RazonInactividad, 35);
            vParams.AddInString("Email", valRecord.Email, 100);
            vParams.AddInBoolean("ActivarAvisoAlEscoger", valRecord.ActivarAvisoAlEscogerAsBool);
            vParams.AddInString("TextoDelAviso", valRecord.TextoDelAviso, 150);
            vParams.AddInString("CuentaContableCxC", valRecord.CuentaContableCxC, 30);
            vParams.AddInString("CuentaContableIngresos", valRecord.CuentaContableIngresos, 30);
            vParams.AddInString("CuentaContableAnticipo", valRecord.CuentaContableAnticipo, 30);
            vParams.AddInEnum("InfoGalac", valRecord.InfoGalacAsDB);
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
            vParams.AddInString("CampoDefinible1", valRecord.CampoDefinible1, 60);
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
        #region Miembros de ILibDataMasterComponent<IList<Cliente>, IList<Cliente>>

        LibResponse ILibDataMasterComponent<IList<Entity.Cliente>, IList<Entity.Cliente>>.CanBeChoosen(IList<Entity.Cliente> refRecord, eAccionSR valAction) {
            LibResponse vResult = new LibResponse();
            Entity.Cliente vRecord = refRecord[0];
            StringBuilder vSbInfo = new StringBuilder();
            string vErrMsg = "";
            LibDatabase insDB = new LibDatabase(clsCkn.ConfigKeyForDbService);
            if (valAction == eAccionSR.Eliminar) {
                if (insDB.ExistsValueOnMultifile("dbo.CxC", "CodigoCliente", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Codigo), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                    vSbInfo.AppendLine("Cx C");
                }
                if (insDB.ExistsValueOnMultifile("dbo.Cotizacion", "CodigoCliente", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Codigo), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                   vSbInfo.AppendLine("Cotizacion");
                }
                if (insDB.ExistsValueOnMultifile("dbo.ParametrosCompania", "CodigoGenericoCliente", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Codigo), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                    vSbInfo.AppendLine("Parametros Compania");
                }
                if (insDB.ExistsValueOnMultifile("dbo.Cobranza", "CodigoCliente", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Codigo), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                    vSbInfo.AppendLine("Cobranza");
                }
                if (insDB.ExistsValueOnMultifile("dbo.Factura", "CodigoCliente", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Codigo), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                    vSbInfo.AppendLine("Facturas");
                }
                if (insDB.ExistsValueOnMultifile("dbo.FacturaMayorista", "CodigoCliente", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Codigo), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                    vSbInfo.AppendLine("Factura Mayorista");
                }
                if (insDB.ExistsValueOnMultifile("dbo.FacturaMayorista", "CodigoVendedor", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Codigo), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                    vSbInfo.AppendLine("Factura Mayorista");
                }
                if (insDB.ExistsValueOnMultifile("dbo.Autorizacion", "CodigoCliente", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Codigo), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                    vSbInfo.AppendLine("Autorizacion");
                }
            	if (insDB.ExistsValueOnMultifile("dbo.Llamada", "CodigoCliente", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Codigo), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                    vSbInfo.AppendLine("Llamada");
                }
                if (insDB.ExistsValueOnMultifile("dbo.Visita", "CodigoCliente", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Codigo), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                    vSbInfo.AppendLine("Visita");
                }
                if (insDB.ExistsValueOnMultifile("dbo.Almacen", "CodigoCliente", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Codigo), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                    vSbInfo.AppendLine("Almac?n");
                }
                if (insDB.ExistsValueOnMultifile("dbo.NotaDeEntradaSalida", "CodigoCliente", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Codigo), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                    vSbInfo.AppendLine("Nota de Entrada/Salida");
                }
                if (insDB.ExistsValueOnMultifile("dbo.DireccionDeDespacho", "CodigoCliente", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Codigo), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                    vSbInfo.AppendLine("Direccion De Despacho");
                }
                if (insDB.ExistsValueOnMultifile("dbo.Despacho", "CodigoCliente", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Codigo), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                    vSbInfo.AppendLine("Despacho");
                }
                if (insDB.ExistsValueOnMultifile("dbo.Despacho", "CiudadDestino", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Ciudad), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                    vSbInfo.AppendLine("Despacho");
                }
                if (insDB.ExistsValueOnMultifile("dbo.Despacho", "DireccionDestino", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Direccion), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                    vSbInfo.AppendLine("Despacho");
                }
                if (insDB.ExistsValueOnMultifile("dbo.OrdenDeServicio", "CodigoCliente", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Codigo), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                    vSbInfo.AppendLine("Orden De Servicio");
                }
                if (insDB.ExistsValueOnMultifile("dbo.Participante", "Cliente", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Nombre), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                    vSbInfo.AppendLine("Participante");
                }
                if (insDB.ExistsValueOnMultifile("dbo.Contrato", "CodigoCliente", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Codigo), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                    vSbInfo.AppendLine("Contrato");
                }
                if (insDB.ExistsValueOnMultifile("dbo.Contrato", "CodigoClienteAFacturar", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Codigo), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                    vSbInfo.AppendLine("Contrato");
                }
                if (insDB.ExistsValueOnMultifile("dbo.ClaveSuperUtilitario", "CodigoCliente", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Codigo), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                    vSbInfo.AppendLine("Clave Super Utilitario");
                }
                if (insDB.ExistsValueOnMultifile("dbo.RevisionDeData", "CodigoCliente", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Codigo), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                    vSbInfo.AppendLine("Revision De Data");
                }
                if (insDB.ExistsValueOnMultifile("dbo.CorreccionDeFechas", "CodigoCliente", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Codigo), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                    vSbInfo.AppendLine("Correccion De Fechas");
                }
                if (insDB.ExistsValueOnMultifile("dbo.MailFax", "CodigoCliente", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Codigo), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                    vSbInfo.AppendLine("Mail Fax");
                }
                if (insDB.ExistsValueOnMultifile("dbo.GestionDeCobranza", "CodigoCliente", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Codigo), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                    vSbInfo.AppendLine("Gesti?n de Cobranza");
                }
                if (insDB.ExistsValueOnMultifile("dbo.ConversionIVA", "CodigoCliente", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Codigo), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                    vSbInfo.AppendLine("Conversion IVA");
                }
                if (insDB.ExistsValueOnMultifile("dbo.ConversionIVA", "Telefono", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Telefono), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                    vSbInfo.AppendLine("Conversion IVA");
                }
                if (insDB.ExistsValueOnMultifile("dbo.Anticipo", "CodigoCliente", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Codigo), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                    vSbInfo.AppendLine("Anticipo");
                }
                if (insDB.ExistsValueOnMultifile("dbo.NotaDeEntrega", "CodigoCliente", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Codigo), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                    vSbInfo.AppendLine("Nota De Entrega");
                }
                if (insDB.ExistsValueOnMultifile("dbo.Vehiculo", "CodigoCliente", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Codigo), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                    vSbInfo.AppendLine("Vehiculo");
                }
                if (insDB.ExistsValueOnMultifile("dbo.ControlDespacho", "CodigoCliente", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Codigo), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                    vSbInfo.AppendLine("Control de Despacho");
                }
                if (insDB.ExistsValueOnMultifile("dbo.ImpresionComprobanteDeDespacho", "CodigoCliente", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Codigo), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                    vSbInfo.AppendLine("Comprobante de despacho");
                }
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
        LibResponse ILibDataMasterComponent<IList<Entity.Cliente>, IList<Entity.Cliente>>.Delete(IList<Entity.Cliente> refRecord) {
            LibResponse vResult = new LibResponse();
            try {
                string vErrMsg = "";
                CurrentRecord = refRecord[0];
                if (Validate(eAccionSR.Eliminar, out vErrMsg)) {
                    if (ExecuteProcessBeforeDelete()) {
                        insTrn.StartTransaction();
                        vResult.Success = insTrn.ExecSpNonQuery(insTrn.ToSpName(DbSchema, "ClienteDEL"), ParametrosClave(CurrentRecord, true, true));
                        if (vResult.Success) {
                            ExecuteProcessAfterDelete();
                        }
                        insTrn.CommitTransaction();
                    }
                } else {
                    throw new GalacException(vErrMsg, eExceptionManagementType.Validation);
                }
                return vResult;
            } finally {
                if (!vResult.Success) {
                    insTrn.RollBackTransaction();
                }
            }
        }

        IList<Entity.Cliente> ILibDataMasterComponent<IList<Entity.Cliente>, IList<Entity.Cliente>>.GetData(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters, bool valUseDetail) {
            List<Entity.Cliente> vResult = new List<Entity.Cliente>();
            LibDatabase insDb = new LibDatabase(clsCkn.ConfigKeyForDbService);
            switch (valType) {
                case eProcessMessageType.SpName:
                    valProcessMessage = insDb.ToSpName(DbSchema, valProcessMessage);
                    vResult = insDb.LoadFromSp<Entity.Cliente>(valProcessMessage, valParameters, CmdTimeOut);
					if (valUseDetail && vResult != null && vResult.Count > 0) {
                        new clsDireccionDeDespachoDat().GetDetailAndAppendToMaster(ref vResult);
                    }

                    break;
                case eProcessMessageType.Query:
                    vResult = insDb.LoadData<Entity.Cliente>(valProcessMessage, CmdTimeOut, valParameters);
                    break;
                default: throw new ProgrammerMissingCodeException();
            }
            insDb.Dispose();
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Cliente.Insertar")]
        [PrincipalPermission(SecurityAction.Demand, Role = "Compañía.Insertar")]
        LibResponse ILibDataMasterComponent<IList<Entity.Cliente>, IList<Entity.Cliente>>.Insert(IList<Entity.Cliente> refRecord, bool valUseDetail) {
            LibResponse vResult = new LibResponse();
            try {
                string vErrMsg = "";
                CurrentRecord = refRecord[0];
                insTrn.StartTransaction();
                if (ExecuteProcessBeforeInsert()) {
                    if (ValidateMasterDetail(eAccionSR.Insertar, CurrentRecord, valUseDetail)) {
                        if (insTrn.ExecSpNonQuery(insTrn.ToSpName(DbSchema, "ClienteINS"), ParametrosActualizacion(CurrentRecord, eAccionSR.Insertar))) {
                            if (valUseDetail) {
                                vResult.Success = true;
                                InsertDetail(CurrentRecord);
                            } else {
                                vResult.Success = true;
                            }
                            if (vResult.Success) {
                                ExecuteProcessAfterInsert();
                            }
                        }
                    }
                }
                insTrn.CommitTransaction();
                return vResult;
            } finally {
                if (!vResult.Success) {
                    insTrn.RollBackTransaction();
                }
            }
        }

        XElement ILibDataMasterComponent<IList<Entity.Cliente>, IList<Entity.Cliente>>.QueryInfo(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters) {
            XElement vResult = null;
            LibDatabase insDb = new LibDatabase(clsCkn.ConfigKeyForDbService);
            switch (valType) {
                case eProcessMessageType.Message:
                    if (valProcessMessage.Equals("ProximoCodigo")) {
                       vResult = LibXml.ValueToXElement(insDb.NextStrConsecutive("dbo.Cliente", "Codigo", valParameters, true, 10), "Codigo");
                    }
                    break;
                case eProcessMessageType.SpName:
                    vResult = LibXml.ToXElement(insDb.LoadFromSp(valProcessMessage, valParameters, CmdTimeOut));
                    break;
                case eProcessMessageType.Query:
                    vResult = LibXml.ToXElement(insDb.LoadData(valProcessMessage, CmdTimeOut, valParameters));
                    break;
                default: throw new ProgrammerMissingCodeException();
            }
            insDb.Dispose();
            return vResult;
        }

        LibResponse ILibDataMasterComponent<IList<Entity.Cliente>, IList<Entity.Cliente>>.SpecializedUpdate(IList<Entity.Cliente> refRecord,  bool valUseDetail, string valSpecializedAction) {
            throw new ProgrammerMissingCodeException();
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Cliente.Modificar")]
        LibResponse ILibDataMasterComponent<IList<Entity.Cliente>, IList<Entity.Cliente>>.Update(IList<Entity.Cliente> refRecord, bool valUseDetail, eAccionSR valAction) {
            LibResponse vResult = new LibResponse();
            string vErrMsg ="";
			try{
	            CurrentRecord = refRecord[0];
				if (ValidateMasterDetail(valAction, CurrentRecord, valUseDetail)) {
                	insTrn.StartTransaction();
		            if (ExecuteProcessBeforeUpdate()) {
		                if (Validate(eAccionSR.Modificar, out vErrMsg)) {
		                    LibDatabase insDb = new LibDatabase(clsCkn.ConfigKeyForDbService);
		                    vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "ClienteUPD"), ParametrosActualizacion(CurrentRecord, eAccionSR.Modificar));
		                    insDb.Dispose();
		                } else {
		                    throw new GalacValidationException(vErrMsg);
		                }
				
						 if (valUseDetail) {
		                    vResult = UpdateMasterAndDetail(CurrentRecord, valAction);
		                } else {
		                    vResult = UpdateMaster(CurrentRecord, valAction); //por si requiriese especialización por acción
		                }
				
						 if (vResult.Success) {
		                    ExecuteProcessAfterUpdate();
		                }
		            }
	            insTrn.CommitTransaction();
			}
	            return vResult;
			} finally{
			    if (!vResult.Success) {
                    insTrn.RollBackTransaction();
                }
			}
        }

        bool ILibDataMasterComponent<IList<Entity.Cliente>, IList<Entity.Cliente>>.ValidateAll(IList<Entity.Cliente> refRecords, bool valUseDetail, eAccionSR valAction, StringBuilder refErrorMessage) {
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

        //LibResponse ILibDataComponent<IList<Entity.Cliente>, IList<Entity.Cliente>>.SpecializedUpdate(IList<Entity.Cliente> refRecord,  bool valUseDetail, string valSpecializedAction) {
        //    throw new NotImplementedException();
        //}
        #endregion ////ILibDataMasterComponent<IList<Cliente>, IList<Cliente>>
		
		LibResponse UpdateMaster(Entity.Cliente refRecord, eAccionSR valAction) {
            LibResponse vResult = new LibResponse();
            vResult.Success = insTrn.ExecSpNonQuery(insTrn.ToSpName(DbSchema, "ClienteUPD"), ParametrosActualizacion(refRecord, valAction));
            return vResult;
        }

        LibResponse UpdateMasterAndDetail(Entity.Cliente refRecord, eAccionSR valAction) {
            LibResponse vResult = new LibResponse();
            string vErrorMessage = "";
            if (ValidateDetail(refRecord, eAccionSR.Modificar,out vErrorMessage)) {
                if (UpdateDetail(refRecord)) {
                    vResult = UpdateMaster(refRecord, valAction);
                }
            }
            return vResult;
        }

        private bool InsertDetail(Entity.Cliente valRecord) {
            bool vResult = true;
            vResult = vResult && SetPkInDetailDireccionDeDespachoAndUpdateDb(valRecord);
            return vResult;
        }

        private bool SetPkInDetailDireccionDeDespachoAndUpdateDb(Entity.Cliente valRecord) {
            bool vResult = false;
            int vConsecutivo = 1;
            clsDireccionDeDespachoDat insDireccionDeDespacho = new clsDireccionDeDespachoDat();
            foreach (DireccionDeDespacho vDetail in valRecord.DetailDireccionDeDespacho) {
                vDetail.ConsecutivoCompania = valRecord.ConsecutivoCompania;
                vDetail.CodigoCliente = valRecord.Codigo;
                vDetail.ConsecutivoDireccion = vConsecutivo;
                vConsecutivo++;
            }
            vResult = insDireccionDeDespacho.InsertChild(valRecord, insTrn);
            return vResult;
        }

        private bool UpdateDetail(Entity.Cliente valRecord) {
            bool vResult = true;
            vResult = vResult && SetPkInDetailDireccionDeDespachoAndUpdateDb(valRecord);
            return vResult;
        }
        #region Validaciones
        protected override bool Validate(eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            ClearValidationInfo();
            vResult = IsValidConsecutivoCompania(valAction, CurrentRecord.ConsecutivoCompania, CurrentRecord.Codigo);            
            vResult = IsValidCodigo(valAction, CurrentRecord.ConsecutivoCompania, CurrentRecord.Codigo) && vResult;
            vResult = IsValidNombre(valAction, CurrentRecord.ConsecutivoCompania, CurrentRecord.Nombre) && vResult;
            vResult = IsValidNumeroRIF(valAction, CurrentRecord.ConsecutivoCompania, CurrentRecord.NumeroRIF) && vResult;
            vResult = IsValidNumeroNIT(valAction, CurrentRecord.ConsecutivoCompania, CurrentRecord.NumeroNIT) && vResult;
            vResult = IsValidCiudad(valAction, CurrentRecord.Ciudad) && vResult;
            vResult = IsValidZonaDeCobranza(valAction, CurrentRecord.ZonaDeCobranza) && vResult;
            vResult = IsValidCodigoVendedor(valAction, CurrentRecord.CodigoVendedor) && vResult;
			vResult = IsValidConsecutivoVendedor(valAction, CurrentRecord.ConsecutivoVendedor) && vResult;
            //vResult = IsValidNombreVendedor(valAction, CurrentRecord.NombreVendedor) && vResult;
            //vResult = IsValidCuentaContableCxC(valAction, CurrentRecord.CuentaContableCxC) && vResult;
            //vResult = IsValidCuentaContableIngresos(valAction, CurrentRecord.CuentaContableIngresos) && vResult;
            //vResult = IsValidCuentaContableAnticipo(valAction, CurrentRecord.CuentaContableAnticipo) && vResult;
            vResult = IsValidSectorDeNegocio(valAction, CurrentRecord.SectorDeNegocio) && vResult;
            vResult = IsValidClienteDesdeFecha(valAction, CurrentRecord.ClienteDesdeFecha) && vResult;
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

        private bool IsValidConsecutivo(eAccionSR valAction, int valConsecutivoCompania, int valConsecutivo){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valConsecutivo == 0) {
                BuildValidationInfo(MsgRequiredField("Consecutivo"));
                vResult = false;
            } else if (valAction == eAccionSR.Insertar) {
                Entity.Cliente vRecBusqueda = new Entity.Cliente();
                vRecBusqueda.Consecutivo = valConsecutivo;
                if (KeyExists(valConsecutivoCompania, vRecBusqueda)) {
                    BuildValidationInfo(MsgFieldValueAlreadyExist("Consecutivo", valConsecutivo));
                    vResult = false;
                }
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
                if (KeyExists(valConsecutivoCompania, valCodigo)) {
                    BuildValidationInfo(MsgFieldValueAlreadyExist("Código", valCodigo));
                    vResult = false;
                }
            }
            return vResult;
        } 
		
		private bool IsValidNombre(eAccionSR valAction, int valConsecutivoCompania, string valNombre){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valNombre = LibString.Trim(valNombre);
            if (LibString.IsNullOrEmpty(valNombre, true)) {
                BuildValidationInfo(MsgRequiredField("Nombre"));
                vResult = false;
            } else if (valAction == eAccionSR.Insertar) {
                Entity.Cliente vRecBusqueda = new Entity.Cliente();
                vRecBusqueda.Nombre = valNombre;
                if (KeyExists(valConsecutivoCompania, vRecBusqueda)) {
                    BuildValidationInfo(MsgFieldValueAlreadyExist("Nombre", valNombre));
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
                BuildValidationInfo(MsgRequiredField("Nro R.I.F"));
                vResult = false;
            } else if (valAction == eAccionSR.Insertar) {
                Entity.Cliente vRecBusqueda = new Entity.Cliente();
                vRecBusqueda.NumeroRIF = valNumeroRIF;
                LibGpParams vParams = new LibGpParams();
                vParams.AddInInteger("ConsecutivoCompania", vRecBusqueda.ConsecutivoCompania);
                vParams.AddInString("NumeroRIF", vRecBusqueda.NumeroRIF, 20);
                if (KeyExists(valConsecutivoCompania, vRecBusqueda.NumeroRIF)) {
                    BuildValidationInfo(MsgFieldValueAlreadyExist("Cédula", valNumeroRIF));
                    vResult = false;
                }
            }
            return vResult;
        }

		 private bool IsValidNumeroNIT(eAccionSR valAction, int valConsecutivoCompania, string valNumeroNIT){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valNumeroNIT = LibString.Trim(valNumeroNIT);
            if (LibString.IsNullOrEmpty(valNumeroNIT, true)) {
                BuildValidationInfo(MsgRequiredField("N? N.I.T."));
                vResult = false;
            } else if (valAction == eAccionSR.Insertar) {
                Entity.Cliente vRecBusqueda = new Entity.Cliente();
                vRecBusqueda.NumeroNIT = valNumeroNIT;
                if (KeyExists(valConsecutivoCompania, vRecBusqueda)) {
                    BuildValidationInfo(MsgFieldValueAlreadyExist("N? N.I.T.", valNumeroNIT));
                    vResult = false;
                }
            }
            return vResult;
        }
		
		private bool IsValidCiudad(eAccionSR valAction, string valCiudad){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCiudad = LibString.Trim(valCiudad);
            if (LibString.IsNullOrEmpty(valCiudad , true)) {
                BuildValidationInfo(MsgRequiredField("Ciudad"));
                vResult = false;
            } else {
                LibDatabase insDb = new LibDatabase();
                if (!insDb.ExistsValue("Comun.Ciudad", "NombreCiudad", insDb.InsSql.ToSqlValue(valCiudad), true)) {
                    BuildValidationInfo("El valor asignado al campo Ciudad no existe, escoga nuevamente.");
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidZonaDeCobranza(eAccionSR valAction, string valZonaDeCobranza){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valZonaDeCobranza = LibString.Trim(valZonaDeCobranza);
            if (LibString.IsNullOrEmpty(valZonaDeCobranza , true)) {
                BuildValidationInfo(MsgRequiredField("Zona De Cobranza"));
                vResult = false;
            } else {
                LibDatabase insDb = new LibDatabase();
                if (!insDb.ExistsValue("Saw.ZonaCobranza", "Nombre", insDb.InsSql.ToSqlValue(valZonaDeCobranza), true)) {
                    BuildValidationInfo("El valor asignado al campo Zona de Cobranza no existe, escoga nuevamente.");
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidSectorDeNegocio(eAccionSR valAction, string valSectorDeNegocio){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valSectorDeNegocio = LibString.Trim(valSectorDeNegocio);
            if (LibString.IsNullOrEmpty(valSectorDeNegocio , true)) {
                BuildValidationInfo(MsgRequiredField("Sector De Negocio"));
                vResult = false;
            } else {
                LibDatabase insDb = new LibDatabase();
                if (!insDb.ExistsValue("Comun.SectorDeNegocio", "Descripcion", insDb.InsSql.ToSqlValue(valSectorDeNegocio), true)) {
                    BuildValidationInfo("El valor asignado al campo Sector De Negocio no existe, escoga nuevamente.");
                    vResult = false;
                }
            }
            return vResult;
        }

        //private bool IsValidCiudad(eAccionSR valAction, string valCiudad){
        //    bool vResult = true;
        //    if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
        //        return true;
        //    }
        //    valCiudad = LibString.Trim(valCiudad);
        //    if (LibString.IsNullOrEmpty(valCiudad , true)) {
        //        BuildValidationInfo(MsgRequiredField("Ciudad"));
        //        vResult = false;
        //    } else {
        //        LibDatabase insDb = new LibDatabase();
        //        if (!insDb.ExistsValue("Comun.Ciudad", "NombreCiudad", insDb.InsSql.ToSqlValue(valCiudad), true)) {
        //            BuildValidationInfo("El valor asignado al campo Ciudad no existe, escoga nuevamente.");
        //            vResult = false;
        //        }
        //    }
        //    return vResult;
        //}

        private bool IsValidCodigoVendedor(eAccionSR valAction, string valCodigoVendedor){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCodigoVendedor = LibString.Trim(valCodigoVendedor);
            if (LibString.IsNullOrEmpty(valCodigoVendedor , true)) {
                BuildValidationInfo(MsgRequiredField("C?digo del Vendedor"));
                vResult = false;
            } else {
                LibDatabase insDb = new LibDatabase();
                if (!insDb.ExistsValue("Adm.Vendedor", "Codigo", insDb.InsSql.ToSqlValue(valCodigoVendedor), true)) {
                    BuildValidationInfo("El valor asignado al campo C?digo del Vendedor no existe, escoga nuevamente.");
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidConsecutivoVendedor(eAccionSR valAction, int valConsecutivoVendedor){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valConsecutivoVendedor == 0) {
                BuildValidationInfo(MsgRequiredField("Consecutivo Vendedor"));
                vResult = false;
            }
            return vResult;
        }

        private bool IsValidCuentaContableCxC(eAccionSR valAction, string valCuentaContableCxC){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCuentaContableCxC = LibString.Trim(valCuentaContableCxC);
            if (LibString.IsNullOrEmpty(valCuentaContableCxC , true)) {
                BuildValidationInfo(MsgRequiredField("CxC Cliente"));
                vResult = false;
            } else {
                LibDatabase insDb = new LibDatabase();
                if (!insDb.ExistsValue("dbo.Cuenta", "Codigo", insDb.InsSql.ToSqlValue(valCuentaContableCxC), true)) {
                    BuildValidationInfo("El valor asignado al campo CxC Cliente no existe, escoga nuevamente.");
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidCuentaContableIngresos(eAccionSR valAction, string valCuentaContableIngresos){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCuentaContableIngresos = LibString.Trim(valCuentaContableIngresos);
            if (LibString.IsNullOrEmpty(valCuentaContableIngresos , true)) {
                BuildValidationInfo(MsgRequiredField("Ingresos"));
                vResult = false;
            } else {
                LibDatabase insDb = new LibDatabase();
                if (!insDb.ExistsValue("dbo.Cuenta", "Codigo", insDb.InsSql.ToSqlValue(valCuentaContableIngresos), true)) {
                    BuildValidationInfo("El valor asignado al campo Ingresos no existe, escoga nuevamente.");
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidCuentaContableAnticipo(eAccionSR valAction, string valCuentaContableAnticipo){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCuentaContableAnticipo = LibString.Trim(valCuentaContableAnticipo);
            if (LibString.IsNullOrEmpty(valCuentaContableAnticipo , true)) {
                BuildValidationInfo(MsgRequiredField("Anticipos"));
                vResult = false;
            } else {
                LibDatabase insDb = new LibDatabase();
                if (!insDb.ExistsValue("dbo.Cuenta", "Codigo", insDb.InsSql.ToSqlValue(valCuentaContableAnticipo), true)) {
                    BuildValidationInfo("El valor asignado al campo Anticipos no existe, escoga nuevamente.");
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidClienteDesdeFecha(eAccionSR valAction, DateTime valClienteDesdeFecha){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (LibDefGen.DateIsGreaterThanDateLimitForEnterData(valClienteDesdeFecha, false, valAction)) {
                BuildValidationInfo(LibDefGen.MessageDateRestrictionDemoProgram());
                vResult = false;
            }
            return vResult;
        }

        private bool KeyExists(int valConsecutivoCompania, string valCodigo) {
            bool vResult = false;
            Entity.Cliente vRecordBusqueda = new Entity.Cliente();
            vRecordBusqueda.ConsecutivoCompania = valConsecutivoCompania;
            vRecordBusqueda.Codigo = valCodigo;
            LibDatabase insDb = new LibDatabase(clsCkn.ConfigKeyForDbService);
            vResult = insDb.ExistsRecord("dbo.Cliente", "ConsecutivoCompania", ParametrosClave(vRecordBusqueda, false, false));
            insDb.Dispose();
            return vResult;
        }

        private bool KeyExists(int valConsecutivoCompania, Entity.Cliente valRecordBusqueda) {
            bool vResult = false;
            valRecordBusqueda.ConsecutivoCompania = valConsecutivoCompania;
            LibDatabase insDb = new LibDatabase(clsCkn.ConfigKeyForDbService);
            vResult = insDb.ExistsRecord("dbo.Cliente", "ConsecutivoCompania", ParametrosClave(valRecordBusqueda, false, false));
            insDb.Dispose();
            return vResult;
        }

        private bool ValidateMasterDetail(eAccionSR valAction, Entity.Cliente valRecordMaster, bool valUseDetail) {
            bool vResult = false;
            string vErrMsg;
            if (Validate(valAction, out vErrMsg)) {
                if (valUseDetail) {
                    if (ValidateDetail(valRecordMaster, eAccionSR.Insertar, out vErrMsg)) {
                        vResult = true;
                    } else {
                        throw new GalacValidationException("Cliente (detalle)\n" + vErrMsg);
                    }
                } else {
                    vResult = true;
                }
            } else {
                throw new GalacValidationException(vErrMsg);
            }
            return vResult;
        }

        private bool ValidateDetail(Entity.Cliente valRecord, eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            outErrorMessage = "";
            vResult = vResult && ValidateDetailDireccionDeDespacho(valRecord, valAction, out outErrorMessage);
            return vResult;
        }

        private bool ValidateDetailDireccionDeDespacho(Entity.Cliente valRecord, eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            StringBuilder vSbErrorInfo = new StringBuilder();
            int vNumeroDeLinea = 1;
            outErrorMessage = string.Empty;
            foreach (DireccionDeDespacho vDetail in valRecord.DetailDireccionDeDespacho) {
                bool vLineHasError = true;
                //agregar validaciones
                if (LibString.IsNullOrEmpty(vDetail.Ciudad)) {
                    vSbErrorInfo.AppendLine("Línea " + vNumeroDeLinea.ToString() + ": No fue asignado el Ciudad.");
                } else {
                    vLineHasError = false;
                }
                vResult = vResult && (!vLineHasError);
                vNumeroDeLinea++;
            }
            if (!vResult) {
                outErrorMessage = "Direccion De Despacho"  + Environment.NewLine + vSbErrorInfo.ToString();
            }
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

        #region //Miembros de ILibDataRpt
        DataTable ILibDataRpt.GetDt(string valSqlStringCommand, int valCmdTimeout) {
            return new LibDataReport().GetDataTableForReport(valSqlStringCommand, valCmdTimeout);
        }

        DataTable ILibDataRpt.GetDt(string valSpName, StringBuilder valXmlParamsExpression, int valCmdTimeout) {
            return new LibDataReport().GetDataTableForReport(valSpName, valXmlParamsExpression, valCmdTimeout);
        }
        #endregion ////Miembros de ILibDataRpt
        #endregion //Metodos Generados

        protected override bool ExecuteProcessBeforeInsert() {
            StringBuilder vParametro = ParametrosProximoConsecutivo(CurrentRecord);
            LibDataScope insDb = new LibDataScope();
            CurrentRecord.Consecutivo = insDb.NextLngConsecutive("dbo.Cliente", "Consecutivo", vParametro);
            return base.ExecuteProcessBeforeInsert();
        }
    } //End of class clsClienteDat

} //End of namespace Galac.Saw.Dal.Clientes

