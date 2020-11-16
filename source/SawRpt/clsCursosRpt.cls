VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsCursosRpt"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit
Private rCCConjuntoDeClientes As String
Private rCCNumeroDeCurso As Long
Private Const CM_FILE_NAME As String = "clsRptCursosConfigurar"
Private Const CM_MESSAGE_NAME As String = "Configuracion de Cursos"

Private Enum eCursosRpt
   eCurRpt_LISTA_DE_PARTICIPANTES = 0
   eCurRpt_CURSOS_POR_PRODUCTO
   eCurRpt_CURSOS_POR_INSTRUCTOR
   eCurRpt_CURSOS_ENTRE_FECHAS
   eCurRpt_ESTADISTICO_DE_CURSOS
   eCurRpt_CURSOS_POR_MESES_DE_UN_ANO
   eCurRpt_CARTA_DE_CONFIRMACION
   eCurRpt_CURSO_DE_ADIESTRAMIENTO
   eCurRpt_CERTIFICADO_DE_ASISTENCIA
   eCurRpt_CARTA_RESERVACION_EMAIL
End Enum

Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function

Private Function fNombreReporteParaMensajes(ByVal valIndexDelReporte As eCursosRpt) As String
   Dim varNombreReporte As String
   On Error GoTo h_ERROR
   Select Case valIndexDelReporte
      Case eCurRpt_LISTA_DE_PARTICIPANTES: varNombreReporte = "Lista de Participantes"
      Case eCurRpt_CURSOS_POR_PRODUCTO: varNombreReporte = "Cursos por Producto"
      Case eCurRpt_CURSOS_POR_INSTRUCTOR: varNombreReporte = "Cursos por Instructor"
      Case eCurRpt_CURSOS_ENTRE_FECHAS: varNombreReporte = "Cursos entre Fechas"
      Case eCurRpt_ESTADISTICO_DE_CURSOS: varNombreReporte = "Estadístico de Cursos"
      Case eCurRpt_CURSOS_POR_MESES_DE_UN_ANO: varNombreReporte = "Cursos por Meses de un Año"
      Case eCurRpt_CARTA_DE_CONFIRMACION: varNombreReporte = "Carta de Confirmación"
      Case eCurRpt_CURSO_DE_ADIESTRAMIENTO: varNombreReporte = "Curso de Adistramiento"
      Case eCurRpt_CERTIFICADO_DE_ASISTENCIA: varNombreReporte = "Certificado de Asistencia"
      Case eCurRpt_CARTA_RESERVACION_EMAIL: varNombreReporte = "Email de Reservación de Curso"
      Case Else: varNombreReporte = ""
   End Select
