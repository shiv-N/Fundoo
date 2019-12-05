using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayerModel.Models
{
    public class ResetPasswordModel
    {
        [Required]
        public string Token { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        [Required]
        public string Password { get; set; }
        public string Email { get; set; }
        public string Id { get; set; }
    }
}
