VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsImpFiscalQPrint"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Attribute VB_Ext_KEY = "SavedWithClassBuilder6" ,"Yes"
Attribute VB_Ext_KEY = "Top_Level" ,"Yes"
Private Declare Function SendCommand Lib "quorion.dll" (ByVal Cmd As String, ByVal trama As String) As Integer
Private Declare Function OpenComPort Lib "quorion.dll" (ByVal port As Integer, ByVal BAUDRATE As Integer) As Integer
Private Declare Function CloseComPort Lib "quorion.dll" () As Integer
Private Declare Function OpenIPPort Lib "quorion.dll" (ByVal ip0 As Integer, ByVal ip1 As Integer, ByVal ip2 As Integer, ByVal ip3 As Integer, ByVal reg As Integer) As Integer
Private Declare Function CloseIPPort Lib "quorion.dll" () As Integer
Private Declare Function GetVer Lib "quorion.dll" (ByVal vers As String) As Integer
Private Declare Sub Sleep Lib "kernel32.dll" (ByVal dwMilliseconds As Long)

Private Const ESTATUS1 = "F0;1"
Private Const ESTATUS3 = "F0;3"
Private Const BAUDRATEDEFECTO = 6 '57600
Private Const SEPARADORQF = "QF"
Private Const SEPARADORBARRA = "|"
Private Const FORMATO_PRECIO As String = "000000000"
Private Const FORMATO_CANTIDAD As String = "00000"
Private Const NUMERO_MAXIMO = 9999999.99
'Private Tipo_Conexion As String

Private Function CM_FILE_NAME() As String
   CM_FILE_NAME = "clsImpFiscalQPrintMF"
End Function

Private Function CM_MESSAGE_NAME() As String
   CM_MESSAGE_NAME = "Impresora Fiscal QPrint"
End Function

Private Function GetGender() As Enum_Gender
   GetGender = eg_Female
End Function

Public Function fSerialMemoriaFiscal_QPrintMF(ByRef refReady As Boolean, valPuerto As String, valTipoConexion As enum_TipoConexion, ByVal valIp As String, ByVal valNumeroCaja As String) As String
   Dim vError As String
   Dim vTrama As String
   Dim vSerial As String
   Dim vMensaje As String
   On Error GoTo h_Error
   vSerial = ""
   fAbrirConexion valTipoConexion, valPuerto, valIp, valNumeroCaja
   vTrama = Space$(1024)
   vError = SendCommand(ESTATUS1, vTrama)
   If (fAnalizarRespuestaDeSendCommand(vError, vMensaje) = "1") Then
      gMessage.Advertencia vMensaje
      refReady = False
      GoTo h_EXIT:
   End If
   If (vError <> -1) Then
      vSerial = gTexto.DfLeft(gTexto.DfMid(vTrama, InStr(vTrama, SEPARADORQF) + 3), InStr(gTexto.DfMid(vTrama, InStr(vTrama, SEPARADORQF) + 3), SEPARADORBARRA) - 1)
      refReady = True
   End If
   fSerialMemoriaFiscal_QPrintMF = vSerial
h_EXIT: On Error GoTo 0
   vError = fCerrarConexion(valTipoConexion)
   Exit Function
h_Error:
   fCerrarConexion valTipoConexion
   refReady = False
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSerialMemoriaFiscal_QPrintMF", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Sub sRealizarCierreZ_QPrintMF(ByRef refReady As Boolean, valPuerto As String, valTipoConexion As enum_TipoConexion, ByVal valIp As String, ByVal valCajaNumero As String)
   Dim vError As String
   Dim vTrama As String
   Dim vSerial As String
   Dim vComandoZ As String
   On Error GoTo h_Error
   vComandoZ = "F3;1"
   vTrama = Space$(1024)
   fAbrirConexion valTipoConexion, valPuerto, valIp, valCajaNumero
   vError = SendCommand(vComandoZ, vTrama)
   If (vError <> -1) Then
      refReady = True
   Else
      refReady = False
   End If
h_EXIT: On Error GoTo 0
   vError = fCerrarConexion(valTipoConexion)
   Exit Sub
h_Error:
   fCerrarConexion valTipoConexion
   refReady = False
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sRealizarCierreZ_QPrintMF", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Sub sAbreCF_QPrintMF(ByVal valEnumPrinter As Enum_ImpresorasFiscales, ByVal valDatosImprFiscal As clsDatosImprFiscal, ByRef refReady As Boolean, ByVal valPuerto As String, ByVal valTipoConexion As enum_TipoConexion, ByVal valIp As String, ByVal valCajaNumero As String)
   Dim vError As String
   Dim vTrama As String
   Dim vCommand As String
   On Error GoTo h_Error
   vTrama = Space$(1024)
   vError = fAbrirConexion(valTipoConexion, valPuerto, valIp, valCajaNumero)
   If (vError <> -1) Then
      refReady = True
      vError = fImprimeDatosClientesQPrintMF(valEnumPrinter, valDatosImprFiscal, refReady)
   Else
      refReady = False
   End If
h_EXIT:    On Error GoTo 0
   vError = fCerrarConexion(valTipoConexion)
   Exit Sub
h_Error:
   fCerrarConexion valTipoConexion
   refReady = False
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sAbreCF_QPrintMF", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Function fUltimoNumeroMemoriaFiscal_QPrintMF(ByRef refReady As Boolean, ByVal valDatosImprFiscal As clsDatosImprFiscal, ByVal valPuerto As String, ByVal valTipoConexion As enum_TipoConexion, ByVal valIp As String, ByVal valCajaNumero As String) As String
   Dim vError As String
   Dim vTrama As String
   Dim vComando As String
   Dim vUltimoNumeroComprobanteFiscal As String
   On Error GoTo h_Error
   vTrama = Space$(1024)
   vComando = ESTATUS3
   vUltimoNumeroComprobanteFiscal = ""
   vError = fAbrirConexion(valTipoConexion, valPuerto, valIp, valCajaNumero)
   vError = SendCommand(vComando, vTrama)
   If (vError <> -1) Then
      vUltimoNumeroComprobanteFiscal = gTexto.DfMid(vTrama, InStr(vTrama, SEPARADORQF) + 3)
      vUltimoNumeroComprobanteFiscal = gTexto.DfMid(vUltimoNumeroComprobanteFiscal, InStr(vUltimoNumeroComprobanteFiscal, SEPARADORBARRA) + 1)
      vUltimoNumeroComprobanteFiscal = gTexto.DfMid(vUltimoNumeroComprobanteFiscal, InStr(vUltimoNumeroComprobanteFiscal, SEPARADORBARRA) + 1)
      vUltimoNumeroComprobanteFiscal = gTexto.DfMid(vUltimoNumeroComprobanteFiscal, InStr(vUltimoNumeroComprobanteFiscal, SEPARADORBARRA) + 1)
      If (Not valDatosImprFiscal.GetEsNotaDeCredito) Then
         vUltimoNumeroComprobanteFiscal = gTexto.DfMid(vUltimoNumeroComprobanteFiscal, InStr(vUltimoNumeroComprobanteFiscal, SEPARADORBARRA) + 1)
         vUltimoNumeroComprobanteFiscal = gTexto.DfLeft(vUltimoNumeroComprobanteFiscal, InStr(vUltimoNumeroComprobanteFiscal, SEPARADORBARRA) - 1)
      Else
         vUltimoNumeroComprobanteFiscal = gTexto.DfLeft(vUltimoNumeroComprobanteFiscal, InStr(vUltimoNumeroComprobanteFiscal, SEPARADORBARRA) - 1)
      End If
      refReady = True
   End If
   fUltimoNumeroMemoriaFiscal_QPrintMF = vUltimoNumeroComprobanteFiscal
