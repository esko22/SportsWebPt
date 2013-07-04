define('router', ['backbone', 'jquery', 'presenter'],
    function (backbone, $, presenter) {

        var configure = function () {
            var mainRouter = backbone.Router.extend({
                routes: {
                    '': 'nav',
                    'videos': 'videos',
                    'exercises': 'exercises',
                    '*actions': 'defaultRoute'
                }
            });

            var router = new mainRouter();

            router.on('route:defaultRoute', function () {
            });

            router.on('route:nav', function () {
                $('.view').hide();
                $('.active').removeClass('active');
                presenter.transitionTo($('#admin-nav-panel'),'','');
            });

            router.on('route:videos', function () {
                $('.view').hide();
                $('.active').removeClass('active');
                presenter.transitionTo($('#admin-video-panel'), '', '');
            });

            router.on('route:exercises', function () {
                $('.view').hide();
                $('.active').removeClass('active');
                presenter.transitionTo($('#admin-exercise-panel'), '', '');
            });

 
            backbone.history.start();
        };

        return {
            configure: configure
        };

    });