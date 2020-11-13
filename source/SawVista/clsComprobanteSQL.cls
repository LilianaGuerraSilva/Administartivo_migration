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
   Dim vSQL As String
   Dim vWhere As String
   On Error GoTo h_ERROR
   vWhere = ""
  
   vWhere = gUtilSQL.fSQLValueDateWithComparisonRule("", "COMPROBANTE.Fecha", valFecha, "=")
   vWhere = gUtilSQL.fSQLEnumValueWithAnd(vWhere, "COMPROBANTE.GeneradoPor", enum_ComprobanteGeneradoPor.eCG_INVENTARIO)
   vWhere = gUtilSQL.fSQLNumberValueWithAnd(vWhere, "COMPROBANTE.ConsecutivoPeriodo", valConsecutivoPerido)
   vWhere = gUtilSQL.getWhereSQL(vWhere)
   
   vSQL = " SELECT  COMPROBANTE.Fecha "
   vSQL = vSQL & "   FROM   COMPROBANTE  "
   vSQL = vSQL & vWhere
  fSQLFechaUltimoComprobanteDelPeriodo = vSQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLFechaUltimoComprobanteDelPeriodo", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLFechaAContabilizar(ByVal valConsecutivoCompania As Long, ByVal valFechaCierrePeriodo As Date, ByVal valFechaInicioMetodo As Date) As String
   Dim vSQL As String
   Dim vWhere As String
   On Error GoTo h_ERROR
   vWhere = ""

   vWhere = gUtilSQL.fSQLValueDatesBetween("", "COMPROBANTE.Fecha", valFechaInicioMetodo, valFechaCierrePeriodo)
   vWhere = gUtilSQL.fSQLEnumValueWithAnd(vWhere, "COMPROBANTE.GeneradoPor", enum_ComprobanteGeneradoPor.eCG_INVENTARIO)
   vWhere = gUtilSQL.fSQLNumberValueWithAnd(vWhere, "PERIODO.ConsecutivoCompania", valConsecutivoCompania)
   vWhere = gUtilSQL.getWhereSQL(vWhere)

   vSQL = " SELECT ISNULL (  MAX (COMPROBANTE.Fecha)+ 1 , " & gUtilSQL.fDateToSQLValue(gUtilDate.fColocaUltimoDiaDelMes(valFechaInicioMetodo)) & "  )"
   vSQL = vSQL & "   FROM   COMPROBANTE  "
   vSQL = vSQL & "   INNER JOIN "
   vSQL = vSQL & "   dbo.PERIODO ON "
   vSQL = vSQL & "   dbo.COMPROBANTE.ConsecutivoPeriodo = dbo.periodo.ConsecutivoPeriodo "
   vSQL = vSQL & vWhere
 fSQLFechaAContabilizar = vSQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLFechaAContabilizar", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fAgregaUnaCuentaAlSQL(ByVal valDebeOHaber As String, ByVal valEsUnReverso As Boolean, _
            ByVal valMontoDeLaCuenta As String, ByVal valCodigoDeLaCuenta As String, _
            ByVal valLaCuentaUsaAuxiliares As Boolean, ByVal valCodigoDelAuxiliar As String, _
            ByVal valNoDocumentoOrigen As String, ByVal valDescripcionDelAsiento As String, _
            ByVal valCondicionDeStatusAnulado As String, ByVal valCodigoCentroDeCosto As String, ByVal valInsCompaniaActual As Object, ByVal valInsgProyParametrosCompania As Object) As String
     
   Dim SQL As String
   Dim gProyCompaniaActual As Object
   Dim gProyParametrosCompania As Object
   On Error GoTo h_ERROR
   Set gProyCompaniaActual = valInsCompaniaActual
   Set gProyParametrosCompania = valInsgProyParametrosCompania
   SQL = gUtilSQL.fSimpleSqlValue(fCambiaDebeHaberSiEsUnReverso(valDebeOHaber, valEsUnReverso)) & ", "
   SQL = SQL & valMontoDeLaCuenta & ", "
   
   If gTexto.DfInStr(valCodigoDeLaCuenta, gUtilSQL.getPrefijoIIF) <> 0 Then
      SQL = SQL & valCodigoDeLaCuenta & ", "
   Else
      SQL = SQL & "'" & valCodigoDeLaCuenta & "' , "
   End If
   SQL = SQL & fColocaAuxiliarSiAplica(valLaCuentaUsaAuxiliares, valCodigoDelAuxiliar, gProyCompaniaActual.GetUsaAuxiliares)
   If (gProyCompaniaActual.GetUsaCentroDeCostos And gProyParametrosCompania.GetAsociaCentroDeCostoyAlmacen) And valCodigoCentroDeCosto <> "" Then
        SQL = SQL & valCodigoCentroDeCosto & ", "
   Else
   SQL = SQL & gUtilSQL.fSimpleSqlValue("NA") & ", "
   End If
   If gTexto.DfLeft(valNoDocumentoOrigen, 1) <> vbTab Then
         SQL = SQL & valNoDocumentoOrigen & ", "
   Else
      valNoDocumentoOrigen = gTexto.fLimpiaStringDeCharAAmbosLados(valNoDocumentoOrigen, vbTab)
      SQL = SQL & valNoDocumentoOrigen & ", "
   End If
   SQL = SQL & valDescripcionDelAsiento
   fAgregaUnaCuentaAlSQL = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fAgregaUnaCuentaAlSQL", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
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
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fColocaAuxiliarSiAplica", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
 Public Function fSQLCostoDeVenta(ByVal valConsecutivoCompania As Long, ByVal valFechaDesde As Date, ByVal valFechaHasta As Date, ByVal valEsUnReverso As Boolean, ByVal valDetallado As Boolean, ByVal valAgruparPorCuentaArticulo As Boolean, ByVal valInsCompaniaActual As Object, ByVal valInsgProyParametrosCompania As Object, ByVal valInsgProyReglasDeContabilizacion As Object, ByVal valNroDocumentoOrigen As String, ByVal valDescripcionAsiento As String, ByVal valFechaCierreCostoPromedio As Date) As String
