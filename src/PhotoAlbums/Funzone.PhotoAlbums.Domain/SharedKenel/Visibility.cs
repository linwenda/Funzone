﻿using Funzone.BuildingBlocks.Domain;

namespace Funzone.PhotoAlbums.Domain.SharedKenel
{
    public class Visibility : ValueObject
    {
        public static Visibility Public => new Visibility(nameof(Public));
        public static Visibility Private => new Visibility(nameof(Private));

        public Visibility(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public Visibility MakePublic()
        {
            return Public;
        }

        public Visibility MakePrivate()
        {
            return Private;
        }
    }
}