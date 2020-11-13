VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsUtilImpFiscalBMC"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = False
Attribute VB_Ext_KEY = "SavedWithClassBuilder6" ,"Yes"
Attribute VB_Ext_KEY = "Top_Level" ,"Yes"
Option Explicit

'//////////////////////////////////////////////////////////
'// Fiscal Printer library definitions
'//////////////////////////////////////////////////////////
Private Declare Function OpenFpctrl Lib "FPCTRL.DLL" (ByVal lpPortName As String) As Long
Private Declare Function CloseFpctrl Lib "FPCTRL.DLL" () As Long
Private Declare Function CheckFprinter Lib "FPCTRL.DLL" () As Boolean
Private Declare Function ReadFpStatus Lib "FPCTRL.DLL" (ByRef Status As Long, ByRef error As Long) As Long
Private Declare Function SendCmd Lib "FPCTRL.DLL" (ByRef Status As Long, ByRef error As Long, ByVal Cmd As String) As Long
Private Declare Function SendNCmd Lib "FPCTRL.DLL" (ByRef Status As Long, ByRef error As Long, ByVal buffer As String) As Long
Private Declare Function SendFileCmd Lib "FPCTRL.DLL" (ByRef Status As Long, ByRef error As Long, ByVal file As String) As Long
Private Declare Function UploadReportCmd Lib "FPCTRL.DLL" (ByRef Status As Long, ByRef error As Long, ByVal Cmd As String, ByVal file As String) As Long
Private Declare Function UploadStatusCmd Lib "FPCTRL.DLL" (ByRef Status As Long, ByRef error As Long, ByVal Cmd As String, ByVal respuesta As String) As Long
Private Declare Function Sleep Lib "kernel32.dll" (ByVal mSeg As Integer) As Long

'//status
Private Const FPCTL_NON_STATUS = &H0
Private Const FPCTL_NON_FISCAL_IN_IDLE = &H1
Private Const FPCTL_NON_FISCAL_IN_TRANSACTION = &H2
Private Const FPCTL_NON_FISCAL_NON_TRANSACTION = &H3
Private Const FPCTL_FISCAL_IN_IDLE = &H4
Private Const FPCTL_FISCAL_IN_TRANSACTION = &H5
Private Const FPCTL_FISCAL_NON_TRANSACTION = &H6
Private Const FPCTL_FISCAL_MEMORY_NEAR_FULL_IN_TRANSACTION = &H7
Private Const FPCTL_FISCAL_MEMORY_NEAR_FULL_NON_TRANSACTION = &H8
Private Const FPCTL_FISCAL_MEMORY_NEAR_FULL_IN_IDLE = &H9
Private Const FPCTL_FISCAL_MEMORY_FULL_IN_TRANSACTION = &HA
Private Const FPCTL_FISCAL_MEMORY_FULL_IN_NON_TRANSACTION = &HB
Private Const FPCTL_FISCAL_MEMORY_FULL_IN_IDLE = &HC
Private Const FPCTL_SENDNCMD_SUCCESS = &H0

'// error
Private Const FPCTL_ERROR_NO_ERROR = &H0
Private Const FPCTL_ERROR_PAPER_END = &H1
Private Const FPCTL_ERROR_PRINTER_MECHA_ERROR = &H2
Private Const FPCTL_ERROR_PAPER_END_MECHA_ERROR = &H3
Private Const FPCTL_ERROR_COMMAND_INVALID_VAL = &H50
Private Const FPCTL_ERROR_COMMAND_INVALID_TAX = &H54
Private Const FPCTL_ERROR_NOT_ASSIGNED_CLERK = &H58
Private Const FPCTL_ERROR_INVALID_COMMAND = &H5C
Private Const FPCTL_ERROR_FISCAL_ERROR = &H60
Private Const FPCTL_ERROR_FISCAL_MEMORY_ERROR = &H64
Private Const FPCTL_ERROR_FISCAL_MEMORY_FULL = &H6C

Private Const FPCTL_ERROR_BUFFER_FULL = &H70
Private Const FPCTL_ERROR_ANSWERED_NAK = &H80
Private Const FPCTL_ERROR_NOT_ANSWERED = &H89
Private Const FPCTL_ERROR_UNKNOWN_ANSWERED = &H90
Private Const FPCTL_ERROR_COMM_API = &H91
Private Const FPCTL_ERROR_FILE_OPEN = &H99

Const mDecimalesParaCantidad As Byte = 3
Const mDecimalesParaMonto As Byte = 2
Const mDecimalesParaDescuento As Byte = 2
Const mEnterosParaMonto As Byte = 8
Const mEnterosParaCantidad As Byte = 5
Const mEnterosParaDescuento As Byte = 2
Const mEnterosParaMontosDePagos As Byte = 10

Private Const PUERTO As String = "COM1"
Private Const MAX_LEN_CADENA As Integer = 40
'Importante Impresora debe tener el flag FLG(3)=26 (Modo Extendido)

Private Function CM_FILE_NAME() As String
   CM_FILE_NAME = "clsUtilImprFiscal"
End Function

Private Function CM_MESSAGE_NAME() As String
   CM_MESSAGE_NAME = "Util Impresora Fiscal BMC"
End Function

Private Function GetGender() As Enum_Gender
   GetGender = eg_Female
End Function

