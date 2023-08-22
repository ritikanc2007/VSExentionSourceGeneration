
namespace Restarted.System.Contracts.Administration
{
    public record AuthenticationResponse(
    Guid Id,
    string Name,
    string Email,
    string Token);
    //ICollection<RoleDTO> Roles);
}