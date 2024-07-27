namespace Eshop.RazorPage.Models.Products.Commands;

public class CreateProductCommand
{
    public string Title { get;  set; }

    public IFormFile ImageFile { get; set; }

    public string Description { get; set; }

    public long CategoryId { get; set; }

    public long SubCategoryId { get; set; }

    public long ?FirstSubCategoryId { get; set; }//Level 3 

    public string Slug { get; set; }

    public SeoData SeoData { get; set; }

    public Dictionary<string, string> Specifications { get; set; }
}

public class EditProductCommand
{
    public long Id { get; set; }

    public string Title { get; set; }

    public IFormFile? ImageFile { get; set; }

    public string Description { get; set; }

    public long CategoryId { get; set; }

    public long SubCategoryId { get; set; }

    public long ? FirstSubCategoryId { get; set; }//Level 3 

    public string Slug { get; set; }

    public SeoData SeoData { get; set; }

    public Dictionary<string, string> Specifications { get; set; }
}

public class AddProductImageCommand
{
    public IFormFile ImageFile { get; set; }

    public long ProductId { get; set; }

    public int Sequence { get; set; }

}

public class DeleteProductImageCommand
{
    public long ImageId { get; set; }

    public long ProductId { get; set; }
}