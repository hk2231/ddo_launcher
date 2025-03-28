/*
 * DDO.Launcher
 * Copyright (C) 2024 DDO.Launcher Contributors
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU Affero General Public License as published
 * by the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU Affero General Public License for more details.

 * You should have received a copy of the GNU Affero General Public License
 * along with this program.  If not, see <https://www.gnu.org/licenses/>.
 */

using System;
using System.Collections.Generic;
using MiniCommon.Logger.Enums;
using MiniCommon.Models;

namespace MiniCommon.Providers;

public static class NotificationProvider
{
    private static readonly List<Notification> _notifications = [];
    public static int MaxSize { get; set; } = 100;

    public static void Add(Notification item) => _notifications.Add(item);

    public static void BenchmarkLog(params string[] _params) =>
        _notifications.Add(new(NativeLogLevel.Benchmark, "log", _params));

    public static void Benchmark(string id, params string[] _params) =>
        _notifications.Add(new(NativeLogLevel.Benchmark, id, _params));

    public static void DebugLog(params string[] _params) =>
        _notifications.Add(new(NativeLogLevel.Debug, "log", _params));

    public static void Debug(string id, params string[] _params) =>
        _notifications.Add(new(NativeLogLevel.Debug, id, _params));

    public static void WarnLog(params string[] _params) =>
        _notifications.Add(new(NativeLogLevel.Warn, "log", _params));

    public static void Warn(string id, params string[] _params) =>
        _notifications.Add(new(NativeLogLevel.Warn, id, _params));

    public static void ErrorLog(params string[] _params) =>
        _notifications.Add(new(NativeLogLevel.Error, "log", _params));

    public static void Error(string id, params string[] _params) =>
        _notifications.Add(new(NativeLogLevel.Error, id, _params));

    public static void FatalLog(params string[] _params) =>
        _notifications.Add(new(NativeLogLevel.Fatal, "log", _params));

    public static void Fatal(string id, params string[] _params) =>
        _notifications.Add(new(NativeLogLevel.Fatal, id, _params));

    public static void InfoLog(params string[] _params) =>
        _notifications.Add(new(NativeLogLevel.Info, "log", _params));

    public static void Info(string id, params string[] _params) =>
        _notifications.Add(new(NativeLogLevel.Info, id, _params));

    public static void NativeLog(params string[] _params) =>
        _notifications.Add(new(NativeLogLevel.Native, "log", _params));

    public static void Native(string id, params string[] _params) =>
        _notifications.Add(new(NativeLogLevel.Native, id, _params));

    public static void PrintLog(NativeLogLevel level, params string[] _params) =>
        _notifications.Add(new(level, "log", _params));

    public static void Log(NativeLogLevel level, string id) => _notifications.Add(new(level, id));

    public static void Log(NativeLogLevel level, string id, params string[] _params) =>
        _notifications.Add(new(level, id, _params));

    public static void Clear() => _notifications.Clear();

    /// <summary>
    /// Execute a user-defined method when a Notification is added to the list.
    /// </summary>
    public static void OnNotificationAdded(Action<Notification> func)
    {
        Notification.OnNotificationAdded += func;
        Notification.OnNotificationAdded += Manage;
    }

    /// <summary>
    /// Keep the Notification list in a rotating list of MaxSize.
    /// </summary>
    private static void Manage(Notification _)
    {
        if (_notifications.Count > MaxSize)
            _notifications.RemoveAt(0);
    }
}
