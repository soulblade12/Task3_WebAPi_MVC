using Microsoft.EntityFrameworkCore;
using MyRESTServices.Data.Interfaces;
using MyRESTServices.Domain.Models;

namespace MyRESTServices.Data
{
    public class UserData : IUserData
    {
        private readonly AppDbContext _context;

        public UserData(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }
        public Task<Task> ChangePassword(string username, string newPassword)
        {
            throw new NotImplementedException();
        }

        public Task<int> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            var userALL = await _context.Users.ToListAsync();
            return userALL;
        }

        public async Task<IEnumerable<User>> GetAllWithRoles()
        {
            var userALLrole = await _context.Users.Include(u => u.Roles).ToListAsync();
            return userALLrole;
        }

        public Task<User> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetByUsername(string username)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetUserWithRoles(string username)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Username == username);
            var roles = await _context.Users
    .Where(ur => ur.Username == user.Username)
    .Select(ur => ur.Roles)
    .ToListAsync();
            user.Roles = (ICollection<Role>)roles;
            return user;

        }

        public async Task<User> Insert(User entity)
        {
            try
            {
                await _context.Users.AddAsync(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {

                throw new ArgumentException($"{ex.Message}");
            }
        }

        public async Task<User> Login(string username, string password)
        {
            var login = await _context.Users.Include(u => u.Roles)
                .Where(u => u.Username == username && u.Password == password).SingleOrDefaultAsync();
            return login;

        }

        public Task<User> Update(int id, User entity)
        {
            throw new NotImplementedException();
        }
    }
}