Public Function fSerialMemoriaFiscal_BMC(ByRef refReady As Boolean, valPuerto As String, ByVal valImpresoraFiscal As Enum_ImpresorasFiscales) As String
   Dim vCommand As String
   Dim vExito As Boolean
   Dim vMessage As String
   Dim vStatus As Long
   Dim vSerial As String
   Dim vError As Long
   Dim lport As Long
   Dim vTemporal As String
   On Error GoTo h_Error
   vSerial = ""
   If BMCOpenPort(valPuerto) Then
      vCommand = "S1"
      vMessage = Space(255)
      vExito = UploadStatusCmd(vStatus, vError, vCommand, vMessage)
      sHandleStatusAPi vStatus
      If vExito Then
         vTemporal = gTexto.fPrimerTokenYLoEliminaDelTexto(vMessage, Chr(10))
         vTemporal = gTexto.fPrimerTokenYLoEliminaDelTexto(vMessage, Chr(10))
         vTemporal = gTexto.fPrimerTokenYLoEliminaDelTexto(vMessage, Chr(10))
         vTemporal = gTexto.fPrimerTokenYLoEliminaDelTexto(vMessage, Chr(10))
         vTemporal = gTexto.fPrimerTokenYLoEliminaDelTexto(vMessage, Chr(10))
         vTemporal = gTexto.fPrimerTokenYLoEliminaDelTexto(vMessage, Chr(10))
         vTemporal = gTexto.fPrimerTokenYLoEliminaDelTexto(vMessage, Chr(10))
         vTemporal = gTexto.fPrimerTokenYLoEliminaDelTexto(vMessage, Chr(10))
         vTemporal = gTexto.fPrimerTokenYLoEliminaDelTexto(vMessage, Chr(10))
         vTemporal = gTexto.fPrimerTokenYLoEliminaDelTexto(vMessage, Chr(10))
         vTemporal = gTexto.fPrimerTokenYLoEliminaDelTexto(vMessage, Chr(10))
         vSerial = gTexto.fPrimerTokenYLoEliminaDelTexto(vMessage, Chr(10)) ' Serial
      Else
         sHandleErrorAPi vError
      End If
      BMCClosePort
   Else
      vExito = False
      gMessage.Advertencia "No se puede establecer comunicación con la impresora." & vbCrLf & "1.- Por favor salga del Sistema." & vbCrLf & "2.- Apague la impresora y enciéndala de nuevo." & vbCrLf & "3.-  Verifique todas las conexiones desde el PC a l Impresora. " & vbCrLf & "4.- Vuelva a Intentar la operación."
   End If
h_EXIT: On Error GoTo 0
   fSerialMemoriaFiscal_BMC = vSerial
   refReady = vExito
   Exit Function
h_Error:
   refReady = False
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSerialMemoriaFiscal_BMC", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Sub sHandleErrorAPi(ByVal valError As Long)
   Dim vMessage As String
   On Error GoTo h_Error
   vMessage = ""
   Select Case valError
      Case FPCTL_ERROR_PAPER_END: vMessage = "Fin de Papel."
      Case FPCTL_ERROR_PRINTER_MECHA_ERROR: vMessage = "Caebezal de la impresora fiscal presenta error."
      Case FPCTL_ERROR_PAPER_END_MECHA_ERROR: vMessage = "Cabezal de la impresora fiscal presenta error."
      Case FPCTL_ERROR_COMMAND_INVALID_VAL: vMessage = "Parámetros del Comando Inválidos."
      Case FPCTL_ERROR_COMMAND_INVALID_TAX: vMessage = "Taza de Impuesto escogida para imprimir es inválida."
      Case FPCTL_ERROR_NOT_ASSIGNED_CLERK: vMessage = "Valor del Clerk no asignado."
      Case FPCTL_ERROR_INVALID_COMMAND: vMessage = "Comando Inválido."
      Case FPCTL_ERROR_FISCAL_ERROR: vMessage = "Error en comprobante Fiscal."
      Case FPCTL_ERROR_FISCAL_MEMORY_ERROR: vMessage = "Error de la memoria fiscal."
      Case FPCTL_ERROR_FISCAL_MEMORY_FULL: vMessage = "Memoria Fiscal de la Impresora esta llena."
      Case FPCTL_ERROR_BUFFER_FULL: vMessage = "Errores de Comunicación, Buffer esta lleno"
      Case FPCTL_ERROR_ANSWERED_NAK: vMessage = "Errores de Comunicación, valor de retorno Inválido"
      Case FPCTL_ERROR_NOT_ANSWERED: vMessage = "Errores de Comunicación, No se obtuvo respuesta de la impresora"
      Case FPCTL_ERROR_UNKNOWN_ANSWERED: vMessage = "Errores de Comunicación, respuesta desconocida"
      Case FPCTL_ERROR_COMM_API: vMessage = "Errores de Comunicación, Error desconocido con la API"
      Case FPCTL_ERROR_FILE_OPEN: vMessage = "Errores de Comunicación, Error al escribir archivo de retorno de la impresora."
   End Select
   If gTexto.DfLen(vMessage) > 0 Then
      gMessage.sCriticalErrorMessage "Se ha presentado un error con la impresora fiscal." & vbCrLf & vMessage, "Status con la impresora Fiscal - BMC"
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_Error:
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sHandleErrorAPi", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sHandleStatusAPi(ByVal valStatus As Long)
   Dim vMessage As String
   On Error GoTo h_Error
   vMessage = ""
   Select Case valStatus
      Case FPCTL_NON_FISCAL_IN_IDLE: vMessage = " No hay comprobante no fiscal en la transacción."
      Case FPCTL_NON_FISCAL_IN_TRANSACTION: vMessage = " No hay comprobante no fiscal en la transacción."
      Case FPCTL_NON_FISCAL_NON_TRANSACTION: vMessage = " No hay comprobante no fiscal y no existen transacciones."
      Case FPCTL_FISCAL_IN_IDLE: vMessage = " No hay comprobante fiscal en la transacción."
      Case FPCTL_FISCAL_IN_TRANSACTION: vMessage = " No hay comprobante fiscal en la transacción."
      Case FPCTL_FISCAL_NON_TRANSACTION: vMessage = " No hay comprobante no fiscal y no existen transacciones."
      Case FPCTL_FISCAL_MEMORY_NEAR_FULL_IN_TRANSACTION: vMessage = " Memoria Fiscal cerca de llenarse."
      Case FPCTL_FISCAL_MEMORY_NEAR_FULL_NON_TRANSACTION: vMessage = " Memoria Fiscal cerca de llenar. No hay transacciones."
      Case FPCTL_FISCAL_MEMORY_NEAR_FULL_IN_IDLE: vMessage = " Memoria Fiscal cerca de llenar. Transacción en curso."
      Case FPCTL_FISCAL_MEMORY_FULL_IN_TRANSACTION: vMessage = " Memoria Fiscal Llena. Transacción en curso."
      Case FPCTL_FISCAL_MEMORY_FULL_IN_NON_TRANSACTION: vMessage = " Memoria Fiscal Llena. No hay transacciones."
      Case FPCTL_FISCAL_MEMORY_FULL_IN_IDLE: vMessage = " Memoria Fiscal Llena. Transacción en curso."
   End Select
   If gTexto.DfLen(vMessage) > 0 Then
      gMessage.sCriticalErrorMessage "La impresora fiscal ha indicado un Status." & vbCrLf & vMessage, "Error con la impresora Fiscal - BMC"
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_Error:
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sHandleErrorAPi", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Sub sRealizarCierreZ_BMC(ByRef refReady As Boolean, valPuerto As String)
   Dim vCommand As String
   Dim vExito As Boolean
   Dim vStatus As Long
   Dim vError As Long
   Dim lport As Long
   Dim lCmdResult As Long
   On Error GoTo h_Error
   vExito = False
   If BMCOpenPort(valPuerto) Then
      vCommand = "I0Z"
      lCmdResult = SendCmd(vStatus, vError, vCommand)
      sHandleStatusAPi vStatus
      sHandleErrorAPi vError
      If lCmdResult = 1 Then
         vExito = True
         BMCClosePort
      End If
   Else
      vExito = False
      gMessage.Advertencia "No se puede establecer comunicación con la impresora." & vbCrLf & "1.- Por favor salga del Sistema." & vbCrLf & "2.- Apague la impresora y enciéndala de nuevo." & vbCrLf & "3.-  Verifique todas las conexiones desde el PC a l Impresora. " & vbCrLf & "4.- Vuelva a Intentar la operación."
   End If
