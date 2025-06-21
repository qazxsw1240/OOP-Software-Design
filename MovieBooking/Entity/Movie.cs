using System;
using System.ComponentModel.DataAnnotations;

using Microsoft.EntityFrameworkCore;

namespace MovieBooking.Entity
{
    [Index(nameof(Title), nameof(Director), IsUnique = true)]
    public class Movie
    {
        [Key]
        public int Id { get; private set; }

        public string Title { get; set; } = "N/A";

        public string Director { get; set; } = "N/A";

        [Required]
        public TimeSpan RunningTime { get; set; }

        [Required]
        public DateOnly ReleaseDate { get; set; }
    }
}
