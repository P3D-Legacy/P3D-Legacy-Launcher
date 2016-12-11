using System;
using System.Collections.Generic;

namespace P3D.Legacy.Launcher.Extensions
{
    // https://github.com/OpenRA/OpenRA/blob/master/OpenRA.Game/Exts.cs
    public static class IEnumerableExtensions
    {
        public static T MaxByOrDefault<T, U>(this IEnumerable<T> ts, Func<T, U> selector) => ts.CompareBy(selector, -1, false);

        private static T CompareBy<T, U>(this IEnumerable<T> ts, Func<T, U> selector, int modifier, bool throws)
        {
            var comparer = Comparer<U>.Default;
            T t;
            U u;
            using (var e = ts.GetEnumerator())
            {
                if (!e.MoveNext())
                    if (throws)
                        throw new ArgumentException(@"Collection must not be empty.", nameof(ts));
                    else
                        return default(T);
                t = e.Current;
                u = selector(t);
                while (e.MoveNext())
                {
                    var nextT = e.Current;
                    var nextU = selector(nextT);
                    if (comparer.Compare(nextU, u) * modifier < 0)
                    {
                        t = nextT;
                        u = nextU;
                    }
                }

                return t;
            }
        }
    }
}