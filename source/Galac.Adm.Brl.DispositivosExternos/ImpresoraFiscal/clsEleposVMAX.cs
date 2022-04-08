using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using AxVMAXOCX;
using System.Threading;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using Galac.Adm.Ccl.DispositivosExternos;
using Galac.Saw.Ccl.Inventario;

namespace Galac.Adm.Brl.DispositivosExternos.ImpresoraFiscal {
    public class clsEleposVMAX : IImpresoraFiscalPdn {

        private AxVMAX mVMax;
        private byte _DecimalParaCantidad = 3;
        private byte _DecimalParaMonto = 2;
        private byte _EnterosParaMonto = 10;
        private byte _EnterosParaCantidad = 5;
        private bool _PuertoEstaAbierto = false;


        public clsEleposVMAX(XElement valXmlDatosImpresora) {
            ConfigurarImpresora(valXmlDatosImpresora);
        }

        private void ConfigurarImpresora(XElement valXmlDatosImpresora) {
            mVMax = new AxVMAX();
            mVMax.CreateControl();
            string vNPuertoCom = LibXml.GetPropertyString(valXmlDatosImpresora, "PuertoMaquinaFiscal");
            string vNPuertoVisor = LibXml.GetPropertyString(valXmlDatosImpresora, "Puerto");
            short vValorPuertoCom = short.Parse(vNPuertoCom);
            mVMax.NPuerto = vValorPuertoCom;
            mVMax.NPuertoVisor = vNPuertoVisor;
        }

        public string ObtenerFechaYHora() {
            string vResult = "";
            string vFecha = "";
            string vHora = "";

            try {
                mVMax.LeeDatosFiscales();
                vResult = LibText.Trim(mVMax.FechaMaquinaFiscal);
                vFecha = LibText.SubString(vResult, 0, 2) + "/" + LibText.SubString(vResult, 2, 2) + "/" + LibText.SubString(vResult, 4, 4);
                vResult = LibText.Trim(mVMax.HoraMaquinaFiscal);
                vHora = LibText.SubString(vResult, 0, 2) + ":" + LibText.SubString(vResult, 2, 2);
                vResult = vFecha + LibText.Space(1) + vHora;
            } catch (GalacException vEx) {
                throw new LibGalac.Aos.Catching.GalacException("Obtener Fecha y Hora" + vEx.Message, eExceptionManagementType.Controlled);
            }
            return vResult;
        }

        public bool AbrirConexion() {
            try {
                if (!_PuertoEstaAbierto) {
                    _PuertoEstaAbierto = mVMax.AbrirPuerto();
                    Thread.Sleep(500);
                }
                return _PuertoEstaAbierto;
            } catch (GalacException vEx) {
                _PuertoEstaAbierto = false;
                throw new GalacException("Abrir Puerto " + vEx.Message, eExceptionManagementType.Controlled);
            }
        }

        public bool CerrarConexion() {
            try {
                if (_PuertoEstaAbierto) {
                    mVMax.CerrarPuerto();
                    Thread.Sleep(500);
                    _PuertoEstaAbierto = false;
                }
                return _PuertoEstaAbierto;
            } catch (GalacException vEx) {
                _PuertoEstaAbierto = false;
                throw new GalacException("Cerrar Puerto " + vEx.Message, eExceptionManagementType.Controlled);
            }
        }

        public bool ComprobarEstado() {
            string vEstado = "";
            bool vResult = false;
            string vMensaje = "";
            try {
                mVMax.LeeEstatus();
                vEstado = mVMax.EstadoFiscal;
                vResult = CheckStatus(vEstado, ref vMensaje) && _PuertoEstaAbierto;
                if (!vResult) {
                    throw new GalacException(vMensaje, eExceptionManagementType.Controlled);
                }
            } catch (GalacException vEx) {
                throw vEx;
            }
            return vResult;
        }

        public string ObtenerSerial(bool valAbrirConexion) {
            string vSerial = "";
            try {
                if (valAbrirConexion) {
                    AbrirConexion();
                }
                mVMax.ObtDatosSerializacion();
                vSerial = mVMax.s_serial;
                if (valAbrirConexion) {
                    CerrarConexion();
                }
                return vSerial;
            } catch (GalacException vEx) {
                throw new GalacException("Obtener Serial " + vEx.Message, eExceptionManagementType.Controlled);
            }
        }

