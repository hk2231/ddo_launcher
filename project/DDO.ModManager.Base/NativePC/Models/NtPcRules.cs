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

using System.Collections.Generic;
using System.IO;
using System.Text.Json.Serialization;
using MiniCommon.Extensions;
using MiniCommon.IO;
using MiniCommon.Validation;
using MiniCommon.Validation.Operators;
using MiniCommon.Validation.Validators;

namespace DDO.ModManager.Base.NativePC.Models;

public class NtPcRules
{
    [JsonPropertyName("Addons")]
    public List<NtPcAddon>? Addons { get; set; }

    [JsonPropertyName("Exclusions")]
    public List<NtPcExclusion>? Exclusions { get; set; }

    [JsonPropertyName("LoadOrders")]
    public List<NtPcLoadOrder>? LoadOrders { get; set; }

    [JsonPropertyName("Renames")]
    public List<NtPcRename>? Renames { get; set; }

    [JsonPropertyName("IgnorePrefixes")]
    public List<string>? IgnorePrefixes { get; set; }

    public NtPcRules() { }

    /// <summary>
    /// Read and deserialize a file as an NtPcRules object.
    /// </summary>
    public static NtPcRules? Read(string path)
    {
        NtPcRules? rules = Json.Deserialize<NtPcRules>(VFS.ReadAllText(path), NtPcRulesContext.Default);

        if (Validate.For.IsNull(rules))
            return default;
        return rules!;
    }

    /// <summary>
    /// Create a list of file paths to exclude based on a list of NtPcExclusion objects.
    /// </summary>
    public static List<string> Exclude(NtPcRules rules, string fileEntry, string basePath)
    {
        List<string> filesToExclude = [];

        if (Validate.For.IsNullOrWhiteSpace([fileEntry, basePath]))
            return [];

        foreach (NtPcExclusion exclusion in rules.Exclusions ?? Validate.For.EmptyList<NtPcExclusion>())
        {
            if (Validate.For.IsNull(exclusion))
                return [];

            if (Validate.For.IsNullOrWhiteSpace([exclusion.Name, exclusion.Path]))
                return [];

            if (exclusion.Name != fileEntry)
                continue;

            string outputPath = VFS.Combine(basePath, exclusion.Path!);

            if (!VFS.Exists(outputPath))
                continue;

            if (VFS.IsDirFile(outputPath!) == true) // Directory
            {
                FileInfo[] files = VFS.GetFileInfos(outputPath, "*", SearchOption.AllDirectories);

                foreach (FileInfo file in files)
                    filesToExclude.Add(file.FullName.NormalizePath());

                continue;
            }

            filesToExclude.Add(outputPath.NormalizePath());
        }

        return filesToExclude;
    }
}

[JsonSourceGenerationOptions(WriteIndented = true)]
[JsonSerializable(typeof(NtPcRules))]
internal partial class NtPcRulesContext : JsonSerializerContext;
