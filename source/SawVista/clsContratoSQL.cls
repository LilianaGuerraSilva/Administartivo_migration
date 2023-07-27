VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsContratoSQL"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit
Private Const CM_FILE_NAME As String = "clsContratoSQL"
Private Const CM_MESSAGE_NAME As String = "Contrato SQL"

Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function

Public Function fSQLgenerarContrato(ByVal valConsecutivoCompania As Long, ByVal valCantidadAGenerar As Boolean, ByVal valNumeroContrato As String, ByVal valMesAnoGeneracion As Date, ByVal valOrden As String) As String
   Dim SQL As String
   Dim gEnumProyecto As clsEnumAdministrativo
   Dim mesGeneracion, mesInicio, mesFinal As String
   Dim anoGeneracion, anoInicio, anoFinal As String
   Dim valConcatFecha As String
   Dim fechainicial, fechaFinal As String
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   mesGeneracion = gUtilSQL.DfRightSQL(gUtilSQL.fCast(gUtilDate.fMonth(valMesAnoGeneracion), eTDSS_VARCHAR, ""), 2, "0")
   mesInicio = gUtilSQL.DfRightSQL(gUtilSQL.fCast(gUtilSQL.DfSQLMonthOfDate("contrato.FechaDeInicio"), eTDSS_VARCHAR, ""), 2, "0")
   mesFinal = gUtilSQL.DfRightSQL(gUtilSQL.fCast(gUtilSQL.DfSQLMonthOfDate("contrato.FechaFinal"), eTDSS_VARCHAR, ""), 2, 0)

   anoGeneracion = gUtilSQL.fCast(gUtilDate.fYear(valMesAnoGeneracion), eTDSS_VARCHAR, "")
   anoInicio = gUtilSQL.fCast(gUtilSQL.DfSQLYearOfDate("contrato.FechaDeInicio"), eTDSS_VARCHAR, "")
   anoFinal = gUtilSQL.fCast(gUtilSQL.DfSQLYearOfDate("contrato.FechaFinal"), eTDSS_VARCHAR, "")

   valConcatFecha = gConvert.fConvierteAString(anoGeneracion) & gUtilSQL.CharConcat & mesGeneracion
        
   SQL = " SELECT " & gUtilSQL.getIIF("FacturarAOtroCliente = " & gUtilSQL.fBooleanToSqlValue(True), "contrato.CodigoClienteAfacturar", "contrato.CodigoCliente") & " AS CodigoCliente,"
   SQL = SQL & " contrato.NumeroContrato, " & _
         "contrato.Observaciones, contrato.DuracionDelContrato, " & _
         "contrato.Moneda, contrato.CodigoVendedor, "
   SQL = SQL & gUtilSQL.getIIF("FacturarAOtroCliente = " & gUtilSQL.fBooleanToSqlValue(True), "contrato.NombreClienteAfacturar", "Cliente.Nombre") & " AS NombreDelCliente,"
   SQL = SQL & " vendedor.Nombre AS NombreDelVendedor, vendedor.Consecutivo AS ConsecutivoVendedor, contrato.CodigoMoneda"
   SQL = SQL & " FROM contrato INNER JOIN cliente"
   SQL = SQL & " ON ( contrato.ConsecutivoCompania = cliente.ConsecutivoCompania"
   SQL = SQL & " AND cliente.Codigo =  contrato.CodigoCliente)"
   SQL = SQL & " INNER JOIN adm.vendedor"
   SQL = SQL & " ON (contrato.ConsecutivoCompania = vendedor.ConsecutivoCompania"
   SQL = SQL & " AND vendedor.Codigo = contrato.CodigoVendedor)"
   SQL = SQL & " WHERE contrato.ConsecutivoCompania = " & valConsecutivoCompania & _
         " AND contrato.StatusContrato = " & gUtilSQL.fSimpleSqlValue(Trim(gEnumProyecto.strStatusContratoToNum(gEnumProyecto.enumStatusContratoToString(eSCo_VIGENTE))))

         
   If valCantidadAGenerar Then
      SQL = SQL & " AND contrato.NumeroContrato = " & gUtilSQL.fSimpleSqlValue(valNumeroContrato)
   End If
      
   SQL = SQL & " AND ((" & valConcatFecha & _
   " BETWEEN " & anoInicio & gUtilSQL.CharConcat & mesInicio & " And " & anoFinal & gUtilSQL.CharConcat & mesFinal _
   & ") OR ( " & valConcatFecha & " >= " & anoInicio & gUtilSQL.CharConcat & mesInicio _
   & " AND contrato.DuracionDelContrato = " _
   & Trim(gEnumProyecto.strDuracionDelContratoToNum(gEnumProyecto.enumDuracionDelContratoToString(eDD_DURACIONINDEFINIDA))) _
   & "))"
      
   If Not valCantidadAGenerar Then
      If valOrden = gEnumProyecto.enumFormaDeEscogerCompaniaToString(eFD_PORCODIGO) Then
         SQL = SQL & " ORDER BY contrato.CodigoCliente"
      Else
         SQL = SQL & " ORDER BY cliente.Nombre"
      End If
   End If

   Set gEnumProyecto = Nothing
