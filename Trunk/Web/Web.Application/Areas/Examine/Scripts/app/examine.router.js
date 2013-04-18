define('router', ['backbone','jquery','presenter'],
    function (backbone,$, presenter) {

        var configure = function () {
            var mainRouter = backbone.Router.extend({
                routes: {
                    '': 'main',
                    'crap': 'defaultRoute'
                }
            });

            var router = new mainRouter();

            router.on('route:defaultRoute', function () {
                $('.view').hide();
                presenter.transitionTo(
                    $('#page2'),
                    '',
                    '');
                
            });
            router.on('route:main', function() {
                $('.view').hide();
                presenter.transitionTo(
                    $('#skeletor-container'),
                    '',
                    '');
            });

            backbone.history.start();
        };

        return {
            configure: configure
        };

    });