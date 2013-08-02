define('router', ['backbone', 'jquery', 'presenter', 'knockback', 'vm.research.exercise.listing', 'vm.research.plan.listing', 'vm.research.injury.listing'],
    function (backbone, $, presenter, kb, exerciseVm, planVm, injuryVm) {

        var configure = function () {
            var mainRouter = backbone.Router.extend({
                routes: {
                    '': 'nav',
                    'injuries': 'injuries',
                    'plans': 'plans',
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
                $('.view').hide();
                $('.active').removeClass('active');
                planVm.init();
                kb.applyBindings(planVm, $('#research-plan-panel').get(0));
                presenter.transitionTo($('#research-plan-panel'), '', '');
            });

            router.on('route:injuries', function () {
                $('.view').hide();
                $('.active').removeClass('active');
                injuryVm.init();
                kb.applyBindings(injuryVm, $('#research-injury-panel').get(0));
                presenter.transitionTo($('#research-injury-panel'), '', '');
            });

            router.on('route:exercises', function () {
                $('.view').hide();
                $('.active').removeClass('active');
                exerciseVm.init();
                kb.applyBindings(exerciseVm, $('#research-exercise-panel').get(0));
                presenter.transitionTo($('#research-exercise-panel'), '', '');
            });


            backbone.history.start();
        };

        return {
            configure: configure
        };

    });