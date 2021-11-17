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
using Galac.Saw.Ccl.Inventario;

namespace Galac.Saw.Dal.Inventario {
    public class clsAlmacenDat : LibData, ILibDataComponentWithSearch<IList<Almacen>, IList<Almacen>> {
        #region Variables
        Almacen _CurrentRecord;
        #endregion //Variables
        #region Propiedades
        private Almacen CurrentRecord {
            get { return _CurrentRecord; }
            set { _CurrentRecord = value; }
        }
        #endregion //Propiedades
        #region Constructores
        
        public clsAlmacenDat() {
            DbSchema = "Saw";
        }
        #endregion //Constructores
        #region Metodos Generados

        private StringBuilder ParametrosActualizacion(Almacen valRecord, eAccionSR valAction) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInInteger("Consecutivo", valRecord.Consecutivo);
            vParams.AddInString("Codigo", valRecord.Codigo, 5);
            vParams.AddInString("NombreAlmacen", valRecord.NombreAlmacen, 40);
            vParams.AddInEnum("TipoDeAlmacen", valRecord.TipoDeAlmacenAsDB);
            vParams.AddInInteger("ConsecutivoCliente", valRecord.ConsecutivoCliente);
            vParams.AddInString("CodigoCc", valRecord.CodigoCc, 5);
            vParams.AddInString("Descripcion", valRecord.Descripcion, 40);
            vParams.AddInString("NombreOperador", ((CustomIdentity) Thread.CurrentPrincipal.Identity).Login, 10);
            vParams.AddInDateTime("FechaUltimaModificacion", LibDate.Today());
            if (valAction == eAccionSR.Modificar) {
                vParams.AddInTimestamp("TimeStampAsInt", valRecord.fldTimeStamp);
            }
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosClave(Almacen valRecord, bool valIncludeTimestamp, bool valAddReturnParameter) {
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

        private StringBuilder ParametrosProximoConsecutivo(Almacen valRecord) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosCliente(Almacen valRecord) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInInteger("Consecutivo", valRecord.ConsecutivoCliente);
            vResult = vParams.Get();
            return vResult;
        }
        #region Miembros de ILibDataComponent<IList<Almacen>, IList<Almacen>>

