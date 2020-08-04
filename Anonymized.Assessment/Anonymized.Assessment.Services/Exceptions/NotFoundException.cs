using System;
using System.Linq.Expressions;

namespace Anonymized.Assessment.Services.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(Type type, string keyName, object key)
            : base($"Could not find a {type.Name} with {keyName} {key}")
        {
            Type = type;
            KeyName = keyName;
            Key = key;
        }

        /// <summary>
        /// Gets the type of entity that does not exist.
        /// </summary>
        public Type Type { get; }

        /// <summary>
        /// Gets the name of the key that was used to access the entity.
        /// </summary>
        public string KeyName { get; }

        /// <summary>
        /// Gets the key value.
        /// </summary>
        public object Key { get; }

        /// <summary>
        /// Raise an exception for the given arguments.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity that was not found.</typeparam>
        /// <param name="property">The property used to search for the entity..</param>
        /// <param name="key">The value of the <paramref name="property"/>.</param>
        /// <returns></returns>
        public static NotFoundException Raise<TEntity>(Expression<Func<TEntity, object>> property, object key)
        {
            var member = property.Body as MemberExpression ?? throw new ArgumentException("not a member expression", nameof(property));
            return new NotFoundException(typeof(TEntity), member.Member.Name, key);
        }
    }
}