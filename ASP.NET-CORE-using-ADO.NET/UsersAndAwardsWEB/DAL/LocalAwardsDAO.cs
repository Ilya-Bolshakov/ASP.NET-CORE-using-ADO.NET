using System;
using System.Collections.Generic;
using System.Text;
using Entities;
using Interfaces;

namespace DAL
{
    public class LocalAwardsDAO : IAwardDAO
    {
        private readonly List<Award> _awards;
        private static int _ID;

        public LocalAwardsDAO()
        {
            _awards = new List<Award>()
        {
            new Award { ID = 0, Title = "Nobel Prize", Description = "NP" },
            new Award { ID = 1, Title = "Darvim Prize", Description = "DP" }
        };
            _ID = 2;
        }

        public Award this[int index]
        {
            get
            {
                return _awards[_awards.FindIndex(0, x => _awards.GetEnumerator().Current.ID == index)];
            }
            set
            {
                _awards[index] = value;
            }
        }

        public void AddNewAward(Award award)
        {
            award.ID = _ID++;
            _awards.Add(award);
        }

        public void ChangeAward(Award award, int index)
        {
            _awards[index] = award;
        }

        public void DeleteAward(Award award)
        {
            _awards.Remove(award);
        }

        public IList<Award> GetAll()
        {
            return _awards;
        }

        public Award GetAward(int ID)
        {
            return _awards[ID];
        }
    }
}
