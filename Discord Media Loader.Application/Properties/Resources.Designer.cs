#region LICENSE
/**********************************************************************************************
 * Copyright (C) 2017-2019 - All Rights Reserved
 * 
 * This file is part of "DML.Application".
 * The official repository is hosted at https://github.com/Serraniel/DiscordMediaLoader
 * 
 * "DML.Application" is licensed under Apache 2.0.
 * Full license is included in the project repository.
 * 
 * Users who edited Resources.Designer.cs under the condition of the used license:
 * - Serraniel (https://github.com/Serraniel)
 **********************************************************************************************/
#endregion

namespace DML.Application.Properties {
    using System;
    
    
    /// <summary>
    ///   Eine stark typisierte Ressourcenklasse zum Suchen von lokalisierten Zeichenfolgen usw.
    /// </summary>
    // Diese Klasse wurde von der StronglyTypedResourceBuilder automatisch generiert
    // -Klasse über ein Tool wie ResGen oder Visual Studio automatisch generiert.
    // Um einen Member hinzuzufügen oder zu entfernen, bearbeiten Sie die .ResX-Datei und führen dann ResGen
    // mit der /str-Option erneut aus, oder Sie erstellen Ihr VS-Projekt neu.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Gibt die zwischengespeicherte ResourceManager-Instanz zurück, die von dieser Klasse verwendet wird.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("DML.Application.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Überschreibt die CurrentUICulture-Eigenschaft des aktuellen Threads für alle
        ///   Ressourcenzuordnungen, die diese stark typisierte Ressourcenklasse verwenden.
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
        ///   Sucht eine lokalisierte Zeichenfolge, die Discord Media Loader by Serraniel - Apache 2.0 License
        ///https://github.com/Serraniel/DiscordMediaLoader/
        ///
        ///Made with:
        ///SweetLib (Copyright (c) 2017 Serraniel - GNU General Public License v3.0)
        ///Discord.Net (Copyright (c) 2015 RogueException - MIT License)
        ///Newtonsoft.Json (Copyright (c) 2007 James Newton-King - MIT License)
        ///Nito.AsyncEx (Copyright (c) 2014 StephenCleary - MIT License)
        ///RestSharp (Copyright (c) restsharp - Apache 2.0 License)
        ///WebSocket4Net (Copyright (c) kerryjiang - Apache 2.0 License)
        /// [Rest der Zeichenfolge wurde abgeschnitten]&quot;; ähnelt.
        /// </summary>
        internal static string AboutString {
            get {
                return ResourceManager.GetString("AboutString", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Ressource vom Typ System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap Serraniel_Logo4_NO_BG {
            get {
                object obj = ResourceManager.GetObject("Serraniel_Logo4_NO_BG", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
    }
}
