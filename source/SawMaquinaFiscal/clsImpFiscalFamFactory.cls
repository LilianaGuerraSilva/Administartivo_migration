VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsImpFiscalFamFactory"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Attribute VB_Ext_KEY = "SavedWithClassBuilder6" ,"Yes"
Attribute VB_Ext_KEY = "Top_Level" ,"Yes"
'//////////////////////////////////////////////////////////
'// METODOS EN LA DLL tfhkaif.dll Version 1.0.1.2.
'//////////////////////////////////////////////////////////
Private Declare Function OpenFpctrl Lib "tfhkaif.dll" (ByVal lpPortName As String) As Long
Private Declare Function CloseFpctrl Lib "tfhkaif.dll" () As Long
Private Declare Function CheckFprinter Lib "tfhkaif.dll" () As Long
Private Declare Function ReadFpStatus Lib "tfhkaif.dll" (Status As Long, error As Long) As Long
Private Declare Function SendCmd Lib "tfhkaif.dll" (Status As Long, error As Long, ByVal Cmd As String) As Long
Private Declare Function SendNCmd Lib "tfhkaif.dll" (Status As Long, error As Long, ByVal buffer As String) As Long
Private Declare Function SendFileCmd Lib "tfhkaif.dll" (Status As Long, error As Long, ByVal file As String) As Long
Private Declare Function UploadStatusCmd Lib "tfhkaif.dll" (Status As Long, error As Long, ByVal Cmd As String, ByVal file As String) As Long
Private Declare Function UploadReportCmd Lib "tfhkaif.dll" (Status As Long, error As Long, ByVal Cmd As String, ByVal file As String) As Long
Private Declare Function UploadStatusDin Lib "tfhkaif.dll" (Status As Long, error As Long, ByVal Cmd As String, ByVal Cadena As String) As Long
Private Declare Function UploadReportDin Lib "tfhkaif.dll" (Status As Long, error As Long, ByVal Cmd As String, ByVal Cadena As String) As Long

Private Declare Function GetSystemDirectory Lib "kernel32" Alias "GetSystemDirectoryA" (ByVal lpBuffer As String, ByVal nSize As Long) As Long

Const FORMATO_PRECIO As String = "0000000000"
Const FORMATO_CANTIDAD As String = "00000000"
Const FORMATO_DECIMAL As String = "#0.00"
Const FORMATO_ENTERO_2 As String = "00"
Const FORMATO_ENTERO_12 As String = "000000000000"
Private Const INT_VERSION_DE_HKA_DLL_MINIMA_SOPORTADA As Integer = 1012 ' EQUIVALE  A LA VERSION 1.0.1.2
Private Const STR_VERSION_DE_HKA_DLL_MINIMA_SOPORTADA As String = "1.0.1.2" ' EQUIVALE  A LA VERSION 1.0.1.2


Private Function CM_FILE_NAME() As String
   CM_FILE_NAME = "clsImpFiscalFamFactory"
End Function

Private Function CM_MESSAGE_NAME() As String
   CM_MESSAGE_NAME = "Impresora Fiscal Familia Factory"
End Function

Private Function GetGender() As Enum_Gender
   GetGender = eg_Female
End Function

Public Function fGetVersionDLLEnSystem32() As Boolean
   Dim fso As FileSystemObject
   Dim vVersion As String
   Dim vResultado As Boolean
   Dim vVersionToCOmpare As String
   Dim vBuffer As String
   Dim vLongitudDeDirectorio As Long
   Dim vDirectorioSystem32 As String
   On Error GoTo h_Error
   Set fso = New FileSystemObject
   vResultado = True
   vBuffer = String$(260, Chr(0))
   vLongitudDeDirectorio = GetSystemDirectory(vBuffer, Len(vBuffer))
   vDirectorioSystem32 = Left$(vBuffer, vLongitudDeDirectorio)
   If gUtilFile.fExisteElArchivo(vDirectorioSystem32 & "\tfhkaif.dll") Then
      vVersion = fso.GetFileVersion(vDirectorioSystem32 & "\tfhkaif.dll")
       gTexto.ReemplazaCaracteresEnElString vVersion, ".", ""
      If gConvert.ConvierteAInteger(vVersion) < INT_VERSION_DE_HKA_DLL_MINIMA_SOPORTADA Then
         vResultado = False
         gMessage.Advertencia "Por favor actualice su archivo tfhkaif.dll, actualmente en su equipo esta instalada la versión Nro.: " & vVersion & " del archivo tfhkaif.dll, la version mínima recomendada es " & STR_VERSION_DE_HKA_DLL_MINIMA_SOPORTADA & ", es posible que la impresora no funcione correctamente. " & vbCrLf & "Comuníquese con Gálac Software para obtener el archivo indicado."
      End If
   Else
      vResultado = False
      gMessage.Advertencia "Usted no tiene el archivo tfhkaif.dll en la carpeta System32"
   End If
   Set fso = Nothing
   
h_EXIT: On Error GoTo 0
   fGetVersionDLLEnSystem32 = vResultado
   Exit Function
h_Error:
   fGetVersionDLLEnSystem32 = False
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fGetVersionDLLEnSystem32", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function sAbreCF(ByVal valPuerto As String, ByVal valImpresoraFiscal As Enum_ImpresorasFiscales) As Boolean
   Dim vPuerto As String
   Dim vPuertoAbierto As Boolean
   On Error GoTo h_Error
   If gTexto.fS1SeEncuentraEnS2("COM", valPuerto) Then
      vPuertoAbierto = OpenFpctrl(valPuerto)
   Else
      vPuertoAbierto = OpenFpctrl("COM" & valPuerto)
   End If
   If Not vPuertoAbierto Then
      gMessage.Advertencia "Error de comunicación "
      CloseFpctrl
   End If
   sAbreCF = vPuertoAbierto
h_EXIT: On Error GoTo 0
   Exit Function
h_Error:
   sAbreCF = False
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sAbreCF", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function sCierraCF(ByVal valCerrarCF As Boolean)
   Dim vCmd As String
   On Error GoTo h_Error
   vCmd = "7"
   If Not valCerrarCF Then
      mbRet = SendCmd(0, 0, vCmd)
   End If
    mbRet = CloseFpctrl
h_EXIT: On Error GoTo 0
   Exit Function
h_Error: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sCierraCF", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSerialMemoriaFiscal(ByRef refReady As Boolean, valPuerto As String, ByVal valImpresora As Enum_ImpresorasFiscales) As String
   Dim vCmd As String
   Dim vSerial As String
   Dim vCadena As String
   Dim vStatus As Long
   Dim vError As Long
   Dim vResultado As Boolean
   On Error GoTo h_Error
   If Not fGetVersionDLLEnSystem32 Then
      refReady = False
      GoTo h_EXIT
   End If
   If sAbreCF(valPuerto, valImpresora) Then
      vCmd = "S1"
      vCadena = (String(250, "*"))
      vResultado = UploadStatusDin(vStatus, vError, vCmd, vCadena)
      refReady = vResultado
      If vResultado Then
         Select Case valImpresora
         Case eIF_DASCOM_TALLY_DT_230, eIF_ACLAS_PP9, eIF_SAMSUNG_BIXOLON_SRP_812, eif_HKA80
            vSerial = gTexto.DfMid(vCadena, 93, 10)
         Case eif_HKA112
               vSerial = gTexto.DfMid(vCadena, 71, 10)
         Case Else
            vSerial = gTexto.DfMid(vCadena, 67, 10)
         End Select
      Else
         gMessage.Advertencia "Error de comunicación"
      End If
   Else
      gMessage.Advertencia "Error de comunicación"
   End If
h_EXIT: On Error GoTo 0
   CloseFpctrl
   sRetardoTiempo 10000
   fSerialMemoriaFiscal = vSerial
   Exit Function
