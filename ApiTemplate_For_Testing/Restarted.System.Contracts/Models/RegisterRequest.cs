namespace Restarted.System.Contracts.Administration
{
    public record RegisterRequest(
     string Name,
     string Email,
     string Password);
}