SET DATEFORMAT dmy
--SET LANGUAGE spanish

DECLARE @Compania AS int

SET @Compania = 1

----SELECT TOP 1 CambioABolivares FROM Cambio WHERE CodigoMoneda = 'USD' AND FechaDeVigencia <= cxC.Fecha

/* Cuentas por Cobrar 
"CuentasPorCobrar"
*/
;WITH CTE_CxC AS (
SELECT 
	Numero,
	MONTH(Fecha) AS mesCxC,
	ROUND((CASE WHEN FechaVencimiento >= GETDATE() THEN ((MontoExento + MontoGravado + MontoIVA) - MontoAbonado) ELSE 0 END)/ ISNULL(CASE WHEN CodigoMoneda = 'VED' THEN (SELECT TOP 1 CambioABolivares FROM Cambio WHERE CodigoMoneda = 'USD' AND FechaDeVigencia <= Fecha) ELSE 1 END, 1), 2) AS montoVigente,
	ROUND((CASE WHEN FechaVencimiento <  GETDATE() THEN ((MontoExento + MontoGravado + MontoIVA) - MontoAbonado) ELSE 0 END)/ ISNULL(CASE WHEN CodigoMoneda = 'VED' THEN (SELECT TOP 1 CambioABolivares FROM Cambio WHERE CodigoMoneda = 'USD' AND FechaDeVigencia <= Fecha) ELSE 1 END, 1), 2) AS montoVencido 
FROM cxC 
WHERE ConsecutivoCompania = @Compania AND Status IN ('0','3') 
    AND Fecha BETWEEN CAST('01/' + CAST(MONTH(DATEADD(m,-1, GETDATE())) AS varchar) + '/' + CAST(YEAR(DATEADD(m,-1, GETDATE())) AS varchar) AS smalldatetime) AND GETDATE()
	AND TipoCxC IN ('0', '3',  '4', '<')
UNION
SELECT '', MONTH(GETDATE())  AS mesCxC, 0 AS montoVigente, 0 AS montoVencido 
UNION
SELECT '', MONTH(GETDATE())-1  AS mesCxC, 0 AS montoVigente, 0 AS montoVencido 
)
SELECT
	mesCxC AS mesCxC, 
	SUM(montoVigente) AS montoVigente,
	SUM(montoVencido) AS montoVencido
FROM CTE_CxC
GROUP BY mesCxC
ORDER BY mesCxC


/* Cuentas por Pagar 
"CuentasPorPagar"
*/
;WITH CTE_CxP AS (
SELECT
	ConsecutivoCxP,
	MONTH(Fecha) AS mesCxP, 
	(CASE WHEN FechaVencimiento >= GETDATE() THEN ROUND(((MontoExento + MontoGravado + MontoIVA) - MontoAbonado) / ISNULL(CASE WHEN CodigoMoneda = 'VED' THEN (SELECT TOP 1 CambioABolivares FROM Cambio WHERE CodigoMoneda = 'USD' AND FechaDeVigencia <= Fecha) ELSE 1 END, 1), 2) ELSE 0 END) AS montoVigente,
	(CASE WHEN FechaVencimiento <  GETDATE() THEN ROUND(((MontoExento + MontoGravado + MontoIVA) - MontoAbonado) / ISNULL(CASE WHEN CodigoMoneda = 'VED' THEN (SELECT TOP 1 CambioABolivares FROM Cambio WHERE CodigoMoneda = 'USD' AND FechaDeVigencia <= Fecha) ELSE 1 END, 1), 2) ELSE 0 END) AS montoVencido 
FROM cxP 
WHERE ConsecutivoCompania = @Compania AND Status IN ('0','3')
	AND Fecha BETWEEN CAST('01/' + CAST(MONTH(DATEADD(m,-1, GETDATE())) AS varchar) + '/' + CAST(YEAR(DATEADD(m,-1, GETDATE())) AS varchar) AS smalldatetime) AND GETDATE()
	AND TipoDeCxP IN ('0', '3',  '4', '<')
UNION
SELECT 0, MONTH(GETDATE()) AS mesCxP,  0 AS montoVigente, 0 AS montoVencido 
UNION
SELECT 0, MONTH(GETDATE())-1 AS mesCxP, 0 AS montoVigente, 0 AS montoVencido 
)
SELECT
	mesCxP AS mesCxP,
	SUM(montoVigente) AS montoVigente,
	SUM(montoVencido) AS montoVencido
FROM CTE_CxP
GROUP BY mesCxP
ORDER BY mesCxP


