using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
 

namespace Galac.Saw.Ccl.SttDef {
    [Serializable]
    public class ClienteStt : ISettDefinition {
        private string _GroupName = null;
        private string _Module = null;

        public string GroupName {
            get { return _GroupName; }
            set { _GroupName = value; }
        }

        public string Module {
            get { return _Module; }
            set { _Module = value; }
        }
        #region Variables
        private string _CodigoGenericoCliente;
        private int _LongitudCodigoCliente;
        private bool _AvisoDeClienteConDeuda;
        private decimal _MontoApartirDelCualEnviarAvisoDeuda;
        private bool _UsaCodigoClienteEnPantalla;
        private bool _BuscarClienteXRifAlFacturar;
        private bool _ColocarEnFacturaElVendedorAsinagoAlCliente;
        private bool _ImprimirDatosClienteEnCompFiscal;
        private bool _AvisoDeFacturacionMenor;
        private bool _RellenaCerosAlaIzquierda;
        private string _NombreCampoDefinibleCliente1;
        private long _fldTimeStamp;
        XmlDocument _datos;
        #endregion //Variables
        #region Propiedades

        public string CodigoGenericoCliente {
            get { return _CodigoGenericoCliente; }
            set { _CodigoGenericoCliente = LibString.Mid(value, 0, 10); }
        }

        public int LongitudCodigoCliente {
            get { return _LongitudCodigoCliente; }
            set { _LongitudCodigoCliente = value; }
        }

        public bool AvisoDeClienteConDeudaAsBool {
            get { return _AvisoDeClienteConDeuda; }
            set { _AvisoDeClienteConDeuda = value; }
        }

        public string AvisoDeClienteConDeuda {
            set { _AvisoDeClienteConDeuda = LibConvert.SNToBool(value); }
        }


        public decimal MontoApartirDelCualEnviarAvisoDeuda {
            get { return _MontoApartirDelCualEnviarAvisoDeuda; }
            set { _MontoApartirDelCualEnviarAvisoDeuda = value; }
        }

        public bool UsaCodigoClienteEnPantallaAsBool {
            get { return _UsaCodigoClienteEnPantalla; }
            set { _UsaCodigoClienteEnPantalla = value; }
        }

        public string UsaCodigoClienteEnPantalla {
            set { _UsaCodigoClienteEnPantalla = LibConvert.SNToBool(value); }
        }


        public bool BuscarClienteXRifAlFacturarAsBool {
            get { return _BuscarClienteXRifAlFacturar; }
            set { _BuscarClienteXRifAlFacturar = value; }
        }

        public string BuscarClienteXRifAlFacturar {
            set { _BuscarClienteXRifAlFacturar = LibConvert.SNToBool(value); }
        }


        public bool ColocarEnFacturaElVendedorAsinagoAlClienteAsBool {
            get { return _ColocarEnFacturaElVendedorAsinagoAlCliente; }
            set { _ColocarEnFacturaElVendedorAsinagoAlCliente = value; }
        }

        public string ColocarEnFacturaElVendedorAsinagoAlCliente {
            set { _ColocarEnFacturaElVendedorAsinagoAlCliente = LibConvert.SNToBool(value); }
        }


        public bool ImprimirDatosClienteEnCompFiscalAsBool {
            get { return _ImprimirDatosClienteEnCompFiscal; }
            set { _ImprimirDatosClienteEnCompFiscal = value; }
        }

        public string ImprimirDatosClienteEnCompFiscal {
            set { _ImprimirDatosClienteEnCompFiscal = LibConvert.SNToBool(value); }
        }


        public bool AvisoDeFacturacionMenorAsBool {
            get { return _AvisoDeFacturacionMenor; }
            set { _AvisoDeFacturacionMenor = value; }
        }

        public string AvisoDeFacturacionMenor {
            set { _AvisoDeFacturacionMenor = LibConvert.SNToBool(value); }
        }

        public bool RellenaCerosAlaIzquierdaAsBool {
            get { return _RellenaCerosAlaIzquierda; }
            set { _RellenaCerosAlaIzquierda = value; }
        }

