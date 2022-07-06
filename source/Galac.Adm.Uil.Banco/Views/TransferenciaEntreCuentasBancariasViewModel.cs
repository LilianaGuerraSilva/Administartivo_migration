using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Xml.Linq;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using LibGalac.Aos.DefGen;
using LibGalac.Aos.UI.Mvvm;
using LibGalac.Aos.UI.Mvvm.Command;
using LibGalac.Aos.UI.Mvvm.Helpers;
using LibGalac.Aos.UI.Mvvm.Messaging;
using LibGalac.Aos.UI.Mvvm.Ribbon;
using LibGalac.Aos.UI.Mvvm.Validation;
using Galac.Adm.Brl.Banco;
using Galac.Adm.Ccl.Banco;
using Galac.Comun.Brl.TablasGen;
using Galac.Comun.Ccl.TablasGen;
using Galac.Comun.Uil.TablasGen.ViewModel;

namespace Galac.Adm.Uil.Banco.ViewModel {
	public class TransferenciaEntreCuentasBancariasViewModel : LibInputViewModelMfc<TransferenciaEntreCuentasBancarias> {
		#region Constantes
		public const string ConsecutivoPropertyName = "Consecutivo";
		public const string StatusPropertyName = "Status";
		public const string FechaPropertyName = "Fecha";
		public const string NumeroDocumentoPropertyName = "NumeroDocumento";
		public const string DescripcionPropertyName = "Descripcion";
		public const string FechaDeAnulacionPropertyName = "FechaDeAnulacion";
		public const string CodigoCuentaBancariaOrigenPropertyName = "CodigoCuentaBancariaOrigen";
		public const string NombreCuentaBancariaOrigenPropertyName = "NombreCuentaBancariaOrigen";
		public const string SaldoCuentaBancariaOrigenPropertyName = "SaldoCuentaBancariaOrigen";
		public const string CodigoMonedaCuentaBancariaOrigenPropertyName = "CodigoMonedaCuentaBancariaOrigen";
		public const string NombreMonedaCuentaBancariaOrigenPropertyName = "NombreMonedaCuentaBancariaOrigen";
		public const string ManejaDebitoCuentaBancariaOrigenPropertyName = "ManejaDebitoCuentaBancariaOrigen";
		public const string CambioABolivaresEgresoPropertyName = "CambioABolivaresEgreso";
		public const string MontoTransferenciaEgresoPropertyName = "MontoTransferenciaEgreso";
		public const string CodigoConceptoEgresoPropertyName = "CodigoConceptoEgreso";
		public const string DescripcionConceptoEgresoPropertyName = "DescripcionConceptoEgreso";
		public const string GeneraComisionEgresoPropertyName = "GeneraComisionEgreso";
		public const string MontoComisionEgresoPropertyName = "MontoComisionEgreso";
		public const string CodigoConceptoComisionEgresoPropertyName = "CodigoConceptoComisionEgreso";
		public const string DescripcionConceptoComisionEgresoPropertyName = "DescripcionConceptoComisionEgreso";
		public const string GeneraImpuestoBancarioEgresoPropertyName = "GeneraImpuestoBancarioEgreso";
		public const string MontoImpuestoBancarioEgresoPropertyName = "MontoImpuestoBancarioEgreso";
		public const string CodigoCuentaBancariaDestinoPropertyName = "CodigoCuentaBancariaDestino";
		public const string NombreCuentaBancariaDestinoPropertyName = "NombreCuentaBancariaDestino";
		public const string SaldoCuentaBancariaDestinoPropertyName = "SaldoCuentaBancariaDestino";
		public const string CodigoMonedaCuentaBancariaDestinoPropertyName = "CodigoMonedaCuentaBancariaDestino";
		public const string NombreMonedaCuentaBancariaDestinoPropertyName = "NombreMonedaCuentaBancariaDestino";
		public const string ManejaCreditoCuentaBancariaDestinoPropertyName = "ManejaCreditoCuentaBancariaDestino";
		public const string CambioABolivaresIngresoPropertyName = "CambioABolivaresIngreso";
		public const string MontoTransferenciaIngresoPropertyName = "MontoTransferenciaIngreso";
		public const string CodigoConceptoIngresoPropertyName = "CodigoConceptoIngreso";
		public const string DescripcionConceptoIngresoPropertyName = "DescripcionConceptoIngreso";
		public const string GeneraComisionIngresoPropertyName = "GeneraComisionIngreso";
		public const string MontoComisionIngresoPropertyName = "MontoComisionIngreso";
		public const string CodigoConceptoComisionIngresoPropertyName = "CodigoConceptoComisionIngreso";
		public const string DescripcionConceptoComisionIngresoPropertyName = "DescripcionConceptoComisionIngreso";
		public const string GeneraImpuestoBancarioIngresoPropertyName = "GeneraImpuestoBancarioIngreso";
		public const string MontoImpuestoBancarioIngresoPropertyName = "MontoImpuestoBancarioIngreso";
		public const string NombreOperadorPropertyName = "NombreOperador";
		public const string FechaUltimaModificacionPropertyName = "FechaUltimaModificacion";
		public const string NotIsInMonedaLocalEgresoPropertyName = "NotIsInMonedaLocalEgreso";
		public const string NotIsInMonedaLocalIngresoPropertyName = "NotIsInMonedaLocalIngreso";
		public const string IsEnabledGeneraComisionEgresoPropertyName = "IsEnabledGeneraComisionEgreso";
		public const string IsEnabledGeneraImpuestoBancarioEgresoPropertyName = "IsEnabledGeneraImpuestoBancarioEgreso";
		public const string IsEnabledGeneraComisionIngresoPropertyName = "IsEnabledGeneraComisionIngreso";
		public const string IsEnabledGeneraImpuestoBancarioIngresoPropertyName = "IsEnabledGeneraImpuestoBancarioIngreso";
		#endregion

		#region Variables
		private FkCuentaBancariaViewModel _ConexionCodigoCuentaBancariaOrigen = null;
		private FkConceptoBancarioViewModel _ConexionCodigoConceptoEgreso = null;
		private FkConceptoBancarioViewModel _ConexionCodigoConceptoComisionEgreso = null;
		private FkCuentaBancariaViewModel _ConexionCodigoCuentaBancariaDestino = null;
		private FkConceptoBancarioViewModel _ConexionCodigoConceptoIngreso = null;
		private FkConceptoBancarioViewModel _ConexionCodigoConceptoComisionIngreso = null;
		private FkMonedaViewModel _ConexionCodigoMonedaEgreso = null;
		private FkMonedaViewModel _ConexionCodigoMonedaIngreso = null;
		private Saw.Lib.clsNoComunSaw vMonedaLocal = null;
		#endregion //Variables

		#region Propiedades
		public override string ModuleName {
			get { return "Transferencia entre Cuentas Bancarias"; }
		}

		public int DecimalesTasaDeCambio {
			get;
			set;
		}

		public int ConsecutivoCompania {
			get {
				return Model.ConsecutivoCompania;
			}
			set {
				if (Model.ConsecutivoCompania != value) {
					Model.ConsecutivoCompania = value;
				}
			}
		}

		[LibGridColum("Consecutivo", eGridColumType.Integer, Alignment = eTextAlignment.Right, Width = 80)]
		public int Consecutivo {
			get {
				return Model.Consecutivo;
			}
			set {
				if (Model.Consecutivo != value) {
					Model.Consecutivo = value;
					RaisePropertyChanged(ConsecutivoPropertyName);
				}
			}
		}

