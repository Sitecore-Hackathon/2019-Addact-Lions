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
            }
            catch (Exception ex)
            {
                Log.Error("ContentTranslate - Run - " + ex.Message, typeof(ContentTranslate));
            }
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