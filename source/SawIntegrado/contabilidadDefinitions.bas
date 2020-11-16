Attribute VB_Name = "contabilidadDefinitions"
Option Explicit
Public gContPeriodoActual As clsPeriodoNavigator
Public gContNiveles As clsNiveles
Public gProyParametrosActivo As clsParametrosActivoFijoNav
Public gProyParametrosWinCont As clsParametrosNavigator
Public gProySeguridadPorCompania As clsSeguridadCompaniaNav
Public gContAuditar As clsAuditoriaComprobanteNavigator
Public gProyAuditarProcesos As clsAuditoriaDeProcesosNavigator
Public TrabajandoConSaldoInicialContabilidad As Boolean
Public Sub sInitializeContabilidadClases()
   On Error GoTo h_ERROR
   Set gContPeriodoActual = New clsPeriodoNavigator
   Set gContNiveles = New clsNiveles
   Set gProyParametrosWinCont = New clsParametrosNavigator
   Set gProyParametrosActivo = New clsParametrosActivoFijoNav
   Set gProySeguridadPorCompania = New clsSeguridadCompaniaNav
   Set gContAuditar = New clsAuditoriaComprobanteNavigator
   Set gProyAuditarProcesos = New clsAuditoriaDeProcesosNavigator
   TrabajandoConSaldoInicialContabilidad = False
h_EXIT:
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "contabilidadDefinitions", _
         "sInitializeContabilidadClases", "contabilidadDefinitions", eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Sub DisposeContabilidadClases()
   Set gProyAuditarProcesos = Nothing
   Set gContAuditar = Nothing
   Set gContPeriodoActual = Nothing
   Set gContNiveles = Nothing
   Set gProyParametrosWinCont = Nothing
   Set gProyParametrosActivo = Nothing
   Set gProySeguridadPorCompania = Nothing
End Sub

