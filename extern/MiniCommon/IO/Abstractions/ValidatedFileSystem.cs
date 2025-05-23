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

using System.IO;
using System.Linq;
using System.Text;
using MiniCommon.IO.Models;
using MiniCommon.Logger.Enums;
using MiniCommon.Validation;
using MiniCommon.Validation.Exceptions;
using MiniCommon.Validation.Validators;

namespace MiniCommon.IO.Abstractions;

public class ValidatedFileSystem : BaseFileSystem
{
    /// <inheritdoc />
    public override bool? HasAttribute(string filepath, FileAttributes attribute)
    {
        if (Validate.For.IsNullOrWhiteSpace([filepath], NativeLogLevel.Fatal))
            return null;
        return base.HasAttribute(filepath, attribute);
    }

    /// <inheritdoc />
    public override string GetRedactedPath(string filepath)
    {
        if (Validate.For.IsNullOrWhiteSpace([filepath], NativeLogLevel.Fatal))
            return string.Empty;
        return base.GetRedactedPath(filepath);
    }

    /// <inheritdoc />
    public override (bool, PathCheck?) CheckPathForProblemLocations(string filepath)
    {
        if (Validate.For.IsNullOrWhiteSpace([filepath], NativeLogLevel.Fatal))
            return (true, default);
        return base.CheckPathForProblemLocations(filepath);
    }

    /// <inheritdoc />
    public override string GetFullPath(string filepath)
    {
        if (Validate.For.IsNullOrWhiteSpace([filepath], NativeLogLevel.Fatal))
            return string.Empty;
        return base.GetFullPath(filepath);
    }

    /// <inheritdoc />
    public override bool? IsDirFile(string filepath)
    {
        if (Validate.For.IsNullOrWhiteSpace([filepath], NativeLogLevel.Fatal))
            return null;
        return base.IsDirFile(filepath);
    }

    /// <inheritdoc />
    public override string Combine(string path1, string path2)
    {
        if (Validate.For.IsNullOrWhiteSpace([path1, path2], NativeLogLevel.Fatal))
            return string.Empty;
        return base.Combine(path1, path2);
    }

    /// <inheritdoc />
    public override string Combine(string path1, string path2, string path3)
    {
        if (Validate.For.IsNullOrWhiteSpace([path1, path2, path3], NativeLogLevel.Fatal))
            return string.Empty;
        return base.Combine(path1, path2, path3);
    }

    /// <inheritdoc />
    public override string Combine(string path1, string path2, string path3, string path4)
    {
        if (Validate.For.IsNullOrWhiteSpace([path1, path2, path3, path4], NativeLogLevel.Fatal))
            return string.Empty;
        return base.Combine(path1, path2, path3, path4);
    }

    /// <inheritdoc />
    public override string Combine(params string[] paths)
    {
        if (Validate.For.IsNullOrWhiteSpace(paths, NativeLogLevel.Fatal))
            return string.Empty;
        return base.Combine(paths);
    }

    /// <inheritdoc />
    public override string FromCwd(string filepath)
    {
        if (Validate.For.IsNullOrWhiteSpace([filepath], NativeLogLevel.Fatal))
            return string.Empty;
        return base.Combine(Cwd, filepath);
    }

#pragma warning disable S2234
    /// <inheritdoc />
    public override string FromCwd(string path1, string path2)
    {
        if (Validate.For.IsNullOrWhiteSpace([path1, path2], NativeLogLevel.Fatal))
            return string.Empty;
        return base.Combine(Cwd, path1, path2);
    }

    /// <inheritdoc />
    public override string FromCwd(string path1, string path2, string path3)
    {
        if (Validate.For.IsNullOrWhiteSpace([path1, path2, path3], NativeLogLevel.Fatal))
            return string.Empty;
        return base.Combine(Cwd, path1, path2, path3);
    }
#pragma warning restore S2234

    /// <inheritdoc />
    public override string FromCwd(params string[] paths)
    {
        if (Validate.For.IsNullOrWhiteSpace(paths, NativeLogLevel.Fatal))
            return string.Empty;
        return base.Combine([.. paths.Prepend(Cwd)]);
    }

