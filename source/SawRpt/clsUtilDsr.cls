VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsUtilDsr"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit
Private Const CM_FILE_NAME As String = "clsUtilDsr"
Private Const CM_MESSAGE_NAME As String = "Util Dsr"

Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function

Public Function enumDiaDeLaSemanaToString(DiaDeLaSemana As enum_DiaDeLaSemana) As String
   Select Case DiaDeLaSemana
      Case eDD_LUNES: enumDiaDeLaSemanaToString = "Lunes"
      Case eDD_MARTES: enumDiaDeLaSemanaToString = "Martes"
      Case eDD_MIERCOLES: enumDiaDeLaSemanaToString = "Miércoles"
      Case eDD_JUEVES: enumDiaDeLaSemanaToString = "Jueves"
      Case eDD_VIERNES: enumDiaDeLaSemanaToString = "Viernes"
      Case eDD_SABADO: enumDiaDeLaSemanaToString = "Sábado"
      Case eDD_DOMINGO: enumDiaDeLaSemanaToString = "Domingo"
      Case Else: enumDiaDeLaSemanaToString = enumDiaDeLaSemanaToString(0)
   End Select
End Function


