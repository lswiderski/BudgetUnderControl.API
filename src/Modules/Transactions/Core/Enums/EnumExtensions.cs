using BudgetUnderControl.Common.Attributes;
using BudgetUnderControl.Common.Enums;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace BudgetUnderControl.Common.Extensions
{
    public static class EnumExtensions
    {

        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetStringValue(this Enum value)
        {
            // Get the type
            Type type = value.GetType();

            // Get fieldinfo for this type
            FieldInfo fieldInfo = type.GetField(value.ToString());

            // Get the stringvalue attributes
            StringValueAttribute[] attribs = fieldInfo.GetCustomAttributes(
                typeof(StringValueAttribute), false) as StringValueAttribute[];

            // Return the first if there was a match.
            return attribs.Length > 0 ? attribs[0].StringValue : null;
        }

        public static T GetEnumValue<T>(this string value) where T : struct
        {
            T result;
            Enum.TryParse(value, out result);
            return result;
        }

        public static TransactionType ToTransactionType(this ExtendedTransactionType value)
        {
            switch (value)
            {
                case ExtendedTransactionType.Income:
                    return TransactionType.Income;
                case ExtendedTransactionType.Expense:
                    return TransactionType.Expense;
                default:
                    throw new ArgumentException();
            }
        }

        public static ExtendedTransactionType ToExtendedTransactionType(this TransactionType value)
        {
            switch (value)
            {
                case TransactionType.Income:
                    return ExtendedTransactionType.Income;
                case TransactionType.Expense:
                    return ExtendedTransactionType.Expense;
                default:
                    throw new ArgumentException();
            }
        }
    }
}
