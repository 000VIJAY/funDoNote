using RepositoryLayer.Interfaces;
using System;
using RepositoryLayer.Services.Entities;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace RepositoryLayer.Services
{
    public class LabelRL : ILabelRL
    {
       FunDoNoteContext _funDoNoteContext;
        private IConfiguration _config;
        public LabelRL(FunDoNoteContext funDoNoteContext, IConfiguration config)
        {
            _funDoNoteContext = funDoNoteContext;
            this._config = config;
        }

        public void AddLabel(int UserId, int NoteId, string labelName)
        {
            try
            {
                 Label _label = new Label();

                _label.UserId = UserId;
                _label.NoteId = NoteId; 
                _label.LabelName = labelName;
                _funDoNoteContext.Labels.Add(_label);
                _funDoNoteContext.SaveChanges();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
