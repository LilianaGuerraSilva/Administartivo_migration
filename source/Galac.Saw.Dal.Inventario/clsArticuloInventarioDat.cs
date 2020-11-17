using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Security;
using System.Security.Permissions;
using System.Xml;
using System.Xml.Linq;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Dal;
using LibGalac.Aos.Catching;
using LibGalac.Aos.Dal;
using LibGalac.Aos.DefGen;
using Galac.Saw.Ccl.Inventario;

namespace Galac.Saw.Dal.Inventario {
    public class clsArticuloInventarioDat:LibData, ILibDataMasterComponentWithSearch<IList<ArticuloInventario>,IList<ArticuloInventario>>, ILibDataRpt {
        #region Variables
        LibTrn insTrn;
        ArticuloInventario _CurrentRecord;
        #endregion //Variables
        #region Propiedades
        private ArticuloInventario CurrentRecord {
            get { return _CurrentRecord; }
            set { _CurrentRecord = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public clsArticuloInventarioDat() {
            DbSchema = "Saw";
            insTrn = new LibTrn();
        }
        #endregion //Constructores
        #region Metodos Generados

        private StringBuilder ParametrosActualizacion(ArticuloInventario valRecord,eAccionSR valAction) {
            StringBuilder vResult = new StringBuilder();
            //LibGpParams vParams = new LibGpParams();
            //vParams.AddReturn();
            //vParams.AddInDateFormat("DateFormat");
            //vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            //vParams.AddInString("Codigo", valRecord.Codigo, 15);
            //vParams.AddInString("Descripcion", valRecord.Descripcion, 255);
            //vParams.AddInString("LineaDeProducto", valRecord.LineaDeProducto, 20);
            //vParams.AddInEnum("StatusdelArticulo", valRecord.StatusdelArticuloAsDB);
            //vParams.AddInEnum("TipoDeArticulo", valRecord.TipoDeArticuloAsDB);
            //vParams.AddInEnum("AlicuotaIVA", valRecord.AlicuotaIVAAsDB);
            //vParams.AddInDecimal("PrecioSinIVA", valRecord.PrecioSinIVA, 2);
            //vParams.AddInDecimal("PrecioConIVA", valRecord.PrecioConIVA, 2);
            //vParams.AddInDecimal("Existencia", valRecord.Existencia, 2);
            //vParams.AddInString("Categoria", valRecord.Categoria, 20);
            //vParams.AddInString("Marca", valRecord.Marca, 30);
            //vParams.AddInDateTime("FechaDeVencimiento", valRecord.FechaDeVencimiento);
            //vParams.AddInString("UnidadDeVenta", valRecord.UnidadDeVenta, 20);
            //vParams.AddInEnum("TipoArticuloInv", valRecord.TipoArticuloInvAsDB);
            //vParams.AddInString("NombreOperador", ((CustomIdentity) Thread.CurrentPrincipal.Identity).Login, 10);
            //vParams.AddInDateTime("FechaUltimaModificacion", LibDate.Today());
            //if (valAction == eAccionSR.Modificar) {
            //    vParams.AddInTimestamp("TimeStampAsInt", valRecord.fldTimeStamp);
            //}
            //vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosClave(ArticuloInventario valRecord,bool valIncludeTimestamp,bool valAddReturnParameter) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            if(valAddReturnParameter) {
                vParams.AddReturn();
            }
            vParams.AddInInteger("ConsecutivoCompania",valRecord.ConsecutivoCompania);
            vParams.AddInString("Codigo",valRecord.Codigo,15);
            if(valIncludeTimestamp) {
                vParams.AddInTimestamp("TimeStampAsInt",valRecord.fldTimeStamp);
            }
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosProximoConsecutivo(ArticuloInventario valRecord) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania",valRecord.ConsecutivoCompania);
            vResult = vParams.Get();
            return vResult;
        }
        #region Miembros de ILibDataMasterComponent<IList<ArticuloInventario>, IList<ArticuloInventario>>

        LibResponse ILibDataMasterComponent<IList<ArticuloInventario>,IList<ArticuloInventario>>.CanBeChoosen(IList<ArticuloInventario> refRecord,eAccionSR valAction) {
            LibResponse vResult = new LibResponse();
            ArticuloInventario vRecord = refRecord[0];
            StringBuilder vSbInfo = new StringBuilder();
            string vErrMsg = "";
            LibDatabase insDB = new LibDatabase();
            if(valAction == eAccionSR.Eliminar) {
                //if (insDB.ExistsValueOnMultifile("dbo.RenglonCotizacion", "CodigoArticulo", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Codigo), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                //    vSbInfo.AppendLine("Renglon Cotizacion");
                //}
                //if (insDB.ExistsValueOnMultifile("dbo.RenglonCotizacion", "Descripcion", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Descripcion), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                //    vSbInfo.AppendLine("Renglon Cotizacion");
                //}
                //if (insDB.ExistsValueOnMultifile("dbo.RenglonCotizacion", "AlicuotaIVA", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.AlicuotaIVA), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                //    vSbInfo.AppendLine("Renglon Cotizacion");
                //}
                //if (insDB.ExistsValueOnMultifile("dbo.RenglonCotizacion", "PrecioSinIVA", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.PrecioSinIVA), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                //    vSbInfo.AppendLine("Renglon Cotizacion");
                //}
                //if (insDB.ExistsValueOnMultifile("dbo.RenglonCotizacion", "PrecioConIVA", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.PrecioConIVA), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                //    vSbInfo.AppendLine("Renglon Cotizacion");
                //}
                //if (insDB.ExistsValueOnMultifile("dbo.ProductoCompuesto", "CodigoArticulo", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Codigo), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                //    vSbInfo.AppendLine("Producto Compuesto");
                //}
                //if (insDB.ExistsValueOnMultifile("dbo.CodigoDeBarras", "CodigoArticulo", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Codigo), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                //    vSbInfo.AppendLine("Codigo De Barras");
                //}
                //if (insDB.ExistsValueOnMultifile("dbo.CodigoDeBarras", "CodigoDeBarra", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Codigo), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                //    vSbInfo.AppendLine("Codigo De Barras");
                //}
                //if (insDB.ExistsValueOnMultifile("dbo.RenglonFactura", "Articulo", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Codigo), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                //    vSbInfo.AppendLine("Renglon Factura");
                //}
                //if (insDB.ExistsValueOnMultifile("dbo.RenglonFactura", "Descripcion", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Descripcion), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                //    vSbInfo.AppendLine("Renglon Factura");
                //}
                //if (insDB.ExistsValueOnMultifile("dbo.RenglonFactura", "PrecioSinIVA", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.PrecioSinIVA), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                //    vSbInfo.AppendLine("Renglon Factura");
                //}
                //if (insDB.ExistsValueOnMultifile("dbo.RenglonFactura", "PrecioConIVA", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.PrecioConIVA), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                //    vSbInfo.AppendLine("Renglon Factura");
                //}
                //if (insDB.ExistsValueOnMultifile("dbo.FacturaMayoristaDetalle", "Articulo", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Codigo), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                //    vSbInfo.AppendLine("Factura Mayorista Detalle");
                //}
                //if (insDB.ExistsValueOnMultifile("dbo.FacturaMayoristaDetalle", "Descripcion", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Descripcion), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                //    vSbInfo.AppendLine("Factura Mayorista Detalle");
                //}
                //if (insDB.ExistsValueOnMultifile("dbo.FacturaMayoristaDetalle", "PrecioSinIVA", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.PrecioSinIVA), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                //    vSbInfo.AppendLine("Factura Mayorista Detalle");
                //}
                //if (insDB.ExistsValueOnMultifile("dbo.ExistenciaPorAlmacen", "CodigoArticulo", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Codigo), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                //    vSbInfo.AppendLine("Existencia Por Almacen");
                //}
                //if (insDB.ExistsValueOnMultifile("dbo.RenglonNotaES", "CodigoArticulo", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Codigo), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                //    vSbInfo.AppendLine("Renglon Nota ES");
                //}
                //if (insDB.ExistsValueOnMultifile("Saw.TransferenciaDetalle", "CodigoArticuloInventario", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Codigo), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                //    vSbInfo.AppendLine("Transferencia Detalle");
                //}
                //if (insDB.ExistsValueOnMultifile("dbo.RenglonContrato", "Articulo", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Codigo), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                //    vSbInfo.AppendLine("Renglon Contrato");
                //}
                //if (insDB.ExistsValueOnMultifile("dbo.RenglonContrato", "Descripcion", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Descripcion), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                //    vSbInfo.AppendLine("Renglon Contrato");
                //}
                //if (insDB.ExistsValueOnMultifile("dbo.CamposMonedaExtranjera", "Codigo", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Codigo), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                //    vSbInfo.AppendLine("Campos Moneda Extranjera");
                //}
                //if (insDB.ExistsValueOnMultifile("dbo.RenglonCompra", "CodigoArticulo", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Codigo), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                //    vSbInfo.AppendLine("Renglon Compra");
                //}
                //if (insDB.ExistsValueOnMultifile("dbo.RenglonConteoFisico", "CodigoArticulo", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Codigo), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                //    vSbInfo.AppendLine("Renglon Conteo Fisico");
                //}
                //if (insDB.ExistsValueOnMultifile("dbo.RenglonNotaDeEntrega", "CodigoArticulo", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Codigo), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                //    vSbInfo.AppendLine("Renglon Nota De Entrega");
                //}
                //if (insDB.ExistsValueOnMultifile("dbo.RenglonNotaDeEntrega", "Descripcion", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Descripcion), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                //    vSbInfo.AppendLine("Renglon Nota De Entrega");
                //}
                //if (insDB.ExistsValueOnMultifile("dbo.RenglonNotaDeEntrega", "PrecioSinIVA", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.PrecioSinIVA), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                //    vSbInfo.AppendLine("Renglon Nota De Entrega");
                //}
                //if (insDB.ExistsValueOnMultifile("dbo.RenglonNotaDeEntrega", "PrecioConIVA", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.PrecioConIVA), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                //    vSbInfo.AppendLine("Renglon Nota De Entrega");
                //}
                //if (insDB.ExistsValueOnMultifile("dbo.RenglonNEntregaXSerial", "CodigoArticulo", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Codigo), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                //    vSbInfo.AppendLine("Renglon NEntrega XSerial");
                //}
                //if (insDB.ExistsValueOnMultifile("Adm.FacturacionRapidaDetalle", "Codigo", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Codigo), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                //    vSbInfo.AppendLine("Facturacion Rapida Detalle");
                //}
                //if (insDB.ExistsValueOnMultifile("Adm.FacturacionRapidaDetalle", "Descripcion", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Descripcion), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                //    vSbInfo.AppendLine("Facturacion Rapida Detalle");
                //}
                //if (insDB.ExistsValueOnMultifile("Adm.FacturacionRapidaDetalle", "PrecioSinIVA", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.PrecioSinIVA), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                //    vSbInfo.AppendLine("Facturacion Rapida Detalle");
                //}
                //if (insDB.ExistsValueOnMultifile("Adm.FacturacionRapidaDetalle", "PrecioConIVA", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.PrecioConIVA), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                //    vSbInfo.AppendLine("Facturacion Rapida Detalle");
                //}
                //if (vSbInfo.Length == 0) {
                //    vResult.Success = true;
                //}
            } else {
                vResult.Success = true;
            }
            insDB.Dispose();
            if(!vResult.Success) {
                vErrMsg = LibResMsg.InfoForeignKeyCanNotBeDeleted(vSbInfo.ToString());
                throw new GalacAlertException(vErrMsg);
            } else {
                return vResult;
            }
        }

        [PrincipalPermission(SecurityAction.Demand,Role = "Articulo Inventario.Eliminar")]
        LibResponse ILibDataMasterComponent<IList<ArticuloInventario>,IList<ArticuloInventario>>.Delete(IList<ArticuloInventario> refRecord) {
            LibResponse vResult = new LibResponse();
            //try {
            //    string vErrMsg = "";
            //    CurrentRecord = refRecord[0];
            //    if (Validate(eAccionSR.Eliminar, out vErrMsg)) {
            //        if (ExecuteProcessBeforeDelete()) {
            //            insTrn.StartTransaction();
            //            vResult.Success = insTrn.ExecSpNonQuery(insTrn.ToSpName(DbSchema, "ArticuloInventarioDEL"), ParametrosClave(CurrentRecord, true, true));
            //            if (vResult.Success) {
            //                ExecuteProcessAfterDelete();
            //            }
            //            insTrn.CommitTransaction();
            //        }
            //    } else {
            //        throw new GalacException(vErrMsg, eExceptionManagementType.Validation);
            //    }
            return vResult;
            //} finally {
            //    if (!vResult.Success) {
            //        insTrn.RollBackTransaction();
            //    }
            //}
        }

        IList<ArticuloInventario> ILibDataMasterComponent<IList<ArticuloInventario>,IList<ArticuloInventario>>.GetData(eProcessMessageType valType,string valProcessMessage,StringBuilder valParameters,bool valUseDetail) {
            List<ArticuloInventario> vResult = new List<ArticuloInventario>();
            LibDatabase insDb = new LibDatabase();
            switch(valType) {
            case eProcessMessageType.SpName:
            valProcessMessage = insDb.ToSpName(DbSchema,valProcessMessage);
            vResult = insDb.LoadFromSp<ArticuloInventario>(valProcessMessage,valParameters,CmdTimeOut);
            if(valUseDetail && vResult != null && vResult.Count > 0) {
                //new clsProductoCompuestoDat().GetDetailAndAppendToMaster(ref vResult);
                //new clsExistenciaPorGrupoDat().GetDetailAndAppendToMaster(ref vResult);
                //new clsCodigoDeBarrasDat().GetDetailAndAppendToMaster(ref vResult);
            }
            break;
            default: throw new ProgrammerMissingCodeException();
            }
            insDb.Dispose();
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand,Role = "Articulo Inventario.Insertar")]
        LibResponse ILibDataMasterComponent<IList<ArticuloInventario>,IList<ArticuloInventario>>.Insert(IList<ArticuloInventario> refRecord,bool valUseDetail) {
            LibResponse vResult = new LibResponse();
            //try {
            //    CurrentRecord = refRecord[0];
            //    insTrn.StartTransaction();
            //    if (ExecuteProcessBeforeInsert()) {
            //        if (ValidateMasterDetail(eAccionSR.Insertar, CurrentRecord, valUseDetail)) {
            //            if (insTrn.ExecSpNonQuery(insTrn.ToSpName(DbSchema, "ArticuloInventarioINS"), ParametrosActualizacion(CurrentRecord, eAccionSR.Insertar))) {
            //                if (valUseDetail) {
            //                    vResult.Success = true;
            //                    InsertDetail(CurrentRecord);
            //                } else {
            //                    vResult.Success = true;
            //                }
            //                if (vResult.Success) {
            //                    ExecuteProcessAfterInsert();
            //                }
            //            }
            //        }
            //    }
            //    insTrn.CommitTransaction();
            return vResult;
            //} finally {
            //    if (!vResult.Success) {
            //        insTrn.RollBackTransaction();
            //    }
            //}
        }

