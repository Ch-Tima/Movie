using Movie.Interfis;

namespace Movie.Models
{
    public class Film : IMovieType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Evaluation { get; set; }
        public string Description { get; set; }
        public string PosterPath { get; set; }   
    }
}
