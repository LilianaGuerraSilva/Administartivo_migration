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
	public class TransferenciaEntreCuentasBancarias {
		#region Variables
		private int _ConsecutivoCompania;
		private int _Consecutivo;
		private eStatusTransferenciaBancaria _Status;
		private DateTime _Fecha;
		private string _NumeroDocumento;
		private string _Descripcion;
		private DateTime _FechaDeAnulacion;
		private string _CodigoCuentaBancariaOrigen;
		private string _NombreCuentaBancariaOrigen;
		private decimal _SaldoCuentaBancariaOrigen;
		private string _CodigoMonedaCuentaBancariaOrigen;
		private string _NombreMonedaCuentaBancariaOrigen;
		private bool _ManejaDebitoCuentaBancariaOrigen;
		private decimal _CambioABolivaresEgreso;
		private decimal _MontoTransferenciaEgreso;
		private string _CodigoConceptoEgreso;
		private string _DescripcionConceptoEgreso;
		private bool _GeneraComisionEgreso;
		private decimal _MontoComisionEgreso;
		private string _CodigoConceptoComisionEgreso;
		private string _DescripcionConceptoComisionEgreso;
        private bool _GeneraIGTFComisionEgreso;
		private string _CodigoCuentaBancariaDestino;
		private string _NombreCuentaBancariaDestino;
		private decimal _SaldoCuentaBancariaDestino;
		private string _CodigoMonedaCuentaBancariaDestino;
		private string _NombreMonedaCuentaBancariaDestino;
		private bool _ManejaCreditoCuentaBancariaDestino;
		private decimal _CambioABolivaresIngreso;
		private decimal _MontoTransferenciaIngreso;
		private string _CodigoConceptoIngreso;
		private string _DescripcionConceptoIngreso;
		private bool _GeneraComisionIngreso;
		private decimal _MontoComisionIngreso;
		private string _CodigoConceptoComisionIngreso;
		private string _DescripcionConceptoComisionIngreso;
        private bool _GeneraIGTFComisionIngreso;
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

		public eStatusTransferenciaBancaria StatusAsEnum {
			get { return _Status; }
			set { _Status = value; }
		}

		public string Status {
			set { _Status = (eStatusTransferenciaBancaria) LibConvert.DbValueToEnum(value); }
		}

		public string StatusAsDB {
			get { return LibConvert.EnumToDbValue((int) _Status); }
		}

		public string StatusAsString {
			get { return LibEnumHelper.GetDescription(_Status); }
		}

		public DateTime Fecha {
			get { return _Fecha; }
			set { _Fecha = LibConvert.DateToDbValue(value); }
		}

		public string NumeroDocumento {
			get { return _NumeroDocumento; }
			set { _NumeroDocumento = LibString.Mid(value, 0, 20); }
		}

		public string Descripcion {
			get { return _Descripcion; }
			set { _Descripcion = LibString.Mid(value, 0, 255); }
		}

		public DateTime FechaDeAnulacion {
			get { return _FechaDeAnulacion; }
			set { _FechaDeAnulacion = LibConvert.DateToDbValue(value); }
		}

		public string CodigoCuentaBancariaOrigen {
			get { return _CodigoCuentaBancariaOrigen; }
            set { _CodigoCuentaBancariaOrigen = LibString.Mid(value, 0, 8); }
		}

		public string NombreCuentaBancariaOrigen {
			get { return _NombreCuentaBancariaOrigen; }
			set { _NombreCuentaBancariaOrigen = LibString.Mid(value, 0, 40); }
		}

		public decimal SaldoCuentaBancariaOrigen {
			get { return _SaldoCuentaBancariaOrigen; }
			set { _SaldoCuentaBancariaOrigen = value; }
		}

		public string CodigoMonedaCuentaBancariaOrigen {
			get { return _CodigoMonedaCuentaBancariaOrigen; }
			set { _CodigoMonedaCuentaBancariaOrigen = LibString.Mid(value, 0, 4); }
		}

		public string NombreMonedaCuentaBancariaOrigen {
			get { return _NombreMonedaCuentaBancariaOrigen; }
			set { _NombreMonedaCuentaBancariaOrigen = LibString.Mid(value, 0, 80); }
		}

		public bool ManejaDebitoCuentaBancariaOrigenAsBool {
			get { return _ManejaDebitoCuentaBancariaOrigen; }
			set { _ManejaDebitoCuentaBancariaOrigen = value; }
		}

		public string ManejaDebitoCuentaBancariaOrigen {
			set { _ManejaDebitoCuentaBancariaOrigen = LibConvert.SNToBool(value); }
		}

		public decimal CambioABolivaresEgreso {
			get { return _CambioABolivaresEgreso; }
			set { _CambioABolivaresEgreso = value; }
		}

		public decimal MontoTransferenciaEgreso {
			get { return _MontoTransferenciaEgreso; }
			set { _MontoTransferenciaEgreso = value; }
		}

		public string CodigoConceptoEgreso {
			get { return _CodigoConceptoEgreso; }
			set { _CodigoConceptoEgreso = LibString.Mid(value, 0, 8); }
		}

		public string DescripcionConceptoEgreso {
			get { return _DescripcionConceptoEgreso; }
			set { _DescripcionConceptoEgreso = LibString.Mid(value, 0, 30); }
		}

		public bool GeneraComisionEgresoAsBool {
			get { return _GeneraComisionEgreso; }
			set { _GeneraComisionEgreso = value; }
		}

		public string GeneraComisionEgreso {
			set { _GeneraComisionEgreso = LibConvert.SNToBool(value); }
		}

		public decimal MontoComisionEgreso {
			get { return _MontoComisionEgreso; }
			set { _MontoComisionEgreso = value; }
		}

		public string CodigoConceptoComisionEgreso {
			get { return _CodigoConceptoComisionEgreso; }
			set { _CodigoConceptoComisionEgreso = LibString.Mid(value, 0, 8); }
		}

		public string DescripcionConceptoComisionEgreso {
			get { return _DescripcionConceptoComisionEgreso; }
			set { _DescripcionConceptoComisionEgreso = LibString.Mid(value, 0, 30); }
		}

        public bool GeneraIGTFComisionEgresoAsBool {
            get { return _GeneraIGTFComisionEgreso; }
            set { _GeneraIGTFComisionEgreso = value; }
		}

        public string GeneraIGTFComisionEgreso {
            set { _GeneraIGTFComisionEgreso = LibConvert.SNToBool(value); }
		}

		public string CodigoCuentaBancariaDestino {
			get { return _CodigoCuentaBancariaDestino; }
            set { _CodigoCuentaBancariaDestino = LibString.Mid(value, 0, 8); }
		}

		public string NombreCuentaBancariaDestino {
			get { return _NombreCuentaBancariaDestino; }
			set { _NombreCuentaBancariaDestino = LibString.Mid(value, 0, 40); }
		}

		public decimal SaldoCuentaBancariaDestino {
			get { return _SaldoCuentaBancariaDestino; }
			set { _SaldoCuentaBancariaDestino = value; }
		}

		public string CodigoMonedaCuentaBancariaDestino {
			get { return _CodigoMonedaCuentaBancariaDestino; }
			set { _CodigoMonedaCuentaBancariaDestino = LibString.Mid(value, 0, 4); }
		}

		public string NombreMonedaCuentaBancariaDestino {
			get { return _NombreMonedaCuentaBancariaDestino; }
			set { _NombreMonedaCuentaBancariaDestino = LibString.Mid(value, 0, 80); }
		}

		public bool ManejaCreditoCuentaBancariaDestinoAsBool {
			get { return _ManejaCreditoCuentaBancariaDestino; }
			set { _ManejaCreditoCuentaBancariaDestino = value; }
		}

		public string ManejaCreditoCuentaBancariaDestino {
			set { _ManejaCreditoCuentaBancariaDestino = LibConvert.SNToBool(value); }
		}

		public decimal CambioABolivaresIngreso {
			get { return _CambioABolivaresIngreso; }
			set { _CambioABolivaresIngreso = value; }
		}

		public decimal MontoTransferenciaIngreso {
			get { return _MontoTransferenciaIngreso; }
			set { _MontoTransferenciaIngreso = value; }
		}

		public string CodigoConceptoIngreso {
			get { return _CodigoConceptoIngreso; }
			set { _CodigoConceptoIngreso = LibString.Mid(value, 0, 8); }
		}

		public string DescripcionConceptoIngreso {
			get { return _DescripcionConceptoIngreso; }
			set { _DescripcionConceptoIngreso = LibString.Mid(value, 0, 30); }
		}

		public bool GeneraComisionIngresoAsBool {
			get { return _GeneraComisionIngreso; }
			set { _GeneraComisionIngreso = value; }
		}

		public string GeneraComisionIngreso {
			set { _GeneraComisionIngreso = LibConvert.SNToBool(value); }
		}

		public decimal MontoComisionIngreso {
			get { return _MontoComisionIngreso; }
			set { _MontoComisionIngreso = value; }
		}

		public string CodigoConceptoComisionIngreso {
			get { return _CodigoConceptoComisionIngreso; }
			set { _CodigoConceptoComisionIngreso = LibString.Mid(value, 0, 8); }
		}

		public string DescripcionConceptoComisionIngreso {
			get { return _DescripcionConceptoComisionIngreso; }
			set { _DescripcionConceptoComisionIngreso = LibString.Mid(value, 0, 30); }
		}

        public bool GeneraIGTFComisionIngresoAsBool {
            get { return _GeneraIGTFComisionIngreso; }
            set { _GeneraIGTFComisionIngreso = value; }
		}

        public string GeneraIGTFComisionIngreso {
            set { _GeneraIGTFComisionIngreso = LibConvert.SNToBool(value); }
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
		public TransferenciaEntreCuentasBancarias() {
			Clear();
		}
		#endregion //Constructores

		#region Metodos Generados
		public object TextDateLastModifiedForInput() {
			return string.Empty;
		}

		public void Clear() {
			ConsecutivoCompania = LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania");
			Consecutivo = 0;
			StatusAsEnum = eStatusTransferenciaBancaria.Vigente;
			Fecha = LibDate.Today();
			NumeroDocumento = string.Empty;
			Descripcion = string.Empty;
			FechaDeAnulacion = LibDate.MinDateForDB();
			CodigoCuentaBancariaOrigen = string.Empty;
			CambioABolivaresEgreso = 1;
			MontoTransferenciaEgreso = 0;
			CodigoConceptoEgreso = string.Empty;
			GeneraComisionEgresoAsBool = false;
			MontoComisionEgreso = 0;
			CodigoConceptoComisionEgreso = string.Empty;
            GeneraIGTFComisionEgresoAsBool = false;
			CodigoCuentaBancariaDestino = string.Empty;
			CambioABolivaresIngreso = 1;
			MontoTransferenciaIngreso = 0;
			CodigoConceptoIngreso = string.Empty;
			GeneraComisionIngresoAsBool = false;
			MontoComisionIngreso = 0;
			CodigoConceptoComisionIngreso = string.Empty;
            GeneraIGTFComisionIngresoAsBool = false;
			NombreOperador = string.Empty;
			FechaUltimaModificacion = LibDate.Today();
			fldTimeStamp = 0;
		}

		public TransferenciaEntreCuentasBancarias Clone() {
			TransferenciaEntreCuentasBancarias vResult = new TransferenciaEntreCuentasBancarias();
			vResult.ConsecutivoCompania = _ConsecutivoCompania;
			vResult.Consecutivo = _Consecutivo;
			vResult.StatusAsEnum = _Status;
			vResult.Fecha = _Fecha;
			vResult.NumeroDocumento = _NumeroDocumento;
			vResult.Descripcion = _Descripcion;
			vResult.FechaDeAnulacion = _FechaDeAnulacion;
			vResult.CodigoCuentaBancariaOrigen = _CodigoCuentaBancariaOrigen;
			vResult.CambioABolivaresEgreso = _CambioABolivaresEgreso;
			vResult.MontoTransferenciaEgreso = _MontoTransferenciaEgreso;
			vResult.CodigoConceptoEgreso = _CodigoConceptoEgreso;
			vResult.GeneraComisionEgresoAsBool = _GeneraComisionEgreso;
			vResult.MontoComisionEgreso = _MontoComisionEgreso;
			vResult.CodigoConceptoComisionEgreso = _CodigoConceptoComisionEgreso;
            vResult.GeneraIGTFComisionEgresoAsBool = _GeneraIGTFComisionEgreso;
			vResult.CodigoCuentaBancariaDestino = _CodigoCuentaBancariaDestino;
			vResult.CambioABolivaresIngreso = _CambioABolivaresIngreso;
			vResult.MontoTransferenciaIngreso = _MontoTransferenciaIngreso;
			vResult.CodigoConceptoIngreso = _CodigoConceptoIngreso;
			vResult.GeneraComisionIngresoAsBool = _GeneraComisionIngreso;
			vResult.MontoComisionIngreso = _MontoComisionIngreso;
			vResult.CodigoConceptoComisionIngreso = _CodigoConceptoComisionIngreso;
            vResult.GeneraIGTFComisionIngresoAsBool = _GeneraIGTFComisionIngreso;
			vResult.NombreOperador = _NombreOperador;
			vResult.FechaUltimaModificacion = _FechaUltimaModificacion;
			vResult.fldTimeStamp = _fldTimeStamp;
			return vResult;
		}

		public override string ToString() {
			return "Consecutivo Compania = " + _ConsecutivoCompania.ToString() +
				"\nNº Movimiento = " + _Consecutivo.ToString() +
				"\nStatus = " + _Status.ToString() +
				"\nNombre Operador = " + _NombreOperador +
				"\nFecha Ultima Modificacion = " + _FechaUltimaModificacion.ToShortDateString();
		}
		#endregion //Metodos Generados

	} //End of class TransferenciaEntreCuentasBancarias

} //End of namespace Galac.Adm.Ccl.Banco

