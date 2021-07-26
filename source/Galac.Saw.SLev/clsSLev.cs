using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base;


namespace Galac.Saw.SLev {
    public class clsSLev {
        /// <summary>
        /// Plantilla especifica del SAW
        /// </summary>
        /// <param name="valLevel"></param>
        /// <returns></returns>
        public List<CustomRole> PlantillaPermisos() {

            List<CustomRole> vPermisos = new List<CustomRole>();

            #region Compania
            vPermisos.Add(new CustomRole("Compañía", "Insertar", "Compañía / Parámetros / Niveles de Precio", 2));
            vPermisos.Add(new CustomRole("Compañía", "Consultar", "Compañía / Parámetros / Niveles de Precio", 2));
            vPermisos.Add(new CustomRole("Compañía", "Modificar", "Compañía / Parámetros / Niveles de Precio", 2));
            vPermisos.Add(new CustomRole("Compañía", "Eliminar", "Compañía / Parámetros / Niveles de Precio", 2));
            vPermisos.Add(new CustomRole("Compañía", "Copiar Parámetros Administrativos", "Compañía / Parámetros / Niveles de Precio", 2));
            #endregion

            #region Factura
            vPermisos.Add(new CustomRole("Factura", "Consultar", "Principal", 1));
            vPermisos.Add(new CustomRole("Factura", "Insertar", "Principal", 1));
            vPermisos.Add(new CustomRole("Factura", "Modificar", "Principal", 1));
            vPermisos.Add(new CustomRole("Factura", "Eliminar", "Principal", 1));
            vPermisos.Add(new CustomRole("Factura", "Emitir", "Principal", 1));
            vPermisos.Add(new CustomRole("Factura", "Emitir Directo", "Principal", 1));
            vPermisos.Add(new CustomRole("Factura", "Anular", "Principal", 1));
            vPermisos.Add(new CustomRole("Factura", "Insertar Copia", "Principal", 1));
            vPermisos.Add(new CustomRole("Factura", "Insertar Factura Borrador", "Principal", 1));
            vPermisos.Add(new CustomRole("Factura", "Insertar Factura Manual", "Principal", 1));
            vPermisos.Add(new CustomRole("Factura", "Insertar Venta con Débito Fiscal Diferido", "Principal", 1));
            vPermisos.Add(new CustomRole("Factura", "Insertar Devolución / Reverso", "Principal", 1));
            vPermisos.Add(new CustomRole("Factura", "Insertar Nota de Crédito", "Principal", 1));
            vPermisos.Add(new CustomRole("Factura", "Insertar Nota de Débito", "Principal", 1));
            vPermisos.Add(new CustomRole("Factura", "Ingresar Fecha de Entrega", "Principal", 1));
            vPermisos.Add(new CustomRole("Factura", "Insertar Factura Histórica", "Principal", 1));
            vPermisos.Add(new CustomRole("Factura", "Insertar Nota de Crédito Histórica", "Principal", 1));
            vPermisos.Add(new CustomRole("Factura", "Insertar Nota de Débito Histórica", "Principal", 1));
            vPermisos.Add(new CustomRole("Factura", "Imprimir Orden De Despacho", "Principal", 1));
            vPermisos.Add(new CustomRole("Factura", "Generar Factura desde Contrato", "Principal", 1));
            vPermisos.Add(new CustomRole("Factura", "Generar Factura desde Cotización", "Principal", 1));
            vPermisos.Add(new CustomRole("Factura", "Informes Gerenciales", "Principal", 1));
            vPermisos.Add(new CustomRole("Factura", "Informes de Libros", "Principal", 1));
            vPermisos.Add(new CustomRole("Factura", "Informes", "Principal", 1));
            vPermisos.Add(new CustomRole("Factura", "Otorgar Descuento", "Principal", 1));
            vPermisos.Add(new CustomRole("Factura", "Cobro Directo", "Principal", 1));
            vPermisos.Add(new CustomRole("Factura", "Cambiar Descripción y Precio", "Principal", 1));
            vPermisos.Add(new CustomRole("Factura", "Modificar Precio en Factura", "Principal", 1));
            vPermisos.Add(new CustomRole("Factura", "Importar", "Principal", 1));
            vPermisos.Add(new CustomRole("Factura", "Exportar", "Principal", 1));
            vPermisos.Add(new CustomRole("Factura", "Reservar Mercancía", "Principal", 1));
            vPermisos.Add(new CustomRole("Factura", "Modificar Vendedor en Factura Emitida", "Principal", 1));
            vPermisos.Add(new CustomRole("Factura", "Reactivar Factura Anulada", "Principal", 1));
            vPermisos.Add(new CustomRole("Factura", "Emisión sin Impresión Fiscal", "Principal", 1));
            vPermisos.Add(new CustomRole("Factura", "Emitir y Cobrar sin Impresión Fiscal", "Principal", 1));
            #endregion

            #region Nota de Entrega
            vPermisos.Add(new CustomRole("Nota de Entrega", "Consultar", "Principal", 1));
            vPermisos.Add(new CustomRole("Nota de Entrega", "Insertar", "Principal", 1));
            vPermisos.Add(new CustomRole("Nota de Entrega", "Modificar", "Principal", 1));
            vPermisos.Add(new CustomRole("Nota de Entrega", "Eliminar", "Principal", 1));
            vPermisos.Add(new CustomRole("Nota de Entrega", "Anular", "Principal", 1));
            vPermisos.Add(new CustomRole("Nota de Entrega", "Emitir", "Principal", 1));
            vPermisos.Add(new CustomRole("Nota de Entrega", "Reimprimir", "Principal", 1));
            #endregion

            #region Cotizacion
            vPermisos.Add(new CustomRole("Cotización", "Consultar", "Principal", 1));
            vPermisos.Add(new CustomRole("Cotización", "Insertar", "Principal", 1));
            vPermisos.Add(new CustomRole("Cotización", "Modificar", "Principal", 1));
            vPermisos.Add(new CustomRole("Cotización", "Eliminar", "Principal", 1));
            vPermisos.Add(new CustomRole("Cotización", "Informes", "Principal", 1));
            vPermisos.Add(new CustomRole("Cotización", "Otorgar Descuento", "Principal", 1));
            vPermisos.Add(new CustomRole("Cotización", "Reservar Mercancía", "Principal", 1));
            vPermisos.Add(new CustomRole("Cotización", "Insertar Copia Cotizacion", "Principal", 1));
            #endregion

            #region ControlDepacho
            vPermisos.Add(new CustomRole("Control Despacho", "Consultar", "Principal", 1));
            vPermisos.Add(new CustomRole("Control Despacho", "Insertar", "Principal", 1));
            vPermisos.Add(new CustomRole("Control Despacho", "Modificar", "Principal", 1));
            vPermisos.Add(new CustomRole("Control Despacho", "Eliminar", "Principal", 1));
            vPermisos.Add(new CustomRole("Control Despacho", "Anular", "Principal", 1));
            vPermisos.Add(new CustomRole("Control Despacho", "Generar Borrador de Factura", "Principal", 1));
            vPermisos.Add(new CustomRole("Control Despacho", "Informes", "Principal", 1));
            #endregion

            #region Cliente
            vPermisos.Add(new CustomRole("Cliente", "Consultar", "Cliente / CxC", 3));
            vPermisos.Add(new CustomRole("Cliente", "Insertar", "Cliente / CxC", 3));
            vPermisos.Add(new CustomRole("Cliente", "Modificar", "Cliente / CxC", 3));
            vPermisos.Add(new CustomRole("Cliente", "Eliminar", "Cliente / CxC", 3));
            vPermisos.Add(new CustomRole("Cliente", "Informes", "Cliente / CxC", 3));
            vPermisos.Add(new CustomRole("Cliente", "Unificar", "Cliente / CxC", 3));
            vPermisos.Add(new CustomRole("Cliente", "Informes de Libros", "Cliente / CxC", 3));
            vPermisos.Add(new CustomRole("Cliente", "Importar", "Cliente / CxC", 3));
            vPermisos.Add(new CustomRole("Cliente", "Exportar", "Cliente / CxC", 3));
            vPermisos.Add(new CustomRole("Cliente", "Ingresar cliente por mostrador", "Cliente / CxC", 3));
            #endregion

            #region CxC
            vPermisos.Add(new CustomRole("CxC", "Consultar", "Cliente / CxC", 3));
            vPermisos.Add(new CustomRole("CxC", "Insertar", "Cliente / CxC", 3));
            vPermisos.Add(new CustomRole("CxC", "Modificar", "Cliente / CxC", 3));
            vPermisos.Add(new CustomRole("CxC", "Eliminar", "Cliente / CxC", 3));
            vPermisos.Add(new CustomRole("CxC", "Anular", "Cliente / CxC", 3));
            vPermisos.Add(new CustomRole("CxC", "Reimprimir Comprobante CxC", "Cliente / CxC", 3));
            vPermisos.Add(new CustomRole("CxC", "Informes de Libros", "Cliente / CxC", 3));
            vPermisos.Add(new CustomRole("CxC", "Informes", "Cliente / CxC", 3));
            vPermisos.Add(new CustomRole("CxC", "Importar", "Cliente / CxC", 3));
            vPermisos.Add(new CustomRole("CxC", "Exportar", "Cliente / CxC", 3));
            vPermisos.Add(new CustomRole("CxC", "Refinanciar", "Cliente / CxC", 3));
            vPermisos.Add(new CustomRole("CxC", "Anular Refinanciamiento", "Cliente / CxC", 3));
            #endregion

            #region Cobranza
            vPermisos.Add(new CustomRole("Cobranza", "Consultar", "Cliente / CxC", 3));
            vPermisos.Add(new CustomRole("Cobranza", "Insertar", "Cliente / CxC", 3));
            vPermisos.Add(new CustomRole("Cobranza", "Modificar", "Cliente / CxC", 3));
            vPermisos.Add(new CustomRole("Cobranza", "Anular", "Cliente / CxC", 3));
            vPermisos.Add(new CustomRole("Cobranza", "Informes", "Cliente / CxC", 3));
            vPermisos.Add(new CustomRole("Cobranza", "Distribuir Retención IVA", "Cliente / CxC", 3));
            vPermisos.Add(new CustomRole("Cobranza", "Aplicar Ret. a Documento Cobrado", "Cliente / CxC", 3));
            vPermisos.Add(new CustomRole("Cobranza", "Modificar Tasa de Cambio", "Cliente / CxC", 3));
            vPermisos.Add(new CustomRole("Cobranza", "Buscar Tasa de Cambio Original de CxC", "Cliente / CxC", 3));
            #endregion

            #region Artículo
            vPermisos.Add(new CustomRole("Artículo", "Consultar", "Inventario", 4));
            vPermisos.Add(new CustomRole("Artículo", "Insertar", "Inventario", 4));
            vPermisos.Add(new CustomRole("Artículo", "Modificar", "Inventario", 4));
            vPermisos.Add(new CustomRole("Artículo", "Eliminar", "Inventario", 4));
            vPermisos.Add(new CustomRole("Artículo", "Reincorporar", "Inventario", 4));
            vPermisos.Add(new CustomRole("Artículo", "Desincorporar", "Inventario", 4));
            vPermisos.Add(new CustomRole("Artículo", "Recalcular Existencia", "Inventario", 4));
            vPermisos.Add(new CustomRole("Artículo", "Ajustar Precio", "Inventario", 4));
            vPermisos.Add(new CustomRole("Artículo", "Importar", "Inventario", 4));
            vPermisos.Add(new CustomRole("Artículo", "Exportar", "Inventario", 4));
            vPermisos.Add(new CustomRole("Artículo", "Autorizar Sobregiro", "Inventario", 4));
            vPermisos.Add(new CustomRole("Artículo", "Informes", "Inventario", 4));
            vPermisos.Add(new CustomRole("Artículo", "Insertar Art. por Porcentaje de Comisión", "Inventario", 4));
            vPermisos.Add(new CustomRole("Artículo", "Modificar Serial", "Inventario", 4));
            vPermisos.Add(new CustomRole("Artículo", "Modificar Rollo", "Inventario", 4));
            vPermisos.Add(new CustomRole("Artículo", "Consultar Cortes", "Inventario", 4));
            vPermisos.Add(new CustomRole("Artículo", "Ajustar Precios por Costos", "Inventario", 4));
            vPermisos.Add(new CustomRole("Artículo", "Método de Costo", "Inventario", 4));
            vPermisos.Add(new CustomRole("Artículo", "Alerta Por Reservas", "Inventario", 4));
            vPermisos.Add(new CustomRole("Artículo", "Liberar Reservas", "Inventario", 4));
            vPermisos.Add(new CustomRole("Artículo", "Ajuste de Gastos Admisibles", "Inventario", 4));
            vPermisos.Add(new CustomRole("Artículo", "Informes Ley Costos", "Inventario", 4));
            #endregion

            #region Almacen
            vPermisos.Add(new CustomRole("Almacén", "Consultar", "Inventario", 4));
            vPermisos.Add(new CustomRole("Almacén", "Insertar", "Inventario", 4));
            vPermisos.Add(new CustomRole("Almacén", "Modificar", "Inventario", 4));
            vPermisos.Add(new CustomRole("Almacén", "Eliminar", "Inventario", 4));
            #endregion

            #region Nota Ent/Salida
            vPermisos.Add(new CustomRole("Nota de Entrada/Salida", "Consultar", "Inventario", 4));
            vPermisos.Add(new CustomRole("Nota de Entrada/Salida", "Insertar", "Inventario", 4));
            vPermisos.Add(new CustomRole("Nota de Entrada/Salida", "Eliminar", "Inventario", 4));
            vPermisos.Add(new CustomRole("Nota de Entrada/Salida", "Informes", "Inventario", 4));
            #endregion

            #region Exist x Almacen
            vPermisos.Add(new CustomRole("Existencia por Almacén", "Consultar", "Inventario", 4));
            vPermisos.Add(new CustomRole("Existencia por Almacén", "Informes", "Inventario", 4));
            #endregion

            #region Transferencia
            vPermisos.Add(new CustomRole("Transferencia", "Consultar", "Inventario", 4));
            vPermisos.Add(new CustomRole("Transferencia", "Insertar", "Inventario", 4));
            vPermisos.Add(new CustomRole("Transferencia", "Eliminar", "Inventario", 4));
            vPermisos.Add(new CustomRole("Transferencia", "Informes", "Inventario", 4));
            #endregion

            #region Vendedor
            vPermisos.Add(new CustomRole("Vendedor", "Consultar", "CxP / Vendedor", 5));
            vPermisos.Add(new CustomRole("Vendedor", "Insertar", "CxP / Vendedor", 5));
            vPermisos.Add(new CustomRole("Vendedor", "Modificar", "CxP / Vendedor", 5));
            vPermisos.Add(new CustomRole("Vendedor", "Eliminar", "CxP / Vendedor", 5));
            vPermisos.Add(new CustomRole("Vendedor", "Informes", "CxP / Vendedor", 5));
            vPermisos.Add(new CustomRole("Vendedor", "ComisionXVencimiento", "CxP / Vendedor", 5));
            vPermisos.Add(new CustomRole("Vendedor", "Importar", "CxP / Vendedor", 5));
            vPermisos.Add(new CustomRole("Vendedor", "Exportar", "CxP / Vendedor", 5));
            #endregion

            #region Parametros
            vPermisos.Add(new CustomRole("Parámetros", "Consultar", "Compañía / Parámetros / Niveles de Precio", 2));
            vPermisos.Add(new CustomRole("Parámetros", "Modificar", "Compañía / Parámetros / Niveles de Precio", 2));
            vPermisos.Add(new CustomRole("Parámetros", "Informes", "Compañía / Parámetros / Niveles de Precio", 2));
            #endregion

            #region CxP
            vPermisos.Add(new CustomRole("CxP", "Consultar", "CxP / Vendedor", 5));
            vPermisos.Add(new CustomRole("CxP", "Insertar", "CxP / Vendedor", 5));
            vPermisos.Add(new CustomRole("CxP", "Modificar", "CxP / Vendedor", 5));
            vPermisos.Add(new CustomRole("CxP", "Eliminar", "CxP / Vendedor", 5));
            vPermisos.Add(new CustomRole("CxP", "Anular", "CxP / Vendedor", 5));
            vPermisos.Add(new CustomRole("CxP", "Reimprimir Comprobante CxP", "CxP / Vendedor", 5));
            vPermisos.Add(new CustomRole("CxP", "Informes de Libros", "CxP / Vendedor", 5));
            vPermisos.Add(new CustomRole("CxP", "Imprimir Comprobante de Ret. de IVA", "CxP / Vendedor", 5));
            vPermisos.Add(new CustomRole("CxP", "Insertar CxP Histórica", "CxP / Vendedor", 5));
            vPermisos.Add(new CustomRole("CxP", "Importar", "CxP / Vendedor", 5));
            vPermisos.Add(new CustomRole("CxP", "Exportar", "CxP / Vendedor", 5));
            vPermisos.Add(new CustomRole("CxP", "Insertar CxP por Cuenta de Terceros", "CxP / Vendedor", 5));
            vPermisos.Add(new CustomRole("CxP", "Reimprimir  Retenciones Municipales", "CxP / Vendedor", 5));
            #endregion

            #region Pago
            vPermisos.Add(new CustomRole("Pago", "Consultar", "CxP / Vendedor", 5));
            vPermisos.Add(new CustomRole("Pago", "Insertar", "CxP / Vendedor", 5));
            vPermisos.Add(new CustomRole("Pago", "Anular", "CxP / Vendedor", 5));
            vPermisos.Add(new CustomRole("Pago", "Informes", "CxP / Vendedor", 5));
            vPermisos.Add(new CustomRole("Pago", "Insertar Pago Histórico", "CxP / Vendedor", 5));
            vPermisos.Add(new CustomRole("Pago", "Reimprimir Comprobante de Pago", "CxP / Vendedor", 5));
            vPermisos.Add(new CustomRole("Pago", "Reimprimir Comprobante de Retención", "CxP / Vendedor", 5));
            vPermisos.Add(new CustomRole("Pago", "Reimprimir Comprobante de Ret. de IVA", "CxP / Vendedor", 5));
            vPermisos.Add(new CustomRole("Pago", "Modificar Beneficiario Cheque", "CxP / Vendedor", 5));
            vPermisos.Add(new CustomRole("Pago", "Cambiar Código del Seniat en Pagos", "CxP / Vendedor", 5));
            #endregion

            #region Proveedor
            vPermisos.Add(new CustomRole("Proveedor", "Consultar", "CxP / Vendedor", 5));
            vPermisos.Add(new CustomRole("Proveedor", "Insertar", "CxP / Vendedor", 5));
            vPermisos.Add(new CustomRole("Proveedor", "Modificar", "CxP / Vendedor", 5));
            vPermisos.Add(new CustomRole("Proveedor", "Eliminar", "CxP / Vendedor", 5));
            vPermisos.Add(new CustomRole("Proveedor", "Informes", "CxP / Vendedor", 5));
            vPermisos.Add(new CustomRole("Proveedor", "Unificar", "CxP / Vendedor", 5));
            vPermisos.Add(new CustomRole("Proveedor", "Informes de Libros", "CxP / Vendedor", 5));
            vPermisos.Add(new CustomRole("Proveedor", "Importar", "CxP / Vendedor", 5));
            vPermisos.Add(new CustomRole("Proveedor", "Exportar", "CxP / Vendedor", 5));
            #endregion

            #region Contrato
            vPermisos.Add(new CustomRole("Contrato", "Consultar", "Principal", 1));
            vPermisos.Add(new CustomRole("Contrato", "Insertar", "Principal", 1));
            vPermisos.Add(new CustomRole("Contrato", "Modificar", "Principal", 1));
            vPermisos.Add(new CustomRole("Contrato", "Eliminar", "Principal", 1));
            vPermisos.Add(new CustomRole("Contrato", "Informes", "Principal", 1));
            vPermisos.Add(new CustomRole("Contrato", "Extender", "Principal", 1));
            vPermisos.Add(new CustomRole("Contrato", "Activar", "Principal", 1));
            vPermisos.Add(new CustomRole("Contrato", "Desactivar", "Principal", 1));
            vPermisos.Add(new CustomRole("Contrato", "Ajustar Fecha Contrato", "Principal", 1));
            #endregion

            #region Cuenta Bancaria
            vPermisos.Add(new CustomRole("Cuenta Bancaria", "Consultar", "Bancos", 8));
            vPermisos.Add(new CustomRole("Cuenta Bancaria", "Insertar", "Bancos", 8));
            vPermisos.Add(new CustomRole("Cuenta Bancaria", "Modificar", "Bancos", 8));
            vPermisos.Add(new CustomRole("Cuenta Bancaria", "Eliminar", "Bancos", 8));
            vPermisos.Add(new CustomRole("Cuenta Bancaria", "Informes", "Bancos", 8));
            vPermisos.Add(new CustomRole("Cuenta Bancaria", "Recalcular", "Bancos", 8));
            #endregion

            #region Movimiento Bancario
            vPermisos.Add(new CustomRole("Movimiento Bancario", "Consultar", "Bancos", 8));
            vPermisos.Add(new CustomRole("Movimiento Bancario", "Insertar", "Bancos", 8));
            vPermisos.Add(new CustomRole("Movimiento Bancario", "Modificar", "Bancos", 8));
            vPermisos.Add(new CustomRole("Movimiento Bancario", "Eliminar", "Bancos", 8));
            vPermisos.Add(new CustomRole("Movimiento Bancario", "Anular", "Bancos", 8));
            vPermisos.Add(new CustomRole("Movimiento Bancario", "Reimprimir", "Bancos", 8));
            vPermisos.Add(new CustomRole("Movimiento Bancario", "Informes", "Bancos", 8));
            vPermisos.Add(new CustomRole("Movimiento Bancario", "Reimprimir Cheque", "Bancos", 8));
            #endregion

            #region Concepto Bancarion
            vPermisos.Add(new CustomRole("Concepto Bancario", "Consultar", "Bancos", 8));
            vPermisos.Add(new CustomRole("Concepto Bancario", "Insertar", "Bancos", 8));
            vPermisos.Add(new CustomRole("Concepto Bancario", "Modificar", "Bancos", 8));
            vPermisos.Add(new CustomRole("Concepto Bancario", "Eliminar", "Bancos", 8));
            #endregion

            #region Compra
            vPermisos.Add(new CustomRole("Compra", "Consultar", "Inventario", 4));
            vPermisos.Add(new CustomRole("Compra", "Insertar", "Inventario", 4));
            vPermisos.Add(new CustomRole("Compra", "Modificar", "Inventario", 4));
            vPermisos.Add(new CustomRole("Compra", "Eliminar", "Inventario", 4));
            vPermisos.Add(new CustomRole("Compra", "Anular", "Inventario", 4));
            vPermisos.Add(new CustomRole("Compra", "Informes", "Inventario", 4));
            #endregion

            #region Conteo Fisico
            vPermisos.Add(new CustomRole("Conteo Físico", "Consultar", "Inventario", 4));
            vPermisos.Add(new CustomRole("Conteo Físico", "Insertar", "Inventario", 4));
            vPermisos.Add(new CustomRole("Conteo Físico", "Modificar", "Inventario", 4));
            vPermisos.Add(new CustomRole("Conteo Físico", "Eliminar", "Inventario", 4));
            vPermisos.Add(new CustomRole("Conteo Físico", "Emitir", "Inventario", 4));
            vPermisos.Add(new CustomRole("Conteo Físico", "Anular", "Inventario", 4));
            vPermisos.Add(new CustomRole("Conteo Físico", "Informes", "Inventario", 4));
            #endregion

            #region Periodo
            #endregion

            #region Cuenta
            #endregion

            #region Catálogo General
            vPermisos.Add(new CustomRole("Catálogo General", "Consultar", "Contabilidad", 7));
            vPermisos.Add(new CustomRole("Catálogo General", "Insertar", "Contabilidad", 7));
            vPermisos.Add(new CustomRole("Catálogo General", "Modificar", "Contabilidad", 7));
            vPermisos.Add(new CustomRole("Catálogo General", "Eliminar", "Contabilidad", 7));
            #endregion

            #region Activo Fijo
            #endregion

            #region Conciliacion
            vPermisos.Add(new CustomRole("Conciliación", "Consultar", "Bancos", 8));
            vPermisos.Add(new CustomRole("Conciliación", "Insertar", "Bancos", 8));
            vPermisos.Add(new CustomRole("Conciliación", "Modificar", "Bancos", 8));
            vPermisos.Add(new CustomRole("Conciliación", "Eliminar", "Bancos", 8));
            vPermisos.Add(new CustomRole("Conciliación", "Informes", "Bancos", 8));
            vPermisos.Add(new CustomRole("Conciliación", "Cerrar", "Bancos", 8));
            vPermisos.Add(new CustomRole("Conciliación", "Abrir", "Bancos", 8));
            #endregion

            #region Plantilla Ret
            vPermisos.Add(new CustomRole("Plantilla Ret", "Consultar", "Retenciones / Forma 30", 6));
            vPermisos.Add(new CustomRole("Plantilla Ret", "Insertar", "Retenciones / Forma 30", 6));
            vPermisos.Add(new CustomRole("Plantilla Ret", "Modificar", "Retenciones / Forma 30", 6));
            vPermisos.Add(new CustomRole("Plantilla Ret", "Eliminar", "Retenciones / Forma 30", 6));
            #endregion

            #region ARCV
            vPermisos.Add(new CustomRole("ARCV", "Consultar", "Retenciones / Forma 30", 6));
            vPermisos.Add(new CustomRole("ARCV", "Insertar", "Retenciones / Forma 30", 6));
            vPermisos.Add(new CustomRole("ARCV", "Modificar", "Retenciones / Forma 30", 6));
            vPermisos.Add(new CustomRole("ARCV", "Eliminar", "Retenciones / Forma 30", 6));
            vPermisos.Add(new CustomRole("ARCV", "Reimprimir", "Retenciones / Forma 30", 6));
            vPermisos.Add(new CustomRole("ARCV", "Generar", "Retenciones / Forma 30", 6));
            #endregion

            #region Relacion Anual
            vPermisos.Add(new CustomRole("Relación Anual", "Forma Impresa", "Retenciones / Forma 30", 6));
            vPermisos.Add(new CustomRole("Relación Anual", "Medio Magnético", "Retenciones / Forma 30", 6));
            #endregion

            #region Planilla Forma 00030
            vPermisos.Add(new CustomRole("Planilla Forma 00030", "Consultar", "Retenciones / Forma 30", 6));
            vPermisos.Add(new CustomRole("Planilla Forma 00030", "Insertar", "Retenciones / Forma 30", 6));
            vPermisos.Add(new CustomRole("Planilla Forma 00030", "Insertar Sustitutiva", "Retenciones / Forma 30", 6));
            vPermisos.Add(new CustomRole("Planilla Forma 00030", "Modificar", "Retenciones / Forma 30", 6));
            vPermisos.Add(new CustomRole("Planilla Forma 00030", "Eliminar", "Retenciones / Forma 30", 6));
            vPermisos.Add(new CustomRole("Planilla Forma 00030", "Imprimir", "Retenciones / Forma 30", 6));
            vPermisos.Add(new CustomRole("Planilla Forma 00030", "Borrador", "Retenciones / Forma 30", 6));
            #endregion

            #region Tabla Retencion
            vPermisos.Add(new CustomRole("Tabla Retención", "Consultar", "Retenciones / Forma 30", 6));
            vPermisos.Add(new CustomRole("Tabla Retención", "Reinstalar", "Retenciones / Forma 30", 6));
            #endregion

            #region Lote Adm
            vPermisos.Add(new CustomRole("Lote Adm", "Consultar", "Compañía / Parámetros / Niveles de Precio", 2));
            vPermisos.Add(new CustomRole("Lote Adm", "Modificar", "Compañía / Parámetros / Niveles de Precio", 2));
            vPermisos.Add(new CustomRole("Lote Adm", "Eliminar", "Compañía / Parámetros / Niveles de Precio", 2));
            #endregion

            #region Resumen Diario de Ventas
            vPermisos.Add(new CustomRole("Resumen Diario", "Consultar", "Principal", 1));
            vPermisos.Add(new CustomRole("Resumen Diario", "Insertar", "Principal", 1));
            vPermisos.Add(new CustomRole("Resumen Diario", "Modificar", "Principal", 1));
            vPermisos.Add(new CustomRole("Resumen Diario", "Eliminar", "Principal", 1));
            vPermisos.Add(new CustomRole("Resumen Diario", "Realizar Cierre Z", "Principal", 1));
            #endregion

            #region Anticipo Cobrado
            vPermisos.Add(new CustomRole("Anticipo Cobrado", "Consultar", "Cliente / CxC", 3));
            vPermisos.Add(new CustomRole("Anticipo Cobrado", "Insertar", "Cliente / CxC", 3));
            vPermisos.Add(new CustomRole("Anticipo Cobrado", "Modificar", "Cliente / CxC", 3));
            vPermisos.Add(new CustomRole("Anticipo Cobrado", "Eliminar", "Cliente / CxC", 3));
            vPermisos.Add(new CustomRole("Anticipo Cobrado", "Anular", "Cliente / CxC", 3));
            vPermisos.Add(new CustomRole("Anticipo Cobrado", "Devolver", "Cliente / CxC", 3));
            vPermisos.Add(new CustomRole("Anticipo Cobrado", "Reimprimir", "Cliente / CxC", 3));
            vPermisos.Add(new CustomRole("Anticipo Cobrado", "Informes", "Cliente / CxC", 3));
            #endregion

            #region Anticipo Pagado
            vPermisos.Add(new CustomRole("Anticipo Pagado", "Consultar", "CxP / Vendedor", 5));
            vPermisos.Add(new CustomRole("Anticipo Pagado", "Insertar", "CxP / Vendedor", 5));
            vPermisos.Add(new CustomRole("Anticipo Pagado", "Modificar", "CxP / Vendedor", 5));
            vPermisos.Add(new CustomRole("Anticipo Pagado", "Eliminar", "CxP / Vendedor", 5));
            vPermisos.Add(new CustomRole("Anticipo Pagado", "Anular", "CxP / Vendedor", 5));
            vPermisos.Add(new CustomRole("Anticipo Pagado", "Devolver", "CxP / Vendedor", 5));
            vPermisos.Add(new CustomRole("Anticipo Pagado", "Reimprimir", "CxP / Vendedor", 5));
            vPermisos.Add(new CustomRole("Anticipo Pagado", "Informes", "CxP / Vendedor", 5));
            #endregion

            #region Caja Registradora
            vPermisos.Add(new CustomRole("Caja Registradora", "Consultar", "Principal", 1));
            vPermisos.Add(new CustomRole("Caja Registradora", "Insertar", "Principal", 1));
            vPermisos.Add(new CustomRole("Caja Registradora", "Modificar", "Principal", 1));
            vPermisos.Add(new CustomRole("Caja Registradora", "Eliminar", "Principal", 1));
            vPermisos.Add(new CustomRole("Caja Registradora", "Abrir Gaveta", "Principal", 1));
            vPermisos.Add(new CustomRole("Caja Registradora", "Cancelar Documento", "Principal", 1));
            vPermisos.Add(new CustomRole("Caja Registradora", "Asignar Caja Registradora", "Principal", 1));
            vPermisos.Add(new CustomRole("Caja Registradora", "Informes", "Principal", 1));
            vPermisos.Add(new CustomRole("Caja Registradora", "Crear Caja Generica", "Principal", 1));
            vPermisos.Add(new CustomRole("Caja Registradora", "Activar Modo Mejorado", "Principal", 1));
            #endregion

            #region Color
            vPermisos.Add(new CustomRole("Color", "Consultar", "Inventario", 4));
            vPermisos.Add(new CustomRole("Color", "Insertar", "Inventario", 4));
            vPermisos.Add(new CustomRole("Color", "Modificar", "Inventario", 4));
            vPermisos.Add(new CustomRole("Color", "Eliminar", "Inventario", 4));
            #endregion

            #region Talla
            vPermisos.Add(new CustomRole("Talla", "Consultar", "Inventario", 4));
            vPermisos.Add(new CustomRole("Talla", "Insertar", "Inventario", 4));
            vPermisos.Add(new CustomRole("Talla", "Modificar", "Inventario", 4));
            vPermisos.Add(new CustomRole("Talla", "Eliminar", "Inventario", 4));
            #endregion

            #region Grupo Talla/Color
            vPermisos.Add(new CustomRole("Grupo Talla/Color", "Consultar", "Inventario", 4));
            vPermisos.Add(new CustomRole("Grupo Talla/Color", "Insertar", "Inventario", 4));
            vPermisos.Add(new CustomRole("Grupo Talla/Color", "Modificar", "Inventario", 4));
            vPermisos.Add(new CustomRole("Grupo Talla/Color", "Eliminar", "Inventario", 4));
            #endregion

            #region Cajero Factura
            vPermisos.Add(new CustomRole("Cajero Factura", "Consultar", "Opciones de Cajero", 9));
            vPermisos.Add(new CustomRole("Cajero Factura", "Insertar", "Opciones de Cajero", 9));
            vPermisos.Add(new CustomRole("Cajero Factura", "Emitir y Cobrar", "Opciones de Cajero", 9));
            vPermisos.Add(new CustomRole("Cajero Factura", "Modificar Precio", "Opciones de Cajero", 9));
            vPermisos.Add(new CustomRole("Cajero Factura", "Emitir Directo", "Opciones de Cajero", 9));
            #endregion

            #region Cajero Cliente
            vPermisos.Add(new CustomRole("Cajero Cliente", "Consultar", "Opciones de Cajero", 9));
            vPermisos.Add(new CustomRole("Cajero Cliente", "Insertar", "Opciones de Cajero", 9));
            vPermisos.Add(new CustomRole("Cajero Cliente", "Modificar", "Opciones de Cajero", 9));
            vPermisos.Add(new CustomRole("Cajero Cliente", "Insertar por Mostrador", "Opciones de Cajero", 9));
            #endregion

            #region Cajero Art Inventario
            vPermisos.Add(new CustomRole("Cajero Artículo", "Consultar", "Opciones de Cajero", 9));
            vPermisos.Add(new CustomRole("Cajero Artículo", "Consultar Corte", "Opciones de Cajero", 9));
            #endregion

            #region Cajero Caja
            vPermisos.Add(new CustomRole("Cajero Caja", "Abrir Gaveta", "Opciones de Cajero", 9));
            #endregion

            #region Usuario Tipo cajero
            vPermisos.Add(new CustomRole("Acceso", "Es Usuario TipoCajero", "Opciones de Cajero", 9));
            #endregion

            #region Importar Exportar SAW
            vPermisos.Add(new CustomRole("Importar/Exportar", "Importar", "Principal", 1));
            vPermisos.Add(new CustomRole("Importar/Exportar", "Exportar", "Principal", 1));
            vPermisos.Add(new CustomRole("Importar/Exportar", "Exportar Pagos Formato Banco", "Principal", 1));
            #endregion

            #region Parametros Activo Fijo
            #endregion

            #region Niveles de Precio
            vPermisos.Add(new CustomRole("Niveles de Precio", "Nivel 1", "Compañía / Parámetros / Niveles de Precio", 2));
            vPermisos.Add(new CustomRole("Niveles de Precio", "Nivel 2", "Compañía / Parámetros / Niveles de Precio", 2));
            vPermisos.Add(new CustomRole("Niveles de Precio", "Nivel 3", "Compañía / Parámetros / Niveles de Precio", 2));
            vPermisos.Add(new CustomRole("Niveles de Precio", "Nivel 4", "Compañía / Parámetros / Niveles de Precio", 2));
            #endregion

            #region Tablas
            vPermisos.Add(new CustomRole("Tablas", "Ingresar Cambio del Día", "Tablas Generales", 10));
            vPermisos.Add(new CustomRole("Tablas", "Reinstalar", "Tablas Generales", 10));
            vPermisos.Add(new CustomRole("Tablas", "Ingresar Cambio de Fechas Adelantadas", "Tablas Generales", 10));
            #endregion

            #region Vehiculos
            vPermisos.Add(new CustomRole("Vehículo", "Consultar", "Vehículos", 11));
            vPermisos.Add(new CustomRole("Vehículo", "Insertar", "Vehículos", 11));
            vPermisos.Add(new CustomRole("Vehículo", "Modificar", "Vehículos", 11));
            vPermisos.Add(new CustomRole("Vehículo", "Eliminar", "Vehículos", 11));
            vPermisos.Add(new CustomRole("Vehículo", "Informes", "Vehículos", 11));
            #endregion

            #region ModeloVehiculo
            vPermisos.Add(new CustomRole("Modelo", "Consultar", "Vehículos", 11));
            vPermisos.Add(new CustomRole("Modelo", "Insertar", "Vehículos", 11));
            vPermisos.Add(new CustomRole("Modelo", "Modificar", "Vehículos", 11));
            vPermisos.Add(new CustomRole("Modelo", "Eliminar", "Vehículos", 11));
            #endregion

            #region MarcaVehiculo
            vPermisos.Add(new CustomRole("Marca", "Consultar", "Vehículos", 11));
            vPermisos.Add(new CustomRole("Marca", "Insertar", "Vehículos", 11));
            vPermisos.Add(new CustomRole("Marca", "Modificar", "Vehículos", 11));
            vPermisos.Add(new CustomRole("Marca", "Eliminar", "Vehículos", 11));
            #endregion

            #region Solicitudes de pago
            vPermisos.Add(new CustomRole("Solicitudes de Pago", "Consultar", "Bancos", 8));
            vPermisos.Add(new CustomRole("Solicitudes de Pago", "Insertar", "Bancos", 8));
            vPermisos.Add(new CustomRole("Solicitudes de Pago", "Modificar", "Bancos", 8));
            vPermisos.Add(new CustomRole("Solicitudes de Pago", "Eliminar", "Bancos", 8));
            vPermisos.Add(new CustomRole("Solicitudes de Pago", "Informes", "Bancos", 8));
            vPermisos.Add(new CustomRole("Solicitudes de Pago", "Procesar", "Bancos", 8));
            vPermisos.Add(new CustomRole("Solicitudes de Pago", "Anular", "Bancos", 8));
            vPermisos.Add(new CustomRole("Solicitudes de Pago", "Reimprimir Cheque", "Bancos", 8));

            #endregion

            #region  Beneficiario
            vPermisos.Add(new CustomRole("Beneficiario", "Consultar", "Bancos", 8));
            vPermisos.Add(new CustomRole("Beneficiario", "Insertar", "Bancos", 8));
            vPermisos.Add(new CustomRole("Beneficiario", "Modificar", "Bancos", 8));
            vPermisos.Add(new CustomRole("Beneficiario", "Eliminar", "Bancos", 8));
            #endregion

            #region  Integracion
            vPermisos.Add(new CustomRole("Integracion", "Consultar", "Bancos", 8));
            vPermisos.Add(new CustomRole("Integracion", "Insertar", "Bancos", 8));
            vPermisos.Add(new CustomRole("Integracion", "Modificar", "Bancos", 8));
            vPermisos.Add(new CustomRole("Integracion", "Eliminar", "Bancos", 8));
            #endregion

            #region  Adelantos
            //vPermisos.Add(new CustomRole("Adelantos", "Consultar", "Caja chica / Rendiciones", 12));
            //vPermisos.Add(new CustomRole("Adelantos", "Insertar", "Caja chica / Rendiciones", 12));
            //vPermisos.Add(new CustomRole("Adelantos", "Modificar", "Caja chica / Rendiciones", 12));
            //vPermisos.Add(new CustomRole("Adelantos", "Eliminar", "Caja chica / Rendiciones", 12));
            //vPermisos.Add(new CustomRole("Adelantos", "Anular", "Caja chica / Rendiciones", 12));
            //vPermisos.Add(new CustomRole("Adelantos", "Devolver", "Caja chica / Rendiciones", 12));
            //vPermisos.Add(new CustomRole("Adelantos", "Reimprimir", "Caja chica / Rendiciones", 12));
            //vPermisos.Add(new CustomRole("Adelantos", "Informes", "Caja chica / Rendiciones", 12));
            #endregion

            #region  Reposicion de Caja Chica

            vPermisos.Add(new CustomRole("Reposicion de Caja Chica", "Consultar", "Caja chica", 12));
            vPermisos.Add(new CustomRole("Reposicion de Caja Chica", "Insertar", "Caja chica", 12));
            vPermisos.Add(new CustomRole("Reposicion de Caja Chica", "Modificar", "Caja chica", 12));
            vPermisos.Add(new CustomRole("Reposicion de Caja Chica", "Eliminar", "Caja chica", 12));
            vPermisos.Add(new CustomRole("Reposicion de Caja Chica", "Anular", "Caja chica", 12));
            vPermisos.Add(new CustomRole("Reposicion de Caja Chica", "Cierre", "Caja chica", 12));
            vPermisos.Add(new CustomRole("Reposicion de Caja Chica", "Informes", "Caja chica", 12));
            vPermisos.Add(new CustomRole("Reposicion de Caja Chica", "Reimprimir Comprobante", "Caja chica", 12));

            #endregion

            #region  Rendiciones
            //vPermisos.Add(new CustomRole("Rendicion", "Consultar", "Caja chica / Rendiciones", 12));
            //vPermisos.Add(new CustomRole("Rendicion", "Insertar", "Caja chica / Rendiciones", 12));
            //vPermisos.Add(new CustomRole("Rendicion", "Modificar", "Caja chica / Rendiciones", 12));
            //vPermisos.Add(new CustomRole("Rendicion", "Eliminar", "Caja chica / Rendiciones", 12));
            //vPermisos.Add(new CustomRole("Rendicion", "Anular", "Caja chica / Rendiciones", 12));
            //vPermisos.Add(new CustomRole("Rendicion", "Cierre", "Caja chica / Rendiciones", 12));
            //vPermisos.Add(new CustomRole("Rendicion", "Informes", "Caja chica / Rendiciones", 12));
            #endregion

            #region Máquina Fiscal
            vPermisos.Add(new CustomRole("Máquina Fiscal", "Activar", "Tablas Generales", 10));
            vPermisos.Add(new CustomRole("Máquina Fiscal", "Desactivar", "Tablas Generales", 10));
            #endregion

            #region Punto de Venta
            vPermisos.Add(new CustomRole("Punto de Venta", "Insertar", "Principal", 1));
            vPermisos.Add(new CustomRole("Punto de Venta", "Modificar", "Principal", 1));
            vPermisos.Add(new CustomRole("Punto de Venta", "Modificar Precio del Item", "Principal", 1));
            vPermisos.Add(new CustomRole("Punto de Venta", "Modificar Descripción del Item", "Principal", 1));
            vPermisos.Add(new CustomRole("Punto de Venta", "Eliminar Item en Factura", "Principal", 1));
            #endregion

            #region Dispositivos Externos
            vPermisos.Add(new CustomRole("Balanza", "Insertar", "Principal", 1));
            vPermisos.Add(new CustomRole("Balanza", "Consultar", "Principal", 1));
            vPermisos.Add(new CustomRole("Balanza", "Modificar", "Principal", 1));
            vPermisos.Add(new CustomRole("Balanza", "Eliminar", "Principal", 1));
            #endregion

            #region Orden de Compra
            vPermisos.Add(new CustomRole("Orden de Compra", "Consultar", "Inventario", 4));
            vPermisos.Add(new CustomRole("Orden de Compra", "Insertar", "Inventario", 4));
            vPermisos.Add(new CustomRole("Orden de Compra", "Modificar", "Inventario", 4));
            vPermisos.Add(new CustomRole("Orden de Compra", "Eliminar", "Inventario", 4));
            vPermisos.Add(new CustomRole("Orden de Compra", "Anular", "Inventario", 4));

            #endregion

            #region Carga Inicial
            vPermisos.Add(new CustomRole("Carga Inicial", "Insertar", "Carga Inicial", 2));
            vPermisos.Add(new CustomRole("Carga Inicial", "Modificar", "Carga Inicial", 2));
            vPermisos.Add(new CustomRole("Carga Inicial", "Consultar", "Carga Inicial", 2));
            #endregion

            #region Lotes de Contalbilidad
            vPermisos.Add(new CustomRole("Lotes", "Consultar", "Mantenimiento", 2));
            vPermisos.Add(new CustomRole("Lotes", "Modificar", "Mantenimiento", 2));
            vPermisos.Add(new CustomRole("Lotes", "Eliminar", "Mantenimiento", 2));
            #endregion

            #region Lista de Materiales 
            vPermisos.Add(new CustomRole("Lista de Materiales", "Consultar", "Produccíon", 13));
            vPermisos.Add(new CustomRole("Lista de Materiales", "Insertar", "Produccíon", 13));
            vPermisos.Add(new CustomRole("Lista de Materiales", "Modificar", "Produccíon", 13));
            vPermisos.Add(new CustomRole("Lista de Materiales", "Eliminar", "Produccíon", 13));
            vPermisos.Add(new CustomRole("Lista de Materiales", "Informes", "Produccíon", 13));
            #endregion

            #region Orden de Produción 
            vPermisos.Add(new CustomRole("Orden De Producción", "Consultar", "Produccíon", 13));
            vPermisos.Add(new CustomRole("Orden De Producción", "Insertar", "Produccíon", 13));
            vPermisos.Add(new CustomRole("Orden De Producción", "Modificar", "Produccíon", 13));
            vPermisos.Add(new CustomRole("Orden De Producción", "Eliminar", "Produccíon", 13));
            vPermisos.Add(new CustomRole("Orden De Producción", "Informes", "Produccíon", 13));
            vPermisos.Add(new CustomRole("Orden De Producción", "Iniciar", "Produccíon", 13));
            vPermisos.Add(new CustomRole("Orden De Producción", "Anular", "Produccíon", 13));
            vPermisos.Add(new CustomRole("Orden De Producción", "Cerrar", "Produccíon", 13));
            #endregion

            return vPermisos;
        }
    }
}
