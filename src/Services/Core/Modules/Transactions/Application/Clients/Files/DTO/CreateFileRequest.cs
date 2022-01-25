using System;

namespace BudgetUnderControl.Modules.Transactions.Application.Clients.Files.DTO
{
    public class CreateFileRequest
    {
        public CreateFileRequest(Guid id, string fileName, Guid userId, DateTime createdOn,
            string contentType, byte[] content, DateTime? modifiedOn)
        {
            Id = id;
            FileName = fileName;
            UserId = userId;
            CreatedOn = createdOn;
            ContentType = contentType;
            Content = content;
            ModifiedOn = modifiedOn;
        }

        public Guid Id { get; set; }
        public string FileName { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ContentType { get; set; }

        public byte[] Content { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}