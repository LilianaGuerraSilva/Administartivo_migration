using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.UI.Mvvm.Helpers;
using Galac.Saw.Ccl.Tablas;

namespace Galac.Saw.Uil.Tablas.ViewModel {

   public class FkNotaFinalViewModel : IFkNotaFinalViewModel {
      [LibGridColum("Codigo", Width = 300)]
      public string CodigoDeLaNota { get; set; }

      [LibGridColum("Descripcion", Width = 300)]
      public string Descripcion { get; set; }

      public int ConsecutivoCompania { get; set; }
   }


   public class FkTipoProveedorViewModel : IFkTipoProveedorViewModel {

      public int ConsecutivoCompania { get; set; }

      public string Nombre { get; set; }

   }

   public class FkMaquinaFiscalViewModel : IFkMaquinaFiscalViewModel {
      public int ConsecutivoCompania { get; set; }
      [LibGridColum("Consecutivo", Width = 150)]
      public string ConsecutivoMaquinaFiscal { get; set; }
      [LibGridColum("Descripción", Width = 200)]
      public string Descripcion { get; set; }
      [LibGridColum("Número Registro", Width = 150)]
      public string NumeroRegistro { get; set; }
      [LibGridColum("Status", Width = 150, PrintingMemberPath = "StatusStr", Type = eGridColumType.Enum, DbMemberPath = "StatusStr")]
      public eStatusMaquinaFiscal Status { get; set; }
      public int LongitudNumeroFiscal { get; set; }
   }
   public class FkUnidadDeVentaViewModel : IFkUnidadDeVentaViewModel {
      [LibGridColum("Unidad de Venta")]
      public string Nombre { get; set; }
      [LibGridColum("Código Unidad de Venta")]
      public string Codigo { get; set; }
   }
   public class FkZonaCobranzaViewModel : IFkZonaCobranzaViewModel {
      public int ConsecutivoCompania { get; set; }
      [LibGridColum("Nombre")]
      public string Nombre { get; set; }
   }
   public class FkFormaDelCobroViewModel : IFkFormaDelCobroViewModel {
      [LibGridColum("Código")]
      public string Codigo { get; set; }
      [LibGridColum("Nombre")]
      public string Nombre { get; set; }
      [LibGridColum("TipoDePago")]
      public eTipoDeFormaDePago TipoDePago { get; set; }
   }

   public class FkPropAnalisisVencViewModel : IFkPropAnalisisVencViewModel {
      public int SecuencialUnique0 { get; set; }
   }

   public class FkUrbanizacionZPViewModel : IFkUrbanizacionZPViewModel {
      [LibGridColum("Urbanización")]
      public string Urbanizacion { get; set; }
      [LibGridColum("Zona Postal")]
      public string ZonaPostal { get; set; }
   }
   public class FkLineaDeProductoViewModel : IFkLineaDeProductoViewModel {   
	   [LibGridColum("ConsecutivoCompania")]
       public int ConsecutivoCompania { get; set; }
        public int Consecutivo { get; set; }
        [LibGridColum("Nombre de la Línea")]
        public string Nombre { get; set; }
        [LibGridColum("Porcentaje de Comisión", eGridColumType.Numeric,Width =140, Alignment =eTextAlignment.Right)]
        public decimal PorcentajeComision { get; set; }
    }
   public class FkCentrodeCostosViewModel:IFkCentrodeCostosViewModel {
       public int ConsecutivoCompania { get; set; }
       public int Consecutivo { get; set; }
       [LibGridColum("Código")]
       public string Codigo { get; set; }
       [LibGridColum("Descripción")]
       public string Descripcion { get; set; }
   }

    public class FkCondicionesDePagoViewModel : IFkCondicionesDePagoViewModel {
        public int ConsecutivoCompania { get; set; }
        public int Consecutivo { get; set; }
        [LibGridColum("Descripcion")]
        public string Descripcion { get; set; }
    }
	
	public class FkImpuestoBancarioViewModel:IFkImpuestoBancarioViewModel {
        public DateTime FechaDeInicioDeVigencia { get; set; }        
    }
	
	public class FkRutaDeComercializacionViewModel : IFkRutaDeComercializacionViewModel {
        public int ConsecutivoCompania { get; set; }
        public int Consecutivo { get; set; }
        public string Descripcion { get; set; }
    }

    public class FkOtrosCargosDeFacturaViewModel: IFkOtrosCargosDeFacturaViewModel {
        public int ConsecutivoCompania { get; set; }
        [LibGridColum("Código del Cargo")]
        public string Codigo { get; set; }
        [LibGridColum("Descripción")]
        public string Descripcion { get; set; }
        [LibGridColum("Status")]
        public eStatusOtrosCargosyDescuentosDeFactura Status { get; set; }
        public eBaseCalculoOtrosCargosDeFactura SeCalculaEnBaseA { get; set; }
        public eComoAplicaOtrosCargosDeFactura ComoAplicaAlTotalFactura { get; set; }
        public decimal Monto { get; set; }
        public decimal PorcentajeSobreBase { get; set; }
        public decimal Sustraendo { get; set; }
        public decimal PorcentajeComision { get; set; }
        public bool ExcluirDeComision { get; set; }
        public eBaseFormulaOtrosCargosDeFactura BaseFormula { get; set; }
    }
}