		[LibGridColum("Status", eGridColumType.Enum, PrintingMemberPath = "StatusStr", Width = 80)]
		public eStatusTransferenciaBancaria Status {
			get {
				return Model.StatusAsEnum;
			}
			set {
				if (Model.StatusAsEnum != value) {
					Model.StatusAsEnum = value;
					IsDirty = true;
					RaisePropertyChanged(StatusPropertyName);
				}
			}
		}

		public string StatusYFechaDeAnulacion {
			get {
				return Status.GetDescription() + (Status == eStatusTransferenciaBancaria.Anulada ? (" - " + FechaDeAnulacion.ToShortDateString()) : string.Empty);
			}
		}

		[LibRequired(ErrorMessage = "El campo Fecha es requerido.")]
		[LibCustomValidation("FechaValidating")]
		[LibGridColum("Fecha", eGridColumType.DatePicker, Width = 80)]
		public DateTime Fecha {
			get {
				return Model.Fecha;
			}
			set {
				if (Model.Fecha != value) {
					Model.Fecha = value;
					IsDirty = true;
					AsignaTasaDelDiaEgreso();
					AsignaTasaDelDiaIngreso();
					RaisePropertyChanged(FechaPropertyName);
				}
			}
		}

		[LibRequired(ErrorMessage = "El campo Nº Documento es requerido.")]
		[LibGridColum("Nº Documento", MaxLength = 20, Width = 200)]
		public string NumeroDocumento {
			get {
				return Model.NumeroDocumento;
			}
			set {
				if (Model.NumeroDocumento != value) {
					Model.NumeroDocumento = value;
					IsDirty = true;
					RaisePropertyChanged(NumeroDocumentoPropertyName);
				}
			}
		}

		[LibRequired(ErrorMessage = "El campo Descripción es requerido.")]
		public string Descripcion {
			get {
				return Model.Descripcion;
			}
			set {
				if (Model.Descripcion != value) {
					Model.Descripcion = value;
					IsDirty = true;
					RaisePropertyChanged(DescripcionPropertyName);
				}
			}
		}

		public DateTime FechaDeAnulacion {
			get {
				return Model.FechaDeAnulacion;
			}
			set {
				if (Model.FechaDeAnulacion != value) {
					Model.FechaDeAnulacion = value;
					RaisePropertyChanged(FechaDeAnulacionPropertyName);
				}
			}
		}

		[LibRequired(ErrorMessage = "El campo Cuenta Bancaria Origen es requerido.")]
		[LibCustomValidation("CodigoCuentasBancariasValidating")]
		[LibGridColum("Cuenta Origen", eGridColumType.Connection, ConnectionDisplayMemberPath = "Saw.Gv_CuentaBancaria_B1.Codigo", ConnectionModelPropertyName = "CodigoCuentaBancariaOrigen", ConnectionSearchCommandName = "ChooseCodigoCuentaBancariaOrigenCommand", Width = 100)]
		public string CodigoCuentaBancariaOrigen {
			get {
				return Model.CodigoCuentaBancariaOrigen;
			}
			set {
				if (Model.CodigoCuentaBancariaOrigen != value) {
					Model.CodigoCuentaBancariaOrigen = value;
					IsDirty = true;
					RaisePropertyChanged(CodigoCuentaBancariaOrigenPropertyName);
					if (LibString.IsNullOrEmpty(CodigoCuentaBancariaOrigen, true)) {
						ConexionCodigoCuentaBancariaOrigen = null;
					}
				}
			}
		}

		public string NombreCuentaBancariaOrigen {
			get {
				return Model.NombreCuentaBancariaOrigen;
			}
			set {
				if (Model.NombreCuentaBancariaOrigen != value) {
					Model.NombreCuentaBancariaOrigen = value;
					IsDirty = true;
					RaisePropertyChanged(NombreCuentaBancariaOrigenPropertyName);
				}
			}
		}

		public decimal SaldoCuentaBancariaOrigen {
			get {
				return Model.SaldoCuentaBancariaOrigen;
			}
			set {
				if (Model.SaldoCuentaBancariaOrigen != value) {
					Model.SaldoCuentaBancariaOrigen = value;
					IsDirty = true;
					RaisePropertyChanged(SaldoCuentaBancariaOrigenPropertyName);
				}
			}
		}

		public string CodigoMonedaCuentaBancariaOrigen {
			get {
				return Model.CodigoMonedaCuentaBancariaOrigen;
			}
			set {
				if (Model.CodigoMonedaCuentaBancariaOrigen != value) {
					Model.CodigoMonedaCuentaBancariaOrigen = value;
					IsDirty = true;
					AsignaTasaDelDiaEgreso();
					RaisePropertyChanged(CodigoMonedaCuentaBancariaOrigenPropertyName);
				}
			}
		}

		public string NombreMonedaCuentaBancariaOrigen {
			get {
				return Model.NombreMonedaCuentaBancariaOrigen;
			}
			set {
				if (Model.NombreMonedaCuentaBancariaOrigen != value) {
					Model.NombreMonedaCuentaBancariaOrigen = value;
					IsDirty = true;
					RaisePropertyChanged(NombreMonedaCuentaBancariaOrigenPropertyName);
					RaisePropertyChanged(NotIsInMonedaLocalEgresoPropertyName);
				}
			}
		}

		public bool ManejaDebitoCuentaBancariaOrigen {
			get {
				return Model.ManejaDebitoCuentaBancariaOrigenAsBool;
			}
			set {
				if (Model.ManejaDebitoCuentaBancariaOrigenAsBool != value) {
					Model.ManejaDebitoCuentaBancariaOrigenAsBool = value;
					IsDirty = true;
					RaisePropertyChanged(ManejaDebitoCuentaBancariaOrigenPropertyName);
				}
			}
		}

		[LibCustomValidation("CambioABolivaresEgresoValidating")]
		public decimal CambioABolivaresEgreso {
			get {
				return LibMath.RoundToNDecimals(Model.CambioABolivaresEgreso, LibDefGen.ProgramInfo.IsCountryPeru() ? 3 : 4);
			}
			set {
				if (Model.CambioABolivaresEgreso != value) {
					Model.CambioABolivaresEgreso = value;
					IsDirty = true;
					RaisePropertyChanged(CambioABolivaresEgresoPropertyName);
				}
			}
		}

		[LibCustomValidation("MontoTransferenciaEgresoValidating")]
		public decimal MontoTransferenciaEgreso {
			get {
				return Model.MontoTransferenciaEgreso;
			}
			set {
				if (Model.MontoTransferenciaEgreso != value) {
					Model.MontoTransferenciaEgreso = value;
					IsDirty = true;
					RaisePropertyChanged(MontoTransferenciaEgresoPropertyName);
				}
			}
		}

		[LibRequired(ErrorMessage = "El campo Concepto Bancario de Egreso es requerido.")]
		[LibGridColum("Concepto Egreso", eGridColumType.Connection, ConnectionDisplayMemberPath = "Codigo", ConnectionModelPropertyName = "CodigoConceptoEgreso", ConnectionSearchCommandName = "ChooseCodigoConceptoEgresoCommand", Width = 120)]
		public string CodigoConceptoEgreso {
			get {
				return Model.CodigoConceptoEgreso;
			}
			set {
				if (Model.CodigoConceptoEgreso != value) {
					Model.CodigoConceptoEgreso = value;
					IsDirty = true;
					RaisePropertyChanged(CodigoConceptoEgresoPropertyName);
					if (LibString.IsNullOrEmpty(CodigoConceptoEgreso, true)) {
						ConexionCodigoConceptoEgreso = null;
					}
				}
			}
		}

