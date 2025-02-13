VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsUtilImprFiscal"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Attribute VB_Ext_KEY = "SavedWithClassBuilder6" ,"Yes"
Attribute VB_Ext_KEY = "Top_Level" ,"Yes"
Option Explicit
Private Declare Function Bematech_FI_RetornoImpresora Lib "BEMAFI32.DLL" (ByRef ACK As Integer, ByRef ST1 As Integer, ByRef ST2 As Integer) As Integer
Private Declare Function Bematech_FI_NumeroCupon Lib "BEMAFI32.DLL" (ByVal NumeroCupon As String) As Integer
Private Const CM_FILE_NAME As String = "clsUtilImprFiscal"
   
Private Const CM_MESSAGE_NAME As String = "Util Impresora Fiscal"
   
Private Const GetGender = Enum_Gender.eg_Male
   
Public Sub sAbreImpresionFiscal(ByRef refDatosImprFiscal As Object, ByRef Ready As Boolean)
   Dim insfrmImpresionFiscal As frmImpresorasFiscales
   On Error GoTo h_Error
   Set insfrmImpresionFiscal = New frmImpresorasFiscales
   refDatosImprFiscal.SetEstado Enum_EstadoImpresionFiscal.eEIF_VERIFICANDO_CONEXION_CON_IMPRESORA
   insfrmImpresionFiscal.sInitLookAndFeelAndSetValues refDatosImprFiscal, Ready
   Set insfrmImpresionFiscal = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_Error: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sAbreImpresionFiscal", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Sub sImprimeComprobanteFiscal(ByRef refDatosImprFiscal As Object, ByRef Ready As Boolean)
   Dim insfrmImpresionFiscal As frmImpresorasFiscales
   On Error GoTo h_Error
   Set insfrmImpresionFiscal = New frmImpresorasFiscales
   refDatosImprFiscal.SetEstado Enum_EstadoImpresionFiscal.eEIF_IMPRIMIR_VENTA
   insfrmImpresionFiscal.sInitLookAndFeelAndSetValues refDatosImprFiscal, Ready
   Set insfrmImpresionFiscal = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_Error: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sImprimeComprobanteFiscal", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Sub sEjecutaCierreZ(ByRef refDatosImprFiscal As Object, ByRef Ready As Boolean)
   Dim insfrmImpresionFiscal As frmImpresorasFiscales
   On Error GoTo h_Error
   Set insfrmImpresionFiscal = New frmImpresorasFiscales
   refDatosImprFiscal.SetEstado Enum_EstadoImpresionFiscal.eEIF_CIERRE_Z
   insfrmImpresionFiscal.sInitLookAndFeelAndSetValues refDatosImprFiscal, Ready
   Set insfrmImpresionFiscal = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_Error: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sEjecutaCierreZ", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Sub sEjecutaCierreX(ByRef refDatosImprFiscal As Object, ByRef Ready As Boolean)
   Dim insfrmImpresionFiscal  As frmImpresorasFiscales
   On Error GoTo h_Error
   Set insfrmImpresionFiscal = New frmImpresorasFiscales
   refDatosImprFiscal.SetEstado Enum_EstadoImpresionFiscal.eEIF_CIERRE_X
   insfrmImpresionFiscal.sInitLookAndFeelAndSetValues refDatosImprFiscal, Ready
   Set insfrmImpresionFiscal = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_Error: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sEjecutaCierreX", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Sub sObtenerSerial(ByRef refDatosImprFiscal As Object, ByRef Ready As Boolean)
   Dim insfrmImpresionFiscal As frmImpresorasFiscales
   On Error GoTo h_Error
   Set insfrmImpresionFiscal = New frmImpresorasFiscales
   refDatosImprFiscal.SetEstado Enum_EstadoImpresionFiscal.eEIF_OBTENER_SERIAL
   insfrmImpresionFiscal.sInitLookAndFeelAndSetValues refDatosImprFiscal, Ready
   Set insfrmImpresionFiscal = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_Error: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sObtenerSerial", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Sub sObtenerUltimoNumeroCupon(ByRef refDatosImprFiscal As Object, ByRef Ready As Boolean)
   Dim insfrmImpresionFiscal As frmImpresorasFiscales
   On Error GoTo h_Error
   Set insfrmImpresionFiscal = New frmImpresorasFiscales
   refDatosImprFiscal.SetEstado Enum_EstadoImpresionFiscal.eEIF_OBTENER_ULTIMO_NUMERO_FISCAL
   insfrmImpresionFiscal.sInitLookAndFeelAndSetValues refDatosImprFiscal, Ready
   Set insfrmImpresionFiscal = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_Error: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sObtenerUltimoNumeroCupon", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Public Sub sCancelaCuponFiscal(ByRef refDatosImprFiscal As Object, ByRef Ready As Boolean)
   Dim insfrmImpresionFiscal As frmImpresorasFiscales
   On Error GoTo h_Error
   Set insfrmImpresionFiscal = New frmImpresorasFiscales
   refDatosImprFiscal.SetEstado Enum_EstadoImpresionFiscal.eEIF_CANCELA_CUPON_FISCAL
   insfrmImpresionFiscal.sInitLookAndFeelAndSetValues refDatosImprFiscal, Ready
   Set insfrmImpresionFiscal = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_Error: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sCancelaCuponFiscal", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Public Function fVerificaRetornoImpresora(ByVal Label As String, ByVal Retorno As Integer, ByRef Ready As Boolean) As Boolean
   Dim ACK As Integer
   Dim ST1 As Integer
   Dim ST2 As Integer
   Dim ST3 As Integer
   Dim RetornaMensaje As Integer
   Dim StringRetorno As String
   Dim ValorRetorno As String
   Dim RetornoStatus As Integer
   Dim Mensaje As String
   Dim exito As Boolean
   Const TituloVentana = "Impresión Fiscal"
   On Error GoTo h_Error
   exito = False
   Ready = True
   If Retorno = 0 Then
     gMessage.sCriticalErrorMessage "Error de comunicación con la impresora.", "Impresora Fiscal"
      Ready = False
     Exit Function
   ElseIf Retorno = 1 Or Retorno = -27 Then
      RetornoStatus = Bematech_FI_RetornoImpresora(ACK, ST1, ST2)
      ValorRetorno = Str(ACK) & "," & Str(ST1) & "," & Str(ST2)
   End If
   If Label <> "" And Retorno <> 0 Then
      RetornaMensaje = 1
   End If
   If ACK = 21 Then
      gMessage.Advertencia "Status de la Impresora: 21" & vbCrLf & "Comando no ejecutado"
      Ready = False
      Exit Function
   End If
   If (ST1 <> 0 Or ST2 <> 0) Then
      If (ST1 >= 128) Then
          StringRetorno = "Fin de Papel" & vbCr
          ST1 = ST1 - 128
          Ready = True
      End If
      If (ST1 >= 64) Then
          StringRetorno = StringRetorno & "Poco Papel" & vbCr
          ST1 = ST1 - 64
          Ready = True
      End If
      
      If (ST1 >= 32) Then
          StringRetorno = StringRetorno & "Error en el reloj" & vbCr
          ST1 = ST1 - 32
          Ready = False
      End If
      
      If (ST1 >= 16) Then
          StringRetorno = StringRetorno & "Impresora en error" & vbCr
          ST1 = ST1 - 16
          Ready = False
      End If
          
      If (ST1 >= 8) Then
          StringRetorno = StringRetorno & "Primer dato del comando no fue Esc" & vbCr
          ST1 = ST1 - 8
          Ready = False
      End If
      
      If (ST1 >= 4) Then
          StringRetorno = StringRetorno & "Comando inexistente" & vbCr
          ST1 = ST1 - 4
          Ready = False
      End If
          
      If (ST1 >= 2) Then
          StringRetorno = StringRetorno & "Cupón fiscal abierto" & vbCr
          ST1 = ST1 - 2
          Ready = False
      End If
      
      If (ST1 >= 1) Then
          StringRetorno = StringRetorno & "Número de parámetros inválidos en el comando" & vbCr
          ST1 = ST1 - 1
          Ready = False
      End If
      If (ST2 >= 128) Then
          StringRetorno = "Tipo de Parámetro de comando inválido" & vbCr
          ST2 = ST2 - 128
          Ready = False
      End If
      If (ST2 >= 64) Then
          StringRetorno = StringRetorno & "Memória fiscal llena" & vbCr
          ST2 = ST2 - 64
          Ready = False
      End If
      If (ST2 >= 32) Then
          StringRetorno = StringRetorno & "Error en la CMOS" & vbCr
          ST2 = ST2 - 32
          Ready = False
      End If
      If (ST2 >= 16) Then
          StringRetorno = StringRetorno & "Alicuota no programada" & vbCr
          ST2 = ST2 - 16
          Ready = False
      End If
      If (ST2 >= 8) Then
          StringRetorno = StringRetorno & "Capacidad de alicuota programables llena" & vbCr
          ST2 = ST2 - 8
          Ready = False
      End If
      If (ST2 >= 4) Then
          StringRetorno = StringRetorno & "Cancelamiento no permitido" & vbCr
          ST2 = ST2 - 4
      End If
      If (ST2 >= 2) Then
          StringRetorno = StringRetorno & "RIF del propietario no programados" & vbCr
          ST2 = ST2 - 2
          Ready = False
      End If
      If (ST2 >= 1) Then
          StringRetorno = StringRetorno & "Comando no ejecutado" & vbCr
          ST2 = ST2 - 1
          Ready = False
      End If
      If RetornaMensaje Then
          Mensaje = "Status de la Impresora: " & ValorRetorno & _
                 vbCrLf & StringRetorno & vbCrLf & _
                 Label
      Else
          Mensaje = "Status de la Impresora: " & ValorRetorno & _
             vbCr & vbLf & StringRetorno
      End If
      gMessage.Advertencia Mensaje
      Exit Function
   End If 'fin del ST1 <> 0 y ST2 <> 0
   If RetornaMensaje Then
      Mensaje = Label '& Contenido
   End If
   If Mensaje <> "" Then
      gMessage.Advertencia Mensaje
      exito = True
      GoTo h_EXIT
   End If
   Select Case Retorno
      Case -1: gMessage.sCriticalErrorMessage "Error de ejecución de la función.", TituloVentana
      Case -2: gMessage.sCriticalErrorMessage "Parámetro inválido en la función.", TituloVentana
      Case -3: gMessage.sCriticalErrorMessage "Alicuota no programada.", TituloVentana
      Case -4: gMessage.sCriticalErrorMessage "El archivo de inicialización BemaFI32.ini no fue encontrado en el directorio default. " + vbCrLf + "Por favor, copie ese archivo para el directorio de sistema de Windows." + vbCrLf + "Si utilizas el Windows 95 o 98 es el directorio 'System' si utilizas el Windows NT,2000 o XP es el directorio 'System32'.", TituloVentana
      Case -5: gMessage.sCriticalErrorMessage "Error al abrir la puerta de comunicación.", TituloVentana
      Case -6: gMessage.sCriticalErrorMessage "Impresora colgada o cable de comunicación desconectado.", TituloVentana
      Case -7: gMessage.sCriticalErrorMessage "Banco no encontrado en el archivo BemaFI32.ini.", TituloVentana
      Case -8: gMessage.sCriticalErrorMessage "Error al crear o grabar en el archivo status.txt o retorno.txt.", TituloVentana
      Case -18: gMessage.sCriticalErrorMessage "No fue posible abrir el archivo INTPOS.001 !", TituloVentana
      Case -19: gMessage.sCriticalErrorMessage "Parámetro diferentes !", TituloVentana
      Case -20: gMessage.sCriticalErrorMessage "Transación anulada por el Operador !", TituloVentana
      Case -21: gMessage.sCriticalErrorMessage "La transación no fue aprobada !", TituloVentana
      Case -22: gMessage.sCriticalErrorMessage "No fue posible finalizar la impresión !", TituloVentana
      Case -23: gMessage.sCriticalErrorMessage "No fue posible finalizar la operación !", TituloVentana
      Case -24: gMessage.sCriticalErrorMessage "Forma de pago no programada.", TituloVentana
      Case -25: gMessage.sCriticalErrorMessage "Totalizador no fiscal no programado.", TituloVentana
      Case -26: gMessage.sCriticalErrorMessage "Transación ya realizada.", TituloVentana
      Case -28: gMessage.sCriticalErrorMessage "No hay datos para serem impresos.", TituloVentana
      Case Else: exito = True
   End Select
