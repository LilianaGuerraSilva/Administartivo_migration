using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base;
using DataDynamics.ActiveReports;
using LibGalac.Aos.ARRpt;
using LibGalac.Aos.DefGen;

namespace Galac.Saw.Lib {
    public class clsUtilRpt {
        
        public clsUtilRpt() {
          
        }

        public bool EsFormatoRpxValidoParaAOS(string valNameRpx) {
            bool vResult = false;
            if (LibText.IsNullOrEmpty(valNameRpx)) {
                return vResult;
            } else {
                string RutaRpx = ObtenerRutaPlantilla(valNameRpx);
                if (!LibText.IsNullOrEmpty(RutaRpx)) {
                    System.IO.StreamReader vFile = System.IO.File.OpenText(RutaRpx);
                    vResult = vFile.ReadToEnd().Contains("ScriptLang=\"C#\"");
                }
            }
            return vResult;
        }

        private string ObtenerRutaPlantilla(string valNameRpx) {
            string vResult = string.Empty;
            string rutaReportes = System.IO.Path.Combine(LibWorkPaths.ProgramDir,"Reportes");
            string rutaReportesULS = System.IO.Path.Combine(LibWorkPaths.LogicUnitDir,"Reportes");
            string rutaOriginal = System.IO.Path.Combine(rutaReportes,"Original");
            string rutaUsuario = System.IO.Path.Combine(rutaReportes,"Usuario");
            string rutaOriginalULS = System.IO.Path.Combine(rutaReportesULS,"Original");
            string rutaUsuarioULS = System.IO.Path.Combine(rutaReportesULS,"Usuario");
            if(LibFile.FileExists(rutaOriginal + @"\" + valNameRpx + ".rpx")) {
                vResult = rutaOriginal + @"\" + valNameRpx + ".rpx";
            }
            if(LibFile.FileExists(rutaUsuario + @"\" + valNameRpx + ".rpx")) {
                vResult = rutaUsuario + @"\" + valNameRpx + ".rpx";
            }
            if(LibFile.FileExists(rutaOriginalULS + @"\" + valNameRpx + ".rpx")) {
                vResult = rutaOriginalULS + @"\" + valNameRpx + ".rpx";
            }
            if(LibFile.FileExists(rutaUsuarioULS + @"\" + valNameRpx + ".rpx")) {
                vResult = rutaUsuarioULS + @"\" + valNameRpx + ".rpx";
            }
            return vResult;
        }

        public bool IsNullOrEmpty(string valString,bool valDoTrim) {
            return LibText.IsNullOrEmpty(valString,valDoTrim);
        }

        public bool IsNullOrEmpty(string valString) {
            return LibText.IsNullOrEmpty(valString);
        }

        public void ChangeControlVisibility(ref ActiveReport refRpt,string valObjectName,bool valVisible) {
            LibReport.ChangeControlVisibility(refRpt,valObjectName,valVisible);
        }

        public string NumericFormat(decimal valValue) {
            return LibConvert.ToStr(valValue);
        }

        public decimal ToDec(string valValue,int valDecimalDigits) {
            return LibConvert.ToDec(valValue,valDecimalDigits);
        }

        public decimal ToDec(string valValue) {
            return LibConvert.ToDec(valValue);
        }

        public int ToInt(string valValue) {
            return LibConvert.ToInt(valValue);
        }

        public string ToNumberInLetters(decimal valValue,bool valReturnWithCoinName,string valCoinName) {
            return LibConvert.ToNumberInLetters(valValue,valReturnWithCoinName,valCoinName);
        }

        public string ToStr(decimal valValue) {
            return LibConvert.ToStr(valValue);
        }

        public string ToStr(int valValue) {
            return LibConvert.ToStr(valValue);
        }

        public decimal RoundToNDecimals(decimal valValue,int valDecimalDigits) {
            return LibMath.RoundToNDecimals(valValue,valDecimalDigits);
        }

        public bool IsCountryPeru() {
            return LibDefGen.ProgramInfo.IsCountryPeru();
        }

        public bool IsCountryVenezuela() {
            return LibDefGen.ProgramInfo.IsCountryVenezuela();
        }

        public string SubString(string valText,int valStartIndex) {
            return LibString.SubString(valText,valStartIndex);
        }

        public string SubString(string valText,int valStartIndex,int valLength) {
            return LibString.SubString(valText,valStartIndex,valLength);
        }

        public string MensajesDeMonedaParaInformes(eTasaDeCambioParaImpresion valTasaDeCambio) {
            string vResult = "";
            if((LibDefGen.ProgramInfo.IsCountryVenezuela() || LibDefGen.ProgramInfo.IsCountryPeru()) && valTasaDeCambio == eTasaDeCambioParaImpresion.Original) {
                vResult = "NOTA: La tasa de cambio es la tasa original del día que se efectuó la operación.";
            } else {
                vResult = "NOTA: La tasa de cambio es la tasa registrada más cercana al dia que se imprimió el informe";
            }
            return vResult;
        }

        public eMonedaParaImpresion MonedaLocalDeLosReportes(eMonedaParaImpresion vMonedaParaImpresionIn) {
            //Galac.Comun.Ccl.TablasGen.IMonedaLocalActual insMonedaLocal = new Galac.Comun.Brl.TablasGen.clsMonedaLocalActual();
            //insMonedaLocal.CargarTodasEnMemoriaYAsignarValoresDeLaActual(LibDefGen.ProgramInfo.Country,LibDate.Today());            
            string vMonedaLocal = new clsNoComunSaw().InstanceMonedaLocalActual.NombreMoneda(LibDate.Today());
            eMonedaParaImpresion vResult = LibString.S1IsInS2(vMonedaLocal,vMonedaParaImpresionIn.GetDescription()) ? vMonedaParaImpresionIn : eMonedaParaImpresion.EnMonedaOriginal;
            return vResult;
        }
    }//End Of ClsUtilRpt
}//End of Namespace Galac.Saw.Lib