h_EXIT:       On Error GoTo 0
   vError = fCerrarConexion(valTipoConexion)
   Exit Function
h_Error:
   refReady = False
   vError = fCerrarConexion(valTipoConexion)
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fUltimoNumeroMemoriaFiscal_QPrintMF", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Sub sRealizarCierreX_QPrintMF(ByRef refReady As Boolean, ByVal valPuerto As String, ByVal valTipoConexion As enum_TipoConexion, ByVal valIp As String, ByVal valCajaNumero As String)
   Dim vError As String
   Dim vTrama As String
   Dim vSerial As String
   Dim vComandoX As String
   On Error GoTo h_Error
   vComandoX = "F3;1;1"
   vTrama = Space$(1024)
   vError = fAbrirConexion(valTipoConexion, valPuerto, valIp, valCajaNumero)
   vError = SendCommand(vComandoX, vTrama)
   If (vError <> -1) Then
      refReady = True
   Else
      refReady = False
   End If
h_EXIT:    On Error GoTo 0
   vError = fCerrarConexion(valTipoConexion)
   Exit Sub
h_Error:
   refReady = False
   vError = fCerrarConexion(valTipoConexion)
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sRelaizarCierreX_QPrintMF", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Function fImprimeDatosClientesQPrintMF(ByVal valEnumPrinter As Enum_ImpresorasFiscales, ByVal valDatosImprFiscal As clsDatosImprFiscal, ByRef refReady As Boolean) As Boolean
   Dim Texto As String
   Dim Cmd As String
   Dim vExito As Boolean
   Dim Hora As String
   Dim Dia As String
   Dim Mes As String
   Dim Ano As String
   Dim Fecha As String
   Dim vCommand As String
   Dim vError As String
   Dim vTrama As String
   On Error GoTo h_Error
   vTrama = Space(1024)
   If valDatosImprFiscal.GetEsNotaDeCredito Then
      vCommand = "F2;12;0;3"
   Else
      vCommand = "F2;1;0;3"
   End If
   If Not valDatosImprFiscal.GetNombreCliente = "" Then
      vCommand = vCommand & fGenerarStringParaInformacionDelCliente("Nombre: " & valDatosImprFiscal.GetNombreCliente)
      vCommand = vCommand & fGenerarStringParaInformacionDelCliente("CI/RIF: " & valDatosImprFiscal.GetNumeroRIFCliente)
      vCommand = vCommand & fGenerarStringParaInformacionDelCliente("Tlf: " & valDatosImprFiscal.GetTelefono)
      vCommand = vCommand & fGenerarStringParaInformacionDelCliente("Direccion: " & valDatosImprFiscal.GetDireccion)
      vCommand = vCommand & fGenerarStringParaInformacionDelCliente("Observaciones: " & valDatosImprFiscal.GetObservaciones)
   End If
   If valDatosImprFiscal.GetEsNotaDeCredito Then
      Hora = valDatosImprFiscal.GetHoraModificacion
      Dia = gConvert.fConvierteAString(Day(valDatosImprFiscal.GetFecha))
      Mes = gConvert.fConvierteAString(Month(valDatosImprFiscal.GetFecha))
      Ano = gConvert.fConvierteAString(Year(valDatosImprFiscal.GetFecha))
      Fecha = Dia & "-" & Mes & "-" & Ano
      vCommand = vCommand & fGenerarStringParaInformacionDelCliente("# de Factura:" & valDatosImprFiscal.GetNumeroTicket)
      vCommand = vCommand & fGenerarStringParaInformacionDelCliente("Serial Maquina: " & valDatosImprFiscal.GetSerialImpresoraFiscal)
      vCommand = vCommand & fGenerarStringParaInformacionDelCliente(Fecha)
   End If
   vCommand = vCommand & ";"
   vError = SendCommand(vCommand, vTrama)
h_EXIT: On Error GoTo 0
   refReady = vError <> -1
   fImprimeDatosClientesQPrintMF = vExito
   Exit Function
h_Error:
   refReady = False
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fImprimeDatosClientesQPrintMF", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fCancelaCuponFiscal_QPrintMF(ByVal valMotivo, ByVal valPuerto As String, ByVal valTipoConexion As enum_TipoConexion, ByVal valIp As String, ByVal valCajaNumero As String) As Boolean
   Dim vError As Integer
   Dim vTrama As String
   Dim vCommand  As String
   On Error GoTo h_Error
   vTrama = Space$(1024)
   'vError = fAbrirConexion(valTipoConexion, valPuerto, valIp, valCajaNumero)
   If (vError <> -1) Then
      vError = SendCommand("F2;5", vTrama)
      gMessage.Advertencia "Su factura ha sido cancelada, por: " & valMotivo
   End If
   fCancelaCuponFiscal_QPrintMF = vExito <> -1
h_EXIT:    On Error GoTo 0
   vError = fCerrarConexion(valTipoConexion)
   Exit Function