        LibResponse ILibDataComponent<IList<Almacen>, IList<Almacen>>.CanBeChoosen(IList<Almacen> refRecord, eAccionSR valAction) {
            LibResponse vResult = new LibResponse();
            Almacen vRecord = refRecord[0];
            StringBuilder vSbInfo = new StringBuilder();
            string vErrMsg = "";
            LibDatabase insDB = new LibDatabase();
            if (valAction == eAccionSR.Eliminar) {
                if (LibDefGen.IsProduct(LibProduct.GetInitialsSaw())) {
                    if (getParametrosCompania<string>(vRecord.ConsecutivoCompania, "CodigoAlmacenGenerico", this).Equals(vRecord.Codigo)) {
                        vSbInfo.AppendLine("Parámetros Compañía");
                    }
                } else {
                    if (insDB.ExistsValueOnMultifile("ParametrosCompania", "CodigoAlmacenGenerico", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Codigo), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                        vSbInfo.AppendLine("Parámetros Compañía");
                    }
                }
                if (insDB.ExistsValueOnMultifile("dbo.Factura", "CodigoAlmacen", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Codigo), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                    vSbInfo.AppendLine("Factura");
                }
                if (insDB.ExistsValueOnMultifile("dbo.ExistenciaPorAlmacen", "CodigoAlmacen", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Codigo), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                    vSbInfo.AppendLine("Existencia Por Almacen");
                }
                if (insDB.ExistsValueOnMultifile("dbo.RenglonExistenciaAlmacen", "CodigoAlmacen", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Codigo), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                    vSbInfo.AppendLine("Renglon Existencia por Almacen");
                }
                if (insDB.ExistsValueOnMultifile("dbo.NotaDeEntradaSalida", "CodigoAlmacen", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Codigo), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                    vSbInfo.AppendLine("Nota de Entrada/Salida");
                }
                if (insDB.ExistsValueOnMultifile("dbo.Transferencia", "CodigoAlmacenEntrada", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Codigo), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                    vSbInfo.AppendLine("Transferencia");
                }
                if (insDB.ExistsValueOnMultifile("dbo.Transferencia", "CodigoAlmacenSalida", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Codigo), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                    vSbInfo.AppendLine("Transferencia");
                }
                if (insDB.ExistsValueOnMultifile("dbo.Compra", "CodigoAlmacen", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Codigo), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                    vSbInfo.AppendLine("Compra");
                }
                if (insDB.ExistsValueOnMultifile("dbo.ConteoFisico", "CodigoAlmacen", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Codigo), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
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

        private T getParametrosCompania<T>(int valConsecutivoCompania, string ValParametro, ILibDataComponent<IList<Almacen>, IList<Almacen>> instanciaDal) {
            string vDbSchema = "";
            if (LibDefGen.IsProduct(LibProduct.GetInitialsAdmEcuador())){
                vDbSchema = "Adme";
            } else {
                vDbSchema = "Comun";
            }
            StringBuilder sql = new StringBuilder("SELECT " + vDbSchema + ".SettValueByCompany.Value AS Valor, " + vDbSchema + ".SettValueByCompany.NameSettDefinition FROM " + vDbSchema + ".SettDefinition INNER JOIN " + vDbSchema + ".SettValueByCompany ON " + vDbSchema + ".SettDefinition.Name = " + vDbSchema + ".SettValueByCompany.NameSettDefinition WHERE (" + vDbSchema + ".SettDefinition.Name = '" + ValParametro + "') AND (" + vDbSchema + ".SettValueByCompany.ConsecutivoCompania = " + valConsecutivoCompania + ")", 300);
            XElement Auxiliar = instanciaDal.QueryInfo(eProcessMessageType.Query, null, sql);
            Object vValor = LibXml.GetPropertyString(Auxiliar, "Valor");
            return (T)vValor;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Almacén.Eliminar")]
        LibResponse ILibDataComponent<IList<Almacen>, IList<Almacen>>.Delete(IList<Almacen> refRecord) {
            LibResponse vResult = new LibResponse();
            string vErrMsg = "";
            CurrentRecord = refRecord[0];
            if (Validate(eAccionSR.Eliminar, out vErrMsg)) {
                LibDatabase insDb = new LibDatabase();
                vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "AlmacenDEL"), ParametrosClave(CurrentRecord, true, true));
                insDb.Dispose();
            } else {
                throw new GalacValidationException(vErrMsg);
            }
            return vResult;
        }

        IList<Almacen> ILibDataComponent<IList<Almacen>, IList<Almacen>>.GetData(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters) {
            List<Almacen> vResult = new List<Almacen>();
            LibDatabase insDb = new LibDatabase();
            switch (valType) {
                case eProcessMessageType.SpName:
                    valProcessMessage = insDb.ToSpName(DbSchema, valProcessMessage);
                    vResult = insDb.LoadFromSp<Almacen>(valProcessMessage, valParameters, CmdTimeOut);
                    break;
                default: break;
            }
            insDb.Dispose();
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Almacén.Insertar")]
        [PrincipalPermission(SecurityAction.Demand, Role = "Compañía.Insertar")]
        LibResponse ILibDataComponent<IList<Almacen>, IList<Almacen>>.Insert(IList<Almacen> refRecord) {
            LibResponse vResult = new LibResponse();
            string vErrMsg = "";
            CurrentRecord = refRecord[0];
            if (ExecuteProcessBeforeInsert()) {
                if (Validate(eAccionSR.Insertar, out vErrMsg)) {
                    LibDatabase insDb = new LibDatabase();
                    CurrentRecord.Consecutivo = insDb.NextLngConsecutive("Saw.Almacen", "Consecutivo", ParametrosProximoConsecutivo(CurrentRecord));
                    vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "AlmacenINS"), ParametrosActualizacion(CurrentRecord, eAccionSR.Insertar));
                    insDb.Dispose();
                } else {
                    throw new GalacValidationException(vErrMsg);
                }
            }
            return vResult;
        }

        XElement ILibDataComponent<IList<Almacen>, IList<Almacen>>.QueryInfo(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters) {
            XElement vResult = null;
            LibDatabase insDb = new LibDatabase();
            switch (valType) {
                case eProcessMessageType.SpName:
                    vResult = LibXml.ToXElement(insDb.LoadFromSp(valProcessMessage, valParameters, CmdTimeOut));
                    break;
                case eProcessMessageType.Query:
                    vResult = LibXml.ToXElement(insDb.LoadData(valParameters.ToString(), CmdTimeOut));
                    break;
                case eProcessMessageType.Message:
                    if (valProcessMessage == "ProximoNumero") {
                        vResult = LibXml.ToXElement(LibXml.ValueToXmlDocument(insDb.NextLngConsecutive("Saw.Almacen", "Consecutivo", valParameters.ToString()), "Consecutivo"));
                    }
                     break;
                default: break;
            }
            insDb.Dispose();
            return vResult;
        }


         [PrincipalPermission(SecurityAction.Demand, Role = "Almacén.Modificar")]
        LibResponse ILibDataComponent<IList<Almacen>, IList<Almacen>>.Update(IList<Almacen> refRecord) {
            LibResponse vResult = new LibResponse();
            string vErrMsg ="";
            CurrentRecord = refRecord[0];
            if (ExecuteProcessBeforeUpdate()) {
                if (Validate(eAccionSR.Modificar, out vErrMsg)) {
                    LibDatabase insDb = new LibDatabase();
                    vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "AlmacenUPD"), ParametrosActualizacion(CurrentRecord, eAccionSR.Modificar));
                    insDb.Dispose();
                } else {
                    throw new GalacValidationException(vErrMsg);
                }
            }
            return vResult;
        }

