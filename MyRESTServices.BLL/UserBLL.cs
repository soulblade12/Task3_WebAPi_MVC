using AutoMapper;
using MyRESTServices.BLL.DTOs;
using MyRESTServices.BLL.Interfaces;
using MyRESTServices.Data.Interfaces;
using MyRESTServices.Domain.Models;

namespace MyRESTServices.BLL
{
    public class UserBLL : IUserBLL
    {

        private readonly IUserData _userData;
        private readonly IMapper _mapper;

        public UserBLL(IUserData userData, IMapper mapper)
        {
            _userData = userData;
            _mapper = mapper;
        }
        public Task<Task> ChangePassword(string username, string newPassword)
        {
            throw new NotImplementedException();
        }

        public Task<Task> Delete(string username)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<UserDTO>> GetAll()
        {
            var userALL = await _userData.GetAll();
            var userDTO = _mapper.Map<IEnumerable<UserDTO>>(userALL);
            return userDTO;
        }

        public async Task<IEnumerable<UserDTO>> GetAllWithRoles()
        {
            var userALL = await _userData.GetAllWithRoles();
            var userDTO = _mapper.Map<IEnumerable<UserDTO>>(userALL);
            return userDTO;
        }

        public Task<UserDTO> GetByUsername(string username)
        {
            throw new NotImplementedException();
        }

        public async Task<UserDTO> GetUserWithRoles(string username)
        {
            var loginRoles = await _userData.GetUserWithRoles(username);
            var loginMAP = _mapper.Map<UserDTO>(loginRoles);
            return loginMAP;
        }
        public async Task<Task> Insert(UserCreateDTO entity)
        {
            var Password = Helper.GetHash(entity.Password);
            entity.Password = Password;
            var map = _mapper.Map<User>(entity);
            var add = await _userData.Insert(map);
            return Task.FromResult(add);
        }


        public async Task<UserDTO> Login(string username, string password)
        {
            throw new NotImplementedException();

        }

        public async Task<UserDTO> LoginMVC(LoginDTO loginDTO)
        {
            var Password = Helper.GetHash(loginDTO.Password);
            var loginuser = await _userData.Login(loginDTO.Username, Password);
            var loginMAP = _mapper.Map<UserDTO>(loginuser);
            return loginMAP;
        }
    }
}
