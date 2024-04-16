VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsAnticipoSQL"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit
Private Const CM_FILE_NAME As String = "clsAnticipoSQL"
Private Const CM_MESSAGE_NAME As String = "SQL Anticipo"

Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function

Public Function fSQLAnticipoCobradosPagadosEntreFechas(ByVal monedaOriginal As Boolean, _
                                                        ByVal valoptTasaDeCambio As Boolean, _
                                                         ByVal valConsecutivoCompania As String, _
                                                          ByVal valFechaInicial As Date, _
                                                           ByVal valFechaFinal As Date, _
                                                            ByVal valTipoDeAnticipo As enum_TipoDeAnticipo, _
                                                             ByVal ValcmbStatus As String, _
                                                              ByVal gMonedaLocalActual As Object, _
                                                               ByVal gUltimaTasaDeCambio As Object) As String
   Dim SQL As String
   Dim sqlMontoTotal As String
   Dim sqlMontoUsado As String
   Dim sqlMontodevuelto As String
   Dim sqlMontoDiferenciaEnDevolucion As String
   Dim usarTasaOriginal As Boolean
   Dim ReporteEnMonedaLocal As Boolean
   Dim sqlCampoCambioABs As String
   Dim sqlCampoMoneda As String
   Dim gEnumProyecto As clsEnumAdministrativo
   Dim vSQLTipoAnticipo As String
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   If monedaOriginal Then
      ReporteEnMonedaLocal = False
   Else
      ReporteEnMonedaLocal = True
   End If
   If ReporteEnMonedaLocal Then
      usarTasaOriginal = valoptTasaDeCambio
      sqlCampoCambioABs = "Anticipo.Cambio"
      sqlCampoMoneda = "Anticipo.Moneda"

      sqlMontoTotal = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCampoCambioABs, sqlCampoMoneda, _
                     "Anticipo.MontoTotal", usarTasaOriginal, "")
      sqlMontoUsado = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCampoCambioABs, sqlCampoMoneda, _
                     "Anticipo.MontoUsado", usarTasaOriginal, "")
      sqlMontodevuelto = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCampoCambioABs, sqlCampoMoneda, _
                     "Anticipo.MontoDevuelto", usarTasaOriginal, "")
      sqlMontoDiferenciaEnDevolucion = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCampoCambioABs, sqlCampoMoneda, _
                     "Anticipo.MontoDiferenciaEnDevolucion", usarTasaOriginal, "")
                                                                             
      sqlMontoTotal = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, sqlMontoTotal, "Anticipo.fecha")
      sqlMontoUsado = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, sqlMontoUsado, "Anticipo.fecha")
      sqlMontodevuelto = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, sqlMontodevuelto, "Anticipo.fecha")
      sqlMontoDiferenciaEnDevolucion = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, sqlMontoDiferenciaEnDevolucion, "Anticipo.fecha")

   Else
      sqlMontoTotal = gUtilSQL.fRoundNDecimales("Anticipo.MontoTotal")
      sqlMontoUsado = gUtilSQL.fRoundNDecimales("Anticipo.MontoUsado")
      sqlMontodevuelto = gUtilSQL.fRoundNDecimales("Anticipo.MontoDevuelto")
      sqlMontoDiferenciaEnDevolucion = gUtilSQL.fRoundNDecimales("Anticipo.MontoDiferenciaEnDevolucion")
   End If
   
   vSQLTipoAnticipo = " AND Anticipo.Tipo = "
   
   If valTipoDeAnticipo = eTDA_COBRADO Then
      vSQLTipoAnticipo = vSQLTipoAnticipo & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeAnticipo.eTDA_COBRADO)
   Else
      vSQLTipoAnticipo = vSQLTipoAnticipo & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeAnticipo.eTDA_PAGADO)
   End If
   
   vSQLTipoAnticipo = vSQLTipoAnticipo & " "
   
   SQL = "SELECT "
   SQL = SQL & fSQLNombreClienteProveedor(valTipoDeAnticipo) & " As Nombre, "
   SQL = SQL & fSQLCodigoClienteProveedor(valTipoDeAnticipo) & " AS Codigo, "
   If ReporteEnMonedaLocal Then
      SQL = SQL & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda) & " AS moneda, "
   Else
      SQL = SQL & "Anticipo.Moneda as moneda, "
   End If
   SQL = SQL & gUtilSQL.DfSQLCaseIfForEnum("Anticipo.Status", enum_StatusAnticipo.eSDA_VIGENTE, gEnumProyecto.fenumStatusAnticipoToStrinInArray(True), "status") & ", "
   SQL = SQL & "Anticipo.Fecha, "
   SQL = SQL & "Anticipo.Numero, "
   SQL = SQL & sqlMontoTotal & " AS MontoTotal, "
   SQL = SQL & sqlMontoUsado & " AS MontoUsado, "
   SQL = SQL & sqlMontodevuelto & " AS MontoDevuelto, "
   SQL = SQL & sqlMontoDiferenciaEnDevolucion & " AS MontoDiferenciaEnDevolucion,"
   SQL = SQL & "Anticipo.NumeroCheque"
   SQL = SQL & fSQLFromClienteProvedor(valTipoDeAnticipo)
   SQL = SQL & " WHERE "
   SQL = SQL & "Anticipo.ConsecutivoCompania = " & valConsecutivoCompania
   SQL = SQL & " AND " & gUtilSQL.DfSQLDateValueBetween("Anticipo.Fecha", valFechaInicial, valFechaFinal)
   If ValcmbStatus <> gDefgen.getSearchAllComboBox Then
      SQL = SQL & " AND Anticipo.Status = " & gUtilSQL.fSQLSimpleValueForEnum(gEnumProyecto.strStatusAnticipoToNum(ValcmbStatus))
   End If
   SQL = SQL & vSQLTipoAnticipo
   SQL = SQL & " ORDER BY "
   SQL = SQL & "Moneda, "
   SQL = SQL & "Anticipo.Status, "
   SQL = SQL & "Anticipo.Fecha, "
   SQL = SQL & "Anticipo.Numero "
   fSQLAnticipoCobradosPagadosEntreFechas = SQL
   Set gEnumProyecto = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLAnticipoCobradosPagadosEntreFechas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLNombreClienteProveedor(ByVal valTipoDeAnticipo As enum_TipoDeAnticipo) As String
   Dim SQL As String
   On Error GoTo h_ERROR
   If valTipoDeAnticipo = eTDA_COBRADO Then
      SQL = "Cliente.Nombre"
   Else
      SQL = "Proveedor.NombreProveedor"
   End If
   fSQLNombreClienteProveedor = SQL
