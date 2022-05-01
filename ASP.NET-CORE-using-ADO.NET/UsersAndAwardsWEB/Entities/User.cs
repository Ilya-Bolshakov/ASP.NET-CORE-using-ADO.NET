using System;
using System.Collections.Generic;

namespace Entities
{
    public class User
    {
        private List<Award> _awards = new List<Award>();
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public int Age 
        {
            get
            {
                if (DateTime.Now.Month - BirthDate.Month < 0 || (DateTime.Now.Month - BirthDate.Month == 0 && DateTime.Now.Day - BirthDate.Day < 0))
                {
                    return DateTime.Now.Year - BirthDate.Year - 1;
                }
                else
                {
                    return DateTime.Now.Year - BirthDate.Year;
                }
            }
        }

        public List<Award> Awards
        {
            get => _awards;
            set
            {
                _awards = value;
            }
        }

        public User()
        {
            BirthDate = DateTime.Today;
            
        }

        public User(User user)
        {
            
            ID = user.ID;
            FirstName = new string(user.FirstName);
            LastName = new string(user.LastName);
            BirthDate = user.BirthDate;
            _awards = new List<Award>(user._awards);
        }


        public void AddAward(Award award)
        {
            _awards.Add(award);
        }

        public void DeleteAward(Award award)
        {
            _awards.Remove(award);
        }
    }
}
