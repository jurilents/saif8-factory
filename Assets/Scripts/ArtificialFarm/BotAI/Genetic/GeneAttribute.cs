using System;

namespace ArtificialFarm.BotAI.Genetic
{
    [AttributeUsage(AttributeTargets.Method)]
    public class GeneAttribute : Attribute
    {
        public string Name { get; }
        public string Group { get; }

        public GeneAttribute(string group, string name = "")
        {
            Name = string.IsNullOrWhiteSpace(name) ? group : name;
            Group = group;
        }
    }
}