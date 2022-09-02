using CommonLayer.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface INoteBL
    {
        void AddNote(NoteModel noteModel, int UserId);
        public void UpdateNote(UpdateNoteModel updateNoteModel, int UserId, int NoteId);
    }
}
