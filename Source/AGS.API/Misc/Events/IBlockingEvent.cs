﻿using System;
using System.Threading.Tasks;

namespace AGS.API
{
    /// <summary>
    /// Represents an event which can be subscribed and invoked synchronously.
    /// An event is a notification for something that has happened.
    /// Interested parties can subscribe to the event and be notified when it triggers (https://en.wikipedia.org/wiki/Event-driven_programming).
    /// </summary>
	public interface IBlockingEvent<TEventArgs>
	{
        /// <summary>
        /// Gets the number of subscribers to the event.
        /// </summary>
        /// <value>The subscribers count.</value>
		int SubscribersCount { get; }

        /// <summary>
        /// Subscribe the specified callback to the event.
        /// Once subscribed, whenever the event happens this callback will be called.
        /// </summary>
        /// <param name="callback">Callback.</param>
		void Subscribe(Action<TEventArgs> callback);

        /// <summary>
        /// Unsubscribe the specified callback from the event.
        /// This will stops notifications to call this callback.
        /// </summary>
        /// <param name="callback">Callback.</param>
		void Unsubscribe(Action<TEventArgs> callback);

        /// <summary>
        /// Asynchronously wait until the event fires and the specific condition applies.
        /// </summary>
        /// <returns>The task to be awaited.</returns>
        /// <param name="condition">The condition we are waiting to apply before moving on.</param>
        Task WaitUntilAsync(Predicate<TEventArgs> condition);

        /// <summary>
        /// Invoke the event synchronously (i.e will wait for all subscribers to process the event before moving on).
        /// </summary>
        /// <param name="args">Event arguments which can be used to provide additional data.</param>
		void Invoke(TEventArgs args);
	}

    /// <summary>
    /// Represents an event which can be subscribed and invoked synchronously.
    /// An event is a notification for something that has happened.
    /// Interested parties can subscribe to the event and be notified when it triggers (https://en.wikipedia.org/wiki/Event-driven_programming).
    /// </summary>
    public interface IBlockingEvent
    {
        /// <summary>
        /// Gets the number of subscribers to the event.
        /// </summary>
        /// <value>The subscribers count.</value>
        int SubscribersCount { get; }

        /// <summary>
        /// Subscribe the specified callback to the event.
        /// Once subscribed, whenever the event happens this callback will be called.
        /// </summary>
        /// <param name="callback">Callback.</param>
        void Subscribe(Action callback);

        /// <summary>
        /// Unsubscribe the specified callback from the event.
        /// This will stops notifications to call this callback.
        /// </summary>
        /// <param name="callback">Callback.</param>
        void Unsubscribe(Action callback);

        /// <summary>
        /// Asynchronously wait until the event fires and the specific condition applies.
        /// </summary>
        /// <returns>The task to be awaited.</returns>
        /// <param name="condition">The condition we are waiting to apply before moving on.</param>
        Task WaitUntilAsync(Func<bool> condition);

        /// <summary>
        /// Invoke the event synchronously (i.e will wait for all subscribers to process the event before moving on).
        /// </summary>
        void Invoke();
    }
}

