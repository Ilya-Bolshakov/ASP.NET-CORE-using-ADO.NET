using System;
using Entities;
using System.Collections.Generic;

namespace Interfaces
{
    public interface IUserDAO
    {
        public void AddUser(User user);
        public void DeleteUser(User user);
        public void ChangeUser(User user, int index);
        public User this[int index]
        { get; /*set;*/ }
        public IList<User> GetAll();
        public IList<Award> GetAllAwardsOfCurrentUser(int index);




    }
}
