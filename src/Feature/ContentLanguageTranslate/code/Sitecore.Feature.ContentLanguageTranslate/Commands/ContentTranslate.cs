using Sitecore.Data;
using Sitecore.Shell.Framework.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Feature.ContentLanguageTranslate.Models;
using Sitecore.Data.Items;
using Sitecore.Data.Fields;
using Sitecore.Diagnostics;
using System.Collections.Specialized;
using Sitecore.Web.UI.Sheer;
using Google.Cloud.Translation.V2;
using Sitecore.Data.Managers;

namespace Sitecore.Feature.ContentLanguageTranslate.Commands
{
    public class ContentTranslate : Command
    {
        public override void Execute(CommandContext context)
        {
            try
            {
                Database dbweb = Database.GetDatabase(CTConfiguration.SitecoreMasterDatabase);
                Item configurationitem = dbweb.GetItem(new ID(CTConfiguration.ItemId));
                CheckboxField __enableCT = configurationitem.Fields[CTConfiguration.EnableCT];
                if (__enableCT.Checked)
                {
                    Assert.ArgumentNotNull(context, "context");

                    if (context.Items.Length == 1)
                    {
                        NameValueCollection parameters = new NameValueCollection();
                        parameters["items"] = base.SerializeItems(context.Items);
                        parameters.Add(context.Parameters);
                        Context.ClientPage.Start(this, "Run", parameters);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("ContentTranslate -Execute - " + ex.Message, typeof(ContentTranslate));
            }
        }

        protected void Run(ClientPipelineArgs args)
        {
            try
            {
                Item item = base.DeserializeItems(args.Parameters["items"])[0];
                if (item == null)
                {
                    return;
                }
                Item englishVersion = item.Database.GetItem(item.ID, Sitecore.Globalization.Language.Parse("en"));
                if (args.Parameters["id"] != null)
                {
                    IEnumerable<Sitecore.Globalization.Language> languages = LanguageManager.GetLanguages(item.Database).Where(a => a != Sitecore.Globalization.Language.Parse("en"));
                    if (languages.Any())
                    {
                        if (args.IsPostBack)
                        {
                            if (args.Result == "yes")
                            {
                                if (SheerResponse.CheckModified())
                                {

                                    foreach (Sitecore.Globalization.Language language in languages)
                                    {
                                        Item localizedItem = item.Database.GetItem(item.ID, language);
                                        try
                                        {
                                            localizedItem.Editing.BeginEdit();
                                            localizedItem.Versions.AddVersion();
                                            localizedItem.Editing.EndEdit();

                                            foreach (Field field in englishVersion.Fields.Where(x => (FieldTypeManager.GetField(x) is HtmlField) || (FieldTypeManager.GetField(x) is TextField)))
                                            {
                                                if (!IsStandardTemplateField(field) && ((FieldTypeManager.GetField(field) is HtmlField) || (FieldTypeManager.GetField(field) is TextField)))
                                                {
                                                    var enLanguageValue = field.Value;
                                                    if (!string.IsNullOrEmpty(enLanguageValue))
                                                    {
                                                        //// call google translate api.
                                                        string translatedValue = ConvertToTargetLanguage(enLanguageValue, language.CultureInfo.TwoLetterISOLanguageName, "en", (FieldTypeManager.GetField(field) is HtmlField), true);

                                                        localizedItem.Editing.BeginEdit();
                                                        localizedItem.Fields[field.ID].Value = translatedValue;
                                                        localizedItem.Editing.EndEdit();
                                                    }
                                                }
                                            }
                                        }
                                        catch (Google.GoogleApiException ex)
                                        {
                                            localizedItem.Versions.RemoveVersion();
                                            localizedItem.Editing.EndEdit();
                                            SheerResponse.Alert(ex.Message);
                                            return;
                                        }
                                    }
                                    SheerResponse.Alert("Translation is successfully done.");

                                }
                            }
                        }
                        else
                        {
                            SheerResponse.Confirm("Are you sure to translate in all languages ?");
                            args.WaitForPostBack();
                        }
                    }
                    else
                    {
                        SheerResponse.Alert("No languages available in language manager.");
                    }
                }
                else
                {
                    if (SheerResponse.CheckModified())
                    {
                        Item localizedItem = item.Database.GetItem(item.ID, item.Language);

                        try
                        {
                            localizedItem.Editing.BeginEdit();
                            localizedItem.Versions.AddVersion();

                            foreach (Field field in englishVersion.Fields.Where(x => (FieldTypeManager.GetField(x) is HtmlField) || (FieldTypeManager.GetField(x) is TextField)))
                            {
                                if (!IsStandardTemplateField(field) && ((FieldTypeManager.GetField(field) is HtmlField) || (FieldTypeManager.GetField(field) is TextField)))
                                {
                                    var enLanguageValue = field.Value;
                                    if (!string.IsNullOrEmpty(enLanguageValue))
                                    {
                                        //// call google translate api.
                                        string translatedValue = ConvertToTargetLanguage(enLanguageValue, item.Language.CultureInfo.TwoLetterISOLanguageName, "en", (FieldTypeManager.GetField(field) is HtmlField), false);
                                        if (string.Equals(translatedValue, "translation failure"))
                                        {
                                            //SheerResponse.Alert(string.Format("language code '{0}' not supported to translate.", item.Language.CultureInfo.TwoLetterISOLanguageName));
                                            SheerResponse.Alert("Selected language is not supported by google to translate.");
                                            localizedItem.Versions.RemoveVersion();
                                            localizedItem.Editing.EndEdit();
                                            return;
                                        }
                                        localizedItem.Editing.BeginEdit();
                                        localizedItem.Fields[field.ID].Value = translatedValue;
                                        localizedItem.Editing.EndEdit();
                                    }
                                }
                            }
                            SheerResponse.Alert("Translation is successfully done.");
                        }
                        catch (Google.GoogleApiException ex)
                        {
                            localizedItem.Versions.RemoveVersion();
                            localizedItem.Editing.EndEdit();
                            SheerResponse.Alert(ex.Message);
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("ContentTranslate - Run - " + ex.Message, typeof(ContentTranslate));
            }
        }

        private string ConvertToTargetLanguage(string text, string toLang, string fromLang, bool isHtml, bool isMultipleTranslation)
        {
            Database dbweb = Database.GetDatabase(CTConfiguration.SitecoreMasterDatabase);
            Item configurationitem = dbweb.GetItem(new ID(CTConfiguration.ItemId));
            string Key = configurationitem.Fields[CTConfiguration.AccessKey] != null ? configurationitem.Fields[CTConfiguration.AccessKey].Value : string.Empty;
            TranslationClient translationClient = TranslationClient.CreateFromApiKey(Key);

            TranslationResult result = null;
            IList<Google.Cloud.Translation.V2.Language> response = translationClient.ListLanguages();
            bool IsValidLangCode = response.Any(x => x.Code == toLang);
            if (!IsValidLangCode && !isMultipleTranslation)
            {
                return "translation failure";
            }
            else
            {
                if (IsValidLangCode)
                {
                    if (isHtml)
                        result = translationClient.TranslateHtml(text, toLang, fromLang);
                    else
                        result = translationClient.TranslateText(text, toLang, fromLang);
                }
                else
                {
                    result = translationClient.TranslateText(text, "en");
                }
                return result.TranslatedText;
            }

        }


        private bool IsStandardTemplateField(Field field)
        {
            Sitecore.Data.Templates.Template template = Sitecore.Data.Managers.TemplateManager.GetTemplate(
              Sitecore.Configuration.Settings.DefaultBaseTemplate,
              field.Database);
            Sitecore.Diagnostics.Assert.IsNotNull(template, "template");
            return template.ContainsField(field.ID);
        }

        public override CommandState QueryState(CommandContext context)
        {
            Database dbweb = Database.GetDatabase(CTConfiguration.SitecoreMasterDatabase);
            Item configurationitem = dbweb.GetItem(new ID(CTConfiguration.ItemId));
            CheckboxField __enableCT = configurationitem.Fields[CTConfiguration.EnableCT];

            if (__enableCT.Checked)
            {

                Error.AssertObject((object)context, "context");
                if (context.Items.Length == 0)
                {
                    return CommandState.Disabled;
                }
                if (!context.Items[0].Paths.IsContentItem)
                {
                    return CommandState.Disabled;
                }
                if (context.Items[0].Locking.IsLocked())
                {
                    return CommandState.Disabled;
                }
                if (context.Items[0].Language.CultureInfo.TwoLetterISOLanguageName == "en")
                {
                    return CommandState.Disabled;
                }
                return base.QueryState(context);
            }
            return CommandState.Hidden;
        }
    }
}