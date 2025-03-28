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
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MiniCommon.Web.Abstractions;
using MiniCommon.Web.Interfaces;

namespace MiniCommon.Web;

public class Request : IHttpRequest
{
    public static ValidatedRequest HttpRequest { get; } = new();

    /// <inheritdoc />
    public static async Task<HttpResponseMessage?> GetAsync(string request)
    {
        return await HttpRequest.GetAsync(request);
    }

    /// <inheritdoc />
    public static async Task<byte[]?> GetByteArrayAsync(string request)
    {
        return await HttpRequest.GetByteArrayAsync(request);
    }

    /// <inheritdoc />
    public static async Task<Stream?> GetStreamAsync(string request)
    {
        return await HttpRequest.GetStreamAsync(request);
    }

    /// <inheritdoc />
    public static async Task<T?> GetObjectFromJsonAsync<T>(
        string request,
        JsonSerializerContext ctx
    )
        where T : struct
    {
        return await HttpRequest.GetObjectFromJsonAsync<T>(request, ctx);
    }

    /// <inheritdoc />
    public static async Task<string?> GetJsonAsync<T>(
        string request,
        string filepath,
        Encoding encoding,
        JsonSerializerContext ctx
    )
        where T : class
    {
        return await HttpRequest.GetJsonAsync<T>(request, filepath, encoding, ctx);
    }

    /// <inheritdoc />
    public static async Task<string?> GetStringAsync(
        string request,
        string filepath,
        Encoding encoding
    )
    {
        return await HttpRequest.GetStringAsync(request, filepath, encoding);
    }

    /// <inheritdoc />
    public static async Task<string?> GetStringAsync(string request)
    {
        return await HttpRequest.GetStringAsync(request);
    }

    /// <inheritdoc />
    public static async Task<bool> DownloadSHA256(string request, string filepath, string hash)
    {
        return await HttpRequest.DownloadSHA256(request, filepath, hash);
    }

    /// <inheritdoc />
    public static async Task<bool> DownloadSHA1(string request, string filepath, string hash)
    {
        return await HttpRequest.DownloadSHA1(request, filepath, hash);
    }

    /// <inheritdoc />
    public static async Task<bool> Download(string request, string filepath)
    {
        return await HttpRequest.Download(request, filepath);
    }
}
