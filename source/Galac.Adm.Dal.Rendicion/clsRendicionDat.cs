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
using LibGalac.Aos.Cib;
using LibGalac.Aos.Base.Dal;
using LibGalac.Aos.Catching;
using LibGalac.Aos.Dal;
using LibGalac.Aos.DefGen;
using Galac.Adm.Ccl.CajaChica;

// #############   Cambiar
namespace Galac.Adm.Dal.CajaChica {
    public class clsRendicionDat: LibData, ILibDataMasterComponentWithSearch<IList<Rendicion>, IList<Rendicion>>, ILibDataRpt {
        #region Variables
        LibDataScope insTrn;
        Rendicion _CurrentRecord;
        #endregion //Variables
        #region Propiedades
        private Rendicion CurrentRecord {
            get { return _CurrentRecord; }
            set { _CurrentRecord = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public clsRendicionDat() {
            DbSchema = "Adm";
            insTrn = new LibDataScope();
        }
        #endregion //Constructores
        #region Metodos Generados

        private StringBuilder ParametrosActualizacion(Rendicion valRecord, eAccionSR valAction) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInDateFormat("DateFormat");
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInInteger("Consecutivo", valRecord.Consecutivo);
            vParams.AddInString("Numero", valRecord.Numero, 15);
            vParams.AddInEnum("TipoDeDocumento", valRecord.TipoDeDocumentoAsDB);
            vParams.AddInInteger("ConsecutivoBeneficiario", valRecord.ConsecutivoBeneficiario);
            vParams.AddInDateTime("FechaApertura", valRecord.FechaApertura);
            vParams.AddInDateTime("FechaCierre", valRecord.FechaCierre);
            vParams.AddInDateTime("FechaAnulacion", valRecord.FechaAnulacion);
            vParams.AddInEnum("StatusRendicion", valRecord.StatusRendicionAsDB);
            vParams.AddInDecimal("TotalAdelantos", valRecord.TotalAdelantos, 2);
            vParams.AddInDecimal("TotalGastos", valRecord.TotalGastos, 2);
            vParams.AddInDecimal("TotalIVA", valRecord.TotalIVA, 2);
            vParams.AddInString("CodigoCuentaBancaria", valRecord.CodigoCuentaBancaria, 5);
			vParams.AddInBoolean("GeneraImpuestoBancario", valRecord.GeneraImpuestoBancarioAsBool);
            vParams.AddInString("CodigoConceptoBancario", valRecord.CodigoConceptoBancario, 8);
            vParams.AddInString("NumeroDocumento", valRecord.NumeroDocumento, 15);
            vParams.AddInString("BeneficiarioCheque", valRecord.BeneficiarioCheque, 60);
            vParams.AddInString("CodigoCtaBancariaCajaChica", valRecord.CodigoCtaBancariaCajaChica, 5);
            vParams.AddInString("Descripcion", valRecord.Descripcion, 50);
            vParams.AddInString("Observaciones", valRecord.Observaciones, 200);
            vParams.AddInString("NombreOperador", ((CustomIdentity) Thread.CurrentPrincipal.Identity).Login, 10);
            vParams.AddInDateTime("FechaUltimaModificacion", LibDate.Today());
            if (valAction == eAccionSR.Modificar || valAction == eAccionSR.Cerrar) {
                vParams.AddInTimestamp("TimeStampAsInt", valRecord.fldTimeStamp);
            }
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosClave(Rendicion valRecord, bool valIncludeTimestamp, bool valAddReturnParameter) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            if (valAddReturnParameter) {
                vParams.AddReturn();
            }
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInInteger("Consecutivo", valRecord.Consecutivo);
            if (valIncludeTimestamp) {
                vParams.AddInTimestamp("TimeStampAsInt", valRecord.fldTimeStamp);
            }
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosProximoConsecutivo(Rendicion valRecord) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vResult = vParams.Get();
            return vResult;
        }
        #region Miembros de ILibDataMasterComponent<IList<Rendicion>, IList<Rendicion>>

        LibResponse ILibDataMasterComponent<IList<Rendicion>, IList<Rendicion>>.CanBeChoosen(IList<Rendicion> refRecord, eAccionSR valAction) {
            LibResponse vResult = new LibResponse();
            Rendicion vRecord = refRecord[0];
            StringBuilder vSbInfo = new StringBuilder();
            string vErrMsg = "";
            LibDatabase insDB = new LibDatabase();
            if (valAction == eAccionSR.Eliminar) {
                
             // AQUIII

                //if (insDB.ExistsValueOnMultifile("dbo.CxP", "ConsecutivoRendicion", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Consecutivo), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                //    vSbInfo.AppendLine("Cx P");
                //}
                //if (insDB.ExistsValueOnMultifile("dbo.Anticipo", "ConsecutivoRendicion", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Consecutivo), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                //    vSbInfo.AppendLine("Anticipo");
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

        [PrincipalPermission(SecurityAction.Demand, Role = "Reposicion de Caja Chica.Eliminar")]
        LibResponse ILibDataMasterComponent<IList<Rendicion>, IList<Rendicion>>.Delete(IList<Rendicion> refRecord) {
            LibResponse vResult = new LibResponse();
                string vErrMsg = "";
                CurrentRecord = refRecord[0];
                if (Validate(eAccionSR.Eliminar, out vErrMsg)) {
                    if (ExecuteProcessBeforeDelete()) {
                        //  
                        foreach (Anticipo ant in CurrentRecord.Adelantos) {
                            if (ant.ConsecutivoAnticipo == 0) continue;
                            ant.ConsecutivoRendicion = 0;
                            ((ILibDataComponent<IList<Anticipo>, IList<Anticipo>>)new clsAnticipoDat()).Update(new List<Anticipo>() { ant });
                            //    CurrentRecord.Adelantos.Remove(ant);
                        }

                        vResult.Success = insTrn.ExecSpNonQueryWithScope(insTrn.ToSpName(DbSchema, "RendicionDEL"), ParametrosClave(CurrentRecord, true, true));
                        if (vResult.Success) {
                            ExecuteProcessAfterDelete();
                        }
                    }
                } else {
                    throw new GalacException(vErrMsg, eExceptionManagementType.Validation);
                }
                return vResult;
        }

        IList<Rendicion> ILibDataMasterComponent<IList<Rendicion>, IList<Rendicion>>.GetData(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters, bool valUseDetail) {
            List<Rendicion> vResult = new List<Rendicion>();
            LibDatabase insDb = new LibDatabase();
            switch (valType) {
                case eProcessMessageType.SpName:
                    valProcessMessage = insDb.ToSpName(DbSchema, valProcessMessage);
                    vResult = insDb.LoadFromSp<Rendicion>(valProcessMessage, valParameters, CmdTimeOut);
                    if (valUseDetail && vResult != null && vResult.Count > 0) {
                        new clsDetalleDeRendicionDat().GetDetailAndAppendToMaster(ref vResult);
                        LibGpParams vParams = new LibGpParams();
                        vParams.AddInString("SQLWhere", "dbo.Gv_Anticipo_B1.ConsecutivoCompania = " + vResult[0].ConsecutivoCompania.ToString() +
                        " AND  dbo.Gv_Anticipo_B1.ConsecutivoRendicion = " + vResult[0].Consecutivo.ToString(), 200);
                        var parametros = vParams.Get();
                        clsAnticipoDat insAnticipoDat = new clsAnticipoDat();
                        XmlDocument XmlProperties = new XmlDocument();
                        ((ILibDataFKSearch)insAnticipoDat).ConnectFk(ref XmlProperties, eProcessMessageType.SpName, "dbo.Gp_AnticipoSCH", parametros);
                        vResult[0].Datos = XmlProperties;
                    }
                    break;
                default: throw new ProgrammerMissingCodeException();
            }
            insDb.Dispose();
            return vResult;
        }

       [PrincipalPermission(SecurityAction.Demand, Role = "Reposicion de Caja Chica.Insertar")]
        LibResponse ILibDataMasterComponent<IList<Rendicion>, IList<Rendicion>>.Insert(IList<Rendicion> refRecord, bool valUseDetail) {
            LibResponse vResult = new LibResponse();
                CurrentRecord = refRecord[0];
                //la generacin del cosnecutivo anteriormente estaba en ExecuteProcessBeforeInsert()
                CurrentRecord.Consecutivo = new LibDatabase().NextLngConsecutive(DbSchema + ".Rendicion", "Consecutivo", ParametrosProximoConsecutivo(CurrentRecord));
                if (ExecuteProcessBeforeInsert()) {
                    if (ValidateMasterDetail(eAccionSR.Insertar, CurrentRecord, valUseDetail)) {
                        if (insTrn.ExecSpNonQueryWithScope(insTrn.ToSpName(DbSchema, "RendicionINS"), ParametrosActualizacion(CurrentRecord, eAccionSR.Insertar))) {
                            if (valUseDetail) {
							    InsertDetail(CurrentRecord);
                                foreach (Anticipo ant in CurrentRecord.Adelantos) {
                                    if (ant.ConsecutivoAnticipo == 0 || ant.ConsecutivoRendicion == 0) continue;
                                    ((ILibDataComponent<IList<Anticipo>, IList<Anticipo>>)new clsAnticipoDat()).Update(new List<Anticipo>() {ant});
                                //    CurrentRecord.Adelantos.Remove(ant);
                                }
                                vResult.Success = true;
                            } else {
                                vResult.Success = true;
                            }
                            if (vResult.Success) {
                                ExecuteProcessAfterInsert();
                            }
                        }
                    }
                }
                return vResult;
        }

        XElement ILibDataMasterComponent<IList<Rendicion>, IList<Rendicion>>.QueryInfo(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters) {
            XElement vResult = null;
            LibDatabase insDb = new LibDatabase();
            switch (valType) {
                case eProcessMessageType.SpName:
                    vResult = LibXml.ToXElement(insDb.LoadFromSp(valProcessMessage, valParameters, CmdTimeOut));
                    break;
                case eProcessMessageType.Query:
                    vResult = LibXml.ToXElement(insDb.LoadData(valProcessMessage, CmdTimeOut, valParameters));
                    break;
                case eProcessMessageType.Message:
                    if (valProcessMessage.Equals("Consecutivo")) {
                        vResult = LibXml.ValueToXElement(insDb.NextLngConsecutive("Adm.Rendicion", valProcessMessage, valParameters).ToString(), valProcessMessage);

                    } else if (valProcessMessage.Equals("Numero")) {
                        vResult = LibXml.ValueToXElement(insDb.NextStrConsecutive("Adm.Rendicion", valProcessMessage, valParameters,true,10).ToString(), valProcessMessage);
                    }
                        break;
                default: throw new ProgrammerMissingCodeException();
            }
            insDb.Dispose();
            return vResult;
        }

        LibResponse ILibDataMasterComponent<IList<Rendicion>, IList<Rendicion>>.SpecializedUpdate(IList<Rendicion> refRecord,  bool valUseDetail, string valSpecializedAction) {
            throw new ProgrammerMissingCodeException();
        }

       [PrincipalPermission(SecurityAction.Demand, Role = "Reposicion de Caja Chica.Modificar")]
        LibResponse ILibDataMasterComponent<IList<Rendicion>, IList<Rendicion>>.Update(IList<Rendicion> refRecord, bool valUseDetail, eAccionSR valAction) {
            LibResponse vResult = new LibResponse();
                CurrentRecord = refRecord[0];
                if (ValidateMasterDetail(valAction, CurrentRecord, valUseDetail)) {
                    if (ExecuteProcessBeforeUpdate()) {
                        if (valUseDetail) {
                            vResult = UpdateMasterAndDetail(CurrentRecord, valAction);
                        } else {
                            vResult = UpdateMaster(CurrentRecord, valAction); //por si requiriese especialización por acción
                        }

                        ////##################### CAMBIARRRRRR
                        if(valAction != eAccionSR.Cerrar)
                        foreach (Anticipo ant in CurrentRecord.Adelantos) {
                            if (ant.ConsecutivoAnticipo == 0) continue;
                         vResult =  ((ILibDataComponent<IList<Anticipo>, IList<Anticipo>>)new clsAnticipoDat()).Update(new List<Anticipo>() { ant });
                        }

                        if (vResult.Success) {
                            ExecuteProcessAfterUpdate();
                        }
                    }
                }
                return vResult;
        }

        bool ILibDataMasterComponent<IList<Rendicion>, IList<Rendicion>>.ValidateAll(IList<Rendicion> refRecords, bool valUseDetail, eAccionSR valAction, StringBuilder refErrorMessage) {
            bool vResult = true;
            string vErroMessage = "";
            foreach (Rendicion vItem in refRecords) {
                CurrentRecord = vItem;
                vResult = vResult && Validate(valAction, out vErroMessage);
                if (LibString.IsNullOrEmpty(vErroMessage, true)) {
                    refErrorMessage.AppendLine(vErroMessage);
                }
            }
            return vResult;
        }
        #endregion //ILibDataMasterComponent<IList<Rendiciones>, IList<Rendiciones>>

        LibResponse UpdateMaster(Rendicion refRecord, eAccionSR valAction) {
            LibResponse vResult = new LibResponse();
            vResult.Success = insTrn.ExecSpNonQueryWithScope(insTrn.ToSpName(DbSchema, "RendicionUPD"), ParametrosActualizacion(refRecord, valAction));
            return vResult;
        }

        LibResponse UpdateMasterAndDetail(Rendicion refRecord, eAccionSR valAction) {
            LibResponse vResult = new LibResponse();
            string vErrorMessage = "";
            if (ValidateDetail(refRecord, eAccionSR.Modificar,out vErrorMessage)) {

                if (UpdateDetail(refRecord)) {
                    vResult = UpdateMaster(refRecord, valAction);
                } else { 
                if(refRecord.DetailDetalleDeRendicion.Count == 0 ){
                    vResult = UpdateMaster(refRecord, valAction);
                }
                }


            }
            return vResult;
        }

        private bool InsertDetail(Rendicion valRecord) {
            bool vResult = true;
            vResult = vResult && SetPkInDetailDetalleDeRendicionAndUpdateDb(valRecord,"N");
            return vResult;
        }

        private bool SetPkInDetailDetalleDeRendicionAndUpdateDb(Rendicion valRecord,string validaMaestro) {
            bool vResult = false;
            int vConsecutivo = 1;
            clsDetalleDeRendicionDat insDetalleDeRendicion = new clsDetalleDeRendicionDat();
            foreach (DetalleDeRendicion vDetail in valRecord.DetailDetalleDeRendicion) {
                vDetail.ConsecutivoCompania = valRecord.ConsecutivoCompania;
                vDetail.ConsecutivoRendicion = valRecord.Consecutivo;               
                vConsecutivo++;
            }
            vResult = insDetalleDeRendicion.InsertChild(valRecord, insTrn, validaMaestro);
            return vResult;
        }

        private bool UpdateDetail(Rendicion valRecord) {
            bool vResult = true;
            vResult = vResult && SetPkInDetailDetalleDeRendicionAndUpdateDb(valRecord,"S");
            return vResult;
        }
        #region Validaciones
        protected override bool Validate(eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            ClearValidationInfo();
            vResult = IsValidConsecutivoCompania(valAction, CurrentRecord.ConsecutivoCompania, CurrentRecord.Consecutivo);
            vResult = IsValidConsecutivo(valAction, CurrentRecord.ConsecutivoCompania, CurrentRecord.Consecutivo) && vResult;
            vResult = IsValidNumero(valAction, CurrentRecord.ConsecutivoCompania, CurrentRecord.Numero) && vResult;
            //vResult = IsValidConsecutivoBeneficiario(valAction, CurrentRecord.ConsecutivoBeneficiario) && vResult;
            vResult = IsValidFechaApertura(valAction, CurrentRecord.FechaApertura) && vResult;
            vResult = IsValidFechaCierre(valAction, CurrentRecord.FechaCierre) && vResult;
            vResult = IsValidFechaAnulacion(valAction, CurrentRecord.FechaAnulacion) && vResult;
            vResult = IsValidBeneficiarioCheque(valAction, CurrentRecord.BeneficiarioCheque) && vResult;
            outErrorMessage = Information.ToString();
            return vResult;
        }

        private bool IsValidConsecutivoCompania(eAccionSR valAction, int valConsecutivoCompania, int valConsecutivo){
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
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar) || (valAction == eAccionSR.Insertar)) {
                return true;
            }
            if (valConsecutivo == 0) {
                BuildValidationInfo(MsgRequiredField("Consecutivo"));
                vResult = false;
            } else if (valAction == eAccionSR.Insertar) {
                if (KeyExists(valConsecutivoCompania, valConsecutivo)) {
                    BuildValidationInfo(MsgFieldValueAlreadyExist("Consecutivo", valConsecutivo));
                    vResult = false;
                }
            }
            return vResult;
        }
        private bool IsValidNumero(eAccionSR valAction, int valConsecutivoCompania, string valNumero){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valNumero = LibString.Trim(valNumero);
            if (LibString.IsNullOrEmpty(valNumero, true)) {
                BuildValidationInfo(MsgRequiredField("Número"));
                vResult = false;
            } else if (valAction == eAccionSR.Insertar) {
                Rendicion vRecBusqueda = new Rendicion();
                vRecBusqueda.Numero = valNumero;
                if (KeyExists(valConsecutivoCompania, vRecBusqueda)) {
                    BuildValidationInfo(MsgFieldValueAlreadyExist("Número", valNumero));
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidConsecutivoBeneficiario(eAccionSR valAction, int valConsecutivoBeneficiario){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valConsecutivoBeneficiario == 0) {
                BuildValidationInfo(MsgRequiredField("Código del Beneficiario"));
                vResult = false;
            }
            return vResult;
        }

        private bool IsValidFechaApertura(eAccionSR valAction, DateTime valFechaApertura){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (LibDefGen.DateIsGreaterThanDateLimitForEnterData(valFechaApertura, false, valAction)) {
                BuildValidationInfo(LibDefGen.MessageDateRestrictionDemoProgram());
                vResult = false;
            }
            return vResult;
        }

        private bool IsValidFechaCierre(eAccionSR valAction, DateTime valFechaCierre){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valAction == eAccionSR.Cerrar && LibDefGen.DateIsGreaterThanDateLimitForEnterData(valFechaCierre, false, valAction)) {
                BuildValidationInfo(LibDefGen.MessageDateRestrictionDemoProgram());
                vResult = false;
            }
            return vResult;
        }
        private bool IsValidFechaAnulacion(eAccionSR valAction, DateTime valFechaAnulacion){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valAction == eAccionSR.Anular && LibDefGen.DateIsGreaterThanDateLimitForEnterData(valFechaAnulacion, false, valAction)) {
                BuildValidationInfo(LibDefGen.MessageDateRestrictionDemoProgram());
                vResult = false;
            }
            return vResult;
        }
		
        private bool KeyExists(int valConsecutivoCompania, int valConsecutivo) {
            bool vResult = false;
            Rendicion vRecordBusqueda = new Rendicion();
            vRecordBusqueda.ConsecutivoCompania = valConsecutivoCompania;
            vRecordBusqueda.Consecutivo = valConsecutivo;
            LibDataScope insDb = new LibDataScope();
            vResult = insDb.ExistsRecord("Adm.Rendicion", "ConsecutivoCompania", ParametrosClave(vRecordBusqueda, false, false));
            insDb.Dispose();
            return vResult;
        }

        private bool KeyExists(int valConsecutivoCompania, Rendicion valRecordBusqueda) {
            bool vResult = false;
            valRecordBusqueda.ConsecutivoCompania = valConsecutivoCompania;
            //Programador ajuste el codigo necesario para busqueda de claves unicas;
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valRecordBusqueda.ConsecutivoCompania);
            vParams.AddInString("Numero", valRecordBusqueda.Numero, 15);
            LibDatabase insDb = new LibDatabase();
            vResult = insDb.ExistsRecord(DbSchema + ".Rendicion", "ConsecutivoCompania", vParams.Get());
            insDb.Dispose();
            return vResult;
        }

