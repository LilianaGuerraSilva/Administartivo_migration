using System.Xml.Linq;

namespace Galac.Saw.Lib {
    public interface ILookupDataService {
        XElement GetDataPageByCode(string valCodeFilter,int companyCode,int valPage);
        XElement GetDataPageByDescription(string valDescriptionFilter, int companyCode, int valPage);
    }
}