        XElement ILibDataMasterComponent<IList<ArticuloInventario>,IList<ArticuloInventario>>.QueryInfo(eProcessMessageType valType,string valProcessMessage,StringBuilder valParameters) {
            XElement vResult = null;
            LibDatabase insDb = new LibDatabase();
            switch(valType) {
            case eProcessMessageType.SpName:
            vResult = LibXml.ToXElement(insDb.LoadFromSp(valProcessMessage,valParameters,CmdTimeOut));
            break;
            case eProcessMessageType.Query:
            vResult = LibXml.ToXElement(insDb.LoadData(valProcessMessage,CmdTimeOut,valParameters));
            break;
            default: throw new ProgrammerMissingCodeException();
            }
            insDb.Dispose();
            return vResult;
        }

        LibResponse ILibDataMasterComponent<IList<ArticuloInventario>,IList<ArticuloInventario>>.SpecializedUpdate(IList<ArticuloInventario> refRecord,bool valUseDetail,string valSpecializedAction) {
            throw new ProgrammerMissingCodeException();
        }

        [PrincipalPermission(SecurityAction.Demand,Role = "Articulo Inventario.Modificar")]
        LibResponse ILibDataMasterComponent<IList<ArticuloInventario>,IList<ArticuloInventario>>.Update(IList<ArticuloInventario> refRecord,bool valUseDetail,eAccionSR valAction) {
            LibResponse vResult = new LibResponse();
            //try {
            //    CurrentRecord = refRecord[0];
            //    if (ValidateMasterDetail(valAction, CurrentRecord, valUseDetail)) {
            //        insTrn.StartTransaction();
            //        if (ExecuteProcessBeforeUpdate()) {
            //            if (valUseDetail) {
            //                vResult = UpdateMasterAndDetail(CurrentRecord, valAction);
            //            } else {
            //                vResult = UpdateMaster(CurrentRecord, valAction); //por si requiriese especialización por acción
            //            }
            //            if (vResult.Success) {
            //                ExecuteProcessAfterUpdate();
            //            }
            //        }
            //        insTrn.CommitTransaction();
            //    }
            return vResult;
            //} finally {
            //    if (!vResult.Success) {
            //        insTrn.RollBackTransaction();
            //    }
            //}
        }

