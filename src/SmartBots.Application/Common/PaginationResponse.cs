﻿namespace SmartBots.Application.Common;
public sealed class PaginationResponse<TResult>(List<TResult> items, int total) where TResult : BaseDto
{
    public List<TResult> Items { get; init; } = items;
    public int Total { get; init; } = total;
}