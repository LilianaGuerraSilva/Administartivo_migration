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
    public class clsCategoriaDat: LibData, ILibDataComponentWithSearch<IList<Categoria>, IList<Categoria>> {
        #region Variables
        Categoria _CurrentRecord;
        #endregion //Variables
        #region Propiedades
        private Categoria CurrentRecord {
            get { return _CurrentRecord; }
            set { _CurrentRecord = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public clsCategoriaDat() {
            DbSchema = "Saw";
        }
        #endregion //Constructores
        #region Metodos Generados

        private StringBuilder ParametrosActualizacion(Categoria valRecord, eAccionSR valAction) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInInteger("Consecutivo", valRecord.Consecutivo);
            vParams.AddInString("Descripcion", valRecord.Descripcion, 20);
            vParams.AddInString("NombreOperador", ((CustomIdentity) Thread.CurrentPrincipal.Identity).Login, 10);
            vParams.AddInDateTime("FechaUltimaModificacion", LibDate.Today());
            if (valAction == eAccionSR.Modificar) {
                vParams.AddInTimestamp("TimeStampAsInt", valRecord.fldTimeStamp);
            }
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosClave(Categoria valRecord, bool valIncludeTimestamp, bool valAddReturnParameter) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            if (valAddReturnParameter) {
                vParams.AddReturn();
            }
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInString("Descripcion", valRecord.Descripcion, 20);
            if (valIncludeTimestamp) {
                vParams.AddInTimestamp("TimeStampAsInt", valRecord.fldTimeStamp);
            }
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosProximoConsecutivo(Categoria valRecord) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vResult = vParams.Get();
            return vResult;
        }
        #region Miembros de ILibDataComponent<IList<Categoria>, IList<Categoria>>

        LibResponse ILibDataComponent<IList<Categoria>, IList<Categoria>>.CanBeChoosen(IList<Categoria> refRecord, eAccionSR valAction) {
            LibResponse vResult = new LibResponse();
            Categoria vRecord = refRecord[0];
            StringBuilder vSbInfo = new StringBuilder();
            string vErrMsg = "";
            LibDatabase insDB = new LibDatabase();
            if (valAction == eAccionSR.Eliminar) {
                if (insDB.ExistsValueOnMultifile("dbo.ArticuloInventario", "Categoria", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Descripcion), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), (LibDefGen.IsProduct(LibProduct.GetInitialsAdmInterno()) || LibDefGen.IsProduct(LibProduct.GetInitialsSaw())))) {
                    vSbInfo.AppendLine("ArticuloInventario");
                }
                if (insDB.ExistsValueOnMultifile("dbo.PreguntasMasComunes", "NombrePrograma", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Descripcion), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), LibDefGen.IsProduct(LibProduct.GetInitialsAdmInterno()))) {
                    vSbInfo.AppendLine("Preguntas Mas Comunes");
                }
                if (insDB.ExistsValueOnMultifile("dbo.RevisionDeData", "NombrePrograma", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Descripcion), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), LibDefGen.IsProduct(LibProduct.GetInitialsAdmInterno()))) {
                    vSbInfo.AppendLine("Revision De Data");
                }
                if (insDB.ExistsValueOnMultifile("dbo.versionPrograma", "NombrePrograma", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Descripcion), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), LibDefGen.IsProduct(LibProduct.GetInitialsAdmInterno()))) {
                    vSbInfo.AppendLine("version Programa");
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
        LibResponse ILibDataComponent<IList<Categoria>, IList<Categoria>>.Delete(IList<Categoria> refRecord) {
            LibResponse vResult = new LibResponse();
            string vErrMsg = "";
            CurrentRecord = refRecord[0];
            if (Validate(eAccionSR.Eliminar, out vErrMsg)) {
                LibDatabase insDb = new LibDatabase();
                vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "CategoriaDEL"), ParametrosClave(CurrentRecord, true, true));
                insDb.Dispose();
            } else {
                throw new GalacValidationException(vErrMsg);
            }
            return vResult;
        }

        IList<Categoria> ILibDataComponent<IList<Categoria>, IList<Categoria>>.GetData(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters) {
            List<Categoria> vResult = new List<Categoria>();
            LibDatabase insDb = new LibDatabase();
            switch (valType) {
                case eProcessMessageType.SpName:
                    valProcessMessage = insDb.ToSpName(DbSchema, valProcessMessage);
                    vResult = insDb.LoadFromSp<Categoria>(valProcessMessage, valParameters, CmdTimeOut);
                    break;
                default: break;
            }
            insDb.Dispose();
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Tablas.Insertar")]
        LibResponse ILibDataComponent<IList<Categoria>, IList<Categoria>>.Insert(IList<Categoria> refRecord) {
            LibResponse vResult = new LibResponse();
            string vErrMsg = "";
            CurrentRecord = refRecord[0];
            if (ExecuteProcessBeforeInsert()) {
                if (Validate(eAccionSR.Insertar, out vErrMsg)) {
                    LibDatabase insDb = new LibDatabase();
                    CurrentRecord.Consecutivo = insDb.NextLngConsecutive("Saw.Categoria", "Consecutivo", ParametrosProximoConsecutivo(CurrentRecord));
                    vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "CategoriaINS"), ParametrosActualizacion(CurrentRecord, eAccionSR.Insertar));
                    insDb.Dispose();
                } else {
                    throw new GalacValidationException(vErrMsg);
                }
            }
            return vResult;
        }

        XElement ILibDataComponent<IList<Categoria>, IList<Categoria>>.QueryInfo(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters) {
            XElement vResult = null;
            LibDatabase insDb = new LibDatabase();
            switch (valType) {
                case eProcessMessageType.SpName:
                    vResult = LibXml.ToXElement(insDb.LoadFromSp(valProcessMessage, valParameters, CmdTimeOut));
                    break;
                default: break;
            }
            insDb.Dispose();
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Tablas.Modificar")]
        LibResponse ILibDataComponent<IList<Categoria>, IList<Categoria>>.Update(IList<Categoria> refRecord) {
            LibResponse vResult = new LibResponse();
            string vErrMsg ="";
            CurrentRecord = refRecord[0];
            try {
                if (ExecuteProcessBeforeUpdate()) {
                    if (Validate(eAccionSR.Modificar, out vErrMsg)) {
                        LibDatabase insDb = new LibDatabase();
                        vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "CategoriaUPD"), ParametrosActualizacion(CurrentRecord, eAccionSR.Modificar));
                        insDb.Dispose();
                    } else {
                        throw new GalacValidationException(vErrMsg);
                    }
                }
            } catch (GalacAlertException ex) {
                vErrMsg = ex.Message;
                if (LibText.S1IsInS2("Ya existe la clave", ex.Message))
                    vErrMsg = "Ya existe la categoría: " + CurrentRecord.Descripcion;
                throw new GalacAlertException(vErrMsg);
            }
            return vResult;
        }
        bool ILibDataComponent<IList<Categoria>, IList<Categoria>>.ValidateAll(IList<Categoria> refRecords, eAccionSR valAction, StringBuilder refErrorMessage) {
            bool vResult = true;
            string vErroMessage = "";
            foreach (Categoria vItem in refRecords) {
                CurrentRecord = vItem;
                vResult = vResult && Validate(valAction, out vErroMessage);
                if (LibString.IsNullOrEmpty(vErroMessage, true)) {
                    refErrorMessage.AppendLine(vErroMessage);
                }
            }
            return vResult;
        }

        LibResponse ILibDataComponent<IList<Categoria>, IList<Categoria>>.SpecializedUpdate(IList<Categoria> refRecord, string valSpecializedAction) {
            throw new NotImplementedException();
        }
        #endregion //ILibDataComponent<IList<Categoria>, IList<Categoria>>
        #region Validaciones
        protected override bool Validate(eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            ClearValidationInfo();
            vResult = IsValidConsecutivoCompania(valAction, CurrentRecord.ConsecutivoCompania, CurrentRecord.Consecutivo);
            vResult = IsValidDescripcion(valAction, CurrentRecord.ConsecutivoCompania, CurrentRecord.Descripcion) && vResult;
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

        private bool IsValidDescripcion(eAccionSR valAction, int valConsecutivoCompania, string valDescripcion){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valDescripcion = LibString.Trim(valDescripcion);
            if (LibString.IsNullOrEmpty(valDescripcion, true)) {
                BuildValidationInfo(MsgRequiredField("Categoría"));
                vResult = false;
            } else if (valAction == eAccionSR.Insertar) {
                if (KeyExists(valConsecutivoCompania, valDescripcion)) {
                    BuildValidationInfo(MsgFieldValueAlreadyExist("Categoría", valDescripcion));
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool KeyExists(int valConsecutivoCompania, string valDescripcion) {
            bool vResult = false;
            Categoria vRecordBusqueda = new Categoria();
            vRecordBusqueda.ConsecutivoCompania = valConsecutivoCompania;
            vRecordBusqueda.Descripcion = valDescripcion;
            LibDatabase insDb = new LibDatabase();
            vResult = insDb.ExistsRecord("Saw.Categoria", "ConsecutivoCompania", ParametrosClave(vRecordBusqueda, false, false));
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


    } //End of class clsCategoriaDat

} //End of namespace Galac.Saw.Dal.Inventario

