using static DC.AnimalChipization.Application.Features.Areas.Validators.Helpers.AreaValidationHelper;

namespace DC.AnimalChipization.Application.Tests
{
    public class AreaValidationHelpersTest : AreaValidationHelpersTestBase
    {
        [TestCase(ValidPolygonIndex, false)]
        [TestCase(LinePolygon1Index, true)]
        [TestCase(LinePolygon2Index, true)]
        public void PolygonIsLineTests(int polygonIndex, bool result)
        {
            var polygon = Polygons[polygonIndex];

            Assert.That(IsLine(polygon), Is.EqualTo(result));
        }

        [TestCase(ValidPolygonIndex, false)]
        [TestCase(PolygonWithIntersectedEdges1Index, true)]
        [TestCase(PolygonWithIntersectedEdges2Index, true)]
        [TestCase(PolygonWithIntersectedEdges3Index, true)]
        public void PolygonHasIntersectedEdgesTests(int polygonIndex, bool result)
        {
            var polygon = Polygons[polygonIndex];

            Assert.That(AreEdgesIntersecting(polygon), Is.EqualTo(result));
        }

        [TestCase(ValidPolygonIndex, PolygonWithIntersectedEdges1Index, false)]
        [TestCase(PolygonDuplicate1Index, PolygonDuplicate2Index, true)]
        public void PolygonsDuplicateTests(int polygonIndex1, int polygonIndex2, bool result)
        {
            var polygon1 = Polygons[polygonIndex1];
            var polygon2 = Polygons[polygonIndex2];

            Assert.That(IsPolygonsDuplicate(polygon1, polygon2), Is.EqualTo(result));
        }

        [TestCase(ValidPolygonIndex, NotIntersectedPolygon1Index, false)]
        [TestCase(ValidPolygonIndex, NotIntersectedPolygon2Index, false)]
        [TestCase(NotIntersectedPolygon1Index, NotIntersectedPolygon2Index, false)]
        [TestCase(IntersectedPolygon1Index, IntersectedPolygon2Index, true)]
        public void PolygonsIntersectedTests(int polygonIndex1, int polygonIndex2, bool result)
        {
            var polygon1 = Polygons[polygonIndex1];
            var polygon2 = Polygons[polygonIndex2];

            Assert.That(ArePolygonsIntersecting(polygon1, polygon2), Is.EqualTo(result));
        }

        [TestCase(OuterPolygonIndex, InnerPolygon1Index, true)]
        [TestCase(OuterPolygonIndex, InnerPolygon2Index, true)]
        [TestCase(ValidPolygonIndex, NotIntersectedPolygon1Index, false)]
        public void PolygonsContainmentTests(int polygonIndex1, int polygonIndex2, bool result)
        {
            var polygon1 = Polygons[polygonIndex1];
            var polygon2 = Polygons[polygonIndex2];

            Assert.That(AreContainedPolygons(polygon1, polygon2), Is.EqualTo(result));
        }
    }
}