using Galac.Saw.Ccl.SttDef;
using Galac.Saw.Ccl.Tablas;
using LibGalac.Aos.Base;
using LibGalac.Aos.Dal;
using Galac.Saw.Ccl.Inventario;
using System;
using System.Text;
using LibGalac.Aos.Base.Dal;
using System.Windows.Forms;
using Galac.Saw.Dal.Tablas;
using LibGalac.Aos.Base;
using LibGalac.Aos.Dal;
using System;
using System.Text;
using Galac.Saw.Brl.Tablas;
using System.ComponentModel.DataAnnotations;
using LibGalac.Aos.Brl;
using System.Data;
using LibGalac.Aos.Cnf;
using Galac.Saw.Lib;
using LibGalac.Aos.DefGen;
using Galac.Adm.Dal.Venta;
using Galac.Saw.Dal.Inventario;
using Galac.Adm.Ccl.Venta;
using LibGalac.Aos.Catching;

namespace Galac.Saw.DDL.VersionesReestructuracion {

    class clsVersionTemporalNoOficial: clsVersionARestructurar {

        public clsVersionTemporalNoOficial(string valCurrentDataBaseName) : base(valCurrentDataBaseName) { }
        public override bool UpdateToVersion() {
            StartConnectionNoTransaction();
            CrearTabFormaDelCobro();
            CrearCampoManejaMerma();
            CrearCampoManejaMermaOP();
            AmpliarColumnaCompaniaImprentaDigitalClave();
            AgregarReglaContabilizacionProduccionMermaAnormal();
            ParametrosCreditoElectronico();
            FormaDelCobro();
            CxC();
            ActualizaArticulosLote_LoteFeVec();
            CrearOtrosCargosDeFactura();
            CamposCreditoElectronicoEnCajaApertura();
            AgregaColumnasReglasDeContabilizacionCxCCreditoElectronico();
            AgregarDefaultValueOtrosCargos();
            CorrigeConstrainsGUIDNOtNullFactura();
            ActivaMostrarTotalEnDivisas();
            AgregarTablaExistenciaPorAlmacenDetLoteInv();
            CrearCamposIDEnCxP();
            QuitarUniqueCaja();
            DisposeConnectionNoTransaction();
            return true;
        }

        private void CrearCampoManejaMerma() {
            if (!ColumnExists("Adm.ListaDeMateriales", "ManejaMerma")) {
                AddColumnBoolean("Adm.ListaDeMateriales", "ManejaMerma", "CONSTRAINT nnLisDeMatManejaMerm NOT NULL", false);
                if (AddColumnNumeric("Adm.ListaDeMaterialesDetalleArticulo", "MermaNormal", 25, 8, "", 0)) {
                    AddDefaultConstraint("Adm.ListaDeMaterialesDetalleArticulo", "d_LisDeMatDetArtMeNo", "0", "MermaNormal");
                }

                if (AddColumnNumeric("Adm.ListaDeMaterialesDetalleArticulo", "PorcentajeMermaNormal", 25, 8, "", 0)) {
                    AddDefaultConstraint("Adm.ListaDeMaterialesDetalleArticulo", "d_LisDeMatDetArtPoMeNo", "0", "PorcentajeMermaNormal");
                }

                if (AddColumnNumeric("Adm.ListaDeMaterialesDetalleSalidas", "MermaNormal", 25, 8, "", 0)) {
                    AddDefaultConstraint("Adm.ListaDeMaterialesDetalleSalidas", "d_LisDeMatDetSalMeNo", "0", "MermaNormal");
                }

                if (AddColumnNumeric("Adm.ListaDeMaterialesDetalleSalidas", "PorcentajeMermaNormal", 25, 8, "", 0)) {
                    AddDefaultConstraint("Adm.ListaDeMaterialesDetalleSalidas", "d_LisDeMatDetSalPoMeNo", "0", "PorcentajeMermaNormal");
                }
            }
        }

