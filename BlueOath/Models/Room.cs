using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace BlueOath.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string? RoomType { get; set; }
        public string? Facilities { get; set; }
        public string? Description { get; set; }
        public decimal Rate { get; set; }
        public string? Status { get; set; }

        [NotMapped]
        [DisplayName("Upload File")]
        public IFormFile? RoomImage { get; set; }
    }
}