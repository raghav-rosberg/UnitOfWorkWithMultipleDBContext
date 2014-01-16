using System;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace UoW_MultipleDBContext.Entity
{
    public class User : BaseEntity<int>
    {
        public User()
        {
            DateCreated = DateTime.Now;
        }

        [Key]
        public int UserId { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string PasswordHash { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? LastLoginTime { get; set; }
        public bool Activated { get; set; }     
        public int RoleId { get; set; }

        public string Password;
        //{
        //    set
        //    {
        //        PasswordHash = Md5Encrypt.Md5EncryptPassword(value);
        //    }
        //}       
        internal static string GenerateRandomString()
        {
            var builder = new StringBuilder();
            var random = new Random();
            for (int i = 0; i < 6; i++)
            {
                char ch = Convert.ToChar(Convert.ToInt32(Math.Floor(25 * random.NextDouble() + 75)));
                builder.Append(ch);
            }
            return builder.ToString();
        }

        public string ResetPassword()
        {
            var newPass = GenerateRandomString();
            Password = newPass;
            return newPass;
        }
        public string DisplayName
        {
            get { return FirstName; }
        }
    }
}