h_Error:
   vError = fCerrarConexion(valTipoConexion)
   fCancelaCuponFiscal_QPrintMF = False
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCancelaCuponFiscal_QPrintMF", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Sub sImprimeVentaArticulo_QPrintMF(ByVal valEnumPrinter As Enum_ImpresorasFiscales, ByVal valDatosImprFiscal As clsDatosImprFiscal, ByRef refReady As Boolean, ByVal valPuerto As String, ByVal valTipoConexion As enum_TipoConexion, ByVal valIp As String, ByVal valCajaNumero As String)
   Dim rsDatosArticulo As ADODB.Recordset
   Dim Alicuotas As String
   Dim Cantidades As String
   Dim Precios As String
   Dim Salida As String
   Dim Descripcion As String
   Dim Tipo  As String
   Dim PorcetajeDesRenglon As String
   Dim PorcetajeDesTotal As String
   Dim Texto As String
   Dim vExito As Boolean
   Dim vCommand  As String
   Dim vStatus As Long
   Dim vError As Long
   Dim vTrama As String
   Dim vDescuentoTotal As String
   Dim vTotalDevolucion As Double
   On Error GoTo h_Error
   vTotalDevolucion = 0
   vTrama = Space$(1024)
   vError = fAbrirConexion(valTipoConexion, valPuerto, valIp, valCajaNumero)
   If (vError <> -1) Then
      PorcetajeDesTotal = valDatosImprFiscal.GetPorcentajeDeDescuento
      Set rsDatosArticulo = valDatosImprFiscal.GetItems
      If Not rsDatosArticulo.BOF And Not rsDatosArticulo.EOF Then
         rsDatosArticulo.MoveFirst
         Do While Not rsDatosArticulo.EOF
         
            Alicuotas = rsDatosArticulo.Fields("AlicuotaIVA").Value
            Cantidades = rsDatosArticulo.Fields("Cantidad").Value
            If gConvert.getSystemDefaultThousandSetting = "." Then Cantidades = fDarFormatoANumero(Cantidades)
            Cantidades = Abs(Cantidades)
            'Cantidades = fDarFormatoANumero(Cantidades)
            Precios = rsDatosArticulo.Fields("PrecioSinIVA").Value
            If gConvert.getSystemDefaultThousandSetting = "." Then Precios = fDarFormatoANumero(Precios)
            Precios = Abs(Precios)
            'Precios = fDarFormatoANumero(Precios)
            PorcetajeDesRenglon = rsDatosArticulo.Fields("Descuento").Value
            If gConvert.getSystemDefaultThousandSetting = "." Then PorcetajeDesRenglon = fDarFormatoANumero(PorcetajeDesRenglon)
            'PorcetajeDesRenglon = fDarFormatoANumero(PorcetajeDesRenglon)
            Descripcion = rsDatosArticulo.Fields("Descripcion").Value
            Tipo = rsDatosArticulo.Fields("TipoAlicuota").Value
            'vTotalDevolucion = vTotalDevolucion + (Precios * (1 - (PorcetajeDesRenglon / 100)) * (1 + (Alicuotas / 100)))
            Dim vAuxiliar As Double
            vAuxiliar = gUtilMathOperations.fPowNDecimalsAndTruncValue(Precios * Cantidades, 2)
            vAuxiliar = gUtilMathOperations.fPowNDecimalsAndTruncValue(vAuxiliar * (1 - (PorcetajeDesRenglon / 100)), 2)
            vAuxiliar = gUtilMathOperations.fPowNDecimalsAndTruncValue(vAuxiliar * (1 - (PorcetajeDesTotal / 100)), 2)
            vAuxiliar = gUtilMathOperations.fPowNDecimalsAndTruncValue(vAuxiliar * (1 + (Alicuotas / 100)), 2)
            If ((vAuxiliar >= NUMERO_MAXIMO) Or (Cantidades >= 1000) Or (Precios >= NUMERO_MAXIMO)) Then
               fCancelaCuponFiscal_QPrintMF " cantidad debe ser menor a 1000, el precio menor a  9.999.999,19 y el monto total de la factura debe ser menor a 9.999.999,19", valPuerto, valTipoConexion, valIp, valCajaNumero
               refReady = False
               GoTo h_EXIT
            End If
            vTotalDevolucion = vTotalDevolucion + vAuxiliar
            vExito = fEfectuaVentaQPrintMF(Descripcion, fDarFormatoANumero(Cantidades), fDarFormatoANumero(Precios), Alicuotas, Tipo, fDarFormatoANumero(PorcetajeDesRenglon), valDatosImprFiscal.GetEsNotaDeCredito)
            If gTexto.DfLen(rsDatosArticulo.Fields("Serial").Value) > 0 Then
               sEnviarDatoAdicionalQPrintMF rsDatosArticulo.Fields("Serial").Value
            End If
            If gTexto.DfLen(rsDatosArticulo.Fields("Rollo").Value) > 0 Then
               sEnviarDatoAdicionalQPrintMF rsDatosArticulo.Fields("Rollo").Value
            End If
            rsDatosArticulo.MoveNext
         Loop
         If gTexto.DfLen(valDatosImprFiscal.GetTotalesMonedaExtranjera) > 0 And valDatosImprFiscal.GetImpresoraFiscal <> eIF_BMC_TH34_EJ Then
            sImprimeTotalesEnDivisaQPrintMF valDatosImprFiscal.GetTotalesMonedaExtranjera
         ElseIf valDatosImprFiscal.GetImpresoraFiscal <> eIF_BMC_TH34_EJ And Not valDatosImprFiscal.GetEsNotaDeCredito Then
            sImprimeCamposDefiniblesQPrintMF valDatosImprFiscal
         End If
         If PorcetajeDesTotal > 0 Then
            vDescuentoTotal = fDarFormatoADescuentoParaImpresion(PorcetajeDesTotal)
            vCommand = "F2;6;;1;;" & vDescuentoTotal & ";;567;"
            vError = SendCommand(vCommand, vTrama)
         End If
      End If
      If vError <> -1 Then
         If Not valDatosImprFiscal.GetFormaDePago Is Nothing And Not (valDatosImprFiscal.GetEsNotaDeCredito) Then
            sFormaDePago_QPrintMF valDatosImprFiscal
         Else
            vCommand = "F2;13;0;" & fDarFormatoANumero(vTotalDevolucion) & ";;" & Chr$(34) & "N.C. Total" & Chr$(34) & ";"
            vError = SendCommand(vCommand, vTrama)
         End If
      Else
         fCancelaCuponFiscal_QPrintMF "Error Inesperado", valPuerto, valTipoConexion, valIp, valCajaNumero
      End If
      Set rsDatosArticulo = Nothing
      fCerrarConexion valTipoConexion
   Else
      refReady = False
      gMessage.Advertencia "Problemas con el puerto no permiten su apertura, verifique su computador."
   End If
   refReady = (vError <> -1)
h_EXIT: On Error GoTo 0
   fCerrarConexion valTipoConexion
   
   Exit Sub