		public string DescripcionConceptoEgreso {
			get {
				return Model.DescripcionConceptoEgreso;
			}
			set {
				if (Model.DescripcionConceptoEgreso != value) {
					Model.DescripcionConceptoEgreso = value;
					IsDirty = true;
					RaisePropertyChanged(DescripcionConceptoEgresoPropertyName);
				}
			}
		}

		public bool GeneraComisionEgreso {
			get {
				return Model.GeneraComisionEgresoAsBool;
			}
			set {
				if (Model.GeneraComisionEgresoAsBool != value) {
					Model.GeneraComisionEgresoAsBool = value;
					IsDirty = true;
					MontoComisionEgreso = 0;
					CodigoConceptoComisionEgreso = string.Empty;
					DescripcionConceptoComisionEgreso = string.Empty;
					RaisePropertyChanged(GeneraComisionEgresoPropertyName);
					RaisePropertyChanged(IsEnabledGeneraComisionEgresoPropertyName);
				}
			}
		}

		[LibCustomValidation("MontoComisionEgresoValidating")]
		public decimal MontoComisionEgreso {
			get {
				return Model.MontoComisionEgreso;
			}
			set {
				if (Model.MontoComisionEgreso != value) {
					Model.MontoComisionEgreso = value;
					IsDirty = true;
					RaisePropertyChanged(MontoComisionEgresoPropertyName);
				}
			}
		}

		[LibCustomValidation("CodigoConceptoComisionEgresoValidating")]
		public string CodigoConceptoComisionEgreso {
			get {
				return Model.CodigoConceptoComisionEgreso;
			}
			set {
				if (Model.CodigoConceptoComisionEgreso != value) {
					Model.CodigoConceptoComisionEgreso = value;
					IsDirty = true;
					RaisePropertyChanged(CodigoConceptoComisionEgresoPropertyName);
					if (LibString.IsNullOrEmpty(CodigoConceptoComisionEgreso, true)) {
						ConexionCodigoConceptoComisionEgreso = null;
					}
				}
			}
		}

		public string DescripcionConceptoComisionEgreso {
			get {
				return Model.DescripcionConceptoComisionEgreso;
			}
			set {
				if (Model.DescripcionConceptoComisionEgreso != value) {
					Model.DescripcionConceptoComisionEgreso = value;
					IsDirty = true;
					RaisePropertyChanged(DescripcionConceptoComisionEgresoPropertyName);
				}
			}
		}

		public bool GeneraImpuestoBancarioEgreso {
			get {
				return Model.GeneraImpuestoBancarioEgresoAsBool;
			}
			set {
				if (Model.GeneraImpuestoBancarioEgresoAsBool != value) {
					Model.GeneraImpuestoBancarioEgresoAsBool = value;
					IsDirty = true;
					MontoImpuestoBancarioEgreso = 0;
					RaisePropertyChanged(GeneraImpuestoBancarioEgresoPropertyName);
					RaisePropertyChanged(IsEnabledGeneraImpuestoBancarioEgresoPropertyName);
				}
			}
		}

		[LibCustomValidation("MontoImpuestoBancarioEgresoValidating")]
		public decimal MontoImpuestoBancarioEgreso {
			get {
				return Model.MontoImpuestoBancarioEgreso;
			}
			set {
				if (Model.MontoImpuestoBancarioEgreso != value) {
					Model.MontoImpuestoBancarioEgreso = value;
					IsDirty = true;
					RaisePropertyChanged(MontoImpuestoBancarioEgresoPropertyName);
				}
			}
		}

		[LibRequired(ErrorMessage = "El campo Cuenta Bancaria Destino es requerido.")]
		[LibCustomValidation("CodigoCuentasBancariasValidating")]
		[LibGridColum("Cuenta Destino", eGridColumType.Connection, ConnectionDisplayMemberPath = "Saw.Gv_CuentaBancaria_B1.Codigo", ConnectionModelPropertyName = "CodigoCuentaBancariaDestino", ConnectionSearchCommandName = "ChooseCodigoCuentaBancariaDestinoCommand", Width = 100)]
		public string CodigoCuentaBancariaDestino {
			get {
				return Model.CodigoCuentaBancariaDestino;
			}
			set {
				if (Model.CodigoCuentaBancariaDestino != value) {
					Model.CodigoCuentaBancariaDestino = value;
					IsDirty = true;
					RaisePropertyChanged(CodigoCuentaBancariaDestinoPropertyName);
					if (LibString.IsNullOrEmpty(CodigoCuentaBancariaDestino, true)) {
						ConexionCodigoCuentaBancariaDestino = null;
					}
				}
			}
		}

		public string NombreCuentaBancariaDestino {
			get {
				return Model.NombreCuentaBancariaDestino;
			}
			set {
				if (Model.NombreCuentaBancariaDestino != value) {
					Model.NombreCuentaBancariaDestino = value;
					IsDirty = true;
					RaisePropertyChanged(NombreCuentaBancariaDestinoPropertyName);
				}
			}
		}

		public decimal SaldoCuentaBancariaDestino {
			get {
				return Model.SaldoCuentaBancariaDestino;
			}
			set {
				if (Model.SaldoCuentaBancariaDestino != value) {
					Model.SaldoCuentaBancariaDestino = value;
					IsDirty = true;
					RaisePropertyChanged(SaldoCuentaBancariaDestinoPropertyName);
				}
			}
		}

		public string CodigoMonedaCuentaBancariaDestino {
			get {
				return Model.CodigoMonedaCuentaBancariaDestino;
			}
			set {
				if (Model.CodigoMonedaCuentaBancariaDestino != value) {
					Model.CodigoMonedaCuentaBancariaDestino = value;
					IsDirty = true;
					AsignaTasaDelDiaIngreso();
					RaisePropertyChanged(CodigoMonedaCuentaBancariaDestinoPropertyName);
				}
			}
		}

		public string NombreMonedaCuentaBancariaDestino {
			get {
				return Model.NombreMonedaCuentaBancariaDestino;
			}
			set {
				if (Model.NombreMonedaCuentaBancariaDestino != value) {
					Model.NombreMonedaCuentaBancariaDestino = value;
					IsDirty = true;
					RaisePropertyChanged(NombreMonedaCuentaBancariaDestinoPropertyName);
					RaisePropertyChanged(NotIsInMonedaLocalIngresoPropertyName);
				}
			}
		}

		public bool ManejaCreditoCuentaBancariaDestino {
			get {
				return Model.ManejaCreditoCuentaBancariaDestinoAsBool;
			}
			set {
				if (Model.ManejaCreditoCuentaBancariaDestinoAsBool != value) {
					Model.ManejaCreditoCuentaBancariaDestinoAsBool = value;
					IsDirty = true;
					RaisePropertyChanged(ManejaCreditoCuentaBancariaDestinoPropertyName);
				}
			}
		}

		[LibCustomValidation("CambioABolivaresIngresoValidating")]
		public decimal CambioABolivaresIngreso {
			get {
				return LibMath.RoundToNDecimals(Model.CambioABolivaresIngreso, LibDefGen.ProgramInfo.IsCountryPeru() ? 3 : 4);

			}
			set {
				if (Model.CambioABolivaresIngreso != value) {
					Model.CambioABolivaresIngreso = value;
					IsDirty = true;
					RaisePropertyChanged(CambioABolivaresIngresoPropertyName);
				}
			}
		}

