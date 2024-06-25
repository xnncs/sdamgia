using Api.Contracts.Requests;
using Application.Abstract;
using Application.Dto;
using AutoMapper;

namespace Api.Endpoints;

public static class PostEndpoints
{
    public static IEndpointRouteBuilder MapPostEndpoints(this IEndpointRouteBuilder app)
    {
        IEndpointRouteBuilder endpoints = app.MapGroup("api/school/posts");
        
        
        // POST: api/school/posts/create
        endpoints.MapPost("create", CreatePostAsync);

        // PUT: api/school/posts/update
        endpoints.MapPut("update", UpdatePostAsync);

        // DELETE: api/school/posts/delete/{postId}
        endpoints.MapDelete("delete/{postId}", DeletePostAsync);
        
        
        return endpoints;
    }
    
    private static async Task<IResult> CreatePostAsync(CreatePostRequest request, IAuthorizationService authorizationService,
        HttpContext context, ISchoolService schoolService, IMapper mapper)
    {
        int userId = authorizationService.GetUserIdFromJwt(context);
        CreatePostRequestDto contract = GenerateCreatePostRequestDtoObject(request, userId, mapper);

        await schoolService.CreatePost(contract);

        return TypedResults.Ok();
    }

    private static async Task<IResult> UpdatePostAsync(EditPostRequest request, IAuthorizationService authorizationService,
        HttpContext context, ISchoolService schoolService, IMapper mapper)
    {
        int userId = authorizationService.GetUserIdFromJwt(context);
        EditPostRequestDto contract = GenerateEditPostRequestDtoObject(request, userId, mapper);

        await schoolService.UpdatePost(contract);

        return TypedResults.Ok();
    }
    
    private static async Task<IResult> DeletePostAsync(int postId, IAuthorizationService authorizationService,
        HttpContext context, ISchoolService schoolService)
    {
        int userId = authorizationService.GetUserIdFromJwt(context);
        
        DeletePostRequestDto contract = DeletePostRequestDto.Create(
            postId: postId,
            userId: userId);

        await schoolService.DeletePost(contract);

        return TypedResults.Ok();
    }
    
    private static CreatePostRequestDto GenerateCreatePostRequestDtoObject(CreatePostRequest request, int userId,
        IMapper mapper)
    {
        CreatePostRequestDto contract = mapper.Map<CreatePostRequest, CreatePostRequestDto>(request);
        contract.UserId = userId;

        return contract;
    }
    
    private static EditPostRequestDto GenerateEditPostRequestDtoObject(EditPostRequest request, int userId,
        IMapper mapper)
    {
        EditPostRequestDto contract = mapper.Map<EditPostRequest, EditPostRequestDto>(request);
        contract.UserId = userId;

        return contract;
    }
}