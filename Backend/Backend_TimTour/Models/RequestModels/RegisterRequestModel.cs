﻿using System.ComponentModel.DataAnnotations;

namespace Backend_TimTour.Models.RequestModels
{
    public class RegisterRequestModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Type { get; set; }
    }
}
