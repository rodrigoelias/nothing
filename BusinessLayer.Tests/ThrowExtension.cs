using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace BusinessLayer.Tests
{
    public static class ShouldEx
    {
        public static async Task ThrowsAsync<TException>(Func<Task> func)
        {
            var expected = typeof(TException);
            Type actual = null;
            try
            {
                await func();
            }
            catch (Exception e)
            {
                actual = e.GetType();
            }
            Assert.AreEqual(expected, actual);
        }
    }
}
