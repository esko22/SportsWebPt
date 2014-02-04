define('router', ['backbone', 'jquery', 'knockback', 'presenter', 'vm.admin.equipment', 'vm.admin.videos', 'vm.admin.exercise.page', 'vm.admin.plan.page',
        'vm.admin.signs', 'vm.admin.causes', 'vm.admin.injury.page', 'vm.admin.body.parts', 'vm.admin.body.regions', 'vm.admin.treatments', 'vm.admin.prognoses'],
    function (backbone, $, kb, presenter, EquipmentVM, VideoVM, ExerciseVM, PlanVM, SignVM, CauseVM, InjuryVM, BodyPartVM, BodyRegionVM, TreatmentVM, PrognosesVM) {

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
                    'bodypart': 'bodypart',
                    'bodyregion': 'bodyregion',
                    'treatments' : 'treatments',
                    'prognoses': 'prognoses',
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
                VideoVM.init();
                presenter.transitionTo($('#admin-video-panel'), '', '');
            });

            router.on('route:signs', function () {
                $('.view').hide();
                $('.active').removeClass('active');
                SignVM.init();
                presenter.transitionTo($('#admin-sign-panel'), '', '');
            });
            
            router.on('route:bodypart', function () {
                $('.view').hide();
                $('.active').removeClass('active');
                BodyPartVM.init();
                presenter.transitionTo($('#admin-body-part-panel'), '', '');
            });
            
            router.on('route:bodyregion', function () {
                $('.view').hide();
                $('.active').removeClass('active');
                BodyRegionVM.init();
                presenter.transitionTo($('#admin-body-region-panel'), '', '');
            });

            router.on('route:causes', function () {
                $('.view').hide();
                $('.active').removeClass('active');
                CauseVM.init();
                presenter.transitionTo($('#admin-cause-panel'), '', '');
            });

            router.on('route:treatments', function () {
                $('.view').hide();
                $('.active').removeClass('active');
                TreatmentVM.init();
                presenter.transitionTo($('#admin-treatment-panel'), '', '');
            });

            router.on('route:prognoses', function () {
                $('.view').hide();
                $('.active').removeClass('active');
                PrognosesVM.init();
                presenter.transitionTo($('#admin-prognosis-panel'), '', '');
            });

            router.on('route:exercises', function () {
                $('.view').hide();
                $('.active').removeClass('active');
                ExerciseVM.init();
                presenter.transitionTo($('#admin-exercise-panel'), '', '');
            });

            router.on('route:plans', function () {
                $('.view').hide();
                $('.active').removeClass('active');
                PlanVM.init();
                presenter.transitionTo($('#admin-plan-panel'), '', '');
            });

            router.on('route:injuries', function () {
                $('.view').hide();
                $('.active').removeClass('active');
                InjuryVM.init();
                presenter.transitionTo($('#admin-injury-panel'), '', '');
            });


            router.on('route:equipment', function () {
                $('.view').hide();
                $('.active').removeClass('active');
                EquipmentVM.init();
                presenter.transitionTo($('#admin-equipment-panel'), '', '');
            });

 
            backbone.history.start();
        };

        return {
            configure: configure
        };

    });