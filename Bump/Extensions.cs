using System;

namespace Bump
{
    public static class Extensions
    {
        public static void Run<T>(this T subject, Action<T> block)
        {
            block(subject);
        }

        public static void RunNullable<T>(this T subject, Action<T> block, Action onNull)
        {
            if (subject != null)
            {
                block(subject);
            }
            else
            {
                onNull();
            }
        }
    }
}