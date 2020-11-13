VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsSolicitudesDePagoSQL"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit
Private Const CM_FILE_NAME As String = "clsSolicitudesDePagoSQL"
Private Const CM_MESSAGE_NAME As String = "SQL SolicitudesDePago"

Private Function GetGender() As Enum_Gender
   GetGender = eg_Female
End Function



Public Function fSqlSolicitudesDePagoEntreFechaReporte(ByVal valConsecutivoCompania As Long, ByVal valFechaDesde As Date, ByVal valFechaHasta As Date, ByVal valFiltro As String) As String
   Dim vSQL As String
   Dim vSQLWhere As String
   Dim insBancoVista As clsBancoVista
   Dim insSolicitudesDePagoVista As clsSolicitudesDePagoVista
   On Error GoTo h_ERROR
   Set insSolicitudesDePagoVista = New clsSolicitudesDePagoVista
   Set insBancoVista = New clsBancoVista


   vSQLWhere = gUtilSQL.fSQLValueDatesBetween(valFiltro, insSolicitudesDePagoVista.GetViewNameBeneficiarioSolicitudesDePago_B1() & ".FechaSolicitud", valFechaDesde, valFechaHasta)
   vSQLWhere = gUtilSQL.fSQLNumberValueWithAnd(vSQLWhere, insSolicitudesDePagoVista.GetViewNameBeneficiarioSolicitudesDePago_B1() & ".ConsecutivoCompania", valConsecutivoCompania)
   vSQLWhere = gUtilSQL.getWhereSQL(vSQLWhere)
   vSQL = vSQL & " SELECT "
   vSQL = vSQL & insSolicitudesDePagoVista.GetViewNameBeneficiarioSolicitudesDePago_B1() & ".ConsecutivoSolicitud,"
   vSQL = vSQL & insSolicitudesDePagoVista.GetViewNameBeneficiarioSolicitudesDePago_B1() & ".FechaSolicitud,"
   vSQL = vSQL & insSolicitudesDePagoVista.GetViewNameBeneficiarioSolicitudesDePago_B1() & ".NumeroDocumentoOrigen,"
   vSQL = vSQL & insSolicitudesDePagoVista.GetViewNameBeneficiarioSolicitudesDePago_B1() & ".StatusSolicitudStr,"
   vSQL = vSQL & " SUM ( " & insSolicitudesDePagoVista.GetViewNameBeneficiarioSolicitudesDePago_B1() & ".Monto) AS Monto "
   vSQL = vSQL & " FROM  " & insSolicitudesDePagoVista.GetViewNameBeneficiarioSolicitudesDePago_B1() & " "
   vSQL = vSQL & vSQLWhere
   vSQL = vSQL & " Group by "
   vSQL = vSQL & insSolicitudesDePagoVista.GetViewNameBeneficiarioSolicitudesDePago_B1() & ".ConsecutivoSolicitud,"
   vSQL = vSQL & insSolicitudesDePagoVista.GetViewNameBeneficiarioSolicitudesDePago_B1() & ".FechaSolicitud,"
   vSQL = vSQL & insSolicitudesDePagoVista.GetViewNameBeneficiarioSolicitudesDePago_B1() & ".NumeroDocumentoOrigen,"
   vSQL = vSQL & insSolicitudesDePagoVista.GetViewNameBeneficiarioSolicitudesDePago_B1() & ".StatusSolicitudStr,"
   vSQL = vSQL & insSolicitudesDePagoVista.GetViewNameBeneficiarioSolicitudesDePago_B1() & ".GeneradoPorStr"
 
   fSqlSolicitudesDePagoEntreFechaReporte = vSQL
   Set insSolicitudesDePagoVista = Nothing
   Set insBancoVista = Nothing
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSqlSolicitudesDePagoEntreFechaReporte", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
 End Function


