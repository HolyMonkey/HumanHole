using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Networking;

public class UnityWebRequestAwaiter : INotifyCompletion
{
    public bool IsCompleted => asyncOp.isDone;

    private readonly UnityWebRequestAsyncOperation asyncOp;
    private Action continuation;

    public UnityWebRequestAwaiter(UnityWebRequestAsyncOperation asyncOp)
    {
        this.asyncOp = asyncOp;
        asyncOp.completed += OnRequestCompleted;
    }

    public void OnCompleted(Action continuation)
    {
        this.continuation = continuation;
    }

    public void GetResult() { }

    private void OnRequestCompleted(AsyncOperation obj)
    {
        continuation();
    }
}