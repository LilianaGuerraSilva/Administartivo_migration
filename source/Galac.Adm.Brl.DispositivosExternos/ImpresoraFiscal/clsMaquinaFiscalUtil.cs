using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using System.Globalization;


namespace Galac.Adm.Brl.DispositivosExternos.MaquinaFiscal {
    #region Factura Prueba
    /* Esta clase esta diseñada para trabajar con un formato de documento fiscal como el siguiente
            @"<DOCUMENTO_FISCAL>
              <DATOS_DOCUMENTO>
                <TIPO>Factura</TIPO>
                <NUMERO_FACTURA_AFECTADA></NUMERO_FACTURA_AFECTADA>
                <SERIAL_MAQUINA></SERIAL_MAQUINA>
                <FECHA></FECHA>
                <HORA></HORA>
              </DATOS_DOCUMENTO>
              <DATOS_CLIENTE>
                <RAZON_SOCIAL>Salvatore [é|è|ë|ê] [í|ì|ï|î] [ó|ò|ö|ô] [ú|ù|ü|û] ?&^$#@!()+-,:;<>’\'-_* [á|à|ä|â] Gonzalez</RAZON_SOCIAL>
                <RIF>J183729456</RIF>
                <DIRECCION>La direccion donde vivo</DIRECCION>
                <OBSERVACION>Observaciones</OBSERVACION>
              </DATOS_CLIENTE>
              <DATOS_PRODUCTOS>
                <PRODUCTO >
	                <DESCRIPCION>Item de Prueba E</DESCRIPCION>
	                <CANTIDAD>1000</CANTIDAD>
	                <MONTO>9999</MONTO>
	                <ALICUOTA>E</ALICUOTA>
                    <SERIAL></SERIAL>
                    <ROLLO></ROLLO>
                </PRODUCTO >
              </DATOS_PRODUCTOS>
              <DATOS_PAGOS>
                <PAGO>
	                <DESCRIPCION_PAGO>Efectivo</DESCRIPCION_PAGO>
	                <MONTO_PAGO>Efectivo</MONTO_PAGO>
                </PAGO>
              </DATOS_PAGOS>
              <PORCENTAJE_DESCUENTO_TOTAL>0</PORCENTAJE_DESCUENTO_TOTAL>
            </DOCUMENTO_FISCAL>";
    */
    #endregion Factura Prueba
    
    public static class clsMaquinaFiscalUtil {

        public static string RemoveAccents(string valInputString) {
            System.Text.RegularExpressions.Regex replace_a_Accents = new System.Text.RegularExpressions.Regex("[á|à|ä|â]", System.Text.RegularExpressions.RegexOptions.Compiled);
            System.Text.RegularExpressions.Regex replace_e_Accents = new System.Text.RegularExpressions.Regex("[é|è|ë|ê]", System.Text.RegularExpressions.RegexOptions.Compiled);
            System.Text.RegularExpressions.Regex replace_i_Accents = new System.Text.RegularExpressions.Regex("[í|ì|ï|î]", System.Text.RegularExpressions.RegexOptions.Compiled);
            System.Text.RegularExpressions.Regex replace_o_Accents = new System.Text.RegularExpressions.Regex("[ó|ò|ö|ô]", System.Text.RegularExpressions.RegexOptions.Compiled);
            System.Text.RegularExpressions.Regex replace_u_Accents = new System.Text.RegularExpressions.Regex("[ú|ù|ü|û]", System.Text.RegularExpressions.RegexOptions.Compiled);
            System.Text.RegularExpressions.Regex replace_A_Accents = new System.Text.RegularExpressions.Regex("[Á|À|Ä|Â]", System.Text.RegularExpressions.RegexOptions.Compiled);
            System.Text.RegularExpressions.Regex replace_E_Accents = new System.Text.RegularExpressions.Regex("[É|È|Ë|Ê]", System.Text.RegularExpressions.RegexOptions.Compiled);
            System.Text.RegularExpressions.Regex replace_I_Accents = new System.Text.RegularExpressions.Regex("[Í|Ì|Ï|Î]", System.Text.RegularExpressions.RegexOptions.Compiled);
            System.Text.RegularExpressions.Regex replace_O_Accents = new System.Text.RegularExpressions.Regex("[Ó|Ò|Ö|Ô]", System.Text.RegularExpressions.RegexOptions.Compiled);
            System.Text.RegularExpressions.Regex replace_U_Accents = new System.Text.RegularExpressions.Regex("[Ú|Ù|Ü|Û]", System.Text.RegularExpressions.RegexOptions.Compiled);
            valInputString = replace_a_Accents.Replace(valInputString, "a");
            valInputString = replace_e_Accents.Replace(valInputString, "e");
            valInputString = replace_i_Accents.Replace(valInputString, "i");
            valInputString = replace_o_Accents.Replace(valInputString, "o");
            valInputString = replace_u_Accents.Replace(valInputString, "u");
            valInputString = replace_A_Accents.Replace(valInputString, "A");
            valInputString = replace_E_Accents.Replace(valInputString, "E");
            valInputString = replace_I_Accents.Replace(valInputString, "I");
            valInputString = replace_O_Accents.Replace(valInputString, "O");
            valInputString = replace_U_Accents.Replace(valInputString, "U");
            return valInputString;
        }

