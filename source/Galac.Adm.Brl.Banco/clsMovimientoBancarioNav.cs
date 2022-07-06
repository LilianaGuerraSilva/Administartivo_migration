using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.Brl;
using Galac.Adm.Ccl.Banco;
using System.Xml.Linq;
using System.Linq;
using System.Threading;

namespace Galac.Adm.Brl.Banco {
    public partial class clsMovimientoBancarioNav : LibBaseNav<IList<MovimientoBancario>, IList<MovimientoBancario>>, IMovimientoBancarioPdn {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores

        public clsMovimientoBancarioNav() {
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override ILibDataComponentWithSearch<IList<MovimientoBancario>, IList<MovimientoBancario>> GetDataInstance() {
            return new Galac.Adm.Dal.Banco.clsMovimientoBancarioDat();
        }
        #region Miembros de ILibPdn

        bool ILibPdn.CanBeChoosen(string valCallingModule, eAccionSR valAction, string valExtendedAction, XmlDocument valXmlRow) {
            bool vResult = false;
            //ILibDataFKSearch instanciaDal = new Galac.Adm.Dal.Banco.clsMovimientoBancarioDat();
            switch (valCallingModule) {
                default:
                    vResult = true;
                    break;
            }
            return vResult;
        }

        bool ILibPdn.GetDataForList(string valCallingModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            ILibDataFKSearch instanciaDal = new Galac.Adm.Dal.Banco.clsMovimientoBancarioDat();
            return instanciaDal.ConnectFk(ref refXmlDocument, eProcessMessageType.SpName, "Saw.Gp_MovimientoBancarioSCH", valXmlParamsExpression);
        }

        System.Xml.Linq.XElement ILibPdn.GetFk(string valCallingModule, StringBuilder valParameters) {
            ILibDataComponent<IList<MovimientoBancario>, IList<MovimientoBancario>> instanciaDal = new Galac.Adm.Dal.Banco.clsMovimientoBancarioDat();
            return instanciaDal.QueryInfo(eProcessMessageType.SpName, "Saw.Gp_MovimientoBancarioGetFk", valParameters);
        }
        #endregion //Miembros de ILibPdn

        protected override bool RetrieveListInfo(string valModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            bool vResult = false;
            ILibPdn vPdnModule;
            switch (valModule) {
                case "Movimiento Bancario":
                    vResult = ((ILibPdn)this).GetDataForList(valModule, ref refXmlDocument, valXmlParamsExpression);
                    break;
                case "Cuenta Bancaria":
                    vPdnModule = new Galac.Adm.Brl.Banco.clsCuentaBancariaNav();
                    vResult = vPdnModule.GetDataForList("Movimiento Bancario", ref refXmlDocument, valXmlParamsExpression);
                    break;
                //case "Concepto Bancario":
                //    vPdnModule = new Galac.Saw.  CajaChica.clsConceptoBancarioNav();
                //    vResult = vPdnModule.GetDataForList("Movimiento Bancario", ref refXmlDocument, valXmlParamsExpression);
                //    break;
                default: throw new NotImplementedException();
            }
            return vResult;
        }
        #endregion //Metodos Generados

        public bool Insert(List<MovimientoBancario> list) {
            MovimientoBancario movimiento = new MovimientoBancario();
            ILibDataComponentWithSearch<IList<MovimientoBancario>, IList<MovimientoBancario>> instanciaDal = new Galac.Adm.Dal.Banco.clsMovimientoBancarioDat();
            instanciaDal.Insert(list);
            return true;
        }

        int IMovimientoBancarioPdn.BuscarSiguienteConsecutivoMovimientoBancario(int valConsecutivoCompania) {
            StringBuilder vSql = new StringBuilder();
            LibGpParams vParametros = new LibGpParams();
            vParametros.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vSql.AppendLine("SELECT Max(ConsecutivoMovimiento) AS ConsecutivoMovimiento FROM MovimientoBancario");
            vSql.AppendLine(" WHERE ConsecutivoCompania = @ConsecutivoCompania");
            XElement vResult = LibBusiness.ExecuteSelect(vSql.ToString(), vParametros.Get(), string.Empty, 0);
            if(vResult != null) {
                int vSiguienteConsecutivo = LibImportData.ToInt(LibXml.GetPropertyString(vResult, "ConsecutivoMovimiento"));
                vSiguienteConsecutivo += 1;
                return vSiguienteConsecutivo;
            }
            return 1;
        }
		bool IMovimientoBancarioPdn.AnularMovimientosBancarios(int valConsecutivoCompania, string valNumeroDocumento, eGeneradoPor eGeneradoPor) {
			LibGpParams vParams = new LibGpParams();
			StringBuilder vSql = new StringBuilder();
			vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
			vParams.AddInDecimal("Monto", 0, 2);
			vParams.AddInString("NumeroDocumento", valNumeroDocumento, 20);
			vParams.AddInEnum("GeneradoPor", (int) eGeneradoPor);
			vParams.AddInDateTime("FechaUltimaModificacion", LibDate.Today());
			vParams.AddInString("NombreOperador", ((CustomIdentity) Thread.CurrentPrincipal.Identity).Login, 10);
			vSql.AppendLine("UPDATE dbo.MovimientoBancario");
			vSql.AppendLine("SET Monto = @Monto,");
			vSql.AppendLine("FechaUltimaModificacion = @FechaUltimaModificacion,");
			vSql.AppendLine("NombreOperador = @NombreOperador");
			vSql.AppendLine("WHERE ConsecutivoCompania = @ConsecutivoCompania");
			vSql.AppendLine("AND NumeroDocumento=@NumeroDocumento");
			vSql.AppendLine("AND GeneradoPor=@GeneradoPor");
			return LibBusiness.ExecuteUpdateOrDelete(vSql.ToString(), vParams.Get(), string.Empty, 0) > 0;
		}
    } //End of class clsMovimientoBancarioNav

} //End of namespace Galac.Adm.Brl.Banco

