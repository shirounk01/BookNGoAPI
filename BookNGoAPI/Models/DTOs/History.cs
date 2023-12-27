namespace BookNGoAPI.Models.DTOs
{
	public class History
	{
		public List<Tuple<BookFlight, Flight>>? Flights { get; set; }
		public List<Tuple<BookHotel, Hotel>>? Hotels { get; set; }
	}
}
