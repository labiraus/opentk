//
// CLContextProperties.cs
//
// Copyright (C) 2020 OpenTK
//
// This software may be modified and distributed under the terms
// of the MIT license. See the LICENSE file for details.
//

using System;
using System.Collections.Generic;

namespace OpenTK.Compute.OpenCL
{
    /// <summary>
    /// Convenience class for handling CLContext Properties
    /// </summary>
    public class CLContextProperties
    {
        public CLPlatform ContextPlatform { get; set; }
        public IntPtr? GlContextKHR { get; set; }
        public IntPtr? EglDisplayKHR { get; set; }
        public IntPtr? GlxDisplayKHR { get; set; }
        public IntPtr? WglHDCKHR { get; set; }
        public IntPtr? CglShareGroupKHR { get; set; }
        public IntPtr? ContextInteropUserSync { get; set; }
        public IntPtr? ContextD3D10DeviceKHR { get; set; }
        public IntPtr? ContextD3D11DeviceKHR { get; set; }
        public IntPtr? ContextAdapterD3D9KHR { get; set; }
        public IntPtr? ContextAdapterD3D9ExKHR { get; set; }
        public IntPtr? ContextAdapterDXVAKHR { get; set; }
        public IntPtr? ContextMemoryInitializeKHR { get; set; }
        public IntPtr? ContextTerminateKHR { get; set; }

        /// <summary>
        /// Gets or sets additional properties.
        /// Will usually be the major and minor version numbers of the context.
        /// </summary>
        public IntPtr[] AdditionalProperties { get; set; }

        public CLContextProperties(){

        }

        public CLContextProperties(
            CLPlatform contextPlatform,
            int? contextInteropUserSync,
            int? glContextKHR,
            int? eglDisplayKHR,
            int? glxDisplayKHR,
            int? wglHDCKHR,
            int? cglShareGroupKHR,
            int? contextD3D10DeviceKHR,
            int? contextD3D11DeviceKHR,
            int? contextAdapterD3D9KHR,
            int? contextAdapterD3D9ExKHR,
            int? contextAdapterDXVAKHR,
            int? contextMemoryInitializeKHR,
            int? contextTerminateKHR)
        {
            if (contextPlatform != null)
            {
                ContextPlatform = contextPlatform;
            }
            if (contextInteropUserSync.HasValue)
            {
                ContextInteropUserSync = new IntPtr(contextInteropUserSync.Value);
            }
            if (glContextKHR.HasValue)
            {
                GlContextKHR = new IntPtr(glContextKHR.Value);
            }
            if (eglDisplayKHR.HasValue)
            {
                EglDisplayKHR = new IntPtr(eglDisplayKHR.Value);
            }
            if (glxDisplayKHR.HasValue)
            {
                GlxDisplayKHR = new IntPtr(glxDisplayKHR.Value);
            }
            if (wglHDCKHR.HasValue)
            {
                WglHDCKHR = new IntPtr(wglHDCKHR.Value);
            }
            if (cglShareGroupKHR.HasValue)
            {
                CglShareGroupKHR = new IntPtr(cglShareGroupKHR.Value);
            }
            if (contextD3D10DeviceKHR.HasValue)
            {
                ContextD3D10DeviceKHR = new IntPtr(contextD3D10DeviceKHR.Value);
            }
            if (contextD3D11DeviceKHR.HasValue)
            {
                ContextD3D11DeviceKHR = new IntPtr(contextD3D11DeviceKHR.Value);
            }
            if (contextAdapterD3D9KHR.HasValue)
            {
                ContextAdapterD3D9KHR = new IntPtr(contextAdapterD3D9KHR.Value);
            }
            if (contextAdapterD3D9ExKHR.HasValue)
            {
                ContextAdapterD3D9ExKHR = new IntPtr(contextAdapterD3D9ExKHR.Value);
            }
            if (contextAdapterDXVAKHR.HasValue)
            {
                ContextAdapterDXVAKHR = new IntPtr(contextAdapterDXVAKHR.Value);
            }
            if (contextMemoryInitializeKHR.HasValue)
            {
                ContextMemoryInitializeKHR = new IntPtr(contextMemoryInitializeKHR.Value);
            }
            if (contextTerminateKHR.HasValue)
            {
                ContextTerminateKHR = new IntPtr(contextTerminateKHR.Value);
            }
        }


