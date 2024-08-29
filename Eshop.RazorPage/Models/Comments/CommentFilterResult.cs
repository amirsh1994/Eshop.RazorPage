namespace Eshop.RazorPage.Models.Comments;

public class CommentFilterResult:BaseFilter<CommentDto, CommentFilterParam>
{

}

public class CommentDto : BaseDto
{
    public long ProductID { get; set; }

    public string ProductTitle { get; set; }

    public string Text { get; set; }

    public CommentStatus Status { get; set; }

    public long UserId { get; set; }

    public string UserFullName { get; set; }
}

public class CommentFilterParam : BaseFilterParam
{
    public long? UserId { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public CommentStatus? Status { get; set; }
}

public enum CommentStatus
{

    Rejected,
    Pending,
    Accepted

}