h_EXIT: On Error GoTo 0
   fSQLgenerarContrato = SQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLgenerarContrato", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLBuscaRenglonesDelContratoQueDebeSerIncluidoEnLaFactura(ByVal valConsecutivoCompania As Long, ByVal valMesAnoGeneracion As Date, ByVal valNumeroContrato As String) As String
   Dim SQL As String
   Dim mesGeneracion, mesInicio, mesFinal, mesFactura As String
   Dim anoGeneracion, anoInicio, anoFinal, anoFactura As String
   Dim valConcatFecha, valFechaActual As String
   Dim valFechaGeneracion As String
   On Error GoTo h_ERROR
   
   mesGeneracion = gUtilSQL.DfRightSQL(gUtilSQL.fCast(gUtilDate.fMonth(valMesAnoGeneracion), eTDSS_VARCHAR, ""), 2, "0")
   mesGeneracion = gUtilSQL.DfRightSQL(gUtilSQL.fCast(gUtilDate.fMonth(valMesAnoGeneracion), eTDSS_VARCHAR, ""), 2, "0")
   anoGeneracion = gUtilSQL.fCast(gUtilDate.fYear(valMesAnoGeneracion), eTDSS_VARCHAR, "")
   mesInicio = gUtilSQL.DfRightSQL(gUtilSQL.fCast(gUtilSQL.DfSQLMonthOfDate("RenglonContrato.FechaDeInicio"), eTDSS_VARCHAR, ""), 2, "0")
   anoInicio = gUtilSQL.fCast(gUtilSQL.DfSQLYearOfDate("RenglonContrato.FechaDeInicio"), eTDSS_VARCHAR, "")
   mesFinal = gUtilSQL.DfRightSQL(gUtilSQL.fCast(gUtilSQL.DfSQLMonthOfDate("RenglonContrato.FechaFinal"), eTDSS_VARCHAR, ""), 2, "0")
   anoFinal = gUtilSQL.fCast(gUtilSQL.DfSQLYearOfDate("RenglonContrato.FechaFinal"), eTDSS_VARCHAR, "")
   mesFactura = gUtilSQL.DfRightSQL(gUtilSQL.fCast(gUtilSQL.DfSQLMonthOfDate("RenglonContrato.FechaPrimeraFactura"), eTDSS_VARCHAR, ""), 2, "0")
   anoFactura = gUtilSQL.fCast(gUtilSQL.DfSQLYearOfDate("RenglonContrato.FechaPrimeraFactura"), eTDSS_VARCHAR, "")
   valFechaActual = gUtilSQL.fCast(gUtilDate.fYear(gUtilDate.getFechaDeHoy), eTDSS_VARCHAR, "") & gUtilSQL.CharConcat & gUtilSQL.DfRightSQL(gUtilSQL.fCast(gUtilDate.fMonth(gUtilDate.getFechaDeHoy), eTDSS_VARCHAR, ""), 2, "0")
   valFechaGeneracion = anoGeneracion & gUtilSQL.CharConcat & mesGeneracion

   SQL = "SELECT "
   SQL = SQL & "RenglonContrato.ConsecutivoCompania, "
   SQL = SQL & "RenglonContrato.NumeroContrato, "
   SQL = SQL & "RenglonContrato.ConsecutivoContrato, "
   SQL = SQL & "RenglonContrato.Articulo, "
   SQL = SQL & "RenglonContrato.Descripcion, "
   SQL = SQL & "RenglonContrato.CambioDeValorDelItem, "
   SQL = SQL & "RenglonContrato.Imponible, "
   SQL = SQL & "RenglonContrato.Cantidad, "
   SQL = SQL & "RenglonContrato.Periodicidad, "
   SQL = SQL & "RenglonContrato.PeriodoDeAplicacion, "
   SQL = SQL & "RenglonContrato.FechaDeInicio, "
   SQL = SQL & "RenglonContrato.FechaFinal, "
   SQL = SQL & "RenglonContrato.FechaPrimeraFactura, "
   SQL = SQL & "Contrato.DuracionDelContrato, "
   SQL = SQL & "RenglonContrato.PorcentajeDescuento "
   SQL = SQL & "FROM RenglonContrato INNER JOIN "
   SQL = SQL & " Contrato ON RenglonContrato.ConsecutivoCompania = Contrato.ConsecutivoCompania "
   SQL = SQL & " AND RenglonContrato.NumeroContrato = Contrato.NumeroContrato"
   SQL = SQL & " WHERE RenglonContrato.ConsecutivoCompania = " & valConsecutivoCompania
   SQL = SQL & " AND RenglonContrato.NumeroContrato = " & gUtilSQL.fSimpleSqlValue(valNumeroContrato)
   '--- Validaciones de que renglones aplican para Factura -----
   '--- Validación que el contrato está en el Rango
   
   SQL = SQL & " AND (" & anoFactura & gUtilSQL.CharConcat & mesFactura & " <= " & valFechaGeneracion & _
            " AND " & anoInicio & gUtilSQL.CharConcat & mesInicio & " <= " & valFechaGeneracion & ")"
               
   '--- Validación del contrato tiene duracion Fija o no se ha acabado
               
   SQL = SQL & " AND ((Contrato.DuracionDelContrato = " & gUtilSQL.fSimpleSqlValue(enum_DuracionDelContrato.eDD_DURACIONINDEFINIDA)
   SQL = SQL & " ) OR (RenglonContrato.PeriodoDeAplicacion = " & enum_PeriodoDeAplicacion.ePD_ESPECIALINDICAR
   SQL = SQL & " AND " & anoFinal & gUtilSQL.CharConcat & mesFinal & " >= " & valFechaGeneracion
   SQL = SQL & ") OR " & anoFinal & gUtilSQL.CharConcat & mesFinal & " >= " & valFechaGeneracion
   SQL = SQL & ")"
   
