using System.Threading.Tasks;

namespace SMeat.API.Helpers
{
    public interface IDataBaseInitializer
    {
        Task Initialize();
    }
}