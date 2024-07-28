using System.Reflection;

namespace Eshop.RazorPage.Models.Users;

public class UserDto:BaseDto
{
    public string Name { get; set; }

    public string Family { get; set; }

    public string AvatarName { get; set; }

    public string PhoneNumber { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public Gender Gender { get; set; }

    public bool IsActive { get; set; }

    public List<UserRoleDto> UserRoles { get; set; } = new();
}

public class UserRoleDto:BaseDto
{
    public long RoleId { get; set; }

    public string RoleTitle { get; set; }

}
public enum Gender
{
    None,
    Male,
    Female
}

public class UserFilterResult:BaseFilter<UserFilterData,UserFilterParams>
{

}

public class UserFilterData:BaseDto
{
    public string Name { get; set; }

    public string Family { get; set; }

    public string AvatarName { get; set; }

    public string PhoneNumber { get; set; }

    public string? Email { get; set; }

    public Gender Gender { get; set; }
}

public class UserFilterParams:BaseFilterParam
{
    public long? Id { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Email { get; set; }

}