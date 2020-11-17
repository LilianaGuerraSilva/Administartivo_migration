using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Galac.Saw.Wrp.Impuesto {
    public interface IWrpFormatosImpMunicipalesVb {
        void Execute(string vfwAction);
        string Choose(string vfwParamInitializationList, string vfwParamFixedList);
        void InitializeComponent(string vfwLogin, string vfwPassword, string vfwPath);
        void InitializeDefProg(string vfwProgramInitials, string vfwProgramVersion, string vfwDbVersion, string vfwStrDateOfVersion, string vfwStrHourOfVersion, string vfwValueSpecialCharacteristic, string vfwCountry, string vfwCMTO, bool vfwUsePASOnLine);
        void InitializeContext(string vfwInfo);
        string NumeroComprobanteFormateado(string valCodigoMunicipio, string valDatosAEvaluar);
        string CondicionParaFiltroComprobante(string valCodigoMunicipio, string valFechaAplicacion);
        string DatosDeltxtImpuestos(string valCodigoMunicipio, string  valLinea);
        string CampoFormateado(string valCodigoMunicipio, string valColumna, string valValor, string  valNumerico);
        string UsaTipoDeTipodeOperacion(string valCodigoMunicipio);
        bool EscribirTxtImpuestoMunicipal(string valRutaArchivo, string valSQLWhere, int valConsecutivoCompania);
    }
}
