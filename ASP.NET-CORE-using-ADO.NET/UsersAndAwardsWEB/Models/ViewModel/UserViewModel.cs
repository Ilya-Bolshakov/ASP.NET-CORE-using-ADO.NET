using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UsersAndAwardsWEB.Models.ViewModel
{
    public class UserViewModel : IValidatableObject
    {
        private List<Award> _awards = new List<Award>();
        public int ID { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Birth Date")]
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

        public List<string> AwardsString { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Age < 0)
            {
                yield return new ValidationResult(
                    "Age must be a more than 0", new[] { nameof(BirthDate) });
            }
            if (Age > 150)
            {
                yield return new ValidationResult(
                    "Age must be a less than 150", new[] { nameof(BirthDate) });
            }
            foreach (var item in FirstName)
            {
                if (!char.IsLetter(item))
                {
                    yield return new ValidationResult(
                    "Please, exclude all characters except letters", new[] { nameof(FirstName) });
                }
            }
            foreach (var item in LastName)
            {
                if (!char.IsLetter(item))
                {
                    yield return new ValidationResult(
                    "Please, exclude all characters except letters", new[] { nameof(LastName) });
                }
            }
        }

        public UserViewModel()
        {
        }
        public UserViewModel(User user)
        {
            Awards = user.Awards;
            FirstName = user.FirstName;
            LastName = user.LastName;
            BirthDate = user.BirthDate;
            ID = user.ID;
        }
    }

    public class PostSelectedViewModel
    {
        public int[] SelectedIds { get; set; }
    }
}
