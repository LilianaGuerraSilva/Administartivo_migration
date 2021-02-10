using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using LibGalac.Aos.Base;
using System.Text.RegularExpressions;
using System.Globalization;
using Galac.Adm.Ccl.DispositivosExternos;


namespace Galac.Adm.Brl.DispositivosExternos.ImpresoraFiscal {

    public static class LibImpresoraFiscalUtil {

        public static string CadenaCaracteresValidos(string valInputString) {
            System.Text.RegularExpressions.Regex replace_a_Accents = new System.Text.RegularExpressions.Regex("[á|à|ä|â]",System.Text.RegularExpressions.RegexOptions.Compiled);
            System.Text.RegularExpressions.Regex replace_e_Accents = new System.Text.RegularExpressions.Regex("[é|è|ë|ê]",System.Text.RegularExpressions.RegexOptions.Compiled);
            System.Text.RegularExpressions.Regex replace_i_Accents = new System.Text.RegularExpressions.Regex("[í|ì|ï|î]",System.Text.RegularExpressions.RegexOptions.Compiled);
            System.Text.RegularExpressions.Regex replace_o_Accents = new System.Text.RegularExpressions.Regex("[ó|ò|ö|ô]",System.Text.RegularExpressions.RegexOptions.Compiled);
            System.Text.RegularExpressions.Regex replace_u_Accents = new System.Text.RegularExpressions.Regex("[ú|ù|ü|û]",System.Text.RegularExpressions.RegexOptions.Compiled);
            System.Text.RegularExpressions.Regex replace_A_Accents = new System.Text.RegularExpressions.Regex("[Á|À|Ä|Â]",System.Text.RegularExpressions.RegexOptions.Compiled);
            System.Text.RegularExpressions.Regex replace_E_Accents = new System.Text.RegularExpressions.Regex("[É|È|Ë|Ê]",System.Text.RegularExpressions.RegexOptions.Compiled);
            System.Text.RegularExpressions.Regex replace_I_Accents = new System.Text.RegularExpressions.Regex("[Í|Ì|Ï|Î]",System.Text.RegularExpressions.RegexOptions.Compiled);
            System.Text.RegularExpressions.Regex replace_O_Accents = new System.Text.RegularExpressions.Regex("[Ó|Ò|Ö|Ô]",System.Text.RegularExpressions.RegexOptions.Compiled);
            System.Text.RegularExpressions.Regex replace_U_Accents = new System.Text.RegularExpressions.Regex("[Ú|Ù|Ü|Û]",System.Text.RegularExpressions.RegexOptions.Compiled);
            System.Text.RegularExpressions.Regex replace_NN_Accents = new System.Text.RegularExpressions.Regex("[Ñ]",System.Text.RegularExpressions.RegexOptions.Compiled);
            System.Text.RegularExpressions.Regex replace_nn_Accents = new System.Text.RegularExpressions.Regex("[ñ]",System.Text.RegularExpressions.RegexOptions.Compiled);
            valInputString = replace_a_Accents.Replace(valInputString,"a");
            valInputString = replace_e_Accents.Replace(valInputString,"e");
            valInputString = replace_i_Accents.Replace(valInputString,"i");
            valInputString = replace_o_Accents.Replace(valInputString,"o");
            valInputString = replace_u_Accents.Replace(valInputString,"u");
            valInputString = replace_A_Accents.Replace(valInputString,"A");
            valInputString = replace_E_Accents.Replace(valInputString,"E");
            valInputString = replace_I_Accents.Replace(valInputString,"I");
            valInputString = replace_O_Accents.Replace(valInputString,"O");
            valInputString = replace_U_Accents.Replace(valInputString,"U");
            valInputString = replace_NN_Accents.Replace(valInputString,"N");
            valInputString = replace_nn_Accents.Replace(valInputString,"n");
            return valInputString;
        }

