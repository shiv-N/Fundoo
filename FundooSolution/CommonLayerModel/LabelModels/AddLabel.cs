using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayerModel.LabelModels
{
    /// <summary>
    /// this is class AddLabel
    /// </summary>
    public class AddLabel
    {
        /// <summary>
        /// Gets or sets the name of the label.
        /// </summary>
        /// <value>
        /// The name of the label.
        /// </value>
        public string LabelName { get; set; }

        /// <summary>
        /// Gets or sets the created date time.
        /// </summary>
        /// <value>
        /// The created date time.
        /// </value>
        public DateTime CreatedDateTime { get; set; }
    }
}
