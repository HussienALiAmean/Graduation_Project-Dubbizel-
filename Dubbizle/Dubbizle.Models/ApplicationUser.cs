using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Data;

namespace Dubbizle.Models

{
    public class ApplicationUser:IdentityUser
    {
       
        public string? Gender { get; set; }
        public string? AboutMe { get; set; }
        public DateTime? BirthDate { get; set; }
        public float? Rating { get; set; }
        public string? Location { get; set; }   

        public List<ApplicationUser_Package> ApplicationUser_PackagesList { get; set; }
        public List<Favorite> FavoritesList { get; set; }
        [InverseProperty("Sender")]
        public List<Chat> SenderChatList { get; set; }
        [InverseProperty("Reciver")]
       
        public List<Chat> ReciverChatList { get; set; }
        public bool? Deleted { get; set; }    

    }
}