/* Cobranzas 
"Cobranzas"
*/
;WITH CTE_cobranza AS (
SELECT
	Numero,
	MONTH(Fecha) AS mesCobranza,
	ROUND(TotalDocumentos / ISNULL(CASE WHEN CodigoMoneda = 'VED' THEN (SELECT TOP 1 CambioABolivares FROM Cambio WHERE CodigoMoneda = 'USD' AND FechaDeVigencia <= Fecha) ELSE 1 END, 1), 2) AS totalCobrado
FROM cobranza
WHERE ConsecutivoCompania = @Compania AND StatusCobranza = '0' 
	AND Fecha BETWEEN CAST('01/' + CAST(MONTH(DATEADD(m,-1, GETDATE())) AS varchar) + '/' + CAST(YEAR(DATEADD(m,-1, GETDATE())) AS varchar) AS smalldatetime) AND GETDATE()
UNION
SELECT '', MONTH(GETDATE()) AS mesCobranza, 0 AS totalCobrado 
UNION
SELECT '', MONTH(GETDATE())-1 AS mesCobranza, 0 AS totalCobrado 
)
SELECT 
	mesCobranza AS mesCobranzas, 
	SUM(totalCobrado) AS totalCobrado 
FROM CTE_cobranza 
GROUP BY mesCobranza
ORDER BY mesCobranza


/* Factura 
"ClientesFacturados"
*/
;WITH CTE_factura AS (
SELECT DISTINCT
	MONTH(Fecha) AS mesFactura, 
	CodigoCliente,
	1 AS clientesFacturados 
FROM factura 
WHERE ConsecutivoCompania = @Compania AND StatusFactura = '0' AND TipoDeDocumento = '0' 
	AND Fecha BETWEEN CAST('01/' + CAST(MONTH(DATEADD(m,-1, GETDATE())) AS varchar) + '/' + CAST(YEAR(DATEADD(m,-1, GETDATE())) AS varchar) AS smalldatetime) AND GETDATE()
GROUP BY MONTH(Fecha), CodigoCliente
UNION
SELECT MONTH(GETDATE()) AS mesFactura, '', 0 AS clientesFacturados 
UNION
SELECT MONTH(GETDATE())-1 AS mesFactura,'', 0 AS clientesFacturados 
)
SELECT
	mesFactura,
	SUM(clientesFacturados) AS CantClientes
FROM CTE_factura
GROUP BY mesFactura


/* Comparativo de Ventas
"ComparativoDeVentas"
*/
;WITH CTE_factura AS (
SELECT 
	Numero,
	TipoDeDocumento,
	YEAR(Fecha) AS anoFactura, 
	MONTH(Fecha) AS mesFactura, 
	ROUND((TotalBaseImponible + TotalMontoExento) / CambioMonedaCXC, 2) AS totalFactura 
	FROM factura 
	WHERE ConsecutivoCompania = @Compania AND StatusFactura = '0' AND GeneradoPor <> '3' AND TipoDeDocumento IN ('0', '1', '2') 
	AND YEAR(Fecha) BETWEEN YEAR(GETDATE())-1 AND YEAR(GETDATE())
UNION SELECT '', '', YEAR(GETDATE())-1 AS anoFactura, 1 AS mesFactura, 0 AS totalFactura  
UNION SELECT '', '', YEAR(GETDATE())-1 AS anoFactura, 2 AS mesFactura, 0 AS totalFactura   
UNION SELECT '', '', YEAR(GETDATE())-1 AS anoFactura, 3 AS mesFactura, 0 AS totalFactura  
UNION SELECT '', '', YEAR(GETDATE())-1 AS anoFactura, 4 AS mesFactura, 0 AS totalFactura  
UNION SELECT '', '', YEAR(GETDATE())-1 AS anoFactura, 5 AS mesFactura, 0 AS totalFactura  
UNION SELECT '', '', YEAR(GETDATE())-1 AS anoFactura, 6 AS mesFactura, 0 AS totalFactura  
UNION SELECT '', '', YEAR(GETDATE())-1 AS anoFactura, 7 AS mesFactura, 0 AS totalFactura  
UNION SELECT '', '', YEAR(GETDATE())-1 AS anoFactura, 8 AS mesFactura, 0 AS totalFactura  
UNION SELECT '', '', YEAR(GETDATE())-1 AS anoFactura, 9 AS mesFactura, 0 AS totalFactura  
UNION SELECT '', '', YEAR(GETDATE())-1 AS anoFactura, 10 AS mesFactura, 0 AS totalFactura  
UNION SELECT '', '', YEAR(GETDATE())-1 AS anoFactura, 11 AS mesFactura, 0 AS totalFactura  
UNION SELECT '', '', YEAR(GETDATE())-1 AS anoFactura, 12 AS mesFactura, 0 AS totalFactura  
UNION SELECT '', '', YEAR(GETDATE()) AS anoFactura, 1 AS mesFactura, 0 AS totalFactura 
UNION SELECT '', '', YEAR(GETDATE()) AS anoFactura, 2 AS mesFactura, 0 AS totalFactura 
UNION SELECT '', '', YEAR(GETDATE()) AS anoFactura, 3 AS mesFactura, 0 AS totalFactura 
UNION SELECT '', '', YEAR(GETDATE()) AS anoFactura, 4 AS mesFactura, 0 AS totalFactura 
UNION SELECT '', '', YEAR(GETDATE()) AS anoFactura, 5 AS mesFactura, 0 AS totalFactura 
UNION SELECT '', '', YEAR(GETDATE()) AS anoFactura, 6 AS mesFactura, 0 AS totalFactura 
UNION SELECT '', '', YEAR(GETDATE()) AS anoFactura, 7 AS mesFactura, 0 AS totalFactura  
UNION SELECT '', '', YEAR(GETDATE()) AS anoFactura, 8 AS mesFactura, 0 AS totalFactura  
UNION SELECT '', '', YEAR(GETDATE()) AS anoFactura, 9 AS mesFactura, 0 AS totalFactura  
UNION SELECT '', '', YEAR(GETDATE()) AS anoFactura, 10 AS mesFactura, 0 AS totalFactura  
UNION SELECT '', '', YEAR(GETDATE()) AS anoFactura, 11 AS mesFactura, 0 AS totalFactura  
UNION SELECT '', '', YEAR(GETDATE()) AS anoFactura, 12 AS mesFactura, 0 AS totalFactura  
)
SELECT 
	anoFactura, 
	mesFactura, 
	SUM(totalFactura) AS totalFactura 
