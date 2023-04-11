using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using LibGalac.Aos.Base;

namespace Galac.Adm.Ccl.Banco {
	public interface ITransferenciaEntreCuentasBancariasPdn : ILibPdn {
		#region Metodos Generados
		XElement FindByConsecutivoCompaniaNumeroDocumentoCodigoCuentaBancariaOrigen(int valConsecutivoCompania, string valNumeroDocumento, string valCodigoCuentaBancariaOrigen);
		#endregion //Metodos Generados

		#region Código Programador
		bool CambiarStatusTransferencia(TransferenciaEntreCuentasBancarias valCompra, eAccionSR valAction);

		bool PerteneceAUnMesCerrado(DateTime valFecha);
		#endregion
	} //End of class ITransferenciaEntreCuentasBancariasPdn

} //End of namespace Galac.Adm.Ccl.Banco

