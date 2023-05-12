using DC.AnimalChipization.Application.Features.Areas.DataTransfer;

namespace DC.AnimalChipization.Application.Tests;

public class AreaValidationHelpersTestBase
{
    protected const int ValidPolygonIndex = 0;
    protected const int LinePolygon1Index = 1;
    protected const int LinePolygon2Index = 2;
    protected const int PolygonWithIntersectedEdges1Index = 3;
    protected const int PolygonWithIntersectedEdges2Index = 4;
    protected const int PolygonDuplicate1Index = 5;
    protected const int PolygonDuplicate2Index = 6;
    protected const int IntersectedPolygon1Index = 7;
    protected const int IntersectedPolygon2Index = 8;
    protected const int NotIntersectedPolygon1Index = 9;
    protected const int NotIntersectedPolygon2Index = 10;
    protected const int OuterPolygonIndex = 11;
    protected const int InnerPolygon1Index = 12;
    protected const int InnerPolygon2Index = 13;
    protected const int PolygonWithIntersectedEdges3Index = 14;

    protected Dictionary<int, List<AreaPointDto>> Polygons => new()
    {
        #region Valid

        [ValidPolygonIndex] = new List<AreaPointDto>
        {
            new(){Latitude = 1, Longitude = 1},
            new(){Latitude = 1, Longitude = 4},
            new(){Latitude = 5, Longitude = 5},
            new(){Latitude = 5, Longitude = 1},
        },

        #endregion

        #region Is Line

        [LinePolygon1Index] = new List<AreaPointDto>
        {
            new (){Longitude = 1, Latitude = 2},
            new (){Longitude = 4, Latitude = 2},
            new (){Longitude = 5, Latitude = 2},
            new (){Longitude = 2, Latitude = 2},
        },
        [LinePolygon2Index] = new List<AreaPointDto>
        {
            new (){Longitude = 2, Latitude = 1},
            new (){Longitude = 2, Latitude = 4},
            new (){Longitude = 2, Latitude = 5},
            new (){Longitude = 2, Latitude = 2},
        },

        #endregion

        #region With Intersected Edges

        [PolygonWithIntersectedEdges1Index] = new List<AreaPointDto>
        {
            new (){Longitude = 1, Latitude = 1},
            new (){Longitude = 3, Latitude = 3},
            new (){Longitude = 5, Latitude = 1},
            new (){Longitude = 1, Latitude = 3},
            new (){Longitude = 2, Latitude = 1},
        },
        [PolygonWithIntersectedEdges2Index] = new List<AreaPointDto>
        {
            new (){Longitude = 1, Latitude = 1},
            new (){Longitude = 3, Latitude = 3},
            new (){Longitude = 5, Latitude = 1},
            new (){Longitude = 1, Latitude = 3},
        },
        [PolygonWithIntersectedEdges3Index] = new List<AreaPointDto>
        {
            new (){Longitude = -29, Latitude = -179},
            new (){Longitude = -29, Latitude = -166},
            new (){Longitude = -16, Latitude = -180},
            new (){Longitude = -16, Latitude = -166},
        },

        #endregion

        #region Duplicated

        [PolygonDuplicate1Index] = new List<AreaPointDto>
        {
            new(){Latitude = 1, Longitude = 1},
            new(){Latitude = 1, Longitude = 4},
            new(){Latitude = 5, Longitude = 5},
            new(){Latitude = 5, Longitude = 1},
        },
        [PolygonDuplicate2Index] = new List<AreaPointDto>
        {
            new(){Latitude = 5, Longitude = 5},
            new(){Latitude = 5, Longitude = 1},
            new(){Latitude = 1, Longitude = 1},
            new(){Latitude = 1, Longitude = 4},
        },

        #endregion

        #region Intersected Polygons

        [IntersectedPolygon1Index] = new List<AreaPointDto>
        {
            new(){Latitude = 1, Longitude = 1},
            new(){Latitude = 4, Longitude = 4},
            new(){Latitude = 7, Longitude = 4},
            new(){Latitude = 7, Longitude = 1},
        },
        [IntersectedPolygon2Index] = new List<AreaPointDto>
        {
            new(){Latitude = 1, Longitude = 2},
            new(){Latitude = 1, Longitude = 5},
            new(){Latitude = 4, Longitude = 5},
            new(){Latitude = 4, Longitude = 2},
        },

        #endregion

        #region Not Intersected Polygons

        [NotIntersectedPolygon1Index] = new List<AreaPointDto>
        {
            new(){Latitude = 5, Longitude = 6},
            new(){Latitude = 5, Longitude = 8},
            new(){Latitude = 7, Longitude = 8},
            new(){Latitude = 7, Longitude = 6},
        },
        [NotIntersectedPolygon2Index] = new List<AreaPointDto>
        {
            new(){Latitude = 7, Longitude = 8},
            new(){Latitude = 7, Longitude = 6},
            new(){Latitude = 9, Longitude = 6},
            new(){Latitude = 9, Longitude = 8},
        },

        #endregion

        #region Containment

        [OuterPolygonIndex] = new List<AreaPointDto>
        {
            new(){Latitude = 1, Longitude = 1},
            new(){Latitude = 1, Longitude = 3},
            new(){Latitude = 4, Longitude = 5},
            new(){Latitude = 4, Longitude = 1},
        },
        [InnerPolygon1Index] = new List<AreaPointDto>
        {
            new(){Latitude = 2, Longitude = 2},
            new(){Latitude = 2, Longitude = 3},
            new(){Latitude = 3, Longitude = 3},
            new(){Latitude = 3, Longitude = 2},
        },
        [InnerPolygon2Index] = new List<AreaPointDto>
        {
            new(){Latitude = 1, Longitude = 3},
            new(){Latitude = 4, Longitude = 5},
            new(){Latitude = 4, Longitude = 5},
            new(){Latitude = 2, Longitude = 2},
        },
        #endregion
    };
}