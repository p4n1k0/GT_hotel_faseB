using TrybeHotel.Models;
using TrybeHotel.Dto;

namespace TrybeHotel.Repository
{
    public class UserRepository : IUserRepository
    {
        protected readonly ITrybeHotelContext _context;
        public UserRepository(ITrybeHotelContext context)
        {
            _context = context;
        }
        public UserDto GetUserById(int userId)
        {
            throw new NotImplementedException();
        }

        public UserDto Login(LoginDto login)
        {
            var user = _context.Users
            .SingleOrDefault(u => u.Email == login.Email && u.Password == login.Password);

            return user == null
                ? throw new UnauthorizedAccessException("Incorrect e-mail or password")
                : new UserDto
            {
                UserId = user.UserId,
                Name = user.Name,
                Email = user.Email,
                UserType = user.UserType
            };
        }
        public UserDto Add(UserDtoInsert user)
        {
            if (!_context.Users.Any(u => u.Email == user.Email))
            {
                var newUser = new User
                {
                    Name = user.Name,
                    Password = user.Password,
                    Email = user.Email,
                    UserType = "client"
                };
                _context.Users.Add(newUser);
                _context.SaveChanges();
                return new UserDto
                {
                    UserId = newUser.UserId,
                    Name = newUser.Name,
                    Email = newUser.Email,
                    UserType = newUser.UserType,
                };
            }
            else
            {
                throw new InvalidOperationException("User email already exists");
            }
        }

        public UserDto GetUserByEmail(string userEmail)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserDto> GetUsers()
        {
            throw new NotImplementedException();
        }

    }
}