        public eStatusImpresorasFiscales EstadoDelPapel(bool valAbrirPuerto) {
            eStatusImpresorasFiscales vResult = eStatusImpresorasFiscales.eListoEnEspera;
            try {
                if (valAbrirPuerto) {
                    AbrirConexion();
                }
                mVMax.LeeEstatus();
                if (mVMax.ErrorImpresora) {
                    vResult = eStatusImpresorasFiscales.eSinPapel;
                }
                if (valAbrirPuerto) {
                    CerrarConexion();
                }

            } catch (GalacException vEx) {
                throw new GalacException("Estado del Papel " + vEx.Message, eExceptionManagementType.Controlled);
            }
            return vResult;
        }

        public bool RealizarReporteZ() {
            bool vResult = false;
            string vReporteZ = "Z";
            try {
                AbrirConexion();
                vResult = mVMax.CierreDiario(ref vReporteZ);
                Thread.Sleep(250);
                CerrarConexion();
                return true;
            } catch (GalacException vEx) {
                CerrarConexion();
                throw new GalacException("Realizar Reporte Z", eExceptionManagementType.Controlled);
            }
        }

        public bool RealizarReporteX() {
            bool vResult = false;
            string vReporteX = "X";
            try {
                AbrirConexion();
                vResult = mVMax.CierreDiario(ref vReporteX);
                CerrarConexion();
                return true;
            } catch (GalacException vEx) {
                CerrarConexion();
                throw new GalacException("Realizar Reporte X", eExceptionManagementType.Controlled);
            }
        }

        public string ObtenerUltimoNumeroFactura(bool valAbrirConexion) {
            string vUltimoNumeroFactura = "";
            try {

                if (valAbrirConexion) {
                    AbrirConexion();
                }
                mVMax.LeeDatosFiscales();
                vUltimoNumeroFactura = mVMax.UltimoCFAbierto;
                Thread.Sleep(250);
                if (valAbrirConexion) {
                    CerrarConexion();
                }
                return vUltimoNumeroFactura;
            } catch (GalacException vEx) {
                CerrarConexion();
                throw new GalacException("Último Numero de Factura", eExceptionManagementType.Controlled);
            }
        }

        public string ObtenerUltimoNumeroNotaDeCredito(bool valAbrirConexion) {
            string vUltimoNumeroNotaDeCredito = "";
            try {
                if (valAbrirConexion) {
                    AbrirConexion();
                }
                mVMax.LeeDatosFiscales();
                vUltimoNumeroNotaDeCredito = mVMax.UltimaNCAbierta;
                CerrarConexion();
                Thread.Sleep(250);
                if (valAbrirConexion) {
                    CerrarConexion();
                }
                return vUltimoNumeroNotaDeCredito;
            } catch (GalacException vEx) {
                CerrarConexion();
                throw new GalacException("Último Numero Nota de Crédito", eExceptionManagementType.Controlled);
            }
        }

        public string ObtenerUltimoNumeroReporteZ(bool valAbrirConexion) {
            string vUltimoNumeroReporteZ = "";
            try {

                if (valAbrirConexion) {
                    AbrirConexion();
                }
                mVMax.LeeZ();
                vUltimoNumeroReporteZ = mVMax.rNumZ;
                if (valAbrirConexion) {
                    CerrarConexion();
                }
                return vUltimoNumeroReporteZ;
            } catch (GalacException vEx) {
                CerrarConexion();
                throw new GalacException("Último Numero Nota de Crédito", eExceptionManagementType.Controlled);
            }
        }

        public bool CancelarDocumentoFiscalEnImpresion(bool valAbrirConexion) {
            bool vResult;
            try {
                if (valAbrirConexion) {
                    AbrirConexion();
                }
                mVMax.SoftReset();
                Thread.Sleep(6500);
                mVMax.CancelaCF();
                vResult = true;
                if (valAbrirConexion) {
                    CerrarConexion();
                }
                return vResult;
            } catch (Exception vEx) {
                throw new LibGalac.Aos.Catching.GalacWrapperException("Cancelar Documento Fiscal en Impresion", vEx);
            }
        }

