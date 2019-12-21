using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayerModel.AccountModels.Response
{
    public class GetAllUserResponce
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string ServiceType { get; set; }
        public string UserType { get; set; }
    }
}
