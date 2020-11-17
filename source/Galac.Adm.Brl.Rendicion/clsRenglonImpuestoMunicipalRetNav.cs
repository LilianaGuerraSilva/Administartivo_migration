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
using Galac.Adm.Ccl.CajaChica;

namespace Galac.Adm.Brl.CajaChica {
    public partial class clsRenglonImpuestoMunicipalRetNav: LibBaseNavDetail<IList<RenglonImpuestoMunicipalRet>, IList<RenglonImpuestoMunicipalRet>> {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores

        public clsRenglonImpuestoMunicipalRetNav() {
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override ILibDataDetailComponent<IList<RenglonImpuestoMunicipalRet>, IList<RenglonImpuestoMunicipalRet>> GetDataInstance() {
            return new Galac.Adm.Dal.CajaChica.clsRenglonImpuestoMunicipalRetDat();
        }
        #endregion //Metodos Generados
        /* Codigo de Ejemplo

        bool IRenglonImpuestoMunicipalRetPdn.InsertarRegistroPorDefecto(int valConsecutivoCompania) {
            ILibDataComponent<IList<RenglonImpuestoMunicipalRet>, IList<RenglonImpuestoMunicipalRet>> instanciaDal = new clsRenglonImpuestoMunicipalRetDat();
            IList<RenglonImpuestoMunicipalRet> vLista = new List<RenglonImpuestoMunicipalRet>();
            RenglonImpuestoMunicipalRet vCurrentRecord = new RenglonImpuestoMunicipalRet();
            vCurrentRecord.ConsecutivoCompania = valConsecutivoCompania;
            vCurrentRecord.ConsecutivoCompania = 0;
            vCurrentRecord.Consecutivo = 0;
            vCurrentRecord.ConsecutivoCxp = 0;
            vCurrentRecord.CodigoRetencion = "";
            vCurrentRecord.MontoBaseImponible = 0;
            vCurrentRecord.AlicuotaRetencion = 0;
            vCurrentRecord.MontoRetencion = 0;
            vCurrentRecord.TipoDeTransaccionAsEnum = eTipoDeTransaccionMunicipal.NoAplica;
            vCurrentRecord.NombreOperador = "";
            vCurrentRecord.FechaUltimaModificacion = LibDate.Today();
            vLista.Add(vCurrentRecord);
            return instanciaDal.Insert(vLista).Success;
        }

        private List<RenglonImpuestoMunicipalRet> ParseToListEntity(XElement valXmlEntity) {
            List<RenglonImpuestoMunicipalRet> vResult = new List<RenglonImpuestoMunicipalRet>();
            var vEntity = from vRecord in valXmlEntity.Descendants("GpResult")
                          select vRecord;
            foreach (XElement vItem in vEntity) {
                RenglonImpuestoMunicipalRet vRecord = new RenglonImpuestoMunicipalRet();
                vRecord.Clear();
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("ConsecutivoCompania"), null))) {
                    vRecord.ConsecutivoCompania = LibConvert.ToInt(vItem.Element("ConsecutivoCompania"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Consecutivo"), null))) {
                    vRecord.Consecutivo = LibConvert.ToInt(vItem.Element("Consecutivo"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("ConsecutivoCxp"), null))) {
                    vRecord.ConsecutivoCxp = LibConvert.ToInt(vItem.Element("ConsecutivoCxp"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("CodigoRetencion"), null))) {
                    vRecord.CodigoRetencion = vItem.Element("CodigoRetencion").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("MontoBaseImponible"), null))) {
                    vRecord.MontoBaseImponible = LibConvert.ToDec(vItem.Element("MontoBaseImponible"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("AlicuotaRetencion"), null))) {
                    vRecord.AlicuotaRetencion = LibConvert.ToDec(vItem.Element("AlicuotaRetencion"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("MontoRetencion"), null))) {
                    vRecord.MontoRetencion = LibConvert.ToDec(vItem.Element("MontoRetencion"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("TipoDeTransaccion"), null))) {
                    vRecord.TipoDeTransaccion = vItem.Element("TipoDeTransaccion").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("NombreOperador"), null))) {
                    vRecord.NombreOperador = vItem.Element("NombreOperador").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("FechaUltimaModificacion"), null))) {
                    vRecord.FechaUltimaModificacion = LibConvert.ToDate(vItem.Element("FechaUltimaModificacion"));
                }
                vResult.Add(vRecord);
            }
            return vResult;
        }
        */


    } //End of class clsRenglonImpuestoMunicipalRetNav

} //End of namespace Galac.Dbo.Brl.CajaChica