h_EXIT: On Error GoTo 0
   fNombreReporteParaMensajes = varNombreReporte
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fNombreReporteParaMensajes", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fNombreFileDeImpresionDelReporte(ByVal valIndexDelReporte As eCursosRpt) As String
   Dim varNombreFileXML As String
   On Error GoTo h_ERROR
   Select Case valIndexDelReporte
      Case eCurRpt_LISTA_DE_PARTICIPANTES: varNombreFileXML = "rpxListaDeParticipantes"
      Case eCurRpt_CURSOS_POR_PRODUCTO: varNombreFileXML = "rpxCursosPorProducto"
      Case eCurRpt_CURSOS_POR_INSTRUCTOR: varNombreFileXML = "rpxCursosPorInstructor"
      Case eCurRpt_CURSOS_ENTRE_FECHAS: varNombreFileXML = "rpxCursosEntreFechas"
      Case eCurRpt_ESTADISTICO_DE_CURSOS: varNombreFileXML = "rpxEstadisticoDeCursos"
      Case eCurRpt_CURSOS_POR_MESES_DE_UN_ANO: varNombreFileXML = "rpxCursosPorMesesDeUnAno"
      Case eCurRpt_CARTA_DE_CONFIRMACION: varNombreFileXML = "rpxCartaDeConfirmacion"
      Case eCurRpt_CURSO_DE_ADIESTRAMIENTO: varNombreFileXML = "rpxCursoDeAdiestramiento"
      Case eCurRpt_CERTIFICADO_DE_ASISTENCIA: varNombreFileXML = "rpxCertificadoAsisteciaCurso"
      Case eCurRpt_CARTA_RESERVACION_EMAIL: varNombreFileXML = "rpxCartaDeReservacionEmail"
      Case Else: varNombreFileXML = ""
   End Select
   If varNombreFileXML <> "" Then
      fNombreFileDeImpresionDelReporte = gUtilReports.getCompletePathDelFileDeImpresionRpx(varNombreFileXML, True, fNombreReporteParaMensajes(valIndexDelReporte))
   Else
      fNombreFileDeImpresionDelReporte = ""
   End If
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fNombreFileDeImpresionDelReporte", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarListaDeParticipantes(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSQL As String, ByVal valNombreCompaniaParaInformes As String) As Boolean
   Dim varFileName As String
   Dim exito As Boolean
   On Error GoTo h_ERROR
   exito = False
   varFileName = fNombreFileDeImpresionDelReporte(eCurRpt_LISTA_DE_PARTICIPANTES)
   If gUtilReports.fLoadLayout(refRpt, varFileName) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "DcOrigenData", valSQL) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFecha", "", "Fecha", eFT_DATE
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtParticipante", "", "NombreParticipante"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCurso", "", "Producto"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCliente", "", "Cliente"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtStatus", "", "StatusParticipante"
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", _
                                    "lblNumeroDePagina", "", _
                                    gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         'refRpt.AddNamedItem "gEnumProyecto", gEnumProyecto
         'refRpt.AddNamedItem "gConvert", gConvert
         gUtilMargins.sAsignarMargenesGenerales refRpt
         exito = True
      End If
   End If
h_EXIT: On Error GoTo 0
   fConfigurarListaDeParticipantes = exito
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfigurarListaDeParticipantes", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarCursosPorProducto(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSQL As String, ByVal valNombreDelProducto As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valNombreCompaniaParaInformes As String) As Boolean
   Dim varContinuar As Boolean
   Dim varFileName As String
   On Error GoTo h_ERROR
   varContinuar = False
   fConfigurarCursosPorProducto = False
   varFileName = fNombreFileDeImpresionDelReporte(eCurRpt_CURSOS_POR_PRODUCTO)
   If gUtilReports.fLoadLayout(refRpt, varFileName) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "DcOrigenData", valSQL) Then
         varContinuar = True
      End If
   End If
   If varContinuar Then
      gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
      gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
      gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", "Del " & gConvert.fechaAString(valFechaInicial) & " al " & gConvert.fechaAString(valFechaFinal)
      gUtilReports.sDefaultConfigurationForLabels refRpt, "lblTituloDelReporte", "Cursos del Producto " & valNombreDelProducto
      gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtDiaDeLaSemana", "", "Fecha", eFT_DATE
      gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtProducto", "", "Producto"
      gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtInstructor", "", "Nombre"
      gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtTurno", "", "Turno"
      gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtStatus", "", "StatusCurso"
      gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtSala", "", "Sala"
      gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", _
                                 "lblNumeroDePagina", "lblFechaInicialYFinal", _
                                 gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
      'refRpt.AddNamedItem "gEnumProyecto", gEnumProyecto
      refRpt.AddNamedItem "gConvert", gConvert
      'refRpt.AddNamedItem "gUtilDate", gUtilDate
      gUtilMargins.sAsignarMargenesGenerales refRpt
   End If