        private void CrearCampoManejaMermaOP() {
            if (!ColumnExists("Adm.OrdenDeProduccion", "ListaUsaMerma")) {
                AddColumnBoolean("Adm.OrdenDeProduccion", "ListaUsaMerma", "CONSTRAINT nnOrdDeProListaUsaMer NOT NULL", false);
                if (AddColumnNumeric("Adm.OrdenDeProduccionDetalleArticulo", "PorcentajeMermaNormalOriginal", 25, 8, "", 0)) {
                    AddDefaultConstraint("Adm.OrdenDeProduccionDetalleArticulo", "d_OrdDeProDetArtPoMeNoOr", "0", "PorcentajeMermaNormalOriginal");
                }

                if (AddColumnNumeric("Adm.OrdenDeProduccionDetalleArticulo", "CantidadMermaNormal", 25, 8, "", 0)) {
                    AddDefaultConstraint("Adm.OrdenDeProduccionDetalleArticulo", "d_OrdDeProDetArtCaMeNo", "0", "CantidadMermaNormal");
                }

                if (AddColumnNumeric("Adm.OrdenDeProduccionDetalleArticulo", "PorcentajeMermaNormal", 25, 8, "", 0)) {
                    AddDefaultConstraint("Adm.OrdenDeProduccionDetalleArticulo", "d_OrdDeProDetArtPoMeNo", "0", "PorcentajeMermaNormal");
                }

                if (AddColumnNumeric("Adm.OrdenDeProduccionDetalleArticulo", "CantidadMermaAnormal", 25, 8, "", 0)) {
                    AddDefaultConstraint("Adm.OrdenDeProduccionDetalleArticulo", "d_OrdDeProDetArtCaMeAn", "0", "CantidadMermaAnormal");
                }

                if (AddColumnNumeric("Adm.OrdenDeProduccionDetalleArticulo", "PorcentajeMermaAnormal", 25, 8, "", 0)) {
                    AddDefaultConstraint("Adm.OrdenDeProduccionDetalleArticulo", "d_OrdDeProDetArtPoMeAn", "0", "PorcentajeMermaAnormal");
                }

                if (AddColumnNumeric("Adm.OrdenDeProduccionDetalleMateriales", "PorcentajeMermaNormalOriginal", 25, 8, "", 0)) {
                    AddDefaultConstraint("Adm.OrdenDeProduccionDetalleMateriales", "d_OrdDeProDetMatPoMeNoOr", "0", "PorcentajeMermaNormalOriginal");
                }

                if (AddColumnNumeric("Adm.OrdenDeProduccionDetalleMateriales", "CantidadMermaNormal", 25, 8, "", 0)) {
                    AddDefaultConstraint("Adm.OrdenDeProduccionDetalleMateriales", "d_OrdDeProDetMatCaMeNo", "0", "CantidadMermaNormal");
                }

                if (AddColumnNumeric("Adm.OrdenDeProduccionDetalleMateriales", "PorcentajeMermaNormal", 25, 8, "", 0)) {
                    AddDefaultConstraint("Adm.OrdenDeProduccionDetalleMateriales", "d_OrdDeProDetMatPoMeNo", "0", "PorcentajeMermaNormal");
                }

                if (AddColumnNumeric("Adm.OrdenDeProduccionDetalleMateriales", "CantidadMermaAnormal", 25, 8, "", 0)) {
                    AddDefaultConstraint("Adm.OrdenDeProduccionDetalleMateriales", "d_OrdDeProDetMatCaMeAn", "0", "CantidadMermaAnormal");
                }

                if (AddColumnNumeric("Adm.OrdenDeProduccionDetalleMateriales", "PorcentajeMermaAnormal", 25, 8, "", 0)) {
                    AddDefaultConstraint("Adm.OrdenDeProduccionDetalleMateriales", "d_OrdDeProDetMatPoMeAn", "0", "PorcentajeMermaAnormal");
                }
            }
        }


        private void AmpliarColumnaCompaniaImprentaDigitalClave() {
            ModifyLengthOfColumnString("Compania", "ImprentaDigitalClave", 1000, "");
        }

        private void AgregarReglaContabilizacionProduccionMermaAnormal() {
            if (!ColumnExists("Saw.ReglasDeContabilizacion", "CuentaMermaAnormal")) {
                if (AddColumnString("Saw.ReglasDeContabilizacion", "CuentaMermaAnormal", 30, "", "")) {
                    AddDefaultConstraint("Saw.ReglasDeContabilizacion", "d_RegDeConCuMeAn", _insSql.ToSqlValue(""), "CuentaMermaAnormal");
                }
            }
        }

        private void CxC() {
            if (!ColumnExists("CxC", "VieneDeCreditoElectronico")) {
                AddColumnBoolean("CxC", "VieneDeCreditoElectronico", "CONSTRAINT nnCxCVieneDeCre NOT NULL", false);
            }
        }

        private void FormaDelCobro() {
            StringBuilder vSQL = new StringBuilder();
            vSQL.AppendLine("SELECT Codigo FROM SAW.FormaDelCobro");
            if (RecordCountOfSql(vSQL.ToString()) <= 0) {
                string vNextCodigo = "";
                vNextCodigo = new LibGalac.Aos.Dal.LibDatabase("").NextStrConsecutive("SAW.FormaDelCobro", "Codigo", "", true, 5);
                vSQL.Clear();
                vSQL.AppendLine("INSERT INTO SAW.FormaDelCobro (Codigo, Nombre, TipoDePago) VALUES (" + InsSql.ToSqlValue(vNextCodigo) + ", " + InsSql.ToSqlValue("Crédito Electrónico") + ", " + _insSql.EnumToSqlValue((int)eFormaDeCobro.CreditoElectronico) + ")");
                Execute(vSQL.ToString(), 0);
            }
        }

        private void ParametrosCreditoElectronico() {
            string vGroupNameNuevo = "2.9.- Cobro de Factura";
            string vGroupNameActual = "2.2.- Facturación (Continuación) ";
            int vLevelGroupNuevo = 9;
            AgregarNuevoParametro("UsaCreditoElectronico", "Factura", 2, vGroupNameNuevo, 9, "", eTipoDeDatoParametros.String, "", 'N', "N");
            AgregarNuevoParametro("NombreCreditoElectronico", "Factura", 2, vGroupNameNuevo, 9, "", eTipoDeDatoParametros.String, "", 'N', "Crédito Electrónico");
            AgregarNuevoParametro("DiasDeCreditoPorCuotaCreditoElectronico", "Factura", 2, vGroupNameNuevo, 9, "", eTipoDeDatoParametros.Int, "", 'N', "14");
            AgregarNuevoParametro("CantidadCuotasUsualesCreditoElectronico", "Factura", 2, vGroupNameNuevo, 9, "", eTipoDeDatoParametros.Int, "", 'N', "6");
            AgregarNuevoParametro("MaximaCantidadCuotasCreditoElectronico", "Factura", 2, vGroupNameNuevo, 9, "", eTipoDeDatoParametros.Int, "", 'N', "6");
            AgregarNuevoParametro("UsaClienteUnicoCreditoElectronico", "Factura", 2, vGroupNameNuevo, 9, "", eTipoDeDatoParametros.String, "", 'N', "N");
            AgregarNuevoParametro("CodigoClienteCreditoElectronico", "Factura", 2, vGroupNameNuevo, 9, "", eTipoDeDatoParametros.String, "", 'N', "");
            AgregarNuevoParametro("GenerarUnaUnicaCuotaCreditoElectronico", "Factura", 2, vGroupNameNuevo, 9, "", eTipoDeDatoParametros.String, "", 'N', "N");

            MoverGroupName("EmitirDirecto", vGroupNameActual, vGroupNameNuevo, vLevelGroupNuevo);
            MoverGroupName("UsaCobroDirecto", vGroupNameActual, vGroupNameNuevo, vLevelGroupNuevo);
            MoverGroupName("UsaCobroDirectoEnMultimoneda", vGroupNameActual, vGroupNameNuevo, vLevelGroupNuevo);
            MoverGroupName("CuentaBancariaCobroDirecto", vGroupNameActual, vGroupNameNuevo, vLevelGroupNuevo);
            MoverGroupName("UsaMediosElectronicosDeCobro", vGroupNameActual, vGroupNameNuevo, vLevelGroupNuevo);
            MoverGroupName("ConceptoBancarioCobroDirecto", vGroupNameActual, vGroupNameNuevo, vLevelGroupNuevo);
            MoverGroupName("CuentaBancariaCobroMultimoneda", vGroupNameActual, vGroupNameNuevo, vLevelGroupNuevo);
            MoverGroupName("ConceptoBancarioCobroMultimoneda", vGroupNameActual, vGroupNameNuevo, vLevelGroupNuevo);
        }

