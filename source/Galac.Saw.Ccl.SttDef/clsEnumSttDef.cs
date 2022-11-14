using System;
using System.ComponentModel;
using LibGalac.Aos.Base;

namespace Galac.Saw.Ccl.SttDef {


    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eItemsMontoFactura {
        [LibEnumDescription("No Permitir ITEMS Negativos")]
        NO_PERMITIR_ITEMS_NEGATIVOS = 0,
        [LibEnumDescription("Permitir con Autorización")]
        PERMITIR_CON_AUTORIZACION,
        [LibEnumDescription("Permitir Siempre")]
        PERMITIR_SIEMPRE
    }


    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eTipoDePrefijoFactura {
        [LibEnumDescription("Sin Prefijo")]
        SinPrefijo = 0,
        [LibEnumDescription("Año")]
        Ano,
        [LibEnumDescription("Indicar")]
        Indicar
    }


    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eCantidadDeDecimales {
        [LibEnumDescription("Dos")]
        Dos = 0,
        [LibEnumDescription("Tres")]
        Tres,
        [LibEnumDescription("Cuatro")]
        Cuatro
    }

    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eTipoDeMetodoDeCosteo {
        [LibEnumDescription("Último Costo")]
        UltimoCosto = 0,
        [LibEnumDescription("Costo Promedio")]
        CostoPromedio
    }


    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eTipoDeOrdenDePagoAImprimir {
        [LibEnumDescription("Orden de  Pago con  Cheque")]
        OrdendePagoconCheque = 0,
        [LibEnumDescription("Solo  Orden de  Pago")]
        SoloOrdendePago,
        [LibEnumDescription("Orden de  Pago separada de  Cheque")]
        OrdendePagoseparadadeCheque
    }


    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eFormaDeReiniciarComprobanteRetIVA {
        [LibEnumDescription("Sin  Escoger")]
        SinEscoger = 0,
        [LibEnumDescription("Por  Mes")]
        PorMes,
        [LibEnumDescription("Por  Año")]
        PorAno,
        [LibEnumDescription("Al  Completar")]
        AlCompletar
    }


    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eDondeSeEfectuaLaRetencionIVA {
        [LibEnumDescription("No  Retenida")]
        NoRetenida = 0,
        [LibEnumDescription("CxP")]
        CxP,
        [LibEnumDescription("Pago")]
        Pago
    }


    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eDondeSeEfectuaLaRetencionISLR {
        [LibEnumDescription("No  Retenida")]
        NoRetenida = 0,
        [LibEnumDescription("Cx P")]
        CxP,
        [LibEnumDescription("Pago")]
        Pago
    }


    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eTipoDeSolicitudDeIngresoDeTasaDeCambio {
        [LibEnumDescription("Siempre Al Emitir Primera Factura")]
        SiempreAlEmitirPrimeraFactura = 0,
        [LibEnumDescription("Solo Si Es Necesario")]
        SoloSiEsNecesario
    }


    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eComprobanteConCheque {
        [LibEnumDescription("Comprobante con  Cheque")]
        ComprobanteconCheque = 0,
        [LibEnumDescription("Solo  Comprobante")]
        SoloComprobante,
        [LibEnumDescription("Comprobante separado de  Cheque")]
        ComprobanteseparadodeCheque
    }


    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eTipoDeAgrupacionParaLibrosDeVenta {
        [LibEnumDescription("POR RESUMEN DE VENTAS")]
        PORRESUMENDEVENTAS = 0,
        [LibEnumDescription("POR TIPO DE CONTRIBUYENTE")]
        PORTIPODECONTRIBUYENTE,
        [LibEnumDescription("DETALLADO POR FACTURA")]
        DETALLADOPORFACTURA
    }


    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eFormaDeOrdenarCodigos {
        [LibEnumDescription("NORMAL")]
        NORMAL = 0,
        [LibEnumDescription("ESPECIAL")]
        ESPECIAL
    }


    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eDecision {
        [LibEnumDescription("Sí")]
        Si = 0,
        [LibEnumDescription("No")]
        No
    }


    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eCalculoParaComisionesSobreCobranzaEnBaseA {
        [LibEnumDescription("Monto")]
        Monto = 0,
        [LibEnumDescription("Días Vencidos")]
        DiasVencidos,
        [LibEnumDescription("Porcentaje por Artículo")]
        Porcentaje_por_Articulo,
        [LibEnumDescription("Porcentaje por Línea de Producto")]
        Porcentaje_por_Linea_de_Producto,
        [LibEnumDescription("Mixto")]
        Mixto
    }


    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eFormaDeEscogerCompania {
        [LibEnumDescription("Por Nombre")]
        PorNombre = 0,
        [LibEnumDescription("Por Código")]
        PorCodigo
    }