        public static string RemoverCaracteresInvalidos(string valEntrada) {
            string vPatronEspacios = "\\s+";
            string vPatronCaracteresInvalidos = "[^a-zA-Z0-9 /s]+";
            string vReemplazoCaracteresInvalidos = "";
            string vReemplazoEspacios = " ";
            Regex rgx = new Regex(vPatronEspacios);
            string vResult = Regex.Replace(valEntrada,vPatronCaracteresInvalidos,vReemplazoCaracteresInvalidos,RegexOptions.Compiled);
            vResult = rgx.Replace(vResult,vReemplazoEspacios);
            return vResult;
        }

        public static string NormalizarString(string inputString) {
            string vNormalizedString = inputString.Normalize(NormalizationForm.FormD);
            StringBuilder vStringBuilder = new StringBuilder();
            for(int i = 0;i < vNormalizedString.Length;i++) {
                UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(vNormalizedString[i]);
                if(uc != UnicodeCategory.NonSpacingMark) {
                    vStringBuilder.Append(vNormalizedString[i]);
                }
            }
            return (vStringBuilder.ToString().Normalize(NormalizationForm.FormC));
        }

        public static string LimpiarXmlAntesDeParsear(string valXml) {
            //valXml = valXml.Replace("#","");            
            valXml = valXml.Replace("^","");
            //valXml = valXml.Replace("$","");
            return valXml;
        }

        public static string ObtenerValorNodoUnico(XElement valXmlDocumentoFiscal,string valNombreNodo) {
            string vResult = "";
            try {
                List<XElement> vNodos = valXmlDocumentoFiscal.Descendants().Where(e => e.Name.LocalName == valNombreNodo).ToList();
                if(vNodos.Count > 1) {
                    throw new LibGalac.Aos.Catching.GalacWrapperException("Existe 2 o mas nodos con el mismo nombre ",new Exception());
                } else if(vNodos.Count > 0) {
                    vResult = vNodos[0].Value;
                    vResult = NormalizarString(vResult);
                    vResult = RemoverCaracteresInvalidos(vResult);
                }
                return vResult;
            } catch(Exception vEx) {
                throw new LibGalac.Aos.Catching.GalacWrapperException("El formato del Xml Generado para el documento Fiscal no es valido, Nodo " + valNombreNodo,vEx);
            }
        }

        public static string DarFormatoNumericoParaImpresion(string valNumero,int valCantidadEnteros,int valCantidadDecimales,string WithDecimalSeparator = "") {
            string vValorFinal = "";
            decimal vDecimalValue = 0;
            valNumero = LibText.Trim(valNumero);
            valNumero = SetDecimalSeparator(valNumero);
            vDecimalValue = LibMath.RoundToNDecimals(LibImportData.ToDec(valNumero),valCantidadDecimales);
            vDecimalValue = LibMath.Abs(vDecimalValue);
            vDecimalValue = LibImportData.ToDec(((double)vDecimalValue * Math.Pow(10,valCantidadDecimales)).ToString(CultureInfo.InvariantCulture));
            vValorFinal = vDecimalValue.ToString(CultureInfo.InvariantCulture);
            if(LibString.Len(WithDecimalSeparator) > 0) {
                vValorFinal = LibString.InsertAt(vValorFinal,WithDecimalSeparator,LibString.Len(vValorFinal) - valCantidadDecimales);
                vValorFinal = (vDecimalValue == 0) ? "0" + WithDecimalSeparator + "00" : vValorFinal;
            } else {
                vValorFinal = LibText.FillWithCharToLeft(vValorFinal,"0",LibConvert.ToByte(valCantidadEnteros + valCantidadDecimales));
            }
            return vValorFinal;
        }

        private static string SetDecimalSeparator(string valNumero) {
            string vResult = "";
            string CurrentDecimalSep = "";
            CurrentDecimalSep = LibConvert.CurrentDecimalSeparator();
            if(LibText.S1IsInS2(".",valNumero) && CurrentDecimalSep.Equals(",")) {
                vResult = LibText.Replace(valNumero,".",",");
            } else if(LibText.S1IsInS2(",",valNumero) && CurrentDecimalSep.Equals(".")) {
                vResult = LibText.Replace(valNumero,",",".");
            } else {
                vResult = valNumero;
            }
            return vResult;
        }