        bool ILibDataMasterComponent<IList<ArticuloInventario>,IList<ArticuloInventario>>.ValidateAll(IList<ArticuloInventario> refRecords,bool valUseDetail,eAccionSR valAction,StringBuilder refErrorMessage) {
            bool vResult = true;
            string vErroMessage = "";
            foreach(ArticuloInventario vItem in refRecords) {
                CurrentRecord = vItem;
                vResult = vResult && Validate(valAction,out vErroMessage);
                if(LibString.IsNullOrEmpty(vErroMessage,true)) {
                    refErrorMessage.AppendLine(vErroMessage);
                }
            }
            return vResult;
        }
        #endregion //ILibDataMasterComponent<IList<ArticuloInventario>, IList<ArticuloInventario>>

        LibResponse UpdateMaster(ArticuloInventario refRecord,eAccionSR valAction) {
            LibResponse vResult = new LibResponse();
            //vResult.Success = insTrn.ExecSpNonQuery(insTrn.ToSpName(DbSchema, "ArticuloInventarioUPD"), ParametrosActualizacion(refRecord, valAction));
            return vResult;
        }

        LibResponse UpdateMasterAndDetail(ArticuloInventario refRecord,eAccionSR valAction) {
            LibResponse vResult = new LibResponse();
            //string vErrorMessage = "";
            //if (ValidateDetail(refRecord, eAccionSR.Modificar,out vErrorMessage)) {
            //    if (UpdateDetail(refRecord)) {
            //        vResult = UpdateMaster(refRecord, valAction);
            //    }
            //}
            return vResult;
        }

