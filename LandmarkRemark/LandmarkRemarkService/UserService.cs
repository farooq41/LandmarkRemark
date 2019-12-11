using LandmarkRemarkData;
using LandmarkRemarkModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LandmarkRemarkService
{
    public class UserService : IUserService
    {
        private Context _context;
        private const string _duplicateUserName = "User name already exists. Please choose a different one.";
        private readonly AppSettings _appSettings;

        public UserService(Context context, IOptions<AppSettings> appSettings)
        {
            _context = context;
            _appSettings = appSettings.Value;
        }

        //creates user based on registered info.
        public async Task<string> CreateUser(User user)
        {
            if (_context.Users.Any(u => u.Username == user.Username))
            {
                return _duplicateUserName; // Username must be unique.
            }

            var result = _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return string.Empty;
        }

        //checks if user exists and creates a JWT token for valid user
        public async Task<User> GetUser(string userName, string password)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Username == userName && u.Password == password);

            if (user == null)
                return null;

            //generate JWT token that is valid for 60 minutes. This token has to be sent in every request.
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(60),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            // remove password before returning
            user.Password = null;

            return user;
        }
    }
}