h_EXIT: On Error GoTo 0
      Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
            "fSQLNombreClienteProveedor", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLCodigoClienteProveedor(ByVal valTipoDeAnticipo As enum_TipoDeAnticipo) As String
   Dim SQL As String
   On Error GoTo h_ERROR
   If valTipoDeAnticipo = eTDA_COBRADO Then
      SQL = "Anticipo.CodigoCliente"
   Else
      SQL = "Anticipo.CodigoProveedor"
   End If
   fSQLCodigoClienteProveedor = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLCodigoClienteProveedor", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLFromClienteProvedor(ByVal valTipoDeAnticipo As enum_TipoDeAnticipo) As String
   Dim SQL As String
   On Error GoTo h_ERROR
   If valTipoDeAnticipo = eTDA_COBRADO Then
      SQL = SQL & " FROM Anticipo"
      SQL = SQL & " INNER JOIN Cliente ON"
      SQL = SQL & " Anticipo.ConsecutivoCompania = Cliente.ConsecutivoCompania"
      SQL = SQL & " AND Anticipo.CodigoCliente = Cliente.Codigo"
   Else
      SQL = SQL & " FROM Anticipo"
      SQL = SQL & " INNER JOIN Proveedor ON"
      SQL = SQL & " Anticipo.ConsecutivoCompania = Proveedor.ConsecutivoCompania"
      SQL = SQL & " AND Anticipo.CodigoProveedor = Proveedor.CodigoProveedor"
   End If
   fSQLFromClienteProvedor = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLFromClienteProvedor", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLAnticipoAsociadosconCotizacion(ByVal ReporteEnMonedaLocal As Boolean, _
                                                    ByVal valoptTasaDeCambio As Boolean, _
                                                     ByVal valConsecutivoCompania As String, _
                                                      ByVal valTipoDeAnticipo As enum_TipoDeAnticipo, _
                                                       ByVal valImprimirUno As Boolean, _
                                                        ByVal valtxtNumeroCotizacion As String, _
                                                         ByVal gMonedaLocalActual As Object, _
                                                          ByVal gUltimaTasaDeCambio As Object) As String
                        
   Dim sqlMontoTotal As String
   Dim sqlMontoUsado As String
   Dim sqlMontodevuelto As String
   Dim sqlMontoDiferenciaEnDevolucion As String
   Dim sqlCampoCambioABs As String
   Dim sqlCampoMoneda As String
   Dim SQL As String
   Dim efecturaCambioABs As Boolean
   Dim usarTasaOriginal As Boolean
   On Error GoTo h_ERROR
   If ReporteEnMonedaLocal Then
      usarTasaOriginal = valoptTasaDeCambio
      sqlCampoCambioABs = "Anticipo.Cambio"
      sqlCampoMoneda = "Anticipo.Moneda"
      
      sqlMontoTotal = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCampoCambioABs, sqlCampoMoneda, _
                     "Anticipo.MontoTotal", usarTasaOriginal, "")
      sqlMontoUsado = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCampoCambioABs, sqlCampoMoneda, _
                     "Anticipo.MontoUsado", usarTasaOriginal, "")
      sqlMontodevuelto = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCampoCambioABs, sqlCampoMoneda, _
                     "Anticipo.MontoDevuelto", usarTasaOriginal, "")
      sqlMontoDiferenciaEnDevolucion = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCampoCambioABs, sqlCampoMoneda, _
                     "Anticipo.MontoDiferenciaEnDevolucion", usarTasaOriginal, "")
                                       
      sqlMontoTotal = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, sqlMontoTotal, "Anticipo.fecha")
      sqlMontoUsado = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, sqlMontoUsado, "Anticipo.fecha")
      sqlMontodevuelto = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, sqlMontodevuelto, "Anticipo.fecha")
      sqlMontoDiferenciaEnDevolucion = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, sqlMontoDiferenciaEnDevolucion, "Anticipo.fecha")
   Else
      sqlMontoTotal = gUtilSQL.fRoundNDecimales("Anticipo.MontoTotal")
      sqlMontoUsado = gUtilSQL.fRoundNDecimales("Anticipo.MontoUsado")
      sqlMontodevuelto = gUtilSQL.fRoundNDecimales("Anticipo.MontoDevuelto")
      sqlMontoDiferenciaEnDevolucion = gUtilSQL.fRoundNDecimales("Anticipo.MontoDiferenciaEnDevolucion")
   End If
   
   If valTipoDeAnticipo = eTDA_COBRADO Then
      SQL = "SELECT "
      SQL = SQL & "Cotizacion.Numero as NumeroCotizacion, "
      SQL = SQL & "Cotizacion.TotalCotizacion, "
      SQL = SQL & "Anticipo.ConsecutivoAnticipo AS numero, "
      SQL = SQL & "cliente.codigo as codigo, "
      SQL = SQL & "cliente.nombre as nombre, "
      If ReporteEnMonedaLocal Then
         SQL = SQL & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda) & " AS moneda, "
      Else
         SQL = SQL & "Anticipo.Moneda AS moneda, "
      End If
      SQL = SQL & "Anticipo.Fecha, "
      SQL = SQL & "Anticipo.NUMERO, "
      SQL = SQL & sqlMontoTotal & " AS MontoTotal, "
      SQL = SQL & sqlMontoUsado & " AS MontoUsado, "
      SQL = SQL & sqlMontodevuelto & " AS MontoDevuelto, "
      SQL = SQL & sqlMontoDiferenciaEnDevolucion & " AS MontoDiferenciaEnDevolucion "
      SQL = SQL & "FROM Anticipo INNER JOIN (Cliente INNER JOIN Cotizacion"
      SQL = SQL & " ON (Cliente.Codigo = cotizacion.CodigoCliente) AND (Cliente.ConsecutivoCompania = cotizacion.ConsecutivoCompania)) ON "
      SQL = SQL & "(anticipo.ConsecutivoCompania = cotizacion.ConsecutivoCompania) AND (anticipo.NumeroCotizacion = cotizacion.Numero)"
      SQL = SQL & " WHERE Anticipo.ConsecutivoCompania = " & valConsecutivoCompania
      If valImprimirUno Then
        SQL = SQL & " AND Anticipo.NumeroCotizacion = " & gUtilSQL.fSimpleSqlValue(valtxtNumeroCotizacion)
      End If
     SQL = SQL & " AND anticipo.status <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusAnticipo.eSDA_ANULADO)
     SQL = SQL & " AND anticipo.status <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusAnticipo.eSDA_COMPLETAMENTE_DEVUELTO)
     SQL = SQL & " ORDER BY "
     SQL = SQL & "MONEDA, "
     SQL = SQL & "Cotizacion.NUMERO, "
     SQL = SQL & "Anticipo.CONSECUTIVOANTICIPO"
   End If
   fSQLAnticipoAsociadosconCotizacion = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLAnticipoAsociadosconCotizacion", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLAnticiposCobradosAUnaFecha(ByVal ReporteEnMonedaLocal As Boolean, _
                                                   ByVal valoptTasaDeCambio As Boolean, _
                                                    ByVal valConsecutivoCompania As String, _
                                                     ByVal valTipoDeAnticipo As enum_TipoDeAnticipo, _
                                                      ByVal valtxtCodigoClienteProveedor As String, _
                                                       ByVal valClienteProveedorUnico As Boolean, _
                                                        ByVal gMonedaLocalActual As Object, _
                                                         ByVal gUltimaTasaDeCambio As Object, _
                                                          ByVal valFechaHasta As Date) As String
   Dim SQL As String
   Dim usarTasaOriginal As Boolean
   Dim sqlMontoTotal As String
   Dim sqlMontoUsado As String
   Dim sqlMontodevuelto As String
   Dim sqlMontoDiferenciaEnDevolucion As String
   Dim sqlCampoCambioABs As String
   Dim sqlCampoMoneda As String
   Dim gEnumProyecto As clsEnumAdministrativo
   Dim cadena As String
   Dim valFechaCompara As String
   Dim sqlMontoRestanteAlDia As String
   Dim sqlMontoAplicado As String
   Dim valMontoAplicado As String
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   valFechaCompara = "31/12/2007"
   
   valMontoAplicado = gUtilSQL.getIIF("IGV_AnticiposCobradosAUnaFecha.MontoRestanteAlDia = 0", "IGV_AnticiposCobradosAUnaFecha.MontoTotal", "IGV_AnticiposCobradosAUnaFecha.MontoRestanteAlDia")
   
   If ReporteEnMonedaLocal Then
      usarTasaOriginal = valoptTasaDeCambio
      If usarTasaOriginal Then
         sqlCampoCambioABs = "IGV_AnticiposCobradosAUnaFecha.Cambio"
      Else
         sqlCampoCambioABs = gMonedaLocalActual.fSQLConvierteMontoALaFecha("IGV_AnticiposCobradosAUnaFecha.Cambio", "IGV_AnticiposCobradosAUnaFecha.Fecha", True)
      End If
      sqlCampoMoneda = "IGV_AnticiposCobradosAUnaFecha.Moneda"
  
      sqlMontoTotal = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCampoCambioABs, sqlCampoMoneda, _
                     "IGV_AnticiposCobradosAUnaFecha.MontoTotal", usarTasaOriginal, "")
      sqlMontoUsado = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCampoCambioABs, sqlCampoMoneda, _
                      "IGV_AnticiposCobradosAUnaFecha.MontoUsado", usarTasaOriginal, "")
      
      sqlMontodevuelto = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCampoCambioABs, sqlCampoMoneda, _
                     "IGV_AnticiposCobradosAUnaFecha.MontoDevuelto", usarTasaOriginal, "")
      sqlMontoDiferenciaEnDevolucion = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCampoCambioABs, sqlCampoMoneda, _
                     "IGV_AnticiposCobradosAUnaFecha.MontoDiferenciaEnDevolucion", usarTasaOriginal, "")
                     
      sqlMontoRestanteAlDia = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCampoCambioABs, sqlCampoMoneda, _
               valMontoAplicado, usarTasaOriginal, "")
                                       
      sqlMontoAplicado = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCampoCambioABs, sqlCampoMoneda, _
               "IGV_AnticiposCobradosAUnaFecha.MontoUsado", usarTasaOriginal, "")
                                       
                                       
      sqlMontoTotal = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, sqlMontoTotal, "IGV_AnticiposCobradosAUnaFecha.Fecha")
      sqlMontoUsado = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, sqlMontoUsado, "IGV_AnticiposCobradosAUnaFecha.Fecha")
      sqlMontodevuelto = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, sqlMontodevuelto, "IGV_AnticiposCobradosAUnaFecha.Fecha")
      sqlMontoDiferenciaEnDevolucion = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, sqlMontoDiferenciaEnDevolucion, "IGV_AnticiposCobradosAUnaFecha.Fecha")
      sqlMontoRestanteAlDia = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, sqlMontoRestanteAlDia, "IGV_AnticiposCobradosAUnaFecha.FechaAnticipo")
      sqlMontoAplicado = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, sqlMontoAplicado, "IGV_AnticiposCobradosAUnaFecha.Fecha")
   Else
      sqlMontoTotal = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "IGV_AnticiposCobradosAUnaFecha.MontoTotal", "IGV_AnticiposCobradosAUnaFecha.Fecha")
      sqlMontoUsado = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "IGV_AnticiposCobradosAUnaFecha.MontoUsado", "IGV_AnticiposCobradosAUnaFecha.Fecha")
      sqlMontodevuelto = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "IGV_AnticiposCobradosAUnaFecha.MontoDevuelto", "IGV_AnticiposCobradosAUnaFecha.Fecha")
      sqlMontoDiferenciaEnDevolucion = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "IGV_AnticiposCobradosAUnaFecha.MontoDiferenciaEnDevolucion", "IGV_AnticiposCobradosAUnaFecha.Fecha")
      sqlMontoRestanteAlDia = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, valMontoAplicado, "IGV_AnticiposCobradosAUnaFecha.FechaAnticipo")
      sqlMontoAplicado = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "IGV_AnticiposCobradosAUnaFecha.MontoUsado", "IGV_AnticiposCobradosAUnaFecha.Fecha")
   End If

   SQL = "SELECT "
   SQL = SQL & "IGV_AnticiposCobradosAUnaFecha.Fecha, "
   SQL = SQL & "IGV_AnticiposCobradosAUnaFecha.FechaAnticipo, "
   SQL = SQL & "IGV_AnticiposCobradosAUnaFecha.Numero, "
   SQL = SQL & "IGV_AnticiposCobradosAUnaFecha.CodigoCliente AS Codigo, "
   SQL = SQL & "IGV_AnticiposCobradosAUnaFecha.Nombre AS Nombre, "
   SQL = SQL & gUtilSQL.getIIF("IGV_AnticiposCobradosAUnaFecha.EsUnaDevolucion = " & gUtilSQL.fBooleanToStrSQL(True), 0, sqlMontoTotal) & " AS MontoTotal, "
   SQL = SQL & "sum(" & sqlMontoUsado & ") as MontoUsado,"
   SQL = SQL & gUtilSQL.getIIF("IGV_AnticiposCobradosAUnaFecha.EsUnaDevolucion = " & gUtilSQL.fBooleanToStrSQL(True), sqlMontodevuelto, 0) & " AS MontoDevuelto, "
   SQL = SQL & "sum(" & sqlMontoDiferenciaEnDevolucion & ") as MontoDiferenciaEnDevolucion,"
   SQL = SQL & gUtilSQL.fNumToStrSQL(0) & " AS MontoAnulado, "
   If ReporteEnMonedaLocal Then
      SQL = SQL & gUtilSQL.getIIF("IGV_AnticiposCobradosAUnaFecha.Moneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda), gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda), _
               "IGV_AnticiposCobradosAUnaFecha.Moneda", True) & " As Moneda, "
   Else
      SQL = SQL & "IGV_AnticiposCobradosAUnaFecha.Moneda AS moneda, "
   End If
   SQL = SQL & "IGV_AnticiposCobradosAUnaFecha.Cambio,"
   If ReporteEnMonedaLocal Then
      SQL = SQL & gUtilSQL.getIIF("IGV_AnticiposCobradosAUnaFecha.CodigoMoneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMoneda), gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMoneda), _
               "IGV_AnticiposCobradosAUnaFecha.CodigoMoneda", True) & " As CodigoMoneda, "
   Else
      SQL = SQL & "IGV_AnticiposCobradosAUnaFecha.CodigoMoneda AS CodigoMoneda, "
   End If
   SQL = SQL & gUtilSQL.getIIF("IGV_AnticiposCobradosAUnaFecha.EsUnaDevolucion = " & gUtilSQL.fBooleanToStrSQL(True), gUtilSQL.fSimpleSqlValue(gEnumProyecto.enumStatusAnticipoToString(enum_StatusAnticipo.eSDA_COMPLETAMENTE_DEVUELTO)), _
   gUtilSQL.getIIF(sqlMontoRestanteAlDia & " - " & sqlMontoAplicado & " = 0", gUtilSQL.fSimpleSqlValue(gEnumProyecto.enumStatusAnticipoToString(enum_StatusAnticipo.eSDA_COMPLETAMENTE_USADO)), _
   gUtilSQL.getIIF(sqlMontoAplicado & " = 0", gUtilSQL.fSimpleSqlValue(gEnumProyecto.enumStatusAnticipoToString(enum_StatusAnticipo.eSDA_VIGENTE)), _
   gUtilSQL.getIIF("IGV_AnticiposCobradosAUnaFecha.status = " & enum_StatusAnticipo.eSDA_ANULADO, gUtilSQL.fSimpleSqlValue(gEnumProyecto.enumStatusAnticipoToString(enum_StatusAnticipo.eSDA_ANULADO)), _
   gUtilSQL.fSimpleSqlValue(gEnumProyecto.enumStatusAnticipoToString(enum_StatusAnticipo.eSDA_PARCIALMENTE_USADO)))))) & " AS status "
   SQL = SQL & "FROM IGV_AnticiposCobradosAUnaFecha "
   SQL = SQL & " WHERE "
   SQL = SQL & " IGV_AnticiposCobradosAUnaFecha.ConsecutivoCompania = " & valConsecutivoCompania
   If valClienteProveedorUnico Then
       SQL = SQL & " AND " & "IGV_AnticiposCobradosAUnaFecha.CodigoCliente = " & gUtilSQL.fSimpleSqlValue(valtxtCodigoClienteProveedor)
   End If
   SQL = SQL & " AND IGV_AnticiposCobradosAUnaFecha.Fecha <= " & gUtilSQL.fSimpleSqlValue(valFechaHasta)
   SQL = SQL & " AND IGV_AnticiposCobradosAUnaFecha.status <>  " & enum_StatusAnticipo.eSDA_ANULADO
   SQL = SQL & " GROUP BY IGV_AnticiposCobradosAUnaFecha.Fecha, IGV_AnticiposCobradosAUnaFecha.FechaAnticipo, IGV_AnticiposCobradosAUnaFecha.Numero, "
   SQL = SQL & " IGV_AnticiposCobradosAUnaFecha.CodigoCliente, IGV_AnticiposCobradosAUnaFecha.Fecha, IGV_AnticiposCobradosAUnaFecha.FechaAnticipo, "
   SQL = SQL & " IGV_AnticiposCobradosAUnaFecha.Numero, IGV_AnticiposCobradosAUnaFecha.CodigoCliente , IGV_AnticiposCobradosAUnaFecha.Nombre, "
   SQL = SQL & " IGV_AnticiposCobradosAUnaFecha.Moneda, IGV_AnticiposCobradosAUnaFecha.Cambio, IGV_AnticiposCobradosAUnaFecha.CodigoMoneda, "
   SQL = SQL & " IGV_AnticiposCobradosAUnaFecha.Status, IGV_AnticiposCobradosAUnaFecha.MontoTotal, IGV_AnticiposCobradosAUnaFecha.MontoUsado,  "
   SQL = SQL & " IGV_AnticiposCobradosAUnaFecha.EsUnaDevolucion, IGV_AnticiposCobradosAUnaFecha.MontoRestanteAlDia, IGV_AnticiposCobradosAUnaFecha.MontoDevuelto "
   SQL = SQL & " UNION "
   SQL = SQL & " SELECT "
   SQL = SQL & "IGV_AnticiposCobradosAUnaFecha.Fecha, "
   SQL = SQL & "IGV_AnticiposCobradosAUnaFecha.FechaAnticipo, "
   SQL = SQL & "IGV_AnticiposCobradosAUnaFecha.Numero, "
   SQL = SQL & "IGV_AnticiposCobradosAUnaFecha.CodigoCliente AS Codigo, "
   SQL = SQL & "IGV_AnticiposCobradosAUnaFecha.Nombre AS Nombre, "
   SQL = SQL & gUtilSQL.fNumToStrSQL(0) & " as MontoTotal, "
   SQL = SQL & gUtilSQL.fNumToStrSQL(0) & " as MontoUsado,"
   SQL = SQL & gUtilSQL.fNumToStrSQL(0) & " as MontoDevuelto,"
   SQL = SQL & gUtilSQL.fNumToStrSQL(0) & " as MontoDiferenciaEnDevolucion,"
   SQL = SQL & "sum(" & sqlMontoTotal & ") as MontoAnulado, "
   If ReporteEnMonedaLocal Then
      SQL = SQL & gUtilSQL.getIIF("IGV_AnticiposCobradosAUnaFecha.Moneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda), gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda), _
               "IGV_AnticiposCobradosAUnaFecha.Moneda", True) & " As Moneda, "
   Else
      SQL = SQL & "IGV_AnticiposCobradosAUnaFecha.Moneda AS moneda, "
   End If
   SQL = SQL & "IGV_AnticiposCobradosAUnaFecha.Cambio,"
   If ReporteEnMonedaLocal Then
      SQL = SQL & gUtilSQL.getIIF("IGV_AnticiposCobradosAUnaFecha.CodigoMoneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMoneda), gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMoneda), _
               "IGV_AnticiposCobradosAUnaFecha.CodigoMoneda", True) & " As CodigoMoneda, "
   Else
      SQL = SQL & "IGV_AnticiposCobradosAUnaFecha.CodigoMoneda AS CodigoMoneda, "
   End If
   SQL = SQL & gUtilSQL.fSimpleSqlValue(gEnumProyecto.enumStatusAnticipoToString(enum_StatusAnticipo.eSDA_ANULADO)) & " AS status "
   SQL = SQL & "FROM IGV_AnticiposCobradosAUnaFecha "
   SQL = SQL & " WHERE "
   SQL = SQL & " IGV_AnticiposCobradosAUnaFecha.ConsecutivoCompania = " & valConsecutivoCompania
   If valClienteProveedorUnico Then
       SQL = SQL & " AND " & "IGV_AnticiposCobradosAUnaFecha.CodigoCliente = " & gUtilSQL.fSimpleSqlValue(valtxtCodigoClienteProveedor)
   End If
   SQL = SQL & " AND IGV_AnticiposCobradosAUnaFecha.Fecha <= " & gUtilSQL.fSimpleSqlValue(valFechaHasta)
   SQL = SQL & " AND IGV_AnticiposCobradosAUnaFecha.status = " & enum_StatusAnticipo.eSDA_ANULADO
   SQL = SQL & " GROUP BY IGV_AnticiposCobradosAUnaFecha.Fecha, IGV_AnticiposCobradosAUnaFecha.FechaAnticipo, IGV_AnticiposCobradosAUnaFecha.Numero, "
   SQL = SQL & " IGV_AnticiposCobradosAUnaFecha.CodigoCliente, IGV_AnticiposCobradosAUnaFecha.Fecha, IGV_AnticiposCobradosAUnaFecha.FechaAnticipo, "
   SQL = SQL & " IGV_AnticiposCobradosAUnaFecha.Numero, IGV_AnticiposCobradosAUnaFecha.CodigoCliente , IGV_AnticiposCobradosAUnaFecha.Nombre, "
   SQL = SQL & " IGV_AnticiposCobradosAUnaFecha.Moneda, IGV_AnticiposCobradosAUnaFecha.Cambio, IGV_AnticiposCobradosAUnaFecha.CodigoMoneda, "
   SQL = SQL & " IGV_AnticiposCobradosAUnaFecha.Status, IGV_AnticiposCobradosAUnaFecha.MontoTotal, IGV_AnticiposCobradosAUnaFecha.MontoUsado  "
   SQL = SQL & " ORDER BY "
   SQL = SQL & "CodigoMoneda, "
   SQL = SQL & "IGV_AnticiposCobradosAUnaFecha.Nombre, "
   SQL = SQL & "IGV_AnticiposCobradosAUnaFecha.Numero, "
   SQL = SQL & "IGV_AnticiposCobradosAUnaFecha.Fecha, "
   SQL = SQL & "MontoUsado"

   fSQLAnticiposCobradosAUnaFecha = SQL
   Set gEnumProyecto = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLAnticiposCobradosAUnaFecha", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function


