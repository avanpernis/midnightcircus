﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.3082
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TokenAssist.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0")]
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
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("TokenAssist.Properties.Resources", typeof(Resources).Assembly);
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
        
        internal static System.Drawing.Bitmap dropbox {
            get {
                object obj = ResourceManager.GetObject("dropbox", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;!-- generic variables that were populated by TokenAssist --&gt;
        ///[H : PowerName = &quot;__POWER_NAME__&quot;]
        ///
        ///&lt;!-- html power card, as found in the compendium --&gt;
        ///[H : PowerCard = &quot;__POWER_CARD__&quot;]
        ///
        ///&lt;!-- show the user the power they are about to use --&gt;
        ///[H : InputPrompt = input(PowerName + &quot; | &lt;html&gt;&quot; + PowerCard + &quot;&lt;/html&gt; | | LABEL | SPAN = TRUE&quot;)]
        ///
        ///&lt;!-- bail if the user cancels the dialog --&gt;
        ///[H : abort(InputPrompt)]
        ///
        ///&lt;!-- show the power card to the other players --&gt;
        ///{PowerCard}.
        /// </summary>
        internal static string NoWeaponTemplate {
            get {
                return ResourceManager.GetString("NoWeaponTemplate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;!-- generic variables that were populated by TokenAssist --&gt;
        ///[H : PowerName = &quot;__POWER_NAME__&quot;]
        ///[H : AttackBonus = &quot;__ATTACK_BONUS__&quot;]
        ///[H : Damage = &quot;__DAMAGE__&quot;]
        ///[H : MaxDamage = eval(&quot;__MAX_DAMAGE__&quot;)]
        ///[H : MultipleTargets = __MULTIPLE_TARGETS__]
        ///
        ///&lt;!-- html power card, as found in the compendium --&gt;
        ///[H : PowerCard = &quot;__POWER_CARD__&quot;]
        ///
        ///&lt;!-- retrieve previously stored values --&gt;
        ///[H : NumberOfTargets = getStrProp(CombatStatus, &quot;LastNumberOfTargets&quot;)]
        ///[H : CombatAdvantage = getStrProp(CombatStatu [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string WeaponTemplate {
            get {
                return ResourceManager.GetString("WeaponTemplate", resourceCulture);
            }
        }
    }
}
