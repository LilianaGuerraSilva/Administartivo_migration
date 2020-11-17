using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Dal;
using LibGalac.Aos.Base;
using LibGalac.Aos.Dal.Usal;
using LibGalac.Aos.Base.Dal;
using Galac.Comun.Ccl.TablasLey;

namespace Galac.Saw.DDL.VersionesReestructuracion {
    class clsVersion5_69 : clsVersionARestructurar {

      public clsVersion5_69(string valCurrentDataBaseName)
         : base(valCurrentDataBaseName) {
         _VersionDataBase = "5.69";
      }

      public override bool UpdateToVersion() {
         StartConnectionNoTransaction();
         AgregarParametrosVentasDiferidas();
         AgregarCamposFacturasVentaDiferida();
         AgregarCamposDocumentoCobradoVentaDiferida();
         AgregarCamposReglasDeContabilizacion();
         FillNullValuesFactura();
         FillNullValuesDocumentoCobrado();
         CambiaLongitudDeCampoEnTablaCliente();
         AgregarPermisosVentasDiferidas();
         CambiarCamposVarcharToEnumAnticipo();
         CambiarCamposVarcharToEnumCobranza();
         DisposeConnectionNoTransaction();
         return true;
      }

      private void AgregarParametrosVentasDiferidas()
      {
         AgregarNuevoParametro("UsarVentasConIvaDiferido", "DatosGenerales", 1, "1.1.-Compania", 1, "", '2', "", 'N', "N");
         AgregarNuevoParametro("ImprimirVentasDiferidas", "DatosGenerales", 1, "1.1.-Compania", 1, "", '2', "", 'N', "N");         
      }

      private void AgregarCamposFacturasVentaDiferida()
      {         
         if (!ColumnExists("factura", "EsDiferida"))
         {            
            AddColumnBoolean("factura", "EsDiferida", "EsDiferida NOT NULL", false);
         }
         if (!ColumnExists("factura", "EsOriginalmenteDiferida"))
         {            
            AddColumnBoolean("factura", "EsOriginalmenteDiferida", "EsOriginalmenteDiferida NOT NULL", false);
         }
         if (!ColumnExists("factura", "SeContabilizoIvaDiferido"))
         {            
            AddColumnBoolean("factura", "SeContabilizoIvaDiferido", "SeContabilizoIvaDiferido NOT NULL", false);
         }

      }

      private void AgregarCamposReglasDeContabilizacion()
      {
         if (!ColumnExists("Saw.ReglasDeContabilizacion", "CuentaFacturacionIvaDiferido"))
         {
            AddColumnString("Saw.ReglasDeContabilizacion", "CuentaFacturacionIvaDiferido", 30, "", "");
         }
         if (!ColumnExists("Saw.ReglasDeContabilizacion", "CuentaCobranzaIvaDiferido"))
         {
            AddColumnString("Saw.ReglasDeContabilizacion", "CuentaCobranzaIvaDiferido", 30, "", "");
         }
      }

      private void FillNullValuesFactura()
      {
         StringBuilder vSql = new StringBuilder();
         vSql.AppendLine("UPDATE dbo.Factura ");
         vSql.AppendLine("SET dbo.Factura.EsDiferida =  'N' ");
         vSql.AppendLine(" WHERE dbo.Factura.EsDiferida IS NULL OR dbo.Factura.EsDiferida = ''");
         Execute(vSql.ToString(), -1);
         vSql.Clear();
         vSql.AppendLine("UPDATE dbo.Factura ");
         vSql.AppendLine("SET dbo.Factura.EsOriginalmenteDiferida =  'N' ");
         vSql.AppendLine(" WHERE dbo.Factura.EsOriginalmenteDiferida IS NULL OR dbo.Factura.EsOriginalmenteDiferida = ''");
         Execute(vSql.ToString(), -1);
         vSql.Clear();
         vSql.AppendLine("UPDATE dbo.Factura ");
         vSql.AppendLine("SET dbo.Factura.SeContabilizoIvaDiferido =  'N' ");
         vSql.AppendLine(" WHERE dbo.Factura.SeContabilizoIvaDiferido IS NULL OR dbo.Factura.SeContabilizoIvaDiferido = ''");
         Execute(vSql.ToString(), -1);
      }