        public static string DecimalToStringFormat(decimal valDecimalInput, int valDecimalRound) {
            string vResult = "";
            CultureInfo vCultureInfo = CultureInfo.InvariantCulture;
            valDecimalInput = LibMath.RoundToNDecimals(valDecimalInput,valDecimalRound);
            vResult = valDecimalInput.ToString(vCultureInfo);
            return vResult;
        }

        public static string RemoverCaracteresInvalidosParaNumeros(string valEntrada) {
            string vPatronEspacios = "\\s+";
            string vPatronCaracteresInvalidos = "[^0-9 /s /. ,]+";
            string vReemplazoCaracteresInvalidos = "";
            string vReemplazoEspacios = "";
            Regex rgx = new Regex(vPatronEspacios);
            string vResult = Regex.Replace(valEntrada,vPatronCaracteresInvalidos,vReemplazoCaracteresInvalidos,RegexOptions.Compiled);
            vResult = rgx.Replace(vResult,vReemplazoEspacios);
            return vResult;
        }

        public static string ObtenerValorNodoUnicoNumerico(XElement valXmlDocumentoFiscal,string valNombreNodo) {
            string vResult = "";
            try {
                List<XElement> vNodos = valXmlDocumentoFiscal.Descendants().Where(e => e.Name.LocalName == valNombreNodo).ToList();
                if(vNodos.Count > 1) {
                    throw new LibGalac.Aos.Catching.GalacWrapperException("Existe 2 o mas nodos con el mismo nombre ",new Exception());
                } else if(vNodos.Count > 0) {
                    vResult = vNodos[0].Value;
                    vResult = NormalizarString(vResult);
                    vResult = RemoverCaracteresInvalidosParaNumeros(vResult);
                }
                return vResult;
            } catch(Exception vEx) {
                throw vEx;
                throw new LibGalac.Aos.Catching.GalacWrapperException("El formato del Xml Generado para el documento Fiscal no es valido, Nodo " + valNombreNodo,vEx);
            }
        }

        public static double RedondearANDecimales(double valNumero,int valNumeroDecimales) {
            return Math.Round(valNumero,valNumeroDecimales);
        }

        public static string RedondearNumeroANDecimalesyDarFormatoSeparadorDeMiles(double valNumero,int valNumeroDecimales) {
            return string.Format("{0:n}",RedondearANDecimales(valNumero,valNumeroDecimales));
        }

        public static string ObtenerValorDesdeXML(XElement valXmlMaquinaFiscal,string valNombreNodo) {
            string vResult;
            vResult = LibXml.GetPropertyString(valXmlMaquinaFiscal,valNombreNodo);
            if(LibString.IsNullOrEmpty(vResult)) {
                vResult = LibXml.GetElementValueOrEmpty(valXmlMaquinaFiscal,valNombreNodo);
                return vResult;
            } else {
                return vResult;
            }
        }

        public static string GetFormaDePago(string valFormaDePago) {
            string vResult = "";
            try {
                Dictionary<string,string> FormasDePago = new Dictionary<string,string>{
                {eFormaDeCobroImprFiscal.Efectivo.GetDescription(1),eFormaDeCobroImprFiscal.Efectivo.GetDescription(0)},
                {eFormaDeCobroImprFiscal.Tarjeta.GetDescription(1),eFormaDeCobroImprFiscal.Tarjeta.GetDescription(0)},
                {eFormaDeCobroImprFiscal.Cheque.GetDescription(1),eFormaDeCobroImprFiscal.Cheque.GetDescription(0)},
                {eFormaDeCobroImprFiscal.Deposito.GetDescription(1),eFormaDeCobroImprFiscal.Deposito.GetDescription(0)},
                {eFormaDeCobroImprFiscal.Anticipo.GetDescription(1),eFormaDeCobroImprFiscal.Anticipo.GetDescription(0)},
                {eFormaDeCobroImprFiscal.Transferencia.GetDescription(1),eFormaDeCobroImprFiscal.Transferencia.GetDescription(0)}
            };
            vResult = FormasDePago[valFormaDePago];
            } catch {
                vResult = "Efectivo";
            }
            return vResult;
        }

