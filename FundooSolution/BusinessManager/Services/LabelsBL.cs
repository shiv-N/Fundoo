
namespace BusinessManager.Services
{
    using BusinessManager.Interface;
    using CommonLayerModel.LabelModels;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// this is class LabelsBL
    /// </summary>
    /// <seealso cref="BusinessManager.Interface.ILabelsBL" />
    public class LabelsBL : ILabelsBL
    {
        /// <summary>
        /// The labels
        /// </summary>
        ILabelsRL labels;

        /// <summary>
        /// Initializes a new instance of the <see cref="LabelsBL"/> class.
        /// </summary>
        /// <param name="labels">The labels.</param>
        public LabelsBL(ILabelsRL labels)
        {
            this.labels = labels;
        }

        /// <summary>
        /// Adds the label.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public async Task<bool> AddLabel(AddLabel model,int userId)
        {
            return await labels.AddLabel(model, userId);
        }

        /// <summary>
        /// Deletes the label.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public async Task<bool> DeleteLabel(DeleteLabelRequest model,int userId)
        {
            return await labels.DeleteLabel(model, userId);
        }


        /// <summary>
        /// Edits the label.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public async Task<bool> EditLabel(EditLabel model,int userId)
        {
            return await labels.EditLabel(model, userId);
        }
    }
}
