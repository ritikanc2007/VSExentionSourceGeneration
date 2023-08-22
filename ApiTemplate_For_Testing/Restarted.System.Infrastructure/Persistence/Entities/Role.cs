//using Restarted.Generators;
//using Restarted.Generators.Common.Configurations;

namespace Restarted.System.Infrastructure.Persistence.Entities;
//[GenerateService  (templates: "ModelDTO.tt", DTOFieldNames: "tempId,Temp1")]

//[Generators.Generators.DTO.Attributes.GeneratorDto(
//    featureName: "",
//    featureModuleName: "",
//    convention: "{entity}DTO",
//    members: "Id,Name",
//    allPropertiesNullable: true,
//    GenerationPath = FolderPath.COMMON_DTO_PATH)]
public partial class Role
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
