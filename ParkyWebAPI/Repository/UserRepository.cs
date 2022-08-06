using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ParkyWebAPI.Models;
using ParkyWebAPI.Repository.IRepository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ParkyWebAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly AppSettings _appSettings;
        public UserRepository(ApplicationDbContext db, IOptions<AppSettings> appSettings)
        {
            _db = db;
            _appSettings = appSettings.Value;

        }
        public User addUser(string username, string password)
        {
            User user = new User()
            {
                password = password,
                userName = username,
                role = "Admin"
            };            
            _db.Users.Add(user);
            _db.SaveChanges();
            user.password = "";
            user.role = "Admin";
            return user;

        }

        public User authenticate(string username, string password)
        {
            var user = _db.Users.FirstOrDefault(x => x.userName == username && x.password == password);
            if (user == null)
            {
                return null;
            }
            else
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, user.id.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                user.userName = username;
                user.token = tokenHandler.WriteToken(token);
                return user;

            }
        }

        public bool userExist(string username)
        {
            var user = _db.Users.FirstOrDefault(x => x.userName == username);
            if (user == null)
                return false;

            return true;
        }

        public List<User> getUsers()
        {
            var users = new List<User>();
            users = _db.Users.ToList();
            if (users!=null && users.Count > 0)
            {
                return users;
            }
            else
            {
                return null;
            }
        }

        public User getUser(int id)
        {
            var user = new User();
            user = _db.Users.FirstOrDefault(x=>x.id == id);
            if (user != null)
            {
                return user;
            }
            else
            {
                return null;
            }
        }
    }
}