		[LibCustomValidation("MontoTransferenciaIngresoValidating")]
		public decimal MontoTransferenciaIngreso {
			get {
				return Model.MontoTransferenciaIngreso;
			}
			set {
				if (Model.MontoTransferenciaIngreso != value) {
					Model.MontoTransferenciaIngreso = value;
					IsDirty = true;
					RaisePropertyChanged(MontoTransferenciaIngresoPropertyName);
				}
			}
		}

		[LibRequired(ErrorMessage = "El campo Concepto Bancario de Ingreso es requerido.")]
		[LibGridColum("Concepto Ingreso", eGridColumType.Connection, ConnectionDisplayMemberPath = "Codigo", ConnectionModelPropertyName = "CodigoConceptoIngreso", ConnectionSearchCommandName = "ChooseCodigoConceptoIngresoCommand", Width = 120)]
		public string CodigoConceptoIngreso {
			get {
				return Model.CodigoConceptoIngreso;
			}
			set {
				if (Model.CodigoConceptoIngreso != value) {
					Model.CodigoConceptoIngreso = value;
					IsDirty = true;
					RaisePropertyChanged(CodigoConceptoIngresoPropertyName);
					if (LibString.IsNullOrEmpty(CodigoConceptoIngreso, true)) {
						ConexionCodigoConceptoIngreso = null;
					}
				}
			}
		}

		public string DescripcionConceptoIngreso {
			get {
				return Model.DescripcionConceptoIngreso;
			}
			set {
				if (Model.DescripcionConceptoIngreso != value) {
					Model.DescripcionConceptoIngreso = value;
					IsDirty = true;
					RaisePropertyChanged(DescripcionConceptoIngresoPropertyName);
				}
			}
		}

		public bool GeneraComisionIngreso {
			get {
				return Model.GeneraComisionIngresoAsBool;
			}
			set {
				if (Model.GeneraComisionIngresoAsBool != value) {
					Model.GeneraComisionIngresoAsBool = value;
					IsDirty = true;
					MontoComisionIngreso = 0;
					CodigoConceptoComisionIngreso = string.Empty;
					DescripcionConceptoComisionIngreso = string.Empty;
					RaisePropertyChanged(GeneraComisionIngresoPropertyName);
					RaisePropertyChanged(IsEnabledGeneraComisionIngresoPropertyName);
				}
			}
		}

		[LibCustomValidation("MontoComisionIngresoValidating")]
		public decimal MontoComisionIngreso {
			get {
				return Model.MontoComisionIngreso;
			}
			set {
				if (Model.MontoComisionIngreso != value) {
					Model.MontoComisionIngreso = value;
					IsDirty = true;
					RaisePropertyChanged(MontoComisionIngresoPropertyName);
				}
			}
		}

		[LibCustomValidation("CodigoConceptoComisionIngresoValidating")]
		public string CodigoConceptoComisionIngreso {
			get {
				return Model.CodigoConceptoComisionIngreso;
			}
			set {
				if (Model.CodigoConceptoComisionIngreso != value) {
					Model.CodigoConceptoComisionIngreso = value;
					IsDirty = true;
					RaisePropertyChanged(CodigoConceptoComisionIngresoPropertyName);
					if (LibString.IsNullOrEmpty(CodigoConceptoComisionIngreso, true)) {
						ConexionCodigoConceptoComisionIngreso = null;
					}
				}
			}
		}

		public string DescripcionConceptoComisionIngreso {
			get {
				return Model.DescripcionConceptoComisionIngreso;
			}
			set {
				if (Model.DescripcionConceptoComisionIngreso != value) {
					Model.DescripcionConceptoComisionIngreso = value;
					IsDirty = true;
					RaisePropertyChanged(DescripcionConceptoComisionIngresoPropertyName);
				}
			}
		}

		public bool GeneraImpuestoBancarioIngreso {
			get {
				return Model.GeneraImpuestoBancarioIngresoAsBool;
			}
			set {
				if (Model.GeneraImpuestoBancarioIngresoAsBool != value) {
					Model.GeneraImpuestoBancarioIngresoAsBool = value;
					IsDirty = true;
					MontoImpuestoBancarioIngreso = 0;
					RaisePropertyChanged(GeneraImpuestoBancarioIngresoPropertyName);
					RaisePropertyChanged(IsEnabledGeneraImpuestoBancarioIngresoPropertyName);
				}
			}
		}

		[LibCustomValidation("MontoImpuestoBancarioIngresoValidating")]
		public decimal MontoImpuestoBancarioIngreso {
			get {
				return Model.MontoImpuestoBancarioIngreso;
			}
			set {
				if (Model.MontoImpuestoBancarioIngreso != value) {
					Model.MontoImpuestoBancarioIngreso = value;
					IsDirty = true;
					RaisePropertyChanged(MontoImpuestoBancarioIngresoPropertyName);
				}
			}
		}

		public string NombreOperador {
			get {
				return Model.NombreOperador;
			}
			set {
				if (Model.NombreOperador != value) {
					Model.NombreOperador = value;
					IsDirty = true;
					RaisePropertyChanged(NombreOperadorPropertyName);
				}
			}
		}

		public DateTime FechaUltimaModificacion {
			get {
				return Model.FechaUltimaModificacion;
			}
			set {
				if (Model.FechaUltimaModificacion != value) {
					Model.FechaUltimaModificacion = value;
					IsDirty = true;
					RaisePropertyChanged(FechaUltimaModificacionPropertyName);
				}
			}
		}

		public eStatusTransferenciaBancaria[] ArrayStatusTransferenciaBancaria {
			get {
				return LibEnumHelper<eStatusTransferenciaBancaria>.GetValuesInArray();
			}
		}

		public FkCuentaBancariaViewModel ConexionCodigoCuentaBancariaOrigen {
			get {
				return _ConexionCodigoCuentaBancariaOrigen;
			}
			set {
				if (_ConexionCodigoCuentaBancariaOrigen != value) {
					_ConexionCodigoCuentaBancariaOrigen = value;
					RaisePropertyChanged(CodigoCuentaBancariaOrigenPropertyName);
				}
				if (_ConexionCodigoCuentaBancariaOrigen != null) {
					CodigoCuentaBancariaOrigen = value.Codigo;
					NombreCuentaBancariaOrigen = value.NombreCuenta;
					SaldoCuentaBancariaOrigen = value.SaldoDisponible;
					CodigoMonedaCuentaBancariaOrigen = value.CodigoMoneda;
					NombreMonedaCuentaBancariaOrigen = value.NombreDeLaMoneda;
					ManejaDebitoCuentaBancariaOrigen = value.ManejaDebitoBancario;
				} else {
					CodigoCuentaBancariaOrigen = string.Empty;
					NombreCuentaBancariaOrigen = string.Empty;
					SaldoCuentaBancariaOrigen = 0;
					CodigoMonedaCuentaBancariaOrigen = string.Empty;
					NombreMonedaCuentaBancariaOrigen = string.Empty;
					ManejaDebitoCuentaBancariaOrigen = false;
				}
			}
		}

		public FkConceptoBancarioViewModel ConexionCodigoConceptoEgreso {
			get {
				return _ConexionCodigoConceptoEgreso;
			}
			set {
				if (_ConexionCodigoConceptoEgreso != value) {
					_ConexionCodigoConceptoEgreso = value;
					RaisePropertyChanged(CodigoConceptoEgresoPropertyName);
				}
				if (_ConexionCodigoConceptoEgreso != null) {
					CodigoConceptoEgreso = value.Codigo;
					DescripcionConceptoEgreso = value.Descripcion;
				} else {
					CodigoConceptoEgreso = string.Empty;
					DescripcionConceptoEgreso = string.Empty;
				}
			}
		}

