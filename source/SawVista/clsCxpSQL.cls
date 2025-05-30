VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsCxpSQL"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit
Private Const CM_FILE_NAME As String = "clsCxpSQL"
Private Const CM_MESSAGE_NAME As String = "SQL Cxp"
Private Const CM_MODULO_CxP As Integer = 3
Private Const CM_MODULO_CxC As Integer = 2
    
Private Enum eCxpOPT
eCxpOPT_ANALISIS_DE_VENCIMIENTO = 0
eCxpOPT_CxP_ENTRE_FECHAS
eCxpOPT_LIBRO_DE_COMPRAS
eCxpOPT_ESTADO_DE_CUENTA
eCxpOPT_ANALISIS_CxP_HISTORICO
eCxpOPT_CxP_PENDIENTE_ENTRE_FECHAS
eCxpOPT_HISTORICO_PROVEEDOR
eCxpOPT_LISTA_DE_PROVEEDORES
eCxpOPT_PROVEEDORES_SIN_MOVIMIENTOS
eCxpOPT_ANALISIS_DE_VENCIMIENTO_A_UNA_FECHA
eCxpOPT_ANALISIS_DE_VENCIMIENTO_ENTRE_FECHA
eCxpOPT_RETENCION_IVA_EN_CXP
End Enum
  
Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function
Private Function OPT_CxP_ENTRE_FECHAS() As Integer
   OPT_CxP_ENTRE_FECHAS = 1
End Function

Public Function fSQLEstadoDeCuenta(ByVal valsqlTipoDeCxP As String, _
                                    ByVal valReporteEnMonedaLocal As Boolean, _
                                     ByVal valUsarCambioOriginal As Boolean, _
                                      ByVal valConsecutivoCompania As String, _
                                       ByVal ValImprimirProveedorUnico As Boolean, _
                                        ByVal ValCodigoProveedor As String, _
                                         ByVal gMonedaLocalActual As Object, _
                                          ByVal gUltimaTasaDeCambio As Object) As String
   Dim Sql As String
   Dim sqlTipoDeCxP As String
   Dim sqlTipoDeAnticipo As String
   Dim sqlDateDiffCxP As String
   Dim sqlMontoCxP As String
   Dim sqlMontoAnticipo As String
   Dim sqlStatusCxP As String
   Dim sqlStatusAnticipo As String
   Dim sqlCambioBsCxP As String
   Dim sqlCambioBsAnticipo As String
   Dim sqlMonedaCxP As String
   Dim sqlMonedaAnticipo As String
   Dim ReporteEnMonedaLocal As Boolean
   On Error GoTo h_ERROR
   sqlTipoDeCxP = valsqlTipoDeCxP
   sqlTipoDeAnticipo = gUtilSQL.fSimpleSqlValue("Pagado")
   sqlDateDiffCxP = gUtilSQL.getDateDiff("d", "CxP.FechaVencimiento", gUtilSQL.fDateToSQLValue(gUtilDate.getFechaDeHoy))
   sqlMontoCxP = "((CxP.MontoExento + CxP.MontoGravado + CxP.MontoIva) - CxP.MontoAbonado)"
   sqlMontoAnticipo = "(Anticipo.MontoTotal - (Anticipo.MontoUsado + Anticipo.MontoDevuelto + Anticipo.MontoDiferenciaEnDevolucion))"
   sqlCambioBsCxP = "CxP.CambioABolivares"
   sqlCambioBsAnticipo = "Anticipo.Cambio"
   sqlMonedaCxP = "CxP.Moneda"
   sqlMonedaAnticipo = "Anticipo.Moneda"
   If valReporteEnMonedaLocal Then
      If valUsarCambioOriginal Then
         sqlCambioBsCxP = gMonedaLocalActual.fSQLConvierteMontoSiAplicaMasDecimales(gMonedaLocalActual.GetHoyCodigoMoneda, sqlCambioBsCxP, "CxP.Fecha")
         sqlCambioBsAnticipo = gMonedaLocalActual.fSQLConvierteMontoSiAplicaMasDecimales(gMonedaLocalActual.GetHoyCodigoMoneda, sqlCambioBsAnticipo, "Anticipo.Fecha")
      Else
         sqlCambioBsCxP = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("", sqlMonedaCxP, 1, valUsarCambioOriginal, "")
         sqlCambioBsAnticipo = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("", "Anticipo.Moneda", 1, valUsarCambioOriginal, "")
      End If
      sqlMontoCxP = gUtilSQL.getIIF("CxP.CodigoMoneda <> " & gMonedaLocalActual.fSQLCodigoMonedaALaFecha("CxP.Fecha"), _
                        gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCambioBsCxP, sqlMonedaCxP, sqlMontoCxP, valUsarCambioOriginal, ""), _
                        gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, sqlMontoCxP, "CxP.Fecha"), True)
      sqlMontoAnticipo = gUtilSQL.getIIF("Anticipo.CodigoMoneda <> " & gMonedaLocalActual.fSQLCodigoMonedaALaFecha("Anticipo.Fecha"), _
                        gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCambioBsAnticipo, "Anticipo.Moneda", sqlMontoAnticipo, valUsarCambioOriginal, ""), _
                        gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, sqlMontoAnticipo, "Anticipo.Fecha"), True)
      sqlMonedaCxP = gUtilSQL.getIIF(sqlMonedaCxP & " = " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda), sqlMonedaCxP, gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda), True)
      sqlMonedaAnticipo = gUtilSQL.getIIF(sqlMonedaAnticipo & " = " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda), sqlMonedaAnticipo, gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda), True)
   End If
   sqlStatusCxP = gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ABONADO) & ", " & _
                  gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_PORCANCELAR)
   sqlStatusAnticipo = gUtilSQL.fSQLSimpleValueForEnum(enum_StatusAnticipo.eSDA_PARCIALMENTE_DEVUELTO) & ", " & _
                  gUtilSQL.fSQLSimpleValueForEnum(enum_StatusAnticipo.eSDA_PARCIALMENTE_USADO) & ", " & _
                  gUtilSQL.fSQLSimpleValueForEnum(enum_StatusAnticipo.eSDA_VIGENTE)
   Sql = "SELECT "
   Sql = Sql & "Proveedor.CodigoProveedor AS Codigo, "
   Sql = Sql & "Proveedor.NombreProveedor AS Nombre, "
   Sql = Sql & "Proveedor.Direccion AS Direccion, "
   Sql = Sql & sqlMonedaCxP & " AS Moneda, "
   Sql = Sql & sqlCambioBsCxP & " AS CambioBs, "
   Sql = Sql & gUtilSQL.fSimpleSqlValue("0") & " AS TipoReporte, "
   Sql = Sql & gUtilSQL.fSimpleSqlValue("Cuentas por Pagar") & " AS TituloTipoReporte, "
   Sql = Sql & sqlTipoDeCxP & " AS TipoDocumento, "
   Sql = Sql & "CxP.Numero AS Numero, "
   Sql = Sql & "CxP.Fecha AS Fecha, "
   Sql = Sql & "CxP.FechaVencimiento AS FechaVencimiento, "
   Sql = Sql & gUtilSQL.getIIF(sqlDateDiffCxP & " > 0 ", sqlDateDiffCxP, "0", True) & " AS DiasVencidos, "
   Sql = Sql & gUtilSQL.getIIF(sqlDateDiffCxP & " > 0 ", sqlMontoCxP, "0", True) & " AS MontoVencido, "
   Sql = Sql & gUtilSQL.getIIF(sqlDateDiffCxP & " <=0 ", sqlMontoCxP, "0", True) & " AS MontoNoVencido "
   Sql = Sql & " FROM Proveedor INNER JOIN CxP ON "
   Sql = Sql & "Proveedor.ConsecutivoCompania = CxP.ConsecutivoCompania"
   Sql = Sql & " AND Proveedor.CodigoProveedor = CxP.CodigoProveedor"
   Sql = Sql & " WHERE Proveedor.ConsecutivoCompania = " & valConsecutivoCompania
   Sql = Sql & " AND CxP.Status IN (" & sqlStatusCxP & ")"
   If ValImprimirProveedorUnico Then
      Sql = Sql & " AND Proveedor.CodigoProveedor = " & gUtilSQL.fSimpleSqlValue(ValCodigoProveedor)
   End If
   Sql = Sql & "UNION SELECT "
   Sql = Sql & "Proveedor.CodigoProveedor AS Codigo, "
   Sql = Sql & "Proveedor.NombreProveedor AS Nombre, "
   Sql = Sql & "Proveedor.Direccion AS Direccion, "
   Sql = Sql & sqlMonedaAnticipo & " AS Moneda, "
   Sql = Sql & sqlCambioBsAnticipo & " AS CambioBs, "
   Sql = Sql & gUtilSQL.fSimpleSqlValue("1") & " AS TipoReporte, "
   Sql = Sql & gUtilSQL.fSimpleSqlValue("Anticipos") & " AS TituloTipoReporte, "
   Sql = Sql & sqlTipoDeAnticipo & " AS TipoDocumento, "
   Sql = Sql & "Anticipo.Numero AS Numero, "
   Sql = Sql & "Anticipo.Fecha AS Fecha, "
   Sql = Sql & gUtilSQL.fSimpleSqlValue("") & " AS FechaVencimiento, "
   Sql = Sql & "0 AS DiasVencidos, "
   Sql = Sql & "0 AS MontoVencido, "
   Sql = Sql & sqlMontoAnticipo & " AS MontoNoVencido "
   Sql = Sql & " FROM Proveedor INNER JOIN Anticipo ON "
   Sql = Sql & "Proveedor.ConsecutivoCompania = Anticipo.ConsecutivoCompania"
   Sql = Sql & " AND Proveedor.CodigoProveedor = Anticipo.CodigoProveedor"
   Sql = Sql & " WHERE Proveedor.ConsecutivoCompania = " & valConsecutivoCompania
   Sql = Sql & " AND Anticipo.Status IN (" & sqlStatusAnticipo & ")"
   Sql = Sql & " AND Anticipo.Tipo = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeAnticipo.eTDA_PAGADO)
   If ValImprimirProveedorUnico Then
      Sql = Sql & " AND Proveedor.CodigoProveedor = " & gUtilSQL.fSimpleSqlValue(ValCodigoProveedor)
   End If
   Sql = Sql & " AND Anticipo.EsUnaDevolucion = " & gUtilSQL.fBooleanToSqlValue(False)
   Sql = Sql & " ORDER BY Codigo, Moneda, TipoReporte, Fecha"
   fSQLEstadoDeCuenta = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLEstadoDeCuenta", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConstruirSqlDeListarProveedores(ByVal UsaCodigoProveedorEnPantalla As Boolean, _
                                                  ByVal valcmbOrdenadoPor As String, _
                                                   ByVal ValFormaDeEscogerCompaniaToString As String, _
                                                    ByVal valConsecutivoCompania As String) As String
   Dim Sql As String
   Dim gEnumTablaRetencion As clsEnumTablaRetencion
   On Error GoTo h_ERROR
   Set gEnumTablaRetencion = New clsEnumTablaRetencion
   Sql = "SELECT "
   If UsaCodigoProveedorEnPantalla Or valcmbOrdenadoPor = ValFormaDeEscogerCompaniaToString Then
      Sql = Sql & "CodigoProveedor" & gUtilSQL.CharConcat & gUtilSQL.fSimpleSqlValue(" - ") & gUtilSQL.CharConcat & "NombreProveedor AS Nombre, "
   Else
      Sql = Sql & "NombreProveedor AS Nombre, "
   End If
   Sql = Sql & "NumeroRif as NumeroRifProveedor, "
   Sql = Sql & "Direccion AS DireccionProveedor, "
   Sql = Sql & "Telefonos AS TelefonoProveedor, "
   Sql = Sql & "Fax AS FaxProveedor, "
   Sql = Sql & "Contacto AS ContactoProveedor, "
   Sql = Sql & "Proveedor.PorcentajeRetencionIVA AS PorcentajeDeIva, "
   Sql = Sql & "Proveedor.CodigoRetencionUsual AS CodigoRetencionUsual, "
   Sql = Sql & gUtilSQL.DfSQLCaseIfForEnum("Proveedor.TipoDePersona", enum_TipodePersonaRetencion.eTP_PJ_DOMICILIADA, _
               gEnumTablaRetencion.fEnumTipoDePersonaRetencionSiglasToStringInArray(True), "TipoDePersona")
   Sql = Sql & " FROM Proveedor"
   Sql = Sql & " WHERE Proveedor.ConsecutivoCompania = " & valConsecutivoCompania
   Sql = Sql & " ORDER BY "
   If valcmbOrdenadoPor = ValFormaDeEscogerCompaniaToString Then
      Sql = Sql & "Proveedor.CodigoProveedor"
   Else
      Sql = Sql & "Proveedor.NombreProveedor"
   End If
   fConstruirSqlDeListarProveedores = Sql
   Set gEnumTablaRetencion = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fConstruirSqlDeListarProveedores ", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLSaldoInicialAnalisisCxPHistoricoCxP(ByVal valDescontarAbonos As Boolean, _
                              ByVal valFechaInicial As Date, ByVal valConsecutivoCompania As String, _
                              ByVal ReporteEnMonedaLocal As Boolean, ByVal gUltimaTasaDeCambio As Object, _
                              ByVal gMonedaLocalActual As Object) As String
   Dim Sql As String
   Dim sqlCxPTableName As String
   Dim sqlMonto As String
   On Error GoTo h_ERROR
   sqlCxPTableName = "CxP_1"
   sqlMonto = "(" & sqlCxPTableName & ".MontoExento" & _
       " + " & sqlCxPTableName & ".MontoGravado" & _
       " + " & sqlCxPTableName & ".MontoIva) "
   If valDescontarAbonos Then
      sqlMonto = "(" & sqlMonto & " - " & sqlCxPTableName & ".MontoAbonado)"
   End If
   If ReporteEnMonedaLocal Then
      sqlMonto = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCxPTableName & ".CambioABolivares", _
                  sqlCxPTableName & ".Moneda", sqlMonto, True, "")
   
      sqlMonto = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, sqlMonto, sqlCxPTableName & ".fecha")
   End If
   Sql = "(SELECT SUM(" & sqlMonto & ") AS SaldoInicial"
   Sql = Sql & " FROM CxP AS " & sqlCxPTableName
   Sql = Sql & " WHERE " & sqlCxPTableName & ".ConsecutivoCompania = " & valConsecutivoCompania
   Sql = Sql & " AND " & sqlCxPTableName & ".CodigoProveedor = CxP.CodigoProveedor"
   Sql = Sql & " AND " & sqlCxPTableName & ".Moneda = CxP.Moneda"
   Sql = Sql & " AND " & sqlCxPTableName & ".Status <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ANULADO)
   Sql = Sql & " AND " & sqlCxPTableName & ".Fecha < " & gUtilSQL.fDateToSQLValue(valFechaInicial) & ")"
   fSQLSaldoInicialAnalisisCxPHistoricoCxP = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLSaldoInicialAnalisisCxPHistoricoCxP", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLSeparadorDeCamposStandardDelNroDocumentoOrigen() As String
   On Error Resume Next
   fSQLSeparadorDeCamposStandardDelNroDocumentoOrigen = gUtilSQL.CharConcat & gUtilSQL.DfChar(9) & gUtilSQL.CharConcat
   On Error GoTo 0
End Function

Private Function fSQLSaldoInicialAnalisisCxPHistoricoAnticipos(ByVal valDescontarAbonos As Boolean, _
                              ByVal valFechaInicial As Date, ByVal valConsecutivoCompania As String, _
                              ByVal ReporteEnMonedaLocal As Boolean, ByVal gUltimaTasaDeCambio As Object, _
                              ByVal gMonedaLocalActual As Object) As String
   Dim Sql As String
   Dim sqlAnticipoTableName As String
   Dim sqlMonto As String
   On Error GoTo h_ERROR
   sqlAnticipoTableName = "Anticipo_1"
   sqlMonto = sqlAnticipoTableName & ".MontoTotal"
   If valDescontarAbonos Then
      sqlMonto = "(" & sqlMonto & _
            " - (" & sqlAnticipoTableName & ".MontoUsado" & _
            " + " & sqlAnticipoTableName & ".MontoDevuelto" & _
            " + " & sqlAnticipoTableName & ".MontoDiferenciaEnDevolucion))"
   End If
   If ReporteEnMonedaLocal Then
      sqlMonto = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlAnticipoTableName & ".Cambio", _
               sqlAnticipoTableName & ".Moneda", sqlMonto, True, "")
      sqlMonto = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, sqlMonto, sqlAnticipoTableName & ".fecha")
   End If
   Sql = "(SELECT SUM(" & sqlMonto & ") AS SaldoInicial"
   Sql = Sql & " FROM Anticipo AS " & sqlAnticipoTableName
   Sql = Sql & " WHERE " & sqlAnticipoTableName & ".ConsecutivoCompania = " & valConsecutivoCompania
   Sql = Sql & " AND " & sqlAnticipoTableName & ".Tipo = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeAnticipo.eTDA_PAGADO)
   Sql = Sql & " AND " & sqlAnticipoTableName & ".CodigoProveedor = Anticipo.CodigoProveedor"
   Sql = Sql & " AND " & sqlAnticipoTableName & ".Moneda = Anticipo.Moneda"
   Sql = Sql & " AND " & sqlAnticipoTableName & ".Status <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusAnticipo.eSDA_ANULADO)
   Sql = Sql & " AND " & sqlAnticipoTableName & ".Fecha < " & gUtilSQL.fDateToSQLValue(valFechaInicial) & ")"
   fSQLSaldoInicialAnalisisCxPHistoricoAnticipos = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLSaldoInicialAnalisisCxPHistoricoAnticipos", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConstruirSQLDelReporteAnalisisDeVencimiento(ByVal valMonedaOriginal As Boolean, ByVal valPropAnalisisVencActual As Boolean, ByVal gUltimaTasaDeCambio As Object, _
                              ByVal gMonedaLocalActual As Object, ByVal NombreMonedaLocal As String, ByVal valPrimerVencimiento As Long, ByVal valSegundoVencimiento As Long, ByVal valTercerVencimiento As Long, _
                           ByVal ConsecutivoCompania As Long, ByVal EsReporteOrdenadoPorNombre As Boolean) As String
   Dim Sql As String
   Dim sqlDateDiff As String
   Dim sqlMtoRestante As String
   Dim sqlMtoAbonado As String
   Dim sqlMtoTotal As String
   Dim sqlMonedaReporte As String
   On Error GoTo h_ERROR
   Sql = ""
   sqlMtoTotal = "(cxP.MontoExento + " & "cxP.MontoGravado + " & "cxP.MontoIva)"
   sqlMtoAbonado = "cxP.MontoAbonado"
   sqlMtoRestante = "(" & sqlMtoTotal & " - " & sqlMtoAbonado & ")"
   sqlMonedaReporte = "cxP.Moneda"
   If valPropAnalisisVencActual Then
      If Not valMonedaOriginal Then
         sqlMtoRestante = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("cxP.CambioABolivares", "cxP.Moneda", sqlMtoRestante, True, "")
         sqlMtoTotal = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("cxP.CambioABolivares", "cxP.Moneda", sqlMtoTotal, True, "")
         sqlMonedaReporte = gUtilSQL.fSimpleSqlValue(NombreMonedaLocal)
         sqlMtoTotal = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.fCodigoMoneda(gUtilDate.getFechaDeHoy), sqlMtoTotal, "cxP.fecha")
         sqlMtoAbonado = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.fCodigoMoneda(gUtilDate.getFechaDeHoy), "cxP.MontoAbonado", "cxP.fecha")
         sqlMtoRestante = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.fCodigoMoneda(gUtilDate.getFechaDeHoy), sqlMtoRestante, "cxP.fecha")
      End If
      sqlDateDiff = gUtilSQL.getDateDiff("d", "cxP.FechaVencimiento", gUtilSQL.fDateToSQLValue(gUtilDate.getFechaDeHoy))
      Sql = "SELECT "
      Sql = Sql & "cxP.CodigoProveedor, "
      Sql = Sql & "proveedor.NombreProveedor, "
      Sql = Sql & "cxP.Moneda, "
      Sql = Sql & sqlMonedaReporte & " AS MonedaReporte, "
      Sql = Sql & gUtilSQL.getIIF(sqlDateDiff & " <= 0 ", sqlMtoRestante, "0", True) & " AS MtoNoVencido, "
      Sql = Sql & gUtilSQL.getIIF(sqlDateDiff & " > 0 AND " & sqlDateDiff & " <= " & valPrimerVencimiento, sqlMtoRestante, "0", True) & " AS Mto1Vto, "
      Sql = Sql & gUtilSQL.getIIF(sqlDateDiff & " > " & valPrimerVencimiento & " AND " & sqlDateDiff & " <= " & valSegundoVencimiento, sqlMtoRestante, "0", True) & " AS Mto2Vto, "
      Sql = Sql & gUtilSQL.getIIF(sqlDateDiff & " > " & valSegundoVencimiento & " AND " & sqlDateDiff & " <= " & valTercerVencimiento, sqlMtoRestante, "0", True) & " AS Mto3Vto, "
      Sql = Sql & gUtilSQL.getIIF(sqlDateDiff & " > " & valTercerVencimiento, sqlMtoRestante, "0", True) & " AS Mto4Vto, "
      Sql = Sql & valPrimerVencimiento & " AS Vto1, "
      Sql = Sql & valSegundoVencimiento & " AS Vto2, "
      Sql = Sql & valTercerVencimiento & " AS Vto3, "
      Sql = Sql & sqlDateDiff & " AS DiasVencidos, "
      Sql = Sql & sqlMtoRestante & " AS MtoRestante, "
      Sql = Sql & "cxP.FechaVencimiento, "
      Sql = Sql & "cxP.Numero, "
      Sql = Sql & "cxP.Fecha, "
      Sql = Sql & sqlMtoTotal & " AS MtoTotal"
      Sql = Sql & " FROM cxP INNER JOIN proveedor ON "
      Sql = Sql & "cxP.ConsecutivoCompania = proveedor.ConsecutivoCompania"
      Sql = Sql & " AND cxP.CodigoProveedor = proveedor.CodigoProveedor"
      Sql = Sql & " WHERE cxP.ConsecutivoCompania = " & ConsecutivoCompania
      Sql = Sql & " AND cxP.Status IN (" & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ABONADO) & ", " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_PORCANCELAR) & ")"
      Sql = Sql & " ORDER BY "
      Sql = Sql & "cxP.Moneda, "
      If EsReporteOrdenadoPorNombre Then
         Sql = Sql & "proveedor.NombreProveedor, "
      Else
         Sql = Sql & "cxP.CodigoProveedor, "
      End If
      Sql = Sql & "cxP.FechaVencimiento"
   End If
   fConstruirSQLDelReporteAnalisisDeVencimiento = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fConstruirSQLDelReporteAnalisisDeVencimiento", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLDelAnalisisDeCxPAUnaFecha(ByVal valTableName As String, ByVal valnombreParaTabla As String, ByVal valFecha As Date, ByVal valOrdenadoPorNombre As Boolean, ByVal valMonedaOriginal As Boolean, _
               ByVal valPropAnalisisVencActual As Boolean, ByVal gUltimaTasaDeCambio As Object, ByVal gMonedaLocalActual As Object, ByVal valPrimerVencimiento As Long, ByVal valSegundoVencimiento As Long, ByVal valTercerVencimiento As Long, ByVal ConsecutivoCompania As Long) As String
   Dim Sql As String
   Dim sqlDateDiff As String
   Dim sqlMtoRestante As String
   Dim sqlMtoTotal As String
   Dim tasaOrig As Boolean
   Dim sqlMtoAbonado As String
   On Error GoTo h_ERROR
   If valPropAnalisisVencActual Then
      sqlMtoTotal = "(" & valTableName & ".MontoExento + " & valTableName & ".MontoGravado" & " + " & valTableName & ".MontoIva)"
      sqlMtoAbonado = "(" & valTableName & ".MontoAbonado)"
      sqlMtoRestante = "(" & sqlMtoTotal & " - " & sqlMtoAbonado & ")"
      sqlDateDiff = gUtilSQL.getDateDiff("d", valTableName & ".FechaVencimiento", gUtilSQL.fDateToSQLValue(valFecha))
      If Not valMonedaOriginal Then
        sqlMtoRestante = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(valTableName & ".CambioABolivares", valTableName & ".Moneda", sqlMtoRestante, True, "")
        sqlMtoTotal = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(valTableName & ".CambioABolivares", valTableName & ".Moneda", sqlMtoTotal, True, "")
        sqlMtoTotal = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.fCodigoMoneda(gUtilDate.getFechaDeHoy), sqlMtoTotal, valTableName & ".fecha")
        sqlMtoAbonado = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.fCodigoMoneda(gUtilDate.getFechaDeHoy), sqlMtoAbonado, valTableName & ".fecha")
        sqlMtoRestante = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.fCodigoMoneda(gUtilDate.getFechaDeHoy), sqlMtoRestante, valTableName & ".fecha")
      End If
      Sql = "SELECT "
      Sql = Sql & valTableName & ".CodigoCliProv AS CodigoProveedor, "
      Sql = Sql & "proveedor.NombreProveedor, "
      Sql = Sql & valTableName & ".Moneda, "
      Sql = Sql & gUtilSQL.getIIF(sqlDateDiff & " <= 0 ", sqlMtoRestante, "0", True) & " AS MtoNoVencido, "
      Sql = Sql & gUtilSQL.getIIF(sqlDateDiff & " > 0 AND " & sqlDateDiff & " <= " & valPrimerVencimiento, sqlMtoRestante, "0", True) & " AS Mto1Vto, "
      Sql = Sql & gUtilSQL.getIIF(sqlDateDiff & " > " & valPrimerVencimiento & " AND " & sqlDateDiff & " <= " & valSegundoVencimiento, sqlMtoRestante, "0", True) & " AS Mto2Vto, "
      Sql = Sql & gUtilSQL.getIIF(sqlDateDiff & " > " & valSegundoVencimiento & " AND " & sqlDateDiff & " <= " & valTercerVencimiento, sqlMtoRestante, "0", True) & " AS Mto3Vto, "
      Sql = Sql & gUtilSQL.getIIF(sqlDateDiff & " > " & valTercerVencimiento, sqlMtoRestante, "0", True) & " AS Mto4Vto, "
      Sql = Sql & valPrimerVencimiento & " AS Vto1, "
      Sql = Sql & valSegundoVencimiento & " AS Vto2, "
      Sql = Sql & valTercerVencimiento & " AS Vto3, "
      Sql = Sql & sqlDateDiff & " AS DiasVencidos, "
      Sql = Sql & sqlMtoRestante & " AS MtoRestante, "
      Sql = Sql & valTableName & ".FechaVencimiento, "
      Sql = Sql & valTableName & ".Numero, "
      Sql = Sql & valTableName & ".Fecha, "
      Sql = Sql & sqlMtoTotal & " AS MtoTotal"
      Sql = Sql & " FROM " & valTableName & " INNER JOIN "
      Sql = Sql & "proveedor ON "
      Sql = Sql & valTableName & ".ConsecutivoCompania" & " = proveedor.ConsecutivoCompania"
      Sql = Sql & " AND " & valTableName & ".CodigoCliProv" & " = proveedor.CodigoProveedor "
      Sql = Sql & " WHERE " & valTableName & ".ConsecutivoCompania" & " = " & ConsecutivoCompania
      Sql = Sql & " AND " & valTableName & ".Status" & " IN (" & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ABONADO) & ", " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_PORCANCELAR) & ")"
      Sql = Sql & " AND " & valTableName & ".Idusuario" & " = " & gUtilSQL.fSimpleSqlValue(valnombreParaTabla)
      Sql = Sql & " AND " & valTableName & ".Idmodulo = " & gUtilSQL.fSimpleSqlValue(CM_MODULO_CxP)
      Sql = Sql & " AND " & valTableName & ".Fecha <= " & gUtilSQL.fDateToSQLValue(valFecha)
