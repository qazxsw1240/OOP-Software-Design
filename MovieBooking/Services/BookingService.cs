using System;
using System.Collections.Generic;
using System.Linq;

using MovieBooking.Entity;
using MovieBooking.Repositories;

namespace MovieBooking.Services
{
    public class BookingService(
        IRepository<Booking> bookingRepository,
        IRepository<ShowTime> showTimeRepository) : IBookingService
    {
        public IQueryable<Booking> GetBookingsByUser(User user)
        {
            return bookingRepository.Entities.Where(b => b.User.Id == user.Id);
        }

        public IQueryable<Booking> GetUpcomingBookingsByUser(User user)
        {
            return bookingRepository.Entities.Where(b => b.User.Id == user.Id);
        }

        public Booking? GetBooking(User user, Movie movie)
        {
            return bookingRepository.Entities.SingleOrDefault(b => b.User.Id == user.Id && b.Movie.Id == movie.Id);
        }

        public bool CreateBookingByUser(BookingCreateRequest request)
        {
            IEnumerable<Booking> bookings = GetBookingsByUser(request.User);
            if (bookings.Any(b => b.ShowTime == request.ShowTime))
            {
                return false;
            }
            if (showTimeRepository.Entities
                .Where(showTime => showTime.Movie.Id == request.Movie.Id)
                .All(showTime => showTime.Time != request.ShowTime.Time))
            {
                return false;
            }
            DateTime dateTime = DateTime.UtcNow;
            if (request.ShowTime.Time > dateTime)
            {
                return false;
            }
            Booking booking = new()
            {
                User = request.User,
                Movie = request.Movie,
                ShowTime = request.ShowTime
            };
            return bookingRepository.Add(booking);
        }

        public bool UpdateBookingByUser(BookingCreateRequest request, ShowTime newShowTime)
        {
            Booking? booking = GetBooking(request.User, request.Movie);
            if (booking is null)
            {
                return false;
            }
            DateTime dateTime = DateTime.UtcNow;
            if (newShowTime.Time > dateTime)
            {
                return false;
            }
            booking.ShowTime = newShowTime;
            return bookingRepository.Update(booking);
        }
    }
}
