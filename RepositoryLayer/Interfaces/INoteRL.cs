using CommonLayer.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface INoteRL
    {
        public void AddNote(NoteModel noteModel, int UserId);
        public void UpdateNote(NoteModel noteModel, int UserId , int userId);
    }
}
