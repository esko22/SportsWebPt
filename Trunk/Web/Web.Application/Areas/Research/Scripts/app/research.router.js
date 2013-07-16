define('router', ['backbone', 'jquery', 'presenter', 'knockback', 'vm.research.exercises'],
    function (backbone, $, presenter, kb, exerciseVm) {

        var configure = function () {
            var mainRouter = backbone.Router.extend({
                routes: {
                    '': 'nav',
                    'injuries': 'injuries',
                    'plan': 'plan',
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
                presenter.transitionTo($('#research-nav-panel'),'','');
            });
            
            router.on('route:plans', function () {
            });

            router.on('route:injuries', function () {
                $('.view').hide();
                $('.active').removeClass('active');
                presenter.transitionTo($('#research-injury-panel'), '', '');
            });

            router.on('route:exercises', function () {
                $('.view').hide();
                $('.active').removeClass('active');
                kb.applyBindings(exerciseVm, $('#research-exercise-panel').get(0));
                presenter.transitionTo($('#research-exercise-panel'), '', '');
            });


            backbone.history.start();
        };

        return {
            configure: configure
        };

    });