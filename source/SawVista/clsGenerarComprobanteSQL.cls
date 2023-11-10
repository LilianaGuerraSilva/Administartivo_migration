VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsGenerarComprobanteSQL"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit

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
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsGenerarComprobanteSQL", "fCambiaDebeHaberSiEsUnReverso", "Generar Comprobante", eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fAgregaUnaCuentaAlSQL(ByVal valDebeOHaber As String, ByVal valEsUnReverso As Boolean, ByVal valMontoDeLaCuenta As String, ByVal valCodigoDeLaCuenta As String, ByVal valLaCuentaUsaAuxiliares As Boolean, ByVal valCodigoDelAuxiliar As String, ByVal valNoDocumentoOrigen As String, ByVal valDescripcionDelAsiento As String, ByVal valCondicionDeStatusAnulado As String, ByVal valUsaCentroDeCostos As Boolean, ByVal valCodigoCentroDeCosto As String, ByVal valAsociarCentroDeCostos As enum_FormaDeAsociarCentroDeCostos) As String
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
   Sql = Sql & fColocaAuxiliarSiAplica(valLaCuentaUsaAuxiliares, valCodigoDelAuxiliar, valLaCuentaUsaAuxiliares)
   If valUsaCentroDeCostos And (valAsociarCentroDeCostos <> eFDACDC_NoAsociar) And (valCodigoCentroDeCosto <> "") Then
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
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsGenerarComprobanteSQL", "fAgregaUnaCuentaAlSQL", "Generar Comprobante", eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fColocaAuxiliarSiAplica(ByVal valLaCuentaUsaAuxiliares As Boolean, ByVal valCodigoDelAuxiliar As String, ByVal valUsaAuxiliares As Boolean) As String
   Dim ColocarAuxiliar As Boolean
   On Error GoTo h_ERROR
   ColocarAuxiliar = False
   If valUsaAuxiliares Then
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
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsGenerarComprobanteSQL", "fColocaAuxiliarSiAplica", "Generar Comprobante", eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

