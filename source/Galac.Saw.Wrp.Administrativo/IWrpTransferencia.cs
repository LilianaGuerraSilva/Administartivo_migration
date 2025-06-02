using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Galac.Saw.Wrp.Administrativo {
    public interface IWrpTransferencia {        
        void InitializeComponent(string vfwLogin, string vfwPassword, string vfwPath);
        void InitializeContext(string vfwInfo);
        void InitializeDefProg(string vfwProgramInitials, string vfwProgramVersion, string vfwDbVersion, string vfwStrDateOfVersion, string vfwStrHourOfVersion, string vfwValueSpecialCharacteristic, string vfwCountry, string vfwCMTO, bool vfwUsePASOnLine);        
        bool ActualizarLoteDeInventario(int valConsecutivoCompania, string valNumeroDocumento, bool valEsInsertar);

    }
}
