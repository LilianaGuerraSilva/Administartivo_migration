VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsEnumTablaRetencion"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit

Public Enum enum_TipodePersonaRetencion
   eTP_PJ_DOMICILIADA = 0
   eTP_PJ_NODOMICILIADA
   eTP_PN_RESIDENTE
   eTP_PN_NORESIDENTE
   eTP_TODOS
End Enum

Private Function CM_FILE_NAME() As String
   CM_FILE_NAME = "clsEnumTablaRetencion"
End Function

Private Function CM_MESSAGE_NAME() As String
   CM_MESSAGE_NAME = " Enumatives Ret"
End Function

Public Function enumTipodePersonaRetencionToString(tipodePersonaRetencion As enum_TipodePersonaRetencion, Optional ByVal valUSAReTP_TODOS As Boolean = False) As String
   Select Case tipodePersonaRetencion
      Case eTP_PJ_DOMICILIADA: enumTipodePersonaRetencionToString = "Jurídica Domiciliada"
      Case eTP_PJ_NODOMICILIADA: enumTipodePersonaRetencionToString = "Jurídica No Domiciliada"
      Case eTP_PN_RESIDENTE: enumTipodePersonaRetencionToString = "Natural Residente"
      Case eTP_PN_NORESIDENTE: enumTipodePersonaRetencionToString = "Natural No Residente"
      Case eTP_TODOS
         If valUSAReTP_TODOS Then
            enumTipodePersonaRetencionToString = "TODOS"
         End If
      Case Else
         enumTipodePersonaRetencionToString = enumTipodePersonaRetencionToString(0)
         End Select
End Function

Public Function enumTipodePersonaRetencionToString4(tipodePersonaRetencion As enum_TipodePersonaRetencion) As String
   Select Case tipodePersonaRetencion
      Case eTP_PJ_DOMICILIADA: enumTipodePersonaRetencionToString4 = "PJD"
      Case eTP_PJ_NODOMICILIADA: enumTipodePersonaRetencionToString4 = "PJND"
      Case eTP_PN_RESIDENTE: enumTipodePersonaRetencionToString4 = "PNR"
      Case eTP_PN_NORESIDENTE: enumTipodePersonaRetencionToString4 = "PNNR"
      Case Else
         enumTipodePersonaRetencionToString4 = enumTipodePersonaRetencionToString4(0)
         End Select
End Function

