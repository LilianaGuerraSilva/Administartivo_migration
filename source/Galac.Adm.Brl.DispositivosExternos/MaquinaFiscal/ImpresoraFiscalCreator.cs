using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using Galac.Adm.Ccl.DispositivosExternos;

namespace Galac.Adm.Brl.DispositivosExternos.MaquinaFiscal {
    public class ImpresoraFiscalCreator {

        public virtual IMaquinaFiscalPdn Crear(XElement valXmlMaquinaFiscal) {
            eImpresoraFiscal vModelo = (eImpresoraFiscal)LibConvert.DbValueToEnum(LibXml.GetPropertyString(valXmlMaquinaFiscal, "ModeloDeMaquinaFiscal"));            
            switch (vModelo) {
                case eImpresoraFiscal.BEMATECH_MP_20_FI_II:
                case eImpresoraFiscal.BEMATECH_MP_2100_FI:
                case eImpresoraFiscal.BEMATECH_MP_4000_TH:
                    return new clsBematech(valXmlMaquinaFiscal);
                case eImpresoraFiscal.ELEPOSVMAX:
                    return new clsEleposVMAX(valXmlMaquinaFiscal);
                case eImpresoraFiscal.ACLASPP1F3:
                case eImpresoraFiscal.ACLASPP9:
                case eImpresoraFiscal.BIXOLON270:
                case eImpresoraFiscal.BIXOLON280:
                case eImpresoraFiscal.BIXOLON350:
                case eImpresoraFiscal.BIXOLON812:
                case eImpresoraFiscal.DASCOMTALLY1125:
                case eImpresoraFiscal.DASCOMTALLYDT230:
                case eImpresoraFiscal.HKA80:
                case eImpresoraFiscal.OKIML1120:
                    return new clsTheFactory(valXmlMaquinaFiscal);
                default:
                    throw new GalacException("Modelo de impresora aun no implementado", eExceptionManagementType.Controlled);
            }
        }
    }
}
