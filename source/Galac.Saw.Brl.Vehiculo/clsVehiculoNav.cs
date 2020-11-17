using System;
using System.Collections.Generic;
using System.Text;
using System.Security;
using System.Security.Permissions;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.Brl;
using Entity = Galac.Saw.Ccl.Vehiculo;
using System.Xml.Linq;
using System.Linq;
using Galac.Saw.Brl.Inventario;


namespace Galac.Saw.Brl.Vehiculo {
    public partial class clsVehiculoNav : LibBaseNav<IList<Entity.Vehiculo>, IList<Entity.Vehiculo>>, ILibPdn {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores

        public clsVehiculoNav() {
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override ILibDataComponentWithSearch<IList<Entity.Vehiculo>, IList<Entity.Vehiculo>> GetDataInstance() {
            return new Galac.Saw.Dal.Vehiculo.clsVehiculoDat();
        }
        #region Miembros de ILibPdn

        bool ILibPdn.CanBeChoosen(string valCallingModule, eAccionSR valAction, string valExtendedAction, XmlDocument valXmlRow) {
            bool vResult = false;
            //ILibDataFKSearch instanciaDal = new Galac.Saw.Dal.Vehiculo.clsVehiculoDat();
            switch (valCallingModule) {
                default:
                    vResult = true;
                    break;
            }
            return vResult;
        }

        bool ILibPdn.GetDataForList(string valCallingModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            ILibDataFKSearch instanciaDal = new Galac.Saw.Dal.Vehiculo.clsVehiculoDat();
            bool vResult = instanciaDal.ConnectFk(ref refXmlDocument, eProcessMessageType.SpName, "Saw.Gp_VehiculoSCH", valXmlParamsExpression);
            if (!LibXml.IsEmptyOrNull(refXmlDocument)) {
               XElement vData = LibXml.ToXElement(refXmlDocument);
               XElement vDataFKCliente = FindInfoCliente(XmlForeignKeyFromCliente(vData));
               XElement vDataFKColor = FindInfoColor(XmlForeingKeyFromColor(vData));
               refXmlDocument = LibXml.CreateXmlDocument(FillWithAllForeignInfo(vData, vDataFKCliente, vDataFKColor));
            }
           return vResult;
        }

        private XElement FillWithAllForeignInfo(XElement valData, XElement valDataFKCliente, XElement valDataFKColor) {
           var vDataFk =
               (from vVehiculo in valData.Descendants("GpResult")
                join vCliente in valDataFKCliente.Descendants("GpResult")
                           on (string)vVehiculo.Element("CodigoCliente").Value equals
                              (string)vCliente.Element("Codigo").Value
                join vColor in valDataFKColor.Descendants("GpResult")
                           on (string)vVehiculo.Element("CodigoColor").Value equals
                              (string)vColor.Element("CodigoColor").Value
                select new {
                   Consecutivo = LibConvert.ToInt(vVehiculo.Element("Consecutivo").Value),
                   NombreCliente = vCliente.Element("Nombre").Value,
                   DescripcionColor = vColor.Element("DescripcionColor").Value
                }).ToList();
           foreach (var item in valData.Descendants("GpResult")) {
              int vConsecutivo = LibConvert.ToInt(item.Element("Consecutivo").Value);
              var vResultFk = vDataFk.Where(p => p.Consecutivo == vConsecutivo);
                  foreach (var vItenFK in vResultFk) {                       
                        item.Add(new XElement("NombreCliente"), vItenFK.NombreCliente);
                        item.Add(new XElement("DescripcionColor"), vItenFK.DescripcionColor);
                     break;
              }
           }
           return valData;
        }

        private XElement XmlForeignKeyFromCliente(XElement valDataVehiculo) {
           if (valDataVehiculo == null) {
              return null;
           }
           XElement[] vCodigo = valDataVehiculo.Descendants("GpResult")
               .Select(p => new XElement("GpResult",
                   new XElement("Codigo",
                   p.Element("CodigoCliente").Value))).ToArray();
           XElement vResult = new XElement("GpData", vCodigo);
           return vResult;
        }

        private XElement XmlForeingKeyFromColor(XElement valDataColor) {
           if (valDataColor == null) {
              return null;
           }

           XElement[] vCodigoColor = valDataColor.Descendants("GpResult")
              .Select(p => new XElement("GpResult",
                 new XElement("CodigoColor",
                  p.Element("CodigoColor").Value))).ToArray();
           XElement vResult = new XElement("GpData", vCodigoColor);
           return vResult;
        }

        private XElement FindInfoCliente(XElement valDataCLiente) {
           XElement vDataCodigoCliente = valDataCLiente;
           Ccl.Cliente.IClientePdn  vClientePdn = new Galac.Saw.Brl.Cliente.clsClienteNav();           
           LibGpParams vParams = new LibGpParams();
           vParams.AddInInteger("ConsecutivoCompania", LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"));
           vParams.AddInXml("XmlDataDetail", vDataCodigoCliente);
           return vClientePdn.GetFk("Vehículo", vParams.Get());
        }


        
        private XElement FindInfoColor(XElement valDataColor) {
           XElement vDataColor = valDataColor;
           ILibPdn vColor = new Galac.Saw.Brl.Inventario.clsColorNav();
           LibGpParams vParams = new LibGpParams();
           vParams.AddInInteger("ConsecutivoCompania", LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"));
           vParams.AddInXml("XmlDataDetail", vDataColor);
           return vColor.GetFk("Vehículo", vParams.Get());
        }

        System.Xml.Linq.XElement ILibPdn.GetFk(string valCallingModule, StringBuilder valParameters) {
            ILibDataComponent<IList<Entity.Vehiculo>, IList<Entity.Vehiculo>> instanciaDal = new Galac.Saw.Dal.Vehiculo.clsVehiculoDat();
            return instanciaDal.QueryInfo(eProcessMessageType.SpName, "Saw.Gp_VehiculoGetFk", valParameters);
        }
        #endregion //Miembros de ILibPdn

        protected override bool RetrieveListInfo(string valModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            bool vResult = false;
            ILibPdn vPdnModule;
            switch (valModule) {
                case "Vehículo":
                    vResult = ((ILibPdn)this).GetDataForList(valModule, ref refXmlDocument, valXmlParamsExpression);
                    break;
                case "Modelo":
                    vPdnModule = new Galac.Saw.Brl.Vehiculo.clsModeloNav();
                    vResult = vPdnModule.GetDataForList("Vehículo", ref refXmlDocument, valXmlParamsExpression);
                    break;
                case "Marca":
                    vPdnModule = new Galac.Saw.Brl.Vehiculo.clsMarcaNav();
                    vResult = vPdnModule.GetDataForList("Vehículo", ref refXmlDocument, valXmlParamsExpression);
                    break;
                case "Color":
                    vPdnModule = new Galac.Saw.Brl.Inventario.clsColorNav();
                    vResult = vPdnModule.GetDataForList("Vehículo", ref refXmlDocument, valXmlParamsExpression);
                    break;
                case "Cliente":
                    vPdnModule = new Galac.Saw.Brl.Cliente.clsClienteNav();
                    vResult = vPdnModule.GetDataForList("Vehículo", ref refXmlDocument, valXmlParamsExpression);
                    break;
                default: break;
            }
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class clsVehiculoNav

} //End of namespace Galac.Saw.Brl.Vehiculo