        /// <summary>
        /// Converts these context properties to a <see cref="CL.CreateContext(IntPtr[], CLDevice[], IntPtr, IntPtr, out CLResultCode)"/> compatible list.
        /// Alternativly, consider using the more convenient <see cref="CL.CreateContext(CLContextProperties, CLDevice[], IntPtr, IntPtr, out CLResultCode)"/> overload.
        /// </summary>
        /// <returns>The attibute list in the form of a span.</returns>
        public IntPtr[] CreatePropertyArray()
        {
            // The number of members * 2 + AdditionalProperties
            List<IntPtr> propertyList = new List<IntPtr>();
            int index = 0;

            void AddProperty(IntPtr? value, CLContext.Property property)
            {
                if (value != null)
                {
                    propertyList.Add((IntPtr)property);
                    propertyList.Add(value.Value);
                }
            }

            AddProperty(ContextPlatform, CLContext.Property.ContextPlatform);
            AddProperty(GlContextKHR, CLContext.Property.GlContextKHR);
            AddProperty(EglDisplayKHR, CLContext.Property.EglDisplayKHR);
            AddProperty(GlxDisplayKHR, CLContext.Property.GlxDisplayKHR);
            AddProperty(WglHDCKHR, CLContext.Property.WglHDCKHR);
            AddProperty(CglShareGroupKHR, CLContext.Property.CglShareGroupKHR);
            AddProperty(ContextInteropUserSync, CLContext.Property.ContextInteropUserSync);
            AddProperty(ContextD3D10DeviceKHR, CLContext.Property.ContextD3D10DeviceKHR);
            AddProperty(ContextD3D11DeviceKHR, CLContext.Property.ContextD3D11DeviceKHR);
            AddProperty(ContextAdapterD3D9KHR, CLContext.Property.ContextAdapterD3D9KHR);
            AddProperty(ContextAdapterD3D9ExKHR, CLContext.Property.ContextAdapterD3D9ExKHR);
            AddProperty(ContextAdapterDXVAKHR, CLContext.Property.ContextAdapterDXVAKHR);
            AddProperty(ContextMemoryInitializeKHR, CLContext.Property.ContextMemoryInitializeKHR);
            AddProperty(ContextTerminateKHR, CLContext.Property.ContextTerminateKHR);

            if (AdditionalProperties != null)
            {
                propertyList.AddRange(AdditionalProperties);
            }

            // Add the trailing null byte.
            propertyList.Add(IntPtr.Zero);

            return propertyList.ToArray();
        }

