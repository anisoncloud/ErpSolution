using System.ComponentModel.DataAnnotations;

namespace Erp.Web.Models
{
    public class AccountViewModels
    {
        [Required] public string FullName { get; set; } = string.Empty;

        [Required, EmailAddress] public string Email { get; set; } = string.Empty;

        [Required, DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [DataType(DataType.Password), Compare("Password")]
        public string ConfirmPassword { get; set; } = string.Empty;

        public string? Department { get; set; }
    }
    public class RegisterViewModel
    {
        [Required] public string FullName { get; set; } = string.Empty;

        [Required, EmailAddress] public string Email { get; set; } = string.Empty;

        [Required, DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [DataType(DataType.Password), Compare("Password")]
        public string ConfirmPassword { get; set; } = string.Empty;

        public string? Department { get; set; }
    }

    public class LoginViewModel
    {
        [Required, EmailAddress] public string Email { get; set; } = string.Empty;

        [Required, DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        public bool RememberMe { get; set; }
    }
    public class CreateRoleViewModel
    {
        [Required] public string RoleName { get; set; } = string.Empty;
        public string? Description { get; set; }
    }

    public class AssignRoleViewModel
    {
        public Guid UserId { get; set; }
        public string? UserEmail { get; set; }
        public List<string> AllRoles { get; set; } = new();
        public List<string> SelectedRoles { get; set; } = new();
    }
}
