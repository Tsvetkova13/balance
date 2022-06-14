using System;

namespace BalaceController.Models
{
    public class ArrowOutput
    {
        int id;
        String name;
        double reconcliedValue;

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public double ReconcliedValue { get => reconcliedValue; set => reconcliedValue = value; }
    }
}