        private void ActualizaArticulosLote_LoteFeVec() {
            StringBuilder vSql = new StringBuilder();
            vSql.AppendLine("UPDATE Saw.LoteDeInventario ");
            vSql.AppendLine("SET Saw.LoteDeInventario.FechaDeVencimiento = " + InsSql.ToSqlValue(LibDate.MaxDateForDB()));
            vSql.AppendLine(", Saw.LoteDeInventario.FechaDeElaboracion = " + InsSql.ToSqlValue(LibDate.MaxDateForDB()));
            vSql.AppendLine("FROM ArticuloInventario INNER JOIN Saw.LoteDeInventario ");
            vSql.AppendLine("ON ArticuloInventario.ConsecutivoCompania = Saw.LoteDeInventario.ConsecutivoCompania ");
            vSql.AppendLine("AND ArticuloInventario.Codigo = Saw.LoteDeInventario.CodigoArticulo");
            vSql.AppendLine("WHERE ArticuloInventario.TipoDeArticulo = " + InsSql.EnumToSqlValue((int)eTipoDeArticulo.Mercancia));
            vSql.AppendLine("AND ArticuloInventario.TipoArticuloInv = " + InsSql.EnumToSqlValue((int)eTipoArticuloInv.Lote));
            Execute(vSql.ToString());
        }
        private void CrearOtrosCargosDeFactura() {
            new clsOtrosCargosDeFacturaED().InstalarVistasYSps();
        }

        private void CamposCreditoElectronicoEnCajaApertura() {
            if (!ColumnExists("Adm.CajaApertura", "MontoCreditoElectronico")) {
                if (AddColumnDecimal("Adm.CajaApertura", "MontoCreditoElectronico", 25, 4, "", 0)) {
                    AddDefaultConstraint("Adm.CajaApertura", "d_CajApeMoCrEl", "0", "MontoCreditoElectronico");
                }
            }
        }

        private void AgregaColumnasReglasDeContabilizacionCxCCreditoElectronico() {
            if (!ColumnExists("Saw.ReglasDeContabilizacion", "CuentaFacturacionCxCCreditoElectronico")) {
                if (AddColumnString("Saw.ReglasDeContabilizacion", "CuentaFacturacionCxCCreditoElectronico", 30, "", "")) {
                    AddDefaultConstraint("Saw.ReglasDeContabilizacion", "d_RegDeConCuFacCxCCreEle", _insSql.ToSqlValue(""), "CuentaFacturacionCxCCreditoElectronico");
                }
            }
        }

        private void AgregarDefaultValueOtrosCargos() {
            if (ColumnExists("dbo.otrosCargosDeFactura", "Status")) {
                AddDefaultConstraint("dbo.otrosCargosDeFactura", "d_otrCarDeFacSt", "'0'", "Status");
            }
            if (ColumnExists("dbo.otrosCargosDeFactura", "SeCalculaEnBasea")) {
                AddDefaultConstraint("dbo.otrosCargosDeFactura", "d_otrCarDeFacSeCaEnBa", "'0'", "SeCalculaEnBasea");
            }
            if (ColumnExists("dbo.otrosCargosDeFactura", "Monto")) {
                AddDefaultConstraint("dbo.otrosCargosDeFactura", "d_otrCarDeFacMo", "0", "Monto");
            }
            if (ColumnExists("dbo.otrosCargosDeFactura", "BaseFormula")) {
                AddDefaultConstraint("dbo.otrosCargosDeFactura", "d_otrCarDeFacBaFo", "'0'", "BaseFormula");
            }
            if (ColumnExists("dbo.otrosCargosDeFactura", "PorcentajeSobreBase")) {
                AddDefaultConstraint("dbo.otrosCargosDeFactura", "d_otrCarDeFacPoSoBa", "0", "PorcentajeSobreBase");
            }
            if (ColumnExists("dbo.otrosCargosDeFactura", "Sustraendo")) {
                AddDefaultConstraint("dbo.otrosCargosDeFactura", "d_otrCarDeFacSu", "0", "Sustraendo");
            }
            if (ColumnExists("dbo.otrosCargosDeFactura", "ComoAplicaAlTotalFactura")) {
                AddDefaultConstraint("dbo.otrosCargosDeFactura", "d_otrCarDeFacCoApAlToFa", "'0'", "ComoAplicaAlTotalFactura");
            }
            if (ColumnExists("dbo.otrosCargosDeFactura", "CuentaContableOtrosCargos")) {
                AddDefaultConstraint("dbo.otrosCargosDeFactura", "d_otrCarDeFacCuCoOtCa", _insSql.ToSqlValue(""), "CuentaContableOtrosCargos");
            }
            if (ColumnExists("dbo.otrosCargosDeFactura", "PorcentajeComision")) {
                AddDefaultConstraint("dbo.otrosCargosDeFactura", "d_otrCarDeFacPoCo", "0", "PorcentajeComision");
            }
        }

