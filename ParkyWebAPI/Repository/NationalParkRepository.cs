using ParkyWebAPI.Models;
using ParkyWebAPI.Repository.IRepository;

namespace ParkyWebAPI.Repository
{
    public class NationalParkRepository : INationalParkRepository
    {
        public readonly ApplicationDbContext _db;
        public NationalParkRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public bool CreateNationalPark(NationalPark park)
        {
            _db.NationalPark.Add(park);
            return Save();
        }

        public bool DeleteNationalPark(NationalPark park)
        {
            _db.NationalPark.Remove(park);
            return Save();
        }

        public NationalPark GetNationalPark(int id)
        {
            return _db.NationalPark.FirstOrDefault(a => a.Id == id);
        }

        public ICollection<NationalPark> GetNationalParks()
        {
            //return _db.NationalPark.OrderBy(a=>a.Name).ToList();
            return _db.NationalPark.ToList();

        }

        public bool NationalParkExists(string name)
        {
            bool value = _db.NationalPark.Any(a => a.Name.ToLower().Trim() == name.ToLower().Trim());
            return value;
        }

        public bool NationalParkExists(int id)
        {
            bool value = (_db.NationalPark.Any(a => a.Id == id));
            return value;
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateNationalPark(NationalPark park)
        {
            _db.NationalPark.Update(park);
            return Save();
        }
    }
}
