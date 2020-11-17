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
using LibGalac.Aos.Catching;
using LibGalac.Aos.Dal;
using LibGalac.Aos.DefGen;
using Galac.Saw.Ccl.Integracion;

namespace Galac.Saw.Dal.Integracion {
    public class clsIntegracionDat: LibData, ILibDataComponentWithSearch {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores

        public clsIntegracionDat() {
            DbSchema = "Saw";
        }
        #endregion //Constructores
        #region Metodos Generados

        private StringBuilder ParametrosActualizacion(recIntegracion valRecord, eAccionSR valAction) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInEnum("TipoIntegracion", valRecord.TipoIntegracionAsDB);
            vParams.AddInString("version", valRecord.version, 8);
            if (valAction == eAccionSR.Modificar ) {
                vParams.AddInEnum("TipoIntegracionOriginal", valRecord.TipoIntegracionOriginalAsDB);
                vParams.AddInString("versionOriginal", valRecord.versionOriginal, 8);
            }
            vParams.AddInString("NombreOperador", ((CustomIdentity) Thread.CurrentPrincipal.Identity).Login, 10);
            vParams.AddInDateTime("FechaUltimaModificacion", LibDate.Today());
            if (valAction == eAccionSR.Modificar) {
                vParams.AddInTimestamp("TimeStampAsInt", valRecord.fldTimeStamp);
            }
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosClave(recIntegracion valRecord, bool valIncludeTimestamp, bool valAddReturnParameter) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            if (valAddReturnParameter) {
                vParams.AddReturn();
            }
            vParams.AddInEnum("TipoIntegracion", valRecord.TipoIntegracionAsDB);
            vParams.AddInString("version", valRecord.version, 8);
            if (valIncludeTimestamp) {
                vParams.AddInTimestamp("TimeStampAsInt", valRecord.fldTimeStamp);
            }
            vResult = vParams.Get();
            return vResult;
        }

