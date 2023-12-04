using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;


namespace Galac.Adm.Ccl.Venta {
    [Serializable]
    public class CajaApertura {
        #region Variables
        private int _ConsecutivoCompania;
        private int _Consecutivo;
        private int _ConsecutivoCaja;
        private string _NombreCaja;
        private string _NombreDelUsuario;
        private decimal _MontoApertura;
        private decimal _MontoCierre;
        private decimal _MontoEfectivo;
        private decimal _MontoTarjeta;
        private decimal _MontoCheque;
        private decimal _MontoDeposito;
        private decimal _MontoAnticipo;
        private decimal _MontoVuelto;
        private decimal _MontoVueltoPM;
        private decimal _MontoTarjetaMS;
        private decimal _MontoC2P;
        private decimal _MontoTransferenciaMS;
        private decimal _MontoPagoMovil;
        private decimal _MontoDepositoMS;
        private DateTime _Fecha;
        private string _HoraApertura;
        private string _HoraCierre;
        private bool _CajaCerrada;
        private string _CodigoMoneda;
        private string _Moneda;
        private decimal _Cambio;
        private decimal _MontoAperturaME;
        private decimal _MontoCierreME;
        private decimal _MontoEfectivoME;
        private decimal _MontoTarjetaME;
        private decimal _MontoChequeME;
        private decimal _MontoDepositoME;
        private decimal _MontoAnticipoME;
        private decimal _MontoVueltoME;
        private decimal _MontoZelle;
        private string _NombreOperador;
        private DateTime _FechaUltimaModificacion;
        private long _fldTimeStamp;
        XmlDocument _datos;
        #endregion //Variables
        #region Propiedades

        public int ConsecutivoCompania {
            get { return _ConsecutivoCompania; }
            set { _ConsecutivoCompania = value; }
        }

        public int Consecutivo {
            get { return _Consecutivo; }
            set { _Consecutivo = value; }
        }

        public int ConsecutivoCaja {
            get { return _ConsecutivoCaja; }
            set { _ConsecutivoCaja = value; }
        }

        public string NombreCaja {
            get { return _NombreCaja; }
            set { _NombreCaja = LibString.Mid(value, 0, 60); }
        }

        public string NombreDelUsuario {
            get { return _NombreDelUsuario; }
            set { _NombreDelUsuario = LibString.Mid(value, 0, 20); }
        }

        public decimal MontoApertura {
            get { return _MontoApertura; }
            set { _MontoApertura = value; }
        }

        public decimal MontoCierre {
            get { return _MontoCierre; }
            set { _MontoCierre = value; }
        }

        public decimal MontoEfectivo {
            get { return _MontoEfectivo; }
            set { _MontoEfectivo = value; }
        }

        public decimal MontoTarjeta {
            get { return _MontoTarjeta; }
            set { _MontoTarjeta = value; }
        }

        public decimal MontoCheque {
            get { return _MontoCheque; }
            set { _MontoCheque = value; }
        }

        public decimal MontoDeposito {
            get { return _MontoDeposito; }
            set { _MontoDeposito = value; }
        }

        public decimal MontoAnticipo {
            get { return _MontoAnticipo; }
            set { _MontoAnticipo = value; }
        }

        public decimal MontoVuelto {
            get { return _MontoVuelto; }
            set { _MontoVuelto = value; }
        }
		
        public decimal MontoVueltoPM {
            get { return _MontoVueltoPM; }
            set { _MontoVueltoPM = value; }
        }
		
		public decimal MontoTarjetaMS {
            get { return _MontoTarjetaMS; }
            set { _MontoTarjetaMS = value; }
        }

        public decimal MontoC2P {
            get { return _MontoC2P; }
            set { _MontoC2P = value; }
        }

        public decimal MontoTransferenciaMS {
            get { return _MontoTransferenciaMS; }
            set { _MontoTransferenciaMS = value; }
        }

        public decimal MontoPagoMovil {
            get { return _MontoPagoMovil; }
            set { _MontoPagoMovil = value; }
        }
		
		public decimal MontoDepositoMS {
            get { return _MontoDepositoMS; }
            set { _MontoDepositoMS = value; }
        }

        public DateTime Fecha {
            get { return _Fecha; }
            set { _Fecha = LibConvert.DateToDbValue(value); }
        }

        public string HoraApertura {
            get { return _HoraApertura; }
            set { _HoraApertura = LibString.Mid(value, 0, 5); }
        }

        public string HoraCierre {
            get { return _HoraCierre; }
            set { _HoraCierre = LibString.Mid(value, 0, 5); }
        }

