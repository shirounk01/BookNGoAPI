﻿using BookNGoAPI.Models;

namespace BookNGoAPI.Repositories.Interfaces
{
    public interface IHotelRepository : IRepositoryBase<Hotel>
    {
        List<Hotel> FindByModel(Hotel hotel);
    }
}
