using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayerModel.NotesModels.Request
{
    public class AddCollaboratorRequest
    {
        [Required]
        public int CollaboratorId { get; set; }
    }
}
