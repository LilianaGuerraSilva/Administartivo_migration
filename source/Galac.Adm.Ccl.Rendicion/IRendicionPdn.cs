using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base;
using System.Xml;

namespace Galac.Adm.Ccl.CajaChica {
    public interface IRendicionPdn : ILibPdn {
         LibResponse  cerrar(Rendicion refRecord);
         LibResponse anular(Rendicion refRecord);
         LibResponse cerrarReposicion(Rendicion refRecord);
         LibResponse anularReposicion(Rendicion refRecord);
         LibResponse GenerarInfoContab(Rendicion refRecord);
         XmlReader generarResultadoXml(Rendicion refRecord);
         bool EsValidaCuentaBancariaCajaChica(int valConsecutivoCompania, string valCtaBancariaCajaChica);
        }
}