h_Error:
   fCerrarConexion valTipoConexion
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sImprimeVentaArticulo_QPrintMF", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sEnviarDatoAdicionalQPrintMF(ByVal valTextoEntrada As String)
   Dim vCommand  As String
   Dim vError As Long
   Dim vTrama As String
   Dim vTextoEnviar As String
   Dim vIndiceInicio As Long
   Dim vIndiceFinal As Long
   Dim vNumeroCaracteresPorEnviar As Long
   On Error GoTo h_Error
   vIndiceInicio = 1
   vTrama = Space$(1024)
   vNumeroCaracteresPorEnviar = gTexto.DfLen(valTextoEntrada)
   While vNumeroCaracteresPorEnviar > 0 And vIndiceInicio <= gTexto.DfLen(valTextoEntrada)
      If (gTexto.DfLen(valTextoEntrada) - vIndiceInicio > 24) Then
         vIndiceFinal = vIndiceInicio + 24
      Else
         vIndiceFinal = gTexto.DfLen(valTextoEntrada) + 1
      End If
      vTextoEnviar = gTexto.DfMid(valTextoEntrada, vIndiceInicio, vIndiceFinal - vIndiceInicio)
      vCommand = "U4000;1;" & Chr$(34) & vTextoEnviar & Chr$(34)
      vExito = SendCommand(vCommand, vTrama)
      vCommand = "U5;4001;"
      vExito = SendCommand(vCommand, vTrama)
      vNumeroCaracteresPorEnviar = vNumeroCaracteresPorEnviar - (vIndiceFinal - vIndiceInicio)
      vIndiceInicio = vIndiceInicio + 24
   Wend
h_EXIT: On Error GoTo 0
   refReady = (vError <> -1)
   Exit Sub
h_Error:
   CloseComPort
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sEnviarDatoAdicionalQPrintMF", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Function fGenerarStringParaInformacionDelCliente(ByVal valTextoEntrada As String) As String
   Dim vIndiceInicio As Long
   Dim vIndiceFinal As Long
   Dim vCaracteresPorAnalizar As Long
   Dim vTextoFinal As String
   On Error GoTo h_Error
   vIndiceInicio = 1
   vCaracteresPorAnalizar = gTexto.DfLen(valTextoEntrada)
   vTextoFinal = ""
   While vIndiceInicio < gTexto.DfLen(valTextoEntrada) And vCaracteresPorAnalizar > 0
      If (gTexto.DfLen(valTextoEntrada) - vIndiceInicio > 41) Then
         vIndiceFinal = vIndiceInicio + 41
      Else
         vIndiceFinal = gTexto.DfLen(valTextoEntrada) + 1
      End If
      vTextoFinal = vTextoFinal & ";" & Chr$(34) & gTexto.DfMid(valTextoEntrada, vIndiceInicio, vIndiceFinal - vIndiceInicio) & Chr$(34)
      vCaracteresPorAnalizar = vCaracteresPorAnalizar - (vIndiceFinal - vIndiceInicio)
      vIndiceInicio = vIndiceInicio + 41
   Wend
   fGenerarStringParaInformacionDelCliente = vTextoFinal
h_EXIT: On Error GoTo 0
   Exit Function
h_Error:
   fGenerarStringParaInformacionDelCliente = ""
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fGenerarStringParaInformacionDelCliente", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Sub sFormaDePago_QPrintMF(ByVal valDatosImprFiscal As clsDatosImprFiscal)
   Dim MontoCancelado As Currency
   Dim rsFormaDePago As ADODB.Recordset
   Dim formadePago As String
   Dim FormaDeCobro As String
   Dim vCommand  As String
   Dim vError As Long
   Dim vTrama As String
   Dim vMonto As String
   Dim vMensaje As String
   On Error GoTo h_Error
   vTrama = Space$(1024)
   Set rsFormaDePago = valDatosImprFiscal.GetFormaDePago
   If gDbUtil.fRecordCount(rsFormaDePago) > 0 Then
      rsFormaDePago.MoveFirst
      vError = SendCommand("F2;4", vTrama)
      
      Do While Not rsFormaDePago.EOF
         FormaDeCobro = fFormaDeCobro(rsFormaDePago("CodigoFormaDelCobro").Value)
         Select Case FormaDeCobro
            Case "1": vMensaje = "Efectivo"
            Case "2": vMensaje = "Cheque o Deposito"
            Case "3": vMensaje = "Tarjeta"
            Case Else: vMensaje = "Efectivo"
         End Select
         'vMonto = rsFormaDePago(3).Value * 100
         vMonto = gConvert.fConvertStringToCurrency(rsFormaDePago.Fields("Monto").Value)
         vMonto = fDarFormatoANumero(vMonto)
         vCommand = "F2;2;" & vMonto & ";" & FormaDeCobro & ";" & Chr$(34) & vMensaje & Chr$(34) & ";;;;;;;;"
         'vCommand = "F2;2;" & "9999999,99" & ";" & FormaDeCobro & ";" & Chr$(34) & vMensaje & Chr$(34) & ";;;;;;;;"
         vError = SendCommand(vCommand, vTrama)
         rsFormaDePago.MoveNext
      Loop
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_Error: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sFormaDePago_QPrintMF", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sFormaDePagoNotaDeCredito_QPrintMF(ByVal valDatosImprFiscal As clsDatosImprFiscal)
   Dim MontoCancelado As Currency
   Dim FormaDeCobro As String
   Dim vCommand  As String
   Dim vError As Long
   Dim vTrama As String
   Dim vMonto As Double
   Dim vMensaje As String
   On Error GoTo h_Error
   vTrama = Space$(1024)
   FormaDeCobro = ""
   Select Case FormaDeCobro
      Case "1": vMensaje = "NC. Efectivo"
      Case "2": vMensaje = "NC. Cheque o Deposito"
      Case "3": vMensaje = "NC. Tarjeta"
      Case Else: vMensaje = "NC. Efectivo"
   End Select
   vMonto = 500 * 100
   vCommand = "F2;13;0;" & vMonto & ";;" & Chr$(34) & vMensaje & Chr$(34) & ";"
   vError = SendCommand(vCommand, vTrama)
h_EXIT: On Error GoTo 0
   Exit Sub
h_Error: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sFormaDePagoNotaDeCredito_QPrintMF", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Function fFormaDeCobro(FormaDeCobro As String) As String
   Dim vResult As String
   On Error GoTo h_Error
   Select Case FormaDeCobro
      Case "00001": vResult = "1" 'Efectivo
      Case "00002", "00004":   vResult = "2" 'Cheque
      Case "00003": vResult = "3" 'Tarjeta
      Case Else: vResult = "1"
   End Select
h_EXIT: On Error GoTo 0
   fFormaDeCobro = vResult
   Exit Function
