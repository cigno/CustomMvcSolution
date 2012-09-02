using CustomMvcSolution.Domain.Entities.Attributes;
using CustomMvcSolution.Helpers.Generic;

namespace CustomMvcSolution.Domain.Entities
{
    public class Member
    {
        public int MemberId { get; set; }

        [Unique]
        [Indexed]
        public string UserId { get; set; }
        [Unique]
        public string Email { get; set; }
        public Profile Profile { get; set; }
        public Role Role { get; set; }
    }

    public class Profile
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompleteName { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }
        public string Building { get; set; }
        public string Floor { get; set; }
        public string Office { get; set; }
    }

    public class Password
    {
        public Password() { }
        public Password(string password)
        {
            SetPassword(password);
        }
        public Password(string password, Member member)
        {
            SetPassword(password);
            Member = member;
        }

        public int PasswordId { get; set; }

        public string Hash { get; set; }
        public string Salt { get; set; }
        public Member Member { get; set; }

        public void SetPassword(string password)
        {
            Salt = PasswordHelper.CreateSalt();
            Hash = PasswordHelper.CreatePasswordHash(password, Salt);
        }
    }
}