'      Sql = Sql & " AND " & valTableName & ".Fecha >= " & gUtilSQL.fDateToSQLValue(valFecha)
      Sql = Sql & " ORDER BY "
      Sql = Sql & valTableName & ".Moneda, "
      If valOrdenadoPorNombre Then
         Sql = Sql & "proveedor.NombreProveedor, "
      Else
         Sql = Sql & valTableName & ".CodigoCliProv, "
      End If
      Sql = Sql & valTableName & ".FechaVencimiento"
   End If
h_EXIT: On Error GoTo 0
   fSQLDelAnalisisDeCxPAUnaFecha = Sql
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLDelAnalisisDeCxPAUnaFecha", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLCxPEntreFechasCxPPendientesEntreFechas(ByVal valMonedaOriginal As Boolean, ByVal usaCambioOriginal As Boolean, ByVal valElementInComboBoxToString As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valInformeSeleccionado As Integer, ByVal valConsecutivoCompania As String, ByVal gMonedaLocalActual As Object, ByVal gUltimaTasaDeCambio As Object, ByVal valImprimeUno As Boolean, ByVal ValCodigoProveedor As String) As String
   Dim Sql As String
   Dim sqlStatusCxP As String
   Dim sqlMtoTotal As String
   Dim sqlMtoRestante As String
   Dim sqlMonedaReporte As String
   Dim sqlCambio As String
   Dim unSoloStatus As Boolean
   Dim statusABuscarChr As String
   Dim sqlMtoAbonado As String
   Dim gEnumProyecto As clsEnumAdministrativo
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   sqlStatusCxP = gUtilSQL.DfSQLCaseIfForEnum("CxP.Status", enum_StatusDocumento.eSD_PORCANCELAR, gEnumProyecto.fEnumStatusCxCToStringInArray(True, False), "")
   sqlMtoTotal = "(CxP.MontoExento + MontoGravado + MontoIva)"
   sqlMtoAbonado = "CxP.MontoAbonado"
   sqlMtoRestante = "(" & sqlMtoTotal & " - " & sqlMtoAbonado & ")"
   sqlMonedaReporte = "CxP.Moneda"
   sqlCambio = "CxP.CambioABolivares"
   If Not valMonedaOriginal Then
      sqlMonedaReporte = gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda)
      If usaCambioOriginal Then
         sqlCambio = gMonedaLocalActual.fSQLConvierteMontoSiAplicaMasDecimales(gMonedaLocalActual.GetHoyCodigoMoneda, sqlCambio, "CxP.Fecha")
      Else
         sqlCambio = gUtilSQL.getIIF("CxP.CodigoMoneda = " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMoneda) & " OR " & _
                                      "CxP.CodigoMoneda = " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMonedaAnt), _
                                      gMonedaLocalActual.fSQLConvierteMontoSiAplicaMasDecimales(gMonedaLocalActual.GetHoyCodigoMoneda, sqlCambio, "CxP.Fecha"), _
                                      gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("", "CxP.Moneda", 1, usaCambioOriginal, ""), True)
      End If
      sqlMtoTotal = gUtilSQL.getIIF("CxP.CodigoMoneda = " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMoneda) & " OR " & _
                                   "CxP.CodigoMoneda = " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMonedaAnt), _
                                   gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, sqlMtoTotal, "CxP.Fecha"), _
                                   gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCambio, "CxP.Moneda", sqlMtoTotal, usaCambioOriginal, ""), True)
      sqlMtoRestante = gUtilSQL.getIIF("CxP.CodigoMoneda = " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMoneda) & " OR " & _
                                   "CxP.CodigoMoneda = " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMonedaAnt), _
                                   gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, sqlMtoRestante, "CxP.Fecha"), _
                                   gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCambio, "CxP.Moneda", sqlMtoRestante, usaCambioOriginal, ""), True)
   End If
   unSoloStatus = False
   If valElementInComboBoxToString <> gDefgen.getSearchAllComboBox Then
      unSoloStatus = True
      statusABuscarChr = gUtilSQL.fSQLSimpleValueForEnum(gEnumProyecto.strStatusDocumentoToNum(valElementInComboBoxToString))
   End If
   Sql = "SELECT "
   Sql = Sql & "CxP.Numero, "
   Sql = Sql & "CxP.CodigoProveedor, "
   Sql = Sql & "Proveedor.NombreProveedor, "
   Sql = Sql & "CxP.Fecha, "
   Sql = Sql & "CxP.FechaVencimiento, "
   Sql = Sql & sqlStatusCxP & " AS StatusStr, "
   Sql = Sql & sqlMtoTotal & " AS MontoTotal, "
   Sql = Sql & sqlMtoRestante & " AS MontoRestante, "
   Sql = Sql & sqlMonedaReporte & " AS MonedaReporte, "
   Sql = Sql & "CxP.Moneda AS MonedaDocumento, "
   Sql = Sql & sqlCambio & " AS Cambio"
   Sql = Sql & " FROM CxP INNER JOIN Proveedor ON "
   Sql = Sql & "CxP.CodigoProveedor = Proveedor.CodigoProveedor"
   Sql = Sql & " AND CxP.ConsecutivoCompania = Proveedor.ConsecutivoCompania"
   Sql = Sql & " WHERE " & gUtilSQL.DfSQLDateValueBetween("CxP.Fecha", valFechaInicial, valFechaFinal)
   If valImprimeUno Then
      Sql = Sql & " AND CxP.CodigoProveedor = " & ValCodigoProveedor
   End If
   If unSoloStatus Then
      Sql = Sql & " AND CxP.Status = " & statusABuscarChr
   End If
   Sql = Sql & " AND CxP.ConsecutivoCompania = " & valConsecutivoCompania
   Sql = Sql & " ORDER BY "
   Sql = Sql & "CxP.Status, "
   Sql = Sql & "MonedaReporte, "
   Sql = Sql & "CxP.Fecha, "
   Sql = Sql & "CxP.Numero, "
   Sql = Sql & "CxP.CodigoProveedor"
   Set gEnumProyecto = Nothing
   fSQLCxPEntreFechasCxPPendientesEntreFechas = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLCxPEntreFechasCxPPendientesEntreFechas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLAnalisisCxPHistorico(ByVal valUsaCambioOriginal As Boolean, ByVal gUltimaTasaDeCambio As Object, ByVal gMonedaLocalActual As Object, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, _
                                 ByVal valConsecutivoCompania As String, ByVal valReporteEnMonedaLocal As Boolean, ByVal valImprimeUno As Boolean, ByVal ValCodigoProveedor As String, _
                                 ByVal valTipoDeAnticipo As String, ByVal valDocOrigenContabilizacionCxP As String, ByVal valDocOrigenContabilizacionAnticipo As String) As String
   Dim Sql As String
   Dim SQLCXP As String
   Dim SqlAnticipos As String
   Dim SqlPagos As String
   Dim SqlReposiciones As String
   Dim sqlOrderBy As String
   On Error GoTo h_ERROR
   SQLCXP = fSQLAnalisisCxPHistoricoCxP(valUsaCambioOriginal, gUltimaTasaDeCambio, gMonedaLocalActual, valFechaInicial, valFechaFinal, valConsecutivoCompania, valReporteEnMonedaLocal, valDocOrigenContabilizacionCxP, valImprimeUno, ValCodigoProveedor)
   SqlAnticipos = fSQLAnalisisCxPHistoricoAnticipos(valTipoDeAnticipo, valUsaCambioOriginal, gUltimaTasaDeCambio, gMonedaLocalActual, valFechaInicial, valFechaFinal, valConsecutivoCompania, valReporteEnMonedaLocal, valDocOrigenContabilizacionAnticipo, valImprimeUno, ValCodigoProveedor)
   SqlPagos = fSQLAnalisisCxPHistoricoPagos(valUsaCambioOriginal, gUltimaTasaDeCambio, gMonedaLocalActual, valFechaInicial, valFechaFinal, valConsecutivoCompania, valReporteEnMonedaLocal, valImprimeUno, ValCodigoProveedor)
   SqlReposiciones = fSQLAnalisisCxPHistoricoReposiciones(valUsaCambioOriginal, gUltimaTasaDeCambio, gMonedaLocalActual, valFechaInicial, valFechaFinal, valConsecutivoCompania, valReporteEnMonedaLocal, valImprimeUno, ValCodigoProveedor)
   sqlOrderBy = " ORDER BY Codigo, MonedaReporte, ColumnaParaGrupoTipoReporte, Fecha, ColumnaParaGrupoDetalle"
   Sql = SQLCXP
   Sql = Sql & " UNION " & SqlAnticipos
   Sql = Sql & " UNION " & SqlPagos
   Sql = Sql & " UNION " & SqlReposiciones
   Sql = Sql & sqlOrderBy
   fSQLAnalisisCxPHistorico = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLAnalisisCxPHistorico", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLAnalisisCxPHistoricoCxP(ByVal valUsaCambioOriginal As Boolean, ByVal gUltimaTasaDeCambio As Object, ByVal gMonedaLocalActual As Object, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, _
                                       ByVal valConsecutivoCompania As String, ByVal valReporteEnMonedaLocal As Boolean, ByVal valDocOrigenContabilizacion As String, ByVal valImprimeUno As Boolean, ByVal ValCodigoProveedor As String) As String
   Dim Sql As String
   Dim sqlTipoDeCxP As String
   Dim sqlStatusCxP As String
   Dim sqlMontoTotalCxP As String
   Dim sqlMontoActualCxP As String
   Dim sqlColumnaParaGrupoDetalle As String
   Dim SqlSaldoInicialSinAbonos As String
   Dim SqlSaldoInicialConAbonos As String
   Dim sqlStatusCxPAnulado As String
   Dim sqlMoneda As String
   Dim sqlCambio As String
   Dim sqlMtoAbonado As String
   Dim gEnumProyecto As clsEnumAdministrativo
   Dim gEnumReport As clsEnumReport
   Dim sqlMonedaReporte As String
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   Set gEnumReport = New clsEnumReport
   sqlTipoDeCxP = gUtilSQL.DfSQLCaseIfForEnum("cxP.TipoDeCxP", _
                  enum_TipoDeTransaccion.eTD_FACTURA, gEnumProyecto.fEnumTipoDeTransaccioToStringInArray(True, False), "")
   sqlStatusCxP = gUtilSQL.DfSQLCaseIfForEnum("cxP.Status", _
                  enum_StatusDocumento.eSD_PORCANCELAR, gEnumProyecto.fEnumStatusCxCToStringInArray(True, False), "")
   sqlStatusCxPAnulado = "cxP.Status = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ANULADO)
   sqlMontoTotalCxP = "(cxP.MontoExento" & _
                    " + cxP.MontoGravado" & _
                    " + cxP.MontoIva)"
   sqlMontoTotalCxP = gUtilSQL.getIIF(sqlStatusCxPAnulado, "0", sqlMontoTotalCxP, True)
   sqlMtoAbonado = "(cxP.MontoAbonado)"
   sqlMontoActualCxP = "(" & sqlMontoTotalCxP & " - " & sqlMtoAbonado & ")"
   sqlMontoActualCxP = gUtilSQL.getIIF(sqlStatusCxPAnulado, "0", sqlMontoActualCxP, True)
   sqlMoneda = "cxP.Moneda"
   sqlMonedaReporte = "cxP.Moneda"
   sqlCambio = "cxP.CambioABolivares"
   If valReporteEnMonedaLocal Then
'      If valUsaCambioOriginal Then
         sqlMontoTotalCxP = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCambio, sqlMoneda, sqlMontoTotalCxP, True, "")
         sqlMontoActualCxP = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCambio, sqlMoneda, sqlMontoActualCxP, True, "")
         sqlMontoTotalCxP = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.fCodigoMoneda(gUtilDate.getFechaDeHoy), sqlMontoTotalCxP, "cxp.fecha")
         sqlMtoAbonado = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.fCodigoMoneda(gUtilDate.getFechaDeHoy), "cxP.MontoAbonado", "cxp.fecha")
         sqlMontoActualCxP = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.fCodigoMoneda(gUtilDate.getFechaDeHoy), sqlMontoActualCxP, "cxp.fecha")
'      End If
      sqlMonedaReporte = gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda)
   End If
   SqlSaldoInicialSinAbonos = fSQLSaldoInicialAnalisisCxPHistoricoCxP(False, valFechaInicial, valConsecutivoCompania, valReporteEnMonedaLocal, _
                                    gUltimaTasaDeCambio, gMonedaLocalActual)
   SqlSaldoInicialConAbonos = fSQLSaldoInicialAnalisisCxPHistoricoCxP(True, valFechaInicial, valConsecutivoCompania, valReporteEnMonedaLocal, _
                                    gUltimaTasaDeCambio, gMonedaLocalActual)
   Sql = "SELECT "
   Sql = Sql & gUtilSQL.fSimpleSqlValue("0") & " AS ColumnaParaGrupoTipoReporte, "
   Sql = Sql & gUtilSQL.fSimpleSqlValue("Cuentas por Pagar") & " AS TituloDelGrupo, "
   Sql = Sql & "cxP.CodigoProveedor AS Codigo, "
   Sql = Sql & sqlMoneda & " AS Moneda, "
   Sql = Sql & sqlMonedaReporte & " AS MonedaReporte, "
   Sql = Sql & sqlTipoDeCxP & " AS TipoDocumento, "
   Sql = Sql & gUtilSQL.fCast(valDocOrigenContabilizacion, eTDSS_VARCHAR, "") & " AS ColumnaParaGrupoDetalle, "
   Sql = Sql & SqlSaldoInicialSinAbonos & " AS SaldoInicialSinAbonos, "
   Sql = Sql & SqlSaldoInicialConAbonos & " AS SaldoInicialConAbonos, "
   Sql = Sql & "cxP.Numero AS Numero, "
   Sql = Sql & gUtilSQL.DfSQLFormat("cxP.fecha", "dd/MM/yyyy", "") & " AS Fecha, "
   Sql = Sql & gUtilSQL.DfSQLFormat("cxP.FechaVencimiento", "dd/MM/yyyy", "") & " AS FechaVencimiento, "
   Sql = Sql & sqlStatusCxP & " AS Status, "
   Sql = Sql & sqlMontoActualCxP & " AS MontoActual, "
   Sql = Sql & sqlMontoTotalCxP & " AS MontoTotal, "
   Sql = Sql & "proveedor.NombreProveedor AS Nombre, "
   Sql = Sql & sqlCambio & " AS CambioBs, "
   Sql = Sql & gUtilSQL.fSimpleSqlValue("") & " AS TipoDocPagado, "
   Sql = Sql & gUtilSQL.fSimpleSqlValue("") & " AS NumeroDocPagado, "
   Sql = Sql & gUtilSQL.getIIF("Cxp.EstaAsociadoARendicion = 'S'", "cxP.MontoAbonado", "0") & " AS MontoAbonado "
   Sql = Sql & " FROM cxP INNER JOIN proveedor ON "
   Sql = Sql & "cxP.ConsecutivoCompania = proveedor.ConsecutivoCompania"
   Sql = Sql & " AND cxP.CodigoProveedor = proveedor.CodigoProveedor"
   Sql = Sql & " WHERE cxP.ConsecutivoCompania = " & valConsecutivoCompania
   If valImprimeUno Then
      Sql = Sql & " AND cxP.CodigoProveedor = " & ValCodigoProveedor
   End If
   Sql = Sql & " AND " & gUtilSQL.DfSQLDateValueBetween("cxP.Fecha", valFechaInicial, valFechaFinal)
