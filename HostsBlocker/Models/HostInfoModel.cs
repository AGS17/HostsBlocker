namespace HostsBlocker.Models
{
    public class HostInfoModel
    {
        public bool IsSelected { get; set; }

        private string title;
        public string Title
        {
            get { return this.title ?? this.Target; }
            set { this.title = value; }
        }

        public string Target { get; set; }
        public string RedirectTo { get; set; }
        public bool IsBlocking { get; set; }
        
        public HostInfoModel() { }

        public HostInfoModel(string title, string target, string redirectTo, bool isBlocking)
        {
            this.IsSelected = false;
            this.Title = title;
            this.Target = target;
            this.RedirectTo = redirectTo;
            this.IsBlocking = isBlocking;
        }

        public HostInfoModel(HostInfoModel hostInfo)
        {
            if (hostInfo == null)
            {
                return;
            }
            this.IsSelected = hostInfo.IsSelected;
            this.Title = hostInfo.Title;
            this.Target = hostInfo.Target;
            this.RedirectTo = hostInfo.RedirectTo;
            this.IsBlocking = hostInfo.IsBlocking;
        }
    }
}
