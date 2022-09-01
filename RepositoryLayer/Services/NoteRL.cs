using CommonLayer.User;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Services.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Services
{
    public class NoteRL : INoteRL
    {
        readonly FunDoNoteContext _noteContext;

        public NoteRL(FunDoNoteContext noteContext)
        {
            this._noteContext = noteContext;
        }

        public void AddNote(NoteModel noteModel, int UserId)
        {
            try
            {
                Note note = new Note();
                note.Title = noteModel.Title;
                note.Description = noteModel.Description;
                note.Color = noteModel.Color;
                note.UserId = UserId;
                note.Reminder = DateTime.Now;
                note.CreatedDate = DateTime.Now;
                note.ModifiedDate = DateTime.Now;
                _noteContext.Add(note);
                _noteContext.SaveChanges();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