h_EXIT: On Error GoTo 0
   fSQLAnalisisCxPHistoricoCxP = Sql
   Set gEnumProyecto = Nothing
   Set gEnumReport = Nothing
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLAnalisisCxPHistoricoCxP", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLAnalisisCxPHistoricoAnticipos(ByVal valTipoDeAnticipo As String, ByVal valUsaCambioOriginal As Boolean, ByVal gUltimaTasaDeCambio As Object, ByVal gMonedaLocalActual As Object, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, _
                                       ByVal valConsecutivoCompania As String, ByVal valReporteEnMonedaLocal As Boolean, ByVal valDocOrigenContabilizacion As String, ByVal valImprimeUno As Boolean, ByVal ValCodigoProveedor As String) As String
   Dim Sql As String
   Dim sqlTipoDeAnticipo As String
   Dim sqlStatusDeAnticipo As String
   Dim sqlMontoTotal As String
   Dim sqlMontoActual As String
   Dim sqlMontoNoDisponible As String
   Dim SqlSaldoInicialSinAbonos As String
   Dim SqlSaldoInicialConAbonos As String
   Dim sqlStatusAnticipoAnulado As String
   Dim sqlMoneda As String
   Dim sqlCambio As String
   Dim gEnumProyecto As clsEnumAdministrativo
   Dim gEnumReport As clsEnumReport
   Dim sqlMonedaReporte As String
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   Set gEnumReport = New clsEnumReport
   sqlTipoDeAnticipo = valTipoDeAnticipo
   sqlStatusDeAnticipo = gUtilSQL.DfSQLCaseIfForEnum("anticipo.Status", _
                        enum_StatusAnticipo.eSDA_VIGENTE, gEnumProyecto.fenumStatusAnticipoToStrinInArray(True), "")
   sqlMontoNoDisponible = "(anticipo.MontoUsado" & _
                        " + anticipo.MontoDevuelto" & _
                        " + anticipo.MontoDiferenciaEnDevolucion)"
   sqlStatusAnticipoAnulado = "anticipo.Status = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusAnticipo.eSDA_ANULADO)
   sqlMontoTotal = "anticipo.MontoTotal"
   sqlMontoTotal = gUtilSQL.getIIF(sqlStatusAnticipoAnulado, "0", sqlMontoTotal, True)
   sqlMontoActual = "(" & sqlMontoTotal & " - " & sqlMontoNoDisponible & ")"
   sqlMontoActual = gUtilSQL.getIIF(sqlStatusAnticipoAnulado, "0", sqlMontoActual, True)
   sqlMoneda = "anticipo.Moneda"
   sqlMonedaReporte = "anticipo.Moneda"
   sqlCambio = "anticipo.Cambio"
   If valReporteEnMonedaLocal Then
      If valUsaCambioOriginal Then
         sqlMontoTotal = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCambio, sqlMoneda, sqlMontoTotal, True, "")
         sqlMontoActual = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCambio, sqlMoneda, sqlMontoActual, True, "")
         sqlMontoNoDisponible = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.fCodigoMoneda(gUtilDate.getFechaDeHoy), sqlMontoNoDisponible, "anticipo.fecha")
         sqlMontoTotal = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.fCodigoMoneda(gUtilDate.getFechaDeHoy), "anticipo.MontoTotal", "anticipo.fecha")
         sqlMontoActual = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.fCodigoMoneda(gUtilDate.getFechaDeHoy), sqlMontoActual, "anticipo.fecha")
      End If
      sqlMonedaReporte = gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda)
   End If
   SqlSaldoInicialSinAbonos = fSQLSaldoInicialAnalisisCxPHistoricoAnticipos(False, valFechaInicial, valConsecutivoCompania, valReporteEnMonedaLocal, _
                                    gUltimaTasaDeCambio, gMonedaLocalActual)
   SqlSaldoInicialConAbonos = fSQLSaldoInicialAnalisisCxPHistoricoAnticipos(True, valFechaInicial, valConsecutivoCompania, valReporteEnMonedaLocal, _
                                    gUltimaTasaDeCambio, gMonedaLocalActual)
   Sql = "SELECT "
   Sql = Sql & gUtilSQL.fSimpleSqlValue("1") & " AS ColumnaParaGrupoTipoReporte, "
   Sql = Sql & gUtilSQL.fSimpleSqlValue("Anticipos") & " AS TituloDelGrupo, "
   Sql = Sql & "anticipo.CodigoProveedor AS Codigo, "
   Sql = Sql & sqlMoneda & " AS Moneda, "
   Sql = Sql & sqlMonedaReporte & " AS MonedaReporte, "
   Sql = Sql & sqlTipoDeAnticipo & " AS TipoDocumento, "
   Sql = Sql & gUtilSQL.fCast(valDocOrigenContabilizacion, eTDSS_VARCHAR, "") & " AS ColumnaParaGrupoDetalle, "
   Sql = Sql & SqlSaldoInicialSinAbonos & " AS SaldoInicialSinAbonos, "
   Sql = Sql & SqlSaldoInicialConAbonos & " AS SaldoInicialConAbonos, "
   Sql = Sql & "anticipo.Numero AS Numero, "
   Sql = Sql & gUtilSQL.DfCDateSQL(gUtilSQL.DfSQLFormat("anticipo.fecha", "dd/MM/yyyy", "")) & " AS Fecha, "
   Sql = Sql & gUtilSQL.fSimpleSqlValue("") & " AS FechaVencimiento, "
   Sql = Sql & sqlStatusDeAnticipo & " AS Status, "
   Sql = Sql & sqlMontoActual & " AS MontoActual, "
   Sql = Sql & sqlMontoTotal & " AS MontoTotal, "
   Sql = Sql & "proveedor.NombreProveedor AS Nombre, "
   Sql = Sql & sqlCambio & " AS CambioBs, "
   Sql = Sql & gUtilSQL.fSimpleSqlValue("") & " AS TipoDocPagado, "
   Sql = Sql & gUtilSQL.fSimpleSqlValue("") & " AS NumeroDocPagado, "
   Sql = Sql & "0 AS MontoAbonado "
   Sql = Sql & " FROM anticipo INNER JOIN proveedor ON "
   Sql = Sql & "anticipo.ConsecutivoCompania = proveedor.ConsecutivoCompania"
   Sql = Sql & " AND anticipo.CodigoProveedor = proveedor.CodigoProveedor"
   Sql = Sql & " WHERE anticipo.ConsecutivoCompania = " & valConsecutivoCompania
   If valImprimeUno Then
      Sql = Sql & " AND anticipo.CodigoProveedor = " & ValCodigoProveedor
   End If
   Sql = Sql & " AND " & gUtilSQL.DfSQLDateValueBetween("anticipo.Fecha", valFechaInicial, valFechaFinal)
   Sql = Sql & " AND anticipo.Tipo = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeAnticipo.eTDA_PAGADO)
   Sql = Sql & " AND anticipo.EsUnaDevolucion = " & gUtilSQL.fBooleanToSqlValue(False)
h_EXIT: On Error GoTo 0
   fSQLAnalisisCxPHistoricoAnticipos = Sql
   Set gEnumProyecto = Nothing
   Set gEnumReport = Nothing
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLAnalisisCxPHistoricoAnticipos", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
   
Private Function fSQLAnalisisCxPHistoricoPagos(ByVal valUsaCambioOriginal As Boolean, ByVal gUltimaTasaDeCambio As Object, ByVal gMonedaLocalActual As Object, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, _
                                       ByVal valConsecutivoCompania As String, ByVal valReporteEnMonedaLocal As Boolean, ByVal valImprimeUno As Boolean, ByVal ValCodigoProveedor As String) As String

   Dim Sql As String
   Dim sqlStatusPago As String
   Dim sqlMontoPago As String
   Dim sqlMontoCheque As String
   Dim sqlTipoDeDocPagado As String
   Dim sqlColumnaParaGrupoDetalle As String
   Dim sqlNumeroDocumentoPagado As String
   Dim sqlMontoAbonado As String
   Dim SqlSaldoInicialSinAbonos As String
   Dim SqlSaldoInicialConAbonos As String
   Dim sqlStatusPagoAnulado As String
   Dim sqlMonedaDocs As String
   Dim sqlMonedaPago As String
   Dim sqlCambio As String
   Dim gEnumProyecto As clsEnumAdministrativo
   Dim gEnumReport As clsEnumReport
   Dim sqlMonedaReporte As String
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   Set gEnumReport = New clsEnumReport
   sqlStatusPago = gUtilSQL.getIIF("pago.StatusOrdenDePago = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusPago.eSP_ANULADA), _
            gUtilSQL.fSimpleSqlValue(gEnumProyecto.enumStatusPagoToString(eSP_ANULADA)), _
            gUtilSQL.fSimpleSqlValue(gEnumProyecto.enumStatusPagoToString(eSP_VIGENTE)), True)
   sqlStatusPagoAnulado = "pago.StatusOrdenDePago = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusPago.eSP_ANULADA)
   sqlMontoPago = "pago.TotalDocumentos"
   'sqlMontoPago = "IGV_DocPagadosPorMoneda.SumaMoneda"
   sqlMontoPago = gUtilSQL.getIIF(sqlStatusPagoAnulado, "0", sqlMontoPago, True)
   sqlMontoCheque = "pago.MontoCheque"
   sqlMontoCheque = gUtilSQL.getIIF(sqlStatusPagoAnulado, "0", sqlMontoCheque, True)
   sqlTipoDeDocPagado = gUtilSQL.getIIF(sqlStatusPagoAnulado, gUtilSQL.fSimpleSqlValue(""), _
   gUtilSQL.DfSQLCaseIfForEnum("cxP.TipoDeCxP", _
                  enum_TipoDeTransaccion.eTD_FACTURA, gEnumProyecto.fEnumTipoDeTransaccioToStringInArray(True, False), ""), True)
   sqlNumeroDocumentoPagado = gUtilSQL.getIIF(sqlStatusPagoAnulado, gUtilSQL.fSimpleSqlValue(""), "documentoPagado.NumeroDelDocumentoPagado", True)
   sqlMontoAbonado = "documentoPagado.MontoAbonado"
   sqlMontoAbonado = gUtilSQL.getIIF(sqlStatusPagoAnulado, "0", sqlMontoAbonado, True)
   sqlMonedaDocs = "pago.Moneda" '"documentoPagado.CambioAMonedaDelPago"
   sqlMonedaPago = "cuentaBancaria.NombreDeLaMoneda"
   sqlCambio = "pago.CambioaBolivares"
   sqlMonedaReporte = "pago.Moneda"
   'sqlMonedaReporte = "Moneda.Nombre"
   If valReporteEnMonedaLocal Then
'      If valUsaCambioOriginal Then
        sqlMontoPago = "pago.TotalDocumentos"
        sqlMontoPago = gUtilSQL.getIIF(sqlStatusPagoAnulado, "0", sqlMontoPago, True)
        sqlMontoPago = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCambio, sqlMonedaPago, sqlMontoPago, True, "")
        sqlMontoAbonado = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCambio, sqlMonedaDocs, sqlMontoAbonado, True, "")
        sqlMontoCheque = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCambio, sqlMonedaDocs, sqlMontoCheque, True, "")
        sqlMontoPago = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.fCodigoMoneda(gUtilDate.getFechaDeHoy), sqlMontoPago, "pago.fecha")
        sqlMontoCheque = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.fCodigoMoneda(gUtilDate.getFechaDeHoy), sqlMontoCheque, "pago.fecha")
        sqlMontoAbonado = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.fCodigoMoneda(gUtilDate.getFechaDeHoy), sqlMontoAbonado, "pago.fecha")
        sqlMonedaReporte = gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda)
'      Else
'        sqlMontoAbonado = "documentoPagado.MontoAbonado / documentoPagado.CambioAMonedaDelPago"
'      End If
   End If
   sqlColumnaParaGrupoDetalle = gUtilSQL.fCast("pago.NumeroComprobante", eTDSS_VARCHAR, "") & gUtilSQL.CharConcat & gUtilSQL.fSimpleSqlValue(" ") & gUtilSQL.CharConcat & gUtilSQL.fCast(gUtilSQL.fSimpleSqlValue("0"), eTDSS_VARCHAR, "")
   SqlSaldoInicialSinAbonos = "0"
   SqlSaldoInicialConAbonos = fSQLSaldoInicialAnalisisCxPHistoricoPagos(valUsaCambioOriginal, gUltimaTasaDeCambio, gMonedaLocalActual, valConsecutivoCompania, valFechaInicial, valReporteEnMonedaLocal)
   Sql = "SELECT "
   Sql = Sql & gUtilSQL.fSimpleSqlValue("2") & " AS ColumnaParaGrupoTipoReporte, "
   Sql = Sql & gUtilSQL.fSimpleSqlValue("Pagos") & " AS TituloDelGrupo, "
   Sql = Sql & "pago.CodigoProveedor AS Codigo, "
   Sql = Sql & sqlMonedaDocs & " AS Moneda, "
   Sql = Sql & sqlMonedaReporte & " AS MonedaReporte, "
   Sql = Sql & gUtilSQL.fSimpleSqlValue("Pagos") & " AS TipoDocumento, "
   Sql = Sql & sqlColumnaParaGrupoDetalle & " AS ColumnaParaGrupoDetalle, "
   Sql = Sql & SqlSaldoInicialSinAbonos & " AS SaldoInicialSinAbonos, "
   Sql = Sql & SqlSaldoInicialConAbonos & " AS SaldoInicialConAbonos, "
   Sql = Sql & "pago.NumeroCheque AS Numero, "
   Sql = Sql & gUtilSQL.DfCDateSQL(gUtilSQL.DfSQLFormat("pago.fecha", "dd/MM/yyyy", "")) & " AS Fecha, "
   Sql = Sql & gUtilSQL.fSimpleSqlValue("") & " AS FechaVencimiento, "
   Sql = Sql & sqlStatusPago & " AS Status, "
   Sql = Sql & sqlMontoPago & " AS MontoActual, "
   Sql = Sql & sqlMontoCheque & " AS MontoTotal, "
   Sql = Sql & "proveedor.NombreProveedor AS Nombre, "
   Sql = Sql & sqlCambio & " AS CambioBs, "
   Sql = Sql & sqlTipoDeDocPagado & " AS TipoDocCobrado, "
   Sql = Sql & sqlNumeroDocumentoPagado & " AS NumeroDocCobrado, "
   Sql = Sql & sqlMontoAbonado & " AS MontoAbonado "
   Sql = Sql & " FROM proveedor INNER JOIN (("
   Sql = Sql & "cuentaBancaria INNER JOIN (pago"
   Sql = Sql & " INNER JOIN documentoPagado ON ("
   Sql = Sql & "pago.NumeroComprobante = documentoPagado.NumeroComprobante"
   Sql = Sql & ") AND (pago.ConsecutivoCompania = documentoPagado.ConsecutivoCompania"
   Sql = Sql & ")) ON (cuentaBancaria.Codigo = pago.CodigoCuentaBancaria"
   Sql = Sql & ") AND (cuentaBancaria.ConsecutivoCompania = pago.ConsecutivoCompania"
   Sql = Sql & ")) INNER JOIN cxP ON ("
   Sql = Sql & "cxP.Numero = documentoPagado.NumeroDelDocumentoPagado"
   Sql = Sql & ") AND (documentoPagado.ConsecutivoCompania = cxP.ConsecutivoCompania"
   Sql = Sql & ")) ON (proveedor.CodigoProveedor = pago.CodigoProveedor"
   Sql = Sql & ") AND (proveedor.ConsecutivoCompania = pago.ConsecutivoCompania"
   Sql = Sql & ") AND (proveedor.CodigoProveedor = cxP.CodigoProveedor"
   Sql = Sql & ") AND (proveedor.ConsecutivoCompania = cxP.ConsecutivoCompania)"
   Sql = Sql & "   INNER JOIN Moneda ON DocumentoPagado.CodigoMonedaDeCxP = Moneda.Codigo"
   Sql = Sql & " INNER JOIN IGV_DocPagadosPorMoneda  On"
   Sql = Sql & " (Pago.ConsecutivoCompania = IGV_DocPagadosPorMoneda.ConsecutivoCompania)"
   Sql = Sql & " AND (Pago.NumeroComprobante = IGV_DocPagadosPorMoneda.NumeroPago) "
   Sql = Sql & " AND (Moneda.Codigo = IGV_DocPagadosPorMoneda.CodigoMonedaDeCxP) "
   Sql = Sql & " WHERE pago.ConsecutivoCompania = " & valConsecutivoCompania
   If valImprimeUno Then
      Sql = Sql & " AND pago.CodigoProveedor = " & ValCodigoProveedor
   End If
   Sql = Sql & " AND " & gUtilSQL.DfSQLDateValueBetween("pago.Fecha", valFechaInicial, valFechaFinal)
   sqlTipoDeDocPagado = gUtilSQL.getIIF(sqlStatusPagoAnulado, gUtilSQL.fSimpleSqlValue(""), gUtilSQL.fSimpleSqlValue("Anticipo"), True)
   sqlNumeroDocumentoPagado = gUtilSQL.getIIF(sqlStatusPagoAnulado, gUtilSQL.fSimpleSqlValue(""), "anticipoPagado.NumeroAnticipo", True)
   sqlMontoAbonado = gUtilSQL.getIIF(sqlStatusPagoAnulado, "0", "anticipoPagado.MontoAplicado", True)
   sqlColumnaParaGrupoDetalle = gUtilSQL.fCast("pago.NumeroComprobante", eTDSS_VARCHAR, "") & gUtilSQL.CharConcat & gUtilSQL.fSimpleSqlValue(" ") & gUtilSQL.CharConcat & gUtilSQL.fCast(gUtilSQL.fSimpleSqlValue("1"), eTDSS_VARCHAR, "")
   Sql = Sql & " UNION "
   Sql = Sql & "SELECT "
   Sql = Sql & gUtilSQL.fSimpleSqlValue("2") & " AS ColumnaParaGrupoTipoReporte, "
   Sql = Sql & gUtilSQL.fSimpleSqlValue("Pagos") & " AS TituloDelGrupo, "
   Sql = Sql & "pago.CodigoProveedor AS Codigo, "
   Sql = Sql & sqlMonedaPago & " AS Moneda, "
   Sql = Sql & sqlMonedaReporte & " AS MonedaReporte, "
   Sql = Sql & gUtilSQL.fSimpleSqlValue("Pago") & " AS TipoDocumento, "
   Sql = Sql & sqlColumnaParaGrupoDetalle & " AS ColumnaParaGrupoDetalle, "
   Sql = Sql & SqlSaldoInicialSinAbonos & " AS SaldoInicialSinAbonos, "
   Sql = Sql & SqlSaldoInicialConAbonos & " AS SaldoInicialConAbonos, "
   Sql = Sql & "pago.NumeroCheque AS Numero, "
   Sql = Sql & gUtilSQL.DfCDateSQL(gUtilSQL.DfSQLFormat("pago.fecha", "dd/MM/yyyy", "")) & " AS Fecha, "
   Sql = Sql & gUtilSQL.fSimpleSqlValue("") & " AS FechaVencimiento, "
   Sql = Sql & sqlStatusPago & " AS Status, "
   Sql = Sql & sqlMontoCheque & " AS MontoActual, "
   Sql = Sql & sqlMontoPago & " AS MontoTotal, "
   Sql = Sql & "proveedor.NombreProveedor AS Nombre, "
   Sql = Sql & sqlCambio & " AS CambioBs, "
   Sql = Sql & sqlTipoDeDocPagado & " AS TipoDocCobrado, "
   Sql = Sql & sqlNumeroDocumentoPagado & " AS NumeroDocCobrado, "
   Sql = Sql & sqlMontoAbonado & " AS MontoAbonado "
   Sql = Sql & " FROM anticipo INNER JOIN (("
   Sql = Sql & "proveedor INNER JOIN ("
   Sql = Sql & "cuentaBancaria INNER JOIN pago ON ("
   Sql = Sql & "cuentaBancaria.Codigo = pago.CodigoCuentaBancaria"
   Sql = Sql & ") AND (cuentaBancaria.ConsecutivoCompania = pago.ConsecutivoCompania"
   Sql = Sql & ")) ON (proveedor.CodigoProveedor = pago.CodigoProveedor"
   Sql = Sql & ") AND (proveedor.ConsecutivoCompania = pago.ConsecutivoCompania"
   Sql = Sql & ")) INNER JOIN anticipoPagado ON ("
   Sql = Sql & "pago.NumeroComprobante = anticipoPagado.NumeroComprobante"
   Sql = Sql & ") AND (pago.ConsecutivoCompania = anticipoPagado.ConsecutivoCompania"
   Sql = Sql & ")) ON (anticipoPagado.ConsecutivoAnticipoUsado = anticipo.ConsecutivoAnticipo"
   Sql = Sql & ") AND (anticipoPagado.ConsecutivoCompania = anticipo.ConsecutivoCompania"
   Sql = Sql & ") AND (anticipo.CodigoProveedor = proveedor.CodigoProveedor"
   Sql = Sql & ") AND (anticipo.ConsecutivoCompania = proveedor.ConsecutivoCompania)"
   Sql = Sql & "   INNER JOIN Moneda ON AnticipoPagado.CodigoMoneda = Moneda.Codigo"
   Sql = Sql & " INNER JOIN IGV_DocPagadosPorMoneda  On"
   Sql = Sql & " (Pago.ConsecutivoCompania = IGV_DocPagadosPorMoneda.ConsecutivoCompania)"
   Sql = Sql & " AND (Pago.NumeroComprobante = IGV_DocPagadosPorMoneda.NumeroPago) "
   Sql = Sql & " AND (Moneda.Codigo = IGV_DocPagadosPorMoneda.CodigoMonedaDeCxP) "
   Sql = Sql & " WHERE pago.ConsecutivoCompania = " & valConsecutivoCompania
   If valImprimeUno Then
      Sql = Sql & " AND pago.CodigoProveedor = " & ValCodigoProveedor
   End If
   Sql = Sql & " AND " & gUtilSQL.DfSQLDateValueBetween("pago.Fecha", valFechaInicial, valFechaFinal)
   Sql = Sql & " AND anticipo.Tipo = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeAnticipo.eTDA_PAGADO)
