using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using LibGalac.Aos.Base;
using System.Xml;

namespace Galac.Saw.Ccl.Contabilizacion {
    public interface IReglasDeContabilizacionPdn : ILibPdn {
        XElement GetListaGrupoDeActivos(string valModule, int valConsecutivoPeriodo);
        void InsertarRegistroPorDefecto(int valConsecutivoCompania);
        void CopiarReglasDeContabilizacion(int valConsecutivoCompania, string valNumero, int valConsecutivoCompaniaDestino);
        bool SePuedeUsarEstaCuenta(bool valUsaAuxiliares, bool valUsaModuloDeActivoFijo, bool valEscogerAuxiliares, string valGetCierreDelEjercicio, ref string valMensaje, XmlDocument valXmlDocument);
        string CorrigeYAjustaLaCuenta(string valCode);
        bool LasCuentasDeReglasDeContabilizacionEstanCompletas(int valConsecutivoCompania,string  valModule, bool valUsaModuloDeActivoFijo, bool valConMensaje, int valTipoDocumento);
    }
}
