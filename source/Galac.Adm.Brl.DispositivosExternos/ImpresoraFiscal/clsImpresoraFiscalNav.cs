using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using Galac.Saw.Ccl.SttDef;
using Galac.Adm.Ccl.DispositivosExternos;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;

namespace Galac.Adm.Brl.DispositivosExternos.ImpresoraFiscal {
    public class clsImpresoraFiscalNav {
        #region variables
        private string _SerialImpresoraFiscal = "";
        private string _NumeroComprobanteFiscal = "";
        IImpresoraFiscalPdn _ImpresoraFiscal;
        Galac.Saw.Ccl.Tablas.IAuditoriaConfiguracionPdn _AuditoriaConfiguracion;

        #endregion variables

        #region propiedades

        public string SerialImpresoraFiscal {
            get {
                return _SerialImpresoraFiscal;
            }
            set {
                _SerialImpresoraFiscal = value;
            }
        }

        public string NumeroComprobanteFiscal {
            get {
                return _NumeroComprobanteFiscal;
            }
            set {
                _NumeroComprobanteFiscal = value;
            }
        }

        public IImpresoraFiscalPdn ImpresoraFiscal {
            get { return _ImpresoraFiscal; }
            set { _ImpresoraFiscal = value; }
        }
        #endregion propiedades

        #region Constructor
        public clsImpresoraFiscalNav(IImpresoraFiscalPdn valImpresoraFiscal) {
            _AuditoriaConfiguracion = new Galac.Saw.Brl.Tablas.clsAuditoriaConfiguracionNav();
            _ImpresoraFiscal = valImpresoraFiscal;
        }

        #endregion Contructor
        #region Metodos Generados       

        public void LeerDatosDeImpresoraFiscal(eTipoDocumentoFiscal valTipoOperacion) {
            SerialImpresoraFiscal = _ImpresoraFiscal.ObtenerSerial(false);
            switch (valTipoOperacion) {
                case eTipoDocumentoFiscal.FacturaFiscal:
                    NumeroComprobanteFiscal = _ImpresoraFiscal.ObtenerUltimoNumeroFactura(false);
                    break;
                case eTipoDocumentoFiscal.NotadeCredito:
                    NumeroComprobanteFiscal = _ImpresoraFiscal.ObtenerUltimoNumeroNotaDeCredito(false);
                    break;
                case eTipoDocumentoFiscal.ReporteZ:
                    NumeroComprobanteFiscal = _ImpresoraFiscal.ObtenerUltimoNumeroReporteZ(false);
                    break;
            }
            NumeroComprobanteFiscal = LibConvert.ToStr(LibConvert.ToInt(NumeroComprobanteFiscal) + 1);
            NumeroComprobanteFiscal = LibText.FillWithCharToLeft(NumeroComprobanteFiscal, "0", 8);
        }

        private bool FechaYHoraValidaEnImpresoraFiscal(DateTime valFechaHora, ref string valMensaje) {
            bool vResult = false;
            DateTime vFechaActual = LibDate.Today().Date;
            int vHoraActual = LibConvert.ToInt(LibText.SubString(LibDate.CurrentHourAsStr, 0, 2));
            int vMinActual = LibConvert.ToInt(LibText.SubString(LibDate.CurrentHourAsStr, 3, 2));
            int vHoraMFiscal = LibConvert.ToInt(LibText.SubString(LibConvert.ToShortTimeStr(valFechaHora), 0, 2));
            int vMinMFiscal = LibConvert.ToInt(LibText.SubString(LibConvert.ToShortTimeStr(valFechaHora), 3, 2));
            int vMinutosDiferencia = LibConvert.ToInt(LibMath.Abs(vMinActual - vMinMFiscal));

            if (valFechaHora.Date == vFechaActual) {
                vResult = true;
            } else {
                valMensaje = "La fecha del computador no corresponde con la fecha de la impresora fiscal\r\nSincronizar hora de los dispositivos\r\nFecha de la Impresora Fiscal:" + LibConvert.ToStr(valFechaHora.Date);
                vResult = true;
            }
            return vResult;
        }

        public bool DetectarImpresoraFiscal(ref eStatusImpresorasFiscales refStatusPapel) {//Todo cambio se debe adaptar en DetectarImpresoraFiscalVb
            bool vResult = false;
            if (_ImpresoraFiscal == null) {
                vResult = false;
            } else if (_ImpresoraFiscal.AbrirConexion()) {
                vResult = ComprobarEstadosDeImpresora(ref refStatusPapel);
                _ImpresoraFiscal.CerrarConexion();
            } else {
                string vMensaje = "No se pudo conectar a la Impresora Fiscal, Revisar Conexiones";
                _AuditoriaConfiguracion.Auditar("Impresora Fiscal:" + vMensaje, "Detectar Impresora", "", "");
                throw new GalacAlertException(vMensaje);
            }
            return vResult;
        }

        public bool DetectarImpresoraFiscalVb(ref eStatusImpresorasFiscales refStatusPapel, ref string refErrorMessage) {//Todo cambio se debe adaptar en DetectarImpresoraFiscal
            bool vResult = false;
            refErrorMessage = "";
            if (_ImpresoraFiscal == null) {
                vResult = false;
            } else if (_ImpresoraFiscal.AbrirConexion()) {
                vResult = ComprobarEstadosDeImpresoraVb(ref refStatusPapel, ref refErrorMessage);
                _ImpresoraFiscal.CerrarConexion();
            } else {
                refErrorMessage = "No se pudo conectar a la Impresora Fiscal. Revisar Conexiones. ";
                _AuditoriaConfiguracion.Auditar("Impresora Fiscal:" + refErrorMessage, "Detectar Impresora", "", "");
            }
            return vResult;
        }
            

        private bool ComprobarEstadosDeImpresora(ref eStatusImpresorasFiscales refStatusPapel) {//Todo cambio se debe adaptar en ComprobarEstadosDeImpresoraVb
            bool vResult = false;
            string vSerialImpresoraFiscalInDB = "";
            string vSerialImpresoraFiscalInConnection = "";
            DateTime vFechaYHoraInConnection;
            string vMensaje = "";

            try {
                if (_ImpresoraFiscal.ComprobarEstado()) {
                    refStatusPapel = _ImpresoraFiscal.EstadoDelPapel(false);
                    vSerialImpresoraFiscalInDB = SerialImpresoraFiscal;
                    vSerialImpresoraFiscalInConnection = _ImpresoraFiscal.ObtenerSerial(false);
                    vFechaYHoraInConnection = LibConvert.ToDate(_ImpresoraFiscal.ObtenerFechaYHora());
                    if (refStatusPapel.Equals(eStatusImpresorasFiscales.ePocoPapel)) {
                        vResult = true;
                    } else if (refStatusPapel.Equals(eStatusImpresorasFiscales.eSinPapel) || refStatusPapel.Equals(eStatusImpresorasFiscales.eAtascoDePapel)) {
                        vMensaje = "Papel agotado, favor reemplazar.";
                        _AuditoriaConfiguracion.Auditar("Impresora Fiscal:" + vMensaje, "Comprobar Estados", "", "");
                        throw new GalacAlertException(vMensaje);
                    }
                    if (!vSerialImpresoraFiscalInDB.Equals(vSerialImpresoraFiscalInConnection)) {
                        vMensaje = "El serial de la Impresora Fiscal configurada en la caja actual no corresponde con el serial de la Impresora Fiscal conectada al computador.\r\n\r\nValide la Impresora Fiscal conectada o configure nuevamente la caja para continuar.";
                        _AuditoriaConfiguracion.Auditar("Impresora Fiscal:" + vMensaje, "Comprobar Estadosl", "", "");
                        throw new GalacAlertException(vMensaje);
                    }
                    if (!(FechaYHoraValidaEnImpresoraFiscal(vFechaYHoraInConnection, ref vMensaje))) {
                        vResult = true;
                        _AuditoriaConfiguracion.Auditar("Impresora Fiscal:" + vMensaje, "Comprobar Estados", "", "");
                        throw new GalacAlertException(vMensaje);
                    }
                    vResult = true;
                } else {
                    vMensaje = "No se pudo conectar a la Impresora Fiscal. Revisar Conexiones.";
                    _AuditoriaConfiguracion.Auditar("Impresora Fiscal:" + vMensaje, "Comprobar Estados", "", "");
                    throw new GalacAlertException(vMensaje);
                }
                return vResult;
            } catch (GalacException vEx) {                
                throw vEx;
            }
        }

        private bool ComprobarEstadosDeImpresoraVb(ref eStatusImpresorasFiscales refStatusPapel, ref string refMensaje) {//Todo cambio se debe adaptar en ComprobarEstadosDeImpresora
            bool vResult = false;
            string vSerialImpresoraFiscalInDB = "";
            string vSerialImpresoraFiscalInConnection = "";
            DateTime vFechaYHoraInConnection;
            string vMensaje = "";

            try {
                if (_ImpresoraFiscal.ComprobarEstado()) {
                    refStatusPapel = _ImpresoraFiscal.EstadoDelPapel(false);
                    vSerialImpresoraFiscalInDB = SerialImpresoraFiscal;
                    vSerialImpresoraFiscalInConnection = _ImpresoraFiscal.ObtenerSerial(false);
                    vFechaYHoraInConnection = LibConvert.ToDate(_ImpresoraFiscal.ObtenerFechaYHora());
                    if (refStatusPapel.Equals(eStatusImpresorasFiscales.ePocoPapel)) {
                        vResult = true;
                    } else if (refStatusPapel.Equals(eStatusImpresorasFiscales.eSinPapel) || refStatusPapel.Equals(eStatusImpresorasFiscales.eAtascoDePapel)) {
                        refMensaje = "Papel agotado, favor reemplazar.";
                        _AuditoriaConfiguracion.Auditar("Impresora Fiscal:" + refMensaje, "Comprobar Estados", "", "");
                    }
                    if (!vSerialImpresoraFiscalInDB.Equals(vSerialImpresoraFiscalInConnection)) {
                        refMensaje = "El serial de la Impresora Fiscal configurada en la caja actual no corresponde con el serial de la Impresora Fiscal conectada al computador.\r\n\r\nValide la Impresora Fiscal conectada o configure nuevamente la caja para continuar.";
                        _AuditoriaConfiguracion.Auditar("Impresora Fiscal:" + refMensaje, "Comprobar Estados", "", "");
                    }
                    if (!(FechaYHoraValidaEnImpresoraFiscal(vFechaYHoraInConnection, ref vMensaje))) {
                        vResult = true;
                        refMensaje = vMensaje;
                        _AuditoriaConfiguracion.Auditar("Impresora Fiscal:" + refMensaje, "Comprobar Estados", "", "");
                    }
                    vResult = true;
                } else {
                    refMensaje = "No se pudo conectar a la Impresora Fiscal. Revisar Conexiones.";
                    _AuditoriaConfiguracion.Auditar("Impresora Fiscal:" + refMensaje, "Comprobar Estados", "", "");
                }
                return vResult;
            } catch (GalacException vEx) {
                _AuditoriaConfiguracion.Auditar("Impresora Fiscal:" + vEx.Message, "Comprobar Estados", "", "");
                throw vEx;
            }
        }

        public bool ImprimirDocumentoFiscal(XElement valData) {
            bool vResult = false;
            try {
                string CampoTipoDeDocumento = LibText.CleanSpacesToBothSides(LibXml.GetPropertyString(valData, "GpResult", "TipoDeDocumento"));
                eTipoDocumentoFactura TipoDocumento = (eTipoDocumentoFactura)LibConvert.DbValueToEnum(CampoTipoDeDocumento);
                if (TipoDocumento.Equals(eTipoDocumentoFactura.ComprobanteFiscal)) {
                    vResult = _ImpresoraFiscal.ImprimirFacturaFiscal(valData);
                } else {
                    vResult = _ImpresoraFiscal.ImprimirNotaCredito(valData);
                }
                return vResult;
            } catch (GalacAlertException vEx) {
                _AuditoriaConfiguracion.Auditar("Impresora Fiscal:" + vEx.Message, "Imprimir Documento", "", "");
                throw vEx;
            }
        }

        public bool ImprimirReporteZ() {
            bool vResult = false;
            try {
                vResult = _ImpresoraFiscal.RealizarReporteZ();
                return vResult;
            } catch (GalacAlertException vEx) {
                _AuditoriaConfiguracion.Auditar("Impresora Fiscal:" + vEx.Message, "Imprimir Reporte Z", "", "");
                throw vEx;
            }
        }

        public bool ImprimirReporteX() {
            bool vResult = false;
            try {
                vResult = _ImpresoraFiscal.RealizarReporteX();
                return vResult;
            } catch (GalacAlertException vEx) {
                _AuditoriaConfiguracion.Auditar("Impresora Fiscal:" + vEx.Message, "Imprimir Reporte X", "", "");
                throw vEx;
            }
        }
        #endregion Metodos Generados
    }
}
