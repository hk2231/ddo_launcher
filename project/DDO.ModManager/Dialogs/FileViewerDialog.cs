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

using Avalonia.Controls;
using MiniCommon.Avalonia.Dialogs.Abstractions;
using Window = Avalonia.Controls.Window;

namespace DDO.ModManager.Dialogs;

public class FileViewerDialog : BaseDialog
{
    public FileViewerDialog(string title, Window window)
        : base(new FileViewerDialogWindow() { Title = title }, window)
    {
        Dialog.Title = title;
        FindControl<TextBlock>("TitleTextBlock").Text = title;
        FindControl<Button>("ConfirmButton").Click += OnClick;
        FindControl<Button>("CancelButton").Click += OnClick;
    }
}
