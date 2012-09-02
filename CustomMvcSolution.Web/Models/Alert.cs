using System;
using System.Text;

namespace CustomMvcSolution.Web.Models
{
    public enum AlertType
    {
        Error=0,
        Warning,
        Success,
        Info
    }
    public class Alert
    {
        public Alert()
        {
        }

        public Alert(AlertType type, string title, string description)
        {
            Type = type;
            Title = title;
            Description = description;
        }

        public Alert(Exception e)
        {
            var msgDescription = new StringBuilder();
            msgDescription.AppendLine(e.Message);
            msgDescription.AppendLine(e.StackTrace);

            Type = AlertType.Error;
            Title = Resources.Resources.AnErrorOccurred;
            Description = msgDescription.ToString();
        }

        public AlertType Type { get; set; }
        public string CssClass
        {
            get { return GetClass(Type); }
        }

        public string Title { get; set; }
        public string Description { get; set; }

        private string GetClass(AlertType type)
        {
            string[] classes = new[] {"Error", "Warning", "Success", "Info"};

            return  classes[(int)type];
        }
    }
}