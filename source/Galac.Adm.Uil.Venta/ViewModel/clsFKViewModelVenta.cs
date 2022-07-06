using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Ccl.Usal;
using LibGalac.Aos.UI.Mvvm.Helpers;
using Galac.Saw.Ccl.Inventario;
using Galac.Adm.Ccl.Venta;
using Galac.Saw.Ccl.Vendedor;
using Galac.Saw.Ccl.SttDef;
using Galac.Saw.Ccl.Cliente;
using Galac.Saw.Ccl.Tablas;
using Galac.Comun.Ccl.TablasGen;
using Galac.Adm.Ccl.CAnticipo;
using Galac.Adm.Ccl.Banco;
using Galac.Adm.Ccl.DispositivosExternos;
using LibGalac.Aos.Uil.Usal.ViewModel;
namespace Galac.Adm.Uil.Venta.ViewModel {

    public class FkGUserViewModel : ILibFkGUserViewModel {
        [LibGridColum("Nombre (Login)")]
        public string UserName { get; set; }
        [LibGridColum("Nombre y Apellido")]
        public string FirstAndLastName { get; set; }
        //[LibGridColum("Cargo")]
        public string Cargo { get; set; }
        //[LibGridColum("E-mail")]
        public string Email { get; set; }
        //[LibGridColum("Status")]
        public eStatusUsuario Status { get; set; }
        //[LibGridColum("Es Supervisor")]
        public bool IsSuperviser { get; set; }
    }

    public class FkVendedorViewModel : IFkVendedorViewModel {
        public int ConsecutivoCompania { get; set; }
        [LibGridColum("Código")]
        public string Codigo { get; set; }
        [LibGridColum("Cédula")]
        public string RIF { get; set; }
        [LibGridColum("Nombre")]
        public string Nombre { get; set; }
        //[LibGridColum("Status")]
        public eStatusVendedor StatusVendedor { get; set; }
        //[LibGridColum("Ciudad")]
        public string Ciudad { get; set; }
        //[LibGridColum("Zona Postal")]
        public string ZonaPostal { get; set; }
        //[LibGridColum("Telefono")]
        public string Telefono { get; set; }
    }

    public class FkZonaCobranzaViewModel : IFkZonaCobranzaViewModel {
        public int ConsecutivoCompania { get; set; }
        [LibGridColum("Nombre")]
        public string Nombre { get; set; }
    }

    public class FkClienteViewModel : IFkClienteViewModel {
        public int ConsecutivoCompania { get; set; }
        //[LibGridColum("Código")]
        public string Codigo { get; set; }
        public int Consecutivo { get; set; }
        [LibGridColum("Cédula", Width = 150)]
        public string NumeroRIF { get; set; }
        [LibGridColum("Nombre", Width = 300)]
        public string Nombre { get; set; }
        //[LibGridColum("Teléfonos")]
        public string Telefono { get; set; }
        public string StatusStr { get; set; }
        public string Direccion { get; set; }
        public DateTime ClienteDesdeFecha { get; set; }
        public string TipoDeContribuyente { get; set; }
    }
	
