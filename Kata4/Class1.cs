using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Kata4
{
    public class DictionaryReader : IDictionaryReader
    {
        private readonly IEnumerable<string> _dictionarylist;
        private readonly string DictionaryPath = "e:\\dict.txt";

        public DictionaryReader()
        {
            _dictionarylist = ReadFromFile(DictionaryPath);
        }

        public DictionaryReader(string path)
        {
            DictionaryPath = path;
            _dictionarylist = ReadFromFile(DictionaryPath);
        }

        public IEnumerable<string> FindAll(string word)
        {
            return _dictionarylist.Where(s => new string(s.OrderBy(c => c).ToArray()) == new string(word.OrderBy(c => c).ToArray())).ToList();
        }

        private IEnumerable<string> ReadFromFile(string fileName)
        {
            if (File.Exists(fileName))
            {
                var sm = new StreamReader(fileName);
                var sr = sm.ReadToEndAsync().Result.Split(new [] {'\r', '\n'});
                return sr.ToList();
            }
            return new List<string>();
        }
    }

    public class AnagramSearcher
    {
        private readonly IDictionaryReader _dictionaryReader;

        public AnagramSearcher(IDictionaryReader dictionaryReader)
        {
            _dictionaryReader = dictionaryReader;
        }

        public IEnumerable<string> Transform(string word)
        {
            return _dictionaryReader.FindAll(word);
        }
    }
}