h_EXIT: On Error GoTo 0
   fConfigurarCursosPorProducto = varContinuar
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfigurarCursosPorProducto", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarCursosPorInstructor(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSQL As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valNombreCompaniaParaInformes As String) As Boolean
   Dim varContinuar As Boolean
   Dim varFileName As String
   On Error GoTo h_ERROR
   varContinuar = False
   fConfigurarCursosPorInstructor = False
   varFileName = fNombreFileDeImpresionDelReporte(eCurRpt_CURSOS_POR_INSTRUCTOR)
   If gUtilReports.fLoadLayout(refRpt, varFileName) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "DcOrigenData", valSQL) Then
         varContinuar = True
      End If
   End If
   If varContinuar Then
      gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
      gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
      gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaInicialYFinal"
      gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtDiaDeLaSemana", "", "Fecha", eFT_DATE
      gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtProducto", "", "Producto"
      gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtInstructor", "", "Nombre"
      gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtHorario", "", "Horario"
      gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtSala", "", "Sala"
      gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHInstructor", "Nombre", _
               ddGrpNone, ddRepeatOnPage, True, ddNPNone
      gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", _
                                 "lblNumeroDePagina", "lblFechaInicialYFinal", _
                                 gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
'      refRpt.AddNamedItem "gEnumProyecto", gEnumProyecto
'      refRpt.AddNamedItem "gConvert", gConvert
'      refRpt.AddNamedItem "gUtilDate", gUtilDate
      gUtilMargins.sAsignarMargenesGenerales refRpt
   End If
h_EXIT: On Error GoTo 0
   fConfigurarCursosPorInstructor = varContinuar
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfigurarCursosPorInstructor", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarCursosEntreFechas(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSQL As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valNombreCompaniaParaInformes As String) As Boolean
   Dim varContinuar As Boolean
   Dim varFileName As String
   On Error GoTo h_ERROR
   varContinuar = False
   fConfigurarCursosEntreFechas = False
   varFileName = fNombreFileDeImpresionDelReporte(eCurRpt_CURSOS_ENTRE_FECHAS)
   If gUtilReports.fLoadLayout(refRpt, varFileName) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "DcOrigenData", valSQL) Then
         varContinuar = True
      End If
   End If
   If varContinuar Then
      gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
      gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
      gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", "Del " & gConvert.fechaAString(valFechaInicial) & " al " & gConvert.fechaAString(valFechaFinal)
      gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtDiaDeLaSemana", "", "Fecha", eFT_DATE
      gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtProducto", "", "Producto"
      gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtInstructor", "", "Nombre"
      gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtTurno", "", "Turno"
      gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtSala", "", "Sala"
      gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtStatus", "", "StatusCurso"
      gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHProducto", "Producto", _
               ddGrpNone, ddRepeatOnPage, True, ddNPNone
      gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", _
                                 "lblNumeroDePagina", "lblFechaInicialYFinal", _
                                 gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
'      refRpt.AddNamedItem "gEnumProyecto", gEnumProyecto
      refRpt.AddNamedItem "gConvert", gConvert
'      refRpt.AddNamedItem "gUtilDate", gUtilDate
      gUtilMargins.sAsignarMargenesGenerales refRpt
   End If