Public Function strTipodePersonaRetencionToNum(ByVal tipodePersonaRetencion As String, _
      Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_TipodePersonaRetencion
   Dim nCount As Integer
   nCount = 0
   For mEnum = eTP_PJ_DOMICILIADA To eTP_PN_NORESIDENTE
      If UCase(tipodePersonaRetencion) = UCase(enumTipodePersonaRetencionToString(mEnum)) Then
         strTipodePersonaRetencionToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strTipodePersonaRetencionToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & tipodePersonaRetencion & " no existe en tipo enumerativo tipodePersonaRetencion"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithTipodePersonaRetencion(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   If TypeOf varComboBox Is ComboBox Then
      varComboBox.Clear
      nCount = 0
      If action = Buscar Then
         varComboBox.Text = gDefGen.getSearchAllComboBox()
         varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
         nCount = nCount + 1
      Else
         varComboBox.Text = enumTipodePersonaRetencionToString(enum_TipodePersonaRetencion.eTP_PJ_DOMICILIADA)
      End If
      varComboBox.List(nCount) = enumTipodePersonaRetencionToString(enum_TipodePersonaRetencion.eTP_PJ_DOMICILIADA)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumTipodePersonaRetencionToString(enum_TipodePersonaRetencion.eTP_PJ_NODOMICILIADA)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumTipodePersonaRetencionToString(enum_TipodePersonaRetencion.eTP_PN_RESIDENTE)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumTipodePersonaRetencionToString(enum_TipodePersonaRetencion.eTP_PN_NORESIDENTE)
      varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
      If action = Buscar Then
         varComboBox.ListIndex = 0
      Else
         varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
      End If
   End If
End Sub

Public Function enumTipodePersonaRetencionToSiglas(tipodePersonaRetencion As enum_TipodePersonaRetencion) As String
   Select Case tipodePersonaRetencion
      Case eTP_PJ_DOMICILIADA: enumTipodePersonaRetencionToSiglas = "PJD"
      Case eTP_PJ_NODOMICILIADA: enumTipodePersonaRetencionToSiglas = "PJND"
      Case eTP_PN_RESIDENTE: enumTipodePersonaRetencionToSiglas = "PNR"
      Case eTP_PN_NORESIDENTE: enumTipodePersonaRetencionToSiglas = "PNNR"
      Case Else
         enumTipodePersonaRetencionToSiglas = enumTipodePersonaRetencionToSiglas(0)
         End Select
End Function

Public Function fEnumTipoDePersonaRetencionSiglasToStringInArray(Optional ByVal valPrepararParaSql As Boolean = False) As Variant
   Dim varEnum As enum_TipodePersonaRetencion
   Dim varArray() As String
   Dim varLimiteSuperior As Integer
   Dim nCount As Integer
   Dim varCaracterDelimitador As String
   On Error Resume Next
   varCaracterDelimitador = ""
   If valPrepararParaSql Then
      varCaracterDelimitador = "'"
   End If
   varLimiteSuperior = enum_TipodePersonaRetencion.eTP_PN_NORESIDENTE - enum_TipodePersonaRetencion.eTP_PJ_DOMICILIADA
   nCount = 0
   ReDim varArray(0 To varLimiteSuperior)
   For varEnum = eTP_PJ_DOMICILIADA To eTP_PN_NORESIDENTE
      varArray(nCount) = varCaracterDelimitador & enumTipodePersonaRetencionToSiglas(varEnum) & varCaracterDelimitador
      nCount = nCount + 1
   Next
   fEnumTipoDePersonaRetencionSiglasToStringInArray = varArray
   On Error GoTo 0
End Function



Public Function fEnumTipoDePersonaRetencionToStringInArray(Optional ByVal valPrepararParaSql As Boolean = False) As Variant
   Dim varEnum As enum_TipodePersonaRetencion
   Dim varArray() As String
   Dim varLimiteSuperior As Integer
   Dim nCount As Integer
   Dim varCaracterDelimitador As String
   On Error Resume Next
   varCaracterDelimitador = ""
   If valPrepararParaSql Then
      varCaracterDelimitador = "'"
   End If
   varLimiteSuperior = enum_TipodePersonaRetencion.eTP_PN_NORESIDENTE - enum_TipodePersonaRetencion.eTP_PJ_DOMICILIADA
   nCount = 0
   ReDim varArray(0 To varLimiteSuperior)
   For varEnum = eTP_PJ_DOMICILIADA To eTP_PN_NORESIDENTE
      varArray(nCount) = varCaracterDelimitador & enumTipodePersonaRetencionToString(varEnum) & varCaracterDelimitador
      nCount = nCount + 1
   Next
   fEnumTipoDePersonaRetencionToStringInArray = varArray
   On Error GoTo 0
End Function

Public Function fAbreviacionTipodePersonaRetToTitulo(ByVal tipodePersonaRetencion As String) As String
   Select Case UCase(tipodePersonaRetencion)
      Case "PJD"
         fAbreviacionTipodePersonaRetToTitulo = enumTipodePersonaRetencionToString(eTP_PJ_DOMICILIADA)
      Case "PNR"
         fAbreviacionTipodePersonaRetToTitulo = enumTipodePersonaRetencionToString(eTP_PN_RESIDENTE)
      Case "PJND"
         fAbreviacionTipodePersonaRetToTitulo = enumTipodePersonaRetencionToString(eTP_PJ_NODOMICILIADA)
      Case "PNNR"
         fAbreviacionTipodePersonaRetToTitulo = enumTipodePersonaRetencionToString(eTP_PN_NORESIDENTE)
      Case Else
         fAbreviacionTipodePersonaRetToTitulo = ""
   End Select
End Function