        public static bool ObtenerVersionDeControlador(string valRuta,ref string refDllVersion) {
            bool vReq = false;
            vReq = LibFile.FileExists(valRuta);
            if(vReq) {
                System.Diagnostics.FileVersionInfo vFileFileInfo = System.Diagnostics.FileVersionInfo.GetVersionInfo(valRuta);
                refDllVersion = vFileFileInfo.FileVersion;
            }
            return vReq;
        }

        public static string EstatusDeComunicacionDescription(bool valStatus) {
            return valStatus ? "Correcto, en espera" : "No se pudo establecer comunicación, verifícar puertos y cableados";
        }

        public static string EstatusVersionDeControladorDescription(bool valStatus,bool valControllerIsSame,string valPath, string valOldVersion, string valNewVersion) {
            if(valStatus) {
                return valControllerIsSame ? "Controladores Actualizados" : $"Controladores No  Actualizados. Se encontró {valOldVersion}, se esperaba {valNewVersion} \r\nactualice el controlador en la ruta de instalación " + valPath;

            } else {
                return "Controlador no encontrado, actualice el controaldor en la ruta de instalación";
            }

        }

        public static string EstatusHorayFechaDescription(bool valStatus) {
            return valStatus ? "La fecha esta actualizada" : "La fecha esta desactualizada o no corresponde con la del computador, consulte con su proveedor";
        }

        public static string EstatusColadeImpresionDescription(bool valStatus) {
            return valStatus ? "Listo, En espera" : "Documento en cola\r\noperación fiscal sin completar, debe cancelar el documento fiscal en la opción disponible en el menú";
        }


        public static bool ValidarAlicuotasRegistradas(decimal valAlicuota1,decimal valAlicuota2,decimal valAlicuota3,ref string refAlicoutasRegistradasDescription) {
            bool vResult = false;
            string vMensaje = "";
            LibImpresoraFiscalUtil.AlicuotaIva.LoadAlicuotas();
            vResult = (valAlicuota1 == LibImpresoraFiscalUtil.AlicuotaIva.Alicuota1);
            vResult &= (valAlicuota2 == LibImpresoraFiscalUtil.AlicuotaIva.Alicuota2);
            vResult &= (valAlicuota3 == LibImpresoraFiscalUtil.AlicuotaIva.Alicuota3);
            if(vResult) {
                refAlicoutasRegistradasDescription = "Las Alicuotas estan actualizadas";
            } else {
                vMensaje = "Las Alicuotas estan desactualizadas\r\n";
                vMensaje += (valAlicuota1 == LibImpresoraFiscalUtil.AlicuotaIva.Alicuota1) ? "\r\n" : "Se encontró " + LibConvert.ToStr(valAlicuota1) + " Se esperaba " + LibConvert.ToStr(LibImpresoraFiscalUtil.AlicuotaIva.Alicuota1) + "\r\n";
                vMensaje += (valAlicuota2 == LibImpresoraFiscalUtil.AlicuotaIva.Alicuota2) ? "\r\n" : "Se encontró " + LibConvert.ToStr(valAlicuota2) + " Se esperaba " + LibConvert.ToStr(LibImpresoraFiscalUtil.AlicuotaIva.Alicuota2) + "\r\n";
                vMensaje += (valAlicuota3 == LibImpresoraFiscalUtil.AlicuotaIva.Alicuota3) ? "\r\n" : "Se encontró " + LibConvert.ToStr(valAlicuota3) + " Se esperaba " + LibConvert.ToStr(LibImpresoraFiscalUtil.AlicuotaIva.Alicuota3) + "\r\n";
                refAlicoutasRegistradasDescription = vMensaje;
            }
            return vResult;
        }

        public static class AlicuotaIva {
            public static decimal Alicuota1 { get; set; }
            public static decimal Alicuota2 { get; set; }
            public static decimal Alicuota3 { get; set; }
            public static void LoadAlicuotas() {
                Alicuota1 = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetDecimal("Parametros","PorcentajeAlicuota1");
                Alicuota2 = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetDecimal("Parametros","PorcentajeAlicuota2");
                Alicuota3 = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetDecimal("Parametros","PorcentajeAlicuota3");
            }
        }

    }
}
