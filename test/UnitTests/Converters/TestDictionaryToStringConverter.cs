using iRLeagueDatabaseCore.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Converters
{
    public class TestDictionaryToStringConverter
    {
        private const char pairDelimiter = ';';
        private const char valueDelimiter = ':';

        [Fact]
        public void ShouldConvertIntToString()
        {
            var testDict = new Dictionary<string, int>() { { "val1", 1 }, { "val2", 2 } };
            var expected = "val1:1;val2:2";
            AssertConvertToString(testDict, expected);
        }

        private static void AssertConvertToString<TKey, TValue>(IDictionary<TKey, TValue> dict, string expected)
        {
            var converter = new DictionaryToStringConverter<TKey, TValue>();
            var result = (string)converter.ConvertToProvider(dict);
            Assert.Equal(expected, result);
        }
    }
}
