VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsCobranzaSQL"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit
Private Const CM_FILE_NAME As String = "clsCobranzaSQL"
Private Const CM_MESSAGE_NAME As String = "SQL Cobranza"
Private gEnumReport As clsEnumReport

Private Enum enum_OpcionInfCobranzaEntreFecha
   eOC_Cobrador = 0
   eOC_Cliente
   eOC_Banco
End Enum

Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function

Public Function fConstruirSqlDelReporteCobranzaPorDia(ByVal valReporteEnMonedaLocal As Boolean, ByVal valFechaInicial As Date, _
                                               ByVal valFechaFinal As Date, ByVal valConsecutivoCompania As String, _
                                               ByVal gMonedaLocalActual As Object, ByVal gUltimaTasaDeCambio As Object, _
                                               ByVal valUsarCambioOriginal As Boolean, ByVal valMostrarCobranzasPorVentasDiferidas As Boolean) As String
   Dim SQL As String
   Dim NumeroUno As Long
   Dim sqlFechaCobranza As String
   Dim sqlMontoTotal As String
   Dim sqlMaximoDocCobrado As String
   Dim sqlMoneda As String
   Dim sqlCambio As String
   Dim SQLMontoOriginal As String
   Dim sqlCambioCxC As String
   Dim sqlMonedaCxC As String
   Dim vFechaDeUltimoCambio As Date
   On Error GoTo h_ERROR
   NumeroUno = 1
   sqlMoneda = "Cobranza.moneda"
   sqlFechaCobranza = "Cobranza.Fecha"
   sqlMontoTotal = "Cobranza.TotalCobrado"
   sqlMaximoDocCobrado = "Cobranza.TotalCobrado"
   sqlCambio = "Cobranza.CambioABolivares"
   SQLMontoOriginal = "Cobranza.TotalCobrado"
   sqlCambioCxC = "Cobranza.CambioABolivares"
   sqlMonedaCxC = "Cobranza.Moneda"
      
   If valReporteEnMonedaLocal Then
      sqlMoneda = gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda)
      sqlMonedaCxC = gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda)
      If valUsarCambioOriginal Then
         sqlCambio = gMonedaLocalActual.fSQLConvierteMontoSiAplicaMasDecimales(gMonedaLocalActual.GetHoyCodigoMoneda, sqlCambio, "Cobranza.Fecha")
         sqlCambioCxC = gMonedaLocalActual.fSQLConvierteMontoSiAplicaMasDecimales(gMonedaLocalActual.GetHoyCodigoMoneda, sqlCambioCxC, "Cobranza.Fecha")
      Else
         sqlCambio = gUltimaTasaDeCambio.getUltimaTasaDeCambio("Cobranza.Moneda", False)
         sqlCambioCxC = gUltimaTasaDeCambio.getUltimaTasaDeCambio("Cobranza.Moneda", False)
      End If
      sqlMontoTotal = gUtilSQL.getIIF("Cobranza.CodigoMoneda = " & gMonedaLocalActual.fSQLCodigoMonedaALaFecha(sqlFechaCobranza), _
                     gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, sqlMontoTotal, sqlFechaCobranza), _
                     gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCambio, "Cobranza.moneda", sqlMontoTotal, valUsarCambioOriginal, ""), True)

      SQLMontoOriginal = gUtilSQL.getIIF("Cobranza.CodigoMoneda = " & gMonedaLocalActual.fSQLCodigoMonedaALaFecha(sqlFechaCobranza), _
                     gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, SQLMontoOriginal, sqlFechaCobranza), _
                     gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCambioCxC, "Cobranza.moneda", SQLMontoOriginal, valUsarCambioOriginal, ""), True)

      sqlMaximoDocCobrado = gUtilSQL.getIIF("Cobranza.CodigoMoneda = " & gMonedaLocalActual.fSQLCodigoMonedaALaFecha(sqlFechaCobranza), _
                     gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, sqlMaximoDocCobrado, sqlFechaCobranza), _
                     gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCambio, "Cobranza.Moneda", sqlMaximoDocCobrado, valUsarCambioOriginal, ""), True)
      sqlCambioCxC = 1
   End If

   SQL = SQL & "SELECT " & NumeroUno & " AS NumeroDeCobranzas, "
   SQL = SQL & sqlFechaCobranza & " AS Fecha, "
   SQL = SQL & sqlMoneda & " AS MonedaDelReporte, "
   SQL = SQL & sqlMonedaCxC & " AS MonedaDeCxC, "
   SQL = SQL & sqlCambioCxC & " As Cambio, "
   SQL = SQL & "Cobranza.CodigoMoneda, "
   SQL = SQL & sqlMontoTotal & " AS MontoTotal, "
   SQL = SQL & sqlMaximoDocCobrado & " As MaximoDocCobrado, "
   SQL = SQL & SQLMontoOriginal & " AS MontoOriginal "

   SQL = SQL & " FROM cobranza  "
   
   SQL = SQL & " WHERE " & gUtilSQL.DfSQLDateValueBetween(sqlFechaCobranza, _
                           valFechaInicial, valFechaFinal)
   SQL = SQL & " AND (Cobranza.ConsecutivoCompania =" & _
               valConsecutivoCompania & ")"
   SQL = SQL & " AND Cobranza.StatusCobranza" & _
               " = " & gUtilSQL.fSimpleSqlValue(gConvert.enumerativoAChar(enum_StatusCobranza.eSC_VIGENTE))
   SQL = SQL & " ORDER BY "
   SQL = SQL & "MonedaDelReporte, "
   SQL = SQL & "Cobranza.Fecha"
h_EXIT: On Error GoTo 0
   fConstruirSqlDelReporteCobranzaPorDia = SQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fConstruirSqlDelReporteCobranzaPorDia", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function


Public Function fSqlDeComparativoCobranzaPorAnos(ByVal valtxtAnoInicial As String, ByVal valtxtAnoFinal As String, ByVal valConsecutivoCompania As String, _
                                    ByVal gMonedaLocalActual As Object, ByVal gUltimaTasaDeCambio As Object) As String
   Dim SQL As String
   Dim i As Integer
   Dim valTotalanos As Integer
   Dim valanoincremento As Integer
   Dim valAnoInicial As Integer
   Dim valAnoFinal As Integer
   Dim sqlMontosConvertidosAMonedalocal As String
   Dim sqlCambio As String
   On Error GoTo h_ERROR
   valAnoInicial = gConvert.ConvierteAInteger(valtxtAnoInicial)
   valAnoFinal = gConvert.ConvierteAInteger(valtxtAnoFinal)
   valTotalanos = valAnoFinal - valAnoInicial
   valanoincremento = valAnoInicial
   sqlCambio = gMonedaLocalActual.fSQLConvierteMontoSiAplicaMasDecimales(gMonedaLocalActual.GetHoyCodigoMoneda, "Cobranza.CambioABolivares", "Cobranza.Fecha")
   
   sqlMontosConvertidosAMonedalocal = gUtilSQL.getIIF("Cobranza.CodigoMoneda = " & gMonedaLocalActual.fSQLCodigoMonedaALaFecha("Cobranza.Fecha"), _
                     gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "Cobranza.TotalCobrado", "Cobranza.Fecha"), _
                     gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCambio, "Cobranza.moneda", "Cobranza.TotalCobrado", True, ""), True)

   SQL = "SELECT "
   For i = 0 To valTotalanos
      SQL = SQL & gUtilSQL.getIIF(gUtilSQL.DfSQLYearOfDate("Fecha") & " = " & gConvert.fConvierteAString(valanoincremento), "SUM" & sqlMontosConvertidosAMonedalocal, "0") & " AS TotalCobradoAno" & gConvert.fConvierteAString(i) & ", "
      valanoincremento = valanoincremento + 1
   Next
   SQL = SQL & gUtilSQL.DfSQLMonthOfDate("Fecha", "Mes") & ", "
   SQL = SQL & gUtilSQL.DfSQLYearOfDate("Fecha", "Ano") & ", "
   SQL = SQL & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda) & " AS MonedaDoc "
   SQL = SQL & " FROM Cobranza "
   SQL = SQL & " WHERE  StatusCobranza = " & gUtilSQL.fSimpleSqlValue(gConvert.enumerativoAChar(enum_StatusFactura.eSF_EMITIDA))
   SQL = SQL & " AND ConsecutivoCompania = " & valConsecutivoCompania
   SQL = SQL & " AND " & gUtilSQL.fSQLValueBetween(gUtilSQL.DfSQLYearOfDate("Fecha"), gConvert.fConvertStringToLong(valtxtAnoInicial), gConvert.fConvertStringToLong(valtxtAnoFinal))
   SQL = SQL & " GROUP BY "
   SQL = SQL & gUtilSQL.DfSQLMonthOfDate("Fecha") & ", "
   SQL = SQL & gUtilSQL.DfSQLYearOfDate("Fecha")
   SQL = SQL & " ORDER BY MONTH(FECHA) "
   fSqlDeComparativoCobranzaPorAnos = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSqlDeComparativoCobranzaPorAnos", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLDelReporteCobranzaEntreFechas(ByVal valReporteEnMonedaLocal As Boolean, ByVal valUsaModuloContabilidad As Boolean, _
                                  ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valConsecutivoCompania As String, _
                                  ByVal ValStatusCobranzaToString As String, ByVal gMonedaLocalActual As Object, ByVal gUltimaTasaDeCambio As Object, _
                                  ByVal NombreTablaComprobante As String, ByVal usaCambioOriginal As Boolean, ByVal valOpcionInforme As Integer, _
                                  ByVal valAgruparInforme As Boolean, ByVal valFiltroSql As String, ByVal valMostrarCobranzasPorVentasDiferidas As Boolean) As String
   Dim SQL As String
   Dim sqlCambio As String
   Dim sqlTotalCobrado As String
   Dim sqlFechaCobranza As String
   Dim sqlMonedaCobro As String
   Dim sqlMonedaCxC As String
   On Error GoTo h_ERROR
   sqlFechaCobranza = "Cobranza.Fecha"
   sqlMonedaCobro = "Cobranza.moneda"
   If valOpcionInforme = enum_OpcionInfCobranzaEntreFecha.eOC_Banco Then
      sqlTotalCobrado = "cobranza.TotalCobrado - (cobranza.RetencionIslr + cobranza.TotalOtros + cobranza.RetencionIva + cobranza.CobradoAnticipo + cobranza.Vuelto + cobranza.DescProntoPago)"
   Else
      sqlTotalCobrado = "cobranza.TotalCobrado"
   End If
   sqlCambio = "cobranza.CambioABolivares"
   sqlMonedaCxC = "cxC.Moneda"
         
   If valReporteEnMonedaLocal Then
      If usaCambioOriginal Then
         sqlCambio = gMonedaLocalActual.fSQLConvierteMontoSiAplicaMasDecimales(gMonedaLocalActual.GetHoyCodigoMoneda, sqlCambio, "Cobranza.Fecha")
      Else
'         sqlCambio = gUltimaTasaDeCambio.getUltimaTasaDeCambio(sqlMonedaCobro, True)
         sqlCambio = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("", sqlMonedaCobro, "1", False, "")
      End If
      sqlTotalCobrado = gUtilSQL.getIIF("Cobranza.CodigoMoneda = " & gMonedaLocalActual.fSQLCodigoMonedaALaFecha(sqlFechaCobranza), _
                  gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, sqlTotalCobrado, sqlFechaCobranza), _
                  gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCambio, sqlMonedaCobro, sqlTotalCobrado, usaCambioOriginal, ""), True)
      sqlMonedaCobro = gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda)
   Else
      sqlMonedaCobro = "Cobranza.moneda"
   End If
   
   SQL = "SELECT "
   SQL = SQL & "Cobranza.Fecha, "
   SQL = SQL & "Cobranza.Numero, "
   SQL = SQL & "Cobranza.CodigoCobrador, "
   SQL = SQL & "Vendedor.Nombre AS NombreVendedor, "
   SQL = SQL & "Cobranza.CodigoCliente, "
   SQL = SQL & "Cliente.Nombre AS NombreCliente, "
   SQL = SQL & "Cobranza.CodigoMoneda, "
   SQL = SQL & sqlMonedaCobro & " AS MonedaCobro, "
   SQL = SQL & sqlCambio & " AS Cambio, "
   SQL = SQL & gUtilSQL.getIIF("Cobranza.StatusCobranza = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusCobranza.eSC_ANULADA), "0", sqlTotalCobrado, True) & " AS TotalCobrado, "
   SQL = SQL & gUtilSQL.getIIF("Cobranza.StatusCobranza = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusCobranza.eSC_ANULADA), _
               gUtilSQL.fSimpleSqlValue(ValStatusCobranzaToString), gUtilSQL.fSimpleSqlValue(""), True) & " AS Status, "
   SQL = SQL & "CuentaBancaria.NombreCuenta "
   If valUsaModuloContabilidad Then
      SQL = SQL & ", " & gUtilSQL.getIIF(fSQLGenerarCuentaContable(valConsecutivoCompania, NombreTablaComprobante, valFechaInicial, valFechaFinal, "Cobranza.Numero") & " <> " & gUtilSQL.fSimpleSqlValue(""), fSQLGenerarCuentaContable(valConsecutivoCompania, NombreTablaComprobante, valFechaInicial, valFechaFinal, "Cobranza.Numero"), _
                        gUtilSQL.fSimpleSqlValue("No Aplica"), True)
      SQL = SQL & " AS NumeroComprobante"
   End If
   SQL = SQL & " FROM vendedor INNER JOIN "
   SQL = SQL & " cliente INNER JOIN "
   SQL = SQL & " cobranza ON cliente.Codigo = cobranza.CodigoCliente "
   SQL = SQL & " AND cliente.ConsecutivoCompania = cobranza.ConsecutivoCompania "
   SQL = SQL & " ON vendedor.Codigo = cobranza.CodigoCobrador "
   SQL = SQL & " AND vendedor.ConsecutivoCompania = cobranza.ConsecutivoCompania "
   If valOpcionInforme = enum_OpcionInfCobranzaEntreFecha.eOC_Banco Then
      SQL = SQL & " INNER JOIN CuentaBancaria ON Cobranza.CodigoCuentaBancaria = CuentaBancaria.Codigo "
   Else
      SQL = SQL & " LEFT JOIN CuentaBancaria ON Cobranza.CodigoCuentaBancaria = CuentaBancaria.Codigo "
   End If
   SQL = SQL & " AND Cobranza.ConsecutivoCompania = CuentaBancaria.ConsecutivoCompania "
   SQL = SQL & fSQLMostrarCobranzasPorVentasDiferidas(valMostrarCobranzasPorVentasDiferidas)
   SQL = SQL & " WHERE " & gUtilSQL.DfSQLDateValueBetween(sqlFechaCobranza, valFechaInicial, valFechaFinal)
   SQL = SQL & " AND (Cobranza.ConsecutivoCompania = " & valConsecutivoCompania & ")"
   If valFiltroSql <> "" Then
      SQL = SQL & " AND " & fNombreCampoFiltroInformeCobranzasEntreFecha(valOpcionInforme) & " = " & gUtilSQL.fSimpleSqlValue(valFiltroSql)
   End If
   SQL = SQL & " ORDER BY MonedaCobro, "
   If valAgruparInforme Then
      SQL = SQL & fNombreCampoFiltroInformeCobranzasEntreFecha(valOpcionInforme) & ", "
      SQL = SQL & "Cobranza.Fecha, "
   Else
      SQL = SQL & "Cobranza.Fecha, "
      SQL = SQL & fNombreCampoFiltroInformeCobranzasEntreFecha(valOpcionInforme) & ", "
   End If
   SQL = SQL & "Cobranza.Numero "
   fSQLDelReporteCobranzaEntreFechas = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLDelReporteCobranzaEntreFechas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLGenerarCuentaContable(ByVal valConsecutivoCompania As String, ByVal NombreTablaComprobante As String, _
                                             ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valNumeroCobranza As String) As String
   Dim SQL As String
   Dim SqlPeriodo As String
   On Error GoTo h_ERROR
   SQL = "SELECT PERIODO.ConsecutivoPeriodo"
   SQL = SQL & " FROM PERIODO"
   SQL = SQL & " WHERE PERIODO.ConsecutivoCompania = " & valConsecutivoCompania
   SqlPeriodo = gDbUtil.fBuildResultSetAsString(SQL, False)
   SQL = "(SELECT " & NombreTablaComprobante & ".NUMERO"
   SQL = SQL & " FROM " & NombreTablaComprobante
   SQL = SQL & " WHERE " & NombreTablaComprobante & ".NoDocumentoOrigen = " & valNumeroCobranza
   SQL = SQL & " AND " & NombreTablaComprobante & ".GeneradoPor = " & gUtilSQL.fSQLSimpleValueForEnum(enum_ComprobanteGeneradoPor.eCG_COBRANZA)
   SQL = SQL & " AND " & NombreTablaComprobante & ".ConsecutivoPeriodo IN (" & SqlPeriodo & ")"
   SQL = SQL & " AND " & gUtilSQL.DfSQLDateValueBetween("cobranza.Fecha", valFechaInicial, valFechaFinal) & ")"
   fSQLGenerarCuentaContable = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLGenerarCuentaContable", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLDesgloseCobranza(ByVal ValDesgloseXCliente As Boolean, ByVal valConsecutivoCompania As String, _
                                     ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, _
                                     ByVal ValImprimirClienteUnico As String, ByVal ValtxtNombreDeCliente As String, _
                                     ByVal gMonedaLocalActual As Object, ByVal gUltimaTasaDeCambio As Object, valMostrarCobranzasPorVentasDiferidas As Boolean) As String
    Dim SQL As String
    Dim SqlTipoTarjeta As String
    Dim sqlMonedaCobranza As String
    Dim gEnumProyecto As clsEnumAdministrativo
    On Error GoTo h_ERROR
    Set gEnumProyecto = New clsEnumAdministrativo
    
    sqlMonedaCobranza = "Cobranza.moneda"
                         
    SqlTipoTarjeta = gUtilSQL.DfSQLCaseIfForEnum("cobranza.CualTarjeta", enum_Tarjeta.eTa_VISA, gEnumProyecto.fenumTarjetaToStringInArray(True), "")
   
    SQL = "SELECT cobranza.Fecha,"
    SQL = SQL & " cobranza.Numero,"
    If ValDesgloseXCliente Then
        SQL = SQL & " cobranza.TotalCobrado,"
        SQL = SQL & " cobranza.RetencionISLR,"
        SQL = SQL & " cobranza.CobradoEfectivo,"
        SQL = SQL & " cobranza.CobradoCheque,"
        SQL = SQL & " cobranza.CobradoTarjeta,"
        SQL = SQL & " cobranza.TotalOtros,"
        SQL = SQL & " cobranza.RetencionIVA AS MontoIvaRetenido,"
    Else
        SQL = SQL & " cobranza.TotalCobrado * cobranza.CambioAbolivares AS TotalCobrado,"
        SQL = SQL & " cobranza.RetencionISLR * cobranza.CambioAbolivares AS RetencionISLR,"
        SQL = SQL & " cobranza.CobradoEfectivo * cobranza.CambioAbolivares AS CobradoEfectivo,"
        SQL = SQL & " cobranza.CobradoCheque * cobranza.CambioAbolivares AS CobradoCheque,"
        SQL = SQL & " cobranza.CobradoTarjeta * cobranza.CambioAbolivares AS CobradoTarjeta,"
        SQL = SQL & " cobranza.TotalOtros * cobranza.CambioAbolivares AS TotalOtros,"
        SQL = SQL & " cobranza.RetencionIVA * cobranza.CambioAbolivares AS MontoIvaRetenido,"
    End If
    SQL = SQL & " cobranza.NumerodelCheque,"
    SQL = SQL & sqlMonedaCobranza & " AS Moneda,"
    SQL = SQL & gUtilSQL.getIIF("cobranza.CobradoTarjeta = 0", gUtilSQL.fSimpleSqlValue(""), SqlTipoTarjeta, True)
    SQL = SQL & " As TipoTarjeta , "
    SQL = SQL & " cobranza.NroDeLaTarjeta, "
    SQL = SQL & " cobranza.NombreBanco, cliente.Nombre, cobranza.NroComprobanteRetIVA as NumeroComprobanteRetIVA, cuentaBancaria.CodigoMoneda"
    SQL = SQL & " FROM (cliente INNER JOIN cobranza ON (cliente.Codigo = cobranza.CodigoCliente) AND "
    SQL = SQL & "(cliente.ConsecutivoCompania = cobranza.ConsecutivoCompania))"
    SQL = SQL & "LEFT JOIN cuentaBancaria ON (cobranza.CodigoCuentaBancaria = cuentaBancaria.Codigo "
    SQL = SQL & " AND cuentaBancaria.ConsecutivoCompania = cobranza.ConsecutivoCompania)"
    SQL = SQL & fSQLMostrarCobranzasPorVentasDiferidas(valMostrarCobranzasPorVentasDiferidas)
    SQL = SQL & " WHERE (" & gUtilSQL.DfSQLDateValueBetween("cobranza.fecha", valFechaInicial, valFechaFinal) & ")"
    SQL = SQL & " AND cobranza.ConsecutivoCompania = " & valConsecutivoCompania
    SQL = SQL & " AND cobranza.StatusCobranza = " & gUtilSQL.fSimpleSqlValue("0")
   If ValDesgloseXCliente Then
      If ValImprimirClienteUnico Then
         SQL = SQL & " AND (Cliente.Nombre = " & gUtilSQL.fSimpleSqlValue(ValtxtNombreDeCliente) & ")"
      End If
      SQL = SQL & " ORDER BY cliente.Nombre, Moneda, cobranza.Fecha"
   Else
      SQL = SQL & " ORDER BY cliente.Nombre, cobranza.Fecha, Moneda"
   End If
   Set gEnumProyecto = Nothing
h_EXIT:
   fSQLDesgloseCobranza = SQL
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLDesgloseCobranza", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConstruirSQLDelReporteCobranzasXVendedor(ByVal usaCambioOriginal As Boolean, ByVal valNombreMonedaLocal As String, ByVal valElReporteEsEnMonLocal As Boolean, ByVal valFechaInicial As Date, _
                                                            ByVal valFechaFinal As Date, ByVal valUsaModuloContabilidad As Boolean, ByVal valIncluirDocumentosCobrados As Integer, ByVal valConsecutivoCompania As String, _
                                                               ByVal valCantidadAImprimir As String, ByVal valcodigoDeVendedor As String, ByVal valUsaCodigoVendedor As Boolean, _
                                                                  ByVal NombreTablaComprobante As String, ByVal gMonedaLocalActual As Object, ByVal gUltimaTasaDeCambio As Object, ByVal valMostrarCobranzasPorVentasDiferidas As Boolean) As String
   Dim SQL As String
   Dim sqlNombreMonedaReporte As String
   Dim sqlMonedaCuentaBancaria As String
   Dim sqlCobranzaCodigoCta As String
   Dim sqlMtoTotalCobrado As String
   Dim sqlMtoRestante As String
   Dim sqlMtoAbonado As String
   Dim sqlMonedaCobranza As String
   Dim sqlCambio As String
   Dim sqlMonedaCxC As String
   Dim sqlMonedaLocalSegunFecha As String
   Dim sqlMtoTotalReporte As String
   Dim sqlFechaCobranza As String
   Dim sqlCambioCxC As String
   Dim sqlCambioCxCBs As String
   Dim sqlMonedaCodigoCxC As String
   Dim sqlCambiosMonedaCobranza As String
   Dim sqlSimboloMonedaCobranza As String
   
   On Error GoTo h_ERROR
   Set gEnumReport = New clsEnumReport
   sqlFechaCobranza = "Cobranza.Fecha"
   sqlCambioCxC = "ISNULL(DocumentoCobrado.CambioAMonedaLocal, 1)"
   sqlMonedaLocalSegunFecha = gMonedaLocalActual.fSQLNombreMonedaALaFecha("cobranza.Fecha")
   sqlMonedaCuentaBancaria = gUtilSQL.getIIF("cuentaBancaria.NombreDeLaMoneda = " & gUtilSQL.fSimpleSqlValue(valNombreMonedaLocal), sqlMonedaLocalSegunFecha, "cuentaBancaria.NombreDeLaMoneda", True)
   sqlCobranzaCodigoCta = "cobranza.CodigoCuentaBancaria"
   sqlMonedaCobranza = "Cobranza.moneda"
   sqlMtoTotalCobrado = "cobranza.TotalCobrado"
   sqlMtoRestante = "documentoCobrado.MontoOriginalRestanteAlDiaDelc"
   
   sqlMtoAbonado = "DocumentoCobrado.MontoAbonado"
   
   sqlCambio = "cobranza.CambioABolivares"
   sqlMonedaCxC = "cxC.Moneda"
   sqlCambioCxCBs = "cxC.CambioABolivares"
   sqlMonedaCodigoCxC = "cxC.CodigoMoneda"
   If valElReporteEsEnMonLocal Then
      If usaCambioOriginal Then
         sqlCambio = gMonedaLocalActual.fSQLConvierteMontoSiAplicaMasDecimales(gMonedaLocalActual.GetHoyCodigoMoneda, sqlCambio, "Cobranza.Fecha")
         sqlCambioCxCBs = gMonedaLocalActual.fSQLConvierteMontoSiAplicaMasDecimales(gMonedaLocalActual.GetHoyCodigoMoneda, sqlCambioCxCBs, "Cobranza.Fecha")
         sqlMtoAbonado = "(CASE" & vbCrLf
         sqlMtoAbonado = sqlMtoAbonado & "   WHEN CxC.CodigoMoneda <> Cobranza.CodigoMoneda THEN" & vbCrLf
         sqlMtoAbonado = sqlMtoAbonado & "      (CASE" & vbCrLf
         sqlMtoAbonado = sqlMtoAbonado & "         WHEN (Cobranza.CodigoMoneda <> '" & gMonedaLocalActual.GetHoyCodigoMoneda & "')" & vbCrLf
         sqlMtoAbonado = sqlMtoAbonado & "            THEN (DocumentoCobrado.MontoAbonado * ISNULL(DocumentoCobrado.CambioAMonedaLocal, 1))" & vbCrLf
         sqlMtoAbonado = sqlMtoAbonado & "            ELSE documentoCobrado.MontoAbonado" & vbCrLf
         sqlMtoAbonado = sqlMtoAbonado & "      END)" & vbCrLf
         sqlMtoAbonado = sqlMtoAbonado & "   ELSE" & vbCrLf
         sqlMtoAbonado = sqlMtoAbonado & "   (CASE" & vbCrLf
         sqlMtoAbonado = sqlMtoAbonado & "      WHEN (Cobranza.CodigoMoneda = CxC.CodigoMoneda)" & vbCrLf
         sqlMtoAbonado = sqlMtoAbonado & "         THEN documentoCobrado.MontoAbonado * Cobranza.CambioABolivares" & vbCrLf
         sqlMtoAbonado = sqlMtoAbonado & "     ELSE documentoCobrado.MontoAbonado" & vbCrLf
         sqlMtoAbonado = sqlMtoAbonado & "   END)" & vbCrLf
         sqlMtoAbonado = sqlMtoAbonado & "END)" & vbCrLf
      Else
         sqlCambio = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("CambioABolivares", "Cobranza.Moneda", "1", False, "")
         sqlCambioCxCBs = gUltimaTasaDeCambio.getUltimaTasaDeCambio("cxC.Moneda", False)
         sqlCambiosMonedaCobranza = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("CambioABolivares", "Cobranza.Moneda", "DocumentoCobrado.MontoAbonado", False, "")
         sqlMtoAbonado = "(CASE" & vbCrLf
         sqlMtoAbonado = sqlMtoAbonado & "   WHEN CxC.CodigoMoneda <> Cobranza.CodigoMoneda THEN" & vbCrLf
         sqlMtoAbonado = sqlMtoAbonado & "      (CASE" & vbCrLf
         sqlMtoAbonado = sqlMtoAbonado & "         WHEN (Cobranza.CodigoMoneda <> '" & gMonedaLocalActual.GetHoyCodigoMoneda & "')" & vbCrLf
         sqlMtoAbonado = sqlMtoAbonado & "            THEN " & sqlCambiosMonedaCobranza & vbCrLf
         sqlMtoAbonado = sqlMtoAbonado & "            ELSE documentoCobrado.MontoAbonado" & vbCrLf
         sqlMtoAbonado = sqlMtoAbonado & "      END)" & vbCrLf
         sqlMtoAbonado = sqlMtoAbonado & "   ELSE" & vbCrLf
         sqlMtoAbonado = sqlMtoAbonado & "   (CASE" & vbCrLf
         sqlMtoAbonado = sqlMtoAbonado & "      WHEN (Cobranza.CodigoMoneda = CxC.CodigoMoneda)" & vbCrLf
         sqlMtoAbonado = sqlMtoAbonado & "         THEN " & sqlCambiosMonedaCobranza & vbCrLf
         sqlMtoAbonado = sqlMtoAbonado & "     ELSE documentoCobrado.MontoAbonado" & vbCrLf
         sqlMtoAbonado = sqlMtoAbonado & "   END)" & vbCrLf
         sqlMtoAbonado = sqlMtoAbonado & "END)" & vbCrLf
      End If
      sqlMtoTotalReporte = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCambio, sqlMonedaCobranza, sqlMtoTotalCobrado, usaCambioOriginal, "")
      sqlMtoRestante = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCambioCxCBs, sqlMonedaCxC, sqlMtoRestante, usaCambioOriginal, "")
      sqlNombreMonedaReporte = gUtilSQL.fSimpleSqlValue(valNombreMonedaLocal)
      sqlSimboloMonedaCobranza = "Moneda.Simbolo AS SimboloMonedaCobro,"
   Else
      sqlMtoTotalReporte = sqlMtoTotalCobrado
      sqlNombreMonedaReporte = sqlMonedaCobranza
      sqlMtoRestante = "(CASE WHEN CxC.CodigoMoneda <> Cobranza.CodigoMoneda THEN" & vbCrLf
      sqlMtoRestante = sqlMtoRestante & "    (CASE WHEN (Cobranza.CodigoMoneda = '" & gMonedaLocalActual.GetHoyCodigoMoneda & "')"
      sqlMtoRestante = sqlMtoRestante & "    AND (CxC.CodigoMoneda <> '" & gMonedaLocalActual.GetHoyCodigoMoneda & "')" & vbCrLf
      sqlMtoRestante = sqlMtoRestante & "        THEN(DocumentoCobrado.MontoOriginalRestanteAlDiaDelc * ISNULL(DocumentoCobrado.CambioAMonedaLocal, 1))" & vbCrLf
      sqlMtoRestante = sqlMtoRestante & "    ELSE (DocumentoCobrado.MontoOriginalRestanteAlDiaDelc / ISNULL(DocumentoCobrado.CambioAMonedaLocal, 1))" & vbCrLf
      sqlMtoRestante = sqlMtoRestante & "    END)" & vbCrLf
      sqlMtoRestante = sqlMtoRestante & "ELSE documentoCobrado.MontoOriginalRestanteAlDiaDelc" & vbCrLf
      sqlMtoRestante = sqlMtoRestante & "END)" & vbCrLf
   End If
   SQL = ""
   SQL = " SELECT "
   SQL = SQL & "cobranza.CodigoCobrador, "
   SQL = SQL & "vendedor.Nombre AS NombreVendedor, "
   SQL = SQL & sqlNombreMonedaReporte & " AS MonedaCobro, "
   SQL = SQL & "cobranza.Numero, "
   SQL = SQL & "cobranza.Fecha, "
   SQL = SQL & "cliente.Nombre AS NombreCliente, "
   SQL = SQL & sqlMtoTotalReporte & " AS MtoTotalCobrado, "
   If valUsaModuloContabilidad Then
      SQL = SQL & gUtilSQL.getIIF(fSQLGenerarCuentaContable(valConsecutivoCompania, NombreTablaComprobante, valFechaInicial, valFechaFinal, "Cobranza.Numero") & " <> " & gUtilSQL.fSimpleSqlValue(""), fSQLGenerarCuentaContable(valConsecutivoCompania, NombreTablaComprobante, valFechaInicial, valFechaFinal, "Cobranza.Numero"), _
                        gUtilSQL.fSimpleSqlValue("No Aplica"), True)
      SQL = SQL & " AS NumeroComprobante, "
   End If
   If valIncluirDocumentosCobrados = vbChecked Then
      SQL = SQL & "documentoCobrado.NumeroDelDocumentoCobrado, "
      SQL = SQL & "documentoCobrado.TipoDeDocumentoCobrado, "
      SQL = SQL & sqlMtoRestante & " AS MtoOriginal, "
      SQL = SQL & sqlMtoAbonado & " AS MtoAbonado, "
      SQL = SQL & sqlMonedaCxC & " AS MonedaDoc, "
      SQL = SQL & sqlSimboloMonedaCobranza
   End If
   SQL = SQL & sqlCambio & " AS CambioABolivares"
   If valIncluirDocumentosCobrados = vbChecked Then
      SQL = SQL & " FROM vendedor INNER JOIN"
      SQL = SQL & " cliente INNER JOIN"
      SQL = SQL & " cobranza ON cliente.Codigo = cobranza.CodigoCliente"
      SQL = SQL & " AND cliente.ConsecutivoCompania = cobranza.ConsecutivoCompania ON"
      SQL = SQL & " vendedor.Codigo = cobranza.CodigoCobrador"
      SQL = SQL & " AND vendedor.ConsecutivoCompania = cobranza.ConsecutivoCompania"
      SQL = SQL & " INNER JOIN documentoCobrado ON cobranza.Numero = documentoCobrado.NumeroCobranza AND"
      SQL = SQL & " cobranza.ConsecutivoCompania = documentoCobrado.ConsecutivoCompania "
      SQL = SQL & " INNER JOIN cxC ON vendedor.ConsecutivoCompania = cxC.ConsecutivoCompania"
      'SQL = SQL & " AND vendedor.Codigo = cxC.CodigoVendedor "
      SQL = SQL & " AND cliente.ConsecutivoCompania = cxC.ConsecutivoCompania"
      SQL = SQL & " AND cliente.Codigo = cxC.CodigoCliente AND"
      SQL = SQL & " documentoCobrado.ConsecutivoCompania = cxC.ConsecutivoCompania"
      SQL = SQL & " AND documentoCobrado.NumeroDelDocumentoCobrado = cxC.Numero AND"
      SQL = SQL & " documentoCobrado.TipoDeDocumentoCobrado = cxC.tipoCxC"
      SQL = SQL & " INNER JOIN Moneda ON Moneda.Codigo = Cobranza.CodigoMoneda"
   Else
      SQL = SQL & " FROM vendedor INNER JOIN (cliente"
      SQL = SQL & " INNER JOIN (cobranza LEFT JOIN cuentaBancaria ON ("
      SQL = SQL & " cobranza.ConsecutivoCompania = cuentaBancaria.ConsecutivoCompania"
      SQL = SQL & ") AND (cobranza.CodigoCuentaBancaria = cuentaBancaria.Codigo"
      SQL = SQL & ")) ON (cliente.Codigo = cobranza.CodigoCliente"
      SQL = SQL & ") AND (cliente.ConsecutivoCompania = cobranza.ConsecutivoCompania"
      SQL = SQL & ")) ON (vendedor.Codigo = cobranza.CodigoCobrador"
      SQL = SQL & ") AND (vendedor.ConsecutivoCompania = cobranza.ConsecutivoCompania)"
   End If
   SQL = SQL & " WHERE " & gUtilSQL.DfSQLDateValueBetween("cobranza.Fecha", valFechaInicial, valFechaFinal)
   If valCantidadAImprimir = gEnumReport.enumCantidadAImprimirToString(eCI_uno) Then
      SQL = SQL & " AND cobranza.CodigoCobrador = " & gUtilSQL.fSimpleSqlValue(valcodigoDeVendedor)
   End If
   SQL = SQL & " AND cobranza.StatusCobranza = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusCobranza.eSC_VIGENTE)
   SQL = SQL & " AND vendedor.ConsecutivoCompania = " & valConsecutivoCompania
   SQL = SQL & " ORDER BY "
   If valUsaCodigoVendedor Then
      SQL = SQL & "cobranza.CodigoCobrador, "
      SQL = SQL & "vendedor.Nombre, "
   Else
      SQL = SQL & "vendedor.Nombre, "
      SQL = SQL & "cobranza.CodigoCobrador, "
   End If
   SQL = SQL & " MonedaCobro, "
   SQL = SQL & "cobranza.Fecha, "
   SQL = SQL & "cobranza.Numero"
   fConstruirSQLDelReporteCobranzasXVendedor = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: SQL = ""
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fConstruirSQLDelReporteCobranzasXVendedor", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLDevuelveElCamBioALaFechaDelCobro(ByVal sqlMoneda As String, ByVal sqlFecha As String) As String
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = ""
   SQL = "SELECT "
   SQL = SQL & "cambio.CambioAbolivares AS TasaDeCambioALaFechaDelDocumento "
   SQL = SQL & "FROM cobranza LEFT JOIN IGV_CambioConPeriodoVigencia AS Cambio ON " & sqlMoneda & " = Cambio.nombre "
   SQL = SQL & "AND sqlfecha BETWEEN Cambio.VigenteDesde AND Cambio.VigenteHasta;"
   fSQLDevuelveElCamBioALaFechaDelCobro = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: SQL = ""
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fConstruirSQLDelReporteCobranzasXVendedor", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLComisionDeVendedoresXCobroDetallado(ByVal valConsecutivoCompania As Long, ByVal valCodigoCobrador As String, ByRef gMonedaLocalActual As Object, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date) As String
   Dim vSQL As String
   Dim insCobranzaVista As clsCobranzaVista
   On Error GoTo h_ERROR
   Set insCobranzaVista = New clsCobranzaVista
   vSQL = "SELECT CodigoCobrador,NombreVenededor, "
   vSQL = vSQL & "FechaCobro, NumeroCobro,NombreCliente, "
   vSQL = vSQL & gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "SUM(MontoAbonadoACxC)", "FechaCxC") & " as TotalDocumento, "
   vSQL = vSQL & gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "SUM(MontoComisionable)", "FechaCxC") & " as MontoComisionable "
   vSQL = vSQL & " FROM " & insCobranzaVista.GetViewNameComisionDeVendedoresPorCobroDetallado
   vSQL = vSQL & " WHERE " & gUtilSQL.fSQLValueBetween("FechaCobro", gConvert.dateToString(valFechaInicial), gConvert.dateToString(valFechaFinal))
   vSQL = vSQL & " AND ConsecutivoCompania = " & valConsecutivoCompania
   If gTexto.DfLen(valCodigoCobrador) <> 0 Then
      vSQL = vSQL & " AND CodigoCobrador = " & gUtilSQL.fSimpleSqlValue(valCodigoCobrador)
   End If
   vSQL = vSQL & "GROUP BY CodigoCobrador,NombreVenededor, FechaCobro, NumeroCobro,NombreCliente, FechaCxC"
   vSQL = vSQL & " ORDER BY CodigoCobrador, FechaCobro"
   Set insCobranzaVista = Nothing
