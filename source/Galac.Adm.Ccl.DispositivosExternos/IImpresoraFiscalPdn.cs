using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using LibGalac.Aos.Base;
using Galac.Adm.Ccl.DispositivosExternos;

namespace Galac.Adm.Ccl.DispositivosExternos {
    public interface IImpresoraFiscalPdn:ILibPdn {
        bool AbrirConexion();
        bool CerrarConexion();
        bool RealizarReporteZ();
        bool RealizarReporteX();
        bool ComprobarEstado();
        eStatusImpresorasFiscales EstadoDelPapel(bool valAbrirConexion);
        bool ImprimirNotaCredito(XElement valDocumentoFiscal);
        bool ImprimirFacturaFiscal(XElement valDocumentoFiscal);
        string ObtenerSerial(bool valAbrirConexion);
        string ObtenerUltimoNumeroFactura(bool valAbrirConexion);
        string ObtenerUltimoNumeroNotaDeCredito(bool valAbrirConexion);
        string ObtenerUltimoNumeroReporteZ(bool valAbrirConexion);
        bool CancelarDocumentoFiscalEnImpresion(bool valAbrirConexion);
        string ObtenerFechaYHora();
        bool ReimprimirDocumentoNoFiscal(string valDesde,string valHasta);
        bool ReimprimirDocumentoFiscal(string valDesde,string valHasta,string valTipo);
        IFDiagnostico RealizarDiagnostico(bool valAbrirPuerto = false);
        bool EstatusDeComunicacion(IFDiagnostico vDiagnostico);
        bool VersionDeControladores(IFDiagnostico vDiagnostico);
        bool AlicuotasRegistradas(IFDiagnostico vDiagnostico);
        bool ConsultarConfiguracion(IFDiagnostico iFDiagnostico);
        bool FechaYHora(IFDiagnostico vDiagnostico);
        bool ColaDeImpresion(IFDiagnostico vDiagnostico);
        bool ImprimirDocumentoNoFiscal(string valTextoNoFiscal, XElement valDatosDelDocumento);
    }
}