Public Function fSqlSolicitudesDePagoEntreFechaReportePivote(ByVal valConsecutivoCompania As Long, ByVal valFechaDesde As Date, ByVal valFechaHasta As Date, ByVal valFiltro As String) As String
   Dim vSQL As String
   Dim vSQLWhere As String
   Dim insBancoVista As clsBancoVista
   Dim insSolicitudesDePagoVista As clsSolicitudesDePagoVista
   On Error GoTo h_ERROR
   Set insSolicitudesDePagoVista = New clsSolicitudesDePagoVista
   Set insBancoVista = New clsBancoVista


   vSQLWhere = gUtilSQL.fSQLValueDatesBetween(valFiltro, "FechaSolicitud", valFechaDesde, valFechaHasta)
   vSQLWhere = gUtilSQL.fSQLNumberValueWithAnd(vSQLWhere, "ConsecutivoCompania", valConsecutivoCompania)
   vSQLWhere = gUtilSQL.getWhereSQL(vSQLWhere)
   
   vSQL = vSQL & " SELECT "
   vSQL = vSQL & "ConsecutivoCompania,ConsecutivoSolicitud,FechaSolicitud,NumeroDocumentoOrigen, StatusSolicitudStr ,[0] Efectivo,[1] Transferencia, [2] Cheque "
   vSQL = vSQL & " FROM ( "
   vSQL = vSQL & " SELECT "
   vSQL = vSQL & insSolicitudesDePagoVista.GetViewNameBeneficiarioSolicitudesDePago_B1() & ".ConsecutivoCompania,"
   vSQL = vSQL & insSolicitudesDePagoVista.GetViewNameBeneficiarioSolicitudesDePago_B1() & ".ConsecutivoSolicitud,"
   vSQL = vSQL & insSolicitudesDePagoVista.GetViewNameBeneficiarioSolicitudesDePago_B1() & ".FechaSolicitud,"
   vSQL = vSQL & insSolicitudesDePagoVista.GetViewNameBeneficiarioSolicitudesDePago_B1() & ".NumeroDocumentoOrigen,"
   vSQL = vSQL & insSolicitudesDePagoVista.GetViewNameBeneficiarioSolicitudesDePago_B1() & ".FormaDePago   AS FormaDePago, StatusSolicitudStr,"
   
   vSQL = vSQL & insSolicitudesDePagoVista.GetViewNameBeneficiarioSolicitudesDePago_B1() & ".Monto "
   vSQL = vSQL & " FROM  " & insSolicitudesDePagoVista.GetViewNameBeneficiarioSolicitudesDePago_B1()
   vSQL = vSQL & " ) "
   vSQL = vSQL & " V PIVOT ( sum (Monto)  FOR FormaDePago IN ([0], [1], [2]) ) as PT "
   vSQL = vSQL & vSQLWhere
   
   fSqlSolicitudesDePagoEntreFechaReportePivote = vSQL
   Set insSolicitudesDePagoVista = Nothing
   Set insBancoVista = Nothing
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSqlSolicitudesDePagoEntreFechaReportePivote", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
 End Function


 