        private bool InsertDetail(ArticuloInventario valRecord) {
            bool vResult = true;
            //vResult = vResult && SetPkInDetailProductoCompuestoAndUpdateDb(valRecord);
            //vResult = vResult && SetPkInDetailExistenciaPorGrupoAndUpdateDb(valRecord);
            //vResult = vResult && SetPkInDetailCodigoDeBarrasAndUpdateDb(valRecord);
            return vResult;
        }

        private bool SetPkInDetailProductoCompuestoAndUpdateDb(ArticuloInventario valRecord) {
            bool vResult = false;
            //int vConsecutivo = 1;
            //clsProductoCompuestoDat insProductoCompuesto = new clsProductoCompuestoDat();
            //foreach (ProductoCompuesto vDetail in valRecord.DetailProductoCompuesto) {
            //    vDetail.ConsecutivoCompania = valRecord.ConsecutivoCompania;
            //    vDetail.CodigoConexionConElMaster = valRecord.Codigo;
            //    vConsecutivo++;
            //}
            //vResult = insProductoCompuesto.InsertChild(valRecord, insTrn);
            return vResult;
        }

        private bool SetPkInDetailExistenciaPorGrupoAndUpdateDb(ArticuloInventario valRecord) {
            bool vResult = false;
            //int vConsecutivo = 1;
            //clsExistenciaPorGrupoDat insExistenciaPorGrupo = new clsExistenciaPorGrupoDat();
            //foreach (ExistenciaPorGrupo vDetail in valRecord.DetailExistenciaPorGrupo) {
            //    vDetail.ConsecutivoCompania = valRecord.ConsecutivoCompania;
            //    vDetail.CodigoArticulo = valRecord.Codigo;
            //    vConsecutivo++;
            //}
            //vResult = insExistenciaPorGrupo.InsertChild(valRecord, insTrn);
            return vResult;
        }

