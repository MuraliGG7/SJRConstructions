namespace SJRConstructions.Web.ViewModels
{
    public class ProjectDetailsVM
    {
        public int ProjectId { get; set; }
        public int StatusId { get; set; }
        public int Status { get; set; }
        public string ProjectName { get; set; }
        public string Address1 { get; set; }
        public string? Address2 { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string ZIPCode { get; set; }
        public string OwnerName { get; set; }
        public string OwnerContact { get; set; }
        public bool Active { get; set; }
    }
}
