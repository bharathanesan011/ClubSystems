using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ClubSystems.Data;

namespace ClubSystems.Models
{
    public class MemberShip
    {
        [Key]
        public int MemberShipNumber { get; set; }
        [Required]
        public MemberShipType MemberShipType { get; set;}
        [Required]
        public double AccountBalance { get; set;}

        [Required]
        [ForeignKey("Person")]
        public int PersonID { get; set; }
        public Person Person { get; set; }

        public bool IsOverdrawn { get; set; } = false;

        public MemberShip() { }

        public MemberShip(int memberShipNumber, MemberShipType memberShipType, double accountBalance, int personID, bool isOverdrawn)
        {
            MemberShipNumber = memberShipNumber;
            MemberShipType = memberShipType;
            AccountBalance = accountBalance;
            PersonID = personID;
            IsOverdrawn = isOverdrawn;
        }
    }
}
