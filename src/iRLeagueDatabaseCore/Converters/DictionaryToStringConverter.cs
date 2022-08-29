using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace iRLeagueDatabaseCore.Converters
{
    public class DictionaryToStringConverter<TKey, TValue> : ValueConverter<IDictionary<TKey, TValue>, string>
    {
        public DictionaryToStringConverter(char pairDelimiter = ';', char valueDelimiter = ':') : 
            base(ConvertToString(pairDelimiter, valueDelimiter), ConvertToDictionary(pairDelimiter, valueDelimiter))
        {
        }

        private static Expression<Func<IDictionary<TKey, TValue>, string>> ConvertToString(char pairDelimiter, char valueDelimiter) =>
            dict => string.Join(pairDelimiter, dict
                .Select(x => string.Join(valueDelimiter, x.Key, x.Value)));

        private static Expression<Func<string, IDictionary<TKey, TValue>>> ConvertToDictionary(char pairDelimiter, char valueDelimiter) =>
            str => str.Split(pairDelimiter, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => str.Split(valueDelimiter, StringSplitOptions.None))
                .ToDictionary(x => (TKey)Convert.ChangeType(x[0], typeof(TKey)), x => (TValue)Convert.ChangeType(x[1], typeof(TValue)));
    }
}