h_Error:
   CloseFpctrl
   fSerialMemoriaFiscal = ""
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSerialMemoriaFiscal", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fUltimoNumeroMemoriaFiscal(ByVal valPuerto As String, ByVal valImpresora As Enum_ImpresorasFiscales, ByVal valEsNotaDeCredito As Boolean, ByRef refComandoEjecutado) As String
   Dim vCmd As String
   Dim vCadena As String
   Dim vStatus As Long
   Dim vError As Long
   Dim vNumeroCupon  As String
   Dim vComandoEjecutado As Boolean
   On Error GoTo h_Error
   vStatus = 0
   vError = 0
   If sAbreCF(valPuerto, valImpresora) Then
      vCadena = (String(250, "*"))
      If valEsNotaDeCredito Then
         Select Case valImpresora
            Case eIF_DASCOM_TALLY_DT_230, eIF_ACLAS_PP9, eIF_SAMSUNG_BIXOLON_SRP_812, eif_HKA80
               vCmd = "S1"
               vComandoEjecutado = UploadStatusDin(vStatus, vError, vCmd, vCadena)
               vNumeroCupon = gTexto.DfMid(vCadena, 48, 8)
            Case eIF_SAMSUNG_BIXOLON_SRP_280
               vCmd = "S1"
               vComandoEjecutado = UploadStatusDin(vStatus, vError, vCmd, vCadena)
               vNumeroCupon = gTexto.DfMid(vCadena, 89, 8)
            Case eif_HKA112
               vCmd = "S1"
               vComandoEjecutado = UploadStatusDin(vStatus, vError, vCmd, vCadena)
               vNumeroCupon = gTexto.DfMid(vCadena, 96, 8)
            Case Else
               vCmd = "U0X"
               vComandoEjecutado = UploadReportDin(vStatus, vError, vCmd, vCadena)
               vNumeroCupon = gTexto.DfMid(vCadena, 169, 8)
         End Select
      Else
         vCmd = "S1"
         vComandoEjecutado = UploadStatusDin(vStatus, vError, vCmd, vCadena)
         vNumeroCupon = gTexto.DfMid(vCadena, 22, 8)
      End If
   Else
      gMessage.Advertencia "Error al intentar obtener Numeros en Memoria Fiscal "
   End If
   refComandoEjecutado = vComandoEjecutado
h_EXIT: On Error GoTo 0
   CloseFpctrl
   sRetardoTiempo 10000
   fUltimoNumeroMemoriaFiscal = vNumeroCupon
   Exit Function
h_Error:
   CloseFpctrl
   refComandoEjecutado = False
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fUltimoNumeroMemoriaFiscal", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Sub sRetardoTiempo(valTiempo As Integer)
   On Error GoTo h_Error
   For i = 0 To valTiempo
      For j = 0 To valTiempo
      Next j
   Next i
h_EXIT: On Error GoTo 0
   Exit Sub
h_Error:
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sRetardoTiempo", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Sub sCierreZ(ByRef refReady As Boolean, valPuerto As String, ByVal valImpresora As Enum_ImpresorasFiscales)
'Realiza un reporte Z
   Dim vCmd As String
   Dim vStatus As Long
   Dim vError As Long
   Dim vComandoProcesado As Long
   On Error GoTo h_Error
   vStatus = 0
   vError = 0
   If sAbreCF(valPuerto, valImpresora) Then
      vCmd = "I0Z"
      vComandoProcesado = SendCmd(vStatus, vError, vCmd)
      refReady = vComandoProcesado
   Else
      refReady = False
   End If
   sCierraCF True
   sRetardoTiempo 10000
h_EXIT: On Error GoTo 0
   Exit Sub
h_Error: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sCierreZ", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Function fObtenerUltimoNumeroDeReporteZ(ByVal valPuerto As String, ByVal valImpresora As Enum_ImpresorasFiscales) As String
   Dim vUltimoNumeroDeReporteZ As String
   Dim vComandoProcesado As Boolean
   Dim vCmd As String
   Dim vStatus As Long
   Dim vError As Long
   Dim vCadena As String
   On Error GoTo h_Error
   vStatus = 0
   vError = 0
   vCadena = (String(250, "*"))
   For j = 0 To 10 'Tiempo para que libere el recurso
      sRetardoTiempo 10000
   Next j
   If sAbreCF(valPuerto, valImpresora) Then
      If valImpresora = eIF_SAMSUNG_BIXOLON_SRP_350 Then
         vCmd = "S1"
         vComandoProcesado = UploadReportDin(vStatus, vError, vCmd, vCadena)
         vUltimoNumeroDeReporteZ = gTexto.DfMid(vCadena, 48, 4)
      Else
         vCmd = "U0Z"
         vComandoProcesado = UploadReportDin(vStatus, vError, vCmd, vCadena)
         vUltimoNumeroDeReporteZ = gTexto.DfMid(vCadena, 1, 4)
      End If
      sRetardoTiempo 10000
   End If
h_EXIT:    On Error GoTo 0
   CloseFpctrl
   sRetardoTiempo 10000
   fObtenerUltimoNumeroDeReporteZ = vUltimoNumeroDeReporteZ
   Exit Function
