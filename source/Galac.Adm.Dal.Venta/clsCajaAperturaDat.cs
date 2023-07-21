using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Galac.Adm.Dal.Venta {
    public class clsCajaAperturaDat:LibData, ILibDataComponentWithSearch<IList<CajaApertura>,IList<CajaApertura>> {
        #region Variables
        CajaApertura _CurrentRecord;
        #endregion //Variables
        #region Propiedades
        private CajaApertura CurrentRecord {
            get {
                return _CurrentRecord;
            }
            set {
                _CurrentRecord = value;
            }
        }
        #endregion //Propiedades
        #region Constructores

        public clsCajaAperturaDat() {
            DbSchema = "Adm";
        }
        #endregion //Constructores
        #region Metodos Generados

        private StringBuilder ParametrosActualizacion(CajaApertura valRecord,eAccionSR valAction) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();            
            vParams.AddInInteger("ConsecutivoCompania",valRecord.ConsecutivoCompania);
            vParams.AddInInteger("Consecutivo",valRecord.Consecutivo);
            vParams.AddInInteger("ConsecutivoCaja",valRecord.ConsecutivoCaja);
            vParams.AddInString("NombreDelUsuario",valRecord.NombreDelUsuario,20);
            vParams.AddInDecimal("MontoApertura",valRecord.MontoApertura,4);
            vParams.AddInDecimal("MontoCierre",valRecord.MontoCierre,4);
            vParams.AddInDecimal("MontoEfectivo",valRecord.MontoEfectivo,4);
            vParams.AddInDecimal("MontoTarjeta",valRecord.MontoTarjeta,4);
            vParams.AddInDecimal("MontoCheque",valRecord.MontoCheque,4);
            vParams.AddInDecimal("MontoDeposito", valRecord.MontoDeposito, 4);
            vParams.AddInDecimal("MontoAnticipo", valRecord.MontoAnticipo, 4);
            vParams.AddInDateTime("Fecha", valRecord.Fecha);
            vParams.AddInString("HoraApertura", valRecord.HoraApertura, 5);
            vParams.AddInString("HoraCierre", valRecord.HoraCierre, 5);
            vParams.AddInBoolean("CajaCerrada", valRecord.CajaCerradaAsBool);
            vParams.AddInString("CodigoMoneda", valRecord.CodigoMoneda, 4);
            vParams.AddInDecimal("Cambio", valRecord.Cambio, 4);
            vParams.AddInDecimal("MontoAperturaME", valRecord.MontoAperturaME, 4);
            vParams.AddInDecimal("MontoCierreME", valRecord.MontoCierreME, 4);
            vParams.AddInDecimal("MontoEfectivoME", valRecord.MontoEfectivoME, 4);
            vParams.AddInDecimal("MontoTarjetaME", valRecord.MontoTarjetaME, 4);
            vParams.AddInDecimal("MontoChequeME", valRecord.MontoChequeME, 4);
            vParams.AddInDecimal("MontoDepositoME", valRecord.MontoDepositoME, 4);
            vParams.AddInDecimal("MontoAnticipoME", valRecord.MontoAnticipoME, 4);
            vParams.AddInString("NombreOperador",((CustomIdentity)Thread.CurrentPrincipal.Identity).Login,10);
            vParams.AddInDateTime("FechaUltimaModificacion",LibDate.Today());
            if(valAction == eAccionSR.Modificar) {
                vParams.AddInTimestamp("TimeStampAsInt",valRecord.fldTimeStamp);
            }
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosClave(CajaApertura valRecord,bool valIncludeTimestamp,bool valAddReturnParameter) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            if(valAddReturnParameter) {
                vParams.AddReturn();
            }
            vParams.AddInInteger("ConsecutivoCompania",valRecord.ConsecutivoCompania);
            vParams.AddInInteger("Consecutivo",valRecord.Consecutivo);
            vParams.AddInInteger("ConsecutivoCaja",valRecord.ConsecutivoCaja);
            if(valIncludeTimestamp) {
                vParams.AddInTimestamp("TimeStampAsInt",valRecord.fldTimeStamp);
            }
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosProximoConsecutivo(CajaApertura valRecord) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania",valRecord.ConsecutivoCompania);            
            vParams.AddInInteger("ConsecutivoCaja",valRecord.ConsecutivoCaja);
            vResult = vParams.Get();
            return vResult;
        }
        #region Miembros de ILibDataComponent<IList<CajaApertura>, IList<CajaApertura>>

        LibResponse ILibDataComponent<IList<CajaApertura>,IList<CajaApertura>>.CanBeChoosen(IList<CajaApertura> refRecord,eAccionSR valAction) {
            LibResponse vResult = new LibResponse();
            CajaApertura vRecord = refRecord[0];
            StringBuilder vSbInfo = new StringBuilder();
            string vErrMsg = "";
            LibDatabase insDB = new LibDatabase();
            if(valAction == eAccionSR.Eliminar) {
                if(vSbInfo.Length == 0) {
                    vResult.Success = true;
                }
            } else {
                vResult.Success = true;
            }
            insDB.Dispose();
            if(!vResult.Success) {
                vErrMsg = LibResMsg.InfoForeignKeyCanNotBeDeleted(vSbInfo.ToString());
                throw new GalacAlertException(vErrMsg);
            } else {
                return vResult;
            }
        }

        [PrincipalPermission(SecurityAction.Demand,Role = "Caja Registradora.Eliminar")]
        LibResponse ILibDataComponent<IList<CajaApertura>,IList<CajaApertura>>.Delete(IList<CajaApertura> refRecord) {
            LibResponse vResult = new LibResponse();
            string vErrMsg = "";
            CurrentRecord = refRecord[0];
            if(Validate(eAccionSR.Eliminar,out vErrMsg)) {
                LibDatabase insDb = new LibDatabase();
                vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema,"CajaAperturaDEL"),ParametrosClave(CurrentRecord,true,true));
                insDb.Dispose();
            } else {
                throw new GalacValidationException(vErrMsg);
            }
            return vResult;
        }

        IList<CajaApertura> ILibDataComponent<IList<CajaApertura>,IList<CajaApertura>>.GetData(eProcessMessageType valType,string valProcessMessage,StringBuilder valParameters) {
            List<CajaApertura> vResult = new List<CajaApertura>();
            LibDatabase insDb = new LibDatabase();
            switch(valType) {
                case eProcessMessageType.SpName:
                    valProcessMessage = insDb.ToSpName(DbSchema,valProcessMessage);
                    vResult = insDb.LoadFromSp<CajaApertura>(valProcessMessage,valParameters,CmdTimeOut);
                    break;
                case eProcessMessageType.Query:
                    vResult = insDb.LoadData<CajaApertura>(valProcessMessage,CmdTimeOut,valParameters);
                    break;
                default:
                    throw new ProgrammerMissingCodeException();
            }
            insDb.Dispose();
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand,Role = "Caja Registradora.Insertar")]
        LibResponse ILibDataComponent<IList<CajaApertura>,IList<CajaApertura>>.Insert(IList<CajaApertura> refRecord) {
            LibResponse vResult = new LibResponse();
            string vErrMsg = "";
                CurrentRecord = refRecord[0];
                if(ExecuteProcessBeforeInsert()) {
                    if(Validate(eAccionSR.Insertar,out vErrMsg)) {
                        LibDatabase insDb = new LibDatabase();
                        CurrentRecord.Consecutivo = insDb.NextLngConsecutive(DbSchema + ".CajaApertura","Consecutivo",ParametrosProximoConsecutivo(CurrentRecord));						
                        vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema,"CajaAperturaINS"),ParametrosActualizacion(CurrentRecord,eAccionSR.Insertar));
                        insDb.Dispose();
                    } else {
                        throw new GalacValidationException(vErrMsg);
                }
            }
            return vResult;
        }

        XElement ILibDataComponent<IList<CajaApertura>,IList<CajaApertura>>.QueryInfo(eProcessMessageType valType,string valProcessMessage,StringBuilder valParameters) {
            XElement vResult = null;
            LibDatabase insDb = new LibDatabase();
            switch(valType) {
                case eProcessMessageType.SpName:
                    vResult = LibXml.ToXElement(insDb.LoadFromSp(valProcessMessage,valParameters,CmdTimeOut));
                    break;
                case eProcessMessageType.Query:
                    vResult = LibXml.ToXElement(insDb.LoadData(valProcessMessage,CmdTimeOut,valParameters));
                    break;
                default:
                    throw new ProgrammerMissingCodeException();
            }
            insDb.Dispose();
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand,Role = "Caja Registradora.Modificar")]
        LibResponse ILibDataComponent<IList<CajaApertura>,IList<CajaApertura>>.Update(IList<CajaApertura> refRecord) {
            LibResponse vResult = new LibResponse();
            string vErrMsg = "";
            CurrentRecord = refRecord[0];
            if(ExecuteProcessBeforeUpdate()) {
                if(Validate(eAccionSR.Modificar,out vErrMsg)) {
                    LibDatabase insDb = new LibDatabase();
                    vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema,"CajaAperturaUPD"),ParametrosActualizacion(CurrentRecord,eAccionSR.Modificar));
                    insDb.Dispose();
                } else {
                    throw new GalacValidationException(vErrMsg);
                }
            }
            return vResult;
        }

        bool ILibDataComponent<IList<CajaApertura>,IList<CajaApertura>>.ValidateAll(IList<CajaApertura> refRecords,eAccionSR valAction,StringBuilder refErrorMessage) {
            bool vResult = true;
            string vErroMessage = "";
            foreach(CajaApertura vItem in refRecords) {
                CurrentRecord = vItem;
                vResult = vResult && Validate(valAction,out vErroMessage);
                if(LibString.IsNullOrEmpty(vErroMessage,true)) {
                    refErrorMessage.AppendLine(vErroMessage);
                }
            }
            return vResult;
        }

        LibResponse ILibDataComponent<IList<CajaApertura>,IList<CajaApertura>>.SpecializedUpdate(IList<CajaApertura> refRecord,string valSpecializedAction) {
            throw new ProgrammerMissingCodeException();
        }
        #endregion //ILibDataComponent<IList<CajaApertura>, IList<CajaApertura>>
        #region Validaciones
        protected override bool Validate(eAccionSR valAction,out string outErrorMessage) {
            bool vResult = true;
            ClearValidationInfo();
            vResult = IsValidConsecutivoCompania(valAction,CurrentRecord.ConsecutivoCompania,CurrentRecord.Consecutivo,CurrentRecord.ConsecutivoCaja);
            vResult = IsValidConsecutivo(valAction,CurrentRecord.ConsecutivoCompania,CurrentRecord.Consecutivo,CurrentRecord.ConsecutivoCaja) && vResult;
            //vResult = IsValidConsecutivoCaja(valAction,CurrentRecord.ConsecutivoCompania,CurrentRecord.Consecutivo,CurrentRecord.ConsecutivoCaja) && vResult;
            vResult = IsValidNombreDelUsuario(valAction,CurrentRecord.NombreDelUsuario) && vResult;
            vResult = IsValidFecha(valAction,CurrentRecord.Fecha) && vResult;
            vResult = IsValidCodigoMoneda(valAction, CurrentRecord.CodigoMoneda) && vResult;
            outErrorMessage = Information.ToString();
            return vResult;
        }

        private bool IsValidConsecutivoCompania(eAccionSR valAction,int valConsecutivoCompania,int valConsecutivo,int valConsecutivoCaja) {
            bool vResult = true;
            if((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if(valConsecutivoCompania <= 0) {
                BuildValidationInfo(MsgRequiredField("Consecutivo Compania"));
                vResult = false;
            }
            return vResult;
        }

        private bool IsValidConsecutivo(eAccionSR valAction,int valConsecutivoCompania,int valConsecutivo,int valConsecutivoCaja) {
            bool vResult = true;
            if((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar) || (valAction == eAccionSR.Insertar)) {
                return true;
            }
            if(valConsecutivo == 0) {
                BuildValidationInfo(MsgRequiredField("Consecutivo"));
                vResult = false;
            } else if(valAction == eAccionSR.Insertar) {
                CajaApertura vRecBusqueda = new CajaApertura();
                vRecBusqueda.Consecutivo = valConsecutivo;
                if(KeyExists(valConsecutivoCompania,valConsecutivo,valConsecutivoCaja)) {
                    BuildValidationInfo(MsgFieldValueAlreadyExist("Consecutivo",valConsecutivo));
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidConsecutivoCaja(eAccionSR valAction,int valConsecutivoCompania,int valConsecutivo,int valConsecutivoCaja) {
            bool vResult = true;
            if((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if(valConsecutivoCaja < 0) {
                BuildValidationInfo(MsgRequiredField("Consecutivo Caja"));
                vResult = false;
            } else if(valAction == eAccionSR.Insertar) {
                if(KeyExists(valConsecutivoCompania,valConsecutivo,valConsecutivoCaja)) {
                    BuildValidationInfo(MsgFieldValueAlreadyExist("Consecutivo Caja",valConsecutivoCaja));
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidNombreDelUsuario(eAccionSR valAction,string valNombreDelUsuario) {
            bool vResult = true;
            if((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valNombreDelUsuario = LibString.Trim(valNombreDelUsuario);
            if(LibString.IsNullOrEmpty(valNombreDelUsuario,true)) {
                BuildValidationInfo(MsgRequiredField("Operador"));
                vResult = false;
            }
            return vResult;
        }

        private bool IsValidFecha(eAccionSR valAction, DateTime valFecha) {
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

        private bool IsValidCodigoMoneda(eAccionSR valAction, string valCodigoMoneda){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCodigoMoneda = LibString.Trim(valCodigoMoneda);
            if (LibString.IsNullOrEmpty(valCodigoMoneda , true)) {
                BuildValidationInfo(MsgRequiredField("Codigo Moneda"));
                vResult = false;
            } else {
                LibDatabase insDb = new LibDatabase();
                if (!insDb.ExistsValue("Comun.Moneda", "Codigo", insDb.InsSql.ToSqlValue(valCodigoMoneda), true)) {
                    BuildValidationInfo("El valor asignado al campo Codigo Moneda no existe, escoga nuevamente.");
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool KeyExists(int valConsecutivoCompania,int valConsecutivo,int valConsecutivoCaja) {
            bool vResult = false;
            CajaApertura vRecordBusqueda = new CajaApertura();
            vRecordBusqueda.ConsecutivoCompania = valConsecutivoCompania;
            vRecordBusqueda.Consecutivo = valConsecutivo;
            vRecordBusqueda.ConsecutivoCaja = valConsecutivoCaja;
            LibDatabase insDb = new LibDatabase();
            vResult = insDb.ExistsRecord(DbSchema + ".CajaApertura","ConsecutivoCompania",ParametrosClave(vRecordBusqueda,false,false));
            insDb.Dispose();
            return vResult;
        }

        private bool KeyExists(int valConsecutivoCompania,CajaApertura valRecordBusqueda) {
            bool vResult = false;
            valRecordBusqueda.ConsecutivoCompania = valConsecutivoCompania;
            LibDatabase insDb = new LibDatabase();
            //Programador ajuste el codigo necesario para busqueda de claves unicas;
            vResult = insDb.ExistsRecord(DbSchema + ".CajaApertura","ConsecutivoCompania",ParametrosClave(valRecordBusqueda,false,false));
            insDb.Dispose();
            return vResult;
        }
        #endregion //Validaciones
        #region Miembros de ILibDataFKSearch
        bool ILibDataFKSearch.ConnectFk(ref XmlDocument refResulset,eProcessMessageType valType,string valProcessMessage,StringBuilder valXmlParamsExpression) {
            bool vResult = false;
            LibDatabase insDb = new LibDatabase();
            refResulset = insDb.LoadForConnect(valProcessMessage,valXmlParamsExpression,CmdTimeOut);
            if(refResulset != null && refResulset.DocumentElement != null && refResulset.DocumentElement.HasChildNodes) {
                vResult = true;
            }
            return vResult;
        }

        public bool ListDataCajaApertura(ref XmlDocument refResulset,eProcessMessageType valType,string valProcessMessage,StringBuilder valXmlParamsExpression) {
            bool vResult = false;
            LibDatabase insDb = new LibDatabase();
            refResulset = insDb.LoadFromSp(valProcessMessage,valXmlParamsExpression,CmdTimeOut);
            if(refResulset != null && refResulset.DocumentElement != null && refResulset.DocumentElement.HasChildNodes) {
                vResult = true;
            }         
            return vResult;
        }

        public bool CajasCerradas(StringBuilder valXmlParamsExpression) {
            XmlDocument vResulset;           
            bool vResult = false;
            LibDatabase insDb = new LibDatabase();
            vResulset = insDb.LoadFromSp("Adm.Gp_CajasCerradasGet",valXmlParamsExpression,0);
            if(vResulset != null && vResulset.DocumentElement != null && vResulset.DocumentElement.HasChildNodes) {
                XElement vXmlResult = LibXml.ToXElement(vResulset);                
                vResult = LibConvert.SNToBool(LibXml.GetPropertyString(vXmlResult,"ReqCajasCerradas"));              
            }
            return vResult;
        }

        public bool UsuarioAsignado(StringBuilder valXmlParamsExpression) {
            XmlDocument vResulset;
            bool vResult = false;
            LibDatabase insDb = new LibDatabase();
            vResulset = insDb.LoadFromSp(insDb.ToSpName(DbSchema,"CajaUsuarioAsignadoGet"),valXmlParamsExpression,0);
            if(vResulset != null && vResulset.DocumentElement != null && vResulset.DocumentElement.HasChildNodes) {
                XElement vXmlResult = LibXml.ToXElement(vResulset); 
                vResult = LibConvert.SNToBool(LibXml.GetPropertyString(vXmlResult,"UsuarioYaAsignado"));
            }
            return vResult;
        }

        public int ConsecutivoApertura(StringBuilder valXmlParamsExpression) {
            XmlDocument vResulset;           
            int vResult =0;
            LibDatabase insDb = new LibDatabase();
            vResulset = insDb.LoadFromSp("Adm.Gp_CajaAperturaConsecutivoGet",valXmlParamsExpression,0);
            if(vResulset != null && vResulset.DocumentElement != null && vResulset.DocumentElement.HasChildNodes) {
                XElement vXmlResult = LibXml.ToXElement(vResulset);
                vResult = LibConvert.ToInt(LibXml.GetPropertyString(vXmlResult,"NextConsecutivo"));                
            }
            return vResult;
        }

        public bool CerrarCaja(StringBuilder valXmlParamsExpression) {
            bool vResult = false;
            try {
                LibDatabase insDb = new LibDatabase();
                XmlDocument vResulset = insDb.LoadFromSp(insDb.ToSpName(DbSchema,"CajaAperturaCerrar"),valXmlParamsExpression,0);
                if(vResulset != null && vResulset.DocumentElement != null && vResulset.DocumentElement.HasChildNodes) {
                    XElement vXmlResult = LibXml.ToXElement(vResulset);
                    vResult = LibConvert.ToInt(LibXml.GetPropertyString(vXmlResult,"RowsAfects")) > 0;
                }
                return vResult;
            } catch(Exception vEx) {
                throw vEx;
            }
        }               
        #endregion //Miembros de ILibDataFKSearch
        #endregion //Metodos Generados
    } //End of class clsCajaAperturaDat

} //End of namespace Galac.Adm.Dal.Venta