h_EXIT:     On Error GoTo 0
   fSQLComisionDeVendedoresXCobroDetallado = vSQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlFichaCliente", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLRetencionIVA(ByVal valConsecutivoCompania As Long, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valInsMonedaLocalActual As Object) As String
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = "SELECT cobranza.Fecha, cobranza.Numero, documentoCobrado.NumeroDelDocumentoCobrado, cliente.Codigo, cliente.Nombre, "
   SQL = SQL & "documentoCobrado.MontoAbonado AS TotalCobrado, "
   SQL = SQL & valInsMonedaLocalActual.fSQLConvierteMontoSiAplica(valInsMonedaLocalActual.GetHoyCodigoMoneda(), "documentoCobrado.MontoIvaRetenido", "CxC.Fecha") & " as RetencionIva, "
   SQL = SQL & "documentoCobrado.NumeroComprobanteRetIVA, cliente.NumeroRIF "
   SQL = SQL & " FROM cliente INNER JOIN ("
   SQL = SQL & "cobranza INNER JOIN documentoCobrado "
   SQL = SQL & " ON (cobranza.Numero = documentoCobrado.NumeroCobranza"
   SQL = SQL & ") AND (cobranza.ConsecutivoCompania = documentoCobrado.ConsecutivoCompania"
   SQL = SQL & ")) ON (cliente.Codigo = cobranza.CodigoCliente"
   SQL = SQL & ") AND (cliente.ConsecutivoCompania = cobranza.ConsecutivoCompania)"
   SQL = SQL & " INNER JOIN CxC "
   SQL = SQL & " ON (CxC.CodigoCliente = Cliente.Codigo"
   SQL = SQL & ") AND (CxC.Numero = documentoCobrado.NumeroDelDocumentoCobrado"
   SQL = SQL & ") AND (CxC.ConsecutivoCompania = documentoCobrado.ConsecutivoCompania"
   SQL = SQL & ") AND (CxC.tipoCxC = documentoCobrado.TipoDeDocumentoCobrado)"
   SQL = SQL & " WHERE cliente.ConsecutivoCompania = " & valConsecutivoCompania
   SQL = SQL & " AND " & gUtilSQL.DfSQLDateValueBetween("cobranza.Fecha", valFechaInicial, valFechaFinal)
   SQL = SQL & " AND cobranza.RetencionIVA <> 0 "
   SQL = SQL & " AND cobranza.StatusCobranza = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusCobranza.eSC_VIGENTE)
   SQL = SQL & " AND CxC.NoAplicaParaLibroDeVentas = " & gUtilSQL.fBooleanToSqlValue(False)
   SQL = SQL & " GROUP BY  cobranza.Fecha, cobranza.Numero, cliente.Codigo,"
   SQL = SQL & " cliente.Nombre,  documentoCobrado.MontoAbonado, documentoCobrado.NumeroDelDocumentoCobrado, "
   SQL = SQL & " documentoCobrado.MontoIvaRetenido, documentoCobrado.FechaComprobanteRetIVA, cxc.Fecha, documentoCobrado.NumeroComprobanteRetIVA, cliente.NumeroRIF "
   SQL = SQL & " ORDER BY cobranza.Fecha, cobranza.Numero, "
   SQL = SQL & "documentoCobrado.NumeroComprobanteRetIVA"
h_EXIT:   On Error GoTo 0
   fSQLRetencionIVA = SQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLRetencionIVA", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLCobranzasConRetencionIVAPendientePorDistribuir(ByVal valConsecutivoCompania As Long) As String
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = "SELECT "
   SQL = SQL & "cliente.Codigo, "
   SQL = SQL & "cliente.Nombre, "
   SQL = SQL & "cobranza.Numero, "
   SQL = SQL & "cobranza.Fecha, "
   SQL = SQL & "cobranza.TotalCobrado, "
   SQL = SQL & "cobranza.RetencionIVA, "
   SQL = SQL & "cobranza.Moneda "
   SQL = SQL & " FROM cobranza"
   SQL = SQL & " INNER JOIN cliente"
   SQL = SQL & " ON cobranza.ConsecutivoCompania" & _
               " = cliente.ConsecutivoCompania"
   SQL = SQL & " AND cobranza.CodigoCliente" & _
               " = cliente.Codigo"
   SQL = SQL & " WHERE cobranza.StatusRetencionIVA" & _
               " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusRetencionIVACobranza.eSR_PENDIENTE_POR_DISTRIBUIR)
   SQL = SQL & " AND cobranza.StatusCobranza" & _
               " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusCobranza.eSC_VIGENTE)
   SQL = SQL & " AND cobranza.ConsecutivoCompania" & _
               " = " & valConsecutivoCompania
   SQL = SQL & " ORDER BY "
   SQL = SQL & "cobranza.Fecha, "
   SQL = SQL & "cobranza.Moneda, "
   SQL = SQL & "cobranza.Numero"
h_EXIT:
   fSQLCobranzasConRetencionIVAPendientePorDistribuir = SQL
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLCobranzasConRetencionIVAPendientePorDistribuir", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function


Public Function fSQLComisionAgentesXLineaDeProducto(ByVal gMonedaLocalActual As Object, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, _
                                             ByVal valImprimeUno As Boolean, ByVal CodigoDeVendedor As String) As String
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = "SELECT dbo.comisionesAgentesVistaBase.codigovendedor AS CodigoVendedor, MAX(dbo.comisionesAgentesVistaBase.nombreVendedor) AS NombreVendedor, " & _
                      "dbo.comisionesAgentesVistaBase.NumeroCobranza AS NumeroCobranza, dbo.comisionesAgentesVistaBase.FechaCobranza, MIN(dbo.factura.Numero) AS NumeroFactura, " & _
                      "MIN(" & gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda(), "dbo.factura.TotalBaseImponible", "factura.Fecha") & ") AS MontoFactura, dbo.articuloInventario.LineaDeProducto AS NombreLineaProducto, " & gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "SUM(dbo.renglonFactura.TotalRenglon) " & _
                      "/ (1 + dbo.factura.PorcentajeDescuento / 100) / (CASE WHEN Factura.fecha < CONVERT(DATETIME, '2005-10-01 00:00:00', 102)THEN 1.15 ELSE (CASE WHEN Factura.fecha <CONVERT(DATETIME, '2007-03-01 00:00:00', 102) THEN 1.14 ELSE (CASE WHEN Factura.fecha <CONVERT(DATETIME, '2007-07-01 00:00:00', 102) THEN 1.11 ELSE 1.09 END)END)END) " & _
                      "", "factura.Fecha") & "AS MontoXLinea, SUM(dbo.renglonFactura.TotalRenglon) / (1 + dbo.factura.PorcentajeDescuento / 100)" & _
                      "/ (CASE WHEN Factura.fecha < CONVERT(DATETIME, '2005-10-01 00:00:00', 102)THEN 1.15 ELSE (CASE WHEN Factura.fecha <CONVERT(DATETIME, '2007-03-01 00:00:00', 102) THEN 1.14 ELSE (CASE WHEN Factura.fecha <CONVERT(DATETIME, '2007-07-01 00:00:00', 102) THEN 1.11 ELSE 1.09 END)END)END) / MIN(CASE WHEN (dbo.factura.TotalBaseImponible = 0) THEN 1 ELSE (dbo.factura.TotalBaseImponible) END  )" & _
                      "* 100 AS PorcentajeLinea, " & gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "dbo.comisionesAgentesVistaBase.MontoAbonado", "dbo.comisionesAgentesVistaBase.FechaCxC") & " / (CASE WHEN Factura.fecha < CONVERT(DATETIME, '2005-10-01 00:00:00', 102)THEN 1.15 ELSE (CASE WHEN Factura.fecha <CONVERT(DATETIME, '2007-03-01 00:00:00', 102) THEN 1.14 ELSE (CASE WHEN Factura.fecha <CONVERT(DATETIME, '2007-07-01 00:00:00', 102) THEN 1.11 ELSE 1.09 END)END)END)" & _
                      "AS MontoAbonado, " & gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "SUM(dbo.renglonFactura.TotalRenglon) / (1 + dbo.factura.PorcentajeDescuento / 100)/ (CASE WHEN Factura.fecha < CONVERT(DATETIME, '2005-10-01 00:00:00', 102)THEN 1.15 ELSE (CASE WHEN Factura.fecha <CONVERT(DATETIME, '2007-03-01 00:00:00', 102) THEN 1.14 ELSE (CASE WHEN Factura.fecha <CONVERT(DATETIME, '2007-07-01 00:00:00', 102) THEN 1.11 ELSE 1.09 END)END)END) / MIN( CASE WHEN (dbo.factura.TotalBaseImponible = 0) THEN 1 ELSE (dbo.factura.TotalBaseImponible)  )" & _
                      "* MIN(dbo.comisionesAgentesVistaBase.MontoAbonado) / (CASE WHEN Factura.fecha < CONVERT(DATETIME, '2005-10-01 00:00:00', 102)THEN 1.15 ELSE (CASE WHEN Factura.fecha <CONVERT(DATETIME, '2007-03-01 00:00:00', 102) THEN 1.14 ELSE (CASE WHEN Factura.fecha <CONVERT(DATETIME, '2007-07-01 00:00:00', 102) THEN 1.11 ELSE 1.09 END)END)END)" & _
                      "", "Factura.Fecha") & " AS MontoComisionable, cliente.Nombre as NombreCliente"
   SQL = SQL & " FROM renglonFactura INNER JOIN " & _
                      "factura ON renglonFactura.ConsecutivoCompania = factura.ConsecutivoCompania AND renglonFactura.NumeroFactura = factura.Numero AND " & _
                      "renglonFactura.TipoDeDocumento = factura.TipoDeDocumento INNER JOIN " & _
                      "articuloInventario ON renglonFactura.ConsecutivoCompania = articuloInventario.ConsecutivoCompania AND " & _
                      "renglonFactura.Articulo = articuloInventario.Codigo INNER JOIN " & _
                      "comisionesAgentesVistaBase ON factura.Numero = comisionesAgentesVistaBase.NumeroDocumentoOrigen INNER JOIN " & _
                      " cliente ON factura.ConsecutivoCompania = cliente.ConsecutivoCompania AND factura.CodigoCliente = cliente.Codigo "
   SQL = SQL & "GROUP BY dbo.articuloInventario.LineaDeProducto, dbo.comisionesAgentesVistaBase.NumeroCobranza, dbo.comisionesAgentesVistaBase.MontoAbonado, " & _
                      "dbo.comisionesAgentesVistaBase.FechaCobranza , fecha, dbo.factura.porcentajeDescuento, dbo.comisionesAgentesVistaBase.CodigoVendedor, cliente.nombre, dbo.comisionesAgentesVistaBase.FechaCxC " & _
   "HAVING      FechaCobranza BETWEEN " & gUtilSQL.fDateToSQLValue(valFechaInicial) & " AND " & gUtilSQL.fDateToSQLValue(valFechaFinal) & " "
   
   If valImprimeUno Then
      SQL = gUtilSQL.fSQLValueWithAnd(SQL, "dbo.comisionesAgentesVistaBase.codigovendedor", CodigoDeVendedor, False)
   End If
   SQL = SQL & "ORDER BY dbo.comisionesAgentesVistaBase.codigovendedor, dbo.comisionesAgentesVistaBase.FechaCobranza, numeroCobranza, numerofactura, lineadeproducto"
h_EXIT:    On Error GoTo 0
   fSQLComisionAgentesXLineaDeProducto = SQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLComisionAgentesXLineaDeProducto", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

'Private Function fSQLComisionAgentes() As String
'   Dim Sql As String
'   Dim SQLDescXFactura As String
'   Dim SQLDescXRenglon As String
'   Dim SqlTotalRenglon As String
'   On Error GoTo h_ERROR
'   SQLDescXFactura = "(1 + (factura.Porcentajedescuento/100))"
'   SQLDescXRenglon = "(1 + (renglonfactura.PorcentajeDescuento/100))"
'   SqlTotalRenglon = "(renglonFactura.PrecioSinIva * renglonFactura.cantidad)"
'   Sql = " SELECT   cobranza.CodigoCobrador AS CodigoVendedor, vendedor.Nombre AS NombreVendedor, "
'   Sql = Sql & " cobranza.Numero AS NumeroCobranza, cobranza.Fecha as FechaCobranza, "
'   Sql = Sql & " cobranza.TotalCobrado as MontoCobranza, factura.Numero AS NumeroFactura,"
'   Sql = Sql & " factura.Fecha AS FechaFactura, factura.TotalBaseImponible as MontoFacturado, "
'   Sql = Sql & " articuloInventario.LineaDeProducto as NombreLineaProducto, "
'   Sql = Sql & " renglonFactura.Articulo as CodigoArticulo,"
'   Sql = Sql & " renglonFactura.Descripcion as DescripcionArticulo,"
'   Sql = Sql & " ( " & SqlTotalRenglon & " / " & SQLDescXFactura & " / " & SQLDescXRenglon & " )   as TotalRenglon "
'   Sql = Sql & " FROM renglonFactura INNER JOIN "
'   Sql = Sql & " factura ON renglonFactura.ConsecutivoCompania = factura.ConsecutivoCompania AND renglonFactura.NumeroFactura = "
'   Sql = Sql & " factura.Numero AND"
'   Sql = Sql & " renglonFactura.TipoDeDocumento = factura.TipoDeDocumento INNER JOIN"
'   Sql = Sql & " cobranza INNER JOIN"
'   Sql = Sql & " documentoCobrado ON cobranza.ConsecutivoCompania = documentoCobrado.ConsecutivoCompania AND"
'   Sql = Sql & " cobranza.Numero = documentoCobrado.NumeroCobranza ON factura.Numero = documentoCobrado.NumeroDelDocumentoCobrado"
'   Sql = Sql & " Inner Join"
'   Sql = Sql & " vendedor ON factura.ConsecutivoCompania = vendedor.ConsecutivoCompania AND factura.CodigoVendedor = vendedor.Codigo"
'   Sql = Sql & " AND"
'   Sql = Sql & " cobranza.ConsecutivoCompania = vendedor.ConsecutivoCompania AND cobranza.CodigoCobrador = vendedor.Codigo INNER JOIN"
'   Sql = Sql & " articuloInventario ON renglonFactura.ConsecutivoCompania = articuloInventario.ConsecutivoCompania AND"
'   Sql = Sql & " renglonFactura.Articulo = ArticuloInventario.codigo"
'   Sql = Sql & " WHERE " & gUtilSQL.DfSQLDateValueBetween("cobranza.Fecha", valFechaInicial, valFechaFinal)
'   Sql = Sql & " AND cobranza.StatusCobranza <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusCobranza.eSC_ANULADA)
'   If gApi.SelectedElementInComboBoxToString(CmbCantidadAImprimir) = gEnumReport.enumCantidadAImprimirToString(eCI_uno) Then
'      Sql = gUtilSQL.fSQLValueWithAnd(Sql, "cobranza.CodigoCobrador", txtCodigoDeVendedor.Text)
'   End If
'   Sql = Sql & " AND cobranza.ConsecutivoCompania = " & valConsecutivoCompania
'   Sql = Sql & " ORDER BY vendedor.Nombre, cobranza.Fecha, cobranza.Numero, factura.Fecha, articuloInventario.LineaDeProducto, renglonFactura.Articulo "
'h_EXIT:    On Error GoTo 0
'   fSQLComisionAgentes = Sql
'   Exit Function
'h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
'      "fSQLComisionAgentes", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
'End Function


Public Function fConstruirSQLPorVentasDelReporteComisionDeVendedorDetallado(ByVal sqlMontoRenglonSinIvaConDescFact As String, ByVal ReporteEnMonedaLocal As Boolean, ByVal mUsarCambioOriginal As Boolean, _
                                                            ByRef gUltimaTasaDeCambio As Object, ByVal gMonedaLocalActual As Object, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valCantidadAImprimir As String, _
                                                            ByVal valNombreDeVendedor As String, ByVal valConsecutivoCompania As Long) As String
   Dim SQL As String
   Dim SqlTallaYColores As String
   Dim montoAbonado As String
   Dim montoExento As String
   Dim montoBaseImponible As String
   Dim sqlSumaTotalRenglon As String
   Dim SqlTotalcomision As String
   Dim sqlComisionConLaExclusion As String
   Dim montoAbonadoBs As String
   Dim montoExentoBs As String
   Dim montoBaseImponibleBs As String
   
   On Error GoTo h_ERROR
   sqlSumaTotalRenglon = sqlMontoRenglonSinIvaConDescFact
   montoAbonadoBs = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("factura.CambioABolivares", _
   "factura.Moneda", "factura.TotalFactura", mUsarCambioOriginal, "")
   montoExentoBs = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("factura.CambioABolivares", _
   "factura.Moneda", "factura.TotalMontoExento", mUsarCambioOriginal, "")
   montoBaseImponibleBs = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("factura.CambioABolivares", _
   "factura.Moneda", _
   "factura.TotalBaseImponible", _
   mUsarCambioOriginal, "")
   
   If ReporteEnMonedaLocal Then
      montoAbonado = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("factura.CambioABolivares", _
      "factura.Moneda", _
      "factura.TotalFactura", _
      mUsarCambioOriginal, "")
      montoExento = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("factura.CambioABolivares", _
      "factura.Moneda", _
      "factura.TotalMontoExento", _
      mUsarCambioOriginal, "")
      montoBaseImponible = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("factura.CambioABolivares", _
      "factura.Moneda", _
      "factura.TotalBaseImponible", _
      mUsarCambioOriginal, "")
      sqlSumaTotalRenglon = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("factura.CambioABolivares", _
      "factura.Moneda", _
      sqlSumaTotalRenglon, _
      mUsarCambioOriginal, "")
   Else
      montoAbonado = "factura.TotalFactura"
      montoExento = "factura.TotalMontoExento"
      montoBaseImponible = "factura.TotalBaseImponible"
   End If
   sqlSumaTotalRenglon = gUtilSQL.DfCDecSQL(sqlSumaTotalRenglon)
   sqlSumaTotalRenglon = " SUM(" & sqlSumaTotalRenglon & ")"
   sqlSumaTotalRenglon = gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull(sqlSumaTotalRenglon), 0, sqlSumaTotalRenglon, True)
   SQL = "SELECT cliente.Nombre as NombreCliente, "
   SQL = SQL & " 'Fact. ' + factura.Numero as NumeroDelDocumento, "
   SQL = SQL & "factura.Fecha as FechaDelDocumento, "
   If ReporteEnMonedaLocal Then
      SQL = SQL & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda)
   Else
      SQL = SQL & "factura.Moneda"
   End If
   SQL = SQL & " as DescripcionDeLaMoneda,"
   SQL = SQL & "factura.CambioABolivares as CambioABolivares, "
   SQL = SQL & montoAbonado & " As MontoAbonado, "
   SQL = SQL & montoExento & " As MontoExento, "
   SQL = SQL & montoBaseImponible & " As MontoBaseImponible, "
   SQL = SQL & montoAbonadoBs & " As MontoAbonadoBs, "
   SQL = SQL & montoExentoBs & " As MontoExentoBs, "
   SQL = SQL & montoBaseImponibleBs & " As MontoBaseImponibleBs, "
   SQL = SQL & sqlSumaTotalRenglon & " As MontoComision,"
   SQL = SQL & "(" & montoExentoBs & " + " & montoBaseImponibleBs & ") As MontoComisionBs, "
   SQL = SQL & "Moneda.simbolo as SimboloDeLaMoneda, "
   SQL = SQL & "vendedor.Codigo as CodigoDelVendedor, "
   SQL = SQL & "vendedor.Nombre as NombreDelVendedor, "
   SQL = SQL & "vendedor.comisionPorVenta as PorcentajeDeComision "
