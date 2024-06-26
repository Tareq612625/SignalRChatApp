﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SignalRChatApp.Models
{
    public class ChatMessage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Sender is required")]
        [StringLength(100, ErrorMessage = "Sender name cannot be longer than 100 characters")]
        public string Sender { get; set; }

        [Required(ErrorMessage = "Receiver is required")]
        [StringLength(100, ErrorMessage = "Receiver name cannot be longer than 100 characters")]
        public string Receiver { get; set; }

        [Required(ErrorMessage = "Receiver is required")]
        public string Message { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.Now;
        public Boolean IsRead { get; set; } = false;
    }
}