        public bool ImprimirFacturaFiscal(XElement vDocumentoFiscal) {

            bool vResult = false;
            try {
                if (!_PuertoEstaAbierto) {
                    AbrirConexion();
                }
                if (AbrirComprobanteFiscal(vDocumentoFiscal, false)) {
                    vResult = ImprimirTodosLosArticulos(vDocumentoFiscal, false);
                    vResult &= EnviarPagos(vDocumentoFiscal);
                    ImprmirCamposDefinibles(vDocumentoFiscal);
                    vResult &= mVMax.CerrarCF();
                }
                if (_PuertoEstaAbierto) {
                    CerrarConexion();
                }
            } catch (LibGalac.Aos.Catching.GalacException vEx) {
                CerrarConexion();
                throw new GalacException("imprimir factura fiscal " + vEx.Message, eExceptionManagementType.Controlled);
            }
            return vResult;
        }

        private bool AbrirComprobanteFiscal(XElement vDocumentoFiscal, bool EsNotaDeCredito) {
            bool vResult = false;
            string vRif = LibText.CleanSpacesToBothSides(LibXml.GetPropertyString(vDocumentoFiscal, "NumeroRIF"));
            string vRazonSocial = LibText.CleanSpacesToBothSides(LibXml.GetPropertyString(vDocumentoFiscal, "NombreCliente"));
            string vDireccion = LibText.CleanSpacesToBothSides(LibXml.GetPropertyString(vDocumentoFiscal, "DireccionCliente"));
            string vObservaciones = LibText.CleanSpacesToBothSides(LibXml.GetPropertyString(vDocumentoFiscal, "Observaciones"));
            string vNumeroComprobanteOriginal = "";
            string vSerialMaquina = "";
            string vFecha = "";
            string vHora = "";
            string vTipo = "";
            string vSubString = "";
            vRazonSocial = LibImpresoraFiscalUtil.CadenaCaracteresValidos(vRazonSocial);
            vDireccion = LibImpresoraFiscalUtil.CadenaCaracteresValidos(vDireccion);
            vObservaciones = LibImpresoraFiscalUtil.CadenaCaracteresValidos(vObservaciones);
            vObservaciones = LibString.Replace(vObservaciones, "\n", "");

            if (EsNotaDeCredito) {
                vTipo = "D";
                vNumeroComprobanteOriginal = LibText.CleanSpacesToBothSides(LibXml.GetPropertyString(vDocumentoFiscal, "NumeroComprobanteFiscal"));
                vSerialMaquina = LibText.CleanSpacesToBothSides(LibXml.GetPropertyString(vDocumentoFiscal, "NumeroComprobanteFiscal"));
                vFecha = LibText.CleanSpacesToBothSides(LibXml.GetPropertyString(vDocumentoFiscal, "Fecha"));
                vHora = LibText.CleanSpacesToBothSides(LibXml.GetPropertyString(vDocumentoFiscal, "HoraModificacion"));
            } else {
                vTipo = "F";
            }

            try {
                vResult = mVMax.AbrirCF(ref vRazonSocial, ref vRif, ref vTipo, ref vNumeroComprobanteOriginal, ref vSerialMaquina, ref vFecha, ref vHora);

                if (!LibString.IsNullOrEmpty(vDireccion)) {
                    vDireccion += " Dir: " + vDireccion;
                    vSubString = LibString.SubString(vDireccion, 0, 40);
                    mVMax.TextoDNF(ref vSubString);
                    vSubString = LibString.SubString(vDireccion, 40, 40);
                    if (vSubString != "") {
                        mVMax.TextoDNF(ref vSubString);
                    }
                }
                if (!LibString.IsNullOrEmpty(vObservaciones)) {
                    AgregrLineaSeparacion();
                    vObservaciones += " Obs: " + vObservaciones;
                    vSubString = LibString.SubString(vObservaciones, 0, 40);
                    mVMax.TextoDNF(ref vSubString);
                    vSubString = LibString.SubString(vObservaciones, 40, 40);
                    if (vSubString != "") {
                        mVMax.TextoDNF(ref vSubString);
                    }
                    vSubString = LibString.SubString(vObservaciones, 80, 40);
                    if (vSubString != "") {
                        mVMax.TextoDNF(ref vSubString);
                    }
                }
                return vResult;
            } catch (Exception vEx) {
                throw new LibGalac.Aos.Catching.GalacWrapperException("Abrir Comprobante Fiscal", vEx);
            }
        }

