using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayerModel.AccountModels
{
    public class ApplicationSettings
    {
        /// <summary>
        /// Gets or sets the JWT secret.
        /// </summary>
        /// <value>
        /// The JWT secret.
        /// </value>
        public string JWT_Secret { get; set; }
    }
}
