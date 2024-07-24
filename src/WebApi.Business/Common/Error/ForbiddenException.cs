﻿namespace WebApi.Business.Common.Error;

public sealed class ForbiddenException : Exception
{
    public ForbiddenException()
    {
    }

    public ForbiddenException(string? message) : base(message)
    {
    }

    public ForbiddenException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}