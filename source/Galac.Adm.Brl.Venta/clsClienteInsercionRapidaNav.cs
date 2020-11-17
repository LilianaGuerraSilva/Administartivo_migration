using LibGalac.Aos.Base;
using LibGalac.Aos.Brl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Galac.Adm.Brl.Venta {
    public class clsClienteInsercionRapidaNav {
        public string ObtenerProvincia(string valCodigoUbigeo) {
            return ObtenerNombre("Provincia", valCodigoUbigeo, 4);
        }
        public string ObtenerDepartamento(string valCodigoUbigeo) {
            return ObtenerNombre("Departamento", valCodigoUbigeo, 2);
        }
        public string ObtenerDistrito(string valCodigoUbigeo) {
            return ObtenerNombre("Ubigeo", valCodigoUbigeo, 6);
        }
        private string ObtenerNombre(string valNombreTabla, string valCodigoUbigeo, int valLongitudCodigo) {
            string vCodigo = valCodigoUbigeo.Substring(0, valLongitudCodigo);
            LibGpParams vParams = new LibGpParams();
            vParams.AddInString("Codigo", vCodigo, valLongitudCodigo);
            StringBuilder vSql = new StringBuilder();
            vSql.AppendLine("SELECT Nombre FROM Comun." + valNombreTabla);
            vSql.AppendLine("WHERE Codigo = @Codigo");
            var vXmlResult = LibBusiness.ExecuteSelect(vSql.ToString(), vParams.Get(), "", 0);
            if (vXmlResult != null) {
                return vXmlResult.Descendants("Nombre").First().Value;
            }
            return string.Empty;
        }

    }
}
