VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsNoComunWincont"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = False
Option Explicit
Private Const CM_FILE_NAME As String = "clsNoComunWincont"
Private Const CM_MESSAGE_NAME As String = "clsNoComunWincont"
Private Const CM_IMPRIME_COMPROBANTE_DE_PAGO As Byte = 0
Private Const CM_IMPRIME_COMPROBANTE_DE_MOV_BANCARIO As Byte = 1
Private Const CM_IMPRIME_COMPROBANTE_DE_PAGO_SUELDOS As Byte = 2
Private Const GetGender = Enum_Gender.eg_Male

Public Sub sLLamadoAModificarParametrosDesdePeriodoInput(ByVal valSeEstanDefiniendoCuentasInformes As Boolean)
   On Error GoTo h_ERROR
   If Not valSeEstanDefiniendoCuentasInformes Then
      Call frmCompaniaInput.sInsertaLosValoresDeParametros
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sLLamadoAModificarParametrosDesdePeriodoInput", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Function fHasReglasContabilizacionWithCodigoCuenta(ByVal valCodigo As String) As Boolean
   On Error GoTo h_ERROR
   fHasReglasContabilizacionWithCodigoCuenta = False
   If gProyReglasDeContabilizacion.fExistCodigoCuentaInAnyFieldOfReglas(valCodigo) Then
      fHasReglasContabilizacionWithCodigoCuenta = True
   End If
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: fHasReglasContabilizacionWithCodigoCuenta = False
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fHasReglasContabilizacionWithCodigoCuenta", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fGetStrGeneradoPorOptionsWincont() As String
   Dim varEnum As enum_ComprobanteGeneradoPor
   Dim strOpciones As String
   On Error GoTo h_ERROR
   strOpciones = ""
   For varEnum = eCG_USUARIO To eCG_RESERVA
      strOpciones = strOpciones & gConvert.enumerativoAChar(varEnum)
   Next varEnum
   strOpciones = strOpciones & gConvert.enumerativoAChar(enum_ComprobanteGeneradoPor.eCG_RETIRO_ACTIVOFIJO)
   fGetStrGeneradoPorOptionsWincont = strOpciones
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fGetStrGeneradoPorOptionsWincont", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fEsComprobanteDeContabilizacionAutomatica(ByVal valGeneradoPorChr As String) As Boolean
   On Error GoTo h_ERROR
   fEsComprobanteDeContabilizacionAutomatica = False
   If (gTexto.DfInStr(fGetStrGeneradoPorOptionsWincont, valGeneradoPorChr) = 0) Then
      fEsComprobanteDeContabilizacionAutomatica = True
   End If
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fEsComprobanteDeContabilizacionAutomatica", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSePuedeEscogerElComprobanteParaLaAccion(ByVal valAction As AccionSobreRecord, ByVal valGeneradoPorChr As String, ByVal valNoDocumentoOrigen As String, ByVal valShowMessage As Boolean, ByVal valConsecutivoDocumento As Long) As Boolean
   Dim SePuede As Boolean
   On Error GoTo h_ERROR
   SePuede = True
   If Not fEsComprobanteDeContabilizacionAutomatica(valGeneradoPorChr) Then
      SePuede = True
      GoTo h_EXIT
   End If
   Select Case valAction
      Case Eliminar
         If gDefgen.GetElProgramaEstaEnModoAvanzado Then
            SePuede = Not fEsComprobanteGeneradoPorInventario(valGeneradoPorChr)
         Else
            If valShowMessage Then
               gMessage.Advertencia "No se puede " & gDefgen.AccionSobreRecordStr(valAction) _
                     & " un comprobante generado por contabilización automática desde el" _
                     & " sistema administrativo." & vbCr & "Si desea eliminarlo debe proceder a eliminar el" _
                     & " documento de origen."
            End If
            SePuede = False
         End If
      Case Modificar
         If Not gProyUsuarioActual.GetModificarComprobantesAutomaticos Then
            If valShowMessage Then
               gMessage.Advertencia "El usuario actual no tiene nivel de seguridad para " & gDefgen.AccionSobreRecordStr(valAction) & " comprobantes generados automáticamente desde el sistema administrativo."
            End If
            SePuede = False
         Else
            If Not fElDocumentoDeOrigenDelComprobanteFueAnulado(valGeneradoPorChr, valNoDocumentoOrigen, valConsecutivoDocumento) Then
               If valShowMessage Then
                  gMessage.Advertencia "Este comprobante fue  " _
                        & "generado automáticamente desde el sistema administrativo; cualquier modificación que realice en el mismo " _
                        & "puede generarle inconsistencias entre la Contabilidad y el Administrativo."
               End If
               SePuede = Not fEsComprobanteGeneradoPorInventario(valGeneradoPorChr)
            Else
               If valShowMessage Then
                  gMessage.Advertencia "El documento de origen de este comprobante fue anulado, " _
                        & "por lo tanto el mismo no puede ser Modificado."
               End If
               SePuede = False
            End If
         End If
   End Select
h_EXIT: On Error GoTo 0
   fSePuedeEscogerElComprobanteParaLaAccion = SePuede
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSePuedeEscogerElComprobanteParaLaAccion", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fElDocumentoDeOrigenDelComprobanteFueAnulado(ByVal valGeneradoPorChr As String, ByVal valNoDocumentoOrigen As String, ByVal valConsecutivoDocumentoOrigen As Long) As Boolean
   Dim varDocumentoAnulado As Boolean
   On Error GoTo h_ERROR
   varDocumentoAnulado = False
   Select Case gConvert.charAEnumerativoInt(valGeneradoPorChr)
      Case eCG_CXC
         Dim varCxCNavigator As clsCxCNavigator
         Set varCxCNavigator = New clsCxCNavigator
         If varCxCNavigator.fSearchCxCPorNumero("Numero", valNoDocumentoOrigen) Then
            varDocumentoAnulado = (varCxCNavigator.GetStatusAsEnum = eSD_ANULADO)
         End If
         Set varCxCNavigator = Nothing
      Case eCG_CXP
         Dim varCxPNavigator As clsCxPNavigator
         Set varCxPNavigator = New clsCxPNavigator
         If varCxPNavigator.fSearchByConsecutivoCxP(valConsecutivoDocumentoOrigen, False) Then
            varDocumentoAnulado = (varCxPNavigator.GetStatusAsEnum = eSD_ANULADO)
         End If
         Set varCxPNavigator = Nothing
      Case eCG_COBRANZA
         Dim varCobranzaNavigator As clsCobranzaNavigator
         Set varCobranzaNavigator = New clsCobranzaNavigator
         If varCobranzaNavigator.fSearchByField("Numero", valNoDocumentoOrigen, False) Then
            varDocumentoAnulado = (varCobranzaNavigator.GetStatusCobranzaAsEnum = eSC_ANULADA)
         End If
         Set varCobranzaNavigator = Nothing
      Case eCG_PAGOS
         Dim varPagoNavigator As clsPagoNavigator
         Set varPagoNavigator = New clsPagoNavigator
         If varPagoNavigator.fSearchByNumericField("NumeroComprobante", valNoDocumentoOrigen, False) Then
            varDocumentoAnulado = (varPagoNavigator.GetStatusOrdenDePagoAsEnum = eSP_ANULADA)
         End If
         Set varPagoNavigator = Nothing
      Case eCG_FACTURA
         Dim varFacturaNavigator As clsFacturaNavigator
         Dim numeroFactura As String
         Dim tipoDocumentoEnum As enum_TipoDocumentoFactura
         Set varFacturaNavigator = New clsFacturaNavigator
         varFacturaNavigator.setClaseDeTrabajo eCTFC_Factura
         numeroFactura = gTexto.DfMid(valNoDocumentoOrigen, gTexto.DfInStr(valNoDocumentoOrigen, gTexto.fSeparadorStandardDeElementosString) + 1)
         tipoDocumentoEnum = gConvert.charAEnumerativoInt(gTexto.DfMid(valNoDocumentoOrigen, 1, gTexto.DfInStr(valNoDocumentoOrigen, gTexto.fSeparadorStandardDeElementosString) - 1))
         If varFacturaNavigator.fSearchByNumeroTipoDocumento(numeroFactura, tipoDocumentoEnum, True) Then
            varDocumentoAnulado = (varFacturaNavigator.GetStatusFacturaAsEnum = eSF_ANULADA)
         End If
         Set varFacturaNavigator = Nothing
      Case eCG_MOVIMIENTO_BANCARIO
         'No Tiene Opcion Anular
   End Select
h_EXIT: On Error GoTo 0
   fElDocumentoDeOrigenDelComprobanteFueAnulado = varDocumentoAnulado
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fElDocumentoDeOrigenDelComprobanteFueAnulado", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Sub sCopiaLasReglasDeContabilizacionDelCatalogo(ByVal valConsecutivoCompaniaEscogida As Long)
   Dim varReglasDeContabilizacionNavigator As clsReglasDeContabilizacionNavigator
   On Error GoTo h_ERROR
   Set varReglasDeContabilizacionNavigator = New clsReglasDeContabilizacionNavigator
   varReglasDeContabilizacionNavigator.sCopiarReglasDeContabilizacion gProyCompaniaActual.GetConsecutivoCompania, valConsecutivoCompaniaEscogida
   Set varReglasDeContabilizacionNavigator = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sCopiaLasReglasDeContabilizacionDelCatalogo", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Function fSeInicializanDatosParaVerificarExistenciaDelComprobanteDeContabilizacionAutomatica(ByVal valModulo As enum_ComprobanteGeneradoPor, ByVal valFechaDocumento As Date, ByRef refFechaDesde As Date, ByRef refFechaHasta As Date, ByRef refNumeroDocumentoOrigen As String) As Boolean
   Dim puedoContinuar As Boolean
   Dim insGenerarComprobante As clsGenerarComprobanteNavigator
   On Error GoTo h_ERROR
   puedoContinuar = False
   If gProyReglasDeContabilizacion.GetTipoContabilizacionCxCAsEnum = eTD_CADA_DOCUMENTO Then
      GoTo h_EXIT
   End If
   gMessage.ProgrammerMessage "LA FUNCION : 'fSeInicializanDatosParaVerificarExistenciaDelComprobanteDeContabilizacionAutomatica' ESTÁ INCOMPLETA - SI ESTA PROGRAMANDO CONTABILIZACION POR LOTE, VER CODIGO EN EL IVA"
   If gProyReglasDeContabilizacion.GetContabPorLoteCxCAsEnum = eCP_MENSUAL Then
      refFechaDesde = gUtilDate.fColocaDiaEnFecha(1, valFechaDocumento)
      refFechaHasta = gUtilDate.fColocaUltimoDiaDelMes(valFechaDocumento)
   End If
   Set insGenerarComprobante = New clsGenerarComprobanteNavigator
   refNumeroDocumentoOrigen = insGenerarComprobante.fConstruyeNumeroDocumentoDeOrigen(refFechaDesde, refFechaHasta)
   Set insGenerarComprobante = Nothing
   puedoContinuar = True
