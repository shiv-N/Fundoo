using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayerModel.NotesModels.Responce
{
    public class AddCollaboratorResponce
    {
        public int CollaborationRecordId { get; set; }
        public int UserId { get; set; }
        public int NoteId { get; set; }
        public int CollaboratorId { get; set; }
        public DateTime CreatedDateTime { get; set; }
    }
}
