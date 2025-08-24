using WebWorker.BLL.Dtos.User;
using WebWorker.DAL.Repositories.User;

namespace WebWorker.BLL.Services.User
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public ServiceResponse GetAll()
        {
            var users = _userRepository.GetAll();

            var result = users.Select(user => new UserDto
            {
                Id = user.Id,
                FullName = $"{user.FirstName} {user.LastName}",
                Email = user.Email ?? "",
                Image = user.Image ?? null,
                Roles = user.UserRoles!.Select(role => role.Role!.Name ?? string.Empty).ToArray()
            }).ToList();

            return ServiceResponse.Success("Users retrieved successfully", result);
        }
    }
}
