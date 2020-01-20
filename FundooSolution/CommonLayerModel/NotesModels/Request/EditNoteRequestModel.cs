using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayerModel.NotesModels
{
    /// <summary>
    /// this is class EditNoteRequestModel
    /// </summary>
    public class EditNoteRequestModel
    {

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message { get; set; }

        public DateTime? Reminder { get; set; }

    }
}
