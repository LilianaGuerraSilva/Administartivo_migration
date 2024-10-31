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
using LibGalac.Aos.Dal;
using LibGalac.Aos.Catching;
using LibGalac.Aos.Base.Report;
using System.Collections.ObjectModel;

namespace Galac.Saw.Brl.Inventario {
    public partial class clsLoteDeInventarioNav: LibBaseNavMaster<IList<LoteDeInventario>, IList<LoteDeInventario>>, ILoteDeInventarioPdn {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores
        public clsLoteDeInventarioNav() {
        }
        #endregion //Constructores
        #region Metodos Generados
        protected override ILibDataMasterComponentWithSearch<IList<LoteDeInventario>, IList<LoteDeInventario>> GetDataInstance() {
            return new Galac.Saw.Dal.Inventario.clsLoteDeInventarioDat();
        }
        #region Miembros de ILibPdn

        bool ILibPdn.CanBeChoosen(string valCallingModule, eAccionSR valAction, string valExtendedAction, XmlDocument valXmlRow) {
            bool vResult = false;
            //ILibDataFKSearch instanciaDal = new Galac.Saw.Dal.Inventario.clsLoteDeInventarioDat();
            switch (valCallingModule) {
                default:
                    vResult = true;
                    break;
            }
            return vResult;
        }

        protected override bool CanBeChoosenForAction(IList<LoteDeInventario> refRecord, eAccionSR valAction) {
            bool vResult = base.CanBeChoosenForAction(refRecord, valAction);
            if (vResult) {
                if (valAction == eAccionSR.Eliminar || valAction == eAccionSR.Modificar) {
                    foreach (LoteDeInventario vLoteDeInventario in refRecord) {
                        bool vEsTipoDeInvLoteFechaDeVcto = new clsLoteDeInventarioNav().EsTipoDeInventarioLoteFechaVencimiento(vLoteDeInventario.ConsecutivoCompania, vLoteDeInventario.CodigoArticulo);
                        bool vTieneMovimientos = new clsLoteDeInventarioNav().ExistenMovimientosDeInvetario(vLoteDeInventario.ConsecutivoCompania, vLoteDeInventario.CodigoArticulo, vLoteDeInventario.Consecutivo);
                        if (valAction == eAccionSR.Modificar && (!vEsTipoDeInvLoteFechaDeVcto || vTieneMovimientos)) {                            
                            throw new GalacAlertException("No es posible " + LibEnumHelper.GetDescription(valAction) + " este lote. Solo se pueden " + LibEnumHelper.GetDescription(valAction) + " los lotes con fecha de vencimiento y sin movimientos.");
                        } else if (valAction == eAccionSR.Eliminar && vTieneMovimientos) {
                            throw new GalacAlertException("No es posible " + LibEnumHelper.GetDescription(valAction) + " este lote. Solo se pueden " + LibEnumHelper.GetDescription(valAction) + " lotes sin movimientos.");
                        }
                    }
                }
            }
            return vResult;
        }

        bool ILibPdn.GetDataForList(string valCallingModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            ILibDataFKSearch instanciaDal = new Galac.Saw.Dal.Inventario.clsLoteDeInventarioDat();
            return instanciaDal.ConnectFk(ref refXmlDocument, eProcessMessageType.SpName, "Saw.Gp_LoteDeInventarioSCH", valXmlParamsExpression);
        }

        System.Xml.Linq.XElement ILibPdn.GetFk(string valCallingModule, StringBuilder valParameters) {
            ILibDataMasterComponent<IList<LoteDeInventario>, IList<LoteDeInventario>> instanciaDal = new Galac.Saw.Dal.Inventario.clsLoteDeInventarioDat();
            return instanciaDal.QueryInfo(eProcessMessageType.SpName, "Saw.Gp_LoteDeInventarioGetFk", valParameters);
        }
        #endregion //Miembros de ILibPdn

        protected override bool RetrieveListInfo(string valModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            bool vResult = false;
            ILibPdn vPdnModule;
            switch (valModule) {
                case "Lote de Inventario":
                    vResult = ((ILibPdn)this).GetDataForList(valModule, ref refXmlDocument, valXmlParamsExpression);
                    break;
                case "Artículo Inventario":
                    vPdnModule = new Galac.Saw.Brl.Inventario.clsArticuloInventarioNav();
                    vResult = vPdnModule.GetDataForList(valModule, ref refXmlDocument, valXmlParamsExpression);
                    break;
                case "Línea de Producto":
                    vPdnModule = new Galac.Saw.Brl.Tablas.clsLineaDeProductoNav();
                    vResult = vPdnModule.GetDataForList(valModule, ref refXmlDocument, valXmlParamsExpression);
                    break;
                default: throw new NotImplementedException();
            }
            return vResult;
        }

        protected override void FillWithForeignInfo(ref IList<LoteDeInventario> refData) {
            FillWithForeignInfoLoteDeInventario(ref refData);
        }
        #region LoteDeInventario

        private void FillWithForeignInfoLoteDeInventario(ref IList<LoteDeInventario> refData) {
            XElement vInfoConexionArticuloInventario = FindInfoArticuloInventario(refData);
            foreach (LoteDeInventario vItem in refData) {
                vItem.CodigoArticulo = LibXml.GetPropertyString(vInfoConexionArticuloInventario, "Codigo");
            }
        }

        private XElement FindInfoArticuloInventario(IList<LoteDeInventario> valData) {
            XElement vXElement = new XElement("GpData");
            foreach (LoteDeInventario vItem in valData) {
                vXElement.Add(FilterLoteDeInventarioByDistinctArticuloInventario(vItem).Descendants("GpResult"));
            }
            ILibPdn insArticuloInventario = new Galac.Saw.Brl.Inventario.clsArticuloInventarioNav();
            XElement vXElementResult = insArticuloInventario.GetFk("Lote De Inventario", ParametersGetFKArticuloInventarioForXmlSubSet(valData[0].ConsecutivoCompania, vXElement));
            return vXElementResult;
        }

        private XElement FilterLoteDeInventarioByDistinctArticuloInventario(LoteDeInventario valMaster) {
            XElement vXElement = new XElement("GpData", new XElement("GpResult", new XElement("Codigo", valMaster.CodigoArticulo)));
            return vXElement;
        }

        private StringBuilder ParametersGetFKArticuloInventarioForXmlSubSet(int valConsecutivoCompania, XElement valXElement) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInXml("XmlData", valXElement);
            vResult = vParams.Get();
            return vResult;
        }
        #endregion //LoteDeInventario

