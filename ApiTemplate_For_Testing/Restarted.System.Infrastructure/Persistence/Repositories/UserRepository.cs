using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Restarted.System.Contracts.DTO;
using Restarted.System.Contracts.DTO.Common;
using Restarted.System.Contracts.Interfaces.Persistence;
using Restarted.System.Infrastructure.Persistence.Entities;

namespace Restarted.System.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AwakenedSystemContext context;
        private readonly IMapper mapper;

        public UserRepository(AwakenedSystemContext context, IMapper mapper)
        {
            this.context=context;
            this.mapper=mapper;
        }


        //public async Task<UserAuthDTO> GetUserByUserName(string userName)
        //{
        //    var result = await this.context.Users

        //         .Include(o => o.Roles)
        //         .Where(o => o.Username == userName)
        //         .FirstOrDefaultAsync();

        //    var dto = mapper.Map<UserAuthDTO>(result);
        //    return dto;

        //}

        //public async Task<UserAuthDTO> Register(UserAuthDTO? entityDto)
        //{
        //    var entity = this.mapper.Map<User>(entityDto);

        //    if (entity.Roles is not null)
        //    {
        //        var assignedRoles = entityDto.Roles.Select(o => o.Id).ToList();
        //        entity.Roles = this.context.Roles.Where(o => assignedRoles.Contains(o.Id)).ToList();
        //    }

        //    this.context.Add(entity);
        //    this.context.SaveChanges();

        //    entityDto.Id=entity.Id;
        //    return await Task.FromResult(entityDto);
        //}
        public  Task<int> Add(UserDTO entityDto)
        {
            var entity = this.mapper.Map<User>(entityDto);
            if (entity.Roles is not null)
            {
                var assignedRoles = entityDto.Roles.Select(o => o.Id).ToList();
                entity.Roles = this.context.Roles.Where(o => assignedRoles.Contains(o.Id)).ToList();
            }
            this.context.Add(entity);
            return  this.context.SaveChangesAsync();

        }

        public async Task<int> Delete(int id)
        {
            var entity = this.context.Users
                  .Include(a => a.Address)
                  .Include(c => c.Contact)
                  .Include(r => r.Roles)
                  .Include(a => a.UserAttributes)
                  .Where(u => u.Id == id)
                  .FirstOrDefault();

            //if (entity == null)
            //  throw new RecordNowFoundException($"{nameof(User)} not found in the database for ID {id}");
            if (entity == null)
                return default;

            if (context?.Entry(entity).State == EntityState.Detached)
            {
                context.Attach(entity);
            }
            if (entity.Address is not null)
                this.context.Remove(entity.Address);
            if (entity.Contact is not null)
                this.context.Remove(entity.Contact);
            if (entity.Roles is not null)
                entity.Roles.Clear();

            if (entity.UserAttributes is not null)
                entity.UserAttributes.Clear();

            this.context.Remove(entity);

            return await this.context.SaveChangesAsync();
        }
        public async Task<UserDTO> Get(int id)
        {
            var result = await this.context.Users

                  .Include(a => a.Address)
                   .Include(c => c.Contact)
                   .Include(r => r.Roles)
                   .Include(a => a.UserAttributes)

                 .FirstOrDefaultAsync(o => o.Id == id);

            var user = this.mapper.Map<UserDTO>(result);

            return user;
        }


        public async Task<IEnumerable<UserDTO>> GetAll()
        {
            return await this.context.Users
                .ProjectTo<UserDTO>(mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<PagedResponse<IEnumerable<UserListDTO>>> GetAll(int rowIndex, int pageSize)
        {
            int totalCount = this.context.Users.Count();
            var data =  this.context.Users
                .Include(a => a.Address)
                  .Include(c => c.Contact)
                  .Include(r => r.Roles)
                 .OrderBy(o => o.Name)
                 .Skip((rowIndex  -1) * pageSize)
                 .Take(pageSize)
                 .AsEnumerable()
                 .Select(usr => new UserListDTO
                 (
                     usr.Id,
                     usr.Name,
                     usr.Username,
                     usr?.Address?.Line1,
                     usr?.Contact?.PrimaryEmail,
                     usr?.Contact?.PrimaryPhone
                    
                 ));
            //.Select(usr => new UserListDTO
            //{
            //    Id = usr.Id,
            //    Name = usr.Name,
            //    Username = usr.Username,
            //    Image = usr.Image,
            //    Roles = string.Join(",", usr.Roles.Select(r => r.Name)),
            //    Address = usr?.Address?.Line1,
            //    Email = usr?.Contact?.PrimaryEmail,
            //    Phone = usr?.Contact?.PrimaryPhone,
            //    IsActive = usr?.IsActive,
            //    IsDeleted = usr.IsDeleted
            //});

            return await Task.FromResult(new PagedResponse<IEnumerable<UserListDTO>>(data, rowIndex, pageSize, totalCount));
        }

        public async Task<bool> IsExists(string userName)
        {
            return await this.context
                 .Users
                 .Where(o => o.Username == userName)
                 .CountAsync() >0;
        }



        public Task<int> Update(UserDTO entityDto)
        {
            var entity = this.context
                            .Users
                              .Include(a => a.Address)
                              .Include(c => c.Contact)
                              .Include(r => r.Roles)
                              .Include(a => a.UserAttributes)
                            .Where(u => u.Id == entityDto.Id)
                            .FirstOrDefault();

            if (entity == null)
                return Task.FromResult(0);

            this.mapper.Map(entityDto, entity);
            //entity.Id = entityDto.Id.Value;
            //entity.Name=entityDto.Name;
            //entity.Username = entityDto.Username;
            //entity.Password = entityDto.Password;
            //entity.AddressId = entityDto.AddressId;
            //entity.ContactId=entityDto.ContactId;
            //entity.Image=entityDto.Image;
            //entity.IsActive=entityDto.IsActive;
            //entity.IsDeleted= entityDto.IsDeleted.Value;
            if (entityDto.Address != null)
            {
                if (entityDto.Address.Id is 0)
                    entity.Address = new Address();

                entity.Address.Line1 = entityDto.Address.Line1;
                entity.Address.Line2 = entityDto.Address.Line2;
                entity.Address.Country = entityDto.Address.Country;
                entity.Address.State = entityDto.Address.State;
                entity.Address.City = entityDto.Address.City;
                entity.Address.Pin = entity.Address.Pin;
            }

            if (entityDto.Contact != null)
            {
                if (entityDto.Contact.Id is 0)
                    entity.Contact = new Contact();

                entity.Contact.PrimaryPhone = entityDto.Contact.PrimaryPhone;
                entity.Contact.PrimaryEmail = entityDto.Contact.PrimaryEmail;
                //entity.Contact.SecondaryPhone = entityDto.Contact.SecondaryPhone;
                //entity.Contact.SecondaryEmail = entityDto.Contact.SecondaryEmail;
                //entity.Contact.PrimaryFax = entityDto.Contact.PrimaryFax;
                //entity.Contact.SecondaryFax = entityDto.Contact.SecondaryFax;

            }

            //Map roles
            if (entityDto?.Roles is not null)
            {
                var assignedRoles = entityDto?.Roles?.Select(o => o.Id).ToList();
                entity.Roles = this.context.Roles.Where(o => assignedRoles.Contains(o.Id)).ToList();
            }
            //entity.Roles = mapper.Map<ICollection<Role>>(entityDto.Roles);
            //entity.UserAttributes = mapper.Map<ICollection<UserAttribute>>(entityDto.UserAttributes);
            //if (entityDto.UserAttributes != null)
            //{
            //    //DELETE ALL ATTRIBUTES
            //    entity.UserAttributes.Clear();

            //    //ADD ALL ATTRIBUTES
            //    foreach (var attribute in entityDto.UserAttributes)
            //        entity.UserAttributes.Add(new UserAttribute
            //        {
            //            AttributeId = attribute.AttributeId, // As ID is identity, no need to set the value
            //            UserId = attribute.UserId,
            //            Value = attribute.Value
            //        });
            //}

            this.context.Entry(entity).State = EntityState.Modified;

            return this.context.SaveChangesAsync();
        }

        public async Task<IEnumerable<NameListDTO?>> Lookup()
        {
            return await this.context.Users
                  .OrderBy(o => o.Name)
                  .Select(o =>
                  new NameListDTO { Id = o.Id, Name= o.Name }
                  ).ToListAsync();
        }

     
    }
}
