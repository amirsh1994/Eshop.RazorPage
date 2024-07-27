namespace Eshop.RazorPage.Models.Sellers;

public class SellerDto:BaseDto
{
    public long UserId { get; set; }

    public string ShopName { get; set; }

    public string NationalCode { get; set; }

    public SellerStatus Status { get; set; }
}

public class SellerFilterParams:BaseFilterParam
{
    public string ShopName { get; set; }

    public string NationalCode { get; set; }
}

public class SellerFilterResult:BaseFilter<SellerDto,SellerFilterParams>
{

}


public enum SellerStatus
{
    Accepted,
    Active,
    Inactive,
    New,
    Rejected
}