using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;

namespace Galac.Adm.Ccl.CajaChica {
    [Serializable]
    public class Rendicion {
        #region Variables
        private int _ConsecutivoCompania;
        private int _Consecutivo;
        private string _Numero;
        private eTipoDeDocumentoRendicion _TipoDeDocumento;
        private int _ConsecutivoBeneficiario;
        private string _CodigoBeneficiario;
        private string _NombreBeneficiario;
        private DateTime _FechaApertura;
        private string _CodigoCtaBancariaCajaChica;
        private string _NombreCuentaBancariaCajaChica;
        private string _Descripcion;
        private eStatusRendicion _StatusRendicion;
        private DateTime _FechaCierre;
        private string _CodigoCuentaBancaria;
        private string _NombreCuentaBancaria;
        private bool _GeneraImpuestoBancario;
        private string _NumeroDocumento;
        private string _BeneficiarioCheque;
        private string _CodigoConceptoBancario;
        private string _NombreConceptoBancario;
        private DateTime _FechaAnulacion;
        private decimal _TotalAdelantos;
        private decimal _TotalGastos;
        private decimal _TotalIVA;
        private decimal _TotalRetencion;
        private string _Observaciones;
        private string _NombreOperador;
        private DateTime _FechaUltimaModificacion;
        private long _fldTimeStamp;
		private ObservableCollection<DetalleDeRendicion> _DetailDetalleDeRendicion;
        private ObservableCollection<Anticipo> _Adelantos; 

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

        public string Numero {
            get { return _Numero; }
            set { _Numero = LibString.Mid(value, 0, 15); }
        }
        public eTipoDeDocumentoRendicion TipoDeDocumentoAsEnum {
            get { return _TipoDeDocumento; }
            set { _TipoDeDocumento = value; }
        }
        public string TipoDeDocumento {
            set { _TipoDeDocumento = (eTipoDeDocumentoRendicion)LibConvert.DbValueToEnum(value); }
        }
        public string TipoDeDocumentoAsDB {
            get { return LibConvert.EnumToDbValue((int) _TipoDeDocumento); }
        }
        public string TipoDeDocumentoAsString {
            get { return LibEnumHelper.GetDescription(_TipoDeDocumento); }
        }
        public int ConsecutivoBeneficiario {
            get { return _ConsecutivoBeneficiario; }
            set { _ConsecutivoBeneficiario = value; }
        }
        [Obsolete("esto se agrego pero nunca se hizo la implementacion asociada completa, alerta con el uso siempre toma al beneficiario por defecto")]
        public string CodigoBeneficiario {
            get { return _CodigoBeneficiario; }
            set { _CodigoBeneficiario = LibString.Mid(value, 0, 10); }
        }
        [Obsolete("esto se agrego pero nunca se hizo la implementacion asociada completa, alerta con el uso siempre toma al beneficiario por defecto")]
        public string NombreBeneficiario {
            get { return _NombreBeneficiario; }
            set { _NombreBeneficiario = LibString.Mid(value, 0, 80); }
        }

        public DateTime FechaApertura {
            get { return _FechaApertura; }
            set { _FechaApertura = LibConvert.DateToDbValue(value); }
        }

        public string CodigoCtaBancariaCajaChica {
            get { return _CodigoCtaBancariaCajaChica; }
            set { _CodigoCtaBancariaCajaChica = LibString.Mid(value, 0, 5); }
        }

        public string NombreCuentaBancariaCajaChica {
            get { return _NombreCuentaBancariaCajaChica; }
            set { _NombreCuentaBancariaCajaChica = LibString.Mid(value, 0, 40); }
        }

        public string Descripcion {
            get { return _Descripcion; }
            set { _Descripcion = LibString.Mid(value, 0, 50); }
        }

        public eStatusRendicion StatusRendicionAsEnum {
            get { return _StatusRendicion; }
            set { _StatusRendicion = value; }
        }

        public string StatusRendicion {
            set { _StatusRendicion = (eStatusRendicion)LibConvert.DbValueToEnum(value); }
        }

        public string StatusRendicionAsDB {
            get { return LibConvert.EnumToDbValue((int)_StatusRendicion); }
        }

        public string StatusRendicionAsString {
            get { return LibEnumHelper.GetDescription(_StatusRendicion); }
        }

        public DateTime FechaCierre {
            get { return _FechaCierre; }
            set { _FechaCierre = LibConvert.DateToDbValue(value); }
        }