    public class FkArticuloInventarioViewModel : IFkArticuloInventarioViewModel {
        public int ConsecutivoCompania { get; set; }
        [LibGridColum("Código", DbMemberPath = "dbo.Gv_ArticuloInventario_B1.Codigo", Width = 150)]
        public string Codigo { get; set; }
        [LibGridColum("Descripción", DbMemberPath = "dbo.Gv_ArticuloInventario_B1.Descripcion", Width = 200)]
        public string Descripcion { get; set; }
        [LibGridColum("Precio Sin IVA", eGridColumType.Numeric, DbMemberPath = "dbo.Gv_ArticuloInventario_B1.PrecioSinIVA", Width = 150)]
        public decimal PrecioSinIVA { get; set; }
        [LibGridColum("Precio Con IVA", eGridColumType.Numeric, DbMemberPath = "dbo.Gv_ArticuloInventario_B1.PrecioConIVA", Width = 150)]
        public decimal PrecioConIVA { get; set; }
        public string LineaDeProducto { get; set; }
        public string Categoria { get; set; }
        [LibGridColum("Existencia", eGridColumType.Numeric, DbMemberPath = "dbo.Gv_ArticuloInventario_B1.Existencia", Width = 150)]
        public decimal Existencia { get; set; }
        public int AlicuotaIVA { get; set; }
        public decimal PorcentajeBaseImponible { get; set; }
        public eTipoDeArticulo TipoDeArticulo { get; set; }
        public eTipoArticuloInv TipoArticuloInv { get; set; }
        public bool UsaBalanza { get; set; }
        public decimal CostoUnitario { get; set; }
        public decimal Peso { get; set; }
        public string UnidadDeVenta { get; set; }
        public int AlicuotaIva { get; set; }
        [LibGridColum("Me Precio Sin IVA", eGridColumType.Numeric, DbMemberPath = "dbo.Gv_ArticuloInventario_B1.MePrecioSinIva", Width = 100, IsForSearch = false)]
        public decimal MePrecioSinIva { get; set; }
        [LibGridColum("Me Precio Con IVA", eGridColumType.Numeric, DbMemberPath = "dbo.Gv_ArticuloInventario_B1.MePrecioConIva", Width = 100, IsForSearch = false)]
        public decimal MePrecioConIva { get; set; }
        public decimal MePrecioSinIva2 { get; set; }
        public decimal MePrecioConIva2 { get; set; }
        public decimal MePrecioSinIva3 { get; set; }
        public decimal MePrecioConIva3 { get; set; }
        public decimal MePrecioSinIva4 { get; set; }
        public decimal MePrecioConIva4 { get; set; }
    }

