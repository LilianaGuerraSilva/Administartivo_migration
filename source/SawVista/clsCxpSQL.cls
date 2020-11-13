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
   Dim SQL As String
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
   SQL = "SELECT "
   SQL = SQL & "Proveedor.CodigoProveedor AS Codigo, "
   SQL = SQL & "Proveedor.NombreProveedor AS Nombre, "
   SQL = SQL & "Proveedor.Direccion AS Direccion, "
   SQL = SQL & sqlMonedaCxP & " AS Moneda, "
   SQL = SQL & sqlCambioBsCxP & " AS CambioBs, "
   SQL = SQL & gUtilSQL.fSimpleSqlValue("0") & " AS TipoReporte, "
   SQL = SQL & gUtilSQL.fSimpleSqlValue("Cuentas por Pagar") & " AS TituloTipoReporte, "
   SQL = SQL & sqlTipoDeCxP & " AS TipoDocumento, "
   SQL = SQL & "CxP.Numero AS Numero, "
   SQL = SQL & "CxP.Fecha AS Fecha, "
   SQL = SQL & "CxP.FechaVencimiento AS FechaVencimiento, "
   SQL = SQL & gUtilSQL.getIIF(sqlDateDiffCxP & " > 0 ", sqlDateDiffCxP, "0", True) & " AS DiasVencidos, "
   SQL = SQL & gUtilSQL.getIIF(sqlDateDiffCxP & " > 0 ", sqlMontoCxP, "0", True) & " AS MontoVencido, "
   SQL = SQL & gUtilSQL.getIIF(sqlDateDiffCxP & " <=0 ", sqlMontoCxP, "0", True) & " AS MontoNoVencido "
   SQL = SQL & " FROM Proveedor INNER JOIN CxP ON "
   SQL = SQL & "Proveedor.ConsecutivoCompania = CxP.ConsecutivoCompania"
   SQL = SQL & " AND Proveedor.CodigoProveedor = CxP.CodigoProveedor"
   SQL = SQL & " WHERE Proveedor.ConsecutivoCompania = " & valConsecutivoCompania
   SQL = SQL & " AND CxP.Status IN (" & sqlStatusCxP & ")"
   If ValImprimirProveedorUnico Then
      SQL = SQL & " AND Proveedor.CodigoProveedor = " & gUtilSQL.fSimpleSqlValue(ValCodigoProveedor)
   End If
   SQL = SQL & "UNION SELECT "
   SQL = SQL & "Proveedor.CodigoProveedor AS Codigo, "
   SQL = SQL & "Proveedor.NombreProveedor AS Nombre, "
   SQL = SQL & "Proveedor.Direccion AS Direccion, "
   SQL = SQL & sqlMonedaAnticipo & " AS Moneda, "
   SQL = SQL & sqlCambioBsAnticipo & " AS CambioBs, "
   SQL = SQL & gUtilSQL.fSimpleSqlValue("1") & " AS TipoReporte, "
   SQL = SQL & gUtilSQL.fSimpleSqlValue("Anticipos") & " AS TituloTipoReporte, "
   SQL = SQL & sqlTipoDeAnticipo & " AS TipoDocumento, "
   SQL = SQL & "Anticipo.Numero AS Numero, "
   SQL = SQL & "Anticipo.Fecha AS Fecha, "
   SQL = SQL & gUtilSQL.fSimpleSqlValue("") & " AS FechaVencimiento, "
   SQL = SQL & "0 AS DiasVencidos, "
   SQL = SQL & "0 AS MontoVencido, "
   SQL = SQL & sqlMontoAnticipo & " AS MontoNoVencido "
   SQL = SQL & " FROM Proveedor INNER JOIN Anticipo ON "
   SQL = SQL & "Proveedor.ConsecutivoCompania = Anticipo.ConsecutivoCompania"
   SQL = SQL & " AND Proveedor.CodigoProveedor = Anticipo.CodigoProveedor"
   SQL = SQL & " WHERE Proveedor.ConsecutivoCompania = " & valConsecutivoCompania
   SQL = SQL & " AND Anticipo.Status IN (" & sqlStatusAnticipo & ")"
   SQL = SQL & " AND Anticipo.Tipo = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeAnticipo.eTDA_PAGADO)
   If ValImprimirProveedorUnico Then
      SQL = SQL & " AND Proveedor.CodigoProveedor = " & gUtilSQL.fSimpleSqlValue(ValCodigoProveedor)
   End If
   SQL = SQL & " AND Anticipo.EsUnaDevolucion = " & gUtilSQL.fBooleanToSqlValue(False)
   SQL = SQL & " ORDER BY Codigo, Moneda, TipoReporte, Fecha"
   fSQLEstadoDeCuenta = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLEstadoDeCuenta", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConstruirSqlDeListarProveedores(ByVal UsaCodigoProveedorEnPantalla As Boolean, _
                                                  ByVal valcmbOrdenadoPor As String, _
                                                   ByVal ValFormaDeEscogerCompaniaToString As String, _
                                                    ByVal valConsecutivoCompania As String) As String
   Dim SQL As String
   Dim gEnumTablaRetencion As clsEnumTablaRetencion
   On Error GoTo h_ERROR
   Set gEnumTablaRetencion = New clsEnumTablaRetencion
   SQL = "SELECT "
   If UsaCodigoProveedorEnPantalla Or valcmbOrdenadoPor = ValFormaDeEscogerCompaniaToString Then
      SQL = SQL & "CodigoProveedor" & gUtilSQL.CharConcat & gUtilSQL.fSimpleSqlValue(" - ") & gUtilSQL.CharConcat & "NombreProveedor AS Nombre, "
   Else
      SQL = SQL & "NombreProveedor AS Nombre, "
   End If
   SQL = SQL & "NumeroRif as NumeroRifProveedor, "
   SQL = SQL & "Direccion AS DireccionProveedor, "
   SQL = SQL & "Telefonos AS TelefonoProveedor, "
   SQL = SQL & "Fax AS FaxProveedor, "
   SQL = SQL & "Contacto AS ContactoProveedor, "
   SQL = SQL & "Proveedor.PorcentajeRetencionIVA AS PorcentajeDeIva, "
   SQL = SQL & "Proveedor.CodigoRetencionUsual AS CodigoRetencionUsual, "
   SQL = SQL & gUtilSQL.DfSQLCaseIfForEnum("Proveedor.TipoDePersona", enum_TipodePersonaRetencion.eTP_PJ_DOMICILIADA, _
               gEnumTablaRetencion.fEnumTipoDePersonaRetencionSiglasToStringInArray(True), "TipoDePersona")
   SQL = SQL & " FROM Proveedor"
   SQL = SQL & " WHERE Proveedor.ConsecutivoCompania = " & valConsecutivoCompania
   SQL = SQL & " ORDER BY "
   If valcmbOrdenadoPor = ValFormaDeEscogerCompaniaToString Then
      SQL = SQL & "Proveedor.CodigoProveedor"
   Else
      SQL = SQL & "Proveedor.NombreProveedor"
   End If
   fConstruirSqlDeListarProveedores = SQL
   Set gEnumTablaRetencion = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fConstruirSqlDeListarProveedores ", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLHistoricoDeProveedor(ByVal valReporteEnMonedaLocal As Boolean, ByVal valConsecutivoCompania As String, _
                                 ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal ValCmbCantidadAImprimirUnProveedor As String, _
                                 ByVal ValtxtCodigoProveedor As String, ByVal gUltimaTasaDeCambio As Object, ByVal gMonedaLocalActual As Object) As String
   Dim SQL As String
   Dim SQLCXP As String
   Dim sqlAnticipo As String
   Dim sqlOrderBy As String
   On Error GoTo h_ERROR
   SQLCXP = fSQLHistoricoDeProveedorCxP(valReporteEnMonedaLocal, valConsecutivoCompania, valFechaInicial, valFechaFinal, _
                           ValCmbCantidadAImprimirUnProveedor, ValtxtCodigoProveedor, gUltimaTasaDeCambio, gMonedaLocalActual)
   sqlAnticipo = fSQLHistoricoDeProveedorAnticipos(valReporteEnMonedaLocal, valConsecutivoCompania, valFechaInicial, valFechaFinal, _
                           ValCmbCantidadAImprimirUnProveedor, ValtxtCodigoProveedor, gUltimaTasaDeCambio, gMonedaLocalActual)
   sqlOrderBy = " ORDER BY Codigo, MonedaReporte, TipoReporte, NumeroDocumentoGrupo, NumeroPago, FechaDocumento, FechaPago"
   SQL = SQLCXP
   SQL = SQL & " UNION " & sqlAnticipo
   SQL = SQL & sqlOrderBy
h_EXIT: On Error GoTo 0
   fSQLHistoricoDeProveedor = SQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLHistoricoDeProveedor", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLHistoricoDeProveedorCxP(ByVal valReporteEnMonedaLocal As Boolean, ByVal valConsecutivoCompania As String, _
                                 ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal ValCmbCantidadAImprimirUnProveedor As String, _
                                 ByVal ValtxtCodigoProveedor As String, ByVal gUltimaTasaDeCambio As Object, ByVal gMonedaLocalActual As Object) As String
   Dim SQL As String
   Dim usarCambioBs As Boolean
   Dim sqlSaldoActual As String
   Dim sqlMonto As String
   Dim sqlMontoPagado As String
   Dim sqlMontoPagadoReposicion As String
   Dim SqlSaldoInicial As String
   Dim sqlTipoDeCxP As String
   Dim sqlTipoDeDocPagado As String
   Dim sqlStatusPagos As String
   Dim sqlStatusReposicion As String
   Dim sqlStatusPagosAnulado As String
   Dim sqlStatusReposicionAnulada As String
   Dim sqlMonedaCxP As String
   Dim sqlCambioCxP As String
   Dim sqlMonedaPag As String
   Dim sqlCambioPag As String
   Dim gEnumProyecto As clsEnumAdministrativo
   Dim gEnumReport As clsEnumReport
   Dim sqlMonedaReporte As String
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   Set gEnumReport = New clsEnumReport
   usarCambioBs = valReporteEnMonedaLocal
   SqlSaldoInicial = fSQLSaldoInicialAnalisisCxPHistoricoCxP(True, valFechaInicial, valConsecutivoCompania, valReporteEnMonedaLocal, _
                                    gUltimaTasaDeCambio, gMonedaLocalActual)
   sqlMonedaCxP = "CxP.Moneda"
   sqlCambioCxP = "CxP.CambioABolivares"
   sqlMonedaPag = "Pago.Moneda"
   sqlCambioPag = "Pago.CambioaBolivares"
   sqlStatusPagos = "Pago.StatusOrdenDePago"
   sqlStatusReposicion = "Adm.Rendicion.StatusRendicion"
   sqlStatusPagosAnulado = sqlStatusPagos & " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusPago.eSP_ANULADA)
   sqlStatusReposicionAnulada = sqlStatusReposicion & " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusRendicion.eSRd_ANULADA)
   sqlMontoPagado = "(DocumentoPagado.MontoAbonado / DocumentoPagado.CambioAMonedaDelPago )"
   sqlMontoPagado = gUtilSQL.getIIF(sqlStatusPagosAnulado, "0", gUtilSQL.DfCCurSQL(sqlMontoPagado))
   sqlMontoPagadoReposicion = "(Adm.DetalleDeRendicion.MontoExento+ Adm.DetalleDeRendicion.MontoGravable + Adm.DetalleDeRendicion.MontoIVA)"
   sqlMontoPagadoReposicion = gUtilSQL.getIIF(sqlStatusReposicionAnulada, "0", gUtilSQL.DfCCurSQL(sqlMontoPagadoReposicion))
   sqlMonto = "(CxP.MontoExento + CxP.MontoGravado + CxP.MontoIva)"
   sqlSaldoActual = "(" & sqlMonto & " - CxP.MontoAbonado)"
   sqlTipoDeCxP = gUtilSQL.DfSQLCaseIfForEnum("CxP.TipoDeCxP", _
                  enum_TipoDeTransaccion.eTD_FACTURA, gEnumProyecto.fEnumTipoDeTransaccioToStringInArray(True, False), "")
   sqlTipoDeDocPagado = gUtilSQL.fSimpleSqlValue("Pagos")
   sqlMonedaReporte = "CxP.Moneda"
   If usarCambioBs Then
      sqlMonto = gUtilSQL.getIIF("CxP.CodigoMoneda <> " & gMonedaLocalActual.fSQLCodigoMonedaALaFecha("CxP.Fecha"), _
                        gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCambioCxP, sqlMonedaCxP, sqlMonto, False, ""), _
                        gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, sqlMonto, "CxP.Fecha"), True)
      sqlSaldoActual = gUtilSQL.getIIF("CxP.CodigoMoneda <> " & gMonedaLocalActual.fSQLCodigoMonedaALaFecha("CxP.Fecha"), _
                        gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCambioCxP, sqlMonedaCxP, sqlSaldoActual, False, ""), _
                        gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, sqlSaldoActual, "CxP.Fecha"), True)
      sqlMontoPagado = gUtilSQL.getIIF("CxP.CodigoMoneda <> " & gMonedaLocalActual.fSQLCodigoMonedaALaFecha("CxP.Fecha"), _
                        gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCambioCxP, sqlMonedaCxP, sqlMontoPagado, False, ""), _
                        gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, sqlMontoPagado, "CxP.Fecha"), True)
      sqlMonedaCxP = gUtilSQL.getIIF(sqlMonedaCxP & " = " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda), sqlMonedaCxP, gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda), True)
      sqlMonedaReporte = gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda)
   End If
   SQL = "SELECT "
   SQL = SQL & gUtilSQL.fSimpleSqlValue("0") & " AS TipoReporte, "
   SQL = SQL & "CxP.CodigoProveedor AS Codigo, "
   SQL = SQL & "Proveedor.NombreProveedor AS Nombre, "
   SQL = SQL & sqlMonedaCxP & " AS Moneda, "
   SQL = SQL & sqlMonedaReporte & " AS MonedaReporte, "
   SQL = SQL & gUtilSQL.fSimpleSqlValue("Cuentas por Pagar") & " AS TituloTipoReporte, "
   SQL = SQL & SqlSaldoInicial & " AS SaldoInicial, "
   SQL = SQL & "CxP.Fecha AS FechaDocumento, "
   SQL = SQL & sqlTipoDeCxP & " AS TipoDeDocumento, "
   SQL = SQL & "CxP.Numero AS NumeroDocumento, "
   SQL = SQL & gUtilSQL.fCast("CxP.CodigoProveedor" & fSQLSeparadorDeCamposStandardDelNroDocumentoOrigen & "CxP.Numero", eTDSS_VARCHAR, "") & " AS NumeroDocumentoGrupo, "
   SQL = SQL & sqlMonto & " AS MontoOriginal, "
   SQL = SQL & sqlSaldoActual & " AS SaldoActual, "
   SQL = SQL & sqlTipoDeDocPagado & " AS TipoDocumentoDetalle, "
   SQL = SQL & gUtilSQL.fSimpleSqlValue("") & " AS NumeroPago, "
   SQL = SQL & gUtilSQL.fSimpleSqlValue("") & " AS FechaPago, "
   SQL = SQL & "0 AS MontoPagado, "
   SQL = SQL & gUtilSQL.fSimpleSqlValue("") & " AS StatusPago "
   SQL = SQL & " FROM (Proveedor INNER JOIN CxP ON"
   SQL = SQL & " (Proveedor.CodigoProveedor = CxP.CodigoProveedor)"
   SQL = SQL & " AND (Proveedor.ConsecutivoCompania = CxP.ConsecutivoCompania)) "
   SQL = SQL & " WHERE CxP.ConsecutivoCompania = " & valConsecutivoCompania
   SQL = SQL & " AND " & gUtilSQL.DfSQLDateValueBetween("CxP.Fecha", valFechaInicial, valFechaFinal)
   SQL = SQL & " AND CxP.Status <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ANULADO)
   If ValCmbCantidadAImprimirUnProveedor = gEnumReport.enumCantidadAImprimirToString(eCI_uno) Then
      SQL = SQL & " AND CxP.CodigoProveedor = " & gUtilSQL.fSimpleSqlValue(ValtxtCodigoProveedor)
   End If
   SQL = SQL & " UNION SELECT "
   SQL = SQL & gUtilSQL.fSimpleSqlValue("0") & " AS TipoReporte, "
   SQL = SQL & "CxP.CodigoProveedor AS Codigo, "
   SQL = SQL & "Proveedor.NombreProveedor AS Nombre, "
   SQL = SQL & sqlMonedaCxP & " AS Moneda, "
   SQL = SQL & sqlMonedaReporte & " AS MonedaReporte, "
   SQL = SQL & gUtilSQL.fSimpleSqlValue("Cuentas por Pagar") & " AS TituloTipoReporte, "
   SQL = SQL & SqlSaldoInicial & " AS SaldoInicial, "
   SQL = SQL & gUtilSQL.fSimpleSqlValue("") & " AS FechaDocumento, "
   SQL = SQL & gUtilSQL.fSimpleSqlValue("") & " AS TipoDeDocumento, "
   SQL = SQL & gUtilSQL.fSimpleSqlValue("Pagos") & " AS NumeroDocumento, "
   SQL = SQL & gUtilSQL.fCast("CxP.CodigoProveedor" & fSQLSeparadorDeCamposStandardDelNroDocumentoOrigen & "CxP.Numero", eTDSS_VARCHAR, "") & " AS NumeroDocumentoGrupo, "
   SQL = SQL & "0 AS MontoOriginal,"
   SQL = SQL & "0 AS SaldoActual, "
   SQL = SQL & gUtilSQL.fSimpleSqlValue("Pagos") & " AS TipoDocumentoDetalle, "
   SQL = SQL & "Pago.NumeroCheque AS NumeroPago, "
   SQL = SQL & "Pago.Fecha AS FechaPago, "
   SQL = SQL & sqlMontoPagado & " AS MontoPagado, "
   SQL = SQL & sqlStatusPagos & " AS StatusPago "
   SQL = SQL & " FROM Pago RIGHT JOIN "
   SQL = SQL & "(Proveedor INNER JOIN "
   SQL = SQL & "(CxP LEFT JOIN DocumentoPagado ON "
   SQL = SQL & "(CxP.Numero = DocumentoPagado.NumeroDelDocumentoPagado)"
   SQL = SQL & " AND (CxP.ConsecutivoCompania = DocumentoPagado.ConsecutivoCompania))"
   SQL = SQL & " ON (Proveedor.CodigoProveedor = CxP.CodigoProveedor)"
   SQL = SQL & " AND (Proveedor.ConsecutivoCompania = CxP.ConsecutivoCompania))"
   SQL = SQL & " ON (Pago.NumeroComprobante = DocumentoPagado.NumeroComprobante)"
   SQL = SQL & " AND (Pago.ConsecutivoCompania = DocumentoPagado.ConsecutivoCompania)"
   SQL = SQL & " WHERE " & gUtilSQL.DfSQLDateValueBetween("CxP.Fecha", valFechaInicial, valFechaFinal)
   SQL = SQL & " AND CxP.Status <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ANULADO)
   SQL = SQL & " AND Pago.CodigoProveedor = CxP.CodigoProveedor"
   SQL = SQL & " AND " & sqlStatusPagos & " <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusPago.eSP_ANULADA)
   If ValCmbCantidadAImprimirUnProveedor = gEnumReport.enumCantidadAImprimirToString(eCI_uno) Then
      SQL = SQL & " AND CxP.CodigoProveedor = " & gUtilSQL.fSimpleSqlValue(ValtxtCodigoProveedor)
   End If
   SQL = SQL & " AND CxP.ConsecutivoCompania = " & valConsecutivoCompania
    SQL = SQL & " UNION SELECT "
   SQL = SQL & gUtilSQL.fSimpleSqlValue("0") & " AS TipoReporte, "
   SQL = SQL & "CxP.CodigoProveedor AS Codigo, "
   SQL = SQL & "Proveedor.NombreProveedor AS Nombre, "
   SQL = SQL & sqlMonedaCxP & " AS Moneda, "
   SQL = SQL & sqlMonedaReporte & " AS MonedaReporte, "
   SQL = SQL & gUtilSQL.fSimpleSqlValue("Cuentas por Pagar") & " AS TituloTipoReporte, "
   SQL = SQL & SqlSaldoInicial & " AS SaldoInicial, "
   SQL = SQL & gUtilSQL.fSimpleSqlValue("") & " AS FechaDocumento, "
   SQL = SQL & gUtilSQL.fSimpleSqlValue("") & " AS TipoDeDocumento, "
   SQL = SQL & gUtilSQL.fSimpleSqlValue("Reposicion") & " AS NumeroDocumento, "
   SQL = SQL & gUtilSQL.fCast("CxP.CodigoProveedor" & fSQLSeparadorDeCamposStandardDelNroDocumentoOrigen & "CxP.Numero", eTDSS_VARCHAR, "") & " AS NumeroDocumentoGrupo, "
   SQL = SQL & "0 AS MontoOriginal,"
   SQL = SQL & "0 AS SaldoActual, "
   SQL = SQL & gUtilSQL.getIIF("Adm.Rendicion.TipoDeDocumento = " & "1", gUtilSQL.fSimpleSqlValue("Reposicion"), gUtilSQL.fSimpleSqlValue("Rendicion")) & " AS TipoDocumentoDetalle, "
   SQL = SQL & "Adm.Rendicion.Numero AS NumeroPago, "
   SQL = SQL & "Adm.Rendicion.FechaCierre AS FechaPago, "
   SQL = SQL & sqlMontoPagadoReposicion & " AS MontoPagado, "
   SQL = SQL & sqlStatusReposicion & " AS StatusPago "
   SQL = SQL & " FROM Adm.Rendicion RIGHT JOIN "
   SQL = SQL & "(Proveedor INNER JOIN "
   SQL = SQL & "(CxP LEFT JOIN Adm.DetalleDeRendicion ON "
   SQL = SQL & "(CxP.Numero = Adm.DetalleDeRendicion.NumeroDocumento)"
   SQL = SQL & " AND (CxP.ConsecutivoRendicion = Adm.DetalleDeRendicion.ConsecutivoRendicion)"
   SQL = SQL & " AND (CxP.ConsecutivoCompania = Adm.DetalleDeRendicion.ConsecutivoCompania))"
   SQL = SQL & " ON (Proveedor.CodigoProveedor = CxP.CodigoProveedor)"
   SQL = SQL & " AND (Proveedor.ConsecutivoCompania = CxP.ConsecutivoCompania))"
   SQL = SQL & " ON (Adm.Rendicion.Consecutivo = Adm.DetalleDeRendicion.ConsecutivoRendicion)"
   SQL = SQL & " AND (Adm.Rendicion.ConsecutivoCompania = Adm.DetalleDeRendicion.ConsecutivoCompania)"
   SQL = SQL & " WHERE " & gUtilSQL.DfSQLDateValueBetween("CxP.Fecha", valFechaInicial, valFechaFinal)
   SQL = SQL & " AND CxP.Status <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ANULADO)
   SQL = SQL & " AND Adm.DetalleDeRendicion.CodigoProveedor = CxP.CodigoProveedor"
   SQL = SQL & " AND " & sqlStatusReposicion & " <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusRendicion.eSRd_ANULADA)
   If ValCmbCantidadAImprimirUnProveedor = gEnumReport.enumCantidadAImprimirToString(eCI_uno) Then
      SQL = SQL & " AND CxP.CodigoProveedor = " & gUtilSQL.fSimpleSqlValue(ValtxtCodigoProveedor)
   End If
   SQL = SQL & " AND CxP.ConsecutivoCompania = " & valConsecutivoCompania

