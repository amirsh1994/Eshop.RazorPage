namespace Eshop.RazorPage.Models.ShippingMethods;

public class ShippingMethodDto : BaseDto
{
    public string Title { get; set; }

    public int Cost { get; set; }
}

public class CreateShippingMethodeCommand
{
    public string Title { get; set; }

    public int Cost { get; set; }
}

public class EditShippingMethodeCommand
{
    public long Id { get; set; }

    public string Title { get; set; }

    public int Cost { get; set; }
}