''   Sql = Sql & " FROM cliente"
''   Sql = Sql & " INNER JOIN (Moneda"
''   Sql = Sql & " INNER JOIN ((vendedor"
''   Sql = Sql & " INNER JOIN factura"
''   Sql = Sql & " ON (vendedor.Codigo = factura.CodigoVendedor"
''   Sql = Sql & ") AND (vendedor.ConsecutivoCompania = factura.ConsecutivoCompania"
''   Sql = Sql & ")) INNER JOIN (articuloInventario"
''   Sql = Sql & " INNER JOIN renglonFactura"
''   Sql = Sql & " ON (articuloInventario.Codigo = renglonFactura.Articulo"
''   Sql = Sql & ") AND (articuloInventario.ConsecutivoCompania = renglonFactura.ConsecutivoCompania"
''   Sql = Sql & ")) ON (factura.TipoDeDocumento = renglonFactura.TipoDeDocumento"
''   Sql = Sql & ") AND (factura.Numero = renglonFactura.NumeroFactura"
''   Sql = Sql & ") AND (factura.ConsecutivoCompania = renglonFactura.ConsecutivoCompania"
''   Sql = Sql & ")) ON Moneda.Nombre = factura.Moneda"
''   Sql = Sql & ") ON (cliente.Codigo = factura.CodigoCliente"
''   Sql = Sql & ") AND (cliente.ConsecutivoCompania = factura.ConsecutivoCompania"
''   Sql = Sql & ")"
   
  
   SQL = SQL & " FROM ExistenciaPorGrupo RIGHT JOIN "
   SQL = SQL & "factura INNER JOIN "
   SQL = SQL & "renglonFactura ON factura.ConsecutivoCompania = renglonFactura.ConsecutivoCompania "
   SQL = SQL & "AND factura.Numero = renglonFactura.NumeroFactura "
   SQL = SQL & "AND factura.TipoDeDocumento = renglonFactura.TipoDeDocumento "
   SQL = SQL & "INNER JOIN cliente ON factura.ConsecutivoCompania = cliente.ConsecutivoCompania "
   SQL = SQL & "AND factura.CodigoCliente = cliente.Codigo "
   SQL = SQL & "INNER JOIN vendedor "
   SQL = SQL & "ON factura.ConsecutivoCompania = vendedor.ConsecutivoCompania "
   SQL = SQL & "AND factura.CodigoVendedor = vendedor.Codigo "
   SQL = SQL & "INNER JOIN Moneda "
   SQL = SQL & "ON factura.CodigoMoneda = Moneda.Codigo "
   SQL = SQL & "ON ExistenciaPorGrupo.ConsecutivoCompania = renglonFactura.ConsecutivoCompania "
   SQL = SQL & "AND ExistenciaPorGrupo.CodigoArticulo + ExistenciaPorGrupo.CodigoColor + ExistenciaPorGrupo.CodigoTalla = renglonFactura.Articulo "
   SQL = SQL & "AND ExistenciaPorGrupo.Serial = renglonFactura.Serial "
   SQL = SQL & "LEFT Join "
   SQL = SQL & "articuloInventario ON "
   SQL = SQL & "renglonFactura.ConsecutivoCompania = articuloInventario.ConsecutivoCompania "
   SQL = SQL & "AND renglonFactura.Articulo = articuloInventario.Codigo "
   'Sql = Sql & "AND articuloInventario.ExcluirDeComision = " & gUtilSQL.fBooleanToSqlValue(False)
   SQL = SQL & " WHERE factura.StatusFactura" & _
               " =" & gUtilSQL.fSimpleSqlValue(gConvert.enumerativoAChar(eSF_EMITIDA))
   SQL = SQL & " AND " & gUtilSQL.DfSQLDateValueBetween("factura.Fecha", valFechaInicial, valFechaFinal)
   If valCantidadAImprimir <> "Todos" Then
      SQL = SQL & " AND " & gUtilSQL.fSQLValue("vendedor.Nombre", valNombreDeVendedor, False)
   End If
   SQL = SQL & " AND (articuloInventario.ExcluirDeComision <> " & gUtilSQL.fBooleanToSqlValue(True)
   SQL = SQL & " OR articuloInventario.ExcluirDeComision IS NULL)"
    
   SQL = SQL & " AND factura.ConsecutivoCompania" & _
               " = " & valConsecutivoCompania
   SQL = SQL & " GROUP BY "
   SQL = SQL & "cliente.Nombre,"
   SQL = SQL & "factura.Numero, "
   SQL = SQL & "factura.Fecha, "
   SQL = SQL & "factura.Moneda, "
   SQL = SQL & "factura.CambioABolivares, "
   SQL = SQL & "factura.TotalFactura,"
   SQL = SQL & "factura.TotalMontoExento, "
   SQL = SQL & "factura.TotalBaseImponible, "
   SQL = SQL & "Moneda.simbolo, "
   SQL = SQL & "vendedor.Codigo, "
   SQL = SQL & "vendedor.Nombre, "
   SQL = SQL & "vendedor.comisionPorVenta "
   
   If ReporteEnMonedaLocal Then
      SQL = SQL & " ORDER BY "
      SQL = SQL & "vendedor.Codigo, "
      SQL = SQL & "factura.Fecha, "
      SQL = SQL & "factura.Numero"
   Else
      SQL = SQL & " ORDER BY "
      SQL = SQL & "factura.Moneda, "
      SQL = SQL & "vendedor.Codigo, "
      SQL = SQL & "factura.Fecha, "
      SQL = SQL & "factura.Numero"
   End If
  
   fConstruirSQLPorVentasDelReporteComisionDeVendedorDetallado = SQL
   
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: SQL = ""
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fConstruirSQLPorVentasDelReporteComisionDeVendedorDetallado", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function


'
'Private Function fConstruirSQLPorVentasDelReporteComisionDeVendedor() As String
'   Dim Sql As String
'   Dim insCliente As clsClienteNavigator
'   Dim insFactura As clsFacturaNavigator
'   Dim insVendedor As clsVendedorNavigator
'   Dim insMoneda As clsMonedaNavigator
'   On Error GoTo h_ERROR
'   Set insCliente = New clsClienteNavigator
'   Set insFactura = New clsFacturaNavigator
'   Set insVendedor = New clsVendedorNavigator
'   Set insMoneda = New clsMonedaNavigator
'   Sql = "SELECT cliente.Nombre as NombreCliente, "
'   Sql = Sql & " 'Fact. ' + factura.Numero as NumeroDelDocumento, "
'   Sql = Sql & "factura.Fecha as FechaDelDocumento, "
'   Sql = Sql & "factura.Moneda as Moneda, "
'   Sql = Sql & "factura.CambioABolivares as CambioABolivares, "
'   Sql = Sql & gUtilSQL.DfSQLFormat(" (factura." & valNombreCampoTotalFactura & _
'               " * factura.CambioABolivares)", "#,##0.00", "TotalDelDocumento") & ", "
'   Sql = Sql & gUtilSQL.DfSQLFormat(" (factura." & valNombreCampoTotalMontoExento & _
'               " + factura.TotalBaseImponible)", "#,##0.00", "MontoComisionable") & ", "
'   Sql = Sql & "Moneda.simbolo as SimboloDeLaMoneda, "
'   Sql = Sql & "vendedor.Codigo as CodigoDelVendedor, "
'   Sql = Sql & "vendedor.Nombre as NombreDelVendedor, "
'   Sql = Sql & "vendedor.comisionPorVenta as PorcentajeDeComision "
'   Sql = Sql & " FROM vendedor INNER JOIN (Moneda"
'   Sql = Sql & " INNER JOIN (cliente INNER JOIN factura ON ("
'   Sql = Sql & "cliente.Codigo" & _
'               " = factura.CodigoCliente"
'   Sql = Sql & ") AND (cliente.ConsecutivoCompania" & _
'               " = factura.ConsecutivoCompania"
'   Sql = Sql & ")) ON Moneda.Nombre" & _
'               " = factura.Moneda"
'   Sql = Sql & ") ON (vendedor.Codigo" & _
'               " = factura.CodigoVendedor"
'   Sql = Sql & ") AND (vendedor.ConsecutivoCompania" & _
'               " = factura.ConsecutivoCompania)"
'   Sql = Sql & " WHERE factura.StatusFactura" & _
'               " =" & gUtilSQL.fSimpleSqlValue(gConvert.enumerativoAChar(eSF_EMITIDA))
'   Sql = Sql & " AND " & gUtilSQL.DfSQLDateValueBetween("factura.Fecha", valFechaInicial, valFechaFinal)
'   If ByVal valCantidadAImprimir As String <> "Todos" Then
'      Sql = Sql & " AND " & gUtilSQL.fSQLValue("vendedor.Nombre", valNombreDelVendedor, False)
'   End If
'   Sql = Sql & " AND factura.ConsecutivoCompania" & _
'               " = " & valConsecutivoCompania
'   Sql = Sql & " ORDER BY vendedor.Codigo, "
'   Sql = Sql & "factura.Fecha, "
'   Sql = Sql & "factura.Numero"
'   Set insCliente = Nothing
'   Set insFactura = Nothing
'   Set insVendedor = Nothing
'   Set insMoneda = Nothing
'   fConstruirSQLPorVentasDelReporteComisionDeVendedor = Sql
'h_EXIT: On Error GoTo 0
'   Exit Function
'h_ERROR: Sql = ""
'   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
'         "fConstruirSQLPorVentasDelReporteComisionDeVendedor", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
'End Function
'
'
Public Function fConstruirSQLPorVentasTotalesDeCadaVendedor(ByVal valNombreCampoTotalMontoExento As String, ByVal valNombreCampoTotalFactura As String, ByVal mUsarCambioOriginal As Boolean, ByRef gUltimaTasaDeCambio As Object, _
                                                               ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valConsecutivoCompania As Long, ByVal sqlSumaTotalRenglonVentas As String, ByVal valCantidadAImprimir As String, ByVal valNombreDeVendedor As String) As String
   Dim SQL As String
   Dim sqlMonto As String
   Dim sqlSumaTotalRenglon  As String
   Dim SQLMontoComisionable As String
   Dim sqlPorcentajeDeComision As String
   Dim sqlMoneda As String
   Dim sqlCambio As String
   Dim sqlTotalFactura As String
   On Error GoTo h_ERROR
   sqlMoneda = "factura.Moneda"
   sqlCambio = "factura.CambioABolivares"
   SQLMontoComisionable = "(factura." & valNombreCampoTotalMontoExento & _
                        " + factura.TotalBaseImponible)"
   SQLMontoComisionable = gUtilSQL.DfCDecSQL(SQLMontoComisionable)
   sqlPorcentajeDeComision = "(vendedor.comisionPorVenta/100)"
   sqlPorcentajeDeComision = gUtilSQL.DfCDecSQL(sqlPorcentajeDeComision)
   sqlMonto = "(" & SQLMontoComisionable & " * " & sqlPorcentajeDeComision & ")"
   sqlMonto = gUtilSQL.DfCDecSQL(sqlMonto)
   sqlTotalFactura = "factura." & valNombreCampoTotalFactura
   sqlTotalFactura = gUtilSQL.DfCDecSQL(sqlTotalFactura)
   sqlSumaTotalRenglon = fSQLGenerarElTotalTotalRenglonVentas("factura.TipoDeDocumento", False, sqlSumaTotalRenglonVentas, valConsecutivoCompania)
   sqlSumaTotalRenglon = gUtilSQL.DfCDecSQL(sqlSumaTotalRenglon)
      SQLMontoComisionable = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCambio, sqlMoneda, SQLMontoComisionable, mUsarCambioOriginal, "")
      sqlMonto = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCambio, sqlMoneda, sqlMonto, mUsarCambioOriginal, "")
      sqlMonto = gUtilSQL.fSQLTruncaA2Decimales(sqlMonto)
      sqlTotalFactura = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCambio, sqlMoneda, sqlTotalFactura, mUsarCambioOriginal, "")
      sqlSumaTotalRenglon = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCambio, sqlMoneda, sqlSumaTotalRenglon, mUsarCambioOriginal, "")
   'sqlSumaTotalRenglon = gUtilSQL.DfCDecSQL(sqlSumaTotalRenglon)
   sqlSumaTotalRenglon = gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull(sqlSumaTotalRenglon), 0, sqlSumaTotalRenglon, True)
   SQL = "SELECT "
   SQL = SQL & "(SUM(" & SQLMontoComisionable & ")-" & sqlSumaTotalRenglon & ") AS MontoComisionable, "
   SQL = SQL & "SUM(" & sqlTotalFactura & ") AS TotalDelDocumento, "
   SQL = SQL & sqlPorcentajeDeComision & " AS PorcentajeDeComision, "
   SQL = SQL & "vendedor.Codigo AS CodigoDelVendedor, "
   SQL = SQL & "vendedor.Nombre AS NombreDelVendedor "
   SQL = SQL & " FROM cliente INNER JOIN ("
   SQL = SQL & "factura INNER JOIN vendedor ON "
   SQL = SQL & "factura.ConsecutivoCompania = vendedor.ConsecutivoCompania"
   SQL = SQL & " AND factura.CodigoVendedor = vendedor.Codigo"
   SQL = SQL & ") ON cliente.ConsecutivoCompania = factura.ConsecutivoCompania"
   SQL = SQL & " AND cliente.Codigo = factura.CodigoCliente"
   SQL = SQL & " WHERE factura.StatusFactura=" & gUtilSQL.fSimpleSqlValue(gConvert.enumerativoAChar(eSF_EMITIDA))
   SQL = SQL & " AND " & gUtilSQL.DfSQLDateValueBetween("factura.Fecha", valFechaInicial, valFechaFinal)
    If valCantidadAImprimir <> "Todos" Then
      SQL = SQL & " AND " & gUtilSQL.fSQLValue("vendedor.Nombre", valNombreDeVendedor, False)
   End If
  
   SQL = SQL & " AND factura.ConsecutivoCompania = " & valConsecutivoCompania
   SQL = SQL & " GROUP BY vendedor.Codigo, "
   SQL = SQL & "vendedor.Nombre, "
   SQL = SQL & "factura.Moneda, "
   SQL = SQL & "vendedor.comisionPorVenta, "
   SQL = SQL & "factura.CambioABolivares"
   fConstruirSQLPorVentasTotalesDeCadaVendedor = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: SQL = ""
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fConstruirSQLPorVentasTotalesDeCadaVendedor", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
'
Public Function fConstruirSQLPorCobrosDelReporteComisionDeVendedorDetallado(ByVal mUsarCambioOriginal As Boolean, ByRef gUltimaTasaDeCambio As Object, ByVal ReporteEnMonedaLocal As Boolean, _
                                                            ByRef gMonedaLocalActual As Object, ByVal valAsignarComisionDeVendedorEnCobranza As Boolean, ByVal valEsMontoBruto As Boolean, ByVal valConsecutivoCompania As Long, _
                                                            ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valCantidadAImprimir As String, ByVal valNombreDelVendedor As String) As String
   Dim SQL As String
   Dim sqlMtoTotal As String
   Dim sqlMontoAbonado As String
   Dim sqlMontoExento As String
   Dim sqlMontoIva As String
   Dim sqlMontoGravado As String
   Dim SQLMontoAbonadoBs As String
   Dim SQLMontoExentoBs As String
   Dim SQLMontoIvaBs As String
   Dim SQLMontoGravadoBs As String
   Dim SQLMontoAbonadoCob As String
   Dim SQLMontoExentoCob As String
   Dim SQLMontoIvaCob As String
   Dim SQLMontoGravadoCob As String
   Dim sqlMonedaCobro As String
   Dim sqlCambio As String
   Dim SQLMontoDesc As String
   Dim SQLMontoDescBS As String
   Dim sqlCambioCxC As String
   Dim sqlCodigoMonedaCxc As String
   Dim sqlNombreMonedaCxc As String
   Dim sqlMonedaSimbolo As String
   Dim sqlFechaCobranza As String
   Dim sqlCambioCxCBs As String
   Dim sqlMontoComision As String
   Dim sqlMontoComisionBs As String
   Dim sqlCodigoMonedaCobro As String
   Dim sqlCambioABsDeMonedaCobranza As String
   Dim SQLMontoComisionable As String
   On Error GoTo h_ERROR
   sqlCodigoMonedaCobro = "cobranza.CodigoMoneda"
   sqlMonedaCobro = "cobranza.Moneda"
   sqlCambio = "cobranza.CambioABolivares"
   sqlMontoAbonado = "documentoCobrado.MontoAbonado"
   sqlMontoIva = gUtilSQL.getIIF("cxC.MontoIVA=0", "1", "cxC.MontoIVA", True)
   sqlMontoExento = "cxC.MontoExento"
   sqlMontoGravado = "cxC.MontoGravado"
   sqlCambioCxC = "documentoCobrado.CambioAMonedaLocal"
   sqlCambioCxCBs = "cxC.CambioABolivares"
   sqlCodigoMonedaCxc = "documentoCobrado.CodigoMonedaDeCxC"
   sqlNombreMonedaCxc = "cxC.Moneda"
   sqlMonedaSimbolo = "documentoCobrado.SimboloMonedaDeCxC"
   sqlFechaCobranza = "Cobranza.Fecha"
   SQLMontoDesc = "cobranza.DescProntoPago - cobranza.RetencionIslr"
   SQLMontoAbonadoCob = "documentoCobrado.MontoAbonado"
   sqlCambioABsDeMonedaCobranza = "CASE WHEN documentoCobrado.CambioAMonedaLocal = 0 THEN 1 ELSE documentoCobrado.CambioAMonedaLocal END"
   If mUsarCambioOriginal Then
      sqlCambio = gMonedaLocalActual.fSQLConvierteMontoSiAplicaMasDecimales(gMonedaLocalActual.GetHoyCodigoMoneda, sqlCambio, "Cobranza.Fecha")
      sqlCambioCxCBs = gMonedaLocalActual.fSQLConvierteMontoSiAplicaMasDecimales(gMonedaLocalActual.GetHoyCodigoMoneda, sqlCambioCxCBs, "Cobranza.Fecha")
   Else
      sqlCambio = gUltimaTasaDeCambio.getUltimaTasaDeCambio("Cobranza.Moneda", False)
      sqlCambioCxCBs = gUltimaTasaDeCambio.getUltimaTasaDeCambio("cxC.Moneda", False)
   End If
    
   'SQLMontoAbonadoCob = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCambioCxC, sqlMonedaCobro, sqlMontoAbonado, True, "")
   SQLMontoIvaCob = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCambioCxC, sqlMonedaCobro, sqlMontoIva, True, "")
   SQLMontoExentoCob = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCambioCxC, sqlMonedaCobro, sqlMontoExento, True, "")
   SQLMontoGravadoCob = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCambioCxC, sqlMonedaCobro, sqlMontoGravado, True, "")
  
   'SQLMontoAbonadoCob = gMonedaLocalActual.fSQLConvierteMontoSiAplicaMasDecimales(sqlCodigoMonedaCobro, SQLMontoAbonadoCob, sqlFechaCobranza)
   SQLMontoIvaCob = gMonedaLocalActual.fSQLConvierteMontoSiAplicaMasDecimales(sqlCodigoMonedaCobro, SQLMontoIvaCob, sqlFechaCobranza)
   SQLMontoExentoCob = gMonedaLocalActual.fSQLConvierteMontoSiAplicaMasDecimales(sqlCodigoMonedaCobro, SQLMontoExentoCob, sqlFechaCobranza)
   SQLMontoGravadoCob = gMonedaLocalActual.fSQLConvierteMontoSiAplicaMasDecimales(sqlCodigoMonedaCobro, SQLMontoGravadoCob, sqlFechaCobranza)
'OJO Cambios DAVID
   SQLMontoAbonadoBs = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCambioABsDeMonedaCobranza, "Cobranza.Moneda", sqlMontoAbonado, mUsarCambioOriginal, "")
   
   SQLMontoIvaBs = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCambioCxCBs, "cxC.Moneda", sqlMontoIva, mUsarCambioOriginal, "")
   SQLMontoExentoBs = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCambioCxCBs, "cxC.Moneda", sqlMontoExento, mUsarCambioOriginal, "")
   SQLMontoGravadoBs = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCambioCxCBs, "cxC.Moneda", sqlMontoGravado, mUsarCambioOriginal, "")
   SQLMontoDescBS = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCambio, "Cobranza.moneda", SQLMontoDesc, mUsarCambioOriginal, "")
       
   If ReporteEnMonedaLocal Then
      sqlMontoAbonado = SQLMontoAbonadoBs
      sqlMontoIva = SQLMontoIvaBs
      sqlMontoExento = SQLMontoExentoBs
      sqlMontoGravado = SQLMontoGravadoBs
   End If
    
   sqlMtoTotal = "(" & sqlMontoExento & " + " & sqlMontoGravado & " + " & sqlMontoIva & ")"
   
   If valEsMontoBruto Then
      sqlMontoComision = "(" & SQLMontoAbonadoCob & " - ROUND((" & SQLMontoAbonadoCob & " / ROUND(( 1  + (" & SQLMontoExentoCob & " + " & SQLMontoGravadoCob & ")/" & SQLMontoIvaCob & "),3)),3))- ( " & SQLMontoDesc & ")"
      sqlMontoComisionBs = "(" & SQLMontoAbonadoBs & " - ROUND((" & SQLMontoAbonadoBs & " / ROUND(nullif( ( 1  + (" & SQLMontoExentoBs & " + " & SQLMontoGravadoBs & ")/" & SQLMontoIvaBs & "),1),3)),3)) - (" & SQLMontoDescBS & ")"
   Else
      sqlMontoComision = "(" & SQLMontoAbonadoCob & " - ROUND((" & SQLMontoAbonadoCob & " / ROUND(( 1  + (" & SQLMontoExentoCob & " + " & SQLMontoGravadoCob & ")/" & SQLMontoIvaCob & "),3)),3))"
      sqlMontoComisionBs = "(" & SQLMontoAbonadoBs & " - ROUND((" & SQLMontoAbonadoBs & " / ROUND(nullif( ( 1  + (" & SQLMontoExentoBs & " + " & SQLMontoGravadoBs & ")/" & SQLMontoIvaBs & "),1),3)),3))"
   End If
   
   SQLMontoComisionable = " (ROUND(documentoCobrado.MontoAbonadoEnMonedaOriginal  - CASE WHEN cxC.MontoIVA <> 0 THEN  (documentoCobrado.MontoAbonadoEnMonedaOriginal /(1 + ((cxC.MontoExento  +  cxC.MontoGravado) / cxC.MontoIVA))) ELSE 0 END ,3))   "
    
   If ReporteEnMonedaLocal And mUsarCambioOriginal Then
      SQLMontoComisionable = " (ROUND(documentoCobrado.MontoAbonadoEnMonedaOriginal  -  CASE WHEN cxC.MontoIVA <> 0 THEN  (documentoCobrado.MontoAbonadoEnMonedaOriginal /(1 + ((cxC.MontoExento  +  cxC.MontoGravado) / cxC.MontoIVA))) ELSE 0 END ,3) * ( DocumentoCobrado.CambioAMonedaLocal )) AS MontoComision"
   ElseIf ReporteEnMonedaLocal Then
   'Public Function fSQLCampoMontoPorTasaDeCambio(ByVal valFieldNameCambioABolivares As String, ByVal valFieldNameNombreMoneda As String, ByVal valFieldNameMonto As String, ByVal valUsarCambioOriginal As Boolean, ByVal valAlias As String) As String
      SQLMontoComisionable = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("DocumentoCobrado.CambioAMonedaLocal", "cxC.Moneda", SQLMontoComisionable, mUsarCambioOriginal, "MontoComision")
   End If
   
   
   SQL = "SELECT "
   SQL = SQL & "cliente.Nombre AS NombreCliente, " & vbCrLf
   SQL = SQL & sqlMonedaCobro & ", " & vbCrLf
   SQL = SQL & "cxC.FechaVencimiento AS FechaV, " & vbCrLf
   SQL = SQL & "cxC.Numero AS NumeroFactura, " & vbCrLf
   SQL = SQL & "cxC.Fecha AS FechaFactura, " & vbCrLf
   SQL = SQL & sqlMtoTotal & " AS MontoFactura, " & vbCrLf
   SQL = SQL & "vendedor.Codigo, " & vbCrLf
   SQL = SQL & "cobranza.Fecha, " & vbCrLf
   SQL = SQL & "cobranza.Numero, " & vbCrLf
   SQL = SQL & " ('Cob. ' + cobranza.Numero + '/ Fact. ' + CxC.NumeroDocumentoOrigen)  AS NumeroDelDocumento, " & vbCrLf
   SQL = SQL & "cobranza.Fecha AS FechaDelDocumento, " & vbCrLf
   SQL = SQL & sqlMonedaSimbolo & " AS SimboloDeLaMoneda, " & vbCrLf
   If ReporteEnMonedaLocal Then
      SQL = SQL & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda) & vbCrLf
   Else
      SQL = SQL & sqlMonedaCobro & vbCrLf
   End If
   SQL = SQL & " AS DescripcionDeLaMoneda, " & vbCrLf
   SQL = SQL & sqlCambioCxC & " As CambioABolivares, " & vbCrLf
   SQL = SQL & sqlMontoAbonado & " AS MontoAbonado, " & vbCrLf
   SQL = SQL & sqlMontoIva & " AS MontoIva, " & vbCrLf
   SQL = SQL & sqlMontoExento & " AS MontoExento, " & vbCrLf
   SQL = SQL & sqlMontoGravado & " AS MontoGravado, " & vbCrLf
   SQL = SQL & "vendedor.Codigo AS CodigoDelVendedor, " & vbCrLf
   SQL = SQL & "vendedor.Nombre AS NombreDelVendedor, " & vbCrLf
   If valAsignarComisionDeVendedorEnCobranza Then
      SQL = SQL & "cobranza.ComisionVendedor" & vbCrLf
   Else
      SQL = SQL & "vendedor.comisionPorCobro" & vbCrLf
   End If
   SQL = SQL & " AS PorcentajeDeComision, " & vbCrLf
   'REVISA
   If ReporteEnMonedaLocal Then
      SQL = SQL & vbCrLf & SQLMontoComisionable & ", " & vbCrLf & vbCrLf
   Else
      SQL = SQL & SQLMontoComisionable & " AS MontoComision, " & vbCrLf
   End If
   SQL = SQL & sqlMontoComisionBs & " AS MontoComisionBs" & vbCrLf

   If valAsignarComisionDeVendedorEnCobranza Then
      SQL = SQL & ", (cobranza.ComisionVendedor/100) * " & sqlMontoComision & " As ComisionVendedor" & vbCrLf
   End If
   SQL = SQL & " FROM ((vendedor INNER JOIN cobranza" & vbCrLf
   SQL = SQL & " ON (vendedor.Codigo" & _
               " = cobranza.CodigoCobrador" & vbCrLf
   SQL = SQL & ") AND (vendedor.ConsecutivoCompania" & _
               " = cobranza.ConsecutivoCompania" & vbCrLf
   SQL = SQL & ")) INNER JOIN (documentoCobrado INNER JOIN cxC" & vbCrLf
   SQL = SQL & " ON (documentoCobrado.ConsecutivoCompania = cxC.ConsecutivoCompania"
   SQL = SQL & ") AND (documentoCobrado.TipoDeDocumentoCobrado = cxC.TipoCxC"
   SQL = SQL & ") AND (documentoCobrado.NumeroDelDocumentoCobrado = cxC.Numero"
   SQL = SQL & ")) ON (cobranza.Numero = documentoCobrado.NumeroCobranza"
   SQL = SQL & ") AND (cobranza.ConsecutivoCompania = documentoCobrado.ConsecutivoCompania"
   SQL = SQL & ")) INNER JOIN cliente"
   SQL = SQL & " ON (cxC.CodigoCliente = cliente.Codigo"
   SQL = SQL & ") AND (cxC.ConsecutivoCompania = cliente.ConsecutivoCompania)"
   SQL = SQL & " WHERE cobranza.ConsecutivoCompania = " & valConsecutivoCompania
   SQL = SQL & " AND cobranza.StatusCobranza = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusCobranza.eSC_VIGENTE)
   SQL = SQL & " AND " & gUtilSQL.DfSQLDateValueBetween("cobranza.Fecha", valFechaInicial, valFechaFinal)
   SQL = SQL & " AND cxC.Origen = " & gUtilSQL.fSQLSimpleValueForEnum(enum_OrigenFacturacionOManual.eOF_MANUAL)
   SQL = SQL & " AND cxC.MontoAbonado + cxC.MontoIVA + cxC.MontoExento <> 0 "
   If valCantidadAImprimir <> "Todos" Then
      SQL = SQL & " AND vendedor.Nombre = " & gUtilSQL.fSimpleSqlValue(valNombreDelVendedor)
   End If
   fConstruirSQLPorCobrosDelReporteComisionDeVendedorDetallado = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: SQL = ""
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fConstruirSQLPorCobrosDelReporteComisionDeVendedorDetallado", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConstruirSQLPorCobrosDelReporteComisionDeVendedorPorDiasVencidoDetallado(ByVal ReporteEnMonedaLocal As Boolean, _
                              ByVal mUsarCambioOriginal As Boolean, ByRef gMonedaLocalActual As Object, ByRef gUltimaTasaDeCambio As Object, _
                              ByVal valConsecutivoCompania As Long, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, _
                              ByVal ValimprimirSoloUno As Boolean, ByVal valNombreDelVendedor As String, ByVal valMostarDiasTrasncurridos As Boolean) As String
   Dim sqlPorcXComision As String
   Dim SQL As String
   Dim sqlMtoTotal As String
   Dim sqlMontoAbonado As String
   Dim sqlMontoExento As String
   Dim sqlMontoIva As String
   Dim sqlMontoGravado As String
   Dim SQLMontoAbonadoBs As String
   Dim SQLMontoExentoBs As String
   Dim SQLMontoIvaBs As String
   Dim SQLMontoGravadoBs As String
   Dim sqlMonedaCobro As String
   Dim sqlCambio As String
   Dim sqlCambioCxC As String
   Dim sqlCodigoMonedaCxc As String
   Dim sqlNombreMonedaCxc As String
   Dim sqlMonedaSimbolo As String
   Dim sqlFechaCobranza As String
   Dim sqlCambioCxCBs As String
   Dim sqlMontoComision As String
   Dim sqlMontoComisionBs As String
   Dim sqlCodigoMonedaCobro As String
   Dim sqlDiasTranscurridos As String
   Dim SQLMontoAbonadoCob As String
   Dim SQLMontoIvaCob As String
   Dim SQLMontoExentoCob As String
   Dim SQLMontoGravadoCob As String
   Dim vSeparador  As String
   Dim vCadenaComparacion As String
   Dim vCadenaResultadoStr  As String
   Dim SQLMontoComisionable As String
   On Error GoTo h_ERROR
   sqlCodigoMonedaCobro = "cobranza.CodigoMoneda"
   sqlMonedaCobro = "cobranza.Moneda"
   sqlCambio = "cobranza.CambioABolivares"
   sqlMontoAbonado = "documentoCobrado.MontoAbonado"
   sqlMontoIva = "cxC.MontoIVA"
   sqlMontoExento = "cxC.MontoExento"
   sqlMontoGravado = "cxC.MontoGravado"
   sqlCambioCxC = "documentoCobrado.CambioAMonedaDeCobranza"
   sqlCambioCxCBs = "cxC.CambioABolivares"
   sqlCodigoMonedaCxc = "documentoCobrado.CodigoMonedaDeCxC"
   sqlNombreMonedaCxc = "cxC.Moneda"
   sqlMonedaSimbolo = "documentoCobrado.SimboloMonedaDeCxC"
   SQLMontoAbonadoCob = "documentoCobrado.MontoAbonado"
   sqlFechaCobranza = "Cobranza.Fecha"
   sqlDiasTranscurridos = gUtilSQL.fSQLDiasDeDistanciaEntre2Fechas("cxc.Fecha", "Cobranza.Fecha")
   If mUsarCambioOriginal Then
      sqlCambio = gMonedaLocalActual.fSQLConvierteMontoSiAplicaMasDecimales(gMonedaLocalActual.GetHoyCodigoMoneda, sqlCambio, "Cobranza.Fecha")
      sqlCambioCxCBs = gMonedaLocalActual.fSQLConvierteMontoSiAplicaMasDecimales(gMonedaLocalActual.GetHoyCodigoMoneda, sqlCambioCxCBs, "Cobranza.Fecha")
   Else
      sqlCambio = gUltimaTasaDeCambio.getUltimaTasaDeCambio("Cobranza.Moneda", False)
      sqlCambioCxCBs = gUltimaTasaDeCambio.getUltimaTasaDeCambio("cxC.Moneda", False)
   End If
   SQLMontoIvaCob = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCambioCxC, sqlMonedaCobro, sqlMontoIva, True, "")
   SQLMontoExentoCob = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCambioCxC, sqlMonedaCobro, sqlMontoExento, True, "")
   SQLMontoGravadoCob = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCambioCxC, sqlMonedaCobro, sqlMontoGravado, True, "")
   SQLMontoIvaCob = gMonedaLocalActual.fSQLConvierteMontoSiAplicaMasDecimales(sqlCodigoMonedaCobro, SQLMontoIvaCob, sqlFechaCobranza)
   SQLMontoExentoCob = gMonedaLocalActual.fSQLConvierteMontoSiAplicaMasDecimales(sqlCodigoMonedaCobro, SQLMontoExentoCob, sqlFechaCobranza)
   SQLMontoGravadoCob = gMonedaLocalActual.fSQLConvierteMontoSiAplicaMasDecimales(sqlCodigoMonedaCobro, SQLMontoGravadoCob, sqlFechaCobranza)
   SQLMontoAbonadoBs = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCambioCxCBs, "Cobranza.Moneda", sqlMontoAbonado, mUsarCambioOriginal, "")
   SQLMontoIvaBs = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCambioCxCBs, "Cobranza.Moneda", sqlMontoIva, mUsarCambioOriginal, "")
   SQLMontoExentoBs = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCambioCxCBs, "Cobranza.Moneda", sqlMontoExento, mUsarCambioOriginal, "")
   SQLMontoGravadoBs = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCambioCxCBs, "Cobranza.Moneda", sqlMontoGravado, mUsarCambioOriginal, "")
   sqlMontoComision = "(" & SQLMontoAbonadoCob & " - (" & SQLMontoAbonadoCob & " * (" & SQLMontoIvaCob & " / (" & SQLMontoIvaCob & " + " & SQLMontoExentoCob & " + " & SQLMontoGravadoCob & "))))"
   sqlMontoComisionBs = "(" & SQLMontoAbonadoBs & " - (" & SQLMontoAbonadoBs & " * (" & SQLMontoIvaBs & " / (" & SQLMontoIvaBs & " + " & SQLMontoExentoBs & " + " & SQLMontoGravadoBs & "))))"
   sqlPorcXComision = " (SELECT TOP 1 "
   sqlPorcXComision = sqlPorcXComision & "comisionCobranzaXVcto.PorcentajeComision"
   sqlPorcXComision = sqlPorcXComision & " FROM comisionCobranzaXVcto WHERE"
   sqlPorcXComision = sqlPorcXComision & " comisionCobranzaXVcto.ConsecutivoCompania"
   sqlPorcXComision = sqlPorcXComision & " = vendedor.ConsecutivoCompania"
   sqlPorcXComision = sqlPorcXComision & " AND (comisionCobranzaXVcto.PorcentajeComision"
   sqlPorcXComision = sqlPorcXComision & "> 0 OR " & gUtilSQL.DfCIntSQL("comisionCobranzaXVcto.DiasVencidos")
   sqlPorcXComision = sqlPorcXComision & ">0) AND " & gUtilSQL.DfCIntSQL("comisionCobranzaXVcto.DiasVencidos")
   sqlPorcXComision = sqlPorcXComision & " >=" & gUtilSQL.getDateDiff("d", "cxC.FechaVencimiento", "cobranza.Fecha")
   sqlPorcXComision = sqlPorcXComision & " ORDER BY comisionCobranzaXVcto.PorcentajeComision DESC, "
   sqlPorcXComision = sqlPorcXComision & "comisionCobranzaXVcto.DiasVencidos)"
   sqlMtoTotal = sqlMontoExento & " + " & sqlMontoGravado & " + " & sqlMontoIva
   vSeparador = gTexto.fSeparadorStandardDeElementosString
   vCadenaComparacion = vCadenaComparacion & "Cobranza.Moneda"
   vCadenaResultadoStr = vCadenaResultadoStr & "documentoCobrado.MontoAbonado" & vSeparador & sqlMtoTotal
   sqlMtoTotal = gUtilSQL.DfSQLCaseIf("cxC.CodigoMoneda", "Cobranza.Moneda", vCadenaResultadoStr, vSeparador, "<>", "")
   SQL = "SELECT "
   SQL = SQL & "cliente.Nombre AS NombreCliente, "
   SQL = SQL & " 'Cob. ' + cobranza.Numero AS NumeroDelDocumento, "
   SQL = SQL & "cobranza.Fecha AS FechaDelDocumento, "
   SQL = SQL & "cxC.FechaVencimiento AS FechaV, "
   SQL = SQL & "cxC.Numero AS NumeroFactura, "
   SQL = SQL & "cxC.Fecha AS FechaFactura, "
   SQL = SQL & sqlMtoTotal & " AS MontoFactura, "
   SQL = SQL & sqlMonedaSimbolo & " AS SimboloDeLaMoneda, "
   If ReporteEnMonedaLocal Then
      SQL = SQL & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda)
   Else
      SQL = SQL & sqlMonedaCobro
   End If
   SQL = SQL & " AS DescripcionDeLaMoneda, "
   SQL = SQL & sqlCambioCxC & " AS CambioABolivares, "
   SQL = SQL & sqlMontoAbonado & " AS MontoAbonado, "
   SQL = SQL & sqlMontoIva & " AS MontoIva, "
   SQL = SQL & sqlMontoExento & " AS MontoExento, "
   SQL = SQL & sqlMontoGravado & " AS MontoGravado, "
   SQL = SQL & "vendedor.Codigo AS CodigoDelVendedor, "
   SQL = SQL & "vendedor.Nombre AS NombreDelVendedor, "
   SQL = SQL & gUtilSQL.fIfResultIsNullReplace(sqlPorcXComision, "0") & " AS PorcentajedeComisionPorDiasVencidos, "
   SQL = SQL & gUtilSQL.fIfResultIsNullReplace("vendedor.comisionPorCobro", "0") & " AS PorcentajeDeComision, "
   SQL = SQL & sqlMontoComision & " AS MontoComision, "
   SQL = SQL & gUtilSQL.fIfResultIsNullReplace("(" & sqlMontoComision & ") * (" & sqlPorcXComision & "/100)", "0") & " AS MontoComisionablePorPorcentaje, "
   SQL = SQL & gUtilSQL.fIfResultIsNullReplace(sqlMontoComisionBs, "0") & " AS MontoComisionBs, "
   SQL = SQL & gUtilSQL.fIfResultIsNullReplace("(" & sqlMontoComisionBs & ") * (" & sqlPorcXComision & "/100)", "0") & " AS MontoComisionablePorPorcentajeBs"
   If valMostarDiasTrasncurridos Then
      SQL = SQL & ", " & sqlDiasTranscurridos & " As DiasTranscurridos"
   End If
   SQL = SQL & " FROM (vendedor INNER JOIN ("
   SQL = SQL & " cliente INNER JOIN cobranza ON ("
   SQL = SQL & " cliente.ConsecutivoCompania = cobranza.ConsecutivoCompania)"
   SQL = SQL & " AND (cliente.Codigo = cobranza.CodigoCliente))"
   SQL = SQL & " ON (vendedor.ConsecutivoCompania = cobranza.ConsecutivoCompania)"
   SQL = SQL & " AND (vendedor.Codigo = cobranza.CodigoCobrador))"
   SQL = SQL & " INNER JOIN (documentoCobrado INNER JOIN cxC"
   SQL = SQL & " ON (documentoCobrado.NumeroDelDocumentoCobrado = cxC.Numero)"
   SQL = SQL & " AND (documentoCobrado.TipoDeDocumentoCobrado = cxC.TipoCxC)"
   SQL = SQL & " AND (documentoCobrado.ConsecutivoCompania = cxC.ConsecutivoCompania))"
   SQL = SQL & " ON (cobranza.ConsecutivoCompania = documentoCobrado.ConsecutivoCompania)"
   SQL = SQL & " AND (cobranza.Numero = documentoCobrado.NumeroCobranza)"
   SQL = SQL & " WHERE cobranza.ConsecutivoCompania = " & valConsecutivoCompania
   SQL = SQL & " AND cobranza.StatusCobranza = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusCobranza.eSC_VIGENTE)
   SQL = SQL & " AND cxC.TipoCxC = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeTransaccion.eTD_FACTURA)
   SQL = SQL & " AND " & gUtilSQL.DfSQLDateValueBetween("cobranza.Fecha", valFechaInicial, valFechaFinal)
   If ValimprimirSoloUno Then
      SQL = SQL & " AND vendedor.Nombre = " & gUtilSQL.fSimpleSqlValue(valNombreDelVendedor)
   End If
   SQL = SQL & " ORDER BY "
   If Not ReporteEnMonedaLocal Then
      SQL = SQL & sqlMonedaCobro & ", "
   End If
   SQL = SQL & "vendedor.Codigo, "
   SQL = SQL & "cobranza.Fecha, "
   SQL = SQL & "cobranza.Numero"
   fConstruirSQLPorCobrosDelReporteComisionDeVendedorPorDiasVencidoDetallado = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: SQL = ""
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fConstruirSQLPorCobrosDelReporteComisionDeVendedorPorDiasVencidoDetallado", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

