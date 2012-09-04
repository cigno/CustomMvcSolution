using System.ComponentModel.DataAnnotations;

using CustomMvcSolution.Resource;

namespace CustomMvcSolution.WebUI.Models
{
    public class LogOnViewModel
    {
        public LogOnViewModel(){ }
        public LogOnViewModel(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }

        [Required(ErrorMessageResourceName = "YouMustSpecifyUsername", ErrorMessageResourceType = typeof(Resources))]
        [Display(Name="UserName", ResourceType = typeof(Resources))]
        public string UserName { get; set; }

        [Required(ErrorMessageResourceName = "YouMustSpecifyPassword", ErrorMessageResourceType = typeof(Resources))]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(Resources))]
        public string Password { get; set; }
    }
}