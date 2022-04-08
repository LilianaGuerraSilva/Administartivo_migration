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
using Galac.Adm.Ccl.Venta;
using Galac.Saw.Ccl.SttDef;

namespace Galac.Adm.Dal.Venta {
    public class clsCXCDat: LibData, ILibDataMasterComponentWithSearch<IList<CxC>, IList<CxC>>, ILibDataRpt {
        #region Variables
        LibTrn insTrn;
        CxC _CurrentRecord;
        #endregion //Variables

        #region Propiedades
        private CxC CurrentRecord {
            get { return _CurrentRecord; }
            set { _CurrentRecord = value; }
        }
        #endregion //Propiedades

        #region Constructores
		public clsCXCDat() {
            DbSchema = "Adm";
            insTrn = new LibTrn();
        }
        #endregion //Constructores
	
        #region Metodos Generados
        public string CXCSqlInsertar() {
            StringBuilder vSql = new StringBuilder();

            vSql.AppendLine(" INSERT INTO cxc");
            vSql.AppendLine(" (ConsecutivoCompania");
            vSql.AppendLine(" ,Numero, Status, TipoCxc, CodigoCliente, CodigoVendedor, Origen ");
            vSql.AppendLine(" ,Fecha ,FechaCancelacion, FechaVencimiento ");
            vSql.AppendLine(" ,MontoExento, MontoGravado, MontoIva, MontoAbonado ");
            vSql.AppendLine(" ,Descripcion, Moneda, CambioAbolivares, CodigoCc, CentroDeCostos, SeRetuvoIva ");
            vSql.AppendLine(" ,NumeroDocumentoOrigen, RefinanciadoSn, NoAplicaParaLibroDeVentas, CodigoLote, CodigoMoneda ");
            vSql.AppendLine(" ,NombreOperador, FechaUltimaModificacion, NumeroControl ,NumeroComprobanteFiscal, FechaLimiteCambioAMonedaLocal) ");
            vSql.AppendLine(" VALUES( " + "@ConsecutivoCompania");
            vSql.AppendLine(" , " + "@NumeroCXC");
            vSql.AppendLine(" , " + "@StatusCXC");
            vSql.AppendLine(" , " + "@TipoCxC");
            vSql.AppendLine(" , " + "@CodigoCliente");
            vSql.AppendLine(" , " + "@CodigoVendedor");
            vSql.AppendLine(" , " + "@OrigenDocumento");
            vSql.AppendLine(" , " + "@Fecha");
            vSql.AppendLine(" , " + "@FechaCancelacion");
            vSql.AppendLine(" , " + "@FechaVencimiento");            
            vSql.AppendLine(" , " + "@MontoExento");
            vSql.AppendLine(" , " + "@BaseImponible");
            vSql.AppendLine(" , " + "@MontoIva");
            vSql.AppendLine(" , " + "@MontoAbonado");
            vSql.AppendLine(" , " + "@Descripcion");
            vSql.AppendLine(" , " + "@Moneda");
            vSql.AppendLine(" , " + "@CambioABolivar");
            vSql.AppendLine(" , " + "@CodigoCC");
            vSql.AppendLine(" , " + "@CentroDeCostos");
            vSql.AppendLine(" , " + "@SeRetuvoIva");
            vSql.AppendLine(" , " + "@NumeroDocumentoOrigen");
            vSql.AppendLine(" , " + "@Refinanciado");
            vSql.AppendLine(" , " + "@AplicaParalibroDeVentas");
            vSql.AppendLine(" , " + "@CodigoLote");
            vSql.AppendLine(" , " + "@CodigoMoneda");
            vSql.AppendLine(" , " + "@NombreOperador");
            vSql.AppendLine(" , " + "@FechaUltimaModificacion");
            vSql.AppendLine(" , " + "@NumeroControl");
            vSql.AppendLine(" , " + "@ComprobanteFiscal");
            vSql.AppendLine(" , " + "@FechaLimiteCambioAMonedaLocal)");
            return vSql.ToString();
        }

