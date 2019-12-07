﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayerModel.NotesModels
{
    public class AddNotesRequestModel
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public string Image { get; set; }
        public string Color { get; set; }
        public bool IsPin { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public DateTime AddReminder { get; set; }
        public bool IsArchive { get; set; }
        public bool IsNote { get; set; }
        public bool IsTrash { get; set; }
    }
}