        private bool ValidateMasterDetail(eAccionSR valAction, Rendicion valRecordMaster, bool valUseDetail) {
            bool vResult = false;
            string vErrMsg;
            if (Validate(valAction, out vErrMsg)) {
                if (valUseDetail) {
                    if (ValidateDetail(valRecordMaster, eAccionSR.Insertar, out vErrMsg)) {
                        vResult = true;
                    } else {
                        throw new GalacValidationException("Reposición de Caja Chica (detalle)\n" + vErrMsg);
                    }
                } else {
                    vResult = true;
                }
            } else {
                throw new GalacValidationException(vErrMsg);
            }
            return vResult;
        }

        private bool ValidateDetail(Rendicion valRecord, eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            outErrorMessage = "";
            vResult = vResult && ValidateDetailDetalleDeRendicion(valRecord, valAction, out outErrorMessage);
            return vResult;
        }

        private bool ValidateDetailDetalleDeRendicion(Rendicion valRecord, eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            StringBuilder vSbErrorInfo = new StringBuilder();
            int vNumeroDeLinea = 1;
            foreach (DetalleDeRendicion vDetail in valRecord.DetailDetalleDeRendicion) {
                bool vLineHasError = true;
                if (vDetail.NumeroDocumento.Equals(string.Empty))
                    continue;
                //agregar validaciones
                
                if (LibString.IsNullOrEmpty(vDetail.CodigoProveedor)) {
                    vSbErrorInfo.AppendLine("Línea " + vNumeroDeLinea.ToString() + ": No fue asignado el Código del Proveedor.");
                } else {
                    vLineHasError = false;
                }

                if (!IsValidMontoTotal(valAction, new decimal[] { vDetail.MontoExento, vDetail.MontoGravable, vDetail.MontoGravableAlicuotaEspecial1, vDetail.MontoGravableAlicuotaEspecial2 })) {
                    vSbErrorInfo.AppendLine("Línea " + vNumeroDeLinea.ToString() + " :No se puede Ingresar facturas con monto total igual a 0.");
                    vLineHasError = true;
                }

                vResult = vResult && (!vLineHasError);
                vNumeroDeLinea++;
            }
            outErrorMessage = vSbErrorInfo.ToString();
            return vResult;
        }
        #endregion //Validaciones
        #region Miembros de ILibDataFKSearch
        bool ILibDataFKSearch.ConnectFk(ref XmlDocument refResulset, eProcessMessageType valType, string valProcessMessage, StringBuilder valXmlParamsExpression) {
            bool vResult = false;
            LibDataScope insDb = new LibDataScope();
            refResulset = insDb.LoadForConnect(valProcessMessage, valXmlParamsExpression, CmdTimeOut);
            if (refResulset != null && refResulset.DocumentElement != null && refResulset.DocumentElement.HasChildNodes) {
                vResult = true;
            }
            return vResult;
        }
        #endregion //Miembros de ILibDataFKSearch
        #region //Miembros de ILibDataRpt

