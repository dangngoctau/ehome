using System.Data;
using System.Threading.Tasks;

namespace EHome.Storage
{
    public interface IDbConnectionSelection
    {
        Task<IDbConnection> OpenDbConnectionAsync(string path = "EHome.sqlite");  
    }
}
