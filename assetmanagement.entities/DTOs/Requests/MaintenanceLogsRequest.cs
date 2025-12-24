using AssetManagement.Entities.Models;

namespace AssetManagement.Entities.DTOs.Requests;

public class MaintenanceLogsRequest
{
    public class MaintenanceLogsCreateRequest :  BaseModel
    {
        [Required]
        public Guid AssetId { get; set; }

        [Required]
        public Guid InstitutionId { get; set; }

        [Required]
        public decimal Cost { get; set; }

        [Required]
        public DateTime MaintenanceDate { get; set; }

        [Required, MaxLength(200)]
        public string PerformedBy { get; set; } = string.Empty;

        [Required, MaxLength(500)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public DateTime NextDueDate { get; set; }
    }
    
    public class MaintenanceLogUpdateRequest
    {  
        [Required]
        public Guid AssetId { get; set; }

        [Required]
        public Guid InstitutionId { get; set; }

        [Required]
        public decimal Cost { get; set; }

        [Required]
        public DateTime MaintenanceDate { get; set; }

        [Required, MaxLength(200)]
        public string PerformedBy { get; set; } = string.Empty;

        [Required, MaxLength(500)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public DateTime NextDueDate { get; set; } 
    }
}