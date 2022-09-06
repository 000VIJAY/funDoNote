using System;
using System.Collections.Generic;
using RepositoryLayer.Services.Entities;
using System.Text;
using System.Threading.Tasks;
using CommonLayer.User;

namespace RepositoryLayer.Interfaces
{
    public interface ILabelRL
    {
         Task AddLabel(int UserId, int NoteId, string labelName);
        Task<Label> GetLabelsByNoteId(int UserId , int NoteId);
        Task<GetLabelModel> GetLabelByNoteIdwithJoin(int UserId , int NoteId);
        List<GetLabelModel> GetLabelByUserIdWithJoin(int UserId);
        Task UpdateLabel(int UserId,int NoteId, string newLabel);
    }
}