        public bool CajaCerradaAsBool {
            get { return _CajaCerrada; }
            set { _CajaCerrada = value; }
        }

        public string CajaCerrada {
            set { _CajaCerrada = LibConvert.SNToBool(value); }
        }


        public string CodigoMoneda {
            get { return _CodigoMoneda; }
            set { _CodigoMoneda = LibString.Mid(value, 0, 4); }
        }

        public string Moneda {
            get { return _Moneda; }
            set { _Moneda = LibString.Mid(value, 0, 80); }
        }

        public decimal Cambio {
            get { return _Cambio; }
            set { _Cambio = value; }
        }

        public decimal MontoAperturaME {
            get { return _MontoAperturaME; }
            set { _MontoAperturaME = value; }
        }

        public decimal MontoCierreME {
            get { return _MontoCierreME; }
            set { _MontoCierreME = value; }
        }

        public decimal MontoEfectivoME {
            get { return _MontoEfectivoME; }
            set { _MontoEfectivoME = value; }
        }

        public decimal MontoTarjetaME {
            get { return _MontoTarjetaME; }
            set { _MontoTarjetaME = value; }
        }

        public decimal MontoChequeME {
            get { return _MontoChequeME; }
            set { _MontoChequeME = value; }
        }

        public decimal MontoDepositoME {
            get { return _MontoDepositoME; }
            set { _MontoDepositoME = value; }
        }

        public decimal MontoAnticipoME {
            get { return _MontoAnticipoME; }
            set { _MontoAnticipoME = value; }
        }

        public decimal MontoVueltoME {
            get { return _MontoVueltoME; }
            set { _MontoVueltoME = value; }
        }

        public decimal MontoZelle {
            get { return _MontoZelle; }
            set { _MontoZelle = value; }
        }

        public string NombreOperador {
            get { return _NombreOperador; }
            set { _NombreOperador = LibString.Mid(value, 0, 10); }
        }

        public DateTime FechaUltimaModificacion {
            get { return _FechaUltimaModificacion; }
            set { _FechaUltimaModificacion = LibConvert.DateToDbValue(value); }
        }

        public long fldTimeStamp {
            get { return _fldTimeStamp; }
            set { _fldTimeStamp = value; }
        }