'Private Function fConstruirSQLPorCobrosDelReporteComisionDeVendedor() As String
'   Dim Sql As String
'   Dim insCliente As clsClienteNavigator
'   Dim insVendedor As clsVendedorNavigator
'   Dim insDocCobrado As clsDocumentoCobradoNavigator
'   Dim insCobranza As clsCobranzaNavigator
'   Dim insCxC As clsCxCNavigator
'   Dim insMoneda As clsMonedaNavigator
'   On Error GoTo h_ERROR
'   Set insCliente = New clsClienteNavigator
'   Set insVendedor = New clsVendedorNavigator
'   Set insDocCobrado = New clsDocumentoCobradoNavigator
'   Set insCobranza = New clsCobranzaNavigator
'   Set insCxC = New clsCxCNavigator
'   Set insMoneda = New clsMonedaNavigator
'   Sql = "SELECT "
'   Sql = Sql & "cliente.Nombre as NombreCliente,"
'   Sql = Sql & " 'Cob. ' + cobranza.Numero as NumeroDelDocumento,"
'   Sql = Sql & "cobranza.Fecha as FechaDelDocumento,"
'   Sql = Sql & "cobranza.Moneda as Moneda, "
'   Sql = Sql & "Moneda.simbolo as SimboloDeLaMoneda, "
'   Sql = Sql & "cobranza.CambioABolivares as CambioABolivares, "
'   Sql = Sql & " (documentoCobrado.MontoAbonado" & _
'               " * cobranza.CambioABolivares) as TotalDelDocumento, "
' ' sql = sql & "documentoCobrado.MontoAbonado as TotalDelDocumento,"
'   Sql = Sql & " (documentoCobrado.MontoAbonado * (1 - (" & _
'               "cxC.MontoIVA / (" & _
'               "cxC.MontoExento + " & _
'               "cxC.MontoGravado + " & _
'               "cxC.MontoIVA)))) as MontoComisionable,"
'   Sql = Sql & "vendedor.Codigo as CodigoDelVendedor,"
'   Sql = Sql & "vendedor.Nombre as NombreDelVendedor,"
'   Sql = Sql & "vendedor.comisionPorCobro  as PorcentajeDeComision"
'   Sql = Sql & " FROM (vendedor INNER JOIN (Moneda INNER JOIN ("
'   Sql = Sql & "cliente INNER JOIN cobranza ON ("
'   Sql = Sql & "cliente.Codigo" & _
'               " = cobranza.CodigoCliente) AND ("
'   Sql = Sql & "cliente.ConsecutivoCompania" & _
'               " = cobranza.ConsecutivoCompania)) ON "
'   Sql = Sql & "Moneda.Nombre" & _
'               " = cobranza.Moneda) ON ("
'   Sql = Sql & "vendedor.Codigo" & _
'               " = cobranza.CodigoCobrador) AND ("
'   Sql = Sql & "vendedor.ConsecutivoCompania" & _
'               " = cobranza.ConsecutivoCompania)) INNER JOIN ("
'   Sql = Sql & "documentoCobrado INNER JOIN cxC ON ("
'   Sql = Sql & "documentoCobrado.ConsecutivoCompania" & _
'               " = cxC.ConsecutivoCompania) AND ("
'   Sql = Sql & "documentoCobrado.NumeroDelDocumentoCobrado" & _
'               " = cxC.Numero)) ON ("
'   Sql = Sql & "cobranza.Numero" & _
'               " = documentoCobrado.NumeroCobranza) AND ("
'   Sql = Sql & "cobranza.ConsecutivoCompania" & _
'               " = documentoCobrado.ConsecutivoCompania)"
'   Sql = Sql & " WHERE cobranza.StatusCobranza" & _
'               " = " & gUtilSQL.fSimpleSqlValue(gConvert.enumerativoAChar(enum_StatusCobranza.eSC_VIGENTE))
'   Sql = Sql & " AND " & gUtilSQL.DfSQLDateValueBetween("cobranza.Fecha", valFechaInicial, valFechaFinal)
'   If valCantidadAImprimir <> "Todos" Then
'      Sql = Sql & " AND " & gUtilSQL.fSQLValue("vendedor.Nombre", valNombreDelVendedor, False)
'   End If
'   Sql = Sql & " AND cobranza.ConsecutivoCompania" & _
'               " = " & valConsecutivoCompania
'   Sql = Sql & " ORDER BY vendedor.Codigo, "
'   Sql = Sql & "cobranza.Fecha, "
'   Sql = Sql & "cobranza.Numero"
'   Set insCliente = Nothing
'   Set insVendedor = Nothing
'   Set insDocCobrado = Nothing
'   Set insCobranza = Nothing
'   Set insCxC = Nothing
'   Set insMoneda = Nothing
'   fConstruirSQLPorCobrosDelReporteComisionDeVendedor = Sql
'h_EXIT: On Error GoTo 0
'   Exit Function
'h_ERROR: Sql = ""
'   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
'         "fConstruirSQLPorCobrosDelReporteComisionDeVendedorDetallado", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
'End Function
'
'Private Function fEjecutaElReportedetalladoPorCobro(ByVal OpcionesComisionesSobreCobranza As String, _
'                                                       ByRef refCobranzaporDiasVencidos As Boolean) As String
'   Dim Sql As String
'   Dim insCobranza As clsCobranzaNavigator
'   Dim insVendedor As clsVendedorNavigator
'   On Error GoTo h_ERROR
'   Set insCobranza = New clsCobranzaNavigator
'   Set insVendedor = New clsVendedorNavigator
'   refCobranzaporDiasVencidos = False
'   Select Case OpcionesComisionesSobreCobranza
'      Case gEnumProyecto.enumCalculoParaComisionesSobreCobranzaEnBaseAToString(eCP_MONTO)
'         Sql = "(" & fConstruirSQLPorCobrosDelReporteComisionDeVendedorDetallado & ")"
'         Sql = Sql & " UNION "
'         Sql = Sql & "(" & fConstruirSQLPorCobrosDelReporteComisionDeVendedorDetalladoOrigenFactura & ")"
'         Sql = Sql & " ORDER BY "
'         IfReporteEnMonedaLocal) Then
'            Sql = Sql & "vendedor.Codigo, "
'         Else
'            Sql = Sql & "cobranza.Moneda, "
'            Sql = Sql & "vendedor.Codigo, "
'         End If
'         Sql = Sql & "cobranza.Fecha, "
'         Sql = Sql & "cobranza.Numero"
'      Case gEnumProyecto.enumCalculoParaComisionesSobreCobranzaEnBaseAToString(eCP_DIASVENCIDOS)
'         refCobranzaporDiasVencidos = True
'         Sql = fConstruirSQLPorCobrosDelReporteComisionDeVendedorPorDiasVencidoDetallado
'         refCobranzaporDiasVencidos = True
'   End Select
'   fEjecutaElReportedetalladoPorCobro = Sql
'h_EXIT: On Error GoTo 0
'   Exit Function
'h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
'         "fEjecutaElReportedetalladoPorCobro", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
'End Function
'
'Private Function fEjecutaElReportedetalladoPorVentas(ByVal OpcionesComisionesSobreCobranza As String) As String
'   Dim Sql As String
'   On Error GoTo h_ERROR
'   Set insCobranzaSQL = New clsCobranzaSQL
'   Select Case OpcionesComisionesSobreCobranza
'      Case gEnumProyecto.enumCalculoParaComisionesSobreCobranzaEnBaseAToString(eCP_MONTO)
'         Sql = insCobranzaSQL.fConstruirSQLPorVentasDelReporteComisionDeVendedorDetallado
'      Case gEnumProyecto.enumCalculoParaComisionesSobreCobranzaEnBaseAToString(eCP_PORCENTAJE_POR_ARTICULO)
'         Sql = "(" & fConstruirSQLPorVentasDelReporteComisionDeVendedorDetalladoPoArticulo(mUsarCambioOriginal) & ")"
'         Sql = Sql & " UNION "
'         Sql = Sql & "(" & fConstruirSQLPorVentasDelReporteComisionDeVendedorDetalladoPoArticuloDeOtrosCargos(mUsarCambioOriginal) & ")"
'         Sql = Sql & " ORDER BY CodigoVendedor,ColumnaParaGrupoTipoReporte,CodigoArticulo, FechaDeFactura, NumeroDeFactura"
'   End Select
'   fEjecutaElReportedetalladoPorVentas = Sql
'h_EXIT: On Error GoTo 0
'   Exit Function
'h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
'         "fEjecutaElReportedetalladoPorVentas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
'End Function


Public Function fConstruirSQLPorVentasDelReporteComisionDeVendedorDetalladoPoArticulo(ByVal valUsarCambioOriginal As Boolean, ByVal ReporteEnMonedaLocal As Boolean, ByRef gUltimaTasaDeCambio As Object, _
                                                         ByVal valConsecutivoCompania As Long, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByRef gMonedaLocalActual As Object, ByVal valCantidadAImprimir As String, ByVal valNombreDeVendedor As String) As String
   Dim SQL As String
   Dim sqlPrecioSinIva As String
   Dim sqlPrecioConIva As String
   Dim sqlMoneda As String
   Dim SqlTasaDeCambio As String
   Dim sqlMonedaReporte As String
   On Error GoTo h_ERROR
   sqlPrecioConIva = "renglonFactura.PrecioConIVA"
   sqlPrecioSinIva = "renglonFactura.PrecioSinIVA"
   sqlMoneda = "factura.Moneda"
   SqlTasaDeCambio = "factura.CambioABolivares"
   sqlMonedaReporte = sqlMoneda
   
   If ReporteEnMonedaLocal Then
      If valUsarCambioOriginal Then
         SqlTasaDeCambio = gMonedaLocalActual.fSQLConvierteMontoSiAplicaMasDecimales(gMonedaLocalActual.GetHoyCodigoMoneda, SqlTasaDeCambio, "factura.Fecha")
      Else
         SqlTasaDeCambio = gUltimaTasaDeCambio.getUltimaTasaDeCambio(sqlMoneda, False)
      End If
         sqlPrecioConIva = gUtilSQL.getIIF("factura.CodigoMoneda = " & gMonedaLocalActual.fSQLCodigoMonedaALaFecha("factura.Fecha"), _
                     gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, sqlPrecioConIva, "factura.Fecha"), _
                     gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(SqlTasaDeCambio, sqlMoneda, sqlPrecioConIva, valUsarCambioOriginal, ""), True)
         
         sqlPrecioSinIva = gUtilSQL.getIIF("factura.CodigoMoneda = " & gMonedaLocalActual.fSQLCodigoMonedaALaFecha("factura.Fecha"), _
                     gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, sqlPrecioSinIva, "factura.Fecha"), _
                     gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(SqlTasaDeCambio, sqlMoneda, sqlPrecioSinIva, valUsarCambioOriginal, ""), True)
         
         sqlMonedaReporte = gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda)

'         sqlPrecioConIva = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(SqlTasaDeCambio, sqlMoneda, sqlPrecioConIva, valUsarCambioOriginal, "")
'         sqlPrecioSinIva = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(SqlTasaDeCambio, sqlMoneda, sqlPrecioSinIva, valUsarCambioOriginal, "")
   End If
   SQL = "SELECT "
   SQL = SQL & gUtilSQL.fSimpleSqlValue("Articulo") & " AS ColumnaParaGrupoTipoReporte, "
   SQL = SQL & "vendedor.Codigo AS CodigoVendedor, "
   SQL = SQL & "vendedor.Nombre AS NombreVendedor, "
   SQL = SQL & "renglonFactura.Articulo AS CodigoArticulo1, "
   SQL = SQL & "factura.Numero AS NumeroDeFactura, "
   SQL = SQL & sqlMonedaReporte & " AS MonedaDeFactura, "
   SQL = SQL & "factura.Fecha AS FechaDeFactura, "
   SQL = SQL & "factura.CodigoCliente AS CodigoDelCliente, "
   SQL = SQL & "cliente.Nombre AS NombreDelCliente, "
   SQL = SQL & "cliente.Contacto AS Contacto, "
   SQL = SQL & "renglonFactura.Descripcion AS DescripcionArticulo, "
   SQL = SQL & sqlPrecioConIva & "* renglonFactura.Cantidad " & " AS PrecioConIVA, "
   SQL = SQL & "(" & sqlPrecioSinIva & "* renglonFactura.Cantidad " & "-( " & sqlPrecioSinIva & "* renglonFactura.Cantidad  * (renglonFactura.PorcentajeDescuento)/100 ) )" & "- ((" & sqlPrecioSinIva & "* renglonFactura.Cantidad " & "-( " & sqlPrecioSinIva & "* renglonFactura.Cantidad  * (renglonFactura.PorcentajeDescuento)/100 ) )*(Factura.PorcentajeDescuento)/100 )" & " AS PrecioSinIVA, "
   SQL = SQL & "renglonFactura.Cantidad as Cantidad, "
   SQL = SQL & "renglonFactura.Articulo AS CodigoArticulo, "
   SQL = SQL & "articuloInventario.PorcentajeComision AS PorcentajeComision"
   SQL = SQL & " FROM ((vendedor"
   SQL = SQL & " INNER JOIN factura"
   SQL = SQL & " ON (vendedor.Codigo"
   SQL = SQL & " = factura.CodigoVendedor)"
   SQL = SQL & " AND (vendedor.ConsecutivoCompania"
   SQL = SQL & " = factura.ConsecutivoCompania))"
   SQL = SQL & " INNER JOIN (articuloInventario"
   SQL = SQL & " INNER JOIN renglonFactura"
   SQL = SQL & " ON (articuloInventario.Codigo"
   SQL = SQL & " = renglonFactura.Articulo)"
   SQL = SQL & " AND (articuloInventario.ConsecutivoCompania"
   SQL = SQL & " = renglonFactura.ConsecutivoCompania))"
   SQL = SQL & " ON (factura.TipoDeDocumento"
   SQL = SQL & " = renglonFactura.TipoDeDocumento)"
   SQL = SQL & " AND (factura.Numero"
   SQL = SQL & " = renglonFactura.NumeroFactura)"
   SQL = SQL & " AND (factura.ConsecutivoCompania"
   SQL = SQL & " = renglonFactura.ConsecutivoCompania))"
   SQL = SQL & " INNER JOIN cliente"
   SQL = SQL & " ON (cliente.Codigo"
   SQL = SQL & " = factura.CodigoCliente)"
   SQL = SQL & " AND (cliente.ConsecutivoCompania"
   SQL = SQL & " = factura.ConsecutivoCompania)"
   SQL = SQL & " WHERE factura.ConsecutivoCompania" & _
               " = " & valConsecutivoCompania
   SQL = SQL & " AND " & gUtilSQL.DfSQLDateValueBetween("factura." & _
         "Fecha", valFechaInicial, valFechaFinal)
   SQL = SQL & " AND factura.TipoDeDocumento" _
            & " <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_RESUMENDIARIODEVENTAS)
   SQL = SQL & " AND (factura.StatusFactura" & _
                  " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_EMITIDA) & _
                  ")"
   If valCantidadAImprimir <> "Todos" Then
      SQL = SQL & " AND " & gUtilSQL.fSQLValue("vendedor.Nombre", valNombreDeVendedor, False)
   End If
    SQL = SQL & " AND (articuloInventario.ExcluirDeComision <> " & gUtilSQL.fBooleanToSqlValue(True) & ")"
   fConstruirSQLPorVentasDelReporteComisionDeVendedorDetalladoPoArticulo = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fConstruirSQLPorVentasDelReporteComisionDeVendedorDetalladoPoArticulo", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConstruirSQLPorVentasDelReporteComisionDeVendedorDetalladoPoArticuloDeOtrosCargos(ByVal valUsarCambioOriginal As Boolean, ByVal ReporteEnMonedaLocal As Boolean, _
                                                                                       ByRef gMonedaLocalActual As Object, ByRef gUltimaTasaDeCambio As Object, ByVal valConsecutivoCompania As Long, _
                                                                                       ByVal valFechaInicial As Date, ByVal valFechaFinal As Date) As String
   Dim SQL As String
   Dim sqlPrecioSinIva As String
   Dim sqlPrecioConIva As String
   Dim sqlMoneda As String
   Dim SqlTasaDeCambio As String
   Dim sqlMonedaReporte As String
   On Error GoTo h_ERROR
   sqlPrecioConIva = "renglonDetalleDeOtrosCargosFactura.TotalRenglon"
   sqlPrecioSinIva = "renglonDetalleDeOtrosCargosFactura.TotalRenglon"
   sqlMoneda = "factura.Moneda"
   SqlTasaDeCambio = "factura.CambioABolivares"
   sqlMonedaReporte = sqlMoneda
   If ReporteEnMonedaLocal Then
      If valUsarCambioOriginal Then
         SqlTasaDeCambio = gMonedaLocalActual.fSQLConvierteMontoSiAplicaMasDecimales(gMonedaLocalActual.GetHoyCodigoMoneda, SqlTasaDeCambio, "factura.Fecha")
      Else
         SqlTasaDeCambio = gUltimaTasaDeCambio.getUltimaTasaDeCambio(sqlMoneda, False)
      End If
         sqlPrecioConIva = gUtilSQL.getIIF("factura.CodigoMoneda = " & gMonedaLocalActual.fSQLCodigoMonedaALaFecha("factura.Fecha"), _
                     gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, sqlPrecioConIva, "factura.Fecha"), _
                     gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(SqlTasaDeCambio, sqlMoneda, sqlPrecioConIva, valUsarCambioOriginal, ""), True)
  
         sqlPrecioSinIva = gUtilSQL.getIIF("factura.CodigoMoneda = " & gMonedaLocalActual.fSQLCodigoMonedaALaFecha("factura.Fecha"), _
                     gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, sqlPrecioSinIva, "factura.Fecha"), _
                     gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(SqlTasaDeCambio, sqlMoneda, sqlPrecioSinIva, valUsarCambioOriginal, ""), True)
         
         sqlMonedaReporte = gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda)
   End If
   
'   If ReporteEnMonedaLocal Then
'         sqlPrecioConIva = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(SqlTasaDeCambio, sqlMoneda, sqlPrecioConIva, valUsarCambioOriginal, "")
'         sqlPrecioSinIva = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(SqlTasaDeCambio, sqlMoneda, sqlPrecioSinIva, valUsarCambioOriginal, "")
'   End If

   SQL = "SELECT "
   SQL = SQL & gUtilSQL.fSimpleSqlValue("Otros Cargos Factura") & " AS ColumnaParaGrupoTipoReporte, "
   SQL = SQL & "vendedor.Codigo AS CodigoVendedor,"
   SQL = SQL & "vendedor.Nombre AS NombreVendedor,"
   SQL = SQL & "renglonDetalleDeOtrosCargosFactura.CodigoDeCargo AS CodigoArticulo1,"
   SQL = SQL & "factura.Numero AS NumeroDeFactura, "
   SQL = SQL & sqlMonedaReporte & " AS MonedaDeFactura, "
   SQL = SQL & "factura.Fecha AS FechaDeFactura, "
   SQL = SQL & "factura.CodigoCliente AS CodigoDelCliente,"
   SQL = SQL & "cliente.Nombre AS NombreDelCliente, "
   SQL = SQL & "cliente.Contacto AS Contacto, "
   SQL = SQL & "renglonDetalleDeOtrosCargosFactura.Descripcion AS DescripcionArticulo, "
   SQL = SQL & sqlPrecioConIva & " AS PrecioConIVA,"
   SQL = SQL & sqlPrecioSinIva & " AS PrecioSinIVA,"
   SQL = SQL & "0 ,"
   SQL = SQL & "renglonDetalleDeOtrosCargosFactura.CodigoDeCargo AS CodigoArticulo,"
   SQL = SQL & "otrosCargosDeFactura.PorcentajeComision AS PorcentajeComision"
   SQL = SQL & " FROM vendedor"
   SQL = SQL & " INNER JOIN (cliente"
   SQL = SQL & " INNER JOIN (factura"
   SQL = SQL & " INNER JOIN (otrosCargosDeFactura"
   SQL = SQL & " INNER JOIN renglonDetalleDeOtrosCargosFactura"
   SQL = SQL & " ON (otrosCargosDeFactura.Codigo"
   SQL = SQL & " = renglonDetalleDeOtrosCargosFactura.CodigoDeCargo"
   SQL = SQL & ") AND (otrosCargosDeFactura.ConsecutivoCompania"
   SQL = SQL & " = renglonDetalleDeOtrosCargosFactura.ConsecutivoCompania"
   SQL = SQL & ")) ON (factura.TipoDeDocumento"
   SQL = SQL & " = renglonDetalleDeOtrosCargosFactura.TipoDeDocumento"
   SQL = SQL & ") AND (factura.Numero"
   SQL = SQL & " = renglonDetalleDeOtrosCargosFactura.NumeroFactura"
   SQL = SQL & ") AND (factura.ConsecutivoCompania"
   SQL = SQL & " = renglonDetalleDeOtrosCargosFactura.ConsecutivoCompania"
   SQL = SQL & ")) ON (cliente.Codigo"
   SQL = SQL & " = factura.CodigoCliente"
   SQL = SQL & ") AND (cliente.ConsecutivoCompania"
   SQL = SQL & " = factura.ConsecutivoCompania"
   SQL = SQL & ")) ON (vendedor.Codigo"
   SQL = SQL & " = factura.CodigoVendedor"
   SQL = SQL & ") AND (vendedor.ConsecutivoCompania"
   SQL = SQL & " = factura.ConsecutivoCompania"
   SQL = SQL & ") AND (vendedor.ConsecutivoCompania"
   SQL = SQL & " = cliente.ConsecutivoCompania)"
   SQL = SQL & " WHERE factura.ConsecutivoCompania" & _
               " = " & valConsecutivoCompania
   SQL = SQL & " AND " & gUtilSQL.DfSQLDateValueBetween("factura." & _
         "Fecha", valFechaInicial, valFechaFinal)
   SQL = SQL & " AND factura.TipoDeDocumento" _
            & " <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_RESUMENDIARIODEVENTAS)
   SQL = SQL & " AND (factura.StatusFactura" & _
                  " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA) & _
                  " OR factura.StatusFactura" & _
                  " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_EMITIDA) & _
                  ")"
   SQL = SQL & " AND otrosCargosDeFactura.ExcluirDeComision" _
            & " = " & gUtilSQL.fBooleanToSqlValue(False)
   fConstruirSQLPorVentasDelReporteComisionDeVendedorDetalladoPoArticuloDeOtrosCargos = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fConstruirSQLPorVentasDelReporteComisionDeVendedorDetalladoPoArticuloDeOtrosCargos", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
