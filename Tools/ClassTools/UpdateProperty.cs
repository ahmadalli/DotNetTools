using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ahmadalli.Tools.ClassTools
{
    public class UpdatePropertyTool
    {
        /// <summary>
        /// Compares two object's specified property and updates outOfDated if neccessary
        /// </summary>
        /// <typeparam name="TSource">Type of objects</typeparam>
        /// <param name="outOfDated">Object that is going to be updated if neccessary</param>
        /// <param name="updateData">The data that will be used for updating</param>
        /// <param name="property">The property that is goign to be updated in outOfDate object</param>
        public static void UpdateProperty<TSource>(TSource outOfDated, TSource updateData, Expression<Func<TSource, object>> property)
        {
            var propertyInfo = GetPropertyInfo(outOfDated, property);

            if (!(propertyInfo.GetValue(outOfDated).Equals(propertyInfo.GetValue(updateData))))
            {
                propertyInfo.SetValue(outOfDated, propertyInfo.GetValue(updateData));
            }
        }

        /// <summary>
        /// Returns PropertyInfo of TSource's property that specified by lambda expression
        /// </summary>
        /// <typeparam name="TSource">Type of object</typeparam>
        /// <typeparam name="TProperty">Type of preoperty</typeparam>
        /// <param name="source">Source object</param>
        /// <param name="propertyLambda">Lambda expression that specifies the property</param>
        /// <returns>PropertyInfo of the property</returns>
        private static PropertyInfo GetPropertyInfo<TSource, TProperty>(TSource source, Expression<Func<TSource, TProperty>> propertyLambda)
        {
            Type type = typeof(TSource);

            MemberExpression member = propertyLambda.Body as MemberExpression;
            if (member == null)
                throw new ArgumentException(string.Format(
                    "Expression '{0}' refers to a method, not a property.",
                    propertyLambda.ToString()));

            PropertyInfo propInfo = member.Member as PropertyInfo;
            if (propInfo == null)
                throw new ArgumentException(string.Format(
                    "Expression '{0}' refers to a field, not a property.",
                    propertyLambda.ToString()));

            if (type != propInfo.ReflectedType &&
                !type.IsSubclassOf(propInfo.ReflectedType))
                throw new ArgumentException(string.Format(
                    "Expresion '{0}' refers to a property that is not from type {1}.",
                    propertyLambda.ToString(),
                    type));


            return propInfo;
        }
    }
}
