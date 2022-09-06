﻿using CommonLayer;
using CommonLayer.User;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Services.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                var user = _noteContext.Users.Where(x=> x.UserId == UserId).FirstOrDefault();
                Note note = new Note();
                note.user = user;
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
                if (note != null || note.IsTrash == false)
                {

                    note.Title = updateNoteModel.Title != "string" ? updateNoteModel.Title : note.Title;
                    note.Description = updateNoteModel.Description != "string" ? updateNoteModel.Description : note.Description;
                    note.Color = updateNoteModel.Color != "string" ? updateNoteModel.Color : note.Color;
                    note.IsPin = updateNoteModel.IsPin;
                    note.IsReminder = updateNoteModel.IsReminder;
                    note.IsArchieve = updateNoteModel.IsArchieve;
                    note.IsTrash = updateNoteModel.IsArchieve;
                    note.Reminder = updateNoteModel.Reminder;
                    note.ModifiedDate = DateTime.Now;
                }
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
                if (note != null || note.IsTrash == false)
                {
                    if (note == null)
                    {
                        return false;
                    }
                    _noteContext.Note.Remove(note);
                }
                _noteContext.SaveChanges();
                return true;
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
                var note = _noteContext.Note.Where(x => x.NoteId == NoteId).FirstOrDefault();
                
                return note;
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
                var user = _noteContext.Users.Where(x => x.UserId == UserId).FirstOrDefault();
                var note =  _noteContext.Note.Where(x => x.UserId == UserId).ToList(); //using LINQ
                return note;
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
                //Using LINQ join
                return _noteContext.Users.Where(u => u.UserId == UserId)
                .Join(_noteContext.Note,
                u => u.UserId,
                n => n.UserId,
                (u, n) => new NoteResponseModel
                {
                    NoteId = n.NoteId,
                    UserId = n.UserId,
                    Title = n.Title,
                    Description = n.Description,
                    Color = n.Color,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    CreatedDate = u.CreatedDate
                }).ToList();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> ArchieveNote(int UserId, int NoteId)
        {
            try
            {

                var note = await _noteContext.Note.Where(x => x.NoteId == NoteId).FirstOrDefaultAsync();
                if (note == null)
                {
                    return false;
                }
                if (note.IsTrash == false)
                {
                    if (note.IsArchieve == true)
                    {
                        note.IsArchieve = false;
                    }
                    else { note.IsArchieve = true; } 
                }
               
                await _noteContext.SaveChangesAsync();
                return  true;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> PinNote(int UserId, int NoteId)
        {
            try
            {

                var note = await _noteContext.Note.Where(x => x.NoteId == NoteId).FirstOrDefaultAsync();
                if (note == null)
                {
                    return false;
                }

                if (note.IsTrash == false)
                {
                    if (note.IsPin == true)
                    {
                        note.IsPin = false;
                    }
                    else { note.IsPin = true; }
                }
                await _noteContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> TrashNote(int UserId, int NoteId)
        {
            try
            {
                var note = await _noteContext.Note.Where(x => x.NoteId == NoteId).FirstOrDefaultAsync();
                if (note == null)
                {
                    return false;
                }
                if (note.IsTrash == true)
                {
                    note.IsTrash = false;
                }
                else { note.IsTrash = true; }
                await _noteContext.SaveChangesAsync();
                return true;
            }
            catch(Exception ex)
            {
                throw ex;
            }
           
        }

        public async Task<bool> ReminderNote(int UserId, int NoteId ,DateTime reminder)
        {
            try
            {
                var note = await _noteContext.Note.Where(x => x.NoteId == NoteId).FirstOrDefaultAsync();
                if (note == null || note.IsTrash == true)
                {
                    return false;
                }
                note.IsReminder = true;
                note.Reminder = reminder;
            
                _noteContext.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteReminderNote(int UserId, int NoteId)
        {
            try
            {
                var note = await _noteContext.Note.Where(x => x.NoteId == NoteId).FirstOrDefaultAsync();
                if (note == null || note.IsTrash == true)
                {
                    return false;
                }
                note.IsReminder = false;
                note.Reminder = DateTime.Now;

                _noteContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task UpdateColor(int UserId, int NoteId, string Color)
        {
            try
            {
                var note = await _noteContext.Note.Where(x => x.NoteId == NoteId).FirstOrDefaultAsync();
                if (note != null && note.IsTrash != true)
                {
                    note.Color = Color;
                }
                
                _noteContext.SaveChanges();
               
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public  List<GetColorModel> GetAllColour(int UserId)
        {
            try
            {
                return  _noteContext.Users.Where(u => u.UserId == UserId)
                .Join(_noteContext.Note,
                u => u.UserId,
                n => n.UserId,
                (u, n) => new GetColorModel
                {
                    Color = n.Color
                }).ToList();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
