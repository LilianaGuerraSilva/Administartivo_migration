using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base;

namespace Galac.Saw.Uil.SttDef {
    public static class clsGlobalValues {
        public static LibXmlMemInfo AppMemoryInfo { get; set; }
        public static LibXmlMFC Mfc { get; set; }

        public static void SetPropertyvalue(string valModule, string valProperty, object  valValue) {
            string xpath = "GpParameters/" + valModule + "/" + valProperty;
            System.Xml.XPath.XPathNavigator xNav = clsGlobalValues.AppMemoryInfo.CreateNavigator().SelectSingleNode(xpath);
            System.Xml.XPath.XPathNavigator xNavModule = clsGlobalValues.AppMemoryInfo.CreateNavigator().SelectSingleNode("GpParameters/" + valModule);
            if(xNavModule == null) {
                clsGlobalValues.AppMemoryInfo.DocumentElement.AppendChild(clsGlobalValues.AppMemoryInfo.CreateElement(valModule));
            }
            if(xNav == null) {
                xNav = clsGlobalValues.AppMemoryInfo.CreateNavigator().SelectSingleNode("GpParameters/" + valModule);                
                xNav.AppendChild("<" + valProperty +">" + LibConvert.ToStr(valValue) + "</" + valProperty + ">");
            } else {
                xNav.InnerXml = LibConvert.ToStr(valValue);
            }

        }
    }
}
