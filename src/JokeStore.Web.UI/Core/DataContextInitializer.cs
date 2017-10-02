using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JokeStore.Core.Entity;
using JokeStore.Core.Repository.EntityFramework;

namespace JokeStore.Web.Core
{
    public class DataContextInitializer// : DropCreateDatabaseIfModelChanges<DataContext>
    {
        //protected override void Seed(DataContext context)
        public static void Seed(DataContext context)
        {
            //Domains
            Domain domain;
            if (context.Domains.Count() == 0)
            {
                domain = new Domain
                {
                    Url = "localhost",
                    Heading = "JokeStore",
                    SubHeading = "Vtípky, vtípky, vtípky ..."
                };
                context.Domains.Add(domain);
            }
            else
            {
                domain = context.Domains.FirstOrDefault(d => d.Url == "localhost");
            }

            //Entries
            if (context.Entries.Count() == 0)
            {
                context.Entries.Add(new Entry
                {
                    Content = @"Tři manželské páry mají zájem vstoupit do ortodoxní sekty, kde podminkou pro iniciaci je čtrnáctidenní sexuální abstinence.
Přijdou na přijímací pohovor a vrchní starší se jich ptá, jestli dodrželi tuto podmínku.
První manžel odpovídá: - Jistě, bez problému, naše víra je silnější než pudy.
Druhý manžel odpovídá: - No, druhý týden jsem si musel jít lehnout na gauč v obýváku, ale vydrželi jsme.
Třetí manžel v rozpacích říká: - Ze začátku to šlo, ale potom manželka sahala na polici pro plechovku s fazolemi, ta jí spadla a jak se pro ní ohnula, tak jsem se neovládnul...
- No ale to k nám nemůžete.
- Já vím. Ostatně, do Delvity od té doby taky ne ...",
                    Created = DateTime.Now,
                    Approved = true,
                    Category = "Sex",
                    CategoryUrl = "sex",
                    Domain = domain
                });
                context.Entries.Add(new Entry
                {
                    Content = @"Paní rodí doma. Je u ní doktor a vypadne proud.
Zavolá tedy pětiletého synka, aby svítil baterkou.
Porod se podaří, doktor plácne mimino po zadečku a syn s očima navrch hlavy vyhrkne: 'Správně! A ještě druhou přes hubu, neměl tam lízt!'",
                    Created = DateTime.Now,
                    Approved = true,
                    Category = "Doktoři",
                    CategoryUrl = "doktori",
                    Domain = domain
                });
                context.Entries.Add(new Entry
                {
                    Content = @"'Pane doktore, můžu dostat pohlavní nemoc na záchodě?'
'Můžete, ale v posteli je to pohodlnější!'",
                    Created = DateTime.Now,
                    Approved = true,
                    Category = "Doktoři",
                    CategoryUrl = "doktori",
                    Domain = domain
                });
                context.Entries.Add(new Entry
                {
                    Content = @"'Haló, policie? Jel jsem autem a srazil jsem dvě slepice.'
'No, tak je položte na krajnici. Ať je další auta nerozmažou po vozovce.'
'Rozumím. A co mám udělat s jejich motorkou?'",
                    Created = DateTime.Now,
                    Approved = true,
                    Category = "Policajti",
                    CategoryUrl = "policajti",
                    Domain = domain
                });
                context.Entries.Add(new Entry
                {
                    Content = @"Přijde policajt za náčelníkem a říká:
'Pane náčelníku, tady v tomhle hlášení máte chybu, tady máte napsáno fčela.'
Náčelník se podívá:
'No, máte pravdu, má tam být fčera.'",
                    Created = DateTime.Now,
                    Approved = true,
                    Category = "Policajti",
                    CategoryUrl = "policajti",
                    Domain = domain
                });
                context.Entries.Add(new Entry
                {
                    Content = @"Ide Franta pulnocni Prahou, kdyz ho zastavi hlidka. 'Obcansky prukaz, obcane!' Franta ho poda tomu nejvyssimu, ten si ho prohlizi a za chvilku se pta: 'vase jmeno?' A Franta odpovi: 'Ten co umi cist uz u vas nedela?'",
                    Created = DateTime.Now,
                    Approved = true,
                    Category = "Policajti",
                    CategoryUrl = "policajti",
                    Domain = domain
                });
            }

            context.SaveChanges();
        }
    }
}