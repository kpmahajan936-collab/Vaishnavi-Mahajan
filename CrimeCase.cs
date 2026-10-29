using System.ComponentModel.DataAnnotations;

namespace CrimeCaseManagement.Models;

public class CrimeCase
{
    public int Id { get; set; }

    [Required, StringLength(30)]
    [Display(Name = "Case Number")]
    public string CaseNumber { get; set; } = "";

    [Required, StringLength(100)]
    [Display(Name = "Case Title")]
    public string Title { get; set; } = "";

    [Required, StringLength(80)]
    [Display(Name = "Crime Type")]
    public string CrimeType { get; set; } = "";

    [Required, StringLength(100)]
    [Display(Name = "Location")]
    public string Location { get; set; } = "";

    [Required, DataType(DataType.Date)]
    [Display(Name = "Incident Date")]
    public DateTime IncidentDate { get; set; } = DateTime.Today;

    [Required, StringLength(100)]
    [Display(Name = "Investigating Officer")]
    public string OfficerName { get; set; } = "";

    [Required, StringLength(20)]
    public string Status { get; set; } = "Open";

    [StringLength(1000)]
    public string? Description { get; set; }
}
