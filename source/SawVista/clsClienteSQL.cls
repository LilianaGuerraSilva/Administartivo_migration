VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsClienteSQL"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit

Private Const CM_FILE_NAME As String = "clsClienteSQL"
Private Const CM_MESSAGE_NAME As String = "SQL Cliente"

Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function

Public Function getCodigoClienteResumen() As String
   getCodigoClienteResumen = "RD_Cliente"
End Function

Public Function fSqlFichaCliente(ByVal valConsecutivoCompania As Long, ByVal valcodigoCliente As String) As String
   Dim vSQL As String
   Dim insClienteVista As clsClienteVista
   On Error GoTo h_ERROR
   Set insClienteVista = New clsClienteVista
   
   vSQL = ""
   vSQL = "SELECT "
   vSQL = vSQL & "ConsecutivoCompania, "
   vSQL = vSQL & "Codigo, "
   vSQL = vSQL & "NombreCliente, "
   vSQL = vSQL & "Status,"
   vSQL = vSQL & "Direccion, "
   vSQL = vSQL & "Contacto, "
   vSQL = vSQL & "Telefono, "
   vSQL = vSQL & "Fax, "
   vSQL = vSQL & "Ciudad,  "
   vSQL = vSQL & "ZonaPostal, "
   vSQL = vSQL & "NumeroRIF, "
   vSQL = vSQL & "NumeroNIT, "
   vSQL = vSQL & "Email, "
   vSQL = vSQL & "NombreVendedor"
   vSQL = vSQL & " FROM " & insClienteVista.GetViewNameFichaCliente
   vSQL = vSQL & " WHERE ConsecutivoCompania = " & valConsecutivoCompania
   vSQL = vSQL & " AND " & gUtilSQL.fSQLValue("Codigo", Trim(valcodigoCliente), False)
   
   fSqlFichaCliente = vSQL
   Set insClienteVista = Nothing
   
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlFichaCliente", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConstruirSqlDelReporteClientesPorCiudad(ByVal valUsaCodigoClienteEnPantalla As Boolean, ByVal valConsecutivoCompania As Long, _
                                                            ByVal valCantidadAImprimir As Long, ByVal valCiudad As String) As String
   Dim SQL As String
   On Error GoTo h_ERROR
   
   SQL = "SELECT "
   If valUsaCodigoClienteEnPantalla Then
      SQL = SQL & "Cliente.Codigo" & _
                  gUtilSQL.CharConcat & "'-'" & gUtilSQL.CharConcat & _
                  "Cliente.Nombre AS NombreCliente, "
   Else
      SQL = SQL & "Cliente.Nombre AS NombreCliente, "
   End If
   
   SQL = SQL & "Cliente.ZonaDeCobranza AS ZonasCobranzas, "
   SQL = SQL & gUtilSQL.DfUcaseSQL("Cliente.Ciudad") & " AS Ciudades, "
   SQL = SQL & "Cliente.Telefono, "
   SQL = SQL & "Cliente.FAX, "
   SQL = SQL & "Cliente.Contacto"
   SQL = SQL & " FROM Cliente"
   SQL = SQL & " WHERE Cliente.ConsecutivoCompania" & _
               " = " & valConsecutivoCompania
   SQL = SQL & " AND Cliente.Codigo <> " & gUtilSQL.fSimpleSqlValue(getCodigoClienteResumen)
   If valCantidadAImprimir = 0 Then
      SQL = SQL & " AND Cliente.Ciudad" & _
                  " = '" & Trim(valCiudad) & "'"
      SQL = SQL & " GROUP BY Cliente.Ciudad, "
      If valUsaCodigoClienteEnPantalla Then
         SQL = SQL & "Cliente.Codigo, "
      End If
      
      SQL = SQL & "Cliente.Nombre, "
      SQL = SQL & "Cliente.ZonaDeCobranza, "
      SQL = SQL & "Cliente.Telefono, "
      SQL = SQL & "Cliente.FAX, "
      SQL = SQL & "Cliente.Contacto"
      SQL = SQL & " ORDER BY Cliente.Ciudad"
   Else
      SQL = SQL & " GROUP BY Cliente.Ciudad,"
      If valUsaCodigoClienteEnPantalla Then
         SQL = SQL & "Cliente.Codigo" & _
            gUtilSQL.CharConcat & "'-'" & gUtilSQL.CharConcat & _
            "Cliente.Nombre, "
      Else
         SQL = SQL & "Cliente.Nombre, "
      End If
      SQL = SQL & "Cliente.ZonaDeCobranza, "
      SQL = SQL & "Cliente.Telefono, "
      SQL = SQL & "Cliente.FAX, "
      SQL = SQL & "Cliente.Contacto"
      SQL = SQL & " ORDER BY Cliente.Ciudad"
   End If
   fConstruirSqlDelReporteClientesPorCiudad = SQL
   
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fConstruirSqlDelReporteClientesPorCiudad", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConstruirSqlDelReporteClienesPorZona(ByVal valUsaCodigoClienteEnPantalla As Boolean, ByVal valCantidadAImprimir As Long, ByVal valConsecutivoCompania As Long, ByVal valZonaCobranza As String) As String
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = " SELECT "
   If valUsaCodigoClienteEnPantalla Then
      SQL = SQL & " Cliente.Codigo " & gUtilSQL.CharConcat & " '-' " & gUtilSQL.CharConcat & " Cliente.Nombre as NombreCliente, "
   Else
      SQL = SQL & " Cliente.Nombre as NombreCliente, "
   End If
   SQL = SQL & " Cliente.NumeroRif as NumeroDeRif, "
   SQL = SQL & "cliente.ZonaDeCobranza  as ZonasCobranzas, "
   SQL = SQL & "Cliente.Direccion, "
   SQL = SQL & "Cliente.Telefono, "
   SQL = SQL & "Cliente.Fax, Cliente.Contacto "
   SQL = SQL & " FROM Cliente "
   SQL = SQL & " WHERE Cliente.ConsecutivoCompania = " & valConsecutivoCompania
   SQL = SQL & " AND cliente.codigo <> " & gUtilSQL.fSimpleSqlValue(getCodigoClienteResumen)
   If valCantidadAImprimir = 0 Then
      If valZonaCobranza <> "TODAS" Then
          SQL = SQL & " AND Cliente.ZonaDeCobranza ='" & Trim(valZonaCobranza) & "'"
      End If
   End If
   SQL = SQL & " GROUP BY "
   If valUsaCodigoClienteEnPantalla Then
      SQL = SQL & " Cliente.Codigo, Cliente.Nombre, "
   Else
      SQL = SQL & " Cliente.Nombre,  "
   End If
   SQL = SQL & " Cliente.NumeroRif, "
   SQL = SQL & "cliente.ZonaDeCobranza, "
   SQL = SQL & "Cliente.Direccion, "
   SQL = SQL & "Cliente.Telefono, "
   SQL = SQL & "Cliente.Fax, "
   SQL = SQL & "Cliente.Contacto "
   SQL = SQL & "ORDER BY Cliente.ZonaDeCobranza, "
   SQL = SQL & "Cliente.Nombre"
   fConstruirSqlDelReporteClienesPorZona = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fConstruirSqlDelReporteClienesPorZona", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConstruirSqlDelReporteListadoDeClientes(ByVal valOrdenadoPor As String, ByVal valEscogerCompaniaToString As String, ByVal valConsecutivoCompania As Long, ByVal valFiltrarPorStatus As Boolean, ByVal valStatus As String, Optional ByVal valIncluirEmail As Boolean) As String
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = "SELECT "
   If valOrdenadoPor = valEscogerCompaniaToString Then
      SQL = SQL & "cliente.Codigo" & _
               gUtilSQL.CharConcat & "'-'" & gUtilSQL.CharConcat & _
               "cliente.Nombre AS NombreCliente, "
   Else
      SQL = SQL & "cliente.Nombre" & _
            gUtilSQL.CharConcat & "'-'" & gUtilSQL.CharConcat & _
            "cliente.Codigo" & _
            " AS NombreCliente, "
   End If
   SQL = SQL & "cliente.NumeroRif, "
   SQL = SQL & "cliente.ZonaDeCobranza AS ZonasCobranzas, "
   SQL = SQL & gUtilSQL.DfUcaseSQL("cliente.Ciudad") & " AS Ciudades, "
   SQL = SQL & "cliente.Telefono, "
   SQL = SQL & "cliente.FAX, "
   SQL = SQL & "cliente.Contacto"
   If valIncluirEmail Then
   SQL = SQL & ", cliente.Email"
   End If
   
   SQL = SQL & " FROM cliente"
   SQL = SQL & " WHERE "
   SQL = SQL & "cliente.ConsecutivoCompania" & _
               " = " & valConsecutivoCompania
   If valFiltrarPorStatus Then
   SQL = SQL & " AND cliente.Status = " & gUtilSQL.fSimpleSqlValue(valStatus)
   End If
   SQL = SQL & " AND cliente.Codigo <> " & gUtilSQL.fSimpleSqlValue(getCodigoClienteResumen)
   SQL = SQL & " ORDER BY "
   If valOrdenadoPor = valEscogerCompaniaToString Then
      SQL = SQL & "cliente.Codigo"
   Else
      SQL = SQL & "cliente.Nombre"
   End If
   fConstruirSqlDelReporteListadoDeClientes = SQL
   
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fConstruirSqlDelReporteListadoDeClientes", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConstruirSqlDelReporteCumpleanosClientes(ByVal valConsecutivoCompania As Long, ByVal valMesesDelAnoToNum As Long) As String
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = " SELECT "
   SQL = SQL & "cliente.Nombre AS NombreCliente, "
   SQL = SQL & "cliente.Telefono AS Telefono, "
   SQL = SQL & "cliente.DiaCumpleanos AS DiaCumpleano, "
   SQL = SQL & "cliente.Email AS Correo "
   SQL = SQL & " FROM Cliente"
   SQL = SQL & " WHERE cliente.ConsecutivoCompania=" & valConsecutivoCompania
   SQL = SQL & " AND cliente.MesCumpleanos=" & valMesesDelAnoToNum
   SQL = SQL & " ORDER BY cliente.DiaCumpleanos"
   fConstruirSqlDelReporteCumpleanosClientes = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fConstruirSqlDelReporteCumpleanosClientes", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConstruirSQLDelReporteCotFactLlamPorCliente(ByVal valConsecutivoCompania As Long, ByVal valCodigo As String) As String
   Dim conjuntoDeNumeroDeCotizaciones As String
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = "SELECT TOP 10 Cotizacion.Numero"
   SQL = SQL & " FROM Cotizacion"
   SQL = SQL & " WHERE "
   SQL = SQL & "Cotizacion.CONSECUTIVOCOMPANIA = " & valConsecutivoCompania
   SQL = SQL & " AND Cotizacion.CODIGOCLIENTE  = '" & Trim(valCodigo) & "'"
   SQL = SQL & " ORDER BY Cotizacion.FECHA  DESC "
   conjuntoDeNumeroDeCotizaciones = gDbUtil.fBuildResultSetAsString(SQL)
   If conjuntoDeNumeroDeCotizaciones = "" Then
      conjuntoDeNumeroDeCotizaciones = "'0'"
   End If
   SQL = "SELECT "
   SQL = SQL & "cotizacion.FECHA, "
   SQL = SQL & "cotizacion.NUMERO , "
   SQL = SQL & "cotizacion.TotalCotizacion, "
   SQL = SQL & "cotizacion.NOMBREOPERADOR , "
   SQL = SQL & "RenglonCotizacion.CodigoArticulo , "
   SQL = SQL & "RenglonCotizacion.CANTIDAD"
   SQL = SQL & " FROM cotizacion INNER JOIN RenglonCotizacion"
   SQL = SQL & " ON (cotizacion.NUMERO  " & _
               " =RenglonCotizacion.NumeroCotizacion"
   SQL = SQL & ") AND (cotizacion.CONSECUTIVOCOMPANIA " & _
               " = RenglonCotizacion.CONSECUTIVOCOMPANIA )"
   SQL = SQL & " WHERE cotizacion.NUMERO " & _
               " IN(" & conjuntoDeNumeroDeCotizaciones & ")"
   SQL = SQL & " AND cotizacion.CONSECUTIVOCOMPANIA " & _
               " = " & valConsecutivoCompania
   SQL = SQL & " ORDER BY cotizacion.FECHA DESC "
   fConstruirSQLDelReporteCotFactLlamPorCliente = SQL
   
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: SQL = ""
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fConstruirSQLDelReporteCotFactLlamPorCliente", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConstruirSqlDelReporteClientesPorEnviarCorrespondencia(ByVal valConsecutivoCompania As Long, ByVal valCorrespondenciaXEnviar As String) As String
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = " SELECT "
   SQL = SQL & "Cliente.Nombre AS NombreCliente, "
   SQL = SQL & "Cliente.Telefono AS Telefono, "
   SQL = SQL & "Cliente.Direccion AS Direccion, "
   SQL = SQL & "Cliente.Email AS Correo "
   SQL = SQL & " FROM Cliente"
   SQL = SQL & " WHERE Cliente.ConsecutivoCompania = " & valConsecutivoCompania
   SQL = SQL & " AND Cliente.CorrespondenciaXEnviar = " & valCorrespondenciaXEnviar
   fConstruirSqlDelReporteClientesPorEnviarCorrespondencia = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fConstruirSqlDelReporteClientesPorEnviarCorrespondencia", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function




