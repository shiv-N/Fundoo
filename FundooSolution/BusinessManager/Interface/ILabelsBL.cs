﻿
namespace BusinessManager.Interface
{
    using CommonLayerModel.LabelModels;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// this is interface ILabelsBL
    /// </summary>
    public interface ILabelsBL
    {
        /// <summary>
        /// Adds the label.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        Task<bool> AddLabel(AddLabel model,int userId);

        /// <summary>
        /// Edits the label.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        Task<bool> EditLabel(EditLabel model,int userId);

        /// <summary>
        /// Deletes the label.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        Task<bool> DeleteLabel(DeleteLabelRequest model,int userId);
    }
}
