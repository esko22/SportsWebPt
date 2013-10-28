(function() {
    var root = this;
    
    define3rdPartyModules();
    loadPluginsAndBoot();

    function define3rdPartyModules() {
        // These are already loaded via bundles. 
        // We define them and put them in the root object.
        define('jquery', [], function () { return root.jQuery; });
        define('ko', [], function () { return root.ko; });
        define('backbone', [], function () { return root.Backbone; });
        define('backbone-relational', [], function () { return root.Backbone.Relation; });
        define('infuser', [], function () { return root.infuser; });
        define('underscore', [], function () { return root._; });
        define('knockback', [], function () { return root.kb; });
        define('logger', [], function () { return root.logger; });
        define('uri', [], function() { return root.Uri; });
        define('kendo', [], function () { return root.kendo; });
        define('toastr', [], function () { return root.toastr; });
    }

    function loadPluginsAndBoot() {
        // Plugins must be loaded after jQuery and Knockout, 
        // since they depend on them.
        //requirejs([
        //        'ko.bindingHandlers',
        //        'ko.debug.helpers'
        //], boot);
        boot();
    }

    function boot() {
        require(['bootstrapper', 'shared.bootstrapper','router'],
            function(bs, sbs, router) {
                bs.run();
                sbs.run();
                
                if(typeof (router) !== "undefined")
                    router.configure();
            });
    }
    
})();