		public FkConceptoBancarioViewModel ConexionCodigoConceptoComisionEgreso {
			get {
				return _ConexionCodigoConceptoComisionEgreso;
			}
			set {
				if (_ConexionCodigoConceptoComisionEgreso != value) {
					_ConexionCodigoConceptoComisionEgreso = value;
					RaisePropertyChanged(CodigoConceptoComisionEgresoPropertyName);
				}
				if (_ConexionCodigoConceptoComisionEgreso != null) {
					CodigoConceptoComisionEgreso = value.Codigo;
					DescripcionConceptoComisionEgreso = value.Descripcion;
				} else {
					CodigoConceptoComisionEgreso = string.Empty;
					DescripcionConceptoComisionEgreso = string.Empty;
				}
			}
		}

		public FkCuentaBancariaViewModel ConexionCodigoCuentaBancariaDestino {
			get {
				return _ConexionCodigoCuentaBancariaDestino;
			}
			set {
				if (_ConexionCodigoCuentaBancariaDestino != value) {
					_ConexionCodigoCuentaBancariaDestino = value;
					RaisePropertyChanged(CodigoCuentaBancariaDestinoPropertyName);
				}
				if (_ConexionCodigoCuentaBancariaDestino != null) {
					CodigoCuentaBancariaDestino = value.Codigo;
					NombreCuentaBancariaDestino = value.NombreCuenta;
					SaldoCuentaBancariaDestino = value.SaldoDisponible;
					CodigoMonedaCuentaBancariaDestino = value.CodigoMoneda;
					NombreMonedaCuentaBancariaDestino = value.NombreDeLaMoneda;
					ManejaCreditoCuentaBancariaDestino = value.ManejaCreditoBancario;
				} else {
					CodigoCuentaBancariaDestino = string.Empty;
					NombreCuentaBancariaDestino = string.Empty;
					SaldoCuentaBancariaDestino = 0;
					CodigoMonedaCuentaBancariaDestino = string.Empty;
					NombreMonedaCuentaBancariaDestino = string.Empty;
					ManejaCreditoCuentaBancariaDestino = false;
				}
			}
		}

		public FkConceptoBancarioViewModel ConexionCodigoConceptoIngreso {
			get {
				return _ConexionCodigoConceptoIngreso;
			}
			set {
				if (_ConexionCodigoConceptoIngreso != value) {
					_ConexionCodigoConceptoIngreso = value;
					RaisePropertyChanged(CodigoConceptoIngresoPropertyName);
				}
				if (_ConexionCodigoConceptoIngreso != null) {
					CodigoConceptoIngreso = value.Codigo;
					DescripcionConceptoIngreso = value.Descripcion;
				} else {
					CodigoConceptoIngreso = string.Empty;
					DescripcionConceptoIngreso = string.Empty;
				}
			}
		}

		public FkConceptoBancarioViewModel ConexionCodigoConceptoComisionIngreso {
			get {
				return _ConexionCodigoConceptoComisionIngreso;
			}
			set {
				if (_ConexionCodigoConceptoComisionIngreso != value) {
					_ConexionCodigoConceptoComisionIngreso = value;
					RaisePropertyChanged(CodigoConceptoComisionIngresoPropertyName);
				}
				if (_ConexionCodigoConceptoComisionIngreso != null) {
					CodigoConceptoComisionIngreso = value.Codigo;
					DescripcionConceptoComisionIngreso = value.Descripcion;
				} else {
					CodigoConceptoComisionIngreso = string.Empty;
					DescripcionConceptoComisionIngreso = string.Empty;
				}
			}
		}

		public FkMonedaViewModel ConexionCodigoMonedaEgreso {
			get {
				return _ConexionCodigoMonedaEgreso;
			}
			set {
				if (_ConexionCodigoMonedaEgreso != value) {
					_ConexionCodigoMonedaEgreso = value;
				}
			}
		}

		public FkMonedaViewModel ConexionCodigoMonedaIngreso {
			get {
				return _ConexionCodigoMonedaIngreso;
			}
			set {
				if (_ConexionCodigoMonedaIngreso != value) {
					_ConexionCodigoMonedaIngreso = value;
				}
			}
		}

		public RelayCommand<string> ChooseCodigoCuentaBancariaOrigenCommand {
			get;
			private set;
		}

		public RelayCommand<string> ChooseCodigoConceptoEgresoCommand {
			get;
			private set;
		}

		public RelayCommand<string> ChooseCodigoConceptoComisionEgresoCommand {
			get;
			private set;
		}

		public RelayCommand<string> ChooseCodigoCuentaBancariaDestinoCommand {
			get;
			private set;
		}

		public RelayCommand<string> ChooseCodigoConceptoIngresoCommand {
			get;
			private set;
		}

		public RelayCommand<string> ChooseCodigoConceptoComisionIngresoCommand {
			get;
			private set;
		}
		#endregion //Propiedades

		#region Constructores
		public TransferenciaEntreCuentasBancariasViewModel()
			: this(new TransferenciaEntreCuentasBancarias(), eAccionSR.Insertar) {
		}

		public TransferenciaEntreCuentasBancariasViewModel(TransferenciaEntreCuentasBancarias initModel, eAccionSR initAction)
			: base(initModel, initAction, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
			DecimalesTasaDeCambio = LibDefGen.ProgramInfo.IsCountryPeru() ? 3 : 4;
			DefaultFocusedPropertyName = FechaPropertyName;
			Model.ConsecutivoCompania = Mfc.GetInt("Compania");
			IsInsertar = initAction == eAccionSR.Insertar;
			vMonedaLocal = new Saw.Lib.clsNoComunSaw();
		}
		#endregion //Constructores

		#region Metodos Generados
		protected override TransferenciaEntreCuentasBancarias FindCurrentRecord(TransferenciaEntreCuentasBancarias valModel) {
			if (valModel == null) {
				return null;
			}
			LibGpParams vParams = new LibGpParams();
			vParams.AddInInteger("ConsecutivoCompania", valModel.ConsecutivoCompania);
			vParams.AddInInteger("Consecutivo", valModel.Consecutivo);
			return BusinessComponent.GetData(eProcessMessageType.SpName, "TransferenciaEntreCuentasBancariasGET", vParams.Get()).FirstOrDefault();
		}

		protected override ILibBusinessComponentWithSearch<IList<TransferenciaEntreCuentasBancarias>, IList<TransferenciaEntreCuentasBancarias>> GetBusinessComponent() {
			return new clsTransferenciaEntreCuentasBancariasNav();
		}

		protected override void InitializeCommands() {
			base.InitializeCommands();
			ChooseCodigoCuentaBancariaOrigenCommand = new RelayCommand<string>(ExecuteChooseCodigoCuentaBancariaOrigenCommand);
			ChooseCodigoConceptoEgresoCommand = new RelayCommand<string>(ExecuteChooseCodigoConceptoEgresoCommand);
			ChooseCodigoConceptoComisionEgresoCommand = new RelayCommand<string>(ExecuteChooseCodigoConceptoComisionEgresoCommand);
			ChooseCodigoCuentaBancariaDestinoCommand = new RelayCommand<string>(ExecuteChooseCodigoCuentaBancariaDestinoCommand);
			ChooseCodigoConceptoIngresoCommand = new RelayCommand<string>(ExecuteChooseCodigoConceptoIngresoCommand);
			ChooseCodigoConceptoComisionIngresoCommand = new RelayCommand<string>(ExecuteChooseCodigoConceptoComisionIngresoCommand);
		}