      private void AgregarCamposDocumentoCobradoVentaDiferida()
      {
         if (!ColumnExists("DocumentoCobrado", "SeContabilizoIvaDiferido"))
         {
            AddColumnBoolean("DocumentoCobrado", "SeContabilizoIvaDiferido", "nnDocumentoCobradoSeContabil NOT NULL", false);
         }

      }

      private void FillNullValuesDocumentoCobrado()
      {
         StringBuilder vSql = new StringBuilder();
         vSql.AppendLine("UPDATE dbo.DocumentoCobrado ");
         vSql.AppendLine("SET dbo.DocumentoCobrado.SeContabilizoIvaDiferido =  'N' ");
         vSql.AppendLine(" WHERE dbo.DocumentoCobrado.SeContabilizoIvaDiferido IS NULL OR dbo.DocumentoCobrado.SeContabilizoIvaDiferido = ''");
         Execute(vSql.ToString(), -1);
      }

      private void CambiaLongitudDeCampoEnTablaCliente()
      {
         if (TableExists("dbo.Cliente") && ColumnExists("dbo.Cliente", "Nombre"))
         {
            if (TableExists("dbo.participante")) {
                 if (ForeignKeyNameExists ("fk_Participantee")) {
                     DeleteAllrelationShipsBetweenTables(_CurrentDataBaseName, "dbo.Cliente", "dbo.participante");
                 }
            }
            ModifyLengthOfColumnString("dbo.Cliente", "Nombre", 160, "");
            if (!ForeignKeyNameExists("fk_Participantee") && TableExists("dbo.participante")) { 
                ModifyLengthOfColumnString("dbo.participante", "Cliente", 160, "");
                AddForeignKey("dbo.Cliente", "dbo.participante",new string[] { "ConsecutivoCompania", "Nombre" }, new string[] { "ConsecutivoCompania", "Cliente" }, false);
            }
         }
      }

      private void AgregarPermisosVentasDiferidas() 
      {
         LibGUserReestScripts SqlSecurityLevel = new LibGUserReestScripts();         
         StringBuilder vSql = new StringBuilder();
         List<string> vActions = new List<string>();
         System.Collections.Hashtable vFiltros = new System.Collections.Hashtable();
         vActions.Add("Insertar Venta con Débito Fiscal Diferido");
         vFiltros.Add("Insertar", true);
         vSql.Append(SqlSecurityLevel.SqlAddSecurityLevel("Factura", vActions, "Principal", 1, "SAW", vFiltros));         
         Execute(vSql.ToString(), -1);
      }

      private void CambiarCamposVarcharToEnumAnticipo()
      {
         StringBuilder vSql = new StringBuilder();
         if (ColumnExists("dbo.Anticipo", "GeneradoPor"))
         {
            if (!ColumnExists("dbo.Anticipo", "AuxGeneradoPor"))
            {
               AddColumnEnumerative("dbo.Anticipo", "AuxGeneradoPor", "", "");
               vSql.Clear();
               vSql.AppendLine("UPDATE dbo.Anticipo ");
               vSql.AppendLine("SET dbo.Anticipo.AuxGeneradoPor = dbo.Anticipo.GeneradoPor");
               Execute(vSql.ToString(), -1);
               DropColumnIfExist("dbo.Anticipo", "GeneradoPor");
               AddColumnEnumerative("dbo.Anticipo", "GeneradoPor", "", "");
               vSql.Clear();
               vSql.AppendLine("UPDATE dbo.Anticipo ");
               vSql.AppendLine("SET dbo.Anticipo.GeneradoPor = dbo.Anticipo.AuxGeneradoPor");
               Execute(vSql.ToString(), -1);
               DropColumnIfExist("dbo.Anticipo", "AuxGeneradoPor");
            }
            else
            {
               vSql.Clear();
               vSql.AppendLine("SELECT * FROM dbo.Anticipo WHERE AuxGeneradoPor IS NOT NULL OR AuxGeneradoPor <> ''");
               if (RecordCountOfSql(vSql.ToString()) > 0)
               {
                  DropColumnIfExist("dbo.Anticipo", "AuxGeneradoPor");
                  AddColumnEnumerative("dbo.Anticipo", "AuxGeneradoPor", "", "");
                  vSql.Clear();
                  vSql.AppendLine("UPDATE dbo.Anticipo ");
                  vSql.AppendLine("SET dbo.Anticipo.AuxGeneradoPor = dbo.Anticipo.GeneradoPor");
                  Execute(vSql.ToString(), -1);
                  DropColumnIfExist("dbo.Anticipo", "GeneradoPor");
                  AddColumnEnumerative("dbo.Anticipo", "GeneradoPor", "", "");
                  vSql.Clear();
                  vSql.AppendLine("UPDATE dbo.Anticipo ");
                  vSql.AppendLine("SET dbo.Anticipo.GeneradoPor = dbo.Anticipo.AuxGeneradoPor");
                  Execute(vSql.ToString(), -1);
                  DropColumnIfExist("dbo.Anticipo", "AuxGeneradoPor");
               }
               else
               {
                  DropColumnIfExist("dbo.Anticipo", "GeneradoPor");
                  AddColumnEnumerative("dbo.Anticipo", "GeneradoPor", "", "");
                  vSql.Clear();
                  vSql.AppendLine("UPDATE dbo.Anticipo ");
                  vSql.AppendLine("SET dbo.Anticipo.GeneradoPor = dbo.Anticipo.AuxGeneradoPor");
                  Execute(vSql.ToString(), -1);
                  DropColumnIfExist("dbo.Anticipo", "AuxGeneradoPor");
               }

            }
         }
      }

