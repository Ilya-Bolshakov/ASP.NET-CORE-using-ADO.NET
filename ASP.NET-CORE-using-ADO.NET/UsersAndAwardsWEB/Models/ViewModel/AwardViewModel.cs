using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Entities;

namespace UsersAndAwardsWEB.Models.ViewModel
{
    public class AwardViewModel : IValidatableObject
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        [Required]
        [StringLength(250)]
        public string Description { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            foreach (var item in Title)
            {
                if (!char.IsLetter(item))
                {
                    if (item != ' ')
                    {
                        yield return new ValidationResult(
                        "Please, exclude all characters except letters", new[] { nameof(Title) });
                    }
                }
            }
            foreach (var item in Description)
            {
                if (!char.IsLetter(item))
                {
                    if (item != ' ')
                    {
                        yield return new ValidationResult(
                        "Please, exclude all characters except letters", new[] { nameof(Description) });
                    }
                   
                }
            }
        }
        public AwardViewModel()
        {
        }

        public AwardViewModel(Award award)
        {
            ID = award.ID;
            Title = award.Title;
            Description = award.Description;
        }
    }
}
