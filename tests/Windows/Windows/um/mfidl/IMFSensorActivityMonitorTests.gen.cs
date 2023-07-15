// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.
// Ported from um/mfidl.h in the Windows SDK for Windows 10.0.22621.0
// Original source is Copyright © Microsoft. All rights reserved.
using NUnit.Framework;
using System;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using static TerraFX.Interop.Windows.IID;

namespace TerraFX.Interop.Windows.UnitTests;
/// <summary>Provides validation of the <see cref = "IMFSensorActivityMonitor"/> struct.</summary>
[SupportedOSPlatform("windows10.0.15063.0")]
public static unsafe partial class IMFSensorActivityMonitorTests
{
    /// <summary>Validates that the <see cref = "Guid"/> of the <see cref = "IMFSensorActivityMonitor"/> struct is correct.</summary>
    [Test]
    public static void GuidOfTest()
    {
        Assert.That(typeof(IMFSensorActivityMonitor).GUID, Is.EqualTo(IID_IMFSensorActivityMonitor));
    }

    /// <summary>Validates that the <see cref = "IMFSensorActivityMonitor"/> struct is blittable.</summary>
    [Test]
    public static void IsBlittableTest()
    {
        Assert.That(Marshal.SizeOf<IMFSensorActivityMonitor>(), Is.EqualTo(sizeof(IMFSensorActivityMonitor)));
    }

    /// <summary>Validates that the <see cref = "IMFSensorActivityMonitor"/> struct has the right <see cref = "LayoutKind"/>.</summary>
    [Test]
    public static void IsLayoutSequentialTest()
    {
        Assert.That(typeof(IMFSensorActivityMonitor).IsLayoutSequential, Is.True);
    }

    /// <summary>Validates that the <see cref = "IMFSensorActivityMonitor"/> struct has the correct size.</summary>
    [Test]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.That(sizeof(IMFSensorActivityMonitor), Is.EqualTo(8));
        }
        else
        {
            Assert.That(sizeof(IMFSensorActivityMonitor), Is.EqualTo(4));
        }
    }
}