'*************************************ESTO ES EL PROYECTO ORIGINAL DEL HEAD********************************************
Public Function fSQLEstadoDeCuenta(ByVal ReporteEnMonedaLocal As Boolean, ByVal usarCambioOriginal As Boolean, _
                                 ByVal valConsecutivoCompania As String, ByVal valImprimirUnCliente As Boolean, _
                                 ByVal valtxtCodigoCliente As String, ByVal gMonedaLocalActual As Object, _
                                 ByVal gUltimaTasaDeCambio As Object _
                                 , ByVal valMostrarInformacionCamposDef As Boolean) As String
   Dim SQL As String
   Dim sqlTipoDeCxC As String
   Dim sqlTipoDeAnticipo As String
   Dim sqlDateDiffCxC As String
   Dim sqlMontoCxC As String
   Dim sqlMontoAnticipo As String
   Dim sqlStatusCxC As String
   Dim sqlStatusAnticipo As String
   Dim sqlCambioBsCxC As String
   Dim sqlCambioBsAnticipo As String
   Dim sqlMonedaCxC As String
   Dim sqlMonedaAnticipo As String
   Dim sqlMontoBsCxC As String
   Dim sqlMontoExtranjeroCxC As String
   Dim sqlMontoBsAnticipo As String
   Dim sqlMontoExtranjeroAnticipo As String
   Dim gEnumProyecto As clsEnumAdministrativo
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   sqlTipoDeCxC = gUtilSQL.DfSQLCaseIfForEnum("CxC.TipoCxC", enum_TipoDeTransaccion.eTD_FACTURA, gEnumProyecto.fEnumTipoDeTransaccioToStringInArray(True), "")
   Set gEnumProyecto = Nothing
   sqlTipoDeAnticipo = gUtilSQL.fSimpleSqlValue("Cobrado")
   sqlDateDiffCxC = gUtilSQL.getDateDiff("d", "CxC.FechaVencimiento", gUtilSQL.fDateToSQLValue(gUtilDate.getFechaDeHoy))
   sqlMontoCxC = "((CxC.MontoExento + CxC.MontoGravado + CxC.MontoIVA) - CxC.MontoAbonado)"
   sqlMontoAnticipo = "(Anticipo.MontoTotal - (Anticipo.MontoUsado + Anticipo.MontoDevuelto + Anticipo.MontoDiferenciaEnDevolucion))"
   sqlCambioBsCxC = "CxC.CambioABolivares"
   sqlCambioBsAnticipo = "Anticipo.Cambio"
   sqlMonedaCxC = "CxC.Moneda"
   sqlMonedaAnticipo = "Anticipo.Moneda"
   sqlStatusCxC = gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ABONADO) & ", " & _
                  gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_PORCANCELAR)
   sqlStatusAnticipo = gUtilSQL.fSQLSimpleValueForEnum(enum_StatusAnticipo.eSDA_PARCIALMENTE_DEVUELTO) & ", " & _
                  gUtilSQL.fSQLSimpleValueForEnum(enum_StatusAnticipo.eSDA_PARCIALMENTE_USADO) & ", " & _
                  gUtilSQL.fSQLSimpleValueForEnum(enum_StatusAnticipo.eSDA_VIGENTE)
   If ReporteEnMonedaLocal Then
      'Moneda Local
      If usarCambioOriginal Then
         sqlCambioBsCxC = gMonedaLocalActual.fSQLConvierteMontoSiAplicaMasDecimales(gMonedaLocalActual.GetHoyCodigoMoneda, sqlCambioBsCxC, "CxC.Fecha")
         sqlCambioBsAnticipo = gMonedaLocalActual.fSQLConvierteMontoSiAplicaMasDecimales(gMonedaLocalActual.GetHoyCodigoMoneda, sqlCambioBsAnticipo, "Anticipo.Fecha")
      Else
         sqlCambioBsCxC = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("", sqlMonedaCxC, 1, usarCambioOriginal, "")
         sqlCambioBsAnticipo = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("", sqlMonedaAnticipo, 1, usarCambioOriginal, "")
      End If
      sqlMontoBsCxC = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, sqlMontoCxC, "CxC.Fecha")
      sqlMontoExtranjeroCxC = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCambioBsCxC, sqlMonedaCxC, sqlMontoCxC, usarCambioOriginal, "")
      sqlMontoCxC = gUtilSQL.getIIF("(CxC.CodigoMoneda = " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMoneda) & ") OR " & _
                                   "(CxC.CodigoMoneda = " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMonedaAnt) & ")", _
                                   sqlMontoBsCxC, sqlMontoExtranjeroCxC, True)
      sqlMontoBsAnticipo = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, sqlMontoAnticipo, "Anticipo.Fecha")
      sqlMontoExtranjeroAnticipo = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCambioBsAnticipo, sqlMonedaAnticipo, sqlMontoAnticipo, usarCambioOriginal, "")
      sqlMontoAnticipo = gUtilSQL.getIIF("(Anticipo.CodigoMoneda = " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMoneda) & ") OR " & _
                                   "(Anticipo.CodigoMoneda = " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMonedaAnt) & ")", _
                                   sqlMontoBsAnticipo, sqlMontoExtranjeroAnticipo, True)
      sqlMonedaCxC = gUtilSQL.getIIF("CxC.CodigoMoneda = " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMoneda), _
                                 "CxC.Moneda", gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda), True)
      sqlMonedaAnticipo = gUtilSQL.getIIF("Anticipo.CodigoMoneda = " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMoneda), _
                                 "Anticipo.Moneda", gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda), True)
   End If
   
   SQL = "SELECT "
   SQL = SQL & "Cliente.Codigo AS Codigo, "
   SQL = SQL & "Cliente.Nombre AS Nombre, "
   SQL = SQL & "Cliente.Direccion AS Direccion, "
   SQL = SQL & "Cliente.Ciudad AS Ciudad, "
   SQL = SQL & sqlMonedaCxC & " AS Moneda, "
   SQL = SQL & sqlCambioBsCxC & " AS CambioBs, "
   SQL = SQL & gUtilSQL.fSimpleSqlValue("0") & " AS TipoReporte, "
   SQL = SQL & gUtilSQL.fSimpleSqlValue("Cuentas por Cobrar") & " AS TituloTipoReporte, "
   SQL = SQL & sqlTipoDeCxC & " AS TipoDocumento, "
   SQL = SQL & "CxC.Numero AS Numero, "
   SQL = SQL & "CxC.Fecha AS Fecha, "
   SQL = SQL & "CxC.FechaVencimiento AS FechaVencimiento, "
   SQL = SQL & gUtilSQL.getIIF(sqlDateDiffCxC & " > 0 ", sqlDateDiffCxC, "0", True) & " AS DiasVencidos, "
   SQL = SQL & gUtilSQL.getIIF(sqlDateDiffCxC & " > 0 ", sqlMontoCxC, "0", True) & " AS MontoVencido, "
   SQL = SQL & gUtilSQL.getIIF(sqlDateDiffCxC & " <=0 ", sqlMontoCxC, "0", True) & " AS MontoNoVencido, "
   SQL = SQL & " (CxC.MontoExento + CxC.MontoGravado + CxC.MontoIVA)AS MontoOriginal "
   SQL = SQL & ", CxC.NumeroComprobanteFiscal AS NumeroComprobanteFiscal "
   If valMostrarInformacionCamposDef Then
      SQL = SQL & ", ISNULL(camposDefFactura.CampoDefinible1, '') AS  CampoDefinible1, ISNULL(camposDefFactura.CampoDefinible2, '') AS  CampoDefinible2 "
      SQL = SQL & ", ISNULL(camposDefFactura.CampoDefinible3, '') AS  CampoDefinible3, ISNULL(camposDefFactura.CampoDefinible4, '') AS  CampoDefinible4 "
      SQL = SQL & ", ISNULL(camposDefFactura.CampoDefinible5, '') AS  CampoDefinible5, ISNULL(camposDefFactura.CampoDefinible6, '') AS  CampoDefinible6 "
      SQL = SQL & ", ISNULL(camposDefFactura.CampoDefinible7, '') AS  CampoDefinible7, ISNULL(camposDefFactura.CampoDefinible8, '') AS  CampoDefinible8 "
      SQL = SQL & ", ISNULL(camposDefFactura.CampoDefinible9, '') AS  CampoDefinible9, ISNULL(camposDefFactura.CampoDefinible10, '') AS CampoDefinible10 "
      SQL = SQL & ", ISNULL(camposDefFactura.CampoDefinible11, '') AS CampoDefinible11, ISNULL(camposDefFactura.CampoDefinible12, '') AS CampoDefinible12 "
      SQL = SQL & " FROM CxC LEFT JOIN camposDefFactura ON CxC.ConsecutivoCompania = camposDefFactura.ConsecutivoCompania AND CxC.NumeroDocumentoOrigen = camposDefFactura.NumeroFactura AND"
      SQL = SQL & "     (CxC.TipoCxc = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeTransaccion.eTD_FACTURA) & " AND camposDefFactura.TipoDeDocumento = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_FACTURA)  '  -- FACTURA"
      SQL = SQL & "     OR CxC.TipoCxc = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeTransaccion.eTD_NOTADECREDITO) & "AND camposDefFactura.TipoDeDocumento = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_NOTADECREDITO)  ' -- Nota de credito"
      SQL = SQL & "     OR CxC.TipoCxc =" & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeTransaccion.eTD_NOTADEDEBITO) & " AND camposDefFactura.TipoDeDocumento = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_NOTADEDEBITO)   '-- Nota de Debito"
      SQL = SQL & "     OR CxC.TipoCxc = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeTransaccion.eTD_TICKETMAQUINAREGISTRADORA) & " AND camposDefFactura.TipoDeDocumento = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_COMPROBANTEFISCAL)   '  -- Comprobante Fiscal"
      SQL = SQL & "     OR CxC.TipoCxc = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeTransaccion.eTD_NOTADECREDITOCOMPROBANTEFISCAL) & " AND camposDefFactura.TipoDeDocumento = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_COMPROBANTEFISCAL)   '  -- NC Fiscal"
      SQL = SQL & "     OR CxC.TipoCxc = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeTransaccion.eTD_NOTAENTREGA) & " AND camposDefFactura.TipoDeDocumento = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_NOTAENTREGA)  '  -- Nota de Entrega"
      SQL = SQL & " ) INNER JOIN Cliente ON "
      SQL = SQL & " Cliente.ConsecutivoCompania = CxC.ConsecutivoCompania"
      SQL = SQL & " AND Cliente.Codigo = CxC.CodigoCliente"
   Else
      SQL = SQL & " FROM Cliente INNER JOIN CxC ON"
      SQL = SQL & " Cliente.ConsecutivoCompania = CxC.ConsecutivoCompania"
      SQL = SQL & " AND Cliente.Codigo = CxC.CodigoCliente"
   End If
   SQL = SQL & " WHERE Cliente.ConsecutivoCompania = " & valConsecutivoCompania
   SQL = SQL & " AND CxC.Status IN (" & sqlStatusCxC & ")"
   SQL = SQL & " AND Cliente.Codigo <> " & gUtilSQL.fSimpleSqlValue(getCodigoClienteResumen)
   If valImprimirUnCliente Then
      SQL = SQL & " AND Cliente.Codigo = " & gUtilSQL.fSimpleSqlValue(valtxtCodigoCliente)
   End If
   SQL = SQL & " UNION SELECT "
   SQL = SQL & "Cliente.Codigo AS Codigo, "
   SQL = SQL & "Cliente.Nombre AS Nombre, "
   SQL = SQL & "Cliente.Direccion AS Direccion, "
   SQL = SQL & "Cliente.Ciudad AS Ciudad, "
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
   SQL = SQL & sqlMontoAnticipo & " AS MontoNoVencido,"
   SQL = SQL & " Anticipo.MontoTotal AS MontoOriginal   "
   SQL = SQL & ", " & gUtilSQL.fSimpleSqlValue("") & " AS NumeroComprobanteFiscal   "
   If valMostrarInformacionCamposDef Then
      SQL = SQL & ", " & gUtilSQL.fSimpleSqlValue("") & " AS  CampoDefinible1, " & gUtilSQL.fSimpleSqlValue("") & " AS  CampoDefinible2 "
      SQL = SQL & ", " & gUtilSQL.fSimpleSqlValue("") & " AS  CampoDefinible3, " & gUtilSQL.fSimpleSqlValue("") & " AS  CampoDefinible4 "
      SQL = SQL & ", " & gUtilSQL.fSimpleSqlValue("") & " AS  CampoDefinible5, " & gUtilSQL.fSimpleSqlValue("") & " AS  CampoDefinible6 "
      SQL = SQL & ", " & gUtilSQL.fSimpleSqlValue("") & " AS  CampoDefinible7, " & gUtilSQL.fSimpleSqlValue("") & " AS  CampoDefinible8 "
      SQL = SQL & ", " & gUtilSQL.fSimpleSqlValue("") & " AS  CampoDefinible9, " & gUtilSQL.fSimpleSqlValue("") & " AS  CampoDefinible10 "
      SQL = SQL & ", " & gUtilSQL.fSimpleSqlValue("") & " AS  CampoDefinible11, " & gUtilSQL.fSimpleSqlValue("") & " AS  CampoDefinible12 "
   End If
   SQL = SQL & " FROM Cliente INNER JOIN Anticipo ON"
   SQL = SQL & " Cliente.ConsecutivoCompania = Anticipo.ConsecutivoCompania"
   SQL = SQL & " AND Cliente.Codigo = Anticipo.CodigoCliente"
   SQL = SQL & " WHERE Cliente.ConsecutivoCompania = " & valConsecutivoCompania
   SQL = SQL & " AND Anticipo.Status IN (" & sqlStatusAnticipo & ")"
   SQL = SQL & " AND Anticipo.Tipo = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeAnticipo.eTDA_COBRADO)
   SQL = SQL & " AND Cliente.Codigo <> " & gUtilSQL.fSimpleSqlValue(getCodigoClienteResumen)
   If valImprimirUnCliente Then
      SQL = SQL & " AND Cliente.Codigo = " & gUtilSQL.fSimpleSqlValue(valtxtCodigoCliente)
   End If
   SQL = SQL & " AND Anticipo.EsUnaDevolucion = " & gUtilSQL.fBooleanToSqlValue(False)
   SQL = SQL & " ORDER BY Codigo, Moneda, TipoReporte, Fecha"
   fSQLEstadoDeCuenta = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLEstadoDeCuenta", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLDeZona(ByVal valCompaniaActual As Long) As String
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = "SELECT Nombre FROM ZonaCobranza"
   SQL = SQL & " WHERE ConsecutivoCompania = " & valCompaniaActual
