using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Galac.Adm.Ccl.Banco;

namespace Galac.Adm.Ccl.Banco {
	public interface IFkCuentaBancariaViewModel {
		#region Propiedades
		int ConsecutivoCompania { get; set; }
		string Codigo { get; set; }
		eStatusCtaBancaria Status { get; set; }
		string NumeroCuenta { get; set; }
		string NombreCuenta { get; set; }
		int CodigoBanco { get; set; }
		string NombreBanco { get; set; }
		eTipoDeCtaBancaria TipoCtaBancaria { get; set; }
		string CodigoMoneda { get; set; }
		string NombreDeLaMoneda { get; set; }
		bool ManejaDebitoBancario { get; set; }
		bool ManejaCreditoBancario { get; set; }
		decimal SaldoDisponible { get; set; }
		string CuentaContable { get; set; }
		string NombrePlantillaCheque { get; set; }
		eTipoAlicPorContIGTF TipoDeAlicuotaPorContribuyente { get; set; }
		bool ExcluirDelInformeDeDeclaracionIGTF { get; set; }
		bool GeneraMovBancarioPorIGTF { get; set; }
		#endregion //Propiedades

	} //End of class IFkCuentaBancariaViewModel

} //End of namespace Galac.Adm.Ccl.Banco