h_EXIT:  On Error GoTo 0
   fVerificaRetornoImpresora = exito
   Exit Function
h_Error: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fVerificaRetornoImpresora", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Sub sImprimeNotaDeCreditoComprobanteFiscal(ByRef refDatosImprFiscal As Object, ByRef Ready As Boolean)
   Dim insfrmImpresionFiscal As frmImpresorasFiscales
   On Error GoTo h_Error
   Set insfrmImpresionFiscal = New frmImpresorasFiscales
   refDatosImprFiscal.SetEstado Enum_EstadoImpresionFiscal.eEIF_IMPRIMIR_NOTA_DE_CREDITO
   insfrmImpresionFiscal.sInitLookAndFeelAndSetValues refDatosImprFiscal, Ready
   Set insfrmImpresionFiscal = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_Error: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sImprimeNotaDeCreditoComprobanteFiscal", CM_FILE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Sub sImprimeNotaDeDebitoComprobanteFiscal(ByRef refDatosImprFiscal As Object, ByRef Ready As Boolean)
   Dim insfrmImpresionFiscal As frmImpresorasFiscales
   On Error GoTo h_Error
   Set insfrmImpresionFiscal = New frmImpresorasFiscales
   refDatosImprFiscal.SetEstado Enum_EstadoImpresionFiscal.eEIF_IMPRIMIR_NOTA_DE_DEBITO
   insfrmImpresionFiscal.sInitLookAndFeelAndSetValues refDatosImprFiscal, Ready
   Set insfrmImpresionFiscal = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_Error: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sImprimeNotaDeDebitoComprobanteFiscal", CM_FILE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Sub sVerificaVigenciaDeAlicuota(ByRef refDatosImprFiscal As Object, ByRef Ready As Boolean, ByVal gAdmAlicuotaIvaActual As Object)
   Dim insfrmImpresionFiscal As frmImpresorasFiscales
   On Error GoTo h_Error
   Set insfrmImpresionFiscal = New frmImpresorasFiscales
   refDatosImprFiscal.SetEstado Enum_EstadoImpresionFiscal.eEIF_VERIFICA_ALICUOTA_VIGENTE
   insfrmImpresionFiscal.sInitLookAndFeelAndSetValues refDatosImprFiscal, Ready, gAdmAlicuotaIvaActual
   Set insfrmImpresionFiscal = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_Error: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sVerificaVigenciaDeAlicuota", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Sub sVerStatus(ByRef refDatosImprFiscal As Object, ByRef refReady As Boolean)
   Dim insfrmImpresionFiscal As frmImpresorasFiscales
   On Error GoTo h_Error
   Set insfrmImpresionFiscal = New frmImpresorasFiscales
   refDatosImprFiscal.SetEstado Enum_EstadoImpresionFiscal.eEIF_VER_STATUS
   insfrmImpresionFiscal.sInitLookAndFeelAndSetValues refDatosImprFiscal, refReady
   Set insfrmImpresionFiscal = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_Error: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sVerificaVigenciaDeAlicuota", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Sub sActivaRedondeo(ByRef refDatosImprFiscal As Object, ByRef refReady As Boolean)
   Dim insfrmImpresionFiscal As frmImpresorasFiscales
   On Error GoTo h_Error
   Set insfrmImpresionFiscal = New frmImpresorasFiscales
   refDatosImprFiscal.SetEstado Enum_EstadoImpresionFiscal.eEIF_ACTIVA_REDONDEO
   insfrmImpresionFiscal.sInitLookAndFeelAndSetValues refDatosImprFiscal, refReady
   Set insfrmImpresionFiscal = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_Error: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sActivaRedondeo", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Sub sObtenerUltimoReporteZ(ByRef refDatosImprFiscal As Object, ByRef Ready As Boolean)
   Dim insfrmImpresionFiscal As frmImpresorasFiscales
   On Error GoTo h_Error
   Set insfrmImpresionFiscal = New frmImpresorasFiscales
   refDatosImprFiscal.SetEstado Enum_EstadoImpresionFiscal.eEIF_OBTENER_ULTIMO_REPORTE_Z
   insfrmImpresionFiscal.sInitLookAndFeelAndSetValues refDatosImprFiscal, Ready
   Set insfrmImpresionFiscal = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_Error: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sObtenerUltimoReporteZ", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Sub sAsignarConfiguracionImpresora(ByRef refDatosImprFiscal As Object, ByRef Ready As Boolean)
   Dim insfrmImpresionFiscal As frmImpresorasFiscales
   On Error GoTo h_Error
   Set insfrmImpresionFiscal = New frmImpresorasFiscales
   refDatosImprFiscal.SetEstado Enum_EstadoImpresionFiscal.eEIF_ASIGNAR_CONFIGURACION
   insfrmImpresionFiscal.sInitLookAndFeelAndSetValues refDatosImprFiscal, Ready
   Set insfrmImpresionFiscal = Nothing
