using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace F1GuessAPI.DataAccess.User
{
    public class UserData : IUserData
    {
        private readonly string cnnStringLocal= "F1GuessLocal";
        private readonly string cnnString= "F1Guess";
        readonly ISqlDataAccess _sql;

        public UserData(ISqlDataAccess sql)
        {
            _sql = sql;
        }

        public bool Registration(string username, string email, string password)
        {
            try
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

                _sql.SaveData("dbo.spUser_Insert", user, cnnStringLocal);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public LoggedInUserModel? Authenticate(string username, string password)
        {
            try
            {
                var user = _sql.LoadData<UserModel, dynamic>("dbo.spUser_Lookup", new { username }, cnnStringLocal).FirstOrDefault();
                if (user is null) return null;

                bool isPasswordMatched = VertifyPassword(password, user.StoredSalt, user.Password);

                if (!isPasswordMatched) return null;

                var token = GenerateJwtToken(user);

                var output = new LoggedInUserModel
                {
                    Id= user.Id,
                    Username = user.Username,
                    Email = user.Email,
                    Token = token,
                    AvatarSourceName = user.AvatarSourceName,
                };

                return output;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool Update(int id, string username, string email, string password)
        {
            try
            {
                var user = _sql.LoadData<UserModel, dynamic>("dbo.spUser_Lookup", new { username }, cnnStringLocal).FirstOrDefault();
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

                _sql.SaveData("dbo.spUser_Update", user, cnnStringLocal);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
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
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();

            rng.GetBytes(salt);

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
            var key = Encoding.ASCII.GetBytes("1234567890123456");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
