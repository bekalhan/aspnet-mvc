using Microsoft.Extensions.Localization;

namespace WP.Models
{
    public interface ILanguageService
    {
        public LocalizedString Getkey(string key);
    }
}