        public StringBuilder CXCParametrosInsertar(int valConsecutivoCompania, XElement valData) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            decimal vIGTF = LibImportData.ToDec(LibXml.GetPropertyString(valData, "IGTFML"), 2);
            decimal vTotalBaseImponible = LibImportData.ToDec(LibXml.GetPropertyString(valData, "TotalBaseImponible"), 2);
            decimal vTotalMontoIva = LibImportData.ToDec(LibXml.GetPropertyString(valData, "TotalIVA"), 2);
            decimal vMontoAbonado = LibImportData.ToDec(LibXml.GetPropertyString(valData, "TotalFactura"), 2);
            decimal vTotalMontoExento = LibImportData.ToDec(LibXml.GetPropertyString(valData, "TotalMontoExento"), 2);
            eTipoDocumentoFactura vTipoDeDocumento = (eTipoDocumentoFactura)LibConvert.DbValueToEnum(LibXml.GetPropertyString(valData, "TipoDeDocumento"));
            eTipoDeTransaccion vTipoCxC = eTipoDeTransaccion.TICKETMAQUINAREGISTRADORA;
            string vCodigoCliente = LibXml.GetPropertyString(valData, "CodigoCliente");
            string vCodigoVendedor = LibXml.GetPropertyString(valData, "CodigoVendedor");
            string vComprobanteFiscal = LibXml.GetPropertyString(valData, "NumeroComprobanteFiscal");
            string vNumeroFactura = LibXml.GetPropertyString(valData, "Numero");
            string vNumeroCXC = vNumeroFactura;
            eStatusCXC vStatusCXC = eStatusCXC.CANCELADO;
            DateTime vFechaFactura = LibConvert.ToDate(LibXml.GetPropertyString(valData, "Fecha"));
            eTipoDeTransaccion vOrigenDocumento = eTipoDeTransaccion.TICKETMAQUINAREGISTRADORA;
            bool vSeRetuvoIva = false;
            bool vRefinanciado = false;
            string vCodigoCC = "0";
            string vDescripcion = "";
            bool vAplicaParalibroDeVentas = false;
            string vNombreOperador = LibXml.GetPropertyString(valData, "NombreOperador");
            string vCodigoMoneda = LibXml.GetPropertyString(valData, "CodigoMoneda");
            string vMoneda = LibXml.GetPropertyString(valData, "Moneda");
            decimal vCambioABolivar = AsignarCambioABolivares(valData);
            string vNumeroControl = "0";
            string vCodigoLote = "0";
            string vCentroDeCostos = "";
            DateTime vFechaLimiteCambioAMonedaLocal = LibConvert.ToDate(LibXml.GetPropertyString(valData, "Fecha"));
            vParams.AddReturn();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInString("NumeroCXC", vNumeroCXC, 20);
            vParams.AddInEnum("StatusCXC", (int)vStatusCXC);
            vParams.AddInEnum("TipoCxC", (int)vTipoCxC);
            vParams.AddInString("CodigoCliente", vCodigoCliente, 10);
            vParams.AddInString("CodigoVendedor", vCodigoVendedor, 5);
            vParams.AddInEnum("OrigenDocumento", (int)vOrigenDocumento);
            vParams.AddInDateTime("Fecha", vFechaFactura);
            vParams.AddInDateTime("FechaCancelacion", vFechaFactura);
            vParams.AddInDateTime("FechaVencimiento", vFechaFactura);
            vParams.AddInDecimal("MontoExento", vTotalMontoExento + vIGTF, 2);
            vParams.AddInDecimal("BaseImponible", vTotalBaseImponible, 2);
            vParams.AddInDecimal("MontoIva", vTotalMontoIva, 2);
            vParams.AddInDecimal("MontoAbonado", vMontoAbonado + vIGTF, 2);
            vParams.AddInString("Descripcion", vDescripcion, 150);
            vParams.AddInString("Moneda", vMoneda, 80);
            vParams.AddInDecimal("CambioABolivar", vCambioABolivar, 4);
            vParams.AddInString("CodigoCC", vCodigoCC, 5);
            vParams.AddInString("CentroDeCostos", vCentroDeCostos, 40);
            vParams.AddInBoolean("SeRetuvoIva", vSeRetuvoIva);
            vParams.AddInString("NumeroDocumentoOrigen", vNumeroFactura, 20);
            vParams.AddInBoolean("Refinanciado", vRefinanciado);
            vParams.AddInBoolean("AplicaParalibroDeVentas", vAplicaParalibroDeVentas);
            vParams.AddInString("CodigoLote", vCodigoLote, 10);
            vParams.AddInString("CodigoMoneda", vCodigoMoneda, 4);
            vParams.AddInString("NombreOperador", ((CustomIdentity)Thread.CurrentPrincipal.Identity).Login, 10);
            vParams.AddInDateTime("FechaUltimaModificacion", vFechaFactura);
            vParams.AddInString("NumeroControl", vNumeroControl, 11);
            vParams.AddInString("ComprobanteFiscal", vComprobanteFiscal, 12);
            vParams.AddInDateTime("FechaLimiteCambioAMonedaLocal", vFechaLimiteCambioAMonedaLocal);
            vResult = vParams.Get();
            return vResult;
        }

