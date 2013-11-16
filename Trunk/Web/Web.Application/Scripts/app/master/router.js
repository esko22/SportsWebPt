define('router', ['backbone', 'presenter', 'config', 'vm.examine.page', 'vm.splash', 'vm.research.page', 'vm.dashboard.page'],
    function (backbone, presenter, config, examinePage, splashPage, researchPage, dashboardPage) {

        var configure = function() {
            var mainRouter = backbone.Router.extend({
                routes: {
                    '': 'main',
                    'examine': 'examine',
                    'examine/detail': 'examineDetail',
                    'examine/report': 'examineReport',
                    'research': 'research',
                    'research/injuries': 'researchInjury',
                    'research/plans': 'researchPlan',
                    'research/locations': 'researchLocate',
                    'research/exercises': 'researchExercise',
                    'dashboard' : 'dashboard',
                    '*actions': 'defaultRoute'
                }
            });

            var router = new mainRouter();

            router.on('route:defaultRoute', function() {
            });

            router.on('route:examine', function () {
                if (!examinePage.isVisible()) {
                    researchPage.isVisible(false);
                    splashPage.isVisible(false);
                    dashboardPage.isVisible(false);
                    examinePage.onVisible();
                }
                examinePage.showSkeleton();
                transitionTo(config.viewIds.examineSkeleton);
            });

            router.on('route:examineDetail', function () {
                if (examinePage.canShowDetail()) {
                    examinePage.showDetail();
                    transitionTo(config.viewIds.examineDetail);
                } else {
                    window.location.hash = '/examine';
                }
            });

            router.on('route:examineReport', function () {
                if (examinePage.canShowReport()) {
                    //TODO: Diag Nav Temp
                    examinePage.isVisible(true);
                    examinePage.showReport();
                    transitionTo(config.viewIds.examineReport);
                } else {
                    window.location.hash = '/examine';
                }
            });

            router.on('route:main', function () {
                examinePage.isVisible(false);
                researchPage.isVisible(false);
                dashboardPage.isVisible(false);
                splashPage.isVisible(true);
            });
            
            router.on('route:research', function () {
                if (!researchPage.isVisible()) {
                    showResearch();
                }
                transitionTo(config.viewIds.researchNav);
            });
            
            router.on('route:researchInjury', function () {
                if (!researchPage.isVisible()) {
                    showResearch();
                }
                researchPage.showInjuries();
                transitionTo(config.viewIds.researchInjury);
            });
            
            router.on('route:researchPlan', function () {
                if (!researchPage.isVisible()) {
                    showResearch();
                }
                researchPage.showPlans();
                transitionTo(config.viewIds.researchPlan);
            });

            router.on('route:researchExercise', function () {
                if (!researchPage.isVisible()) {
                    showResearch();
                }
                researchPage.showExercises();
                transitionTo(config.viewIds.researchExercise);
            });

            router.on('route:researchLocate', function () {
                if (!researchPage.isVisible()) {
                    showResearch();
                }
                researchPage.showLocations();
                transitionTo(config.viewIds.researchLocate);
            });

            router.on('route:dashboard', function () {
                if (!dashboardPage.isVisible()) {
                    showDashboard();
                }
            });


            backbone.history.start();
        };
            
        function transitionTo(viewId) {
            presenter.transitionTo($(viewId), '', '');
        }

        function showDashboard() {
            examinePage.isVisible(false);
            splashPage.isVisible(false);
            dashboardPage.isVisible(true);
            researchPage.isVisible(false);
        }

        function showResearch() {
            examinePage.isVisible(false);
            splashPage.isVisible(false);
            dashboardPage.isVisible(false);
            researchPage.onVisible();
        }
        
        return {
            configure: configure
        };

    });