h_Error:
   fObtenerUltimoNumeroDeReporteZ = ""
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fObtenerUltimoNumeroDeReporteZ", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Sub sCierreX(ByVal valPuerto As String, ByVal valImpresoraFiscal As Enum_ImpresorasFiscales, ByRef vComandoProcesado As Boolean)
   Dim vCmd As String
   Dim vError As Long
   Dim vStatus As Long
   On Error GoTo h_Error
   vError = 0
   vStatus = 0
   If sAbreCF(valPuerto, valImpresoraFiscal) Then
      vCmd = "I0X"
      vComandoProcesado = SendCmd(vStatus, vError, vCmd)
      sCierraCF True
   Else
      vComandoProcesado = False
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_Error:
   vComandoProcesado = False
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sCierreX", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Sub sImprimeVentaArticulo(ByVal valEnumPrinter As Enum_ImpresorasFiscales, insDatosImprFiscal As clsDatosImprFiscal, ByRef refReady As Boolean)
      Dim rsDatosArticulo As ADODB.Recordset
      Dim Alicuotas As String
      Dim Cantidades As String
      Dim Precios As String
      Dim Salida As String
      Dim Descripcion As String
      Dim sql  As String
      Dim Tipo  As String
      Dim Cmd As String
      Dim PorcetajeDesRenglon As Currency
      Dim PorcetajeDesTotal As Integer
      Dim Texto As String
      Dim enImprFiscal As Byte
      Dim mbRet As Boolean
      Dim vPrecioFormateado As String
      Dim vCantidadFormateada As String
      
      On Error GoTo h_Error
      If Not fGetVersionDLLEnSystem32 Then
         refReady = False
         GoTo h_EXIT
      End If
      Select Case valEnumPrinter
      Case eIF_DASCOM_TALLY_DT_230, eIF_SAMSUNG_BIXOLON_SRP_280, eIF_ACLAS_PP9, eIF_SAMSUNG_BIXOLON_SRP_812, eif_HKA80, eif_HKA112:
         sImprimeDatosClientesDasconT valEnumPrinter, insDatosImprFiscal
      Case eIF_OKI_ML_1120:
         sImprimeDatosClientesOki1120 valEnumPrinter, insDatosImprFiscal
      Case Else
         sImprimeDatosClientesSamsung valEnumPrinter, insDatosImprFiscal
      End Select
      vComandoProcesado = True
      PorcetajeDesTotal = insDatosImprFiscal.GetPorcentajeDeDescuento * 100
      Set rsDatosArticulo = insDatosImprFiscal.GetItems
      If Not rsDatosArticulo.BOF And Not rsDatosArticulo.EOF Then
         rsDatosArticulo.MoveFirst
         Do While Not rsDatosArticulo.EOF
            Alicuotas = rsDatosArticulo.Fields("AlicuotaIVA").Value
            Cantidades = rsDatosArticulo.Fields("Cantidad").Value
            Precios = rsDatosArticulo.Fields("PrecioSinIVA").Value
            Descripcion = rsDatosArticulo.Fields("Descripcion").Value
            Tipo = rsDatosArticulo.Fields("TipoAlicuota").Value
            gTexto.LimpiaStringDeChar Cantidades, "-"
            gTexto.LimpiaStringDeChar Precios, "-"
            Dim vPorcentajeDescuentoFormateado As String
            vPorcentajeDescuentoFormateado = rsDatosArticulo.Fields("Descuento").Value
            vPorcentajeDescuentoFormateado = fDarFormatoADescuentoParaImpresion(vPorcentajeDescuentoFormateado)
            vPrecioFormateado = fDarFormatoAPrecioParaImpresion(Precios)
            vCantidadFormateada = fDarFormatoACantidadParaImpresion(Cantidades)
            
            If Not (insDatosImprFiscal.GetImprimeSerialRolloLuegoDeDescripArticulo) Then
               fImprimirSerialYRollo rsDatosArticulo.Fields("Serial").Value, rsDatosArticulo.Fields("Rollo").Value
            End If
            If Not fVenta(Descripcion, vCantidadFormateada, vPrecioFormateado, Alicuotas, Tipo, vPorcentajeDescuentoFormateado, False, valEnumPrinter) Then
               sCierraCF False
               GoTo h_EXIT
            End If
            If (insDatosImprFiscal.GetImprimeSerialRolloLuegoDeDescripArticulo) Then
               fImprimirSerialYRollo rsDatosArticulo.Fields("Serial").Value, rsDatosArticulo.Fields("Rollo").Value
            End If
           rsDatosArticulo.MoveNext
         Loop
        If insDatosImprFiscal.GetImprimeCamposDefinibles Then
            Select Case valEnumPrinter
            
            Case eIF_DASCOM_TALLY_DT_230, eIF_SAMSUNG_BIXOLON_SRP_280, eIF_SAMSUNG_BIXOLON_SRP_812, eif_HKA80, eif_HKA112
                sImprimeCamposDefiniblesDascomT insDatosImprFiscal
            Case Else
               sImprimeCamposDefinibles insDatosImprFiscal
            End Select
            
        End If
        If PorcetajeDesTotal <> 0 Then
            Cmd = "3"
            vComandoProcesado = SendCmd(0, 0, Cmd)
            vPorcentajeDescuentoFormateado = insDatosImprFiscal.GetPorcentajeDeDescuento
            vPorcentajeDescuentoFormateado = fDarFormatoADescuentoParaImpresion(vPorcentajeDescuentoFormateado)
            Cmd = "p" & "-" & vPorcentajeDescuentoFormateado
            vComandoProcesado = SendCmd(0, 0, Cmd)
         End If
      End If
      If vComandoProcesado Then
         If Not insDatosImprFiscal.GetFormaDePago Is Nothing Then
            sFormaDePago insDatosImprFiscal
         Else
            Select Case valEnumPrinter
               Case eIF_DASCOM_TALLY_DT_230, eIF_SAMSUNG_BIXOLON_SRP_280, eIF_ACLAS_PP9, eIF_SAMSUNG_BIXOLON_SRP_812, eif_HKA80, eif_HKA112
                  Cmd = "101"
               Case Else
                  Cmd = "f01000000000000"
            End Select
            vComandoProcesado = SendCmd(0, 0, Cmd)
         End If
      End If
      Set rsDatosArticulo = Nothing
      sCierraCF True
      sRetardoTiempo 10000
      refReady = vComandoProcesado
h_EXIT: On Error GoTo 0
   Exit Sub
h_Error:
   refReady = False
   sCierraCF False
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sImprimeVentaArticulo_SANSSUMG", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Function fVenta(ByVal Descripcion As String, ByVal Cantidad As String, ByVal Precio As String, ByVal Alicuota As String, ByVal Tipo As Integer, ByVal PorcetajeDesRenglon As String, ByVal EsDevolucion As Boolean, ByVal valModelo As Enum_ImpresorasFiscales) As Boolean
   Dim strPrecio As String
   Dim strCantidad As String
   Dim Posicion As Long
   Dim Cmd  As String
   Dim vRealizoVenta As Boolean
   If EsDevolucion Then
      Cmd = "d"
      Cmd = Cmd & Tipo
   Else
      Cmd = Chr(&H20 + Tipo)
    End If
   Cmd = Cmd & Precio
   Cmd = Cmd & Cantidad
      
   Select Case valModelo
   
   Case eIF_DASCOM_TALLY_1125, eIF_DASCOM_TALLY_DT_230, eIF_ACLAS_PP9, eIF_SAMSUNG_BIXOLON_SRP_812, eif_HKA80, eif_HKA112
       Cmd = Cmd & gTexto.DfLeft(Descripcion, 150)
   Case Else
       Cmd = Cmd & gTexto.DfLeft(Descripcion & Space(50), 40)
   End Select

   vRealizoVenta = SendCmd(0, 0, Cmd)
   If PorcetajeDesRenglon > 0 Then
      Cmd = "p- " & PorcetajeDesRenglon
      vRealizoVenta = SendCmd(0, 0, Cmd)
   End If
  fVenta = vRealizoVenta
End Function

Private Sub sImprimeDatosClientesSamsung(ByVal valEnumPrinter As Enum_ImpresorasFiscales, ByVal insDatosImprFiscal As clsDatosImprFiscal)
   Dim Texto As String
   Dim Cmd As String
   Dim mbRet As Boolean
      If Not insDatosImprFiscal.GetNombreCliente = "" Then
         Texto = gTexto.DfMid(insDatosImprFiscal.GetNombreCliente, 1, 30)
         Cmd = "i01Nombre: " & Texto
           mbRet = SendCmd(0, 0, Cmd)
         Texto = gTexto.DfMid(insDatosImprFiscal.GetNombreCliente, 31, 30)
         Cmd = "i02          " & Texto
           mbRet = SendCmd(0, 0, Cmd)
         Cmd = "i03CI/RIF:" & insDatosImprFiscal.GetNumeroRIFCliente & "  Tlf: " & insDatosImprFiscal.GetTelefono
           mbRet = SendCmd(0, 0, Cmd)
         Texto = gTexto.DfMid(insDatosImprFiscal.GetDireccion, 1, 30)
         Cmd = "i04Dirección: " & Texto
           mbRet = SendCmd(0, 0, Cmd)
         Texto = gTexto.DfMid(insDatosImprFiscal.GetDireccion, 31, 30)
         Cmd = "i05          " & Texto
           mbRet = SendCmd(0, 0, Cmd)
        If insDatosImprFiscal.GetObservaciones <> "" Then
            Texto = gTexto.DfMid(insDatosImprFiscal.GetObservaciones, 1, 30)
            Cmd = "i06Observaciones: " & Texto
            mbRet = SendCmd(0, 0, Cmd)
            Texto = gTexto.DfMid(insDatosImprFiscal.GetObservaciones, 31, 30)
            Cmd = "i07          " & Texto
             mbRet = SendCmd(0, 0, Cmd)
        End If
      ElseIf insDatosImprFiscal.GetObservaciones <> "" Then
         Texto = gTexto.DfMid(insDatosImprFiscal.GetObservaciones, 1, 30)
         Cmd = "i01Observaciones: " & Texto
            mbRet = SendCmd(0, 0, Cmd)
         Texto = gTexto.DfMid(insDatosImprFiscal.GetObservaciones, 31, 30)
         Cmd = "i02          " & Texto
            mbRet = SendCmd(0, 0, Cmd)
      End If

End Sub

