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
    public partial class clsCobroDeFacturaRapidaAnticipoDetalleNav : LibBaseNavDetail<IList<CobroDeFacturaRapidaAnticipoDetalle>, IList<CobroDeFacturaRapidaAnticipoDetalle>> {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores

        public clsCobroDeFacturaRapidaAnticipoDetalleNav() {
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override ILibDataDetailComponent<IList<CobroDeFacturaRapidaAnticipoDetalle>, IList<CobroDeFacturaRapidaAnticipoDetalle>> GetDataInstance() {
            return new Galac.Adm.Dal.Venta.clsCobroDeFacturaRapidaAnticipoDetalleDat();
        }
    }
}
        #endregion //Metodos Generados
        #region Codigo Ejemplo
        /* Codigo de Ejemplo

        bool ICobroDeFacturaRapidaDepositoTransfDetallePdn.InsertDefaultRecord(int valConsecutivoCompania) {
            ILibDataComponent<IList<CobroDeFacturaRapidaDepositoTransfDetalle>, IList<CobroDeFacturaRapidaDepositoTransfDetalle>> instanciaDal = new clsCobroDeFacturaRapidaDepositoTransfDetalleDat();
            IList<CobroDeFacturaRapidaDepositoTransfDetalle> vLista = new List<CobroDeFacturaRapidaDepositoTransfDetalle>();
            CobroDeFacturaRapidaDepositoTransfDetalle vCurrentRecord = new Galac.Adm.Dal.VentaCobroDeFacturaRapidaDepositoTransfDetalle();
            vCurrentRecord.ConsecutivoCompania = valConsecutivoCompania;
            vCurrentRecord.ConsecutivoCompania = 0;
            vCurrentRecord.CodigoFormaDelCobro = "";
            vCurrentRecord.NumeroDelDocumento = "";
            vCurrentRecord.CodigoBanco = 0;
            vCurrentRecord.Monto = 0;
            vCurrentRecord.NumeroDocumentoAprobacion = "";
            vLista.Add(vCurrentRecord);
            return instanciaDal.Insert(vLista).Success;
        }

        private List<CobroDeFacturaRapidaDepositoTransfDetalle> ParseToListEntity(XElement valXmlEntity) {
            List<CobroDeFacturaRapidaDepositoTransfDetalle> vResult = new List<CobroDeFacturaRapidaDepositoTransfDetalle>();
            var vEntity = from vRecord in valXmlEntity.Descendants("GpResult")
                          select vRecord;
            foreach (XElement vItem in vEntity) {
                CobroDeFacturaRapidaDepositoTransfDetalle vRecord = new CobroDeFacturaRapidaDepositoTransfDetalle();
                vRecord.Clear();
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("ConsecutivoCompania"), null))) {
                    vRecord.ConsecutivoCompania = LibConvert.ToInt(vItem.Element("ConsecutivoCompania"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("CodigoFormaDelCobro"), null))) {
                    vRecord.CodigoFormaDelCobro = vItem.Element("CodigoFormaDelCobro").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("NumeroDelDocumento"), null))) {
                    vRecord.NumeroDelDocumento = vItem.Element("NumeroDelDocumento").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("CodigoBanco"), null))) {
                    vRecord.CodigoBanco = LibConvert.ToInt(vItem.Element("CodigoBanco"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Monto"), null))) {
                    vRecord.Monto = LibConvert.ToDec(vItem.Element("Monto"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("NumeroDocumentoAprobacion"), null))) {
                    vRecord.NumeroDocumentoAprobacion = vItem.Element("NumeroDocumentoAprobacion").Value;
                }
                vResult.Add(vRecord);
            }
            return vResult;
        }
        */
        #endregion //Codigo Ejemplo