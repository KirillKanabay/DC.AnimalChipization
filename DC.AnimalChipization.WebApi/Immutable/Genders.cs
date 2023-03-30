using System;
using System.Collections.Generic;
using DC.AnimalChipization.Application.Features.Animals.Enums;

namespace DC.AnimalChipization.WebApi.Immutable
{
    public static class Genders
    {
        public const string Male = "MALE";
        public const string Female = "FEMALE";
        public const string Other = "OTHER";

        public static readonly IReadOnlyList<string> All = new[]
        {
            Male, Female, Other
        };

        public static Gender ConvertToEnum(string gender) => gender switch
        {
            Male   => Gender.Male,
            Female => Gender.Female,
            Other  => Gender.Other,
            _      => throw new ArgumentOutOfRangeException(nameof(gender))
        };

        public static string ConvertToString(Gender gender) => gender switch
        {
            Gender.Male   => Male,
            Gender.Female => Female,
            Gender.Other  => Other,
            _             => throw new ArgumentOutOfRangeException(nameof(gender))
        };
    }
}