Private Sub sImprimeDatosClientesOki1120(ByVal valEnumPrinter As Enum_ImpresorasFiscales, ByVal insDatosImprFiscal As clsDatosImprFiscal)
   Dim Texto As String
   Dim Cmd As String
   Dim mbRet As Boolean
      If Not insDatosImprFiscal.GetNombreCliente = "" Then
         Texto = gTexto.DfMid(insDatosImprFiscal.GetNombreCliente, 1, 30)
         Cmd = "i01Nombre: " & Texto
         mbRet = SendCmd(0, 0, Cmd)
         Texto = gTexto.DfMid(insDatosImprFiscal.GetNombreCliente, 31, 39)
         Cmd = "i02" & Texto
         mbRet = SendCmd(0, 0, Cmd)
         Cmd = "i03CI/RIF:" & insDatosImprFiscal.GetNumeroRIFCliente & "  TLF: " & insDatosImprFiscal.GetTelefono
            mbRet = SendCmd(0, 0, Cmd)
         Texto = gTexto.DfMid(insDatosImprFiscal.GetDireccion, 1, 27)
         Cmd = "i04Dirección: " & Texto
            mbRet = SendCmd(0, 0, Cmd)
         Texto = gTexto.DfMid(insDatosImprFiscal.GetDireccion, 28, 36)
         Cmd = "i05" & Texto
            mbRet = SendCmd(0, 0, Cmd)
        If insDatosImprFiscal.GetObservaciones <> "" Then
            Texto = gTexto.DfMid(insDatosImprFiscal.GetObservaciones, 1, 34)
            Cmd = "i06OBS: " & Texto
               mbRet = SendCmd(0, 0, Cmd)
            Texto = gTexto.DfMid(insDatosImprFiscal.GetObservaciones, 35, 39)
            Cmd = "i07" & Texto
            mbRet = SendCmd(0, 0, Cmd)
            Texto = gTexto.DfMid(insDatosImprFiscal.GetObservaciones, 74, 39)
            Cmd = "i08" & Texto
            mbRet = SendCmd(0, 0, Cmd)
        End If
      ElseIf insDatosImprFiscal.GetObservaciones <> "" Then
         Texto = gTexto.DfMid(insDatosImprFiscal.GetObservaciones, 1, 30)
         Cmd = "i01Observaciones: " & Texto
            mbRet = SendCmd(0, 0, Cmd)
         Texto = gTexto.DfMid(insDatosImprFiscal.GetDireccion, 30, 30)
         Cmd = "i02          " & Texto
            mbRet = SendCmd(0, 0, Cmd)
      End If
End Sub

Private Sub sImprimeCamposDefinibles(ByVal valInsDatosImprFiscal As clsDatosImprFiscal)
   Dim vRsCamposDefinibles As ADODB.Recordset
   Dim vText As String
   Dim vOutPut As String
   Dim Cmd As String
   Dim vLinea As Integer
   Dim vComandoProcesado As Boolean
   On Error GoTo h_Error
   vLinea = 91
   sLimpiarPieDePagina
   If valInsDatosImprFiscal.GetImprimeCamposDefinibles Then
      Set vRsCamposDefinibles = valInsDatosImprFiscal.GetRSCamposDefinibles
      If Not (vRsCamposDefinibles Is Nothing) Then
         If gDbUtil.fRecordCount(vRsCamposDefinibles) > 0 Then
            vRsCamposDefinibles.MoveFirst
            Do While Not vRsCamposDefinibles.EOF
               vText = gTexto.DfMid(vRsCamposDefinibles.Fields("CampoDefinible").Value, 1, 40)
               If gTexto.DfLen(vText) > 0 Then
                  Cmd = "P" & "H" & vLinea & vText
                  vComandoProcesado = SendCmd(0, 0, Cmd)
                  vLinea = vLinea + 1
                  If Not vComandoProcesado Then
                     GoTo h_EXIT
                  End If
               End If
               vRsCamposDefinibles.MoveNext
            Loop
         End If
      End If
      Set vRsCamposDefinibles = Nothing
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_Error: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sImprimeCamposDefiniblesSAMSUNG", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sImprimeCamposDefiniblesDascomT(ByVal valInsDatosImprFiscal As clsDatosImprFiscal)
   Dim vRsCamposDefinibles As ADODB.Recordset
   Dim vText As String
   Dim vOutPut As String
   Dim Cmd As String
   Dim vLinea As Integer
   Dim vComandoProcesado As Boolean
   On Error GoTo h_Error
   vLinea = 5
   If valInsDatosImprFiscal.GetImprimeCamposDefinibles Then
      Set vRsCamposDefinibles = valInsDatosImprFiscal.GetRSCamposDefinibles
      If Not (vRsCamposDefinibles Is Nothing) Then
         If gDbUtil.fRecordCount(vRsCamposDefinibles) > 0 Then
            vRsCamposDefinibles.MoveFirst
            Do While (Not vRsCamposDefinibles.EOF) And vLinea < 10
               vText = gTexto.DfMid(vRsCamposDefinibles.Fields("CampoDefinible").Value, 1, 40)
               If gTexto.DfLen(vText) > 0 Then
                  Cmd = "i0" & vLinea & vText
                  vComandoProcesado = SendCmd(0, 0, Cmd)
                  vLinea = vLinea + 1
                  If Not vComandoProcesado Then
                     GoTo h_EXIT
                  End If
               End If
               vRsCamposDefinibles.MoveNext
            Loop
         End If
      End If
      Set vRsCamposDefinibles = Nothing
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_Error: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sImprimeCamposDefiniblesSAMSUNG", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sFormaDePago(insDatosImprFiscal As clsDatosImprFiscal)
   Dim MontoCancelado As Currency
   Dim rsFormaDePago As ADODB.Recordset
   Dim formadePago As String
   Dim FormaDeCobro As String
   Dim Cmd As String
   On Error GoTo h_Error
   Set rsFormaDePago = insDatosImprFiscal.GetFormaDePago
   If gDbUtil.fRecordCount(rsFormaDePago) > 0 Then
      rsFormaDePago.MoveFirst
      Do While Not rsFormaDePago.EOF
         MontoCancelado = 0
         FormaDeCobro = fFormaDeCobro(rsFormaDePago("CodigoFormaDelCobro").Value, insDatosImprFiscal.GetImpresoraFiscal)
         If rsFormaDePago.RecordCount = 1 Then
            Cmd = "1" & FormaDeCobro ' asigna el monto total al documento
         Else
            MontoCancelado = gConvert.fConvierteACurrency(rsFormaDePago.Fields("Monto").Value)
            MontoCancelado = MontoCancelado * 100
            Cmd = "2" & FormaDeCobro & Format(MontoCancelado, FORMATO_ENTERO_12)  ' Se utiliza para varias formas de cobro
         End If
            mbRet = SendCmd(0, 0, Cmd)
            rsFormaDePago.MoveNext
      Loop
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_Error: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sFormaDePago_SANSSUMG", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Function fPosicionDeCaracterInv(ByVal valCadena As String, ByVal valCaracter As String) As Long
   Dim vPos As Long
   Dim i As Long
   Dim vCount As Long
   On Error GoTo h_Error
   vPos = 0
   vCount = 0
   If gTexto.DfLen(valCadena) > 0 Then
      For i = gTexto.DfLen(valCadena) To 0 Step -1
         If gTexto.DfMid(valCadena, i, 1) = valCaracter Then
            vPos = vCount
            Exit For
         End If
         vCount = vCount + 1
      Next i
   End If
h_EXIT:    On Error GoTo 0
   fPosicionDeCaracterInv = vPos
   Exit Function
h_Error: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fPosicionDeCaracterInv", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Sub sLimpiarPieDePagina()
   Dim Cmd As String
   Dim vLinea As Integer
   Dim i As Integer
   Dim vComandoProcesado As Boolean
   On Error GoTo h_Error
   For i = 91 To 98
    Cmd = "P" & "H" & i & ""
    vComandoProcesado = SendCmd(0, 0, Cmd)
    If Not vComandoProcesado Then
      GoTo h_EXIT
    End If
   Next i