        private void CorrigeConstrainsGUIDNOtNullFactura() {
            AddNotNullConstraint("factura", "ImprentaDigitalGUID", InsSql.VarCharTypeForDb(50));
        }

        private void ActivaMostrarTotalEnDivisas() {
            StringBuilder vSql = new StringBuilder();
            vSql.AppendLine("UPDATE Comun.SettvalueByCompany");
            vSql.AppendLine("SET VALUE =" + InsSql.ToSqlValue(true));
            vSql.AppendLine(" WHERE NameSettDefinition = " + InsSql.ToSqlValue("SeMuestraTotalEnDivisas"));
            vSql.AppendLine(" AND ConsecutivoCompania IN (SELECT ConsecutivoCompania FROM Comun.SettvalueByCompany WHERE NameSettDefinition = " + InsSql.ToSqlValue("UsaCobroDirectoEnMultimoneda") + " AND Value = " + InsSql.ToSqlValue(true) + ")");
            Execute(vSql.ToString());
        }

        private void AgregarTablaExistenciaPorAlmacenDetLoteInv() {
            if (!TableExists("ExistenciaPorAlmacenDetLoteInv")) {
                if (new clsExistenciaPorAlmacenDetLoteInvED().InstalarTabla()) {
                    StringBuilder vSqlSb = new StringBuilder();
                    vSqlSb.AppendLine("DELETE FROM ExistenciaPorAlmacenDetLoteInv");
                    Execute(vSqlSb.ToString(), 0);
                    vSqlSb.Clear();
                    vSqlSb.AppendLine("INSERT INTO ExistenciaPorAlmacenDetLoteInv ");
                    vSqlSb.AppendLine("      (ConsecutivoCompania, ConsecutivoAlmacen, CodigoArticulo, ConsecutivoLoteInventario, Cantidad, Ubicacion)");
                    vSqlSb.AppendLine("SELECT ");
                    vSqlSb.AppendLine("		ExistenciaPorAlmacen.ConsecutivoCompania AS ConsecutivoCompania,");
                    vSqlSb.AppendLine("		ExistenciaPorAlmacen.ConsecutivoAlmacen AS CosecutivoAlmacen,");
                    vSqlSb.AppendLine("		Saw.LoteDeInventario.CodigoArticulo AS CodigoArticulo,");
                    vSqlSb.AppendLine("		Saw.LoteDeInventario.Consecutivo AS ConsecutivoLoteInventario,");
                    vSqlSb.AppendLine("		SUM( Saw.LoteDeInventarioMovimiento.Cantidad) AS Cantidad,");
                    vSqlSb.AppendLine("		ExistenciaPorAlmacen.Ubicacion");
                    vSqlSb.AppendLine("	FROM");
                    vSqlSb.AppendLine("	    Saw.LoteDeInventario");
                    vSqlSb.AppendLine("	INNER JOIN Saw.LoteDeInventarioMovimiento");
                    vSqlSb.AppendLine("		ON Saw.LoteDeInventario.ConsecutivoCompania = Saw.LoteDeInventarioMovimiento.ConsecutivoCompania");
                    vSqlSb.AppendLine("		AND Saw.LoteDeInventario.Consecutivo = Saw.LoteDeInventarioMovimiento.ConsecutivoLote");
                    vSqlSb.AppendLine("	INNER JOIN ExistenciaPorAlmacen");
                    vSqlSb.AppendLine("		ON ExistenciaPorAlmacen.ConsecutivoCompania  = Saw.LoteDeInventario.ConsecutivoCompania");
                    vSqlSb.AppendLine("		AND ExistenciaPorAlmacen.CodigoArticulo = Saw.LoteDeInventario.CodigoArticulo");
                    vSqlSb.AppendLine("		GROUP BY");
                    vSqlSb.AppendLine("			ExistenciaPorAlmacen.ConsecutivoCompania,");
                    vSqlSb.AppendLine("			ExistenciaPorAlmacen.ConsecutivoAlmacen,");
                    vSqlSb.AppendLine("			Saw.LoteDeInventario.CodigoArticulo ,");
                    vSqlSb.AppendLine("			Saw.LoteDeInventario.Consecutivo,");
                    vSqlSb.AppendLine("			ExistenciaPorAlmacen.Ubicacion");
                    Execute(vSqlSb.ToString(), 0);
                }
            }
        }

        private void CrearTabFormaDelCobro() {
            if (!TableExists("Adm.FormaDelCobro")) {
                new clsFormaDelCobroED().InstalarTabla();
                DeleteAllrelationShipsBetweenTables(_CurrentDataBaseName, "Saw.FormaDelCobro", "dbo.renglonCobroDeFactura");
                CrearColumnaConsecutivoFormaDelCobro();
                InsertDefaultRecord();
                InsertarFormasDeCobroCreadasPorUsuario();
                InsertarFormasDeCobroModificadasPorUsuario();
                ActualizarConsecutivoFormaDelCobro();
                AddForeignKey("Adm.FormaDelCobro", "dbo.renglonCobroDeFactura", new string[] { "ConsecutivoCompania", "Consecutivo" }, new string[] { "ConsecutivoCompania", "ConsecutivoFormaDelCobro" }, false, false);
            }
        }

