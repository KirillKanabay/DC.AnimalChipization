using System;

namespace DC.AnimalChipization.WebApi.ViewModels.Areas.Requests;

public class AreaAnalyticRequest
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}