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
using Galac.Adm.Ccl.CajaChica;

namespace Galac.Adm.Brl.CajaChica {
    public partial class clsDetalleDeRendicionNav: LibBaseNavDetail<IList<DetalleDeRendicion>, IList<DetalleDeRendicion>> {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores

        public clsDetalleDeRendicionNav() {
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override ILibDataDetailComponent<IList<DetalleDeRendicion>, IList<DetalleDeRendicion>> GetDataInstance() {
            return new Galac.Adm.Dal.CajaChica.clsDetalleDeRendicionDat();
        }


        public bool IsValidNroDocumentoCodigoProveedorKey(DetalleDeRendicion detalle) {
            Galac.Adm.Brl.CajaChica.clsCxPNav CxpNav = new Galac.Adm.Brl.CajaChica.clsCxPNav();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInString("SQLWhere", "dbo.Gv_CXP_B1.ConsecutivoCompania = " + detalle.ConsecutivoCompania +
            " AND  dbo.Gv_CXP_B1.Numero = '" + detalle.NumeroDocumento + "' AND dbo.Gv_CXP_B1.CodigoProveedor = '" + detalle.CodigoProveedor + "'", 200);
            var parametros = vParams.Get();
            List<CxP> auxiliarList = ((List<CxP>)CxpNav.buscarCxp(parametros));
            //  LibResponse f = new LibResponse();
            if (auxiliarList.Count > 0) {
            return false;
            }

            return true;
        } 

        #endregion //Metodos Generados


    } //End of class clsDetalleDeRendicionNav

} //End of namespace Galac.Saw.Brl.Rendicion

