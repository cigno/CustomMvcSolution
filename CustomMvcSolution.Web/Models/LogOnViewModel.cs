using System.ComponentModel.DataAnnotations;

namespace CustomMvcSolution.Web.Models
{
    public class LogOnViewModel
    {
        public LogOnViewModel(){ }
        public LogOnViewModel(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }

        [Required(ErrorMessageResourceName = "YouMustSpecifyUsername", ErrorMessageResourceType = typeof(Resources.Resources))]
        [Display(Name="UserName", ResourceType = typeof(Resources.Resources))]
        public string UserName { get; set; }

        [Required(ErrorMessageResourceName = "YouMustSpecifyPassword", ErrorMessageResourceType = typeof(Resources.Resources))]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(Resources.Resources))]
        public string Password { get; set; }
    }
}