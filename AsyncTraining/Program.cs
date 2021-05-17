using System;
using System.Threading;
using System.Threading.Tasks;

namespace Async_Training
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Starting Main()");
            var async = new Async();

            //async.AsyncMethod();

            //async.AsyncVoidExceptionsCannotBeCaughtByCatch();

            //await async.TaskRunMethod();

            CancellationTokenSource source = new CancellationTokenSource();
            await async.ContinueWithMethod(source.Token);

            Console.WriteLine("Finished Main()");
        }

        void DoSyncWork()
        {

        }
    }
}
