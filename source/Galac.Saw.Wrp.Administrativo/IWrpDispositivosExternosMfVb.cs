using LibGalac.Aos.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Galac.Saw.Wrp.DispositivosExternos
{
    public interface IWrpBalanzaVb {        
        void Execute(string vfwAction, string vfwCurrentCompany, string vfwCurrentParameters);
        string Choose(string vfwParamInitializationList, string vfwParamFixedList);
        void InitializeComponent(string vfwLogin, string vfwPassword, string vfwPath);
        void InitializeDefProg(string vfwProgramInitials, string vfwProgramVersion, string vfwDbVersion, string vfwStrDateOfVersion, string vfwStrHourOfVersion, string vfwValueSpecialCharacteristic, string vfwCountry, string vfwCMTO, bool vfwUsePASOnLine);
        void InitializeContext(string vfwInfo);
    }

    public interface IWrpImpresoraFisaclVb {
        void InitializeComponent(string vfwLogin, string vfwPassword, string vfwPath);
        void InitializeDefProg(string vfwProgramInitials, string vfwProgramVersion, string vfwDbVersion, string vfwStrDateOfVersion, string vfwStrHourOfVersion, string vfwValueSpecialCharacteristic, string vfwCountry, string vfwCMTO, bool vfwUsePASOnLine);
        void InitializeContext(string vfwInfo);
        string ObtenerSerialMaquinaFiscal(string vfwXmlImpresoraFiscal);
        string ObtenerUltimoNumeroFacturaImpresa(string vfwXmlImpresoraFiscal);
        string ObtenerUltimoNumeroNotaDeCreditoImpresa(string vfwXmlImpresoraFiscal);
        string ObtenerUltimoNumeroReporteZ(string vfwXmlImpresoraFiscal);
        bool RealizarCierreZ(string vfwXmlImpresoraFiscal,ref string NumDocumento);
        bool RealizarCierreX(string vfwXmlImpresoraFiscal);
        bool ImprimirVenta(string vfwXmlImpresoraFiscal, string vfwXmlDocumentoFiscal,ref string NumDocumento);
        bool ImprimirNotaDeCredito(string vfwXmlImpresoraFiscal, string vfwXmlDocumentoFiscal,ref string NumDocumento);
        bool AnularDocumentoFiscal(string vfwXmlImpresoraFiscal, bool vfwXmlAbrirConexion);
        bool ImprimirDocumentoNoFiscal(string vfwXmlImpresoraFiscal, string valTextoNoFiscal, string valDescripcion);
    }
}