'*********
Public Function fSQLTransferenciaEntreCuentasEntreFechas(ByVal valConsecutivoCompania As Integer, ByVal valConsecutivoPeriodo As Integer, ByVal valConjuntoSinContabilizar As String, ByVal valFechaDesde As Date, ByVal valFechaHasta As Date, ByVal valEsUnReverso As Boolean, ByVal valDescripcionDelAsiento As String, ByVal valContabilizarSolo1Documento As Boolean, ByVal valNoDocumentoOrigen As String, ByVal valTipoAlicPorContIGTF As enum_TipoAlicPorContIGTF, _
      ByVal valPorcentajeIGTF As Currency, _
      ByVal valCuentaTransfCtasBancoDestino As String, _
      ByVal valCuentaTransfCtasGastoComOrigen As String, _
      ByVal valCuentaDebitoBancarioGasto As String, _
      ByVal valCuentaTransfCtasBancoOrigen As String, _
      ByVal valCuentaTransfCtasGastoComDestino As String, _
      ByVal valCuentaCreditoBancarioGasto As String) As String
   Dim vSql As String
   Dim vSqlTasaDeCambioIngreso As String
   Dim vSqlTasaDeCambioEgreso As String
   Dim vCuentaContableDebeIngreso As String
   Dim vCuentaContableHaberIngreso As String
   Dim vCuentaContableHaberEgreso As String
   Dim vMontoAsientoIngreso As String
   Dim vMontoAsientoEgreso As String
   Dim vMontoAsientoIGTFComisionIngreso As String
   Dim vMontoAsientoIGTFComisionEgreso As String
   Dim vMontoAsientoComisionIngreso As String
   Dim vMontoAsientoComisionEgreso As String
   Dim vCuentaContableDebeIGTFIngreso As String
   Dim vCuentaContableDebeComisionEgreso As String
   Dim vCuentaContableDebeIGTFEgreso As String
   Dim vCuentaContableHaberIGTFEgreso As String
   Dim vCuentaContableDebeComisionIngreso As String
   Dim vSqlStatusConsecutivo As String
   On Error GoTo h_ERROR
   If valConjuntoSinContabilizar = "" Then
      vSql = ""
      GoTo h_EXIT
   End If
   
   vSqlStatusConsecutivo = gUtilSQL.fCast("Adm.TransferenciaEntreCuentasBancarias.Status", eTDSS_VARCHAR, "")
   vSqlStatusConsecutivo = vSqlStatusConsecutivo & gUtilSQL.CharConcat & gUtilSQL.fSimpleSqlValue(gTexto.fSeparadorStandardDeElementosString) & gUtilSQL.CharConcat
   vSqlStatusConsecutivo = vSqlStatusConsecutivo & gUtilSQL.fCast("Adm.TransferenciaEntreCuentasBancarias.Consecutivo", eTDSS_VARCHAR, "")
   
   '  EL MOVIMIENTO BANCARIO ES UN INGRESO
   vSqlTasaDeCambioIngreso = "Adm.TransferenciaEntreCuentasBancarias.CambioABolivaresIngreso"
   vMontoAsientoIngreso = "SUM( " & gUtilSQL.fRoundNDecimales("Adm.TransferenciaEntreCuentasBancarias.MontoTransferenciaIngreso * " & vSqlTasaDeCambioIngreso) & ")"
   vCuentaContableDebeIngreso = gUtilSQL.getIIF("(cuenta.Codigo IS NULL OR cuenta.Codigo = '')", gUtilSQL.fSimpleSqlValue(valCuentaTransfCtasBancoDestino), "cuenta.Codigo")
   vSql = "SELECT Adm.TransferenciaEntreCuentasBancarias.CodigoConceptoIngreso, "
   vSql = vSql & fAgregaUnaCuentaAlSQL("DEBE", valEsUnReverso, vMontoAsientoIngreso, vCuentaContableDebeIngreso, False, "", valNoDocumentoOrigen, valDescripcionDelAsiento, "", False, "", -1) & " "
   vSql = vSql & " FROM Adm.TransferenciaEntreCuentasBancarias INNER JOIN cuentaBancaria ON "
   vSql = vSql & " Adm.TransferenciaEntreCuentasBancarias.ConsecutivoCompania = cuentaBancaria.ConsecutivoCompania AND "
   vSql = vSql & " Adm.TransferenciaEntreCuentasBancarias.CodigoCuentaBancariaDestino = cuentaBancaria.Codigo"
   
   vSql = vSql & " LEFT JOIN CUENTA"
   vSql = vSql & " ON cuentaBancaria.ConsecutivoCompania = cuenta.consecutivocompania"
   vSql = vSql & " AND cuentaBancaria.cuentacontable = cuenta.codigo"
   vSql = vSql & " AND cuentabancaria.consecutivocompania = " & valConsecutivoCompania
   vSql = vSql & " AND CUENTA.ConsecutivoPeriodo = " & valConsecutivoPeriodo
   
   vSql = vSql & " WHERE Adm.TransferenciaEntreCuentasBancarias.ConsecutivoCompania = " & valConsecutivoCompania
   vSql = vSql & " AND " & vSqlStatusConsecutivo & " IN (" & valConjuntoSinContabilizar & ")"
   If (valEsUnReverso) Then
      vSql = vSql & " AND (" & gUtilSQL.DfSQLDateValueBetween("Adm.TransferenciaEntreCuentasBancarias.FechaDeAnulacion", valFechaDesde, valFechaHasta) & ")"
   Else
      vSql = vSql & " AND (" & gUtilSQL.DfSQLDateValueBetween("Adm.TransferenciaEntreCuentasBancarias.Fecha", valFechaDesde, valFechaHasta) & ")"
   End If
   vSql = vSql & " GROUP BY Adm.TransferenciaEntreCuentasBancarias.CodigoConceptoIngreso, cuenta.Codigo"
   
   '  ESTAS INSTRUCCIONES SON PARA LA COMISION DE EGRESO
   vSqlTasaDeCambioEgreso = "Adm.TransferenciaEntreCuentasBancarias.CambioABolivaresEgreso"
   vMontoAsientoComisionEgreso = "SUM( " & gUtilSQL.fRoundNDecimales("Adm.TransferenciaEntreCuentasBancarias.MontoComisionEgreso * " & vSqlTasaDeCambioEgreso) & ")"
   vCuentaContableDebeComisionEgreso = valCuentaTransfCtasGastoComOrigen
   vSql = vSql & " UNION SELECT Adm.TransferenciaEntreCuentasBancarias.CodigoConceptoComisionEgreso, "
   vSql = vSql & fAgregaUnaCuentaAlSQL("DEBE", valEsUnReverso, vMontoAsientoComisionEgreso, vCuentaContableDebeComisionEgreso, False, "", valNoDocumentoOrigen, valDescripcionDelAsiento, "", False, "", -1)
   vSql = vSql & " FROM Adm.TransferenciaEntreCuentasBancarias INNER JOIN cuentaBancaria ON "
   vSql = vSql & " Adm.TransferenciaEntreCuentasBancarias.ConsecutivoCompania = cuentaBancaria.ConsecutivoCompania AND "
   vSql = vSql & " Adm.TransferenciaEntreCuentasBancarias.CodigoCuentaBancariaOrigen = cuentaBancaria.Codigo "
   
   vSql = vSql & " LEFT JOIN CUENTA"
   vSql = vSql & " ON cuentaBancaria.ConsecutivoCompania = cuenta.consecutivocompania"
   vSql = vSql & " AND cuentaBancaria.cuentacontable = cuenta.codigo"
   vSql = vSql & " AND cuentabancaria.consecutivocompania = " & valConsecutivoCompania
   vSql = vSql & " AND CUENTA.ConsecutivoPeriodo = " & valConsecutivoPeriodo
   
   vSql = vSql & " WHERE Adm.TransferenciaEntreCuentasBancarias.ConsecutivoCompania = " & valConsecutivoCompania
   vSql = vSql & " AND Adm.TransferenciaEntreCuentasBancarias.GeneraComisionEgreso = " & gUtilSQL.fBooleanToStrSQL(True)
   vSql = vSql & " AND " & vSqlStatusConsecutivo & " IN (" & valConjuntoSinContabilizar & ")"
   If (valEsUnReverso) Then
      vSql = vSql & " AND (" & gUtilSQL.DfSQLDateValueBetween("Adm.TransferenciaEntreCuentasBancarias.FechaDeAnulacion", valFechaDesde, valFechaHasta) & ")"
   Else
      vSql = vSql & " AND (" & gUtilSQL.DfSQLDateValueBetween("Adm.TransferenciaEntreCuentasBancarias.Fecha", valFechaDesde, valFechaHasta) & ")"
   End If
   vSql = vSql & " GROUP BY Adm.TransferenciaEntreCuentasBancarias.CodigoConceptoComisionEgreso, cuenta.Codigo"
   
   'ESTAS INSTRUCCIONES SON PARA IGTF DE LA COMISION DE EGRESO
   vSqlTasaDeCambioEgreso = "Adm.TransferenciaEntreCuentasBancarias.CambioABolivaresEgreso"
   vMontoAsientoIGTFComisionEgreso = gUtilSQL.fRoundNDecimales("Adm.TransferenciaEntreCuentasBancarias.MontoComisionEgreso * " & vSqlTasaDeCambioEgreso)
   vMontoAsientoIGTFComisionEgreso = gUtilSQL.fRoundNDecimales(vMontoAsientoIGTFComisionEgreso & " * " & valPorcentajeIGTF & " /100")
   vMontoAsientoIGTFComisionEgreso = "SUM(" & vMontoAsientoIGTFComisionEgreso & ")"
   vCuentaContableDebeIGTFEgreso = valCuentaDebitoBancarioGasto
   vCuentaContableHaberIGTFEgreso = gUtilSQL.getIIF(gUtilSQL.fFirst("cuentaBancaria.CuentaContable", "NONE") & " = ''", gUtilSQL.fSimpleSqlValue(valCuentaTransfCtasBancoOrigen), gUtilSQL.fFirst("cuentaBancaria.CuentaContable", "NONE"), True)
   vSql = vSql & " UNION SELECT Adm.TransferenciaEntreCuentasBancarias.CodigoConceptoComisionEgreso, "
   vSql = vSql & fAgregaUnaCuentaAlSQL("DEBE", valEsUnReverso, vMontoAsientoIGTFComisionEgreso, vCuentaContableDebeIGTFEgreso, False, "", valNoDocumentoOrigen, valDescripcionDelAsiento, "", False, "", -1)
   vSql = vSql & " FROM Adm.TransferenciaEntreCuentasBancarias INNER JOIN cuentaBancaria ON "
   vSql = vSql & " Adm.TransferenciaEntreCuentasBancarias.ConsecutivoCompania = cuentaBancaria.ConsecutivoCompania AND "
   vSql = vSql & " Adm.TransferenciaEntreCuentasBancarias.CodigoCuentaBancariaOrigen = cuentaBancaria.Codigo "
   
   vSql = vSql & " LEFT JOIN CUENTA"
   vSql = vSql & " ON cuentaBancaria.ConsecutivoCompania = cuenta.consecutivocompania"
   vSql = vSql & " AND cuentaBancaria.cuentacontable = cuenta.codigo"
   vSql = vSql & " AND cuentabancaria.consecutivocompania = " & valConsecutivoCompania
   vSql = vSql & " AND CUENTA.ConsecutivoPeriodo = " & valConsecutivoPeriodo
   
   vSql = vSql & " WHERE Adm.TransferenciaEntreCuentasBancarias.ConsecutivoCompania = " & valConsecutivoCompania
   vSql = vSql & " AND Adm.TransferenciaEntreCuentasBancarias.GeneraIGTFComisionEgreso = " & gUtilSQL.fBooleanToStrSQL(True)
   vSql = vSql & " AND " & vSqlStatusConsecutivo & " IN (" & valConjuntoSinContabilizar & ")"
   If (valEsUnReverso) Then
      vSql = vSql & " AND (" & gUtilSQL.DfSQLDateValueBetween("Adm.TransferenciaEntreCuentasBancarias.FechaDeAnulacion", valFechaDesde, valFechaHasta) & ")"
   Else
      vSql = vSql & " AND (" & gUtilSQL.DfSQLDateValueBetween("Adm.TransferenciaEntreCuentasBancarias.Fecha", valFechaDesde, valFechaHasta) & ")"
   End If
   vSql = vSql & " GROUP BY Adm.TransferenciaEntreCuentasBancarias.CodigoConceptoComisionEgreso, cuenta.Codigo"

   '  ESTAS INSTRUCCIONES SON PARA LA COMISION DE INGRESO
   vSqlTasaDeCambioIngreso = "Adm.TransferenciaEntreCuentasBancarias.CambioABolivaresIngreso"
   vMontoAsientoComisionIngreso = "SUM( " & gUtilSQL.fRoundNDecimales("Adm.TransferenciaEntreCuentasBancarias.MontoComisionIngreso * " & vSqlTasaDeCambioIngreso) & ")"
   vCuentaContableDebeComisionIngreso = valCuentaTransfCtasGastoComDestino
   vSql = vSql & " UNION SELECT Adm.TransferenciaEntreCuentasBancarias.CodigoConceptoComisionIngreso, "
   vSql = vSql & fAgregaUnaCuentaAlSQL("DEBE", valEsUnReverso, vMontoAsientoComisionIngreso, vCuentaContableDebeComisionIngreso, False, "", valNoDocumentoOrigen, valDescripcionDelAsiento, "", False, "", -1)
   vSql = vSql & " FROM Adm.TransferenciaEntreCuentasBancarias INNER JOIN cuentaBancaria ON "
   vSql = vSql & " Adm.TransferenciaEntreCuentasBancarias.ConsecutivoCompania = cuentaBancaria.ConsecutivoCompania AND "
   vSql = vSql & " Adm.TransferenciaEntreCuentasBancarias.CodigoCuentaBancariaDestino = cuentaBancaria.Codigo "
   
   vSql = vSql & " LEFT JOIN CUENTA"
   vSql = vSql & " ON cuentaBancaria.ConsecutivoCompania = cuenta.consecutivocompania"
   vSql = vSql & " AND cuentaBancaria.cuentacontable = cuenta.codigo"
   vSql = vSql & " AND cuentabancaria.consecutivocompania = " & valConsecutivoCompania
   vSql = vSql & " AND CUENTA.ConsecutivoPeriodo = " & valConsecutivoPeriodo
   
   vSql = vSql & " WHERE Adm.TransferenciaEntreCuentasBancarias.ConsecutivoCompania = " & valConsecutivoCompania
   vSql = vSql & " AND Adm.TransferenciaEntreCuentasBancarias.GeneraComisionIngreso = " & gUtilSQL.fBooleanToStrSQL(True)
   vSql = vSql & " AND " & vSqlStatusConsecutivo & " IN (" & valConjuntoSinContabilizar & ")"
   If (valEsUnReverso) Then
      vSql = vSql & " AND (" & gUtilSQL.DfSQLDateValueBetween("Adm.TransferenciaEntreCuentasBancarias.FechaDeAnulacion", valFechaDesde, valFechaHasta) & ")"
   Else
      vSql = vSql & " AND (" & gUtilSQL.DfSQLDateValueBetween("Adm.TransferenciaEntreCuentasBancarias.Fecha", valFechaDesde, valFechaHasta) & ")"
   End If
   vSql = vSql & " GROUP BY Adm.TransferenciaEntreCuentasBancarias.CodigoConceptoComisionIngreso, cuenta.Codigo"
   
   '  ESTAS INSTRUCCIONES SON PARA IGTF DE LA COMISION DE INGRESO
   vSqlTasaDeCambioIngreso = "Adm.TransferenciaEntreCuentasBancarias.CambioABolivaresIngreso"
   vMontoAsientoIGTFComisionIngreso = gUtilSQL.fRoundNDecimales("Adm.TransferenciaEntreCuentasBancarias.MontoComisionIngreso * " & vSqlTasaDeCambioIngreso)
   vMontoAsientoIGTFComisionIngreso = gUtilSQL.fRoundNDecimales(vMontoAsientoIGTFComisionIngreso & " * " & valPorcentajeIGTF & " / 100")
   vMontoAsientoIGTFComisionIngreso = "SUM(" & vMontoAsientoIGTFComisionIngreso & ")"
   vCuentaContableDebeIGTFIngreso = valCuentaCreditoBancarioGasto
   vSql = vSql & " UNION SELECT Adm.TransferenciaEntreCuentasBancarias.CodigoConceptoComisionIngreso, "
   vSql = vSql & fAgregaUnaCuentaAlSQL("DEBE", valEsUnReverso, vMontoAsientoIGTFComisionIngreso, vCuentaContableDebeIGTFIngreso, False, "", valNoDocumentoOrigen, valDescripcionDelAsiento, "", False, "", -1)
   vSql = vSql & " FROM Adm.TransferenciaEntreCuentasBancarias INNER JOIN cuentaBancaria ON "
   vSql = vSql & " Adm.TransferenciaEntreCuentasBancarias.ConsecutivoCompania = cuentaBancaria.ConsecutivoCompania AND "
   vSql = vSql & " Adm.TransferenciaEntreCuentasBancarias.CodigoCuentaBancariaDestino = cuentaBancaria.Codigo "
   
   vSql = vSql & " LEFT JOIN CUENTA"
   vSql = vSql & " ON cuentaBancaria.ConsecutivoCompania = cuenta.consecutivocompania"
   vSql = vSql & " AND cuentaBancaria.cuentacontable = cuenta.codigo"
   vSql = vSql & " AND cuentabancaria.consecutivocompania = " & valConsecutivoCompania
   vSql = vSql & " AND CUENTA.ConsecutivoPeriodo = " & valConsecutivoPeriodo
   
   vSql = vSql & " WHERE Adm.TransferenciaEntreCuentasBancarias.ConsecutivoCompania = " & valConsecutivoCompania
   vSql = vSql & " AND Adm.TransferenciaEntreCuentasBancarias.GeneraIGTFComisionIngreso = " & gUtilSQL.fBooleanToStrSQL(True)
   vSql = vSql & " AND " & vSqlStatusConsecutivo & " IN (" & valConjuntoSinContabilizar & ")"
   If (valEsUnReverso) Then
      vSql = vSql & " AND (" & gUtilSQL.DfSQLDateValueBetween("Adm.TransferenciaEntreCuentasBancarias.FechaDeAnulacion", valFechaDesde, valFechaHasta) & ")"
   Else
      vSql = vSql & " AND (" & gUtilSQL.DfSQLDateValueBetween("Adm.TransferenciaEntreCuentasBancarias.Fecha", valFechaDesde, valFechaHasta) & ")"
   End If
   vSql = vSql & " GROUP BY Adm.TransferenciaEntreCuentasBancarias.CodigoConceptoComisionIngreso, cuenta.Codigo"

   '  EL MOVIMIENTO BANCARIO ES UN EGRESO
   vSqlTasaDeCambioEgreso = "Adm.TransferenciaEntreCuentasBancarias.CambioABolivaresEgreso"
   vMontoAsientoEgreso = "SUM( " & gUtilSQL.fRoundNDecimales("Adm.TransferenciaEntreCuentasBancarias.MontoTransferenciaEgreso * " & vSqlTasaDeCambioEgreso) & ") + " & gUtilSQL.getIIF("Adm.TransferenciaEntreCuentasBancarias.GeneraIGTFComisionEgreso = " & gUtilSQL.fBooleanToStrSQL(True), vMontoAsientoIGTFComisionEgreso, "0") & " + " & gUtilSQL.getIIF("Adm.TransferenciaEntreCuentasBancarias.GeneraComisionEgreso = " & gUtilSQL.fBooleanToStrSQL(True), vMontoAsientoComisionEgreso, "0")
   vCuentaContableHaberEgreso = gUtilSQL.getIIF("(cuenta.Codigo IS NULL OR cuenta.Codigo = '')", gUtilSQL.fSimpleSqlValue(valCuentaTransfCtasBancoOrigen), "cuenta.Codigo")
   vSql = vSql & " UNION SELECT Adm.TransferenciaEntreCuentasBancarias.CodigoConceptoEgreso, "
   vSql = vSql & fAgregaUnaCuentaAlSQL("HABER", valEsUnReverso, vMontoAsientoEgreso, vCuentaContableHaberEgreso, False, "", valNoDocumentoOrigen, valDescripcionDelAsiento, "", False, "", -1)
   vSql = vSql & " FROM Adm.TransferenciaEntreCuentasBancarias INNER JOIN cuentaBancaria ON "
   vSql = vSql & " Adm.TransferenciaEntreCuentasBancarias.ConsecutivoCompania = cuentaBancaria.ConsecutivoCompania AND "
   vSql = vSql & " Adm.TransferenciaEntreCuentasBancarias.CodigoCuentaBancariaOrigen = cuentaBancaria.Codigo"
   
   vSql = vSql & " LEFT JOIN CUENTA"
   vSql = vSql & " ON cuentaBancaria.ConsecutivoCompania = cuenta.consecutivocompania"
   vSql = vSql & " AND cuentaBancaria.cuentacontable = cuenta.codigo"
   vSql = vSql & " AND cuentabancaria.consecutivocompania = " & valConsecutivoCompania
   vSql = vSql & " AND CUENTA.ConsecutivoPeriodo = " & valConsecutivoPeriodo
   
   vSql = vSql & " WHERE Adm.TransferenciaEntreCuentasBancarias.ConsecutivoCompania = " & valConsecutivoCompania
   vSql = vSql & " AND " & vSqlStatusConsecutivo & " IN (" & valConjuntoSinContabilizar & ")"
   If (valEsUnReverso) Then
      vSql = vSql & " AND (" & gUtilSQL.DfSQLDateValueBetween("Adm.TransferenciaEntreCuentasBancarias.FechaDeAnulacion", valFechaDesde, valFechaHasta) & ")"
   Else
      vSql = vSql & " AND (" & gUtilSQL.DfSQLDateValueBetween("Adm.TransferenciaEntreCuentasBancarias.Fecha", valFechaDesde, valFechaHasta) & ")"
   End If
   vSql = vSql & " GROUP BY Adm.TransferenciaEntreCuentasBancarias.CodigoConceptoEgreso, Adm.TransferenciaEntreCuentasBancarias.GeneraIGTFComisionEgreso, Adm.TransferenciaEntreCuentasBancarias.GeneraComisionEgreso, cuenta.Codigo"
   
   '  EL MOVIMIENTO BANCARIO ES UN COMISION DE INGRESO + IGTF INGRESO
   vMontoAsientoIngreso = gUtilSQL.getIIF("Adm.TransferenciaEntreCuentasBancarias.GeneraIGTFComisionIngreso = " & gUtilSQL.fBooleanToStrSQL(True), vMontoAsientoIGTFComisionIngreso, "0") & " + " & gUtilSQL.getIIF("Adm.TransferenciaEntreCuentasBancarias.GeneraComisionIngreso = " & gUtilSQL.fBooleanToStrSQL(True), vMontoAsientoComisionIngreso, "0")
   vCuentaContableHaberIngreso = gUtilSQL.getIIF("(cuenta.Codigo IS NULL OR cuenta.Codigo = '')", gUtilSQL.fSimpleSqlValue(valCuentaTransfCtasBancoDestino), "cuenta.Codigo")
   vSql = vSql & " UNION SELECT Adm.TransferenciaEntreCuentasBancarias.CodigoConceptoIngreso, "
   vSql = vSql & fAgregaUnaCuentaAlSQL("HABER", valEsUnReverso, vMontoAsientoIngreso, vCuentaContableHaberIngreso, False, "", valNoDocumentoOrigen, valDescripcionDelAsiento, "", False, "", -1)
   vSql = vSql & " FROM Adm.TransferenciaEntreCuentasBancarias INNER JOIN cuentaBancaria ON "
   vSql = vSql & " Adm.TransferenciaEntreCuentasBancarias.ConsecutivoCompania = cuentaBancaria.ConsecutivoCompania AND "
   vSql = vSql & " Adm.TransferenciaEntreCuentasBancarias.CodigoCuentaBancariaDestino = cuentaBancaria.Codigo"
   
   vSql = vSql & " LEFT JOIN CUENTA"
   vSql = vSql & " ON cuentaBancaria.ConsecutivoCompania = cuenta.consecutivocompania"
   vSql = vSql & " AND cuentaBancaria.cuentacontable = cuenta.codigo"
   vSql = vSql & " AND cuentabancaria.consecutivocompania = " & valConsecutivoCompania
   vSql = vSql & " AND CUENTA.ConsecutivoPeriodo = " & valConsecutivoPeriodo
   
   vSql = vSql & " WHERE Adm.TransferenciaEntreCuentasBancarias.ConsecutivoCompania = " & valConsecutivoCompania
   vSql = vSql & " AND " & vSqlStatusConsecutivo & " IN (" & valConjuntoSinContabilizar & ")"
   If (valEsUnReverso) Then
      vSql = vSql & " AND (" & gUtilSQL.DfSQLDateValueBetween("Adm.TransferenciaEntreCuentasBancarias.FechaDeAnulacion", valFechaDesde, valFechaHasta) & ")"
   Else
      vSql = vSql & " AND (" & gUtilSQL.DfSQLDateValueBetween("Adm.TransferenciaEntreCuentasBancarias.Fecha", valFechaDesde, valFechaHasta) & ")"
   End If
   vSql = vSql & " GROUP BY Adm.TransferenciaEntreCuentasBancarias.CodigoConceptoIngreso, Adm.TransferenciaEntreCuentasBancarias.GeneraIGTFComisionIngreso, Adm.TransferenciaEntreCuentasBancarias.GeneraComisionIngreso, cuenta.Codigo"
