using System;
using System.Collections.Generic;
using System.IO;
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

        //public IConfigurationRoot Getconfiguration()
        //{
        //    var Builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).Add
        //}
    }
}
