namespace RestProject.Data
{
    public class LinkDto
    {
        // URL
        public string Href { get; set; }

        //What it does
        public string Rel { get; set; }
        //GET , POST, PUT, DELETE
        public string Method { get; set; }

    }
}
