using Eshop.RazorPage.Infrastructure;
using Eshop.RazorPage.Models;
using Eshop.RazorPage.Models.Comments;

namespace Eshop.RazorPage.Services.Comments;

public interface ICommentService
{
    Task<CommentFilterResult?> GetCommentsByFilter(CommentFilterParam filter);

    Task<CommentFilterResult?> GetProductComments(int pageId, int take, long productId);

    Task<CommentDto?> GetCommentById(long commentId);

    Task<ApiResult?> AddComment(CreateCommentCommand command);

    Task<ApiResult?> EditComment(EditCommentCommand command);

    Task<ApiResult?> ChangeStatus(long commentId, CommentStatus status);

}

public class CommentService(HttpClient client) : ICommentService
{
    public async Task<CommentFilterResult?> GetCommentsByFilter(CommentFilterParam filter)
    {
        var url = filter.GenerateBaseFilterUrl("comment");
        if (filter.UserId != null)
            url += $"&UserId={filter.UserId}";

        if (filter.Status != null)
            url += $"&Status={filter.Status}";

        if (filter.EndDate != null)
            url += $"&EndDate={filter.EndDate}";
        var result = await client.GetFromJsonAsync<ApiResult<CommentFilterResult>>(url);
        return result.Data;
    }

    public async Task<CommentFilterResult?> GetProductComments(int pageId = 1, int take = 10, long productId = 0)
    {
        var url = $"comment/productComments?pageId={pageId}&take={take}&productId={productId}";
        var result = await client.GetFromJsonAsync<ApiResult<CommentFilterResult>>(url);
        return result.Data;
    }

    public async Task<CommentDto?> GetCommentById(long commentId)
    {
        var result = await client.GetFromJsonAsync<ApiResult<CommentDto>>($"comment/{commentId}");
        return result?.Data;
    }

    public async Task<ApiResult?> AddComment(CreateCommentCommand command)
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