h_EXIT: On Error GoTo 0
   fSQLHistoricoDeProveedorCxP = SQL
   Set gEnumProyecto = Nothing
   Set gEnumReport = Nothing
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLHistoricoDeProveedorCxP", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLSaldoInicialAnalisisCxPHistoricoCxP(ByVal valDescontarAbonos As Boolean, _
                              ByVal valFechaInicial As Date, ByVal valConsecutivoCompania As String, _
                              ByVal ReporteEnMonedaLocal As Boolean, ByVal gUltimaTasaDeCambio As Object, _
                              ByVal gMonedaLocalActual As Object) As String
   Dim SQL As String
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
   SQL = "(SELECT SUM(" & sqlMonto & ") AS SaldoInicial"
   SQL = SQL & " FROM CxP AS " & sqlCxPTableName
   SQL = SQL & " WHERE " & sqlCxPTableName & ".ConsecutivoCompania = " & valConsecutivoCompania
   SQL = SQL & " AND " & sqlCxPTableName & ".CodigoProveedor = CxP.CodigoProveedor"
   SQL = SQL & " AND " & sqlCxPTableName & ".Moneda = CxP.Moneda"
   SQL = SQL & " AND " & sqlCxPTableName & ".Status <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ANULADO)
   SQL = SQL & " AND " & sqlCxPTableName & ".Fecha < " & gUtilSQL.fDateToSQLValue(valFechaInicial) & ")"
   fSQLSaldoInicialAnalisisCxPHistoricoCxP = SQL
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

Private Function fSQLHistoricoDeProveedorAnticipos(ByVal valReporteEnMonedaLocal As Boolean, ByVal valConsecutivoCompania As String, _
                                 ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal ValCmbCantidadAImprimirUnProveedor As String, _
                                 ByVal ValtxtCodigoProveedor As String, ByVal gUltimaTasaDeCambio As Object, ByVal gMonedaLocalActual As Object) As String
   Dim SQL As String
   Dim usarCambioBs As Boolean
   Dim sqlMontoOriginal As String
   Dim SqlSaldoInicial As String
   Dim sqlSaldoActual As String
   Dim sqlMontoNoDisponible As String
   Dim sqlTipoDeDocumento As String
   Dim sqlStatusPagos As String
   Dim sqlStatusPagosAnulada As String
   Dim sqlMontoAplicado As String
   Dim sqlMonedaAnticipo As String
   Dim sqlCambioAnticipo As String
   Dim sqlMonedaCob As String
   Dim sqlCambioCob As String
   Dim sqlMonedaReporte As String
   Dim sqlMoneda As String
   Dim gEnumProyecto As clsEnumAdministrativo
   Dim gEnumReport As clsEnumReport
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   Set gEnumReport = New clsEnumReport
   SqlSaldoInicial = fSQLSaldoInicialAnalisisCxPHistoricoAnticipos(True, valFechaInicial, valConsecutivoCompania, valReporteEnMonedaLocal, _
                                    gUltimaTasaDeCambio, gMonedaLocalActual)
   usarCambioBs = valReporteEnMonedaLocal
   sqlMonedaAnticipo = "Anticipo.Moneda"
   sqlCambioAnticipo = "Anticipo.Cambio"
   sqlMonedaCob = "Pago.Moneda"
   sqlCambioCob = "Pago.CambioaBolivares"
   sqlMontoOriginal = "Anticipo.MontoTotal"
   sqlMoneda = "Anticipo.Moneda"
   sqlMontoNoDisponible = "(Anticipo.MontoUsado + Anticipo.MontoDevuelto + Anticipo.MontoDiferenciaEnDevolucion)"
   sqlSaldoActual = "(" & sqlMontoOriginal & " - " & sqlMontoNoDisponible & ")"
   sqlTipoDeDocumento = gUtilSQL.fSimpleSqlValue(gEnumProyecto.enumTipoDeAnticipoToString(eTDA_PAGADO))
   sqlStatusPagos = "Pago.StatusOrdenDePago"
   sqlStatusPagosAnulada = sqlStatusPagos & " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusAnticipo.eSDA_ANULADO)
   sqlMontoAplicado = "AnticipoPagado.MontoAplicado"
   sqlMontoAplicado = gUtilSQL.getIIF(sqlStatusPagosAnulada, "0", sqlMontoAplicado)
   sqlMonedaReporte = "Anticipo.Moneda"
   If usarCambioBs Then
      sqlMontoOriginal = gUtilSQL.getIIF("Anticipo.CodigoMoneda <> " & gMonedaLocalActual.fSQLCodigoMonedaALaFecha("Anticipo.Fecha"), _
                        gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("", "Anticipo.Moneda", sqlMontoOriginal, False, ""), _
                        gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, sqlMontoOriginal, "Anticipo.Fecha"), True)
      sqlSaldoActual = gUtilSQL.getIIF("Anticipo.CodigoMoneda <> " & gMonedaLocalActual.fSQLCodigoMonedaALaFecha("Anticipo.Fecha"), _
                        gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("", "Anticipo.Moneda", sqlSaldoActual, False, ""), _
                        gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, sqlSaldoActual, "Anticipo.Fecha"), True)
     sqlMontoAplicado = gUtilSQL.getIIF("Anticipo.CodigoMoneda <> " & gMonedaLocalActual.fSQLCodigoMonedaALaFecha("Pago.Fecha"), _
                        gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("", "Anticipo.Moneda", sqlMontoAplicado, False, ""), _
                        gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, sqlMontoAplicado, "Anticipo.Fecha"), True)
      sqlMonedaReporte = gUtilSQL.getIIF(sqlMonedaAnticipo & " = " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda), sqlMonedaReporte, _
                                          gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda), True)
      sqlMonedaReporte = gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda)
   Else
      sqlMontoAplicado = sqlMontoAplicado & "/ AnticipoPagado.Cambio"
   End If
   SQL = " ( SELECT "
   SQL = SQL & gUtilSQL.fSimpleSqlValue("1") & " AS TipoReporte, "
   SQL = SQL & "Anticipo.CodigoProveedor AS Codigo, "
   SQL = SQL & "Proveedor.NombreProveedor AS Nombre, "
   SQL = SQL & sqlMonedaAnticipo & " AS Moneda, "
   SQL = SQL & sqlMonedaReporte & " AS MonedaReporte, "
   SQL = SQL & gUtilSQL.fSimpleSqlValue("Anticipos") & " AS TituloTipoReporte, "
   SQL = SQL & SqlSaldoInicial & " AS SaldoInicial, "
   SQL = SQL & "Anticipo.Fecha AS FechaDocumento, "
   SQL = SQL & sqlTipoDeDocumento & " AS TipoDeDocumento, "
   SQL = SQL & "Anticipo.Numero AS NumeroDocumento, "
   SQL = SQL & gUtilSQL.fCast("Anticipo.ConsecutivoAnticipo", eTDSS_VARCHAR, "") & " AS NumeroDocumentoGrupo, "
   SQL = SQL & sqlMontoOriginal & " AS MontoOriginal, "
   SQL = SQL & sqlSaldoActual & " AS SaldoActual, "
   SQL = SQL & gUtilSQL.fSimpleSqlValue("Pago") & " AS TipoDocumentoDetalle, "
   SQL = SQL & "Pago.NumeroCheque AS NumeroPago, "
   SQL = SQL & "Pago.Fecha AS FechaPago, "
   SQL = SQL & gUtilSQL.getIIF("Anticipo.Status = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusAnticipo.eSDA_VIGENTE), 0, sqlMontoAplicado) & " AS MontoPagado, "
   SQL = SQL & sqlStatusPagos & " AS StatusPago "
   SQL = SQL & " FROM (Anticipo LEFT JOIN (Pago RIGHT JOIN AnticipoPagado ON "
   SQL = SQL & "(Pago.NumeroComprobante = AnticipoPagado.NumeroComprobante)"
   SQL = SQL & " AND (Pago.ConsecutivoCompania = AnticipoPagado.ConsecutivoCompania))"
   SQL = SQL & " ON (Anticipo.ConsecutivoAnticipo = AnticipoPagado.ConsecutivoAnticipoUsado)"
   SQL = SQL & " AND (Anticipo.ConsecutivoCompania = AnticipoPagado.ConsecutivoCompania)"
   SQL = SQL & " AND " & sqlStatusPagos & " <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusPago.eSP_ANULADA) & ")"
   SQL = SQL & " INNER JOIN Proveedor ON (Anticipo.CodigoProveedor = Proveedor.CodigoProveedor)"
   SQL = SQL & " AND (Anticipo.ConsecutivoCompania = Proveedor.ConsecutivoCompania) "
   SQL = SQL & " WHERE " & gUtilSQL.DfSQLDateValueBetween("Anticipo.Fecha", valFechaInicial, valFechaFinal)
   SQL = SQL & " AND Anticipo.Status <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusAnticipo.eSDA_ANULADO)
   SQL = SQL & " AND Anticipo.EsUnaDevolucion = " & gUtilSQL.fBooleanToSqlValue(False)
   SQL = SQL & " AND Anticipo.Tipo = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeAnticipo.eTDA_PAGADO)
   If ValCmbCantidadAImprimirUnProveedor = gEnumReport.enumCantidadAImprimirToString(eCI_uno) Then
      SQL = SQL & " AND Anticipo.CodigoProveedor = " & gUtilSQL.fSimpleSqlValue(ValtxtCodigoProveedor)
   End If
   SQL = SQL & " AND Anticipo.ConsecutivoCompania = " & valConsecutivoCompania
   
   '********************************************************
   SQL = SQL & " UNION "
   SQL = SQL & " SELECT "
   SQL = SQL & gUtilSQL.fSimpleSqlValue("1") & " AS TipoReporte, "
   SQL = SQL & " Anticipo.CodigoProveedor AS Codigo, "
   SQL = SQL & " Proveedor.NombreProveedor AS Nombre, "
   SQL = SQL & sqlMonedaAnticipo & " AS Moneda, "
   SQL = SQL & sqlMonedaReporte & " AS MonedaReporte, "
   SQL = SQL & gUtilSQL.fSimpleSqlValue("Anticipos") & " AS TituloTipoReporte, "
   SQL = SQL & SqlSaldoInicial & " AS SaldoInicial, "
   SQL = SQL & " '' AS FechaDocumento, "
   SQL = SQL & " '' AS TipoDeDocumento, "
   SQL = SQL & gUtilSQL.fSimpleSqlValue("Vigente") & " AS NumeroDocumento, "
   SQL = SQL & gUtilSQL.fCast("Anticipo.ConsecutivoAnticipo", eTDSS_VARCHAR, "") & " AS NumeroDocumentoGrupo, "
   SQL = SQL & " 0 AS MontoOriginal, "
   SQL = SQL & " 0 AS SaldoActual, "
   SQL = SQL & gUtilSQL.fSimpleSqlValue("Anticipo Vigente") & " AS TipoDocumentoDetalle, "
   SQL = SQL & " ''  AS NumeroPago, "
   SQL = SQL & " Anticipo.Fecha AS FechaPago, "
   SQL = SQL & " 0 AS MontoPagado, "
   SQL = SQL & " '0' AS StatusPago "
   SQL = SQL & " FROM Anticipo "
   SQL = SQL & " INNER JOIN Proveedor ON (Anticipo.CodigoProveedor = Proveedor.CodigoProveedor)"
   SQL = SQL & " AND (Anticipo.ConsecutivoCompania = Proveedor.ConsecutivoCompania) "
   SQL = SQL & " WHERE " & gUtilSQL.DfSQLDateValueBetween("Anticipo.Fecha", valFechaInicial, valFechaFinal)
   SQL = SQL & " AND Anticipo.Status = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusAnticipo.eSDA_VIGENTE)
   SQL = SQL & " AND Anticipo.EsUnaDevolucion = " & gUtilSQL.fBooleanToSqlValue(False)
   'SQL = SQL & " AND " & sqlStatusPagos & " <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusPago.eSP_ANULADA)
   If ValCmbCantidadAImprimirUnProveedor = gEnumReport.enumCantidadAImprimirToString(eCI_uno) Then
      SQL = SQL & " AND Anticipo.CodigoProveedor = " & gUtilSQL.fSimpleSqlValue(ValtxtCodigoProveedor)
   End If
   SQL = SQL & " AND Anticipo.ConsecutivoCompania = " & valConsecutivoCompania
   SQL = SQL & " ) "


