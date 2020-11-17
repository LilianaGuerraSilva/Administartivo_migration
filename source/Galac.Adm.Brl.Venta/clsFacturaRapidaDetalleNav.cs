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
using Galac.Adm.Ccl.Venta;

namespace Galac.Adm.Brl.Venta {
    public partial class clsFacturaRapidaDetalleNav: LibBaseNavDetail<IList<FacturaRapidaDetalle>, IList<FacturaRapidaDetalle>> {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores

        public clsFacturaRapidaDetalleNav() {
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override ILibDataDetailComponent<IList<FacturaRapidaDetalle>, IList<FacturaRapidaDetalle>> GetDataInstance() {
            return new Galac.Adm.Dal.Venta.clsFacturaRapidaDetalleDat();
        }
        #endregion //Metodos Generados
        #region Codigo Ejemplo
        /* Codigo de Ejemplo

        bool IFacturaRapidaDetallePdn.InsertDefaultRecord(int valConsecutivoCompania) {
            ILibDataComponent<IList<FacturaRapidaDetalle>, IList<FacturaRapidaDetalle>> instanciaDal = new clsFacturaRapidaDetalleDat();
            IList<FacturaRapidaDetalle> vLista = new List<FacturaRapidaDetalle>();
            FacturaRapidaDetalle vCurrentRecord = new Galac.Adm.Dal.VentaFacturaRapidaDetalle();
            vCurrentRecord.ConsecutivoCompania = valConsecutivoCompania;
            vCurrentRecord.ConsecutivoCompania = 0;
            vCurrentRecord.NumeroFactura = "";
            vCurrentRecord.TipoDeDocumentoAsEnum = eTipoDocumentoFactura.Factura;
            vCurrentRecord.ConsecutivoRenglon = 0;
            vCurrentRecord.Articulo = "";
            vCurrentRecord.Descripcion = "";
            vCurrentRecord.CodigoVendedor1 = "";
            vCurrentRecord.CodigoVendedor2 = "";
            vCurrentRecord.CodigoVendedor3 = "";
            vCurrentRecord.AlicuotaIVAAsEnum = eTipoDeAlicuota.Exento;
            vCurrentRecord.Cantidad = 0;
            vCurrentRecord.PrecioSinIVA = 0;
            vCurrentRecord.PrecioConIVA = 0;
            vCurrentRecord.PorcentajeDescuento = 0;
            vCurrentRecord.TotalRenglon = 0;
            vCurrentRecord.PorcentajeBaseImponible = 0;
            vCurrentRecord.Serial = "";
            vCurrentRecord.Rollo = "";
            vCurrentRecord.PorcentajeAlicuota = 0;
            vCurrentRecord.CampoExtraEnRenglonFactura1 = "";
            vCurrentRecord.CampoExtraEnRenglonFactura2 = "";
            vLista.Add(vCurrentRecord);
            return instanciaDal.Insert(vLista).Success;
        }

        private List<FacturaRapidaDetalle> ParseToListEntity(XElement valXmlEntity) {
            List<FacturaRapidaDetalle> vResult = new List<FacturaRapidaDetalle>();
            var vEntity = from vRecord in valXmlEntity.Descendants("GpResult")
                          select vRecord;
            foreach (XElement vItem in vEntity) {
                FacturaRapidaDetalle vRecord = new FacturaRapidaDetalle();
                vRecord.Clear();
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("ConsecutivoCompania"), null))) {
                    vRecord.ConsecutivoCompania = LibConvert.ToInt(vItem.Element("ConsecutivoCompania"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("NumeroFactura"), null))) {
                    vRecord.NumeroFactura = vItem.Element("NumeroFactura").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("TipoDeDocumento"), null))) {
                    vRecord.TipoDeDocumento = vItem.Element("TipoDeDocumento").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("ConsecutivoRenglon"), null))) {
                    vRecord.ConsecutivoRenglon = LibConvert.ToInt(vItem.Element("ConsecutivoRenglon"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Articulo"), null))) {
                    vRecord.Articulo = vItem.Element("Articulo").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Descripcion"), null))) {
                    vRecord.Descripcion = vItem.Element("Descripcion").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("CodigoVendedor1"), null))) {
                    vRecord.CodigoVendedor1 = vItem.Element("CodigoVendedor1").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("CodigoVendedor2"), null))) {
                    vRecord.CodigoVendedor2 = vItem.Element("CodigoVendedor2").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("CodigoVendedor3"), null))) {
                    vRecord.CodigoVendedor3 = vItem.Element("CodigoVendedor3").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("AlicuotaIVA"), null))) {
                    vRecord.AlicuotaIVA = vItem.Element("AlicuotaIVA").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Cantidad"), null))) {
                    vRecord.Cantidad = LibConvert.ToDec(vItem.Element("Cantidad"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("PrecioSinIVA"), null))) {
                    vRecord.PrecioSinIVA = LibConvert.ToDec(vItem.Element("PrecioSinIVA"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("PrecioConIVA"), null))) {
                    vRecord.PrecioConIVA = LibConvert.ToDec(vItem.Element("PrecioConIVA"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("PorcentajeDescuento"), null))) {
                    vRecord.PorcentajeDescuento = LibConvert.ToDec(vItem.Element("PorcentajeDescuento"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("TotalRenglon"), null))) {
                    vRecord.TotalRenglon = LibConvert.ToDec(vItem.Element("TotalRenglon"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("PorcentajeBaseImponible"), null))) {
                    vRecord.PorcentajeBaseImponible = LibConvert.ToDec(vItem.Element("PorcentajeBaseImponible"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Serial"), null))) {
                    vRecord.Serial = vItem.Element("Serial").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Rollo"), null))) {
                    vRecord.Rollo = vItem.Element("Rollo").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("PorcentajeAlicuota"), null))) {
                    vRecord.PorcentajeAlicuota = LibConvert.ToDec(vItem.Element("PorcentajeAlicuota"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("CampoExtraEnRenglonFactura1"), null))) {
                    vRecord.CampoExtraEnRenglonFactura1 = vItem.Element("CampoExtraEnRenglonFactura1").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("CampoExtraEnRenglonFactura2"), null))) {
                    vRecord.CampoExtraEnRenglonFactura2 = vItem.Element("CampoExtraEnRenglonFactura2").Value;
                }
                vResult.Add(vRecord);
            }
            return vResult;
        }
        */
        #endregion //Codigo Ejemplo


    } //End of class clsFacturaRapidaDetalleNav

} //End of namespace Galac.Adm.Brl.Venta