    public class FkArticuloInventarioBuscarViewModel : IFkArticuloInventarioViewModel {
        public int ConsecutivoCompania { get; set; }
        [LibGridColum("Código", DbMemberPath = "dbo.Gv_ArticuloInventario_B1.Codigo", Width = 120)]
        public string Codigo { get; set; }
        [LibGridColum("Descripción", DbMemberPath = "dbo.Gv_ArticuloInventario_B1.Descripcion", Width = 200)]
        public string Descripcion { get; set; }
        [LibGridColum("Precio Sin IVA", eGridColumType.Numeric, DbMemberPath = "dbo.Gv_ArticuloInventario_B1.PrecioSinIVA", Width = 100, IsForSearch = false)]
        public decimal PrecioSinIVA { get; set; }
        [LibGridColum("Precio Con IVA", eGridColumType.Numeric, DbMemberPath = "dbo.Gv_ArticuloInventario_B1.PrecioConIVA", Width = 100, IsForSearch = false)]
        public decimal PrecioConIVA { get; set; }
        [LibGridColum("Precio Sin IVA 2", eGridColumType.Numeric, DbMemberPath = "dbo.Gv_ArticuloInventario_B1.PrecioSinIVA2", Width = 100, IsForList = false, IsForSearch = false)]
        public decimal PrecioSinIVA2 { get; set; }
        [LibGridColum("Precio Con IVA 2", eGridColumType.Numeric, DbMemberPath = "dbo.Gv_ArticuloInventario_B1.PrecioConIVA2", Width = 100, IsForList = false, IsForSearch = false)]
        public decimal PrecioConIVA2 { get; set; }
        [LibGridColum("Precio Sin IVA 3", eGridColumType.Numeric, DbMemberPath = "dbo.Gv_ArticuloInventario_B1.PrecioSinIVA3", Width = 100, IsForList = false, IsForSearch = false)]
        public decimal PrecioSinIVA3 { get; set; }
        [LibGridColum("Precio Con IVA 3", eGridColumType.Numeric, DbMemberPath = "dbo.Gv_ArticuloInventario_B1.PrecioConIVA3", Width = 100, IsForList = false, IsForSearch = false)]
        public decimal PrecioConIVA3 { get; set; }
        [LibGridColum("Precio Sin IVA 4", eGridColumType.Numeric, DbMemberPath = "dbo.Gv_ArticuloInventario_B1.PrecioSinIVA4", Width = 100, IsForList = false, IsForSearch = false)]
        public decimal PrecioSinIVA4 { get; set; }
        [LibGridColum("Precio Con IVA 4", eGridColumType.Numeric, DbMemberPath = "dbo.Gv_ArticuloInventario_B1.PrecioConIVA4", Width = 100, IsForList = false, IsForSearch = false)]
        public decimal PrecioConIVA4 { get; set; }
        public string LineaDeProducto { get; set; }
        public string Categoria { get; set; }
        public decimal Existencia { get; set; }
        public decimal AlicuotaIVA { get; set; }
        public int AlicuotaIva { get; set; }
        public decimal PorcentajeBaseImponible { get; set; }
        public eTipoDeArticulo TipoDeArticulo { get; set; }
        public eTipoArticuloInv TipoArticuloInv { get; set; }
        public bool UsaBalanza { get; set; }
        public decimal CostoUnitario { get; set; }
        public decimal Peso { get; set; }
        public string UnidadDeVenta { get; set; }
        [LibGridColum("Me Precio Sin IVA", eGridColumType.Numeric, DbMemberPath = "dbo.Gv_ArticuloInventario_B1.MePrecioSinIva", Width = 100, IsForSearch = false)]
        public decimal MePrecioSinIva { get; set; }
        [LibGridColum("Me Precio Sin IVA2", eGridColumType.Numeric, DbMemberPath = "dbo.Gv_ArticuloInventario_B1.MePrecioSinIva2", Width = 100, IsForSearch = false)]
        public decimal MePrecioSinIva2 { get; set; }
        [LibGridColum("Me Precio Sin IVA3", eGridColumType.Numeric, DbMemberPath = "dbo.Gv_ArticuloInventario_B1.MePrecioSinIva3", Width = 100, IsForSearch = false)]
        public decimal MePrecioSinIva3 { get; set; }
        [LibGridColum("Me Precio Sin IVA4", eGridColumType.Numeric, DbMemberPath = "dbo.Gv_ArticuloInventario_B1.MePrecioSinIva4", Width = 100, IsForSearch = false)]
        public decimal MePrecioSinIva4 { get; set; }
        [LibGridColum("Me Precio Con IVA", eGridColumType.Numeric, DbMemberPath = "dbo.Gv_ArticuloInventario_B1.MePrecioConIva", Width = 100, IsForSearch = false)]
        public decimal MePrecioConIva { get; set; }
        [LibGridColum("Me Precio Con IVA2", eGridColumType.Numeric, DbMemberPath = "dbo.Gv_ArticuloInventario_B1.MePrecioConIva2", Width = 100, IsForSearch = false)]
        public decimal MePrecioConIva2 { get; set; }
        [LibGridColum("Me Precio Con IVA3", eGridColumType.Numeric, DbMemberPath = "dbo.Gv_ArticuloInventario_B1.MePrecioConIva3", Width = 100, IsForSearch = false)]
        public decimal MePrecioConIva3 { get; set; }
        [LibGridColum("Me Precio Con IVA4", eGridColumType.Numeric, DbMemberPath = "dbo.Gv_ArticuloInventario_B1.MePrecioConIva4", Width = 100, IsForSearch = false)]
        public decimal MePrecioConIva4 { get; set; }
    }