h_EXIT: On Error GoTo 0
   fSQLHistoricoDeProveedorAnticipos = SQL
   Set gEnumProyecto = Nothing
   Set gEnumReport = Nothing
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLHistoricoDeProveedorAnticipos", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLSaldoInicialAnalisisCxPHistoricoAnticipos(ByVal valDescontarAbonos As Boolean, _
                              ByVal valFechaInicial As Date, ByVal valConsecutivoCompania As String, _
                              ByVal ReporteEnMonedaLocal As Boolean, ByVal gUltimaTasaDeCambio As Object, _
                              ByVal gMonedaLocalActual As Object) As String
   Dim SQL As String
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
   SQL = "(SELECT SUM(" & sqlMonto & ") AS SaldoInicial"
   SQL = SQL & " FROM Anticipo AS " & sqlAnticipoTableName
   SQL = SQL & " WHERE " & sqlAnticipoTableName & ".ConsecutivoCompania = " & valConsecutivoCompania
   SQL = SQL & " AND " & sqlAnticipoTableName & ".Tipo = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeAnticipo.eTDA_PAGADO)
   SQL = SQL & " AND " & sqlAnticipoTableName & ".CodigoProveedor = Anticipo.CodigoProveedor"
   SQL = SQL & " AND " & sqlAnticipoTableName & ".Moneda = Anticipo.Moneda"
   SQL = SQL & " AND " & sqlAnticipoTableName & ".Status <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusAnticipo.eSDA_ANULADO)
   SQL = SQL & " AND " & sqlAnticipoTableName & ".Fecha < " & gUtilSQL.fDateToSQLValue(valFechaInicial) & ")"
   fSQLSaldoInicialAnalisisCxPHistoricoAnticipos = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLSaldoInicialAnalisisCxPHistoricoAnticipos", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConstruirSQLDelReporteAnalisisDeVencimiento(ByVal valMonedaOriginal As Boolean, ByVal valPropAnalisisVencActual As Boolean, ByVal gUltimaTasaDeCambio As Object, _
                              ByVal gMonedaLocalActual As Object, ByVal NombreMonedaLocal As String, ByVal valPrimerVencimiento As Long, ByVal valSegundoVencimiento As Long, ByVal valTercerVencimiento As Long, _
                           ByVal ConsecutivoCompania As Long, ByVal EsReporteOrdenadoPorNombre As Boolean) As String
   Dim SQL As String
   Dim sqlDateDiff As String
   Dim sqlMtoRestante As String
   Dim sqlMtoAbonado As String
   Dim sqlMtoTotal As String
   Dim sqlMonedaReporte As String
   On Error GoTo h_ERROR
   SQL = ""
   sqlMtoTotal = "(cxP.MontoExento + " _
                    & "cxP.MontoGravado + " _
                    & "cxP.MontoIva)"
   sqlMtoAbonado = "cxP.MontoAbonado"
   sqlMtoRestante = "(" & sqlMtoTotal & " - " & sqlMtoAbonado & ")"
   sqlMonedaReporte = "cxP.Moneda"
   If valPropAnalisisVencActual Then
      If Not valMonedaOriginal Then
         sqlMtoRestante = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio( _
                     "cxP.CambioABolivares", _
                     "cxP.Moneda", sqlMtoRestante, True, "")
         sqlMtoTotal = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio( _
                     "cxP.CambioABolivares", _
                     "cxP.Moneda", sqlMtoTotal, True, "")
         sqlMonedaReporte = gUtilSQL.fSimpleSqlValue(NombreMonedaLocal)
         sqlMtoTotal = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.fCodigoMoneda(gUtilDate.getFechaDeHoy), sqlMtoTotal, "cxP.fecha")
         sqlMtoAbonado = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.fCodigoMoneda(gUtilDate.getFechaDeHoy), "cxP.MontoAbonado", "cxP.fecha")
         sqlMtoRestante = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.fCodigoMoneda(gUtilDate.getFechaDeHoy), sqlMtoRestante, "cxP.fecha")
      End If
      sqlDateDiff = gUtilSQL.getDateDiff("d", "cxP.FechaVencimiento", _
                     gUtilSQL.fDateToSQLValue(gUtilDate.getFechaDeHoy))
      SQL = "SELECT "
      SQL = SQL & "cxP.CodigoProveedor, "
      SQL = SQL & "proveedor.NombreProveedor, "
      SQL = SQL & "cxP.Moneda, "
      SQL = SQL & sqlMonedaReporte & " AS MonedaReporte, "
      SQL = SQL & gUtilSQL.getIIF(sqlDateDiff & " <= 0 ", sqlMtoRestante, "0", True) & " AS MtoNoVencido, "
      SQL = SQL & gUtilSQL.getIIF(sqlDateDiff & " > 0 AND " & sqlDateDiff & " <= " & _
                  valPrimerVencimiento, sqlMtoRestante, "0", True) & " AS Mto1Vto, "
      SQL = SQL & gUtilSQL.getIIF(sqlDateDiff & " > " & valPrimerVencimiento & " AND " & sqlDateDiff & " <= " & _
                  valSegundoVencimiento, sqlMtoRestante, "0", True) & " AS Mto2Vto, "
      SQL = SQL & gUtilSQL.getIIF(sqlDateDiff & " > " & valSegundoVencimiento & " AND " & sqlDateDiff & " <= " & _
                  valTercerVencimiento, sqlMtoRestante, "0", True) & " AS Mto3Vto, "
      SQL = SQL & gUtilSQL.getIIF(sqlDateDiff & " > " & valTercerVencimiento, sqlMtoRestante, _
                  "0", True) & " AS Mto4Vto, "
      SQL = SQL & valPrimerVencimiento & " AS Vto1, "
      SQL = SQL & valSegundoVencimiento & " AS Vto2, "
      SQL = SQL & valTercerVencimiento & " AS Vto3, "
      SQL = SQL & sqlDateDiff & " AS DiasVencidos, "
      SQL = SQL & sqlMtoRestante & " AS MtoRestante, "
      SQL = SQL & "cxP.FechaVencimiento, "
      SQL = SQL & "cxP.Numero, "
      SQL = SQL & "cxP.Fecha, "
      SQL = SQL & sqlMtoTotal & " AS MtoTotal"
      SQL = SQL & " FROM cxP INNER JOIN proveedor ON "
      SQL = SQL & "cxP.ConsecutivoCompania" & _
                  " = proveedor.ConsecutivoCompania"
      SQL = SQL & " AND cxP.CodigoProveedor" & _
                  " = proveedor.CodigoProveedor"
      SQL = SQL & " WHERE cxP.ConsecutivoCompania" & _
                  " = " & ConsecutivoCompania
      SQL = SQL & " AND cxP.Status" & _
                  " IN (" & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ABONADO) & ", " & _
                  gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_PORCANCELAR) & ")"
      SQL = SQL & " ORDER BY "
      SQL = SQL & "cxP.Moneda, "
      If EsReporteOrdenadoPorNombre Then
         SQL = SQL & "proveedor.NombreProveedor, "
      Else
         SQL = SQL & "cxP.CodigoProveedor, "
      End If
      SQL = SQL & "cxP.FechaVencimiento"
   End If
   fConstruirSQLDelReporteAnalisisDeVencimiento = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fConstruirSQLDelReporteAnalisisDeVencimiento", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function


Public Function fSQLDelAnalisisDeCxPAUnaFecha(ByVal valTableName As String, ByVal valnombreParaTabla As String, ByVal valFecha As Date, ByVal valOrdenadoPorNombre As Boolean, ByVal valMonedaOriginal As Boolean, _
                              ByVal valPropAnalisisVencActual As Boolean, ByVal gUltimaTasaDeCambio As Object, ByVal gMonedaLocalActual As Object, ByVal valPrimerVencimiento As Long, _
                           ByVal valSegundoVencimiento As Long, ByVal valTercerVencimiento As Long, ByVal ConsecutivoCompania As Long) As String
   Dim SQL As String
   Dim sqlDateDiff As String
   Dim sqlMtoRestante As String
   Dim sqlMtoTotal As String
   Dim tasaOrig As Boolean
   Dim sqlMtoAbonado As String
   On Error GoTo h_ERROR
   If valPropAnalisisVencActual Then
      sqlMtoTotal = "(" & valTableName & ".MontoExento + " & valTableName & ".MontoGravado" & _
                        " + " & valTableName & ".MontoIva)"
      sqlMtoAbonado = "(" & valTableName & ".MontoAbonado)"
      sqlMtoRestante = "(" & sqlMtoTotal & " - " & sqlMtoAbonado & ")"
      sqlDateDiff = gUtilSQL.getDateDiff("d", valTableName & ".FechaVencimiento", gUtilSQL.fDateToSQLValue(valFecha))
      If Not valMonedaOriginal Then
        sqlMtoRestante = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(valTableName & ".CambioABolivares", _
                     valTableName & ".Moneda", sqlMtoRestante, True, "")
        sqlMtoTotal = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(valTableName & ".CambioABolivares", _
                     valTableName & ".Moneda", sqlMtoTotal, True, "")
        sqlMtoTotal = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.fCodigoMoneda(gUtilDate.getFechaDeHoy), sqlMtoTotal, valTableName & ".fecha")
        sqlMtoAbonado = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.fCodigoMoneda(gUtilDate.getFechaDeHoy), sqlMtoAbonado, valTableName & ".fecha")
        sqlMtoRestante = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.fCodigoMoneda(gUtilDate.getFechaDeHoy), sqlMtoRestante, valTableName & ".fecha")
      End If
      SQL = "SELECT "
      SQL = SQL & valTableName & ".CodigoCliProv AS CodigoProveedor, "
      SQL = SQL & "proveedor.NombreProveedor, "
      SQL = SQL & valTableName & ".Moneda, "
      SQL = SQL & gUtilSQL.getIIF(sqlDateDiff & " <= 0 ", sqlMtoRestante, "0", True) & _
                  " AS MtoNoVencido, "
      SQL = SQL & gUtilSQL.getIIF(sqlDateDiff & " > 0 AND " & _
                  sqlDateDiff & " <= " & valPrimerVencimiento, sqlMtoRestante, "0", True) & _
                  " AS Mto1Vto, "
      SQL = SQL & gUtilSQL.getIIF(sqlDateDiff & " > " & valPrimerVencimiento & " AND " & _
                  sqlDateDiff & " <= " & valSegundoVencimiento, sqlMtoRestante, "0", True) & _
                  " AS Mto2Vto, "
      SQL = SQL & gUtilSQL.getIIF(sqlDateDiff & " > " & valSegundoVencimiento & " AND " & _
                  sqlDateDiff & " <= " & valTercerVencimiento, sqlMtoRestante, "0", True) & _
                  " AS Mto3Vto, "
      SQL = SQL & gUtilSQL.getIIF(sqlDateDiff & " > " & valTercerVencimiento, _
                  sqlMtoRestante, "0", True) & _
                  " AS Mto4Vto, "
      SQL = SQL & valPrimerVencimiento & " AS Vto1, "
      SQL = SQL & valSegundoVencimiento & " AS Vto2, "
      SQL = SQL & valTercerVencimiento & " AS Vto3, "
      SQL = SQL & sqlDateDiff & " AS DiasVencidos, "
      SQL = SQL & sqlMtoRestante & " AS MtoRestante, "
      SQL = SQL & valTableName & ".FechaVencimiento, "
      SQL = SQL & valTableName & ".Numero, "
      SQL = SQL & valTableName & ".Fecha, "
      SQL = SQL & sqlMtoTotal & " AS MtoTotal"
      SQL = SQL & " FROM " & valTableName & " INNER JOIN "
      SQL = SQL & "proveedor ON "
      SQL = SQL & valTableName & ".ConsecutivoCompania" & _
                  " = proveedor.ConsecutivoCompania"
      SQL = SQL & " AND " & valTableName & ".CodigoCliProv" & _
                  " = proveedor.CodigoProveedor "
      SQL = SQL & " WHERE " & valTableName & ".ConsecutivoCompania" & _
                  " = " & ConsecutivoCompania
      SQL = SQL & " AND " & valTableName & ".Status" & _
                  " IN (" & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ABONADO) & ", " & _
                            gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_PORCANCELAR) & ")"
      SQL = SQL & " AND " & valTableName & ".Idusuario" & _
                  " = " & gUtilSQL.fSimpleSqlValue(valnombreParaTabla)
      SQL = SQL & " AND " & valTableName & ".Idmodulo = " & gUtilSQL.fSimpleSqlValue(CM_MODULO_CxP)
      SQL = SQL & " AND " & valTableName & ".Fecha <= " & gUtilSQL.fDateToSQLValue(valFecha)
'      Sql = Sql & " AND " & valTableName & ".Fecha >= " & gUtilSQL.fDateToSQLValue(valFecha)
      SQL = SQL & " ORDER BY "
      SQL = SQL & valTableName & ".Moneda, "
      If valOrdenadoPorNombre Then
         SQL = SQL & "proveedor.NombreProveedor, "
      Else
         SQL = SQL & valTableName & ".CodigoCliProv, "
      End If
      SQL = SQL & valTableName & ".FechaVencimiento"
   End If
h_EXIT: On Error GoTo 0
   fSQLDelAnalisisDeCxPAUnaFecha = SQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLDelAnalisisDeCxPAUnaFecha", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLCxPEntreFechasCxPPendientesEntreFechas(ByVal valMonedaOriginal As Boolean, _
                                                            ByVal usaCambioOriginal As Boolean, _
                                                             ByVal valElementInComboBoxToString As String, _
                                                              ByVal valFechaInicial As Date, _
                                                               ByVal valFechaFinal As Date, _
                                                                ByVal valInformeSeleccionado As Integer, _
                                                                 ByVal valConsecutivoCompania As String, _
                                                                  ByVal gMonedaLocalActual As Object, _
                                                                   ByVal gUltimaTasaDeCambio As Object, _
                                                                    ByVal valImprimeUno As Boolean, _
                                                                     ByVal ValCodigoProveedor As String) As String
   Dim SQL As String
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
   SQL = "SELECT "
   SQL = SQL & "CxP.Numero, "
   SQL = SQL & "CxP.CodigoProveedor, "
   SQL = SQL & "Proveedor.NombreProveedor, "
   SQL = SQL & "CxP.Fecha, "
   SQL = SQL & "CxP.FechaVencimiento, "
   SQL = SQL & sqlStatusCxP & " AS StatusStr, "
   SQL = SQL & sqlMtoTotal & " AS MontoTotal, "
   SQL = SQL & sqlMtoRestante & " AS MontoRestante, "
   SQL = SQL & sqlMonedaReporte & " AS MonedaReporte, "
   SQL = SQL & "CxP.Moneda AS MonedaDocumento, "
   SQL = SQL & sqlCambio & " AS Cambio"
   SQL = SQL & " FROM CxP INNER JOIN Proveedor ON "
   SQL = SQL & "CxP.CodigoProveedor = Proveedor.CodigoProveedor"
   SQL = SQL & " AND CxP.ConsecutivoCompania = Proveedor.ConsecutivoCompania"
   SQL = SQL & " WHERE " & gUtilSQL.DfSQLDateValueBetween("CxP.Fecha", valFechaInicial, valFechaFinal)
   If valImprimeUno Then
      SQL = SQL & " AND CxP.CodigoProveedor = " & ValCodigoProveedor
   End If
   If unSoloStatus Then
      SQL = SQL & " AND CxP.Status = " & statusABuscarChr
   End If
   SQL = SQL & " AND CxP.ConsecutivoCompania = " & valConsecutivoCompania
   SQL = SQL & " ORDER BY "
   SQL = SQL & "CxP.Status, "
   SQL = SQL & "MonedaReporte, "
   SQL = SQL & "CxP.Fecha, "
   SQL = SQL & "CxP.Numero, "
   SQL = SQL & "CxP.CodigoProveedor"
   Set gEnumProyecto = Nothing
   fSQLCxPEntreFechasCxPPendientesEntreFechas = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLCxPEntreFechasCxPPendientesEntreFechas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLAnalisisCxPHistorico(ByVal valUsaCambioOriginal As Boolean, ByVal gUltimaTasaDeCambio As Object, ByVal gMonedaLocalActual As Object, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, _
                                 ByVal valConsecutivoCompania As String, ByVal valReporteEnMonedaLocal As Boolean, ByVal valImprimeUno As Boolean, ByVal ValCodigoProveedor As String, _
                                 ByVal valTipoDeAnticipo As String, ByVal valDocOrigenContabilizacionCxP As String, ByVal valDocOrigenContabilizacionAnticipo As String) As String
   Dim SQL As String
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
   SQL = SQLCXP
   SQL = SQL & " UNION " & SqlAnticipos
   SQL = SQL & " UNION " & SqlPagos
   SQL = SQL & " UNION " & SqlReposiciones
   SQL = SQL & sqlOrderBy
   fSQLAnalisisCxPHistorico = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLAnalisisCxPHistorico", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLAnalisisCxPHistoricoCxP(ByVal valUsaCambioOriginal As Boolean, ByVal gUltimaTasaDeCambio As Object, ByVal gMonedaLocalActual As Object, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, _
                                       ByVal valConsecutivoCompania As String, ByVal valReporteEnMonedaLocal As Boolean, ByVal valDocOrigenContabilizacion As String, ByVal valImprimeUno As Boolean, ByVal ValCodigoProveedor As String) As String
   Dim SQL As String
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
   SQL = "SELECT "
   SQL = SQL & gUtilSQL.fSimpleSqlValue("0") & " AS ColumnaParaGrupoTipoReporte, "
   SQL = SQL & gUtilSQL.fSimpleSqlValue("Cuentas por Pagar") & " AS TituloDelGrupo, "
   SQL = SQL & "cxP.CodigoProveedor AS Codigo, "
   SQL = SQL & sqlMoneda & " AS Moneda, "
   SQL = SQL & sqlMonedaReporte & " AS MonedaReporte, "
   SQL = SQL & sqlTipoDeCxP & " AS TipoDocumento, "
   SQL = SQL & gUtilSQL.fCast(valDocOrigenContabilizacion, eTDSS_VARCHAR, "") & " AS ColumnaParaGrupoDetalle, "
   SQL = SQL & SqlSaldoInicialSinAbonos & " AS SaldoInicialSinAbonos, "
   SQL = SQL & SqlSaldoInicialConAbonos & " AS SaldoInicialConAbonos, "
   SQL = SQL & "cxP.Numero AS Numero, "
   SQL = SQL & gUtilSQL.DfSQLFormat("cxP.fecha", "dd/MM/yyyy", "") & " AS Fecha, "
   SQL = SQL & gUtilSQL.DfSQLFormat("cxP.FechaVencimiento", "dd/MM/yyyy", "") & " AS FechaVencimiento, "
   SQL = SQL & sqlStatusCxP & " AS Status, "
   SQL = SQL & sqlMontoActualCxP & " AS MontoActual, "
   SQL = SQL & sqlMontoTotalCxP & " AS MontoTotal, "
   SQL = SQL & "proveedor.NombreProveedor AS Nombre, "
   SQL = SQL & sqlCambio & " AS CambioBs, "
   SQL = SQL & gUtilSQL.fSimpleSqlValue("") & " AS TipoDocPagado, "
   SQL = SQL & gUtilSQL.fSimpleSqlValue("") & " AS NumeroDocPagado, "
   SQL = SQL & gUtilSQL.getIIF("Cxp.EstaAsociadoARendicion = 'S'", "cxP.MontoAbonado", "0") & " AS MontoAbonado "
   SQL = SQL & " FROM cxP INNER JOIN proveedor ON "
   SQL = SQL & "cxP.ConsecutivoCompania = proveedor.ConsecutivoCompania"
   SQL = SQL & " AND cxP.CodigoProveedor = proveedor.CodigoProveedor"
   SQL = SQL & " WHERE cxP.ConsecutivoCompania = " & valConsecutivoCompania
   If valImprimeUno Then
      SQL = SQL & " AND cxP.CodigoProveedor = " & ValCodigoProveedor
   End If
   SQL = SQL & " AND " & gUtilSQL.DfSQLDateValueBetween("cxP.Fecha", valFechaInicial, valFechaFinal)
