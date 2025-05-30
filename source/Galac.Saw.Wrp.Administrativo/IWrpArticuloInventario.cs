using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Galac.Saw.Wrp.Administrativo {
    public interface IWrpArticuloInventario {

        string Choose(string vfwParamInitializationList, string vfwParamFixedList);
        void Execute(string vfwAction, string vfwCurrentMfc, string vfwCurrentParameters);
        void InitializeComponent(string vfwLogin, string vfwPassword, string vfwPath);
        void InitializeContext(string vfwInfo);
        void InitializeDefProg(string vfwProgramInitials, string vfwProgramVersion, string vfwDbVersion, string vfwStrDateOfVersion, string vfwStrHourOfVersion, string vfwValueSpecialCharacteristic, string vfwCountry, string vfwCMTO, bool vfwUsePASOnLine);
        void ImportarPreciosDesdeArchivo(string valfwCurrentMfc, string vfwCurrentParameters);
        void RecalcularInventario(string valfwCurrentMfc, string vfwCurrentParameters);
        void RecalcularInventarioProceso(string valfwCurrentMfc, string vfwCurrentParameters);
        void CalcularCosto(string valfwCurrentMfc, string vfwCurrentParameters);
        decimal DisponibilidadDeArticulo(int valConsecutivoCompania, string valCodigoArticulo, string valLoteDeInventario, int valConsecutivoAlmacen);
    }
}
