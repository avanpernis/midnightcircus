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
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;!-- Confirm that the user has am action point --&gt;
        ///[H : NoActionPointsAlert = if(ActionPoints &lt; 1, &apos;input(&quot;UNUSED | No Action Points Left | Warning | LABEL&quot;)&apos;, &quot;ActionPoints&quot;)]
        ///[H : eval(NoActionPointsAlert)]
        ///[H : abort(if(ActionPoints &lt; 1, 0, 1))]
        ///
        ///&lt;!-- Confirm the user&apos;s decision to use an action point --&gt;
        ///[H : InputPrompt = input(&quot;SpendActionPoint | 1 | Spend Action Point? | CHECK&quot;)]
        ///
        ///&lt;!-- bail if the user cancels the dialog --&gt;
        ///[H : abort(InputPrompt)]
        ///[H : abort(if(SpendActionPoint == 0, 0, 1 [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string ActionPointTemplate {
            get {
                return ResourceManager.GetString("ActionPointTemplate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;!-- generic variables that were populated by TokenAssist --&gt;
        ///[H : CheckNameList = &quot;__CHECK_NAME_LIST__&quot;]
        ///[H : CheckBonusList = &quot;__CHECK_BONUS_LIST__&quot;]
        ///
        ///&lt;!-- retrieve previously stored values --&gt;
        ///[H : LastCheckChoice = getStrProp(CombatStatus, &quot;LastCheckChoice&quot;)]
        ///[H : LastTempCheckBonus = getStrProp(CombatStatus, &quot;LastTempCheckBonus&quot;)]
        ///
        ///&lt;!-- build a list of checks and bonuses to display in order to faciliate selection by the user --&gt;
        ///[H : CheckNameAndBonusList = &quot;&quot;]
        ///[H, C(listCount(CheckNameList)) [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string CheckTemplate {
            get {
                return ResourceManager.GetString("CheckTemplate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to .
        /// </summary>
        internal static string ConsumableTemplate {
            get {
                return ResourceManager.GetString("ConsumableTemplate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;!-- The Power ID is a list of numbers that identify the power. A Unique number is required for each use of the power. No power should use a number that is part of another Power&apos;s List unless their usage is tied together like Cleric&apos;s Channel Divinity powers --&gt;
        ///[H : CurrentPowerID = &quot;__POWER_ID__&quot;]
        ///[H : CurrentPowerUses = listCount(&quot;&quot; + CurrentPowerID)]
        ///[H : Used = 0]
        ///[H : Reuse = 0]
        ///[H : PowersUsed = getStrProp(CombatStatus, &quot;__USAGE_TYPE__PowersUsed&quot;)]
        ///[H ,C(CurrentPowerUses) : Used = Used + if(ban [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string DailyItemTemplate {
            get {
                return ResourceManager.GetString("DailyItemTemplate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;!-- show the user damage options --&gt;
        ///[H : InputPrompt = input(
        ///	&quot;UNUSED | &quot; + CurrentHitPoints + &quot; / &quot; + MaxHitPoints + if(TempHitPoints &gt; 0, &quot; (Temp: &quot; + TempHitPoints + &quot;)&quot;, &quot;&quot;) + &quot; | Hit Points | Label&quot;,
        ///	&quot;UNUSED | &quot; + CurrentHealingSurges + &quot; / &quot; + MaxHealingSurges + &quot; (Value: &quot; + HealingSurgeValue + &quot;) | Healing Surges | Label&quot;,
        ///	&quot;UNUSED | &lt;html&gt;&lt;/html&gt; | | LABEL | SPAN = TRUE&quot;,
        ///	&quot;DamageAmount | &quot; + getStrProp(CombatStatus, &quot;LastDamageAmount&quot;) + &quot; | Amount of Damage | TEXT&quot;)]
        ///
        ///&lt;!-- bail if the  [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string DamageTemplate {
            get {
                return ResourceManager.GetString("DamageTemplate", resourceCulture);
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
        ///{setPC()}
        ///
        ///&lt;!-- character details --&gt;
        ///{setProperty(&quot;Level&quot;, __LEVEL__)}
        ///{setProperty(&quot;HalfLevel&quot;, floor(__LEVEL__ / 2))}
        ///{setProperty(&quot;Speed&quot;, __SPEED__)}
        ///{setProperty(&quot;ActionPoints&quot;, 1)}
        ///{setProperty(&quot;DailyItemUses&quot;, ceiling(__LEVEL__ / 10))}
        ///
        ///&lt;!-- ability scores --&gt;
        ///{setProperty(&quot;Strength&quot;, __STRENGTH__)}
        ///{setProperty(&quot;Dexterity&quot;, __DEXTERIT [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string HeaderTemplate {
            get {
                return ResourceManager.GetString("HeaderTemplate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;!-- show the user healing options --&gt;
        ///[H : InputPrompt = input(
        ///	&quot;UNUSED | &quot; + CurrentHitPoints + &quot; / &quot; + MaxHitPoints + if(TempHitPoints &gt; 0, &quot; (Temp: &quot; + TempHitPoints + &quot;)&quot;, &quot;&quot;) + &quot; | Hit Points | Label&quot;,
        ///	&quot;UNUSED | &quot; + CurrentHealingSurges + &quot; / &quot; + MaxHealingSurges + &quot; (Value: &quot; + HealingSurgeValue + &quot;) | Healing Surges | Label&quot;,
        ///	&quot;UNUSED | &lt;html&gt;&lt;/html&gt; | | LABEL | SPAN = TRUE&quot;,
        ///	&quot;HealingAmount | &quot; + getStrProp(CombatStatus, &quot;LastHealingAmount&quot;) + &quot; | Amount to Heal | TEXT&quot;,
        ///	&quot;SpendHealingSurge [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string HealingTemplate {
            get {
                return ResourceManager.GetString("HealingTemplate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;!-- generic variables that were populated by TokenAssist --&gt;
        ///[H : InitBonus = &quot;__INIT_BONUS__&quot;]
        ///
        ///&lt;!-- retrieve previously stored values --&gt;
        ///[H : LastTempInitBonus = getStrProp(CombatStatus, &quot;LastTempInitBonus&quot;)]
        ///
        ///[H : InputPrompt = input(
        ///   &quot;UNUSED | Bonus (+&quot; + InitBonus + &quot;) | Roll Initiative | LABEL&quot;,
        ///   &quot;TempInitBonus | &quot; + getStrProp(CombatStatus, &quot;LastTempInitBonus&quot;) + &quot; | Temporary bonus | TEXT&quot;)]
        ///
        ///&lt;!-- bail if the user cancels the dialog --&gt;
        ///[H : abort(InputPrompt)]
        ///
        ///&lt;!-- store the va [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string InitiativeTemplate {
            get {
                return ResourceManager.GetString("InitiativeTemplate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;!-- The Power ID is a list of numbers that identify the power. A Unique number is required for each use of the power. No power should use a number that is part of another Power&apos;s List unless their usage is tied together like Cleric&apos;s Channel Divinity powers --&gt;
        ///[H : CurrentPowerID = &quot;__POWER_ID__&quot;]
        ///[H : CurrentPowerUses = listCount(&quot;&quot; + CurrentPowerID)]
        ///[H : Used = 0]
        ///[H : Reuse = 0]
        ///[H : PowersUsed = getStrProp(CombatStatus, &quot;__USAGE_TYPE__PowersUsed&quot;)]
        ///[H ,C(CurrentPowerUses) : Used = Used + if(ban [rest of string was truncated]&quot;;.
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
        ///   Looks up a localized string similar to [H : CurrentMagicItemID = &quot;__POWER_ID__&quot;]
        ///[H : MagicItemsUsed = getStrProp(CombatStatus, &quot;MagicItemsUsed&quot;)]
        ///[H : Used = if(band(2^eval(&quot;&quot; + CurrentMagicItemID), MagicItemsUsed), 1, 0)]
        ///[H : Recharge = 0]
        ///[H : UsedAlert = if(Used, &apos;input(&quot;UNUSED | Recharge Item | This Magic Item has already been Used | LABEL&quot;, &quot;Recharge | 0 | Recharge this item? | CHECK&quot;) &apos;, &quot;Used&quot;)]
        ///[H : eval(UsedAlert)]
        ///[H : abort(if(Used &amp;&amp; !Recharge, 0, 1))]
        ///
        ///&lt;!-- If recharging, confirm that the user has a healing surge --&gt;
        ///[H : [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string MagicItemHealingSurgeTemplate {
            get {
                return ResourceManager.GetString("MagicItemHealingSurgeTemplate", resourceCulture);
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
        ///   Looks up a localized string similar to &lt;div style=&apos;width: 600;&apos;&gt;&lt;h1 style=&apos;font-size: 1.09em; line-height: 2; padding-left: 15px; margin: 0; color: #ffffff; background: #619869;&apos;&gt;Melee Basic Attack: Basic Attack&lt;/h1&gt;&lt;p style=&apos;padding-left: color: #3e141e; display: block; padding: 2px 15px; margin: 0; background: #d6d6c2;&apos;&gt;&lt;i&gt;You resort to the simple attack you learned when you first picked up a melee weapon.&lt;/i&gt;&lt;/p&gt;&lt;p style=&apos;padding-left: color: #3e141e; padding: 0px 0px 0px 15px; margin: 0; background: #ffffff;&apos;&gt;&lt;b&gt;At-Will&lt;/b&gt;&amp;nbsp;&amp;nbsp; &lt;img  [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string MeleeBasicAttack {
            get {
                return ResourceManager.GetString("MeleeBasicAttack", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;!-- Confirm the user&apos;s reaching of a milestone --&gt;
        ///[H : InputPrompt = input(&quot;ReachedMilestone | 1 | Reached a Milestone? | CHECK&quot;)]
        ///
        ///&lt;!-- bail if the user cancels the dialog --&gt;
        ///[H : abort(InputPrompt)]
        ///[H : abort(if(ReachedMilestone == 0, 0, 1))]
        ///
        ///&lt;!-- award an action point and a daily item use --&gt;
        ///[H : ActionPoints = ActionPoints + 1]
        ///[H : DailyItemUses = DailyItemUses + 1]
        ///
        ///&lt;!-- output the results --&gt;
        ///You reached a Milestone.&lt;br&gt;
        ///You have &lt;b&gt;{ActionPoints}&lt;/b&gt; Action Point{if(ActionPoints &gt; [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string MilestoneTemplate {
            get {
                return ResourceManager.GetString("MilestoneTemplate", resourceCulture);
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
        ///   Looks up a localized string similar to &lt;div style=&apos;width: 600;&apos;&gt;&lt;h1 style=&apos;font-size: 1.09em; line-height: 2; padding-left: 15px; margin: 0; color: #ffffff; background: #619869;&apos;&gt;Ranged Basic Attack: Basic Attack&lt;/h1&gt;&lt;p style=&apos;padding-left: color: #3e141e; display: block; padding: 2px 15px; margin: 0; background: #d6d6c2;&apos;&gt;&lt;i&gt;You resort to the simple attack you learned when you first picked up a ranged weapon.&lt;/i&gt;&lt;/p&gt;&lt;p style=&apos;padding-left: color: #3e141e; padding: 0px 0px 0px 15px; margin: 0; background: #ffffff;&apos;&gt;&lt;b&gt;At-Will&lt;/b&gt;&amp;nbsp;&amp;nbsp; &lt;im [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string RangedBasicAttack {
            get {
                return ResourceManager.GetString("RangedBasicAttack", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;!-- show the user rest options --&gt;
        ///[H : InputPrompt = input(
        ///	&quot;RestType | Short, Extended | How long are you resting? | RADIO&quot;)]
        ///
        ///&lt;!-- bail if the user cancels the dialog --&gt;
        ///[H : abort(InputPrompt)]
        ///
        ///&lt;!-- regardless of rest type, all encounter powers are restored --&gt;
        ///[H: CombatStatus = setStrProp(CombatStatus, &quot;EncounterPowersUsed&quot;, 0), CombatStatus)]
        ///
        ///[H:blah = &quot;&quot;]
        ///
        ///&lt;!-- restore the text color so encounter powers appear as usable once again --&gt;
        ///[H, FOREACH(index, getMacroGroup(&quot;Encounter&quot;)), [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string RestTemplate {
            get {
                return ResourceManager.GetString("RestTemplate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;!-- generic variables that were populated by TokenAssist --&gt;
        ///[H : SaveBonus = &quot;__SAVE_BONUS__&quot;]
        ///
        ///&lt;!-- retrieve previously stored values --&gt;
        ///[H : LastTempSaveBonus = getStrProp(CombatStatus, &quot;LastTempSaveBonus&quot;)]
        ///
        ///[H : InputPrompt = input(
        ///   &quot;UNUSED | Bonus (+&quot; + SaveBonus + &quot;) | Saving Throw | LABEL&quot;,
        ///   &quot;TempSaveBonus | &quot; + getStrProp(CombatStatus, &quot;LastTempSaveBonus&quot;) + &quot; | Temporary bonus | TEXT&quot;)]
        ///
        ///&lt;!-- bail if the user cancels the dialog --&gt;
        ///[H : abort(InputPrompt)]
        ///
        ///&lt;!-- store the value [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string SavingThrowTemplate {
            get {
                return ResourceManager.GetString("SavingThrowTemplate", resourceCulture);
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
        ///   Looks up a localized string similar to &lt;!-- show the user healing options --&gt;
        ///[H : InputPrompt = input(
        ///	&quot;UNUSED | &quot; + CurrentHitPoints + &quot; / &quot; + MaxHitPoints + if(TempHitPoints &gt; 0, &quot; (Temp: &quot; + TempHitPoints + &quot;)&quot;, &quot;&quot;) + &quot; | Hit Points | Label&quot;,
        ///	&quot;UNUSED | &quot; + CurrentHealingSurges + &quot; / &quot; + MaxHealingSurges + &quot; (Value: &quot; + HealingSurgeValue + &quot;) | Healing Surges | Label&quot;,
        ///	&quot;UNUSED | &lt;html&gt;&lt;/html&gt; | | LABEL | SPAN = TRUE&quot;,
        ///	&quot;TempHPAmount | &quot; + getStrProp(CombatStatus, &quot;LastTempHPAmount&quot;) + &quot; | Amount of Temp HP | TEXT&quot;,
        ///	&quot;TempHPStacks | &quot; [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string TempHPTemplate {
            get {
                return ResourceManager.GetString("TempHPTemplate", resourceCulture);
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
