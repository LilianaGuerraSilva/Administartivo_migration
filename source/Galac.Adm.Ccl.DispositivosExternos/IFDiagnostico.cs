using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Galac.Adm.Ccl.DispositivosExternos {
    public class IFDiagnostico {
        public bool EstatusDeComunicacion { get; set; }

        public string EstatusDeComunicacionDescription { get; set; }

        public bool VersionDeControladores { get; set; }

        public string VersionDeControladoresDescription { get; set; }

        public bool AlicuotasRegistradas { get; set; }

        public string AlicoutasRegistradasDescription { get; set; }

        public string ConfiguracionImpresora { get; set; }

        public string ConfiguracionImpresoraDescription { get; set; }

        public bool FechaYHora { get; set; }

        public string FechaYHoraDescription { get; set; }

        public bool ColaDeImpresion { get; set; }

        public string ColaDeImpresioDescription { get; set; }

        public IFDiagnostico() {
            Clear();
        }


        public void Clear() {
            EstatusDeComunicacion = false;
            EstatusDeComunicacionDescription = string.Empty;
            VersionDeControladores = false;
            VersionDeControladoresDescription = string.Empty;
            AlicuotasRegistradas = false;
            AlicoutasRegistradasDescription = string.Empty;
            ConfiguracionImpresora = string.Empty;
            ConfiguracionImpresoraDescription = string.Empty;
            FechaYHora = false;
            FechaYHoraDescription = string.Empty;
            ColaDeImpresion = false;
            ColaDeImpresioDescription = string.Empty;
        }

        public IFDiagnostico Clone() {
            IFDiagnostico vResult = new IFDiagnostico();
            vResult.AlicoutasRegistradasDescription = AlicoutasRegistradasDescription;
            vResult.AlicuotasRegistradas = AlicuotasRegistradas;
            vResult.ConfiguracionImpresoraDescription = ConfiguracionImpresoraDescription;
            vResult.ConfiguracionImpresora = ConfiguracionImpresora;
            vResult.ColaDeImpresioDescription = ColaDeImpresioDescription;
            vResult.ColaDeImpresion = ColaDeImpresion;
            vResult.EstatusDeComunicacion = EstatusDeComunicacion;
            vResult.EstatusDeComunicacionDescription = EstatusDeComunicacionDescription;
            vResult.FechaYHora = FechaYHora;
            vResult.FechaYHoraDescription = FechaYHoraDescription;
            vResult.VersionDeControladores = VersionDeControladores;
            vResult.VersionDeControladoresDescription = VersionDeControladoresDescription;
            return vResult;
        }

        public override string ToString() {
            return  "AlicuotasRegistradas ="        + AlicuotasRegistradas +
                    "\nConfiguracionImpresora = "   + ConfiguracionImpresora +
                    "\nColaDeImpresión = "          + ColaDeImpresion +
                    "\nFechaYHora = "               + FechaYHora +
                    "\nEstatusDeComunicación = "    + EstatusDeComunicacion +
                    "\nVersionDeControladores = "   + VersionDeControladores;
        }
    }
}