h_EXIT: On Error GoTo 0
    refReady = vExito
   Exit Sub
h_Error: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sRelaizarCierreZ_BMC", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Function fImprimeDatosClientesBMC(ByVal valEnumPrinter As Enum_ImpresorasFiscales, ByVal valDatosImprFiscal As clsDatosImprFiscal, ByRef refReady As Boolean) As Boolean
   Dim Texto As String
   Dim Cmd As String
   Dim vExito As Boolean
   Dim Hora As String
   Dim Dia As String
   Dim Mes As String
   Dim Ano As String
   Dim Fecha As String
   Dim vError As Long
   Dim vStatus As Long
   Dim insUtilImprFiscal As clsUtilImprFiscal
   On Error GoTo h_Error
      Set insUtilImprFiscal = New clsUtilImprFiscal
      If Not valDatosImprFiscal.GetNombreCliente = "" Then
         Texto = insUtilImprFiscal.fCadenaCaracteresValidos(gTexto.DfMid(valDatosImprFiscal.GetNombreCliente, 1, 30), MAX_LEN_CADENA)
         Cmd = "i01Nombre: " & Texto
         vExito = SendCmd(vStatus, vError, Cmd)
         Texto = insUtilImprFiscal.fCadenaCaracteresValidos(gTexto.DfMid(valDatosImprFiscal.GetNombreCliente, 30, 30), MAX_LEN_CADENA)
         Cmd = "i02          " & Texto
         If vExito Then vExito = SendCmd(0, 0, Cmd)
         Cmd = "i03CI/RIF:" & valDatosImprFiscal.GetNumeroRIFCliente & " Tlf: " & valDatosImprFiscal.GetTelefono
         If vExito Then vExito = SendCmd(0, 0, Cmd)
         Texto = insUtilImprFiscal.fCadenaCaracteresValidos(gTexto.DfMid(valDatosImprFiscal.GetDireccion, 1, 29), MAX_LEN_CADENA)
         Cmd = "i04Direccion: " & Texto
         If vExito Then vExito = SendCmd(0, 0, Cmd)
         Texto = insUtilImprFiscal.fCadenaCaracteresValidos(gTexto.DfMid(valDatosImprFiscal.GetDireccion, 30, 40), MAX_LEN_CADENA)
         Cmd = "i05" & Texto
         If vExito Then vExito = SendCmd(0, 0, Cmd)
         If Not valDatosImprFiscal.GetEsNotaDeCredito Then
            If valDatosImprFiscal.GetObservaciones <> "" Then
               Texto = insUtilImprFiscal.fCadenaCaracteresValidos(gTexto.DfMid(valDatosImprFiscal.GetObservaciones, 1, 25), MAX_LEN_CADENA)
               Cmd = "i06Observaciones: " & Texto
               If vExito Then vExito = SendCmd(0, 0, Cmd)
               Texto = insUtilImprFiscal.fCadenaCaracteresValidos(gTexto.DfMid(valDatosImprFiscal.GetObservaciones, 26, 40), MAX_LEN_CADENA)
               Cmd = "i07" & Texto
               If vExito Then vExito = SendCmd(0, 0, Cmd)
               Texto = insUtilImprFiscal.fCadenaCaracteresValidos(gTexto.DfMid(valDatosImprFiscal.GetObservaciones, 66, 40), MAX_LEN_CADENA)
               Cmd = "i08" & Texto
               If vExito Then vExito = SendCmd(0, 0, Cmd)
            End If
         End If
      ElseIf valDatosImprFiscal.GetObservaciones <> "" Then
         Texto = insUtilImprFiscal.fCadenaCaracteresValidos(gTexto.DfMid(valDatosImprFiscal.GetObservaciones, 1, 25), MAX_LEN_CADENA)
         Cmd = "i01Observaciones: " & Texto
         vExito = SendCmd(0, 0, Cmd)
         Texto = insUtilImprFiscal.fCadenaCaracteresValidos(gTexto.DfMid(valDatosImprFiscal.GetObservaciones, 26, 40), MAX_LEN_CADENA)
         Cmd = "i02" & Texto
         If vExito Then vExito = SendCmd(0, 0, Cmd)
         Texto = insUtilImprFiscal.fCadenaCaracteresValidos(gTexto.DfMid(valDatosImprFiscal.GetObservaciones, 66, 40), MAX_LEN_CADENA)
         Cmd = "i03" & Texto
         If vExito Then vExito = SendCmd(0, 0, Cmd)
      Else
         vExito = True
      End If
      If valDatosImprFiscal.GetEsNotaDeCredito Then
         Hora = valDatosImprFiscal.GetHoraModificacion
         Dia = gConvert.fConvierteAString(Day(valDatosImprFiscal.GetFecha))
         Mes = gConvert.fConvierteAString(Month(valDatosImprFiscal.GetFecha))
         Ano = gConvert.fConvierteAString(Year(valDatosImprFiscal.GetFecha))
         Fecha = Dia & "-" & Mes & "-" & Ano
         Cmd = "i06# de Factura:" & valDatosImprFiscal.GetNumeroTicket
         vExito = SendCmd(0, 0, Cmd)
         Cmd = "i07Serial Maquina: " & valDatosImprFiscal.GetSerialImpresoraFiscal
         vExito = SendCmd(0, 0, Cmd)
         Cmd = "i08Fecha: " & Fecha & " Hora: " & Hora
         vExito = SendCmd(0, 0, Cmd)
         If valDatosImprFiscal.GetObservaciones <> "" Then
               Texto = insUtilImprFiscal.fCadenaCaracteresValidos(gTexto.DfMid(valDatosImprFiscal.GetObservaciones, 1, 25), MAX_LEN_CADENA)
               Cmd = "i09Observaciones: " & Texto
               If vExito Then vExito = SendCmd(0, 0, Cmd)
               Texto = insUtilImprFiscal.fCadenaCaracteresValidos(gTexto.DfMid(valDatosImprFiscal.GetObservaciones, 26, 40), MAX_LEN_CADENA)
               Cmd = "i10" & Texto
               If vExito Then vExito = SendCmd(0, 0, Cmd)
               Texto = insUtilImprFiscal.fCadenaCaracteresValidos(gTexto.DfMid(valDatosImprFiscal.GetObservaciones, 66, 40), MAX_LEN_CADENA)
               Cmd = "i11" & Texto
               If vExito Then vExito = SendCmd(0, 0, Cmd)
            End If
      End If
