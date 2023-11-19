namespace WebSite.Models.NovaPoshta
{
    public class NovaPoshtaResponse
    {
        public bool success { get; set; }
        public List<NovaPoshtaData> data { get; set; }
    }
    public class NovaPoshtaResponseWarehouse
    {
        public bool success { get; set; }
        public List<NovaPoshtaDataWarehouse> data { get; set; } // Зміни List<NovaPoshtaDataWarehouse> на List<Warehouse>
        public List<string> errors { get; set; }
        public List<string> warnings { get; set; }
        public int totalCount { get; set; }
        public List<string> messageCodes { get; set; }
        public List<string> errorCodes { get; set; }
        public List<string> warningCodes { get; set; }
        public List<string> infoCodes { get; set; }
    }

    public class NovaPoshtaData
    {
        public string TotalCount { get; set; }
        public List<Settlement> Addresses { get; set; }
    }

    public class Settlement
    {
        public string TotalCount { get; set; }
        public List<Address> Addresses { get; set; }
        public string Warehouses { get; set; }
        public string MainDescription { get; set; }
        public string Area { get; set; }
        public string Region { get; set; }
        public string SettlementTypeCode { get; set; }
        public string Ref { get; set; }
        public string DeliveryCity { get; set; }
    }

    public class Address
    {
        public string Warehouses { get; set; }
        public string MainDescription { get; set; }
        public string Area { get; set; }
        public string Region { get; set; }
        public string SettlementTypeCode { get; set; }
        public string Ref { get; set; }
        public string DeliveryCity { get; set; }
    }

    public class NovaPoshtaDataWarehouse
    {
        public string SiteKey { get; set; }
        public string Description { get; set; }
        public string DescriptionRu { get; set; }
        public string ShortAddress { get; set; }
        public string ShortAddressRu { get; set; }
        public string Phone { get; set; }
        public string TypeOfWarehouse { get; set; }
        public string Ref { get; set; }
        public string Number { get; set; }
        public string CityRef { get; set; }
        public string CityDescription { get; set; }
        public string CityDescriptionRu { get; set; }
        public string SettlementRef { get; set; }
        public string SettlementDescription { get; set; }
        public string SettlementAreaDescription { get; set; }
        public string SettlementRegionsDescription { get; set; }
        public string SettlementTypeDescription { get; set; }
        public string SettlementTypeDescriptionRu { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public string PostFinance { get; set; }
        public string BicycleParking { get; set; }
        public string PaymentAccess { get; set; }
        public string POSTerminal { get; set; }
        public string InternationalShipping { get; set; }
        public string SelfServiceWorkplacesCount { get; set; }
        public string TotalMaxWeightAllowed { get; set; }
        public string PlaceMaxWeightAllowed { get; set; }
        public Dimensions SendingLimitationsOnDimensions { get; set; }
        public Dimensions ReceivingLimitationsOnDimensions { get; set; }
        public ReceptionInfo Reception { get; set; }
        public ReceptionInfo Delivery { get; set; }
        public ReceptionInfo Schedule { get; set; }
        public string DistrictCode { get; set; }
        public string WarehouseStatus { get; set; }
        public string WarehouseStatusDate { get; set; }
        public string WarehouseIllusha { get; set; }
        public string CategoryOfWarehouse { get; set; }
        public string RegionCity { get; set; }
        public string WarehouseForAgent { get; set; }
        public string GeneratorEnabled { get; set; }
        public string MaxDeclaredCost { get; set; }
        public string WorkInMobileAwis { get; set; }
        public string DenyToSelect { get; set; }
        public string CanGetMoneyTransfer { get; set; }
        public string HasMirror { get; set; }
        public string HasFittingRoom { get; set; }
        public string OnlyReceivingParcel { get; set; }
        public string PostMachineType { get; set; }
        public string PostalCodeUA { get; set; }
        public string WarehouseIndex { get; set; }
        public string BeaconCode { get; set; }
        public string PostomatFor { get; set; }
    }

    public class Dimensions
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public int Length { get; set; }
    }

    public class ReceptionInfo
    {
        public string Monday { get; set; }
        public string Tuesday { get; set; }
        public string Wednesday { get; set; }
        public string Thursday { get; set; }
        public string Friday { get; set; }
        public string Saturday { get; set; }
        public string Sunday { get; set; }
    }
}
