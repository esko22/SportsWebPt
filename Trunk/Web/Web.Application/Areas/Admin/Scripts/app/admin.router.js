define('router', ['backbone', 'jquery', 'presenter', 'vm.admin.equipment', 'vm.admin.videos', 'vm.admin.exercise.page', 'vm.admin.plan.page', 'vm.admin.signs', 'vm.admin.causes', 'vm.admin.injury.page'],
    function (backbone, $, presenter, EquipmentVM, VideoVM, ExerciseVM, PlanVM, SignVM, CauseVM, InjuryVM) {

        var configure = function () {
            var mainRouter = backbone.Router.extend({
                routes: {
                    '': 'nav',
                    'videos': 'videos',
                    'exercises': 'exercises',
                    'equipment': 'equipment',
                    'plans': 'plans',
                    'injuries': 'injuries',
                    'signs': 'signs',
                    'causes' : 'causes',
                    '*actions': 'defaultRoute'
                }
            });

            var router = new mainRouter();

            router.on('route:defaultRoute', function () {
            });

            router.on('route:nav', function () {
                $('.view').hide();
                $('.active').removeClass('active');
            });

            router.on('route:videos', function () {
                $('.view').hide();
                $('.active').removeClass('active');
                kb.applyBindings(VideoVM, $('#admin-video-panel').get(0));
                presenter.transitionTo($('#admin-video-panel'), '', '');
            });

            router.on('route:signs', function () {
                $('.view').hide();
                $('.active').removeClass('active');
                kb.applyBindings(SignVM, $('#admin-sign-panel').get(0));
                presenter.transitionTo($('#admin-sign-panel'), '', '');
            });

            router.on('route:causes', function () {
                $('.view').hide();
                $('.active').removeClass('active');
                kb.applyBindings(CauseVM, $('#admin-cause-panel').get(0));
                presenter.transitionTo($('#admin-cause-panel'), '', '');
            });

            router.on('route:exercises', function () {
                $('.view').hide();
                $('.active').removeClass('active');
                ExerciseVM.bindViewModels();
                presenter.transitionTo($('#admin-exercise-panel'), '', '');
            });

            router.on('route:plans', function () {
                $('.view').hide();
                $('.active').removeClass('active');
                PlanVM.bindViewModels();
                presenter.transitionTo($('#admin-plan-panel'), '', '');
            });

            router.on('route:injuries', function () {
                $('.view').hide();
                $('.active').removeClass('active');
                InjuryVM.bindViewModels();
                presenter.transitionTo($('#admin-injury-panel'), '', '');
            });


            router.on('route:equipment', function () {
                $('.view').hide();
                $('.active').removeClass('active');
                kb.applyBindings(EquipmentVM, $('#admin-equipment-panel').get(0));
                presenter.transitionTo($('#admin-equipment-panel'), '', '');
            });

 
            backbone.history.start();
        };

        return {
            configure: configure
        };

    });