h_EXIT: On Error GoTo 0
   refReady = vExito
   fImprimeDatosClientesBMC = vExito
   Exit Function
h_Error:
 refReady = False
Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fImprimeDatosClientesBMC", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fCancelaCuponFiscal_BMC(ByVal valAbrirElPuerto As Boolean, valPuerto As String) As String
   Dim vCommand As String
   Dim vExito As Boolean
   Dim vStatus As Long
   Dim vSerial As String
   Dim vError As Long
   Dim lport As Long
   On Error GoTo h_Error
   '////////////////////////////////////////////
   '    Transaction Cancel
   '////////////////////////////////////////////
   If valAbrirElPuerto Then
      BMCOpenPort (valPuerto)
   Else
      lport = 1
   End If
   vCommand = "7" ' Comando que cancela la transacción
   vExito = SendCmd(vStatus, vError, vCommand)
   sHandleStatusAPi vStatus
   If Not vExito Then
      sHandleErrorAPi vError
      BMCClosePort
   End If
   If valAbrirElPuerto Then
     BMCClosePort
   End If
h_EXIT: On Error GoTo 0
   fCancelaCuponFiscal_BMC = vExito
   Exit Function
h_Error:
   fCancelaCuponFiscal_BMC = False
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCancelaCuponFiscal_BMC", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Sub sImprimeVentaArticulo_BMC(ByVal valEnumPrinter As Enum_ImpresorasFiscales, ByVal valDatosImprFiscal As clsDatosImprFiscal, ByRef refReady As Boolean, valPuerto As String)
      Dim rsDatosArticulo As ADODB.Recordset
      Dim Alicuotas As String
      Dim Cantidades As String
      Dim Precios As String
      Dim Salida As String
      Dim Descripcion As String
      Dim Tipo  As String
      Dim PorcetajeDesRenglon As String
      Dim PorcetajeDesTotal As Integer
      Dim Texto As String
      Dim vExito As Boolean
      Dim vCommand  As String
      Dim vStatus As Long
      Dim vError As Long
      Dim lPto As Long
      Dim insUtilImprFiscal As clsUtilImprFiscal
      On Error GoTo h_Error
      
      If (BMCOpenPort(valPuerto)) Then
         fImprimeDatosClientesBMC valEnumPrinter, valDatosImprFiscal, refReady
         PorcetajeDesTotal = valDatosImprFiscal.GetPorcentajeDeDescuento
         PorcetajeDesTotal = fDarFormatoNumericoParaImpresion(PorcetajeDesTotal, mEnterosParaDescuento, mDecimalesParaDescuento)
         Set rsDatosArticulo = valDatosImprFiscal.GetItems
         Set insUtilImprFiscal = New clsUtilImprFiscal
         If Not rsDatosArticulo.BOF And Not rsDatosArticulo.EOF Then
            rsDatosArticulo.MoveFirst
            Do While Not rsDatosArticulo.EOF
               Alicuotas = rsDatosArticulo.Fields("AlicuotaIVA").Value
               Cantidades = gConvert.fConvierteAString(rsDatosArticulo.Fields("Cantidad").Value)
               Cantidades = fDarFormatoNumericoParaImpresion(Cantidades, mEnterosParaCantidad, mDecimalesParaCantidad)
               Precios = gConvert.fConvierteAString(rsDatosArticulo.Fields("PrecioSinIVA").Value)
               Precios = fDarFormatoNumericoParaImpresion(Precios, mEnterosParaMonto, mDecimalesParaMonto)
               Descripcion = gTexto.fLimpiaStringdeBlancosAAmbosLados(rsDatosArticulo.Fields("Descripcion").Value)
               Descripcion = insUtilImprFiscal.fCadenaCaracteresValidos(Descripcion, MAX_LEN_CADENA)
               Tipo = rsDatosArticulo.Fields("TipoAlicuota").Value
               PorcetajeDesRenglon = rsDatosArticulo.Fields("Descuento")
               PorcetajeDesRenglon = fDarFormatoNumericoParaImpresion(PorcetajeDesRenglon, mEnterosParaDescuento, mDecimalesParaDescuento)
               If gTexto.DfLen(rsDatosArticulo.Fields("Serial").Value) > 0 Then
                 vCommand = "@" & rsDatosArticulo.Fields("Serial").Value
                 vExito = SendCmd(vStatus, vError, vCommand)
               End If
               If gTexto.DfLen(rsDatosArticulo.Fields("Rollo").Value) > 0 Then
                 vCommand = "@" & rsDatosArticulo.Fields("Rollo").Value
                 vExito = SendCmd(vStatus, vError, vCommand)
               End If
               vExito = fEfectuaVentaBMC(Descripcion, Cantidades, Precios, Alicuotas, Tipo, PorcetajeDesRenglon, valDatosImprFiscal.GetEsNotaDeCredito) '
               rsDatosArticulo.MoveNext
            Loop
            If gTexto.DfLen(valDatosImprFiscal.GetTotalesMonedaExtranjera) > 0 And valDatosImprFiscal.GetImpresoraFiscal <> eIF_BMC_TH34_EJ Then
               sImprimeTotalesEnDivisaBMC valDatosImprFiscal.GetTotalesMonedaExtranjera
            ElseIf valDatosImprFiscal.GetImpresoraFiscal <> eIF_BMC_TH34_EJ And Not valDatosImprFiscal.GetEsNotaDeCredito Then
               sImprimeCamposDefiniblesBMC valDatosImprFiscal
               sHandleStatusAPi vStatus
            End If
            If gConvert.fConvierteACurrency(PorcetajeDesTotal) <> 0 Then
               vCommand = "3"
               refReady = SendCmd(vStatus, vError, vCommand)
               If PorcetajeDesTotal > 999 Then
                  vCommand = "p" & "-" & PorcetajeDesTotal
               Else
                  vCommand = "p-0" & PorcetajeDesTotal
               End If
               refReady = SendCmd(vStatus, vError, vCommand)
            End If
            
         End If
         If vExito Then
            If Not valDatosImprFiscal.GetFormaDePago Is Nothing And Not (valDatosImprFiscal.GetEsNotaDeCredito) Then
               sFormaDePago_BMC valDatosImprFiscal
            Else
               vCommand = "f01000000000000"
               refReady = SendCmd(vStatus, vError, vCommand)
            End If
         Else
            fCancelaCuponFiscal_BMC False, valPuerto
         End If
         sHandleStatusAPi vStatus
         sHandleErrorAPi vError
         Set rsDatosArticulo = Nothing
      Else
         refReady = False
         gMessage.Advertencia "Problemas con el puerto no permiten su apertura, verifique su computador."
         BMCClosePort
      End If
     BMCClosePort
