using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using LibGalac.Aos.Base;
#if IsExeBsF
namespace Galac.SawBsF.Wrp.DDL {
#else 
namespace Galac.Saw.Wrp.DDL {
#endif
    public class clsLibSawDDL {
        public static void SetAppConfigToCurrentDomain(string valPath) {
            AppDomain.CurrentDomain.SetData("APP_CONFIG_FILE", valPath);
            FieldInfo fiInit = typeof(
               System.Configuration.ConfigurationSettings).GetField(
                   "_configurationInitialized",
                   BindingFlags.NonPublic | BindingFlags.Static);
            FieldInfo fiSystem = typeof(
                System.Configuration.ConfigurationSettings).GetField(
                    "_configSystem", BindingFlags.NonPublic | BindingFlags.Static);
            if (fiInit != null && fiSystem != null) {
                fiInit.SetValue(null, false);
                fiSystem.SetValue(null, null);
            }
        }

        public static string[] TransformArray(string valArgs, char valDelimiter) {
            string vJoined = valArgs;
            string[] vResult = LibString.Split(valArgs, valDelimiter); 
            return vResult;
        }
    }//End of class
}//End of namespace
