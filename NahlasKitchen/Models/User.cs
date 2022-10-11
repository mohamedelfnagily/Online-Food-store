using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NahlasKitchen.Models
{
    public class User
    {
        public User()
        {
            carts = new HashSet<Cart>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        [Required]
        [StringLength(20,ErrorMessage ="Maximum length 20 characters")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(20, ErrorMessage = "Maximum length 20 characters")]
        public string LastName { get; set; }

        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$" , ErrorMessage ="Invalid Email format")]
        [Remote("checkEmail","User",ErrorMessage ="Email already Exists",AdditionalFields ="id,Register",HttpMethod = "get")]
        public string Email { get; set; }
        [Required]
        [StringLength(20,ErrorMessage ="Maximum length 20 characters")]
        public string UserName { get; set; }
        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",ErrorMessage ="Invalid Format")]
        public string Password { get; set; }
        [NotMapped]
        [Compare("Password",ErrorMessage ="Passwords don't match")]
        public string ConfirmPassword { get; set; }
        [Required]
        [StringLength(11)]
        [RegularExpression("^[0125][0-9]{8}$" , ErrorMessage ="Egyptian numbers only")]
        public string mobileNumber { get; set; }
        public string Role { get; set; }
        public virtual ICollection<Cart> carts { get; set; }
        public string Image { get; set; }

    }
}