h_EXIT:    On Error GoTo 0
   Exit Sub
h_Error: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sAsignarConfiguracionImpresora", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Sub sCortarPapel(ByRef refDatosImprFiscal As Object, ByRef Ready As Boolean)
   Dim insfrmImpresionFiscal As frmImpresorasFiscales
   On Error GoTo h_Error
   Set insfrmImpresionFiscal = New frmImpresorasFiscales
   refDatosImprFiscal.SetEstado Enum_EstadoImpresionFiscal.eEIF_CORTAR_PAPEL
   insfrmImpresionFiscal.sInitLookAndFeelAndSetValues refDatosImprFiscal, Ready
   Set insfrmImpresionFiscal = Nothing
h_EXIT:    On Error GoTo 0
   Exit Sub
h_Error: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sCortarPapel", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Function fGenerarXmlConDatosDeLaVenta(ByVal valDatosImprFiscal As clsDatosImprFiscal) As String
   Dim vXmlDatosDeLaVenta As String
   Dim vDatosDelDocumento As String
   Dim vDatosDelCliente As String
   Dim vDatosDeLosProductos As String
   Dim vDatosDeLosPagos As String
   Dim vDatosDeDescuentoGeneral As String
   Dim vDatosDeCamposDefinibles As String
   Dim vTotalFactura As String
   Dim vTipoDocumento As enum_TipoDocumentoFactura
   Dim vUsaCamposDefinibles As Boolean
   Dim vSiguienteLinea As String
   On Error GoTo h_Error
   vSiguienteLinea = vbCrLf
   vDatosDeCamposDefinibles = ""
   vUsaCamposDefinibles = valDatosImprFiscal.GetImprimeCamposDefinibles
   vTotalFactura = gConvert.fConvierteAString(Abs(valDatosImprFiscal.GetTotalFactura))
   vXmlDatosDeLaVenta = "<GpData>" & vSiguienteLinea & "<GpResult>"
   vDatosDelDocumento = fGenerarDatosDelDocumentoParaXml(gConvert.enumerativoAChar(valDatosImprFiscal.GetTipoDocumentoAsEnum), valDatosImprFiscal.GetNumeroTicket, valDatosImprFiscal.GetSerialImpresoraFiscal, valDatosImprFiscal.GetFechaFacturaAfectada, valDatosImprFiscal.GetHoraModificacion, vTotalFactura, valDatosImprFiscal.GetTotalesMonedaExtranjera, valDatosImprFiscal.GetBaseImponibleIGTF, valDatosImprFiscal.GetTotalAPagar, valDatosImprFiscal.GetIGTFML, valDatosImprFiscal.GetCodigoMonedaCompania)
   vDatosDelCliente = fGenerarDatosDelClienteParaXml(valDatosImprFiscal.GetNumeroRIFCliente, valDatosImprFiscal.GetNombreCliente, valDatosImprFiscal.GetDireccion, valDatosImprFiscal.GetTelefono, valDatosImprFiscal.GetObservaciones)
   vDatosDeLosProductos = fGenerarDatosDeLosProductosParaXml(valDatosImprFiscal.GetItems)
   vDatosDeLosPagos = fGenerarDatosDeLosPagosParaXml(valDatosImprFiscal)
   vDatosDeDescuentoGeneral = fGenerarDatosDelDescuentoGeneralParaXml(valDatosImprFiscal.GetPorcentajeDeDescuento)
   If vUsaCamposDefinibles Then
   vDatosDeCamposDefinibles = fGenerarDatosDeCamposDefiniblesParaXml(valDatosImprFiscal)
   End If
   vXmlDatosDeLaVenta = vXmlDatosDeLaVenta & vDatosDelDocumento & vDatosDelCliente & vDatosDeLosProductos & vDatosDeLosPagos & vDatosDeDescuentoGeneral & vDatosDeCamposDefinibles
   vXmlDatosDeLaVenta = vXmlDatosDeLaVenta & vSiguienteLinea & "</GpResult>" & vSiguienteLinea & "</GpData>"
