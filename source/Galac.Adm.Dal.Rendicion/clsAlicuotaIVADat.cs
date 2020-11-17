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
using Galac.Adm.Ccl.CajaChica;

namespace Galac.Adm.Dal.CajaChica {
    public class clsAlicuotaIVADat: LibData, ILibDataComponentWithSearch<IList<AlicuotaIVA>, IList<AlicuotaIVA>> {
        #region Variables
        AlicuotaIVA _CurrentRecord;
        #endregion //Variables
        #region Propiedades
        private AlicuotaIVA CurrentRecord {
            get { return _CurrentRecord; }
            set { _CurrentRecord = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public clsAlicuotaIVADat() {
            DbSchema = "dbo";
        }
        #endregion //Constructores
        #region Metodos Generados

        private StringBuilder ParametrosActualizacion(AlicuotaIVA valRecord, eAccionSR valAction) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInDateFormat("DateFormat");
            vParams.AddInDateTime("FechaDeInicioDeVigencia", valRecord.FechaDeInicioDeVigencia);
            vParams.AddInDecimal("MontoAlicuotaGeneral", valRecord.MontoAlicuotaGeneral, 2);
            vParams.AddInDecimal("MontoAlicuota2", valRecord.MontoAlicuota2, 2);
            vParams.AddInDecimal("MontoAlicuota3", valRecord.MontoAlicuota3, 2);
            if (valAction == eAccionSR.Modificar) {
                vParams.AddInTimestamp("TimeStampAsInt", valRecord.fldTimeStamp);
            }
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosClave(AlicuotaIVA valRecord, bool valIncludeTimestamp, bool valAddReturnParameter) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            if (valAddReturnParameter) {
                vParams.AddReturn();
            }
            vParams.AddInDateFormat("DateFormat");
            vParams.AddInDateTime("FechaDeInicioDeVigencia", valRecord.FechaDeInicioDeVigencia);
            if (valIncludeTimestamp) {
                vParams.AddInTimestamp("TimeStampAsInt", valRecord.fldTimeStamp);
            }
            vResult = vParams.Get();
            return vResult;
        }
        #region Miembros de ILibDataComponent<IList<AlicuotaIVA>, IList<AlicuotaIVA>>

        LibResponse ILibDataComponent<IList<AlicuotaIVA>, IList<AlicuotaIVA>>.CanBeChoosen(IList<AlicuotaIVA> refRecord, eAccionSR valAction) {
            LibResponse vResult = new LibResponse();
            AlicuotaIVA vRecord = refRecord[0];
            StringBuilder vSbInfo = new StringBuilder();
            string vErrMsg = "";
            LibDatabase insDB = new LibDatabase();
            if (valAction == eAccionSR.Eliminar) {
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

        [PrincipalPermission(SecurityAction.Demand, Role = "Alicuota IVA.Eliminar")]
        LibResponse ILibDataComponent<IList<AlicuotaIVA>, IList<AlicuotaIVA>>.Delete(IList<AlicuotaIVA> refRecord) {
            LibResponse vResult = new LibResponse();
            string vErrMsg = "";
            CurrentRecord = refRecord[0];
            if (Validate(eAccionSR.Eliminar, out vErrMsg)) {
                LibDatabase insDb = new LibDatabase();
                vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "AlicuotaIVADEL"), ParametrosClave(CurrentRecord, true, true));
                insDb.Dispose();
            } else {
                throw new GalacValidationException(vErrMsg);
            }
            return vResult;
        }

        IList<AlicuotaIVA> ILibDataComponent<IList<AlicuotaIVA>, IList<AlicuotaIVA>>.GetData(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters) {
            List<AlicuotaIVA> vResult = new List<AlicuotaIVA>();
            LibDatabase insDb = new LibDatabase();
            switch (valType) {
                case eProcessMessageType.SpName:
                    valProcessMessage = insDb.ToSpName(DbSchema, valProcessMessage);
                    vResult = insDb.LoadFromSp<AlicuotaIVA>(valProcessMessage, valParameters, CmdTimeOut);
                    break;
                default: throw new NotImplementedException();
            }
            insDb.Dispose();
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Alicuota IVA.Insertar")]
        LibResponse ILibDataComponent<IList<AlicuotaIVA>, IList<AlicuotaIVA>>.Insert(IList<AlicuotaIVA> refRecord) {
            LibResponse vResult = new LibResponse();
            string vErrMsg = "";
            CurrentRecord = refRecord[0];
            if (ExecuteProcessBeforeInsert()) {
                if (Validate(eAccionSR.Insertar, out vErrMsg)) {
                    LibDatabase insDb = new LibDatabase();
                    vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "AlicuotaIVAINS"), ParametrosActualizacion(CurrentRecord, eAccionSR.Insertar));
                    insDb.Dispose();
                } else {
                    throw new GalacValidationException(vErrMsg);
                }
            }
            return vResult;
        }

