using LibGalac.Aos.Base;
using System.Collections.Generic;
using System.Linq;

namespace Galac.Saw.Lib {
    public static class ConfigLocalHelper {
        public static int LeerConsecutivoLocal(int valConsecutivoCompania) {
            IList<LocalByCompany> values = SettingsJsonHelper.GetModelFromJsonNodeInAppSettings<List<LocalByCompany>>();
            if (values != null) {
                var vLocal = values.Where(t => t.ConsecutivoCompania == valConsecutivoCompania).FirstOrDefault();
                return (vLocal!= null) ? vLocal.ConsecutivoLocal : 0;
            }
            return 0;
        }

        public static void EscribirConsecutivoLocal(int valConsecutivoCompania, int valConsecutivoLocal) {
            IList<LocalByCompany> values = SettingsJsonHelper.GetModelFromJsonNodeInAppSettings<List<LocalByCompany>>();
            if (values == null) {
                values = new List<LocalByCompany>();
            }
            if (values.Any(t => t.ConsecutivoCompania == valConsecutivoCompania)) {
                values = values.Select(value => (value.ConsecutivoCompania != valConsecutivoCompania)
                                    ? value
                                    : new LocalByCompany() {
                                        ConsecutivoCompania = valConsecutivoCompania,
                                        ConsecutivoLocal = valConsecutivoLocal
                                    }).ToList();
            } else {
                values.Add(new LocalByCompany() { ConsecutivoCompania = valConsecutivoCompania, ConsecutivoLocal = valConsecutivoLocal });
            }
            SettingsJsonHelper.WriteJsonNodeInAppSettings(values);

        }
        private class LocalByCompany {
            public int ConsecutivoCompania { get; set; }
            public int ConsecutivoLocal { get; set; }
        }
    }
    
}