h_EXIT: On Error GoTo 0
   fGenerarXmlConDatosDeLaVenta = vXmlDatosDeLaVenta
   Exit Function
h_Error:
   fGenerarXmlConDatosDeLaVenta = ""
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fGenerarXmlConDatosDeLaVenta", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fCaracteresXMLValidosEnDatosImprFiscal(ByVal valModeloImprFiscal As String)
    On Error GoTo h_Error
   If gTexto.DfInStr(valModeloImprFiscal, "&") > 0 Then
      gTexto.ReemplazaCaracteresEnElString valModeloImprFiscal, "&", "&amp;"
   End If
   If gTexto.DfInStr(valModeloImprFiscal, ">") > 0 Then
      gTexto.ReemplazaCaracteresEnElString valModeloImprFiscal, ">", "&gt;"
   End If
   If gTexto.DfInStr(valModeloImprFiscal, "<") > 0 Then
      gTexto.ReemplazaCaracteresEnElString valModeloImprFiscal, "<", "&lt;"
   End If
   If gTexto.DfInStr(valModeloImprFiscal, Chr(34)) > 0 Then
      gTexto.ReemplazaCaracteresEnElString valModeloImprFiscal, Chr(34), "&quot;"
   End If
   If gTexto.DfInStr(valModeloImprFiscal, Chr(39)) > 0 Then
      gTexto.ReemplazaCaracteresEnElString valModeloImprFiscal, Chr(39), "&apos;"
   End If
   If gTexto.DfInStr(valModeloImprFiscal, vbCrLf) > 0 Then
    valModeloImprFiscal = gTexto.DfReplace(valModeloImprFiscal, vbCrLf, "&#10;")
   End If
   valModeloImprFiscal = gTexto.fLimpiaStringdeBlancosAAmbosLados(valModeloImprFiscal)
