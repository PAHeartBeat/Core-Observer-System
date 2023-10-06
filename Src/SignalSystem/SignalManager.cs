// --------------------------------------------------------------------------------------
// <copyright file="SignalManager.cs" company="iPAHeartBeat">
// Copyright (c) iPAHeartBeat. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------

/*
Author:				Ankur Ranpariya {iPAHeartBeat}
EMail:				ankur30884@gmail.com
Copyright:			(c) 2017, Ankur Ranpariya {iPAHeartBeat}
Social:				@iPAHeartBeat,
GitHub:				https://www.github.com/PAHeartBeat

Original Source:	http://wiki.unity3d.com/index.php/NotificationCenter
Last Modified:		Ankur Ranpariya
Contributed By:		N/A

All rights reserved.
Redistribution and use in source and binary forms, with or without modification, are permitted provided that the
following conditions are met:

* Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.
* Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.
* Neither the name of the [ORGANIZATION] nor the names of its contributors may be used to endorse or promote products derived from this software without specific prior written permission.


The above copyright notice and this permission notice shall be included in all copies or substantial portions of the
Software.

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated
documentation files (the "Software"), to deal in the Software without restriction, including without limitation the
rights to use, copy, modify, merge, publish, distribute, sub license, and/or sell copies of the Software, and to permit
persons to whom the Software is furnished to do so, subject to the following conditions:

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR
OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE
*/

using System;
using System.Collections;

using System.Threading.Tasks;

using iPAHeartBeat.Core.Abstraction;
using iPAHeartBeat.Core.Extensions;
using iPAHeartBeat.Core.Singleton;

namespace iPAHeartBeat.Core.SignalSystem;

/// <summary>
/// Signal Handler to pass data or command between different and multiple module without coupling them with each other via delegate event which required reference of the object/class from another module. This Feature or module will be used a singleton.
/// </summary>
public class SignalManager : Singleton<SignalManager>, ISignalManager {
	/// <summary>
	/// internal hash-table data to store mapping of signal and it's listeners.
	/// </summary>
	private readonly Hashtable _listenersData = new Hashtable();

	/// <summary>
	/// Generic Logger System to user logging with different system.
	/// </summary>
	private readonly IMasterLogger _log;

	/// <summary>
	/// Initializes a new instance of the <see cref="SignalManager"/> class.
	/// </summary>
	public SignalManager() {
		Me = this;
		this._log = Dependency.DependencyResolver.Resolve<IMasterLogger>();
	}

	/// <summary>
	/// Will register/subscribe a method which has a parameter of same signal type as Listener of signal for the particular signal type. If the method is already subscribed for particular signal, it will not be re-added as duplicate listener to avoid multiple call of a single listener and ignored silently without any error.
	/// </summary>
	/// <typeparam name="TType">Signal type.</typeparam>
	/// <param name="handler">Reference of the method or action which will be executed when Signal fired.</param>
	public void SubscribeSignal<TType>(Action<TType> handler) {
		if (handler.IsNull()) {
			_ = this._log?.LogDebug("SignalSystem", 11, "Subscribe => empty name specified for method in AddListener.");
			return;
		}

		if (!this._listenersData.Contains(typeof(TType))) {
			this._listenersData[typeof(TType)] = new ArrayList();
		}

		var listenerList = (ArrayList)this._listenersData[typeof(TType)];
		if (!listenerList.Contains(handler)) {
			_ = listenerList.Add(handler);
		}
	}

	/// <summary>
	/// Will register/subscribe a method which has a parameter of same signal type as Listener of signal for the particular signal type. If the method is already subscribed for particular signal, it will not be re-added as duplicate listener to avoid multiple call of a single listener and ignored silently without any error.
	/// </summary>
	/// <typeparam name="TType">Signal type.</typeparam>
	/// <param name="handler">Reference of the method or action which will be executed when Signal fired.</param>
	public void SubscribeSignal<TType>(Action handler) {
		if (handler.IsNull()) {
			_ = this._log?.LogDebug("SignalSystem", 11, "Subscribe => empty name specified for method in AddListener.");
			return;
		}

		if (!this._listenersData.Contains(typeof(TType))) {
			this._listenersData[typeof(TType)] = new ArrayList();
		}

		var listenerList = (ArrayList)this._listenersData[typeof(TType)];
		if (!listenerList.Contains(handler)) {
			_ = listenerList.Add(handler);
		}
	}

