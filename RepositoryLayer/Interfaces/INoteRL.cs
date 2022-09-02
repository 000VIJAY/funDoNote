using CommonLayer.User;
using RepositoryLayer.Services.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface INoteRL
    {
        public void AddNote(NoteModel noteModel, int UserId);
        public void UpdateNote(UpdateNoteModel updateNoteModel, int UserId , int NoteId);
        public bool DeleteNote(int UserId , int NoteId);
    }
}