h_EXIT: On Error GoTo 0
   fSQLAnalisisCxPHistoricoPagos = Sql
   Set gEnumProyecto = Nothing
   Set gEnumReport = Nothing
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLAnalisisCxPHistoricoPagos", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLSaldoInicialAnalisisCxPHistoricoPagos(ByVal valUsaCambioOriginal As Boolean, ByVal gUltimaTasaDeCambio As Object, ByVal gMonedaLocalActual As Object, ByVal valConsecutivoCompania As String, _
                                                   ByVal valFechaInicial As Date, ByVal valReporteEnMonedaLocal As Boolean) As String
   Dim Sql  As String
   Dim sqlPagoTableName As String
   Dim sqlMonto As String
   On Error GoTo h_ERROR
   sqlPagoTableName = "pago_1"
   sqlMonto = sqlPagoTableName & ".TotalDocumentos"
   If valReporteEnMonedaLocal Then
      If valUsaCambioOriginal Then
         sqlMonto = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlPagoTableName & ".CambioaBolivares", _
                  sqlPagoTableName & ".Moneda", sqlMonto, True, "")
      End If
   End If
   sqlMonto = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.fCodigoMoneda(gUtilDate.getFechaDeHoy), sqlMonto, sqlPagoTableName & ".fecha")
   Sql = "(SELECT SUM(" & sqlMonto & ") AS SaldoInicial"
   Sql = Sql & " FROM pago AS " & sqlPagoTableName
   Sql = Sql & " WHERE " & sqlPagoTableName & ".ConsecutivoCompania = " & valConsecutivoCompania
   Sql = Sql & " AND " & sqlPagoTableName & ".CodigoProveedor = pago.CodigoProveedor"
   Sql = Sql & " AND " & sqlPagoTableName & ".Moneda = pago.Moneda"
   Sql = Sql & " AND StatusOrdenDePago = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusPago.eSP_VIGENTE)
   Sql = Sql & " AND Fecha < " & gUtilSQL.fDateToSQLValue(valFechaInicial) & ")"
   fSQLSaldoInicialAnalisisCxPHistoricoPagos = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLSaldoInicialAnalisisCxPHistoricoPagos", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLAnalisisCxPHistoricoReposiciones(ByVal valUsaCambioOriginal As Boolean, ByVal gUltimaTasaDeCambio As Object, ByVal gMonedaLocalActual As Object, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, _
                                       ByVal valConsecutivoCompania As String, ByVal valReporteEnMonedaLocal As Boolean, ByVal valImprimeUno As Boolean, ByVal ValCodigoProveedor As String) As String
   Dim Sql As String
   Dim sqlStatusReposicion As String
   Dim sqlMontoReposicion As String
   Dim sqlMontoCheque As String
   Dim sqlTipoDeDocPagado As String
   Dim sqlColumnaParaGrupoDetalle As String
   Dim sqlNumeroDocumentoPagado As String
   Dim sqlMontoAbonado As String
   Dim SqlSaldoInicialSinAbonos As String
   Dim SqlSaldoInicialConAbonos As String
   Dim sqlStatusReposicionAnulada As String
   Dim sqlMonedaDocs As String
   Dim sqlMonedaReposicion As String
   Dim sqlCambio As String
   Dim gEnumProyecto As clsEnumAdministrativo
   Dim gEnumReport As clsEnumReport
   Dim sqlMonedaReporte As String
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   Set gEnumReport = New clsEnumReport
   sqlStatusReposicionAnulada = "Adm.Rendicion.StatusRendicion = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusRendicion.eSRd_ANULADA)
   sqlStatusReposicion = gUtilSQL.getIIF(sqlStatusReposicionAnulada, _
            gUtilSQL.fSimpleSqlValue(gEnumProyecto.enumStatusRendicionToString(enum_StatusRendicion.eSRd_ANULADA)), _
            gUtilSQL.getIIF("Adm.Rendicion.StatusRendicion = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusRendicion.eSRd_CERRADA), _
            gUtilSQL.fSimpleSqlValue(gEnumProyecto.enumStatusRendicionToString(enum_StatusRendicion.eSRd_CERRADA)), _
            gUtilSQL.fSimpleSqlValue(gEnumProyecto.enumStatusRendicionToString(enum_StatusRendicion.eSRd_EN_PROCESO)), _
            True), True)
   sqlMontoReposicion = " (select Sum( Adm.DetalleDeRendicion.MontoExento + Adm.DetalleDeRendicion.MontoGravable + Adm.DetalleDeRendicion.MontoIVA) from Adm.DetalleDeRendicion where consecutivoCompania = Adm.Rendicion.ConsecutivoCompania "
   sqlMontoReposicion = sqlMontoReposicion + "and ConsecutivoRendicion = Adm.Rendicion.Consecutivo "
   sqlMontoReposicion = sqlMontoReposicion + " and Adm.DetalleDeRendicion.CodigoProveedor = Proveedor.CodigoProveedor )"
   sqlMontoReposicion = gUtilSQL.getIIF(sqlStatusReposicionAnulada, "0", sqlMontoReposicion, True)
   sqlMontoCheque = "0"
   sqlTipoDeDocPagado = gUtilSQL.getIIF(sqlStatusReposicionAnulada, gUtilSQL.fSimpleSqlValue(""), gUtilSQL.fSimpleSqlValue("Factura"), True)
   sqlNumeroDocumentoPagado = gUtilSQL.getIIF(sqlStatusReposicionAnulada, gUtilSQL.fSimpleSqlValue(""), "Adm.DetalleDeRendicion.NumeroDocumento", True)
   sqlMontoAbonado = " Adm.DetalleDeRendicion.MontoExento + Adm.DetalleDeRendicion.MontoGravable + Adm.DetalleDeRendicion.MontoIVA "
   sqlMontoAbonado = gUtilSQL.getIIF(sqlStatusReposicionAnulada, "0", sqlMontoAbonado, True)
   sqlMonedaDocs = "cuentaBancaria.NombreDeLaMoneda"
   sqlMonedaReposicion = "cuentaBancaria.NombreDeLaMoneda"
   sqlCambio = "1"
   sqlMonedaReporte = "cuentaBancaria.NombreDeLaMoneda"
   If valReporteEnMonedaLocal Then
        sqlMontoReposicion = "Adm.Rendicion.TotalGastos"
        sqlMontoReposicion = gUtilSQL.getIIF(sqlStatusReposicionAnulada, "0", sqlMontoReposicion, True)
        sqlMontoReposicion = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCambio, sqlMonedaReposicion, sqlMontoReposicion, True, "")
        sqlMontoAbonado = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCambio, sqlMonedaDocs, sqlMontoAbonado, True, "")
        sqlMontoCheque = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCambio, sqlMonedaDocs, sqlMontoCheque, True, "")
        sqlMontoReposicion = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.fCodigoMoneda(gUtilDate.getFechaDeHoy), sqlMontoReposicion, "Adm.Rendicion.FechaCierre")
        sqlMontoCheque = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.fCodigoMoneda(gUtilDate.getFechaDeHoy), sqlMontoCheque, "Adm.Rendicion.FechaCierre")
        sqlMontoAbonado = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.fCodigoMoneda(gUtilDate.getFechaDeHoy), sqlMontoAbonado, "Adm.Rendicion.FechaCierre")
        sqlMonedaReporte = gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda)
   End If
   sqlColumnaParaGrupoDetalle = gUtilSQL.fCast("Adm.Rendicion.Consecutivo", eTDSS_VARCHAR, "") & gUtilSQL.CharConcat & gUtilSQL.fSimpleSqlValue(" ") & gUtilSQL.CharConcat & gUtilSQL.fCast(gUtilSQL.fSimpleSqlValue("0"), eTDSS_VARCHAR, "")
   SqlSaldoInicialSinAbonos = "0"
   SqlSaldoInicialConAbonos = fSQLSaldoInicialAnalisisCxPHistoricoReposicion(valUsaCambioOriginal, gUltimaTasaDeCambio, gMonedaLocalActual, valConsecutivoCompania, valFechaInicial, valReporteEnMonedaLocal)
   Sql = "SELECT "
   Sql = Sql & gUtilSQL.fSimpleSqlValue("3") & " AS ColumnaParaGrupoTipoReporte, "
   Sql = Sql & gUtilSQL.fSimpleSqlValue("Reposiciones de Caja Chica") & " AS TituloDelGrupo, "
   Sql = Sql & "Adm.DetalleDeRendicion.CodigoProveedor  AS Codigo, "
   Sql = Sql & sqlMonedaDocs & " AS Moneda, "
   Sql = Sql & sqlMonedaReporte & " AS MonedaReporte, "
   Sql = Sql & gUtilSQL.fSimpleSqlValue("Reposición") & " AS TipoDocumento, "
   Sql = Sql & sqlColumnaParaGrupoDetalle & " AS ColumnaParaGrupoDetalle, "
   Sql = Sql & SqlSaldoInicialSinAbonos & " AS SaldoInicialSinAbonos, "
   Sql = Sql & SqlSaldoInicialConAbonos & " AS SaldoInicialConAbonos, "
   Sql = Sql & "Adm.Rendicion.Numero AS Numero, "
   Sql = Sql & gUtilSQL.DfCDateSQL(gUtilSQL.DfSQLFormat("Adm.Rendicion.FechaCierre", "dd/MM/yyyy", "")) & " AS Fecha, "
   Sql = Sql & gUtilSQL.fSimpleSqlValue("") & " AS FechaVencimiento, "
   Sql = Sql & sqlStatusReposicion & " AS Status, "
   Sql = Sql & sqlMontoReposicion & " AS MontoActual, "
   Sql = Sql & sqlMontoCheque & " AS MontoTotal, "
   Sql = Sql & "proveedor.NombreProveedor AS Nombre, "
   Sql = Sql & sqlCambio & " AS CambioBs, "
   Sql = Sql & sqlTipoDeDocPagado & " AS TipoDocCobrado, "
   Sql = Sql & sqlNumeroDocumentoPagado & " AS NumeroDocCobrado, "
   Sql = Sql & sqlMontoAbonado & " AS MontoAbonado "
   Sql = Sql & " FROM  Adm.DetalleDeRendicion INNER JOIN "
   Sql = Sql & "Adm.Rendicion On (Adm.DetalleDeRendicion.ConsecutivoCompania=Adm.Rendicion.ConsecutivoCompania and Adm.DetalleDeRendicion.ConsecutivoRendicion=Adm.Rendicion.Consecutivo )"
   Sql = Sql & " INNER JOIN proveedor ON ("
   Sql = Sql & "proveedor.CodigoProveedor = Adm.DetalleDeRendicion.CodigoProveedor "
   Sql = Sql & ") AND (proveedor.ConsecutivoCompania = Adm.DetalleDeRendicion.ConsecutivoCompania"
   Sql = Sql & ") INNER JOIN  CuentaBancaria ON (cuentaBancaria.Codigo = Adm.Rendicion.CodigoCtaBancariaCajaChica "
   Sql = Sql & ") AND (cuentaBancaria.ConsecutivoCompania = Adm.Rendicion.ConsecutivoCompania"
   Sql = Sql & ") INNER JOIN Moneda ON cuentaBancaria.CodigoMoneda = Moneda.Codigo"
   Sql = Sql & " WHERE Adm.DetalleDeRendicion .ConsecutivoCompania = " & valConsecutivoCompania
   If valImprimeUno Then
      Sql = Sql & " AND Adm.DetalleDeRendicion.CodigoProveedor = " & ValCodigoProveedor
   End If
   Sql = Sql & " AND " & gUtilSQL.DfSQLDateValueBetween("Adm.DetalleDeRendicion.Fecha", valFechaInicial, valFechaFinal)
   Sql = Sql & " AND " & "Adm.Rendicion.StatusRendicion = " & gUtilSQL.fSimpleSqlValue(enum_StatusRendicion.eSRd_CERRADA)