'
'Private Function fEjecutaElReportedetalladoPorVentasResumidas(ByVal OpcionesComisionesSobreCobranza As String) As String
'   Dim Sql As String
'   On Error GoTo h_ERROR
'   Select Case OpcionesComisionesSobreCobranza
'      Case gEnumProyecto.enumCalculoParaComisionesSobreCobranzaEnBaseAToString(eCP_MONTO)
'         Sql = fConstruirSQLPorVentasTotalesDeCadaVendedor
'      Case gEnumProyecto.enumCalculoParaComisionesSobreCobranzaEnBaseAToString(eCP_PORCENTAJE_POR_ARTICULO)
'         Sql = "(" & fConstruirSQLPorVentasDelReporteComisionDeVendedorResumidoPoArticulo(mUsarCambioOriginal) & ")"
'         Sql = Sql & " UNION "
'         Sql = Sql & "(" & fConstruirSQLPorVentasDelReporteComisionDeVendedorResumidoPoArticuloDeOtrosCargos(mUsarCambioOriginal) & ")"
'         Sql = Sql & " ORDER BY CodigoVendedor,ColumnaParaGrupoTipoReporte,CodigoArticulo"
'   End Select
'   fEjecutaElReportedetalladoPorVentasResumidas = Sql
'h_EXIT: On Error GoTo 0
'   Exit Function
'h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
'         "fEjecutaElReportedetalladoPorVentasResumidas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
'End Function
'
'Private Function fConstruirSQLPorVentasDelReporteComisionDeVendedorResumidoPoArticulo(ByVal valUsarCambioOriginal As Boolean) As String
'   Dim Sql As String
'   Dim insRenglonFactura As clsRenglonFacturaNavigator
'   Dim insFactura As clsFacturaNavigator
'   Dim insVendedor As clsVendedorNavigator
'   Dim insArticuloInventario As clsArticuloInventarioNavigator
'   Dim insCliente As clsClienteNavigator
'   Dim sqlPrecioSinIva As String
'   Dim sqlPrecioConIva As String
'   Dim sqlMoneda As String
'   Dim SqlTasaDeCambio As String
'   On Error GoTo h_ERROR
'   Set insRenglonFactura = New clsRenglonFacturaNavigator
'   Set insFactura = New clsFacturaNavigator
'   Set insVendedor = New clsVendedorNavigator
'   Set insArticuloInventario = New clsArticuloInventarioNavigator
'   Set insCliente = New clsClienteNavigator
'   insFactura.setClaseDeTrabajo eCTFC_Factura
'   sqlPrecioConIva = "renglonFactura.PrecioConIVA"
'   sqlPrecioSinIva = "renglonFactura.PrecioSinIVA"
'   sqlMoneda = "factura.Moneda"
'   SqlTasaDeCambio = "factura.CambioABolivares"
'   If ReporteEnMonedaLocal Then
'         sqlPrecioConIva = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(SqlTasaDeCambio, sqlMoneda, sqlPrecioConIva, valUsarCambioOriginal, "")
'         sqlPrecioSinIva = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(SqlTasaDeCambio, sqlMoneda, sqlPrecioSinIva, valUsarCambioOriginal, "")
'   End If
'   Sql = "SELECT "
'   Sql = Sql & gUtilSQL.fSimpleSqlValue("Articulo") & " AS ColumnaParaGrupoTipoReporte, "
'   Sql = Sql & "vendedor.Codigo AS CodigoVendedor,"
'   Sql = Sql & "vendedor.Nombre AS NombreVendedor,"
'   Sql = Sql & "renglonFactura.Articulo AS CodigoArticulo,"
'   Sql = Sql & "factura.Moneda AS MonedaDeFactura, "
'   Sql = Sql & "renglonFactura.Descripcion AS DescripcionArticulo, "
'   Sql = Sql & "articuloInventario.PorcentajeComision AS PorcentajeComision,"
'   Sql = Sql & "SUM(" & sqlPrecioConIva & ") AS PrecioConIVA,"
'   Sql = Sql & "SUM(" & sqlPrecioSinIva & ") AS PrecioSinIVA"
'   Sql = Sql & " FROM ((vendedor"
'   Sql = Sql & " INNER JOIN factura"
'   Sql = Sql & " ON (vendedor.Codigo"
'   Sql = Sql & " = factura.CodigoVendedor)"
'   Sql = Sql & " AND (vendedor.ConsecutivoCompania"
'   Sql = Sql & " = factura.ConsecutivoCompania))"
'   Sql = Sql & " INNER JOIN (articuloInventario"
'   Sql = Sql & " INNER JOIN renglonFactura"
'   Sql = Sql & " ON (articuloInventario.Codigo"
'   Sql = Sql & " = renglonFactura.Articulo)"
'   Sql = Sql & " AND (articuloInventario.ConsecutivoCompania"
'   Sql = Sql & " = renglonFactura.ConsecutivoCompania))"
'   Sql = Sql & " ON (factura.TipoDeDocumento"
'   Sql = Sql & " = renglonFactura.TipoDeDocumento)"
'   Sql = Sql & " AND (factura.Numero"
'   Sql = Sql & " = renglonFactura.NumeroFactura)"
'   Sql = Sql & " AND (factura.ConsecutivoCompania"
'   Sql = Sql & " = renglonFactura.ConsecutivoCompania))"
'   Sql = Sql & " INNER JOIN cliente"
'   Sql = Sql & " ON (cliente.Codigo"
'   Sql = Sql & " = factura.CodigoCliente)"
'   Sql = Sql & " AND (cliente.ConsecutivoCompania"
'   Sql = Sql & " = factura.ConsecutivoCompania)"
'   Sql = Sql & " WHERE factura.ConsecutivoCompania" & _
'               " = " & valConsecutivoCompania
'   Sql = Sql & " AND " & gUtilSQL.DfSQLDateValueBetween("factura." & _
'         "Fecha", valFechaInicial, valFechaFinal)
'   Sql = Sql & " AND factura.TipoDeDocumento" _
'            & " <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_RESUMENDIARIODEVENTAS)
'   Sql = Sql & " AND (factura.StatusFactura" & _
'                  " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA) & _
'                  " OR factura.StatusFactura" & _
'                  " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_EMITIDA) & _
'                  ")"
'   Sql = Sql & " GROUP BY "
'   Sql = Sql & "vendedor.Codigo,"
'   Sql = Sql & "vendedor.Nombre,"
'   Sql = Sql & "renglonFactura.Articulo,"
'   Sql = Sql & "factura.Moneda, "
'   Sql = Sql & "renglonFactura.Descripcion, "
'   Sql = Sql & "articuloInventario.PorcentajeComision"
'   Set insRenglonFactura = Nothing
'   Set insFactura = Nothing
'   Set insVendedor = Nothing
'   Set insArticuloInventario = Nothing
'   Set insCliente = Nothing
'   fConstruirSQLPorVentasDelReporteComisionDeVendedorResumidoPoArticulo = Sql
'h_EXIT: On Error GoTo 0
'   Exit Function
'h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
'      "fConstruirSQLPorVentasDelReporteComisionDeVendedorResumidoPoArticulo", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
'End Function
'
'Private Function fConstruirSQLPorVentasDelReporteComisionDeVendedorResumidoPoArticuloDeOtrosCargos(ByVal valUsarCambioOriginal As Boolean) As String
'   Dim Sql As String
'   Dim insRenglonOtrosCargosFacturaNav As clsRenglonOtrosCargosFacturaNav
'   Dim insFactura As clsFacturaNavigator
'   Dim insVendedor As clsVendedorNavigator
'   Dim insOtrosCargosDeFacturaNavigator As clsOtrosCargosDeFacturaNavigator
'   Dim insCliente As clsClienteNavigator
'   Dim sqlPrecioSinIva As String
'   Dim sqlPrecioConIva As String
'   Dim sqlMoneda As String
'   Dim SqlTasaDeCambio As String
'   On Error GoTo h_ERROR
'   Set insRenglonOtrosCargosFacturaNav = New clsRenglonOtrosCargosFacturaNav
'   Set insFactura = New clsFacturaNavigator
'   Set insVendedor = New clsVendedorNavigator
'   Set insOtrosCargosDeFacturaNavigator = New clsOtrosCargosDeFacturaNavigator
'   Set insCliente = New clsClienteNavigator
'   insFactura.setClaseDeTrabajo eCTFC_Factura
'   sqlPrecioConIva = "renglonDetalleDeOtrosCargosFactura.TotalRenglon"
'   sqlPrecioSinIva = "renglonDetalleDeOtrosCargosFactura.TotalRenglon"
'   sqlMoneda = "factura.Moneda"
'   SqlTasaDeCambio = "factura.CambioABolivares"
'   If ReporteEnMonedaLocal Then
'         sqlPrecioConIva = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(SqlTasaDeCambio, sqlMoneda, sqlPrecioConIva, valUsarCambioOriginal, "")
'         sqlPrecioSinIva = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(SqlTasaDeCambio, sqlMoneda, sqlPrecioSinIva, valUsarCambioOriginal, "")
'   End If
'   Sql = "SELECT "
'   Sql = Sql & gUtilSQL.fSimpleSqlValue("Otros Cargos Factura") & " AS ColumnaParaGrupoTipoReporte, "
'   Sql = Sql & "vendedor.Codigo AS CodigoVendedor,"
'   Sql = Sql & "vendedor.Nombre AS NombreVendedor,"
'   Sql = Sql & "renglonDetalleDeOtrosCargosFactura.CodigoDeCargo AS CodigoArticulo,"
'   Sql = Sql & "factura.Moneda AS MonedaDeFactura, "
'   Sql = Sql & "renglonDetalleDeOtrosCargosFactura.Descripcion AS DescripcionArticulo, "
'   Sql = Sql & "otrosCargosDeFactura.PorcentajeComision AS PorcentajeComision,"
'   Sql = Sql & "SUM(" & sqlPrecioConIva & ") AS PrecioConIVA,"
'   Sql = Sql & "SUM(" & sqlPrecioSinIva & ") AS PrecioSinIVA"
'   Sql = Sql & " FROM vendedor"
'   Sql = Sql & " INNER JOIN (cliente"
'   Sql = Sql & " INNER JOIN (factura"
'   Sql = Sql & " INNER JOIN (otrosCargosDeFactura"
'   Sql = Sql & " INNER JOIN renglonDetalleDeOtrosCargosFactura"
'   Sql = Sql & " ON (otrosCargosDeFactura.Codigo"
'   Sql = Sql & " = renglonDetalleDeOtrosCargosFactura.CodigoDeCargo"
'   Sql = Sql & ") AND (otrosCargosDeFactura.ConsecutivoCompania"
'   Sql = Sql & " = renglonDetalleDeOtrosCargosFactura.ConsecutivoCompania"
'   Sql = Sql & ")) ON (factura.TipoDeDocumento"
'   Sql = Sql & " = renglonDetalleDeOtrosCargosFactura.TipoDeDocumento"
'   Sql = Sql & ") AND (factura.Numero"
'   Sql = Sql & " = renglonDetalleDeOtrosCargosFactura.NumeroFactura"
'   Sql = Sql & ") AND (factura.ConsecutivoCompania"
'   Sql = Sql & " = renglonDetalleDeOtrosCargosFactura.ConsecutivoCompania"
'   Sql = Sql & ")) ON (cliente.Codigo"
'   Sql = Sql & " = factura.CodigoCliente"
'   Sql = Sql & ") AND (cliente.ConsecutivoCompania"
'   Sql = Sql & " = factura.ConsecutivoCompania"
'   Sql = Sql & ")) ON (vendedor.Codigo"
'   Sql = Sql & " = factura.CodigoVendedor"
'   Sql = Sql & ") AND (vendedor.ConsecutivoCompania"
'   Sql = Sql & " = factura.ConsecutivoCompania"
'   Sql = Sql & ") AND (vendedor.ConsecutivoCompania"
'   Sql = Sql & " = cliente.ConsecutivoCompania)"
'   Sql = Sql & " WHERE factura.ConsecutivoCompania" & _
'               " = " & valConsecutivoCompania
'   Sql = Sql & " AND " & gUtilSQL.DfSQLDateValueBetween("factura." & _
'         "Fecha", valFechaInicial, valFechaFinal)
'   Sql = Sql & " AND factura.TipoDeDocumento" _
'            & " <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_RESUMENDIARIODEVENTAS)
'   Sql = Sql & " AND (factura.StatusFactura" & _
'                  " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA) & _
'                  " OR factura.StatusFactura" & _
'                  " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_EMITIDA) & _
'                  ")"
'   Sql = Sql & " GROUP BY "
'   Sql = Sql & "vendedor.Codigo,"
'   Sql = Sql & "vendedor.Nombre,"
'   Sql = Sql & "renglonDetalleDeOtrosCargosFactura.CodigoDeCargo,"
'   Sql = Sql & "factura.Moneda, "
'   Sql = Sql & "renglonDetalleDeOtrosCargosFactura.Descripcion,"
'   Sql = Sql & "otrosCargosDeFactura.PorcentajeComision"
'   Set insOtrosCargosDeFacturaNavigator = Nothing
'   Set insFactura = Nothing
'   Set insVendedor = Nothing
'   Set insOtrosCargosDeFacturaNavigator = Nothing
'   Set insCliente = Nothing
'   fConstruirSQLPorVentasDelReporteComisionDeVendedorResumidoPoArticuloDeOtrosCargos = Sql
'h_EXIT: On Error GoTo 0
'   Exit Function
'h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
'      "fConstruirSQLPorVentasDelReporteComisionDeVendedorResumidoPoArticuloDeOtrosCargos", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
'End Function
'
Public Function fConstruirSQLPorCobrosDelReporteComisionDeVendedorDetalladoOrigenFactura(ByVal mUsarCambioOriginal As Boolean, ByRef gUltimaTasaDeCambio As Object, ByVal ReporteEnMonedaLocal As Boolean, _
                                                            ByRef gMonedaLocalActual As Object, ByVal valAsignarComisionDeVendedorEnCobranza As Boolean, ByVal valEsMontoBruto As Boolean, ByVal valConsecutivoCompania As Long, _
                                                            ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valCantidadAImprimir As String, ByVal valNombreDelVendedor As String, ByVal valUsarOtrosCargosDeFactura As Boolean) As String
   Dim SQL As String
   Dim sqlMoneda As String
   Dim sqlMontoAbonadoCxC As String
   Dim sqlMontoExento As String
   Dim sqlMontoIva As String
   Dim sqlMontoGravado As String
   Dim sqlMtoTotal As String
   Dim SQLMontoAbonadoBs As String
   Dim SQLMontoExentoBs As String
   Dim SQLMontoIvaBs As String
   Dim SQLMontoGravadoBs As String
   Dim SQLMontoAbonadoCob As String
   Dim SQLMontoExentoCob As String
   Dim SQLMontoIvaCob As String
   Dim SQLMontoGravadoCob As String
   Dim SqlTotalRenglon As String
   Dim SQLPorcentajeDelIVA As String
   Dim SQLPorcentajeDelArticuloExcluidos As String
   Dim SQLPorcentajeDelOtrosCargos As String
   Dim SQLTotalRenglonOtrosCargos As String
   Dim sqlMonedaCobro As String
   Dim sqlCambioCobranza As String
   Dim SQLTotalDocumento As String
   Dim SQLMontoComisionable As String
   Dim SQLMontoComisionableBs As String
   Dim SQLMontoDesc As String
   Dim SQLMontoDescBS As String
   Dim sqlCambioCxC As String
   Dim sqlCodigoMonedaCxc As String
   Dim sqlNombreMonedaCxc As String
   Dim sqlMonedaSimbolo As String
   Dim sqlFechaCobranza As String
   Dim sqlCambioCxCBs As String
   Dim SQLMontoComisionableCob As String
   Dim sqlCodigoMonedaCobro As String
   Dim SQLTotalRenglonOtrosCargosCob As String
   Dim SQLTotalRenglonOtrosCargosBs As String
   Dim SQLTotalDocumentoBs As String
   Dim SqlTotalRenglonCob As String
   Dim SqlTotalRenglonBs As String
   Dim vSQLMontoAbonadoCobranzaEnMonedaDeCxC  As String
   On Error GoTo h_ERROR
   sqlCodigoMonedaCobro = "cobranza.CodigoMoneda"
   sqlMonedaCobro = "cobranza.Moneda"
   sqlCambioCobranza = "cobranza.CambioABolivares"
   sqlMontoAbonadoCxC = "cxC.MontoAbonado"
   sqlMontoIva = gUtilSQL.getIIF("cxC.MontoIVA=0", "1", "cxC.MontoIVA", True)
   sqlMontoExento = "cxC.MontoExento"
   sqlMontoGravado = "cxC.MontoGravado"
   sqlCambioCxC = "documentoCobrado.CambioAMonedaLocal"
   sqlCambioCxCBs = "cxC.CambioABolivares"
   sqlCodigoMonedaCxc = "documentoCobrado.CodigoMonedaDeCxC"
   sqlNombreMonedaCxc = "cxC.Moneda"
   sqlMonedaSimbolo = "documentoCobrado.SimboloMonedaDeCxC"
   sqlFechaCobranza = "Cobranza.Fecha"
   SQLMontoAbonadoCob = "documentoCobrado.MontoAbonadoEnMonedaOriginal"
   vSQLMontoAbonadoCobranzaEnMonedaDeCxC = "documentoCobrado.MontoAbonadoEnMonedaOriginal"

   SqlTotalRenglon = fSQLGenerarElTotalTotalRenglon("cxC.TipoCxC", True, gUltimaTasaDeCambio, gMonedaLocalActual)
   SQLMontoDesc = "cobranza.DescProntoPago + cobranza.RetencionIslr"
   SQLTotalRenglonOtrosCargos = fSQLGenerarElTotalTotalRenglonCargosYDescuentos("cxC.TipoCxC")
   
    If mUsarCambioOriginal Then
      sqlCambioCobranza = gMonedaLocalActual.fSQLConvierteMontoSiAplicaMasDecimales(gMonedaLocalActual.GetHoyCodigoMoneda, sqlCambioCobranza, "Cobranza.Fecha")
      'sqlCambioCxCBs = gMonedaLocalActual.fSQLConvierteMontoSiAplicaMasDecimales(gMonedaLocalActual.GetHoyCodigoMoneda, sqlCambioCxCBs, "Cobranza.Fecha")
   Else
      sqlCambioCobranza = gUltimaTasaDeCambio.getUltimaTasaDeCambio("Cobranza.Moneda", False)
      'fSQLCampoTasaDeCambio(ByVal valFieldNameCambioABolivares As String, ByVal valFieldNameNombreMoneda As String, ByVal valUsarCambioOriginal As Boolean, ByVal valAlias As String) As String
      
   End If
   
    
   SQLMontoIvaCob = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCambioCxC, sqlMonedaCobro, sqlMontoIva, True, "")
   SQLMontoExentoCob = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCambioCxC, sqlMonedaCobro, sqlMontoExento, True, "")
   SQLMontoGravadoCob = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCambioCxC, sqlMonedaCobro, sqlMontoGravado, True, "")
   SQLTotalRenglonOtrosCargosCob = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCambioCxC, sqlMonedaCobro, SQLTotalRenglonOtrosCargos, True, "")
   SqlTotalRenglonCob = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCambioCxC, sqlMonedaCobro, SqlTotalRenglon, True, "")
  
   SQLMontoIvaCob = gMonedaLocalActual.fSQLConvierteMontoSiAplicaMasDecimales(sqlCodigoMonedaCobro, SQLMontoIvaCob, sqlFechaCobranza)
   SQLMontoExentoCob = gMonedaLocalActual.fSQLConvierteMontoSiAplicaMasDecimales(sqlCodigoMonedaCobro, SQLMontoExentoCob, sqlFechaCobranza)
   SQLMontoGravadoCob = gMonedaLocalActual.fSQLConvierteMontoSiAplicaMasDecimales(sqlCodigoMonedaCobro, SQLMontoGravadoCob, sqlFechaCobranza)
   SQLTotalRenglonOtrosCargosCob = gMonedaLocalActual.fSQLConvierteMontoSiAplicaMasDecimales(sqlCodigoMonedaCobro, SQLTotalRenglonOtrosCargosCob, sqlFechaCobranza)
   SqlTotalRenglonCob = gMonedaLocalActual.fSQLConvierteMontoSiAplicaMasDecimales(sqlCodigoMonedaCobro, SqlTotalRenglonCob, sqlFechaCobranza)

   SQLMontoAbonadoBs = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCambioCxC, "cxC.Moneda", SQLMontoAbonadoCob, mUsarCambioOriginal, "")
   SQLMontoIvaBs = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCambioCxCBs, "cxC.Moneda", sqlMontoIva, mUsarCambioOriginal, "")
   SQLMontoExentoBs = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCambioCxCBs, "cxC.Moneda", sqlMontoExento, mUsarCambioOriginal, "")
   SQLMontoGravadoBs = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCambioCxCBs, "cxC.Moneda", sqlMontoGravado, mUsarCambioOriginal, "")
   SQLMontoDescBS = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCambioCobranza, "Cobranza.Moneda", SQLMontoDesc, mUsarCambioOriginal, "")
   SQLTotalRenglonOtrosCargosBs = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCambioCxCBs, "cxC.Moneda", SQLTotalRenglonOtrosCargos, mUsarCambioOriginal, "")
   'SqlTotalRenglonBs = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCambioCxCBs, "cxC.Moneda", SqlTotalRenglon, mUsarCambioOriginal, "")
   SqlTotalRenglonBs = SqlTotalRenglon 'gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCambioCxCBs, "cxC.Moneda", SqlTotalRenglon, mUsarCambioOriginal, "")
        
   If ReporteEnMonedaLocal Then
      SQLMontoAbonadoCob = SQLMontoAbonadoBs
      sqlMontoIva = SQLMontoIvaBs
      sqlMontoExento = SQLMontoExentoBs
      sqlMontoGravado = SQLMontoGravadoBs
      sqlCambioCxC = gUltimaTasaDeCambio.fSQLCampoTasaDeCambio(sqlCambioCxC, "cxC.Moneda", mUsarCambioOriginal, "")
   End If
    
   sqlMtoTotal = "(" & sqlMontoExento & " + " & sqlMontoGravado & " + " & sqlMontoIva & ")"
   
      
   If valEsMontoBruto Then
      SQLMontoComisionable = "(" & vSQLMontoAbonadoCobranzaEnMonedaDeCxC & " - ROUND((" & SQLMontoAbonadoCob & " / ROUND(( 1  + (" & SQLMontoExentoCob & " + " & SQLMontoGravadoCob & ")/" & SQLMontoIvaCob & "),3)),3))- ( " & SQLMontoDesc & ")"
      SQLMontoComisionableBs = "(" & SQLMontoAbonadoBs & " - ROUND((" & SQLMontoAbonadoBs & " / ROUND(nullif( ( 1  + (" & SQLMontoExentoBs & " + " & SQLMontoGravadoBs & ")/" & SQLMontoIvaBs & "),1),3)),3)) - (" & SQLMontoDescBS & ")"
   Else
      SQLMontoComisionable = "(" & vSQLMontoAbonadoCobranzaEnMonedaDeCxC & " - ROUND((" & SQLMontoAbonadoCob & " / ROUND(( 1  + (" & SQLMontoExentoCob & " + " & SQLMontoGravadoCob & ")/" & SQLMontoIvaCob & "),3)),3))"
      SQLMontoComisionableBs = "(" & SQLMontoAbonadoBs & " - ROUND((" & SQLMontoAbonadoBs & " / ROUND(nullif( ( 1  + (" & SQLMontoExentoBs & " + " & SQLMontoGravadoBs & ")/" & SQLMontoIvaBs & "),1),3)),3))"
   End If

   If valUsarOtrosCargosDeFactura Then
      SQLTotalDocumento = SQLMontoIvaCob & " + " & SQLMontoExentoCob & " + " & SQLMontoGravadoCob
      SQLMontoComisionable = "((" & SQLMontoAbonadoCob & ") - ((((" & SQLMontoAbonadoCob & " / 10000) * (" & SQLMontoIvaCob & " / 10000)) / (" & SQLMontoIvaCob & " + " & SQLMontoExentoCob & " + " & SQLMontoGravadoCob & "))) * 10000)"
      SQLMontoComisionable = SQLMontoComisionable & " - ((((" & SQLMontoAbonadoCob & " / 10000) * (" & SqlTotalRenglonCob & " / 10000)) / (" & SQLTotalDocumento & ")) * 10000)"
      SQLMontoComisionable = SQLMontoComisionable & " - ((((" & SQLMontoAbonadoCob & " / 10000) * (" & SQLTotalRenglonOtrosCargosCob & " / 10000)) / (" & SQLTotalDocumento & ")) * 10000)"
      SQLMontoComisionable = "(" & SQLMontoComisionable & ")"

      SQLTotalDocumentoBs = SQLMontoIvaBs & " + " & SQLMontoExentoBs & " + " & SQLMontoGravadoBs

      SQLMontoComisionableBs = " ((" & SQLMontoAbonadoBs & ") - ((((" & SQLMontoAbonadoBs & " / 10000) * (" & SQLMontoIvaBs & " / 10000)) / nullif( (" & SQLMontoIvaBs & " + " & SQLMontoExentoBs & " + " & SQLMontoGravadoBs & "),0))) * 10000)"
      SQLMontoComisionableBs = SQLMontoComisionableBs & " - ((((" & SQLMontoAbonadoBs & " / 10000) * (" & SqlTotalRenglonBs & " / 10000)) / (" & SQLTotalDocumentoBs & ")) * 10000)"
      SQLMontoComisionableBs = SQLMontoComisionableBs & " - ((((" & SQLMontoAbonadoBs & " / 10000) * (" & SQLTotalRenglonOtrosCargosBs & " / 10000)) / (" & SQLTotalDocumentoBs & ")) * 10000)"
      SQLMontoComisionableBs = "(" & SQLMontoComisionableBs & ")"
   End If
    
   SQLMontoComisionable = " (ROUND(documentoCobrado.MontoAbonadoEnMonedaOriginal  - CASE WHEN cxC.MontoIVA <> 0 THEN  (documentoCobrado.MontoAbonadoEnMonedaOriginal /(1 + ((cxC.MontoExento  +  cxC.MontoGravado) / cxC.MontoIVA))) ELSE 0 END ,3))  "
    
   If ReporteEnMonedaLocal And mUsarCambioOriginal Then
      SQLMontoComisionable = " (ROUND(documentoCobrado.MontoAbonadoEnMonedaOriginal  - CASE WHEN cxC.MontoIVA <> 0 THEN  (documentoCobrado.MontoAbonadoEnMonedaOriginal /(1 + ((cxC.MontoExento  +  cxC.MontoGravado) / cxC.MontoIVA))) ELSE 0 END ,3) * ( DocumentoCobrado.CambioAMonedaLocal )) AS MontoComision"
   ElseIf ReporteEnMonedaLocal Then
   'Public Function fSQLCampoMontoPorTasaDeCambio(ByVal valFieldNameCambioABolivares As String, ByVal valFieldNameNombreMoneda As String, ByVal valFieldNameMonto As String, ByVal valUsarCambioOriginal As Boolean, ByVal valAlias As String) As String
      SQLMontoComisionable = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("DocumentoCobrado.CambioAMonedaLocal", "cxC.Moneda", SQLMontoComisionable, mUsarCambioOriginal, "MontoComision")
   End If
   
    
   
   SQL = "SELECT " & vbCrLf
   SQL = SQL & "cliente.Nombre AS NombreCliente, " & vbCrLf
   SQL = SQL & sqlMonedaCobro & ", " & vbCrLf
   SQL = SQL & "cxC.FechaVencimiento AS FechaV, " & vbCrLf
   SQL = SQL & "cxC.Numero AS NumeroFactura, " & vbCrLf
   SQL = SQL & "cxC.Fecha AS FechaFactura, " & vbCrLf
   SQL = SQL & sqlMtoTotal & " AS MontoFactura, " & vbCrLf
   SQL = SQL & "vendedor.Codigo, " & vbCrLf
   SQL = SQL & "cobranza.Fecha, " & vbCrLf
   SQL = SQL & "cobranza.Numero, " & vbCrLf
   SQL = SQL & " ('Cob. ' + cobranza.Numero + '/ Fact. ' + CxC.NumeroDocumentoOrigen)  AS NumeroDelDocumento, " & vbCrLf
   SQL = SQL & "cobranza.Fecha AS FechaDelDocumento, " & vbCrLf
   SQL = SQL & sqlMonedaSimbolo & " AS SimboloDeLaMoneda, " & vbCrLf
   If ReporteEnMonedaLocal Then
      SQL = SQL & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMoneda) & vbCrLf
   Else
      SQL = SQL & sqlMonedaCobro & vbCrLf
   End If
   SQL = SQL & " AS DescripcionDeLaMoneda, " & vbCrLf
   SQL = SQL & sqlCambioCxC & " As CambioABolivares, " & vbCrLf
   SQL = SQL & SQLMontoAbonadoCob & " AS MontoAbonado, " & vbCrLf
   SQL = SQL & sqlMontoIva & " AS MontoIva, " & vbCrLf
   SQL = SQL & sqlMontoExento & " AS MontoExento, " & vbCrLf
   SQL = SQL & sqlMontoGravado & " AS MontoGravado, " & vbCrLf
   SQL = SQL & "vendedor.Codigo AS CodigoDelVendedor, " & vbCrLf
   SQL = SQL & "vendedor.Nombre AS NombreDelVendedor, " & vbCrLf
   If valAsignarComisionDeVendedorEnCobranza Then
      SQL = SQL & "cobranza.ComisionVendedor" & vbCrLf
   Else
      SQL = SQL & "vendedor.comisionPorCobro" & vbCrLf
   End If
   SQL = SQL & " AS PorcentajeDeComision, " & vbCrLf
   If ReporteEnMonedaLocal Then
      SQL = SQL & vbCrLf & SQLMontoComisionable & ", " & vbCrLf & vbCrLf
   Else
      SQL = SQL & SQLMontoComisionable & " AS MontoComision, " & vbCrLf
   End If
   SQL = SQL & SQLMontoComisionableBs & " AS MontoComisionBs" & vbCrLf
   If valAsignarComisionDeVendedorEnCobranza Then
      SQL = SQL & ", (cobranza.ComisionVendedor/100) * (" & SQLMontoComisionable & ") As ComisionVendedor" & vbCrLf
   End If
   SQL = SQL & " FROM ((vendedor INNER JOIN cobranza" & vbCrLf
   SQL = SQL & " ON (vendedor.Codigo " & _
               " = cobranza.CodigoCobrador " & vbCrLf
   SQL = SQL & ") AND (vendedor.ConsecutivoCompania" & _
               " = cobranza.ConsecutivoCompania " & vbCrLf
   SQL = SQL & ")) INNER JOIN (documentoCobrado INNER JOIN cxC " & vbCrLf
   SQL = SQL & " ON (documentoCobrado.ConsecutivoCompania = cxC.ConsecutivoCompania " & vbCrLf
   SQL = SQL & ") AND (documentoCobrado.TipoDeDocumentoCobrado = cxC.TipoCxC " & vbCrLf
   SQL = SQL & ") AND (documentoCobrado.NumeroDelDocumentoCobrado = cxC.Numero " & vbCrLf
   SQL = SQL & ")) ON (cobranza.Numero = documentoCobrado.NumeroCobranza " & vbCrLf
   SQL = SQL & ") AND (cobranza.ConsecutivoCompania = documentoCobrado.ConsecutivoCompania " & vbCrLf
   SQL = SQL & ")) INNER JOIN cliente " & vbCrLf
   SQL = SQL & " ON (cxC.CodigoCliente = cliente.Codigo " & vbCrLf
   SQL = SQL & ") AND (cxC.ConsecutivoCompania = cliente.ConsecutivoCompania) " & vbCrLf
   SQL = SQL & " WHERE cobranza.ConsecutivoCompania = " & valConsecutivoCompania & vbCrLf
   SQL = SQL & " AND cobranza.StatusCobranza = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusCobranza.eSC_VIGENTE) & vbCrLf
   SQL = SQL & " AND " & gUtilSQL.DfSQLDateValueBetween("cobranza.Fecha", valFechaInicial, valFechaFinal) & vbCrLf
   SQL = SQL & " AND cxC.Origen = " & gUtilSQL.fSQLSimpleValueForEnum(enum_OrigenFacturacionOManual.eOF_FACTURA) & vbCrLf
   If valCantidadAImprimir <> "Todos" Then
      SQL = SQL & " AND vendedor.Nombre = " & gUtilSQL.fSimpleSqlValue(valNombreDelVendedor) & vbCrLf
   End If
   fConstruirSQLPorCobrosDelReporteComisionDeVendedorDetalladoOrigenFactura = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: SQL = ""
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fConstruirSQLPorCobrosDelReporteComisionDeVendedorDetalladoOrigenFactura", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLConvertirElTipoDeDocumentoCxCATipodeDocumentoFactura(ByVal valCampoTipoCX As String) As String
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = gUtilSQL.getIIF(valCampoTipoCX & " = " & gUtilSQL.fSimpleSqlValue(enum_TipoDeTransaccion.eTD_FACTURA), _
         gUtilSQL.fSimpleSqlValue(enum_TipoDocumentoFactura.eTF_FACTURA), _
         gUtilSQL.getIIF(valCampoTipoCX & " = " & gUtilSQL.fSimpleSqlValue(enum_TipoDeTransaccion.eTD_NOTADECREDITO), _
         gUtilSQL.fSimpleSqlValue(enum_TipoDocumentoFactura.eTF_NOTADECREDITO), _
         gUtilSQL.getIIF(valCampoTipoCX & " = " & gUtilSQL.fSimpleSqlValue(enum_TipoDeTransaccion.eTD_NOTADEDEBITO), _
         gUtilSQL.fSimpleSqlValue(enum_TipoDocumentoFactura.eTF_NOTADEDEBITO), _
         gUtilSQL.fSimpleSqlValue(enum_TipoDocumentoFactura.eTF_FACTURA), False), False), True)
   fSQLConvertirElTipoDeDocumentoCxCATipodeDocumentoFactura = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLConvertirElTipoDeDocumentoCxCATipodeDocumentoFactura", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
