using System;

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
    }
}