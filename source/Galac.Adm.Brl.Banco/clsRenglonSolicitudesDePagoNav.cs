using System;
using System.Collections.Generic;
using System.Text;
using System.Security;
using System.Security.Permissions;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.Brl;
using Galac.Adm.Ccl.Banco;
using System.Xml.Linq;
using System.Linq;
using LibGalac.Aos.Catching;

namespace Galac.Adm.Brl.Banco {
    public partial class clsRenglonSolicitudesDePagoNav : LibBaseNavDetail<IList<RenglonSolicitudesDePago>, IList<RenglonSolicitudesDePago>> {
        #region Variables
        XElement _ListadoBeneficiarios;
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores

        public clsRenglonSolicitudesDePagoNav() {
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override ILibDataDetailComponent<IList<RenglonSolicitudesDePago>, IList<RenglonSolicitudesDePago>> GetDataInstance() {
            return new Galac.Adm.Dal.Banco.clsRenglonSolicitudesDePagoDat();
        }
        #endregion //Metodos Generados

        public void ParseDetailToList(XElement valItemMaster, XElement valListadoBeneficiarios, int valConsecutivoCompania, ref GBindingList<RenglonSolicitudesDePago> refDetailList, string valCuentaBancariaGenerica) {
            IList<RenglonSolicitudesDePago> vRenglonSolicitudesDePago = new List<RenglonSolicitudesDePago>();
            _ListadoBeneficiarios = valListadoBeneficiarios;
            foreach (XElement vItemDetail in valItemMaster.Descendants("GpDetailRenglonSolicitudesDePago")) {
                RenglonSolicitudesDePago insDetail = new RenglonSolicitudesDePago();
                insDetail.ConsecutivoCompania = valConsecutivoCompania;
                insDetail.CuentaBancaria = "";
                if (!(System.NullReferenceException.ReferenceEquals(vItemDetail.Element("CedulaDeIdentidad"), null))) {
                    insDetail.ConsecutivoBeneficiario = GetConsecutivoBeneficiario(valConsecutivoCompania, vItemDetail.Element("CedulaDeIdentidad").Value, false);
                } else {
                    insDetail.ConsecutivoBeneficiario = GetConsecutivoBeneficiario(valConsecutivoCompania, "", true);
                }
                insDetail.FormaDePagoAsEnum = ToEnumFromFormaDePagoSolicitud(vItemDetail.Element("FormaDePago").Value);
                insDetail.StatusAsEnum = eStatusSolicitudRenglon.PorProcesar;
                insDetail.Monto = LibConvert.ToDec(vItemDetail.Element("Monto"), 2);
                insDetail.NumeroDocumento = "";
                insDetail.CuentaBancaria = valCuentaBancariaGenerica;
                insDetail.ContabilizadoAsBool = false;
                if (System.NullReferenceException.ReferenceEquals(vItemDetail.Element("Moneda"), null)) {
                    insDetail.CodigoMoneda = CodigoMonedaValido(string.Empty);
                    insDetail.TasaDeCambio = 1;
                }else{
                    insDetail.CodigoMoneda = CodigoMonedaValido(vItemDetail.Element("Moneda").Value);
                    insDetail.TasaDeCambio = TasaDeCambioValida(LibConvert.ToDec(vItemDetail.Element("TasaDeCambio"), 4));
                }
                refDetailList.Add(insDetail);
            }
        }

        private int GetConsecutivoBeneficiario(int valConsecutivoCompania, string valRif, bool valTraerBeneficiarioGenerico) {
            int vResult = 0;
            IBeneficiarioPdn vPdnModule;
            vPdnModule = new Galac.Adm.Brl.Banco.clsBeneficiarioNav();
            if (valTraerBeneficiarioGenerico) {
                vResult = vPdnModule.ConsecutivoBeneficiarioGenerico(valConsecutivoCompania);
            } else {
                vResult = GetConsecutivoBeneficiarioDeLaLista(valRif);
            }
            return vResult;
        }

        private int GetConsecutivoBeneficiarioDeLaLista(string valRif) {
            int vResult = 0;
            var vEntity = from vRecord in _ListadoBeneficiarios.Descendants("GpResult")
                          where LibConvert.ToStr(vRecord.Element("NumeroRIF").Value) == valRif
                          select vRecord;
            foreach (XElement vItem in vEntity) {
                vResult = LibConvert.ToInt(vItem.Element("Consecutivo").Value);
            }
            return vResult;
        }
		
        private eTipoDeFormaDePagoSolicitud ToEnumFromFormaDePagoSolicitud(string valValor) {
            switch (valValor) {
                case "Cheque": return eTipoDeFormaDePagoSolicitud.Cheque;
                case "Efectivo": return eTipoDeFormaDePagoSolicitud.Efectivo;
                case "Transferencia": return eTipoDeFormaDePagoSolicitud.transferencia;
                default:
                    return eTipoDeFormaDePagoSolicitud.Cheque;
            }
        }

        private string CodigoMonedaValido(string valCodigoMoneda) {
            string vResult = string.Empty;
            if (LibString.IsNullOrEmpty(valCodigoMoneda, true)) {
                vResult = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Compania", "CodigoMoneda");
                if (LibString.IsNullOrEmpty(vResult, true)) {
                    if (LibGalac.Aos.DefGen.LibDefGen.ProgramInfo.IsCountryEcuador()) {
                        vResult = "USD";
                    } else {
                        vResult = "VES";
                    }
                }
            } else {
                vResult = valCodigoMoneda;
            }
            return vResult;
        }

        private decimal TasaDeCambioValida(decimal valTasaDeCambio) {
            Decimal vResult = 1;
            if (valTasaDeCambio != 0) {
                vResult = valTasaDeCambio;
            }
            return vResult;
        }
    } //End of class clsRenglonSolicitudesDePagoNav

} //End of namespace Galac.Dbo.Brl.RenglonSolicitudesDePago

