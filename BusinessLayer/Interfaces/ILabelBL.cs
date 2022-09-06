using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface ILabelBL
    {
        void AddLabel(int UserId , int NoteId , string labelName);
    }
}
