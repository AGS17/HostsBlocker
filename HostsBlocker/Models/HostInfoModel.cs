namespace HostsBlocker.Models
{
    public class HostInfoModel
    {
        public string Comment { get; set; }
        public string Target { get; set; }
        public string RedirectTo { get; set; }
        public bool IsBlocking { get; set; }

        public string Title => !string.IsNullOrWhiteSpace(this.Comment) ? this.Comment : this.Target;

        public HostInfoModel() { }

        public HostInfoModel(string comment, string target, string redirectTo, bool isBlocking)
        {
            this.Comment = comment;
            this.Target = target;
            this.RedirectTo = redirectTo;
            this.IsBlocking = isBlocking;
        }
    }
}
