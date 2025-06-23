using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieBooking.Entity
{
    using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

    [Index(nameof(MovieId), nameof(UserId), nameof(ShowTimeId), IsUnique = true)]
    public class Booking
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; private set; }

        [Required]
        private int MovieId { get; set; }

        public required Movie Movie { get; init; }

        [Required]
        private int UserId { get; set; }

        public required User User { get; init; }

        [Required]
        private int ShowTimeId { get; set; }

        public required ShowTime ShowTime { get; set; }
    }
}