'   SQL = SQL & ")" 'final del and de duración indefinida

   
   '--- Valida Que Renglones Aplican para el Mes en cuestion
   SQL = SQL & " AND ("
   SQL = SQL & " (RenglonContrato.Periodicidad = " & gUtilSQL.fSimpleSqlValue(enum_PeriodicidadContratos.ePC_MENSUAL) & ") "
   SQL = SQL & " OR (RenglonContrato.Periodicidad = " & gUtilSQL.fSimpleSqlValue(enum_PeriodicidadContratos.ePC_BIMESTRAL)
   SQL = SQL & " AND (" & gUtilDate.fMonth(valMesAnoGeneracion) & " = " & gUtilSQL.DfSQLMonthOfDate("RenglonContrato.FechaPrimeraFactura")
   SQL = SQL & " OR (" & gUtilDate.fMonth(valMesAnoGeneracion) & " = (" & gUtilSQL.getIIF(gUtilSQL.DfSQLMonthOfDate("RenglonContrato.FechaPrimeraFactura") & " + 2 <= 12 ", gUtilSQL.DfSQLMonthOfDate("RenglonContrato.FechaPrimeraFactura") & " + 2", "(" & gUtilSQL.DfSQLMonthOfDate("RenglonContrato.FechaPrimeraFactura") & " + 2) - 12 ") & "))"
   SQL = SQL & " OR (" & gUtilDate.fMonth(valMesAnoGeneracion) & " = (" & gUtilSQL.getIIF(gUtilSQL.DfSQLMonthOfDate("RenglonContrato.FechaPrimeraFactura") & " + 4 <= 12 ", gUtilSQL.DfSQLMonthOfDate("RenglonContrato.FechaPrimeraFactura") & " + 4", "(" & gUtilSQL.DfSQLMonthOfDate("RenglonContrato.FechaPrimeraFactura") & " + 4) - 12") & "))"
   SQL = SQL & " OR (" & gUtilDate.fMonth(valMesAnoGeneracion) & " = (" & gUtilSQL.getIIF(gUtilSQL.DfSQLMonthOfDate("RenglonContrato.FechaPrimeraFactura") & " + 6 <= 12 ", gUtilSQL.DfSQLMonthOfDate("RenglonContrato.FechaPrimeraFactura") & " + 6", "(" & gUtilSQL.DfSQLMonthOfDate("RenglonContrato.FechaPrimeraFactura") & " + 6) - 12 ") & "))"
   SQL = SQL & " OR (" & gUtilDate.fMonth(valMesAnoGeneracion) & " = (" & gUtilSQL.getIIF(gUtilSQL.DfSQLMonthOfDate("RenglonContrato.FechaPrimeraFactura") & " + 8 <= 12 ", gUtilSQL.DfSQLMonthOfDate("RenglonContrato.FechaPrimeraFactura") & " + 8", "(" & gUtilSQL.DfSQLMonthOfDate("RenglonContrato.FechaPrimeraFactura") & " + 8) - 12 ") & "))"
   SQL = SQL & " OR (" & gUtilDate.fMonth(valMesAnoGeneracion) & " = (" & gUtilSQL.getIIF(gUtilSQL.DfSQLMonthOfDate("RenglonContrato.FechaPrimeraFactura") & " + 10 <= 12 ", gUtilSQL.DfSQLMonthOfDate("RenglonContrato.FechaPrimeraFactura") & " + 10", "(" & gUtilSQL.DfSQLMonthOfDate("RenglonContrato.FechaPrimeraFactura") & " + 10) - 12") & "))"
   SQL = SQL & " )" ' parentensis del primer or para bimestral
   SQL = SQL & " )" ' parentensis del AND para bimestral
   SQL = SQL & " OR (RenglonContrato.Periodicidad = " & gUtilSQL.fSimpleSqlValue(enum_PeriodicidadContratos.ePC_TRIMESTRAL)
   SQL = SQL & " AND (" & gUtilDate.fMonth(valMesAnoGeneracion) & " = " & gUtilSQL.DfSQLMonthOfDate("RenglonContrato.FechaPrimeraFactura")
   SQL = SQL & " OR (" & gUtilDate.fMonth(valMesAnoGeneracion) & " = (" & gUtilSQL.getIIF(gUtilSQL.DfSQLMonthOfDate("RenglonContrato.FechaPrimeraFactura") & " + 3 <= 12 ", gUtilSQL.DfSQLMonthOfDate("RenglonContrato.FechaPrimeraFactura") & " + 3", "(" & gUtilSQL.DfSQLMonthOfDate("RenglonContrato.FechaPrimeraFactura") & " + 3) - 12 ") & "))"
   SQL = SQL & " OR (" & gUtilDate.fMonth(valMesAnoGeneracion) & " = (" & gUtilSQL.getIIF(gUtilSQL.DfSQLMonthOfDate("RenglonContrato.FechaPrimeraFactura") & " + 6 <= 12 ", gUtilSQL.DfSQLMonthOfDate("RenglonContrato.FechaPrimeraFactura") & " + 6", "(" & gUtilSQL.DfSQLMonthOfDate("RenglonContrato.FechaPrimeraFactura") & " + 6) - 12") & "))"
   SQL = SQL & " OR (" & gUtilDate.fMonth(valMesAnoGeneracion) & " = (" & gUtilSQL.getIIF(gUtilSQL.DfSQLMonthOfDate("RenglonContrato.FechaPrimeraFactura") & " + 9 <= 12 ", gUtilSQL.DfSQLMonthOfDate("RenglonContrato.FechaPrimeraFactura") & " + 9", "(" & gUtilSQL.DfSQLMonthOfDate("RenglonContrato.FechaPrimeraFactura") & " + 9) - 12") & "))"
   SQL = SQL & " )" 'parentesis del and trimestral"
   SQL = SQL & " )" 'parentesis del or trimestral
   SQL = SQL & " OR (RenglonContrato.Periodicidad = " & gUtilSQL.fSimpleSqlValue(enum_PeriodicidadContratos.ePC_SEMESTRAL)
   SQL = SQL & " AND (" & gUtilDate.fMonth(valMesAnoGeneracion) & " = " & gUtilSQL.DfSQLMonthOfDate("RenglonContrato.FechaPrimeraFactura")
   SQL = SQL & " OR (" & gUtilDate.fMonth(valMesAnoGeneracion) & " = (" & gUtilSQL.getIIF(gUtilSQL.DfSQLMonthOfDate("RenglonContrato.FechaPrimeraFactura") & " + 6 <= 12 ", gUtilSQL.DfSQLMonthOfDate("RenglonContrato.FechaPrimeraFactura"), "(" & gUtilSQL.DfSQLMonthOfDate("RenglonContrato.FechaPrimeraFactura") & " + 6) - 12") & "))"
   SQL = SQL & " )" 'parentesis del and semestral"
   SQL = SQL & " )" 'parentesis del or semestral
   SQL = SQL & " OR (RenglonContrato.Periodicidad = " & gUtilSQL.fSimpleSqlValue(enum_PeriodicidadContratos.ePC_ANUAL)
   SQL = SQL & " AND (" & gUtilDate.fMonth(valMesAnoGeneracion) & " = " & gUtilSQL.DfSQLMonthOfDate("RenglonContrato.FechaPrimeraFactura")
   SQL = SQL & " )" 'parentesis del and anual
   SQL = SQL & " )" 'parentesis del or anual
   SQL = SQL & " OR (RenglonContrato.Periodicidad = " & gUtilSQL.fSimpleSqlValue(enum_PeriodicidadContratos.ePC_CUOTAUNICA)
   SQL = SQL & " AND (" & gUtilDate.fMonth(valMesAnoGeneracion) & " = " & gUtilSQL.DfSQLMonthOfDate("RenglonContrato.FechaPrimeraFactura")
   SQL = SQL & " )" 'parentesis del and cuota unica
   SQL = SQL & " )" 'parentesis del or cuota unica
   SQL = SQL & " ) " 'parentesis del AND general'

