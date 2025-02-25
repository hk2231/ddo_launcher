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

using System.Threading.Tasks;
using MiniCommon.Logger.Enums;

namespace MiniCommon.Logger;

public interface ILogger
{
    public Task Base(NativeLogLevel level, string message);
    public Task Base(NativeLogLevel level, string format, params object[] args);

    public Task Debug(string message);
    public Task Debug(string format, params object[] args);

    public Task Info(string message);
    public Task Info(string format, params object[] args);

    public Task Warn(string message);
    public Task Warn(string format, params object[] args);

    public Task Native(string message);
    public Task Native(string format, params object[] args);

    public Task Error(string message);
    public Task Error(string format, params object[] args);

    public Task Fatal(string message);
    public Task Fatal(string format, params object[] args);

    public Task Benchmark(string message);
    public Task Benchmark(string format, params object[] args);
}