        void InsertarFormasDeCobroCreadasPorUsuario() {
            LibTrn insTrn = new LibTrn();
            insTrn.StartConnectionNoTransaction();
            string vSqlCompanias = "SELECT ConsecutivoCompania, Value AS CtaBancaria FROM Comun.SettValueByCompany WHERE NameSettDefinition = 'CodigoGenericoCuentaBancaria'";
            string vSqlUpdatesCodigoFormasDeCobroInsertadas = SqlUpdatesCodigoFormasDeCobroInsertadas();
            DataSet vDsUpdates = insTrn.ExecuteDataset(vSqlUpdatesCodigoFormasDeCobroInsertadas, 0);
            DataSet vDsCia = insTrn.ExecuteDataset(vSqlCompanias, 0);
            if (vDsCia != null && vDsCia.Tables != null && vDsCia.Tables.Count > 0 && vDsCia.Tables[0] != null && LibDataTable.DataTableHasRows(vDsCia.Tables[0])) {
                if (vDsUpdates != null && vDsUpdates.Tables != null && vDsUpdates.Tables.Count > 0 && vDsUpdates.Tables[0] != null && LibDataTable.DataTableHasRows(vDsUpdates.Tables[0])) {
                    foreach (DataRow vRow in vDsCia.Tables[0].Rows) {
                        int vConsecutivoCompania = LibConvert.ToInt(vRow["ConsecutivoCompania"].ToString());
                        string vCodigoCtaBancaria = LibConvert.ToStr(vRow["CtaBancaria"].ToString());
                        string vSql = SqlInsertarFormasDeCobroInsertadas(vConsecutivoCompania, vCodigoCtaBancaria);
                        LibBusiness.ExecuteUpdateOrDelete(vSql, new StringBuilder(), "", 0);
                    }
                    foreach (DataRow vRow in vDsUpdates.Tables[0].Rows) {
                        string vSql = LibConvert.ToStr(vRow["UpdateCambioCodigo"].ToString());
                        LibBusiness.ExecuteUpdateOrDelete(vSql, new StringBuilder(), "", 0);
                    }
                }
            }
        }

        void InsertarFormasDeCobroModificadasPorUsuario() {
            LibTrn insTrn = new LibTrn();
            insTrn.StartConnectionNoTransaction();
            string vSqlCompanias = "SELECT ConsecutivoCompania, Value AS CtaBancaria FROM Comun.SettValueByCompany WHERE NameSettDefinition = 'CodigoGenericoCuentaBancaria'";
            string vSqlUpdatesCodigoFormasDeCobroModificadas = SqlUpdatesCodigoFormasDeCobroModificadas();
            DataSet vDsUpdates = insTrn.ExecuteDataset(vSqlUpdatesCodigoFormasDeCobroModificadas, 0);
            DataSet vDsCia = insTrn.ExecuteDataset(vSqlCompanias, 0);
            if (vDsCia != null && vDsCia.Tables != null && vDsCia.Tables.Count > 0 && vDsCia.Tables[0] != null && LibDataTable.DataTableHasRows(vDsCia.Tables[0])) {
                if (vDsUpdates != null && vDsUpdates.Tables != null && vDsUpdates.Tables.Count > 0 && vDsUpdates.Tables[0] != null && LibDataTable.DataTableHasRows(vDsUpdates.Tables[0])) {
                    foreach (DataRow vRow in vDsCia.Tables[0].Rows) {
                        int vConsecutivoCompania = LibConvert.ToInt(vRow["ConsecutivoCompania"].ToString());
                        string vCodigoCtaBancaria = LibConvert.ToStr(vRow["CtaBancaria"].ToString());
                        string vSql = SqlInsertarFormasDeCobroModificadas(vConsecutivoCompania, vCodigoCtaBancaria);
                        LibBusiness.ExecuteUpdateOrDelete(vSql, new StringBuilder(), "", 0);
                    }
                    foreach (DataRow vRow in vDsUpdates.Tables[0].Rows) {
                        string vSql = LibConvert.ToStr(vRow["UpdateCambioCodigo"].ToString());
                        LibBusiness.ExecuteUpdateOrDelete(vSql, new StringBuilder(), "", 0);
                    }
                }
            }
        }
// Solo para las formas de Cobro que fueron Modificadas e Ingresadas se Agrega temporalmente Codigo de Cuenta según parametro "CodigoGenericoCuentaBancaria" y Moneda "VED"
        string SqlInsertarFormasDeCobroModificadas(int valConsecutivoCompania, string valCodigoCuentaBancaria) {
            StringBuilder vSql = new StringBuilder();
            vSql.AppendLine(";WITH ");
            vSql.AppendLine("CTE_FormasDelCobroModificadas AS (");
            vSql.AppendLine("SELECT ROW_NUMBER() OVER(ORDER BY TablaNueva.Nombre ASC) + (SELECT DISTINCT COUNT(*) FROM Adm.FormaDelCobro WHERE ConsecutivoCompania = " + InsSql.ToSqlValue(valConsecutivoCompania) + " GROUP BY ConsecutivoCompania) ConsecutivoNuevo,");
            vSql.AppendLine("    TablaVieja.Nombre, TablaVieja.TipoDePago");
            vSql.AppendLine("FROM Saw.FormaDelCobro AS TablaVieja LEFT JOIN Adm.FormaDelCobro AS TablaNueva ");
            vSql.AppendLine("    ON TablaVieja.Nombre = TablaNueva.Nombre AND TablaNueva.ConsecutivoCompania = " + InsSql.ToSqlValue(valConsecutivoCompania));
            vSql.AppendLine("WHERE TablaNueva.Nombre IS NULL ");
            vSql.AppendLine(")");
            vSql.AppendLine("INSERT INTO Adm.FormaDelCobro (ConsecutivoCompania, Consecutivo, Codigo, Nombre, TipoDePago, CodigoCuentaBancaria, CodigoMoneda, CodigoTheFactory, Origen)");
            vSql.AppendLine("SELECT");
            vSql.AppendLine("    " + InsSql.ToSqlValue(valConsecutivoCompania));
            vSql.AppendLine("    , ConsecutivoNuevo,");
            vSql.AppendLine("    'Z' + RIGHT('0000' + CAST(CTE_FormasDelCobroModificadas.ConsecutivoNuevo as varchar), 4),");
            vSql.AppendLine("    Nombre, TipoDePago,");
            vSql.AppendLine("    " + InsSql.ToSqlValue(valCodigoCuentaBancaria));
            vSql.AppendLine("    ,'VED','01', '1'");
            vSql.AppendLine("FROM CTE_FormasDelCobroModificadas ");

            return vSql.ToString();
        }

