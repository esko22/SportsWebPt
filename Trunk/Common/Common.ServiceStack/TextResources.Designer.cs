﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18034
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SportsWebPt.Common.ServiceStack.Infrastructure {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class TextResources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal TextResources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("SportWebPt.Common.ServiceStack.Infrastructure.TextResources", typeof(TextResources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Bad or expired token.  This was because of not having a valid token, please authenticate..
        /// </summary>
        internal static string Error401 {
            get {
                return ResourceManager.GetString("Error401", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to You don&apos;t have access to the requested resource or operation.
        /// </summary>
        internal static string Error403 {
            get {
                return ResourceManager.GetString("Error403", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to This isn&apos;t a recognized path. Please refer to the API documentation at for the available resources..
        /// </summary>
        internal static string Error404 {
            get {
                return ResourceManager.GetString("Error404", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to We had an unexpected issue.  Please try your request again.  Be assured that we are looking into it!.
        /// </summary>
        internal static string Error500 {
            get {
                return ResourceManager.GetString("Error500", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The service is currently not available.  Please try your request again later..
        /// </summary>
        internal static string Error503 {
            get {
                return ResourceManager.GetString("Error503", resourceCulture);
            }
        }
    }
}
