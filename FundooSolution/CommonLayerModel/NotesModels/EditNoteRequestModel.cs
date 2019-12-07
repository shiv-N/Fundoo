using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayerModel.NotesModels
{
    public class EditNoteRequestModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string Image { get; set; }
        public string Color { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
