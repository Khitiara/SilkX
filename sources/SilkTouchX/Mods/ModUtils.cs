﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using ClangSharp;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace SilkTouchX.Mods;

/// <summary>
/// Common utilities useful to mods.
/// </summary>
public static class ModUtils
{
    /// <summary>
    /// Converts a namespace string into an <see cref="NameSyntax"/>.
    /// </summary>
    /// <param name="ns">The namespace string.</param>
    /// <returns>The <see cref="NameSyntax"/> representing this namespace.</returns>
    public static NameSyntax NamespaceIntoIdentifierName(ReadOnlySpan<char> ns)
    {
        var idx = ns.LastIndexOf('.');
        if (idx == -1)
        {
            return IdentifierName(ns.ToString());
        }

        return QualifiedName(
            NamespaceIntoIdentifierName(ns[..idx]),
            IdentifierName(ns[(idx + 1)..].ToString())
        );
    }

    /// <summary>
    /// Matches a potential replacement candidate against the given list of regex-replacement mappings and, if a regex
    /// matches, performs a group substitution on the replacement to replace the match.
    /// </summary>
    /// <param name="mappings">
    /// Regex-to-replacement mappings. The replacements can have group references that will get substituted in (e.g.
    /// <c>"Silk.NET.$1</c>)
    /// </param>
    /// <param name="candidate">The string to run the replacement on.</param>
    /// <returns>The replaced string. It may not have been modified at all.</returns>
    public static string GroupedRegexReplace(
        IEnumerable<(Regex Regex, string Replacement)> mappings,
        string candidate
    )
    {
        foreach (var (regex, replacement) in mappings)
        {
            var match = regex.Match(candidate);
            if (!match.Success)
            {
                continue;
            }

            candidate = replacement;
            for (var i = 0; i < match.Groups.Count; i++)
            {
                candidate = candidate.Replace($"${i}", match.Groups[i].Value);
            }
        }

        return candidate;
    }

