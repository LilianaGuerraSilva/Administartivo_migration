using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using LibGalac.Aos.Base;
using Galac.Adm.Ccl.DispositivosExternos;
using System.IO.Ports;
using System.IO;
using System.Runtime.InteropServices;
using LibGalac.Aos.Catching;
namespace Galac.Adm.Brl.DispositivosExternos {
    public class clsConexionPuertoSerial:clsConexionPeriferico {
        [DllImport("kernel32.dll")]
        static extern void Sleep(int mSeg);

        XElement vSerialPortDat;
        SerialPort vPuertoSerial;
        string _DataIn = "";
        string _DataInAux = "";
        bool _dtr;
        bool _rts;
        StopBits vBitsDeParada;
        Parity vParidad;
        Handshake vHandShake;
        eBaudRate _BaudRate;
        ePuerto _Puerto;
        eBitsDeDatos _BitsDeDatos;
        eBitsDeParada _BitsDeParada;
        eParidad _Paridad;
        eControlDeFlujo _ControlFlujo;      
        List<String> listPrevios,listActuales;

        #region MetodosGenerados
        public override bool abrirConexion() {
            try {
                if(!vPuertoSerial.IsOpen) {
                    vPuertoSerial.Open();
                }
                return vPuertoSerial.IsOpen;
            } catch(IOException vEx) {
                throw new GalacException("No hay comunicación con el dispositivo, " + vEx.Message,eExceptionManagementType.Validation);
            } catch(Exception vEx) {
                throw new GalacException("No hay comunicación con el dispositivo, " + vEx.Message,eExceptionManagementType.Validation);
            }
        }

        public override bool cerrarConexion() {
            bool vResult = true;
            if(vPuertoSerial.IsOpen) {
                vPuertoSerial.Close();
                vResult = true;
            }
            return vResult;
        }

        public override bool enviarDatosSync(string datos) {
            bool vReq = false;
            byte NumIntentos = 0;
            bool vCheck = false;
            try {
                do {
                    vPuertoSerial.Write(datos);
                    Sleep(100);
                    _DataInAux = vPuertoSerial.ReadExisting();
                    if(_DataInAux==string.Empty){
                        NumIntentos++;
                    }
                    vCheck=LibString.Len(_DataInAux) < 6;
                    vCheck |= (_DataInAux == string.Empty);
                    vCheck &=NumIntentos<3;
                } while(vCheck);
                vPuertoSerial.DiscardInBuffer();
                if(_DataInAux == string.Empty) {
                    throw new GalacException(" Datos Nulos",eExceptionManagementType.Validation);
                } else if(_DataInAux != _DataIn) {
                    _DataIn = _DataInAux;
                    _DataInAux = "";
                    vReq = true;
                }
            } catch(GalacException vEx) {
                throw new GalacException("No hay comunicación con el dispositivo, " + vEx.Message,eExceptionManagementType.Validation);
            } catch(IOException vEx) {
                throw new GalacException("No hay comunicación con el dispositivo, " + vEx.Message,eExceptionManagementType.Validation);
            } catch(Exception vEx) {
                throw new GalacException("No hay comunicación con el dispositivo, " + vEx.Message,eExceptionManagementType.Validation);
            }
            return vReq;
        }

        public override bool enviarDatosASync(string datos) {
            try {
                vPuertoSerial.Write(datos);
                Sleep(300);
                vPuertoSerial.DiscardInBuffer();            
            } catch(IOException vEx) {
                throw new GalacException("No hay comunicación con el dispositivo, " + vEx.Message,eExceptionManagementType.Validation);
            } catch(Exception vEx) {
                throw new GalacException("No hay comunicación con el dispositivo, " + vEx.Message,eExceptionManagementType.Validation);
            }           
            return true;
        }

        public override string recibirDatos() {
            return _DataIn;
        }

        public override void liberarBuffer() {
            vPuertoSerial.DiscardInBuffer();
            vPuertoSerial.DiscardOutBuffer();
        }
        #endregion MetodosGenerados
        #region MetodosDeLaClase
        public clsConexionPuertoSerial() {

        }
        public clsConexionPuertoSerial(XElement valSerialPortDat) {
            vSerialPortDat = valSerialPortDat;
            InitSerialPort();
        }

        public List<String> getNuevasConexionesEnPuertos() {
            listPrevios.RemoveRange(0,listPrevios.Count);
            listPrevios.AddRange(listActuales);
            listActuales.RemoveRange(0,listActuales.Count);
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;
            foreach(string s in SerialPort.GetPortNames()) {
                listActuales.Add(s);
            }
            List<String> listPuertos = new List<string>();
            for(int i = 0;i < listActuales.Count;i++) {
                if(!listPrevios.Contains(listActuales.ElementAt(i))) {
                    listPuertos.Add(listActuales.ElementAt(i));
                }
            }
            return listPuertos;
        }