Public Function fSQLAnticiposPagadosAUnaFecha(ByVal ReporteEnMonedaLocal As Boolean, _
                                                   ByVal valoptTasaDeCambio As Boolean, _
                                                    ByVal valConsecutivoCompania As String, _
                                                     ByVal valTipoDeAnticipo As enum_TipoDeAnticipo, _
                                                      ByVal valtxtCodigoClienteProveedor As String, _
                                                       ByVal valClienteProveedorUnico As Boolean, _
                                                         ByVal gUltimaTasaDeCambio As Object, _
                                                          ByVal valFechaHasta As Date, _
                                                            ByVal gMonedaLocalActual As Object) As String
   Dim SQL As String
   Dim usarTasaOriginal As Boolean
   Dim sqlMontoTotal As String
   Dim sqlMontoUsado As String
   Dim sqlMontodevuelto As String
   Dim sqlMontoDiferenciaEnDevolucion As String
   Dim sqlCampoCambioABs As String
   Dim sqlCampoMoneda As String
   Dim gEnumProyecto As clsEnumAdministrativo
   Dim cadena As String
   Dim sqlMontoRestanteAlDia As String
   Dim sqlMontoAplicado As String
   Dim valMontoAplicado As String
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   valMontoAplicado = gUtilSQL.getIIF("IGV_AnticiposPagadosAUnaFecha.MontoRestanteAlDia = 0", "IGV_AnticiposPagadosAUnaFecha.MontoTotal", "IGV_AnticiposPagadosAUnaFecha.MontoRestanteAlDia")
   If ReporteEnMonedaLocal Then
      usarTasaOriginal = valoptTasaDeCambio
      If usarTasaOriginal Then
         sqlCampoCambioABs = "IGV_AnticiposPagadosAUnaFecha.Cambio"
      Else
         sqlCampoCambioABs = gMonedaLocalActual.fSQLConvierteMontoALaFecha("IGV_AnticiposPagadosAUnaFecha.Cambio", "IGV_AnticiposPagadosAUnaFecha.Fecha", True)
      End If
      sqlCampoMoneda = "IGV_AnticiposPagadosAUnaFecha.Moneda"
   
      sqlMontoTotal = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCampoCambioABs, sqlCampoMoneda, _
                          "IGV_AnticiposPagadosAUnaFecha.MontoTotal", usarTasaOriginal, "")

      sqlMontoUsado = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCampoCambioABs, sqlCampoMoneda, _
                      "IGV_AnticiposPagadosAUnaFecha.MontoUsado", usarTasaOriginal, "")
      sqlMontodevuelto = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCampoCambioABs, sqlCampoMoneda, _
                         "IGV_AnticiposPagadosAUnaFecha.MontoDevuelto", usarTasaOriginal, "")
      sqlMontoDiferenciaEnDevolucion = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCampoCambioABs, sqlCampoMoneda, _
                                       "IGV_AnticiposPagadosAUnaFecha.MontoDiferenciaEnDevolucion", usarTasaOriginal, "")
      sqlMontoRestanteAlDia = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCampoCambioABs, sqlCampoMoneda, _
               valMontoAplicado, usarTasaOriginal, "")
      sqlMontoAplicado = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCampoCambioABs, sqlCampoMoneda, _
               "IGV_AnticiposPagadosAUnaFecha.MontoUsado", usarTasaOriginal, "")
      sqlMontoRestanteAlDia = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, sqlMontoRestanteAlDia, "IGV_AnticiposPagadosAUnaFecha.FechaAnticipo")
      sqlMontoAplicado = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, sqlMontoAplicado, "IGV_AnticiposPagadosAUnaFecha.Fecha")
   Else
      sqlMontoTotal = gUtilSQL.fRoundNDecimales(gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "IGV_AnticiposPagadosAUnaFecha.MontoTotal", "IGV_AnticiposPagadosAUnaFecha.Fecha"), 2)
      sqlMontoUsado = gUtilSQL.fRoundNDecimales(gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "IGV_AnticiposPagadosAUnaFecha.MontoUsado", "IGV_AnticiposPagadosAUnaFecha.Fecha"), 2)
      sqlMontodevuelto = gUtilSQL.fRoundNDecimales(gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "IGV_AnticiposPagadosAUnaFecha.MontoDevuelto", "IGV_AnticiposPagadosAUnaFecha.Fecha"), 2)
      sqlMontoDiferenciaEnDevolucion = gUtilSQL.fRoundNDecimales(gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "IGV_AnticiposPagadosAUnaFecha.MontoDiferenciaEnDevolucion", "IGV_AnticiposPagadosAUnaFecha.Fecha"), 2)
      sqlMontoRestanteAlDia = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, valMontoAplicado, "IGV_AnticiposPagadosAUnaFecha.FechaAnticipo")
      sqlMontoAplicado = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "IGV_AnticiposPagadosAUnaFecha.MontoUsado", "IGV_AnticiposPagadosAUnaFecha.Fecha")
   End If

   SQL = "SELECT "
   SQL = SQL & "IGV_AnticiposPagadosAUnaFecha.Fecha, "
   SQL = SQL & "IGV_AnticiposPagadosAUnaFecha.FechaAnticipo, "
   SQL = SQL & "IGV_AnticiposPagadosAUnaFecha.Numero, "
   SQL = SQL & "IGV_AnticiposPagadosAUnaFecha.CodigoProveedor AS Codigo, "
   SQL = SQL & "IGV_AnticiposPagadosAUnaFecha.Nombre AS Nombre, "
   SQL = SQL & gUtilSQL.getIIF("IGV_AnticiposPagadosAUnaFecha.EsUnaDevolucion = " & gUtilSQL.fBooleanToStrSQL(True), 0, sqlMontoTotal) & " AS MontoTotal, "
   SQL = SQL & "sum(" & sqlMontoUsado & ") as MontoUsado,"
   SQL = SQL & gUtilSQL.getIIF("IGV_AnticiposPagadosAUnaFecha.EsUnaDevolucion = " & gUtilSQL.fBooleanToStrSQL(True), sqlMontodevuelto, 0) & " AS MontoDevuelto, "
   SQL = SQL & "sum(" & sqlMontoDiferenciaEnDevolucion & ") as MontoDiferenciaEnDevolucion,"
   SQL = SQL & gUtilSQL.fNumToStrSQL(0) & " AS MontoAnulado, "
   If ReporteEnMonedaLocal Then
      SQL = SQL & gUtilSQL.getIIF("IGV_AnticiposPagadosAUnaFecha.Moneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda), gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda), _
               "IGV_AnticiposPagadosAUnaFecha.Moneda", True) & " As Moneda, "
   Else
      SQL = SQL & "IGV_AnticiposPagadosAUnaFecha.Moneda AS moneda, "
   End If
   SQL = SQL & "IGV_AnticiposPagadosAUnaFecha.Cambio,"
   If ReporteEnMonedaLocal Then
      SQL = SQL & gUtilSQL.getIIF("IGV_AnticiposPagadosAUnaFecha.CodigoMoneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMoneda), gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMoneda), _
               "IGV_AnticiposPagadosAUnaFecha.CodigoMoneda", True) & " As CodigoMoneda, "
   Else
      SQL = SQL & "IGV_AnticiposPagadosAUnaFecha.CodigoMoneda AS CodigoMoneda, "
   End If
   SQL = SQL & gUtilSQL.getIIF("IGV_AnticiposPagadosAUnaFecha.EsUnaDevolucion = " & gUtilSQL.fBooleanToStrSQL(True), gUtilSQL.fSimpleSqlValue(gEnumProyecto.enumStatusAnticipoToString(enum_StatusAnticipo.eSDA_COMPLETAMENTE_DEVUELTO)), _
   gUtilSQL.getIIF(sqlMontoRestanteAlDia & " - " & sqlMontoAplicado & " = 0", gUtilSQL.fSimpleSqlValue(gEnumProyecto.enumStatusAnticipoToString(enum_StatusAnticipo.eSDA_COMPLETAMENTE_USADO)), _
   gUtilSQL.getIIF(sqlMontoAplicado & " = 0", gUtilSQL.fSimpleSqlValue(gEnumProyecto.enumStatusAnticipoToString(enum_StatusAnticipo.eSDA_VIGENTE)), _
   gUtilSQL.getIIF("IGV_AnticiposPagadosAUnaFecha.status = " & enum_StatusAnticipo.eSDA_ANULADO, gUtilSQL.fSimpleSqlValue(gEnumProyecto.enumStatusAnticipoToString(enum_StatusAnticipo.eSDA_ANULADO)), _
   gUtilSQL.fSimpleSqlValue(gEnumProyecto.enumStatusAnticipoToString(enum_StatusAnticipo.eSDA_PARCIALMENTE_USADO)))))) & " AS status "

   SQL = SQL & ", IGV_AnticiposPagadosAUnaFecha.NumeroComprobante "
   SQL = SQL & "FROM IGV_AnticiposPagadosAUnaFecha "
   SQL = SQL & " WHERE "
   SQL = SQL & " IGV_AnticiposPagadosAUnaFecha.ConsecutivoCompania = " & valConsecutivoCompania
  If valClienteProveedorUnico Then
       SQL = SQL & " AND " & "IGV_AnticiposPagadosAUnaFecha.CodigoProveedor = " & gUtilSQL.fSimpleSqlValue(valtxtCodigoClienteProveedor)
   End If
   SQL = SQL & " AND IGV_AnticiposPagadosAUnaFecha.Fecha <= " & gUtilSQL.fSimpleSqlValue(valFechaHasta)
   SQL = SQL & " AND IGV_AnticiposPagadosAUnaFecha.status <>  " & enum_StatusAnticipo.eSDA_ANULADO
   SQL = SQL & " GROUP BY IGV_AnticiposPagadosAUnaFecha.Fecha, IGV_AnticiposPagadosAUnaFecha.FechaAnticipo, IGV_AnticiposPagadosAUnaFecha.Numero, "
   SQL = SQL & " IGV_AnticiposPagadosAUnaFecha.CodigoProveedor, IGV_AnticiposPagadosAUnaFecha.Fecha, IGV_AnticiposPagadosAUnaFecha.FechaAnticipo, "
   SQL = SQL & " IGV_AnticiposPagadosAUnaFecha.Numero, IGV_AnticiposPagadosAUnaFecha.CodigoProveedor , IGV_AnticiposPagadosAUnaFecha.Nombre, "
   SQL = SQL & " IGV_AnticiposPagadosAUnaFecha.Moneda, IGV_AnticiposPagadosAUnaFecha.Cambio, IGV_AnticiposPagadosAUnaFecha.CodigoMoneda, "
   SQL = SQL & " IGV_AnticiposPagadosAUnaFecha.Status, IGV_AnticiposPagadosAUnaFecha.MontoTotal, IGV_AnticiposPagadosAUnaFecha.MontoUsado,IGV_AnticiposPagadosAUnaFecha.EsUnaDevolucion, "
   SQL = SQL & " IGV_AnticiposPagadosAUnaFecha.MontoRestanteAlDia, IGV_AnticiposPagadosAUnaFecha.MontoDevuelto"
   SQL = SQL & ", IGV_AnticiposPagadosAUnaFecha.NumeroComprobante "
   SQL = SQL & " UNION "
   SQL = SQL & " SELECT "
   SQL = SQL & "IGV_AnticiposPagadosAUnaFecha.Fecha, "
   SQL = SQL & "IGV_AnticiposPagadosAUnaFecha.FechaAnticipo, "
   SQL = SQL & "IGV_AnticiposPagadosAUnaFecha.Numero, "
   SQL = SQL & "IGV_AnticiposPagadosAUnaFecha.CodigoProveedor AS Codigo, "
   SQL = SQL & "IGV_AnticiposPagadosAUnaFecha.Nombre AS Nombre, "
   SQL = SQL & gUtilSQL.fNumToStrSQL(0) & " as MontoTotal, "
   SQL = SQL & gUtilSQL.fNumToStrSQL(0) & " as MontoUsado,"
   SQL = SQL & gUtilSQL.fNumToStrSQL(0) & " as MontoDevuelto,"
   SQL = SQL & gUtilSQL.fNumToStrSQL(0) & " as MontoDiferenciaEnDevolucion,"
   SQL = SQL & "sum(" & sqlMontoTotal & ") as MontoAnulado, "
   If ReporteEnMonedaLocal Then
      SQL = SQL & gUtilSQL.getIIF("IGV_AnticiposPagadosAUnaFecha.Moneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda), gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda), _
               "IGV_AnticiposPagadosAUnaFecha.Moneda", True) & " As Moneda, "
   Else
      SQL = SQL & "IGV_AnticiposPagadosAUnaFecha.Moneda AS moneda, "
   End If
   SQL = SQL & "IGV_AnticiposPagadosAUnaFecha.Cambio,"
   If ReporteEnMonedaLocal Then
      SQL = SQL & gUtilSQL.getIIF("IGV_AnticiposPagadosAUnaFecha.CodigoMoneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMoneda), gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMoneda), _
               "IGV_AnticiposPagadosAUnaFecha.CodigoMoneda", True) & " As CodigoMoneda, "
   Else
      SQL = SQL & "IGV_AnticiposPagadosAUnaFecha.CodigoMoneda AS CodigoMoneda, "
   End If

   SQL = SQL & gUtilSQL.fSimpleSqlValue(gEnumProyecto.enumStatusAnticipoToString(enum_StatusAnticipo.eSDA_ANULADO)) & " AS status "
   SQL = SQL & ", IGV_AnticiposPagadosAUnaFecha.NumeroComprobante "
   SQL = SQL & "FROM IGV_AnticiposPagadosAUnaFecha "
   SQL = SQL & " WHERE "
   SQL = SQL & " IGV_AnticiposPagadosAUnaFecha.ConsecutivoCompania = " & valConsecutivoCompania
  If valClienteProveedorUnico Then
       SQL = SQL & " AND " & "IGV_AnticiposPagadosAUnaFecha.CodigoProveedor = " & gUtilSQL.fSimpleSqlValue(valtxtCodigoClienteProveedor)
   End If
   SQL = SQL & " AND IGV_AnticiposPagadosAUnaFecha.Fecha <= " & gUtilSQL.fSimpleSqlValue(valFechaHasta)
   SQL = SQL & " AND IGV_AnticiposPagadosAUnaFecha.status =  " & enum_StatusAnticipo.eSDA_ANULADO
   SQL = SQL & " GROUP BY IGV_AnticiposPagadosAUnaFecha.Fecha, IGV_AnticiposPagadosAUnaFecha.FechaAnticipo, IGV_AnticiposPagadosAUnaFecha.Numero, "
   SQL = SQL & " IGV_AnticiposPagadosAUnaFecha.CodigoProveedor, IGV_AnticiposPagadosAUnaFecha.Fecha, IGV_AnticiposPagadosAUnaFecha.FechaAnticipo, "
   SQL = SQL & " IGV_AnticiposPagadosAUnaFecha.Numero, IGV_AnticiposPagadosAUnaFecha.CodigoProveedor , IGV_AnticiposPagadosAUnaFecha.Nombre, "
   SQL = SQL & " IGV_AnticiposPagadosAUnaFecha.Moneda, IGV_AnticiposPagadosAUnaFecha.Cambio, IGV_AnticiposPagadosAUnaFecha.CodigoMoneda, "
   SQL = SQL & " IGV_AnticiposPagadosAUnaFecha.Status, IGV_AnticiposPagadosAUnaFecha.MontoTotal, IGV_AnticiposPagadosAUnaFecha.MontoUsado "
   SQL = SQL & ", IGV_AnticiposPagadosAUnaFecha.NumeroComprobante "
   SQL = SQL & " ORDER BY "
   SQL = SQL & "CodigoMoneda, "
   SQL = SQL & "IGV_AnticiposPagadosAUnaFecha.Nombre, "
   SQL = SQL & "IGV_AnticiposPagadosAUnaFecha.Numero, "
   SQL = SQL & "IGV_AnticiposPagadosAUnaFecha.Fecha, "
   SQL = SQL & "MontoUsado"
   fSQLAnticiposPagadosAUnaFecha = SQL
   Set gEnumProyecto = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLAnticiposPagadosAUnaFecha", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLComprobanteDeAnticipo(ByVal valTipoAnticipo As String, valConsecutivoCompania As Long, _
                                           ByVal valConsecutivoAnticipo As Long, Optional ByVal valVieneDeAdelanto As Boolean) As String
   Dim SQL As String
   Dim sqlCliProvCodigo As String
   Dim sqlCliProvNombre As String
   Dim gEnumProyecto As clsEnumAdministrativo
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   If valTipoAnticipo Then
      sqlCliProvCodigo = "Cliente.Codigo"
      sqlCliProvNombre = "Cliente.Nombre"
   Else
      sqlCliProvCodigo = "Proveedor.CodigoProveedor"
      sqlCliProvNombre = "Proveedor.NombreProveedor"
   End If
   If valVieneDeAdelanto Then
      sqlCliProvCodigo = "Saw.Beneficiario.Codigo"
      sqlCliProvNombre = "Saw.Beneficiario.NombreBeneficiario"
   End If
   SQL = "SELECT "
   SQL = SQL & "anticipo.ConsecutivoAnticipo, "
   If valTipoAnticipo Then 'cobrado
      SQL = SQL & "cliente.NumeroRIF, cliente.Direccion, cliente.Telefono, "
   End If
   SQL = SQL & "anticipo.Fecha, anticipo.Numero, "
   If valTipoAnticipo Then 'cobrado
      SQL = SQL & "Banco.Nombre AS Banco, "
   End If
   SQL = SQL & gUtilSQL.DfSQLCaseIfForEnum("anticipo.Status", enum_StatusAnticipo.eSDA_VIGENTE, gEnumProyecto.fenumStatusAnticipoToStrinInArray(True), "Status") & ", "
   SQL = SQL & "anticipo.Moneda, anticipo.Cambio, "
   SQL = SQL & sqlCliProvCodigo & " AS Codigo, "
   SQL = SQL & sqlCliProvNombre & " AS Nombre, "
   SQL = SQL & "anticipo.NombreBeneficiario" & " AS beneficiario, "
   SQL = SQL & "anticipo.CodigoCuentaBancaria, "
   SQL = SQL & "cuentaBancaria.NombreCuenta, "
   SQL = SQL & "anticipo.CodigoConceptoBancario, "
   SQL = SQL & "conceptoBancario.Descripcion AS DescripcionConceptoBancario, "
   SQL = SQL & "anticipo.Descripcion AS DescripcionAnticipo, "
   SQL = SQL & "anticipo.MontoTotal, "
   SQL = SQL & "anticipo.MontoUsado, "
   SQL = SQL & "anticipo.MontoDevuelto, "
   SQL = SQL & "anticipo.MontoDiferenciaEnDevolucion, "
   SQL = SQL & "(anticipo.MontoTotal - (anticipo.MontoUsado + anticipo.MontoDiferenciaEnDevolucion + anticipo.MontoDevuelto)) AS MontoRestante, "
   SQL = SQL & " anticipo.NumeroCheque  As NumeroCheque "
   SQL = SQL & fSQLFromComprobanteAnticipo(valTipoAnticipo, valVieneDeAdelanto)
   SQL = SQL & " WHERE anticipo.ConsecutivoAnticipo = " & valConsecutivoAnticipo
   SQL = SQL & " AND anticipo.ConsecutivoCompania = " & valConsecutivoCompania
   SQL = SQL & " ORDER BY anticipo.ConsecutivoAnticipo"
   Set gEnumProyecto = Nothing
