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
using System.IO;
using DDO.ModManager.Base.NativePC.Helpers;
using DDO.ModManager.Base.NativePC.Models;
using MiniCommon.Extensions;
using MiniCommon.IO;
using MiniCommon.IO.Enums;
using MiniCommon.IO.Models;
using MiniCommon.Providers;
using MiniCommon.Validation;
using MiniCommon.Validation.Operators;
using MiniCommon.Validation.Validators;

namespace DDO.ModManager.Base.NativePC.Providers;

public static class NtPcProvider
{
    /// <summary>
    /// Process a directory of files according to an NtPcGame and NtPcRules object.
    /// </summary>
    public static void Deploy(string source, string destination, NtPcGame game, NtPcRules rules)
    {
        (bool sourcePathProblen, PathCheck sourcePathCheck) = VFS.CheckPathForProblemLocations(source);
        if (sourcePathProblen)
        {
            ProblemPath(source, sourcePathCheck);
            return;
        }

        (bool destinationPathProblem, PathCheck destinationPathCheck) = VFS.CheckPathForProblemLocations(destination);
        if (destinationPathProblem)
        {
            ProblemPath(source, destinationPathCheck);
            return;
        }

        if (Validate.For.IsNullOrWhiteSpace([source, destination]))
            return;

        if (Validate.For.IsNull(game))
            return;

        if (Validate.For.IsNull(game.Engine))
            return;

        if (Validate.For.IsNull(rules))
            return;

        ProcessDirectory(source, destination, game, rules);
    }

    /// <summary>
    /// Switch handle for path checks.
    /// </summary>
    private static void ProblemPath(string filepath, PathCheck check)
    {
        switch (check.Action)
        {
            case PathCheckAction.Warn: // Fall-through
            case PathCheckAction.Deny:
                NotificationProvider.Error("error.badpath", filepath, check.Target ?? Validate.For.EmptyString());
                break;
            default:
                throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Process a directory of files according to an NtPcGame and NtPcRules object.
    /// </summary>
    private static void ProcessDirectory(string source, string destination, NtPcGame game, NtPcRules rules)
    {
        VFS.CreateDirectory(destination);

        string[] directories = [.. FixDirectories([.. VFS.GetDirectories(source)], rules)];

        foreach (string directoryName in directories)
        {
            string pathEntry = VFS.Combine(source, directoryName);
            (string search, NtPcPath path) = SearchHelper.Search(pathEntry, game.Engine!.Paths!);
            List<string> exclusions = NtPcRules.Exclude(rules, directoryName, pathEntry);
            if (search.Length == 0)
                continue;

            if (path.Unsupported == true)
                continue;

            FileInfo[] additionalDirectories = VFS.GetFileInfos(pathEntry, "*", SearchOption.TopDirectoryOnly);
            foreach (FileInfo file in additionalDirectories)
            {
                if (VFS.IsDirFile(file.FullName) == false && file.Extension == ".dll")
                {
                    CopyHelper.CopyHooks(source, file.FullName, destination, game, rules, exclusions, []);
                    continue;
                }
            }

            List<string> hookNames = (game.Engine.Hooks ?? Validate.For.EmptyList<NtPcHook>()).ConvertAll(hook =>
                hook.Name ?? string.Empty
            );
            CopyHelper.CopyFiles(directoryName, search, destination, path, rules, exclusions, hookNames);
            CopyHelper.CopyAddons(source, directoryName, destination, rules, []);
        }

        CopyHelper.CopyPostAddons(source, destination, rules, []);
    }

    /// <summary>
    /// Fix directories by only including the final part of the directory path.
    /// </summary>
    public static List<string> FixDirectories(List<string> directories, NtPcRules rules)
    {
        List<string> fixedDirectories = [];

        foreach (string directory in directories)
        {
            if (VFS.IsDirFile(directory) == true)
                fixedDirectories.Add(VFS.GetFileName(directory).TrimEnd(Path.DirectorySeparatorChar));
        }

        return SortLoadOrder(fixedDirectories, rules);
    }

    /// <summary>
    /// Sort a list of directories based on a a list of NtPcLoadOrder objects.
    /// </summary>
    public static List<string> SortLoadOrder(List<string> directories, NtPcRules rules)
    {
        List<string?> sorted = directories!;

        foreach (NtPcLoadOrder order in rules.LoadOrders ?? Validate.For.EmptyList<NtPcLoadOrder>())
        {
            if (Validate.For.IsNull(order))
                return [];

            int index = order.Index ?? -1;

            if (index > directories.Count)
                return sorted!;

            if (index == -1)
                index = directories.Count;

            sorted = sorted!.MoveEntry(order.Name, index) ?? [];
        }

        return sorted!;
    }

    /// <summary>
    /// Deletes the specified directory from FS current working directory.
    /// </summary>
    public static void DeleteDirectory(string path)
    {
        if (Validate.For.IsNullOrWhiteSpace([path]))
            return;
        VFS.DeleteDirectory(VFS.GetFullPath(VFS.FromCwd(path)), true, true);
    }
}
