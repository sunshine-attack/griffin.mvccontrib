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

Read the wiki for a more detailed introduction.

### Usage
To localize text in your application, use the `T("text")` alias method. Here's an example of localizing text in a Razor view:

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


	



