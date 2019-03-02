   
  # [](#documentation)Documentation

The documentation for this years Hackathon must be provided as a readme in Markdown format as part of your submission.

You can find a very good reference to Github flavoured markdown reference in [this cheatsheet](https://github.com/adam-p/markdown-here/wiki/Markdown-Cheatsheet). If you want something a bit more WYSIWYG for editing then could use [StackEdit](https://stackedit.io/app) which provides a more user friendly interface for generating the Markdown code. Those of you who are [VS Code fans](https://code.visualstudio.com/docs/languages/markdown#_markdown-preview) can edit/preview directly in that interface too.

Examples of things to include are the following.

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

Provide detailed instructions on how to install the module, and include screenshots where necessary.

-   [Technical Guide](#link-to-package)
-   [Users Guide](#link-to-package)

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

Please provide a video highlighing your Hackathon module submission and provide a link to the video. Either a [direct link](https://www.youtube.com/watch?v=EpNhxW4pNKk) to the video, upload it to this documentation folder or maybe upload it to Youtube...

[![Sitecore Hackathon Video Embedding Alt Text](https://camo.githubusercontent.com/4b4783d38a116f8ef5d606982781b8eb25ec9938/68747470733a2f2f696d672e796f75747562652e636f6d2f76692f45704e68785734704e4b6b2f302e6a7067)](https://www.youtube.com/watch?v=EpNhxW4pNKk)

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
