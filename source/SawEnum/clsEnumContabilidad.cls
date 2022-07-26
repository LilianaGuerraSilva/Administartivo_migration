VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsEnumContabilidad"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit

Enum AccionEscogerCompania
   EscogerCompania = 0
   EscogerPeriodo
End Enum

Enum enum_TipoRefleja
   eTR_NoAplicar_REFLEJA = 0
   eTR_ORIGEN_REFLEJA
   eTR_DESTINO_REFLEJA
End Enum

Enum enum_ContabilidadSystemModules
   eSM_ActivoFijo = 0
   eSM_Asiento
   eSM_AuditoriaComprobante
   eSM_AuditoriaDeProcesos
   eSM_Auxiliar
   eSM_CentroDeCostos
   eSM_Comprobante
   eSM_Cuenta
   eSM_DefCuenta
   eSM_Lote
   eSM_MontoCuenta
   eSM_ParametrosActivoFijo
   eSM_ParametrosContabilidad
   eSM_Periodo
   eSM_SeguridadPorCompania
   eSM_GrupoDeActivos
   eSM_GrupoDeInventario
   eSM_TipoDeComprobante
   eSM_UnaCuentaAuxiliarActivoFijo
   eSM_UnGrupoAjuste
   eSM_ConciliacionContable
   eSM_ParametrosCiaWco
   eSM_ImpuestoSobreLaRenta
   eSM_AjustePorInflacion
   eSM_PeriodoRenglon
   eSM_EsquemaBalanceMov
   eSM_EsquemaBalance
   eSM_ParametrosConciliacion
   eSM_Revaluacion_Activo
   eSM_Producto
   eSM_CriterioDeDistribucion
   eSM_CriterioDeDistribucionDetalle
   eSM_ElementoDelCosto
End Enum

Enum enum_TipoNumeracionComprobante
   eTN_TIPOMESCONSECUTIVO = 0
   eTN_CONSECUTIVO
   eTN_MESCONSECUTIVO
End Enum

Enum enum_ComprobanteGeneradoPor
   eCG_USUARIO = 0
   eCG_APERTURA
   eCG_DEPRECIACION
   eCG_CIERRE_CUENTAS_RESULTADO
   eCG_CIERRE_INVENTARIO
   eCG_ADMINISTRATIVO
   eCG_IMPORTADO
   eCG_ACTIVOFIJO
   eCG_RESERVA
'Contabilizacion Automática
   eCG_CXC
   eCG_FACTURA
   eCG_CXP
   eCG_COBRANZA
   eCG_PAGOS
   eCG_MOVIMIENTO_BANCARIO
   eCG_RETIRO_ACTIVOFIJO
   eCG_CONCILIACION
   eCG_RESUMEN_DIARIO_VENTAS
   eCG_ANTICIPO
'Reconversion Monetaria
   eCG_RECONVERSION
'Contabilizacion Automática

'inventario'
   eCG_INVENTARIO
'Contabilizacion Automática
   eCG_ANULACION_COBRANZA
   eCG_ANULACION_PAGO
   eCG_PAGO_SUELDOS
   eCG_ANULACION_PAGO_SUELDOS
   eCG_REVALORIZACION_ACTIVO
   eCG_DEPRECIACION_REVALORIZACION
   eCG_ANULACION_MOVIMIENTO_BANCARIO
   eCG_RENDICION
   eCG_REPOSICION
   eCG_ANULACION_REPOSICION
   eCG_TRANSFERENCIA_ENTRE_CUENTAS_BANCARIA
   eCG_GANANCIA_PERDIDA_CAMBIARIA = 80
End Enum

Enum enum_NaturalezaDeLaCuenta
   eND_DEBE = 0
   eND_HABER
End Enum

Public Enum enum_GrupoAuxiliar
   eGA_CLIENTES = 0
   eGA_PROVEEDORES
   eGA_Personales
   eGA_SINGRUPO
End Enum

Enum enum_StatusComprobante
   eSC_CONTABILIZADO = 0
   eSC_NOCONTABILIZADO
   eSC_DESCUADRADO
End Enum

Enum enum_TipoAsientoModificable
   eTA_SOLODEBE = 0
   eTA_SOLOHABER
   eTA_TODOS
   eTA_NINGUNO
End Enum

Enum enum_SeccionEstadoDemostrativo
   eSE_INGRESOSPROPIOSACTIVIDAD = 0
   eSE_COSTODEVENTAS
   eSE_GASTOS
   eSE_SINCONEXION
End Enum

Enum enum_Conciliacion
   eCo_A45CONTRIBSOCIALESNOPAGADAS = 0
   eCo_A46RESERVASPROVISIONESAPARTA
   eCo_A47MULTASSANCIONESPECUNARIAS
   eCo_A48GASTOSDEDUCIDOSEJANTERIO
   eCo_A49INGRESOSEJANTCOBRADOS
   eCo_A50EXCEDENTESUELDODIRECTORES
   eCo_A51DEPRECIACIONAMORTIAZACION
   eCo_A52OTROS
   eCo_A55INGRESOSEXTRATERRITORIALE
   eCo_A56INGRESOCONTABNOCOBRADO
   eCo_A57GASTOEJERCANTPAGADOS
   eCo_A58DEPRECIACIONAMORTIZACION
End Enum

Enum enum_CostoDeVentas
   eCD_A12INVENTARIOINICIAL = 0
   eCD_A13COMPRASNETASNACIONALES
   eCD_A14COMPRASNETASEXTERIOR
   eCD_A17MENOSINVENTARIOFINAL
   eCD_A19INVINICIALMATERIASPRIMAS
   eCD_A20COMPRASNETASMATERIAPRIMA
   eCD_A21MENOSINVFINALMATPRIMAS
   eCD_A22MANODEOBRADIRECTA
   eCD_A24OTRASREMUNERACIONES
   eCD_A25CONTRIBUCIONES
   eCD_A26COMPRADEBIENESSERVICIOS
   eCD_A27INVINICPRODENPROCESO
   eCD_A28MENOSINVFINALPRODPROCES
   eCD_A29INVINICPRODUCTTERMINADOS
   eCD_A30MENOSINVFINALPRODTERMIN
   eCD_A32OTROSCOSTOSDEVENTAS
   eCD_A33COSTOSDESERVICIOS
End Enum

Enum enum_Gastos
   eGa_A35SUELDOSYSALARIOS = 0
   eGa_A36INTERESESSOBRECREDITOS
   eGa_A37CONRIBUCIONESSOCIALES
   eGa_A38PRESTACIONESSOCIALES
   eGa_A39GASTOSADMININMUEBLES
   eGa_A40GASTOSADMINREGALIAS
   eGa_A41DEPRECIACIONAMORTIZACION
   eGa_A42OTROSGASTOS
End Enum

Enum enum_IngresosPropiosActividad
   eIP_A1EXPORTACIONES = 0
   eIP_A2VENTASBRUTASSECTPUBLICO
   eIP_A3VENTASBRUTASSECTPRIVADO
   eIP_A4VENTASINMUEBLESACREDITO
   eIP_A5REGALIASNOMINERAS
   eIP_A6REGALIASMINERAS
   eIP_A7INTERESESSOBRECREDITOS
   eIP_A8ARRENDAMIENTOSSUBARRENDAM
   eIP_A9OTROSINGRESOS
   eIP_A10MENOSDEVOLUCIONESREBAJAS
End Enum

Enum enum_MotivoRetiro
   eMR_ENAJENACION = 0
   eMR_CONSUMO
   eMR_PERDIDA
   eMR_RECLASIFICACION
   eMR_OTRO
   eMR_NO_APLICA
End Enum

Enum enum_TipoReclasificacion
   eTR_NOESTARECLASIFICADO = 0
   eTR_ORIGEN
   eTR_DESTINO
End Enum

Enum enum_StatusCuenta
   eSC_NORMAL = 0
   eSC_ELIMINADA
   eSC_COMPRIMIDA
   eSC_EXPANDIDA
End Enum

Enum enum_GrupoDeCuentas
   eGC_Todas = 0
   eGC_Balance
   eGC_RESULTADO
End Enum

Enum enum_TipoDeComprobante
   eTDC_CHEQUES = 0
   eTDC_DEPOSITOS
   eTDC_CUENTASXCOBRAR
   eTDC_CUENTASXPAGAR
   eTDC_VENTAS_INGRESOS
   eTDC_GASTOS
   eTDC_APERTURA
   eTDC_CONCILIACION
   eTDC_CIERREDEINVENTARIO
   eTDC_RETIRODEACTIVOSFIJOS
   eTDC_COMPRADEACTIVOSFIJOS
   eTDC_DEPRECIACION
   eTDC_CIERREDEPERIODO
   eTDC_IMPORTACION
   eTDC_ADMINISTRATIVO
   eTDC_ANTICIPO
End Enum

Enum enum_CantidadParaImpresion
   eCPI_UNO = 0
   eCPI_ENTRENUMEROS
   eCPI_ENTREFECHAS
End Enum

Enum enum_VidaUtilExpresadaEn
   eVU_MESES = 0
   eVU_ANOS
   eVU_ANOSYMESES
End Enum

Enum enum_DepreciarDesde
   eDD_MESADQUISICION = 0
   eDD_MESSIGUIENTE
End Enum

Enum enum_CualesActivosImprimir
   eCA_VIGENTES = 0
   eCA_RETIRADOS
   eCA_ADQUISICIONESDELPERIODO
End Enum

Enum enum_TipoDeOperacionParaAuditar
   eTDA_INSERTAR = 0
   eTDA_MODIFICAR
   eTDA_ELIMINAR
   eTDA_RESINCRONIZAR
   eTDA_SQL
End Enum

Enum enum_TipoDeAsiento
   eTDA_NORMAL = 0
   eTDA_ACTIVOFIJO
   eTDA_CONCILIACION
End Enum

Enum enum_FormaDeValidarCentroDeCostos
   eFD_NO_VALIDAR = 0
   eFD_MOSTRAR_MENSAJE_Y_GUARDAR
   eFD_MOSTRAR_MENSAJE_Y_NO_GUARDAR
End Enum
Public Enum enum_Dpc10MetodoAjuste
   eD10MA_ACTIVOFIJO = 0
   eD10MA_MONEDAEXTRANJERA
   eD10MA_CLAUSULADEREAJUSTABILIDAD
   eD10MA_INVENTARIO
   eD10MA_PATRIMONIO
   Ed10ma_Obrasenproceso
   eD10MA_MONETARIA
   eD10MA_OTRASNOMONETARIAS
End Enum

Public Enum enum_MetodoAjuste
   eMA_PORIPC = 0
   eMA_MonedaExtranjera
   eMA_ClausuladeReajustabilidad
   eMA_Inventario
   eMA_Patrimonio
   Ema_Obrasenproceso
   eMA_Monetaria
   eMA_OtrasNoMonetarias
'   eMA_Titulo
End Enum
Public Enum enum_TipoDeCuentaDpc10
   ETDCD10_ACTIVO = 0
   Etdcd10_Pasivo
   Etdcd10_Patrimonio
End Enum

Public Enum enum_TipoDeCuentaDpc10Monetaria
   ETCM_COSTO
   Etcm_Otras
End Enum

Public Enum enum_TipoDeOtrasNoMonetarias
   eTDONM_Diferidos = 0
   Etdonm_Inversiones
   Etdonm_Prepagados
   Etdonm_Otras
End Enum

Public Enum enum_TipoDeInventario
   eTD_prima = 0
   Etd_Enproceso
   Etd_Terminados
End Enum

Public Enum enum_TipoPartidaAExportar
   Etpae_Todaslaspartidas = 0
   eTPAE_UnaPartida
End Enum
Public Enum enum_TipoCuenta
   eTC_Activo = 0
   eTC_Pasivo
   eTC_Capital
   eTC_Ingresos
   eTC_Costos
   eTC_Gasto
   eTC_OrdenDeudora
   eTC_OrdenAcreedora
End Enum

Public Enum enum_TipoDeCompraEspecial
   eTDCE_NoEs = 0
   eTDCE_Informativa
   eTDCE_SueldosySalarios
End Enum

Public Enum enum_ActividadFlujoEfectivo
   eAFE_Provisiones = 0
   eAFE_Inversion
   eAFE_Financieras
   eAFE_Efectivo
End Enum

Public Enum enum_TipoCuentaParaCosto
   eTCC_SinAsignar = 0
   eTCC_Ingreso
   eTCC_Costo
   eTCC_gasto
   eTCC_NoDeducible
End Enum

Enum enum_IngresosPropiosActividad2014
   eIP2014_1VentasExportacion = 0
   eIP2014_2VtasBienesSectorPublico
   eIP2014_3VtasBienesSectorPrivado
   eIP2014_4VtasInmueblesCredito
   eIP2014_5VtasInmueblesContado
   eIP2014_6ServiciosExportacion
   eIP2014_7ServiciosSectorPublico
   eIP2014_8ServiciosSectorPrivado
   eIP2014_9Honorarios
   eIP2014_10RegaliasNoMineras
   eIP2014_11RegaliasMineras
   eIP2014_12RegaliasHidrocarburos
   eIP2014_13InteresesSobreCreditos
   eIP2014_14InteresesVarios
   eIP2014_15IngresosVtaAcciones
   eIP2014_16EnriqConsorcios
   eIP2014_17ArrendBienesMuebles
   eIP2014_18ArrendBienesInmuebles
   eIP2014_19OtrosIngresos
   eIP2014_20IngresosSujetosIP
   eIP2014_21MenosDevolucionesRebajas
End Enum

Enum enum_CostoDeVentas2014
   eCD2014_23INVENTARIOINICIAL = 0
   eCD2014_24ComprasNetasNacionales
   eCD2014_25ComprasNetasExterior
   eCD2014_28InventarioFinal
   eCD2014_30InventarioInicialMP
   eCD2014_31ComprasNetasMP
   eCD2014_32InventarioFinalMP
   eCD2014_33ManoObraDirecta
   eCD2014_35OtrasRemuneraciones
   eCD2014_36Contribuciones
   eCD2014_37ComprasBienesServicios
   eCD2014_38InventarioInicialPP
   eCD2014_39InventarioFinalPP
   eCD2014_40InventarioInicialPT
   eCD2014_41InventarioFinalPT
   eCD2014_43CostoAdqBienesInmueblesVendidos
   eCD2014_44MejorasInmueblesVendidos
   eCD2014_45GastosRegistroAdqInmueblesVendidos
   eCD2014_46AxIAcumuladoVentaInmuebles
   eCD2014_48TotalCVAccionesVendidas
   eCD2014_49OtrosCostosVentas
   eCD2014_50CostosPrestaciónServicios
End Enum

Enum enum_Gastos2014
   eGa2014_52SueldosSalarios = 0
   eGa2014_53SueldosSalariosDirectivos
   eGa2014_54InteresesSobreCreditos
   eGa2014_55TributosPagados
   eGa2014_56IndemnizacionesLaborales
   eGa2014_57GastosAdmConsInmueblesArrendamiento
   eGa2014_58GastosAdmRegalíasNoMineras
   eGa2014_59DepreciacionesAmortizaciones
   eGa2014_60InteresesVarios
   eGa2014_61PerdidasSufridasBienes
   eGa2014_62GastosTrasladosNuevosEmpleados
   eGa2014_63PerdidaCuentasIncobrables
   eGa2014_64ReservasLey
   eGa2014_65CostosConstruccionesLot
   eGa2014_66ArrendamientoBienesMuebles
   eGa2014_67ArrendamientoBienesInmuebles
   eGa2014_68GastosTransporte
   eGa2014_69ComisionesVentaBienesInmuebles
   eGa2014_70ComisionesVarias
   eGa2014_71DerechosExhibicion
   eGa2014_72RegaliasParticipacionesAnalogas
   eGa2014_73HonorariosProfesionales
   eGa2014_74AsistenciaTecnicaServTecnologicos
   eGa2014_75GastosReparacionesOrdinarias
   eGa2014_76PublicidadPropaganda
   eGa2014_77PrimasSeguroBienesPersonas
   eGa2014_78GastosInvestigacionDesarrollo
   eGa2014_79GastosRepresentacion
   eGa2014_80GastosAdmRegaliasMineras
   eGa2014_81GastosAdmHidrocarburos
   eGa2014_82OtrosGastos
   eGa2014_85LiberalidadesDonacionesAutorizadas
End Enum

Enum enum_TipoDeMoneda
   eTM_Fisica = 0
   eTM_CriptoActivo
   eTM_Otras
End Enum

Enum enum_GenerarComprobanteDeDiferenciaCambiaria
   eGC_Mensual = 0
   eGc_Anual
   eGC_Nunca
End Enum

Enum enum_ExpresarBalancesEnMonedaExtrangera
   eGC_NoAplicar = 0
   eGc_ConDifCambiria
   eGC_ConGananciaPerdidaCambiaria
End Enum

Private Function CM_FILE_NAME() As String
   CM_FILE_NAME = "clsEnumContabilidad"
End Function

Private Function CM_MESSAGE_NAME() As String
   CM_MESSAGE_NAME = "Enumeratives Contabilidad"
End Function

Public Function enumTipoReflejaToString(TipoRefleja As enum_TipoRefleja) As String
   Select Case TipoRefleja
      Case eTR_NoAplicar_REFLEJA: enumTipoReflejaToString = "No  Aplicar"
      Case eTR_ORIGEN_REFLEJA: enumTipoReflejaToString = "Origen"
      Case eTR_DESTINO_REFLEJA: enumTipoReflejaToString = "Destino"
      Case Else
         enumTipoReflejaToString = enumTipoReflejaToString(0)
   End Select
End Function

