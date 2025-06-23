using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieBooking.Entity
{
    using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

    [Index(nameof(Title), nameof(Director), IsUnique = true)]
    public class Movie
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
