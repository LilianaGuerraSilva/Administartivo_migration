using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using LibGalac.Aos.Base;
namespace Galac.Saw.Ccl.Inventario {

    public interface INotaDeEntradaSalidaPdn : ILibPdn {
        XElement FindByNumeroDocumento(int valConsecutivoCompania, string valNumeroDocumento);
        LibResponse AgregarNotaDeEntradaSalida(IList<NotaDeEntradaSalida> valListNotaDeEntradaSalida);
        LibResponse AnularNotaDeSalidaAsociadaProduccion(int valConsecutivoCompania, int valConsecutivoOrdenDeProduccion );
        XElement FindByConsecutivoCompaniaNumeroDocumento(int valConsecutivoCompania, string valNumeroDocumento);
        LibResponse AnularRecord(IList<NotaDeEntradaSalida> refRecord);
        LibResponse ReversarNotaES(int valConsecutivoCompania, string valNumeroDocumento);

    } //End of class INotaDeEntradaSalidaPdn

} //End of namespace Galac.Saw.Ccl.Inventario

