using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Galac.Saw.Reconv {
    public static class clsUtilReconv {
        private static DateTime vFechaReconversion = new DateTime(2021, 10, 1);
        private static DateTime vFechaDisposicionesTransitorias = new DateTime(2021, 9, 1);
        private static decimal vFactorDeConversion = 1000000m;

        public static DateTime GetFechaReconversion() {
            return vFechaReconversion;
        }

        public static decimal GetFactorDeConversion() {
            return vFactorDeConversion;
        }

        public static DateTime GetFechaDisposicionesTransitorias() {
            return vFechaDisposicionesTransitorias;
        }
    }
}