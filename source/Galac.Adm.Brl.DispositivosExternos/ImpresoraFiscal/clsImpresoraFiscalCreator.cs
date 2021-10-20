using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using Galac.Adm.Ccl.DispositivosExternos;

namespace Galac.Adm.Brl.DispositivosExternos.ImpresoraFiscal {
    public class clsImpresoraFiscalCreator {

        public virtual IImpresoraFiscalPdn Crear(XElement valXmlMaquinaFiscal) {
            eImpresoraFiscal vModelo = (eImpresoraFiscal)LibConvert.DbValueToEnum(LibXml.GetPropertyString(valXmlMaquinaFiscal,"ModeloDeMaquinaFiscal"));
            switch(vModelo) {
            case eImpresoraFiscal.BEMATECH_MP_20_FI_II:
            case eImpresoraFiscal.BEMATECH_MP_2100_FI:
            case eImpresoraFiscal.BEMATECH_MP_4000_FI:
                return new clsBematech(valXmlMaquinaFiscal);
            case eImpresoraFiscal.ELEPOSVMAX_220_F:
            case eImpresoraFiscal.ELEPOSVMAX_300:
            case eImpresoraFiscal.ELEPOSVMAX_580:
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
            case eImpresoraFiscal.HKA112:
            case eImpresoraFiscal.OKIML1120:
            case eImpresoraFiscal.DASCOMTALLY1140:
                return new clsTheFactory(valXmlMaquinaFiscal);
            case eImpresoraFiscal.EPSON_PF_220:
            case eImpresoraFiscal.EPSON_PF_220II:
            case eImpresoraFiscal.EPSON_PF_300:
            case eImpresoraFiscal.EPSON_TM_675_PF:
            case eImpresoraFiscal.EPSON_TM_950_PF:
                return new clsEpson(valXmlMaquinaFiscal);
            case eImpresoraFiscal.QPRINT_MF:
                return new clsQPrint(valXmlMaquinaFiscal);
            case eImpresoraFiscal.BMC_CAMEL:
            case eImpresoraFiscal.BMC_SPARK_614:
            case eImpresoraFiscal.BMC_TH34_EJ:
                return new clsBMC(valXmlMaquinaFiscal);
            default:
                throw new GalacException("Modelo de impresora aun no implementado",eExceptionManagementType.Controlled);
            }
        }
    }
}