        private bool SetPkInDetailCodigoDeBarrasAndUpdateDb(ArticuloInventario valRecord) {
            bool vResult = false;
            //int vConsecutivo = 1;
            //clsCodigoDeBarrasDat insCodigoDeBarras = new clsCodigoDeBarrasDat();
            //foreach (CodigoDeBarras vDetail in valRecord.DetailCodigoDeBarras) {
            //    vDetail.ConsecutivoCompania = valRecord.ConsecutivoCompania;
            //    vDetail.CodigoArticulo = valRecord.Codigo;
            //    vConsecutivo++;
            //}
            //vResult = insCodigoDeBarras.InsertChild(valRecord, insTrn);
            return vResult;
        }

        private bool UpdateDetail(ArticuloInventario valRecord) {
            bool vResult = true;
            //vResult = vResult && SetPkInDetailProductoCompuestoAndUpdateDb(valRecord);
            //vResult = vResult && SetPkInDetailExistenciaPorGrupoAndUpdateDb(valRecord);
            //vResult = vResult && SetPkInDetailCodigoDeBarrasAndUpdateDb(valRecord);
            return vResult;
        }
        #region Validaciones
        protected override bool Validate(eAccionSR valAction,out string outErrorMessage) {
            bool vResult = true;
            //ClearValidationInfo();
            //vResult = IsValidConsecutivoCompania(valAction, CurrentRecord.ConsecutivoCompania, CurrentRecord.Codigo);
            //vResult = IsValidCodigo(valAction, CurrentRecord.ConsecutivoCompania, CurrentRecord.Codigo) && vResult;
            //vResult = IsValidDescripcion(valAction, CurrentRecord.Descripcion) && vResult;
            //vResult = IsValidLineaDeProducto(valAction, CurrentRecord.LineaDeProducto) && vResult;
            //vResult = IsValidCategoria(valAction, CurrentRecord.Categoria) && vResult;
            //vResult = IsValidFechaDeVencimiento(valAction, CurrentRecord.FechaDeVencimiento) && vResult;
            //vResult = IsValidUnidadDeVenta(valAction, CurrentRecord.UnidadDeVenta) && vResult;
            //vResult = IsValidTipoArticuloInv(valAction, CurrentRecord.TipoArticuloInvAsEnum) && vResult;
            outErrorMessage = Information.ToString();
            return vResult;
        }

