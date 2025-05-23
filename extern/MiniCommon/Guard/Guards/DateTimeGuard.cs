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

public static class DateTimeGuard
{
    public static void DateTimeLessThan(
        this IGuardClause guardClause,
        DateTime argument,
        string argumentName,
        DateTime min,
        string minArgumentName = "",
        string format = "dd/MM/yyyy"
    )
    {
        if (argument < min)
        {
            throw new ArgumentException(
                string.Format(
                    "{0} is not allowing less than {1}",
                    argumentName,
                    !string.IsNullOrWhiteSpace(minArgumentName)
                        ? minArgumentName
                        : min.ToString(format)
                )
            );
        }
    }

    public static void DateTimeGreaterThan(
        this IGuardClause guardClause,
        DateTime argument,
        string argumentName,
        DateTime max,
        string maxArgumentName = "",
        string format = "dd/MM/yyyy"
    )
    {
        if (argument > max)
        {
            throw new ArgumentException(
                string.Format(
                    "{0} is not allowing more than {1}",
                    argumentName,
                    !string.IsNullOrWhiteSpace(maxArgumentName)
                        ? maxArgumentName
                        : max.ToString(format)
                )
            );
        }
    }

    public static void DateTimeLessThanOrEqual(
        this IGuardClause guardClause,
        DateTime argument,
        string argumentName,
        DateTime min,
        string minArgumentName = "",
        string format = "dd/MM/yyyy"
    )
    {
        if (argument <= min)
        {
            throw new ArgumentException(
                string.Format(
                    "{0} is not allowing less than or equals to {1}",
                    argumentName,
                    !string.IsNullOrWhiteSpace(minArgumentName)
                        ? minArgumentName
                        : min.ToString(format)
                )
            );
        }
    }

    public static void DateTimeGreaterThanOrEqual(
        this IGuardClause guardClause,
        DateTime argument,
        string argumentName,
        DateTime max,
        string maxArgumentName = "",
        string format = "dd/MM/yyyy"
    )
    {
        if (argument >= max)
        {
            throw new ArgumentException(
                string.Format(
                    "{0} is not allowing greater than or equals to {1}",
                    argumentName,
                    !string.IsNullOrWhiteSpace(maxArgumentName)
                        ? maxArgumentName
                        : max.ToString(format)
                )
            );
        }
    }

    public static void DateTimeOutOfRange(
        this IGuardClause guardClause,
        DateTime argument,
        string argumentName,
        DateTime startRange,
        DateTime endRange
    )
    {
        if (argument < startRange || argument > endRange)
        {
            throw new ArgumentException(string.Format("{0} is out of range", argumentName));
        }
    }
}
