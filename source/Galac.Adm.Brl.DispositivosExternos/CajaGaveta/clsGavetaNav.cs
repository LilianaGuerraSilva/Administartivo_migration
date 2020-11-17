using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Galac.Adm.Ccl.DispositivosExternos;
using System.IO;
using LibGalac.Aos.Catching;
using System.Xml.Linq;
using LibGalac.Aos.Base;

namespace Galac.Adm.Brl.DispositivosExternos.CajaGaveta {
   
    public class clsGavetaNav:IGavetaPdn {

        clsConexionPuertoSerial _ConexionPuertoSerial;
       
        public clsGavetaNav() { 
        
        }

        XElement SerialPortSettings(ePuerto valPuerto) {
            XElement vResult = new XElement("GpData",
                new XElement("GpResult",
                new XElement("Puerto", LibConvert.EnumToDbValue((int)valPuerto)),
                new XElement("Paridad" , LibConvert.EnumToDbValue((int)eParidad.Ninguna)),
                new XElement("BitsDeParada", LibConvert.EnumToDbValue((int)eBitsDeParada.Ninguno)),
                new XElement("ControlDeFlujo", LibConvert.EnumToDbValue((int)eControlDeFlujo.Ninguno)),
                new XElement("BitsDeDatos", LibConvert.EnumToDbValue((int)eBitsDeDatos.d8)),
                new XElement("BaudRate", LibConvert.EnumToDbValue((int)eBaudRate.b9600))));           
            return vResult;
        }

        bool IGavetaPdn.AbrirGaveta(ePuerto valPuerto, string valComando) {
            bool vResult=false;
            try {
                _ConexionPuertoSerial = new clsConexionPuertoSerial(SerialPortSettings(valPuerto));     
               vResult= _ConexionPuertoSerial.abrirConexion();
               vResult &= _ConexionPuertoSerial.enviarDatosASync(valComando);
               vResult&=_ConexionPuertoSerial.cerrarConexion();
               return vResult;
            } catch (GalacException vEx) {
                throw vEx;
            }                
        }
    }
}