h_EXIT:    On Error GoTo 0
      fCaracteresXMLValidosEnDatosImprFiscal = valModeloImprFiscal
   Exit Function
h_Error:
   fCaracteresXMLValidosEnDatosImprFiscal = ""
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCaracteresXMLValidosEnDatosImprFiscal", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fGenerarXmlConDatosDeLaMaquinaFiscal(ByVal valDatosImpFiscal As clsDatosImprFiscal) As String
   Dim vXmlMaquinaFiscal As String
   Dim vSaltoLinea As String
   Dim insEnumProyecto As clsEnumAdministrativo
   Dim NumberModeloImpresora As Integer
   Dim ModeloImprFiscal As String
   Dim vTipoConexion As String
   Dim vPuertoImpFiscal As String
   Dim vRegistroDeRetornoEnTxt As String
   
   vSaltoLinea = vbCrLf
   ModeloImprFiscal = gConvert.enumerativoAChar(valDatosImpFiscal.GetImpresoraFiscal)
   ModeloImprFiscal = fCaracteresXMLValidosEnDatosImprFiscal(ModeloImprFiscal)
   vTipoConexion = fCaracteresXMLValidosEnDatosImprFiscal(gConvert.enumerativoAChar(valDatosImpFiscal.GetTipoConexion))
   vPuertoImpFiscal = fCaracteresXMLValidosEnDatosImprFiscal(gConvert.enumerativoAChar(valDatosImpFiscal.GetPuertoImpresoraFiscal))
   vRegistroDeRetornoEnTxt = gConvert.ConvertBooleanToString(valDatosImpFiscal.GetRegistroDeRetornoEnTxt)
   On Error GoTo h_Error
   Set insEnumProyecto = New clsEnumAdministrativo
   vXmlMaquinaFiscal = "<GpData>" & vSaltoLinea & "<GpResult>" & vSaltoLinea
   vXmlMaquinaFiscal = vXmlMaquinaFiscal & "<ModeloDeMaquinaFiscal>" & ModeloImprFiscal & "</ModeloDeMaquinaFiscal>" & vSaltoLinea
   vXmlMaquinaFiscal = vXmlMaquinaFiscal & "<PuertoMaquinaFiscal>" & vPuertoImpFiscal & "</PuertoMaquinaFiscal>" & vSaltoLinea
   vXmlMaquinaFiscal = vXmlMaquinaFiscal & "<TipoConexion>" & vTipoConexion & "</TipoConexion>" & vSaltoLinea
   vXmlMaquinaFiscal = vXmlMaquinaFiscal & "<SerialDeMaquinaFiscal>" & valDatosImpFiscal.GetSerialImpresoraFiscal & "</SerialDeMaquinaFiscal>" & vSaltoLinea
   vXmlMaquinaFiscal = vXmlMaquinaFiscal & "<PuertoCom>1</PuertoCom>" & vSaltoLinea
   vXmlMaquinaFiscal = vXmlMaquinaFiscal & "<RegistroDeRetornoEnTxt>" & vRegistroDeRetornoEnTxt & "</RegistroDeRetornoEnTxt>" & vSaltoLinea
   vXmlMaquinaFiscal = vXmlMaquinaFiscal & "</GpResult>" & vSaltoLinea & "</GpData>"
h_EXIT: On Error GoTo 0
   fGenerarXmlConDatosDeLaMaquinaFiscal = vXmlMaquinaFiscal
   Exit Function
h_Error:
   fGenerarXmlConDatosDeLaMaquinaFiscal = ""
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fGenerarXmlConDatosDeLaMaquinaFiscal", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fGenerarDatosDelDocumentoParaXml(ByVal valTipoDocumento As enum_TipoDocumentoFactura, ByVal valNumeroComprobanteFiscal As String, ByVal valSerialMaquina As String, ByVal valFechaDocumento As String, ByVal valHoraDocumento As String, ByVal valTotalFactura As String, ByVal valTotalMonedaExtranjera As String, ByVal valBaseImponibleIGTF As Currency, ByVal valTotalAPagar As Currency, ByVal valIGTML As Currency, ByVal valCodigoMoneda As String) As String
   Dim vDatosDelDocumento As String
   Dim vSiguienteLinea As String
   On Error GoTo h_Error
   vSiguienteLinea = vbCrLf
   vDatosDelDocumento = vSiguienteLinea & "<TipoDeDocumento>" & gConvert.enumerativoAChar(valTipoDocumento) & "</TipoDeDocumento>" & vSiguienteLinea & _
                        "<NumeroComprobanteFiscal>" & valNumeroComprobanteFiscal & "</NumeroComprobanteFiscal>" & vSiguienteLinea & _
                        "<SerialMaquinaFiscal>" & valSerialMaquina & "</SerialMaquinaFiscal>" & vSiguienteLinea & _
                        "<Fecha>" & valFechaDocumento & "</Fecha>" & vSiguienteLinea & _
                        "<HoraModificacion>" & valHoraDocumento & "</HoraModificacion>" & vSiguienteLinea & _
                        "<TotalFactura>" & valTotalFactura & "</TotalFactura>" & vSiguienteLinea & _
                        "<TotalMonedaExtranjera>" & fCaracteresXMLValidosEnDatosImprFiscal(valTotalMonedaExtranjera) & "</TotalMonedaExtranjera>" & vSiguienteLinea & _
                        "<BaseImponibleIGTF>" & fCaracteresXMLValidosEnDatosImprFiscal(gConvert.fConvierteAString(valBaseImponibleIGTF)) & "</BaseImponibleIGTF>" & vSiguienteLinea & _
                        "<IGTFML>" & fCaracteresXMLValidosEnDatosImprFiscal(gConvert.fConvierteAString(valIGTML)) & "</IGTFML>" & vSiguienteLinea & _
                        "<TotalAPagar>" & fCaracteresXMLValidosEnDatosImprFiscal(gConvert.fConvierteAString(valTotalAPagar)) & "</TotalAPagar>" & vSiguienteLinea & _
                        "<CodigoMoneda>" & fCaracteresXMLValidosEnDatosImprFiscal(valCodigoMoneda) & "</CodigoMoneda>" & vSiguienteLinea