h_EXIT: On Error GoTo 0
   fSQLAnalisisCxPHistoricoReposiciones = Sql
   Set gEnumProyecto = Nothing
   Set gEnumReport = Nothing
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLAnalisisCxPHistoricoReposiciones", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Private Function fSQLSaldoInicialAnalisisCxPHistoricoReposicion(ByVal valUsaCambioOriginal As Boolean, ByVal gUltimaTasaDeCambio As Object, ByVal gMonedaLocalActual As Object, ByVal valConsecutivoCompania As String, _
                                                   ByVal valFechaInicial As Date, ByVal valReporteEnMonedaLocal As Boolean) As String
   Dim Sql  As String
   Dim sqlTableName As String
   Dim sqlMonto As String
   On Error GoTo h_ERROR
   sqlTableName = "Rendicion_1"
   sqlMonto = "Adm.DetalleDeRendicion.MontoExento + Adm.DetalleDeRendicion.MontoGravable + Adm.DetalleDeRendicion.MontoIVA"
   sqlMonto = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.fCodigoMoneda(gUtilDate.getFechaDeHoy), sqlMonto, sqlTableName & ".fechaCierre")
   Sql = "(SELECT SUM(" & sqlMonto & ")"
   Sql = Sql & " FROM Adm.Rendicion AS " & sqlTableName
   Sql = Sql & " INNER JOIN Adm.DetalleDeRendicion On (Adm.DetalleDeRendicion.ConsecutivoCompania=Rendicion_1.ConsecutivoCompania and Adm.DetalleDeRendicion.ConsecutivoRendicion=Rendicion_1.Consecutivo )"
   Sql = Sql & " WHERE " & sqlTableName & ".ConsecutivoCompania = " & valConsecutivoCompania
   Sql = Sql & " AND " & "Adm.DetalleDeRendicion" & ".CodigoProveedor = Proveedor.CodigoProveedor"
   Sql = Sql & " AND Rendicion_1.StatusRendicion = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusRendicion.eSRd_CERRADA)
   Sql = Sql & " AND Rendicion_1.TipoDeDocumento = 1 "
   Sql = Sql & " AND Rendicion_1.FechaCierre < " & gUtilSQL.fDateToSQLValue(valFechaInicial) & ") "
   fSQLSaldoInicialAnalisisCxPHistoricoReposicion = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLSaldoInicialAnalisisCxPHistoricoReposicion", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function fSQLActualizaAsientoComprobantesDeCxp() As String
   Dim Sql  As String
   On Error GoTo h_ERROR
   Sql = "Update Asiento"
   Sql = Sql & " SET ASIENTO.MontoDebe=(CASE ASIENTO.MontoDebe WHEN 0 THEN 0 ELSE ASIENTO.MontoDebe/1000 END),"
   Sql = Sql & "ASIENTO.MontoHaber= (CASE ASIENTO.MontoHaber WHEN 0 THEN 0 ELSE ASIENTO.MontoHaber/1000 END)"
   Sql = Sql & " FROM COMPROBANTE INNER JOIN cxP"
   Sql = Sql & " ON ((COMPROBANTE.NoDocumentoOrigen = cxP.CodigoProveedor + CHAR(9) + cxP.Numero)"
   Sql = Sql & " AND COMPROBANTE.GeneradoPor = ';'"
   Sql = Sql & " AND cxP.Fecha<'01/01/2008' AND cxP.AnoDeAplicacion >= '2008')"
   Sql = Sql & " AND (cxP.Fecha<'01/01/2008' AND COMPROBANTE.Fecha >= '01/01/2008')"
   Sql = Sql & " AND (COMPROBANTE.TotalDebe = CxP.MontoExento + MontoGravado + MontoIva)"
   Sql = Sql & " INNER JOIN Asiento ON(Asiento.NumeroComprobante=COMPROBANTE.Numero)"
   Sql = Sql & " INNER JOIN PERIODO ON"
   Sql = Sql & " (Cxp.ConsecutivoCompania = PERIODO.consecutivoCompania)"
   Sql = Sql & " AND (PERIODO.ConsecutivoPeriodo = COMPROBANTE.ConsecutivoPeriodo)"
   Sql = Sql & " AND (PERIODO.ConsecutivoPeriodo = ASIENTO.ConsecutivoPeriodo)"
   fSQLActualizaAsientoComprobantesDeCxp = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLActualizaAsientoComprobantesDeCxp", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLActualizaComprobantesDeCxp() As String
   Dim Sql  As String
   On Error GoTo h_ERROR
   Sql = "Update COMPROBANTE"
   Sql = Sql & " SET COMPROBANTE.TotalDebe= (CASE COMPROBANTE.TotalDebe WHEN 0 THEN 0 ELSE COMPROBANTE.TotalDebe/1000 END),"
   Sql = Sql & " COMPROBANTE.TotalHaber= (CASE COMPROBANTE.TotalHaber WHEN 0 THEN 0 ELSE COMPROBANTE.TotalHaber/1000 END)"
   Sql = Sql & " FROM COMPROBANTE INNER JOIN cxP"
   Sql = Sql & " ON (COMPROBANTE.NoDocumentoOrigen = cxP.CodigoProveedor + CHAR(9) + cxP.Numero)"
   Sql = Sql & " AND COMPROBANTE.GeneradoPor = ';'"
   Sql = Sql & " AND (cxP.Fecha<'01/01/2008' AND cxP.AnoDeAplicacion >= '2008')"
   Sql = Sql & " AND (cxP.Fecha<'01/01/2008' AND COMPROBANTE.Fecha >= '01/01/2008')"
   Sql = Sql & " AND (COMPROBANTE.TotalDebe = CxP.MontoExento + MontoGravado + MontoIva)"
   Sql = Sql & " INNER JOIN PERIODO ON"
   Sql = Sql & " (Cxp.ConsecutivoCompania = PERIODO.consecutivoCompania) AND"
   Sql = Sql & " (PERIODO.ConsecutivoPeriodo = COMPROBANTE.ConsecutivoPeriodo)"
   fSQLActualizaComprobantesDeCxp = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLActualizaComprobantesDeCxp", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLDelReporteAnalisisDeVencimientoEntreFechas(ByVal valAnalisisDeVencimientoPor As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valConsecutivoCompania As String, ByVal valNombreMonedaLocal As String, ByVal valMonedaOriginal As Boolean, ByRef gMonedaLocalActual As Object, ByRef InsAdmPropAnalisisVenc As Object, ByRef gUltimaTasaDeCambio As Object) As String
   Dim Sql As String
   Dim sqlMtoRestante As String
   Dim sqlDateDiff As String
   Dim sqlMtoTotal As String
   Dim sqlCambio As String
   Dim sqlMoneda As String
   Dim sqlFecha As String
   Dim sqlMtoAbonado As String
   Dim sqlMonedaReporte  As String
   On Error GoTo h_ERROR
   Set gEnumReport = New clsEnumReport
   If InsAdmPropAnalisisVenc.fBuscaValoresDeLasPropAnalisisVencActual Then
      sqlMtoTotal = "(" & "CxP.MontoExento" & " + " & "CxP.MontoGravado" & " + " & "CxP.MontoIva)"
      sqlMtoAbonado = "CxP.MontoAbonado"
      sqlMtoRestante = "(" & sqlMtoTotal & " - " & sqlMtoAbonado & ")"
      sqlDateDiff = gUtilSQL.getDateDiff("d", "CxP.FechaVencimiento", gUtilSQL.fDateToSQLValue(gUtilDate.getFechaDeHoy))
      sqlCambio = "CxP.CambioABolivares"
      sqlMoneda = "CxP.Moneda"
      sqlFecha = "CxP.Fecha"
      sqlMonedaReporte = "CxP.Moneda"
      If Not valMonedaOriginal Then
        sqlMtoRestante = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCambio, sqlMoneda, sqlMtoRestante, True, "")
        sqlMtoTotal = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCambio, sqlMoneda, sqlMtoTotal, True, "")
        sqlMonedaReporte = gUtilSQL.fSimpleSqlValue(valNombreMonedaLocal)
        sqlMtoTotal = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.fCodigoMoneda(gUtilDate.getFechaDeHoy), sqlMtoTotal, "CxP.fecha")
        sqlMtoAbonado = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.fCodigoMoneda(gUtilDate.getFechaDeHoy), "CxP.MontoAbonado", "CxP.fecha")
        sqlMtoRestante = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.fCodigoMoneda(gUtilDate.getFechaDeHoy), sqlMtoRestante, "CxP.fecha")
      End If
      Sql = "SELECT "
      Sql = Sql & sqlMoneda & ", "
      Sql = Sql & sqlMonedaReporte & " AS MonedaReporte, "
      Sql = Sql & gUtilSQL.fMonth(sqlFecha, "") & " AS Mes, "
      Sql = Sql & gUtilSQL.fYear(sqlFecha, "") & " AS Ano, "
      Sql = Sql & "CxP.CodigoProveedor, "
      Sql = Sql & "proveedor.NombreProveedor, "
      Sql = Sql & "CxP.Numero, "
      Sql = Sql & sqlFecha & ", "
      Sql = Sql & "CxP.FechaVencimiento, "
      Sql = Sql & sqlDateDiff & " AS DiasVencidos, "
      Sql = Sql & gUtilSQL.getIIF(sqlDateDiff & " <= 0 ", sqlMtoRestante, "0", True) & " AS MtoNoVencido, "
      Sql = Sql & gUtilSQL.getIIF(sqlDateDiff & " > 0 AND " & sqlDateDiff & " <= " & InsAdmPropAnalisisVenc.GetPrimerVencimiento, sqlMtoRestante, "0", True) & " AS Mto1Vto, "
      Sql = Sql & gUtilSQL.getIIF(sqlDateDiff & " > " & InsAdmPropAnalisisVenc.GetPrimerVencimiento & " AND " & sqlDateDiff & " <= " & InsAdmPropAnalisisVenc.GetSegundoVencimiento, sqlMtoRestante, "0", True) & " AS Mto2Vto, "
      Sql = Sql & gUtilSQL.getIIF(sqlDateDiff & " > " & InsAdmPropAnalisisVenc.GetSegundoVencimiento & " AND " & sqlDateDiff & " <= " & InsAdmPropAnalisisVenc.GetTercerVencimiento, sqlMtoRestante, "0", True) & " AS Mto3Vto, "
      Sql = Sql & gUtilSQL.getIIF(sqlDateDiff & " > " & InsAdmPropAnalisisVenc.GetTercerVencimiento, sqlMtoRestante, "0", True) & " AS Mto4Vto, "
      Sql = Sql & sqlMtoRestante & " AS MtoTotal"
      Sql = Sql & " FROM " & "proveedor INNER JOIN " & "CxP ON ("
      Sql = Sql & "proveedor.CodigoProveedor = " & "CxP.CodigoProveedor"
      Sql = Sql & ") AND (" & "proveedor.ConsecutivoCompania = " & "CxP.ConsecutivoCompania)"
      Sql = Sql & " WHERE " & "CxP.ConsecutivoCompania = " & valConsecutivoCompania
      Sql = Sql & " AND (" & "CxP.Status" & _
                  " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ABONADO) & _
                  " OR " & "CxP.Status" & _
                  " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_PORCANCELAR) & ")"
      Sql = Sql & " AND " & gUtilSQL.DfSQLDateValueBetween("CxP.Fecha", valFechaInicial, valFechaFinal)
      Sql = Sql & " ORDER BY "
      Sql = Sql & sqlMoneda & ", "
      Sql = Sql & sqlFecha & ", "
      If valAnalisisDeVencimientoPor = gEnumReport.enumReporteOrdenadoPorToString(enum_ReporteOrdenadoPor.eRO_Nombre) Then
         Sql = Sql & "proveedor.NombreProveedor, "
      Else
         Sql = Sql & "CxP.CodigoProveedor, "
      End If
      Sql = Sql & "CxP.FechaVencimiento"
   End If
   fSQLDelReporteAnalisisDeVencimientoEntreFechas = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLDelReporteAnalisisDeVencimientoEntreFechas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLComprobanteRetIVA(ByVal valFechaRetencion As Date, ByVal valConsecutivoCompania As Long, _
                              ByVal valNumero As String, ByVal ValCodigoProveedor As String, _
                                 ByVal gMonedaLocalActual As Object, ByVal valRetAplicadaPago As Boolean, _
                                    ByVal valAnoAplicacion As Integer, ByVal valFecha As Date, _
                                       ByVal gUltimaTasaDeCambio As Object, ByVal valEsUnaCxPCuentaATercero As Boolean) As String
   Dim Sql As String
   Dim sqlTotalCxP As String
   Dim sqlMontoGravado As String
   Dim sqlMontoExento As String
   Dim sqlMontoGravableAlicuotaGeneral As String
   Dim sqlMontoGravableAlicuota2 As String
   Dim sqlMontoGravableAlicuota3 As String
   Dim sqlMontoIva As String
   Dim sqlMontoIVAALicuotaGeneral As String
   Dim sqlMontoIVAALicuota2 As String
   Dim sqlMontoIVAALicuota3 As String
   Dim sqlCampoCambio As String
   Dim sqlCampoMoneda As String
   Dim gEnumProyecto As clsEnumAdministrativo
   Dim insPagoSQL As clsPagoSQL
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   Set insPagoSQL = New clsPagoSQL
   If valRetAplicadaPago Then
      sqlCampoCambio = gUtilSQL.getIIF("cxP.OrigenDeLaRetencion" & _
            " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_DondeSeEfectuaLaRetencionIVA.eDS_CXP), _
            "cxP.CambioABolivares", gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull(insPagoSQL.fSubSQLDecInfCambioOMonedaOriginalDesdePago(True, valConsecutivoCompania)), "1", insPagoSQL.fSubSQLDecInfCambioOMonedaOriginalDesdePago(True, valConsecutivoCompania), True), True)
      sqlCampoMoneda = gUtilSQL.getIIF("cxP.OrigenDeLaRetencion" & _
            " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_DondeSeEfectuaLaRetencionIVA.eDS_CXP), _
            "cxP.Moneda", insPagoSQL.fSubSQLDecInfCambioOMonedaOriginalDesdePago(False, valConsecutivoCompania), True)
   Else
      sqlCampoCambio = "cxP.CambioABolivares"
      sqlCampoMoneda = "cxP.Moneda"
   End If
   Dim vMontoGeneral As String
   Dim vmontoreducida As String
   Dim vmontoadicional As String
   Dim vMontoAlicuotaIVAGeneral As String
   Dim vMontoAlicuotaIVAreducida As String
   Dim vMontoAlicuotaIVAadicional As String

   If valAnoAplicacion >= gUtilDate.fYear(gMonedaLocalActual.GetHoyVigenteDesde) And valFecha < gMonedaLocalActual.GetHoyVigenteDesde Then
      sqlTotalCxP = "(cxP.MontoExento * " & gMonedaLocalActual.fFactorDeConversion(valFechaRetencion) & _
                  " + cxP.MontoGravado * " & gMonedaLocalActual.fFactorDeConversion(valFechaRetencion) & _
                  " + cxP.MontoIva * " & gMonedaLocalActual.fFactorDeConversion(valFechaRetencion) & ")"
      sqlMontoExento = " cxP.montoExento * " & gMonedaLocalActual.fFactorDeConversion(valFechaRetencion)
      sqlMontoGravado = " cxP.montoGravado * " & gMonedaLocalActual.fFactorDeConversion(valFechaRetencion)
      sqlMontoGravableAlicuotaGeneral = "cxP.MontoGravableAlicuotaGeneral * " & gMonedaLocalActual.fFactorDeConversion(valFechaRetencion)
      sqlMontoGravableAlicuota2 = " cxP.MontoGravableAlicuota2 * " & gMonedaLocalActual.fFactorDeConversion(valFechaRetencion)
      sqlMontoGravableAlicuota3 = " cxP.MontoGravableAlicuota3 * " & gMonedaLocalActual.fFactorDeConversion(valFechaRetencion)
      sqlMontoIva = " cxP.montoIva * " & gMonedaLocalActual.fFactorDeConversion(valFechaRetencion)
      sqlMontoIVAALicuotaGeneral = " cxP.MontoIVAAlicuotaGeneral * " & gMonedaLocalActual.fFactorDeConversion(valFechaRetencion)
      sqlMontoIVAALicuota2 = " cxP.MontoIVAAlicuota2 * " & gMonedaLocalActual.fFactorDeConversion(valFechaRetencion)
      sqlMontoIVAALicuota3 = " cxP.MontoIVAAlicuota3 * " & gMonedaLocalActual.fFactorDeConversion(valFechaRetencion)
   Else
      sqlTotalCxP = "(cxP.MontoExento " & " + cxP.MontoGravado " & " + cxP.MontoIva ) "
      sqlMontoExento = "cxP.MontoExento "
      sqlMontoGravado = "cxP.MontoGravado "
      sqlMontoGravableAlicuota2 = "cxP.MontoGravableAlicuota2"
      sqlMontoGravableAlicuota3 = "cxP.MontoGravableAlicuota3"
      vMontoGeneral = "cxP.MontoGravableAlicuotaGeneral "
      vmontoreducida = "cxP.MontoGravableAlicuotaEspecial2"
      vmontoadicional = "cxP.MontoGravableAlicuotaEspecial1"
      vMontoAlicuotaIVAGeneral = "cxP.MontoIVAAlicuotaGeneral "
      vMontoAlicuotaIVAreducida = "cxP.MontoIVAAlicuotaEspecial2"
      vMontoAlicuotaIVAadicional = "cxP.MontoIVAAlicuotaEspecial1"
      sqlMontoGravableAlicuotaGeneral = gUtilSQL.getIIF(vMontoGeneral & " <> " & "0", vMontoGeneral, gUtilSQL.getIIF(vmontoreducida & " <> " & "0", vmontoreducida, vmontoadicional))
      sqlMontoIva = "cxP.MontoIva"
      sqlMontoIVAALicuotaGeneral = gUtilSQL.getIIF(vMontoAlicuotaIVAGeneral & " <> " & "0", vMontoAlicuotaIVAGeneral, gUtilSQL.getIIF(vMontoAlicuotaIVAreducida & " <> " & "0", vMontoAlicuotaIVAreducida, vMontoAlicuotaIVAadicional))
      sqlMontoIVAALicuota2 = "cxP.MontoIVAAlicuota2"
      sqlMontoIVAALicuota3 = "cxP.MontoIVAAlicuota3"
   End If

   sqlTotalCxP = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCampoCambio, sqlCampoMoneda, sqlTotalCxP, True, "")
   sqlMontoExento = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCampoCambio, sqlCampoMoneda, sqlMontoExento, True, "")
   sqlMontoGravado = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCampoCambio, sqlCampoMoneda, sqlMontoGravado, True, "")
   sqlMontoGravableAlicuotaGeneral = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCampoCambio, sqlCampoMoneda, sqlMontoGravableAlicuotaGeneral, True, "")
   sqlMontoGravableAlicuota2 = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCampoCambio, sqlCampoMoneda, sqlMontoGravableAlicuota2, True, "")
   sqlMontoGravableAlicuota3 = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCampoCambio, sqlCampoMoneda, sqlMontoGravableAlicuota3, True, "")
   sqlMontoIva = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCampoCambio, sqlCampoMoneda, sqlMontoIva, True, "")
   sqlMontoIVAALicuotaGeneral = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCampoCambio, sqlCampoMoneda, sqlMontoIVAALicuotaGeneral, True, "")
   sqlMontoIVAALicuota2 = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCampoCambio, sqlCampoMoneda, sqlMontoIVAALicuota2, True, "")
   sqlMontoIVAALicuota3 = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCampoCambio, sqlCampoMoneda, sqlMontoIVAALicuota3, True, "")

   Sql = "SELECT "
   Sql = Sql & sqlTotalCxP & " As TotalCXPComprobanteRetIva, "
   Sql = Sql & "proveedor.Direccion, "
   Sql = Sql & "proveedor.Telefonos, "
   Sql = Sql & "proveedor.NumeroRIF, "
   Sql = Sql & "proveedor.NumeroNIT, "
   Sql = Sql & "proveedor.NombreProveedor, "
   Sql = Sql & "proveedor.CodigoProveedor, "
   Sql = Sql & "cxp.FechaAplicacionRetIVA, "
   Sql = Sql & "cxp.MesDeAplicacion, "
   Sql = Sql & "cxp.AnoDeAplicacion, "
   Sql = Sql & gUtilSQL.getIIF(gUtilSQL.DfCIntSQL("cxP.NumeroComprobanteRetencion") & " <> " & "0", "cxP.NumeroComprobanteRetencion", gUtilSQL.fSimpleSqlValue("Sin N° Asignado"), True) & " AS NumeroComprobanteRetencion, "
   Sql = Sql & "cxP.PorcentajeRetencionAplicado, "
   Sql = Sql & "cxP.Fecha AS FechaDelDocOrigen, "
   Sql = Sql & gUtilSQL.getIIF("cxP.TipoDeCxP = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeTransaccion.eTD_FACTURA), gUtilSQL.getIIF("cxP.UsaPrefijoSerie='S'", "'Serie ' + cxP.Numero", "cxP.Numero", True), gUtilSQL.fSimpleSqlValue(""), True) & " AS NumeroDeFactura, "
   Sql = Sql & "cxP.NumeroControl, "
   Sql = Sql & gUtilSQL.getIIF("cxP.TipoDeCxP = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeTransaccion.eTD_NOTADEDEBITO), _
                              "cxP.Numero", gUtilSQL.fSimpleSqlValue(""), True) & _
               " AS NumeroDeNotaDebito, "
   Sql = Sql & gUtilSQL.getIIF("cxP.TipoDeCxP = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeTransaccion.eTD_NOTADECREDITO), _
                              "cxP.Numero", gUtilSQL.fSimpleSqlValue(""), True) & _
               " AS NumeroDeNotaCredito, "
   Sql = Sql & gUtilSQL.DfSQLCaseIfForEnum("cxP.TipoDeTransaccion", _
                        enum_TipoDeTransaccionDeLibrosFiscales.eTD_A01_REGISTRO, _
                        gEnumProyecto.fEnumTransaccionFiscalToStringInArray(True), "") & _
               " AS TipoDeTransaccion, "
   Sql = Sql & "cxP.NumeroDeFacturaAfectada, "
   Sql = Sql & sqlMontoExento & " AS MontoExento, "
   Sql = Sql & sqlMontoGravado & " AS MontoGravado, "
   Sql = Sql & sqlMontoGravableAlicuotaGeneral & " AS MontoGravableAlicuotaGeneral, "
   Sql = Sql & sqlMontoGravableAlicuota2 & " AS MontoGravableAlicuota2, "
   Sql = Sql & sqlMontoGravableAlicuota3 & " AS MontoGravableAlicuota3, "
   Sql = Sql & gUtilSQL.getIIF("cxp.AplicaIvaAlicuotaEspecial " & " = " & "'N'", _
                        "(SELECT TOP 1 alicuotaIVA.MontoAlicuotaGeneral FROM alicuotaIVA WHERE " & _
                        " alicuotaIVA.FechaDeInicioDeVigencia <= cxP.Fecha ORDER BY alicuotaIVA.FechaDeInicioDeVigencia DESC)", _
                        gUtilSQL.getIIF("cxP.MontoGravableAlicuotaEspecial1 " & " > " & "0", _
                        "cxP.PorcentajeIvaAlicuotaEspecial1 ", "cxP.PorcentajeIvaAlicuotaEspecial2 "), True) & "As AlicuotaG,"
   Sql = Sql & "(SELECT TOP 1 alicuotaIVA.MontoAlicuota2" & _
                        " FROM alicuotaIVA WHERE alicuotaIVA.FechaDeInicioDeVigencia <= cxP.Fecha" & _
                        " ORDER BY alicuotaIVA.FechaDeInicioDeVigencia" & _
                        " DESC) AS Alicuota2, "
   Sql = Sql & "(SELECT TOP 1 alicuotaIVA.MontoAlicuota3" & _
                        " FROM alicuotaIVA WHERE alicuotaIVA.FechaDeInicioDeVigencia <= cxP.Fecha" & _
                        " ORDER BY alicuotaIVA.FechaDeInicioDeVigencia" & _
                        " DESC) AS Alicuota3, "
   Sql = Sql & sqlMontoIva & " AS MontoIva, "
   Sql = Sql & sqlMontoIVAALicuotaGeneral & " AS MontoIVAAlicuotaGeneral, "
   Sql = Sql & sqlMontoIVAALicuota2 & " AS MontoIVAAlicuota2, "
   Sql = Sql & sqlMontoIVAALicuota3 & " AS MontoIVAAlicuota3, "
   If valAnoAplicacion >= gUtilDate.fYear(gMonedaLocalActual.GetHoyVigenteDesde) And valFecha < gMonedaLocalActual.GetHoyVigenteDesde Then
      Sql = Sql & "cxP.MontoRetenido * " & gMonedaLocalActual.fFactorDeConversion(valFechaRetencion) & " as MontoRetenido, "
   Else
      Sql = Sql & "cxP.MontoRetenido, "
   End If
   Sql = Sql & fSQLAnoYMesDeAplicacionIVAParaComprobante
   Sql = Sql & " FROM proveedor"
   Sql = Sql & " INNER JOIN cxP ON ("
   If valEsUnaCxPCuentaATercero Then
      Sql = Sql & " proveedor.CodigoProveedor = cxP.CodigoProveedorOriginalServicio"
   Else
      Sql = Sql & " proveedor.CodigoProveedor = cxP.CodigoProveedor"
   End If
   Sql = Sql & ") AND (proveedor.ConsecutivoCompania = cxP.ConsecutivoCompania)"
   Sql = Sql & " WHERE cxP.ConsecutivoCompania = " & valConsecutivoCompania
   Sql = Sql & " AND cxP.Numero = " & valNumero
   If valEsUnaCxPCuentaATercero Then
      Sql = Sql & " AND cxP.CodigoProveedorOriginalServicio = " & ValCodigoProveedor
   Else
      Sql = Sql & " AND cxP.CodigoProveedor = " & ValCodigoProveedor
   End If
   Sql = Sql & " AND cxP.SeHizoLaRetencionIVA = " & gUtilSQL.fBooleanToSqlValue(True)
h_EXIT:
   Set gEnumProyecto = Nothing
   Set insPagoSQL = Nothing
   On Error GoTo 0
   fSQLComprobanteRetIVA = Sql
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLComprobanteRetIVA", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLAnoYMesDeAplicacionIVAParaComprobante() As String
   Dim Sql As String
   Dim sqlMes As String
   Dim sqlAno As String
   On Error GoTo h_ERROR
   sqlMes = "cxP.FechaAplicacionRetIva"
   sqlMes = gUtilSQL.fMonth(sqlMes, "")
   sqlMes = gUtilSQL.fCast(sqlMes, eTDSS_VARCHAR, "")
   sqlMes = gUtilSQL.DfRightSQL(sqlMes, 2, "0")
   sqlAno = "cxP.FechaAplicacionRetIva"
   sqlAno = gUtilSQL.fYear(sqlAno, "")
   Sql = sqlAno & " AS AnoAplicRetIVA, "
   Sql = Sql & sqlMes & " AS MesAplicRetIVA "
h_EXIT: On Error GoTo 0
   fSQLAnoYMesDeAplicacionIVAParaComprobante = Sql
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLAnoYMesDeAplicacionIVAParaComprobante", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLRetIVAenCxP(ByVal valMonedaOriginal As Boolean, _
                                                            ByVal usaCambioOriginal As Boolean, _
                                                             ByVal valElementInComboBoxToString As String, _
                                                              ByVal valFechaInicial As Date, _
                                                               ByVal valFechaFinal As Date, _
                                                                ByVal valInformeSeleccionado As Integer, _
                                                                 ByVal valConsecutivoCompania As String, _
                                                                  ByVal gMonedaLocalActual As Object, _
                                                                   ByVal gUltimaTasaDeCambio As Object) As String
   Dim Sql As String
   Dim sqlStatusCxP As String
   Dim sqlMtoTotal As String
   Dim sqlMtoRestante As String
   Dim sqlMonedaReporte As String
   Dim sqlCambio As String
   Dim unSoloStatus As Boolean
   Dim statusABuscarChr As String
   Dim sqlMtoAbonado As String
   Dim sqlMtoRetenido As String
   Dim gEnumProyecto As clsEnumAdministrativo
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   sqlStatusCxP = gUtilSQL.DfSQLCaseIfForEnum("CxP.Status", enum_StatusDocumento.eSD_PORCANCELAR, gEnumProyecto.fEnumStatusCxCToStringInArray(True, False), "")
   sqlMtoTotal = "(CxP.MontoExento + MontoGravado + MontoIva)"
   sqlMtoAbonado = "CxP.MontoAbonado"
   sqlMtoRestante = "(" & sqlMtoTotal & " - " & sqlMtoAbonado & ")"
   sqlMonedaReporte = "CxP.Moneda"
   sqlCambio = "CxP.CambioABolivares"
   sqlMtoRetenido = "CxP.MontoRetenido"
   If Not valMonedaOriginal Then
      sqlMonedaReporte = gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda)
      If usaCambioOriginal Then
         sqlCambio = gMonedaLocalActual.fSQLConvierteMontoSiAplicaMasDecimales(gMonedaLocalActual.GetHoyCodigoMoneda, sqlCambio, "CxP.Fecha")
      Else
         sqlCambio = gUtilSQL.getIIF("CxP.CodigoMoneda = " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMoneda) & " OR " & _
                                      "CxP.CodigoMoneda = " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMonedaAnt), _
                                      gMonedaLocalActual.fSQLConvierteMontoSiAplicaMasDecimales(gMonedaLocalActual.GetHoyCodigoMoneda, sqlCambio, "CxP.Fecha"), _
                                      gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("", "CxP.Moneda", 1, usaCambioOriginal, ""), True)
      End If
      sqlMtoTotal = gUtilSQL.getIIF("CxP.CodigoMoneda = " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMoneda) & " OR " & _
                                "CxP.CodigoMoneda = " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMonedaAnt), _
                                gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, sqlMtoTotal, "CxP.Fecha"), _
                                gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCambio, "CxP.Moneda", sqlMtoTotal, usaCambioOriginal, ""), True)
      sqlMtoRestante = gUtilSQL.getIIF("CxP.CodigoMoneda = " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMoneda) & " OR " & _
                                "CxP.CodigoMoneda = " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMonedaAnt), _
                                gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, sqlMtoRestante, "CxP.Fecha"), _
                                gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCambio, "CxP.Moneda", sqlMtoRestante, usaCambioOriginal, ""), True)
   End If
   unSoloStatus = False
   If valElementInComboBoxToString <> gDefgen.getSearchAllComboBox Then
      unSoloStatus = True
      statusABuscarChr = gUtilSQL.fSQLSimpleValueForEnum(gEnumProyecto.strStatusDocumentoToNum(valElementInComboBoxToString))
   End If
   Sql = "SELECT "
   Sql = Sql & "CxP.Numero, "
   Sql = Sql & "CxP.CodigoProveedor, "
   Sql = Sql & "Proveedor.NombreProveedor, "
   Sql = Sql & "CxP.Fecha, "
   Sql = Sql & "CxP.FechaVencimiento, "
   Sql = Sql & sqlStatusCxP & " AS StatusStr, "
   Sql = Sql & sqlMtoTotal & " AS MontoTotal, "
   Sql = Sql & sqlMtoRestante & " AS MontoRestante, "
   Sql = Sql & sqlMonedaReporte & " AS MonedaReporte, "
   Sql = Sql & "CxP.Moneda AS MonedaDocumento, "
   Sql = Sql & sqlCambio & " AS Cambio, "
   Sql = Sql & "CxP.porcentajeRetencionAplicado, "
   Sql = Sql & sqlMtoRetenido & " AS MontoRetenido "
   Sql = Sql & ", CxP.Status"
   Sql = Sql & " FROM CxP INNER JOIN Proveedor ON "
   Sql = Sql & "CxP.CodigoProveedor = Proveedor.CodigoProveedor"
   Sql = Sql & " AND CxP.ConsecutivoCompania = Proveedor.ConsecutivoCompania"
   Sql = Sql & " WHERE " & gUtilSQL.DfSQLDateValueBetween("CxP.Fecha", valFechaInicial, valFechaFinal)
   If unSoloStatus Then
      Sql = Sql & " AND CxP.Status = " & statusABuscarChr
   End If
   Sql = Sql & " AND CxP.ConsecutivoCompania = " & valConsecutivoCompania
   Sql = Sql & " AND cxP.EsUnaCuentaATerceros = " & gUtilSQL.fBooleanToSqlValue(False)
   Sql = Sql & " UNION"
   Sql = Sql & " SELECT "
   Sql = Sql & "CxP.Numero, "
   Sql = Sql & "CxP.CodigoProveedor, "
   Sql = Sql & "Proveedor.NombreProveedor, "
   Sql = Sql & "CxP.Fecha, "
   Sql = Sql & "CxP.FechaVencimiento, "
   Sql = Sql & sqlStatusCxP & " AS StatusStr, "
   Sql = Sql & sqlMtoTotal & " AS MontoTotal, "
   Sql = Sql & sqlMtoRestante & " AS MontoRestante, "
   Sql = Sql & sqlMonedaReporte & " AS MonedaReporte, "
   Sql = Sql & "CxP.Moneda AS MonedaDocumento, "
   Sql = Sql & sqlCambio & " AS Cambio, "
   Sql = Sql & "CxP.porcentajeRetencionAplicado, "
   Sql = Sql & sqlMtoRetenido & " AS MontoRetenido "
   Sql = Sql & ", CxP.Status"
   Sql = Sql & " FROM CxP INNER JOIN Proveedor ON "
   Sql = Sql & "CxP.CodigoProveedorOriginalServicio = Proveedor.CodigoProveedor"
   Sql = Sql & " AND CxP.ConsecutivoCompania = Proveedor.ConsecutivoCompania"
   Sql = Sql & " WHERE " & gUtilSQL.DfSQLDateValueBetween("CxP.Fecha", valFechaInicial, valFechaFinal)
   If unSoloStatus Then
      Sql = Sql & " AND CxP.Status = " & statusABuscarChr
   End If
   Sql = Sql & " AND CxP.ConsecutivoCompania = " & valConsecutivoCompania
   Sql = Sql & " AND cxP.EsUnaCuentaATerceros = " & gUtilSQL.fBooleanToSqlValue(True)
   
   
   Sql = Sql & " ORDER BY "
   Sql = Sql & "CxP.Status, "
   Sql = Sql & "MonedaReporte, "
   Sql = Sql & "CxP.Fecha, "
   Sql = Sql & "CxP.Numero, "
   Sql = Sql & "CxP.CodigoProveedor"
   Set gEnumProyecto = Nothing
   fSQLRetIVAenCxP = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLRetIVAenCxP", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function fConstruirSQLDeComprobanteCxP(ByVal valConsecutivoCompania As Long, ByVal valNumero As String, ByVal ValCodigoProveedor, ByVal valEsUnaCuentaDeTercero As Boolean) As String
   Dim gEnumProyecto As clsEnumAdministrativo
   Dim Sql As String
   Dim sqlTotal As String
   Dim sqlMontoRestante As String
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   Sql = " SELECT "
   Sql = Sql & gUtilSQL.DfSQLCaseIfForEnum("cxP" & ".TipoDeCxp", enum_TipoDeTransaccion.eTD_FACTURA, gEnumProyecto.fEnumTipoDeTransaccioToStringInArray(True), "TipoDeCxP") & ","
   Sql = Sql & gUtilSQL.getIIF("cxP" & ".UsaPrefijoSerie='S'", "'Serie ' +" & "cxP" & ".Numero", "cxP" & ".Numero", True) & " AS Numero, "
   Sql = Sql & "cxP" & ".NumeroControl AS NumeroControl, "
   Sql = Sql & "cxP" & ".Fecha AS Fecha, "
   Sql = Sql & "cxP" & ".FechaVencimiento AS FechaVencimiento, "
   Sql = Sql & "cxP" & ".NumDiasDeVencimiento AS DiasVencimiento, "
   Sql = Sql & "cxP" & ".FechaCancelacion AS FechaCancelacion, "
   Sql = Sql & gUtilSQL.DfSQLCaseIfForEnum("cxP" & ".Status", enum_StatusDocumento.eSD_PORCANCELAR, gEnumProyecto.fEnumStatusCxCToStringInArray(True), "Status") & ","
   Sql = Sql & "cxP" & ".EsCxPhistorica AS CxPHistorico, "
   Sql = Sql & "cxP" & ".Moneda AS Moneda, "
   Sql = Sql & "cxP" & ".CambioAbolivares AS CambioABolivares, "
   Sql = Sql & "proveedor" & ".CodigoProveedor As CodigoProveedor, "
   Sql = Sql & "proveedor" & ".NumeroRIF AS NumeroRif, "
   Sql = Sql & "proveedor" & ".NombreProveedor As NombreProveedor, "
   Sql = Sql & "cxP" & ".Observaciones AS Observaciones, "
   sqlTotal = "(" & "cxP" & ".MontoExento + " & "cxP" & ".MontoGravado + " & "cxP" & ".MontoIva)"
   Sql = Sql & sqlTotal & " AS Total, "
   Sql = Sql & "cxP" & ".MontoAbonado AS MontoAbonado,"
   sqlMontoRestante = sqlTotal & "-" & "cxP" & ".MontoAbonado"
   Sql = Sql & sqlMontoRestante & " AS RestaPorPagar,"
   Sql = Sql & "cxP" & ".MesDeAplicacion AS Mes, "
   Sql = Sql & "cxP" & ".AnoDeAplicacion AS Ano, "
   Sql = Sql & gUtilSQL.DfSQLCaseIfForEnum("cxP" & ".TipoDeCompra", enum_TipoDeCompra.eTD_COMPRASEXENTAS, gEnumProyecto.fenumTipoCompraToStringInArray(True), "TipodeCompra") & ","
   Sql = Sql & "cxP" & ".NumeroPlanillaDeImportacion AS PlanillaImportacion, "
   Sql = Sql & "cxP" & ".NumeroExpedienteDeImportacion AS ExpedienteImportacion, "
   Sql = Sql & "cxP" & ".NumeroDeclaracionAduana AS NumeroDeclaracionAduana, "
   Sql = Sql & "cxP" & ".FechaDeclaracionAduana AS FechaDeclaracionAduana, "
   Sql = Sql & "cxP" & ".UsaPrefijoSerie AS UsaPrefijoSerie, "
   Sql = Sql & gUtilSQL.DfSQLCaseIfForEnum("cxP" & ".TipoDeTransaccion", enum_TipoDeTransaccionDeLibrosFiscales.eTD_A01_REGISTRO, gEnumProyecto.fEnumTransaccionFiscalToStringInArray(True), "TipoTransaccion") & ","
   Sql = Sql & "cxP" & ".NumeroDeFacturaAfectada AS FacturaAfectada, "
   Sql = Sql & gUtilSQL.DfSQLCaseIfForEnum("cxP" & ".CreditoFiscal", enum_CreditoFiscal.eCF_DEDUCIBLE, gEnumProyecto.fEnumCreditoFiscalToStringInArray(True), "CreditoFiscal") & ","
   Sql = Sql & "cxP" & ".MontoExento AS MontoExento, "
