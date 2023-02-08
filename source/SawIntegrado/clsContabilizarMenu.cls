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
Private Function CM_FILE_NAME() As String
   CM_FILE_NAME = "clsContabilizarMenu"
End Function

Private Function CM_MESSAGE_NAME() As String
   CM_MESSAGE_NAME = "clsContabilizarMenu"
End Function

Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function

Private Function CM_ANTICIPO_COBRADO() As String
   CM_ANTICIPO_COBRADO = gProyReglasDeContabilizacion.fTextoMenuDeContabilizar(eCG_ANTICIPO) & "-" & gEnumProyecto.enumTipoDeAnticipoToString(eTDA_COBRADO)
End Function

Private Function CM_ANTICIPO_PAGADO() As String
   CM_ANTICIPO_PAGADO = gProyReglasDeContabilizacion.fTextoMenuDeContabilizar(eCG_ANTICIPO) & "-" & gEnumProyecto.enumTipoDeAnticipoToString(eTDA_PAGADO)
End Function

Private Function CM_NO_APLICA_TIPO() As Integer
   CM_NO_APLICA_TIPO = -1
End Function

Public Sub sCreaElMenuDeContabilizar(ByRef refForma As Form)
   Dim numSubMenu As Long
   Dim ID As Long
   Dim puedoActivar As Boolean
   Dim insLibServerProperty As clsLibServerProperty

   On Error GoTo h_ERROR
   Set insLibServerProperty = New clsLibServerProperty
   puedoActivar = Not gProyCompaniaActual.GetEsCatalogoGeneral
   If gProyUsuarioActual.GetUsaContabilidad Then
      numSubMenu = gUtilMenus.fAddSubMenu(refForma, gEnumProyecto.enum_SystemModulesToStr(enum_SystemModules.eSM_Contabilizar), ID)
      Call gUtilMenus.fAddMenuItem(refForma, gProyReglasDeContabilizacion.fTextoMenuDeContabilizar(eCG_FACTURA), numSubMenu, gProyReglasDeContabilizacion.fMostrarElMenu(eCG_FACTURA) And puedoActivar, False, eSC_NoShortcut, True, 13)
      Call gUtilMenus.fAddMenuItem(refForma, gProyReglasDeContabilizacion.fTextoMenuDeContabilizar(eCG_CXC), numSubMenu, gProyReglasDeContabilizacion.fMostrarElMenu(eCG_CXC) And puedoActivar, False, eSC_NoShortcut, True, 13)
      Call gUtilMenus.fAddMenuItem(refForma, gProyReglasDeContabilizacion.fTextoMenuDeContabilizar(eCG_COBRANZA), numSubMenu, gProyReglasDeContabilizacion.fMostrarElMenu(eCG_COBRANZA) And puedoActivar, False, eSC_NoShortcut, True, 15)
      Call gUtilMenus.fAddMenuItem(refForma, gProyReglasDeContabilizacion.fTextoMenuDeContabilizar(eCG_CXP), numSubMenu, gProyReglasDeContabilizacion.fMostrarElMenu(eCG_CXP) And puedoActivar, False, eSC_NoShortcut, True, 15)
      Call gUtilMenus.fAddMenuItem(refForma, gProyReglasDeContabilizacion.fTextoMenuDeContabilizar(eCG_PAGOS), numSubMenu, gProyReglasDeContabilizacion.fMostrarElMenu(eCG_PAGOS) And puedoActivar, False, eSC_NoShortcut, True, 14)
      Call gUtilMenus.fAddMenuItem(refForma, gProyReglasDeContabilizacion.fTextoMenuDeContabilizar(eCG_MOVIMIENTO_BANCARIO), numSubMenu, gProyReglasDeContabilizacion.fMostrarElMenu(eCG_MOVIMIENTO_BANCARIO) And puedoActivar, False, eSC_NoShortcut, True, 13)
      Call gUtilMenus.fAddMenuItem(refForma, gProyReglasDeContabilizacion.fTextoMenuDeContabilizar(eCG_RESUMEN_DIARIO_VENTAS), numSubMenu, gProyReglasDeContabilizacion.fMostrarElMenu(eCG_RESUMEN_DIARIO_VENTAS) And puedoActivar, False, eSC_NoShortcut, True, 13)
      Call gUtilMenus.fAddMenuItem(refForma, CM_ANTICIPO_COBRADO, numSubMenu, gProyReglasDeContabilizacion.fMostrarElMenu(eCG_ANTICIPO) And puedoActivar, False, eSC_NoShortcut, True, 8)
      Call gUtilMenus.fAddMenuItem(refForma, CM_ANTICIPO_PAGADO, numSubMenu, gProyReglasDeContabilizacion.fMostrarElMenu(eCG_ANTICIPO) And puedoActivar, False, eSC_NoShortcut, True, 8)
      Call gUtilMenus.fAddMenuItem(refForma, gProyReglasDeContabilizacion.fTextoMenuDeContabilizar(eCG_REPOSICION), numSubMenu, gProyReglasDeContabilizacion.fMostrarElMenu(eCG_REPOSICION) And puedoActivar, False, eSC_NoShortcut, True, 17)
      Call gUtilMenus.fAddMenuItem(refForma, gProyReglasDeContabilizacion.fTextoMenuDeContabilizar(eCG_TRANSFERENCIA_ENTRE_CUENTAS_BANCARIA), numSubMenu, gProyReglasDeContabilizacion.fMostrarElMenu(eCG_TRANSFERENCIA_ENTRE_CUENTAS_BANCARIA) And puedoActivar, False, eSC_NoShortcut, True, 17)
      Set insLibServerProperty = Nothing
      If gDefgen.GetElProgramaEstaEnModoAvanzado Then
         Call gUtilMenus.fAddSeparador(refForma, numSubMenu)
         Call gUtilMenus.fAddMenuItem(refForma, "Contabiliza Documento Por Lote", numSubMenu, gProyUsuarioActual.GetInsertarComprobante And puedoActivar And gDefgen.GetElProgramaEstaEnModoAvanzado, False)
      End If
      Call gUtilMenus.fAddSeparador(refForma, numSubMenu)
      Call gUtilMenus.fAddMenuItem(refForma, "Contabiliza Pago de Sueldos", numSubMenu, gProyReglasDeContabilizacion.fMostrarElMenu(eCG_PAGO_SUELDOS) And puedoActivar, False, eSC_NoShortcut, True, 13)
     If (gProyParametrosCompania.GetMetodoDeCosteoAsEnum = enum_TipoDeMetodoDeCosteo.eTDMDC_CostoPromedio) Then
       Call gUtilMenus.fAddMenuItem(refForma, "Contabilizar Costos Inventario", numSubMenu, gProyReglasDeContabilizacion.fMostrarElMenu(eCG_INVENTARIO) And puedoActivar, False, eSC_NoShortcut, True, 16)
       Call gUtilMenus.fAddMenuItem(refForma, "Borrar comprobante de  Costos Inventario", numSubMenu, gProyReglasDeContabilizacion.fMostrarElMenu(eCG_INVENTARIO) And puedoActivar, False, eSC_NoShortcut, True, 16)
     End If
     Call gUtilMenus.fAddSeparador(refForma, numSubMenu)
     Call gUtilMenus.fAddMenuItem(refForma, "Reparar Inconsistencias", numSubMenu, gDefgen.GetElProgramaEstaEnModoAvanzado And puedoActivar, False, eSC_NoShortcut, gDefgen.GetElProgramaEstaEnModoAvanzado)
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sCreaElMenuDeContabilizar", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Sub sEjecutaAccionEscogidaMenuContabilizar(ByVal valCaptionMenuEscogido As String)
   On Error GoTo h_ERROR
   Select Case valCaptionMenuEscogido
      Case gProyReglasDeContabilizacion.fTextoMenuDeContabilizar(eCG_CXC): sEjecutaContabilizar eCG_CXC, CM_NO_APLICA_TIPO
      Case gProyReglasDeContabilizacion.fTextoMenuDeContabilizar(eCG_FACTURA): sEjecutaContabilizar eCG_FACTURA, CM_NO_APLICA_TIPO
      Case gProyReglasDeContabilizacion.fTextoMenuDeContabilizar(eCG_COBRANZA): sEjecutaContabilizar eCG_COBRANZA, CM_NO_APLICA_TIPO
      Case gProyReglasDeContabilizacion.fTextoMenuDeContabilizar(eCG_CXP): sEjecutaContabilizar eCG_CXP, CM_NO_APLICA_TIPO
      Case gProyReglasDeContabilizacion.fTextoMenuDeContabilizar(eCG_PAGOS): sEjecutaContabilizar eCG_PAGOS, CM_NO_APLICA_TIPO
      Case gProyReglasDeContabilizacion.fTextoMenuDeContabilizar(eCG_MOVIMIENTO_BANCARIO): sEjecutaContabilizar eCG_MOVIMIENTO_BANCARIO, CM_NO_APLICA_TIPO
      Case gProyReglasDeContabilizacion.fTextoMenuDeContabilizar(eCG_RESUMEN_DIARIO_VENTAS): sEjecutaContabilizar eCG_RESUMEN_DIARIO_VENTAS, CM_NO_APLICA_TIPO
      Case CM_ANTICIPO_COBRADO: sEjecutaContabilizar eCG_ANTICIPO, enum_TipoDeAnticipo.eTDA_COBRADO
      Case CM_ANTICIPO_PAGADO: sEjecutaContabilizar eCG_ANTICIPO, enum_TipoDeAnticipo.eTDA_PAGADO
      Case "Contabiliza Documento Por Lote": sEjecutaProcesoDeContabilizacionPorLote
      Case "Contabilizar Costos Inventario": sEjecutaContabilizar eCG_INVENTARIO, CM_NO_APLICA_TIPO
      Case "Borrar comprobante de  Costos Inventario":   sEjecutaBorrarComprobantesDeCostosDeInventario eCG_INVENTARIO, CM_NO_APLICA_TIPO
      Case "Contabiliza Pago de Sueldos": sEjecutaContabilizar eCG_PAGO_SUELDOS, CM_NO_APLICA_TIPO
      Case "Contabilizar Reposición de Caja Chica": sEjecutaProcesoDeContabilizacionDeReposicion
      Case "Contabilizar Transferencia entre Cuentas Bancarias": sEjecutaProcesoDeContabilizacionDeTransferenciaEntreCuentas
      Case "Reparar Inconsistencias": sEjecutaRepararInconsistenciasAuxiliar
   End Select
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sEjecutaAccionEscogidaMenuContabilizar", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
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
      Case eCG_INVENTARIO
           sEjecutaInventario eCG_INVENTARIO, valTipoDocumento
       Case eCG_PAGO_SUELDOS
            sEjecutaAccionProcesar Contabilizar
   End Select
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sEjecutaContabilizar", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sEjecutaContabilizarPorLote(ByVal valModulo As enum_ComprobanteGeneradoPor, ByVal valTipoDocumento As Integer)
   Dim insFrmGeneracionDeComprobantes As frmGeneracionDeComprobantes
   On Error GoTo h_ERROR
   If gProyCompaniaActual.fLaCompaniaYElPeriodoFueronEscogidos Then
      If Not gContPeriodoActual.ElPeriodoEstaCerrado(True) Then
         Set insFrmGeneracionDeComprobantes = New frmGeneracionDeComprobantes
         insFrmGeneracionDeComprobantes.sInitLookAndFeelAndSetValues insertar, valModulo, valTipoDocumento
         insFrmGeneracionDeComprobantes.Show vbModal
      End If
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sEjecutaContabilizarPorLote", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sEjecutaContabilizarCxCDiaria()
   Dim insFrmContabilizar As frmListContabilizacion
   Dim insCxC As clsCxCNavigator
   Dim fixedAdditionalWhereClauses As String
   On Error GoTo h_ERROR
   If gProyCompaniaActual.fLaCompaniaYElPeriodoFueronEscogidos Then
      If Not gContPeriodoActual.ElPeriodoEstaCerrado(True) Then
         If gAPI.fVentanaAsociadaARecordsEstaAbiertaPonDeprimera(Forms, gEnumProyecto.enum_SystemModulesToStr(eSM_CxC), Contabilizar) Then
            GoTo h_EXIT
         End If
         Set insCxC = New clsCxCNavigator
         Set insFrmContabilizar = New frmListContabilizacion
         fixedAdditionalWhereClauses = insCxC.GetTableName & ".Origen = " & gUtilSQL.fSQLSimpleValueForEnum(enum_OrigenFacturacionOManual.eOF_MANUAL)
         insFrmContabilizar.sInitLookAndFeel Contabilizar, gEnumProyecto.enum_SystemModulesToStr(eSM_CxC), insCxC.GetTableName, insCxC, insCxC.getFN_NO_DOC_ORIGEN_CONTABILIZACION_PARA_INPUT(False), fixedAdditionalWhereClauses
      End If
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sEjecutaContabilizarCxCdiaria", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sEjecutaContabilizarCobranzaDiaria()
   Dim insFrmContabilizar As frmListContabilizacion
   Dim insCobranza As clsCobranzaNavigator
   On Error GoTo h_ERROR
   If gProyCompaniaActual.fLaCompaniaYElPeriodoFueronEscogidos Then
      If Not gContPeriodoActual.ElPeriodoEstaCerrado(True) Then
         If gAPI.fVentanaAsociadaARecordsEstaAbiertaPonDeprimera(Forms, gEnumProyecto.enum_SystemModulesToStr(eSM_Cobranza), Contabilizar) Then
            GoTo h_EXIT
         End If
         Set insCobranza = New clsCobranzaNavigator
         Set insFrmContabilizar = New frmListContabilizacion
         insFrmContabilizar.sInitLookAndFeel Contabilizar, gEnumProyecto.enum_SystemModulesToStr(eSM_Cobranza), insCobranza.GetTableName, insCobranza, "Numero"
      End If
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sEjecutaContabilizarCobranzaDiaria", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sEjecutaContabilizarCxPdiaria()
   Dim insFrmContabilizar As frmListContabilizacion
   Dim insCxP As clsCxPNavigator
   On Error GoTo h_ERROR
   If gProyCompaniaActual.fLaCompaniaYElPeriodoFueronEscogidos Then
      If Not gContPeriodoActual.ElPeriodoEstaCerrado(True) Then
         If gAPI.fVentanaAsociadaARecordsEstaAbiertaPonDeprimera(Forms, gEnumProyecto.enum_SystemModulesToStr(eSM_CxP), Contabilizar) Then
            GoTo h_EXIT
         End If
         Set insCxP = New clsCxPNavigator
         Set insFrmContabilizar = New frmListContabilizacion
         insFrmContabilizar.sInitLookAndFeel Contabilizar, gEnumProyecto.enum_SystemModulesToStr(eSM_CxP), insCxP.GetTableName, insCxP, insCxP.getFN_NO_DOC_ORIGEN_CONTABILIZACION_PARA_INPUT(False), "EstaAsociadoARendicion = 'N'"
      End If
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sEjecutaContabilizarCxPdiaria", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sEjecutaContabilizarPagoDiaria()
   Dim insFrmContabilizar As frmListContabilizacion
   Dim insPago As clsPagoNavigator
   On Error GoTo h_ERROR
   If gProyCompaniaActual.fLaCompaniaYElPeriodoFueronEscogidos Then
      If Not gContPeriodoActual.ElPeriodoEstaCerrado(True) Then
         If gAPI.fVentanaAsociadaARecordsEstaAbiertaPonDeprimera(Forms, gEnumProyecto.enum_SystemModulesToStr(eSM_Pago), Contabilizar) Then
            GoTo h_EXIT
         End If
         Set insPago = New clsPagoNavigator
         Set insFrmContabilizar = New frmListContabilizacion
         insFrmContabilizar.sInitLookAndFeel Contabilizar, gEnumProyecto.enum_SystemModulesToStr(eSM_Pago), insPago.GetTableName, insPago, "NumeroComprobante", " Origen <> " & gConvert.enumerativoAChar(enum_OrigenCXPoCXCoManual.eOCXPCXCM_CxP)
      End If
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sEjecutaContabilizarPagodiaria", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sEjecutaContabilizarMovimientoBancarioDiaria()
   Dim insFrmContabilizar As frmListContabilizacion
   Dim insMovBancario As clsMovimientoBancarioNavigator
   Dim fixedAdditionalWhereClauses As String
   On Error GoTo h_ERROR
   If gProyCompaniaActual.fLaCompaniaYElPeriodoFueronEscogidos Then
      If Not gContPeriodoActual.ElPeriodoEstaCerrado(True) Then
         If gAPI.fVentanaAsociadaARecordsEstaAbiertaPonDeprimera(Forms, gEnumProyecto.enum_SystemModulesToStr(eSM_MovimientoBancario), Contabilizar) Then
            GoTo h_EXIT
         End If
         Set insMovBancario = New clsMovimientoBancarioNavigator
         Set insFrmContabilizar = New frmListContabilizacion
         fixedAdditionalWhereClauses = insMovBancario.GetTableName & ".GeneradoPor = " & gUtilSQL.fSQLSimpleValueForEnum(enum_GeneradoPor.eGP_USUARIO)
         insFrmContabilizar.sInitLookAndFeel Contabilizar, gEnumProyecto.enum_SystemModulesToStr(eSM_MovimientoBancario), insMovBancario.GetTableName, insMovBancario, "ConsecutivoMovimiento", fixedAdditionalWhereClauses
      End If
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sEjecutaContabilizarMovimientoBancariodiaria", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sEjecutaContabilizarFacturaDiaria()
   Dim insFrmContabilizar As frmListContabilizacion
   Dim insFactura As clsFacturaNavigator
   Dim fixedAdditionalWhereClauses As String
   On Error GoTo h_ERROR
   If gProyCompaniaActual.fLaCompaniaYElPeriodoFueronEscogidos Then
      If Not gContPeriodoActual.ElPeriodoEstaCerrado(True) Then
         If gAPI.fVentanaAsociadaARecordsEstaAbiertaPonDeprimera(Forms, gEnumProyecto.enum_SystemModulesToStr(eSM_Factura), Contabilizar) Then
            GoTo h_EXIT
         End If
         Set insFrmContabilizar = New frmListContabilizacion
         Set insFactura = New clsFacturaNavigator
         insFactura.setClaseDeTrabajo eCTFC_Factura
         fixedAdditionalWhereClauses = insFactura.GetTableName & "." & insFactura.getFN_STATUS_FACTURA & " <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_BORRADOR) & " AND " & insFactura.GetTableName & "." & insFactura.getFN_TIPO_DE_DOCUMENTO & " <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_RESUMENDIARIODEVENTAS)
         insFrmContabilizar.sInitLookAndFeel Contabilizar, gEnumProyecto.enum_SystemModulesToStr(eSM_Factura), insFactura.GetTableName, insFactura, insFactura.getFN_NO_DOC_ORIGEN_CONTABILIZACION_PARA_INPUT(False), fixedAdditionalWhereClauses
      End If
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sEjecutaContabilizarFacturadiaria", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sEjecutaContabilizarResumenVentasDiaria()
   Dim insFrmContabilizar As frmListContabilizacion
   Dim insResumenVts As clsFacturaNavigator
   Dim fixedAdditionalWhereClauses As String
   On Error GoTo h_ERROR
   If gProyCompaniaActual.fLaCompaniaYElPeriodoFueronEscogidos Then
      If Not gContPeriodoActual.ElPeriodoEstaCerrado(True) Then
         If gAPI.fVentanaAsociadaARecordsEstaAbiertaPonDeprimera(Forms, gEnumProyecto.enum_SystemModulesToStr(eSM_Factura), Contabilizar) Then
            GoTo h_EXIT
         End If
         Set insFrmContabilizar = New frmListContabilizacion
         Set insResumenVts = New clsFacturaNavigator
         insResumenVts.setClaseDeTrabajo eCTFC_Factura
         insResumenVts.SetTipoDeDocumentoStr gEnumProyecto.enumTipodocumentoFacturaToString(eTF_RESUMENDIARIODEVENTAS)
         fixedAdditionalWhereClauses = insResumenVts.GetTableName & "." & insResumenVts.getFN_STATUS_FACTURA & " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_BORRADOR) & " AND " & insResumenVts.GetTableName & "." & insResumenVts.getFN_TIPO_DE_DOCUMENTO & " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_RESUMENDIARIODEVENTAS) & _
         " AND " & insResumenVts.GetTableName & ".RealizoCierreZ = '" & gConvert.ConvertBooleanToString(False) & "'"
         insFrmContabilizar.sInitLookAndFeel Contabilizar, gEnumProyecto.enum_SystemModulesToStr(eSM_Factura), insResumenVts.GetTableName, insResumenVts, insResumenVts.getFN_NO_DOC_ORIGEN_CONTABILIZACION_PARA_INPUT(False), fixedAdditionalWhereClauses
      End If
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sEjecutaContabilizarResumenVentasDiaria", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sEjecutaContabilizarAnticipo(ByVal valTipoDocumento As enum_TipoDeAnticipo)
   Dim insFrmContabilizar As frmListContabilizacion
   Dim insAnticipo As clsAnticipoNavigator
   Dim fixedAdditionalWhereClauses As String
   On Error GoTo h_ERROR
   If gProyCompaniaActual.fLaCompaniaYElPeriodoFueronEscogidos Then
      If Not gContPeriodoActual.ElPeriodoEstaCerrado(True) Then
         If gAPI.fVentanaAsociadaARecordsEstaAbiertaPonDeprimera(Forms, gEnumProyecto.enum_SystemModulesToStr(eSM_Anticipo), Contabilizar) Then
            GoTo h_EXIT
         End If
         Set insFrmContabilizar = New frmListContabilizacion
         Set insAnticipo = New clsAnticipoNavigator
         insAnticipo.SetTipoStr gEnumProyecto.enumTipoDeAnticipoToString(valTipoDocumento)
         fixedAdditionalWhereClauses = insAnticipo.GetTableName & ".Tipo = " & gUtilSQL.fSQLSimpleValueForEnum(valTipoDocumento) & " AND " & _
            insAnticipo.GetTableName & ".GeneradoPor <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_GeneradoPor.eGP_RETENCION_EN_COBRANZA)
         insFrmContabilizar.sInitLookAndFeel Contabilizar, gEnumProyecto.enum_SystemModulesToStr(eSM_Anticipo), insAnticipo.GetTableName, insAnticipo, "ConsecutivoAnticipo", fixedAdditionalWhereClauses
      End If
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sEjecutaContabilizarAnticipo", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Private Sub sEjecutaProcesoDeContabilizacionPorLote()
   Dim insFrmContabilizacionPorLote As frmContabilizacionPorLote
   On Error GoTo h_ERROR
   If gProyCompaniaActual.fLaCompaniaYElPeriodoFueronEscogidos Then
      If gAPI.fVentanaAsociadaARecordsEstaAbiertaPonDeprimera(Forms, gEnumProyectoWincont.enum_ContabilidadSystemModulesToStr(eSM_Comprobante), consultar) Then
         GoTo h_EXIT
      End If
      Set insFrmContabilizacionPorLote = New frmContabilizacionPorLote
      insFrmContabilizacionPorLote.Show
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sEjecutaProcesoDeContabilizacionPorLote", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Private Sub sEjecutaInventario(ByVal valModulo As enum_ComprobanteGeneradoPor, ByVal valTipoDocumento As Integer)
   Dim insGenerarComprobanteInv As frmGeneracionDeComprobantes
   Dim insReglasDeContabilizacion As clsReglasDeContabilizacionNavigator
   On Error GoTo h_ERROR
   Set insReglasDeContabilizacion = New clsReglasDeContabilizacionNavigator
   If gProyCompaniaActual.fLaCompaniaYElPeriodoFueronEscogidos Then
      If (Not gContPeriodoActual.ElPeriodoEstaCerrado(True) And insReglasDeContabilizacion.fPuedoContabilizarInv()) Then
         Set insGenerarComprobanteInv = New frmGeneracionDeComprobantes
         insGenerarComprobanteInv.sInitLookAndFeelAndSetValues Generar, valModulo, valTipoDocumento
         insGenerarComprobanteInv.Show vbModal
     End If
   End If
    Set insReglasDeContabilizacion = Nothing
    Set insGenerarComprobanteInv = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sEjecutaContabilizarPorLote", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Private Sub sEjecutaBorrarComprobantesDeCostosDeInventario(ByVal valModulo As enum_ComprobanteGeneradoPor, ByVal valTipoDocumento As Integer)
   Dim insGenerarComprobanteInv As frmGeneracionDeComprobantes
   Dim insReglasDeContabilizacion As clsReglasDeContabilizacionNavigator
   On Error GoTo h_ERROR
   Set insReglasDeContabilizacion = New clsReglasDeContabilizacionNavigator
   If gProyCompaniaActual.fLaCompaniaYElPeriodoFueronEscogidos Then
      If (Not gContPeriodoActual.ElPeriodoEstaCerrado(True)) Then
         Set insGenerarComprobanteInv = New frmGeneracionDeComprobantes
         insGenerarComprobanteInv.sInitLookAndFeelAndSetValues Eliminar, valModulo, valTipoDocumento
         insGenerarComprobanteInv.Show vbModal
     End If
   End If
    Set insReglasDeContabilizacion = Nothing
    Set insGenerarComprobanteInv = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sEjecutaBorrarComprobantesDeCostosDeInventario", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub


