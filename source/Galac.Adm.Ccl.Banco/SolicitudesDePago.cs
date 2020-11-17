using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using Galac.Adm.Ccl.Banco;

namespace Galac.Adm.Ccl.Banco {
    [Serializable]
    public class SolicitudesDePago {
        #region Variables
        private int _ConsecutivoCompania;
        private int _ConsecutivoSolicitud;
        private int _NumeroDocumentoOrigen;
        private DateTime _FechaSolicitud;
        private eStatusSolicitud _Status;
        private eSolicitudGeneradaPor _GeneradoPor;
        private string _Observaciones;
        private string _NombreOperador;
        private DateTime _FechaUltimaModificacion;
        private long _fldTimeStamp;
		private GBindingList<RenglonSolicitudesDePago> _DetailRenglonSolicitudesDePago;
        XmlDocument _datos;
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

        public int NumeroDocumentoOrigen {
            get { return _NumeroDocumentoOrigen; }
            set { _NumeroDocumentoOrigen = value; }
        }

        public DateTime FechaSolicitud {
            get { return _FechaSolicitud; }
            set { _FechaSolicitud = LibConvert.DateToDbValue(value); }
        }

        public eStatusSolicitud StatusAsEnum {
            get { return _Status; }
            set { _Status = value; }
        }

        public string Status {
            set { _Status = (eStatusSolicitud)LibConvert.DbValueToEnum(value); }
        }

        public string StatusAsDB {
            get { return LibConvert.EnumToDbValue((int) _Status); }
        }

        public string StatusAsString {
            get { return LibEnumHelper.GetDescription(_Status); }
        }

        public eSolicitudGeneradaPor GeneradoPorAsEnum {
            get { return _GeneradoPor; }
            set { _GeneradoPor = value; }
        }

        public string GeneradoPor {
            set { _GeneradoPor = (eSolicitudGeneradaPor)LibConvert.DbValueToEnum(value); }
        }

        public string GeneradoPorAsDB {
            get { return LibConvert.EnumToDbValue((int) _GeneradoPor); }
        }

        public string GeneradoPorAsString {
            get { return LibEnumHelper.GetDescription(_GeneradoPor); }
        }

        public string Observaciones {
            get { return _Observaciones; }
            set { _Observaciones = LibString.Mid(value, 0, 40); }
        }

        public string NombreOperador {
            get { return _NombreOperador; }
            set { _NombreOperador = LibString.Mid(value, 0, 20); }
        }

        public DateTime FechaUltimaModificacion {
            get { return _FechaUltimaModificacion; }
            set { _FechaUltimaModificacion = LibConvert.DateToDbValue(value); }
        }

        public long fldTimeStamp {
            get { return _fldTimeStamp; }
            set { _fldTimeStamp = value; }
        }

        public GBindingList<RenglonSolicitudesDePago> DetailRenglonSolicitudesDePago {
            get { return _DetailRenglonSolicitudesDePago; }
            set { _DetailRenglonSolicitudesDePago = value; }
        }

        public XmlDocument Datos {
            get { return _datos; }
            set { _datos = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public SolicitudesDePago() {
            _DetailRenglonSolicitudesDePago = new GBindingList<RenglonSolicitudesDePago>();
            Clear();
        }
        #endregion //Constructores
        #region Metodos Generados

        public object TextDateLastModifiedForInput() {
            return "";
        }

        public void Clear() {
            ConsecutivoCompania = 0;
            ConsecutivoSolicitud = 0;
            NumeroDocumentoOrigen = 0;
            FechaSolicitud = LibDate.Today();
            StatusAsEnum = eStatusSolicitud.PorProcesar;
            GeneradoPorAsEnum = eSolicitudGeneradaPor.CalculosDeNomina;
            Observaciones = "";
            NombreOperador = "";
            FechaUltimaModificacion = LibDate.Today();
            fldTimeStamp = 0;
        }

        public SolicitudesDePago Clone() {
            SolicitudesDePago vResult = new SolicitudesDePago();
            vResult.ConsecutivoCompania = _ConsecutivoCompania;
            vResult.ConsecutivoSolicitud = _ConsecutivoSolicitud;
            vResult.NumeroDocumentoOrigen = _NumeroDocumentoOrigen;
            vResult.FechaSolicitud = _FechaSolicitud;
            vResult.StatusAsEnum = _Status;
            vResult.GeneradoPorAsEnum = _GeneradoPor;
            vResult.Observaciones = _Observaciones;
            vResult.NombreOperador = _NombreOperador;
            vResult.FechaUltimaModificacion = _FechaUltimaModificacion;
            vResult.fldTimeStamp = _fldTimeStamp;
            return vResult;
        }

        public override string ToString() {
           return "Consecutivo Compania = " + _ConsecutivoCompania.ToString() +
               "\nConsecutivo Solicitud = " + _ConsecutivoSolicitud.ToString() +
               "\nNumero Documento Origen = " + _NumeroDocumentoOrigen.ToString() +
               "\nFecha de Solicitud = " + _FechaSolicitud.ToShortDateString() +
               "\nStatus = " + _Status.ToString() +
               "\nGenerada = " + _GeneradoPor.ToString() +
               "\nObservaciones = " + _Observaciones +
               "\nNombre Operador = " + _NombreOperador +
               "\nFecha Ultima Modificacion = " + _FechaUltimaModificacion.ToShortDateString();
        }
        #endregion //Metodos Generados


    } //End of class SolicitudesDePago

} //End of namespace Galac.Adm.Ccl.Banco