h_EXIT: On Error GoTo 0
   fSeInicializanDatosParaVerificarExistenciaDelComprobanteDeContabilizacionAutomatica = puedoContinuar
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSeInicializanDatosParaVerificarExistenciaDelComprobanteDeContabilizacionAutomatica", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fMensajeComprobanteResumenYaExiste(ByVal valModulo As enum_ComprobanteGeneradoPor, ByVal valFechaHasta As Date, ByVal valFechaDocumento As Date) As String
   Dim varMensaje As String
   On Error GoTo h_ERROR
   varMensaje = "Este Comprobante Resumen que corresponde a la Contabilización del módulo de " & gEnumProyectoWincont.enumComprobanteGeneradoPorToString(valModulo)
   If gProyReglasDeContabilizacion.GetContabPorLoteCxCAsEnum = eCP_DIARIA Then
      varMensaje = varMensaje & " del Día " & gConvert.dateToString(valFechaHasta)
   Else
      varMensaje = varMensaje & " del Mes de " & gConvert.MesString(valFechaHasta)
   End If
   varMensaje = varMensaje & " ya fue generado. " & vbCr & vbCr & "No pueden ser realizadas operaciones tales como Insertar, Modifica y Eliminar documentos con fecha " & gConvert.dateToString(valFechaDocumento)
   fMensajeComprobanteResumenYaExiste = varMensaje
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fMensajeComprobanteResumenYaExiste", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Sub sColocaPeriodoInsertadoComoElActual(ByVal VarFechaDeApertura As Date)
   Dim insClsBarrasDeMenus As clsBarrasDeMenu
   On Error GoTo h_ERROR
   If gContPeriodoActual.SearchByFechaDeApertura(VarFechaDeApertura) Then
      Set insClsBarrasDeMenus = New clsBarrasDeMenu
      insClsBarrasDeMenus.sReasignaCompaniaYPeriodo gProyCompaniaActual, gContPeriodoActual
      Set insClsBarrasDeMenus = Nothing
      gProyParametrosActivo.fEscogeElRecordDeParametrosActivo
   Else
      gMessage.Advertencia "No se pudo colocar el Periodo insertado como el actual."
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sColocaPeriodoInsertadoComoElActual", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Sub sSiLaCompaniaActualNoTienePeriodosPideElPrimero()
   Dim gBarrasDeMenu As clsBarrasDeMenu
   On Error GoTo h_ERROR
   Set gBarrasDeMenu = New clsBarrasDeMenu
   gBarrasDeMenu.sSiLaCompaniaActualNoTienePeriodosPideElPrimero
   Set gBarrasDeMenu = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sSiLaCompaniaActualNoTienePeriodosPideElPrimero", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Sub sGenerarAuxiliaresDelGrupoEnElPeriodoActual(ByVal valGrupoAuxiliar As enum_GrupoAuxiliar, ByVal valTableName As String, ByVal valFieldNameCodigo As String, ByVal valFieldNameNombre As String, ByVal valFieldNameRIF As String, ByVal valFieldNameNIT As String)
   Dim varAuxiliarNavigator As clsAuxiliarNavigator
   Dim varSQL As String
   On Error GoTo h_ERROR
   Set varAuxiliarNavigator = New clsAuxiliarNavigator
   varSQL = "INSERT INTO " & varAuxiliarNavigator.GetTableName _
            & "(TipoDeAuxiliar" _
            & ", Codigo" _
            & ", Nombre" _
            & ", NoRif" _
            & ", NoNit" _
            & ", NombreOperador" _
            & ", FechaUltimaModificacion" _
            & ", ConsecutivoPeriodo " _
            & ", Consecutivo) "
   varSQL = varSQL & " SELECT '" & gConvert.enumerativoAChar(valGrupoAuxiliar) & "'" _
            & ", " & valTableName & "." & valFieldNameCodigo _
            & ", " & gUtilSQL.DfMidSQL(valTableName & "." & valFieldNameNombre, 1, 35) _
            & ", " & gUtilSQL.DfMidSQL(valTableName & "." & valFieldNameRIF, 1, 10) _
            & ", " & gUtilSQL.DfMidSQL(valTableName & "." & valFieldNameNIT, 1, 10) _
            & ", '" & gProyUsuarioActual.GetNombreDelUsuario & "'" _
            & ", " & gUtilSQL.fDateToSQLValue(gUtilDate.getFechaDeHoy) _
            & ", " & gContPeriodoActual.GetConsecutivoPeriodo _
            & ", ROW_NUMBER() OVER(ORDER BY Consecutivo DESC) + (SELECT ISNULL(MAX(Consecutivo), 0) FROM Contab.Auxiliar WHERE ConsecutivoPeriodo = " & gContPeriodoActual.GetConsecutivoPeriodo & ") AS Conse" _
            & " FROM " & valTableName _
            & " WHERE " & valTableName & ".ConsecutivoCompania " _
            & " = " & gProyCompaniaActual.GetConsecutivoCompania _
            & " AND " & valTableName & "." & valFieldNameCodigo _
            & " NOT IN (SELECT " & varAuxiliarNavigator.GetTableName & ".Codigo" _
            & " FROM " & varAuxiliarNavigator.GetTableName _
            & " WHERE " & varAuxiliarNavigator.GetTableName & ".TipoDeAuxiliar" _
            & " = '" & gConvert.enumerativoAChar(valGrupoAuxiliar) & "'" _
            & " AND " & varAuxiliarNavigator.GetTableName & ".ConsecutivoPeriodo" _
            & " = " & gContPeriodoActual.GetConsecutivoPeriodo & ")"
   gDbUtil.Execute gDefDatabase.Conexion, varSQL
   gMessage.Success "Se generaron los auxiliares exitosamente"
   Set varAuxiliarNavigator = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sGenerarAuxiliaresDelGrupoEnElPeriodoActual", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Function fCopiaClientesYProveedores(ByRef refPrgBarProceso As ProgressBar, ByRef refCompania As clsCompaniaNavigator) As Boolean
   On Error GoTo h_ERROR
   If fCopiaLosProveedores(refPrgBarProceso, refCompania) Then
      If fCopiaLosVendedores(refPrgBarProceso, refCompania) Then
       If fCopiaLosZonaCobranza(refPrgBarProceso, refCompania) Then
         If fCopiaLosClientes(refPrgBarProceso, refCompania) Then
            fCopiaClientesYProveedores = True
         End If
       End If
      End If
   End If
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCopiaClientesYProveedores", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fCopiaLosProveedores(ByRef refPrgBarProceso As ProgressBar, ByRef refCompania As clsCompaniaNavigator) As Boolean
   Dim insProveedor As clsProveedorNavigator
   On Error GoTo h_ERROR
   Set insProveedor = New clsProveedorNavigator
   fCopiaLosProveedores = insProveedor.fCopiaLosProveedoresDeOtraCompania(refCompania)
   gAPI.sAddProgress refPrgBarProceso
   Set insProveedor = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCopiaLosProveedores", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fCopiaLosVendedores(ByRef refPrgBarProceso As ProgressBar, ByRef refCompania As clsCompaniaNavigator) As Boolean
   Dim insVendedor As clsVendedorNavigator
   On Error GoTo h_ERROR
   Set insVendedor = New clsVendedorNavigator
   fCopiaLosVendedores = insVendedor.fCopiaLosVendedoresDeOtraCompania(refCompania)
   gAPI.sAddProgress refPrgBarProceso
   Set insVendedor = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCopiaLosVendedores", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fCopiaLosZonaCobranza(ByRef refPrgBarProceso As ProgressBar, ByRef refCompania As clsCompaniaNavigator) As Boolean
   Dim insZonaCobranza As clsZonaCobranzaNavigator
   On Error GoTo h_ERROR
   Set insZonaCobranza = New clsZonaCobranzaNavigator
   gAPI.sAddProgress refPrgBarProceso
   fCopiaLosZonaCobranza = insZonaCobranza.fCopiaLasZonaCobranzaDeOtraCompania(refCompania.GetConsecutivoCompania)
   Set insZonaCobranza = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCopiaLosZonaCobranza", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fCopiaLosClientes(ByRef refPrgBarProceso As ProgressBar, ByRef refCompania As clsCompaniaNavigator) As Boolean
   Dim insCliente As clsClienteNavigator
   On Error GoTo h_ERROR
   Set insCliente = New clsClienteNavigator
   fCopiaLosClientes = insCliente.fCopiaLosClientesDeOtraCompania(refCompania)
   gAPI.sAddProgress refPrgBarProceso
   Set insCliente = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCopiaLosClientes", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Sub sAddOtherOptionsMenuAuxiliar(ByRef refForma As Form, ByRef refNumSubMenu As Long, ByVal valPuedoActivar As Boolean)
   On Error GoTo h_ERROR
   Call gUtilMenus.fAddSeparador(refForma, refNumSubMenu)
   Call gUtilMenus.fAddMenuItem(refForma, "Generar Auxiliares de Clientes", refNumSubMenu, gProyUsuarioActual.GetModificarCliente And gProyUsuarioActual.GetInsertarTablas And gDefgen.GetElProgramaEstaEnModoAvanzado And valPuedoActivar, False, eSC_NoShortcut, gDefgen.GetElProgramaEstaEnModoAvanzado)
   Call gUtilMenus.fAddMenuItem(refForma, "Generar Auxiliares de Proveedores", refNumSubMenu, gProyUsuarioActual.GetModificarProveedor And gProyUsuarioActual.GetInsertarTablas And gDefgen.GetElProgramaEstaEnModoAvanzado And valPuedoActivar, False, eSC_NoShortcut, gDefgen.GetElProgramaEstaEnModoAvanzado)
   Call gUtilMenus.fAddMenuItem(refForma, "Listar Auxiliares Sin Saldo (Todos los periodos)", refNumSubMenu, gProyUsuarioActual.GetEsSupervisor And gProyUsuarioActual.GetEliminarTablas And gDefgen.GetElProgramaEstaEnModoAvanzado And valPuedoActivar, False, eSC_NoShortcut, gDefgen.GetElProgramaEstaEnModoAvanzado)
   Call gUtilMenus.fAddMenuItem(refForma, "Listar Auxiliares Con Saldo (Todos los periodos)", refNumSubMenu, gProyUsuarioActual.GetEsSupervisor And gProyUsuarioActual.GetConsultarTablas And gDefgen.GetElProgramaEstaEnModoAvanzado And valPuedoActivar, False, eSC_NoShortcut, gDefgen.GetElProgramaEstaEnModoAvanzado)
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sAddOtherOptionsMenuAuxiliar", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Sub sEjecutaAuxiliarGenerar(ByVal valEnumGrupoAuxiliar As enum_GrupoAuxiliar)
   Dim varClienteNavigator As clsClienteNavigator
   Dim varProveedorNavigator As clsProveedorNavigator
   On Error GoTo h_ERROR
   Select Case valEnumGrupoAuxiliar
      Case enum_GrupoAuxiliar.eGA_CLIENTES
         Set varClienteNavigator = New clsClienteNavigator
         sGenerarAuxiliaresDelGrupoEnElPeriodoActual valEnumGrupoAuxiliar, varClienteNavigator.GetTableName, "Codigo", "Nombre", "NumeroRIF", "NumeroNIT"
         Set varClienteNavigator = Nothing
      Case enum_GrupoAuxiliar.eGA_PROVEEDORES
         Set varProveedorNavigator = New clsProveedorNavigator
         sGenerarAuxiliaresDelGrupoEnElPeriodoActual valEnumGrupoAuxiliar, varProveedorNavigator.GetTableName, "CodigoProveedor", "NombreProveedor", "NumeroRIF", "NumeroNIT"
         Set varProveedorNavigator = Nothing
   End Select
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sEjecutaAuxiliarGenerar", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Sub sEjecutaGenerarCXCyCXPaPartirDelComprobanteDeApertura()
   Dim insFrmComprobanteAperturaGenerarCXCyCXP As frmComprobanteAperturaGenerarCXCyCXP
   On Error GoTo h_ERROR
   If gAPI.fExistOpenForm(Forms) Then
      gMessage.Advertencia "Debe cerrar todas las pantallas para poder Generar las CXC y CXP a partir Comprobante Apertura"
   ElseIf Not gContPeriodoActual.ExisteElComprobanteDeApertura Then
      gMessage.Advertencia "Debe crear el comprobante de apertura para poder ejecutar esta opción"
   Else
      Set insFrmComprobanteAperturaGenerarCXCyCXP = New frmComprobanteAperturaGenerarCXCyCXP
      insFrmComprobanteAperturaGenerarCXCyCXP.Show vbModal
      Set insFrmComprobanteAperturaGenerarCXCyCXP = Nothing
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sEjecutaGenerarCXCyCXPaPartirDelComprobanteDeApertura", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Sub sEjecutaInformesDeContabilizacionAutomatica()
   Dim insFrmInformesDeComprobantes As frmRepCompDeContabilizacionAutomac
   On Error GoTo h_ERROR
   If gProyCompaniaActual.fLaCompaniaYElPeriodoFueronEscogidos Then
      Set insFrmInformesDeComprobantes = New frmRepCompDeContabilizacionAutomac
      insFrmInformesDeComprobantes.sInitLookAndFeel
      insFrmInformesDeComprobantes.Show
      Set insFrmInformesDeComprobantes = Nothing
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sEjecutaInformesDeContabilizacionAutomatica", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Sub sEjecutaConversionInfo2000(ByVal valInvocadoDesdeGenerarApertura As Boolean)
'
End Sub

Public Function fUnificarAuxiliarDeClienteOProveedor(ByVal valCodigoASustituir As String, ByVal valCodigoNuevo As String, ByVal valTipoDeAuxiliarChar As String) As Boolean
   Dim varAuxiliarNavigator As clsAuxiliarNavigator
   Dim varAsientoNavigator As clsAsientoNavigator
   Dim varCuentaNavigator As clsCuentaNavigator
   Dim varComprobanteNavigator As clsComprobanteNavigator
   Dim i As Integer
   Dim varSqlSet As String
   Dim varSqlJoin As String
   Dim varSQLWhere  As String
   Dim varSQL As String
   On Error GoTo h_ERROR
   fUnificarAuxiliarDeClienteOProveedor = False
   Set varAuxiliarNavigator = New clsAuxiliarNavigator
   Set varAsientoNavigator = New clsAsientoNavigator
   Set varCuentaNavigator = New clsCuentaNavigator
   Set varComprobanteNavigator = New clsComprobanteNavigator
   For i = 1 To 2
      varSqlSet = " SET " & varAsientoNavigator.GetTableName & ".Auxiliar = '" & valCodigoNuevo & "'"
      varSqlJoin = " INNER JOIN " & varCuentaNavigator.GetTableName & " ON " _
               & varAsientoNavigator.GetTableName & ".CodigoCuenta = " & varCuentaNavigator.GetTableName & ".Codigo" _
               & " AND " & varAsientoNavigator.GetTableName & ".ConsecutivoPeriodo = " & varCuentaNavigator.GetTableName & ".ConsecutivoPeriodo"
      varSQLWhere = " WHERE " & varCuentaNavigator.GetTableName & ".GrupoAuxiliar = '" & valTipoDeAuxiliarChar & "'" _
               & " AND " & varAsientoNavigator.GetTableName & ".Auxiliar = '" & valCodigoASustituir & "'" _
               & " AND " & varAsientoNavigator.GetTableName & ".EsAsientoDeActivoFijo = 'N'" _
               & " AND " & varAsientoNavigator.GetTableName & ".ConsecutivoPeriodo" _
               & " IN ( SELECT ConsecutivoPeriodo" _
               & " FROM " & gContPeriodoActual.GetTableName _
               & " WHERE ConsecutivoCompania" _
               & " = " & gProyCompaniaActual.GetConsecutivoCompania & " ) "
      varSQL = gUtilSQL.DfSQLUpdateWithJoin(varAsientoNavigator.GetTableName, varSqlSet, varSqlJoin, varSQLWhere)
      gDbUtil.Execute gDefDatabase.Conexion, varSQL
      TrabajandoConSaldoInicialContabilidad = (i = 1)
   Next
   varSQL = "UPDATE " & varComprobanteNavigator.GetTableName _
         & " SET NoDocumentoOrigen" _
         & " = '" & valCodigoNuevo & "'" _
         & gUtilSQL.CharConcat _
         & gUtilSQL.fMidFromChar("NoDocumentoOrigen", Trim(gUtilSQL.DfChar(9)), 1) _
         & " WHERE GeneradoPor" _
         & " = '" & gConvert.enumerativoAChar(enum_ComprobanteGeneradoPor.eCG_CXP) & "' " _
         & " AND " & gUtilSQL.DfLeftSQL("NoDocumentoOrigen", 10) & " = '" & valCodigoASustituir & "' " _
         & " AND " & varComprobanteNavigator.GetTableName & ".ConsecutivoPeriodo" _
         & " IN ( SELECT ConsecutivoPeriodo" _
         & " FROM " & gContPeriodoActual.GetTableName _
         & " WHERE ConsecutivoCompania" _
         & " = " & gProyCompaniaActual.GetConsecutivoCompania & " ) "
   gDbUtil.Execute gDefDatabase.Conexion, varSQL
   varSQL = "DELETE FROM " & varAuxiliarNavigator.GetTableName _
         & " WHERE " & varAuxiliarNavigator.GetTableName & ".Codigo = '" & valCodigoASustituir & "'" _
         & " AND " & varAuxiliarNavigator.GetTableName & ".TipoDeAuxiliar = '" & valTipoDeAuxiliarChar & "'" _
         & " AND " & varAuxiliarNavigator.GetTableName & ".ConsecutivoPeriodo" _
         & " IN ( SELECT ConsecutivoPeriodo" _
         & " FROM " & gContPeriodoActual.GetTableName _
         & " WHERE ConsecutivoCompania" _
         & " = " & gProyCompaniaActual.GetConsecutivoCompania & " ) "
   gDbUtil.Execute gDefDatabase.Conexion, varSQL
   fUnificarAuxiliarDeClienteOProveedor = True
   Set varCuentaNavigator = Nothing
   Set varAsientoNavigator = Nothing
   Set varAuxiliarNavigator = Nothing
   Set varComprobanteNavigator = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fUnificarAuxiliarDeClienteOProveedor", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSePuedeEscogerTipoDeComprobante(ByVal valAction As AccionSobreRecord, ByVal valCodigoTipoComprobante As String) As Boolean
   Dim varSePuede As Boolean
   Dim varSQL As String
   On Error GoTo h_ERROR
   varSePuede = True
   If valAction = Modificar Or valAction = Eliminar Then
      varSQL = "SELECT * FROM Saw.ReglasDeContabilizacion" _
            & " WHERE (FacturaTipoComprobante = '" & valCodigoTipoComprobante & "')" _
            & " OR (CxCTipoComprobante = '" & valCodigoTipoComprobante & "')" _
            & " OR (CxPTipoComprobante = '" & valCodigoTipoComprobante & "')" _
            & " OR (CobranzaTipoComprobante = '" & valCodigoTipoComprobante & "')" _
            & " OR (PagoTipoComprobante = '" & valCodigoTipoComprobante & "')" _
            & " OR (MovimientoBancarioTipoComprobante = '" & valCodigoTipoComprobante & "')" _
            & " OR (AnticipoTipoComprobante = '" & valCodigoTipoComprobante & "')"
      varSePuede = (gDbUtil.fRecordCountOfSql(varSQL) <= 0)
      If Not varSePuede Then
         gMessage.sForeingKeyCanNotBeDeleted "Tipo de Comprobante", "Reglas de Contabilización", GetGender
      End If
   End If