        private bool ImprmirCamposDefinibles(XElement valData) {
            bool vResult = false;
            string vField;

            List<XElement> vCamposDefinibles = valData.Descendants("GpResultDetailCamposDefinibles").ToList();
            if (vCamposDefinibles.Count > 0) {
                foreach (XElement vRecord in vCamposDefinibles) {
                    vField = LibXml.GetElementValueOrEmpty(vRecord, "CampoDefinibleValue");
                    vField = LibImpresoraFiscalUtil.CadenaCaracteresValidos(vField);
                    AgregrLineaSeparacion();
                    vResult = mVMax.TextoDNF(ref vField);
                }
            } else {
                vResult = true;
            }
            return vResult;
        }

        private void AgregrLineaSeparacion() {
            string vResult = "";
            vResult = LibText.FillWithCharToLeft(vResult, "-", 40);
            mVMax.TextoDNF(ref vResult);
        }

        private bool ImprimirTodosLosArticulos(XElement valDocumentoFiscal, bool valIsNotaDeCredito) {
            bool vEstatus = false;
            string vGetResult = "";
            string vDescripcion;
            decimal vCantidad;
            string vCantidadFormat = "";
            decimal vMonto;
            string vMontoFormat = "";
            string vTipoTasa;
            decimal vPrcDescuento = 0;
            decimal vPrcDescuentoTotal = 0;
            decimal vMontoDctoTotal = 0;
            string vSerial;
            string vRollo;
            decimal vMontoDescuentoAlicuotaE = 0;
            decimal vMontoDescuentoAlicuotaA = 0;
            decimal vMontoDescuentoAlicuotaR = 0;
            decimal vMontoDescuentoAlicuotaG = 0;
            string vSDescuentoAlicuotaE;
            string vSDescuentoAlicuotaA;
            string vSDescuentoAlicuotaR;
            string vSDescuentoAlicuotaG;
            decimal vMontoDescuento = 0;
            string vMontoDctoFormat = "";
            string vDescripcionDescuentoTotal = "Descuento Global";
            eStatusImpresorasFiscales PrintStatus;

            try {
                vGetResult = LibImpresoraFiscalUtil.SetDecimalSeparator((LibXml.GetPropertyString(valDocumentoFiscal, "PorcentajeDescuento")));
                vPrcDescuentoTotal = LibMath.Abs(LibImportData.ToDec(vGetResult));
                List<XElement> vRecord = valDocumentoFiscal.Descendants("GpResultDetailRenglonFactura").ToList();
                foreach (XElement vXElement in vRecord) {
                    vDescripcion = LibText.CleanSpacesToBothSides(LibXml.GetElementValueOrEmpty(vXElement, "Descripcion"));
                    vDescripcion = LibImpresoraFiscalUtil.CadenaCaracteresValidos(vDescripcion);
                    vTipoTasa = LibText.CleanSpacesToBothSides(LibXml.GetElementValueOrEmpty(vXElement, "AlicuotaIva"));
                    vTipoTasa = DarFormatoAlicuotaIva((eTipoDeAlicuota)LibConvert.DbValueToEnum(vTipoTasa));
                    vGetResult = LibImpresoraFiscalUtil.SetDecimalSeparator(LibXml.GetElementValueOrEmpty(vXElement, "Cantidad"));
                    vCantidad = LibMath.Abs(LibImportData.ToDec(vGetResult));
                    vCantidadFormat = LibImpresoraFiscalUtil.DarFormatoNumericoParaImpresion(vGetResult, _EnterosParaCantidad, _DecimalParaCantidad);
                    //
                    vGetResult = LibImpresoraFiscalUtil.SetDecimalSeparator(LibXml.GetElementValueOrEmpty(vXElement, "PrecioSinIVA"));
                    vMonto = LibMath.Abs(LibImportData.ToDec(vGetResult));
                    vMontoFormat = LibImpresoraFiscalUtil.DarFormatoNumericoParaImpresion(vGetResult, _EnterosParaMonto, _DecimalParaMonto);
                    //                        
                    vGetResult = LibImpresoraFiscalUtil.SetDecimalSeparator(LibXml.GetElementValueOrEmpty(vXElement, "PorcentajeDescuento"));
                    vPrcDescuento = LibMath.Abs(LibImportData.ToDec(vGetResult));
                    vMontoDescuento = CalcularMontoDescuento(vMonto, vPrcDescuento, vCantidad);
                    vMontoDescuento = (vMonto * vCantidad) - vMontoDescuento;
                    vMontoDctoFormat = LibImpresoraFiscalUtil.DarFormatoNumericoParaImpresion(LibImpresoraFiscalUtil.DecimalToStringFormat(vMontoDescuento, _DecimalParaMonto), _EnterosParaMonto, _DecimalParaMonto);
                    //
                    if (vPrcDescuentoTotal > 0) {
                        vMontoDctoTotal = CalcularMontoDescuentoTotal(vMontoDescuento, vCantidad, vMonto, vPrcDescuentoTotal);
                        TotalizarDescuentoGlobalPorAlicuota(vTipoTasa, vMontoDctoTotal, ref vMontoDescuentoAlicuotaG, ref vMontoDescuentoAlicuotaA, ref vMontoDescuentoAlicuotaR, ref vMontoDescuentoAlicuotaE);
                    }
                    //
                    vSerial = LibText.CleanSpacesToBothSides(LibXml.GetElementValueOrEmpty(vXElement, "Serial"));
                    vRollo = LibText.CleanSpacesToBothSides(LibXml.GetElementValueOrEmpty(vXElement, "Rollo"));
                    AgregrLineaSeparacion();
                    vEstatus = ImprimirArticuloVenta(vDescripcion, vCantidadFormat, vMontoFormat, vTipoTasa, vMontoDctoFormat, vSerial, vRollo, valIsNotaDeCredito);

                    if (!vEstatus) {
                        vEstatus &= false;
                        throw new Exception("Articulo no impreso");
                    }
                }

                if(vMontoDctoTotal > 0) {
                    string vPercibido = "0.00";
                    vSDescuentoAlicuotaA = LibImpresoraFiscalUtil.DarFormatoNumericoParaImpresion(LibImpresoraFiscalUtil.DecimalToStringFormat(vMontoDescuentoAlicuotaA,_DecimalParaMonto),10,_DecimalParaMonto);
                    vSDescuentoAlicuotaG = LibImpresoraFiscalUtil.DarFormatoNumericoParaImpresion(LibImpresoraFiscalUtil.DecimalToStringFormat(vMontoDescuentoAlicuotaG,_DecimalParaMonto),10,_DecimalParaMonto);
                    vSDescuentoAlicuotaR = LibImpresoraFiscalUtil.DarFormatoNumericoParaImpresion(LibImpresoraFiscalUtil.DecimalToStringFormat(vMontoDescuentoAlicuotaR,_DecimalParaMonto),10,_DecimalParaMonto);
                    vSDescuentoAlicuotaE = LibImpresoraFiscalUtil.DarFormatoNumericoParaImpresion(LibImpresoraFiscalUtil.DecimalToStringFormat(vMontoDescuentoAlicuotaE,_DecimalParaMonto),10,_DecimalParaMonto);
                    mVMax.DescuentoCF(ref vDescripcionDescuentoTotal,ref vSDescuentoAlicuotaE,ref vSDescuentoAlicuotaG,ref vSDescuentoAlicuotaR,ref vSDescuentoAlicuotaA,ref vPercibido);
                }
            } catch (Exception vEx) {
                CancelarDocumentoFiscalEnImpresion(false);
                CerrarConexion();
                throw new GalacException(vEx.Message, eExceptionManagementType.Controlled);
            }
            return vEstatus;
        }

