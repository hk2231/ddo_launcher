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
using MiniCommon.Guard.Interfaces;

namespace MiniCommon.Guard.Guards;

#pragma warning disable IDE0060, RCS1175, RCS1163, RCS1158, S107

public static class BooleanGuard
{
    public static void True(this IGuardClause guardClause, bool argument, string argumentName)
    {
        if (argument)
        {
            throw new ArgumentException(
                string.Format("{0} is not allowing to be true", argumentName)
            );
        }
    }

    public static void False(this IGuardClause guardClause, bool argument, string argumentName)
    {
        if (!argument)
        {
            throw new ArgumentException(
                string.Format("{0} is not allowing to be false", argumentName)
            );
        }
    }
}
