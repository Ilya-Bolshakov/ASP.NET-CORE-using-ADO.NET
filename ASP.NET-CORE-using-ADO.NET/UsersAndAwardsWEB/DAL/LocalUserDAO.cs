using Entities;
using Interfaces;
using System.Collections.Generic;



namespace DAL
{
    public class LocalUserDAO : IUserDAO
    {
        private readonly List<User> _users;
        private static int _ID;

        public LocalUserDAO()
        {
            _users = new List<User>();
        }

        public User this[int index]
        {
            get
            {
                return _users[_users.FindIndex(0, x=> _users.GetEnumerator().Current.ID == index)];
            }
        }

        public void AddUser(User user)
        {
            user.ID = _ID++;
            _users.Add(user);
        }


        public void DeleteUser(User user)
        {
            _users.Remove(user);
        }

        public void ChangeUser(User user, int index)
        {
            _users[index] = user;
        }

        public IList<User> GetAll() => _users;

        public IList<Award> GetAllAwardsOfCurrentUser(int index)
        {
            return _users[index].Awards;
        }
    }
}