        private bool IsValidConsecutivoCompania(eAccionSR valAction,int valConsecutivoCompania,string valCodigo) {
            bool vResult = true;
            //if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
            //    return true;
            //}
            //if (valConsecutivoCompania <= 0) {
            //    BuildValidationInfo(MsgRequiredField("Consecutivo Compania"));
            //    vResult = false;
            //}
            return vResult;
        }

        private bool IsValidCodigo(eAccionSR valAction,int valConsecutivoCompania,string valCodigo) {
            bool vResult = true;
            //if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
            //    return true;
            //}
            //valCodigo = LibString.Trim(valCodigo);
            //if (LibString.IsNullOrEmpty(valCodigo, true)) {
            //    BuildValidationInfo(MsgRequiredField("Código del Artículo"));
            //    vResult = false;
            //} else if (valAction == eAccionSR.Insertar) {
            //    if (KeyExists(valConsecutivoCompania, valCodigo)) {
            //        BuildValidationInfo(MsgFieldValueAlreadyExist("Código del Artículo", valCodigo));
            //        vResult = false;
            //    }
            //}
            return vResult;
        }

        private bool IsValidDescripcion(eAccionSR valAction,string valDescripcion) {
            bool vResult = true;
            //if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
            //    return true;
            //}
            //valDescripcion = LibString.Trim(valDescripcion);
            //if (LibString.IsNullOrEmpty(valDescripcion, true)) {
            //    BuildValidationInfo(MsgRequiredField("Descripción"));
            //    vResult = false;
            //}
            return vResult;
        }

        private bool IsValidLineaDeProducto(eAccionSR valAction,string valLineaDeProducto) {
            bool vResult = true;
            //if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
            //    return true;
            //}
            //valLineaDeProducto = LibString.Trim(valLineaDeProducto);
            //if (LibString.IsNullOrEmpty(valLineaDeProducto , true)) {
            //    BuildValidationInfo(MsgRequiredField("Linea De Producto"));
            //    vResult = false;
            //} else {
            //    LibDatabase insDb = new LibDatabase();
            //    if (!insDb.ExistsValue("dbo.LineaDeProducto", "Nombre", insDb.InsSql.ToSqlValue(valLineaDeProducto), true)) {
            //        BuildValidationInfo("El valor asignado al campo Linea De Producto no existe, escoga nuevamente.");
            //        vResult = false;
            //    }
            //}
            return vResult;
        }

