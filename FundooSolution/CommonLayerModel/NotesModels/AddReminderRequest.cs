using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayerModel.NotesModels
{
    public class AddReminderRequest
    {
        [Required]
        public DateTime AddReminder { get; set; }
        [Required]
        public DateTime ModifiedDate { get; set; }
    }
}