    public class FkFacturaRapidaViewModel : IFkFacturaRapidaViewModel {
        public int ConsecutivoCompania { get; set; }
        [LibGridColum("Número de Factura")]
        public string Numero { get; set; }
        [LibGridColum("Fecha", BindingStringFormat = "dd/MM/yyyy")]
        public DateTime Fecha { get; set; }
        [LibGridColum("Rif o Cédula")]
        public string NumeroRIF { get; set; }
        [LibGridColum("Cliente")]
        public string NombreCliente { get; set; }
        public eTipoDocumentoFactura TipoDeDocumento { get; set; }
    }

    public class FkFormaDelCobroViewModel : IFkFormaDelCobroViewModel {
        [LibGridColum("Código")]
        public string Codigo { get; set; }
        [LibGridColum("Nombre")]
        public string Nombre { get; set; }
        [LibGridColum("eTipoDeFormaDePago")]
        public eTipoDeFormaDePago TipoDePago { get; set; }
    }

    public class FkCobroDeFacturaRapidaViewModel : IFkCobroDeFacturaRapidaViewModel {
        public int ConsecutivoCompania { get; set; }
        public string NumeroFactura { get; set; }
    }

    public class FkBancoViewModel : IFkBancoViewModel {
        public int Consecutivo { get; set; }
        [LibGridColum("Nombre")]
        public string Nombre { get; set; }
        [LibGridColum("Código")]
        public string Codigo { get; set; }

    }

    public class FkAnticipoViewModel : IFkAnticipoViewModel {
        [LibGridColum("Status", eGridColumType.Enum, DbMemberPath = "dbo.Gv_Anticipo_B1.Status", EnumItemsSource = "ArrayStatus")]
        public eStatusAnticipo Status { get; set; }
        [LibGridColum("Fecha", eGridColumType.DatePicker, BindingStringFormat = "dd/MM/yyyy")]
        public DateTime Fecha { get; set; }
        public int ConsecutivoCompania { get; set; }
        public int ConsecutivoAnticipo { get; set; }
        [LibGridColum("Numero", DbMemberPath = "dbo.Gv_Anticipo_B1.Numero")]
        public string Numero { get; set; }
        [LibGridColum("Monto Disponible", eGridColumType.Numeric)]
        public decimal MontoDisponible {
            get {
                return MontoTotal - MontoUsado;
            }
        }
        public string NombreCliente { get; set; }
        [LibGridColum("Monto Total", eGridColumType.Numeric)]
        public decimal MontoTotal { get; set; }
        public decimal MontoUsado { get; set; }
        public string CodigoCliente { get; set; }
        public string CodigoProveedor { get; set; }
        public string NombreProveedor { get; set; }
        public string NumeroCheque { get; set; }
        public int ConsecutivoRendicion { get; set; }
        public int ConsecutivoCaja { get; set; }
        public eStatusAnticipo[] ArrayStatus {
            get {
                List<eStatusAnticipo> vResult = new List<eStatusAnticipo>();
                vResult.Add(eStatusAnticipo.ParcialmenteUsado);
                vResult.Add(eStatusAnticipo.Vigente);
                return vResult.ToArray();
            }
        }
    }
	
    public class FkMonedaViewModel : IFkMonedaViewModel {
        [LibGridColum("Codigo")]
        public string Codigo {
            get; set;
        }
        [LibGridColum("Nombre")]
        public string Nombre {
            get; set;
        }
        [LibGridColum("Simbolo")]
        public string Simbolo {
            get; set;
        }
    }
	
    public class FkCuentaBancariaViewModel : IFkCuentaBancariaViewModel {
        public int ConsecutivoCompania { get; set; }
        public string Codigo { get; set; }
        public eStatusCtaBancaria Status { get; set; }
        [LibGridColum("Numero de Cuenta")]
        public string NumeroCuenta { get; set; }
        [LibGridColum("Nombre de la Cuenta")]
        public string NombreCuenta { get; set; }
        public int CodigoBanco { get; set; }
        [LibGridColum("Nombre del Banco")]
        public string NombreBanco { get; set; }
        public eTipoDeCtaBancaria TipoCtaBancaria { get; set; }
        public string CodigoMoneda { get; set; }
        public string NombreDeLaMoneda { get; set; }
        public bool ManejaDebitoBancario { get; set; }
        public bool ManejaCreditoBancario { get; set; }
        public decimal SaldoDisponible { get; set; }
        public string CuentaContable { get; set; }
        public string NombrePlantillaCheque { get; set; }
        public eTipoAlicPorContIGTF TipoDeAlicuotaPorContribuyente { get; set; }
        public bool GeneraMovBancarioPorIGTF { get; set; }
    }
	
