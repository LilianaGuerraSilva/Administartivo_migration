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

namespace Galac.Saw.Brl.Inventario {
    public partial class clsRenglonNotaESNav: LibBaseNavDetail<IList<RenglonNotaES>, IList<RenglonNotaES>> {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores

        public clsRenglonNotaESNav() {
        }
        #endregion //Constructores
        #region Metodos Generados
        protected override ILibDataDetailComponent<IList<RenglonNotaES>, IList<RenglonNotaES>> GetDataInstance() {
            return new Galac.Saw.Dal.Inventario.clsRenglonNotaESDat();
        }

        private void FillWithForeignInfo(ref IList<RenglonNotaES> refData) {
            FillWithForeignInfoRenglonNotaES(ref refData);
        }
        #region RenglonNotaES

        private void FillWithForeignInfoRenglonNotaES(ref IList<RenglonNotaES> refData) {
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
                                          CostoUnitario = LibConvert.ToDec(vRecord.Element("CostoUnitario")), 
                                          Existencia = LibConvert.ToDec(vRecord.Element("Existencia"))                                           
                                      }).Distinct();

            foreach (RenglonNotaES vItem in refData) {
            }
        }

        private XElement FindInfoArticuloInventario(IList<RenglonNotaES> valData) {
            XElement vXElement = new XElement("GpData");
            foreach(RenglonNotaES vItem in valData) {
                vXElement.Add(FilterRenglonNotaESByDistinctArticuloInventario(vItem).Descendants("GpResult"));
            }
            ILibPdn insArticuloInventario = new Galac.Saw.Brl.Inventario.clsArticuloInventarioNav();
            XElement vXElementResult = insArticuloInventario.GetFk("RenglonNotaES", ParametersGetFKArticuloInventarioForXmlSubSet(valData[0].ConsecutivoCompania, vXElement));
            return vXElementResult;
        }

        private XElement FilterRenglonNotaESByDistinctArticuloInventario(RenglonNotaES valMaster) {
            XElement vXElement = new XElement("GpData",                
                new XElement("GpResult",
                    new XElement("CodigoArticulo", valMaster.CodigoArticulo)));
            return vXElement;
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
        #endregion //RenglonNotaES
        #endregion //Metodos Generados
        #region Codigo Ejemplo
        /* Codigo de Ejemplo

        bool IRenglonNotaESPdn.InsertDefaultRecord(int valConsecutivoCompania) {
            ILibDataComponent<IList<RenglonNotaES>, IList<RenglonNotaES>> instanciaDal = new clsRenglonNotaESDat();
            IList<RenglonNotaES> vLista = new List<RenglonNotaES>();
            RenglonNotaES vCurrentRecord = new Galac.Saw.Dal.InventarioRenglonNotaES();
            vCurrentRecord.ConsecutivoCompania = valConsecutivoCompania;
            vCurrentRecord.ConsecutivoCompania = 0;
            vCurrentRecord.NumeroDocumento = "";
            vCurrentRecord.ConsecutivoRenglon = 0;
            vCurrentRecord.CodigoArticulo = "";
            vCurrentRecord.Cantidad = 0;
            vCurrentRecord.TipoArticuloInvAsEnum = eTipoArticuloInv.Simple;
            vCurrentRecord.Serial = "";
            vCurrentRecord.Rollo = "";
            vCurrentRecord.CostoUnitario = 0;
            vCurrentRecord.CostoUnitarioME = 0;
            vCurrentRecord.LoteDeInventario = "";
            vLista.Add(vCurrentRecord);
            return instanciaDal.Insert(vLista).Success;
        }
        */
        #endregion //Codigo Ejemplo

        internal List<RenglonNotaES> ParseToListEntity(XElement valXmlEntity) {
            List<RenglonNotaES> vResult = new List<RenglonNotaES>();
            var vEntity = from vRecord in valXmlEntity.Descendants("GpResult")
                          select vRecord;
            foreach (XElement vItem in vEntity) {
                RenglonNotaES vRecord = new RenglonNotaES();
                vRecord.Clear();
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("ConsecutivoCompania"), null))) {
                    vRecord.ConsecutivoCompania = LibConvert.ToInt(vItem.Element("ConsecutivoCompania"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("NumeroDocumento"), null))) {
                    vRecord.NumeroDocumento = vItem.Element("NumeroDocumento").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("ConsecutivoRenglon"), null))) {
                    vRecord.ConsecutivoRenglon = LibConvert.ToInt(vItem.Element("ConsecutivoRenglon"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("CodigoArticulo"), null))) {
                    vRecord.CodigoArticulo = vItem.Element("CodigoArticulo").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Cantidad"), null))) {
                    vRecord.Cantidad = LibConvert.ToDec(vItem.Element("Cantidad"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("TipoArticuloInv"), null))) {
                    vRecord.TipoArticuloInv = vItem.Element("TipoArticuloInv").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Serial"), null))) {
                    vRecord.Serial = vItem.Element("Serial").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Rollo"), null))) {
                    vRecord.Rollo = vItem.Element("Rollo").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("CostoUnitario"), null))) {
                    vRecord.CostoUnitario = LibConvert.ToDec(vItem.Element("CostoUnitario"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("CostoUnitarioME"), null))) {
                    vRecord.CostoUnitarioME = LibConvert.ToDec(vItem.Element("CostoUnitarioME"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("LoteDeInventario"), null))) {
                    vRecord.LoteDeInventario = vItem.Element("LoteDeInventario").Value;
                }
                vResult.Add(vRecord);
            }
            return vResult;
        }


    } //End of class clsRenglonNotaESNav

} //End of namespace Galac.Saw.Brl.Inventario

