using LibGalac.Aos.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#if IsExeBsF 
namespace Galac.SawBsF.Wrp.Venta {
#else
namespace Galac.Saw.Wrp.Venta {
#endif
    public interface IWrpCajaApertura {
        void Execute(string vfwAction,string vfwCurrentCompany,string vfwCurrentParameters);
        string Choose(string vfwParamInitializationList,string vfwParamFixedList);
        void InitializeComponent(string vfwLogin,string vfwPassword,string vfwPath);
        void InitializeDefProg(string vfwProgramInitials,string vfwProgramVersion,string vfwDbVersion,string vfwStrDateOfVersion,string vfwStrHourOfVersion,string vfwValueSpecialCharacteristic,string vfwCountry,string vfwCMTO,bool vfwUsePASOnLine);
        void InitializeContext(string vfwInfo);
        bool UsuarioFueAsignado(int valConsecutivoCompania,int valConsecutivoCaja,string valNombreDelUsuario,bool valCajaCerrada,bool valResumenDiario);
        bool CajasCerradas(int valConsecutivoCompania,int valConsecutivo,bool valCajaCerrada);
    }
}
