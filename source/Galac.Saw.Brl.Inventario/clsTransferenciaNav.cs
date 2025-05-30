using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security;
using System.Security.Permissions;
using System.Xml;
using System.Xml.Linq;
using LibGalac.Aos.Base;
using LibGalac.Aos.Brl;
using LibGalac.Aos.Base.Dal;
using Galac.Saw.Ccl.Inventario;
using System.Collections.ObjectModel;
using System.Collections;
using System.Xml.Schema;

namespace Galac.Saw.Brl.Inventario {
    public partial class clsTransferenciaNav : ITransferenciaPdn {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores

        public clsTransferenciaNav() {
        }

        #endregion //Constructores
        #region Metodos Generados

        public bool ActualizarLoteDeInventario(int valConsecutivoCompania, string valNumeroDocumento, bool valInsertar) {
            IList<LoteDeInventario> vListLote = new List<LoteDeInventario>();
            Transferencia transferencia = BuscarTransferencia(valConsecutivoCompania, valNumeroDocumento);
            transferencia.DetailRenglonTransferencia = BuscarDataDetalleTransferencia(valConsecutivoCompania, valNumeroDocumento);
            List<LoteDeInventario> ListLoteDeInventario = BuscarLotes(valConsecutivoCompania, transferencia.DetailRenglonTransferencia);
            List<ExistenciaPorAlmacenDetLoteInv> ListExistenciaPorAlmacenDetLoteInv = new List<ExistenciaPorAlmacenDetLoteInv>();
            foreach (var item in transferencia.DetailRenglonTransferencia) {
                if (item.TipoArticuloInvAsEnum == eTipoArticuloInv.Lote || item.TipoArticuloInvAsEnum == eTipoArticuloInv.LoteFechadeVencimiento || item.TipoArticuloInvAsEnum == eTipoArticuloInv.LoteFechadeElaboracion) {
                    LoteDeInventario lote = ListLoteDeInventario.Where(p => p.CodigoLote == item.CodigoLoteDeInventario && p.CodigoArticulo == item.CodigoArticulo).FirstOrDefault();
                    ExistenciaPorAlmacenDetLoteInv MovExistenciaSalida = CrearExixtenciaPorAlmacenDetLoteInv(valConsecutivoCompania, item.CodigoArticulo,
                        valInsertar ? transferencia.ConsecutivoAlmacenSalida : transferencia.ConsecutivoAlmacenEntrada, lote.Consecutivo, -1 * item.Cantidad);
                    ExistenciaPorAlmacenDetLoteInv MovExistenciaEntrada = CrearExixtenciaPorAlmacenDetLoteInv(valConsecutivoCompania, item.CodigoArticulo,
                        valInsertar ? transferencia.ConsecutivoAlmacenEntrada : transferencia.ConsecutivoAlmacenSalida, lote.Consecutivo, item.Cantidad);
                    ListExistenciaPorAlmacenDetLoteInv.Add(MovExistenciaSalida);
                    ListExistenciaPorAlmacenDetLoteInv.Add(MovExistenciaEntrada);

                    LoteDeInventarioMovimiento MovLoteDeInventarioSalida = CrearLoteDeInventarioMovimiento(valConsecutivoCompania, lote.Consecutivo, transferencia.Fecha,
                        transferencia.NumeroDocumento, eTipodeOperacion.SalidadeInventario, MovExistenciaSalida.Cantidad, valInsertar);
                    LoteDeInventarioMovimiento MovLoteDeInventarioEntrada = CrearLoteDeInventarioMovimiento(valConsecutivoCompania, lote.Consecutivo, transferencia.Fecha,
                        transferencia.NumeroDocumento, eTipodeOperacion.EntradadeInventario , MovExistenciaEntrada.Cantidad, valInsertar);
                    lote.DetailLoteDeInventarioMovimiento.Add(MovLoteDeInventarioSalida);
                    lote.DetailLoteDeInventarioMovimiento.Add(MovLoteDeInventarioEntrada);
                    vListLote.Add(lote);
                }
            }
            if (ListExistenciaPorAlmacenDetLoteInv.Count > 0) {
                IArticuloInventarioPdn insArticulo = new Galac.Saw.Brl.Inventario.clsArticuloInventarioNav();
                insArticulo.ActualizarExistencia(valConsecutivoCompania, ListExistenciaPorAlmacenDetLoteInv);
                ILoteDeInventarioPdn vLotePnd = new clsLoteDeInventarioNav();
                vLotePnd.ActualizarLote(vListLote);
            }
            return true;
        }

