using System.Text;
using LibGalac.Aos.Dal;
using LibGalac.Aos.Dal.Contracts;

namespace Galac.Saw.Dal.Inventario {
    [LibMefDalComponentMetadata(typeof(clsExistenciaPorAlmacenDetLoteInvED))]
    public class clsExistenciaPorAlmacenDetLoteInvED: LibED, ILibMefDalComponent {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores
        public clsExistenciaPorAlmacenDetLoteInvED(): base(){
            DbSchema = "Dbo";
        }
        #endregion //Constructores
        #region Metodos Generados

        #region Miembros de ILibMefDalComponent
        string ILibMefDalComponent.DbSchema {
            get { return DbSchema; }
        }

        string ILibMefDalComponent.Name {
            get { return GetType().Name; }
        }

        string ILibMefDalComponent.Table {
            get { return "ExistenciaPorAlmacenDetLoteInv"; }
        }

        bool ILibMefDalComponent.InstallTable() {
            return InstalarTabla();
        }
        #endregion
        #region Queries

        private string SqlCreateTable() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine(InsSql.CreateTable("ExistenciaPorAlmacenDetLoteInv", DbSchema) + " ( ");
            SQL.AppendLine("ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnExiPorAlmDetLotInvConsecutiv NOT NULL, ");
            SQL.AppendLine("CosecutivoAlmacen" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnExiPorAlmDetLotInvCosecutivo NOT NULL, ");
            SQL.AppendLine("CodigoArticulo" + InsSql.VarCharTypeForDb(30) + " CONSTRAINT nnExiPorAlmDetLotInvCodigoArti NOT NULL, ");
            SQL.AppendLine("ConsecutivoLoteInventario" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnExiPorAlmDetLotInvConsecutiv NOT NULL, ");
            SQL.AppendLine("Cantidad" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT nnExiPorAlmDetLotInvCantidad NOT NULL, ");
            SQL.AppendLine("Ubicacion" + InsSql.VarCharTypeForDb(100) + " CONSTRAINT d_ExiPorAlmDetLotInvUb DEFAULT (''), ");
            SQL.AppendLine("fldTimeStamp" + InsSql.TimeStampTypeForDb() + ",");
            SQL.AppendLine("CONSTRAINT p_ExistenciaPorAlmacenDetLoteInv PRIMARY KEY CLUSTERED");
            SQL.AppendLine("(ConsecutivoCompania ASC, CosecutivoAlmacen ASC, CodigoArticulo ASC, ConsecutivoLoteInventario ASC)");
            SQL.AppendLine(",CONSTRAINT fk_ExistenciaPorAlmacenDetLoteInvExistenciaPorAlmacen FOREIGN KEY (ConsecutivoCompania, CosecutivoAlmacen, CodigoArticulo)");
            SQL.AppendLine("REFERENCES dbo.ExistenciaPorAlmacen(ConsecutivoCompania, CodigoAlmacen, CodigoArticulo)");
            SQL.AppendLine("ON DELETE CASCADE");
            SQL.AppendLine("ON UPDATE CASCADE");
            SQL.AppendLine(", CONSTRAINT fk_ExistenciaPorAlmacenDetLoteInvCompania FOREIGN KEY (ConsecutivoCompania)");
            SQL.AppendLine("REFERENCES Dbo.Compania(Consecutivo)");
            SQL.AppendLine("ON UPDATE CASCADE");
            SQL.AppendLine(", CONSTRAINT fk_ExistenciaPorAlmacenDetLoteInvExistenciaPorAlmacen FOREIGN KEY (ConsecutivoCompania, CosecutivoAlmacen)");
            SQL.AppendLine("REFERENCES dbo.ExistenciaPorAlmacen(ConsecutivoCompania, ConsecutivoAlmacen)");
            SQL.AppendLine("ON UPDATE CASCADE");
            SQL.AppendLine(", CONSTRAINT fk_ExistenciaPorAlmacenDetLoteInvExistenciaPorAlmacen FOREIGN KEY (ConsecutivoCompania, CodigoArticulo)");
            SQL.AppendLine("REFERENCES dbo.ExistenciaPorAlmacen(ConsecutivoCompania, CodigoArticulo)");
            SQL.AppendLine("ON UPDATE CASCADE");
            SQL.AppendLine(", CONSTRAINT fk_ExistenciaPorAlmacenDetLoteInvLoteDeInventario FOREIGN KEY (ConsecutivoCompania, ConsecutivoLoteInventario)");
            SQL.AppendLine("REFERENCES Saw.LoteDeInventario(ConsecutivoCompania, Consecutivo)");
            SQL.AppendLine("ON UPDATE CASCADE");
            SQL.AppendLine(")");
            return SQL.ToString();
        }

        #endregion //Queries

        bool CrearTabla() {
            bool vResult = insDbo.Create(DbSchema + ".ExistenciaPorAlmacenDetLoteInv", SqlCreateTable(), false, eDboType.Tabla);
            return vResult;
        }

        

        public bool InstalarTabla() {
            bool vResult = false;
            if (CrearTabla()) {
                vResult = true;
            }
            return vResult;
        }

        
        #endregion //Metodos Generados


    } //End of class clsExistenciaPorAlmacenDetLoteInvED

} //End of namespace Galac.Saw.Dal.Inventario

