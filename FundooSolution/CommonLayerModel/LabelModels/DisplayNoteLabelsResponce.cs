using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayerModel.LabelModels
{
    public class DisplayNoteLabelsResponce
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int NoteId { get; set; }
        public int LabelId { get; set; }
        public string LabelName { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime? ModifiedTime { get; set; }
    }
}