h_EXIT: On Error GoTo 0
   fSQLAnalisisCxPHistoricoCxP = SQL
   Set gEnumProyecto = Nothing
   Set gEnumReport = Nothing
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLAnalisisCxPHistoricoCxP", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLAnalisisCxPHistoricoAnticipos(ByVal valTipoDeAnticipo As String, ByVal valUsaCambioOriginal As Boolean, ByVal gUltimaTasaDeCambio As Object, ByVal gMonedaLocalActual As Object, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, _
                                       ByVal valConsecutivoCompania As String, ByVal valReporteEnMonedaLocal As Boolean, ByVal valDocOrigenContabilizacion As String, ByVal valImprimeUno As Boolean, ByVal ValCodigoProveedor As String) As String
   Dim SQL As String
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
   SQL = "SELECT "
   SQL = SQL & gUtilSQL.fSimpleSqlValue("1") & " AS ColumnaParaGrupoTipoReporte, "
   SQL = SQL & gUtilSQL.fSimpleSqlValue("Anticipos") & " AS TituloDelGrupo, "
   SQL = SQL & "anticipo.CodigoProveedor AS Codigo, "
   SQL = SQL & sqlMoneda & " AS Moneda, "
   SQL = SQL & sqlMonedaReporte & " AS MonedaReporte, "
   SQL = SQL & sqlTipoDeAnticipo & " AS TipoDocumento, "
   SQL = SQL & gUtilSQL.fCast(valDocOrigenContabilizacion, eTDSS_VARCHAR, "") & " AS ColumnaParaGrupoDetalle, "
   SQL = SQL & SqlSaldoInicialSinAbonos & " AS SaldoInicialSinAbonos, "
   SQL = SQL & SqlSaldoInicialConAbonos & " AS SaldoInicialConAbonos, "
   SQL = SQL & "anticipo.Numero AS Numero, "
   SQL = SQL & gUtilSQL.DfCDateSQL(gUtilSQL.DfSQLFormat("anticipo.fecha", "dd/MM/yyyy", "")) & " AS Fecha, "
   SQL = SQL & gUtilSQL.fSimpleSqlValue("") & " AS FechaVencimiento, "
   SQL = SQL & sqlStatusDeAnticipo & " AS Status, "
   SQL = SQL & sqlMontoActual & " AS MontoActual, "
   SQL = SQL & sqlMontoTotal & " AS MontoTotal, "
   SQL = SQL & "proveedor.NombreProveedor AS Nombre, "
   SQL = SQL & sqlCambio & " AS CambioBs, "
   SQL = SQL & gUtilSQL.fSimpleSqlValue("") & " AS TipoDocPagado, "
   SQL = SQL & gUtilSQL.fSimpleSqlValue("") & " AS NumeroDocPagado, "
   SQL = SQL & "0 AS MontoAbonado "
   SQL = SQL & " FROM anticipo INNER JOIN proveedor ON "
   SQL = SQL & "anticipo.ConsecutivoCompania = proveedor.ConsecutivoCompania"
   SQL = SQL & " AND anticipo.CodigoProveedor = proveedor.CodigoProveedor"
   SQL = SQL & " WHERE anticipo.ConsecutivoCompania = " & valConsecutivoCompania
   If valImprimeUno Then
      SQL = SQL & " AND anticipo.CodigoProveedor = " & ValCodigoProveedor
   End If
   SQL = SQL & " AND " & gUtilSQL.DfSQLDateValueBetween("anticipo.Fecha", valFechaInicial, valFechaFinal)
   SQL = SQL & " AND anticipo.Tipo = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeAnticipo.eTDA_PAGADO)
   SQL = SQL & " AND anticipo.EsUnaDevolucion = " & gUtilSQL.fBooleanToSqlValue(False)
h_EXIT: On Error GoTo 0
   fSQLAnalisisCxPHistoricoAnticipos = SQL
   Set gEnumProyecto = Nothing
   Set gEnumReport = Nothing
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLAnalisisCxPHistoricoAnticipos", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
   
Private Function fSQLAnalisisCxPHistoricoPagos(ByVal valUsaCambioOriginal As Boolean, ByVal gUltimaTasaDeCambio As Object, ByVal gMonedaLocalActual As Object, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, _
                                       ByVal valConsecutivoCompania As String, ByVal valReporteEnMonedaLocal As Boolean, ByVal valImprimeUno As Boolean, ByVal ValCodigoProveedor As String) As String

   Dim SQL As String
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
   SQL = "SELECT "
   SQL = SQL & gUtilSQL.fSimpleSqlValue("2") & " AS ColumnaParaGrupoTipoReporte, "
   SQL = SQL & gUtilSQL.fSimpleSqlValue("Pagos") & " AS TituloDelGrupo, "
   SQL = SQL & "pago.CodigoProveedor AS Codigo, "
   SQL = SQL & sqlMonedaDocs & " AS Moneda, "
   SQL = SQL & sqlMonedaReporte & " AS MonedaReporte, "
   SQL = SQL & gUtilSQL.fSimpleSqlValue("Pagos") & " AS TipoDocumento, "
   SQL = SQL & sqlColumnaParaGrupoDetalle & " AS ColumnaParaGrupoDetalle, "
   SQL = SQL & SqlSaldoInicialSinAbonos & " AS SaldoInicialSinAbonos, "
   SQL = SQL & SqlSaldoInicialConAbonos & " AS SaldoInicialConAbonos, "
   SQL = SQL & "pago.NumeroCheque AS Numero, "
   SQL = SQL & gUtilSQL.DfCDateSQL(gUtilSQL.DfSQLFormat("pago.fecha", "dd/MM/yyyy", "")) & " AS Fecha, "
   SQL = SQL & gUtilSQL.fSimpleSqlValue("") & " AS FechaVencimiento, "
   SQL = SQL & sqlStatusPago & " AS Status, "
   SQL = SQL & sqlMontoPago & " AS MontoActual, "
   SQL = SQL & sqlMontoCheque & " AS MontoTotal, "
   SQL = SQL & "proveedor.NombreProveedor AS Nombre, "
   SQL = SQL & sqlCambio & " AS CambioBs, "
   SQL = SQL & sqlTipoDeDocPagado & " AS TipoDocCobrado, "
   SQL = SQL & sqlNumeroDocumentoPagado & " AS NumeroDocCobrado, "
   SQL = SQL & sqlMontoAbonado & " AS MontoAbonado "
   SQL = SQL & " FROM proveedor INNER JOIN (("
   SQL = SQL & "cuentaBancaria INNER JOIN (pago"
   SQL = SQL & " INNER JOIN documentoPagado ON ("
   SQL = SQL & "pago.NumeroComprobante = documentoPagado.NumeroComprobante"
   SQL = SQL & ") AND (pago.ConsecutivoCompania = documentoPagado.ConsecutivoCompania"
   SQL = SQL & ")) ON (cuentaBancaria.Codigo = pago.CodigoCuentaBancaria"
   SQL = SQL & ") AND (cuentaBancaria.ConsecutivoCompania = pago.ConsecutivoCompania"
   SQL = SQL & ")) INNER JOIN cxP ON ("
   SQL = SQL & "cxP.Numero = documentoPagado.NumeroDelDocumentoPagado"
   SQL = SQL & ") AND (documentoPagado.ConsecutivoCompania = cxP.ConsecutivoCompania"
   SQL = SQL & ")) ON (proveedor.CodigoProveedor = pago.CodigoProveedor"
   SQL = SQL & ") AND (proveedor.ConsecutivoCompania = pago.ConsecutivoCompania"
   SQL = SQL & ") AND (proveedor.CodigoProveedor = cxP.CodigoProveedor"
   SQL = SQL & ") AND (proveedor.ConsecutivoCompania = cxP.ConsecutivoCompania)"
   SQL = SQL & "   INNER JOIN Moneda ON DocumentoPagado.CodigoMonedaDeCxP = Moneda.Codigo"
   SQL = SQL & " INNER JOIN IGV_DocPagadosPorMoneda  On"
   SQL = SQL & " (Pago.ConsecutivoCompania = IGV_DocPagadosPorMoneda.ConsecutivoCompania)"
   SQL = SQL & " AND (Pago.NumeroComprobante = IGV_DocPagadosPorMoneda.NumeroPago) "
   SQL = SQL & " AND (Moneda.Codigo = IGV_DocPagadosPorMoneda.CodigoMonedaDeCxP) "
   SQL = SQL & " WHERE pago.ConsecutivoCompania = " & valConsecutivoCompania
   If valImprimeUno Then
      SQL = SQL & " AND pago.CodigoProveedor = " & ValCodigoProveedor
   End If
   SQL = SQL & " AND " & gUtilSQL.DfSQLDateValueBetween("pago.Fecha", valFechaInicial, valFechaFinal)
   sqlTipoDeDocPagado = gUtilSQL.getIIF(sqlStatusPagoAnulado, gUtilSQL.fSimpleSqlValue(""), gUtilSQL.fSimpleSqlValue("Anticipo"), True)
   sqlNumeroDocumentoPagado = gUtilSQL.getIIF(sqlStatusPagoAnulado, gUtilSQL.fSimpleSqlValue(""), "anticipoPagado.NumeroAnticipo", True)
   sqlMontoAbonado = gUtilSQL.getIIF(sqlStatusPagoAnulado, "0", "anticipoPagado.MontoAplicado", True)
   sqlColumnaParaGrupoDetalle = gUtilSQL.fCast("pago.NumeroComprobante", eTDSS_VARCHAR, "") & gUtilSQL.CharConcat & gUtilSQL.fSimpleSqlValue(" ") & gUtilSQL.CharConcat & gUtilSQL.fCast(gUtilSQL.fSimpleSqlValue("1"), eTDSS_VARCHAR, "")
   SQL = SQL & " UNION "
   SQL = SQL & "SELECT "
   SQL = SQL & gUtilSQL.fSimpleSqlValue("2") & " AS ColumnaParaGrupoTipoReporte, "
   SQL = SQL & gUtilSQL.fSimpleSqlValue("Pagos") & " AS TituloDelGrupo, "
   SQL = SQL & "pago.CodigoProveedor AS Codigo, "
   SQL = SQL & sqlMonedaPago & " AS Moneda, "
   SQL = SQL & sqlMonedaReporte & " AS MonedaReporte, "
   SQL = SQL & gUtilSQL.fSimpleSqlValue("Pago") & " AS TipoDocumento, "
   SQL = SQL & sqlColumnaParaGrupoDetalle & " AS ColumnaParaGrupoDetalle, "
   SQL = SQL & SqlSaldoInicialSinAbonos & " AS SaldoInicialSinAbonos, "
   SQL = SQL & SqlSaldoInicialConAbonos & " AS SaldoInicialConAbonos, "
   SQL = SQL & "pago.NumeroCheque AS Numero, "
   SQL = SQL & gUtilSQL.DfCDateSQL(gUtilSQL.DfSQLFormat("pago.fecha", "dd/MM/yyyy", "")) & " AS Fecha, "
   SQL = SQL & gUtilSQL.fSimpleSqlValue("") & " AS FechaVencimiento, "
   SQL = SQL & sqlStatusPago & " AS Status, "
   SQL = SQL & sqlMontoCheque & " AS MontoActual, "
   SQL = SQL & sqlMontoPago & " AS MontoTotal, "
   SQL = SQL & "proveedor.NombreProveedor AS Nombre, "
   SQL = SQL & sqlCambio & " AS CambioBs, "
   SQL = SQL & sqlTipoDeDocPagado & " AS TipoDocCobrado, "
   SQL = SQL & sqlNumeroDocumentoPagado & " AS NumeroDocCobrado, "
   SQL = SQL & sqlMontoAbonado & " AS MontoAbonado "
   SQL = SQL & " FROM anticipo INNER JOIN (("
   SQL = SQL & "proveedor INNER JOIN ("
   SQL = SQL & "cuentaBancaria INNER JOIN pago ON ("
   SQL = SQL & "cuentaBancaria.Codigo = pago.CodigoCuentaBancaria"
   SQL = SQL & ") AND (cuentaBancaria.ConsecutivoCompania = pago.ConsecutivoCompania"
   SQL = SQL & ")) ON (proveedor.CodigoProveedor = pago.CodigoProveedor"
   SQL = SQL & ") AND (proveedor.ConsecutivoCompania = pago.ConsecutivoCompania"
   SQL = SQL & ")) INNER JOIN anticipoPagado ON ("
   SQL = SQL & "pago.NumeroComprobante = anticipoPagado.NumeroComprobante"
   SQL = SQL & ") AND (pago.ConsecutivoCompania = anticipoPagado.ConsecutivoCompania"
   SQL = SQL & ")) ON (anticipoPagado.ConsecutivoAnticipoUsado = anticipo.ConsecutivoAnticipo"
   SQL = SQL & ") AND (anticipoPagado.ConsecutivoCompania = anticipo.ConsecutivoCompania"
   SQL = SQL & ") AND (anticipo.CodigoProveedor = proveedor.CodigoProveedor"
   SQL = SQL & ") AND (anticipo.ConsecutivoCompania = proveedor.ConsecutivoCompania)"
   SQL = SQL & "   INNER JOIN Moneda ON AnticipoPagado.CodigoMoneda = Moneda.Codigo"
   SQL = SQL & " INNER JOIN IGV_DocPagadosPorMoneda  On"
   SQL = SQL & " (Pago.ConsecutivoCompania = IGV_DocPagadosPorMoneda.ConsecutivoCompania)"
   SQL = SQL & " AND (Pago.NumeroComprobante = IGV_DocPagadosPorMoneda.NumeroPago) "
   SQL = SQL & " AND (Moneda.Codigo = IGV_DocPagadosPorMoneda.CodigoMonedaDeCxP) "
   SQL = SQL & " WHERE pago.ConsecutivoCompania = " & valConsecutivoCompania
   If valImprimeUno Then
      SQL = SQL & " AND pago.CodigoProveedor = " & ValCodigoProveedor
   End If
   SQL = SQL & " AND " & gUtilSQL.DfSQLDateValueBetween("pago.Fecha", valFechaInicial, valFechaFinal)
   SQL = SQL & " AND anticipo.Tipo = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeAnticipo.eTDA_PAGADO)
h_EXIT: On Error GoTo 0
   fSQLAnalisisCxPHistoricoPagos = SQL
   Set gEnumProyecto = Nothing
   Set gEnumReport = Nothing
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLAnalisisCxPHistoricoPagos", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLSaldoInicialAnalisisCxPHistoricoPagos(ByVal valUsaCambioOriginal As Boolean, ByVal gUltimaTasaDeCambio As Object, ByVal gMonedaLocalActual As Object, ByVal valConsecutivoCompania As String, _
                                                   ByVal valFechaInicial As Date, ByVal valReporteEnMonedaLocal As Boolean) As String
   Dim SQL  As String
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
   SQL = "(SELECT SUM(" & sqlMonto & ") AS SaldoInicial"
   SQL = SQL & " FROM pago AS " & sqlPagoTableName
   SQL = SQL & " WHERE " & sqlPagoTableName & ".ConsecutivoCompania = " & valConsecutivoCompania
   SQL = SQL & " AND " & sqlPagoTableName & ".CodigoProveedor = pago.CodigoProveedor"
   SQL = SQL & " AND " & sqlPagoTableName & ".Moneda = pago.Moneda"
   SQL = SQL & " AND StatusOrdenDePago = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusPago.eSP_VIGENTE)
   SQL = SQL & " AND Fecha < " & gUtilSQL.fDateToSQLValue(valFechaInicial) & ")"
   fSQLSaldoInicialAnalisisCxPHistoricoPagos = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLSaldoInicialAnalisisCxPHistoricoPagos", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLAnalisisCxPHistoricoReposiciones(ByVal valUsaCambioOriginal As Boolean, ByVal gUltimaTasaDeCambio As Object, ByVal gMonedaLocalActual As Object, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, _
                                       ByVal valConsecutivoCompania As String, ByVal valReporteEnMonedaLocal As Boolean, ByVal valImprimeUno As Boolean, ByVal ValCodigoProveedor As String) As String
   Dim SQL As String
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
   SQL = "SELECT "
   SQL = SQL & gUtilSQL.fSimpleSqlValue("3") & " AS ColumnaParaGrupoTipoReporte, "
   SQL = SQL & gUtilSQL.fSimpleSqlValue("Reposiciones de Caja Chica") & " AS TituloDelGrupo, "
   SQL = SQL & "Adm.DetalleDeRendicion.CodigoProveedor  AS Codigo, "
   SQL = SQL & sqlMonedaDocs & " AS Moneda, "
   SQL = SQL & sqlMonedaReporte & " AS MonedaReporte, "
   SQL = SQL & gUtilSQL.fSimpleSqlValue("Reposición") & " AS TipoDocumento, "
   SQL = SQL & sqlColumnaParaGrupoDetalle & " AS ColumnaParaGrupoDetalle, "
   SQL = SQL & SqlSaldoInicialSinAbonos & " AS SaldoInicialSinAbonos, "
   SQL = SQL & SqlSaldoInicialConAbonos & " AS SaldoInicialConAbonos, "
   SQL = SQL & "Adm.Rendicion.Numero AS Numero, "
   SQL = SQL & gUtilSQL.DfCDateSQL(gUtilSQL.DfSQLFormat("Adm.Rendicion.FechaCierre", "dd/MM/yyyy", "")) & " AS Fecha, "
   SQL = SQL & gUtilSQL.fSimpleSqlValue("") & " AS FechaVencimiento, "
   SQL = SQL & sqlStatusReposicion & " AS Status, "
   SQL = SQL & sqlMontoReposicion & " AS MontoActual, "
   SQL = SQL & sqlMontoCheque & " AS MontoTotal, "
   SQL = SQL & "proveedor.NombreProveedor AS Nombre, "
   SQL = SQL & sqlCambio & " AS CambioBs, "
   SQL = SQL & sqlTipoDeDocPagado & " AS TipoDocCobrado, "
   SQL = SQL & sqlNumeroDocumentoPagado & " AS NumeroDocCobrado, "
   SQL = SQL & sqlMontoAbonado & " AS MontoAbonado "
   SQL = SQL & " FROM  Adm.DetalleDeRendicion INNER JOIN "
   SQL = SQL & "Adm.Rendicion On (Adm.DetalleDeRendicion.ConsecutivoCompania=Adm.Rendicion.ConsecutivoCompania and Adm.DetalleDeRendicion.ConsecutivoRendicion=Adm.Rendicion.Consecutivo )"
   SQL = SQL & " INNER JOIN proveedor ON ("
   SQL = SQL & "proveedor.CodigoProveedor = Adm.DetalleDeRendicion.CodigoProveedor "
   SQL = SQL & ") AND (proveedor.ConsecutivoCompania = Adm.DetalleDeRendicion.ConsecutivoCompania"
   SQL = SQL & ") INNER JOIN  CuentaBancaria ON (cuentaBancaria.Codigo = Adm.Rendicion.CodigoCtaBancariaCajaChica "
   SQL = SQL & ") AND (cuentaBancaria.ConsecutivoCompania = Adm.Rendicion.ConsecutivoCompania"
   SQL = SQL & ") INNER JOIN Moneda ON cuentaBancaria.CodigoMoneda = Moneda.Codigo"
   SQL = SQL & " WHERE Adm.DetalleDeRendicion .ConsecutivoCompania = " & valConsecutivoCompania
   If valImprimeUno Then
      SQL = SQL & " AND Adm.DetalleDeRendicion.CodigoProveedor = " & ValCodigoProveedor
   End If
   SQL = SQL & " AND " & gUtilSQL.DfSQLDateValueBetween("Adm.DetalleDeRendicion.Fecha", valFechaInicial, valFechaFinal)
   SQL = SQL & " AND " & "Adm.Rendicion.StatusRendicion = " & gUtilSQL.fSimpleSqlValue(enum_StatusRendicion.eSRd_CERRADA)
