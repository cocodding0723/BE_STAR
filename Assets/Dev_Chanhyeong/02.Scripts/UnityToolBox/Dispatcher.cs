using UnityEngine;
using System;
using System.Collections.Generic;
using System.Threading;

namespace UnityToolBox
{
    /// <summary>
    /// A system for dispatching code to execute on the main thread. 
    /// </summary>
    [AddComponentMenu("Dispatcher")]
    public class Dispatcher : MonoBehaviour
    {
        private static Dispatcher s_instance;

        // We can't use the behaviour reference from other threads, so we use a separate bool
        // to track the instance so we can use that on the other threads.
        private static bool s_instanceExists;

        private static Thread s_mainThread;
        private static readonly object LockObject = new object();
        private static readonly Queue<Action> Actions = new Queue<Action>();

        /// <summary>
        /// Gets a value indicating whether or not the current thread is the game's main thread.
        /// </summary>
        public static bool IsMainThread => Thread.CurrentThread == s_mainThread;

        /// <summary>
        /// Queues an action to be invoked on the main game thread.
        /// </summary>
        /// <param name="action">The action to be queued.</param>
        public static void InvokeAsync(Action action)
        {
            if (!s_instanceExists) {
                Debug.LogError("No Dispatcher exists in the scene. Actions will not be invoked!");
                return;
            }

            if (IsMainThread) {
                // Don't bother queuing work on the main thread; just execute it.
                action();
            }
            else {
                lock (LockObject) {
                    Actions.Enqueue(action);
                }
            }
        }

        /// <summary>
        /// Queues an action to be invoked on the main game thread and blocks the
        /// current thread until the action has been executed.
        /// </summary>
        /// <param name="action">The action to be queued.</param>
        public static void Invoke(Action action)
        {
            if (!s_instanceExists) {
                Debug.LogError("No Dispatcher exists in the scene. Actions will not be invoked!");
                return;
            }

            bool hasRun = false;

            InvokeAsync(() =>
            {
                action();
                hasRun = true;
            });

            // Lock until the action has run
            while (!hasRun) {
                Thread.Sleep(5);
            }
        }

        void Awake()
        {
            if (s_instance) {
                DestroyImmediate(this);
            }
            else {
                s_instance = this;
                s_instanceExists = true;
                s_mainThread = Thread.CurrentThread;
            }
        }

        void OnDestroy()
        {
            if (s_instance == this) {
                s_instance = null;
                s_instanceExists = false;
            }
        }

        void Update()
        {
            lock (LockObject) {
                while (Actions.Count > 0) {
                    Actions.Dequeue()();
                }
            }
        }
    }
}