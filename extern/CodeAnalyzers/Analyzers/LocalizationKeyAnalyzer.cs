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

using System.Text.RegularExpressions;
using CodeAnalyzers.Analyzers.Models;
using MiniCommon.IO;
using MiniCommon.Logger.Enums;
using MiniCommon.Models;
using MiniCommon.Providers;
using MiniCommon.Validation;
using MiniCommon.Validation.Validators;

namespace CodeAnalyzers.Analyzers;

public static partial class LocalizationKeyAnalyzer
{
    /// <summary>
    /// Analyze all files, find all occurences of NotificationService and check that all used localization keys are valid.
    /// </summary>
    public static void Analyze(string[] files, Localization localization)
    {
        int success = 0;
        int fail = 0;

        if (Validate.For.IsNullOrEmpty(localization?.Entries))
            return;

        foreach (string file in files)
        {
            if (AnalyzerFiles.Restricted.Exists(file.Contains))
                continue;

            Regex matchNotification = NotificationRegex();
            MatchCollection notificationMatches = matchNotification.Matches(VFS.ReadAllText(file));

            foreach (Match match in notificationMatches)
            {
                Regex matchQuotes = QuoteRegex();
                Match quoteMatch = matchQuotes.Match(match.Value);

                if (Validate.For.IsNullOrWhiteSpace([quoteMatch?.Value], NativeLogLevel.Debug))
                    continue;

                if (
                    !localization!.Entries!.ContainsKey(
                        quoteMatch!.Value!.Replace("\"", string.Empty)
                    )
                )
                {
                    fail++;
                    NotificationProvider.Error(
                        "analyzer.error.localization",
                        file,
                        quoteMatch!.Value
                    );
                }
                else
                {
                    success++;
                }
            }
        }

        NotificationProvider.Info(
            "analyzer.output",
            nameof(LocalizationKeyAnalyzer),
            success.ToString(),
            fail.ToString(),
            (success + fail).ToString()
        );
    }

    [GeneratedRegex(@"NotificationProvider\.[^;]*\);(?=\s|$)")]
    private static partial Regex NotificationRegex();

    [GeneratedRegex("\"([^\"]*)\"")]
    private static partial Regex QuoteRegex();
}
