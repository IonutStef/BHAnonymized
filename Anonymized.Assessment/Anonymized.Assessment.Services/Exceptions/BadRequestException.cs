using System;
using System.Linq.Expressions;

namespace Anonymized.Assessment.Services.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException()
            : base($"Invalid request.")
        {
        }

        public BadRequestException(Type type)
            : base($"Invalid request.")
        {
            Type = type;
        }

        public BadRequestException(Type type, string keyName, object key)
            : base($"Invalid value {key} for field {keyName} of {type.Name}")
        {
            Type = type;
            KeyName = keyName;
            Key = key;
        }

        /// <summary>
        /// Gets the type of entity that triggered the exception.
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
        /// <typeparam name="TEntity">Type of entity that triggered the exception.</typeparam>
        /// <param name="property">The property that triggered the exception.</param>
        /// <param name="key">The value of the <paramref name="property"/>.</param>
        /// <returns></returns>
        public static BadRequestException Raise<TEntity>(Expression<Func<TEntity, object>> property, object key)
        {
            if (property.Body is MemberExpression)
            {
                var member = (MemberExpression)property.Body;
                return new BadRequestException(typeof(TEntity), member.Member.Name, key);
            }

            else if(property.Body is UnaryExpression)
            {
                var member = (UnaryExpression)property.Body;
                return new BadRequestException(typeof(TEntity), ((dynamic)member.Operand).Member.Name, key);
            }

            throw new ArgumentException("Is not a valid expression", nameof(property));
        }

        /// <summary>
        /// Raise a generic exception.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity that triggered the exception</typeparam>
        /// <returns></returns>
        public static BadRequestException Raise<TEntity>()
        {
            return new BadRequestException(typeof(TEntity));
        }
    }
}
