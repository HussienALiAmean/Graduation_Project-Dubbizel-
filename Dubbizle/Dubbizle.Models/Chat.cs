using System.ComponentModel.DataAnnotations.Schema;

namespace Dubbizle.Models

{
    public class Chat : BaseModel
    {
        public virtual ApplicationUser Sender { get; set; }
        [ForeignKey("Sender")]

        public string SenderID { get; set; }

        public virtual ApplicationUser Reciver { get; set; }
        [ForeignKey("Reciver")]

        public string ReciverID { get; set; }
        public string Content { get; set; }
        [ForeignKey("Room")]
        public int RoomId { get; set; }
        public virtual Room Room { get; set; }
        public string? File { get; set; }
        public DateTime Date { get; set; }
    }
}
