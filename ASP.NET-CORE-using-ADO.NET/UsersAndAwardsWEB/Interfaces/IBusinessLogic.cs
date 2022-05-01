using System;
using System.Collections.Generic;
using System.Text;
using Entities;


namespace Interfaces
{
    public interface IBusinessLogic
    {
        IList<User> GetAllUsers();
        IList<Award> GetAllAwards();
        public IList<Award> GetAllAwardsOfCurrentUser(int index);
        User GetUser(int index);
        Award GetAward(int ID);
        void AddUser(User user);
        void DeleteUser(User user);
        void ChangeUser(int index, User user);
        void AddNewAward(Award award);
        void DeleteAward(Award award);
        void ChangeAward(Award award, int index);
    }
}
