using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;

namespace Galac.Adm.Ccl.CajaChica {
    [Serializable]
    public class RenglonImpuestoMunicipalRet: IEquatable<RenglonImpuestoMunicipalRet>, INotifyPropertyChanged {
        #region Variables
        private int _ConsecutivoCompania;
        private int _Consecutivo;
        private int _ConsecutivoCxp;
        private string _CodigoRetencion;
        private string _CodigoActividad;
        private decimal _MontoBaseImponible;
        private decimal _AlicuotaRetencion;
        private decimal _MontoRetencion;
        private eTipoDeTransaccionMunicipal _TipoDeTransaccion;
        private string _NombreOperador;
        private DateTime _FechaUltimaModificacion;
        #endregion //Variables
        #region Propiedades

        public int ConsecutivoCompania {
            get { return _ConsecutivoCompania; }
            set { 
                _ConsecutivoCompania = value;
                OnPropertyChanged("ConsecutivoCompania");
            }
        }

        public int Consecutivo {
            get { return _Consecutivo; }
            set { 
                _Consecutivo = value;
                OnPropertyChanged("Consecutivo");
            }
        }

        public int ConsecutivoCxp {
            get { return _ConsecutivoCxp; }
            set { 
                _ConsecutivoCxp = value;
                OnPropertyChanged("ConsecutivoCxp");
            }
        }

        public string CodigoRetencion {
            get { return _CodigoRetencion; }
            set { 
                _CodigoRetencion = LibString.Mid(value, 0, 10);
                OnPropertyChanged("CodigoRetencion");
            }
        }

        public string CodigoActividad {
            get { return _CodigoActividad; }
            set { 
                _CodigoActividad = LibString.Mid(value, 0, 10);
                OnPropertyChanged("CodigoActividad");
            }
        }

        public decimal MontoBaseImponible {
            get { return _MontoBaseImponible; }
            set { 
                _MontoBaseImponible = value;
                OnPropertyChanged("MontoBaseImponible");
            }
        }

        public decimal AlicuotaRetencion {
            get { return _AlicuotaRetencion; }
            set { 
                _AlicuotaRetencion = value;
                OnPropertyChanged("AlicuotaRetencion");
            }
        }

        public decimal MontoRetencion {
            get { return _MontoRetencion; }
            set { 
                _MontoRetencion = value;
                OnPropertyChanged("MontoRetencion");
            }
        }

        public eTipoDeTransaccionMunicipal TipoDeTransaccionAsEnum {
            get { return _TipoDeTransaccion; }
            set { 
                _TipoDeTransaccion = value;
                OnPropertyChanged("TipoDeTransaccion");
            }
        }

        public string TipoDeTransaccion {
            set { _TipoDeTransaccion = (eTipoDeTransaccionMunicipal)LibConvert.DbValueToEnum(value); }
        }

        public string TipoDeTransaccionAsDB {
            get { return LibConvert.EnumToDbValue((int) _TipoDeTransaccion); }
        }

        public string TipoDeTransaccionAsString {
            get { return LibEnumHelper.GetDescription(_TipoDeTransaccion); }
        }

        public string NombreOperador {
            get { return _NombreOperador; }
            set { _NombreOperador = LibString.Mid(value, 0, 10); }
        }

        public DateTime FechaUltimaModificacion {
            get { return _FechaUltimaModificacion; }
            set { _FechaUltimaModificacion = LibConvert.DateToDbValue(value); }
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
            Consecutivo = 0;
            ConsecutivoCxp = 0;
            CodigoRetencion = "";
            CodigoActividad = "";
            MontoBaseImponible = 0;
            AlicuotaRetencion = 0;
            MontoRetencion = 0;
            TipoDeTransaccionAsEnum = eTipoDeTransaccionMunicipal.NoAplica;
            NombreOperador = "";
            FechaUltimaModificacion = LibDate.Today();
        }

        public RenglonImpuestoMunicipalRet Clone() {
            RenglonImpuestoMunicipalRet vResult = new RenglonImpuestoMunicipalRet();
            vResult.ConsecutivoCompania = _ConsecutivoCompania;
            vResult.Consecutivo = _Consecutivo;
            vResult.ConsecutivoCxp = _ConsecutivoCxp;
            vResult.CodigoRetencion = _CodigoRetencion;
            vResult.CodigoActividad = _CodigoActividad;
            vResult.MontoBaseImponible = _MontoBaseImponible;
            vResult.AlicuotaRetencion = _AlicuotaRetencion;
            vResult.MontoRetencion = _MontoRetencion;
            vResult.TipoDeTransaccionAsEnum = _TipoDeTransaccion;
            vResult.NombreOperador = _NombreOperador;
            vResult.FechaUltimaModificacion = _FechaUltimaModificacion;
            return vResult;
        }

        public override string ToString() {
           return "Consecutivo Compania = " + _ConsecutivoCompania.ToString() +
               "\nConsecutivo = " + _Consecutivo.ToString() +
               "\nConsecutivo Cxp = " + _ConsecutivoCxp.ToString() +
               "\nCodigo Retencion = " + _CodigoRetencion +
               "\nBase Imponible  = " + _MontoBaseImponible.ToString() +
               "\n Alicuota Retencion  = " + _AlicuotaRetencion.ToString() +
               "\n MontoRetencion = " + _MontoRetencion.ToString() +
               "\nTipo de Transaccion = " + _TipoDeTransaccion.ToString() +
               "\nNombre Operador = " + _NombreOperador +
               "\nFecha Ultima Modificacion = " + _FechaUltimaModificacion.ToShortDateString();
        }

        #region Miembros de IEquatable<RenglonImpuestoMunicipalRet>
        bool IEquatable<RenglonImpuestoMunicipalRet>.Equals(RenglonImpuestoMunicipalRet other) {
            if (this._ConsecutivoCompania == other.ConsecutivoCompania
                && this._Consecutivo == other.Consecutivo) {
                return true;
            } else {
                return false;
            }
        }
        #endregion //IEquatable<RenglonImpuestoMunicipalRet>

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name) {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
        #endregion //Metodos Generados


    } //End of class RenglonImpuestoMunicipalRet

} //End of namespace Galac.Dbo.Ccl.CajaChica

