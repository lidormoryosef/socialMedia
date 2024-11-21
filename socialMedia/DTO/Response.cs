using Microsoft.AspNetCore.Http.HttpResults;

namespace socialMedia.Models;

public enum Response
{
    NotFound = -1,
    Excist = 0,
    Success = 1,
    Error = 2,
}