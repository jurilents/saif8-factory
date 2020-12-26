using System;

namespace ArtificialFarm.BotAI.Genetic
{
    public delegate void GeneAction(Bot bot);

    public struct Gene
    {
        private static ushort _summary;

        public ushort Id { get; }
        public bool Available { get; private set; }
        public string Group { get; }
        public string Name { get; }


        public GeneAction Apply { get; }


        public Gene(GeneAction foo, string group, string name)
        {
            Id = _summary++;
            Available = true;
            Group = group;
            Name = name;
            Apply = foo ?? throw new ArgumentNullException();
        }

        public void SetActive(bool state) => Available = state;

        public override string ToString() => $"{Id}#{Name}";
    }
}