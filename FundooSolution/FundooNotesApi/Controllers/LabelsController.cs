using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessManager.Interface;
using CommonLayerModel.LabelModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FundooNotesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabelsController : ControllerBase
    {
        private readonly ILabelsBL labels;
        public LabelsController(ILabelsBL labels)
        {
            this.labels = labels;
        }
        [HttpPost]
        public IActionResult AddLabel(AddLabel model)
        {
            int userId = TokenUserId();
            return Ok(labels.AddLabel(model, userId));
        }

        [HttpPut]
        public IActionResult EditLabel(EditLabel model)
        {
            var userId = TokenUserId();
            return Ok(labels.EditLabel(model, userId));
        }
        [HttpDelete]
        public IActionResult DeleteLabel(DeleteLabelRequest model)
        {
            var userId = TokenUserId();
            return Ok(labels.DeleteLabel(model, userId));
        }
        private int TokenUserId()
        {
            return Convert.ToInt32(User.FindFirst("Id").Value);
        }
    }
}