        string SqlInsertarFormasDeCobroInsertadas(int valConsecutivoCompania, string valCodigoCuentaBancaria) {
            StringBuilder vSql = new StringBuilder();
            vSql.AppendLine(";WITH ");
            vSql.AppendLine("CTE_FormasDelCobroInsertadas AS (");
            vSql.AppendLine("SELECT ROW_NUMBER() OVER(ORDER BY TablaNueva.Nombre ASC) + (SELECT DISTINCT COUNT(*) FROM Adm.FormaDelCobro WHERE ConsecutivoCompania = " + InsSql.ToSqlValue(valConsecutivoCompania) + " GROUP BY ConsecutivoCompania) ConsecutivoNuevo,");
            vSql.AppendLine("    TablaVieja.Nombre, TablaVieja.TipoDePago");
            vSql.AppendLine("FROM Saw.FormaDelCobro AS TablaVieja LEFT JOIN Adm.FormaDelCobro AS TablaNueva ");
            vSql.AppendLine("    ON TablaVieja.Codigo = TablaNueva.Codigo ");
            vSql.AppendLine("WHERE TablaNueva.Codigo IS NULL ");
            vSql.AppendLine(")");
            vSql.AppendLine("INSERT INTO Adm.FormaDelCobro (ConsecutivoCompania, Consecutivo, Codigo, Nombre, TipoDePago, CodigoCuentaBancaria, CodigoMoneda, CodigoTheFactory, Origen)");
            vSql.AppendLine("SELECT");
            vSql.AppendLine("    " + InsSql.ToSqlValue(valConsecutivoCompania));
            vSql.AppendLine("    , ConsecutivoNuevo,");
            vSql.AppendLine("    'Z' + RIGHT('0000' + CAST(CTE_FormasDelCobroInsertadas.ConsecutivoNuevo as varchar), 4),");
            vSql.AppendLine("    Nombre, TipoDePago,");
            vSql.AppendLine("    " + InsSql.ToSqlValue(valCodigoCuentaBancaria));
            vSql.AppendLine("    ,'VED','01', '1'");
            vSql.AppendLine("FROM CTE_FormasDelCobroInsertadas ");
            vSql.AppendLine();
            vSql.AppendLine();
            return vSql.ToString();
        }

        string SqlUpdatesCodigoFormasDeCobroModificadas() {
            StringBuilder vSql = new StringBuilder();
            vSql.AppendLine(";WITH ");
            vSql.AppendLine("CTE_FormasDelCobro AS (");
            vSql.AppendLine("SELECT ");
            vSql.AppendLine("    ROW_NUMBER() OVER(ORDER BY TablaNueva.Nombre ASC) + (SELECT DISTINCT COUNT(*) FROM Adm.FormaDelCobro GROUP BY ConsecutivoCompania) ConsecutivoNuevo, ");
            vSql.AppendLine("    TablaVieja.Codigo AS CodigoActual");
            vSql.AppendLine("FROM Saw.FormaDelCobro AS TablaVieja LEFT JOIN Adm.FormaDelCobro AS TablaNueva ");
            vSql.AppendLine("    ON TablaVieja.Nombre = TablaNueva.Nombre ");
            vSql.AppendLine("WHERE TablaNueva.Nombre IS NULL ");
            vSql.AppendLine(")");
            vSql.AppendLine("SELECT 'UPDATE renglonCobroDeFactura SET CodigoFormaDelCobro = ''' + 'Z' + RIGHT('0000' + CAST(CTE_FormasDelCobro.ConsecutivoNuevo as varchar), 4) + ''' WHERE CodigoFormaDelCobro = ''' + CTE_FormasDelCobro.CodigoActual + '''' AS UpdateCambioCodigo");
            vSql.AppendLine("FROM CTE_FormasDelCobro");
            return vSql.ToString();
        }

