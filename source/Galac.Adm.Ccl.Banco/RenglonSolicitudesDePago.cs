using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;
using Galac.Adm.Ccl.Banco;

namespace Galac.Adm.Ccl.Banco {
    [Serializable]
    public class RenglonSolicitudesDePago: IEquatable<RenglonSolicitudesDePago> {
        #region Variables
        private int _ConsecutivoCompania;
        private int _ConsecutivoSolicitud;
        private int _consecutivoRenglon;
        private string _CuentaBancaria;
        private int _ConsecutivoBeneficiario;
        private eTipoDeFormaDePagoSolicitud _FormaDePago;
        private eStatusSolicitudRenglon _Status;
        private decimal _Monto;
        private string _NumeroDocumento;
        private bool _Contabilizado;
        #endregion //Variables
        #region Propiedades

        public int ConsecutivoCompania {
            get { return _ConsecutivoCompania; }
            set { _ConsecutivoCompania = value; }
        }

        public int ConsecutivoSolicitud {
            get { return _ConsecutivoSolicitud; }
            set { _ConsecutivoSolicitud = value; }
        }

        public int consecutivoRenglon {
            get { return _consecutivoRenglon; }
            set { _consecutivoRenglon = value; }
        }

        public string CuentaBancaria {
            get { return _CuentaBancaria; }
            set { _CuentaBancaria = LibString.Mid(value, 0, 5); }
        }

        public int ConsecutivoBeneficiario {
            get { return _ConsecutivoBeneficiario; }
            set { _ConsecutivoBeneficiario = value; }
        }

        public eTipoDeFormaDePagoSolicitud FormaDePagoAsEnum {
            get { return _FormaDePago; }
            set { _FormaDePago = value; }
        }

        public string FormaDePago {
            set { _FormaDePago = (eTipoDeFormaDePagoSolicitud)LibConvert.DbValueToEnum(value); }
        }

        public string FormaDePagoAsDB {
            get { return LibConvert.EnumToDbValue((int) _FormaDePago); }
        }

        public string FormaDePagoAsString {
            get { return LibEnumHelper.GetDescription(_FormaDePago); }
        }

        public eStatusSolicitudRenglon StatusAsEnum {
            get { return _Status; }
            set { _Status = value; }
        }

        public string Status {
            set { _Status = (eStatusSolicitudRenglon)LibConvert.DbValueToEnum(value); }
        }

        public string StatusAsDB {
            get { return LibConvert.EnumToDbValue((int) _Status); }
        }

        public string StatusAsString {
            get { return LibEnumHelper.GetDescription(_Status); }
        }

        public decimal Monto {
            get { return _Monto; }
            set { _Monto = value; }
        }

        public string NumeroDocumento {
            get { return _NumeroDocumento; }
            set { _NumeroDocumento = LibString.Mid(value, 0, 15); }
        }

        public bool ContabilizadoAsBool {
            get { return _Contabilizado; }
            set { _Contabilizado = value; }
        }

        public string Contabilizado {
            set { _Contabilizado = LibConvert.SNToBool(value); }
        }

        #endregion //Propiedades
        #region Constructores
        #endregion //Constructores
        #region Metodos Generados

        public object TextDateLastModifiedForInput() {
            return "";
        }

        public void Clear() {
            ConsecutivoCompania = 0;
            ConsecutivoSolicitud = 0;
            consecutivoRenglon = 0;
            CuentaBancaria = "";
            ConsecutivoBeneficiario = 0;
            FormaDePagoAsEnum = eTipoDeFormaDePagoSolicitud.Efectivo;
            StatusAsEnum = eStatusSolicitudRenglon.PorProcesar;
            Monto = 0;
            NumeroDocumento = "";
            ContabilizadoAsBool = false;
        }

        public RenglonSolicitudesDePago Clone() {
            RenglonSolicitudesDePago vResult = new RenglonSolicitudesDePago();
            vResult.ConsecutivoCompania = _ConsecutivoCompania;
            vResult.ConsecutivoSolicitud = _ConsecutivoSolicitud;
            vResult.consecutivoRenglon = _consecutivoRenglon;
            vResult.CuentaBancaria = _CuentaBancaria;
            vResult.ConsecutivoBeneficiario = _ConsecutivoBeneficiario;
            vResult.FormaDePagoAsEnum = _FormaDePago;
            vResult.StatusAsEnum = _Status;
            vResult.Monto = _Monto;
            vResult.NumeroDocumento = _NumeroDocumento;
            vResult.ContabilizadoAsBool = _Contabilizado;
            return vResult;
        }

        public override string ToString() {
           return "Consecutivo Compania = " + _ConsecutivoCompania.ToString() +
               "\nConsecutivo Solicitud = " + _ConsecutivoSolicitud.ToString() +
               "\nconsecutivo Renglon = " + _consecutivoRenglon.ToString() +
               "\nCuenta Bancaria = " + _CuentaBancaria +
               "\nConsecutivo Beneficiario = " + _ConsecutivoBeneficiario.ToString() +
               "\nForma De Pago = " + _FormaDePago.ToString() +
               "\nStatus = " + _Status.ToString() +
               "\nMonto = " + _Monto.ToString() +
               "\nNumero Documento  = " + _NumeroDocumento +
               "\nContabilizado = " + _Contabilizado;
        }

        #region Miembros de IEquatable<RenglonSolicitudesDePago>
        bool IEquatable<RenglonSolicitudesDePago>.Equals(RenglonSolicitudesDePago other) {
            if (this._ConsecutivoCompania == other.ConsecutivoCompania
                && this._ConsecutivoSolicitud == other.ConsecutivoSolicitud
                && this._consecutivoRenglon == other.consecutivoRenglon) {
                return true;
            } else {
                return false;
            }
        }
        #endregion //IEquatable<RenglonSolicitudesDePago>
        #endregion //Metodos Generados


    } //End of class RenglonSolicitudesDePago

} //End of namespace Galac.Dbo.Ccl.RenglonSolicitudesDePago

