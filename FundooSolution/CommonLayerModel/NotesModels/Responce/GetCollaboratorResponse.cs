using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayerModel.NotesModels.Responce
{
    public class GetCollaboratorResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string email { get; set; }
    }
}
