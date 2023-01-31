
namespace Bookshop_Business.Repository;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _db;
    private readonly IMapper _mapper;

    public UserRepository(ApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<bool> ChangeUserActivationState(string userId)
    {
        try
        {
            var user = await _db.ApplicationUser.FindAsync(userId);
            if (user is null)
                return false;

            if (user.IsActive)
            {
                user.IsActive = false;
            }
            else
            {
                user.IsActive = true;
            }

            _db.ApplicationUser.Update(user);
            await _db.SaveChangesAsync();

            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public async Task<int> GetActiveUserCount()
    {
        try
        {
            var ActiveUser = await _db.ApplicationUser.Where(u => u.IsActive).ToListAsync();

            if (ActiveUser is null)
                return 0;

            return ActiveUser.Count;
        }
        catch (Exception e)
        {
            return 0;
        }
    }

    public async Task<Tuple<List<ApplicationUserDTO>, int>> GetAllUsers(int pageId, string Name)
    {
        try
        {
            IQueryable<ApplicationUser> users = _db.ApplicationUser;

            if (!string.IsNullOrWhiteSpace(Name))
            {
                users = users
                    .Where(u => u.UserName.Contains(Name.Trim().ToLower()) || u.FullName.Contains(Name.Trim().ToLower()));
            }

            var usersDTO = _mapper.Map<IQueryable<ApplicationUser>, List<ApplicationUserDTO>>(users);


            int take = 5;
            int skip = (pageId - 1) * take;
            var pageCount = usersDTO.Count() / take;
            if (pageCount % 2 != 0)
            {
                pageCount++;
            }
            var query = usersDTO.OrderByDescending(a => a.FullName).Skip(skip).Take(take).ToList();
            return Tuple.Create(query, pageCount);


        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public async Task<ApplicationUserDTO> GetUserByEmail(string email)
    {
        try
        {
            var user = await _db.ApplicationUser.SingleOrDefaultAsync(u => u.Email == email.Trim().ToLower());
            if (user is null)
                return null;

            var userDTO = _mapper.Map<ApplicationUser,ApplicationUserDTO>(user);

            return userDTO;


        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public async Task<ApplicationUserDTO> GetUserById(string userId)
    {
        try
        {
            var user = await _db.ApplicationUser.FindAsync(userId);
            if (user is null)
                return null;

            var userDTO = _mapper.Map<ApplicationUser, ApplicationUserDTO>(user);
            return userDTO;

        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public string getUserRoleByUserId(string userId)
    {
        try
        {
            var user = _db.ApplicationUser.Find(userId);
            if (user is null)
                return null;

            var userRole = _db.UserRoles.SingleOrDefault(u => u.UserId == userId);
            if (userRole is null)
                return null;

            var role = _db.Roles.SingleOrDefault(r => r.Id == userRole.RoleId);

            if (role is null)
                return null;

            return role.Name;

        }
        catch (Exception ex)
        {
            return null;
        }
    }
}
