using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Galac.Comun.Ccl.TablasGen;
using LibGalac.Aos.Base;

namespace Galac.Adm.Brl.Venta {
    public class clsNoComunSaw {
        public IMonedaLocalActual InstanceMonedaLocalActual;

        public clsNoComunSaw() {
            InstanceMonedaLocalActual = new Galac.Comun.Brl.TablasGen.clsMonedaLocalActual();
            InstanceMonedaLocalActual.CargarTodasEnMemoriaYAsignarValoresDeLaActual(LibGalac.Aos.DefGen.LibDefGen.ProgramInfo.Country, LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetDateTime("Periodo", "FechaDeCierre"));            
        }
    }
}
