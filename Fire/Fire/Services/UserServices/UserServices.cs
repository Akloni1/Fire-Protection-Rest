using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fire.Cryptography;
using Fire.Data;
using Fire.Models;
using Fire.ViewModels.User;

namespace Fire.Services.UserServices
{
    public class UserServices : IUserServices
    {
        private readonly PwdHash _pwdHash;
        private readonly eeContext _context;
        private readonly IMapper _mapper;

        public UserServices(IMapper mapper, eeContext context)
        {
            _pwdHash = new PwdHash();
            _context = context;
            _mapper = mapper;
        }
        public async Task<UserViewModels> AddUser(InputUserViewModels inputModel)
        {
            
            
            User userModel = _mapper.Map<User>(inputModel);
            Person personModel = _mapper.Map<Person>(inputModel);
            if (!LoginExists(personModel.Login))
            {
                personModel.Role = "user";
                personModel.Password = _pwdHash.sha256encrypt(inputModel.Password, inputModel.Login);

                var person = _context.Add(personModel).Entity;
                _context.SaveChanges();
                 userModel.IdUser = person.IdPerson;
               // userModel.IdUserNavigation= person.IdPerson;
                var user = _context.Add(userModel).Entity;
                 await _context.SaveChangesAsync();
                 var userViewModels = _mapper.Map<UserViewModels>(user);
                 userViewModels.Login = person.Login;
                 userViewModels.Role = person.Role;
                 return userViewModels;
              //  return new UserViewModels();
            }
            else
            {
                return null;
            }
        }

        public async Task<UserViewModels> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            var person = await _context.People.FindAsync(id);
            if (user == null) return null;
            _context.Users.Remove(user);
            _context.People.Remove(person);
            await _context.SaveChangesAsync();
            var userViewModels = _mapper.Map<UserViewModels>(user);
            userViewModels.Login = person.Login;
            userViewModels.Role = person.Role;
            return userViewModels;
        }

        public async Task<dynamic> GetAllUsers()
        {
            var users = await _context.Users.Join(_context.People,
                u => u.IdUser,
                p => p.IdPerson,
                (u, p) => new { u.IdUser, u.FirstName, u.LastName, u.MiddleName, u.DateOfBirth, p.Login, p.Role }).ToListAsync();

            return users;
        }

        public async Task<dynamic> GetUser(int id)
        {

            var user = await _context.Users.Join(_context.People,
               u => u.IdUser,
               p => p.IdPerson,
               (u, p) => new { u.IdUser, u.FirstName, u.LastName, u.MiddleName, u.DateOfBirth, p.Login, p.Role })
               .FirstOrDefaultAsync(m => m.IdUser == id);


            return user;
        }

        public async Task<dynamic> GetUserByLogin(string login)
        {


            var user = await _context.Users.Join(_context.People,
               u => u.IdUser,
               p => p.IdPerson,
               (u, p) => new { u.FirstName, u.LastName, u.MiddleName, u.DateOfBirth, p.Login, p.Role })
               .FirstOrDefaultAsync(p=>p.Login == login);


            return user;
        }

        public async Task<EditUserViewModels> UpdateUser(int id, EditUserViewModels userModel)
        {
            try
            {

                User user = _mapper.Map<User>(userModel);
                user.IdUser = id;
                _context.Update(user);
                await _context.SaveChangesAsync();
                return _mapper.Map<EditUserViewModels>(user);
            }
            catch (DbUpdateException)
            {
                if (!await UserExists(id))
                {
                    return null;
                }
                else
                {
                    return null;
                }
            }
        }


        private async Task<bool> UserExists(int id)
        {
            return await _context.Users.AnyAsync(e => e.IdUser == id);
        }
        private bool LoginExists(string login)
        {
            var person = _context.People.Any(e => e.Login == login);

            if (person)
            {
                return person;
            }
            else
            {
                return false;
            }

        }
    }
}