h_EXIT: On Error GoTo 0
   fSePuedeEscogerTipoDeComprobante = varSePuede
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSePuedeEscogerTipoDeComprobante", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSePuedeUnificarElAuxiliar(ByVal valTipoDeAuxiliarAsEnum As enum_GrupoAuxiliar, ByVal valCodigoAuxiliar As String) As Boolean
   Dim varSePuede As Boolean
   Dim varClienteNavigator As clsClienteNavigator
   Dim varProveedorNavigator As clsProveedorNavigator
   Dim varRegistroDe As String
   Dim insCnxAos As clsConexionesSawAOS
   Dim refCodigoProveedor As String
   Dim refNombreProveedor As String
   On Error GoTo h_ERROR
   fSePuedeUnificarElAuxiliar = False
   varSePuede = True
   Select Case valTipoDeAuxiliarAsEnum
      Case enum_GrupoAuxiliar.eGA_CLIENTES
         Set varClienteNavigator = New clsClienteNavigator
         varSePuede = Not varClienteNavigator.fSearchByField("Codigo", valCodigoAuxiliar, False)
         varRegistroDe = varClienteNavigator.GetTableName
         Set varClienteNavigator = Nothing
      Case enum_GrupoAuxiliar.eGA_PROVEEDORES
         Set varProveedorNavigator = New clsProveedorNavigator
         Set insCnxAos = New clsConexionesSawAOS
         varSePuede = Not insCnxAos.fSelectAndSetValuesOfProveedorFromAOS(varProveedorNavigator, refCodigoProveedor, refNombreProveedor, valCodigoAuxiliar, "")
         varRegistroDe = varProveedorNavigator.GetTableName
         Set varProveedorNavigator = Nothing
         Set insCnxAos = Nothing
   End Select
   If Not varSePuede Then
      gMessage.Advertencia "El auxiliar " & valCodigoAuxiliar & " no puede ser unificado pues fue originado en el módulo de " & gTexto.fPlural(varRegistroDe) & "."
   End If
h_EXIT: On Error GoTo 0
   fSePuedeUnificarElAuxiliar = varSePuede
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSePuedeUnificarElAuxiliar", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Sub sEliminaComprobantesAnuladosConSaldoDistintoDeCero()
   Dim varComprobanteNavigator As clsComprobanteNavigator
   Dim varCxCNavigator As clsCxCNavigator
   Dim varCxPNavigator As clsCxPNavigator
   Dim varCobranzaNavigator As clsCobranzaNavigator
   Dim varPagoNavigator As clsPagoNavigator
   Dim varFacturaNavigator As clsFacturaNavigator
   Dim varSQL As String
   Dim varConjuntoDocumentosAnulados As String
   Dim varNroRegistrosEliminados As Long
   Dim varEnum As enum_ComprobanteGeneradoPor
   Dim varElimineAlMenosUNo As Boolean
   On Error GoTo h_ERROR
   Set varComprobanteNavigator = New clsComprobanteNavigator
   Set varCxCNavigator = New clsCxCNavigator
   Set varCxPNavigator = New clsCxPNavigator
   Set varCobranzaNavigator = New clsCobranzaNavigator
   Set varPagoNavigator = New clsPagoNavigator
   Set varFacturaNavigator = New clsFacturaNavigator
   varFacturaNavigator.setClaseDeTrabajo eCTFC_Factura
   varElimineAlMenosUNo = False
   For varEnum = eCG_CXC To eCG_RESUMEN_DIARIO_VENTAS
      Select Case varEnum
         Case eCG_CXC
            varSQL = "SELECT Numero FROM " & varCxCNavigator.GetTableName _
               & " WHERE ConsecutivoCompania = " & gProyCompaniaActual.GetConsecutivoCompania _
               & " AND Status = '" & gConvert.enumerativoAChar(enum_StatusDocumento.eSD_ANULADO) & "'"
         Case eCG_COBRANZA
            varSQL = "SELECT Numero FROM " & varCobranzaNavigator.GetTableName _
               & " WHERE ConsecutivoCompania = " & gProyCompaniaActual.GetConsecutivoCompania _
               & " AND StatusCobranza = '" & gConvert.enumerativoAChar(enum_StatusCobranza.eSC_ANULADA) & "'"
         Case eCG_CXP
            varSQL = "SELECT CodigoProveedor" & gProyReglasDeContabilizacion.fSQLSeparadorDeCamposStandardDelNroDocumentoOrigen & "Numero FROM " & varCxPNavigator.GetTableName _
               & " WHERE ConsecutivoCompania = " & gProyCompaniaActual.GetConsecutivoCompania _
               & " AND Status = '" & gConvert.enumerativoAChar(enum_StatusDocumento.eSD_ANULADO) & "'"
         Case eCG_FACTURA
            varSQL = "SELECT " & varFacturaNavigator.getFN_NUMERO & " FROM " & varFacturaNavigator.GetTableName _
               & " WHERE " & varFacturaNavigator.getFN_CONSECUTIVO_COMPANIA & " = " & gProyCompaniaActual.GetConsecutivoCompania _
               & " AND " & varFacturaNavigator.getFN_STATUS_FACTURA & " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA) _
               & " AND " & varFacturaNavigator.getFN_TIPO_DE_DOCUMENTO & " <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_RESUMENDIARIODEVENTAS)
         Case eCG_MOVIMIENTO_BANCARIO
            varSQL = ""
         Case eCG_PAGOS
            varSQL = "SELECT NumeroComprobante FROM " & varPagoNavigator.GetTableName _
               & " WHERE ConsecutivoCompania = " & gProyCompaniaActual.GetConsecutivoCompania _
               & " AND StatusOrdenDePago = '" & gConvert.enumerativoAChar(enum_StatusPago.eSP_ANULADA) & "'"
         Case eCG_RESUMEN_DIARIO_VENTAS
            varSQL = "SELECT " & varFacturaNavigator.getFN_NUMERO & " FROM " & varFacturaNavigator.GetTableName _
               & " WHERE " & varFacturaNavigator.getFN_CONSECUTIVO_COMPANIA & " = " & gProyCompaniaActual.GetConsecutivoCompania _
               & " AND " & varFacturaNavigator.getFN_STATUS_FACTURA & " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA) _
               & " AND " & varFacturaNavigator.getFN_TIPO_DE_DOCUMENTO & " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_RESUMENDIARIODEVENTAS)
      End Select
      If varSQL <> "" Then
         varConjuntoDocumentosAnulados = gDbUtil.fBuildResultSetAsString(varSQL, True)
         varNroRegistrosEliminados = varComprobanteNavigator.fEliminaSiAplicaComprobantesAutomaticosAnulados(varEnum, varConjuntoDocumentosAnulados)
         If varNroRegistrosEliminados > 0 Then
            gMessage.Advertencia "Eliminado(s) " & varNroRegistrosEliminados & " comprobante(s) del período actual cuyo(s) saldo(s) era(n) distinto(s) de cero aún cuando provenía(n) de documento(s) anulado(s)."
            varElimineAlMenosUNo = True
         End If
      End If
   Next varEnum
   If Not varElimineAlMenosUNo Then
      gMessage.Success "No se encontraron inconsistencias entre comprobantes y documentos de origen anulados."
   End If
   Set varComprobanteNavigator = Nothing
   Set varCxCNavigator = Nothing
   Set varCxPNavigator = Nothing
   Set varCobranzaNavigator = Nothing
   Set varPagoNavigator = Nothing
   Set varFacturaNavigator = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sEliminaComprobantesAnuladosConSaldoDistintoDeCero", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Function fHasExternalModuleWithCodigoCuentaContable(ByVal valCodigoCuenta As String, ByRef refReplayerRecordUserName As String) As Boolean
   Dim varHas As Boolean
   On Error GoTo h_ERROR
   fHasExternalModuleWithCodigoCuentaContable = False
   varHas = False
   refReplayerRecordUserName = ""
   If fHasCuentaBancariaWithCuentaContable(valCodigoCuenta) Then
      varHas = True
      refReplayerRecordUserName = gEnumProyecto.enum_SystemModulesToStr(enum_SystemModules.eSM_CuentaBancaria)
   End If
h_EXIT: On Error GoTo 0
   fHasExternalModuleWithCodigoCuentaContable = varHas
   Exit Function
h_ERROR: fHasExternalModuleWithCodigoCuentaContable = False
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fHasExternalModuleWithCodigoCuentaContable", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fHasCuentaBancariaWithCuentaContable(ByVal valCodigo As String) As Boolean
   Dim insCnxAos As clsConexionesSawAOS
   Dim insCuentaBancaria As clsCuentaBancariaNavigator
   On Error GoTo h_ERROR
   Set insCnxAos = New clsConexionesSawAOS
   If insCnxAos.fSelectAndSetValuesOfCuentaBancariaFromAOS(insCuentaBancaria, valCodigo, "CuentaContable", "", "", "", False, False, False, False) Then
      fHasCuentaBancariaWithCuentaContable = True
   End If
   Set insCnxAos = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: fHasCuentaBancariaWithCuentaContable = False
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fHasCuentaBancariaWithCuentaContable", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fCuentaPuedeManejarAuxiliar(ByVal valCodigo As String) As Boolean
   fCuentaPuedeManejarAuxiliar = True
End Function

Public Function fCuentaPuedeManejarActivoFijo(ByVal valCodigo As String) As Boolean
   fCuentaPuedeManejarActivoFijo = True
End Function

Public Function fLaCuentaPerteneceALasReglasDeContabilizacion(ByVal valCodigo As String) As Boolean
   On Error GoTo h_ERROR
   fLaCuentaPerteneceALasReglasDeContabilizacion = False
   If gProyReglasDeContabilizacion.fExistCodigoCuentaInAnyFieldOfReglas(valCodigo) Then
      fLaCuentaPerteneceALasReglasDeContabilizacion = True
   End If
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fLaCuentaPerteneceALasReglasDeContabilizacion", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fEstoyIntegradoConAdministrativo() As Boolean
   fEstoyIntegradoConAdministrativo = True
End Function