'
Private Function fSQLGenerarElTotalTotalRenglon(ByVal valCampoTipoCX As String, ByVal valAplicaEnDetalle As Boolean, ByRef gUltimaTasaDeCambio As Object, ByRef gMonedaLocalActual As Object) As String
   Dim sqlValidarElTipoDocumento As String
   Dim SqlTotalRenglon As String
   Dim sqlSumaTotalRenglon As String
   On Error GoTo h_ERROR
   sqlValidarElTipoDocumento = fSQLConvertirElTipoDeDocumentoCxCATipodeDocumentoFactura(valCampoTipoCX)
   sqlSumaTotalRenglon = fSQLMontoTotalDelRenglonSinIvaConElDescuentoDeFactura(False, False, gUltimaTasaDeCambio, gMonedaLocalActual)
   sqlSumaTotalRenglon = gUtilSQL.DfCDecSQL(sqlSumaTotalRenglon)
   sqlSumaTotalRenglon = " SUM(" & sqlSumaTotalRenglon & ")"
   sqlSumaTotalRenglon = gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull(sqlSumaTotalRenglon), 0, sqlSumaTotalRenglon, True)
   SqlTotalRenglon = " SELECT " & sqlSumaTotalRenglon
   SqlTotalRenglon = SqlTotalRenglon & " FROM factura INNER JOIN (articuloInventario"
   SqlTotalRenglon = SqlTotalRenglon & " INNER JOIN renglonFactura"
   SqlTotalRenglon = SqlTotalRenglon & " ON (articuloInventario.Codigo = renglonFactura.Articulo"
   SqlTotalRenglon = SqlTotalRenglon & ") AND (articuloInventario.ConsecutivoCompania = renglonFactura.ConsecutivoCompania"
   SqlTotalRenglon = SqlTotalRenglon & ")) ON (factura.TipoDeDocumento = renglonFactura.TipoDeDocumento"
   SqlTotalRenglon = SqlTotalRenglon & ") AND (factura.Numero = renglonFactura.NumeroFactura"
   SqlTotalRenglon = SqlTotalRenglon & ") AND (factura.ConsecutivoCompania = renglonFactura.ConsecutivoCompania)"
   SqlTotalRenglon = SqlTotalRenglon & " INNER JOIN cxc ON"
   SqlTotalRenglon = SqlTotalRenglon & " (factura.ConsecutivoCompania = cxC.ConsecutivoCompania"
   SqlTotalRenglon = SqlTotalRenglon & " AND factura.CodigoVendedor = cxC.CodigoVendedor"
   SqlTotalRenglon = SqlTotalRenglon & " AND factura.Numero = cxC.NumeroDocumentoOrigen)"
   SqlTotalRenglon = SqlTotalRenglon & " WHERE "
   SqlTotalRenglon = SqlTotalRenglon & " articuloInventario.ExcluirDeComision = " & gUtilSQL.fBooleanToSqlValue(True)
   SqlTotalRenglon = SqlTotalRenglon & " AND factura.TipoDeDocumento = " & sqlValidarElTipoDocumento
   SqlTotalRenglon = "(" & SqlTotalRenglon & ")"
   fSQLGenerarElTotalTotalRenglon = SqlTotalRenglon
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLGenerarElTotalTotalRenglon", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLGenerarElTotalTotalRenglonVentas(ByVal valCampoTipoCX As String, ByVal valAplicaEnDetalle As Boolean, ByVal sqlSumaTotalRenglon As String, ByVal valConsecutivoCompania As String) As String
   Dim sqlValidarElTipoDocumento As String
   Dim SqlTotalRenglon As String
   On Error GoTo h_ERROR
   sqlValidarElTipoDocumento = fSQLConvertirElTipoDeDocumentoCxCATipodeDocumentoFactura(valCampoTipoCX)
   SqlTotalRenglon = " SELECT " & sqlSumaTotalRenglon
   SqlTotalRenglon = SqlTotalRenglon & " FROM articuloInventario INNER JOIN (factura INNER JOIN renglonFactura ON (factura.TipoDeDocumento = renglonFactura.TipoDeDocumento) AND (factura.Numero = renglonFactura.NumeroFactura) AND (factura.ConsecutivoCompania = renglonFactura.ConsecutivoCompania)) ON (articuloInventario.Codigo = renglonFactura.Articulo) AND (articuloInventario.ConsecutivoCompania = renglonFactura.ConsecutivoCompania) "
   SqlTotalRenglon = SqlTotalRenglon & " WHERE "
   SqlTotalRenglon = SqlTotalRenglon & "factura.ConsecutivoCompania = " & valConsecutivoCompania
   SqlTotalRenglon = SqlTotalRenglon & " AND factura.CodigoVendedor = factura.CodigoVendedor"
   SqlTotalRenglon = SqlTotalRenglon & " AND articuloInventario.ExcluirDeComision = " & gUtilSQL.fBooleanToSqlValue(True)
   SqlTotalRenglon = SqlTotalRenglon & " AND factura.Numero = factura.Numero"
   SqlTotalRenglon = SqlTotalRenglon & " AND factura.TipoDeDocumento = factura.TipoDeDocumento"
   SqlTotalRenglon = "(" & SqlTotalRenglon & ")"
   fSQLGenerarElTotalTotalRenglonVentas = SqlTotalRenglon
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLGenerarElTotalTotalRenglonVentas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLGenerarElTotalTotalRenglonCargosYDescuentos(ByVal valCampoTipoCX As String) As String
   Dim sqlValidarElTipoDocumento As String
   Dim SQL As String
   Dim sqlSumaTotalRenglon As String
   On Error GoTo h_ERROR
   sqlValidarElTipoDocumento = fSQLConvertirElTipoDeDocumentoCxCATipodeDocumentoFactura(valCampoTipoCX)
   SQL = ""
   sqlSumaTotalRenglon = "renglonDetalleDeOtrosCargosFactura.TotalRenglon"
   sqlSumaTotalRenglon = gUtilSQL.DfCDecSQL(sqlSumaTotalRenglon)
   sqlSumaTotalRenglon = " SUM(" & sqlSumaTotalRenglon & ")"
   sqlSumaTotalRenglon = gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull(sqlSumaTotalRenglon), 0, sqlSumaTotalRenglon, True)
   SQL = " SELECT " & sqlSumaTotalRenglon
   SQL = SQL & " FROM otrosCargosDeFactura INNER JOIN (factura"
   SQL = SQL & " INNER JOIN renglonDetalleDeOtrosCargosFactura"
   SQL = SQL & " ON (factura.TipoDeDocumento = renglonDetalleDeOtrosCargosFactura.TipoDeDocumento)"
   SQL = SQL & " AND (factura.Numero = renglonDetalleDeOtrosCargosFactura.NumeroFactura)"
   SQL = SQL & " AND (factura.ConsecutivoCompania = renglonDetalleDeOtrosCargosFactura.ConsecutivoCompania))"
   SQL = SQL & " ON (otrosCargosDeFactura.Codigo = renglonDetalleDeOtrosCargosFactura.CodigoDeCargo)"
   SQL = SQL & " AND (otrosCargosDeFactura.ConsecutivoCompania = renglonDetalleDeOtrosCargosFactura.ConsecutivoCompania)"
   SQL = SQL & " WHERE "
   SQL = SQL & "factura.ConsecutivoCompania = cxC.ConsecutivoCompania"
   SQL = SQL & " AND factura.CodigoVendedor = cxC.CodigoVendedor"
   SQL = SQL & " AND otrosCargosDeFactura.ExcluirDeComision = " & gUtilSQL.fBooleanToSqlValue(True)
   SQL = SQL & " AND factura.Numero = cxC.NumeroDocumentoOrigen"
   SQL = SQL & " AND otrosCargosDeFactura.ComoAplicaAlTotalFactura <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_ComoAplicaOtrosCargosDeFactura.eCA_NO_APLICA_SOLO_INFORMATIVO)
   SQL = SQL & " AND factura.TipoDeDocumento = " & sqlValidarElTipoDocumento
   SQL = "(" & SQL & ")"
   fSQLGenerarElTotalTotalRenglonCargosYDescuentos = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLGenerarElTotalTotalRenglonCargosYDescuentos", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLMontoTotalDelRenglonSinIvaConElDescuentoDeFactura(ByVal valUsarCambioABolivares As Boolean, ByVal valVieneDelLibroDeVentas As Boolean, ByRef gUltimaTasaDeCambio As Object, ByRef gMonedaLocalActual As Object) As String
   Dim SQL As String
   On Error GoTo h_ERROR
      SQL = "(renglonFactura.PrecioSinIVA * renglonFactura.Cantidad)"
      SQL = "(" & SQL & " * (1 - (renglonFactura.PorcentajeDescuento / 100.00)))"
      SQL = "(" & SQL & " * (1 - (factura.PorcentajeDescuento / 100.00)))"
      If valUsarCambioABolivares Then
         SQL = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("factura.CambioABolivares", _
               "factura.Moneda", SQL, True, "")
      End If
      If Not valVieneDelLibroDeVentas Then
            SQL = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, SQL, "Factura.Fecha")
      End If
   fSQLMontoTotalDelRenglonSinIvaConElDescuentoDeFactura = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLMontoTotalDelRenglonSinIvaConElDescuentoDeFactura", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSqlCorrigeMonedaEnCobranza()
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = " UPDATE COBRANZA SET COBRANZA.Moneda = CuentaBancaria.NombreDeLaMoneda,"
   SQL = SQL & " cobranza.CodigoMoneda = CuentaBancaria.CodigoMoneda"
   SQL = SQL & " From cobranza LEFT JOIN CuentaBancaria ON"
   SQL = SQL & " cobranza.codigoCuentaBancaria = CuentaBancaria.codigo"
   SQL = SQL & " AND Cobranza.ConsecutivoCompania = CuentaBancaria.ConsecutivoCompania"
   SQL = SQL & " WHERE (COBRANZA.CodigoMoneda = 'VEB' OR  COBRANZA.CodigoMoneda = 'VEF')"
   SQL = SQL & " AND  (CuentaBancaria.CodigoMoneda <> 'VEF' AND CuentaBancaria.CodigoMoneda <> 'VEB')"
   fSqlCorrigeMonedaEnCobranza = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSqlCorrigeMonedaEnCobranza", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSqlCorrigeCambioABolivaresEnCobranza()
'OJO ESTE PROCESO SOLO DEBE EJECUTARSE SOLO SI ES ANTES DEL CAMBIO DEL MODULO PARA MANEJO DE MULTIPLES DOCUMENTOS Y MONEDAS
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = " UPDATE COBRANZA SET COBRANZA.Moneda = CXC.MONEDA,COBRANZA.CodigoMoneda = CXC.CodigoMoneda,"
   SQL = SQL & " COBRANZA.cambioABolivares = 0.001 From cobranza"
   SQL = SQL & " INNER JOIN DocumentoCobrado ON cobranza.Numero = DocumentoCobrado.NumeroCobranza"
   SQL = SQL & " AND Cobranza.ConsecutivoCompania = DocumentoCobrado.ConsecutivoCompania"
   SQL = SQL & " INNER JOIN CXC ON DocumentoCobrado.NumeroDelDocumentoCobrado = cxC.Numero"
   SQL = SQL & " AND Cobranza.CodigoCliente = cxc.CodigoCliente AND CXC.ConsecutivoCompania = DocumentoCobrado.ConsecutivoCompania"
   SQL = SQL & " WHERE COBRANZA.CodigoMoneda <> CxC.CodigoMoneda AND"
   SQL = SQL & " (COBRANZA.CodigoMoneda = 'VEB' OR COBRANZA.CodigoMoneda = 'VEF') AND"
   SQL = SQL & " (COBRANZA.Fecha >= '01/01/2008' AND CXC.Fecha < '01/01/2008')"
   fSqlCorrigeCambioABolivaresEnCobranza = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSqlCorrigeCambioABolivaresEnCobranza", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fNombreCampoFiltroInformeCobranzasEntreFecha(ByVal valOpcion As Integer) As String
   On Error GoTo h_ERROR
   Select Case valOpcion
      Case enum_OpcionInfCobranzaEntreFecha.eOC_Cobrador
         fNombreCampoFiltroInformeCobranzasEntreFecha = "Vendedor.Nombre"
      Case enum_OpcionInfCobranzaEntreFecha.eOC_Cliente
         fNombreCampoFiltroInformeCobranzasEntreFecha = "Cliente.Nombre"
      Case enum_OpcionInfCobranzaEntreFecha.eOC_Banco
         fNombreCampoFiltroInformeCobranzasEntreFecha = "CuentaBancaria.NombreCuenta"
   End Select
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fNombreCampoFiltroInformeCobranzasEntreFecha", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSqlCobranzaPorZona(ByVal valFechaInicial As Date, _
                                     ByVal valFechaFinal As Date, _
                                      ByVal valCantidadAImprimir As String, _
                                       ByVal valConsecutivoCompania As String, ByVal valZona As String, _
                                        ByVal valoptTasaDeCambio As Boolean, _
                                         ByVal ReporteEnMonedaLocal As Boolean, _
                                           ByVal gUltimaTasaDeCambio As Object, _
                                             ByVal valCodigoCobrador As String) As String
   Dim SQL As String
   Dim sqlMontoTotal As String
   Dim usarTasaOriginal As Boolean
   Dim sqlCampoCambioABs As String
   Dim sqlCampoMoneda As String
   
   On Error GoTo h_ERROR
   Set gEnumReport = New clsEnumReport
   
   If ReporteEnMonedaLocal Then
       usarTasaOriginal = valoptTasaDeCambio
      sqlCampoCambioABs = "IGV_CobranzaPorZona.CambioAbolivares"
      sqlCampoMoneda = "IGV_CobranzaPorZona.Moneda"
      sqlMontoTotal = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCampoCambioABs, sqlCampoMoneda, _
                     "IGV_CobranzaPorZona.TotalCobrado", usarTasaOriginal, "")
   Else
       sqlMontoTotal = "IGV_CobranzaPorZona.TotalCobrado"
   End If
   
   SQL = "SELECT "
   SQL = SQL & "IGV_CobranzaPorZona.ConsecutivoCompania, "
   SQL = SQL & "IGV_CobranzaPorZona.NombreZona, "
   SQL = SQL & "IGV_CobranzaPorZona.Numero, "
   SQL = SQL & "IGV_CobranzaPorZona.Fecha, "
   SQL = SQL & "IGV_CobranzaPorZona.CodigoMoneda, "
   SQL = SQL & "IGV_CobranzaPorZona.Moneda, "
   SQL = SQL & "IGV_CobranzaPorZona.CambioAbolivares, "
   SQL = SQL & sqlMontoTotal & " AS TotalCobrado, "
   SQL = SQL & "IGV_CobranzaPorZona.CodigoCliente, "
   SQL = SQL & "IGV_CobranzaPorZona.NombreCliente, "
   SQL = SQL & "IGV_CobranzaPorZona.CodigoCobrador, "
   SQL = SQL & "IGV_CobranzaPorZona.NombreCobrador "
   SQL = SQL & " FROM IGV_CobranzaPorZona "
   SQL = SQL & " WHERE " & gUtilSQL.DfSQLDateValueBetween("IGV_CobranzaPorZona.Fecha", valFechaInicial, valFechaFinal)
   SQL = SQL & " AND IGV_CobranzaPorZona.ConsecutivoCompania = " & valConsecutivoCompania
   
   If valCantidadAImprimir = gEnumReport.enumCantidadAImprimirToString(eCI_uno) Then
      SQL = SQL & " AND IGV_CobranzaPorZona.NombreZona= '" & valZona & "'"
   End If
   
   If valCodigoCobrador <> "" Then
      SQL = SQL & " AND IGV_CobranzaPorZona.CodigoCobrador= '" & valCodigoCobrador & "'"
   End If
   
   SQL = SQL & " ORDER BY "
   SQL = SQL & "IGV_CobranzaPorZona.CodigoMoneda, IGV_CobranzaPorZona.NombreZona, IGV_CobranzaPorZona.CodigoCobrador, " _
   & "IGV_CobranzaPorZona.CodigoCliente, IGV_CobranzaPorZona.Fecha, IGV_CobranzaPorZona.Numero"
   
   fSqlCobranzaPorZona = SQL
   
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: SQL = ""
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSqlCobranzaPorZona", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLCobranza(ByVal gMonedaLocalActual As Object, ByVal valConsecutivoCompania As Long, _
                           ByVal valNumero As String, ByVal valGeneraMovBancario As Boolean, _
                             ByVal gUltimaTasaDeCambio As Object) As String
   Dim SQL As String
   Dim SQLMontoOriginalRestante As String
   Dim sqlMontoAbonado As String
   Dim SQLMontoOriginalRestanteConvertido As String
   Dim esMismaMonedaCobranzaYCxC As String
   Dim esCobranzaEnMonedaLocal As String
   
   Dim sqlMontoRestante As String
   Dim montoOriginalRestConvertidoEnMonedaLocal As String
   Dim montoOriginalRestConvertidoEnMonedaExt As String
   Dim montoRestanteEnMonedaLocal As String
   Dim montoRestanteEnMonedaExt As String
   Dim gEnumProyecto As clsEnumAdministrativo
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   SQLMontoOriginalRestante = "documentoCobrado.MontoOriginalRestanteAlDiaDelc * documentoCobrado.CambioAMonedaDeCobranza"
   
   esCobranzaEnMonedaLocal = "cobranza.CodigoMoneda = " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMoneda)
   esMismaMonedaCobranzaYCxC = "Cobranza.CodigoMoneda = CxC.CodigoMoneda"
   montoOriginalRestConvertidoEnMonedaLocal = gUtilSQL.getIIF(esMismaMonedaCobranzaYCxC, "documentoCobrado.MontoOriginalRestanteAlDiaDelc", "documentoCobrado.MontoOriginalRestanteAlDiaDelc * DocumentoCobrado.CambioAMonedaLocal", True)
   montoOriginalRestConvertidoEnMonedaExt = gUtilSQL.getIIF(esMismaMonedaCobranzaYCxC, "documentoCobrado.MontoOriginalRestanteAlDiaDelc", "(documentoCobrado.MontoOriginalRestanteAlDiaDelc * DocumentoCobrado.CambioAMonedaLocal) / DocumentoCobrado.CambioAMonedaDeCobranza", True)
   SQLMontoOriginalRestanteConvertido = gUtilSQL.fRoundNDecimales(gUtilSQL.getIIF(esCobranzaEnMonedaLocal, montoOriginalRestConvertidoEnMonedaLocal, montoOriginalRestConvertidoEnMonedaExt), 2, "MontoOriginalRestanteConvertido")
   
   montoRestanteEnMonedaLocal = gUtilSQL.getIIF(esMismaMonedaCobranzaYCxC, "documentoCobrado.MontoOriginalRestanteAlDiaDelc - DocumentoCobrado.MontoAbonadoEnMonedaOriginal", "(DocumentoCobrado.MontoOriginalRestanteAlDiaDelc * DocumentoCobrado.CambioAMonedaLocal) - DocumentoCobrado.MontoAbonado", True)
   montoRestanteEnMonedaExt = gUtilSQL.getIIF(esMismaMonedaCobranzaYCxC, "documentoCobrado.MontoOriginalRestanteAlDiaDelc - DocumentoCobrado.MontoAbonadoEnMonedaOriginal", "((documentoCobrado.MontoOriginalRestanteAlDiaDelc * DocumentoCobrado.CambioAMonedaLocal) / DocumentoCobrado.CambioAMonedaDeCobranza) - DocumentoCobrado.MontoAbonado", True)
   sqlMontoRestante = gUtilSQL.fRoundNDecimales(gUtilSQL.getIIF(esCobranzaEnMonedaLocal, montoRestanteEnMonedaLocal, montoRestanteEnMonedaExt), 2, "MontoRestante")
   
   
   sqlMontoAbonado = gUtilSQL.getIIF("cobranza.CodigoMoneda = " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMoneda) _
                                    & "OR cobranza.CodigoMoneda = " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMonedaAnt), _
                                    gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "documentoCobrado.montoAbonado", "cobranza.Fecha"), _
                                    gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("cobranza.CambioAbolivares", "cobranza.Moneda", _
                                    "documentoCobrado.montoAbonado", True, ""), True)
                            
                            
  
   SQL = "SELECT DISTINCT documentoCobrado.NumeroDelDocumentoCobrado, "
   SQL = SQL & " documentoCobrado.MontoOriginalRestanteAlDiaDelC, "
   SQL = SQL & " documentoCobrado.CambioAMonedaDeCobranza,"
   SQL = SQL & " documentoCobrado.MontoAbonado, "
   SQL = SQL & " " & SQLMontoOriginalRestanteConvertido & " ,"
    '  WHEN cobranza.TipoDeDocumento = '1' THEN 0
   SQL = SQL & " " & sqlMontoRestante & " ,"
   SQL = SQL & " documentoCobrado.SeRetuvoIVA, "
   SQL = SQL & " documentoCobrado.MontoIvaRetenido AS MontoIvaRetenido, "
   SQL = SQL & " documentoCobrado.NumeroComprobanteRetIVA, "
   SQL = SQL & " documentoCobrado.FechaComprobanteRetIVA,  "
   SQL = SQL & " cobranza.Numero,  "
   SQL = SQL & " cobranza.Fecha,  "
   SQL = SQL & " cobranza.CodigoCuentaBancaria,"
   If valGeneraMovBancario Then
      SQL = SQL & " cuentaBancaria.NombreCuenta, "
      SQL = SQL & " cuentaBancaria.CodigoMoneda, "
      SQL = SQL & gUtilSQL.getIIF("cuentaBancaria.CodigoMoneda = " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMoneda) & " AND cobranza.Fecha <" & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.fVigenteDesdeByCodigo(gMonedaLocalActual.GetHoyCodigoMoneda)), gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMonedaAnt), "CuentaBancaria.NombreDeLaMoneda", True) & " AS NombreDeLaMonedaCuentaBancaria, "
      SQL = SQL & " conceptoBancario.Descripcion AS DescripcionConcepto, "
   Else
      SQL = SQL & " '' AS NombreCuenta, "
      SQL = SQL & " '' AS CodigoMoneda, "
      SQL = SQL & " '' AS NombreDeLaMonedaCuentaBancaria, "
      SQL = SQL & " '' AS DescripcionConcepto, "
   End If
   SQL = SQL & " cobranza.CodigoCliente, "
   SQL = SQL & " cliente.Nombre AS Cliente, "
   SQL = SQL & " cobranza.CodigoCobrador, "
   SQL = SQL & " vendedor.Nombre AS Cobrador, "
   SQL = SQL & " cobranza.CodigoConcepto, "
   SQL = SQL & " cobranza.CambioAbolivares, "
   
   SQL = SQL & " cobranza.RetencionIslr AS RetencionISRL, "
   SQL = SQL & " cobranza.RetencionIva  AS RetencionIVA, "
   SQL = SQL & " cobranza.TotalOtros, "
   SQL = SQL & " cobranza.CobradoEfectivo, "
   SQL = SQL & " cobranza.CobradoCheque, "
   SQL = SQL & " cobranza.NumerodelCheque, "
   SQL = SQL & " cobranza.NombreBanco, "
   SQL = SQL & " cobranza.CobradoTarjeta, "
   SQL = SQL & " cobranza.NroDeLaTarjeta, "
   SQL = SQL & gUtilSQL.DfSQLCaseIfForEnum("cobranza.CualTarjeta", enum_Tarjeta.eTa_VISA, gEnumProyecto.fenumTarjetaToStringInArray(True), "") & " AS CualTarjeta, "
   SQL = SQL & " cobranza.CobradoAnticipo,  "
   SQL = SQL & " cobranza.Vuelto, "
   SQL = SQL & " cobranza.DescProntoPago, "
   SQL = SQL & " cobranza.NumeroDeComprobanteISLR " & gUtilSQL.fAlias("NumeroComprobanteRetISLR")
   SQL = SQL & " FROM documentoCobrado INNER JOIN cxC "
   SQL = SQL & " ON documentoCobrado.ConsecutivoCompania = cxC.ConsecutivoCompania  "
   SQL = SQL & " AND documentoCobrado.NumeroDelDocumentoCobrado = cxC.Numero "
   SQL = SQL & " INNER JOIN cobranza ON documentoCobrado.ConsecutivoCompania = cobranza.ConsecutivoCompania  "
   SQL = SQL & " AND documentoCobrado.NumeroCobranza = cobranza.Numero  "
   SQL = SQL & " INNER JOIN vendedor ON cobranza.ConsecutivoCompania = vendedor.ConsecutivoCompania  "
   SQL = SQL & " AND cobranza.CodigoCobrador = vendedor.Codigo "
   SQL = SQL & " INNER JOIN  cliente ON cobranza.ConsecutivoCompania = cliente.ConsecutivoCompania  "
   SQL = SQL & " AND cobranza.CodigoCliente = cliente.Codigo"
   If valGeneraMovBancario Then
      SQL = SQL & " INNER JOIN cuentaBancaria ON cobranza.ConsecutivoCompania = cuentaBancaria.ConsecutivoCompania  "
      SQL = SQL & " AND cobranza.CodigoCuentaBancaria = cuentaBancaria.Codigo  "
      SQL = SQL & " INNER JOIN conceptoBancario ON cobranza.CodigoConcepto = conceptoBancario.Codigo"
   End If
   SQL = SQL & "  WHERE documentoCobrado.NumeroCobranza = " & valNumero
   SQL = SQL & " AND documentoCobrado.ConsecutivoCompania = " & valConsecutivoCompania
   Set gEnumProyecto = Nothing
h_EXIT: On Error GoTo 0
   fSQLCobranza = SQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLCobranza", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function


