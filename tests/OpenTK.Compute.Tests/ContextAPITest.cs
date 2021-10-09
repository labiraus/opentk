using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenTK.Compute.OpenCL;
using System;
using System.Linq;

namespace OpenTK.Compute.Tests
{
	[TestClass]
	public class ContextAPITest
    {
        CLPlatform platform;

        [TestInitialize()]
        public void Starup(){
            CL.GetPlatformIds(out CLPlatform[] platformIds);
            platform = platformIds[0];
        }

        [TestMethod]
        public void CreateContext()
        {
            var properties = new CLContextProperties(platform, false);
            platform.GetDeviceIds(CLDevice.Type.All, out CLDevice[] devices);
            var context = properties.CreateContext(devices, null, IntPtr.Zero, out CLResultCode resultCode);
            context.ReleaseContext();
            Assert.AreEqual(CLResultCode.Success, resultCode);
        }

        [TestMethod]
        public void CreateContextFromType()
        {
            var properties = new CLContextProperties(platform, false);
            var context = properties.CreateContextFromType(CLDevice.Type.Default, null, IntPtr.Zero, out CLResultCode resultCode);
            context.ReleaseContext();
            Assert.AreEqual(CLResultCode.Success, resultCode);
        }

        [TestMethod]
        public void RetainContext()
        {
            var properties = new CLContextProperties(platform, false);
            var context = properties.CreateContextFromType(CLDevice.Type.Default, null, IntPtr.Zero, out _);
            var resultCode = context.RetainContext();
            Assert.AreEqual(CLResultCode.Success, resultCode);
            context.ReleaseContext();
            resultCode = context.ReleaseContext();
            Assert.AreEqual(CLResultCode.Success, resultCode);
        }

        [TestMethod]
        public void ReleaseContext()
        {
            var properties = new CLContextProperties(platform, false);
            var context = properties.CreateContextFromType(CLDevice.Type.Default, null, IntPtr.Zero, out _);
            var resultCode = context.ReleaseContext();
            Assert.AreEqual(CLResultCode.Success, resultCode);
        }

        [TestMethod]
        [DataRow(CLContext.Info.Devices)]
        [DataRow(CLContext.Info.NumberOfDevices)]
        [DataRow(CLContext.Info.ReferenceCount)]
        [DataRow(CLContext.Info.Properties)]
        public void GetContextInfo(CLContext.Info param)
        {
            var properties = new CLContextProperties(platform, false);
            var context = properties.CreateContextFromType(CLDevice.Type.Default, null, IntPtr.Zero, out _);
            var resultCode = context.GetContextInfo(param, out byte[] paramValue);
            context.ReleaseContext();
            Assert.AreEqual(CLResultCode.Success, resultCode);
            Assert.IsTrue(paramValue.Length > 0);
        }
    }
}