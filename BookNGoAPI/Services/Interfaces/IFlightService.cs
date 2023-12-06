using BookNGoAPI.Models;

namespace BookNGoAPI.Services.Interfaces
{
    public interface IFlightService
    {
        List<Flight> GetFlightsByFilter(Filter filter);
        List<Flight> GetFlightsByModel(Flight flight);
        Flight SwapRoute(Flight flight);
        bool CheckTime(Flight flight, Time time);
        Flight GetFlightById(int id);
        void AddFlights(Flight flight);
        List<Tuple<BookFlight, Flight>> GetHotelsByReservations(List<BookFlight> bookedFlights);
    }
}
