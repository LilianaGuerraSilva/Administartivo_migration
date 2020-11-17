using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Dal;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Dal;
using LibGalac.Aos.Dal.Usal;
using Galac.Comun.Ccl.TablasLey;

namespace Galac.Saw.DDL.VersionesReestructuracion {
    class clsVersion5_67 : clsVersionARestructurar {

        public clsVersion5_67(string valCurrentDataBaseName)
            : base(valCurrentDataBaseName) {
            _VersionDataBase = "5.67";
        }

        public override bool UpdateToVersion() {
            StartConnectionNoTransaction();
            CheckReimprimirMovimientoBancarioAccess();
            DisposeConnectionNoTransaction();
            return true;
        }

        private void CheckReimprimirMovimientoBancarioAccess()
        {
           LibGUserReestScripts SqlSecurityLevel = new LibGUserReestScripts();
           StringBuilder vSql = new StringBuilder();
           List<string> vActions = new List<string>();
           System.Collections.Hashtable vFiltros = new System.Collections.Hashtable();
           vActions.Add("Reimprimir Cheque");           
           vFiltros.Add("Anular", true);
           vSql.Append(SqlSecurityLevel.SqlAddSecurityLevel("Movimiento Bancario", vActions, "Bancos", 8, "SAW", vFiltros));
           Execute(vSql.ToString(), -1);
           
           vSql.Clear();
           vActions.Clear();
           vFiltros.Clear();           
           vActions.Add("Reimprimir");
           vFiltros.Add("Anular", true);
           vSql.Append(SqlSecurityLevel.SqlAddSecurityLevel("Movimiento Bancario", vActions, "Bancos", 8, "SAW", vFiltros));
           Execute(vSql.ToString(), -1);           
        }         
    }
}
