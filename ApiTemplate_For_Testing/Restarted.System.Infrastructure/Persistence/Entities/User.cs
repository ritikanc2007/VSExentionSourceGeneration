namespace Restarted.System.Infrastructure.Persistence.Entities;

//using global::Restarted.Generators;
//using Restarted.Generators.Generators.DTO.Attributes;
//using Restarted.Generators.Generators.Repositories.Service.Attributes;

//[GenerateService (templates: "ModelDTO.tt", DTOFieldNames: "tempId,Temp1")]

//[Generators.Generators.DTO.Attributes.GeneratorDto(
//    featureName:"Admin",
//    featureModuleName:"Users",
//    convention: "{entity}DTO",
//    members: "Id,Name:string,UserName, Address:AddressDTO, Contact:ContactDTO, Roles:List<RoleDTO>",
//    allPropertiesNullable: true)]
//[Generators.Generators.DTO.Attributes.GeneratorDto(
//    featureName: "Admin",
//    featureModuleName: "Users",
//    convention: "{entity}ListDTO",
//    members: "Id,Name,Username,Address:string,Email,Phone",
//    allPropertiesNullable: true)]
//[GeneratorRepository(
//    featureName: "Admin",
//    featureModuleName: "Users",
//    convention: "{entity}Repository",
//    dtoName: "UserDTO",
//    dbContextName: "AwakenedSystemContext",
//    disable:  "false")]
public partial class User
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public int? AddressId { get; set; }

    public int? ContactId { get; set; }

    public byte[]? Image { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public int? DeletedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public bool? IsActive { get; set; }

    public virtual Address? Address { get; set; }

    public virtual Contact? Contact { get; set; }

    public virtual ICollection<UserAttribute> UserAttributes { get; set; } = new List<UserAttribute>();

    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
}
