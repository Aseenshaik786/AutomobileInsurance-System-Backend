namespace AutomobileInsuranceSystem.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; } // User or Officer
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string AadhaarNumber { get; set; }
        public string PANNumber { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public string Occupation { get; set; }
        public string Website { get; set; }
        public string PictureUrl { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

       
        public ICollection<Vehicle> Vehicles { get; set; }
        public ICollection<Proposal> Proposals { get; set; }
        public ICollection<Review> Reviews { get; set; }

    }

}