        XElement ILibDataComponent<IList<AlicuotaIVA>, IList<AlicuotaIVA>>.QueryInfo(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters) {
            XElement vResult = null;
            LibDatabase insDb = new LibDatabase();
            switch (valType) {
                case eProcessMessageType.SpName:
                    vResult = LibXml.ToXElement(insDb.LoadFromSp(valProcessMessage, valParameters, CmdTimeOut));
                    break;
                default: throw new NotImplementedException();
            }
            insDb.Dispose();
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Alicuota IVA.Modificar")]
        LibResponse ILibDataComponent<IList<AlicuotaIVA>, IList<AlicuotaIVA>>.Update(IList<AlicuotaIVA> refRecord) {
            LibResponse vResult = new LibResponse();
            string vErrMsg ="";
            CurrentRecord = refRecord[0];
            if (ExecuteProcessBeforeUpdate()) {
                if (Validate(eAccionSR.Modificar, out vErrMsg)) {
                    LibDatabase insDb = new LibDatabase();
                    vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "AlicuotaIVAUPD"), ParametrosActualizacion(CurrentRecord, eAccionSR.Modificar));
                    insDb.Dispose();
                } else {
                    throw new GalacValidationException(vErrMsg);
                }
            }
            return vResult;
        }

        bool ILibDataComponent<IList<AlicuotaIVA>, IList<AlicuotaIVA>>.ValidateAll(IList<AlicuotaIVA> refRecords, eAccionSR valAction, StringBuilder refErrorMessage) {
            bool vResult = true;
            string vErroMessage = "";
            foreach (AlicuotaIVA vItem in refRecords) {
                CurrentRecord = vItem;
                vResult = vResult && Validate(valAction, out vErroMessage);
                if (LibString.IsNullOrEmpty(vErroMessage, true)) {
                    refErrorMessage.AppendLine(vErroMessage);
                }
            }
            return vResult;
        }

        LibResponse ILibDataComponent<IList<AlicuotaIVA>, IList<AlicuotaIVA>>.SpecializedUpdate(IList<AlicuotaIVA> refRecord, string valSpecializedAction) {
            throw new NotImplementedException();
        }
        #endregion //ILibDataComponent<IList<AlicuotaIVA>, IList<AlicuotaIVA>>
        #region Validaciones
        protected override bool Validate(eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            ClearValidationInfo();
            vResult = IsValidFechaDeInicioDeVigencia(valAction, CurrentRecord.FechaDeInicioDeVigencia);
            outErrorMessage = Information.ToString();
            return vResult;
        }

        private bool IsValidFechaDeInicioDeVigencia(eAccionSR valAction, DateTime valFechaDeInicioDeVigencia){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (LibDefGen.DateIsGreaterThanDateLimitForEnterData(valFechaDeInicioDeVigencia, false, valAction)) {
                BuildValidationInfo(LibDefGen.MessageDateRestrictionDemoProgram());
                vResult = false;
            }
            return vResult;
        }

        private bool KeyExists(DateTime valFechaDeInicioDeVigencia) {
            bool vResult = false;
            AlicuotaIVA vRecordBusqueda = new AlicuotaIVA();
            vRecordBusqueda.FechaDeInicioDeVigencia = valFechaDeInicioDeVigencia;
            LibDatabase insDb = new LibDatabase();
            vResult = insDb.ExistsRecord("dbo.AlicuotaIVA", "FechaDeInicioDeVigencia", ParametrosClave(vRecordBusqueda, false, false));
            insDb.Dispose();
            return vResult;
        }

        private bool KeyExists(AlicuotaIVA valRecordBusqueda) {
            bool vResult = false;
            LibDatabase insDb = new LibDatabase();
            //Programador ajuste el codigo necesario para busqueda de claves unicas;
            vResult = insDb.ExistsRecord("dbo.AlicuotaIVA", "FechaDeInicioDeVigencia", ParametrosClave(valRecordBusqueda, false, false));
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


    } //End of class clsAlicuotaIVADat

} //End of namespace Galac.Dbo.Dal.CajaChica

