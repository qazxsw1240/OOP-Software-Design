using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieBooking.Entity
{
    public class ShowTime
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; private set; }

        [Required]
        private int MovieId { get; set; }

        public required Movie Movie { get; init; }

        [Required]
        public required DateTime Time { get; set; }
    }
}
