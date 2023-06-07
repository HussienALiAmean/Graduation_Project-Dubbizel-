using System.ComponentModel.DataAnnotations.Schema;

namespace Dubbizle.Models

{
    public class Chat : BaseModel
    {
        public ApplicationUser Sender { get; set; }
        [ForeignKey("Sender")]

        public string SenderID { get; set; }

        public ApplicationUser Reciver { get; set; }
        [ForeignKey("Reciver")]

        public string ReciverID { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
    }
}
