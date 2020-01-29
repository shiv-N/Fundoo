using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayerModel.NotesModels.Responce
{
    public class GetAllLabelsResponce
    {
        public int Id { get; set; }
        public string LabelName { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedDateTime { get; set; }

        public DateTime? ModifiedDateTime { get; set; }

    }
}
