namespace Movie.Models
{
    public class Serial
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int Season { get; set; }
        public int Episode { get; set; }
        public bool Completed{ get; set; }

        public float Evaluation { get; set; }
        public string Description { get; set; }
        public string PosterPath { get; set; }

    }
}
