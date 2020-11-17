using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;

namespace Galac.Adm.Ccl.Banco {
    public interface ISolicitudesDePagoPdn : ILibPdn {
      bool  InsertoSolicitudesDePagoDesdeNomina(XmlReader valXmlEntityReader, int valConsecutivoCompania,string valCuentaBancariaGenerica);
      bool EliminarSolicitudesDePagoDesdeNomina(string valNumeroDocumentoOrigen, int valConsecutivoCompania);
      string  GetSolicitudDePago(string valNumeroDocumentoOrigen, int valConsecutivoCompania);
      int GenerarProximoConsecutivoSolicitud(int valConsecutivoCompania);
    }
}
