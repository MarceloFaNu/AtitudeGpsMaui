﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AtitudeGpsMauiApp.Resources {
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
    internal class AppResources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal AppResources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("AtitudeGpsMauiApp.Resources.AppResources", typeof(AppResources).Assembly);
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
        ///   Looks up a localized string similar to Este aplicativo de testes irá coletar sua posição GPS a cada intervalo de tempo predefinido. Os resultados das leituras poderão ser comparilhadas para análise ao término dos ciclos praticados pelo usuário. Você pode criar vários ciclos de leituras com diferentes configurações de precisão e sensibilidade..
        /// </summary>
        internal static string AvisoPrincipal {
            get {
                return ResourceManager.GetString("AvisoPrincipal", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Ajuste as opções conforme necessário.
        /// </summary>
        internal static string ConfigPageAviso {
            get {
                return ResourceManager.GetString("ConfigPageAviso", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Configurações do Monitor.
        /// </summary>
        internal static string ConfigPageTitulo {
            get {
                return ResourceManager.GetString("ConfigPageTitulo", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Pressione os controles de acordo com o status em tempo real para auxiliar na análise das estatísticas GPS..
        /// </summary>
        internal static string CopilotoPageAviso {
            get {
                return ResourceManager.GetString("CopilotoPageAviso", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Copiloto do Monitor.
        /// </summary>
        internal static string CopilotoPageTitulo {
            get {
                return ResourceManager.GetString("CopilotoPageTitulo", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Casas Decimais das Coordenadas.
        /// </summary>
        internal static string pkrCasasDecimaisText {
            get {
                return ResourceManager.GetString("pkrCasasDecimaisText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Distância Mínima Considerada (mts).
        /// </summary>
        internal static string pkrDistanciaMinimaText {
            get {
                return ResourceManager.GetString("pkrDistanciaMinimaText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Timeout da Solicitação de Coordenadas (seg).
        /// </summary>
        internal static string pkrLocationRequestTimeoutText {
            get {
                return ResourceManager.GetString("pkrLocationRequestTimeoutText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Coordenadas para media aritmética.
        /// </summary>
        internal static string pkrMediaAritmeticaText {
            get {
                return ResourceManager.GetString("pkrMediaAritmeticaText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Nível de Sensibilidade da Leitura.
        /// </summary>
        internal static string pkrNivelPrecisaoText {
            get {
                return ResourceManager.GetString("pkrNivelPrecisaoText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Intervalo Mínimo Entre os Ticks (seg).
        /// </summary>
        internal static string pkrTickIntervalText {
            get {
                return ResourceManager.GetString("pkrTickIntervalText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Monitor de Coordenadas.
        /// </summary>
        internal static string Titulo {
            get {
                return ResourceManager.GetString("Titulo", resourceCulture);
            }
        }
    }
}
