using CommonLayerModel.LabelModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessManager.Interface
{
    public interface ILabelsRL
    {
        string AddLabel(AddLabel model,int userId);
        string EditLabel(EditLabel model,int userId);
        string DeleteLabel(DeleteLabelRequest model,int userId);
    }
}
