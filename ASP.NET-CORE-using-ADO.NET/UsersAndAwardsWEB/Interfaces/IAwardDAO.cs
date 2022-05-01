using System;
using System.Collections.Generic;
using System.Text;
using Entities;

namespace Interfaces
{
    public interface IAwardDAO
    {
        void AddNewAward(Award award);
        void DeleteAward(Award award);
        void ChangeAward(Award award, int index);
        Award this[int index]
        { get; set; }
        IList<Award> GetAll();
        Award GetAward(int ID);
    }
}
