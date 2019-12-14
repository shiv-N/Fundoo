using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayerModel.NotesModels
{
    public class ColourRequestModel
    {
        [Required]
        public string Color { get; set; }
        [Required]
        public DateTime ModifiedDate { get; set; }
    }
}