h_Error: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fFormaDeCobro", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fEfectuaVentaQPrintMF(ByVal valDescripcion As String, ByVal valCantidad As String, ByVal valPrecio As String, _
                              ByVal valAlicuota As String, ByVal valTipo As Integer, ByVal valPorcetajeDesRenglon As String, ByVal valEsDevolucion As Boolean) As Boolean

   Dim vCommand  As String
   Dim vError As Long
   Dim vExito As Boolean
   Dim vTrama As String
   Dim vAlicuota As String
   Dim vDescuento As String
   On Error GoTo h_Error
   vTrama = Space$(1024)
   Select Case valAlicuota
      Case 12: vAlicuota = "1"
      Case 22: vAlicuota = "3"
      Case 8: vAlicuota = "2"
      Case 0: vAlicuota = "4"
      Case Else: vAlicuota = "1"
   End Select
   If valEsDevolucion Then
      vCommand = "F2;3;" & Chr$(34) & gTexto.DfLeft(valDescripcion, 24) & Chr$(34) & ";" & valPrecio & ";" & vAlicuota & ";" & valCantidad
      vError = SendCommand(vCommand, vTrama)
      If gTexto.DfLen(valDescripcion) > 24 Then
         sEnviarDatoAdicionalQPrintMF gTexto.DfMid(valDescripcion, 25, gTexto.DfLen(valDescripcion))
      End If
   Else
      vCommand = "F2;3;" & Chr$(34) & gTexto.DfLeft(valDescripcion, 24) & Chr$(34) & ";" & valPrecio & ";" & vAlicuota & ";" & valCantidad
      vError = SendCommand(vCommand, vTrama)
      If gTexto.DfLen(valDescripcion) > 24 Then
         sEnviarDatoAdicionalQPrintMF gTexto.DfMid(valDescripcion, 25, gTexto.DfLen(valDescripcion))
      End If
   End If
   If (vError <> -1) Then
      If valPorcetajeDesRenglon > 0 Then
         'vDescuento = valPorcetajeDesRenglon * 100
         vDescuento = fDarFormatoADescuentoParaImpresion(valPorcetajeDesRenglon)
         vCommand = "F2;6;;1;" & Chr$(34) & "Monto Desc. -" & Chr$(34) & ";" & vDescuento & ";;46;"
         vError = SendCommand(vCommand, vTrama)
      End If
         vExito = True
      Else
         vExito = False
   End If
h_EXIT: On Error GoTo 0
   fEfectuaVentaQPrintMF = vExito
   Exit Function
h_Error:
   fEfectuaVentaQPrintMF = False
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fEfectuaVentaBMC", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Sub sLimpiarPieDePaginaQPrintMF()
   Dim vCommand As String
   Dim vError As String
   Dim vTrama As String
   Dim i As Integer
   On Error GoTo h_Error
   vTrama = Space(1024)
   vCommand = "U5;1"
   vError = SendCommand(vCommand, vTrama)
   For i = 1 To 9
      vCommand = "U9;" & i & ";0;" & Chr$(34) & Chr$(34)
      vError = SendCommand(vCommand, vTrama)
   Next i
      vCommand = "U5;1"
      vError = SendCommand(vCommand, vTrama)
h_EXIT: On Error GoTo 0
   Exit Sub
h_Error: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sLimpiaPieDePaginaQPrintMF", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sImprimeCamposDefiniblesQPrintMF(ByVal valInsDatosImprFiscal As clsDatosImprFiscal)
   Dim vRsCamposDefinibles As ADODB.Recordset
   Dim vText As String
   Dim vOutPut As String
   Dim vCommand As String
   Dim vLinea As Integer
   Dim vError As String
   Dim vTrama As String
   On Error GoTo h_Error
   vTrama = Space(1024)
   sLimpiarPieDePaginaQPrintMF
   vLinea = 1
   If valInsDatosImprFiscal.GetImprimeCamposDefinibles Then
      Set vRsCamposDefinibles = valInsDatosImprFiscal.GetRSCamposDefinibles
      If Not (vRsCamposDefinibles Is Nothing) Then
         If gDbUtil.fRecordCount(vRsCamposDefinibles) > 0 Then
            vRsCamposDefinibles.MoveFirst
            vCommand = "U5;1"
            vError = SendCommand(vCommand, vTrama)
            Do While Not vRsCamposDefinibles.EOF
               vText = gTexto.DfMid(vRsCamposDefinibles.Fields("CampoDefinible").Value, 1, 43)
               If gTexto.DfLen(vText) > 0 Then
                  vCommand = "U9;" & vLinea & ";0;" & Chr$(34) & vText & Chr$(34)
                  vError = SendCommand(vCommand, vTrama)
                  vLinea = vLinea + 1
               End If
               vRsCamposDefinibles.MoveNext
            Loop
            vCommand = "U5;1"
            vError = SendCommand(vCommand, vTrama)
         End If
      End If
      Set vRsCamposDefinibles = Nothing
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_Error: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sImprimeCamposDefiniblesQPrint", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sImprimeTotalesEnDivisaQPrintMF(ByVal valTotales As String)
   Dim vOutPut As String
   Dim vCommand As String
   Dim vLinea As Integer
   Dim vError As String
   Dim vTrama As String
   Dim vList As Variant
   Dim vPos As Integer
   On Error GoTo h_Error
   vTrama = Space(1024)
   sLimpiarPieDePaginaQPrintMF
   vLinea = 1
   vList = gTexto.fConvertCadenaEnArray(valTotales, vbNewLine)
   If UBound(vList) > 0 Then
      vCommand = "U5;1"
      vError = SendCommand(vCommand, vTrama)
      For vPos = LBound(vList) To UBound(vList)
         If LenB(vList(vCount)) > 0 Then
            vCommand = "U9;" & vLinea & ";0;" & Chr$(34) & vList(vPos) & Chr$(34)
            vError = SendCommand(vCommand, vTrama)
         End If
         vLinea = vLinea + 1
      Next
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_Error: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sImprimeTotalesEnDivisaQPrintMF", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Function fObtenerUltimoReporteZ_QPrintMF(ByVal valPuerto As String, ByVal valTipoConexion As enum_TipoConexion, ByVal valIp As String, ByVal valCajaNumero As String) As String
   Dim vError As String
   Dim vTrama As String
   Dim vComando As String
   Dim vUltimoReporteZ As String
   On Error GoTo h_Error
   vTrama = Space$(1024)
   vComando = ESTATUS3
   vUltimoReporteZ = ""
   vError = fAbrirConexion(valTipoConexion, valPuerto, valIp, valCajaNumero)
   vError = SendCommand(vComando, vTrama)
   If (vError <> -1) Then
      vUltimoReporteZ = gTexto.DfMid(vTrama, InStr(vTrama, SEPARADORQF) + 3)
      vUltimoReporteZ = gTexto.DfMid(vUltimoReporteZ, InStr(vUltimoReporteZ, SEPARADORBARRA) + 1)
      vUltimoReporteZ = gTexto.DfMid(vUltimoReporteZ, InStr(vUltimoReporteZ, SEPARADORBARRA) + 1)
      vUltimoReporteZ = gTexto.DfMid(vUltimoReporteZ, InStr(vUltimoReporteZ, SEPARADORBARRA) + 1)
      vUltimoReporteZ = gTexto.DfMid(vUltimoReporteZ, InStr(vUltimoReporteZ, SEPARADORBARRA) + 1)
      vUltimoReporteZ = gTexto.DfMid(vUltimoReporteZ, InStr(vUltimoReporteZ, SEPARADORBARRA) + 1)
      vUltimoReporteZ = gTexto.DfLeft(vUltimoReporteZ, InStr(vUltimoReporteZ, SEPARADORBARRA) - 1)
      refReady = True
   End If
   fObtenerUltimoReporteZ_QPrintMF = vUltimoReporteZ