        public string CodigoCuentaBancaria {
            get { return _CodigoCuentaBancaria; }
            set { _CodigoCuentaBancaria = LibString.Mid(value, 0, 5); }
        }

        public string NombreCuentaBancaria {
            get { return _NombreCuentaBancaria; }
            set { _NombreCuentaBancaria = LibString.Mid(value, 0, 40); }
        }

        public bool GeneraImpuestoBancarioAsBool {
            get { return _GeneraImpuestoBancario; }
            set { _GeneraImpuestoBancario = value; }
        }
        public string GeneraImpuestoBancario {
            set { _GeneraImpuestoBancario = LibConvert.SNToBool(value); }
        }
        public string NumeroDocumento {
            get { return _NumeroDocumento; }
            set { _NumeroDocumento = LibString.Mid(value, 0, 15); }
        }

        public string BeneficiarioCheque {
            get { return _BeneficiarioCheque; }
            set { _BeneficiarioCheque = LibString.Mid(value, 0, 60); }
        }

        public string CodigoConceptoBancario {
            get { return _CodigoConceptoBancario; }
            set { _CodigoConceptoBancario = LibString.Mid(value, 0, 8); }
        }

        public string NombreConceptoBancario {
            get { return _NombreConceptoBancario; }
            set { _NombreConceptoBancario = LibString.Mid(value, 0, 30); }
        }

        public DateTime FechaAnulacion {
            get { return _FechaAnulacion; }
            set { _FechaAnulacion = LibConvert.DateToDbValue(value); }
        }

        public decimal TotalAdelantos {
            get { return _TotalAdelantos; }
            set { _TotalAdelantos = value; }
        }

        public decimal TotalGastos {
            get { return _TotalGastos; }
            set { _TotalGastos = value; }
        }

        public decimal TotalIVA {
           get {
              if (DetailDetalleDeRendicion != null)
                 return DetailDetalleDeRendicion.Sum(p => p.MontoIVA + p.MontoIVAAlicuotaEspecial1 + p.MontoIVAAlicuotaEspecial2);
              else
                 return 0;
           }
           set { _TotalIVA = value; }
        }

        public decimal TotalRetencion {
            get {
                if (DetailDetalleDeRendicion != null)
                    return DetailDetalleDeRendicion.Sum(p => p.MontoRetencion );
                else
                    return 0;
            }
            set { _TotalRetencion = value; }
        }

        public decimal TotalExento {
           get {
              if (DetailDetalleDeRendicion != null)
                 return DetailDetalleDeRendicion.Sum(p => p.MontoExento);
              else
                 return 0;
           }
        }

        public decimal TotalGravable {
           get {
              if (DetailDetalleDeRendicion != null)
                  return DetailDetalleDeRendicion.Sum(p => p.MontoGravable + p.MontoGravableAlicuotaEspecial1 + p.MontoGravableAlicuotaEspecial2);
              else
                 return 0;
           }
        }

        public decimal Total {
           get {
              return TotalExento + TotalGravable + TotalIVA;
           }
        }