    /// <inheritdoc />
    public override string GetDirectoryName(string filepath)
    {
        if (Validate.For.IsNullOrWhiteSpace([filepath], NativeLogLevel.Fatal))
            return string.Empty;
        return base.GetDirectoryName(filepath);
    }

    /// <inheritdoc />
    public override string GetFileName(string filepath)
    {
        return base.GetFileName(filepath);
    }

    /// <inheritdoc />
    public override string GetFileExtension(string filepath)
    {
        if (Validate.For.IsNullOrWhiteSpace([filepath], NativeLogLevel.Fatal))
            return string.Empty;
        return base.GetFileExtension(filepath);
    }

    /// <inheritdoc />
    public override string GetFileNameWithoutExtension(string filepath)
    {
        if (Validate.For.IsNullOrWhiteSpace([filepath], NativeLogLevel.Fatal))
            return string.Empty;
        return base.GetFileNameWithoutExtension(filepath);
    }

    /// <inheritdoc />
    public override string GetRelativePath(string relativeTo, string path)
    {
        if (Validate.For.IsNullOrWhiteSpace([relativeTo, path], NativeLogLevel.Fatal))
            return string.Empty;
        return base.GetRelativePath(relativeTo, path);
    }

    /// <inheritdoc />
    public override string GetRelativePath(string relativeTo)
    {
        if (Validate.For.IsNullOrWhiteSpace([relativeTo], NativeLogLevel.Fatal))
            return string.Empty;
        return base.GetRelativePath(relativeTo);
    }

    /// <inheritdoc />
    public override void MoveFile(string a, string b, bool overwrite = true)
    {
        if (Validate.For.IsNullOrWhiteSpace([a, b], NativeLogLevel.Fatal))
            return;
        base.MoveFile(a, b, overwrite);
    }

    /// <inheritdoc />
    public override void CopyFile(string a, string b, bool overwrite = true)
    {
        if (Validate.For.IsNullOrWhiteSpace([a, b], NativeLogLevel.Fatal))
            return;
        base.CopyFile(a, b, overwrite);
    }

    /// <inheritdoc />
    public override void CopyDirectory(string a, string b, bool recursive = false)
    {
        if (Validate.For.IsNullOrWhiteSpace([a, b], NativeLogLevel.Fatal))
            return;
        base.CopyDirectory(a, b, recursive);
    }

    /// <inheritdoc />
    public override bool Exists(string filepath)
    {
        if (Validate.For.IsNullOrWhiteSpace([filepath], NativeLogLevel.Fatal))
            return false;
        return base.Exists(filepath);
    }

    /// <inheritdoc />
    public override void CreateDirectory(string filepath)
    {
        if (Validate.For.IsNullOrWhiteSpace([filepath], NativeLogLevel.Fatal))
            return;
        base.CreateDirectory(filepath);
    }

    /// <inheritdoc />
    public override byte[] ReadFile(string filepath)
    {
        if (Validate.For.IsNullOrWhiteSpace([filepath], NativeLogLevel.Fatal))
            return [];
        return base.ReadFile(filepath);
    }

    /// <inheritdoc />
    public override string ReadFile(string filepath, Encoding encoding)
    {
        if (Validate.For.IsNullOrWhiteSpace([filepath], NativeLogLevel.Fatal))
            return string.Empty;
        return base.ReadFile(filepath, encoding);
    }

    /// <inheritdoc />
    public override string ReadAllText(string filepath)
    {
        if (Validate.For.IsNullOrWhiteSpace([filepath], NativeLogLevel.Fatal))
            return string.Empty;
        return base.ReadAllText(filepath);
    }

    /// <inheritdoc />
    public override string[] ReadAllLines(string filepath)
    {
        if (Validate.For.IsNullOrWhiteSpace([filepath], NativeLogLevel.Fatal))
            return [];
        return base.ReadAllLines(filepath);
    }

