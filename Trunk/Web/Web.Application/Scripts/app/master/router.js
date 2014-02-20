define('router', ['backbone', 'presenter', 'config', 'vm.examine.page', 'vm.splash', 'vm.research.page', 'vm.dashboard.page', 'youtube.video.manager'],
    function (backbone, presenter, config, examinePage, splashPage, researchPage, dashboardPage, youtubeVideoManager) {

        var configure = function() {
            var mainRouter = backbone.Router.extend({
                routes: {
                    '': 'main',
                    'examine': 'examine',
                    'examine/detail': 'examineDetail',
                    'examine/report': 'examineReport',
                    'research': 'research',
                    'research/injuries': 'researchInjury',
                    'research/injuries/:searchKey': 'researchInjuryDetail',
                    'research/plans': 'researchPlan',
                    'research/plans/:searchKey': 'researchPlanDetail',
                    'research/locations': 'researchLocate',
                    'research/exercises': 'researchExercise',
                    'research/exercises/:searchKey': 'researchExerciseDetail',
                    'dashboard': 'dashboard',
                    '*notfound': 'notfound'
                }
            });

            var router = new mainRouter();

            router.on('route:notfound', function () {
                config.notifier.error('route not found');
            });

            router.on('route:examine', function () {
                youtubeVideoManager.destroyAll();
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
                youtubeVideoManager.destroyAll();
                if (examinePage.canShowDetail()) {
                    examinePage.showDetail();
                    transitionTo(config.viewIds.examineDetail);
                } else {
                    window.location.hash = '/examine';
                }
            });

            router.on('route:examineReport', function () {
                youtubeVideoManager.destroyAll();
                if (examinePage.canShowReport()) {
                    //TODO: Diag Nav Temp
                    //examinePage.isVisible(true);
                    examinePage.showReport();
                    transitionTo(config.viewIds.examineReport);
                } else {
                    window.location.hash = '/examine';
                }
            });

            router.on('route:main', function () {
                youtubeVideoManager.destroyAll();
                examinePage.isVisible(false);
                researchPage.isVisible(false);
                dashboardPage.isVisible(false);
                splashPage.isVisible(true);
            });
            
            router.on('route:research', function () {
                youtubeVideoManager.destroyAll();
                if (!researchPage.isVisible()) {
                    showResearch();
                }
                transitionTo(config.viewIds.researchNav);
            });
            
            router.on('route:researchInjury', function () {
                youtubeVideoManager.destroyAll();
                if (!researchPage.isVisible()) {
                    showResearch();
                }
                researchPage.showInjuries();
                transitionTo(config.viewIds.researchInjury);
            });
            
            router.on('route:researchInjuryDetail', function (searchKey) {
                youtubeVideoManager.destroyAll();
                if (!researchPage.isVisible()) {
                    showResearch();
                }
                showResearchDetail('injury', searchKey);
            });

            
            router.on('route:researchPlan', function () {
                youtubeVideoManager.destroyAll();
                if (!researchPage.isVisible()) {
                    showResearch();
                }
                researchPage.showPlans();
                transitionTo(config.viewIds.researchPlan);
            });
            
            router.on('route:researchPlanDetail', function (searchKey) {
                youtubeVideoManager.destroyAll();
                if (!researchPage.isVisible()) {
                    showResearch();
                }
                showResearchDetail('plan', searchKey);
            });

            router.on('route:researchExercise', function () {
                youtubeVideoManager.destroyAll();
                if (!researchPage.isVisible()) {
                    showResearch();
                }
                researchPage.showExercises();
                transitionTo(config.viewIds.researchExercise);
            });
            
            router.on('route:researchExerciseDetail', function (searchKey) {
                youtubeVideoManager.destroyAll();
                if (!researchPage.isVisible()) {
                    showResearch();
                }
                showResearchDetail('exercise', searchKey);
            });

            router.on('route:researchLocate', function () {
                youtubeVideoManager.destroyAll();
                if (!researchPage.isVisible()) {
                    showResearch();
                }
                researchPage.showLocations();
                transitionTo(config.viewIds.researchLocate);
            });

            router.on('route:dashboard', function () {
                youtubeVideoManager.destroyAll();
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
        
        function showResearchDetail(detailType, searchKey) {
            switch(detailType) {
                case 'injury':
                    researchPage.showInjuryDetail(searchKey);
                    transitionTo(config.viewIds.researchInjuryDetail);
                    break;
                case 'plan':
                    researchPage.showPlanDetail(searchKey);
                    transitionTo(config.viewIds.researchPlanDetail);
                    break;
                case 'exercise':
                    researchPage.showExerciseDetail(searchKey);
                    transitionTo(config.viewIds.researchExerciseDetail);
                    break;
                default:
                    break;
            }
        }
        
        return {
            configure: configure
        };

    });