Public Function fImprimirEspecialAlGrabarComprobante(ByVal valImprimirEspecialAlGrabarComprobante As Boolean, ByVal valTipoDeComprobanteAImprimir As Byte, ByRef RefPagoProcesoDeImpresionEspecial As clsProcesoDeImpresionEspecial, Optional ByVal valNumeroDeMovimientoBancario As Long = 1) As Boolean
   Dim insPagoProcesoDeImpresionEspecial As clsProcesoDeImpresionEspecial
   On Error GoTo h_ERROR
   fImprimirEspecialAlGrabarComprobante = False
   If valImprimirEspecialAlGrabarComprobante Then
      Set insPagoProcesoDeImpresionEspecial = New clsProcesoDeImpresionEspecial
      If gProyParametrosCompania.GetImprimirComprobanteContableDePago Then
         If valTipoDeComprobanteAImprimir = CM_IMPRIME_COMPROBANTE_DE_PAGO And TypeOf RefPagoProcesoDeImpresionEspecial Is clsProcesoDeImpresionEspecial Then
            Set insPagoProcesoDeImpresionEspecial = RefPagoProcesoDeImpresionEspecial
            insPagoProcesoDeImpresionEspecial.sImprimirComprobanteDePago eQI_TODO
            fImprimirEspecialAlGrabarComprobante = True
         ElseIf valTipoDeComprobanteAImprimir = CM_IMPRIME_COMPROBANTE_DE_MOV_BANCARIO And TypeOf RefPagoProcesoDeImpresionEspecial Is clsProcesoDeImpresionEspecial Then
            Set insPagoProcesoDeImpresionEspecial = RefPagoProcesoDeImpresionEspecial
            gMessage.Advertencia "Prepare su impresora, se procederá a imprmir el cheque y sus comprobantes."
            insPagoProcesoDeImpresionEspecial.sImprimirComprobanteDeImpresiondeCheque eQI_TODO, valNumeroDeMovimientoBancario, reimprimir
            fImprimirEspecialAlGrabarComprobante = True
         ElseIf valTipoDeComprobanteAImprimir = CM_IMPRIME_COMPROBANTE_DE_PAGO_SUELDOS And TypeOf RefPagoProcesoDeImpresionEspecial Is clsProcesoDeImpresionEspecial Then
            Set insPagoProcesoDeImpresionEspecial = RefPagoProcesoDeImpresionEspecial
            RefPagoProcesoDeImpresionEspecial.sImprimirComprobanteDeImpresiondeChequeParaPagos
            fImprimirEspecialAlGrabarComprobante = True
         End If
      ElseIf gProyParametrosCompania.GetImprimirCompContDespuesDeChequeMovBancario Then
         If valTipoDeComprobanteAImprimir = CM_IMPRIME_COMPROBANTE_DE_MOV_BANCARIO And TypeOf RefPagoProcesoDeImpresionEspecial Is clsProcesoDeImpresionEspecial Then
            Set insPagoProcesoDeImpresionEspecial = RefPagoProcesoDeImpresionEspecial
            gMessage.Advertencia "Prepare su impresora, se procederá a imprmir el cheque y sus comprobantes."
            insPagoProcesoDeImpresionEspecial.sImprimirComprobanteDeImpresiondeCheque eQI_TODO, valNumeroDeMovimientoBancario, reimprimir
            fImprimirEspecialAlGrabarComprobante = True
         End If
      End If
      Set insPagoProcesoDeImpresionEspecial = Nothing
   End If
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: fImprimirEspecialAlGrabarComprobante = False
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fmImprimirEspecialAlGrabarComprobante", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fBuscaYEliminaCodigoAndGrupoAuxiliar(ByVal valCodigo As String, ByVal valTipoDeAuxiliarChar As String) As Boolean
   Dim vConexionAos As clsConexionesWcoAOS
   Dim varOtherUserUpdatedFirst As Boolean
   On Error GoTo h_ERROR
   Set vConexionAos = New clsConexionesWcoAOS
   fBuscaYEliminaCodigoAndGrupoAuxiliar = False
   If vConexionAos.fSelectAndSetValuesOfAuxiliarFromAOSBuscarPorCodigoYTipo(valCodigo, gEnumProyectoWincont.strGrupoAuxiliarToNum(valTipoDeAuxiliarChar, False)) Then
      If vConexionAos.fSelectAndSetValuesOfAuxiliarFromAOSExecuteAccionSobreRecordFromExternalModule(Eliminar, gEnumProyectoWincont.strGrupoAuxiliarToNum(valTipoDeAuxiliarChar, False), valCodigo, "", "", "", "") Then
         fBuscaYEliminaCodigoAndGrupoAuxiliar = True
      End If
   Else
      fBuscaYEliminaCodigoAndGrupoAuxiliar = True
   End If
   Set vConexionAos = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: fBuscaYEliminaCodigoAndGrupoAuxiliar = False
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fBuscaYEliminaCodigoAndGrupoAuxiliar", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fMostrarOpcionDeConversion() As Boolean
   fMostrarOpcionDeConversion = False
End Function

Public Sub sEjecutaConversionDeInfoCont()
'
End Sub

Public Sub sBorraLosDatosDeEstePeriodoDeLaTabla(ByVal valTableName As String, ByVal valFieldNameConsecutivoPeriodo As String, ByVal valConsecutivoPeriodo As Integer)
'
End Sub

Public Sub sActivaModuloDeActivosFijosCopiandoCatalogo()
'
End Sub

Public Sub sActivaModuloDeAuxiliaresCopiandoCatalogo()
'
End Sub

Public Sub sActualizacionDeNoDocumentoOrigenDeFacturaYCxC(ByVal valUsaConsecutivoPeriodo As Boolean, Optional ByVal valConsecutivoPeriodoNew As Long = -1)
   Dim Sql As String
   Dim sqlSetClause As String
   Dim sqlFromClause As String
   Dim sqlWhereClause As String
   Dim sqlComun As String
   Dim insFactura As clsFacturaNavigator
   Dim insCxC As clsCxCNavigator
   Dim insComprobante As clsComprobanteNavigator
   Dim insperiodo As clsPeriodoNavigator
   On Error GoTo h_ERROR
   Set insFactura = New clsFacturaNavigator
   insFactura.setClaseDeTrabajo eCTFC_Factura
   Set insCxC = New clsCxCNavigator
   Set insComprobante = New clsComprobanteNavigator
   Set insperiodo = New clsPeriodoNavigator
   sqlComun = gUtilSQL.CharConcat & gUtilSQL.fSimpleSqlValue(gTexto.fSeparadorStandardDeElementosString) & gUtilSQL.CharConcat & insComprobante.GetTableName & ".NoDocumentoOrigen"
   sqlSetClause = " SET " & insComprobante.GetTableName & ".NoDocumentoOrigen = " & insFactura.GetTableName & "." & insFactura.getFN_TIPO_DE_DOCUMENTO & sqlComun
   sqlFromClause = insFactura.GetTableName & " INNER JOIN  (" & insComprobante.GetTableName & " INNER JOIN " & insperiodo.GetTableName & " ON " & insComprobante.GetTableName & ".ConsecutivoPeriodo = " & insperiodo.GetTableName & ".ConsecutivoPeriodo) ON " & insperiodo.GetTableName & ".ConsecutivoCompania = " & insFactura.GetTableName & "." & insFactura.getFN_CONSECUTIVO_COMPANIA
   sqlWhereClause = " WHERE " & insComprobante.GetTableName & ".GeneradoPor = " & gUtilSQL.fSQLSimpleValueForEnum(enum_ComprobanteGeneradoPor.eCG_FACTURA) & " AND " & insComprobante.GetTableName & ".NoDocumentoOrigen = " & insFactura.GetTableName & "." & insFactura.getFN_NUMERO
   If valUsaConsecutivoPeriodo Then
      sqlWhereClause = sqlWhereClause & " AND " & insperiodo.GetTableName & ".ConsecutivoPeriodo = " & valConsecutivoPeriodoNew
   End If
   Sql = gUtilSQL.fSQLUpdateWithNJoin(insComprobante.GetTableName, sqlSetClause, sqlFromClause, sqlWhereClause)
   gDbUtil.Execute gDefDatabase.Conexion, Sql
   sqlSetClause = " SET " & insComprobante.GetTableName & ".NoDocumentoOrigen = " & insCxC.GetTableName & ".TipoCxC" & sqlComun
   sqlFromClause = insCxC.GetTableName & " INNER JOIN (" & insComprobante.GetTableName & " INNER JOIN " & insperiodo.GetTableName & " ON " & insComprobante.GetTableName & ".ConsecutivoPeriodo = " & insperiodo.GetTableName & ".ConsecutivoPeriodo) ON " & insperiodo.GetTableName & ".ConsecutivoCompania = " & insCxC.GetTableName & ".ConsecutivoCompania"
   sqlWhereClause = " WHERE " & insComprobante.GetTableName & ".GeneradoPor = " & gUtilSQL.fSQLSimpleValueForEnum(enum_ComprobanteGeneradoPor.eCG_CXC) & " AND " & insComprobante.GetTableName & ".NoDocumentoOrigen = " & insCxC.GetTableName & ".Numero"
   If valUsaConsecutivoPeriodo Then
      sqlWhereClause = sqlWhereClause & " AND " & insperiodo.GetTableName & ".ConsecutivoPeriodo = " & valConsecutivoPeriodoNew
   End If
   Sql = gUtilSQL.fSQLUpdateWithNJoin(insComprobante.GetTableName, sqlSetClause, sqlFromClause, sqlWhereClause)
   gDbUtil.Execute gDefDatabase.Conexion, Sql
   Set insFactura = Nothing
   Set insCxC = Nothing
   Set insComprobante = Nothing
   Set insperiodo = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sActualizacionDeNoDocumentoOrigenDeFacturaYCxC", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Function fConstruirSQLDelInformesDeDocumentosSinComprobante(ByVal valTipoDeDocumento As enum_ComprobanteGeneradoPor, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date) As String
   Dim Sql As String
   Dim insCxC As clsCxCNavigator
   Dim insCxP As clsCxPNavigator
   Dim insCobranza As clsCobranzaNavigator
   Dim insFactura As clsFacturaNavigator
   Dim insMovimientoBancario As clsMovimientoBancarioNavigator
   Dim insPago As clsPagoNavigator
   Dim insAnticipoSQL As clsAnticipoSQL
   Dim insAnticipoNav As clsAnticipoNavigator
   Dim insComprobanteNav As clsComprobanteNavigator
   Dim ConjuntoDeDocumentosSinContabilizar As String
   Dim NOHAYDOCUMENTOSCONTABILIZADOS As String
   Dim insPagoSQL As clsPagoSQL
   Dim insProveedor As clsProveedorNavigator
   On Error GoTo h_ERROR
   Set insComprobanteNav = New clsComprobanteNavigator
   Set insPagoSQL = New clsPagoSQL
   Set insProveedor = New clsProveedorNavigator
   NOHAYDOCUMENTOSCONTABILIZADOS = ""
   Select Case valTipoDeDocumento
      Case enum_ComprobanteGeneradoPor.eCG_CXC
         Set insCxC = New clsCxCNavigator
         Sql = insComprobanteNav.fConstruirSQLParaReportes(eCG_CXC, True)
         ConjuntoDeDocumentosSinContabilizar = gDbUtil.fBuildResultSetAsString(Sql)
         Sql = insCxC.fConstruirSQLDeCxCEntreFechas(valFechaInicial, valFechaFinal)
         If ConjuntoDeDocumentosSinContabilizar <> NOHAYDOCUMENTOSCONTABILIZADOS Then
            Sql = Sql & " AND " & insCxC.GetTableName & ".Origen = " & gUtilSQL.fSQLSimpleValueForEnum(enum_OrigenFacturacionOManual.eOF_MANUAL)
            Sql = Sql & " AND " & insCxC.getFN_NO_DOC_ORIGEN_CONTABILIZACION_PARA_SQL(True)
            Sql = Sql & " NOT IN (" & ConjuntoDeDocumentosSinContabilizar & ") "
         End If
         Sql = Sql & " ORDER BY " & insCxC.GetTableName & ".Fecha"
         Set insCxC = Nothing
      Case enum_ComprobanteGeneradoPor.eCG_CXP
         Set insCxP = New clsCxPNavigator
         Sql = insComprobanteNav.fConstruirSQLParaReportes(eCG_CXP, False)
         ConjuntoDeDocumentosSinContabilizar = gDbUtil.fBuildResultSetAsString(Sql)
         Sql = insCxP.fConstruirSQLDeCxPEntreFechas(valFechaInicial, valFechaFinal)
         If ConjuntoDeDocumentosSinContabilizar <> NOHAYDOCUMENTOSCONTABILIZADOS Then
            Sql = Sql & " AND " & insCxP.getFN_NO_DOC_ORIGEN_CONTABILIZACION_PARA_SQL(True)
            Sql = Sql & " NOT IN (" & ConjuntoDeDocumentosSinContabilizar & ") "
         End If
         Sql = Sql & " ORDER BY " & insCxP.GetTableName & ".Fecha"
         Set insCxP = Nothing
      Case enum_ComprobanteGeneradoPor.eCG_COBRANZA
         Set insCobranza = New clsCobranzaNavigator
         Sql = insComprobanteNav.fConstruirSQLParaReportes(eCG_COBRANZA, True)
         ConjuntoDeDocumentosSinContabilizar = gDbUtil.fBuildResultSetAsString(Sql)
         Sql = insCobranza.fConstruirSQLDeCobranzaEntreFechas(valFechaInicial, valFechaFinal)
         If ConjuntoDeDocumentosSinContabilizar <> NOHAYDOCUMENTOSCONTABILIZADOS Then
            Sql = Sql & " AND " & insCobranza.GetTableName & ".Numero"
            Sql = Sql & " NOT IN (" & ConjuntoDeDocumentosSinContabilizar & ") "
         End If
         Sql = Sql & " ORDER BY " & insCobranza.GetTableName & ".Fecha"
         Set insCobranza = Nothing
      Case enum_ComprobanteGeneradoPor.eCG_FACTURA
         Set insFactura = New clsFacturaNavigator
         insFactura.setClaseDeTrabajo eCTFC_Factura
         Sql = insComprobanteNav.fConstruirSQLParaReportes(eCG_FACTURA, True)
         ConjuntoDeDocumentosSinContabilizar = gDbUtil.fBuildResultSetAsString(Sql)
         Sql = insFactura.fConstruirSQLDeFacturaEntreFechas(valFechaInicial, valFechaFinal, False)
         If ConjuntoDeDocumentosSinContabilizar <> NOHAYDOCUMENTOSCONTABILIZADOS Then
            Sql = Sql & " AND " & insFactura.getFN_NO_DOC_ORIGEN_CONTABILIZACION_PARA_SQL(True)
            Sql = Sql & " NOT IN (" & ConjuntoDeDocumentosSinContabilizar & ") "
            Sql = Sql & " AND (" & insFactura.getFN_STATUS_FACTURA & " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA)
            Sql = Sql & " OR " & insFactura.getFN_STATUS_FACTURA & " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_EMITIDA) & ")"
            Sql = Sql & " AND " & insFactura.getFN_TIPO_DE_DOCUMENTO & " <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_RESUMENDIARIODEVENTAS)
         End If
         Sql = Sql & " ORDER BY " & insFactura.GetTableName & "." & insFactura.getFN_FECHA
         Set insFactura = Nothing
      Case enum_ComprobanteGeneradoPor.eCG_MOVIMIENTO_BANCARIO
         Set insMovimientoBancario = New clsMovimientoBancarioNavigator
         Sql = insComprobanteNav.fConstruirSQLParaReportes(eCG_MOVIMIENTO_BANCARIO, True)
         ConjuntoDeDocumentosSinContabilizar = gDbUtil.fBuildResultSetAsString(Sql, False)
         Sql = insMovimientoBancario.fConstruirSQLDeMovimientoBancarioEntreFechas(valFechaInicial, valFechaFinal)
         Sql = Sql & " AND " & insMovimientoBancario.GetTableName & ".GeneradoPor = " & gUtilSQL.fSQLSimpleValueForEnum(enum_GeneradoPor.eGP_USUARIO)
         If ConjuntoDeDocumentosSinContabilizar <> NOHAYDOCUMENTOSCONTABILIZADOS Then
            Sql = Sql & " AND " & insMovimientoBancario.GetTableName & ".ConsecutivoMovimiento"
            Sql = Sql & " NOT IN (" & ConjuntoDeDocumentosSinContabilizar & ") "
         End If
         Sql = Sql & " ORDER BY " & insMovimientoBancario.GetTableName & ".Fecha"
         Set insMovimientoBancario = Nothing
      Case enum_ComprobanteGeneradoPor.eCG_PAGOS
         Set insPago = New clsPagoNavigator
         Sql = insComprobanteNav.fConstruirSQLParaReportes(eCG_PAGOS, True)
         ConjuntoDeDocumentosSinContabilizar = gDbUtil.fBuildResultSetAsString(Sql, False)
         Sql = insPagoSQL.fConstruirSQLDePagoEntreFechas(valFechaInicial, valFechaFinal, insPago.GetTableName, insProveedor.GetTableName, gProyCompaniaActual.GetConsecutivoCompania)
         If ConjuntoDeDocumentosSinContabilizar <> NOHAYDOCUMENTOSCONTABILIZADOS Then
            Sql = Sql & " AND " & insPago.GetTableName & ".NumeroComprobante"
            Sql = Sql & " NOT IN (" & ConjuntoDeDocumentosSinContabilizar & ") "
         End If
         Sql = Sql & " ORDER BY " & insPago.GetTableName & ".Fecha"
         Set insPago = Nothing
      Case enum_ComprobanteGeneradoPor.eCG_RESUMEN_DIARIO_VENTAS
         Set insFactura = New clsFacturaNavigator
         insFactura.setClaseDeTrabajo eCTFC_Factura
         Sql = insComprobanteNav.fConstruirSQLParaReportes(eCG_RESUMEN_DIARIO_VENTAS, True)
         ConjuntoDeDocumentosSinContabilizar = gDbUtil.fBuildResultSetAsString(Sql)
         Sql = insFactura.fConstruirSQLDeFacturaEntreFechas(valFechaInicial, valFechaFinal, True)
         If ConjuntoDeDocumentosSinContabilizar <> NOHAYDOCUMENTOSCONTABILIZADOS Then
            Sql = Sql & " AND " & insFactura.getFN_NO_DOC_ORIGEN_CONTABILIZACION_PARA_SQL(True)
            Sql = Sql & " NOT IN (" & ConjuntoDeDocumentosSinContabilizar & ") "
            Sql = Sql & " AND " & insFactura.getFN_STATUS_FACTURA & " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_BORRADOR)
            Sql = Sql & " AND " & insFactura.getFN_TIPO_DE_DOCUMENTO & " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_RESUMENDIARIODEVENTAS)
         End If
         Sql = Sql & " ORDER BY " & insFactura.GetTableName & "." & insFactura.getFN_FECHA
         Set insFactura = Nothing
      Case enum_ComprobanteGeneradoPor.eCG_ANTICIPO
         Set insAnticipoSQL = New clsAnticipoSQL
         Set insAnticipoNav = New clsAnticipoNavigator
         Sql = insComprobanteNav.fConstruirSQLParaReportes(eCG_ANTICIPO, True)
         ConjuntoDeDocumentosSinContabilizar = gDbUtil.fBuildResultSetAsString(Sql, False)
         Sql = insAnticipoSQL.fConstruirSQLDeAnticiposEntreFechas(valFechaInicial, valFechaFinal, gProyCompaniaActual)
         If ConjuntoDeDocumentosSinContabilizar <> NOHAYDOCUMENTOSCONTABILIZADOS Then
            Sql = Sql & " AND " & insAnticipoNav.getFN_NO_DOC_ORIGEN_CONTABILIZACION_PARA_SQL(True)
            Sql = Sql & " NOT IN (" & ConjuntoDeDocumentosSinContabilizar & ")"
         End If
         Sql = Sql & " ORDER BY " & insAnticipoNav.GetTableName & ".Fecha "
         Set insAnticipoSQL = Nothing
         Set insAnticipoNav = Nothing
   End Select
   Set insPagoSQL = Nothing
   Set insComprobanteNav = Nothing
   fConstruirSQLDelInformesDeDocumentosSinComprobante = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: fConstruirSQLDelInformesDeDocumentosSinComprobante = ""
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConstruirSQLDelInformesDeDocumentosSinComprobante", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Sub sInsertarLosRecordsDeTipoDeComprobante(ByVal valTableName As String, ByVal valFN_NombreDelTipo As String, ByVal valFN_CodigoDelTipo As String)
   Dim varEnum As enum_TipoDeComprobante
   Dim txtTipodeComprobante As String
   Dim CodigoTipoDeComprobante As String
   Dim pos As Integer
   Dim Sql As String
   On Error GoTo 0
   For varEnum = eTDC_CHEQUES To eTDC_ANTICIPO
      txtTipodeComprobante = gEnumProyectoWincont.enumTipoDeComprobanteToString(varEnum)
      pos = InStr(txtTipodeComprobante, " - ")
      CodigoTipoDeComprobante = Trim(gTexto.DfMid(txtTipodeComprobante, 1, pos - 1))
      txtTipodeComprobante = gTexto.DfMid(txtTipodeComprobante, pos + 3)
      txtTipodeComprobante = gUtilSQL.fSimpleSqlValue(txtTipodeComprobante)
      CodigoTipoDeComprobante = gUtilSQL.fSimpleSqlValue(CodigoTipoDeComprobante)
      Sql = "INSERT INTO " & valTableName & "(" & valFN_NombreDelTipo & ", " & valFN_CodigoDelTipo
      Sql = Sql & " ) VALUES (" & txtTipodeComprobante & ", " & CodigoTipoDeComprobante & ")"
      gDbUtil.Execute gDefDatabase.Conexion, Sql
   Next
