using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.UI.Mvvm.Helpers;
using Galac.Adm.Ccl.Banco;
using Galac.Saw.Ccl.Inventario;
using Galac.Comun.Ccl.TablasGen;
using Galac.Saw.Ccl.Vendedor;
using Galac.Saw.Ccl.Cliente;

namespace Galac.Saw.Uil.SttDef.ViewModel {
    public class FkMonedaViewModel : IFkMonedaViewModel {
        [LibGridColum("C�digo")]
        public string Codigo { get; set; }
        [LibGridColum("Nombre", Width = 200)]
        public string Nombre { get; set; }
        [LibGridColum("S�mbolo")]
        public string Simbolo { get; set; }
    }
    public class FkVendedorViewModel  {
        public int ConsecutivoCompania { get; set; }
        [LibGridColum("C�digo")]
        public string Codigo { get; set; }
        [LibGridColum("Nombre")]
        public string Nombre { get; set; }
        [LibGridColum("N� R.I.F.")]
        public string RIF { get; set; }
        [LibGridColum("Status")]
        public eStatusVendedor StatusVendedor { get; set; }
        [LibGridColum("Ciudad")]
        public string Ciudad { get; set; }
        [LibGridColum("Zona Postal")]
        public string ZonaPostal { get; set; }
        [LibGridColum("Telefono")]
        public string Telefono { get; set; }
    }
    public class FkClienteViewModel : IFkClienteViewModel {
        public int ConsecutivoCompania { get; set; }
        public int Consecutivo { get; set; }
        [LibGridColum("C�digo")]
        public string Codigo { get; set; }
        [LibGridColum("Nombre")]
        public string Nombre { get; set; }
        [LibGridColum("N� R.I.F.")]
        public string NumeroRIF { get; set; }
        [LibGridColum("Ciudad")]
        public string Ciudad { get; set; }
        [LibGridColum("Zona Postal")]
        public string ZonaPostal { get; set; }
        [LibGridColum("Tel�fonos")]
        public string Telefono { get; set; }
        [LibGridColum("Zona De Cobranza")]
        public string ZonaDeCobranza { get; set; }
        [LibGridColum("C�digo del Vendedor")]
        public string CodigoVendedor { get; set; }
        [LibGridColum("Sector De Negocio")]
        public string SectorDeNegocio { get; set; }
        public string StatusStr { get; set; }
        public string Direccion { get; set; }
        public DateTime ClienteDesdeFecha { get; set; }
        public string TipoDeContribuyente { get; set; }
    }
    public class FkAlmacenViewModel : IFkAlmacenViewModel {
        public int ConsecutivoCompania { get; set; }
        public int Consecutivo { get; set; }
        [LibGridColum("C�digo", Width=50)]
        public string Codigo { get; set; }
        [LibGridColum("Nombre Almac�n", Width = 200)]
        public string NombreAlmacen { get; set; }
        [LibGridColum("Tipo De Almacen", Width = 150)]
        public eTipoDeAlmacen TipoDeAlmacen { get; set; }       
        public int ConsecutivoCliente { get; set; }
        [LibGridColum("C�digo del Cliente", Width = 100)]
        public string CodigoCliente { get; set; }
        [LibGridColum("Nombre Cliente", Width = 200)]
        public string NombreCliente { get; set; }
        [LibGridColum("C�digo Centro de Costos", Width = 150)]
        public string CodigoCc { get; set; }
        [LibGridColum("Descripci�n Centro de Costos", Width = 200)]
        public string Descripcion { get; set; }
    }
    public class FkConceptoBancarioViewModel : IFkConceptoBancarioViewModel {
        public int Consecutivo { get; set; }
        [LibGridColum("C�digo")]
        public string Codigo { get; set; }
        [LibGridColum("Descripci�n", Width = 300)]
        public string Descripcion { get; set; }
        [LibGridColum("Tipo", Type = eGridColumType.Enum)]
        public eIngresoEgreso Tipo { get; set; }
    }
    public class FkCuentaBancariaViewModel : IFkCuentaBancariaViewModel  {
        public int ConsecutivoCompania { get; set; }
        [LibGridColum("C�digo Cuenta Bancaria", Width = 200)]
        public string Codigo { get; set; }
        [LibGridColum("Status", Type = eGridColumType.Enum, DbMemberPath = "Gv_CuentaBancaria_B1.Status")]
        public eStatusCtaBancaria Status { get; set; }
        [LibGridColum("N� de Cuenta Bancaria", Width = 200)]
        public string NumeroCuenta { get; set; }
        [LibGridColum("Nombre de la Cuenta", Width = 300)]
        public string NombreCuenta { get; set; }
        [LibGridColum("C�digo del Banco")]
        public int CodigoBanco { get; set; }
        [LibGridColum("Nombre del Banco")]
        public string NombreBanco { get; set; }
        [LibGridColum("Tipo de Cuenta", Type = eGridColumType.Enum)]
        public eTipoDeCtaBancaria TipoCtaBancaria { get; set; }
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
    public class FkBeneficiarioViewModel : IFkBeneficiarioViewModel {
        public int ConsecutivoCompania { get; set; }
        public int Consecutivo { get; set; }
        [LibGridColum("C�digo")]
        public string Codigo { get; set; }
        [LibGridColum("N�mero RIF")]
        public string NumeroRIF { get; set; }
        [LibGridColum("Nombre Beneficiario")]
        public string NombreBeneficiario { get; set; }
        [LibGridColum("Tipo de Beneficiario")]
        public eTipoDeBeneficiario TipoDeBeneficiario { get; set; }
    }

    public class FkCiudadViewModel : IFkCiudadViewModel {
        [LibGridColum("Nombre", Width = 200)]
        public string NombreCiudad { get; set; }
    }

}