        public static string RemoverCaracteresInvalidos(string valEntrada) {
            string vPatronEspacios = "\\s+";
            string vPatronCaracteresInvalidos = "[^a-zA-Z0-9 /s]+";
            string vReemplazoCaracteresInvalidos = "";
            string vReemplazoEspacios = " ";
            Regex rgx = new Regex(vPatronEspacios);
            string vResult = Regex.Replace(valEntrada, vPatronCaracteresInvalidos, vReemplazoCaracteresInvalidos, RegexOptions.Compiled);
            vResult = rgx.Replace(vResult, vReemplazoEspacios);
            return vResult;
        }

        public static string NormalizarString(string inputString) {
            string vNormalizedString = inputString.Normalize(NormalizationForm.FormD);
            StringBuilder vStringBuilder = new StringBuilder();
            for(int i = 0; i < vNormalizedString.Length; i++) {
                UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(vNormalizedString[i]);
                if(uc != UnicodeCategory.NonSpacingMark) {
                    vStringBuilder.Append(vNormalizedString[i]);
                }
            }
            return (vStringBuilder.ToString().Normalize(NormalizationForm.FormC));
        }
        
        public static string LimpiarXmlAntesDeParsear(string valXml){
            valXml = valXml.Replace("#","");
            valXml = valXml.Replace("&", "");
            valXml = valXml.Replace("^", "");
            valXml = valXml.Replace("$", "");
            return valXml;
        }

        public static string ObtenerValorNodoUnico(XElement valXmlDocumentoFiscal, string valNombreNodo) {
            string vResult = "";
            try {
                List<XElement> vNodos = valXmlDocumentoFiscal.Descendants().Where(e => e.Name.LocalName == valNombreNodo).ToList();
                if(vNodos.Count > 1){
                    throw new LibGalac.Aos.Catching.GalacWrapperException("Existe 2 o mas nodos con el mismo nombre ",new Exception());
                }else if(vNodos.Count > 0){
                    vResult = vNodos[0].Value;
                    vResult = NormalizarString(vResult);
                    vResult = RemoverCaracteresInvalidos(vResult);
                }
                return vResult;
            }catch(Exception vEx) {
                throw new LibGalac.Aos.Catching.GalacWrapperException("El formato del Xml Generado para el documento Fiscal no es valido, Nodo " + valNombreNodo, vEx);
            }
        }

        public static string DarFormatoANumero(string valEntrada, int valNumeroEnteros, int valNumeroDecimales, bool valUsaSeparadorDecimal) {
            string vResult = valEntrada;
            string vParteEntera = "";
            string vParteDecimal = "";
            if(LibGalac.Aos.Base.LibConvert.CurrentDecimalSeparator().Equals(",")) {
                vResult = "" + LibGalac.Aos.Base.LibConvert.ToDouble(valEntrada.Replace(".", ","));
            } else {
                vResult = "" + LibGalac.Aos.Base.LibConvert.ToDouble(valEntrada);
                if(vResult.Contains('.')) {
                    vResult=vResult.Replace('.', ',');
                }
            }
            if(vResult.Contains(',')) {
                vParteEntera = LibGalac.Aos.Base.LibText.SubString(vResult, 0, LibGalac.Aos.Base.LibText.IndexOf(vResult, ','));
                vParteDecimal = LibGalac.Aos.Base.LibText.SubString(vResult, LibGalac.Aos.Base.LibText.IndexOf(vResult, ',')+1,2);
            } else {
                vParteEntera = vResult;
            }
            while(vParteDecimal.Length<valNumeroDecimales){
                vParteDecimal += "0";
            }
            while(vParteEntera.Length < valNumeroEnteros) {
                vParteEntera = "0" + vParteEntera;
            }
            if(valUsaSeparadorDecimal) {
                vResult = vParteEntera + "," + vParteDecimal;
            } else vResult = vParteEntera + vParteDecimal;
            return vResult;
        }

        public static string RemoverCaracteresInvalidosParaNumeros(string valEntrada) {
            string vPatronEspacios = "\\s+";
            string vPatronCaracteresInvalidos = "[^0-9 /s /. ,]+";
            string vReemplazoCaracteresInvalidos = "";
            string vReemplazoEspacios = "";
            Regex rgx = new Regex(vPatronEspacios);
            string vResult = Regex.Replace(valEntrada, vPatronCaracteresInvalidos, vReemplazoCaracteresInvalidos, RegexOptions.Compiled);
            vResult = rgx.Replace(vResult, vReemplazoEspacios);
            return vResult;
        }

        public static string ObtenerValorNodoUnicoNumerico(XElement valXmlDocumentoFiscal, string valNombreNodo) {
            string vResult = "";
            try {
                List<XElement> vNodos = valXmlDocumentoFiscal.Descendants().Where(e => e.Name.LocalName == valNombreNodo).ToList();
                if(vNodos.Count > 1) {
                    throw new LibGalac.Aos.Catching.GalacWrapperException("Existe 2 o mas nodos con el mismo nombre ", new Exception());
                } else if(vNodos.Count > 0) {
                    vResult = vNodos[0].Value;
                    vResult = NormalizarString(vResult);
                    vResult = RemoverCaracteresInvalidosParaNumeros(vResult);
                }
                return vResult;
            } catch(Exception vEx) {
                throw vEx;
                throw new LibGalac.Aos.Catching.GalacWrapperException("El formato del Xml Generado para el documento Fiscal no es valido, Nodo " +valNombreNodo, vEx);
            }
        }

        public static double RedondearANDecimales(double valNumero, int valNumeroDecimales) {
            return Math.Round(valNumero, valNumeroDecimales);
        }

        public static string RedondearNumeroANDecimalesyDarFormatoSeparadorDeMiles(double valNumero, int valNumeroDecimales) {
            return string.Format("{0:n}", RedondearANDecimales(valNumero, valNumeroDecimales));
        }
    }
}
