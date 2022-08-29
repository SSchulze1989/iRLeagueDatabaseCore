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
    public class CollectionToStringConverter<T> : ValueConverter<ICollection<T>, string>
    {
        public CollectionToStringConverter(CultureInfo culture, char delimiter = ';') : 
            base(ConvertToString(delimiter, culture), ConvertToArray(delimiter, culture))
        {
        }

        public CollectionToStringConverter(char delimiter = ';') : this(CultureInfo.InvariantCulture, delimiter)
        {
        }

        private static Expression<Func<ICollection<T>, string>> ConvertToString(char delimiter, CultureInfo culture) =>
            array => string.Join(delimiter, array.Select(x => Convert.ToString(x, culture)));

        private static Expression<Func<string, ICollection<T>>> ConvertToArray(char delimiter, CultureInfo culture) => 
            str => str.Split(delimiter, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => (T)Convert.ChangeType(x, typeof(T), CultureInfo.InvariantCulture))
                .ToList();
    }
}
