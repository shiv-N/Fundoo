using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Text;

namespace CommonLayerModel.NotesModels.Request
{
    public class DeleteReminderRequest
    {

        /// <summary>
        /// Gets or sets the add reminder.
        /// </summary>
        /// <value>
        /// The add reminder.
        /// </value>
        
        public DateTime? Reminder { get; set; }
    }
}
