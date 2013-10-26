define('router', ['backbone'],
    function (backbone) {

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

            router.on('route:main', function () {
            });

            backbone.history.start();
        };
        
        return {
            configure: configure
        };

    });