h_EXIT:
   fSQLTransferenciaEntreCuentasEntreFechas = vSql
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsGenerarComprobanteNavigator", "fSQLTransferenciaEntreCuentasEntreFechas", "Generar Comprobante", eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLOrdenDeProduccionEntreFechas(ByVal valConsecutivoCompania As Integer, ByVal valConjuntoSinContabilizar As String, ByVal valFechaDesde As Date, ByVal valFechaHasta As Date, ByVal valEsUnReverso As Boolean, ByVal valDescripcionDelAsiento As String, ByVal valContabilizarSolo1Documento As Boolean, ByVal valNoDocumentoOrigen As String, _
      ByVal valCuentaOrdenDeProduccionProductoTerminado As String, ByVal valCuentaOrdenDeProduccionMateriales As String) As String
   Dim vSql As String
   Dim vCCProductoTerminado As String
   Dim vCCMateriales As String
   Dim vSqlStatusConsecutivo As String
   Dim vSqlFecha As String
   Dim vSqlCambio As String
   On Error GoTo h_ERROR
   
   vSqlCambio = gUtilSQL.getIIF("OdP.CostoTerminadoCalculadoAPartirDe = " & gUtilSQL.fSQLSimpleValueForEnum(enum_FormaDeCalcularCostoTerminado.eFDCCT_APartirDeMonedaLocal), "1", "OdP.CambioCostoProduccion", True)
   vCCProductoTerminado = gUtilSQL.getIIF("(AI_Articulo.CuentaInventario = '')", gUtilSQL.fSimpleSqlValue(valCuentaOrdenDeProduccionProductoTerminado), "AI_Articulo.CuentaInventario", True)
   vCCMateriales = gUtilSQL.getIIF("(AI_Material.CuentaInventario = '')", gUtilSQL.fSimpleSqlValue(valCuentaOrdenDeProduccionMateriales), "AI_Material.CuentaInventario", True)
   
   vSqlStatusConsecutivo = gUtilSQL.fCast("OdP.StatusOp", eTDSS_VARCHAR, "")
   vSqlStatusConsecutivo = vSqlStatusConsecutivo & gUtilSQL.CharConcat & gUtilSQL.fSimpleSqlValue(gTexto.fSeparadorStandardDeElementosString) & gUtilSQL.CharConcat
   vSqlStatusConsecutivo = vSqlStatusConsecutivo & gUtilSQL.fCast("OdP.Consecutivo", eTDSS_VARCHAR, "")
      
   vSqlFecha = "OdP.FechaFinalizacion"
   
   vSql = "SELECT OdP.Consecutivo, "
   vSql = vSql & fAgregaUnaCuentaAlSQL("DEBE", valEsUnReverso, " ROUND(SUM(OdP_DetArticulo.MontoSubTotal * " & vSqlCambio & " ),2 ) ", vCCProductoTerminado, False, "", valNoDocumentoOrigen, valDescripcionDelAsiento, "", False, "", -1)
      
   vSql = vSql & " FROM Adm.OrdenDeProduccion AS OdP"
   vSql = vSql & " INNER JOIN "
   vSql = vSql & " Adm.OrdenDeProduccionDetalleArticulo AS OdP_DetArticulo ON"
   vSql = vSql & " OdP.ConsecutivoCompania = OdP_DetArticulo.ConsecutivoCompania"
   vSql = vSql & " AND OdP.Consecutivo = OdP_DetArticulo.ConsecutivoOrdenDeProduccion"
   vSql = vSql & " INNER JOIN "
   vSql = vSql & " ArticuloInventario AS AI_Articulo ON OdP_DetArticulo.CodigoArticulo = AI_Articulo.Codigo"
   vSql = vSql & " AND OdP_DetArticulo.ConsecutivoCompania = AI_Articulo.ConsecutivoCompania"
   
   vSql = vSql & " WHERE OdP.ConsecutivoCompania = " & valConsecutivoCompania
   vSql = vSql & " AND " & vSqlStatusConsecutivo & " IN (" & valConjuntoSinContabilizar & ")"
   vSql = vSql & " AND " & gUtilSQL.DfSQLDateValueBetween(vSqlFecha, valFechaDesde, valFechaHasta)
   
   vSql = vSql & " GROUP BY OdP.Consecutivo, AI_Articulo.CuentaInventario "
    
   vSql = vSql & " UNION "
   vSql = vSql & " SELECT OdP.Consecutivo, "
   vSql = vSql & fAgregaUnaCuentaAlSQL("HABER", valEsUnReverso, " ROUND(SUM(OdP_DetMateriales.MontoSubtotal * " & vSqlCambio & " ),2 ) ", vCCMateriales, False, "", valNoDocumentoOrigen, valDescripcionDelAsiento, "", False, "", -1)
   
   vSql = vSql & " FROM ArticuloInventario AS AI_Material"
   vSql = vSql & " INNER JOIN "
   vSql = vSql & " Adm.OrdenDeProduccionDetalleMateriales AS OdP_DetMateriales ON AI_Material.ConsecutivoCompania = OdP_DetMateriales.ConsecutivoCompania"
   vSql = vSql & " AND AI_Material.Codigo = OdP_DetMateriales.CodigoArticulo"
   vSql = vSql & " INNER JOIN "
   vSql = vSql & " Adm.OrdenDeProduccion AS OdP ON OdP_DetMateriales.ConsecutivoCompania = OdP.ConsecutivoCompania"
   vSql = vSql & " AND OdP_DetMateriales.ConsecutivoOrdenDeProduccion = OdP.Consecutivo"
   
   vSql = vSql & " WHERE OdP.ConsecutivoCompania = " & valConsecutivoCompania
   vSql = vSql & " AND " & vSqlStatusConsecutivo & " IN (" & valConjuntoSinContabilizar & ")"
   vSql = vSql & " AND " & gUtilSQL.DfSQLDateValueBetween(vSqlFecha, valFechaDesde, valFechaHasta)
   
   vSql = vSql & " GROUP BY OdP.Consecutivo, AI_Material.CuentaInventario "
   