Public Function fSQLAnticipoAplicadoEnCobro(ByVal valConsecutivoCompania As Long, ByVal valNumero As String, ByVal gMonedaLocalActual As Object) As String
   Dim SQL As String
   Dim SQLMontoOriginal As String
   Dim SQLMontoRestanteAlDiaDelCobro As String
   Dim vEsMismaMonedaAntYCobranza As String
   Dim vEsCobranzaEnMonedaLocal As String
   Dim SQLMontoOriginalConvertido As String
   Dim SQLMontoRestanteAlDiaDelCobroConvertido As String
   On Error GoTo h_ERROR
   vEsMismaMonedaAntYCobranza = "anticipoCobrado.CodigoMoneda = Cobranza.CodigoMoneda"
   vEsCobranzaEnMonedaLocal = "Cobranza.CodigoMoneda = " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMoneda)
   SQLMontoOriginalConvertido = gUtilSQL.getIIF(vEsCobranzaEnMonedaLocal, "anticipoCobrado.MontoOriginal * anticipoCobrado.CambioAMonedaLocal", "(anticipoCobrado.MontoOriginal * anticipoCobrado.CambioAMonedaLocal)/ anticipoCobrado.Cambio", True)
   SQLMontoOriginal = gUtilSQL.fRoundNDecimales(gUtilSQL.getIIF(vEsMismaMonedaAntYCobranza, "anticipoCobrado.MontoOriginal", SQLMontoOriginalConvertido, True), 2, "MontoOriginal")
   SQLMontoRestanteAlDiaDelCobroConvertido = gUtilSQL.getIIF(vEsCobranzaEnMonedaLocal, "anticipoCobrado.MontoRestanteAlDia * anticipoCobrado.CambioAMonedaLocal", "(anticipoCobrado.MontoRestanteAlDia * anticipoCobrado.CambioAMonedaLocal)/ anticipoCobrado.Cambio", True)
   SQLMontoRestanteAlDiaDelCobro = gUtilSQL.fRoundNDecimales(gUtilSQL.getIIF(vEsMismaMonedaAntYCobranza, "anticipoCobrado.MontoRestanteAlDia", SQLMontoRestanteAlDiaDelCobroConvertido, True), 2, "MontoRestanteAlDia")
   SQL = "SELECT "
   SQL = SQL & " anticipoCobrado.ConsecutivoAnticipoUsado"
   SQL = SQL & " , anticipoCobrado.NumeroAnticipo"
   SQL = SQL & " , " & SQLMontoOriginal
   SQL = SQL & " , " & SQLMontoRestanteAlDiaDelCobro
   SQL = SQL & " , anticipoCobrado.MontoAplicado "
   SQL = SQL & " FROM anticipoCobrado "
   SQL = SQL & " INNER JOIN cobranza ON anticipoCobrado.NumeroCobranza = cobranza.Numero AND anticipoCobrado.ConsecutivoCompania = cobranza.ConsecutivoCompania "
   SQL = SQL & " WHERE cobranza.Numero = " & valNumero
   SQL = SQL & " AND cobranza.consecutivoCompania = " & valConsecutivoCompania
h_EXIT: On Error GoTo 0
   fSQLAnticipoAplicadoEnCobro = SQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLAnticipoAplicadoEnCobro", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLRetencionIVAFormal(ByVal valConsecutivoCompania As Long, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valInsMonedaLocalActual As Object, ByVal OrdenarPorFechaComprobante As Boolean) As String
   Dim SQL As String
   On Error GoTo h_ERROR
    SQL = SQL & "SELECT documentoCobrado.FechaComprobanteRetIva AS 'Fecha de Retencion', cobranza.Numero"
    SQL = SQL & " AS 'Numero de cobranza',CASE WHEN cxc.NumeroComprobanteFiscal <> '' THEN cxc.NumeroComprobanteFiscal ELSE "
    SQL = SQL & " (CASE WHEN cxc.numeroDocumentoOrigen <> ''  THEN cxc.numeroDocumentoOrigen ELSE cxc.numero  END)"
    SQL = SQL & "  END AS 'Numero del Documento"
    SQL = SQL & " Cobrado', documentoCobrado.NumeroComprobanteRetIva,"
    SQL = SQL & " documentoCobrado.MontoTotalDeCxC, cxc.MontoExento"
    SQL = SQL & " + cxc.MontoGravado + cxc.MontoIva AS 'Total Factura',cxc.MontoGravado"
    SQL = SQL & " AS 'Monto Gravable',cxc.MontoIva,documentoCobrado.MontoIvaRetenido,"
    SQL = SQL & " cxc.fecha AS 'fecha del Documento',cliente.Codigo,cliente.Nombre"
    SQL = SQL & " ,cliente.NumeroRif,  CASE WHEN cxc.NumeroComprobanteFiscal <> '' THEN factura.NumeroControl ELSE (CASE WHEN cxc.Origen = '0' THEN factura.NumeroControl ELSE cxc.NumeroControl  END) END AS 'NumeroControl'  ,"
    SQL = SQL & " cxc.MontoGravado + cxc.MontoExento As 'MontoSinIva' FROM cobranza INNER JOIN documentoCobrado"
    SQL = SQL & " ON (documentoCobrado.consecutivocompania=cobranza.consecutivoCompania"
    SQL = SQL & " AND documentoCobrado.NumeroCobranza= cobranza.Numero)INNER"
    SQL = SQL & " JOIN cliente ON(cliente.codigo = cobranza.codigocliente"
    SQL = SQL & " AND cliente.consecutivoCompania= cobranza.consecutivoCompania)INNER"
    SQL = SQL & " JOIN cxc ON (cxc.ConsecutivoCompania = documentoCobrado.consecutivoCompania"
    SQL = SQL & " AND cxc.Numero= documentoCobrado.NumeroDelDocumentoCobrado AND CxC.tipoCxC = documentoCobrado.TipoDeDocumentoCobrado)"
    SQL = SQL & "LEFT JOIN factura ON(cxc.numeroDocumentoOrigen = factura.numero and (factura.TipoDeDocumento = '0' OR  factura.TipoDeDocumento = '1' OR  factura.TipoDeDocumento = '2' OR  factura.TipoDeDocumento = '5' OR  factura.TipoDeDocumento = '7') and cxc.ConsecutivoCompania = factura.ConsecutivoCompania and  (factura.NumeroComprobanteFiscal= (CASE WHEN cxc.NumeroComprobanteFiscal ='' THEN '0' ELSE cxc.NumeroComprobanteFiscal END)  Or factura.NumeroComprobanteFiscal = cxc.NumeroComprobanteFiscal) )"
    SQL = SQL & " WHERE cliente.ConsecutivoCompania = " & valConsecutivoCompania & " AND cobranza.Fecha"
    SQL = SQL & " BETWEEN '" & valFechaInicial & "' AND '" & valFechaFinal & "' AND cobranza.RetencionIVA"
    SQL = SQL & " <> 0  AND cobranza.StatusCobranza = '0' "
    SQL = SQL & " AND CxC.NoAplicaParaLibroDeVentas = " & gUtilSQL.fBooleanToSqlValue(False)
    SQL = SQL & " ORDER BY"
    If (OrdenarPorFechaComprobante) Then
      SQL = SQL & " documentoCobrado.FechaComprobanteRetIva,"
    Else
      SQL = SQL & " cobranza.Fecha,"
    End If
    SQL = SQL & " cobranza.Numero, documentoCobrado.NumeroComprobanteRetIVA"
h_EXIT:   On Error GoTo 0
   fSQLRetencionIVAFormal = SQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLRetencionIVAFormal", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConstruirSQLDelReporteComisionDeVendedorPorCobranzaPorArticuloDetallado(ByVal mUsarCambioOriginal As Boolean, ByRef gUltimaTasaDeCambio As Object, ByVal ReporteEnMonedaLocal As Boolean, _
                                                            ByRef gMonedaLocalActual As Object, ByVal valConsecutivoCompania As Long, _
                                                            ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valCantidadAImprimir As String, ByVal valNombreDelVendedor As String) As String
   Dim SQL As String
   Dim sqlMonedaCobro As String
   Dim sqlCambio As String
   Dim sqlMontoComision As String
   Dim sqlComision As String
   Dim sqlCambioCxCBs As String
   Dim sqlMontoComisionBs As String
   On Error GoTo h_ERROR
   
   sqlMonedaCobro = "co.Moneda"
   sqlComision = sqlComision & " (CASE WHEN cvca.comisionaporcentaje = 'S' THEN MAX(((dc.montoabonado/cvca.totalfactura) * cvca.totalsiniva) * cvca.porcentajecomision) "
   sqlComision = sqlComision & " ELSE MAX((dc.montoabonado/cvca.totalfactura) * cvca.totalsiniva) END )"
   
   If mUsarCambioOriginal Then
      sqlCambio = gMonedaLocalActual.fSQLConvierteMontoSiAplicaMasDecimales(gMonedaLocalActual.GetHoyCodigoMoneda, sqlCambio, "Co.Fecha")
   Else
      sqlCambio = gUltimaTasaDeCambio.getUltimaTasaDeCambio("Co.Moneda", False)
   End If
    
   sqlMontoComision = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCambio, sqlMonedaCobro, sqlComision, True, "")

   sqlMontoComisionBs = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCambio, sqlMonedaCobro, sqlComision, mUsarCambioOriginal, "")
       
   If ReporteEnMonedaLocal Then
      sqlMontoComision = sqlMontoComisionBs
   End If
    
   SQL = "SELECT "
   SQL = SQL & "cxc.codigovendedor,"
   SQL = SQL & "vendedor.nombre AS nombrevendedor,"
   SQL = SQL & "cvca.articulo AS codigoarticulo, "
   SQL = SQL & "cvca.numero AS numerofactura, "
   SQL = SQL & "cvca.monedafactura, "
   SQL = SQL & "cvca.codigocliente AS codigodelcliente, "
   SQL = SQL & "cliente.nombre AS nombredelcliente, "
   SQL = SQL & "cliente.contacto, "
   SQL = SQL & "cvca.descripcion AS descripcionarticulo,"
   SQL = SQL & sqlMontoComision & " AS comision,"
   SQL = SQL & "MAX(cvca.cantidad) AS cantidad, "
   SQL = SQL & "cvca.porcentajecomision,"
   SQL = SQL & "cvca.totalfactura,"
   SQL = SQL & "cvca.fecha AS fechafactura,"
   SQL = SQL & " cvca.totalsiniva"
   SQL = SQL & " FROM cxc "
   SQL = SQL & " INNER JOIN documentocobrado dc "
   SQL = SQL & " ON cxc.numero = dc.numerodeldocumentocobrado " '
   SQL = SQL & " AND cxc.consecutivocompania = dc.consecutivocompania "
   SQL = SQL & " INNER JOIN cobranza co "
   SQL = SQL & " ON co.numero = dc.numerocobranza "
   SQL = SQL & " AND " & gUtilSQL.DfSQLDateValueBetween("co.fecha", valFechaInicial, valFechaFinal)
   SQL = SQL & " AND co.consecutivocompania = dc.consecutivocompania "
   SQL = SQL & " INNER JOIN IGV_ComisionVendedorCobranzaXArticulo cvca "
   SQL = SQL & " ON cvca.numero = dc.numerodeldocumentocobrado "
   SQL = SQL & " AND cvca.consecutivocompania = dc.consecutivocompania "
   SQL = SQL & " INNER JOIN vendedor "
   SQL = SQL & " ON vendedor.codigo = cvca.codigovendedor "
   SQL = SQL & " AND vendedor.consecutivocompania = cvca.consecutivocompania "
   If valCantidadAImprimir <> "Todos" Then
      SQL = SQL & " AND vendedor.Nombre = " & gUtilSQL.fSimpleSqlValue(valNombreDelVendedor)
   End If
   SQL = SQL & " INNER JOIN cliente "
   SQL = SQL & " ON cliente.codigo = cvca.codigocliente "
   SQL = SQL & " AND cliente.consecutivocompania = cvca.consecutivocompania "
   SQL = SQL & " WHERE cxc.consecutivocompania = " & valConsecutivoCompania
   SQL = SQL & " GROUP BY cxc.codigovendedor,vendedor.nombre ,cvca.articulo ,"
   SQL = SQL & " cvca.numero, cvca.monedafactura, cvca.codigocliente ,"
   SQL = SQL & " cliente.nombre, cliente.contacto, cvca.descripcion ,"
   SQL = SQL & " cvca.articulo , cvca.porcentajecomision, cvca.totalfactura, cvca.fecha, cvca.comisionaporcentaje,cvca.totalsiniva, "
   SQL = SQL & "co.moneda, dc.cambioamonedadecobranza "
   SQL = SQL & " ORDER BY nombrevendedor, codigoarticulo, fechafactura"
   fConstruirSQLDelReporteComisionDeVendedorPorCobranzaPorArticuloDetallado = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: SQL = ""
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fConstruirSQLDelReporteComisionDeVendedorPorCobranzaPorArticuloDetallado", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConstruirSQLDelReporteComisionDeVendedorPorCobranzaPorArticuloResumido(ByVal mUsarCambioOriginal As Boolean, ByRef gUltimaTasaDeCambio As Object, ByVal ReporteEnMonedaLocal As Boolean, _
                                                            ByRef gMonedaLocalActual As Object, ByVal valAsignarComisionDeVendedorEnCobranza As Boolean, ByVal valEsMontoBruto As Boolean, ByVal valConsecutivoCompania As Long, _
                                                            ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valCantidadAImprimir As String, ByVal valNombreDelVendedor As String) As String
  Dim SQL As String
   Dim sqlMonedaCobro As String
   Dim sqlCambio As String
   Dim sqlMontoComision As String
   Dim sqlComision As String
   Dim sqlCambioCxCBs As String
   Dim sqlMontoComisionBs As String
   On Error GoTo h_ERROR
   
   sqlMonedaCobro = "co.Moneda"
   sqlComision = sqlComision & "CASE WHEN cvca.comisionaporcentaje = 'S' THEN SUM(((dc.montoabonado/cvca.totalfactura) * cvca.totalsiniva) * cvca.porcentajecomision) "
   sqlComision = sqlComision & " ELSE SUM((dc.montoabonado/cvca.totalfactura) * cvca.totalsiniva) END "
   
   If mUsarCambioOriginal Then
      sqlCambio = gMonedaLocalActual.fSQLConvierteMontoSiAplicaMasDecimales(gMonedaLocalActual.GetHoyCodigoMoneda, sqlCambio, "Co.Fecha")
   Else
      sqlCambio = gUltimaTasaDeCambio.getUltimaTasaDeCambio("Co.Moneda", False)
   End If
    
   sqlMontoComision = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCambio, sqlMonedaCobro, sqlComision, True, "")

   sqlMontoComisionBs = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCambio, sqlMonedaCobro, sqlComision, mUsarCambioOriginal, "")
       
   If ReporteEnMonedaLocal Then
      sqlMontoComision = sqlMontoComisionBs
   End If
   
   SQL = "SELECT "
   SQL = SQL & "cxc.codigovendedor,"
   SQL = SQL & "vendedor.nombre AS nombrevendedor,"
   SQL = SQL & "cvca.articulo AS codigoarticulo, "
   SQL = SQL & "cvca.descripcion AS descripcionarticulo,"
   SQL = SQL & sqlMontoComision & " AS comision,"
   SQL = SQL & " SUM(cvca.cantidad) AS cantidad, "
   SQL = SQL & " SUM(cvca.totalfactura) AS totalfactura,"
   SQL = SQL & " SUM(cvca.totalsiniva) AS totasiniva"
   SQL = SQL & " FROM cxc "
   SQL = SQL & " INNER JOIN documentocobrado dc "
   SQL = SQL & " ON cxc.numero = dc.numerodeldocumentocobrado "
   SQL = SQL & " AND cxc.consecutivocompania = dc.consecutivocompania "
   SQL = SQL & " INNER JOIN cobranza co "
   SQL = SQL & " ON co.numero = dc.numerocobranza "
   SQL = SQL & " AND " & gUtilSQL.DfSQLDateValueBetween("co.fecha", valFechaInicial, valFechaFinal)
   SQL = SQL & " AND co.consecutivocompania = dc.consecutivocompania "
   SQL = SQL & " INNER JOIN IGV_ComisionVendedorCobranzaXArticulo cvca "
   SQL = SQL & " ON cvca.numero = dc.numerodeldocumentocobrado "
   SQL = SQL & " AND cvca.consecutivocompania = dc.consecutivocompania "
   SQL = SQL & " INNER JOIN vendedor "
   SQL = SQL & " ON vendedor.codigo = cvca.codigovendedor "
   SQL = SQL & " AND vendedor.consecutivocompania = cvca.consecutivocompania "
   If valCantidadAImprimir <> "Todos" Then
      SQL = SQL & " AND vendedor.Nombre = " & gUtilSQL.fSimpleSqlValue(valNombreDelVendedor)
   End If
   SQL = SQL & " INNER JOIN cliente "
   SQL = SQL & " ON cliente.codigo = cvca.codigocliente "
   SQL = SQL & " AND cliente.consecutivocompania = cvca.consecutivocompania "
   SQL = SQL & " WHERE cxc.consecutivocompania = " & valConsecutivoCompania
   SQL = SQL & " GROUP BY cxc.codigovendedor,vendedor.nombre,cvca.articulo,"
   SQL = SQL & " cvca.descripcion,cvca.comisionaporcentaje, co.Moneda"
   SQL = SQL & " ORDER BY nombrevendedor, codigoarticulo"
   fConstruirSQLDelReporteComisionDeVendedorPorCobranzaPorArticuloResumido = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: SQL = ""
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fConstruirSQLDelReporteComisionDeVendedorPorCobranzaPorArticuloResumido", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

'MOISES
Public Function fSQLRetencionISLR(ByVal valConsecutivoCompania As Long, ByVal valFechaInicial As Date, _
                                  ByVal valFechaFinal As Date, ByVal valInsMonedaLocalActual As Object, _
                                  ByVal valNombreDelCliente As String) As String
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = "SELECT "
   SQL = SQL & "cobranza.Fecha"
   SQL = SQL & ", cobranza.Numero"
   SQL = SQL & ", cliente.Codigo"
   SQL = SQL & ", cliente.NumeroRIF"
   SQL = SQL & ", cliente.Nombre"
   SQL = SQL & ", cobranza.TotalDocumentos AS TotalCobrado"
   SQL = SQL & ", cobranza.retencionislr"
   SQL = SQL & ", cobranza.NumeroDeComprobanteISLR"
   SQL = SQL & " FROM"
   SQL = SQL & " Cobranza"
   SQL = SQL & " INNER JOIN Cliente"
   SQL = SQL & " ON Cobranza.ConsecutivoCompania = Cliente.ConsecutivoCompania"
   SQL = SQL & " AND Cobranza.CodigoCliente = Cliente.Codigo"
   SQL = SQL & " WHERE"
   SQL = SQL & " cliente.ConsecutivoCompania = " & valConsecutivoCompania
   SQL = SQL & " AND " & gUtilSQL.DfSQLDateValueBetween("cobranza.Fecha", valFechaInicial, valFechaFinal)
   SQL = SQL & " AND cobranza.RetencionISLR <> 0 "
   SQL = SQL & " AND cobranza.StatusCobranza = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusCobranza.eSC_VIGENTE)
   If valNombreDelCliente <> "" Then
      SQL = SQL & " AND cliente.Nombre = " & gUtilSQL.fSimpleSqlValue(valNombreDelCliente)
   End If
   SQL = SQL & " ORDER BY"
   SQL = SQL & " cobranza.Fecha"