        bool ILibDataComponent<IList<Almacen>, IList<Almacen>>.ValidateAll(IList<Almacen> refRecords, eAccionSR valAction, StringBuilder refErrorMessage) {
            bool vResult = true;
            string vErroMessage = "";
            foreach (Almacen vItem in refRecords) {
                CurrentRecord = vItem;
                vResult = vResult && Validate(valAction, out vErroMessage);
                if (LibString.IsNullOrEmpty(vErroMessage, true)) {
                    refErrorMessage.AppendLine(vErroMessage);
                }
            }
            return vResult;
        }

        LibResponse ILibDataComponent<IList<Almacen>, IList<Almacen>>.SpecializedUpdate(IList<Almacen> refRecord, string valSpecializedAction) {
            throw new NotImplementedException();
        }
        #endregion //ILibDataComponent<IList<Almacen>, IList<Almacen>>
        #region Validaciones
        protected override bool Validate(eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            ClearValidationInfo();
            vResult = IsValidConsecutivoCompania(valAction, CurrentRecord.ConsecutivoCompania, CurrentRecord.Consecutivo);
            //vResult = IsValidConsecutivo(valAction, CurrentRecord.Consecutivo) && vResult;
            vResult = IsValidCodigo(valAction, CurrentRecord.ConsecutivoCompania, CurrentRecord.Consecutivo, CurrentRecord.Codigo) && vResult;
            vResult = IsValidNombreAlmacen(valAction, CurrentRecord.NombreAlmacen) && vResult;
            vResult = IsValidConsecutivoCliente(valAction, CurrentRecord.ConsecutivoCompania, CurrentRecord.ConsecutivoCliente) && vResult;
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
        //private bool IsValidConsecutivo(eAccionSR valAction, int valConsecutivo){
        //    bool vResult = true;
        //    if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
        //        return true;
        //    }
        //    if (valConsecutivo == 0) {
        //        BuildValidationInfo(MsgRequiredField("Consecutivo"));
        //        vResult = false;
        //    }
        //    return vResult;
        //}

        private bool IsValidCodigo(eAccionSR valAction, int valConsecutivoCompania, int valConsecutivo, string valCodigo){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCodigo = LibString.Trim(valCodigo);
            if (LibString.IsNullOrEmpty(valCodigo, true)) {
                BuildValidationInfo(MsgRequiredField("Codigo"));
                vResult = false;
            } else if (valAction == eAccionSR.Insertar) {
                if (KeyExists(valConsecutivoCompania, valConsecutivo)) {
                    BuildValidationInfo(MsgFieldValueAlreadyExist("Codigo", valCodigo));
                    vResult = false;
                }
            
            } else if (valAction == eAccionSR.Eliminar) {
                StringBuilder vSbInfo = new StringBuilder();
                vSbInfo = ValidateFK(valConsecutivoCompania, valCodigo);
                if (vSbInfo != null && vSbInfo.Length > 0) {
                    BuildValidationInfo(LibResMsg.InfoForeignKeyCanNotBeDeleted(vSbInfo.ToString()));
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidNombreAlmacen(eAccionSR valAction, string valNombreAlmacen) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (LibText.IsNullOrEmpty(valNombreAlmacen)) {
                BuildValidationInfo(MsgRequiredField("Nombre Almacen"));
                vResult = false;
            }
            return vResult;
        }

        private bool IsValidConsecutivoCliente(eAccionSR valAction, int valConsecutivoCompania, int valConsecutivoCliente) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            //if (valConsecutivoCliente != 0) {
                Almacen vRecordBusqueda = new Almacen();
                vRecordBusqueda.ConsecutivoCompania = valConsecutivoCompania;
                vRecordBusqueda.ConsecutivoCliente = valConsecutivoCliente;
                LibDatabase insDb = new LibDatabase();
                if (!insDb.ExistsRecord("Cliente", "ConsecutivoCompania", ParametrosCliente(vRecordBusqueda)))
                    vResult = false;
            //}
            return vResult;
        }

        private bool KeyExists(int valConsecutivoCompania, int valConsecutivo) {
            bool vResult = false;
            Almacen vRecordBusqueda = new Almacen();
            vRecordBusqueda.ConsecutivoCompania = valConsecutivoCompania;
            vRecordBusqueda.Consecutivo = valConsecutivo;
            LibDatabase insDb = new LibDatabase();
            vResult = insDb.ExistsRecord("Saw.Almacen", "ConsecutivoCompania", ParametrosClave(vRecordBusqueda, false, false));
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
        #endregion //Metodos Generados

        private StringBuilder ValidateFK(int valConsecutivoCompania, string valValue) {
            LibDatabase insDB = new LibDatabase();
            StringBuilder vResult = new StringBuilder();
            string vFieldSQLValue = insDB.InsSql.ToSqlValue(valValue);
            bool vIsAdmInt = LibDefGen.IsProduct(LibProduct.GetInitialsAdmInterno());
            bool vIsSaw = LibDefGen.IsProduct(LibProduct.GetInitialsSaw());
            bool vIsAdmEc = LibDefGen.IsProduct(LibProduct.GetInitialsAdmEcuador());
            if (insDB.ExistsValueOnMultifile("dbo.ParametrosCompania", "CodigoAlmacenGenerico", "ConsecutivoCompania", vFieldSQLValue, insDB.InsSql.ToSqlValue(valConsecutivoCompania), true)) {
                vResult.AppendLine("Parametros Compania");
            }
            if (insDB.ExistsValueOnMultifile("dbo.Factura", "CodigoAlmacen", "ConsecutivoCompania", vFieldSQLValue, insDB.InsSql.ToSqlValue(valConsecutivoCompania), true)) {
                vResult.AppendLine("Factura");
            }
            if (insDB.ExistsValueOnMultifile("dbo.ExistenciaPorAlmacen", "CodigoAlmacen", "ConsecutivoCompania", vFieldSQLValue, insDB.InsSql.ToSqlValue(valConsecutivoCompania), true)) {
                vResult.AppendLine("Existencia Por Almacen");
            }
            if (insDB.ExistsValueOnMultifile("dbo.RenglonExistenciaAlmacen", "CodigoAlmacen", "ConsecutivoCompania", vFieldSQLValue, insDB.InsSql.ToSqlValue(valConsecutivoCompania), true)) {
                vResult.AppendLine("Renglon Existencia por Almacen");
            }
            if (insDB.ExistsValueOnMultifile("dbo.NotaDeEntradaSalida", "CodigoAlmacen", "ConsecutivoCompania", vFieldSQLValue, insDB.InsSql.ToSqlValue(valConsecutivoCompania), true)) {
                vResult.AppendLine("Nota de Entrada/Salida");
            }
            if (insDB.ExistsValueOnMultifile("dbo.Transferencia", "CodigoAlmacenEntrada", "ConsecutivoCompania", vFieldSQLValue, insDB.InsSql.ToSqlValue(valConsecutivoCompania), true)) {
                vResult.AppendLine("Transferencia");
            }
            if (insDB.ExistsValueOnMultifile("dbo.Transferencia", "CodigoAlmacenSalida", "ConsecutivoCompania", vFieldSQLValue, insDB.InsSql.ToSqlValue(valConsecutivoCompania), true)) {
                vResult.AppendLine("Transferencia");
            }
            if (insDB.ExistsValueOnMultifile("dbo.Compra", "CodigoAlmacen", "ConsecutivoCompania", vFieldSQLValue, insDB.InsSql.ToSqlValue(valConsecutivoCompania), true)) {
                vResult.AppendLine("Compra");
            }
            if (insDB.ExistsValueOnMultifile("dbo.ConteoFisico", "CodigoAlmacen", "ConsecutivoCompania", vFieldSQLValue, insDB.InsSql.ToSqlValue(valConsecutivoCompania), true)) {
                vResult.AppendLine("Conteo Fisico");
            }
            insDB.Dispose();
            return vResult;
        }

    } //End of class clsAlmacenDat

} //End of namespace Galac.Saw.Dal.Inventario

