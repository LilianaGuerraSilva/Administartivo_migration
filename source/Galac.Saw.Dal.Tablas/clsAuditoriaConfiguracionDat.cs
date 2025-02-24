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
using System.Diagnostics;

namespace Galac.Saw.Dal.Tablas {
    public class clsAuditoriaConfiguracionDat: LibData,IAuditoriaConfiguracionDat  {
        #region Variables
        AuditoriaConfiguracion _CurrentRecord;
        #endregion //Variables
        #region Propiedades
        private AuditoriaConfiguracion CurrentRecord {
            get { return _CurrentRecord; }
            set { _CurrentRecord = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public clsAuditoriaConfiguracionDat() {
            DbSchema = "Adm";
        }
        #endregion //Constructores
        #region Metodos Generados

        private StringBuilder ParametrosActualizacion(AuditoriaConfiguracion valRecord, eAccionSR valAction) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInDateFormat("DateFormat");
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInInteger("ConsecutivoAuditoria", valRecord.ConsecutivoAuditoria);
            vParams.AddInString("VersionPrograma", LibDefGen.ProgramInfo.ProgramVersion, 10);
            //vParams.AddInDateTime("FechayHora", valRecord.FechayHora);
            vParams.AddInString("Accion", valRecord.Accion, 20);
            vParams.AddInString("Motivo", valRecord.Motivo, 255);
            vParams.AddInString("ConfiguracionOriginal", valRecord.ConfiguracionOriginal, 500);
            vParams.AddInString("ConfiguracionNueva", valRecord.ConfiguracionNueva, 500);
            vParams.AddInString("NombreOperador", ((CustomIdentity) Thread.CurrentPrincipal.Identity).Login, 10);
            //vParams.AddInDateTime("FechaUltimaModificacion", LibDate.Today());
            if (valAction == eAccionSR.Modificar) {
                vParams.AddInTimestamp("TimeStampAsInt", valRecord.fldTimeStamp);
            }
            vResult = vParams.Get();
            return vResult;
        }

        #endregion //Metodos Generados

        LibResponse IAuditoriaConfiguracionDat.Auditar(AuditoriaConfiguracion refRecord) {
            LibResponse vResult = new LibResponse();
            CurrentRecord = refRecord;
            LibDatabase insDb = new LibDatabase();
            CurrentRecord.ConsecutivoAuditoria = insDb.NextLngConsecutive("Adm.AuditoriaConfiguracion", "ConsecutivoAuditoria", "ConsecutivoCompania = " + new QAdvSql(string.Empty).ToSqlValue( CurrentRecord.ConsecutivoCompania));
            vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "AuditoriaConfiguracionAUD"), ParametrosActualizacion(CurrentRecord, eAccionSR.Insertar));
            insDb.Dispose();
            return vResult;
        }
    } //End of class clsAuditoriaConfiguracionDat

} //End of namespace Galac.Saw.Dal.Tablas

