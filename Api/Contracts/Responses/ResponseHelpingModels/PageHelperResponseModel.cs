using Core.Models;

namespace Api.Contracts.Responses.ResponseHelpingModels;

public record PageHelperResponseModel
{
    public int? Id { get; set; } = null;
    public List<Post> Posts { get; set; } = Enumerable.Empty<Post>().ToList();
}