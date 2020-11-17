using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;

namespace Galac.Saw.Ccl.Tablas {
    [Serializable]
    public class PropAnalisisVenc {
        #region Variables
        private int _SecuencialUnique0;
        private int _PrimerVencimiento;
        private int _SegundoVencimiento;
        private int _TercerVencimiento;
        private string _NombreOperador;
        private DateTime _FechaUltimaModificacion;
        private long _fldTimeStamp;
        XmlDocument _datos;
        #endregion //Variables
        #region Propiedades

        public int SecuencialUnique0 {
            get { return _SecuencialUnique0; }
            set { _SecuencialUnique0 = value; }
        }

        public int PrimerVencimiento {
            get { return _PrimerVencimiento; }
            set { _PrimerVencimiento = value; }
        }

        public int SegundoVencimiento {
            get { return _SegundoVencimiento; }
            set { _SegundoVencimiento = value; }
        }

        public int TercerVencimiento {
            get { return _TercerVencimiento; }
            set { _TercerVencimiento = value; }
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

        public XmlDocument Datos {
            get { return _datos; }
            set { _datos = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public PropAnalisisVenc() {
            Clear();
        }
        #endregion //Constructores
        #region Metodos Generados

        public object TextDateLastModifiedForInput() {
            return "";
        }

        public void Clear() {
            SecuencialUnique0 = 0;
            PrimerVencimiento = 0;
            SegundoVencimiento = 0;
            TercerVencimiento = 0;
            NombreOperador = string.Empty;
            FechaUltimaModificacion = LibDate.Today();
            fldTimeStamp = 0;
        }

        public PropAnalisisVenc Clone() {
            PropAnalisisVenc vResult = new PropAnalisisVenc();
            vResult.SecuencialUnique0 = _SecuencialUnique0;
            vResult.PrimerVencimiento = _PrimerVencimiento;
            vResult.SegundoVencimiento = _SegundoVencimiento;
            vResult.TercerVencimiento = _TercerVencimiento;
            vResult.NombreOperador = _NombreOperador;
            vResult.FechaUltimaModificacion = _FechaUltimaModificacion;
            vResult.fldTimeStamp = _fldTimeStamp;
            return vResult;
        }

        public override string ToString() {
           return "Secuencial Unique 0 = " + _SecuencialUnique0.ToString() +
               "\nPrimer Vencimiento = " + _PrimerVencimiento.ToString() +
               "\nSegundo Vencimiento = " + _SegundoVencimiento.ToString() +
               "\nTercer Vencimiento = " + _TercerVencimiento.ToString() +
               "\nNombre Operador = " + _NombreOperador +
               "\nFecha Ultima Modificacion = " + _FechaUltimaModificacion.ToShortDateString();
        }
        #endregion //Metodos Generados


    } //End of class PropAnalisisVenc

} //End of namespace Galac.Saw.Ccl.Tablas