FROM CTE_factura 
GROUP BY anoFactura, mesFactura
ORDER BY anoFactura,mesFactura


/* Comparativo de Cobranzas 
"ComparativoDeCobranzas"
*/
;WITH CTE_Cobranza AS (
SELECT
	Numero,
	YEAR(Fecha) AS anoCobranza, 
	MONTH(Fecha) AS mesCobranza, 
	ROUND(TotalDocumentos / ISNULL(CASE WHEN CodigoMoneda = 'VED' THEN (SELECT TOP 1 CambioABolivares FROM Cambio WHERE CodigoMoneda = 'USD' AND FechaDeVigencia <= Fecha) ELSE 1 END, 1), 2) AS totalCobrado 
FROM cobranza 
WHERE ConsecutivoCompania = @Compania AND StatusCobranza = '0' AND StatusCobranza = '0'
	AND YEAR(Fecha) BETWEEN YEAR(GETDATE())-1 AND YEAR(GETDATE())
UNION SELECT '', YEAR(GETDATE())-1 AS anoCobranza, 1 AS mesCobranza, 0 AS totalCobrado   
UNION SELECT '', YEAR(GETDATE())-1 AS anoCobranza, 2 AS mesCobranza, 0 AS totalCobrado   
UNION SELECT '', YEAR(GETDATE())-1 AS anoCobranza, 3 AS mesCobranza, 0 AS totalCobrado   
UNION SELECT '', YEAR(GETDATE())-1 AS anoCobranza, 4 AS mesCobranza, 0 AS totalCobrado   
UNION SELECT '', YEAR(GETDATE())-1 AS anoCobranza, 5 AS mesCobranza, 0 AS totalCobrado   
UNION SELECT '', YEAR(GETDATE())-1 AS anoCobranza, 6 AS mesCobranza, 0 AS totalCobrado   
UNION SELECT '', YEAR(GETDATE())-1 AS anoCobranza, 7 AS mesCobranza, 0 AS totalCobrado   
UNION SELECT '', YEAR(GETDATE())-1 AS anoCobranza, 8 AS mesCobranza, 0 AS totalCobrado   
UNION SELECT '', YEAR(GETDATE())-1 AS anoCobranza, 9 AS mesCobranza, 0 AS totalCobrado   
UNION SELECT '', YEAR(GETDATE())-1 AS anoCobranza, 10 AS mesCobranza, 0 AS totalCobrado   
UNION SELECT '', YEAR(GETDATE())-1 AS anoCobranza, 11 AS mesCobranza, 0 AS totalCobrado   
UNION SELECT '', YEAR(GETDATE())-1 AS anoCobranza, 12 AS mesCobranza, 0 AS totalCobrado   
UNION SELECT '', YEAR(GETDATE()) AS anoCobranza, 1 AS mesCobranza, 0 AS totalCobrado   
UNION SELECT '', YEAR(GETDATE()) AS anoCobranza, 2 AS mesCobranza, 0 AS totalCobrado   
UNION SELECT '', YEAR(GETDATE()) AS anoCobranza, 3 AS mesCobranza, 0 AS totalCobrado   
UNION SELECT '', YEAR(GETDATE()) AS anoCobranza, 4 AS mesCobranza, 0 AS totalCobrado   
UNION SELECT '', YEAR(GETDATE()) AS anoCobranza, 5 AS mesCobranza, 0 AS totalCobrado   
UNION SELECT '', YEAR(GETDATE()) AS anoCobranza, 6 AS mesCobranza, 0 AS totalCobrado   
UNION SELECT '', YEAR(GETDATE()) AS anoCobranza, 7 AS mesCobranza, 0 AS totalCobrado   
UNION SELECT '', YEAR(GETDATE()) AS anoCobranza, 8 AS mesCobranza, 0 AS totalCobrado   
UNION SELECT '', YEAR(GETDATE()) AS anoCobranza, 9 AS mesCobranza, 0 AS totalCobrado   
UNION SELECT '', YEAR(GETDATE()) AS anoCobranza, 10 AS mesCobranza, 0 AS totalCobrado   
UNION SELECT '', YEAR(GETDATE()) AS anoCobranza, 11 AS mesCobranza, 0 AS totalCobrado   
UNION SELECT '', YEAR(GETDATE()) AS anoCobranza, 12 AS mesCobranza, 0 AS totalCobrado   
)
SELECT 
	anoCobranza, 
	mesCobranza, 
	SUM(totalCobrado) AS totalCobranza 
