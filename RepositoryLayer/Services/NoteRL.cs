﻿using CommonLayer.User;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Services.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public void UpdateNote(UpdateNoteModel updateNoteModel, int UserId, int NoteId)
        {
            try
            {
               var note = _noteContext.Note.Where(x => x.NoteId == NoteId).FirstOrDefault();
 
                note.Title = updateNoteModel.Title!= "string" ? updateNoteModel.Title:note.Title;
                note.Description = updateNoteModel.Description!= "string"?updateNoteModel.Description:note.Description;
                note.Color = updateNoteModel.Color!="string"? updateNoteModel.Color:note.Color;
                note.IsPin = updateNoteModel.IsPin;
                note.IsReminder = updateNoteModel.IsReminder;
                note.IsArchieve = updateNoteModel.IsArchieve;
                note.IsTrash = updateNoteModel.IsArchieve;
                note.Reminder = updateNoteModel.Reminder;
                note.ModifiedDate = DateTime.Now;
                _noteContext.SaveChanges();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public bool DeleteNote(int UserId, int NoteId)
        {
            try
            {
                var note = _noteContext.Note.Where(x => x.NoteId == NoteId).FirstOrDefault();
                if(note == null)
                {
                    return false;
                }
               _noteContext.Note.Remove(note);
                _noteContext.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