'   SQL = SQL & "cxP" & ".MontoGravableAlicuotaGeneral AS AGMontoGravable, "
'   SQL = SQL & "cxP" & ".MontoIvaalicuotaGeneral AS AGMontoIVA, "
   Sql = Sql & gUtilSQL.getIIF("cxp.AplicaIvaAlicuotaEspecial = " & gUtilSQL.fSimpleSqlValue("S"), gUtilSQL.getIIF("cxP.MontoGravableAlicuotaEspecial1 <> 0", "cxP.MontoGravableAlicuotaEspecial1", "cxP.MontoGravableAlicuotaEspecial2", True), "cxP.MontoGravableAlicuotaGeneral", True) & "AS AGMontoGravable,"
   Sql = Sql & gUtilSQL.getIIF("cxp.AplicaIvaAlicuotaEspecial = " & gUtilSQL.fSimpleSqlValue("S"), gUtilSQL.getIIF("cxP.MontoIVAAlicuotaEspecial1 <> 0", "cxP.MontoIVAAlicuotaEspecial1", "cxP.MontoIVAAlicuotaEspecial2", True), "cxP.MontoIvaalicuotaGeneral", True) & "AS AGMontoIVA,"
   Sql = Sql & "cxP" & ".MontoGravableAlicuota2 AS A2MontoGravable, "
   Sql = Sql & "cxP" & ".MontoIvaalicuota2 AS A2MontoIVA, "
   Sql = Sql & "cxP" & ".NumeroComprobanteRetencion AS NumeroComprobanteRetencionIVA, "
   Sql = Sql & "cxP" & ".FechaAplicacionRetIva AS FechaAplicacion, "
   Sql = Sql & "cxP" & ".OrigenDeLaRetencion AS ModuloDeOrigen, "
   Sql = Sql & "cxP" & ".PorcentajeRetencionAplicado AS PorcentajeRetenido, "
   Sql = Sql & "cxP" & ".MontoRetenido AS MontoRetencionIVA, "
   Sql = Sql & "cxP" & ".CodigoMoneda AS CodigoMoneda"
   If valEsUnaCuentaDeTercero Then
      Sql = Sql & "cxP" & ", cxp.CodigoProveedorOriginalServicio as CodigoProvOriginal, proveedorOriginal.NombreProveedor as NombreProvOriginal"
   End If
   Sql = Sql & " FROM " & "proveedor as Proveedor INNER JOIN " & "cxP" & " ON  ( " & "proveedor" & ".CodigoProveedor = " & "cxP" & ".CodigoProveedor )" & _
   " AND (" & "proveedor" & ".ConsecutivoCompania = " & "cxP" & ".ConsecutivoCompania ) "
   If valEsUnaCuentaDeTercero Then
      Sql = Sql & " INNER JOIN proveedor as proveedorOriginal ON ( proveedorOriginal.CodigoProveedor = cxP.CodigoProveedorOriginalServicio) "
      Sql = Sql & " AND (proveedorOriginal.ConsecutivoCompania = cxP.ConsecutivoCompania)"
   End If
   Sql = Sql & "WHERE " & "cxP" & ".ConsecutivoCompania = " & gUtilSQL.fNumToStrSQL(valConsecutivoCompania) & _
   " AND " & "cxP" & ".Numero = " & gUtilSQL.fSimpleSqlValue(valNumero) & _
   " AND " & "cxP" & ".CodigoProveedor = " & gUtilSQL.fSimpleSqlValue(ValCodigoProveedor)
   Set gEnumProyecto = Nothing
   fConstruirSQLDeComprobanteCxP = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fConstruirSQLDeComprobanteCxP", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLSearchCxPPendientes(ByVal valConsecutivoCompania As Long, ByVal valEnviadoAPago As Boolean, ByVal valNumeroEnviadoAPago As String, ByVal refCodigoProveedor As String, ByVal valTraerTotales As Boolean, _
                                          ByVal valCodigoMonedaCxP As String, ByVal valFecha As Date, ByVal valfSQLMontoTotalCxP As String, _
                                            ByVal valOrdenarCxPPorFacturaDocumento As Boolean, Optional ByVal valPrefijoRIS As String, Optional ByVal valFiltraPorOrigenRetencion As Boolean = False) As String
   
   Dim sqlMontoOriginal As String
   Dim sqlMontoRestante As String
   Dim Sql As String
   On Error GoTo h_ERROR
   If valTraerTotales Then
      sqlMontoOriginal = valfSQLMontoTotalCxP
      sqlMontoRestante = "(" & sqlMontoOriginal & " - " & "cxP" & ".MontoAbonado)"
      Sql = "SELECT "
      Sql = Sql & "cxP" & ".Numero, "
      Sql = Sql & "cxP" & ".NumeroControl, " 'Pendiente
      Sql = Sql & "cxP" & ".ConsecutivoCxp, "
      Sql = Sql & "cxP" & ".Status, "
      Sql = Sql & "cxP" & ".MontoIva, "
      Sql = Sql & sqlMontoOriginal & " AS MontoOriginalCxP, "
      Sql = Sql & sqlMontoRestante & " AS MontoRestante, "
      Sql = Sql & "cxP" & ".Moneda, "
      If valEnviadoAPago Then
         Sql = Sql & "cxP" & ".CambioABolivares, "
      End If
      Sql = Sql & "cxP" & ".SeHizoLaRetencion AS Retenido, "
      Sql = Sql & "cxP" & ".RetencionAplicadaEnPago AS RetenerEnPago, "
      Sql = Sql & gUtilSQL.getIIF("cxP" & ".RetencionAplicadaEnPago" & " = " & gUtilSQL.fBooleanToSqlValue(False), "cxP" & ".MontoRetenido", "0", True) & " AS MontoRetIVA, "
      Sql = Sql & "cxP" & ".SeHizoLaRetencionIva AS IvaRetenido, "
      Sql = Sql & "cxP" & ".CodigoMoneda, "
      Sql = Sql & "cxP.EsUnaCuentaATerceros, "
      Sql = Sql & "cxP.CodigoProveedorOriginalServicio "
      Sql = Sql & " FROM " & "cxP"
      Sql = Sql & " WHERE " & "cxP" & ".ConsecutivoCompania" & _
                  " = " & gUtilSQL.fNumToStrSQL(valConsecutivoCompania)
      Sql = Sql & " AND " & "cxP" & ".CodigoProveedor" & _
                  " = " & gUtilSQL.fSimpleSqlValue(refCodigoProveedor)
      Sql = Sql & " AND " & "cxP" & ".Status" & _
                  " <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ANULADO)
     
     If (valFiltraPorOrigenRetencion) Then
      
      Sql = Sql & " AND " & "cxP" & ".OrigenDeLaRetencionISLR" & _
                  " <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_DondeSeEfectuaLaRetencionIVA.eDS_NO_RETENIDA)
     
     End If
    
      Sql = Sql & " AND " & "cxP" & ".MontoAbonado" & _
                  " <> (" & "cxP" & ".MontoExento" & _
                     " + " & "cxP" & ".MontoGravado" & _
                     " + " & "cxP" & ".MontoIva)"
      Sql = Sql & " AND Fecha <= " & gUtilSQL.fDateToSQLValue(valFecha)
      Sql = Sql & " AND " & gUtilSQL.DfMidSQL("cxP" & ".Numero", 1, 3) & " <> " & gUtilSQL.fSimpleSqlValue(valPrefijoRIS)
    
      If valEnviadoAPago Then
         Sql = Sql & " AND " & "cxP" & ".Numero" & " = " & gUtilSQL.fSimpleSqlValue(valNumeroEnviadoAPago)
      End If
         Sql = Sql & " AND " & "cxP" & ".EstaAsociadoARendicion  = 'N' "
    
   Else
      Sql = "SELECT "
      Sql = Sql & "cxP" & ".Numero"
      Sql = Sql & " FROM " & "cxP"
      Sql = Sql & " WHERE " & "cxP" & ".ConsecutivoCompania" & " = " & gUtilSQL.fNumToStrSQL(valConsecutivoCompania)
      Sql = Sql & " AND " & "cxP" & ".Status <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ANULADO)
      Sql = Sql & " AND " & "cxP" & ".CodigoProveedor" & " = " & gUtilSQL.fSimpleSqlValue(refCodigoProveedor)
      Sql = Sql & " AND " & gUtilSQL.DfMidSQL("cxP" & ".Numero", 1, 3) & " <> " & gUtilSQL.fSimpleSqlValue(valPrefijoRIS)
   End If
   If valOrdenarCxPPorFacturaDocumento Then
      Sql = Sql & " ORDER BY " & gUtilSQL.fSQLFillRightWithCaracter("cxP.Numero", 25, "0")
   End If
   fSQLSearchCxPPendientes = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: fSQLSearchCxPPendientes = ""
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLSearchCxPPendientes", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLSearchByCodigoProveedoryNumeroCxPParaActualizarDesdePago(ByVal ValCodigoProveedor As String, ByVal valNumeroCxP As String, ByVal valConsecutivoCompania As Long) As String
   Dim Sql As String
   Dim sqlWhere As String
   On Error GoTo h_ERROR
   sqlWhere = ""
   sqlWhere = gUtilSQL.fSQLNumberValueWithAnd(sqlWhere, "ConsecutivoCompania", valConsecutivoCompania)
   sqlWhere = gUtilSQL.fSQLValueWithAnd(sqlWhere, "CodigoProveedor", ValCodigoProveedor, False)
   sqlWhere = gUtilSQL.fSQLValueWithAnd(sqlWhere, "Numero", valNumeroCxP, False)
   Sql = "SELECT "
   Sql = Sql & "ConsecutivoCompania, "
   Sql = Sql & "Numero, "
   Sql = Sql & "Status, "
   Sql = Sql & "CodigoProveedor, "
   Sql = Sql & "MontoExento, "
   Sql = Sql & "MontoGravado, "
   Sql = Sql & "MontoIva, "
   Sql = Sql & "MontoAbonado, "
   Sql = Sql & "TotalOtrosImpuestos, "
   Sql = Sql & "SeHizoLaRetencion, "
   Sql = Sql & "SeHizoLaRetencionIva, "
   Sql = Sql & "MontoRetenido, "
   Sql = Sql & "NumeroComprobanteRetencion, "
   Sql = Sql & "RetencionAplicadaEnPago, "
   Sql = Sql & "FechaCancelacion, "
   Sql = Sql & "OrigenDeLaRetencion, "
   Sql = Sql & "OrigenInformacionRetencion, "
   Sql = Sql & "FechaAplicacionRetIva, "
   Sql = Sql & "EsCxPhistorica, "
   Sql = Sql & "DondeContabilRetIva, "
   Sql = Sql & "SeContabilRetIva, "
   Sql = Sql & "ConsecutivoCxP, "
   Sql = Sql & "NombreOperador, "
   Sql = Sql & "OrigenDeLaRetencionISLR,"
   Sql = Sql & "SeContabilISLR,"
   Sql = Sql & "ISLRAplicadaEnPago,"
   Sql = Sql & "FechaUltimaModificacion"
   Sql = Sql & " FROM " & "cxP"
   Sql = Sql & gUtilSQL.getWhereSQL(sqlWhere)
   Sql = Sql & " ORDER BY Numero"
   fSQLSearchByCodigoProveedoryNumeroCxPParaActualizarDesdePago = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: fSQLSearchByCodigoProveedoryNumeroCxPParaActualizarDesdePago = ""
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLSearchByCodigoProveedoryNumeroCxPParaActualizarDesdePago", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function fSQLActualizarPrimerPaso(ByVal valConsecutivoCompania As Long, ByVal valConsecutivoCxp As Long) As String
   Dim Sql As String
   On Error GoTo h_ERROR
   Sql = "SELECT "
   Sql = Sql & "ConsecutivoCompania, "
   Sql = Sql & "ConsecutivoCxP, "
   Sql = Sql & "Numero, "
   Sql = Sql & "CodigoProveedor, "
   Sql = Sql & "TipoDeCxp, "
   Sql = Sql & "Status, "
   Sql = Sql & "Fecha, "
   Sql = Sql & "FechaCancelacion, "
   Sql = Sql & "FechaVencimiento, "
   Sql = Sql & "FechaAnulacion, "
   Sql = Sql & "Moneda, "
   Sql = Sql & "CambioAbolivares, "
   Sql = Sql & "AplicaParaLibrodeCompras, "
   Sql = Sql & "MontoExento, "
   Sql = Sql & "MontoGravado, "
   Sql = Sql & "MontoIva, "
   Sql = Sql & "MontoAbonado, "
   Sql = Sql & "MesDeAplicacion, "
   Sql = Sql & "AnoDeAplicacion, "
   Sql = Sql & "Observaciones, "
   Sql = Sql & "CreditoFiscal, "
   Sql = Sql & "TipoDeCompra, "
   Sql = Sql & "SeHizoLaRetencion, "
   Sql = Sql & "NombreOperador, "
   Sql = Sql & "NumDiasDeVencimiento, "
   Sql = Sql & "FechaUltimaModificacion, "
   Sql = Sql & "CodigoMoneda,"
   Sql = Sql & "BaseImponibleIGTFML, "
   Sql = Sql & "AlicuotaIGTFML, "
   Sql = Sql & "MontoIGTFML "
   Sql = Sql & " FROM " & "cxP"
   Sql = Sql & " WHERE ConsecutivoCompania = " & gUtilSQL.fNumToStrSQL(valConsecutivoCompania)
   Sql = Sql & " AND ConsecutivoCxP = " & gUtilSQL.fNumToStrSQL(valConsecutivoCxp)
   fSQLActualizarPrimerPaso = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLActualizarPrimerPaso", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function fSQLActualizarSegundoPaso(ByVal valConsecutivoCompania As Long, ByVal valConsecutivoCxp As Long) As String
   Dim Sql As String
   On Error GoTo h_ERROR
   Sql = "SELECT "
   Sql = Sql & "ConsecutivoCompania, "
   Sql = Sql & "ConsecutivoCxP, "
   Sql = Sql & "Numero, "
   Sql = Sql & "CodigoProveedor, "
   Sql = Sql & "MontoGravableAlicuotaGeneral, "
   Sql = Sql & "MontoGravableAlicuota2, "
   Sql = Sql & "MontoGravableAlicuota3, "
   Sql = Sql & "MontoIvaalicuotaGeneral, "
   Sql = Sql & "MontoIvaalicuota2, "
   Sql = Sql & "MontoIvaalicuota3, "
   Sql = Sql & "NumeroComprobanteRetencion, "
   Sql = Sql & "NumeroPlanillaDeImportacion, "
   Sql = Sql & "NumeroExpedienteDeImportacion, "
   Sql = Sql & "TipoDeTransaccion, "
   Sql = Sql & "NumeroDeFacturaAfectada, "
   Sql = Sql & "NumeroControl, "
   Sql = Sql & "SeHizoLaRetencionIva, "
   Sql = Sql & "FechaAplicacionRetIva, "
   Sql = Sql & "PorcentajeRetencionAplicado, "
   Sql = Sql & "MontoRetenido, "
   Sql = Sql & "OrigenDeLaRetencion, "
   Sql = Sql & "DondeContabilRetIva, "
   Sql = Sql & "OrigenInformacionRetencion,"
   Sql = Sql & "TotalOtrosImpuestos,"
   Sql = Sql & "RetencionAplicadaEnPago, "
   Sql = Sql & "OrigenDeLaRetencionISLR,"
   Sql = Sql & "SeContabilISLR,"
   Sql = Sql & "ISLRAplicadaEnPago,"
   Sql = Sql & "MontoRetenidoISLR,"
   Sql = Sql & "DondeContabilISLR,"
   Sql = Sql & "EstaAsociadoARendicion, "
   Sql = Sql & "ConsecutivoRendicion, "
   Sql = Sql & "FechaAplicacionImpuestoMunicipal, "
   Sql = Sql & "NumeroComprobanteImpuestoMunicipal, "
   Sql = Sql & "MontoRetenidoImpuestoMunicipal, "
   Sql = Sql & "ImpuestoMunicipalRetenido, "
   Sql = Sql & "NumeroControlDeFacturaAfectada, "
   Sql = Sql & "NumeroDeclaracionAduana, "
   Sql = Sql & "FechaDeclaracionAduana, "
   Sql = Sql & "UsaPrefijoSerie, "
   Sql = Sql & "EsUnaCuentaATerceros, "
   Sql = Sql & "CodigoProveedorOriginalServicio, "
   Sql = Sql & "SeHizoLaDetraccion , AplicaIvaAlicuotaEspecial, MontoGravableAlicuotaEspecial1, MontoIVAAlicuotaEspecial1, PorcentajeIvaAlicuotaEspecial1, MontoGravableAlicuotaEspecial2, MontoIVAAlicuotaEspecial2, PorcentajeIvaAlicuotaEspecial2, "
   Sql = Sql & "DiaDeAplicacion, "
   Sql = Sql & "MotivoDeAnulacionDeComprobante "
   Sql = Sql & " FROM " & "cxP"
   Sql = Sql & " WHERE ConsecutivoCompania = " & gUtilSQL.fNumToStrSQL(valConsecutivoCompania)
   Sql = Sql & " AND ConsecutivoCxP = " & gUtilSQL.fNumToStrSQL(valConsecutivoCxp)
   fSQLActualizarSegundoPaso = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLActualizarSegundoPaso", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