    /// <inheritdoc />
    public override void WriteFile(string filepath, byte[] data)
    {
        if (
            Validate.For.IsNullOrWhiteSpace([filepath], NativeLogLevel.Fatal)
            || Validate.For.IsNull(data, NativeLogLevel.Fatal)
        )
        {
            return;
        }

        base.WriteFile(filepath, data);
    }

    /// <inheritdoc />
    public override void WriteFile(string filepath, string data, Encoding? encoding = null)
    {
        if (Validate.For.IsNullOrWhiteSpace([filepath, data], NativeLogLevel.Fatal))
            return;
        base.WriteFile(filepath, data, encoding);
    }

    /// <inheritdoc />
    public override FileStream OpenWrite(string filepath)
    {
        if (Validate.For.IsNullOrWhiteSpace([filepath], NativeLogLevel.Fatal))
            throw new ObjectValidationException(nameof(filepath));
        return base.OpenWrite(filepath);
    }

    /// <inheritdoc />
    public override FileStream OpenRead(string filepath)
    {
        if (Validate.For.IsNullOrWhiteSpace([filepath], NativeLogLevel.Fatal))
            throw new ObjectValidationException(nameof(filepath));
        return base.OpenRead(filepath);
    }

    /// <inheritdoc />
    public override string[] GetDirectories(string filepath)
    {
        if (Validate.For.IsNullOrWhiteSpace([filepath], NativeLogLevel.Fatal))
            return [];
        return base.GetDirectories(filepath);
    }

    /// <inheritdoc />
    public override DirectoryInfo[] GetDirectoryInfos(
        string filepath,
        string searchPattern,
        SearchOption searchOption
    )
    {
        if (Validate.For.IsNullOrWhiteSpace([filepath], NativeLogLevel.Fatal))
            return [];
        return base.GetDirectoryInfos(filepath, searchPattern, searchOption);
    }

    /// <inheritdoc />
    public override string[] GetFiles(string filepath)
    {
        if (Validate.For.IsNullOrWhiteSpace([filepath], NativeLogLevel.Fatal))
            return [];
        return base.GetFiles(filepath);
    }

    /// <inheritdoc />
    public override string[] GetFiles(
        string filepath,
        string searchPattern,
        SearchOption searchOption = SearchOption.AllDirectories,
        bool includeExtension = true
    )
    {
        if (Validate.For.IsNullOrWhiteSpace([filepath], NativeLogLevel.Fatal))
            return [];
        return base.GetFiles(filepath, searchPattern, searchOption, includeExtension);
    }

    /// <inheritdoc />
    public override FileInfo[] GetFileInfos(
        string filepath,
        string searchPattern,
        SearchOption searchOption
    )
    {
        if (Validate.For.IsNullOrWhiteSpace([filepath], NativeLogLevel.Fatal))
            return [];
        return base.GetFileInfos(filepath, searchPattern, searchOption);
    }

    /// <inheritdoc />
    public override void DeleteDirectory(
        string filepath,
        bool recursive = false,
        bool includeReadOnly = false
    )
    {
        if (Validate.For.IsNullOrWhiteSpace([filepath], NativeLogLevel.Fatal))
            return;
        base.DeleteDirectory(filepath, recursive, includeReadOnly);
    }

    /// <inheritdoc />
    public override void DeleteFile(string filepath)
    {
        if (Validate.For.IsNullOrWhiteSpace([filepath], NativeLogLevel.Fatal))
            return;
        base.DeleteFile(filepath);
    }

    /// <inheritdoc />
    public override int GetFilesCount(string filepath)
    {
        if (Validate.For.IsNullOrWhiteSpace([filepath], NativeLogLevel.Fatal))
            return 0;
        return base.GetFilesCount(filepath);
    }

    /// <inheritdoc />
    public override string MakeRelativePath(string a, string b)
    {
        if (Validate.For.IsNullOrWhiteSpace([a, b], NativeLogLevel.Fatal))
            return string.Empty;
        return base.MakeRelativePath(a, b);
    }
}