		protected override void ReloadRelatedConnections() {
			base.ReloadRelatedConnections();
			ConexionCodigoCuentaBancariaOrigen = FirstConnectionRecordOrDefault<FkCuentaBancariaViewModel>("Cuenta Bancaria", LibSearchCriteria.CreateCriteria("Saw.Gv_CuentaBancaria_B1.Codigo", CodigoCuentaBancariaOrigen));
			ConexionCodigoConceptoEgreso = FirstConnectionRecordOrDefault<FkConceptoBancarioViewModel>("Concepto Bancario", LibSearchCriteria.CreateCriteria("Codigo", CodigoConceptoEgreso));
			ConexionCodigoConceptoComisionEgreso = FirstConnectionRecordOrDefault<FkConceptoBancarioViewModel>("Concepto Bancario", LibSearchCriteria.CreateCriteria("Codigo", CodigoConceptoComisionEgreso));
			ConexionCodigoCuentaBancariaDestino = FirstConnectionRecordOrDefault<FkCuentaBancariaViewModel>("Cuenta Bancaria", LibSearchCriteria.CreateCriteria("Saw.Gv_CuentaBancaria_B1.Codigo", CodigoCuentaBancariaDestino));
			ConexionCodigoConceptoIngreso = FirstConnectionRecordOrDefault<FkConceptoBancarioViewModel>("Concepto Bancario", LibSearchCriteria.CreateCriteria("Codigo", CodigoConceptoIngreso));
			ConexionCodigoConceptoComisionIngreso = FirstConnectionRecordOrDefault<FkConceptoBancarioViewModel>("Concepto Bancario", LibSearchCriteria.CreateCriteria("Codigo", CodigoConceptoComisionIngreso));
		}

		private void ExecuteChooseCodigoCuentaBancariaOrigenCommand(string valCodigo) {
			try {
				if (valCodigo == null) {
					valCodigo = string.Empty;
				}
				LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Saw.Gv_CuentaBancaria_B1.Codigo", valCodigo);
				LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("ConsecutivoCompania", Mfc.GetInt("Compania"));
				vFixedCriteria.Add("Saw.Gv_CuentaBancaria_B1.Status", eStatusCtaBancaria.Activo);
				ConexionCodigoCuentaBancariaOrigen = null;
				ConexionCodigoCuentaBancariaOrigen = ChooseRecord<FkCuentaBancariaViewModel>("Cuenta Bancaria", vDefaultCriteria, vFixedCriteria, string.Empty);
			} catch (AccessViolationException) {
				throw;
			} catch (Exception vEx) {
				LibMessages.RaiseError.ShowError(vEx, ModuleName);
			}
		}

		private void ExecuteChooseCodigoConceptoEgresoCommand(string valCodigo) {
			try {
				if (valCodigo == null) {
					valCodigo = string.Empty;
				}
				LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo", valCodigo);
				LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("Tipo", eIngresoEgreso.Egreso);
				ConexionCodigoConceptoEgreso = null;
				ConexionCodigoConceptoEgreso = ChooseRecord<FkConceptoBancarioViewModel>("Concepto Bancario", vDefaultCriteria, vFixedCriteria, string.Empty);
			} catch (AccessViolationException) {
				throw;
			} catch (Exception vEx) {
				LibMessages.RaiseError.ShowError(vEx, ModuleName);
			}
		}

		private void ExecuteChooseCodigoConceptoComisionEgresoCommand(string valCodigo) {
			try {
				if (valCodigo == null) {
					valCodigo = string.Empty;
				}
				LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo", valCodigo);
				LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("Tipo", eIngresoEgreso.Egreso);
				ConexionCodigoConceptoComisionEgreso = null;
				ConexionCodigoConceptoComisionEgreso = ChooseRecord<FkConceptoBancarioViewModel>("Concepto Bancario", vDefaultCriteria, vFixedCriteria, string.Empty);
			} catch (AccessViolationException) {
				throw;
			} catch (Exception vEx) {
				LibMessages.RaiseError.ShowError(vEx, ModuleName);
			}
		}

		private void ExecuteChooseCodigoCuentaBancariaDestinoCommand(string valCodigo) {
			try {
				if (valCodigo == null) {
					valCodigo = string.Empty;
				}
				LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Saw.Gv_CuentaBancaria_B1.Codigo", valCodigo);
				LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("ConsecutivoCompania", Mfc.GetInt("Compania"));
				vFixedCriteria.Add("Saw.Gv_CuentaBancaria_B1.Status", eStatusCtaBancaria.Activo);
				ConexionCodigoCuentaBancariaDestino = null;
				ConexionCodigoCuentaBancariaDestino = ChooseRecord<FkCuentaBancariaViewModel>("Cuenta Bancaria", vDefaultCriteria, vFixedCriteria, string.Empty);
			} catch (AccessViolationException) {
				throw;
			} catch (Exception vEx) {
				LibMessages.RaiseError.ShowError(vEx, ModuleName);
			}
		}

		private void ExecuteChooseCodigoConceptoIngresoCommand(string valCodigo) {
			try {
				if (valCodigo == null) {
					valCodigo = string.Empty;
				}
				LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo", valCodigo);
				LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("Tipo", eIngresoEgreso.Ingreso);
				ConexionCodigoConceptoIngreso = null;
				ConexionCodigoConceptoIngreso = ChooseRecord<FkConceptoBancarioViewModel>("Concepto Bancario", vDefaultCriteria, vFixedCriteria, string.Empty);
			} catch (AccessViolationException) {
				throw;
			} catch (Exception vEx) {
				LibMessages.RaiseError.ShowError(vEx, ModuleName);
			}
		}

		private void ExecuteChooseCodigoConceptoComisionIngresoCommand(string valCodigo) {
			try {
				if (valCodigo == null) {
					valCodigo = string.Empty;
				}
				LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo", valCodigo);
				LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("Tipo", eIngresoEgreso.Ingreso);
				ConexionCodigoConceptoComisionIngreso = null;
				ConexionCodigoConceptoComisionIngreso = ChooseRecord<FkConceptoBancarioViewModel>("Concepto Bancario", vDefaultCriteria, vFixedCriteria, string.Empty);
			} catch (AccessViolationException) {
				throw;
			} catch (Exception vEx) {
				LibMessages.RaiseError.ShowError(vEx, ModuleName);
			}
		}

		private ValidationResult FechaValidating() {
			ValidationResult vResult = ValidationResult.Success;
			if ((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar) || (Action == eAccionSR.Anular)) {
				return ValidationResult.Success;
			} else {
				if (LibDefGen.DateIsGreaterThanDateLimitForEnterData(Fecha, false, Action)) {
					vResult = new ValidationResult(LibDefGen.TooltipMessageDateRestrictionDemoProgram("Fecha"));
				} else if (LibDate.DateIsGreaterThanToday(Fecha, false, string.Empty)) {
					vResult = new ValidationResult(LibDate.MsgDateIsGreaterThanToday("Fecha"));
				}
			}
			return vResult;
		}
		#endregion //Metodos Generados

