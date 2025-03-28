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
using System.Linq;
using MiniCommon.CommandParser;
using MiniCommon.CommandParser.Helpers;
using MiniCommon.Logger.Enums;
using MiniCommon.Validation;
using MiniCommon.Validation.Validators;

namespace MiniCommon.Models;

public class Command
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<CommandParameter> Parameters { get; set; } = [];

    public Command()
    {
        CommandHelper.Add(this);
    }

    public string Usage()
    {
        string _parameters = string.Join(
            " ",
            Parameters.Select(a => $"\n\t<{a.Name}(Optional:{a.Optional})=value>")
        );
        string parameters = Validate.For.IsNotNullOrWhiteSpace([_parameters], NativeLogLevel.Debug)
            ? _parameters
            : "\n\tNo parameters.";
        string description = Validate.For.IsNotNullOrWhiteSpace([Description], NativeLogLevel.Debug)
            ? Description
            : "No description.";
        return $"\nCommand:\n\t{CommandLine.BaseCommandLine.Prefix}{Name}\nParameters:\t{parameters}\nDescription:\n\t{description}";
    }
}
