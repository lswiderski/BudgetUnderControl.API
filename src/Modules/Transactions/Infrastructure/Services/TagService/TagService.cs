using BudgetUnderControl.Modules.Transactions.Application.DTO;
using BudgetUnderControl.Domain;
using BudgetUnderControl.Domain.Repositiories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BudgetUnderControl.Modules.Transactions.Application.Services;
using BudgetUnderControl.Modules.Transactions.Application.Commands.Tags.CreateTag;
using BudgetUnderControl.Modules.Transactions.Application.Commands.Tags.UpdateTag;
using BudgetUnderControl.Shared.Abstractions.Contexts;

namespace BudgetUnderControl.Infrastructure.Services
{
    public class TagService : ITagService
    {
        private readonly ITagRepository tagRepository;
        private readonly IContext context;
        public TagService(ITagRepository tagRepository, IContext context)
        {
            this.tagRepository = tagRepository;
            this.context = context;
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

        public async Task AddTagAsync(CreateTagCommand command)
        {
            var tag = Tag.Create(command.Name, context.Identity.Id, false, command.ExternalId);
            await this.tagRepository.AddAsync(tag);
        }

        public async Task EditTagAsync(UpdateTagCommand command)
        {
            var tag = await this.tagRepository.GetAsync(command.ExternalId);
            tag.Edit(command.Name, tag.UserId,  command.IsDeleted);
            await this.tagRepository.UpdateAsync(tag);
        }
    }
}
