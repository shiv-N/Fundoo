using BusinessManager.Interface;
using CommonLayerModel.LabelModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessManager.Services
{
    public class LabelsBL : ILabelsBL
    {
        ILabelsRL labels;
        public LabelsBL(ILabelsRL labels)
        {
            this.labels = labels;
        }


        public string AddLabel(AddLabel model,int userId)
        {
            return labels.AddLabel(model, userId);
        }

        public string DeleteLabel(DeleteLabelRequest model,int userId)
        {
            return labels.DeleteLabel(model, userId);
        }

        public string EditLabel(EditLabel model,int userId)
        {
            return labels.EditLabel(model, userId);
        }
    }
}
