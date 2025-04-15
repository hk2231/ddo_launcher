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

using System.Text.Json;
using System.Text.Json.Serialization;

namespace DDO.ModManager.Base.NativePC.Models;

public class NtPcDeploy
{
    [JsonPropertyName("Mods")]
    public string? Mods { get; set; }

    [JsonPropertyName("Temp")]
    public string? Temp { get; set; }

    [JsonPropertyName("Output")]
    public string? Output { get; set; }

    public NtPcDeploy() { }
}

[JsonSourceGenerationOptions(
    WriteIndented = true,
    ReadCommentHandling = JsonCommentHandling.Skip,
    AllowTrailingCommas = true
)]
[JsonSerializable(typeof(NtPcDeploy))]
internal partial class NtPcDeployContext : JsonSerializerContext;
