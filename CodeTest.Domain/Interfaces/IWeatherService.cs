using CodeTest.Domain.Models;
using System.Threading.Tasks;

namespace CodeTest.Domain.Interfaces
{
    public interface IWeatherService
    {
        Task<Weather> Get(string longitude, string latitude, string timestamp);
    }
}