h_EXIT: On Error GoTo 0
   fSQLDeZona = SQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLDeZona", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLFaxDeMudanza() As String
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = "SELECT * FROM Cliente"
   SQL = SQL & " WHERE Status =" & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusCliente.eSC_ACTIVO)
   SQL = SQL & " AND FechaUltimaModificacion >= " & gUtilSQL.fDateToSQLValue(gConvert.fConvertStringToDate("28/05/2003"))
   SQL = SQL & " AND " & gUtilSQL.DfTrimSQL("FAX") & "<> '' "
   SQL = SQL & " ORDER BY FAX Desc"
h_EXIT: On Error GoTo 0
   fSQLFaxDeMudanza = SQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLFaxDeMudanza", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSqlListadoDeClientesSinMovimientos(ByVal valConsecutivoCompania As String, ByVal valOrdenadoPor As String) As String
   Dim vSql As String
   Dim gEnumProyecto As clsEnumAdministrativo
   Dim sqlCountFactura As String
   Dim sqlCountCotizacion As String
   Dim sqlCountContrato As String
   Dim sqlCountCxC As String
   Dim sqlCountCobranza As String
   Dim sqlCountAlmacen As String
   Dim sqlCountNotaES As String
   Dim sqlCountAnticipo As String
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   
   sqlCountFactura = "(SELECT " & gUtilSQL.fCOUNT("Factura.CodigoCliente", "") & " FROM Factura WHERE Factura.ConsecutivoCompania = " & valConsecutivoCompania & " AND Factura.CodigoCliente =  Cliente.Codigo) AS T1, "
   sqlCountCotizacion = "(SELECT " & gUtilSQL.fCOUNT("Cotizacion.CodigoCliente", "") & " FROM Cotizacion WHERE Cotizacion.ConsecutivoCompania = " & valConsecutivoCompania & " AND Cotizacion.CodigoCliente =  Cliente.Codigo) AS T2, "
   sqlCountContrato = "(SELECT " & gUtilSQL.fCOUNT("Contrato.CodigoCliente", "") & " FROM Contrato WHERE Contrato.ConsecutivoCompania = " & valConsecutivoCompania & " AND Contrato.CodigoCliente =  Cliente.Codigo) AS T3, "
   sqlCountCxC = "(SELECT " & gUtilSQL.fCOUNT("CxC.CodigoCliente", "") & " FROM CxC WHERE CxC.ConsecutivoCompania = " & valConsecutivoCompania & " AND CxC.CodigoCliente =  Cliente.Codigo) AS T4, "
   sqlCountCobranza = "(SELECT " & gUtilSQL.fCOUNT("Cobranza.CodigoCliente", "") & " FROM Cobranza WHERE Cobranza.ConsecutivoCompania = " & valConsecutivoCompania & " AND Cobranza.CodigoCliente = Cliente.Codigo) AS T5, "
   sqlCountAlmacen = "(SELECT " & gUtilSQL.fCOUNT("Almacen.ConsecutivoCliente", "") & " FROM Almacen WHERE Almacen.ConsecutivoCompania = " & valConsecutivoCompania & " AND Almacen.ConsecutivoCliente =  Cliente.Consecutivo) AS T6, "
   sqlCountNotaES = "(SELECT " & gUtilSQL.fCOUNT("NotaDeEntradaSalida.CodigoCliente", "") & " FROM NotaDeEntradaSalida WHERE NotaDeEntradaSalida.ConsecutivoCompania = " & valConsecutivoCompania & " AND NotaDeEntradaSalida.CodigoCliente =  Cliente.Codigo) AS T7, "
   sqlCountAnticipo = "(SELECT " & gUtilSQL.fCOUNT("Anticipo.CodigoCliente", "") & " FROM Anticipo WHERE Anticipo.ConsecutivoCompania = " & valConsecutivoCompania & " AND Anticipo.Tipo = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeAnticipo.eTDA_COBRADO) & " AND Anticipo.CodigoCliente = Cliente.Codigo) AS T8, "
   
   vSql = ";WITH CTE_CountClientePorModulo AS ("
   vSql = vSql & " SELECT "
   vSql = vSql & sqlCountFactura
   vSql = vSql & sqlCountCotizacion
   vSql = vSql & sqlCountContrato
   vSql = vSql & sqlCountCxC
   vSql = vSql & sqlCountCobranza
   vSql = vSql & sqlCountAlmacen
   vSql = vSql & sqlCountNotaES
   vSql = vSql & sqlCountAnticipo
   vSql = vSql & " Cliente.Codigo AS CodigoCliente, "
   vSql = vSql & " Cliente.Nombre AS NombreCliente, "
   vSql = vSql & " Cliente.Ciudad AS CiudadCliente, "
   vSql = vSql & " Cliente.Telefono AS TelefonoCliente, "
   vSql = vSql & " Cliente.FAX AS FaxCliente, "
   vSql = vSql & " Cliente.Contacto AS ContactoCliente "
   vSql = vSql & " FROM Cliente "
   vSql = vSql & " WHERE "
   vSql = vSql & " Cliente.ConsecutivoCompania" & " = " & valConsecutivoCompania
   vSql = vSql & " AND Cliente.Codigo <> " & gUtilSQL.fSimpleSqlValue("RD_Cliente")
   vSql = vSql & ") "
   
   vSql = vSql & "SELECT "
   vSql = vSql & "CASE WHEN T1 > 0 THEN 1 ELSE 0 END AS T1, "
   vSql = vSql & "CASE WHEN T2 > 0 THEN 1 ELSE 0 END AS T2, "
   vSql = vSql & "CASE WHEN T3 > 0 THEN 1 ELSE 0 END AS T3, "
   vSql = vSql & "CASE WHEN T4 > 0 THEN 1 ELSE 0 END AS T4, "
   vSql = vSql & "CASE WHEN T5 > 0 THEN 1 ELSE 0 END AS T5, "
   vSql = vSql & "CASE WHEN T6 > 0 THEN 1 ELSE 0 END AS T6, "
   vSql = vSql & "CASE WHEN T7 > 0 THEN 1 ELSE 0 END AS T7, "
   vSql = vSql & "CASE WHEN T8 > 0 THEN 1 ELSE 0 END AS T8, "
   vSql = vSql & "CodigoCliente, "
   vSql = vSql & "NombreCliente, "
   vSql = vSql & "CiudadCliente, "
   vSql = vSql & "TelefonoCliente, "
   vSql = vSql & "FaxCliente, "
   vSql = vSql & "ContactoCliente "
   vSql = vSql & " FROM CTE_CountClientePorModulo "
   vSql = vSql & " ORDER BY "
   If valOrdenadoPor = gEnumProyecto.enumFormaDeEscogerCompaniaToString(enum_FormaDeEscogerCompania.eFD_PORCODIGO) Then
      vSql = vSql & " CodigoCliente"
   Else
      vSql = vSql & " NombreCliente"
   End If
   fSqlListadoDeClientesSinMovimientos = vSql
   Set gEnumProyecto = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlListadoDeClientesSinMovimientos", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLUpdateCambiaElNombreDeLaCompania(ByVal valNombreCiudadOriginal As String, ByVal valNuevoNombreCiudad As String, ByVal gProyCompaniaActual As Object) As String
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = "UPDATE cliente "
   SQL = SQL & "SET cliente.Ciudad = '" & valNuevoNombreCiudad & "' "
   SQL = SQL & " WHERE cliente.ConsecutivoCompania = " & gProyCompaniaActual.GetConsecutivoCompania
   SQL = SQL & " AND cliente.Ciudad = '" & valNombreCiudadOriginal & "'"
