using System;

using MovieBooking.Entity;

namespace MovieBooking.Services
{
    public record BookingCreateRequest(User User, Movie Movie, TimeOnly ShowTime);
}