End Sub

Public Function getCampoTipodePlanillaISLR() As String
   Dim insCompania As clsCompaniaNavigator
   On Error GoTo h_ERROR
   Set insCompania = New clsCompaniaNavigator
   getCampoTipodePlanillaISLR = "PersonaFiscal"
   Set insCompania = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "getCampoTipodePlanillaISLR", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fEjecutaProcesoPostEliminacion(ByVal valNumeroDocOrigen As String, ByVal valGeneradoPor As enum_ComprobanteGeneradoPor) As Boolean
   Dim vResultado As Boolean
   On Error GoTo h_ERROR
   Select Case valGeneradoPor
      Case eCG_CXC: vResultado = True
      Case eCG_FACTURA: vResultado = True
      Case eCG_CXP: vResultado = fEjecutaProcesoPostEliminacionCxP(valNumeroDocOrigen)
      Case eCG_COBRANZA: vResultado = fEjecutaProcesoPostEliminacionCobranza(valNumeroDocOrigen)
      Case eCG_PAGOS: vResultado = fEjecutaProcesoPostEliminacionPago(valNumeroDocOrigen)
      Case eCG_MOVIMIENTO_BANCARIO: vResultado = True
      Case eCG_RESUMEN_DIARIO_VENTAS: vResultado = True
      Case eCG_ANTICIPO: vResultado = True
   End Select
   fEjecutaProcesoPostEliminacion = vResultado
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fEjecutaProcesoPostEliminacion", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fEjecutaProcesoPostEliminacionCxP(ByVal valNumeroDocOrigen As String) As Boolean
   Dim vResultado As Boolean
   Dim insCxP As clsCxPNavigator
   Dim vCodigoProveedor As String
   Dim vNumeroCxP As String
   Dim Sql As String
   On Error GoTo h_ERROR
   vResultado = False
   If gProyCompaniaActual.fPuedoUsarOpcionesDeContribuyenteEspecial Then
      Set insCxP = New clsCxPNavigator
      vCodigoProveedor = gTexto.DfMid(valNumeroDocOrigen, 1, gTexto.DfInStr(valNumeroDocOrigen, gTexto.fSeparadorStandardDeElementosString) - 1)
      vNumeroCxP = gTexto.DfMid(valNumeroDocOrigen, gTexto.DfInStr(valNumeroDocOrigen, gTexto.fSeparadorStandardDeElementosString) + 1)
      Sql = "UPDATE " & insCxP.GetTableName
      Sql = Sql & " SET SeContabilRetIva = " & gUtilSQL.fBooleanToSqlValue(False)
      Sql = Sql & " WHERE ConsecutivoCompania = " & gProyCompaniaActual.GetConsecutivoCompania
      Sql = Sql & " AND CodigoProveedor = " & gUtilSQL.fSimpleSqlValue(vCodigoProveedor)
      Sql = Sql & " AND Numero = " & gUtilSQL.fSimpleSqlValue(vNumeroCxP)
      Sql = Sql & " AND DondeContabilRetIVA = " & gUtilSQL.fSQLSimpleValueForEnum(enum_DondeEfectuoContabilizacionRetIVA.eDC_CxP)
      Sql = Sql & " AND MontoRetenido <> 0"
      gMessage.ProgrammerMessage (gDbUtil.fExecuteAndReturnTotalOfRecordsAffected(gDefDatabase.Conexion, Sql))
      Set insCxP = Nothing
      vResultado = True
   Else
      vResultado = True
   End If
   fEjecutaProcesoPostEliminacionCxP = vResultado
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fEjecutaProcesoPostEliminacionCxP", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fEjecutaProcesoPostEliminacionPago(ByVal valNumeroDocOrigen As String) As Boolean
   Dim vResultado As Boolean
   Dim Sql As String
   Dim sqlTableToUpdate As String
   Dim sqlSetClause As String
   Dim sqlFROM As String
   Dim sqlWhere As String
   Dim insCxP As clsCxPNavigator
   Dim insPago As clsPagoNavigator
   Dim insDocPagado As clsDocumentoPagadoNavigator
   On Error GoTo h_ERROR
   vResultado = False
   If gProyCompaniaActual.fPuedoUsarOpcionesDeContribuyenteEspecial Then
      Set insCxP = New clsCxPNavigator
      Set insPago = New clsPagoNavigator
      Set insDocPagado = New clsDocumentoPagadoNavigator
      sqlTableToUpdate = insCxP.GetTableName
      sqlSetClause = " SET " & insCxP.GetTableName & ".SeContabilRetIva = " & gUtilSQL.fBooleanToSqlValue(False)
      sqlFROM = insCxP.GetTableName & " INNER JOIN (" & insPago.GetTableName & " INNER JOIN " & insDocPagado.GetTableName
      sqlFROM = sqlFROM & " ON (" & insPago.GetTableName & ".NumeroComprobante = " & insDocPagado.GetTableName & ".NumeroComprobante"
      sqlFROM = sqlFROM & ") AND (" & insPago.GetTableName & ".ConsecutivoCompania = " & insDocPagado.GetTableName & ".ConsecutivoCompania"
      sqlFROM = sqlFROM & ")) ON (" & insDocPagado.GetTableName & ".NumeroDelDocumentoPagado = " & insCxP.GetTableName & ".Numero"
      sqlFROM = sqlFROM & ") AND (" & insDocPagado.GetTableName & ".ConsecutivoCompania = " & insCxP.GetTableName & ".ConsecutivoCompania"
      sqlFROM = sqlFROM & ") AND (" & insCxP.GetTableName & ".CodigoProveedor = " & insPago.GetTableName & ".CodigoProveedor)"
      sqlWhere = " WHERE " & insCxP.GetTableName & ".ConsecutivoCompania = " & gProyCompaniaActual.GetConsecutivoCompania
      sqlWhere = sqlWhere & " AND " & insPago.GetTableName & ".NumeroComprobante = " & gConvert.fConvertStringToLong(valNumeroDocOrigen)
      sqlWhere = sqlWhere & " AND " & insCxP.GetTableName & ".MontoRetenido <> 0"
      sqlWhere = sqlWhere & " AND " & insCxP.GetTableName & ".DondeContabilRetIVA = " & gUtilSQL.fSQLSimpleValueForEnum(enum_DondeEfectuoContabilizacionRetIVA.eDC_Pago)
      sqlWhere = sqlWhere & " AND " & insDocPagado.GetTableName & ".MontoIvaRetenido <> 0"
      Set insCxP = Nothing
      Set insPago = Nothing
      Set insDocPagado = Nothing
      Sql = gUtilSQL.fSQLUpdateWithNJoin(sqlTableToUpdate, sqlSetClause, sqlFROM, sqlWhere)
      gDbUtil.Execute gDefDatabase.Conexion, Sql
      vResultado = True
   Else
      vResultado = True
   End If
   fEjecutaProcesoPostEliminacionPago = vResultado
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fEjecutaProcesoPostEliminacionPago", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSearchExisteConciliacionAnterioraFecha(ByVal valCodigoCuenta As String, ByVal valFechaHasta As Date) As Boolean
   fSearchExisteConciliacionAnterioraFecha = False
End Function

Public Function fLaCompaniaActualNoTienePeriodos() As Boolean
   fLaCompaniaActualNoTienePeriodos = False
End Function

Public Sub sEliminaReglasdeContabilizacionCiaActual(ByVal valConcecutivoCompania As Long)
'
End Sub

Public Sub sLimpiaReglasDeContabilizacionClientesYProveedoresCiaActual()
'
End Sub

