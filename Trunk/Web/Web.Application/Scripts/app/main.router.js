define('router', ['backbone', 'presenter', 'config'],
    function (backbone, presenter, config) {

        var configure = function() {
            var mainRouter = backbone.Router.extend({
                routes: {
                    '': 'main',
                    '*actions': 'defaultRoute'
                }
            });

            var router = new mainRouter();

            router.on('route:defaultRoute', function() {
            });

            router.on('route:main', function() {
                transitionTo(config.viewIds.mainSplash);
            });

            backbone.history.start();
        };
            
        function transitionTo(viewId) {
            presenter.transitionTo($(viewId), '', '');
        }

        
        return {
            configure: configure
        };

    });