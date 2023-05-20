// Copyright (c) 2015 - 2021 Doozy Entertainment. All Rights Reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement
// A Copy of the EULA APPENDIX 1 is available at http://unity3d.com/company/legal/as_terms

//.........................
//.....Generated Class.....
//.........................
//.......Do not edit.......
//.........................

using UnityEngine;
// ReSharper disable All

namespace Doozy.Runtime.Signals
{
    public partial class Signal
    {
        public static bool Send(StreamId.GameEvents id, string message = "") => SignalsService.SendSignal(nameof(StreamId.GameEvents), id.ToString(), message);
        public static bool Send(StreamId.GameEvents id, GameObject signalSource, string message = "") => SignalsService.SendSignal(nameof(StreamId.GameEvents), id.ToString(), signalSource, message);
        public static bool Send(StreamId.GameEvents id, SignalProvider signalProvider, string message = "") => SignalsService.SendSignal(nameof(StreamId.GameEvents), id.ToString(), signalProvider, message);
        public static bool Send(StreamId.GameEvents id, Object signalSender, string message = "") => SignalsService.SendSignal(nameof(StreamId.GameEvents), id.ToString(), signalSender, message);
        public static bool Send<T>(StreamId.GameEvents id, T signalValue, string message = "") => SignalsService.SendSignal(nameof(StreamId.GameEvents), id.ToString(), signalValue, message);
        public static bool Send<T>(StreamId.GameEvents id, T signalValue, GameObject signalSource, string message = "") => SignalsService.SendSignal(nameof(StreamId.GameEvents), id.ToString(), signalValue, signalSource, message);
        public static bool Send<T>(StreamId.GameEvents id, T signalValue, SignalProvider signalProvider, string message = "") => SignalsService.SendSignal(nameof(StreamId.GameEvents), id.ToString(), signalValue, signalProvider, message);
        public static bool Send<T>(StreamId.GameEvents id, T signalValue, Object signalSender, string message = "") => SignalsService.SendSignal(nameof(StreamId.GameEvents), id.ToString(), signalValue, signalSender, message);

        public static bool Send(StreamId.Score id, string message = "") => SignalsService.SendSignal(nameof(StreamId.Score), id.ToString(), message);
        public static bool Send(StreamId.Score id, GameObject signalSource, string message = "") => SignalsService.SendSignal(nameof(StreamId.Score), id.ToString(), signalSource, message);
        public static bool Send(StreamId.Score id, SignalProvider signalProvider, string message = "") => SignalsService.SendSignal(nameof(StreamId.Score), id.ToString(), signalProvider, message);
        public static bool Send(StreamId.Score id, Object signalSender, string message = "") => SignalsService.SendSignal(nameof(StreamId.Score), id.ToString(), signalSender, message);
        public static bool Send<T>(StreamId.Score id, T signalValue, string message = "") => SignalsService.SendSignal(nameof(StreamId.Score), id.ToString(), signalValue, message);
        public static bool Send<T>(StreamId.Score id, T signalValue, GameObject signalSource, string message = "") => SignalsService.SendSignal(nameof(StreamId.Score), id.ToString(), signalValue, signalSource, message);
        public static bool Send<T>(StreamId.Score id, T signalValue, SignalProvider signalProvider, string message = "") => SignalsService.SendSignal(nameof(StreamId.Score), id.ToString(), signalValue, signalProvider, message);
        public static bool Send<T>(StreamId.Score id, T signalValue, Object signalSender, string message = "") => SignalsService.SendSignal(nameof(StreamId.Score), id.ToString(), signalValue, signalSender, message);

        public static bool Send(StreamId.Shop id, string message = "") => SignalsService.SendSignal(nameof(StreamId.Shop), id.ToString(), message);
        public static bool Send(StreamId.Shop id, GameObject signalSource, string message = "") => SignalsService.SendSignal(nameof(StreamId.Shop), id.ToString(), signalSource, message);
        public static bool Send(StreamId.Shop id, SignalProvider signalProvider, string message = "") => SignalsService.SendSignal(nameof(StreamId.Shop), id.ToString(), signalProvider, message);
        public static bool Send(StreamId.Shop id, Object signalSender, string message = "") => SignalsService.SendSignal(nameof(StreamId.Shop), id.ToString(), signalSender, message);
        public static bool Send<T>(StreamId.Shop id, T signalValue, string message = "") => SignalsService.SendSignal(nameof(StreamId.Shop), id.ToString(), signalValue, message);
        public static bool Send<T>(StreamId.Shop id, T signalValue, GameObject signalSource, string message = "") => SignalsService.SendSignal(nameof(StreamId.Shop), id.ToString(), signalValue, signalSource, message);
        public static bool Send<T>(StreamId.Shop id, T signalValue, SignalProvider signalProvider, string message = "") => SignalsService.SendSignal(nameof(StreamId.Shop), id.ToString(), signalValue, signalProvider, message);
        public static bool Send<T>(StreamId.Shop id, T signalValue, Object signalSender, string message = "") => SignalsService.SendSignal(nameof(StreamId.Shop), id.ToString(), signalValue, signalSender, message);

        public static bool Send(StreamId.UIKeys id, string message = "") => SignalsService.SendSignal(nameof(StreamId.UIKeys), id.ToString(), message);
        public static bool Send(StreamId.UIKeys id, GameObject signalSource, string message = "") => SignalsService.SendSignal(nameof(StreamId.UIKeys), id.ToString(), signalSource, message);
        public static bool Send(StreamId.UIKeys id, SignalProvider signalProvider, string message = "") => SignalsService.SendSignal(nameof(StreamId.UIKeys), id.ToString(), signalProvider, message);
        public static bool Send(StreamId.UIKeys id, Object signalSender, string message = "") => SignalsService.SendSignal(nameof(StreamId.UIKeys), id.ToString(), signalSender, message);
        public static bool Send<T>(StreamId.UIKeys id, T signalValue, string message = "") => SignalsService.SendSignal(nameof(StreamId.UIKeys), id.ToString(), signalValue, message);
        public static bool Send<T>(StreamId.UIKeys id, T signalValue, GameObject signalSource, string message = "") => SignalsService.SendSignal(nameof(StreamId.UIKeys), id.ToString(), signalValue, signalSource, message);
        public static bool Send<T>(StreamId.UIKeys id, T signalValue, SignalProvider signalProvider, string message = "") => SignalsService.SendSignal(nameof(StreamId.UIKeys), id.ToString(), signalValue, signalProvider, message);
        public static bool Send<T>(StreamId.UIKeys id, T signalValue, Object signalSender, string message = "") => SignalsService.SendSignal(nameof(StreamId.UIKeys), id.ToString(), signalValue, signalSender, message);   
    }

    public partial class StreamId
    {
        public enum GameEvents
        {
            Ended,
            EndWave,
            PausedToggled,
            Reset,
            SaveHighScore,
            Started,
            StartWave
        }

        public enum Score
        {
            increased
        }

        public enum Shop
        {
            RerollOccured,
            RerollRequested
        }
        public enum UIKeys
        {
            Any,
            Esc
        }         
    }
}
