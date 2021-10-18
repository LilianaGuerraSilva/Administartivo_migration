using System;
using System.ComponentModel;
using LibGalac.Aos.Base;

namespace Galac.Adm.Ccl.Banco {

	[System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
	public enum eIngresoEgreso {
        [LibEnumDescription("Ingreso")]
        Ingreso = 0, 
        [LibEnumDescription("Egreso")]
        Egreso
	}


	[System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eGeneradoPor
    {
        [LibEnumDescription("Usuario")]
        Usuario = 0,
        [LibEnumDescription("Cobranza")]
        Cobranza,
        [LibEnumDescription("Orden de Pago")]
        OrdendePago,
        [LibEnumDescription("CxP")]
        CxP,
        [LibEnumDescription("Débito  Bancario")]
        DebitoBancario,
        [LibEnumDescription("CxC")]
        CxC,
        [LibEnumDescription("Gestión  Cobranza")]
        GestionCobranza,
        [LibEnumDescription("Compra")]
        Compra,
        [LibEnumDescription("Anticipo")]
        Anticipo,
        [LibEnumDescription("Reconversión Monetaria")]
        ReconversionMonetaria,
        [LibEnumDescription("S10")]
        S10,
        [LibEnumDescription("Rendición")]
        Rendicion,
        [LibEnumDescription("Solicitud de Pago")]
        SolicitudDePago,
        [LibEnumDescription("Reposición De Caja Chica")]
        ReposicionDeCajaChica
    }


	[System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
	public enum eOrigenBeneficiario {
        [LibEnumDescription("Administrativo")]
        Administrativo = 0, 
        [LibEnumDescription("Nómina")]
        Nomina
	}


	[System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
	public enum eSolicitudGeneradaPor {
        [LibEnumDescription("Cálculos de Nómina")]
        CalculosDeNomina = 0, 
        [LibEnumDescription("Vacaciones")]
        Vacaciones,
        [LibEnumDescription("Utilidades")]
        Utilidades,
        [LibEnumDescription("Pago de Intereses")]
        PagoDeIntereses,
        [LibEnumDescription("Fideicomiso")]
        Fideicomiso,
        [LibEnumDescription("Liquidación")]
        Liquidacion,
        [LibEnumDescription("Adelanto de Vacaciones")]
        AdelantoDeVacaciones,
        [LibEnumDescription("Anticipo de Prestaciones")]
        AnticipoDePrestaciones, 
        [LibEnumDescription("No Aplica")]
        NoAplica,
        [LibEnumDescription("Tickets de Alimentación")]
        TicketsAlimentacion
    }


	[System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
	public enum eStatusSolicitud {
        [LibEnumDescription("Por Procesar")]
        PorProcesar = 0, 
        [LibEnumDescription("Procesada")]
        Procesada, 
        [LibEnumDescription("Parcialmente  Procesada")]
        ParcialmenteProcesada, 
        [LibEnumDescription("Anulada")]
        Anulada
	}


	[System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
	public enum eStatusSolicitudRenglon {
        [LibEnumDescription("Por Procesar")]
        PorProcesar = 0, 
        [LibEnumDescription("Procesada")]
        Procesada
	}


	[System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
	public enum eTipoDeFormaDePagoSolicitud {
        [LibEnumDescription("Efectivo")]
        Efectivo = 0, 
        [LibEnumDescription("Transferencia")]
        transferencia, 
        [LibEnumDescription("Cheque")]
        Cheque
	}


	[System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
	public enum eTipoDeBeneficiario {
        [LibEnumDescription("Persona Jurídica")]
        PersonaJuridica = 0, 
        [LibEnumDescription("Persona Natural")]
        PersonaNatural
	}

    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
	public enum eTipoDeCtaBancaria {
        [LibEnumDescription("Corriente")]
        Corriente = 0, 
        [LibEnumDescription("Ahorros")]
        Ahorros, 
        [LibEnumDescription("Activos  Líquidos")]
        ActivosLiquidos
	}

    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
	public enum eStatusCtaBancaria {
        [LibEnumDescription("Activo")]
        Activo = 0, 
        [LibEnumDescription("Inactivo")]
        Inactivo
	}

    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eConceptoBancarioPorDefecto {
        [LibEnumDescription("DEBITO BANCARIO AUTOMATICO")]
        DebitoBancarioAutomatico = 0,
        [LibEnumDescription("REVERSO AUTOMATICO COBRANZA")]
        ReversoAutomaticoCobranza,
        [LibEnumDescription("REV. AUTOMATICO ANT. COBRADO")]
        RevAutomaticoAntCobrado,
        [LibEnumDescription("REV. AUTOMATICO ANT. PAGADO")]
        RevAutomaticoAntPagado,
        [LibEnumDescription("ANTICIPO COBRADO")]
        AnticipoCobrado,
        [LibEnumDescription("ANTICIPO PAGADO")]
        AnticipoPagado,
        [LibEnumDescription("COBRO DIRECTO DE FACT.")]
        CobroDirectoDeFact,
        [LibEnumDescription("REVERSO AUTOMATICO PAGO")]
        ReversoAutomaticoPago,
        [LibEnumDescription("INGRESO POR TRANSFERENCIA")]
        TranserenciaIngreso,
        [LibEnumDescription("EGRESO POR TRANSFERENCIA")]
        TranserenciaEgreso,
        [LibEnumDescription("INGRESO POR DETRACCION")]
        DetraccionIngreso,
        [LibEnumDescription("EGRESO POR DETRACCION")]
        DetraccionEgreso,
        [LibEnumDescription("REVERSO AUTOMATICO SOLICITUD DE PAGO")]
        RevAutomaticoSoliPagado,
        [LibEnumDescription("PAGO DIRECTO RETENCION DESDE CXP.")]
        PagoRetencionDirectoDesdeCxp,
        [LibEnumDescription("PAGO PROVEEDORES - CAJA CHICA")]
        PagoCajaChica,
        [LibEnumDescription("EGRESO - REPOSICION CAJA CHICA")]
        EgresoReposicionCajaChica,
        [LibEnumDescription("REPOSICION CAJA CHICA")]
        IngresoReposicionCajaChica,
        [LibEnumDescription("REV PAGO PROVEEDORES-C.CHICA")]
        ReversoPagoCajaChica,
        [LibEnumDescription("REV EGRESO-REPOSICION C.CHICA")]
        ReversoEgresoReposicionCajaChica,
        [LibEnumDescription("REV REPOSICION CAJA CHICA")]
        ReversoIngresoReposicionCajaChica,
        [LibEnumDescription("I.T.F. AUTOMATICO")]
        ITFDebitoBancario
    }
     

} //End of namespace namespace Galac.Adm.Ccl.Banco
