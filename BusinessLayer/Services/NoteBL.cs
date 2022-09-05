﻿using BusinessLayer.Interfaces;
using CommonLayer.User;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Services;
using RepositoryLayer.Services.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class NoteBL : INoteBL
    {
        readonly INoteRL _noteRL; 
        public NoteBL(INoteRL noteRL)
        {
            _noteRL = noteRL;
        }
        public void AddNote(NoteModel noteModel, int UserId)
        {
            try
            {
                this._noteRL.AddNote(noteModel,UserId);
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
             this._noteRL.UpdateNote(updateNoteModel, UserId, NoteId);
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
                return this._noteRL.DeleteNote(UserId, NoteId);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public Note GetNote(int UserId, int NoteId)
        {
            try
            {
                return _noteRL.GetNote(UserId, NoteId);
            }
            catch(Exception ex)
            {
                throw ex;
            }
          
        }

        public List<Note> GetAllNotes(int UserId)
        {
            try
            {
                return _noteRL.GetAllNotes(UserId);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public List<NoteResponseModel> GetAllNotesUsingJoin(int UserId)
        {
            try
            {
                return _noteRL.GetAllNotesUsingJoin(UserId);
            }
            catch(Exception ex)
            {
                throw ex;
            }
          
        }

        public async Task<bool> ArchieveNote(int UserId, int NoteId)
        {
            try
            {
                return await this._noteRL.ArchieveNote(UserId, NoteId);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
