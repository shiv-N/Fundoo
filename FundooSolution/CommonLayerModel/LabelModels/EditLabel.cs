using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayerModel.LabelModels
{
    /// <summary>
    /// this is class EditLabel
    /// </summary>
    public class EditLabel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the label.
        /// </summary>
        /// <value>
        /// The name of the label.
        /// </value>
        public string LabelName { get; set; }

        /// <summary>
        /// Gets or sets the modified date time.
        /// </summary>
        /// <value>
        /// The modified date time.
        /// </value>
        public DateTime ModifiedDateTime { get; set; }
    }
}
