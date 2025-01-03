using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security;
using System.Security.Permissions;
using System.Xml;
using System.Xml.Linq;
using LibGalac.Aos.Base;
using LibGalac.Aos.Brl;
using LibGalac.Aos.Base.Dal;
using Galac.Saw.Ccl.Tablas;
using Galac.Saw.Dal.Tablas;

namespace Galac.Saw.Brl.Tablas {
    public partial class clsAuditoriaConfiguracionNav: IAuditoriaConfiguracionPdn {


        public clsAuditoriaConfiguracionNav() {
        }
    
        
        bool IAuditoriaConfiguracionPdn.Auditar(string valMotivo, string valAccion, string valConfiguracionOriginal, string valConfiguracionNueva) {
            IAuditoriaConfiguracionDat instanciaDal = new clsAuditoriaConfiguracionDat();
            AuditoriaConfiguracion vCurrentRecord = new AuditoriaConfiguracion();
            vCurrentRecord.ConsecutivoCompania = LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania");
            vCurrentRecord.Accion = valAccion;
            vCurrentRecord.Motivo = valMotivo;
            vCurrentRecord.ConfiguracionOriginal = valConfiguracionOriginal;
            vCurrentRecord.ConfiguracionNueva = valConfiguracionNueva;
            return instanciaDal.Auditar(vCurrentRecord).Success;
        }
    } //End of class clsAuditoriaConfiguracionNav

} //End of namespace Galac.Saw.Brl.Tablas

