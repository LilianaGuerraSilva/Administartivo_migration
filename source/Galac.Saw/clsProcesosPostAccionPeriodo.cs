using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.UI.Mvvm;
using LibGalac.Aos.Brl;
using LibGalac.Aos.Base;
using Galac.Contab.Ccl.WinCont;
using Galac.Contab.Brl.WinCont;

namespace Galac.Saw {
    internal class clsProcesosPostAccionPeriodo {
         #region Variables
        private static clsProcesosPostAccionPeriodo _Instance = new clsProcesosPostAccionPeriodo();
        private static ILibMainProcess _MainProcess;
        #endregion

        #region Constructor
        private clsProcesosPostAccionPeriodo() {
        }
        #endregion

        #region Métodos
        public static void RegisterMessages(ILibMainProcess valMainProcess) {
            _MainProcess = valMainProcess;
            LibBusinessProcess.Register(_Instance, "ProcesosPostInsertarPeriodo", EjecutarProcesosPostInsertarPeriodo);
            LibBusinessProcess.Register(_Instance, "ProcesosPostEliminarPeriodo", EjecutarProcesosPostEliminarPeriodo);
            LibBusinessProcess.Register(_Instance, "ProcesosPostModificarPeriodo", EjecutarProcesosPostModificarPeriodo);
            LibBusinessProcess.Register(_Instance, "RefrescarParametrosContables", EjecutarRefrescarParametrosContables);    
        }

        #region ProcesosPostInsertarPeriodo
        private static void EjecutarProcesosPostInsertarPeriodo(LibBusinessProcessMessage valMessage) {
            string vNombre = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Compania", "NombreCorto");
            _MainProcess.ChooseCompany(vNombre);
           
        }
        #endregion
        #region ProcesosPostEliminarPeriodo
        private static void EjecutarProcesosPostEliminarPeriodo(LibBusinessProcessMessage valMessage) {
            string vNombre = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Compania", "NombreCorto");
            _MainProcess.ChooseCompany(vNombre);

        }
        #endregion

        #region ProcesosPostModificarPeriodo
        private static void EjecutarProcesosPostModificarPeriodo(LibBusinessProcessMessage valMessage) {
            string vNombre = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Compania", "NombreCorto");
            _MainProcess.ChooseCompany(vNombre);
            LibBusinessProcess.Call("RefrescarParametrosContables");  
        }
        #endregion
        private static void EjecutarRefrescarParametrosContables(LibBusinessProcessMessage valMessage) {
            int vConsecutivoCompania = LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania");
            int vConsecutivoPeriodo = LibGlobalValues.Instance.GetMfcInfo().GetInt("Periodo");
            /*
            ((IPeriodoPdn)new clsPeriodoNav()).SetGlobalValues(vConsecutivoCompania, vConsecutivoPeriodo);

            IParametrosActivoFijoPdn insParametrosActivoFijoPdn = new clsParametrosActivoFijoNav();
            vResult = insParametrosActivoFijoPdn.ChooseCurrent();

            IParametrosConciliacionPdn insParamsConciliacion = new clsParametrosConciliacionNav();
            vResult = insParamsConciliacion.ChooseCurrent(vConsecutivoCompania);

            IParametrosGenPdn insParamsGenPdn = new clsParametrosGenNav();
            vResult = insParamsGenPdn.ChooseCurrent();
            */
            string vNombre = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Compania", "NombreCorto");
            _MainProcess.ChooseCompany(vNombre);
            _MainProcess.LoadGlobalValuesForComponents();
        }
       
        #endregion
    }
}
