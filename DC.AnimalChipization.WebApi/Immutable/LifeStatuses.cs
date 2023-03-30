using System;
using System.Collections.Generic;
using DC.AnimalChipization.Application.Features.Animals.Enums;

namespace DC.AnimalChipization.WebApi.Immutable;

public static class LifeStatuses
{
    public const string Alive = "ALIVE";
    public const string Dead = "DEAD";

    public static readonly IReadOnlyList<string> All = new[]
    {
        Alive, Dead
    };

    public static LifeStatus ConvertToEnum(string lifeStatus) => lifeStatus switch
    {
        Alive => LifeStatus.Alive,
        Dead  => LifeStatus.Dead,
        _     => throw new ArgumentOutOfRangeException(nameof(lifeStatus))
    };

    public static string ConvertToString(LifeStatus lifeStatus) => lifeStatus switch
    {
        LifeStatus.Alive => Alive,
        LifeStatus.Dead  => Dead,
        _                => throw new ArgumentOutOfRangeException(nameof(lifeStatus))
    };
}