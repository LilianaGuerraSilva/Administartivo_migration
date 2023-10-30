using Galac.Adm.Ccl.DispositivosExternos;
using Galac.Adm.Ccl.Venta;
using Galac.Adm.Brl.Venta;
using Galac.Adm.Uil.DispositivosExternos.ViewModel;
using Galac.Saw.Ccl.Inventario;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using LibGalac.Aos.UI.Mvvm.Messaging;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System;
using System.Reflection;

namespace Galac.Adm.Uil.Venta.ViewModel {
    public abstract class CobroRapidoVzlaViewModelBase : CobroRapidoViewModelBase {
        #region Variables
        private XElement _XmlDatosImprFiscal;
        protected int _AlicuotaIvaASustituir;
        protected Saw.Lib.clsNoComunSaw _MonedaLocalNav = null;
        #endregion

        #region Propiedades
        protected override bool CanExecuteCobrarCommand() {
            return XmlDatosImprFiscal != null;
        }

        public XElement XmlDatosImprFiscal {
            get {
                return _XmlDatosImprFiscal;
            }
            set {
                _XmlDatosImprFiscal = value;
            }
        }
        #endregion

        #region Metodos
        public bool ImprimirFacturaFiscal(List<RenglonCobroDeFactura> valListDeCobro) {
            bool vResult = true;
            string vSerialMaquinaFiscal = "";
            string vComprobanteFiscal = "";
            try {
                XElement xElementFacturaRapida = DarFormatoADatosDeFactura(insFactura, valListDeCobro);
                if (vResult) {
                    vResult = false;
                    ImpresoraFiscalViewModel insImpresoraFiscalViewModel = new ImpresoraFiscalViewModel(_XmlDatosImprFiscal, xElementFacturaRapida, eTipoDocumentoFiscal.FacturaFiscal);
                    LibMessages.EditViewModel.ShowEditor(insImpresoraFiscalViewModel, true);
                    vSerialMaquinaFiscal = insImpresoraFiscalViewModel.SerialImpresoraFiscal;
                    vComprobanteFiscal = insImpresoraFiscalViewModel.NumeroComprobante;
                    vResult = insImpresoraFiscalViewModel.SeImprimioDocumento;
                }
                if (vResult) {
                    ActualizarCamposEnFactura(xElementFacturaRapida, vComprobanteFiscal, vSerialMaquinaFiscal, valListDeCobro);
                }
            } catch (GalacException vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
            }
            return vResult;
        }      