h_EXIT: On Error GoTo 0
   refReady = vExito
   Exit Sub
h_Error: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sImprimeVentaArticulo_BMC", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sFormaDePago_BMC(ByVal valDatosImprFiscal As clsDatosImprFiscal)
   Dim MontoCancelado As String
   Dim rsFormaDePago As ADODB.Recordset
   Dim formadePago As String
   Dim FormaDeCobro As String
   Dim vCommand  As String
   Dim vStatus As Long
   Dim vError As Long
   Dim vExito As Boolean
   On Error GoTo h_Error
   Set rsFormaDePago = valDatosImprFiscal.GetFormaDePago
   If gDbUtil.fRecordCount(rsFormaDePago) > 0 Then
      rsFormaDePago.MoveFirst
      Do While Not rsFormaDePago.EOF
         MontoCancelado = 0
         FormaDeCobro = fFormaDeCobro(rsFormaDePago("CodigoFormaDelCobro").Value)
         If rsFormaDePago.RecordCount = 1 Then
            vCommand = "1" & FormaDeCobro  ' asigna el monto total al documento
         Else
            MontoCancelado = rsFormaDePago.Fields("Monto").Value
            MontoCancelado = fDarFormatoNumericoParaImpresion(MontoCancelado, mEnterosParaMontosDePagos, mDecimalesParaMonto)
            vCommand = "2" & FormaDeCobro & MontoCancelado  ' Se utiliza para varias formas de cobro
         End If
            vExito = SendCmd(vStatus, vError, vCommand)
            sHandleStatusAPi vStatus
            If Not vExito Then
               sHandleErrorAPi vError
               Exit Do
            End If
            rsFormaDePago.MoveNext
      Loop
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_Error: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sFormaDePago_BMC", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Function fFormaDeCobro(FormaDeCobro As String) As String
    Dim vResult As String
    On Error GoTo h_Error
   Select Case FormaDeCobro
      Case "00001": vResult = "01" 'Format(1, FORMATO_ENTERO_2)
      Case "00002": vResult = "05" 'Format(5, FORMATO_ENTERO_2)
      Case "00003": vResult = "09" 'Format(9, FORMATO_ENTERO_2)
      Case Else: vResult = "01"
   End Select
