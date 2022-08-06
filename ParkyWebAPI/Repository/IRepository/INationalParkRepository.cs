using ParkyWebAPI.Models;

namespace ParkyWebAPI.Repository.IRepository
{
    public interface INationalParkRepository
    {
        ICollection<NationalPark> GetNationalParks();
        NationalPark GetNationalPark(int id);
        bool NationalParkExists(string name);
        bool NationalParkExists(int id);
        bool CreateNationalPark (NationalPark park);
        bool UpdateNationalPark(NationalPark park);
        bool DeleteNationalPark(NationalPark park);
        bool Save();



    }
}