    /// <summary>
    /// Reconstructs the <see cref="PInvokeGeneratorConfigurationOptions"/> from the given
    /// <see cref="PInvokeGeneratorConfiguration"/> because this is a PITA to do manually.
    /// </summary>
    /// <param name="cfg">The ClangSharp config.</param>
    /// <returns>The options contained within.</returns>
    public static PInvokeGeneratorConfigurationOptions ReconstructOptions(
        this PInvokeGeneratorConfiguration cfg
    )
    {
        var options = PInvokeGeneratorConfigurationOptions.None;
        options |= cfg.DontUseUsingStaticsForEnums
            ? PInvokeGeneratorConfigurationOptions.DontUseUsingStaticsForEnums
            : PInvokeGeneratorConfigurationOptions.None;
        options |= cfg.ExcludeAnonymousFieldHelpers
            ? PInvokeGeneratorConfigurationOptions.ExcludeAnonymousFieldHelpers
            : PInvokeGeneratorConfigurationOptions.None;
        options |= cfg.ExcludeComProxies
            ? PInvokeGeneratorConfigurationOptions.ExcludeComProxies
            : PInvokeGeneratorConfigurationOptions.None;
        options |= cfg.ExcludeEmptyRecords
            ? PInvokeGeneratorConfigurationOptions.ExcludeEmptyRecords
            : PInvokeGeneratorConfigurationOptions.None;
        options |= cfg.ExcludeEnumOperators
            ? PInvokeGeneratorConfigurationOptions.ExcludeEnumOperators
            : PInvokeGeneratorConfigurationOptions.None;
        options |= cfg.ExcludeFnptrCodegen
            ? PInvokeGeneratorConfigurationOptions.ExcludeFnptrCodegen
            : PInvokeGeneratorConfigurationOptions.None;
        options |= cfg.ExcludeFunctionsWithBody
            ? PInvokeGeneratorConfigurationOptions.ExcludeFunctionsWithBody
            : PInvokeGeneratorConfigurationOptions.None;
        options |= cfg.ExcludeNIntCodegen
            ? PInvokeGeneratorConfigurationOptions.ExcludeNIntCodegen
            : PInvokeGeneratorConfigurationOptions.None;
        options |= cfg.GenerateAggressiveInlining
            ? PInvokeGeneratorConfigurationOptions.GenerateAggressiveInlining
            : PInvokeGeneratorConfigurationOptions.None;
        options |= cfg.GenerateCompatibleCode
            ? PInvokeGeneratorConfigurationOptions.GenerateCompatibleCode
            : PInvokeGeneratorConfigurationOptions.None;
        options |= cfg.GenerateCppAttributes
            ? PInvokeGeneratorConfigurationOptions.GenerateCppAttributes
            : PInvokeGeneratorConfigurationOptions.None;
        options |= cfg.GenerateDocIncludes
            ? PInvokeGeneratorConfigurationOptions.GenerateDocIncludes
            : PInvokeGeneratorConfigurationOptions.None;
        options |= cfg.GenerateExplicitVtbls
            ? PInvokeGeneratorConfigurationOptions.GenerateExplicitVtbls
            : PInvokeGeneratorConfigurationOptions.None;
        options |= cfg.GenerateFileScopedNamespaces
            ? PInvokeGeneratorConfigurationOptions.GenerateFileScopedNamespaces
            : PInvokeGeneratorConfigurationOptions.None;
        options |= cfg.GenerateGuidMember
            ? PInvokeGeneratorConfigurationOptions.GenerateGuidMember
            : PInvokeGeneratorConfigurationOptions.None;
        options |= cfg.GenerateHelperTypes
            ? PInvokeGeneratorConfigurationOptions.GenerateHelperTypes
            : PInvokeGeneratorConfigurationOptions.None;
        options |= cfg.GenerateLatestCode
            ? PInvokeGeneratorConfigurationOptions.GenerateLatestCode
            : PInvokeGeneratorConfigurationOptions.None;
        options |= cfg.GenerateMacroBindings
            ? PInvokeGeneratorConfigurationOptions.GenerateMacroBindings
            : PInvokeGeneratorConfigurationOptions.None;
        options |= cfg.GenerateMarkerInterfaces
            ? PInvokeGeneratorConfigurationOptions.GenerateMarkerInterfaces
            : PInvokeGeneratorConfigurationOptions.None;
        options |= cfg.GenerateMultipleFiles
            ? PInvokeGeneratorConfigurationOptions.GenerateMultipleFiles
            : PInvokeGeneratorConfigurationOptions.None;
        options |= cfg.GenerateNativeBitfieldAttribute
            ? PInvokeGeneratorConfigurationOptions.GenerateNativeBitfieldAttribute
            : PInvokeGeneratorConfigurationOptions.None;
        options |= cfg.GenerateNativeInheritanceAttribute
            ? PInvokeGeneratorConfigurationOptions.GenerateNativeInheritanceAttribute
            : PInvokeGeneratorConfigurationOptions.None;
        options |= cfg.GeneratePreviewCode
            ? PInvokeGeneratorConfigurationOptions.GeneratePreviewCode
            : PInvokeGeneratorConfigurationOptions.None;
        options |= cfg.GenerateSetsLastSystemErrorAttribute
            ? PInvokeGeneratorConfigurationOptions.GenerateSetsLastSystemErrorAttribute
            : PInvokeGeneratorConfigurationOptions.None;
        options |= cfg.GenerateSourceLocationAttribute
            ? PInvokeGeneratorConfigurationOptions.GenerateSourceLocationAttribute
            : PInvokeGeneratorConfigurationOptions.None;
        options |= cfg.GenerateTemplateBindings
            ? PInvokeGeneratorConfigurationOptions.GenerateTemplateBindings
            : PInvokeGeneratorConfigurationOptions.None;
        options |= cfg.GenerateTestsNUnit
            ? PInvokeGeneratorConfigurationOptions.GenerateTestsNUnit
            : PInvokeGeneratorConfigurationOptions.None;
        options |= cfg.GenerateTestsXUnit
            ? PInvokeGeneratorConfigurationOptions.GenerateTestsXUnit
            : PInvokeGeneratorConfigurationOptions.None;
        options |= cfg.GenerateTrimmableVtbls
            ? PInvokeGeneratorConfigurationOptions.GenerateTrimmableVtbls
            : PInvokeGeneratorConfigurationOptions.None;
        options |= cfg.GenerateUnixTypes
            ? PInvokeGeneratorConfigurationOptions.GenerateUnixTypes
            : PInvokeGeneratorConfigurationOptions.None;
        options |= cfg.GenerateUnmanagedConstants
            ? PInvokeGeneratorConfigurationOptions.GenerateUnmanagedConstants
            : PInvokeGeneratorConfigurationOptions.None;
        options |= cfg.GenerateVtblIndexAttribute
            ? PInvokeGeneratorConfigurationOptions.GenerateVtblIndexAttribute
            : PInvokeGeneratorConfigurationOptions.None;
        options |= cfg.LogExclusions
            ? PInvokeGeneratorConfigurationOptions.LogExclusions
            : PInvokeGeneratorConfigurationOptions.None;
        options |= cfg.LogPotentialTypedefRemappings
            ? PInvokeGeneratorConfigurationOptions.LogPotentialTypedefRemappings
            : PInvokeGeneratorConfigurationOptions.None;
        options |= cfg.LogVisitedFiles
            ? PInvokeGeneratorConfigurationOptions.LogVisitedFiles
            : PInvokeGeneratorConfigurationOptions.None;
        if ((options & PInvokeGeneratorConfigurationOptions.GeneratePreviewCode) != 0)
        {
            options &= ~PInvokeGeneratorConfigurationOptions.GenerateCompatibleCode;
            options &= ~PInvokeGeneratorConfigurationOptions.GenerateLatestCode;
        }
        if ((options & PInvokeGeneratorConfigurationOptions.GenerateLatestCode) != 0)
        {
            options &= ~PInvokeGeneratorConfigurationOptions.GeneratePreviewCode;
            options &= ~PInvokeGeneratorConfigurationOptions.GenerateCompatibleCode;
        }
        if ((options & PInvokeGeneratorConfigurationOptions.GenerateCompatibleCode) != 0)
        {
            options &= ~PInvokeGeneratorConfigurationOptions.GeneratePreviewCode;
            options &= ~PInvokeGeneratorConfigurationOptions.GenerateLatestCode;
        }
        return options;
    }

