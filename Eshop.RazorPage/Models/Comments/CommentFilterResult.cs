namespace Eshop.RazorPage.Models.Comments;

public class CommentFilterResult:BaseFilter<CommentDto,CommentsFilterParam>
{

}

public class CommentDto:BaseDto
{
    public int ProductId { get; set; }

    public string ProductTitle { get; set; }

    public string Text { get; set; }

    public int Status { get; set; }

    public int UserId { get; set; }

    public string UserFullName { get; set; }

}

public class CommentsFilterParam:BaseFilterParam
{
    public int UserId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public int Status { get; set; }

}

public class CreateCommentCommand
{
    public string Text { get; set; }

    public int UserId { get; set; }

    public int ProductId { get; set; }
}
public class EditCommentCommand
{
    public long CommentId { get; set; }

    public string Text { get; set; }

    public int UserId { get; set; }

}
public enum CommentStatus
{

    Rejected,
    Pending,
    Accepted

}
