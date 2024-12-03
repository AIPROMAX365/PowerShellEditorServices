// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Microsoft.PowerShell.EditorServices.Services.DebugAdapter;
using OmniSharp.Extensions.DebugAdapter.Protocol.Models;

namespace Microsoft.PowerShell.EditorServices.Utility
{
    internal static class LspDebugUtils
    {
        internal static Breakpoint CreateBreakpoint(
            BreakpointDetails breakpointDetails)
        {
            Validate.IsNotNull(nameof(breakpointDetails), breakpointDetails);

            return new Breakpoint
            {
                Id = breakpointDetails.Id,
                Verified = breakpointDetails.Verified,
                Message = breakpointDetails.Message,
                Source = new Source { Path = breakpointDetails.Source },
                Line = breakpointDetails.LineNumber,
                Column = breakpointDetails.ColumnNumber
            };
        }

        internal static Breakpoint CreateBreakpoint(
            CommandBreakpointDetails breakpointDetails)
        {
            Validate.IsNotNull(nameof(breakpointDetails), breakpointDetails);

            return new Breakpoint
            {
                Verified = breakpointDetails.Verified,
                Message = breakpointDetails.Message
            };
        }

        public static Breakpoint CreateBreakpoint(
            SourceBreakpoint sourceBreakpoint,
            string source,
            string message,
            bool verified = false)
        {
            Validate.IsNotNull(nameof(sourceBreakpoint), sourceBreakpoint);
            Validate.IsNotNull(nameof(source), source);
            Validate.IsNotNull(nameof(message), message);

            return new Breakpoint
            {
                Verified = verified,
                Message = message,
                Source = new Source { Path = source },
                Line = sourceBreakpoint.Line,
                Column = sourceBreakpoint.Column
            };
        }

        public static Scope CreateScope(VariableScope scope)
        {
            return new Scope
            {
                Name = scope.Name,
                VariablesReference = scope.Id,
                // Temporary fix for #95 to get debug hover tips to work well at least for the local scope.
                Expensive = scope.Name is not VariableContainerDetails.LocalScopeName and
                             not VariableContainerDetails.AutoVariablesName
            };
        }

        public static Variable CreateVariable(VariableDetailsBase variable)
        {
            return new Variable
            {
                Name = variable.Name,
                Value = variable.ValueString ?? string.Empty,
                Type = variable.Type,
                EvaluateName = variable.Name,
                VariablesReference =
                    variable.IsExpandable ?
                        variable.Id : 0
            };
        }
    }
}