Dim vResult As String
vResult = ""
 If (valDetallado) Then
        
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
End Function

Private Function fSelectCostoVentaDetalladoComprobante(ByVal valConsecutivoCompania As Long, ByVal valFechaDesde As Date, ByVal valFechaHasta As Date, ByVal valEsUnReverso As Boolean, ByVal valInsCompaniaActual As Object, ByVal valInsgProyParametrosCompania As Object, ByVal valInsgProyReglasDeContabilizacion As Object, ByVal valFechaCierreCostoPromedio As Date) As String
   Dim gProyReglasDeContabilizacion As Object
   Set gProyReglasDeContabilizacion = valInsgProyReglasDeContabilizacion
   Dim sqlWhere As String
   Dim vSQL As String
   Dim vMonto As String
   On Error GoTo h_ERROR
   vMonto = " ISNULL(SUM (salidas * costofinal),0) "
   sqlWhere = gUtilSQL.fSQLValueDatesBetween(fSqlCondicionTipoDocumento(), "Gf_CostoPromedioPonderado.Fecha", valFechaDesde, valFechaHasta)
   sqlWhere = gUtilSQL.getWhereSQL(sqlWhere)
   vSQL = "  SELECT "
   vSQL = vSQL & " fecha " & " , "
   vSQL = vSQL & fAgregaUnaCuentaAlSQL("DEBE", valEsUnReverso, vMonto, gProyReglasDeContabilizacion.GetCuentaCostoDeVenta, False, "", "numerodocumento", "TipoDeDocumento", "", "", valInsCompaniaActual, valInsgProyParametrosCompania) & " , "
   vSQL = vSQL & fAgregaUnaCuentaAlSQL("HABER", valEsUnReverso, vMonto, gProyReglasDeContabilizacion.GetCuentaInventario, False, "", "numerodocumento AS Numero", "TipoDeDocumento AS TIPO ", "", "", valInsCompaniaActual, valInsgProyParametrosCompania)
 
   vSQL = vSQL & "   FROM   dbo.Gf_CostoPromedioPonderado( " & gUtilSQL.fNumToStrSQL(valConsecutivoCompania) & "," & gUtilSQL.fDateToSQLValue(valFechaCierreCostoPromedio) & "," & gUtilSQL.fDateToSQLValue(valFechaHasta) & "," & fArticuloInventarioMaxOrMinCodigo(valConsecutivoCompania, False) & "," & fArticuloInventarioMaxOrMinCodigo(valConsecutivoCompania, True) & ") AS Gf_CostoPromedioPonderado  "
   vSQL = vSQL & sqlWhere
   vSQL = vSQL & "  group by fecha,numerodocumento,tipodedocumento "
   vSQL = vSQL & " order by fecha  ,numerodocumento"
   fSelectCostoVentaDetalladoComprobante = vSQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSelectCostoVentaDetalladoComprobante", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSelectCostoVentaResumidoComprobante(ByVal valConsecutivoCompania As Long, ByVal valFechaDesde As Date, ByVal valFechaHasta As Date, ByVal valEsUnReverso As Boolean, ByVal valInsCompaniaActual As Object, ByVal valInsgProyParametrosCompania As Object, ByVal valInsgProyReglasDeContabilizacion As Object, ByVal valNroDocumentoOrigen As String, ByVal valDescripcionAsiento As String, ByVal valFechaCierreCostoPromedio As Date) As String
   Dim gProyReglasDeContabilizacion As Object
   Set gProyReglasDeContabilizacion = valInsgProyReglasDeContabilizacion
   Dim sqlWhere As String
   Dim vSQL As String
   Dim vMonto As String
   On Error GoTo h_ERROR
   vMonto = " ISNULL(SUM (salidas * costofinal),0) "
   sqlWhere = gUtilSQL.fSQLValueDatesBetween(fSqlCondicionTipoDocumento(), "Gf_CostoPromedioPonderado.Fecha", valFechaDesde, valFechaHasta)
   sqlWhere = gUtilSQL.getWhereSQL(sqlWhere)
   vSQL = "  SELECT "
   vSQL = vSQL & gUtilSQL.fDateToSQLValue(valFechaHasta) & " AS FECHA " & " , "
   vSQL = vSQL & fAgregaUnaCuentaAlSQL("DEBE", valEsUnReverso, vMonto, gProyReglasDeContabilizacion.GetCuentaCostoDeVenta, False, "", gUtilSQL.fSimpleSqlValue(valNroDocumentoOrigen), gUtilSQL.fSimpleSqlValue(valDescripcionAsiento), "", "", valInsCompaniaActual, valInsgProyParametrosCompania) & " , "
   vSQL = vSQL & fAgregaUnaCuentaAlSQL("HABER", valEsUnReverso, vMonto, gProyReglasDeContabilizacion.GetCuentaInventario, False, "", gUtilSQL.fSimpleSqlValue(valNroDocumentoOrigen), gUtilSQL.fSimpleSqlValue(valDescripcionAsiento), "", "", valInsCompaniaActual, valInsgProyParametrosCompania)

   vSQL = vSQL & "   FROM   dbo.Gf_CostoPromedioPonderado( " & gUtilSQL.fNumToStrSQL(valConsecutivoCompania) & "," & gUtilSQL.fDateToSQLValue(valFechaCierreCostoPromedio) & "," & gUtilSQL.fDateToSQLValue(valFechaHasta) & "," & fArticuloInventarioMaxOrMinCodigo(valConsecutivoCompania, False) & "," & fArticuloInventarioMaxOrMinCodigo(valConsecutivoCompania, True) & ") AS Gf_CostoPromedioPonderado "
   vSQL = vSQL & sqlWhere
   fSelectCostoVentaResumidoComprobante = vSQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSelectCostoVentaDetalladoComprobante", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSelectCostoVentaDetalladoAgrupadoPorCuentaArticuloComprobante(ByVal valConsecutivoCompania As Long, ByVal valFechaDesde As Date, ByVal valFechaHasta As Date, ByVal valEsUnReverso As Boolean, ByVal valInsCompaniaActual As Object, ByVal valInsgProyParametrosCompania As Object, ByVal valInsgProyReglasDeContabilizacion As Object, ByVal valFechaCierreCostoPromedio As Date) As String
   Dim gProyReglasDeContabilizacion As Object
   Set gProyReglasDeContabilizacion = valInsgProyReglasDeContabilizacion
   Dim sqlWhere As String
   Dim sqlCuentaInvHaber As String
   Dim sqlCuentaInvDebe As String
   Dim vSQL As String
   Dim vMonto As String
   On Error GoTo h_ERROR
   vMonto = " ISNULL(SUM (salidas * costofinal),0) "
   
   sqlCuentaInvHaber = " ArticuloInventario.CuentaInventario = " & gUtilSQL.fSimpleSqlValue("") & " OR " & gUtilSQL.DfSQLIsNull("ArticuloInventario.CuentaInventario")
   sqlCuentaInvHaber = gUtilSQL.getIIF(sqlCuentaInvHaber, gUtilSQL.fSimpleSqlValue(gProyReglasDeContabilizacion.GetCuentaInventario), "ArticuloInventario.CuentaInventario", True)
   
   
   sqlCuentaInvDebe = " ArticuloInventario.CuentaCostoDeVenta = " & gUtilSQL.fSimpleSqlValue("") & " OR " & gUtilSQL.DfSQLIsNull("ArticuloInventario.CuentaCostoDeVenta")
   sqlCuentaInvDebe = gUtilSQL.getIIF(sqlCuentaInvDebe, gUtilSQL.fSimpleSqlValue(gProyReglasDeContabilizacion.GetCuentaCostoDeVenta), "ArticuloInventario.CuentaCostoDeVenta", True)
   
   
   sqlWhere = gUtilSQL.fSQLValueDatesBetween(fSqlCondicionTipoDocumento(), "Gf_CostoPromedioPonderado.Fecha", valFechaDesde, valFechaHasta)
   sqlWhere = gUtilSQL.getWhereSQL(sqlWhere)
   
   vSQL = "  SELECT "
   vSQL = vSQL & " fecha " & " , "
   vSQL = vSQL & fAgregaUnaCuentaAlSQL("DEBE", valEsUnReverso, vMonto, sqlCuentaInvDebe, False, "", "numerodocumento", "TipoDeDocumento", gUtilSQL.fSimpleSqlValue(""), gUtilSQL.fSimpleSqlValue(""), valInsCompaniaActual, valInsgProyParametrosCompania)
  
   vSQL = vSQL & " FROM   dbo.Gf_CostoPromedioPonderado( " & gUtilSQL.fNumToStrSQL(valConsecutivoCompania) & "," & gUtilSQL.fDateToSQLValue(valFechaCierreCostoPromedio) & "," & gUtilSQL.fDateToSQLValue(valFechaHasta) & "," & fArticuloInventarioMaxOrMinCodigo(valConsecutivoCompania, False) & "," & fArticuloInventarioMaxOrMinCodigo(valConsecutivoCompania, True) & ") AS Gf_CostoPromedioPonderado  INNER JOIN "
   vSQL = vSQL & " ArticuloInventario ON "
   vSQL = vSQL & " Gf_CostoPromedioPonderado.Codigo = dbo.ArticuloInventario.Codigo AND "
   vSQL = vSQL & gUtilSQL.fNumToStrSQL(valConsecutivoCompania) & "  = ArticuloInventario.ConsecutivoCompania "
   
   vSQL = vSQL & sqlWhere
   vSQL = vSQL & "  group by fecha,numerodocumento,tipodedocumento,CuentaCostoDeVenta"

   vSQL = vSQL & " UNION "
   vSQL = vSQL & " SELECT "
   vSQL = vSQL & " fecha " & " , "
   vSQL = vSQL & fAgregaUnaCuentaAlSQL("HABER", valEsUnReverso, vMonto, sqlCuentaInvHaber, False, "", "numerodocumento", "TipoDeDocumento", gUtilSQL.fSimpleSqlValue(""), gUtilSQL.fSimpleSqlValue(""), valInsCompaniaActual, valInsgProyParametrosCompania)
   
   
   vSQL = vSQL & " FROM   dbo.Gf_CostoPromedioPonderado( " & gUtilSQL.fNumToStrSQL(valConsecutivoCompania) & "," & gUtilSQL.fDateToSQLValue(valFechaCierreCostoPromedio) & "," & gUtilSQL.fDateToSQLValue(valFechaHasta) & "," & fArticuloInventarioMaxOrMinCodigo(valConsecutivoCompania, False) & "," & fArticuloInventarioMaxOrMinCodigo(valConsecutivoCompania, True) & ") AS Gf_CostoPromedioPonderado INNER JOIN "
   vSQL = vSQL & " ArticuloInventario ON "
   vSQL = vSQL & " Gf_CostoPromedioPonderado.Codigo = dbo.ArticuloInventario.Codigo AND "
   vSQL = vSQL & gUtilSQL.fNumToStrSQL(valConsecutivoCompania) & "  = ArticuloInventario.ConsecutivoCompania "
   
   vSQL = vSQL & sqlWhere
   vSQL = vSQL & " group by fecha,numerodocumento,tipodedocumento,CuentaInventario "
   vSQL = vSQL & " order by fecha  ,numerodocumento"
   fSelectCostoVentaDetalladoAgrupadoPorCuentaArticuloComprobante = vSQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSelectCostoVentaDetalladoComprobante", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSelectCostoVentaResumidoAgrupadoPorCuentaArticuloComprobante(ByVal valConsecutivoCompania As Long, ByVal valFechaDesde As Date, ByVal valFechaHasta As Date, ByVal valEsUnReverso As Boolean, ByVal valInsCompaniaActual As Object, ByVal valInsgProyParametrosCompania As Object, ByVal valInsgProyReglasDeContabilizacion As Object, ByVal valNroDocumentoOrigen As String, ByVal valDescripcionAsiento As String, ByVal valFechaCierreCostoPromedio As Date) As String
   Dim gProyReglasDeContabilizacion As Object
   Set gProyReglasDeContabilizacion = valInsgProyReglasDeContabilizacion
   Dim sqlWhere As String
   Dim sqlCuentaInvHaber As String
   Dim sqlCuentaInvDebe As String
   Dim vSQL As String
   Dim vMonto As String
   On Error GoTo h_ERROR
   vMonto = " ISNULL(SUM (salidas * costofinal),0) "
   
   sqlCuentaInvHaber = " ArticuloInventario.CuentaInventario = " & gUtilSQL.fSimpleSqlValue("") & " OR " & gUtilSQL.DfSQLIsNull("ArticuloInventario.CuentaInventario")
   sqlCuentaInvHaber = gUtilSQL.getIIF(sqlCuentaInvHaber, gUtilSQL.fSimpleSqlValue(gProyReglasDeContabilizacion.GetCuentaInventario), "ArticuloInventario.CuentaInventario", True)
   
   sqlCuentaInvDebe = " ArticuloInventario.CuentaCostoDeVenta = " & gUtilSQL.fSimpleSqlValue("") & " OR " & gUtilSQL.DfSQLIsNull("ArticuloInventario.CuentaCostoDeVenta")
   sqlCuentaInvDebe = gUtilSQL.getIIF(sqlCuentaInvDebe, gUtilSQL.fSimpleSqlValue(gProyReglasDeContabilizacion.GetCuentaCostoDeVenta), "ArticuloInventario.CuentaCostoDeVenta", True)
  
   
   sqlWhere = gUtilSQL.fSQLValueDatesBetween(fSqlCondicionTipoDocumento(), "Gf_CostoPromedioPonderado.Fecha", valFechaDesde, valFechaHasta)
   sqlWhere = gUtilSQL.getWhereSQL(sqlWhere)
   
   vSQL = "  SELECT "
   vSQL = vSQL & gUtilSQL.fDateToSQLValue(valFechaHasta) & " AS FECHA " & " , "
   vSQL = vSQL & fAgregaUnaCuentaAlSQL("DEBE", valEsUnReverso, vMonto, sqlCuentaInvDebe, False, "", gUtilSQL.fSimpleSqlValue(valNroDocumentoOrigen), gUtilSQL.fSimpleSqlValue(valDescripcionAsiento), gUtilSQL.fSimpleSqlValue(""), gUtilSQL.fSimpleSqlValue(""), valInsCompaniaActual, valInsgProyParametrosCompania)
  
   vSQL = vSQL & " FROM   dbo.Gf_CostoPromedioPonderado( " & gUtilSQL.fNumToStrSQL(valConsecutivoCompania) & "," & gUtilSQL.fDateToSQLValue(valFechaCierreCostoPromedio) & "," & gUtilSQL.fDateToSQLValue(valFechaHasta) & "," & fArticuloInventarioMaxOrMinCodigo(valConsecutivoCompania, False) & "," & fArticuloInventarioMaxOrMinCodigo(valConsecutivoCompania, True) & ") AS Gf_CostoPromedioPonderado  INNER JOIN "
   vSQL = vSQL & " ArticuloInventario ON "
   vSQL = vSQL & " Gf_CostoPromedioPonderado.Codigo = dbo.ArticuloInventario.Codigo AND "
   vSQL = vSQL & gUtilSQL.fNumToStrSQL(valConsecutivoCompania) & "  = ArticuloInventario.ConsecutivoCompania "
   vSQL = vSQL & sqlWhere
   vSQL = vSQL & " group by ArticuloInventario.CuentaCostoDeVenta "
 
   vSQL = vSQL & " UNION "
   vSQL = vSQL & " SELECT "
   vSQL = vSQL & gUtilSQL.fDateToSQLValue(valFechaHasta) & " AS FECHA " & " , "
   vSQL = vSQL & fAgregaUnaCuentaAlSQL("HABER", valEsUnReverso, vMonto, sqlCuentaInvHaber, False, "", gUtilSQL.fSimpleSqlValue(valNroDocumentoOrigen), gUtilSQL.fSimpleSqlValue(valDescripcionAsiento), gUtilSQL.fSimpleSqlValue(""), gUtilSQL.fSimpleSqlValue(""), valInsCompaniaActual, valInsgProyParametrosCompania)
   
   
   vSQL = vSQL & " FROM   dbo.Gf_CostoPromedioPonderado( " & gUtilSQL.fNumToStrSQL(valConsecutivoCompania) & "," & gUtilSQL.fDateToSQLValue(valFechaCierreCostoPromedio) & "," & gUtilSQL.fDateToSQLValue(valFechaHasta) & "," & fArticuloInventarioMaxOrMinCodigo(valConsecutivoCompania, False) & "," & fArticuloInventarioMaxOrMinCodigo(valConsecutivoCompania, True) & ") AS Gf_CostoPromedioPonderado INNER JOIN "
   vSQL = vSQL & " ArticuloInventario ON "
   vSQL = vSQL & " Gf_CostoPromedioPonderado.Codigo = dbo.ArticuloInventario.Codigo AND "
   vSQL = vSQL & gUtilSQL.fNumToStrSQL(valConsecutivoCompania) & "  = ArticuloInventario.ConsecutivoCompania "
   
   vSQL = vSQL & sqlWhere
   vSQL = vSQL & " group by CuentaInventario "
   
   fSelectCostoVentaResumidoAgrupadoPorCuentaArticuloComprobante = vSQL
