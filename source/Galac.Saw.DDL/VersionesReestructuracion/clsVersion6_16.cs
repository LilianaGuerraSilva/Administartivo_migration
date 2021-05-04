using LibGalac.Aos.Base;
using Galac.Saw.Ccl.SttDef;
using System.Threading;
using System.Text;
using LibGalac.Aos.Brl;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Linq;
using LibGalac.Aos.Dal;

namespace Galac.Saw.DDL.VersionesReestructuracion {
    class clsVersion6_16:clsVersionARestructurar {

        public clsVersion6_16(string valCurrentDataBaseName) : base(valCurrentDataBaseName) {
            _VersionDataBase = "6.16";
        }

        public override bool UpdateToVersion() {
            StartConnectionNoTransaction();
            CrearParametrosCXPImagenesComprobantesDeRetencion();
            CambiarColumnaCuentaSaldoInicialMoneyADecimal();            
            DisposeConnectionNoTransaction();
            return true;
        }

        private void CrearParametrosCXPImagenesComprobantesDeRetencion() {
            AgregarNuevoParametro("NombreLogo","CxP/Compras",6,"6.6.- Imágenes para Comprobantes",6,"",eTipoDeDatoParametros.String,"",'N',"");
            AgregarNuevoParametro("NombreSello","CxP/Compras",6,"6.6.- Imágenes para Comprobantes",6,"",eTipoDeDatoParametros.String,"",'N',"");
            AgregarNuevoParametro("NombreFirma","CxP/Compras",6,"6.6.- Imágenes para Comprobantes",6,"",eTipoDeDatoParametros.String,"",'N',"");
        }

        private void CambiarColumnaCuentaSaldoInicialMoneyADecimal() {

            AlterColumnIfExist("Cuenta","SaldoInicial",InsSql.DecimalTypeForDb(25,4),"","");

            if(ColumnExists("dbo.tblSaldoContableTemp","SaldoInicial")) {
                ExecuteRemoveNotNullConstraint("dbo.tblSaldoContableTemp","SaldoInicial","[decimal](25, 4)");
            }
            if(ColumnExists("dbo.tblSaldoContableTemp","SubTotalDebe")) {
                ExecuteRemoveNotNullConstraint("dbo.tblSaldoContableTemp","SubTotalDebe","[decimal](25,4)");
            }
            if(ColumnExists("dbo.tblSaldoContableTemp","SubTotalHaber")) {
                ExecuteRemoveNotNullConstraint("dbo.tblSaldoContableTemp","SubTotalHaber","[decimal](25,4)");
            }
            if(ColumnExists("dbo.tblSaldoContableTemp","Saldo")) {
                ExecuteRemoveNotNullConstraint("dbo.tblSaldoContableTemp","Saldo","[decimal](25,4)");
            }
            if(ColumnExists("dbo.tblSaldoContableTemp","SaldoAntesDeCambioSigno")) {
                ExecuteRemoveNotNullConstraint("dbo.tblSaldoContableTemp","SaldoAntesDeCambioSigno","[decimal](25,4)");
            }
            if(ColumnExists("dbo.tblMovimientoContableTemp","SubTotalDebe")) {
                ExecuteRemoveNotNullConstraint("dbo.tblMovimientoContableTemp","SubTotalDebe","[decimal](25,4)");
            }
            if(ColumnExists("dbo.tblMovimientoContableTemp","SubTotalHaber")) {
                ExecuteRemoveNotNullConstraint("dbo.tblMovimientoContableTemp","SubTotalHaber","[decimal](25,4)");
            }
            if(ColumnExists("dbo.tblMovimientoContableTemp","SubTotalSaldoInicial")) {
                ExecuteRemoveNotNullConstraint("dbo.tblMovimientoContableTemp","SubTotalSaldoInicial","[decimal](25,4)");
            }
            if(ColumnExists("dbo.tblMovAuxiliarCentroDeCostoTemp","SubTotalDebe")) {
                ExecuteRemoveNotNullConstraint("dbo.tblMovAuxiliarCentroDeCostoTemp","SubTotalDebe","[decimal](25,4)");
            }
            if(ColumnExists("dbo.tblMovAuxiliarCentroDeCostoTemp","SubTotalHaber")) {
                ExecuteRemoveNotNullConstraint("dbo.tblMovAuxiliarCentroDeCostoTemp","SubTotalHaber","[decimal](25,4)");
            }
            if(ColumnExists("dbo.CUENTA","SaldoInicial")) {
                ExecuteRemoveNotNullConstraint("dbo.CUENTA","SaldoInicial","[decimal](25,4)");
            }
        }        

        private void ExecuteRemoveNotNullConstraint(string valTableName,string valColumnName,string valColumnType) {
            string vSQL;
            vSQL = "ALTER TABLE " + LibDbo.ToFullDboName(valTableName) + " ALTER COLUMN " + valColumnName + " " + valColumnType + " NULL";
            Execute(vSQL,0);
        }
    }
}

