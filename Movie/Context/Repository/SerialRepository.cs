using Movie.Interface;
using Movie.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie.Context.Repository
{
    public class SerialRepository : IRepository<Serial>
    {
        private readonly MovieContext _movieContext;
        public SerialRepository(MovieContext mc) => _movieContext = mc;

        public void Add(Serial obj)
        {
            _movieContext.Serials.Add(obj);
            _movieContext.SaveChanges();
        }

        public void Delete(Serial obj)
        {
            _movieContext.Serials.Remove(obj);
            _movieContext.SaveChanges();
        }

        public void Delete(int id)
        {
            _movieContext.Serials.Remove(_movieContext.Serials.First(x => x.Id == id));
            _movieContext.SaveChanges();
        }

        public List<Serial> GetAll() => _movieContext.Serials.ToList();

        public Serial GetId(int id) => _movieContext.Serials.First(x => x.Id == id);

        public void Update(int id, Serial newObj)
        {
            var tf = _movieContext.Serials.First(x => x.Id == id);

            tf.Name = newObj.Name;
            tf.Evaluation = newObj.Evaluation;
            tf.Description = newObj.Description;
            tf.PosterPath = newObj.PosterPath;


            tf.Season = newObj.Season;
            tf.Episode = newObj.Episode;
            tf.Completed = newObj.Completed;

            _movieContext.SaveChanges();
        }
    }
}
