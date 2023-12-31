﻿using BookNGoAPI.Models;
using BookNGoAPI.Repositories.Interfaces;
using BookNGoAPI.Services.Interfaces;

namespace BookNGoAPI.Services
{
    public class BookFlightService : IBookFlightService
    {
        private readonly IRepositoryWrapper _repo;
        private readonly IFlightService _flightService;
        private readonly IUserService _userService;

        public BookFlightService(IRepositoryWrapper repo, IFlightService flightService, IUserService userService)
        {
            _repo = repo;
            _flightService = flightService;
            _userService = userService;
        }

        public void BookFlight(string userGuid, Flight flight)
        {
            BookFlight bookFlight = BuildNewBookFlight(userGuid, flight);

            _repo.FlightRepository.Update(flight);
            _repo.BookFlightRepository.Create(bookFlight);

        }

        private static BookFlight BuildNewBookFlight(string userGuid, Flight flight)
        {
            BookFlight bookFlight = new BookFlight();
            bookFlight.Flight = flight;
            bookFlight.UserGuid = userGuid;
            bookFlight.RegistrationDate = DateTime.Now;
            return bookFlight;
        }

        public void BookFlights(int goingId, int comingId)
        {
            var userGuid = _userService.GetGuid();
            Flight goingFlight = _flightService.GetFlightById(goingId);
            Flight comingFlight = _flightService.GetFlightById(comingId);
            BookFlight(userGuid, goingFlight);
            BookFlight(userGuid, comingFlight);

            _repo.Save();
        }

        public List<BookFlight> GetReservationsByUserId(string userGuid)
        {
            List<BookFlight> flightReservations = _repo.BookFlightRepository.FindByCondition(flight => flight.UserGuid == userGuid).ToList();
            return flightReservations;
        }

        public bool CheckContinuity(int goingId, int comingId)
        {
            var goingFlight = _flightService.GetFlightById(goingId);
            var comingFlight = _flightService.GetFlightById(comingId);
            return goingFlight.DepartureCity == comingFlight.ArrivalCity && comingFlight.DepartureCity == goingFlight.ArrivalCity && goingFlight.DepartureAirport == comingFlight.ArrivalAirport && comingFlight.DepartureAirport == goingFlight.ArrivalAirport;
        }
    }

}
