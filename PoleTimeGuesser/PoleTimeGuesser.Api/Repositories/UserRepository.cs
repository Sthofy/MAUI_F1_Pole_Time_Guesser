using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace PoleTimeGuesser.Api.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ISqlDataAccess _sql;
        private readonly IConfiguration _config;
        private readonly string cnnString = "F1GuessDB";

        public UserRepository(ISqlDataAccess sql, IConfiguration config)
        {
            _sql = sql;
            _config = config;
        }

        public async Task<RegistrationModel> Registration(string username, string email, string password)
        {
            byte[] salt = GenerateSalt();

            var user = new UserModel
            {
                Username = username,
                Email = email,
                Password = GeneratePawword(password, salt),
                StoredSalt = salt,
                AvatarSourceName = "default_avatar.png",
            };

            var data = await _sql.SaveUser("dbo.spUser_Insert", user, cnnString);

            var output = new RegistrationModel
            {
                Id = Convert.ToInt32(data),
            };

            return output;
        }

        public async Task<LoggedInUserModel> Authenticate(string username, string password)
        {
            var response = await _sql.LoadData<UserModel, dynamic>("dbo.spUser_Lookup", new { username }, cnnString);
            var user = response.FirstOrDefault();
            if (user is null) return null;

            bool isPasswordMatched = VertifyPassword(password, user.StoredSalt, user.Password);

            if (!isPasswordMatched) return null;

            var token = GenerateJwtToken(user);

            var output = new LoggedInUserModel
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Token = token,
                AvatarSourceName = user.AvatarSourceName,
            };

            return output;
        }

        public async Task<bool> Update(int id, string username, string email, string password)
        {

            var response = await _sql.LoadData<UserModel, dynamic>("dbo.spUser_GetById", new { id }, cnnString);
            var user = response.FirstOrDefault();

            if (user is null) return false;

            if (username.Trim() != "")
                user.Username = username;
            if (email.Trim() != "")
                user.Email = email;
            if (password.Trim() != "")
            {
                byte[] salt = GenerateSalt();
                user.StoredSalt = salt;
                user.Password = GeneratePawword(password, salt);
            }

            await _sql.SaveData("dbo.spUser_Update", user, cnnString);

            return true;
        }

        public async Task<IEnumerable<ScoreboardModel>> GetScoreboard()
        {
            var data = await _sql.LoadData<ScoreboardModel, dynamic>("dbo.spUsersScoreboard_GetAll", new { }, cnnString);

            return data;
        }

        public async Task<bool> GetByUsername(string username)
        {
            var user = await _sql.LoadData<LoggedInUserModel, dynamic>("dbo.spUsers_GetByUsername", new { username }, cnnString);

            if (user.FirstOrDefault() is not null)
                return true;
            else
                return false;
        }

        private string GeneratePawword(string enteredPassword, byte[] enteredSalt)
        {
            string output = Convert.ToBase64String(
                KeyDerivation.Pbkdf2(
                    password: enteredPassword,
                    salt: enteredSalt,
                    prf: KeyDerivationPrf.HMACSHA512,
                    iterationCount: 10000,
                    numBytesRequested: 128));

            return output;
        }

        private byte[] GenerateSalt()
        {
            byte[] salt = new byte[32];

            RandomNumberGenerator.Fill(salt);

            return salt;
        }

        private bool VertifyPassword(string enteredPassword, byte[] storedSalt, string storedPassword)
        {
            string encryptedPassword = GeneratePawword(enteredPassword, storedSalt);

            return encryptedPassword.Equals(storedPassword);
        }

        private string GenerateJwtToken(UserModel user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.Username),
                new Claim(ClaimTypes.Email,user.Email),
            };

            var token = new JwtSecurityToken
            (
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                notBefore: DateTime.UtcNow,
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"])),
                    SecurityAlgorithms.HmacSha256)
            );

            return tokenHandler.WriteToken(token);
        }
    }
}
