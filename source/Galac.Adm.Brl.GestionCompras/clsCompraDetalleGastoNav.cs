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
using Galac.Adm.Ccl.GestionCompras;

namespace Galac.Adm.Brl.GestionCompras {
    public partial class clsCompraDetalleGastoNav: LibBaseNavDetail<IList<CompraDetalleGasto>, IList<CompraDetalleGasto>>, ICompraDetalleGastoPdn {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores

        public clsCompraDetalleGastoNav() {
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override ILibDataDetailComponent<IList<CompraDetalleGasto>, IList<CompraDetalleGasto>> GetDataInstance() {
            return new Galac.Adm.Dal.GestionCompras.clsCompraDetalleGastoDat();
        }

        internal void FillWithForeignInfo(ref IList<Compra> refData) {
            FillWithForeignInfoCompraDetalleGasto(ref refData);
        }
        #region CompraDetalleGasto


        private void FillWithForeignInfoCompraDetalleGasto(ref IList<Compra> refData) {
            XElement vInfoConexionCxP = FindInfoCxP(refData);
            var vListCxP = (from vRecord in vInfoConexionCxP.Descendants("GpResult")
                            select new {
                                ConsecutivoCompania = LibConvert.ToInt(vRecord.Element("ConsecutivoCompania")),
                                ConsecutivoCxp = LibConvert.ToInt(vRecord.Element("ConsecutivoCxp")),
                                CodigoProveedor = vRecord.Element("CodigoProveedor").Value,
                                NombreProveedor = vRecord.Element("NombreProveedor").Value,
                                Fecha = vRecord.Element("Fecha").Value,
                                MontoExento = LibConvert.ToDec(vRecord.Element("MontoExento")),
                                MontoGravado = LibConvert.ToDec(vRecord.Element("MontoGravado")),
                                MontoIva = LibConvert.ToDec(vRecord.Element("MontoIva")),
                                MontoAbonado = LibConvert.ToDec(vRecord.Element("MontoAbonado")),

                            }).Distinct();
            foreach (Compra vItem in refData) {
                vItem.DetailCompraDetalleGasto =
                    new System.Collections.ObjectModel.ObservableCollection<CompraDetalleGasto>((
                        from vDetail in vItem.DetailCompraDetalleGasto
                        join vCxP in vListCxP
                        on new { consecutivo = vDetail.ConsecutivoCxP , ConsecutivoCompania = vDetail.ConsecutivoCompania }
                        equals
                        new { consecutivo = vCxP.ConsecutivoCxp, ConsecutivoCompania = vCxP.ConsecutivoCompania }
                        select new CompraDetalleGasto {
                            ConsecutivoCompania = vDetail.ConsecutivoCompania,
                            ConsecutivoCompra = vDetail.ConsecutivoCompra,
                            ConsecutivoCxP = vDetail.ConsecutivoCxP ,
                            ConsecutivoRenglon = vDetail.ConsecutivoRenglon,
                            Monto = vDetail.Monto
                        }).ToList<CompraDetalleGasto>());
            }
        }

        private XElement FindInfoCxP(IList<Compra> valData) {
            XElement vXElement = new XElement("GpData");
            foreach (Compra vItem in valData) {
                vXElement.Add(FilterCompraDetalleGastoByDistinctCxP(vItem).Descendants("GpResult"));
            }
            XElement vXElementResult = CxPGetFk("Compra", ParametersGetFKCxPForXmlSubSet(valData[0].ConsecutivoCompania, vXElement));
            return vXElementResult;
        }

        private XElement CxPGetFk(string v, StringBuilder stringBuilder) {
            throw new NotImplementedException();
        }

        private XElement FilterCompraDetalleGastoByDistinctCxP(Compra valMaster) {
            XElement vXElement = new XElement("GpData",
                from vEntity in valMaster.DetailCompraDetalleGasto.Distinct()
                select new XElement("GpResult",
                    new XElement("ConsecutivoCxp", vEntity.ConsecutivoCxP )));
            return vXElement;
        }

        private StringBuilder ParametersGetFKCxPForXmlSubSet(int valConsecutivoCompania, XElement valXElement) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInXml("XmlData", valXElement);
            vResult = vParams.Get();
            return vResult;
        }
        #endregion //CompraDetalleGasto
        #endregion //Metodos Generados
        #region Codigo Ejemplo
        /* Codigo de Ejemplo

        bool ICompraDetalleGastoPdn.InsertDefaultRecord(int valConsecutivoCompania) {
            ILibDataComponent<IList<CompraDetalleGasto>, IList<CompraDetalleGasto>> instanciaDal = new clsCompraDetalleGastoDat();
            IList<CompraDetalleGasto> vLista = new List<CompraDetalleGasto>();
            CompraDetalleGasto vCurrentRecord = new Galac.Adm.Dal.GestionComprasCompraDetalleGasto();
            vCurrentRecord.ConsecutivoCompania = valConsecutivoCompania;
            vCurrentRecord.ConsecutivoCompania = 0;
            vCurrentRecord.ConsecutivoCompra = 0;
            vCurrentRecord.ConsecutivoCxp = 0;
            vCurrentRecord.ConsecutivoRenglon = 0;
            vCurrentRecord.Monto = 0;
            vLista.Add(vCurrentRecord);
            return instanciaDal.Insert(vLista).Success;
        }

        private List<CompraDetalleGasto> ParseToListEntity(XElement valXmlEntity) {
            List<CompraDetalleGasto> vResult = new List<CompraDetalleGasto>();
            var vEntity = from vRecord in valXmlEntity.Descendants("GpResult")
                          select vRecord;
            foreach (XElement vItem in vEntity) {
                CompraDetalleGasto vRecord = new CompraDetalleGasto();
                vRecord.Clear();
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("ConsecutivoCompania"), null))) {
                    vRecord.ConsecutivoCompania = LibConvert.ToInt(vItem.Element("ConsecutivoCompania"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("ConsecutivoCompra"), null))) {
                    vRecord.ConsecutivoCompra = LibConvert.ToInt(vItem.Element("ConsecutivoCompra"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("ConsecutivoCxp"), null))) {
                    vRecord.ConsecutivoCxp = LibConvert.ToInt(vItem.Element("ConsecutivoCxp"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("ConsecutivoRenglon"), null))) {
                    vRecord.ConsecutivoRenglon = LibConvert.ToInt(vItem.Element("ConsecutivoRenglon"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Monto"), null))) {
                    vRecord.Monto = LibConvert.ToDec(vItem.Element("Monto"));
                }
                vResult.Add(vRecord);
            }
            return vResult;
        }
        */
        #endregion //Codigo Ejemplo

        bool ICompraDetalleGastoPdn.CxpEstaSiendoUsadaEnOtroCompra(int valConsecutivoCompania, int valConsecutivoCxP, int valConsecutivoCompra) {
            bool vResult = false;
            StringBuilder vSQL = new StringBuilder();
            vSQL.AppendLine(" SELECT COUNT(ConsecutivoCompania) AS Cantidad FROM Adm.CompraDetalleGasto ");
            vSQL.AppendLine(" WHERE ConsecutivoCompania = @ConsecutivoCompania  AND  ConsecutivoCxP = @ConsecutivoCxP AND ConsecutivoCompra <> @ConsecutivoCompra ");
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInInteger("ConsecutivoCxP", valConsecutivoCxP);
            vParams.AddInInteger("ConsecutivoCompra", valConsecutivoCompra);
            XElement vData = LibBusiness.ExecuteSelect(vSQL.ToString(), vParams.Get(), "", 0);
            if (vData != null) {
                vResult = LibConvert.ToInt(LibXml.GetPropertyString(vData, "Cantidad")) > 0;
            }
            return vResult;

        }

    } //End of class clsCompraDetalleGastoNav

} //End of namespace Galac.Adm.Brl.GestionCompras