FROM CTE_Cobranza 
GROUP BY anoCobranza, mesCobranza
ORDER BY anoCobranza, mesCobranza


/* Flujo de Transacciones 
"FlujoDeTransacciones"
*/
;WITH CTE_MovBancario AS (
SELECT
	ConsecutivoMovimiento,
	MONTH(MB.Fecha) AS mesMovBancario, 
	(CASE WHEN MB.TipoConcepto = '0' THEN ROUND(MB.Monto / ISNULL(CASE WHEN CB.CodigoMoneda = 'VED' THEN (SELECT TOP 1 CambioABolivares FROM Cambio WHERE CodigoMoneda = 'USD' AND FechaDeVigencia <= MB.Fecha) ELSE 1 END, 1), 2) ELSE 0 END) AS totalCobrado, 
	(CASE WHEN MB.TipoConcepto = '1' THEN ROUND(MB.Monto / ISNULL(CASE WHEN CB.CodigoMoneda = 'VED' THEN (SELECT TOP 1 CambioABolivares FROM Cambio WHERE CodigoMoneda = 'USD' AND FechaDeVigencia <= MB.Fecha) ELSE 1 END, 1), 2) ELSE 0 END) AS totalPagado 
FROM movimientoBancario MB INNER JOIN Saw.CuentaBancaria CB ON MB.ConsecutivoCompania = CB.ConsecutivoCompania AND MB.CodigoCtaBancaria = CB.Codigo
WHERE MB.ConsecutivoCompania = @Compania AND YEAR(MB.Fecha) = YEAR(GETDATE()) 
UNION SELECT 0, 1, 0 AS totalCobrado, 0
UNION SELECT 0, 2, 0 AS totalCobrado, 0
UNION SELECT 0, 3, 0 AS totalCobrado, 0
UNION SELECT 0, 4, 0 AS totalCobrado, 0
UNION SELECT 0, 5, 0 AS totalCobrado, 0
UNION SELECT 0, 6, 0 AS totalCobrado, 0
UNION SELECT 0, 7, 0 AS totalCobrado, 0
UNION SELECT 0, 8, 0 AS totalCobrado, 0
UNION SELECT 0, 9, 0 AS totalCobrado, 0
UNION SELECT 0, 10, 0 AS totalCobrado, 0
UNION SELECT 0, 11, 0 AS totalCobrado, 0
UNION SELECT 0, 12, 0 AS totalCobrado, 0
)
SELECT 
	mesMovBancario AS mes, 
	SUM(TotalCobrado) AS totalCobrado, 
	SUM(TotalPagado) AS totalPagado 
