using System;
using System.Collections.Generic;
using System.Reflection.Metadata;

namespace KeyPathWebApplication.Models;

public partial class HeroModel
{

    public int IdHero { get; set; }

    public string Name { get; set; }

    public string? Gender { get; set; }

    public string? EyeColor { get; set; }

    public string? HairColor { get; set; }

    public double? Height { get; set; }

    public string? Publisher { get; set; }

    public string? Alignment { get; set; }

    public long? Weight { get; set; }
}