h_EXIT: On Error GoTo 0
   fConfigurarCursosEntreFechas = varContinuar
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfigurarCursosEntreFechas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarEstadisticoDeCursos(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSQL As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valNombreCompaniaParaInformes As String) As Boolean
   Dim varContinuar As Boolean
   Dim varFileName As String
   Dim varRS As ADODB.Recordset
   On Error GoTo h_ERROR
   varContinuar = False
   fConfigurarEstadisticoDeCursos = False
   varFileName = fNombreFileDeImpresionDelReporte(eCurRpt_ESTADISTICO_DE_CURSOS)
   If gUtilReports.fLoadLayout(refRpt, varFileName) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "DcOrigenData", valSQL) Then
         varContinuar = True
      End If
   End If
   If varContinuar Then
      gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
      gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
      gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", "Del " & gConvert.fechaAString(valFechaInicial) & " al " & gConvert.fechaAString(valFechaFinal)
      gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFecha", "", "Fecha", eFT_DATE
      gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCurso", "", "Producto"
      gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtSala", "", "Sala"
      gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtInstructor", "", "Nombre"
      gUtilReports.sDefaultConfigurationForIntegerFields refRpt, "txtNoParticipantes", "", "CountParticipantes"
      gUtilReports.sDefaultConfigurationForIntegerFields refRpt, "txtExonerados", "", "CountExonerados"
      gUtilReports.sDefaultConfigurationForIntegerFields refRpt, "txtPagos", "", "CountPagos"
      gUtilReports.sDefaultConfigurationForIntegerFields refRpt, "txtAsistieron", "", "CountAsistieron"
      gUtilReports.sDefaultConfigurationForIntegerFields refRpt, "txtTotalNoParticipantes", "", "CountParticipantes"
      gUtilReports.sDefaultConfigurationForIntegerFields refRpt, "txtTotalExonerados", "", "CountExonerados"
      gUtilReports.sDefaultConfigurationForIntegerFields refRpt, "txtTotalPagos", "", "CountPagos"
      gUtilReports.sDefaultConfigurationForIntegerFields refRpt, "txtTotalAsistieron", "", "CountAsistieron"
      gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", _
                                 "lblNumeroDePagina", "lblFechaInicialYFinal", _
                                 gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
      gUtilMargins.sAsignarMargenesGenerales refRpt
   End If