        private bool ImprimirArticuloVenta(string valDescripcion, string valCantidad, string valMonto, string valImpuesto, string valMtoDescuento, string valSerial, string valRollo, bool valIsNotaCredito) {
            bool vResult = true;
            short vTipo = 1;
            string vDescripcionDescuento = "Dcto en Articulo";
            string vSubstring = "";


            if (!valIsNotaCredito) {
                vResult = mVMax.Item(ref valDescripcion, ref valCantidad, ref valMonto, ref valImpuesto, ref vTipo);
            } else {
                vResult = mVMax.ItemDev(ref valDescripcion, ref valCantidad, ref valMonto, ref valImpuesto, ref vTipo);
            }

            if (!valSerial.Trim().Equals("")) {
                valSerial = LibImpresoraFiscalUtil.CadenaCaracteresValidos(valSerial);
                vSubstring = LibString.SubString(valSerial, 0, 40);
                mVMax.TextoDNF(ref vSubstring);
            }
            if (!valRollo.Trim().Equals("")) {
                valRollo = LibImpresoraFiscalUtil.CadenaCaracteresValidos(valRollo);
                vSubstring = LibString.SubString(valRollo, 0, 40);
                mVMax.TextoDNF(ref vSubstring);
            }

            if (LibImportData.ToDec(valMtoDescuento) > 0) {
                vResult &= mVMax.ItemDesc(ref vDescripcionDescuento, ref valMtoDescuento, ref valImpuesto);
            }
            return vResult;
        }

