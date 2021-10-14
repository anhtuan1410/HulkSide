using BenchmarkDotNet.Attributes;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace HulkSide.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CodeFastSkillController : ControllerBase
    {
        Random _random = new Random();

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
            })
            .ToArray();
        }

        [Route("GetSample")]
        [HttpPost]
        [Benchmark]
        public object GetSample()
        {
            HashSet<ObjectHashSet> _hs = new HashSet<ObjectHashSet>();
            List<ObjectHashSet> _lst = new List<ObjectHashSet>();

            for (int i = 0; i < 100_000; i++)
            {
                var _obj = new ObjectHashSet
                {
                    id = i,
                    name = RandomString(i / 5),
                    address = RandomString(i / 6),
                };
                if(!_hs.Contains(_obj))
                {
                    _hs.Add(_obj);
                    _lst.Add(_obj);
                }
            }
            Stopwatch p = Stopwatch.StartNew();
            p.Start();
            var _fin1 = _hs.Where(x => !string.IsNullOrEmpty(x.name) && x.id % 2 == 0);
            p.Stop();
            TimeSpan ts1 = p.Elapsed;

            p.Start();
            var _fin2 = _lst.Where(x => !string.IsNullOrEmpty(x.name) && x.id % 2 == 0);
            p.Stop();
            TimeSpan ts2 = p.Elapsed;

            var _fin = _Find(_hs, _lst, 1);

            return new { 
                //HS = _hs,
                TS1 = ts1,
                TS2 = ts2
            };
        }

        [Route("SortListDictionary")]
        [HttpPost]
        [Benchmark]
        public object SortListDictionary()
        {
            SortedDictionary<string, ObjectHashSet> v_dicSort = new SortedDictionary<string, ObjectHashSet>();
            SortedList<string, ObjectHashSet> v_listSort = new SortedList<string, ObjectHashSet>();
            SortedSet<ObjectHashSet> v_sortSet = new SortedSet<ObjectHashSet>();
            
            for (int i = 0; i < 20_000; i++)
            {
                var _obj = new ObjectHashSet
                {
                    id = i,
                    name = RandomString(i / 5),
                    address = RandomString(i / 6),
                };

                if(!v_dicSort.ContainsKey(_obj.id.ToString()))
                {
                    v_dicSort.Add(_obj.id.ToString(), _obj);
                    v_listSort.Add(_obj.id.ToString(), _obj);
                    v_sortSet.Add(_obj);
                }

                
            }

            bool _insertAt = false;

            Stopwatch p = Stopwatch.StartNew();
            p.Start();
            int _i = 0;
            while (_i < 10 && _insertAt)
            {
                _i++;
                v_dicSort["0"] = new ObjectHashSet { id = 197234, name = "Name_SortDic_"+_i, address = "HCM" };
            }
            var _fin1 = v_dicSort.Values.Where(x => !string.IsNullOrEmpty(x.name) && x.id*-1 % 2 == 0);            
            p.Stop();
            TimeSpan ts1 = p.Elapsed;




            Stopwatch p1 = Stopwatch.StartNew();
            p1.Start();
            _i = 0;
            while (_i < 10 && _insertAt)
            {
                _i++;
                v_listSort["0"] = new ObjectHashSet { id = 197234, name = "Name_SortList_" + _i, address = "HCM" };
            }
            var _fin2 = v_listSort.Values.Where(x => !string.IsNullOrEmpty(x.name) && x.id * -1 % 2 == 1);
            p1.Stop();
            TimeSpan ts2 = p1.Elapsed;




            Stopwatch p2 = Stopwatch.StartNew();
            p2.Start();
            _i = 0;
            while (_i < 10 && _insertAt)
            {
                _i++;
                ObjectHashSet _p = v_sortSet.ElementAt(5);
                _p = new ObjectHashSet { id = 197234, name = "Name_SortedSet_" + _i, address = "HN" };
            }
            var _fin3 = v_sortSet.Where(x => !string.IsNullOrEmpty(x.name) && x.id * -1 % 2 == 1);
            p2.Stop();
            TimeSpan ts3 = p2.Elapsed;

            return new
            {
                //HS = _hs,
                TS1 = ts1,
                TS2 = ts2,
                TS3 = ts3
            };
        }

        [Route("LocalFuntion")]
        [HttpPost]
        [Benchmark]
        public async ValueTask<object> LocalFuntion()
        {

            SortedList<int, int> _hs = new SortedList<int, int>();
            int _count = 0;
            while(_count < 10_000)
            {
                _hs.Add(_count,_count++);
            }

            TimeSpan getSum(SortedList<int, int> _hsPs)
            {
                Stopwatch p = Stopwatch.StartNew();
                p.Start();

                int _ind = 0, _sum =0;
                if(_hsPs != null)
                {
                    int _nextind = _ind + 1;
                    if(_nextind < _hsPs.Count)
                    {
                        _hs[_ind] = _hs[_ind] + _hs[_nextind];
                    }
                    _hsPs.Values.ToList().ForEach(x => _sum += x);
                    p.Stop();
                    return p.Elapsed;
                }
                return new TimeSpan();
            }

            return new
            {
                SumLocalFunction = getSum(_hs)
            };

        }

        [Route("LoadTupple")]
        [HttpPost]
        [Benchmark]
        public async Task<object> LoadTupple()
        {

            Tuple<int, int, bool, string, List<string>, double, int, Tuple<int>> _tuple =
                new Tuple<int, int, bool, string, List<string>, double, int, Tuple<int>>
                (1, 2, false, "Hello tupple", new List<string> { "AAA", "BBB" }, 35, 78, new Tuple<int>(577) );
            double _refTime = -1;
            if(_tuple.Item6 is double p)
            {
                _refTime = p;
            }

            return new
            {
                Tuple = _tuple,
                ChkItem1 = _tuple.Item1 is int,
                RefVar = _refTime
            };

        }

        [Benchmark]
        private object _Find(HashSet<ObjectHashSet> _hs, List<ObjectHashSet> _lst, int p)
        {
            var _fin1 = p == 1 ? _hs.Where(x => !string.IsNullOrEmpty(x.name)) : 
                _lst.Where(x => !string.IsNullOrEmpty(x.name));
            return _fin1;
        }

        string RandomString(int length)
        {            
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[_random.Next(s.Length)]).ToArray());
        }


    }

    class ObjectHashSet : IComparable
    {
        public int id { get; set; }
        public string name { get; set; }
        public string address { get; set; }

        public int CompareTo(object obj)
        {
            return 42;
        }
    }

   



}

