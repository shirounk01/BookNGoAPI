using BookNGoAPI.Models;

namespace BookNGoAPI.Repositories.Interfaces
{
    public interface IFlightRepository:IRepositoryBase<Flight>
    {
        List<Flight> FindByFilter(Filter filter);
        List<Flight> FindByModel(Flight flight);
    }
}