h_EXIT:    On Error GoTo 0
   Exit Sub
h_Error: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sImprimeCamposDefinibles", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Function fFormaDeCobro(FormaDeCobro As String, ImprFiscal As Enum_ImpresorasFiscales) As String
    Dim Resultado As String
    Dim EsNuevaTheFactory As Boolean
    On Error GoTo h_Error
    
    EsNuevaTheFactory = (eIF_DASCOM_TALLY_DT_230 Or ImprFiscal = eIF_ACLAS_PP9 Or ImprFiscal = eIF_SAMSUNG_BIXOLON_SRP_812 Or ImprFiscal = eif_HKA80)
    
    If EsNuevaTheFactory Then
      Select Case FormaDeCobro
          Case "00001":    Resultado = "01" ' Efectivo
          Case "00002":    Resultado = "07" 'Cheque
          Case "00003":    Resultado = "13" 'Debito
          Case Else: Resultado = "01"
       End Select
    Else
      Select Case FormaDeCobro
          Case "00001":    Resultado = "01" ' Efectivo
          Case "00002":    Resultado = "05" 'Cheque
          Case "00003":    Resultado = "09" 'Debito
          Case Else: Resultado = "01"
       End Select
    End If
    fFormaDeCobro = Resultado
h_EXIT: On Error GoTo 0
   Exit Function
h_Error: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fFormaDeCobro", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fCancelaCuponFiscal(ByVal valAbrirElPuerto As Boolean, ByVal valPuerto As String, ByVal valImpresora As Enum_ImpresorasFiscales) As String
   Dim vCommand As String
   Dim vExito As Boolean
   Dim vStatus As Long
   Dim vSerial As String
   Dim vError As Long
   Dim lport As Long
   Dim PUERTO As String
   On Error GoTo h_Error
   If valAbrirElPuerto Then
      lport = OpenFpctrl(PUERTO)
   Else
      lport = 1
   End If
   vCommand = "7" ' Comando que cancela la transacción
   vExito = SendCmd(vStatus, vError, vCommand)
   If Not vExito Then
    '  sHandleErrorAPi vError
   End If
   If valAbrirElPuerto Then CloseFpctrl
h_EXIT: On Error GoTo 0
   fCancelaCuponFiscal = vExito
   Exit Function
h_Error:
   fCancelaCuponFiscal = False
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCancelaCuponFiscal", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fImprimirSerialYRollo(ByVal valSerial As String, ByVal valRollo As String) As Boolean
   Dim vProcesoComando As Boolean
   Dim vCmd As String
   On Error GoTo h_Error
   vProcesoComando = False
   If gTexto.DfLen(valSerial) > 0 Then
      vCmd = "@" & valSerial
      vProcesoComando = SendCmd(0, 0, vCmd)
   End If
   If gTexto.DfLen(valRollo) > 0 Then
      vCmd = "@" & valRollo
      vProcesoComando = SendCmd(0, 0, vCmd)
   End If
h_EXIT:    On Error GoTo 0
   fImprimirSerialYRollo = vProcesoComando
   Exit Function
h_Error:
   fImprimirSerialYRollo = False
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fImprimirSerialYRollo", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fDarFormatoADescuentoParaImpresion(ByVal valNumero As String) As String
   Dim vValorFinal As String
   Dim vParteEntera As String
   Dim vParteDecimal As String
   vValorFinal = valNumero
   If gConvert.getSystemDefaultThousandSetting = "," Then
      vParteEntera = gTexto.fPrimerTokenYLoEliminaDelTexto(vValorFinal, ",")
      vParteDecimal = vValorFinal
      If gTexto.fS1SeEncuentraEnS2(".", vParteEntera) Then
         vValorFinal = valNumero
         vParteEntera = gTexto.fPrimerTokenYLoEliminaDelTexto(vValorFinal, ".")
         vParteDecimal = vValorFinal
         If (gTexto.DfLen(vParteEntera) < 2) Then vParteEntera = "0" + vParteEntera
         If (gTexto.DfLen(vParteDecimal) < 2) Then vParteDecimal = vParteDecimal + "0"
      End If
      If (gTexto.DfLen(vParteDecimal) < 2) Then vParteDecimal = vParteDecimal + "0"
      If (gTexto.DfLen(vParteDecimal) < 2) Then vParteDecimal = vParteDecimal + "0"
      If (gTexto.DfLen(vParteEntera) < 2) Then vParteEntera = "0" + vParteEntera
      If (gTexto.DfLen(vParteEntera) < 2) Then vParteEntera = "0" + vParteEntera
      vValorFinal = vParteEntera + vParteDecimal
      
   Else
      vParteEntera = gTexto.fPrimerTokenYLoEliminaDelTexto(vValorFinal, ",")
      vParteDecimal = vValorFinal
      If gTexto.fS1SeEncuentraEnS2(".", vParteEntera) Then
         vValorFinal = valNumero
         vParteEntera = gTexto.fPrimerTokenYLoEliminaDelTexto(vValorFinal, ".")
         vParteDecimal = vValorFinal
         If (gTexto.DfLen(vParteEntera) < 2) Then vParteEntera = "0" + vParteEntera
         If (gTexto.DfLen(vParteDecimal) < 2) Then vParteDecimal = vParteDecimal + "0"
      End If
      
      If (gTexto.DfLen(vParteDecimal) < 2) Then vParteDecimal = vParteDecimal + "0"
      If (gTexto.DfLen(vParteDecimal) < 2) Then vParteDecimal = vParteDecimal + "0"
      If (gTexto.DfLen(vParteEntera) < 2) Then vParteEntera = "0" + vParteEntera
      If (gTexto.DfLen(vParteEntera) < 2) Then vParteEntera = "0" + vParteEntera
      vValorFinal = vParteEntera + vParteDecimal
   End If
   'If (Not gTexto.fS1SeEncuentraEnS2(",", vValorFinal)) Then vValorFinal = vValorFinal + "00"
   fDarFormatoADescuentoParaImpresion = vValorFinal
End Function

Private Function fDarFormatoAPrecioParaImpresion(ByVal valNumero As String) As String
   Dim vValorFinal As String
   Dim vParteEntera As String
   Dim vParteDecimal As String
   vValorFinal = valNumero
   If gConvert.getSystemDefaultThousandSetting = "," Then
      vParteEntera = gTexto.fPrimerTokenYLoEliminaDelTexto(vValorFinal, ",")
      vParteDecimal = vValorFinal
      If gTexto.fS1SeEncuentraEnS2(".", vParteEntera) Then
         vValorFinal = valNumero
         vParteEntera = gTexto.fPrimerTokenYLoEliminaDelTexto(vValorFinal, ".")
         vParteDecimal = vValorFinal
      End If
      If (gTexto.DfLen(vParteDecimal) < 2) Then vParteDecimal = vParteDecimal + "0"
      If (gTexto.DfLen(vParteDecimal) < 2) Then vParteDecimal = vParteDecimal + "0"
      If (gTexto.DfLen(vParteEntera) < 2) Then vParteEntera = "0" + vParteEntera
      If (gTexto.DfLen(vParteEntera) < 2) Then vParteEntera = "0" + vParteEntera
      
      vValorFinal = vParteEntera + vParteDecimal
      vValorFinal = gTexto.DfRight(FORMATO_PRECIO & vValorFinal, 10)
   Else
      vParteEntera = gTexto.fPrimerTokenYLoEliminaDelTexto(vValorFinal, ",")
      vParteDecimal = vValorFinal
      If gTexto.fS1SeEncuentraEnS2(".", vParteEntera) Then
         vValorFinal = valNumero
         vParteEntera = gTexto.fPrimerTokenYLoEliminaDelTexto(vValorFinal, ".")
         vParteDecimal = vValorFinal
         If (gTexto.DfLen(vParteEntera) < 2) Then vParteEntera = "0" + vParteEntera
         If (gTexto.DfLen(vParteDecimal) < 2) Then vParteDecimal = vParteDecimal + "0"
      End If
      
      If (gTexto.DfLen(vParteDecimal) < 2) Then vParteDecimal = vParteDecimal + "0"
      If (gTexto.DfLen(vParteDecimal) < 2) Then vParteDecimal = vParteDecimal + "0"
      If (gTexto.DfLen(vParteEntera) < 2) Then vParteEntera = "0" + vParteEntera
      If (gTexto.DfLen(vParteEntera) < 2) Then vParteEntera = "0" + vParteEntera
      
      vValorFinal = vParteEntera + vParteDecimal
      vValorFinal = gTexto.DfRight(FORMATO_PRECIO & vValorFinal, 10)
      
   End If
   'If (Not gTexto.fS1SeEncuentraEnS2(",", vValorFinal)) Then vValorFinal = vValorFinal + "00"
   fDarFormatoAPrecioParaImpresion = vValorFinal