h_EXIT:       On Error GoTo 0
   vError = fCerrarConexion(valTipoConexion)
   Exit Function
h_Error:
   fObtenerUltimoReporteZ_QPrintMF = ""
   vError = fCerrarConexion(valTipoConexion)
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fObtenerUltimoReporteZ_QPrintMF", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fDarFormatoANumero(ByVal valNumero As String) As String
   Dim vValorFinal As String
   vValorFinal = valNumero
   If gConvert.getSystemDefaultThousandSetting = "," Then
      gTexto.ReemplazaCaracteresEnElString vValorFinal, gConvert.getSystemDefaultThousandSetting, ""
      gTexto.ReemplazaCaracteresEnElString vValorFinal, ".", ","
   Else
      gTexto.ReemplazaCaracteresEnElString vValorFinal, ".", ","
      gTexto.ReemplazaCaracteresEnElString vValorFinal, ".", ""
   End If
   If (Not gTexto.fS1SeEncuentraEnS2(",", vValorFinal)) Then vValorFinal = vValorFinal + ",00"
   fDarFormatoANumero = vValorFinal
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
      End If
      If (gTexto.DfLen(vParteDecimal) < 2) Then vParteDecimal = vParteDecimal + "0"
      If (gTexto.DfLen(vParteDecimal) < 2) Then vParteDecimal = vParteDecimal + "0"
      vValorFinal = vParteEntera + "," + vParteDecimal
      gTexto.ReemplazaCaracteresEnElString vValorFinal, gConvert.getSystemDefaultThousandSetting, ""
      gTexto.ReemplazaCaracteresEnElString vValorFinal, ".", ""
   Else
      vParteEntera = gTexto.fPrimerTokenYLoEliminaDelTexto(vValorFinal, ",")
      vParteDecimal = vValorFinal
      If (gTexto.DfLen(vParteDecimal) < 2) Then vParteDecimal = vParteDecimal + "0"
      If (gTexto.DfLen(vParteDecimal) < 2) Then vParteDecimal = vParteDecimal + "0"
      vValorFinal = vParteEntera + vParteDecimal
      gTexto.ReemplazaCaracteresEnElString vValorFinal, ".", ""
      gTexto.ReemplazaCaracteresEnElString vValorFinal, ".", ""
   End If
   'If (Not gTexto.fS1SeEncuentraEnS2(",", vValorFinal)) Then vValorFinal = vValorFinal + "00"
   fDarFormatoADescuentoParaImpresion = vValorFinal
End Function

