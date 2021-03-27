using BudgetUnderControl.Common.Contracts;
using BudgetUnderControl.Domain;
using BudgetUnderControl.Domain.Repositiories;
using BudgetUnderControl.CommonInfrastructure.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BudgetUnderControl.CommonInfrastructure;

namespace BudgetUnderControl.Infrastructure.Services
{
    public class TagService : ITagService
    {
        private readonly ITagRepository tagRepository;
        private readonly IUserIdentityContext userIdentityContext;
        public TagService(ITagRepository tagRepository, IUserIdentityContext userIdentityContext)
        {
            this.tagRepository = tagRepository;
            this.userIdentityContext = userIdentityContext;
        }


        public async Task<ICollection<TagDTO>> GetTagsAsync()
        {
            var tags = await this.tagRepository.GetAsync();

            var result = tags.Select(t => new TagDTO
            {
                Id = t.Id,
                Name = t.Name,
                IsDeleted = t.IsDeleted,
                ExternalId = t.ExternalId
            }).ToList();
            return result;
        }

        public async Task<ICollection<TagDTO>> GetActiveTagsAsync()
        {
            var tags = await this.tagRepository.GetAsync();

            var result = tags
                .Where(t => t.IsDeleted == false)
                .Select(t => new TagDTO
            {
                Id = t.Id,
                Name = t.Name,
                IsDeleted = t.IsDeleted,
                ExternalId = t.ExternalId
            }).ToList();
            return result;
        }

        public async Task<TagDTO> GetTagAsync(Guid tagId)
        {
            var tag = await this.tagRepository.GetAsync(tagId);

            var result = new TagDTO
            {
                Id = tag.Id,
                Name = tag.Name,
                IsDeleted = tag.IsDeleted,
                ExternalId = tag.ExternalId
            };
            return result;
        }

        public async Task AddTagAsync(AddTag command)
        {
            var tag = Tag.Create(command.Name, userIdentityContext.UserId, false, command.ExternalId);
            await this.tagRepository.AddAsync(tag);
        }

        public async Task EditTagAsync(EditTag command)
        {
            var tag = await this.tagRepository.GetAsync(command.ExternalId);
            tag.Edit(command.Name, tag.OwnerId,  command.IsDeleted);
            await this.tagRepository.UpdateAsync(tag);
        }
    }
}
