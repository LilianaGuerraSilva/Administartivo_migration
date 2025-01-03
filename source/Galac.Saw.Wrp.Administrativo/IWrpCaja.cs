using LibGalac.Aos.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
#if IsExeBsF
namespace Galac.SawBsF.Wrp.Venta {
#else
namespace Galac.Saw.Wrp.Venta {
#endif
    public interface IWrpCaja {
        void Execute(string vfwAction,string vfwCurrentCompany,string vfwCurrentParameters);
        string Choose(string vfwParamInitializationList,string vfwParamFixedList);
        void InitializeComponent(string vfwLogin,string vfwPassword,string vfwPath);
        void InitializeDefProg(string vfwProgramInitials,string vfwProgramVersion,string vfwDbVersion,string vfwStrDateOfVersion,string vfwStrHourOfVersion,string vfwValueSpecialCharacteristic,string vfwCountry,string vfwCMTO,bool vfwUsePASOnLine);
        void InitializeContext(string vfwInfo);
        bool InsertarCajaPorDefecto(int vfwConsecutivoCompania);
        bool AbrirGaveta(int valConsecutivoCompania,int valConsecutivoCaja);
        bool ActualizaUltimoNumComprobante(int valConsecutivoCompania,int valConsecutivoCaja,string valNumero,bool valEsNotaDeCredito);
        bool FindBySearchValues(int valConsecutivoCompania, int valConsecutivo,string valSqlWhere,ref string refXElement);
        string ValidateImpresoraFiscal(string vfwCurrentParameters);
    }
}
