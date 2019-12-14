using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayerModel.Models
{
    /// <summary>
    /// this is class RegisterRequestModel
    /// </summary>
    public class RegisterRequestModel
    {
        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Name should be in between 3 and 100 character")]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Name should be in between 3 and 100 character")]
        [DataType(DataType.Text)]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the phone number.
        /// </summary>
        /// <value>
        /// The phone number.
        /// </value>
        [Phone]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the user address.
        /// </summary>
        /// <value>
        /// The user address.
        /// </value>
        [StringLength(250, MinimumLength = 3, ErrorMessage = "Name should be in between 3 and 250 character")]
        [DataType(DataType.MultilineText)]
        public string UserAddress { get; set; }
    }
}
