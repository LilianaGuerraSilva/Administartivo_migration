VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsRendicionSQL"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit
Private Const CM_FILE_NAME As String = "clsRendicionSQL"
Private Const CM_MESSAGE_NAME As String = "SQL de Rendiciones"

Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function

Public Function fSQLComprobanteDeRendicion(ByVal ConsecutivoRendicion As Long, ByVal valConsecutivoCompania As String) As String
   Dim SQL As String
   Dim valSelectSql As String
   Dim valSQLFromRendicion As String
   
   On Error GoTo h_ERROR
   
   valSelectSql = " SELECT Adm.Rendicion.Consecutivo,Adm.Rendicion.Numero,Adm.Rendicion.FechaCierre, "
   valSelectSql = valSelectSql & " Adm.Rendicion.StatusRendicion"
   valSelectSql = valSelectSql & " , Adm.Rendicion.Descripcion"
   valSelectSql = valSelectSql & " , '('+ Adm.Rendicion.CodigoCuentaBancaria + ') ' + CrossNombreCuentaBancaria.NombreCuenta  AS CodigoCuentaBancaria"
   valSelectSql = valSelectSql & " , Adm.Rendicion.Observaciones "
   valSelectSql = valSelectSql & " , '('+ Adm.Rendicion.codigoCtaBancariaCajaChica + ') ' + CrossNombreCuentaBancariaCajaChica.NombreCuenta AS codigoCtaBancariaCajaChica"
   valSelectSql = valSelectSql & " , Adm.Rendicion.codigoConceptoBancario"
   valSelectSql = valSelectSql & " , Adm.Rendicion.NumeroDocumento"
   valSelectSql = valSelectSql & " , Adm.Rendicion.TotalGastos"
   valSelectSql = valSelectSql & " , CrossNombreCuentaBancaria.Nombre"
   valSelectSql = valSelectSql & " , CrossNombreCuentaBancaria.Simbolo"
   valSelectSql = valSelectSql & " , Adm.DetalleDeRendicion.NumeroDocumento AS NumeroFactura"
   valSelectSql = valSelectSql & " , Adm.DetalleDeRendicion.MontoExento"
   valSelectSql = valSelectSql & " , Adm.DetalleDeRendicion.MontoIVA  +  Adm.DetalleDeRendicion.MontoIVAAlicuotaEspecial1 +  Adm.DetalleDeRendicion.MontoIVAAlicuotaEspecial2 As MontoIVA"
   valSelectSql = valSelectSql & " , Adm.DetalleDeRendicion.MontoRetencion "
   valSelectSql = valSelectSql & " , Adm.DetalleDeRendicion.MontoGravable  + Adm.DetalleDeRendicion.MontoGravableAlicuotaEspecial1 + Adm.DetalleDeRendicion.MontoGravableAlicuotaEspecial2 As MontoGravable,"
   valSelectSql = valSelectSql & " (Adm.DetalleDeRendicion.MontoExento + Adm.DetalleDeRendicion.MontoIVA + Adm.DetalleDeRendicion.MontoGravable + Adm.DetalleDeRendicion.MontoIVAAlicuotaEspecial1 +  Adm.DetalleDeRendicion.MontoIVAAlicuotaEspecial2 "
   valSelectSql = valSelectSql & " + Adm.DetalleDeRendicion.MontoGravableAlicuotaEspecial1 + Adm.DetalleDeRendicion.MontoGravableAlicuotaEspecial2 - Adm.DetalleDeRendicion.MontoRetencion) As MontoTotalFactura, Adm.Rendicion.BeneficiarioCheque "
   
   
   valSQLFromRendicion = " FROM Adm.RENDICION "
 
   SQL = valSelectSql

   SQL = SQL & valSQLFromRendicion
   
   
   SQL = SQL & "OUTER APPLY (SELECT Saw.CuentaBancaria.NombreCuenta ,dbo.Banco.Nombre,Saw.CuentaBancaria.CodigoMoneda,dbo.Moneda.Simbolo  FROM Saw.CuentaBancaria  "
   
   SQL = SQL & "inner join dbo.Banco on (dbo.Banco.Codigo = Saw.CuentaBancaria.codigoBanco) "
   
   SQL = SQL & "inner join dbo.Moneda on (dbo.Moneda.Codigo = Saw.CuentaBancaria.codigoMoneda) "
   
   SQL = SQL & "Where Adm.Rendicion.CodigoCuentaBancaria = Saw.CuentaBancaria.Codigo AND Adm.Rendicion.ConsecutivoCompania = Saw.CuentaBancaria.ConsecutivoCompania) AS CrossNombreCuentaBancaria "
             
             
   SQL = SQL & "OUTER APPLY (SELECT * FROM Saw.CuentaBancaria WHERE Adm.Rendicion.codigoCtaBancariaCajaChica = Saw.CuentaBancaria.Codigo "
   
   SQL = SQL & " AND Adm.Rendicion.ConsecutivoCompania = Saw.CuentaBancaria.ConsecutivoCompania) AS CrossNombreCuentaBancariaCajaChica "

  
   SQL = SQL & " Inner join Adm.DetalleDeRendicion on (Adm.Rendicion.ConsecutivoCompania = Adm.DetalleDeRendicion.ConsecutivoCompania"
    
   SQL = SQL & " And Adm.DetalleDeRendicion.ConsecutivoRendicion = Adm.Rendicion.Consecutivo) "
 
             
             
             
   
   SQL = SQL & " WHERE Adm.Rendicion.Consecutivo = " & gUtilSQL.fNumToStrSQL(ConsecutivoRendicion)

   SQL = SQL & " And Adm.Rendicion.ConsecutivoCompania = " & gUtilSQL.fNumToStrSQL(valConsecutivoCompania)


h_EXIT: On Error GoTo 0
   fSQLComprobanteDeRendicion = SQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLComprobanteDeRendicion", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function


