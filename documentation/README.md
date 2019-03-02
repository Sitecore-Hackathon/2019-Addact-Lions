   
  # [](#documentation)Documentation

Check below links for documentation

-   [Technical Guide](2019-Addact-Lions/documentation/ContentTranslationGuide_TechGuide_V1.docx)
-   [Users Guide](2019-Addact-Lions/documentation/ContentTranslation_UserGuide_V1.docx)


## [](#summary)Summary

**Category:** Best enhancement to the Sitecore Admin (XP) UI for Content Editors & Marketers

This Module "Content Translate" is designed to help content managers or marketers to easily translate the language via Google Translate. To translate multiple versions for an item is very long and lot of back and forth needs to be done. This module is your on the go tranlation tool from English as base language using Google Translate API. New language version will be auto-generated using this tool.

## [](#pre-requisites)Pre-requisites

Does your module rely on other Sitecore modules or frameworks? No

 - List any dependencies
    - "Newtonsoft.Json.dll" should be upgraded to 10.0.2 or above Install
    - NuGet Package bundle "Google.Cloud.Translation.V2"
 - Services
    - Google Translate Service API Key is required

## [](#installation)Installation

Check below links for documentation

-   [Technical Guide](2019-Addact-Lions/documentation/ContentTranslationGuide_TechGuide_V1.docx)
-   [Users Guide](2019-Addact-Lions/documentation/ContentTranslation_UserGuide_V1.docx)

1.  Use the Sitecore Installation wizard to install the [Sitecore Package](#link-to-package)

## [](#configuration)Configuration

How do you configure your module once it is installed? Are there items that need to be updated with settings, or maybe config files need to have keys updated?

Remember you are using Markdown, you can provide code samples too:

<?xml  version="1.0"  encoding="utf-8"?>
<!--
 Purpose: Configuration settings for my hackathon module
-->
<configuration  xmlns:patch="http://www.sitecore.net/xmlconfig/"  xmlns:role="http://www.sitecore.net/xmlconfig/role/"  xmlns:xdt="http://schemas.microsoft.com/XML-Document-Transform">
<sitecore  role:require="Standalone or ContentManagement">
<commands>
<command  name="ContentTranslate:Translate"  type="Sitecore.Feature.ContentLanguageTranslate.Commands.ContentTranslate,Sitecore.Feature.ContentLanguageTranslate" />
</commands>
</sitecore>
</configuration>


## [](#usage)Usage

Translate Item using Google Translate

[![Team](https://www.addact.in/wp-content/uploads/2019/03/lion-bones-for-asian-market2017-02-080.jpg "Team")](https://www.addact.in/wp-content/uploads/2019/03/lion-bones-for-asian-market2017-02-080.jpg)

## [](#video)Video

[![Sitecore Hackathon 2019 Video Team Addact Lions]](https://www.youtube.com/watch?v=UDHWfsr67e4&feature=youtu.be)

Go

-   © 2019 GitHub, Inc.
-   [Terms](https://github.com/site/terms)
-   [Privacy](https://github.com/site/privacy)
-   [Security](https://github.com/security)
-   [Status](https://githubstatus.com/)
-   [Help](https://help.github.com)

[](https://github.com "GitHub")

-   [Contact GitHub](https://github.com/contact)
-   [Pricing](https://github.com/pricing)
-   [API](https://developer.github.com)
-   [Training](https://training.github.com)
-   [Blog](https://github.blog)
-   [About](https://github.com/about)

You can’t perform that action at this time.

You signed in with another tab or window. Reload to refresh your session. You signed out in another tab or window. Reload to refresh your session.