h_EXIT: On Error GoTo 0
   fSQLBuscaRenglonesDelContratoQueDebeSerIncluidoEnLaFactura = SQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLBuscaRenglonesDelContratoQueDebeSerIncluidoEnLaFactura", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function


Public Function AjustarFechasContrato(ByVal valConsecutivoCompania As Long) As String
   Dim gEnumProyecto As clsEnumAdministrativo
   Dim SQL As String
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   
   SQL = "UPDATE rengloncontrato "
   SQL = SQL & " SET rengloncontrato.FechaDeInicio =  contrato.FechaDeInicio, "
   SQL = SQL & " rengloncontrato.FechaFinal = contrato.FechaFinal "
   SQL = SQL & " FROM  rengloncontrato INNER JOIN contrato ON (rengloncontrato.ConsecutivoCompania =  contrato.ConsecutivoCompania AND rengloncontrato.numeroContrato = contrato.numeroContrato) "
   SQL = SQL & " WHERE rengloncontrato.ConsecutivoCompania = " & valConsecutivoCompania
   SQL = SQL & " AND rengloncontrato.PeriodoDeAplicacion = " & gUtilSQL.fSimpleSqlValue(Trim(gEnumProyecto.strPeriodoDeAplicacionToNum(gEnumProyecto.enumPeriodoDeAplicacionToString(ePD_ELDELCONTRATO))))
   SQL = SQL & " AND (rengloncontrato.FechaDeInicio <>  contrato.FechaDeInicio "
   SQL = SQL & " OR rengloncontrato.FechaFinal <>  contrato.FechaFinal) "
   Set gEnumProyecto = Nothing

