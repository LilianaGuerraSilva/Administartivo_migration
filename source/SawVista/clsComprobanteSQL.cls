VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsComprobanteSQL"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit

Private Const CM_FILE_NAME As String = "clsComprobanteSQL"
Private Const CM_MESSAGE_NAME As String = "SQL de Comprobante"

Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function

Public Function fSQLFechaUltimoComprobanteDelPeriodo(ByVal valConsecutivoPerido As Long, ByVal valFecha As Date) As String
   Dim vSql As String
   Dim vWhere As String
   On Error GoTo h_ERROR
   vWhere = gUtilSQL.fSQLValueDateWithComparisonRule("", "COMPROBANTE.Fecha", valFecha, "=")
   vWhere = gUtilSQL.fSQLEnumValueWithAnd(vWhere, "COMPROBANTE.GeneradoPor", enum_ComprobanteGeneradoPor.eCG_INVENTARIO)
   vWhere = gUtilSQL.fSQLNumberValueWithAnd(vWhere, "COMPROBANTE.ConsecutivoPeriodo", valConsecutivoPerido)
   
   vSql = " SELECT COMPROBANTE.Fecha FROM COMPROBANTE  " & gUtilSQL.getWhereSQL(vWhere)
  fSQLFechaUltimoComprobanteDelPeriodo = vSql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLFechaUltimoComprobanteDelPeriodo", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLFechaAContabilizar(ByVal valConsecutivoCompania As Long, ByVal valFechaCierrePeriodo As Date, ByVal valFechaInicioMetodo As Date) As String
   Dim vSql As String
   Dim vWhere As String
   On Error GoTo h_ERROR
   vWhere = gUtilSQL.fSQLValueDatesBetween("", "COMPROBANTE.Fecha", valFechaInicioMetodo, valFechaCierrePeriodo)
   vWhere = gUtilSQL.fSQLEnumValueWithAnd(vWhere, "COMPROBANTE.GeneradoPor", enum_ComprobanteGeneradoPor.eCG_INVENTARIO)
   vWhere = gUtilSQL.fSQLNumberValueWithAnd(vWhere, "PERIODO.ConsecutivoCompania", valConsecutivoCompania)

   vSql = " SELECT ISNULL(MAX(COMPROBANTE.Fecha)+ 1, " & gUtilSQL.fDateToSQLValue(gUtilDate.fColocaUltimoDiaDelMes(valFechaInicioMetodo)) & "  )"
   vSql = vSql & "   FROM COMPROBANTE INNER JOIN PERIODO ON COMPROBANTE.ConsecutivoPeriodo = periodo.ConsecutivoPeriodo " & gUtilSQL.getWhereSQL(vWhere)
 fSQLFechaAContabilizar = vSql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLFechaAContabilizar", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fAgregaUnaCuentaAlSQL(ByVal valDebeOHaber As String, ByVal valEsUnReverso As Boolean, ByVal valMontoDeLaCuenta As String, ByVal valCodigoDeLaCuenta As String, _
            ByVal valLaCuentaUsaAuxiliares As Boolean, ByVal valCodigoDelAuxiliar As String, _
            ByVal valNoDocumentoOrigen As String, ByVal valDescripcionDelAsiento As String, _
            ByVal valCondicionDeStatusAnulado As String, ByVal valCodigoCentroDeCosto As String, ByVal valInsCompaniaActual As Object, ByVal valInsgProyParametrosCompania As Object) As String
   Dim Sql As String
   Dim gProyCompaniaActual As Object
   Dim gProyParametrosCompania As Object
   On Error GoTo h_ERROR
   Set gProyCompaniaActual = valInsCompaniaActual
   Set gProyParametrosCompania = valInsgProyParametrosCompania
   Sql = gUtilSQL.fSimpleSqlValue(fCambiaDebeHaberSiEsUnReverso(valDebeOHaber, valEsUnReverso)) & ", "
   Sql = Sql & valMontoDeLaCuenta & ", "
   If gTexto.DfInStr(valCodigoDeLaCuenta, gUtilSQL.getPrefijoIIF) <> 0 Then
      Sql = Sql & valCodigoDeLaCuenta & ", "
   Else
      Sql = Sql & "'" & valCodigoDeLaCuenta & "' , "
   End If
   Sql = Sql & fColocaAuxiliarSiAplica(valLaCuentaUsaAuxiliares, valCodigoDelAuxiliar, gProyCompaniaActual.GetUsaAuxiliares)
   If (gProyCompaniaActual.GetUsaCentroDeCostos And gProyParametrosCompania.GetAsociaCentroDeCostoyAlmacen) And valCodigoCentroDeCosto <> "" Then
        Sql = Sql & valCodigoCentroDeCosto & ", "
   Else
   Sql = Sql & gUtilSQL.fSimpleSqlValue("NA") & ", "
   End If
   If gTexto.DfLeft(valNoDocumentoOrigen, 1) <> vbTab Then
         Sql = Sql & valNoDocumentoOrigen & ", "
   Else
      valNoDocumentoOrigen = gTexto.fLimpiaStringDeCharAAmbosLados(valNoDocumentoOrigen, vbTab)
      Sql = Sql & valNoDocumentoOrigen & ", "
   End If
   Sql = Sql & valDescripcionDelAsiento
   fAgregaUnaCuentaAlSQL = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fAgregaUnaCuentaAlSQL", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
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
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCambiaDebeHaberSiEsUnReverso", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
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
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fColocaAuxiliarSiAplica", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLCostoDeVenta(ByVal valConsecutivoCompania As Long, ByVal valFechaDesde As Date, ByVal valFechaHasta As Date, ByVal valEsUnReverso As Boolean, ByVal valDetallado As Boolean, ByVal valAgruparPorCuentaArticulo As Boolean, ByVal valInsCompaniaActual As Object, ByVal valInsgProyParametrosCompania As Object, ByVal valInsgProyReglasDeContabilizacion As Object, ByVal valNroDocumentoOrigen As String, ByVal valDescripcionAsiento As String, ByVal valFechaCierreCostoPromedio As Date) As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = ""
   If valDetallado Then
     If (valAgruparPorCuentaArticulo) Then
          vResult = fSelectCostoVentaDetalladoAgrupadoPorCuentaArticuloComprobante(valConsecutivoCompania, valFechaDesde, valFechaHasta, valEsUnReverso, valInsCompaniaActual, valInsgProyParametrosCompania, valInsgProyReglasDeContabilizacion, valFechaCierreCostoPromedio)
         Else
           vResult = fSelectCostoVentaDetalladoComprobante(valConsecutivoCompania, valFechaDesde, valFechaHasta, valEsUnReverso, valInsCompaniaActual, valInsgProyParametrosCompania, valInsgProyReglasDeContabilizacion, valFechaCierreCostoPromedio)
         End If
    Else
         If (valAgruparPorCuentaArticulo) Then
            vResult = fSelectCostoVentaResumidoAgrupadoPorCuentaArticuloComprobante(valConsecutivoCompania, valFechaDesde, valFechaHasta, valEsUnReverso, valInsCompaniaActual, valInsgProyParametrosCompania, valInsgProyReglasDeContabilizacion, valNroDocumentoOrigen, valDescripcionAsiento, valFechaCierreCostoPromedio)
         Else
            vResult = fSelectCostoVentaResumidoComprobante(valConsecutivoCompania, valFechaDesde, valFechaHasta, valEsUnReverso, valInsCompaniaActual, valInsgProyParametrosCompania, valInsgProyReglasDeContabilizacion, valNroDocumentoOrigen, valDescripcionAsiento, valFechaCierreCostoPromedio)
         End If
    End If
  fSQLCostoDeVenta = vResult
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fColocaAuxiliarSiAplica", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSelectCostoVentaDetalladoComprobante(ByVal valConsecutivoCompania As Long, ByVal valFechaDesde As Date, ByVal valFechaHasta As Date, ByVal valEsUnReverso As Boolean, ByVal valInsCompaniaActual As Object, ByVal valInsgProyParametrosCompania As Object, ByVal valInsgProyReglasDeContabilizacion As Object, ByVal valFechaCierreCostoPromedio As Date) As String
   Dim gProyReglasDeContabilizacion As Object
   Set gProyReglasDeContabilizacion = valInsgProyReglasDeContabilizacion
   Dim sqlWhere As String
   Dim vSql As String
   Dim vMonto As String
   On Error GoTo h_ERROR
   vMonto = " ISNULL(SUM (salidas * costofinal),0) "
   sqlWhere = gUtilSQL.fSQLValueDatesBetween(fSqlCondicionTipoDocumento(), "Gf_CostoPromedioPonderado.Fecha", valFechaDesde, valFechaHasta)
   sqlWhere = gUtilSQL.getWhereSQL(sqlWhere)
   vSql = "  SELECT fecha, "
   vSql = vSql & fAgregaUnaCuentaAlSQL("DEBE", valEsUnReverso, vMonto, gProyReglasDeContabilizacion.GetCuentaCostoDeVenta, False, "", "numerodocumento", "TipoDeDocumento", "", "", valInsCompaniaActual, valInsgProyParametrosCompania) & " , "
   vSql = vSql & fAgregaUnaCuentaAlSQL("HABER", valEsUnReverso, vMonto, gProyReglasDeContabilizacion.GetCuentaInventario, False, "", "numerodocumento AS Numero", "TipoDeDocumento AS TIPO ", "", "", valInsCompaniaActual, valInsgProyParametrosCompania)
   vSql = vSql & " FROM   dbo.Gf_CostoPromedioPonderado( " & gUtilSQL.fNumToStrSQL(valConsecutivoCompania) & "," & gUtilSQL.fDateToSQLValue(valFechaCierreCostoPromedio) & "," & gUtilSQL.fDateToSQLValue(valFechaHasta) & "," & fArticuloInventarioMaxOrMinCodigo(valConsecutivoCompania, False) & "," & fArticuloInventarioMaxOrMinCodigo(valConsecutivoCompania, True) & ") AS Gf_CostoPromedioPonderado  "
   vSql = vSql & sqlWhere
   vSql = vSql & " GROUP BY fecha,numerodocumento,tipodedocumento "
   vSql = vSql & " ORDER BY fecha  ,numerodocumento"
   fSelectCostoVentaDetalladoComprobante = vSql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSelectCostoVentaDetalladoComprobante", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSelectCostoVentaResumidoComprobante(ByVal valConsecutivoCompania As Long, ByVal valFechaDesde As Date, ByVal valFechaHasta As Date, ByVal valEsUnReverso As Boolean, ByVal valInsCompaniaActual As Object, ByVal valInsgProyParametrosCompania As Object, ByVal valInsgProyReglasDeContabilizacion As Object, ByVal valNroDocumentoOrigen As String, ByVal valDescripcionAsiento As String, ByVal valFechaCierreCostoPromedio As Date) As String
   Dim gProyReglasDeContabilizacion As Object
   Set gProyReglasDeContabilizacion = valInsgProyReglasDeContabilizacion
   Dim sqlWhere As String
   Dim vSql As String
   Dim vMonto As String
   On Error GoTo h_ERROR
   vMonto = " ISNULL(SUM (salidas * costofinal),0) "
   sqlWhere = gUtilSQL.fSQLValueDatesBetween(fSqlCondicionTipoDocumento(), "Gf_CostoPromedioPonderado.Fecha", valFechaDesde, valFechaHasta)
   sqlWhere = gUtilSQL.getWhereSQL(sqlWhere)
   vSql = "SELECT "
   vSql = vSql & gUtilSQL.fDateToSQLValue(valFechaHasta) & " AS FECHA  , "
   vSql = vSql & fAgregaUnaCuentaAlSQL("DEBE", valEsUnReverso, vMonto, gProyReglasDeContabilizacion.GetCuentaCostoDeVenta, False, "", gUtilSQL.fSimpleSqlValue(valNroDocumentoOrigen), gUtilSQL.fSimpleSqlValue(valDescripcionAsiento), "", "", valInsCompaniaActual, valInsgProyParametrosCompania) & " , "
   vSql = vSql & fAgregaUnaCuentaAlSQL("HABER", valEsUnReverso, vMonto, gProyReglasDeContabilizacion.GetCuentaInventario, False, "", gUtilSQL.fSimpleSqlValue(valNroDocumentoOrigen), gUtilSQL.fSimpleSqlValue(valDescripcionAsiento), "", "", valInsCompaniaActual, valInsgProyParametrosCompania)

   vSql = vSql & " FROM dbo.Gf_CostoPromedioPonderado( " & gUtilSQL.fNumToStrSQL(valConsecutivoCompania) & "," & gUtilSQL.fDateToSQLValue(valFechaCierreCostoPromedio) & "," & gUtilSQL.fDateToSQLValue(valFechaHasta) & "," & fArticuloInventarioMaxOrMinCodigo(valConsecutivoCompania, False) & "," & fArticuloInventarioMaxOrMinCodigo(valConsecutivoCompania, True) & ") AS Gf_CostoPromedioPonderado "
   vSql = vSql & sqlWhere
   fSelectCostoVentaResumidoComprobante = vSql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSelectCostoVentaDetalladoComprobante", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSelectCostoVentaDetalladoAgrupadoPorCuentaArticuloComprobante(ByVal valConsecutivoCompania As Long, ByVal valFechaDesde As Date, ByVal valFechaHasta As Date, ByVal valEsUnReverso As Boolean, ByVal valInsCompaniaActual As Object, ByVal valInsgProyParametrosCompania As Object, ByVal valInsgProyReglasDeContabilizacion As Object, ByVal valFechaCierreCostoPromedio As Date) As String
   Dim gProyReglasDeContabilizacion As Object
   Set gProyReglasDeContabilizacion = valInsgProyReglasDeContabilizacion
   Dim sqlWhere As String
   Dim sqlCuentaInvHaber As String
   Dim sqlCuentaInvDebe As String
   Dim vSql As String
   Dim vMonto As String
   On Error GoTo h_ERROR
   vMonto = " ISNULL(SUM (salidas * costofinal),0) "
   sqlCuentaInvHaber = " ArticuloInventario.CuentaInventario = " & gUtilSQL.fSimpleSqlValue("") & " OR " & gUtilSQL.DfSQLIsNull("ArticuloInventario.CuentaInventario")
   sqlCuentaInvHaber = gUtilSQL.getIIF(sqlCuentaInvHaber, gUtilSQL.fSimpleSqlValue(gProyReglasDeContabilizacion.GetCuentaInventario), "ArticuloInventario.CuentaInventario", True)
   sqlCuentaInvDebe = " ArticuloInventario.CuentaCostoDeVenta = " & gUtilSQL.fSimpleSqlValue("") & " OR " & gUtilSQL.DfSQLIsNull("ArticuloInventario.CuentaCostoDeVenta")
   sqlCuentaInvDebe = gUtilSQL.getIIF(sqlCuentaInvDebe, gUtilSQL.fSimpleSqlValue(gProyReglasDeContabilizacion.GetCuentaCostoDeVenta), "ArticuloInventario.CuentaCostoDeVenta", True)
   sqlWhere = gUtilSQL.fSQLValueDatesBetween(fSqlCondicionTipoDocumento(), "Gf_CostoPromedioPonderado.Fecha", valFechaDesde, valFechaHasta)
   sqlWhere = gUtilSQL.getWhereSQL(sqlWhere)
   vSql = "SELECT fecha, "
   vSql = vSql & fAgregaUnaCuentaAlSQL("DEBE", valEsUnReverso, vMonto, sqlCuentaInvDebe, False, "", "numerodocumento", "TipoDeDocumento", gUtilSQL.fSimpleSqlValue(""), gUtilSQL.fSimpleSqlValue(""), valInsCompaniaActual, valInsgProyParametrosCompania)
   vSql = vSql & " FROM   dbo.Gf_CostoPromedioPonderado( " & gUtilSQL.fNumToStrSQL(valConsecutivoCompania) & "," & gUtilSQL.fDateToSQLValue(valFechaCierreCostoPromedio) & "," & gUtilSQL.fDateToSQLValue(valFechaHasta) & "," & fArticuloInventarioMaxOrMinCodigo(valConsecutivoCompania, False) & "," & fArticuloInventarioMaxOrMinCodigo(valConsecutivoCompania, True) & ") AS Gf_CostoPromedioPonderado  INNER JOIN "
   vSql = vSql & " ArticuloInventario ON "
   vSql = vSql & " Gf_CostoPromedioPonderado.Codigo = dbo.ArticuloInventario.Codigo AND "
   vSql = vSql & gUtilSQL.fNumToStrSQL(valConsecutivoCompania) & "  = ArticuloInventario.ConsecutivoCompania "
   vSql = vSql & sqlWhere
   vSql = vSql & " GROUP BY fecha,numerodocumento,tipodedocumento,CuentaCostoDeVenta"

   vSql = vSql & " UNION "
   vSql = vSql & " SELECT fecha, "
   vSql = vSql & fAgregaUnaCuentaAlSQL("HABER", valEsUnReverso, vMonto, sqlCuentaInvHaber, False, "", "numerodocumento", "TipoDeDocumento", gUtilSQL.fSimpleSqlValue(""), gUtilSQL.fSimpleSqlValue(""), valInsCompaniaActual, valInsgProyParametrosCompania)
   vSql = vSql & " FROM   dbo.Gf_CostoPromedioPonderado( " & gUtilSQL.fNumToStrSQL(valConsecutivoCompania) & "," & gUtilSQL.fDateToSQLValue(valFechaCierreCostoPromedio) & "," & gUtilSQL.fDateToSQLValue(valFechaHasta) & "," & fArticuloInventarioMaxOrMinCodigo(valConsecutivoCompania, False) & "," & fArticuloInventarioMaxOrMinCodigo(valConsecutivoCompania, True) & ") AS Gf_CostoPromedioPonderado INNER JOIN "
   vSql = vSql & " ArticuloInventario ON Gf_CostoPromedioPonderado.Codigo = dbo.ArticuloInventario.Codigo AND "
   vSql = vSql & gUtilSQL.fNumToStrSQL(valConsecutivoCompania) & "  = ArticuloInventario.ConsecutivoCompania "
   vSql = vSql & sqlWhere
   vSql = vSql & " GROUP BY fecha, numerodocumento, tipodedocumento, CuentaInventario "
   vSql = vSql & " ORDER BY fecha, numerodocumento"
   fSelectCostoVentaDetalladoAgrupadoPorCuentaArticuloComprobante = vSql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSelectCostoVentaDetalladoComprobante", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSelectCostoVentaResumidoAgrupadoPorCuentaArticuloComprobante(ByVal valConsecutivoCompania As Long, ByVal valFechaDesde As Date, ByVal valFechaHasta As Date, ByVal valEsUnReverso As Boolean, ByVal valInsCompaniaActual As Object, ByVal valInsgProyParametrosCompania As Object, ByVal valInsgProyReglasDeContabilizacion As Object, ByVal valNroDocumentoOrigen As String, ByVal valDescripcionAsiento As String, ByVal valFechaCierreCostoPromedio As Date) As String
   Dim gProyReglasDeContabilizacion As Object
   Set gProyReglasDeContabilizacion = valInsgProyReglasDeContabilizacion
   Dim sqlWhere As String
   Dim sqlCuentaInvHaber As String
   Dim sqlCuentaInvDebe As String
   Dim vSql As String
   Dim vMonto As String
   On Error GoTo h_ERROR
   vMonto = " ISNULL(SUM (salidas * costofinal),0) "
   sqlCuentaInvHaber = " ArticuloInventario.CuentaInventario = " & gUtilSQL.fSimpleSqlValue("") & " OR " & gUtilSQL.DfSQLIsNull("ArticuloInventario.CuentaInventario")
   sqlCuentaInvHaber = gUtilSQL.getIIF(sqlCuentaInvHaber, gUtilSQL.fSimpleSqlValue(gProyReglasDeContabilizacion.GetCuentaInventario), "ArticuloInventario.CuentaInventario", True)
   sqlCuentaInvDebe = " ArticuloInventario.CuentaCostoDeVenta = " & gUtilSQL.fSimpleSqlValue("") & " OR " & gUtilSQL.DfSQLIsNull("ArticuloInventario.CuentaCostoDeVenta")
   sqlCuentaInvDebe = gUtilSQL.getIIF(sqlCuentaInvDebe, gUtilSQL.fSimpleSqlValue(gProyReglasDeContabilizacion.GetCuentaCostoDeVenta), "ArticuloInventario.CuentaCostoDeVenta", True)
   sqlWhere = gUtilSQL.fSQLValueDatesBetween(fSqlCondicionTipoDocumento(), "Gf_CostoPromedioPonderado.Fecha", valFechaDesde, valFechaHasta)
   sqlWhere = gUtilSQL.getWhereSQL(sqlWhere)
   
   vSql = "SELECT "
   vSql = vSql & gUtilSQL.fDateToSQLValue(valFechaHasta) & " AS FECHA, "
   vSql = vSql & fAgregaUnaCuentaAlSQL("DEBE", valEsUnReverso, vMonto, sqlCuentaInvDebe, False, "", gUtilSQL.fSimpleSqlValue(valNroDocumentoOrigen), gUtilSQL.fSimpleSqlValue(valDescripcionAsiento), gUtilSQL.fSimpleSqlValue(""), gUtilSQL.fSimpleSqlValue(""), valInsCompaniaActual, valInsgProyParametrosCompania)
   vSql = vSql & " FROM   dbo.Gf_CostoPromedioPonderado( " & gUtilSQL.fNumToStrSQL(valConsecutivoCompania) & "," & gUtilSQL.fDateToSQLValue(valFechaCierreCostoPromedio) & "," & gUtilSQL.fDateToSQLValue(valFechaHasta) & "," & fArticuloInventarioMaxOrMinCodigo(valConsecutivoCompania, False) & "," & fArticuloInventarioMaxOrMinCodigo(valConsecutivoCompania, True) & ") AS Gf_CostoPromedioPonderado  INNER JOIN "
   vSql = vSql & " ArticuloInventario ON "
   vSql = vSql & " Gf_CostoPromedioPonderado.Codigo = dbo.ArticuloInventario.Codigo AND "
   vSql = vSql & gUtilSQL.fNumToStrSQL(valConsecutivoCompania) & "  = ArticuloInventario.ConsecutivoCompania "
   vSql = vSql & sqlWhere
   vSql = vSql & " GROUP BY ArticuloInventario.CuentaCostoDeVenta "
   vSql = vSql & " UNION "
   vSql = vSql & " SELECT "
   vSql = vSql & gUtilSQL.fDateToSQLValue(valFechaHasta) & " AS FECHA, "
   vSql = vSql & fAgregaUnaCuentaAlSQL("HABER", valEsUnReverso, vMonto, sqlCuentaInvHaber, False, "", gUtilSQL.fSimpleSqlValue(valNroDocumentoOrigen), gUtilSQL.fSimpleSqlValue(valDescripcionAsiento), gUtilSQL.fSimpleSqlValue(""), gUtilSQL.fSimpleSqlValue(""), valInsCompaniaActual, valInsgProyParametrosCompania)
   vSql = vSql & " FROM   dbo.Gf_CostoPromedioPonderado( " & gUtilSQL.fNumToStrSQL(valConsecutivoCompania) & "," & gUtilSQL.fDateToSQLValue(valFechaCierreCostoPromedio) & "," & gUtilSQL.fDateToSQLValue(valFechaHasta) & "," & fArticuloInventarioMaxOrMinCodigo(valConsecutivoCompania, False) & "," & fArticuloInventarioMaxOrMinCodigo(valConsecutivoCompania, True) & ") AS Gf_CostoPromedioPonderado INNER JOIN "
   vSql = vSql & " ArticuloInventario ON Gf_CostoPromedioPonderado.Codigo = dbo.ArticuloInventario.Codigo AND "
   vSql = vSql & gUtilSQL.fNumToStrSQL(valConsecutivoCompania) & "  = ArticuloInventario.ConsecutivoCompania "
   vSql = vSql & sqlWhere
   vSql = vSql & " GROUP BY CuentaInventario "
   fSelectCostoVentaResumidoAgrupadoPorCuentaArticuloComprobante = vSql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSelectCostoVentaDetalladoComprobante", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fArticuloInventarioMaxOrMinCodigo(ByVal valConsecutivoCompania As Long, ByVal valMax As Boolean) As String
   Dim insArticulo As clsArticuloSQL
   Dim vResult As String
   On Error GoTo h_ERROR
   Set insArticulo = New clsArticuloSQL
   vResult = insArticulo.fArticuloInventarioMaxOrMinCodigo(valConsecutivoCompania, valMax)
   fArticuloInventarioMaxOrMinCodigo = vResult
 Set insArticulo = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fArticuloInventarioMaxOrMinCodigo", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSqlCondicionTipoDocumento() As String
   Dim vResult As String
   Dim gEnumProyecto As clsEnumAdministrativo
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   vResult = "Gf_CostoPromedioPonderado.TipoDeDocumento = " & gUtilSQL.fSimpleSqlValue(gEnumProyecto.enumTipodocumentoFacturaToString(eTF_FACTURA))
   vResult = vResult & " OR Gf_CostoPromedioPonderado.TipoDeDocumento= " & gUtilSQL.fSimpleSqlValue(gEnumProyecto.enumTipodocumentoFacturaToString(eTF_COMPROBANTEFISCAL))
   fSqlCondicionTipoDocumento = "(" & vResult & ")"
   Set gEnumProyecto = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlCondicionTipoDocumento", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLCoutComprobantes(ByVal valConsecutivoCompania As Long, ByVal valGeneradoPor As enum_ComprobanteGeneradoPor, ByVal valFechaOperacion As Date) As String
   Dim vSql As String
   Dim vWhere As String
   On Error GoTo h_ERROR
   vWhere = gUtilSQL.fSQLValueDateWithComparisonRule("", "COMPROBANTE.Fecha", valFechaOperacion, ">=")
   vWhere = gUtilSQL.fSQLValueWithAnd(vWhere, "COMPROBANTE.GeneradoPor", gConvert.enumerativoAChar(valGeneradoPor), False)
   vWhere = gUtilSQL.fSQLNumberValueWithAnd(vWhere, "periodo.ConsecutivoCompania", valConsecutivoCompania)
   
   vSql = " SELECT COUNT(Numero) AS  Total FROM periodo INNER JOIN  COMPROBANTE ON periodo.ConsecutivoPeriodo = COMPROBANTE.ConsecutivoPeriodo " & gUtilSQL.getWhereSQL(vWhere)
   fSQLCoutComprobantes = vSql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLCoutComprobantes", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLFechaMaxComprobanteDelPeriodoInventario(ByVal valConsecutivoPerido As Long) As String
   Dim vSql As String
   Dim vWhere As String
   On Error GoTo h_ERROR
   vWhere = gUtilSQL.fSQLEnumValueWithAnd(vWhere, "COMPROBANTE.GeneradoPor", enum_ComprobanteGeneradoPor.eCG_INVENTARIO)
   vWhere = gUtilSQL.fSQLNumberValueWithAnd(vWhere, "COMPROBANTE.ConsecutivoPeriodo", valConsecutivoPerido)
   
   vSql = " SELECT  MAX(COMPROBANTE.Fecha) FROM   COMPROBANTE  " & gUtilSQL.getWhereSQL(vWhere)
 fSQLFechaMaxComprobanteDelPeriodoInventario = vSql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLFechaMaxComprobanteDelPeriodoInventario", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLDeleteComprobantesDeCostoDeInventarioEntreFechas(ByVal valConsecutivoPerido As Long, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date) As String
   Dim vSql As String
   Dim vWhere As String
   On Error GoTo h_ERROR
   vWhere = gUtilSQL.fSQLValueDatesBetween("", "COMPROBANTE.Fecha", valFechaInicial, valFechaFinal)
   vWhere = gUtilSQL.fSQLEnumValueWithAnd(vWhere, "COMPROBANTE.GeneradoPor", enum_ComprobanteGeneradoPor.eCG_INVENTARIO)
   vWhere = gUtilSQL.fSQLNumberValueWithAnd(vWhere, "COMPROBANTE.ConsecutivoPeriodo", valConsecutivoPerido)
   
   vSql = " DELETE FROM  COMPROBANTE " & gUtilSQL.getWhereSQL(vWhere)
  fSQLDeleteComprobantesDeCostoDeInventarioEntreFechas = vSql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLDeleteComprobantesDeCostoDeInventarioEntreFechas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