	/// <summary>
	/// Will unregister/unsubscribe  method which subscribed to listen signals to execute. If method is not subscribed for particular signal type it will be silently ignored without any error.
	/// </summary>
	/// <typeparam name="TType">Signal type.</typeparam>
	/// <param name="handler">Reference of the method or action which will be executed when Signal fired.</param>
	public void UnsubscribeSignal<TType>(Action<TType> handler) {
		var listenerList = (ArrayList)this._listenersData[typeof(TType)];

		if (listenerList.IsNotNull()) {
			if (listenerList.Contains(handler)) {
				listenerList.Remove(handler);
			}

			if (listenerList.Count == 0) {
				this._listenersData.Remove(typeof(TType));
			}
		}
	}

	/// <summary>
	/// Will unregister/unsubscribe  method which subscribed to listen signals to execute. If method is not subscribed for particular signal type it will be silently ignored without any error.
	/// </summary>
	/// <typeparam name="TType">Signal type.</typeparam>
	/// <param name="handler">Reference of the method or action which will be executed when Signal fired.</param>
	public void UnsubscribeSignal<TType>(Action handler) {
		var listenerList = (ArrayList)this._listenersData[typeof(TType)];

		if (listenerList.IsNotNull()) {
			if (listenerList.Contains(handler)) {
				listenerList.Remove(handler);
			}

			if (listenerList.Count == 0) {
				this._listenersData.Remove(typeof(TType));
			}
		}
	}

	/// <summary>
	/// Will Execute the Signal with delay. Delay will be managed by Threading and execution of actual signal will happens with separate thread. Which could cause issue in certain system which are not allowed other than main thread of the application.
	/// </summary>
	/// <typeparam name="TType">Signal type.</typeparam>
	/// <param name="data">Data with as same signal type which need pass with module or system.</param>
	/// <param name="wait">wait time in seconds before it will executes.</param>
	public void DelayedFire<TType>(TType data, float wait) =>
		_ = Task.Run(() => this.DelayedSignalFire(data, wait));

	/// <summary>
	/// Will Execute the Signal instant in same thread from where execution asked.
	/// </summary>
	/// <typeparam name="TType">Signal type.</typeparam>
	public void Fire<TType>()
		=> this.Fire(default(TType));

	/// <summary>
	/// Will Execute the Signal instant in same thread from where execution asked.
	/// </summary>
	/// <typeparam name="TType">Signal type.</typeparam>
	/// <param name="data">Data with as same signal type which need pass with module or system.</param>
	public void Fire<TType>(TType data) {
		try {
			var listenerList = (ArrayList)this._listenersData[typeof(TType)];

			if (listenerList.IsNull()) {
				_ = this._log?.LogWarning("SignalSystem", 11, "Fire => Did not found any Subscribers for " + typeof(TType));
				return;
			}

			var observersToRemove = new ArrayList();
			for (var index = 0; index < listenerList.Count; index++) {
				var listener = listenerList[index];

				if (listener.IsNull()) {
					_ = observersToRemove.Add(listener);
				} else {
					if (listener is Action<TType> parAction) {
						parAction?.Invoke(data);
					} else if (listener is Action simpleAction) {
						simpleAction?.Invoke();
					} else {
						_ = this._log?.LogWarning("SignalSystem", 11, $"Fire => not valid action to execute for {typeof(TType)} at index {index}");
						_ = observersToRemove.Add(listener);
					}
				}
			}

			foreach (var observer in observersToRemove) {
				listenerList.Remove(observer);
			}
		} catch (Exception ex) {
			Console.WriteLine($"ERROR: {ex.Message}");
		}
	}

	/// <summary>
	/// Internal system as wait for particular time and then needs to be fire or executed.
	/// </summary>
	/// <typeparam name="TType">Signal type.</typeparam>
	/// <param name="data">Data with as same signal type which need pass with module or system.</param>
	/// <param name="waitSec">wait time in seconds before it will executes.</param>
	/// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
	private async Task DelayedSignalFire<TType>(TType data, float waitSec) {
		var waitMS = (int)(waitSec * 1000);
		await Task.Delay(waitMS);
		this.Fire(data);
	}
}
