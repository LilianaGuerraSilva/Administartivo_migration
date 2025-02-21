using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using LibGalac.Aos.Base;
using Galac.Adm.Ccl.DispositivosExternos;
using Galac.Saw.Ccl.SttDef;

namespace Galac.Adm.Ccl.DispositivosExternos {
    public interface IImpresoraFiscalPdn:ILibPdn {
        bool AbrirConexion();
        bool CerrarConexion();
        bool RealizarReporteZ();
        bool RealizarReporteX();
        bool ComprobarEstado();
        eStatusImpresorasFiscales EstadoDelPapel(bool valAbrirConexion);
        bool ImprimirNotaCredito(XElement valDocumentoFiscal, eTipoDocumentoFactura valTipoDocumento);
        bool ImprimirFacturaFiscal(XElement valDocumentoFiscal, eTipoDocumentoFactura valTipoDocumento);
        bool ImprimirNotaDebito(XElement valDocumentoFiscal, eTipoDocumentoFactura valTipoDocumento);
        string ObtenerSerial(bool valAbrirConexion);
        string ObtenerUltimoNumeroFactura(bool valAbrirConexion);
        string ObtenerUltimoNumeroNotaDeCredito(bool valAbrirConexion);
        string ObtenerUltimoNumeroNotaDeDebito(bool valAbrirConexion);
        string ObtenerUltimoNumeroReporteZ(bool valAbrirConexion);
        bool CancelarDocumentoFiscalEnImpresion(bool valAbrirConexion);
        string ObtenerFechaYHora();
        bool ReimprimirDocumentoNoFiscal(string valDesde,string valHasta);
        bool ReimprimirDocumentoFiscal(string valDesde,string valHasta, eTipoDocumentoFiscal valTipoDeDocumento, eTipoDeBusqueda vtipoDeBusqueda);
        IFDiagnostico RealizarDiagnostico(bool valAbrirPuerto = false);
        bool EstatusDeComunicacion(IFDiagnostico vDiagnostico);
        bool VersionDeControladores(IFDiagnostico vDiagnostico);
        bool AlicuotasRegistradas(IFDiagnostico vDiagnostico);
        bool ConsultarConfiguracion(IFDiagnostico iFDiagnostico);
        bool FechaYHora(IFDiagnostico vDiagnostico);
        bool ColaDeImpresion(IFDiagnostico vDiagnostico);
        bool ImprimirDocumentoNoFiscal(string valTextoNoFiscal, string valDescripcion);
    }
}
