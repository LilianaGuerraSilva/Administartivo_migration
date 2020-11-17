using LibGalac.Aos.Base;
using System.Xml.Linq;

namespace Galac.Saw.Ccl.Cliente {
    public  interface IClientePdn : ILibPdn {
       
        XElement ClientePorDefecto(int valConcecutivoCompania);
        object ConsultaCampoClientePorCodigo(string valCampo, string valCodigo, int valConsecutivoCompania);
        LibResponse InsertClienteForExternalRecord(string valNombre, string valNumeroRIF, string valDireccion, string valTelefono, ref string refCodigo, eTipoDocumentoIdentificacion valTipoDocumentoIdentificacion);
    }

}
