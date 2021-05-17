using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Async_Training
{
    public class Async
    {
        public async void AsyncMethod()
        {
            for(int i = 0; i < 10; i++)
            {
                Thread.Sleep(1000);
                Console.WriteLine($"In Async Method: {i}");
            }
        }

        private async void ThrowExceptionAsync()
        {
            throw new InvalidOperationException();
        }

        public void AsyncVoidExceptionsCannotBeCaughtByCatch()
        {
            try
            {
                ThrowExceptionAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task TaskRunMethod()
        {
            Console.WriteLine("Started in Task Run Method!");
            await Task.Run(() =>
            {
                for(int i = 0; i < 10; i++)
                {
                    Thread.Sleep(1000);
                    Console.WriteLine($"In Task Run: {i}");
                }
            });

            Console.WriteLine("Finished in Task Run Method!");
        }

        public async Task ContinueWithMethod(CancellationToken source)
        {

            var taskMethod = Task.Run(async () =>
            {
                for(int i = 0; i < 2; i++)
                {
                    await Task.Delay(1000, source);
                    Console.WriteLine($"In Task Run: {i}");
                }
                //throw new Exception();
            });

            var s = taskMethod.ContinueWith(t =>
            {
                Console.WriteLine("We have a problem in original Task");
            }, TaskContinuationOptions.OnlyOnFaulted);

            var continueWithTask = taskMethod.ContinueWith(async t =>
            {
                
                for(int i = 0; i < 2; i++)
                {
                    await Task.Delay(1000, source); //Почему не работает? разобрался.  https://stackoverflow.com/questions/58855484/async-in-task-continuewith-behavior
                    //Thread.Sleep(1000);
                    Console.WriteLine($"In Task Continuation: {i}");
                }
            }, TaskContinuationOptions.OnlyOnRanToCompletion);
            
            await continueWithTask.ContinueWith(t =>
            {
                Thread.Sleep(5000);
                Console.WriteLine("Finished Task Continuation");
            }, TaskContinuationOptions.OnlyOnRanToCompletion);

        }
    }
}
