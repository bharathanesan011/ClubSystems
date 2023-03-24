using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace ClubSystems.Models
{
    public class Person
    {
        [Key]
        public int PersonID { get; set; }
        [Required]
        [NotNull]
        public string Forenames { get; set; }
        [Required]
        [NotNull]
        public string Surname { get; set; }
        [Required]
        [NotNull]
        public string Email { get; set; }

        public string Phone { get; set; }

        public string PostCode { get; set; }

        public Person() { }
        public Person(int personID, string forenames, string surname, string email, string phone, string postCode)
        {
            PersonID = personID;
            Forenames = forenames;
            Surname = surname;
            Email = email;
            Phone = phone;
            PostCode = postCode;
        }
    }
}
