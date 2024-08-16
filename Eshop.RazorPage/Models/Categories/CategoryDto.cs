namespace Eshop.RazorPage.Models.Categories;

public class CategoryDto : BaseDto
{
    public string Title { get; set; }

    public string Slug { get; set; }

    public SeoData SeoData { get; set; }

    public List<ChildCategoryDto> Children { get; set; }
}
public class ChildCategoryDto : BaseDto
{
    public string Title { get; set; }

    public string Slug { get; set; }

    public SeoData SeoData { get; set; }

    public long ParentId { get; set; }

    public List<SecondaryChildCategoryDto> Children { get; set; }
}
public class SecondaryChildCategoryDto : BaseDto
{
    public string Title { get; set; }

    public string Slug { get; set; }

    public SeoData SeoData { get; set; }

    public long ParentId { get; set; }


}

public class CreateCategoryCommand
{
    public string Title { get; set; }

    public SeoData SeoData { get; set; }

    public string Slug { get; set; }
}

public class EditCategoryCommand
{
    public long Id { get; set; }

    public string Title { get; set; }

    public string Slug { get; set; }

    public SeoData SeoData { get; set; }
}

public class AddChildCommand
{
    public long ParentId { get; set; }

    public string Title { get; set; }

    public SeoData SeoData { get; set; }

    public string Slug { get; set; }
}

