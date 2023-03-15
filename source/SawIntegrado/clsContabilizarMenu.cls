VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsContabilizarMenu"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = False
Option Explicit

Private Function fTextoMenuDeContabilizar(ByVal valModulo As enum_ComprobanteGeneradoPor) As String
   On Error GoTo h_ERROR
   Select Case valModulo
      Case eCG_CXC: fTextoMenuDeContabilizar = fGenericoDeTextoMenuContabilizar(valModulo, gProyReglasDeContabilizacion.GetTipoContabilizacionCxCAsEnum, gProyReglasDeContabilizacion.GetContabPorLoteCxCAsEnum)
      Case eCG_FACTURA: fTextoMenuDeContabilizar = fGenericoDeTextoMenuContabilizar(valModulo, gProyReglasDeContabilizacion.GetTipoContabilizacionFacturacionAsEnum, gProyReglasDeContabilizacion.GetContabPorLoteFacturacionAsEnum)
      Case eCG_CXP: fTextoMenuDeContabilizar = fGenericoDeTextoMenuContabilizar(valModulo, gProyReglasDeContabilizacion.GetTipoContabilizacionCxPAsEnum, gProyReglasDeContabilizacion.GetContabPorLoteCxPAsEnum)
      Case eCG_COBRANZA: fTextoMenuDeContabilizar = fGenericoDeTextoMenuContabilizar(valModulo, gProyReglasDeContabilizacion.GetTipoContabilizacionCobranzaAsEnum, gProyReglasDeContabilizacion.GetContabPorLoteCobranzaAsEnum)
      Case eCG_PAGOS: fTextoMenuDeContabilizar = fGenericoDeTextoMenuContabilizar(valModulo, gProyReglasDeContabilizacion.GetTipoContabilizacionPagosAsEnum, gProyReglasDeContabilizacion.GetContabPorLotePagosAsEnum)
      Case eCG_MOVIMIENTO_BANCARIO: fTextoMenuDeContabilizar = fGenericoDeTextoMenuContabilizar(valModulo, gProyReglasDeContabilizacion.GetTipoContabilizacionMovBancarioAsEnum, gProyReglasDeContabilizacion.GetContabPorLoteMovBancarioAsEnum)
      Case eCG_RESUMEN_DIARIO_VENTAS: fTextoMenuDeContabilizar = fGenericoDeTextoMenuContabilizar(valModulo, gProyReglasDeContabilizacion.GetTipoContabilizacionRDVtasAsEnum, gProyReglasDeContabilizacion.GetContabPorLoteRDVtasAsEnum)
      Case eCG_ANTICIPO: fTextoMenuDeContabilizar = fGenericoDeTextoMenuContabilizar(valModulo, gProyReglasDeContabilizacion.GetTipoContabilizacionAnticipoAsEnum, gProyReglasDeContabilizacion.GetContabPorLoteAnticipoAsEnum)
      Case eCG_REPOSICION: fTextoMenuDeContabilizar = fGenericoDeTextoMenuContabilizar(valModulo, gProyReglasDeContabilizacion.GetTipoContabilizacionCajaChicaAsEnum, eCP_DIARIA)
      Case eCG_TRANSFERENCIA_ENTRE_CUENTAS_BANCARIA: fTextoMenuDeContabilizar = fGenericoDeTextoMenuContabilizar(valModulo, eTD_CADA_DOCUMENTO, eCP_DIARIA)
      Case eCG_ORDEN_DE_PRODUCCION: fTextoMenuDeContabilizar = fGenericoDeTextoMenuContabilizar(valModulo, eTD_CADA_DOCUMENTO, eCP_DIARIA)
      Case Else: gMessage.Advertencia "Falta fTextoMenuDeContabilizar clsReglasDeContabilizacionNavigator - Case Else"
   End Select
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, "clsContabilizarMenu", "fTextoMenuDeContabilizar", "clsContabilizarMenu", eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fGenericoDeTextoMenuContabilizar(ByVal valModulo As enum_ComprobanteGeneradoPor, ByVal valTipoDeContabilizacion As enum_TipoDeContabilizacion, ByVal valContabPorLote As enum_ContabilizacionPorLote) As String
   Dim TextoMenu As String
   On Error GoTo h_ERROR
   TextoMenu = gEnumProyectoWincont.enumComprobanteGeneradoPorToString(valModulo)
   If valTipoDeContabilizacion = eTD_POR_LOTE Then
      If valContabPorLote = eCP_DIARIA Then
         TextoMenu = TextoMenu & " por Lote Diario"
      Else
         TextoMenu = TextoMenu & " por Lote Mensual"
      End If
   End If
