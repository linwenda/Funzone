﻿using System;
using Funzone.Domain.SeedWork;

namespace Funzone.Domain.PageAggregate
{
    public class Block : Entity
    {
        public Guid Id { get; private set; }
        public string Text { get; private set; }
        public BlockType Type { get; private set; }

        public Block(string text, BlockType type)
        {
            Text = text;
            Type = type;
        }
    }
}