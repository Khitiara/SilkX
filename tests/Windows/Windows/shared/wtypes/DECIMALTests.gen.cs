// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.
// Ported from shared/wtypes.h in the Windows SDK for Windows 10.0.22621.0
// Original source is Copyright © Microsoft. All rights reserved.
using NUnit.Framework;
using System.Runtime.InteropServices;

namespace TerraFX.Interop.Windows.UnitTests;
/// <summary>Provides validation of the <see cref = "DECIMAL"/> struct.</summary>
public static unsafe partial class DECIMALTests
{
    /// <summary>Validates that the <see cref = "DECIMAL"/> struct is blittable.</summary>
    [Test]
    public static void IsBlittableTest()
    {
        Assert.That(Marshal.SizeOf<DECIMAL>(), Is.EqualTo(sizeof(DECIMAL)));
    }

    /// <summary>Validates that the <see cref = "DECIMAL"/> struct has the right <see cref = "LayoutKind"/>.</summary>
    [Test]
    public static void IsLayoutSequentialTest()
    {
        Assert.That(typeof(DECIMAL).IsLayoutSequential, Is.True);
    }

    /// <summary>Validates that the <see cref = "DECIMAL"/> struct has the correct size.</summary>
    [Test]
    public static void SizeOfTest()
    {
        Assert.That(sizeof(DECIMAL), Is.EqualTo(16));
    }
}