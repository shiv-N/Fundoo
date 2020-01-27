using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayerModel.NotesModels.Request
{
    public class UploadImage
    {
        public IFormFile file { get; set; }
    }
}
