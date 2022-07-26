using Movie.Interface;
using Movie.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie.Context.Repository
{
    public class FilmRepository : IRepository<Film>
    {
        private readonly MovieContext _movieContext;
        public FilmRepository(MovieContext mc) => _movieContext = mc;

        public void Add(Film obj)
        {
            _movieContext.Films.Add(obj);
            _movieContext.SaveChanges();
        }

        public void Delete(Film obj)
        {
            _movieContext.Films.Remove(obj);
            _movieContext.SaveChanges();
        }

        public void Delete(int id)
        {
            _movieContext.Films.Remove(_movieContext.Films.First(x => x.Id == id));
            _movieContext.SaveChanges();
        }

        public List<Film> GetAll() => _movieContext.Films.ToList();

        public Film GetId(int id) => _movieContext.Films.First(x => x.Id == id);

        public void Update(int id, Film newObj)
        {
            var tf = _movieContext.Films.First(x => x.Id == id);

            tf.Name = newObj.Name;
            tf.Evaluation = newObj.Evaluation;
            tf.Description = newObj.Description;
            tf.PosterPath = newObj.PosterPath;

            _movieContext.SaveChanges();
        }
    }
}