h_EXIT: On Error GoTo 0
   fConfigurarEstadisticoDeCursos = varContinuar
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfigurarEstadisticoDeCursos", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarCursosPorMesesDeUnAno(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSQL As String, ByVal valAnoAplicacion As Integer, ByVal valNombreCompaniaParaInformes As String) As Boolean
   Dim varContinuar As Boolean
   Dim varFileName As String
   On Error GoTo h_ERROR
   varContinuar = False
   fConfigurarCursosPorMesesDeUnAno = False
   varFileName = fNombreFileDeImpresionDelReporte(eCurRpt_CURSOS_POR_MESES_DE_UN_ANO)
   If gUtilReports.fLoadLayout(refRpt, varFileName) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "DcOrigenData", valSQL) Then
         varContinuar = True
      End If
   End If
   If varContinuar Then
      gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
      gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
      gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", "Año: " & gConvert.fConvierteAString(valAnoAplicacion)
      gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDetailTipoDeProducto", "", "Producto"
      gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreDelVendedor", "", "NombreVendedor"
      gUtilReports.sDefaultConfigurationForIntegerFields refRpt, "txtDetailCantidad", "", "NumeroDeCursos"
      gUtilReports.sDefaultConfigurationForIntegerFields refRpt, "txtDetailMes1", "", "MES1"
      gUtilReports.sDefaultConfigurationForIntegerFields refRpt, "txtDetailMes2", "", "MES2"
      gUtilReports.sDefaultConfigurationForIntegerFields refRpt, "txtDetailMes3", "", "MES3"
      gUtilReports.sDefaultConfigurationForIntegerFields refRpt, "txtDetailMes4", "", "MES4"
      gUtilReports.sDefaultConfigurationForIntegerFields refRpt, "txtDetailMes5", "", "MES5"
      gUtilReports.sDefaultConfigurationForIntegerFields refRpt, "txtDetailMes6", "", "MES6"
      gUtilReports.sDefaultConfigurationForIntegerFields refRpt, "txtDetailMes7", "", "MES7"
      gUtilReports.sDefaultConfigurationForIntegerFields refRpt, "txtDetailMes8", "", "MES8"
      gUtilReports.sDefaultConfigurationForIntegerFields refRpt, "txtDetailMes9", "", "MES9"
      gUtilReports.sDefaultConfigurationForIntegerFields refRpt, "txtDetailMes10", "", "MES10"
      gUtilReports.sDefaultConfigurationForIntegerFields refRpt, "txtDetailMes11", "", "MES11"
      gUtilReports.sDefaultConfigurationForIntegerFields refRpt, "txtDetailMes12", "", "MES12"
      gUtilReports.sDefaultConfigurationForIntegerFields refRpt, "txtCantidad", "", "NumeroDeCursos"
      gUtilReports.sDefaultConfigurationForIntegerFields refRpt, "txtMes1", "", "MES1"
      gUtilReports.sDefaultConfigurationForIntegerFields refRpt, "txtMes2", "", "MES2"
      gUtilReports.sDefaultConfigurationForIntegerFields refRpt, "txtMes3", "", "MES3"
      gUtilReports.sDefaultConfigurationForIntegerFields refRpt, "txtMes4", "", "MES4"
      gUtilReports.sDefaultConfigurationForIntegerFields refRpt, "txtMes5", "", "MES5"
      gUtilReports.sDefaultConfigurationForIntegerFields refRpt, "txtMes6", "", "MES6"
      gUtilReports.sDefaultConfigurationForIntegerFields refRpt, "txtMes7", "", "MES7"
      gUtilReports.sDefaultConfigurationForIntegerFields refRpt, "txtMes8", "", "MES8"
      gUtilReports.sDefaultConfigurationForIntegerFields refRpt, "txtMes9", "", "MES9"
      gUtilReports.sDefaultConfigurationForIntegerFields refRpt, "txtMes10", "", "MES10"
      gUtilReports.sDefaultConfigurationForIntegerFields refRpt, "txtMes11", "", "MES11"
      gUtilReports.sDefaultConfigurationForIntegerFields refRpt, "txtMes12", "", "MES12"
      gUtilReports.sDefaultConfigurationForIntegerFields refRpt, "txtTotalCantidad", "", "NumeroDeCursos"
      gUtilReports.sDefaultConfigurationForIntegerFields refRpt, "txtTotalMes1", "", "MES1"
      gUtilReports.sDefaultConfigurationForIntegerFields refRpt, "txtTotalMes2", "", "MES2"
      gUtilReports.sDefaultConfigurationForIntegerFields refRpt, "txtTotalMes3", "", "MES3"
      gUtilReports.sDefaultConfigurationForIntegerFields refRpt, "txtTotalMes4", "", "MES4"
      gUtilReports.sDefaultConfigurationForIntegerFields refRpt, "txtTotalMes5", "", "MES5"
      gUtilReports.sDefaultConfigurationForIntegerFields refRpt, "txtTotalMes6", "", "MES6"
      gUtilReports.sDefaultConfigurationForIntegerFields refRpt, "txtTotalMes7", "", "MES7"
      gUtilReports.sDefaultConfigurationForIntegerFields refRpt, "txtTotalMes8", "", "MES8"
      gUtilReports.sDefaultConfigurationForIntegerFields refRpt, "txtTotalMes9", "", "MES9"
      gUtilReports.sDefaultConfigurationForIntegerFields refRpt, "txtTotalMes10", "", "MES10"
      gUtilReports.sDefaultConfigurationForIntegerFields refRpt, "txtTotalMes11", "", "MES11"
      gUtilReports.sDefaultConfigurationForIntegerFields refRpt, "txtTotalMes12", "", "MES12"
      gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHNombreVendedor", "NombreVendedor", _
               ddGrpNone, ddRepeatNone, False, ddNPNone
      gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHProducto", "Producto", _
               ddGrpNone, ddRepeatNone, False, ddNPNone
      gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", _
                                 "lblNumeroDePagina", "lblFechaInicialYFinal", _
                                 gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
      gUtilMargins.sAsignarMargenesGenerales refRpt
   End If