FROM CTE_MovBancario
GROUP BY mesMovBancario
ORDER BY mesMovBancario


/* Distribución de Egresos 
"DistribucionDeEgresos"
*/
;WITH CTE_MovBancario AS(
SELECT 
	MB.ConsecutivoMovimiento,
	MB.CodigoConcepto AS CodigoCB,
	CB.Descripcion AS descripcion, 
	(ROUND(MB.Monto / ISNULL(CASE WHEN CtaB.CodigoMoneda = 'VED' THEN (SELECT TOP 1 CambioABolivares FROM Cambio WHERE CodigoMoneda = 'USD' AND FechaDeVigencia <= MB.Fecha) ELSE 1 END, 1), 2)) AS monto 
FROM movimientoBancario MB INNER JOIN ConceptoBancario CB ON MB.CodigoConcepto = CB.Codigo INNER JOIN Saw.CuentaBancaria CtaB ON MB.ConsecutivoCompania = CtaB.ConsecutivoCompania AND MB.CodigoCtaBancaria = CtaB.Codigo
WHERE MB.ConsecutivoCompania = @Compania AND MB.TipoConcepto = '1'
	AND YEAR(MB.Fecha) = YEAR(GETDATE())
UNION SELECT 0, 'S/M', 'sin movimientos', 0
),
CTE_SumaTop10 AS (
SELECT TOP 10
	CodigoCB, descripcion AS descripcionMovimiento,  SUM(monto) AS monto 
FROM CTE_MovBancario
GROUP BY CodigoCB, descripcion
ORDER BY monto DESC
),
CTE_Resto AS (
SELECT '*** Otros Egresos ***' AS descripcionMovimiento, ISNULL(SUM(monto), 0) AS monto 
FROM CTE_MovBancario
WHERE CodigoCB NOT IN (SELECT CodigoCB FROM CTE_SumaTop10)
)
SELECT '0' AS ColOrden, descripcionMovimiento, monto 
FROM CTE_SumaTop10
UNION
SELECT '1' AS ColOrden, descripcionMovimiento, monto 
FROM CTE_Resto
ORDER BY ColOrden


--;WITH CTE_MovBancario AS(
--SELECT 
--	CB.Descripcion AS descripcion, 
--	(ROUND(MB.Monto / ISNULL(CASE WHEN CtaB.CodigoMoneda = 'VED' THEN (SELECT TOP 1 CambioABolivares FROM Cambio WHERE CodigoMoneda = 'USD' AND FechaDeVigencia <= MB.Fecha) ELSE 1 END, 1), 2)) AS monto 
--FROM movimientoBancario MB INNER JOIN ConceptoBancario CB ON MB.CodigoConcepto = CB.Codigo INNER JOIN Saw.CuentaBancaria CtaB ON MB.ConsecutivoCompania = CtaB.ConsecutivoCompania AND MB.CodigoCtaBancaria = CtaB.Codigo
--WHERE MB.ConsecutivoCompania = @Compania AND YEAR(MB.Fecha) = YEAR(GETDATE()) AND MB.TipoConcepto = '1' 
--UNION
--SELECT 'sin movimientos' AS descripcion, 0 AS monto  
--)
--SELECT 
--	descripcion AS descripcionMovimiento, 
--	SUM(monto) AS monto 
--FROM CTE_MovBancario
--GROUP BY descripcion
--ORDER BY monto DESC

/*
--;WITH 
--CTE_IngresosEgresos AS (
--SELECT MONTH(Fecha) AS Mes, SUM(ROUND(TotalDocumentos / CambioABolivares, 2)) AS TotalCobrado, 0 AS TotalPagado
--FROM cobranza
--WHERE ConsecutivoCompania = @Compania AND YEAR(Fecha) = YEAR(GETDATE()) AND StatusCobranza = '0'
--GROUP BY MONTH(Fecha)
--UNION
--SELECT MONTH(Fecha) AS Mes, 0 AS TotalCobrado, SUM(ROUND(TotalDocumentos / CambioaBolivares, 2)) AS TotalPagado
--FROM pago
--WHERE ConsecutivoCompania = @Compania AND YEAR(Fecha) = YEAR(GETDATE()) AND StatusOrdenDePago = '0'
--GROUP BY MONTH(Fecha)
--)
--SELECT Mes, SUM(TotalCobrado) AS TotalCobrado, SUM(TotalPagado) AS TotalPagado
--FROM CTE_IngresosEgresos
--GROUP BY Mes
--ORDER BY Mes


*/