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
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MiniCommon.Web.Interfaces;

public interface IBaseHttpRequest
{
    /// <summary>
    /// Gets or sets the http clients retry count.
    /// </summary>
    public abstract int Retry { get; set; }

    /// <summary>
    /// Gets or sets the http clients time out.
    /// </summary>
    public abstract TimeSpan HttpClientTimeOut { get; set; }

    /// <summary>
    /// Get the http client.
    /// </summary>
    public abstract HttpClient GetHttpClient();

    /// <summary>
    /// Sends a GET async request to the specified URI.
    /// </summary>
    public abstract Task<HttpResponseMessage?> GetAsync(string request);

    /// <summary>
    /// Sends a GET async request to the specified URI, and returns the response body as a byte array.
    /// </summary>
    public abstract Task<byte[]?> GetByteArrayAsync(string request);

    /// <summary>
    /// Sends a GET async request to the specified URI, and returns the response body as a stream.
    /// </summary>
    public abstract Task<Stream?> GetStreamAsync(string request);

    /// <summary>
    /// Sends a GET async request to the specified URI, and returns the response body as a deserialized object of type T.
    /// </summary>
    public abstract Task<T?> GetObjectFromJsonAsync<T>(string request, JsonSerializerContext ctx)
        where T : struct;

    /// <summary>
    /// Sends a GET async request to the specified URI, and saves the deserialized response of type T to a file.
    /// </summary>
    public abstract Task<string?> GetJsonAsync<T>(
        string request,
        string filepath,
        Encoding encoding,
        JsonSerializerContext ctx
    )
        where T : class;

    /// <summary>
    /// Sends a GET async request to the specified URI, and saves the response to a file.
    /// </summary>
    public abstract Task<string?> GetStringAsync(
        string request,
        string filepath,
        Encoding encoding
    );

    /// <summary>
    /// Sends a GET async request to the specified URI, and returns the response body as a string.
    /// </summary>
    public abstract Task<string?> GetStringAsync(string request);

    /// <summary>
    /// Sends a GET async request to the specified URI, compares SHA256 hashes, and saves file if comparison is false.
    /// </summary>
    public abstract Task<bool> DownloadSHA256(string request, string filepath, string hash);

    /// <summary>
    /// Sends a GET async request to the specified URI, compares SHA1 hashes, and saves file if comparison is false.
    /// </summary>
    public abstract Task<bool> DownloadSHA1(string request, string filepath, string hash);

    /// <summary>
    /// Sends a GET async request to the specified URI, and saves the response as a filestream.
    /// </summary>
    public abstract Task<bool> Download(string request, string filepath);
}
