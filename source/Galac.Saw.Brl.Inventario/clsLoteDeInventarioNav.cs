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
            XElement vXElement = new XElement("GpData",
                new XElement("GpResult",
                    new XElement("Codigo", valMaster.CodigoArticulo)));
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

        } //End of class clsLoteDeInventarioNav

} //End of namespace Galac.Saw.Brl.Inventario
