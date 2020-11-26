using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.UI.Mvvm.Messaging;
using LibGalac.Aos.Base;
using System.IO;
using LibGalac.Aos.UI.Mvvm.Helpers;
using System.Xml;
using System.Xml.Linq;
using Galac.Saw.Ccl.SttDef;
using Galac.Saw.Brl.SttDef;
using LibGalac.Aos.Catching;

namespace Galac.Saw.Uil.SttDef {
    class clsUtilParameters {
        private string ModuleName = "Parametros Administrativos";
        private string _FileNameWithoutExtension;
        public  string FileNameWithoutExtension {
            get {
                return _FileNameWithoutExtension;
            }
            set {
                if(_FileNameWithoutExtension != value) {
                    _FileNameWithoutExtension = value;
                }
            }
        }

  
        public string BuscarNombrePlantilla(string valFilter) {
            try {
                LibFileDialogMessage vMessage = new LibFileDialogMessage("Escoger Plantilla", SetFileNameWithoutExtension);
                vMessage.Filter = valFilter;
                vMessage.InitialDirectory = LibWorkPaths.OriginalReportDir;
                LibMessages.OpenFile.Send(vMessage);
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
            return (LibString.IsNullOrEmpty(FileNameWithoutExtension)?string.Empty:FileNameWithoutExtension);
        }

        private void SetFileNameWithoutExtension(FileInfo valFileInfo) {
            FileNameWithoutExtension = LibFile.FileNameWithoutExtension(valFileInfo.FullName);
        }

        public List<string> GetModelosPlanillasList() {
            List<string> vResult = new List<string>();
            List<string> vListModelosPlantillas = new List<string>();
            LibXmlDataParse insDataParse = new LibXmlDataParse(LibGlobalValues.Instance.GetAppMemInfo());
            string[] vModelosPlanillasArray;
            string vModelosPlanillas = insDataParse.GetString("Parametros", 0, "ModelosPlanillas", "");
            vModelosPlanillasArray = LibString.Split(vModelosPlanillas, ',');
            for(int i = 0; i < vModelosPlanillasArray.Length; i += 1) {
                vListModelosPlantillas.Add(vModelosPlanillasArray[i]);
            }
            vResult = vListModelosPlantillas;
            return vResult;
        }


        public static bool SePuedeRetenerParaEsteMunicipio(string vNombreCiudad, int vConsecutivoMunicipio) {
            return ((Ccl.SttDef.ISettValueByCompanyPdn)new clsSettValueByCompanyNav()).ExisteMunicipio(vConsecutivoMunicipio, vNombreCiudad);
        }

        public static bool PuedeActivarModulo(string vCodigoMunicipio) {
            return ((Ccl.SttDef.ISettValueByCompanyPdn)new clsSettValueByCompanyNav()).PuedeActivarModulo(vCodigoMunicipio);
        }

        internal static bool EsValidoNombrePlantilla(string valNameRpx) {
            bool vResult = false;

            string rutaReportes = System.IO.Path.Combine(LibWorkPaths.ProgramDir, "Reportes");
            string rutaReportesULS = System.IO.Path.Combine(LibWorkPaths.LogicUnitDir, "Reportes");
            string rutaOriginal = System.IO.Path.Combine(rutaReportes, "Original");
            string rutaUsuario = System.IO.Path.Combine(rutaReportes, "Usuario");
            string rutaOriginalULS = System.IO.Path.Combine(rutaReportesULS, "Original");
            string rutaUsuarioULS = System.IO.Path.Combine(rutaReportesULS, "Usuario");
           
            if (File.Exists(rutaOriginal + @"\" + valNameRpx + ".rpx")) {
                vResult = true;
            }
            if (File.Exists(rutaUsuario + @"\" + valNameRpx + ".rpx")) {
                vResult = true;
            }
            if(File.Exists(rutaOriginalULS + @"\" + valNameRpx + ".rpx")) {
               vResult = true;
            }
            if(File.Exists(rutaUsuarioULS + @"\" + valNameRpx + ".rpx")) {
               vResult = true;
            }
            return vResult;
        }

        internal static  bool EsSistemaParaIG() {
            bool vResult = false;
            try {
                vResult = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "EsSistemaParaIG");
            } catch (GalacException vEx) {
                if (LibString.S1IsInS2("no se encuentra en el conjunto", vEx.Message)) {
                    vResult = false;
                } else {
                    throw;
                }
            }
            return vResult;
        }
    }
}