Public Function strTipoReflejaToNum(ByVal TipoRefleja As String, Optional ByVal ShowMessage As Boolean = True) As Integer
   Dim mEnum As enum_TipoRefleja
   Dim nCount As Integer
   nCount = 0
   For mEnum = eTR_NoAplicar_REFLEJA To eTR_DESTINO_REFLEJA
      If UCase(TipoRefleja) = UCase(enumTipoReflejaToString(mEnum)) Then
         strTipoReflejaToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strTipoReflejaToNum = 0
   If ShowMessage Then
      gMessage.ProgrammerMessage "Valor " & TipoRefleja & " no existe en tipo enumerativo TipoRefleja"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithTipoRefleja(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   varComboBox.Clear
   nCount = 0
   If action = Buscar Then
      varComboBox.Text = gDefGen.getSearchAllComboBox()
      varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
      nCount = nCount + 1
   Else
      varComboBox.Text = enumTipoReflejaToString(enum_TipoRefleja.eTR_NoAplicar_REFLEJA)
   End If
   varComboBox.List(nCount) = enumTipoReflejaToString(enum_TipoRefleja.eTR_NoAplicar_REFLEJA)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumTipoReflejaToString(enum_TipoRefleja.eTR_ORIGEN_REFLEJA)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumTipoReflejaToString(enum_TipoRefleja.eTR_DESTINO_REFLEJA)
   varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
End Sub

Public Function enum_ContabilidadSystemModulesToStr(module As enum_ContabilidadSystemModules) As String
   Select Case module
      Case enum_ContabilidadSystemModules.eSM_Auxiliar: enum_ContabilidadSystemModulesToStr = "Auxiliar"
      Case enum_ContabilidadSystemModules.eSM_Periodo: enum_ContabilidadSystemModulesToStr = "Periodo"
      Case enum_ContabilidadSystemModules.eSM_Cuenta: enum_ContabilidadSystemModulesToStr = "Cuenta"
      Case enum_ContabilidadSystemModules.eSM_Comprobante: enum_ContabilidadSystemModulesToStr = "Comprobante"
      Case enum_ContabilidadSystemModules.eSM_Asiento: enum_ContabilidadSystemModulesToStr = "Asiento"
      Case enum_ContabilidadSystemModules.eSM_CentroDeCostos: enum_ContabilidadSystemModulesToStr = "Centro De Costos"
      Case enum_ContabilidadSystemModules.eSM_Lote: enum_ContabilidadSystemModulesToStr = "Lote"
      Case enum_ContabilidadSystemModules.eSM_UnaCuentaAuxiliarActivoFijo: enum_ContabilidadSystemModulesToStr = "Una Cuenta Auxiliar Activo Fijo"
      Case enum_ContabilidadSystemModules.eSM_UnGrupoAjuste: enum_ContabilidadSystemModulesToStr = "Un Grupo Ajuste"
      Case enum_ContabilidadSystemModules.eSM_MontoCuenta: enum_ContabilidadSystemModulesToStr = "Monto Cuenta"
      Case enum_ContabilidadSystemModules.eSM_DefCuenta: enum_ContabilidadSystemModulesToStr = "Definicion Cuenta"
      Case enum_ContabilidadSystemModules.eSM_GrupoDeInventario: enum_ContabilidadSystemModulesToStr = "Grupo de Inventario"
      Case enum_ContabilidadSystemModules.eSM_ParametrosContabilidad: enum_ContabilidadSystemModulesToStr = "Parámetros Contabilidad"
      Case enum_ContabilidadSystemModules.eSM_ParametrosActivoFijo: enum_ContabilidadSystemModulesToStr = "Parámetros Activo Fijo"
      Case enum_ContabilidadSystemModules.eSM_TipoDeComprobante: enum_ContabilidadSystemModulesToStr = "Tipo De Comprobante"
      Case enum_ContabilidadSystemModules.eSM_GrupoDeActivos: enum_ContabilidadSystemModulesToStr = "Grupo de Activos"
      Case enum_ContabilidadSystemModules.eSM_ActivoFijo: enum_ContabilidadSystemModulesToStr = "Activo Fijo"
      Case enum_ContabilidadSystemModules.eSM_SeguridadPorCompania: enum_ContabilidadSystemModulesToStr = "Seguridad Por Compañía"
      Case enum_ContabilidadSystemModules.eSM_AuditoriaDeProcesos: enum_ContabilidadSystemModulesToStr = "Auditoria De Procesos"
      Case enum_ContabilidadSystemModules.eSM_AuditoriaComprobante: enum_ContabilidadSystemModulesToStr = "Auditoria Comprobante"
      Case enum_ContabilidadSystemModules.eSM_ParametrosCiaWco: enum_ContabilidadSystemModulesToStr = "Parametros Cia Wco"
      Case enum_ContabilidadSystemModules.eSM_ImpuestoSobreLaRenta: enum_ContabilidadSystemModulesToStr = "I.S.L.R"
      Case enum_ContabilidadSystemModules.eSM_AjustePorInflacion: enum_ContabilidadSystemModulesToStr = "AXI"
      Case enum_ContabilidadSystemModules.eSM_Producto: enum_ContabilidadSystemModulesToStr = "Producto"
      Case enum_ContabilidadSystemModules.eSM_CriterioDeDistribucion: enum_ContabilidadSystemModulesToStr = "Criterios De Distribución"
      Case eSM_ElementoDelCosto: enum_ContabilidadSystemModulesToStr = "Elemento Del Costo"
   End Select
End Function

Public Function enumTipoNumeracionComprobanteToString(tipoNumeracionComprobante As enum_TipoNumeracionComprobante) As String
   Select Case tipoNumeracionComprobante
      Case eTN_TIPOMESCONSECUTIVO: enumTipoNumeracionComprobanteToString = "Tipo-Mes-Consecutivo"
      Case eTN_CONSECUTIVO: enumTipoNumeracionComprobanteToString = "Consecutivo"
      Case eTN_MESCONSECUTIVO: enumTipoNumeracionComprobanteToString = "Mes-Consecutivo"
      Case Else
      enumTipoNumeracionComprobanteToString = enumTipoNumeracionComprobanteToString(0)
   End Select
End Function

Public Function strTipoNumeracionComprobanteToNum(ByVal tipoNumeracionComprobante As String, Optional ByVal ShowMessage As Boolean = True) As Integer
   Dim mEnum As enum_TipoNumeracionComprobante
   Dim nCount As Integer
   nCount = 0
   For mEnum = eTN_TIPOMESCONSECUTIVO To eTN_MESCONSECUTIVO
   If UCase(tipoNumeracionComprobante) = UCase(enumTipoNumeracionComprobanteToString(mEnum)) Then
      strTipoNumeracionComprobanteToNum = nCount
      GoTo h_EXIT
   End If
   nCount = nCount + 1
   Next
   strTipoNumeracionComprobanteToNum = 0
   If ShowMessage Then
      gMessage.ProgrammerMessage "Valor " & tipoNumeracionComprobante & " no existe en tipo enumerativo tipoNumeracionComprobante"
   End If
h_EXIT:
   Exit Function
End Function

Public Function enumStringToTipoNumeracion(ByVal mTipoNumeracion As String) As enum_TipoNumeracionComprobante
   If mTipoNumeracion = "Tipo-Mes-Consecutivo" Then
      enumStringToTipoNumeracion = eTN_TIPOMESCONSECUTIVO
   ElseIf mTipoNumeracion = "Consecutivo" Then
      enumStringToTipoNumeracion = eTN_CONSECUTIVO
   Else
      enumStringToTipoNumeracion = eTN_MESCONSECUTIVO ' "Mes-Consecutivo"
   End If
End Function

Public Sub FillComboBoxWithTipoNumeracionComprobante(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   varComboBox.Clear
   nCount = 0
   If action = Buscar Then
      varComboBox.Text = gDefGen.getSearchAllComboBox()
      varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
      nCount = nCount + 1
   Else
      varComboBox.Text = enumTipoNumeracionComprobanteToString(enum_TipoNumeracionComprobante.eTN_TIPOMESCONSECUTIVO)
   End If
   varComboBox.List(nCount) = enumTipoNumeracionComprobanteToString(enum_TipoNumeracionComprobante.eTN_TIPOMESCONSECUTIVO)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumTipoNumeracionComprobanteToString(enum_TipoNumeracionComprobante.eTN_CONSECUTIVO)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumTipoNumeracionComprobanteToString(enum_TipoNumeracionComprobante.eTN_MESCONSECUTIVO)
   varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
   varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
End Sub

Public Function enumComprobanteGeneradoPorToString(comprobanteGeneradoPor As enum_ComprobanteGeneradoPor) As String
   Select Case comprobanteGeneradoPor
      Case eCG_USUARIO: enumComprobanteGeneradoPorToString = "Usuario"
      Case eCG_APERTURA: enumComprobanteGeneradoPorToString = "Apertura"
      Case eCG_DEPRECIACION: enumComprobanteGeneradoPorToString = "Depreciación"
      Case eCG_CIERRE_CUENTAS_RESULTADO: enumComprobanteGeneradoPorToString = "Cierre Ctas. de Resultado"
      Case eCG_CIERRE_INVENTARIO: enumComprobanteGeneradoPorToString = "Cierre de Inventario"
      Case eCG_ADMINISTRATIVO: enumComprobanteGeneradoPorToString = "Administrativo"
      Case eCG_IMPORTADO: enumComprobanteGeneradoPorToString = "Importado"
      Case eCG_ACTIVOFIJO: enumComprobanteGeneradoPorToString = "Activo Fijo"
      Case eCG_RESERVA: enumComprobanteGeneradoPorToString = "Reserva"
      Case eCG_RETIRO_ACTIVOFIJO: enumComprobanteGeneradoPorToString = "Retiro de Activo"
      Case eCG_CONCILIACION: enumComprobanteGeneradoPorToString = "Conciliación"
      Case eCG_RECONVERSION: enumComprobanteGeneradoPorToString = "Reconversion Monetaria"
      Case eCG_CXC: enumComprobanteGeneradoPorToString = "CxC"
      Case eCG_FACTURA: enumComprobanteGeneradoPorToString = "Factura"
      Case eCG_CXP: enumComprobanteGeneradoPorToString = "CxP"
      Case eCG_COBRANZA: enumComprobanteGeneradoPorToString = "Cobranza"
      Case eCG_PAGOS: enumComprobanteGeneradoPorToString = "Pagos"
      Case eCG_MOVIMIENTO_BANCARIO: enumComprobanteGeneradoPorToString = "Movimiento Bancario"
      Case eCG_RESUMEN_DIARIO_VENTAS: enumComprobanteGeneradoPorToString = "Resumen Diario de Ventas"
      Case eCG_ANTICIPO: enumComprobanteGeneradoPorToString = "Anticipo"
      Case eCG_INVENTARIO: enumComprobanteGeneradoPorToString = "Inventario"
      Case eCG_ANULACION_COBRANZA: enumComprobanteGeneradoPorToString = "Anulación de Cobranza"
      Case eCG_ANULACION_PAGO: enumComprobanteGeneradoPorToString = "Anulación de Pago"
      Case eCG_PAGO_SUELDOS: enumComprobanteGeneradoPorToString = "Pago Sueldos"
      Case eCG_ANULACION_PAGO_SUELDOS: enumComprobanteGeneradoPorToString = "Anulación de Pago Sueldos"
      Case eCG_ANULACION_MOVIMIENTO_BANCARIO: enumComprobanteGeneradoPorToString = "Anulación de Movimiento Bancario"
      Case eCG_RENDICION: enumComprobanteGeneradoPorToString = "Rendición"
      Case eCG_REPOSICION: enumComprobanteGeneradoPorToString = "Reposición de Caja Chica"
      Case eCG_ANULACION_REPOSICION: enumComprobanteGeneradoPorToString = "Anulación de Reposición de Caja Chica"
      Case eCG_TRANSFERENCIA_ENTRE_CUENTAS_BANCARIA: enumComprobanteGeneradoPorToString = "Transferencia entre Cuentas Bancarias"
      Case eCG_GANANCIA_PERDIDA_CAMBIARIA: enumComprobanteGeneradoPorToString = "Ganancia/Pérdida Cambiaria"
      Case Else: enumComprobanteGeneradoPorToString = enumComprobanteGeneradoPorToString(0)
   End Select
End Function

Public Function enumComprobanteGeneradoPorToStringCorto(comprobanteGeneradoPor As enum_ComprobanteGeneradoPor) As String
   Select Case comprobanteGeneradoPor
      Case eCG_MOVIMIENTO_BANCARIO: enumComprobanteGeneradoPorToStringCorto = "Mov.Bancario"
      Case Else: enumComprobanteGeneradoPorToStringCorto = enumComprobanteGeneradoPorToString(comprobanteGeneradoPor)
   End Select
End Function

Public Function strComprobanteGeneradoPorToNum(ByVal comprobanteGeneradoPor As String, Optional ByVal ShowMessage As Boolean = True) As Integer
   Dim mEnum As enum_ComprobanteGeneradoPor
   Dim nCount As Integer
   nCount = 0
   For mEnum = eCG_USUARIO To eCG_GANANCIA_PERDIDA_CAMBIARIA
   If UCase(comprobanteGeneradoPor) = UCase(enumComprobanteGeneradoPorToString(mEnum)) Then
      strComprobanteGeneradoPorToNum = nCount
      GoTo h_EXIT
   End If
   nCount = nCount + 1
   Next
   strComprobanteGeneradoPorToNum = 0
   If ShowMessage Then
      gMessage.ProgrammerMessage "Valor " & comprobanteGeneradoPor & " no existe en tipo enumerativo comprobanteGeneradoPor"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithComprobanteGeneradoPor(ByRef varComboBox As Object, ByVal valApartirDe As enum_ComprobanteGeneradoPor, _
                  Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   varComboBox.Clear
   nCount = 0
   If action = Buscar Then
      varComboBox.Text = gDefGen.getSearchAllComboBox()
      varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
      nCount = nCount + 1
   Else
      varComboBox.Text = enumComprobanteGeneradoPorToString(enum_ComprobanteGeneradoPor.eCG_USUARIO)
   End If
   If valApartirDe = eCG_CXC Then
      GoTo A_Partir_De_Aqui:
   End If
   varComboBox.List(nCount) = enumComprobanteGeneradoPorToString(enum_ComprobanteGeneradoPor.eCG_USUARIO)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumComprobanteGeneradoPorToString(enum_ComprobanteGeneradoPor.eCG_APERTURA)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumComprobanteGeneradoPorToString(enum_ComprobanteGeneradoPor.eCG_DEPRECIACION)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumComprobanteGeneradoPorToString(enum_ComprobanteGeneradoPor.eCG_CIERRE_CUENTAS_RESULTADO)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumComprobanteGeneradoPorToString(enum_ComprobanteGeneradoPor.eCG_CIERRE_INVENTARIO)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumComprobanteGeneradoPorToString(enum_ComprobanteGeneradoPor.eCG_ADMINISTRATIVO)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumComprobanteGeneradoPorToString(enum_ComprobanteGeneradoPor.eCG_IMPORTADO)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumComprobanteGeneradoPorToString(enum_ComprobanteGeneradoPor.eCG_ACTIVOFIJO)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumComprobanteGeneradoPorToString(enum_ComprobanteGeneradoPor.eCG_RESERVA)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumComprobanteGeneradoPorToString(enum_ComprobanteGeneradoPor.eCG_RETIRO_ACTIVOFIJO)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumComprobanteGeneradoPorToString(enum_ComprobanteGeneradoPor.eCG_CONCILIACION)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumComprobanteGeneradoPorToString(enum_ComprobanteGeneradoPor.eCG_RECONVERSION)
   nCount = nCount + 1
A_Partir_De_Aqui:
   varComboBox.List(nCount) = enumComprobanteGeneradoPorToString(enum_ComprobanteGeneradoPor.eCG_CXC)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumComprobanteGeneradoPorToString(enum_ComprobanteGeneradoPor.eCG_FACTURA)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumComprobanteGeneradoPorToString(enum_ComprobanteGeneradoPor.eCG_CXP)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumComprobanteGeneradoPorToString(enum_ComprobanteGeneradoPor.eCG_COBRANZA)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumComprobanteGeneradoPorToString(enum_ComprobanteGeneradoPor.eCG_PAGOS)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumComprobanteGeneradoPorToString(enum_ComprobanteGeneradoPor.eCG_MOVIMIENTO_BANCARIO)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumComprobanteGeneradoPorToString(enum_ComprobanteGeneradoPor.eCG_RESUMEN_DIARIO_VENTAS)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumComprobanteGeneradoPorToString(enum_ComprobanteGeneradoPor.eCG_ANTICIPO)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumComprobanteGeneradoPorToString(enum_ComprobanteGeneradoPor.eCG_RENDICION)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumComprobanteGeneradoPorToString(enum_ComprobanteGeneradoPor.eCG_INVENTARIO)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumComprobanteGeneradoPorToString(enum_ComprobanteGeneradoPor.eCG_ANULACION_COBRANZA)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumComprobanteGeneradoPorToString(enum_ComprobanteGeneradoPor.eCG_ANULACION_PAGO)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumComprobanteGeneradoPorToString(enum_ComprobanteGeneradoPor.eCG_PAGO_SUELDOS)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumComprobanteGeneradoPorToString(enum_ComprobanteGeneradoPor.eCG_ANULACION_PAGO_SUELDOS)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumComprobanteGeneradoPorToString(enum_ComprobanteGeneradoPor.eCG_ANULACION_MOVIMIENTO_BANCARIO)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumComprobanteGeneradoPorToString(enum_ComprobanteGeneradoPor.eCG_REPOSICION)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumComprobanteGeneradoPorToString(enum_ComprobanteGeneradoPor.eCG_ANULACION_REPOSICION)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumComprobanteGeneradoPorToString(enum_ComprobanteGeneradoPor.eCG_TRANSFERENCIA_ENTRE_CUENTAS_BANCARIA)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumComprobanteGeneradoPorToString(enum_ComprobanteGeneradoPor.eCG_GANANCIA_PERDIDA_CAMBIARIA)
   varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
   varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
End Sub

Public Sub FillComboBoxWithComprobanteGeneradoPorSinComprobanteDeApertura(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   nCount = 0
   If action = Buscar Then
      varComboBox.Text = gDefGen.getSearchAllComboBox()
      varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
      nCount = nCount + 1
   Else
      varComboBox.Text = enumComprobanteGeneradoPorToString(enum_ComprobanteGeneradoPor.eCG_USUARIO)
   End If
   varComboBox.List(nCount) = enumComprobanteGeneradoPorToString(enum_ComprobanteGeneradoPor.eCG_USUARIO)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumComprobanteGeneradoPorToString(enum_ComprobanteGeneradoPor.eCG_APERTURA)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumComprobanteGeneradoPorToString(enum_ComprobanteGeneradoPor.eCG_DEPRECIACION)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumComprobanteGeneradoPorToString(enum_ComprobanteGeneradoPor.eCG_CIERRE_CUENTAS_RESULTADO)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumComprobanteGeneradoPorToString(enum_ComprobanteGeneradoPor.eCG_CIERRE_INVENTARIO)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumComprobanteGeneradoPorToString(enum_ComprobanteGeneradoPor.eCG_ADMINISTRATIVO)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumComprobanteGeneradoPorToString(enum_ComprobanteGeneradoPor.eCG_IMPORTADO)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumComprobanteGeneradoPorToString(enum_ComprobanteGeneradoPor.eCG_ACTIVOFIJO)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumComprobanteGeneradoPorToString(enum_ComprobanteGeneradoPor.eCG_RESERVA)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumComprobanteGeneradoPorToString(enum_ComprobanteGeneradoPor.eCG_RETIRO_ACTIVOFIJO)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumComprobanteGeneradoPorToString(enum_ComprobanteGeneradoPor.eCG_CONCILIACION)
   If gGlobalization.fEsCodigoVenezuela Then
   	nCount = nCount + 1
   	varComboBox.List(nCount) = enumComprobanteGeneradoPorToString(enum_ComprobanteGeneradoPor.eCG_RECONVERSION)
   End If
   nCount = nCount + 1
   varComboBox.List(nCount) = enumComprobanteGeneradoPorToString(enum_ComprobanteGeneradoPor.eCG_CXC)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumComprobanteGeneradoPorToString(enum_ComprobanteGeneradoPor.eCG_FACTURA)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumComprobanteGeneradoPorToString(enum_ComprobanteGeneradoPor.eCG_CXP)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumComprobanteGeneradoPorToString(enum_ComprobanteGeneradoPor.eCG_COBRANZA)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumComprobanteGeneradoPorToString(enum_ComprobanteGeneradoPor.eCG_PAGOS)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumComprobanteGeneradoPorToString(enum_ComprobanteGeneradoPor.eCG_MOVIMIENTO_BANCARIO)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumComprobanteGeneradoPorToString(enum_ComprobanteGeneradoPor.eCG_RESUMEN_DIARIO_VENTAS)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumComprobanteGeneradoPorToString(enum_ComprobanteGeneradoPor.eCG_ANTICIPO)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumComprobanteGeneradoPorToString(enum_ComprobanteGeneradoPor.eCG_RENDICION)
   If gGlobalization.fEsCodigoVenezuela Then
   	nCount = nCount + 1
   	varComboBox.List(nCount) = enumComprobanteGeneradoPorToString(enum_ComprobanteGeneradoPor.eCG_INVENTARIO)
   End If
   nCount = nCount + 1
   varComboBox.List(nCount) = enumComprobanteGeneradoPorToString(enum_ComprobanteGeneradoPor.eCG_ANULACION_COBRANZA)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumComprobanteGeneradoPorToString(enum_ComprobanteGeneradoPor.eCG_ANULACION_PAGO)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumComprobanteGeneradoPorToString(enum_ComprobanteGeneradoPor.eCG_PAGO_SUELDOS)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumComprobanteGeneradoPorToString(enum_ComprobanteGeneradoPor.eCG_ANULACION_PAGO_SUELDOS)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumComprobanteGeneradoPorToString(enum_ComprobanteGeneradoPor.eCG_ANULACION_MOVIMIENTO_BANCARIO)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumComprobanteGeneradoPorToString(enum_ComprobanteGeneradoPor.eCG_REPOSICION)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumComprobanteGeneradoPorToString(enum_ComprobanteGeneradoPor.eCG_ANULACION_REPOSICION)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumComprobanteGeneradoPorToString(enum_ComprobanteGeneradoPor.eCG_TRANSFERENCIA_ENTRE_CUENTAS_BANCARIA)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumComprobanteGeneradoPorToString(enum_ComprobanteGeneradoPor.eCG_GANANCIA_PERDIDA_CAMBIARIA)
   varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
   varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
End Sub

Public Function enumNaturalezaDeLaCuentaToString(naturalezaDeLaCuenta As enum_NaturalezaDeLaCuenta) As String
   Select Case naturalezaDeLaCuenta
      Case eND_DEBE: enumNaturalezaDeLaCuentaToString = "Debe"
      Case eND_HABER: enumNaturalezaDeLaCuentaToString = "Haber"
      Case Else: enumNaturalezaDeLaCuentaToString = enumNaturalezaDeLaCuentaToString(0)
   End Select
End Function

Public Function strNaturalezaDeLaCuentaToNum(ByVal naturalezaDeLaCuenta As String, _
                  Optional ByVal ShowMessage As Boolean = True) As Integer
   Dim mEnum As enum_NaturalezaDeLaCuenta
   Dim nCount As Integer
   nCount = 0
   For mEnum = eND_DEBE To eND_HABER
      If UCase(naturalezaDeLaCuenta) = UCase(enumNaturalezaDeLaCuentaToString(mEnum)) Then
         strNaturalezaDeLaCuentaToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strNaturalezaDeLaCuentaToNum = 0
   If ShowMessage Then
         gMessage.ProgrammerMessage "Valor " & naturalezaDeLaCuenta & " no existe en tipo enumerativo NaturalezaDeLaCuenta"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithNaturalezaDeLaCuenta(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   varComboBox.Clear
   nCount = 0
   If action = Buscar Then
      varComboBox.Text = gDefGen.getSearchAllComboBox()
      varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
      nCount = nCount + 1
   Else
      varComboBox.Text = enumNaturalezaDeLaCuentaToString(enum_NaturalezaDeLaCuenta.eND_DEBE)
   End If
   varComboBox.List(nCount) = enumNaturalezaDeLaCuentaToString(enum_NaturalezaDeLaCuenta.eND_DEBE)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumNaturalezaDeLaCuentaToString(enum_NaturalezaDeLaCuenta.eND_HABER)
   varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
   varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
End Sub

Public Function enumGrupoAuxiliarToString(grupoAuxiliar As enum_GrupoAuxiliar) As String
   Select Case grupoAuxiliar
      Case eGA_CLIENTES: enumGrupoAuxiliarToString = "Clientes"
      Case eGA_PROVEEDORES: enumGrupoAuxiliarToString = "Proveedores"
      Case eGA_Personales: enumGrupoAuxiliarToString = "Personales"
      Case eGA_SINGRUPO: enumGrupoAuxiliarToString = "Sin Grupo"
      Case Else
      enumGrupoAuxiliarToString = enumGrupoAuxiliarToString(0)
   End Select
End Function

Public Function strGrupoAuxiliarToNum(ByVal grupoAuxiliar As String, Optional ByVal ShowMessage As Boolean = True) As Integer
   Dim mEnum As enum_GrupoAuxiliar
   Dim nCount As Integer
   nCount = 0
   For mEnum = eGA_CLIENTES To eGA_SINGRUPO
   If UCase(grupoAuxiliar) = UCase(enumGrupoAuxiliarToString(mEnum)) Then
      strGrupoAuxiliarToNum = nCount
      GoTo h_EXIT
   End If
   nCount = nCount + 1
   Next
   strGrupoAuxiliarToNum = 0
   If ShowMessage Then
      gMessage.ProgrammerMessage "Valor " & grupoAuxiliar & " no existe en tipo enumerativo grupoAuxiliar"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithGrupoAuxiliar(ByRef varComboBox As Object, _
         Optional action As AccionSobreRecord = Abrir, Optional ByVal AgregarSinGrupo As Boolean = False)
   Dim nCount As Integer
   varComboBox.Clear
   nCount = 0
   If action = Buscar Then
      varComboBox.Text = gDefGen.getSearchAllComboBox()
      varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
      nCount = nCount + 1
   Else
      varComboBox.Text = enumGrupoAuxiliarToString(enum_GrupoAuxiliar.eGA_CLIENTES)
   End If
   varComboBox.List(nCount) = enumGrupoAuxiliarToString(enum_GrupoAuxiliar.eGA_CLIENTES)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumGrupoAuxiliarToString(enum_GrupoAuxiliar.eGA_PROVEEDORES)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumGrupoAuxiliarToString(enum_GrupoAuxiliar.eGA_Personales)
   If AgregarSinGrupo Then
      nCount = nCount + 1
      varComboBox.List(nCount) = enumGrupoAuxiliarToString(enum_GrupoAuxiliar.eGA_SINGRUPO)
   End If
   varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
   varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
End Sub

Public Function enumStatusComprobanteToString(statusComprobante As enum_StatusComprobante) As String
   Select Case statusComprobante
      Case eSC_CONTABILIZADO: enumStatusComprobanteToString = "Contabilizado"
      Case eSC_NOCONTABILIZADO: enumStatusComprobanteToString = "No Contabilizado"
      Case eSC_DESCUADRADO: enumStatusComprobanteToString = "Descuadrado"
      Case Else: enumStatusComprobanteToString = enumStatusComprobanteToString(0)
   End Select
End Function

Public Function strStatusComprobanteToNum(ByVal statusComprobante As String, Optional ByVal ShowMessage As Boolean = True) As Integer
   Dim mEnum As enum_StatusComprobante
   Dim nCount As Integer
   nCount = 0
   For mEnum = eSC_CONTABILIZADO To eSC_DESCUADRADO
   If UCase(statusComprobante) = UCase(enumStatusComprobanteToString(mEnum)) Then
      strStatusComprobanteToNum = nCount
      GoTo h_EXIT
   End If
   nCount = nCount + 1
   Next
   strStatusComprobanteToNum = 0
   If ShowMessage Then
      gMessage.ProgrammerMessage "Valor " & statusComprobante & " no existe en tipo enumerativo statusComprobante"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithStatusComprobante(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   varComboBox.Clear
   nCount = 0
   If action = Buscar Then
      varComboBox.Text = gDefGen.getSearchAllComboBox()
      varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
      nCount = nCount + 1
   Else
      varComboBox.Text = enumStatusComprobanteToString(enum_StatusComprobante.eSC_CONTABILIZADO)
   End If
   varComboBox.List(nCount) = enumStatusComprobanteToString(enum_StatusComprobante.eSC_CONTABILIZADO)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumStatusComprobanteToString(enum_StatusComprobante.eSC_DESCUADRADO)
   varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
   varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
End Sub

Public Function enumTipoAsientoModificableToString(tipoAsientoModificable As enum_TipoAsientoModificable) As String
   Select Case tipoAsientoModificable
      Case eTA_SOLODEBE: enumTipoAsientoModificableToString = "Solo Debe"
      Case eTA_SOLOHABER: enumTipoAsientoModificableToString = "Solo Haber"
      Case eTA_TODOS: enumTipoAsientoModificableToString = "Todos"
      Case eTA_NINGUNO: enumTipoAsientoModificableToString = "Ninguno"
      Case Else: enumTipoAsientoModificableToString = enumTipoAsientoModificableToString(0)
   End Select
End Function

Public Function strTipoAsientoModificableToNum(ByVal tipoAsientoModificable As String, Optional ByVal ShowMessage As Boolean = True) As Integer
   Dim mEnum As enum_TipoAsientoModificable
   Dim nCount As Integer
   nCount = 0
   For mEnum = eTA_SOLODEBE To eTA_NINGUNO
   If UCase(tipoAsientoModificable) = UCase(enumTipoAsientoModificableToString(mEnum)) Then
      strTipoAsientoModificableToNum = nCount
      GoTo h_EXIT
   End If
   nCount = nCount + 1
   Next
   strTipoAsientoModificableToNum = 0
   If ShowMessage Then
      gMessage.ProgrammerMessage "Valor " & tipoAsientoModificable & " no existe en tipo enumerativo tipoAsientoModificable"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithTipoAsientoModificable(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   varComboBox.Clear
   nCount = 0
   If action = Buscar Then
      varComboBox.Text = gDefGen.getSearchAllComboBox()
      varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
      nCount = nCount + 1
   Else
      varComboBox.Text = enumTipoAsientoModificableToString(enum_TipoAsientoModificable.eTA_SOLODEBE)
   End If
   varComboBox.List(nCount) = enumTipoAsientoModificableToString(enum_TipoAsientoModificable.eTA_SOLODEBE)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumTipoAsientoModificableToString(enum_TipoAsientoModificable.eTA_SOLOHABER)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumTipoAsientoModificableToString(enum_TipoAsientoModificable.eTA_TODOS)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumTipoAsientoModificableToString(enum_TipoAsientoModificable.eTA_NINGUNO)
   varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
   varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
End Sub


Public Function enumSeccionEstadoDemostrativoToString(seccionEstadoDemostrativo As enum_SeccionEstadoDemostrativo) As String
   Select Case seccionEstadoDemostrativo
      Case eSE_INGRESOSPROPIOSACTIVIDAD: enumSeccionEstadoDemostrativoToString = "Ingresos Propios de la Actividad"
      Case eSE_COSTODEVENTAS: enumSeccionEstadoDemostrativoToString = "Costo De Ventas"
      Case eSE_GASTOS: enumSeccionEstadoDemostrativoToString = "Gastos"
      Case eSE_SINCONEXION: enumSeccionEstadoDemostrativoToString = "Sin Conexión"
      Case Else: enumSeccionEstadoDemostrativoToString = enumSeccionEstadoDemostrativoToString(0)
   End Select
End Function

Public Function strSeccionEstadoDemostrativoToNum(ByVal seccionEstadoDemostrativo As String, Optional ByVal ShowMessage As Boolean = True) As Integer
   Dim mEnum As enum_SeccionEstadoDemostrativo
   Dim nCount As Integer
   nCount = 0
   For mEnum = eSE_INGRESOSPROPIOSACTIVIDAD To eSE_SINCONEXION
   If UCase(seccionEstadoDemostrativo) = UCase(enumSeccionEstadoDemostrativoToString(mEnum)) Then
      strSeccionEstadoDemostrativoToNum = nCount
      GoTo h_EXIT
   End If
   nCount = nCount + 1
   Next
   strSeccionEstadoDemostrativoToNum = 0
   If ShowMessage Then
      gMessage.ProgrammerMessage "Valor " & seccionEstadoDemostrativo & " no existe en tipo enumerativo seccionEstadoDemostrativo"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithSeccionEstadoDemostrativo(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   varComboBox.Clear
   nCount = 0
   If action = Buscar Then
      varComboBox.Text = gDefGen.getSearchAllComboBox()
      varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
      nCount = nCount + 1
   Else
      varComboBox.Text = enumSeccionEstadoDemostrativoToString(enum_SeccionEstadoDemostrativo.eSE_INGRESOSPROPIOSACTIVIDAD)
   End If
   varComboBox.List(nCount) = enumSeccionEstadoDemostrativoToString(enum_SeccionEstadoDemostrativo.eSE_INGRESOSPROPIOSACTIVIDAD)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumSeccionEstadoDemostrativoToString(enum_SeccionEstadoDemostrativo.eSE_COSTODEVENTAS)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumSeccionEstadoDemostrativoToString(enum_SeccionEstadoDemostrativo.eSE_GASTOS)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumSeccionEstadoDemostrativoToString(enum_SeccionEstadoDemostrativo.eSE_SINCONEXION)
   varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
   varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
End Sub

Public Function enumConciliacionToString(conciliacion As enum_Conciliacion) As String
   Select Case conciliacion
      Case eCo_A45CONTRIBSOCIALESNOPAGADAS: enumConciliacionToString = "45. Contribuciones Sociales No Pagadas"
      Case eCo_A46RESERVASPROVISIONESAPARTA: enumConciliacionToString = "46. Reservas Provisiones, Apart."
      Case eCo_A47MULTASSANCIONESPECUNARIAS: enumConciliacionToString = "47. Multas/Sanciones Pecunarias"
      Case eCo_A48GASTOSDEDUCIDOSEJANTERIO: enumConciliacionToString = "48. Gastos Deducidos Ej. Anterior"
      Case eCo_A49INGRESOSEJANTCOBRADOS: enumConciliacionToString = "49. Ingresos Ej. Ant. Cobrados"
      Case eCo_A50EXCEDENTESUELDODIRECTORES: enumConciliacionToString = "50. Excedente Sueldo Directores"
      Case eCo_A51DEPRECIACIONAMORTIAZACION: enumConciliacionToString = "51. Depreciación / Amortización"
      Case eCo_A52OTROS: enumConciliacionToString = "52. Otros"
      Case eCo_A55INGRESOSEXTRATERRITORIALE: enumConciliacionToString = "55. Ingresos Extraterritoriales"
      Case eCo_A56INGRESOCONTABNOCOBRADO: enumConciliacionToString = "56. Ingreso Contab. No Cobrado"
      Case eCo_A57GASTOEJERCANTPAGADOS: enumConciliacionToString = "57. Gastos Ejerc. Ant. Pagados"
      Case eCo_A58DEPRECIACIONAMORTIZACION: enumConciliacionToString = "58. Depreciación / Amortización"
      Case Else: enumConciliacionToString = enumConciliacionToString(0)
   End Select
End Function

Public Function strConciliacionToNum(ByVal conciliacion As String, Optional ByVal ShowMessage As Boolean = True) As Integer
   Dim mEnum As enum_Conciliacion
   Dim nCount As Integer
   nCount = 0
   For mEnum = eCo_A45CONTRIBSOCIALESNOPAGADAS To eCo_A58DEPRECIACIONAMORTIZACION
   If UCase(conciliacion) = UCase(enumConciliacionToString(mEnum)) Then
      strConciliacionToNum = nCount
      GoTo h_EXIT
   End If
   nCount = nCount + 1
   Next
   strConciliacionToNum = 0
   If ShowMessage Then
      gMessage.ProgrammerMessage "Valor " & conciliacion & " no existe en tipo enumerativo conciliacion"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithConciliacion(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   varComboBox.Clear
   nCount = 0
   If action = Buscar Then
      varComboBox.Text = gDefGen.getSearchAllComboBox()
      varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
      nCount = nCount + 1
   Else
      varComboBox.Text = enumConciliacionToString(enum_Conciliacion.eCo_A45CONTRIBSOCIALESNOPAGADAS)
   End If
   varComboBox.List(nCount) = enumConciliacionToString(enum_Conciliacion.eCo_A45CONTRIBSOCIALESNOPAGADAS)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumConciliacionToString(enum_Conciliacion.eCo_A46RESERVASPROVISIONESAPARTA)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumConciliacionToString(enum_Conciliacion.eCo_A47MULTASSANCIONESPECUNARIAS)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumConciliacionToString(enum_Conciliacion.eCo_A48GASTOSDEDUCIDOSEJANTERIO)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumConciliacionToString(enum_Conciliacion.eCo_A49INGRESOSEJANTCOBRADOS)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumConciliacionToString(enum_Conciliacion.eCo_A50EXCEDENTESUELDODIRECTORES)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumConciliacionToString(enum_Conciliacion.eCo_A51DEPRECIACIONAMORTIAZACION)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumConciliacionToString(enum_Conciliacion.eCo_A52OTROS)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumConciliacionToString(enum_Conciliacion.eCo_A55INGRESOSEXTRATERRITORIALE)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumConciliacionToString(enum_Conciliacion.eCo_A56INGRESOCONTABNOCOBRADO)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumConciliacionToString(enum_Conciliacion.eCo_A57GASTOEJERCANTPAGADOS)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumConciliacionToString(enum_Conciliacion.eCo_A58DEPRECIACIONAMORTIZACION)
   varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
   varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
End Sub

Public Function enumCostoDeVentasToString(costoDeVentas As enum_CostoDeVentas) As String
   Select Case costoDeVentas
      Case eCD_A12INVENTARIOINICIAL: enumCostoDeVentasToString = "12. Inventario Inicial"
      Case eCD_A13COMPRASNETASNACIONALES: enumCostoDeVentasToString = "13. Compras Netas Nacionales"
      Case eCD_A14COMPRASNETASEXTERIOR: enumCostoDeVentasToString = "14. Compras Netas Exterior"
      Case eCD_A17MENOSINVENTARIOFINAL: enumCostoDeVentasToString = "17. MENOS: Inventario Final"
      Case eCD_A19INVINICIALMATERIASPRIMAS: enumCostoDeVentasToString = "19. Inv. Inicial Materias Primas"
      Case eCD_A20COMPRASNETASMATERIAPRIMA: enumCostoDeVentasToString = "20. Compras Netas Materia Prima"
      Case eCD_A21MENOSINVFINALMATPRIMAS: enumCostoDeVentasToString = "21. MENOS: Inv. Final Materias Primas"
      Case eCD_A22MANODEOBRADIRECTA: enumCostoDeVentasToString = "22. Mano de Obra Directa"
      Case eCD_A24OTRASREMUNERACIONES: enumCostoDeVentasToString = "24. Otras Remuneraciones"
      Case eCD_A25CONTRIBUCIONES: enumCostoDeVentasToString = "25. Contribuciones"
      Case eCD_A26COMPRADEBIENESSERVICIOS: enumCostoDeVentasToString = "26. Compra de Bienes / Servicios"
      Case eCD_A27INVINICPRODENPROCESO: enumCostoDeVentasToString = "27. Inv. Inicial Productos en Proceso"
      Case eCD_A28MENOSINVFINALPRODPROCES: enumCostoDeVentasToString = "28. MENOS: Inv. Final Prod. en Proceso"
      Case eCD_A29INVINICPRODUCTTERMINADOS: enumCostoDeVentasToString = "29. Inv. Inicial Productos Terminados"
      Case eCD_A30MENOSINVFINALPRODTERMIN: enumCostoDeVentasToString = "30. MENOS: Inv. Final Prod. Terminados"
      Case eCD_A32OTROSCOSTOSDEVENTAS: enumCostoDeVentasToString = "32. Otros Costos de Ventas"
      Case eCD_A33COSTOSDESERVICIOS: enumCostoDeVentasToString = "33. Costos de Servicios"
      Case Else: enumCostoDeVentasToString = enumCostoDeVentasToString(0)
   End Select
End Function

Public Function strCostoDeVentasToNum(ByVal costoDeVentas As String, Optional ByVal ShowMessage As Boolean = True) As Integer
   Dim mEnum As enum_CostoDeVentas
   Dim nCount As Integer
   nCount = 0
   For mEnum = eCD_A12INVENTARIOINICIAL To eCD_A33COSTOSDESERVICIOS
   If UCase(costoDeVentas) = UCase(enumCostoDeVentasToString(mEnum)) Then
      strCostoDeVentasToNum = nCount
      GoTo h_EXIT
   End If
   nCount = nCount + 1
   Next
   strCostoDeVentasToNum = 0
   If ShowMessage Then
      gMessage.ProgrammerMessage "Valor " & costoDeVentas & " no existe en tipo enumerativo costoDeVentas"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithCostoDeVentas(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   varComboBox.Clear
   nCount = 0
   If action = Buscar Then
      varComboBox.Text = gDefGen.getSearchAllComboBox()
      varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
      nCount = nCount + 1
   Else
      varComboBox.Text = enumCostoDeVentasToString(enum_CostoDeVentas.eCD_A12INVENTARIOINICIAL)
   End If
   varComboBox.List(nCount) = enumCostoDeVentasToString(enum_CostoDeVentas.eCD_A12INVENTARIOINICIAL)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumCostoDeVentasToString(enum_CostoDeVentas.eCD_A13COMPRASNETASNACIONALES)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumCostoDeVentasToString(enum_CostoDeVentas.eCD_A14COMPRASNETASEXTERIOR)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumCostoDeVentasToString(enum_CostoDeVentas.eCD_A17MENOSINVENTARIOFINAL)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumCostoDeVentasToString(enum_CostoDeVentas.eCD_A19INVINICIALMATERIASPRIMAS)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumCostoDeVentasToString(enum_CostoDeVentas.eCD_A20COMPRASNETASMATERIAPRIMA)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumCostoDeVentasToString(enum_CostoDeVentas.eCD_A21MENOSINVFINALMATPRIMAS)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumCostoDeVentasToString(enum_CostoDeVentas.eCD_A22MANODEOBRADIRECTA)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumCostoDeVentasToString(enum_CostoDeVentas.eCD_A24OTRASREMUNERACIONES)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumCostoDeVentasToString(enum_CostoDeVentas.eCD_A25CONTRIBUCIONES)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumCostoDeVentasToString(enum_CostoDeVentas.eCD_A26COMPRADEBIENESSERVICIOS)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumCostoDeVentasToString(enum_CostoDeVentas.eCD_A27INVINICPRODENPROCESO)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumCostoDeVentasToString(enum_CostoDeVentas.eCD_A28MENOSINVFINALPRODPROCES)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumCostoDeVentasToString(enum_CostoDeVentas.eCD_A29INVINICPRODUCTTERMINADOS)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumCostoDeVentasToString(enum_CostoDeVentas.eCD_A30MENOSINVFINALPRODTERMIN)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumCostoDeVentasToString(enum_CostoDeVentas.eCD_A32OTROSCOSTOSDEVENTAS)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumCostoDeVentasToString(enum_CostoDeVentas.eCD_A33COSTOSDESERVICIOS)
   varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
   varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
End Sub

Public Function enumGastosToString(gastos As enum_Gastos) As String
   Select Case gastos
      Case eGa_A35SUELDOSYSALARIOS: enumGastosToString = "35. Sueldos y Salarios"
      Case eGa_A36INTERESESSOBRECREDITOS: enumGastosToString = "36. Intereses Sobre Créditos"
      Case eGa_A37CONRIBUCIONESSOCIALES: enumGastosToString = "37. Contribuciones Sociales"
      Case eGa_A38PRESTACIONESSOCIALES: enumGastosToString = "38. Prestaciones Sociales"
      Case eGa_A39GASTOSADMININMUEBLES: enumGastosToString = "39. Gastos Admin. Inmuebles"
      Case eGa_A40GASTOSADMINREGALIAS: enumGastosToString = "40. Gastos Admin. Regalías"
      Case eGa_A41DEPRECIACIONAMORTIZACION: enumGastosToString = "41. Depreciacion / Amortizacion"
      Case eGa_A42OTROSGASTOS: enumGastosToString = "42. Otros Gastos"
      Case Else: enumGastosToString = enumGastosToString(0)
   End Select
End Function

Public Function strGastosToNum(ByVal gastos As String, Optional ByVal ShowMessage As Boolean = True) As Integer
   Dim mEnum As enum_Gastos
   Dim nCount As Integer
   nCount = 0
   For mEnum = eGa_A35SUELDOSYSALARIOS To eGa_A42OTROSGASTOS
   If UCase(gastos) = UCase(enumGastosToString(mEnum)) Then
      strGastosToNum = nCount
      GoTo h_EXIT
   End If
   nCount = nCount + 1
   Next
   strGastosToNum = 0
   If ShowMessage Then
      gMessage.ProgrammerMessage "Valor " & gastos & " no existe en tipo enumerativo gastos"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithGastos(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   varComboBox.Clear
   nCount = 0
   If action = Buscar Then
      varComboBox.Text = gDefGen.getSearchAllComboBox()
      varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
      nCount = nCount + 1
   Else
      varComboBox.Text = enumGastosToString(enum_Gastos.eGa_A35SUELDOSYSALARIOS)
   End If
   varComboBox.List(nCount) = enumGastosToString(enum_Gastos.eGa_A35SUELDOSYSALARIOS)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumGastosToString(enum_Gastos.eGa_A36INTERESESSOBRECREDITOS)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumGastosToString(enum_Gastos.eGa_A37CONRIBUCIONESSOCIALES)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumGastosToString(enum_Gastos.eGa_A38PRESTACIONESSOCIALES)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumGastosToString(enum_Gastos.eGa_A39GASTOSADMININMUEBLES)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumGastosToString(enum_Gastos.eGa_A40GASTOSADMINREGALIAS)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumGastosToString(enum_Gastos.eGa_A41DEPRECIACIONAMORTIZACION)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumGastosToString(enum_Gastos.eGa_A42OTROSGASTOS)
   varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
   varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
End Sub

Public Function enumIngresosPropiosActividadToString(ingresosPropiosActividad As enum_IngresosPropiosActividad) As String
   Select Case ingresosPropiosActividad
      Case eIP_A1EXPORTACIONES: enumIngresosPropiosActividadToString = "1. Exportaciones"
      Case eIP_A2VENTASBRUTASSECTPUBLICO: enumIngresosPropiosActividadToString = "2. Ventas Brutas Sector Público"
      Case eIP_A3VENTASBRUTASSECTPRIVADO: enumIngresosPropiosActividadToString = "3. Ventas Brutas Sector Privado"
      Case eIP_A4VENTASINMUEBLESACREDITO: enumIngresosPropiosActividadToString = "4. Ventas Inmuebles a Crédito"
      Case eIP_A5REGALIASNOMINERAS: enumIngresosPropiosActividadToString = "5. Regalías No Mineras"
      Case eIP_A6REGALIASMINERAS: enumIngresosPropiosActividadToString = "6. Regalías Mineras"
      Case eIP_A7INTERESESSOBRECREDITOS: enumIngresosPropiosActividadToString = "7. Intereses Sobre Créditos"
      Case eIP_A8ARRENDAMIENTOSSUBARRENDAM: enumIngresosPropiosActividadToString = "8. Arrendamientos / SubArrendamientos"
      Case eIP_A9OTROSINGRESOS: enumIngresosPropiosActividadToString = "9. Otros Ingresos"
      Case eIP_A10MENOSDEVOLUCIONESREBAJAS: enumIngresosPropiosActividadToString = "10. MENOS: Devoluciones / Rebajas"
      Case Else: enumIngresosPropiosActividadToString = enumIngresosPropiosActividadToString(0)
   End Select
End Function

Public Function strIngresosPropiosActividadToNum(ByVal ingresosPropiosActividad As String, Optional ByVal ShowMessage As Boolean = True) As Integer
   Dim mEnum As enum_IngresosPropiosActividad
   Dim nCount As Integer
   nCount = 0
   For mEnum = eIP_A1EXPORTACIONES To eIP_A10MENOSDEVOLUCIONESREBAJAS
   If UCase(ingresosPropiosActividad) = UCase(enumIngresosPropiosActividadToString(mEnum)) Then
      strIngresosPropiosActividadToNum = nCount
      GoTo h_EXIT
   End If
   nCount = nCount + 1
   Next
   strIngresosPropiosActividadToNum = 0
   If ShowMessage Then
      gMessage.ProgrammerMessage "Valor " & ingresosPropiosActividad & " no existe en tipo enumerativo ingresosPropiosActividad"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithIngresosPropiosActividad(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   varComboBox.Clear
   nCount = 0
   If action = Buscar Then
      varComboBox.Text = gDefGen.getSearchAllComboBox()
      varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
      nCount = nCount + 1
   Else
      varComboBox.Text = enumIngresosPropiosActividadToString(enum_IngresosPropiosActividad.eIP_A1EXPORTACIONES)
   End If
   varComboBox.List(nCount) = enumIngresosPropiosActividadToString(enum_IngresosPropiosActividad.eIP_A1EXPORTACIONES)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumIngresosPropiosActividadToString(enum_IngresosPropiosActividad.eIP_A2VENTASBRUTASSECTPUBLICO)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumIngresosPropiosActividadToString(enum_IngresosPropiosActividad.eIP_A3VENTASBRUTASSECTPRIVADO)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumIngresosPropiosActividadToString(enum_IngresosPropiosActividad.eIP_A4VENTASINMUEBLESACREDITO)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumIngresosPropiosActividadToString(enum_IngresosPropiosActividad.eIP_A5REGALIASNOMINERAS)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumIngresosPropiosActividadToString(enum_IngresosPropiosActividad.eIP_A6REGALIASMINERAS)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumIngresosPropiosActividadToString(enum_IngresosPropiosActividad.eIP_A7INTERESESSOBRECREDITOS)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumIngresosPropiosActividadToString(enum_IngresosPropiosActividad.eIP_A8ARRENDAMIENTOSSUBARRENDAM)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumIngresosPropiosActividadToString(enum_IngresosPropiosActividad.eIP_A9OTROSINGRESOS)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumIngresosPropiosActividadToString(enum_IngresosPropiosActividad.eIP_A10MENOSDEVOLUCIONESREBAJAS)
   varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
   varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
End Sub

Public Function enumMotivoRetiroToString(motivoRetiro As enum_MotivoRetiro) As String
   Select Case motivoRetiro
      Case eMR_ENAJENACION: enumMotivoRetiroToString = "Enajenación"
      Case eMR_CONSUMO: enumMotivoRetiroToString = "Consumo"
      Case eMR_PERDIDA: enumMotivoRetiroToString = "Pérdida"
      Case eMR_RECLASIFICACION: enumMotivoRetiroToString = "Reclasificación"
      Case eMR_OTRO: enumMotivoRetiroToString = "Otro"
      Case eMR_NO_APLICA: enumMotivoRetiroToString = "No Aplica"
      Case Else: enumMotivoRetiroToString = enumMotivoRetiroToString(0)
   End Select
End Function

Public Function strMotivoRetiroToNum(ByVal motivoRetiro As String, Optional ByVal ShowMessage As Boolean = True) As Integer
   Dim mEnum As enum_MotivoRetiro
   Dim nCount As Integer
   nCount = 0
   For mEnum = eMR_ENAJENACION To eMR_NO_APLICA
   If UCase(motivoRetiro) = UCase(enumMotivoRetiroToString(mEnum)) Then
      strMotivoRetiroToNum = nCount
      GoTo h_EXIT
   End If
   nCount = nCount + 1
   Next
   strMotivoRetiroToNum = 0
   If ShowMessage Then
      gMessage.ProgrammerMessage "Valor " & motivoRetiro & " no existe en tipo enumerativo motivoRetiro"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithMotivoRetiro(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   varComboBox.Clear
   nCount = 0
   If action = Buscar Then
      varComboBox.Text = gDefGen.getSearchAllComboBox()
      varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
      nCount = nCount + 1
   Else
      varComboBox.Text = enumMotivoRetiroToString(enum_MotivoRetiro.eMR_ENAJENACION)
   End If
   varComboBox.List(nCount) = enumMotivoRetiroToString(enum_MotivoRetiro.eMR_ENAJENACION)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumMotivoRetiroToString(enum_MotivoRetiro.eMR_CONSUMO)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumMotivoRetiroToString(enum_MotivoRetiro.eMR_PERDIDA)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumMotivoRetiroToString(enum_MotivoRetiro.eMR_RECLASIFICACION)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumMotivoRetiroToString(enum_MotivoRetiro.eMR_OTRO)
   varComboBox.Width = 2100    'gAPI.sugestedWidthForComboBox(varComboBox)
   If action <> Retirar Then
      nCount = nCount + 1
      varComboBox.List(nCount) = enumMotivoRetiroToString(enum_MotivoRetiro.eMR_NO_APLICA)
   End If
   varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
End Sub

Public Function enumTipoReclasificacionToString(tipoReclasificacion As enum_TipoReclasificacion) As String
   Select Case tipoReclasificacion
      Case eTR_NOESTARECLASIFICADO: enumTipoReclasificacionToString = "No Está Reclasificado"
      Case eTR_ORIGEN: enumTipoReclasificacionToString = "Origen"
      Case eTR_DESTINO: enumTipoReclasificacionToString = "Destino"
      Case Else: enumTipoReclasificacionToString = enumTipoReclasificacionToString(0)
   End Select
End Function

Public Function strTipoReclasificacionToNum(ByVal tipoReclasificacion As String, Optional ByVal ShowMessage As Boolean = True) As Integer
   Dim mEnum As enum_TipoReclasificacion
   Dim nCount As Integer
   nCount = 0
   For mEnum = eTR_NOESTARECLASIFICADO To eTR_DESTINO
   If UCase(tipoReclasificacion) = UCase(enumTipoReclasificacionToString(mEnum)) Then
      strTipoReclasificacionToNum = nCount
      GoTo h_EXIT
   End If
   nCount = nCount + 1
   Next
   strTipoReclasificacionToNum = 0
   If ShowMessage Then
      gMessage.ProgrammerMessage "Valor " & tipoReclasificacion & " no existe en tipo enumerativo tipoReclasificacion"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithTipoReclasificacion(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   varComboBox.Clear
   nCount = 0
   If action = Buscar Then
      varComboBox.Text = gDefGen.getSearchAllComboBox()
      varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
      nCount = nCount + 1
   Else
      varComboBox.Text = enumTipoReclasificacionToString(enum_TipoReclasificacion.eTR_NOESTARECLASIFICADO)
   End If
   varComboBox.List(nCount) = enumTipoReclasificacionToString(enum_TipoReclasificacion.eTR_NOESTARECLASIFICADO)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumTipoReclasificacionToString(enum_TipoReclasificacion.eTR_ORIGEN)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumTipoReclasificacionToString(enum_TipoReclasificacion.eTR_DESTINO)
   varComboBox.Width = 2100 'gAPI.sugestedWidthForComboBox(varComboBox)
   varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
End Sub

Public Function AccionEscogerCompaniaStr(accion As AccionEscogerCompania) As String
   Select Case accion
      Case EscogerCompania: AccionEscogerCompaniaStr = "Escoger Compañía"
      Case EscogerPeriodo: AccionEscogerCompaniaStr = "Escoger Periodo"
   End Select
End Function

Public Sub FillComboBoxWithNivelDeDetalle(ByRef varComboBox As Object, ByVal gContNiveles As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   Dim i As Integer
   nCount = 0
   For i = 1 To gContNiveles.CantidadTotalDeNivelesDelPeriodo
      If i > 1 Then
         If gContNiveles.GetArrTextosValue(i) <> "" Then
            varComboBox.List(nCount) = gContNiveles.GetArrTextosValue(i)
            nCount = nCount + 1
            If i = 2 Then
               varComboBox.Text = gContNiveles.GetArrTextosValue(i)
            End If
         End If
      End If
   Next
   varComboBox.Width = 2100 'gAPI.sugestedWidthForComboBox(varComboBox)
   varComboBox.ListIndex = 0
End Sub

Public Function enumTipoDeComprobanteToString(tipoDeComprobante As enum_TipoDeComprobante) As String
   Select Case tipoDeComprobante
      Case eTDC_CHEQUES: enumTipoDeComprobanteToString = "01 - Cheques"
      Case eTDC_DEPOSITOS: enumTipoDeComprobanteToString = "02 - Depositos"
      Case eTDC_CUENTASXCOBRAR: enumTipoDeComprobanteToString = "03 - Cuentas x Cobrar"
      Case eTDC_CUENTASXPAGAR: enumTipoDeComprobanteToString = "04 - Cuentas x Pagar"
      Case eTDC_VENTAS_INGRESOS: enumTipoDeComprobanteToString = "05 - Ventas / Ingresos"
      Case eTDC_GASTOS: enumTipoDeComprobanteToString = "06 - Gastos"
      Case eTDC_APERTURA: enumTipoDeComprobanteToString = "93 - Apertura"
      Case eTDC_CONCILIACION: enumTipoDeComprobanteToString = "94 - Conciliación"
      Case eTDC_CIERREDEINVENTARIO: enumTipoDeComprobanteToString = "95 - Cierre de Inventario"
      Case eTDC_RETIRODEACTIVOSFIJOS: enumTipoDeComprobanteToString = "96 - Retiro de Activos Fijos"
      Case eTDC_COMPRADEACTIVOSFIJOS: enumTipoDeComprobanteToString = "97 - Compra de Activos Fijos"
      Case eTDC_DEPRECIACION: enumTipoDeComprobanteToString = "98 - Depreciación"
      Case eTDC_CIERREDEPERIODO: enumTipoDeComprobanteToString = "99 - Cierre de Período"
      Case eTDC_IMPORTACION: enumTipoDeComprobanteToString = "91 - Importación"
      Case eTDC_ADMINISTRATIVO: enumTipoDeComprobanteToString = "92 - Administrativo"
      Case eTDC_ANTICIPO: enumTipoDeComprobanteToString = "90 - Anticipo"
   End Select
End Function

Public Function enumStatusCuentaToString(statusCuenta As enum_StatusCuenta) As String
   Select Case statusCuenta
      Case eSC_NORMAL: enumStatusCuentaToString = "Normal"
      Case eSC_ELIMINADA: enumStatusCuentaToString = "Eliminada"
      Case eSC_COMPRIMIDA: enumStatusCuentaToString = "Comprimida"
      Case eSC_EXPANDIDA: enumStatusCuentaToString = "Expandida"
      Case Else: enumStatusCuentaToString = enumStatusCuentaToString(0)
   End Select
End Function

Public Function strStatusCuentaToNum(ByVal statusCuenta As String, Optional ByVal ShowMessage As Boolean = True) As Integer
   Dim mEnum As enum_StatusCuenta
   Dim nCount As Integer
   nCount = 0
   For mEnum = eSC_NORMAL To eSC_EXPANDIDA
   If UCase(statusCuenta) = UCase(enumStatusCuentaToString(mEnum)) Then
      strStatusCuentaToNum = nCount
      GoTo h_EXIT
   End If
   nCount = nCount + 1
   Next
   strStatusCuentaToNum = 0
   If ShowMessage Then
      gMessage.ProgrammerMessage "Valor " & statusCuenta & " no existe en tipo enumerativo statusCuenta"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithStatusCuenta(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   varComboBox.Clear
   nCount = 0
   If action = Buscar Then
      varComboBox.Text = gDefGen.getSearchAllComboBox()
      varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
      nCount = nCount + 1
   Else
      varComboBox.Text = enumStatusCuentaToString(enum_StatusCuenta.eSC_NORMAL)
   End If
   varComboBox.List(nCount) = enumStatusCuentaToString(enum_StatusCuenta.eSC_NORMAL)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumStatusCuentaToString(enum_StatusCuenta.eSC_ELIMINADA)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumStatusCuentaToString(enum_StatusCuenta.eSC_COMPRIMIDA)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumStatusCuentaToString(enum_StatusCuenta.eSC_EXPANDIDA)
   varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
   varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
End Sub

Public Function enumCantidadParaImpresionToString(cantidadParaImpresion As enum_CantidadParaImpresion) As String
   Select Case cantidadParaImpresion
      Case eCPI_UNO: enumCantidadParaImpresionToString = "Uno"
      Case eCPI_ENTRENUMEROS: enumCantidadParaImpresionToString = "Entre Dos Números"
      Case eCPI_ENTREFECHAS: enumCantidadParaImpresionToString = "Entre Dos Fechas"
   End Select
End Function

Public Sub FillComboBoxWithCantidadParaImpresion(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   nCount = 0
   If action = Buscar Then
      varComboBox.Text = gDefGen.getSearchAllComboBox()
      varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
      nCount = nCount + 1
   Else
      varComboBox.Text = enumCantidadParaImpresionToString(enum_CantidadParaImpresion.eCPI_UNO)
   End If
   varComboBox.List(nCount) = enumCantidadParaImpresionToString(enum_CantidadParaImpresion.eCPI_UNO)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumCantidadParaImpresionToString(enum_CantidadParaImpresion.eCPI_ENTRENUMEROS)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumCantidadParaImpresionToString(enum_CantidadParaImpresion.eCPI_ENTREFECHAS)
   varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
   varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
End Sub

Public Sub FillComboBoxWithTipoDeComprobante(ByRef varComboBox As Object, ByVal TipoDeComprobanteNavigator As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   Dim i As Integer
'   Dim TipoDeComprobanteNavigator As clsTipoDeComprobanteNav
'   Set TipoDeComprobanteNavigator = New clsTipoDeComprobanteNav
   If TipoDeComprobanteNavigator.fSearchByField("CodigoDelTipo", "*") Then
      nCount = 0
'      For I = 1 To tipoDeComprobanteNavigator.GetRsTipoDeComprobante.RecordCount
      For i = 1 To TipoDeComprobanteNavigator.fRsRecordCount
         If i > 1 Then
            TipoDeComprobanteNavigator.fRsNextRecord
         End If
         varComboBox.List(nCount) = TipoDeComprobanteNavigator.GetNombreDelTipo
         nCount = nCount + 1
         If i = 1 Then
            varComboBox.Text = TipoDeComprobanteNavigator.GetNombreDelTipo
         End If
      Next
   End If
 '  Set TipoDeComprobanteNavigator = Nothing
   varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
   varComboBox.ListIndex = 0
End Sub

Public Sub FillComboBoxWithGrupodeActivo(ByRef varComboBox As Object, ByVal GrupodeActivoNavigator As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   Dim i As Integer
'   Dim GrupodeActivoNavigator As clsGrupoDeActivosNavigator
'   Set GrupodeActivoNavigator = New clsGrupoDeActivosNavigator
   If GrupodeActivoNavigator.fSearchByField("Nombre", "*") Then
      nCount = 0
      For i = 1 To GrupodeActivoNavigator.GetRsGrupoDeActivos.RecordCount
         If i > 1 Then
            GrupodeActivoNavigator.fRsNextRecord
         End If
         varComboBox.List(nCount) = GrupodeActivoNavigator.GetNombre
         nCount = nCount + 1
         If i = 1 Then
            varComboBox.Text = GrupodeActivoNavigator.GetNombre
         End If
      Next
      varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
      varComboBox.ListIndex = 0
   End If
'   Set GrupodeActivoNavigator = Nothing
End Sub

Public Function enumVidaUtilExpresadaEnToString(vidaUtilExpresadaEn As enum_VidaUtilExpresadaEn) As String
   Select Case vidaUtilExpresadaEn
      Case eVU_MESES: enumVidaUtilExpresadaEnToString = "Meses"
      Case eVU_ANOS: enumVidaUtilExpresadaEnToString = "Años"
      Case eVU_ANOSYMESES: enumVidaUtilExpresadaEnToString = "Años y Meses"
   End Select
End Function

Public Function strVidaUtilExpresadaEnToNum(ByVal vidaUtilExpresadaEn As String, Optional ByVal ShowMessage As Boolean = True) As Integer
   Dim mEnum As enum_VidaUtilExpresadaEn
   Dim nCount As Integer
   nCount = 0
   For mEnum = eVU_MESES To eVU_ANOSYMESES
   If UCase(vidaUtilExpresadaEn) = UCase(enumVidaUtilExpresadaEnToString(mEnum)) Then
      strVidaUtilExpresadaEnToNum = nCount
      GoTo h_EXIT
   End If
   nCount = nCount + 1
   Next
   strVidaUtilExpresadaEnToNum = 0
   If ShowMessage Then
      gMessage.ProgrammerMessage "Valor " & vidaUtilExpresadaEn & " no existe en tipo enumerativo vidaUtilExpresadaEn"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithVidaUtilExpresadaEn(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   nCount = 0
   If action = Buscar Then
      varComboBox.Text = gDefGen.getSearchAllComboBox()
      varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
      nCount = nCount + 1
   Else
      varComboBox.Text = enumVidaUtilExpresadaEnToString(enum_VidaUtilExpresadaEn.eVU_MESES)
   End If
   varComboBox.List(nCount) = enumVidaUtilExpresadaEnToString(enum_VidaUtilExpresadaEn.eVU_MESES)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumVidaUtilExpresadaEnToString(enum_VidaUtilExpresadaEn.eVU_ANOS)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumVidaUtilExpresadaEnToString(enum_VidaUtilExpresadaEn.eVU_ANOSYMESES)
   varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
   varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
End Sub

Public Function enumDepreciarDesdeToString(depreciarDesde As enum_DepreciarDesde) As String
   Select Case depreciarDesde
      Case eDD_MESADQUISICION: enumDepreciarDesdeToString = "Mes Adquisición"
      Case eDD_MESSIGUIENTE: enumDepreciarDesdeToString = "Mes Siguiente"
      Case Else: enumDepreciarDesdeToString = enumDepreciarDesdeToString(0)
   End Select
End Function

Public Function strDepreciarDesdeToNum(ByVal depreciarDesde As String, Optional ByVal ShowMessage As Boolean = True) As Integer
   Dim mEnum As enum_DepreciarDesde
   Dim nCount As Integer
   nCount = 0
   For mEnum = eDD_MESADQUISICION To eDD_MESSIGUIENTE
   If UCase(depreciarDesde) = UCase(enumDepreciarDesdeToString(mEnum)) Then
      strDepreciarDesdeToNum = nCount
      GoTo h_EXIT
   End If
   nCount = nCount + 1
   Next
   strDepreciarDesdeToNum = 0
   If ShowMessage Then
      gMessage.ProgrammerMessage "Valor " & depreciarDesde & " no existe en tipo enumerativo depreciarDesde"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithDepreciarDesde(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   nCount = 0
   If action = Buscar Then
      varComboBox.Text = gDefGen.getSearchAllComboBox()
      varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
      nCount = nCount + 1
   Else
      varComboBox.Text = enumDepreciarDesdeToString(enum_DepreciarDesde.eDD_MESADQUISICION)
   End If
   varComboBox.List(nCount) = enumDepreciarDesdeToString(enum_DepreciarDesde.eDD_MESADQUISICION)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumDepreciarDesdeToString(enum_DepreciarDesde.eDD_MESSIGUIENTE)
   varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
   varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
End Sub


Public Function enumCualesActivosImprimirToString(CualesActivosImprimir As enum_CualesActivosImprimir) As String
   Select Case CualesActivosImprimir
      Case eCA_VIGENTES: enumCualesActivosImprimirToString = "Vigentes"
      Case eCA_RETIRADOS: enumCualesActivosImprimirToString = "Retirados del Período"
      Case eCA_ADQUISICIONESDELPERIODO: enumCualesActivosImprimirToString = "Adquisiciones del Período"
   End Select
End Function

Public Sub FillComboBoxWithCualesActivosImprimir(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   nCount = 0
   If action = Buscar Then
      varComboBox.Text = gDefGen.getSearchAllComboBox()
      varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
      nCount = nCount + 1
   Else
      varComboBox.Text = enumCualesActivosImprimirToString(enum_CualesActivosImprimir.eCA_VIGENTES)
   End If
   varComboBox.List(nCount) = enumCualesActivosImprimirToString(enum_CualesActivosImprimir.eCA_VIGENTES)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumCualesActivosImprimirToString(enum_CualesActivosImprimir.eCA_RETIRADOS)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumCualesActivosImprimirToString(enum_CualesActivosImprimir.eCA_ADQUISICIONESDELPERIODO)
   varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
   varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
End Sub

Public Sub FillComboBoxWithMeses(ByRef varComboBox As Object, ByVal gContPeriodoActual As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   nCount = 0
   If action = Buscar Then
      varComboBox.Text = gDefGen.getSearchAllComboBox()
      varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
      nCount = nCount + 1
   Else
      varComboBox.Text = gConvert.MesString(gContPeriodoActual.GetFechaDeCierre1)
   End If
   varComboBox.List(nCount) = gConvert.MesString(gContPeriodoActual.GetFechaDeCierre1)
   nCount = nCount + 1
   varComboBox.List(nCount) = gConvert.MesString(gContPeriodoActual.GetFechaDeCierre2)
   nCount = nCount + 1
   varComboBox.List(nCount) = gConvert.MesString(gContPeriodoActual.GetFechaDeCierre3)
   nCount = nCount + 1
   varComboBox.List(nCount) = gConvert.MesString(gContPeriodoActual.GetFechaDeCierre4)
   nCount = nCount + 1
   varComboBox.List(nCount) = gConvert.MesString(gContPeriodoActual.GetFechaDeCierre5)
   nCount = nCount + 1
   varComboBox.List(nCount) = gConvert.MesString(gContPeriodoActual.GetFechaDeCierre6)
   nCount = nCount + 1
   varComboBox.List(nCount) = gConvert.MesString(gContPeriodoActual.GetFechaDeCierre7)
   nCount = nCount + 1
   varComboBox.List(nCount) = gConvert.MesString(gContPeriodoActual.GetFechaDeCierre8)
   nCount = nCount + 1
   varComboBox.List(nCount) = gConvert.MesString(gContPeriodoActual.GetFechaDeCierre9)
   nCount = nCount + 1
   varComboBox.List(nCount) = gConvert.MesString(gContPeriodoActual.GetFechaDeCierre10)
   nCount = nCount + 1
   varComboBox.List(nCount) = gConvert.MesString(gContPeriodoActual.GetFechaDeCierre11)
   nCount = nCount + 1
   varComboBox.List(nCount) = gConvert.MesString(gContPeriodoActual.GetFechaCierreDelPeriodo)
   varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
   varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
End Sub

Public Sub FillComboBoxWithComprobantesEspeciales(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   nCount = 0
   If action = Buscar Then
      varComboBox.Text = gDefGen.getSearchAllComboBox()
      varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
      nCount = nCount + 1
   Else
      varComboBox.Text = enumComprobanteGeneradoPorToString(enum_ComprobanteGeneradoPor.eCG_DEPRECIACION)
   End If
   varComboBox.List(nCount) = enumComprobanteGeneradoPorToString(enum_ComprobanteGeneradoPor.eCG_DEPRECIACION)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumComprobanteGeneradoPorToString(enum_ComprobanteGeneradoPor.eCG_CIERRE_CUENTAS_RESULTADO)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumComprobanteGeneradoPorToString(enum_ComprobanteGeneradoPor.eCG_CIERRE_INVENTARIO)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumComprobanteGeneradoPorToString(enum_ComprobanteGeneradoPor.eCG_RESERVA)
   varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
   varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
End Sub

Public Function strCualesActivosImprimirPorToNum(ByVal cualesActivos As String, Optional ByVal ShowMessage As Boolean = True) As Integer
   Dim mEnum As enum_CualesActivosImprimir
   Dim nCount As Integer
   nCount = 0
   For mEnum = eCA_VIGENTES To eCA_ADQUISICIONESDELPERIODO
   If UCase(cualesActivos) = UCase(enumCualesActivosImprimirToString(mEnum)) Then
      strCualesActivosImprimirPorToNum = nCount
      GoTo h_EXIT
   End If
   nCount = nCount + 1
   Next
   strCualesActivosImprimirPorToNum = 0
   If ShowMessage Then
      gMessage.ProgrammerMessage "Valor " & cualesActivos & " no existe en tipo enumerativo cualesActivosImprimir"
   End If
h_EXIT:
   Exit Function
End Function

Public Function enumFormaDeEscogerCompaniaToString(ByVal formaDeEscogerCompania As Integer) As String
   Dim strTemp As String
   Dim insEnumAdm As clsEnumAdministrativo
   On Error Resume Next
   Set insEnumAdm = New clsEnumAdministrativo
   strTemp = insEnumAdm.enumFormaDeEscogerCompaniaToString(formaDeEscogerCompania)
   enumFormaDeEscogerCompaniaToString = strTemp
   Set insEnumAdm = Nothing
   On Error GoTo 0
End Function

Public Function strFormaDeEscogerCompaniaToNum(ByVal formaDeEscogerCompania As String, Optional ByVal ShowMessage As Boolean = True) As Integer
   Dim insEnumAdm As clsEnumAdministrativo
   On Error Resume Next
   Set insEnumAdm = New clsEnumAdministrativo
   strFormaDeEscogerCompaniaToNum = insEnumAdm.strFormaDeEscogerCompaniaToNum(formaDeEscogerCompania, ShowMessage)
   Set insEnumAdm = Nothing
End Function

Public Sub FillComboBoxWithFormaDeEscogerCompania(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim insEnumAdm As clsEnumAdministrativo
   On Error Resume Next
   Set insEnumAdm = New clsEnumAdministrativo
   insEnumAdm.FillComboBoxWithFormaDeEscogerCompania varComboBox, action
   Set insEnumAdm = Nothing
End Sub

Public Function fEnumNaturalezaDeLaCuentaToStringInArray(Optional ByVal valPrepararParaSql As Boolean = False) As Variant
   Dim varEnum As enum_NaturalezaDeLaCuenta
   Dim varArray() As String
   Dim varLimiteSuperior As Integer
   Dim nCount As Integer
   Dim varCaracterDelimitador As String
   On Error Resume Next
   varCaracterDelimitador = ""
   If valPrepararParaSql Then
      varCaracterDelimitador = "'"
   End If
   varLimiteSuperior = eND_HABER - eND_DEBE
   nCount = 0
   ReDim varArray(0 To varLimiteSuperior)
   For varEnum = eND_DEBE To eND_HABER
      varArray(nCount) = varCaracterDelimitador & enumNaturalezaDeLaCuentaToString(varEnum) & varCaracterDelimitador
      nCount = nCount + 1
   Next
   fEnumNaturalezaDeLaCuentaToStringInArray = varArray
   On Error GoTo 0
End Function

Public Function fEnumStatusComprobanteToStringInArray(Optional ByVal valPrepararParaSql As Boolean = False) As Variant
   Dim varEnum As enum_StatusComprobante
   Dim varArray() As String
   Dim varLimiteSuperior As Integer
   Dim nCount As Integer
   Dim varCaracterDelimitador As String
   On Error Resume Next
   varCaracterDelimitador = ""
   If valPrepararParaSql Then
      varCaracterDelimitador = "'"
   End If
   varLimiteSuperior = enum_StatusComprobante.eSC_DESCUADRADO - enum_StatusComprobante.eSC_CONTABILIZADO
   nCount = 0
   ReDim varArray(0 To varLimiteSuperior)
   For varEnum = eSC_CONTABILIZADO To eSC_DESCUADRADO
      varArray(nCount) = varCaracterDelimitador & enumStatusComprobanteToString(varEnum) & varCaracterDelimitador
      nCount = nCount + 1
   Next
   fEnumStatusComprobanteToStringInArray = varArray
   On Error GoTo 0
End Function

Public Function fEnumGrupoAuxiliarToStringInArray(Optional ByVal valPrepararParaSql As Boolean = False) As Variant
   Dim varEnum As enum_GrupoAuxiliar
   Dim varArray() As String
   Dim varLimiteSuperior As Integer
   Dim nCount As Integer
   Dim varCaracterDelimitador As String
   On Error Resume Next
   varCaracterDelimitador = ""
   If valPrepararParaSql Then
      varCaracterDelimitador = "'"
   End If
   varLimiteSuperior = enum_GrupoAuxiliar.eGA_SINGRUPO - enum_GrupoAuxiliar.eGA_CLIENTES
   nCount = 0
   ReDim varArray(0 To varLimiteSuperior)
   For varEnum = eGA_CLIENTES To eGA_SINGRUPO
      varArray(nCount) = varCaracterDelimitador & enumGrupoAuxiliarToString(varEnum) & varCaracterDelimitador
      nCount = nCount + 1
   Next
   fEnumGrupoAuxiliarToStringInArray = varArray
   On Error GoTo 0
End Function

Public Function enumTipoDeOperacionParaAuditarToString(tipoDeOperacionParaAuditar As enum_TipoDeOperacionParaAuditar) As String
   Select Case tipoDeOperacionParaAuditar
      Case eTDA_INSERTAR: enumTipoDeOperacionParaAuditarToString = "Insertar"
      Case eTDA_MODIFICAR: enumTipoDeOperacionParaAuditarToString = "Modificar"
      Case eTDA_ELIMINAR: enumTipoDeOperacionParaAuditarToString = "Eliminar"
      Case eTDA_RESINCRONIZAR: enumTipoDeOperacionParaAuditarToString = "Resincronizar"
      Case eTDA_SQL: enumTipoDeOperacionParaAuditarToString = "SQL Query"
      Case Else: enumTipoDeOperacionParaAuditarToString = enumTipoDeOperacionParaAuditarToString(0)
   End Select
End Function

Public Function strTipoDeOperacionParaAuditarToNum(ByVal tipoDeOperacionParaAuditar As String, Optional ByVal ShowMessage As Boolean = True) As Integer
   Dim mEnum As enum_TipoDeOperacionParaAuditar
   Dim nCount As Integer
   nCount = 0
   For mEnum = eTDA_INSERTAR To eTDA_SQL
      If UCase(tipoDeOperacionParaAuditar) = UCase(enumTipoDeOperacionParaAuditarToString(mEnum)) Then
         strTipoDeOperacionParaAuditarToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strTipoDeOperacionParaAuditarToNum = 0
   If ShowMessage Then
      gMessage.ProgrammerMessage "Valor " & tipoDeOperacionParaAuditar & " no existe en tipo enumerativo tipoDeOperacionParaAuditar"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithTipoDeOperacionParaAuditar(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   nCount = 0
   If action = Buscar Then
      varComboBox.Text = gDefGen.getSearchAllComboBox()
      varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
      nCount = nCount + 1
   Else
      varComboBox.Text = enumTipoDeOperacionParaAuditarToString(enum_TipoDeOperacionParaAuditar.eTDA_INSERTAR)
   End If
   varComboBox.List(nCount) = enumTipoDeOperacionParaAuditarToString(enum_TipoDeOperacionParaAuditar.eTDA_INSERTAR)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumTipoDeOperacionParaAuditarToString(enum_TipoDeOperacionParaAuditar.eTDA_MODIFICAR)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumTipoDeOperacionParaAuditarToString(enum_TipoDeOperacionParaAuditar.eTDA_ELIMINAR)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumTipoDeOperacionParaAuditarToString(enum_TipoDeOperacionParaAuditar.eTDA_RESINCRONIZAR)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumTipoDeOperacionParaAuditarToString(enum_TipoDeOperacionParaAuditar.eTDA_SQL)
   varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
   varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
End Sub

Public Function fEnumTipoDeOperacionAuditoriaToStringInArray(Optional ByVal valPrepararParaSql As Boolean = False) As Variant
   Dim varEnum As enum_TipoDeOperacionParaAuditar
   Dim varArray() As String
   Dim varLimiteSuperior As Integer
   Dim nCount As Integer
   Dim varCaracterDelimitador As String
   On Error Resume Next
   varCaracterDelimitador = ""
   If valPrepararParaSql Then
      varCaracterDelimitador = "'"
   End If
   varLimiteSuperior = eTDA_SQL - eTDA_INSERTAR
   nCount = 0
   ReDim varArray(0 To varLimiteSuperior)
   For varEnum = eTDA_INSERTAR To eTDA_SQL
      varArray(nCount) = varCaracterDelimitador & UCase(enumTipoDeOperacionParaAuditarToString(varEnum)) & varCaracterDelimitador
      nCount = nCount + 1
   Next
   fEnumTipoDeOperacionAuditoriaToStringInArray = varArray
   On Error GoTo 0
End Function

Public Function fEnumComprobanteGeneradoPorToStringInArray(Optional ByVal valPrepararParaSql As Boolean = False) As Variant
   Dim varEnum As enum_ComprobanteGeneradoPor
   Dim varArray() As String
   Dim varLimiteSuperior As Integer
   Dim nCount As Integer
   Dim varCaracterDelimitador As String
   On Error Resume Next
   varCaracterDelimitador = ""
   If valPrepararParaSql Then
      varCaracterDelimitador = "'"
   End If
   varLimiteSuperior = enum_ComprobanteGeneradoPor.eCG_RESERVA - enum_ComprobanteGeneradoPor.eCG_USUARIO
   nCount = 0
   ReDim varArray(0 To varLimiteSuperior)
   For varEnum = eCG_USUARIO To eCG_RESERVA
      varArray(nCount) = varCaracterDelimitador & enumComprobanteGeneradoPorToString(varEnum) & varCaracterDelimitador
      nCount = nCount + 1
   Next
   fEnumComprobanteGeneradoPorToStringInArray = varArray
   On Error GoTo 0
End Function

Public Function enumTipoDeAsientoToString(tipoDeAsiento As enum_TipoDeAsiento) As String
   Select Case tipoDeAsiento
      Case eTDA_NORMAL: enumTipoDeAsientoToString = "Normal"
      Case eTDA_ACTIVOFIJO: enumTipoDeAsientoToString = "Activo Fijo"
      Case eTDA_CONCILIACION: enumTipoDeAsientoToString = "Conciliación"
      Case Else: enumTipoDeAsientoToString = enumTipoDeAsientoToString(0)
   End Select
End Function

Public Function strTipoDeAsientoToNum(ByVal tipoDeAsiento As String, Optional ByVal ShowMessage As Boolean = True) As Integer
   Dim mEnum As enum_TipoDeAsiento
   Dim nCount As Integer
   nCount = 0
   For mEnum = eTDA_NORMAL To eTDA_CONCILIACION
      If UCase(tipoDeAsiento) = UCase(enumTipoDeAsientoToString(mEnum)) Then
         strTipoDeAsientoToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strTipoDeAsientoToNum = 0
   If ShowMessage Then
      gMessage.ProgrammerMessage "Valor " & tipoDeAsiento & " no existe en tipo enumerativo tipoDeAsiento"
   End If
h_EXIT:
   Exit Function
End Function

Public Function enumTipoPlanillaISLRToString(ByVal TipoContribuyente As Integer) As String
   Select Case TipoContribuyente
      Case eTD_PERSONAJURIDICA: enumTipoPlanillaISLRToString = "DPJ-Persona Jurídica"
      Case eTD_PERSONANATURAL: enumTipoPlanillaISLRToString = "DPJ-Persona Natural"
      Case Else
      enumTipoPlanillaISLRToString = enumTipoPlanillaISLRToString(0)
   End Select
End Function

Public Function fEnumIngresosPropiosToStringInArray(Optional ByVal valPrepararParaSql As Boolean = False) As Variant
   Dim varEnum As enum_NaturalezaDeLaCuenta
   Dim varArray() As String
   Dim varLimiteSuperior As Integer
   Dim nCount As Integer
   Dim varCaracterDelimitador As String
   On Error Resume Next
   varCaracterDelimitador = ""
   If valPrepararParaSql Then
      varCaracterDelimitador = "'"
   End If
   varLimiteSuperior = eIP_A10MENOSDEVOLUCIONESREBAJAS - eIP_A1EXPORTACIONES
   nCount = 0
   ReDim varArray(0 To varLimiteSuperior)
   For varEnum = eIP_A1EXPORTACIONES To eIP_A10MENOSDEVOLUCIONESREBAJAS
      varArray(nCount) = varCaracterDelimitador & enumIngresosPropiosActividadToString(varEnum) & varCaracterDelimitador
      nCount = nCount + 1
   Next
   fEnumIngresosPropiosToStringInArray = varArray
   On Error GoTo 0
End Function

Public Function fEnumCostoDeVentaToStringInArray(Optional ByVal valPrepararParaSql As Boolean = False) As Variant
   Dim varEnum As enum_NaturalezaDeLaCuenta
   Dim varArray() As String
   Dim varLimiteSuperior As Integer
   Dim nCount As Integer
   Dim varCaracterDelimitador As String
   On Error Resume Next
   varCaracterDelimitador = ""
   If valPrepararParaSql Then
      varCaracterDelimitador = "'"
   End If
   varLimiteSuperior = eCD_A33COSTOSDESERVICIOS - eCD_A12INVENTARIOINICIAL
   nCount = 0
   ReDim varArray(0 To varLimiteSuperior)
   For varEnum = eCD_A12INVENTARIOINICIAL To eCD_A33COSTOSDESERVICIOS
      varArray(nCount) = varCaracterDelimitador & enumCostoDeVentasToString(varEnum) & varCaracterDelimitador
      nCount = nCount + 1
   Next
   fEnumCostoDeVentaToStringInArray = varArray
   On Error GoTo 0
End Function

Public Function fEnumGastosToStringInArray(Optional ByVal valPrepararParaSql As Boolean = False) As Variant
   Dim varEnum As enum_NaturalezaDeLaCuenta
   Dim varArray() As String
   Dim varLimiteSuperior As Integer
   Dim nCount As Integer
   Dim varCaracterDelimitador As String
   On Error Resume Next
   varCaracterDelimitador = ""
   If valPrepararParaSql Then
      varCaracterDelimitador = "'"
   End If
   varLimiteSuperior = eGa_A42OTROSGASTOS - eGa_A35SUELDOSYSALARIOS
   nCount = 0
   ReDim varArray(0 To varLimiteSuperior)
   For varEnum = eGa_A35SUELDOSYSALARIOS To eGa_A42OTROSGASTOS
      varArray(nCount) = varCaracterDelimitador & enumGastosToString(varEnum) & varCaracterDelimitador
      nCount = nCount + 1
   Next
   fEnumGastosToStringInArray = varArray
   On Error GoTo 0
End Function

Public Function enumFormaDeValidarCentroDeCostosToString(formaDeValidarCentroDeCostos As enum_FormaDeValidarCentroDeCostos) As String
   Select Case formaDeValidarCentroDeCostos
      Case eFD_NO_VALIDAR: enumFormaDeValidarCentroDeCostosToString = "No Validar"
      Case eFD_MOSTRAR_MENSAJE_Y_GUARDAR: enumFormaDeValidarCentroDeCostosToString = "Mostrar Mensaje y Guardar"
      Case eFD_MOSTRAR_MENSAJE_Y_NO_GUARDAR: enumFormaDeValidarCentroDeCostosToString = "Mostrar Mensaje y NO Guardar"
      Case Else
         enumFormaDeValidarCentroDeCostosToString = enumFormaDeValidarCentroDeCostosToString(0)
         End Select
End Function

Public Function strFormaDeValidarCentroDeCostosToNum(ByVal formaDeValidarCentroDeCostos As String, _
      Optional ByVal ShowMessage As Boolean = True) As Integer
   Dim mEnum As enum_FormaDeValidarCentroDeCostos
   Dim nCount As Integer
   nCount = 0
   For mEnum = eFD_NO_VALIDAR To eFD_MOSTRAR_MENSAJE_Y_NO_GUARDAR
      If UCase(formaDeValidarCentroDeCostos) = UCase(enumFormaDeValidarCentroDeCostosToString(mEnum)) Then
         strFormaDeValidarCentroDeCostosToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strFormaDeValidarCentroDeCostosToNum = 0
   If ShowMessage Then
      gMessage.ProgrammerMessage "Valor " & formaDeValidarCentroDeCostos & " no existe en tipo enumerativo formaDeValidarCentroDeCostos"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithFormaDeValidarCentroDeCostos(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   varComboBox.Clear
   nCount = 0
   If action = Buscar Then
      varComboBox.Text = gDefGen.getSearchAllComboBox()
      varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
      nCount = nCount + 1
   Else
      varComboBox.Text = enumFormaDeValidarCentroDeCostosToString(enum_FormaDeValidarCentroDeCostos.eFD_NO_VALIDAR)
   End If
   varComboBox.List(nCount) = enumFormaDeValidarCentroDeCostosToString(enum_FormaDeValidarCentroDeCostos.eFD_NO_VALIDAR)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumFormaDeValidarCentroDeCostosToString(enum_FormaDeValidarCentroDeCostos.eFD_MOSTRAR_MENSAJE_Y_GUARDAR)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumFormaDeValidarCentroDeCostosToString(enum_FormaDeValidarCentroDeCostos.eFD_MOSTRAR_MENSAJE_Y_NO_GUARDAR)
   varComboBox.ListIndex = 0
   varComboBox.Width = 2650
End Sub
Public Function enumDpc10MetodoAjusteToString(Dpc10MetodoAjuste As enum_Dpc10MetodoAjuste) As String
   Select Case Dpc10MetodoAjuste
      Case eD10MA_ACTIVOFIJO: enumDpc10MetodoAjusteToString = "Activo Fijo"
      Case eD10MA_MONEDAEXTRANJERA: enumDpc10MetodoAjusteToString = "Moneda Extranjera"
      Case eD10MA_CLAUSULADEREAJUSTABILIDAD: enumDpc10MetodoAjusteToString = "Clausula de Reajustabilidad"
      Case eD10MA_INVENTARIO: enumDpc10MetodoAjusteToString = "Inventario"
      Case eD10MA_PATRIMONIO: enumDpc10MetodoAjusteToString = "Patrimonio"
      Case Ed10ma_Obrasenproceso: enumDpc10MetodoAjusteToString = "Obras en Proceso"
      Case eD10MA_MONETARIA: enumDpc10MetodoAjusteToString = "Monetaria"
      Case eD10MA_OTRASNOMONETARIAS: enumDpc10MetodoAjusteToString = "Otras No Monetarias"
      Case Else
         enumDpc10MetodoAjusteToString = enumDpc10MetodoAjusteToString(0)
   End Select
End Function

Public Function strDpc10MetodoAjusteToNum(ByVal Dpc10MetodoAjuste As String, Optional ByVal ShowMessage As Boolean = True) As Integer
   Dim mEnum As enum_Dpc10MetodoAjuste
   Dim nCount As Integer
   nCount = 0
   For mEnum = eD10MA_ACTIVOFIJO To eD10MA_OTRASNOMONETARIAS
      If UCase(Dpc10MetodoAjuste) = UCase(enumDpc10MetodoAjusteToString(mEnum)) Then
         strDpc10MetodoAjusteToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strDpc10MetodoAjusteToNum = 6
   If ShowMessage Then
      gMessage.ProgrammerMessage "Valor " & Dpc10MetodoAjuste & " no existe en tipo enumerativo Dpc10MetodoAjuste"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithDpc10MetodoAjuste(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir, _
                                                Optional EsActivoPasivoOPatrimonio As Boolean)
   Dim nCount As Integer
   varComboBox.Clear
   nCount = 0
   If action = Buscar Then
      varComboBox.Text = gDefGen.getSearchAllComboBox()
      varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
      nCount = nCount + 1
   Else
      varComboBox.Text = enumDpc10MetodoAjusteToString(enum_Dpc10MetodoAjuste.eD10MA_ACTIVOFIJO)
   End If
   If EsActivoPasivoOPatrimonio Then
      varComboBox.List(nCount) = enumDpc10MetodoAjusteToString(enum_Dpc10MetodoAjuste.eD10MA_ACTIVOFIJO)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumDpc10MetodoAjusteToString(enum_Dpc10MetodoAjuste.eD10MA_MONEDAEXTRANJERA)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumDpc10MetodoAjusteToString(enum_Dpc10MetodoAjuste.eD10MA_CLAUSULADEREAJUSTABILIDAD)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumDpc10MetodoAjusteToString(enum_Dpc10MetodoAjuste.eD10MA_INVENTARIO)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumDpc10MetodoAjusteToString(enum_Dpc10MetodoAjuste.eD10MA_PATRIMONIO)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumDpc10MetodoAjusteToString(enum_Dpc10MetodoAjuste.Ed10ma_Obrasenproceso)
      nCount = nCount + 1
   End If
   varComboBox.List(nCount) = enumDpc10MetodoAjusteToString(enum_Dpc10MetodoAjuste.eD10MA_MONETARIA)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumDpc10MetodoAjusteToString(enum_Dpc10MetodoAjuste.eD10MA_OTRASNOMONETARIAS)
   varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
End Sub
Public Sub FillComboBoxWithDpc10MetodoAjusteAExportar(ByRef varComboBox As Object, _
                                                         Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   varComboBox.Clear
   nCount = 0
   If action = Buscar Then
      varComboBox.Text = gDefGen.getSearchAllComboBox()
      varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
      nCount = nCount + 1
   Else
      varComboBox.Text = enumDpc10MetodoAjusteToString(enum_Dpc10MetodoAjuste.eD10MA_ACTIVOFIJO)
   End If
   varComboBox.List(nCount) = enumDpc10MetodoAjusteToString(enum_Dpc10MetodoAjuste.eD10MA_ACTIVOFIJO)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumDpc10MetodoAjusteToString(enum_Dpc10MetodoAjuste.eD10MA_MONEDAEXTRANJERA)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumDpc10MetodoAjusteToString(enum_Dpc10MetodoAjuste.eD10MA_CLAUSULADEREAJUSTABILIDAD)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumDpc10MetodoAjusteToString(enum_Dpc10MetodoAjuste.eD10MA_INVENTARIO)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumDpc10MetodoAjusteToString(enum_Dpc10MetodoAjuste.eD10MA_PATRIMONIO)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumDpc10MetodoAjusteToString(enum_Dpc10MetodoAjuste.Ed10ma_Obrasenproceso)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumDpc10MetodoAjusteToString(enum_Dpc10MetodoAjuste.eD10MA_MONETARIA)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumDpc10MetodoAjusteToString(enum_Dpc10MetodoAjuste.eD10MA_OTRASNOMONETARIAS)
   varComboBox.ListIndex = 6  ' Se selecciona el valor por defecto
'   varComboBox.Width = gAPI.sugestedWidthForComboBox(varComboBox)
End Sub

Public Function enumMetodoAjusteToString(MetodoAjuste As enum_MetodoAjuste) As String
   Select Case MetodoAjuste
      Case eMA_PORIPC: enumMetodoAjusteToString = "Por IPC (Act.Fijo, etc)"
      Case eMA_MonedaExtranjera: enumMetodoAjusteToString = "Moneda Extranjera"
      Case eMA_ClausuladeReajustabilidad: enumMetodoAjusteToString = "Clausula Reajustabilidad"
      Case eMA_Inventario: enumMetodoAjusteToString = "Inventario"
      Case eMA_Patrimonio: enumMetodoAjusteToString = "Patrimonio"
      Case Ema_Obrasenproceso: enumMetodoAjusteToString = "Obra en Proceso"
      Case eMA_Monetaria: enumMetodoAjusteToString = "Monetaria"
      Case eMA_OtrasNoMonetarias: enumMetodoAjusteToString = "Otras No Monetarias"
'      Case eMA_Titulo: enumMetodoAjusteToString = "Titulo"
      Case Else
         enumMetodoAjusteToString = enumMetodoAjusteToString(0)
   End Select
End Function

Public Function strMetodoAjusteToNum(ByVal MetodoAjuste As String, Optional ByVal ShowMessage As Boolean = True) As Integer
   Dim mEnum As enum_MetodoAjuste
   Dim nCount As Integer
   nCount = 0
   For mEnum = eMA_PORIPC To eMA_OtrasNoMonetarias
      If UCase(MetodoAjuste) = UCase(enumMetodoAjusteToString(mEnum)) Then
         strMetodoAjusteToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strMetodoAjusteToNum = 6
   If ShowMessage Then
      gMessage.ProgrammerMessage "Valor " & MetodoAjuste & " no existe en tipo enumerativo MetodoAjuste"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithMetodoAjuste(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir, _
                                          Optional valActivoPasivoPatrimonio As Boolean)
   Dim nCount As Integer
   varComboBox.Clear
   nCount = 0
   If action = Buscar Then
      varComboBox.Text = gDefGen.getSearchAllComboBox()
      varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
      nCount = nCount + 1
   Else
      varComboBox.Text = enumMetodoAjusteToString(enum_MetodoAjuste.eMA_PORIPC)
   End If
      varComboBox.List(nCount) = enumMetodoAjusteToString(enum_MetodoAjuste.eMA_PORIPC)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumMetodoAjusteToString(enum_MetodoAjuste.eMA_MonedaExtranjera)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumMetodoAjusteToString(enum_MetodoAjuste.eMA_ClausuladeReajustabilidad)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumMetodoAjusteToString(enum_MetodoAjuste.eMA_Inventario)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumMetodoAjusteToString(enum_MetodoAjuste.eMA_Patrimonio)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumMetodoAjusteToString(enum_MetodoAjuste.Ema_Obrasenproceso)
      nCount = nCount + 1
   varComboBox.List(nCount) = enumMetodoAjusteToString(enum_MetodoAjuste.eMA_Monetaria)
      nCount = nCount + 1
   varComboBox.List(nCount) = enumMetodoAjusteToString(enum_MetodoAjuste.eMA_OtrasNoMonetarias)
'   nCount = nCount + 1
'   varComboBox.List(nCount) = enumMetodoAjusteToString(enum_MetodoAjuste.eMA_Titulo)
   varComboBox.ListIndex = 0    ' Se selecciona el valor por defecto
   'varComboBox.Width = gAPI.sugestedWidthForComboBox(varComboBox)
End Sub

Public Function enumTipoDeCuentaDpc10ToString(TipoDeCuentaDPC10 As enum_TipoDeCuentaDpc10) As String
   Select Case TipoDeCuentaDPC10
      Case ETDCD10_ACTIVO: enumTipoDeCuentaDpc10ToString = "Activo"
      Case Etdcd10_Pasivo: enumTipoDeCuentaDpc10ToString = "Pasivo"
      Case Etdcd10_Patrimonio: enumTipoDeCuentaDpc10ToString = "Patrimonio (Capital)"
      Case Else
         enumTipoDeCuentaDpc10ToString = enumTipoDeCuentaDpc10ToString(0)
   End Select
End Function

Public Function enum_TipoDeCuentaDpc10MonetariaToString(TipoDeCuentaDpc10Monetaria As enum_TipoDeCuentaDpc10Monetaria) As String
   Select Case TipoDeCuentaDpc10Monetaria
      Case ETCM_COSTO: enum_TipoDeCuentaDpc10MonetariaToString = "Costo De Venta"
      Case Etcm_Otras: enum_TipoDeCuentaDpc10MonetariaToString = "Otras"
      Case Else
         enum_TipoDeCuentaDpc10MonetariaToString = enum_TipoDeCuentaDpc10MonetariaToString(0)
   End Select
End Function


Public Function strTipoDeCuentaDpc10ToNum(ByVal TipoDeCuentaDPC10 As String, Optional ByVal ShowMessage As Boolean = True) As Integer
   Dim mEnum As enum_TipoDeCuentaDpc10
   Dim nCount As Integer
   nCount = 0
   For mEnum = ETDCD10_ACTIVO To Etdcd10_Patrimonio
      If UCase(TipoDeCuentaDPC10) = UCase(enumTipoDeCuentaDpc10ToString(mEnum)) Then
         strTipoDeCuentaDpc10ToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strTipoDeCuentaDpc10ToNum = 0
   If ShowMessage Then
      gMessage.ProgrammerMessage "Valor " & TipoDeCuentaDPC10 & " no existe en tipo enumerativo TipoDeCuentaDpc10"
   End If
h_EXIT:
   Exit Function
End Function
Public Function strTipoDeCuentaDpc10MonetariaToNum(ByVal TipoDeCuentaDpc10Monetaria As String, Optional ByVal ShowMessage As Boolean = True) As Integer
   Dim mEnum As enum_TipoDeCuentaDpc10Monetaria
   Dim nCount As Integer
   nCount = 0
   For mEnum = ETCM_COSTO To Etcm_Otras
      If UCase(TipoDeCuentaDpc10Monetaria) = UCase(enum_TipoDeCuentaDpc10MonetariaToString(mEnum)) Then
         strTipoDeCuentaDpc10MonetariaToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strTipoDeCuentaDpc10MonetariaToNum = 0
   If ShowMessage Then
      gMessage.ProgrammerMessage "Valor " & TipoDeCuentaDpc10Monetaria & " no existe en tipo enumerativo TipoDeCuentaDpc10Monetaria"
   End If
h_EXIT:
   Exit Function
End Function


Public Sub FillComboBoxWithTipoDeCuentaDpc10(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   varComboBox.Clear
   nCount = 0
   If action = Buscar Then
      varComboBox.Text = gDefGen.getSearchAllComboBox()
      varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
      nCount = nCount + 1
   Else
      varComboBox.Text = enumTipoDeCuentaDpc10ToString(enum_TipoDeCuentaDpc10.ETDCD10_ACTIVO)
   End If
   varComboBox.List(nCount) = enumTipoDeCuentaDpc10ToString(enum_TipoDeCuentaDpc10.ETDCD10_ACTIVO)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumTipoDeCuentaDpc10ToString(enum_TipoDeCuentaDpc10.Etdcd10_Pasivo)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumTipoDeCuentaDpc10ToString(enum_TipoDeCuentaDpc10.Etdcd10_Patrimonio)
      varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
End Sub

Public Sub FillComboBoxWithTipoDeCuentaDpc10Monetaria(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir, Optional valTextoCombo As String)
   Dim nCount As Integer
   varComboBox.Clear
   nCount = 0
   If action = Buscar Then
      varComboBox.Text = gDefGen.getSearchAllComboBox()
      varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
      nCount = nCount + 1
   Else
      varComboBox.Text = enum_TipoDeCuentaDpc10MonetariaToString(enum_TipoDeCuentaDpc10Monetaria.ETCM_COSTO)
   End If
   varComboBox.List(nCount) = enum_TipoDeCuentaDpc10MonetariaToString(ETCM_COSTO)
   nCount = nCount + 1
   varComboBox.List(nCount) = enum_TipoDeCuentaDpc10MonetariaToString(Etcm_Otras)
   varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
End Sub

Public Function enumTipoDeOtrasNoMonetariasToString(TipoDeOtrasNoMonetarias As enum_TipoDeOtrasNoMonetarias) As String
   Select Case TipoDeOtrasNoMonetarias
      Case eTDONM_Diferidos: enumTipoDeOtrasNoMonetariasToString = "Diferidos"
      Case Etdonm_Inversiones: enumTipoDeOtrasNoMonetariasToString = "Inversiones"
      Case Etdonm_Prepagados: enumTipoDeOtrasNoMonetariasToString = "Prepagados"
      Case Etdonm_Otras: enumTipoDeOtrasNoMonetariasToString = "Otras"
      Case Else
         enumTipoDeOtrasNoMonetariasToString = enumTipoDeOtrasNoMonetariasToString(0)
   End Select
End Function

Public Function strTipoDeOtrasNoMonetariasToNum(ByVal TipoDeOtrasNoMonetarias As String, Optional ByVal ShowMessage As Boolean = True) As Integer
   Dim mEnum As enum_TipoDeOtrasNoMonetarias
   Dim nCount As Integer
   nCount = 0
   For mEnum = eTDONM_Diferidos To Etdonm_Otras
      If UCase(TipoDeOtrasNoMonetarias) = UCase(enumTipoDeOtrasNoMonetariasToString(mEnum)) Then
         strTipoDeOtrasNoMonetariasToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strTipoDeOtrasNoMonetariasToNum = 0
   If ShowMessage Then
      gMessage.ProgrammerMessage "Valor " & TipoDeOtrasNoMonetarias & " no existe en tipo enumerativo TipoDeOtrasNoMonetarias"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithTipoDeOtrasNoMonetarias(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   varComboBox.Clear
   nCount = 0
   If action = Buscar Then
      varComboBox.Text = gDefGen.getSearchAllComboBox()
      varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
      nCount = nCount + 1
   Else
      varComboBox.Text = enumTipoDeOtrasNoMonetariasToString(enum_TipoDeOtrasNoMonetarias.eTDONM_Diferidos)
   End If
   varComboBox.List(nCount) = enumTipoDeOtrasNoMonetariasToString(enum_TipoDeOtrasNoMonetarias.eTDONM_Diferidos)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumTipoDeOtrasNoMonetariasToString(enum_TipoDeOtrasNoMonetarias.Etdonm_Inversiones)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumTipoDeOtrasNoMonetariasToString(enum_TipoDeOtrasNoMonetarias.Etdonm_Prepagados)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumTipoDeOtrasNoMonetariasToString(enum_TipoDeOtrasNoMonetarias.Etdonm_Otras)
   varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
End Sub

Public Function enumTipoDeInventarioToString(tipoDeInventario As enum_TipoDeInventario) As String
   Select Case tipoDeInventario
      Case eTD_prima: enumTipoDeInventarioToString = "Prima"
      Case Etd_Enproceso: enumTipoDeInventarioToString = "En Proceso"
      Case Etd_Terminados: enumTipoDeInventarioToString = "Terminados"
      Case Else
         enumTipoDeInventarioToString = enumTipoDeInventarioToString(0)
   End Select
End Function

Public Function strTipoDeInventarioToNum(ByVal tipoDeInventario As String, Optional ByVal ShowMessage As Boolean = True) As Integer
   Dim mEnum As enum_TipoDeInventario
   Dim nCount As Integer
   nCount = 0
   For mEnum = eTD_prima To Etd_Terminados
      If UCase(tipoDeInventario) = UCase(enumTipoDeInventarioToString(mEnum)) Then
         strTipoDeInventarioToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strTipoDeInventarioToNum = 0
   If ShowMessage Then
      gMessage.ProgrammerMessage "Valor " & tipoDeInventario & " no existe en tipo enumerativo TipoDeInventario"
   End If
   
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithTipoDeInventario(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   varComboBox.Clear
   nCount = 0
   If action = Buscar Then
      varComboBox.Text = gDefGen.getSearchAllComboBox()
      varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
      nCount = nCount + 1
   Else
      varComboBox.Text = enumTipoDeInventarioToString(enum_TipoDeInventario.eTD_prima)
   End If
   varComboBox.List(nCount) = enumTipoDeInventarioToString(enum_TipoDeInventario.eTD_prima)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumTipoDeInventarioToString(enum_TipoDeInventario.Etd_Enproceso)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumTipoDeInventarioToString(enum_TipoDeInventario.Etd_Terminados)
   varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   'varComboBox.Width = gAPI.sugestedWidthForComboBox(varComboBox)
End Sub

Public Function enumTipoPartidaAExportarToString(TipoPartidaAExportar As enum_TipoPartidaAExportar) As String
   Select Case TipoPartidaAExportar
      Case Etpae_Todaslaspartidas: enumTipoPartidaAExportarToString = "Todas Las Partidas"
      Case eTPAE_UnaPartida: enumTipoPartidaAExportarToString = "Una Partida"
      Case Else
         enumTipoPartidaAExportarToString = enumTipoPartidaAExportarToString(0)
   End Select
End Function

Public Function strTipoPartidaAExportarToNum(ByVal TipoPartidaAExportar As String, Optional ByVal ShowMessage As Boolean = True) As Integer
   Dim mEnum As enum_TipoPartidaAExportar
   Dim nCount As Integer
   nCount = 0
   For mEnum = Etpae_Todaslaspartidas To eTPAE_UnaPartida
      If UCase(TipoPartidaAExportar) = UCase(enumTipoPartidaAExportarToString(mEnum)) Then
         strTipoPartidaAExportarToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strTipoPartidaAExportarToNum = 0
   If ShowMessage Then
      gMessage.ProgrammerMessage "Valor " & TipoPartidaAExportar & " no existe en tipo enumerativo TipoPartidaAExportar"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithTipoPartidaAExportar(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   varComboBox.Clear
   nCount = 0
   If action = Buscar Then
      varComboBox.Text = gDefGen.getSearchAllComboBox()
      varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
      nCount = nCount + 1
   Else
      varComboBox.Text = enumTipoPartidaAExportarToString(enum_TipoPartidaAExportar.Etpae_Todaslaspartidas)
   End If
   varComboBox.List(nCount) = enumTipoPartidaAExportarToString(enum_TipoPartidaAExportar.Etpae_Todaslaspartidas)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumTipoPartidaAExportarToString(enum_TipoPartidaAExportar.eTPAE_UnaPartida)
   varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
End Sub

Public Function fEnumDpc10MetodoAjusteToStringInArray(Optional ByVal valPrepararParaSql As Boolean = False) As Variant
   Dim varEnum As enum_Dpc10MetodoAjuste
   Dim varArray() As String
   Dim varLimiteSuperior As Integer
   Dim nCount As Integer
   Dim varCaracterDelimitador As String
   On Error Resume Next
   varCaracterDelimitador = ""
   If valPrepararParaSql Then
      varCaracterDelimitador = "'"
   End If
   varLimiteSuperior = enum_Dpc10MetodoAjuste.eD10MA_OTRASNOMONETARIAS - enum_Dpc10MetodoAjuste.eD10MA_ACTIVOFIJO
   nCount = 0
   ReDim varArray(0 To varLimiteSuperior)
   For varEnum = enum_Dpc10MetodoAjuste.eD10MA_ACTIVOFIJO To enum_Dpc10MetodoAjuste.eD10MA_OTRASNOMONETARIAS
      varArray(nCount) = varCaracterDelimitador & enumDpc10MetodoAjusteToStringCorto(varEnum) & varCaracterDelimitador
      nCount = nCount + 1
   Next
   fEnumDpc10MetodoAjusteToStringInArray = varArray
   On Error GoTo 0
End Function

Public Function enumDpc10MetodoAjusteToStringCorto(PartidaDpc10 As enum_Dpc10MetodoAjuste) As String
   Select Case PartidaDpc10
      Case eD10MA_ACTIVOFIJO: enumDpc10MetodoAjusteToStringCorto = "Activo Fijo"
      Case eD10MA_MONEDAEXTRANJERA: enumDpc10MetodoAjusteToStringCorto = "Moneda Extranjera"
      Case eD10MA_CLAUSULADEREAJUSTABILIDAD: enumDpc10MetodoAjusteToStringCorto = "Clausula"
      Case eD10MA_INVENTARIO: enumDpc10MetodoAjusteToStringCorto = "Inventario"
      Case eD10MA_PATRIMONIO: enumDpc10MetodoAjusteToStringCorto = "Patrimonio Part. Dpc10"
      Case Ed10ma_Obrasenproceso: enumDpc10MetodoAjusteToStringCorto = "Obras"
      Case eD10MA_MONETARIA: enumDpc10MetodoAjusteToStringCorto = "Monetaria"
      Case eD10MA_OTRASNOMONETARIAS: enumDpc10MetodoAjusteToStringCorto = "No Monetaria"
   End Select
End Function
 
Public Function fEnumMetodoAjusteToStringInArray(Optional ByVal valPrepararParaSql As Boolean = False) As Variant
   Dim varEnum As enum_MetodoAjuste
   Dim varArray() As String
   Dim varLimiteSuperior As Integer
   Dim nCount As Integer
   Dim varCaracterDelimitador As String
   On Error Resume Next
   varCaracterDelimitador = ""
   If valPrepararParaSql Then
      varCaracterDelimitador = "'"
   End If
   varLimiteSuperior = enum_MetodoAjuste.eMA_OtrasNoMonetarias - enum_MetodoAjuste.eMA_PORIPC
   nCount = 0
   ReDim varArray(0 To varLimiteSuperior)
   For varEnum = enum_MetodoAjuste.eMA_PORIPC To enum_MetodoAjuste.eMA_OtrasNoMonetarias
      varArray(nCount) = varCaracterDelimitador & enumMetodoAjusteToStringCorto(varEnum) & varCaracterDelimitador
      nCount = nCount + 1
   Next
   fEnumMetodoAjusteToStringInArray = varArray
   On Error GoTo 0
End Function

Public Function enumMetodoAjusteToStringCorto(PartidaFiscal As enum_MetodoAjuste) As String
   Select Case PartidaFiscal
      Case eMA_PORIPC: enumMetodoAjusteToStringCorto = "Ipc"
      Case eMA_MonedaExtranjera: enumMetodoAjusteToStringCorto = "Monext Fiscal"
      Case eMA_ClausuladeReajustabilidad: enumMetodoAjusteToStringCorto = "Clausula Fiscal"
      Case eMA_Inventario: enumMetodoAjusteToStringCorto = "Inventario Fiscal"
      Case eMA_Patrimonio: enumMetodoAjusteToStringCorto = "Patrimonio Part. Fiscal"
      Case Ema_Obrasenproceso: enumMetodoAjusteToStringCorto = "Obras Proceso"
      Case eMA_Monetaria: enumMetodoAjusteToStringCorto = "Monetaria Fiscal"
      Case eMA_OtrasNoMonetarias: enumMetodoAjusteToStringCorto = "No Monetaria Fiscal"
   End Select
End Function
Public Function fEnumTipoDeCuentaDpc10ToStringInArray(Optional ByVal valPrepararParaSql As Boolean = False) As Variant
   Dim varEnum As enum_TipoDeCuentaDpc10
   Dim varArray() As String
   Dim varLimiteSuperior As Integer
   Dim nCount As Integer
   Dim varCaracterDelimitador As String
   On Error Resume Next
   varCaracterDelimitador = ""
   If valPrepararParaSql Then
      varCaracterDelimitador = "'"
   End If
   varLimiteSuperior = enum_TipoDeCuentaDpc10.Etdcd10_Patrimonio - enum_TipoDeCuentaDpc10.ETDCD10_ACTIVO
   nCount = 0
   ReDim varArray(0 To varLimiteSuperior)
   For varEnum = enum_TipoDeCuentaDpc10.ETDCD10_ACTIVO To enum_TipoDeCuentaDpc10.Etdcd10_Patrimonio
      varArray(nCount) = varCaracterDelimitador & enumTipoDeCuentaDpc10ToStringCorto(varEnum) & varCaracterDelimitador
      nCount = nCount + 1
   Next
   fEnumTipoDeCuentaDpc10ToStringInArray = varArray
   On Error GoTo 0
End Function

Public Function enumTipoDeCuentaDpc10ToStringCorto(CuentaDpc10 As enum_TipoDeCuentaDpc10) As String
   Select Case CuentaDpc10
      Case ETDCD10_ACTIVO: enumTipoDeCuentaDpc10ToStringCorto = "Activo"
      Case Etdcd10_Pasivo: enumTipoDeCuentaDpc10ToStringCorto = "Pasivo"
      Case Etdcd10_Patrimonio: enumTipoDeCuentaDpc10ToStringCorto = "Patrimonio Cta. Dpc10"
   End Select
End Function

Public Function fEnumTipoDeCuentaFiscalToStringInArray(Optional ByVal valPrepararParaSql As Boolean = False) As Variant
   Dim varEnum As enum_TipoDeCuentaDpc10
   Dim varArray() As String
   Dim varLimiteSuperior As Integer
   Dim nCount As Integer
   Dim varCaracterDelimitador As String
   On Error Resume Next
   varCaracterDelimitador = ""
   If valPrepararParaSql Then
      varCaracterDelimitador = "'"
   End If
   varLimiteSuperior = enum_TipoDeCuentaDpc10.Etdcd10_Patrimonio - enum_TipoDeCuentaDpc10.ETDCD10_ACTIVO
   nCount = 0
   ReDim varArray(0 To varLimiteSuperior)
   For varEnum = enum_TipoDeCuentaDpc10.ETDCD10_ACTIVO To enum_TipoDeCuentaDpc10.Etdcd10_Patrimonio
      varArray(nCount) = varCaracterDelimitador & enumTipoDeCuentaFiscalToStringCorto(varEnum) & varCaracterDelimitador
      nCount = nCount + 1
   Next
   fEnumTipoDeCuentaFiscalToStringInArray = varArray
   On Error GoTo 0
End Function

Public Function enumTipoDeCuentaFiscalToStringCorto(CuentaFiscal As enum_TipoDeCuentaDpc10) As String
   Select Case CuentaFiscal
      Case ETDCD10_ACTIVO: enumTipoDeCuentaFiscalToStringCorto = "Activo Fiscal"
      Case Etdcd10_Pasivo: enumTipoDeCuentaFiscalToStringCorto = "Pasivo Fiscal"
      Case Etdcd10_Patrimonio: enumTipoDeCuentaFiscalToStringCorto = "Patrimonio Cta. Fiscal"
   End Select
End Function
 

Public Function fEnumTipoDeCuentaDpc10MonetariaToStringInArray(Optional ByVal valPrepararParaSql As Boolean = False) As Variant
   Dim varEnum As enum_TipoDeCuentaDpc10Monetaria
   Dim varArray() As String
   Dim varLimiteSuperior As Integer
   Dim nCount As Integer
   Dim varCaracterDelimitador As String
   On Error Resume Next
   varCaracterDelimitador = ""
   If valPrepararParaSql Then
      varCaracterDelimitador = "'"
   End If
   varLimiteSuperior = enum_TipoDeCuentaDpc10Monetaria.Etcm_Otras - enum_TipoDeCuentaDpc10Monetaria.ETCM_COSTO
   nCount = 0
   ReDim varArray(0 To varLimiteSuperior)
   For varEnum = enum_TipoDeCuentaDpc10Monetaria.ETCM_COSTO To enum_TipoDeCuentaDpc10Monetaria.Etcm_Otras
      varArray(nCount) = varCaracterDelimitador & enumTipoDeCuentaDpc10MonetariaToStringCorto(varEnum) & varCaracterDelimitador
      nCount = nCount + 1
   Next
   fEnumTipoDeCuentaDpc10MonetariaToStringInArray = varArray
   On Error GoTo 0
End Function

Public Function enumTipoDeCuentaDpc10MonetariaToStringCorto(CuentaDpc10Monetaria As enum_TipoDeCuentaDpc10) As String
   Select Case CuentaDpc10Monetaria
      Case ETCM_COSTO: enumTipoDeCuentaDpc10MonetariaToStringCorto = "Costo"
      Case Etcm_Otras: enumTipoDeCuentaDpc10MonetariaToStringCorto = "Otras"
   End Select
End Function

Public Function fEnumTipoDeInventarioToStringInArray(Optional ByVal valPrepararParaSql As Boolean = False) As Variant
   Dim varEnum As enum_TipoDeInventario
   Dim varArray() As String
   Dim varLimiteSuperior As Integer
   Dim nCount As Integer
   Dim varCaracterDelimitador As String
   On Error Resume Next
   varCaracterDelimitador = ""
   If valPrepararParaSql Then
      varCaracterDelimitador = "'"
   End If
   varLimiteSuperior = enum_TipoDeInventario.Etd_Terminados - enum_TipoDeInventario.eTD_prima
   nCount = 0
   ReDim varArray(0 To varLimiteSuperior)
   For varEnum = enum_TipoDeInventario.eTD_prima To enum_TipoDeInventario.Etd_Terminados
      varArray(nCount) = varCaracterDelimitador & enumTipoDeInventarioToStringCorto(varEnum) & varCaracterDelimitador
      nCount = nCount + 1
   Next
   fEnumTipoDeInventarioToStringInArray = varArray
   On Error GoTo 0
End Function

Public Function enumTipoDeInventarioToStringCorto(TipoInventario As enum_TipoDeInventario) As String
   Select Case TipoInventario
      Case eTD_prima: enumTipoDeInventarioToStringCorto = "Prima"
      Case Etd_Enproceso: enumTipoDeInventarioToStringCorto = "Proceso"
      Case Etd_Terminados: enumTipoDeInventarioToStringCorto = "Terminados"
   End Select
End Function

Public Function fEnumTipoDeOtrasNoMonetariasToStringInArray(Optional ByVal valPrepararParaSql As Boolean = False) As Variant
   Dim varEnum As enum_TipoDeOtrasNoMonetarias
   Dim varArray() As String
   Dim varLimiteSuperior As Integer
   Dim nCount As Integer
   Dim varCaracterDelimitador As String
   On Error Resume Next
   varCaracterDelimitador = ""
   If valPrepararParaSql Then
      varCaracterDelimitador = "'"
   End If
   varLimiteSuperior = enum_TipoDeOtrasNoMonetarias.Etdonm_Otras - enum_TipoDeOtrasNoMonetarias.eTDONM_Diferidos
   nCount = 0
   ReDim varArray(0 To varLimiteSuperior)
   For varEnum = enum_TipoDeOtrasNoMonetarias.eTDONM_Diferidos To enum_TipoDeOtrasNoMonetarias.Etdonm_Otras
      varArray(nCount) = varCaracterDelimitador & enumTipoDeOtrasNoMonetariasToStringCorto(varEnum) & varCaracterDelimitador
      nCount = nCount + 1
   Next
   fEnumTipoDeOtrasNoMonetariasToStringInArray = varArray
   On Error GoTo 0
End Function

Public Function enumTipoDeOtrasNoMonetariasToStringCorto(NoMonetarias As enum_TipoDeInventario) As String
   Select Case NoMonetarias
      Case eTDONM_Diferidos: enumTipoDeOtrasNoMonetariasToStringCorto = "Diferidos"
      Case Etdonm_Inversiones: enumTipoDeOtrasNoMonetariasToStringCorto = "Inversiones"
      Case Etdonm_Prepagados: enumTipoDeOtrasNoMonetariasToStringCorto = "Prepagados"
      Case Etdonm_Otras: enumTipoDeOtrasNoMonetariasToStringCorto = "Otras"
   End Select
End Function
Public Function stringCortoDpc10MetodoAjusteToNum(ByVal Dpc10MetodoAjuste As String, _
      Optional ByVal ShowMessage As Boolean = True) As Integer
   Dim mEnum As enum_Dpc10MetodoAjuste
   Dim nCount As Integer
   nCount = 0
   For mEnum = eD10MA_ACTIVOFIJO To eD10MA_OTRASNOMONETARIAS
      If UCase(Dpc10MetodoAjuste) = UCase(enumDpc10MetodoAjusteToStringCorto(mEnum)) Then
         stringCortoDpc10MetodoAjusteToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   stringCortoDpc10MetodoAjusteToNum = 6
   If ShowMessage Then
      gMessage.ProgrammerMessage "Valor " & Dpc10MetodoAjuste & " no existe en tipo enumerativo Dpc10MetodoAjuste"
   End If
h_EXIT:
   Exit Function
End Function
Public Function stringCortoMetodoAjusteToNum(ByVal MetodoAjuste As String, _
      Optional ByVal ShowMessage As Boolean = True) As Integer
   Dim mEnum As enum_MetodoAjuste
   Dim nCount As Integer
   nCount = 0
   For mEnum = eD10MA_ACTIVOFIJO To eD10MA_OTRASNOMONETARIAS
      If UCase(MetodoAjuste) = UCase(enumMetodoAjusteToStringCorto(mEnum)) Then
         stringCortoMetodoAjusteToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   stringCortoMetodoAjusteToNum = 6
   If ShowMessage Then
      gMessage.ProgrammerMessage "Valor " & MetodoAjuste & " no existe en tipo enumerativo MetodoAjuste"
   End If
h_EXIT:
   Exit Function
End Function
Public Function stringCortoTipoDeCuentaDpc10ToNum(ByVal CuentaDpc10 As String, _
      Optional ByVal ShowMessage As Boolean = True) As Integer
   Dim mEnum As enum_TipoDeCuentaDpc10
   Dim nCount As Integer
   nCount = 0
   For mEnum = ETDCD10_ACTIVO To Etdcd10_Patrimonio
      If UCase(CuentaDpc10) = UCase(enumTipoDeCuentaDpc10ToStringCorto(mEnum)) Then
         stringCortoTipoDeCuentaDpc10ToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   stringCortoTipoDeCuentaDpc10ToNum = 0
   If ShowMessage Then
      gMessage.ProgrammerMessage "Valor " & CuentaDpc10 & " no existe en tipo enumerativo TipoDeCuentaDpc10"
   End If
h_EXIT:
   Exit Function
End Function
Public Function stringCortoTipoDeCuentaDpc10MonetariaToNum(ByVal CuentaDpc10Monetaria As String, _
      Optional ByVal ShowMessage As Boolean = True) As Integer
   Dim mEnum As enum_TipoDeCuentaDpc10
   Dim nCount As Integer
   nCount = 0
   For mEnum = ETCM_COSTO To Etcm_Otras
      If UCase(CuentaDpc10Monetaria) = UCase(enumTipoDeCuentaDpc10MonetariaToStringCorto(mEnum)) Then
         stringCortoTipoDeCuentaDpc10MonetariaToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   stringCortoTipoDeCuentaDpc10MonetariaToNum = 0
   If ShowMessage Then
      gMessage.ProgrammerMessage "Valor " & CuentaDpc10Monetaria & " no existe en tipo enumerativo TipoDeCuentaDpc10Monetaria"
   End If
h_EXIT:
   Exit Function
End Function

Public Function stringCortoTipoDeInventarioToNum(ByVal TipoInventario As String, _
      Optional ByVal ShowMessage As Boolean = True) As Integer
   Dim mEnum As enum_TipoDeInventario
   Dim nCount As Integer
   nCount = 0
   For mEnum = eTD_prima To Etd_Terminados
      If UCase(TipoInventario) = UCase(enumTipoDeInventarioToStringCorto(mEnum)) Then
         stringCortoTipoDeInventarioToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   stringCortoTipoDeInventarioToNum = 0
   If ShowMessage Then
      gMessage.ProgrammerMessage "Valor " & TipoInventario & " no existe en tipo enumerativo TipoDeInventario"
   End If
h_EXIT:
   Exit Function
End Function


Public Function stringCortoTipoDeOtrasNoMomentariasToNum(ByVal OtrasNoMomentarias As String, _
      Optional ByVal ShowMessage As Boolean = True) As Integer
   Dim mEnum As enum_TipoDeOtrasNoMonetarias
   Dim nCount As Integer
   nCount = 0
   For mEnum = eTDONM_Diferidos To Etdonm_Otras
      If UCase(OtrasNoMomentarias) = UCase(enumTipoDeOtrasNoMonetariasToStringCorto(mEnum)) Then
         stringCortoTipoDeOtrasNoMomentariasToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   stringCortoTipoDeOtrasNoMomentariasToNum = 0
   If ShowMessage Then
      gMessage.ProgrammerMessage "Valor " & OtrasNoMomentarias & " no existe en tipo enumerativo TipoDeOtrasNoMonetarias"
   End If
h_EXIT:
   Exit Function
End Function
Public Function stringCortoTipoDeCuentaFiscalToNum(ByVal CuentaFiscal As String, _
      Optional ByVal ShowMessage As Boolean = True) As Integer
   Dim mEnum As enum_TipoDeCuentaDpc10
   Dim nCount As Integer
   nCount = 0
   For mEnum = ETDCD10_ACTIVO To Etdcd10_Patrimonio
      If UCase(CuentaFiscal) = UCase(enumTipoDeCuentaFiscalToStringCorto(mEnum)) Then
         stringCortoTipoDeCuentaFiscalToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   stringCortoTipoDeCuentaFiscalToNum = 0
   If ShowMessage Then
      gMessage.ProgrammerMessage "Valor " & CuentaFiscal & " no existe en tipo enumerativo TipoDeCuentaDpc10"
   End If
h_EXIT:
   Exit Function
End Function

Public Function fEnumTipoReflejaToStringInArray(Optional ByVal valPrepararParaSql As Boolean = False) As Variant
   Dim varEnum As enum_TipoRefleja
   Dim varArray() As String
   Dim varLimiteSuperior As Integer
   Dim nCount As Integer
   Dim varCaracterDelimitador As String
   On Error Resume Next
   varCaracterDelimitador = ""
   If valPrepararParaSql Then
      varCaracterDelimitador = "'"
   End If
   varLimiteSuperior = eTR_DESTINO_REFLEJA - eTR_NoAplicar_REFLEJA
   nCount = 0
   ReDim varArray(0 To varLimiteSuperior)
   For varEnum = eTR_NoAplicar_REFLEJA To eTR_DESTINO_REFLEJA
      varArray(nCount) = varCaracterDelimitador & enumTipoReflejaToString(varEnum) & varCaracterDelimitador
      nCount = nCount + 1
   Next
   fEnumTipoReflejaToStringInArray = varArray
   On Error GoTo 0
End Function

Public Function enumTipoCuentaToString(TipoCuenta As enum_TipoCuenta) As String
   Select Case TipoCuenta
      Case eTC_Activo: enumTipoCuentaToString = "ACTIVO"
      Case eTC_Pasivo: enumTipoCuentaToString = "PASIVO"
      Case eTC_Capital: enumTipoCuentaToString = "CAPITAL"
      Case eTC_Ingresos: enumTipoCuentaToString = "INGRESOS"
      Case eTC_Costos: enumTipoCuentaToString = "COSTOS"
      Case eTC_Gasto: enumTipoCuentaToString = "GASTOS"
      Case eTC_OrdenDeudora: enumTipoCuentaToString = "CUENTAS DE ORDEN"
      Case eTC_OrdenAcreedora: enumTipoCuentaToString = "CUENTAS DE ORDEN PER CONTRA"
      Case Else
         enumTipoCuentaToString = enumTipoCuentaToString(0)
   End Select
End Function

Public Function strTipoCuentaToNum(ByVal TipoCuenta As String, Optional ByVal ShowMessage As Boolean = True) As Integer
   Dim mEnum As enum_TipoCuenta
   Dim nCount As Integer
   nCount = 0
   For mEnum = eTC_Activo To eTC_OrdenAcreedora
      If UCase(TipoCuenta) = UCase(enumTipoCuentaToString(mEnum)) Then
         strTipoCuentaToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strTipoCuentaToNum = 0
   If ShowMessage Then
      gMessage.ProgrammerMessage "Valor " & TipoCuenta & " no existe en tipo enumerativo TipoCuenta"
   End If
h_EXIT:
   Exit Function
End Function
Public Sub FillComboBoxWithTipoCuenta(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   varComboBox.Clear
   nCount = 0
   If action = Buscar Then
      varComboBox.Text = gDefGen.getSearchAllComboBox()
      varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
      nCount = nCount + 1
   Else
      varComboBox.Text = enumTipoCuentaToString(enum_TipoCuenta.eTC_Activo)
   End If
   varComboBox.List(nCount) = enumTipoCuentaToString(enum_TipoCuenta.eTC_Activo)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumTipoCuentaToString(enum_TipoCuenta.eTC_Pasivo)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumTipoCuentaToString(enum_TipoCuenta.eTC_Capital)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumTipoCuentaToString(enum_TipoCuenta.eTC_Ingresos)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumTipoCuentaToString(enum_TipoCuenta.eTC_Costos)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumTipoCuentaToString(enum_TipoCuenta.eTC_Gasto)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumTipoCuentaToString(enum_TipoCuenta.eTC_OrdenDeudora)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumTipoCuentaToString(enum_TipoCuenta.eTC_OrdenAcreedora)
   varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
End Sub

Public Function enumTipoDeCompraEspecialToString(TipoDeCompraEspecial As enum_TipoDeCompraEspecial) As String
   Select Case TipoDeCompraEspecial
      Case eTDCE_NoEs: enumTipoDeCompraEspecialToString = "No Es"
      Case eTDCE_Informativa: enumTipoDeCompraEspecialToString = "Informativa"
      Case eTDCE_SueldosySalarios: enumTipoDeCompraEspecialToString = "Sueldos y  Salarios"
      Case Else
         enumTipoDeCompraEspecialToString = enumTipoDeCompraEspecialToString(0)
   End Select
End Function

Public Function strTipoDeCompraEspecialToNum(ByVal TipoDeCompraEspecial As String, Optional ByVal ShowMessage As Boolean = True) As Integer
   Dim mEnum As enum_TipoDeCompraEspecial
   Dim nCount As Integer
   nCount = 0
   For mEnum = eTDCE_NoEs To eTDCE_SueldosySalarios
      If UCase(TipoDeCompraEspecial) = UCase(enumTipoDeCompraEspecialToString(mEnum)) Then
         strTipoDeCompraEspecialToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strTipoDeCompraEspecialToNum = 0
   If ShowMessage Then
      gMessage.ProgrammerMessage "Valor " & TipoDeCompraEspecial & " no existe en tipo enumerado TipoDeCompraEspecial"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithTipoDeCompraEspecial(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   varComboBox.Clear
   nCount = 0
   If action = Buscar Then
      varComboBox.Text = gDefGen.getSearchAllComboBox()
      varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
      nCount = nCount + 1
   Else
      varComboBox.Text = enumTipoDeCompraEspecialToString(enum_TipoDeCompraEspecial.eTDCE_NoEs)
   End If
   varComboBox.List(nCount) = enumTipoDeCompraEspecialToString(enum_TipoDeCompraEspecial.eTDCE_NoEs)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumTipoDeCompraEspecialToString(enum_TipoDeCompraEspecial.eTDCE_Informativa)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumTipoDeCompraEspecialToString(enum_TipoDeCompraEspecial.eTDCE_SueldosySalarios)
   varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
End Sub

Public Function enumActividadFlujoEfectivoToString(ActividadFlujoEfectivo As enum_ActividadFlujoEfectivo) As String
   Select Case ActividadFlujoEfectivo
      Case eAFE_Provisiones: enumActividadFlujoEfectivoToString = "Provisiones"
      Case eAFE_Inversion: enumActividadFlujoEfectivoToString = "Inversión"
      Case eAFE_Financieras: enumActividadFlujoEfectivoToString = "Financieras"
      Case eAFE_Efectivo: enumActividadFlujoEfectivoToString = "Efectivo"
      Case Else
         enumActividadFlujoEfectivoToString = enumActividadFlujoEfectivoToString(0)
   End Select
End Function

Public Function strActividadFlujoEfectivoToNum(ByVal ActividadFlujoEfectivo As String, Optional ByVal ShowMessage As Boolean = True) As Integer
   Dim mEnum As enum_ActividadFlujoEfectivo
   Dim nCount As Integer
   nCount = 0
   For mEnum = eAFE_Provisiones To eAFE_Efectivo
      If UCase(ActividadFlujoEfectivo) = UCase(enumActividadFlujoEfectivoToString(mEnum)) Then
         strActividadFlujoEfectivoToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strActividadFlujoEfectivoToNum = 0
   If ShowMessage Then
      gMessage.ProgrammerMessage "Valor " & ActividadFlujoEfectivo & " no existe en tipo enumerativo ActividadFlujoEfectivo"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithActividadFlujoEfectivo(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   varComboBox.Clear
   nCount = 0
   If action = Buscar Then
      varComboBox.Text = gDefgen.getSearchAllComboBox()
      varComboBox.List(nCount) = gDefgen.getSearchAllComboBox()
      nCount = nCount + 1
   Else
      varComboBox.Text = enumActividadFlujoEfectivoToString(enum_ActividadFlujoEfectivo.eAFE_Provisiones)
   End If
   varComboBox.List(nCount) = enumActividadFlujoEfectivoToString(enum_ActividadFlujoEfectivo.eAFE_Provisiones)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumActividadFlujoEfectivoToString(enum_ActividadFlujoEfectivo.eAFE_Inversion)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumActividadFlujoEfectivoToString(enum_ActividadFlujoEfectivo.eAFE_Financieras)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumActividadFlujoEfectivoToString(enum_ActividadFlujoEfectivo.eAFE_Efectivo)
   varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   varComboBox.Width = gAPI.sugestedWidthForComboBox(varComboBox)
End Sub

Public Function enumTipoCuentaParaCostoToString(TipoCuentaParaCosto As enum_TipoCuentaParaCosto) As String
   Select Case TipoCuentaParaCosto
      Case eTCC_SinAsignar: enumTipoCuentaParaCostoToString = "Sin Asignar"
      Case eTCC_Ingreso: enumTipoCuentaParaCostoToString = "Ingreso"
      Case eTCC_Costo: enumTipoCuentaParaCostoToString = "Costo"
      Case eTCC_gasto: enumTipoCuentaParaCostoToString = "Gasto"
      Case eTCC_NoDeducible: enumTipoCuentaParaCostoToString = "No Deducible"
      Case Else
         enumTipoCuentaParaCostoToString = enumTipoCuentaParaCostoToString(0)
   End Select
End Function

Public Function strTipoCuentaParaCostoToNum(ByVal TipoCuentaParaCosto As String, Optional ByVal ShowMessage As Boolean = True) As Integer
   Dim mEnum As enum_TipoCuentaParaCosto
   Dim nCount As Integer
   nCount = 0
   For mEnum = eTCC_SinAsignar To eTCC_NoDeducible
      If UCase(TipoCuentaParaCosto) = UCase(enumTipoCuentaParaCostoToString(mEnum)) Then
         strTipoCuentaParaCostoToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strTipoCuentaParaCostoToNum = 0
   If ShowMessage Then
      gMessage.ProgrammerMessage "Valor " & TipoCuentaParaCosto & " no existe en tipo enumerativo TipoCuentaParaCoto"
   End If
   
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithTipoCuentaParaCosto(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir _
                 , Optional ByVal AgregarSinGrupo As Boolean = False)
   Dim nCount As Integer
   varComboBox.Clear
   
   nCount = 0
   If action = Buscar Then
      varComboBox.Text = gDefGen.getSearchAllComboBox()
      varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
      nCount = nCount + 1
   Else
      varComboBox.Text = enumTipoCuentaParaCostoToString(eTCC_SinAsignar)
   End If
   
   varComboBox.List(nCount) = enumTipoCuentaParaCostoToString(eTCC_SinAsignar)
   
   nCount = nCount + 1
   varComboBox.List(nCount) = enumTipoCuentaParaCostoToString(eTCC_Ingreso)
   
   nCount = nCount + 1
   varComboBox.List(nCount) = enumTipoCuentaParaCostoToString(eTCC_Costo)
   
   nCount = nCount + 1
   varComboBox.List(nCount) = enumTipoCuentaParaCostoToString(eTCC_gasto)
   
   nCount = nCount + 1
   varComboBox.List(nCount) = enumTipoCuentaParaCostoToString(eTCC_NoDeducible)
   varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
   varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
End Sub

Public Function enumIngresosPropiosActividad2014ToString(ingresosPropiosActividad2014 As enum_IngresosPropiosActividad2014) As String
   Select Case ingresosPropiosActividad2014
      Case eIP2014_1VentasExportacion: enumIngresosPropiosActividad2014ToString = "1.Ventas de Exportación"
      Case eIP2014_2VtasBienesSectorPublico: enumIngresosPropiosActividad2014ToString = "2.Ventas Bienes al Sector Público"
      Case eIP2014_3VtasBienesSectorPrivado: enumIngresosPropiosActividad2014ToString = "3.Ventas Bienes al Sector Privado"
      Case eIP2014_4VtasInmueblesCredito: enumIngresosPropiosActividad2014ToString = "4.Ventas de Inmuebles a Crédito"
      Case eIP2014_5VtasInmueblesContado: enumIngresosPropiosActividad2014ToString = "5.Ventas de Inmuebles de Contado"
      Case eIP2014_6ServiciosExportacion: enumIngresosPropiosActividad2014ToString = "6.Servicios de Exportacíón"
      Case eIP2014_7ServiciosSectorPublico: enumIngresosPropiosActividad2014ToString = "7.Servicios Prestados al S.Público"
      Case eIP2014_8ServiciosSectorPrivado: enumIngresosPropiosActividad2014ToString = "8.Servicios Prestados al S.Privado"
      Case eIP2014_9Honorarios: enumIngresosPropiosActividad2014ToString = "9.Honorarios Profesionales"
      Case eIP2014_10RegaliasNoMineras: enumIngresosPropiosActividad2014ToString = "10.Regalías No Mineras y Partic. Análogas"
      Case eIP2014_11RegaliasMineras: enumIngresosPropiosActividad2014ToString = "11.Regalías Mineras y Partic. Análogas"
      Case eIP2014_12RegaliasHidrocarburos: enumIngresosPropiosActividad2014ToString = "12.Regalías de Hidrocarburos"
      Case eIP2014_13InteresesSobreCreditos: enumIngresosPropiosActividad2014ToString = "13.Intereses sobre Créditos"
      Case eIP2014_14InteresesVarios: enumIngresosPropiosActividad2014ToString = "14.Intereses Varios"
      Case eIP2014_15IngresosVtaAcciones: enumIngresosPropiosActividad2014ToString = "15.Ingresos por Venta de Acciones"
      Case eIP2014_16EnriqConsorcios: enumIngresosPropiosActividad2014ToString = "16.Enriquec. por partic. en consorcios"
      Case eIP2014_17ArrendBienesMuebles: enumIngresosPropiosActividad2014ToString = "17.Arrend./Subarrend. Bienes Muebles"
      Case eIP2014_18ArrendBienesInmuebles: enumIngresosPropiosActividad2014ToString = "18.Arrend./Subarrend. Bienes Inmuebles"
      Case eIP2014_19OtrosIngresos: enumIngresosPropiosActividad2014ToString = "19.Otros Ingresos"
      Case eIP2014_20IngresosSujetosIP: enumIngresosPropiosActividad2014ToString = "20.Ingresos sujetos a IP"
      Case eIP2014_21MenosDevolucionesRebajas: enumIngresosPropiosActividad2014ToString = "21.Menos: Devoluciones, Rebajas y Descuentos"
      Case Else
      enumIngresosPropiosActividad2014ToString = enumIngresosPropiosActividad2014ToString(0)
   End Select
End Function
Public Function strIngresosPropiosActividad2014ToNum(ByVal ingresosPropiosActividad2014 As String, _
   Optional ByVal ShowMessage As Boolean = True) As Integer
   Dim mEnum As enum_IngresosPropiosActividad2014
   Dim nCount As Integer
   nCount = 0
   For mEnum = eIP2014_1VentasExportacion To eIP2014_21MenosDevolucionesRebajas
   If UCase(ingresosPropiosActividad2014) = UCase(enumIngresosPropiosActividad2014ToString(mEnum)) Then
      strIngresosPropiosActividad2014ToNum = nCount
      GoTo h_EXIT
   End If
   nCount = nCount + 1
   Next
   strIngresosPropiosActividad2014ToNum = 0
   If ShowMessage Then
      gMessage.ProgrammerMessage "Valor " & ingresosPropiosActividad2014 & " no existe en tipo enumerativo ingresosPropiosActividad"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithIngresosPropiosActividad2014(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   varComboBox.Clear
   nCount = 0
   If action = Buscar Then
      varComboBox.Text = gDefGen.getSearchAllComboBox()
      varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
      nCount = nCount + 1
   Else
      varComboBox.Text = enumIngresosPropiosActividad2014ToString(enum_IngresosPropiosActividad2014.eIP2014_1VentasExportacion)
   End If
   varComboBox.List(nCount) = enumIngresosPropiosActividad2014ToString(enum_IngresosPropiosActividad2014.eIP2014_1VentasExportacion)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumIngresosPropiosActividad2014ToString(enum_IngresosPropiosActividad2014.eIP2014_2VtasBienesSectorPublico)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumIngresosPropiosActividad2014ToString(enum_IngresosPropiosActividad2014.eIP2014_3VtasBienesSectorPrivado)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumIngresosPropiosActividad2014ToString(enum_IngresosPropiosActividad2014.eIP2014_4VtasInmueblesCredito)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumIngresosPropiosActividad2014ToString(enum_IngresosPropiosActividad2014.eIP2014_5VtasInmueblesContado)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumIngresosPropiosActividad2014ToString(enum_IngresosPropiosActividad2014.eIP2014_6ServiciosExportacion)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumIngresosPropiosActividad2014ToString(enum_IngresosPropiosActividad2014.eIP2014_7ServiciosSectorPublico)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumIngresosPropiosActividad2014ToString(enum_IngresosPropiosActividad2014.eIP2014_8ServiciosSectorPrivado)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumIngresosPropiosActividad2014ToString(enum_IngresosPropiosActividad2014.eIP2014_9Honorarios)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumIngresosPropiosActividad2014ToString(enum_IngresosPropiosActividad2014.eIP2014_10RegaliasNoMineras)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumIngresosPropiosActividad2014ToString(enum_IngresosPropiosActividad2014.eIP2014_11RegaliasMineras)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumIngresosPropiosActividad2014ToString(enum_IngresosPropiosActividad2014.eIP2014_12RegaliasHidrocarburos)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumIngresosPropiosActividad2014ToString(enum_IngresosPropiosActividad2014.eIP2014_13InteresesSobreCreditos)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumIngresosPropiosActividad2014ToString(enum_IngresosPropiosActividad2014.eIP2014_14InteresesVarios)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumIngresosPropiosActividad2014ToString(enum_IngresosPropiosActividad2014.eIP2014_15IngresosVtaAcciones)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumIngresosPropiosActividad2014ToString(enum_IngresosPropiosActividad2014.eIP2014_16EnriqConsorcios)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumIngresosPropiosActividad2014ToString(enum_IngresosPropiosActividad2014.eIP2014_17ArrendBienesMuebles)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumIngresosPropiosActividad2014ToString(enum_IngresosPropiosActividad2014.eIP2014_18ArrendBienesInmuebles)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumIngresosPropiosActividad2014ToString(enum_IngresosPropiosActividad2014.eIP2014_19OtrosIngresos)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumIngresosPropiosActividad2014ToString(enum_IngresosPropiosActividad2014.eIP2014_20IngresosSujetosIP)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumIngresosPropiosActividad2014ToString(enum_IngresosPropiosActividad2014.eIP2014_21MenosDevolucionesRebajas)
   varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
   varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
End Sub
Public Function enumCostoDeVentas2014ToString(costoDeVentas2014 As enum_CostoDeVentas2014) As String
   Select Case costoDeVentas2014
      Case eCD2014_23INVENTARIOINICIAL: enumCostoDeVentas2014ToString = "23.Inventario Inicial"
      Case eCD2014_24ComprasNetasNacionales: enumCostoDeVentas2014ToString = "24.Compras Netas Nacionales"
      Case eCD2014_25ComprasNetasExterior: enumCostoDeVentas2014ToString = "25.Compras Netas Exterior"
      Case eCD2014_28InventarioFinal: enumCostoDeVentas2014ToString = "28.Menos: Inventario Final"
      Case eCD2014_30InventarioInicialMP: enumCostoDeVentas2014ToString = "30.Inventario Inicial de Materias Primas"
      Case eCD2014_31ComprasNetasMP: enumCostoDeVentas2014ToString = "31.Compras Netas de Materias Primas"
      Case eCD2014_32InventarioFinalMP: enumCostoDeVentas2014ToString = "32.Menos: Inventario Final Materias Primas"
      Case eCD2014_33ManoObraDirecta: enumCostoDeVentas2014ToString = "33.Mano de Obra Directa"
      Case eCD2014_35OtrasRemuneraciones: enumCostoDeVentas2014ToString = "35.Otras Remuneraciones"
      Case eCD2014_36Contribuciones: enumCostoDeVentas2014ToString = "36.Contribuciones"
      Case eCD2014_37ComprasBienesServicios: enumCostoDeVentas2014ToString = "37.Compras de Bienes y Servicios"
      Case eCD2014_38InventarioInicialPP: enumCostoDeVentas2014ToString = "38.Inventario Inicial de Productos en Proceso"
      Case eCD2014_39InventarioFinalPP: enumCostoDeVentas2014ToString = "39.Menos: Inventario Final Productos en Proceso"
      Case eCD2014_40InventarioInicialPT: enumCostoDeVentas2014ToString = "40.Inventario Inicial de Productos Terminados"
      Case eCD2014_41InventarioFinalPT: enumCostoDeVentas2014ToString = "41.Menos: Inventario Final Productos Terminados"
      Case eCD2014_43CostoAdqBienesInmueblesVendidos: enumCostoDeVentas2014ToString = "43.Costo Adquisición Bienes Inmuebles Vendidos"
      Case eCD2014_44MejorasInmueblesVendidos: enumCostoDeVentas2014ToString = "44.Mejoras efectuadas Inmuebles Vendidos"
      Case eCD2014_45GastosRegistroAdqInmueblesVendidos: enumCostoDeVentas2014ToString = "45.Gastos Registro Adquisición Inmuebles Vendidos"
      Case eCD2014_46AxIAcumuladoVentaInmuebles: enumCostoDeVentas2014ToString = "46.AxI Acumulado por Venta de Inmuebles"
      Case eCD2014_48TotalCVAccionesVendidas: enumCostoDeVentas2014ToString = "48.Total CV Acciones Vendidas"
      Case eCD2014_49OtrosCostosVentas: enumCostoDeVentas2014ToString = "49.Otros Costos de Ventas"
      Case eCD2014_50CostosPrestaciónServicios: enumCostoDeVentas2014ToString = "50.Costos Prestación Servicios"
      Case Else
      enumCostoDeVentas2014ToString = enumCostoDeVentas2014ToString(0)
   End Select
End Function

Public Function strCostoDeVentas2014ToNum(ByVal costoDeVentas2014 As String, _
   Optional ByVal ShowMessage As Boolean = True) As Integer
   Dim mEnum As enum_CostoDeVentas2014
   Dim nCount As Integer
   nCount = 0
   For mEnum = eCD2014_23INVENTARIOINICIAL To eCD2014_50CostosPrestaciónServicios
   If UCase(costoDeVentas2014) = UCase(enumCostoDeVentas2014ToString(mEnum)) Then
      strCostoDeVentas2014ToNum = nCount
      GoTo h_EXIT
   End If
   nCount = nCount + 1
   Next
   strCostoDeVentas2014ToNum = 0
   If ShowMessage Then
      gMessage.ProgrammerMessage "Valor " & costoDeVentas2014 & " no existe en tipo enumerativo costoDeVentas"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithCostoDeVentas2014(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   varComboBox.Clear
   nCount = 0
   If action = Buscar Then
      varComboBox.Text = gDefGen.getSearchAllComboBox()
      varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
      nCount = nCount + 1
   Else
      varComboBox.Text = enumCostoDeVentas2014ToString(enum_CostoDeVentas2014.eCD2014_23INVENTARIOINICIAL)
   End If
   varComboBox.List(nCount) = enumCostoDeVentas2014ToString(enum_CostoDeVentas2014.eCD2014_23INVENTARIOINICIAL)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumCostoDeVentas2014ToString(enum_CostoDeVentas2014.eCD2014_24ComprasNetasNacionales)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumCostoDeVentas2014ToString(enum_CostoDeVentas2014.eCD2014_25ComprasNetasExterior)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumCostoDeVentas2014ToString(enum_CostoDeVentas2014.eCD2014_28InventarioFinal)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumCostoDeVentas2014ToString(enum_CostoDeVentas2014.eCD2014_30InventarioInicialMP)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumCostoDeVentas2014ToString(enum_CostoDeVentas2014.eCD2014_31ComprasNetasMP)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumCostoDeVentas2014ToString(enum_CostoDeVentas2014.eCD2014_32InventarioFinalMP)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumCostoDeVentas2014ToString(enum_CostoDeVentas2014.eCD2014_33ManoObraDirecta)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumCostoDeVentas2014ToString(enum_CostoDeVentas2014.eCD2014_35OtrasRemuneraciones)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumCostoDeVentas2014ToString(enum_CostoDeVentas2014.eCD2014_36Contribuciones)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumCostoDeVentas2014ToString(enum_CostoDeVentas2014.eCD2014_37ComprasBienesServicios)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumCostoDeVentas2014ToString(enum_CostoDeVentas2014.eCD2014_38InventarioInicialPP)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumCostoDeVentas2014ToString(enum_CostoDeVentas2014.eCD2014_39InventarioFinalPP)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumCostoDeVentas2014ToString(enum_CostoDeVentas2014.eCD2014_40InventarioInicialPT)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumCostoDeVentas2014ToString(enum_CostoDeVentas2014.eCD2014_41InventarioFinalPT)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumCostoDeVentas2014ToString(enum_CostoDeVentas2014.eCD2014_43CostoAdqBienesInmueblesVendidos)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumCostoDeVentas2014ToString(enum_CostoDeVentas2014.eCD2014_44MejorasInmueblesVendidos)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumCostoDeVentas2014ToString(enum_CostoDeVentas2014.eCD2014_45GastosRegistroAdqInmueblesVendidos)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumCostoDeVentas2014ToString(enum_CostoDeVentas2014.eCD2014_46AxIAcumuladoVentaInmuebles)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumCostoDeVentas2014ToString(enum_CostoDeVentas2014.eCD2014_48TotalCVAccionesVendidas)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumCostoDeVentas2014ToString(enum_CostoDeVentas2014.eCD2014_49OtrosCostosVentas)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumCostoDeVentas2014ToString(enum_CostoDeVentas2014.eCD2014_50CostosPrestaciónServicios)
   varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
   varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
End Sub

Public Function enumGastos2014ToString(gastos As enum_Gastos2014) As String
   Select Case gastos
      Case eGa2014_52SueldosSalarios: enumGastos2014ToString = "52.Sueldos y Salarios"
      Case eGa2014_53SueldosSalariosDirectivos: enumGastos2014ToString = "53.Sueldos y Salarios a Directivos"
      Case eGa2014_54InteresesSobreCreditos: enumGastos2014ToString = "54.Intereses sobre Créditos"
      Case eGa2014_55TributosPagados: enumGastos2014ToString = "55.Tributos Pagados"
      Case eGa2014_56IndemnizacionesLaborales: enumGastos2014ToString = "56.Indemnizaciones Laborales"
      Case eGa2014_57GastosAdmConsInmueblesArrendamiento: enumGastos2014ToString = "57.Gastos Adm/Conservacion Inmuebles en arrendamiento"
      Case eGa2014_58GastosAdmRegalíasNoMineras: enumGastos2014ToString = "58.Gastos de Administración de Regalías no mineras"
      Case eGa2014_59DepreciacionesAmortizaciones: enumGastos2014ToString = "59.Depreciaciones y Amortizaciones"
      Case eGa2014_60InteresesVarios: enumGastos2014ToString = "60.Intereses Varios"
      Case eGa2014_61PerdidasSufridasBienes: enumGastos2014ToString = "61.Pérdidas Sufridas en Bienes"
      Case eGa2014_62GastosTrasladosNuevosEmpleados: enumGastos2014ToString = "62.Gastos Traslados Nuevos Empleados"
      Case eGa2014_63PerdidaCuentasIncobrables: enumGastos2014ToString = "63.Pérdida Cuentas Incobrables"
      Case eGa2014_64ReservasLey: enumGastos2014ToString = "64.Reservas de Ley"
      Case eGa2014_65CostosConstruccionesLot: enumGastos2014ToString = "65.Costos Construcciones Según Lot"
      Case eGa2014_66ArrendamientoBienesMuebles: enumGastos2014ToString = "66.Arrendamiento Bienes Muebles"
      Case eGa2014_67ArrendamientoBienesInmuebles: enumGastos2014ToString = "67.Arrendamiento Bienes Inmuebles"
      Case eGa2014_68GastosTransporte: enumGastos2014ToString = "68.Gastos de Transporte"
      Case eGa2014_69ComisionesVentaBienesInmuebles: enumGastos2014ToString = "69.Comisiones intermediarios Venta Bienes Inmuebles"
      Case eGa2014_70ComisionesVarias: enumGastos2014ToString = "70.Comisiones Varias"
      Case eGa2014_71DerechosExhibicion: enumGastos2014ToString = "71.Derechos Exhibición Películas Cine o TV"
      Case eGa2014_72RegaliasParticipacionesAnalogas: enumGastos2014ToString = "72.Regalías y participaciones análogas"
      Case eGa2014_73HonorariosProfesionales: enumGastos2014ToString = "73.Honorarios Profesionales"
      Case eGa2014_74AsistenciaTecnicaServTecnologicos: enumGastos2014ToString = "74.Asistencia Técnica/Serv. Tecnológicos"
      Case eGa2014_75GastosReparacionesOrdinarias: enumGastos2014ToString = "75.Gastos Reparaciones Ordinarias"
      Case eGa2014_76PublicidadPropaganda: enumGastos2014ToString = "76.Publicidad y Propaganda"
      Case eGa2014_77PrimasSeguroBienesPersonas: enumGastos2014ToString = "77.Primas de Seguro Bienes y Personas"
      Case eGa2014_78GastosInvestigacionDesarrollo: enumGastos2014ToString = "78.Gastos Investigación y Desarrollo"
      Case eGa2014_79GastosRepresentacion: enumGastos2014ToString = "79.Gastos de Representación"
      Case eGa2014_80GastosAdmRegaliasMineras: enumGastos2014ToString = "80.Gastos Adm. Regalías Mineras"
      Case eGa2014_81GastosAdmHidrocarburos: enumGastos2014ToString = "81.Gastos Adm. Hidrocarburos"
      Case eGa2014_82OtrosGastos: enumGastos2014ToString = "82.Otros Gastos"
      Case eGa2014_85LiberalidadesDonacionesAutorizadas: enumGastos2014ToString = "85.Liberalidades y Donaciones Autorizadas"
      Case Else
      enumGastos2014ToString = enumGastos2014ToString(0)
   End Select
End Function

Public Function strGastos2014ToNum(ByVal gastos2014 As String, _
   Optional ByVal ShowMessage As Boolean = True) As Integer
   Dim mEnum As enum_Gastos2014
   Dim nCount As Integer
   nCount = 0
   For mEnum = eGa2014_52SueldosSalarios To eGa2014_85LiberalidadesDonacionesAutorizadas
   If UCase(gastos2014) = UCase(enumGastos2014ToString(mEnum)) Then
      strGastos2014ToNum = nCount
      GoTo h_EXIT
   End If
   nCount = nCount + 1
   Next
   strGastos2014ToNum = 0
   If ShowMessage Then
      gMessage.ProgrammerMessage "Valor " & gastos2014 & " no existe en tipo enumerativo gastos"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithGastos2014(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   varComboBox.Clear
   nCount = 0
   If action = Buscar Then
      varComboBox.Text = gDefGen.getSearchAllComboBox()
      varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
      nCount = nCount + 1
   Else
      varComboBox.Text = enumGastos2014ToString(enum_Gastos2014.eGa2014_52SueldosSalarios)
   End If
   varComboBox.List(nCount) = enumGastos2014ToString(enum_Gastos2014.eGa2014_52SueldosSalarios)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumGastos2014ToString(enum_Gastos2014.eGa2014_53SueldosSalariosDirectivos)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumGastos2014ToString(enum_Gastos2014.eGa2014_54InteresesSobreCreditos)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumGastos2014ToString(enum_Gastos2014.eGa2014_55TributosPagados)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumGastos2014ToString(enum_Gastos2014.eGa2014_56IndemnizacionesLaborales)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumGastos2014ToString(enum_Gastos2014.eGa2014_57GastosAdmConsInmueblesArrendamiento)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumGastos2014ToString(enum_Gastos2014.eGa2014_58GastosAdmRegalíasNoMineras)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumGastos2014ToString(enum_Gastos2014.eGa2014_59DepreciacionesAmortizaciones)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumGastos2014ToString(enum_Gastos2014.eGa2014_60InteresesVarios)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumGastos2014ToString(enum_Gastos2014.eGa2014_61PerdidasSufridasBienes)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumGastos2014ToString(enum_Gastos2014.eGa2014_62GastosTrasladosNuevosEmpleados)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumGastos2014ToString(enum_Gastos2014.eGa2014_63PerdidaCuentasIncobrables)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumGastos2014ToString(enum_Gastos2014.eGa2014_64ReservasLey)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumGastos2014ToString(enum_Gastos2014.eGa2014_65CostosConstruccionesLot)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumGastos2014ToString(enum_Gastos2014.eGa2014_66ArrendamientoBienesMuebles)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumGastos2014ToString(enum_Gastos2014.eGa2014_67ArrendamientoBienesInmuebles)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumGastos2014ToString(enum_Gastos2014.eGa2014_68GastosTransporte)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumGastos2014ToString(enum_Gastos2014.eGa2014_69ComisionesVentaBienesInmuebles)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumGastos2014ToString(enum_Gastos2014.eGa2014_70ComisionesVarias)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumGastos2014ToString(enum_Gastos2014.eGa2014_71DerechosExhibicion)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumGastos2014ToString(enum_Gastos2014.eGa2014_72RegaliasParticipacionesAnalogas)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumGastos2014ToString(enum_Gastos2014.eGa2014_73HonorariosProfesionales)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumGastos2014ToString(enum_Gastos2014.eGa2014_74AsistenciaTecnicaServTecnologicos)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumGastos2014ToString(enum_Gastos2014.eGa2014_75GastosReparacionesOrdinarias)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumGastos2014ToString(enum_Gastos2014.eGa2014_76PublicidadPropaganda)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumGastos2014ToString(enum_Gastos2014.eGa2014_77PrimasSeguroBienesPersonas)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumGastos2014ToString(enum_Gastos2014.eGa2014_78GastosInvestigacionDesarrollo)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumGastos2014ToString(enum_Gastos2014.eGa2014_79GastosRepresentacion)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumGastos2014ToString(enum_Gastos2014.eGa2014_80GastosAdmRegaliasMineras)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumGastos2014ToString(enum_Gastos2014.eGa2014_81GastosAdmHidrocarburos)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumGastos2014ToString(enum_Gastos2014.eGa2014_82OtrosGastos)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumGastos2014ToString(enum_Gastos2014.eGa2014_85LiberalidadesDonacionesAutorizadas)
   varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
   varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
End Sub

Public Function fEnumGastos2014ToStringInArray(Optional ByVal valPrepararParaSql As Boolean = False) As Variant
   Dim varEnum As enum_NaturalezaDeLaCuenta
   Dim varArray() As String
   Dim varLimiteSuperior As Integer
   Dim nCount As Integer
   Dim varCaracterDelimitador As String
   On Error Resume Next
   varCaracterDelimitador = ""
   If valPrepararParaSql Then
      varCaracterDelimitador = "'"
   End If
   varLimiteSuperior = eGa2014_85LiberalidadesDonacionesAutorizadas - eGa2014_52SueldosSalarios
   nCount = 0
   ReDim varArray(0 To varLimiteSuperior)
   For varEnum = eGa2014_52SueldosSalarios To eGa2014_85LiberalidadesDonacionesAutorizadas
      varArray(nCount) = varCaracterDelimitador & enumGastos2014ToString(varEnum) & varCaracterDelimitador
      nCount = nCount + 1
   Next
   fEnumGastos2014ToStringInArray = varArray
   On Error GoTo 0
End Function

Public Function enumTipoDeMonedaToString(TipoDeMoneda As enum_TipoDeMoneda) As String
   Select Case TipoDeMoneda
      Case eTM_Fisica: enumTipoDeMonedaToString = "Física"
      Case eTM_CriptoActivo: enumTipoDeMonedaToString = "CriptoActivo"
      Case eTM_Otras: enumTipoDeMonedaToString = "Otras"
      Case Else
      enumTipoDeMonedaToString = enumTipoDeMonedaToString(0)
   End Select
End Function

Public Function strTipoDeMonedaToNum(ByVal TipoDeMoneda As String, _
   Optional ByVal ShowMessage As Boolean = True) As Integer
   Dim mEnum As enum_TipoDeMoneda
   Dim nCount As Integer
   nCount = 0
   For mEnum = eTM_Fisica To eTM_Otras
   If UCase(TipoDeMoneda) = UCase(enumTipoDeMonedaToString(mEnum)) Then
      strTipoDeMonedaToNum = nCount
      GoTo h_EXIT
   End If
   nCount = nCount + 1
   Next
   strTipoDeMonedaToNum = 0
   If ShowMessage Then
      gMessage.ProgrammerMessage "Valor " & TipoDeMoneda & " no existe en tipo enumerativo TipoDeMoneda"
   End If
h_EXIT:
   Exit Function
End Function

Public Function enumGenerarComprobanteDeDiferenciaCambiariaToString(GeneraComprobanteDiferenciaCambiaria As enum_GenerarComprobanteDeDiferenciaCambiaria) As String
   Select Case GeneraComprobanteDiferenciaCambiaria
      Case eGC_Mensual: enumGenerarComprobanteDeDiferenciaCambiariaToString = "Mensual"
      Case eGc_Anual: enumGenerarComprobanteDeDiferenciaCambiariaToString = "Anual"
      Case eGC_Nunca: enumGenerarComprobanteDeDiferenciaCambiariaToString = "Nunca"
      Case Else
         enumGenerarComprobanteDeDiferenciaCambiariaToString = enumGenerarComprobanteDeDiferenciaCambiariaToString(0)
   End Select
End Function

Public Function strGenerarComprobanteDeDiferenciaCambiariaToNum(ByVal GenerarComprobanteDeDiferenciaCambiaria As String, _
   Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_GenerarComprobanteDeDiferenciaCambiaria
   Dim nCount As Integer
   nCount = 0
   For mEnum = eGC_Mensual To eGC_Nunca
   If UCase(GenerarComprobanteDeDiferenciaCambiaria) = UCase(enumGenerarComprobanteDeDiferenciaCambiariaToString(mEnum)) Then
      strGenerarComprobanteDeDiferenciaCambiariaToNum = nCount
      GoTo h_EXIT
   End If
   nCount = nCount + 1
   Next
   strGenerarComprobanteDeDiferenciaCambiariaToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & GenerarComprobanteDeDiferenciaCambiaria & " no existe en tipo enumerativo GenerarComprobanteDeDiferenciaCambiaria"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithGenerarComprobanteDeDiferenciaCambiaria(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   nCount = 0
   If action = Buscar Then
      varComboBox.Text = gDefGen.getSearchAllComboBox()
      varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
      nCount = nCount + 1
   Else
      varComboBox.Text = enumGenerarComprobanteDeDiferenciaCambiariaToString(enum_GenerarComprobanteDeDiferenciaCambiaria.eGC_Mensual)
   End If
   varComboBox.List(nCount) = enumGenerarComprobanteDeDiferenciaCambiariaToString(enum_GenerarComprobanteDeDiferenciaCambiaria.eGC_Mensual)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumGenerarComprobanteDeDiferenciaCambiariaToString(enum_GenerarComprobanteDeDiferenciaCambiaria.eGc_Anual)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumGenerarComprobanteDeDiferenciaCambiariaToString(enum_GenerarComprobanteDeDiferenciaCambiaria.eGC_Nunca)
   varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
   varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
End Sub

Public Function enumExpresarBalancesEnMonedaExtrangeraToString(ExpresarBalancesEnMonedaExtrangera As enum_ExpresarBalancesEnMonedaExtrangera) As String
   Select Case ExpresarBalancesEnMonedaExtrangera
      Case eGC_NoAplicar: enumExpresarBalancesEnMonedaExtrangeraToString = "No aplica"
      Case eGc_ConDifCambiria: enumExpresarBalancesEnMonedaExtrangeraToString = "Saldo diario ajustado según la tasa del día"
      Case eGC_ConGananciaPerdidaCambiaria: enumExpresarBalancesEnMonedaExtrangeraToString = "Con Ganancia o Pérdida cambiaria calculada automáticamente"
      Case Else
         enumExpresarBalancesEnMonedaExtrangeraToString = enumExpresarBalancesEnMonedaExtrangeraToString(0)
   End Select
End Function

Public Function strExpresarBalancesEnMonedaExtrangeraToNum(ByVal ExpresarBalancesEnMonedaExtrangera As String, _
   Optional ByVal ShowMessage As Boolean = True) As Integer
   Dim mEnum As enum_ExpresarBalancesEnMonedaExtrangera
   Dim nCount As Integer
   nCount = 0
   For mEnum = eGC_Mensual To eGC_Nunca
   If UCase(ExpresarBalancesEnMonedaExtrangera) = UCase(enumExpresarBalancesEnMonedaExtrangeraToString(mEnum)) Then
      strExpresarBalancesEnMonedaExtrangeraToNum = nCount
      GoTo h_EXIT
   End If
   nCount = nCount + 1
   Next
   strExpresarBalancesEnMonedaExtrangeraToNum = 0
   If ShowMessage Then
      gMessage.ProgrammerMessage "Valor " & ExpresarBalancesEnMonedaExtrangera & " no existe en tipo enumerativo ExpresarBalancesEnMonedaExtrangera"
   End If
h_EXIT:
   Exit Function
End Function
