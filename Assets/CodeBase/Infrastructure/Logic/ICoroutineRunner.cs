using System.Collections;
using UnityEngine;

namespace CodeBase.Infrastructure.Logic
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator enumerator);
    }
}