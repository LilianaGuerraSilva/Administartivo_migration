using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.UI.Mvvm.Helpers;
using Galac.Comun.Ccl.TablasGen;
using Galac.Contab.Ccl.WinCont;
using Galac.Adm.Ccl.Banco;
using Galac.Adm.Uil.Banco.Properties;

namespace Galac.Adm.Uil.Banco.ViewModel {

	public class FkCuentaBancariaViewModel : IFkCuentaBancariaViewModel {
		public int ConsecutivoCompania { get; set; }
		[LibGridColum("Código", DbMemberPath = "Saw.Gv_CuentaBancaria_B1.Codigo", Width = 50)]
		public string Codigo { get; set; }
		//[LibGridColum("Código del Banco")]
		public int CodigoBanco { get; set; }
		[LibGridColum("Status", eGridColumType.Enum, PrintingMemberPath = "StatusStr", DbMemberPath = "Saw.Gv_CuentaBancaria_B1.Status", Width = 50)]
		public eStatusCtaBancaria Status { get; set; }
		[LibGridColum("Nombre de la Cuenta", Width = 180)]
		public string NombreCuenta { get; set; }
		public string NombreBanco { get; set; }
		[LibGridColum("Nº Cuenta Bancaria", Width = 180)]
		public string NumeroCuenta { get; set; }
		[LibGridColum("Tipo Cta", eGridColumType.Enum, PrintingMemberPath = "TipoCtaBancariaStr", Width = 70)]
		public eTipoDeCtaBancaria TipoCtaBancaria { get; set; }
		public string CodigoMoneda { get; set; }
		public string NombreDeLaMoneda { get; set; }
		public bool ManejaCreditoBancario { get; set; }
		public bool ManejaDebitoBancario { get; set; }
		[LibGridColum("Saldo", eGridColumType.Numeric, Width = 120)]
		public decimal SaldoDisponible { get; set; }
		public string CuentaContable { get; set; }
		public string NombrePlantillaCheque { get; set; }
		public eTipoAlicPorContIGTF TipoDeAlicuotaPorContribuyente { get; set; }
		public bool GeneraMovBancarioPorIGTF { get; set; }
	}

	public class FkBeneficiarioViewModel : IFkBeneficiarioViewModel {
		public int ConsecutivoCompania { get; set; }
		public int Consecutivo { get; set; }
		[LibGridColum("Código")]
		public string Codigo { get; set; }
		[LibGridColum(HeaderResourceName = "lblEtiquetaNumero", HeaderResourceType = typeof(Resources))]
		public string NumeroRIF { get; set; }
		[LibGridColum("Nombre Beneficiario")]
		public string NombreBeneficiario { get; set; }
		[LibGridColum("Tipo de Beneficiario")]
		public eTipoDeBeneficiario TipoDeBeneficiario { get; set; }
	}

	public class FkCuentaViewModel : IFkCuentaViewModel {
		public int ConsecutivoPeriodo { get; set; }
		[LibGridColum("Código")]
		public string Codigo { get; set; }
		[LibGridColum("Descripción", Width = 300)]
		public string Descripcion { get; set; }
		[LibGridColum("Naturaleza", eGridColumType.Enum, PrintingMemberPath = "NaturalezaDeLaCuentaStr")]
		public eNaturalezaDeLaCuenta NaturalezaDeLaCuenta { get; set; }
		[LibGridColum("De Titulo?", eGridColumType.YesNo)]
		public bool TieneSubCuentas { get; set; }
		[LibGridColum("Tiene Auxiliar", eGridColumType.YesNo)]
		public bool TieneAuxiliar { get; set; }
		[LibGridColum("Grupo Auxiliar", eGridColumType.Enum, PrintingMemberPath = "GrupoAuxiliarStr")]
		public eGrupoAuxiliar GrupoAuxiliar { get; set; }
		[LibGridColum("Es Activo Fijo", eGridColumType.YesNo)]
		public bool EsActivoFijo { get; set; }
		[LibGridColum("Codigo Grupo Activo")]
		public int CodigoGrupoActivo { get; set; }
		public eMetodoAjuste MetodoAjuste { get; set; }
	}
	public class FkBancoViewModel : IFkBancoViewModel {
		public int Consecutivo { get; set; }
		[LibGridColum("Código")]
		public string Codigo { get; set; }
		[LibGridColum("Nombre")]
		public string Nombre { get; set; }
	}
	public class FkMonedaViewModel : IFkMonedaViewModel {
		[LibGridColum("Código")]
		public string Codigo { get; set; }
		[LibGridColum("Nombre")]
		public string Nombre { get; set; }
		[LibGridColum("Símbolo")]
		public string Simbolo { get; set; }
	}

	public class FkConceptoBancarioViewModel : IFkConceptoBancarioViewModel {
		public int Consecutivo { get; set; }
		[LibGridColum("Código")]
		public string Codigo { get; set; }
		[LibGridColum("Descripción", Width = 300)]
		public string Descripcion { get; set; }
		[LibGridColum("Tipo")]
		public eIngresoEgreso Tipo { get; set; }
	}

	public class FkSolicitudesDePagoViewModel : IFkSolicitudesDePagoViewModel {
		public int ConsecutivoCompania { get; set; }
		public int ConsecutivoSolicitud { get; set; }
		[LibGridColum("Número Documento Origen", eGridColumType.Generic)]
		public int NumeroDocumentoOrigen { get; set; }
		[LibGridColum("Fecha de Solicitud", eGridColumType.DatePicker)]
		public DateTime FechaSolicitud { get; set; }
		public string StatusStr { get; set; }
		public string GeneradoPorStr { get; set; }
		//eSolicitudGeneradaPor GeneradoPor { get; set; }
	}

	public class FkTransferenciaEntreCuentasBancariasViewModel : IFkTransferenciaEntreCuentasBancariasViewModel {
		public int ConsecutivoCompania { get; set; }
		[LibGridColum("Nº Movimiento")]
		public int Consecutivo { get; set; }
		[LibGridColum("Status")]
		public eStatusTransferenciaBancaria Status { get; set; }
		[LibGridColum("Fecha")]
		public DateTime Fecha { get; set; }
		[LibGridColum("Nº Documento")]
		public string NumeroDocumento { get; set; }
		[LibGridColum("Cuenta Bancaria Origen")]
		public string CodigoCuentaBancariaOrigen { get; set; }
		[LibGridColum("Concepto Bancario de Egreso")]
		public string CodigoConceptoEgreso { get; set; }
		[LibGridColum("Cuenta Bancaria Destino")]
		public string CodigoCuentaBancariaDestino { get; set; }
		[LibGridColum("Concepto Bancario de Ingreso")]
		public string CodigoConceptoIngreso { get; set; }
	}
}

