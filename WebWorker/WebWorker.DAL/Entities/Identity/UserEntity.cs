﻿using Microsoft.AspNetCore.Identity;

namespace WebWorker.Data.Entities.Identity
{
    public class UserEntity : IdentityUser<long>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Image { get; set; }
        public DateTime DataCreated { get; set; } = DateTime.UtcNow;

        public virtual ICollection<UserRoleEntity>? UserRoles { get; set; }
    }
}
