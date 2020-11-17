using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using System.ComponentModel;


namespace Galac.Adm.Ccl.CajaChica {
    
    public class AdelantoHelper: INotifyPropertyChanged {

        #region Variables
        private int _ConsecutivoCompania;
        private int _Consecutivo;
        private int _ConsecutivoBeneficiario;
        private DateTime _Fecha;
        private int _ConsecutivoRendicion;
        private decimal _monto;
        private string _Observaciones;


        #endregion //Variables

        #region Propiedades
        public int ConsecutivoCompania {
            get { return _ConsecutivoCompania; }
            set { _ConsecutivoCompania = value;
            PropertyChanged(this, new PropertyChangedEventArgs("ConsecutivoCompania"));
            }
        }

        public int Consecutivo {
            get { return _Consecutivo; }
            set { _Consecutivo = value;
            PropertyChanged(this, new PropertyChangedEventArgs("Consecutivo"));
            }
        }

     
        public int ConsecutivoBeneficiario {
            get { return _ConsecutivoBeneficiario; }
            set { _ConsecutivoBeneficiario = value; }
        }

       
        public string Observaciones {
            get { return _Observaciones; }
            set { _Observaciones = LibString.Mid(value, 0, 200);
                 PropertyChanged(this,new PropertyChangedEventArgs("Observaciones"));          
            }
        }

        public DateTime Fecha {
            get { return _Fecha;}
            set { _Fecha = value;
            PropertyChanged(this, new PropertyChangedEventArgs("Fecha"));
            }
        }

        public int ConsecutivoRendicion {
            get { return _ConsecutivoRendicion; }
            set { _ConsecutivoRendicion = value; }
        }

        public decimal Monto {
            get { return _monto; }
            set { _monto = value;
            PropertyChanged(this, new PropertyChangedEventArgs("Monto"));
            }
        }

        #endregion//Propiedades

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