        private bool IsValidCategoria(eAccionSR valAction,string valCategoria) {
            bool vResult = true;
            //if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
            //    return true;
            //}
            //valCategoria = LibString.Trim(valCategoria);
            //if (LibString.IsNullOrEmpty(valCategoria , true)) {
            //    BuildValidationInfo(MsgRequiredField("Categoria"));
            //    vResult = false;
            //} else {
            //    LibDatabase insDb = new LibDatabase();
            //    if (!insDb.ExistsValue("Saw.Categoria", "Descripcion", insDb.InsSql.ToSqlValue(valCategoria), true)) {
            //        BuildValidationInfo("El valor asignado al campo Categoria no existe, escoga nuevamente.");
            //        vResult = false;
            //    }
            //}
            return vResult;
        }

        private bool IsValidFechaDeVencimiento(eAccionSR valAction,DateTime valFechaDeVencimiento) {
            bool vResult = true;
            //if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
            //    return true;
            //}
            //if (LibDefGen.DateIsGreaterThanDateLimitForEnterData(valFechaDeVencimiento, false, valAction)) {
            //    BuildValidationInfo(LibDefGen.MessageDateRestrictionDemoProgram());
            //    vResult = false;
            //}
            return vResult;
        }

        private bool IsValidUnidadDeVenta(eAccionSR valAction,string valUnidadDeVenta) {
            bool vResult = true;
            //if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
            //    return true;
            //}
            //valUnidadDeVenta = LibString.Trim(valUnidadDeVenta);
            //if (LibString.IsNullOrEmpty(valUnidadDeVenta , true)) {
            //    BuildValidationInfo(MsgRequiredField("Unidad De Venta"));
            //    vResult = false;
            //} else {
            //    LibDatabase insDb = new LibDatabase();
            //    if (!insDb.ExistsValue("Saw.UnidadDeVenta", "Nombre", insDb.InsSql.ToSqlValue(valUnidadDeVenta), true)) {
            //        BuildValidationInfo("El valor asignado al campo Unidad De Venta no existe, escoga nuevamente.");
            //        vResult = false;
            //    }
            //}
            return vResult;
        }

        private bool IsValidTipoArticuloInv(eAccionSR valAction,eTipoArticuloInv valTipoArticuloInv) {
            bool vResult = true;
            //if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
            //    return true;
            //}
            return vResult;
        }

        private bool KeyExists(int valConsecutivoCompania,string valCodigo) {
            bool vResult = false;
            //ArticuloInventario vRecordBusqueda = new ArticuloInventario();
            //vRecordBusqueda.ConsecutivoCompania = valConsecutivoCompania;
            //vRecordBusqueda.Codigo = valCodigo;
            //LibDatabase insDb = new LibDatabase();
            //vResult = insDb.ExistsRecord(DbSchema + ".ArticuloInventario", "ConsecutivoCompania", ParametrosClave(vRecordBusqueda, false, false));
            //insDb.Dispose();
            return vResult;
        }

        private bool ValidateMasterDetail(eAccionSR valAction,ArticuloInventario valRecordMaster,bool valUseDetail) {
            bool vResult = false;
            string vErrMsg;
            if(Validate(valAction,out vErrMsg)) {
                if(valUseDetail) {
                    if(ValidateDetail(valRecordMaster,eAccionSR.Insertar,out vErrMsg)) {
                        vResult = true;
                    } else {
                        throw new GalacValidationException("Artículo Inventario (detalle)\n" + vErrMsg);
                    }
                } else {
                    vResult = true;
                }
            } else {
                throw new GalacValidationException(vErrMsg);
            }
            return vResult;
        }

        private bool ValidateDetail(ArticuloInventario valRecord,eAccionSR valAction,out string outErrorMessage) {
            bool vResult = true;
            outErrorMessage = "";
            //vResult = vResult && ValidateDetailProductoCompuesto(valRecord, valAction, out outErrorMessage);
            //vResult = vResult && ValidateDetailExistenciaPorGrupo(valRecord, valAction, out outErrorMessage);
            //vResult = vResult && ValidateDetailCodigoDeBarras(valRecord, valAction, out outErrorMessage);
            return vResult;
        }

        private bool ValidateDetailProductoCompuesto(ArticuloInventario valRecord,eAccionSR valAction,out string outErrorMessage) {
            bool vResult = true;
            StringBuilder vSbErrorInfo = new StringBuilder();
            //int vNumeroDeLinea = 1;
            //foreach (ProductoCompuesto vDetail in valRecord.DetailProductoCompuesto) {
            //    bool vLineHasError = true;
            //    //agregar validaciones
            //    if (LibString.IsNullOrEmpty(vDetail.CodigoArticulo)) {
            //        vSbErrorInfo.AppendLine("Línea " + vNumeroDeLinea.ToString() + ": No fue asignado el Código Inventario.");
            //    } else {
            //        vLineHasError = false;
            //    }
            //    vResult = vResult && (!vLineHasError);
            //    vNumeroDeLinea++;
            //}
            outErrorMessage = vSbErrorInfo.ToString();
            return vResult;
        }

