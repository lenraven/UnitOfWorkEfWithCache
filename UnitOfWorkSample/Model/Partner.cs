using System;
using System.Collections.Generic;
using System.Linq;

namespace UnitOfWorkSample.Model
{
    public class Partner
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Organization> Organizations { get; set; }
        public ICollection<LocaleConnection> Locales { get; private set; }

        public Partner()
        {
            Locales = new List<LocaleConnection>();
        }

        public void AddLocales(IEnumerable<Locale> locales)
        {
            var newLocales = locales.Where(lc => !Locales.Select(p => p.Locale.Id).Contains(lc.Id)).ToArray();

            foreach (var newLocale in newLocales)
            {
                Locales.Add(new LocaleConnection { Partner = this, Locale = newLocale});
            }
        }

        public void RemoveLocales(IEnumerable<Locale> locales)
        {
            var oldLocales = Locales.Where(lc => locales.Select(p => p.Id).Contains(lc.Locale.Id)).ToArray();

            foreach (var oldLocale in oldLocales)
            {
                Locales.Remove(oldLocale);
            }
        }

        public class LocaleConnection
        {
            public Partner Partner { get; set; }
            public Locale Locale { get; set; }
        }
    }
}
