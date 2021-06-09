using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Funzone.Domain.SeedWork
{
    public abstract class ValueObject : IEquatable<ValueObject>
    {
        private List<PropertyInfo> _properties;

        private List<FieldInfo> _fields;

        public static bool operator ==(ValueObject obj1, ValueObject obj2)
        {
            return obj1?.Equals(obj2) ?? Equals(obj2, null);
        }

        public static bool operator !=(ValueObject obj1, ValueObject obj2)
        {
            return !(obj1 == obj2);
        }

        public bool Equals(ValueObject obj)
        {
            return Equals(obj as object);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            return GetProperties().All(p => PropertiesAreEqual(obj, p))
                   && GetFields().All(f => FieldsAreEqual(obj, f));
        }

        public override int GetHashCode()
        {
            var hash = GetProperties().Select(prop => prop.GetValue(this, null))
                .Aggregate(17, HashValue);

            return GetFields().Select(field => field.GetValue(this)).Aggregate(hash, HashValue);
        }

        private bool PropertiesAreEqual(object obj, PropertyInfo p)
        {
            return Equals(p.GetValue(this, null), p.GetValue(obj, null));
        }

        private bool FieldsAreEqual(object obj, FieldInfo f)
        {
            return Equals(f.GetValue(this), f.GetValue(obj));
        }

        private IEnumerable<PropertyInfo> GetProperties()
        {
            return _properties ??= GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(p => p.GetCustomAttribute(typeof(IgnoreMemberAttribute)) == null)
                .ToList();
        }

        private IEnumerable<FieldInfo> GetFields()
        {
            return _fields ??= GetType()
                .GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                .Where(p => p.GetCustomAttribute(typeof(IgnoreMemberAttribute)) == null)
                .ToList();
        }

        private static int HashValue(int seed, object value)
        {
            var currentHash = value?.GetHashCode() ?? 0;
            return seed * 23 + currentHash;
        }
    }
}