        private decimal CalcularMontoDescuento(decimal valMonto, decimal valPorcDescuento, decimal valCantidad) {
            decimal vResultado = 0;
            vResultado = valMonto * valCantidad - (valMonto * valCantidad * valPorcDescuento / 100m);
            vResultado = LibMath.RoundToNDecimals(vResultado, _DecimalParaMonto);
            return vResultado;
        }

        private void TotalizarDescuentoGlobalPorAlicuota(string valAlicuotas, decimal valMontoDesctotal, ref decimal refMontoDescuentoAlicuotaG, ref decimal refMontoDescuentoAlicuotaA, ref decimal refMontoDescuentoAlicuotaR, ref decimal refMontoDescuentoAlicuotaE) {
            switch (valAlicuotas) {
                case "1":
                    refMontoDescuentoAlicuotaG = refMontoDescuentoAlicuotaG + valMontoDesctotal;
                    break;
                case "2":
                    refMontoDescuentoAlicuotaR = refMontoDescuentoAlicuotaR + valMontoDesctotal;
                    break;
                case "3":
                    refMontoDescuentoAlicuotaA = refMontoDescuentoAlicuotaA + valMontoDesctotal;
                    break;
                case "0":
                    refMontoDescuentoAlicuotaE = refMontoDescuentoAlicuotaE + valMontoDesctotal;
                    break;
            }
        }

        private decimal CalcularMontoDescuentoTotal(decimal valMontoDescuento, decimal valCantidad, decimal valMonto, decimal valPrcDescuentoTotal) {
            decimal vMontoDescuentoTotal = 0;
            vMontoDescuentoTotal = valMontoDescuento * valPrcDescuentoTotal / 100m;
            vMontoDescuentoTotal = LibMath.RoundToNDecimals(vMontoDescuentoTotal, _DecimalParaMonto);
            return vMontoDescuentoTotal;
        }

