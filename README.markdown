SunshineAttack.Localization
----------------------------

Forked https://github.com/jgauffin/griffin.mvccontrib to focus on localization and using RavenDB as the primary data provider. The following changes have been made:

* Removed membership provider features
* Added ASP.NET MVC 5 support
* Updated and removed dependencies as needed

Current features
----------------

* Easy model, view and validation localization without ugly attributes.
* HtmlHelpers that allows you to extend them or modify the generated HTML before it's written to the view.
* Base structure for JSON responses to allow integration between different plugins.
* Web-based administration

### Usage
To localize text in your Razor Views, use the `@T("text")` alias method:
```html
    <div id="content">
        <h2>@T("Welcome to my web app!")</h2>
        <h3><span>@T("Marketing slogan here")</span></h3>
        <span class="button">
            <a href="@Url.Action("Plans", "Home", new { area = "" })">
                <strong>@T("BUY,BUY,BUY")</strong>
            </a>
        </span>
    </div>
```

### Installing

1. Add the following assembly references to your ASP.NET MVC 5 project:
```
* SunshineAttack.Localization.Admin
* SunshineAttack.Localization.RavenDb
* SunshineAttack.Localization
```

2. In your project's web.config, add the following elements to the `<configuration>` section and replace the `connectionString` with your own `url` and `database` information.:
```xml
<connectionStrings>
    <add name="RavenDB" connectionString="Url=http://YOURSERVER:8080;Database=SunshineAttack.Localization" />
  </connectionStrings>
```

3. Make sure your project's ***global.asax*** has most of the code from the [SunshineAttack.Localization.Admin.TestProject ***global.asax***](https://github.com/sunshine-attack/SunshineAttack.Localization/blob/master/source/SunshineAttack.Localization.Admin.TestProject/Global.asax.cs)




	



