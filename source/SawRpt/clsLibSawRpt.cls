VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsLibSawRpt"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit

Public Function GetVersionNumber() As String
   GetVersionNumber = "0.01"
End Function

Public Sub sInitializeGlobalValues(ByRef refProyParametros As Object, ByVal valEsSistemaInfotax As Boolean, ByRef refProyParametrosCompania As Object)
   SetProyParametros refProyParametros
   SetEsSistemaInfotax valEsSistemaInfotax
   SetProyParametrosCompania refProyParametrosCompania
End Sub
