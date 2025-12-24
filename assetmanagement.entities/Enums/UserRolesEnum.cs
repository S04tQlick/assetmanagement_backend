namespace AssetManagement.Entities.Enums;

public enum UserRolesEnum
{
    //// System Administrator (Super Admin)
    //// Full access to all system features and data.
    //// Manages user accounts, roles, and permissions.
    //// Configures system settings, integrations, and security policies.
    SystemAdmin,
    
    //// Institution/Branch Administrator
    //// Manages assets tied to a specific institution or branch.
    //// Handles asset requests within their institution.
    //// Generates localized reports (usage, inventory, depreciation).
    BranchAdmin,
   
    //// Asset Manager
    //// Oversees all assets across institutions/branches.
    //// Creates, updates, and retires asset records.
    //// Assigns assets to departments or users.
    //// Approves asset transfers, disposals, or procurement.
    AssetManager,
    
    //// Department Manager
    //// Manages assets within their department/team.
    //// Approves requests from staff.
    //// Oversees asset allocation and returns.
    DepartmentManager,
    
    //// Procurement Officer
    //// Requests new assets based on organizational needs.
    //// Manages supplier/vendor interactions.
    //// Approves or rejects purchase requisitions.
    ProcurementOfficer,
    
    //// Inventory/Store Officer
    //// Tracks assets in stock or storage.
    //// Logs asset issuance, transfers, and returns.
    //// Maintains asset condition and tagging.
    InventoryOfficer,
    
    //// Maintenance Officer / Technician
    //// Handles asset servicing, maintenance, and repairs.
    //// Logs maintenance history and schedules.
    //// Flags assets for replacement/disposal.
    MaintenanceOfficer,
    
    //// Employee / Asset User
    //// Uses assets assigned to them.
    //// Can request new assets or report damaged/lost ones.
    //// Responsible for returning or transferring assets.
    Employee,
    
    //// Auditor
    //// Has read-only access to asset records.
    //// Reviews compliance, audit trails, and reports.
    //// Ensures accountability and transparency.
    Auditor,
    
    //// Finance Officer
    //// Monitors asset depreciation, valuation, and budgeting.
    //// Reviews cost vs. ROI of assets.
    //// Approves financial aspects of procurement/disposal.
    FinanceOfficer,
    
    //// Security Officer
    //// Ensures assets with sensitive data are protected.
    //// Manages access control policies for IT/Software assets.
    //// Investigates asset-related incidents.
    SecurityOfficer
}




// "Administrator",
// "Manager",
// "Supervisor",
// "Coordinator",
// "Analyst",
// "Auditor",
// "Technician",
// "Operator",
// "Consultant",
// "Assistant"