Public Function fSqlSolicitudDePago(ByVal valConsecutivoCompania As Long, ByVal valFiltro As String) As String
   Dim vSQL As String
   Dim vSQLWhere As String
   Dim insBancoVista As clsBancoVista
   Dim insSolicitudesDePagoVista As clsSolicitudesDePagoVista
   On Error GoTo h_ERROR
   Set insSolicitudesDePagoVista = New clsSolicitudesDePagoVista
   Set insBancoVista = New clsBancoVista

   vSQLWhere = gUtilSQL.fSQLNumberValueWithAnd(valFiltro, insSolicitudesDePagoVista.GetViewNameBeneficiarioSolicitudesDePago_B1() & ".ConsecutivoCompania", valConsecutivoCompania)
   vSQLWhere = gUtilSQL.getWhereSQL(vSQLWhere)
   vSQL = vSQL & " SELECT "
   vSQL = vSQL & insSolicitudesDePagoVista.GetViewNameBeneficiarioSolicitudesDePago_B1() & ".ConsecutivoSolicitud,"
   vSQL = vSQL & insSolicitudesDePagoVista.GetViewNameBeneficiarioSolicitudesDePago_B1() & ".FechaSolicitud,"
   vSQL = vSQL & insSolicitudesDePagoVista.GetViewNameBeneficiarioSolicitudesDePago_B1() & ".NumeroDocumentoOrigen,"
   vSQL = vSQL & insSolicitudesDePagoVista.GetViewNameBeneficiarioSolicitudesDePago_B1() & ".StatusSolicitudStr,"
   vSQL = vSQL & insSolicitudesDePagoVista.GetViewNameBeneficiarioSolicitudesDePago_B1() & ".GeneradoPorStr,"
   vSQL = vSQL & insSolicitudesDePagoVista.GetViewNameBeneficiarioSolicitudesDePago_B1() & ".RIF,"
   vSQL = vSQL & insSolicitudesDePagoVista.GetViewNameBeneficiarioSolicitudesDePago_B1() & ".NombreBeneficiario,"
   vSQL = vSQL & insSolicitudesDePagoVista.GetViewNameBeneficiarioSolicitudesDePago_B1() & ".FormaDePagoStr,"
   vSQL = vSQL & insSolicitudesDePagoVista.GetViewNameBeneficiarioSolicitudesDePago_B1() & ".StatusStr,"
   vSQL = vSQL & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull(insSolicitudesDePagoVista.GetViewNameBeneficiarioSolicitudesDePago_B1() & ".NumeroDocumento"), gUtilSQL.fSimpleSqlValue(""), insSolicitudesDePagoVista.GetViewNameBeneficiarioSolicitudesDePago_B1() & ".NumeroDocumento") & " AS NumeroDocumentoMovimiento,"
   vSQL = vSQL & insSolicitudesDePagoVista.GetViewNameBeneficiarioSolicitudesDePago_B1() & ".CuentaBancaria,"
   vSQL = vSQL & insSolicitudesDePagoVista.GetViewNameBeneficiarioSolicitudesDePago_B1() & ".Monto,"
   vSQL = vSQL & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull("dbo.COMPROBANTE.Numero"), gUtilSQL.fSimpleSqlValue(""), "dbo.COMPROBANTE.Numero") & " AS NumeroComprobante"
   vSQL = vSQL & " FROM  " & insSolicitudesDePagoVista.GetViewNameBeneficiarioSolicitudesDePago_B1()
   vSQL = vSQL & " LEFT JOIN  " & insBancoVista.GetViewNameMovimientosBancarios_B1() & " ON "
   vSQL = vSQL & insSolicitudesDePagoVista.GetViewNameBeneficiarioSolicitudesDePago_B1() & ".ConsecutivoCompania = dbo.IGV_MovimientosBancarios_B1.ConsecutivoCompania And "
   vSQL = vSQL & insSolicitudesDePagoVista.GetViewNameBeneficiarioSolicitudesDePago_B1() & ".NumeroAsociado = dbo.IGV_MovimientosBancarios_B1.NroMovimientoRelacionado And "
   vSQL = vSQL & insSolicitudesDePagoVista.GetViewNameBeneficiarioSolicitudesDePago_B1() & ".Monto = " & insBancoVista.GetViewNameMovimientosBancarios_B1() & ".Monto"
   vSQL = vSQL & " LEFT JOIN     dbo.COMPROBANTE ON "
   vSQL = vSQL & insBancoVista.GetViewNameMovimientosBancarios_B1() & ".ConsecutivoMovimiento = dbo.COMPROBANTE.ConsecutivoDocOrigen "
   vSQL = vSQL & " AND  dbo.COMPROBANTE.GeneradoPor = " & gUtilSQL.fSimpleSqlValue("G") & " "
   vSQL = vSQL & vSQLWhere
   
  fSqlSolicitudDePago = vSQL
   Set insSolicitudesDePagoVista = Nothing
   Set insBancoVista = Nothing
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSqlSolicitudDePago", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
 End Function


