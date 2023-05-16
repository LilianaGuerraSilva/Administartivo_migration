using System;
using System.Collections.Generic;
using System.Text;
using System.Security;
using System.Security.Permissions;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.Brl;
using Entity = Galac.Adm.Ccl.Vendedor;
using System.Xml.Linq;
using System.Linq;
using Galac.Comun.Brl.TablasGen;
using Galac.Saw.Brl.Tablas;
using Galac.Adm.Ccl.Vendedor;
using Galac.Adm.Dal.Vendedor;
using System.Threading;

namespace Galac.Adm.Brl.Vendedor {
    public partial class clsVendedorNav : LibBaseNavMaster<IList<Entity.Vendedor>, IList<Entity.Vendedor>>, ILibPdn, Entity.IVendedorPdn {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores

        public clsVendedorNav() {
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override ILibDataMasterComponentWithSearch<IList<Entity.Vendedor>, IList<Entity.Vendedor>> GetDataInstance() {
            return new Dal.Vendedor.clsVendedorDat();
        }
        #region Miembros de ILibPdn

        bool ILibPdn.CanBeChoosen(string valCallingModule, eAccionSR valAction, string valExtendedAction, XmlDocument valXmlRow) {
            bool vResult = false;
            //ILibDataFKSearch instanciaDal = new Galac.Saw.Dal.Vendedor.clsVendedorDat();
            switch (valCallingModule) {
                default:
                    vResult = true;
                    break;
            }
            return vResult;
        }

        bool ILibPdn.GetDataForList(string valCallingModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            ILibDataFKSearch instanciaDal = new Dal.Vendedor.clsVendedorDat();
            return instanciaDal.ConnectFk(ref refXmlDocument, eProcessMessageType.SpName, "Adm.Gp_VendedorSCH", valXmlParamsExpression);
        }

        System.Xml.Linq.XElement ILibPdn.GetFk(string valCallingModule, StringBuilder valParameters) { 
            ILibDataMasterComponent<IList<Entity.Vendedor>, IList<Entity.Vendedor>> instanciaDal = new Galac.Adm.Dal.Vendedor.clsVendedorDat();
            return instanciaDal.QueryInfo(eProcessMessageType.SpName, "Adm.Gp_VendedorGetFk", valParameters);
        }
        #endregion //Miembros de ILibPdn

        protected override bool RetrieveListInfo(string valModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            bool vResult = false;
            ILibPdn vPdnModule;
            switch (valModule) {
                case "Vendedor":
                    vResult = ((ILibPdn)this).GetDataForList(valModule, ref refXmlDocument, valXmlParamsExpression);
                    break;
                case "Ciudad":
                    vPdnModule = new clsCiudadNav();
                    vResult = vPdnModule.GetDataForList("Vendedor", ref refXmlDocument, valXmlParamsExpression);
                    break;
                case "Línea de Producto":
                    vPdnModule = new clsLineaDeProductoNav();
                    vResult = vPdnModule.GetDataForList("Vendedor", ref refXmlDocument, valXmlParamsExpression);
                    //vResult = ((ILibPdn)this).GetDataForList("Vendedor", ref refXmlDocument, valXmlParamsExpression);
                    break;
                default: throw new NotImplementedException();
            }
            return vResult;
        }
        #endregion //Metodos Generados

        XElement VendedorToXml(Entity.Vendedor valEntidad) {
            XElement vXElement = new XElement("GpData",
                    new XElement("GpResult",
                    new XElement("ConsecutivoCompania", valEntidad.ConsecutivoCompania),
                    new XElement("Codigo", valEntidad.Codigo)));
            return vXElement;
        }

        protected LibResponse InsertRecord(IList<Entity.Vendedor> refRecord) {
            return new LibResponse();
            //return vResult;
        }

        Entity.Vendedor VendedorPorDefecto(int valConsecutivoCompania) {
            Entity.Vendedor insVendedor = new Entity.Vendedor();
            insVendedor.ConsecutivoCompania = valConsecutivoCompania;
            insVendedor.Consecutivo = 1;
            insVendedor.Codigo = "00001";
            insVendedor.Nombre = "OFICINA";
            insVendedor.RIF = "00001";
            insVendedor.Ciudad = "CARACAS";
            return insVendedor;
        }
 
        XElement Entity.IVendedorPdn.VendedorPorDefecto(int valConsecutivoCompania) {
            return VendedorToXml(VendedorPorDefecto(valConsecutivoCompania)); 
        }

        string Entity.IVendedorPdn.BuscarNombreVendedor(int valConsecutivoCompania, string valCodigo) {
            string vResult = "";
            LibGpParams vParams = new LibGpParams();
            vParams.AddInString("Codigo", valCodigo, 5);
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            RegisterClient();
            string vSql = "SELECT Nombre FROM Vendedor WHERE Codigo = @Codigo AND ConsecutivoCompania = @ConsecutivoCompania";
            XElement vData = _Db.QueryInfo(eProcessMessageType.Query, vSql, vParams.Get());
            if (vData != null) {
                vResult = LibConvert.ToStr(LibXml.GetPropertyString(vData, "Nombre"));
            }
            return vResult;
        }

        XElement BuscarTopeInicialYFinalCobranza(int valConsecutivoCompania, string valCodigo) {
            XElement vResult;
            StringBuilder vSql = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInString("Codigo", valCodigo, 6);
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vSql.AppendLine("   SELECT");
            vSql.AppendLine("		ConsecutivoCompania");
            vSql.AppendLine("      , Codigo");
            vSql.AppendLine("      , TopeInicialCobranza1");
            vSql.AppendLine("      , TopeFinalCobranza1");
            vSql.AppendLine("      , PorcentajeCobranza1");
            vSql.AppendLine("      , TopeFinalCobranza2");
            vSql.AppendLine("      , PorcentajeCobranza2");
            vSql.AppendLine("      , TopeFinalCobranza3");
            vSql.AppendLine("      , PorcentajeCobranza3");
            vSql.AppendLine("      , TopeFinalCobranza4");
            vSql.AppendLine("      , PorcentajeCobranza4");
            vSql.AppendLine("      , TopeFinalCobranza5");
            vSql.AppendLine("      , PorcentajeCobranza5");
            vSql.AppendLine("  FROM Vendedor");
            vSql.AppendLine("  WHERE");
            vSql.AppendLine("	Codigo = @Codigo");
            vSql.AppendLine("	AND ConsecutivoCompania = @ConsecutivoCompania");
            vResult = LibBusiness.ExecuteSelect(vSql.ToString(), vParams.Get(), "", 0);
            return vResult;
        }
        #region Utilizado en reportes de cobranza

        void Entity.IVendedorPdn.CalculaMontoPorcentajeYNivelDeComisionInforme(string valCodigo, decimal valMontoComisionable, decimal valMontoComisionableEnMonedaLocal, ref decimal refMontoComision, ref decimal refPorcentajeComision, ref string refNivelDeComision) {
            int vConsecutivoCompania = LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania");
            Entity.eNivelDeComisionVentaYCobranza nivelDeComisionCobranza;
            XElement datosDeComisionesVendedor = BuscarTopeInicialYFinalCobranza(vConsecutivoCompania, valCodigo);
            Entity.Vendedor vendedorConNivelesDeComision = AsignacionDatosDeComisionVendedor(datosDeComisionesVendedor);
            nivelDeComisionCobranza = ObtenerNivelDeComisionPorCobranza(vendedorConNivelesDeComision, valMontoComisionableEnMonedaLocal);
            refNivelDeComision = nivelDeComisionCobranza.GetDescription();
            refPorcentajeComision = ObtenerPorcentajeSegunNivelDeComisionPorCobranza(vendedorConNivelesDeComision, nivelDeComisionCobranza);
            refMontoComision = CalculoComisionSegunNivelDeComisionPorCobranza(vendedorConNivelesDeComision, nivelDeComisionCobranza, valMontoComisionable);
        }

        private Entity.Vendedor AsignacionDatosDeComisionVendedor (XElement valVendedor) {
            Entity.Vendedor insVendedor = new Entity.Vendedor();
            insVendedor.TopeInicialCobranza1 = valVendedor.Descendants().Select(s => (decimal)s.Element("TopeInicialCobranza1")).FirstOrDefault();
            insVendedor.TopeFinalCobranza1 = valVendedor.Descendants("GpResult").Select(s => (decimal)s.Element("TopeFinalCobranza1")).FirstOrDefault();
            insVendedor.PorcentajeCobranza1 = valVendedor.Descendants("GpResult").Select(s => (decimal)s.Element("PorcentajeCobranza1")).FirstOrDefault();
            insVendedor.TopeFinalCobranza2 = valVendedor.Descendants().Select(s => (decimal)s.Element("TopeFinalCobranza2")).FirstOrDefault();
            insVendedor.PorcentajeCobranza2 = valVendedor.Descendants("GpResult").Select(s => (decimal)s.Element("PorcentajeCobranza2")).FirstOrDefault();
            insVendedor.TopeFinalCobranza3 = valVendedor.Descendants("GpResult").Select(s => (decimal)s.Element("TopeFinalCobranza3")).FirstOrDefault();
            insVendedor.PorcentajeCobranza3 = valVendedor.Descendants("GpResult").Select(s => (decimal)s.Element("PorcentajeCobranza3")).FirstOrDefault();
            insVendedor.TopeFinalCobranza4 = valVendedor.Descendants().Select(s => (decimal)s.Element("TopeFinalCobranza4")).FirstOrDefault();
            insVendedor.PorcentajeCobranza4 = valVendedor.Descendants("GpResult").Select(s => (decimal)s.Element("PorcentajeCobranza4")).FirstOrDefault();
            insVendedor.TopeFinalCobranza5 = valVendedor.Descendants("GpResult").Select(s => (decimal)s.Element("TopeFinalCobranza5")).FirstOrDefault();
            insVendedor.PorcentajeCobranza5 = valVendedor.Descendants("GpResult").Select(s => (decimal)s.Element("PorcentajeCobranza5")).FirstOrDefault();
            return insVendedor;
        }

        private Entity.eNivelDeComisionVentaYCobranza ObtenerNivelDeComisionPorCobranza(Entity.Vendedor valVendedor, decimal valMontoComisionableEnMonedaLocal) {
            Entity.eNivelDeComisionVentaYCobranza vResult = Entity.eNivelDeComisionVentaYCobranza.MenorANivel1;
            if (valMontoComisionableEnMonedaLocal < valVendedor.TopeInicialCobranza1) {
                vResult = Entity.eNivelDeComisionVentaYCobranza.MenorANivel1;
            } else if(valMontoComisionableEnMonedaLocal >= valVendedor.TopeInicialCobranza1 && valMontoComisionableEnMonedaLocal <= valVendedor.TopeFinalCobranza1) {
                vResult = Entity.eNivelDeComisionVentaYCobranza.Nivel1;
            } else if (valMontoComisionableEnMonedaLocal > valVendedor.TopeFinalCobranza1 && valMontoComisionableEnMonedaLocal <= valVendedor.TopeFinalCobranza2) {
                vResult = Entity.eNivelDeComisionVentaYCobranza.Nivel2;
            } else if (valMontoComisionableEnMonedaLocal > valVendedor.TopeFinalCobranza2 && valMontoComisionableEnMonedaLocal <= valVendedor.TopeFinalCobranza3) {
                vResult = Entity.eNivelDeComisionVentaYCobranza.Nivel3;
            } else if (valMontoComisionableEnMonedaLocal > valVendedor.TopeFinalCobranza3 && valMontoComisionableEnMonedaLocal <= valVendedor.TopeFinalCobranza4) {
                vResult = Entity.eNivelDeComisionVentaYCobranza.Nivel4;
            } else if (valMontoComisionableEnMonedaLocal > valVendedor.TopeFinalCobranza4) {
                if ( valVendedor.PorcentajeCobranza5 > 0 ) {
                    vResult = Entity.eNivelDeComisionVentaYCobranza.Nivel5;
                } else {
                    vResult = Entity.eNivelDeComisionVentaYCobranza.SinAsignar;
                }
            }
            return vResult;
        }

        private decimal CalculoComisionSegunNivelDeComisionPorCobranza (Entity.Vendedor valVendedor,Entity.eNivelDeComisionVentaYCobranza valNivelComision, decimal valMontoComisionable) {
            decimal vResult = 0;
            switch (valNivelComision) {
                case Entity.eNivelDeComisionVentaYCobranza.Nivel1:
                    vResult = (valMontoComisionable * (valVendedor.PorcentajeCobranza1 / 100));
                    break;
                case Entity.eNivelDeComisionVentaYCobranza.Nivel2:
                    vResult = (valMontoComisionable * (valVendedor.PorcentajeCobranza2 / 100));
                    break;
                case Entity.eNivelDeComisionVentaYCobranza.Nivel3:
                    vResult = (valMontoComisionable * (valVendedor.PorcentajeCobranza3 / 100));
                    break;
                case Entity.eNivelDeComisionVentaYCobranza.Nivel4:
                    vResult = (valMontoComisionable * (valVendedor.PorcentajeCobranza4 / 100));
                    break;
                case Entity.eNivelDeComisionVentaYCobranza.Nivel5:
                    vResult = (valMontoComisionable * (valVendedor.PorcentajeCobranza5 / 100));
                    break;
                default:
                    vResult = 0;
                    break;
            };
            return vResult;
        }

        private decimal ObtenerPorcentajeSegunNivelDeComisionPorCobranza(Entity.Vendedor valVendedor, Entity.eNivelDeComisionVentaYCobranza valNivelComision) {
            decimal vResult = 0;
            switch (valNivelComision) {
                case Entity.eNivelDeComisionVentaYCobranza.Nivel1:
                    vResult = valVendedor.PorcentajeCobranza1;
                    break;
                case Entity.eNivelDeComisionVentaYCobranza.Nivel2:
                    vResult = valVendedor.PorcentajeCobranza2;
                    break;
                case Entity.eNivelDeComisionVentaYCobranza.Nivel3:
                    vResult = valVendedor.PorcentajeCobranza3;
                    break;
                case Entity.eNivelDeComisionVentaYCobranza.Nivel4:
                    vResult = valVendedor.PorcentajeCobranza4;
                    break;
                case Entity.eNivelDeComisionVentaYCobranza.Nivel5:
                    vResult = valVendedor.PorcentajeCobranza5;
                    break;
                default:
                    vResult = 0;
                    break;
            }
            return vResult;
        }

        void IVendedorPdn.InsertarElPrimerVendedor(int valConsecutivoCompania) {
            List<Entity.Vendedor> vVendedores = new List<Entity.Vendedor> {
                VendedorPorDefecto(valConsecutivoCompania)
            };
            IVendedorDatPdn insVendedorDatPdn = new clsVendedorDat();
            insVendedorDatPdn.InsertarListaDeVendedores(vVendedores);
        }
        #endregion

    } //End of class clsVendedorNav

} //End of namespace Galac.Saw.Brl.Vendedor