        private LoteDeInventarioMovimiento CrearLoteDeInventarioMovimiento(int valConsecutivoCompania, int consecutivoLote, DateTime fecha, string numeroDocumento, eTipodeOperacion tipoOperacion, decimal cantidad, bool valInsertar) {

            return new LoteDeInventarioMovimiento() {
                ConsecutivoCompania = valConsecutivoCompania,
                ConsecutivoLote = consecutivoLote,
                Fecha = fecha,
                ModuloAsEnum = eOrigenLoteInv.Transferencia,
                ConsecutivoDocumentoOrigen = 0,
                NumeroDocumentoOrigen = numeroDocumento,
                StatusDocumentoOrigenAsEnum = valInsertar?eStatusDocOrigenLoteInv.Vigente: eStatusDocOrigenLoteInv.Anulado,
                TipoOperacionAsEnum = tipoOperacion,
                Cantidad = cantidad };
        }

        private ExistenciaPorAlmacenDetLoteInv CrearExixtenciaPorAlmacenDetLoteInv(int valConsecutivoCompania, string codigoArticulo, int consecutivoAlmacen, int consecutivolote, decimal cantidad) {
            return new ExistenciaPorAlmacenDetLoteInv() {
                ConsecutivoCompania = valConsecutivoCompania,
                CodigoArticulo = codigoArticulo,
                ConsecutivoAlmacen = consecutivoAlmacen,
                ConsecutivoLoteInventario = consecutivolote,
                Ubicacion = "",
                Cantidad = cantidad
            };
    }

        private List<LoteDeInventario> BuscarLotes(int valConsecutivoCompania, ObservableCollection<RenglonTransferencia> detailRenglonTransferencia) {
            List<Tuple<string, string>> valListaLote = detailRenglonTransferencia
                .Where(p => p.TipoArticuloInvAsEnum == eTipoArticuloInv.Lote || p.TipoArticuloInvAsEnum == eTipoArticuloInv.LoteFechadeVencimiento || p.TipoArticuloInvAsEnum == eTipoArticuloInv.LoteFechadeElaboracion)
                .Select(p => new Tuple<string, string>(p.CodigoLoteDeInventario, p.CodigoArticulo))
                .Distinct().ToList();

            var condiciones = new List<string>();
            foreach (var item in valListaLote) {
                condiciones.Add($"(CodigoLote = '{item.Item1}' AND CodigoArticulo = '{item.Item2}')");
            }
            string whereClause = string.Join(" OR ", condiciones);
            LibGpParams vParams = new LibGpParams();

            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("SELECT * FROM Saw.LoteDeInventario");
            SQL.AppendLine("WHERE ConsecutivoCompania = @ConsecutivoCompania");
            if (valListaLote.Count > 0) {
                SQL.AppendLine($" AND ({whereClause})");
            }
            XElement XmlData = LibBusiness.ExecuteSelect(SQL.ToString(), vParams.Get(), "", -1);
            return LibParserHelper.ParseToList<LoteDeInventario>(XmlData);

        }

        Transferencia BuscarTransferencia(int valConsecutivoCompania, string valNumeroDocumento) {
            LibGpParams vParams = new LibGpParams();
            vParams.AddInString("NumeroDocumento", valNumeroDocumento, 10);
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("SELECT * FROM dbo.Transferencia");
            SQL.AppendLine("WHERE NumeroDocumento = @NumeroDocumento");
            SQL.AppendLine("AND ConsecutivoCompania = @ConsecutivoCompania");
            XElement XmlData = LibBusiness.ExecuteSelect(SQL.ToString(), vParams.Get(), "", -1);
            List<Transferencia> ListData = LibParserHelper.ParseToList<Transferencia>(XmlData);
            return ListData.FirstOrDefault();
        }

        ObservableCollection<RenglonTransferencia> BuscarDataDetalleTransferencia(int valConsecutivoCompania, string valNumeroDocumento) {
            LibGpParams vParams = new LibGpParams();
            vParams.AddInString("NumeroDocumento", valNumeroDocumento, 10);
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("SELECT * FROM dbo.RenglonTransferencia");
            SQL.AppendLine("WHERE NumeroDocumento = @NumeroDocumento");
            SQL.AppendLine("AND ConsecutivoCompania = @ConsecutivoCompania");
            XElement XmlDataDetalle = LibBusiness.ExecuteSelect(SQL.ToString(), vParams.Get(), "", -1);
            List<RenglonTransferencia> listDetalle = LibParserHelper.ParseToList<RenglonTransferencia>(XmlDataDetalle);
            return new System.Collections.ObjectModel.ObservableCollection<RenglonTransferencia>(listDetalle);
            

        }
        #endregion //Metodos Generados

    } //End of class clsTransferenciaNav

} //End of namespace Galac.Saw.Brl.Inventario

