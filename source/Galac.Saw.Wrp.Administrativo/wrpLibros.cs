using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Catching;
using System.Runtime.InteropServices;
using LibGalac.Aos.Base;
using LibGalac.Aos.Vbwa;
using LibGalac.Aos.Dal;
using LibGalac.Aos.Brl;
using LibGalac.Aos.DefGen;
using LibGalac.Aos.UI.Wpf;
using Galac.Saw.Wrp.Administrativo;
#if IsExeBsF
namespace Galac.SawBsF.Wrp.Administrativo {
#elif IsExeBsS​
namespace Galac.SawBsS.Wrp.Administrativo {
#else
namespace Galac.Saw.Wrp.Administrativo {
#endif    
    [ClassInterface(ClassInterfaceType.None)]
    public class wrpLibros : System.EnterpriseServices.ServicedComponent, IWrpLibros {
        public string Title { get { return "Util - Libros"; } }

        #region Miembros de IWrpLibros
        void IWrpLibros.InitializeComponent(string vfwLogin, string vfwPassword, string vfwPath) {
            try {
                LibWrp.SetAppConfigToCurrentDomain(vfwPath);
                LibWrpHelper.ConfigureRuntimeContext(vfwLogin, vfwPassword);
            } catch (Exception vEx) {
                if (vEx is AccessViolationException) {
                    throw;
                }
                throw new GalacWrapperException(Title + " - Inicializar", vEx);
            }
        }

        void IWrpLibros.InitializeDefProg(string vfwProgramInitials, string vfwProgramVersion, string vfwDbVersion, string vfwStrDateOfVersion, string vfwStrHourOfVersion, string vfwValueSpecialCharacteristic, string vfwCountry, string vfwCMTO, bool vfwUsePASOnLine) {
            try {
                string vLogicUnitDir = LibGalac.Aos.Cnf.LibAppSettings.ULS;
                LibGalac.Aos.DefGen.LibDefGen.InitializeProgramInfo(vfwProgramInitials, vfwProgramVersion, vfwDbVersion, LibConvert.ToDate(vfwStrDateOfVersion), vfwStrHourOfVersion, "", vfwCountry, LibConvert.ToInt(vfwCMTO));
                LibGalac.Aos.DefGen.LibDefGen.InitializeWorkPaths("", vLogicUnitDir, LibApp.AppPath(), LibGalac.Aos.DefGen.LibDefGen.ProgramInfo.ProgramInitials);
                LibGalac.Aos.DefGen.LibDefGen.UsePASOnLine = vfwUsePASOnLine;
            } catch (Exception vEx) {
                if (vEx is AccessViolationException) {
                    throw;
                }
                throw new GalacWrapperException(Title + " - Inicializar", vEx);
            }
        }

        void IWrpLibros.InitializeContext(string vfwInfo) {
            try {
                LibGalac.Aos.DefGen.LibDefGen.Initialize(vfwInfo);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                throw new GalacWrapperException(Title + " - Inicializar", vEx);
            }
        }
        void IWrpLibros.CreaDataTemporalSiAplica(int vfwConsecutivoCompania, DateTime vfwFechaDesde, DateTime vfwFechaHasta) {
            string vBddContingencia = "SAWDB_LIBROS";
            if (ExisteMarcaManejoDataTemporal() && LaVersionDeLaBDDEsCorrecta(vBddContingencia, true)) {
                QAdvDb insDb = new QAdvDb();
                LibGpParams insParams = new LibGpParams();
                insParams.AddInInteger("ConsecutivoCompania", vfwConsecutivoCompania);
                insParams.AddInDateTime("FechaDesde", vfwFechaDesde);
                insParams.AddInDateTime("FechaHasta", vfwFechaHasta);
                StringBuilder vSbParams = insParams.Get();
                insDb.ExecSpNonQueryNonTransaction("dbo.Gp_TraspasarDataParaLibros", ref vSbParams, 120);
            }
        }
        #endregion //Miembros de IWrpLibros
        bool ExisteMarcaManejoDataTemporal() {
            bool vResult = false;
            vResult = new LibGalac.Aos.Dal.LibDbo().Exists("dbo.Gf_UsaDataTemporalParaLibros", eDboType.Funcion);
            return vResult;
        }

        bool LaVersionDeLaBDDEsCorrecta(string valNombreBdd, bool valArrojarExcepcion) {
            object vValor = null;
            try {
                vValor = LibBusiness.ExecuteScalar("SELECT COUNT(fldSiglasPrograma) AS Cantidad FROM " + valNombreBdd + ".dbo.Version WHERE fldSiglasPrograma = 'SAW' AND fldVersionBDD = '"
                 + LibDefGen.ProgramInfo.DataBaseVersion + "' AND fldVersionPrograma = '"
                 + LibDefGen.ProgramInfo.ProgramVersion + "'", string.Empty, -1);
            } catch (Exception) { }
            if (vValor != null && vValor.ToString() == "1") {
                return true;
            }
            if (valArrojarExcepcion) {
                throw new GalacWrapperException(Title, new GalacException("La plataforma no está configurada de manera correcta para la ejecución de Libros de compra y venta. Comuníquese con " + LibDefGen.GalacName(), eExceptionManagementType.Uncontrolled));
            }
            return false;
        }
    }
}
