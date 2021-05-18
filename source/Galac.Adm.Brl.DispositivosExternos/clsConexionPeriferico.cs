using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Galac.Adm.Brl.DispositivosExternos {
    public abstract class clsConexionPeriferico{
        public abstract bool abrirConexion();
        public abstract bool cerrarConexion();
        public abstract bool enviarDatosSync(string datos,bool sendEnquire);
        public abstract bool enviarDatosASync(string datos);
        public abstract string recibirDatos();
        public abstract void liberarBuffer();
        public abstract string[] ListarPuertos();
        public abstract void SetPort(string valPortName);
    }
}
