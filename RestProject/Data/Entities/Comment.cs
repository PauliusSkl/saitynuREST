namespace RestProject.Data.Entities
{
    public class Comment
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public DateTime CreationDate { get; set; }


        public Post Post { get; set; }
    }
}
