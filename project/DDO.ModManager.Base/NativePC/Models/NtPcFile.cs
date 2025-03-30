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

using System.Text.Json.Serialization;

namespace DDO.ModManager.Base.NativePC.Models;

public class NtPcFile
{
    [JsonPropertyName("Source")]
    public string? Source { get; set; }

    [JsonPropertyName("Destination")]
    public string? Destination { get; set; }

    [JsonPropertyName("Crc")]
    public string? Crc { get; set; }

    public NtPcFile() { }
}

[JsonSourceGenerationOptions(WriteIndented = true)]
[JsonSerializable(typeof(NtPcFile))]
internal partial class NtPcFileContext : JsonSerializerContext;
