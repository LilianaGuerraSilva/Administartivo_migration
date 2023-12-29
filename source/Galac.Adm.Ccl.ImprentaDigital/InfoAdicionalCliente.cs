using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Galac.Adm.Ccl.ImprentaDigital {    
    public class InfoAdicionalCliente {
        public string Codigo {
            get; set;
        }
        public string PersonaContacto {
            get; set;
        }
        public string Direccion {
            get; set;
        }
        public string Ciudad {
            get; set;
        }
        public string ZonaPostal {
            get; set;
        }

        public InfoAdicionalCliente() {
            Codigo = string.Empty;
            PersonaContacto = string.Empty;
            Direccion = string.Empty;
            Ciudad = string.Empty;
            ZonaPostal = string.Empty;
        }
    }
}
