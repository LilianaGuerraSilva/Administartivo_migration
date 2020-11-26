using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Galac.Comun.Ccl.TablasGen;
using LibGalac.Aos.Base;
namespace Galac.Saw.Brl.SttDef {
    public class clsUtilMonedaLocal {
        public IMonedaLocalActual InstanceMonedaLocalActual;
        public clsUtilMonedaLocal() {
            InstanceMonedaLocalActual = new Galac.Comun.Brl.TablasGen.clsMonedaLocalActual();
            InstanceMonedaLocalActual.CargarTodasEnMemoriaYAsignarValoresDeLaActual(LibGalac.Aos.DefGen.LibDefGen.ProgramInfo.Country, LibDate.Today());
        }
    }
}