    //[System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
    //public enum eMes {
    //    [LibEnumDescription("Enero")]
    //    Enero = 0, 
    //    [LibEnumDescription("Febrero")]
    //    Febrero, 
    //    [LibEnumDescription("Marzo")]
    //    Marzo,
    //    [LibEnumDescription("Abril")]
    //    Abril,
    //    [LibEnumDescription("Mayo")]
    //    Mayo,
    //    [LibEnumDescription("Junio")]
    //    Junio,
    //    [LibEnumDescription("Julio")]
    //    Julio,
    //    [LibEnumDescription("Agosto")]
    //    Agosto,
    //    [LibEnumDescription("Septiembre")]
    //    Septiembre,
    //    [LibEnumDescription("Octubre")]
    //    Octubre,
    //    [LibEnumDescription("Noviembre")]
    //    Noviembre,
    //    [LibEnumDescription("Diciembre")]
    //    Diciembre
    //}

    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eTipoReverso {
        [LibEnumDescription("NOTA DE CRÉDITO")]
        NOTADECREDITO = 0,
        [LibEnumDescription("FACTURA")]
        FACTURA
    }


    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eAccionAlAnularFactDeMesesAnt {
        [LibEnumDescription("Permitir  Anular  Sin  Chequear")]
        PermitirAnularSinChequear = 0,
        [LibEnumDescription("Preguntar  Si  Desea  Anular")]
        PreguntarSiDeseaAnular,
        [LibEnumDescription("No  Permitir  Anular")]
        NoPermitirAnular
    }


    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eTipoDocumentoFactura {
        [LibEnumDescription("Factura")]
        Factura = 0,
        [LibEnumDescription("Nota De Crédito")]
        NotaDeCredito,
        [LibEnumDescription("Nota De Débito")]
        NotaDeDebito,
        [LibEnumDescription("Resumen Diario De Ventas")]
        ResumenDiarioDeVentas,
        [LibEnumDescription("No Asignado")]
        NoAsignado,
        [LibEnumDescription("Comprobante Fiscal")]
        ComprobanteFiscal,
        [LibEnumDescription("Boleta")]
        Boleta,
        [LibEnumDescription("Nota de Crédito Comprobante Fiscal")]
        NotaDeCreditoComprobanteFiscal,
        [LibEnumDescription("Nota de Entrega")]
        NotaEntrega,
        [LibEnumDescription("Todos")]
        Todos
    }


    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eTipoDeNivelDePrecios {
        [LibEnumDescription("Por Usuario")]
        PorUsuario = 0,
        [LibEnumDescription("Por Cliente")]
        PorCliente
    }


    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eComisionesEnFactura {
        [LibEnumDescription("Sobre Total Factura")]
        SobreTotalFactura = 0,
        [LibEnumDescription("Sobre Renglones")]
        SobreRenglones,
        [LibEnumDescription("Sobre Total Factura y Renglones")]
        SobreTotalFacturayRenglones
    }

    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eBloquearEmision {
        [LibEnumDescription("No Bloquear")]
        NoBloquear = 0,
        [LibEnumDescription("Bloquear Por Caja")]
        BloquearPorCaja,
        [LibEnumDescription("Bloqueo General")]
        BloqueoGeneral
    }

    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eComisionesEnRenglones {
        [LibEnumDescription("Por Un Vendedor")]
        PorUnVendedor = 0,
        [LibEnumDescription("Por Dos Vendedores")]
        PordosVendedores,
        [LibEnumDescription("Por Tres Vendedores")]
        PorTresVendedores
    }


    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eFormaDeOrdenarDetalleFactura {
        [LibEnumDescription("Como fue cargada")]
        Comofuecargada = 0,
        [LibEnumDescription("Por Código de Artículo")]
        PorCodigodeArticulo,
        [LibEnumDescription("Por Descripción de Artículo")]
        PorDescripciondeArticulo
    }


    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eAccionLimiteItemsFactura {
        [LibEnumDescription("Solo Advertir")]
        SoloAdvertir = 0,
        [LibEnumDescription("No Exceder Número Items")]
        NoExedernumeroItems
    }


    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
    public enum ePermitirSobregiro {
        [LibEnumDescription("No Chequear Existencia")]
        NoChequearExistencia = 0,
        [LibEnumDescription("Permitir Sobregiro")]
        PermitirSobregiro,
        [LibEnumDescription("No Permitir Sobregiro")]
        NoPermitirSobregiro
    }


    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eModeloDeFactura {
        [LibEnumDescription("Forma Libre (Carta)")]
        eMD_FORMALIBRE = 0,
        [LibEnumDescription("Otro")]
        eMD_OTRO,
        [LibEnumDescription("Impresión Modo Texto")]
        eMD_IMPRESION_MODO_TEXTO

    }


    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eTipoDePrefijo {
        [LibEnumDescription("Sin Prefijo")]
        SinPrefijo = 0,
        [LibEnumDescription("Año")]
        Ano,
        [LibEnumDescription("Indicar")]
        Indicar
    }


    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eModeloPlanillaForma00030 {
        [LibEnumDescription("F 00030_ F03_ Grafibond")]
        F00030_F03_Grafibond = 0,
        [LibEnumDescription("F 00030_ F03_ Olivenca")]
        F00030_F03_Olivenca,
        [LibEnumDescription("F 00030_ F04_ Grafibond")]
        F00030_F04_Grafibond
    }


    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eTipoNegocio {
        [LibEnumDescription("General")]
        eTN_General = 0,
        [LibEnumDescription("Taller Mecánico")]
        eTN_TallerMecanico
    }


    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eTipoDeDatoParametros {
        [LibEnumDescription("Enumerativo")]
        Enumerativo = 0,
        [LibEnumDescription("Int")]
        Int,
        [LibEnumDescription("String")]
        String,
        [LibEnumDescription("Decimal")]
        Decimal
    }

    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eTipoDeFormatoFecha {
        [LibEnumDescription("dd/mm/yyyy")]
        eCSF_CON_BARRA = 0,
        [LibEnumDescription("dd-mm-yyyy")]
        eCSF_CON_GUION,
        [LibEnumDescription("dd.mm.yyyy")]
        eCSF_CON_PUNTO,
        [LibEnumDescription("Otro")]
        eCSF_CON_OTRO
    }

    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eCampoCodigoAlternativoDeArticulo {
        [LibEnumDescription("No Asignado")]
        eCCADA_NoAsignado = 0,
        [LibEnumDescription("Campo Definible 1")]
        eCCADA_CampoDefinible1,
        [LibEnumDescription("Campo Definible 2")]
        eCCADA_CampoDefinible2,
        [LibEnumDescription("Campo Definible 3")]
        eCCADA_CampoDefinible3,
        [LibEnumDescription("Campo Definible 4")]
        eCCADA_CampoDefinible4
    }