h_EXIT: On Error GoTo 0
   fSQLAnalisisCxPHistoricoReposiciones = SQL
   Set gEnumProyecto = Nothing
   Set gEnumReport = Nothing
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLAnalisisCxPHistoricoReposiciones", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Private Function fSQLSaldoInicialAnalisisCxPHistoricoReposicion(ByVal valUsaCambioOriginal As Boolean, ByVal gUltimaTasaDeCambio As Object, ByVal gMonedaLocalActual As Object, ByVal valConsecutivoCompania As String, _
                                                   ByVal valFechaInicial As Date, ByVal valReporteEnMonedaLocal As Boolean) As String
   Dim SQL  As String
   Dim sqlTableName As String
   Dim sqlMonto As String
   On Error GoTo h_ERROR
   sqlTableName = "Rendicion_1"
   sqlMonto = "Adm.DetalleDeRendicion.MontoExento + Adm.DetalleDeRendicion.MontoGravable + Adm.DetalleDeRendicion.MontoIVA"
   sqlMonto = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.fCodigoMoneda(gUtilDate.getFechaDeHoy), sqlMonto, sqlTableName & ".fechaCierre")
   SQL = "(SELECT SUM(" & sqlMonto & ")"
   SQL = SQL & " FROM Adm.Rendicion AS " & sqlTableName
   SQL = SQL & " INNER JOIN Adm.DetalleDeRendicion On (Adm.DetalleDeRendicion.ConsecutivoCompania=Rendicion_1.ConsecutivoCompania and Adm.DetalleDeRendicion.ConsecutivoRendicion=Rendicion_1.Consecutivo )"
   SQL = SQL & " WHERE " & sqlTableName & ".ConsecutivoCompania = " & valConsecutivoCompania
   SQL = SQL & " AND " & "Adm.DetalleDeRendicion" & ".CodigoProveedor = Proveedor.CodigoProveedor"
   SQL = SQL & " AND Rendicion_1.StatusRendicion = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusRendicion.eSRd_CERRADA)
   SQL = SQL & " AND Rendicion_1.TipoDeDocumento = 1 "
   SQL = SQL & " AND Rendicion_1.FechaCierre < " & gUtilSQL.fDateToSQLValue(valFechaInicial) & ") "
   fSQLSaldoInicialAnalisisCxPHistoricoReposicion = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLSaldoInicialAnalisisCxPHistoricoReposicion", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function fSQLActualizaAsientoComprobantesDeCxp() As String
   Dim SQL  As String
   On Error GoTo h_ERROR
   SQL = "Update Asiento"
   SQL = SQL & " SET ASIENTO.MontoDebe=(CASE ASIENTO.MontoDebe WHEN 0 THEN 0 ELSE ASIENTO.MontoDebe/1000 END),"
   SQL = SQL & "ASIENTO.MontoHaber= (CASE ASIENTO.MontoHaber WHEN 0 THEN 0 ELSE ASIENTO.MontoHaber/1000 END)"
   SQL = SQL & " FROM COMPROBANTE INNER JOIN cxP"
   SQL = SQL & " ON ((COMPROBANTE.NoDocumentoOrigen = cxP.CodigoProveedor + CHAR(9) + cxP.Numero)"
   SQL = SQL & " AND COMPROBANTE.GeneradoPor = ';'"
   SQL = SQL & " AND cxP.Fecha<'01/01/2008' AND cxP.AnoDeAplicacion >= '2008')"
   SQL = SQL & " AND (cxP.Fecha<'01/01/2008' AND COMPROBANTE.Fecha >= '01/01/2008')"
   SQL = SQL & " AND (COMPROBANTE.TotalDebe = CxP.MontoExento + MontoGravado + MontoIva)"
   SQL = SQL & " INNER JOIN Asiento ON(Asiento.NumeroComprobante=COMPROBANTE.Numero)"
   SQL = SQL & " INNER JOIN PERIODO ON"
   SQL = SQL & " (Cxp.ConsecutivoCompania = PERIODO.consecutivoCompania)"
   SQL = SQL & " AND (PERIODO.ConsecutivoPeriodo = COMPROBANTE.ConsecutivoPeriodo)"
   SQL = SQL & " AND (PERIODO.ConsecutivoPeriodo = ASIENTO.ConsecutivoPeriodo)"
   fSQLActualizaAsientoComprobantesDeCxp = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLActualizaAsientoComprobantesDeCxp", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLActualizaComprobantesDeCxp() As String
   Dim SQL  As String
   On Error GoTo h_ERROR
   SQL = "Update COMPROBANTE"
   SQL = SQL & " SET COMPROBANTE.TotalDebe= (CASE COMPROBANTE.TotalDebe WHEN 0 THEN 0 ELSE COMPROBANTE.TotalDebe/1000 END),"
   SQL = SQL & " COMPROBANTE.TotalHaber= (CASE COMPROBANTE.TotalHaber WHEN 0 THEN 0 ELSE COMPROBANTE.TotalHaber/1000 END)"
   SQL = SQL & " FROM COMPROBANTE INNER JOIN cxP"
   SQL = SQL & " ON (COMPROBANTE.NoDocumentoOrigen = cxP.CodigoProveedor + CHAR(9) + cxP.Numero)"
   SQL = SQL & " AND COMPROBANTE.GeneradoPor = ';'"
   SQL = SQL & " AND (cxP.Fecha<'01/01/2008' AND cxP.AnoDeAplicacion >= '2008')"
   SQL = SQL & " AND (cxP.Fecha<'01/01/2008' AND COMPROBANTE.Fecha >= '01/01/2008')"
   SQL = SQL & " AND (COMPROBANTE.TotalDebe = CxP.MontoExento + MontoGravado + MontoIva)"
   SQL = SQL & " INNER JOIN PERIODO ON"
   SQL = SQL & " (Cxp.ConsecutivoCompania = PERIODO.consecutivoCompania) AND"
   SQL = SQL & " (PERIODO.ConsecutivoPeriodo = COMPROBANTE.ConsecutivoPeriodo)"
   fSQLActualizaComprobantesDeCxp = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLActualizaComprobantesDeCxp", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLDelReporteAnalisisDeVencimientoEntreFechas(ByVal valAnalisisDeVencimientoPor As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valConsecutivoCompania As String, ByVal valNombreMonedaLocal As String, ByVal valMonedaOriginal As Boolean, ByRef gMonedaLocalActual As Object, ByRef InsAdmPropAnalisisVenc As Object, ByRef gUltimaTasaDeCambio As Object) As String
   Dim SQL As String
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
      SQL = "SELECT "
      SQL = SQL & sqlMoneda & ", "
      SQL = SQL & sqlMonedaReporte & " AS MonedaReporte, "
      SQL = SQL & gUtilSQL.fMonth(sqlFecha, "") & " AS Mes, "
      SQL = SQL & gUtilSQL.fYear(sqlFecha, "") & " AS Ano, "
      SQL = SQL & "CxP.CodigoProveedor, "
      SQL = SQL & "proveedor.NombreProveedor, "
      SQL = SQL & "CxP.Numero, "
      SQL = SQL & sqlFecha & ", "
      SQL = SQL & "CxP.FechaVencimiento, "
      SQL = SQL & sqlDateDiff & " AS DiasVencidos, "
      SQL = SQL & gUtilSQL.getIIF(sqlDateDiff & " <= 0 ", sqlMtoRestante, "0", True) & " AS MtoNoVencido, "
      SQL = SQL & gUtilSQL.getIIF(sqlDateDiff & " > 0 AND " & sqlDateDiff & " <= " & InsAdmPropAnalisisVenc.GetPrimerVencimiento, sqlMtoRestante, "0", True) & " AS Mto1Vto, "
      SQL = SQL & gUtilSQL.getIIF(sqlDateDiff & " > " & InsAdmPropAnalisisVenc.GetPrimerVencimiento & " AND " & sqlDateDiff & " <= " & InsAdmPropAnalisisVenc.GetSegundoVencimiento, sqlMtoRestante, "0", True) & " AS Mto2Vto, "
      SQL = SQL & gUtilSQL.getIIF(sqlDateDiff & " > " & InsAdmPropAnalisisVenc.GetSegundoVencimiento & " AND " & sqlDateDiff & " <= " & InsAdmPropAnalisisVenc.GetTercerVencimiento, sqlMtoRestante, "0", True) & " AS Mto3Vto, "
      SQL = SQL & gUtilSQL.getIIF(sqlDateDiff & " > " & InsAdmPropAnalisisVenc.GetTercerVencimiento, sqlMtoRestante, "0", True) & " AS Mto4Vto, "
      SQL = SQL & sqlMtoRestante & " AS MtoTotal"
      SQL = SQL & " FROM " & "proveedor INNER JOIN " & "CxP ON ("
      SQL = SQL & "proveedor.CodigoProveedor = " & "CxP.CodigoProveedor"
      SQL = SQL & ") AND (" & "proveedor.ConsecutivoCompania = " & "CxP.ConsecutivoCompania)"
      SQL = SQL & " WHERE " & "CxP.ConsecutivoCompania = " & valConsecutivoCompania
      SQL = SQL & " AND (" & "CxP.Status" & _
                  " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ABONADO) & _
                  " OR " & "CxP.Status" & _
                  " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_PORCANCELAR) & ")"
      SQL = SQL & " AND " & gUtilSQL.DfSQLDateValueBetween("CxP.Fecha", valFechaInicial, valFechaFinal)
      SQL = SQL & " ORDER BY "
      SQL = SQL & sqlMoneda & ", "
      SQL = SQL & sqlFecha & ", "
      If valAnalisisDeVencimientoPor = gEnumReport.enumReporteOrdenadoPorToString(enum_ReporteOrdenadoPor.eRO_Nombre) Then
         SQL = SQL & "proveedor.NombreProveedor, "
      Else
         SQL = SQL & "CxP.CodigoProveedor, "
      End If
      SQL = SQL & "CxP.FechaVencimiento"
   End If
   fSQLDelReporteAnalisisDeVencimientoEntreFechas = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLDelReporteAnalisisDeVencimientoEntreFechas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLComprobanteRetIVA(ByVal valFechaRetencion As Date, ByVal valConsecutivoCompania As Long, _
                              ByVal valNumero As String, ByVal ValCodigoProveedor As String, _
                                 ByVal gMonedaLocalActual As Object, ByVal valRetAplicadaPago As Boolean, _
                                    ByVal valAnoAplicacion As Integer, ByVal valFecha As Date, _
                                       ByVal gUltimaTasaDeCambio As Object, ByVal valEsUnaCxPCuentaATercero As Boolean) As String
   Dim SQL As String
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

   SQL = "SELECT "
   SQL = SQL & sqlTotalCxP & " As TotalCXPComprobanteRetIva, "
   SQL = SQL & "proveedor.Direccion, "
   SQL = SQL & "proveedor.Telefonos, "
   SQL = SQL & "proveedor.NumeroRIF, "
   SQL = SQL & "proveedor.NumeroNIT, "
   SQL = SQL & "proveedor.NombreProveedor, "
   SQL = SQL & "proveedor.CodigoProveedor, "
   SQL = SQL & "cxp.FechaAplicacionRetIVA, "
   SQL = SQL & "cxp.MesDeAplicacion, "
   SQL = SQL & "cxp.AnoDeAplicacion, "
   SQL = SQL & gUtilSQL.getIIF(gUtilSQL.DfCIntSQL("cxP.NumeroComprobanteRetencion") & " <> " & "0", "cxP.NumeroComprobanteRetencion", gUtilSQL.fSimpleSqlValue("Sin N° Asignado"), True) & " AS NumeroComprobanteRetencion, "
   SQL = SQL & "cxP.PorcentajeRetencionAplicado, "
   SQL = SQL & "cxP.Fecha AS FechaDelDocOrigen, "
   SQL = SQL & gUtilSQL.getIIF("cxP.TipoDeCxP = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeTransaccion.eTD_FACTURA), gUtilSQL.getIIF("cxP.UsaPrefijoSerie='S'", "'Serie ' + cxP.Numero", "cxP.Numero", True), gUtilSQL.fSimpleSqlValue(""), True) & " AS NumeroDeFactura, "
   SQL = SQL & "cxP.NumeroControl, "
   SQL = SQL & gUtilSQL.getIIF("cxP.TipoDeCxP = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeTransaccion.eTD_NOTADEDEBITO), _
                              "cxP.Numero", gUtilSQL.fSimpleSqlValue(""), True) & _
               " AS NumeroDeNotaDebito, "
   SQL = SQL & gUtilSQL.getIIF("cxP.TipoDeCxP = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeTransaccion.eTD_NOTADECREDITO), _
                              "cxP.Numero", gUtilSQL.fSimpleSqlValue(""), True) & _
               " AS NumeroDeNotaCredito, "
   SQL = SQL & gUtilSQL.DfSQLCaseIfForEnum("cxP.TipoDeTransaccion", _
                        enum_TipoDeTransaccionDeLibrosFiscales.eTD_A01_REGISTRO, _
                        gEnumProyecto.fEnumTransaccionFiscalToStringInArray(True), "") & _
               " AS TipoDeTransaccion, "
   SQL = SQL & "cxP.NumeroDeFacturaAfectada, "
   SQL = SQL & sqlMontoExento & " AS MontoExento, "
   SQL = SQL & sqlMontoGravado & " AS MontoGravado, "
   SQL = SQL & sqlMontoGravableAlicuotaGeneral & " AS MontoGravableAlicuotaGeneral, "
   SQL = SQL & sqlMontoGravableAlicuota2 & " AS MontoGravableAlicuota2, "
   SQL = SQL & sqlMontoGravableAlicuota3 & " AS MontoGravableAlicuota3, "
   SQL = SQL & gUtilSQL.getIIF("cxp.AplicaIvaAlicuotaEspecial " & " = " & "'N'", _
                        "(SELECT TOP 1 alicuotaIVA.MontoAlicuotaGeneral FROM alicuotaIVA WHERE " & _
                        " alicuotaIVA.FechaDeInicioDeVigencia <= cxP.Fecha ORDER BY alicuotaIVA.FechaDeInicioDeVigencia DESC)", _
                        gUtilSQL.getIIF("cxP.MontoGravableAlicuotaEspecial1 " & " > " & "0", _
                        "cxP.PorcentajeIvaAlicuotaEspecial1 ", "cxP.PorcentajeIvaAlicuotaEspecial2 "), True) & "As AlicuotaG,"
   SQL = SQL & "(SELECT TOP 1 alicuotaIVA.MontoAlicuota2" & _
                        " FROM alicuotaIVA WHERE alicuotaIVA.FechaDeInicioDeVigencia <= cxP.Fecha" & _
                        " ORDER BY alicuotaIVA.FechaDeInicioDeVigencia" & _
                        " DESC) AS Alicuota2, "
   SQL = SQL & "(SELECT TOP 1 alicuotaIVA.MontoAlicuota3" & _
                        " FROM alicuotaIVA WHERE alicuotaIVA.FechaDeInicioDeVigencia <= cxP.Fecha" & _
                        " ORDER BY alicuotaIVA.FechaDeInicioDeVigencia" & _
                        " DESC) AS Alicuota3, "
   SQL = SQL & sqlMontoIva & " AS MontoIva, "
   SQL = SQL & sqlMontoIVAALicuotaGeneral & " AS MontoIVAAlicuotaGeneral, "
   SQL = SQL & sqlMontoIVAALicuota2 & " AS MontoIVAAlicuota2, "
   SQL = SQL & sqlMontoIVAALicuota3 & " AS MontoIVAAlicuota3, "
   If valAnoAplicacion >= gUtilDate.fYear(gMonedaLocalActual.GetHoyVigenteDesde) And valFecha < gMonedaLocalActual.GetHoyVigenteDesde Then
      SQL = SQL & "cxP.MontoRetenido * " & gMonedaLocalActual.fFactorDeConversion(valFechaRetencion) & " as MontoRetenido, "
   Else
      SQL = SQL & "cxP.MontoRetenido, "
   End If
   SQL = SQL & fSQLAnoYMesDeAplicacionIVAParaComprobante
   SQL = SQL & " FROM proveedor"
   SQL = SQL & " INNER JOIN cxP ON ("
   If valEsUnaCxPCuentaATercero Then
      SQL = SQL & " proveedor.CodigoProveedor = cxP.CodigoProveedorOriginalServicio"
   Else
      SQL = SQL & " proveedor.CodigoProveedor = cxP.CodigoProveedor"
   End If
   SQL = SQL & ") AND (proveedor.ConsecutivoCompania = cxP.ConsecutivoCompania)"
   SQL = SQL & " WHERE cxP.ConsecutivoCompania = " & valConsecutivoCompania
   SQL = SQL & " AND cxP.Numero = " & valNumero
   If valEsUnaCxPCuentaATercero Then
      SQL = SQL & " AND cxP.CodigoProveedorOriginalServicio = " & ValCodigoProveedor
   Else
      SQL = SQL & " AND cxP.CodigoProveedor = " & ValCodigoProveedor
   End If
   SQL = SQL & " AND cxP.SeHizoLaRetencionIVA = " & gUtilSQL.fBooleanToSqlValue(True)
h_EXIT:
   Set gEnumProyecto = Nothing
   Set insPagoSQL = Nothing
   On Error GoTo 0
   fSQLComprobanteRetIVA = SQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLComprobanteRetIVA", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLAnoYMesDeAplicacionIVAParaComprobante() As String
   Dim SQL As String
   Dim sqlMes As String
   Dim sqlAno As String
   On Error GoTo h_ERROR
   sqlMes = "cxP.FechaAplicacionRetIva"
   sqlMes = gUtilSQL.fMonth(sqlMes, "")
   sqlMes = gUtilSQL.fCast(sqlMes, eTDSS_VARCHAR, "")
   sqlMes = gUtilSQL.DfRightSQL(sqlMes, 2, "0")
   sqlAno = "cxP.FechaAplicacionRetIva"
   sqlAno = gUtilSQL.fYear(sqlAno, "")
   SQL = sqlAno & " AS AnoAplicRetIVA, "
   SQL = SQL & sqlMes & " AS MesAplicRetIVA "
h_EXIT: On Error GoTo 0
   fSQLAnoYMesDeAplicacionIVAParaComprobante = SQL
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
   Dim SQL As String
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
   SQL = "SELECT "
   SQL = SQL & "CxP.Numero, "
   SQL = SQL & "CxP.CodigoProveedor, "
   SQL = SQL & "Proveedor.NombreProveedor, "
   SQL = SQL & "CxP.Fecha, "
   SQL = SQL & "CxP.FechaVencimiento, "
   SQL = SQL & sqlStatusCxP & " AS StatusStr, "
   SQL = SQL & sqlMtoTotal & " AS MontoTotal, "
   SQL = SQL & sqlMtoRestante & " AS MontoRestante, "
   SQL = SQL & sqlMonedaReporte & " AS MonedaReporte, "
   SQL = SQL & "CxP.Moneda AS MonedaDocumento, "
   SQL = SQL & sqlCambio & " AS Cambio, "
   SQL = SQL & "CxP.porcentajeRetencionAplicado, "
   SQL = SQL & sqlMtoRetenido & " AS MontoRetenido "
   SQL = SQL & ", CxP.Status"
   SQL = SQL & " FROM CxP INNER JOIN Proveedor ON "
   SQL = SQL & "CxP.CodigoProveedor = Proveedor.CodigoProveedor"
   SQL = SQL & " AND CxP.ConsecutivoCompania = Proveedor.ConsecutivoCompania"
   SQL = SQL & " WHERE " & gUtilSQL.DfSQLDateValueBetween("CxP.Fecha", valFechaInicial, valFechaFinal)
   If unSoloStatus Then
      SQL = SQL & " AND CxP.Status = " & statusABuscarChr
   End If
   SQL = SQL & " AND CxP.ConsecutivoCompania = " & valConsecutivoCompania
   SQL = SQL & " AND cxP.EsUnaCuentaATerceros = " & gUtilSQL.fBooleanToSqlValue(False)
   SQL = SQL & " UNION"
   SQL = SQL & " SELECT "
   SQL = SQL & "CxP.Numero, "
   SQL = SQL & "CxP.CodigoProveedor, "
   SQL = SQL & "Proveedor.NombreProveedor, "
   SQL = SQL & "CxP.Fecha, "
   SQL = SQL & "CxP.FechaVencimiento, "
   SQL = SQL & sqlStatusCxP & " AS StatusStr, "
   SQL = SQL & sqlMtoTotal & " AS MontoTotal, "
   SQL = SQL & sqlMtoRestante & " AS MontoRestante, "
   SQL = SQL & sqlMonedaReporte & " AS MonedaReporte, "
   SQL = SQL & "CxP.Moneda AS MonedaDocumento, "
   SQL = SQL & sqlCambio & " AS Cambio, "
   SQL = SQL & "CxP.porcentajeRetencionAplicado, "
   SQL = SQL & sqlMtoRetenido & " AS MontoRetenido "
   SQL = SQL & ", CxP.Status"
   SQL = SQL & " FROM CxP INNER JOIN Proveedor ON "
   SQL = SQL & "CxP.CodigoProveedorOriginalServicio = Proveedor.CodigoProveedor"
   SQL = SQL & " AND CxP.ConsecutivoCompania = Proveedor.ConsecutivoCompania"
   SQL = SQL & " WHERE " & gUtilSQL.DfSQLDateValueBetween("CxP.Fecha", valFechaInicial, valFechaFinal)
   If unSoloStatus Then
      SQL = SQL & " AND CxP.Status = " & statusABuscarChr
   End If
   SQL = SQL & " AND CxP.ConsecutivoCompania = " & valConsecutivoCompania
   SQL = SQL & " AND cxP.EsUnaCuentaATerceros = " & gUtilSQL.fBooleanToSqlValue(True)
   
   
   SQL = SQL & " ORDER BY "
   SQL = SQL & "CxP.Status, "
   SQL = SQL & "MonedaReporte, "
   SQL = SQL & "CxP.Fecha, "
   SQL = SQL & "CxP.Numero, "
   SQL = SQL & "CxP.CodigoProveedor"
   Set gEnumProyecto = Nothing
   fSQLRetIVAenCxP = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLRetIVAenCxP", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function fConstruirSQLDeComprobanteCxP(ByVal valConsecutivoCompania As Long, ByVal valNumero As String, ByVal ValCodigoProveedor, ByVal valEsUnaCuentaDeTercero As Boolean) As String
   Dim gEnumProyecto As clsEnumAdministrativo
   Dim SQL As String
   Dim sqlTotal As String
   Dim sqlMontoRestante As String
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   SQL = " SELECT "
   SQL = SQL & gUtilSQL.DfSQLCaseIfForEnum("cxP" & ".TipoDeCxp", enum_TipoDeTransaccion.eTD_FACTURA, gEnumProyecto.fEnumTipoDeTransaccioToStringInArray(True), "TipoDeCxP") & ","
   SQL = SQL & gUtilSQL.getIIF("cxP" & ".UsaPrefijoSerie='S'", "'Serie ' +" & "cxP" & ".Numero", "cxP" & ".Numero", True) & " AS Numero, "
   SQL = SQL & "cxP" & ".NumeroControl AS NumeroControl, "
   SQL = SQL & "cxP" & ".Fecha AS Fecha, "
   SQL = SQL & "cxP" & ".FechaVencimiento AS FechaVencimiento, "
   SQL = SQL & "cxP" & ".NumDiasDeVencimiento AS DiasVencimiento, "
   SQL = SQL & "cxP" & ".FechaCancelacion AS FechaCancelacion, "
   SQL = SQL & gUtilSQL.DfSQLCaseIfForEnum("cxP" & ".Status", enum_StatusDocumento.eSD_PORCANCELAR, gEnumProyecto.fEnumStatusCxCToStringInArray(True), "Status") & ","
   SQL = SQL & "cxP" & ".EsCxPhistorica AS CxPHistorico, "
   SQL = SQL & "cxP" & ".Moneda AS Moneda, "
   SQL = SQL & "cxP" & ".CambioAbolivares AS CambioABolivares, "
   SQL = SQL & "proveedor" & ".CodigoProveedor As CodigoProveedor, "
   SQL = SQL & "proveedor" & ".NumeroRIF AS NumeroRif, "
   SQL = SQL & "proveedor" & ".NombreProveedor As NombreProveedor, "
   SQL = SQL & "cxP" & ".Observaciones AS Observaciones, "
   sqlTotal = "(" & "cxP" & ".MontoExento + " & "cxP" & ".MontoGravado + " & "cxP" & ".MontoIva)"
   SQL = SQL & sqlTotal & " AS Total, "
   SQL = SQL & "cxP" & ".MontoAbonado AS MontoAbonado,"
   sqlMontoRestante = sqlTotal & "-" & "cxP" & ".MontoAbonado"
   SQL = SQL & sqlMontoRestante & " AS RestaPorPagar,"
   SQL = SQL & "cxP" & ".MesDeAplicacion AS Mes, "
   SQL = SQL & "cxP" & ".AnoDeAplicacion AS Ano, "
   SQL = SQL & gUtilSQL.DfSQLCaseIfForEnum("cxP" & ".TipoDeCompra", enum_TipoDeCompra.eTD_COMPRASEXENTAS, gEnumProyecto.fenumTipoCompraToStringInArray(True), "TipodeCompra") & ","
   SQL = SQL & "cxP" & ".NumeroPlanillaDeImportacion AS PlanillaImportacion, "
   SQL = SQL & "cxP" & ".NumeroExpedienteDeImportacion AS ExpedienteImportacion, "
   SQL = SQL & "cxP" & ".NumeroDeclaracionAduana AS NumeroDeclaracionAduana, "
   SQL = SQL & "cxP" & ".FechaDeclaracionAduana AS FechaDeclaracionAduana, "
   SQL = SQL & "cxP" & ".UsaPrefijoSerie AS UsaPrefijoSerie, "
   SQL = SQL & gUtilSQL.DfSQLCaseIfForEnum("cxP" & ".TipoDeTransaccion", enum_TipoDeTransaccionDeLibrosFiscales.eTD_A01_REGISTRO, gEnumProyecto.fEnumTransaccionFiscalToStringInArray(True), "TipoTransaccion") & ","
   SQL = SQL & "cxP" & ".NumeroDeFacturaAfectada AS FacturaAfectada, "
   SQL = SQL & gUtilSQL.DfSQLCaseIfForEnum("cxP" & ".CreditoFiscal", enum_CreditoFiscal.eCF_DEDUCIBLE, gEnumProyecto.fEnumCreditoFiscalToStringInArray(True), "CreditoFiscal") & ","
   SQL = SQL & "cxP" & ".MontoExento AS MontoExento, "
'   SQL = SQL & "cxP" & ".MontoGravableAlicuotaGeneral AS AGMontoGravable, "
'   SQL = SQL & "cxP" & ".MontoIvaalicuotaGeneral AS AGMontoIVA, "
   SQL = SQL & gUtilSQL.getIIF("cxp.AplicaIvaAlicuotaEspecial = " & gUtilSQL.fSimpleSqlValue("S"), gUtilSQL.getIIF("cxP.MontoGravableAlicuotaEspecial1 <> 0", "cxP.MontoGravableAlicuotaEspecial1", "cxP.MontoGravableAlicuotaEspecial2", True), "cxP.MontoGravableAlicuotaGeneral", True) & "AS AGMontoGravable,"
   SQL = SQL & gUtilSQL.getIIF("cxp.AplicaIvaAlicuotaEspecial = " & gUtilSQL.fSimpleSqlValue("S"), gUtilSQL.getIIF("cxP.MontoIVAAlicuotaEspecial1 <> 0", "cxP.MontoIVAAlicuotaEspecial1", "cxP.MontoIVAAlicuotaEspecial2", True), "cxP.MontoIvaalicuotaGeneral", True) & "AS AGMontoIVA,"
   SQL = SQL & "cxP" & ".MontoGravableAlicuota2 AS A2MontoGravable, "
   SQL = SQL & "cxP" & ".MontoIvaalicuota2 AS A2MontoIVA, "
   SQL = SQL & "cxP" & ".NumeroComprobanteRetencion AS NumeroComprobanteRetencionIVA, "
   SQL = SQL & "cxP" & ".FechaAplicacionRetIva AS FechaAplicacion, "
   SQL = SQL & "cxP" & ".OrigenDeLaRetencion AS ModuloDeOrigen, "
   SQL = SQL & "cxP" & ".PorcentajeRetencionAplicado AS PorcentajeRetenido, "
   SQL = SQL & "cxP" & ".MontoRetenido AS MontoRetencionIVA, "
   SQL = SQL & "cxP" & ".CodigoMoneda AS CodigoMoneda"
   If valEsUnaCuentaDeTercero Then
      SQL = SQL & "cxP" & ", cxp.CodigoProveedorOriginalServicio as CodigoProvOriginal, proveedorOriginal.NombreProveedor as NombreProvOriginal"
   End If
   SQL = SQL & " FROM " & "proveedor as Proveedor INNER JOIN " & "cxP" & " ON  ( " & "proveedor" & ".CodigoProveedor = " & "cxP" & ".CodigoProveedor )" & _
   " AND (" & "proveedor" & ".ConsecutivoCompania = " & "cxP" & ".ConsecutivoCompania ) "
   If valEsUnaCuentaDeTercero Then
      SQL = SQL & " INNER JOIN proveedor as proveedorOriginal ON ( proveedorOriginal.CodigoProveedor = cxP.CodigoProveedorOriginalServicio) "
      SQL = SQL & " AND (proveedorOriginal.ConsecutivoCompania = cxP.ConsecutivoCompania)"
   End If
   SQL = SQL & "WHERE " & "cxP" & ".ConsecutivoCompania = " & gUtilSQL.fNumToStrSQL(valConsecutivoCompania) & _
   " AND " & "cxP" & ".Numero = " & gUtilSQL.fSimpleSqlValue(valNumero) & _
   " AND " & "cxP" & ".CodigoProveedor = " & gUtilSQL.fSimpleSqlValue(ValCodigoProveedor)
   Set gEnumProyecto = Nothing
   fConstruirSQLDeComprobanteCxP = SQL
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
   Dim SQL As String
   On Error GoTo h_ERROR
   If valTraerTotales Then
      sqlMontoOriginal = valfSQLMontoTotalCxP
      sqlMontoRestante = "(" & sqlMontoOriginal & " - " & "cxP" & ".MontoAbonado)"
      SQL = "SELECT "
      SQL = SQL & "cxP" & ".Numero, "
      SQL = SQL & "cxP" & ".NumeroControl, " 'Pendiente
      SQL = SQL & "cxP" & ".ConsecutivoCxp, "
      SQL = SQL & "cxP" & ".Status, "
      SQL = SQL & "cxP" & ".MontoIva, "
      SQL = SQL & sqlMontoOriginal & " AS MontoOriginalCxP, "
      SQL = SQL & sqlMontoRestante & " AS MontoRestante, "
      SQL = SQL & "cxP" & ".Moneda, "
      If valEnviadoAPago Then
         SQL = SQL & "cxP" & ".CambioABolivares, "
      End If
      SQL = SQL & "cxP" & ".SeHizoLaRetencion AS Retenido, "
      SQL = SQL & "cxP" & ".RetencionAplicadaEnPago AS RetenerEnPago, "
      SQL = SQL & gUtilSQL.getIIF("cxP" & ".RetencionAplicadaEnPago" & " = " & gUtilSQL.fBooleanToSqlValue(False), "cxP" & ".MontoRetenido", "0", True) & " AS MontoRetIVA, "
      SQL = SQL & "cxP" & ".SeHizoLaRetencionIva AS IvaRetenido, "
      SQL = SQL & "cxP" & ".CodigoMoneda, "
      SQL = SQL & "cxP.EsUnaCuentaATerceros, "
      SQL = SQL & "cxP.CodigoProveedorOriginalServicio "
      SQL = SQL & " FROM " & "cxP"
      SQL = SQL & " WHERE " & "cxP" & ".ConsecutivoCompania" & _
                  " = " & gUtilSQL.fNumToStrSQL(valConsecutivoCompania)
      SQL = SQL & " AND " & "cxP" & ".CodigoProveedor" & _
                  " = " & gUtilSQL.fSimpleSqlValue(refCodigoProveedor)
      SQL = SQL & " AND " & "cxP" & ".Status" & _
                  " <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ANULADO)
     
     If (valFiltraPorOrigenRetencion) Then
      
      SQL = SQL & " AND " & "cxP" & ".OrigenDeLaRetencionISLR" & _
                  " <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_DondeSeEfectuaLaRetencionIVA.eDS_NO_RETENIDA)
     
     End If
    
      SQL = SQL & " AND " & "cxP" & ".MontoAbonado" & _
                  " <> (" & "cxP" & ".MontoExento" & _
                     " + " & "cxP" & ".MontoGravado" & _
                     " + " & "cxP" & ".MontoIva)"
      SQL = SQL & " AND Fecha <= " & gUtilSQL.fDateToSQLValue(valFecha)
      SQL = SQL & " AND " & gUtilSQL.DfMidSQL("cxP" & ".Numero", 1, 3) & " <> " & gUtilSQL.fSimpleSqlValue(valPrefijoRIS)
    
      If valEnviadoAPago Then
         SQL = SQL & " AND " & "cxP" & ".Numero" & " = " & gUtilSQL.fSimpleSqlValue(valNumeroEnviadoAPago)
      End If
         SQL = SQL & " AND " & "cxP" & ".EstaAsociadoARendicion  = 'N' "
    
   Else
      SQL = "SELECT "
      SQL = SQL & "cxP" & ".Numero"
      SQL = SQL & " FROM " & "cxP"
      SQL = SQL & " WHERE " & "cxP" & ".ConsecutivoCompania" & " = " & gUtilSQL.fNumToStrSQL(valConsecutivoCompania)
      SQL = SQL & " AND " & "cxP" & ".Status <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ANULADO)
      SQL = SQL & " AND " & "cxP" & ".CodigoProveedor" & " = " & gUtilSQL.fSimpleSqlValue(refCodigoProveedor)
      SQL = SQL & " AND " & gUtilSQL.DfMidSQL("cxP" & ".Numero", 1, 3) & " <> " & gUtilSQL.fSimpleSqlValue(valPrefijoRIS)
   End If
   If valOrdenarCxPPorFacturaDocumento Then
      SQL = SQL & " ORDER BY " & gUtilSQL.fSQLFillRightWithCaracter("cxP.Numero", 25, "0")
   End If
   fSQLSearchCxPPendientes = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: fSQLSearchCxPPendientes = ""
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLSearchCxPPendientes", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLSearchByCodigoProveedoryNumeroCxPParaActualizarDesdePago(ByVal ValCodigoProveedor As String, ByVal valNumeroCxP As String, ByVal valConsecutivoCompania As Long) As String
   Dim SQL As String
   Dim sqlWhere As String
   On Error GoTo h_ERROR
   sqlWhere = ""
   sqlWhere = gUtilSQL.fSQLNumberValueWithAnd(sqlWhere, "ConsecutivoCompania", valConsecutivoCompania)
   sqlWhere = gUtilSQL.fSQLValueWithAnd(sqlWhere, "CodigoProveedor", ValCodigoProveedor, False)
   sqlWhere = gUtilSQL.fSQLValueWithAnd(sqlWhere, "Numero", valNumeroCxP, False)
   SQL = "SELECT "
   SQL = SQL & "ConsecutivoCompania, "
   SQL = SQL & "Numero, "
   SQL = SQL & "Status, "
   SQL = SQL & "CodigoProveedor, "
   SQL = SQL & "MontoExento, "
   SQL = SQL & "MontoGravado, "
   SQL = SQL & "MontoIva, "
   SQL = SQL & "MontoAbonado, "
   SQL = SQL & "TotalOtrosImpuestos, "
   SQL = SQL & "SeHizoLaRetencion, "
   SQL = SQL & "SeHizoLaRetencionIva, "
   SQL = SQL & "MontoRetenido, "
   SQL = SQL & "NumeroComprobanteRetencion, "
   SQL = SQL & "RetencionAplicadaEnPago, "
   SQL = SQL & "FechaCancelacion, "
   SQL = SQL & "OrigenDeLaRetencion, "
   SQL = SQL & "OrigenInformacionRetencion, "
   SQL = SQL & "FechaAplicacionRetIva, "
   SQL = SQL & "EsCxPhistorica, "
   SQL = SQL & "DondeContabilRetIva, "
   SQL = SQL & "SeContabilRetIva, "
   SQL = SQL & "ConsecutivoCxP, "
   SQL = SQL & "NombreOperador, "
   SQL = SQL & "OrigenDeLaRetencionISLR,"
   SQL = SQL & "SeContabilISLR,"
   SQL = SQL & "ISLRAplicadaEnPago,"
   SQL = SQL & "FechaUltimaModificacion"
   SQL = SQL & " FROM " & "cxP"
   SQL = SQL & gUtilSQL.getWhereSQL(sqlWhere)
   SQL = SQL & " ORDER BY Numero"
   fSQLSearchByCodigoProveedoryNumeroCxPParaActualizarDesdePago = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: fSQLSearchByCodigoProveedoryNumeroCxPParaActualizarDesdePago = ""
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLSearchByCodigoProveedoryNumeroCxPParaActualizarDesdePago", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function fSQLActualizarPrimerPaso(ByVal valConsecutivoCompania As Long, ByVal valConsecutivoCxp As Long) As String
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = "SELECT "
   SQL = SQL & "ConsecutivoCompania, "
   SQL = SQL & "ConsecutivoCxP, "
   SQL = SQL & "Numero, "
   SQL = SQL & "CodigoProveedor, "
   SQL = SQL & "TipoDeCxp, "
   SQL = SQL & "Status, "
   SQL = SQL & "Fecha, "
   SQL = SQL & "FechaCancelacion, "
   SQL = SQL & "FechaVencimiento, "
   SQL = SQL & "FechaAnulacion, "
   SQL = SQL & "Moneda, "
   SQL = SQL & "CambioAbolivares, "
   SQL = SQL & "AplicaParaLibrodeCompras, "
   SQL = SQL & "MontoExento, "
   SQL = SQL & "MontoGravado, "
   SQL = SQL & "MontoIva, "
   SQL = SQL & "MontoAbonado, "
   SQL = SQL & "MesDeAplicacion, "
   SQL = SQL & "AnoDeAplicacion, "
   SQL = SQL & "Observaciones, "
   SQL = SQL & "CreditoFiscal, "
   SQL = SQL & "TipoDeCompra, "
   SQL = SQL & "SeHizoLaRetencion, "
   SQL = SQL & "NombreOperador, "
   SQL = SQL & "NumDiasDeVencimiento, "
   SQL = SQL & "FechaUltimaModificacion, "
    SQL = SQL & "CodigoMoneda"
   SQL = SQL & " FROM " & "cxP"
   SQL = SQL & " WHERE ConsecutivoCompania = " & gUtilSQL.fNumToStrSQL(valConsecutivoCompania)
   SQL = SQL & " AND ConsecutivoCxP = " & gUtilSQL.fNumToStrSQL(valConsecutivoCxp)
   fSQLActualizarPrimerPaso = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLActualizarPrimerPaso", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function fSQLActualizarSegundoPaso(ByVal valConsecutivoCompania As Long, ByVal valConsecutivoCxp As Long) As String
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = "SELECT "
   SQL = SQL & "ConsecutivoCompania, "
   SQL = SQL & "ConsecutivoCxP, "
   SQL = SQL & "Numero, "
   SQL = SQL & "CodigoProveedor, "
   SQL = SQL & "MontoGravableAlicuotaGeneral, "
   SQL = SQL & "MontoGravableAlicuota2, "
   SQL = SQL & "MontoGravableAlicuota3, "
   SQL = SQL & "MontoIvaalicuotaGeneral, "
   SQL = SQL & "MontoIvaalicuota2, "
   SQL = SQL & "MontoIvaalicuota3, "
   SQL = SQL & "NumeroComprobanteRetencion, "
   SQL = SQL & "NumeroPlanillaDeImportacion, "
   SQL = SQL & "NumeroExpedienteDeImportacion, "
   SQL = SQL & "TipoDeTransaccion, "
   SQL = SQL & "NumeroDeFacturaAfectada, "
   SQL = SQL & "NumeroControl, "
   SQL = SQL & "SeHizoLaRetencionIva, "
   SQL = SQL & "FechaAplicacionRetIva, "
   SQL = SQL & "PorcentajeRetencionAplicado, "
   SQL = SQL & "MontoRetenido, "
   SQL = SQL & "OrigenDeLaRetencion, "
   SQL = SQL & "DondeContabilRetIva, "
   SQL = SQL & "OrigenInformacionRetencion,"
   SQL = SQL & "TotalOtrosImpuestos,"
   SQL = SQL & "RetencionAplicadaEnPago, "
   SQL = SQL & "OrigenDeLaRetencionISLR,"
   SQL = SQL & "SeContabilISLR,"
   SQL = SQL & "ISLRAplicadaEnPago,"
   SQL = SQL & "MontoRetenidoISLR,"
   SQL = SQL & "DondeContabilISLR,"
   SQL = SQL & "EstaAsociadoARendicion, "
   SQL = SQL & "ConsecutivoRendicion, "
   SQL = SQL & "FechaAplicacionImpuestoMunicipal, "
   SQL = SQL & "NumeroComprobanteImpuestoMunicipal, "
   SQL = SQL & "MontoRetenidoImpuestoMunicipal, "
   SQL = SQL & "ImpuestoMunicipalRetenido, "
   SQL = SQL & "NumeroControlDeFacturaAfectada, "
   SQL = SQL & "NumeroDeclaracionAduana, "
   SQL = SQL & "FechaDeclaracionAduana, "
   SQL = SQL & "UsaPrefijoSerie, "
   SQL = SQL & "EsUnaCuentaATerceros, "
   SQL = SQL & "CodigoProveedorOriginalServicio, "
   SQL = SQL & "SeHizoLaDetraccion , AplicaIvaAlicuotaEspecial, MontoGravableAlicuotaEspecial1, MontoIVAAlicuotaEspecial1, PorcentajeIvaAlicuotaEspecial1, MontoGravableAlicuotaEspecial2, MontoIVAAlicuotaEspecial2, PorcentajeIvaAlicuotaEspecial2"
   SQL = SQL & ", DiaDeAplicacion "
   SQL = SQL & " FROM " & "cxP"
   SQL = SQL & " WHERE ConsecutivoCompania = " & gUtilSQL.fNumToStrSQL(valConsecutivoCompania)
   SQL = SQL & " AND ConsecutivoCxP = " & gUtilSQL.fNumToStrSQL(valConsecutivoCxp)
   fSQLActualizarSegundoPaso = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLActualizarSegundoPaso", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

'********************Generar pago de ISLR en CxP
Public Function fSQLInsertarCxP(ByVal valnombreParaTabla As String, ByVal valConsecutivoCompania As Long) As String
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = "INSERT INTO tblInformeCxCCxP " & "("
   SQL = SQL & "ConsecutivoCompania, "
   SQL = SQL & "Idusuario, "
   SQL = SQL & "Idmodulo, "
   SQL = SQL & "Numero, "
   SQL = SQL & "Status, "
   SQL = SQL & "CodigoCliProv, "
   SQL = SQL & "Origen, "
   SQL = SQL & "Tipo, "
   SQL = SQL & "MontoExento, "
   SQL = SQL & "MontoGravado, "
   SQL = SQL & "MontoIva, "
   SQL = SQL & "MontoAbonado, "
   SQL = SQL & "Fecha, "
   SQL = SQL & "FechaCancelacion, "
   SQL = SQL & "FechaVencimiento, "
   SQL = SQL & "FechaAnulacion, "
   SQL = SQL & "Moneda, "
   SQL = SQL & "CambioAbolivares, "
   SQL = SQL & "CodigoMoneda"
   SQL = SQL & ") SELECT "
   SQL = SQL & valConsecutivoCompania & ", "
   SQL = SQL & gUtilSQL.fSimpleSqlValue(valnombreParaTabla) & ", "
   SQL = SQL & gUtilSQL.fSimpleSqlValue(CM_MODULO_CxP) & ", "
   SQL = SQL & "Numero, "
   SQL = SQL & "Status, "
   SQL = SQL & "CodigoProveedor, "
   SQL = SQL & gUtilSQL.fSQLSimpleValueForEnum(enum_OrigenFacturacionOManual.eOF_MANUAL) & ", "
   SQL = SQL & "TipoDeCxP, "
   SQL = SQL & "MontoExento, "
   SQL = SQL & "MontoGravado, "
   SQL = SQL & "MontoIva, "
   SQL = SQL & "MontoAbonado, "
   SQL = SQL & "Fecha, "
   SQL = SQL & "FechaCancelacion, "
   SQL = SQL & "FechaVencimiento, "
   SQL = SQL & "FechaAnulacion, "
   SQL = SQL & "Moneda, "
   SQL = SQL & "CambioABolivares, "
   SQL = SQL & "CodigoMoneda"
   SQL = SQL & " FROM cxP "
   SQL = SQL & " WHERE ConsecutivoCompania = " & valConsecutivoCompania
h_EXIT: On Error GoTo 0
   fSQLInsertarCxP = SQL
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
   Dim vSQL As String
   On Error GoTo h_ERROR
   vSQL = " SELECT retPago.* "
   vSQL = vSQL & " FROM RetPago "
   vSQL = vSQL & " INNER JOIN retDocumentoPagado "
   vSQL = vSQL & " ON retPago.ConsecutivoCompania = retDocumentoPagado.ConsecutivoCompania "
   vSQL = vSQL & " AND retPago.NumeroComprobante = retDocumentoPagado.NumeroComprobante "
   vSQL = vSQL & " INNER JOIN cxP "
   vSQL = vSQL & " ON retDocumentoPagado.ConsecutivoCompania = cxP.ConsecutivoCompania "
   vSQL = vSQL & " AND retDocumentoPagado.NumeroDelDocumento = Numero "
   vSQL = vSQL & " AND retPago.CodigoProveedor = cxP.CodigoProveedor "
   vSQL = vSQL & " WHERE retPago.consecutivoCompania = " & valConsecutivoCompania
   vSQL = vSQL & " AND cxP.Numero = " & gUtilSQL.fSimpleSqlValue(valNumero)
   vSQL = vSQL & " AND cxP.CodigoProveedor = " & gUtilSQL.fSimpleSqlValue(ValCodigoProveedor)
h_EXIT: On Error GoTo 0
   fSQLRetPago = vSQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLRetPago", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
 End Function

Public Function fSQLSearchDetail(ByVal valNumeroReferencia As String, ByVal valProveedor As String, ByVal valConsecutivoCompania As Long) As String
   Dim SQL As String
   Dim sqlSortOrder As String
   On Error GoTo h_ERROR
   sqlSortOrder = " ORDER BY " & "RetDocumentoPagado.ConsecutivoCompania" _
      & ", " & "RetDocumentoPagado.NumeroComprobante" _
      & ", " & "RetDocumentoPagado.SecuencialDocPagado"
   SQL = ""
   SQL = gUtilSQL.fSQLValueWithAnd(SQL, " RetPago.CodigoProveedor", valProveedor, False)
   SQL = gUtilSQL.fSQLNumberValueWithAnd(SQL, " RetPago.ConsecutivoCompania", valConsecutivoCompania)
   SQL = gUtilSQL.fSQLValueWithAnd(SQL, " RetPago.NumeroReferencia", valNumeroReferencia, False)
   fSQLSearchDetail = "SELECT NumeroDelDocumento, NumeroControl, " _
         & "CodigoRetencion,  PorcentajeDeRetencion, MontoOriginal, " _
         & "MontoBaseImponible, MontoRetencion, " _
         & "TipoOperacion,retDocumentoPagado.ConsecutivoCompania, " _
         & "retDocumentoPagado.NumeroComprobante, SecuencialDocPagado " _
         & " FROM RetDocumentoPagado INNER JOIN RetPago " _
         & " ON " & " RetPago.ConsecutivoCompania = retDocumentoPagado.ConsecutivoCompania" _
         & " AND  " & " RetPago.NumeroComprobante = retDocumentoPagado.NumeroComprobante" _
         & gUtilSQL.getWhereSQL(SQL) & sqlSortOrder
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: fSQLSearchDetail = ""
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLSearchDetail", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConstruirSQLDeCxPConComprobante(ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valComprobanteNavTableName As String, ByVal valConsecutivoCompania As Long, ByVal valDocOrigenContabilizacionParaSQL As String) As String
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = " SELECT "
   SQL = SQL & " cxP.Fecha AS FechaDocumento, "
   SQL = SQL & " cxP.Numero AS NumeroDocumento, "
   SQL = SQL & " proveedor.CodigoProveedor"
   SQL = SQL & " AS CodigoProveedor , "
   SQL = SQL & " cxP.TipoDeCxp AS Tipo, "
   SQL = SQL & "proveedor.NombreProveedor"
   SQL = SQL & " AS NombreProveedor , "
   SQL = SQL & valComprobanteNavTableName & "." & "GeneradoPor"
   SQL = SQL & " AS TipoDeDocumento, "
   SQL = SQL & valComprobanteNavTableName & "." & "Fecha"
   SQL = SQL & " AS FechaCombrobante, "
   SQL = SQL & valComprobanteNavTableName & "." & "NUMERO"
   SQL = SQL & " AS NumeroComprobante, "
   SQL = SQL & valComprobanteNavTableName & "." & "DESCRIPCION"
   SQL = SQL & " AS DescripcionComprobante, "
   SQL = SQL & valComprobanteNavTableName & "." & "TotalHaber"
   SQL = SQL & " AS TotalHaber, "
   SQL = SQL & valComprobanteNavTableName & "." & "NoDocumentoOrigen"
   SQL = SQL & " AS NumeroDocumentoOrigen, "
   SQL = SQL & "(" & "cxP.MontoExento + "
   SQL = SQL & "cxP.MontoGravado ) "
   SQL = SQL & " AS TotalDocumento "
   SQL = SQL & " FROM " & "cxP, " & valComprobanteNavTableName & ", "
   SQL = SQL & " proveedor "
   SQL = SQL & " WHERE " & "cxP.CodigoProveedor = "
   SQL = SQL & "proveedor.CodigoProveedor"
   SQL = SQL & " AND " & "cxP.ConsecutivoCompania = "
   SQL = SQL & "proveedor.ConsecutivoCompania"
   SQL = SQL & " AND " & valComprobanteNavTableName & "." & "NoDocumentoOrigen" & " = "
   SQL = SQL & "(" & valDocOrigenContabilizacionParaSQL & ")"
'   SQL = SQL & "(" & getFN_NO_DOC_ORIGEN_CONTABILIZACION_PARA_SQL(True) & ")"
   SQL = SQL & " AND " & "cxP.ConsecutivoCompania = "
   SQL = SQL & valConsecutivoCompania
   SQL = SQL & " AND " & valComprobanteNavTableName & "." & "GeneradoPor" & " ='"
   SQL = SQL & gConvert.enumerativoAChar(enum_ComprobanteGeneradoPor.eCG_CXP) & "'"
   SQL = SQL & " AND " & "cxP.Fecha"
   SQL = SQL & " BETWEEN "
   SQL = SQL & gUtilSQL.fDateToSQLValue(valFechaInicial) & " AND "
   SQL = SQL & gUtilSQL.fDateToSQLValue(valFechaFinal)
   SQL = SQL & " AND " & valComprobanteNavTableName & "." & "Fecha"
   SQL = SQL & " BETWEEN "
   SQL = SQL & gUtilSQL.fDateToSQLValue(valFechaInicial) & " AND "
   SQL = SQL & gUtilSQL.fDateToSQLValue(valFechaFinal)
   fConstruirSQLDeCxPConComprobante = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConstruirSQLDeCxPConComprobante", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLProcesoPostEliminacionCxP(ByVal valNumeroDocOrigen As String, ByVal valConsecutivoCompania As Long) As String
   Dim vCodigoProveedor As String
   Dim vNumeroCxP As String
   Dim SQL As String
   On Error GoTo h_ERROR
   vCodigoProveedor = gTexto.DfMid(valNumeroDocOrigen, 1, gTexto.DfInStr(valNumeroDocOrigen, gTexto.fSeparadorStandardDeElementosString) - 1)
   vNumeroCxP = gTexto.DfMid(valNumeroDocOrigen, gTexto.DfInStr(valNumeroDocOrigen, gTexto.fSeparadorStandardDeElementosString) + 1)
   SQL = "UPDATE cxP "
   SQL = SQL & " SET SeContabilRetIva = " & gUtilSQL.fBooleanToSqlValue(False)
   SQL = SQL & " SET SeContabilISLR = " & gUtilSQL.fBooleanToSqlValue(False)
   SQL = SQL & " WHERE ConsecutivoCompania = " & valConsecutivoCompania
   SQL = SQL & " AND CodigoProveedor = " & gUtilSQL.fSimpleSqlValue(vCodigoProveedor)
   SQL = SQL & " AND Numero = " & gUtilSQL.fSimpleSqlValue(vNumeroCxP)
   SQL = SQL & " AND DondeContabilRetIVA = " & gUtilSQL.fSQLSimpleValueForEnum(enum_DondeEfectuoContabilizacionRetIVA.eDC_CxP)
   SQL = SQL & " AND DondeContabilISLR = " & gUtilSQL.fSQLSimpleValueForEnum(enum_DondeEfectuoContabilizacionRetIVA.eDC_CxP)
   SQL = SQL & " AND MontoRetenido <> 0"
   SQL = SQL & " AND MontoRetenidoISLR <> 0"
   fSQLProcesoPostEliminacionCxP = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
       "fSQLProcesoPostEliminacionCxP", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLPagosEnXML(ByVal valFechaDesde As Date, ByVal valFechaHasta As Date, ByVal valConsecutivoCompania As Long) As String
   Dim vSQL As String
   On Error GoTo h_ERROR
   vSQL = " SELECT "
   vSQL = vSQL & " retPago.ConsecutivoCompania, "
   vSQL = vSQL & " retPago.NumeroComprobante "
   vSQL = vSQL & "FROM retPago "
   vSQL = vSQL & " INNER JOIN Proveedor ON "
   vSQL = vSQL & "retPago.ConsecutivoCompania = "
   vSQL = vSQL & "Proveedor.ConsecutivoCompania "
   vSQL = vSQL & " AND retPago.CodigoProveedor = "
   vSQL = vSQL & "Proveedor.CodigoProveedor "
   vSQL = vSQL & " WHERE "
   vSQL = vSQL & " retPago.ConsecutivoCompania = " & valConsecutivoCompania
   vSQL = vSQL & " AND " & gUtilSQL.fSQLValueBetween("retPago.Fecha", gUtilSQL.fSimpleSqlValue(valFechaDesde), gUtilSQL.fSimpleSqlValue(valFechaHasta))
   fSQLPagosEnXML = vSQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLPagosEnXML", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLCxPPorCancelar(ByVal valConsecutivoCompania As Long, ByVal ValCodigoProveedor As String, ByVal valFiltroPorProveedor As Boolean) As String
   Dim vSQL As String
   Dim vSQLWhere As String
   On Error GoTo h_ERROR
    vSQLWhere = gUtilSQL.fSQLEnumValueWithAnd("", "cxP.OrigenDeLaRetencionISLR", enum_DondeSeEfectuaLaRetencionIVA.eDS_NO_RETENIDA)
    vSQLWhere = gUtilSQL.fSQLEnumValueWithAnd(vSQLWhere, "cxP.Status", enum_StatusDocumento.eSD_PORCANCELAR)
    vSQLWhere = gUtilSQL.fSQLEnumValueWithAnd(vSQLWhere, "cxP.TipoDeCxp", enum_TipoDeTransaccion.eTD_FACTURA)

   
    If (valFiltroPorProveedor) Then
     vSQLWhere = gUtilSQL.fSQLValueWithAnd(vSQLWhere, "cxP.CodigoProveedor", ValCodigoProveedor, False)
    End If
    vSQLWhere = gUtilSQL.fSQLNumberValueWithAnd(vSQLWhere, "cxP.ConsecutivoCompania", valConsecutivoCompania)
    vSQLWhere = gUtilSQL.getWhereSQL(vSQLWhere)
    
    vSQL = "SELECT "
    vSQL = vSQL & "cxP" & ".Fecha, "
    vSQL = vSQL & "cxP" & ".Numero, "
    vSQL = vSQL & "cxP" & ".NumeroControl, "
    vSQL = vSQL & "cxP" & ".NombreOperador "
  
    vSQL = vSQL & " FROM  cxP "
    vSQL = vSQL & vSQLWhere
    
   fSQLCxPPorCancelar = vSQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: fSQLCxPPorCancelar = ""
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLCxPPorCancelar", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLComprobanteRetencionEnCxP(ByVal valConsecutivoCompania As Long, ByVal valNumeroComprobanteRetencion As Long, ByVal valTipoDePersona As String, ByVal valFechaInicioVigencia As Date) As String
   Dim vSQL As String
   Dim vSQLWhere As String
   On Error GoTo h_ERROR
      
    vSQLWhere = gUtilSQL.fSQLValueWithAnd("", "TipoDePersona", valTipoDePersona, False)
    vSQLWhere = gUtilSQL.fSQLNumberValueWithAnd(vSQLWhere, "Pago.NumeroComprobante", valNumeroComprobanteRetencion)
    vSQLWhere = gUtilSQL.fSQLNumberValueWithAnd(vSQLWhere, "retDocumentoPagado.NumeroComprobante", valNumeroComprobanteRetencion)
    vSQLWhere = gUtilSQL.fSQLNumberValueWithAnd(vSQLWhere, "retDocumentoPagado.ConsecutivoCompania", valConsecutivoCompania)
    vSQLWhere = gUtilSQL.fSQLValueWithAnd(vSQLWhere, "TablaRetencion.FechaDeInicioDeVigencia", valFechaInicioVigencia, False)
    vSQLWhere = gUtilSQL.getWhereSQL(vSQLWhere)
    
   vSQL = " SELECT "
   vSQL = vSQL & "NumeroDelDocumento,"
   vSQL = vSQL & "retDocumentoPagado.NumeroControl,"
   vSQL = vSQL & "MontoRetencion,"
   vSQL = vSQL & "MontoOriginal,"
   vSQL = vSQL & "MontoBaseImponible,"
   vSQL = vSQL & "Sustraendo,"
   vSQL = vSQL & "TipoDePago,"
   vSQL = vSQL & "Tarifa,"
   vSQL = vSQL & "CxP.Fecha AS FechaFact "
   vSQL = vSQL & "From retDocumentoPagado "
   vSQL = vSQL & "INNER JOIN TablaRetencion  ON "
   vSQL = vSQL & "(retDocumentoPagado.CodigoRetencion = TablaRetencion.Codigo) "
   vSQL = vSQL & "INNER JOIN CxP INNER JOIN Pago ON "
   vSQL = vSQL & "(CxP.CodigoProveedor = Pago.CodigoProveedor AND "
   vSQL = vSQL & " Pago.ConsecutivoCompania = cxp.ConsecutivoCompania) ON "
   vSQL = vSQL & "(CxP.Numero = retDocumentoPagado.NumeroDelDocumento  AND "
   vSQL = vSQL & " CxP.ConsecutivoCompania = retDocumentoPagado.ConsecutivoCompania) "
   
    vSQL = vSQL & vSQLWhere
    
  fSQLComprobanteRetencionEnCxP = vSQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: fSQLComprobanteRetencionEnCxP = ""
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLComprobanteRetencionEnCxP", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLComprobanteRetencionImpuestoMunicipalEnCxP(ByVal valConsecutivoCompania As Long, ByVal valConsecutivoCxp As Long, ByVal valTieneContabilidadActiva As Boolean, ByVal valConsecutivoPeriodo) As String
   Dim vSQL As String
   Dim vSQLWhere As String
   Dim vCxPVista As clsCxPVista
   On Error GoTo h_ERROR
   Set vCxPVista = New clsCxPVista
    vSQLWhere = gUtilSQL.fSQLNumberValueWithAnd("", "ConsecutivoCxp", valConsecutivoCxp)
    vSQLWhere = gUtilSQL.fSQLNumberValueWithAnd(vSQLWhere, "dbo.COMPANIA.ConsecutivoCompania", valConsecutivoCompania)
    If (valTieneContabilidadActiva And valConsecutivoPeriodo > 0) Then
        vSQLWhere = gUtilSQL.fSQLNumberValueWithAnd(vSQLWhere, "PERIODO.ConsecutivoPeriodo", valConsecutivoPeriodo)
    End If
    vSQLWhere = gUtilSQL.getWhereSQL(vSQLWhere)
    vSQL = ""
    vSQL = "SELECT "
    vSQL = vSQL & "Comun.Gv_Municipio_B1.NombreMunicipio,"
    vSQL = vSQL & "NumeroComprobanteImpuestoMunicipal AS NumeroComprobante,"
    vSQL = vSQL & vCxPVista.GetViewNameRetencionImpuestoMunicipalCxP() & ".Numero,"
    vSQL = vSQL & vCxPVista.GetViewNameRetencionImpuestoMunicipalCxP() & ".NombreCompania AS RazonSocialAgente, "
    vSQL = vSQL & vCxPVista.GetViewNameRetencionImpuestoMunicipalCxP() & ".CodigoContribuyente AS NumeroLicenciaActividadEconominca,"
    vSQL = vSQL & "RifCompaniaModelo1 AS NumeroRifAgente,"
    vSQL = vSQL & vCxPVista.GetViewNameRetencionImpuestoMunicipalCxP() & ".Fecha AS FechaCxp,"
    vSQL = vSQL & vCxPVista.GetViewNameRetencionImpuestoMunicipalCxP() & ".Direccion AS DireccionAgente,"
    vSQL = vSQL & "NombreProveedor AS RazonSocialSujetoRetenido,"
    vSQL = vSQL & vCxPVista.GetViewNameRetencionImpuestoMunicipalCxP() & ".CodigoContribuyenteProveedor AS  NumeroLicenciaActividadEconomincaRetenido,"
    vSQL = vSQL & "RifProveedorModelo1 AS NumeroRifAgenteSujetoRetenido, Telefono1 AS TelefonoAgenteSujetoRetenido, "
    vSQL = vSQL & gUtilSQL.fYear("FechaAplicacionImpuestoMunicipal", "YEAR") & ", "
    vSQL = vSQL & gUtilSQL.fSQLFillRightWithCaracter(gUtilSQL.fCast(gUtilSQL.fMonth("FechaAplicacionImpuestoMunicipal", ""), eTDSS_VARCHAR, ""), 2, "0") & gUtilSQL.fAlias("MES") & ", "
    vSQL = vSQL & "DireccionProveedor  AS   DireccionSujetoRetenido,"
    vSQL = vSQL & fTipoDeOperacion() & ", "
    vSQL = vSQL & vCxPVista.GetViewNameRetencionImpuestoMunicipalCxP() & ".Fecha  AS FechaCxpDetalle,"
    vSQL = vSQL & gUtilSQL.getIIF("TipoDeCxP = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeTransaccion.eTD_FACTURA), vCxPVista.GetViewNameRetencionImpuestoMunicipalCxP() & ".Numero", gUtilSQL.fSimpleSqlValue(""), True) & " AS NumeroCxp, "
    vSQL = vSQL & "NumeroControl AS NumeroControlFactura,"
    vSQL = vSQL & gUtilSQL.getIIF("TipoDeCxP = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeTransaccion.eTD_NOTADEDEBITO), vCxPVista.GetViewNameRetencionImpuestoMunicipalCxP() & ".Numero", gUtilSQL.fSimpleSqlValue(""), True) & " AS NumeroDeNotaDebito, "
    vSQL = vSQL & gUtilSQL.getIIF("TipoDeCxP = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeTransaccion.eTD_NOTADECREDITO), vCxPVista.GetViewNameRetencionImpuestoMunicipalCxP() & ".Numero", gUtilSQL.fSimpleSqlValue(""), True) & " AS NumeroDeNotaCREDITO, "
    vSQL = vSQL & fTipoDeTransaccion() & " , "
    vSQL = vSQL & "NumeroDeFacturaAfectada AS NumeroDeLaFacturaAfectada,"
    vSQL = vSQL & "MontoBaseImponible AS TotalComprasSinIVABaseImponible,"
    vSQL = vSQL & "AlicuotaRetencion AS  ALICUOTA,"
    vSQL = vSQL & "CodigoActividad AS GRUPO ,"
    vSQL = vSQL & "MontoRetencion  As ImpuestoMunicipalRetenido, "
    vSQL = vSQL & "NumeroRUC  As NumeroRUC, "
    vSQL = vSQL & "FechaAplicacionImpuestoMunicipal, "
    vSQL = vSQL & "MontoExento  As MontoExentoCxp, "
    vSQL = vSQL & "MontoGravado  As MontoGravadoCxp, "
    vSQL = vSQL & "MontoIva  As MontoIvaCxp, "
    vSQL = vSQL & "MontoExento +  MontoGravado + MontoIva  As MontoTotalFacturadoCxp, "
    vSQL = vSQL & vCxPVista.GetViewNameRetencionImpuestoMunicipalCxP() & ".Descripcion  As DescripcionActividad, "
    vSQL = vSQL & "Comprobante.Numero as NroComprobanteCxp "
    vSQL = vSQL & " FROM dbo.COMPANIA "
    vSQL = vSQL & " INNER JOIN " & vCxPVista.GetViewNameRetencionImpuestoMunicipalCxP() & " ON "
    vSQL = vSQL & " dbo.COMPANIA.ConsecutivoCompania = " & vCxPVista.GetViewNameRetencionImpuestoMunicipalCxP() & ".ConsecutivoCompania "
    vSQL = vSQL & " INNER JOIN Comun.Gv_MunicipioCiudad_B1 ON "
    vSQL = vSQL & " dbo.COMPANIA.ConsecutivoMunicipio = Comun.Gv_MunicipioCiudad_B1.Consecutivo "
    vSQL = vSQL & " INNER JOIN Comun.Gv_Municipio_B1 ON "
    vSQL = vSQL & " Comun.Gv_MunicipioCiudad_B1.CodigoMunicipio = Comun.Gv_Municipio_B1.Codigo "
    vSQL = vSQL & " LEFT OUTER JOIN PERIODO ON"
    vSQL = vSQL & " dbo.COMPANIA.ConsecutivoCompania = PERIODO.ConsecutivoCompania "
    vSQL = vSQL & " LEFT OUTER JOIN COMPROBANTE ON"
    vSQL = vSQL & " PERIODO.ConsecutivoPeriodo = COMPROBANTE.ConsecutivoPeriodo AND "
    vSQL = vSQL & " COMPROBANTE.ConsecutivoDocOrigen = " & vCxPVista.GetViewNameRetencionImpuestoMunicipalCxP() & ".ConsecutivoCxp "
    vSQL = vSQL & vSQLWhere
    fSQLComprobanteRetencionImpuestoMunicipalEnCxP = vSQL
    Set vCxPVista = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: fSQLComprobanteRetencionImpuestoMunicipalEnCxP = ""
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLComprobanteRetencionImpuestoMunicipalEnCxP", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fTipoDeTransaccion() As String
 Dim gEnumProyecto As clsEnumAdministrativo
   Dim vSQL As String
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
   vSQL = gUtilSQL.DfSQLCaseIf(vCxPVista.GetViewNameRetencionImpuestoMunicipalCxP() & ".TipoDeTransaccionImpuesto", vCadenaComparacion, vCadenaResultadoStr, gTexto.fSeparadorStandardDeElementosString, "=", "TipoDeTransaccion")
   Set gEnumProyecto = Nothing
   Set vCxPVista = Nothing
  fTipoDeTransaccion = vSQL
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fTipoDeTransaccion", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
 
Private Function fTipoDeOperacion() As String
   Dim gEnumProyecto As clsEnumAdministrativo
   Dim vCxPVista As clsCxPVista
   Dim vSQL As String
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
   vSQL = gUtilSQL.DfSQLCaseIf(vCxPVista.GetViewNameRetencionImpuestoMunicipalCxP() & ".TipoDeCxp ", vCadenaComparacion, vCadenaResultadoStr, gTexto.fSeparadorStandardDeElementosString, "=", "TipoDeOperacion ")
   Set gEnumProyecto = Nothing
   Set vCxPVista = Nothing
   fTipoDeOperacion = vSQL
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fTipoDeOperacion", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSqlWhereArchivoDeTextoRetencionImpuestos(ByVal valAno As Long, ByVal valMes As Long, ByVal valConsecutivoCompania As Long) As String
   Dim vResult As String
   Dim vSQLWhere As String
   Dim vSQL As String
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
   vSQLWhere = vCxPVista.GetViewNameRetencionImpuestoMunicipalCxP() & ".Status IN (" & vSqlStatusCxP & ")"
   vSQLWhere = gUtilSQL.fSQLValueDatesBetween(vSQLWhere, "FechaAplicacionImpuestoMunicipal", vFechaDesde, vFechaHasta)
   vSQLWhere = gUtilSQL.fSQLNumberValueWithAnd(vSQLWhere, "ConsecutivoCompania", valConsecutivoCompania)
   vSQLWhere = gUtilSQL.getWhereSQL(vSQLWhere)
   
   vSQL = vSQL & vSQLWhere
   vResult = vSQL
   fSqlWhereArchivoDeTextoRetencionImpuestos = vResult
   Set vCxPVista = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlWhereArchivoDeTextoRetencionImpuestos", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLResumenRetencionesActividadesEconomicas(ByVal valAno As Long, ByVal valMes As Long, ByVal valConsecutivoCompania As Long) As String
   Dim vSQL As String
   Dim vSQLWhere As String
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
    vSQLWhere = vCxPVista.GetViewNameRetencionImpuestoMunicipalCxP() & ".Status IN (" & vSqlStatusCxP & ")"
    vSQLWhere = gUtilSQL.fSQLValueDatesBetween(vSQLWhere, "FechaAplicacionImpuestoMunicipal", vFechaDesde, vFechaHasta)
    vSQLWhere = gUtilSQL.fSQLNumberValueWithAnd(vSQLWhere, vCxPVista.GetViewNameRetencionImpuestoMunicipalCxP() & ".ConsecutivoCompania", valConsecutivoCompania)
    vSQLWhere = gUtilSQL.getWhereSQL(vSQLWhere)
    vSQL = ""
    vSQL = "SELECT "
    vSQL = vSQL & "Comun.Gv_Municipio_B1.NombreMunicipio,"
    vSQL = vSQL & vCxPVista.GetViewNameRetencionImpuestoMunicipalCxP() & ".CodigoContribuyente,"
    vSQL = vSQL & "NombreCompania AS RazonSocialAgente, "
    vSQL = vSQL & vCxPVista.GetViewNameRetencionImpuestoMunicipalCxP() & ".Direccion AS DireccionAgente,"
    vSQL = vSQL & "RifCompaniaModelo3 AS NumeroRifAgente,"
    vSQL = vSQL & "RifProveedorModelo3 AS NumeroRifAgenteSujetoRetenido,"
    vSQL = vSQL & "NombreProveedor AS RazonSocialSujetoRetenido,"
    vSQL = vSQL & "Fecha,"
    vSQL = vSQL & "FechaAplicacionImpuestoMunicipal,"
    vSQL = vSQL & "NumeroControl AS NumeroControlFactura,"
    vSQL = vSQL & "Numero AS NumeroCxp,"
    vSQL = vSQL & gUtilSQL.fYear("FechaAplicacionImpuestoMunicipal", "YEAR") & ", "
    vSQL = vSQL & gUtilSQL.fMonth("FechaAplicacionImpuestoMunicipal", "MES") & ","
    vSQL = vSQL & "((MontoExento+MontoGravado+MontoIva+TotalOtrosImpuestos) * CambioABolivares) AS CantidadPagada,"
    vSQL = vSQL & "MontoBaseImponible,"
    vSQL = vSQL & "AlicuotaRetencion,"
    vSQL = vSQL & "MontoRetencion,"
    vSQL = vSQL & gUtilSQL.fNumToStrSQL(1) & " AS Contador, "
    vSQL = vSQL & "CodigoActividad, "
    vSQL = vSQL & "NumeroRuc, "
    vSQL = vSQL & "ROW_NUMBER() OVER (ORDER BY Comun.Gv_Municipio_B1.NombreMunicipio) AS NumeroConsecutivo, "
    vSQL = vSQL & "(IGV_RetencionImpuestoMunicipalCxP.MontoExento + IGV_RetencionImpuestoMunicipalCxP.MontoGravado + IGV_RetencionImpuestoMunicipalCxP.MontoIva) AS MontoTotal, "
    vSQL = vSQL & " IGV_RetencionImpuestoMunicipalCxP.MontoExento, "
    vSQL = vSQL & " IGV_RetencionImpuestoMunicipalCxP.NumeroComprobanteImpuestoMunicipal"
    vSQL = vSQL & " FROM dbo.COMPANIA "
    vSQL = vSQL & " INNER JOIN " & vCxPVista.GetViewNameRetencionImpuestoMunicipalCxP() & " ON "
    vSQL = vSQL & " dbo.COMPANIA.ConsecutivoCompania = " & vCxPVista.GetViewNameRetencionImpuestoMunicipalCxP() & ".ConsecutivoCompania "
    vSQL = vSQL & " INNER JOIN Comun.Gv_MunicipioCiudad_B1 ON "
    vSQL = vSQL & " dbo.COMPANIA.ConsecutivoMunicipio = Comun.Gv_MunicipioCiudad_B1.Consecutivo "
    vSQL = vSQL & " INNER JOIN Comun.Gv_Municipio_B1 ON "
    vSQL = vSQL & " Comun.Gv_MunicipioCiudad_B1.CodigoMunicipio = Comun.Gv_Municipio_B1.Codigo "
    vSQL = vSQL & " "
    vSQL = vSQL & vSQLWhere
    fSQLResumenRetencionesActividadesEconomicas = vSQL
    Set vCxPVista = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: fSQLResumenRetencionesActividadesEconomicas = ""
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLResumenRetencionesActividadesEconomicas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLPagoMasivoDeDetracciones(ByVal valAno As Long, ByVal valMes As Long, ByVal valConsecutivoCompania As Long) As String
   Dim vSQL As String
   On Error GoTo h_ERROR
   vSQL = "SELECT " & gUtilSQL.fSimpleSqlValue("6") & " AS TipoDocumentoProveedor,"
   vSQL = vSQL & " Adm.Proveedor.NumeroRIF AS NumeroRUC, Adm.Proveedor.NombreProveedor AS NombreProveedor, "
   vSQL = vSQL & gUtilSQL.fSimpleSqlValue(gTexto.nCar("0", 9)) & " AS NumProforma, OtrosImpuestosCxP.CodigoOI AS CodigoDetraccion,"
   vSQL = vSQL & " Adm.Proveedor.NumeroCuentaBancaria AS NumeroCuentaBanco, "
   vSQL = vSQL & " (OtrosImpuestosCxP.Monto * cxP.CambioAbolivares) AS MontoDeposito, "
   vSQL = vSQL & gUtilSQL.fSimpleSqlValue("01") & " AS TipoOperacion, " ' ojo una vez definido en formulario de peru hacer case
   vSQL = vSQL & gUtilSQL.fSimpleSqlValue(gConvert.fConvierteAString(valAno) & gTexto.llenaConCaracterALaIzquierda(gConvert.fConvierteAString(valMes), "0", 2)) & " AS PeriodoTributario, "
   vSQL = vSQL & gUtilSQL.getIIF("cxP.TipoDeCxp = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeTransaccion.eTD_BOLETADEVENTA), _
            gUtilSQL.fSimpleSqlValue("03"), gUtilSQL.fSimpleSqlValue("01"), True) & " AS TipoComprobante, "
   vSQL = vSQL & "cxP.numero AS Comprobante"
   vSQL = vSQL & " FROM cxP INNER JOIN Adm.Proveedor ON cxP.codigoproveedor = Adm.Proveedor.CodigoProveedor"
   vSQL = vSQL & " AND cxp.ConsecutivoCompania = Adm.Proveedor.ConsecutivoCompania"
   vSQL = vSQL & " INNER JOIN OtrosImpuestosCxP ON cxp.ConsecutivoCxp = OtrosImpuestosCxP.ConsecutivoCxP"
   vSQL = vSQL & " AND cxP.ConsecutivoCompania = OtrosImpuestosCxP.ConsecutivoCompania"
   vSQL = vSQL & " WHERE cxP.ConsecutivoCompania = " & gUtilSQL.fSimpleSqlValueForNumeric(valConsecutivoCompania)
   vSQL = vSQL & " AND cxP.MesDeAplicacion = " & gUtilSQL.fSimpleSqlValueForNumeric(valMes)
   vSQL = vSQL & " AND cxP.AnoDeAplicacion = " & gUtilSQL.fSimpleSqlValueForNumeric(valAno)
   vSQL = vSQL & " AND cxP.Status = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_CANCELADO)
   fSQLPagoMasivoDeDetracciones = vSQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: fSQLPagoMasivoDeDetracciones = ""
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLPagoMasivoDeDetracciones", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
