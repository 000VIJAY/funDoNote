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
       public void AddLabel(int UserId, int NoteId, string labelName);
        Label GetLabelsByNoteId(int UserId , int NoteId);
        List<GetLabelModel> GetLabelByNoteIdwithJoin(int UserId , int NoteId);
        List<GetLabelModel> GetLabelByUserIdWithJoin(int UserId);
    }
}
