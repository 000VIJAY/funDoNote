﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces
{
    public interface ILabelRL
    {
       public void AddLabel(int UserId, int NoteId, string labelName);
    }
}
