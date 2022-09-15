﻿using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace iRLeagueDatabaseCore.Converters
{
    public class DictionaryToStringConverter<TKey, TValue> : ValueConverter<IDictionary<TKey, TValue>, string>
    {
        public DictionaryToStringConverter(CultureInfo culture, char pairDelimiter = ';', char valueDelimiter = ':') : 
            base(ConvertToString(pairDelimiter, valueDelimiter, culture), ConvertToDictionary(pairDelimiter, valueDelimiter, culture))
        {
        }

        public DictionaryToStringConverter(char pairDelimiter = ';', char valueDelimiter = ':') :
            this(CultureInfo.InvariantCulture, pairDelimiter, valueDelimiter)
        {
        }

        private static Expression<Func<IDictionary<TKey, TValue>, string>> ConvertToString(char pairDelimiter, char valueDelimiter, CultureInfo culture) =>
            dict => string.Join(pairDelimiter, dict
                .Select(x => string.Join(valueDelimiter, Convert.ToString(x.Key, culture), Convert.ToString(x.Value, culture))));

        private static Expression<Func<string, IDictionary<TKey, TValue>>> ConvertToDictionary(char pairDelimiter, char valueDelimiter, CultureInfo culture) =>
            str => str.Split(pairDelimiter, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Split(valueDelimiter, StringSplitOptions.None))
                .ToDictionary(x => ChangeType<TKey>(x[0], culture), x => ChangeType<TValue>(x[1], culture));

        private static T ChangeType<T>(string value, CultureInfo culture)
        {
            if (typeof(T).IsEnum)
            {
                return (T)Enum.Parse(typeof(T), value);
            }
            return (T)Convert.ChangeType(value, typeof(T), culture);
        }
    }
}