End Function

Public Sub sImprimeNotaDeCredito(ByVal valEnumPrinter As Enum_ImpresorasFiscales, ByVal valPuerto As String, insDatosImprFiscal As clsDatosImprFiscal, ByRef refReady As Boolean)
  Dim rsDatosArticulo As ADODB.Recordset
      Dim Alicuotas As String
      Dim Cantidades As String
      Dim Precios As String
      Dim Salida As String
      Dim Descripcion As String
      Dim sql  As String
      Dim Tipo  As String
      Dim Cmd As String
      Dim Fecha As String
      Dim Hora As String
      Dim Dia As String
      Dim Mes As String
      Dim Ano As String
      Dim vComandoProcesado As Boolean
      Dim PorcetajeDesRenglon As Currency
      Dim PorcetajeDesTotal As Currency
      On Error GoTo h_Error
      If Not fGetVersionDLLEnSystem32 Then
         refReady = False
         GoTo h_EXIT
      End If
      If sAbreCF(valPuerto, valEnumPrinter) Then
         Cmd = "i01# de Factura:" & insDatosImprFiscal.GetNumeroTicket
         vComandoProcesado = SendCmd(0, 0, Cmd)
      If Not insDatosImprFiscal.GetNumeroRIFCliente = "" Then
         Cmd = "i02Nombre: " & insDatosImprFiscal.GetNombreCliente
            vComandoProcesado = SendCmd(0, 0, Cmd)
         Cmd = "i03Cédula/RIF:" & insDatosImprFiscal.GetNumeroRIFCliente
            vComandoProcesado = SendCmd(0, 0, Cmd)
         Cmd = "i04Teléfono: " & insDatosImprFiscal.GetTelefono
            vComandoProcesado = SendCmd(0, 0, Cmd)
         Cmd = "i05Dirección: " & insDatosImprFiscal.GetDireccion
            vComandoProcesado = SendCmd(0, 0, Cmd)
      End If
         sImprimeCamposDefinibles insDatosImprFiscal
         Hora = insDatosImprFiscal.GetHoraModificacion
         Dia = gConvert.fConvierteAString(Day(insDatosImprFiscal.GetFechaFacturaAfectada))
         Mes = gConvert.fConvierteAString(Month(insDatosImprFiscal.GetFechaFacturaAfectada))
         Ano = gConvert.fConvierteAString(Year(insDatosImprFiscal.GetFechaFacturaAfectada))
         Fecha = Dia & "-" & Mes & "-" & Ano
         
         Cmd = "i06Serial Maquina: " & insDatosImprFiscal.GetSerialImpresoraFiscal
         vComandoProcesado = SendCmd(0, 0, Cmd)
         Cmd = "i07" + "Fecha: " + Fecha + "  Hora: " + Hora
         vComandoProcesado = SendCmd(0, 0, Cmd)
         PorcetajeDesTotal = insDatosImprFiscal.GetPorcentajeDeDescuento * 100
         Set rsDatosArticulo = insDatosImprFiscal.GetItems
         If Not rsDatosArticulo.BOF And Not rsDatosArticulo.EOF Then
            rsDatosArticulo.MoveFirst
            Do While Not rsDatosArticulo.EOF
               Alicuotas = rsDatosArticulo.Fields("AlicuotaIVA").Value
               Cantidades = rsDatosArticulo.Fields("Cantidad").Value
               Precios = rsDatosArticulo.Fields("PrecioSinIVA").Value
               
               Descripcion = rsDatosArticulo.Fields("Descripcion").Value
               Tipo = rsDatosArticulo.Fields("TipoAlicuota").Value
               gTexto.LimpiaStringDeChar Cantidades, "-"
               gTexto.LimpiaStringDeChar Precios, "-"
               PorcetajeDesRenglon = rsDatosArticulo.Fields("Descuento").Value
               
               Dim vPorcentajeDescuentoFormateado As String
               vPorcentajeDescuentoFormateado = rsDatosArticulo.Fields("Descuento").Value
               vPorcentajeDescuentoFormateado = fDarFormatoADescuentoParaImpresion(vPorcentajeDescuentoFormateado)
               
               Dim vPrecioFormateado As String
               vPrecioFormateado = fDarFormatoAPrecioParaImpresion(Precios)
               
               Dim vCantidadFormateada As String
               vCantidadFormateada = fDarFormatoACantidadParaImpresion(Cantidades)
               
               If Not fVenta(Descripcion, vCantidadFormateada, vPrecioFormateado, Alicuotas, Tipo, vPorcentajeDescuentoFormateado, True, valEnumPrinter) Then
                  refReady = False
                  sCierraCF False
                  GoTo h_EXIT
               End If
              rsDatosArticulo.MoveNext
            Loop
            If PorcetajeDesTotal > 0 Then
               Cmd = "3"
               vComandoProcesado = SendCmd(0, 0, Cmd)
               vPorcentajeDescuentoFormateado = insDatosImprFiscal.GetPorcentajeDeDescuento
               vPorcentajeDescuentoFormateado = fDarFormatoADescuentoParaImpresion(vPorcentajeDescuentoFormateado)
               Cmd = "p" & "-" & vPorcentajeDescuentoFormateado
               vComandoProcesado = SendCmd(0, 0, Cmd)
            End If
         End If
         Cmd = "f01000000000000"
         mReady = SendCmd(0, 0, Cmd)
         'refReady
         Set rsDatosArticulo = Nothing
         sCierraCF True
         sRetardoTiempo 10000
         refReady = vComandoProcesado
      End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_Error: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sImprimeNotaDeCredito", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Function fDarFormatoACantidadParaImpresion(ByVal valNumero As String) As String
   Dim vValorFinal As String
   Dim vParteEntera As String
   Dim vParteDecimal As String
   vValorFinal = valNumero
   If gConvert.getSystemDefaultThousandSetting = "," Then
   
      vParteEntera = gTexto.fPrimerTokenYLoEliminaDelTexto(vValorFinal, ",")
      vParteDecimal = vValorFinal
      If gTexto.fS1SeEncuentraEnS2(".", vParteEntera) Then
         vValorFinal = valNumero
         vParteEntera = gTexto.fPrimerTokenYLoEliminaDelTexto(vValorFinal, ".")
         vParteDecimal = vValorFinal
      End If
      If (gTexto.DfLen(vParteDecimal) < 3) Then vParteDecimal = vParteDecimal + "0"
      If (gTexto.DfLen(vParteDecimal) < 3) Then vParteDecimal = vParteDecimal + "0"
      If (gTexto.DfLen(vParteDecimal) < 3) Then vParteDecimal = vParteDecimal + "0"
      If (gTexto.DfLen(vParteEntera) < 2) Then vParteEntera = "0" + vParteEntera
      If (gTexto.DfLen(vParteEntera) < 2) Then vParteEntera = "0" + vParteEntera
      
      vValorFinal = vParteEntera + vParteDecimal
      vValorFinal = gTexto.DfRight(FORMATO_CANTIDAD & vValorFinal, 8)
   Else
      vParteEntera = gTexto.fPrimerTokenYLoEliminaDelTexto(vValorFinal, ",")
      vParteDecimal = vValorFinal
      If gTexto.fS1SeEncuentraEnS2(".", vParteEntera) Then
         vValorFinal = valNumero
         vParteEntera = gTexto.fPrimerTokenYLoEliminaDelTexto(vValorFinal, ".")
         vParteDecimal = vValorFinal
      End If
      If (gTexto.DfLen(vParteDecimal) < 3) Then vParteDecimal = vParteDecimal + "0"
      If (gTexto.DfLen(vParteDecimal) < 3) Then vParteDecimal = vParteDecimal + "0"
      If (gTexto.DfLen(vParteDecimal) < 3) Then vParteDecimal = vParteDecimal + "0"
      If (gTexto.DfLen(vParteEntera) < 2) Then vParteEntera = "0" + vParteEntera
      If (gTexto.DfLen(vParteEntera) < 2) Then vParteEntera = "0" + vParteEntera
      
      vValorFinal = vParteEntera + vParteDecimal
      vValorFinal = gTexto.DfRight(FORMATO_CANTIDAD & vValorFinal, 8)
   End If
   'If (Not gTexto.fS1SeEncuentraEnS2(",", vValorFinal)) Then vValorFinal = vValorFinal + "00"
   fDarFormatoACantidadParaImpresion = vValorFinal
