using Eshop.RazorPage.Models;
using Eshop.RazorPage.Models.Comments;

namespace Eshop.RazorPage.Services.Comments;

public interface ICommentService
{
    Task<CommentFilterResult?> GetCommentsByFilter(CommentsFilterParam filter);

    Task<CommentDto?> GetCommentById(long commentId);

    Task<ApiResult?> CreateComment(CreateCommentCommand command);

    Task<ApiResult?> EditComment(EditCommentCommand command);

    Task<ApiResult?> ChangeStatus(long commentId, CommentStatus status);






}

public class CommentService(HttpClient client) : ICommentService
{
    public async Task<CommentFilterResult?> GetCommentsByFilter(CommentsFilterParam filter)
    {
        var result = await client.GetFromJsonAsync<ApiResult<CommentFilterResult>>($"comment?UserId={filter.UserId}&StartDate={filter.StartDate}EndDate={filter.EndDate}&Status={filter.Status}&PageId={filter.PageId}&Take={filter.Take}");
        var response = result?.Data;
        return response;
    }

    public async Task<CommentDto?> GetCommentById(long commentId)
    {
        var result = await client.GetFromJsonAsync<ApiResult<CommentDto>>($"comment/{commentId}");
        return result?.Data;
    }

    public async Task<ApiResult?> CreateComment(CreateCommentCommand command)
    {
        var result = await client.PostAsJsonAsync("comment", command);
        var response = await result.Content.ReadFromJsonAsync<ApiResult>();
        return response;
    }

    public async Task<ApiResult?> EditComment(EditCommentCommand command)
    {
        var result = await client.PutAsJsonAsync("comment", command);
        var response = await result.Content.ReadFromJsonAsync<ApiResult>();
        return response;

    }

    public async Task<ApiResult?> ChangeStatus(long commentId, CommentStatus status)
    {
        var result = await client.PutAsJsonAsync("comment/changeStatus", new { commentId, status });
        var response = await result.Content.ReadFromJsonAsync<ApiResult>();
        return response;
    }
}