h_EXIT:
   fSQLUpdateCambiaElNombreDeLaCompania = SQL
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLUpdateCambiaElNombreDeLaCompania", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConstruirSqlDelReporteClientesPorSectorDeNegocio(ByVal valUsaCodigoClienteEnPantalla As Boolean, ByVal valCantidadAImprimir As Long, ByVal valConsecutivoCompania As Long, ByVal valSectorDeNegocio As String) As String
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = " SELECT "
   If valUsaCodigoClienteEnPantalla Then
      SQL = SQL & " Cliente.Codigo " & gUtilSQL.CharConcat & " '-' " & gUtilSQL.CharConcat & " Cliente.Nombre as Nombre, "
   Else
   SQL = SQL & " Cliente.Nombre, "
   End If
   SQL = SQL & " Cliente.NumeroRif, "
   SQL = SQL & " Cliente.Contacto, "
   SQL = SQL & "Cliente.Telefono, "
   SQL = SQL & "Cliente.Fax, "
   SQL = SQL & "cliente.SectorDeNegocio, "
   SQL = SQL & "Cliente.Direccion "
   SQL = SQL & " FROM Cliente "
   SQL = SQL & " WHERE Cliente.ConsecutivoCompania = " & valConsecutivoCompania
   SQL = SQL & " AND cliente.codigo <> " & gUtilSQL.fSimpleSqlValue(getCodigoClienteResumen)
   If valCantidadAImprimir = 0 Then
       If valSectorDeNegocio <> "TODOS" Then
          SQL = SQL & " AND Cliente.SectorDeNegocio ='" & Trim(valSectorDeNegocio) & "'"
      End If
    End If
   SQL = SQL & " GROUP BY "
   If valUsaCodigoClienteEnPantalla Then
      SQL = SQL & " Cliente.Codigo, Cliente.Nombre, "
   Else
      SQL = SQL & " Cliente.Nombre,  "
   End If
   SQL = SQL & " Cliente.NumeroRif, "
   SQL = SQL & "Cliente.SectorDeNegocio, "
   SQL = SQL & "Cliente.Direccion, "
   SQL = SQL & "Cliente.Telefono, "
   SQL = SQL & "Cliente.Fax, "
   SQL = SQL & "Cliente.Contacto "
   SQL = SQL & "ORDER BY Cliente.SectorDeNegocio, "
   SQL = SQL & "Cliente.Nombre"
   fConstruirSqlDelReporteClientesPorSectorDeNegocio = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fConstruirSqlDelReporteClientesPorSectorDeNegocio", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLSectorDeNegocio(ByVal valCompaniaActual As Long) As String
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = "SELECT Descripcion FROM SectorDeNegocio"
h_EXIT: On Error GoTo 0
   fSQLSectorDeNegocio = SQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLSectorDeNegocio", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLInsertarCliente(ByVal valConsecutivoCompania As Long, ByVal valCodigo As String, ByVal valNombre As String, ByVal valNumeroRIF As String, ByVal valNextCodigoCliente As Long, ByVal valCiudadActual As String, ByVal valNombreDelUsuarioActual As String) As String
   Dim SQL As String
   On Error GoTo 0
   SQL = "INSERT INTO Cliente (ConsecutivoCompania, "
   SQL = SQL & "Consecutivo, "
   SQL = SQL & "Codigo, Nombre, "
   SQL = SQL & "Ciudad, Status, "
   SQL = SQL & "ZonaDeCobranza, ConsecutivoVendedor, CodigoVendedor, "
   SQL = SQL & "NumeroRif, ActivarAvisoAlEscoger, "
   SQL = SQL & "CorrespondenciaXenviar, "
   SQL = SQL & "EsExtranjero, "
   SQL = SQL & "NombreOperador, FechaUltimaModificacion, "
   SQL = SQL & "Contacto, Email, "
   SQL = SQL & "Telefono, DiaCumpleanos, "
   SQL = SQL & "MesCumpleanos, "
   SQL = SQL & "Fax, Direccion, "
   SQL = SQL & "ZonaPostal"
   SQL = SQL & " ) VALUES (" & valConsecutivoCompania & ", " & valNextCodigoCliente & ", "
   SQL = SQL & gUtilSQL.fSimpleSqlValue(valCodigo) & ", " & gUtilSQL.fSimpleSqlValue(valNombre) & ", "
   SQL = SQL & gUtilSQL.fSimpleSqlValue(valCiudadActual) & ", " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusCliente.eSC_ACTIVO) & ", "
   SQL = SQL & gUtilSQL.fSimpleSqlValue(valCiudadActual) & ", " & gUtilSQL.fSimpleSqlValueForNumeric("1") & ", " & gUtilSQL.fSimpleSqlValue("00001") & ", "
   SQL = SQL & gUtilSQL.fSimpleSqlValue(valNumeroRIF) & ", " & gUtilSQL.fBooleanToSqlValue(False) & ", " & gUtilSQL.fBooleanToSqlValue(False) & ", "
   SQL = SQL & gUtilSQL.fBooleanToSqlValue(False) & ", "
   SQL = SQL & gUtilSQL.fSimpleSqlValue(valNombreDelUsuarioActual) & ", " & gUtilSQL.fDateToSQLValue(gUtilDate.getFechaDeHoy) & ", "
   SQL = SQL & gUtilSQL.fSimpleSqlValue("") & ", " & gUtilSQL.fSimpleSqlValue("") & ", "
   SQL = SQL & gUtilSQL.fSimpleSqlValue("") & ", " & 0 & ", "
   SQL = SQL & 0 & ", "
   SQL = SQL & gUtilSQL.fSimpleSqlValue("") & ", " & gUtilSQL.fSimpleSqlValue("") & ", "
   SQL = SQL & gUtilSQL.fSimpleSqlValue("") & " )"
h_EXIT: On Error GoTo 0
   fSQLInsertarCliente = SQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLInsertarCliente", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

