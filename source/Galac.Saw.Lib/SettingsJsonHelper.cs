using LibGalac.Aos.Base;
using LibGalac.Aos.Cnf;
using Newtonsoft.Json;
using System;
using System.Xml;

namespace Galac.Saw.Lib {
    public static class SettingsJsonHelper {
        public static TModel GetModelFromJsonNodeInAppSettings<TModel>() where TModel : class {
            string value = LibAppSettings.ReadAppSettingsKey("CONFIGLOCAL");
            return JsonConvert.DeserializeObject<TModel>(value);
        }

        public static void WriteJsonNodeInAppSettings<TModel>(TModel model) where TModel : class {
            string value = JsonConvert.SerializeObject(model);
            ConfigHelper.AddKeyToAppSettings("CONFIGLOCAL", value);
        }
    }
}
