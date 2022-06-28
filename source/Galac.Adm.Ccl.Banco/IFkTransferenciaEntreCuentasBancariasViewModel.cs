using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Galac.Adm.Ccl.Banco {
	public interface IFkTransferenciaEntreCuentasBancariasViewModel {
		#region Propiedades
		int ConsecutivoCompania { get; set; }
		int Consecutivo { get; set; }
		eStatusTransferenciaBancaria Status { get; set; }
		DateTime Fecha { get; set; }
		string NumeroDocumento { get; set; }
		string CodigoCuentaBancariaOrigen { get; set; }
		string CodigoConceptoEgreso { get; set; }
		string CodigoCuentaBancariaDestino { get; set; }
		string CodigoConceptoIngreso { get; set; }
		#endregion //Propiedades

	} //End of class IFkTransferenciaEntreCuentasBancariasViewModel

} //End of namespace Galac.Adm.Ccl.Banco

