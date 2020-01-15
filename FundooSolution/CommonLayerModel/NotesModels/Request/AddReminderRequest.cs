using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayerModel.NotesModels
{
    /// <summary>
    /// this is class AddReminderRequest
    /// </summary>
    public class AddReminderRequest
    {
        /// <summary>
        /// Gets or sets the add reminder.
        /// </summary>
        /// <value>
        /// The add reminder.
        /// </value>
        [Required]
        public DateTime Reminder { get; set; }
    }
}