h_EXIT: On Error GoTo 0
   fConfigurarCursosPorMesesDeUnAno = varContinuar
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfigurarCursosPorMesesDeUnAno", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarCartaDeConfirmacion(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSQL As String, ByVal valNombreDelProducto As String, ByVal valEnvia As String, _
                     ByVal valDiaDeLaSemana As String, ByVal valFecha As Date, ByVal valHorario As String, ByVal valConjuntoDeClientes As String, ByVal valNumeroDeCurso As Long, ByVal valAsunto As String, ByVal valConfirmacion As Boolean, Optional ByVal valCostoDelCurso As Currency) As Boolean
   Dim varContinuar As Boolean
   Dim varFileName As String
   Dim varParrafoUno As String
   Dim varParrafoCinco As String
   On Error GoTo h_ERROR
   varContinuar = False
   fConfigurarCartaDeConfirmacion = False

   If valConfirmacion Then
      varFileName = fNombreFileDeImpresionDelReporte(eCurRpt_CARTA_DE_CONFIRMACION)
      varParrafoUno = "Nos dirigimos a usted en la oportunidad de recordarle su participación en el curso de adiestramiento" & UCase(valNombreDelProducto) & ", " & _
                "el cual está pautado para el día: " & UCase(valDiaDeLaSemana) & " " & valFecha & ", en horario comprendido: " & valHorario & _
                ", para los siguientes participantes : "
   Else
      varFileName = fNombreFileDeImpresionDelReporte(eCurRpt_CARTA_RESERVACION_EMAIL)
      varParrafoUno = "La siguiente comunicación tiene por objetivo hacer de su conocimiento que de acuerdo a la solicitud efectuada por usted(es), " & _
               "ha sido reservado un cupo para el curso de adiestramiento del  software " & UCase(valNombreDelProducto) & ", el cual está pautado para el día " & UCase(valDiaDeLaSemana) & " " & valFecha & _
               ", en horario comprendido de: " & valHorario
   End If
   If gUtilReports.fLoadLayout(refRpt, varFileName) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "DcOrigenData", valSQL) Then
         varContinuar = True
      End If
   End If
   If varContinuar Then
'      varParrafoUno = "La  siguiente  comunicación  tiene el fin de confirmar la  asistencia  del  personal  de  su empresa para el curso de adiestramiento en  el programa" & _
'                      " " & UCase(valNombreDelProducto) & ",  pautado para el día " & UCase(valDiaDeLaSemana) & _
'                      " " & gConvert.fechaAString(valFecha) & " en el horario " & valHorario & _
'                      " de los siguientes participantes : "
      varParrafoCinco = "Para  mayor  Información  comunicarse  con  " & valEnvia & " en el Dpto. de Adiestramiento"
      rCCConjuntoDeClientes = valConjuntoDeClientes
      rCCNumeroDeCurso = valNumeroDeCurso
'      gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtEnvia", valEnvia, ""
      gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtParrafo1", varParrafoUno, ""
      gUtilReports.sDefaultConfigurationForLabels refRpt, "lblParrafo5", varParrafoCinco
      gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtSenores", "", "Cliente"
      gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtAtencion", "", "Contacto"
      gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtAsunto", valAsunto
      gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtProducto", valNombreDelProducto
      gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtFecha", valFecha
      gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtHorario", valHorario
      gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtFechaDoc", gConvert.dateToString(gUtilDate.getFechaDeHoy)
            If valConfirmacion Then
         gUtilReports.sWriteInfoInTagPropertyOfControl refRpt, "txtCosto", gConvert.fNumToStringConSeparadorDeMiles(valCostoDelCurso, False, 2)
   '      gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtSres", "", "Cliente"
   '      gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtFAX", "", "Fax"
   '      gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtAtencion", "", "Contacto"
   '      gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtTelf", "", "Telefono"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreParticipante", "", "NombreParticipante"
               gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHCliente", "Cliente", _
               ddGrpFirstDetail, ddRepeatOnPage, True, ddNPAfter
      End If
      refRpt.AddNamedItem "insRptConfigurar", Me
      gUtilMargins.sAsignarMargenesGenerales refRpt
   End If
