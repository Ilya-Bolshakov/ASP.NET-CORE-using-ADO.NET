using System.Collections.Generic;
using Entities;
using Interfaces;


namespace BLL
{
    public class BusinessLogic : IBusinessLogic
    {
        private readonly IUserDAO _user;
        private readonly IAwardDAO _awards; 

        public BusinessLogic(IUserDAO user, IAwardDAO awards)
        {
            _user = user;
            _awards = awards;
        }

        public IList<User> GetAllUsers()
        {
            return _user.GetAll();
        }

        public IList<Award> GetAllAwards()
        {
            return _awards.GetAll();
        }

        public void AddUser(User user)
        {
            _user.AddUser(user);
        }

        public void DeleteUser(User user)
        {
            _user.DeleteUser(user);
        }

        public void ChangeUser(int index, User user)
        {
            _user.ChangeUser(user, index);
        }

        public User GetUser(int index)
        {
            return _user[index];
        }

        public void AddNewAward(Award award)
        {
            _awards.AddNewAward(award);
        }
        
        public void DeleteAward(Award award)
        {
            _awards.DeleteAward(award);
        }

        public void ChangeAward(Award award, int index)
        {
            _awards.ChangeAward(award, index);
        }

        public Award GetAward(int ID)
        {
            return _awards.GetAward(ID);
        }

        public IList<Award> GetAllAwardsOfCurrentUser(int index)
        {
            return _user.GetAllAwardsOfCurrentUser(index);
        }
    }
}
