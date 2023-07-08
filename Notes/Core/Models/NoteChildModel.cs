namespace Core.Models
{
    public class NoteChildModel
    {
        public int id { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public DateTime createdDate { get; set; }
        public int? noteId { get; set; }
        public int baseId { get; set; }
    }
}
