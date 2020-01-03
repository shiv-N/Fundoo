
namespace FundooNotesApi.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using BusinessManager.Interface;
    using CommonLayerModel.LabelModels;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// this is class LabelsController
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class LabelsController : ControllerBase
    {
        /// <summary>
        /// The labels
        /// </summary>
        private readonly ILabelsBL labels;

        /// <summary>
        /// Initializes a new instance of the <see cref="LabelsController"/> class.
        /// </summary>
        /// <param name="labels">The labels.</param>
        public LabelsController(ILabelsBL labels)
        {
            this.labels = labels;
        }

        /// <summary>
        /// Adds the label.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost("Add")]
        public async Task<IActionResult> AddLabel(AddLabel model)
        {
            int userId = TokenUserId();
            if(await labels.AddLabel(model, userId))
            {
                string status = "Label added";
                return Ok(new { status,userId,model});
            }
            else
            {
                string status = "Label did not added";
                return BadRequest(new { status, userId, model });
            }
        }

        /// <summary>
        /// Edits the label.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPut("edit")]
        public async Task<IActionResult> EditLabel(EditLabel model)
        {
            var userId = TokenUserId();
            if (await labels.EditLabel(model, userId))
            {
                string status = "Label edited";
                return Ok(new { status, userId, model });
            }
            else
            {
                string status = "Label did not edited";
                return BadRequest(new { status, userId, model });
            }
        }

        /// <summary>
        /// Deletes the label.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteLabel(DeleteLabelRequest model)
        {
            var userId = TokenUserId();
            if (await labels.DeleteLabel(model, userId))
            {
                string status = "Label deleted";
                return Ok(new { status, userId, model });
            }
            else
            {
                string status = "Label did not deleted";
                return BadRequest(new { status, userId, model });
            }
        }

        /// <summary>
        /// Tokens the user identifier.
        /// </summary>
        /// <returns></returns>
        private int TokenUserId()
        {
            return Convert.ToInt32(User.FindFirst("Id").Value);
        }
    }
}