        private bool ValidateDetailExistenciaPorGrupo(ArticuloInventario valRecord,eAccionSR valAction,out string outErrorMessage) {
            bool vResult = true;
            StringBuilder vSbErrorInfo = new StringBuilder();
            //int vNumeroDeLinea = 1;
            //foreach (ExistenciaPorGrupo vDetail in valRecord.DetailExistenciaPorGrupo) {
            //    bool vLineHasError = true;
            //    //agregar validaciones
            //    if (LibString.IsNullOrEmpty(vDetail.codigoGrupo)) {
            //        vSbErrorInfo.AppendLine("Línea " + vNumeroDeLinea.ToString() + ": No fue asignado el codigo Grupo.");
            //    } else if (LibString.IsNullOrEmpty(vDetail.CodigoTalla)) {
            //        vSbErrorInfo.AppendLine("Línea " + vNumeroDeLinea.ToString() + ": No fue asignado el Codigo Talla.");
            //    } else if (LibString.IsNullOrEmpty(vDetail.CodigoCOlor)) {
            //        vSbErrorInfo.AppendLine("Línea " + vNumeroDeLinea.ToString() + ": No fue asignado el Codigo COlor.");
            //    } else {
            //        vLineHasError = false;
            //    }
            //    vResult = vResult && (!vLineHasError);
            //    vNumeroDeLinea++;
            //}
            outErrorMessage = vSbErrorInfo.ToString();
            return vResult;
        }

        private bool ValidateDetailCodigoDeBarras(ArticuloInventario valRecord,eAccionSR valAction,out string outErrorMessage) {
            bool vResult = true;
            StringBuilder vSbErrorInfo = new StringBuilder();
            //int vNumeroDeLinea = 1;
            //foreach (CodigoDeBarras vDetail in valRecord.DetailCodigoDeBarras) {
            //    bool vLineHasError = true;
            //    //agregar validaciones
            //    if (LibString.IsNullOrEmpty(vDetail.CodigoArticulo)) {
            //        vSbErrorInfo.AppendLine("Línea " + vNumeroDeLinea.ToString() + ": No fue asignado el Código Inventario.");
            //    } else if (LibString.IsNullOrEmpty(vDetail.CodigoDeBarra)) {
            //        vSbErrorInfo.AppendLine("Línea " + vNumeroDeLinea.ToString() + ": No fue asignado el Código Inventario.");
            //    } else {
            //        vLineHasError = false;
            //    }
            //    vResult = vResult && (!vLineHasError);
            //    vNumeroDeLinea++;
            //}
            outErrorMessage = vSbErrorInfo.ToString();
            return vResult;
        }
        #endregion //Validaciones
        #region Miembros de ILibDataFKSearch
        bool ILibDataFKSearch.ConnectFk(ref XmlDocument refResulset,eProcessMessageType valType,string valProcessMessage,StringBuilder valXmlParamsExpression) {
            bool vResult = false;
            LibDatabase insDb = new LibDatabase();
            refResulset = insDb.LoadForConnect(valProcessMessage,valXmlParamsExpression,CmdTimeOut);
            if(refResulset != null && refResulset.DocumentElement != null && refResulset.DocumentElement.HasChildNodes) {
                vResult = true;
            }
            return vResult;
        }
        #endregion //Miembros de ILibDataFKSearch

        #region //Miembros de ILibDataRpt

        System.Data.DataTable ILibDataRpt.GetDt(string valSqlStringCommand,int valCmdTimeout) {
            return new LibDataReport().GetDataTableForReport(valSqlStringCommand,valCmdTimeout);
        }

        System.Data.DataTable ILibDataRpt.GetDt(string valSpName,StringBuilder valXmlParamsExpression,int valCmdTimeout) {
            return new LibDataReport().GetDataTableForReport(valSpName,valXmlParamsExpression,valCmdTimeout);
        }
        #endregion ////Miembros de ILibDataRpt
        #endregion //Metodos Generados


    } //End of class clsArticuloInventarioDat

} //End of namespace Galac.Saw.Dal.Inventario