Private Function fAnalizarRespuestaDeSendCommand(ByVal valRespuesta As String, ByRef refMensaje As String) As String
   Dim vResultado As String
   On Error GoTo h_Error
   Select Case valRespuesta
      Case "-1": refMensaje = "No se pudo Establecer Conexion"
      Case "0": refMensaje = "Accion realizada con exito"
      Case "1": refMensaje = "Entrada Invalida"
      Case "2": refMensaje = "Hora Invalida"
      Case "3": refMensaje = "Fecha Invalida"
      Case "4": refMensaje = "Articulo Invalido"
      Case "5": refMensaje = "Reporte Desconocido"
      Case "6": refMensaje = "Funcion Invalida"
      Case "7": refMensaje = "Buffer de Transferecia Lleno"
      Case "8": refMensaje = "Transaccion en Curso"
      Case "9": refMensaje = "Cerrado por el Usuario"
      Case "10": refMensaje = "En Proceso de Licitacion"
      Case "11": refMensaje = "Todavia en Proceso de Licitacion"
      Case "12": refMensaje = "Escaneando Error"
      Case "13": refMensaje = "Seleccione Empleado"
      Case "14": refMensaje = "Seleccione Vendedor"
      Case "15": refMensaje = "Articulo no Vendido"
      Case "16": refMensaje = "No Ingreso el Precio del Articulo"
      Case "17": refMensaje = "El Precio del Articulo es Cero (0)"
      Case "18": refMensaje = "Cajon Cerrado"
      Case "19": refMensaje = "Necesita un Gerente"
      Case "20": refMensaje = "El Valor es Muy Alto"
      Case "21": refMensaje = "Descuento no Permitido"
      Case "22": refMensaje = "Correcion no Permitida"
      Case "23": refMensaje = "Ya Realizo un Descuento"
      Case "24": refMensaje = "Ingrese la Cantidad"
      Case "25": refMensaje = "Monto diario Maximo"
      Case "26", "27", "29", "36", "38": refMensaje = "No identificado"
      Case "28": refMensaje = "Empleado equivocado"
      Case "30": refMensaje = "Imprimiendo Factura"
      Case "31": refMensaje = "Fin de Papel"
      Case "32": refMensaje = "Fin de Papel Diario"
      Case "33": refMensaje = "No hay Papel"
      Case "34": refMensaje = "Error de Registro"
      Case "35": refMensaje = "KP Error"
      Case "37": refMensaje = "Buffer de Entrada Lleno"
      Case "39": refMensaje = "Arhivo PLU lleno"
      Case "40": refMensaje = "Volver a Indezar Archivo PLU"
      Case "41": refMensaje = "Se Supero el Tiempo de Espera para la Conexion"
      Case "42": refMensaje = "Vuelva a Intetar MC#"
      Case "43": refMensaje = "Error de Red"
      Case "44": refMensaje = "Aun Esta Abierto el Balance"
      Case "45": refMensaje = "Ingrese el Monto"
      Case "46": refMensaje = "Aun esta en Espera"
      Case "47": refMensaje = "Error de Escala"
      Case "48": refMensaje = "No Esta Autoizado"
      Case "49": refMensaje = "Select Condimen"
      Case "50": refMensaje = "No se Permiten Valores Negativos"
      Case "51": refMensaje = "EFT Comm Error"
      Case "52": refMensaje = "EFT No Aceptado"
      Case "53": refMensaje = "El Subtotal va Antes"
      Case "54": refMensaje = "Enter Covers!"
      Case "55": refMensaje = "Ingrese Numeros!"
      Case "56": refMensaje = "Remueva el Slip"
      Case "57": refMensaje = "Error en el Dispensador"
      Case "58": refMensaje = "Error en el Login"
      Case "59": refMensaje = "Primera Declaracion"
      Case "60": refMensaje = "Se Sobrepaso el Limite del Balance!"
      Case "61": refMensaje = "Seleccione el Peso de la Tara!"
      Case "62": refMensaje = "No se Encontro la Memoria USB"
      Case "63": refMensaje = "No se Encontraron Archivos"
      Case "64": refMensaje = "Se Requiere Firma"
      Case "65": refMensaje = "EJ Error de Escritura"
      Case "66": refMensaje = "EJ Error de Lectura"
      Case "67": refMensaje = "Ha Expirado el Tiempo de Espera"
      Case "68": refMensaje = "Orden muy Grande"
      Case "69": refMensaje = "El Articulo no Esta en el Inventario"
      Case "70": refMensaje = "Seleccione el nivel del Teclado"
      Case "71": refMensaje = "Sistema Ocupado, Intente de Nuevo"
      Case "72": refMensaje = "Archivo de Cupon Lleno"
      Case "73": refMensaje = "Reporte Diario Lleno"
      Case "74": refMensaje = "Error en Impresora Fiscal"
      Case "75": refMensaje = "Ingrese el Codigo de Arrendamiento"
      Case "129": refMensaje = "La Memoria Fiscal Esta Llena"
      Case "130": refMensaje = "Registro Incorrecto en la Memoria Fiscal (Leyendo Registros)"
      Case "131": refMensaje = "No se Detecta Modulo Fiscal"
      Case "132": refMensaje = "Realiza un Reporte Z para Continuar"
      Case "133": refMensaje = "Registro Incorrecto en la Memoria Fiscal (Escribiendo Registros)"
      Case "134": refMensaje = "Sin VAT# Gratis"
      Case "135": refMensaje = "El Modulo Fiscal no esta Limpio"
      Case "136": refMensaje = "El Departamento o PLU Tiene un Numero de Tasa Distinto a 1"
      Case "137": refMensaje = "Sin Espacio Disponible para Cambiar Tasas"
      Case "138": refMensaje = "Sin Espacio Disponible para Liberar RAM"
      Case "139": refMensaje = "El Tiempo para la Programacion Es Menor al Guardado la Ultima Vez en el Modulo Fiscal"
      Case "140": refMensaje = "La Fecha para la Programacion Es Menor al Guardado la Ultima Vez en el Modulo Fiscal"
      Case "141": refMensaje = "Sin Uso"
      Case "142": refMensaje = "No se Permiten Ventas Con Valor 0"
      Case "143": refMensaje = "No se Usa Mas"
      Case "144": refMensaje = "No se Realizaron Operaciones Validas de Venta Antes de Realizar el Reporte Z"
      Case "145": refMensaje = "Sin Espacio de Memoria Disponible para los Contadores"
      Case "146": refMensaje = "Se Intenta Programar Las Tasa Con los Valores ya Asignados"
      Case "147": refMensaje = "Se Intento Cambiar el Modulo Fiscal con Otro"
      Case "148": refMensaje = "Memoria RAM Corrupta"
      Case "149": refMensaje = "Se Intentaron Ventas Con Total 0"
      Case "150": refMensaje = "Se Intento Hacer un Volcado de Memoria con un Rango Invalido de Fecha o Contador"
      Case "151": refMensaje = "Procedimiento Especial de Apertura d Dia"
      Case "152": refMensaje = "No se Permite Ningun Reporte Z si la Tablas estan Abiertas"
      Case "153": refMensaje = "Muestra Error"
      Case "154": refMensaje = "El Numero Maximo de Dueños fue Alcanzado"
      Case "155": refMensaje = "No se Permiten Alicuotas con Valor 0"
      Case "156": refMensaje = "Contraseña para Programacion Invalida"
      Case "157": refMensaje = "Tiene mas de 24h sin Hacer Reporte Z"
      Case "158": refMensaje = "Se ha Desconectado la Impresora"
      Case "159": refMensaje = "Ya se realizo un Reporte Z este Dia"
      Case "160": refMensaje = "Alicuota Preprogramada"
      Case "161": refMensaje = "Conversion no Preprogramada"
      Case "162": refMensaje = "No se ha asignado el ECR"
      Case "163", "164": refMensaje = "Sin Firma Fiscal Guardada en al Impresora Fiscal"
      Case "165": refMensaje = "EJ Externo no Puede Escribir"
      Case "166": refMensaje = "EJ Externo no Puede Leer"
      Case "167": refMensaje = "Sin Puente Cuando Recupere"
      Case "168": refMensaje = "EJ No Limpia Cuando Inicie"
      Case "169": refMensaje = "No Permitido"
      Case "170": refMensaje = "Error en Configuracion Interna"
      Case "171": refMensaje = "Mientras la Impresora Fiscal Estaba Conectada se Cambio la EJ"
      Case "172": refMensaje = "EJ Esta Llena"
      Case "173": refMensaje = "EJ Fuera de Linea"
      Case "174": refMensaje = "EJ Casi Llena"
      Case "175": refMensaje = "No Permitido (Modo No Fiscal)"
      Case "176": refMensaje = "Bateria Baja"
      Case Else: refMensaje = "Error Desconocido"
   End Select
   If (refMensaje = "Accion realizada con exito") Then
      fAnalizarRespuestaDeSendCommand = "0"
   Else: fAnalizarRespuestaDeSendCommand = "1"
   End If
h_EXIT: On Error GoTo 0
   Exit Function
