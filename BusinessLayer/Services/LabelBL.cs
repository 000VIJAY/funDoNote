﻿using BusinessLayer.Interfaces;
using CommonLayer.User;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Services;
using RepositoryLayer.Services.Entities;
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
        public Label GetLabelsByNoteId(int UserId ,int NoteId)
        {
            try
            {
               return this._levelRL.GetLabelsByNoteId(UserId,NoteId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<GetLabelModel> GetLabelByNoteIdwithJoin(int UserId, int NoteId)
        {
            try
            {
                return this._levelRL.GetLabelByNoteIdwithJoin(UserId, NoteId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<GetLabelModel> GetLabelByUserIdWithJoin(int UserId)
        {
            try
            {
                return this._levelRL.GetLabelByUserIdWithJoin(UserId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