        string SqlUpdatesCodigoFormasDeCobroInsertadas() {
            StringBuilder vSql = new StringBuilder();
            vSql.AppendLine(";WITH ");
            vSql.AppendLine("CTE_FormasDelCobro AS (");
            vSql.AppendLine("SELECT ");
            vSql.AppendLine("    ROW_NUMBER() OVER(ORDER BY TablaNueva.Nombre ASC) + (SELECT DISTINCT COUNT(*) FROM Adm.FormaDelCobro GROUP BY ConsecutivoCompania) ConsecutivoNuevo, ");
            vSql.AppendLine("    TablaVieja.Codigo AS CodigoActual");
            vSql.AppendLine("FROM Saw.FormaDelCobro AS TablaVieja LEFT JOIN Adm.FormaDelCobro AS TablaNueva ");
            vSql.AppendLine("    ON TablaVieja.Codigo = TablaNueva.Codigo ");
            vSql.AppendLine("WHERE TablaNueva.Codigo IS NULL ");
            vSql.AppendLine(")");
            vSql.AppendLine("SELECT 'UPDATE renglonCobroDeFactura SET CodigoFormaDelCobro = ''' + 'Z' + RIGHT('0000' + CAST(CTE_FormasDelCobro.ConsecutivoNuevo as varchar), 4) + ''' WHERE CodigoFormaDelCobro = ''' + CTE_FormasDelCobro.CodigoActual + '''' AS UpdateCambioCodigo");
            vSql.AppendLine("FROM CTE_FormasDelCobro");
            return vSql.ToString();
        }

        private void CrearColumnaConsecutivoFormaDelCobro() {
            if (AddColumnInteger("dbo.renglonCobroDeFactura", "ConsecutivoFormaDelCobro", " CONSTRAINT d_RenCobDeFacCoFoDeCo NOT NULL", 0)) {
                AddNotNullConstraint("dbo.renglonCobroDeFactura", "ConsecutivoFormaDelCobro", InsSql.NumericTypeForDb(10, 0));
            }
        }

        private void ActualizarConsecutivoFormaDelCobro() {
            StringBuilder vSQL = new StringBuilder();
            LibDataScope vDb = new LibDataScope();
            vSQL.AppendLine("UPDATE dbo.renglonCobroDeFactura SET dbo.renglonCobroDeFactura.ConsecutivoFormaDelCobro = Adm.FormaDelCobro.Consecutivo ");
            vSQL.AppendLine("FROM dbo.renglonCobroDeFactura");
            vSQL.AppendLine("INNER JOIN Adm.FormaDelCobro ON dbo.renglonCobroDeFactura.ConsecutivoCompania = Adm.FormaDelCobro.ConsecutivoCompania ");
            vSQL.AppendLine("AND dbo.renglonCobroDeFactura.CodigoFormaDelCobro = Adm.FormaDelCobro.Codigo");
            vDb.ExecuteWithScope(vSQL.ToString());
        }

        void InsertDefaultRecord() {
            InsertFormaDelCobroPorDefecto(1, "00001", "Efectivo", eFormaDeCobro.Efectivo, "VED", CodigosTheFactory(eFormaDeCobro.Efectivo));
            InsertFormaDelCobroPorDefecto(2, "00002", "Tarjeta", eFormaDeCobro.Tarjeta, "VED", CodigosTheFactory(eFormaDeCobro.Tarjeta));
            InsertFormaDelCobroPorDefecto(3, "00003", "Cheque", eFormaDeCobro.Cheque, "VED", CodigosTheFactory(eFormaDeCobro.Cheque));
            InsertFormaDelCobroPorDefecto(4, "00004", "Depósito", eFormaDeCobro.Deposito, "VED", CodigosTheFactory(eFormaDeCobro.Deposito));
            InsertFormaDelCobroPorDefecto(5, "00005", "Anticipo", eFormaDeCobro.Anticipo, "VED", CodigosTheFactory(eFormaDeCobro.Anticipo));
            InsertFormaDelCobroPorDefecto(6, "00006", "Transferencia", eFormaDeCobro.Transferencia, "VED", CodigosTheFactory(eFormaDeCobro.Transferencia));
            InsertFormaDelCobroPorDefecto(7, "00007", "Vuelto Efectivo", eFormaDeCobro.VueltoEfectivo, "VED", CodigosTheFactory(eFormaDeCobro.VueltoEfectivo));
            InsertFormaDelCobroPorDefecto(8, "00008", "Vuelto Pago Móvil", eFormaDeCobro.VueltoPM, "VED", CodigosTheFactory(eFormaDeCobro.VueltoPM));
            InsertFormaDelCobroPorDefecto(9, "00009", "Tarjeta Medios Electrónicos", eFormaDeCobro.TarjetaMS, "VED", CodigosTheFactory(eFormaDeCobro.TarjetaMS));
            InsertFormaDelCobroPorDefecto(10, "00010", "Zelle", eFormaDeCobro.Zelle, "USD", CodigosTheFactory(eFormaDeCobro.Zelle));
            InsertFormaDelCobroPorDefecto(11, "00011", "Pago Móvil P2C", eFormaDeCobro.PagoMovil, "VED", CodigosTheFactory(eFormaDeCobro.PagoMovil));
            InsertFormaDelCobroPorDefecto(12, "00012", "Transferencia Medios Electrónicos", eFormaDeCobro.TransferenciaMS, "VED", CodigosTheFactory(eFormaDeCobro.TransferenciaMS));
            InsertFormaDelCobroPorDefecto(13, "00013", "Pago Móvil C2P", eFormaDeCobro.C2P, "VED", CodigosTheFactory(eFormaDeCobro.C2P));
            InsertFormaDelCobroPorDefecto(14, "00014", "Depósito Medios Electrónicos", eFormaDeCobro.DepositoMS, "VED", CodigosTheFactory(eFormaDeCobro.DepositoMS));
            InsertFormaDelCobroPorDefecto(15, "00015", "Crédito Electrónico", eFormaDeCobro.CreditoElectronico, "USD", CodigosTheFactory(eFormaDeCobro.CreditoElectronico));
            InsertFormaDelCobroPorDefecto(16, "00016", "Tarjeta de Crédito", eFormaDeCobro.TarjetadeCredito, "VED", CodigosTheFactory(eFormaDeCobro.TarjetadeCredito));
            InsertFormaDelCobroPorDefecto(17, "00017", "Tarjeta de Débito", eFormaDeCobro.TarjetadeDebito, "VED", CodigosTheFactory(eFormaDeCobro.TarjetadeDebito));
            InsertFormaDelCobroPorDefecto(18, "00018", "Efectivo Divisas", eFormaDeCobro.EfectivoDivisas, "USD", CodigosTheFactory(eFormaDeCobro.EfectivoDivisas));
            InsertFormaDelCobroPorDefecto(19, "00019", "Transferencia Divisas", eFormaDeCobro.TransferenciaDivisas, "USD", CodigosTheFactory(eFormaDeCobro.TransferenciaDivisas));
            InsertFormaDelCobroPorDefecto(20, "00020", "Vuelto Efectivo Divisas", eFormaDeCobro.VueltoEfectivoDivisas, "USD", CodigosTheFactory(eFormaDeCobro.VueltoEfectivoDivisas));
        }

