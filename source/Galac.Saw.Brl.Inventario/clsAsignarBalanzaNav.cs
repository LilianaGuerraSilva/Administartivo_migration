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
using Galac.Saw.Ccl.Inventario;
using LibGalac.Aos.Catching;

namespace Galac.Saw.Brl.Inventario {
    public partial class clsAsignarBalanzaNav: LibBaseNav<IList<AsignarBalanza>, IList<AsignarBalanza>>, IAsignarBalanzaPdn {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores

        public clsAsignarBalanzaNav() {
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override ILibDataComponentWithSearch<IList<AsignarBalanza>, IList<AsignarBalanza>> GetDataInstance() {
            return null; //new Galac.Saw.Dal.Inventario.clsInventarioAsignarBalanzaDat();
        }
        #region Miembros de ILibPdn

        bool ILibPdn.CanBeChoosen(string valCallingModule, eAccionSR valAction, string valExtendedAction, XmlDocument valXmlRow) {
            bool vResult = false;            
            return vResult;
        }

        bool ILibPdn.GetDataForList(string valCallingModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            return false;
        }

        System.Xml.Linq.XElement ILibPdn.GetFk(string valCallingModule, StringBuilder valParameters) {
            return null;
        }
        #endregion //Miembros de ILibPdn

        protected override bool RetrieveListInfo(string valModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            bool vResult = false;
            ILibPdn vPdnModule;
            switch (valModule) {
                case "Inventario Asignar Balanza":
                    vResult = ((ILibPdn)this).GetDataForList(valModule, ref refXmlDocument, valXmlParamsExpression);
                    break;
                case "Articulo Inventario":
                    vPdnModule = new Galac.Saw.Brl.Inventario.clsArticuloInventarioNav();
                    vResult = vPdnModule.GetDataForList("Inventario Asignar Balanza", ref refXmlDocument, valXmlParamsExpression);
                    break;
                default: throw new NotImplementedException();
            }
            return vResult;
        }

        protected override void FillWithForeignInfo(ref IList<AsignarBalanza> refData) {
            FillWithForeignInfoAsignarBalanza(ref refData);
        }
        #region InventarioAsignarBalanza

        private void FillWithForeignInfoAsignarBalanza(ref IList<AsignarBalanza> refData) {
            XElement vInfoConexionArticuloInventario = FindInfoArticuloInventario(refData);
            var vListArticuloInventario = (from vRecord in vInfoConexionArticuloInventario.Descendants("GpResult")
                                      select new {
                                          ConsecutivoCompania = LibConvert.ToInt(vRecord.Element("ConsecutivoCompania")),                                         
                                          Codigo = vRecord.Element("Codigo").Value, 
                                          Descripcion = vRecord.Element("Descripcion").Value, 
                                          LineaDeProducto = vRecord.Element("LineaDeProducto").Value, 
                                          StatusdelArticulo = vRecord.Element("StatusdelArticulo").Value, 
                                          TipoDeArticulo = vRecord.Element("TipoDeArticulo").Value, 
                                          AlicuotaIVA = vRecord.Element("AlicuotaIVA").Value, 
                                          PrecioSinIVA = LibConvert.ToDec(vRecord.Element("PrecioSinIVA")), 
                                          PrecioConIVA = LibConvert.ToDec(vRecord.Element("PrecioConIVA")), 
                                          Existencia = LibConvert.ToDec(vRecord.Element("Existencia")), 
                                          Categoria = vRecord.Element("Categoria").Value, 
                                          Marca = vRecord.Element("Marca").Value, 
                                          FechaDeVencimiento = vRecord.Element("FechaDeVencimiento").Value, 
                                          UnidadDeVenta = vRecord.Element("UnidadDeVenta").Value, 
                                          TipoArticuloInv = vRecord.Element("TipoArticuloInv").Value
                                      }).Distinct();

            foreach (AsignarBalanza vItem in refData) {
            }
        }

        private XElement FindInfoArticuloInventario(IList<AsignarBalanza> valData) {
            XElement vXElement = new XElement("GpData");
            foreach(AsignarBalanza vItem in valData) {
                vXElement.Add(FilterAsignarBalanzaByDistinctArticuloInventario(vItem).Descendants("GpResult"));
            }
            ILibPdn insArticuloInventario = new Galac.Saw.Brl.Inventario.clsArticuloInventarioNav();
            XElement vXElementResult = insArticuloInventario.GetFk("InventarioAsignarBalanza", ParametersGetFKArticuloInventarioForXmlSubSet(valData[0].ConsecutivoCompania, vXElement));
            return vXElementResult;
        }

        private XElement FilterAsignarBalanzaByDistinctArticuloInventario(AsignarBalanza valMaster) {           
            return null;
        }

        private StringBuilder ParametersGetFKArticuloInventarioForXmlSubSet(int valConsecutivoCompania, XElement valXElement) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInXml("XmlData", valXElement);
            vResult = vParams.Get();
            return vResult;
        }
        #endregion //InventarioAsignarBalanza

