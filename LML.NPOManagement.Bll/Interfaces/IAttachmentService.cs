using LML.NPOManagement.Bll.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LML.NPOManagement.Bll.Interfaces
{
    public interface IAttachmentService
    {
        public IEnumerable<AttachmentModel> GetAllAttachments();
        public AttachmentModel GetAttachmentById(int id);
        public int AddAttachment(AttachmentModel attachmentModel);
        public int ModifyAttachment(AttachmentModel attachmentModel, int id);
        public void DeleteAttachment(int id);
    }
}