Public Sub sGenerarAuxiliaresParaProveedoresYClientes(ByVal mModuloAImportar As enum_EntidadAExportarOImportar)
   On Error GoTo h_ERROR
   If gProyCompaniaActual.GetTieneAccesoACaracteristicaContabilidadActiva And gProyCompaniaActual.GetUsaAuxiliares Then
      If gMessage.Confirm("¿Desea generar los auxiliares para los " & gEnumProyecto.enumEntidadAExportarOImportarToString(mModuloAImportar) & " importados?") Then
         Select Case mModuloAImportar
            Case eEAEI_PROVEEDOR: sEjecutaAuxiliarGenerar enum_GrupoAuxiliar.eGA_PROVEEDORES
            Case eEAEI_CLIENTE: sEjecutaAuxiliarGenerar enum_GrupoAuxiliar.eGA_CLIENTES
         End Select
      End If
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sGenerarAuxiliaresParaProveedoresYClientes", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Function fSePuedeCrearUKEnComprobante() As Boolean
   Dim insComprobante As clsComprobanteNavigator
   On Error GoTo h_ERROR
   Set insComprobante = New clsComprobanteNavigator
   fSePuedeCrearUKEnComprobante = True
   If insComprobante.fExistenComprobanteConNumerDocOrigenRepetido(True) Then
      gMessage.Advertencia "Existen comprobantes con Número de documento Origen repetido, lo cual indica inconsistencia en su data, Comuníquese con el personal de Soporte de " & gDefgen.fNombreInformaticaGalac
      fSePuedeCrearUKEnComprobante = False
   End If
   Set insComprobante = Nothing
h_EXIT:
   On Error GoTo 0
   Exit Function
h_ERROR:
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSePuedeCrearUKEnComprobante", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fNoConsecutivoDocOrigenCompraVenta()
   fNoConsecutivoDocOrigenCompraVenta = 0
End Function

Public Function fCambiaGeneradoPorEnIntegracion(ByRef refGeneradoPor As enum_ComprobanteGeneradoPor) As Boolean
   fCambiaGeneradoPorEnIntegracion = False
End Function

Public Sub sEliminaRecordCuentasFlujoEfectivo(ByVal valConsecutivoCompania As String)
'
End Sub

Public Sub sInsertaElRecordDeCuentasFlujoEfectivo()
'
End Sub

Public Sub sEjecutaDefinirCuentasDeFlujoEfectivo()
'
End Sub

Public Function fExisteCuentaEnDefinicionFlujoEfectivo(ByVal valConsecutivoCompania As String, ByVal valCodigoCuenta As String) As Boolean
   fExisteCuentaEnDefinicionFlujoEfectivo = False
End Function

Private Function fEsComprobanteGeneradoPorInventario(ByVal valGeneradoPorChr As String) As Boolean
   On Error GoTo h_ERROR
   fEsComprobanteGeneradoPorInventario = False
   If (gConvert.enumerativoAChar(enum_ComprobanteGeneradoPor.eCG_INVENTARIO) = valGeneradoPorChr) Then
       fEsComprobanteGeneradoPorInventario = True
       gMessage.Advertencia "Es un comprobante de costo de venta por lo que no es permitida esta operación, Gracias. "
   End If
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fEsComprobanteGeneradoPorInventario", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fMuestraEstaOpcion() As Boolean
   Dim vResult As Boolean
   On Error GoTo h_ERROR
   vResult = True
   If (gDefProg.GetSiglasDelPrograma = GetSiglasSAW()) Then
      vResult = (gProyParametrosCompania.GetMetodoDeCosteoAsEnum = eTDMDC_UltimoCosto)
   End If
   fMuestraEstaOpcion = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, " fMuestraEstaOpcion", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fExisteEnDefFlujoEfectivo(ByVal valCodigoAEliminar As String) As Boolean
   fExisteEnDefFlujoEfectivo = False
End Function

