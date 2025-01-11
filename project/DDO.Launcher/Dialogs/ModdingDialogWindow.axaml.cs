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

using System.ComponentModel;
using System.Threading.Tasks;
using Avalonia.Controls;
using DDO.Launcher.Base.Managers;
using DDO.Launcher.Base.NativePC.Helpers;
using DDO.Launcher.Base.NativePC.Models;
using DDO.Launcher.Base.NativePC.Providers;
using MiniCommon.BuildInfo;
using MiniCommon.Extensions;
using MiniCommon.IO;
using MiniCommon.Providers;
using MiniCommon.Validation;
using MiniCommon.Validation.Validators;
using RoutedEventArgs = Avalonia.Interactivity.RoutedEventArgs;

namespace DDO.Launcher.Dialogs;

public partial class ModdingDialogWindow : Window, INotifyPropertyChanged
{
    private string _game = string.Empty;

    public new event PropertyChangedEventHandler? PropertyChanged;

    public string Game
    {
        get => _game;
        set
        {
            if (_game != value)
            {
                _game = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Game)));
            }
        }
    }

    public ModdingDialogWindow()
    {
        if (ServiceManager.Settings is null)
            NotificationProvider.Warn("log.unhandled.exception", "Settings is null");

        InitializeComponent();
        DataContext = this;
    }

    private void DeployButton_Click(object sender, RoutedEventArgs e)
    {
        IsHitTestVisible = false;
        Topmost = false;
        Task task = Task.Run(DeployTask);
        if (task.IsCompleted)
        {
            IsHitTestVisible = true;
            Topmost = true;
        }
    }

    private Task DeployTask()
    {
        if (Validate.For.IsNullOrWhiteSpace([_game]))
            return Task.CompletedTask;

        string modPath = VFS.GetRelativePath(VFS.FileSystem.Cwd, VFS.Combine("mods", _game)).NormalizePath();
        string tempPath = VFS.GetRelativePath(VFS.FileSystem.Cwd, VFS.Combine("temp", _game)).NormalizePath();
        string outputPath = VFS.GetRelativePath(VFS.FileSystem.Cwd, VFS.Combine("output", _game)).NormalizePath();
        string gamePath = VFS.GetRelativePath(
                VFS.FileSystem.Cwd,
                VFS.Combine(AssemblyConstants.DataDirectory, "games", _game + ".json")
            )
            .NormalizePath();
        string rulePath = VFS.GetRelativePath(
                VFS.FileSystem.Cwd,
                VFS.Combine(AssemblyConstants.DataDirectory, "user", _game + ".json")
            )
            .NormalizePath();

        if (!VFS.Exists(modPath))
        {
            NotificationProvider.Error("error.readfile", gamePath);
            return Task.CompletedTask;
        }

        if (!VFS.Exists(gamePath))
        {
            NotificationProvider.Error("error.readfile", gamePath);
            return Task.CompletedTask;
        }

        if (!VFS.Exists(rulePath))
        {
            NotificationProvider.Error("error.readfile", rulePath);
            return Task.CompletedTask;
        }

        NtPcGame game = NtPcGame.Read(gamePath);
        NtPcRules rules = NtPcRules.Read(rulePath);
        ExtractHelper.Extract(modPath, tempPath, game);
        NtPcProvider.DeleteDirectory(outputPath);
        NtPcProvider.Deploy(tempPath, outputPath, game, rules);
        NtPcProvider.DeleteDirectory(tempPath);

        return Task.CompletedTask;
    }
}
