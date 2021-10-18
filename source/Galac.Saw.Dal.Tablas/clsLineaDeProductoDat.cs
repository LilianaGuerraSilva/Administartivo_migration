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
using Galac.Saw.Ccl.Tablas;
using System.Data;

namespace Galac.Saw.Dal.Tablas {
    public class clsLineaDeProductoDat: LibData, ILibDataComponentWithSearch<IList<LineaDeProducto>, IList<LineaDeProducto>> , ILibDataImport {
        #region Variables
        LineaDeProducto _CurrentRecord;
        #endregion //Variables
        #region Propiedades
        private LineaDeProducto CurrentRecord {
            get { return _CurrentRecord; }
            set { _CurrentRecord = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public clsLineaDeProductoDat() {
            DbSchema = "Adm";
        }
        #endregion //Constructores
        #region Metodos Generados

        private StringBuilder ParametrosActualizacion(LineaDeProducto valRecord, eAccionSR valAction) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInInteger("Consecutivo", valRecord.Consecutivo);
            vParams.AddInString("Nombre", valRecord.Nombre, 20);
            vParams.AddInDecimal("PorcentajeComision", valRecord.PorcentajeComision, 2);
            vParams.AddInString("CentroDeCosto", valRecord.CentroDeCosto, 20);
            vParams.AddInString("NombreOperador", ((CustomIdentity) Thread.CurrentPrincipal.Identity).Login, 10);
            vParams.AddInDateTime("FechaUltimaModificacion", LibDate.Today());
            if (valAction == eAccionSR.Modificar) {
                vParams.AddInTimestamp("TimeStampAsInt", valRecord.fldTimeStamp);
            }
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosClave(LineaDeProducto valRecord, bool valIncludeTimestamp, bool valAddReturnParameter) {
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

        private StringBuilder ParametrosProximoConsecutivo(LineaDeProducto valRecord) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vResult = vParams.Get();
            return vResult;
        }
        #region Miembros de ILibDataComponent<IList<LineaDeProducto>, IList<LineaDeProducto>>

        LibResponse ILibDataComponent<IList<LineaDeProducto>, IList<LineaDeProducto>>.CanBeChoosen(IList<LineaDeProducto> refRecord, eAccionSR valAction) {
            LibResponse vResult = new LibResponse();
            LineaDeProducto vRecord = refRecord[0];
            StringBuilder vSbInfo = new StringBuilder();
            string vErrMsg = "";
            LibDatabase insDB = new LibDatabase();
            if (valAction == eAccionSR.Eliminar) {
                if (insDB.ExistsValueOnMultifile("dbo.ArticuloInventario", "LineaDeProducto", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Nombre), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                    vSbInfo.AppendLine("Artículo Inventario");
                }
                if (insDB.ExistsValueOnMultifile("dbo.ConteoFisico", "LineaDeProducto", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Nombre), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                    vSbInfo.AppendLine("Conteo Fisico");
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

        [PrincipalPermission(SecurityAction.Demand, Role = "Tablas.Eliminar")]
        LibResponse ILibDataComponent<IList<LineaDeProducto>, IList<LineaDeProducto>>.Delete(IList<LineaDeProducto> refRecord) {
            LibResponse vResult = new LibResponse();
            string vErrMsg = "";
            CurrentRecord = refRecord[0];
            if (Validate(eAccionSR.Eliminar, out vErrMsg)) {
                LibDatabase insDb = new LibDatabase();
                vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "LineaDeProductoDEL"), ParametrosClave(CurrentRecord, true, true));
                insDb.Dispose();
            } else {
                throw new GalacValidationException(vErrMsg);
            }
            return vResult;
        }

        IList<LineaDeProducto> ILibDataComponent<IList<LineaDeProducto>, IList<LineaDeProducto>>.GetData(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters) {
            List<LineaDeProducto> vResult = new List<LineaDeProducto>();
            LibDatabase insDb = new LibDatabase();
            switch (valType) {
                case eProcessMessageType.SpName:
                    valProcessMessage = insDb.ToSpName(DbSchema, valProcessMessage);
                    vResult = insDb.LoadFromSp<LineaDeProducto>(valProcessMessage, valParameters, CmdTimeOut);
                    break;
                case eProcessMessageType.Query:
                    vResult = insDb.LoadData<LineaDeProducto>(valProcessMessage, CmdTimeOut, valParameters);
                    break;
                default: throw new ProgrammerMissingCodeException();
            }
            insDb.Dispose();
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Tablas.Insertar")]
        [PrincipalPermission(SecurityAction.Demand, Role = "Compañía.Insertar")]
        LibResponse ILibDataComponent<IList<LineaDeProducto>, IList<LineaDeProducto>>.Insert(IList<LineaDeProducto> refRecord) {
            LibResponse vResult = new LibResponse();
            string vErrMsg = "";
            CurrentRecord = refRecord[0];
            if (ExecuteProcessBeforeInsert()) {
                if (Validate(eAccionSR.Insertar, out vErrMsg)) {
                    LibDatabase insDb = new LibDatabase();
                    vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "LineaDeProductoINS"), ParametrosActualizacion(CurrentRecord, eAccionSR.Insertar));
                    insDb.Dispose();
                } else {
                    throw new GalacValidationException(vErrMsg);
                }
            }
            return vResult;
        }

        XElement ILibDataComponent<IList<LineaDeProducto>, IList<LineaDeProducto>>.QueryInfo(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters) {
            XElement vResult = null;
            LibDatabase insDb = new LibDatabase();
            switch (valType) {
                case eProcessMessageType.Message:
                    if (valProcessMessage.Equals("ProximoConsecutivo")) {
                        vResult = LibXml.ValueToXElement(insDb.NextLngConsecutive(DbSchema + ".LineaDeProducto", "Consecutivo", valParameters), "Consecutivo");
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

        [PrincipalPermission(SecurityAction.Demand, Role = "Tablas.Modificar")]
        LibResponse ILibDataComponent<IList<LineaDeProducto>, IList<LineaDeProducto>>.Update(IList<LineaDeProducto> refRecord) {
            LibResponse vResult = new LibResponse();
            string vErrMsg ="";
            CurrentRecord = refRecord[0];
            if (ExecuteProcessBeforeUpdate()) {
                if (Validate(eAccionSR.Modificar, out vErrMsg)) {
                    LibDatabase insDb = new LibDatabase();
                    vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "LineaDeProductoUPD"), ParametrosActualizacion(CurrentRecord, eAccionSR.Modificar));
                    insDb.Dispose();
                } else {
                    throw new GalacValidationException(vErrMsg);
                }
            }
            return vResult;
        }

        bool ILibDataComponent<IList<LineaDeProducto>, IList<LineaDeProducto>>.ValidateAll(IList<LineaDeProducto> refRecords, eAccionSR valAction, StringBuilder refErrorMessage) {
            bool vResult = true;
            string vErroMessage = "";
            foreach (LineaDeProducto vItem in refRecords) {
                CurrentRecord = vItem;
                vResult = vResult && Validate(valAction, out vErroMessage);
                if (LibString.IsNullOrEmpty(vErroMessage, true)) {
                    refErrorMessage.AppendLine(vErroMessage);
                }
            }
            return vResult;
        }

        LibResponse ILibDataComponent<IList<LineaDeProducto>, IList<LineaDeProducto>>.SpecializedUpdate(IList<LineaDeProducto> refRecord, string valSpecializedAction) {
            throw new ProgrammerMissingCodeException();
        }
        #endregion //ILibDataComponent<IList<LineaDeProducto>, IList<LineaDeProducto>>
        #region Validaciones
        protected override bool Validate(eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            ClearValidationInfo();
            vResult = IsValidConsecutivoCompania(valAction, CurrentRecord.ConsecutivoCompania, CurrentRecord.Consecutivo);
            vResult = IsValidNombre(valAction, CurrentRecord.ConsecutivoCompania, CurrentRecord.Nombre) && vResult;
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

        private bool IsValidNombre(eAccionSR valAction, int valConsecutivoCompania, string valNombre) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valNombre = LibString.Trim(valNombre);
            if (LibString.IsNullOrEmpty(valNombre, true)) {
                BuildValidationInfo(MsgRequiredField("Nombre de la Línea"));
                vResult = false;
            } else if (valAction == eAccionSR.Insertar) {
                LineaDeProducto vRecBusqueda = new LineaDeProducto();
                vRecBusqueda.Nombre = valNombre;
                if (KeyExists(valConsecutivoCompania, valNombre)) {
                    BuildValidationInfo(MsgFieldValueAlreadyExist("Nombre", valNombre));
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool KeyExists(int valConsecutivoCompania, string valNombre) {
            bool vResult = false;
            LineaDeProducto vRecordBusqueda = new LineaDeProducto();
            vRecordBusqueda.ConsecutivoCompania = valConsecutivoCompania;
            vRecordBusqueda.Nombre = valNombre;
            LibDatabase insDb = new LibDatabase();
            vResult = insDb.ExistsRecord(DbSchema + ".LineaDeProducto", "ConsecutivoCompania", ParametrosClave(vRecordBusqueda, false, false));
            insDb.Dispose();
            return vResult;
        }

        private bool KeyExists(int valConsecutivoCompania, LineaDeProducto valRecordBusqueda) {
            bool vResult = false;
            valRecordBusqueda.ConsecutivoCompania = valConsecutivoCompania;
            LibDatabase insDb = new LibDatabase();
            vResult = insDb.ExistsRecord(DbSchema + ".LineaDeProducto", "ConsecutivoCompania", ParametrosClave(valRecordBusqueda, false, false));
            insDb.Dispose();
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

        [PrincipalPermission(SecurityAction.Demand, Role = "Tablas.Insertar")]
        [PrincipalPermission(SecurityAction.Demand, Role = "Tablas.Importar")]
        LibXmlResult ILibDataImport.Import(XmlReader refRecord, LibProgressManager valManager, bool valShowMessage) {
            try {
                string vMessage = "";
                int vIndex = 0;
                LibXmlResult vResult = new LibXmlResult();
                vResult.AddTitle("Importación Línea de Producto");
                List<LineaDeProducto> vList = ParseToListEntity(refRecord);
                if (vList.Count > 0) {
                    LibDatabase insDb = new LibDatabase();
                    if (((ILibDataImportBulkInsert)this).Action == eActionImpExp.eAIE_Instalar) {
                        try {
                            string vTablename = DbSchema + ".LineaDeProducto";
                            DataTable vDataTable = insDb.ParseListToDt<LineaDeProducto>(vList, vTablename, valManager);
                            valManager.ReportProgress(vList.Count, "Realizando inserción en lote, por favor espere...", string.Empty, false);
                            insDb.BulkInsert(vDataTable, vTablename);
                        } catch (System.Data.SqlClient.SqlException vEx) {
                            throw new GalacException("Error procesando los datos, debe verificar los datos a importar.", eExceptionManagementType.Controlled, vEx);
                        }
                    } else {
                        int vTotal = vList.Count();
                        foreach (LineaDeProducto item in vList) {
                            try {
                                vMessage = string.Format("Insertando {0:n0} de {1:n0}", vIndex, vTotal);
                                item.ConsecutivoCompania = LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania");
                                item.Consecutivo = 1;
                                insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "LineaDeProductoINST"), ParametrosActualizacion(item, eAccionSR.ReInstalar));
                            } catch (System.Data.SqlClient.SqlException vEx) {
                                if (LibExceptionMng.IsPrimaryKeyViolation(vEx)) {
                                    vResult.AddDetailWithAttribute(item.Nombre, "Ya existe", eXmlResultType.Error);
                                } else {
                                    throw;
                                }
                            } catch (GalacException valException) {
                                if (LibString.S1IsInS2("Ya existe la clave única.", valException.Message)) {
                                    vResult.AddDetailWithAttribute(item.Nombre, "Ya existe", eXmlResultType.Error);
                                } else {
                                    throw;
                                }
                            }
                            if (valManager.CancellationPending) {
                                break;
                            }
                            vIndex++;
                            valManager.ReportProgress(vIndex, "Ejecutando por favor espere...", vMessage, (vIndex >= vTotal) && (valShowMessage));
                        }
                    }
                    insDb.Dispose();
                }
                return vResult;
            } catch (Exception) {
                throw;
            }
        }

        private List<LineaDeProducto> ParseToListEntity(XmlReader valXmlEntity) {
            List<LineaDeProducto> vResult = new List<LineaDeProducto>();
            XDocument xDoc = XDocument.Load(valXmlEntity);
            var vEntity = from vRecord in xDoc.Descendants("GpResult")
                          select vRecord;
            int vConsecutivo = 0;
            foreach (XElement vItem in vEntity) {
                LineaDeProducto vRecord = new LineaDeProducto();
                vRecord.Clear();
                vRecord.ConsecutivoCompania = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("Compania", "ConsecutivoCompania");
                vRecord.Consecutivo = vConsecutivo++;
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Consecutivo"), null))) {
                    vRecord.Consecutivo = LibConvert.ToInt(vItem.Element("Consecutivo"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Nombre"), null))) {
                    vRecord.Nombre = vItem.Element("Nombre").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("PorcentajeComision"), null))) {
                    vRecord.PorcentajeComision = LibConvert.ToDec(vItem.Element("PorcentajeComision"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("CentroDeCosto"), null))) {
                    vRecord.CentroDeCosto = vItem.Element("CentroDeCosto").Value;
                }
                vRecord.NombreOperador = ((CustomIdentity)Thread.CurrentPrincipal.Identity).Login;
                vResult.Add(vRecord);
            }
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class clsLineaDeProductoDat

} //End of namespace Galac.Saw.Dal.Tablas