h_EXIT: On Error GoTo 0
   fGenerarDatosDelDocumentoParaXml = vDatosDelDocumento
Exit Function
h_Error:
   fGenerarDatosDelDocumentoParaXml = ""
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fGenerarDatosDelDocumentoParaXml", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fGenerarDatosDelClienteParaXml(ByVal valRIF As String, ByVal valRazonSocial As String, ByVal valDireccion As String, ByVal valTelefono As String, ByVal valObservacion As String) As String
   Dim vDatosDelCliente As String
   Dim vSiguienteLinea As String
   On Error GoTo h_Error
   vSiguienteLinea = vbCrLf
   vDatosDelCliente = "<NombreCliente>" & fCaracteresXMLValidosEnDatosImprFiscal(valRazonSocial) & "</NombreCliente>" & vSiguienteLinea & _
                        "<NumeroRIF>" & gTexto.fLimpiaStringdeBlancosAAmbosLados(valRIF) & "</NumeroRIF>" & vSiguienteLinea & _
                        "<DireccionCliente>" & fCaracteresXMLValidosEnDatosImprFiscal(valDireccion) & "</DireccionCliente>" & vSiguienteLinea & _
                        "<TelefonoCliente>" & valTelefono & "</TelefonoCliente>" & vSiguienteLinea & _
                        "<Observaciones>" & fCaracteresXMLValidosEnDatosImprFiscal(valObservacion) & "</Observaciones>" & vSiguienteLinea
h_EXIT: On Error GoTo 0
   fGenerarDatosDelClienteParaXml = vDatosDelCliente
Exit Function
h_Error:
   fGenerarDatosDelClienteParaXml = ""
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fGenerarDatosDelClienteParaXml", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fGenerarDatosDeLosProductosParaXml(ByVal valDatosArticulo As ADODB.Recordset) As String
   Dim vXML As String
   Dim vRenglonItems As String
   Dim vCantidad As String
   Dim vDescripcion As String
   Dim vPrecio As String
   Dim vCodigo As String
   Dim vAlicuota As String
   Dim vSerial As String
   Dim vRollo As String
   Dim vMontoAlicuota As String
   Dim vDescuento As String
   Dim vCodigoArticulo As String
   Dim vPorcentajeAlicuota As String
   Dim rsDatosArticulo As ADODB.Recordset
   Dim vXmlRenglon As String
   Dim vSiguienteLinea  As String
   On Error GoTo h_Error
   vSiguienteLinea = vbCrLf
   Set rsDatosArticulo = valDatosArticulo
   vXML = ""
   vXmlRenglon = ""
   If gDbUtil.fRecordCount(rsDatosArticulo) > 0 Then
      rsDatosArticulo.MoveFirst
      Do While Not rsDatosArticulo.EOF
            vCantidad = rsDatosArticulo.Fields("Cantidad").Value
            gTexto.ReemplazaCaracteresEnElString vCantidad, "-", ""
            vCodigoArticulo = rsDatosArticulo.Fields("Codigo").Value
            vDescripcion = rsDatosArticulo.Fields("Descripcion").Value
            vPrecio = rsDatosArticulo.Fields("PrecioSinIVA").Value
            gTexto.ReemplazaCaracteresEnElString vPrecio, "-", ""
            vAlicuota = rsDatosArticulo.Fields("TipoAlicuota").Value
            vPorcentajeAlicuota = rsDatosArticulo.Fields("AlicuotaIva").Value
            vSerial = rsDatosArticulo.Fields("Serial").Value
            vRollo = rsDatosArticulo.Fields("Rollo").Value
            vDescuento = rsDatosArticulo.Fields("Descuento").Value
            vRenglonItems = "<GpResultDetailRenglonFactura>" & vSiguienteLinea & _
                                 "<Descripcion>" & fCaracteresXMLValidosEnDatosImprFiscal(vDescripcion) & "</Descripcion>" & vSiguienteLinea & _
                                 "<Articulo>" & vCodigoArticulo & "</Articulo>" & vSiguienteLinea & _
                                 "<Cantidad>" & vCantidad & "</Cantidad>" & vSiguienteLinea & _
                                 "<PrecioSinIVA>" & vPrecio & "</PrecioSinIVA>" & vSiguienteLinea & _
                                 "<AlicuotaIva>" & vAlicuota & "</AlicuotaIva>" & vSiguienteLinea & _
                                 "<PorcentajeAlicuota>" & vPorcentajeAlicuota & "</PorcentajeAlicuota>" & vSiguienteLinea & _
                                 "<PorcentajeDescuento>" & vDescuento & "</PorcentajeDescuento>" & vSiguienteLinea & _
                                 "<Rollo>" & fCaracteresXMLValidosEnDatosImprFiscal(vRollo) & "</Rollo>" & vSiguienteLinea & _
                                 "<Serial>" & fCaracteresXMLValidosEnDatosImprFiscal(vSerial) & "</Serial> " & vSiguienteLinea & _
                              "</GpResultDetailRenglonFactura>" & vSiguienteLinea
            vXmlRenglon = vXmlRenglon + vRenglonItems
            rsDatosArticulo.MoveNext
      Loop
      vXML = "<GpDataDetailRenglonFactura>" & vXmlRenglon & "</GpDataDetailRenglonFactura>"
   End If
h_EXIT: On Error GoTo 0
   fGenerarDatosDeLosProductosParaXml = vXML