Public Sub sEjecutaInformeTotalesCentroDeCostoEntreFechas(pgbProcess As ProgressBar, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal mDondeImprimir As enum_DondeImprimir, ByVal valIncluirSaldosPeriodosAntCuentasGanancias As Boolean, Optional ByVal valEsInformeConCodigoDeMoneda As Boolean = False, Optional ByVal valCodigoMoneda As String, Optional ByVal valNombreMoneda As String)
   Dim SqlDelReporte As String
   Dim fechaInicial As Date
   Dim FechaFinal As Date
   Dim esReportePorCentroDeCostos As Boolean
   Dim mTableNameMovCC As String
   Dim insCalculaSaldos As clsCalculaTotales
   Dim varReporte As DDActiveReports2.ActiveReport
   Dim insConfigurar As clsRptCentroDeCtosConfigurar
   Dim insLib As clsLibWincont
   On Error GoTo h_ERROR
   Set varReporte = New ActiveReport
   Set insConfigurar = New clsRptCentroDeCtosConfigurar
   Set insCalculaSaldos = New clsCalculaTotales
   Set insLib = New clsLibWincont
   mTableNameMovCC = gContPeriodoActual.GenerarNombreParaTablasDeInformes("MovCentCostos")
   If valFechaInicial > gContPeriodoActual.GetFechaAperturaDelPeriodo Then
      FechaFinal = gUtilDate.SumaNdias(valFechaInicial, -1)
      fechaInicial = gContPeriodoActual.GetFechaAperturaDelPeriodo
   Else
      fechaInicial = gUtilDate.SumaNdias(valFechaInicial, -1)
      FechaFinal = fechaInicial
   End If
   insCalculaSaldos.ConstruirTablaTemporalConSaldosYAuxiliares pgbProcess, mTableNameMovCC, valFechaInicial, valFechaFinal, fechaInicial, FechaFinal, "", "", "", , True, True, valIncluirSaldosPeriodosAntCuentasGanancias, True, False, False, False, valEsInformeConCodigoDeMoneda, valCodigoMoneda
   insCalculaSaldos.EliminarLasCuentasTipoTituloDeLaTablaTemporal mTableNameMovCC
   SqlDelReporte = fSQLMovimientosPorCentroDeCostos(mTableNameMovCC, valFechaInicial, valFechaFinal, valEsInformeConCodigoDeMoneda, valCodigoMoneda)
   If insConfigurar.fConfigurarDatosDelReporteInformeTotalesCentroDeCostoEntreFechas(varReporte, SqlDelReporte, valFechaInicial, valFechaFinal, valIncluirSaldosPeriodosAntCuentasGanancias, gPrinterSettings.GetPrintPageNumber, gPrinterSettings.GetPrintEmitDate, gProyParametrosWinCont, gProyCompaniaActual, valNombreMoneda, Not valEsInformeConCodigoDeMoneda, insLib) Then
      gUtilReports.sMostrarOImprimirReporte varReporte, 1, mDondeImprimir, "Informe Totales Por Centro De Costo Entre Fechas"
   End If
   Set insCalculaSaldos = Nothing
   Set varReporte = Nothing
   Set insConfigurar = Nothing
   Set insLib = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sEjecutaInformeTotalesCentroDeCostoEntreFechas", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Private Function fSQLMovimientosPorCentroDeCostos(ByVal identificadorDatos As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, Optional ByVal valEsInformeConCodigoDeMoneda As Boolean = False, Optional ByVal valCodigoMoneda As String) As String
   Dim Sql As String
   Dim varNombreTablaAsiento As String
   Dim CteMovimientosDolar As String
   Dim NombreTablaCambio As String
   Dim NombreCampoTablaCambio As String
   On Error GoTo h_ERROR
   fSQLMovimientosPorCentroDeCostos = ""
   If (gDefProg.GetSiglasDelPrograma = GetSiglasIVA Or gDefProg.GetSiglasDelPrograma = GetSiglasWinCont Or gDefProg.GetSiglasDelPrograma = GetSiglasSAW) Then
      NombreCampoTablaCambio = "CambioAMonedaLocal"
      NombreTablaCambio = "Comun.Cambio"
   Else
      NombreTablaCambio = "dbo.Cambio"
      NombreCampoTablaCambio = "cambioABolivares"
   End If
   CteMovimientosDolar = ""
   If valEsInformeConCodigoDeMoneda Then
      varNombreTablaAsiento = "MovimientosDolar_CTE"
      CteMovimientosDolar = " set dateformat dmy; WITH MovimientosDolar_CTE (ConsecutivoPeriodo, Codigo, Descripcion, CodigoCuenta, Fecha, MontoDebe, MontoHaber, Contabilizado) AS "
      CteMovimientosDolar = CteMovimientosDolar & " (SELECT ConsecutivoPeriodo, Codigo, Descripcion, CodigoCuenta, Fecha, ROUND(MontoDebe / " & NombreCampoTablaCambio & ", 2), ROUND(MontoHaber / " & NombreCampoTablaCambio & ", 2), Contabilizado"
      CteMovimientosDolar = CteMovimientosDolar & " FROM IGv_ListadoCentroDeCostos INNER JOIN " & NombreTablaCambio & " ON (" & NombreTablaCambio & ".FechaDeVigencia = IGv_ListadoCentroDeCostos.Fecha And " & NombreTablaCambio & ".CodigoMoneda = '" & valCodigoMoneda & "') "
      CteMovimientosDolar = CteMovimientosDolar & " WHERE IGv_ListadoCentroDeCostos.ConsecutivoPeriodo = " & gContPeriodoActual.GetConsecutivoPeriodo & ")"
   Else
      varNombreTablaAsiento = "IGv_ListadoCentroDeCostos"
   End If
   Sql = CteMovimientosDolar & " SELECT "
   Sql = Sql & "tblSaldoContableTemp.Codigo"
   Sql = Sql & gUtilSQL.CharConcat & gUtilSQL.fSimpleSqlValue("  -  ") & gUtilSQL.CharConcat
   Sql = Sql & "tblSaldoContableTemp.Descripcion as Descripcion,"
   Sql = Sql & "MAX(tblSaldoContableTemp.Saldo) AS SaldoInicial, "
   Sql = Sql & gUtilSQL.fIfResultIsNullReplace("SUM(" & varNombreTablaAsiento & ".MontoDebe)", 0) & "AS MontoDebe, "
   Sql = Sql & gUtilSQL.fIfResultIsNullReplace("SUM(" & varNombreTablaAsiento & ".MontoHaber)", 0) & "AS MontoHaber "
   Sql = Sql & "FROM "
   Sql = Sql & "tblSaldoContableTemp "
   Sql = Sql & "LEFT JOIN "
   Sql = Sql & varNombreTablaAsiento & "  ON tblSaldoContableTemp.Codigo = " & varNombreTablaAsiento & ".Codigo "
   Sql = Sql & " AND " & varNombreTablaAsiento & ".ConsecutivoPeriodo  = " & gContPeriodoActual.GetConsecutivoPeriodo
   Sql = Sql & " AND " & varNombreTablaAsiento & ".Fecha BETWEEN " & gUtilSQL.fDateToSQLValue(valFechaInicial) & " AND " & gUtilSQL.fDateToSQLValue(valFechaFinal) & ""
   Sql = gUtilSQL.fSQLValueWithAnd(Sql, "Contabilizado", "S", False)
   Sql = Sql & "WHERE "
   Sql = Sql & "tblSaldoContableTemp.ID= " & gUtilSQL.fSimpleSqlValue(identificadorDatos)
   Sql = Sql & "GROUP BY "
   Sql = Sql & "tblSaldoContableTemp.Codigo, "
   Sql = Sql & "tblSaldoContableTemp.Descripcion "
   Sql = Sql & "ORDER BY "
   Sql = Sql & gUtilSQL.fJustificaADerechaElString("tblSaldoContableTemp", "Codigo", 30)
   fSQLMovimientosPorCentroDeCostos = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLMovimientosPorCentroDeCostos", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Sub sEjecutaElReporteBalanceComprobacionXCentroCosto(ByVal pgbProcess As ProgressBar, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal mDondeImprimir As enum_DondeImprimir, ByVal valCentrodeCosto As String, ByVal varNivelDeDetalle As Integer, ByVal valCedulaContador As String, ByVal valRifRepLegal As String, ByVal valsaltarPaginas As Boolean, ByRef refIncluirSaldosPeriodosAntCuentasGanancias As Boolean, ByRef refIncluirSaldoInicial As Boolean, ByRef refIncluirSaldoFinal As Boolean, ByRef refIncluirCodigoDeCuenta As Boolean, ByRef refIncluirCuentasSinMovConSaldoInicial As Boolean, ByRef refIdentificadorDatos As String, Optional ByVal valEsInformeConCodigoDeMoneda As Boolean = False, Optional ByVal valCodigoMoneda As String, Optional ByVal valNombreMoneda As String)
   Dim SqlDelReporte As String
   Dim SqlCentroDeCosto As String
   Dim maximoNivelDeDetalle As Integer
   Dim incluirSaldoInicial As Boolean
   Dim incluirSaldoFinal As Boolean
   Dim incluirCodigoDeCuenta As Boolean
   Dim varPuedoContinuar As Boolean
   Dim incluirCuentasSinMovConSaldoInicial As Boolean
   Dim varReporte As DDActiveReports2.ActiveReport
   Dim insConfigurar As clsRptCentroDeCtosConfigurar
   Dim insNoComunWincontEcVzla As clsNoComunWincontEcVzla
   Dim insLib As clsLibWincont
   On Error GoTo h_ERROR
   Set varReporte = New ActiveReport
   Set insConfigurar = New clsRptCentroDeCtosConfigurar
   Set insNoComunWincontEcVzla = New clsNoComunWincontEcVzla
   Set insLib = New clsLibWincont
   varPuedoContinuar = True
   If varPuedoContinuar Then
      sConstruyeTablaDeBalCompCCYArmaSQLDelInforme pgbProcess, valFechaInicial, valFechaFinal, valCentrodeCosto, varNivelDeDetalle, SqlDelReporte, SqlCentroDeCosto, refIncluirSaldoInicial, refIncluirSaldoFinal, refIncluirCodigoDeCuenta, refIncluirCuentasSinMovConSaldoInicial, maximoNivelDeDetalle, refIncluirSaldosPeriodosAntCuentasGanancias, refIdentificadorDatos, valEsInformeConCodigoDeMoneda, valCodigoMoneda
      If insConfigurar.fConfigurarDatosDelReporteBalanceDeComprobacionCC(varReporte, SqlDelReporte, refIncluirSaldoInicial, refIncluirSaldoFinal, refIncluirCodigoDeCuenta, gConvert.fechaAString(valFechaInicial), gConvert.fechaAString(valFechaFinal), maximoNivelDeDetalle, valCedulaContador, valRifRepLegal, valsaltarPaginas, insNoComunWincontEcVzla.fUsaDatosParaLaFirmaEnInformes, _
               gEnumProyectoWincont, gMonedaLocalActual, gContPeriodoActual, gContNiveles, gProyParametrosWinCont, gProyCompaniaActual, gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision, valNombreMoneda, Not valEsInformeConCodigoDeMoneda, insLib) Then
               gUtilReports.sMostrarOImprimirReporte varReporte, 1, mDondeImprimir, gProyParametrosWinCont.fEnumTipoDeReporteToString(eRPT_BalanceDeComprobacionXCentroDeCostos)
      End If
      gAPI.SetMouseNormal Me
      pgbProcess.Visible = False
   End If
   Set insLib = Nothing
   Set varReporte = Nothing
   Set insConfigurar = Nothing
   Set insNoComunWincontEcVzla = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sEjecutaElReporteBalanceComprobacionXCentroCosto", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sConstruyeTablaDeBalCompCCYArmaSQLDelInforme(ByVal pgbProcess As ProgressBar, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valCentrodeCosto As String, ByVal varNivelDeDetalle As Integer, ByRef refSQLDelReporte As String, ByRef refSqlCentroDeCostos As String, ByRef varIncluirSaldoInicial As Boolean, ByRef varIncluirSaldoFinal As Boolean, ByRef varIncluirCodigoDeCuenta As Boolean, ByRef refIncluirCuentasSinMovConSaldoInicial As Boolean, ByRef varMaximoNivelDeDetalle As Integer, ByRef refIncluirSaldosPeriodosAntCuentasGanancias As Boolean, ByRef mTableNameBalComproCC As String, Optional ByVal valEsInformeConCodigoDeMoneda As Boolean = False, Optional ByVal valCodigoMoneda As String)
   Dim borrarCuentasConSaldoCero As Boolean
   Dim fechaInicial As Date
   Dim FechaFinal As Date
   Dim esReportePorCentroDeCostos As Boolean
   Dim maximaLongitudDelCodigo As Integer
   Dim mCalculaSaldos As clsCalculaTotales
   On Error GoTo h_ERROR
   Set mCalculaSaldos = New clsCalculaTotales
   gAPI.SetMouseClock Me
   mTableNameBalComproCC = gContPeriodoActual.GenerarNombreParaTablasDeInformes("BalComCC")
   borrarCuentasConSaldoCero = True
   esReportePorCentroDeCostos = True
   If valFechaInicial > gContPeriodoActual.GetFechaAperturaDelPeriodo Then
      FechaFinal = valFechaInicial - 1
      fechaInicial = gContPeriodoActual.GetFechaAperturaDelPeriodo
   Else
      fechaInicial = valFechaInicial - 1
      FechaFinal = fechaInicial
   End If
   refIncluirSaldosPeriodosAntCuentasGanancias = refIncluirSaldosPeriodosAntCuentasGanancias
   mCalculaSaldos.ConstruirTablaTemporalConSaldosYAuxiliares pgbProcess, mTableNameBalComproCC, valFechaInicial, valFechaFinal, fechaInicial, FechaFinal, "", "", valCentrodeCosto, "", esReportePorCentroDeCostos, True, refIncluirSaldosPeriodosAntCuentasGanancias, False, True, False, False, valEsInformeConCodigoDeMoneda, valCodigoMoneda
   mCalculaSaldos.AgregaYLlenaLaColumnaGrupoDeCuentaALaQuePertenece mTableNameBalComproCC, eGC_Todas
   CompletarTablaDeSaldoyAuxiliares mTableNameBalComproCC, valFechaInicial, valFechaFinal, valEsInformeConCodigoDeMoneda, valCodigoMoneda
   mCalculaSaldos.ConstruirEstadoFinancieroEnMemoriaPorCentroDeCosto pgbProcess, mTableNameBalComproCC, Not borrarCuentasConSaldoCero
   mCalculaSaldos.RecalculaSaldosDelEstadoFinanciero mTableNameBalComproCC
   sCalculaResultadoDelEjercicio valEsInformeConCodigoDeMoneda, mCalculaSaldos, mTableNameBalComproCC
   mCalculaSaldos.FiltrarRenglonesAImprimirSegunMaximaLongitud mTableNameBalComproCC, maximaLongitudDelCodigo, varMaximoNivelDeDetalle, varNivelDeDetalle
   mCalculaSaldos.AgregarRenglonesDeTotalesPorNivelFicticios mTableNameBalComproCC, maximaLongitudDelCodigo, varMaximoNivelDeDetalle, , True
   EliminarCuentasTituloNoValidas mTableNameBalComproCC
   refSqlCentroDeCostos = fSQLCentroDeCostosEqualOrLike(valCentrodeCosto)
   refSQLDelReporte = fSQLBalancePorCentroDeCostos(mTableNameBalComproCC, refSqlCentroDeCostos, valFechaInicial, valFechaFinal, refIncluirCuentasSinMovConSaldoInicial)
   gAPI.sAddProgress pgbProcess
   varIncluirSaldoInicial = varIncluirSaldoInicial
   varIncluirSaldoFinal = varIncluirSaldoFinal
   varIncluirCodigoDeCuenta = varIncluirCodigoDeCuenta
   gAPI.SetMouseNormal Me
   pgbProcess.Visible = False
   Set mCalculaSaldos = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sConstruyeTablaDeBalCompCCYArmaSQLDelInforme", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Function fSQLCentroDeCostosEqualOrLike(ByVal valCodigoCentroDeCostos As String) As String
   Dim sqlCentroDeCostos  As String
   On Error GoTo h_ERROR
   fSQLCentroDeCostosEqualOrLike = ""
   If valCodigoCentroDeCostos = "" Then
      sqlCentroDeCostos = " AND tblSaldoContableTemp.CodeAux <> ''"
   Else
      sqlCentroDeCostos = " AND " & gUtilSQL.fSQLValue("tblSaldoContableTemp.CodeAux", valCodigoCentroDeCostos, False)
   End If
   fSQLCentroDeCostosEqualOrLike = sqlCentroDeCostos
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLCentroDeCostosEqualOrLike", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLBalancePorCentroDeCostos(ByVal valTableName As String, ByVal valSqlCentroDeCostos As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal incluirCuentasSinMovimiento As Boolean) As String
   Dim Sql As String
   Dim ConjuntoSQL As String
   On Error GoTo h_ERROR
   ConjuntoSQL = "SELECT tblSaldoContableTemp.Codigo FROM tblSaldoContableTemp WHERE tblSaldoContableTemp.TieneSubCuentas = " & gUtilSQL.fBooleanToStrSQL(False)
   ConjuntoSQL = ConjuntoSQL & " AND tblSaldoContableTemp.ID = " & gUtilSQL.fSimpleSqlValue(valTableName)
   ConjuntoSQL = ConjuntoSQL & " AND (tblSaldoContableTemp.SaldoInicial = 0 "
   ConjuntoSQL = ConjuntoSQL & " AND tblSaldoContableTemp.SubTotalDebe = 0 "
   ConjuntoSQL = ConjuntoSQL & " AND tblSaldoContableTemp.SubTotalHaber = 0)"
   Sql = "SELECT CentroDeCostos.Codigo,"
   Sql = Sql & "': ' + CentroDeCostos.Descripcion AS DescripcionCC, "
   Sql = Sql & "tblSaldoContableTemp.Codigo AS CodigoCuenta, "
   Sql = Sql & "tblSaldoContableTemp.Descripcion, "
   Sql = Sql & "tblSaldoContableTemp.TieneSubCuentas AS TieneSubCuentas, "
   Sql = Sql & "SUM(tblSaldoContableTemp.SaldoInicial) AS SaldoInicial, "
   Sql = Sql & "SUM(tblSaldoContableTemp.SubTotalDebe) AS SaldoDebe, "
   Sql = Sql & "SUM(tblSaldoContableTemp.SubTotalHaber) AS SaldoHaber, "
   Sql = Sql & "SUM(tblSaldoContableTemp.Saldo) AS SaldoActual, "
   Sql = Sql & "MIN(tblSaldoContableTemp.CodeAux) "
   Sql = Sql & " FROM tblSaldoContableTemp INNER JOIN CentroDeCostos "
   Sql = Sql & " ON CentroDeCostos.Codigo = tblSaldoContableTemp.CodeAux "
   Sql = Sql & " AND CentroDeCostos.ConsecutivoPeriodo = " & gContPeriodoActual.GetConsecutivoPeriodo
   Sql = Sql & " WHERE tblSaldoContableTemp.ID = " & gUtilSQL.fSimpleSqlValue(valTableName)
   Sql = Sql & " AND Grupo >=  " & gUtilSQL.fSimpleSqlValue("A")
   Sql = Sql & valSqlCentroDeCostos
   If Not incluirCuentasSinMovimiento Then
      Sql = Sql & " AND (tblSaldoContableTemp.Codigo NOT IN (" & ConjuntoSQL & "))"
   End If
   Sql = Sql & " GROUP BY CentroDeCostos.Codigo, CentroDeCostos.Descripcion, tblSaldoContableTemp.Codigo, tblSaldoContableTemp.Descripcion,tblSaldoContableTemp.TieneSubCuentas"
   Sql = Sql & " ORDER BY CentroDeCostos.Codigo, CentroDeCostos.Descripcion, tblSaldoContableTemp.Codigo, tblSaldoContableTemp.Descripcion,tblSaldoContableTemp.TieneSubCuentas"
   fSQLBalancePorCentroDeCostos = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLBalancePorCentroDeCostos", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Sub EliminarCuentasTituloNoValidas(ByVal identificadorDatos As String)
   Dim Sql As String
   Sql = "DELETE  FROM tblSaldoContableTemp WHERE (tblSaldoContableTemp.TieneSubCuentas = " & gUtilSQL.fSimpleSqlValue("S")
   Sql = Sql & " OR tblSaldoContableTemp.TieneSubCuentas = " & gUtilSQL.fSimpleSqlValue("T") & ")"
   Sql = Sql & " AND tblSaldoContableTemp.SaldoInicial = 0"
   Sql = Sql & " AND tblSaldoContableTemp.SubTotalDebe = 0"
   Sql = Sql & " AND tblSaldoContableTemp.SubTotalHaber = 0"
   Sql = Sql & " AND tblSaldoContableTemp.Saldo = 0"
   Sql = Sql & " AND tblSaldoContableTemp.ID = " & gUtilSQL.fSimpleSqlValue(identificadorDatos)
   On Error Resume Next
   gDbUtil.Execute Conexion, Sql
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "EliminarCuentasTituloNoValidas", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Sub CompletarTablaDeSaldoyAuxiliares(ByVal valTableName As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, Optional ByVal valEsInformeConCodigoDeMoneda As Boolean = False, Optional ByVal valCodigoMoneda As String)
   Dim Sql As String
   Dim vMontoDebe As String
   Dim vMontoHaber As String
   Dim vSaldo As String
   Dim insWcoViews As clsWcoViews
   Dim vRecordset As ADODB.Recordset
   Dim vUpdateSQL As String
   Dim insCalculaTotales As clsCalculaTotales
   Dim varNombreTablaAsiento As String
   Dim CteMovimientosDolar As String
   Dim NombreTablaCambio As String
   Dim NombreCampoTablaCambio As String
   On Error GoTo h_ERROR
   Set vRecordset = New ADODB.Recordset
   Set insWcoViews = New clsWcoViews
   Set insCalculaTotales = New clsCalculaTotales
   If (gDefProg.GetSiglasDelPrograma = GetSiglasIVA Or gDefProg.GetSiglasDelPrograma = GetSiglasWinCont Or gDefProg.GetSiglasDelPrograma = GetSiglasSAW) Then
      NombreCampoTablaCambio = "CambioAMonedaLocal"
      NombreTablaCambio = "Comun.Cambio"
   Else
      NombreTablaCambio = "dbo.Cambio"
      NombreCampoTablaCambio = "cambioABolivares"
   End If
   CteMovimientosDolar = ""
   If valEsInformeConCodigoDeMoneda Then
      varNombreTablaAsiento = "MovimientosDolar_CTE"
      CteMovimientosDolar = " set dateformat dmy; WITH MovimientosDolar_CTE (ConsecutivoPeriodo, Codigo, DescripcionCC, TituloCC, CodigoCuenta, Descripcion, FechaComprobante, MontoDebe, MontoHaber) AS "
      CteMovimientosDolar = CteMovimientosDolar & " (SELECT ConsecutivoPeriodo, Codigo, DescripcionCC, TituloCC, CodigoCuenta, Descripcion, FechaComprobante, ROUND(MontoDebe / " & NombreCampoTablaCambio & ", 2), ROUND(MontoHaber / " & NombreCampoTablaCambio & ", 2)"
      CteMovimientosDolar = CteMovimientosDolar & " FROM " & insWcoViews.GetViewCuentasPorCentroDeCostos & " INNER JOIN " & NombreTablaCambio & " ON (" & NombreTablaCambio & ".FechaDeVigencia = " & insWcoViews.GetViewCuentasPorCentroDeCostos & ".FechaComprobante And " & NombreTablaCambio & ".CodigoMoneda = '" & valCodigoMoneda & "') "
      CteMovimientosDolar = CteMovimientosDolar & " WHERE " & insWcoViews.GetViewCuentasPorCentroDeCostos & ".ConsecutivoPeriodo = " & gContPeriodoActual.GetConsecutivoPeriodo & ")"
   Else
      varNombreTablaAsiento = insWcoViews.GetViewCuentasPorCentroDeCostos
   End If
   vSaldo = gUtilSQL.getIIF(" tblSaldoContableTemp.Saldo IS NULL ", 0, "tblSaldoContableTemp.Saldo", True)
   vMontoDebe = gUtilSQL.getIIF(varNombreTablaAsiento & ".MontoDebe IS NULL ", 0, varNombreTablaAsiento & ".MontoDebe", True)
   vMontoHaber = gUtilSQL.getIIF(varNombreTablaAsiento & ".MontoHaber IS NULL ", 0, varNombreTablaAsiento & ".MontoHaber", True)
   Sql = CteMovimientosDolar & " SELECT tblSaldoContableTemp.CodeAux AS CodigoCC, "
   Sql = Sql & "tblSaldoContableTemp.Codigo AS CodigoCuenta, "
   Sql = Sql & "tblSaldoContableTemp.Descripcion, "
   Sql = Sql & "MIN " & vSaldo & "   AS SaldoInicial, "
   Sql = Sql & "SUM" & vMontoDebe & " as SaldoDebe, "
   Sql = Sql & "SUM" & vMontoHaber & " as SaldoHaber, "
   Sql = Sql & "SUM(" & vSaldo & "  + (" & vMontoDebe & " - " & vMontoHaber & ")) AS SaldoActual, "
   Sql = Sql & " AplicaReconversion "
   Sql = Sql & " FROM tblSaldoContableTemp "
   Sql = Sql & " LEFT JOIN " & varNombreTablaAsiento
   Sql = Sql & " ON " & varNombreTablaAsiento & ".Codigo = tblSaldoContableTemp.CodeAux"
   Sql = Sql & " AND " & varNombreTablaAsiento & ".CodigoCuenta = tblSaldoContableTemp.Codigo"
   Sql = Sql & " AND " & gUtilSQL.fSQLValueDatesBetween("", varNombreTablaAsiento & ".FechaComprobante ", valFechaInicial, valFechaFinal)
   Sql = Sql & " AND " & varNombreTablaAsiento & ".ConsecutivoPeriodo = " & gContPeriodoActual.GetConsecutivoPeriodo
   Sql = Sql & " WHERE tblSaldoContableTemp.ID = " & gUtilSQL.fSimpleSqlValue(valTableName)
   Sql = Sql & " AND tblSaldoContableTemp.TieneSubCuentas = " & gUtilSQL.fSimpleSqlValue(gConvert.ConvertBooleanToString(False))
   Sql = Sql & " GROUP BY tblSaldoContableTemp.CodeAux, tblSaldoContableTemp.Codigo, tblSaldoContableTemp.Descripcion,AplicaReconversion "
   Sql = Sql & " ORDER BY tblSaldoContableTemp.CodeAux, tblSaldoContableTemp.Codigo, tblSaldoContableTemp.Descripcion,AplicaReconversion "
   If gDbUtil.fOpenRecordset(vRecordset, Sql, Conexion) Then
      If gDbUtil.fRecordCount(vRecordset) > 0 Then
         vRecordset.MoveFirst
         While Not vRecordset.EOF
            vUpdateSQL = "UPDATE tblSaldoContableTemp SET SaldoInicial = " & gUtilSQL.fNumToStrSQL(vRecordset("SaldoInicial").value)
            vUpdateSQL = vUpdateSQL & ", SubTotalDebe = " & gUtilSQL.fNumToStrSQL(vRecordset("SaldoDebe").value) & ", SubTotalHaber = " & gUtilSQL.fNumToStrSQL(vRecordset("SaldoHaber").value) & ", Saldo = " & gUtilSQL.fNumToStrSQL(vRecordset("SaldoActual").value)
            vUpdateSQL = vUpdateSQL & " WHERE Codigo = " & gUtilSQL.fSimpleSqlValue(vRecordset("CodigoCuenta").value)
            vUpdateSQL = vUpdateSQL & " AND CodeAux = " & gUtilSQL.fSimpleSqlValue(vRecordset("CodigoCC").value)
            vUpdateSQL = vUpdateSQL & " AND ID = " & gUtilSQL.fSimpleSqlValue(valTableName)
            gDbUtil.Execute Conexion, vUpdateSQL
            vRecordset.MoveNext
         Wend
      End If
   End If
   insCalculaTotales.RecalculaSaldosDelEstadoFinanciero valTableName
   Set vRecordset = Nothing
   Set insWcoViews = Nothing
   Set insCalculaTotales = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "CompletarTablaDeSaldoyAuxiliares", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Public Sub sEliminaDatosEsquemasAsociadosAPeriodo(ByVal valConsecutivoPeriodo As Long)
   Dim insEsquemaBalanceMov As clsEsquemaDeBalanceMovNavigator
   Dim insEsquemaBalance As clsEsquemaDeBalanceNavigator
   Dim varSQL As String
   On Error GoTo h_ERROR
   Set insEsquemaBalanceMov = New clsEsquemaDeBalanceMovNavigator
   Set insEsquemaBalance = New clsEsquemaDeBalanceNavigator
   varSQL = "DELETE FROM " & insEsquemaBalanceMov.GetTableName & " WHERE ConsecutivoMultiArchivo = " & valConsecutivoPeriodo
   gDbUtil.Execute Conexion, varSQL
   varSQL = "DELETE FROM " & insEsquemaBalance.GetTableName & " WHERE ConsecutivoMultiArchivo = " & valConsecutivoPeriodo
   gDbUtil.Execute Conexion, varSQL
   Set insEsquemaBalanceMov = Nothing
   Set insEsquemaBalance = Nothing
