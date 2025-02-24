VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsImpFiscalEleposVMAX"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Attribute VB_Ext_KEY = "SavedWithClassBuilder6" ,"Yes"
Option Explicit
Private Declare Sub Sleep Lib "kernel32.dll" (ByVal dwMilliseconds As Long)
Private mVmax As VMAXOCX.vMax
Private gUtilImprFiscal As clsUtilImprFiscal
Const mDecimalesParaCantidad As Byte = 3
Const mDecimalesParaMonto As Byte = 2
Const mEnterosParaMonto As Byte = 10
Const mEnterosParaCantidad As Byte = 5
Const mEnterosParaDescuento As Byte = 10
Const mEnterosParaPagos As Byte = 10
Const mEnterosParaIGTF As Byte = 12

Private Function CM_FILE_NAME() As String
   CM_FILE_NAME = "clsImFiscalEleposVMAX"
End Function

Private Function CM_ERROR_MESSAGE(ByVal valErrDescripcion As String) As String
   CM_ERROR_MESSAGE = valErrDescripcion + vbCrLf + "Verificar conexiones y estado de la impresora, cerrar la aplicacíon e intentar nuevamente"
End Function

Private Function CM_MESSAGE_NAME() As String
   CM_MESSAGE_NAME = "Impresora Fiscal Elepos VMAX"
End Function

Private Function GetGender() As Enum_Gender
   GetGender = eg_Female
End Function

Public Sub InitializeValues(ByVal valVmax As Object)
   Set mVmax = valVmax
   Set gUtilImprFiscal = New clsUtilImprFiscal
End Sub

Public Function sAbreCF(ByVal valPuerto As String) As Boolean
   Dim vPuerto As String
   Dim vEstado As Boolean
   Dim mMensaje As String
   Dim vRespuesta As Boolean
   On Error GoTo h_Error
   mVmax.NPuerto = gConvert.ConvierteAInteger(valPuerto)
   mVmax.NPuertoVisor = "1"
   vRespuesta = mVmax.AbrirPuerto
   Sleep 500
   sAbreCF = vRespuesta
h_EXIT: On Error GoTo 0
   Exit Function
