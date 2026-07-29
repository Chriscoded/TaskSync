using MediatR;
using TaskSync.Application.DTOs;

namespace TaskSync.Application.Features.Projects.Queries.GetProjects;

public sealed record GetProjectsQuery
    : IRequest<List<ProjectDto>>;