h_EXIT: On Error GoTo 0
   fFormaDeCobro = vResult
   Exit Function
h_Error: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fFormaDeCobro", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function CalcularDescuentoParaDevolucion(ByVal valPrecio As String, ByVal valPorcDcto As String) As String
   Dim vResult As String
   On Error GoTo h_Error
   Dim vPrecioConDecimales As Currency
   Dim vDescuentoConDeciamles As Currency
          
   vPrecioConDecimales = gConvert.fConvertStringToCurrency(valPrecio) * gUtilMathOperations.fPotenciaDeBaseX(10, mDecimalesParaMonto * -1)
   vDescuentoConDeciamles = gConvert.fConvertStringToCurrency(valPorcDcto) * gUtilMathOperations.fPotenciaDeBaseX(10, mDecimalesParaDescuento * -1)
   vResult = gConvert.fConvierteAString(vPrecioConDecimales * ((100 - vDescuentoConDeciamles) / 100))
   vResult = fDarFormatoNumericoParaImpresion(vResult, mEnterosParaMonto, mDecimalesParaMonto)
h_EXIT: On Error GoTo 0
   CalcularDescuentoParaDevolucion = vResult
   Exit Function
   CalcularDescuentoParaDevolucion = 0
h_Error: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "CalcularDescuentoParaDevolucion", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fEfectuaVentaBMC(ByVal valDescripcion As String, ByVal valCantidad As String, ByVal valPrecio As String, _
                              ByVal valAlicuota As String, ByVal valTipo As Integer, ByVal valPorcetajeDesRenglon As String, ByVal valEsDevolucion As Boolean) As Boolean
   Dim vCmdRequest As Long
   Dim vCommand  As String
   Dim vStatus As Long
   Dim vError As Long
   Dim vExito As Boolean
  
   On Error GoTo h_Error
   If valEsDevolucion Then
      vCommand = "d"
      vCommand = vCommand & Chr(&H20 + valTipo)
      valPrecio = CalcularDescuentoParaDevolucion(valPrecio, valPorcetajeDesRenglon)
   Else
      vCommand = Chr(&H20 + valTipo)
   End If
   vCommand = vCommand & valPrecio
   vCommand = vCommand & valCantidad
   vCommand = vCommand & gTexto.DfLeft(valDescripcion & Space(50), 40)
   vCmdRequest = SendCmd(vStatus, vError, vCommand)
   sHandleStatusAPi vStatus
   If vCmdRequest = 1 Then
      vExito = True
      If Not valEsDevolucion Then
         If valPorcetajeDesRenglon > 0 Then
            vCommand = "p-" & valPorcetajeDesRenglon
            vCmdRequest = SendCmd(vStatus, vError, vCommand)
            sHandleStatusAPi vStatus
            If vCmdRequest <> 1 Then
               sHandleErrorAPi vError
            End If
         End If
      End If
   Else
      sHandleErrorAPi vError
   End If
h_EXIT: On Error GoTo 0
      fEfectuaVentaBMC = vExito
   Exit Function
   fEfectuaVentaBMC = False
h_Error: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fEfectuaVentaBMC", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fUltimoNumeroMemoriaFiscal_BMC(ByRef refReady As Boolean, ByVal valDatosImprFiscal As clsDatosImprFiscal, valPuerto As String) As String
   Dim vCommand As String
   Dim lCmdResult As Long
   Dim vExito As Long
   Dim vMessage As String
   Dim vStatus As Long
   Dim vUltimoNumero As String
   Dim vError As Long
   Dim lport As Long
   Dim vTemporal As String
   On Error GoTo h_Error
   vUltimoNumero = ""
      
   If BMCOpenPort(valPuerto) = 1 Then
      vCommand = "S1"
      vMessage = Space(255)
      lCmdResult = UploadStatusCmd(vStatus, vError, vCommand, vMessage)
      sHandleStatusAPi vStatus
      If lCmdResult = 1 Then
         vTemporal = gTexto.fPrimerTokenYLoEliminaDelTexto(vMessage, Chr(10))
         vTemporal = gTexto.fPrimerTokenYLoEliminaDelTexto(vMessage, Chr(10))
         If valDatosImprFiscal.GetEsNotaDeCredito Then
            vTemporal = gTexto.fPrimerTokenYLoEliminaDelTexto(vMessage, Chr(10))
            vTemporal = gTexto.fPrimerTokenYLoEliminaDelTexto(vMessage, Chr(10))
            vTemporal = gTexto.fPrimerTokenYLoEliminaDelTexto(vMessage, Chr(10))
            vTemporal = gTexto.fPrimerTokenYLoEliminaDelTexto(vMessage, Chr(10))
         End If
         vExito = True
         vUltimoNumero = gTexto.fPrimerTokenYLoEliminaDelTexto(vMessage, Chr(10)) ' Serial
      Else
         sHandleErrorAPi vError
      End If
      BMCClosePort
   Else
      vExito = False
      gMessage.Advertencia "No se puede establecer comunicación con la impresora." & vbCrLf & "1.- Por favor salga del Sistema." & vbCrLf & "2.- Apague la impresora y enciéndala de nuevo." & vbCrLf & "3.-  Verifique todas las conexiones desde el PC a l Impresora. " & vbCrLf & "4.- Vuelva a Intentar la operación."
   End If
