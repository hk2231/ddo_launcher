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
using MiniCommon.IO.Abstractions;
using MiniCommon.IO.Interfaces;

namespace MiniCommon.IO;

public class Json : IJson
{
    public static ValidatedJson BaseJson { get; } = new();

    /// <inheritdoc />
    public static string Serialize<T>(T data, JsonSerializerOptions options)
        where T : class
    {
        return BaseJson.Serialize(data, options);
    }

    /// <inheritdoc />
    public static string Serialize<T>(T data, JsonSerializerContext ctx)
        where T : class
    {
        return BaseJson.Serialize(data, ctx);
    }

    /// <inheritdoc />
    public static T? Deserialize<T>(string json, JsonSerializerOptions options)
        where T : class
    {
        return BaseJson.Deserialize<T>(json, options);
    }

    /// <inheritdoc />
    public static T? Deserialize<T>(string data, JsonSerializerContext ctx)
        where T : class
    {
        return BaseJson.Deserialize<T>(data, ctx);
    }

    /// <inheritdoc />
    public static void Save<T>(string filepath, T data, JsonSerializerOptions options)
        where T : class
    {
        BaseJson.Save(filepath, data, options);
    }

    /// <inheritdoc />
    public static void Save<T>(string filepath, T data, JsonSerializerContext ctx)
        where T : class
    {
        BaseJson.Save(filepath, data, ctx);
    }

    /// <inheritdoc />
    public static T? Load<T>(string filepath, JsonSerializerOptions options)
        where T : class
    {
        return BaseJson.Load<T>(filepath, options);
    }

    /// <inheritdoc />
    public static T? Load<T>(string filepath, JsonSerializerContext ctx)
        where T : class
    {
        return BaseJson.Load<T>(filepath, ctx);
    }
}
