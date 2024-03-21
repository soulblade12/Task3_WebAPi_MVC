

using MyRESTServices.Domain.Models;

namespace MyRESTServices.Data.Interfaces
{
    public interface IRoleData : ICrud<Role>
    {
        Task<Task> AddUserToRole(string username, int roleId);
    }
}
