using System.Collections;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Strategy
{
    public static class AsyncExtensions
    {
        public struct Void { }
        public static Task<TResult> AsTask<TResult>(this IAwaitable<TResult> awaitable) => Task.Run(async () => await awaitable);
        
        public static async Task<TResult> WithCancellation<TResult>(this Task<TResult> originalTask, CancellationToken cancellationToken)
        {
            var cancelTask = new TaskCompletionSource<Void>();
            using (cancellationToken.Register(t => ((TaskCompletionSource<Void>)t).TrySetResult(new Void()), cancelTask))
            {
                var any = await Task.WhenAny(originalTask, cancelTask.Task);
                if(any == cancelTask.Task) cancellationToken.ThrowIfCancellationRequested();
            }
            return await originalTask;
        }

        public static async Task<TResult> WithCancellation<TResult>(this IAwaitable<TResult> awaitable, CancellationToken cancellationToken) =>
            await WithCancellation(awaitable.AsTask(), cancellationToken);
    }
}
