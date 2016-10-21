using System.Threading.Tasks;
using System.Xml;

namespace TeamCityCompanion
{
    public interface ICommunicator
    {
        Task<XmlReader> Get(string endpoint, string query);
    }
}