h_EXIT:   On Error GoTo 0
   fSQLRetencionISLR = SQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLRetencionISLR", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLMostrarCobranzasPorVentasDiferidas(ByVal valMostrarCobranzasPorVentasDiferidas As Boolean) As String
   Dim vSQL As String
   On Error GoTo h_ERROR
   If valMostrarCobranzasPorVentasDiferidas Then
      vSQL = " INNER JOIN DocumentoCobrado"
      vSQL = vSQL & " ON DocumentoCobrado.ConsecutivoCompania = Cobranza.ConsecutivoCompania"
      vSQL = vSQL & " AND DocumentoCobrado.NumeroCobranza = Cobranza.Numero"
      vSQL = vSQL & " INNER JOIN factura"
      vSQL = vSQL & " ON factura.Numero = DocumentoCobrado.NumeroDelDocumentoCobrado"
      vSQL = vSQL & " AND factura.EsOriginalmenteDiferida =  " & gUtilSQL.fBooleanToSqlValue(True)
      vSQL = vSQL & " AND DocumentoCobrado.ConsecutivoCompania = factura.ConsecutivoCompania "
   End If
   fSQLMostrarCobranzasPorVentasDiferidas = vSQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLMostrarCobranzasPorVentasDiferidas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConstruirSQLDelReporteCobranzasXVendedorConVentasDiferidas(ByVal usaCambioOriginal As Boolean, ByVal valNombreMonedaLocal As String, ByVal valElReporteEsEnMonLocal As Boolean, ByVal valFechaInicial As Date, _
                                                            ByVal valFechaFinal As Date, ByVal valUsaModuloContabilidad As Boolean, ByVal valIncluirDocumentosCobrados As Integer, ByVal valConsecutivoCompania As String, _
                                                               ByVal valCantidadAImprimir As String, ByVal valcodigoDeVendedor As String, ByVal valUsaCodigoVendedor As Boolean, _
                                                                  ByVal NombreTablaComprobante As String, ByVal gMonedaLocalActual As Object, ByVal gUltimaTasaDeCambio As Object, ByVal valMostrarCobranzasPorVentasDiferidas As Boolean) As String
   Dim vSQL As String
   Dim vSQLNombreMonedaReporte As String
   Dim vSQLMonedaCuentaBancaria As String
   Dim vSQLCobranzaCodigoCta As String
   Dim vSQLMtoTotalCobrado As String
   Dim vSQLMtoRestante As String
   Dim vSQLMtoAbobado As String
   Dim vSQLMonedaCobranza As String
   Dim vSQLCambio As String
   Dim vSQLMonedaCxC As String
   Dim vSQLMonedaLocalSegunFecha As String
   Dim vSQLMtoTotalReporte As String
   Dim vSQLFechaCobranza As String
   Dim vSQLCambioCxC As String
   Dim vSQLCambioCxCBs As String
   Dim vSQLMonedaCodigoCxC As String
   On Error GoTo h_ERROR
   Set gEnumReport = New clsEnumReport
   vSQLFechaCobranza = "Cobranza.Fecha"
   vSQLCambioCxC = "documentoCobrado.CambioAMonedaDeCobranza"
   vSQLMonedaLocalSegunFecha = gMonedaLocalActual.fSQLNombreMonedaALaFecha("cobranza.Fecha")
   vSQLMonedaCuentaBancaria = gUtilSQL.getIIF("cuentaBancaria.NombreDeLaMoneda = " & gUtilSQL.fSimpleSqlValue(valNombreMonedaLocal), vSQLMonedaLocalSegunFecha, "cuentaBancaria.NombreDeLaMoneda", True)
   vSQLCobranzaCodigoCta = "cobranza.CodigoCuentaBancaria"
   vSQLMonedaCobranza = "Cobranza.moneda"
   vSQLMtoTotalCobrado = "DocumentoCobrado.MontoAbonado"
   vSQLMtoRestante = "documentoCobrado.MontoOriginalRestanteAlDiaDelc"
   vSQLMtoAbobado = "DocumentoCobrado.MontoAbonado"
   vSQLCambio = "cobranza.CambioABolivares"
   vSQLMonedaCxC = "cxC.Moneda"
   vSQLCambioCxCBs = "cxC.CambioABolivares"
   vSQLMonedaCodigoCxC = "cxC.CodigoMoneda"
   If valElReporteEsEnMonLocal Then
      If usaCambioOriginal Then
         vSQLCambio = gMonedaLocalActual.fSQLConvierteMontoSiAplicaMasDecimales(gMonedaLocalActual.GetHoyCodigoMoneda, vSQLCambio, "Cobranza.Fecha")
         vSQLCambioCxCBs = gMonedaLocalActual.fSQLConvierteMontoSiAplicaMasDecimales(gMonedaLocalActual.GetHoyCodigoMoneda, vSQLCambioCxCBs, "Cobranza.Fecha")
      Else
         vSQLCambio = gUltimaTasaDeCambio.getUltimaTasaDeCambio("Cobranza.Moneda", False)
         vSQLCambioCxCBs = gUltimaTasaDeCambio.getUltimaTasaDeCambio("cxC.Moneda", False)
      End If
      vSQLMtoTotalReporte = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(vSQLCambio, vSQLMonedaCobranza, vSQLMtoTotalCobrado, usaCambioOriginal, "")
      vSQLMtoRestante = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(vSQLCambioCxCBs, vSQLMonedaCxC, vSQLMtoRestante, usaCambioOriginal, "")
      vSQLMtoAbobado = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(vSQLCambioCxCBs, vSQLMonedaCxC, vSQLMtoAbobado, usaCambioOriginal, "")
      vSQLNombreMonedaReporte = gUtilSQL.fSimpleSqlValue(valNombreMonedaLocal)
   Else
     vSQLMtoTotalReporte = vSQLMtoTotalCobrado
     vSQLNombreMonedaReporte = vSQLMonedaCobranza
   End If
   
   vSQL = ""
   vSQL = vSQL & "SELECT "
   vSQL = vSQL & "cobranza.CodigoCobrador "
   vSQL = vSQL & ", vendedor.Nombre AS NombreVendedor "
   vSQL = vSQL & "," & vSQLNombreMonedaReporte & " AS MonedaCobro"
   vSQL = vSQL & ",cobranza.Numero"
   vSQL = vSQL & ",cobranza.Fecha"
   vSQL = vSQL & ",cliente.Nombre AS NombreCliente"
   vSQL = vSQL & ",SUM (" & vSQLMtoTotalReporte & ") AS MtoTotalCobrado"
   If valUsaModuloContabilidad Then
      vSQL = vSQL & "," & gUtilSQL.getIIF(fSQLGenerarCuentaContable(valConsecutivoCompania, NombreTablaComprobante, valFechaInicial, valFechaFinal, "Cobranza.Numero") & " <> " & gUtilSQL.fSimpleSqlValue(""), fSQLGenerarCuentaContable(valConsecutivoCompania, NombreTablaComprobante, valFechaInicial, valFechaFinal, "Cobranza.Numero"), _
                        gUtilSQL.fSimpleSqlValue("No Aplica"), True)
      vSQL = vSQL & " AS NumeroComprobante"
   End If
   vSQL = vSQL & " FROM vendedor INNER JOIN (cliente"
   vSQL = vSQL & " INNER JOIN (cobranza LEFT JOIN cuentaBancaria ON ("
   vSQL = vSQL & " cobranza.ConsecutivoCompania = cuentaBancaria.ConsecutivoCompania"
   vSQL = vSQL & ") AND (cobranza.CodigoCuentaBancaria = cuentaBancaria.Codigo"
   vSQL = vSQL & ")) ON (cliente.Codigo = cobranza.CodigoCliente"
   vSQL = vSQL & ") AND (cliente.ConsecutivoCompania = cobranza.ConsecutivoCompania"
   vSQL = vSQL & ")) ON (vendedor.Codigo = cobranza.CodigoCobrador"
   vSQL = vSQL & ") AND (vendedor.ConsecutivoCompania = cobranza.ConsecutivoCompania)"
   vSQL = vSQL & fSQLMostrarCobranzasPorVentasDiferidas(valMostrarCobranzasPorVentasDiferidas)
      vSQL = vSQL & " WHERE " & gUtilSQL.DfSQLDateValueBetween("cobranza.Fecha", valFechaInicial, valFechaFinal)
   If valCantidadAImprimir = gEnumReport.enumCantidadAImprimirToString(eCI_uno) Then
      vSQL = vSQL & " AND cobranza.CodigoCobrador = " & gUtilSQL.fSimpleSqlValue(valcodigoDeVendedor)
   End If
   vSQL = vSQL & " AND cobranza.StatusCobranza = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusCobranza.eSC_VIGENTE)
   vSQL = vSQL & " AND vendedor.ConsecutivoCompania = " & valConsecutivoCompania
   vSQL = vSQL & " GROUP BY"
   vSQL = vSQL & " cobranza.CodigoCobrador"
   vSQL = vSQL & " ,vendedor.Nombre"
   vSQL = vSQL & " ,Cobranza.moneda"
   vSQL = vSQL & " ,cobranza.Numero"
   vSQL = vSQL & " ,cobranza.Fecha"
   vSQL = vSQL & " ,cliente.Nombre"
   fConstruirSQLDelReporteCobranzasXVendedorConVentasDiferidas = vSQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConstruirSQLDelReporteCobranzasXVendedorConVentasDiferidas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConstruirSQLDelReporteCobranzasXVendedorDocumentosCobradosConVentasDiferidas(ByVal usaCambioOriginal As Boolean, ByVal valNombreMonedaLocal As String, ByVal valElReporteEsEnMonLocal As Boolean, ByVal valFechaInicial As Date, _
                                                            ByVal valFechaFinal As Date, ByVal valUsaModuloContabilidad As Boolean, ByVal valIncluirDocumentosCobrados As Integer, ByVal valConsecutivoCompania As String, _
                                                               ByVal valCantidadAImprimir As String, ByVal valcodigoDeVendedor As String, ByVal valUsaCodigoVendedor As Boolean, _
                                                                  ByVal NombreTablaComprobante As String, ByVal gMonedaLocalActual As Object, ByVal gUltimaTasaDeCambio As Object, ByVal valMostrarCobranzasPorVentasDiferidas As Boolean) As String
   Dim vSQL As String
   Dim vvSQLNombreMonedaReporte As String
   Dim vvSQLMonedaCuentaBancaria As String
   Dim vvSQLCobranzaCodigoCta As String
   Dim vSQLMtoTotalCobrado As String
   Dim vSQLMtoRestante As String
   Dim vSQLMtoAbobado As String
   Dim vSQLMonedaCobranza As String
   Dim vSQLCambio As String
   Dim vSQLMonedaCxC As String
   Dim vSQLMonedaLocalSegunFecha As String
   Dim vSQLMtoTotalReporte As String
   Dim vSQLFechaCobranza As String
   Dim vSQLCambioCxC As String
   Dim vSQLCambioCxCBs As String
   Dim vSQLMonedaCodigoCxC As String
   Dim vSQLEncabezadoCTE As String
   Dim vSQLUsaContabilidad As String
   On Error GoTo h_ERROR
   Set gEnumReport = New clsEnumReport
   vSQLFechaCobranza = "Cobranza.Fecha"
   vSQLCambioCxC = "documentoCobrado.CambioAMonedaDeCobranza"
   vSQLMonedaLocalSegunFecha = gMonedaLocalActual.fSQLNombreMonedaALaFecha("cobranza.Fecha")
   vvSQLMonedaCuentaBancaria = gUtilSQL.getIIF("cuentaBancaria.NombreDeLaMoneda = " & gUtilSQL.fSimpleSqlValue(valNombreMonedaLocal), vSQLMonedaLocalSegunFecha, "cuentaBancaria.NombreDeLaMoneda", True)
   vvSQLCobranzaCodigoCta = "cobranza.CodigoCuentaBancaria"
   vSQLMonedaCobranza = "Cobranza.moneda"
   vSQLMtoTotalCobrado = "DocumentoCobrado.MontoAbonado"
   vSQLMtoRestante = "documentoCobrado.MontoOriginalRestanteAlDiaDelc"
   vSQLMtoAbobado = "DocumentoCobrado.MontoAbonado"
   vSQLCambio = "cobranza.CambioABolivares"
   vSQLMonedaCxC = "cxC.Moneda"
   vSQLCambioCxCBs = "cxC.CambioABolivares"
   vSQLMonedaCodigoCxC = "cxC.CodigoMoneda"
   If valElReporteEsEnMonLocal Then
      If usaCambioOriginal Then
         vSQLCambio = gMonedaLocalActual.fSQLConvierteMontoSiAplicaMasDecimales(gMonedaLocalActual.GetHoyCodigoMoneda, vSQLCambio, "Cobranza.Fecha")
         vSQLCambioCxCBs = gMonedaLocalActual.fSQLConvierteMontoSiAplicaMasDecimales(gMonedaLocalActual.GetHoyCodigoMoneda, vSQLCambioCxCBs, "Cobranza.Fecha")
      Else
         vSQLCambio = gUltimaTasaDeCambio.getUltimaTasaDeCambio("Cobranza.Moneda", False)
         vSQLCambioCxCBs = gUltimaTasaDeCambio.getUltimaTasaDeCambio("cxC.Moneda", False)
      End If
      vSQLMtoTotalReporte = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(vSQLCambio, vSQLMonedaCobranza, vSQLMtoTotalCobrado, usaCambioOriginal, "")
      vSQLMtoRestante = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(vSQLCambioCxCBs, vSQLMonedaCxC, vSQLMtoRestante, usaCambioOriginal, "")
      vSQLMtoAbobado = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(vSQLCambioCxCBs, vSQLMonedaCxC, vSQLMtoAbobado, usaCambioOriginal, "")
      vvSQLNombreMonedaReporte = gUtilSQL.fSimpleSqlValue(valNombreMonedaLocal)
   Else
     vSQLMtoTotalReporte = vSQLMtoTotalCobrado
     vvSQLNombreMonedaReporte = vSQLMonedaCobranza
   End If
   
   vSQL = ""
   vSQL = vSQL & ""
   If valUsaModuloContabilidad Then
      vSQLEncabezadoCTE = "; WITH CTE_MontoTotalCobrado(Numero,Fecha,MtoTotalCobrado,NumeroComprobante,TipoDeDocumentoCobrado,"
      vSQLUsaContabilidad = gUtilSQL.getIIF(fSQLGenerarCuentaContable(valConsecutivoCompania, NombreTablaComprobante, valFechaInicial, valFechaFinal, "Cobranza.Numero") & " <> " & gUtilSQL.fSimpleSqlValue(""), fSQLGenerarCuentaContable(valConsecutivoCompania, NombreTablaComprobante, valFechaInicial, valFechaFinal, "Cobranza.Numero"), _
                           gUtilSQL.fSimpleSqlValue("No Aplica"), True)
      vSQLUsaContabilidad = vSQLUsaContabilidad & " AS NumeroComprobante, "
   Else
      vSQLEncabezadoCTE = "; WITH CTE_MontoTotalCobrado(Numero,Fecha,MtoTotalCobrado,TipoDeDocumentoCobrado,"
   End If
   vSQL = vSQL & vSQLEncabezadoCTE
   vSQL = vSQL & "MonedaDoc,CambioABolivares) AS "
   vSQL = vSQL & "(SELECT "
   vSQL = vSQL & "cobranza.Numero, "
   vSQL = vSQL & "cobranza.Fecha, "
   vSQL = vSQL & "SUM (" & vSQLMtoTotalReporte & ") AS MtoTotalCobrado, "
   vSQL = vSQL & vSQLUsaContabilidad
   vSQL = vSQL & "documentoCobrado.TipoDeDocumentoCobrado, "
   vSQL = vSQL & vSQLMonedaCxC & " AS MonedaDoc, "
   vSQL = vSQL & vSQLCambio
    vSQL = vSQL & " FROM vendedor INNER JOIN"
      vSQL = vSQL & " cliente INNER JOIN"
      vSQL = vSQL & " cobranza ON cliente.Codigo = cobranza.CodigoCliente"
      vSQL = vSQL & " AND cliente.ConsecutivoCompania = cobranza.ConsecutivoCompania ON"
      vSQL = vSQL & " vendedor.Codigo = cobranza.CodigoCobrador"
      vSQL = vSQL & " AND vendedor.ConsecutivoCompania = cobranza.ConsecutivoCompania"
      vSQL = vSQL & fSQLMostrarCobranzasPorVentasDiferidas(valMostrarCobranzasPorVentasDiferidas)
      vSQL = vSQL & " INNER JOIN cxC ON vendedor.ConsecutivoCompania = cxC.ConsecutivoCompania"
      vSQL = vSQL & " AND vendedor.Codigo = cxC.CodigoVendedor AND"
      vSQL = vSQL & " cliente.ConsecutivoCompania = cxC.ConsecutivoCompania"
      vSQL = vSQL & " AND cliente.Codigo = cxC.CodigoCliente AND"
      vSQL = vSQL & " documentoCobrado.ConsecutivoCompania = cxC.ConsecutivoCompania"
      vSQL = vSQL & " AND documentoCobrado.NumeroDelDocumentoCobrado = cxC.Numero AND"
      vSQL = vSQL & " documentoCobrado.TipoDeDocumentoCobrado = cxC.tipoCxC"
      vSQL = vSQL & " WHERE " & gUtilSQL.DfSQLDateValueBetween("cobranza.Fecha", valFechaInicial, valFechaFinal)
   If valCantidadAImprimir = gEnumReport.enumCantidadAImprimirToString(eCI_uno) Then
      vSQL = vSQL & " AND cobranza.CodigoCobrador = " & gUtilSQL.fSimpleSqlValue(valcodigoDeVendedor)
   End If
   vSQL = vSQL & " AND cobranza.StatusCobranza = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusCobranza.eSC_VIGENTE)
   vSQL = vSQL & " AND vendedor.ConsecutivoCompania = " & valConsecutivoCompania
   vSQL = vSQL & " GROUP BY"
   vSQL = vSQL & " cobranza.Numero"
   vSQL = vSQL & " ,cobranza.Fecha"
   vSQL = vSQL & " ,cliente.Nombre"
   vSQL = vSQL & " ,documentoCobrado.TipoDeDocumentoCobrado"
   vSQL = vSQL & " ,cxC.Moneda"
   vSQL = vSQL & " ,cobranza.CambioABolivares) "
  
   vSQL = vSQL & "SELECT "
   vSQL = vSQL & "cobranza.CodigoCobrador, "
   vSQL = vSQL & "vendedor.Nombre AS NombreVendedor, "
   vSQL = vSQL & vvSQLNombreMonedaReporte & " AS MonedaCobro, "
   vSQL = vSQL & "CTE_MontoTotalCobrado.Numero, "
   vSQL = vSQL & "CTE_MontoTotalCobrado.Fecha, "
   vSQL = vSQL & "cliente.Nombre AS NombreCliente, "
   vSQL = vSQL & "CTE_MontoTotalCobrado.MtoTotalCobrado, "
   If valUsaModuloContabilidad Then
      vSQL = vSQL & " CTE_MontoTotalCobrado.NumeroComprobante, "
   End If
   vSQL = vSQL & "documentoCobrado.NumeroDelDocumentoCobrado, "
   vSQL = vSQL & "documentoCobrado.TipoDeDocumentoCobrado, "
   vSQL = vSQL & vSQLMtoRestante & " AS MtoOriginal, "
   vSQL = vSQL & vSQLMtoAbobado & " AS MtoAbonado, "
   vSQL = vSQL & "CTE_MontoTotalCobrado.MonedaDoc, "
   vSQL = vSQL & "CTE_MontoTotalCobrado.CambioABolivares"
   vSQL = vSQL & " FROM vendedor INNER JOIN (cliente"
   vSQL = vSQL & " INNER JOIN (cobranza LEFT JOIN cuentaBancaria ON ("
   vSQL = vSQL & " cobranza.ConsecutivoCompania = cuentaBancaria.ConsecutivoCompania"
   vSQL = vSQL & ") AND (cobranza.CodigoCuentaBancaria = cuentaBancaria.Codigo"
   vSQL = vSQL & ")) ON (cliente.Codigo = cobranza.CodigoCliente"
   vSQL = vSQL & ") AND (cliente.ConsecutivoCompania = cobranza.ConsecutivoCompania"
   vSQL = vSQL & ")) ON (vendedor.Codigo = cobranza.CodigoCobrador"
   vSQL = vSQL & ") AND (vendedor.ConsecutivoCompania = cobranza.ConsecutivoCompania)"
   vSQL = vSQL & fSQLMostrarCobranzasPorVentasDiferidas(valMostrarCobranzasPorVentasDiferidas)
   vSQL = vSQL & " INNER JOIN CTE_MontoTotalCobrado ON CTE_MontoTotalCobrado.Numero = Cobranza.Numero"
   vSQL = vSQL & " INNER JOIN cxC ON vendedor.ConsecutivoCompania = cxC.ConsecutivoCompania"
   vSQL = vSQL & " AND vendedor.Codigo = cxC.CodigoVendedor AND"
   vSQL = vSQL & " cliente.ConsecutivoCompania = cxC.ConsecutivoCompania"
   vSQL = vSQL & " AND cliente.Codigo = cxC.CodigoCliente AND"
   vSQL = vSQL & " documentoCobrado.ConsecutivoCompania = cxC.ConsecutivoCompania"
   vSQL = vSQL & " AND documentoCobrado.NumeroDelDocumentoCobrado = cxC.Numero AND"
   vSQL = vSQL & " documentoCobrado.TipoDeDocumentoCobrado = cxC.tipoCxC"
   vSQL = vSQL & " WHERE " & gUtilSQL.DfSQLDateValueBetween("cobranza.Fecha", valFechaInicial, valFechaFinal)
   If valCantidadAImprimir = gEnumReport.enumCantidadAImprimirToString(eCI_uno) Then
      vSQL = vSQL & " AND cobranza.CodigoCobrador = " & gUtilSQL.fSimpleSqlValue(valcodigoDeVendedor)
   End If
   vSQL = vSQL & " AND cobranza.StatusCobranza = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusCobranza.eSC_VIGENTE)
   vSQL = vSQL & " AND vendedor.ConsecutivoCompania = " & valConsecutivoCompania
   fConstruirSQLDelReporteCobranzasXVendedorDocumentosCobradosConVentasDiferidas = vSQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConstruirSQLDelReporteCobranzasXVendedorDocumentosCobradosConVentasDiferidas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLDelReporteCobranzaEntreFechasConVentasDiferidas(ByVal valReporteEnMonedaLocal As Boolean, ByVal valUsaModuloContabilidad As Boolean, _
                                  ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valConsecutivoCompania As String, _
                                  ByVal ValStatusCobranzaToString As String, ByVal gMonedaLocalActual As Object, ByVal gUltimaTasaDeCambio As Object, _
                                  ByVal NombreTablaComprobante As String, ByVal usaCambioOriginal As Boolean, ByVal valOpcionInforme As Integer, _
                                  ByVal valAgruparInforme As Boolean, ByVal valFiltrovSQL As String, ByVal valMostrarCobranzasPorVentasDiferidas As Boolean) As String
   Dim vSQL As String
   Dim vSQLCambio As String
   Dim vSQLTotalCobrado As String
   Dim vSQLFechaCobranza As String
   Dim vSQLMonedaCobro As String
   Dim vSQLMonedaCxC As String
   On Error GoTo h_ERROR
   vSQLFechaCobranza = "Cobranza.Fecha"
   vSQLMonedaCobro = "Cobranza.moneda"
   If valOpcionInforme = enum_OpcionInfCobranzaEntreFecha.eOC_Banco Then
      vSQLTotalCobrado = "(SUM(DocumentoCobrado.MontoAbonado - DocumentoCobrado.MontoIvaRetenido) / (cobranza.TotalCobrado - cobranza.RetencionIva))"
      vSQLTotalCobrado = vSQLTotalCobrado & " * (cobranza.TotalCobrado - (cobranza.RetencionIslr + cobranza.TotalOtros + cobranza.RetencionIva + cobranza.CobradoAnticipo + cobranza.Vuelto + cobranza.DescProntoPago))"
   Else
      vSQLTotalCobrado = "SUM (DocumentoCobrado.MontoAbonado)"
   End If
   vSQLCambio = "cobranza.CambioABolivares"
   vSQLMonedaCxC = "cxC.Moneda"
         
   If valReporteEnMonedaLocal Then
      If usaCambioOriginal Then
         vSQLCambio = gMonedaLocalActual.fSQLConvierteMontoSiAplicaMasDecimales(gMonedaLocalActual.GetHoyCodigoMoneda, vSQLCambio, "Cobranza.Fecha")
      Else
         vSQLCambio = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("", vSQLMonedaCobro, "1", False, "")
      End If
      vSQLTotalCobrado = gUtilSQL.getIIF("Cobranza.CodigoMoneda = " & gMonedaLocalActual.fSQLCodigoMonedaALaFecha(vSQLFechaCobranza), _
                  gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, vSQLTotalCobrado, vSQLFechaCobranza), _
                  gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(vSQLCambio, vSQLMonedaCobro, vSQLTotalCobrado, usaCambioOriginal, ""), True)
      vSQLMonedaCobro = gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda)
   Else
      vSQLMonedaCobro = "Cobranza.moneda"
   End If
   
   vSQL = "SELECT "
   vSQL = vSQL & "Cobranza.Fecha, "
   vSQL = vSQL & "Cobranza.Numero, "
   vSQL = vSQL & "Cobranza.CodigoCobrador, "
   vSQL = vSQL & "Vendedor.Nombre AS NombreVendedor, "
   vSQL = vSQL & "Cobranza.CodigoCliente, "
   vSQL = vSQL & "Cliente.Nombre AS NombreCliente, "
   vSQL = vSQL & "Cobranza.CodigoMoneda, "
   vSQL = vSQL & vSQLMonedaCobro & " AS MonedaCobro, "
   vSQL = vSQL & vSQLCambio & " AS Cambio, "
   vSQL = vSQL & gUtilSQL.getIIF("Cobranza.StatusCobranza = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusCobranza.eSC_ANULADA), "0", vSQLTotalCobrado, True) & " AS TotalCobrado, "
   vSQL = vSQL & gUtilSQL.getIIF("Cobranza.StatusCobranza = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusCobranza.eSC_ANULADA), _
               gUtilSQL.fSimpleSqlValue(ValStatusCobranzaToString), gUtilSQL.fSimpleSqlValue(""), True) & " AS Status, "
   vSQL = vSQL & "CuentaBancaria.NombreCuenta "
   If valUsaModuloContabilidad Then
      vSQL = vSQL & ", " & gUtilSQL.getIIF(fSQLGenerarCuentaContable(valConsecutivoCompania, NombreTablaComprobante, valFechaInicial, valFechaFinal, "Cobranza.Numero") & " <> " & gUtilSQL.fSimpleSqlValue(""), fSQLGenerarCuentaContable(valConsecutivoCompania, NombreTablaComprobante, valFechaInicial, valFechaFinal, "Cobranza.Numero"), _
                        gUtilSQL.fSimpleSqlValue("No Aplica"), True)
      vSQL = vSQL & " AS NumeroComprobante"
   End If
   vSQL = vSQL & " FROM vendedor INNER JOIN "
   vSQL = vSQL & " cliente INNER JOIN "
   vSQL = vSQL & " cobranza ON cliente.Codigo = cobranza.CodigoCliente "
   vSQL = vSQL & " AND cliente.ConsecutivoCompania = cobranza.ConsecutivoCompania "
   vSQL = vSQL & " ON vendedor.Codigo = cobranza.CodigoCobrador "
   vSQL = vSQL & " AND vendedor.ConsecutivoCompania = cobranza.ConsecutivoCompania "
   If valOpcionInforme = enum_OpcionInfCobranzaEntreFecha.eOC_Banco Then
      vSQL = vSQL & " INNER JOIN CuentaBancaria ON Cobranza.CodigoCuentaBancaria = CuentaBancaria.Codigo "
   Else
      vSQL = vSQL & " LEFT JOIN CuentaBancaria ON Cobranza.CodigoCuentaBancaria = CuentaBancaria.Codigo "
   End If
   vSQL = vSQL & " AND Cobranza.ConsecutivoCompania = CuentaBancaria.ConsecutivoCompania "
   vSQL = vSQL & fSQLMostrarCobranzasPorVentasDiferidas(valMostrarCobranzasPorVentasDiferidas)
   vSQL = vSQL & " WHERE " & gUtilSQL.DfSQLDateValueBetween(vSQLFechaCobranza, valFechaInicial, valFechaFinal)
   vSQL = vSQL & " AND (Cobranza.ConsecutivoCompania = " & valConsecutivoCompania & ")"
   If valFiltrovSQL <> "" Then
      vSQL = vSQL & " AND " & fNombreCampoFiltroInformeCobranzasEntreFecha(valOpcionInforme) & " = " & gUtilSQL.fSimpleSqlValue(valFiltrovSQL)
   End If
   vSQL = vSQL & "GROUP BY"
   vSQL = vSQL & " Cobranza.Fecha"
   vSQL = vSQL & " , Cobranza.Numero"
   vSQL = vSQL & " , Cobranza.CodigoCobrador"
   vSQL = vSQL & " , Vendedor.Nombre"
   vSQL = vSQL & " , Cobranza.CodigoCliente"
   vSQL = vSQL & " , Cliente.Nombre"
   vSQL = vSQL & " , Cobranza.CodigoMoneda"
   vSQL = vSQL & " , Cobranza.moneda"
   vSQL = vSQL & " , cobranza.CambioABolivares"
   vSQL = vSQL & " , CuentaBancaria.NombreCuenta"
   vSQL = vSQL & " , Cobranza.StatusCobranza"
   vSQL = vSQL & " , cobranza.TotalCobrado"
   vSQL = vSQL & " , cobranza.RetencionIslr"
   vSQL = vSQL & " , cobranza.TotalOtros"
   vSQL = vSQL & " , cobranza.RetencionIva"
   vSQL = vSQL & " , cobranza.CobradoAnticipo"
   vSQL = vSQL & " , cobranza.Vuelto"
   vSQL = vSQL & " , cobranza.DescProntoPago"
   fSQLDelReporteCobranzaEntreFechasConVentasDiferidas = vSQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLDelReporteCobranzaEntreFechasConVentasDiferidas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConstruirSQLDelReporteCobranzaPorDiaConVentasDiferidas(ByVal valReporteEnMonedaLocal As Boolean, ByVal valFechaInicial As Date, _
                                               ByVal valFechaFinal As Date, ByVal valConsecutivoCompania As String, _
                                               ByVal gMonedaLocalActual As Object, ByVal gUltimaTasaDeCambio As Object, _
                                               ByVal valUsarCambioOriginal As Boolean, ByVal valMostrarCobranzasPorVentasDiferidas As Boolean) As String
   Dim vSQL As String
   Dim NumeroUno As Long
   Dim vSQLFechaCobranza As String
   Dim vSQLMontoTotal As String
   Dim vSQLMaximoDocCobrado As String
   Dim vSQLMoneda As String
   Dim vSQLCambio As String
   Dim vSQLMontoOriginal As String
   Dim vSQLCambioCxC As String
   Dim vSQLMonedaCxC As String
   Dim vFechaDeUltimoCambio As Date
   On Error GoTo h_ERROR
   NumeroUno = 1
   vSQLMoneda = "Cobranza.moneda"
   vSQLFechaCobranza = "Cobranza.Fecha"
   vSQLMontoTotal = "DocumentoCobrado.MontoAbonado"
   vSQLMaximoDocCobrado = "DocumentoCobrado.MontoAbonado"
   vSQLCambio = "Cobranza.CambioABolivares"
   vSQLMontoOriginal = "DocumentoCobrado.MontoAbonado"
   vSQLCambioCxC = "Cobranza.CambioABolivares"
   vSQLMonedaCxC = "Cobranza.Moneda"
   
   If valReporteEnMonedaLocal Then
      vSQLMoneda = gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda)
      vSQLMonedaCxC = gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda)
      If valUsarCambioOriginal Then
         vSQLCambio = gMonedaLocalActual.fSQLConvierteMontoSiAplicaMasDecimales(gMonedaLocalActual.GetHoyCodigoMoneda, vSQLCambio, "Cobranza.Fecha")
         vSQLCambioCxC = gMonedaLocalActual.fSQLConvierteMontoSiAplicaMasDecimales(gMonedaLocalActual.GetHoyCodigoMoneda, vSQLCambioCxC, "Cobranza.Fecha")
      Else
         vSQLCambio = gUltimaTasaDeCambio.getUltimaTasaDeCambio("Cobranza.Moneda", False)
         vSQLCambioCxC = gUltimaTasaDeCambio.getUltimaTasaDeCambio("Cobranza.Moneda", False)
      End If
      vSQLMontoTotal = gUtilSQL.getIIF("Cobranza.CodigoMoneda = " & gMonedaLocalActual.fSQLCodigoMonedaALaFecha(vSQLFechaCobranza), _
                     gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, vSQLMontoTotal, vSQLFechaCobranza), _
                     gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(vSQLCambio, "Cobranza.moneda", vSQLMontoTotal, valUsarCambioOriginal, ""), True)

      vSQLMontoOriginal = gUtilSQL.getIIF("Cobranza.CodigoMoneda = " & gMonedaLocalActual.fSQLCodigoMonedaALaFecha(vSQLFechaCobranza), _
                     gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, vSQLMontoOriginal, vSQLFechaCobranza), _
                     gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(vSQLCambioCxC, "Cobranza.moneda", vSQLMontoOriginal, valUsarCambioOriginal, ""), True)

      vSQLMaximoDocCobrado = gUtilSQL.getIIF("Cobranza.CodigoMoneda = " & gMonedaLocalActual.fSQLCodigoMonedaALaFecha(vSQLFechaCobranza), _
                     gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, vSQLMaximoDocCobrado, vSQLFechaCobranza), _
                     gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(vSQLCambio, "Cobranza.Moneda", vSQLMaximoDocCobrado, valUsarCambioOriginal, ""), True)
      vSQLCambioCxC = 1
   End If
   
   vSQL = vSQL & "SELECT "
   vSQL = vSQL & gUtilSQL.fCOUNT("distinct(cobranza.numero)", "NumeroDeCobranzas, ")
   vSQL = vSQL & vSQLFechaCobranza & " AS Fecha, "
   vSQL = vSQL & vSQLMoneda & " AS MonedaDelReporte, "
   vSQL = vSQL & vSQLMonedaCxC & " AS MonedaDeCxC, "
   vSQL = vSQL & vSQLCambioCxC & " As Cambio, "
   vSQL = vSQL & "Cobranza.CodigoMoneda, "
   vSQL = vSQL & "SUM(" & vSQLMontoTotal & ") AS MontoTotal, "
   vSQL = vSQL & "MAX(" & vSQLMaximoDocCobrado & ") As MaximoDocCobrado, "
   vSQL = vSQL & "SUM(" & vSQLMontoOriginal & ") AS MontoOriginal"
   vSQL = vSQL & " FROM cobranza  "
   vSQL = vSQL & fSQLMostrarCobranzasPorVentasDiferidas(valMostrarCobranzasPorVentasDiferidas)

   vSQL = vSQL & " WHERE " & gUtilSQL.DfSQLDateValueBetween(vSQLFechaCobranza, _
                           valFechaInicial, valFechaFinal)
   vSQL = vSQL & " AND (Cobranza.ConsecutivoCompania =" & _
               valConsecutivoCompania & ")"
   vSQL = vSQL & " AND Cobranza.StatusCobranza" & _
               " = " & gUtilSQL.fSimpleSqlValue(gConvert.enumerativoAChar(enum_StatusCobranza.eSC_VIGENTE))
   vSQL = vSQL & "GROUP BY "
   vSQL = vSQL & "Cobranza.Fecha"
   vSQL = vSQL & ",Cobranza.moneda"
   vSQL = vSQL & ",Cobranza.Moneda"
   vSQL = vSQL & ",Cobranza.CambioABolivares"
   vSQL = vSQL & ",Cobranza.CodigoMoneda"
   vSQL = vSQL & ",Cobranza.ConsecutivoCompania"
h_EXIT: On Error GoTo 0
   fConstruirSQLDelReporteCobranzaPorDiaConVentasDiferidas = vSQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConstruirSQLDelReporteCobranzaPorDiaConVentasDiferidas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLDesgloseCobranzaConVentasDiferidas(ByVal ValDesgloseXCliente As Boolean, ByVal valConsecutivoCompania As String, _
                                     ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, _
                                     ByVal ValImprimirClienteUnico As String, ByVal ValtxtNombreDeCliente As String, _
                                     ByVal gMonedaLocalActual As Object, ByVal gUltimaTasaDeCambio As Object, valMostrarCobranzasPorVentasDiferidas As Boolean) As String
   Dim vSQL As String
   Dim vSQLTipoTarjeta As String
   Dim vSQLMonedaCobranza As String
   Dim gEnumProyecto As clsEnumAdministrativo
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   
   vSQLMonedaCobranza = "Cobranza.moneda"
                        
   vSQLTipoTarjeta = gUtilSQL.DfSQLCaseIfForEnum("cobranza.CualTarjeta", enum_Tarjeta.eTa_VISA, gEnumProyecto.fenumTarjetaToStringInArray(True), "")
   
   vSQL = "SELECT cobranza.Fecha, cobranza.Numero"
   vSQL = vSQL & ", (CASE WHEN Cobranza.StatusCobranza = '1' THEN 0 ELSE SUM(DocumentoCobrado.montoAbonado)  END )  AS TotalCobrado"
   vSQL = vSQL & ", cobranza.RetencionISLR , "
   vSQL = vSQL & gUtilSQL.fRoundNDecimales("SUM((DocumentoCobrado.montoAbonado / cobranza.TotalCobrado) * cobranza.CobradoEfectivo)", 0, "CobradoEfectivo, ")
   vSQL = vSQL & gUtilSQL.fRoundNDecimales("SUM((DocumentoCobrado.montoAbonado / cobranza.TotalCobrado) * cobranza.CobradoCheque)", 0, "CobradoCheque, ")
   vSQL = vSQL & " cobranza.NumerodelCheque ,"
   vSQL = vSQL & gUtilSQL.fRoundNDecimales("SUM((DocumentoCobrado.montoAbonado / cobranza.TotalCobrado) * cobranza.CobradoTarjeta)", 0, "CobradoTarjeta, ")
   vSQL = vSQL & vSQLMonedaCobranza & " as Moneda,"
   vSQL = vSQL & gUtilSQL.getIIF("cobranza.CobradoTarjeta = 0", gUtilSQL.fSimpleSqlValue(""), vSQLTipoTarjeta, True)
   vSQL = vSQL & " As TipoTarjeta , "
   vSQL = vSQL & " cobranza.NroDeLaTarjeta, "
   vSQL = vSQL & " cobranza.TotalOtros, cobranza.NombreBanco, cliente.Nombre, cobranza.RetencionIVA AS MontoIvaRetenido, cobranza.NroComprobanteRetIVA as NumeroComprobanteRetIVA, cuentaBancaria.CodigoMoneda"
   vSQL = vSQL & " FROM (cliente INNER JOIN cobranza ON (cliente.Codigo = cobranza.CodigoCliente) AND "
   vSQL = vSQL & "(cliente.ConsecutivoCompania = cobranza.ConsecutivoCompania))"
   vSQL = vSQL & "LEFT JOIN cuentaBancaria ON (cobranza.CodigoCuentaBancaria = cuentaBancaria.Codigo "
   vSQL = vSQL & " AND cuentaBancaria.ConsecutivoCompania = cobranza.ConsecutivoCompania)"
   vSQL = vSQL & fSQLMostrarCobranzasPorVentasDiferidas(valMostrarCobranzasPorVentasDiferidas)
   vSQL = vSQL & " WHERE (" & gUtilSQL.DfSQLDateValueBetween("cobranza.fecha", valFechaInicial, valFechaFinal) & ")"
   vSQL = vSQL & " AND cobranza.ConsecutivoCompania = " & valConsecutivoCompania
   vSQL = vSQL & " AND cobranza.StatusCobranza = " & gUtilSQL.fSimpleSqlValue("0")
   If ValDesgloseXCliente Then
      If ValImprimirClienteUnico Then
         vSQL = vSQL & " AND (Cliente.Nombre = " & gUtilSQL.fSimpleSqlValue(ValtxtNombreDeCliente) & ")"
      End If
   End If
   vSQL = vSQL & " GROUP BY"
   vSQL = vSQL & " cobranza.Numero"
   vSQL = vSQL & ", cobranza.Fecha"
   vSQL = vSQL & ", cobranza.StatusCobranza"
   vSQL = vSQL & ", cobranza.RetencionISLR"
   vSQL = vSQL & ", cobranza.NumerodelCheque"
   vSQL = vSQL & ", Cobranza.moneda"
   vSQL = vSQL & ", cobranza.CobradoTarjeta"
   vSQL = vSQL & ", cobranza.CualTarjeta"
   vSQL = vSQL & ", cobranza.NroDeLaTarjeta"
   vSQL = vSQL & ", cobranza.TotalOtros"
   vSQL = vSQL & ", cobranza.NombreBanco"
   vSQL = vSQL & ", cliente.Nombre"
   vSQL = vSQL & ", cobranza.RetencionIVA"
   vSQL = vSQL & ", cobranza.NroComprobanteRetIVA"
   vSQL = vSQL & ", cuentaBancaria.CodigoMoneda"
   Set gEnumProyecto = Nothing
h_EXIT:
   fSQLDesgloseCobranzaConVentasDiferidas = vSQL
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLDesgloseCobranzaConVentasDiferidas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

