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
    public partial class clsCobroDeFacturaRapidaDetalleNav: LibBaseNavDetail<IList<CobroDeFacturaRapidaDetalle>, IList<CobroDeFacturaRapidaDetalle>> {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores

        public clsCobroDeFacturaRapidaDetalleNav() {
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override ILibDataDetailComponent<IList<CobroDeFacturaRapidaDetalle>, IList<CobroDeFacturaRapidaDetalle>> GetDataInstance() {
            return new Galac.Adm.Dal.Venta.clsCobroDeFacturaRapidaDetalleDat();
        }
        #endregion //Metodos Generados
        #region Codigo Ejemplo
        /* Codigo de Ejemplo

        bool ICobroDeFacturaRapidaDetallePdn.InsertDefaultRecord(int valConsecutivoCompania) {
            ILibDataComponent<IList<CobroDeFacturaRapidaDetalle>, IList<CobroDeFacturaRapidaDetalle>> instanciaDal = new clsCobroDeFacturaRapidaDetalleDat();
            IList<CobroDeFacturaRapidaDetalle> vLista = new List<CobroDeFacturaRapidaDetalle>();
            CobroDeFacturaRapidaDetalle vCurrentRecord = new Galac.Adm.Dal.VentaCobroDeFacturaRapidaDetalle();
            vCurrentRecord.ConsecutivoCompania = valConsecutivoCompania;
            vCurrentRecord.ConsecutivoCompania = 0;
            vCurrentRecord.CodigoFormaDelCobro = "";
            vCurrentRecord.MontoEfectivo = 0;
            vCurrentRecord.MontoCheque = 0;
            vCurrentRecord.MontoTarjeta = 0;
            vCurrentRecord.MontoDeposito = 0;
            vCurrentRecord.MontoAnticipo = 0;
            vLista.Add(vCurrentRecord);
            return instanciaDal.Insert(vLista).Success;
        }

        private List<CobroDeFacturaRapidaDetalle> ParseToListEntity(XElement valXmlEntity) {
            List<CobroDeFacturaRapidaDetalle> vResult = new List<CobroDeFacturaRapidaDetalle>();
            var vEntity = from vRecord in valXmlEntity.Descendants("GpResult")
                          select vRecord;
            foreach (XElement vItem in vEntity) {
                CobroDeFacturaRapidaDetalle vRecord = new CobroDeFacturaRapidaDetalle();
                vRecord.Clear();
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("ConsecutivoCompania"), null))) {
                    vRecord.ConsecutivoCompania = LibConvert.ToInt(vItem.Element("ConsecutivoCompania"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("CodigoFormaDelCobro"), null))) {
                    vRecord.CodigoFormaDelCobro = vItem.Element("CodigoFormaDelCobro").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("MontoEfectivo"), null))) {
                    vRecord.MontoEfectivo = LibConvert.ToDec(vItem.Element("MontoEfectivo"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("MontoCheque"), null))) {
                    vRecord.MontoCheque = LibConvert.ToDec(vItem.Element("MontoCheque"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("MontoTarjeta"), null))) {
                    vRecord.MontoTarjeta = LibConvert.ToDec(vItem.Element("MontoTarjeta"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("MontoDeposito"), null))) {
                    vRecord.MontoDeposito = LibConvert.ToDec(vItem.Element("MontoDeposito"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("MontoAnticipo"), null))) {
                    vRecord.MontoAnticipo = LibConvert.ToDec(vItem.Element("MontoAnticipo"));
                }
                vResult.Add(vRecord);
            }
            return vResult;
        }
        */
        #endregion //Codigo Ejemplo


    } //End of class clsCobroDeFacturaRapidaDetalleNav

} //End of namespace Galac.Adm.Brl.Venta