End Function

Private Sub sImprimeDatosClientesDasconT(ByVal valEnumPrinter As Enum_ImpresorasFiscales, ByVal insDatosImprFiscal As clsDatosImprFiscal)
   Dim Texto As String
   Dim Cmd As String
   Dim mbRet As Boolean
     If Not insDatosImprFiscal.GetNombreCliente = "" Then
         Texto = gTexto.DfMid(insDatosImprFiscal.GetNumeroRIFCliente, 1, 11)
         Cmd = "iR*" & Texto
         mbRet = SendCmd(0, 0, Cmd)
         Texto = gTexto.DfMid(insDatosImprFiscal.GetNombreCliente, 1, 40)
         Cmd = "iS*" & Texto
         mbRet = SendCmd(0, 0, Cmd)
         Texto = gTexto.DfMid(insDatosImprFiscal.GetTelefono, 1, 20)
         Cmd = "i00" & "Telf: " & Texto
         mbRet = SendCmd(0, 0, Cmd)
         Texto = gTexto.DfMid(insDatosImprFiscal.GetDireccion, 1, 29)
         Cmd = "i01" & "Direccion: " & Texto
         mbRet = SendCmd(0, 0, Cmd)
         Texto = gTexto.DfMid(insDatosImprFiscal.GetDireccion, 30, 40)
         Cmd = "i02" & Texto
         mbRet = SendCmd(0, 0, Cmd)
        If insDatosImprFiscal.GetObservaciones <> "" Then
            Texto = gTexto.DfMid(insDatosImprFiscal.GetObservaciones, 1, 25)
            Cmd = "i03Observaciones: " & Texto
               mbRet = SendCmd(0, 0, Cmd)
            Texto = gTexto.DfMid(insDatosImprFiscal.GetObservaciones, 26, 40)
            Cmd = "i04" & Texto
            mbRet = SendCmd(0, 0, Cmd)
        Else
         Texto = gTexto.DfMid(insDatosImprFiscal.GetDireccion, 70, 40)
         Cmd = "i03" & Texto
         mbRet = SendCmd(0, 0, Cmd)
        End If
     ElseIf insDatosImprFiscal.GetObservaciones <> "" Then
         Texto = gTexto.DfMid(insDatosImprFiscal.GetObservaciones, 1, 30)
         Cmd = "i01Observaciones: " & Texto
            mbRet = SendCmd(0, 0, Cmd)
         Texto = gTexto.DfMid(insDatosImprFiscal.GetObservaciones, 31, 30)
         Cmd = "i02          " & Texto
            mbRet = SendCmd(0, 0, Cmd)
     End If
End Sub

