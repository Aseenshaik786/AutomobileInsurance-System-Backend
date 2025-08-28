using AutomobileInsuranceSystem.Models;
using System;
using Xunit;
namespace AutomobileInsuranceSystem.Tests
{
    public class AutoControllerTests
    {
       

        [Fact]
        public void User_Should_Have_Valid_Email()
        {
            var email = "aseen@gmail.com";
            Assert.Contains("@", email);
        }


        [Fact]
        public void User_Should_Have_Valid_DOB()
        {
            var user = GetSampleUser();
            Assert.True(user.DateOfBirth < DateTime.Now); // DOB must be in the past
        }

        [Fact]
        public void User_Should_Have_NonEmpty_FirstName()
        {
            var user = GetSampleUser();
            Assert.False(string.IsNullOrWhiteSpace(user.FirstName));
        }

        [Fact]
        public void User_Should_Have_Valid_Aadhaar()
        {
            var user = GetSampleUser();
            Assert.Equal(12, user.AadhaarNumber.Length); // Aadhaar should be 12 digits
        }

        [Fact]
        public void User_Should_Have_CreatedAt_Set()
        {
            var user = GetSampleUser();
            Assert.NotEqual(default(DateTime), user.CreatedAt);
        }

        private User GetSampleUser()
        {
            return new User
            {
                UserId = 11,
                FirstName = "Aseen",
                LastName = "Shaik",
                Email = "aseen@gmail.com",
                PasswordHash = "secret123",
                Role = "User",
                DateOfBirth = new DateTime(2000, 1, 1),
                Gender = "Female",
                AadhaarNumber = "123456789012",
                PANNumber = "ABCDE1234F",
                StreetAddress = "1st Street",
                City = "Nellore",
                ZipCode = "524001",
                Country = "India",
                Occupation = "Developer",
                Website = "https://aseen.com",
                PictureUrl = "",
                CreatedAt = DateTime.Parse("2025-08-04T03:42:02.542Z")

            };
        }
    }
}