        XElement ILoteDeInventarioPdn.FindByConsecutivoCompaniaCodigoLoteCodigoArticulo(int valConsecutivoCompania, string valCodigoLote, string valCodigoArticulo) {
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInString("CodigoLote", valCodigoLote, 30);
            vParams.AddInString("CodigoArticulo", valCodigoArticulo, 30);
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("SELECT * FROM Saw.LoteDeInventario");
            SQL.AppendLine("WHERE ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("AND CodigoLote = @CodigoLote");
            SQL.AppendLine("AND CodigoArticulo = @CodigoArticulo");
            return LibBusiness.ExecuteSelect(SQL.ToString(), vParams.Get(), "", -1);
        }

        XElement ILoteDeInventarioPdn.FindByConsecutivoCompaniaConsecutivoLoteCodigoArticulo(int valConsecutivoCompania, int valConsecutivoLote, string valCodigoArticulo) {
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInInteger("Consecutivo", valConsecutivoLote);
            vParams.AddInString("CodigoArticulo", valCodigoArticulo, 30);
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("SELECT * FROM Saw.LoteDeInventario");
            SQL.AppendLine("WHERE ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("AND Consecutivo = @Consecutivo");
            SQL.AppendLine("AND CodigoArticulo = @CodigoArticulo");
            return LibBusiness.ExecuteSelect(SQL.ToString(), vParams.Get(), "", -1);
        }

        XElement ILoteDeInventarioPdn.FindByConsecutivoCompaniaCodigoArticulo(int valConsecutivoCompania, string valCodigoArticulo) {
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInString("CodigoArticulo", valCodigoArticulo, 30);
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("SELECT * FROM Saw.LoteDeInventario");
            SQL.AppendLine("WHERE ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("AND CodigoArticulo = @CodigoArticulo");
            return LibBusiness.ExecuteSelect(SQL.ToString(), vParams.Get(), "", -1);
        }
        #endregion //Metodos Generados
        #region Codigo Ejemplo
        /* Codigo de Ejemplo
        bool ILoteDeInventarioPdn.InsertDefaultRecord(int valConsecutivoCompania) {
            ILibDataComponent<IList<LoteDeInventario>, IList<LoteDeInventario>> instanciaDal = new clsLoteDeInventarioDat();
            IList<LoteDeInventario> vLista = new List<LoteDeInventario>();
            LoteDeInventario vCurrentRecord = new Galac.Saw.Dal.InventarioLoteDeInventario();
            vCurrentRecord.ConsecutivoCompania = valConsecutivoCompania;
            vCurrentRecord.ConsecutivoCompania = 0;
            vCurrentRecord.Consecutivo = 0;
            vCurrentRecord.CodigoLote = "";
            vCurrentRecord.CodigoArticulo = "";
            vCurrentRecord.FechaDeElaboracion = LibDate.Today();
            vCurrentRecord.FechaDeVencimiento = LibDate.Today();
            vCurrentRecord.Existencia = 0;
            vCurrentRecord.StatusLoteInvAsEnum = eStatusLoteDeInventario.Vigente;
            vCurrentRecord.NombreOperador = "";
            vCurrentRecord.FechaUltimaModificacion = LibDate.Today();
            vLista.Add(vCurrentRecord);
            return instanciaDal.Insert(vLista).Success;
        }
        */
        #endregion //Codigo Ejemplo

        internal List<LoteDeInventario> ParseToListEntity(XElement valXmlEntity) {
            List<LoteDeInventario> vResult = new List<LoteDeInventario>();
            var vEntity = from vRecord in valXmlEntity.Descendants("GpResult") select vRecord;
            foreach (XElement vItem in vEntity) {
                LoteDeInventario vRecord = new LoteDeInventario();
                vRecord.Clear();
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("ConsecutivoCompania"), null))) {
                    vRecord.ConsecutivoCompania = LibConvert.ToInt(vItem.Element("ConsecutivoCompania"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Consecutivo"), null))) {
                    vRecord.Consecutivo = LibConvert.ToInt(vItem.Element("Consecutivo"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("CodigoLote"), null))) {
                    vRecord.CodigoLote = vItem.Element("CodigoLote").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("CodigoArticulo"), null))) {
                    vRecord.CodigoArticulo = vItem.Element("CodigoArticulo").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("FechaDeElaboracion"), null))) {
                    vRecord.FechaDeElaboracion = LibConvert.ToDate(vItem.Element("FechaDeElaboracion"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("FechaDeVencimiento"), null))) {
                    vRecord.FechaDeVencimiento = LibConvert.ToDate(vItem.Element("FechaDeVencimiento"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Existencia"), null))) {
                    vRecord.Existencia = LibConvert.ToDec(vItem.Element("Existencia"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("StatusLoteInv"), null))) {
                    vRecord.StatusLoteInv = vItem.Element("StatusLoteInv").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("NombreOperador"), null))) {
                    vRecord.NombreOperador = vItem.Element("NombreOperador").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("FechaUltimaModificacion"), null))) {
                    vRecord.FechaUltimaModificacion = LibConvert.ToDate(vItem.Element("FechaUltimaModificacion"));
                }
                vResult.Add(vRecord);
            }
            return vResult;
        }

        bool ILoteDeInventarioPdn.ExisteLoteDeInventario(int valConsecutivoCompania, string valCodigoArticulo, string valLoteDeInventario) {
            bool vResult = false;
            LibGpParams vParam = new LibGpParams();
            vParam.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParam.AddInString("CodigoArticulo", valCodigoArticulo, 30);
            vParam.AddInString("CodigoLote", valLoteDeInventario, 30);
            vResult = new LibDatabase().ExistsRecord("Saw.LoteDeInventario", "CodigoLote", vParam.Get());
            return vResult;
        }
        bool ILoteDeInventarioPdn.ExisteLoteDeInventario(int valConsecutivoCompania, string valCodigoArticulo, int valConsecutivoLoteDeInventario) {
            bool vResult = false;
            LibGpParams vParam = new LibGpParams();
            vParam.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParam.AddInString("CodigoArticulo", valCodigoArticulo, 30);
            vParam.AddInInteger("Consecutivo", valConsecutivoLoteDeInventario);
            vResult = new LibDatabase().ExistsRecord("Saw.LoteDeInventario", "CodigoLote", vParam.Get());
            return vResult;
        }

        bool ILoteDeInventarioPdn.ActualizarLote(IList<LoteDeInventario> valListaLote) {
            bool vResult = true;
            if (valListaLote != null && valListaLote.Count() > 0) {
                foreach (LoteDeInventario vItemLote in valListaLote) {
                    vResult = UpdateLote(vItemLote);
                    if (vResult) {
                        vResult = InsertDetail(vItemLote);
                    }
                }
            }
            return vResult;
        }

        bool ILoteDeInventarioPdn.ActualizarLoteYReversarMov(IList<LoteDeInventario> valListaLote, eOrigenLoteInv valOrigen, int valConsecutivoDocumentoOrigen, string valNumeroDocumentoOrigen, bool valSoloAnulados) {
            bool vResult = true;
            if (valListaLote != null && valListaLote.Count() > 0) {
                foreach (LoteDeInventario vItemLote in valListaLote) {
                    vResult = UpdateLote(vItemLote);
                    if (vResult) {
                        vResult = DeleteDetail(vItemLote, valOrigen, valConsecutivoDocumentoOrigen, valNumeroDocumentoOrigen, valSoloAnulados);
                    }
                }
            }
            return vResult;
        }

        private static bool UpdateLote(LoteDeInventario vItemLote) {
            QAdvSql insSql = new QAdvSql("");
            bool vResult;
            StringBuilder vSqlUpdateLote = new StringBuilder();
            vSqlUpdateLote.AppendLine("UPDATE Saw.LoteDeInventario SET ");
            vSqlUpdateLote.AppendLine("Existencia = " + insSql.ToSqlValue(vItemLote.Existencia));
            vSqlUpdateLote.AppendLine(",NombreOperador = " + insSql.ToSqlValue(((CustomIdentity)System.Threading.Thread.CurrentPrincipal.Identity).Login));
            vSqlUpdateLote.AppendLine(",FechaUltimaModificacion = " + insSql.ToSqlValue(LibDate.Today()));
            vSqlUpdateLote.AppendLine(" WHERE ConsecutivoCompania = " + insSql.ToSqlValue(vItemLote.ConsecutivoCompania));
            vSqlUpdateLote.AppendLine(" AND Consecutivo = " + insSql.ToSqlValue(vItemLote.Consecutivo));
            vResult = new LibDataScope().Execute(vSqlUpdateLote.ToString()) > 0;
            return vResult;
        }

        private static bool InsertDetail(LoteDeInventario vItemLote) {
            bool vResult = false;
            QAdvSql insSql = new QAdvSql("");
            if (vItemLote.DetailLoteDeInventarioMovimiento != null && vItemLote.DetailLoteDeInventarioMovimiento.Count > 0) {
                foreach (LoteDeInventarioMovimiento vItemLoteMov in vItemLote.DetailLoteDeInventarioMovimiento) {
                    LoteDeInventarioMovimiento vLoteMov = vItemLoteMov;
                    StringBuilder vSqlInsertMovimiento = new StringBuilder();
                    vSqlInsertMovimiento.AppendLine("INSERT INTO Saw.LoteDeInventarioMovimiento");
                    vSqlInsertMovimiento.AppendLine("(ConsecutivoCompania, ConsecutivoLote, Consecutivo, Fecha, Modulo, Cantidad, ConsecutivoDocumentoOrigen, NumeroDocumentoOrigen, StatusDocumentoOrigen, TipoOperacion)");
                    vSqlInsertMovimiento.AppendLine("VALUES(");
                    vSqlInsertMovimiento.AppendLine(insSql.ToSqlValue(vLoteMov.ConsecutivoCompania) + ", ");
                    vSqlInsertMovimiento.AppendLine(insSql.ToSqlValue(vLoteMov.ConsecutivoLote) + ", ");
                    vSqlInsertMovimiento.AppendLine(insSql.ToSqlValue(new clsLoteDeInventarioMovimientoNav().ProximoConsecutivo(vItemLoteMov.ConsecutivoCompania, vItemLoteMov.ConsecutivoLote)) + ", ");
                    vSqlInsertMovimiento.AppendLine(insSql.ToSqlValue(vLoteMov.Fecha) + ", ");
                    vSqlInsertMovimiento.AppendLine(insSql.ToSqlValue(vLoteMov.ModuloAsDB) + ", ");
                    vSqlInsertMovimiento.AppendLine(insSql.ToSqlValue(vLoteMov.Cantidad) + ", ");
                    vSqlInsertMovimiento.AppendLine(insSql.ToSqlValue(vLoteMov.ConsecutivoDocumentoOrigen) + ", ");
                    vSqlInsertMovimiento.AppendLine(insSql.ToSqlValue(vLoteMov.NumeroDocumentoOrigen) + ", ");
                    vSqlInsertMovimiento.AppendLine(insSql.ToSqlValue(vLoteMov.StatusDocumentoOrigenAsDB) + ",");
                    vSqlInsertMovimiento.AppendLine(insSql.ToSqlValue(vLoteMov.TipoOperacionAsDB));
                    vSqlInsertMovimiento.AppendLine(")");
                    vResult = new LibDataScope().ExecuteWithScope(vSqlInsertMovimiento.ToString()) > 0;
                }
            }
            return vResult;
        }

        private static bool DeleteDetail(LoteDeInventario vItemLote, eOrigenLoteInv valOrigen, int valConsecutivoDocumentoOrigen, string valNumeroDocumentoOrigen, bool valSoloAnulados) {
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", vItemLote.ConsecutivoCompania);
            vParams.AddInInteger("ConsecutivoLote", vItemLote.Consecutivo);
            vParams.AddInEnum("Modulo", (int)valOrigen);
            bool vResult = true;
            QAdvSql insSql = new QAdvSql("");
            StringBuilder vSqlInsertMovimiento = new StringBuilder();
            vSqlInsertMovimiento.AppendLine("DELETE Saw.LoteDeInventarioMovimiento");
            vSqlInsertMovimiento.AppendLine(" WHERE ");
            vSqlInsertMovimiento.AppendLine("ConsecutivoCompania = @ConsecutivoCompania");
            vSqlInsertMovimiento.AppendLine(" AND ConsecutivoLote = @ConsecutivoLote");
            vSqlInsertMovimiento.AppendLine(" AND Modulo = @Modulo");
            if (valConsecutivoDocumentoOrigen == 0) {
                vParams.AddInString("NumeroDocumentoOrigen", valNumeroDocumentoOrigen, 30);
                vSqlInsertMovimiento.AppendLine(" AND NumeroDocumentoOrigen = @NumeroDocumentoOrigen");
            } else {
                vParams.AddInInteger("ConsecutivoDocumentoOrigen", valConsecutivoDocumentoOrigen);
                vSqlInsertMovimiento.AppendLine(" AND ConsecutivoDocumentoOrigen = @ConsecutivoDocumentoOrigen");
            }
            if (valSoloAnulados) {
                vParams.AddInEnum("StatusDocumentoOrigen", (int)eStatusDocOrigenLoteInv.Anulado);
                vSqlInsertMovimiento.AppendLine(" AND StatusDocumentoOrigen = @StatusDocumentoOrigen");
            }
            LibBusiness.ExecuteUpdateOrDelete(vSqlInsertMovimiento.ToString(), vParams.Get(), "", 0);
            return vResult;
        }

        internal decimal DisponibilidadDeArticulo(int valConsecutivoCompania, string valCodigoArticulo, int valConsecutivoLoteDeInventario) {
            decimal vResult = 0;
            StringBuilder SQL = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            string vTabla = string.Empty;
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInString("CodigoArticulo", valCodigoArticulo, 30);
            vParams.AddInInteger("Consecutivo", valConsecutivoLoteDeInventario);

            SQL.AppendLine(" SELECT CodigoArticulo, Existencia AS Disponibilidad");
            SQL.AppendLine(" FROM Saw.LoteDeInventario ");
            SQL.AppendLine(" WHERE Saw.LoteDeInventario.CodigoArticulo = @CodigoArticulo ");
            SQL.AppendLine(" AND Saw.LoteDeInventario.ConsecutivoCompania = @ConsecutivoCompania ");
            SQL.AppendLine(" AND Saw.LoteDeInventario.Consecutivo = @Consecutivo ");

            XElement xRecord = LibBusiness.ExecuteSelect(SQL.ToString(), vParams.Get(), "", 0);
            if (xRecord != null) {
                vResult = LibImportData.ToDec(LibXml.GetPropertyString(xRecord, "Disponibilidad"), 3);
            }
            return vResult;
        }

        internal XElement DisponibilidadDeArticuloPorLote(int valConsecutivoCompania, XElement valDataArticulo) {
            StringBuilder vSQL = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            QAdvSql InsSql = new QAdvSql("");
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInXml("XmlData", valDataArticulo);
            vSQL.AppendLine(" DECLARE @hdoc int ");
            vSQL.AppendLine(" EXEC sp_xml_preparedocument @hdoc OUTPUT, @XmlData ");
            vSQL.AppendLine("SELECT ");
            vSQL.AppendLine("	 XmlDoc.CodigoArticulo, XmlDoc.ConsecutivoLoteDeInventario, ISNULL(Existencia, 0)  AS Cantidad  ");
            vSQL.AppendLine("FROM   OPENXML(@hdoc, 'GpData/GpResult', 2) ");
            vSQL.AppendLine(" WITH( ");
            vSQL.AppendLine("	   ConsecutivoLoteDeInventario " + InsSql.NumericTypeForDb(10, 0) + ",");
            vSQL.AppendLine("	   CodigoArticulo " + InsSql.VarCharTypeForDb(30) + ") AS XmlDoc");
            vSQL.AppendLine(" LEFT JOIN Saw.LoteDeInventario ON XmlDoc.CodigoArticulo = Saw.LoteDeInventario.CodigoArticulo AND XmlDoc.ConsecutivoLoteDeInventario = Saw.LoteDeInventario.Consecutivo");
            vSQL.AppendLine(" AND Saw.LoteDeInventario.ConsecutivoCompania = @ConsecutivoCompania ");
            vSQL.AppendLine(" EXEC sp_xml_removedocument @hdoc");
            XElement vData = LibBusiness.ExecuteSelect(vSQL.ToString(), vParams.Get(), "", 0);
            return vData;
        }

        bool EsTipoDeInventarioLoteFechaVencimiento(int valConsecutivoCompania, string valCodigoArticulo) {
            bool vResult = false;
            LibDatabase insDb = new LibDatabase();
            StringBuilder SQL = new StringBuilder();
                
            SQL.AppendLine(" SELECT TipoArticuloInv");
            SQL.AppendLine(" FROM ArticuloInventario ");
            SQL.AppendLine(" WHERE Codigo = " + insDb.InsSql.ToSqlValue(valCodigoArticulo));
            SQL.AppendLine(" AND ConsecutivoCompania = " + insDb.InsSql.ToSqlValue(valConsecutivoCompania));
            SQL.AppendLine(" AND TipoArticuloInv = " + insDb.InsSql.EnumToSqlValue((int)eTipoArticuloInv.LoteFechadeVencimiento));

            vResult = insDb.RecordCountOfSql(SQL.ToString()) > 0;

            return vResult;
        }

        bool ExistenMovimientosDeInvetario(int valConsecutivoCompania, string valCodigoArticulo, int valConsecutivoLoteDeInventario) {
            bool vResult = false;
            StringBuilder SQL = new StringBuilder();
            LibDatabase insDb = new LibDatabase();

            SQL.AppendLine(" SELECT LoteInvMov.Consecutivo ");
            SQL.AppendLine(" FROM Saw.LoteDeInventario AS LoteInv INNER JOIN Saw.LoteDeInventarioMovimiento AS LoteInvMov ON  ");
            SQL.AppendLine(" LoteInv.ConsecutivoCompania = LoteInvMov.ConsecutivoCompania AND LoteInv.Consecutivo = LoteInvMov.ConsecutivoLote  ");
            SQL.AppendLine(" WHERE LoteInv.CodigoArticulo = " + insDb.InsSql.ToSqlValue(valCodigoArticulo));
            SQL.AppendLine(" AND LoteInv.Consecutivo = " + insDb.InsSql.ToSqlValue(valConsecutivoLoteDeInventario));
            SQL.AppendLine(" AND LoteInv.ConsecutivoCompania = " + insDb.InsSql.ToSqlValue(valConsecutivoCompania));

            vResult = insDb.RecordCountOfSql(SQL.ToString()) > 0;

            return vResult;
        }

        bool ILoteDeInventarioPdn.RecalcularMovimientosDeLoteDeInventario(int valConsecutivoCompania, eCantidadAImprimir valCantidadArticulos, string valCodigoArticulo, eCantidadAImprimir valCantidadLineas, string valLineaDeProducto) {
            bool vResult =false;
            if (valCantidadArticulos == eCantidadAImprimir.One) {
                vResult = RecalcularMovimientosDeInventarioParaUnArticulo(valConsecutivoCompania, valCodigoArticulo);
            } else {
                if (valCantidadLineas == eCantidadAImprimir.One) {
                    XElement vXmlResult = LibBusiness.ExecuteSelect(SqlArticulosARecalcular(valConsecutivoCompania, valLineaDeProducto), new StringBuilder(), "", 0);
                    if (vXmlResult != null) {
                        vResult = RecalcularConjuntoDeArticulos(vXmlResult, valConsecutivoCompania);
                    }
                } else {
                    XElement vXmlResult = LibBusiness.ExecuteSelect(SqlArticulosARecalcular(valConsecutivoCompania, ""), new StringBuilder(), "", 0);
                    if (vXmlResult != null) {
                        vResult = RecalcularConjuntoDeArticulos(vXmlResult, valConsecutivoCompania);
                    }
                }
            }
            return vResult;
        }

        string SqlArticulosARecalcular(int valConsecutivoCompania, string valLineaDeProducto) {
            StringBuilder vSql = new StringBuilder();
            LibDatabase insDb = new LibDatabase();
            vSql.AppendLine("SELECT Codigo FROM ArticuloInventario ");
            vSql.AppendLine("WHERE ConsecutivoCompania = " + insDb.InsSql.ToSqlValue(valConsecutivoCompania));
            vSql.AppendLine("AND TipoArticuloInv IN (" + insDb.InsSql.EnumToSqlValue((int)eTipoArticuloInv.Lote) + ", " + insDb.InsSql.EnumToSqlValue((int)eTipoArticuloInv.LoteFechadeVencimiento) + ")");
            if (!LibString.IsNullOrEmpty(valLineaDeProducto)) {
                vSql.AppendLine("AND LineaDeProducto = " + insDb.InsSql.ToSqlValue(valLineaDeProducto));
            }
            return vSql.ToString();
        }

        bool RecalcularConjuntoDeArticulos(XElement valXmlEntity, int valConsecutivoCompania) {
            bool vResult = false;
            var vEntity = from vRecord in valXmlEntity.Descendants("GpResult") select vRecord;
            foreach (XElement vItem in vEntity) {
                string vCodigoArticulo = string.Empty;
                if (!System.NullReferenceException.ReferenceEquals(vItem.Element("Codigo"), null)) {
                    vCodigoArticulo = LibConvert.ToStr(vItem.Element("Codigo").Value);
                }
                if (!LibString.IsNullOrEmpty(vCodigoArticulo)) {
                    vResult = RecalcularMovimientosDeInventarioParaUnArticulo(valConsecutivoCompania, vCodigoArticulo);
                }
            }
            return vResult;
        }

        private bool RecalcularMovimientosDeInventarioParaUnArticulo(int valConsecutivoCompania, string valCodigoArticulo) {
            bool vResult = false;
            if (EsTipoDeInventarioLote(valConsecutivoCompania, valCodigoArticulo)) {
                LibDatabase insDb = new LibDatabase();
                insDb.Execute("UPDATE articuloInventario SET Existencia = 0 WHERE Codigo = " + insDb.InsSql.ToSqlValue(valCodigoArticulo) + " AND ConsecutivoCompania = " + insDb.InsSql.ToSqlValue(valConsecutivoCompania));
                insDb.Execute("UPDATE Saw.LoteDeInventario SET Existencia = 0 WHERE CodigoArticulo = " + insDb.InsSql.ToSqlValue(valCodigoArticulo) + " AND ConsecutivoCompania = " + insDb.InsSql.ToSqlValue(valConsecutivoCompania));

                StringBuilder vSql = new StringBuilder();
                vSql.AppendLine("DELETE FROM Saw.LoteDeInventarioMovimiento ");
                vSql.AppendLine("FROM Saw.LoteDeInventarioMovimiento INNER JOIN");
                vSql.AppendLine("Saw.LoteDeInventario ON Saw.LoteDeInventarioMovimiento.ConsecutivoCompania = Saw.LoteDeInventario.ConsecutivoCompania ");
                vSql.AppendLine("AND Saw.LoteDeInventarioMovimiento.ConsecutivoLote = Saw.LoteDeInventario.Consecutivo");
                vSql.AppendLine("WHERE Saw.LoteDeInventarioMovimiento.ConsecutivoCompania = " + insDb.InsSql.ToSqlValue(valConsecutivoCompania));
                vSql.AppendLine("AND Saw.LoteDeInventario.CodigoArticulo = " + insDb.InsSql.ToSqlValue(valCodigoArticulo));
                insDb.Execute(vSql.ToString());

                XElement vRecordLote = LibBusiness.ExecuteSelect(SqlLotesPorArticulo(valConsecutivoCompania, valCodigoArticulo), new StringBuilder(), "", 0);
                if (vRecordLote != null) {
                    List<LoteDeInventario> vListLotes = ParseToListEntity(vRecordLote);
                    if (vListLotes != null) {
                        foreach (LoteDeInventario vLote in vListLotes) {
                            if (vLote != null) {
                                XElement vRecord = LibBusiness.ExecuteSelect(SqlMovimientosDeInventarioDesdeModulos(valConsecutivoCompania, valCodigoArticulo, vLote.Consecutivo), new StringBuilder(), "", 0);
                                if (vRecord != null) {
                                    ObservableCollection<LoteDeInventarioMovimiento> vListMovimientos = new clsLoteDeInventarioMovimientoNav().ParseToListObservableCollectionEntity(vRecord);
                                    vLote.DetailLoteDeInventarioMovimiento = vListMovimientos;
                                    InsertDetail(vLote);
                                }
                                ActualizaExistenciaLote(valConsecutivoCompania, vLote.Consecutivo);
                            }
                        }
                    }
                    ActualizaExistenciaArticulo(valConsecutivoCompania, valCodigoArticulo);
                }
                vResult = true;
            }
            return vResult;
        }

        private string SqlLotesPorArticulo(int valConsecutivoCompania, string valCodigoArticulo) {
            QAdvSql insSql = new QAdvSql("");
            string vSql = "SELECT * FROM Saw.LoteDeInventario WHERE ConsecutivoCompania = " + insSql.ToSqlValue(valConsecutivoCompania) + " AND CodigoArticulo = " + insSql.ToSqlValue(valCodigoArticulo) + " ORDER BY Consecutivo";
            return vSql;
        }

        private string SqlMovimientosDeInventarioDesdeModulos(int valConsecutivoCompania, string valCodigoArticulo, int valConseuctivoLote) {
            StringBuilder vSql = new StringBuilder();
            vSql.AppendLine(SqlMovimientosNotaES(valConsecutivoCompania, valCodigoArticulo, valConseuctivoLote));
            vSql.AppendLine(" UNION ");
            vSql.AppendLine(SqlMovimientosFactura(valConsecutivoCompania, valCodigoArticulo, valConseuctivoLote));
            vSql.AppendLine(" UNION ");
            vSql.AppendLine(SqlMovimientosNC(valConsecutivoCompania, valCodigoArticulo, valConseuctivoLote));
            vSql.AppendLine(" UNION ");
            vSql.AppendLine(SqlMovimientosND(valConsecutivoCompania, valCodigoArticulo, valConseuctivoLote));
            vSql.AppendLine(" UNION ");
            vSql.AppendLine(SqlMovimientosNE(valConsecutivoCompania, valCodigoArticulo, valConseuctivoLote));
            vSql.AppendLine(" UNION ");
            vSql.AppendLine(SqlMovimientosCompra(valConsecutivoCompania, valCodigoArticulo, valConseuctivoLote));
            vSql.AppendLine(" UNION ");
            vSql.AppendLine(SqlMovimientosConteoFisico(valConsecutivoCompania, valCodigoArticulo, valConseuctivoLote));
            vSql.AppendLine(" ORDER BY ColOrden, Fecha, ConsecutivoDocumentoOrigen, NumeroDocumentoOrigen");
            return vSql.ToString();
        }

        private string SqlMovimientosNotaES(int valConsecutivoCompania, string valCodigoArticulo, int valConseuctivoLote) {
            StringBuilder vSql = new StringBuilder();
            QAdvSql insSql = new QAdvSql("");
            //NES Vigentes
            vSql.AppendLine("SELECT 1 AS ColOrden, NES.ConsecutivoCompania, LoteInv.Consecutivo AS ConsecutivoLote, 0 AS Consecutivo, NES.Fecha, ");
            vSql.AppendLine("(CASE WHEN NES.TipoNotaProduccion = '0' THEN " + insSql.EnumToSqlValue((int)eOrigenLoteInv.NotaEntradaSalida) + " ELSE " + insSql.EnumToSqlValue((int)eOrigenLoteInv.Produccion) + " END) AS Modulo, ");
            vSql.AppendLine("(CASE WHEN NES.TipodeOperacion = " + insSql.EnumToSqlValue((int)eTipodeOperacion.EntradadeInventario) + " THEN RNES.Cantidad ELSE RNES.Cantidad * -1 END) AS Cantidad, ");
            vSql.AppendLine("(CASE WHEN NES.TipodeOperacion = '0' THEN '0' ELSE '1' END) AS TipoOperacion, ");
            vSql.AppendLine("0 AS ConsecutivoDocumentoOrigen, NES.NumeroDocumento AS NumeroDocumentoOrigen, " + insSql.EnumToSqlValue((int)eStatusDocOrigenLoteInv.Vigente) + " AS StatusDocumentoOrigen");
            vSql.AppendLine("FROM NotaDeEntradaSalida NES INNER JOIN RenglonNotaES RNES ON NES.ConsecutivoCompania = RNES.ConsecutivoCompania AND NES.NumeroDocumento = RNES.NumeroDocumento");
            vSql.AppendLine("INNER JOIN Saw.LoteDeInventario LoteInv ON RNES.ConsecutivoCompania = LoteInv.ConsecutivoCompania AND RNES.LoteDeInventario = LoteInv.CodigoLote");
            vSql.AppendLine("INNER JOIN ArticuloInventario AI ON AI.ConsecutivoCompania = RNES.ConsecutivoCompania AND AI.Codigo = RNES.CodigoArticulo");
            vSql.AppendLine("WHERE NES.ConsecutivoCompania = " + insSql.ToSqlValue(valConsecutivoCompania));
            vSql.AppendLine("AND AI.TipoArticuloInv IN (" + insSql.EnumToSqlValue((int)eTipoArticuloInv.Lote) + ", " + insSql.EnumToSqlValue((int)eTipoArticuloInv.LoteFechadeVencimiento) + ")");
            vSql.AppendLine("AND AI.Codigo = " + insSql.ToSqlValue(valCodigoArticulo));
            vSql.AppendLine("AND NES.StatusNotaEntradaSalida = " + insSql.EnumToSqlValue((int)eStatusNotaEntradaSalida.Vigente));
            vSql.AppendLine("AND LoteInv.Consecutivo = " + insSql.ToSqlValue(valConseuctivoLote));
            vSql.AppendLine(" UNION ");
            //NES Anuladas y su origen vigente
            vSql.AppendLine("SELECT 2 AS ColOrden, NES.ConsecutivoCompania, LoteInv.Consecutivo AS ConsecutivoLote, 0 AS Consecutivo, NES.Fecha, ");
            vSql.AppendLine("(CASE WHEN NES.TipoNotaProduccion = '0' THEN " + insSql.EnumToSqlValue((int)eOrigenLoteInv.NotaEntradaSalida) + " ELSE " + insSql.EnumToSqlValue((int)eOrigenLoteInv.Produccion) + " END) AS Modulo, ");
            vSql.AppendLine("(CASE WHEN NES.TipodeOperacion = " + insSql.EnumToSqlValue((int)eTipodeOperacion.EntradadeInventario) + " THEN RNES.Cantidad ELSE RNES.Cantidad * -1 END) AS Cantidad, ");
            vSql.AppendLine("(CASE WHEN NES.TipodeOperacion = '0' THEN '0' ELSE '1' END) AS TipoOperacion, ");
            vSql.AppendLine("0 AS ConsecutivoDocumentoOrigen, NES.NumeroDocumento AS NumeroDocumentoOrigen, " + insSql.EnumToSqlValue((int)eStatusDocOrigenLoteInv.Vigente) + " AS StatusDocumentoOrigen");
            vSql.AppendLine("FROM NotaDeEntradaSalida NES INNER JOIN RenglonNotaES RNES ON NES.ConsecutivoCompania = RNES.ConsecutivoCompania AND NES.NumeroDocumento = RNES.NumeroDocumento");
            vSql.AppendLine("INNER JOIN Saw.LoteDeInventario LoteInv ON RNES.ConsecutivoCompania = LoteInv.ConsecutivoCompania AND RNES.LoteDeInventario = LoteInv.CodigoLote");
            vSql.AppendLine("INNER JOIN ArticuloInventario AI ON AI.ConsecutivoCompania = RNES.ConsecutivoCompania AND AI.Codigo = RNES.CodigoArticulo");
            vSql.AppendLine("WHERE NES.ConsecutivoCompania = " + insSql.ToSqlValue(valConsecutivoCompania));
            vSql.AppendLine("AND AI.TipoArticuloInv IN (" + insSql.EnumToSqlValue((int)eTipoArticuloInv.Lote) + ", " + insSql.EnumToSqlValue((int)eTipoArticuloInv.LoteFechadeVencimiento) + ")");
            vSql.AppendLine("AND AI.Codigo = " + insSql.ToSqlValue(valCodigoArticulo));
            vSql.AppendLine("AND NES.StatusNotaEntradaSalida = " + insSql.EnumToSqlValue((int)eStatusNotaEntradaSalida.Anulada));
            vSql.AppendLine("AND LoteInv.Consecutivo = " + insSql.ToSqlValue(valConseuctivoLote));
            vSql.AppendLine(" UNION ");
            vSql.AppendLine("SELECT 2 AS ColOrden, NES.ConsecutivoCompania, LoteInv.Consecutivo AS ConsecutivoLote, 0 AS Consecutivo, NES.Fecha, ");
            vSql.AppendLine("(CASE WHEN NES.TipoNotaProduccion = '0' THEN " + insSql.EnumToSqlValue((int)eOrigenLoteInv.NotaEntradaSalida) + " ELSE " + insSql.EnumToSqlValue((int)eOrigenLoteInv.Produccion) + " END) AS Modulo, ");
            vSql.AppendLine("(CASE WHEN NES.TipodeOperacion = " + insSql.EnumToSqlValue((int)eTipodeOperacion.EntradadeInventario) + " THEN RNES.Cantidad * -1 ELSE RNES.Cantidad END) AS Cantidad, ");
            vSql.AppendLine("(CASE WHEN NES.TipodeOperacion = '0' THEN '1' ELSE '0' END) AS TipoOperacion, ");
            vSql.AppendLine("0 AS ConsecutivoDocumentoOrigen, NES.NumeroDocumento AS NumeroDocumentoOrigen, " + insSql.EnumToSqlValue((int)eStatusDocOrigenLoteInv.Anulado) + " AS StatusDocumentoOrigen");
            vSql.AppendLine("FROM NotaDeEntradaSalida NES INNER JOIN RenglonNotaES RNES ON NES.ConsecutivoCompania = RNES.ConsecutivoCompania AND NES.NumeroDocumento = RNES.NumeroDocumento");
            vSql.AppendLine("INNER JOIN Saw.LoteDeInventario LoteInv ON RNES.ConsecutivoCompania = LoteInv.ConsecutivoCompania AND RNES.LoteDeInventario = LoteInv.CodigoLote");
            vSql.AppendLine("INNER JOIN ArticuloInventario AI ON AI.ConsecutivoCompania = RNES.ConsecutivoCompania AND AI.Codigo = RNES.CodigoArticulo");
            vSql.AppendLine("WHERE NES.ConsecutivoCompania = " + insSql.ToSqlValue(valConsecutivoCompania));
            vSql.AppendLine("AND AI.TipoArticuloInv IN (" + insSql.EnumToSqlValue((int)eTipoArticuloInv.Lote) + ", " + insSql.EnumToSqlValue((int)eTipoArticuloInv.LoteFechadeVencimiento) + ")");
            vSql.AppendLine("AND AI.Codigo = " + insSql.ToSqlValue(valCodigoArticulo));
            vSql.AppendLine("AND NES.StatusNotaEntradaSalida = " + insSql.EnumToSqlValue((int)eStatusNotaEntradaSalida.Anulada));
            vSql.AppendLine("AND LoteInv.Consecutivo = " + insSql.ToSqlValue(valConseuctivoLote));
            return vSql.ToString();
        }

        private string SqlMovimientosFactura(int valConsecutivoCompania, string valCodigoArticulo, int valConseuctivoLote) {
            StringBuilder vSql = new StringBuilder();
            QAdvSql insSql = new QAdvSql("");
            //Facturas vigentes
            vSql.AppendLine("SELECT 3 AS ColOrden, F.ConsecutivoCompania, LoteInv.Consecutivo AS ConsecutivoLote, 0 AS Consecutivo, F.Fecha, ");
            vSql.AppendLine(insSql.EnumToSqlValue((int)eOrigenLoteInv.Factura) + " AS Modulo, ");
            vSql.AppendLine("RF.Cantidad * -1 AS Cantidad, ");
            vSql.AppendLine("'1' AS TipoOperacion, ");
            vSql.AppendLine("0 AS ConsecutivoDocumentoOrigen, F.Numero AS NumeroDocumentoOrigen, " + insSql.EnumToSqlValue((int)eStatusDocOrigenLoteInv.Vigente) + " AS StatusDocumentoOrigen");
            vSql.AppendLine("FROM factura F INNER JOIN renglonFactura RF ON F.ConsecutivoCompania = RF.ConsecutivoCompania AND F.Numero = RF.NumeroFactura AND F.TipoDeDocumento = RF.TipoDeDocumento");
            vSql.AppendLine("INNER JOIN Saw.LoteDeInventario LoteInv ON RF.ConsecutivoCompania = LoteInv.ConsecutivoCompania AND RF.LoteDeInventario = LoteInv.CodigoLote");
            vSql.AppendLine("INNER JOIN ArticuloInventario AI ON AI.ConsecutivoCompania = RF.ConsecutivoCompania AND AI.Codigo = RF.Articulo");
            vSql.AppendLine("WHERE F.ConsecutivoCompania = " + insSql.ToSqlValue(valConsecutivoCompania));
            vSql.AppendLine("AND AI.TipoArticuloInv IN (" + insSql.EnumToSqlValue((int)eTipoArticuloInv.Lote) + ", " + insSql.EnumToSqlValue((int)eTipoArticuloInv.LoteFechadeVencimiento) + ")");
            vSql.AppendLine("AND AI.Codigo = " + insSql.ToSqlValue(valCodigoArticulo));
            vSql.AppendLine("AND F.TipoDeDocumento = '0' AND F.StatusFactura = '0'");
            vSql.AppendLine("AND LoteInv.Consecutivo = " + insSql.ToSqlValue(valConseuctivoLote));
            vSql.AppendLine("UNION");
            //Facturas Anuladas y su origen vigente
            vSql.AppendLine("SELECT 4 AS ColOrden, F.ConsecutivoCompania, LoteInv.Consecutivo AS ConsecutivoLote, 0 AS Consecutivo, F.Fecha, ");
            vSql.AppendLine(insSql.EnumToSqlValue((int)eOrigenLoteInv.Factura) + " AS Modulo, ");
            vSql.AppendLine("RF.Cantidad * -1 AS Cantidad, ");
            vSql.AppendLine("'1' AS TipoOperacion, ");
            vSql.AppendLine("0 AS ConsecutivoDocumentoOrigen, F.Numero AS NumeroDocumentoOrigen, " + insSql.EnumToSqlValue((int)eStatusDocOrigenLoteInv.Vigente) + " AS StatusDocumentoOrigen");
            vSql.AppendLine("FROM factura F INNER JOIN renglonFactura RF ON F.ConsecutivoCompania = RF.ConsecutivoCompania AND F.Numero = RF.NumeroFactura AND F.TipoDeDocumento = RF.TipoDeDocumento");
            vSql.AppendLine("INNER JOIN Saw.LoteDeInventario LoteInv ON RF.ConsecutivoCompania = LoteInv.ConsecutivoCompania AND RF.LoteDeInventario = LoteInv.CodigoLote");
            vSql.AppendLine("INNER JOIN ArticuloInventario AI ON AI.ConsecutivoCompania = RF.ConsecutivoCompania AND AI.Codigo = RF.Articulo");
            vSql.AppendLine("WHERE F.ConsecutivoCompania = " + insSql.ToSqlValue(valConsecutivoCompania));
            vSql.AppendLine("AND AI.TipoArticuloInv IN (" + insSql.EnumToSqlValue((int)eTipoArticuloInv.Lote) + ", " + insSql.EnumToSqlValue((int)eTipoArticuloInv.LoteFechadeVencimiento) + ")");
            vSql.AppendLine("AND AI.Codigo = " + insSql.ToSqlValue(valCodigoArticulo));
            vSql.AppendLine("AND F.TipoDeDocumento = '0' AND F.StatusFactura = '1'");
            vSql.AppendLine("AND LoteInv.Consecutivo = " + insSql.ToSqlValue(valConseuctivoLote));
            vSql.AppendLine("UNION");
            vSql.AppendLine("SELECT 4 AS ColOrden, F.ConsecutivoCompania, LoteInv.Consecutivo AS ConsecutivoLote, 0 AS Consecutivo, F.Fecha, ");
            vSql.AppendLine(insSql.EnumToSqlValue((int)eOrigenLoteInv.Factura) + " AS Modulo, ");
            vSql.AppendLine("RF.Cantidad AS Cantidad, ");
            vSql.AppendLine("'0' AS TipoOperacion, ");
            vSql.AppendLine("0 AS ConsecutivoDocumentoOrigen, F.Numero AS NumeroDocumentoOrigen, " + insSql.EnumToSqlValue((int)eStatusDocOrigenLoteInv.Anulado) + " AS StatusDocumentoOrigen");
            vSql.AppendLine("FROM factura F INNER JOIN renglonFactura RF ON F.ConsecutivoCompania = RF.ConsecutivoCompania AND F.Numero = RF.NumeroFactura AND F.TipoDeDocumento = RF.TipoDeDocumento");
            vSql.AppendLine("INNER JOIN Saw.LoteDeInventario LoteInv ON RF.ConsecutivoCompania = LoteInv.ConsecutivoCompania AND RF.LoteDeInventario = LoteInv.CodigoLote");
            vSql.AppendLine("INNER JOIN ArticuloInventario AI ON AI.ConsecutivoCompania = RF.ConsecutivoCompania AND AI.Codigo = RF.Articulo");
            vSql.AppendLine("WHERE F.ConsecutivoCompania = " + insSql.ToSqlValue(valConsecutivoCompania));
            vSql.AppendLine("AND AI.TipoArticuloInv IN (" + insSql.EnumToSqlValue((int)eTipoArticuloInv.Lote) + ", " + insSql.EnumToSqlValue((int)eTipoArticuloInv.LoteFechadeVencimiento) + ")");
            vSql.AppendLine("AND AI.Codigo = " + insSql.ToSqlValue(valCodigoArticulo));
            vSql.AppendLine("AND F.TipoDeDocumento = '0' AND F.StatusFactura = '1'");
            vSql.AppendLine("AND LoteInv.Consecutivo = " + insSql.ToSqlValue(valConseuctivoLote));
            return vSql.ToString();
        }

        private string SqlMovimientosNC(int valConsecutivoCompania, string valCodigoArticulo, int valConseuctivoLote) {
            StringBuilder vSql = new StringBuilder();
            QAdvSql insSql = new QAdvSql("");
            //Notas de Crédito Vigentes
            vSql.AppendLine("SELECT 5 AS ColOrden, F.ConsecutivoCompania, LoteInv.Consecutivo AS ConsecutivoLote, 0 AS Consecutivo, F.Fecha, ");
            vSql.AppendLine(insSql.EnumToSqlValue((int)eOrigenLoteInv.NotaDeCredito) + " AS Modulo, ");
            vSql.AppendLine("RF.Cantidad * -1 AS Cantidad, ");
            vSql.AppendLine("'0' AS TipoOperacion, ");
            vSql.AppendLine("0 AS ConsecutivoDocumentoOrigen, F.Numero AS NumeroDocumentoOrigen, " + insSql.EnumToSqlValue((int)eStatusDocOrigenLoteInv.Vigente) + " AS StatusDocumentoOrigen");
            vSql.AppendLine("FROM factura F INNER JOIN renglonFactura RF ON F.ConsecutivoCompania = RF.ConsecutivoCompania AND F.Numero = RF.NumeroFactura AND F.TipoDeDocumento = RF.TipoDeDocumento");
            vSql.AppendLine("INNER JOIN Saw.LoteDeInventario LoteInv ON RF.ConsecutivoCompania = LoteInv.ConsecutivoCompania AND RF.LoteDeInventario = LoteInv.CodigoLote");
            vSql.AppendLine("INNER JOIN ArticuloInventario AI ON AI.ConsecutivoCompania = RF.ConsecutivoCompania AND AI.Codigo = RF.Articulo");
            vSql.AppendLine("WHERE F.ConsecutivoCompania = " + insSql.ToSqlValue(valConsecutivoCompania));
            vSql.AppendLine("AND AI.TipoArticuloInv IN (" + insSql.EnumToSqlValue((int)eTipoArticuloInv.Lote) + ", " + insSql.EnumToSqlValue((int)eTipoArticuloInv.LoteFechadeVencimiento) + ")");
            vSql.AppendLine("AND AI.Codigo = " + insSql.ToSqlValue(valCodigoArticulo));
            vSql.AppendLine("AND F.TipoDeDocumento = '1' AND F.StatusFactura = '0'");
            vSql.AppendLine("AND LoteInv.Consecutivo = " + insSql.ToSqlValue(valConseuctivoLote));
            vSql.AppendLine("UNION");
            //Notas de Crédito Anuladas y su origen vigente
            vSql.AppendLine("SELECT 6 AS ColOrden, F.ConsecutivoCompania, LoteInv.Consecutivo AS ConsecutivoLote, 0 AS Consecutivo, F.Fecha, ");
            vSql.AppendLine(insSql.EnumToSqlValue((int)eOrigenLoteInv.NotaDeCredito) + " AS Modulo, ");
            vSql.AppendLine("RF.Cantidad * -1 AS Cantidad, ");
            vSql.AppendLine("'0' AS TipoOperacion, ");
            vSql.AppendLine("0 AS ConsecutivoDocumentoOrigen, F.Numero AS NumeroDocumentoOrigen, " + insSql.EnumToSqlValue((int)eStatusDocOrigenLoteInv.Vigente) + " AS StatusDocumentoOrigen");
            vSql.AppendLine("FROM factura F INNER JOIN renglonFactura RF ON F.ConsecutivoCompania = RF.ConsecutivoCompania AND F.Numero = RF.NumeroFactura AND F.TipoDeDocumento = RF.TipoDeDocumento");
            vSql.AppendLine("INNER JOIN Saw.LoteDeInventario LoteInv ON RF.ConsecutivoCompania = LoteInv.ConsecutivoCompania AND RF.LoteDeInventario = LoteInv.CodigoLote");
            vSql.AppendLine("INNER JOIN ArticuloInventario AI ON AI.ConsecutivoCompania = RF.ConsecutivoCompania AND AI.Codigo = RF.Articulo");
            vSql.AppendLine("WHERE F.ConsecutivoCompania = " + insSql.ToSqlValue(valConsecutivoCompania));
            vSql.AppendLine("AND AI.TipoArticuloInv IN (" + insSql.EnumToSqlValue((int)eTipoArticuloInv.Lote) + ", " + insSql.EnumToSqlValue((int)eTipoArticuloInv.LoteFechadeVencimiento) + ")");
            vSql.AppendLine("AND AI.Codigo = " + insSql.ToSqlValue(valCodigoArticulo));
            vSql.AppendLine("AND F.TipoDeDocumento = '1' AND F.StatusFactura = '1'");
            vSql.AppendLine("AND LoteInv.Consecutivo = " + insSql.ToSqlValue(valConseuctivoLote));
            vSql.AppendLine("UNION");
            vSql.AppendLine("SELECT 6 AS ColOrden, F.ConsecutivoCompania, LoteInv.Consecutivo AS ConsecutivoLote, 0 AS Consecutivo, F.Fecha, ");
            vSql.AppendLine(insSql.EnumToSqlValue((int)eOrigenLoteInv.NotaDeCredito) + " AS Modulo, ");
            vSql.AppendLine("RF.Cantidad AS Cantidad, ");
            vSql.AppendLine("'1' AS TipoOperacion, ");
            vSql.AppendLine("0 AS ConsecutivoDocumentoOrigen, F.Numero AS NumeroDocumentoOrigen, " + insSql.EnumToSqlValue((int)eStatusDocOrigenLoteInv.Anulado) + " AS StatusDocumentoOrigen");
            vSql.AppendLine("FROM factura F INNER JOIN renglonFactura RF ON F.ConsecutivoCompania = RF.ConsecutivoCompania AND F.Numero = RF.NumeroFactura AND F.TipoDeDocumento = RF.TipoDeDocumento");
            vSql.AppendLine("INNER JOIN Saw.LoteDeInventario LoteInv ON RF.ConsecutivoCompania = LoteInv.ConsecutivoCompania AND RF.LoteDeInventario = LoteInv.CodigoLote");
            vSql.AppendLine("INNER JOIN ArticuloInventario AI ON AI.ConsecutivoCompania = RF.ConsecutivoCompania AND AI.Codigo = RF.Articulo");
            vSql.AppendLine("WHERE F.ConsecutivoCompania = " + insSql.ToSqlValue(valConsecutivoCompania));
            vSql.AppendLine("AND AI.TipoArticuloInv IN (" + insSql.EnumToSqlValue((int)eTipoArticuloInv.Lote) + ", " + insSql.EnumToSqlValue((int)eTipoArticuloInv.LoteFechadeVencimiento) + ")");
            vSql.AppendLine("AND AI.Codigo = " + insSql.ToSqlValue(valCodigoArticulo));
            vSql.AppendLine("AND F.TipoDeDocumento = '1' AND F.StatusFactura = '1' ");
            vSql.AppendLine("AND LoteInv.Consecutivo = " + insSql.ToSqlValue(valConseuctivoLote));
            return vSql.ToString();
        }

        private string SqlMovimientosND(int valConsecutivoCompania, string valCodigoArticulo, int valConseuctivoLote) {
            StringBuilder vSql = new StringBuilder();
            QAdvSql insSql = new QAdvSql("");
            //Notas de Débito Vigentes
            vSql.AppendLine("SELECT 7 AS ColOrden, F.ConsecutivoCompania, LoteInv.Consecutivo AS ConsecutivoLote, 0 AS Consecutivo, F.Fecha, ");
            vSql.AppendLine(insSql.EnumToSqlValue((int)eOrigenLoteInv.NotaDeDebito) + " AS Modulo, ");
            vSql.AppendLine("RF.Cantidad * -1 AS Cantidad, ");
            vSql.AppendLine("'1' AS TipoOperacion, ");
            vSql.AppendLine("0 AS ConsecutivoDocumentoOrigen, F.Numero AS NumeroDocumentoOrigen, " + insSql.EnumToSqlValue((int)eStatusDocOrigenLoteInv.Vigente) + " AS StatusDocumentoOrigen");
            vSql.AppendLine("FROM factura F INNER JOIN renglonFactura RF ON F.ConsecutivoCompania = RF.ConsecutivoCompania AND F.Numero = RF.NumeroFactura AND F.TipoDeDocumento = RF.TipoDeDocumento ");
            vSql.AppendLine("INNER JOIN Saw.LoteDeInventario LoteInv ON RF.ConsecutivoCompania = LoteInv.ConsecutivoCompania AND RF.LoteDeInventario = LoteInv.CodigoLote ");
            vSql.AppendLine("INNER JOIN ArticuloInventario AI ON AI.ConsecutivoCompania = RF.ConsecutivoCompania AND AI.Codigo = RF.Articulo");
            vSql.AppendLine("WHERE F.ConsecutivoCompania = " + insSql.ToSqlValue(valConsecutivoCompania));
            vSql.AppendLine("AND AI.TipoArticuloInv IN (" + insSql.EnumToSqlValue((int)eTipoArticuloInv.Lote) + ", " + insSql.EnumToSqlValue((int)eTipoArticuloInv.LoteFechadeVencimiento) + ")");
            vSql.AppendLine("AND AI.Codigo = " + insSql.ToSqlValue(valCodigoArticulo));
            vSql.AppendLine("AND F.TipoDeDocumento = '2' AND F.StatusFactura = '0'");
            vSql.AppendLine("AND LoteInv.Consecutivo = " + insSql.ToSqlValue(valConseuctivoLote));
            vSql.AppendLine("UNION");
            //Notas de Débitos Anuladas y su origen vigente
            vSql.AppendLine("SELECT 8 AS ColOrden, F.ConsecutivoCompania, LoteInv.Consecutivo AS ConsecutivoLote, 0 AS Consecutivo, F.Fecha, ");
            vSql.AppendLine(insSql.EnumToSqlValue((int)eOrigenLoteInv.NotaDeDebito) + " AS Modulo, ");
            vSql.AppendLine("RF.Cantidad * -1 AS Cantidad, ");
            vSql.AppendLine("'1' AS TipoOperacion, ");
            vSql.AppendLine("0 AS ConsecutivoDocumentoOrigen, F.Numero AS NumeroDocumentoOrigen, " + insSql.EnumToSqlValue((int)eStatusDocOrigenLoteInv.Vigente) + " AS StatusDocumentoOrigen");
            vSql.AppendLine("FROM factura F INNER JOIN renglonFactura RF ON F.ConsecutivoCompania = RF.ConsecutivoCompania AND F.Numero = RF.NumeroFactura AND F.TipoDeDocumento = RF.TipoDeDocumento ");
            vSql.AppendLine("INNER JOIN Saw.LoteDeInventario LoteInv ON RF.ConsecutivoCompania = LoteInv.ConsecutivoCompania AND RF.LoteDeInventario = LoteInv.CodigoLote ");
            vSql.AppendLine("INNER JOIN ArticuloInventario AI ON AI.ConsecutivoCompania = RF.ConsecutivoCompania AND AI.Codigo = RF.Articulo");
            vSql.AppendLine("WHERE F.ConsecutivoCompania = " + insSql.ToSqlValue(valConsecutivoCompania));
            vSql.AppendLine("AND AI.TipoArticuloInv IN (" + insSql.EnumToSqlValue((int)eTipoArticuloInv.Lote) + ", " + insSql.EnumToSqlValue((int)eTipoArticuloInv.LoteFechadeVencimiento) + ")");
            vSql.AppendLine("AND AI.Codigo = " + insSql.ToSqlValue(valCodigoArticulo));
            vSql.AppendLine("AND F.TipoDeDocumento = '2' AND F.StatusFactura = '1'");
            vSql.AppendLine("AND LoteInv.Consecutivo = " + insSql.ToSqlValue(valConseuctivoLote));
            vSql.AppendLine("UNION");
            vSql.AppendLine("SELECT 8 AS ColOrden, F.ConsecutivoCompania, LoteInv.Consecutivo AS ConsecutivoLote, 0 AS Consecutivo, F.Fecha, ");
            vSql.AppendLine(insSql.EnumToSqlValue((int)eOrigenLoteInv.NotaDeDebito) + " AS Modulo, ");
            vSql.AppendLine("RF.Cantidad AS Cantidad, ");
            vSql.AppendLine("'0' AS TipoOperacion, ");
            vSql.AppendLine("0 AS ConsecutivoDocumentoOrigen, F.Numero AS NumeroDocumentoOrigen, " + insSql.EnumToSqlValue((int)eStatusDocOrigenLoteInv.Anulado) + " AS StatusDocumentoOrigen");
            vSql.AppendLine("FROM factura F INNER JOIN renglonFactura RF ON F.ConsecutivoCompania = RF.ConsecutivoCompania AND F.Numero = RF.NumeroFactura AND F.TipoDeDocumento = RF.TipoDeDocumento ");
            vSql.AppendLine("INNER JOIN Saw.LoteDeInventario LoteInv ON RF.ConsecutivoCompania = LoteInv.ConsecutivoCompania AND RF.LoteDeInventario = LoteInv.CodigoLote ");
            vSql.AppendLine("INNER JOIN ArticuloInventario AI ON AI.ConsecutivoCompania = RF.ConsecutivoCompania AND AI.Codigo = RF.Articulo");
            vSql.AppendLine("WHERE F.ConsecutivoCompania = " + insSql.ToSqlValue(valConsecutivoCompania));
            vSql.AppendLine("AND AI.TipoArticuloInv IN (" + insSql.EnumToSqlValue((int)eTipoArticuloInv.Lote) + ", " + insSql.EnumToSqlValue((int)eTipoArticuloInv.LoteFechadeVencimiento) + ")");
            vSql.AppendLine("AND AI.Codigo = " + insSql.ToSqlValue(valCodigoArticulo));
            vSql.AppendLine("AND F.TipoDeDocumento = '2' AND F.StatusFactura = '1'");
            vSql.AppendLine("AND LoteInv.Consecutivo = " + insSql.ToSqlValue(valConseuctivoLote));
            return vSql.ToString();
        }

        private string SqlMovimientosNE(int valConsecutivoCompania, string valCodigoArticulo, int valConseuctivoLote) {
            StringBuilder vSql = new StringBuilder();
            QAdvSql insSql = new QAdvSql("");
            //Notas de Entrega Vigentes
            vSql.AppendLine("SELECT 9 AS ColOrden, F.ConsecutivoCompania, LoteInv.Consecutivo AS ConsecutivoLote, 0 AS Consecutivo, F.Fecha, ");
            vSql.AppendLine(insSql.EnumToSqlValue((int)eOrigenLoteInv.NotaDeEntrega) + " AS Modulo, ");
            vSql.AppendLine("RF.Cantidad * -1 AS Cantidad, ");
            vSql.AppendLine("'1' AS TipoOperacion, ");
            vSql.AppendLine("0 AS ConsecutivoDocumentoOrigen, F.Numero AS NumeroDocumentoOrigen, " + insSql.EnumToSqlValue((int)eStatusDocOrigenLoteInv.Vigente) + " AS StatusDocumentoOrigen");
            vSql.AppendLine("FROM factura F INNER JOIN renglonFactura RF ON F.ConsecutivoCompania = RF.ConsecutivoCompania AND F.Numero = RF.NumeroFactura AND F.TipoDeDocumento = RF.TipoDeDocumento ");
            vSql.AppendLine("INNER JOIN Saw.LoteDeInventario LoteInv ON RF.ConsecutivoCompania = LoteInv.ConsecutivoCompania AND RF.LoteDeInventario = LoteInv.CodigoLote ");
            vSql.AppendLine("INNER JOIN ArticuloInventario AI ON AI.ConsecutivoCompania = RF.ConsecutivoCompania AND AI.Codigo = RF.Articulo");
            vSql.AppendLine("WHERE F.ConsecutivoCompania = " + insSql.ToSqlValue(valConsecutivoCompania));
            vSql.AppendLine("AND AI.TipoArticuloInv IN (" + insSql.EnumToSqlValue((int)eTipoArticuloInv.Lote) + ", " + insSql.EnumToSqlValue((int)eTipoArticuloInv.LoteFechadeVencimiento) + ")");
            vSql.AppendLine("AND AI.Codigo = " + insSql.ToSqlValue(valCodigoArticulo));
            vSql.AppendLine("AND F.TipoDeDocumento = '8' AND F.StatusFactura = '0'");
            vSql.AppendLine("AND LoteInv.Consecutivo = " + insSql.ToSqlValue(valConseuctivoLote));
            vSql.AppendLine("UNION");
            //Notas de Entrega Anuladas y su origen vigente
            vSql.AppendLine("SELECT 10 AS ColOrden, F.ConsecutivoCompania, LoteInv.Consecutivo AS ConsecutivoLote, 0 AS Consecutivo, F.Fecha, ");
            vSql.AppendLine(insSql.EnumToSqlValue((int)eOrigenLoteInv.NotaDeEntrega) + " AS Modulo, ");
            vSql.AppendLine("RF.Cantidad * -1 AS Cantidad, ");
            vSql.AppendLine("'1' AS TipoOperacion, ");
            vSql.AppendLine("0 AS ConsecutivoDocumentoOrigen, F.Numero AS NumeroDocumentoOrigen, " + insSql.EnumToSqlValue((int)eStatusDocOrigenLoteInv.Vigente) + " AS StatusDocumentoOrigen");
            vSql.AppendLine("FROM factura F INNER JOIN renglonFactura RF ON F.ConsecutivoCompania = RF.ConsecutivoCompania AND F.Numero = RF.NumeroFactura AND F.TipoDeDocumento = RF.TipoDeDocumento ");
            vSql.AppendLine("INNER JOIN Saw.LoteDeInventario LoteInv ON RF.ConsecutivoCompania = LoteInv.ConsecutivoCompania AND RF.LoteDeInventario = LoteInv.CodigoLote ");
            vSql.AppendLine("INNER JOIN ArticuloInventario AI ON AI.ConsecutivoCompania = RF.ConsecutivoCompania AND AI.Codigo = RF.Articulo");
            vSql.AppendLine("WHERE F.ConsecutivoCompania = " + insSql.ToSqlValue(valConsecutivoCompania));
            vSql.AppendLine("AND AI.TipoArticuloInv IN (" + insSql.EnumToSqlValue((int)eTipoArticuloInv.Lote) + ", " + insSql.EnumToSqlValue((int)eTipoArticuloInv.LoteFechadeVencimiento) + ")");
            vSql.AppendLine("AND AI.Codigo = " + insSql.ToSqlValue(valCodigoArticulo));
            vSql.AppendLine("AND F.TipoDeDocumento = '8' AND F.StatusFactura = '1'");
            vSql.AppendLine("AND LoteInv.Consecutivo = " + insSql.ToSqlValue(valConseuctivoLote));
            vSql.AppendLine("UNION");
            vSql.AppendLine("SELECT 10 AS ColOrden, F.ConsecutivoCompania, LoteInv.Consecutivo AS ConsecutivoLote, 0 AS Consecutivo, F.Fecha, ");
            vSql.AppendLine(insSql.EnumToSqlValue((int)eOrigenLoteInv.NotaDeEntrega) + " AS Modulo, ");
            vSql.AppendLine("RF.Cantidad AS Cantidad, ");
            vSql.AppendLine("'0' AS TipoOperacion, ");
            vSql.AppendLine("0 AS ConsecutivoDocumentoOrigen, F.Numero AS NumeroDocumentoOrigen, " + insSql.EnumToSqlValue((int)eStatusDocOrigenLoteInv.Anulado) + " AS StatusDocumentoOrigen");
            vSql.AppendLine("FROM factura F INNER JOIN renglonFactura RF ON F.ConsecutivoCompania = RF.ConsecutivoCompania AND F.Numero = RF.NumeroFactura AND F.TipoDeDocumento = RF.TipoDeDocumento ");
            vSql.AppendLine("INNER JOIN Saw.LoteDeInventario LoteInv ON RF.ConsecutivoCompania = LoteInv.ConsecutivoCompania AND RF.LoteDeInventario = LoteInv.CodigoLote ");
            vSql.AppendLine("INNER JOIN ArticuloInventario AI ON AI.ConsecutivoCompania = RF.ConsecutivoCompania AND AI.Codigo = RF.Articulo");
            vSql.AppendLine("WHERE F.ConsecutivoCompania = " + insSql.ToSqlValue(valConsecutivoCompania));
            vSql.AppendLine("AND AI.TipoArticuloInv IN (" + insSql.EnumToSqlValue((int)eTipoArticuloInv.Lote) + ", " + insSql.EnumToSqlValue((int)eTipoArticuloInv.LoteFechadeVencimiento) + ")");
            vSql.AppendLine("AND AI.Codigo = " + insSql.ToSqlValue(valCodigoArticulo));
            vSql.AppendLine("AND F.TipoDeDocumento = '8' AND F.StatusFactura = '1' ");
            vSql.AppendLine("AND LoteInv.Consecutivo = " + insSql.ToSqlValue(valConseuctivoLote));
            return vSql.ToString();
        }

        private string SqlMovimientosCompra(int valConsecutivoCompania, string valCodigoArticulo, int valConseuctivoLote) {
            StringBuilder vSql = new StringBuilder();
            QAdvSql insSql = new QAdvSql("");
            //Compras Vigentes
            vSql.AppendLine("SELECT 11 as ColOrden, C.ConsecutivoCompania, LoteInv.Consecutivo AS ConsecutivoLote, 0 AS Consecutivo, C.Fecha, ");
            vSql.AppendLine(insSql.EnumToSqlValue((int)eOrigenLoteInv.Compra) + " AS Modulo, ");
            vSql.AppendLine("CD.CantidadRecibida AS Cantidad, ");
            vSql.AppendLine("'0' AS TipoOperacion, ");
            vSql.AppendLine("C.Consecutivo AS ConsecutivoDocumentoOrigen, C.Numero AS NumeroDocumentoOrigen, " + insSql.EnumToSqlValue((int)eStatusDocOrigenLoteInv.Vigente) + " AS StatusDocumentoOrigen");
            vSql.AppendLine("FROM Adm.Compra C INNER JOIN Adm.CompraDetalleArticuloInventario CD ON C.ConsecutivoCompania = CD.ConsecutivoCompania AND C.Consecutivo = CD.ConsecutivoCompra");
            vSql.AppendLine("INNER JOIN Saw.LoteDeInventario LoteInv ON CD.ConsecutivoCompania = LoteInv.ConsecutivoCompania AND CD.ConsecutivoLoteDeInventario = LoteInv.Consecutivo");
            vSql.AppendLine("INNER JOIN ArticuloInventario AI ON AI.ConsecutivoCompania = CD.ConsecutivoCompania AND AI.Codigo = CD.CodigoArticulo");
            vSql.AppendLine("WHERE C.ConsecutivoCompania = " + insSql.ToSqlValue(valConsecutivoCompania));
            vSql.AppendLine("AND AI.TipoArticuloInv IN (" + insSql.EnumToSqlValue((int)eTipoArticuloInv.Lote) + ", " + insSql.EnumToSqlValue((int)eTipoArticuloInv.LoteFechadeVencimiento) + ")");
            vSql.AppendLine("AND AI.Codigo = " + insSql.ToSqlValue(valCodigoArticulo));
            vSql.AppendLine("AND C.StatusCompra = '0'");
            vSql.AppendLine("AND LoteInv.Consecutivo = " + insSql.ToSqlValue(valConseuctivoLote));
            vSql.AppendLine("UNION");
            //Compras Anuladas y su origen vigente
            vSql.AppendLine("SELECT 12 as ColOrden, C.ConsecutivoCompania, LoteInv.Consecutivo AS ConsecutivoLote, 0 AS Consecutivo, C.Fecha, ");
            vSql.AppendLine(insSql.EnumToSqlValue((int)eOrigenLoteInv.Compra) + " AS Modulo, ");
            vSql.AppendLine("CD.CantidadRecibida AS Cantidad, ");
            vSql.AppendLine("'0' AS TipoOperacion, ");
            vSql.AppendLine("C.Consecutivo AS ConsecutivoDocumentoOrigen, C.Numero AS NumeroDocumentoOrigen, " + insSql.EnumToSqlValue((int)eStatusDocOrigenLoteInv.Vigente) + " AS StatusDocumentoOrigen");
            vSql.AppendLine("FROM Adm.Compra C INNER JOIN Adm.CompraDetalleArticuloInventario CD ON C.ConsecutivoCompania = CD.ConsecutivoCompania AND C.Consecutivo = CD.ConsecutivoCompra");
            vSql.AppendLine("INNER JOIN Saw.LoteDeInventario LoteInv ON CD.ConsecutivoCompania = LoteInv.ConsecutivoCompania AND CD.ConsecutivoLoteDeInventario = LoteInv.Consecutivo");
            vSql.AppendLine("INNER JOIN ArticuloInventario AI ON AI.ConsecutivoCompania = CD.ConsecutivoCompania AND AI.Codigo = CD.CodigoArticulo");
            vSql.AppendLine("WHERE C.ConsecutivoCompania = " + insSql.ToSqlValue(valConsecutivoCompania));
            vSql.AppendLine("AND AI.TipoArticuloInv IN (" + insSql.EnumToSqlValue((int)eTipoArticuloInv.Lote) + ", " + insSql.EnumToSqlValue((int)eTipoArticuloInv.LoteFechadeVencimiento) + ")");
            vSql.AppendLine("AND AI.Codigo = " + insSql.ToSqlValue(valCodigoArticulo));
            vSql.AppendLine("AND C.StatusCompra = '1'");
            vSql.AppendLine("AND LoteInv.Consecutivo = " + insSql.ToSqlValue(valConseuctivoLote));
            vSql.AppendLine("UNION");
            vSql.AppendLine("SELECT 12 as ColOrden, C.ConsecutivoCompania, LoteInv.Consecutivo AS ConsecutivoLote, 0 AS Consecutivo, C.Fecha, ");
            vSql.AppendLine(insSql.EnumToSqlValue((int)eOrigenLoteInv.Compra) + " AS Modulo, ");
            vSql.AppendLine("CD.CantidadRecibida * -1 AS Cantidad, ");
            vSql.AppendLine("'1' AS TipoOperacion, ");
            vSql.AppendLine("C.Consecutivo AS ConsecutivoDocumentoOrigen, C.Numero AS NumeroDocumentoOrigen, " + insSql.EnumToSqlValue((int)eStatusDocOrigenLoteInv.Anulado) + " AS StatusDocumentoOrigen");
            vSql.AppendLine("FROM Adm.Compra C INNER JOIN Adm.CompraDetalleArticuloInventario CD ON C.ConsecutivoCompania = CD.ConsecutivoCompania AND C.Consecutivo = CD.ConsecutivoCompra");
            vSql.AppendLine("INNER JOIN Saw.LoteDeInventario LoteInv ON CD.ConsecutivoCompania = LoteInv.ConsecutivoCompania AND CD.ConsecutivoLoteDeInventario = LoteInv.Consecutivo");
            vSql.AppendLine("INNER JOIN ArticuloInventario AI ON AI.ConsecutivoCompania = CD.ConsecutivoCompania AND AI.Codigo = CD.CodigoArticulo");
            vSql.AppendLine("WHERE C.ConsecutivoCompania = " + insSql.ToSqlValue(valConsecutivoCompania));
            vSql.AppendLine("AND AI.TipoArticuloInv IN (" + insSql.EnumToSqlValue((int)eTipoArticuloInv.Lote) + ", " + insSql.EnumToSqlValue((int)eTipoArticuloInv.LoteFechadeVencimiento) + ")");
            vSql.AppendLine("AND AI.Codigo = " + insSql.ToSqlValue(valCodigoArticulo));
            vSql.AppendLine("AND C.StatusCompra = '1'");
            vSql.AppendLine("AND LoteInv.Consecutivo = " + insSql.ToSqlValue(valConseuctivoLote));
            return vSql.ToString();
        }

        private string SqlMovimientosConteoFisico(int valConsecutivoCompania, string valCodigoArticulo, int valConseuctivoLote) {
            StringBuilder vSql = new StringBuilder();
            QAdvSql insSql = new QAdvSql("");
            vSql.AppendLine("SELECT 13 as ColOrden, CF.ConsecutivoCompania, LoteInv.Consecutivo AS ConsecutivoLote, 0 AS Consecutivo, CF.Fecha, ");
            vSql.AppendLine(insSql.EnumToSqlValue((int)eOrigenLoteInv.ConteoFisico) + " AS Modulo, ");
            vSql.AppendLine("RCF.Diferencia AS Cantidad, ");
            vSql.AppendLine("(CASE WHEN RCF.Diferencia > 0 THEN '0' ELSE '1' END) AS TipoOperacion, ");
            vSql.AppendLine("CF.ConsecutivoConteo AS ConsecutivoDocumentoOrigen, CAST(CF.ConsecutivoConteo AS varchar) AS NumeroDocumentoOrigen, " + insSql.EnumToSqlValue((int)eStatusDocOrigenLoteInv.Vigente) + " AS Status");
            vSql.AppendLine("FROM ConteoFisico CF INNER JOIN RenglonConteoFisico RCF ON CF.ConsecutivoCompania = RCF.ConsecutivoCompania AND CF.ConsecutivoConteo = RCF.ConsecutivoConteo");
            vSql.AppendLine("INNER JOIN Saw.LoteDeInventario LoteInv ON RCF.ConsecutivoCompania = LoteInv.ConsecutivoCompania AND RCF.CodigoLote = LoteInv.CodigoLote");
            vSql.AppendLine("INNER JOIN ArticuloInventario AI ON AI.ConsecutivoCompania = RCF.ConsecutivoCompania AND AI.Codigo = RCF.CodigoArticulo");
            vSql.AppendLine("WHERE CF.ConsecutivoCompania = " + insSql.ToSqlValue(valConsecutivoCompania));
            vSql.AppendLine("AND AI.TipoArticuloInv IN (" + insSql.EnumToSqlValue((int)eTipoArticuloInv.Lote) + ", " + insSql.EnumToSqlValue((int)eTipoArticuloInv.LoteFechadeVencimiento) + ")");
            vSql.AppendLine("AND AI.Codigo = " + insSql.ToSqlValue(valCodigoArticulo));
            vSql.AppendLine("AND CF.Status = '0'");
            vSql.AppendLine("AND LoteInv.Consecutivo = " + insSql.ToSqlValue(valConseuctivoLote));
            vSql.AppendLine("UNION");
            vSql.AppendLine("SELECT 14 as ColOrden, CF.ConsecutivoCompania, LoteInv.Consecutivo AS ConsecutivoLote, 0 AS Consecutivo, CF.Fecha, ");
            vSql.AppendLine(insSql.EnumToSqlValue((int)eOrigenLoteInv.ConteoFisico) + " AS Modulo, ");
            vSql.AppendLine("RCF.Diferencia AS Cantidad, ");
            vSql.AppendLine("(CASE WHEN RCF.Diferencia > 0 THEN '0' ELSE '1' END) AS TipoOperacion, ");
            vSql.AppendLine("CF.ConsecutivoConteo AS ConsecutivoDocumentoOrigen, CAST(CF.ConsecutivoConteo AS varchar) AS NumeroDocumentoOrigen, " + insSql.EnumToSqlValue((int)eStatusDocOrigenLoteInv.Vigente) + " AS Status");
            vSql.AppendLine("FROM ConteoFisico CF INNER JOIN RenglonConteoFisico RCF ON CF.ConsecutivoCompania = RCF.ConsecutivoCompania AND CF.ConsecutivoConteo = RCF.ConsecutivoConteo");
            vSql.AppendLine("INNER JOIN Saw.LoteDeInventario LoteInv ON RCF.ConsecutivoCompania = LoteInv.ConsecutivoCompania AND RCF.CodigoLote = LoteInv.CodigoLote");
            vSql.AppendLine("INNER JOIN ArticuloInventario AI ON AI.ConsecutivoCompania = RCF.ConsecutivoCompania AND AI.Codigo = RCF.CodigoArticulo");
            vSql.AppendLine("WHERE CF.ConsecutivoCompania = " + insSql.ToSqlValue(valConsecutivoCompania));
            vSql.AppendLine("AND AI.TipoArticuloInv IN (" + insSql.EnumToSqlValue((int)eTipoArticuloInv.Lote) + ", " + insSql.EnumToSqlValue((int)eTipoArticuloInv.LoteFechadeVencimiento) + ")");
            vSql.AppendLine("AND AI.Codigo = " + insSql.ToSqlValue(valCodigoArticulo));
            vSql.AppendLine("AND CF.Status = '2'");
            vSql.AppendLine("AND LoteInv.Consecutivo = " + insSql.ToSqlValue(valConseuctivoLote));
            vSql.AppendLine("UNION");
            vSql.AppendLine("SELECT 14 as ColOrden, CF.ConsecutivoCompania, LoteInv.Consecutivo AS ConsecutivoLote, 0 AS Consecutivo, CF.Fecha, ");
            vSql.AppendLine(insSql.EnumToSqlValue((int)eOrigenLoteInv.ConteoFisico) + " AS Modulo, ");
            vSql.AppendLine("RCF.Diferencia AS Cantidad, ");
            vSql.AppendLine("(CASE WHEN RCF.Diferencia > 0 THEN '1' ELSE '0' END) AS TipoOperacion, ");
            vSql.AppendLine("CF.ConsecutivoConteo AS ConsecutivoDocumentoOrigen, CAST(CF.ConsecutivoConteo AS varchar) AS NumeroDocumentoOrigen, " + insSql.EnumToSqlValue((int)eStatusDocOrigenLoteInv.Anulado) + " AS Status");
            vSql.AppendLine("FROM ConteoFisico CF INNER JOIN RenglonConteoFisico RCF ON CF.ConsecutivoCompania = RCF.ConsecutivoCompania AND CF.ConsecutivoConteo = RCF.ConsecutivoConteo");
            vSql.AppendLine("INNER JOIN Saw.LoteDeInventario LoteInv ON RCF.ConsecutivoCompania = LoteInv.ConsecutivoCompania AND RCF.CodigoLote = LoteInv.CodigoLote");
            vSql.AppendLine("INNER JOIN ArticuloInventario AI ON AI.ConsecutivoCompania = RCF.ConsecutivoCompania AND AI.Codigo = RCF.CodigoArticulo");
            vSql.AppendLine("WHERE CF.ConsecutivoCompania = " + insSql.ToSqlValue(valConsecutivoCompania));
            vSql.AppendLine("AND AI.TipoArticuloInv IN (" + insSql.EnumToSqlValue((int)eTipoArticuloInv.Lote) + ", " + insSql.EnumToSqlValue((int)eTipoArticuloInv.LoteFechadeVencimiento) + ")");
            vSql.AppendLine("AND AI.Codigo = " + insSql.ToSqlValue(valCodigoArticulo));
            vSql.AppendLine("AND CF.Status = '2' ");
            vSql.AppendLine("AND LoteInv.Consecutivo = " + insSql.ToSqlValue(valConseuctivoLote));
            return vSql.ToString();
        }

        bool EsTipoDeInventarioLote(int valConsecutivoCompania, string valCodigoArticulo) {
            bool vResult = false;
            LibDatabase insDb = new LibDatabase();
            StringBuilder SQL = new StringBuilder();

            SQL.AppendLine(" SELECT TipoArticuloInv FROM ArticuloInventario ");
            SQL.AppendLine(" WHERE Codigo = " + insDb.InsSql.ToSqlValue(valCodigoArticulo));
            SQL.AppendLine(" AND ConsecutivoCompania = " + insDb.InsSql.ToSqlValue(valConsecutivoCompania));
            SQL.AppendLine(" AND TipoArticuloInv IN (" + insDb.InsSql.EnumToSqlValue((int)eTipoArticuloInv.LoteFechadeVencimiento) + "," + insDb.InsSql.EnumToSqlValue((int)eTipoArticuloInv.Lote) + ")");

            vResult = insDb.RecordCountOfSql(SQL.ToString()) > 0;
            return vResult;
        }

        private static void ActualizaExistenciaLote(int valConsecutivoCompania, int valConsecutivoLote) {
            LibDatabase insDb = new LibDatabase();
            StringBuilder vSqlActualizaExistencia = new StringBuilder();
            StringBuilder vSqlSelectSum = new StringBuilder();
            vSqlSelectSum.AppendLine("SELECT SUM(Cantidad) FROM Saw.LoteDeInventarioMovimiento");
            vSqlSelectSum.AppendLine("WHERE Saw.LoteDeInventarioMovimiento.ConsecutivoCompania = Saw.LoteDeInventario.ConsecutivoCompania");
            vSqlSelectSum.AppendLine("AND Saw.LoteDeInventarioMovimiento.ConsecutivoLote = Saw.LoteDeInventario.Consecutivo");

            vSqlActualizaExistencia.AppendLine("UPDATE Saw.LoteDeInventario");
            vSqlActualizaExistencia.AppendLine("SET Existencia = ISNULL((" + vSqlSelectSum + " ), 0)");
            vSqlActualizaExistencia.AppendLine("WHERE Saw.LoteDeInventario.ConsecutivoCompania = " + insDb.InsSql.ToSqlValue(valConsecutivoCompania));
            vSqlActualizaExistencia.AppendLine("AND Saw.LoteDeInventario.Consecutivo = " + insDb.InsSql.ToSqlValue(valConsecutivoLote));
            insDb.Execute(vSqlActualizaExistencia.ToString());
        }

        private static void ActualizaExistenciaArticulo(int valConsecutivoCompania, string valCodigoArticulo) {
            LibDatabase insDb = new LibDatabase();
            StringBuilder vSqlActualizaExistenciaArticulo = new StringBuilder();
            StringBuilder vSqlSelectSum = new StringBuilder();
            vSqlSelectSum.AppendLine("SELECT SUM(Existencia) FROM Saw.LoteDeInventario ");
            vSqlSelectSum.AppendLine("WHERE Saw.LoteDeInventario.ConsecutivoCompania = ArticuloInventario.ConsecutivoCompania");
            vSqlSelectSum.AppendLine("AND Saw.LoteDeInventario.CodigoArticulo = ArticuloInventario.Codigo ");

            vSqlActualizaExistenciaArticulo.AppendLine("UPDATE ArticuloInventario ");
            vSqlActualizaExistenciaArticulo.AppendLine("SET Existencia = ISNULL(( " + vSqlSelectSum.ToString() + " ), 0)");
            vSqlActualizaExistenciaArticulo.AppendLine("WHERE ArticuloInventario.ConsecutivoCompania = " + insDb.InsSql.ToSqlValue(valConsecutivoCompania));
            vSqlActualizaExistenciaArticulo.AppendLine("AND ArticuloInventario.Codigo = " + insDb.InsSql.ToSqlValue(valCodigoArticulo));
            insDb.Execute(vSqlActualizaExistenciaArticulo.ToString());
        }

    } //End of class clsLoteDeInventarioNav

} //End of namespace Galac.Saw.Brl.Inventario
