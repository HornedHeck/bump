using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bump
{
    public static class Extensions
    {
        public static R Run<R>(Func<R> block)
        {
            return block();
        }

        public static R Run<T, R>(this T subject, Func<T, R> block)
        {
            return block(subject);
        }

        public static T Also<T>(this T subject, Action<T> block)
        {
            block(subject);
            return subject;
        }

        public static IEnumerable<T> ApplyToAll<T>(this IEnumerable<T> subject, Action<T> block) =>
            subject.Select(it =>
            {
                block.Invoke(it);
                return it;
            });

        public static string Format(this DateTime subject, string createdAt)
        {
            return string.Format(createdAt, subject.ToString("d"), subject.ToString("t"));
        }
    }
}