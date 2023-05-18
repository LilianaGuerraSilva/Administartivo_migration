using System;
using System.ComponentModel;
using LibGalac.Aos.Base;

namespace Galac.Saw.Ccl.Inventario { 

	[System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
	public enum eTipoDeAlmacen {
        [LibEnumDescription("Principal")]
        Principal = 0, 
        [LibEnumDescription("Secundario")]
        Secundario, 
        [LibEnumDescription("Consignación")]
        Consignacion, 
        [LibEnumDescription("Mercancía en tienda")]
        Mercanciaentienda
	}

    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
	public enum eTipoDeAlicuota {
        [LibEnumDescription("Exento")]
        Exento = 0, 
        [LibEnumDescription("Alicuota General")]
        AlicuotaGeneral, 
        [LibEnumDescription("Alicuota 2")]
        Alicuota2, 
        [LibEnumDescription("Alicuota 3")]
        Alicuota3,
        [LibEnumDescription("ExentoNC")]
        ExentoNC, 
        [LibEnumDescription("Alicuota General NC")]
        AlicuotaGeneralNC, 
        [LibEnumDescription("Alicuota 2 NC")]
        Alicuota2NC, 
        [LibEnumDescription("Alicuota 3 NC")]
        Alicuota3NC
	}


	[System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
	public enum eStatusArticulo {
        [LibEnumDescription("Vigente")]
        Vigente = 0, 
        [LibEnumDescription("Desincorporado")]
        Desincorporado
	}


    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
	public enum eTipoDeProducto {
        [LibEnumDescription("Nuevo")]
        Nuevo = 0, 
        [LibEnumDescription("Actualizacion")]
        Actualizacion, 
        [LibEnumDescription("Otro")]
        Otro, 
        [LibEnumDescription("POLIZA")]
        POLIZA
	}


    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
	public enum eTipoDeArticulo {
        [LibEnumDescription("Mercancía")]
        Mercancia = 0, 
        [LibEnumDescription("Servicio")]
        Servicio,
        [LibEnumDescription("Producto Compuesto")]
        ProductoCompuesto
	}


	[System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
	public enum eTipoArticuloInv {
        [LibEnumDescription("Simple")]
        Simple = 0, 
        [LibEnumDescription("Usa Talla/Color")]
        UsaTallaColor, 
        [LibEnumDescription("Usa Serial")]
        UsaSerial, 
        [LibEnumDescription("Usa Talla/Color y Serial")]
        UsaTallaColorySerial,
        [LibEnumDescription("Usa Serial y Rollo")]
        UsaSerialRollo
    }
	
	[System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
	public enum eTipoDeAsignacion {
        [LibEnumDescription("Todos")]
        Todos = 0, 
        [LibEnumDescription("Rango De Artículos")]
        RangoDeArticulos, 
        [LibEnumDescription("Línea De Producto")]
        LineaDeProducto
	}
	
	[System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
	public enum eTipoDeAccion {
        [LibEnumDescription("Activar")]
        Activar = 0, 
        [LibEnumDescription("Desactivar")]
        Desactivar
	}

    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eListarPorLineaDeProducto {
        [LibEnumDescription("Todos")]
        Todos = 0,
        [LibEnumDescription("Por Línea De Producto")]
        LineaDeProducto
    }

    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eRedondearPrecio {
        [LibEnumDescription("Sin Redondear")]
        SinRedondear = 0,
        [LibEnumDescription("Próxima Unidad")]
        ProximaUnidad,
        [LibEnumDescription("Próxima Decena")]
        ProximaDecena,
        [LibEnumDescription("Próxima Centena")]
        ProximaCentena,
        [LibEnumDescription("Próxima Unidad De Mil")]
        ProximaUnidadDeMil
    }

    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
    public enum ePrecioAjustar {
        [LibEnumDescription("Precio Sin IVA",CountryCode="VE")]
        [LibEnumDescription("Precio Sin IGV", CountryCode = "PE")]
        PrecioSinIVA = 0,
        [LibEnumDescription("Precio Con IVA", CountryCode = "VE")]
        [LibEnumDescription("Precio Con IGV", CountryCode = "PE")]
        PrecioConIVA
    }

    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eTipoDeMetodoDeCosteo {
        [LibEnumDescription("Ultimo Costo")]
        UltimoCosto = 0,
        [LibEnumDescription("Costo Promedio")]
        CostoPromedio
    }
    
    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eTipoDeRegistroPLE {
        [LibEnumDescription("Registro de Compras")]
        RegistroDeCompras = 0,
        [LibEnumDescription("Registro de Compras Sujeto No Domiciliado")]
        RegistroDeComprasSujtNoDomiciliado,
        [LibEnumDescription("Registro de Compras Simple")]
        RegistroDeComprasSimple,
        [LibEnumDescription("Registro de Ventas")]
        RegistroDeVentas,
        [LibEnumDescription("Registro de Ventas Simple")]
        RegistroDeVentasSimple,
        [LibEnumDescription("Registro De Inventario Permanente Valorizado")]
        RegistroDeInventPermanValorizado,
        [LibEnumDescription("Registro De Inventario Por Unidades Físicas")]
        RegistroDeInventUnidFiscas
    }

    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eTipoDeMercancia {
        [LibEnumDescription("No Aplica")]
        NoAplica = 0,
        [LibEnumDescription("Materia Prima")]
        MateriaPrima,
        [LibEnumDescription("Producto Terminado")]
        ProductoTerminado        
    }
	
    public enum eTipoActualizacion {
        Existencia = 0,
        Costo,
        ExistenciayCosto
    }
	
	[System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
	public enum eTipodeOperacion {
        [LibEnumDescription("Entrada de  Inventario")]
        EntradadeInventario = 0, 
        [LibEnumDescription("Salida de  Inventario")]
        SalidadeInventario,
        [LibEnumDescription("Autoconsumo")]
        Autoconsumo,
        [LibEnumDescription("Retiro")]
        Retiro,
    }
	
	[System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
	public enum eTipoGeneradoPorNotaDeEntradaSalida {
        [LibEnumDescription("Usuario")]
        Usuario = 0, 
        [LibEnumDescription("Orden De Produccion")]
        OrdenDeProduccion
	}

	[System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
	public enum eTipoNotaProduccion {
        [LibEnumDescription("No  Aplica")]
        NoAplica = 0, 
        [LibEnumDescription("Abrir")]
        Abrir, 
        [LibEnumDescription("Cerrar")]
        Cerrar, 
        [LibEnumDescription("Ajuste")]
        Ajuste
	}

	[System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
	public enum eStatusNotaEntradaSalida {
        [LibEnumDescription("Vigente")]
        Vigente = 0, 
        [LibEnumDescription("Anulada")]
        Anulada
	}

    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eMonedaPresentacionDeReporteRM {
        [LibEnumDescription("En Bolivares")]
        EnBolivares = 0,
        [LibEnumDescription("En Bolivares a la Tasa del Dia")]
        EnMonedaExtranjera
    }

} //End of namespace namespace Galac.Saw.Ccl.Inventario
