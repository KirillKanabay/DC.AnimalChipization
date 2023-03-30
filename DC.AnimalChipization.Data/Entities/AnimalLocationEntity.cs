﻿using DC.AnimalChipization.Data.Common.Entities;

namespace DC.AnimalChipization.Data.Entities;

public class AnimalLocationEntity : EntityBase
{
    public long Id { get; set; }
    public DateTime VisitDateTime { get; set; }
    public long LocationPointId { get; set; }
    public long AnimalId { get; set; }

    public AnimalEntity Animal { get; set; }
    public LocationEntity Location { get; set; }
}