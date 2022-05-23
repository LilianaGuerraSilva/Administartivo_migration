VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsGenerarComprobanteNavigator"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = False
Option Explicit
'Estructura de 1 linea de contabilizacion:
'Cada septupla tiene la estructura: DEBE o HABER, monto, cuenta, auxiliar, centrode Costo, NumRef, DescripcionAsiento
Private mImprimirEspecialAlGrabarComprobante As Boolean
Private mImprimeComprobanteDeMovBancarioAlGrabar As Boolean
Private mPagoProcesoDeImpresionEspecial As Object
Private mNumeroDeMovBancario As Long
Private mImprimirEspecialAlGrabarComprobanteSueldo As Boolean
Private mListaCuentasBancariasPagos As String
Private mListaCXPPagosProveedores As String

Private Function CM_FILE_NAME() As String
   CM_FILE_NAME = "clsGenerarComprobanteNavigator"
End Function

Private Function CM_MESSAGE_NAME() As String
   CM_MESSAGE_NAME = "Generar Comprobante"
End Function

Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function

Private Function CM_IMPRIME_COMPROBANTE_DE_PAGO() As Byte
   CM_IMPRIME_COMPROBANTE_DE_PAGO = 0
End Function

Private Function CM_IMPRIME_COMPROBANTE_DE_MOV_BANCARIO() As Byte
   CM_IMPRIME_COMPROBANTE_DE_MOV_BANCARIO = 1
End Function
Private Function CM_IMPRIME_COMPROBANTE_DE_PAGO_SUELDO() As Byte
   CM_IMPRIME_COMPROBANTE_DE_PAGO_SUELDO = 2
End Function



Public Function fGeneraElMasterDeComprobante(ByRef refNumeroDeComprobante As String, ByVal valDescripcionComprobante As String, _
            ByVal valFechaComprobante As Date, ByVal valTipoDeComprobante As String, _
            ByVal valNoDocumentoOrigen As String, ByVal valGeneradoPor As enum_ComprobanteGeneradoPor, ByVal valConsecutivoDoc As Long) As Boolean
   Dim ConExito As Boolean
   Dim comprobanteNavigator As clsComprobanteNavigator
   Dim parametroIniActivo As Boolean
   On Error GoTo h_ERROR
   Set comprobanteNavigator = New clsComprobanteNavigator
   ConExito = False
   parametroIniActivo = (UCase(gUtilFile.fReadIdentificadorFromFile(gDefProg.GetNombreFileINI, True, gSesionEspecial.getCM_OPCION_DE_CLIENTE)) = UCase("RESINCONEXIONCONTABILIZAR"))
   If parametroIniActivo Then
      gDbUtil.sResincronizaLaConexionConElProveedor gDefDatabase.Conexion
   End If
   If comprobanteNavigator.fGeneraUnNuevoRecordComprobante(valDescripcionComprobante, valFechaComprobante, valGeneradoPor, _
            valTipoDeComprobante, 0, 0, refNumeroDeComprobante, valNoDocumentoOrigen, eSC_CONTABILIZADO, valConsecutivoDoc) Then
      ConExito = True
   End If
   If (Not ConExito) And parametroIniActivo Then
      gDbUtil.sResincronizaLaConexionConElProveedor gDefDatabase.Conexion
   End If
   Set comprobanteNavigator = Nothing
h_EXIT:
   fGeneraElMasterDeComprobante = ConExito
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fGeneraElMasterDeComprobante", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fActualizaLosTotalesDelComprobante(ByVal valTotalDebe As Currency, ByVal valTotalHaber As Currency, _
         ByVal valNumeroDeComprobante As String, ByVal valElComprobanteFueModificadoPorElUsuario As Boolean) As Boolean
   Dim Sql As String
   Dim comprobanteNavigator As clsComprobanteNavigator
   Dim ConExito As Boolean
   On Error GoTo h_ERROR
   ConExito = False
   Set comprobanteNavigator = New clsComprobanteNavigator
   Sql = "UPDATE " & comprobanteNavigator.GetTableName & " SET TotalDebe = " & gUtilSQL.fNumToStrSQL(valTotalDebe) & ", " _
      & "TotalHaber = " & gUtilSQL.fNumToStrSQL(valTotalHaber) & ", FueModificado = " & gUtilSQL.fBooleanToStrSQL(valElComprobanteFueModificadoPorElUsuario) _
      & " WHERE NUMERO = '" & valNumeroDeComprobante & "' AND ConsecutivoPeriodo = " & gContPeriodoActual.GetConsecutivoPeriodo
   gDbUtil.Execute gDefDatabase.Conexion, Sql
   ConExito = True
   Set comprobanteNavigator = Nothing
h_EXIT:
   fActualizaLosTotalesDelComprobante = ConExito
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fActualizaLosTotalesDelComprobante", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fBlancoSiElValorEsNA(ByVal valValorAexaminar As String) As String
   Dim varNewValue As String
   On Error GoTo h_ERROR
   varNewValue = valValorAexaminar
   If UCase(valValorAexaminar) = "NA" Then
      varNewValue = ""
   End If
h_EXIT:
   fBlancoSiElValorEsNA = varNewValue
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fActualizaLosTotalesDelComprobante", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Sub sDesglosaLosValoresDelRecordsetyColocaEnLasVariables(ByRef refRecordSet As ADODB.Recordset, _
         ByRef refColumnaDeInicio As Integer, ByRef refMontoDebe As Currency, ByRef refMontoHaber As Currency, _
         ByRef refCodigoDeCuenta As String, ByRef refCodigoAuxiliar As String, ByRef refCentroDeCostos As String, _
         ByRef refNumeroDeReferencia As String, ByRef refDescripcionDelAsiento As String)
   Dim ColocarAlDebe As Boolean
   Dim MontoDelAsiento As Currency
   On Error GoTo h_ERROR
   refCodigoDeCuenta = ""
   refCodigoAuxiliar = ""
   refCentroDeCostos = ""
   refNumeroDeReferencia = ""
   refDescripcionDelAsiento = ""
   refMontoDebe = 0
   refMontoHaber = 0
   If IsNull(refRecordSet.Fields(refColumnaDeInicio + 1)) Then
      MontoDelAsiento = 0
      GoTo h_EXIT
   Else
      MontoDelAsiento = gConvert.fTruncaANDecimales(refRecordSet.Fields(refColumnaDeInicio + 1), 2)
   End If
   ColocarAlDebe = False
   If refRecordSet.Fields(refColumnaDeInicio) = "DEBE" Then
      ColocarAlDebe = True
   End If
   If MontoDelAsiento < 0 Then
      If ColocarAlDebe Then
         ColocarAlDebe = False
      Else
         ColocarAlDebe = True
      End If
   End If
   If ColocarAlDebe Then
      refMontoDebe = Abs(MontoDelAsiento)
   Else
      refMontoHaber = Abs(MontoDelAsiento)
   End If
   refCodigoDeCuenta = fBlancoSiElValorEsNA(refRecordSet.Fields(refColumnaDeInicio + 2))
   refCodigoAuxiliar = fBlancoSiElValorEsNA(refRecordSet.Fields(refColumnaDeInicio + 3))
   refCentroDeCostos = fBlancoSiElValorEsNA(refRecordSet.Fields(refColumnaDeInicio + 4))
   refNumeroDeReferencia = fBlancoSiElValorEsNA(refRecordSet.Fields(refColumnaDeInicio + 5))
   refDescripcionDelAsiento = gTexto.DfMid(fBlancoSiElValorEsNA(refRecordSet.Fields(refColumnaDeInicio + 6)), 1, 40)
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sDesglosaLosValoresDelRecordsetyColocaEnLasVariables", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Function fGeneraLosAsientosDelComprobante(ByVal Sql As String, ByRef refTotalDebe As Currency, _
            ByRef refTotalHaber As Currency, ByVal valNumeroDeComprobante As String, _
            ByVal valNoDocumentoOrigen As String, ByVal valGeneradoPor As enum_ComprobanteGeneradoPor, _
            ByRef refNumeroDeAsiento As Integer, ByVal valFechaComprobante As Date, Optional ByVal valTipoDocumento As String, Optional ByVal valRegla As String) As Boolean
   Dim varContadorAsiento As Integer
   Dim varMontoDebe As Currency
   Dim varMontoHaber As Currency
   Dim varCuentaContable As String
   Dim varCentroDeCostos As String
   Dim varCodigoAuxiliar As String
   Dim varNumeroDeReferencia As String
   Dim varDescripcionAsiento As String
   Dim Continuar As Boolean
   Dim asientoNavigator As clsAsientoNavigator
   Dim mRecordSet As ADODB.Recordset
   Dim nCount As Integer
   Dim Diferencia As Currency
   Dim varTodosLosAsientosVienenEnCero As Boolean
   Dim vAsientosEnCeroEnPago As Boolean
   Dim vTotalDebe As Currency
   Dim vTotalHaber As Currency
    
   On Error GoTo h_ERROR
   Set asientoNavigator = New clsAsientoNavigator
   Set mRecordSet = New ADODB.Recordset
   Continuar = True
   If Not gDbUtil.fOpenRecordSetAllParametersCommandTO(mRecordSet, Sql, gDefDatabase.Conexion, adLockOptimistic, adUseClient, adOpenStatic, , , 32000) Then
      GoTo h_EXIT
   End If
   If gDbUtil.fRecordCount(mRecordSet) > 0 Then
      If refNumeroDeAsiento = 0 Then
         refNumeroDeAsiento = 1
      End If
      If valGeneradoPor = eCG_MOVIMIENTO_BANCARIO Then
         varTodosLosAsientosVienenEnCero = Not fHayAlMenosUnMontoDistintoDeCero(mRecordSet)
      End If
      If valGeneradoPor = eCG_INVENTARIO Then
        varTodosLosAsientosVienenEnCero = True
      End If
      mRecordSet.MoveFirst
      While Not mRecordSet.EOF And Continuar
         nCount = 1
         While nCount < mRecordSet.Fields.Count - 1
            If mRecordSet.Fields(nCount).value <> "" Then
               If (mRecordSet.Fields(nCount).value = "DEBE" Or _
                  mRecordSet.Fields(nCount).value = "HABER") And _
                  (mRecordSet.Fields.Count - 1 >= fCondicion(valGeneradoPor, nCount)) Then
                  sDesglosaLosValoresDelRecordsetyColocaEnLasVariables mRecordSet, nCount, varMontoDebe, _
                     varMontoHaber, varCuentaContable, varCodigoAuxiliar, varCentroDeCostos, varNumeroDeReferencia, varDescripcionAsiento
                  If Not fElAuxiliarExisteEnElPeriodo(varCuentaContable) Then
                     varCuentaContable = valRegla
                  End If
                  vAsientosEnCeroEnPago = (varMontoDebe = 0 And varMontoHaber = 0 And valGeneradoPor = eCG_PAGOS)
                  vTotalDebe = vTotalDebe + varMontoDebe
                  vTotalHaber = vTotalHaber + varMontoHaber
                  If vAsientosEnCeroEnPago Then
                     varTodosLosAsientosVienenEnCero = fCuentaEsDeProveeodrOBanco(varCuentaContable) And vTotalDebe = 0 And vTotalHaber = 0
                     vAsientosEnCeroEnPago = vAsientosEnCeroEnPago Or varTodosLosAsientosVienenEnCero
                     gProyParametrosWinCont.SetPermitirAsientosEnCero vAsientosEnCeroEnPago
                  End If
                  If (varMontoDebe <> 0 Or varMontoHaber <> 0) Or _
                      (varMontoDebe = 0 And varMontoHaber = 0 And gTexto.DfInStr(varDescripcionAsiento, "ANULACION") = 1) Or _
                      varTodosLosAsientosVienenEnCero Then
                     Continuar = asientoNavigator.fGeneraUnNuevoRecordDeAsiento(valNumeroDeComprobante, _
                           valFechaComprobante, varCuentaContable, varMontoDebe, varMontoHaber, varDescripcionAsiento, _
                           refNumeroDeAsiento, varCodigoAuxiliar, varCentroDeCostos, _
                           (gTexto.DfInStr(varDescripcionAsiento, "ANULACION") = 1) Or varTodosLosAsientosVienenEnCero, _
                           True, False, varNumeroDeReferencia, fFechaReferenciaSegunModulo(valGeneradoPor, valFechaComprobante, mRecordSet), eTDA_NORMAL, False, _
                           gDbUtil.getMinValueForDateType, "")
                  End If
                  refTotalDebe = refTotalDebe + varMontoDebe
                  refTotalHaber = refTotalHaber + varMontoHaber
                  refNumeroDeAsiento = refNumeroDeAsiento + 1
               Else
                  gMessage.Advertencia "Advertencia para el programador = Incompatibilidad de Número de Columnas en el RecordSet. " & vbCr & vbCr & _
                              "Si no es programador, por favor comuníquese con " & gDefgen.fNombreInformaticaGalac
               End If
            End If
            nCount = nCount + 7
         Wend
         vTotalDebe = 0
         vTotalHaber = 0
         If Continuar Then
            mRecordSet.MoveNext
         End If
      Wend
      Diferencia = refTotalDebe - refTotalHaber
      If Diferencia <> 0 Then
         varMontoDebe = 0
         varMontoHaber = 0
         If Diferencia > 0 Then
            varMontoHaber = Abs(Diferencia)
            refTotalHaber = refTotalHaber + Abs(Diferencia)
         Else
            varMontoDebe = Abs(Diferencia)
            refTotalDebe = refTotalDebe + Abs(Diferencia)
         End If
         varDescripcionAsiento = gEnumProyectoWincont.enumComprobanteGeneradoPorToString(valGeneradoPor) & " No. " & valNoDocumentoOrigen
         Continuar = asientoNavigator.fGeneraUnNuevoRecordDeAsiento(valNumeroDeComprobante, valFechaComprobante, _
                           gProyReglasDeContabilizacion.GetDiferenciaEnCambioyCalculo, varMontoDebe, varMontoHaber, _
                           varDescripcionAsiento, refNumeroDeAsiento, "", "", False, True, False, "", _
                           valFechaComprobante, eTDA_NORMAL, False, gDbUtil.getMinValueForDateType, "")
      End If
   End If
h_EXIT:
   fGeneraLosAsientosDelComprobante = Continuar
   Set asientoNavigator = Nothing
   gDbUtil.sDestroyRecordSet mRecordSet
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fGeneraLosAsientosDelComprobante ", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fCuentaEsDeProveeodrOBanco(ByVal valCuentaContable As String) As Boolean
    Dim vResult As Boolean
    On Error GoTo h_ERROR
    vResult = fBuscarCuentaBancaria(valCuentaContable)
    vResult = vResult Or fBuscarCuentaProveedor(valCuentaContable)
h_EXIT:     On Error GoTo 0
    fCuentaEsDeProveeodrOBanco = vResult
    Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fCuentaEsDeProveeodrOBanco", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fBuscarCuentaBancaria(ByVal valCodigoCuenta As String) As Boolean
    Dim vResult As Boolean
    Dim insPagoNav As clsPagoNavigator
    On Error GoTo h_ERROR
    If mListaCuentasBancariasPagos = "" Then
        Set insPagoNav = New clsPagoNavigator
        mListaCuentasBancariasPagos = insPagoNav.fListarCuentaBancariaPagos
    End If
    Set insPagoNav = Nothing
    vResult = gTexto.fElementIsInList(mListaCuentasBancariasPagos, valCodigoCuenta, gTexto.fSeparadorStandardDeElementosString)
    vResult = vResult Or gTexto.fS1EsIgualAS2(gProyReglasDeContabilizacion.GetCuentaPagosBanco, valCodigoCuenta)
    vResult = vResult And Not gTexto.fS1EsIgualAS2(gProyReglasDeContabilizacion.GetCuentaPagosOtros, valCodigoCuenta)
h_EXIT: On Error GoTo 0
    fBuscarCuentaBancaria = vResult
Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fBuscarCuentaBancaria", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fBuscarCuentaProveedor(ByVal valCodigoCuenta As String) As Boolean
    Dim vResult As Boolean
    Dim insProveedorNav As clsProveedorNavigator
    On Error GoTo h_ERROR
    If mListaCXPPagosProveedores = "" Then
        Set insProveedorNav = New clsProveedorNavigator
        mListaCXPPagosProveedores = insProveedorNav.fListarCuentaContableProveedores
    End If
    Set insProveedorNav = Nothing
    vResult = gTexto.fElementIsInList(mListaCXPPagosProveedores, valCodigoCuenta, gTexto.fSeparadorStandardDeElementosString)
    vResult = vResult Or gTexto.fS1EsIgualAS2(gProyReglasDeContabilizacion.GetCuentaPagosCxPProveedores, valCodigoCuenta)
h_EXIT: On Error GoTo 0
    fBuscarCuentaProveedor = vResult
Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fBuscarCuentaProveedor", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLConjuntoDeRecordsDeContabilizacion(ByVal valNombreTablaOrigen As String, _
               ByVal valNombreCampoNumero As String, ByVal valNombreCampoFecha As String, _
               ByVal valNombreCampoConsecutivoCompania As String, ByVal valGeneradoPor As enum_ComprobanteGeneradoPor, _
               ByVal valFechaDesde As Date, ByVal valFechaHasta As Date, ByVal valDevuelveLosContabilizados As Boolean, _
               ByVal valElCampoClaveEsStrType As Boolean, Optional ByVal valSQLOtherFilters As String = "") As String
   Dim valorDelSql As String
   Dim ConjuntoDeRecords As String
   Dim insComprobante As clsComprobanteNavigator
   Dim varSQLHavingClause As String
   Dim varFieldNumero As String
   Dim varFechaInicial As Date
   Dim varFNFecha As String
   On Error GoTo h_ERROR
   Set insComprobante = New clsComprobanteNavigator
   varFechaInicial = fGetFechaDesdeValidaParaBusqueda(valFechaDesde)
   varFieldNumero = fFieldNameNumeroWithTableName(valNombreTablaOrigen, valNombreCampoNumero)
   varSQLHavingClause = ""
'********** OJO AYRG LINEAS DE MES/ANO APLICACION   **************
   valorDelSql = "SELECT " & gUtilSQL.fTOP(100) & varFieldNumero & " FROM " & valNombreTablaOrigen
   valorDelSql = valorDelSql & " WHERE " & valNombreTablaOrigen & "." & valNombreCampoConsecutivoCompania _
                  & " = " & gProyCompaniaActual.GetConsecutivoCompania
'MOISES
   If valNombreTablaOrigen = "cxP" Then
      valorDelSql = valorDelSql & " AND " & valNombreTablaOrigen & ".EstaAsociadoARendicion = " _
                    & gUtilSQL.fBooleanToSqlValue(False) & " AND ("
   Else
      valorDelSql = valorDelSql & " AND ("
   End If
   If (valGeneradoPor = eCG_CXP) And (gTexto.DfInStr(valNombreCampoFecha, gUtilSQL.getPrefijoIIF) > 0) Then
      varFNFecha = valNombreCampoFecha
   Else
      varFNFecha = valNombreTablaOrigen & "." & valNombreCampoFecha
   End If
   If varFechaInicial = valFechaHasta Then
      valorDelSql = valorDelSql & varFNFecha & " = " & gUtilSQL.fDateToSQLValue(valFechaHasta) & ")"
   Else
      valorDelSql = valorDelSql & gUtilSQL.DfSQLDateValueBetween(varFNFecha, varFechaInicial, valFechaHasta) & ")"
   End If
   If valSQLOtherFilters <> "" Then
      valorDelSql = valorDelSql & " AND " & valSQLOtherFilters
   End If
   valorDelSql = valorDelSql & " GROUP BY " & varFieldNumero
   If valGeneradoPor <> eCG_CXP And gSesionEspecial.fIsActiveSPModoAvanzado Then
      valorDelSql = valorDelSql & ", " & varFNFecha
   End If
'********** OJO AYRG FIN LINEAS DE MES/ANO APLICACION   **************
   varSQLHavingClause = " SELECT " & insComprobante.GetTableName & ".NoDocumentoOrigen FROM " & insComprobante.GetTableName
   varSQLHavingClause = varSQLHavingClause & " WHERE " & insComprobante.GetTableName & ".ConsecutivoPeriodo = " & gContPeriodoActual.GetConsecutivoPeriodo
   If valGeneradoPor = eCG_FACTURA Then
      varSQLHavingClause = varSQLHavingClause & " AND (" & insComprobante.GetTableName & ".GeneradoPor = " & gUtilSQL.fSQLSimpleValueForEnum(eCG_FACTURA)
      varSQLHavingClause = varSQLHavingClause & " OR " & insComprobante.GetTableName & ".GeneradoPor = " & gUtilSQL.fSQLSimpleValueForEnum(eCG_RESUMEN_DIARIO_VENTAS) & ")"
   Else
      varSQLHavingClause = varSQLHavingClause & " AND " & insComprobante.GetTableName & ".GeneradoPor = " & gUtilSQL.fSQLSimpleValueForEnum(valGeneradoPor)
   End If
   varSQLHavingClause = varSQLHavingClause & " AND "
   If valGeneradoPor <> eCG_CXP And gSesionEspecial.fIsActiveSPModoAvanzado Then
      varSQLHavingClause = varSQLHavingClause & insComprobante.GetTableName & ".FECHA = " & varFNFecha & " AND "
   End If
   If Not valElCampoClaveEsStrType Then
      varSQLHavingClause = varSQLHavingClause & _
                 gUtilSQL.getIIF("(" & insComprobante.GetTableName & ".GeneradoPor = " & gUtilSQL.fSimpleSqlValue(gConvert.enumerativoAChar(valGeneradoPor)) & ") AND " & gUtilSQL.fIsNumeric(insComprobante.GetTableName & ".NoDocumentoOrigen"), gUtilSQL.DfCIntSQL(insComprobante.GetTableName & ".NoDocumentoOrigen"), "NULL", True) & "="
   Else
      varSQLHavingClause = varSQLHavingClause & insComprobante.GetTableName & ".NoDocumentoOrigen = "
   End If
   varSQLHavingClause = varSQLHavingClause & varFieldNumero
   varSQLHavingClause = varSQLHavingClause
   varSQLHavingClause = " HAVING " & gUtilSQL.fCompareWithNull(varSQLHavingClause, Not valDevuelveLosContabilizados)
   valorDelSql = valorDelSql & varSQLHavingClause
   ConjuntoDeRecords = gDbUtil.fBuildResultSetAsString(valorDelSql, valElCampoClaveEsStrType)
   If (ConjuntoDeRecords = "") Or (ConjuntoDeRecords = "''") Then
     ConjuntoDeRecords = ""
     GoTo h_EXIT
   End If
h_EXIT:
   Set insComprobante = Nothing
   fSQLConjuntoDeRecordsDeContabilizacion = ConjuntoDeRecords
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLConjuntoDeRecordsDeContabilizacion", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fGeneraElComprobanteParaUnoOVariosDocumentos(ByVal valFechaDesde As Date, ByVal valFechaHasta As Date, _
      ByVal valModuloAContabilizar As enum_ComprobanteGeneradoPor, ByVal valContabilizarSolo1Documento As Boolean, _
      ByVal valNumeroDocumento As String, ByVal valEsUnReverso As Boolean, ByVal valElMasterYaExiste As Boolean, _
      ByVal valGeneraElMovimientoBancario As Boolean, ByVal valTipoDocumento As Integer, ByVal valConsecutivoDocumentoOrigen As Long, ByVal valTipoAlicPorContIGTF As enum_TipoAlicPorContIGTF) As Boolean
   Dim ConExito As Boolean
   Dim numeroDecomprobante As String
   Dim DescripcionComprobante As String
   Dim DescripcionAsientos As String
   Dim TotalDebe As Currency
   Dim TotalHaber As Currency
   Dim Sql As String
   Dim ContadorAsiento As Integer
   Dim NoDocumentoOrigen As String
   Dim GeneradoAlMenosUnAsiento As Boolean
   Dim ElComprobanteExiste As Boolean
   Dim conjuntoDeDocumentoAContabilizar As String
   Dim mRecordSet As ADODB.Recordset
   Dim valRegla As String
   On Error GoTo h_ERROR
   GeneradoAlMenosUnAsiento = False
   TotalDebe = 0
   TotalHaber = 0
   If valContabilizarSolo1Documento Then
      DescripcionComprobante = fDescripcionComprobanteUnoPorUno(valNumeroDocumento, valTipoDocumento, valModuloAContabilizar, valConsecutivoDocumentoOrigen)
      NoDocumentoOrigen = valNumeroDocumento
   Else
      DescripcionComprobante = fDescripcionComprobanteResumen(valFechaDesde, valFechaHasta, valModuloAContabilizar)
      NoDocumentoOrigen = fConstruyeNumeroDocumentoDeOrigen(valFechaDesde, valFechaHasta)
   End If
   DescripcionAsientos = gTexto.DfMid(DescripcionComprobante, 1, 40)
   ConExito = True
   If valElMasterYaExiste Then
      numeroDecomprobante = fBuscaElNumeroDelComprobanteAutomatico(valModuloAContabilizar, NoDocumentoOrigen)
      If numeroDecomprobante = "" Then
         ConExito = False
      End If
   Else
      ConExito = fGeneraElMasterDeComprobante(numeroDecomprobante, DescripcionComprobante, valFechaHasta, _
                         fTipoDeComprobante(valModuloAContabilizar), NoDocumentoOrigen, valModuloAContabilizar, valConsecutivoDocumentoOrigen)
   End If
   If ConExito Then
      conjuntoDeDocumentoAContabilizar = fSQLConjuntoRecordsDeContabilizacion1oMasDocumentos(valModuloAContabilizar, valContabilizarSolo1Documento, valNumeroDocumento, valFechaDesde, valFechaHasta, valTipoDocumento)
      Sql = fConstruyeSQLsegunElModuloAContabilizar(valFechaDesde, valFechaHasta, valModuloAContabilizar, _
               valEsUnReverso, DescripcionAsientos, valContabilizarSolo1Documento, NoDocumentoOrigen, _
               valGeneraElMovimientoBancario, valTipoDocumento, valRegla, valConsecutivoDocumentoOrigen, valTipoAlicPorContIGTF)
      If valModuloAContabilizar = eCG_INVENTARIO Then
         Set mRecordSet = New ADODB.Recordset
         gDbUtil.fOpenRecordSetAllParametersCommandTO mRecordSet, Sql, gDefDatabase.Conexion, adLockOptimistic, adUseClient, adOpenStatic, , , 32000
         If mRecordSet.RecordCount <= 0 Then
            Sql = fSqlAsientosEnCeroDeCostos(valFechaDesde)
         End If
         Set mRecordSet = Nothing
      End If
      If Sql <> "" Then
         ConExito = fGeneraLosAsientosDelComprobante(Sql, TotalDebe, TotalHaber, numeroDecomprobante, NoDocumentoOrigen, valModuloAContabilizar, ContadorAsiento, valFechaHasta, valTipoDocumento, valRegla)
         If ConExito Then
            GeneradoAlMenosUnAsiento = True
         End If
      End If
   End If
   If ConExito Then
      ConExito = fActualizaLosTotalesDelComprobante(TotalDebe, TotalHaber, numeroDecomprobante, False)
   End If
   If Not ConExito Or Not GeneradoAlMenosUnAsiento Then
      sMensajeDeErrorAlGenerar ConExito, GeneradoAlMenosUnAsiento, valFechaDesde, valFechaHasta, _
               valModuloAContabilizar, valContabilizarSolo1Documento, valNumeroDocumento, valTipoDocumento, valConsecutivoDocumentoOrigen
      If (numeroDecomprobante <> "") Then
         If gContPeriodoActual.BorraElComprobanteGenerado(numeroDecomprobante) Then
         End If
      End If
   Else
      If conjuntoDeDocumentoAContabilizar <> "" Then
         fEjecutaProcesoPostContabilizacion valModuloAContabilizar, valFechaDesde, valFechaHasta, conjuntoDeDocumentoAContabilizar
      End If
      If Not valContabilizarSolo1Documento Then
         gMessage.exito "El Comprobante " & DescripcionComprobante & " fué generado exitosamente"
      ElseIf (valModuloAContabilizar = eCG_PAGO_SUELDOS) Then
          If (gProyReglasDeContabilizacion.fEditaComprobanteDespuesDeInsertar(valModuloAContabilizar)) Then
            sEditaComprobanteGenerado numeroDecomprobante
          Else
            gMessage.exito "El Comprobante " & DescripcionComprobante & " fué generado exitosamente"
          End If
      ElseIf (valModuloAContabilizar <> eCG_PAGO_SUELDOS And gProyReglasDeContabilizacion.fEditaComprobanteDespuesDeInsertar(valModuloAContabilizar)) Then
         If gProyUsuarioActual.GetModificarComprobante Then
            sEditaComprobanteGenerado numeroDecomprobante
         End If
      End If
      fGeneraElComprobanteParaUnoOVariosDocumentos = True
   End If
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fGeneraElComprobanteParaUnoOVariosDocumentos", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
   
Private Function fTipoContabilizacionPorLote(ByVal valModulo As enum_ComprobanteGeneradoPor) As enum_ContabilizacionPorLote
   Dim varTipoContabilizacionPorLote As enum_ContabilizacionPorLote
   On Error GoTo h_ERROR
   Select Case valModulo
      Case eCG_CXC: varTipoContabilizacionPorLote = gProyReglasDeContabilizacion.GetContabPorLoteCxCAsEnum
      Case eCG_FACTURA: varTipoContabilizacionPorLote = gProyReglasDeContabilizacion.GetContabPorLoteFacturacionAsEnum
      Case eCG_CXP: varTipoContabilizacionPorLote = gProyReglasDeContabilizacion.GetContabPorLoteCxPAsEnum
      Case eCG_COBRANZA: varTipoContabilizacionPorLote = gProyReglasDeContabilizacion.GetContabPorLoteCobranzaAsEnum
      Case eCG_PAGOS: varTipoContabilizacionPorLote = gProyReglasDeContabilizacion.GetContabPorLotePagosAsEnum
      Case eCG_MOVIMIENTO_BANCARIO: varTipoContabilizacionPorLote = gProyReglasDeContabilizacion.GetContabPorLoteMovBancarioAsEnum
      Case eCG_RESUMEN_DIARIO_VENTAS: varTipoContabilizacionPorLote = gProyReglasDeContabilizacion.GetContabPorLoteRDVtasAsEnum
      Case eCG_ANTICIPO: varTipoContabilizacionPorLote = gProyReglasDeContabilizacion.GetContabPorLoteAnticipoAsEnum
   End Select
   fTipoContabilizacionPorLote = varTipoContabilizacionPorLote
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fTipoContabilizacionPorLote", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
   
Private Sub sMensajeDeErrorAlGenerar(ByVal valConExito As Boolean, ByVal valGeneradoAlMenosUnAsiento As Boolean, _
         ByVal valFechaDesde As Date, ByVal valFechaHasta As Date, ByVal valModuloAContabilizar As enum_ComprobanteGeneradoPor, _
         ByVal valContabilizarSolo1Documento As Boolean, ByVal valNumeroDocumento As String, ByVal valTipoDocumento As Integer, ByVal valConsecutivoDocumentoOrigen As Long)
   Dim varStrErrorMessage As String
   On Error GoTo h_ERROR
   If Not valConExito Then
      If valContabilizarSolo1Documento Then
         gMessage.Advertencia "No se pudo generar el comprobante del (la) " & _
               fDescripcionComprobanteUnoPorUno(valNumeroDocumento, valTipoDocumento, valModuloAContabilizar, valConsecutivoDocumentoOrigen)
      Else
         gMessage.Advertencia "No se pudo generar el comprobante " & _
               fDescripcionComprobanteResumen(valFechaDesde, valFechaHasta, valModuloAContabilizar)
      End If
   ElseIf Not valGeneradoAlMenosUnAsiento Then
      varStrErrorMessage = "No hay " & gEnumProyectoWincont.enumComprobanteGeneradoPorToString(valModuloAContabilizar) & _
               " para contabilizar "
      If fTipoContabilizacionPorLote(valModuloAContabilizar) = eCP_DIARIA Then
         varStrErrorMessage = varStrErrorMessage & "en el día " & gConvert.dateToString(valFechaHasta)
      Else
         varStrErrorMessage = varStrErrorMessage & "en el mes de " & gConvert.MesString(valFechaHasta)
      End If
      gMessage.Advertencia varStrErrorMessage
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sMensajeDeErrorAlGenerar", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Function fDescripcionComprobanteResumen(ByVal valFechaDesde As Date, ByVal valFechaHasta As Date, _
                     ByVal valModuloAContabilizar As enum_ComprobanteGeneradoPor) As String
   Dim varDescripcion As String
   On Error GoTo h_ERROR
   If (valModuloAContabilizar = eCG_INVENTARIO) Then
     varDescripcion = " Costo de las Ventas " & gEnumProyectoWincont.enumComprobanteGeneradoPorToString(valModuloAContabilizar)
   Else
     varDescripcion = "Resumen de " & gEnumProyectoWincont.enumComprobanteGeneradoPorToString(valModuloAContabilizar)
   End If
   If valFechaDesde = valFechaHasta Then
      varDescripcion = varDescripcion & " del día " & gConvert.dateToString(valFechaDesde)
   Else
      varDescripcion = varDescripcion & " del " & gConvert.dateToString(valFechaDesde) & _
                        " al " & gConvert.dateToString(valFechaHasta)
   End If
h_EXIT:
   fDescripcionComprobanteResumen = varDescripcion
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fDescripcionComprobanteResumen", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConstruyeNumeroDocumentoDeOrigen(ByVal valFechaDesde As Date, ByVal valFechaHasta As Date) As String
   Dim varDocOrigen As String
   On Error GoTo h_ERROR
'*****************************************
'La convención para el No. Doc. Origen es:
'*****************************************
   varDocOrigen = gConvert.fDateToKeyOrder(valFechaHasta)
   If valFechaDesde <> valFechaHasta Then
      varDocOrigen = gTexto.DfMid(varDocOrigen, 1, 6)
   End If
h_EXIT:
   fConstruyeNumeroDocumentoDeOrigen = varDocOrigen
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fConstruyeNumeroDocumentoDeOrigen", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fCambiaDebeHaberSiEsUnReverso(ByVal valNaturaleza As String, ByVal valEsUnReverso As Boolean) As String
   Dim varNaturalezaResultante As String
   On Error GoTo h_ERROR
   varNaturalezaResultante = valNaturaleza
   If valEsUnReverso Then
      If UCase(valNaturaleza) = "DEBE" Then
         varNaturalezaResultante = "HABER"
      Else
         varNaturalezaResultante = "DEBE"
      End If
   End If
h_EXIT:
   fCambiaDebeHaberSiEsUnReverso = varNaturalezaResultante
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fCambiaDebeHaberSiEsUnReverso", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fEsDebe(ByVal valDebeOHaber As String) As Boolean
   Dim EsDebe As Boolean
   On Error GoTo h_ERROR
   EsDebe = True
   If UCase(valDebeOHaber) = "HABER" Then
      EsDebe = False
   End If
h_EXIT:
   fEsDebe = EsDebe
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fEsDebe", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Sub sEditaComprobanteGenerado(ByVal valNumeroDeComprobante As String)
   Dim insComprobanteNavigator As clsComprobanteNavigator
   Dim insFrmComprobanteInput As frmComprobanteInput
   On Error GoTo h_ERROR
   If gProyUsuarioActual.GetModificarComprobantesAutomaticos Then
      Set insComprobanteNavigator = New clsComprobanteNavigator
      Set insFrmComprobanteInput = New frmComprobanteInput
      If insComprobanteNavigator.fSearchComprobantePorNumero(valNumeroDeComprobante, False, True) Then
         If (gTexto.DfLeft(insComprobanteNavigator.GetDescripcion, 12) = "ANULACION - ") And (insComprobanteNavigator.GetTotalDebe = 0 And insComprobanteNavigator.GetTotalHaber = 0) Then
         Else
            If mImprimirEspecialAlGrabarComprobante Then
               insFrmComprobanteInput.setProcesoDeImpresionEspecial CM_IMPRIME_COMPROBANTE_DE_PAGO, mPagoProcesoDeImpresionEspecial
            End If
            If mImprimeComprobanteDeMovBancarioAlGrabar Then
               insFrmComprobanteInput.setProcesoDeImpresionEspecial CM_IMPRIME_COMPROBANTE_DE_MOV_BANCARIO, mPagoProcesoDeImpresionEspecial, mNumeroDeMovBancario
            End If
             If mImprimirEspecialAlGrabarComprobanteSueldo Then
               insFrmComprobanteInput.setProcesoDeImpresionEspecial CM_IMPRIME_COMPROBANTE_DE_PAGO_SUELDO(), mPagoProcesoDeImpresionEspecial, mNumeroDeMovBancario
            End If
            
            insFrmComprobanteInput.sSetEjecutandoEditAfterInsertDocumento True
            insFrmComprobanteInput.sInitLookAndFeelAndSetValues Modificar, Nothing, False, False, insComprobanteNavigator
         End If
      End If
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sEditaComprobanteGenerado", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Function fDescripcionComprobanteUnoPorUno(ByVal valNumeroDocumento As String, ByVal valTipoDocumento As Integer, ByVal valModuloAContabilizar As enum_ComprobanteGeneradoPor, ByVal valConsecutivoDocOrigen As Long) As String
   Dim varDescripcion As String
   Dim varEstaAnulado As Boolean
   Dim varNoComunWincont As clsNoComunWincont
   Dim numeroFactura As String
   Dim numeroCxC As String
   Dim CXP As clsCxPNavigator
   On Error GoTo h_ERROR
   Set CXP = New clsCxPNavigator
   Set varNoComunWincont = New clsNoComunWincont
   varEstaAnulado = varNoComunWincont.fElDocumentoDeOrigenDelComprobanteFueAnulado(gConvert.enumerativoAChar(valModuloAContabilizar), valNumeroDocumento, valConsecutivoDocOrigen)
   Set varNoComunWincont = Nothing
   varDescripcion = ""
   If varEstaAnulado Then
      varDescripcion = "ANULACION - "
   End If
   Select Case valModuloAContabilizar
      Case eCG_CXP:
         If CXP.fSearchFromLibSearch("ConsecutivoCxp = " & valNumeroDocumento, "") Then
            valNumeroDocumento = CXP.GetNumero
         End If
         varDescripcion = varDescripcion & gEnumProyectoWincont.enumComprobanteGeneradoPorToString(valModuloAContabilizar) & " Nº " & _
               gTexto.DfMid(valNumeroDocumento, gTexto.DfInStr(valNumeroDocumento, gTexto.fSeparadorStandardDeElementosString) + 1) & _
               " Proveedor " & CXP.GetCodigoProveedor & " " & CXP.GetNombreProveedor
      Case eCG_PAGOS: varDescripcion = fBuscaDescripcionDelComprobanteContableDePago(valNumeroDocumento)
      Case eCG_CXC:
         numeroCxC = gTexto.DfMid(valNumeroDocumento, gTexto.DfInStr(valNumeroDocumento, gTexto.fSeparadorStandardDeElementosString) + 1)
         varDescripcion = fBuscaDescripcionDelComprobanteContableDeCxC(numeroCxC, valTipoDocumento)
      Case eCG_COBRANZA: varDescripcion = fBuscaDescripcionDelComprobanteContableDeCobranza(valNumeroDocumento)
      Case eCG_FACTURA:
         numeroFactura = gTexto.DfMid(valNumeroDocumento, gTexto.DfInStr(valNumeroDocumento, gTexto.fSeparadorStandardDeElementosString) + 1)
         varDescripcion = fBuscaDescripcionDelComprobanteContableDeFactura(numeroFactura, valTipoDocumento)
      Case eCG_RESUMEN_DIARIO_VENTAS: varDescripcion = varDescripcion & gEnumProyectoWincont.enumComprobanteGeneradoPorToString(valModuloAContabilizar) & " N° " & valNumeroDocumento
      Case eCG_ANTICIPO: varDescripcion = varDescripcion & fBuscaDescripcionDelComprobanteContableDeAnticipo(valNumeroDocumento, valTipoDocumento)
      Case eCG_MOVIMIENTO_BANCARIO: varDescripcion = varDescripcion & fBuscaDescripcionDelComprobanteContableDeMovBancario(valNumeroDocumento)
      Case eCG_PAGO_SUELDOS: varDescripcion = varDescripcion & fBuscaDescripcionDelComprobanteContableDePagoDeSueldos(valConsecutivoDocOrigen)
      Case Else: varDescripcion = varDescripcion & gEnumProyectoWincont.enumComprobanteGeneradoPorToString(valModuloAContabilizar) & " Nº " & valNumeroDocumento
   End Select
   Set CXP = Nothing
h_EXIT:
   fDescripcionComprobanteUnoPorUno = varDescripcion
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fDescripcionComprobanteUnoPorUno", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Sub sDesContabilizar(ByVal valAction As AccionSobreRecord, ByVal valModuloAContabilizar As enum_ComprobanteGeneradoPor, _
         ByVal valNumeroDocumento As String, ByVal valFechaBuscada As Date, ByVal valConsecutivoDocumento As Long, _
         ByVal valStringField As Boolean, Optional ByVal valFechaDeAnulacion As Date)
   Dim insComprobanteContable As clsComprobanteNavigator
   Dim Continuar As Boolean
   Dim ElComprobanteYaExiste As Boolean
   Dim otherUserUpdatedFirst As Boolean
   On Error GoTo h_ERROR
   If Not gProyCompaniaActual.GetTieneAccesoACaracteristicaContabilidadActiva Then
      GoTo h_EXIT
   End If
   If valFechaBuscada < gProyParametrosCompania.GetFechaDeInicioContabilizacion Then
      GoTo h_EXIT
   End If
   If (valAction = Eliminar) Or (valAction = Anular) Then
      If (Not gProyUsuarioActual.GetUsaContabilidad) And gProyReglasDeContabilizacion.fBuscaElTipoDeContabilizacion(valModuloAContabilizar) = eTD_POR_LOTE Then
         GoTo h_EXIT
      End If
      Set insComprobanteContable = New clsComprobanteNavigator
      If (valAction = Eliminar) Then
         Continuar = insComprobanteContable.fEliminarElComprobanteDeContabilizacionAutomatica(valModuloAContabilizar, valFechaBuscada, valNumeroDocumento, valConsecutivoDocumento, valStringField)  'para revision(ConsecutivoDocumento para Comprobante -> 0)
      ElseIf (valAction = Anular) Then
         Continuar = insComprobanteContable.fElComprobanteDeContabilizacionAutomaticaYaExiste(valModuloAContabilizar, valFechaBuscada, valNumeroDocumento, False, valConsecutivoDocumento, True)   'para revision(ConsecutivoDocumento para Comprobante -> 0,True)
         If Continuar Then

            If valModuloAContabilizar = eCG_PAGO_SUELDOS Then
               sGenerarComprobanteDeAnulacionDeCobranzaOPago valModuloAContabilizar, valNumeroDocumento, valConsecutivoDocumento, True, valFechaDeAnulacion
            ElseIf valModuloAContabilizar = eCG_COBRANZA Or valModuloAContabilizar = eCG_PAGOS Then
               sGenerarComprobanteDeAnulacionDeCobranzaOPago valModuloAContabilizar, valNumeroDocumento, 0, True, valFechaDeAnulacion
            ElseIf valModuloAContabilizar = eCG_MOVIMIENTO_BANCARIO Then
               sGenerarComprobanteDeAnulacionDeMovBancario valModuloAContabilizar, valNumeroDocumento, 0, True, valFechaDeAnulacion
            ElseIf valModuloAContabilizar = eCG_REPOSICION Then
               sGenerarComprobanteDeAnulacionDeRendicion valModuloAContabilizar, valNumeroDocumento, 0, True, valFechaDeAnulacion, eCG_ANULACION_REPOSICION
            Else
               insComprobanteContable.sColocaEnCeroTodosLosAsientosDelComprobante
               insComprobanteContable.sColocaDescripcionDeAnuladoAlComprobante insComprobanteContable.GetDescripcion
            End If
         End If
      End If
      Set insComprobanteContable = Nothing
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sDesContabilizar", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Sub sContabilizaDocumento(ByVal valConMensaje As Boolean, ByVal valAction As AccionSobreRecord, _
         ByVal valModulo As enum_ComprobanteGeneradoPor, ByVal valFecha As Date, ByVal valNumero As String, _
         ByVal valEsNotaDeCredito As Boolean, ByVal valGeneraElMovimientoBancario As Boolean, _
         ByVal valTipoDocumento As Integer, ByVal valConsecutivoDoc As Long, ByVal valTipoAlicPorContIGTF As enum_TipoAlicPorContIGTF)
   Dim generarComprobanteNavigator As clsGenerarComprobanteNavigator
   Dim Continuar As Boolean
   Dim tipoDeContabilizacion As enum_TipoDeContabilizacion
   Dim TipoContabilizacionIndividual As enum_ContabilizacionIndividual
   Dim insComprobanteContable As clsComprobanteNavigator
   Dim ElComprobanteYaExiste As Boolean
   Dim NumeroBuscado As String
   Dim refOtherUserUpdatedFirst As Boolean
   On Error GoTo h_ERROR
   ElComprobanteYaExiste = False
   
   If Not gProyCompaniaActual.GetTieneAccesoACaracteristicaContabilidadActiva Then
      GoTo h_EXIT
   End If
   If valFecha < gProyParametrosCompania.GetFechaDeInicioContabilizacion Then
      GoTo h_EXIT
   End If
   Set generarComprobanteNavigator = New clsGenerarComprobanteNavigator
   Set insComprobanteContable = New clsComprobanteNavigator
   tipoDeContabilizacion = gProyReglasDeContabilizacion.fBuscaElTipoDeContabilizacion(valModulo)
   If (Not gProyUsuarioActual.GetUsaContabilidad) And tipoDeContabilizacion = eTD_POR_LOTE Then
      GoTo h_EXIT
   End If
   Set generarComprobanteNavigator = New clsGenerarComprobanteNavigator
   If mImprimirEspecialAlGrabarComprobante Then
      generarComprobanteNavigator.setProcesoDeImpresionEspecial mPagoProcesoDeImpresionEspecial
   End If
   If mImprimeComprobanteDeMovBancarioAlGrabar Then
      generarComprobanteNavigator.setProcesoDeImpresionDeComprobanteDeMovBancario mPagoProcesoDeImpresionEspecial, mNumeroDeMovBancario
   End If
      
   If mImprimirEspecialAlGrabarComprobanteSueldo Then
     generarComprobanteNavigator.setProcesoDeImpresionDeComprobanteDePagoSueldo mPagoProcesoDeImpresionEspecial, mNumeroDeMovBancario

   End If
   If tipoDeContabilizacion = eTD_CADA_DOCUMENTO Then
      TipoContabilizacionIndividual = gProyReglasDeContabilizacion.fBuscaElTipoDeContabilizacionIndividual(valModulo)
      If (valAction = Contabilizar) Then
         Continuar = True
      ElseIf (valAction = insertar) And (TipoContabilizacionIndividual = eCI_INMEDIATA) Then
         Continuar = True
      ElseIf (valAction = Modificar) And (TipoContabilizacionIndividual = eCI_INMEDIATA) Then
         Continuar = True
         If insComprobanteContable.fElComprobanteDeContabilizacionAutomaticaYaExiste(valModulo, valFecha, valNumero, False, 0, True) Then 'para revision(ConsecutivoDocumento para Comprobante -> 0,True)
            ElComprobanteYaExiste = True
            If insComprobanteContable.GetFueModificado And gProyUsuarioActual.GetUsaContabilidad Then
               gMessage.Advertencia "El Comprobante que corresponde al " & _
                  "documento número " & valNumero & " del módulo " & gEnumProyectoWincont.enumComprobanteGeneradoPorToString(valModulo) & _
                  " ha sido modificado usando el Módulo de Comprobantes Contables. " & vbCr & vbCr & "Los asientos del documento de  " & gEnumProyectoWincont.enumComprobanteGeneradoPorToString(valModulo) & _
                  " modificado serán generados de nuevo por lo que los cambios realizados al comprobante se perderán y, si lo desea, los debe volver a realizar."
            End If
         End If
      ElseIf (valAction = Modificar) And (TipoContabilizacionIndividual = eCI_POSPUESTA) Then
         sDesContabilizar Eliminar, valModulo, valNumero, valFecha, 0, True
      End If
   End If
   If Continuar Then
      If Not gContPeriodoActual.fBuscaElPeriodoDeFechaYsiExisteLoColocaComoPeriodoActual(valFecha, gProyUsuarioActual.GetUsaContabilidad) Then
         GoTo h_EXIT
      End If
      If Not gProyReglasDeContabilizacion.fLasCuentasDeReglasDeContabilizacionEstanCompletas(valModulo, gProyUsuarioActual.GetUsaContabilidad, valTipoDocumento) Then
         GoTo h_EXIT
      End If
      If ElComprobanteYaExiste Then
         If Not insComprobanteContable.fEliminaLosAsientosDelComprobante Then
            GoTo h_EXIT
         End If
      End If
      
      If generarComprobanteNavigator.fGeneraElComprobanteParaUnoOVariosDocumentos(valFecha, valFecha, valModulo, True, _
            valNumero, valEsNotaDeCredito, ElComprobanteYaExiste, valGeneraElMovimientoBancario, valTipoDocumento, valConsecutivoDoc, valTipoAlicPorContIGTF) Then
         If (valConMensaje And Not gProyReglasDeContabilizacion.fEditaComprobanteDespuesDeInsertar(valModulo)) _
            And gProyUsuarioActual.GetUsaContabilidad Then
            gMessage.exito "Contabilización del Documento No. " & valNumero & " del Módulo de " & gEnumProyectoWincont.enumComprobanteGeneradoPorToString(valModulo) & " ha sido realizada con éxito."
         End If
      End If
   End If
   Set generarComprobanteNavigator = Nothing
   Set insComprobanteContable = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sContabilizaDocumento", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Public Function fValidaDocumento(ByVal valConMensaje As Boolean, ByVal valAction As AccionSobreRecord, _
         ByVal valModulo As enum_ComprobanteGeneradoPor, ByVal valFecha As Date, ByVal valNumero As String, _
         ByVal valEsNotaDeCredito As Boolean, ByVal valGeneraElMovimientoBancario As Boolean, ByVal valTipoDocumento As Integer, ByVal valConsecutivoDoc As Long) As Boolean
    Dim generarComprobanteNavigator As clsGenerarComprobanteNavigator
   Dim Continuar As Boolean
   Dim tipoDeContabilizacion As enum_TipoDeContabilizacion
   Dim TipoContabilizacionIndividual As enum_ContabilizacionIndividual
   Dim insComprobanteContable As clsComprobanteNavigator
   Dim ElComprobanteYaExiste As Boolean
   Dim NumeroBuscado As String
   Dim refOtherUserUpdatedFirst As Boolean
   Dim result As Boolean
   On Error GoTo h_ERROR
   ElComprobanteYaExiste = False
   result = True
   If Not gProyCompaniaActual.GetTieneAccesoACaracteristicaContabilidadActiva Then
      result = False
   End If
   If valFecha < gProyParametrosCompania.GetFechaDeInicioContabilizacion Then
        result = False
   End If
   Set generarComprobanteNavigator = New clsGenerarComprobanteNavigator
   Set insComprobanteContable = New clsComprobanteNavigator
   tipoDeContabilizacion = gProyReglasDeContabilizacion.fBuscaElTipoDeContabilizacion(valModulo)
   If (Not gProyUsuarioActual.GetUsaContabilidad) And tipoDeContabilizacion = eTD_POR_LOTE Then
        result = False
   End If
   Set generarComprobanteNavigator = New clsGenerarComprobanteNavigator
   If mImprimirEspecialAlGrabarComprobante Then
      generarComprobanteNavigator.setProcesoDeImpresionEspecial mPagoProcesoDeImpresionEspecial
   End If
   If mImprimeComprobanteDeMovBancarioAlGrabar Then
      generarComprobanteNavigator.setProcesoDeImpresionDeComprobanteDeMovBancario mPagoProcesoDeImpresionEspecial, mNumeroDeMovBancario
   End If
   If mImprimirEspecialAlGrabarComprobanteSueldo Then
     generarComprobanteNavigator.setProcesoDeImpresionDeComprobanteDePagoSueldo mPagoProcesoDeImpresionEspecial, mNumeroDeMovBancario
   End If
   If tipoDeContabilizacion = eTD_CADA_DOCUMENTO Then
      TipoContabilizacionIndividual = gProyReglasDeContabilizacion.fBuscaElTipoDeContabilizacionIndividual(valModulo)
      If (valAction = Contabilizar) Then
         Continuar = True
      ElseIf (valAction = insertar) And (TipoContabilizacionIndividual = eCI_INMEDIATA) Then
         Continuar = True
      ElseIf (valAction = Modificar) And (TipoContabilizacionIndividual = eCI_INMEDIATA) Then
         Continuar = True
         If insComprobanteContable.fElComprobanteDeContabilizacionAutomaticaYaExiste(valModulo, valFecha, valNumero, False, 0, True) Then 'para revision(ConsecutivoDocumento para Comprobante -> 0,True)
            ElComprobanteYaExiste = True
            If insComprobanteContable.GetFueModificado And gProyUsuarioActual.GetUsaContabilidad Then
               gMessage.Advertencia "El Comprobante que corresponde al " & _
                  "documento número " & valNumero & " del módulo " & gEnumProyectoWincont.enumComprobanteGeneradoPorToString(valModulo) & _
                  " ha sido modificado usando el Módulo de Comprobantes Contables. " & vbCr & vbCr & "Los asientos del documento de  " & gEnumProyectoWincont.enumComprobanteGeneradoPorToString(valModulo) & _
                  " modificado serán generados de nuevo por lo que los cambios realizados al comprobante se perderán y, si lo desea, los debe volver a realizar."
            End If
         End If
      ElseIf (valAction = Modificar) And (TipoContabilizacionIndividual = eCI_POSPUESTA) Then
         sDesContabilizar Eliminar, valModulo, valNumero, valFecha, 0, True
      End If
   End If
   If Continuar Then
      If Not gContPeriodoActual.fBuscaElPeriodoDeFechaYsiExisteLoColocaComoPeriodoActual(valFecha, gProyUsuarioActual.GetUsaContabilidad) Then
           result = False
      End If
      If gContPeriodoActual.fLaFechaPerteneceAUnPeriodoCerrado(valFecha, True) Then
         result = False
      End If
      
      If gContPeriodoActual.fLaFechaPerteneceAunMesCerrado(valFecha, False) Then
          result = False
         gMessage.Advertencia "No se puede ejecutar la acción " & gDefgen.AccionSobreRecordStr(valAction) & ". La Fecha corresponde a un Mes Cerrado."
      End If

      
      If Not gProyReglasDeContabilizacion.fLasCuentasDeReglasDeContabilizacionEstanCompletas(valModulo, gProyUsuarioActual.GetUsaContabilidad, valTipoDocumento) Then
           result = False
      End If
      If ElComprobanteYaExiste Then
         If Not insComprobanteContable.fEliminaLosAsientosDelComprobante Then
              result = False
         End If
      End If
   End If
   Set generarComprobanteNavigator = Nothing
   Set insComprobanteContable = Nothing
   fValidaDocumento = result
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sContabilizaDocumento", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
'***Carnavalito
Private Function fConstruyeSQLsegunElModuloAContabilizar(ByVal valFechaDesde As Date, ByVal valFechaHasta As Date, _
            ByVal valModuloAContabilizar As enum_ComprobanteGeneradoPor, ByVal valEsUnReverso As Boolean, _
            ByVal valDescripcionDelAsiento As String, ByVal valContabilizarSolo1Documento As Boolean, _
            ByVal valNoDocumentoOrigen As String, ByVal valGeneraElMovimientoBancario As Boolean, _
            ByVal valTipoDocumento As Integer, ByRef valRegla As String, ByVal valConsecutivoDocumentoOrigen As Long, ByVal valTipoAlicPorContIGTF As enum_TipoAlicPorContIGTF) As String
   Dim insArticulo As clsArticuloInventarioNavigator
   Dim Sql As String
   Dim insComprobanteSQL As clsComprobanteSQL
   On Error GoTo h_ERROR
   Set insArticulo = New clsArticuloInventarioNavigator
   Set insComprobanteSQL = New clsComprobanteSQL
   Select Case valModuloAContabilizar
      Case eCG_CXC:
         Sql = fSQLCxCEntreFechas(valFechaDesde, valFechaHasta, valEsUnReverso, valDescripcionDelAsiento, valContabilizarSolo1Documento, valNoDocumentoOrigen, valTipoDocumento)
         valRegla = gProyReglasDeContabilizacion.GetCuentaCxCClientes
      Case eCG_FACTURA:
         If gProyReglasDeContabilizacion.GetContabilizarPorArticulo Then
            Sql = fSQLFacturasEntreFechasPorArticulo(valFechaDesde, valFechaHasta, valEsUnReverso, valDescripcionDelAsiento, valContabilizarSolo1Documento, valNoDocumentoOrigen, valTipoDocumento)
         Else
            Sql = fSQLFacturasEntreFechas(valFechaDesde, valFechaHasta, valEsUnReverso, valDescripcionDelAsiento, valContabilizarSolo1Documento, valNoDocumentoOrigen, valTipoDocumento)
         End If
         valRegla = gProyReglasDeContabilizacion.GetCuentaFacturacionCxCClientes
      Case eCG_CXP:
         Sql = fSQLCxPEntreFechas(valFechaDesde, valFechaHasta, valEsUnReverso, valDescripcionDelAsiento, valContabilizarSolo1Documento, valNoDocumentoOrigen, valTipoDocumento, valConsecutivoDocumentoOrigen)
         valRegla = gProyReglasDeContabilizacion.GetCuentaCxPProveedores
      Case eCG_COBRANZA:
         Sql = fSQLCobranzasEntreFechas(valFechaDesde, valFechaHasta, valEsUnReverso, valDescripcionDelAsiento, valContabilizarSolo1Documento, valNoDocumentoOrigen, valGeneraElMovimientoBancario, valTipoDocumento)
         valRegla = gProyReglasDeContabilizacion.GetCuentaCobranzaCxCClientes
      Case eCG_PAGOS:
         Sql = fSQLPagosEntreFechas(valFechaDesde, valFechaHasta, valEsUnReverso, valDescripcionDelAsiento, valContabilizarSolo1Documento, valNoDocumentoOrigen, valGeneraElMovimientoBancario, valTipoDocumento)
         valRegla = gProyReglasDeContabilizacion.GetCuentaPagosCxPProveedores
      Case eCG_MOVIMIENTO_BANCARIO:
         If valEsUnReverso Then
            Sql = ""
         Else
            Sql = fSQLMovimientoBancariosEntreFechas(valFechaDesde, valFechaHasta, valEsUnReverso, valDescripcionDelAsiento, valContabilizarSolo1Documento, valNoDocumentoOrigen, valTipoDocumento)
         End If
      Case eCG_RESUMEN_DIARIO_VENTAS
         If gProyReglasDeContabilizacion.GetContabilizarPorArticuloRDVtas Then
            Sql = fSQLResumenVentasEntreFechasPorArticulo(valFechaDesde, valFechaHasta, valEsUnReverso, valDescripcionDelAsiento, valContabilizarSolo1Documento, valNoDocumentoOrigen, valTipoDocumento)
            valRegla = gProyReglasDeContabilizacion.GetCuentaFacturacionCxCClientes
         Else
            Sql = fSQLResumenVentasEntreFechas(valFechaDesde, valFechaHasta, valEsUnReverso, valDescripcionDelAsiento, valContabilizarSolo1Documento, valNoDocumentoOrigen, valTipoDocumento)
         End If
      Case eCG_ANTICIPO:
         Sql = fSQLAnticiposEntreFechas(valFechaDesde, valFechaHasta, valEsUnReverso, valDescripcionDelAsiento, valContabilizarSolo1Documento, valNoDocumentoOrigen, valTipoDocumento)
         If valTipoDocumento = enum_TipoDeAnticipo.eTDA_COBRADO Then
            valRegla = gProyReglasDeContabilizacion.GetCuentaAnticipoCobrado
         ElseIf valTipoDocumento = enum_TipoDeAnticipo.eTDA_PAGADO Then
            valRegla = gProyReglasDeContabilizacion.GetCuentaAnticipoPagado
         End If
      Case eCG_INVENTARIO:
         Sql = insComprobanteSQL.fSQLCostoDeVenta(gProyCompaniaActual.GetConsecutivoCompania, valFechaDesde, valFechaHasta, valEsUnReverso, gProyParametrosCompania.GetComprobanteCostoDetallado, gProyReglasDeContabilizacion.GetAgruparPorCuentaDeArticuloInven, gProyCompaniaActual, gProyParametrosCompania, gProyReglasDeContabilizacion, valNoDocumentoOrigen, "Factura", insArticulo.fFechaReporteCostoPromedio(valFechaDesde, True))
      Case eCG_PAGO_SUELDOS:
         Sql = fSqlComprobantePagoSueldos(gProyCompaniaActual.GetConsecutivoCompania, valNoDocumentoOrigen, valDescripcionDelAsiento, valEsUnReverso)
         valRegla = gProyReglasDeContabilizacion.GetCtaDePagosSueldos
     Case eCG_REPOSICION:
         Sql = fSQLCajaChicaEntreFechas(valFechaDesde, valFechaHasta, valEsUnReverso, valDescripcionDelAsiento, valContabilizarSolo1Documento, valNoDocumentoOrigen, valTipoDocumento, valConsecutivoDocumentoOrigen, valTipoAlicPorContIGTF)
         valRegla = gProyReglasDeContabilizacion.GetCuentaCajaChicaGasto
   End Select
   Set insArticulo = Nothing
   Set insComprobanteSQL = Nothing
   
h_EXIT:
   fConstruyeSQLsegunElModuloAContabilizar = Sql
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fConstruyeSQLsegunElModuloAContabilizar", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fTipoDeComprobante(ByVal valModuloAContabilizar As enum_ComprobanteGeneradoPor) As String
   Dim varTipo As String
   On Error GoTo h_ERROR
   varTipo = "03"
   If gContPeriodoActual.GetTipoDeNumeracionAsEnum = eTN_TIPOMESCONSECUTIVO Then
      Select Case valModuloAContabilizar
         Case eCG_CXC: varTipo = gProyReglasDeContabilizacion.GetCxCTipoComprobante
         Case eCG_FACTURA: varTipo = gProyReglasDeContabilizacion.GetFacturaTipoComprobante
         Case eCG_CXP: varTipo = gProyReglasDeContabilizacion.GetCxPTipoComprobante
         Case eCG_COBRANZA: varTipo = gProyReglasDeContabilizacion.GetCobranzaTipoComprobante
         Case eCG_PAGOS: varTipo = gProyReglasDeContabilizacion.GetPagoTipoComprobante
         Case eCG_MOVIMIENTO_BANCARIO: varTipo = gProyReglasDeContabilizacion.GetMovimientoBancarioTipoComprobante
         Case eCG_RESUMEN_DIARIO_VENTAS: varTipo = gProyReglasDeContabilizacion.GetFacturaTipoComprobante
         Case eCG_ANTICIPO: varTipo = gProyReglasDeContabilizacion.GetAnticipoTipoComprobante
         Case eCG_INVENTARIO:  varTipo = gProyReglasDeContabilizacion.GetInventarioTipoComprobante
         Case eCG_REPOSICION:  varTipo = gProyReglasDeContabilizacion.GetSiglasTipoComprobanteCajaChica
      End Select
   End If
h_EXIT:
   fTipoDeComprobante = varTipo
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fTipoDeComprobante", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fEscogeCuentaDeIVASiEsDebitoOcredito(ByVal valDebeOHaber As String, ByVal valTipoDeAlicuota As enum_TipoDeAlicuota, _
         ByVal valGeneradoPor As enum_ComprobanteGeneradoPor, ByRef refUsaAuxiliares As Boolean, Optional valEsVentaDiferida As Boolean = False) As String
   Dim CuentaEscogida As String
   Dim LaCuentaEnTipoDebe As Boolean
   On Error GoTo h_ERROR
   refUsaAuxiliares = False
   LaCuentaEnTipoDebe = fEsDebe(valDebeOHaber)
   If valEsVentaDiferida Then
      CuentaEscogida = gProyReglasDeContabilizacion.GetCuentaFacturacionIvaDiferido
      refUsaAuxiliares = gProyReglasDeContabilizacion.getCuentaFacturacionIvaDiferidoUsaAuxiliares
   Else
      If LaCuentaEnTipoDebe Then
         CuentaEscogida = gProyReglasDeContabilizacion.GetCuentaIva1Debito
         refUsaAuxiliares = gProyReglasDeContabilizacion.GetCuentaIva1DebitoUsaAuxiliares
      Else
         CuentaEscogida = gProyReglasDeContabilizacion.GetCuentaIva1Credito
         refUsaAuxiliares = gProyReglasDeContabilizacion.GetCuentaIva1CreditoUsaAuxiliares
      End If
   End If
h_EXIT:
   fEscogeCuentaDeIVASiEsDebitoOcredito = CuentaEscogida
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fEscogeCuentaDeIVASiEsDebitoOcredito", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fColocaAuxiliarSiAplica(ByVal valLaCuentaUsaAuxiliares As Boolean, ByVal valCodigoDelAuxiliar As String) As String
   Dim ColocarAuxiliar As Boolean
   On Error GoTo h_ERROR
   ColocarAuxiliar = False
   If gProyCompaniaActual.GetUsaAuxiliares Then
      If valLaCuentaUsaAuxiliares Then
         ColocarAuxiliar = True
      End If
   End If
   If ColocarAuxiliar Then
      fColocaAuxiliarSiAplica = valCodigoDelAuxiliar & ", "
   Else
      fColocaAuxiliarSiAplica = "'NA', "
   End If
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fColocaAuxiliarSiAplica", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fAgregaUnaCuentaAlSQL(ByVal valDebeOHaber As String, ByVal valEsUnReverso As Boolean, _
            ByVal valMontoDeLaCuenta As String, ByVal valCodigoDeLaCuenta As String, _
            ByVal valLaCuentaUsaAuxiliares As Boolean, ByVal valCodigoDelAuxiliar As String, _
            ByVal valNoDocumentoOrigen As String, ByVal valDescripcionDelAsiento As String, _
            ByVal valCondicionDeStatusAnulado As String, ByVal valCodigoCentroDeCosto As String) As String
   Dim Sql As String
   On Error GoTo h_ERROR
   
   Sql = gUtilSQL.fSimpleSqlValue(fCambiaDebeHaberSiEsUnReverso(valDebeOHaber, valEsUnReverso)) & ", "
   If valMontoDeLaCuenta = "" Then
      Sql = Sql & "'', "
   ElseIf valCondicionDeStatusAnulado <> "" Then
      Sql = Sql & gUtilSQL.getIIF(valCondicionDeStatusAnulado, "0", valMontoDeLaCuenta) & ", "
   Else
      Sql = Sql & valMontoDeLaCuenta & ", "
   End If
   If gTexto.DfInStr(valCodigoDeLaCuenta, gUtilSQL.getPrefijoIIF) <> 0 Then
      Sql = Sql & valCodigoDeLaCuenta & ", "
   Else
      Sql = Sql & "'" & valCodigoDeLaCuenta & "' , "
   End If
   Sql = Sql & fColocaAuxiliarSiAplica(valLaCuentaUsaAuxiliares, valCodigoDelAuxiliar)
   If (gProyCompaniaActual.GetUsaCentroDeCostos And gProyParametrosCompania.GetAsociaCentroDeCostoyAlmacen) And valCodigoCentroDeCosto <> "" Then
        Sql = Sql & valCodigoCentroDeCosto & ", "
   Else
   Sql = Sql & gUtilSQL.fSimpleSqlValue("NA") & ", "
   End If
   If gTexto.DfLeft(valNoDocumentoOrigen, 1) <> vbTab Then
         Sql = Sql & gUtilSQL.fSimpleSqlValue(valNoDocumentoOrigen) & ", "
   Else
      valNoDocumentoOrigen = gTexto.fLimpiaStringDeCharAAmbosLados(valNoDocumentoOrigen, vbTab)
      Sql = Sql & valNoDocumentoOrigen & ", "
   End If
   Sql = Sql & gUtilSQL.fSimpleSqlValue(valDescripcionDelAsiento)
   fAgregaUnaCuentaAlSQL = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fAgregaUnaCuentaAlSQL", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLCxCEntreFechas(ByVal valFechaDesde As Date, ByVal valFechaHasta As Date, _
            ByVal valEsUnReverso As Boolean, ByVal valDescripcionDelAsiento As String, _
            ByVal valContabilizarSolo1Documento As Boolean, ByVal valNoDocumentoOrigen As String, _
            ByVal valTipoDocumento As Integer) As String
   Dim ConjuntoDeCxCsinContabilizar As String
   Dim DebeOhaber As String
   Dim insCxC As clsCxCNavigator
   Dim ColocarAuxiliar As Boolean
   Dim Sql As String
   Dim codigoDelAuxiliar As String
   Dim CodigoCuentaIVA As String
   Dim CodigoCuentaIVAUsaAuxiliar As Boolean
   Dim SqlTasaDeCambio As String
   Dim CondicionDeStatusAnulado As String
   Dim insCliente As clsClienteNavigator
   Dim varSQLCuentaContableCliente As String
   Dim varSQLCuentaContableIngresos As String
   Dim varAuxiliarCxC As Boolean
   Dim varAuxiliarIngreso As Boolean
   Dim varCodigoCliente As String
   Dim numeroReferencia As String
   On Error GoTo h_ERROR
   CodigoCuentaIVA = ""
   Sql = ""
   Set insCxC = New clsCxCNavigator
   Set insCliente = New clsClienteNavigator
   varCodigoCliente = fBuscaCodClienteSegunModulo(valNoDocumentoOrigen, eCG_CXC, valTipoDocumento)
   varAuxiliarCxC = fElClienteManejaAuxiliar(varCodigoCliente, eCG_CXC, gProyReglasDeContabilizacion.eTC_ReglaCliCxC, valTipoDocumento)
   varAuxiliarIngreso = fElClienteManejaAuxiliar(varCodigoCliente, eCG_CXC, gProyReglasDeContabilizacion.eTC_ReglaCliIngreso, valTipoDocumento)
   SqlTasaDeCambio = insCxC.GetTableName & ".CambioABolivares"
   CondicionDeStatusAnulado = gUtilSQL.fFirst(insCxC.GetTableName() & ".Status", "NONE") & " = '" & gConvert.enumerativoAChar(enum_StatusDocumento.eSD_ANULADO) & "'"
   numeroReferencia = valNoDocumentoOrigen
   If valContabilizarSolo1Documento Then
      gTexto.ReemplazaCaracteresEnElString numeroReferencia, vbTab, "-"
   End If
   ConjuntoDeCxCsinContabilizar = fSQLConjuntoRecordsDeContabilizacion1oMasDocumentos(eCG_CXC, valContabilizarSolo1Documento, valNoDocumentoOrigen, valFechaDesde, valFechaHasta, valTipoDocumento)
   If ConjuntoDeCxCsinContabilizar = "" Then
      GoTo h_EXIT
   End If
   varSQLCuentaContableCliente = gUtilSQL.getIIF(gUtilSQL.fFirst(insCliente.GetTableName() & ".CuentaContableCxC", "NONE") & " <> ''", _
               gUtilSQL.fFirst(insCliente.GetTableName() & ".CuentaContableCxC", "NONE"), _
               "'" & gProyReglasDeContabilizacion.GetCuentaCxCClientes & "'")
   varSQLCuentaContableIngresos = gUtilSQL.getIIF(gUtilSQL.fFirst(insCliente.GetTableName() & ".CuentaContableIngresos", "NONE") & " <> ''", _
               gUtilSQL.fFirst(insCliente.GetTableName() & ".CuentaContableIngresos", "NONE"), _
               "'" & gProyReglasDeContabilizacion.GetCuentaCxCIngresos & "'")
   codigoDelAuxiliar = insCxC.GetTableName() & ".CodigoCliente"
   Sql = "SELECT " & insCxC.GetTableName() & ".CodigoCliente, "
   Sql = Sql & fAgregaUnaCuentaAlSQL("DEBE", valEsUnReverso, _
               "SUM((" & insCxC.GetTableName() & ".MontoExento + " & _
                        insCxC.GetTableName() & ".MontoGravado + " & _
                        insCxC.GetTableName() & ".MontoIVA) * " & _
                  SqlTasaDeCambio & ")", varSQLCuentaContableCliente, varAuxiliarCxC, codigoDelAuxiliar, _
                  numeroReferencia, valDescripcionDelAsiento, CondicionDeStatusAnulado, "") & ", "
   CodigoCuentaIVA = fEscogeCuentaDeIVASiEsDebitoOcredito(fCambiaDebeHaberSiEsUnReverso("HABER", valEsUnReverso), _
                                 eTD_ALICUOTAGENERAL, eCG_CXC, CodigoCuentaIVAUsaAuxiliar)
   Sql = Sql & fAgregaUnaCuentaAlSQL("HABER", valEsUnReverso, _
                  "SUM(" & insCxC.GetTableName() & ".MontoIVA * " & SqlTasaDeCambio & ")", _
                  CodigoCuentaIVA, CodigoCuentaIVAUsaAuxiliar, codigoDelAuxiliar, _
                  numeroReferencia, valDescripcionDelAsiento, CondicionDeStatusAnulado, "") & ", "
   Sql = Sql & fAgregaUnaCuentaAlSQL("HABER", valEsUnReverso, _
                  "SUM((" & insCxC.GetTableName() & ".MontoExento + " & _
                     insCxC.GetTableName() & ".MontoGravado) * " & SqlTasaDeCambio & ")", _
                  varSQLCuentaContableIngresos, varAuxiliarIngreso, codigoDelAuxiliar, _
                  numeroReferencia, valDescripcionDelAsiento, CondicionDeStatusAnulado, "")
   Sql = Sql & " FROM " & insCliente.GetTableName & " INNER JOIN "
   Sql = Sql & insCxC.GetTableName & " ON "
   Sql = Sql & insCliente.GetTableName & ".ConsecutivoCompania = "
   Sql = Sql & insCxC.GetTableName & ".ConsecutivoCompania AND "
   Sql = Sql & insCliente.GetTableName & ".Codigo = "
   Sql = Sql & insCxC.GetTableName & ".CodigoCliente"
   Sql = Sql & " AND " & insCxC.GetTableName & ".TipoCxC" & gUtilSQL.CharConcat & "'" & gTexto.fSeparadorStandardDeElementosString & "'" & gUtilSQL.CharConcat & insCxC.GetTableName & ".Numero IN (" & ConjuntoDeCxCsinContabilizar & ")"
   Sql = Sql & " AND (" & insCxC.GetTableName() & ".Fecha BETWEEN " & _
         gUtilSQL.fDateToSQLValue(valFechaDesde) & " AND " & _
         gUtilSQL.fDateToSQLValue(valFechaHasta) & ")"
   Sql = Sql & " WHERE " & insCxC.GetTableName & ".ConsecutivoCompania = " & gProyParametrosCompania.GetConsecutivoCompania
   Sql = Sql & " GROUP BY " & insCxC.GetTableName() & ".CodigoCliente"
      Set insCxC = Nothing
   Set insCliente = Nothing
h_EXIT:
   fSQLCxCEntreFechas = Sql
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLCxCEntreFechas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fConjuntoDeImpTranBancariaAsociadosACobranza(ByVal valConjuntoDeCobranzasSinContabilizar As String) As String
   Dim Sql As String
   Dim ConjuntoDeRecords As String
   On Error GoTo h_ERROR
   Sql = "SELECT movimientoBancario.ConsecutivoMovimiento FROM movimientoBancario"
   Sql = Sql & " WHERE movimientoBancario.ConsecutivoCompania = " & gProyCompaniaActual.GetConsecutivoCompania
   Sql = Sql & " AND movimientoBancario.NroMovimientoRelacionado IN (" & valConjuntoDeCobranzasSinContabilizar & ")"
   Sql = Sql & " AND movimientoBancario.GeneradoPor = " & gUtilSQL.fSQLSimpleValueForEnum(enum_GeneradoPor.eGP_COBRANZA)
   ConjuntoDeRecords = gDbUtil.fBuildResultSetAsString(Sql)
h_EXIT:
   fConjuntoDeImpTranBancariaAsociadosACobranza = ConjuntoDeRecords
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConjuntoDeImpTranBancariaAsociadosACobranza", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Private Function fGetTasaCambioCobranza(ByVal valConjuntoDeCobranzasSinContabilizar As String, ByVal valFechaDesde As Date, ByVal valFechaHasta As Date) As Currency
   Dim RsTasaCambioCobranza As ADODB.Recordset
   Dim vSQL As String
   Dim vTasaCambioCobranza As Currency
   On Error GoTo h_ERROR
   vTasaCambioCobranza = 1
   Set RsTasaCambioCobranza = New ADODB.Recordset
   vSQL = "SELECT TOP 1 CambioAbolivares FROM Cobranza "
   vSQL = vSQL & " WHERE Cobranza.ConsecutivoCompania = " & gProyCompaniaActual.GetConsecutivoCompania
   vSQL = vSQL & " AND Cobranza.Numero IN (" & valConjuntoDeCobranzasSinContabilizar & ")"
   vSQL = vSQL & " AND " & gUtilSQL.DfSQLDateValueBetween(" Cobranza.Fecha ", valFechaDesde, valFechaHasta)
      
   If gDbUtil.fOpenRecordset(RsTasaCambioCobranza, vSQL, gDefDatabase.Conexion) Then
      If gDbUtil.fRecordCount(RsTasaCambioCobranza) > 0 Then
         vTasaCambioCobranza = gConvert.fConvertStringToCurrency(RsTasaCambioCobranza.Fields("CambioAbolivares").value)
      End If
   End If
   Set RsTasaCambioCobranza = Nothing
h_EXIT: On Error GoTo 0
   fGetTasaCambioCobranza = vTasaCambioCobranza
   Exit Function
h_ERROR:
   Err.Raise Err.Number, Err.Source, gError.fAddInteropMethodToStackTrace(Err.Description, CM_FILE_NAME, "fGetListaParametrosGeneralesFromAOS", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError, Err.Source)
End Function

Private Function fSQLCobranzasEntreFechas(ByVal valFechaDesde As Date, ByVal valFechaHasta As Date, _
            ByVal valEsUnReverso As Boolean, ByVal valDescripcionDelAsiento As String, ByVal valContabilizarSolo1Documento As Boolean, _
            ByVal valNoDocumentoOrigen As String, ByVal valGeneraElMovimientoBancario As Boolean, ByVal valTipoDocumento As Integer) As String
   Dim ConjuntoDeCobranzasSinContabilizar As String
   Dim DebeOhaber As String
   Dim insCobranza As clsCobranzaNavigator
   Dim ColocarAuxiliar As Boolean
   Dim codigoDelAuxiliar As String
   Dim Sql As String
   Dim CodigoCuentaIVA As String
   Dim SqlTasaDeCambio As String
   Dim TasaDeCambioDocumentos As String
   Dim insCuentaBancaria As clsCuentaBancariaNavigator
   Dim sqlSum As String
   Dim CondicionDeStatusAnulado As String
   Dim varCodigoCliente As String
   Dim varAuxiliarCxC As Boolean
   Dim insCliente As clsClienteNavigator
   Dim varSQLCuentaContableCliente As String
   Dim varSqlCuentaCobranzaCobradoEnCheque As String
   Dim varSqlCuentaCobranzaCobradoEnEfectivo As String
   Dim varSqlCuentaCobranzaCobradoEnTarjeta As String
   Dim varSqlCuentaCobranzaOtros As String
   Dim varCuentaBancoUsaAuxiliar As Boolean
   Dim varCuentadeMovimientoBancario As String
   Dim cuentaAnticipoCobrado As String
   Dim sqlRetencionISLR As String
   Dim sqlRetencionIVA As String
   Dim sqlCobradoAnticipos As String
   Dim sqlTotalDocs As String
   Dim sqlVuelto As String
   Dim SqlChequeEfectivoTarjeta As String
   Dim SqlOtros As String
   Dim SqlTarjeta As String
   Dim SqlEfectivo As String
   Dim SqlCheque As String
   Dim sqlSumMontoMov As String
   Dim sqlCuentaContableCuentaBancaria As String
   Dim vDescMovBancario As String
   Dim ConjuntoDeMovimientosBancariosAsociadosACobranza As String
   Dim DocumentosConIvaDiferido As String
   Dim sqlIvaDiferido As String
   Dim vDifCambiariaEnCobranza As Currency
   Dim vDescripcionAsientoDifCambiaria As String
   On Error GoTo h_ERROR
   CodigoCuentaIVA = ""
   Sql = ""
   Set insCobranza = New clsCobranzaNavigator
   Set insCuentaBancaria = New clsCuentaBancariaNavigator
   Set insCliente = New clsClienteNavigator
   
   varCodigoCliente = fBuscaCodClienteSegunModulo(valNoDocumentoOrigen, eCG_COBRANZA, valTipoDocumento)
   varAuxiliarCxC = fElClienteManejaAuxiliar(varCodigoCliente, eCG_COBRANZA, gProyReglasDeContabilizacion.eTC_ReglaCliCxC, valTipoDocumento)
   If valContabilizarSolo1Documento Then
      sqlSum = ""
   Else
      sqlSum = " SUM"
   End If
   ConjuntoDeCobranzasSinContabilizar = fSQLConjuntoRecordsDeContabilizacion1oMasDocumentos(eCG_COBRANZA, valContabilizarSolo1Documento, valNoDocumentoOrigen, valFechaDesde, valFechaHasta, valTipoDocumento)
   If ConjuntoDeCobranzasSinContabilizar = "" Then
      GoTo h_EXIT
   End If
   If insCobranza.fSearchByField("Numero", valNoDocumentoOrigen, False, True, False) Then
      If insCobranza.GetTipoDeDocumentoAsEnum = eTDDC_CobranzaDeFactura Then
         varSQLCuentaContableCliente = gUtilSQL.getIIF(insCliente.GetTableName() & ".CuentaContableCxC <> ''", insCliente.GetTableName() & ".CuentaContableCxC", "'" & gProyReglasDeContabilizacion.GetCuentaCobranzaCxCClientes & "'")
      Else
         varSQLCuentaContableCliente = gUtilSQL.getIIF(insCliente.GetTableName & ".CuentaContableAnticipo <> ''", insCliente.GetTableName & ".CuentaContableAnticipo", gUtilSQL.fSimpleSqlValue(gProyReglasDeContabilizacion.GetCuentaCobranzaCobradoAnticipo), True)
         valGeneraElMovimientoBancario = False
      End If
   End If
   cuentaAnticipoCobrado = gUtilSQL.getIIF(insCliente.GetTableName & ".CuentaContableAnticipo <> ''", insCliente.GetTableName & ".CuentaContableAnticipo", gUtilSQL.fSimpleSqlValue(gProyReglasDeContabilizacion.GetCuentaCobranzaCobradoAnticipo), True)
   If sqlSum <> "" Then
      CondicionDeStatusAnulado = gUtilSQL.fFirst(insCobranza.GetTableName() & ".StatusCobranza", "NONE") & " = '" & gConvert.enumerativoAChar(enum_StatusCobranza.eSC_ANULADA) & "'"
      cuentaAnticipoCobrado = gUtilSQL.fFirst(cuentaAnticipoCobrado, "NONE")
   Else
      CondicionDeStatusAnulado = insCobranza.GetTableName() & ".StatusCobranza = '" & gConvert.enumerativoAChar(enum_StatusCobranza.eSC_ANULADA) & "'"
   End If
   codigoDelAuxiliar = insCobranza.GetTableName() & ".CodigoCliente"
   If valGeneraElMovimientoBancario Then
         SqlTasaDeCambio = gUltimaTasaDeCambio.fSQLCampoTasaDeCambio(insCobranza.GetTableName() & ".CambioABolivares", insCuentaBancaria.GetTableName() & ".NombreDeLaMoneda", True, "")
    Else
         SqlTasaDeCambio = gUltimaTasaDeCambio.fSQLCampoTasaDeCambio(insCobranza.GetTableName() & ".CambioABolivares", insCobranza.GetTableName() & ".Moneda", True, "")
    End If
   TasaDeCambioDocumentos = fGetTasaCambioCobranza(ConjuntoDeCobranzasSinContabilizar, valFechaDesde, valFechaHasta)
   sqlRetencionISLR = sqlSum & "(" & insCobranza.GetTableName() & ".RetencionISLR * " & gUtilSQL.fNumToStrSQL(TasaDeCambioDocumentos) & ")"
   sqlRetencionIVA = sqlSum & "(" & insCobranza.GetTableName() & ".RetencionIVA * " & gUtilSQL.fNumToStrSQL(TasaDeCambioDocumentos) & ")"
   sqlCobradoAnticipos = sqlSum & "(" & insCobranza.GetTableName & ".CobradoAnticipo * " & gUtilSQL.fNumToStrSQL(TasaDeCambioDocumentos) & ")"
   sqlTotalDocs = sqlSum & "(" & insCobranza.GetTableName() & ".TotalDocumentos * " & gUtilSQL.fNumToStrSQL(TasaDeCambioDocumentos) & ")"
   sqlVuelto = sqlSum & "(" & insCobranza.GetTableName() & ".Vuelto * " & gUtilSQL.fNumToStrSQL(TasaDeCambioDocumentos) & ")"
   Sql = "SELECT " & insCobranza.GetTableName() & ".CodigoCliente, "
   DocumentosConIvaDiferido = fSQLCobranzaIvaVentaDiferida(valNoDocumentoOrigen)
   If gTexto.DfLen(DocumentosConIvaDiferido) > 0 Then
      sqlIvaDiferido = sqlSum & "(Documentos.MontoIvaDiferido * " & gUtilSQL.fNumToStrSQL(TasaDeCambioDocumentos) & ")"
      Sql = Sql & fAgregaUnaCuentaAlSQL("DEBE", valEsUnReverso, sqlIvaDiferido, gProyReglasDeContabilizacion.getCuentaCobranzaIvaDiferido, gProyReglasDeContabilizacion.getCuentaCobranzaIvaDiferidoUsaAuxiliares, codigoDelAuxiliar, valNoDocumentoOrigen, valDescripcionDelAsiento, CondicionDeStatusAnulado, "") & ", "
      Sql = Sql & fAgregaUnaCuentaAlSQL("HABER", valEsUnReverso, sqlIvaDiferido, gProyReglasDeContabilizacion.GetCuentaIva1Debito, gProyReglasDeContabilizacion.GetCuentaIva1DebitoUsaAuxiliares, codigoDelAuxiliar, valNoDocumentoOrigen, valDescripcionDelAsiento, CondicionDeStatusAnulado, "") & ", "
   End If
   If valGeneraElMovimientoBancario Then
      varCuentadeMovimientoBancario = fCuentaDeMovimientoBancario(valNoDocumentoOrigen)
      SqlTasaDeCambio = gUtilSQL.DfSQLCaseIf(insCuentaBancaria.GetTableName & ".NombreDeLaMoneda", "'" & gProyParametros.GetNombreMonedaLocal & "'" & gListLibrary.getSeparadorGenericoDeElementos & insCobranza.GetTableName & ".Moneda", "1" & gListLibrary.getSeparadorGenericoDeElementos & gUtilSQL.fNumToStrSQL(TasaDeCambioDocumentos) & gListLibrary.getSeparadorGenericoDeElementos & insCobranza.GetTableName & ".CambioABolivares", gListLibrary.getSeparadorGenericoDeElementos, "=", "")
      SqlChequeEfectivoTarjeta = sqlSum & "((" & insCobranza.GetTableName() & ".CobradoCheque + " & insCobranza.GetTableName() & ".CobradoEfectivo + " & insCobranza.GetTableName() & ".CobradoTarjeta) * " & gUtilSQL.fNumToStrSQL(TasaDeCambioDocumentos) & ")"
      SqlOtros = sqlSum & "(" & insCobranza.GetTableName() & ".TotalOtros * " & gUtilSQL.fNumToStrSQL(TasaDeCambioDocumentos) & ")"
      Sql = Sql & fAgregaUnaCuentaAlSQL("DEBE", valEsUnReverso, SqlChequeEfectivoTarjeta, varCuentadeMovimientoBancario, False, codigoDelAuxiliar, valNoDocumentoOrigen, valDescripcionDelAsiento, CondicionDeStatusAnulado, "") & ", "
   Else
'      TasaDeCambioDocumentos = 1
      SqlCheque = sqlSum & "(" & insCobranza.GetTableName() & ".CobradoCheque * " & gUtilSQL.fNumToStrSQL(TasaDeCambioDocumentos) & ")"
      SqlEfectivo = sqlSum & "(" & insCobranza.GetTableName() & ".CobradoEfectivo * " & gUtilSQL.fNumToStrSQL(TasaDeCambioDocumentos) & ")"
      SqlTarjeta = sqlSum & "(" & insCobranza.GetTableName() & ".CobradoTarjeta * " & gUtilSQL.fNumToStrSQL(TasaDeCambioDocumentos) & ")"
      SqlOtros = sqlSum & "(" & insCobranza.GetTableName() & ".TotalOtros * " & gUtilSQL.fNumToStrSQL(TasaDeCambioDocumentos) & ")"
      Sql = Sql & fAgregaUnaCuentaAlSQL("DEBE", valEsUnReverso, SqlCheque, gProyReglasDeContabilizacion.GetCuentaCobranzaCobradoEnCheque, gProyReglasDeContabilizacion.getCuentaCobranzaCobradoEnChequeUsaAuxiliares, codigoDelAuxiliar, fSiEsUnoSoloLoUsaSinoColocaNumeroReferenciaEstandard(valContabilizarSolo1Documento, insCobranza.GetTableName & ".NumerodelCheque", valNoDocumentoOrigen), valDescripcionDelAsiento, CondicionDeStatusAnulado, "") & ", "
      Sql = Sql & fAgregaUnaCuentaAlSQL("DEBE", valEsUnReverso, SqlEfectivo, gProyReglasDeContabilizacion.GetCuentaCobranzaCobradoEnEfectivo, gProyReglasDeContabilizacion.getCuentaCobranzaCobradoEnEfectivoUsaAuxiliares, codigoDelAuxiliar, valNoDocumentoOrigen, valDescripcionDelAsiento, CondicionDeStatusAnulado, "") & ", "
      Sql = Sql & fAgregaUnaCuentaAlSQL("DEBE", valEsUnReverso, SqlTarjeta, gProyReglasDeContabilizacion.GetCuentaCobranzaCobradoEnTarjeta, gProyReglasDeContabilizacion.getCuentaCobranzaCobradoEnTarjetaUsaAuxiliares, codigoDelAuxiliar, fSiEsUnoSoloLoUsaSinoColocaNumeroReferenciaEstandard(valContabilizarSolo1Documento, insCobranza.GetTableName & ".NroDeLaTarjeta", valNoDocumentoOrigen), valDescripcionDelAsiento, CondicionDeStatusAnulado, "") & ", "
   End If
   '***
   Sql = Sql & fAgregaUnaCuentaAlSQL("DEBE", valEsUnReverso, SqlOtros, gProyReglasDeContabilizacion.GetCuentaCobranzaOtros, gProyReglasDeContabilizacion.getCuentaCobranzaOtrosUsaAuxiliares, codigoDelAuxiliar, valNoDocumentoOrigen, valDescripcionDelAsiento, CondicionDeStatusAnulado, "") & ", "
   '***
   Sql = Sql & fAgregaUnaCuentaAlSQL("DEBE", valEsUnReverso, sqlRetencionISLR, gProyReglasDeContabilizacion.GetCuentaCobranzaRetencionISLR, gProyReglasDeContabilizacion.getCuentaCobranzaRetencionISLRUsaAuxiliares, codigoDelAuxiliar, valNoDocumentoOrigen, valDescripcionDelAsiento, CondicionDeStatusAnulado, "") & ", "
   Sql = Sql & fAgregaUnaCuentaAlSQL("DEBE", valEsUnReverso, sqlRetencionIVA, gProyReglasDeContabilizacion.GetCuentaCobranzaRetencionIVA, gProyReglasDeContabilizacion.getCuentaCobranzaRetencionIVAUsaAuxiliares, codigoDelAuxiliar, valNoDocumentoOrigen, valDescripcionDelAsiento, CondicionDeStatusAnulado, "") & ", "
   Sql = Sql & fAgregaUnaCuentaAlSQL("DEBE", valEsUnReverso, sqlCobradoAnticipos, cuentaAnticipoCobrado, False, "", valNoDocumentoOrigen, valDescripcionDelAsiento, CondicionDeStatusAnulado, "") & ", "
   Sql = Sql & fAgregaUnaCuentaAlSQL("HABER", valEsUnReverso, sqlTotalDocs, varSQLCuentaContableCliente, varAuxiliarCxC, codigoDelAuxiliar, valNoDocumentoOrigen, valDescripcionDelAsiento, CondicionDeStatusAnulado, "") & ", "
   Sql = Sql & fAgregaUnaCuentaAlSQL("HABER", valEsUnReverso, sqlVuelto, gProyReglasDeContabilizacion.GetCuentaCobranzaCobradoEnEfectivo, gProyReglasDeContabilizacion.getCuentaCobranzaCobradoEnEfectivoUsaAuxiliares, codigoDelAuxiliar, valNoDocumentoOrigen, "Vuelto en efectivo /" & valDescripcionDelAsiento, CondicionDeStatusAnulado, "")
   
   If gProyReglasDeContabilizacion.GetManejarDiferenciaCambiariaEnCobranza Then
     vDifCambiariaEnCobranza = fGetDiferenciaCambiariaEnCobranza(valNoDocumentoOrigen)
     vDescripcionAsientoDifCambiaria = "Ganancia/Pérdida Cambiaria Doc No." & valNoDocumentoOrigen
     If vDifCambiariaEnCobranza = 0 Then
     ElseIf vDifCambiariaEnCobranza > 0 Then
       Sql = Sql & "," & fAgregaUnaCuentaAlSQL("DEBE", valEsUnReverso, gUtilSQL.fNumToStrSQL(vDifCambiariaEnCobranza), varSQLCuentaContableCliente, varAuxiliarCxC, codigoDelAuxiliar, valNoDocumentoOrigen, vDescripcionAsientoDifCambiaria, CondicionDeStatusAnulado, "") & ","
       Sql = Sql & fAgregaUnaCuentaAlSQL("HABER", valEsUnReverso, gUtilSQL.fNumToStrSQL(vDifCambiariaEnCobranza), gProyReglasDeContabilizacion.GetCuentaDiferenciaCambiaria, False, "", valNoDocumentoOrigen, vDescripcionAsientoDifCambiaria, CondicionDeStatusAnulado, "")
     ElseIf vDifCambiariaEnCobranza < 0 Then
       vDifCambiariaEnCobranza = Abs(vDifCambiariaEnCobranza)
       Sql = Sql & "," & fAgregaUnaCuentaAlSQL("HABER", valEsUnReverso, gUtilSQL.fNumToStrSQL(vDifCambiariaEnCobranza), varSQLCuentaContableCliente, varAuxiliarCxC, codigoDelAuxiliar, valNoDocumentoOrigen, vDescripcionAsientoDifCambiaria, CondicionDeStatusAnulado, "") & ","
       Sql = Sql & fAgregaUnaCuentaAlSQL("DEBE", valEsUnReverso, gUtilSQL.fNumToStrSQL(vDifCambiariaEnCobranza), gProyReglasDeContabilizacion.GetCuentaDiferenciaCambiaria, False, "", valNoDocumentoOrigen, vDescripcionAsientoDifCambiaria, CondicionDeStatusAnulado, "")
     End If
   End If
   Sql = Sql & " FROM " & insCliente.GetTableName & " INNER JOIN " & insCobranza.GetTableName & " ON "
   Sql = Sql & insCliente.GetTableName & ".Codigo = " & insCobranza.GetTableName & ".CodigoCliente AND "
   Sql = Sql & insCliente.GetTableName & ".ConsecutivoCompania = " & insCobranza.GetTableName & ".ConsecutivoCompania"
   If valGeneraElMovimientoBancario Then
      Sql = Sql & " INNER JOIN " & insCuentaBancaria.GetTableName & " ON "
      Sql = Sql & insCobranza.GetTableName & ".CodigoCuentaBancaria = " & insCuentaBancaria.GetTableName & ".Codigo AND "
      Sql = Sql & insCobranza.GetTableName & ".ConsecutivoCompania = " & insCuentaBancaria.GetTableName & ".ConsecutivoCompania "
   End If
   If gTexto.DfLen(DocumentosConIvaDiferido) > 0 Then
      Sql = Sql & " LEFT JOIN (SELECT DocumentoCobrado.NumeroCobranza, SUM (DocumentoCobrado.MontoIvaDeCxC) AS MontoIvaDiferido " & _
            " FROM DocumentoCobrado WHERE DocumentoCobrado.NumeroDelDocumentoCobrado IN (" & DocumentosConIvaDiferido & ") " & _
            " AND DocumentoCobrado.ConsecutivoCompania = " & gUtilSQL.fSimpleSqlValueForNumeric(gProyCompaniaActual.GetConsecutivoCompania) & _
            " AND DocumentoCobrado.NumeroCobranza in (" & ConjuntoDeCobranzasSinContabilizar & ") GROUP BY DocumentoCobrado.NumeroCobranza) " & _
            " AS Documentos ON " & insCobranza.GetTableName() & ".Numero = Documentos.NumeroCobranza "
   End If
   Sql = Sql & " WHERE " & insCobranza.GetTableName() & ".ConsecutivoCompania = " & gProyCompaniaActual.GetConsecutivoCompania
   Sql = Sql & " AND " & insCobranza.GetTableName() & ".Numero IN (" & ConjuntoDeCobranzasSinContabilizar & ")"
   Sql = Sql & " AND " & gUtilSQL.DfSQLDateValueBetween(insCobranza.GetTableName() & ".Fecha ", valFechaDesde, valFechaHasta)
   If Not valContabilizarSolo1Documento Then
      Sql = Sql & " GROUP BY " & insCobranza.GetTableName() & ".CodigoCliente"
   End If
   
   If valGeneraElMovimientoBancario Then
      ConjuntoDeMovimientosBancariosAsociadosACobranza = fConjuntoDeImpTranBancariaAsociadosACobranza(ConjuntoDeCobranzasSinContabilizar)

      SqlTasaDeCambio = gUltimaTasaDeCambio.fSQLCampoTasaDeCambio("movimientoBancario.CambioABolivares", "cuentaBancaria.NombreDeLaMoneda", True, "")
      vDescMovBancario = "ITF / " & valDescripcionDelAsiento
      sqlCuentaContableCuentaBancaria = gUtilSQL.getIIF(gUtilSQL.fFirst("cuentaBancaria.CuentaContable", "NONE") & " = ''", gUtilSQL.fSimpleSqlValue(gProyReglasDeContabilizacion.GetCuentaDebitoBancarioBancos), gUtilSQL.fFirst("cuentaBancaria.CuentaContable", "NONE"))
      sqlSumMontoMov = "SUM(movimientoBancario.Monto * " & gUtilSQL.fNumToStrSQL(TasaDeCambioDocumentos) & ")"
      Sql = Sql & " UNION SELECT movimientoBancario.CodigoConcepto, "
      Sql = Sql & fAgregaUnaCuentaAlSQL("DEBE", valEsUnReverso, sqlSumMontoMov, gProyReglasDeContabilizacion.GetCuentaDebitoBancarioGasto, False, codigoDelAuxiliar, valNoDocumentoOrigen, vDescMovBancario, "", "") & ", "
      Sql = Sql & fAgregaUnaCuentaAlSQL("HABER", valEsUnReverso, sqlSumMontoMov, sqlCuentaContableCuentaBancaria, False, codigoDelAuxiliar, valNoDocumentoOrigen, vDescMovBancario, "", "") & ", "
      Sql = Sql & fAgregaUnaCuentaAlSQL("", False, "0", "", False, "", "", "", "", "") & ", "
      Sql = Sql & fAgregaUnaCuentaAlSQL("", False, "0", "", False, "", "", "", "", "") & ", "
      Sql = Sql & fAgregaUnaCuentaAlSQL("", False, "0", "", False, "", "", "", "", "") & ", "
      Sql = Sql & fAgregaUnaCuentaAlSQL("", False, "0", "", False, "", "", "", "", "") & ", "
      If gTexto.DfLen(DocumentosConIvaDiferido) > 0 Then
         Sql = Sql & fAgregaUnaCuentaAlSQL("", False, "0", "", False, "", "", "", "", "") & ", "
         Sql = Sql & fAgregaUnaCuentaAlSQL("", False, "0", "", False, "", "", "", "", "") & ", "
      End If
      Sql = Sql & fAgregaUnaCuentaAlSQL("", False, "0", "", False, "", "", "", "", "")
      
      If gProyReglasDeContabilizacion.GetManejarDiferenciaCambiariaEnCobranza And vDifCambiariaEnCobranza <> 0 Then
        Sql = Sql & "," & fAgregaUnaCuentaAlSQL("", False, "0", "", False, "", "", "", "", "") & ", "
        Sql = Sql & fAgregaUnaCuentaAlSQL("", False, "0", "", False, "", "", "", "", "")
      End If
      Sql = Sql & " FROM movimientoBancario INNER JOIN cuentaBancaria ON "
      Sql = Sql & "movimientoBancario.ConsecutivoCompania = cuentaBancaria.ConsecutivoCompania AND "
      Sql = Sql & "movimientoBancario.CodigoCtaBancaria = cuentaBancaria.Codigo"
      Sql = Sql & " WHERE movimientoBancario.ConsecutivoCompania = " & gProyCompaniaActual.GetConsecutivoCompania
      If ConjuntoDeMovimientosBancariosAsociadosACobranza <> "" Then
         Sql = Sql & " AND movimientoBancario.NroMovimientoRelacionado IN (" & ConjuntoDeMovimientosBancariosAsociadosACobranza & ")"
      End If
      Sql = Sql & " AND movimientoBancario.GeneradoPor = " & gUtilSQL.fSQLSimpleValueForEnum(enum_GeneradoPor.eGP_DEBITO_BANCARIO)
      Sql = Sql & " AND " & gUtilSQL.DfSQLDateValueBetween("movimientoBancario.Fecha", valFechaDesde, valFechaHasta)
      Sql = Sql & " GROUP BY movimientoBancario.CodigoConcepto"
   End If
   
   Set insCobranza = Nothing
   Set insCuentaBancaria = Nothing
   Set insCliente = Nothing
h_EXIT:
   fSQLCobranzasEntreFechas = Sql
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLCobranzasEntreFechas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLCxPEntreFechas(ByVal valFechaDesde As Date, valFechaHasta As Date, _
            ByVal valEsUnReverso As Boolean, ByVal valDescripcionDelAsiento As String, _
            ByVal valContabilizarSolo1Documento As Boolean, ByVal valNoDocumentoOrigen As String, _
            ByVal valTipoDocumento As Integer, ByVal valConsecutivoDoc As Long) As String
   Dim ConjuntoDeCxPSinContabilizar As String
   Dim codigoDelAuxiliar As String
   Dim CodigoCuentaIVA As String
   Dim CodigoCuentaIVAUsaAuxiliar As Boolean
   Dim numeroReferencia As String
   Dim CondicionDeStatusAnulado As String
   Dim varSQLCuentaContableProveedor As String
   Dim varSQLCuentaContableGastos As String
   Dim varcodigoProveedor As String
   Dim varAuxiliarCxP As Boolean
   Dim varAuxiliarGasto As Boolean
   Dim insCxP As clsCxPNavigator
   Dim insProveedor As clsProveedorNavigator
   Dim Sql As String
   Dim SqlTasaDeCambio As String
   Dim sqlSumSubTotal As String
   Dim sqlSumDebeIva As String
   Dim sqlSumTotalCxP As String
   Dim sqlSumMtoRetIva As String
   Dim sqlOpcEsp As String
   Dim varSQlFecha  As String
   Dim sqlSumMtoRetISLR As String
   Dim vEsDeCompra As Boolean
   Dim insCompraVista As clsComprasVista
   Dim insRetPago As clsRetPagoNavigator
   Dim insRetDocPagado As clsRetDocumentoPagadoNavigator
   Dim sqlSumMontoRetenidoImpuestoMunicipal As String
   On Error GoTo h_ERROR
   CodigoCuentaIVA = ""
   Sql = ""
   Set insCxP = New clsCxPNavigator
   Set insProveedor = New clsProveedorNavigator
   Set insCompraVista = New clsComprasVista
   Set insRetPago = New clsRetPagoNavigator
   Set insRetDocPagado = New clsRetDocumentoPagadoNavigator
   sqlSumMontoRetenidoImpuestoMunicipal = ""
   varcodigoProveedor = fBuscaCodProveedorSegunModulo(valNoDocumentoOrigen, eCG_CXP)
   varAuxiliarCxP = fElProveedorManejaAuxiliar(varcodigoProveedor, eCG_CXP, gProyReglasDeContabilizacion.eTC_ReglaProvCxP, valTipoDocumento)
   varAuxiliarGasto = fElProveedorManejaAuxiliar(varcodigoProveedor, eCG_CXP, gProyReglasDeContabilizacion.eTC_ReglaProvGasto, valTipoDocumento)
'********* OJO AYRG LINEAS MES/ANO APLICACION ******* 'Ahora el campo fecha es una condicion de SQL pues depende del mes/año aplicacion 'varSQLFNFecha = "(" & gUtilSQL.getIIF(gUtilSQL.DfSQLMonthOfDate(insCxP.GetTableName & "." & insCxP.getFN_FECHA) & " = " & insCxP.GetTableName & "." & insCxP.getFN_MES_DE_APLICACION & " AND " & (gUtilSQL.DfSQLYearOfDate(insCxP.GetTableName & "." & insCxP.getFN_FECHA) & " = " & insCxP.GetTableName & "." & insCxP.getFN_ANO_DE_APLICACION), insCxP.GetTableName & "." & insCxP.getFN_FECHA, gUtilSQL.DfCDateSQL(gUtilSQL.fSimpleSqlValue("01/") & gUtilSQL.CharConcat & insCxP.GetTableName & "." & insCxP.getFN_MES_DE_APLICACION & gUtilSQL.CharConcat & gUtilSQL.fSimpleSqlValue("/") & gUtilSQL.CharConcat & insCxP.GetTableName & "." & insCxP.getFN_ANO_DE_APLICACION)) & ") " '********* OJO AYRG FIN LINEAS MES/ANO APLICACION *******
   CondicionDeStatusAnulado = gUtilSQL.fFirst(insCxP.GetTableName() & ".Status", "NONE") & " = '" & gConvert.enumerativoAChar(enum_StatusDocumento.eSD_ANULADO) & "'"
   SqlTasaDeCambio = insCxP.GetTableName & ".CambioABolivares"
   numeroReferencia = valNoDocumentoOrigen
   If valContabilizarSolo1Documento Then
      gTexto.ReemplazaCaracteresEnElString numeroReferencia, vbTab, "-"
   End If
   ConjuntoDeCxPSinContabilizar = fSQLConjuntoRecordsDeContabilizacion1oMasDocumentos(eCG_CXP, valContabilizarSolo1Documento, valNoDocumentoOrigen, valFechaDesde, valFechaHasta, valTipoDocumento)
   If ConjuntoDeCxPSinContabilizar = "" Then
      GoTo h_EXIT
   End If
   varSQLCuentaContableProveedor = gUtilSQL.getIIF(gUtilSQL.fFirst(insProveedor.GetTableName() & ".CuentaContableCxP", "NONE") & " <> ''", _
               gUtilSQL.fFirst(insProveedor.GetTableName() & ".CuentaContableCxP", "NONE"), _
               "'" & gProyReglasDeContabilizacion.GetCuentaCxPProveedores & "'")
  vEsDeCompra = fCXPGeneradaDeCompras(valConsecutivoDoc)
   If (gProyParametrosCompania.GetMetodoDeCosteoAsEnum = eTDMDC_CostoPromedio) And vEsDeCompra Then
      varSQLCuentaContableGastos = " CuentaInventario = " & gUtilSQL.fSimpleSqlValue("") & " OR " & gUtilSQL.DfSQLIsNull("CuentaInventario")
      varSQLCuentaContableGastos = gUtilSQL.getIIF(varSQLCuentaContableGastos, gUtilSQL.fSimpleSqlValue(gProyReglasDeContabilizacion.GetCuentaInventario), "CuentaInventario", True)
   Else
   varSQLCuentaContableGastos = gUtilSQL.getIIF(gUtilSQL.fFirst(insProveedor.GetTableName() & ".CuentaContableGastos", "NONE") & " <> ''", _
               gUtilSQL.fFirst(insProveedor.GetTableName() & ".CuentaContableGastos", "NONE"), _
               "'" & gProyReglasDeContabilizacion.GetCuentaCxPGasto & "'")
   End If
   codigoDelAuxiliar = insCxP.GetTableName() & ".CodigoProveedor"
   CodigoCuentaIVA = fEscogeCuentaDeIVASiEsDebitoOcredito(fCambiaDebeHaberSiEsUnReverso("DEBE", valEsUnReverso), eTD_ALICUOTAGENERAL, eCG_CXP, CodigoCuentaIVAUsaAuxiliar)
   varSQlFecha = gUtilSQL.getIIF("(" & insCxP.GetTableName & ".Fecha < " & gUtilSQL.fDateToSQLValue(gMonedaLocalActual.GetHoyVigenteDesde) & " AND " & insCxP.GetTableName & ".AnoDeAplicacion >= " & gUtilDate.fYear(gMonedaLocalActual.GetHoyVigenteDesde) & ")", _
               insCxP.GetTableName & ".Fecha", gUtilSQL.fSimpleSqlValue(gUtilDate.getFechaDeHoy))
   If gProyCompaniaActual.fPuedoUsarOpcionesDeContribuyenteEspecial And gProyReglasDeContabilizacion.GetDondeContabilizarRetIvaAsEnum = eDC_CxP Then
      sqlSumMtoRetIva = "SUM(" & gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "( " & insCxP.GetTableName & ".MontoRetenido )", varSQlFecha) & ")"
   End If
   If (gProyParametrosCompania.GetMetodoDeCosteoAsEnum = eTDMDC_CostoPromedio) And vEsDeCompra Then
      sqlSumSubTotal = "SUM (" & gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "( " & insCompraVista.GetViewNameComprasConCuentasArticulos & ".TotalRenglon  ) ", varSQlFecha) & " * " & SqlTasaDeCambio & " )"
  Else
   sqlSumSubTotal = "SUM (" & gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "( " & insCxP.GetTableName() & ".MontoExento + " & insCxP.GetTableName() & ".MontoGravado ) ", varSQlFecha) & " * " & SqlTasaDeCambio & " )"
   End If
   sqlSumMtoRetISLR = "SUM(" & gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "( " & insCxP.GetTableName & ".MontoRetenidoISLR )", varSQlFecha) & ")"
   sqlSumDebeIva = "SUM (" & gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "( " & insCxP.GetTableName() & ".MontoIva ) ", varSQlFecha) & " * " & SqlTasaDeCambio & " )"
   sqlSumTotalCxP = "SUM (" & gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "( " & insCxP.fSQLMontoTotalCxP & " ) ", varSQlFecha) & " * " & SqlTasaDeCambio & " )"
   sqlSumMontoRetenidoImpuestoMunicipal = "SUM (" & gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "( " & insCxP.GetTableName & ".MontoRetenidoImpuestoMunicipal  ) ", varSQlFecha) & ")"
  If ((gProyParametrosCompania.GetEnDondeRetenerISLRAsEnum = eDS_CXP) And (gProyParametrosCompania.GetUsaRetencion)) Then
       If (sqlSumMtoRetIva = "") Then
       sqlSumTotalCxP = "(" & sqlSumTotalCxP & " - " & sqlSumMtoRetISLR & " - " & sqlSumMontoRetenidoImpuestoMunicipal & ")"
       
       Else
       sqlSumTotalCxP = "(" & sqlSumTotalCxP & " - " & sqlSumMtoRetIva & " - " & sqlSumMtoRetISLR & " - " & sqlSumMontoRetenidoImpuestoMunicipal & ")"
       
       End If
   Else
        
        If gProyCompaniaActual.fPuedoUsarOpcionesDeContribuyenteEspecial And gProyReglasDeContabilizacion.GetDondeContabilizarRetIvaAsEnum = eDC_CxP Then
           sqlSumTotalCxP = "(" & sqlSumTotalCxP & " - " & sqlSumMtoRetIva & " - " & sqlSumMontoRetenidoImpuestoMunicipal & ")"
        Else
           sqlSumTotalCxP = "(" & sqlSumTotalCxP & " - " & sqlSumMontoRetenidoImpuestoMunicipal & ")"
        
        End If
        
   
   End If
'****Asiento Debe SubTotal*****
   Sql = ""
  
   Sql = Sql & " SELECT " & insCxP.GetTableName() & ".CodigoProveedor, "
    If (gProyParametrosCompania.GetMetodoDeCosteoAsEnum = eTDMDC_CostoPromedio) And (vEsDeCompra) Then
         Sql = Sql & fAgregaUnaCuentaAlSQL("", False, "0", "", False, "", "", "", "", "") & " ,"
       Else
         Sql = Sql & fAgregaUnaCuentaAlSQL("DEBE", valEsUnReverso, sqlSumSubTotal, varSQLCuentaContableGastos, varAuxiliarGasto, codigoDelAuxiliar, numeroReferencia, valDescripcionDelAsiento, CondicionDeStatusAnulado, "") & ", "
     End If
   '****Asiento Debe Iva*****
   
   Sql = Sql & fAgregaUnaCuentaAlSQL("DEBE", valEsUnReverso, sqlSumDebeIva, CodigoCuentaIVA, CodigoCuentaIVAUsaAuxiliar, codigoDelAuxiliar, numeroReferencia, valDescripcionDelAsiento, CondicionDeStatusAnulado, "") & ", "
   '****Asiento Haber CxP*****
   Sql = Sql & fAgregaUnaCuentaAlSQL("HABER", valEsUnReverso, sqlSumTotalCxP, varSQLCuentaContableProveedor, varAuxiliarCxP, codigoDelAuxiliar, numeroReferencia, valDescripcionDelAsiento, CondicionDeStatusAnulado, "")
    
    If ((gProyParametrosCompania.GetEnDondeRetenerISLRAsEnum = eDS_CXP) And (gProyParametrosCompania.GetUsaRetencion)) Then
      Sql = Sql & " ," & fAgregaUnaCuentaAlSQL("HABER", valEsUnReverso, sqlSumMtoRetISLR, gProyReglasDeContabilizacion.GetCuentaPagosRetencionISLR, gProyReglasDeContabilizacion.getCuentaPagosRetencionISLRUsaAuxiliares, codigoDelAuxiliar, numeroReferencia, valDescripcionDelAsiento, CondicionDeStatusAnulado, "")
    End If
    If (gProyParametrosCompania.GetRetieneImpuestoMunicipal()) Then
      Sql = Sql & " ," & fAgregaUnaCuentaAlSQL("HABER", valEsUnReverso, sqlSumMontoRetenidoImpuestoMunicipal, gProyReglasDeContabilizacion.GetCuentaRetencionImpuestoMunicipal, gProyReglasDeContabilizacion.getCuentaRetencionImpuestoMunicipalUsaAuxiliares, codigoDelAuxiliar, numeroReferencia, valDescripcionDelAsiento, CondicionDeStatusAnulado, "")
    End If
   
   Sql = Sql & " FROM " & insCxP.GetTableName & " INNER JOIN " & insProveedor.GetTableName & " ON "
   Sql = Sql & insCxP.GetTableName & ".ConsecutivoCompania = " & insProveedor.GetTableName & ".ConsecutivoCompania"
   Sql = Sql & " AND " & insCxP.GetTableName & ".CodigoProveedor = " & insProveedor.GetTableName & ".CodigoProveedor"
   Sql = Sql & " WHERE " & insCxP.GetTableName & ".ConsecutivoCompania = " & gProyCompaniaActual.GetConsecutivoCompania
   Sql = Sql & " AND " & insCxP.GetTableName & ".ConsecutivoCxp IN (" & ConjuntoDeCxPSinContabilizar & ")"
   Sql = Sql & " GROUP BY " & insCxP.GetTableName() & ".CodigoProveedor "
   
    If (gProyParametrosCompania.GetMetodoDeCosteoAsEnum = eTDMDC_CostoPromedio) And (vEsDeCompra) Then
       Sql = Sql & " UNION  SELECT " & insCxP.GetTableName() & ".CodigoProveedor, "
       Sql = Sql & fAgregaUnaCuentaAlSQL("DEBE", valEsUnReverso, sqlSumSubTotal, varSQLCuentaContableGastos, varAuxiliarGasto, codigoDelAuxiliar, numeroReferencia, valDescripcionDelAsiento, CondicionDeStatusAnulado, "") & " , "
       Sql = Sql & fAgregaUnaCuentaAlSQL("", False, "0", "", False, "", "", "", "", "") & " ,"
       If ((gProyParametrosCompania.GetEnDondeRetenerISLRAsEnum = eDS_CXP) And (gProyParametrosCompania.GetUsaRetencion)) Then
       Sql = Sql & fAgregaUnaCuentaAlSQL("", False, "0", "", False, "", "", "", "", "") & " ,"
       End If
       If (gProyParametrosCompania.GetRetieneImpuestoMunicipal()) Then
       Sql = Sql & fAgregaUnaCuentaAlSQL("", False, "0", "", False, "", "", "", "", "") & " ,"
       End If
       Sql = Sql & fAgregaUnaCuentaAlSQL("", False, "0", "", False, "", "", "", "", "") & " "
       Sql = Sql & " FROM " & insCxP.GetTableName & " INNER JOIN " & insProveedor.GetTableName & " ON "
       Sql = Sql & insCxP.GetTableName & ".ConsecutivoCompania = " & insProveedor.GetTableName & ".ConsecutivoCompania"
       Sql = Sql & " INNER JOIN " & insCompraVista.GetViewNameComprasConCuentasArticulos & " ON "
       Sql = Sql & " cxP.ConsecutivoCompania =  " & insCompraVista.GetViewNameComprasConCuentasArticulos & ".ConsecutivoCompania and "
       Sql = Sql & " dbo.CXP.Numero = " & insCompraVista.GetViewNameComprasConCuentasArticulos & ".Numero "
       Sql = Sql & " AND " & insCxP.GetTableName & ".CodigoProveedor = " & insProveedor.GetTableName & ".CodigoProveedor"
       Sql = Sql & " WHERE " & insCxP.GetTableName & ".ConsecutivoCompania = " & gProyCompaniaActual.GetConsecutivoCompania
       Sql = Sql & " AND " & insCxP.GetTableName & ".ConsecutivoCxp IN (" & ConjuntoDeCxPSinContabilizar & ")"
       Sql = Sql & " GROUP BY " & insCxP.GetTableName() & ".CodigoProveedor, CuentaInventario "
   
   End If
   If gProyCompaniaActual.fPuedoUsarOpcionesDeContribuyenteEspecial And gProyReglasDeContabilizacion.GetDondeContabilizarRetIvaAsEnum = eDC_CxP Then
      sqlOpcEsp = sqlOpcEsp & " UNION SELECT " & insCxP.GetTableName & ".CodigoProveedor, "
      sqlOpcEsp = sqlOpcEsp & fAgregaUnaCuentaAlSQL("HABER", valEsUnReverso, sqlSumMtoRetIva, gProyReglasDeContabilizacion.GetCuentaRetencionIVA, gProyReglasDeContabilizacion.GetCuentaRetencionIvaUsaAuxiliares, codigoDelAuxiliar, valNoDocumentoOrigen, valDescripcionDelAsiento, CondicionDeStatusAnulado, "") & ", "
      sqlOpcEsp = sqlOpcEsp & fAgregaUnaCuentaAlSQL("", False, "0", "", False, "", "", "", "", "") & ", "
      If ((gProyParametrosCompania.GetEnDondeRetenerISLRAsEnum = eDS_CXP) And (gProyParametrosCompania.GetUsaRetencion)) Then
      sqlOpcEsp = sqlOpcEsp & fAgregaUnaCuentaAlSQL("", False, "0", "", False, "", "", "", "", "") & ", "
      End If
      If (gProyParametrosCompania.GetRetieneImpuestoMunicipal()) Then
      sqlOpcEsp = sqlOpcEsp & fAgregaUnaCuentaAlSQL("", False, "0", "", False, "", "", "", "", "") & ", "
      End If
      sqlOpcEsp = sqlOpcEsp & fAgregaUnaCuentaAlSQL("", False, "0", "", False, "", "", "", "", "")
      sqlOpcEsp = sqlOpcEsp & " FROM " & insCxP.GetTableName & " INNER JOIN " & insProveedor.GetTableName & " ON "
      sqlOpcEsp = sqlOpcEsp & insCxP.GetTableName & ".ConsecutivoCompania = " & insProveedor.GetTableName & ".ConsecutivoCompania"
      sqlOpcEsp = sqlOpcEsp & " AND " & insCxP.GetTableName & ".CodigoProveedor = " & insProveedor.GetTableName & ".CodigoProveedor"
      sqlOpcEsp = sqlOpcEsp & " WHERE " & insCxP.GetTableName & ".ConsecutivoCompania = " & gProyCompaniaActual.GetConsecutivoCompania
      sqlOpcEsp = sqlOpcEsp & " AND " & insCxP.GetTableName & ".ConsecutivoCxP" & _
            " IN (" & ConjuntoDeCxPSinContabilizar & ")"
      sqlOpcEsp = sqlOpcEsp & " AND " & insCxP.GetTableName & ".DondeContabilRetIVA = " & gUtilSQL.fSQLSimpleValueForEnum(enum_DondeEfectuoContabilizacionRetIVA.eDC_CxP)
      sqlOpcEsp = sqlOpcEsp & " AND " & insCxP.GetTableName & ".SeContabilRetIva = " & gUtilSQL.fBooleanToSqlValue(False)
      sqlOpcEsp = sqlOpcEsp & " GROUP BY " & insCxP.GetTableName() & ".CodigoProveedor"
      Sql = Sql & sqlOpcEsp
   End If
   Set insCxP = Nothing
   Set insProveedor = Nothing
   Set insCompraVista = Nothing
h_EXIT:
   fSQLCxPEntreFechas = Sql
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLCxPEntreFechas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fConjuntoDeImpTranBancariaAsociadosAPagos(ByVal valConjuntoDePagosSinContabilizar As String) As String
   Dim Sql As String
   Dim ConjuntoDeRecords As String
   On Error GoTo h_ERROR
   Sql = "SELECT movimientoBancario.ConsecutivoMovimiento FROM movimientoBancario  WHERE movimientoBancario.ConsecutivoCompania = " & gProyCompaniaActual.GetConsecutivoCompania
   Sql = Sql & " AND movimientoBancario.NroMovimientoRelacionado IN (" & valConjuntoDePagosSinContabilizar & ")"
   Sql = Sql & " AND movimientoBancario.GeneradoPor = " & gUtilSQL.fSQLSimpleValueForEnum(enum_GeneradoPor.eGP_ORDEN_DE_PAGO)
   ConjuntoDeRecords = gDbUtil.fBuildResultSetAsString(Sql)
h_EXIT:
   fConjuntoDeImpTranBancariaAsociadosAPagos = ConjuntoDeRecords
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConjuntoDeImpTranBancariaAsociadosAPagos", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLPagosEntreFechas(ByVal valFechaDesde As Date, valFechaHasta As Date, _
            ByVal valEsUnReverso As Boolean, ByVal valDescripcionDelAsiento As String, ByVal valContabilizarSolo1Documento As Boolean, _
            ByVal valNoDocumentoOrigen As String, ByVal valGeneraElMovimientoBancario As Boolean, ByVal valTipoDocumento As Integer) As String
   Dim ConjuntoDePagosSinContabilizar As String
   Dim ConjuntoDeMovimientosBancariosAsociadosAPagos As String
   Dim insPago As clsPagoNavigator
   Dim insProveedor As clsProveedorNavigator
   Dim insCuentaBancaria As clsCuentaBancariaNavigator
   Dim insMovBancario As clsMovimientoBancarioNavigator
   Dim insCxP As clsCxPNavigator
   Dim insDocPagado As clsDocumentoPagadoNavigator
   Dim codigoProveedor As String
   Dim codigoDelAuxiliar As String
   Dim descripcionMovBancario As String
   Dim usaAuxiliarCxP As Boolean
   Dim usaAuxiliarAnticipo As Boolean
   Dim Sql As String
   Dim sqlCondicionDeStatusAnulado As String
   Dim sqlCuentaContableAnticipo As String
   Dim sqlCuentaContableProv As String
   Dim SqlTasaDeCambio As String
   Dim sqlCuentaContableCuentBancaria As String
   Dim sqlSumTotalPago As String
   Dim sqlSumTotalOtros As String
   Dim sqlSumTotalRetenido As String
   Dim sqlSumMontoCheque As String
   Dim sqlSumPagadoAnticipo As String
   Dim sqlSumMontoMov As String
   Dim sqlMtonIvaRetenido As String
   Dim sqlCxPInnerJoin As String
   Dim descripAsiento As String
   Dim insLibSaW As clsLibSAW
   Dim vDescripcionAsientoDifCambiaria As String
   Dim vDifCambiariaEnPago As Currency
   On Error GoTo h_ERROR
   Set insLibSaW = New clsLibSAW
   Sql = ""
   Set insPago = New clsPagoNavigator
   Set insCuentaBancaria = New clsCuentaBancariaNavigator
   Set insProveedor = New clsProveedorNavigator
   Set insMovBancario = New clsMovimientoBancarioNavigator
   Set insCxP = New clsCxPNavigator
   Set insDocPagado = New clsDocumentoPagadoNavigator
   codigoProveedor = fBuscaCodProveedorSegunModulo(valNoDocumentoOrigen, eCG_PAGOS)
   usaAuxiliarCxP = fElProveedorManejaAuxiliar(codigoProveedor, eCG_PAGOS, gProyReglasDeContabilizacion.eTC_ReglaProvCxP, valTipoDocumento)
   usaAuxiliarAnticipo = fElProveedorManejaAuxiliar(codigoProveedor, eCG_ANTICIPO, gProyReglasDeContabilizacion.eTC_ReglaAntPagProveedor, valTipoDocumento)
   sqlCondicionDeStatusAnulado = gUtilSQL.fFirst(insPago.GetTableName & ".StatusOrdenDePago", "NONE") & " = '" & gConvert.enumerativoAChar(enum_StatusPago.eSP_ANULADA) & "'"
   SqlTasaDeCambio = insPago.GetTableName & ".CambioaBolivares"
   If valGeneraElMovimientoBancario Then
      SqlTasaDeCambio = gUtilSQL.DfSQLCaseIf(insCuentaBancaria.GetTableName & ".NombreDeLaMoneda", "'" & gProyParametros.GetNombreMonedaLocal & "'" & gListLibrary.getSeparadorGenericoDeElementos & insPago.GetTableName & ".Moneda", "1" & gListLibrary.getSeparadorGenericoDeElementos & SqlTasaDeCambio & gListLibrary.getSeparadorGenericoDeElementos & insPago.GetTableName & ".CambioaBolivares", gListLibrary.getSeparadorGenericoDeElementos, "=", "")
   Else
      SqlTasaDeCambio = "1"
   End If
   ConjuntoDePagosSinContabilizar = fSQLConjuntoRecordsDeContabilizacion1oMasDocumentos(eCG_PAGOS, valContabilizarSolo1Documento, valNoDocumentoOrigen, valFechaDesde, valFechaHasta, valTipoDocumento)
   If ConjuntoDePagosSinContabilizar = "" Then
      GoTo h_EXIT
   End If
   sqlCuentaContableProv = gUtilSQL.getIIF(gUtilSQL.fFirst(insProveedor.GetTableName & ".CuentaContableCxP", "NONE") & " <> ''", _
               gUtilSQL.fFirst(insProveedor.GetTableName & ".CuentaContableCxP", "NONE"), _
               "'" & gProyReglasDeContabilizacion.GetCuentaPagosCxPProveedores & "'")
   If valContabilizarSolo1Documento Then
      ConjuntoDeMovimientosBancariosAsociadosAPagos = fConjuntoDeImpTranBancariaAsociadosAPagos("'" & ConjuntoDePagosSinContabilizar & "'")
   Else
      ConjuntoDeMovimientosBancariosAsociadosAPagos = fConjuntoDeImpTranBancariaAsociadosAPagos(ConjuntoDePagosSinContabilizar)
   End If
   codigoDelAuxiliar = insPago.GetTableName & ".CodigoProveedor"
   sqlCuentaContableAnticipo = gUtilSQL.fFirst(gUtilSQL.getIIF(insProveedor.GetTableName & ".CuentaContableAnticipo <> ''", _
               insProveedor.GetTableName & ".CuentaContableAnticipo", gUtilSQL.fSimpleSqlValue(gProyReglasDeContabilizacion.GetCuentaPagosPagadoAnticipo)), "NONE")
   If valGeneraElMovimientoBancario Then
      sqlCuentaContableCuentBancaria = gUtilSQL.getIIF(gUtilSQL.fFirst("CuentaContable", "NONE") & " <> ''", _
                                 gUtilSQL.fFirst("CuentaContable", "NONE"), "'" & gProyReglasDeContabilizacion.GetCuentaPagosBanco & "'")
   Else
      sqlCuentaContableCuentBancaria = gProyReglasDeContabilizacion.GetCuentaPagosBanco
   End If
   
   sqlSumTotalPago = "SUM((" & insPago.GetTableName & ".TotalDocumentos - " & insPago.GetTableName & ".TotalRetenidoIVA) * " & SqlTasaDeCambio & ")"
   sqlSumTotalOtros = "SUM(" & insPago.GetTableName & ".TotalOtros * " & SqlTasaDeCambio & ")"
   '
   sqlSumTotalRetenido = "SUM(" & insPago.GetTableName & ".TotalRetenido * " & SqlTasaDeCambio & ")"
   sqlSumMontoCheque = "SUM(" & insPago.GetTableName & ".MontoCheque * " & SqlTasaDeCambio & ")"
   sqlSumPagadoAnticipo = "SUM(" & insPago.GetTableName & ".PagadoAnticipo * " & SqlTasaDeCambio & ")"
   Sql = "SELECT " & insPago.GetTableName & ".CodigoProveedor, "
   Sql = Sql & fAgregaUnaCuentaAlSQL("DEBE", valEsUnReverso, sqlSumTotalPago, sqlCuentaContableProv, usaAuxiliarCxP, codigoDelAuxiliar, valNoDocumentoOrigen, valDescripcionDelAsiento, sqlCondicionDeStatusAnulado, "") & ", "
   Sql = Sql & fAgregaUnaCuentaAlSQL("HABER", valEsUnReverso, sqlSumTotalOtros, gProyReglasDeContabilizacion.GetCuentaPagosOtros, gProyReglasDeContabilizacion.getCuentaPagosOtrosUsaAuxiliares, codigoDelAuxiliar, valNoDocumentoOrigen, valDescripcionDelAsiento, sqlCondicionDeStatusAnulado, "") & ", "
  If gProyParametrosCompania.GetUsaRetencion And gProyParametrosCompania.GetEnDondeRetenerISLRAsEnum = eDS_PAGO Then
      Sql = Sql & fAgregaUnaCuentaAlSQL("HABER", valEsUnReverso, sqlSumTotalRetenido, gProyReglasDeContabilizacion.GetCuentaPagosRetencionISLR, gProyReglasDeContabilizacion.getCuentaPagosRetencionISLRUsaAuxiliares, codigoDelAuxiliar, valNoDocumentoOrigen, valDescripcionDelAsiento, sqlCondicionDeStatusAnulado, "") & ", "
  Else
      Sql = Sql & fAgregaUnaCuentaAlSQL("", False, "0", "", False, "", "", "", "", "") & ", "
  End If
   Sql = Sql & fAgregaUnaCuentaAlSQL("HABER", valEsUnReverso, sqlSumMontoCheque, sqlCuentaContableCuentBancaria, False, codigoDelAuxiliar, valNoDocumentoOrigen, valDescripcionDelAsiento, sqlCondicionDeStatusAnulado, "") & ", "
   Sql = Sql & fAgregaUnaCuentaAlSQL("HABER", valEsUnReverso, sqlSumPagadoAnticipo, sqlCuentaContableAnticipo, usaAuxiliarAnticipo, codigoDelAuxiliar, valNoDocumentoOrigen, valDescripcionDelAsiento, sqlCondicionDeStatusAnulado, "")
   
   If gProyReglasDeContabilizacion.GetManejarDiferenciaCambiariaEnPagos Then
     vDifCambiariaEnPago = fGetDiferenciaCambiariaEnPago(valNoDocumentoOrigen)
     vDescripcionAsientoDifCambiaria = "Ganancia/Pérdida Cambiaria Doc No." & valNoDocumentoOrigen
     If vDifCambiariaEnPago = 0 Then
     ElseIf vDifCambiariaEnPago > 0 Then
       vDifCambiariaEnPago = Abs(vDifCambiariaEnPago)
         Sql = Sql & "," & fAgregaUnaCuentaAlSQL("DEBE", valEsUnReverso, gUtilSQL.fNumToStrSQL(vDifCambiariaEnPago), sqlCuentaContableProv, usaAuxiliarCxP, codigoDelAuxiliar, valNoDocumentoOrigen, vDescripcionAsientoDifCambiaria, sqlCondicionDeStatusAnulado, "") & ","
         Sql = Sql & fAgregaUnaCuentaAlSQL("HABER", valEsUnReverso, gUtilSQL.fNumToStrSQL(vDifCambiariaEnPago), gProyReglasDeContabilizacion.GetCuentaDiferenciaCambiaria, False, "", valNoDocumentoOrigen, vDescripcionAsientoDifCambiaria, sqlCondicionDeStatusAnulado, "")
       ElseIf vDifCambiariaEnPago < 0 Then
         vDifCambiariaEnPago = Abs(vDifCambiariaEnPago)
         Sql = Sql & "," & fAgregaUnaCuentaAlSQL("HABER", valEsUnReverso, gUtilSQL.fNumToStrSQL(vDifCambiariaEnPago), sqlCuentaContableProv, usaAuxiliarCxP, codigoDelAuxiliar, valNoDocumentoOrigen, vDescripcionAsientoDifCambiaria, sqlCondicionDeStatusAnulado, "") & ","
         Sql = Sql & fAgregaUnaCuentaAlSQL("DEBE", valEsUnReverso, gUtilSQL.fNumToStrSQL(vDifCambiariaEnPago), gProyReglasDeContabilizacion.GetCuentaDiferenciaCambiaria, False, "", valNoDocumentoOrigen, vDescripcionAsientoDifCambiaria, sqlCondicionDeStatusAnulado, "")
       End If
   End If
   
   Sql = Sql & " FROM " & insProveedor.GetTableName & " INNER JOIN " & insCuentaBancaria.GetTableName & " INNER JOIN " & insPago.GetTableName & " ON "
   Sql = Sql & insCuentaBancaria.GetTableName & ".Codigo = " & insPago.GetTableName & ".CodigoCuentaBancaria AND "
   Sql = Sql & insCuentaBancaria.GetTableName & ".ConsecutivoCompania = " & insPago.GetTableName & ".ConsecutivoCompania ON "
   Sql = Sql & insProveedor.GetTableName & ".CodigoProveedor = " & insPago.GetTableName & ".CodigoProveedor AND "
   Sql = Sql & insProveedor.GetTableName & ".ConsecutivoCompania = " & insPago.GetTableName & ".ConsecutivoCompania"
   
   Sql = Sql & " WHERE " & insPago.GetTableName & ".ConsecutivoCompania = " & gProyCompaniaActual.GetConsecutivoCompania
   Sql = Sql & " AND " & insPago.GetTableName & ".NumeroComprobante IN (" & ConjuntoDePagosSinContabilizar & ")"
   Sql = Sql & " AND " & gUtilSQL.DfSQLDateValueBetween(insPago.GetTableName & ".Fecha", valFechaDesde, valFechaHasta)
   Sql = Sql & " GROUP BY " & insPago.GetTableName & ".CodigoProveedor"
   '  ESTAS INSTRUCCIONES SON PARA EL DEBITO BANCARIO
   sqlCondicionDeStatusAnulado = ""
   SqlTasaDeCambio = gUltimaTasaDeCambio.fSQLCampoTasaDeCambio(insMovBancario.GetTableName & ".CambioABolivares", insCuentaBancaria.GetTableName & ".NombreDeLaMoneda", True, "")
   descripcionMovBancario = insLibSaW.fTituloITFEnCtaBancaria & " / " & valDescripcionDelAsiento
   sqlCuentaContableCuentBancaria = gUtilSQL.getIIF(gUtilSQL.fFirst("CuentaContable", "NONE") & " <> ''", gUtilSQL.fFirst("CuentaContable", "NONE"), "'" & gProyReglasDeContabilizacion.GetCuentaDebitoBancarioBancos & "'")
   sqlSumMontoMov = "SUM(" & insMovBancario.GetTableName & ".Monto * " & SqlTasaDeCambio & ")"
   Sql = Sql & " UNION SELECT " & insMovBancario.GetTableName & ".CodigoConcepto, "
   Sql = Sql & fAgregaUnaCuentaAlSQL("DEBE", valEsUnReverso, sqlSumMontoMov, gProyReglasDeContabilizacion.GetCuentaDebitoBancarioGasto, False, codigoDelAuxiliar, valNoDocumentoOrigen, descripcionMovBancario, sqlCondicionDeStatusAnulado, "") & ", "
   Sql = Sql & fAgregaUnaCuentaAlSQL("HABER", valEsUnReverso, sqlSumMontoMov, sqlCuentaContableCuentBancaria, False, codigoDelAuxiliar, valNoDocumentoOrigen, descripcionMovBancario, sqlCondicionDeStatusAnulado, "") & ", "
   
   Sql = Sql & fAgregaUnaCuentaAlSQL("", False, "0", "", False, "", "", "", "", "") & ", "
   Sql = Sql & fAgregaUnaCuentaAlSQL("", False, "0", "", False, "", "", "", "", "") & ", "
   Sql = Sql & fAgregaUnaCuentaAlSQL("", False, "0", "", False, "", "", "", "", "")
   If gProyReglasDeContabilizacion.GetManejarDiferenciaCambiariaEnPagos And vDifCambiariaEnPago <> 0 Then
      Sql = Sql & "," & fAgregaUnaCuentaAlSQL("", False, "0", "", False, "", "", "", "", "") & ", "
      Sql = Sql & fAgregaUnaCuentaAlSQL("", False, "0", "", False, "", "", "", "", "")
   End If
   Sql = Sql & " FROM " & insMovBancario.GetTableName & " INNER JOIN " & insCuentaBancaria.GetTableName
   Sql = Sql & " ON " & insMovBancario.GetTableName & ".ConsecutivoCompania = " & insCuentaBancaria.GetTableName & ".ConsecutivoCompania AND "
   Sql = Sql & insMovBancario.GetTableName & ".CodigoCtaBancaria = " & insCuentaBancaria.GetTableName & ".Codigo"
   
   Sql = Sql & " WHERE " & insMovBancario.GetTableName & ".ConsecutivoCompania = " & gProyCompaniaActual.GetConsecutivoCompania
   If ConjuntoDeMovimientosBancariosAsociadosAPagos <> "" Then
      Sql = Sql & " AND " & insMovBancario.GetTableName & ".NroMovimientoRelacionado IN (" & ConjuntoDeMovimientosBancariosAsociadosAPagos & ")"
   End If
   Sql = Sql & " AND " & insMovBancario.GetTableName & ".GeneradoPor = " & gUtilSQL.fSQLSimpleValueForEnum(enum_GeneradoPor.eGP_DEBITO_BANCARIO)
   Sql = Sql & " AND " & gUtilSQL.DfSQLDateValueBetween(insMovBancario.GetTableName & ".Fecha", valFechaDesde, valFechaHasta)
   Sql = Sql & " GROUP BY " & insMovBancario.GetTableName & ".CodigoConcepto"
   
   If gProyCompaniaActual.fPuedoUsarOpcionesDeContribuyenteEspecial Then
      sqlMtonIvaRetenido = "SUM(" & insCxP.GetTableName & ".MontoRetenido)" 'el monto de la retención es guardado en MONEDA LOCAL
      descripAsiento = "Retención IVA - " & valDescripcionDelAsiento
      sqlCondicionDeStatusAnulado = gUtilSQL.fFirst(insPago.GetTableName & ".StatusOrdenDePago", "NONE") & " = '" & gConvert.enumerativoAChar(enum_StatusPago.eSP_ANULADA) & "'"
      
      Sql = Sql & " UNION SELECT " & insPago.GetTableName & ".CodigoProveedor, "
      Sql = Sql & fAgregaUnaCuentaAlSQL("DEBE", valEsUnReverso, sqlMtonIvaRetenido, sqlCuentaContableProv, usaAuxiliarCxP, codigoDelAuxiliar, valNoDocumentoOrigen, valDescripcionDelAsiento, sqlCondicionDeStatusAnulado, "") & ", "
      Sql = Sql & fAgregaUnaCuentaAlSQL("HABER", valEsUnReverso, sqlMtonIvaRetenido, gProyReglasDeContabilizacion.GetCuentaRetencionIVA, gProyReglasDeContabilizacion.GetCuentaRetencionIvaUsaAuxiliares, codigoDelAuxiliar, valNoDocumentoOrigen, valDescripcionDelAsiento, sqlCondicionDeStatusAnulado, "") & ", "
      Sql = Sql & fAgregaUnaCuentaAlSQL("", False, "0", "", False, "", "", "", "", "") & ", "
      Sql = Sql & fAgregaUnaCuentaAlSQL("", False, "0", "", False, "", "", "", "", "") & ", "
      Sql = Sql & fAgregaUnaCuentaAlSQL("", False, "0", "", False, "", "", "", "", "")
      
      If gProyReglasDeContabilizacion.GetManejarDiferenciaCambiariaEnPagos And vDifCambiariaEnPago <> 0 Then
        Sql = Sql & "," & fAgregaUnaCuentaAlSQL("", False, "0", "", False, "", "", "", "", "") & ", "
        Sql = Sql & fAgregaUnaCuentaAlSQL("", False, "0", "", False, "", "", "", "", "")
      End If
      Sql = Sql & " FROM " & insProveedor.GetTableName & " INNER JOIN (" & insPago.GetTableName
      Sql = Sql & " INNER JOIN (" & insCxP.GetTableName & " INNER JOIN " & insDocPagado.GetTableName & " ON ("
      Sql = Sql & insCxP.GetTableName & ".Numero = " & insDocPagado.GetTableName & ".NumeroDelDocumentoPagado"
      Sql = Sql & ") AND (" & insCxP.GetTableName & ".ConsecutivoCompania = " & insDocPagado.GetTableName & ".ConsecutivoCompania"
      Sql = Sql & ")) ON (" & insPago.GetTableName & ".NumeroComprobante = " & insDocPagado.GetTableName & ".NumeroComprobante"
      Sql = Sql & ") AND (" & insPago.GetTableName & ".ConsecutivoCompania = " & insDocPagado.GetTableName & ".ConsecutivoCompania"
      Sql = Sql & ")) ON (" & insProveedor.GetTableName & ".CodigoProveedor = " & insPago.GetTableName & ".CodigoProveedor"
      Sql = Sql & ") AND (" & insProveedor.GetTableName & ".ConsecutivoCompania = " & insPago.GetTableName & ".ConsecutivoCompania"
      Sql = Sql & ") AND (" & insProveedor.GetTableName & ".CodigoProveedor = " & insCxP.GetTableName & ".CodigoProveedor"
      Sql = Sql & ") AND (" & insProveedor.GetTableName & ".ConsecutivoCompania = " & insCxP.GetTableName & ".ConsecutivoCompania)"
      Sql = Sql & " WHERE " & insCxP.GetTableName & ".ConsecutivoCompania = " & gProyCompaniaActual.GetConsecutivoCompania
      Sql = Sql & " AND " & insPago.GetTableName & ".NumeroComprobante IN (" & ConjuntoDePagosSinContabilizar & ")"
      Sql = Sql & " AND " & gUtilSQL.DfSQLDateValueBetween(insPago.GetTableName & ".Fecha", valFechaDesde, valFechaHasta)
      Sql = Sql & " AND " & insCxP.GetTableName & ".MontoRetenido <> 0 "
      Sql = Sql & " AND " & insCxP.GetTableName & ".DondeContabilRetIVA = " & gUtilSQL.fSQLSimpleValueForEnum(enum_DondeEfectuoContabilizacionRetIVA.eDC_Pago)
      Sql = Sql & " AND " & insCxP.GetTableName & ".SeContabilRetIva = " & gUtilSQL.fBooleanToSqlValue(False)
     Sql = Sql & " AND " & insPago.GetTableName & ".CodigoMoneda = '" & gProyParametros.GetCodigoMonedaLocal & "'"
      Sql = Sql & " GROUP BY " & insPago.GetTableName & ".CodigoProveedor"
   End If
     Set insCuentaBancaria = Nothing
   Set insMovBancario = Nothing
   Set insPago = Nothing
   Set insProveedor = Nothing
   Set insCxP = Nothing
   Set insDocPagado = Nothing
   Set insLibSaW = Nothing
h_EXIT:
   fSQLPagosEntreFechas = Sql
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLPagosEntreFechas", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fGetDiferenciaCambiariaEnPago(ByVal valNumeroDelDocumento As String) As Currency
    Dim vResult As Currency
    Dim rsDocPagado As ADODB.Recordset
    Dim vSQL As String
    Dim SqlMontoAMonedaDelPago As String
    On Error GoTo h_ERROR
    Set rsDocPagado = New ADODB.Recordset
    vResult = 0
    SqlMontoAMonedaDelPago = gUtilSQL.getIIF("CXP.CodigoMoneda=" & gUtilSQL.fSimpleSqlValue(gProyCompaniaActual.GetCodigoMoneda), "DocumentoPagado.MontoAbonado * CambioAMonedaDelPago", "DocumentoPagado.MontoAbonado/CambioAMonedaDelPago * CXP.CambioAbolivares")
    vSQL = ""
    vSQL = vSQL & " SELECT SUM(" & SqlMontoAMonedaDelPago & ") - SUM(DocumentoPagado.MontoAbonado * Pago.CambioaBolivares) AS DifCambiariaPago " & vbCrLf
    vSQL = vSQL & " FROM CXP LEFT JOIN DocumentoPagado ON" & vbCrLf
    vSQL = vSQL & " CXP.ConsecutivoCxp = DocumentoPagado.ConsecutivoCXP" & vbCrLf
    vSQL = vSQL & " AND  CXP.ConsecutivoCompania = DocumentoPagado.ConsecutivoCompania" & vbCrLf
    vSQL = vSQL & " LEFT JOIN Pago ON"
    vSQL = vSQL & " DocumentoPagado.numeroComprobante = Pago.numeroComprobante"
    vSQL = vSQL & " AND  DocumentoPagado.ConsecutivoCompania = Pago.ConsecutivoCompania"
    vSQL = vSQL & " WHERE DocumentoPagado.NumeroComprobante =" & gUtilSQL.fSimpleSqlValue(valNumeroDelDocumento) & vbCrLf
    vSQL = vSQL & " AND DocumentoPagado.ConsecutivoCompania=" & gUtilSQL.fNumToStrSQL(gProyCompaniaActual.GetConsecutivoCompania) & vbCrLf
    vSQL = vSQL & " AND CXP.Status IN(" & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ABONADO) & "," & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_CANCELADO) & ")" & vbCrLf
    vSQL = vSQL & " GROUP BY DocumentoPagado.NumeroComprobante"
    If gDbUtil.fOpenRecordset(rsDocPagado, vSQL, gDefDatabase.Conexion) Then
        If gDbUtil.fRecordCount(rsDocPagado) > 0 Then
            vResult = rsDocPagado.Fields("DifCambiariaPago")
        End If
    End If
    Set rsDocPagado = Nothing
h_EXIT:
    fGetDiferenciaCambiariaEnPago = vResult
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fGetDiferenciaCambiariaEnPago", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fGetDiferenciaCambiariaEnCobranza(ByVal valNumeroDelDocumento As String) As Currency
    Dim vResult As Currency
    Dim rsDocCobrado As ADODB.Recordset
    Dim vSQL As String
    On Error GoTo h_ERROR
    Set rsDocCobrado = New ADODB.Recordset
    vResult = 0
    vSQL = vSQL & " SELECT SUM(DocumentoCobrado.MontoAbonado * DocumentoCobrado.CambioAMonedaDeCobranza ) - SUM(DocumentoCobrado.MontoAbonadoEnMonedaOriginal * CXC.CambioAbolivares)  AS DifCambiariaCobro " & vbCrLf
    vSQL = vSQL & " FROM CXC LEFT JOIN DocumentoCobrado ON" & vbCrLf
    vSQL = vSQL & " CXC.Numero =DocumentoCobrado.NumeroDelDocumentoCobrado" & vbCrLf
    vSQL = vSQL & " AND CXC.ConsecutivoCompania = DocumentoCobrado.ConsecutivoCompania" & vbCrLf
    vSQL = vSQL & " WHERE DocumentoCobrado.NumeroCobranza = " & gUtilSQL.fSimpleSqlValue(valNumeroDelDocumento) & vbCrLf
    vSQL = vSQL & " AND DocumentoCobrado.ConsecutivoCompania =" & gUtilSQL.fNumToStrSQL(gProyCompaniaActual.GetConsecutivoCompania) & vbCrLf
    vSQL = vSQL & " AND CXC.Status IN(" & gUtilSQL.fSQLSimpleValueForEnum(eSD_ABONADO) & "," & gUtilSQL.fSQLSimpleValueForEnum(eSD_CANCELADO) & ")" & vbCrLf
    vSQL = vSQL & " GROUP BY DocumentoCobrado.NumeroCobranza" & vbCrLf
    If gDbUtil.fOpenRecordset(rsDocCobrado, vSQL, gDefDatabase.Conexion) Then
        If gDbUtil.fRecordCount(rsDocCobrado) > 0 Then
            vResult = rsDocCobrado.Fields("DifCambiariaCobro")
        End If
    End If
    Set rsDocCobrado = Nothing
h_EXIT:
    fGetDiferenciaCambiariaEnCobranza = vResult
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fGetDiferenciaCambiariaEnCobranza", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLMovimientoBancariosEntreFechas(ByVal valFechaDesde As Date, ByVal valFechaHasta As Date, _
            ByVal valEsUnReverso As Boolean, ByVal valDescripcionDelAsiento As String, _
            ByVal valContabilizarSolo1Documento As Boolean, ByVal valNoDocumentoOrigen As String, _
            ByVal valTipoDocumento As Integer) As String
   Dim ConjuntoDeMovimientoBancariosSinContabilizar As String
   Dim DebeOhaber As String
   Dim insMovBancario As clsMovimientoBancarioNavigator
   Dim ColocarAuxiliar As Boolean
   Dim codigoDelAuxiliar As String
   Dim Sql As String
   Dim CodigoCuentaIVA As String
   Dim CodigoCuentaIVAUsaAuxiliar As Boolean
   Dim vDescripcionAsiento As String
   Dim insCuentaBancaria As clsCuentaBancariaNavigator
   Dim SqlTasaDeCambio As String
   Dim vCuentaContableDebe As String
   Dim vCuentaContableHaber As String
   Dim vMontoAsiento As String
   Dim insLibSaW As clsLibSAW
   On Error GoTo h_ERROR
   Set insLibSaW = New clsLibSAW
   CodigoCuentaIVA = ""
   Sql = ""
   Set insMovBancario = New clsMovimientoBancarioNavigator
   Set insCuentaBancaria = New clsCuentaBancariaNavigator
   SqlTasaDeCambio = insMovBancario.GetTableName & ".CambioABolivares"
   ConjuntoDeMovimientoBancariosSinContabilizar = fSQLConjuntoRecordsDeContabilizacion1oMasDocumentos(eCG_MOVIMIENTO_BANCARIO, valContabilizarSolo1Documento, valNoDocumentoOrigen, valFechaDesde, valFechaHasta, valTipoDocumento)
   If ConjuntoDeMovimientoBancariosSinContabilizar = "" Then
      GoTo h_EXIT
   End If
   codigoDelAuxiliar = ""  'No tiene auxiliares
   vMontoAsiento = "SUM( " & gUtilSQL.fRoundNDecimales("movimientoBancario.Monto * " & SqlTasaDeCambio) & ")"
'  EL MOVIMIENTO BANCARIO ES UN INGRESO
   vCuentaContableDebe = gUtilSQL.getIIF("cuenta.Codigo IS NULL ", gUtilSQL.fSimpleSqlValue(gProyReglasDeContabilizacion.GetCuentaMovBancarioBancosDebe), "cuenta.Codigo")
   vCuentaContableHaber = gProyReglasDeContabilizacion.GetCuentaMovBancarioIngresos
   Sql = "SELECT movimientoBancario.CodigoConcepto, "
   Sql = Sql & fAgregaUnaCuentaAlSQL("DEBE", valEsUnReverso, vMontoAsiento, vCuentaContableDebe, False, codigoDelAuxiliar, valNoDocumentoOrigen, valDescripcionDelAsiento, "", "") & ", "
   Sql = Sql & fAgregaUnaCuentaAlSQL("HABER", valEsUnReverso, vMontoAsiento, vCuentaContableHaber, False, codigoDelAuxiliar, valNoDocumentoOrigen, valDescripcionDelAsiento, "", "")
   Sql = Sql & " FROM movimientoBancario INNER JOIN cuentaBancaria ON "
   Sql = Sql & " movimientoBancario.ConsecutivoCompania = cuentaBancaria.ConsecutivoCompania AND "
   Sql = Sql & " movimientoBancario.CodigoCtaBancaria = cuentaBancaria.Codigo"

   Sql = Sql & " LEFT OUTER JOIN CUENTA"
   Sql = Sql & " ON cuentaBancaria.ConsecutivoCompania = cuenta.consecutivocompania"
   Sql = Sql & " AND cuentaBancaria.cuentacontable = cuenta.codigo"
   Sql = Sql & " AND cuentabancaria.consecutivocompania = " & gProyCompaniaActual.GetConsecutivoCompania
   Sql = Sql & " AND CUENTA.ConsecutivoPeriodo = " & gContPeriodoActual.GetConsecutivoPeriodo

   Sql = Sql & " WHERE movimientoBancario.ConsecutivoCompania = " & gProyCompaniaActual.GetConsecutivoCompania
   Sql = Sql & " AND movimientoBancario.ConsecutivoMovimiento IN (" & ConjuntoDeMovimientoBancariosSinContabilizar & ")"
   Sql = Sql & " AND movimientoBancario.TipoConcepto = '" & gConvert.enumerativoAChar(eIE_INGRESO) & "'"
   Sql = Sql & " AND (" & gUtilSQL.DfSQLDateValueBetween("movimientoBancario.Fecha", valFechaDesde, valFechaHasta) & ")"
   Sql = Sql & " GROUP BY movimientoBancario.CodigoConcepto, cuenta.Codigo"
'  EL MOVIMIENTO BANCARIO ES UN EGRESO
   vCuentaContableDebe = gProyReglasDeContabilizacion.GetCuentaMovBancarioGasto
   vCuentaContableHaber = gUtilSQL.getIIF("cuenta.Codigo IS NULL", gUtilSQL.fSimpleSqlValue(gProyReglasDeContabilizacion.GetCuentaMovBancarioBancosHaber), "cuenta.Codigo")

   Sql = Sql & " UNION SELECT movimientoBancario.CodigoConcepto, "
   Sql = Sql & fAgregaUnaCuentaAlSQL("DEBE", valEsUnReverso, vMontoAsiento, vCuentaContableDebe, False, codigoDelAuxiliar, valNoDocumentoOrigen, valDescripcionDelAsiento, "", "") & ", "
   Sql = Sql & fAgregaUnaCuentaAlSQL("HABER", valEsUnReverso, vMontoAsiento, vCuentaContableHaber, False, codigoDelAuxiliar, valNoDocumentoOrigen, valDescripcionDelAsiento, "", "")
   Sql = Sql & " FROM movimientoBancario INNER JOIN cuentaBancaria ON "
   Sql = Sql & " movimientoBancario.ConsecutivoCompania = cuentaBancaria.ConsecutivoCompania AND "
   Sql = Sql & " movimientoBancario.CodigoCtaBancaria = cuentaBancaria.Codigo"

   Sql = Sql & " LEFT OUTER JOIN CUENTA"
   Sql = Sql & " ON cuentaBancaria.ConsecutivoCompania = cuenta.consecutivocompania"
   Sql = Sql & " AND cuentaBancaria.cuentacontable = cuenta.codigo"
   Sql = Sql & " AND cuentabancaria.consecutivocompania = " & gProyCompaniaActual.GetConsecutivoCompania
   Sql = Sql & " AND CUENTA.ConsecutivoPeriodo = " & gContPeriodoActual.GetConsecutivoPeriodo

   Sql = Sql & " WHERE movimientoBancario.ConsecutivoCompania = " & gProyCompaniaActual.GetConsecutivoCompania
   Sql = Sql & " AND movimientoBancario.ConsecutivoMovimiento IN (" & ConjuntoDeMovimientoBancariosSinContabilizar & ")"
   Sql = Sql & " AND movimientoBancario.TipoConcepto = '" & gConvert.enumerativoAChar(eIE_EGRESO) & "'"
   Sql = Sql & " AND (" & gUtilSQL.DfSQLDateValueBetween("movimientoBancario.Fecha", valFechaDesde, valFechaHasta) & ")"
   Sql = Sql & " GROUP BY movimientoBancario.CodigoConcepto, cuenta.Codigo"
'  ESTAS INSTRUCCIONES SON PARA EL IMPUESTO DE DEBITO/CREDITO BANCARIO
   vDescripcionAsiento = insLibSaW.fTituloITFEnCtaBancaria & " / " & valDescripcionDelAsiento
   vCuentaContableDebe = gUtilSQL.getIIF(gUtilSQL.fFirst("movimientoBancarioRelacionado.TipoConcepto", "NONE") & " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_IngresoEgreso.eIE_INGRESO), gUtilSQL.fSimpleSqlValue(gProyReglasDeContabilizacion.GetCuentaCreditoBancarioGasto), gUtilSQL.fSimpleSqlValue(gProyReglasDeContabilizacion.GetCuentaDebitoBancarioGasto), True)
   vCuentaContableHaber = gUtilSQL.getIIF(gUtilSQL.fFirst("cuentaBancaria.CuentaContable", "NONE") & " = ''", gUtilSQL.fSimpleSqlValue(gProyReglasDeContabilizacion.GetCuentaDebitoBancarioBancos), gUtilSQL.fFirst("cuentaBancaria.CuentaContable", "NONE"), True)
   Sql = Sql & " UNION SELECT movimientoBancario.CodigoConcepto, "
   Sql = Sql & fAgregaUnaCuentaAlSQL("DEBE", valEsUnReverso, vMontoAsiento, vCuentaContableDebe, False, codigoDelAuxiliar, valNoDocumentoOrigen, vDescripcionAsiento, "", "") & ", "
   Sql = Sql & fAgregaUnaCuentaAlSQL("HABER", valEsUnReverso, vMontoAsiento, vCuentaContableHaber, False, codigoDelAuxiliar, valNoDocumentoOrigen, vDescripcionAsiento, "", "")
   Sql = Sql & " FROM movimientoBancario INNER JOIN cuentaBancaria ON "
   Sql = Sql & " movimientoBancario.ConsecutivoCompania = cuentaBancaria.ConsecutivoCompania AND "
   Sql = Sql & " movimientoBancario.CodigoCtaBancaria = cuentaBancaria.Codigo INNER JOIN movimientoBancario AS movimientoBancarioRelacionado ON "
   Sql = Sql & " movimientoBancario.NroMovimientoRelacionado = movimientoBancarioRelacionado.ConsecutivoMovimiento AND "
   Sql = Sql & " movimientoBancario.consecutivoCompania = movimientoBancarioRelacionado.consecutivoCompania"
   Sql = Sql & " WHERE movimientoBancario.ConsecutivoCompania = " & gProyCompaniaActual.GetConsecutivoCompania
   Sql = Sql & " AND movimientoBancario.NroMovimientoRelacionado IN ('" & ConjuntoDeMovimientoBancariosSinContabilizar & "')"
   Sql = Sql & " AND (" & gUtilSQL.DfSQLDateValueBetween("movimientoBancario.Fecha", valFechaDesde, valFechaHasta) & ")"
   Sql = Sql & " GROUP BY movimientoBancario.CodigoConcepto"
   Set insMovBancario = Nothing
   Set insCuentaBancaria = Nothing
   Set insLibSaW = Nothing
h_EXIT:
   fSQLMovimientoBancariosEntreFechas = Sql
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLMovimientoBancariosEntreFechas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLFacturasEntreFechas(ByVal valFechaDesde As Date, ByVal valFechaHasta As Date, _
            ByVal valEsUnReverso As Boolean, ByVal valDescripcionDelAsiento As String, _
            ByVal valContabilizarSolo1Documento As Boolean, ByVal valNoDocumentoOrigen As String, _
            ByVal valTipoDocumento As Integer) As String
   Dim insFactura As clsFacturaNavigator
   Dim insCliente As clsClienteNavigator
   Dim ConjuntoDeFacturasSinContabilizar As String
   Dim DebeOhaber As String
   Dim ColocarAuxiliar As Boolean
   Dim codigoDelAuxiliar As String
   Dim Sql As String
   Dim CodigoCuentaIVA As String
   Dim CodigoCuentaIVAUsaAuxiliar As Boolean
   Dim SqlMontoCargosMenosDescuentos  As String
   Dim SqlTasaDeCambio As String
   Dim CondicionDeStatusAnulado As String
   Dim varCodigoCliente As String
   Dim varAuxiliarCxC As Boolean
   Dim varSQLCuentaContableCliente As String
   Dim numeroReferencia As String
   Dim varCodigoCentroDeCosto As String
   Dim insAlmacen As clsAlmacenNavigator
   Dim vNumeroFactura As String
   Dim vAplicaDiferimiento As Boolean
   Dim vUsaContabilizacionMensualPorLote As Boolean
   Dim vCodigoCuentaIGTF As String
   On Error GoTo h_ERROR
   Set insCliente = New clsClienteNavigator
   Set insFactura = New clsFacturaNavigator
   Set insAlmacen = New clsAlmacenNavigator
   
   vUsaContabilizacionMensualPorLote = (gProyReglasDeContabilizacion.GetTipoContabilizacionFacturacionAsEnum = eTD_POR_LOTE And gProyReglasDeContabilizacion.GetContabPorLoteFacturacionAsEnum = eCP_MENSUAL)
   insFactura.setClaseDeTrabajo eCTFC_Factura
   CodigoCuentaIVA = ""
   Sql = ""
   varCodigoCliente = fBuscaCodClienteSegunModulo(valNoDocumentoOrigen, eCG_FACTURA, valTipoDocumento)
   varAuxiliarCxC = fElClienteManejaAuxiliar(varCodigoCliente, eCG_FACTURA, gProyReglasDeContabilizacion.eTC_ReglaCliCxC, valTipoDocumento)
   CondicionDeStatusAnulado = gUtilSQL.fFirst(insFactura.GetTableName & "." & insFactura.getFN_STATUS_FACTURA, "NONE") & " = " & _
                              gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA)
   SqlTasaDeCambio = insFactura.GetTableName & "." & insFactura.getFN_CAMBIO_ABOLIVARES
   numeroReferencia = valNoDocumentoOrigen
   If valContabilizarSolo1Documento Then
      gTexto.ReemplazaCaracteresEnElString numeroReferencia, vbTab, "-"
   End If
   ConjuntoDeFacturasSinContabilizar = fSQLConjuntoRecordsDeContabilizacion1oMasDocumentos(eCG_FACTURA, valContabilizarSolo1Documento, valNoDocumentoOrigen, valFechaDesde, valFechaHasta, valTipoDocumento)
   If ConjuntoDeFacturasSinContabilizar = "" Then
      GoTo h_EXIT
   End If
   varSQLCuentaContableCliente = gUtilSQL.getIIF(gUtilSQL.fFirst(insCliente.GetTableName & ".CuentaContableCxC", "NONE") & " <> ''", _
               gUtilSQL.fFirst(insCliente.GetTableName & ".CuentaContableCxC", "NONE"), _
               gUtilSQL.fSimpleSqlValue(gProyReglasDeContabilizacion.GetCuentaFacturacionCxCClientes))
   codigoDelAuxiliar = insFactura.GetTableName & "." & insFactura.getFN_CODIGO_CLIENTE
   varCodigoCentroDeCosto = "Almacen.CodigoCc"
   vNumeroFactura = gTexto.DfMid(valNoDocumentoOrigen, gTexto.DfInStr(valNoDocumentoOrigen, gTexto.fSeparadorStandardDeElementosString) + 1)
   If insFactura.fSearchByNumeroTipoDocumento(vNumeroFactura, valTipoDocumento, False, True, False, True) Then
      vAplicaDiferimiento = insFactura.GetEsOriginalmenteDiferida
      If (valTipoDocumento = enum_TipoDocumentoFactura.eTF_NOTADECREDITO) Or (valTipoDocumento = enum_TipoDocumentoFactura.eTF_NOTADECREDITOCOMPROBANTEFISCAL) Then
         vAplicaDiferimiento = insFactura.GetEsDiferida Or fEsNotaCreditoDeFacturaConDiferimiento(insFactura.GetNumeroFacturaAfectada, valTipoDocumento)
      End If
      CodigoCuentaIVA = fEscogeCuentaDeIVASiEsDebitoOcredito(fCambiaDebeHaberSiEsUnReverso("HABER", valEsUnReverso), _
                                 eTD_ALICUOTAGENERAL, eCG_FACTURA, CodigoCuentaIVAUsaAuxiliar, vAplicaDiferimiento)
   End If
   vCodigoCuentaIGTF = gProyReglasDeContabilizacion.GetCuentaCreditoBancarioGasto
   Sql = "SELECT " & insFactura.GetTableName & "." & insFactura.getFN_CODIGO_CLIENTE & ", "
   Sql = Sql & fAgregaUnaCuentaAlSQL("DEBE", valEsUnReverso, _
                  "SUM(" & gUtilSQL.fRoundNDecimales(insFactura.GetTableName & "." & insFactura.getFN_TOTAL_FACTURA & " * " & SqlTasaDeCambio, 2) & " + factura.IGTFML)", _
                  varSQLCuentaContableCliente, varAuxiliarCxC, codigoDelAuxiliar, _
                  numeroReferencia, valDescripcionDelAsiento, CondicionDeStatusAnulado, varCodigoCentroDeCosto) & ", "
   Sql = Sql & fAgregaUnaCuentaAlSQL("HABER", valEsUnReverso, _
                  "SUM(" & gUtilSQL.fRoundNDecimales(insFactura.GetTableName & "." & insFactura.getFN_TOTAL_IVA & " * " & SqlTasaDeCambio, 2) & ")", _
                  CodigoCuentaIVA, CodigoCuentaIVAUsaAuxiliar, codigoDelAuxiliar, _
                  numeroReferencia, valDescripcionDelAsiento, CondicionDeStatusAnulado, varCodigoCentroDeCosto) & ", "
    Sql = Sql & fAgregaUnaCuentaAlSQL("HABER", valEsUnReverso, _
                  "SUM(" & gUtilSQL.fRoundNDecimales("((" & insFactura.GetTableName & "." & insFactura.getFN_TOTAL_FACTURA & " - " & _
                        insFactura.GetTableName & "." & insFactura.getFN_TOTAL_IVA & ") * " & SqlTasaDeCambio & ")", 2) & ")", _
                  gUtilSQL.getIIF(gUtilSQL.fFirst(insCliente.GetTableName & ".CuentaContableIngresos", "NONE") & " <> ''", _
                  gUtilSQL.fFirst(insCliente.GetTableName & ".CuentaContableIngresos", "NONE"), _
                  gUtilSQL.fSimpleSqlValue(gProyReglasDeContabilizacion.GetCuentaFacturacionMontoTotalFactura), True), _
                  gProyReglasDeContabilizacion.getCuentaFacturacionMontoTotalFacturaUsaAuxiliares, _
                  codigoDelAuxiliar, numeroReferencia, valDescripcionDelAsiento, CondicionDeStatusAnulado, varCodigoCentroDeCosto)
                  
   Sql = Sql & " ," & fAgregaUnaCuentaAlSQL("", False, "0", "", False, "", "", "", "", varCodigoCentroDeCosto)
   Sql = Sql & ", " & fAgregaUnaCuentaAlSQL("HABER", valEsUnReverso, _
         "SUM(Factura.IGTFML)", _
         vCodigoCuentaIGTF, False, "", _
         numeroReferencia, valDescripcionDelAsiento, CondicionDeStatusAnulado, "")
   Sql = Sql & " FROM " & insFactura.GetTableName & " INNER JOIN " & insCliente.GetTableName & " ON "
   Sql = Sql & insFactura.GetTableName & "." & insFactura.getFN_CONSECUTIVO_COMPANIA & " = "
   Sql = Sql & insCliente.GetTableName & ".ConsecutivoCompania AND "
   Sql = Sql & insFactura.GetTableName & "." & insFactura.getFN_CODIGO_CLIENTE & " = "
   Sql = Sql & insCliente.GetTableName & ".Codigo"
   If (gProyCompaniaActual.GetUsaCentroDeCostos And gProyParametrosCompania.GetAsociaCentroDeCostoyAlmacen) Then
      Sql = Sql & " INNER JOIN Almacen on  "
      Sql = Sql & insCliente.GetTableName & ".ConsecutivoCompania = Almacen.ConsecutivoCompania AND "
      Sql = Sql & insFactura.GetTableName & ".CodigoAlmacen = Almacen.Codigo "
   End If
   Sql = Sql & " WHERE " & insFactura.GetTableName & "." & insFactura.getFN_CONSECUTIVO_COMPANIA & " = " & gProyCompaniaActual.GetConsecutivoCompania
   Sql = Sql & " AND " & insFactura.GetTableName & "." & insFactura.getFN_STATUS_FACTURA
   If valEsUnReverso Then
      Sql = Sql & " IN (" & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA) & ")"
   Else
      Sql = Sql & " IN (" & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_EMITIDA) & ")"
   End If
   If vUsaContabilizacionMensualPorLote Then
      Sql = Sql & " AND " & insFactura.GetTableName & "." & insFactura.getFN_NUMERO & " IN (" & ConjuntoDeFacturasSinContabilizar & ")"
   Else
      Sql = Sql & " AND " & insFactura.GetTableName & "." & insFactura.getFN_TIPO_DE_DOCUMENTO & gUtilSQL.CharConcat & "'" & gTexto.fSeparadorStandardDeElementosString & "'" & gUtilSQL.CharConcat & insFactura.GetTableName & "." & insFactura.getFN_NUMERO & " IN (" & ConjuntoDeFacturasSinContabilizar & ")"
   End If
   Sql = Sql & " AND (" & gUtilSQL.DfSQLDateValueBetween(insFactura.GetTableName & "." & insFactura.getFN_FECHA, valFechaDesde, valFechaHasta) & ")"
   Sql = Sql & " AND " & insFactura.GetTableName & "." & insFactura.getFN_TIPO_DE_DOCUMENTO & " <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_RESUMENDIARIODEVENTAS)
   Sql = Sql & " GROUP BY " & insFactura.GetTableName & "." & insFactura.getFN_CODIGO_CLIENTE
   If (gProyCompaniaActual.GetUsaCentroDeCostos And gProyParametrosCompania.GetAsociaCentroDeCostoyAlmacen) Then
        Sql = Sql & ", " & varCodigoCentroDeCosto
   End If
   If gProyParametrosCompania.GetUsarOtrosCargoDeFactura Then
      Sql = Sql & " UNION ALL "
      Sql = Sql & fSQLOtrosCargosYDescuentosDeFacturaEntreFechas(valFechaDesde, valFechaHasta, valEsUnReverso, _
         valDescripcionDelAsiento, valContabilizarSolo1Documento, valNoDocumentoOrigen, False, valTipoDocumento)
   End If
   Set insFactura = Nothing
   Set insCliente = Nothing
   Set insAlmacen = Nothing
h_EXIT:
   fSQLFacturasEntreFechas = Sql
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLFacturasEntreFechas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fBuscaElNumeroDelComprobanteAutomatico(ByVal valGeneradoPor As enum_ComprobanteGeneradoPor, ByVal valNoDocumentoOrigen As String) As String
   Dim NumeroBuscado As String
   Dim comprobanteNavigator As clsComprobanteNavigator
   On Error GoTo h_ERROR
   Set comprobanteNavigator = New clsComprobanteNavigator
   NumeroBuscado = ""
   If comprobanteNavigator.fSearchByNoDocumentoOrigenYGeneradoPor(valGeneradoPor, valNoDocumentoOrigen, False, False, 0, True) Then 'para revision(ConsecutivoDocumento para Comprobante -> 0,True)
      NumeroBuscado = comprobanteNavigator.GetNumero
   End If
   Set comprobanteNavigator = Nothing
h_EXIT:
   fBuscaElNumeroDelComprobanteAutomatico = NumeroBuscado
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fBuscaElNumeroDelComprobanteAutomatico", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Sub sMuestraElComprobanteAsociadoAlDocumento(ByVal valGeneradoPor As enum_ComprobanteGeneradoPor, ByVal valNoDocumentoOrigen As String)
   Dim NumeroBuscado As String
   Dim insComprobanteNavigator As clsComprobanteNavigator
   Dim insFrmComprobanteInput As frmComprobanteInput
   On Error GoTo h_ERROR
   Set insComprobanteNavigator = New clsComprobanteNavigator
   NumeroBuscado = ""
   If Not insComprobanteNavigator.fSearchByNoDocumentoOrigenYGeneradoPor(valGeneradoPor, valNoDocumentoOrigen, True, False, 0, True) Then  'para revision(ConsecutivoDocumento para Comprobante -> 0,True)
      gMessage.Advertencia "No existe Comprobante Contable asociado al documento " & _
                           valNoDocumentoOrigen & " del módulo " & _
                           gEnumProyectoWincont.enumComprobanteGeneradoPorToString(valGeneradoPor)
   Else
      Set insFrmComprobanteInput = New frmComprobanteInput
      insFrmComprobanteInput.sInitLookAndFeelAndSetValues consultar, Nothing, False, False, insComprobanteNavigator
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sMuestraElComprobanteAsociadoAlDocumento", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Function fSiEsUnoSoloLoUsaSinoColocaNumeroReferenciaEstandard(ByVal valContabilizarSolo1Documento As Boolean, _
                  ByVal valNumeroEspecifico As String, ByVal valNoDocumentoOrigen As String) As String
   If valContabilizarSolo1Documento Then
      fSiEsUnoSoloLoUsaSinoColocaNumeroReferenciaEstandard = vbTab & valNumeroEspecifico & vbTab
   Else
      fSiEsUnoSoloLoUsaSinoColocaNumeroReferenciaEstandard = valNoDocumentoOrigen
   End If
End Function

Private Function fFieldNameNumeroWithTableName(ByVal valNombreTablaOrigen As String, ByVal valNombreCampoNumero As String) As String
   Dim varFieldNumero As String
   Dim varArrayDeCampos As Variant
   Dim i As Integer
   Dim varLimiteSuperior As Integer
   On Error GoTo h_ERROR
   varFieldNumero = ""
   If gTexto.DfInStr(valNombreCampoNumero, gProyReglasDeContabilizacion.fSQLSeparadorDeCamposStandardDelNroDocumentoOrigen) > 0 Then
      varArrayDeCampos = gTexto.fConvertCadenaEnArray(valNombreCampoNumero, gProyReglasDeContabilizacion.fSQLSeparadorDeCamposStandardDelNroDocumentoOrigen)
      varLimiteSuperior = UBound(varArrayDeCampos)
      For i = LBound(varArrayDeCampos) To UBound(varArrayDeCampos)
         varFieldNumero = varFieldNumero & valNombreTablaOrigen & "." & varArrayDeCampos(i)
         If i < varLimiteSuperior Then
            varFieldNumero = varFieldNumero & gProyReglasDeContabilizacion.fSQLSeparadorDeCamposStandardDelNroDocumentoOrigen
         End If
      Next i
   Else
      varFieldNumero = valNombreTablaOrigen & "." & valNombreCampoNumero
   End If
   fFieldNameNumeroWithTableName = varFieldNumero
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fFieldNameNumeroWithTableName", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fGetFechaDesdeValidaParaBusqueda(ByVal valFechaDesdeOriginal As Date) As Date
   On Error GoTo h_ERROR
   If gUtilDate.fF1IsLessOrEqualToF2(valFechaDesdeOriginal, gProyParametrosCompania.GetFechaDeInicioContabilizacion) Then
      fGetFechaDesdeValidaParaBusqueda = gProyParametrosCompania.GetFechaDeInicioContabilizacion
   Else
      fGetFechaDesdeValidaParaBusqueda = valFechaDesdeOriginal
   End If
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fGetFechaDesdeValidaParaBusqueda", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fHayAlMenosUnMontoDistintoDeCero(ByVal valRecordSet As ADODB.Recordset) As Boolean
   Dim varMonto As Currency
   Dim Continuar As Boolean
   Dim nCount As Integer
   Dim varEncontreUno As Boolean
   Dim varField As ADODB.Field
   On Error GoTo h_ERROR
   Continuar = True
   varEncontreUno = False
   fHayAlMenosUnMontoDistintoDeCero = False
   If valRecordSet.RecordCount > 0 Then
      valRecordSet.MoveFirst
      While Not valRecordSet.EOF And Continuar
         nCount = 1
         For Each varField In valRecordSet.Fields
            If valRecordSet.Fields(nCount).value <> "" Then
               If (valRecordSet.Fields(nCount).value = "DEBE" Or valRecordSet.Fields(nCount).value = "HABER") _
                  And (valRecordSet.Fields.Count - 1 >= nCount + 6) Then
                  If IsNull(valRecordSet.Fields(nCount + 1)) Then
                     varMonto = 0
                  Else
                     varMonto = gConvert.fTruncaANDecimales(valRecordSet.Fields(nCount + 1), 2)
                  End If
                  If varMonto <> 0 Then
                     varEncontreUno = True
                     Continuar = False
                  End If
               End If
            End If
            nCount = nCount + 7
            If nCount >= valRecordSet.Fields.Count Then
               Exit For
            End If
         Next varField
         If Continuar Then
            valRecordSet.MoveNext
         End If
      Wend
   End If
h_EXIT: On Error GoTo 0
   fHayAlMenosUnMontoDistintoDeCero = varEncontreUno
   valRecordSet.MoveFirst
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
            "fHayAlMenosUnMontoDistintoDeCero", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fBuscaDescripcionDelComprobanteContableDePago(ByVal valNumeroDocumento As String) As String
   Dim DescripcionDelPago As String
   Dim NumeroDeChequePago As String
   Dim NombreProveedorDelPago As String
   Dim insPagoNavigator As clsPagoNavigator
   Dim DescripcionBuscada As String
   Dim Sql As String
   On Error GoTo h_ERROR
   Set insPagoNavigator = New clsPagoNavigator
   insPagoNavigator.sBuscaDatosDelPagoParaDescripcionComprobante _
              valNumeroDocumento, NumeroDeChequePago, NombreProveedorDelPago, DescripcionDelPago
   Set insPagoNavigator = Nothing
   DescripcionBuscada = "CH.No." & NumeroDeChequePago & "/" & gTexto.DfMid(NombreProveedorDelPago, 1, 20)
   If DescripcionDelPago <> "" Then
      DescripcionBuscada = DescripcionBuscada & "/" & DescripcionDelPago
   End If
   DescripcionBuscada = gTexto.DfMid(DescripcionBuscada, 1, 40)
h_EXIT:
   fBuscaDescripcionDelComprobanteContableDePago = DescripcionBuscada
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
            "fBuscaDescripcionDelComprobanteContableDePago", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
 End Function
Public Function setProcesoDeImpresionEspecial(ByRef refForm As Object)
   mImprimirEspecialAlGrabarComprobante = True
   Set mPagoProcesoDeImpresionEspecial = refForm
End Function

Private Function fBuscaDescripcionDelComprobanteContableDeCxC(ByVal valNumeroDocumento As String, ByVal valTipoDeCxC As enum_TipoDeTransaccion) As String
   Dim DescripcionDeLaCxC As String
   Dim NombreClienteDeLaCxC As String
   Dim insCXCNavigator As clsCxCNavigator
   Dim DescripcionBuscada As String
   Dim Sql As String
   On Error GoTo h_ERROR
   Set insCXCNavigator = New clsCxCNavigator
   insCXCNavigator.sBuscaDatosDeLaCxCParaDescripcionComprobante valNumeroDocumento, valTipoDeCxC, NombreClienteDeLaCxC, DescripcionDeLaCxC
   Set insCXCNavigator = Nothing
   DescripcionBuscada = "CxC N°" & valNumeroDocumento & "/" & gTexto.DfMid(NombreClienteDeLaCxC, 1, 20)
   If DescripcionDeLaCxC <> "" Then
      DescripcionBuscada = DescripcionBuscada & "/" & DescripcionDeLaCxC
   End If
   DescripcionBuscada = gTexto.DfMid(DescripcionBuscada, 1, 40)
h_EXIT:
   fBuscaDescripcionDelComprobanteContableDeCxC = DescripcionBuscada
   On Error GoTo 0
   Exit Function
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
            "fBuscaDescripcionDelComprobanteContableDeCxC", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fBuscaDescripcionDelComprobanteContableDeCobranza(ByVal valNumeroDocumento As String) As String
   Dim DescripcionDeLaCobranza As String
   Dim NombreClienteDeLaCobranza As String
   Dim insCobranzaNavigator As clsCobranzaNavigator
   Dim DescripcionBuscada As String
   Dim Sql As String
   On Error GoTo h_ERROR
   Set insCobranzaNavigator = New clsCobranzaNavigator
   insCobranzaNavigator.sBuscaDatosDeLaCobranzaParaDescripcionComprobante _
               valNumeroDocumento, NombreClienteDeLaCobranza, DescripcionDeLaCobranza
   Set insCobranzaNavigator = Nothing
   DescripcionBuscada = "Cob.N°" & valNumeroDocumento & "/" & gTexto.DfMid(NombreClienteDeLaCobranza, 1, 20)
   If DescripcionDeLaCobranza <> "" Then
      DescripcionBuscada = DescripcionBuscada & "/" & DescripcionDeLaCobranza
   End If
   DescripcionBuscada = gTexto.DfMid(DescripcionBuscada, 1, 40)
h_EXIT:
   fBuscaDescripcionDelComprobanteContableDeCobranza = DescripcionBuscada
   On Error GoTo 0
   Exit Function
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
            "fBuscaDescripcionDelComprobanteContableDeCobranza", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
 End Function
Private Function fBuscaDescripcionDelComprobanteContableDeFactura(ByVal valNumeroDocumento As String, ByVal valTipoDocumento As enum_TipoDocumentoFactura) As String
   Dim NombreClienteDeLaFactura As String
   Dim insFacturaNavigator As clsFacturaNavigator
   Dim DescripcionBuscada As String
   Dim Sql As String
   On Error GoTo h_ERROR
   Set insFacturaNavigator = New clsFacturaNavigator
   insFacturaNavigator.sBuscaDatosDeLaFacturaParaDescripcionComprobante valNumeroDocumento, valTipoDocumento, NombreClienteDeLaFactura
   Set insFacturaNavigator = Nothing
   DescripcionBuscada = gTexto.DfMid("Fac.N°" & valNumeroDocumento & "/" & gTexto.DfMid(NombreClienteDeLaFactura, 1, 20), 1, 40)
h_EXIT:
   fBuscaDescripcionDelComprobanteContableDeFactura = DescripcionBuscada
   On Error GoTo 0
   Exit Function
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
            "fBuscaDescripcionDelComprobanteContableDeFactura", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
 End Function
Private Function fBuscaCodClienteSegunModulo(ByVal valNroDocOriginal, ByVal valModulo As enum_ComprobanteGeneradoPor, ByVal valTipoDocumento As Integer) As String
   Dim insCxC As clsCxCNavigator
   Dim insFactura As clsFacturaNavigator
   Dim insCobranza As clsCobranzaNavigator
   Dim insAnticipo As clsAnticipoNavigator
   Dim varCodigoCliente As String
   On Error GoTo h_ERROR
   Select Case valModulo
      Case eCG_CXC
         Set insCxC = New clsCxCNavigator
         valNroDocOriginal = gTexto.DfRight(valNroDocOriginal, gTexto.DfLen(valNroDocOriginal) - gTexto.DfLen(valTipoDocumento & vbTab))
         insCxC.SetNumero valNroDocOriginal
         If valTipoDocumento <> -1 Then
            insCxC.SetTipoCxCStr gEnumProyecto.enumTipoDeCxCToString(valTipoDocumento)
         End If
         If insCxC.fSearchByNumeroTipoDeCxC(insCxC.GetNumero, insCxC.GetTipoCxCAsEnum, True, False) Then
            varCodigoCliente = insCxC.GetCodigoCliente
         End If
      Case eCG_FACTURA, eCG_RESUMEN_DIARIO_VENTAS
         Set insFactura = New clsFacturaNavigator
         insFactura.setClaseDeTrabajo eCTFC_Factura
         valNroDocOriginal = gTexto.DfRight(valNroDocOriginal, gTexto.DfLen(valNroDocOriginal) - gTexto.DfLen(valTipoDocumento & vbTab))
         insFactura.SetNumero valNroDocOriginal
         If valTipoDocumento <> -1 Then
            insFactura.SetTipoDeDocumentoStr gEnumProyecto.enumTipodocumentoFacturaToString(valTipoDocumento)
         End If
         If insFactura.fSearchSelectConnection(False) Then
            varCodigoCliente = insFactura.GetCodigoCliente
         End If
      Case eCG_COBRANZA
         Set insCobranza = New clsCobranzaNavigator
         insCobranza.SetNumero valNroDocOriginal
         If insCobranza.fSearchByField("Numero", valNroDocOriginal, True, True) Then
            varCodigoCliente = insCobranza.GetCodigoCliente
         End If
      Case eCG_ANTICIPO
         Set insAnticipo = New clsAnticipoNavigator
         insAnticipo.SetConsecutivoAnticipo gConvert.ConvierteAlong(valNroDocOriginal)
         insAnticipo.SetTipoStr gEnumProyecto.enumTipoDeAnticipoToString(eTDA_COBRADO)
         If insAnticipo.fSearchSelectConnection(igNoMostrarMensaje) Then
            varCodigoCliente = insAnticipo.GetCodigoCliente
         End If
   End Select
   fBuscaCodClienteSegunModulo = varCodigoCliente
   Set insCxC = Nothing
   Set insFactura = Nothing
   Set insCobranza = Nothing
   Set insAnticipo = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fBuscaCodClienteSegunModulo", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fBuscaCodProveedorSegunModulo(ByVal valNroDocOriginal, ByVal valModulo As enum_ComprobanteGeneradoPor) As String
   Dim insPago As clsPagoNavigator
   Dim insAnticipo As clsAnticipoNavigator
   Dim varcodigoProveedor As String
   Dim CXP As clsCxPNavigator
   Dim insRendicion As clsRendicionNavigator
   On Error GoTo h_ERROR
   Set insPago = New clsPagoNavigator
   Set CXP = New clsCxPNavigator
   Set insRendicion = New clsRendicionNavigator
   Select Case valModulo
      Case eCG_CXP
            CXP.SetConsecutivoCxP valNroDocOriginal
            If CXP.fSearchByNumericField("ConsecutivoCxp", valNroDocOriginal, True, True, False) Then
               varcodigoProveedor = CXP.GetCodigoProveedor
            End If
      Case eCG_PAGOS
         insPago.SetNumeroComprobante valNroDocOriginal
         If insPago.fSearchByNumericField("NumeroComprobante", valNroDocOriginal, True, True, False) Then
            varcodigoProveedor = insPago.GetCodigoProveedor
         End If
      Case eCG_ANTICIPO
         Set insAnticipo = New clsAnticipoNavigator
         insAnticipo.SetConsecutivoAnticipo gConvert.ConvierteAlong(valNroDocOriginal)
         insAnticipo.SetTipoStr gEnumProyecto.enumTipoDeAnticipoToString(eTDA_PAGADO)
         If insAnticipo.fSearchSelectConnection(igNoMostrarMensaje) Then
            varcodigoProveedor = insAnticipo.GetCodigoProveedor
         End If
      Case eCG_REPOSICION
         insRendicion.SetConsecutivo valNroDocOriginal
         If insRendicion.fSearchByNumericField("Consecutivo", valNroDocOriginal, False, True, False) Then
            varcodigoProveedor = insRendicion.GetCodigoBeneficiario
         End If
   End Select
   fBuscaCodProveedorSegunModulo = varcodigoProveedor
   Set insPago = Nothing
   Set insAnticipo = Nothing
   Set CXP = Nothing
   Set insRendicion = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fBuscaCodProveedorSegunModulo", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fElProveedorManejaAuxiliar(ByVal valCodProveedor As String, _
      ByVal valModulo As enum_ComprobanteGeneradoPor, ByVal valTipoDeCtaContabilizacion As Integer, ByVal valTipoDocumento As Integer) As Boolean
   Dim insProveedor As clsProveedorNavigator
   Dim insCuenta As clsCuentaNavigator
   Dim varAuxiliar As Boolean
   Dim insCnxAos As clsConexionesSawAOS
   Dim refCodigoProveedor As String
   Dim refNombreProveedor As String
   On Error GoTo h_ERROR
   Set insCuenta = New clsCuentaNavigator
   Set insProveedor = New clsProveedorNavigator
   varAuxiliar = False
   Set insCnxAos = New clsConexionesSawAOS
   If Not gTexto.fLimpiaStringdeBlancosAAmbosLados(valCodProveedor) = "" Then
      If insCnxAos.fSelectAndSetValuesOfProveedorFromAOS(insProveedor, refCodigoProveedor, refNombreProveedor, valCodProveedor, "") Then
         If valTipoDeCtaContabilizacion = gProyReglasDeContabilizacion.eTC_ReglaProvCxP And insProveedor.GetCuentaContableCxP <> "" Then
            varAuxiliar = insCuenta.fLaCuentaManejaAuxiliares(insProveedor.GetCuentaContableCxP)
         ElseIf valTipoDeCtaContabilizacion = gProyReglasDeContabilizacion.eTC_ReglaProvGasto And insProveedor.GetCuentaContableGastos <> "" Then
            varAuxiliar = insCuenta.fLaCuentaManejaAuxiliares(insProveedor.GetCuentaContableGastos)
         ElseIf valTipoDeCtaContabilizacion = gProyReglasDeContabilizacion.eTC_ReglaAntPagProveedor And insProveedor.GetCuentaContableAnticipo <> "" Then
            varAuxiliar = insCuenta.fLaCuentaManejaAuxiliares(insProveedor.GetCuentaContableAnticipo)
         Else
            varAuxiliar = gProyReglasDeContabilizacion.fLaReglaGeneralParaEsteModuloUsaAuxiliar(valModulo, valTipoDeCtaContabilizacion, valTipoDocumento)
         End If
      End If
   Else
      varAuxiliar = gProyReglasDeContabilizacion.fLaReglaGeneralParaEsteModuloUsaAuxiliar(valModulo, valTipoDeCtaContabilizacion, valTipoDocumento)
   End If
   Set insCnxAos = Nothing
   fElProveedorManejaAuxiliar = varAuxiliar
   Set insProveedor = Nothing
   Set insCuenta = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fElProveedorManejaAuxiliar", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fElClienteManejaAuxiliar(ByVal valCodCliente As String, ByVal valModulo As enum_ComprobanteGeneradoPor, _
      ByVal valTipoDeCtaContabilizacion As Integer, ByVal valTipoDocumento As Integer) As Boolean
   Dim insCliente As clsClienteNavigator
   Dim insCuenta As clsCuentaNavigator
   Dim varAuxiliar As Boolean
   On Error GoTo h_ERROR
   Set insCuenta = New clsCuentaNavigator
   Set insCliente = New clsClienteNavigator
   varAuxiliar = False
   If insCliente.fSearchByField("Codigo", valCodCliente, True, True) Then
      If valTipoDeCtaContabilizacion = gProyReglasDeContabilizacion.eTC_ReglaCliCxC And insCliente.GetCuentaContableCxC <> "" Then
         varAuxiliar = insCuenta.fLaCuentaManejaAuxiliares(insCliente.GetCuentaContableCxC)
      ElseIf valTipoDeCtaContabilizacion = gProyReglasDeContabilizacion.eTC_ReglaCliIngreso And insCliente.GetCuentaContableIngresos <> "" Then
         varAuxiliar = insCuenta.fLaCuentaManejaAuxiliares(insCliente.GetCuentaContableIngresos)
      ElseIf valTipoDeCtaContabilizacion = gProyReglasDeContabilizacion.eTC_ReglaAntCobCliente And insCliente.GetCuentaContableAnticipo <> "" Then
         varAuxiliar = insCuenta.fLaCuentaManejaAuxiliares(insCliente.GetCuentaContableAnticipo)
      Else
         varAuxiliar = gProyReglasDeContabilizacion.fLaReglaGeneralParaEsteModuloUsaAuxiliar(valModulo, valTipoDeCtaContabilizacion, valTipoDocumento)
      End If
   Else
      varAuxiliar = gProyReglasDeContabilizacion.fLaReglaGeneralParaEsteModuloUsaAuxiliar(valModulo, valTipoDeCtaContabilizacion, valTipoDocumento)
   End If
   fElClienteManejaAuxiliar = varAuxiliar
   Set insCliente = Nothing
   Set insCuenta = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fElClienteManejaAuxiliar", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fCuentaDeMovimientoBancario(ByVal valNoDocumentoOrigen As String) As String
   Dim insCobranzaNavigator As clsCobranzaNavigator
   Dim insCuentaBancaria As clsCuentaBancariaNavigator
   Dim varCodigoCtaBancaria As String
   Dim cuentaDeMovimientoBancario As String
   Dim insCnxAos As clsConexionesSawAOS
   On Error GoTo h_ERROR
   Set insCobranzaNavigator = New clsCobranzaNavigator
   Set insCuentaBancaria = New clsCuentaBancariaNavigator
   cuentaDeMovimientoBancario = ""
   If insCobranzaNavigator.fSearchByField("Numero", valNoDocumentoOrigen, False, True, False) Then
      varCodigoCtaBancaria = insCobranzaNavigator.GetCodigoCuentaBancaria
      Set insCnxAos = New clsConexionesSawAOS
      If insCnxAos.fSelectAndSetValuesOfCuentaBancariaFromAOS(insCuentaBancaria, varCodigoCtaBancaria, "", enum_StatusCtaBancaria.eSC_ACTIVA, "Gv_CuentaBancaria_B1.Status", "", False, False, False, True) Then
         cuentaDeMovimientoBancario = insCuentaBancaria.GetCuentaContable
         If Trim(insCuentaBancaria.GetCuentaContable) = "" Then
            cuentaDeMovimientoBancario = gProyReglasDeContabilizacion.GetCuentaMovBancarioBancosDebe
         End If
      End If
      Set insCnxAos = Nothing
   End If
   Set insCobranzaNavigator = Nothing
   Set insCuentaBancaria = Nothing
h_EXIT:
   fCuentaDeMovimientoBancario = cuentaDeMovimientoBancario
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fCuentaDeMovimientoBancario", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLFacturasEntreFechasPorArticulo(ByVal valFechaDesde As Date, ByVal valFechaHasta As Date, _
            ByVal valEsUnReverso As Boolean, ByVal valDescripcionDelAsiento As String, _
            ByVal valContabilizarSolo1Documento As Boolean, ByVal valNoDocumentoOrigen As String, _
            ByVal valTipoDocumento As Integer) As String
   Dim ConjuntoDeFacturasSinContabilizar As String
   Dim DebeOhaber As String
   Dim ColocarAuxiliar As Boolean
   Dim codigoDelAuxiliar As String
   Dim Sql As String
   Dim CodigoCuentaIVA As String
   Dim CodigoCuentaIVAUsaAuxiliar As Boolean
   Dim SqlMontoCargosMenosDescuentos  As String
   Dim SqlTasaDeCambio As String
   Dim CondicionDeStatusAnulado As String
   Dim varCodigoCliente As String
   Dim varAuxiliarCxC As Boolean
   Dim varSQLCuentaContableCliente As String
   Dim sqlMtoRenglon As String
   Dim insFactura As clsFacturaNavigator
   Dim insRenglonFactura As clsRenglonFacturaNavigator
   Dim insCliente As clsClienteNavigator
   Dim insArticulo As clsArticuloInventarioNavigator
   Dim numeroReferencia As String
   Dim varCodigoCentroDeCosto As String
   Dim varCuentaContaDescFac As String
   Dim sqlMtoRenglonConDcto As String
   Dim sqlMtoRenglonConCase As String
   Dim numeroFactura As String
   Dim vCodigoCuentaIGTF As String
   On Error GoTo h_ERROR
   Set insCliente = New clsClienteNavigator
   Set insFactura = New clsFacturaNavigator
   Set insRenglonFactura = New clsRenglonFacturaNavigator
   Set insArticulo = New clsArticuloInventarioNavigator
   insFactura.setClaseDeTrabajo eCTFC_Factura
   insRenglonFactura.setClaseDeTrabajo eCTFC_Factura
   CodigoCuentaIVA = ""
   Sql = ""
   varCodigoCliente = fBuscaCodClienteSegunModulo(valNoDocumentoOrigen, eCG_FACTURA, valTipoDocumento)
   varAuxiliarCxC = fElClienteManejaAuxiliar(varCodigoCliente, eCG_FACTURA, gProyReglasDeContabilizacion.eTC_ReglaCliCxC, valTipoDocumento)
   CondicionDeStatusAnulado = gUtilSQL.fFirst(insFactura.GetTableName & "." & insFactura.getFN_STATUS_FACTURA, "NONE") & " = " & _
                              gUtilSQL.fSimpleSqlValue(gConvert.enumerativoAChar(enum_StatusFactura.eSF_ANULADA))
   SqlTasaDeCambio = insFactura.GetTableName & "." & insFactura.getFN_CAMBIO_ABOLIVARES
   ConjuntoDeFacturasSinContabilizar = fSQLConjuntoRecordsDeContabilizacion1oMasDocumentos(eCG_FACTURA, valContabilizarSolo1Documento, valNoDocumentoOrigen, valFechaDesde, valFechaHasta, valTipoDocumento)
   
   If ConjuntoDeFacturasSinContabilizar = "" Then
      GoTo h_EXIT
   End If
   varSQLCuentaContableCliente = gUtilSQL.getIIF(gUtilSQL.fFirst(insCliente.GetTableName & ".CuentaContableCxC", "NONE") & " <> ''", _
               gUtilSQL.fFirst(insCliente.GetTableName & ".CuentaContableCxC", "NONE"), _
               gUtilSQL.fSimpleSqlValue(gProyReglasDeContabilizacion.GetCuentaFacturacionCxCClientes))
   codigoDelAuxiliar = insFactura.GetTableName & "." & insFactura.getFN_CODIGO_CLIENTE
   varCodigoCentroDeCosto = "Almacen.CodigoCc"
   varCuentaContaDescFac = gProyReglasDeContabilizacion.GetCuentaFacturacionDescuentos
   vCodigoCuentaIGTF = gProyReglasDeContabilizacion.GetCuentaCreditoBancarioGasto
   Sql = "SELECT "
   Sql = Sql & insFactura.GetTableName & "." & insFactura.getFN_CODIGO_CLIENTE & ", "
   Sql = Sql & fAgregaUnaCuentaAlSQL("DEBE", valEsUnReverso, _
               "SUM(" & gUtilSQL.fRoundNDecimales(insFactura.GetTableName & "." & insFactura.getFN_TOTAL_FACTURA & " * " & SqlTasaDeCambio, 2) & " + factura.IGTFML )", _
               varSQLCuentaContableCliente, varAuxiliarCxC, codigoDelAuxiliar, _
               numeroReferencia, valDescripcionDelAsiento, CondicionDeStatusAnulado, varCodigoCentroDeCosto) & ", "
               
    Sql = Sql & fAgregaUnaCuentaAlSQL("DEBE", valEsUnReverso, _
               "SUM(" & gUtilSQL.fRoundNDecimales(insFactura.GetTableName & ".MontoDescuento1 * " & SqlTasaDeCambio, 2) & ")", _
               varCuentaContaDescFac, varAuxiliarCxC, codigoDelAuxiliar, _
               numeroReferencia, valDescripcionDelAsiento, CondicionDeStatusAnulado, varCodigoCentroDeCosto) & ", "
   numeroFactura = gTexto.DfMid(valNoDocumentoOrigen, gTexto.DfInStr(valNoDocumentoOrigen, gTexto.fSeparadorStandardDeElementosString) + 1)
   If insFactura.fSearchByNumeroTipoDocumento(numeroFactura, valTipoDocumento, False, True, False, True) Then
      CodigoCuentaIVA = fEscogeCuentaDeIVASiEsDebitoOcredito(fCambiaDebeHaberSiEsUnReverso("HABER", valEsUnReverso), _
                                 eTD_ALICUOTAGENERAL, eCG_FACTURA, CodigoCuentaIVAUsaAuxiliar, insFactura.GetEsOriginalmenteDiferida)
   End If
   Sql = Sql & fAgregaUnaCuentaAlSQL("HABER", valEsUnReverso, _
               "SUM(" & gUtilSQL.fRoundNDecimales(insFactura.GetTableName & "." & insFactura.getFN_TOTAL_IVA & " * " & SqlTasaDeCambio, 2) & ")", _
               CodigoCuentaIVA, CodigoCuentaIVAUsaAuxiliar, codigoDelAuxiliar, _
               numeroReferencia, valDescripcionDelAsiento, CondicionDeStatusAnulado, varCodigoCentroDeCosto)
   Sql = Sql & ", " & fAgregaUnaCuentaAlSQL("HABER", valEsUnReverso, _
         "SUM(Factura.IGTFML)", _
         vCodigoCuentaIGTF, False, "", _
         numeroReferencia, valDescripcionDelAsiento, CondicionDeStatusAnulado, "")
   Sql = Sql & " FROM " & insFactura.GetTableName & " INNER JOIN " & insCliente.GetTableName
   Sql = Sql & " ON " & insFactura.GetTableName & "." & insFactura.getFN_CONSECUTIVO_COMPANIA & " = "
   Sql = Sql & insCliente.GetTableName & ".ConsecutivoCompania AND "
   Sql = Sql & insFactura.GetTableName & "." & insFactura.getFN_CODIGO_CLIENTE & " = "
   Sql = Sql & insCliente.GetTableName & ".Codigo"
   If (gProyCompaniaActual.GetUsaCentroDeCostos And gProyParametrosCompania.GetAsociaCentroDeCostoyAlmacen) Then
      Sql = Sql & " INNER JOIN Almacen on  "
      Sql = Sql & insCliente.GetTableName & ".ConsecutivoCompania = Almacen.ConsecutivoCompania AND "
      Sql = Sql & insFactura.GetTableName & ".CodigoAlmacen = Almacen.Codigo "
   End If
   Sql = Sql & " WHERE " & insFactura.GetTableName & "." & insFactura.getFN_CONSECUTIVO_COMPANIA & " = " & gProyCompaniaActual.GetConsecutivoCompania
   Sql = Sql & " AND " & insFactura.GetTableName & "." & insFactura.getFN_STATUS_FACTURA
   If valEsUnReverso Then
      Sql = Sql & " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA)
   Else
      Sql = Sql & " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_EMITIDA)
   End If
   Sql = Sql & " AND " & insFactura.GetTableName & "." & insFactura.getFN_TIPO_DE_DOCUMENTO & gUtilSQL.CharConcat & "'" & gTexto.fSeparadorStandardDeElementosString & "'" & gUtilSQL.CharConcat & insFactura.GetTableName & "." & insFactura.getFN_NUMERO & " IN (" & ConjuntoDeFacturasSinContabilizar & ")"
   Sql = Sql & " AND " & gUtilSQL.DfSQLDateValueBetween(insFactura.GetTableName & "." & insFactura.getFN_FECHA, valFechaDesde, valFechaHasta)
   Sql = Sql & " GROUP BY " & insFactura.GetTableName & "." & insFactura.getFN_CODIGO_CLIENTE
   If (gProyCompaniaActual.GetUsaCentroDeCostos And gProyParametrosCompania.GetAsociaCentroDeCostoyAlmacen) Then
        Sql = Sql & ", " & varCodigoCentroDeCosto
   End If
   '  ESTAS INSTRUCCIONES SON PARA CONTABLIZAR EL TOTAL FACTURA POR ARTÍCULO
   sqlMtoRenglon = "(" & insRenglonFactura.GetTableName & "." & insRenglonFactura.getFN_PRECIO_SIN_IVA & _
                     " * " & insRenglonFactura.GetTableName & "." & insRenglonFactura.getFN_CANTIDAD & ") "
   sqlMtoRenglon = "(" & sqlMtoRenglon & " * ( 1 - " & _
                     insRenglonFactura.GetTableName & "." & insRenglonFactura.getFN_PORCENTAJE_DESCUENTO & " / 100))"
   sqlMtoRenglon = "(" & sqlMtoRenglon & " * ( 1 - " & _
                     insFactura.GetTableName & "." & insFactura.getFN_PORCENTAJE_DESCUENTO & " / 100))"
   sqlMtoRenglon = gUtilSQL.fRoundNDecimales(sqlMtoRenglon & " * " & SqlTasaDeCambio, 2)
   
   sqlMtoRenglonConDcto = "(" & insRenglonFactura.GetTableName & "." & insRenglonFactura.getFN_PRECIO_SIN_IVA & _
                     " * " & insRenglonFactura.GetTableName & "." & insRenglonFactura.getFN_CANTIDAD & ") "
   sqlMtoRenglonConDcto = "(" & sqlMtoRenglonConDcto & " * ( 1 - " & _
                     insRenglonFactura.GetTableName & "." & insRenglonFactura.getFN_PORCENTAJE_DESCUENTO & " / 100))"
   sqlMtoRenglonConDcto = gUtilSQL.fRoundNDecimales(sqlMtoRenglonConDcto & " * " & SqlTasaDeCambio, 2)
   
   sqlMtoRenglonConCase = gUtilSQL.getIIF("SUM(factura.MontoDescuento1) > 0", "SUM(" & sqlMtoRenglonConDcto & ")", "SUM(" & sqlMtoRenglon & ")", True)
   
   Sql = Sql & " UNION ALL SELECT "
   Sql = Sql & gUtilSQL.fSimpleSqlValue(UCase(insArticulo.GetTableName)) & ", "
   
   Sql = Sql & fAgregaUnaCuentaAlSQL("HABER", valEsUnReverso, sqlMtoRenglonConCase, _
               gUtilSQL.getIIF(insArticulo.GetTableName & ".CuentaContableIngreso = ''" & _
               " OR " & gUtilSQL.DfSQLIsNull(insArticulo.GetTableName & ".CuentaContableIngreso"), _
               gUtilSQL.getIIF(gUtilSQL.fFirst(insCliente.GetTableName & ".CuentaContableIngresos", "NONE") & " <> ''", _
               gUtilSQL.fFirst(insCliente.GetTableName & ".CuentaContableIngresos", "NONE"), _
               gUtilSQL.fSimpleSqlValue(gProyReglasDeContabilizacion.GetCuentaFacturacionMontoTotalFactura), True), _
               insArticulo.GetTableName & ".CuentaContableIngreso", True), False, "", _
               numeroReferencia, valDescripcionDelAsiento, CondicionDeStatusAnulado, varCodigoCentroDeCosto) & ", "
               
   Sql = Sql & fAgregaUnaCuentaAlSQL("", False, "0", "", False, "", "", "", "", varCodigoCentroDeCosto) & ", "
   Sql = Sql & fAgregaUnaCuentaAlSQL("", False, "0", "", False, "", "", "", "", varCodigoCentroDeCosto) & ", "
   Sql = Sql & fAgregaUnaCuentaAlSQL("", False, "0", "", False, "", "", "", "", varCodigoCentroDeCosto)
   Sql = Sql & " FROM " & insFactura.GetTableName & " INNER JOIN ("
   Sql = Sql & insArticulo.GetTableName & " INNER JOIN "
   Sql = Sql & insRenglonFactura.GetTableName & " ON ("
   Sql = Sql & insArticulo.GetTableName & ".Codigo" & _
               " = " & insRenglonFactura.GetTableName & "." & insRenglonFactura.getFN_ARTICULO
   Sql = Sql & ") AND (" & insArticulo.GetTableName & ".ConsecutivoCompania" & _
               " = " & insRenglonFactura.GetTableName & "." & insRenglonFactura.getFN_CONSECUTIVO_COMPANIA
   Sql = Sql & ")) ON (" & insFactura.GetTableName & "." & insFactura.getFN_NUMERO & _
               " = " & insRenglonFactura.GetTableName & "." & insRenglonFactura.getFN_NUMERO_FACTURA
   Sql = Sql & ") AND (" & insFactura.GetTableName & "." & insFactura.getFN_CONSECUTIVO_COMPANIA & _
               " = " & insRenglonFactura.GetTableName & "." & insRenglonFactura.getFN_CONSECUTIVO_COMPANIA
   Sql = Sql & ") AND (" & insFactura.GetTableName & "." & insFactura.getFN_TIPO_DE_DOCUMENTO & _
               " = " & insRenglonFactura.GetTableName & "." & insRenglonFactura.getFN_TIPO_DE_DOCUMENTO & ")"
   Sql = Sql & " INNER JOIN " & insCliente.GetTableName
   Sql = Sql & " ON " & insFactura.GetTableName & "." & insFactura.getFN_CONSECUTIVO_COMPANIA & " = "
   Sql = Sql & insCliente.GetTableName & ".ConsecutivoCompania AND "
   Sql = Sql & insFactura.GetTableName & "." & insFactura.getFN_CODIGO_CLIENTE & " = "
   Sql = Sql & insCliente.GetTableName & ".Codigo"
   If (gProyCompaniaActual.GetUsaCentroDeCostos And gProyParametrosCompania.GetAsociaCentroDeCostoyAlmacen) Then
      Sql = Sql & " INNER JOIN Almacen on  "
      Sql = Sql & insFactura.GetTableName & ".ConsecutivoCompania = Almacen.ConsecutivoCompania AND "
      Sql = Sql & insFactura.GetTableName & ".CodigoAlmacen = Almacen.Codigo "
   End If
   Sql = Sql & " WHERE " & insFactura.GetTableName & "." & insFactura.getFN_CONSECUTIVO_COMPANIA & " = " & gProyCompaniaActual.GetConsecutivoCompania
   Sql = Sql & " AND " & insFactura.GetTableName & "." & insFactura.getFN_STATUS_FACTURA
   If valEsUnReverso Then
      Sql = Sql & " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA)
   Else
      Sql = Sql & " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_EMITIDA)
   End If
   Sql = Sql & " AND " & insFactura.GetTableName & "." & insFactura.getFN_TIPO_DE_DOCUMENTO & gUtilSQL.CharConcat & "'" & gTexto.fSeparadorStandardDeElementosString & "'" & gUtilSQL.CharConcat & insFactura.GetTableName & "." & insFactura.getFN_NUMERO & " IN (" & ConjuntoDeFacturasSinContabilizar & ")"
   Sql = Sql & " AND " & gUtilSQL.DfSQLDateValueBetween(insFactura.GetTableName & "." & insFactura.getFN_FECHA, valFechaDesde, valFechaHasta)
   Sql = Sql & " AND " & insFactura.GetTableName & "." & insFactura.getFN_TIPO_DE_DOCUMENTO & " <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_RESUMENDIARIODEVENTAS)
   Sql = Sql & " GROUP BY "
   Sql = Sql & insArticulo.GetTableName & ".CuentaContableIngreso"
    If (gProyCompaniaActual.GetUsaCentroDeCostos And gProyParametrosCompania.GetAsociaCentroDeCostoyAlmacen) Then
        Sql = Sql & ", " & varCodigoCentroDeCosto
   End If
   If Not gProyReglasDeContabilizacion.GetAgruparPorCuentaDeArticulo Then
      Sql = Sql & ", "
      Sql = Sql & insRenglonFactura.GetTableName & "." & insRenglonFactura.getFN_CONSECUTIVO_RENGLON
   End If
   If gProyParametrosCompania.GetUsarOtrosCargoDeFactura Then
      Sql = Sql & " UNION ALL "
      Sql = Sql & fSQLOtrosCargosYDescuentosDeFacturaEntreFechas(valFechaDesde, valFechaHasta, valEsUnReverso, _
            valDescripcionDelAsiento, valContabilizarSolo1Documento, valNoDocumentoOrigen, True, valTipoDocumento)
   End If
   Set insFactura = Nothing
   Set insCliente = Nothing
   Set insArticulo = Nothing
   Set insRenglonFactura = Nothing
h_EXIT:
   fSQLFacturasEntreFechasPorArticulo = Sql
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLFacturasEntreFechasPorArticulo", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLOtrosCargosYDescuentosDeFacturaEntreFechas(ByVal valFechaDesde As Date, ByVal valFechaHasta As Date, _
         ByVal valEsUnReverso As Boolean, ByVal valDescripcionDelAsiento As String, _
         ByVal valContabilizarSolo1Documento As Boolean, ByVal valNoDocumentoOrigen As String, _
         ByVal valContabilizacionPorArticulo As Boolean, ByVal valTipoDocumento As Integer)
   Dim insFactura As clsFacturaNavigator
   Dim insRenglonOtrosCargosFactura As clsRenglonOtrosCargosFacturaNav
   Dim insOtrosCargosDeFactura As clsOtrosCargosDeFacturaNavigator
   Dim insCliente As clsClienteNavigator
   Dim Sql As String
   Dim SqlTasaDeCambio As String
   Dim codigoDelAuxiliar As String
   Dim CondicionDeStatusAnulado As String
   Dim varCodigoCliente As String
   Dim varAuxiliarCxC As Boolean
   Dim varSQLCuentaContableCliente As String
   Dim numeroFactura  As String
   On Error GoTo h_ERROR
   Set insCliente = New clsClienteNavigator
   Set insFactura = New clsFacturaNavigator
   Set insRenglonOtrosCargosFactura = New clsRenglonOtrosCargosFacturaNav
   Set insOtrosCargosDeFactura = New clsOtrosCargosDeFacturaNavigator
   insFactura.setClaseDeTrabajo eCTFC_Factura
   insRenglonOtrosCargosFactura.setClaseDeTrabajo eCTFC_Factura
   numeroFactura = gTexto.DfMid(valNoDocumentoOrigen, gTexto.DfInStr(valNoDocumentoOrigen, gTexto.fSeparadorStandardDeElementosString) + 1)
   varCodigoCliente = fBuscaCodClienteSegunModulo(valNoDocumentoOrigen, eCG_FACTURA, valTipoDocumento)
   varAuxiliarCxC = fElClienteManejaAuxiliar(varCodigoCliente, eCG_FACTURA, gProyReglasDeContabilizacion.eTC_ReglaCliCxC, valTipoDocumento)
   SqlTasaDeCambio = insFactura.GetTableName & "." & insFactura.getFN_CAMBIO_ABOLIVARES
   codigoDelAuxiliar = insFactura.GetTableName & "." & insFactura.getFN_CODIGO_CLIENTE
   CondicionDeStatusAnulado = gUtilSQL.fFirst(insFactura.GetTableName & "." & insFactura.getFN_STATUS_FACTURA, "NONE") & " = " & _
                              gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA)
   varSQLCuentaContableCliente = gUtilSQL.getIIF(gUtilSQL.fFirst(insCliente.GetTableName & ".CuentaContableCxC", "NONE") & " <> ''", _
               gUtilSQL.fFirst(insCliente.GetTableName & ".CuentaContableCxC", "NONE"), _
               gUtilSQL.fSimpleSqlValue(gProyReglasDeContabilizacion.GetCuentaFacturacionCxCClientes), True)
   codigoDelAuxiliar = insFactura.GetTableName & "." & insFactura.getFN_CODIGO_CLIENTE
'  ESTAS INSTRUCCIONES SON PARA LOS CARGOS
   Sql = Sql & "SELECT 'CARGOS', "
   Sql = Sql & fAgregaUnaCuentaAlSQL("HABER", valEsUnReverso, _
                  "SUM(ABS(" & insRenglonOtrosCargosFactura.GetTableName & "." & insRenglonOtrosCargosFactura.getFN_TOTAL_RENGLON & ") * " & _
                     SqlTasaDeCambio & ")", _
                     gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull(insOtrosCargosDeFactura.GetTableName & "." _
                     & "CuentaContableOtrosCargos") & " OR " & _
                     insOtrosCargosDeFactura.GetTableName & "." _
                     & "CuentaContableOtrosCargos = ''", _
                     gUtilSQL.fSimpleSqlValue(gProyReglasDeContabilizacion.GetCuentaFacturacionCargos), _
                     insOtrosCargosDeFactura.GetTableName & ".CuentaContableOtrosCargos", _
                     True), False, codigoDelAuxiliar, valNoDocumentoOrigen, "Cargos-" & valDescripcionDelAsiento, CondicionDeStatusAnulado, "") & ", "
   Sql = Sql & fAgregaUnaCuentaAlSQL("", False, "0", "", False, "", "", "", "", "")
   If Not valContabilizacionPorArticulo Then
      Sql = Sql & ", " & fAgregaUnaCuentaAlSQL("", False, "0", "", False, "", "", "", "", "")
   End If
   Sql = Sql & ", " & fAgregaUnaCuentaAlSQL("", False, "0", "", False, "", "", "", "", "")
   Sql = Sql & ", " & fAgregaUnaCuentaAlSQL("", False, "0", "", False, "", "", "", "", "")
   Sql = Sql & " FROM " & insFactura.GetTableName & " INNER JOIN ("
   Sql = Sql & insOtrosCargosDeFactura.GetTableName & " INNER JOIN "
   Sql = Sql & insRenglonOtrosCargosFactura.GetTableName
   Sql = Sql & " ON (" & insOtrosCargosDeFactura.GetTableName & ".Codigo" & _
               " = " & insRenglonOtrosCargosFactura.GetTableName & "." & insRenglonOtrosCargosFactura.getFN_CODIGO_DE_CARGO
   Sql = Sql & ") AND (" & insOtrosCargosDeFactura.GetTableName & ".ConsecutivoCompania" & _
               " = " & insRenglonOtrosCargosFactura.GetTableName & "." & insRenglonOtrosCargosFactura.getFN_CONSECUTIVO_COMPANIA
   Sql = Sql & ")) ON (" & insRenglonOtrosCargosFactura.GetTableName & "." & insRenglonOtrosCargosFactura.getFN_CONSECUTIVO_COMPANIA _
             & " = " & insFactura.GetTableName & "." & insFactura.getFN_CONSECUTIVO_COMPANIA
   Sql = Sql & " AND " & insRenglonOtrosCargosFactura.GetTableName & "." & insRenglonOtrosCargosFactura.getFN_NUMERO_FACTURA _
             & " = " & insFactura.GetTableName & "." & insFactura.getFN_NUMERO
   Sql = Sql & " AND " & insRenglonOtrosCargosFactura.GetTableName & "." & insRenglonOtrosCargosFactura.getFN_TIPO_DE_DOCUMENTO _
             & " = " & insFactura.GetTableName & "." & insFactura.getFN_TIPO_DE_DOCUMENTO & ")"
   Sql = Sql & " WHERE " & insRenglonOtrosCargosFactura.GetTableName & "." & insRenglonOtrosCargosFactura.getFN_CONSECUTIVO_COMPANIA & " = " & gProyCompaniaActual.GetConsecutivoCompania
   Sql = Sql & " AND " & insRenglonOtrosCargosFactura.GetTableName & "." & insRenglonOtrosCargosFactura.getFN_NUMERO_FACTURA & " = " & _
                            gUtilSQL.fSimpleSqlValue(numeroFactura)
   Sql = Sql & " AND " & insRenglonOtrosCargosFactura.GetTableName & "." & insRenglonOtrosCargosFactura.getFN_COMO_APLICA_AL_TOTAL_FACTURA & " = " & _
                         gUtilSQL.fSQLSimpleValueForEnum(enum_ComoAplicaOtrosCargosDeFactura.eCA_SUMA)
   Sql = Sql & " GROUP BY " & insOtrosCargosDeFactura.GetTableName & "." & _
                  "CuentaContableOtrosCargos"
   If Not gProyReglasDeContabilizacion.GetAgruparPorCargosDescuentos Then
      Sql = Sql & ", "
      Sql = Sql & insRenglonOtrosCargosFactura.GetTableName & "." & insRenglonOtrosCargosFactura.getFN_CONSECUTIVO_RENGLON
   End If
'  ESTAS INSTRUCCIONES SON PARA LOS DESCUENTOS
   Sql = Sql & " UNION ALL SELECT 'DESCUENTOS', "
   Sql = Sql & fAgregaUnaCuentaAlSQL("DEBE", valEsUnReverso, _
                  "SUM(ABS(" & insRenglonOtrosCargosFactura.GetTableName & "." & insRenglonOtrosCargosFactura.getFN_TOTAL_RENGLON & ") * " & _
                     SqlTasaDeCambio & ")", _
                     gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull(insOtrosCargosDeFactura.GetTableName & "." _
                     & "CuentaContableOtrosCargos") & " OR " & _
                     insOtrosCargosDeFactura.GetTableName & "." _
                     & "CuentaContableOtrosCargos = ''", _
                     gUtilSQL.fSimpleSqlValue(gProyReglasDeContabilizacion.GetCuentaFacturacionDescuentos), _
                     insOtrosCargosDeFactura.GetTableName & ".CuentaContableOtrosCargos", _
                     True), False, codigoDelAuxiliar, valNoDocumentoOrigen, "Descuentos-" & valDescripcionDelAsiento, CondicionDeStatusAnulado, "") & ", "
   Sql = Sql & fAgregaUnaCuentaAlSQL("", False, "0", "", False, "", "", "", "", "")
   If Not valContabilizacionPorArticulo Then
      Sql = Sql & ", " & fAgregaUnaCuentaAlSQL("", False, "0", "", False, "", "", "", "", "")
   End If
   Sql = Sql & ", " & fAgregaUnaCuentaAlSQL("", False, "0", "", False, "", "", "", "", "")
   Sql = Sql & ", " & fAgregaUnaCuentaAlSQL("", False, "0", "", False, "", "", "", "", "")
   Sql = Sql & " FROM " & insFactura.GetTableName & " INNER JOIN ("
   Sql = Sql & insOtrosCargosDeFactura.GetTableName & " INNER JOIN "
   Sql = Sql & insRenglonOtrosCargosFactura.GetTableName
   Sql = Sql & " ON (" & insOtrosCargosDeFactura.GetTableName & ".Codigo" & _
               " = " & insRenglonOtrosCargosFactura.GetTableName & "." & insRenglonOtrosCargosFactura.getFN_CODIGO_DE_CARGO
   Sql = Sql & ") AND (" & insOtrosCargosDeFactura.GetTableName & ".ConsecutivoCompania" & _
               " = " & insRenglonOtrosCargosFactura.GetTableName & "." & insRenglonOtrosCargosFactura.getFN_CONSECUTIVO_COMPANIA
   Sql = Sql & ")) ON (" & insRenglonOtrosCargosFactura.GetTableName & "." & insRenglonOtrosCargosFactura.getFN_CONSECUTIVO_COMPANIA _
             & " = " & insFactura.GetTableName & "." & insFactura.getFN_CONSECUTIVO_COMPANIA
   Sql = Sql & " AND " & insRenglonOtrosCargosFactura.GetTableName & "." & insRenglonOtrosCargosFactura.getFN_NUMERO_FACTURA _
             & " = " & insFactura.GetTableName & "." & insFactura.getFN_NUMERO
   Sql = Sql & " AND " & insRenglonOtrosCargosFactura.GetTableName & "." & insRenglonOtrosCargosFactura.getFN_TIPO_DE_DOCUMENTO _
             & " = " & insFactura.GetTableName & "." & insFactura.getFN_TIPO_DE_DOCUMENTO & ")"
   Sql = Sql & " WHERE " & insRenglonOtrosCargosFactura.GetTableName & "." & insRenglonOtrosCargosFactura.getFN_CONSECUTIVO_COMPANIA & " = " & gProyCompaniaActual.GetConsecutivoCompania
   Sql = Sql & " AND " & insRenglonOtrosCargosFactura.GetTableName & "." & insRenglonOtrosCargosFactura.getFN_NUMERO_FACTURA & " = " & _
                            gUtilSQL.fSimpleSqlValue(numeroFactura)
   Sql = Sql & " AND " & insRenglonOtrosCargosFactura.GetTableName & "." & insRenglonOtrosCargosFactura.getFN_COMO_APLICA_AL_TOTAL_FACTURA & " = " & _
                         gUtilSQL.fSQLSimpleValueForEnum(enum_ComoAplicaOtrosCargosDeFactura.eCA_RESTA)
   Sql = Sql & " GROUP BY " & insOtrosCargosDeFactura.GetTableName & "." & _
                  "CuentaContableOtrosCargos"
   If Not gProyReglasDeContabilizacion.GetAgruparPorCargosDescuentos Then
      Sql = Sql & ", "
      Sql = Sql & insRenglonOtrosCargosFactura.GetTableName & "." & insRenglonOtrosCargosFactura.getFN_CONSECUTIVO_RENGLON
   End If
'  ESTAS INSTRUCCIONES SON PARA LOS CARGOS - DESCUENTOS
   Sql = Sql & " UNION ALL SELECT 'CARGOSDESCUENTOS', "
   Sql = Sql & fAgregaUnaCuentaAlSQL("DEBE", valEsUnReverso, _
                  "SUM(" & insRenglonOtrosCargosFactura.GetTableName & "." & insRenglonOtrosCargosFactura.getFN_TOTAL_RENGLON & " * " & _
                        SqlTasaDeCambio & ")", _
                  varSQLCuentaContableCliente, _
                  varAuxiliarCxC, codigoDelAuxiliar, valNoDocumentoOrigen, _
                  valDescripcionDelAsiento & "OtrosCargoDescuentos", CondicionDeStatusAnulado, "") & ", "
   Sql = Sql & fAgregaUnaCuentaAlSQL("", False, "0", "", False, "", "", "", "", "")
   If Not valContabilizacionPorArticulo Then
      Sql = Sql & ", " & fAgregaUnaCuentaAlSQL("", False, "0", "", False, "", "", "", "", "")
   End If
   Sql = Sql & ", " & fAgregaUnaCuentaAlSQL("", False, "0", "", False, "", "", "", "", "")
   Sql = Sql & ", " & fAgregaUnaCuentaAlSQL("", False, "0", "", False, "", "", "", "", "")
   Sql = Sql & " FROM " & insCliente.GetTableName & " INNER JOIN ("
   Sql = Sql & insFactura.GetTableName & " INNER JOIN "
   Sql = Sql & insRenglonOtrosCargosFactura.GetTableName & " ON ("
   Sql = Sql & insFactura.GetTableName & "." & insFactura.getFN_NUMERO & _
               " = " & insRenglonOtrosCargosFactura.GetTableName & "." & insRenglonOtrosCargosFactura.getFN_NUMERO_FACTURA
   Sql = Sql & ") AND (" & insFactura.GetTableName & "." & insFactura.getFN_TIPO_DE_DOCUMENTO & _
               " = " & insRenglonOtrosCargosFactura.GetTableName & "." & insRenglonOtrosCargosFactura.getFN_TIPO_DE_DOCUMENTO
   Sql = Sql & ") AND (" & insFactura.GetTableName & "." & insFactura.getFN_CONSECUTIVO_COMPANIA & _
               " = " & insRenglonOtrosCargosFactura.GetTableName & "." & insRenglonOtrosCargosFactura.getFN_CONSECUTIVO_COMPANIA
   Sql = Sql & ")) ON (" & insCliente.GetTableName & ".Codigo" & _
               " = " & insFactura.GetTableName & "." & insFactura.getFN_CODIGO_CLIENTE
   Sql = Sql & ") AND (" & insCliente.GetTableName & ".ConsecutivoCompania" & _
               " = " & insFactura.GetTableName & "." & insFactura.getFN_CONSECUTIVO_COMPANIA & ")"
   Sql = Sql & " WHERE " & insRenglonOtrosCargosFactura.GetTableName & "." & insRenglonOtrosCargosFactura.getFN_CONSECUTIVO_COMPANIA & " = " & gProyCompaniaActual.GetConsecutivoCompania
   Sql = Sql & " AND " & insRenglonOtrosCargosFactura.GetTableName & "." & insRenglonOtrosCargosFactura.getFN_NUMERO_FACTURA & " = " & _
                            gUtilSQL.fSimpleSqlValue(numeroFactura)
   Sql = Sql & " AND " & insRenglonOtrosCargosFactura.GetTableName & "." & insRenglonOtrosCargosFactura.getFN_COMO_APLICA_AL_TOTAL_FACTURA & " <> " & _
                         gUtilSQL.fSQLSimpleValueForEnum(enum_ComoAplicaOtrosCargosDeFactura.eCA_NO_APLICA_SOLO_INFORMATIVO)
   Sql = Sql & " GROUP BY " & insFactura.GetTableName & "." & insFactura.getFN_CODIGO_CLIENTE
   Set insRenglonOtrosCargosFactura = Nothing
   Set insFactura = Nothing
   Set insOtrosCargosDeFactura = Nothing
h_EXIT:
   fSQLOtrosCargosYDescuentosDeFacturaEntreFechas = Sql
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLOtrosCargosYDescuentosDeFacturaEntreFechas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function setProcesoDeImpresionDeComprobanteDeMovBancario(ByRef refForm As Object, Optional ByVal valNumeroDeMovBancario As Long = 1)
   mImprimeComprobanteDeMovBancarioAlGrabar = True
   Set mPagoProcesoDeImpresionEspecial = refForm
   mNumeroDeMovBancario = valNumeroDeMovBancario
End Function

Private Function fSQLResumenVentasEntreFechasPorArticulo(ByVal valFechaDesde As Date, ByVal valFechaHasta As Date, _
            ByVal valEsUnReverso As Boolean, ByVal valDescripcionDelAsiento As String, _
            ByVal valContabilizarSolo1Documento As Boolean, ByVal valNoDocumentoOrigen As String, _
            ByVal valTipoDocumento As Integer) As String
   Dim ConjuntoDeFacturasSinContabilizar As String
   Dim DebeOhaber As String
   Dim ColocarAuxiliar As Boolean
   Dim codigoDelAuxiliar As String
   Dim Sql As String
   Dim CodigoCuentaIVA As String
   Dim CodigoCuentaIVAUsaAuxiliar As Boolean
   Dim SqlMontoCargosMenosDescuentos  As String
   Dim SqlTasaDeCambio As String
   Dim CondicionDeStatusAnulado As String
   Dim varCodigoCliente As String
   Dim varAuxiliarCxC As Boolean
   Dim varSQLCuentaContableCliente As String
   Dim sqlMtoRenglon As String
   Dim insFactura As clsFacturaNavigator
   Dim insRenglonFactura As clsRenglonFacturaNavigator
   Dim insCliente As clsClienteNavigator
   Dim insArticulo As clsArticuloInventarioNavigator
   Dim numeroReferencia As String
   On Error GoTo h_ERROR
   Set insCliente = New clsClienteNavigator
   Set insFactura = New clsFacturaNavigator
   Set insRenglonFactura = New clsRenglonFacturaNavigator
   Set insArticulo = New clsArticuloInventarioNavigator
   insFactura.setClaseDeTrabajo eCTFC_Factura
   insRenglonFactura.setClaseDeTrabajo eCTFC_Factura
   CodigoCuentaIVA = ""
   Sql = ""
   varCodigoCliente = fBuscaCodClienteSegunModulo(valNoDocumentoOrigen, eCG_RESUMEN_DIARIO_VENTAS, valTipoDocumento)
   varAuxiliarCxC = False
   CondicionDeStatusAnulado = gUtilSQL.fFirst(insFactura.GetTableName & "." & insFactura.getFN_STATUS_FACTURA, "NONE") & " = " & _
                              gUtilSQL.fSimpleSqlValue(gConvert.enumerativoAChar(enum_StatusFactura.eSF_ANULADA))
   SqlTasaDeCambio = insFactura.GetTableName & "." & insFactura.getFN_CAMBIO_ABOLIVARES
   numeroReferencia = valNoDocumentoOrigen
   If valContabilizarSolo1Documento Then
      gTexto.ReemplazaCaracteresEnElString numeroReferencia, vbTab, "-"
   End If
   ConjuntoDeFacturasSinContabilizar = fSQLConjuntoRecordsDeContabilizacion1oMasDocumentos(eCG_RESUMEN_DIARIO_VENTAS, valContabilizarSolo1Documento, valNoDocumentoOrigen, valFechaDesde, valFechaHasta, valTipoDocumento)
   If ConjuntoDeFacturasSinContabilizar = "" Then
      GoTo h_EXIT
   End If
   varSQLCuentaContableCliente = gUtilSQL.getIIF(gUtilSQL.fFirst(insCliente.GetTableName & ".CuentaContableCxC", "NONE") & " <> ''", _
               gUtilSQL.fFirst(insCliente.GetTableName & ".CuentaContableCxC", "NONE"), _
               gUtilSQL.fSimpleSqlValue(gProyReglasDeContabilizacion.GetCuentaFacturacionCxCClientes))
   codigoDelAuxiliar = insFactura.GetTableName & "." & insFactura.getFN_CODIGO_CLIENTE
   Sql = "SELECT "
   Sql = Sql & insFactura.GetTableName & "." & insFactura.getFN_CODIGO_CLIENTE & ", "
   Sql = Sql & fAgregaUnaCuentaAlSQL("DEBE", valEsUnReverso, _
               "SUM(" & insFactura.GetTableName & "." & insFactura.getFN_TOTAL_FACTURA & " * " & _
               SqlTasaDeCambio & ")", varSQLCuentaContableCliente, varAuxiliarCxC, codigoDelAuxiliar, _
               numeroReferencia, valDescripcionDelAsiento, CondicionDeStatusAnulado, "") & ", "
   CodigoCuentaIVA = fEscogeCuentaDeIVASiEsDebitoOcredito(fCambiaDebeHaberSiEsUnReverso("HABER", valEsUnReverso), eTD_ALICUOTAGENERAL, eCG_FACTURA, CodigoCuentaIVAUsaAuxiliar)
   Sql = Sql & fAgregaUnaCuentaAlSQL("HABER", valEsUnReverso, _
               "SUM(" & insFactura.GetTableName & "." & insFactura.getFN_TOTAL_IVA & " * " & _
               SqlTasaDeCambio & ")", CodigoCuentaIVA, CodigoCuentaIVAUsaAuxiliar, codigoDelAuxiliar, _
               numeroReferencia, valDescripcionDelAsiento, CondicionDeStatusAnulado, "")
   Sql = Sql & " FROM " & insFactura.GetTableName & " INNER JOIN " & insCliente.GetTableName
   Sql = Sql & " ON " & insFactura.GetTableName & "." & insFactura.getFN_CONSECUTIVO_COMPANIA & " = "
   Sql = Sql & insCliente.GetTableName & ".ConsecutivoCompania AND "
   Sql = Sql & insFactura.GetTableName & "." & insFactura.getFN_CODIGO_CLIENTE & " = "
   Sql = Sql & insCliente.GetTableName & ".Codigo"
   Sql = Sql & " WHERE " & insFactura.GetTableName & "." & insFactura.getFN_CONSECUTIVO_COMPANIA & " = " & gProyCompaniaActual.GetConsecutivoCompania
   Sql = Sql & " AND " & insFactura.GetTableName & "." & insFactura.getFN_STATUS_FACTURA
   If valEsUnReverso Then
      Sql = Sql & " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA)
   Else
      Sql = Sql & " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_EMITIDA)
   End If
   Sql = Sql & " AND " & insFactura.GetTableName & "." & insFactura.getFN_TIPO_DE_DOCUMENTO & gUtilSQL.CharConcat & "'" & gTexto.fSeparadorStandardDeElementosString & "'" & gUtilSQL.CharConcat & insFactura.GetTableName & "." & insFactura.getFN_NUMERO & " IN (" & ConjuntoDeFacturasSinContabilizar & ")"
   Sql = Sql & " AND " & gUtilSQL.DfSQLDateValueBetween(insFactura.GetTableName & "." & insFactura.getFN_FECHA, valFechaDesde, valFechaHasta)
   Sql = Sql & " GROUP BY " & insFactura.GetTableName & "." & insFactura.getFN_CODIGO_CLIENTE
   '  ESTAS INSTRUCCIONES SON PARA CONTABLIZAR EL TOTAL FACTURA POR ARTÍCULO
   sqlMtoRenglon = "(" & insRenglonFactura.GetTableName & "." & insRenglonFactura.getFN_PRECIO_SIN_IVA & _
                     " * " & insRenglonFactura.GetTableName & "." & insRenglonFactura.getFN_CANTIDAD & ") "
   sqlMtoRenglon = "(" & sqlMtoRenglon & " * ( 1 - " & _
                     insRenglonFactura.GetTableName & "." & insRenglonFactura.getFN_PORCENTAJE_DESCUENTO & " / 100))"
   sqlMtoRenglon = "(" & sqlMtoRenglon & " * ( 1 - " & _
                     insFactura.GetTableName & "." & insFactura.getFN_PORCENTAJE_DESCUENTO & " / 100))"
   sqlMtoRenglon = sqlMtoRenglon & " * " & SqlTasaDeCambio
   Sql = Sql & " UNION SELECT "
   Sql = Sql & gUtilSQL.fSimpleSqlValue(UCase(insArticulo.GetTableName)) & ", "
   Sql = Sql & fAgregaUnaCuentaAlSQL("HABER", valEsUnReverso, "SUM(" & sqlMtoRenglon & ")", _
               gUtilSQL.getIIF(insArticulo.GetTableName & ".CuentaContableIngreso = ''" & _
               " OR " & gUtilSQL.DfSQLIsNull(insArticulo.GetTableName & ".CuentaContableIngreso"), _
               gUtilSQL.fSimpleSqlValue(gProyReglasDeContabilizacion.GetCuentaFacturacionMontoTotalFactura), _
               insArticulo.GetTableName & ".CuentaContableIngreso", True), False, "", _
               numeroReferencia, valDescripcionDelAsiento, CondicionDeStatusAnulado, "") & ", "
   Sql = Sql & fAgregaUnaCuentaAlSQL("", False, "0", "", False, "", "", "", "", "")
   Sql = Sql & " FROM " & insFactura.GetTableName & " INNER JOIN ("
   Sql = Sql & insArticulo.GetTableName & " INNER JOIN "
   Sql = Sql & insRenglonFactura.GetTableName & " ON ("
   Sql = Sql & insArticulo.GetTableName & ".Codigo" & _
               " = " & insRenglonFactura.GetTableName & "." & insRenglonFactura.getFN_ARTICULO
   Sql = Sql & ") AND (" & insArticulo.GetTableName & ".ConsecutivoCompania" & _
               " = " & insRenglonFactura.GetTableName & "." & insRenglonFactura.getFN_CONSECUTIVO_COMPANIA
   Sql = Sql & ")) ON (" & insFactura.GetTableName & "." & insFactura.getFN_NUMERO & _
               " = " & insRenglonFactura.GetTableName & "." & insRenglonFactura.getFN_NUMERO_FACTURA
   Sql = Sql & ") AND (" & insFactura.GetTableName & "." & insFactura.getFN_CONSECUTIVO_COMPANIA & _
               " = " & insRenglonFactura.GetTableName & "." & insRenglonFactura.getFN_CONSECUTIVO_COMPANIA
   Sql = Sql & ") AND (" & insFactura.GetTableName & "." & insFactura.getFN_TIPO_DE_DOCUMENTO & _
               " = " & insRenglonFactura.GetTableName & "." & insRenglonFactura.getFN_TIPO_DE_DOCUMENTO & ")"
   Sql = Sql & " WHERE " & insFactura.GetTableName & "." & insFactura.getFN_CONSECUTIVO_COMPANIA & " = " & gProyCompaniaActual.GetConsecutivoCompania
   Sql = Sql & " AND " & insFactura.GetTableName & "." & insFactura.getFN_STATUS_FACTURA
   If valEsUnReverso Then
      Sql = Sql & " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA)
   Else
      Sql = Sql & " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_EMITIDA)
   End If
   Sql = Sql & " AND " & insFactura.GetTableName & "." & insFactura.getFN_TIPO_DE_DOCUMENTO & gUtilSQL.CharConcat & "'" & gTexto.fSeparadorStandardDeElementosString & "'" & gUtilSQL.CharConcat & insFactura.GetTableName & "." & insFactura.getFN_NUMERO & " IN (" & ConjuntoDeFacturasSinContabilizar & ")"
   Sql = Sql & " AND " & gUtilSQL.DfSQLDateValueBetween(insFactura.GetTableName & "." & insFactura.getFN_FECHA, valFechaDesde, valFechaHasta)
   Sql = Sql & " AND " & insFactura.GetTableName & "." & insFactura.getFN_TIPO_DE_DOCUMENTO & " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_RESUMENDIARIODEVENTAS)
   Sql = Sql & " GROUP BY "
   Sql = Sql & insArticulo.GetTableName & ".CuentaContableIngreso"
   Set insFactura = Nothing
   Set insCliente = Nothing
   Set insArticulo = Nothing
   Set insRenglonFactura = Nothing
h_EXIT:
   fSQLResumenVentasEntreFechasPorArticulo = Sql
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLResumenVentasEntreFechasPorArticulo", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLResumenVentasEntreFechas(ByVal valFechaDesde As Date, ByVal valFechaHasta As Date, _
            ByVal valEsUnReverso As Boolean, ByVal valDescripcionDelAsiento As String, _
            ByVal valContabilizarSolo1Documento As Boolean, ByVal valNoDocumentoOrigen As String, _
            ByVal valTipoDocumento As Integer) As String
   Dim insFactura As clsFacturaNavigator
   Dim insCliente As clsClienteNavigator
   Dim ConjuntoDeFacturasSinContabilizar As String
   Dim DebeOhaber As String
   Dim ColocarAuxiliar As Boolean
   Dim codigoDelAuxiliar As String
   Dim Sql As String
   Dim CodigoCuentaIVA As String
   Dim CodigoCuentaIVAUsaAuxiliar As Boolean
   Dim SqlMontoCargosMenosDescuentos  As String
   Dim SqlTasaDeCambio As String
   Dim CondicionDeStatusAnulado As String
   Dim varCodigoCliente As String
   Dim varAuxiliarCxC As Boolean
   Dim numeroReferencia As String
   On Error GoTo h_ERROR
   Set insCliente = New clsClienteNavigator
   Set insFactura = New clsFacturaNavigator
   insFactura.setClaseDeTrabajo eCTFC_Factura
   CodigoCuentaIVA = ""
   Sql = ""
   varCodigoCliente = fBuscaCodClienteSegunModulo(valNoDocumentoOrigen, eCG_RESUMEN_DIARIO_VENTAS, valTipoDocumento)
   varAuxiliarCxC = False
   CondicionDeStatusAnulado = gUtilSQL.fFirst(insFactura.GetTableName & "." & insFactura.getFN_STATUS_FACTURA, "NONE") & " = " & _
                              gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA)
   SqlTasaDeCambio = insFactura.GetTableName & "." & insFactura.getFN_CAMBIO_ABOLIVARES
   numeroReferencia = valNoDocumentoOrigen
   If valContabilizarSolo1Documento Then
      gTexto.ReemplazaCaracteresEnElString numeroReferencia, vbTab, "-"
   End If
   ConjuntoDeFacturasSinContabilizar = fSQLConjuntoRecordsDeContabilizacion1oMasDocumentos(eCG_FACTURA, valContabilizarSolo1Documento, valNoDocumentoOrigen, valFechaDesde, valFechaHasta, valTipoDocumento)
   If ConjuntoDeFacturasSinContabilizar = "" Then
      GoTo h_EXIT
   End If
   codigoDelAuxiliar = insFactura.GetTableName & "." & insFactura.getFN_CODIGO_CLIENTE
   CodigoCuentaIVA = fEscogeCuentaDeIVASiEsDebitoOcredito(fCambiaDebeHaberSiEsUnReverso("HABER", valEsUnReverso), eTD_ALICUOTAGENERAL, eCG_FACTURA, CodigoCuentaIVAUsaAuxiliar)
   Sql = "SELECT " & insFactura.GetTableName & "." & insFactura.getFN_CODIGO_CLIENTE & ", "
   Sql = Sql & fAgregaUnaCuentaAlSQL("DEBE", valEsUnReverso, "SUM(" & insFactura.GetTableName & "." & insFactura.getFN_TOTAL_FACTURA & " * " & SqlTasaDeCambio & ")", gProyReglasDeContabilizacion.GetCuentaRDVtasCaja, False, "", numeroReferencia, valDescripcionDelAsiento, CondicionDeStatusAnulado, "") & ", "
   Sql = Sql & fAgregaUnaCuentaAlSQL("HABER", valEsUnReverso, "SUM(" & insFactura.GetTableName & "." & insFactura.getFN_TOTAL_IVA & " * " & SqlTasaDeCambio & ")", CodigoCuentaIVA, CodigoCuentaIVAUsaAuxiliar, codigoDelAuxiliar, numeroReferencia, valDescripcionDelAsiento, CondicionDeStatusAnulado, "") & ", "
   Sql = Sql & fAgregaUnaCuentaAlSQL("HABER", valEsUnReverso, "SUM((" & insFactura.GetTableName & "." & insFactura.getFN_TOTAL_FACTURA & " - " & insFactura.GetTableName & "." & insFactura.getFN_TOTAL_IVA & ") * " & SqlTasaDeCambio & ")", gProyReglasDeContabilizacion.GetCuentaRDVtasMontoTotal, False, "", numeroReferencia, valDescripcionDelAsiento, CondicionDeStatusAnulado, "")
   Sql = Sql & " FROM " & insFactura.GetTableName & " INNER JOIN " & insCliente.GetTableName & " ON "
   Sql = Sql & insFactura.GetTableName & "." & insFactura.getFN_CONSECUTIVO_COMPANIA & " = " & insCliente.GetTableName & ".ConsecutivoCompania"
   Sql = Sql & " AND " & insFactura.GetTableName & "." & insFactura.getFN_CODIGO_CLIENTE & " = " & insCliente.GetTableName & ".Codigo"
   Sql = Sql & " WHERE " & insFactura.GetTableName & "." & insFactura.getFN_CONSECUTIVO_COMPANIA & " = " & gProyCompaniaActual.GetConsecutivoCompania
   Sql = Sql & " AND " & insFactura.GetTableName & "." & insFactura.getFN_STATUS_FACTURA & " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_BORRADOR)
   Sql = Sql & " AND " & insFactura.GetTableName & "." & insFactura.getFN_TIPO_DE_DOCUMENTO & gUtilSQL.CharConcat & "'" & gTexto.fSeparadorStandardDeElementosString & "'" & gUtilSQL.CharConcat & insFactura.GetTableName & "." & insFactura.getFN_NUMERO & " IN (" & ConjuntoDeFacturasSinContabilizar & ")"
   Sql = Sql & " AND (" & gUtilSQL.DfSQLDateValueBetween(insFactura.GetTableName & "." & insFactura.getFN_FECHA, valFechaDesde, valFechaHasta) & ")"
   Sql = Sql & " AND " & insFactura.GetTableName & "." & insFactura.getFN_TIPO_DE_DOCUMENTO & " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_RESUMENDIARIODEVENTAS)
   Sql = Sql & " GROUP BY " & insFactura.GetTableName & "." & insFactura.getFN_CODIGO_CLIENTE
   Set insFactura = Nothing
   Set insCliente = Nothing
h_EXIT:
   fSQLResumenVentasEntreFechas = Sql
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLResumenVentasEntreFechas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fBuscaDescripcionDelComprobanteContableDeAnticipo(ByVal valNumeroDocumento As String, ByVal valTipoDocumento As enum_TipoDeAnticipo) As String
   Dim insAnticipo As clsAnticipoNavigator
   Dim DescripcionBuscada As String
   Dim Sql As String
   On Error GoTo h_ERROR
   Set insAnticipo = New clsAnticipoNavigator
   DescripcionBuscada = insAnticipo.fBuscaDatosDelAnticipoParaDescripcionComprobante(gConvert.ConvierteAlong(valNumeroDocumento), valTipoDocumento)
   Set insAnticipo = Nothing
   DescripcionBuscada = "Anticipo " & DescripcionBuscada & "(" & valNumeroDocumento & ")"
h_EXIT:
   fBuscaDescripcionDelComprobanteContableDeAnticipo = DescripcionBuscada
   On Error GoTo 0
   Exit Function
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
            "fBuscaDescripcionDelComprobanteContableDeAnticipo", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
 End Function
Private Function fSQLAnticiposEntreFechas(ByVal valFechaDesde As Date, ByVal valFechaHasta As Date, _
            ByVal valEsUnReverso As Boolean, ByVal valDescripcionDelAsiento As String, _
            ByVal valContabilizarSolo1Documento As Boolean, ByVal valNoDocumentoOrigen As String, _
            ByVal valTipoDocumento As Integer) As String
   Dim Sql As String
   On Error GoTo h_ERROR
   If valTipoDocumento = enum_TipoDeAnticipo.eTDA_COBRADO Then
      Sql = fSQLAnticiposCobradosEntreFechas(valFechaDesde, valFechaHasta, valEsUnReverso, valDescripcionDelAsiento, valContabilizarSolo1Documento, valNoDocumentoOrigen, valTipoDocumento)
   ElseIf valTipoDocumento = enum_TipoDeAnticipo.eTDA_PAGADO Then
      Sql = fSQLAnticiposPagadosEntreFechas(valFechaDesde, valFechaHasta, valEsUnReverso, valDescripcionDelAsiento, valContabilizarSolo1Documento, valNoDocumentoOrigen, valTipoDocumento)
   Else
      Sql = ""
   End If
h_EXIT:
   fSQLAnticiposEntreFechas = Sql
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLAnticiposEntreFechas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLAnticiposCobradosEntreFechas(ByVal valFechaDesde As Date, ByVal valFechaHasta As Date, _
            ByVal valEsUnReverso As Boolean, ByVal valDescripcionDelAsiento As String, _
            ByVal valContabilizarSolo1Documento As Boolean, ByVal valNoDocumentoOrigen As String, _
            ByVal valTipoDocumento As Integer) As String
   Dim insAnticipo As clsAnticipoNavigator
   Dim insCliente As clsClienteNavigator
   Dim insCuentaBancaria As clsCuentaBancariaNavigator
   Dim insMovBancario As clsMovimientoBancarioNavigator
   Dim ConjuntoDeAnticiposSinContabilizar As String
   Dim ConjuntoDeMovBancariosAsociadosAAnticipos As String
   Dim numeroReferencia As String
   Dim Sql As String
   Dim sqlCondicionDeStatusAnulado As String
   Dim SqlTasaDeCambio As String
   Dim sqlTasaDeCambioMovBancario As String
   Dim sqlMontoMovimientoBancario As String
   Dim sqlCuentaBancariaIDB As String
   Dim descripcionMovBancIDB As String
   Dim sqlCuentaDebe As String
   Dim sqlCuentaHaber1 As String
   Dim sqlCuentaHaber2 As String
   Dim sqlMontoDebe As String
   Dim sqlMontoHaber1 As String
   Dim sqlMontoHaber2 As String
   Dim sqlAnticipoEsUnaDevolucion As String
   Dim sqlBancoCuentaContable As String
   Dim sqlCuentaAnticipoCobrado As String
   Dim sqlCuentaAnticipoCajaBanco As String
   Dim sqlCuentaIDBBancoIngresos As String
   Dim codigoCliente As String
   Dim auxiliarAnticipo As Boolean
   Dim codigoDelAuxiliar As String
   Dim insLibSaW As clsLibSAW
   On Error GoTo h_ERROR
   Set insLibSaW = New clsLibSAW
   Set insAnticipo = New clsAnticipoNavigator
   Set insCliente = New clsClienteNavigator
   Set insCuentaBancaria = New clsCuentaBancariaNavigator
   Set insMovBancario = New clsMovimientoBancarioNavigator
   numeroReferencia = valNoDocumentoOrigen
   ConjuntoDeAnticiposSinContabilizar = fSQLConjuntoRecordsDeContabilizacion1oMasDocumentos(eCG_ANTICIPO, valContabilizarSolo1Documento, valNoDocumentoOrigen, valFechaDesde, valFechaHasta, enum_TipoDeAnticipo.eTDA_COBRADO)
   If ConjuntoDeAnticiposSinContabilizar = "" Then
      GoTo h_EXIT
   End If
   If valContabilizarSolo1Documento Then
      ConjuntoDeMovBancariosAsociadosAAnticipos = fConjuntoDeImpTranBancariaAsociadosAAnticipos(gUtilSQL.fSimpleSqlValue(valNoDocumentoOrigen))
   Else
      ConjuntoDeMovBancariosAsociadosAAnticipos = fConjuntoDeImpTranBancariaAsociadosAAnticipos(ConjuntoDeAnticiposSinContabilizar)
   End If
   codigoCliente = fBuscaCodClienteSegunModulo(valNoDocumentoOrigen, eCG_ANTICIPO, valTipoDocumento)
   auxiliarAnticipo = fElClienteManejaAuxiliar(codigoCliente, eCG_ANTICIPO, gProyReglasDeContabilizacion.eTC_ReglaAntCobCliente, valTipoDocumento)
   codigoDelAuxiliar = insAnticipo.GetTableName & ".CodigoCliente"
   SqlTasaDeCambio = insAnticipo.GetTableName & ".Cambio"
   sqlTasaDeCambioMovBancario = insMovBancario.GetTableName & ".CambioABolivares"
   sqlCondicionDeStatusAnulado = "(" & gUtilSQL.fFirst(insAnticipo.GetTableName & ".Status", "NONE") & " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusAnticipo.eSDA_ANULADO) & ")"
   sqlAnticipoEsUnaDevolucion = insAnticipo.GetTableName & ".EsUnaDevolucion = " & gUtilSQL.fBooleanToSqlValue(True)
   sqlBancoCuentaContable = insCuentaBancaria.GetTableName & ".CuentaContable"
   sqlCuentaAnticipoCobrado = gUtilSQL.getIIF(insCliente.GetTableName & ".CuentaContableAnticipo <> ''", insCliente.GetTableName & ".CuentaContableAnticipo", _
                              gUtilSQL.fSimpleSqlValue(gProyReglasDeContabilizacion.GetCuentaAnticipoCobrado), True)
   sqlCuentaAnticipoCajaBanco = gUtilSQL.getIIF(insCuentaBancaria.GetTableName & ".CuentaContable <> ''", _
                                 insCuentaBancaria.GetTableName & ".CuentaContable", _
                                 gUtilSQL.fSimpleSqlValue(gProyReglasDeContabilizacion.GetCuentaAnticipoCaja), True)
   sqlCuentaIDBBancoIngresos = gUtilSQL.getIIF(insAnticipo.GetTableName & ".DiferenciaEsIDB = " & gUtilSQL.fBooleanToSqlValue(True), _
                                 gUtilSQL.fSimpleSqlValue(gProyReglasDeContabilizacion.GetCuentaDebitoBancarioBancos), _
                                 gUtilSQL.fSimpleSqlValue(gProyReglasDeContabilizacion.GetCuentaAnticipoOtrosIngresos), True)
   sqlCuentaDebe = gUtilSQL.fFirst(gUtilSQL.getIIF(sqlAnticipoEsUnaDevolucion, sqlCuentaAnticipoCobrado, sqlCuentaAnticipoCajaBanco, True), "NONE")
   sqlCuentaHaber1 = gUtilSQL.fFirst(gUtilSQL.getIIF(sqlAnticipoEsUnaDevolucion, sqlCuentaAnticipoCajaBanco, sqlCuentaAnticipoCobrado, True), "NONE")
   sqlCuentaHaber2 = gUtilSQL.fFirst(gUtilSQL.getIIF(sqlAnticipoEsUnaDevolucion, sqlCuentaIDBBancoIngresos, gUtilSQL.fSimpleSqlValue(gProyReglasDeContabilizacion.GetCuentaAnticipoOtrosIngresos), True), "NONE")
   sqlMontoDebe = "SUM(" & gUtilSQL.getIIF(sqlAnticipoEsUnaDevolucion, "(" & insAnticipo.GetTableName & ".MontoDevuelto + " & insAnticipo.GetTableName & ".MontoDiferenciaEnDevolucion)", _
                  insAnticipo.GetTableName & ".MontoTotal", True) & " * " & SqlTasaDeCambio & ")"
   sqlMontoHaber1 = "SUM(" & gUtilSQL.getIIF(sqlAnticipoEsUnaDevolucion, insAnticipo.GetTableName & ".MontoDevuelto", insAnticipo.GetTableName & ".MontoTotal", True) & " * " & SqlTasaDeCambio & ")"
   sqlMontoHaber2 = "SUM(" & gUtilSQL.getIIF(sqlAnticipoEsUnaDevolucion, insAnticipo.GetTableName & ".MontoDiferenciaEnDevolucion", "0", True) & " * " & SqlTasaDeCambio & ")"
   descripcionMovBancIDB = insLibSaW.fTituloITFEnCtaBancaria & " / " & valDescripcionDelAsiento
   sqlMontoMovimientoBancario = "(SUM(" & insMovBancario.GetTableName & ".Monto * " & sqlTasaDeCambioMovBancario & "))"
   sqlCuentaBancariaIDB = gUtilSQL.fFirst(gUtilSQL.getIIF(sqlBancoCuentaContable & " <> ''", sqlBancoCuentaContable, gUtilSQL.fSimpleSqlValue(gProyReglasDeContabilizacion.GetCuentaDebitoBancarioBancos), True), "NONE")
   'DEBE o HABER, monto, cuenta, auxiliar, centroDeCosto, NumRef, DescripcionAsiento
   Sql = "SELECT " & insAnticipo.GetTableName & ".CodigoCliente, "
   Sql = Sql & fAgregaUnaCuentaAlSQL("DEBE", valEsUnReverso, sqlMontoDebe, sqlCuentaDebe, False, "", numeroReferencia, valDescripcionDelAsiento, sqlCondicionDeStatusAnulado, "") & ", "
   Sql = Sql & fAgregaUnaCuentaAlSQL("HABER", valEsUnReverso, sqlMontoHaber1, sqlCuentaHaber1, auxiliarAnticipo, codigoDelAuxiliar, numeroReferencia, valDescripcionDelAsiento, sqlCondicionDeStatusAnulado, "") & ", "
   Sql = Sql & fAgregaUnaCuentaAlSQL("HABER", valEsUnReverso, sqlMontoHaber2, sqlCuentaHaber2, False, "", numeroReferencia, valDescripcionDelAsiento, sqlCondicionDeStatusAnulado, "")
   Sql = Sql & " FROM (" & insAnticipo.GetTableName & " INNER JOIN " & insCliente.GetTableName & " ON ("
   Sql = Sql & insAnticipo.GetTableName & ".CodigoCliente = " & insCliente.GetTableName & ".Codigo"
   Sql = Sql & ") AND (" & insAnticipo.GetTableName & ".ConsecutivoCompania = " & insCliente.GetTableName & ".ConsecutivoCompania"
   Sql = Sql & ")) LEFT JOIN " & insCuentaBancaria.GetTableName & " ON ("
   Sql = Sql & insAnticipo.GetTableName & ".CodigoCuentaBancaria = " & insCuentaBancaria.GetTableName & ".Codigo"
   Sql = Sql & ") AND (" & insAnticipo.GetTableName & ".ConsecutivoCompania = " & insCuentaBancaria.GetTableName & ".ConsecutivoCompania)"
   Sql = Sql & " WHERE " & insAnticipo.GetTableName & ".ConsecutivoCompania = " & gProyCompaniaActual.GetConsecutivoCompania
   Sql = Sql & " AND " & insAnticipo.GetTableName & ".Tipo  = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeAnticipo.eTDA_COBRADO)
   Sql = Sql & " AND " & insAnticipo.GetTableName & ".ConsecutivoAnticipo IN (" & ConjuntoDeAnticiposSinContabilizar & ")"
   Sql = Sql & " AND " & gUtilSQL.DfSQLDateValueBetween(insAnticipo.GetTableName & ".Fecha", valFechaDesde, valFechaHasta)
   Sql = Sql & " GROUP BY " & insAnticipo.GetTableName & ".CodigoCliente"
   sqlCondicionDeStatusAnulado = ""
   Sql = Sql & " UNION SELECT " & insMovBancario.GetTableName & ".CodigoConcepto, "
   Sql = Sql & fAgregaUnaCuentaAlSQL("DEBE", valEsUnReverso, sqlMontoMovimientoBancario, gProyReglasDeContabilizacion.GetCuentaDebitoBancarioGasto, False, "", numeroReferencia, descripcionMovBancIDB, sqlCondicionDeStatusAnulado, "") & ", "
   Sql = Sql & fAgregaUnaCuentaAlSQL("HABER", valEsUnReverso, sqlMontoMovimientoBancario, sqlCuentaBancariaIDB, False, "", numeroReferencia, descripcionMovBancIDB, sqlCondicionDeStatusAnulado, "") & ", "
   Sql = Sql & fAgregaUnaCuentaAlSQL("HABER", valEsUnReverso, "0", "NA", False, "", "", "", "", "")
   Sql = Sql & " FROM " & insMovBancario.GetTableName & " INNER JOIN " & insCuentaBancaria.GetTableName & " ON "
   Sql = Sql & insMovBancario.GetTableName & ".ConsecutivoCompania = " & insCuentaBancaria.GetTableName & ".ConsecutivoCompania"
   Sql = Sql & " AND " & insMovBancario.GetTableName & ".CodigoCtaBancaria = " & insCuentaBancaria.GetTableName & ".Codigo"
   Sql = Sql & " WHERE " & insMovBancario.GetTableName & ".ConsecutivoCompania = " & gProyCompaniaActual.GetConsecutivoCompania
   If ConjuntoDeMovBancariosAsociadosAAnticipos <> "" Then
      Sql = Sql & " AND " & insMovBancario.GetTableName & ".NroMovimientoRelacionado IN(" & ConjuntoDeMovBancariosAsociadosAAnticipos & ")"
   End If
   Sql = Sql & " AND " & insMovBancario.GetTableName & ".GeneradoPor = " & gUtilSQL.fSQLSimpleValueForEnum(enum_GeneradoPor.eGP_DEBITO_BANCARIO)
   Sql = Sql & " AND " & gUtilSQL.DfSQLDateValueBetween(insMovBancario.GetTableName & ".Fecha", valFechaDesde, valFechaHasta)
   Sql = Sql & " GROUP BY " & insMovBancario.GetTableName & ".CodigoConcepto"
   
   Set insAnticipo = Nothing
   Set insCliente = Nothing
   Set insCuentaBancaria = Nothing
   Set insMovBancario = Nothing
   Set insLibSaW = Nothing
h_EXIT:
   fSQLAnticiposCobradosEntreFechas = Sql
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLAnticiposCobradosEntreFechas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLAnticiposPagadosEntreFechas(ByVal valFechaDesde As Date, ByVal valFechaHasta As Date, _
            ByVal valEsUnReverso As Boolean, ByVal valDescripcionDelAsiento As String, _
            ByVal valContabilizarSolo1Documento As Boolean, ByVal valNoDocumentoOrigen As String, _
            ByVal valTipoDocumento As Integer) As String
   Dim insAnticipo As clsAnticipoNavigator
   Dim insProveedor As clsProveedorNavigator
   Dim insCuentaBancaria As clsCuentaBancariaNavigator
   Dim insMovBancario As clsMovimientoBancarioNavigator
   Dim ConjuntoDeAnticiposSinContabilizar As String
   Dim ConjuntoDeMovBancariosAsociadosAAnticipos As String
   Dim ConjDeAnticiposSinContParaBuscarConjDeMovBancarios As String
   Dim numeroReferencia As String
   Dim Sql As String
   Dim sqlCondicionDeStatusAnulado As String
   Dim SqlTasaDeCambio As String
   Dim sqlTasaDeCambioMovBancario As String
   Dim sqlMontoMovimientoBancario As String
   Dim sqlCuentaBancariaIDB As String
   Dim descripcionMovBancIDB As String
   Dim sqlCuentaDebe1 As String
   Dim sqlCuentaDebe2 As String
   Dim sqlCuentaHaber As String
   Dim sqlMontoDebe1 As String
   Dim sqlMontoDebe2 As String
   Dim sqlMontoHaber As String
   Dim sqlAnticipoEsUnaDevolucion As String
   Dim sqlCuentaBanco As String
   Dim sqlBancoCuentaContable As String
   Dim sqlAnticipoProveedor As String
   Dim sqlProveedorCuentaAnticipo As String
   Dim sqlCuentaIDBGastosEgresos As String
   Dim codigoProveedor As String
   Dim auxiliarAnticipo As Boolean
   Dim codigoDelAuxiliar As String
   Dim insLibSaW As clsLibSAW
   On Error GoTo h_ERROR
   Set insLibSaW = New clsLibSAW
   Set insAnticipo = New clsAnticipoNavigator
   Set insProveedor = New clsProveedorNavigator
   Set insCuentaBancaria = New clsCuentaBancariaNavigator
   Set insMovBancario = New clsMovimientoBancarioNavigator
   numeroReferencia = valNoDocumentoOrigen
   ConjuntoDeAnticiposSinContabilizar = fSQLConjuntoRecordsDeContabilizacion1oMasDocumentos(eCG_ANTICIPO, valContabilizarSolo1Documento, valNoDocumentoOrigen, valFechaDesde, valFechaHasta, enum_TipoDeAnticipo.eTDA_PAGADO)
   If ConjuntoDeAnticiposSinContabilizar = "" Then
      GoTo h_EXIT
   End If
   If valContabilizarSolo1Documento Then
      ConjuntoDeMovBancariosAsociadosAAnticipos = fConjuntoDeImpTranBancariaAsociadosAAnticipos(gUtilSQL.fSimpleSqlValue(valNoDocumentoOrigen))
   Else
      ConjuntoDeMovBancariosAsociadosAAnticipos = fConjuntoDeImpTranBancariaAsociadosAAnticipos(ConjuntoDeAnticiposSinContabilizar)
   End If
   codigoProveedor = fBuscaCodProveedorSegunModulo(valNoDocumentoOrigen, eCG_ANTICIPO)
   auxiliarAnticipo = fElProveedorManejaAuxiliar(codigoProveedor, eCG_ANTICIPO, gProyReglasDeContabilizacion.eTC_ReglaAntPagProveedor, valTipoDocumento)
   codigoDelAuxiliar = insAnticipo.GetTableName & ".CodigoProveedor"
   SqlTasaDeCambio = insAnticipo.GetTableName & ".Cambio"
   sqlTasaDeCambioMovBancario = insMovBancario.GetTableName & ".CambioABolivares"
   sqlCondicionDeStatusAnulado = "(" & gUtilSQL.fFirst(insAnticipo.GetTableName & ".Status", "NONE") & " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusAnticipo.eSDA_ANULADO) & ")"
   sqlAnticipoEsUnaDevolucion = insAnticipo.GetTableName & ".EsUnaDevolucion = " & gUtilSQL.fBooleanToSqlValue(True)
   sqlBancoCuentaContable = insCuentaBancaria.GetTableName & ".CuentaContable"
   sqlCuentaBanco = gUtilSQL.getIIF(sqlBancoCuentaContable & " <> ''", sqlBancoCuentaContable, gUtilSQL.fSimpleSqlValue(gProyReglasDeContabilizacion.GetCuentaAnticipoBanco), True)
   sqlProveedorCuentaAnticipo = insProveedor.GetTableName & ".CuentaContableAnticipo"
   sqlAnticipoProveedor = gUtilSQL.getIIF(sqlProveedorCuentaAnticipo & " <> ''", sqlProveedorCuentaAnticipo, gUtilSQL.fSimpleSqlValue(gProyReglasDeContabilizacion.GetCuentaAnticipoPagado), True)
   sqlCuentaIDBGastosEgresos = gUtilSQL.getIIF(insAnticipo.GetTableName & ".DiferenciaEsIDB = " & gUtilSQL.fBooleanToSqlValue(True), _
                                 gUtilSQL.fSimpleSqlValue(gProyReglasDeContabilizacion.GetCuentaDebitoBancarioGasto), _
                                 gUtilSQL.fSimpleSqlValue(gProyReglasDeContabilizacion.GetCuentaAnticipoOtrosEgresos), True)
   sqlCuentaDebe1 = gUtilSQL.fFirst(gUtilSQL.getIIF(sqlAnticipoEsUnaDevolucion, sqlCuentaBanco, sqlAnticipoProveedor, True), "NONE")
   sqlCuentaDebe2 = gUtilSQL.fFirst(gUtilSQL.getIIF(sqlAnticipoEsUnaDevolucion, sqlCuentaIDBGastosEgresos, gUtilSQL.fSimpleSqlValue(gProyReglasDeContabilizacion.GetCuentaAnticipoOtrosEgresos), True), "NONE")
   sqlCuentaHaber = gUtilSQL.fFirst(gUtilSQL.getIIF(sqlAnticipoEsUnaDevolucion, sqlAnticipoProveedor, sqlCuentaBanco, True), "NONE")
   sqlMontoDebe1 = "SUM(" & gUtilSQL.getIIF(sqlAnticipoEsUnaDevolucion, insAnticipo.GetTableName & ".MontoDevuelto", insAnticipo.GetTableName & ".MontoTotal", True) & " * " & SqlTasaDeCambio & ")"
   sqlMontoDebe2 = "SUM(" & gUtilSQL.getIIF(sqlAnticipoEsUnaDevolucion, insAnticipo.GetTableName & ".MontoDiferenciaEnDevolucion", "0", True) & " * " & SqlTasaDeCambio & ")"
   sqlMontoHaber = "SUM(" & gUtilSQL.getIIF(sqlAnticipoEsUnaDevolucion, "(" & insAnticipo.GetTableName & ".MontoDevuelto + " & insAnticipo.GetTableName & ".MontoDiferenciaEnDevolucion)", insAnticipo.GetTableName & ".MontoTotal", True) & " * " & SqlTasaDeCambio & ")"
   descripcionMovBancIDB = insLibSaW.fTituloITFEnCtaBancaria & " / " & valDescripcionDelAsiento
   sqlMontoMovimientoBancario = "(SUM(" & insMovBancario.GetTableName & ".Monto * " & sqlTasaDeCambioMovBancario & "))"
   sqlCuentaBancariaIDB = gUtilSQL.fFirst(gUtilSQL.getIIF(sqlBancoCuentaContable & " <> ''", sqlBancoCuentaContable, gUtilSQL.fSimpleSqlValue(gProyReglasDeContabilizacion.GetCuentaDebitoBancarioBancos), True), "NONE")
   'DEBE o HABER, monto, cuenta, auxiliar, centroDeCosto, NumRef, DescripcionAsiento
   Sql = "SELECT " & insAnticipo.GetTableName & ".CodigoProveedor, "
   Sql = Sql & fAgregaUnaCuentaAlSQL("DEBE", valEsUnReverso, sqlMontoDebe1, sqlCuentaDebe1, auxiliarAnticipo, codigoDelAuxiliar, numeroReferencia, valDescripcionDelAsiento, sqlCondicionDeStatusAnulado, "") & ", "
   Sql = Sql & fAgregaUnaCuentaAlSQL("DEBE", valEsUnReverso, sqlMontoDebe2, sqlCuentaDebe2, False, "", numeroReferencia, valDescripcionDelAsiento, sqlCondicionDeStatusAnulado, "") & ", "
   Sql = Sql & fAgregaUnaCuentaAlSQL("HABER", valEsUnReverso, sqlMontoHaber, sqlCuentaHaber, False, "", numeroReferencia, valDescripcionDelAsiento, sqlCondicionDeStatusAnulado, "")
   Sql = Sql & " FROM (" & insAnticipo.GetTableName & " INNER JOIN " & insProveedor.GetTableName & " ON ("
   Sql = Sql & insAnticipo.GetTableName & ".CodigoProveedor = " & insProveedor.GetTableName & ".CodigoProveedor"
   Sql = Sql & ") AND (" & insAnticipo.GetTableName & ".ConsecutivoCompania = " & insProveedor.GetTableName & ".ConsecutivoCompania"
   Sql = Sql & ")) INNER JOIN " & insCuentaBancaria.GetTableName & " ON ("
   Sql = Sql & insAnticipo.GetTableName & ".CodigoCuentaBancaria = " & insCuentaBancaria.GetTableName & ".Codigo"
   Sql = Sql & ") AND (" & insAnticipo.GetTableName & ".ConsecutivoCompania = " & insCuentaBancaria.GetTableName & ".ConsecutivoCompania)"
   Sql = Sql & " WHERE " & insAnticipo.GetTableName & ".ConsecutivoCompania = " & gProyCompaniaActual.GetConsecutivoCompania
   Sql = Sql & " AND " & insAnticipo.GetTableName & ".Tipo = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeAnticipo.eTDA_PAGADO)
   Sql = Sql & " AND " & insAnticipo.GetTableName & ".ConsecutivoAnticipo IN (" & ConjuntoDeAnticiposSinContabilizar & ")"
   Sql = Sql & " AND " & gUtilSQL.DfSQLDateValueBetween(insAnticipo.GetTableName & ".Fecha", valFechaDesde, valFechaHasta)
   Sql = Sql & " GROUP BY " & insAnticipo.GetTableName & ".CodigoProveedor"
   sqlCondicionDeStatusAnulado = ""
   Sql = Sql & " UNION SELECT " & insMovBancario.GetTableName & ".CodigoConcepto, "
   Sql = Sql & fAgregaUnaCuentaAlSQL("DEBE", valEsUnReverso, sqlMontoMovimientoBancario, gProyReglasDeContabilizacion.GetCuentaDebitoBancarioGasto, False, "", numeroReferencia, descripcionMovBancIDB, sqlCondicionDeStatusAnulado, "") & ", "
   Sql = Sql & fAgregaUnaCuentaAlSQL("HABER", valEsUnReverso, sqlMontoMovimientoBancario, sqlCuentaBancariaIDB, False, "", numeroReferencia, descripcionMovBancIDB, sqlCondicionDeStatusAnulado, "") & ", "
   Sql = Sql & fAgregaUnaCuentaAlSQL("HABER", valEsUnReverso, "0", "NA", False, "", "", "", "", "")
   Sql = Sql & " FROM " & insMovBancario.GetTableName & " INNER JOIN " & insCuentaBancaria.GetTableName & " ON "
   Sql = Sql & insMovBancario.GetTableName & ".ConsecutivoCompania = " & insCuentaBancaria.GetTableName & ".ConsecutivoCompania"
   Sql = Sql & " AND " & insMovBancario.GetTableName & ".CodigoCtaBancaria = " & insCuentaBancaria.GetTableName & ".Codigo"
   Sql = Sql & " WHERE " & insMovBancario.GetTableName & ".ConsecutivoCompania = " & gProyCompaniaActual.GetConsecutivoCompania
   If ConjuntoDeMovBancariosAsociadosAAnticipos <> "" Then
      Sql = Sql & " AND " & insMovBancario.GetTableName & ".NroMovimientoRelacionado IN(" & ConjuntoDeMovBancariosAsociadosAAnticipos & ")"
   End If
   Sql = Sql & " AND " & insMovBancario.GetTableName & ".GeneradoPor = " & gUtilSQL.fSQLSimpleValueForEnum(enum_GeneradoPor.eGP_DEBITO_BANCARIO)
   Sql = Sql & " AND " & gUtilSQL.DfSQLDateValueBetween(insMovBancario.GetTableName & ".Fecha", valFechaDesde, valFechaHasta)
   Sql = Sql & " GROUP BY " & insMovBancario.GetTableName & ".CodigoConcepto"
   Set insAnticipo = New clsAnticipoNavigator
   Set insProveedor = New clsProveedorNavigator
   Set insCuentaBancaria = New clsCuentaBancariaNavigator
   Set insMovBancario = New clsMovimientoBancarioNavigator
   Set insLibSaW = Nothing
h_EXIT:
   fSQLAnticiposPagadosEntreFechas = Sql
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLAnticiposPagadosEntreFechas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fConjuntoDeImpTranBancariaAsociadosAAnticipos(ByVal valConjuntoDeAnticiposSinContabilizar As String) As String
   Dim Sql As String
   Dim ConjuntoDeRecords As String
   On Error GoTo h_ERROR
   Sql = "SELECT movimientoBancario.ConsecutivoMovimiento"
   Sql = Sql & " FROM movimientoBancario WHERE movimientoBancario.ConsecutivoCompania = " & gProyCompaniaActual.GetConsecutivoCompania
   Sql = Sql & " AND movimientoBancario.NroMovimientoRelacionado IN (" & valConjuntoDeAnticiposSinContabilizar & ") AND movimientoBancario.GeneradoPor = " & gUtilSQL.fSQLSimpleValueForEnum(enum_GeneradoPor.eGP_ANTICIPO)
   ConjuntoDeRecords = gDbUtil.fBuildResultSetAsString(Sql)
h_EXIT:
   fConjuntoDeImpTranBancariaAsociadosAAnticipos = ConjuntoDeRecords
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConjuntoDeImpTranBancariaAsociadosAAnticipos", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fEjecutaProcesoPostContabilizacion(ByVal valModuloAContabilizar As enum_ComprobanteGeneradoPor, _
      ByVal valFechaDesde As Date, ByVal valFechaHasta As Date, _
      ByVal valConjuntoDeDocumentosContabilizados As String) As Boolean
   Dim vResultado As Boolean
   On Error GoTo h_ERROR
   vResultado = False
   Select Case valModuloAContabilizar
      Case eCG_CXC: vResultado = True
      Case eCG_FACTURA: vResultado = True
      Case eCG_CXP: vResultado = fEjecutaProcesoPostContabilizacionDeCxP(valConjuntoDeDocumentosContabilizados)
      Case eCG_COBRANZA: vResultado = fEjecutaProcesoPostContabilizacionDeCobranza(valConjuntoDeDocumentosContabilizados)
      Case eCG_PAGOS: vResultado = fEjecutaProcesoPostContabilizacionDePago(valFechaDesde, valFechaHasta, valConjuntoDeDocumentosContabilizados)
      Case eCG_MOVIMIENTO_BANCARIO: vResultado = True
      Case eCG_RESUMEN_DIARIO_VENTAS: vResultado = True
      Case eCG_ANTICIPO: vResultado = True
      Case eCG_REPOSICION: vResultado = True
   End Select
   fEjecutaProcesoPostContabilizacion = vResultado
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fEjecutaProcesoPostContabilizacion", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLConjuntoRecordsDeContabilizacion1oMasDocumentos(ByVal valModuloAContabilizar As enum_ComprobanteGeneradoPor, _
                  ByVal valContabilizarSolo1Documento As Boolean, ByVal valNoDocumentoOrigen As String, _
                  ByVal valFechaDesde As Date, valFechaHasta As Date, ByVal valTipoDocumento As Integer) As String
   Dim vConjunto As String
   Dim vTableName As String
   Dim vFNNumero As String
   Dim vFNFecha As String
   Dim vFNConsecCia As String
   Dim vDevuelveLosContabilizados As Boolean
   Dim vCampoClaveEsStrType As Boolean
   Dim vSQLOtherFilters As String
   On Error GoTo h_ERROR
   vConjunto = ""
   Select Case valModuloAContabilizar
      Case eCG_CXC
         If valContabilizarSolo1Documento Then
            vConjunto = gUtilSQL.fSimpleSqlValue(valNoDocumentoOrigen)
         Else
            Dim insCxC As clsCxCNavigator
            Set insCxC = New clsCxCNavigator
               vTableName = insCxC.GetTableName
               vFNNumero = "Numero"
               vFNFecha = "Fecha"
               vFNConsecCia = "ConsecutivoCompania"
               vDevuelveLosContabilizados = False
               vCampoClaveEsStrType = True
               vSQLOtherFilters = ""
            Set insCxC = Nothing
         End If
      Case eCG_FACTURA
         If valContabilizarSolo1Documento Then
            vConjunto = gUtilSQL.fSimpleSqlValue(valNoDocumentoOrigen)
         Else
            Dim insFactura As clsFacturaNavigator
            Set insFactura = New clsFacturaNavigator
               vTableName = insFactura.GetTableName
               vFNNumero = insFactura.getFN_NUMERO
               vFNFecha = insFactura.getFN_FECHA
               vFNConsecCia = insFactura.getFN_CONSECUTIVO_COMPANIA
               vDevuelveLosContabilizados = False
               vCampoClaveEsStrType = True
               vSQLOtherFilters = ""
            Set insFactura = Nothing
         End If
      Case eCG_CXP
         If valContabilizarSolo1Documento Then
            vConjunto = gUtilSQL.fSimpleSqlValue(valNoDocumentoOrigen)
         Else
            Dim insCxP As clsCxPNavigator
            Set insCxP = New clsCxPNavigator
               vTableName = insCxP.GetTableName
               vFNNumero = "Numero"
               '********* OJO AYRG LINEAS MES/ANO APLICACION *******
               vFNFecha = gUtilSQL.getIIF(gUtilSQL.DfSQLMonthOfDate(insCxP.GetTableName & ".Fecha") & " = " & insCxP.GetTableName & ".MesDeAplicacion AND " & gUtilSQL.DfSQLYearOfDate(insCxP.GetTableName & ".Fecha") & " = " & insCxP.GetTableName & ".AnoDeAplicacion", insCxP.GetTableName & ".Fecha", gUtilSQL.DfCDateSQL(gUtilSQL.fSimpleSqlValue("01/") & gUtilSQL.CharConcat & insCxP.GetTableName & ".MesDeAplicacion" & gUtilSQL.CharConcat & gUtilSQL.fSimpleSqlValue("/") & gUtilSQL.CharConcat & insCxP.GetTableName & ".AnoDeAplicacion"), True)
               '********* OJO AYRG FIN LINEAS MES/ANO APLICACION *******
               vFNConsecCia = "ConsecutivoCompania"
               vDevuelveLosContabilizados = False
               vCampoClaveEsStrType = True
               vSQLOtherFilters = ""
            Set insCxP = Nothing
         End If
      Case eCG_COBRANZA
         If valContabilizarSolo1Documento Then
            vConjunto = gUtilSQL.fSimpleSqlValue(valNoDocumentoOrigen)
         Else
            Dim insCobranza As clsCobranzaNavigator
            Set insCobranza = New clsCobranzaNavigator
               vTableName = insCobranza.GetTableName
               vFNNumero = "Numero"
               vFNFecha = "Fecha"
               vFNConsecCia = "ConsecutivoCompania"
               vDevuelveLosContabilizados = False
               vCampoClaveEsStrType = True
               vSQLOtherFilters = ""
            Set insCobranza = Nothing
         End If
      Case eCG_PAGOS
         If valContabilizarSolo1Documento Then
            vConjunto = valNoDocumentoOrigen
         Else 'OJO PARA LOTES NO VA A FUNCIONAR
            Dim insPago As clsPagoNavigator
            Set insPago = New clsPagoNavigator
               vTableName = insPago.GetTableName
               vFNNumero = "NumeroComprobante"
               vFNFecha = "Fecha"
               vFNConsecCia = "ConsecutivoCompania"
               vDevuelveLosContabilizados = False
               vCampoClaveEsStrType = False
               vSQLOtherFilters = ""
            Set insPago = Nothing
         End If
      Case eCG_MOVIMIENTO_BANCARIO
         If valContabilizarSolo1Documento Then
            vConjunto = valNoDocumentoOrigen
         Else
            Dim insMovBancario As clsMovimientoBancarioNavigator
            Set insMovBancario = New clsMovimientoBancarioNavigator
               vTableName = insMovBancario.GetTableName
               vFNNumero = "NumeroDocumento"
               vFNFecha = "Fecha"
               vFNConsecCia = "ConsecutivoCompania"
               vDevuelveLosContabilizados = False
               vCampoClaveEsStrType = True
               vSQLOtherFilters = ""
            Set insMovBancario = Nothing
         End If
      Case eCG_RESUMEN_DIARIO_VENTAS
         If valContabilizarSolo1Documento Then
            vConjunto = gUtilSQL.fSimpleSqlValue(valNoDocumentoOrigen)
         Else
            Dim insResumen As clsFacturaNavigator
            Set insResumen = New clsFacturaNavigator
               vTableName = insFactura.GetTableName
               vFNNumero = insFactura.getFN_NUMERO
               vFNFecha = insFactura.getFN_FECHA
               vFNConsecCia = insFactura.getFN_CONSECUTIVO_COMPANIA
               vDevuelveLosContabilizados = False
               vCampoClaveEsStrType = True
               vSQLOtherFilters = ""
            Set insResumen = Nothing
         End If
      Case eCG_ANTICIPO
         If valContabilizarSolo1Documento Then
            vConjunto = valNoDocumentoOrigen
         Else
            Dim insAnticipo As clsAnticipoNavigator
            Set insAnticipo = New clsAnticipoNavigator
               vTableName = insAnticipo.GetTableName
               vFNNumero = "ConsecutivoAnticipo"
               vFNFecha = "Fecha"
               vFNConsecCia = "ConsecutivoCompania"
               vDevuelveLosContabilizados = False
               vCampoClaveEsStrType = False
               If valTipoDocumento = enum_TipoDeAnticipo.eTDA_COBRADO Then
                  vSQLOtherFilters = insAnticipo.GetTableName & ".Tipo = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeAnticipo.eTDA_COBRADO)
               ElseIf valTipoDocumento = enum_TipoDeAnticipo.eTDA_PAGADO Then
                  vSQLOtherFilters = insAnticipo.GetTableName & ".Tipo = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeAnticipo.eTDA_PAGADO)
               End If
            Set insAnticipo = Nothing
         End If
      Case eCG_PAGO_SUELDOS
         If valContabilizarSolo1Documento Then
            vConjunto = valNoDocumentoOrigen
         Else
            Dim insSolicitudesDePago As clsSolicitudesDePagoNavigator
            Set insMovBancario = New clsMovimientoBancarioNavigator
               vTableName = "SAW.SolicitudesDePago"
               vFNNumero = "NumeroDocumento"
               vFNFecha = "Fecha"
               vFNConsecCia = "ConsecutivoCompania"
               vDevuelveLosContabilizados = False
               vCampoClaveEsStrType = True
               vSQLOtherFilters = ""
            Set insMovBancario = Nothing
         End If
      Case eCG_REPOSICION
         If valContabilizarSolo1Documento Then
            vConjunto = valNoDocumentoOrigen
         Else
               vTableName = "Adm.Rendiciones"
               vFNNumero = "Consecutivo"
               vFNFecha = "FechaApertura"
               vFNConsecCia = "ConsecutivoCompania"
               vDevuelveLosContabilizados = False
               vCampoClaveEsStrType = True
               vSQLOtherFilters = ""
            Set insMovBancario = Nothing
         End If
   End Select
   If (valModuloAContabilizar = eCG_INVENTARIO) Then
      vConjunto = ""
   Else
 
     If Not valContabilizarSolo1Documento Then
        vConjunto = fSQLConjuntoDeRecordsDeContabilizacion(vTableName, vFNNumero, vFNFecha, vFNConsecCia, valModuloAContabilizar, valFechaDesde, valFechaHasta, vDevuelveLosContabilizados, vCampoClaveEsStrType, vSQLOtherFilters)
     End If
   End If
   fSQLConjuntoRecordsDeContabilizacion1oMasDocumentos = vConjunto
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLConjuntoRecordsDeContabilizacion1oMasDocumentos", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fEjecutaProcesoPostContabilizacionDeCxP(ByVal valConjuntoDeDocumentosContabilizados As String) As Boolean
   Dim vResultado As Boolean
   Dim Sql As String
   Dim insCxP As clsCxPNavigator
   On Error GoTo h_ERROR
   If gProyCompaniaActual.fPuedoUsarOpcionesDeContribuyenteEspecial Then
      Set insCxP = New clsCxPNavigator
      Sql = " UPDATE " & insCxP.GetTableName & " SET SeContabilRetIva = " & gUtilSQL.fBooleanToSqlValue(True) & " WHERE ConsecutivoCompania = " & gProyCompaniaActual.GetConsecutivoCompania & " AND " & insCxP.GetTableName & ".CodigoProveedor" & gUtilSQL.CharConcat & "'" & gTexto.fSeparadorStandardDeElementosString & "'" & gUtilSQL.CharConcat & insCxP.GetTableName & ".Numero IN (" & valConjuntoDeDocumentosContabilizados & ") AND " & insCxP.GetTableName & ".DondeContabilRetIVA = " & gUtilSQL.fSQLSimpleValueForEnum(enum_DondeEfectuoContabilizacionRetIVA.eDC_CxP) & " AND " & insCxP.GetTableName & ".SeContabilRetIva = " & gUtilSQL.fBooleanToSqlValue(False)
      Set insCxP = Nothing
      vResultado = True
   Else
      vResultado = True
   End If
   fEjecutaProcesoPostContabilizacionDeCxP = vResultado
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fEjecutaProcesoPostContabilizacionDeCxP", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fEjecutaProcesoPostContabilizacionDePago(ByVal valFechaDesde As Date, ByVal valFechaHasta As Date, _
      ByVal valConjuntoDeDocumentosContabilizados As String) As Boolean
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
   If gProyCompaniaActual.fPuedoUsarOpcionesDeContribuyenteEspecial Then
      Set insCxP = New clsCxPNavigator
      Set insPago = New clsPagoNavigator
      Set insDocPagado = New clsDocumentoPagadoNavigator
      sqlTableToUpdate = insCxP.GetTableName
      sqlSetClause = " SET " & insCxP.GetTableName & ".SeContabilRetIva = " & gUtilSQL.fBooleanToSqlValue(True)
      sqlFROM = insCxP.GetTableName & " INNER JOIN (" & insPago.GetTableName & " INNER JOIN " & insDocPagado.GetTableName
      sqlFROM = sqlFROM & " ON (" & insPago.GetTableName & ".NumeroComprobante = " & insDocPagado.GetTableName & ".NumeroComprobante"
      sqlFROM = sqlFROM & ") AND (" & insPago.GetTableName & ".ConsecutivoCompania = " & insDocPagado.GetTableName & ".ConsecutivoCompania"
      sqlFROM = sqlFROM & ")) ON (" & insDocPagado.GetTableName & ".NumeroDelDocumentoPagado = " & insCxP.GetTableName & ".Numero"
      sqlFROM = sqlFROM & ") AND (" & insDocPagado.GetTableName & ".ConsecutivoCompania = " & insCxP.GetTableName & ".ConsecutivoCompania"
      sqlFROM = sqlFROM & ") AND (" & insCxP.GetTableName & ".CodigoProveedor = " & insPago.GetTableName & ".CodigoProveedor)"
      sqlWhere = " WHERE " & insCxP.GetTableName & ".ConsecutivoCompania = " & gProyCompaniaActual.GetConsecutivoCompania
      sqlWhere = sqlWhere & " AND " & insPago.GetTableName & ".NumeroComprobante IN (" & valConjuntoDeDocumentosContabilizados & ")"
      sqlWhere = sqlWhere & " AND " & gUtilSQL.DfSQLDateValueBetween(insPago.GetTableName & ".Fecha", valFechaDesde, valFechaHasta)
      sqlWhere = sqlWhere & " AND " & insCxP.GetTableName & ".MontoRetenido <> 0"
      sqlWhere = sqlWhere & " AND " & insCxP.GetTableName & ".DondeContabilRetIVA = " & gUtilSQL.fSQLSimpleValueForEnum(enum_DondeEfectuoContabilizacionRetIVA.eDC_Pago)
      sqlWhere = sqlWhere & " AND " & insCxP.GetTableName & ".SeContabilRetIva = " & gUtilSQL.fBooleanToSqlValue(False)
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
   fEjecutaProcesoPostContabilizacionDePago = vResultado
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fEjecutaProcesoPostContabilizacionDePago", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fBuscaDescripcionDelComprobanteContableDeMovBancario(ByVal valNumeroDocumento As String) As String
   Dim insMovimiento As clsMovimientoBancarioNavigator
   Dim DescripcionBuscada As String
   Dim Sql As String
   On Error GoTo h_ERROR
   Set insMovimiento = New clsMovimientoBancarioNavigator
   DescripcionBuscada = insMovimiento.fBuscaDatosDelMovimientoBancarioComprobante(gConvert.ConvierteAlong(valNumeroDocumento))
   Set insMovimiento = Nothing
h_EXIT:
   fBuscaDescripcionDelComprobanteContableDeMovBancario = DescripcionBuscada
   On Error GoTo 0
   Exit Function
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
            "fBuscaDescripcionDelComprobanteContableDeMovBancario", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
 End Function

Private Function fElAuxiliarExisteEnElPeriodo(ByVal valCodigoCuenta As String) As Boolean
   Dim ManejaAuxiliares As Boolean
   Dim insCuenta As clsCuentaNavigator
   Dim mrsCuenta As ADODB.Recordset
   Dim Sql As String
   On Error GoTo h_ERROR
   Set mrsCuenta = New ADODB.Recordset
   Set insCuenta = New clsCuentaNavigator
   ManejaAuxiliares = False
   If valCodigoCuenta <> "" Then
       Sql = "SELECT codigo FROM " & insCuenta.GetTableName & " WHERE " & insCuenta.GetTableName & ".Codigo = '" & valCodigoCuenta & "' and " & insCuenta.GetTableName & ".ConsecutivoPeriodo = " & gContPeriodoActual.GetConsecutivoPeriodo
      If gDbUtil.fOpenRecordset(mrsCuenta, Sql, gDefDatabase.Conexion) Then
          If mrsCuenta.RecordCount > 0 Then
             ManejaAuxiliares = True
          Else
             ManejaAuxiliares = False
          End If
       End If
   End If
h_EXIT:
   fElAuxiliarExisteEnElPeriodo = ManejaAuxiliares
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fElAuxiliarExisteEnElPeriodo", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function fCondicion(ByVal valModuloAContabilizar As enum_ComprobanteGeneradoPor, ByVal valncount As Integer) As Integer
Dim vResult As Integer
On Error GoTo h_ERROR
  If (valModuloAContabilizar = eCG_INVENTARIO) Then
    vResult = (valncount + 5)
  Else
    vResult = (valncount + 6)
 End If
h_EXIT:
   fCondicion = vResult
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fCondicion", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function fFechaReferenciaSegunModulo(ByVal valModuloAContabilizar As enum_ComprobanteGeneradoPor, ByVal valFechaDefaul As Date, ByVal Record As ADODB.Recordset) As Date
Dim vResult As Date
On Error GoTo h_ERROR
  If (valModuloAContabilizar = eCG_INVENTARIO) Then
    vResult = Record("FECHA").value
  Else
    vResult = valFechaDefaul
 End If
h_EXIT:
   fFechaReferenciaSegunModulo = vResult
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fFechaReferenciaSegunModulo", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fCXPGeneradaDeCompras(ByVal valConsecutivoCxP As Long) As Boolean
Dim insCxPLocal   As clsCxPNavigator
Dim vResult As Boolean
On Error GoTo h_ERROR
 vResult = False
 Set insCxPLocal = New clsCxPNavigator
 
 If (insCxPLocal.fSearchByConsecutivoCxP(valConsecutivoCxP, True)) Then
  vResult = (insCxPLocal.GetCxPgeneradaPorAsEnum = eGP_COMPRA)
 End If
 Set insCxPLocal = Nothing
h_EXIT:
 fCXPGeneradaDeCompras = vResult
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fCXPGeneradaDeCompras", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Sub sGenerarComprobanteDeAnulacionDeCobranzaOPago(ByVal valGeneradoPor As enum_ComprobanteGeneradoPor, ByVal valNoDocumentoOrigen As String, ByVal valConsecutivoDocumento As Long, ByVal valStringField As Boolean, ByVal valFechaDeAnulacion As Date)
   Dim insComprobanteNavigator As clsComprobanteNavigator
   Dim insFrmComprobanteInput As frmComprobanteInput
   Dim refNumeroComprobante As String
   On Error GoTo h_ERROR
   Set insComprobanteNavigator = New clsComprobanteNavigator
   If Not insComprobanteNavigator.fSearchByNoDocumentoOrigenYGeneradoPor(valGeneradoPor, valNoDocumentoOrigen, True, True, valConsecutivoDocumento, valStringField) Then
               gMessage.Advertencia "No existe Comprobante Contable asociado al documento " & _
               valNoDocumentoOrigen & " del módulo " & _
               gEnumProyectoWincont.enumComprobanteGeneradoPorToString(valGeneradoPor)
   Else
      refNumeroComprobante = insComprobanteNavigator.GetNumero
      Set insFrmComprobanteInput = New frmComprobanteInput
      If insComprobanteNavigator.fSearchByField("Numero", refNumeroComprobante, True) Then
         If insComprobanteNavigator.GetStatusStr = gEnumProyectoWincont.enumStatusComprobanteToString(eSC_DESCUADRADO) Then
            gMessage.Advertencia "No se puede reversar un comprobante descuadrado.  "
         Else
            insComprobanteNavigator.sCreaCopia reversar
               insComprobanteNavigator.SetFecha valFechaDeAnulacion
               If gContPeriodoActual.fBuscaElPeriodoDeFechaYsiExisteLoColocaComoPeriodoActual(insComprobanteNavigator.GetFecha, True) Then
                  insComprobanteNavigator.SetConsecutivoPeriodo gContPeriodoActual.GetConsecutivoPeriodo
                  insComprobanteNavigator.SetNumero insComprobanteNavigator.fGenerarNuevoNumeroCompletoDeComprobante
               Else
                  gMessage.Advertencia "No se puede crear el comprobante de reverso. No existe período válido."
                  GoTo h_EXIT
               End If
            insComprobanteNavigator.SetNoDocumentoOrigen valNoDocumentoOrigen
            If valGeneradoPor = eCG_COBRANZA Then
               insComprobanteNavigator.SetGeneradoPorStr gEnumProyectoWincont.enumComprobanteGeneradoPorToString(eCG_ANULACION_COBRANZA)
            ElseIf valGeneradoPor = eCG_PAGOS Then
               insComprobanteNavigator.SetGeneradoPorStr gEnumProyectoWincont.enumComprobanteGeneradoPorToString(eCG_ANULACION_PAGO)
            ElseIf valGeneradoPor = eCG_PAGO_SUELDOS Then
               insComprobanteNavigator.SetGeneradoPorStr gEnumProyectoWincont.enumComprobanteGeneradoPorToString(eCG_ANULACION_PAGO_SUELDOS)
            End If
            If insComprobanteNavigator.fInsert(insertar, True, True) Then
               If valGeneradoPor = eCG_COBRANZA And gProyParametrosCompania.GetUsarVentasConIvaDiferido Then
                  sProcesoPostDescontabilizacionDeCobranza (valNoDocumentoOrigen)
               End If
               If insComprobanteNavigator.fSearchByField("Numero", insComprobanteNavigator.GetNumero, True, True) Then
                  insFrmComprobanteInput.sInitLookAndFeelAndSetValues consultar, Nothing, False, False, insComprobanteNavigator
               End If
            End If
         End If
      End If
      Set insFrmComprobanteInput = Nothing
   End If
   Set insComprobanteNavigator = Nothing
h_EXIT: On Error GoTo 0
      Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sGenerarComprobanteDeAnulacionDeCobranzaOPago", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Function fSqlAsientosEnCeroDeCostos(ByVal valFechaDesde As Date) As String
   Dim Sql As String
   Sql = "SELECT " & gUtilSQL.fSimpleSqlValue(valFechaDesde) & "AS FECHA, 'DEBE', 0,  " & gUtilSQL.fSimpleSqlValue(gProyReglasDeContabilizacion.GetCuentaCostoDeVenta) & ", "
   Sql = Sql & " 'NA','NA','0','Factura' FROM articuloInventario  UNION"
   Sql = Sql & " SELECT " & gUtilSQL.fSimpleSqlValue(valFechaDesde) & " AS FECHA, 'HABER', 0, " & gUtilSQL.fSimpleSqlValue(gProyReglasDeContabilizacion.GetCuentaInventario) & ", "
   Sql = Sql & " 'NA', 'NA', '0', 'Factura' FROM articuloInventario "
   On Error GoTo h_ERROR
   fSqlAsientosEnCeroDeCostos = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSqlAsientosEnCeroDeCostos", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function setProcesoDeImpresionDeComprobanteDePagoSueldo(ByRef refForm As Object, ByVal valNumeroDeMovBancario As Long)
   mImprimirEspecialAlGrabarComprobanteSueldo = True
   Set mPagoProcesoDeImpresionEspecial = refForm
   mNumeroDeMovBancario = valNumeroDeMovBancario
End Function

Private Function fSqlComprobantePagoSueldos(ByVal valConsecutivoCompania As Long, ByVal valNumeroDocumento As String, ByVal valDescripcionDelAsiento As String, ByVal valEsUnReverso As Boolean) As String
   Dim vSQLWhere As String
   Dim sqlCuentaHaber As String
   Dim sqlCuentaDebe As String
   Dim vSQL As String
   Dim vMonto As String
   Dim vDescripcionAsiento As String
   Dim SqlTasaDeCambio As String
   Dim vCuentaContableDebe As String
   Dim vCuentaContableHaber As String
   Dim vMontoAsiento As String
   Dim insLibSaW As clsLibSAW
   On Error GoTo h_ERROR
   Set insLibSaW = New clsLibSAW
   vMonto = " MovimientosBancarios_B1.Monto * MovimientosBancarios_B1.CambioAbolivares "
   sqlCuentaHaber = gUtilSQL.getIIF("MovimientosBancarios_B1.CuentaContable <> '' ", "MovimientosBancarios_B1.CuentaContable", "'" & gProyReglasDeContabilizacion.GetCtaDePagosSueldosBanco & "'")
   sqlCuentaDebe = gProyReglasDeContabilizacion.GetCtaDePagosSueldos
   vSQLWhere = gUtilSQL.fSQLValueWithAnd("", "MovimientosBancarios_B1.NumeroDocumento", valNumeroDocumento, False)
   vSQLWhere = gUtilSQL.fSQLEnumValueWithAnd(vSQLWhere, "MovimientosBancarios_B1.GeneradoPor", enum_GeneradoPor.eGP_SOLICITUD_DE_PAGO)
   vSQLWhere = gUtilSQL.fSQLNumberValueWithAnd(vSQLWhere, "MovimientosBancarios_B1.ConsecutivoCompania", valConsecutivoCompania)
   vSQLWhere = gUtilSQL.getWhereSQL(vSQLWhere)
   vSQL = " SELECT "
   vSQL = vSQL & "  IGV_BeneficiarioSolicitudesDePago_B1.Codigo,"
   vSQL = vSQL & fAgregaUnaCuentaAlSQL("DEBE", valEsUnReverso, vMonto, sqlCuentaDebe, False, "", valNumeroDocumento, valDescripcionDelAsiento, "", "") & ", "
   vSQL = vSQL & fAgregaUnaCuentaAlSQL("HABER", valEsUnReverso, vMonto, sqlCuentaHaber, False, "", valNumeroDocumento, valDescripcionDelAsiento, "", "")
   vSQL = vSQL & " FROM IGV_MovimientosBancarios_B1 AS  MovimientosBancarios_B1 "
   vSQL = vSQL & "INNER JOIN dbo.IGV_BeneficiarioSolicitudesDePago_B1 ON "
   vSQL = vSQL & "MovimientosBancarios_B1.ConsecutivoCompania = dbo.IGV_BeneficiarioSolicitudesDePago_B1.ConsecutivoCompania AND"
   If Not valEsUnReverso Then
      vSQL = vSQL & "  MovimientosBancarios_B1.NroMovimientoRelacionado = dbo.IGV_BeneficiarioSolicitudesDePago_B1.NumeroAsociado AND"
   Else
      vSQL = vSQL & "  MovimientosBancarios_B1.NroMovimientoRelacionado = 'RE'" & gUtilSQL.CharConcat & " dbo.IGV_BeneficiarioSolicitudesDePago_B1.NumeroAsociado AND"
   End If
   vSQL = vSQL & "  MovimientosBancarios_B1.Monto = dbo.IGV_BeneficiarioSolicitudesDePago_B1.Monto"
   vSQL = vSQL & vSQLWhere
   '// DEBITO BANCARIO
   vSQLWhere = gUtilSQL.fSQLValueWithAnd("", "MovimientosBancarios_B1.NumeroDocumento", valNumeroDocumento, False)
   vSQLWhere = gUtilSQL.fSQLEnumValueWithAnd(vSQLWhere, "MovimientosBancarios_B1.GeneradoPor", enum_GeneradoPor.eGP_DEBITO_BANCARIO)
   vSQLWhere = gUtilSQL.fSQLNumberValueWithAnd(vSQLWhere, "MovimientosBancarios_B1.ConsecutivoCompania", valConsecutivoCompania)
   vSQLWhere = gUtilSQL.getWhereSQL(vSQLWhere)
   vDescripcionAsiento = insLibSaW.fTituloITFEnCtaBancaria & " / " & valDescripcionDelAsiento
   vCuentaContableDebe = gUtilSQL.getIIF(gUtilSQL.fFirst("IGV_movimientoBancarioRelacionado.TipoConcepto", "NONE") & " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_IngresoEgreso.eIE_INGRESO), gUtilSQL.fSimpleSqlValue(gProyReglasDeContabilizacion.GetCuentaCreditoBancarioGasto), gUtilSQL.fSimpleSqlValue(gProyReglasDeContabilizacion.GetCuentaDebitoBancarioGasto), True)
   vCuentaContableHaber = gUtilSQL.getIIF(gUtilSQL.fFirst("cuentaBancaria.CuentaContable", "NONE") & " = ''", gUtilSQL.fSimpleSqlValue(gProyReglasDeContabilizacion.GetCuentaDebitoBancarioBancos), gUtilSQL.fFirst("cuentaBancaria.CuentaContable", "NONE"), True)
   vMonto = "SUM(MovimientosBancarios_B1.Monto * MovimientosBancarios_B1.CambioAbolivares)"
   vSQL = vSQL & " UNION SELECT MovimientosBancarios_B1.CodigoConcepto, "
   vSQL = vSQL & fAgregaUnaCuentaAlSQL("DEBE", valEsUnReverso, vMonto, vCuentaContableDebe, False, vCuentaContableDebe, valNumeroDocumento, vDescripcionAsiento, "", "") & ", "
   vSQL = vSQL & fAgregaUnaCuentaAlSQL("HABER", valEsUnReverso, vMonto, vCuentaContableHaber, False, vCuentaContableHaber, valNumeroDocumento, vDescripcionAsiento, "", "")
   vSQL = vSQL & " FROM IGV_MovimientosBancarios_B1 AS  MovimientosBancarios_B1   INNER JOIN cuentaBancaria ON "
   vSQL = vSQL & " MovimientosBancarios_B1.ConsecutivoCompania = cuentaBancaria.ConsecutivoCompania AND "
   vSQL = vSQL & " MovimientosBancarios_B1.CodigoCtaBancaria = cuentaBancaria.codigo INNER JOIN IGV_MovimientosBancarios_B1 AS IGV_movimientoBancarioRelacionado "
   vSQL = vSQL & " ON  MovimientosBancarios_B1.NroMovimientoRelacionado = IGV_movimientoBancarioRelacionado .ConsecutivoMovimiento "
   vSQL = vSQL & " AND MovimientosBancarios_B1.consecutivoCompania = IGV_movimientoBancarioRelacionado.consecutivoCompania"
   vSQL = vSQL & vSQLWhere
   vSQL = vSQL & " GROUP BY MovimientosBancarios_B1.CodigoConcepto"
   Set insLibSaW = Nothing
   fSqlComprobantePagoSueldos = vSQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSqlComprobantePagoSueldos", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fBuscaDescripcionDelComprobanteContableDePagoDeSueldos(ByVal valConsecutivoDocOrigen As Long) As String
   Dim insMovimiento As clsMovimientoBancarioNavigator
   Dim DescripcionBuscada As String
   Dim Sql As String
   On Error GoTo h_ERROR
   Set insMovimiento = New clsMovimientoBancarioNavigator
   DescripcionBuscada = insMovimiento.fBuscaDatosDelMovimientoBancarioAsociadoApagoSueldos(valConsecutivoDocOrigen)
   Set insMovimiento = Nothing
h_EXIT:
   fBuscaDescripcionDelComprobanteContableDePagoDeSueldos = DescripcionBuscada
   On Error GoTo 0
   Exit Function
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
            "fBuscaDescripcionDelComprobanteContableDePagoDeSueldos", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
 End Function

Public Sub sGenerarComprobanteDeAnulacionDeMovBancario(ByVal valGeneradoPor As enum_ComprobanteGeneradoPor, ByVal valNoDocumentoOrigen As String, ByVal valConsecutivoDocumento As Long, ByVal valStringField As Boolean, ByVal valFechaDeAnulacion As Date)
   Dim insComprobanteNavigator As clsComprobanteNavigator
   Dim insFrmComprobanteInput As frmComprobanteInput
   Dim refNumeroComprobante As String
   On Error GoTo h_ERROR
   Set insComprobanteNavigator = New clsComprobanteNavigator
   If Not insComprobanteNavigator.fSearchByNoDocumentoOrigenYGeneradoPor(valGeneradoPor, valNoDocumentoOrigen, True, True, valConsecutivoDocumento, valStringField) Then
               gMessage.Advertencia "No existe Comprobante Contable asociado al documento " & _
               valNoDocumentoOrigen & " del módulo " & _
               gEnumProyectoWincont.enumComprobanteGeneradoPorToString(valGeneradoPor)
   Else
      refNumeroComprobante = insComprobanteNavigator.GetNumero
      Set insFrmComprobanteInput = New frmComprobanteInput
      If insComprobanteNavigator.fSearchByField("Numero", refNumeroComprobante, True) Then
         If insComprobanteNavigator.GetStatusStr = gEnumProyectoWincont.enumStatusComprobanteToString(eSC_DESCUADRADO) Then
            gMessage.Advertencia "No se puede reversar un comprobante descuadrado.  "
         Else
            insComprobanteNavigator.sCreaCopia reversar
               insComprobanteNavigator.SetFecha valFechaDeAnulacion
               If gContPeriodoActual.fBuscaElPeriodoDeFechaYsiExisteLoColocaComoPeriodoActual(insComprobanteNavigator.GetFecha, True) Then
                  insComprobanteNavigator.SetConsecutivoPeriodo gContPeriodoActual.GetConsecutivoPeriodo
                  insComprobanteNavigator.SetNumero insComprobanteNavigator.fGenerarNuevoNumeroCompletoDeComprobante
               Else
                  gMessage.Advertencia "No se puede crear el comprobante de reverso. No existe período válido."
                  GoTo h_EXIT
               End If
            insComprobanteNavigator.SetNoDocumentoOrigen valNoDocumentoOrigen
            If valGeneradoPor = eCG_MOVIMIENTO_BANCARIO Then
               insComprobanteNavigator.SetGeneradoPorStr gEnumProyectoWincont.enumComprobanteGeneradoPorToString(eCG_ANULACION_MOVIMIENTO_BANCARIO)
            End If
            If insComprobanteNavigator.fInsert(insertar, True, True) Then
               If insComprobanteNavigator.fSearchByField("Numero", insComprobanteNavigator.GetNumero, True, True) Then
                  insFrmComprobanteInput.sInitLookAndFeelAndSetValues consultar, Nothing, False, False, insComprobanteNavigator
               End If
            End If
         End If
      End If
      Set insFrmComprobanteInput = Nothing
   End If
   Set insComprobanteNavigator = Nothing
h_EXIT: On Error GoTo 0
      Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sGenerarComprobanteDeAnulacionDeMovBancario", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Function fSQLRendicionEntreFechas(ByVal valFechaDesde As Date, ByVal valFechaHasta As Date, _
            ByVal valEsUnReverso As Boolean, ByVal valDescripcionDelAsiento As String, _
            ByVal valContabilizarSolo1Documento As Boolean, ByVal valNoDocumentoOrigen As String, _
            ByVal valTipoDocumento As Integer, ByVal valConsecutivoDoc As String) As String
   Dim ConjuntoDeRendicionsinContabilizar As String
   Dim DebeOhaber As String
   Dim Sql As String
   Dim CodigoCuentaIVA As String
   Dim CodigoCuentaIVAUsaAuxiliar As Boolean
   Dim SqlTasaDeCambio As String
   Dim CondicionDeStatusAnulado As String
   Dim varConsecutivoBeneficiario As String
   Dim numeroReferencia As String
   Dim sqlMontoMovimientoBancario As String
   Dim sqlCuentaBancariaIDB As String
   Dim sqlBancoCuentaContable As String
   Dim descripcionMovBancIDB As String
   Dim insLibSaW As clsLibSAW
   On Error GoTo h_ERROR
   Set insLibSaW = New clsLibSAW
   CodigoCuentaIVA = ""
   Sql = ""
   SqlTasaDeCambio = 1
   CondicionDeStatusAnulado = gUtilSQL.fFirst("Adm.Rendicion.StatusRendicion", "NONE") & " = '" & gConvert.enumerativoAChar(enum_StatusRendicion.eSRd_ANULADA) & "'"
   numeroReferencia = valNoDocumentoOrigen
   If valContabilizarSolo1Documento Then
      gTexto.ReemplazaCaracteresEnElString numeroReferencia, vbTab, "-"
   End If
   ConjuntoDeRendicionsinContabilizar = fSQLConjuntoRecordsDeContabilizacion1oMasDocumentos(eCG_REPOSICION, valContabilizarSolo1Documento, valConsecutivoDoc, valFechaDesde, valFechaHasta, valTipoDocumento)
   If ConjuntoDeRendicionsinContabilizar = "" Then
      GoTo h_EXIT
   End If
   sqlBancoCuentaContable = "Saw.CuentaBancaria.CuentaContable"
   sqlMontoMovimientoBancario = "(SUM(Movimientobancario.Monto * " & SqlTasaDeCambio & "))"
   sqlCuentaBancariaIDB = gUtilSQL.fFirst(gUtilSQL.getIIF(sqlBancoCuentaContable & " <> ''", sqlBancoCuentaContable, gUtilSQL.fSimpleSqlValue(gProyReglasDeContabilizacion.GetCuentaDebitoBancarioBancos), True), "NONE")
   descripcionMovBancIDB = insLibSaW.fTituloITFEnCtaBancaria & " / " & valDescripcionDelAsiento


   Sql = "SELECT Adm.Rendicion.ConsecutivoBeneficiario, "
   Sql = Sql & fAgregaUnaCuentaAlSQL("DEBE", valEsUnReverso, "SUM((Adm.DetalleDeRendicion.MontoExento + Adm.DetalleDeRendicion.MontoGravable + Adm.DetalleDeRendicion.MontoGravableAlicuotaEspecial1 + Adm.DetalleDeRendicion.MontoGravableAlicuotaEspecial2 ) * " & SqlTasaDeCambio & ")", gProyReglasDeContabilizacion.GetCuentaRendicionesGasto, False, "", numeroReferencia, valDescripcionDelAsiento, CondicionDeStatusAnulado, "") & ", "
   Sql = Sql & fAgregaUnaCuentaAlSQL("DEBE", valEsUnReverso, "SUM((Adm.DetalleDeRendicion.MontoIva + Adm.DetalleDeRendicion.MontoIVAAlicuotaEspecial1 + Adm.DetalleDeRendicion.MontoIVAAlicuotaEspecial2) * " & SqlTasaDeCambio & ")", gProyReglasDeContabilizacion.GetCuentaRendicionesIva, False, "", numeroReferencia, valDescripcionDelAsiento, CondicionDeStatusAnulado, "") & ", "
   Sql = Sql & fAgregaUnaCuentaAlSQL("DEBE", valEsUnReverso, "((Adm.Rendicion.TotalAdelantos - Adm.Rendicion.TotalGastos) * " & SqlTasaDeCambio & ")", gProyReglasDeContabilizacion.GetCuentaRendicionesBanco, False, "", numeroReferencia, valDescripcionDelAsiento, CondicionDeStatusAnulado, "") & ", "
   Sql = Sql & fAgregaUnaCuentaAlSQL("HABER", valEsUnReverso, "((Adm.Rendicion.TotalAdelantos)  * " & SqlTasaDeCambio & ")", gProyReglasDeContabilizacion.GetCuentaRendicionesAnticipos, False, "", numeroReferencia, valDescripcionDelAsiento, CondicionDeStatusAnulado, "")
   Sql = Sql & " FROM Saw.Beneficiario INNER JOIN "
   Sql = Sql & " Adm.Rendicion ON (Saw.Beneficiario.ConsecutivoCompania =  Adm.Rendicion.ConsecutivoCompania "
   Sql = Sql & " AND Saw.Beneficiario.Consecutivo =  Adm.Rendicion.ConsecutivoBeneficiario)"
   Sql = Sql & " INNER JOIN Adm.DetalleDeRendicion ON (Adm.Rendicion.ConsecutivoCompania =  Adm.DetalleDeRendicion.ConsecutivoCompania"
   Sql = Sql & " AND Adm.Rendicion.Consecutivo = Adm.DetalleDeRendicion.ConsecutivoRendicion)"
   Sql = Sql & " WHERE Adm.Rendicion.ConsecutivoCompania = " & gProyCompaniaActual.GetConsecutivoCompania
   Sql = Sql & " AND Adm.Rendicion.Consecutivo IN (" & ConjuntoDeRendicionsinContabilizar & ")"
   Sql = Sql & " AND Adm.Rendicion.TipoDeDocumento = " & gUtilSQL.fSimpleSqlValue(enum_TipoDeDocumentoRendicion.eTDDR_Rendicion)
   Sql = Sql & " AND " & gUtilSQL.DfSQLDateValueBetween("Adm.Rendicion.FechaApertura", valFechaDesde, valFechaHasta)
   Sql = Sql & " GROUP BY Adm.Rendicion.ConsecutivoBeneficiario, Adm.Rendicion.TotalAdelantos, Adm.Rendicion.TotalGastos"
   CondicionDeStatusAnulado = ""
   Sql = Sql & " UNION "
   Sql = Sql & " SELECT MovimientoBancario.CodigoConcepto, "
   Sql = Sql & fAgregaUnaCuentaAlSQL("DEBE", valEsUnReverso, sqlMontoMovimientoBancario, gProyReglasDeContabilizacion.GetCuentaDebitoBancarioGasto, False, "", numeroReferencia, descripcionMovBancIDB, CondicionDeStatusAnulado, "") & ", "
   Sql = Sql & fAgregaUnaCuentaAlSQL("HABER", valEsUnReverso, sqlMontoMovimientoBancario, sqlCuentaBancariaIDB, False, "", numeroReferencia, descripcionMovBancIDB, CondicionDeStatusAnulado, "") & ", "
   Sql = Sql & fAgregaUnaCuentaAlSQL("DEBE", valEsUnReverso, "0", "NA", False, "", "", "", "", "") & ", "
   Sql = Sql & fAgregaUnaCuentaAlSQL("HABER", valEsUnReverso, "0", "NA", False, "", "", "", "", "")
   Sql = Sql & " FROM MovimientoBancario INNER JOIN Saw.CuentaBancaria ON "
   Sql = Sql & " MovimientoBancario.ConsecutivoCompania = Saw.CuentaBancaria.ConsecutivoCompania"
   Sql = Sql & " AND MovimientoBancario.CodigoCtaBancaria = Saw.CuentaBancaria.Codigo"
   Sql = Sql & " WHERE MovimientoBancario.ConsecutivoCompania = " & gProyCompaniaActual.GetConsecutivoCompania
   Sql = Sql & " AND MovimientoBancario.NroMovimientoRelacionado IN(" & ConjuntoDeRendicionsinContabilizar & ")"
   Sql = Sql & " AND MovimientoBancario.GeneradoPor = " & gUtilSQL.fSQLSimpleValueForEnum(enum_GeneradoPor.eGP_DEBITO_BANCARIO)
   Sql = Sql & " AND " & gUtilSQL.DfSQLDateValueBetween("MovimientoBancario.Fecha", valFechaDesde, valFechaHasta)
   Sql = Sql & " GROUP BY MovimientoBancario.CodigoConcepto"
   Set insLibSaW = Nothing
h_EXIT:
   fSQLRendicionEntreFechas = Sql
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLRendicionEntreFechas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Sub sGenerarComprobanteDeAnulacionDeRendicion(ByVal valGeneradoPor As enum_ComprobanteGeneradoPor, ByVal valNoDocumentoOrigen As String, ByVal valConsecutivoDocumento As Long, ByVal valStringField As Boolean, ByVal valFechaDeAnulacion As Date, ByVal valAccionAnulacionModulo As enum_ComprobanteGeneradoPor)
   Dim insComprobanteNavigator As clsComprobanteNavigator
   Dim insFrmComprobanteInput As frmComprobanteInput
   Dim refNumeroComprobante As String
   On Error GoTo h_ERROR
   Set insComprobanteNavigator = New clsComprobanteNavigator
   If Not insComprobanteNavigator.fSearchByNoDocumentoOrigenYGeneradoPor(valGeneradoPor, valNoDocumentoOrigen, True, True, valConsecutivoDocumento, valStringField) Then
               gMessage.Advertencia "No existe Comprobante Contable asociado al documento " & _
               valNoDocumentoOrigen & " del módulo " & _
               gEnumProyectoWincont.enumComprobanteGeneradoPorToString(valGeneradoPor)
   Else
      refNumeroComprobante = insComprobanteNavigator.GetNumero
      Set insFrmComprobanteInput = New frmComprobanteInput
      If insComprobanteNavigator.fSearchByField("Numero", refNumeroComprobante, True) Then
         If insComprobanteNavigator.GetStatusStr = gEnumProyectoWincont.enumStatusComprobanteToString(eSC_DESCUADRADO) Then
            gMessage.Advertencia "No se puede reversar un comprobante descuadrado.  "
         Else
            insComprobanteNavigator.sCreaCopia reversar
               insComprobanteNavigator.SetFecha valFechaDeAnulacion
               If gContPeriodoActual.fBuscaElPeriodoDeFechaYsiExisteLoColocaComoPeriodoActual(insComprobanteNavigator.GetFecha, True) Then
                  insComprobanteNavigator.SetConsecutivoPeriodo gContPeriodoActual.GetConsecutivoPeriodo
                  insComprobanteNavigator.SetNumero insComprobanteNavigator.fGenerarNuevoNumeroCompletoDeComprobante
               Else
                  gMessage.Advertencia "No se puede crear el comprobante de reverso. No existe período válido."
                  GoTo h_EXIT
               End If
            insComprobanteNavigator.SetNoDocumentoOrigen valNoDocumentoOrigen
            insComprobanteNavigator.SetGeneradoPorStr gEnumProyectoWincont.enumComprobanteGeneradoPorToString(valAccionAnulacionModulo)
            If insComprobanteNavigator.fInsert(insertar, True, True) Then
               If insComprobanteNavigator.fSearchByField("Numero", insComprobanteNavigator.GetNumero, True, True) Then
                  
                    If (gProyReglasDeContabilizacion.fEditaComprobanteDespuesDeInsertar(eCG_REPOSICION)) Then
                        insFrmComprobanteInput.sInitLookAndFeelAndSetValues Modificar, Nothing, False, False, insComprobanteNavigator
                    Else
                        gMessage.exito "El Comprobante fué generado exitosamente"
                    End If
                
               
               End If
            End If
         End If
      End If
      Set insFrmComprobanteInput = Nothing
   End If
   Set insComprobanteNavigator = Nothing
h_EXIT: On Error GoTo 0
      Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sGenerarComprobanteDeAnulacionDeRendicion", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sGenerarComprobanteDeAnulacionDeCajaChica(ByVal valGeneradoPor As enum_ComprobanteGeneradoPor, ByVal valNoDocumentoOrigen As String, ByVal valConsecutivoDocumento As Long, ByVal valStringField As Boolean, ByVal valFechaDeAnulacion As Date)
   Dim insComprobanteNavigator As clsComprobanteNavigator
   Dim insFrmComprobanteInput As frmComprobanteInput
   Dim refNumeroComprobante As String
   On Error GoTo h_ERROR
   Set insComprobanteNavigator = New clsComprobanteNavigator
   If Not insComprobanteNavigator.fSearchByNoDocumentoOrigenYGeneradoPor(valGeneradoPor, valNoDocumentoOrigen, True, True, valConsecutivoDocumento, valStringField) Then
               gMessage.Advertencia "No existe Comprobante Contable asociado al documento " & _
               valNoDocumentoOrigen & " del módulo " & _
               gEnumProyectoWincont.enumComprobanteGeneradoPorToString(valGeneradoPor)
   Else
      refNumeroComprobante = insComprobanteNavigator.GetNumero
      Set insFrmComprobanteInput = New frmComprobanteInput
      If insComprobanteNavigator.fSearchByField("Numero", refNumeroComprobante, True) Then
         If insComprobanteNavigator.GetStatusStr = gEnumProyectoWincont.enumStatusComprobanteToString(eSC_DESCUADRADO) Then
            gMessage.Advertencia "No se puede reversar un comprobante descuadrado.  "
         Else
            insComprobanteNavigator.sCreaCopia reversar
               insComprobanteNavigator.SetFecha valFechaDeAnulacion
               If gContPeriodoActual.fBuscaElPeriodoDeFechaYsiExisteLoColocaComoPeriodoActual(insComprobanteNavigator.GetFecha, True) Then
                  insComprobanteNavigator.SetConsecutivoPeriodo gContPeriodoActual.GetConsecutivoPeriodo
                  insComprobanteNavigator.SetNumero insComprobanteNavigator.fGenerarNuevoNumeroCompletoDeComprobante
               Else
                  gMessage.Advertencia "No se puede crear el comprobante de reverso. No existe período válido."
                  GoTo h_EXIT
               End If
            insComprobanteNavigator.SetNoDocumentoOrigen valNoDocumentoOrigen
            insComprobanteNavigator.SetGeneradoPorStr gEnumProyectoWincont.enumComprobanteGeneradoPorToString(eCG_ANULACION_REPOSICION)
            If insComprobanteNavigator.fInsert(insertar, True, True) Then
               If insComprobanteNavigator.fSearchByField("Numero", insComprobanteNavigator.GetNumero, True, True) Then
                  insFrmComprobanteInput.sInitLookAndFeelAndSetValues consultar, Nothing, False, False, insComprobanteNavigator
               End If
            End If
         End If
      End If
      Set insFrmComprobanteInput = Nothing
   End If
   Set insComprobanteNavigator = Nothing
h_EXIT: On Error GoTo 0
      Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sGenerarComprobanteDeAnulacionDeCajaChica", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Function fSQLCajaChicaEntreFechas(ByVal valFechaDesde As Date, ByVal valFechaHasta As Date, _
            ByVal valEsUnReverso As Boolean, ByVal valDescripcionDelAsiento As String, _
            ByVal valContabilizarSolo1Documento As Boolean, ByVal valNoDocumentoOrigen As String, _
            ByVal valTipoDocumento As Integer, ByVal valConsecutivoDoc As Long, ByVal valTipoAlicPorContIGTF As enum_TipoAlicPorContIGTF) As String
   Dim Sql As String
   On Error GoTo h_ERROR
   If gProyReglasDeContabilizacion.GetMostrarDesglosadoCajaChica Or valContabilizarSolo1Documento Then
      Sql = fSQLCajaChicaEntreFechasDesglozado(valFechaDesde, valFechaHasta, valEsUnReverso, valDescripcionDelAsiento, valContabilizarSolo1Documento, valNoDocumentoOrigen, valTipoDocumento, valConsecutivoDoc, valTipoAlicPorContIGTF)
   Else
      Sql = fSQLCajaChicaEntreFechasSinDesglozar(valFechaDesde, valFechaHasta, valEsUnReverso, valDescripcionDelAsiento, valContabilizarSolo1Documento, valNoDocumentoOrigen, valTipoDocumento, valTipoAlicPorContIGTF)
   End If
h_EXIT:
   fSQLCajaChicaEntreFechas = Sql
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLCajaChicaEntreFechas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLCajaChicaEntreFechasSinDesglozar(ByVal valFechaDesde As Date, ByVal valFechaHasta As Date, _
            ByVal valEsUnReverso As Boolean, ByVal valDescripcionDelAsiento As String, _
            ByVal valContabilizarSolo1Documento As Boolean, ByVal valNoDocumentoOrigen As String, _
            ByVal valTipoDocumento As Integer, ByVal valTipoAlicPorContIGTF As enum_TipoAlicPorContIGTF) As String
   Dim ConjuntoDeRendicionsinContabilizar As String
   Dim DebeOhaber As String
   Dim Sql As String
   Dim CodigoCuentaIVA As String
   Dim CodigoCuentaIVAUsaAuxiliar As Boolean
   Dim SqlTasaDeCambio As String
   Dim CondicionDeStatusAnulado As String
   Dim varConsecutivoBeneficiario As String
   Dim numeroReferencia As String
   Dim sqlMontoMovimientoBancario As String
   Dim sqlCuentaBancariaIDB As String
   Dim sqlBancoCuentaContable As String
   Dim sqlCuentaContableCajaChica As String
   Dim descripcionMovBancIDB As String
   Dim insLibSaW As clsLibSAW
   Dim varcodigoProveedor As String
   Dim varAuxiliarGasto As Boolean
   Dim insProveedor As clsProveedorNavigator
   Dim sqlCuentaContableReposicion As String
   
   Dim insMovimiento As clsMovimientoBancarioNavigator
   Dim varGeneraITF As Boolean
   Dim porcentajeITF As Currency
   Dim descripcionMovBancario As String
   Dim sqlItf As String
   
   On Error GoTo h_ERROR
   
   Set insMovimiento = New clsMovimientoBancarioNavigator
   Set insLibSaW = New clsLibSAW
   Set insProveedor = New clsProveedorNavigator
   CodigoCuentaIVA = ""
   Sql = ""
   SqlTasaDeCambio = 1
   CondicionDeStatusAnulado = gUtilSQL.fFirst("Adm.Rendicion.StatusRendicion", "NONE") & " = '" & gConvert.enumerativoAChar(enum_StatusRendicion.eSRd_ANULADA) & "'"
   numeroReferencia = valNoDocumentoOrigen
   If valContabilizarSolo1Documento Then
      gTexto.ReemplazaCaracteresEnElString numeroReferencia, vbTab, "-"
   End If
   ConjuntoDeRendicionsinContabilizar = fSQLConjuntoRecordsDeContabilizacion1oMasDocumentos(eCG_REPOSICION, valContabilizarSolo1Documento, valNoDocumentoOrigen, valFechaDesde, valFechaHasta, valTipoDocumento)
   If ConjuntoDeRendicionsinContabilizar = "" Then
      GoTo h_EXIT
   End If
   Sql = "SELECT Adm.Rendicion.ConsecutivoBeneficiario, "
   Sql = Sql & fAgregaUnaCuentaAlSQL("DEBE", valEsUnReverso, _
   "SUM(" & _
      gUtilSQL.getIIF("Adm.DetalleDeRendicion.GeneradaPor = " & gUtilSQL.fSimpleSqlValue(gConvert.enumerativoAChar(enum_GeneradoPor.eGP_RENDICION)) _
      , "((Adm.DetalleDeRendicion.MontoExento + Adm.DetalleDeRendicion.MontoGravable - Adm.DetalleDeRendicion.MontoRetencion) * " & SqlTasaDeCambio & ")" _
      , "0", True) & ")", _
   gUtilSQL.getIIF(gUtilSQL.fFirst("Adm.DetalleDeRendicion.GeneradaPor", "") & " = " & gUtilSQL.fSimpleSqlValue(gConvert.enumerativoAChar(enum_GeneradoPor.eGP_RENDICION)), gUtilSQL.fSimpleSqlValue(gProyReglasDeContabilizacion.GetCuentaCajaChicaGasto), "'0'"), _
   False, "", numeroReferencia, "Proveedores Caja Chica N° " & numeroReferencia, CondicionDeStatusAnulado, "") & ", "
   
   Sql = Sql & fAgregaUnaCuentaAlSQL("DEBE", valEsUnReverso, _
   "SUM(" & _
      gUtilSQL.getIIF("Adm.DetalleDeRendicion.GeneradaPor = " & gUtilSQL.fSimpleSqlValue(gConvert.enumerativoAChar(enum_GeneradoPor.eGP_USUARIO)) _
      , "((Adm.DetalleDeRendicion.MontoExento + Adm.DetalleDeRendicion.MontoGravable - Adm.DetalleDeRendicion.MontoRetencion) * " & SqlTasaDeCambio & ")" _
      , "0", True) & ")", _
   gUtilSQL.getIIF(gUtilSQL.fMax("Adm.DetalleDeRendicion.GeneradaPor", "") & " = " & gUtilSQL.fSimpleSqlValue(gConvert.enumerativoAChar(enum_GeneradoPor.eGP_USUARIO)), gUtilSQL.fSimpleSqlValue(gProyReglasDeContabilizacion.GetCuentaPagosCxPProveedores), "'0'"), _
   False, "", numeroReferencia, "Proveedores de CxP por Caja Chica N° " & numeroReferencia, CondicionDeStatusAnulado, "") & ", "
   Sql = Sql & fAgregaUnaCuentaAlSQL("DEBE", valEsUnReverso, "((Adm.Rendicion.TotalIva) * " & SqlTasaDeCambio & ")", gProyReglasDeContabilizacion.GetCuentaIva1Debito, False, "", numeroReferencia, "Iva Caja Chica N° " & numeroReferencia, CondicionDeStatusAnulado, "") & ", "
   sqlCuentaContableCajaChica = gUtilSQL.getIIF(gUtilSQL.fFirst(gUtilSQL.getIIF("CrossCuentaContableCajaChica.CuentaContableCajaChica Is Null", gUtilSQL.fSimpleSqlValue(""), "CrossCuentaContableCajaChica.CuentaContableCajaChica"), "NONE") & " <> ''", _
                        "CrossCuentaContableCajaChica.CuentaContableCajaChica", _
                        "'" & gProyReglasDeContabilizacion.GetCuentaCajaChicaBancoHaber & "'")
   Sql = Sql & fAgregaUnaCuentaAlSQL("HABER", valEsUnReverso, "((Adm.Rendicion.TotalGastos) * " & SqlTasaDeCambio & ")", sqlCuentaContableCajaChica, False, "", numeroReferencia, "Total Caja Chica N° " & numeroReferencia, CondicionDeStatusAnulado, "") & ", "
   sqlCuentaContableCajaChica = gUtilSQL.getIIF(gUtilSQL.fFirst(gUtilSQL.getIIF("CrossCuentaContableCajaChica.CuentaContableCajaChica Is Null", gUtilSQL.fSimpleSqlValue(""), "CrossCuentaContableCajaChica.CuentaContableCajaChica"), "NONE") & " <> ''", _
                        "CrossCuentaContableCajaChica.CuentaContableCajaChica", _
                        "'" & gProyReglasDeContabilizacion.GetCuentaCajaChicaBancoDebe & "'")
   Sql = Sql & fAgregaUnaCuentaAlSQL("DEBE", valEsUnReverso, "((Adm.Rendicion.TotalGastos) * " & SqlTasaDeCambio & ")", sqlCuentaContableCajaChica, False, "", numeroReferencia, valDescripcionDelAsiento, CondicionDeStatusAnulado, "") & ", "
   sqlCuentaContableReposicion = gUtilSQL.getIIF(gUtilSQL.fFirst(gUtilSQL.getIIF("CrossCuentaContableRendicion.CuentaContableRendicion Is Null", gUtilSQL.fSimpleSqlValue(""), "CrossCuentaContableRendicion.CuentaContableRendicion"), "NONE") & " <> ''", _
                        "CrossCuentaContableRendicion.CuentaContableRendicion", _
                        "'" & gProyReglasDeContabilizacion.GetCuentaCajaChicaBanco & "'")
   Sql = Sql & fAgregaUnaCuentaAlSQL("HABER", valEsUnReverso, "((Adm.Rendicion.TotalGastos) * " & SqlTasaDeCambio & ")", sqlCuentaContableReposicion, False, "", numeroReferencia, valDescripcionDelAsiento, CondicionDeStatusAnulado, "")
   
   If varGeneraITF Then
      descripcionMovBancario = insLibSaW.fTituloITFEnCtaBancaria & " / Reposición " & numeroReferencia
      If insMovimiento.fAplicaReformaIGTFGO6687(valFechaDesde) Then
         porcentajeITF = insMovimiento.fBuscaAlicuotaImpTranscBancariasReformaIGTFGO6687(valTipoAlicPorContIGTF, valFechaDesde)
      Else
         porcentajeITF = insMovimiento.fBuscaAlicuotaImpTranscBancarias(valFechaDesde, True)
      End If
      sqlItf = "(((Sum(Adm.Rendicion.TotalGastos) * " & SqlTasaDeCambio & ") * " & porcentajeITF & " )/100)"
      Sql = Sql & ", "
      Sql = Sql & fAgregaUnaCuentaAlSQL("DEBE", valEsUnReverso, sqlItf, gProyReglasDeContabilizacion.GetCuentaDebitoBancarioGasto, False, "", numeroReferencia, descripcionMovBancario, CondicionDeStatusAnulado, "") & ", "
      Sql = Sql & fAgregaUnaCuentaAlSQL("HABER", valEsUnReverso, sqlItf, gProyReglasDeContabilizacion.GetCuentaDebitoBancarioBancos, False, "", numeroReferencia, descripcionMovBancario, CondicionDeStatusAnulado, "")
   End If
   
   Sql = Sql & " FROM Saw.Beneficiario INNER JOIN "
   Sql = Sql & " Adm.Rendicion ON (Saw.Beneficiario.ConsecutivoCompania =  Adm.Rendicion.ConsecutivoCompania "
   Sql = Sql & " AND Saw.Beneficiario.Consecutivo =  Adm.Rendicion.ConsecutivoBeneficiario)"
   Sql = Sql & " INNER JOIN Adm.DetalleDeRendicion ON (Adm.Rendicion.ConsecutivoCompania =  Adm.DetalleDeRendicion.ConsecutivoCompania"
   Sql = Sql & " AND Adm.Rendicion.Consecutivo = Adm.DetalleDeRendicion.ConsecutivoRendicion)"
   Sql = Sql & " INNER JOIN Proveedor ON (Adm.DetalleDeRendicion.ConsecutivoCompania = Proveedor.ConsecutivoCompania"
   Sql = Sql & " AND Proveedor.CodigoProveedor = Adm.DetalleDeRendicion.CodigoProveedor)"
   Sql = Sql & " OUTER APPLY (SELECT CuentaContable FROM Saw.CuentaBancaria WHERE Adm.Rendicion.CodigoCuentaBancaria = Saw.CuentaBancaria.Codigo"
   Sql = Sql & " AND Adm.Rendicion.ConsecutivoCompania = Saw.CuentaBancaria.ConsecutivoCompania)"
   Sql = Sql & " AS CrossCuentaContableRendicion(CuentaContableRendicion)"
   Sql = Sql & " OUTER APPLY (SELECT CuentaContable   FROM Saw.CuentaBancaria WHERE Adm.Rendicion.CodigoCtaBancariaCajaChica = Saw.CuentaBancaria.Codigo"
   Sql = Sql & " AND Adm.Rendicion.ConsecutivoCompania = Saw.CuentaBancaria.ConsecutivoCompania)"
   Sql = Sql & " AS CrossCuentaContableCajaChica(CuentaContableCajaChica)"
   Sql = Sql & " WHERE Adm.Rendicion.ConsecutivoCompania = " & gProyCompaniaActual.GetConsecutivoCompania
   Sql = Sql & " AND Adm.Rendicion.Numero IN (" & ConjuntoDeRendicionsinContabilizar & ")"
   Sql = Sql & " AND Adm.Rendicion.TipoDeDocumento = " & gUtilSQL.fSimpleSqlValue(enum_TipoDeDocumentoRendicion.eTDDR_Reposicion)
 
   Sql = Sql & " AND " & gUtilSQL.DfSQLDateValueBetween("Adm.Rendicion.FechaCierre", valFechaDesde, valFechaHasta)
   Sql = Sql & " GROUP BY Adm.Rendicion.ConsecutivoBeneficiario, Adm.Rendicion.TotalGastos, Adm.Rendicion.TotalIva, CrossCuentaContableCajaChica.CuentaContableCajaChica, CrossCuentaContableRendicion.CuentaContableRendicion"
   Set insLibSaW = Nothing
   Set insProveedor = Nothing
h_EXIT:
   fSQLCajaChicaEntreFechasSinDesglozar = Sql
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLCajaChicaEntreFechasSinDesglozar", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

'BUNIAK - 13666
Private Function fSQLCajaChicaEntreFechasDesglozado(ByVal valFechaDesde As Date, ByVal valFechaHasta As Date, _
            ByVal valEsUnReverso As Boolean, ByVal valDescripcionDelAsiento As String, _
            ByVal valContabilizarSolo1Documento As Boolean, ByVal valNoDocumentoOrigen As String, _
            ByVal valTipoDocumento As Integer, ByVal valConsecutivoDoc As Long, ByVal valTipoAlicPorContIGTF As enum_TipoAlicPorContIGTF) As String
   Dim ConjuntoDeRendicionsinContabilizar As String
   Dim DebeOhaber As String
   Dim Sql As String
   Dim SqlTasaDeCambio As String
   Dim CondicionDeStatusAnulado As String
   Dim numeroReferencia As String
   Dim insLibSaW As clsLibSAW
   Dim varcodigoProveedor As String
   Dim varTotalCajaChicaProveedor As Currency
   Dim valDescripcionDeLaFact As String
   Dim varUsaAuxiliarCajaChica As Boolean
   Dim sqlCuentaContableProv  As String
   Dim valContProveedor, valAgregarCuentaxProveedor As Integer
   Dim valAcumula As Boolean
   Dim valTotalSinIvaAcumulado As Currency
   Dim sqlCuentaContableCajaChica As String
   Dim sqlCuentaContableReposicion As String
   Dim sqlItf As String
   Dim insMovimiento As clsMovimientoBancarioNavigator
   Dim varGeneraITF As Boolean
   Dim porcentajeITF As Currency
   Dim descripcionMovBancario As String
   Dim sqlCuentaContableCxPProveedores As String
   Dim vMontoIva As Currency
   
   On Error GoTo h_ERROR
   Set insLibSaW = New clsLibSAW
   Set insMovimiento = New clsMovimientoBancarioNavigator
   Sql = ""
   SqlTasaDeCambio = 1
   vMontoIva = 0
   valAcumula = False
   CondicionDeStatusAnulado = gUtilSQL.fFirst("Adm.Rendicion.StatusRendicion", "NONE") & " = '" & gConvert.enumerativoAChar(enum_StatusRendicion.eSRd_ANULADA) & "'"
   numeroReferencia = valNoDocumentoOrigen
   If valContabilizarSolo1Documento Then
      gTexto.ReemplazaCaracteresEnElString numeroReferencia, vbTab, "-"
   End If
   ConjuntoDeRendicionsinContabilizar = fSQLConjuntoRecordsDeContabilizacion1oMasDocumentos(eCG_REPOSICION, valContabilizarSolo1Documento, valNoDocumentoOrigen, valFechaDesde, valFechaHasta, valTipoDocumento)
   If ConjuntoDeRendicionsinContabilizar = "" Then
      GoTo h_EXIT
   End If
   Sql = " SELECT Adm.Rendicion.ConsecutivoBeneficiario, "
   Dim rsCajaChica As ADODB.Recordset
   Set rsCajaChica = New ADODB.Recordset
   If gDbUtil.fOpenRecordset(rsCajaChica, fSQLDetalleRendicion(valConsecutivoDoc), Conexion) Then
      If rsCajaChica.RecordCount > 0 Then
         rsCajaChica.MoveFirst
         varTotalCajaChicaProveedor = 0
         varGeneraITF = gConvert.ConvertStringToBoolean(rsCajaChica.Fields("GeneraImpuestoBancario").value)
         While Not rsCajaChica.EOF
            If rsCajaChica.Fields("CuentaContableGastos").value <> "" Or gProyReglasDeContabilizacion.GetCuentaCajaChicaGasto <> "" Then
               varcodigoProveedor = rsCajaChica.Fields("CodigoProveedor").value
               varTotalCajaChicaProveedor = rsCajaChica.Fields("TotalRendicion").value
               valDescripcionDeLaFact = rsCajaChica.Fields("NumeroDocumento").value & " / " & rsCajaChica.Fields("NombreProveedor").value
               Dim vTipoCuentaContable As Integer
               If (rsCajaChica.Fields("GeneradaPor") = gConvert.enumerativoAChar(enum_GeneradoPor.eGP_RENDICION)) Then
                  vTipoCuentaContable = gProyReglasDeContabilizacion.eTC_ReglaProvGasto
               Else
                  vTipoCuentaContable = gProyReglasDeContabilizacion.eTC_ReglaProvCxP
                  vMontoIva = vMontoIva + rsCajaChica.Fields("MontoIVA").value
               End If
               varUsaAuxiliarCajaChica = fElProveedorManejaAuxiliar(varcodigoProveedor, eCG_REPOSICION, vTipoCuentaContable, valTipoDocumento)
                  
               sqlCuentaContableProv = gUtilSQL.getIIF("'" & rsCajaChica.Fields("GeneradaPor") & "' = '" & gConvert.enumerativoAChar(enum_GeneradoPor.eGP_RENDICION) & "'", _
                  gUtilSQL.getIIF(gUtilSQL.fFirst(gUtilSQL.fSimpleSqlValue(rsCajaChica.Fields("CuentaContableGastos").value), "NONE") & " <> ''", gUtilSQL.fFirst(gUtilSQL.fSimpleSqlValue(rsCajaChica.Fields("CuentaContableGastos").value), "NONE"), "'" & gProyReglasDeContabilizacion.GetCuentaCajaChicaGasto & "'", True), _
                  gUtilSQL.getIIF(gUtilSQL.fFirst(gUtilSQL.fSimpleSqlValue(rsCajaChica.Fields("CuentaContableCxP").value), "NONE") & " <> ''", gUtilSQL.fFirst(gUtilSQL.fSimpleSqlValue(rsCajaChica.Fields("CuentaContableCxP").value), "NONE"), "'" & gProyReglasDeContabilizacion.GetCuentaPagosCxPProveedores & "'", True))
                  
               Sql = Sql & fAgregaUnaCuentaAlSQL("DEBE", valEsUnReverso, "(" & gUtilSQL.fNumToStrSQL(rsCajaChica.Fields("TotalSinIva").value) & "*" & SqlTasaDeCambio & ")", sqlCuentaContableProv, varUsaAuxiliarCajaChica, gUtilSQL.fSimpleSqlValue(varcodigoProveedor), rsCajaChica.Fields("NumeroDocumento").value, valDescripcionDeLaFact, CondicionDeStatusAnulado, "") & ", "
            Else
               valAcumula = True
               valTotalSinIvaAcumulado = valTotalSinIvaAcumulado + rsCajaChica.Fields("TotalSinIva").value
            End If
            rsCajaChica.MoveNext
         Wend
       End If
   End If
   Set rsCajaChica = Nothing
   If valAcumula Then
      Sql = Sql & fAgregaUnaCuentaAlSQL("DEBE", valEsUnReverso, "(" & gUtilSQL.fNumToStrSQL(valTotalSinIvaAcumulado) & "*" & SqlTasaDeCambio & ")", gProyReglasDeContabilizacion.GetCuentaCajaChicaGasto, False, "", numeroReferencia, "Proveedores Caja Chica " & numeroReferencia, CondicionDeStatusAnulado, "") & ", "
   End If
   Sql = Sql & fAgregaUnaCuentaAlSQL("DEBE", valEsUnReverso, "((Sum(Adm.Rendicion.totalIva) - " & gUtilSQL.fNumToStrSQL(vMontoIva) & ") * " & SqlTasaDeCambio & ")", gProyReglasDeContabilizacion.GetCuentaIva1Debito, False, "", numeroReferencia, "Iva Caja Chica " & numeroReferencia, CondicionDeStatusAnulado, "") & ", "
   sqlCuentaContableCajaChica = gUtilSQL.getIIF(gUtilSQL.fFirst(gUtilSQL.getIIF("CrossCuentaContableCajaChica.CuentaContableCajaChica Is Null", gUtilSQL.fSimpleSqlValue(""), "CrossCuentaContableCajaChica.CuentaContableCajaChica"), "NONE") & " <> ''", _
                        "CrossCuentaContableCajaChica.CuentaContableCajaChica", _
                        "'" & gProyReglasDeContabilizacion.GetCuentaCajaChicaBancoHaber & "'")
   Sql = Sql & fAgregaUnaCuentaAlSQL("HABER", valEsUnReverso, "(Sum(Adm.Rendicion.TotalGastos) * " & SqlTasaDeCambio & ")", sqlCuentaContableCajaChica, False, "", numeroReferencia, "Total Caja Chica " & numeroReferencia, CondicionDeStatusAnulado, "") & ", "
   sqlCuentaContableCajaChica = gUtilSQL.getIIF(gUtilSQL.fFirst(gUtilSQL.getIIF("CrossCuentaContableCajaChica.CuentaContableCajaChica Is Null", gUtilSQL.fSimpleSqlValue(""), "CrossCuentaContableCajaChica.CuentaContableCajaChica"), "NONE") & " <> ''", _
                        "CrossCuentaContableCajaChica.CuentaContableCajaChica", _
                        "'" & gProyReglasDeContabilizacion.GetCuentaCajaChicaBancoDebe & "'")
   Sql = Sql & fAgregaUnaCuentaAlSQL("DEBE", valEsUnReverso, "(Sum(Adm.Rendicion.TotalGastos) * " & SqlTasaDeCambio & ")", sqlCuentaContableCajaChica, False, "", numeroReferencia, "Reposición " & numeroReferencia, CondicionDeStatusAnulado, "") & ", "
   sqlCuentaContableReposicion = gUtilSQL.getIIF(gUtilSQL.fFirst(gUtilSQL.getIIF("CrossCuentaContableRendicion.CuentaContableRendicion Is Null", gUtilSQL.fSimpleSqlValue(""), "CrossCuentaContableRendicion.CuentaContableRendicion"), "NONE") & " <> ''", _
                        "CrossCuentaContableRendicion.CuentaContableRendicion", _
                        "'" & gProyReglasDeContabilizacion.GetCuentaCajaChicaBanco & "'")
   Sql = Sql & fAgregaUnaCuentaAlSQL("HABER", valEsUnReverso, "(Sum(Adm.Rendicion.TotalGastos) * " & SqlTasaDeCambio & ")", sqlCuentaContableReposicion, False, "", numeroReferencia, "Reposición " & numeroReferencia, CondicionDeStatusAnulado, "")
   If varGeneraITF Then
      descripcionMovBancario = insLibSaW.fTituloITFEnCtaBancaria & " / Reposición " & numeroReferencia
      If insMovimiento.fAplicaReformaIGTFGO6687(valFechaDesde) Then
         porcentajeITF = insMovimiento.fBuscaAlicuotaImpTranscBancariasReformaIGTFGO6687(valTipoAlicPorContIGTF, valFechaDesde)
      Else
         porcentajeITF = insMovimiento.fBuscaAlicuotaImpTranscBancarias(valFechaDesde, True)
      End If
      sqlItf = "(((Sum(Adm.Rendicion.TotalGastos) * " & SqlTasaDeCambio & ") * " & gUtilSQL.fNumToStrSQL(porcentajeITF) & " )/100)"
      Sql = Sql & ", "
      Sql = Sql & fAgregaUnaCuentaAlSQL("DEBE", valEsUnReverso, sqlItf, gProyReglasDeContabilizacion.GetCuentaDebitoBancarioGasto, False, "", numeroReferencia, descripcionMovBancario, CondicionDeStatusAnulado, "") & ", "
      
      Sql = Sql & fAgregaUnaCuentaAlSQL("HABER", valEsUnReverso, sqlItf, gUtilSQL.getIIF("CrossCuentaContableBanco.CuentaContableBanco = " & gUtilSQL.fSimpleSqlValue(""), gUtilSQL.fSimpleSqlValue(gProyReglasDeContabilizacion.GetCuentaDebitoBancarioBancos), "CrossCuentaContableBanco.CuentaContableBanco"), False, "", numeroReferencia, descripcionMovBancario, CondicionDeStatusAnulado, "")
   
   End If
   Sql = Sql & " FROM Saw.Beneficiario INNER JOIN "
   Sql = Sql & " Adm.Rendicion ON (Saw.Beneficiario.ConsecutivoCompania =  Adm.Rendicion.ConsecutivoCompania "
   Sql = Sql & " AND Saw.Beneficiario.Consecutivo =  Adm.Rendicion.ConsecutivoBeneficiario)"
   Sql = Sql & " OUTER APPLY (SELECT CuentaContable FROM Saw.CuentaBancaria WHERE Adm.Rendicion.CodigoCuentaBancaria = Saw.CuentaBancaria.Codigo"
   Sql = Sql & " AND Adm.Rendicion.ConsecutivoCompania = Saw.CuentaBancaria.ConsecutivoCompania)"
   Sql = Sql & " AS CrossCuentaContableRendicion(CuentaContableRendicion)"
   Sql = Sql & " OUTER APPLY (SELECT CuentaContable   FROM Saw.CuentaBancaria WHERE Adm.Rendicion.CodigoCtaBancariaCajaChica = Saw.CuentaBancaria.Codigo"
   Sql = Sql & " AND Adm.Rendicion.ConsecutivoCompania = Saw.CuentaBancaria.ConsecutivoCompania)"
   Sql = Sql & " AS CrossCuentaContableCajaChica(CuentaContableCajaChica)"
   
   Sql = Sql & " OUTER APPLY (SELECT CuentaContable"
   Sql = Sql & " FROM Saw.CuentaBancaria"
   Sql = Sql & " WHERE Adm.Rendicion.codigoCuentaBancaria = Saw.CuentaBancaria.codigo"
   Sql = Sql & " AND Adm.Rendicion.ConsecutivoCompania = Saw.CuentaBancaria.ConsecutivoCompania)"
   Sql = Sql & " AS CrossCuentaContableBanco(CuentaContableBanco)"
   
   Sql = Sql & " WHERE Adm.Rendicion.ConsecutivoCompania = " & gProyCompaniaActual.GetConsecutivoCompania
   Sql = Sql & " AND Adm.Rendicion.Numero IN (" & ConjuntoDeRendicionsinContabilizar & ")"
   Sql = Sql & " AND Adm.Rendicion.TipoDeDocumento = " & gUtilSQL.fSimpleSqlValue(enum_TipoDeDocumentoRendicion.eTDDR_Reposicion)
   Sql = Sql & " AND " & gUtilSQL.DfSQLDateValueBetween("Adm.Rendicion.FechaCierre", valFechaDesde, valFechaHasta)
   
   Sql = Sql & " GROUP BY Adm.Rendicion.ConsecutivoBeneficiario, CrossCuentaContableCajaChica.CuentaContableCajaChica, CrossCuentaContableRendicion.CuentaContableRendicion, CrossCuentaContableBanco.CuentaContableBanco"
   
   Sql = Sql
   Set insLibSaW = Nothing
h_EXIT:
   fSQLCajaChicaEntreFechasDesglozado = Sql
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLCajaChicaEntreFechasDesglozado", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Private Function fSQLDetalleRendicion(ByVal valConsecutivoRendicion As Long) As String
   Dim Sql As String
   Dim vMontoTotal As Currency
   On Error GoTo h_ERROR
   Sql = "SELECT Adm.DetalleDeRendicion.ConsecutivoRendicion,  Adm.Rendicion.Numero, Adm.DetalleDeRendicion.CodigoProveedor, Adm.Rendicion.GeneraImpuestoBancario"
   
   Sql = Sql & " , " & gUtilSQL.getIIF("Adm.DetalleDeRendicion.GeneradaPor = " & gUtilSQL.fSimpleSqlValue(gConvert.enumerativoAChar(enum_GeneradoPor.eGP_RENDICION)), _
     "(Adm.DetalleDeRendicion.MontoExento+Adm.DetalleDeRendicion.MontoGravable + Adm.DetalleDeRendicion.MontoGravableAlicuotaEspecial1 + Adm.DetalleDeRendicion.MontoGravableAlicuotaEspecial2 + Adm.DetalleDeRendicion.MontoIva + Adm.DetalleDeRendicion.MontoIVAAlicuotaEspecial1 + Adm.DetalleDeRendicion.MontoIVAAlicuotaEspecial2)", _
     " (Adm.DetalleDeRendicion.MontoExento+Adm.DetalleDeRendicion.MontoGravable + Adm.DetalleDeRendicion.MontoGravableAlicuotaEspecial1 + Adm.DetalleDeRendicion.MontoGravableAlicuotaEspecial2 + Adm.DetalleDeRendicion.MontoIva + Adm.DetalleDeRendicion.MontoIVAAlicuotaEspecial1 + Adm.DetalleDeRendicion.MontoIVAAlicuotaEspecial2 - Adm.DetalleDeRendicion.MontoRetencion)", True)
   Sql = Sql & " AS TotalRendicion "
   
   
   Sql = Sql & " , " & gUtilSQL.getIIF("Adm.DetalleDeRendicion.GeneradaPor = " & gUtilSQL.fSimpleSqlValue(gConvert.enumerativoAChar(enum_GeneradoPor.eGP_RENDICION)), _
       "(Adm.DetalleDeRendicion.MontoExento+Adm.DetalleDeRendicion.MontoGravable+ Adm.DetalleDeRendicion.MontoGravableAlicuotaEspecial1 + Adm.DetalleDeRendicion.MontoGravableAlicuotaEspecial2-Adm.DetalleDeRendicion.MontoRetencion)", _
       " (Adm.DetalleDeRendicion.MontoExento + Adm.DetalleDeRendicion.MontoGravable + Adm.DetalleDeRendicion.MontoGravableAlicuotaEspecial1 + Adm.DetalleDeRendicion.MontoGravableAlicuotaEspecial2 - Adm.DetalleDeRendicion.MontoRetencion)", True)
   Sql = Sql & "  AS TotalSinIva"
   Sql = Sql & " , Adm.DetalleDeRendicion.NumeroDocumento, Adm.DetalleDeRendicion.MontoIVA +Adm.DetalleDeRendicion.MontoIVAAlicuotaEspecial1+Adm.DetalleDeRendicion.MontoIVAAlicuotaEspecial2 AS MontoIVA, proveedor.NombreProveedor, Proveedor.CuentaContableGastos, Proveedor.CuentaContableCxP, Adm.DetalleDeRendicion.GeneradaPor "
   Sql = Sql & " FROM Adm.Rendicion INNER JOIN"
   Sql = Sql & " Adm.DetalleDeRendicion  ON (Adm.Rendicion.ConsecutivoCompania = Adm.DetalleDeRendicion.ConsecutivoCompania"
   Sql = Sql & " AND Adm.Rendicion.Consecutivo = Adm.DetalleDeRendicion.ConsecutivoRendicion)"
   Sql = Sql & " INNER JOIN proveedor ON ( Adm.DetalleDeRendicion.consecutivocompania = proveedor.consecutivocompania "
   Sql = Sql & " AND Adm.DetalleDeRendicion.codigoproveedor = proveedor.codigoproveedor) "
   Sql = Sql & " WHERE Adm.DetalleDeRendicion.ConsecutivoRendicion = " & valConsecutivoRendicion
   Sql = Sql & " AND StatusRendicion = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_CANCELADO)
   Sql = Sql & " AND Adm.DetalleDeRendicion.ConsecutivoCompania = " & gProyCompaniaActual.GetConsecutivoCompania
   Sql = Sql & " ORDER BY CodigoProveedor"
   fSQLDetalleRendicion = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLDetalleRendicion", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fCuentasContablesDeCuentaBancariaSonValidas(ByVal valCuentaContableAsignadaEnCuenta As String, ByVal valFechaDelMovimiento As Date, valManejaDebitoBancario As Boolean, ByRef refNoEsValidaCtaContableAsignadaEnCuentaBancaria As Boolean) As Boolean
   Dim insCtaContable As clsCuentaNavigator
   Dim vResult As Boolean
   Dim vEsValidaCtaContableAsignadaEnCtaBancaria As Boolean
   Dim vCuentaMovBancarioBancosHaber As String, vCuentaMovBancarioBancosDebe As String, vCuentaMovBancarioBancosIngresos As String, vCuentaMovBancarioBancosGasto As String
   Dim vCuentaDebitoBancarioGasto As String, vCuentaDebitoBancarioBancos As String, vCuentaCreditoBancarioGasto As String, vCuentaCreditoBancarioBancos As String
   Dim vEsValidaCtaContableAsignadaEnHaber As Boolean
   Dim vEsValidaCtaContableAsignadaEnDebe As Boolean
   Dim vEsValidaCtaContableAsignadaEnIngresos As Boolean
   Dim vEsValidaCtaContableAsignadaEnGasto As Boolean
   Dim vEsValidaCtaContableAsignadaEnDebitoFiscalGasto As Boolean
   Dim vEsValidaCtaContableAsignadaEnDebitoFiscalBanco As Boolean
   Dim vEsValidaCtaContableAsignadaEnCreditoFiscalGasto As Boolean
   Dim vEsValidaCtaContableAsignadaEnCreditoFiscalBanco As Boolean
   Dim vConsecutivoPeriodo As Long
   Dim vPrefijo As String, vSufijo As String
   On Error GoTo h_ERROR
   If gProyCompaniaActual.GetUsaModuloDeContabilidad Then
      vResult = False
      vEsValidaCtaContableAsignadaEnCtaBancaria = False
      vConsecutivoPeriodo = fGetPeriodoByFechaMovimientoBancario(valFechaDelMovimiento)
      vPrefijo = "La Cuenta Contable Asignada en "
      ' Valida Cuenta Contable Asignada en Cuenta Bancaria
      If fCuentaContableExisteParaElPeriodo(valCuentaContableAsignadaEnCuenta, vConsecutivoPeriodo) Then
         vEsValidaCtaContableAsignadaEnCtaBancaria = True
      Else
         vSufijo = "el módulo de Cuenta Bancaria no existe para el período según la fecha del movimiento."
         gMessage.Advertencia (vPrefijo & vSufijo)
      End If
      sBuscaCuentaContableDeCtaBancatriaAsignadasEnReglas vCuentaMovBancarioBancosHaber, vCuentaMovBancarioBancosDebe, vCuentaMovBancarioBancosIngresos, vCuentaMovBancarioBancosGasto, vCuentaDebitoBancarioGasto, vCuentaDebitoBancarioBancos, vCuentaCreditoBancarioGasto, vCuentaCreditoBancarioBancos
      ' Valida Cuenta Contable Asignada por el Haber
      If fCuentaContableExisteParaElPeriodo(vCuentaMovBancarioBancosHaber, vConsecutivoPeriodo) Then
         vEsValidaCtaContableAsignadaEnHaber = True
      Else
         vSufijo = "Reglas Contables para el HABER no existe para el período según la fecha del movimiento. Se cancelará el proceso de generación del Comprobante Contable."
         gMessage.Advertencia (vPrefijo & vSufijo)
      End If
      ' Valida Cuenta Contable Asignada por el Debe
      If fCuentaContableExisteParaElPeriodo(vCuentaMovBancarioBancosDebe, vConsecutivoPeriodo) Then
         vEsValidaCtaContableAsignadaEnDebe = True
      Else
         vSufijo = "Reglas Contables para el DEBE no existe para el período según la fecha del movimiento. Se cancelará el proceso de generación del Comprobante Contable."
         gMessage.Advertencia (vPrefijo & vSufijo)
      End If
      ' Valida Cuenta Contable Asignada por el Ingresos
      If fCuentaContableExisteParaElPeriodo(vCuentaMovBancarioBancosIngresos, vConsecutivoPeriodo) Then
         vEsValidaCtaContableAsignadaEnIngresos = True
      Else
         vSufijo = "Reglas Contables para INGRESOS no existe para el período según la fecha del movimiento. Se cancelará el proceso de generación del Comprobante Contable."
         gMessage.Advertencia (vPrefijo & vSufijo)
      End If
      ' Valida Cuenta Contable Asignada por el Gasto
      If fCuentaContableExisteParaElPeriodo(vCuentaMovBancarioBancosGasto, vConsecutivoPeriodo) Then
         vEsValidaCtaContableAsignadaEnGasto = True
      Else
         vSufijo = "Reglas Contables para GASTOS no existe para el período según la fecha del movimiento. Se cancelará el proceso de generación del Comprobante Contable."
         gMessage.Advertencia (vPrefijo & vSufijo)
      End If
      ' Valida Cuenta Contable Asignada por el Débito Bancario
      If valManejaDebitoBancario Then
         If fCuentaContableExisteParaElPeriodo(vCuentaDebitoBancarioGasto, vConsecutivoPeriodo) Then
            vEsValidaCtaContableAsignadaEnDebitoFiscalGasto = True
         Else
           vSufijo = "Reglas Contables para ITF en la Cuenta de Débito para GASTOS no existe para el período según la fecha del movimiento. Se cancelará el proceso de generación del Comprobante Contable."
            gMessage.Advertencia (vPrefijo & vSufijo)
         End If
         If fCuentaContableExisteParaElPeriodo(vCuentaDebitoBancarioBancos, vConsecutivoPeriodo) Then
            vEsValidaCtaContableAsignadaEnDebitoFiscalBanco = True
         Else
           vSufijo = "Reglas Contables para ITF en la Cuenta de Débito para BANCO no existe para el período según la fecha del movimiento. Se cancelará el proceso de generación del Comprobante Contable."
            gMessage.Advertencia (vPrefijo & vSufijo)
         End If
'         If fCuentaContableExisteParaElPeriodo(vCuentaCreditoBancarioGasto, vConsecutivoPeriodo) Then
'            vEsValidaCtaContableAsignadaEnCreditoFiscalGasto = True
'         Else
'           vSufijo = "Reglas Contables para ITF en la Cuenta de Crédito para GASTOS no existe para el período según la fecha del movimiento. Se cancelará el proceso de generación del Comprobante Contable."
'            gMessage.Advertencia (vPrefijo & vSufijo)
'         End If
'         If fCuentaContableExisteParaElPeriodo(vCuentaCreditoBancarioBancos, vConsecutivoPeriodo) Then
'            vEsValidaCtaContableAsignadaEnCreditoFiscalBanco = True
'         Else
'           vSufijo = "Reglas Contables para ITF en la Cuenta de Crédito para BANCO no existe para el período según la fecha del movimiento. Se cancelará el proceso de generación del Comprobante Contable."
'            gMessage.Advertencia (vPrefijo & vSufijo)
'         End If
         vResult = vEsValidaCtaContableAsignadaEnDebe And vEsValidaCtaContableAsignadaEnGasto And vEsValidaCtaContableAsignadaEnHaber And vEsValidaCtaContableAsignadaEnIngresos And vEsValidaCtaContableAsignadaEnDebitoFiscalGasto And vEsValidaCtaContableAsignadaEnDebitoFiscalBanco And vEsValidaCtaContableAsignadaEnCreditoFiscalGasto And vEsValidaCtaContableAsignadaEnCreditoFiscalBanco
      Else
         vResult = vEsValidaCtaContableAsignadaEnDebe And vEsValidaCtaContableAsignadaEnGasto And vEsValidaCtaContableAsignadaEnHaber And vEsValidaCtaContableAsignadaEnIngresos
      End If
      refNoEsValidaCtaContableAsignadaEnCuentaBancaria = vEsValidaCtaContableAsignadaEnCtaBancaria
   End If
h_EXIT: On Error GoTo 0
   fCuentasContablesDeCuentaBancariaSonValidas = vResult
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCuentasContablesDeCuentaBancariaSonValidas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fCuentaContableExisteParaElPeriodo(ByVal valCodigoCtaContable As String, ByVal valConsecutivoPeriodo As Long) As Boolean
   Dim vResult As Boolean
   Dim vSQL As String
   Dim rsCuentaContable As ADODB.Recordset
   On Error GoTo h_ERROR
   vResult = False
   vSQL = "SELECT Codigo"
   vSQL = vSQL & " FROM CUENTA"
   vSQL = vSQL & " WHERE CUENTA.ConsecutivoPeriodo = " & valConsecutivoPeriodo
   vSQL = vSQL & " AND CUENTA.Codigo = " & gUtilSQL.fSimpleSqlValue(valCodigoCtaContable)
   Set rsCuentaContable = New ADODB.Recordset
   If gDbUtil.fOpenRecordset(rsCuentaContable, vSQL, Conexion) Then
      If gDbUtil.fRecordCount(rsCuentaContable) > 0 Then
         vResult = True
      End If
   End If
   gDbUtil.sDestroyRecordSet rsCuentaContable
   Set rsCuentaContable = Nothing
h_EXIT: On Error GoTo 0
   fCuentaContableExisteParaElPeriodo = vResult
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCuentaContableExisteParaElPeriodo", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Sub sBuscaCuentaContableDeCtaBancatriaAsignadasEnReglas(ByRef refCuentaMovBancarioBancosHaber As String, ByRef refCuentaMovBancarioBancosDebe As String, _
                                                                ByRef refCuentaMovBancarioBancosIngresos As String, ByRef refCuentaMovBancarioBancosGasto As String, _
                                                                ByRef refCuentaDebitoBancarioGasto As String, ByRef refCuentaDebitoBancarioBancos As String, _
                                                                ByRef refCuentaCreditoBancarioGasto As String, ByRef refCuentaCreditoBancarioBancos As String)
   Dim vSQL As String
   Dim rsCuentasEnReglas As ADODB.Recordset
   On Error GoTo h_ERROR
   vSQL = "SELECT CuentaMovBancarioBancosHaber"
   vSQL = vSQL & ", CuentaMovBancarioBancosDebe"
   vSQL = vSQL & ", CuentaMovBancarioIngresos "
   vSQL = vSQL & ", CuentaMovBancarioGasto "
   vSQL = vSQL & ", CuentaDebitoBancarioGasto "
   vSQL = vSQL & ", CuentaDebitoBancarioBancos"
   vSQL = vSQL & ", CuentaCreditoBancarioGasto"
   vSQL = vSQL & ", CuentaCreditoBancarioBancos"
   vSQL = vSQL & " FROM SAW.ReglasDeContabilizacion"
   vSQL = vSQL & " WHERE SAW.ReglasDeContabilizacion.ConsecutivoCompania = " & gProyCompaniaActual.GetConsecutivoCompania
   Set rsCuentasEnReglas = New ADODB.Recordset
   If gDbUtil.fOpenRecordset(rsCuentasEnReglas, vSQL, Conexion) Then
      If gDbUtil.fRecordCount(rsCuentasEnReglas) > 0 Then
         refCuentaMovBancarioBancosHaber = rsCuentasEnReglas.Fields("CuentaMovBancarioBancosHaber").value
         refCuentaMovBancarioBancosDebe = rsCuentasEnReglas.Fields("CuentaMovBancarioBancosDebe").value
         refCuentaMovBancarioBancosIngresos = rsCuentasEnReglas.Fields("CuentaMovBancarioIngresos").value
         refCuentaMovBancarioBancosGasto = rsCuentasEnReglas.Fields("CuentaMovBancarioGasto").value
         refCuentaDebitoBancarioGasto = rsCuentasEnReglas.Fields("CuentaDebitoBancarioGasto").value
         refCuentaDebitoBancarioBancos = rsCuentasEnReglas.Fields("CuentaDebitoBancarioBancos").value
         refCuentaCreditoBancarioGasto = rsCuentasEnReglas.Fields("CuentaCreditoBancarioGasto").value
         refCuentaCreditoBancarioBancos = rsCuentasEnReglas.Fields("CuentaCreditoBancarioBancos").value
      End If
   End If
   gDbUtil.sDestroyRecordSet rsCuentasEnReglas
   Set rsCuentasEnReglas = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sBuscaCuentaContableDeCtaBancatriaAsignadasEnReglas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Function fGetPeriodoByFechaMovimientoBancario(ByVal valFecha As Date) As Long
   Dim vConsecutivoPeriodo As Long
   Dim rsPeriodo As ADODB.Recordset
   Dim vSQL As String
   On Error GoTo h_ERROR
   vSQL = "SELECT ConsecutivoPeriodo FROM PERIODO WHERE ConsecutivoCompania = " & gProyCompaniaActual.GetConsecutivoCompania & " AND " & gUtilSQL.fDateToSQLValue(valFecha) & " BETWEEN FechaAperturaDelPeriodo AND FechaCierreDelPeriodo"
   gDbUtil.sCloseIfOpened rsPeriodo
   Set rsPeriodo = New ADODB.Recordset
   If gDbUtil.fOpenRecordSetAllParameters(rsPeriodo, vSQL, gDefDatabase.Conexion, adLockOptimistic, adUseClient, adOpenStatic) Then
      If gDbUtil.fRecordCount(rsPeriodo) > 0 Then
         vConsecutivoPeriodo = rsPeriodo.Fields("ConsecutivoPeriodo").value
      End If
   End If
   gDbUtil.sDestroyRecordSet rsPeriodo
h_EXIT:
   fGetPeriodoByFechaMovimientoBancario = vConsecutivoPeriodo
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fGetPeriodoByFechaMovimientoBancario", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLCobranzaIvaVentaDiferida(ByVal valNumeroCobranza As String) As String
   Dim vCobranza As clsCobranzaNavigator
   Dim vDocumentosCobrados As ADODB.Recordset
   Dim vNumeroCxC As String
   Dim vConjuntoCxC As String
   On Error GoTo h_ERROR
   vConjuntoCxC = ""
   Set vCobranza = New clsCobranzaNavigator
   If vCobranza.fSearchByField("Numero", valNumeroCobranza, True, True, False) Then
      Set vDocumentosCobrados = vCobranza.GetRsDetailDocumentoCobrado
      vDocumentosCobrados.MoveFirst
      While Not vDocumentosCobrados.EOF
         vNumeroCxC = vDocumentosCobrados.Fields("NumeroDelDocumentoCobrado").value
         If fBuscarFacturas(vNumeroCxC, valNumeroCobranza) Then
            If gTexto.DfLen(vConjuntoCxC) > 0 Then
               vConjuntoCxC = vConjuntoCxC & "," & gUtilSQL.fSimpleSqlValue(vNumeroCxC)
            Else
               vConjuntoCxC = gUtilSQL.fSimpleSqlValue(vNumeroCxC)
            End If
         End If
         vDocumentosCobrados.MoveNext
      Wend
      Set vCobranza = Nothing
      Set vDocumentosCobrados = Nothing
   End If
h_EXIT:
   fSQLCobranzaIvaVentaDiferida = vConjuntoCxC
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLCobranzaIvaVentaDiferida", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fBuscarFacturas(ByVal valNumeroCxC As String, valNumeroCobranza As String) As Boolean
   Dim vResult As Boolean
   Dim vCxC As clsCxCNavigator
   On Error GoTo h_ERROR
   Set vCxC = New clsCxCNavigator
   vResult = False
   If vCxC.fSearchCxCPorNumero("Numero", valNumeroCxC) Then
      If vCxC.GetTipoCxCAsEnum = eTD_FACTURA Then
         vResult = fChequearSiFacturaEsVentaDiferida(vCxC)
      End If
   End If
   Set vCxC = Nothing
h_EXIT:
   fBuscarFacturas = vResult
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fBuscarFacturas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fChequearSiFacturaEsVentaDiferida(valCxc As clsCxCNavigator) As Boolean
   Dim vResult As Boolean
   Dim vFactura As clsFacturaNavigator
   On Error GoTo h_ERROR
   vResult = False
   Set vFactura = New clsFacturaNavigator
   If vFactura.fSearchByNumeroTipoDocumento(valCxc.GetNumeroDocumentoOrigen, eTF_FACTURA, False, True, False, True) Then
      If vFactura.GetEsOriginalmenteDiferida = True And vFactura.GetSeContabilizoIvaDiferido = False Then
         vResult = True
      End If
   End If
   Set vFactura = Nothing
h_EXIT:
   fChequearSiFacturaEsVentaDiferida = vResult
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fChequearSiFacturaEsVentaDiferida", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fEjecutaProcesoPostContabilizacionDeCobranza(ByVal valConjuntoDeDocumentosContabilizados As String) As Boolean
   Dim vResultado As Boolean
   On Error GoTo h_ERROR
   If gProyParametrosCompania.GetUsarVentasConIvaDiferido Then
      sPostContabilizacionActualizarDocumentoCobrado valConjuntoDeDocumentosContabilizados
      sPostContabilizacionActualizarFactura valConjuntoDeDocumentosContabilizados
      vResultado = True
   Else
      vResultado = True
   End If
   fEjecutaProcesoPostContabilizacionDeCobranza = vResultado
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fEjecutaProcesoPostContabilizacionDeCobranza", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Sub sPostContabilizacionActualizarFactura(ByVal valConjuntoDeDocumentosContabilizados As String)
   Dim Sql As String
   Dim sqlTableToUpdate As String
   Dim sqlSetClause As String
   Dim sqlFROM As String
   Dim sqlWhere As String
   Dim insFactura As clsFacturaNavigator
   Dim insCxC As clsCxCNavigator
   Dim insDocCobrado As clsDocumentoCobradoNavigator
   Dim insCobranza As clsCobranzaNavigator
   On Error GoTo h_ERROR
   
   Set insFactura = New clsFacturaNavigator
   Set insCxC = New clsCxCNavigator
   Set insDocCobrado = New clsDocumentoCobradoNavigator
   Set insCobranza = New clsCobranzaNavigator
   
   sqlTableToUpdate = insFactura.GetTableName
   sqlSetClause = " SET " & insFactura.GetTableName & ".SeContabilizoIvaDiferido = " & gUtilSQL.fBooleanToSqlValue(True)
   sqlFROM = insFactura.GetTableName
   sqlFROM = sqlFROM & " INNER JOIN " & insCxC.GetTableName & " ON (" & insCxC.GetTableName & ".NumeroDocumentoOrigen = " & insFactura.GetTableName & ".Numero)"
   sqlFROM = sqlFROM & " AND (" & insFactura.GetTableName & ".ConsecutivoCompania = " & insCxC.GetTableName & ".ConsecutivoCompania)"
   sqlFROM = sqlFROM & " INNER JOIN " & insDocCobrado.GetTableName & " ON (" & insDocCobrado.GetTableName & ".NumeroDelDocumentoCobrado = " & insCxC.GetTableName & ".Numero)"
   sqlFROM = sqlFROM & " AND (" & insDocCobrado.GetTableName & ".ConsecutivoCompania = " & insCxC.GetTableName & ".ConsecutivoCompania)"
   sqlFROM = sqlFROM & " INNER JOIN " & insCobranza.GetTableName & " ON (" & insCobranza.GetTableName & ".Numero = " & insDocCobrado.GetTableName & ".NumeroCobranza)"
   sqlFROM = sqlFROM & " AND (" & insCobranza.GetTableName & ".ConsecutivoCompania = " & insDocCobrado.GetTableName & ".ConsecutivoCompania)"
   sqlWhere = " WHERE " & insFactura.GetTableName & ".ConsecutivoCompania = " & gProyCompaniaActual.GetConsecutivoCompania
   sqlWhere = sqlWhere & " AND " & insCobranza.GetTableName & ".Numero IN (" & valConjuntoDeDocumentosContabilizados & ")"
   sqlWhere = sqlWhere & " AND " & insFactura.GetTableName & ".EsOriginalmenteDiferida = " & gUtilSQL.fBooleanToSqlValue(True)
   sqlWhere = sqlWhere & " AND " & insFactura.GetTableName & ".SeContabilizoIvaDiferido = " & gUtilSQL.fBooleanToSqlValue(False)
   sqlWhere = sqlWhere & " AND " & insCobranza.GetTableName & ".StatusCobranza = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusCobranza.eSC_VIGENTE)
   
   Set insFactura = Nothing
   Set insCxC = Nothing
   Set insDocCobrado = Nothing
   Set insCobranza = Nothing
   
   Sql = gUtilSQL.fSQLUpdateWithNJoin(sqlTableToUpdate, sqlSetClause, sqlFROM, sqlWhere)
   gDbUtil.Execute gDefDatabase.Conexion, Sql
   
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sPostContabilizacionActualizarFactura", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sPostContabilizacionActualizarDocumentoCobrado(ByVal valConjuntoDeDocumentosContabilizados As String)
   Dim Sql As String
   Dim sqlTableToUpdate As String
   Dim sqlSetClause As String
   Dim sqlFROM As String
   Dim sqlWhere As String
   Dim insFactura As clsFacturaNavigator
   Dim insCxC As clsCxCNavigator
   Dim insDocCobrado As clsDocumentoCobradoNavigator
   Dim insCobranza As clsCobranzaNavigator
   On Error GoTo h_ERROR
   
   Set insFactura = New clsFacturaNavigator
   Set insCxC = New clsCxCNavigator
   Set insDocCobrado = New clsDocumentoCobradoNavigator
   Set insCobranza = New clsCobranzaNavigator
   
   sqlTableToUpdate = insDocCobrado.GetTableName
   sqlSetClause = " SET " & insDocCobrado.GetTableName & ".SeContabilizoIvaDiferido = " & gUtilSQL.fBooleanToSqlValue(True)
   sqlFROM = insDocCobrado.GetTableName
   sqlFROM = sqlFROM & " INNER JOIN " & insCxC.GetTableName & " ON (" & insCxC.GetTableName & ".Numero = " & insDocCobrado.GetTableName & ".NumeroDelDocumentoCobrado)"
   sqlFROM = sqlFROM & " AND (" & insCxC.GetTableName & ".ConsecutivoCompania = " & insDocCobrado.GetTableName & ".ConsecutivoCompania)"
   sqlFROM = sqlFROM & " INNER JOIN " & insFactura.GetTableName & " ON (" & insFactura.GetTableName & ".Numero = " & insCxC.GetTableName & ".NumeroDocumentoOrigen)"
   sqlFROM = sqlFROM & " AND (" & insFactura.GetTableName & ".ConsecutivoCompania = " & insCxC.GetTableName & ".ConsecutivoCompania)"
   sqlFROM = sqlFROM & " INNER JOIN " & insCobranza.GetTableName & " ON (" & insCobranza.GetTableName & ".Numero = " & insDocCobrado.GetTableName & ".NumeroCobranza)"
   sqlFROM = sqlFROM & " AND (" & insCobranza.GetTableName & ".ConsecutivoCompania = " & insDocCobrado.GetTableName & ".ConsecutivoCompania)"
   sqlWhere = " WHERE " & insDocCobrado.GetTableName & ".ConsecutivoCompania = " & gProyCompaniaActual.GetConsecutivoCompania
   sqlWhere = sqlWhere & " AND " & insCobranza.GetTableName & ".Numero IN (" & valConjuntoDeDocumentosContabilizados & ")"
   sqlWhere = sqlWhere & " AND " & insFactura.GetTableName & ".EsOriginalmenteDiferida = " & gUtilSQL.fBooleanToSqlValue(True)
   sqlWhere = sqlWhere & " AND " & insFactura.GetTableName & ".SeContabilizoIvaDiferido = " & gUtilSQL.fBooleanToSqlValue(False)
   sqlWhere = sqlWhere & " AND " & insCobranza.GetTableName & ".StatusCobranza = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusCobranza.eSC_VIGENTE)
   
   Set insFactura = Nothing
   Set insCxC = Nothing
   Set insDocCobrado = Nothing
   Set insCobranza = Nothing
   
   Sql = gUtilSQL.fSQLUpdateWithNJoin(sqlTableToUpdate, sqlSetClause, sqlFROM, sqlWhere)
   gDbUtil.Execute gDefDatabase.Conexion, Sql
   
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sPostContabilizacionActualizarDocumentoCobrado", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Sub sProcesoPostDescontabilizacionDeCobranza(ByVal valNumeroCobranza As String)
   On Error GoTo h_ERROR
      sPostDescontabilizacionActualizarFactura valNumeroCobranza
      sPostDescontabilizacionActualizarDocumentoCobrado valNumeroCobranza
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sProcesoPostDescontabilizacionDeCobranza", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sPostDescontabilizacionActualizarFactura(ByVal valNumeroCobranza As String)
   Dim Sql As String
   Dim sqlTableToUpdate As String
   Dim sqlSetClause As String
   Dim sqlFROM As String
   Dim sqlWhere As String
   Dim insFactura As clsFacturaNavigator
   Dim insCxC As clsCxCNavigator
   Dim insDocCobrado As clsDocumentoCobradoNavigator
   Dim insCobranza As clsCobranzaNavigator
   On Error GoTo h_ERROR
   
   Set insFactura = New clsFacturaNavigator
   Set insCxC = New clsCxCNavigator
   Set insDocCobrado = New clsDocumentoCobradoNavigator
   Set insCobranza = New clsCobranzaNavigator
   
   sqlTableToUpdate = insFactura.GetTableName
   sqlSetClause = " SET " & insFactura.GetTableName & ".SeContabilizoIvaDiferido = " & gUtilSQL.fBooleanToSqlValue(False)
   sqlFROM = insFactura.GetTableName
   sqlFROM = sqlFROM & " INNER JOIN " & insCxC.GetTableName & " ON (" & insCxC.GetTableName & ".NumeroDocumentoOrigen = " & insFactura.GetTableName & ".Numero)"
   sqlFROM = sqlFROM & " AND (" & insFactura.GetTableName & ".ConsecutivoCompania = " & insCxC.GetTableName & ".ConsecutivoCompania)"
   sqlFROM = sqlFROM & " INNER JOIN " & insDocCobrado.GetTableName & " ON (" & insDocCobrado.GetTableName & ".NumeroDelDocumentoCobrado = " & insCxC.GetTableName & ".Numero)"
   sqlFROM = sqlFROM & " AND (" & insDocCobrado.GetTableName & ".ConsecutivoCompania = " & insCxC.GetTableName & ".ConsecutivoCompania)"
   sqlFROM = sqlFROM & " INNER JOIN " & insCobranza.GetTableName & " ON (" & insCobranza.GetTableName & ".Numero = " & insDocCobrado.GetTableName & ".NumeroCobranza)"
   sqlFROM = sqlFROM & " AND (" & insCobranza.GetTableName & ".ConsecutivoCompania = " & insDocCobrado.GetTableName & ".ConsecutivoCompania)"
   sqlWhere = " WHERE " & insFactura.GetTableName & ".ConsecutivoCompania = " & gProyCompaniaActual.GetConsecutivoCompania
   sqlWhere = sqlWhere & " AND " & insCobranza.GetTableName & ".Numero = " & gUtilSQL.fSimpleSqlValue(valNumeroCobranza)
   sqlWhere = sqlWhere & " AND " & insFactura.GetTableName & ".EsOriginalmenteDiferida = " & gUtilSQL.fBooleanToSqlValue(True)
   sqlWhere = sqlWhere & " AND " & insFactura.GetTableName & ".SeContabilizoIvaDiferido = " & gUtilSQL.fBooleanToSqlValue(True)
   
   Set insFactura = Nothing
   Set insCxC = Nothing
   Set insDocCobrado = Nothing
   Set insCobranza = Nothing
   
   Sql = gUtilSQL.fSQLUpdateWithNJoin(sqlTableToUpdate, sqlSetClause, sqlFROM, sqlWhere)
   gDbUtil.Execute gDefDatabase.Conexion, Sql
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sPostDescontabilizacionActualizarFactura", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sPostDescontabilizacionActualizarDocumentoCobrado(ByVal valNumeroCobranza As String)
   Dim Sql As String
   Dim sqlTableToUpdate As String
   Dim sqlSetClause As String
   Dim sqlFROM As String
   Dim sqlWhere As String
   Dim insFactura As clsFacturaNavigator
   Dim insCxC As clsCxCNavigator
   Dim insDocCobrado As clsDocumentoCobradoNavigator
   Dim insCobranza As clsCobranzaNavigator
   On Error GoTo h_ERROR
   
   Set insFactura = New clsFacturaNavigator
   Set insCxC = New clsCxCNavigator
   Set insDocCobrado = New clsDocumentoCobradoNavigator
   Set insCobranza = New clsCobranzaNavigator
   
   sqlTableToUpdate = insDocCobrado.GetTableName
   sqlSetClause = " SET " & insDocCobrado.GetTableName & ".SeContabilizoIvaDiferido = " & gUtilSQL.fBooleanToSqlValue(False)
   sqlFROM = insDocCobrado.GetTableName
   sqlFROM = sqlFROM & " INNER JOIN " & insCobranza.GetTableName & " ON (" & insCobranza.GetTableName & ".Numero = " & insDocCobrado.GetTableName & ".NumeroCobranza)"
   sqlFROM = sqlFROM & " AND (" & insCobranza.GetTableName & ".ConsecutivoCompania = " & insDocCobrado.GetTableName & ".ConsecutivoCompania)"
   sqlWhere = " WHERE " & insDocCobrado.GetTableName & ".ConsecutivoCompania = " & gProyCompaniaActual.GetConsecutivoCompania
   sqlWhere = sqlWhere & " AND " & insCobranza.GetTableName & ".Numero = " & gUtilSQL.fSimpleSqlValue(valNumeroCobranza)
   sqlWhere = sqlWhere & " AND " & insDocCobrado.GetTableName & ".SeContabilizoIvaDiferido = " & gUtilSQL.fBooleanToSqlValue(True)
   
   Set insFactura = Nothing
   Set insCxC = Nothing
   Set insDocCobrado = Nothing
   Set insCobranza = Nothing
   
   Sql = gUtilSQL.fSQLUpdateWithNJoin(sqlTableToUpdate, sqlSetClause, sqlFROM, sqlWhere)
   gDbUtil.Execute gDefDatabase.Conexion, Sql
   
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sPostDescontabilizacionActualizarDocumentoCobrado", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Function fEsNotaCreditoDeFacturaConDiferimiento(ByVal valNumeroFactura As String, ByVal valTipoDocumento As Integer) As Boolean
   Dim vSQL As String
   Dim vRstFactura As ADODB.Recordset
   Dim vResult As Boolean
   Dim vTipoDocumentoFactura As enum_TipoDocumentoFactura
   On Error GoTo h_ERROR
   vResult = False
   Select Case valTipoDocumento
      Case enum_TipoDocumentoFactura.eTF_NOTADECREDITO
         vTipoDocumentoFactura = eTF_FACTURA
      Case enum_TipoDocumentoFactura.eTF_NOTADECREDITOCOMPROBANTEFISCAL
         vTipoDocumentoFactura = eTF_COMPROBANTEFISCAL
   End Select
   vSQL = "SELECT EsOriginalmenteDiferida FROM factura WHERE Numero = " & gUtilSQL.fSimpleSqlValue(valNumeroFactura)
   vSQL = vSQL & " AND TipoDeDocumento = " & gUtilSQL.fSQLSimpleValueForEnum(vTipoDocumentoFactura)
   vSQL = vSQL & " AND ConsecutivoCompania = " & gProyCompaniaActual.GetConsecutivoCompania
   Set vRstFactura = New ADODB.Recordset
   If gDbUtil.fOpenRecordset(vRstFactura, vSQL, gDefDatabase.Conexion) Then
      If gDbUtil.fRecordCount(vRstFactura) > 0 Then
         vResult = gConvert.ConvertStringToBoolean(vRstFactura.Fields("EsOriginalmenteDiferida").value)
      End If
      gDbUtil.sDestroyRecordSet vRstFactura
   End If
h_EXIT: On Error GoTo 0
   fEsNotaCreditoDeFacturaConDiferimiento = vResult
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fEsNotaCreditoDeFacturaConDiferimiento", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
