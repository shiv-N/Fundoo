using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayerModel.NotesModels
{
    /// <summary>
    /// this is class ColourRequestModel
    /// </summary>
    public class ColourRequestModel
    {
        /// <summary>
        /// Gets or sets the color.
        /// </summary>
        /// <value>
        /// The color.
        /// </value>
        [Required]
        public string Color { get; set; }

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