        XmlReader ParseToXml(recIntegracion valEntity) {
            List<recIntegracion> vListEntidades = new List<recIntegracion>();
            vListEntidades.Add(valEntity);
            XElement vXElement = new XElement("GpData",
                from vEntity in vListEntidades
                select new XElement("GpResult",
                    new XElement("TipoIntegracion", vEntity.TipoIntegracionAsDB),
                    new XElement("version", vEntity.version),
                    new XElement("fldTimeStamp", vEntity.fldTimeStamp)));
            XmlReader vResult = vXElement.CreateReader();
            return vResult;
        }
        recIntegracion ParseToEntity(XmlReader valXmlEntityReader, eAccionSR valAction) {
            recIntegracion vResult = new recIntegracion();
            XDocument xDoc = XDocument.Load(valXmlEntityReader);
            var vEntity = from vRecord in xDoc.Descendants("GpResult")
                          select vRecord;
            foreach (XElement vItem in vEntity) {
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("TipoIntegracion"), null))) {
                    vResult.TipoIntegracion = (eTipoIntegracion)LibConvert.DbValueToEnum(vItem.Element("TipoIntegracion"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("version"), null))) {
                    vResult.version = vItem.Element("version").Value;
                }
                if (valAction == eAccionSR.Modificar) {
                    if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("TipoIntegracionOriginal"), null))) {
                        vResult.TipoIntegracionOriginal = (eTipoIntegracion)LibConvert.DbValueToEnum(vItem.Element("TipoIntegracionOriginal"));
                    }
                    if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("versionOriginal"), null))) {
                        vResult.versionOriginal = vItem.Element("versionOriginal").Value;
                    }
                    if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("NombreOperadorOriginal"), null))) {
                    }
                    if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("FechaUltimaModificacionOriginal"), null))) {
                    }
                }
                vResult.fldTimeStamp = LibConvert.ToLong(vItem.Element("fldTimeStamp"));
            }
            return vResult;
        }
        #region Miembros de ILibDataComponent

        bool ILibDataComponent.CanBeChoosen(XmlReader refRecord, eAccionSR valAction) {
            bool vResult = false;
            recIntegracion vRecord = ParseToEntity(refRecord, valAction);
            StringBuilder vSbInfo = new StringBuilder();
            string vErrMsg = "";
            LibDatabase insDB = new LibDatabase();
            if (valAction == eAccionSR.Eliminar) {
                if (vSbInfo.Length == 0) {
                    vResult = true;
                }
            } else {
                vResult = true;
            }
            insDB.Dispose();
            if (!vResult) {
                vErrMsg = LibResMsg.InfoForeignKeyCanNotBeDeleted(vSbInfo.ToString());
                throw new GalacAlertException(vErrMsg);
            } else {
                return vResult;
            }
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Integracion.Eliminar")]
        LibResponse ILibDataComponent.Delete(XmlReader refRecord) {
            LibResponse vResult = new LibResponse();
            string vErrMsg = "";
            CurrentParser = new LibXmlDataParse(refRecord);
            if (Validate(eAccionSR.Eliminar, out vErrMsg)) {
                recIntegracion vRecord = ParseToEntity(CurrentParser.ToReader(), eAccionSR.Eliminar);
                LibDatabase insDb = new LibDatabase();
                vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "IntegracionDEL"), ParametrosClave(vRecord, true, true));
                insDb.Dispose();
            } else {
                throw new GalacValidationException(vErrMsg);
            }
            return vResult;
        }

        XmlReader ILibDataComponent.GetData(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters) {
            XmlDocument vResult = null;
            LibDatabase insDb = new LibDatabase();
            switch (valType) {
                case eProcessMessageType.SpName:
                    valProcessMessage = insDb.ToSpName(DbSchema, valProcessMessage);
                    vResult = insDb.LoadFromSp(valProcessMessage, valParameters, CmdTimeOut);
                    break;
                default: break;
            }
            insDb.Dispose();
            return new XmlNodeReader(vResult);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Integracion.Insertar")]
        LibResponse ILibDataComponent.Insert(XmlReader refRecord) {
            LibResponse vResult = new LibResponse();
            string vErrMsg = "";
            CurrentParser = new LibXmlDataParse(refRecord);
            if (ExecuteProcessBeforeInsert()) {
                if (Validate(eAccionSR.Insertar, out vErrMsg)) {
                    recIntegracion vRecord = ParseToEntity(CurrentParser.ToReader(), eAccionSR.Insertar);
                    LibDatabase insDb = new LibDatabase();
                    vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "IntegracionINS"), ParametrosActualizacion(vRecord, eAccionSR.Insertar));
                    insDb.Dispose();
                } else {
                    throw new GalacValidationException(vErrMsg);
                }
            }
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Integracion.Modificar")]
        LibResponse ILibDataComponent.Update(XmlReader refRecord) {
            LibResponse vResult = new LibResponse();
            string vErrMsg ="";
            CurrentParser = new LibXmlDataParse(refRecord);
            if (ExecuteProcessBeforeUpdate()) {
                if (Validate(eAccionSR.Modificar, out vErrMsg)) {
                    recIntegracion vRecord = ParseToEntity(CurrentParser.ToReader(), eAccionSR.Modificar);
                    LibDatabase insDb = new LibDatabase();
                    vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "IntegracionUPD"), ParametrosActualizacion(vRecord, eAccionSR.Modificar));
                    insDb.Dispose();
                } else {
                    throw new GalacValidationException(vErrMsg);
                }
            }
            return vResult;
        }
        #endregion //ILibDataComponent
        #region Validaciones
        protected override bool Validate(eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            ClearValidationInfo();
            eTipoIntegracion vTipoIntegracion = (eTipoIntegracion)CurrentParser.GetEnum(0, "TipoIntegracion", (int)eTipoIntegracion.NOMINA);
            vResult = IsValidTipoIntegracion(valAction, vTipoIntegracion);
            string vversion = CurrentParser.GetString(0, "version", "");
            vResult = IsValidversion(valAction, vversion) && vResult;
            outErrorMessage = Information.ToString();
            return vResult;
        }

        private bool IsValidTipoIntegracion(eAccionSR valAction, eTipoIntegracion valTipoIntegracion){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            return vResult;
        }

        private bool IsValidversion(eAccionSR valAction, string valversion){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valversion = LibString.Trim(valversion);
            if (LibString.IsNullOrEmpty(valversion, true)) {
                BuildValidationInfo(MsgRequiredField("version"));
                vResult = false;
            } else if (valAction == eAccionSR.Insertar) {
                
            }
            return vResult;
        }

        private bool KeyExists(eTipoIntegracion valTipoIntegracion, string valversion) {
            bool vResult = false;
            recIntegracion vRecordBusqueda = new recIntegracion();
            vRecordBusqueda.TipoIntegracion = valTipoIntegracion;
            vRecordBusqueda.version = valversion;
            LibDatabase insDb = new LibDatabase();
            vResult = insDb.ExistsRecord("Saw.Integracion", "TipoIntegracion", ParametrosClave(vRecordBusqueda, false, false));
            insDb.Dispose();
            return vResult;
        }
        #endregion //Validaciones
        #region Miembros de ILibDataSearch
        bool ILibDataSearch.ConnectFk(string valQuery, ref XmlDocument refResulset, StringBuilder valXmlParamsExpression) {
            bool vResult = false;
            LibDatabase insDb = new LibDatabase();
            refResulset = insDb.LoadData(valQuery, valXmlParamsExpression, CmdTimeOut);
            if (refResulset != null && refResulset.DocumentElement != null && refResulset.DocumentElement.HasChildNodes) {
                vResult = true;
            }
            return vResult;
        }
        #endregion //Miembros de ILibDataSearch
        #endregion //Metodos Generados


    } //End of class clsIntegracionDat

} //End of namespace Galac.Saw.Dal.Integracion