Private Sub sEjecutaAccionProcesar(ByVal valAction As AccionSobreRecord)
   Dim insConexion As clsConexionesSawAOS
   Dim insSolicitud  As clsSolicitudesDePagoNavigator
   Dim xmlResultado As String
   Dim vContinuar As Boolean
   Dim vResult As Boolean
   Dim refConsecutivo  As Long
   Dim insSolicitudesDePagoNavigator As clsSolicitudesDePagoNavigator
   On Error GoTo h_ERROR
   vResult = True
   vContinuar = True
   Set insSolicitud = New clsSolicitudesDePagoNavigator
   Set insConexion = New clsConexionesSawAOS
    If insConexion.fSelectAndSetValuesOfSolicitudesDePagoFromAOS("", xmlResultado) Then
        Dim frmSolicitudesDePagoInput As frmSolicitudesDePagoInput
        Set frmSolicitudesDePagoInput = New frmSolicitudesDePagoInput
        gLibGalacDataParse.Initialize xmlResultado
         If ((valAction = Contabilizar) And (gLibGalacDataParse.GetString(0, "StatusStr", "")) = gEnumProyecto.enumStatusSolicitudToString(eSS_PorProcesar)) Then
             vContinuar = False
         End If
        If (vContinuar) Then
            insSolicitud.SetConsecutivoSolicitud gConvert.fConvertStringToLong(gLibGalacDataParse.GetString(0, "ConsecutivoSolicitud", ""))
            insSolicitud.SetNumeroDocumentoOrigen gLibGalacDataParse.GetString(0, "NumeroDocumentoOrigen", "")
            insSolicitud.SetStatusStr gLibGalacDataParse.GetString(0, "StatusStr", "")
            insSolicitud.SetGeneradoPorStr gLibGalacDataParse.GetString(0, "GeneradoPorStr", "")
            frmSolicitudesDePagoInput.sInitLookAndFeelAndSetValues valAction, insSolicitud, eTDFDPS_Cheque
            frmSolicitudesDePagoInput.Show
        Else
           gMessage.AlertMessage "La solicitud  con este estatus no puede ser procesada con esta accion", gLibGalacDataParse.GetString(0, "StatusStr", "")
        End If
    End If
   Set insSolicitud = Nothing
   Set frmSolicitudesDePagoInput = Nothing
   Set insConexion = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sEjecutaAccion", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Function HayValoresParaProcesar(ByVal valConsecutivoSolicitud As Long) As Boolean
    Dim vResult As Boolean
    Dim vSQLWhere As String
    Dim vSQL As String
    Dim rsRecord As ADODB.Recordset
   On Error GoTo h_ERROR
   Set rsRecord = New ADODB.Recordset
    vResult = False
    vSQLWhere = gUtilSQL.fSQLValueWithAnd(vSQLWhere, "Saw.Gv_RenglonSolicitudesDePago_B1.Contabilizado", "N", False)
    vSQLWhere = gUtilSQL.fSQLEnumValueWithAnd(vSQLWhere, "Saw.Gv_RenglonSolicitudesDePago_B1.Status", eSSR_Procesada)
    vSQLWhere = gUtilSQL.fSQLNumberValueWithAnd(vSQLWhere, "Saw.Gv_RenglonSolicitudesDePago_B1.ConsecutivoSolicitud", valConsecutivoSolicitud)
    vSQLWhere = gUtilSQL.fSQLNumberValueWithAnd(vSQLWhere, "Saw.Gv_RenglonSolicitudesDePago_B1.ConsecutivoCompania", gProyCompaniaActual.GetConsecutivoCompania)
    vSQLWhere = gUtilSQL.getWhereSQL(vSQLWhere)
    vSQL = " SELECT Saw.Gv_RenglonSolicitudesDePago_B1.consecutivoRenglon FROM Saw.Gv_RenglonSolicitudesDePago_B1 "
    vSQL = vSQL & vSQLWhere
   If gDbUtil.fOpenRecordSetAllParameters(rsRecord, vSQL, gDefDatabase.Conexion, adLockOptimistic, adUseClient, adOpenStatic) Then
       If rsRecord.RecordCount > 0 Then
          vResult = True
       Else
          gMessage.AlertMessage "Esta Solicitud No posee, registros que procesar.", "Solicitudes De Pago"
          vResult = False
       End If
   End If
 HayValoresParaProcesar = vResult
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "HayValoresParaProcesar", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
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
      Dim Continuar As Boolean
      Dim vCodigoCuentaBancaria As String
      Dim insMovimientoBancario As clsMovimientoBancarioNavigator
      Dim insCtaBancaria As clsCuentaBancariaNavigator
      On Error GoTo h_ERROR
      Continuar = True
      Set insMovimientoBancario = New clsMovimientoBancarioNavigator
      Set objWrapper = gUtilWrp.fCreateWrpForModule(gUtilWrp.fWrpClassName("CajaChica", "WrpReposicion"), gProyUsuarioActual.GetNombreDelUsuario, gProyUsuarioActual.GetPassword)
      parametros = "<GpData><DatosDocumento><Consecutivo>-1</Consecutivo></DatosDocumento></GpData>"
      datos = objWrapper.ExecuteAndReturnValue(gDefgen.AccionSobreRecordStr(AccionSobreRecord.Contabilizar), gConvert.fConvierteAString(gProyCompaniaActual.GetConsecutivoCompania), parametros)
      If (datos <> "") Then
         If (gProyCompaniaActual.GetUsaModuloDeContabilidad()) Then
            gLibGalacDataParse.Initialize datos
            gLibGalacDataParse.SetRootName "Datos"
            gLibGalacDataParse.SetResultName "Documento"
            fecha = gConvert.fConvertStringToDate(gLibGalacDataParse.GetString(0, "FechaCierre", ""))
            Numero = gLibGalacDataParse.GetString(0, "Numero", "")
            Consecutivo = gConvert.fConvertStringToLong(gLibGalacDataParse.GetString(0, "Consecutivo", ""))
            vCodigoCuentaBancaria = gLibGalacDataParse.GetString(0, "CodigoCuentaCajaChica")
            Set generarComprobanteNavigator = New clsGenerarComprobanteNavigator
            If (generarComprobanteNavigator.fValidaDocumento(True, Contabilizar, eCG_REPOSICION, fecha, Numero, False, False, eTDDR_Reposicion, Consecutivo)) Then
               Set insCtaBancaria = New clsCuentaBancariaNavigator
               insMovimientoBancario.sCargarValoresDeLaCuentaBancaria insCtaBancaria, vCodigoCuentaBancaria, True
               generarComprobanteNavigator.sContabilizaDocumento True, Contabilizar, eCG_REPOSICION, fecha, Numero, False, False, eTDDR_Reposicion, Consecutivo, insCtaBancaria.GetTipoDeAlicuotaPorContribuyenteAsEnum
            End If
            Set generarComprobanteNavigator = Nothing
         End If
      Else
         Continuar = False
      End If
      parametros = "<GpData><DatosDocumento><Consecutivo>-1</Consecutivo></DatosDocumento></GpData>"
      Set insMovimientoBancario = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sEjecutaProcesoDeContabilizacionDeReposicion", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sEjecutaRepararInconsistenciasAuxiliar()
   Dim insfrmRepararAuxiliaresEnBlanco As frmRepararAuxiliaresEnBlanco
   On Error GoTo h_ERROR
   Set insfrmRepararAuxiliaresEnBlanco = New frmRepararAuxiliaresEnBlanco
   insfrmRepararAuxiliaresEnBlanco.Show vbModal
   Set insfrmRepararAuxiliaresEnBlanco = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sEjecutaRepararInconsistenciasAuxiliar", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sEjecutaProcesoDeContabilizacionDeTransferenciaEntreCuentas()
   Dim parametros As String
   Dim objWrapper As Object
   Dim vDatos As String
   Dim vFecha As Date
   Dim vNumero As String
   Dim vStatus As String
   Dim vConsecutivo As String
   Dim generarComprobanteNavigator As clsGenerarComprobanteNavigator
   Dim TransferenciaEntreCuentasNav As clsTransferenciaEntreCuentasNav
   Dim vFechaAnulacion As Date
   Dim Continuar As Boolean
   Dim vCodigoCuentaBancaria As String
   Dim insMovimientoBancario As clsMovimientoBancarioNavigator
   Dim insCtaBancaria As clsCuentaBancariaNavigator
   Dim vNroDocContabiliacion As String
   On Error GoTo h_ERROR
   Continuar = True
   Set insMovimientoBancario = New clsMovimientoBancarioNavigator
   Set objWrapper = gUtilWrp.fCreateWrpForModule(gUtilWrp.fWrpClassName("TransferenciaEntreCuentasBancarias", "wrpTransferenciaEntreCuentasBancarias"), gProyUsuarioActual.GetNombreDelUsuario, gProyUsuarioActual.GetPassword)

   parametros = "<GpData><DatosDocumento><Consecutivo>-1</Consecutivo></DatosDocumento></GpData>"
   vDatos = objWrapper.ExecuteAndReturnValue(gDefgen.AccionSobreRecordStr(AccionSobreRecord.Contabilizar), gConvert.fConvierteAString(gProyCompaniaActual.GetConsecutivoCompania), parametros)
   If (vDatos <> "") Then
        If (gProyCompaniaActual.GetUsaModuloDeContabilidad()) Then
            gLibGalacDataParse.Initialize vDatos
            gLibGalacDataParse.SetRootName "Datos"
            gLibGalacDataParse.SetResultName "Documento"
            vFecha = gConvert.fConvertStringToDate(gLibGalacDataParse.GetString(0, "Fecha", ""))
            vNumero = gLibGalacDataParse.GetString(0, "NumeroDocumento", "")
            vConsecutivo = gConvert.fConvertStringToLong(gLibGalacDataParse.GetString(0, "Consecutivo", ""))
            vCodigoCuentaBancaria = gLibGalacDataParse.GetString(0, "CodigoCuentaBancariaOrigen")
            vFechaAnulacion = gConvert.fConvertStringToDate(gLibGalacDataParse.GetString(0, "FechaDeAnulacion", ""))
            vStatus = gLibGalacDataParse.GetString(0, "Status")
            vNroDocContabiliacion = gConvert.enumerativoAChar(gEnumProyecto.strStatusTransferenciaEntreCuentasToNum(vStatus)) & gTexto.fSeparadorStandardDeElementosString & vNumero
            Set TransferenciaEntreCuentasNav = New clsTransferenciaEntreCuentasNav
            Set generarComprobanteNavigator = New clsGenerarComprobanteNavigator
            If (generarComprobanteNavigator.fValidaDocumento(True, Contabilizar, eCG_TRANSFERENCIA_ENTRE_CUENTAS_BANCARIA, vFecha, vNumero, False, False, eSM_TransferenciaEntreCuentasBancarias, vConsecutivo)) Then
                Set insCtaBancaria = New clsCuentaBancariaNavigator
                insMovimientoBancario.sCargarValoresDeLaCuentaBancaria insCtaBancaria, vCodigoCuentaBancaria, False
                generarComprobanteNavigator.sContabilizaDocumento True, Contabilizar, eCG_TRANSFERENCIA_ENTRE_CUENTAS_BANCARIA, vFecha, vNroDocContabiliacion, False, True, eSM_TransferenciaEntreCuentasBancarias, vConsecutivo, insCtaBancaria.GetTipoDeAlicuotaPorContribuyenteAsEnum
               Set insCtaBancaria = Nothing
            End If
            Set generarComprobanteNavigator = Nothing
            Set TransferenciaEntreCuentasNav = Nothing
        End If
    Else
        Continuar = False
    End If
    parametros = "<GpData><DatosDocumento><Consecutivo>-1</Consecutivo></DatosDocumento></GpData>"
    Set insMovimientoBancario = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sEjecutaProcesoDeContabilizacionDeTransferenciaEntreCuentas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

