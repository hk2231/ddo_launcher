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
using System.Text;
using System.Threading.Tasks;
using DDO.Launcher.Base.Models;
using DDO.Launcher.Base.Providers;
using MiniCommon.BuildInfo;
using MiniCommon.Enums;
using MiniCommon.Logger;
using MiniCommon.Models;
using MiniCommon.Providers;
using MiniCommon.Validation;
using MiniCommon.Validation.Validators;
using MiniCommon.Web;

namespace DDO.Launcher.Base.Managers;

public static class ServiceManager
{
    public static Settings? Settings { get; private set; }

    /// <summary>
    /// Initialize required services and providers with necessary values.
    /// </summary>
    public static Task<bool> Init()
    {
        try
        {
            LocalizationProvider.Init(
                AssemblyConstants.LocalizationPath(),
                LocalizationSettings.Read(AssemblyConstants.LocalizationSettingsFilePath())
            );
            NotificationProvider.OnNotificationAdded(
                (Notification notification) => Log.Base(notification.LogLevel, notification.Message)
            );
            NotificationProvider.Info("log.initialized");
            SettingsProvider.FirstRun();
            Settings = SettingsProvider.Load();
            if (Validate.For.IsNull(Settings))
                return Task.FromResult(false);
            RequestDataProvider.OnRequestCompleted(
                (RequestData requestData) =>
                    NotificationProvider.Info("request.get.success", requestData.URL, requestData.Elapsed.ToString("c"))
            );
            Request.HttpRequest.HttpClientTimeOut = TimeSpan.FromMinutes(1);
            Tcp.TcpClient.ReceiveTimeout = 5000;
            Tcp.TcpClient.SendTimeout = 5000;
            Tcp.TcpClient.Encoding = new UTF8Encoding(false);
            Watermark.Draw(AssemblyConstants.WatermarkText());
            return Task.FromResult(true);
        }
        catch (Exception ex)
        {
            Log.Fatal(ex.ToString());
            return Task.FromResult(false);
        }
    }
}
