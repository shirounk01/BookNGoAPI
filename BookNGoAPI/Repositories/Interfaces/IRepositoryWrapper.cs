namespace BookNGoAPI.Repositories.Interfaces
{
    public interface IRepositoryWrapper
    {
        IUserRepository UserRepository { get; }
        IFlightRepository FlightRepository { get; }

        IBookFlightRepository BookFlightRepository { get; }
        IHotelRepository HotelRepository { get; }
        IBookHotelRepository BookHotelRepository { get; }
        IReviewRepository ReviewRepository { get; }
        IUserRoleRepository UserRoleRepository { get; }
        IRoleRepository RoleRepository { get; }

        void Save();
    }
}