Public Sub sImprimeNotaDeCreditoDascomT(ByVal valEnumPrinter As Enum_ImpresorasFiscales, ByVal valPuerto As String, insDatosImprFiscal As clsDatosImprFiscal, ByRef refReady As Boolean)
      Dim rsDatosArticulo As ADODB.Recordset
      Dim Alicuotas As String
      Dim Cantidades As String
      Dim Precios As String
      Dim Salida As String
      Dim Descripcion As String
      Dim sql  As String
      Dim Tipo  As String
      Dim Cmd As String
      Dim Fecha As String
      Dim Hora As String
      Dim Dia As String
      Dim Mes As String
      Dim Ano As String
      Dim Direccion As String
      Dim vComandoProcesado As Boolean
      Dim PorcetajeDesRenglon As Currency
      Dim PorcetajeDesTotal As Currency
      On Error GoTo h_Error
      If Not fGetVersionDLLEnSystem32 Then
         refReady = False
         GoTo h_EXIT
      End If
      If sAbreCF(valPuerto, valEnumPrinter) Then
        Cmd = "iR*" & insDatosImprFiscal.GetNumeroRIFCliente
        vComandoProcesado = SendCmd(0, 0, Cmd)
        Cmd = "iS*" & insDatosImprFiscal.GetNombreCliente
        vComandoProcesado = SendCmd(0, 0, Cmd)
        Cmd = "iF*" & insDatosImprFiscal.GetNumeroTicket
        vComandoProcesado = SendCmd(0, 0, Cmd)
        Cmd = "iI*" & insDatosImprFiscal.GetSerialImpresoraFiscal
        vComandoProcesado = SendCmd(0, 0, Cmd)
        Hora = insDatosImprFiscal.GetHoraModificacion
        Dia = gConvert.fConvierteAString(Day(insDatosImprFiscal.GetFechaFacturaAfectada))
        Mes = gConvert.fConvierteAString(Month(insDatosImprFiscal.GetFechaFacturaAfectada))
        Ano = gConvert.fConvierteAString(Year(insDatosImprFiscal.GetFechaFacturaAfectada))
        Fecha = Dia & "-" & Mes & "-" & Ano
        Cmd = "iD*" + Fecha + " HORA:" + Hora
        vComandoProcesado = SendCmd(0, 0, Cmd)
        Cmd = "i00Teléfono: " & insDatosImprFiscal.GetTelefono
        vComandoProcesado = SendCmd(0, 0, Cmd)
        Direccion = insDatosImprFiscal.GetDireccion
        Cmd = "i01Dirección: " & gTexto.DfMid(Direccion, 1, 31)
        vComandoProcesado = SendCmd(0, 0, Cmd)
        Cmd = "i02" & gTexto.DfMid(Direccion, 32, 40)
        vComandoProcesado = SendCmd(0, 0, Cmd)
        Cmd = "i03" & gTexto.DfMid(Direccion, 72, 40)
        vComandoProcesado = SendCmd(0, 0, Cmd)
        PorcetajeDesTotal = insDatosImprFiscal.GetPorcentajeDeDescuento * 100
         Set rsDatosArticulo = insDatosImprFiscal.GetItems
         If Not rsDatosArticulo.BOF And Not rsDatosArticulo.EOF Then
            rsDatosArticulo.MoveFirst
            Do While Not rsDatosArticulo.EOF
               Alicuotas = rsDatosArticulo.Fields("AlicuotaIVA").Value
               Cantidades = rsDatosArticulo.Fields("Cantidad").Value
               Precios = rsDatosArticulo.Fields("PrecioSinIVA").Value
               
               Descripcion = rsDatosArticulo.Fields("Descripcion").Value
               Tipo = rsDatosArticulo.Fields("TipoAlicuota").Value
               gTexto.LimpiaStringDeChar Cantidades, "-"
               gTexto.LimpiaStringDeChar Precios, "-"
               PorcetajeDesRenglon = rsDatosArticulo.Fields("Descuento").Value
               
               Dim vPorcentajeDescuentoFormateado As String
               vPorcentajeDescuentoFormateado = rsDatosArticulo.Fields("Descuento").Value
               vPorcentajeDescuentoFormateado = fDarFormatoADescuentoParaImpresion(vPorcentajeDescuentoFormateado)
               
               Dim vPrecioFormateado As String
               vPrecioFormateado = fDarFormatoAPrecioParaImpresion(Precios)
               
               Dim vCantidadFormateada As String
               vCantidadFormateada = fDarFormatoACantidadParaImpresion(Cantidades)
               
               If Not fVenta(Descripcion, vCantidadFormateada, vPrecioFormateado, Alicuotas, Tipo, vPorcentajeDescuentoFormateado, True, valEnumPrinter) Then
                  refReady = False
                  sCierraCF False
                  GoTo h_EXIT
               End If
              rsDatosArticulo.MoveNext
            Loop
            If PorcetajeDesTotal > 0 Then
               Cmd = "3"
               vComandoProcesado = SendCmd(0, 0, Cmd)
               vPorcentajeDescuentoFormateado = insDatosImprFiscal.GetPorcentajeDeDescuento
               vPorcentajeDescuentoFormateado = fDarFormatoADescuentoParaImpresion(vPorcentajeDescuentoFormateado)
               Cmd = "p" & "-" & vPorcentajeDescuentoFormateado
               vComandoProcesado = SendCmd(0, 0, Cmd)
            End If
         End If
         Cmd = "101"
         mReady = SendCmd(0, 0, Cmd)
          'refReady
         Set rsDatosArticulo = Nothing
         sCierraCF True
         sRetardoTiempo 10000
         refReady = vComandoProcesado
      End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_Error: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sImprimeNotaDeCredito", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Sub sImprimeNotaDeCreditoHKA112(ByVal valEnumPrinter As Enum_ImpresorasFiscales, ByVal valPuerto As String, insDatosImprFiscal As clsDatosImprFiscal, ByRef refReady As Boolean)
      Dim rsDatosArticulo As ADODB.Recordset
      Dim Alicuotas As String
      Dim Cantidades As String
      Dim Precios As String
      Dim Salida As String
      Dim Descripcion As String
      Dim Tipo  As String
      Dim Cmd As String
      Dim Fecha As String
      Dim Hora As String
      Dim Dia As String
      Dim Mes As String
      Dim Ano As String
      Dim Direccion As String
      Dim vComandoProcesado As Boolean
      Dim PorcetajeDesRenglon As Currency
      Dim PorcetajeDesTotal As Currency
      On Error GoTo h_Error
      If Not fGetVersionDLLEnSystem32 Then
         refReady = False
         GoTo h_EXIT
      End If
      If sAbreCF(valPuerto, valEnumPrinter) Then
        Cmd = "iR*" & insDatosImprFiscal.GetNumeroRIFCliente
        vComandoProcesado = SendCmd(0, 0, Cmd)
        Cmd = "iS*" & insDatosImprFiscal.GetNombreCliente
        vComandoProcesado = SendCmd(0, 0, Cmd)
        Cmd = "i00" & insDatosImprFiscal.GetNumeroTicket
        vComandoProcesado = SendCmd(0, 0, Cmd)
        Cmd = "i01" & insDatosImprFiscal.GetSerialImpresoraFiscal
        vComandoProcesado = SendCmd(0, 0, Cmd)
        Hora = insDatosImprFiscal.GetHoraModificacion
        Dia = gConvert.fConvierteAString(Day(insDatosImprFiscal.GetFechaFacturaAfectada))
        Mes = gConvert.fConvierteAString(Month(insDatosImprFiscal.GetFechaFacturaAfectada))
        Ano = gConvert.fConvierteAString(Year(insDatosImprFiscal.GetFechaFacturaAfectada))
        Fecha = Dia & "-" & Mes & "-" & Ano
        Cmd = "i02" + Fecha + " HORA:" + Hora
        vComandoProcesado = SendCmd(0, 0, Cmd)
        Cmd = "i03Teléfono: " & insDatosImprFiscal.GetTelefono
        vComandoProcesado = SendCmd(0, 0, Cmd)
        Direccion = insDatosImprFiscal.GetDireccion
        Cmd = "i04Dirección: " & gTexto.DfMid(Direccion, 1, 31)
        vComandoProcesado = SendCmd(0, 0, Cmd)
        Cmd = "i05" & gTexto.DfMid(Direccion, 32, 40)
        vComandoProcesado = SendCmd(0, 0, Cmd)
        Cmd = "i06" & gTexto.DfMid(Direccion, 72, 40)
        vComandoProcesado = SendCmd(0, 0, Cmd)
        PorcetajeDesTotal = insDatosImprFiscal.GetPorcentajeDeDescuento * 100
        Set rsDatosArticulo = insDatosImprFiscal.GetItems
        If Not rsDatosArticulo.BOF And Not rsDatosArticulo.EOF Then
            rsDatosArticulo.MoveFirst
            Do While Not rsDatosArticulo.EOF
               Alicuotas = rsDatosArticulo.Fields("AlicuotaIVA").Value
               Cantidades = rsDatosArticulo.Fields("Cantidad").Value
               Precios = rsDatosArticulo.Fields("PrecioSinIVA").Value
               
               Descripcion = rsDatosArticulo.Fields("Descripcion").Value
               Tipo = rsDatosArticulo.Fields("TipoAlicuota").Value
               gTexto.LimpiaStringDeChar Cantidades, "-"
               gTexto.LimpiaStringDeChar Precios, "-"
               PorcetajeDesRenglon = rsDatosArticulo.Fields("Descuento").Value
               
               Dim vPorcentajeDescuentoFormateado As String
               vPorcentajeDescuentoFormateado = rsDatosArticulo.Fields("Descuento").Value
               vPorcentajeDescuentoFormateado = fDarFormatoADescuentoParaImpresion(vPorcentajeDescuentoFormateado)
               
               Dim vPrecioFormateado As String
               vPrecioFormateado = fDarFormatoAPrecioParaImpresion(Precios)
               
               Dim vCantidadFormateada As String
               vCantidadFormateada = fDarFormatoACantidadParaImpresion(Cantidades)
               
               If Not fVenta(Descripcion, vCantidadFormateada, vPrecioFormateado, Alicuotas, Tipo, vPorcentajeDescuentoFormateado, True, valEnumPrinter) Then
                  refReady = False
                  sCierraCF False
                  GoTo h_EXIT
               End If
              rsDatosArticulo.MoveNext
            Loop
            If PorcetajeDesTotal > 0 Then
               Cmd = "3"
               vComandoProcesado = SendCmd(0, 0, Cmd)
               vPorcentajeDescuentoFormateado = insDatosImprFiscal.GetPorcentajeDeDescuento
               vPorcentajeDescuentoFormateado = fDarFormatoADescuentoParaImpresion(vPorcentajeDescuentoFormateado)
               Cmd = "p" & "-" & vPorcentajeDescuentoFormateado
               vComandoProcesado = SendCmd(0, 0, Cmd)
            End If
         End If
         Cmd = "101"
         mReady = SendCmd(0, 0, Cmd)
         Set rsDatosArticulo = Nothing
         sCierraCF True
         sRetardoTiempo 10000
         refReady = vComandoProcesado
      End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_Error: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sImprimeNotaDeCreditoHKA112", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