h_EXIT:
   On Error GoTo 0
   Exit Sub
h_ERROR:
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sEliminaDatosEsquemasAsociadosAPeriodo", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Function fPuedoContinuar() As Boolean
   On Error GoTo h_ERROR
   fPuedoContinuar = gProyCompaniaActual.GetUsaModuloDeContabilidad
h_EXIT:
   On Error GoTo 0
   Exit Function
h_ERROR:
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fPuedoContinuar", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Sub sCambiaLaBarraDeMenuSiEsNecesario()
   On Error GoTo h_ERROR
   gBarraDeMenu.sCambiaLaBarraDeMenuSiEsNecesario gDefgen.getMainForm, me_PRINCIPALSAW, "ESCOGER"
h_EXIT:
   On Error GoTo 0
   Exit Sub
h_ERROR:
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sCambiaLaBarraDeMenuSiEsNecesario", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Function fGetDatosCompaniaSAWForXML() As String
   Dim vXmlCompania As String
   On Error GoTo h_ERROR
   vXmlCompania = fCreateElement("UsaModuloDeContabilidad", gConvert.ConvertBooleanToString(gProyCompaniaActual.GetUsaModuloDeContabilidad), True)
   vXmlCompania = vXmlCompania & fCreateElement("NombreCorto", gProyCompaniaActual.GetNombre, True)
   fGetDatosCompaniaSAWForXML = vXmlCompania
h_EXIT:
   On Error GoTo 0
   Exit Function
h_ERROR:
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fGetDatosCompaniaSAWForXML", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fCreateElement(ByVal valNameElement As String, ByVal valValueElement As String, Optional ByVal valElementoAbreYCierraEnUnaLinea As Boolean = False) As String
   Dim vResult As String
   Dim vEspacio As String
   Dim vLibWinCont As clsLibWincont
   On Error GoTo h_ERROR
   vEspacio = vbCrLf
   If valValueElement = "" Then
      vEspacio = ""
   End If
   Set vLibWinCont = New clsLibWincont
   vEspacio = ""
   If valElementoAbreYCierraEnUnaLinea Then
      vResult = vResult & "<" & valNameElement & ">" & vLibWinCont.fToXmlValidStrValue(valValueElement) & "</" & valNameElement & ">" & vbCrLf
   Else
      vResult = vResult & "<" & valNameElement & ">" & vbCrLf
      vResult = vResult & vLibWinCont.fToXmlValidStrValue(valValueElement) & vEspacio
      vResult = vResult & "</" & valNameElement & ">" & vbCrLf
   End If
   Set vLibWinCont = Nothing
   fCreateElement = vResult
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateElement", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fCompaniaUsaModuloContabilidad() As Boolean
   On Error GoTo h_ERROR
   fCompaniaUsaModuloContabilidad = gProyCompaniaActual.GetUsaModuloDeContabilidad
h_EXIT:
   On Error GoTo 0
   Exit Function
h_ERROR:
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCompaniaUsaModuloContabilidad", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fEjecutaProcesoPostEliminacionCobranza(ByVal valNumeroDocOrigen As String) As Boolean
   Dim vResultado As Boolean
   Dim insGenComprobante As clsGenerarComprobanteNavigator
   On Error GoTo h_ERROR
   vResultado = False
   If gProyParametrosCompania.GetUsarVentasConIvaDiferido Then
      Set insGenComprobante = New clsGenerarComprobanteNavigator
      insGenComprobante.sProcesoPostDescontabilizacionDeCobranza valNumeroDocOrigen
      vResultado = True
      Set insGenComprobante = Nothing
   Else
      vResultado = True
   End If
   fEjecutaProcesoPostEliminacionCobranza = vResultado
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fEjecutaProcesoPostEliminacionCobranza", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fExisteLaTasaDelDia(ByVal valFechaDeVigencia As Date, ByVal valCodigoMoneda As String, ByRef refCambioAbolivares As String) As Boolean
   Dim wrpCambio As Object
   Dim mCambio As Currency
   Dim vResult As Boolean
   On Error GoTo h_ERROR
   Set wrpCambio = gUtilWrp.fCreateWrpForModule(gUtilWrp.fWrpClassName("TablasGen", "wrpCambio"), gProyUsuarioActual.GetNombreDelUsuario, gProyUsuarioActual.GetPassword)
   vResult = wrpCambio.ExisteTasaDeCambioParaElDia(valCodigoMoneda, valFechaDeVigencia, refCambioAbolivares)
   mCambio = gConvert.fConvertStringToCurrency(refCambioAbolivares)
   If vResult And mCambio = 0 Then
      vResult = False
   End If
   fExisteLaTasaDelDia = vResult
   Set wrpCambio = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: fExisteLaTasaDelDia = False
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fexisteLaTasaDelDia", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Private Function sCalculaResultadoDelEjercicio(ByVal valEsInformeConCodigoDeMoneda As Boolean, ByVal valCalculaSaldos As clsCalculaTotales, ByVal valTableName As String)
   Dim varMontoDebe As Currency
   Dim varMontoHaber As Currency
   Dim varResultadoDelEjercicio As Currency
   Dim varCuentaIngreso As String
   Dim varCuentaCosto As String
   Dim varCuentaGasto As String
   Dim varNombreCampo As String
   Dim varCodigoCuentaResultado As String
   Dim inscuentaNavigator As clsCuentaNavigator
   Dim tableNameAnterior As String
   Dim Sql As String
   Dim varSQLWhere As String
   Dim mrsTemp As ADODB.Recordset
   On Error GoTo h_ERROR
   varCodigoCuentaResultado = gContPeriodoActual.GetCierreDelEjercicio
   If gContPeriodoActual.ExisteElComprobanteDeCierreDeCuentasDeResultado And valEsInformeConCodigoDeMoneda Then
      Set mrsTemp = New ADODB.Recordset
      Set inscuentaNavigator = New clsCuentaNavigator
      varCodigoCuentaResultado = gContPeriodoActual.GetCierreDelEjercicio
      If inscuentaNavigator.fSearchCuentaByCodigo(gContPeriodoActual.GetCuentaResultadoDelEjercicio, True) Then
         varNombreCampo = "Codigo"
         varCuentaIngreso = fConvierteLikeSQL(varCuentaIngreso, varNombreCampo, gContPeriodoActual.getArrayIngreso, "")
         varCuentaCosto = fConvierteLikeSQL(varCuentaCosto, varNombreCampo, gContPeriodoActual.getArrayCosto, "OR")
         varCuentaGasto = fConvierteLikeSQL(varCuentaGasto, varNombreCampo, gContPeriodoActual.getArrayGasto, "OR")
         varSQLWhere = varSQLWhere & "(" & varCuentaIngreso & varCuentaCosto & varCuentaGasto & ") "
         Sql = "Select SUM(SubTotalDebe) as mTotalDebe, sum(SubTotalHaber) as mTotalHaber FROM " & valCalculaSaldos.getTNSaldoTemp & " WHERE " & varSQLWhere & " AND TieneSubCuentas = 'N' AND " & valCalculaSaldos.SQLIdentificador(valTableName)
         gDbUtil.OpenRecordset mrsTemp, Sql
         varMontoDebe = mrsTemp("mTotalDebe")
         varMontoHaber = mrsTemp("mTotalHaber")
         varResultadoDelEjercicio = 0
         varResultadoDelEjercicio = varMontoDebe - varMontoHaber
         If varResultadoDelEjercicio > 0 Then
            Sql = "UPDATE tblSaldoContableTemp SET SaldoInicial = 0, SubTotalDebe = " & gUtilSQL.fNumToStrSQL(varResultadoDelEjercicio) & " + SubTotalDebe, Saldo = " & gUtilSQL.fNumToStrSQL(varResultadoDelEjercicio) & " WHERE Codigo = '" & varCodigoCuentaResultado & "' AND ID = '" & valTableName & "'"
         Else
            Sql = "UPDATE tblSaldoContableTemp SET SaldoInicial = 0, SubTotalHaber = (" & gUtilSQL.fNumToStrSQL(varResultadoDelEjercicio) & " *-1) + SubTotalHaber, Saldo = " & gUtilSQL.fNumToStrSQL(varResultadoDelEjercicio) & " WHERE Codigo = '" & varCodigoCuentaResultado & "' AND ID = '" & valTableName & "'"
         End If
      End If
      gDbUtil.Execute Conexion, Sql
      Sql = "UPDATE  tblSaldoContableTemp SET SubTotalDebe = (CASE WHEN SubTotalDebe < SubTotalHaber THEN SubTotalHaber ELSE SubTotalDebe END), SubTotalHaber = (CASE WHEN SubTotalHaber < SubTotalDebe THEN SubTotalDebe ELSE SubTotalHaber END) WHERE " & varSQLWhere & " AND ID = '" & valTableName & "'"
      gDbUtil.Execute Conexion, Sql
      Set mrsTemp = Nothing
      Set inscuentaNavigator = Nothing
   End If
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCalculaResultadoDelEjercicio", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fConvierteLikeSQL(ByVal Sql As String, ByVal nombreCampo As String, marreglo As Variant, valOperador As String) As String
   Dim i As Long
   On Error GoTo h_ERROR
   For i = LBound(marreglo) To UBound(marreglo)
      If LenB(marreglo(i)) > 0 Then
         Sql = Sql & " " & valOperador & " " & nombreCampo & " LIKE '" & marreglo(i) & "%'"
         valOperador = "OR"
      End If
   Next
   fConvierteLikeSQL = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConvierteLikeSQL", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
