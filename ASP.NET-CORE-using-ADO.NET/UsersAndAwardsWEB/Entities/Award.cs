using System;
using System.Diagnostics.CodeAnalysis;

namespace Entities
{
    public class Award : IEquatable<Award>
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public Award()
        {
        }

        public Award(Award award)
        {
            ID = award.ID;
            Title = award.Title;
            Description = award.Description;
        }

        public bool Equals(Award award)
        {
            if (ReferenceEquals(null, award)) return false;
            if (ReferenceEquals(this, award)) return true;
            return this.ID == award.ID;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (this.GetType() != obj.GetType()) return false;
            return Equals((Award)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ID);
        }
    }
}
