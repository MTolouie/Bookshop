
namespace Bookshop_Business.Repository.IRepository;

public interface IUserRepository
{
    Task<Tuple<List<ApplicationUserDTO>, int>> GetAllUsers(int pageId, string Name);
    Task<ApplicationUserDTO> GetUserById(string userId);
    Task<ApplicationUserDTO> GetUserByEmail(string email);
    string getUserRoleByUserId(string userId);
    Task<bool> ChangeUserActivationState(string userId);
}