        public string RellenaCerosAlaIzquierda {
            set { _RellenaCerosAlaIzquierda = LibConvert.SNToBool(value); }
        }

        public string NombreCampoDefinibleCliente1 {
            get { return _NombreCampoDefinibleCliente1; }
            set { _NombreCampoDefinibleCliente1 = LibString.Mid(value, 0, 20); }
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

        public ClienteStt() {
            Clear();
        }
        #endregion //Constructores
        #region Metodos Generados

        public object TextDateLastModifiedForInput() {
            return "";
        }

        public void Clear() {
            CodigoGenericoCliente = "";
            LongitudCodigoCliente = 0;
            AvisoDeClienteConDeudaAsBool = false;
            MontoApartirDelCualEnviarAvisoDeuda = 0;
            UsaCodigoClienteEnPantallaAsBool = false;
            BuscarClienteXRifAlFacturarAsBool = false;
            ColocarEnFacturaElVendedorAsinagoAlClienteAsBool = false;
            ImprimirDatosClienteEnCompFiscalAsBool = false;
            AvisoDeFacturacionMenorAsBool = false;
            RellenaCerosAlaIzquierdaAsBool = false;
            NombreCampoDefinibleCliente1 = "";
            fldTimeStamp = 0;
        }

        public ClienteStt Clone() {
            ClienteStt vResult = new ClienteStt();
            vResult.CodigoGenericoCliente = _CodigoGenericoCliente;
            vResult.LongitudCodigoCliente = _LongitudCodigoCliente;
            vResult.AvisoDeClienteConDeudaAsBool = _AvisoDeClienteConDeuda;
            vResult.MontoApartirDelCualEnviarAvisoDeuda = _MontoApartirDelCualEnviarAvisoDeuda;
            vResult.UsaCodigoClienteEnPantallaAsBool = _UsaCodigoClienteEnPantalla;
            vResult.BuscarClienteXRifAlFacturarAsBool = _BuscarClienteXRifAlFacturar;
            vResult.ColocarEnFacturaElVendedorAsinagoAlClienteAsBool = _ColocarEnFacturaElVendedorAsinagoAlCliente;
            vResult.ImprimirDatosClienteEnCompFiscalAsBool = _ImprimirDatosClienteEnCompFiscal;
            vResult.AvisoDeFacturacionMenorAsBool = _AvisoDeFacturacionMenor;
            vResult.RellenaCerosAlaIzquierdaAsBool = _RellenaCerosAlaIzquierda;
            vResult.NombreCampoDefinibleCliente1 = _NombreCampoDefinibleCliente1;
            vResult.fldTimeStamp = _fldTimeStamp;
            return vResult;
        }

        public override string ToString() {
           return "Cliente genérico ...... = " + _CodigoGenericoCliente +
               "\nLongitud del código del Cliente .... = " + _LongitudCodigoCliente.ToString() +
               "\nAvisar si el Cliente tiene una deuda  = " + _AvisoDeClienteConDeuda +
               "\nMonto a partir del cual enviar aviso deuda. = " + _MontoApartirDelCualEnviarAvisoDeuda.ToString() +
               "\nsar el código del Cliente.. = " + _UsaCodigoClienteEnPantalla +
               "\nsar el Rif del Cliente.... = " + _BuscarClienteXRifAlFacturar +
               "\nColocar en Factura y Cotización el Vendedor Asignado al Cliente .......... = " + _ColocarEnFacturaElVendedorAsinagoAlCliente +
               "\nImprimir Datos del Cliente en Comprobante de Impresión Fiscal ... = " + _ImprimirDatosClienteEnCompFiscal +
               "\nn un periodo menor a 30 días..... = " + _AvisoDeFacturacionMenor +
               "\nRellena Ceros Ala Izquierda = " + _RellenaCerosAlaIzquierda +
               "\nCampo Definible 1 = " + _NombreCampoDefinibleCliente1;
        }
        #endregion //Metodos Generados


    } //End of class ClienteStt

} //End of namespace Galac.Saw.Ccl.SttDef

