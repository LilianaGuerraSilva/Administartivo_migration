using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base;

namespace Galac.Saw {
    public class clsBackupSettings : ILibBackupSettings {
        public LibBackupMfcController Controller { get; set; }

        public List<LibBackupMfcController> OthersControllers { get; set; }

        public string ControllerName { get; set; }

        public string ControllerShortName { get; set; }

        public string ControllerCode { get; set; }

        public string ProgramInitials { get; set; }

        public string ControllerSubFolderFieldName { get; set; }

        public void Initialize() {
            ProgramInitials = LibGalac.Aos.DefGen.LibDefGen.ProgramInfo.ProgramInitials;
            ControllerName = "";
            ControllerShortName = "";
            ControllerCode = "";
            if (LibGlobalValues.Instance != null && LibGlobalValues.Instance.GetAppMemInfo() != null && LibGlobalValues.Instance.GetAppMemInfo() != null 
                && LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetCount("Compania") > 0) {
                ControllerName = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Compania", "Nombre");
                ControllerShortName = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Compania", "NombreCorto");
                ControllerCode = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Compania", "Codigo");
            }
            LibGalac.Aos.Dal.LibDbo vDbo = new LibGalac.Aos.Dal.LibDbo();

            //Controlador Multiarchivo Principal
            ControllerSubFolderFieldName = "Codigo";
            Controller = new LibBackupMfcController();
            if (vDbo.Exists("Emp.Compania", LibGalac.Aos.Dal.eDboType.Tabla)) {
                Controller.TableName = "Emp.Compania";
            } else {
                Controller.TableName = "Compania";
            }
            Controller.FieldName = "ConsecutivoCompania";
            Controller.TypeFieldName = LibBackupMfcController.eTypeMfcControler.Numeric;
            Controller.CurrentValue = TryGetCurrentMfcValue("Compania");

            OthersControllers = new List<LibBackupMfcController>();
        }

        public void RefreshController(System.Data.DataRow valDataRow) {
            if (valDataRow != null) {
                ControllerName = "";
                ControllerShortName = "";
                ControllerCode = "";
                Controller.CurrentValue = 0;

                if (!LibConvert.IsNull(valDataRow["Nombre"])) {
                    ControllerName = valDataRow["Nombre"].ToString();
                }
                if (!LibConvert.IsNull(valDataRow["NombreCorto"])) {
                    ControllerShortName = valDataRow["NombreCorto"].ToString();
                }
                if (!LibConvert.IsNull(valDataRow["Codigo"])) {
                    ControllerCode = valDataRow["Codigo"].ToString();
                }
                if (!LibConvert.IsNull(valDataRow["ConsecutivoCompania"])) {
                    Controller.CurrentValue = LibConvert.ToInt(valDataRow["ConsecutivoCompania"].ToString());
                }
            }
        }

        private int TryGetCurrentMfcValue(string valRecordName) {
            int vResult = 0;
            try {
                vResult = LibGlobalValues.Instance.GetMfcInfo().GetInt(valRecordName);
            } catch (Exception) {
            }
            return vResult;
        }

    }//End class clsBackupSettings
}//End namespace Galac.Saw
