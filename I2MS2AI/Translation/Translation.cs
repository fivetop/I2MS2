using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Markup;
using System.Windows;
using System.ComponentModel;
using System.Windows.Data;
using System.Globalization;
using System.Threading;
using System.Resources;
using System.Reflection;
using System.Collections;

// Localization of a WPF application using a custom MarkupExtension
// http://www.wpftutorial.net/LocalizeMarkupExtension.html

namespace I2MS2.Translation
{
    // 한영 처리 
    public class MainWindowViewModel
    {
        public ICollectionView Languages { get; private set; }

        public MainWindowViewModel()
        {
            Languages = CollectionViewSource.GetDefaultView(TranslationManager.Instance.Languages);

            Languages.CurrentChanged += (s, e) => TranslationManager.Instance.CurrentLanguage = (CultureInfo)Languages.CurrentItem;
        }
    }

    public class TranslateExtension : MarkupExtension
    {
        private string _key;

        public TranslateExtension(string key)
        {
            _key = key;
        }

        [ConstructorArgument("key")]
        public string Key
        {
            get { return _key; }
            set { _key = value; }
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            var binding = new Binding("Value")
            {
                Source = new TranslationData(_key)
            };
            return binding.ProvideValue(serviceProvider);
        }
    }

    public class TranslationData : IWeakEventListener,
                      INotifyPropertyChanged, IDisposable
    {
        private string _key;

        public TranslationData(string key)
        {
            _key = key;
            LanguageChangedEventManager.AddListener(
                      TranslationManager.Instance, this);
        }

        ~TranslationData()
        {
            Dispose(false);
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                LanguageChangedEventManager.RemoveListener(
                          TranslationManager.Instance, this);
            }
        }


        public object Value
        {
            get
            {
                var r = TranslationManager.Instance.Translate(_key);
                return r;
            }
        }

        public bool ReceiveWeakEvent(Type managerType,
                                object sender, EventArgs e)
        {
            if (managerType == typeof(LanguageChangedEventManager))
            {
                OnLanguageChanged(sender, e);
                return true;
            }
            return false;
        }

        private void OnLanguageChanged(object sender, EventArgs e)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("Value"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }


    public class TranslationManager
    {
        private static TranslationManager _translationManager;

        public static void set_lang(int lang_id)
        {
            switch (lang_id)
            {
                case 1080001:
                    TranslationManager.Instance.CurrentLanguage = new CultureInfo("ko-KR");
                    break;
                default:
                    TranslationManager.Instance.CurrentLanguage = new CultureInfo("en-US");
                    break;
            }
        }

        public event EventHandler LanguageChanged;

        public CultureInfo CurrentLanguage
        {
            get { return Thread.CurrentThread.CurrentUICulture; }
            set
            {
                if (value != Thread.CurrentThread.CurrentUICulture)
                {
                    Thread.CurrentThread.CurrentUICulture = value;
                    OnLanguageChanged();
                }
            }
        }

        public IEnumerable<CultureInfo> Languages
        {
            get
            {
                if (TranslationProvider != null)
                {
                    return TranslationProvider.Languages;
                }
                return Enumerable.Empty<CultureInfo>();
            }
        }

        public static TranslationManager Instance
        {
            get
            {
                if (_translationManager == null)
                    _translationManager = new TranslationManager();
                return _translationManager;
            }
        }

        public ITranslationProvider TranslationProvider { get; set; }

        private void OnLanguageChanged()
        {
            if (LanguageChanged != null)
            {
                LanguageChanged(this, EventArgs.Empty);
            }
        }

        public object Translate(string key)
        {
            if (TranslationProvider != null)
            {
                object translatedValue = TranslationProvider.Translate(key);
                if (translatedValue != null)
                {
                    return translatedValue;
                }
            }
            return string.Format("!{0}!", key);
        }
    }


    public class LanguageChangedEventManager : WeakEventManager
    {
        public static void AddListener(TranslationManager source, IWeakEventListener listener)
        {
            CurrentManager.ProtectedAddListener(source, listener);
        }

        public static void RemoveListener(TranslationManager source, IWeakEventListener listener)
        {
            CurrentManager.ProtectedRemoveListener(source, listener);
        }

        private void OnLanguageChanged(object sender, EventArgs e)
        {
            DeliverEvent(sender, e);
        }

        protected override void StartListening(object source)
        {
            var manager = (TranslationManager)source;
            manager.LanguageChanged += OnLanguageChanged;
        }

        protected override void StopListening(Object source)
        {
            var manager = (TranslationManager)source;
            manager.LanguageChanged -= OnLanguageChanged;
        }

        private static LanguageChangedEventManager CurrentManager
        {
            get
            {
                Type managerType = typeof(LanguageChangedEventManager);
                var manager = (LanguageChangedEventManager)GetCurrentManager(managerType);
                if (manager == null)
                {
                    manager = new LanguageChangedEventManager();
                    SetCurrentManager(managerType, manager);
                }
                return manager;
            }
        }

    }

    public interface ITranslationProvider
    {
        /// <summary>
        /// Translates the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        object Translate(string key);

        /// <summary>
        /// Gets the available languages.
        /// </summary>
        /// <value>The available languages.</value>
        IEnumerable<CultureInfo> Languages { get; }

    }

    public class ResxTranslationProvider : ITranslationProvider
    {
        #region Private Members

        private readonly ResourceManager _resourceManager;
        #endregion


        private Dictionary<string, string> rMap = new Dictionary<string, string>();


        #region Construction

        /// <summary>
        /// Initializes a new instance of the <see cref="ResxTranslationProvider"/> class.
        /// </summary>
        /// <param name="baseName">Name of the base.</param>
        /// <param name="assembly">The assembly.</param>
        public ResxTranslationProvider(string baseName, Assembly assembly)
        {
            _resourceManager = new ResourceManager(baseName, assembly);

            //try
            //{
            //    String uiString = _resourceManager.GetString("C_Error_Server_Domain_2");
            //    Console.WriteLine(uiString);
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine("Exception. code={0}, message={1}", e.HResult, e.Message);
            //}
            // ...
            //// romee 2015.07.09 로컬언어 지원을 위한 resx 파일 읽기 처리 - 실행 폴더에 항상 파일이 존재 하여야 함 없으면 
            //ResXResourceReader _resx = new ResXResourceReader(@"lang.resx");
            //foreach (DictionaryEntry d in _resx)
            //{
            //    rMap.Add(d.Key.ToString(), d.Value.ToString());
                
            //    Console.WriteLine(d.Key.ToString() + " : " + d.Value); // ...
            //}
            //_resx.Close();
        }

        #endregion

        #region ITranslationProvider Members

        /// <summary>
        /// See <see cref="ITranslationProvider.Translate" />
        /// </summary>
        public object Translate(string key)
        {
            try
            { 
                var r = _resourceManager.GetString(key);
                return r;
            }
            catch(Exception e)
            {
                Console.WriteLine("Exception. code={0}, message={1}", e.HResult, e.Message);
            }
            return null;
        }


        #endregion

        #region ITranslationProvider Members

        /// <summary>
        /// See <see cref="ITranslationProvider.AvailableLanguages" />
        /// </summary>
        public IEnumerable<CultureInfo> Languages
        {
            get
            {
                // TODO: Resolve the available languages
                yield return new CultureInfo("ko-KR");
                yield return new CultureInfo("en-US");
            }
        }

        #endregion


    }
}
