using BusinessLayer.Interfaces;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class LabelBL : ILabelBL
    {
        ILabelRL _levelRL;
        public LabelBL(ILabelRL levelRL)
        {
            this._levelRL = levelRL;
        }

        public void AddLabel(int UserId, int NoteId, string labelName)
        {
            try
            {
                this._levelRL.AddLabel(UserId, NoteId, labelName);
            }catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
