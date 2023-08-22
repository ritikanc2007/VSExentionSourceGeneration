//using Restarted.Generators;
//using Restarted.Generators.Common.Configurations;

namespace Restarted.System.Infrastructure.Persistence.Entities;
//[GenerateService    (templates: "ModelDTO.tt", DTOFieldNames: "tempId,Temp1")]

//[Generators.Generators.DTO.Attributes.GeneratorDto(
//    featureName: "",
//    featureModuleName: "",
//    convention: "{entity}DTO",
//    members: "Id,PrimaryPhone,PrimaryEmail, PrimaryFax",
//    allPropertiesNullable: true,
//    GenerationPath = FolderPath.COMMON_DTO_PATH)]
public partial class Contact
{
    public int Id { get; set; }

    public string? PrimaryPhone { get; set; }

    public string? PrimaryEmail { get; set; }

    public string? SecondaryPhone { get; set; }

    public string? SecondaryEmail { get; set; }

    public string? PrimaryFax { get; set; }

    public string? SecondaryFax { get; set; }

    public virtual ICollection<Branch> Branches { get; set; } = new List<Branch>();

    public virtual ICollection<Organization> Organizations { get; set; } = new List<Organization>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}