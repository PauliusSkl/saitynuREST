namespace RestProject.Data.Entities
{
    public class Topic
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime? ExpiresIn { get; set; }



    }
}