        private bool EnviarPagos(XElement valMedioDePago) {
            string vMedioDePago = "";            
            short vTipoPago = 1;
            decimal vBaseImponibleIGTF = 0;            
            string vMontoME = "";
            string vCodigoMoneda = LibXml.GetPropertyString(valMedioDePago, "CodigoMoneda");
            int vCantidadCaracteres;
            string vCaracteresEspacio;
            try {
                vBaseImponibleIGTF = LibImportData.ToDec(LibXml.GetPropertyString(valMedioDePago, "BaseImponibleIGTF"));
                if (vBaseImponibleIGTF > 0) { // Aqui se envia el IGTF
                    vMedioDePago = "Divisas";
                    vMontoME = LibImpresoraFiscalUtil.DarFormatoNumericoParaImpresion(LibConvert.ToStr(vBaseImponibleIGTF), _EnterosParaMonto, _DecimalParaMonto);
                    mVMax.SubTotalT(ref vMontoME);
                    vMontoME = LibConvert.NumToString(vBaseImponibleIGTF, 2);
                    vCantidadCaracteres = 33 - LibString.Len(vMontoME) - 2;
                    vCaracteresEspacio = LibString.Space((byte)vCantidadCaracteres);
                    vMontoME = vMedioDePago + vCaracteresEspacio + vMontoME;
                    mVMax.TextoDNF(ref vMontoME);
                } else {
                    mVMax.SubTotal();
                }
                List<XElement> vNodos = valMedioDePago.Descendants("GpResultDetailRenglonCobro").Where(p => p.Element("CodigoMoneda").Value == vCodigoMoneda).ToList();
                if (vNodos.Count > 0) {
                    foreach (XElement vXElement in vNodos) {
                        vMedioDePago = LibText.CleanSpacesToBothSides(LibXml.GetElementValueOrEmpty(vXElement, "CodigoFormaDelCobro"));
                        vMedioDePago = FormaDeCobro(vMedioDePago);
                        vMontoME = LibText.CleanSpacesToBothSides(LibXml.GetElementValueOrEmpty(vXElement, "Monto"));
                        vMontoME = LibImpresoraFiscalUtil.DarFormatoNumericoParaImpresion(vMontoME, _EnterosParaMonto, _DecimalParaMonto);                        
                        mVMax.PagoCF(ref vMedioDePago, ref vMontoME, ref vTipoPago);
                    }
                }
                return true;
            } catch (GalacException vEx) {
                throw new LibGalac.Aos.Catching.GalacException(vEx.Message, eExceptionManagementType.Controlled);
            }
        }

        private string FormaDeCobro(string valFormaDeCobro) {
            string vResultado = "";
            switch (valFormaDeCobro) {
                case "00001":
                    vResultado = "Efectivo";
                    break;
                case "00002":
                    vResultado = "Cheque";
                    break;
                case "00003":
                    vResultado = "Tarjeta";
                    break;
                case "00004":
                    vResultado = "Depósito";
                    break;
                case "00005":
                    vResultado = "Anticipo";
                    break;
                case "00006":
                    vResultado = "Transferencia";
                    break;
                default:
                    vResultado = "Efectivo";
                    break;
            }
            return vResultado;
        }

        public bool ImprimirNotaCredito(XElement vDocumentoFiscal) {
            bool vResult = false;
            try {

                if (!_PuertoEstaAbierto) {
                    AbrirConexion();
                }
                if (AbrirComprobanteFiscal(vDocumentoFiscal, true)) {
                    vResult = ImprimirTodosLosArticulos(vDocumentoFiscal, true);
                    vResult &= EnviarPagos(vDocumentoFiscal);
                    vResult &= mVMax.CerrarCF();
                }

                if (_PuertoEstaAbierto) {
                    CerrarConexion();
                }
            } catch (GalacException vEx) {

            }
            return vResult;
        }

        private bool CheckStatus(string valEstatus, ref string refMensaje) {
            bool vResult = false;

            switch (valEstatus) {
                case "0":
                    vResult = true;
                    refMensaje = "Listo En Espera";
                    break;
                case "1":
                case "2":
                    vResult = false;
                    refMensaje = "Comprobante Fiscal Abierto";
                    break;
                case "3":
                    vResult = false;
                    refMensaje = "En Espera de un Medio de  Pago";
                    break;
                case "4":
                    vResult = true;
                    refMensaje = "Se Recibio al menos un pago";
                    break;
                case "5":
                    vResult = true;
                    refMensaje = "Venta Procesada";
                    break;
                case "6":
                    vResult = false;
                    refMensaje = "Operación No fiscal en Proceso";
                    break;
                case "7":
                    vResult = false;
                    refMensaje = "Modo de Programación";
                    break;
                case "8":
                    vResult = false;
                    refMensaje = CheckTipoDeError();
                    break;
                case "9":
                case ":":
                    vResult = false;
                    refMensaje = "Devolucion en Proceso";
                    break;
                default:
                    vResult = false;
                    refMensaje = "Error Desconocido";
                    break;
            }
            return vResult;
        }

