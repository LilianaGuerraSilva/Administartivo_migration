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
using Galac.Saw.Ccl.Tablas;

namespace Galac.Saw.Brl.Tablas {
    public partial class clsTasaDeCambioNav: ITasaDeCambioPdn {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores

        public clsTasaDeCambioNav() {
        }
        #endregion //Constructores
        #region Metodos
        bool ITasaDeCambioPdn.ExisteTasaDeCambioParaElDia(string valMoneda, DateTime valFecha, out decimal outTasa) {
            StringBuilder vSQL = new StringBuilder();
            bool vResult = false;
            outTasa = 0;
            vSQL.AppendLine("SELECT CodigoMoneda, CambioAbolivares FROM dbo.Cambio WHERE CodigoMoneda = @CodigoMoneda AND FechaDeVigencia = @Fecha ");
            LibGpParams vParams = new LibGpParams();
            vParams.AddInDateTime("Fecha", valFecha);
            vParams.AddInString("CodigoMoneda", valMoneda, 4);
            XElement vData = LibBusiness.ExecuteSelect(vSQL.ToString(), vParams.Get(), "", 0);
            if (vData != null) {
                var vEntity = (from vRecord in vData.Descendants("GpResult")
                               select new {
                                   CodigoMoneda = vRecord.Element("CodigoMoneda").Value,
                                   CambioAbolivares = LibConvert.ToDec(vRecord.Element("CambioAbolivares"), 4)
                               }).Distinct();
                foreach (var item in vEntity) {
                    outTasa = item.CambioAbolivares;
                    vResult = true;
                }
            }

            return vResult;
        }

        bool ITasaDeCambioPdn.InsertaTasaDeCambioParaElDia(string valMoneda, DateTime valFechaVigencia, string valNombre, decimal valCambioAbolivares) {
            StringBuilder vSQL = new StringBuilder();
            bool vResult = false;
            LibGpParams vParams = new LibGpParams();
            vParams.AddInDateTime("FechaDeVigencia", valFechaVigencia);
            vParams.AddInString("CodigoMoneda", valMoneda, 4);
            vParams.AddInString("Nombre", valNombre, 80);
            vParams.AddInDecimal("CambioAbolivares", valCambioAbolivares, 4);
            vParams.AddInString("NombreOperador", ((CustomIdentity)System.Threading.Thread.CurrentPrincipal.Identity).Login, 10);
            vParams.AddInDateTime("FechaUltimaModificacion", LibDate.Today());
            vSQL.AppendLine("INSERT INTO dbo.Cambio (CodigoMoneda,FechaDeVigencia,Nombre,CambioAbolivares,NombreOperador, FechaUltimaModificacion) VALUES (@CodigoMoneda,@FechaDeVigencia,@Nombre,@CambioAbolivares,@NombreOperador,@FechaUltimaModificacion)");
            vResult = LibBusiness.ExecuteUpdateOrDelete(vSQL.ToString(), vParams.Get(), "", 0) > 0;
            return vResult;
        }
        #endregion //Metodos
    }
}