h_EXIT:
   fGenericoDeTextoMenuContabilizar = TextoMenu
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsContabilizarMenu", "fGenericoDeTextoMenuContabilizar", "clsContabilizarMenu", eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Sub sCreaElMenuDeContabilizar(ByRef refForma As Form)
   Dim numSubMenu As Long
   Dim ID As Long
   Dim puedoActivar As Boolean
   On Error GoTo h_ERROR
   puedoActivar = Not gProyCompaniaActual.GetEsCatalogoGeneral
   If gProyUsuarioActual.GetUsaContabilidad Then
      numSubMenu = gUtilMenus.fAddSubMenu(refForma, gEnumProyecto.enum_SystemModulesToStr(enum_SystemModules.eSM_Contabilizar), ID)
      Call gUtilMenus.fAddMenuItem(refForma, fTextoMenuDeContabilizar(eCG_FACTURA), numSubMenu, puedoActivar, False, eSC_NoShortcut, True, 1)
      Call gUtilMenus.fAddMenuItem(refForma, fTextoMenuDeContabilizar(eCG_CXC), numSubMenu, puedoActivar, False, eSC_NoShortcut, True, 1)
      Call gUtilMenus.fAddMenuItem(refForma, fTextoMenuDeContabilizar(eCG_COBRANZA), numSubMenu, puedoActivar, False, eSC_NoShortcut, True, 2)
      Call gUtilMenus.fAddMenuItem(refForma, fTextoMenuDeContabilizar(eCG_CXP), numSubMenu, puedoActivar, False, eSC_NoShortcut, True, 2)
      Call gUtilMenus.fAddMenuItem(refForma, fTextoMenuDeContabilizar(eCG_PAGOS), numSubMenu, puedoActivar, False, eSC_NoShortcut, True, 1)
      Call gUtilMenus.fAddMenuItem(refForma, fTextoMenuDeContabilizar(eCG_MOVIMIENTO_BANCARIO), numSubMenu, puedoActivar, False, eSC_NoShortcut, True, 1)
      Call gUtilMenus.fAddMenuItem(refForma, fTextoMenuDeContabilizar(eCG_RESUMEN_DIARIO_VENTAS), numSubMenu, puedoActivar, False, eSC_NoShortcut, True, 1)
      Call gUtilMenus.fAddMenuItem(refForma, fTextoMenuDeContabilizar(eCG_ANTICIPO) & "-" & gEnumProyecto.enumTipoDeAnticipoToString(eTDA_COBRADO), numSubMenu, puedoActivar, False, eSC_NoShortcut, True, 8)
      Call gUtilMenus.fAddMenuItem(refForma, fTextoMenuDeContabilizar(eCG_ANTICIPO) & "-" & gEnumProyecto.enumTipoDeAnticipoToString(eTDA_PAGADO), numSubMenu, puedoActivar, False, eSC_NoShortcut, True, 8)
      Call gUtilMenus.fAddMenuItem(refForma, fTextoMenuDeContabilizar(eCG_REPOSICION), numSubMenu, puedoActivar, False, eSC_NoShortcut, True, 4)
      Call gUtilMenus.fAddMenuItem(refForma, fTextoMenuDeContabilizar(eCG_TRANSFERENCIA_ENTRE_CUENTAS_BANCARIA), numSubMenu, puedoActivar, False, eSC_NoShortcut, True, 4)
      Call gUtilMenus.fAddMenuItem(refForma, fTextoMenuDeContabilizar(eCG_ORDEN_DE_PRODUCCION), numSubMenu, puedoActivar, False, eSC_NoShortcut, gProyParametros.fEsModuloDeProduccion, 2)
      If gDefgen.GetElProgramaEstaEnModoAvanzado Then
         Call gUtilMenus.fAddSeparador(refForma, numSubMenu)
         Call gUtilMenus.fAddMenuItem(refForma, "Documento Por Lote", numSubMenu, gProyUsuarioActual.GetInsertarComprobante And puedoActivar And gDefgen.GetElProgramaEstaEnModoAvanzado, False)
      End If
      Call gUtilMenus.fAddSeparador(refForma, numSubMenu)
      Call gUtilMenus.fAddMenuItem(refForma, "Pago de Sueldos", numSubMenu, puedoActivar, False, eSC_NoShortcut, True, 13)
      
      If (gProyParametrosCompania.GetMetodoDeCosteoAsEnum = enum_TipoDeMetodoDeCosteo.eTDMDC_CostoPromedio) Then
         Call gUtilMenus.fAddMenuItem(refForma, "Costos Inventario", numSubMenu, puedoActivar, False, eSC_NoShortcut, True, 16)
         Call gUtilMenus.fAddMenuItem(refForma, "Borrar Comprobante de Costos Inventario", numSubMenu, puedoActivar, False, eSC_NoShortcut, True, 16)
      End If
      Call gUtilMenus.fAddSeparador(refForma, numSubMenu)
      Call gUtilMenus.fAddMenuItem(refForma, "Reparar Inconsistencias", numSubMenu, gDefgen.GetElProgramaEstaEnModoAvanzado And puedoActivar, False, eSC_NoShortcut, gDefgen.GetElProgramaEstaEnModoAvanzado)
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsContabilizarMenu", "sCreaElMenuDeContabilizar", "clsContabilizarMenu", eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Sub sEjecutaAccionEscogidaMenuContabilizar(ByVal valCaptionMenuEscogido As String)
   On Error GoTo h_ERROR
   Select Case valCaptionMenuEscogido
      Case fTextoMenuDeContabilizar(eCG_CXC): sEjecutaContabilizar eCG_CXC, -1
      Case fTextoMenuDeContabilizar(eCG_FACTURA): sEjecutaContabilizar eCG_FACTURA, -1
      Case fTextoMenuDeContabilizar(eCG_COBRANZA): sEjecutaContabilizar eCG_COBRANZA, -1
      Case fTextoMenuDeContabilizar(eCG_CXP): sEjecutaContabilizar eCG_CXP, -1
      Case fTextoMenuDeContabilizar(eCG_PAGOS): sEjecutaContabilizar eCG_PAGOS, -1
      Case fTextoMenuDeContabilizar(eCG_MOVIMIENTO_BANCARIO): sEjecutaContabilizar eCG_MOVIMIENTO_BANCARIO, -1
      Case fTextoMenuDeContabilizar(eCG_RESUMEN_DIARIO_VENTAS): sEjecutaContabilizar eCG_RESUMEN_DIARIO_VENTAS, -1
      Case fTextoMenuDeContabilizar(eCG_ANTICIPO) & "-" & gEnumProyecto.enumTipoDeAnticipoToString(eTDA_COBRADO): sEjecutaContabilizar eCG_ANTICIPO, enum_TipoDeAnticipo.eTDA_COBRADO
      Case fTextoMenuDeContabilizar(eCG_ANTICIPO) & "-" & gEnumProyecto.enumTipoDeAnticipoToString(eTDA_PAGADO): sEjecutaContabilizar eCG_ANTICIPO, enum_TipoDeAnticipo.eTDA_PAGADO
      Case fTextoMenuDeContabilizar(eCG_TRANSFERENCIA_ENTRE_CUENTAS_BANCARIA): sEjecutaProcesoDeContabilizacionDeTransferenciaEntreCuentas
      Case fTextoMenuDeContabilizar(eCG_ORDEN_DE_PRODUCCION): sEjecutacontabilizarOrdenDeProduccion
      Case "Documento Por Lote": sEjecutaProcesoDeContabilizacionPorLote
      Case "Pago de Sueldos": sEjecutaContabilizar eCG_PAGO_SUELDOS, -1
      Case "Costos Inventario": sEjecutaContabilizar eCG_INVENTARIO, -1
      Case "Borrar comprobante de  Costos Inventario":   sEjecutaBorrarComprobantesDeCostosDeInventario eCG_INVENTARIO, -1
      Case "Reposición de Caja Chica": sEjecutaProcesoDeContabilizacionDeReposicion
      Case "Reparar Inconsistencias": sEjecutaRepararInconsistenciasAuxiliar
   End Select
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsContabilizarMenu", "sEjecutaAccionEscogidaMenuContabilizar", "clsContabilizarMenu", eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sEjecutaContabilizar(ByVal valModulo As enum_ComprobanteGeneradoPor, ByVal valTipoDocumento As Integer)
   On Error GoTo h_ERROR
   Select Case valModulo
      Case eCG_CXC
         If gProyReglasDeContabilizacion.GetTipoContabilizacionCxCAsEnum = eTD_POR_LOTE Then
            sEjecutaContabilizarPorLote eCG_CXC, valTipoDocumento
         Else
            sEjecutaContabilizarCxCDiaria
         End If
      Case eCG_FACTURA
         If gProyReglasDeContabilizacion.GetTipoContabilizacionFacturacionAsEnum = eTD_POR_LOTE Then
            sEjecutaContabilizarPorLote eCG_FACTURA, valTipoDocumento
         Else
            sEjecutaContabilizarFacturaDiaria
         End If
      Case eCG_COBRANZA
         If gProyReglasDeContabilizacion.GetTipoContabilizacionCobranzaAsEnum = eTD_POR_LOTE Then
            sEjecutaContabilizarPorLote eCG_COBRANZA, valTipoDocumento
         Else
            sEjecutaContabilizarCobranzaDiaria
         End If
      Case eCG_CXP
         If gProyReglasDeContabilizacion.GetTipoContabilizacionCxPAsEnum = eTD_POR_LOTE Then
            sEjecutaContabilizarPorLote eCG_CXP, valTipoDocumento
         Else
            sEjecutaContabilizarCxPdiaria
         End If
      Case eCG_PAGOS
         If gProyReglasDeContabilizacion.GetTipoContabilizacionPagosAsEnum = eTD_POR_LOTE Then
            sEjecutaContabilizarPorLote eCG_PAGOS, valTipoDocumento
         Else
            sEjecutaContabilizarPagoDiaria
         End If
      Case eCG_MOVIMIENTO_BANCARIO
         If gProyReglasDeContabilizacion.GetTipoContabilizacionMovBancarioAsEnum = eTD_POR_LOTE Then
            sEjecutaContabilizarPorLote eCG_MOVIMIENTO_BANCARIO, valTipoDocumento
         Else
            sEjecutaContabilizarMovimientoBancarioDiaria
         End If
      Case eCG_RESUMEN_DIARIO_VENTAS
         If gProyReglasDeContabilizacion.GetTipoContabilizacionRDVtasAsEnum = eTD_POR_LOTE Then
            sEjecutaContabilizarPorLote eCG_RESUMEN_DIARIO_VENTAS, valTipoDocumento
         Else
            sEjecutaContabilizarResumenVentasDiaria
         End If
      Case eCG_ANTICIPO
         If gProyReglasDeContabilizacion.GetTipoContabilizacionAnticipoAsEnum = eTD_POR_LOTE Then
            sEjecutaContabilizarPorLote eCG_ANTICIPO, valTipoDocumento
         Else
            sEjecutaContabilizarAnticipo valTipoDocumento
         End If
      Case eCG_INVENTARIO: sEjecutaInventario eCG_INVENTARIO, valTipoDocumento
       Case eCG_PAGO_SUELDOS: sEjecutaContabilizarPagoSueldos
   End Select
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsContabilizarMenu", "sEjecutaContabilizar", "clsContabilizarMenu", eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Function fSePuedeEjecutarContabilizacion(ByVal valNombreRecord As String) As Boolean
   Dim vResult As Boolean
   On Error GoTo h_ERROR
   vResult = False
   If gProyCompaniaActual.GetUsaModuloDeContabilidad Then
      If gProyCompaniaActual.fLaCompaniaYElPeriodoFueronEscogidos Then
         If Not gContPeriodoActual.ElPeriodoEstaCerrado(True) Then
            vResult = Not gAPI.fVentanaAsociadaARecordsEstaAbiertaPonDeprimera(Forms, valNombreRecord, Contabilizar)
         End If
      End If
   End If
   fSePuedeEjecutarContabilizacion = vResult
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsContabilizarMenu", "fSePuedeEjecutarContabilizacion", "clsContabilizarMenu", eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Sub sEjecutaContabilizarPorLote(ByVal valModulo As enum_ComprobanteGeneradoPor, ByVal valTipoDocumento As Integer)
   Dim insFrmGeneracionDeComprobantes As frmGeneracionDeComprobantes
   On Error GoTo h_ERROR
   If fSePuedeEjecutarContabilizacion(gEnumProyectoWincont.enumComprobanteGeneradoPorToString(valModulo)) Then
      Set insFrmGeneracionDeComprobantes = New frmGeneracionDeComprobantes
      insFrmGeneracionDeComprobantes.sInitLookAndFeelAndSetValues insertar, valModulo, valTipoDocumento
      insFrmGeneracionDeComprobantes.Show vbModal
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsContabilizarMenu", "sEjecutaContabilizarPorLote", "clsContabilizarMenu", eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sEjecutaContabilizarCxCDiaria()
   Dim insFrmContabilizar As frmListContabilizacion
   Dim insCxC As clsCxCNavigator
   Dim fixedAdditionalWhereClauses As String
   On Error GoTo h_ERROR
   If fSePuedeEjecutarContabilizacion(gEnumProyecto.enum_SystemModulesToStr(eSM_CxC)) Then
      Set insCxC = New clsCxCNavigator
      Set insFrmContabilizar = New frmListContabilizacion
      fixedAdditionalWhereClauses = insCxC.GetTableName & ".Origen = " & gUtilSQL.fSQLSimpleValueForEnum(enum_OrigenFacturacionOManual.eOF_MANUAL)
      insFrmContabilizar.sInitLookAndFeel Contabilizar, gEnumProyecto.enum_SystemModulesToStr(eSM_CxC), insCxC.GetTableName, insCxC, insCxC.getFN_NO_DOC_ORIGEN_CONTABILIZACION_PARA_INPUT(False), fixedAdditionalWhereClauses
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsContabilizarMenu", "sEjecutaContabilizarCxCdiaria", "clsContabilizarMenu", eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sEjecutaContabilizarCobranzaDiaria()
   Dim insFrmContabilizar As frmListContabilizacion
   Dim insCobranza As clsCobranzaNavigator
   On Error GoTo h_ERROR
   If fSePuedeEjecutarContabilizacion(gEnumProyecto.enum_SystemModulesToStr(eSM_Cobranza)) Then
      Set insCobranza = New clsCobranzaNavigator
      Set insFrmContabilizar = New frmListContabilizacion
      insFrmContabilizar.sInitLookAndFeel Contabilizar, gEnumProyecto.enum_SystemModulesToStr(eSM_Cobranza), insCobranza.GetTableName, insCobranza, "Numero"
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsContabilizarMenu", "sEjecutaContabilizarCobranzaDiaria", "clsContabilizarMenu", eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sEjecutaContabilizarCxPdiaria()
   Dim insFrmContabilizar As frmListContabilizacion
   Dim insCxP As clsCxPNavigator
   On Error GoTo h_ERROR
   If fSePuedeEjecutarContabilizacion(gEnumProyecto.enum_SystemModulesToStr(eSM_CxP)) Then
      Set insCxP = New clsCxPNavigator
      Set insFrmContabilizar = New frmListContabilizacion
      insFrmContabilizar.sInitLookAndFeel Contabilizar, gEnumProyecto.enum_SystemModulesToStr(eSM_CxP), insCxP.GetTableName, insCxP, insCxP.getFN_NO_DOC_ORIGEN_CONTABILIZACION_PARA_INPUT(False), "EstaAsociadoARendicion = 'N'"
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsContabilizarMenu", "sEjecutaContabilizarCxPdiaria", "clsContabilizarMenu", eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sEjecutaContabilizarPagoDiaria()
   Dim insFrmContabilizar As frmListContabilizacion
   Dim insPago As clsPagoNavigator
   On Error GoTo h_ERROR
   If fSePuedeEjecutarContabilizacion(gEnumProyecto.enum_SystemModulesToStr(eSM_Pago)) Then
      Set insPago = New clsPagoNavigator
      Set insFrmContabilizar = New frmListContabilizacion
      insFrmContabilizar.sInitLookAndFeel Contabilizar, gEnumProyecto.enum_SystemModulesToStr(eSM_Pago), insPago.GetTableName, insPago, "NumeroComprobante", " Origen <> " & gConvert.enumerativoAChar(enum_OrigenCXPoCXCoManual.eOCXPCXCM_CxP)
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsContabilizarMenu", "sEjecutaContabilizarPagodiaria", "clsContabilizarMenu", eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sEjecutaContabilizarMovimientoBancarioDiaria()
   Dim insFrmContabilizar As frmListContabilizacion
   Dim insMovBancario As clsMovimientoBancarioNavigator
   Dim fixedAdditionalWhereClauses As String
   On Error GoTo h_ERROR
   If fSePuedeEjecutarContabilizacion(gEnumProyecto.enum_SystemModulesToStr(eSM_MovimientoBancario)) Then
      Set insMovBancario = New clsMovimientoBancarioNavigator
      Set insFrmContabilizar = New frmListContabilizacion
      fixedAdditionalWhereClauses = insMovBancario.GetTableName & ".GeneradoPor = " & gUtilSQL.fSQLSimpleValueForEnum(enum_GeneradoPor.eGP_USUARIO)
      insFrmContabilizar.sInitLookAndFeel Contabilizar, gEnumProyecto.enum_SystemModulesToStr(eSM_MovimientoBancario), insMovBancario.GetTableName, insMovBancario, "ConsecutivoMovimiento", fixedAdditionalWhereClauses
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsContabilizarMenu", "sEjecutaContabilizarMovimientoBancariodiaria", "clsContabilizarMenu", eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sEjecutaContabilizarFacturaDiaria()
   Dim insFrmContabilizar As frmListContabilizacion
   Dim insFactura As clsFacturaNavigator
   Dim fixedAdditionalWhereClauses As String
   On Error GoTo h_ERROR
   If fSePuedeEjecutarContabilizacion(gEnumProyecto.enum_SystemModulesToStr(eSM_Factura)) Then
      Set insFrmContabilizar = New frmListContabilizacion
      Set insFactura = New clsFacturaNavigator
      insFactura.setClaseDeTrabajo eCTFC_Factura
      fixedAdditionalWhereClauses = insFactura.GetTableName & "." & insFactura.getFN_STATUS_FACTURA & " <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_BORRADOR) & " AND " & insFactura.GetTableName & "." & insFactura.getFN_TIPO_DE_DOCUMENTO & " <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_RESUMENDIARIODEVENTAS)
      insFrmContabilizar.sInitLookAndFeel Contabilizar, gEnumProyecto.enum_SystemModulesToStr(eSM_Factura), insFactura.GetTableName, insFactura, insFactura.getFN_NO_DOC_ORIGEN_CONTABILIZACION_PARA_INPUT(False), fixedAdditionalWhereClauses
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsContabilizarMenu", "sEjecutaContabilizarFacturadiaria", "clsContabilizarMenu", eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sEjecutaContabilizarResumenVentasDiaria()
   Dim insFrmContabilizar As frmListContabilizacion
   Dim insResumenVts As clsFacturaNavigator
   Dim fixedAdditionalWhereClauses As String
   On Error GoTo h_ERROR
   If fSePuedeEjecutarContabilizacion(gEnumProyecto.enum_SystemModulesToStr(eSM_Factura)) Then
      Set insFrmContabilizar = New frmListContabilizacion
      Set insResumenVts = New clsFacturaNavigator
      insResumenVts.setClaseDeTrabajo eCTFC_Factura
      insResumenVts.SetTipoDeDocumentoStr gEnumProyecto.enumTipodocumentoFacturaToString(eTF_RESUMENDIARIODEVENTAS)
      fixedAdditionalWhereClauses = insResumenVts.GetTableName & "." & insResumenVts.getFN_STATUS_FACTURA & " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_BORRADOR) & " AND " & insResumenVts.GetTableName & "." & insResumenVts.getFN_TIPO_DE_DOCUMENTO & " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_RESUMENDIARIODEVENTAS) & " AND " & insResumenVts.GetTableName & ".RealizoCierreZ = '" & gConvert.ConvertBooleanToString(False) & "'"
      insFrmContabilizar.sInitLookAndFeel Contabilizar, gEnumProyecto.enum_SystemModulesToStr(eSM_Factura), insResumenVts.GetTableName, insResumenVts, insResumenVts.getFN_NO_DOC_ORIGEN_CONTABILIZACION_PARA_INPUT(False), fixedAdditionalWhereClauses
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsContabilizarMenu", "sEjecutaContabilizarResumenVentasDiaria", "clsContabilizarMenu", eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sEjecutaContabilizarAnticipo(ByVal valTipoDocumento As enum_TipoDeAnticipo)
   Dim insFrmContabilizar As frmListContabilizacion
   Dim insAnticipo As clsAnticipoNavigator
   Dim fixedAdditionalWhereClauses As String
   On Error GoTo h_ERROR
   If fSePuedeEjecutarContabilizacion(gEnumProyecto.enum_SystemModulesToStr(eSM_Anticipo)) Then
      Set insFrmContabilizar = New frmListContabilizacion
      Set insAnticipo = New clsAnticipoNavigator
      insAnticipo.SetTipoStr gEnumProyecto.enumTipoDeAnticipoToString(valTipoDocumento)
      fixedAdditionalWhereClauses = "anticipo.Tipo = " & gUtilSQL.fSQLSimpleValueForEnum(valTipoDocumento) & " AND anticipo.GeneradoPor <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_GeneradoPor.eGP_RETENCION_EN_COBRANZA)
      insFrmContabilizar.sInitLookAndFeel Contabilizar, gEnumProyecto.enum_SystemModulesToStr(eSM_Anticipo), "anticipo", insAnticipo, "ConsecutivoAnticipo", fixedAdditionalWhereClauses
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsContabilizarMenu", "sEjecutaContabilizarAnticipo", "clsContabilizarMenu", eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sEjecutaProcesoDeContabilizacionPorLote()
   Dim insFrmContabilizacionPorLote As frmContabilizacionPorLote
   On Error GoTo h_ERROR
   If fSePuedeEjecutarContabilizacion(gEnumProyectoWincont.enum_ContabilidadSystemModulesToStr(eSM_Comprobante)) Then
      Set insFrmContabilizacionPorLote = New frmContabilizacionPorLote
      insFrmContabilizacionPorLote.Show
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsContabilizarMenu", "sEjecutaProcesoDeContabilizacionPorLote", "clsContabilizarMenu", eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sEjecutaInventario(ByVal valModulo As enum_ComprobanteGeneradoPor, ByVal valTipoDocumento As Integer)
   Dim insGenerarComprobanteInv As frmGeneracionDeComprobantes
   Dim insReglasDeContabilizacion As clsReglasDeContabilizacionNavigator
   On Error GoTo h_ERROR
   Set insReglasDeContabilizacion = New clsReglasDeContabilizacionNavigator
   If fSePuedeEjecutarContabilizacion(gEnumProyectoWincont.enumComprobanteGeneradoPorToString(valModulo)) Then
      If insReglasDeContabilizacion.fPuedoContabilizarInv Then
         Set insGenerarComprobanteInv = New frmGeneracionDeComprobantes
         insGenerarComprobanteInv.sInitLookAndFeelAndSetValues Generar, valModulo, valTipoDocumento
         insGenerarComprobanteInv.Show vbModal
      End If
   End If
   Set insReglasDeContabilizacion = Nothing
   Set insGenerarComprobanteInv = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsContabilizarMenu", "sEjecutaContabilizarPorLote", "clsContabilizarMenu", eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sEjecutaBorrarComprobantesDeCostosDeInventario(ByVal valModulo As enum_ComprobanteGeneradoPor, ByVal valTipoDocumento As Integer)
   Dim insGenerarComprobanteInv As frmGeneracionDeComprobantes
   On Error GoTo h_ERROR
   Set insGenerarComprobanteInv = New frmGeneracionDeComprobantes
   If fSePuedeEjecutarContabilizacion(gEnumProyectoWincont.enumComprobanteGeneradoPorToString(valModulo)) Then
      If (Not gContPeriodoActual.ElPeriodoEstaCerrado(True)) Then
         insGenerarComprobanteInv.sInitLookAndFeelAndSetValues Eliminar, valModulo, valTipoDocumento
         insGenerarComprobanteInv.Show vbModal
      End If
   End If
   Set insGenerarComprobanteInv = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsContabilizarMenu", "sEjecutaBorrarComprobantesDeCostosDeInventario", "clsContabilizarMenu", eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sEjecutaContabilizarPagoSueldos()
   Dim insConexion As clsConexionesSawAOS
   Dim insSolicitud  As clsSolicitudesDePagoNavigator
   Dim xmlResultado As String
   Dim frmSolicitudesDePagoInput As frmSolicitudesDePagoInput
   On Error GoTo h_ERROR
   If fSePuedeEjecutarContabilizacion("Pago de Sueldos") Then
      Set insSolicitud = New clsSolicitudesDePagoNavigator
      Set insConexion = New clsConexionesSawAOS
      Set frmSolicitudesDePagoInput = New frmSolicitudesDePagoInput
      If insConexion.fSelectAndSetValuesOfSolicitudesDePagoFromAOS("", xmlResultado) Then
         gLibGalacDataParse.Initialize xmlResultado
         If ((gLibGalacDataParse.GetString(0, "StatusStr", "")) = gEnumProyecto.enumStatusSolicitudToString(eSS_PorProcesar)) Then
             insSolicitud.SetConsecutivoSolicitud gConvert.fConvertStringToLong(gLibGalacDataParse.GetString(0, "ConsecutivoSolicitud", ""))
             insSolicitud.SetNumeroDocumentoOrigen gLibGalacDataParse.GetString(0, "NumeroDocumentoOrigen", "")
             insSolicitud.SetStatusStr gLibGalacDataParse.GetString(0, "StatusStr", "")
             insSolicitud.SetGeneradoPorStr gLibGalacDataParse.GetString(0, "GeneradoPorStr", "")
             frmSolicitudesDePagoInput.sInitLookAndFeelAndSetValues Contabilizar, insSolicitud, eTDFDPS_Cheque
             frmSolicitudesDePagoInput.Show
         Else
            gMessage.AlertMessage "La solicitud con status Por Profesar no puede ser Contabilizada.", "Contabilizar Pago de Sueldos"
         End If
      End If
   End If
   Set insSolicitud = Nothing
   Set frmSolicitudesDePagoInput = Nothing
   Set insConexion = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsContabilizarMenu", "sEjecutaContabilizarPagoSueldos", "clsContabilizarMenu", eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Function fHayValoresParaProcesar(ByVal valConsecutivoSolicitud As Long) As Boolean
   Dim vResult As Boolean
   Dim vSQLWhere As String
   Dim vSql As String
   On Error GoTo h_ERROR
   vResult = False
   vSQLWhere = gUtilSQL.fSQLValueWithAnd("", "Saw.Gv_RenglonSolicitudesDePago_B1.Contabilizado", "N", False)
   vSQLWhere = gUtilSQL.fSQLEnumValueWithAnd(vSQLWhere, "Saw.Gv_RenglonSolicitudesDePago_B1.Status", eSSR_Procesada)
   vSQLWhere = gUtilSQL.fSQLNumberValueWithAnd(vSQLWhere, "Saw.Gv_RenglonSolicitudesDePago_B1.ConsecutivoSolicitud", valConsecutivoSolicitud)
   vSQLWhere = gUtilSQL.fSQLNumberValueWithAnd(vSQLWhere, "Saw.Gv_RenglonSolicitudesDePago_B1.ConsecutivoCompania", gProyCompaniaActual.GetConsecutivoCompania)
   vSQLWhere = gUtilSQL.getWhereSQL(vSQLWhere)
   vSql = " SELECT Saw.Gv_RenglonSolicitudesDePago_B1.consecutivoRenglon FROM Saw.Gv_RenglonSolicitudesDePago_B1 " & vSQLWhere
   vResult = (gDbUtil.fRecordCountOfSql(vSql) > 0)
   If Not vResult Then
      gMessage.AlertMessage "Esta Solicitud no posee registros que procesar.", "Solicitudes de Pago"
   End If
   fHayValoresParaProcesar = vResult
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsContabilizarMenu", "fHayValoresParaProcesar", "clsContabilizarMenu", eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Sub sEjecutaProcesoDeContabilizacionDeReposicion()
   Dim parametros As String
   Dim objWrapper As Object
   Dim datos As String
   Dim fecha As Date
   Dim Numero As String
   Dim Consecutivo As String
   Dim generarComprobanteNavigator As clsGenerarComprobanteNavigator
   Dim fechaAnulacion As Date
   Dim vCodigoCuentaBancaria As String
   Dim insMovimientoBancario As clsMovimientoBancarioNavigator
   Dim insCtaBancaria As clsCuentaBancariaNavigator
   On Error GoTo h_ERROR
   If fSePuedeEjecutarContabilizacion("Caja Chica") Then
      Set insMovimientoBancario = New clsMovimientoBancarioNavigator
      Set objWrapper = gUtilWrp.fCreateWrpForModule(gUtilWrp.fWrpClassName("CajaChica", "WrpReposicion"), gProyUsuarioActual.GetNombreDelUsuario, gProyUsuarioActual.GetPassword)
      Set generarComprobanteNavigator = New clsGenerarComprobanteNavigator
      parametros = "<GpData><DatosDocumento><Consecutivo>-1</Consecutivo></DatosDocumento></GpData>"
      datos = objWrapper.ExecuteAndReturnValue(gDefgen.AccionSobreRecordStr(AccionSobreRecord.Contabilizar), gConvert.fConvierteAString(gProyCompaniaActual.GetConsecutivoCompania), parametros)
      If (datos <> "") Then
         gLibGalacDataParse.Initialize datos
         gLibGalacDataParse.SetRootName "Datos"
         gLibGalacDataParse.SetResultName "Documento"
         fecha = gConvert.fConvertStringToDate(gLibGalacDataParse.GetString(0, "FechaCierre", ""))
         Numero = gLibGalacDataParse.GetString(0, "Numero", "")
         Consecutivo = gConvert.fConvertStringToLong(gLibGalacDataParse.GetString(0, "Consecutivo", ""))
         vCodigoCuentaBancaria = gLibGalacDataParse.GetString(0, "CodigoCuentaCajaChica")
         If (generarComprobanteNavigator.fValidaDocumento(True, Contabilizar, eCG_REPOSICION, fecha, Numero, False, False, eTDDR_Reposicion, Consecutivo)) Then
            Set insCtaBancaria = New clsCuentaBancariaNavigator
            insMovimientoBancario.sCargarValoresDeLaCuentaBancaria insCtaBancaria, vCodigoCuentaBancaria, True
            generarComprobanteNavigator.sContabilizaDocumento True, Contabilizar, eCG_REPOSICION, fecha, Numero, False, False, eTDDR_Reposicion, Consecutivo, insCtaBancaria.GetTipoDeAlicuotaPorContribuyenteAsEnum
         End If
      End If
   End If
   parametros = "<GpData><DatosDocumento><Consecutivo>-1</Consecutivo></DatosDocumento></GpData>"
   Set insMovimientoBancario = Nothing
   Set generarComprobanteNavigator = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsContabilizarMenu", "sEjecutaProcesoDeContabilizacionDeReposicion", "clsContabilizarMenu", eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sEjecutaRepararInconsistenciasAuxiliar()
   Dim insfrmRepararAuxiliaresEnBlanco As frmRepararAuxiliaresEnBlanco
   On Error GoTo h_ERROR
   Set insfrmRepararAuxiliaresEnBlanco = New frmRepararAuxiliaresEnBlanco
   insfrmRepararAuxiliaresEnBlanco.Show vbModal
   Set insfrmRepararAuxiliaresEnBlanco = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsContabilizarMenu", "sEjecutaRepararInconsistenciasAuxiliar", "clsContabilizarMenu", eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sEjecutaProcesoDeContabilizacionDeTransferenciaEntreCuentas()
   Dim vParametros As String
   Dim objWrapper As Object
   Dim vDatos As String
   Dim vFecha As Date
   Dim vNumero As String
   Dim vStatus As String
   Dim vConsecutivo As Long
   Dim insGenerarComprobanteNavigator As clsGenerarComprobanteNavigator
   Dim vFechaAnulacion As Date
   Dim vCodigoCuentaBancaria As String
   Dim insMovimientoBancario As clsMovimientoBancarioNavigator
   Dim insCtaBancaria As clsCuentaBancariaNavigator
   Dim vNroDocContabiliacion As String
   Dim vEsReverso As Boolean
   Dim vStatusInt As Integer
   On Error GoTo h_ERROR
   If fSePuedeEjecutarContabilizacion("Transferencia entre Cuentas Bancarias") Then
      Set insMovimientoBancario = New clsMovimientoBancarioNavigator
      Set insGenerarComprobanteNavigator = New clsGenerarComprobanteNavigator
      Set objWrapper = gUtilWrp.fCreateWrpForModule(gUtilWrp.fWrpClassName("TransferenciaEntreCuentasBancarias", "wrpTransferenciaEntreCuentasBancarias"), gProyUsuarioActual.GetNombreDelUsuario, gProyUsuarioActual.GetPassword)
      vParametros = "<GpData><DatosDocumento><Consecutivo>-1</Consecutivo></DatosDocumento></GpData>"
      vDatos = objWrapper.ExecuteAndReturnValue(gDefgen.AccionSobreRecordStr(AccionSobreRecord.Contabilizar), gConvert.fConvierteAString(gProyCompaniaActual.GetConsecutivoCompania), vParametros)
      vEsReverso = False
      If (vDatos = "") Then
         gMessage.InformationMessage "Acción cancelada por el usuario. ", "Contabilización de Transferencia entre Cuentas Bancarias"
      Else
         gLibGalacDataParse.Initialize vDatos
         gLibGalacDataParse.SetRootName "Datos"
         gLibGalacDataParse.SetResultName "Documento"
         vFecha = gConvert.fConvertStringToDate(gLibGalacDataParse.GetString(0, "Fecha", ""))
         vNumero = gLibGalacDataParse.GetString(0, "NumeroDocumento", "")
         vConsecutivo = gConvert.fConvertStringToLong(gLibGalacDataParse.GetString(0, "Consecutivo", ""))
         If vConsecutivo = 0 Then
            gMessage.InformationMessage "No fue escogido ningún registro para contabilizar", "Contabilización de Transferencia entre Cuentas Bancarias"
         Else
            vCodigoCuentaBancaria = gLibGalacDataParse.GetString(0, "CodigoCuentaBancariaOrigen")
            vFechaAnulacion = gConvert.fConvertStringToDate(gLibGalacDataParse.GetString(0, "FechaDeAnulacion"))
            vStatus = gLibGalacDataParse.GetString(0, "Status")
            vStatusInt = 0
            If gTexto.fS1EsIgualAS2("Anulada", vStatus) Then
               vFecha = vFechaAnulacion
               vEsReverso = True
               vStatusInt = 1
            End If
            vNroDocContabiliacion = gConvert.enumerativoAChar(vStatusInt) & gTexto.fSeparadorStandardDeElementosString & gConvert.fConvierteAString(vConsecutivo)
            If (insGenerarComprobanteNavigator.fValidaDocumento(True, Contabilizar, eCG_TRANSFERENCIA_ENTRE_CUENTAS_BANCARIA, vFecha, vNumero, vEsReverso, False, -1, vConsecutivo)) Then
                Set insCtaBancaria = New clsCuentaBancariaNavigator
                insMovimientoBancario.sCargarValoresDeLaCuentaBancaria insCtaBancaria, vCodigoCuentaBancaria, False
                insGenerarComprobanteNavigator.sContabilizaDocumento True, Contabilizar, eCG_TRANSFERENCIA_ENTRE_CUENTAS_BANCARIA, vFecha, vNroDocContabiliacion, vEsReverso, True, -1, vConsecutivo, insCtaBancaria.GetTipoDeAlicuotaPorContribuyenteAsEnum
               Set insCtaBancaria = Nothing
            End If
         End If
      End If
   End If
   vParametros = "<GpData><DatosDocumento><Consecutivo>-1</Consecutivo></DatosDocumento></GpData>"
   Set insMovimientoBancario = Nothing
   Set insGenerarComprobanteNavigator = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsContabilizarMenu", "sEjecutaProcesoDeContabilizacionDeTransferenciaEntreCuentas", "clsContabilizarMenu", eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sEjecutacontabilizarOrdenDeProduccion()
   Dim objWrapper As Object
   Dim vParametros As String
   Dim vDatos As String
   Dim vConsecutivo As Long
   Dim insGenerarComprobanteNavigator As clsGenerarComprobanteNavigator
   On Error GoTo h_ERROR
   If fSePuedeEjecutarContabilizacion(gEnumProyectoWincont.enumComprobanteGeneradoPorToString(eCG_ORDEN_DE_PRODUCCION)) Then
      Set insGenerarComprobanteNavigator = New clsGenerarComprobanteNavigator
      'TODO:MAYEL ajustar nombre de valComponentName, valClassName según aplique
      Set objWrapper = gUtilWrp.fCreateWrpForModule(gUtilWrp.fWrpClassName("Inventario", "wrpOrdenDeProduccion"), gProyUsuarioActual.GetNombreDelUsuario, gProyUsuarioActual.GetPassword)

      vParametros = "<GpData><DatosDocumento><Consecutivo>-1</Consecutivo>"
      vParametros = vParametros & "<FechaAperturaDelPeriodo>" & gConvert.dateToString(gContPeriodoActual.GetFechaAperturaDelPeriodo) & "</FechaAperturaDelPeriodo>"
      vParametros = vParametros & "<FechaCierreDelPeriodo>" & gConvert.dateToString(gContPeriodoActual.GetFechaCierreDelPeriodo) & "</FechaCierreDelPeriodo></DatosDocumento>"
      vParametros = vParametros & "<Parametros><UsaMonedaExtranjera>" & gConvert.ConvertBooleanToString(gProyParametrosCompania.GetUsaMonedaExtranjera) & "</UsaMonedaExtranjera>"
      vParametros = vParametros & "<CodigoMonedaExtranjera>" & gProyParametrosCompania.GetCodigoMonedaExtranjera & "</CodigoMonedaExtranjera>"
      vParametros = vParametros & "<CantidadDeDecimales>" & gProyParametrosCompania.GetCantidadDeDecimalesStr & "</CantidadDeDecimales>"
      vParametros = vParametros & "<UsaDivisaComoMonedaPrincipalDeIngresoDeDatos>" & gConvert.ConvertBooleanToString(gProyParametrosCompania.GetUsaDivisaComoMonedaPrincipalDeIngresoDeDatos) & "</UsaDivisaComoMonedaPrincipalDeIngresoDeDatos>"
      vParametros = vParametros & "<CostoTerminadoCalculadoAPartirDe>" & gConvert.enumerativoAChar(gProyParametrosCompania.GetCostoTerminadoCalculadoAPartirDeAsEnum) & "</CostoTerminadoCalculadoAPartirDe>"
      vParametros = vParametros & "<UsaAlmacen>" & gConvert.ConvertBooleanToString(gProyParametrosCompania.GetUsaAlmacen) & "</UsaAlmacen></Parametros>"
      vParametros = vParametros & "</GpData>"
      vDatos = objWrapper.ExecuteAndReturnValue(gDefgen.AccionSobreRecordStr(AccionSobreRecord.Contabilizar), gConvert.fConvierteAString(gProyCompaniaActual.GetConsecutivoCompania), vParametros)
      If (vDatos = "") Then
         gMessage.InformationMessage "Acción cancelada por el usuario. ", "Contabilización de Orden de Producción"
      Else
         gLibGalacDataParse.Initialize vDatos
         gLibGalacDataParse.SetRootName "Datos"
         gLibGalacDataParse.SetResultName "Documento"
         vConsecutivo = gConvert.fConvertStringToLong(gLibGalacDataParse.GetString(0, "Consecutivo"))
         If vConsecutivo = 0 Then
            gMessage.InformationMessage "No fue escogido ningún registro para contabilizar", "Contabilización de Orden de Producción"
         Else
            Dim vStatusOp As String
            Dim vFecha As Date
            Dim vEsReverso As Boolean
            Dim vNumero As String
            Dim vNroDocContabiliacion As String
            Dim vStatusOpInt As Integer
            vEsReverso = False
            vStatusOp = gLibGalacDataParse.GetString(0, "StatusOp")
            vStatusOpInt = gConvert.ConvierteAInteger(vStatusOp)
            vFecha = gConvert.fConvertStringToDate(gLibGalacDataParse.GetString(0, "FechaFinalizacion"))
            If Not gTexto.fS1EsIgualAS2(vStatusOp, "2") Then 'Si no está Cerrada > no se contabiliza
               GoTo h_EXIT
            End If
            vNroDocContabiliacion = gConvert.enumerativoAChar(vStatusOpInt) & gTexto.fSeparadorStandardDeElementosString & gConvert.fConvierteAString(vConsecutivo)
            If insGenerarComprobanteNavigator.fValidaDocumento(True, Contabilizar, eCG_ORDEN_DE_PRODUCCION, vFecha, vNumero, False, False, eCG_ORDEN_DE_PRODUCCION, vConsecutivo) Then
               insGenerarComprobanteNavigator.sContabilizaDocumento True, Contabilizar, eCG_ORDEN_DE_PRODUCCION, vFecha, vNroDocContabiliacion, vEsReverso, False, -1, vConsecutivo, -1
            End If
         End If
      End If
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsContabilizarMenu", "sEjecutacontabilizarOrdenDeProduccion", "clsContabilizarMenu", eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

