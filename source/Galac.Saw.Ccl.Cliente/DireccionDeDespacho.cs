using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;

namespace Galac.Saw.Ccl.Cliente {
    [Serializable]
    public class DireccionDeDespacho: IEquatable<DireccionDeDespacho>, INotifyPropertyChanged, ICloneable{
        #region Variables
        private int _ConsecutivoCompania;
        private string _CodigoCliente;
        private int _ConsecutivoDireccion;
        private string _PersonaContacto;
        private string _Direccion;
        private string _Ciudad;
        private string _ZonaPostal;
        #endregion //Variables
        #region Propiedades

        public int ConsecutivoCompania {
            get { return _ConsecutivoCompania; }
            set { _ConsecutivoCompania = value; }
        }

        public string CodigoCliente {
            get { return _CodigoCliente; }
            set { _CodigoCliente = LibString.Mid(value, 0, 10); }
        }

        public int ConsecutivoDireccion {
            get { return _ConsecutivoDireccion; }
            set { _ConsecutivoDireccion = value; }
        }

        public string PersonaContacto {
            get { return _PersonaContacto; }
            set { 
                _PersonaContacto = LibString.Mid(value, 0, 20);
                OnPropertyChanged("PersonaContacto");
            }
        }

        public string Direccion {
            get { return _Direccion; }
            set { 
                _Direccion = LibString.Mid(value, 0, 100);
                OnPropertyChanged("Direccion");
            }
        }

        public string Ciudad {
            get { return _Ciudad; }
            set { 
                _Ciudad = LibString.Mid(value, 0, 100);
                OnPropertyChanged("Ciudad");
            }
        }

        public string ZonaPostal {
            get { return _ZonaPostal; }
            set { 
                _ZonaPostal = LibString.Mid(value, 0, 7);
                OnPropertyChanged("ZonaPostal");
            }
        }
        #endregion //Propiedades
        #region Constructores
        #endregion //Constructores
        #region Metodos Generados

        public object TextDateLastModifiedForInput() {
            return "";
        }

        public void Clear() {
            ConsecutivoCompania = LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania");
            CodigoCliente = string.Empty;
            ConsecutivoDireccion = 0;
            PersonaContacto = string.Empty;
            Direccion = string.Empty;
            Ciudad = string.Empty;
            ZonaPostal = string.Empty;
        }

        //Tipo de retorno original: DireccionDeDespacho, modificado a object, Cristian
        public object Clone() {
            DireccionDeDespacho vResult = new DireccionDeDespacho();
            vResult.ConsecutivoCompania = _ConsecutivoCompania;
            vResult.CodigoCliente = _CodigoCliente;
            vResult.ConsecutivoDireccion = _ConsecutivoDireccion;
            vResult.PersonaContacto = _PersonaContacto;
            vResult.Direccion = _Direccion;
            vResult.Ciudad = _Ciudad;
            vResult.ZonaPostal = _ZonaPostal;
            return vResult;
        }

        public override string ToString() {
           return "Consecutivo Compania = " + _ConsecutivoCompania.ToString() +
               "\nC?digo del Cliente = " + _CodigoCliente +
               "\nConsecutivo Direccion = " + _ConsecutivoDireccion.ToString() +
               "\nContacto = " + _PersonaContacto +
               "\nDirecci?n = " + _Direccion +
               "\nCiudad = " + _Ciudad +
               "\nZona Postal = " + _ZonaPostal;
        }

        #region Miembros de IEquatable<DireccionDeDespacho>
        bool IEquatable<DireccionDeDespacho>.Equals(DireccionDeDespacho other) {
            return Object.ReferenceEquals(this, other);
        }
        #endregion //IEquatable<DireccionDeDespacho>

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name) {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
        #endregion //Metodos Generados


    } //End of class DireccionDeDespacho

} //End of namespace Galac.Saw.Ccl.Cliente

