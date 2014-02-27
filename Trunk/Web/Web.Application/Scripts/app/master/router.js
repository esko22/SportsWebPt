define('router', ['backbone', 'ko', 'presenter', 'config', 'vm.examine.page', 'vm.splash', 'vm.research.page', 'vm.dashboard.page', 'youtube.video.manager', 'vm.about.page', 'vm.contact.page'],
    function (backbone, ko, presenter, config, examinePage, splashPage, researchPage, dashboardPage, youtubeVideoManager, aboutPage, contactPage) {

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
                    'about': 'about',
                    'contact' : 'contact',
                    '*notfound': 'notfound'
                }
            });

            var router = new mainRouter();
            var activeElement;

            router.on('route:notfound', function () {
                config.notifier.error('route not found');
            });

            router.on('route:examine', function () {
                cleanUp();
                if (!examinePage.isVisible()) {
                    researchPage.isVisible(false);
                    splashPage.isVisible(false);
                    dashboardPage.isVisible(false);
                    contactPage.isVisible(false);
                    aboutPage.isVisible(false);
                    examinePage.onVisible();
                }
                examinePage.showSkeleton();
                transitionTo(config.viewIds.examineSkeleton);
            });

            router.on('route:examineDetail', function () {
                cleanUp();
                if (examinePage.canShowDetail()) {
                    examinePage.showDetail();
                    transitionTo(config.viewIds.examineDetail);
                } else {
                    window.location.hash = '/examine';
                }
            });

            router.on('route:examineReport', function () {
                cleanUp();
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
                cleanUp();
                examinePage.isVisible(false);
                researchPage.isVisible(false);
                dashboardPage.isVisible(false);
                contactPage.isVisible(false);
                aboutPage.isVisible(false);
                splashPage.isVisible(true);
            });
            
            router.on('route:research', function () {
                cleanUp();
                if (!researchPage.isVisible()) {
                    showResearch();
                }
                transitionTo(config.viewIds.researchNav);
            });
            
            router.on('route:researchInjury', function () {
                cleanUp();
                if (!researchPage.isVisible()) {
                    showResearch();
                }
                researchPage.showInjuries();
                transitionTo(config.viewIds.researchInjury);
            });
            
            router.on('route:researchInjuryDetail', function (searchKey) {
                cleanUp();
                if (!researchPage.isVisible()) {
                    showResearch();
                }
                showResearchDetail('injury', searchKey);
            });

            
            router.on('route:researchPlan', function () {
                cleanUp();
                if (!researchPage.isVisible()) {
                    showResearch();
                }
                researchPage.showPlans();
                transitionTo(config.viewIds.researchPlan);
            });
            
            router.on('route:researchPlanDetail', function (searchKey) {
                cleanUp();
                if (!researchPage.isVisible()) {
                    showResearch();
                }
                showResearchDetail('plan', searchKey);
            });

            router.on('route:researchExercise', function () {
                cleanUp();
                if (!researchPage.isVisible()) {
                    showResearch();
                }
                researchPage.showExercises();
                transitionTo(config.viewIds.researchExercise);
            });
            
            router.on('route:researchExerciseDetail', function (searchKey) {
                cleanUp();
                if (!researchPage.isVisible()) {
                    showResearch();
                }
                showResearchDetail('exercise', searchKey);
            });

            router.on('route:researchLocate', function () {
                cleanUp();
                if (!researchPage.isVisible()) {
                    showResearch();
                }
                researchPage.showLocations();
                transitionTo(config.viewIds.researchLocate);
            });

            router.on('route:dashboard', function () {
                cleanUp();
                if (!dashboardPage.isVisible()) {
                    showDashboard();
                }
            });

            router.on('route:about', function () {
                cleanUp();
                if (!aboutPage.isVisible()) {
                    showAbout();
                }
            });

            router.on('route:contact', function () {
                cleanUp();
                if (!contactPage.isVisible()) {
                    showContact();
                }
            });


            backbone.history.start();
        };
            
        function transitionTo(viewId) {
            this.activeElement = $(viewId);
            presenter.transitionTo(this.activeElement, '', '');
        }

        function cleanUp() {
            if (this.activeElement) {
                ko.removeNode(this.activeElement);
            }
            youtubeVideoManager.destroyAll();
            backbone.Relational.store.reset();
        }

        function showDashboard() {
            examinePage.isVisible(false);
            splashPage.isVisible(false);
            dashboardPage.isVisible(true);
            researchPage.isVisible(false);
            contactPage.isVisible(false);
            aboutPage.isVisible(false);
        }

        function showResearch() {
            examinePage.isVisible(false);
            splashPage.isVisible(false);
            dashboardPage.isVisible(false);
            contactPage.isVisible(false);
            aboutPage.isVisible(false);
            researchPage.onVisible();
        }

        function showAbout() {
            examinePage.isVisible(false);
            splashPage.isVisible(false);
            dashboardPage.isVisible(false);
            researchPage.isVisible(false);
            contactPage.isVisible(false);
            aboutPage.onVisible();
        }


        function showContact() {
            examinePage.isVisible(false);
            splashPage.isVisible(false);
            dashboardPage.isVisible(false);
            researchPage.isVisible(false);
            aboutPage.isVisible(false);
            contactPage.onVisible();
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