   public class FkCajaViewModel :IFkCajaViewModel {       
      public int ConsecutivoCompania { get; set; }    
       public int Consecutivo { get; set; }
       [LibGridColum("NombreCaja",eGridColumType.Generic,Header = "Nombre")]
       public string NombreCaja { get; set; }   
       public bool UsaMaquinaFiscal { get; set;}
       public bool UsaGaveta { get;  set; }      
       public ePuerto Puerto {get; set;}
       public string Comando {get; set;}
       public bool PermitirAbrirSinSupervisor {get; set;}
       public bool UsaAccesoRapido {get; set;}
       public eFamiliaImpresoraFiscal FamiliaImpresoraFiscal {get; set;}
       public eImpresoraFiscal ModeloDeMaquinaFiscal {get; set;}
       public string PrimerNumeroComprobanteFiscal { get; set; }
       public string PrimeNumeroNCFiscal { get; set; }
       public string SerialDeMaquinaFiscal {get; set;}
       public eTipoConexion TipoConexion { get; set; }
       public ePuerto PuertoMaquinaFiscal {get; set;}
       public bool AbrirGavetaDeDinero {get; set;}      
       public string IpParaConexion {get; set;}
       public string MascaraSubred {get; set;}
       public string Gateway {get; set;}
       public bool PermitirDescripcionDelArticuloExtendida {get; set;}
       public bool PermitirNombreDelClienteExtendido {get; set;}
       public bool UsarModoDotNet {get; set;}
       public string NombreOperador {get; set;}
       public DateTime FechaUltimaModificacion {get; set;}      
    } 	

   public class FkCajaAperturaViewModel : IFkCajaAperturaViewModel {        
       public int ConsecutivoCompania { get; set; }
       public int Consecutivo { get; set; }
       public int ConsecutivoCaja { get; set; }
       public string NombreDelUsuario { get; set; }
       public string NombreCaja {get; set;  }
    }
   
   public class FkMaquinaFiscalViewModel : IFkMaquinaFiscalViewModel {
        public int ConsecutivoCompania { get; set; }
        [LibGridColum("Consecutivo Máquina Fiscal", DbMemberPath = "ConsecutivoMaquinaFiscal", Width = 9)]
        public string ConsecutivoMaquinaFiscal { get; set; }
        [LibGridColum("Descripción")]
        public string Descripcion { get; set; }
        [LibGridColum("Nro.Registro")]
        public string NumeroRegistro { get; set; }
        [LibGridColum("Status")]
        public eStatusMaquinaFiscal Status { get; set; }
    }
	
   public class FkContratoViewModel : IFkContratoViewModel {
        public int ConsecutivoCompania { get; set; }
        [LibGridColum("N° Contrato", Width = 85, Alignment = eTextAlignment.Center)]
        public string NumeroContrato { get; set; }
        [LibGridColum("Status", Width = 70)]
        public eStatusContrato StatusContrato { get; set; }
        public string CodigoCliente { get; set; }
        [LibGridColum("Nombre de Cliente", Width = 200)]
        public string NombreCliente { get; set; }
        [LibGridColum("Fecha de Inicio", eGridColumType.DatePicker, Width = 100, BindingStringFormat = "dd/MM/yyyy", Alignment = eTextAlignment.Center)]
        public DateTime FechaDeInicio { get; set; }
    }
}