h_EXIT: On Error GoTo 0
   Exit Function
   

h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSelectCostoVentaDetalladoComprobante", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
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
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fArticuloInventarioMaxOrMinCodigo", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function


Private Function fSqlCondicionTipoDocumento() As String
   Dim vResult As String
   Dim gEnumProyecto As clsEnumAdministrativo
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   vResult = ""
   vResult = "Gf_CostoPromedioPonderado" & ".TipoDeDocumento = " & gUtilSQL.fSimpleSqlValue(gEnumProyecto.enumTipodocumentoFacturaToString(eTF_FACTURA))
   vResult = vResult & " OR " & "Gf_CostoPromedioPonderado" & ".TipoDeDocumento= " & gUtilSQL.fSimpleSqlValue(gEnumProyecto.enumTipodocumentoFacturaToString(eTF_COMPROBANTEFISCAL))
   fSqlCondicionTipoDocumento = "(" & vResult & ")"
   Set gEnumProyecto = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSqlCondicionTipoDocumento", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLCoutComprobantes(ByVal valConsecutivoCompania As Long, ByVal valGeneradoPor As enum_ComprobanteGeneradoPor, ByVal valFechaOperacion As Date) As String
   Dim vSQL As String
   Dim vWhere As String
   On Error GoTo h_ERROR
   vWhere = ""
   vWhere = gUtilSQL.fSQLValueDateWithComparisonRule("", "COMPROBANTE.Fecha", valFechaOperacion, ">=")
   vWhere = gUtilSQL.fSQLValueWithAnd(vWhere, "COMPROBANTE.GeneradoPor", gConvert.enumerativoAChar(valGeneradoPor), False)
   vWhere = gUtilSQL.fSQLNumberValueWithAnd(vWhere, "periodo.ConsecutivoCompania", valConsecutivoCompania)
   vWhere = gUtilSQL.getWhereSQL(vWhere)
   
   vSQL = " SELECT COUNT(Numero) AS  Total "
   vSQL = vSQL & " FROM periodo "
   vSQL = vSQL & " INNER JOIN  COMPROBANTE ON "
   vSQL = vSQL & " periodo.ConsecutivoPeriodo = COMPROBANTE.ConsecutivoPeriodo "
   vSQL = vSQL & vWhere
   fSQLCoutComprobantes = vSQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLCoutComprobantes", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function fSQLFechaMaxComprobanteDelPeriodoInventario(ByVal valConsecutivoPerido As Long) As String
   Dim vSQL As String
   Dim vWhere As String
   On Error GoTo h_ERROR
   vWhere = ""
   vWhere = gUtilSQL.fSQLEnumValueWithAnd(vWhere, "COMPROBANTE.GeneradoPor", enum_ComprobanteGeneradoPor.eCG_INVENTARIO)
   vWhere = gUtilSQL.fSQLNumberValueWithAnd(vWhere, "COMPROBANTE.ConsecutivoPeriodo", valConsecutivoPerido)
   vWhere = gUtilSQL.getWhereSQL(vWhere)
   
   vSQL = " SELECT  MAX(COMPROBANTE.Fecha) "
   vSQL = vSQL & "   FROM   COMPROBANTE  "
   vSQL = vSQL & vWhere
 fSQLFechaMaxComprobanteDelPeriodoInventario = vSQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLFechaMaxComprobanteDelPeriodoInventario", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLDeleteComprobantesDeCostoDeInventarioEntreFechas(ByVal valConsecutivoPerido As Long, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date) As String
   Dim vSQL As String
   Dim vWhere As String
   On Error GoTo h_ERROR
   vWhere = ""
   vWhere = gUtilSQL.fSQLValueDatesBetween("", "COMPROBANTE.Fecha", valFechaInicial, valFechaFinal)
   vWhere = gUtilSQL.fSQLEnumValueWithAnd(vWhere, "COMPROBANTE.GeneradoPor", enum_ComprobanteGeneradoPor.eCG_INVENTARIO)
   vWhere = gUtilSQL.fSQLNumberValueWithAnd(vWhere, "COMPROBANTE.ConsecutivoPeriodo", valConsecutivoPerido)
   vWhere = gUtilSQL.getWhereSQL(vWhere)
      
   vSQL = "  DELETE FROM  COMPROBANTE  "
   vSQL = vSQL & vWhere
  fSQLDeleteComprobantesDeCostoDeInventarioEntreFechas = vSQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLDeleteComprobantesDeCostoDeInventarioEntreFechas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function


