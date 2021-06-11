using System;
using Funzone.Domain.SeedWork;

namespace Funzone.Domain.PageAggregate
{
    public class Block : Entity
    {
        public Guid Id { get; private set; }
        public DateTime CreatedTime { get; private set; }
        public string Text { get; private set; }
        public BlockType Type { get; private set; }
        public bool IsRemoved { get; private set; }

        public Block(
            string text,
            BlockType type)
        {
            Id = Guid.NewGuid();
            CreatedTime = DateTime.Now;
            Text = text;
            Type = type;
        }

        internal void Edit(string text, bool isRemoved)
        {
            Text = text;
            IsRemoved = isRemoved;
        }
    }
}