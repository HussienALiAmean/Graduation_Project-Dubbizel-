namespace Dubbizle.Models

{
    public class BaseModel
    {
        public int ID { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public bool Deleted { get; set; }
    }
}
