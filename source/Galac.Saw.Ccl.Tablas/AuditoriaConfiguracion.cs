using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using Galac.Saw.Ccl.Tablas;

namespace Galac.Saw.Ccl.Tablas {
    [Serializable]
    public class AuditoriaConfiguracion {
        #region Variables
        private int _ConsecutivoCompania;
        private int _ConsecutivoAuditoria;
        private string _VersionPrograma;
		private DateTime _FechayHora;
		private string _Accion;
		private string _Motivo;
		private string _ConfiguracionOriginal;
		private string _ConfiguracionNueva;
		private string _NombreOperador;
		private DateTime _FechaUltimaModificacion;
        private long _fldTimeStamp;
        #endregion //Variables
        #region Propiedades

        public int ConsecutivoCompania {
            get { return _ConsecutivoCompania; }
            set { _ConsecutivoCompania = value; }
        }

        public int ConsecutivoAuditoria {
            get { return _ConsecutivoAuditoria; }
            set { _ConsecutivoAuditoria = value; }
        }

        public string VersionPrograma {
            get { return _VersionPrograma; }
            set { _VersionPrograma = LibString.Mid(value, 0, 5); }
        }
		
		public DateTime FechayHora {
			get { return _FechayHora; }
			set { _FechayHora = LibConvert.DateToDbValue(value); }
		}
		
		public string Accion {
			get { return _Accion; }
			set { _Accion = LibString.Mid(value, 0, 20); }
		}
		
		public string Motivo {
			get { return _Motivo; }
			set { _Motivo = LibString.Mid(value, 0, 255); }
		}
		
		public string ConfiguracionOriginal {
			get { return _ConfiguracionOriginal; }
			set { _ConfiguracionOriginal = LibString.Mid(value, 0, 500); }
		}
		
		public string ConfiguracionNueva {
			get { return _ConfiguracionNueva; }
			set { _ConfiguracionNueva = LibString.Mid(value, 0, 500); }
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

        #endregion //Propiedades
        #region Constructores

        public AuditoriaConfiguracion() {
            Clear();
        }
        #endregion //Constructores
        #region Metodos Generados
		
		public object TextDateLastModifiedForInput() {
			return "";
		}

        public void Clear() {
            ConsecutivoCompania = LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania");
            ConsecutivoAuditoria = 0;
			VersionPrograma = string.Empty;
			FechayHora = LibDate.Today();
			Accion = string.Empty;
			Motivo = string.Empty;
			ConfiguracionOriginal = string.Empty;
			ConfiguracionNueva = string.Empty;
			NombreOperador = string.Empty;
			FechaUltimaModificacion = LibDate.Today();
            fldTimeStamp = 0;
        }

        public AuditoriaConfiguracion Clone() {
            AuditoriaConfiguracion vResult = new AuditoriaConfiguracion();
            vResult.ConsecutivoCompania = _ConsecutivoCompania;
            vResult.ConsecutivoAuditoria = _ConsecutivoAuditoria;
            vResult.VersionPrograma = _VersionPrograma;
			vResult.FechayHora = _FechayHora;
			vResult.Accion = _Accion;
			vResult.Motivo = _Motivo;
			vResult.ConfiguracionOriginal = _ConfiguracionOriginal;
			vResult.ConfiguracionNueva = _ConfiguracionNueva;
			vResult.NombreOperador = _NombreOperador;
			vResult.FechaUltimaModificacion = _FechaUltimaModificacion;
            vResult.fldTimeStamp = _fldTimeStamp;
            return vResult;
        }

        public override string ToString() {
           return "Consecutivo Compania = " + _ConsecutivoCompania.ToString() +
               "\nConsecutivoAuditoria = " + _ConsecutivoAuditoria.ToString() +
               "\nVersionPrograma = " + _VersionPrograma.ToString() +
               "\nFechayHora = " + _FechayHora.ToShortDateString() +
               "\nAccion = " + _Accion.ToString() +
               "\nMotivo = " + _Motivo.ToString() +
               "\nConfiguracionOriginal = " + _ConfiguracionOriginal.ToString() +
               "\nConfiguracionNueva = " + _ConfiguracionNueva.ToString() +
               "\nNombreOperador = " + _NombreOperador.ToString() +
               "\nFechaUltimaModificacion = " + _FechaUltimaModificacion.ToString();
        }
        #endregion //Metodos Generados


    } //End of class AuditoriaConfiguracion

} //End of namespace Galac.Saw.Ccl.Tablas