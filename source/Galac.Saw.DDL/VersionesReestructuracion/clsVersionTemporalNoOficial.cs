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
using System.Data.Common;

namespace Galac.Saw.DDL.VersionesReestructuracion {

    class clsVersionTemporalNoOficial: clsVersionARestructurar {
        public clsVersionTemporalNoOficial(string valCurrentDataBaseName) : base(valCurrentDataBaseName) { }
        public override bool UpdateToVersion() {
            StartConnectionNoTransaction();
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
            ActivaMostratTotalEnDivisas();
            DisposeConnectionNoTransaction();
            return true;
        }

        private void CrearCampoManejaMerma() {
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

        private void CrearCampoManejaMermaOP() {
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

        private void AmpliarColumnaCompaniaImprentaDigitalClave() {
            ModifyLengthOfColumnString("Compania", "ImprentaDigitalClave", 1000, "");
        }

        private void AgregarReglaContabilizacionProduccionMermaAnormal() {
            if (AddColumnString("Saw.ReglasDeContabilizacion", "CuentaMermaAnormal", 30, "", "")) {
                AddDefaultConstraint("Saw.ReglasDeContabilizacion", "d_RegDeConCuMeAn", _insSql.ToSqlValue(""), "CuentaMermaAnormal");
            }
        }

        private void CxC() {
            AddColumnBoolean("CxC", "VieneDeCreditoElectronico", "CONSTRAINT nnCxCVieneDeCre NOT NULL", false);
        }

        private void FormaDelCobro() {
            StringBuilder vSQL = new StringBuilder();
            vSQL.AppendLine("SELECT Codigo FROM SAW.FormaDelCobro");
            if (RecordCountOfSql(vSQL.ToString()) <= 0) {
                string vNextCodigo = "";
                vNextCodigo = new LibGalac.Aos.Dal.LibDatabase("").NextStrConsecutive("SAW.FormaDelCobro", "Codigo", "", true, 5);
                vSQL.Clear();
                vSQL.AppendLine("INSERT INTO SAW.FormaDelCobro (Codigo, Nombre, TipoDePago) VALUES (" + InsSql.ToSqlValue(vNextCodigo) + ", " + InsSql.ToSqlValue("Crédito Electrónico") + ", " + _insSql.EnumToSqlValue((int)eTipoDeFormaDePago.CreditoElectronico) + ")");
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
            if (AddColumnDecimal("Adm.CajaApertura", "MontoCreditoElectronico", 25, 4, "", 0)) {
                AddDefaultConstraint("Adm.CajaApertura", "d_CajApeMoCrEl", "0", "MontoCreditoElectronico");
            }
        }

        private void AgregaColumnasReglasDeContabilizacionCxCCreditoElectronico() {
            if (AddColumnString("Saw.ReglasDeContabilizacion", "CuentaFacturacionCxCCreditoElectronico", 30, "", "")) {
                AddDefaultConstraint("Saw.ReglasDeContabilizacion", "d_RegDeConCuFacCxCCreEle", _insSql.ToSqlValue(""), "CuentaFacturacionCxCCreditoElectronico");
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

        private void ActivaMostratTotalEnDivisas() {
            StringBuilder vSql = new StringBuilder();
            vSql.AppendLine("UPDATE Comun.SettvalueByCompany");
            vSql.AppendLine("SET VALUE ='S'");
            vSql.AppendLine("WHERE NameSettDefinition='SeMuestraTotalEnDivisas'");
            vSql.AppendLine("AND ConsecutivoCompania IN (SELECT ConsecutivoCompania FROM Comun.SettvalueByCompany WHERE NameSettDefinition='UsaCobroDirectoEnMultimoneda' AND Value ='S')");
            Execute(vSql.ToString());
        }
    }
}