        private XElement DarFormatoADatosDeFactura(FacturaRapida valFactura, List<RenglonCobroDeFactura> valListDeCobro) {
            _MonedaLocalNav = new Saw.Lib.clsNoComunSaw();
            List<RenglonCobroDeFactura> vCloneListCobro = valListDeCobro.Select(t => t.Clone()).ToList();           
            XElement vResult = null;
            XElement xElementFacturaRapida = LibParserHelper.ParseToXElement(valFactura);
            XElement xElementGpDataDetailRenglonFactura = new XElement("GpDataDetailRenglonFactura");            
            bool vAplicaIvaEspecial = LibConvert.SNToBool(LibXml.GetPropertyString(xElementFacturaRapida, "AplicaDecretoIvaEspecial"));
            int vParametro = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("FacturaRapida", "AplicarIVAEspecial");
                        
            foreach (var item in valFactura.DetailFacturaRapidaDetalle) {
                item.PrecioSinIVA = LibMath.RoundToNDecimals(item.PrecioSinIVA * valFactura.CambioABolivares, 2);
                XElement xDetail = DetailParseToXElement(item);
                XElement xElementGpResultDetailRenglonFactura = new XElement("GpResultDetailRenglonFactura");
                if (vAplicaIvaEspecial) {
                    var detalle = xDetail.Descendants("GpResult")
                        .Where(s => (eTipoDeAlicuota)LibConvert.ToInt(s.Element("AlicuotaIva").Value) == eTipoDeAlicuota.AlicuotaGeneral)
                        .FirstOrDefault();
                    if (detalle != null) {
                        detalle.Element("AlicuotaIva").SetValue(LibConvert.EnumToDbValue((int)_AlicuotaIvaASustituir));
                    }
                }
                xElementGpResultDetailRenglonFactura.Add(xDetail.Descendants("GpResult").Elements());
                xElementGpDataDetailRenglonFactura.Add(xElementGpResultDetailRenglonFactura);
            }
            DarFormatoASerialyRollo(xElementGpDataDetailRenglonFactura);
            xElementFacturaRapida.Element("GpResult").Add(xElementGpDataDetailRenglonFactura);
            XElement xElementGpDataDetailRenglonCobro = new XElement("GpDataDetailRenglonCobro");
            foreach (var renglon in vCloneListCobro.Where(x => x.CodigoMoneda != _MonedaLocalNav.InstanceMonedaLocalActual.GetHoyCodigoMoneda())) {
                renglon.Monto = LibMath.RoundToNDecimals(renglon.Monto * renglon.CambioAMonedaLocal,2);
            }          

            foreach (var item in vCloneListCobro) {
                XElement xDetail = LibParserHelper.ParseToXElement(item);
                XElement xElementGpResultDetailRenglonCobro = new XElement("GpResultDetailRenglonCobro");
                xElementGpResultDetailRenglonCobro.Add(xDetail.Descendants("GpResult").Elements());
                xElementGpDataDetailRenglonCobro.Add(xElementGpResultDetailRenglonCobro);
            }
            if (valFactura.CodigoMonedaDeCobro != _MonedaLocalNav.InstanceMonedaLocalActual.GetHoyCodigoMoneda()) {
                string vTotalesEnDivisas = MostrarTotalesEnDivisasImprFiscal(valFactura);
                XElement xTotalesEnDivisa = new XElement("TotalMonedaExtranjera", vTotalesEnDivisas);
                xElementFacturaRapida.Element("GpResult").Add(xTotalesEnDivisa);
            }
            xElementFacturaRapida.Element("GpResult").Add(xElementGpDataDetailRenglonCobro);
            DarFormatoAFechasEnFactura(xElementFacturaRapida, insFactura);
            vResult = xElementFacturaRapida;
            return vResult;
        }