h_EXIT: On Error GoTo 0
   fUltimoNumeroMemoriaFiscal_BMC = vUltimoNumero
   refReady = vExito
   Exit Function
h_Error:
   refReady = False
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fUltimoNumeroMemoriaFiscal_BMC", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Sub sRealizarCierreX_BMC(ByRef refReady As Boolean, valPuerto As String)
   Dim vCommand As String
   Dim vExito As Boolean
   Dim vStatus As Long
   Dim vError As Long
   Dim lport As Long
   Dim lCmdResult As Long
   On Error GoTo h_Error
   vExito = False
   If BMCOpenPort(valPuerto) Then
      vCommand = "I0X"
      lCmdResult = SendCmd(vStatus, vError, vCommand)
      sHandleStatusAPi vStatus
      sHandleErrorAPi vError
      If lCmdResult = 1 Then vExito = True
      BMCClosePort
   Else
      vExito = False
      gMessage.Advertencia "No se puede establecer comunicación con la impresora." & vbCrLf & "1.- Por favor salga del Sistema." & vbCrLf & "2.- Apague la impresora y enciéndala de nuevo." & vbCrLf & "3.-  Verifique todas las conexiones desde el PC a l Impresora. " & vbCrLf & "4.- Vuelva a Intentar la operación."
   End If
h_EXIT: On Error GoTo 0
    refReady = vExito
   Exit Sub
h_Error: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sRelaizarCierreX_BMC", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sLimpiarPieDePaginaBMC()
   Dim Cmd As String
   Dim vLinea As Integer
   Dim lCmdResult As Long
   Dim i As Integer
   Dim vError As Long
   On Error GoTo h_Error
      
   For i = 91 To 98
    Cmd = "P" & "H" & gConvert.fConvierteAString(i) & ""
    lCmdResult = SendCmd(0, vError, Cmd)
    If vError <> 0 Then
      Exit For
    End If
   Next i
   
h_EXIT: On Error GoTo 0
   Exit Sub
h_Error: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sImprimeCamposDefiniblesSAMSUNG", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)

End Sub

Private Sub sImprimeCamposDefiniblesBMC(ByVal valInsDatosImprFiscal As clsDatosImprFiscal)
   Dim vRsCamposDefinibles As ADODB.Recordset
   Dim vText As String
   Dim vOutPut As String
   Dim Cmd As String
   Dim vLinea As Integer
   Dim lCmdResult As Long
   Dim insUtilImprFiscal As clsUtilImprFiscal
   Dim vError As Long
   On Error GoTo h_Error
   Set insUtilImprFiscal = New clsUtilImprFiscal
   sLimpiarPieDePaginaBMC
   vLinea = 91
   If valInsDatosImprFiscal.GetImprimeCamposDefinibles Then
      Set vRsCamposDefinibles = valInsDatosImprFiscal.GetRSCamposDefinibles
      If Not (vRsCamposDefinibles Is Nothing) Then
         If gDbUtil.fRecordCount(vRsCamposDefinibles) > 0 Then
            vRsCamposDefinibles.MoveFirst
            Do While Not vRsCamposDefinibles.EOF
               vText = gTexto.DfMid(vRsCamposDefinibles.Fields("CampoDefinible").Value, 1, 35)
               vText = insUtilImprFiscal.fCadenaCaracteresValidos(vText, 35)
               If gTexto.DfLen(vText) > 0 Then
                  Cmd = "P" & "H" & gConvert.fConvierteAString(vLinea) & vText
                  lCmdResult = SendCmd(0, vError, Cmd)
                  vLinea = vLinea + 1
               End If
               vRsCamposDefinibles.MoveNext
               If vError <> 0 Then
                  Exit Do
               End If
            Loop
         End If
      End If
      Set vRsCamposDefinibles = Nothing
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_Error: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sImprimeCamposDefiniblesBMC", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sImprimeTotalesEnDivisaBMC(ByVal valTotales As String)
   Dim Cmd As String
   Dim vLinea As Integer
   Dim lCmdResult As Long
   Dim vList As Variant
   Dim vPos As Integer
   Dim vError As Long
   On Error GoTo h_Error
   sLimpiarPieDePaginaBMC
   vLinea = 91
   vList = gTexto.fConvertCadenaEnArray(valTotales, vbNewLine)
   If UBound(vList) > 0 Then
      For vPos = LBound(vList) To UBound(vList)
         If LenB(vList(vPos)) > 0 Then
            Cmd = "P" & "H" & gConvert.fConvierteAString(vLinea) & vList(vPos)
            lCmdResult = SendCmd(0, vError, Cmd)
            vLinea = vLinea + 1
         End If
      Next
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_Error: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sImprimeTotalesEnDivisaBMC", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Function fObtenerUltimoReporteZ_BMC(valPuerto As String) As String
   Dim vCommand As String
   Dim vExito As Boolean
   Dim vMessage As String
   Dim vStatus As Long
   Dim vUltimoReporteZ As String
   Dim vError As Long
   Dim lport As Long
   Dim vTemporal As String
   On Error GoTo h_Error
   If BMCOpenPort(valPuerto) Then
      vCommand = "S1"
      vMessage = Space(255)
      vExito = UploadStatusCmd(vStatus, vError, vCommand, vMessage)
      sHandleStatusAPi vStatus
      If vExito Then
         vTemporal = gTexto.fPrimerTokenYLoEliminaDelTexto(vMessage, Chr(10))
         vTemporal = gTexto.fPrimerTokenYLoEliminaDelTexto(vMessage, Chr(10))
         vTemporal = gTexto.fPrimerTokenYLoEliminaDelTexto(vMessage, Chr(10))
         vTemporal = gTexto.fPrimerTokenYLoEliminaDelTexto(vMessage, Chr(10))
         vTemporal = gTexto.fPrimerTokenYLoEliminaDelTexto(vMessage, Chr(10))
         vTemporal = gTexto.fPrimerTokenYLoEliminaDelTexto(vMessage, Chr(10))
         vTemporal = gTexto.fPrimerTokenYLoEliminaDelTexto(vMessage, Chr(10))
         vTemporal = gTexto.fPrimerTokenYLoEliminaDelTexto(vMessage, Chr(10))
         vUltimoReporteZ = gTexto.fPrimerTokenYLoEliminaDelTexto(vMessage, Chr(10)) ' Contador de Z
      Else
         sHandleErrorAPi vError
      End If
      BMCClosePort
   Else
      vExito = False
      gMessage.Advertencia "No se puede establecer comunicación con la impresora." & vbCrLf & "1.- Por favor salga del Sistema." & vbCrLf & "2.- Apague la impresora y enciéndala de nuevo." & vbCrLf & "3.-  Verifique todas las conexiones desde el PC a l Impresora. " & vbCrLf & "4.- Vuelva a Intentar la operación."
   End If