        public XmlDocument Datos {
            get { return _datos; }
            set { _datos = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public CajaApertura() {
            Clear();
        }
        #endregion //Constructores
        #region Metodos Generados

        public object TextDateLastModifiedForInput() {
            return "";
        }

        public void Clear() {
            ConsecutivoCompania = LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania");
            Consecutivo = 0;
            ConsecutivoCaja = 0;
            NombreCaja = string.Empty;
            NombreDelUsuario = string.Empty;
            MontoApertura = 0;
            MontoCierre = 0;
            MontoEfectivo = 0;
            MontoTarjeta = 0;
            MontoCheque = 0;
            MontoDeposito = 0;
            MontoAnticipo = 0;
            MontoVuelto = 0;
            MontoVueltoPM = 0;
			MontoTarjetaMS = 0;
            MontoC2P = 0;
            MontoTransferenciaMS = 0;
            MontoPagoMovil = 0;
            MontoDepositoMS = 0;
            Fecha = LibDate.Today();
            HoraApertura = string.Empty;
            HoraCierre = string.Empty;
            CajaCerradaAsBool = false;
            CodigoMoneda = string.Empty;
            Moneda = string.Empty;
            Cambio = 0;
            MontoAperturaME = 0;
            MontoCierreME = 0;
            MontoEfectivoME = 0;
            MontoTarjetaME = 0;
            MontoChequeME = 0;
            MontoDepositoME = 0;
            MontoAnticipoME = 0;
            MontoVueltoME = 0;
            MontoZelle = 0;
            NombreOperador = string.Empty;
            FechaUltimaModificacion = LibDate.Today();
            fldTimeStamp = 0;
        }

        public CajaApertura Clone() {
            CajaApertura vResult = new CajaApertura();
            vResult.ConsecutivoCompania = _ConsecutivoCompania;
            vResult.Consecutivo = _Consecutivo;
            vResult.ConsecutivoCaja = _ConsecutivoCaja;
            vResult.NombreCaja = _NombreCaja;
            vResult.NombreDelUsuario = _NombreDelUsuario;
            vResult.MontoApertura = _MontoApertura;
            vResult.MontoCierre = _MontoCierre;
            vResult.MontoEfectivo = _MontoEfectivo;
            vResult.MontoTarjeta = _MontoTarjeta;
            vResult.MontoCheque = _MontoCheque;
            vResult.MontoDeposito = _MontoDeposito;
            vResult.MontoAnticipo = _MontoAnticipo;
            vResult.MontoVuelto = _MontoVuelto;
            vResult.MontoVueltoPM = _MontoVueltoPM;
            vResult.MontoTarjetaMS = _MontoTarjetaMS;
            vResult.MontoC2P = _MontoC2P;
            vResult.MontoTransferenciaMS = _MontoTransferenciaMS;
            vResult.MontoPagoMovil = _MontoPagoMovil;
            vResult.MontoDepositoMS = _MontoDepositoMS;
            vResult.Fecha = _Fecha;
            vResult.HoraApertura = _HoraApertura;
            vResult.HoraCierre = _HoraCierre;
            vResult.CajaCerradaAsBool = _CajaCerrada;
            vResult.CodigoMoneda = _CodigoMoneda;
            vResult.Moneda = _Moneda;
            vResult.Cambio = _Cambio;
            vResult.MontoAperturaME = _MontoAperturaME;
            vResult.MontoCierreME = _MontoCierreME;
            vResult.MontoEfectivoME = _MontoEfectivoME;
            vResult.MontoTarjetaME = _MontoTarjetaME;
            vResult.MontoChequeME = _MontoChequeME;
            vResult.MontoDepositoME = _MontoDepositoME;
            vResult.MontoAnticipoME = _MontoAnticipoME;
            vResult.MontoVueltoME = _MontoVueltoME;
            vResult.MontoZelle = _MontoZelle;
            vResult.NombreOperador = _NombreOperador;
            vResult.FechaUltimaModificacion = _FechaUltimaModificacion;
            vResult.fldTimeStamp = _fldTimeStamp;
            return vResult;
        }

        public override string ToString() {
           return "Consecutivo Compania = " + _ConsecutivoCompania.ToString() +
               "\nConsecutivo = " + _Consecutivo.ToString() +
               "\nConsecutivo Caja = " + _ConsecutivoCaja.ToString() +
               "\nNombreDelUsuario = " + _NombreDelUsuario +
               "\nMonto Apertura = " + _MontoApertura.ToString() +
               "\nMonto Cierre = " + _MontoCierre.ToString() +
               "\nMonto Efectivo = " + _MontoEfectivo.ToString() +
               "\nMonto Tarjeta = " + _MontoTarjeta.ToString() +
               "\nMonto Cheque = " + _MontoCheque.ToString() +
               "\nMonto Deposito = " + _MontoDeposito.ToString() +
               "\nMonto Anticipo = " + _MontoAnticipo.ToString() +
               "\nMonto Vuelto = " + _MontoVuelto.ToString() +
               "\nMonto Vuelto Pago Móvil = " + _MontoVueltoPM.ToString() +
               "\nMonto Tarjeta MS = " + _MontoTarjetaMS.ToString() +
               "\nMonto C2P = " + _MontoC2P.ToString() +
               "\nMonto Transferencia MS = " + _MontoTransferenciaMS.ToString() +
               "\nMonto Pago Movil = " + _MontoPagoMovil.ToString() +
               "\nMonto Deposito MS = " + _MontoDepositoMS.ToString() +
               "\nFecha = " + _Fecha.ToShortDateString() +
               "\nHora Apertura = " + _HoraApertura +
               "\nHora Cierre = " + _HoraCierre +
               "\nCaja Cerrada = " + _CajaCerrada +
               "\nCodigo Moneda = " + _CodigoMoneda +
               "\nCambio = " + _Cambio.ToString() +
               "\nMonto Apertura ME = " + _MontoAperturaME.ToString() +
               "\nMonto Cierre ME = " + _MontoCierreME.ToString() +
               "\nMonto Efectivo ME = " + _MontoEfectivoME.ToString() +
               "\nMonto Tarjeta ME = " + _MontoTarjetaME.ToString() +
               "\nMonto Cheque ME = " + _MontoChequeME.ToString() +
               "\nMonto Deposito ME = " + _MontoDepositoME.ToString() +
               "\nMonto Anticipo ME = " + _MontoAnticipoME.ToString() +
               "\nMonto Vuelto ME = " + _MontoVueltoME.ToString() +
               "\nMonto Zelle = " + _MontoZelle.ToString() +
               "\nNombre Operador = " + _NombreOperador +
               "\nFecha Ultima Modificacion = " + _FechaUltimaModificacion.ToShortDateString();
        }
        #endregion //Metodos Generados


    } //End of class CajaApertura

} //End of namespace Galac.Adm.Ccl.Venta

