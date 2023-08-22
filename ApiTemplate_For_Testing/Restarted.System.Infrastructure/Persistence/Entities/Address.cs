//using Restarted.Generators;
//using Restarted.Generators.Common.Configurations;

namespace Restarted.System.Infrastructure.Persistence.Entities;
//[GenerateService    (templates: "ModelDTO.tt", DTOFieldNames: "tempId,Temp1")]

//[Generators.Generators.DTO.Attributes.GeneratorDto(
//    featureName: "",
//    featureModuleName: "",
//    convention: "{entity}DTO",
//    members: "Id,Line1,Line2, Country, State,City",
//    allPropertiesNullable: true,
//    GenerationPath = FolderPath.COMMON_DTO_PATH)]
public partial class Address
{
    public int Id { get; set; }

    public string? Line1 { get; set; }

    public string? Line2 { get; set; }

    public int? Country { get; set; }

    public string? State { get; set; }

    public string? City { get; set; }

    public string? Pin { get; set; }

    public virtual ICollection<Branch> Branches { get; set; } = new List<Branch>();

    public virtual Country? CountryNavigation { get; set; }

    public virtual ICollection<Organization> Organizations { get; set; } = new List<Organization>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}