        [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eFormaDeAsociarCentroDeCostos {
        [LibEnumDescription("No Asociar")]
        NoAsociar = 0,
        [LibEnumDescription("Por Almacén")]
        PorAlmacen,
        [LibEnumDescription("Por Línea de Producto")]
        PorLineaDeProducto
    }

    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eIngresoEgreso {
        [LibEnumDescription("Ingreso")]
        Ingreso = 0,
        [LibEnumDescription("Egreso")]
        Egreso
    }

    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eAplicacionAlicuota {
        [LibEnumDescription("No Aplica")]
        No_Aplica = 0,
        [LibEnumDescription("Sustituir Alícuota Reducida")]
        Sustituir_Alicuota_Reducida,
        [LibEnumDescription("Sustituir Alícuota Adicional")]
        Sustituir_Alicuota_Adicional
    }

    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eMensajeCopiarParametros {
        [LibEnumDescription("Parámetros copiados exitosamente")]
        Copia_Exitosa = 0,
        [LibEnumDescription("La empresa seleccionada tiene los parámetros incompletos")]
        Error_Parametros_Incompletos,
        [LibEnumDescription("Error al eliminar los parámetros de la compañía actual")]
        Error_Al_Eliminar_Parametros,
        [LibEnumDescription("Error al copiar los parámetros")]
        Error_Al_Copiar_Parametros
    }

    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eFechaSugeridaRetencionesCxP {
        [LibEnumDescription("Fecha de la factura de la CxP")]
        FechaFacturaCxP = 0,
        [LibEnumDescription("Fecha de registro de la CxP")]
        FechaRegistroCxP
    }

    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eBaseCalculoParaAlicuotaEspecial {
        [LibEnumDescription("Total Factura")]
        Total_Factura = 0,
        [LibEnumDescription("Monto Exento más Base Imponible")]
        Monto_Exento_mas_Base_Imponible,
        [LibEnumDescription("Solo Base Imponible")]
        Solo_Base_Imponible,
        [LibEnumDescription("Solo Base Imponible Alícuota General")]
        Solo_Base_Imponible_Alicuota_General
    }


    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eTipoDeBusquedaArticulo {
        [LibEnumDescription("Código")]
        Codigo,
        [LibEnumDescription("Descripción")]
        Descripcion
    }

    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eTipoDePrecioAMostrarEnVerificador {
        [LibEnumDescription("Precio con Impuesto")]
        PrecioConIVA,
        [LibEnumDescription("Precio Desglosado")]
        PrecioDesglosado
    }

    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eNivelDePrecio {
        [LibEnumDescription("Todos")]
        Todos = 0,
        [LibEnumDescription("Nivel 1")]
        Nivel1,
        [LibEnumDescription("Nivel 2")]
        Nivel2,
        [LibEnumDescription("Nivel 3")]
        Nivel3,
        [LibEnumDescription("Nivel 4")]
        Nivel4
    }

    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eTipoDeConversionParaPrecios {
        [LibEnumDescription("De moneda local a divisa")]
        MonedaLocalADivisa = 0,
        [LibEnumDescription("De divisa a moneda local")]
        DivisaAMonedaLocal
    }

    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eFormaDeCalculoDePrecioRenglonFactura {
        [LibEnumDescription("A Partir del Precio Sin IVA")]
        APartirDelPrecioSinIVA = 0, 
        [LibEnumDescription("A Partir del Precio Con IVA")]
        APartirDelPrecioConIVA
    }
} //End of namespace namespace Galac.Saw.Ccl.SttDef