h_EXIT: On Error GoTo 0
   fSQLComprobanteDeAnticipo = SQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLComprobanteDeAnticipo", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLFromComprobanteAnticipo(ByVal valTipoAnticipo As String, Optional ByVal valVieneDeAdelanto As Boolean) As String
   Dim SQL As String
   Dim sqlAnticipoCodigo As String
   Dim sqlCliProvCodigo As String
   Dim sqlCliProvConsecuetivoCia As String
   Dim sqlClienteProveedor As String
   On Error GoTo h_ERROR
   If valTipoAnticipo Then
      sqlAnticipoCodigo = "anticipo.CodigoCliente"
      sqlCliProvCodigo = "Cliente.Codigo"
      sqlCliProvConsecuetivoCia = "Cliente.ConsecutivoCompania"
      sqlClienteProveedor = "Cliente"
   Else
      sqlAnticipoCodigo = "anticipo.CodigoProveedor"
      sqlCliProvCodigo = "Proveedor.CodigoProveedor"
      sqlCliProvConsecuetivoCia = "Proveedor.ConsecutivoCompania"
      sqlClienteProveedor = "Proveedor"
   End If
   If valVieneDeAdelanto Then
      sqlAnticipoCodigo = "anticipo.CodigoBeneficiario"
      sqlCliProvCodigo = "Saw.Beneficiario.Codigo"
      sqlCliProvConsecuetivoCia = "Saw.Beneficiario.ConsecutivoCompania"
      sqlClienteProveedor = "Saw.Beneficiario"
   End If
   SQL = " FROM ((anticipo LEFT JOIN ConceptoBancario ON "
   SQL = SQL & " anticipo.CodigoConceptoBancario = ConceptoBancario.Codigo"
   If valTipoAnticipo Then
      SQL = SQL & ") LEFT JOIN (CuentaBancaria "
      SQL = SQL & " INNER JOIN Banco"
      SQL = SQL & " ON (Banco.Codigo = CuentaBancaria.CodigoBanco))"
   Else
      SQL = SQL & ") LEFT JOIN CuentaBancaria "
   End If
   SQL = SQL & " ON ("
   SQL = SQL & " anticipo.CodigoCuentaBancaria = CuentaBancaria.Codigo"
   SQL = SQL & ") AND (anticipo.ConsecutivoCompania = CuentaBancaria.ConsecutivoCompania"
   SQL = SQL & ")) INNER JOIN " & sqlClienteProveedor & " ON (" & sqlAnticipoCodigo & _
               " = " & sqlCliProvCodigo
   SQL = SQL & ") AND (anticipo.ConsecutivoCompania = " & sqlCliProvConsecuetivoCia & ")"