Exit Function
h_Error:
   fGenerarDatosDeLosProductosParaXml = ""
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fGenerarDatosDeLosProductosParaXml", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fGenerarDatosDeLosPagosParaXml(ByVal valDatosVentImpFiscal As clsDatosImprFiscal) As String
   Dim vXML As String
   Dim vXmlRenglon As String
   Dim vRenglonItems As String
   Dim vMontoCancelado As String
   Dim rsFormaDePago As ADODB.Recordset
   Dim vFormadePago As String
   Dim vSiguienteLinea As String
   Dim vCodigoMoneda As String
   On Error GoTo h_Error
   Set rsFormaDePago = valDatosVentImpFiscal.GetFormaDePago
   vRenglonItems = ""
   vSiguienteLinea = vbCrLf
   vXmlRenglon = ""
   If gDbUtil.fRecordCount(rsFormaDePago) > 0 Then
      rsFormaDePago.MoveFirst
      Do While Not rsFormaDePago.EOF
         vFormadePago = rsFormaDePago("CodigoFormaDelCobro").Value
         vMontoCancelado = rsFormaDePago("Monto").Value
         vCodigoMoneda = rsFormaDePago("CodigoMoneda").Value
         vMontoCancelado = gTexto.DfReplace(vMontoCancelado, "-", "")
         vRenglonItems = "<GpResultDetailRenglonCobro>" & vSiguienteLinea & _
                         "<CodigoFormaDelCobro>" & vFormadePago & "</CodigoFormaDelCobro>" & vSiguienteLinea & _
                         "<Monto>" & vMontoCancelado & "</Monto>" & vSiguienteLinea & _
                         "<CodigoMoneda>" & vCodigoMoneda & "</CodigoMoneda>" & vSiguienteLinea & _
                         "</GpResultDetailRenglonCobro>" & vSiguienteLinea
         vXmlRenglon = vXmlRenglon & vRenglonItems
         rsFormaDePago.MoveNext
      Loop
   Else 'Forma de cobro por defecto Debito
      vXmlRenglon = vSiguienteLinea & "<GpResultDetailRenglonCobro>" & vSiguienteLinea & _
      "<CodigoFormaDelCobro>" & "00003" & "</CodigoFormaDelCobro>" & vSiguienteLinea & _
      "<Monto>" & gConvert.fConvierteAString(0) & "</Monto>" & vSiguienteLinea & _
      "<CodigoMoneda>" & valDatosVentImpFiscal.GetCodigoMonedaCompania & "</CodigoMoneda>" & vSiguienteLinea & _
      "</GpResultDetailRenglonCobro>" & vSiguienteLinea
   End If
   vXML = vSiguienteLinea & "<GpDataDetailRenglonCobro>" & vXmlRenglon & "</GpDataDetailRenglonCobro>" & vSiguienteLinea
h_EXIT: On Error GoTo 0
   fGenerarDatosDeLosPagosParaXml = vXML
Exit Function
h_Error:
   fGenerarDatosDeLosPagosParaXml = ""
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fGenerarDatosDeLosPagosParaXml", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function


Private Function fGenerarDatosDeCamposDefiniblesParaXml(ByVal valDatosVentImpFiscal As clsDatosImprFiscal) As String
   Dim vXML As String
   Dim vXmlRenglon As String
   Dim vRenglonItems As String
   Dim vRsCamposDefinibles As ADODB.Recordset
   Dim vSiguienteLinea As String
   vXML = ""
   vSiguienteLinea = vbCrLf
   Set vRsCamposDefinibles = valDatosVentImpFiscal.GetRSCamposDefinibles
   If Not (vRsCamposDefinibles Is Nothing) Then
      If gDbUtil.fRecordCount(vRsCamposDefinibles) > 0 Then
         vRsCamposDefinibles.MoveFirst
         Do While (Not vRsCamposDefinibles.EOF)
            vRenglonItems = vRsCamposDefinibles.Fields("CampoDefinible").Value
            vXmlRenglon = vXmlRenglon & "<GpResultDetailCamposDefinibles>" & vSiguienteLinea & _
            "<CampoDefinibleValue>" & fCaracteresXMLValidosEnDatosImprFiscal(vRenglonItems) & "</CampoDefinibleValue>" & vSiguienteLinea & _
            "</GpResultDetailCamposDefinibles>" & vSiguienteLinea
            vRsCamposDefinibles.MoveNext
         Loop
         End If
      vXML = "<GpDataDetailCamposDefinibles>" & vXmlRenglon & "</GpDataDetailCamposDefinibles>"
   End If
h_EXIT: On Error GoTo 0
   fGenerarDatosDeCamposDefiniblesParaXml = vXML
Exit Function
h_Error:
   fGenerarDatosDeCamposDefiniblesParaXml = ""
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fGenerarDatosDeCamposDefiniblesParaXml", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fGenerarDatosDelDescuentoGeneralParaXml(ByVal valPrcDescuentoTotal As String) As String
   Dim vDatosDelDocumento As String
   Dim vSiguienteLinea As String
   On Error GoTo h_Error
   vSiguienteLinea = vbCrLf
   vDatosDelDocumento = "<PorcentajeDescuento>" & valPrcDescuentoTotal & "</PorcentajeDescuento>" & vSiguienteLinea
h_EXIT: On Error GoTo 0
   fGenerarDatosDelDescuentoGeneralParaXml = vDatosDelDocumento
