namespace EBuy.Web.Models.Contacts
{
    using System.ComponentModel.DataAnnotations;

    public class MessageInputModel
    {
        [Required(ErrorMessage = "Please enter your first and last name.")]
        [StringLength(30, ErrorMessage = "Name limit is 30 symbols.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter a valid email address.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        [StringLength(40, ErrorMessage = "Email limit is 40 symbols.")]
        public string Email { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Subject limit is 20 symbols.")]
        public string Subject { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "Message limit is {2}-{1} symbols.", MinimumLength = 20)]
        public string Content { get; set; }
    }
}