        System.Data.DataTable ILibDataRpt.GetDt(string valSqlStringCommand, int valCmdTimeout) {
            return new LibDataReport().GetDataTableForReport(valSqlStringCommand, valCmdTimeout);
        }

        System.Data.DataTable ILibDataRpt.GetDt(string valSpName, StringBuilder valXmlParamsExpression, int valCmdTimeout) {
            return new LibDataReport().GetDataTableForReport(valSpName, valXmlParamsExpression, valCmdTimeout);
        }
        #endregion ////Miembros de ILibDataRpt
        #endregion //Metodos Generados

		private bool IsValidBeneficiarioCheque(eAccionSR valAction, string valBeneficiarioCheque){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }

            if (valAction == eAccionSR.Cerrar) {
                valBeneficiarioCheque = LibString.Trim(valBeneficiarioCheque);
                if (LibString.IsNullOrEmpty(valBeneficiarioCheque, true)) {
                    BuildValidationInfo(MsgRequiredField("Beneficiario Cheque"));
                    vResult = false;
                }
            }

            return vResult;
        }

        bool IsValidMontoTotal(eAccionSR valAction, decimal[] montos) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (montos.Sum() <= 0) {
                vResult = false;
            }
            return vResult;
        }



    } //End of class clsRendicionesDat

} //End of namespace Galac.Saw.Dal.Rendicion