h_Error:
   fAnalizarRespuestaDeSendCommand = "0"
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fAnalizarRespuesta", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fAbrirConexion(ByVal valTipoConexion As enum_TipoConexion, ByVal valPuerto As String, ByVal valIp As String, ByVal valNumeroCaja As String) As String
   Dim vTrama As String
   Dim vError As String
   Dim vTipoConexion As enum_TipoConexion
   Dim vPuerto As String
   Dim vIp As String
   Dim vNumeroCaja As String
   Dim vIp1 As String
   Dim vIp2 As String
   Dim vIp3 As String
   Dim vIp4 As String
   On Error GoTo h_Error
   vTrama = Space$(1024)
   vTipoConexion = valTipoConexion
   vPuerto = valPuerto
   vIp = valIp
   vNumeroCaja = valNumeroCaja
   If vTipoConexion = eTDC_USB Or vTipoConexion = eTDC_PUERTO_SERIAL Then
      vError = OpenComPort(vPuerto, BAUDRATEDEFECTO)
      sRetardoTiempo 10000
      'gMessage.exito "Puerto: " & valPuerto & " USB o SERIAL"
   Else
      'gMessage.exito "LAN: " & valIp
      vIp1 = gTexto.fPrimerTokenYLoEliminaDelTexto(vIp, ".")
      vIp2 = gTexto.fPrimerTokenYLoEliminaDelTexto(vIp, ".")
      vIp3 = gTexto.fPrimerTokenYLoEliminaDelTexto(vIp, ".")
      vIp4 = vIp
      vError = OpenIPPort(vIp1, vIp2, vIp3, vIp4, vNumeroCaja)
      sRetardoTiempo 10000
      'gMessage.exito "Puerto: " & valPuerto & " USB o SERIAL"
   End If
h_EXIT: On Error GoTo 0
   fAbrirConexion = vError
   Exit Function
h_Error:
   fAbrirConexion = "-1"
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fAbrirConexion", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSetConexionPuertoSerial() As String
   Dim vTrama As String
   Dim vError As String
   Dim vComando As String
   On Error GoTo h_Error
   vComando = "F4;9;1;6"
   vTrama = Space$(1024)
   vError = SendCommand(vComando, vTrama)
   fSetConexionPuertoSerial = vError
h_EXIT:    On Error GoTo 0
   Exit Function
h_Error:
   fSetConexionPuertoSerial = "-1"
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSetConexionPuertoSerial", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSetConexionPuertoUSB() As String
   Dim vTrama As String
   Dim vError As String
   Dim vComando As String
   On Error GoTo h_Error
   vComando = "F4;9;16;0"
   vTrama = Space$(1024)
   vError = SendCommand(vComando, vTrama)
   fSetConexionPuertoUSB = vError
h_EXIT:    On Error GoTo 0
   Exit Function
h_Error:
   fSetConexionPuertoUSB = "-1"
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSetConexionPuertoUSB", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSetConexionPuertoLAN(ByVal valIp As String, ByVal valMascara As String, ByVal valGateway As String) As String
   Dim vTrama As String
   Dim vError As String
   Dim vComando As String
   On Error GoTo h_Error
   vComando = "F4;9;255;0;" & Chr$(34) & valIp & Chr$(34) & ";" & Chr$(34) & valMascara & Chr$(34) & ";" & Chr$(34) & valGateway & Chr$(34)
   vTrama = Space$(1024)
   vError = SendCommand(vComando, vTrama)
   fSetConexionPuertoLAN = vError
h_EXIT:    On Error GoTo 0
   Exit Function
h_Error:
   fSetConexionPuertoLAN = "-1"
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSetConexionPuertoLAN", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Sub sSetPuertoSerialDesdeUSBOrLAN(valPuerto As String, valTipoConexion As enum_TipoConexion, ByRef refReady As Boolean, ByVal valIp As String, ByVal valCajaNumero As String)
   Dim vTrama As String
   Dim vError As String
   Dim vComando As String
   On Error GoTo h_Error
   fAbrirConexion valTipoConexion, valPuerto, valIp, valCajaNumero
   If fSetConexionPuertoSerial <> -1 Then
      refReady = True
   Else
      refReady = False
   End If
h_EXIT: On Error GoTo 0
   fCerrarConexion valTipoConexion
   Exit Sub
h_Error:
   refReady = False
   fCerrarConexion valTipoConexion
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSetPuertoSerialDesdeUSBorLAN", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Function sSetPuertoUSBDesdeSerialOrLAN(valPuerto As String, valTipoConexion As enum_TipoConexion, ByRef refReady As Boolean, ByVal valIp As String, ByVal valCajaNumero As String)
   Dim vTrama As String
   Dim vError As String
   Dim vComando As String
   On Error GoTo h_Error
   fAbrirConexion valTipoConexion, valPuerto, valIp, valCajaNumero
   If fSetConexionPuertoUSB <> -1 Then
      refReady = True
   Else
      refReady = False
   End If
h_EXIT: On Error GoTo 0
   fCerrarConexion valTipoConexion
   Exit Function
h_Error:
   refReady = False
   fCerrarConexion valTipoConexion
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSetPuertoSerialDesdeUSBorLAN", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function sSetLANDesdePuertoSerialOrUSB(ByVal valPuerto As String, ByVal valTipoConexion As enum_TipoConexion, ByRef refReady As Boolean, ByVal valIp As String, ByVal valCajaNumero As String, ByVal valGateway As String, ByVal valMascaraSubRed As String)
   Dim vTrama As String
   Dim vError As String
   Dim vComando As String
   On Error GoTo h_Error
   fAbrirConexion eTDC_USB, valPuerto, valIp, valCajaNumero
   If fSetConexionPuertoLAN(valIp, valMascaraSubRed, valGateway) <> -1 Then
      refReady = True
   Else
      refReady = False
   End If
h_EXIT: On Error GoTo 0
   fCerrarConexion eTDC_USB
   Exit Function
h_Error:
   refReady = False
   fCerrarConexion valTipoConexion
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSetPuertoSerialDesdeUSBorLAN", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fCerrarConexion(ByVal valTipoConexion As enum_TipoConexion) As String
   On Error GoTo h_Error
   If valTipoConexion = eTDC_USB Or valTipoConexion = eTDC_PUERTO_SERIAL Then
      fCerrarConexion = CloseComPort
   ElseIf valTipoConexion = eTDC_LAN Then
      fCerrarConexion = CloseIPPort
   End If
h_EXIT: On Error GoTo 0
   Exit Function
h_Error:
   fCerrarConexion = "-1"
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCerrarConexion", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function sCortarPapel(ByVal valTipoConexion As enum_TipoConexion, ByVal valPuerto As String, ByVal valIp As String, ByVal valNumeroCaja As String)
   Dim vError As String
   Dim vTrama As String
   On Error GoTo h_Error
   vTrama = Space$(1024)
   vError = fAbrirConexion(valTipoConexion, valPuerto, valIp, valNumeroCaja)
   vError = SendCommand("F1;5", vTrama)
h_EXIT: On Error GoTo 0
   fCerrarConexion valTipoConexion
   Exit Function
h_Error:
   fCerrarConexion valTipoConexion
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sCortarPapel", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
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