        private string MostrarTotalesEnDivisasImprFiscal(FacturaRapida valFactura) {
            StringBuilder vResult = new StringBuilder();
            FkMonedaViewModel MonedaDeCobro;
            string vTexto = "";
            string vSimboloMonedaMe = "";
            decimal vCambioEnBs = valFactura.CambioMostrarTotalEnDivisas;
            decimal vBaseImponibleMEAlicuotaGeneral = 0;
            decimal vBaseImponibleMEAlicuotaReducida = 0;
            decimal vBaseImponibleMEAlicuotaAdicional = 0;
            decimal vMontoIvaMEAlicuotaGeneral = 0;
            decimal vMontoIvaMEAlicuotaReducida = 0;
            decimal vMontoIvaMEAlicuotaAdicional = 0;
            decimal vMontoExento = 0;
            decimal vTotalFactura = 0;

            try {
                MonedaDeCobro = new FacturaRapidaViewModel().FirstConnectionRecordOrDefault<FkMonedaViewModel>("Moneda", LibSearchCriteria.CreateCriteriaFromText("Codigo", valFactura.CodigoMonedaDeCobro));
                if (MonedaDeCobro != null) {
                    vSimboloMonedaMe = MonedaDeCobro.Simbolo + " ";
                }
                bool vDivisaMonedaPredeterminada = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "UsaMonedaExtranjera") &&
                                                           LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "UsaDivisaComoMonedaPrincipalDeIngresoDeDatos");
                vResult.AppendLine("Cambio del día " + LibConvert.NumToString(vCambioEnBs, 2));
                vCambioEnBs = vDivisaMonedaPredeterminada ? 1 : vCambioEnBs;
                if (valFactura.TotalMontoExento > 0) {
                    vMontoExento = LibMath.RoundToNDecimals(valFactura.TotalMontoExento / vCambioEnBs,2);
                    vTexto = DarFormatoATotalesEnDivisaImprFiscal(LibConvert.NumToString(vMontoExento, 2), vSimboloMonedaMe, "Exento", eTipoDeAlicuota.Exento, false);
                    vResult.AppendLine(vTexto);
                }
                if (valFactura.MontoGravableAlicuota1 > 0) {
                    vBaseImponibleMEAlicuotaGeneral = LibMath.RoundToNDecimals(valFactura.MontoGravableAlicuota1 / vCambioEnBs,2);
                    vMontoIvaMEAlicuotaGeneral = LibMath.RoundToNDecimals(valFactura.MontoIvaAlicuota1 / vCambioEnBs,2);
                    vTexto = DarFormatoATotalesEnDivisaImprFiscal(LibConvert.NumToString(vBaseImponibleMEAlicuotaGeneral, 2), vSimboloMonedaMe, LibConvert.NumToString(valFactura.PorcentajeAlicuota1, 2), eTipoDeAlicuota.AlicuotaGeneral, true);
                    vResult.AppendLine(vTexto);
                    vTexto = DarFormatoATotalesEnDivisaImprFiscal(LibConvert.NumToString(vMontoIvaMEAlicuotaGeneral, 2), vSimboloMonedaMe, LibConvert.NumToString(valFactura.PorcentajeAlicuota1, 2), eTipoDeAlicuota.AlicuotaGeneral, false);
                    vResult.AppendLine(vTexto);
                }
                if (valFactura.MontoGravableAlicuota2 > 0) {
                    vBaseImponibleMEAlicuotaReducida = LibMath.RoundToNDecimals(valFactura.MontoGravableAlicuota2 / vCambioEnBs,2);
                    vMontoIvaMEAlicuotaReducida = LibMath.RoundToNDecimals(valFactura.MontoIvaAlicuota2 / vCambioEnBs,2);
                    vTexto = DarFormatoATotalesEnDivisaImprFiscal(LibConvert.NumToString(vBaseImponibleMEAlicuotaReducida, 2), vSimboloMonedaMe, LibConvert.NumToString(valFactura.PorcentajeAlicuota2, 2), eTipoDeAlicuota.Alicuota2, true);
                    vResult.AppendLine(vTexto);
                    vTexto = DarFormatoATotalesEnDivisaImprFiscal(LibConvert.NumToString(vMontoIvaMEAlicuotaReducida, 2), vSimboloMonedaMe, LibConvert.NumToString(valFactura.PorcentajeAlicuota2, 2), eTipoDeAlicuota.Alicuota2, false);
                    vResult.AppendLine(vTexto);
                }
                if (valFactura.MontoGravableAlicuota3 > 0) {
                    vBaseImponibleMEAlicuotaAdicional = LibMath.RoundToNDecimals(valFactura.MontoGravableAlicuota3 / vCambioEnBs,2);
                    vMontoIvaMEAlicuotaAdicional = LibMath.RoundToNDecimals(valFactura.MontoIvaAlicuota3 / vCambioEnBs,2);
                    vTexto = DarFormatoATotalesEnDivisaImprFiscal(LibConvert.NumToString(vBaseImponibleMEAlicuotaAdicional, 2), vSimboloMonedaMe, LibConvert.NumToString(valFactura.PorcentajeAlicuota3, 2), eTipoDeAlicuota.Alicuota3, true);
                    vResult.AppendLine(vTexto);
                    vTexto = DarFormatoATotalesEnDivisaImprFiscal(LibConvert.NumToString(vMontoIvaMEAlicuotaAdicional, 2), vSimboloMonedaMe, LibConvert.NumToString(valFactura.PorcentajeAlicuota3, 2), eTipoDeAlicuota.Alicuota3, false);
                    vResult.AppendLine(vTexto);
                }
                vTotalFactura = LibMath.RoundToNDecimals(valFactura.TotalFactura / vCambioEnBs,2);
                vTexto = DarFormatoATotalesEnDivisaImprFiscal(LibConvert.NumToString(vTotalFactura, 2), vSimboloMonedaMe, "Total Factura", eTipoDeAlicuota.Exento, false);
                vResult.AppendLine(vTexto);
            } catch (System.Exception) {
                throw;
            }
            return vResult.ToString();
        }

        private string DarFormatoATotalesEnDivisaImprFiscal(string valMonto, string valSimboloMoneda, string valPorcentajeAlicuota, eTipoDeAlicuota valAlicuota, bool valEsBaseImponibleOIva) {
            string TextoSalida = "";
            int FinTexto;
            string vTexto = "";
            int InicioMonto;
            string vSimboloAlicuota = ((valAlicuota == eTipoDeAlicuota.Exento) ? "" : (valAlicuota == eTipoDeAlicuota.AlicuotaGeneral) ? "G " : (valAlicuota == eTipoDeAlicuota.Alicuota2) ? "R " : (valAlicuota == eTipoDeAlicuota.Alicuota3) ? "A " : "");
            string vSimboloItem = (valAlicuota == eTipoDeAlicuota.Exento) ? "" : (valEsBaseImponibleOIva ? "BI  " : "IVA ");
            vTexto = vSimboloItem + vSimboloAlicuota + valPorcentajeAlicuota + " " + (valAlicuota == eTipoDeAlicuota.Exento ? "" : "%") + " " + valSimboloMoneda;
            TextoSalida = LibString.NCar('\u0020', 40);
            FinTexto = LibString.Len(vTexto);
            InicioMonto = LibString.Len(valMonto);
            TextoSalida = vTexto + LibString.SubString(TextoSalida, FinTexto, 40);
            TextoSalida = LibString.SubString(TextoSalida, 0, 40 - InicioMonto) + valMonto;
            return TextoSalida;
        }

        private void DarFormatoAFechasEnFactura(XElement valXlmFacturaRapida, FacturaRapida valFacturaRapida) {
            var vXml = valXlmFacturaRapida.Descendants("GpResult");
            foreach (var vItem in vXml) {
                XElement vXElemet = vItem.Element("Fecha");
                if (vXElemet != null) {
                    vXElemet.Value = LibConvert.ToStr(valFacturaRapida.Fecha);
                }
            }
        }

        private void DarFormatoASerialyRollo(XElement valXlmRenglonFactura) {
            var vXml = valXlmRenglonFactura.Descendants("GpResultDetailRenglonFactura");
            foreach (var vItem in vXml) {
                XElement vXElemet = vItem.Element("Rollo");
                if (vXElemet.Value == "0") {
                    vXElemet.Value = "";
                }
                vXElemet = vItem.Element("Serial");
                if (vXElemet.Value == "0") {
                    vXElemet.Value = "";
                }
            }
        }

        static XElement DetailParseToXElement(object valObject) {
            XElement vResult = new XElement("GpData");
            XElement vGpResult = new XElement("GpResult");
            object vValue = null;
            if (valObject != null) {
                PropertyInfo[] vProperties = valObject.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.CanRead && p.CanWrite && !LibString.S1IsEqualToS2("fldTimeStamp", p.Name)).ToArray();
                foreach (PropertyInfo vProperty in vProperties) {
                    if (vProperty.PropertyType == typeof(bool)) {
                        vValue = LibConvert.BoolToSN(LibReflection.GetPropertyValue<bool>(valObject, vProperty));
                    } else if (vProperty.PropertyType.IsEnum) {
                        vValue = LibConvert.EnumToDbValue(LibReflection.GetPropertyValue<int>(valObject, vProperty));
                    } else if (vProperty.PropertyType == typeof(decimal) || vProperty.PropertyType == typeof(double)) {
                        double vDecimalValue = LibReflection.GetPropertyValue<double>(valObject, vProperty);
                        if (LibString.S1IsEqualToS2("Cantidad", vProperty.Name)) {
                            vValue = vDecimalValue.ToString("0.000", System.Globalization.CultureInfo.InvariantCulture);
                        } else {
                            vValue = vDecimalValue.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);
                        }
                    } else {
                        vValue = LibConvert.ToStr(LibReflection.GetPropertyValue<object>(valObject, vProperty));
                    }
                    if (vValue != null) {
                        string vName = LibString.Replace(vProperty.Name, "AsEnum", "");
                        vName = LibString.Replace(vName, "AsBool", "");
                        vName = LibString.Replace(vName, "AsStr", "");
                        vGpResult.Add(new XElement(vName, vValue));
                    }
                }
                vResult.Add(vGpResult);
            }
            return vResult;
        }
        #endregion    
    }
}

