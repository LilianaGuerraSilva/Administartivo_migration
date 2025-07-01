using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using LibGalac.Aos.Base;
namespace Galac.Saw.Ccl.Cliente {

    public interface IClientePdn : ILibPdn {
        #region Metodos Generados

        //NOTA CRISTIAN: FindByConsecutivo(int valConsecutivoCompania, string valConsecutivo);// originalmente valConsecutivo es de tipo string
        XElement FindByConsecutivo(int valConsecutivoCompania, int valConsecutivo);
        #endregion //Metodos Generados

        XElement ClientePorDefecto(int valConcecutivoCompania);
        object ConsultaCampoClientePorCodigo(string valCampo, string valCodigo, int valConsecutivoCompania);
        LibResponse InsertClienteForExternalRecord(string valNombre, string valNumeroRIF, string valDireccion, string valTelefono, ref string refCodigo, eTipoDocumentoIdentificacion valTipoDocumentoIdentificacion);
        bool BuscarClienteResumenDiario();
        bool InsertarClienteResumenDiario();

    } //End of class IClientePdn

} //End of namespace Galac.Saw.Ccl.Cliente

