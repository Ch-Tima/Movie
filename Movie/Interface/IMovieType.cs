namespace Movie.Interfis
{
    public interface IMovieType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Evaluation { get; set; }
        public string Description { get; set; }
        public string PosterPath { get; set; }

    }
}
