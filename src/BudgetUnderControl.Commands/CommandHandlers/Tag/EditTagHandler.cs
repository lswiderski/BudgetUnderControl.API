using System.Threading.Tasks;

namespace BudgetUnderControl.CommonInfrastructure.Commands
{
    public class EditTagHandler : ICommandHandler<EditTag>
    {
        private readonly ITagService tagService;
        public EditTagHandler(ITagService tagService)
        {
            this.tagService = tagService;
        }

        public async Task HandleAsync(EditTag command)
        {
            await this.tagService.EditTagAsync(command);

        }
    }
}