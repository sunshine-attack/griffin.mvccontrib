SunshineAttack.Localization
----------------------------

Forked https://github.com/jgauffin/griffin.mvccontrib to focus on localization and RavenDB. The following changes have been made:

* Removed membership provider features
* Added ASP.NET MVC 5 support
* Updated dependencies as needed

Current features
----------------

Read the wiki for a more detailed introduction.

* Easy model, view and validation localization without ugly attributes.
* HtmlHelpers that allows you to extend them or modify the generated HTML before it's written to the view.
* Base structure for JSON responses to allow integration between different plugins.
* Administration area for localization administration

Installation (nuget)
--------------------

	// base package
    install-package griffin.mvccontrib
	
	// administration area
	install-package griffin.mvccontrib.admin

	// ravendb and localization storage
	install-package griffin.mvccontrib.ravendb
	
	



