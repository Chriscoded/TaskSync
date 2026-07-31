using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskSync.Application.Features.Comments.Commands.AddComment;
using TaskSync.Application.Features.Comments.Queries.GetComments;

namespace TaskSync.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/tasks/{taskId:guid}/comments")]
public sealed class CommentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public CommentsController(
        IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IResult> Get(Guid taskId)
    {
        return Results.Ok(
            await _mediator.Send(
                new GetCommentsQuery(taskId)));
    }

    [HttpPost]
    public async Task<IResult> Create(
        Guid taskId,
        AddCommentRequest request)
    {
        var id = await _mediator.Send(
            new AddCommentCommand(
                taskId,
                request.Text));

        return Results.Created(
            $"/api/comments/{id}",
            id);
    }
}

public sealed record AddCommentRequest(
    string Text);