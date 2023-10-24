﻿using static System.String;

namespace TaskPlusPlus.Application.Helpers;

public class TaskQueryParameters
{
    private string _search = Empty;
    public ulong CategoryId { get; set; }
    public bool SortDescending { get; set; } = false;
    public string SortBy { get; set; } = Empty;
    public string GroupBy { get; set; } = Empty;

    //public DateTime DueDate { get; set; }
    //public DateTime DataAdded { get; set; }
    //public TimeOnly DurationTime { get; set; }
    //public string Priority { get; set; } = Empty;
    //public string Energy { get; set; } = Empty;

    public string Search
    {
        get => _search;
        set => _search = value.ToLower();
    }
}