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
using System.Text;
using MiniCommon.IO.Models;

namespace MiniCommon.IO.Interfaces;

public interface IBaseFileSystem
{
    /// <summary>
    /// Gets or sets the current working directory.
    /// </summary>
    public abstract string Cwd { get; set; }

    /// <summary>
    /// Redact the hostname from a given filepath.
    /// </summary>
    public abstract string GetRedactedPath(string filepath);

    /// <summary>
    /// Check if a path is problematic.
    /// </summary>
    public abstract (bool IsProblem, PathCheck Check) CheckPathForProblemLocations(string filepath);

    /// <summary>
    /// Get the absolute path of the specified string.
    /// </summary>
    public abstract string GetFullPath(string filepath);

    /// <summary>
    /// Determine whether a filepath is a directory or file.
    /// Returns true if the path is a directory, false if it's a file, and null if it's neither.
    /// </summary>
    public abstract bool? IsDirFile(string filepath);

    /// <summary>
    /// Combines two file paths.
    /// </summary>
    public abstract string Combine(string path1, string path2);

    /// <summary>
    /// Combines three file paths.
    /// </summary>
    public abstract string Combine(string path1, string path2, string path3);

    /// <summary>
    /// Combines four file paths.
    /// </summary>
    public abstract string Combine(string path1, string path2, string path3, string path4);

    /// <summary>
    /// Combines an array of file paths.
    /// </summary>
    public abstract string Combine(params string[] paths);

    /// <summary>
    /// Combines a file path with the current working directory.
    /// </summary>
    public abstract string FromCwd(string filepath);

    /// <summary>
    /// Combines two file paths with the current working directory.
    /// </summary>
    public abstract string FromCwd(string path1, string path2);

    /// <summary>
    /// Combines three file paths with the current working directory.
    /// </summary>
    public abstract string FromCwd(string path1, string path2, string path3);

    /// <summary>
    /// Combines an array of file paths with the current working directory.
    /// </summary>
    public abstract string FromCwd(params string[] paths);

    /// <summary>
    /// Gets the directory name of a file path.
    /// </summary>
    public abstract string GetDirectoryName(string filepath);

    /// <summary>
    /// Gets the file name of a file path.
    /// </summary>
    public abstract string GetFileName(string filepath);

    /// <summary>
    /// Gets the file extension of a file path.
    /// </summary>
    public abstract string GetFileExtension(string filepath);

    /// <summary>
    /// Gets the file name without extension of a file path.
    /// </summary>
    public abstract string GetFileNameWithoutExtension(string filepath);

    /// <summary>
    /// Gets the relative path from one path to another.
    /// </summary>
    public abstract string GetRelativePath(string relativeTo, string path);

    /// <summary>
    /// Gets the relative path to itself.
    /// </summary>
    public abstract string GetRelativePath(string relativeTo);

    /// <summary>
    /// Moves a file from one place to another.
    /// </summary>
    public abstract void MoveFile(string a, string b);

    /// <summary>
    /// Copies a file from one place to another.
    /// </summary>
    public abstract void CopyFile(string a, string b, bool overwrite = true);

    /// <summary>
    /// Copies a directory from one place to another.
    /// </summary>
    public abstract void CopyDirectory(string a, string b, bool recursive = false);

    /// <summary>
    /// Determines whether the specified file or directory exists.
    /// </summary>
    public abstract bool Exists(string filepath);

    /// <summary>
    /// Creates a directory.
    /// </summary>
    public abstract void CreateDirectory(string filepath);

    /// <summary>
    /// Reads the contents of a file as bytes.
    /// </summary>
    public abstract byte[] ReadFile(string filepath);

    /// <summary>
    /// Reads the contents of a file as a string.
    /// </summary>
    public abstract string ReadFile(string filepath, Encoding encoding);

    /// <summary>
    /// Reads the contents of a file as a string.
    /// </summary>
    public abstract string ReadAllText(string filepath);

    /// <summary>
    /// Reads the lines of a file.
    /// </summary>
    public abstract string[] ReadAllLines(string filepath);

    /// <summary>
    /// Writes data to a file.
    /// </summary>
    public abstract void WriteFile(string filepath, byte[] data);

    /// <summary>
    /// Writes a string to a file.
    /// </summary>
    public abstract void WriteFile(string filepath, string data, Encoding? encoding = null);

    /// <summary>
    /// Opens a file for writing.
    /// </summary>
    public abstract FileStream OpenWrite(string filepath);

    /// <summary>
    /// Opens a file for reading.
    /// </summary>
    public abstract FileStream OpenRead(string filepath);

    /// <summary>
    /// Gets the directories within a directory.
    /// </summary>
    public abstract string[] GetDirectories(string filepath);

    /// <summary>
    /// Gets the directories within a directory as DirectoryInfo objects.
    /// </summary>
    public abstract DirectoryInfo[] GetDirectoryInfos(
        string filepath,
        string searchPattern,
        SearchOption searchOption
    );

    /// <summary>
    /// Gets the files within a directory.
    /// </summary>
    public abstract string[] GetFiles(string filepath);

    /// <summary>
    /// Gets the files within a directory and its subdirectories.
    /// </summary>
    public abstract string[] GetFiles(
        string filepath,
        string searchPattern,
        SearchOption searchOption = SearchOption.AllDirectories,
        bool includeExtension = true
    );

    /// <summary>
    /// Gets the files within a directory and its subdirectories as FileInfo objects.
    /// </summary>
    public abstract FileInfo[] GetFileInfos(
        string filepath,
        string searchPattern,
        SearchOption searchOption
    );

    /// <summary>
    /// Deletes a directory.
    /// </summary>
    public abstract void DeleteDirectory(string filepath, bool recursive = false);

    /// <summary>
    /// Deletes a file.
    /// </summary>
    public abstract void DeleteFile(string filepath);

    /// <summary>
    /// Gets the count of files within a directory and its subdirectories.
    /// </summary>
    public abstract int GetFilesCount(string filepath);

    /// <summary>
    /// Creates a relative path from one path to another.
    /// </summary>
    public abstract string MakeRelativePath(string a, string b);
}