        XElement IAsignarBalanzaPdn.FindByConsecutivo(int valConsecutivo) {
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("Consecutivo", valConsecutivo);
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("SELECT * FROM Saw.InventarioAsignarBalanza");
            SQL.AppendLine("WHERE Consecutivo = @Consecutivo");
            return LibBusiness.ExecuteSelect(SQL.ToString(), vParams.Get(), "", -1);
        }
        #endregion //Metodos Generados

        public bool AsignarBalanza( string valLineaDeProducto, string valCodigoDesde, string valCodigoHasta, bool valAsignar ) {
            int vConscutivoCompania = LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania");
            QAdvSql insQAdvSql = new QAdvSql("");
            LibGpParams vParams = new LibGpParams();
            try {
                vParams.AddInInteger("ConsecutivoCompania",vConscutivoCompania);
                if(!LibString.IsNullOrEmpty(valCodigoDesde)) {
                    vParams.AddInString("CodigoDesde",valCodigoDesde,30);
                }
                if(!LibString.IsNullOrEmpty(valCodigoHasta)) {
                    vParams.AddInString("CodigoHasta",valCodigoHasta,30);
                }
                if(!LibString.IsNullOrEmpty(valLineaDeProducto)) {
                    vParams.AddInString("LineaDeProducto",valLineaDeProducto,20);
                }
                StringBuilder vSql = new StringBuilder();
                vSql.AppendLine(" Update ArticuloInventario ");
                vSql.AppendLine(" Set UsaBalanza = " + insQAdvSql.ToSqlValue(LibConvert.BoolToSN(valAsignar)));
                vSql.AppendLine(" Where ConsecutivoCompania = @ConsecutivoCompania");
                vSql.AppendLine(" AND TipoDeArticulo  = " + insQAdvSql.EnumToSqlValue((int)eTipoDeArticulo.Mercancia));
                vSql.AppendLine(" AND TipoArticuloInv = " + insQAdvSql.EnumToSqlValue((int)eTipoArticuloInv.Simple));
                if(!LibString.IsNullOrEmpty(valLineaDeProducto)) {
                    vSql.AppendLine(" AND LineaDeProducto=@LineaDeProducto");
                } else if(!LibString.IsNullOrEmpty(valCodigoDesde) && !LibString.IsNullOrEmpty(valCodigoHasta)) {
                    vSql.AppendLine(" AND Codigo BETWEEN @CodigoDesde AND @CodigoHasta ");
                }
                return LibBusiness.ExecuteUpdateOrDelete(vSql.ToString(),vParams.Get(),"",0) != 0;
            } catch(GalacException vEx) {                
                throw vEx;
            }
           
        }      
    } //End of class clsAsignarBalanzaNav

} //End of namespace Galac.Saw.Brl.Inventario