'********************Generar pago de ISLR en CxP
Public Function fSQLInsertarCxP(ByVal valnombreParaTabla As String, ByVal valConsecutivoCompania As Long) As String
   Dim Sql As String
   On Error GoTo h_ERROR
   Sql = "INSERT INTO tblInformeCxCCxP " & "("
   Sql = Sql & "ConsecutivoCompania, "
   Sql = Sql & "Idusuario, "
   Sql = Sql & "Idmodulo, "
   Sql = Sql & "Numero, "
   Sql = Sql & "Status, "
   Sql = Sql & "CodigoCliProv, "
   Sql = Sql & "Origen, "
   Sql = Sql & "Tipo, "
   Sql = Sql & "MontoExento, "
   Sql = Sql & "MontoGravado, "
   Sql = Sql & "MontoIva, "
   Sql = Sql & "MontoAbonado, "
   Sql = Sql & "Fecha, "
   Sql = Sql & "FechaCancelacion, "
   Sql = Sql & "FechaVencimiento, "
   Sql = Sql & "FechaAnulacion, "
   Sql = Sql & "Moneda, "
   Sql = Sql & "CambioAbolivares, "
   Sql = Sql & "CodigoMoneda"
   Sql = Sql & ") SELECT "
   Sql = Sql & valConsecutivoCompania & ", "
   Sql = Sql & gUtilSQL.fSimpleSqlValue(valnombreParaTabla) & ", "
   Sql = Sql & gUtilSQL.fSimpleSqlValue(CM_MODULO_CxP) & ", "
   Sql = Sql & "Numero, "
   Sql = Sql & "Status, "
   Sql = Sql & "CodigoProveedor, "
   Sql = Sql & gUtilSQL.fSQLSimpleValueForEnum(enum_OrigenFacturacionOManual.eOF_MANUAL) & ", "
   Sql = Sql & "TipoDeCxP, "
   Sql = Sql & "MontoExento, "
   Sql = Sql & "MontoGravado, "
   Sql = Sql & "MontoIva, "
   Sql = Sql & "MontoAbonado, "
   Sql = Sql & "Fecha, "
   Sql = Sql & "FechaCancelacion, "
   Sql = Sql & "FechaVencimiento, "
   Sql = Sql & "FechaAnulacion, "
   Sql = Sql & "Moneda, "
   Sql = Sql & "CambioABolivares, "
   Sql = Sql & "CodigoMoneda"
   Sql = Sql & " FROM cxP "
   Sql = Sql & " WHERE ConsecutivoCompania = " & valConsecutivoCompania
h_EXIT: On Error GoTo 0
   fSQLInsertarCxP = Sql
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLInsertarCxP", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLAsignarPorcjARetDocPagado(ByVal valTarifa As Currency, ByVal valCodigo As String, ByVal valTipoDePersona As String) As String
   Dim vUpdateSQL As String
   On Error GoTo h_ERROR
   vUpdateSQL = "UPDATE retDocumentoPagado SET PorcentajeDeRetencion = " & gUtilSQL.fNumToStrSQL(valTarifa)
   vUpdateSQL = vUpdateSQL & " FROM retDocumentoPagado"
   vUpdateSQL = vUpdateSQL & " INNER JOIN retPago"
   vUpdateSQL = vUpdateSQL & " ON retDocumentoPagado.ConsecutivoCompania = retPago.ConsecutivoCompania"
   vUpdateSQL = vUpdateSQL & " AND retDocumentoPagado.NumeroComprobante = retPago.NumeroComprobante"
   vUpdateSQL = vUpdateSQL & " INNER JOIN proveedor"
   vUpdateSQL = vUpdateSQL & " ON retPago.ConsecutivoCompania = proveedor.ConsecutivoCompania"
   vUpdateSQL = vUpdateSQL & " AND retPago.CodigoProveedor = proveedor.CodigoProveedor"
   vUpdateSQL = vUpdateSQL & " WHERE retDocumentoPagado.CodigoRetencion = " & gUtilSQL.fSimpleSqlValue(valCodigo)
   vUpdateSQL = vUpdateSQL & " AND proveedor.TipoDePersona = " & valTipoDePersona
   vUpdateSQL = vUpdateSQL & " AND " & gUtilSQL.DfSQLIsNull("PorcentajeDeRetencion")
   fSQLAsignarPorcjARetDocPagado = vUpdateSQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLAsignarPorcjARetDocPagado", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLRetPago(ByVal valNumero As String, ByVal ValCodigoProveedor As String, ByVal valConsecutivoCompania As Long) As String
   Dim vSql As String
   On Error GoTo h_ERROR
   vSql = " SELECT retPago.* "
   vSql = vSql & " FROM RetPago "
   vSql = vSql & " INNER JOIN retDocumentoPagado "
   vSql = vSql & " ON retPago.ConsecutivoCompania = retDocumentoPagado.ConsecutivoCompania "
   vSql = vSql & " AND retPago.NumeroComprobante = retDocumentoPagado.NumeroComprobante "
   vSql = vSql & " INNER JOIN cxP "
   vSql = vSql & " ON retDocumentoPagado.ConsecutivoCompania = cxP.ConsecutivoCompania "
   vSql = vSql & " AND retDocumentoPagado.NumeroDelDocumento = Numero "
   vSql = vSql & " AND retPago.CodigoProveedor = cxP.CodigoProveedor "
   vSql = vSql & " WHERE retPago.consecutivoCompania = " & valConsecutivoCompania
   vSql = vSql & " AND cxP.Numero = " & gUtilSQL.fSimpleSqlValue(valNumero)
   vSql = vSql & " AND cxP.CodigoProveedor = " & gUtilSQL.fSimpleSqlValue(ValCodigoProveedor)
h_EXIT: On Error GoTo 0
   fSQLRetPago = vSql
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLRetPago", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
 End Function

Public Function fSQLSearchDetail(ByVal valNumeroReferencia As String, ByVal valProveedor As String, ByVal valConsecutivoCompania As Long) As String
   Dim Sql As String
   Dim sqlSortOrder As String
   On Error GoTo h_ERROR
   sqlSortOrder = " ORDER BY " & "RetDocumentoPagado.ConsecutivoCompania" _
      & ", " & "RetDocumentoPagado.NumeroComprobante" _
      & ", " & "RetDocumentoPagado.SecuencialDocPagado"
   Sql = ""
   Sql = gUtilSQL.fSQLValueWithAnd(Sql, " RetPago.CodigoProveedor", valProveedor, False)
   Sql = gUtilSQL.fSQLNumberValueWithAnd(Sql, " RetPago.ConsecutivoCompania", valConsecutivoCompania)
   Sql = gUtilSQL.fSQLValueWithAnd(Sql, " RetPago.NumeroReferencia", valNumeroReferencia, False)
   fSQLSearchDetail = "SELECT NumeroDelDocumento, NumeroControl, " _
         & "CodigoRetencion,  PorcentajeDeRetencion, MontoOriginal, " _
         & "MontoBaseImponible, MontoRetencion, " _
         & "TipoOperacion,retDocumentoPagado.ConsecutivoCompania, " _
         & "retDocumentoPagado.NumeroComprobante, SecuencialDocPagado " _
         & " FROM RetDocumentoPagado INNER JOIN RetPago " _
         & " ON " & " RetPago.ConsecutivoCompania = retDocumentoPagado.ConsecutivoCompania" _
         & " AND  " & " RetPago.NumeroComprobante = retDocumentoPagado.NumeroComprobante" _
         & gUtilSQL.getWhereSQL(Sql) & sqlSortOrder
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: fSQLSearchDetail = ""
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLSearchDetail", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConstruirSQLDeCxPConComprobante(ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valComprobanteNavTableName As String, ByVal valConsecutivoCompania As Long, ByVal valDocOrigenContabilizacionParaSQL As String) As String
   Dim Sql As String
   On Error GoTo h_ERROR
   Sql = " SELECT "
   Sql = Sql & " cxP.Fecha AS FechaDocumento, "
   Sql = Sql & " cxP.Numero AS NumeroDocumento, "
   Sql = Sql & " proveedor.CodigoProveedor"
   Sql = Sql & " AS CodigoProveedor , "
   Sql = Sql & " cxP.TipoDeCxp AS Tipo, "
   Sql = Sql & "proveedor.NombreProveedor"
   Sql = Sql & " AS NombreProveedor , "
   Sql = Sql & valComprobanteNavTableName & "." & "GeneradoPor"
   Sql = Sql & " AS TipoDeDocumento, "
   Sql = Sql & valComprobanteNavTableName & "." & "Fecha"
   Sql = Sql & " AS FechaCombrobante, "
   Sql = Sql & valComprobanteNavTableName & "." & "NUMERO"
   Sql = Sql & " AS NumeroComprobante, "
   Sql = Sql & valComprobanteNavTableName & "." & "DESCRIPCION"
   Sql = Sql & " AS DescripcionComprobante, "
   Sql = Sql & valComprobanteNavTableName & "." & "TotalHaber"
   Sql = Sql & " AS TotalHaber, "
   Sql = Sql & valComprobanteNavTableName & "." & "NoDocumentoOrigen"
   Sql = Sql & " AS NumeroDocumentoOrigen, "
   Sql = Sql & "(" & "cxP.MontoExento + "
   Sql = Sql & "cxP.MontoGravado ) "
   Sql = Sql & " AS TotalDocumento "
   Sql = Sql & " FROM " & "cxP, " & valComprobanteNavTableName & ", "
   Sql = Sql & " proveedor "
   Sql = Sql & " WHERE " & "cxP.CodigoProveedor = "
   Sql = Sql & "proveedor.CodigoProveedor"
   Sql = Sql & " AND " & "cxP.ConsecutivoCompania = "
   Sql = Sql & "proveedor.ConsecutivoCompania"
   Sql = Sql & " AND " & valComprobanteNavTableName & "." & "NoDocumentoOrigen" & " = "
   Sql = Sql & "(" & valDocOrigenContabilizacionParaSQL & ")"