        /// <summary>
        /// Parses a CL context property list.
        /// </summary>
        /// <param name="propertyArray">The CL context attribute list.</param>
        /// <returns>The parsed <see cref="CLContextProperties"/> object.</returns>
        internal static CLContextProperties FromArray(IntPtr[] propertyArray)
        {
            List<IntPtr> extra = new List<IntPtr>();
            CLContextProperties properties = new CLContextProperties();

            void ParseAttribute(IntPtr @enum, IntPtr value)
            {
                switch (@enum.ToInt32())
                {
                    case (int)CLContext.Property.ContextPlatform:
                        properties.ContextPlatform = new CLPlatform(value);
                        break;
                    case (int)CLContext.Property.GlContextKHR:
                        properties.GlContextKHR = value;
                        break;
                    case (int)CLContext.Property.EglDisplayKHR:
                        properties.EglDisplayKHR = value;
                        break;
                    case (int)CLContext.Property.GlxDisplayKHR:
                        properties.GlxDisplayKHR = value;
                        break;
                    case (int)CLContext.Property.WglHDCKHR:
                        properties.WglHDCKHR = value;
                        break;
                    case (int)CLContext.Property.CglShareGroupKHR:
                        properties.CglShareGroupKHR = value;
                        break;
                    case (int)CLContext.Property.ContextInteropUserSync:
                        properties.ContextInteropUserSync = value;
                        break;
                    case (int)CLContext.Property.ContextD3D10DeviceKHR:
                        properties.ContextD3D10DeviceKHR = value;
                        break;
                    case (int)CLContext.Property.ContextD3D11DeviceKHR:
                        properties.ContextD3D11DeviceKHR = value;
                        break;
                    case (int)CLContext.Property.ContextAdapterD3D9KHR:
                        properties.ContextAdapterD3D9KHR = value;
                        break;
                    case (int)CLContext.Property.ContextAdapterD3D9ExKHR:
                        properties.ContextAdapterD3D9ExKHR = value;
                        break;
                    case (int)CLContext.Property.ContextAdapterDXVAKHR:
                        properties.ContextAdapterDXVAKHR = value;
                        break;
                    case (int)CLContext.Property.ContextMemoryInitializeKHR:
                        properties.ContextMemoryInitializeKHR = value;
                        break;
                    case (int)CLContext.Property.ContextTerminateKHR:
                        properties.ContextTerminateKHR = value;
                        break;
                    default:
                        extra.Add(@enum); extra.Add(value);
                        break;
                }
            }

            for (int i = 0; i < propertyArray.Length - 1; i += 2)
            {
                ParseAttribute(propertyArray[i], propertyArray[i + 1]);
            }

            properties.AdditionalProperties = extra.ToArray();

            return properties;
        }

        // Used for ToString.
        private string GetOptionalString<IntPtr>(string title, IntPtr value)
        {
            if (value == null)
            {
                return null;
            }
            else
            {
                return $"{title}: {value}";
            }
        }

        /// <summary>
        /// Converts the attributes to a string representation.
        /// </summary>
        /// <returns>The string representation of the attributes.</returns>
        public override string ToString()
        {
            return $"{GetOptionalString(nameof(ContextPlatform), ContextPlatform)}, " +
                $"{GetOptionalString(nameof(GlContextKHR), GlContextKHR)}, " +
                $"{GetOptionalString(nameof(EglDisplayKHR), EglDisplayKHR)}, " +
                $"{GetOptionalString(nameof(GlxDisplayKHR), GlxDisplayKHR)}, " +
                $"{GetOptionalString(nameof(WglHDCKHR), WglHDCKHR)}, " +
                $"{GetOptionalString(nameof(CglShareGroupKHR), CglShareGroupKHR)}, " +
                $"{GetOptionalString(nameof(ContextInteropUserSync), ContextInteropUserSync)}, " +
                $"{GetOptionalString(nameof(ContextD3D10DeviceKHR), ContextD3D10DeviceKHR)}, " +
                $"{GetOptionalString(nameof(ContextD3D11DeviceKHR), ContextD3D11DeviceKHR)}, " +
                $"{GetOptionalString(nameof(ContextAdapterD3D9KHR), ContextAdapterD3D9KHR)}, " +
                $"{GetOptionalString(nameof(ContextAdapterD3D9ExKHR), ContextAdapterD3D9ExKHR)}, " +
                $"{GetOptionalString(nameof(ContextAdapterDXVAKHR), ContextAdapterDXVAKHR)}, " +
                $"{GetOptionalString(nameof(ContextMemoryInitializeKHR), ContextMemoryInitializeKHR)}, " +
                $"{GetOptionalString(nameof(ContextTerminateKHR), ContextTerminateKHR)}, " +
                $"{((AdditionalProperties != null) ? ", " + string.Join(", ", AdditionalProperties) : string.Empty)}";
        }
    }
}
