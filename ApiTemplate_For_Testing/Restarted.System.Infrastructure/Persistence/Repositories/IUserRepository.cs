using Restarted.Generators;
using Restarted.Generators.Generators.Controllers.Attributes;
using Restarted.Generators.Generators.CQRS.Attributes;
using Restarted.System.Infrastructure.Persistence.DTO;
using Restarted.System.Infrastructure.Persistence.DTO.Common;

namespace Restarted.System.Infrastructure.Persistence.Repositories
{
    [GenerateService(templates: "CQRS.tt", DTOFieldNames: "tempId,Temp1")]
    [GeneratorCQRS(featureName:"Admin",featureModuleName:"Users",requestType:"Query")]
    [GeneratorApi(implementation : ImplementationType.CQRSWithRepository , controllerName: "UsersController", FeatureName = "Admin")]
    public interface IUserRepository 
    {

        //[GeneratorCQRS(requestType: "Command", featureName: "Authentication")]
        //[GenerateApiCQRS(action: "POST", route: "Register", controllerName: "AuthenticationController", cqrsRequestName: "RegisterCommand")]
        //Task<UserAuthDTO> Register(UserAuthDTO? entityDto);

        //[GeneratorCQRS(requestType: "Query", featureName: "Authentication")]
        //[GenerateApiCQRS(action: "GET", route: "UserName/{username}", methodName: "Login", controllerName: "AuthenticationController", cqrsRequestName: "GetUserByUserNameQuery")]
        //Task<UserAuthDTO> GetUserByUserName(string userName);

        [GeneratorCQRS(requestType:"Query",convention:"{MethodName}User")]
        [GenerateApiCQRS(action: "GET", route: "lookup", cqrsRequestName: "LookupUserQuery")]
        IEnumerable<NameListDTO?> Lookup();

        [GeneratorCQRS(requestType: "Query",convention: "{MethodName}User")]
        [GenerateApiCQRS(action: "GET", route: "{id}", cqrsRequestName: "GetUserQuery")]
        Task<UserDTO> Get(int id);

        [GeneratorCQRS(requestType: "Query", convention: "{MethodName}Users")]
        [GenerateApiCQRS(action: "GET", route: "", cqrsRequestName: "GetAllUsersQuery")]
        Task<IEnumerable<UserDTO>> GetAll();

        [GeneratorCQRS(requestType: "Query", convention: "{MethodName}UsersPaged")]

        [GenerateApiCQRS(action: "GET", route: "paged/{pageSize}/{rowIndex}", cqrsRequestName: "GetAllUsersPagedQuery")]
        Task<PagedResponse<IEnumerable<UserListDTO>>> GetAll(int rowIndex, int pageSize);

       
        [GeneratorCQRS(requestType: "Query", convention: "DuplicateUserName")]

        [GenerateApiCQRS(action: "GET", route: "exists/{userName}", cqrsRequestName: "DuplicateUserNameQuery")]
        Task<bool> IsExists(string userName);

        
        [GeneratorCQRS(requestType: "Command", convention: "CreateUser")]
        [GenerateApiCQRS(action: "POST", route: "", cqrsRequestName: "CreateUserCommand")]
        Task<int> Add(UserDTO entityDto);

        
        [GeneratorCQRS(requestType: "Command", convention: "UpdateUser")]

        [GenerateApiCQRS(action: "PUT", route: "", cqrsRequestName: "UpdateUserCommand")]
        Task<int> Update(UserDTO entityDto);

        [GeneratorCQRS(requestType: "Command", convention: "DeleteUser")]

        [GenerateApiCQRS(action: "DELETE", route: "", cqrsRequestName: "DeleteUserCommand")]
        Task<int> Delete(int id);


    }
}
