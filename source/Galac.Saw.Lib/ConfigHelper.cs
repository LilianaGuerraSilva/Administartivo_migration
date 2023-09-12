using LibGalac.Aos.Base;
using LibGalac.Aos.Cnf;
using System;
using System.Xml;

namespace Galac.Saw.Lib {
    public static class ConfigHelper {
        public static void AddKeyToAppSettings(string valKey, string valValue) {           
            string vConfigurationFile = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
            string vAppSettingsFileName = "";
            string vConfigFileName = System.IO.Path.GetFileName(vConfigurationFile);
            string[] vSplitedName;
            if (vConfigFileName == null) {
                vAppSettingsFileName = "App" + ".settings";
            } else {
                vSplitedName = vConfigFileName.Split('.');
                if (vSplitedName.Length > 0) {
                    if (LibAppConfig.UseExternalConfig) {
                        vAppSettingsFileName = vSplitedName[0] + "App" + ".config";
                    } else {
                        vAppSettingsFileName = vSplitedName[0] + "App" + ".settings";
                    }
                }
            }
            vAppSettingsFileName = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, vAppSettingsFileName);
            XmlDocument vAppSettingsFile = new XmlDocument();
            vAppSettingsFile.Load(vAppSettingsFileName);
            XmlElement vAppSettings = vAppSettingsFile.DocumentElement;
            bool vContinue = false;
            foreach (XmlNode item in vAppSettings.ChildNodes) {
                if (vContinue) {                   
                    break;
                }
                foreach (XmlAttribute item1 in item.Attributes) {                    
                    if (LibString.S1IsEqualToS2(item1.Name, "key") && LibString.S1IsEqualToS2(item1.Value, valKey)) {                       
                        var x = item.Attributes["value"];
                        if (x != null) {
                            vContinue = true;
                            x.Value = valValue;
                            break;
                        }
                       
                    }
                }
            }
            vAppSettingsFile.Save(vAppSettingsFileName);
            System.Configuration.ConfigurationManager.RefreshSection("appSettings");
            if (!vContinue) {
                new LibAppConfig().AddKeyToAppSettings(valKey, valValue);
            }
        }
    }
}
