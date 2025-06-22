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

        [MaxLength(128)]
        public string Title { get; init; } = "N/A";

        [MaxLength(128)]
        public string Director { get; init; } = "N/A";

        [Required]
        public TimeSpan RunningTime { get; init; }

        [Required]
        public DateOnly ReleaseDate { get; init; }
    }
}