        private void SetPortValues() {
            _Puerto = (ePuerto)LibConvert.DbValueToEnum(LibXml.GetPropertyString(vSerialPortDat,"Puerto"));
            _BitsDeDatos = (eBitsDeDatos)LibConvert.DbValueToEnum(LibXml.GetPropertyString(vSerialPortDat,"BitsDatos"));
            _BitsDeParada = (eBitsDeParada)LibConvert.DbValueToEnum(LibXml.GetPropertyString(vSerialPortDat,"BitDeParada"));
            _Paridad = (eParidad)LibConvert.DbValueToEnum(LibXml.GetPropertyString(vSerialPortDat,"Paridad"));
            _BaudRate = (eBaudRate)LibConvert.DbValueToEnum(LibXml.GetPropertyString(vSerialPortDat,"BaudRate"));
            _ControlFlujo = (eControlDeFlujo)LibConvert.DbValueToEnum(LibXml.GetPropertyString(vSerialPortDat,"ControlDeFlujo"));
            _rts = false; 
            _dtr = false; 
        }

        private void InitSerialPort() {
            string vPuertoCom;
            int vBaudRate;
            int vBitsDatos;
            SetPortValues();
            vBaudRate = BaudRateParse(_BaudRate);
            vPuertoCom = PuertoComParse(_Puerto);
            vBitsDatos = BitsDatosParse(_BitsDeDatos);
            vBitsDeParada = StopBitParse(_BitsDeParada);
            vParidad = ParityParse(_Paridad);
            vHandShake = HandShakeParse(_ControlFlujo);
            vPuertoSerial = new SerialPort(vPuertoCom,vBaudRate,vParidad,vBitsDatos,vBitsDeParada);
            vPuertoSerial.Handshake = vHandShake;
            vPuertoSerial.Encoding = Encoding.UTF8;
            //vPuertoSerial.DataReceived += new SerialDataReceivedEventHandler(PuertoSerialDataReceived);// Recepcion Automatica Puerto Serie
        }

        public override string[] ListarPuertos() {
            string[] vListarPuertos = new string[] { };
            try {
                vListarPuertos = System.IO.Ports.SerialPort.GetPortNames();
            } catch(Exception) {
                throw;
            }
            return vListarPuertos;
        }

        //private void PuertoSerialDataReceived(object sender, SerialDataReceivedEventArgs e) {
        //    try {
        //        do {
        //            _DataInAux = vPuertoSerial.ReadExisting();
        //            Thread.Sleep(150);
        //        } while (_DataInAux.Length < 5);
        //        vPuertoSerial.DiscardInBuffer();
        //        if (_DataInAux != _DataIn) {
        //            _DataIn = _DataInAux;
        //            _DataInAux = "";
        //        }
        //        //
        //    } catch (Exception) {

        //    }
        //} 

        #endregion MetodosDeLaClase
        #region ParseValues
        private int BitsDatosParse(eBitsDeDatos vBitsDatos) {
            int vResult = 0;
            vResult = LibConvert.ToInt(LibEnumHelper.GetDescription(vBitsDatos));
            return vResult;
        }

        private int BaudRateParse(eBaudRate vBaudRate) {
            int vResult = 0;
            vResult = LibConvert.ToInt(LibEnumHelper.GetDescription(vBaudRate));
            return vResult;
        }

        private string PuertoComParse(ePuerto vPuertoCom) {
            string vResult = "";
            vResult = LibConvert.ToStr(LibEnumHelper.GetDescription(vPuertoCom));
            return vResult;
        }

        private StopBits StopBitParse(eBitsDeParada vStopBits) {
            switch(vStopBits) {
                case eBitsDeParada.Uno:
                    return StopBits.One;
                case eBitsDeParada.UnoYMedio:
                    return StopBits.OnePointFive;
                case eBitsDeParada.Dos:
                    return StopBits.Two;
                default:
                    return StopBits.One;
            }
        }

        private Parity ParityParse(eParidad vParity) {
            switch(vParity) {
                case eParidad.Impar:
                    return Parity.Even;
                case eParidad.Marca:
                    return Parity.Mark;
                case eParidad.Ninguna:
                    return Parity.None;
                case eParidad.Par:
                    return Parity.Odd;
                case eParidad.Espacio:
                    return Parity.Space;
                default:
                    return Parity.None;
            }
        }

        private Handshake HandShakeParse(eControlDeFlujo vControlFlujo) {
            switch(vControlFlujo) {
                case eControlDeFlujo.Ninguno:
                    return Handshake.None;
                case eControlDeFlujo.RequestToSend:
                    return Handshake.RequestToSend;
                case eControlDeFlujo.RequestToSendXOnXOff:
                    return Handshake.RequestToSendXOnXOff;
                case eControlDeFlujo.XonOff:
                    return Handshake.XOnXOff;
                default:
                    return Handshake.None;
            }
        }

        public override void SetPort(string vPortName) {
            vPuertoSerial.PortName = vPortName;
        }
        #endregion ParseValues        
    }
}