h_Error:
   sAbreCF = False
    Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(CM_ERROR_MESSAGE(Err.Description), CM_FILE_NAME, "sAbreCF", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Sub sCierraCF()
   On Error GoTo h_Error
   mVmax.CerrarPuerto
   Sleep 500
   Set mVmax = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_Error: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(CM_ERROR_MESSAGE(Err.Description), CM_FILE_NAME, "sCierraCF", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Function fSerialMemoriaFiscal(ByRef refReady As Boolean, valPuerto As String, ByVal valImpresora As Enum_ImpresorasFiscales) As String
   Dim vCmd As String
   Dim vSerial As String
   Dim vCadena As String
   Dim vStatus As Long
   Dim vError As Long
   Dim vResultado As Boolean
   On Error GoTo h_Error
   
   If sAbreCF(valPuerto) Then
      vResultado = mVmax.ObtDatosSerializacion
      refReady = vResultado
      If vResultado Then
         vSerial = mVmax.s_serial
      Else
         gMessage.Advertencia "Error de comunicación"
      End If
   Else
      gMessage.Advertencia "Error Al Abrir el puerto de comunicaciones"
   End If
   sCierraCF
h_EXIT: On Error GoTo 0
   fSerialMemoriaFiscal = vSerial
   Exit Function
h_Error:
   fSerialMemoriaFiscal = ""
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(CM_ERROR_MESSAGE(Err.Description), CM_FILE_NAME, "fSerialMemoriaFiscal", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fUltimoNumeroMemoriaFiscal(ByVal valPuerto As String, ByVal valImpresora As Enum_ImpresorasFiscales, ByVal valEsNotaDeCredito As Boolean, ByRef refComandoEjecutado As Boolean) As String
   Dim vResultado As Boolean
   Dim vUltimo As String
   
   On Error GoTo h_Error
   If sAbreCF(valPuerto) Then
      vResultado = mVmax.LeeDatosFiscales
      If vResultado Then
         refComandoEjecutado = vResultado
         If valEsNotaDeCredito Then
             vUltimo = mVmax.UltimaNCAbierta
         Else
            vUltimo = mVmax.UltimoCFAbierto
         End If
            vUltimo = gTexto.llenaConCaracterALaIzquierda(vUltimo, "0", 8)
      Else
         gMessage.Advertencia "Error de comunicación"
      End If
   Else
      gMessage.Advertencia "Error Al Abrir el puerto de comunicaciones"
   End If
   sCierraCF
h_EXIT: On Error GoTo 0
   fUltimoNumeroMemoriaFiscal = vUltimo
   Exit Function
h_Error:
   mVmax.CerrarPuerto
   fUltimoNumeroMemoriaFiscal = ""
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(CM_ERROR_MESSAGE(Err.Description), CM_FILE_NAME, "fUltimoNumeroMemoriaFiscal", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Sub sCierreZ(ByRef refReady As Boolean, valPuerto As String)
   Dim vResult As Boolean
   Dim vCmd As String
   
   On Error GoTo h_Error
   vCmd = "Z"
   If sAbreCF(valPuerto) Then
      vResult = mVmax.CierreDiario(vCmd)
      refReady = vResult
   End If
   sCierraCF
h_EXIT: On Error GoTo 0
   Exit Sub
h_Error:
mVmax.CerrarPuerto
Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sCierreZ", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Function fObtenerUltimoNumeroDeReporteZ(ByVal valPuerto As String) As String
   Dim vResult As Boolean
   Dim vLeeZ As String
   Dim vCmd As String
   
   On Error GoTo h_Error
   If sAbreCF(valPuerto) Then
      mVmax.LeeZ
      Sleep 500
      vLeeZ = mVmax.rNumZ
      If gConvert.ConvierteAInteger(vLeeZ) = 0 Then
         vLeeZ = gConvert.fConvierteAString("1")
      End If
      vLeeZ = gTexto.llenaConCaracterALaIzquierda(vLeeZ, "0", 8)
      fObtenerUltimoNumeroDeReporteZ = vLeeZ
      sCierraCF
   End If
h_EXIT: On Error GoTo 0
   Exit Function
h_Error:
mVmax.CerrarPuerto
Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(CM_ERROR_MESSAGE(Err.Description), CM_FILE_NAME, "fObtenerUltimoNumeroDeReporteZ", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Sub sCierreX(ByRef refReady As Boolean, ByVal valPuerto As String)
  Dim vResult As Boolean
   Dim vCmd As String
   
   On Error GoTo h_Error
   vCmd = "X"
   If sAbreCF(valPuerto) Then
      vResult = mVmax.CierreDiario(vCmd)
      refReady = vResult
   End If
   sCierraCF
h_EXIT: On Error GoTo 0
   Exit Sub
h_Error:
mVmax.CerrarPuerto
Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sCierreX", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Function fCalcularMontoDescuento(ByVal valMonto As Currency, ByVal valPorcDescuento As Currency, ByVal valCantidad As Currency) As Currency
   Dim vResultado As Currency
   vResultado = valMonto * valCantidad - (valMonto * valCantidad * valPorcDescuento / 100)
   vResultado = gUtilMathOperations.fRedondeaANDecimales(vResultado, mDecimalesParaMonto)
   fCalcularMontoDescuento = vResultado
End Function

Public Sub sImprimeVentaArticulo(ByVal valEnumPrinter As Enum_ImpresorasFiscales, insDatosImprFiscal As clsDatosImprFiscal, ByRef refReady As Boolean)
      Dim rsDatosArticulo As ADODB.Recordset
      Dim vAlicuotas As String
      Dim vTipoTasa As enum_TipoDeAlicuota
      Dim vCantidades As Currency
      Dim vPrecios As Currency
      Dim vDescripcion As String
      Dim vPorcentajeDescRenglon As Currency
      Dim vMontoDescuentoRenglon As Currency
      Dim vMontoDescuentoTotal As Currency
      Dim vPorcetajeDesTotal As Currency
      Dim vPrecioFormateado As String
      Dim vCantidadFormateada As String
      Dim vMontoDescuentoAlicuotaG As Currency
      Dim vMontoDescuentoAlicuotaR As Currency
      Dim vMontoDescuentoAlicuotaA As Currency
      Dim vMontoDescuentoAlicuotaE As Currency
      Dim vMontoDesctotal As Currency
      Dim vResult As Boolean
      Dim vMontoDescuentoFormateado As String
      Dim vSerial As String
      Dim vRollo As String
      Dim vTotalFactura As String
      Dim vList As Variant
      Dim vCount As Integer
      Dim vTotales As String
      Dim vTotalPagoME As Currency
      Dim vCantidadCaracteres As Integer
      Dim vCaracteresEspacio As String
      Dim vMontoMEStr As String
      On Error GoTo h_Error
      Set rsDatosArticulo = insDatosImprFiscal.GetItems
      If (sAbreCF(insDatosImprFiscal.GetPuertoImpresoraFiscal)) Then
         If fImprimeDatosClientes(insDatosImprFiscal, valEsNotaDeCredito) Then
            vResult = True
            vPorcetajeDesTotal = insDatosImprFiscal.GetPorcentajeDeDescuento
            If Not rsDatosArticulo.BOF And Not rsDatosArticulo.EOF Then
               rsDatosArticulo.MoveFirst
               Do While Not rsDatosArticulo.EOF
                  vTipoTasa = gConvert.charAEnumerativoInt(rsDatosArticulo.Fields("TipoAlicuota").Value)
                  vAlicuotas = fDarFormatoAlicuotaIva(vTipoTasa)
                  vCantidades = gConvert.fConvierteACurrency(fSetDecimalSeparator(rsDatosArticulo.Fields("Cantidad").Value))
                  vPrecios = gConvert.fConvierteACurrency(fSetDecimalSeparator(rsDatosArticulo.Fields("PrecioSinIVA").Value))
                  vPorcentajeDescRenglon = gConvert.fConvierteACurrency(fSetDecimalSeparator(rsDatosArticulo.Fields("Descuento").Value))
                  vDescripcion = gUtilImprFiscal.fCadenaCaracteresValidos(rsDatosArticulo.Fields("Descripcion").Value, 100)
                  vCantidades = Abs(vCantidades)
                  vPrecios = Abs(vPrecios)
                  vMontoDescuentoRenglon = fCalcularMontoDescuento(vPrecios, vPorcentajeDescRenglon, vCantidades)
                  vMontoDesctotal = fCalcularMontoDescuentoTotal(vMontoDescuentoRenglon, vCantidades, vPrecios, vPorcetajeDesTotal)
                  vMontoDescuentoRenglon = (vPrecios * vCantidades) - vMontoDescuentoRenglon
                  sTotalizarDescuentoGlobalPorAlicuota vAlicuotas, vMontoDesctotal, vMontoDescuentoAlicuotaG, vMontoDescuentoAlicuotaA, vMontoDescuentoAlicuotaR, vMontoDescuentoAlicuotaE
                  vMontoDescuentoFormateado = fDarFormatoNumericoParaImpresion(vMontoDescuentoRenglon, mEnterosParaMonto, mDecimalesParaMonto)
                  vCantidadFormateada = fDarFormatoNumericoParaImpresion(vCantidades, mEnterosParaCantidad, mDecimalesParaCantidad)
                  vPrecioFormateado = fDarFormatoNumericoParaImpresion(vPrecios, mEnterosParaMonto, mDecimalesParaMonto)
                  vSerial = gUtilImprFiscal.fCadenaCaracteresValidos(rsDatosArticulo.Fields("Serial"), 100)
                  vRollo = gUtilImprFiscal.fCadenaCaracteresValidos(rsDatosArticulo.Fields("Rollo"), 100)
                  sAgregaLineaSeparacion
                  If Not fVenta(vDescripcion, vCantidadFormateada, vPrecioFormateado, vAlicuotas, valEsNotaDeCredito, vMontoDescuentoFormateado) Then
                     fCancelaCuponFiscal False, insDatosImprFiscal.GetPuertoImpresoraFiscal
                     mVmax.CerrarPuerto
                     GoTo h_Error
                  End If
                  If vSerial <> "" Then
                     mVmax.TextoDNF (gTexto.DfMid(vSerial, 1, 38))
                  End If
                  If vRollo <> "" Then
                     mVmax.TextoDNF (gTexto.DfMid(vRollo, 1, 38))
                  End If
                 rsDatosArticulo.MoveNext
               Loop
            End If
            If vPorcetajeDesTotal <> 0 Then
                sAplicarDescuento vMontoDescuentoAlicuotaA, vMontoDescuentoAlicuotaG, vMontoDescuentoAlicuotaR, vMontoDescuentoAlicuotaE
            End If
            'Aqui se Aplica el IGTF
            vTotalPagoME = insDatosImprFiscal.GetBaseImponibleIGTF
            Dim vTotalPagadoML As Currency
            vTotalPagadoML = fBuscarYSumarPagosML(insDatosImprFiscal)
            If vTotalPagoME > 0 Then
               Dim vBaseIGTF As String
               vBaseIGTF = fDarFormatoNumericoParaImpresion(vTotalPagoME, mEnterosParaIGTF, mDecimalesParaMonto)
               vResult = mVmax.SubTotal_ConRetornoT(vBaseIGTF)
               vMontoMEStr = gConvert.FormatoNumerico(vTotalPagoME, False)
               vCantidadCaracteres = 33 - gTexto.DfLen(vMontoMEStr) - 2
               vCaracteresEspacio = gTexto.fNBlancos(vCantidadCaracteres)
               vMontoMEStr = "Divisas" & vCaracteresEspacio & vMontoMEStr
               vResult = mVmax.TextoDNF(vMontoMEStr)
               If vTotalPagadoML = 0 Then
                  Dim vMedioPago As String
                  Dim vOtrosPagos As Currency
                  vMedioPago = "Otros"
                  vOtrosPagos = Abs(insDatosImprFiscal.GetTotalAPagar) - Abs(insDatosImprFiscal.GetBaseImponibleIGTF)
                  vMontoMEStr = fDarFormatoNumericoParaImpresion(vOtrosPagos, mEnterosParaIGTF, mDecimalesParaMonto)
                  mVmax.PagoCF vMedioPago, vMontoMEStr, 1
               End If
            Else
               If Not insDatosImprFiscal.GetFormaDePago Is Nothing Then
                  sFormaDePago insDatosImprFiscal, True
               Else
                  vResult = mVmax.PagoCF("Debito", vTotalFactura, 1)
               End If
               vResult = mVmax.SubTotal()
            End If
            vTotalFactura = mVmax.TotalFactura
            If vTotalPagadoML > 0 Then
               If Not insDatosImprFiscal.GetFormaDePago Is Nothing Then
                  sFormaDePago insDatosImprFiscal
               Else
                  vResult = mVmax.PagoCF("Debito", vTotalFactura, 1)
               End If
            End If
         End If
         vTotales = insDatosImprFiscal.GetTotalesMonedaExtranjera
         If gTexto.DfLen(vTotales) > 0 And Not vEsNotaDeCredito Then
            vList = gTexto.fConvertCadenaEnArray(vTotales, vbNewLine)
            If UBound(vList) > 0 Then
               For vCount = LBound(vList) To UBound(vList)
                  If LenB(vList(vCount)) > 0 Then
                     vResult = mVmax.TextoDNF(vList(vCount))
                  End If
               Next
            End If
        Else
            sImprimeCamposDefinibles insDatosImprFiscal
        End If
        vResult = mVmax.CerrarCF
        Set rsDatosArticulo = Nothing
      End If
      refReady = vResult
h_EXIT: On Error GoTo 0
   Exit Sub
h_Error:
   refReady = False
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(CM_ERROR_MESSAGE(Err.Description), CM_FILE_NAME, "sImprimeVentaArticulo_SANSSUMG", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Function fImprimeSeialyRollo(ByVal valSerial As String, ByVal valRollo As String) As String
   Dim vDescripcion As String
   If valSerial <> "" Then
      vDescripcion = vDescripcion & " " & valSerial
   End If
   If valRollo <> "" Then
      vDescripcion = vDescripcion & " " & valRollo
   End If
   fImprimeSeialyRollo = vDescripcion
End Function

Private Sub sTotalizarDescuentoGlobalPorAlicuota(ByVal valAlicuotas As String, ByVal valMontoDesctotal As Currency, ByRef refMontoDescuentoAlicuotaG As Currency, ByRef refMontoDescuentoAlicuotaA As Currency, ByRef refMontoDescuentoAlicuotaR As Currency, ByRef refMontoDescuentoAlicuotaE As Currency)
   If gTexto.fS1SeEncuentraEnS2("G", valAlicuotas) Then
      refMontoDescuentoAlicuotaG = refMontoDescuentoAlicuotaG + valMontoDesctotal
   ElseIf gTexto.fS1SeEncuentraEnS2("R", valAlicuotas) Then
      refMontoDescuentoAlicuotaR = refMontoDescuentoAlicuotaR + valMontoDesctotal
   ElseIf gTexto.fS1SeEncuentraEnS2("A", valAlicuotas) Then
      refMontoDescuentoAlicuotaA = refMontoDescuentoAlicuotaA + valMontoDesctotal
   ElseIf gTexto.fS1SeEncuentraEnS2("E", valAlicuotas) Then
      refMontoDescuentoAlicuotaE = refMontoDescuentoAlicuotaE + valMontoDesctotal
   End If
End Sub

Private Sub sAplicarDescuento(ByVal valMontoDescuentoAlicuotaA As Currency, ByVal valMontoDescuentoAlicuotaG As Currency, ByVal valMontoDescuentoAlicuotaR As Currency, ByVal valMontoDescuentoAlicuotaE As Currency)
   Dim vDescuentoAlicuotaA As String
   Dim vDescuentoAlicuotaG As String
   Dim vDescuentoAlicuotaR As String
   Dim vDescuentoAlicuotaE As String
   Dim vDescuentoAlicuotaP As String
   vDescuentoAlicuotaP = "0.00"
   vDescuentoAlicuotaA = fDarFormatoNumericoParaImpresion(gConvert.fConvierteAString(valMontoDescuentoAlicuotaA), mEnterosParaDescuento, mDecimalesParaMonto)
   vDescuentoAlicuotaG = fDarFormatoNumericoParaImpresion(gConvert.fConvierteAString(valMontoDescuentoAlicuotaG), mEnterosParaDescuento, mDecimalesParaMonto)
   vDescuentoAlicuotaR = fDarFormatoNumericoParaImpresion(gConvert.fConvierteAString(valMontoDescuentoAlicuotaR), mEnterosParaDescuento, mDecimalesParaMonto)
   vDescuentoAlicuotaE = fDarFormatoNumericoParaImpresion(gConvert.fConvierteAString(valMontoDescuentoAlicuotaE), mEnterosParaDescuento, mDecimalesParaMonto)
   mVmax.DescuentoCF "Dcto Global", vDescuentoAlicuotaE, vDescuentoAlicuotaG, vDescuentoAlicuotaR, vDescuentoAlicuotaA, vDescuentoAlicuotaP
End Sub

Private Function fCalcularMontoDescuentoTotal(ByVal valMontoDescuento As Currency, ByVal valCantidad As Currency, ByVal valMonto As Currency, ByVal valPrcDescuentoTotal As Currency) As Currency
   Dim vMontoDescuentoTotal As Currency
   vMontoDescuentoTotal = valMontoDescuento * valPrcDescuentoTotal / 100
   vMontoDescuentoTotal = gUtilMathOperations.fRedondeaANDecimales(vMontoDescuentoTotal, mDecimalesParaMonto)
   fCalcularMontoDescuentoTotal = vMontoDescuentoTotal
End Function

Public Function fVenta(ByVal valDescripcion As String, ByVal valCantidad As String, ByVal valPrecio As String, ByVal valAlicuota As String, ByVal valEsNotaDeCredito As Boolean, ByVal valDectoRenglon As String) As Boolean
   Dim vRealizoVenta As Boolean
   Dim vDescripcionDescuento As String
   
   If valEsNotaDeCredito Then
      vRealizoVenta = mVmax.ItemDev(valDescripcion, valCantidad, valPrecio, valAlicuota, 1)
   Else
      vRealizoVenta = mVmax.Item(valDescripcion, valCantidad, valPrecio, valAlicuota, 1)
   End If
   
   If gConvert.fConvertStringToCurrency(valDectoRenglon) > 0 Then
     vDescripcionDescuento = "Dcto En Articulo "
     vRealizoVenta = vRealizoVenta And mVmax.ItemDesc(vDescripcionDescuento, valDectoRenglon, valAlicuota, 0)
   End If
   fVenta = vRealizoVenta
End Function

Private Function fDarFormatoAlicuotaIva(ByVal valTipoAlicuota As enum_TipoDeAlicuota) As String
   Dim vResult  As String
   Select Case valTipoAlicuota
   Case enum_TipoDeAlicuota.eTD_ALICUOTAGENERAL: vResult = "G"
   Case enum_TipoDeAlicuota.eTD_ALICUOTA2: vResult = "R"
   Case enum_TipoDeAlicuota.eTD_ALICUOTA3: vResult = "A"
   Case enum_TipoDeAlicuota.eTD_EXENTO: vResult = "E"
   End Select
   fDarFormatoAlicuotaIva = vResult
End Function

Private Function fImprimeDatosClientes(ByVal insDatosImprFiscal As clsDatosImprFiscal) As Boolean
   Dim vResult As Boolean
   Dim vTipo As String
   Dim vRazonSocial As String
   Dim vRif As String
   Dim vDireccion As String
   Dim vObservaciones As String
   Dim vNumFactura As String
   Dim vSerialMaquinaFiscal As String
   Dim vFechaEmision As String
   Dim vHoraEmision As String
   Dim vDia As String
   Dim vMes As String
   Dim vAno As String
   Dim vHora As String
   Dim vMin As String
   Dim vSubstring As String
   Dim vEsNotaDeCredito As Boolean
         
   On Error GoTo h_Error
   vEsNotaDeCredito = insDatosImprFiscal.GetEsNotaDeCredito
   vRazonSocial = gUtilImprFiscal.fCadenaCaracteresValidos(insDatosImprFiscal.GetNombreCliente, 100)
   vRif = insDatosImprFiscal.GetNumeroRIFCliente
   vDireccion = gUtilImprFiscal.fCadenaCaracteresValidos("Direccion: " & insDatosImprFiscal.GetDireccion, 100)
   vObservaciones = gUtilImprFiscal.fCadenaCaracteresValidos("Observaciones: " & insDatosImprFiscal.GetObservaciones, 100)
   If vEsNotaDeCredito Then
      vTipo = "D"
      vNumFactura = insDatosImprFiscal.GetNumeroTicket
      vSerialMaquinaFiscal = insDatosImprFiscal.GetSerialImpresoraFiscal
      vFechaEmision = insDatosImprFiscal.GetFecha
      vDia = gTexto.DfMid(vFechaEmision, 1, 2)
      vMes = gTexto.DfMid(vFechaEmision, 4, 2)
      vAno = gTexto.DfMid(vFechaEmision, 9, 2)
      vFechaEmision = vDia + vMes + vAno
      vHoraEmision = insDatosImprFiscal.GetHoraModificacion
      vHora = gTexto.DfMid(vHoraEmision, 1, 2)
      vMin = gTexto.DfMid(vHoraEmision, 4, 2)
      vHoraEmision = vHora + vMin
   Else
      vTipo = "0"
      vNumFactura = ""
      vSerialMaquinaFiscal = ""
      vFechaEmision = ""
      vHoraEmision = ""
   End If
   vRazonSocial = gTexto.DfMid(vRazonSocial, 1, 100)
   mVmax.AbrirCF vRazonSocial, vRif, vTipo, vNumFactura, vSerialMaquinaFiscal, vFechaEmision, vHoraEmision
   vSubstring = gTexto.DfMid(vDireccion, 1, 40)
   mVmax.TextoDNF vSubstring
   vSubstring = gTexto.DfMid(vDireccion, 41, 40)
   If vSubstring <> "" Then
   mVmax.TextoDNF vSubstring
   End If
   vSubstring = gTexto.DfMid(vDireccion, 81, 40)
   If vSubstring <> "" Then
   mVmax.TextoDNF vSubstring
   End If
   vSubstring = gTexto.DfMid(vObservaciones, 1, 40)
   If vSubstring <> "" Then
      sAgregaLineaSeparacion
      mVmax.TextoDNF vSubstring
      vSubstring = gTexto.DfMid(vObservaciones, 41, 40)
      If vSubstring <> "" Then
      mVmax.TextoDNF vSubstring
      End If
      vSubstring = gTexto.DfMid(vObservaciones, 81, 40)
      If vSubstring <> "" Then
      mVmax.TextoDNF vSubstring
      End If
   End If
   fImprimeDatosClientes = True
h_EXIT:    On Error GoTo 0
   Exit Function
h_Error:
   fImprimeDatosClientes = False
   mVmax.CerrarPuerto
Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fImprimeDatosClientes", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Sub sAgregaLineaSeparacion()
   On Error GoTo h_Error
   Dim vSubstring As String
   vSubstring = gTexto.LlenaConCaracterALaDerecha(vSubstring, "-", 40)
   mVmax.TextoDNF vSubstring
h_EXIT:    On Error GoTo 0
   Exit Sub
h_Error:
Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sAgregaLineaSeparacion", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Function fFormaDeCobro(ByVal valFormaDeCobro As String, ByVal valDescripcionPagoCreditoElectronico As String) As String
   Dim vValorFinal As String
   Select Case valFormaDeCobro
      Case "00001":    vValorFinal = "Efectivo"
      Case "00002":    vValorFinal = "Cheque"
      Case "00003":    vValorFinal = "Tarjeta"
      Case "00004":    vValorFinal = "Deposito"
      Case "00015":    vValorFinal = valDescripcionPagoCreditoElectronico
      Case Else: vValorFinal = "Efectivo"
   End Select
   fFormaDeCobro = vValorFinal
End Function

Private Sub sFormaDePago(ByVal insDatosImprFiscal As clsDatosImprFiscal, ByVal valEsME As Boolean)
   Dim rsFormaDePago As ADODB.Recordset
   Dim formadePago As String
   Dim vMontoCancelado As Currency
   Dim MontoCanceladoFormateado As String
   Dim vTotalFactura As String
   Dim vResult As Boolean
   Dim vFormaDeCobro As String
   Dim vCodigoMoneda As String
   On Error GoTo h_Error
   Set rsFormaDePago = insDatosImprFiscal.GetFormaDePago
   vTotalFactura = mVmax.TotalFactura
   If gDbUtil.fRecordCount(rsFormaDePago) > 0 Then
      rsFormaDePago.MoveFirst
      Do While Not rsFormaDePago.EOF
         vMontoCancelado = gConvert.fConvierteAString(rsFormaDePago.Fields("Monto").Value)
         vMontoCancelado = Abs(vMontoCancelado)
         vCodigoMoneda = rsFormaDePago.Fields("CodigoMoneda").Value
         vFormaDeCobro = fFormaDeCobro(rsFormaDePago("CodigoFormaDelCobro").Value, insDatosImprFiscal.GetDescripcionPagoCreditoElectronico)
         If rsFormaDePago.RecordCount < 1 Then
             vResult = mVmax.PagoCF("Efectivo", vTotalFactura, 1)
         ElseIf insDatosImprFiscal.GetCodigoMonedaCompania <> vCodigoMoneda And valEsME Then
            MontoCanceladoFormateado = gConvert.fConvierteAString(vMontoCancelado)
            MontoCanceladoFormateado = fDarFormatoNumericoParaImpresion(MontoCanceladoFormateado, mEnterosParaPagos, mDecimalesParaMonto)
            vResult = mVmax.PagoCF(vFormaDeCobro, MontoCanceladoFormateado, 1)
         ElseIf insDatosImprFiscal.GetCodigoMonedaCompania = vCodigoMoneda Then
            MontoCanceladoFormateado = gConvert.fConvierteAString(vMontoCancelado)
            MontoCanceladoFormateado = fDarFormatoNumericoParaImpresion(MontoCanceladoFormateado, mEnterosParaPagos, mDecimalesParaMonto)
            vResult = mVmax.PagoCF(vFormaDeCobro, MontoCanceladoFormateado, 1)
         End If
         rsFormaDePago.MoveNext
      Loop
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_Error:
   mVmax.CerrarPuerto
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sFormaDePago", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sImprimeCamposDefinibles(insDatosImprFiscal As clsDatosImprFiscal)
   Dim rsCamposDefinibles As ADODB.Recordset
   Dim vCampoDef As String
   Dim vResult As Boolean
   On Error GoTo h_Error
   Set rsCamposDefinibles = insDatosImprFiscal.GetRSCamposDefinibles
   If gDbUtil.fRecordCount(rsCamposDefinibles) > 0 Then
      rsCamposDefinibles.MoveFirst
      Do While Not rsCamposDefinibles.EOF
         sAgregaLineaSeparacion
         vCampoDef = rsCamposDefinibles.Fields("CampoDefinible").Value
         vCampoDef = gUtilImprFiscal.fCadenaCaracteresValidos(gTexto.DfMid(vCampoDef, 1, 40), 40)
         vResult = mVmax.TextoDNF(vCampoDef)
         rsCamposDefinibles.MoveNext
      Loop
   Else
      Exit Sub
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_Error:
mVmax.CerrarPuerto
Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sImprimeCamposDefinibles", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Function fCancelaCuponFiscal(ByVal valAbrirElPuerto As Boolean, ByVal valPuerto As String) As Boolean
   Dim vExito As Boolean
   If valAbrirElPuerto Then
      mVmax.AbrirPuerto
   End If
   mVmax.SoftReset          ' Borrar cualquier estado erroneo de la impresora
   Sleep 6500               ' Espera para reestablecer la impresora
   vExito = mVmax.CancelaCF ' Cancela una factura abierta en cola
   If valAbrirElPuerto Then
      mVmax.CerrarPuerto
   End If
   fCancelaCuponFiscal = True
h_EXIT: On Error GoTo 0
   Exit Function
h_Error:
   fCancelaCuponFiscal = False
   mVmax.CerrarPuerto
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCancelaCuponFiscal", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

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
   fSetDecimalSeparator = vResult
h_EXIT:    On Error GoTo 0
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

Private Function fBuscarYSumarPagosML(ByVal valDatosImprFiscal As clsDatosImprFiscal) As Currency
   Dim vResult As Currency
   Dim vCodigoMoneda As String
   Dim vPagosRS As ADODB.Recordset
   On Error GoTo h_Error
   vResult = 0
   Set vPagosRS = valDatosImprFiscal.GetFormaDePago
   If gDbUtil.fRecordCount(vPagosRS) > 0 Then
      vPagosRS.MoveFirst
      Do While Not vPagosRS.EOF
         vCodigoMoneda = vPagosRS("CodigoMoneda").Value
         If vCodigoMoneda = valDatosImprFiscal.GetCodigoMonedaCompania Then
            vResult = vResult + gConvert.fConvierteACurrency(vPagosRS("CodigoMoneda").Value)
         End If
         vPagosRS.MoveNext
      Loop
   End If
   Set vPagosRS = Nothing
   fBuscarYSumarPagosML = vResult
h_EXIT: On Error GoTo 0
   Exit Function
h_Error:
   fBuscarYSumarPagosML = 0
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fBuscarYSumarPagosML", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