h_EXIT:
   fSQLOrdenDeProduccionEntreFechas = vSql
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsGenerarComprobanteSQL", "fSQLOrdenDeProduccionEntreFechas", "Generar Comprobante", eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSqlPostDescontabilizacionActualizarDocumentoCobrado(ByVal valConsecutivoCompania As Integer, ByVal valNumeroCobranza As String) As String
   Dim vSql As String
   Dim vSqlTableToUpdate As String
   Dim vSqlSetClause As String
   Dim vSqlFROM As String
   Dim vSqlWhere As String
   On Error GoTo h_ERROR
   vSqlTableToUpdate = "documentoCobrado"
   vSqlSetClause = " SET documentoCobrado.SeContabilizoIvaDiferido = " & gUtilSQL.fBooleanToSqlValue(False)
   vSqlFROM = "documentoCobrado"
   vSqlFROM = vSqlFROM & " INNER JOIN cobranza ON (cobranza.Numero = documentoCobrado.NumeroCobranza)"
   vSqlFROM = vSqlFROM & " AND (cobranza.ConsecutivoCompania = documentoCobrado.ConsecutivoCompania)"
   vSqlWhere = " WHERE documentoCobrado.ConsecutivoCompania = " & valConsecutivoCompania
   vSqlWhere = vSqlWhere & " AND cobranza.Numero = " & gUtilSQL.fSimpleSqlValue(valNumeroCobranza)
   vSqlWhere = vSqlWhere & " AND documentoCobrado.SeContabilizoIvaDiferido = " & gUtilSQL.fBooleanToSqlValue(True)
   
   vSql = gUtilSQL.fSQLUpdateWithNJoin(vSqlTableToUpdate, vSqlSetClause, vSqlFROM, vSqlWhere)
   fSqlPostDescontabilizacionActualizarDocumentoCobrado = vSql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsGenerarComprobanteNavigator", "fSqlPostDescontabilizacionActualizarDocumentoCobrado", "Generar Comprobante", eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSqlPostDescontabilizacionActualizarFactura(ByVal valConsecutivoCompania As Integer, ByVal valNumeroCobranza As String) As String
   Dim vSql As String
   Dim vSqlTableToUpdate As String
   Dim vSqlSetClause As String
   Dim vSqlFROM As String
   Dim vSqlWhere As String
   On Error GoTo h_ERROR
   vSqlTableToUpdate = "factura"
   vSqlSetClause = " SET factura.SeContabilizoIvaDiferido = " & gUtilSQL.fBooleanToSqlValue(False)
   vSqlFROM = "factura"
   vSqlFROM = vSqlFROM & " INNER JOIN cxC ON (cxC.NumeroDocumentoOrigen = factura.Numero)"
   vSqlFROM = vSqlFROM & " AND (factura.ConsecutivoCompania = cxC.ConsecutivoCompania)"
   vSqlFROM = vSqlFROM & " INNER JOIN documentoCobrado ON (documentoCobrado.NumeroDelDocumentoCobrado = cxC.Numero)"
   vSqlFROM = vSqlFROM & " AND (documentoCobrado.ConsecutivoCompania = cxC.ConsecutivoCompania)"
   vSqlFROM = vSqlFROM & " INNER JOIN cobranza ON (cobranza.Numero = documentoCobrado.NumeroCobranza)"
   vSqlFROM = vSqlFROM & " AND (cobranza.ConsecutivoCompania = documentoCobrado.ConsecutivoCompania)"
   vSqlWhere = " WHERE factura.ConsecutivoCompania = " & valConsecutivoCompania
   vSqlWhere = vSqlWhere & " AND cobranza.Numero = " & gUtilSQL.fSimpleSqlValue(valNumeroCobranza)
   vSqlWhere = vSqlWhere & " AND factura.EsOriginalmenteDiferida = " & gUtilSQL.fBooleanToSqlValue(True)
   vSqlWhere = vSqlWhere & " AND factura.SeContabilizoIvaDiferido = " & gUtilSQL.fBooleanToSqlValue(True)
   
   vSql = gUtilSQL.fSQLUpdateWithNJoin(vSqlTableToUpdate, vSqlSetClause, vSqlFROM, vSqlWhere)
   fSqlPostDescontabilizacionActualizarFactura = vSql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsGenerarComprobanteNavigator", "fSqlPostDescontabilizacionActualizarFactura", "Generar Comprobante", eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function