h_EXIT: On Error GoTo 0
   fSQLFromComprobanteAnticipo = SQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLFromComprobanteAnticipo", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLComprobanteDeAnticiposUsados(ByVal valNumeroComprobante As Long, ByVal valNumeroCobranza As String, ByVal valTableName As String, ByVal valFN_NUMERO_COMPROBANTE_NUMERO_COBRANZA As String, ByVal valTipoDeAnticipo As enum_TipoDeAnticipo, gProyCompaniaActual As Object) As String
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = "SELECT "
   SQL = SQL & valTableName & ".Secuencial, "
   SQL = SQL & valTableName & ".NumeroAnticipo, "
   SQL = SQL & valTableName & ".MontoOriginal, "
   SQL = SQL & valTableName & ".MontoRestanteAlDia, "
   SQL = SQL & valTableName & ".MontoAplicado"
   SQL = SQL & " FROM " & valTableName
   SQL = SQL & " WHERE " & valTableName & ".ConsecutivoCompania = " & gProyCompaniaActual.GetConsecutivoCompania
   SQL = SQL & " AND " & valTableName & "." & valFN_NUMERO_COMPROBANTE_NUMERO_COBRANZA & " = "
   If valTipoDeAnticipo = eTDA_COBRADO Then
      SQL = SQL & gUtilSQL.fSimpleSqlValue(valNumeroCobranza)
   Else
      SQL = SQL & valNumeroComprobante
   End If
   SQL = SQL & " ORDER BY " & valTableName & ".Secuencial"
   fSQLComprobanteDeAnticiposUsados = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLComprobanteDeAnticiposUsados", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function fConstruirSQLDeAnticiposEntreFechas(ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal gProyCompaniaActual As Object) As String
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = " SELECT "
   SQL = SQL & "Fecha" & " AS FechaDocumento, "
   SQL = SQL & "Descripcion" & " AS Descripcion, "
   SQL = SQL & "Status" & " AS Status, "
   SQL = SQL & "Tipo" & " AS Tipo, "
   SQL = SQL & gUtilSQL.fSimpleSqlValue("(") & gUtilSQL.CharConcat & gUtilSQL.fCast("ConsecutivoAnticipo", eTDSS_VARCHAR, "") & gUtilSQL.CharConcat & gUtilSQL.fSimpleSqlValue(") ") & gUtilSQL.CharConcat & gUtilSQL.fCast("Numero", eTDSS_VARCHAR, "") & " AS NumeroDocumento, "
   SQL = SQL & "MontoTotal" & " AS TotalDocumento "
   SQL = SQL & " FROM Anticipo"
   SQL = SQL & " WHERE ConsecutivoCompania" & " = "
   SQL = SQL & gProyCompaniaActual.GetConsecutivoCompania
   SQL = SQL & " AND " & gUtilSQL.DfSQLDateValueBetween("Fecha", valFechaInicial, valFechaFinal)
   fConstruirSQLDeAnticiposEntreFechas = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConstruirSQLDeAnticiposEntreFechas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLSearchAnticiposPendientes(ByVal valCodigoProveedorCliente As String, ByVal valMonedaDocumento As String, ByVal valHastaLaFecha As Date, ByVal gProyCompaniaActual As Object, ByVal valTipoAsEnum As enum_TipoDeAnticipo) As String
   Dim SQL As String
   Dim SqlStatus As String
   On Error GoTo h_ERROR
   SqlStatus = gUtilSQL.fSQLSimpleValueForEnum(enum_StatusAnticipo.eSDA_VIGENTE) & ", "
   SqlStatus = SqlStatus & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusAnticipo.eSDA_PARCIALMENTE_USADO) & ", "
   SqlStatus = SqlStatus & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusAnticipo.eSDA_PARCIALMENTE_DEVUELTO)
   SQL = "SELECT "
   SQL = SQL & "ConsecutivoAnticipo" & ", "
   SQL = SQL & "Numero" & ", "
   SQL = SQL & "Status" & ", "
   SQL = SQL & "MontoTotal" & ", "
   SQL = SQL & "MontoTotal" & " - (" & "MontoUsado" & " + " & "MontoDevuelto" & " + " & "MontoDiferenciaEnDevolucion" & ") AS MontoRestante, "
   SQL = SQL & "Moneda, "
   SQL = SQL & "CodigoMoneda"
   SQL = SQL & " FROM  Anticipo"
   SQL = SQL & " WHERE " & "ConsecutivoCompania" & " = " & gProyCompaniaActual.GetConsecutivoCompania
   If valTipoAsEnum = eTDA_PAGADO Then
      SQL = SQL & " AND " & "CodigoProveedor" & " = " & gUtilSQL.fSimpleSqlValue(valCodigoProveedorCliente)
   Else
      SQL = SQL & " AND " & "CodigoCliente" & " = " & gUtilSQL.fSimpleSqlValue(valCodigoProveedorCliente)
   End If
   SQL = SQL & " AND " & "Status" & " IN( " & SqlStatus & ")"
   SQL = SQL & " AND " & "MontoTotal" & " <> (" & "MontoUsado" & " + " & "MontoDevuelto" & " + " & "MontoDiferenciaEnDevolucion" & ")"
   SQL = SQL & " AND " & "Fecha" & " <= " & gUtilSQL.fDateToSQLValue(valHastaLaFecha)
   SQL = SQL & " AND " & "ConsecutivoRendicion = '0' "
   fSQLSearchAnticiposPendientes = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLSearchAnticiposPendientes", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