        private string CodigosTheFactory(eFormaDeCobro valFormaDelCobro) {
            string vResult = "01";
            switch (valFormaDelCobro) {
                case eFormaDeCobro.Cheque:
                    vResult = "07";
                    break;
                case eFormaDeCobro.Tarjeta:
                case eFormaDeCobro.TarjetadeCredito:
                case eFormaDeCobro.TarjetadeDebito:
                case eFormaDeCobro.TarjetaMS:
                    vResult = "13";
                    break;
                case eFormaDeCobro.Transferencia:
                case eFormaDeCobro.TransferenciaMS:
                case eFormaDeCobro.TransferenciaDivisas:
                    vResult = "06";
                    break;
                case eFormaDeCobro.CreditoElectronico:
                    vResult = "15";
                    break;
            }

            return vResult;
        }

        void InsertFormaDelCobroPorDefecto(int valConsecutivo, string valCodigo, string valNombre, eFormaDeCobro valFormaDelCobro, string valCodigoMoneda, string valCodigoTheFactory) {
            StringBuilder vSQL = new StringBuilder();
            vSQL.AppendLine(";WITH CTE_SettValueCtaBancaria AS (SELECT ConsecutivoCompania, Value AS CtaBancaria FROM Comun.SettValueByCompany WHERE NameSettDefinition = 'CodigoGenericoCuentaBancaria')");
            vSQL.AppendLine("INSERT INTO Adm.FormaDelCobro (ConsecutivoCompania, Consecutivo, Codigo,Nombre, TipoDePago, CodigoCuentaBancaria,CodigoMoneda ,CodigoTheFactory,Origen)");
            vSQL.AppendLine("SELECT ConsecutivoCompania, " + InsSql.ToSqlValue(valConsecutivo) + " , " + InsSql.ToSqlValue(valCodigo) + " , " + InsSql.ToSqlValue(valNombre) + ", ");
            vSQL.AppendLine(InsSql.EnumToSqlValue((int)valFormaDelCobro) + ", " + InsSql.ToSqlValue(string.Empty) + " , " + InsSql.ToSqlValue(valCodigoMoneda) + ", " + InsSql.ToSqlValue(valCodigoTheFactory));
            vSQL.AppendLine(", " + InsSql.EnumToSqlValue((int)eOrigen.Sistema) + " FROM CTE_SettValueCtaBancaria");
            LibBusiness.ExecuteUpdateOrDelete(vSQL.ToString(), new StringBuilder(), string.Empty, 0);
        }

        private void CrearCamposIDEnCxP() {
            if (!ColumnExists("CxP", "NumeroControlRetencionIvaImpDigital")) {
                if (AddColumnString("CxP", "NumeroControlRetencionIvaImpDigital", 20, "", "")) {
                    AddDefaultConstraint("CxP", "nCtID", _insSql.ToSqlValue(""), "NumeroControlRetencionIvaImpDigital");
                }
            }
            if (!ColumnExists("CxP", "ProveedorImprentaDigital")) {
                if (AddColumnEnumerative("CxP", "ProveedorImprentaDigital", "", 0)) {
                    AddDefaultConstraint("CxP", "pRovID", _insSql.ToSqlValue("0"), "ProveedorImprentaDigital");
                }
            }
            if (!ColumnExists("CxP", "RetencionIvaEnviadaImpDigital")) {
                if (AddColumnBoolean("CxP", "RetencionIvaEnviadaImpDigital", "", false)) {
                    AddDefaultConstraint("CxP", "rTEnID", _insSql.ToSqlValue("N"), "RetencionIvaEnviadaImpDigital");
                }
            }
        }
		
		private void QuitarUniqueCaja() {			
			DeleteAllrelationShipsBetweenTables(_CurrentDataBaseName, "Adm.Caja", "Adm.CajaApertura");
			DeleteAllrelationShipsBetweenTables(_CurrentDataBaseName, "Adm.Caja", "factura");
			ExecuteDropConstraint("Adm.Caja", "u_CajConsecutivo", true);
			AddForeignKey("Adm.Caja", "Adm.CajaApertura", new string[] { "ConsecutivoCompania,Consecutivo" }, new string[] { "ConsecutivoCompania,ConsecutivoCaja" }, false, true);
			AddForeignKey("Adm.Caja", "factura", new string[] { "ConsecutivoCompania,Consecutivo" }, new string[] { "ConsecutivoCompania,ConsecutivoCaja" }, false, true);
		}
		
    }
}
