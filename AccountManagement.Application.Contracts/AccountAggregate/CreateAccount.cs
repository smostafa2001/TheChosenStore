using AccountManagement.Application.Contracts.RoleAggregate;
using Framework.Application;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AccountManagement.Application.Contracts.AccountAggregate
{
    public class RegisterAccount
    {
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Fullname { get; set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Username { get; set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Password { get; set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string RePassword { get; set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Mobile { get; set; }
        public long RoleId { get; set; }
        public IFormFile ProfilePhoto { get; set; }
        public List<RoleViewModel> Roles { get; set; }
    }
}
