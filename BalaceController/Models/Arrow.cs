using System;

namespace BalaceController.Models
{
    public class Arrow
    {
        int id;
        String name;
        int? sourceId;
        int? destinationId;
        double tolerance;
        double measured;
        double min, max;

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public int? DestinationId { get => destinationId; set => destinationId = value; }
        public double Tolerance { get => tolerance; set => tolerance = value; }
        public double Measured { get => measured; set => measured = value; }
        public int? SourceId { get => sourceId; set => sourceId = value; }
        public double Min { get => min; set => min = value; }
        public double Max { get => max; set => max = value; }
    }
}