        private string CheckTipoDeError() {
            string vResult = "";

            if (mVMax.ErrorFecha) {
                vResult = "Error en Fecha / Reloj Detenido";
            } else if (mVMax.ErrorImpresora) {
                vResult = "Error en Impresora / Sin Papel / Tapa Abierta";
            } else if (mVMax.ErrorMemoriaFiscal) {
                vResult = "Error en Memoria Fiscal / Memoria Fiscal Llena";
            }
            return vResult;
        }

        private string DarFormatoAlicuotaIva(eTipoDeAlicuota valTipoAlicuota) {
            string vResult = "";
            switch (valTipoAlicuota) {
                case eTipoDeAlicuota.AlicuotaGeneral:
                case eTipoDeAlicuota.AlicuotaGeneralNC:
                    vResult = "G";
                    break;
                case eTipoDeAlicuota.Alicuota2:
                case eTipoDeAlicuota.Alicuota2NC:
                    vResult = "R";
                    break;
                case eTipoDeAlicuota.Alicuota3:
                case eTipoDeAlicuota.Alicuota3NC:
                    vResult = "A";
                    break;
                case eTipoDeAlicuota.Exento:
                case eTipoDeAlicuota.ExentoNC:
                    vResult = "E";
                    break;
                default:
                    vResult = "G";
                    break;
            }
            return vResult;
        }

        public bool ReimprimirFactura(string valDesde, string valHasta) {
            short vModo = 0;
            short vTipo = 0;
            return true;
        }

        public bool ReimprimirNotaDeCredito(string valDesde, string valHasta) {
            short vModo = 0;
            short vTipo = 1;
            return true;
        }

        public bool ReimprimirReporteZ(string valDesde, string valHasta) {
            short vModo = 0;
            short vTipo = 2;
            return true;
        }

        public bool ReimprimirReporteX(string valDesde, string valHasta) {
            short vModo = 0;
            short vTipo = 4;
            return true;
        }

        public bool ReimprimirDocumentoNoFiscal(string valDesde, string valHasta) {
            short vModo = 0;
            short vTipo = 3;
            return true;
        }

        bool IImpresoraFiscalPdn.ReimprimirDocumentoFiscal(string valDesde, string valHasta, string valTipo) {
            bool vResult = false;
            eTipoDocumentoFiscal TipoDeDocumento = (eTipoDocumentoFiscal)LibConvert.DbValueToEnum(valTipo);

            switch (TipoDeDocumento) {
                case eTipoDocumentoFiscal.FacturaFiscal:
                    vResult = ReimprimirFactura(valDesde, valHasta);
                    break;
                case eTipoDocumentoFiscal.NotadeCredito:
                    vResult = ReimprimirNotaDeCredito(valDesde, valHasta);
                    break;
                case eTipoDocumentoFiscal.ReporteX:
                    vResult = ReimprimirReporteX(valDesde, valHasta);
                    break;
                case eTipoDocumentoFiscal.ReporteZ:
                    vResult = ReimprimirReporteZ(valDesde, valHasta);
                    break;
            }
            return vResult;
        }

        bool ILibPdn.CanBeChoosen(string valCallingModule, eAccionSR valAction, string valExtendedAction, XmlDocument valXmlRow) {
            return false;
        }

        bool ILibPdn.GetDataForList(string valCallingModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            return false;
        }

        XElement ILibPdn.GetFk(string valCallingModule, StringBuilder valParameters) {
            return null;
        }

        public IFDiagnostico RealizarDiagnotsico(bool valAbrirPuerto = false) {
            throw new NotImplementedException();
        }

        public bool EstatusDeComunicacion(IFDiagnostico vDiagnostico) {
            throw new NotImplementedException();
        }

        public bool VersionDeControladores(IFDiagnostico vDiagnostico) {
            throw new NotImplementedException();
        }

        public bool AlicuotasRegistradas(IFDiagnostico vDiagnostico) {
            throw new NotImplementedException();
        }

        public bool FechaYHora(IFDiagnostico vDiagnostico) {
            throw new NotImplementedException();
        }

        public bool ColaDeImpresion(IFDiagnostico vDiagnostico) {
            throw new NotImplementedException();
        }
    }
}


