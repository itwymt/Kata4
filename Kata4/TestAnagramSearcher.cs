using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;
using FluentAssertions;

namespace Kata4
{
    public class testDictionaryReader
    {
        [Fact]
        public void test_finding_string()
        {
            var dictionaryReader = new DictionaryReader("c:\\dicttest.txt");

            var words = dictionaryReader.GetDictionary();

            words.Should().BeEquivalentTo(new List<string> {"123", "132", "1234", "1456", "abcd", "window", "store", "hello"});

        }

        [Fact]
        public void test_test_non_existent_file()
        {
            var dictionaryReader = new DictionaryReader("c:\\111");

            var words = dictionaryReader.GetDictionary();

            words.Should().BeEmpty();

        }
    }

    public class TestAnagramSearcher
    {
        [Fact]
        public void test_two_letter_word()
        {
            var dictionaryReader = Substitute.For<IDictionaryReader>();

            dictionaryReader.GetDictionary().Returns(new List<string>() { "abcd", "123", "478878" });

            var anagramS = new AnagramSearcher(dictionaryReader);

            var l = anagramS.Transform("bcda");

            l.Should().BeEquivalentTo(new List<string>() { "abcd" });
        }

        [Fact]
        public void test_wrong_anagram()
        {
            var dictionaryReader = Substitute.For<IDictionaryReader>();

            dictionaryReader.GetDictionary().Returns(new List<string>());

            var anagramS = new AnagramSearcher(dictionaryReader);

            var l = anagramS.Transform("abcd");

            l.Should().BeEmpty();
        }

        [Fact]
        public void test_all_anagrams()
        {
            var dictionaryReader = Substitute.For<IDictionaryReader>();

            dictionaryReader.GetDictionary().Returns(new List<string> { "123", "213", "231", "321", "312", "132", "abcd", "ppp" });

            var anagramS = new AnagramSearcher(dictionaryReader);

            var l = anagramS.Transform("123");

            l.Should().BeEquivalentTo(new List<string> { "123", "213", "231", "321", "312", "132" });

        }

        [Fact]
        public void test_all_anagrams_big()
        {
            var dictionaryReader = Substitute.For<IDictionaryReader>();

            dictionaryReader.GetDictionary().Returns(new List<string>{"akljhlk", "vo;o"});

            var anagramS = new AnagramSearcher(dictionaryReader);

            var l = anagramS.Transform("123456fgfjhk");

            l.Should().BeEmpty();
        }

        [Fact]
        public void test_all_anagrams_four_letters()
        {
            var dictionaryReader = Substitute.For<IDictionaryReader>();

            dictionaryReader.GetDictionary().Returns( new List<string>{"fgd", "dfsd", "4312"});
            
            var anagramS = new AnagramSearcher(dictionaryReader);

            var l = anagramS.Transform("1234");

            l.Should().BeEquivalentTo(new List<string> {"4312"});

        }

        [Fact]
        public void test_two_letter_word_fact_dict()
        {
            var dictionaryReader = new DictionaryReader();

            var anagramS = new AnagramSearcher(dictionaryReader);

            var l = anagramS.Transform("bca");

            l.Should().BeEquivalentTo(new List<string>() { "abc", "bca" });

        }

        [Fact]
        public void test_two_letter_word_fact_dict_b()
        {
            var dictionaryReader = new DictionaryReader();

            var anagramS = new AnagramSearcher(dictionaryReader);

            var l = anagramS.Transform("1234");

            l.Should().BeEquivalentTo(new List<string>{"1234","1243","1324","1342","1423","1432","2134","2143","2314","2341","2431","2413","4123","4132","4213","4231","4312","4321","3124","3142","3214","3241","3412","3421" });

        }
    }

}
