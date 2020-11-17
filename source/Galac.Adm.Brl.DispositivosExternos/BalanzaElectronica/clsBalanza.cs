
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Galac.Adm.Ccl.DispositivosExternos;
using System.IO;

namespace Galac.Adm.Brl.DispositivosExternos.BalanzaElectronica {

    public abstract class clsBalanza : Balanza {
        private clsConexionPuertoSerial _Conexion;

        public clsConexionPuertoSerial Conexion {
            get {
                return _Conexion;
            }
            set {
                _Conexion = value;
            }
        }

        public clsBalanza(clsConexionPuertoSerial valConexion) {
           Conexion = valConexion;
        }        
       
        public abstract string GetPeso();
        public abstract bool abrirConexion();
        public abstract bool cerrarConexion();
        public abstract bool colocarEnCero();
        public abstract bool VerficarEstado();        
        
    }
}
