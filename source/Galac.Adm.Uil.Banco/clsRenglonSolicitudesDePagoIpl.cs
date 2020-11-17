using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using LibGalac.Aos.Base;
using Galac.Adm.Ccl.Banco;

namespace Galac.Adm.Uil.Banco {
    internal class clsRenglonSolicitudesDePagoIpl {
        #region Variables
        IList<RenglonSolicitudesDePago> _ListRenglonSolicitudesDePago;
        #endregion //Variables
        #region Propiedades

        public IList<RenglonSolicitudesDePago> ListRenglonSolicitudesDePago {
            get { return _ListRenglonSolicitudesDePago; }
            set { _ListRenglonSolicitudesDePago = value; }
        }
        #endregion //Propiedades
        #region Constructores
        #endregion //Constructores
        #region Metodos Generados

        internal void ParseDetailToList(XElement valItemMaster, ref GBindingList<RenglonSolicitudesDePago> refDetailList) {
            foreach (XElement vItemDetail in valItemMaster.Descendants("GpDetailRenglonSolicitudesDePago")) {
                RenglonSolicitudesDePago insDetail = new RenglonSolicitudesDePago();
                insDetail.ConsecutivoCompania = LibConvert.ToInt(vItemDetail.Element("ConsecutivoCompania"));
                insDetail.ConsecutivoSolicitud = LibConvert.ToInt(vItemDetail.Element("ConsecutivoSolicitud"));
                insDetail.consecutivoRenglon = LibConvert.ToInt(vItemDetail.Element("consecutivoRenglon"));
                insDetail.CuentaBancaria = vItemDetail.Element("CuentaBancaria").Value;
                insDetail.ConsecutivoBeneficiario = LibConvert.ToInt(vItemDetail.Element("ConsecutivoBeneficiario"));
                insDetail.FormaDePago = vItemDetail.Element("FormaDePago").Value;
                insDetail.Status = vItemDetail.Element("Status").Value;
                insDetail.Monto = LibConvert.ToDec(vItemDetail.Element("Monto"), 2);
                insDetail.NumeroDocumento = vItemDetail.Element("NumeroDocumento").Value;
                insDetail.Contabilizado = vItemDetail.Element("Contabilizado").Value;
                refDetailList.Add(insDetail);
            }
        }

        internal XElement ElementDetail(SolicitudesDePago valMaster) {
            XElement vXElement = new XElement("GpDataRenglonSolicitudesDePago",
                from vEntity in valMaster.DetailRenglonSolicitudesDePago
                select new XElement("GpDetailRenglonSolicitudesDePago",
                    new XElement("ConsecutivoCompania", valMaster.ConsecutivoCompania),
                    new XElement("ConsecutivoSolicitud", valMaster.ConsecutivoSolicitud),
                    new XElement("consecutivoRenglon", vEntity.consecutivoRenglon),
                    new XElement("CuentaBancaria", vEntity.CuentaBancaria),
                    new XElement("ConsecutivoBeneficiario", LibConvert.ToStr(vEntity.ConsecutivoBeneficiario)),
                    new XElement("FormaDePago", vEntity.FormaDePagoAsEnum),
                    new XElement("Status", vEntity.StatusAsEnum),
                    new XElement("Monto", LibConvert.ToStr(vEntity.Monto)),
                    new XElement("NumeroDocumento", vEntity.NumeroDocumento),
                    new XElement("Contabilizado", vEntity.ContabilizadoAsBool)));
            return vXElement;
        }
        #endregion //Metodos Generados


    } //End of class clsRenglonSolicitudesDePagoIpl

} //End of namespace Galac.Dbo.Uil.RenglonSolicitudesDePago

