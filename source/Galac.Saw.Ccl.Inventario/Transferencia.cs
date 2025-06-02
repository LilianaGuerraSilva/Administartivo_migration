using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using System.Collections.ObjectModel;

namespace Galac.Saw.Ccl.Inventario {
    [Serializable]
    public class Transferencia {
        #region Variables
        private int _ConsecutivoCompania;
        private string _NumeroDocumento;
        private string _CodigoAlmacenEntrada;
        private string _NombreAlmacenEntrada;
        private int _ConsecutivoAlmacenEntrada;
        private string _CodigoAlmacenSalida;
        private string _NombreAlmacenSalida;
        private int _ConsecutivoAlmacenSalida;
        private DateTime _Fecha;
        private string _Comentarios;
        private string _NombreOperador;
        private DateTime _FechaUltimaModificacion;
        private long _fldTimeStamp;
		private ObservableCollection<RenglonTransferencia> _DetailRenglonTransferencia;
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

        public int ConsecutivoAlmacenEntrada {
            get { return _ConsecutivoAlmacenEntrada; }
            set { _ConsecutivoAlmacenEntrada = value; }
        }

        public string CodigoAlmacenSalida {
            get { return _CodigoAlmacenSalida; }
            set { _CodigoAlmacenSalida = LibString.Mid(value, 0, 5); }
        }

        public string NombreAlmacenSalida {
            get { return _NombreAlmacenSalida; }
            set { _NombreAlmacenSalida = LibString.Mid(value, 0, 40); }
        }

        public int ConsecutivoAlmacenSalida {
            get { return _ConsecutivoAlmacenSalida; }
            set { _ConsecutivoAlmacenSalida = value; }
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

        public ObservableCollection<RenglonTransferencia> DetailRenglonTransferencia {
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
            _DetailRenglonTransferencia = new ObservableCollection<RenglonTransferencia>();
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
            ConsecutivoAlmacenEntrada = 0;
            CodigoAlmacenSalida = "";
            NombreAlmacenSalida = "";
            ConsecutivoAlmacenSalida = 0;
            Fecha = LibDate.Today();
            Comentarios = "";
            NombreOperador = "";
            FechaUltimaModificacion = LibDate.Today();
            fldTimeStamp = 0;
            DetailRenglonTransferencia = new ObservableCollection<RenglonTransferencia>();
        }

        public Transferencia Clone() {
            Transferencia vResult = new Transferencia();
            vResult.ConsecutivoCompania = _ConsecutivoCompania;
            vResult.NumeroDocumento = _NumeroDocumento;
            vResult.CodigoAlmacenEntrada = _CodigoAlmacenEntrada;
            vResult.NombreAlmacenEntrada = _NombreAlmacenEntrada;
            vResult.ConsecutivoAlmacenEntrada = _ConsecutivoAlmacenEntrada;
            vResult.CodigoAlmacenSalida = _CodigoAlmacenSalida;
            vResult.NombreAlmacenSalida = _NombreAlmacenSalida;
            vResult.ConsecutivoAlmacenSalida = _ConsecutivoAlmacenSalida;
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
               "\nConsecutivo Almacen Entrada = " + _ConsecutivoAlmacenEntrada.ToString() +
               "\nCodigo Almacen Salida = " + _CodigoAlmacenSalida +
               "\nConsecutivo Almacen Salida = " + _ConsecutivoAlmacenSalida.ToString() +
               "\nFecha = " + _Fecha.ToShortDateString() +
               "\nComentarios = " + _Comentarios +
               "\nNombre Operador = " + _NombreOperador +
               "\nFecha Ultima Modificacion = " + _FechaUltimaModificacion.ToShortDateString();
        }
        #endregion //Metodos Generados


    } //End of class Transferencia

} //End of namespace Galac.Saw.Ccl.Inventario