        private decimal AsignarCambioABolivares(XElement valFactura) {
            decimal vCambioABolivares = 1;
            if(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "UsaDivisaComoMonedaPrincipalDeIngresoDeDatos")) {
                vCambioABolivares = LibImportData.ToDec(LibXml.GetPropertyString(valFactura, "CambioABolivares"), 2);
            }
            return vCambioABolivares;
        }

        private StringBuilder ParametrosActualizacion(CxC valRecord, eAccionSR valAction) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInDateFormat("DateFormat");
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInString("Numero", valRecord.Numero, 20);
            vParams.AddInEnum("Status", valRecord.StatusAsDB);
            vParams.AddInEnum("TipoCxC", valRecord.TipoCxCAsDB);
            vParams.AddInString("CodigoCliente", valRecord.CodigoCliente, 10);
            vParams.AddInString("CodigoVendedor", valRecord.CodigoVendedor, 5);
            vParams.AddInEnum("Origen", valRecord.OrigenAsDB);
            vParams.AddInDateTime("Fecha", valRecord.Fecha);
            vParams.AddInDateTime("FechaCancelacion", valRecord.FechaCancelacion);
            vParams.AddInDateTime("FechaVencimiento", valRecord.FechaVencimiento);
            vParams.AddInDateTime("FechaAnulacion", valRecord.FechaAnulacion);
            vParams.AddInDecimal("MontoExento", valRecord.MontoExento, 2);
            vParams.AddInDecimal("MontoGravado", valRecord.MontoGravado, 2);
            vParams.AddInDecimal("MontoIVA", valRecord.MontoIVA, 2);
            vParams.AddInDecimal("MontoAbonado", valRecord.MontoAbonado, 2);
            vParams.AddInString("Descripcion", valRecord.Descripcion, 255);
            vParams.AddInString("Moneda", valRecord.Moneda, 10);
            vParams.AddInDecimal("CambioABolivares", valRecord.CambioABolivares, 2);
            vParams.AddInBoolean("SeRetuvoIva", valRecord.SeRetuvoIvaAsBool);
            vParams.AddInString("NumeroDocumentoOrigen", valRecord.NumeroDocumentoOrigen, 20);
            //vParams.AddInEnum("RefinanciadoSN", valRecord.RefinanciadoSNAsDB);
            vParams.AddInBoolean("NoAplicaParaLibroDeVentas", valRecord.NoAplicaParaLibroDeVentasAsBool);
            vParams.AddInString("CodigoLote", valRecord.CodigoLote, 10);
            vParams.AddInString("CodigoTipoDeDocumentoLey", valRecord.CodigoTipoDeDocumentoLey, 10);
            vParams.AddInBoolean("AplicaDetraccion", valRecord.AplicaDetraccionAsBool);
            vParams.AddInString("NumeroDetraccion", valRecord.NumeroDetraccion, 15);
            vParams.AddInString("CodigoDetraccion", valRecord.CodigoDetraccion, 10);
            vParams.AddInString("DescripcionDeDetraccion", valRecord.DescripcionDeDetraccion, 100);
            vParams.AddInDecimal("PorcentajeDetraccion", valRecord.PorcentajeDetraccion, 2);
            vParams.AddInDecimal("TotalDetraccion", valRecord.TotalDetraccion, 2);
            //vParams.AddInEnum("StatusDetraccion", valRecord.StatusDetraccionAsDB);
            vParams.AddInInteger("ConsecutivoMovimiento", valRecord.ConsecutivoMovimiento);
            vParams.AddInDecimal("TotalOtrosImpuestos", valRecord.TotalOtrosImpuestos, 2);
            vParams.AddInString("CodigoMoneda", valRecord.CodigoMoneda, 4);
            vParams.AddInString("NumeroControl", valRecord.NumeroControl, 11);
            vParams.AddInString("NumeroComprobanteFiscal", valRecord.NumeroComprobanteFiscal, 12);
            vParams.AddInString("NombreOperador", ((CustomIdentity) Thread.CurrentPrincipal.Identity).Login, 10);
            vParams.AddInDateTime("FechaUltimaModificacion", LibDate.Today());
            if (valAction == eAccionSR.Modificar) {
                vParams.AddInTimestamp("TimeStampAsInt", valRecord.fldTimeStamp);
            }
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosClave(CxC valRecord, bool valIncludeTimestamp, bool valAddReturnParameter) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            if (valAddReturnParameter) {
                vParams.AddReturn();
            }
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInString("Numero", valRecord.Numero, 20);
            vParams.AddInEnum("TipoCxC", valRecord.TipoCxCAsDB);
            if (valIncludeTimestamp) {
                vParams.AddInTimestamp("TimeStampAsInt", valRecord.fldTimeStamp);
            }
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosProximoConsecutivo(CxC valRecord) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInString("Numero", valRecord.Numero, 20);
            vResult = vParams.Get();
            return vResult;
        }

        #region Miembros de ILibDataMasterComponent<IList<CxC>, IList<CxC>>
        LibResponse ILibDataMasterComponent<IList<CxC>, IList<CxC>>.CanBeChoosen(IList<CxC> refRecord, eAccionSR valAction) {
            LibResponse vResult = new LibResponse();
            CxC vRecord = refRecord[0];
            StringBuilder vSbInfo = new StringBuilder();
            string vErrMsg = "";
            LibDatabase insDB = new LibDatabase();
            if (valAction == eAccionSR.Eliminar) {
                if (insDB.ExistsValueOnMultifile("Dbo.Cobranza", "TipoDeDocumento", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.TipoCxCAsDB), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                    vSbInfo.AppendLine("Cobranza");
                }
                if (insDB.ExistsValueOnMultifile("dbo.DocumentoCobrado", "TipoDeDocumentoCobrado", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.TipoCxCAsDB), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                    vSbInfo.AppendLine("Documento Cobrado");
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

        [PrincipalPermission(SecurityAction.Demand, Role = "CxC.Eliminar")]
        LibResponse ILibDataMasterComponent<IList<CxC>, IList<CxC>>.Delete(IList<CxC> refRecord) {
            LibResponse vResult = new LibResponse();
            try {
                string vErrMsg = "";
                CurrentRecord = refRecord[0];
                if (Validate(eAccionSR.Eliminar, out vErrMsg)) {
                    if (ExecuteProcessBeforeDelete()) {
                        insTrn.StartTransaction();
                        vResult.Success = insTrn.ExecSpNonQuery(insTrn.ToSpName(DbSchema, "CxCDEL"), ParametrosClave(CurrentRecord, true, true));
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

        IList<CxC> ILibDataMasterComponent<IList<CxC>, IList<CxC>>.GetData(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters, bool valUseDetail) {
            List<CxC> vResult = new List<CxC>();
            LibDatabase insDb = new LibDatabase();
            switch (valType) {
                case eProcessMessageType.SpName:
                    valProcessMessage = insDb.ToSpName(DbSchema, valProcessMessage);
                    vResult = insDb.LoadFromSp<CxC>(valProcessMessage, valParameters, CmdTimeOut);
                    //if (valUseDetail && vResult != null && vResult.Count > 0) {
                    //    new clsOtrosImpuestosCxCDat().GetDetailAndAppendToMaster(ref vResult);
                    //}
                    break;
                default: throw new ProgrammerMissingCodeException();
            }
            insDb.Dispose();
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "CxC.Insertar")]
        LibResponse ILibDataMasterComponent<IList<CxC>, IList<CxC>>.Insert(IList<CxC> refRecord, bool valUseDetail) {
            LibResponse vResult = new LibResponse();
            try {
                CurrentRecord = refRecord[0];
                insTrn.StartTransaction();
                if (ExecuteProcessBeforeInsert()) {
                    if (ValidateMasterDetail(eAccionSR.Insertar, CurrentRecord, valUseDetail)) {
                        if (insTrn.ExecSpNonQuery(insTrn.ToSpName(DbSchema, "CxCINS"), ParametrosActualizacion(CurrentRecord, eAccionSR.Insertar))) {
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

        XElement ILibDataMasterComponent<IList<CxC>, IList<CxC>>.QueryInfo(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters) {
            XElement vResult = null;
            LibDatabase insDb = new LibDatabase();
            switch (valType) {
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

        LibResponse ILibDataMasterComponent<IList<CxC>, IList<CxC>>.SpecializedUpdate(IList<CxC> refRecord, bool valUseDetail, string valSpecializedAction) {
            throw new ProgrammerMissingCodeException();
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "CxC.Modificar")]
        LibResponse ILibDataMasterComponent<IList<CxC>, IList<CxC>>.Update(IList<CxC> refRecord, bool valUseDetail, eAccionSR valAction) {
            LibResponse vResult = new LibResponse();
            try {
                CurrentRecord = refRecord[0];
                if (ValidateMasterDetail(valAction, CurrentRecord, valUseDetail)) {
                    insTrn.StartTransaction();
                    if (ExecuteProcessBeforeUpdate()) {
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
            } finally {
                if (!vResult.Success) {
                    insTrn.RollBackTransaction();
                }
            }
        }

        bool ILibDataMasterComponent<IList<CxC>, IList<CxC>>.ValidateAll(IList<CxC> refRecords, bool valUseDetail, eAccionSR valAction, StringBuilder refErrorMessage) {
            bool vResult = true;
            string vErroMessage = "";
            foreach (CxC vItem in refRecords) {
                CurrentRecord = vItem;
                vResult = vResult && Validate(valAction, out vErroMessage);
                if (LibString.IsNullOrEmpty(vErroMessage, true)) {
                    refErrorMessage.AppendLine(vErroMessage);
                }
            }
            return vResult;
        }
        #endregion //ILibDataMasterComponent<IList<CxC>, IList<CxC>>

        LibResponse UpdateMaster(CxC refRecord, eAccionSR valAction) {
            LibResponse vResult = new LibResponse();
            vResult.Success = insTrn.ExecSpNonQuery(insTrn.ToSpName(DbSchema, "CxCUPD"), ParametrosActualizacion(refRecord, valAction));
            return vResult;
        }

        LibResponse UpdateMasterAndDetail(CxC refRecord, eAccionSR valAction) {
            LibResponse vResult = new LibResponse();
            string vErrorMessage = "";
            if (ValidateDetail(refRecord, eAccionSR.Modificar,out vErrorMessage)) {
                if (UpdateDetail(refRecord)) {
                    vResult = UpdateMaster(refRecord, valAction);
                }
            }
            return vResult;
        }

        private bool InsertDetail(CxC valRecord) {
            bool vResult = true;
            vResult = vResult && SetPkInDetailOtrosImpuestosCxCAndUpdateDb(valRecord);
            return vResult;
        }

        private bool SetPkInDetailOtrosImpuestosCxCAndUpdateDb(CxC valRecord) {
            bool vResult = true;
            //int vConsecutivo = 1;
            //clsOtrosImpuestosCxCDat insOtrosImpuestosCxC = new clsOtrosImpuestosCxCDat();
            //foreach (OtrosImpuestosCxC vDetail in valRecord.DetailOtrosImpuestosCxC) {
            //    vDetail.ConsecutivoCompania = valRecord.ConsecutivoCompania;
            //    vDetail.Numero = valRecord.Numero;
            //    vDetail.TipoCxC = valRecord.TipoCxC;
            //    vDetail.ConsecutivoRenglonOI = vConsecutivo;
            //    vConsecutivo++;
            //}
            //vResult = insOtrosImpuestosCxC.InsertChild(valRecord, insTrn);
            return vResult;
        }

        private bool UpdateDetail(CxC valRecord) {
            bool vResult = true;
            vResult = vResult && SetPkInDetailOtrosImpuestosCxCAndUpdateDb(valRecord);
            return vResult;
        }

        #region Validaciones
        protected override bool Validate(eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            ClearValidationInfo();
            vResult = IsValidConsecutivoCompania(valAction, CurrentRecord.ConsecutivoCompania, CurrentRecord.Numero, CurrentRecord.TipoCxCAsEnum);
            vResult = IsValidNumero(valAction, CurrentRecord.ConsecutivoCompania, CurrentRecord.Numero, CurrentRecord.TipoCxCAsEnum) && vResult;
            vResult = IsValidTipoCxC(valAction, CurrentRecord.ConsecutivoCompania, CurrentRecord.TipoCxCAsEnum) && vResult;
            vResult = IsValidCodigoCliente(valAction, CurrentRecord.CodigoCliente) && vResult;
            vResult = IsValidCodigoVendedor(valAction, CurrentRecord.CodigoVendedor) && vResult;
            vResult = IsValidFecha(valAction, CurrentRecord.Fecha) && vResult;
            vResult = IsValidFechaCancelacion(valAction, CurrentRecord.FechaCancelacion) && vResult;
            vResult = IsValidFechaVencimiento(valAction, CurrentRecord.FechaVencimiento) && vResult;
            vResult = IsValidFechaAnulacion(valAction, CurrentRecord.FechaAnulacion) && vResult;
            vResult = IsValidMoneda(valAction, CurrentRecord.Moneda) && vResult;
            vResult = IsValidCodigoTipoDeDocumentoLey(valAction, CurrentRecord.CodigoTipoDeDocumentoLey) && vResult;
            outErrorMessage = Information.ToString();
            return vResult;
        }

        private bool IsValidConsecutivoCompania(eAccionSR valAction, int valConsecutivoCompania, string valNumero, eTipoDeCxC valTipoCxCAsEnum){
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

        private bool IsValidNumero(eAccionSR valAction, int valConsecutivoCompania, string valNumero, eTipoDeCxC valTipoCxCAsEnumvRecBusqueda) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valNumero = LibString.Trim(valNumero);
            if (LibString.IsNullOrEmpty(valNumero, true)) {
                BuildValidationInfo(MsgRequiredField("Número"));
                vResult = false;
            } else if (valAction == eAccionSR.Insertar) {
                CxC vRecBusqueda = new CxC();
                vRecBusqueda.Numero = valNumero;
                if (KeyExists(valConsecutivoCompania, valNumero, valTipoCxCAsEnumvRecBusqueda)) {
                    BuildValidationInfo(MsgFieldValueAlreadyExist("Número", valNumero));
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidTipoCxC(eAccionSR valAction, int valConsecutivoCompania, eTipoDeCxC valTipoCxC){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            return vResult;
        }

        private bool IsValidCodigoCliente(eAccionSR valAction, string valCodigoCliente){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCodigoCliente = LibString.Trim(valCodigoCliente);
            if (LibString.IsNullOrEmpty(valCodigoCliente , true)) {
                BuildValidationInfo(MsgRequiredField("Código del Cliente"));
                vResult = false;
            } else {
                LibDatabase insDb = new LibDatabase();
                if (!insDb.ExistsValue("Saw.Cliente", "codigo", insDb.InsSql.ToSqlValue(valCodigoCliente), true)) {
                    BuildValidationInfo("El valor asignado al campo Código del Cliente no existe, escoga nuevamente.");
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidCodigoVendedor(eAccionSR valAction, string valCodigoVendedor){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCodigoVendedor = LibString.Trim(valCodigoVendedor);
            if (LibString.IsNullOrEmpty(valCodigoVendedor , true)) {
                BuildValidationInfo(MsgRequiredField("Código del Vendedor"));
                vResult = false;
            } else {
                LibDatabase insDb = new LibDatabase();
                if (!insDb.ExistsValue("dbo.Vendedor", "codigo", insDb.InsSql.ToSqlValue(valCodigoVendedor), true)) {
                    BuildValidationInfo("El valor asignado al campo Código del Vendedor no existe, escoga nuevamente.");
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidFecha(eAccionSR valAction, DateTime valFecha){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (LibDefGen.DateIsGreaterThanDateLimitForEnterData(valFecha, false, valAction)) {
                BuildValidationInfo(LibDefGen.MessageDateRestrictionDemoProgram());
                vResult = false;
            }
            return vResult;
        }

        private bool IsValidFechaCancelacion(eAccionSR valAction, DateTime valFechaCancelacion){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (LibDefGen.DateIsGreaterThanDateLimitForEnterData(valFechaCancelacion, false, valAction)) {
                BuildValidationInfo(LibDefGen.MessageDateRestrictionDemoProgram());
                vResult = false;
            }
            return vResult;
        }

        private bool IsValidFechaVencimiento(eAccionSR valAction, DateTime valFechaVencimiento){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (LibDefGen.DateIsGreaterThanDateLimitForEnterData(valFechaVencimiento, false, valAction)) {
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
            if (LibDefGen.DateIsGreaterThanDateLimitForEnterData(valFechaAnulacion, false, valAction)) {
                BuildValidationInfo(LibDefGen.MessageDateRestrictionDemoProgram());
                vResult = false;
            }
            return vResult;
        }

        private bool IsValidMoneda(eAccionSR valAction, string valMoneda){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valMoneda = LibString.Trim(valMoneda);
            if (LibString.IsNullOrEmpty(valMoneda , true)) {
                BuildValidationInfo(MsgRequiredField("Nombre de la Moneda"));
                vResult = false;
            } else {
                LibDatabase insDb = new LibDatabase();
                if (!insDb.ExistsValue("Comun.Moneda", "Nombre", insDb.InsSql.ToSqlValue(valMoneda), true)) {
                    BuildValidationInfo("El valor asignado al campo Nombre de la Moneda no existe, escoga nuevamente.");
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidCodigoTipoDeDocumentoLey(eAccionSR valAction, string valCodigoTipoDeDocumentoLey){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCodigoTipoDeDocumentoLey = LibString.Trim(valCodigoTipoDeDocumentoLey);
            if (LibString.IsNullOrEmpty(valCodigoTipoDeDocumentoLey, true)) {
                BuildValidationInfo(MsgRequiredField("Tipo de Documento"));
                vResult = false;
            }
            return vResult;
        }

        private bool KeyExists(int valConsecutivoCompania, string valNumero, eTipoDeCxC valTipoCxC) {
            bool vResult = false;
            CxC vRecordBusqueda = new CxC();
            vRecordBusqueda.ConsecutivoCompania = valConsecutivoCompania;
            vRecordBusqueda.Numero = valNumero;
            vRecordBusqueda.TipoCxCAsEnum = valTipoCxC;
            LibDatabase insDb = new LibDatabase();
            vResult = insDb.ExistsRecord(DbSchema + ".CxC", "ConsecutivoCompania", ParametrosClave(vRecordBusqueda, false, false));
            insDb.Dispose();
            return vResult;
        }

        private bool KeyExists(int valConsecutivoCompania, CxC valRecordBusqueda) {
            bool vResult = false;
            valRecordBusqueda.ConsecutivoCompania = valConsecutivoCompania;
            LibDatabase insDb = new LibDatabase();
            //Programador ajuste el codigo necesario para busqueda de claves unicas;
            vResult = insDb.ExistsRecord(DbSchema + ".CxC", "ConsecutivoCompania", ParametrosClave(valRecordBusqueda, false, false));
            insDb.Dispose();
            return vResult;
        }

        private bool ValidateMasterDetail(eAccionSR valAction, CxC valRecordMaster, bool valUseDetail) {
            bool vResult = false;
            string vErrMsg;
            if (Validate(valAction, out vErrMsg)) {
                if (valUseDetail) {
                    if (ValidateDetail(valRecordMaster, eAccionSR.Insertar, out vErrMsg)) {
                        vResult = true;
                    } else {
                        throw new GalacValidationException("Cx C (detalle)\n" + vErrMsg);
                    }
                } else {
                    vResult = true;
                }
            } else {
                throw new GalacValidationException(vErrMsg);
            }
            return vResult;
        }

        private bool ValidateDetail(CxC valRecord, eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            outErrorMessage = "";
            vResult = vResult && ValidateDetailOtrosImpuestosCxC(valRecord, valAction, out outErrorMessage);
            return vResult;
        }

        private bool ValidateDetailOtrosImpuestosCxC(CxC valRecord, eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            StringBuilder vSbErrorInfo = new StringBuilder();
            int vNumeroDeLinea = 1;
            outErrorMessage = string.Empty;
            //foreach (OtrosImpuestosCxC vDetail in valRecord.DetailOtrosImpuestosCxC) {
            //    bool vLineHasError = true;
            //    //agregar validaciones
            //    vResult = vResult && (!vLineHasError);
            //    vNumeroDeLinea++;
            //}
            if (!vResult) {
                outErrorMessage = "Otros Impuestos Cx C"  + Environment.NewLine + vSbErrorInfo.ToString();
            }
            return vResult;
        }
        #endregion //Validaciones

        #region Miembros de ILibDataFKSearch
        bool ILibDataFKSearch.ConnectFk(ref XmlDocument refResulset, eProcessMessageType valType, string valProcessMessage, StringBuilder valXmlParamsExpression) {
            bool vResult = false;
            LibDatabase insDb = new LibDatabase();
            refResulset = insDb.LoadForConnect(valProcessMessage, valXmlParamsExpression, CmdTimeOut);
            if (refResulset != null && refResulset.DocumentElement != null && refResulset.DocumentElement.HasChildNodes) {
                vResult = true;
            }
            return vResult;
        }
        #endregion //Miembros de ILibDataFKSearch

        #region Miembros de ILibDataRpt
        System.Data.DataTable ILibDataRpt.GetDt(string valSqlStringCommand, int valCmdTimeout) {
            return new LibDataReport().GetDataTableForReport(valSqlStringCommand, valCmdTimeout);
        }

        System.Data.DataTable ILibDataRpt.GetDt(string valSpName, StringBuilder valXmlParamsExpression, int valCmdTimeout) {
            return new LibDataReport().GetDataTableForReport(valSpName, valXmlParamsExpression, valCmdTimeout);
        }
        #endregion //Miembros de ILibDataRpt

        #endregion //Metodos Generados

    }
}
