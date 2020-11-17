using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Galac.Saw.Reconv {
    public static class clsUtilReconv {
         private static DateTime vFechaReconversion = new DateTime(2010,12,31);
         private static decimal vFactorDeConversion = 100000m;

        public static DateTime GetFechaReconversion() {
            return vFechaReconversion;
        }

        public static decimal GetFactorDeConversion() {
            return vFactorDeConversion;
        }
    }
}
