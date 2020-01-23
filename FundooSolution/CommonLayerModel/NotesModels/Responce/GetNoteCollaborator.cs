using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayerModel.NotesModels.Responce
{
    public class GetNoteCollaborator
    {
        public int CollabratorId { get; set; }
        public int UserId { get; set; }
        public int CollabratorWithId { get; set; }
        public int NoteId { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public DateTime? ModifiedDateTime { get; set; }
    }
}