        public string Observaciones {
            get { return _Observaciones; }
            set { _Observaciones = LibString.Mid(value, 0, 200); }
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

        public ObservableCollection<DetalleDeRendicion> DetailDetalleDeRendicion {
            get { return _DetailDetalleDeRendicion; }
            set { _DetailDetalleDeRendicion = value; }
        }

        public ObservableCollection<Anticipo> Adelantos {
            get { return _Adelantos; }
            set { _Adelantos = value; }
        
        }

        public XmlDocument Datos {
            get { return _datos; }
            set { _datos = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public Rendicion() {
            _DetailDetalleDeRendicion = new ObservableCollection<DetalleDeRendicion>();
            _Adelantos = new ObservableCollection<Anticipo>();
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
            Numero = string.Empty;
            TipoDeDocumentoAsEnum = eTipoDeDocumentoRendicion.Redicion;
            ConsecutivoBeneficiario = 0;
            CodigoBeneficiario = string.Empty;
            NombreBeneficiario = string.Empty;
            FechaApertura = LibDate.Today();
            CodigoCtaBancariaCajaChica = string.Empty;
            NombreCuentaBancariaCajaChica = string.Empty;
            Descripcion = string.Empty;
            StatusRendicionAsEnum = eStatusRendicion.EnProceso;
            FechaCierre = LibDate.Today();
            CodigoCuentaBancaria = string.Empty;
            NombreCuentaBancaria = string.Empty;
            GeneraImpuestoBancarioAsBool = false;
            NumeroDocumento = string.Empty;
            BeneficiarioCheque = string.Empty;
            CodigoConceptoBancario = string.Empty;
            NombreConceptoBancario = string.Empty;
            FechaAnulacion = LibDate.Today();
            TotalAdelantos = 0;
            TotalGastos = 0;
            TotalIVA = 0;
            TotalRetencion = 0;
            Observaciones = string.Empty;
            NombreOperador = string.Empty;
            FechaUltimaModificacion = LibDate.Today();
            fldTimeStamp = 0;
            DetailDetalleDeRendicion = new ObservableCollection<DetalleDeRendicion>();
        }

        public Rendicion Clone() {
            Rendicion vResult = new Rendicion();
            vResult.ConsecutivoCompania = _ConsecutivoCompania;
            vResult.Consecutivo = _Consecutivo;
            vResult.Numero = _Numero;
            vResult.TipoDeDocumentoAsEnum = _TipoDeDocumento;
            vResult.ConsecutivoBeneficiario = _ConsecutivoBeneficiario;
            vResult.CodigoBeneficiario = _CodigoBeneficiario;
            vResult.NombreBeneficiario = _NombreBeneficiario;
            vResult.FechaApertura = _FechaApertura;
            vResult.CodigoCtaBancariaCajaChica = _CodigoCtaBancariaCajaChica;
            vResult.NombreCuentaBancariaCajaChica = _NombreCuentaBancariaCajaChica;
            vResult.Descripcion = _Descripcion;
            vResult.StatusRendicionAsEnum = _StatusRendicion;
            vResult.FechaCierre = _FechaCierre;
            vResult.CodigoCuentaBancaria = _CodigoCuentaBancaria;
            vResult.NombreCuentaBancaria = _NombreCuentaBancaria;
            vResult.GeneraImpuestoBancarioAsBool = _GeneraImpuestoBancario;
            vResult.NumeroDocumento = _NumeroDocumento;
            vResult.BeneficiarioCheque = _BeneficiarioCheque;
            vResult.CodigoConceptoBancario = _CodigoConceptoBancario;
            vResult.NombreConceptoBancario = _NombreConceptoBancario;
            vResult.FechaAnulacion = _FechaAnulacion;
            vResult.TotalAdelantos = _TotalAdelantos;
            vResult.TotalGastos = _TotalGastos;
            vResult.TotalIVA = _TotalIVA;
            vResult.TotalRetencion = _TotalRetencion;
            vResult.Observaciones = _Observaciones;
            vResult.NombreOperador = _NombreOperador;
            vResult.FechaUltimaModificacion = _FechaUltimaModificacion;
            vResult.fldTimeStamp = _fldTimeStamp;
            return vResult;
        }

        public override string ToString() {
            return "Consecutivo Compania = " + _ConsecutivoCompania.ToString() +
                "\nConsecutivo = " + _Consecutivo.ToString() +
                "\nNúmero = " + _Numero +
                "\nTipo De Documento = " + _TipoDeDocumento.ToString() +
                "\nConsecutivo Beneficiario = " + _ConsecutivoBeneficiario.ToString() +
                "\nFecha de Apertura = " + _FechaApertura.ToShortDateString() +
                "\nCaja Chica = " + _CodigoCtaBancariaCajaChica +
                "\nDescripción = " + _Descripcion +
                "\nEstado = " + _StatusRendicion.ToString() +
                "\nFecha de Cierre = " + _FechaCierre.ToShortDateString() +
                "\nCuenta Bancaria para Reposición = " + _CodigoCuentaBancaria +
                "\nGenerar Impuesto Bancario = " + _GeneraImpuestoBancario +
                "\nNúmero de Cheque = " + _NumeroDocumento +
                "\nBeneficiario = " + _BeneficiarioCheque +
                "\nCódigo Concepto = " + _CodigoConceptoBancario +
                "\nFecha de Anulación = " + _FechaAnulacion.ToShortDateString() +
                "\nTotal Adelantos = " + _TotalAdelantos.ToString() +
                "\nTotal Gastos = " + _TotalGastos.ToString() +
                "\nTotal IVA = " + _TotalIVA.ToString() +
                "\nTotal Retención = " + _TotalRetencion.ToString() +
                "\nObservaciones = " + _Observaciones +
                "\nNombre Operador = " + _NombreOperador +
                "\nFecha Ultima Modificacion = " + _FechaUltimaModificacion.ToShortDateString();
        }
        #endregion //Metodos Generados


    } //End of class Rendiciones

} //End of namespace Galac.Saw.Ccl.Rendicion

