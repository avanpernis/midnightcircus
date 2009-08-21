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
        ///[H : FeatName = &quot;__FEAT_NAME__&quot;]
        ///
        ///&lt;!-- html feat card, as found in the compendium --&gt;
        ///[H : FeatCard = &quot;__FEAT_CARD__&quot;]
        ///
        ///&lt;!-- show the user the feat they have selected --&gt;
        ///[H : InputPrompt = input(FeatName + &quot; | &lt;html&gt;&quot; + FeatCard + &quot;&lt;/html&gt; | | LABEL | SPAN = TRUE&quot;)]
        ///
        ///&lt;!-- bail if the user cancels the dialog --&gt;
        ///[H : abort(InputPrompt)]
        ///
        ///&lt;!-- show the feat to the other players --&gt;
        ///{FeatCard}.
        /// </summary>
        internal static string FeatTemplate {
            get {
                return ResourceManager.GetString("FeatTemplate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;!-- our token is of type 4ePlayer which is defined in the campaign properties file --&gt;
        ///{setPropertyType(&quot;4ePlayer&quot;)}
        ///
        ///&lt;!-- our token is a PC token --&gt;
        ///{setPC()}.
        /// </summary>
        internal static string HeaderTemplate {
            get {
                return ResourceManager.GetString("HeaderTemplate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;!-- The Power ID is a list of numbers that identify the power. A Unique number is required for each use of the power. No power should use a number that is part of another Power&apos;s List unless their usage is tied together like Cleric&apos;s Channel Divinity powers --&gt;
        ///[H:CurrentPowerID=&quot;__POWER_ID__&quot;]
        ///[H:CurrentPowerUses=listCount(&quot;&quot;+CurrentPowerID)]
        ///[H:Used = 0]
        ///[H:Reuse = 0]
        ///[H:PowersUsed = getStrProp(CombatStatus, &quot;__USAGE_TYPE__PowersUsed&quot;)]
        ///[H:PowersUsed = if(PowersUsed == &quot;&quot;, 0, PowersUsed)]
        ///[H,C(Cur [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string LimitedUseTemplate {
            get {
                return ResourceManager.GetString("LimitedUseTemplate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;!-- generic variables that were populated by TokenAssist --&gt;
        ///[H : MacroName = &quot;__MACRO_NAME__&quot;]
        ///[H : MacroBackgroundColor = &quot;__MACRO_BACKGROUND_COLOR__&quot;]
        ///[H : MacroForegroundColor = &quot;__MACRO_FOREGROUND_COLOR__&quot;]
        ///[H : MacroGroup = &quot;__MACRO_GROUP__&quot;]
        ///[H : MacroCode = &quot;__MACRO_CODE__&quot;]
        ///
        ///&lt;!-- construct the macro properties --&gt;
        ///[H : MacroProperties = &quot;autoExecute=true;&quot;]
        ///[H : MacroProperties = setStrProp(MacroProperties, &quot;color&quot;, MacroBackgroundColor)]
        ///[H : MacroProperties = setStrProp(MacroProperties [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string MacroCreationTemplate {
            get {
                return ResourceManager.GetString("MacroCreationTemplate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;!-- generic variables that were populated by TokenAssist --&gt;
        ///[H : MagicItemName = &quot;__MAGIC_ITEM_NAME__&quot;]
        ///
        ///&lt;!-- html magic item card, as found in the compendium --&gt;
        ///[H : MagicItemCard = &quot;__MAGIC_ITEM_CARD__&quot;]
        ///
        ///&lt;!-- show the user the magic item they are about to use --&gt;
        ///[H : InputPrompt = input(MagicItemName + &quot; | &lt;html&gt;&quot; + MagicItemCard + &quot;&lt;/html&gt; | | LABEL | SPAN = TRUE&quot;)]
        ///
        ///&lt;!-- bail if the user cancels the dialog --&gt;
        ///[H : abort(InputPrompt)]
        ///
        ///&lt;!-- show the magic item card to the other players  [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string MagicItemTemplate {
            get {
                return ResourceManager.GetString("MagicItemTemplate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;div style=&apos;width: 600;&apos;&gt;
        ///  &lt;h1 style=&apos;font-size: 1.09em; line-height: 2; padding-left: 15px; margin: 0; color: #ffffff; background: #619869;&apos;&gt;Melee Basic Attack&lt;/h1&gt;
        ///  &lt;p style=&apos;padding-left: color: #3e141e; display: block; padding: 2px 15px; margin: 0; background: #d6d6c2;&apos;&gt;
        ///    &lt;i&gt;You strike at a nearby foe.&lt;/i&gt;
        ///  &lt;/p&gt;
        ///&lt;/div&gt;.
        /// </summary>
        internal static string MeleeBasicAttack {
            get {
                return ResourceManager.GetString("MeleeBasicAttack", resourceCulture);
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
        ///   Looks up a localized string similar to &lt;div style=&apos;width: 600;&apos;&gt;
        ///  &lt;h1 style=&apos;font-size: 1.09em; line-height: 2; padding-left: 15px; margin: 0; color: #ffffff; background: #619869;&apos;&gt;Melee Basic Attack&lt;/h1&gt;
        ///  &lt;p style=&apos;padding-left: color: #3e141e; display: block; padding: 2px 15px; margin: 0; background: #d6d6c2;&apos;&gt;
        ///    &lt;i&gt;You strike at a nearby foe.&lt;/i&gt;
        ///  &lt;/p&gt;
        ///&lt;/div&gt;.
        /// </summary>
        internal static string RangedBasicAttack {
            get {
                return ResourceManager.GetString("RangedBasicAttack", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;div style=&apos;width: 600;&apos;&gt;&lt;h1 style=&apos;font-size: 1.09em; line-height: 2; padding-left: 15px; margin: 0; color: #ffffff; background: #961334;&apos;&gt;Second Wind&lt;/h1&gt;&lt;p style=&apos;padding-left: color: #3e141e; display: block; padding: 2px 15px; margin: 0; background: #d6d6c2;&apos;&gt;&lt;i&gt;You dig into your resolve and endurance to find an extra burst of vitality.&lt;/i&gt;&lt;/p&gt;&lt;p style=&apos;padding-left: color: #3e141e; padding: 0px 0px 0px 15px; margin: 0; background: #ffffff;&apos;&gt;&lt;b&gt;Encounter&lt;/b&gt;&amp;nbsp;&amp;nbsp; &lt;img src=&apos;http://www.wizards.com/ [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string SecondWind {
            get {
                return ResourceManager.GetString("SecondWind", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;!-- generic variables that were populated by TokenAssist --&gt;
        ///[H : PowerName = &quot;__POWER_NAME__&quot;]
        ///[H : WeaponList = &quot;__WEAPON_LIST__&quot;]
        ///[H : AttackBonusList = &quot;__ATTACK_BONUS_LIST__&quot;]
        ///[H : DamageList = &quot;__DAMAGE_LIST__&quot;]
        ///[H : MaxDamageList = &quot;__MAX_DAMAGE_LIST__&quot;]
        ///[H : CriticalDamageList = &quot;__CRITICAL_DAMAGE_LIST__&quot;]
        ///[H : AttackStatList = &quot;__ATTACK_STAT_LIST__&quot;]
        ///[H : DefenseList = &quot;__DEFENSE_LIST__&quot;]
        ///[H : MultipleTargets = __MULTIPLE_TARGETS__]
        ///
        ///&lt;!-- html power card, as found in the compendium --&gt;        /// [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string WeaponTemplate {
            get {
                return ResourceManager.GetString("WeaponTemplate", resourceCulture);
            }
        }
    }
}
