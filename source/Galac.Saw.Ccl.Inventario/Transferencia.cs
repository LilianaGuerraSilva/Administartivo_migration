using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;

namespace Galac.Saw.Ccl.Inventario {
    [Serializable]
    public class Transferencia {
        #region Variables
        private int _ConsecutivoCompania;
        private string _NumeroDocumento;
        private string _CodigoAlmacenEntrada;
        private string _NombreAlmacenEntrada;
        private string _CodigoAlmacenSalida;
        private string _NombreAlmacenSalida;
        private DateTime _Fecha;
        private string _Comentarios;
        private string _NombreOperador;
        private DateTime _FechaUltimaModificacion;
        private long _fldTimeStamp;
		private GBindingList<RenglonTransferencia> _DetailRenglonTransferencia;
        XmlDocument _datos;
        #endregion //Variables
        #region Propiedades

        public int ConsecutivoCompania {
            get { return _ConsecutivoCompania; }
            set { _ConsecutivoCompania = value; }
        }

        public string NumeroDocumento {
            get { return _NumeroDocumento; }
            set { _NumeroDocumento = LibString.Mid(value, 0, 10); }
        }

        public string CodigoAlmacenEntrada {
            get { return _CodigoAlmacenEntrada; }
            set { _CodigoAlmacenEntrada = LibString.Mid(value, 0, 5); }
        }

        public string NombreAlmacenEntrada {
            get { return _NombreAlmacenEntrada; }
            set { _NombreAlmacenEntrada = LibString.Mid(value, 0, 40); }
        }

        public string CodigoAlmacenSalida {
            get { return _CodigoAlmacenSalida; }
            set { _CodigoAlmacenSalida = LibString.Mid(value, 0, 5); }
        }

        public string NombreAlmacenSalida {
            get { return _NombreAlmacenSalida; }
            set { _NombreAlmacenSalida = LibString.Mid(value, 0, 40); }
        }

        public DateTime Fecha {
            get { return _Fecha; }
            set { _Fecha = LibConvert.DateToDbValue(value); }
        }

        public string Comentarios {
            get { return _Comentarios; }
            set { _Comentarios = LibString.Mid(value, 0, 255); }
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

        public GBindingList<RenglonTransferencia> DetailRenglonTransferencia {
            get { return _DetailRenglonTransferencia; }
            set { _DetailRenglonTransferencia = value; }
        }

        public XmlDocument Datos {
            get { return _datos; }
            set { _datos = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public Transferencia() {
            _DetailRenglonTransferencia = new GBindingList<RenglonTransferencia>();
            Clear();
        }
        #endregion //Constructores
        #region Metodos Generados

        public object TextDateLastModifiedForInput() {
            return "";
        }

        public void Clear() {
            ConsecutivoCompania = 0;
            NumeroDocumento = "";
            CodigoAlmacenEntrada = "";
            NombreAlmacenEntrada = "";
            CodigoAlmacenSalida = "";
            NombreAlmacenSalida = "";
            Fecha = LibDate.Today();
            Comentarios = "";
            NombreOperador = "";
            FechaUltimaModificacion = LibDate.Today();
            fldTimeStamp = 0;
        }

        public Transferencia Clone() {
            Transferencia vResult = new Transferencia();
            vResult.ConsecutivoCompania = _ConsecutivoCompania;
            vResult.NumeroDocumento = _NumeroDocumento;
            vResult.CodigoAlmacenEntrada = _CodigoAlmacenEntrada;
            vResult.NombreAlmacenEntrada = _NombreAlmacenEntrada;
            vResult.CodigoAlmacenSalida = _CodigoAlmacenSalida;
            vResult.NombreAlmacenSalida = _NombreAlmacenSalida;
            vResult.Fecha = _Fecha;
            vResult.Comentarios = _Comentarios;
            vResult.NombreOperador = _NombreOperador;
            vResult.FechaUltimaModificacion = _FechaUltimaModificacion;
            vResult.fldTimeStamp = _fldTimeStamp;
            return vResult;
        }

        public override string ToString() {
           return "Consecutivo Compania = " + _ConsecutivoCompania.ToString() +
               "\nNumero Documento = " + _NumeroDocumento +
               "\nNombre Almacén = " + _CodigoAlmacenEntrada +
               "\nCodigo Almacen Salida = " + _CodigoAlmacenSalida +
               "\nFecha = " + _Fecha.ToShortDateString() +
               "\nComentarios = " + _Comentarios +
               "\nNombre Operador = " + _NombreOperador +
               "\nFecha Ultima Modificacion = " + _FechaUltimaModificacion.ToShortDateString();
        }
        #endregion //Metodos Generados


    } //End of class Transferencia

} //End of namespace Galac.Saw.Ccl.Inventario

