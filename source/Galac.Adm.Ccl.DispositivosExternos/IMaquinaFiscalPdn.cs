using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using LibGalac.Aos.Base;
using Galac.Adm.Ccl.DispositivosExternos;

namespace Galac.Adm.Ccl.DispositivosExternos {
    public interface IMaquinaFiscalPdn : ILibPdn {
         bool AbrirConexion();
         bool CerrarConexion();        
         bool RealizarReporteZ();/**/
         bool RealizarReporteX();/**/
		 bool ComprobarConexion();
         eStatusImpresorasFiscales EstadoDelPapel(bool AbrirPuerto);        
         bool ImprimirNotaCredito(XElement valDocumentoFiscal);
         bool ImprimirFacturaFiscal(XElement valDocumentoFiscal);
         string ObtenerSerial();/**/
         string ObtenerUltimoNumeroFactura();/**/
         string ObtenerUltimoNumeroNotaDeCredito();/**/
         string ObtenerUltimoNumeroReporteZ();/**/
         string ObtenerFechaYHora();
         bool CancelarDocumentoFiscalEnImpresion(bool valAbrirConexion);
         bool ReimprimirFactura(string valNumeroFactura);
         bool ReimprimirNotaDeCredito(string valNumeroNotaDeCredito);
         bool ReimprimirReporteZ(string valNumeroReporteZ);
         bool ReimprimirReporteX(string valNumeroReporteX);
         bool ReimprimirDocumentoNoFiscal(string valNumeroDocumentoNoFiscal);           
    }
}
