using Api.Contracts.Requests.Post;
using Application.Abstract;
using Application.Dto.Post;
using AutoMapper;

namespace Api.Endpoints.DataEndpoints;

public static class PostEndpoints
{
    public static IEndpointRouteBuilder MapPostEndpoints(this IEndpointRouteBuilder app)
    {
        IEndpointRouteBuilder endpoints = app.MapGroup("api/schools/posts");
        
        
        // POST: api/schools/posts/create
        endpoints.MapPost("create", CreatePostAsync);

        // PUT: api/schools/posts/update
        endpoints.MapPut("update", UpdatePostAsync);

        // DELETE: api/schools/posts/delete/{postId}
        endpoints.MapDelete("delete/{postId}", DeletePostAsync);
        
        
        return endpoints;
    }
    
    private static async Task<IResult> CreatePostAsync(CreatePostRequest request, IAuthorizationService authorizationService,
        HttpContext context, ISchoolService schoolService, IMapper mapper)
    {
        int userId = authorizationService.GetUserIdFromJwt(context);
        CreatePostDto contract = GenerateCreatePostRequestDtoObject(request, userId, mapper);

        await schoolService.CreatePost(contract);

        return TypedResults.Ok();
    }

    private static async Task<IResult> UpdatePostAsync(EditPostRequest request, IAuthorizationService authorizationService,
        HttpContext context, ISchoolService schoolService, IMapper mapper)
    {
        int userId = authorizationService.GetUserIdFromJwt(context);
        EditPostDto contract = GenerateEditPostRequestDtoObject(request, userId, mapper);

        await schoolService.UpdatePost(contract);

        return TypedResults.Ok();
    }
    
    private static async Task<IResult> DeletePostAsync(int postId, IAuthorizationService authorizationService,
        HttpContext context, ISchoolService schoolService)
    {
        int userId = authorizationService.GetUserIdFromJwt(context);
        
        DeletePostDto contract = DeletePostDto.Create(
            postId: postId,
            userId: userId);

        await schoolService.DeletePost(contract);

        return TypedResults.Ok();
    }
    
    private static CreatePostDto GenerateCreatePostRequestDtoObject(CreatePostRequest request, int userId,
        IMapper mapper)
    {
        CreatePostDto contract = mapper.Map<CreatePostRequest, CreatePostDto>(request);
        contract.UserId = userId;

        return contract;
    }
    
    private static EditPostDto GenerateEditPostRequestDtoObject(EditPostRequest request, int userId,
        IMapper mapper)
    {
        EditPostDto contract = mapper.Map<EditPostRequest, EditPostDto>(request);
        contract.UserId = userId;

        return contract;
    }
}