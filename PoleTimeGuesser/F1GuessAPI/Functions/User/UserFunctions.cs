using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace F1GuessAPI.Functions.User
{
    public class UserFunctions : IUserFunctions
    {
        F1GuessContext _context;

        public UserFunctions(F1GuessContext context)
        {
            _context = context;
        }

        public async Task<UserModel> Registration(string username, string email, string password)
        {
            byte[] salt = GenerateSalt();

            var entity = new TblUser
            {
                Username = username,
                Email = email,
                Password = GeneratePawword(password, salt),
                StoredSalt = salt,
            };

            _context.TblUsers.Add(entity);
            var response = await _context.SaveChangesAsync();

            var result = new UserModel
            {
                Username = username
            };

            return result;
        }

        public UserModel? Authenticate(string username, string password)
        {
            try
            {
                var entity = _context.TblUsers.SingleOrDefault(x => x.Username == username);
                if (entity is null) return null;

                bool isPasswordMatched = VertifyPassword(password, entity.StoredSalt, entity.Password);

                if (!isPasswordMatched) return null;

                //var token = GenerateJwtToken(entity);

                return new UserModel
                {
                    Id = entity.Id,
                    Username = entity.Username,
                    Email = entity.Email,
                    AvatarSourceName = entity.AvatarSourceName,
                };
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<UserModel> Update(int id, string username, string email, string password)
        {
            try
            {
                var entity = _context.TblUsers.SingleOrDefault(x => x.Id == id);
                if (entity is null) return null;

                if (username.Trim() != "")
                    entity.Username = username;
                if (email.Trim() != "")
                    entity.Email = email;
                if (password.Trim() != "")
                {
                    byte[] salt = GenerateSalt();
                    entity.StoredSalt = salt;
                    entity.Password = GeneratePawword(password, salt);
                }

                _context.TblUsers.Update(entity);
                var response = await _context.SaveChangesAsync();

                UserModel result = new UserModel
                {
                    Id = entity.Id,
                    Username = entity.Username,
                    Email = entity.Email,
                    AvatarSourceName = entity.AvatarSourceName,
                };

                return result;
            }
            catch (Exception ex)
            {
                return null;
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
    }
}