h_EXIT: On Error GoTo 0
   fConfigurarCartaDeConfirmacion = varContinuar
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfigurarCartaDeConfirmacion", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLMarcaCartaImpresaALosParticipantesDelCurso() As String
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = ""
   SQL = "UPDATE Participante" _
         & " SET " & "Carta" & " = 'S' " _
         & " WHERE " & "NumeroCurso" & " = " & rCCNumeroDeCurso _
         & " AND " & "Cliente" & " IN(" & rCCConjuntoDeClientes & ")"
h_EXIT: On Error GoTo 0
   fSQLMarcaCartaImpresaALosParticipantesDelCurso = SQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLMarcaCartaImpresaALosParticipantesDelCurso", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarCursoDeAdiestramiento(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSQL As String, ByVal valFechaDelCurso As Date, ByVal valTurnoAsStr As String, ByVal valHorarioAsStr As String, ByVal valNombreDelProducto As String, ByVal valNombreCompletoInstructor As String, ByVal valNombreCompaniaParaInformes As String) As Boolean
   Dim varContinuar As Boolean
   Dim varFileName As String
   On Error GoTo h_ERROR
   varContinuar = False
   fConfigurarCursoDeAdiestramiento = False
   varFileName = fNombreFileDeImpresionDelReporte(eCurRpt_CURSO_DE_ADIESTRAMIENTO)
   If gUtilReports.fLoadLayout(refRpt, varFileName) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "DcOrigenData", valSQL) Then
         varContinuar = True
      End If
   End If
   If varContinuar Then
      gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
      gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
      gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDia", gConvert.dateToString(valFechaDelCurso)
      gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtHorario", valHorarioAsStr
      gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtProducto", UCase(valNombreDelProducto)
      gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtTurno", valTurnoAsStr
      gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtInstructor", valNombreCompletoInstructor
      gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtParticipante", "", "NombreParticipante"
      gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCliente", "", "Cliente"
      gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtStatus", "", "Status"
      gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCarta", "", "CartaEnviada"
      gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCosto", "", "ConCosto"
      gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", _
                                 "lblNumeroDePagina", "", _
                                 gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
'      refRpt.AddNamedItem "gEnumProyecto", gEnumProyecto
      gUtilMargins.sAsignarMargenesGenerales refRpt
   End If
h_EXIT: On Error GoTo 0
   fConfigurarCursoDeAdiestramiento = varContinuar
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfigurarCursoDeAdiestramiento", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarImpresionDeCertificado(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSQL As String, ByVal valProducto As String, ByVal valHorarioAsStr As String, ByVal valFecha As Date, ByVal valNombreDelInstructor As String) As Boolean
   Dim varFileName As String
   Dim valExito As Boolean
   On Error GoTo h_ERROR
   valExito = False
   varFileName = fNombreFileDeImpresionDelReporte(eCurRpt_CERTIFICADO_DE_ASISTENCIA)
   If gUtilReports.fLoadLayout(refRpt, varFileName) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "DcOrigenData", valSQL) Then
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreParticipante", "", "NombreParticipante"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCedulaParticipante", "", "CedulaParticipante"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtProducto", "", "Producto"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtHorario", valHorarioAsStr
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtInstructor", valNombreDelInstructor
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtFecha", gConvert.fConvierteFechaAStringFechaCompleta(valFecha)
        ' sColocarMarcadeAguaAlreporte refRpt, "hojagalac.jpg", True  'Nota: Si va a habilitar esta linea no olvide cargar la propiedad Watermark en el reporte.
'         refRpt.AddNamedItem "gEnumProyecto", gEnumProyecto
         gUtilMargins.sAsignarMargenesGenerales refRpt
         valExito = True
      End If
   End If
h_EXIT:
   fConfigurarImpresionDeCertificado = valExito
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfigurarImpresionDeCertificado", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function