    /// <summary>
    /// Determines (naively) whether the given attribute syntax represents a <see cref="DllImportAttribute"/>.
    /// </summary>
    /// <param name="node">The attribute syntax.</param>
    /// <returns>Whether it is probably a DllImport.</returns>
    public static bool IsDllImport(this AttributeSyntax node)
    {
        var sep = node.Name.ToString().Split("::").Last();
        return sep == "DllImport"
            || sep == "DllImportAttribute"
            || sep.EndsWith("System.Runtime.InteropServices.DllImport")
            || sep.EndsWith("System.Runtime.InteropServices.DllImportAttribute");
    }

    /// <summary>
    /// Determines (naively) whether the given attribute syntax represents a <see cref="DllImportAttribute"/> with
    /// <see cref="DllImportAttribute.ExactSpelling"/> set to <c>true</c>.
    /// </summary>
    /// <param name="node">The attribute syntax.</param>
    /// <returns>Whether it is probably an ExactSpelling DllImport.</returns>
    public static bool IsExactSpellingDllImport(this AttributeSyntax node) =>
        node.IsDllImport()
        && (
            node.ArgumentList?.Arguments.Any(
                x =>
                    x.NameEquals?.Name.Identifier.ToString()
                        == nameof(DllImportAttribute.ExactSpelling)
                    && x.Expression.ToString() == "true"
            ) ?? false
        );

    /// <summary>
    /// Adds <see cref="MaxOpt"/> to the given <see cref="MethodDeclarationSyntax"/>'s attribute lists.
    /// </summary>
    /// <param name="meth">The method.</param>
    /// <returns>The modified method.</returns>
    public static MethodDeclarationSyntax AddMaxOpt(this MethodDeclarationSyntax meth) =>
        meth.AddAttributeLists(MaxOpt);

    /// <summary>
    /// Gets an attribute list representing a <see cref="System.Runtime.CompilerServices.MethodImplAttribute"/> with
    /// <see cref="System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining"/> and
    /// <see cref="System.Runtime.CompilerServices.MethodImplOptions.AggressiveOptimization"/> set.
    /// </summary>
    public static readonly AttributeListSyntax MaxOpt = AttributeList(
        SingletonSeparatedList(
            Attribute(IdentifierName("MethodImpl"))
                .WithArgumentList(
                    AttributeArgumentList(
                        SingletonSeparatedList(
                            AttributeArgument(
                                BinaryExpression(
                                    SyntaxKind.BitwiseOrExpression,
                                    MemberAccessExpression(
                                        SyntaxKind.SimpleMemberAccessExpression,
                                        IdentifierName("MethodImplOptions"),
                                        IdentifierName("AggressiveInlining")
                                    ),
                                    MemberAccessExpression(
                                        SyntaxKind.SimpleMemberAccessExpression,
                                        IdentifierName("MethodImplOptions"),
                                        IdentifierName("AggressiveOptimization")
                                    )
                                )
                            )
                        )
                    )
                )
        )
    );
}