h_EXIT: On Error GoTo 0
   fObtenerUltimoReporteZ_BMC = vUltimoReporteZ
   Exit Function
h_Error:
   fObtenerUltimoReporteZ_BMC = ""
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fObtenerUltimoReporteZ_BMC", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function BMCOpenPort(ByVal valPuerto As String) As Boolean
   Dim vResult As Boolean
   Dim lRet As Long
   On Error GoTo h_Error
      lRet = OpenFpctrl("COM" & valPuerto)
      If lRet = 1 Then
         Sleep (100)
         vResult = CheckFprinter()
         SendCmd 0, 0, "e" 'Reinicia status de impresora fiscal
         If vResult = False Then
            gMessage.Advertencia "Error de comunicación." & vbCrLf & "Verifique conexión de la impresora."
            BMCOpenPort = False
         End If
      Else
         gMessage.Advertencia "Error de comunicación." & vbCrLf & "Verifique conexión de la impresora."
         BMCOpenPort = False
      End If
h_EXIT:    On Error GoTo 0
   BMCOpenPort = vResult
   Exit Function
h_Error:
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "BMCOpenPort", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Sub BMCClosePort()
   On Error GoTo h_Error
   CloseFpctrl
   Sleep (250)
h_EXIT:    On Error GoTo 0
   Exit Sub
h_Error:
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "BMCClosePort", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Function fSetDecimalSeparator(ByVal valNumero As String) As String
   Dim vResult  As String
   Dim DecimalSeparator As String
   
   On Error GoTo h_Error
   DecimalSeparator = gConvert.getSystemDefaultDecimalSetting
   If (gTexto.fS1SeEncuentraEnS2(".", valNumero) And Not gTexto.fS1SeEncuentraEnS2(DecimalSeparator, valNumero)) Then
       vResult = gTexto.DfReplace(valNumero, ".", DecimalSeparator)
    ElseIf (gTexto.fS1SeEncuentraEnS2(",", valNumero) And Not gTexto.fS1SeEncuentraEnS2(DecimalSeparator, valNumero)) Then
       vResult = gTexto.DfReplace(valNumero, ",", DecimalSeparator)
    Else
       vResult = valNumero
   End If
h_EXIT:    On Error GoTo 0
   fSetDecimalSeparator = vResult
   Exit Function
h_Error:
   fSetDecimalSeparator = ""
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSetDecimalSeparator", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fDarFormatoNumericoParaImpresion(ByVal valNumero As String, ByVal valCantidadEnteros As Byte, ByVal valCantidadDecimales As Byte) As String
   Dim vValorFinal As String
   Dim vTokenPosition As Integer
   Dim vValorCurr As Currency
   Dim vParteEntera As String
   Dim vParteDecimal As String
     
   On Error GoTo h_Error
   If valNumero = "" Then
      valNumero = "0"
   End If
   valNumero = gTexto.fLimpiaStringdeBlancosAAmbosLados(valNumero)
   valNumero = fSetDecimalSeparator(valNumero)
   vValorCurr = gUtilMathOperations.fRedondeaANDecimales(gConvert.fConvierteACurrency(valNumero), valCantidadDecimales)
   vValorCurr = Abs(vValorCurr)
   vValorFinal = vValorCurr * gUtilMathOperations.fPotenciaDeBaseX(10, valCantidadDecimales)
   vValorFinal = gTexto.llenaConCaracterALaIzquierda(vValorFinal, "0", valCantidadEnteros + valCantidadDecimales)
   fDarFormatoNumericoParaImpresion = vValorFinal
h_EXIT:
   Exit Function
h_Error:
   fDarFormatoNumericoParaImpresion = ""
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDarFormatoNmericoParaImpresion", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
