﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MadEyeMatt.Extensions.Identity.Permissions.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("MadEyeMatt.Extensions.Identity.Permissions.Properties.Resources", typeof(Resources).Assembly);
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
        ///   Looks up a localized string similar to Duplicate permission name &apos;{0}&apos;..
        /// </summary>
        internal static string DuplicatePermissionName {
            get {
                return ResourceManager.GetString("DuplicatePermissionName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Duplicate tenant name &apos;{0}&apos;..
        /// </summary>
        internal static string DuplicateTenantName {
            get {
                return ResourceManager.GetString("DuplicateTenantName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid permission name &apos;{0}&apos;..
        /// </summary>
        internal static string InvalidPermissionName {
            get {
                return ResourceManager.GetString("InvalidPermissionName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid tenant name &apos;{0}&apos;..
        /// </summary>
        internal static string InvalidTenantName {
            get {
                return ResourceManager.GetString("InvalidTenantName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Tenant already in role &apos;{0}&apos;..
        /// </summary>
        internal static string TenantAlreadyInRole {
            get {
                return ResourceManager.GetString("TenantAlreadyInRole", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Tenant is not in role &apos;{0}&apos;..
        /// </summary>
        internal static string TenantNotInRole {
            get {
                return ResourceManager.GetString("TenantNotInRole", resourceCulture);
            }
        }
    }
}