h_EXIT: On Error GoTo 0
   AjustarFechasContrato = SQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "AjustarFechasContrato", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function BorrarRegistrosMesGenerado(ByVal valConsecutivoCompania As Long) As String
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = "DELETE FROM mesgenerado FROM mesgenerado as mes INNER JOIN factura"
   SQL = SQL & " ON (mes.consecutivocompania = factura.consecutivocompania)"
   SQL = SQL & " WHERE mes.numerocontrato NOT IN ("
   SQL = SQL & " SELECT (" & gUtilSQL.fMid("factura.observaciones", 11, 5) & ") FROM factura"
   SQL = SQL & " WHERE ( " & gUtilSQL.fMid("factura.observaciones", 11, 5) & " IN"
   SQL = SQL & " (SELECT mes2.numerocontrato FROM MESGENERADO as mes2 WHERE mes2.ConsecutivoCompania = " & valConsecutivoCompania & "))"
   SQL = SQL & " AND factura.ConsecutivoCompania = " & valConsecutivoCompania
   SQL = SQL & " AND factura.FechaDeFacturaAfectada = mes.FechaDeGeneracion"
   SQL = SQL & " AND (SUBSTRING(factura.observaciones, 1, 8) = 'CONTRATO' ))"
   SQL = SQL & " AND factura.nocontrato <> " & gUtilSQL.fSimpleSqlValue("")
   SQL = SQL & " AND factura.ConsecutivoCompania = " & valConsecutivoCompania

h_EXIT: On Error GoTo 0
   BorrarRegistrosMesGenerado = SQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "BorrarRegistrosMesGenerado", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
