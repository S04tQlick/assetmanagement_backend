namespace AssetManagement.Entities.Enums;

public enum AssetTypeEnum
{
    ItEquipment,
    Furniture,
    Vehicles,
    Machinery,
    SoftwareLicenses,
    RealEstate,
    MedicalEquipment
}

public static class AssetCategoryMap
{
    public static readonly Dictionary<AssetTypeEnum, string[]> Categories = new()
    {
        [AssetTypeEnum.ItEquipment] = ["Laptop", "Desktop", "Monitor", "Printer"],
        [AssetTypeEnum.Furniture] = ["Chair", "Desk", "Cabinet", "Table"],
        [AssetTypeEnum.Vehicles] = ["Car", "Van", "Motorbike", "Truck"],
        [AssetTypeEnum.Machinery] = ["Power Tool", "Heavy Equipment", "Manufacturing Machine"],
        [AssetTypeEnum.SoftwareLicenses] = ["Productivity Suite", "Dev Tool", "Security Software"],
        [AssetTypeEnum.RealEstate] = ["Office Building", "Warehouse", "Retail Space"],
        [AssetTypeEnum.MedicalEquipment] = ["Diagnostic Device", "Surgical Tool", "Patient Monitor"]
    };
}