		#region Código Programadar
		private ValidationResult CambioABolivaresEgresoValidating() {
			ValidationResult vResult = ValidationResult.Success;
			if ((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar) || (Action == eAccionSR.Anular)) {
				return ValidationResult.Success;
			} else if (NotIsInMonedaLocalEgreso && (CambioABolivaresEgreso == 1 || CambioABolivaresEgreso <= 0)) {
				vResult = new ValidationResult("El campo Cambio de Egreso debe ser distinto de 1 y mayor que 0.");
			} else if (!NotIsInMonedaLocalEgreso && CambioABolivaresEgreso != 1) {
				vResult = new ValidationResult("El campo Cambio de Egreso debe ser igual a 1.");
			}
			return vResult;
		}

		private ValidationResult MontoTransferenciaEgresoValidating() {
			ValidationResult vResult = ValidationResult.Success;
			if ((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar) || (Action == eAccionSR.Anular)) {
				return ValidationResult.Success;
			} else if (MontoTransferenciaEgreso <= 0) {
				vResult = new ValidationResult("El campo Monto de Egreso debe ser mayor que 0.");
			} else if (!LibString.IsNullOrEmpty(CodigoMonedaCuentaBancariaOrigen, true) && LibString.S1IsEqualToS2(CodigoMonedaCuentaBancariaOrigen, CodigoMonedaCuentaBancariaDestino) && MontoTransferenciaEgreso < MontoTransferenciaIngreso) {
				vResult = new ValidationResult("Si las Monedas de las Cuentas Bancarias coinciden, El Monto de Ingreso no debe ser mayor al Monto de Egreso.");
			}
			return vResult;
		}

		private ValidationResult MontoComisionEgresoValidating() {
			ValidationResult vResult = ValidationResult.Success;
			if ((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar) || (Action == eAccionSR.Anular)) {
				return ValidationResult.Success;
			} else if (GeneraComisionEgreso && MontoComisionEgreso <= 0) {
				vResult = new ValidationResult("El campo Monto Comisión de Egreso debe ser mayor que 0.");
			}
			return vResult;
		}

		private ValidationResult MontoImpuestoBancarioEgresoValidating() {
			ValidationResult vResult = ValidationResult.Success;
			if ((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar) || (Action == eAccionSR.Anular)) {
				return ValidationResult.Success;
			} else if (GeneraImpuestoBancarioEgreso && MontoImpuestoBancarioEgreso <= 0) {
				vResult = new ValidationResult("El campo Monto Impuesto Bancario de Egreso debe ser mayor que 0.");
			}
			return vResult;
		}

		private ValidationResult CambioABolivaresIngresoValidating() {
			ValidationResult vResult = ValidationResult.Success;
			if ((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar) || (Action == eAccionSR.Anular)) {
				return ValidationResult.Success;
			} else if (NotIsInMonedaLocalIngreso && (CambioABolivaresIngreso == 1 || CambioABolivaresIngreso <= 0)) {
				vResult = new ValidationResult("El campo Cambio de Ingreso debe ser distinto de 1 y mayor que 0.");
			} else if (!NotIsInMonedaLocalIngreso && CambioABolivaresIngreso != 1) {
				vResult = new ValidationResult("El campo Cambio de Ingreso debe ser igual a 1.");
			}
			return vResult;
		}

		private ValidationResult MontoTransferenciaIngresoValidating() {
			ValidationResult vResult = ValidationResult.Success;
			if ((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar) || (Action == eAccionSR.Anular)) {
				return ValidationResult.Success;
			} else if (MontoTransferenciaIngreso <= 0) {
				vResult = new ValidationResult("El campo Monto de Ingreso debe ser mayor que 0.");
			} else if (!LibString.IsNullOrEmpty(CodigoMonedaCuentaBancariaDestino, true) && LibString.S1IsEqualToS2(CodigoMonedaCuentaBancariaOrigen, CodigoMonedaCuentaBancariaDestino) && MontoTransferenciaEgreso < MontoTransferenciaIngreso) {
				vResult = new ValidationResult("Si las Monedas de las Cuentas Bancarias coinciden, El Monto de Ingreso no debe ser mayor al Monto de Egreso.");
			}
			return vResult;
		}

		private ValidationResult MontoComisionIngresoValidating() {
			ValidationResult vResult = ValidationResult.Success;
			if ((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar) || (Action == eAccionSR.Anular)) {
				return ValidationResult.Success;
			} else if (GeneraComisionIngreso && MontoComisionIngreso <= 0) {
				vResult = new ValidationResult("El campo Monto Comisión de Ingreso debe ser mayor que 0.");
			}
			return vResult;
		}

		private ValidationResult MontoImpuestoBancarioIngresoValidating() {
			ValidationResult vResult = ValidationResult.Success;
			if ((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar) || (Action == eAccionSR.Anular)) {
				return ValidationResult.Success;
			} else if (GeneraImpuestoBancarioIngreso && MontoImpuestoBancarioIngreso <= 0) {
				vResult = new ValidationResult("El campo Monto Impuesto Bancario de Ingreso debe ser mayor que 0.");
			}
			return vResult;
		}

		private ValidationResult CodigoConceptoComisionEgresoValidating() {
			ValidationResult vResult = ValidationResult.Success;
			if ((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar) || (Action == eAccionSR.Anular)) {
				return ValidationResult.Success;
			} else if (GeneraComisionEgreso && LibString.IsNullOrEmpty(CodigoConceptoComisionEgreso)) {
				vResult = new ValidationResult("El campo Concepto Comisión de Egreso es requerido.");
			}
			return vResult;
		}

		private ValidationResult CodigoConceptoComisionIngresoValidating() {
			ValidationResult vResult = ValidationResult.Success;
			if ((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar) || (Action == eAccionSR.Anular)) {
				return ValidationResult.Success;
			} else if (GeneraComisionIngreso && LibString.IsNullOrEmpty(CodigoConceptoComisionIngreso)) {
				vResult = new ValidationResult("El campo Concepto Comisión de Ingreso es requerido.");
			}
			return vResult;
		}

		private ValidationResult CodigoCuentasBancariasValidating() {
			ValidationResult vResult = ValidationResult.Success;
			if ((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar) || (Action == eAccionSR.Anular)) {
				return ValidationResult.Success;
			} else if (LibString.S1IsEqualToS2(CodigoCuentaBancariaDestino, CodigoCuentaBancariaOrigen)) {
				vResult = new ValidationResult("Los campos Cuenta Bancaria (Origen y Destino) no deben coincidir.");
			}
			return vResult;
		}

		public bool NotIsInMonedaLocalEgreso {
			get {
				Comun.Ccl.TablasGen.IMonedaLocalActual vMonedaLocalActual = new Comun.Brl.TablasGen.clsMonedaLocalActual();
				vMonedaLocalActual.CargarTodasEnMemoriaYAsignarValoresDeLaActual(LibDefGen.ProgramInfo.Country, LibDate.Today());
				bool vIsInMonedaLocal = LibString.S1IsInS2(vMonedaLocalActual.NombreMoneda(LibDate.Today()), NombreMonedaCuentaBancariaOrigen);
				if (vIsInMonedaLocal) {
					CambioABolivaresEgreso = 1;
				}
				return IsInsertar && !vIsInMonedaLocal;
			}
		}

		public bool NotIsInMonedaLocalIngreso {
			get {
				Comun.Ccl.TablasGen.IMonedaLocalActual vMonedaLocalActual = new Comun.Brl.TablasGen.clsMonedaLocalActual();
				vMonedaLocalActual.CargarTodasEnMemoriaYAsignarValoresDeLaActual(LibDefGen.ProgramInfo.Country, LibDate.Today());
				bool vIsInMonedaLocal = LibString.S1IsInS2(vMonedaLocalActual.NombreMoneda(LibDate.Today()), NombreMonedaCuentaBancariaDestino);
				if (vIsInMonedaLocal) {
					CambioABolivaresIngreso = 1;
				}
				return IsInsertar && !vIsInMonedaLocal;
			}
		}

