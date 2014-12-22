using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication19
{
    public class ClassWithTread : IDisposable
    {
        private readonly CancellationTokenSource feedCancellationTokenSource =
            new CancellationTokenSource();
        private readonly Task feedTask;

        public ClassWithTread()
        {
            feedTask = Task.Factory.StartNew(() =>
            {
                while (!feedCancellationTokenSource.IsCancellationRequested)
                {
                    Console.WriteLine("thread working....");
                    Thread.Sleep(200);
                }
            });
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            Console.WriteLine("Disposing ...");

            if (disposing)
            {
                feedCancellationTokenSource.Cancel();
                feedTask.Wait();

                feedCancellationTokenSource.Dispose();
                feedTask.Dispose();
            }
        }
    }
}