Exit Function
h_Error:
   fGenerarDatosDelDescuentoGeneralParaXml = ""
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fGenerarDatosDelDescuentoGeneralParaXml", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fCadenaCaracteresValidos(ByVal valTexto As String, ByVal valMaxLen As Integer, Optional ByVal valVieneDeDireccion As Boolean = False)
   Dim CadenaValida As String
   Dim Index As Long
   Dim caracter As String
   On Error GoTo h_Error
   If valVieneDeDireccion Then
      CadenaValida = valTexto
   Else
      CadenaValida = gTexto.DfLeft(valTexto, valMaxLen)
   End If
   gTexto.ReemplazaCaracteresEnElString CadenaValida, "á", "a"
   gTexto.ReemplazaCaracteresEnElString CadenaValida, "à", "a"
   gTexto.ReemplazaCaracteresEnElString CadenaValida, "ä", "a"
   gTexto.ReemplazaCaracteresEnElString CadenaValida, "â", "a"
   gTexto.ReemplazaCaracteresEnElString CadenaValida, "é", "e"
   gTexto.ReemplazaCaracteresEnElString CadenaValida, "è", "e"
   gTexto.ReemplazaCaracteresEnElString CadenaValida, "ë", "e"
   gTexto.ReemplazaCaracteresEnElString CadenaValida, "ê", "e"
   gTexto.ReemplazaCaracteresEnElString CadenaValida, "í", "i"
   gTexto.ReemplazaCaracteresEnElString CadenaValida, "ì", "i"
   gTexto.ReemplazaCaracteresEnElString CadenaValida, "ï", "i"
   gTexto.ReemplazaCaracteresEnElString CadenaValida, "î", "i"
   gTexto.ReemplazaCaracteresEnElString CadenaValida, "ó", "o"
   gTexto.ReemplazaCaracteresEnElString CadenaValida, "ò", "o"
   gTexto.ReemplazaCaracteresEnElString CadenaValida, "ö", "o"
   gTexto.ReemplazaCaracteresEnElString CadenaValida, "ô", "o"
   gTexto.ReemplazaCaracteresEnElString CadenaValida, "ú", "u"
   gTexto.ReemplazaCaracteresEnElString CadenaValida, "ù", "u"
   gTexto.ReemplazaCaracteresEnElString CadenaValida, "ü", "u"
   gTexto.ReemplazaCaracteresEnElString CadenaValida, "û", "u"
   gTexto.ReemplazaCaracteresEnElString CadenaValida, "Á", "A"
   gTexto.ReemplazaCaracteresEnElString CadenaValida, "À", "A"
   gTexto.ReemplazaCaracteresEnElString CadenaValida, "Ä", "A"
   gTexto.ReemplazaCaracteresEnElString CadenaValida, "Â", "A"
   gTexto.ReemplazaCaracteresEnElString CadenaValida, "É", "E"
   gTexto.ReemplazaCaracteresEnElString CadenaValida, "È", "E"
   gTexto.ReemplazaCaracteresEnElString CadenaValida, "Ë", "E"
   gTexto.ReemplazaCaracteresEnElString CadenaValida, "Ê", "E"
   gTexto.ReemplazaCaracteresEnElString CadenaValida, "Í", "I"
   gTexto.ReemplazaCaracteresEnElString CadenaValida, "Ì", "I"
   gTexto.ReemplazaCaracteresEnElString CadenaValida, "Ï", "I"
   gTexto.ReemplazaCaracteresEnElString CadenaValida, "Î", "I"
   gTexto.ReemplazaCaracteresEnElString CadenaValida, "Ó", "O"
   gTexto.ReemplazaCaracteresEnElString CadenaValida, "Ò", "O"
   gTexto.ReemplazaCaracteresEnElString CadenaValida, "Ö", "O"
   gTexto.ReemplazaCaracteresEnElString CadenaValida, "Ô", "O"
   gTexto.ReemplazaCaracteresEnElString CadenaValida, "Ú", "U"
   gTexto.ReemplazaCaracteresEnElString CadenaValida, "Ù", "U"
   gTexto.ReemplazaCaracteresEnElString CadenaValida, "Ü", "U"
   gTexto.ReemplazaCaracteresEnElString CadenaValida, "Û", "U"
   gTexto.ReemplazaCaracteresEnElString CadenaValida, "ñ", "n"
   gTexto.ReemplazaCaracteresEnElString CadenaValida, "Ñ", "N"
   gTexto.ReemplazaCaracteresEnElString CadenaValida, "'", "´"
   gTexto.ReemplazaCaracteresEnElString CadenaValida, "@", ""
   gTexto.ReemplazaCaracteresEnElString CadenaValida, "#", ""
   gTexto.ReemplazaCaracteresEnElString CadenaValida, "%", ""
   gTexto.ReemplazaCaracteresEnElString CadenaValida, "ª", ""
   gTexto.ReemplazaCaracteresEnElString CadenaValida, "º", ""
   gTexto.ReemplazaCaracteresEnElString CadenaValida, "·", ""
   gTexto.ReemplazaCaracteresEnElString CadenaValida, "¨", ""
   gTexto.ReemplazaCaracteresEnElString CadenaValida, "Ç", ""
   gTexto.ReemplazaCaracteresEnElString CadenaValida, "ç", ""
   gTexto.ReemplazaCaracteresEnElString CadenaValida, "&", ""
   gTexto.ReemplazaCaracteresEnElString CadenaValida, Chr(10), ""
   gTexto.ReemplazaCaracteresEnElString CadenaValida, Chr(13), ""
h_EXIT: On Error GoTo 0
   fCadenaCaracteresValidos = CadenaValida
   Exit Function
h_Error: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCadenaCaracteresValidos", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Sub sImprimeComprobanteNoFiscal(ByRef refDatosImprFiscal As Object, ByRef refReady As Boolean)
   Dim insfrmImpresionFiscal As frmImpresorasFiscales
   On Error GoTo h_Error
   Set insfrmImpresionFiscal = New frmImpresorasFiscales
   refDatosImprFiscal.SetEstado Enum_EstadoImpresionFiscal.eEIF_IMPRIME_DOCUMENTO_NOFISCAL
   insfrmImpresionFiscal.sInitLookAndFeelAndSetValues refDatosImprFiscal, refReady
   Set insfrmImpresionFiscal = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_Error: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sImprimeComprobanteNoFiscal", CM_FILE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub


