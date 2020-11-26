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
            _ImpresoraFiscal = valImpresoraFiscal;
        }
      
        #endregion Contructor
        #region Metodos Generados       

        public void LeerDatosDeImpresoraFiscal(eTipoDocumentoFiscal valTipoOperacion) {
            switch(valTipoOperacion) {
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
            SerialImpresoraFiscal = _ImpresoraFiscal.ObtenerSerial(false);
            NumeroComprobanteFiscal = LibConvert.ToStr(LibConvert.ToInt(NumeroComprobanteFiscal) + 1);
            NumeroComprobanteFiscal = LibText.FillWithCharToLeft(NumeroComprobanteFiscal,"0",8);
        }

        private bool FechaYHoraValidaEnImpresoraFiscal(DateTime valFechaHora,ref string valMensaje) {
            bool vResult = false;
            DateTime vFechaActual = LibDate.Today().Date;
            int vHoraActual = LibConvert.ToInt(LibText.SubString(LibDate.CurrentHourAsStr,0,2));
            int vMinActual = LibConvert.ToInt(LibText.SubString(LibDate.CurrentHourAsStr,3,2));
            int vHoraMFiscal = LibConvert.ToInt(LibText.SubString(LibConvert.ToShortTimeStr(valFechaHora),0,2));
            int vMinMFiscal = LibConvert.ToInt(LibText.SubString(LibConvert.ToShortTimeStr(valFechaHora),3,2));
            int vMinutosDiferencia = LibConvert.ToInt(LibMath.Abs(vMinActual - vMinMFiscal));

            if(valFechaHora.Date == vFechaActual) {
                vResult = true;
            } else {
                valMensaje = "La fecha del computador no corresponde con la fecha de la impresora fiscal\r\nSincronizar hora de los dispositivos\r\nFecha de la Maquina Fiscal:" + LibConvert.ToStr(valFechaHora.Date);
                vResult &= true;
            }
            return vResult;
        }

        public bool DetectarImpresoraFiscal(ref eStatusImpresorasFiscales refStatusPapel) {
            bool vResult = false;
            if(_ImpresoraFiscal == null) {
                vResult = false;
            } else if(_ImpresoraFiscal.AbrirConexion()) {
                vResult = ComprobarEstadosDeImpresora(ref refStatusPapel);
                _ImpresoraFiscal.CerrarConexion();
            } else {
                throw new GalacAlertException("No se pudo conectar a la Impresora Fiscal, Revisar Conexiones");
            }
            return vResult;
        }

        private bool ComprobarEstadosDeImpresora(ref eStatusImpresorasFiscales refStatusPapel) {
            bool vResult = false;
            string vSerialImpresoraFiscalInDB = "";
            string vSerialImpresoraFiscalInConnection = "";
            DateTime vFechaYHoraInConnection;
            string vMensaje = "";

            try {
                if(_ImpresoraFiscal.ComprobarEstado()) {
                    refStatusPapel = _ImpresoraFiscal.EstadoDelPapel(false);
                    vSerialImpresoraFiscalInDB = SerialImpresoraFiscal;
                    vSerialImpresoraFiscalInConnection = _ImpresoraFiscal.ObtenerSerial(false);
                    vFechaYHoraInConnection = LibConvert.ToDate(_ImpresoraFiscal.ObtenerFechaYHora());
                    if(refStatusPapel.Equals(eStatusImpresorasFiscales.ePocoPapel)) {
                        vResult = true;
                    } else if(refStatusPapel.Equals(eStatusImpresorasFiscales.eSinPapel) || refStatusPapel.Equals(eStatusImpresorasFiscales.eAtascoDePapel)) {
                        throw new GalacAlertException("Papel agotado, favor reemplazar");
                    }
                    if(!vSerialImpresoraFiscalInDB.Equals(vSerialImpresoraFiscalInConnection)) {
                        throw new GalacAlertException("El serial de la maquina fiscal asignada a esta caja no corresponde, Revisar el dispositivo fiscal para continuar");
                    }
                    if(!(FechaYHoraValidaEnImpresoraFiscal(vFechaYHoraInConnection,ref vMensaje))) {
                        vResult = true;
                        throw new GalacAlertException(vMensaje);
                    }
                    vResult = true;
                } else {
                    throw new GalacAlertException("No se pudo conectar a la Impresora Fiscal, Revisar Conexiones");
                }
                return vResult;
            } catch(GalacException vEx) {
                throw vEx;
            }
        }

        public bool ImprimirDocumentoFiscal(XElement valData) {
            bool vResult = false;
            try {
                string CampoTipoDeDocumento = LibText.CleanSpacesToBothSides(LibXml.GetPropertyString(valData,"GpResult","TipoDeDocumento"));
                eTipoDocumentoFactura TipoDocumento = (eTipoDocumentoFactura)LibConvert.DbValueToEnum(CampoTipoDeDocumento);
                if(TipoDocumento.Equals(eTipoDocumentoFactura.ComprobanteFiscal)) {
                    vResult = _ImpresoraFiscal.ImprimirFacturaFiscal(valData);
                } else {
                    vResult = _ImpresoraFiscal.ImprimirNotaCredito(valData);
                }
                return vResult;
            } catch(GalacAlertException vEx) {
                throw vEx;
            }
        }

        public bool ImprimirReporteZ() {
            bool vResult = false;
            try {
                vResult = _ImpresoraFiscal.RealizarReporteZ();
                return vResult;
            } catch(GalacAlertException vEx) {
                throw vEx;
            }
        }

        public bool ImprimirReporteX() {
            bool vResult = false;
            try {
                vResult = _ImpresoraFiscal.RealizarReporteX();
                return vResult;
            } catch(GalacAlertException vEx) {
                throw vEx;
            }
        }
        #endregion Metodos Generados
    }
}
