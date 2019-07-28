namespace EBuy.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Message
    {
        public string Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        [MaxLength(40)]
        public string Email { get; set; }

        [Required]
        [MaxLength(20)]
        public string Subject { get; set; }

        [Required]
        [MaxLength(500)]
        public string Content { get; set; }

        public bool isActive { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public DateTime SubmissionDate { get; set; }

        public DateTime? ReplyDate { get; set; }

        public string ReplierId { get; set; }
        public User Replier { get; set; }
    }
}
