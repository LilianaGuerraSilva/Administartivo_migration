using LibGalac.Aos.Base;
using Galac.Saw.Ccl.SttDef;
using System.Threading;
using System.Text;
using LibGalac.Aos.Brl;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Linq;
using LibGalac.Aos.Base.Dal;

namespace Galac.Saw.DDL.VersionesReestructuracion {
    class clsVersion6_51 : clsVersionARestructurar {

        public clsVersion6_51(string valCurrentDataBaseName) : base(valCurrentDataBaseName) {
            _VersionDataBase = "6.51";
        }

        public override bool UpdateToVersion() {
            StartConnectionNoTransaction();
            ActualizarNroComprobanteFiscalEnBlanco();
            DisposeConnectionNoTransaction();
            return true;
        }

        private void ActualizarNroComprobanteFiscalEnBlanco() {
            StringBuilder vSql = new StringBuilder();
            vSql.AppendLine("UPDATE FACTURA ");
            vSql.AppendLine("SET NumeroComprobanteFiscal = '0' ");
            vSql.AppendLine("WHERE(LEN(NumeroComprobanteFiscal) = 0 OR NumeroComprobanteFiscal = Null) ");
            vSql.AppendLine("AND LEN(NoCotizacionDeOrigen) > 0 ");
            Execute(vSql.ToString(), 0);
        }             
    }
}