      private void CambiarCamposVarcharToEnumCobranza()
      {
         StringBuilder vSql = new StringBuilder();
         if (ColumnExists("dbo.Cobranza", "TipoDeDocumento"))
         {            
            if (!ColumnExists("dbo.Cobranza", "AuxTipoDeDocumento"))
            {
               AddColumnEnumerative("dbo.Cobranza", "AuxTipoDeDocumento", "", "");
               vSql.Clear();
               vSql.AppendLine("UPDATE dbo.Cobranza ");
               vSql.AppendLine("SET dbo.Cobranza.AuxTipoDeDocumento = dbo.Cobranza.TipoDeDocumento");
               Execute(vSql.ToString(), -1);
               DropColumnIfExist("dbo.Cobranza", "TipoDeDocumento");
               AddColumnEnumerative("dbo.Cobranza", "TipoDeDocumento", "", "");
               vSql.Clear();
               vSql.AppendLine("UPDATE dbo.Cobranza ");
               vSql.AppendLine("SET dbo.Cobranza.TipoDeDocumento = dbo.Cobranza.AuxTipoDeDocumento");
               Execute(vSql.ToString(), -1);
               DropColumnIfExist("dbo.Cobranza", "AuxTipoDeDocumento");
            }
            else
            {
               vSql.Clear();
               vSql.AppendLine("SELECT * FROM dbo.cobranza WHERE AuxTipoDeDocumento IS NOT NULL OR AuxTipoDeDocumento <> ''");
               if (RecordCountOfSql(vSql.ToString()) > 0)
               {
                  DropColumnIfExist("dbo.Cobranza", "AuxTipoDeDocumento");
                  AddColumnEnumerative("dbo.Cobranza", "AuxTipoDeDocumento", "", "");
                  vSql.Clear();
                  vSql.AppendLine("UPDATE dbo.Cobranza ");
                  vSql.AppendLine("SET dbo.Cobranza.AuxTipoDeDocumento = dbo.Cobranza.TipoDeDocumento");
                  Execute(vSql.ToString(), -1);
                  DropColumnIfExist("dbo.Cobranza", "TipoDeDocumento");
                  AddColumnEnumerative("dbo.Cobranza", "TipoDeDocumento", "", "");
                  vSql.Clear();
                  vSql.AppendLine("UPDATE dbo.Cobranza ");
                  vSql.AppendLine("SET dbo.Cobranza.TipoDeDocumento = dbo.Cobranza.AuxTipoDeDocumento");
                  Execute(vSql.ToString(), -1);
                  DropColumnIfExist("dbo.Cobranza", "AuxTipoDeDocumento");
               }
               else 
               {
                  DropColumnIfExist("dbo.Cobranza", "TipoDeDocumento");
                  AddColumnEnumerative("dbo.Cobranza", "TipoDeDocumento", "", "");
                  vSql.Clear();
                  vSql.AppendLine("UPDATE dbo.Cobranza ");
                  vSql.AppendLine("SET dbo.Cobranza.TipoDeDocumento = dbo.Cobranza.AuxTipoDeDocumento");
                  Execute(vSql.ToString(), -1);
                  DropColumnIfExist("dbo.Cobranza", "AuxTipoDeDocumento");
               }
               
            }            
         }
      }
    }
}