		public bool IsEnabledGeneraComisionEgreso {
			get { return IsInsertar && GeneraComisionEgreso; }
		}

		public bool IsEnabledGeneraImpuestoBancarioEgreso {
			get { return IsInsertar && GeneraImpuestoBancarioEgreso; }
		}

		public bool IsEnabledGeneraComisionIngreso {
			get { return IsInsertar && GeneraComisionIngreso; }
		}

		public bool IsEnabledGeneraImpuestoBancarioIngreso {
			get { return IsInsertar && GeneraImpuestoBancarioIngreso; }
		}

		public bool IsInsertar { get; set; }

		public bool NotIsInsertar {
			get { return !IsInsertar; }
		}

		protected override void ExecuteSpecialAction(eAccionSR valAction) {
			if (valAction == eAccionSR.Anular) {
				string vConfirmMsgFormat = string.Format("¿Está seguro de que desea {0} la Transferencia?", LibString.LCase(valAction.GetDescription()));
				if (LibMessages.MessageBox.YesNo(this, vConfirmMsgFormat, ModuleName)) {
					AnulaTransferencia();
					bool vResult = ((ITransferenciaEntreCuentasBancariasPdn) BusinessComponent).CambiarStatusTransferencia(Model, valAction);
					DialogResult = vResult;
					CloseOnActionComplete = vResult;
					LibMessages.RefreshList.Send(ModuleName);
				} else {
					IsDirty = false;
					DialogResult = false;
					RaiseRequestCloseEvent();
				}
			} else {
				base.ExecuteSpecialAction(valAction);
			}
		}

		void AnulaTransferencia() {
			Model.StatusAsEnum = eStatusTransferenciaBancaria.Anulada;
			Model.FechaDeAnulacion = LibDate.Today();
		}

		public void AsignaTasaDelDiaEgreso() {
			if (LibString.IsNullOrEmpty(CodigoMonedaCuentaBancariaOrigen)) {
				return;
			}
			vMonedaLocal.InstanceMonedaLocalActual.CargarTodasEnMemoriaYAsignarValoresDeLaActual(LibDefGen.ProgramInfo.Country, LibDate.Today());
			if (!vMonedaLocal.InstanceMonedaLocalActual.EsMonedaLocalDelPais(CodigoMonedaCuentaBancariaOrigen)) {
				decimal vTasa = 1;
				ConexionCodigoMonedaEgreso = FirstConnectionRecordOrDefault<FkMonedaViewModel>("Moneda", LibSearchCriteria.CreateCriteriaFromText("Codigo", CodigoMonedaCuentaBancariaOrigen));
				if (((ICambioPdn) new clsCambioNav()).ExisteTasaDeCambioParaElDia(CodigoMonedaCuentaBancariaOrigen, Fecha, out vTasa)) {
					CambioABolivaresEgreso = vTasa;
				} else {
					bool vElProgramaEstaEnModoAvanzado = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "EsModoAvanzado");
					bool vUsarLimiteMaximoParaIngresoDeTasaDeCambio = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "UsarLimiteMaximoParaIngresoDeTasaDeCambio");
					decimal vMaximoLimitePermitidoParaLaTasaDeCambio = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetDecimal("Parametros", "MaximoLimitePermitidoParaLaTasaDeCambio");
					CambioViewModel vViewModel = new CambioViewModel(CodigoMonedaCuentaBancariaOrigen, vUsarLimiteMaximoParaIngresoDeTasaDeCambio, vMaximoLimitePermitidoParaLaTasaDeCambio, vElProgramaEstaEnModoAvanzado);
					vViewModel.InitializeViewModel(eAccionSR.Insertar);
					vViewModel.OnCambioAMonedaLocalChanged += CambioChangedEgreso;
					vViewModel.FechaDeVigencia = Fecha;
					vViewModel.CodigoMoneda = CodigoMonedaCuentaBancariaOrigen;
					vViewModel.NombreMoneda = NombreMonedaCuentaBancariaOrigen;
					vViewModel.IsEnabledMoneda = false;
					vViewModel.IsEnabledFecha = false;
					bool vResult = LibMessages.EditViewModel.ShowEditor(vViewModel, true);
					if (!vResult) {
						AsignaTasaDelDiaEgreso();
					}
				}
			} else {
				CambioABolivaresEgreso = 1;
			}
		}

		public void AsignaTasaDelDiaIngreso() {
			if (LibString.IsNullOrEmpty(CodigoMonedaCuentaBancariaDestino)) {
				return;
			}
			vMonedaLocal.InstanceMonedaLocalActual.CargarTodasEnMemoriaYAsignarValoresDeLaActual(LibDefGen.ProgramInfo.Country, LibDate.Today());
			if (!vMonedaLocal.InstanceMonedaLocalActual.EsMonedaLocalDelPais(CodigoMonedaCuentaBancariaDestino)) {
				decimal vTasa = 1;
				ConexionCodigoMonedaIngreso = FirstConnectionRecordOrDefault<FkMonedaViewModel>("Moneda", LibSearchCriteria.CreateCriteriaFromText("Codigo", CodigoMonedaCuentaBancariaDestino));
				if (((ICambioPdn) new clsCambioNav()).ExisteTasaDeCambioParaElDia(CodigoMonedaCuentaBancariaDestino, Fecha, out vTasa)) {
					CambioABolivaresIngreso = vTasa;
				} else {
					bool vElProgramaEstaEnModoAvanzado = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "EsModoAvanzado");
					bool vUsarLimiteMaximoParaIngresoDeTasaDeCambio = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "UsarLimiteMaximoParaIngresoDeTasaDeCambio");
					decimal vMaximoLimitePermitidoParaLaTasaDeCambio = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetDecimal("Parametros", "MaximoLimitePermitidoParaLaTasaDeCambio");
					CambioViewModel vViewModel = new CambioViewModel(CodigoMonedaCuentaBancariaDestino, vUsarLimiteMaximoParaIngresoDeTasaDeCambio, vMaximoLimitePermitidoParaLaTasaDeCambio, vElProgramaEstaEnModoAvanzado);
					vViewModel.InitializeViewModel(eAccionSR.Insertar);
					vViewModel.OnCambioAMonedaLocalChanged += CambioChangedIngreso;
					vViewModel.FechaDeVigencia = Fecha;
					vViewModel.CodigoMoneda = CodigoMonedaCuentaBancariaDestino;
					vViewModel.NombreMoneda = NombreMonedaCuentaBancariaDestino;
					vViewModel.IsEnabledMoneda = false;
					vViewModel.IsEnabledFecha = false;
					bool vResult = LibMessages.EditViewModel.ShowEditor(vViewModel, true);
					if (!vResult) {
						AsignaTasaDelDiaIngreso();
					}
				}
			} else {
				CambioABolivaresIngreso = 1;
			}
		}

		private void CambioChangedEgreso(decimal valCambio) {
			CambioABolivaresEgreso = valCambio;
		}

		private void CambioChangedIngreso(decimal valCambio) {
			CambioABolivaresIngreso = valCambio;
		}
		#endregion //Código Programador

	} //End of class TransferenciaEntreCuentasBancariasViewModel

} //End of namespace Galac.Adm.Uil.Banco

