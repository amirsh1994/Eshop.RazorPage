namespace Eshop.RazorPage.Models.Products;

public class ProductDto:BaseDto
{
    public string Title { get; set; }

    public string ImageName { get; set; }

    public string Description { get; set; }

    public ProductCategoryDto Category { get; set; }

    public ProductCategoryDto SubCategory { get; set; }

    public ProductCategoryDto? SecondarySubCategory { get; set; }//Level 3 

    public string Slug { get; set; }

    public SeoData SeoData { get; set; }

    public List<ProductImageDto> ImagesDtos { get; set; }

    public List<ProductSpecificationDto> SpecificationsDtos { get; set; }

}

public class SingleProductDto
{
    public ProductDto ProductDto { get; set; }

    public List<InventoryDto> Inventories { get; set; }
}

public class InventoryDto : BaseDto
{
    public long SellerId { get; set; }

    public string ShopName { get; set; }

    public long ProductId { get; set; }

    public string ProductTitle { get; set; }

    public string ProductImage { get; set; }

    public int Count { get; set; }

    public int Price { get; set; }

    public int DiscountPercentage { get; set; }

    public int TotalPrice
    {
        get
        {
            var total = Price;
            if (DiscountPercentage <= 0) return total;
            var discount = DiscountPercentage * Price / 100;
            total -= discount;
            return total;
        }
    }
}

public class ProductCategoryDto
{
    public long Id { get; set; }

    public long? ParentId { get; set; }

    public string Title { get; set; }

    public string Slug { get; set; }

    public SeoData SeoData { get; set; }
}

public class ProductImageDto : BaseDto
{
    public long ProductId { get; set; }

    public string ImageName { get; set; }

    public int Sequence { get; set; }
}

public class ProductSpecificationDto : BaseDto
{
    public string Key { get; set; }

    public string Value { get; set; }
}

public class ProductFilterResult:BaseFilter<ProductFilterData, ProductFilterParams>
{

}

public class ProductFilterData:BaseDto
{
    public string Title { get; set; }

    public string ImageName { get; set; }

    public string Slug { get; set; }
}

public class ProductFilterParams:BaseFilterParam
{
    public string? Title { get; set; }

    public long? Id { get; set; }

    public string? Slug { get; set; }

}