'   SQL = SQL & "(" & getFN_NO_DOC_ORIGEN_CONTABILIZACION_PARA_SQL(True) & ")"
   Sql = Sql & " AND " & "cxP.ConsecutivoCompania = "
   Sql = Sql & valConsecutivoCompania
   Sql = Sql & " AND " & valComprobanteNavTableName & "." & "GeneradoPor" & " ='"
   Sql = Sql & gConvert.enumerativoAChar(enum_ComprobanteGeneradoPor.eCG_CXP) & "'"
   Sql = Sql & " AND " & "cxP.Fecha"
   Sql = Sql & " BETWEEN "
   Sql = Sql & gUtilSQL.fDateToSQLValue(valFechaInicial) & " AND "
   Sql = Sql & gUtilSQL.fDateToSQLValue(valFechaFinal)
   Sql = Sql & " AND " & valComprobanteNavTableName & "." & "Fecha"
   Sql = Sql & " BETWEEN "
   Sql = Sql & gUtilSQL.fDateToSQLValue(valFechaInicial) & " AND "
   Sql = Sql & gUtilSQL.fDateToSQLValue(valFechaFinal)
   fConstruirSQLDeCxPConComprobante = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConstruirSQLDeCxPConComprobante", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLProcesoPostEliminacionCxP(ByVal valNumeroDocOrigen As String, ByVal valConsecutivoCompania As Long) As String
   Dim vCodigoProveedor As String
   Dim vNumeroCxP As String
   Dim Sql As String
   On Error GoTo h_ERROR
   vCodigoProveedor = gTexto.DfMid(valNumeroDocOrigen, 1, gTexto.DfInStr(valNumeroDocOrigen, gTexto.fSeparadorStandardDeElementosString) - 1)
   vNumeroCxP = gTexto.DfMid(valNumeroDocOrigen, gTexto.DfInStr(valNumeroDocOrigen, gTexto.fSeparadorStandardDeElementosString) + 1)
   Sql = "UPDATE cxP "
   Sql = Sql & " SET SeContabilRetIva = " & gUtilSQL.fBooleanToSqlValue(False)
   Sql = Sql & " SET SeContabilISLR = " & gUtilSQL.fBooleanToSqlValue(False)
   Sql = Sql & " WHERE ConsecutivoCompania = " & valConsecutivoCompania
   Sql = Sql & " AND CodigoProveedor = " & gUtilSQL.fSimpleSqlValue(vCodigoProveedor)
   Sql = Sql & " AND Numero = " & gUtilSQL.fSimpleSqlValue(vNumeroCxP)
   Sql = Sql & " AND DondeContabilRetIVA = " & gUtilSQL.fSQLSimpleValueForEnum(enum_DondeEfectuoContabilizacionRetIVA.eDC_CxP)
   Sql = Sql & " AND DondeContabilISLR = " & gUtilSQL.fSQLSimpleValueForEnum(enum_DondeEfectuoContabilizacionRetIVA.eDC_CxP)
   Sql = Sql & " AND MontoRetenido <> 0"
   Sql = Sql & " AND MontoRetenidoISLR <> 0"
   fSQLProcesoPostEliminacionCxP = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
       "fSQLProcesoPostEliminacionCxP", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLPagosEnXML(ByVal valFechaDesde As Date, ByVal valFechaHasta As Date, ByVal valConsecutivoCompania As Long) As String
   Dim vSql As String
   On Error GoTo h_ERROR
   vSql = " SELECT "
   vSql = vSql & " retPago.ConsecutivoCompania, "
   vSql = vSql & " retPago.NumeroComprobante "
   vSql = vSql & "FROM retPago "
   vSql = vSql & " INNER JOIN Proveedor ON "
   vSql = vSql & "retPago.ConsecutivoCompania = "
   vSql = vSql & "Proveedor.ConsecutivoCompania "
   vSql = vSql & " AND retPago.CodigoProveedor = "
   vSql = vSql & "Proveedor.CodigoProveedor "
   vSql = vSql & " WHERE "
   vSql = vSql & " retPago.ConsecutivoCompania = " & valConsecutivoCompania
   vSql = vSql & " AND " & gUtilSQL.fSQLValueBetween("retPago.Fecha", gUtilSQL.fSimpleSqlValue(valFechaDesde), gUtilSQL.fSimpleSqlValue(valFechaHasta))
   fSQLPagosEnXML = vSql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLPagosEnXML", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLCxPPorCancelar(ByVal valConsecutivoCompania As Long, ByVal ValCodigoProveedor As String, ByVal valFiltroPorProveedor As Boolean) As String
   Dim vSql As String
   Dim vSqlWhere As String
   On Error GoTo h_ERROR
    vSqlWhere = gUtilSQL.fSQLEnumValueWithAnd("", "cxP.OrigenDeLaRetencionISLR", enum_DondeSeEfectuaLaRetencionIVA.eDS_NO_RETENIDA)
    vSqlWhere = gUtilSQL.fSQLEnumValueWithAnd(vSqlWhere, "cxP.Status", enum_StatusDocumento.eSD_PORCANCELAR)
    vSqlWhere = gUtilSQL.fSQLEnumValueWithAnd(vSqlWhere, "cxP.TipoDeCxp", enum_TipoDeTransaccion.eTD_FACTURA)

   
    If (valFiltroPorProveedor) Then
     vSqlWhere = gUtilSQL.fSQLValueWithAnd(vSqlWhere, "cxP.CodigoProveedor", ValCodigoProveedor, False)
    End If
    vSqlWhere = gUtilSQL.fSQLNumberValueWithAnd(vSqlWhere, "cxP.ConsecutivoCompania", valConsecutivoCompania)
    vSqlWhere = gUtilSQL.getWhereSQL(vSqlWhere)
    
    vSql = "SELECT "
    vSql = vSql & "cxP" & ".Fecha, "
    vSql = vSql & "cxP" & ".Numero, "
    vSql = vSql & "cxP" & ".NumeroControl, "
    vSql = vSql & "cxP" & ".NombreOperador "
  
    vSql = vSql & " FROM  cxP "
    vSql = vSql & vSqlWhere
    
   fSQLCxPPorCancelar = vSql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: fSQLCxPPorCancelar = ""
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLCxPPorCancelar", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLComprobanteRetencionEnCxP(ByVal valConsecutivoCompania As Long, ByVal valNumeroComprobanteRetencion As Long, ByVal valTipoDePersona As String, ByVal valFechaInicioVigencia As Date) As String
   Dim vSql As String
   Dim vSqlWhere As String
   On Error GoTo h_ERROR
   vSqlWhere = gUtilSQL.fSQLValueWithAnd("", "TipoDePersona", valTipoDePersona, False)
   vSqlWhere = gUtilSQL.fSQLNumberValueWithAnd(vSqlWhere, "Pago.NumeroComprobante", valNumeroComprobanteRetencion)
   vSqlWhere = gUtilSQL.fSQLNumberValueWithAnd(vSqlWhere, "retDocumentoPagado.NumeroComprobante", valNumeroComprobanteRetencion)
   vSqlWhere = gUtilSQL.fSQLNumberValueWithAnd(vSqlWhere, "retDocumentoPagado.ConsecutivoCompania", valConsecutivoCompania)
   vSqlWhere = gUtilSQL.fSQLValueWithAnd(vSqlWhere, "TablaRetencion.FechaDeInicioDeVigencia", valFechaInicioVigencia, False)
   vSqlWhere = gUtilSQL.getWhereSQL(vSqlWhere)
   vSql = " SELECT "
   vSql = vSql & "NumeroDelDocumento,"
   vSql = vSql & "retDocumentoPagado.NumeroControl,"
   vSql = vSql & "MontoRetencion,"
   vSql = vSql & "MontoOriginal,"
   vSql = vSql & "MontoBaseImponible,"
   vSql = vSql & "Sustraendo,"
   vSql = vSql & "TipoDePago,"
   vSql = vSql & "Tarifa,"
   vSql = vSql & "CxP.Fecha AS FechaFact "
   vSql = vSql & "From retDocumentoPagado "
   vSql = vSql & "INNER JOIN TablaRetencion  ON "
   vSql = vSql & "(retDocumentoPagado.CodigoRetencion = TablaRetencion.Codigo) "
   vSql = vSql & "INNER JOIN CxP INNER JOIN Pago ON "
   vSql = vSql & "(CxP.CodigoProveedor = Pago.CodigoProveedor AND "
   vSql = vSql & " Pago.ConsecutivoCompania = cxp.ConsecutivoCompania) ON "
   vSql = vSql & "(CxP.Numero = retDocumentoPagado.NumeroDelDocumento  AND "
   vSql = vSql & " CxP.ConsecutivoCompania = retDocumentoPagado.ConsecutivoCompania) "
   vSql = vSql & vSqlWhere
   fSQLComprobanteRetencionEnCxP = vSql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: fSQLComprobanteRetencionEnCxP = ""
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLComprobanteRetencionEnCxP", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLComprobanteRetencionImpuestoMunicipalEnCxP(ByVal valConsecutivoCompania As Long, ByVal valConsecutivoCxp As Long, ByVal valTieneContabilidadActiva As Boolean, ByVal valConsecutivoPeriodo) As String
   Dim vSql As String
   Dim vSqlWhere As String
   Dim vCxPVista As clsCxPVista
   On Error GoTo h_ERROR
   Set vCxPVista = New clsCxPVista
    vSqlWhere = gUtilSQL.fSQLNumberValueWithAnd("", "ConsecutivoCxp", valConsecutivoCxp)
    vSqlWhere = gUtilSQL.fSQLNumberValueWithAnd(vSqlWhere, "dbo.COMPANIA.ConsecutivoCompania", valConsecutivoCompania)
    If (valTieneContabilidadActiva And valConsecutivoPeriodo > 0) Then
        vSqlWhere = gUtilSQL.fSQLNumberValueWithAnd(vSqlWhere, "PERIODO.ConsecutivoPeriodo", valConsecutivoPeriodo)
    End If
    vSqlWhere = gUtilSQL.getWhereSQL(vSqlWhere)
    vSql = ""
    vSql = "SELECT "
    vSql = vSql & "Comun.Gv_Municipio_B1.NombreMunicipio,"
    vSql = vSql & "NumeroComprobanteImpuestoMunicipal AS NumeroComprobante,"
    vSql = vSql & vCxPVista.GetViewNameRetencionImpuestoMunicipalCxP() & ".Numero,"
    vSql = vSql & vCxPVista.GetViewNameRetencionImpuestoMunicipalCxP() & ".NombreCompania AS RazonSocialAgente, "
    vSql = vSql & vCxPVista.GetViewNameRetencionImpuestoMunicipalCxP() & ".CodigoContribuyente AS NumeroLicenciaActividadEconominca,"
    vSql = vSql & "RifCompaniaModelo1 AS NumeroRifAgente,"
    vSql = vSql & vCxPVista.GetViewNameRetencionImpuestoMunicipalCxP() & ".Fecha AS FechaCxp,"
    vSql = vSql & vCxPVista.GetViewNameRetencionImpuestoMunicipalCxP() & ".Direccion AS DireccionAgente,"
    vSql = vSql & "NombreProveedor AS RazonSocialSujetoRetenido,"
    vSql = vSql & vCxPVista.GetViewNameRetencionImpuestoMunicipalCxP() & ".CodigoContribuyenteProveedor AS  NumeroLicenciaActividadEconomincaRetenido,"
    vSql = vSql & "RifProveedorModelo1 AS NumeroRifAgenteSujetoRetenido, Telefono1 AS TelefonoAgenteSujetoRetenido, "
    vSql = vSql & gUtilSQL.fYear("FechaAplicacionImpuestoMunicipal", "YEAR") & ", "
    vSql = vSql & gUtilSQL.fSQLFillRightWithCaracter(gUtilSQL.fCast(gUtilSQL.fMonth("FechaAplicacionImpuestoMunicipal", ""), eTDSS_VARCHAR, ""), 2, "0") & gUtilSQL.fAlias("MES") & ", "
    vSql = vSql & "DireccionProveedor  AS   DireccionSujetoRetenido,"
    vSql = vSql & fTipoDeOperacion() & ", "
    vSql = vSql & vCxPVista.GetViewNameRetencionImpuestoMunicipalCxP() & ".Fecha  AS FechaCxpDetalle,"
    vSql = vSql & gUtilSQL.getIIF("TipoDeCxP = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeTransaccion.eTD_FACTURA), vCxPVista.GetViewNameRetencionImpuestoMunicipalCxP() & ".Numero", gUtilSQL.fSimpleSqlValue(""), True) & " AS NumeroCxp, "
    vSql = vSql & "NumeroControl AS NumeroControlFactura,"
    vSql = vSql & gUtilSQL.getIIF("TipoDeCxP = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeTransaccion.eTD_NOTADEDEBITO), vCxPVista.GetViewNameRetencionImpuestoMunicipalCxP() & ".Numero", gUtilSQL.fSimpleSqlValue(""), True) & " AS NumeroDeNotaDebito, "
    vSql = vSql & gUtilSQL.getIIF("TipoDeCxP = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeTransaccion.eTD_NOTADECREDITO), vCxPVista.GetViewNameRetencionImpuestoMunicipalCxP() & ".Numero", gUtilSQL.fSimpleSqlValue(""), True) & " AS NumeroDeNotaCREDITO, "
    vSql = vSql & fTipoDeTransaccion() & " , "
    vSql = vSql & "NumeroDeFacturaAfectada AS NumeroDeLaFacturaAfectada,"
    vSql = vSql & "MontoBaseImponible AS TotalComprasSinIVABaseImponible,"
    vSql = vSql & "AlicuotaRetencion AS  ALICUOTA,"
    vSql = vSql & "CodigoActividad AS GRUPO ,"
    vSql = vSql & "MontoRetencion  As ImpuestoMunicipalRetenido, "
    vSql = vSql & "NumeroRUC  As NumeroRUC, "
    vSql = vSql & "FechaAplicacionImpuestoMunicipal, "
    vSql = vSql & "MontoExento  As MontoExentoCxp, "
    vSql = vSql & "MontoGravado  As MontoGravadoCxp, "
    vSql = vSql & "MontoIva  As MontoIvaCxp, "
    vSql = vSql & "MontoExento +  MontoGravado + MontoIva  As MontoTotalFacturadoCxp, "
    vSql = vSql & vCxPVista.GetViewNameRetencionImpuestoMunicipalCxP() & ".Descripcion  As DescripcionActividad, "
    vSql = vSql & "Comprobante.Numero as NroComprobanteCxp, "
    vSql = vSql & vCxPVista.GetViewNameRetencionImpuestoMunicipalCxP() & ".CambioABolivares AS CambioABolivares, "
    vSql = vSql & vCxPVista.GetViewNameRetencionImpuestoMunicipalCxP() & ".MonedaDocumento  "
    vSql = vSql & " FROM dbo.COMPANIA "
    vSql = vSql & " INNER JOIN " & vCxPVista.GetViewNameRetencionImpuestoMunicipalCxP() & " ON "
    vSql = vSql & " dbo.COMPANIA.ConsecutivoCompania = " & vCxPVista.GetViewNameRetencionImpuestoMunicipalCxP() & ".ConsecutivoCompania "
    vSql = vSql & " INNER JOIN Comun.Gv_MunicipioCiudad_B1 ON "
    vSql = vSql & " dbo.COMPANIA.ConsecutivoMunicipio = Comun.Gv_MunicipioCiudad_B1.Consecutivo "
    vSql = vSql & " INNER JOIN Comun.Gv_Municipio_B1 ON "
    vSql = vSql & " Comun.Gv_MunicipioCiudad_B1.CodigoMunicipio = Comun.Gv_Municipio_B1.Codigo "
    vSql = vSql & " LEFT OUTER JOIN PERIODO ON"
    vSql = vSql & " dbo.COMPANIA.ConsecutivoCompania = PERIODO.ConsecutivoCompania "
    vSql = vSql & " LEFT OUTER JOIN COMPROBANTE ON"
    vSql = vSql & " PERIODO.ConsecutivoPeriodo = COMPROBANTE.ConsecutivoPeriodo AND "
    vSql = vSql & " COMPROBANTE.ConsecutivoDocOrigen = " & vCxPVista.GetViewNameRetencionImpuestoMunicipalCxP() & ".ConsecutivoCxp "
    vSql = vSql & vSqlWhere
    fSQLComprobanteRetencionImpuestoMunicipalEnCxP = vSql
    Set vCxPVista = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: fSQLComprobanteRetencionImpuestoMunicipalEnCxP = ""
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLComprobanteRetencionImpuestoMunicipalEnCxP", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fTipoDeTransaccion() As String
 Dim gEnumProyecto As clsEnumAdministrativo
   Dim vSql As String
   Dim vCxPVista As clsCxPVista
   Dim vCadenaComparacion As String
   Dim vCadenaResultadoStr As String
   Dim vSeparador As String
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   Set vCxPVista = New clsCxPVista
   vSeparador = gTexto.fSeparadorStandardDeElementosString
   vCadenaComparacion = gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeTransaccionMunicipal.eTDTM_NoAplica)
   vCadenaComparacion = vCadenaComparacion & vSeparador & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeTransaccionMunicipal.eTDTM_Compra)
   vCadenaComparacion = vCadenaComparacion & vSeparador & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeTransaccionMunicipal.eTDTM_Servicio)
   vCadenaResultadoStr = gUtilSQL.fSimpleSqlValue("NA")
   vCadenaResultadoStr = vCadenaResultadoStr & vSeparador & gUtilSQL.fSimpleSqlValue("C")
   vCadenaResultadoStr = vCadenaResultadoStr & vSeparador & gUtilSQL.fSimpleSqlValue("S")
   vSql = gUtilSQL.DfSQLCaseIf(vCxPVista.GetViewNameRetencionImpuestoMunicipalCxP() & ".TipoDeTransaccionImpuesto", vCadenaComparacion, vCadenaResultadoStr, gTexto.fSeparadorStandardDeElementosString, "=", "TipoDeTransaccion")
   Set gEnumProyecto = Nothing
   Set vCxPVista = Nothing
  fTipoDeTransaccion = vSql
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fTipoDeTransaccion", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
 
Private Function fTipoDeOperacion() As String
   Dim gEnumProyecto As clsEnumAdministrativo
   Dim vCxPVista As clsCxPVista
   Dim vSql As String
   Dim vCadenaComparacion As String
   Dim vCadenaResultadoStr As String
   Dim vSeparador As String
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   Set vCxPVista = New clsCxPVista
   vSeparador = gTexto.fSeparadorStandardDeElementosString
   vCadenaComparacion = gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeTransaccion.eTD_FACTURA)
   vCadenaComparacion = vCadenaComparacion & vSeparador & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeTransaccion.eTD_NOTADECREDITO)
   vCadenaComparacion = vCadenaComparacion & vSeparador & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeTransaccion.eTD_NOTADEDEBITO)
   vCadenaResultadoStr = gUtilSQL.fSimpleSqlValue("FT")
   vCadenaResultadoStr = vCadenaResultadoStr & vSeparador & gUtilSQL.fSimpleSqlValue("NC")
   vCadenaResultadoStr = vCadenaResultadoStr & vSeparador & gUtilSQL.fSimpleSqlValue("ND")
   vSql = gUtilSQL.DfSQLCaseIf(vCxPVista.GetViewNameRetencionImpuestoMunicipalCxP() & ".TipoDeCxp ", vCadenaComparacion, vCadenaResultadoStr, gTexto.fSeparadorStandardDeElementosString, "=", "TipoDeOperacion ")
   Set gEnumProyecto = Nothing
   Set vCxPVista = Nothing
   fTipoDeOperacion = vSql
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fTipoDeOperacion", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSqlWhereArchivoDeTextoRetencionImpuestos(ByVal valAno As Long, ByVal valMes As Long, ByVal valConsecutivoCompania As Long) As String
   Dim vResult As String
   Dim vSqlWhere As String
   Dim vSql As String
   Dim vFechaDesde As Date
   Dim vFechaHasta As Date
   Dim vCxPVista As clsCxPVista
   Dim vSqlStatusCxP As String
   On Error GoTo h_ERROR
   Set vCxPVista = New clsCxPVista
   vResult = ""
   
   vFechaDesde = gUtilDate.fObtenLaFechaAPartirDelMesYAnoAplicacion(gTexto.llenaConCaracterALaIzquierda(gConvert.fConvierteAString(valMes), "0", 2), gConvert.fConvierteAString(valAno), True)
   vFechaHasta = gUtilDate.fObtenLaFechaAPartirDelMesYAnoAplicacion(gTexto.llenaConCaracterALaIzquierda(gConvert.fConvierteAString(valMes), "0", 2), gConvert.fConvierteAString(valAno), False)
   
   vSqlStatusCxP = gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ABONADO) & ", " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_PORCANCELAR) & ", "
   vSqlStatusCxP = vSqlStatusCxP & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_CANCELADO) & ", " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_REFINANCIADO) & ", " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_CHEQUEDEVUELTO)
   vSqlWhere = vCxPVista.GetViewNameRetencionImpuestoMunicipalCxP() & ".Status IN (" & vSqlStatusCxP & ")"
   vSqlWhere = gUtilSQL.fSQLValueDatesBetween(vSqlWhere, "FechaAplicacionImpuestoMunicipal", vFechaDesde, vFechaHasta)
   vSqlWhere = gUtilSQL.fSQLNumberValueWithAnd(vSqlWhere, "ConsecutivoCompania", valConsecutivoCompania)
   vSqlWhere = gUtilSQL.getWhereSQL(vSqlWhere)
   
   vSql = vSql & vSqlWhere
   vResult = vSql
   fSqlWhereArchivoDeTextoRetencionImpuestos = vResult
   Set vCxPVista = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlWhereArchivoDeTextoRetencionImpuestos", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLResumenRetencionesActividadesEconomicas(ByVal valAno As Long, ByVal valMes As Long, ByVal valConsecutivoCompania As Long) As String
   Dim vSql As String
   Dim vSqlWhere As String
   Dim vCxPVista As clsCxPVista
   Dim vFechaDesde As Date
   Dim vFechaHasta As Date
   Dim vSqlStatusCxP As String
   On Error GoTo h_ERROR
   Set vCxPVista = New clsCxPVista
   
    vSqlStatusCxP = gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ABONADO) & ", " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_PORCANCELAR)
    vSqlStatusCxP = vSqlStatusCxP & ", " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_CANCELADO) & ", " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_REFINANCIADO) & ", " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_CHEQUEDEVUELTO)
    vFechaDesde = gUtilDate.fObtenLaFechaAPartirDelMesYAnoAplicacion(gTexto.llenaConCaracterALaIzquierda(gConvert.fConvierteAString(valMes), "0", 2), gConvert.fConvierteAString(valAno), True)
    vFechaHasta = gUtilDate.fObtenLaFechaAPartirDelMesYAnoAplicacion(gTexto.llenaConCaracterALaIzquierda(gConvert.fConvierteAString(valMes), "0", 2), gConvert.fConvierteAString(valAno), False)
    vSqlWhere = vCxPVista.GetViewNameRetencionImpuestoMunicipalCxP() & ".Status IN (" & vSqlStatusCxP & ")"
    vSqlWhere = gUtilSQL.fSQLValueDatesBetween(vSqlWhere, "FechaAplicacionImpuestoMunicipal", vFechaDesde, vFechaHasta)
    vSqlWhere = gUtilSQL.fSQLNumberValueWithAnd(vSqlWhere, vCxPVista.GetViewNameRetencionImpuestoMunicipalCxP() & ".ConsecutivoCompania", valConsecutivoCompania)
    vSqlWhere = gUtilSQL.getWhereSQL(vSqlWhere)
    vSql = ""
    vSql = "SELECT "
    vSql = vSql & "Comun.Gv_Municipio_B1.NombreMunicipio,"
    vSql = vSql & vCxPVista.GetViewNameRetencionImpuestoMunicipalCxP() & ".CodigoContribuyente,"
    vSql = vSql & "NombreCompania AS RazonSocialAgente, "
    vSql = vSql & vCxPVista.GetViewNameRetencionImpuestoMunicipalCxP() & ".Direccion AS DireccionAgente,"
    vSql = vSql & "RifCompaniaModelo3 AS NumeroRifAgente,"
    vSql = vSql & "RifProveedorModelo3 AS NumeroRifAgenteSujetoRetenido,"
    vSql = vSql & "NombreProveedor AS RazonSocialSujetoRetenido,"
    vSql = vSql & "Fecha,"
    vSql = vSql & "FechaAplicacionImpuestoMunicipal,"
    vSql = vSql & "NumeroControl AS NumeroControlFactura,"
    vSql = vSql & "Numero AS NumeroCxp,"
    vSql = vSql & gUtilSQL.fYear("FechaAplicacionImpuestoMunicipal", "YEAR") & ", "
    vSql = vSql & gUtilSQL.fMonth("FechaAplicacionImpuestoMunicipal", "MES") & ","
    vSql = vSql & "((MontoExento+MontoGravado+MontoIva+TotalOtrosImpuestos) * CambioABolivares) AS CantidadPagada,"
    vSql = vSql & "MontoBaseImponible,"
    vSql = vSql & "AlicuotaRetencion,"
    vSql = vSql & "MontoRetencion,"
    vSql = vSql & gUtilSQL.fNumToStrSQL(1) & " AS Contador, "
    vSql = vSql & "CodigoActividad, "
    vSql = vSql & "NumeroRuc, "
    vSql = vSql & "ROW_NUMBER() OVER (ORDER BY Comun.Gv_Municipio_B1.NombreMunicipio) AS NumeroConsecutivo, "
    vSql = vSql & "(IGV_RetencionImpuestoMunicipalCxP.MontoExento + IGV_RetencionImpuestoMunicipalCxP.MontoGravado + IGV_RetencionImpuestoMunicipalCxP.MontoIva) AS MontoTotal, "
    vSql = vSql & " IGV_RetencionImpuestoMunicipalCxP.MontoExento, "
    vSql = vSql & " IGV_RetencionImpuestoMunicipalCxP.NumeroComprobanteImpuestoMunicipal"
    vSql = vSql & " FROM dbo.COMPANIA "
    vSql = vSql & " INNER JOIN " & vCxPVista.GetViewNameRetencionImpuestoMunicipalCxP() & " ON "
    vSql = vSql & " dbo.COMPANIA.ConsecutivoCompania = " & vCxPVista.GetViewNameRetencionImpuestoMunicipalCxP() & ".ConsecutivoCompania "
    vSql = vSql & " INNER JOIN Comun.Gv_MunicipioCiudad_B1 ON "
    vSql = vSql & " dbo.COMPANIA.ConsecutivoMunicipio = Comun.Gv_MunicipioCiudad_B1.Consecutivo "
    vSql = vSql & " INNER JOIN Comun.Gv_Municipio_B1 ON "
    vSql = vSql & " Comun.Gv_MunicipioCiudad_B1.CodigoMunicipio = Comun.Gv_Municipio_B1.Codigo "
    vSql = vSql & " "
    vSql = vSql & vSqlWhere
    fSQLResumenRetencionesActividadesEconomicas = vSql
    Set vCxPVista = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: fSQLResumenRetencionesActividadesEconomicas = ""
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLResumenRetencionesActividadesEconomicas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLPagoMasivoDeDetracciones(ByVal valAno As Long, ByVal valMes As Long, ByVal valConsecutivoCompania As Long) As String
   Dim vSql As String
   On Error GoTo h_ERROR
   vSql = "SELECT " & gUtilSQL.fSimpleSqlValue("6") & " AS TipoDocumentoProveedor,"
   vSql = vSql & " Adm.Proveedor.NumeroRIF AS NumeroRUC, Adm.Proveedor.NombreProveedor AS NombreProveedor, "
   vSql = vSql & gUtilSQL.fSimpleSqlValue(gTexto.nCar("0", 9)) & " AS NumProforma, OtrosImpuestosCxP.CodigoOI AS CodigoDetraccion,"
   vSql = vSql & " Adm.Proveedor.NumeroCuentaBancaria AS NumeroCuentaBanco, "
   vSql = vSql & " (OtrosImpuestosCxP.Monto * cxP.CambioAbolivares) AS MontoDeposito, "
   vSql = vSql & gUtilSQL.fSimpleSqlValue("01") & " AS TipoOperacion, " ' ojo una vez definido en formulario de peru hacer case
   vSql = vSql & gUtilSQL.fSimpleSqlValue(gConvert.fConvierteAString(valAno) & gTexto.llenaConCaracterALaIzquierda(gConvert.fConvierteAString(valMes), "0", 2)) & " AS PeriodoTributario, "
   vSql = vSql & gUtilSQL.getIIF("cxP.TipoDeCxp = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeTransaccion.eTD_BOLETADEVENTA), _
            gUtilSQL.fSimpleSqlValue("03"), gUtilSQL.fSimpleSqlValue("01"), True) & " AS TipoComprobante, "
   vSql = vSql & "cxP.numero AS Comprobante"
   vSql = vSql & " FROM cxP INNER JOIN Adm.Proveedor ON cxP.codigoproveedor = Adm.Proveedor.CodigoProveedor"
   vSql = vSql & " AND cxp.ConsecutivoCompania = Adm.Proveedor.ConsecutivoCompania"
   vSql = vSql & " INNER JOIN OtrosImpuestosCxP ON cxp.ConsecutivoCxp = OtrosImpuestosCxP.ConsecutivoCxP"
   vSql = vSql & " AND cxP.ConsecutivoCompania = OtrosImpuestosCxP.ConsecutivoCompania"
   vSql = vSql & " WHERE cxP.ConsecutivoCompania = " & gUtilSQL.fSimpleSqlValueForNumeric(valConsecutivoCompania)
   vSql = vSql & " AND cxP.MesDeAplicacion = " & gUtilSQL.fSimpleSqlValueForNumeric(valMes)
   vSql = vSql & " AND cxP.AnoDeAplicacion = " & gUtilSQL.fSimpleSqlValueForNumeric(valAno)
   vSql = vSql & " AND cxP.Status = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_CANCELADO)
   fSQLPagoMasivoDeDetracciones = vSql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: fSQLPagoMasivoDeDetracciones = ""
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLPagoMasivoDeDetracciones", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
