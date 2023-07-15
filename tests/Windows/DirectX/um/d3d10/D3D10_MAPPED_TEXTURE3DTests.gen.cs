// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.
// Ported from um/d3d10.h in the Windows SDK for Windows 10.0.22621.0
// Original source is Copyright © Microsoft. All rights reserved.
using NUnit.Framework;
using System;
using System.Runtime.InteropServices;

namespace TerraFX.Interop.DirectX.UnitTests;
/// <summary>Provides validation of the <see cref = "D3D10_MAPPED_TEXTURE3D"/> struct.</summary>
public static unsafe partial class D3D10_MAPPED_TEXTURE3DTests
{
    /// <summary>Validates that the <see cref = "D3D10_MAPPED_TEXTURE3D"/> struct is blittable.</summary>
    [Test]
    public static void IsBlittableTest()
    {
        Assert.That(Marshal.SizeOf<D3D10_MAPPED_TEXTURE3D>(), Is.EqualTo(sizeof(D3D10_MAPPED_TEXTURE3D)));
    }

    /// <summary>Validates that the <see cref = "D3D10_MAPPED_TEXTURE3D"/> struct has the right <see cref = "LayoutKind"/>.</summary>
    [Test]
    public static void IsLayoutSequentialTest()
    {
        Assert.That(typeof(D3D10_MAPPED_TEXTURE3D).IsLayoutSequential, Is.True);
    }

    /// <summary>Validates that the <see cref = "D3D10_MAPPED_TEXTURE3D"/> struct has the correct size.</summary>
    [Test]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.That(sizeof(D3D10_MAPPED_TEXTURE3D), Is.EqualTo(16));
        }
        else
        {
            Assert.That(sizeof(D3D10_MAPPED_TEXTURE3D), Is.EqualTo(12));
        }
    }
}