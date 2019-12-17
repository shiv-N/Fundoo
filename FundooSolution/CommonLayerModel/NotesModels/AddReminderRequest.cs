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
        public DateTime AddReminder { get; set; }

        /// <summary>
        /// Gets or sets the modified date.
        /// </summary>
        /// <value>
        /// The modified date.
        /// </value>
        [Required]
        public DateTime ModifiedDate { get; set; }
    }
}
