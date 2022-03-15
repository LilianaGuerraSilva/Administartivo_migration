using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.UI.Mvvm.Helpers;
using Galac.Adm.Ccl.GestionCompras;
using Galac.Adm.Ccl.Banco;
using Galac.Adm.Ccl.CajaChica;

namespace Galac.Adm.Uil.CajaChica.ViewModel {

    public class FkProveedorViewModel : IFkProveedorViewModel {
        public int ConsecutivoCompania { get; set; }
        [LibGridColum("Código")]
        public string CodigoProveedor { get; set; }
        [LibGridColum("Nombre Proveedor", Width = 250)]
        public string NombreProveedor { get; set; }
        [LibGridColum("Contacto", Width = 250)]
        public string Contacto { get; set; }
        [LibGridColum("Teléfonos", Width = 150)]
        public string Telefonos { get; set; }
        public string NombrePaisResidencia{get; set; }
        public string PaisConveniosSunat { get; set; }
    }

    public class FkConceptoBancarioViewModel : IFkConceptoBancarioViewModel {
        public int Consecutivo { get; set; }
        [LibGridColum("Codigo")]
        public string Codigo { get; set; }
        [LibGridColum("Descripcion")]
        public string Descripcion { get; set; }
        [LibGridColum("Tipo")]
        public eIngresoEgreso Tipo { get; set; }
    }

    public class FkCuentaBancariaViewModel : IFkCuentaBancariaViewModel {
        public int ConsecutivoCompania { get; set; }
        [LibGridColum("Código Cuenta Bancaria", Width = 200)]
        public string Codigo { get; set; }
        [LibGridColum("Nº de Cuenta Bancaria", Width = 200)]
        public string NumeroCuenta { get; set; }
        [LibGridColum("Nombre de la Cuenta", Width = 300)]
        public string NombreCuenta { get; set; }
        [LibGridColum("Código del Banco")]
        public int CodigoBanco { get; set; }
        [LibGridColum("Nombre del Banco")]
        public string NombreBanco { get; set; }
        [LibGridColum("Tipo de Cuenta", eGridColumType.Enum, PrintingMemberPath = "TipoCtaBancariaStr", Width = 70)]
        public eTipoDeCtaBancaria TipoCtaBancaria { get; set; }
         [LibGridColum("Status", eGridColumType.Enum, PrintingMemberPath = "StatusStr", DbMemberPath = "Saw.Gv_CuentaBancaria_B1.Status", Width = 50)]
        public eStatusCtaBancaria Status { get; set; }

        public string CodigoMoneda { get; set; }
        public string CuentaContable { get; set; }
        public bool ManejaCreditoBancario { get; set; }
        public bool ManejaDebitoBancario { get; set; }
        public string NombreDeLaMoneda { get; set; }
        public string NombrePlantillaCheque { get; set; }
        public decimal SaldoDisponible { get; set; }
        public eTipoAlicPorContIGTF TipoDeAlicuotaPorContribuyente { get; set; }
        public bool GeneraMovBancarioPorIGTF { get; set; }
    }

    public class FkRendicionViewModel : IFkRendicionViewModel {
        public int ConsecutivoCompania { get; set; }
        public int Consecutivo { get; set; }
        [LibGridColum("Número")]
        public string Numero { get; set; }
        [LibGridColum("Fecha de Apertura", eGridColumType.DatePicker, Width = 200)]
        public DateTime FechaApertura { get; set; }
        [LibGridColum("Caja Chica")]
        public string CodigoCtaBancariaCajaChica { get; set; }
        [LibGridColum("Nombre Caja Chica", Width = 300)]
        public string NombreCuentaBancariaCajaChica { get; set; }
        [LibGridColum("Saldo", eGridColumType.Numeric)]
        public decimal Saldo { get; set; }
        [LibGridColum("Estado", eGridColumType.Enum, PrintingMemberPath = "StatusRendicionStr")]
        public eStatusRendicion StatusRendicion { get; set; }
    }

    public class FkBeneficiarioViewModel : IFkBeneficiarioViewModel {
        public int ConsecutivoCompania { get; set; }
        public int Consecutivo { get; set; }
        [LibGridColum("Código")]
        public string Codigo { get; set; }
        [LibGridColum("Número RIF")]
        public string NumeroRIF { get; set; }
        [LibGridColum("Nombre Beneficiario")]
        public string NombreBeneficiario { get; set; }
        [LibGridColum("Tipo de Beneficiario", eGridColumType.Enum)]
        public eTipoDeBeneficiario TipoDeBeneficiario { get; set; }
    }
}
