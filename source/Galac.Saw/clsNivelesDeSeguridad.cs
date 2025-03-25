using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Uil.Usal;
using LibGalac.Aos.Base;
using Galac.Saw.Lib;
using LibGalac.Aos.UI.Mvvm.Messaging;
#if IsExeBsF
namespace Galac.SawBsF {
#elif IsExeBsS​
namespace Galac.SawBsS {
#else
namespace Galac.Saw {
#endif
    public class clsNivelesDeSeguridad {

        public static void DefinirPlantilla() {            
            LibSecurityLevels vPlantillaPermisologia = new LibGalac.Aos.Uil.Usal.LibSecurityLevels();            
            vPlantillaPermisologia.StartDefinition();
            List<CustomRole> vPlantillaSaw = new Galac.Saw.SLev.clsSLev().PlantillaPermisos();
            foreach (CustomRole vRol in vPlantillaSaw) {
                vPlantillaPermisologia.Add(vRol.Module, vRol.Action, vRol.Group, vRol.GroupLevel);
            }
            int vLastLevel = vPlantillaPermisologia.MaxGroupLevel();
            
            if (!new clsLibSaw().EsVersionFacturadorBasico()) {
                vLastLevel++;
                Galac.Contab.SLev.clsSLev insNivelesWco = new Galac.Contab.SLev.clsSLev(true, true, false, true);
                List<CustomRole> vPlantillaWco = insNivelesWco.PlantillaPermisosComun(vLastLevel);
                foreach (CustomRole vRol in vPlantillaWco) {
                    vPlantillaPermisologia.Add(vRol.Module, vRol.Action, vRol.Group, vRol.GroupLevel);
                }

                List<CustomRole> vPlantillaIntegrado = insNivelesWco.PlantillaPermisosIntegrados(vLastLevel);
                foreach (CustomRole vRol in vPlantillaIntegrado) {
                    vPlantillaPermisologia.Add(vRol.Module, vRol.Action, vRol.Group, vRol.GroupLevel);

                }
            }
            vPlantillaPermisologia.CloseDefinition(true, true, true, true);

        }
    }
}