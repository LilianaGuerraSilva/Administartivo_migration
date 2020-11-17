using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Galac.Saw.Wrp.Administrativo {
    public interface IWrpUbicacionArticulo {
        void Execute(string vfwAction);
        string Choose(string vfwParamInitializationList, string vfwParamFixedList);
        void InitializeComponent(string vfwLogin, string vfwPassword, string vfwPath);
        void InitializeContext(string vfwInfo);
        void InitializeDefProg(string vfwProgramInitials, string vfwProgramVersion, string vfwDbVersion, string vfwStrDateOfVersion, string vfwStrHourOfVersion, string vfwValueSpecialCharacteristic, string vfwCountry, string vfwCMTO, bool vfwUsePASOnLine);
        void VerUbicacionArticulos(string vfmCompania, string vfmAlmacen, string vfwXmlCodigoArticulos);
    }
}
