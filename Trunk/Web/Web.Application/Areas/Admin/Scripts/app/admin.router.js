define('router', ['backbone', 'jquery', 'presenter','vm.admin.equipment'],
    function (backbone, $, presenter, Equipment) {

        var configure = function () {
            var mainRouter = backbone.Router.extend({
                routes: {
                    '': 'nav',
                    'videos': 'videos',
                    'exercises': 'exercises',
                    'equipment': 'equipment',
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

            router.on('route:equipment', function () {
                $('.view').hide();
                $('.active').removeClass('active');
                kb.applyBindings(Equipment, $('#admin-equipment-panel').get(0));
                presenter.transitionTo($('#admin-equipment-panel'), '', '');
            });

 
            backbone.history.start();
        };

        return {
            configure: configure
        };

    });